using CGestServer.Network;
using CGestServer.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using CGestServer.Utilities;
using System.Security.Cryptography;
using CGestServer.Forms;
using System.IO.Compression;

namespace CGestServer
{
    public partial class mainForm : Form
    {
        //Fichier de connection
        public XmlDocument connectionFile;

        public List<CGestClient> clientsList = new List<CGestClient>();

        //Variable de travail du listener
        public volatile bool listenerWork = true;

        //Base de donnée
        public CGestDatabase db;

        //Le listener
        CGestListener listener;

        //Valeur d'affichage de la form
        bool hidden = false;

        //********** CONSTRUCTEUR **********//

        public mainForm()
        {
            InitializeComponent();

            init();
        }

        //********** END CONSTRUCTEUR **********//

        //Fonction d'initialisation du serveur
        public void init()
        {
            startServerButton.Enabled = false;
            stopServeurButton.Enabled = false;

            writeConsole("Chargement du serveur ...");

            //Initialisation de graphics de la form
            initGraphics();

            //Initialisation des fichiers locaux du serveur
            if (!initFiles())
            {
                startServerButton.Enabled = true;

                return;
            }

            //Initialisation de la base de donnée
            if(!initDatabase())
            {
                startServerButton.Enabled = true;

                return;
            }

            db.initDataset();

            if (!db.updateRows())
            {
                startServerButton.Enabled = true;

                return;
            }

            //Initialisation du dataset

            //Initialisation de la couche réseau du serveur
            initServer();

            stopServeurButton.Enabled = true;
        }

        //********** FONCTION INITIALISATION DES FICHIERS **********//

        public bool checkConnectionInfoValiditie(string username, string password)
        {
            DataRowCollection connectInfoDatabase = db.Ds.Dataset.Tables["user"].Rows;



            foreach (DataRow dr in connectInfoDatabase)
            {
                if (dr.ItemArray[1].ToString().TrimEnd(' ') == username)
                {
                    if (HashSHA1(password) == dr.ItemArray[2].ToString().TrimEnd(' '))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public int getUserID(string username, string password)
        {
            DataRowCollection connectInfoDatabase = db.Ds.Dataset.Tables["user"].Rows;

            foreach (DataRow dr in connectInfoDatabase)
            {
                if (dr.ItemArray[1].ToString().TrimEnd(' ') == username)
                {
                    if (HashSHA1(password) == dr.ItemArray[2].ToString().TrimEnd(' '))
                    {
                        return int.Parse(dr.ItemArray[0].ToString());
                    }
                }
            }

            return -1;

        }

        public string getUsername(int id)
        {
            DataRowCollection connectInfoDatabase = db.Ds.Dataset.Tables["user"].Rows;

            foreach (DataRow dr in connectInfoDatabase)
            {
                if (dr.ItemArray[0].ToString() == id.ToString())
                {
                    return dr.ItemArray[1].ToString();
                }
            }

            return "refused";
        }

        public string getColor(int id)
        {
            DataRowCollection connectInfoDatabase = db.Ds.Dataset.Tables["user"].Rows;

            foreach (DataRow dr in connectInfoDatabase)
            {
                if (dr.ItemArray[0].ToString() == id.ToString())
                {
                    return dr.ItemArray[4].ToString();
                }
            }

            return "red";
        }

        public static string HashSHA1(string sInputString)
        {
            SHA1 sha = SHA1.Create();
            byte[] bHash = sha.ComputeHash(Encoding.UTF8.GetBytes(sInputString));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < bHash.Length; i++)
            {
                sBuilder.Append(bHash[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public bool initFiles()
        {
            //Verfication des dossiers
            if (!checkPathExist(CGestURLs.mainFolder.getUrlFromRoot()))
            {
                if (!createFolder(CGestURLs.mainFolder.getUrlFromRoot()))
                {
                    writeConsole("Impossible de lancer le serveur");

                    return false;
                }
            }

            //Verification des fichiers
            if (!checkFileExist(CGestURLs.connectionXmlFile.getUrlFromRoot()))
            {
                if (!createConnectionFile())
                {
                    writeConsole("Impossible de lancer le serveur");

                    return false;
                }
            }

            if (!checkFileExist(CGestURLs.configXmlFile.getUrlFromRoot()))
            {
                if (!createConfigFile())
                {
                    writeConsole("Impossible de lancer le serveur");

                    return false;
                }
            }

            if (!updateConfig(CGestURLs.configXmlFile.getUrlFromRoot()))
            {
                return false;
            }

            if (!updateDll(CGestURLs.configXmlFile.getUrlFromRoot()))
            {
                return false;
            }

            return true;

        }

        public bool updateConfig(string url)
        {
            try
            {
                XmlDocument configFile = new XmlDocument();

                configFile.Load(url);

                List<string> nodes = new List<string>();

                foreach (XmlNode n in configFile.DocumentElement.ChildNodes)
                {
                    nodes.Add(n.Name);
                }

                if (!nodes.Contains("TiersFileVersion"))
                {
                    XmlElement dllVersion = configFile.CreateElement("TiersFileVersion");
                    dllVersion.SetAttribute("CurrentVersion", "1.0.0");
                    configFile.DocumentElement.AppendChild(dllVersion);

                    configFile.Save(url);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public bool updateDll(string url)
        {
            List<string> tiersFiles = new List<string>();
            tiersFiles.Add("ColarsisUserControls.dll");
            tiersFiles.Add("Npgsql.dll");
            tiersFiles.Add("Mono.Security.dll");

            XmlDocument configFile = new XmlDocument();

            configFile.Load(url);

            string actualVersion = configFile.DocumentElement.GetElementsByTagName("Version")[0].Attributes[0].Value;

            try
            {
                if (processVersion(configFile.DocumentElement.GetElementsByTagName("TiersFileVersion")[0].Attributes[0].Value) < processVersion(actualVersion))
                {
                    foreach (string t in tiersFiles)
                    {
                        if (File.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t))
                        {
                            File.Delete(CGestURLs.mainFolder.getUrlFromRoot() + t);
                        }

                        WebClient webClient = new WebClient();

                        webClient.Credentials = new NetworkCredential("cgest", "cgestclientpaswd");

                        webClient.DownloadFile(@"http://www.colarsis.fr/cgest/CGestServer/" + actualVersion.ToString() + "/" + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"));

                        if (File.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip")))
                        {
                            ZipFile.ExtractToDirectory(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ""));

                            if (Directory.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", "")))
                            {
                                File.Copy(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", "") + @"\" + t, CGestURLs.mainFolder.getUrlFromRoot() + t);

                                Directory.Delete(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ""), true);

                                File.Delete(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"));
                            }
                        }
                    }
                }
                else
                {
                    foreach (string t in tiersFiles)
                    {
                        if (!File.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t))
                        {
                            WebClient webClient = new WebClient();

                            webClient.Credentials = new NetworkCredential("cgest", "cgestclientpaswd");

                            webClient.DownloadFile(@"http://www.colarsis.fr/cgest/CGestServer/" + actualVersion.ToString() + "/" + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"));

                            if (File.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip")))
                            {
                                ZipFile.ExtractToDirectory(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ""));

                                if (Directory.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", "")))
                                {
                                    File.Copy(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", "") + @"\" + t, CGestURLs.mainFolder.getUrlFromRoot() + t);

                                    Directory.Delete(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ""), true);

                                    File.Delete(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"));
                                }
                            }
                        }
                    }
                }

                configFile.DocumentElement.GetElementsByTagName("TiersFileVersion")[0].Attributes[0].Value = configFile.DocumentElement.GetElementsByTagName("Version")[0].Attributes[0].Value;

                configFile.Save(url);

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public int processVersion(string version)
        {
            string[] subversions = version.Split('.');

            string returnedValue = "";

            foreach (string s in subversions)
            {
                returnedValue += s;
            }

            return int.Parse(returnedValue);
        }

        //********** END FONCTION INITIALISATION DES FICHIERS **********//

        //********** FONCTION INITIALISATION DE LA BASE DE DONNEE **********//

        public bool initDatabase()
        {
            string defaultConnectionName = hasDefaultConnection();

            if (defaultConnectionName.Split(';')[0] == "true")
            {
                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                XmlNode root = xmlDoc.DocumentElement;

                XmlNodeList nodeList = root.ChildNodes;

                foreach (XmlNode node in nodeList)
                {
                    if (node.Attributes[0].Value == defaultConnectionName.Split(';')[1])
                    {
                        db = new CGestDatabase();

                        CGestMessages m = db.connect(node.ChildNodes[1].Attributes[0].Value, node.ChildNodes[2].Attributes[0].Value, "postgres", node.ChildNodes[4].Attributes[0].Value, node.ChildNodes[3].Attributes[0].Value);

                        if (m != CGestMessages.SUC_100)
                        {
                            //??? Rajout modification connection (defaut, ajout , supression ...) ???//

                            writeConsole("Impossible de se connecter à la base de donnée");

                            return false;
                        }
                        else
                        {
                            if (db.updateDatabase(node.ChildNodes[0].Attributes[0].Value))
                            {

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                //??? Rajout modification connection (defaut, ajout , supression ...) ???//

                writeConsole("Données de connections invalides");

                return false;
            }

            return true;
        }

        //********** END FONCTION INITIALISATION DE LA BASE DE DONNEE **********//

        //********** FONCTION INITIALISATION DE LA COUCHE RESEAU **********//

        public void initServer()
        {
            listenerWork = true;

            listener = new CGestListener(this);
        }

        //********** END FONCTION INITIALISATION DE LA COUCHE RESEAU **********//

        public void initGraphics()
        {
            notifyIcon.Icon = this.Icon;
        }

        //********** UTILITIES FICHIER **********//

        public bool checkPathExist(string path)
        {
            if (Directory.Exists(path))
            {
                CGestLogger.log("The directory \"" + path + "\" already exist.", false);
                return true;
            }
            else
            {
                CGestLogger.log("The directory doesn't exist ! Creating it ...", true);
                return false;
            }
        }

        //Verifie l'existence d'une connection par défaut
        public string hasDefaultConnection()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

            XmlNode root = xmlDoc.DocumentElement;

            XmlNodeList nodeList = root.ChildNodes;

            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes[1].Value != null && node.Attributes[1].Value == "Yes")
                {
                    return "true;" + node.Attributes[0].Value;
                }
            }

            return "false";

        }

        public bool checkFileExist(string path)
        {
            if (File.Exists(path))
            {
                CGestLogger.log("The file \"" + path + "\" has been found.", false);
                return true;
            }
            else
            {
                CGestLogger.log("The file \"" + path + "\" hasn't been found ! Creating it ...", false);
                return false;
            }
        }

        public bool createConnectionFile()
        {
            try
            {
                connectionFile = new XmlDocument();

                XmlDeclaration dec = connectionFile.CreateXmlDeclaration("1.0", "UTF-8", null);
                connectionFile.AppendChild(dec);
                XmlElement root = connectionFile.CreateElement("Connections");
                XmlElement defaultCo = connectionFile.CreateElement("Default");
                XmlElement path = connectionFile.CreateElement("DatabaseName");
                XmlElement username = connectionFile.CreateElement("Username");
                XmlElement password = connectionFile.CreateElement("Password");
                XmlElement port = connectionFile.CreateElement("Port");
                XmlElement source = connectionFile.CreateElement("Source");
                defaultCo.SetAttribute("Name", "default");
                defaultCo.SetAttribute("DefaultCo", "No");
                path.SetAttribute("DatabaseName", "default");
                username.SetAttribute("Username", "default");
                password.SetAttribute("Password", "default");
                port.SetAttribute("Port", "3050");
                source.SetAttribute("Source", "default");
                defaultCo.AppendChild(path);
                defaultCo.AppendChild(username);
                defaultCo.AppendChild(password);
                defaultCo.AppendChild(port);
                defaultCo.AppendChild(source);
                root.AppendChild(defaultCo);
                connectionFile.AppendChild(root);
                connectionFile.Save(CGestURLs.connectionXmlFile.getUrlFromRoot());
                CGestLogger.log("Connection file created.", true);
                connectionFile.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                return true;
            }
            catch (Exception e)
            {
                CGestLogger.error(e, true);
                return false;
            }

        }

        public bool createConfigFile()
        {
            try
            {
                connectionFile = new XmlDocument();

                XmlDeclaration dec = connectionFile.CreateXmlDeclaration("1.0", "UTF-8", null);
                connectionFile.AppendChild(dec);
                XmlElement root = connectionFile.CreateElement("Main");
                XmlElement version = connectionFile.CreateElement("Version");
                XmlElement dllVersion = connectionFile.CreateElement("TiersFileVersion");
                version.SetAttribute("CurrentVersion", "1.1.0");
                dllVersion.SetAttribute("CurrentVersion", "1.0.0");
                root.AppendChild(version);
                connectionFile.AppendChild(root);
                connectionFile.Save(CGestURLs.configXmlFile.getUrlFromRoot());
                CGestLogger.log("Config file created.", true);
                connectionFile.Load(CGestURLs.configXmlFile.getUrlFromRoot());

                return true;
            }
            catch (Exception e)
            {
                CGestLogger.error(e, true);
                return false;
            }
        }

        public bool createFolder(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                CGestLogger.log("Main folder has been created.", false);

                return true;
            }
            catch (Exception e)
            {
                CGestLogger.error(e, true);
                return false;
            }
        }

        //********** END UTILITIES FICHIER **********//

        public void updateLastSeen(CGestClient c)
        {
            if(c.connectionID != -1)
            {
                foreach (DataRow dr in db.Ds.Dataset.Tables["user"].Rows)
                {
                    if (dr.ItemArray[0].ToString() == c.connectionID.ToString())
                    {
                        object[] array = dr.ItemArray;

                        array[3] = c.last_seen.Day + "/" + c.last_seen.Month + '/' + c.last_seen.Year + "H" + c.last_seen.Hour + ":" + c.last_seen.Minute;

                        db.Ds.Dataset.Tables["user"].Rows.Find(array[0]).ItemArray = array;

                        db.Ds.updateDatabase();

                        return;
                    }
                }
            }
        }

        public void sendUpdateToClient()
        {
            foreach (CGestClient c in clientsList)
            {
                c.updateStatus = "outdated";

                c.sendUpdate();
            }
        }

        public void setButtonEnabling(Button b, bool s)
        {
            if (s)
            {
                b.Enabled = true;
            }
            else
            {
                b.Enabled = false;
            }
        }

        //Writer de la console
        public void writeConsole(string text)
        {
            textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm tt") + "] " + text);
        }

        //Masque la form au lieu de la fermer
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            notifyIcon.Visible = true;

            hidden = true;
        }

        //Double clique sur la notif
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            //Fait apparaitre la form
            if (hidden)
            {
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;

                notifyIcon.Visible = false;
            }
        }

        //Quitte l'app
        private void quitterLapplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listenerWork = false;

            listener.stopListener();

            Application.Exit();
        }

        //Ouvre console depuis notif
        private void ouvrirLaConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hidden)
            {
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;

                notifyIcon.Visible = false;
            }
        }

        //Stop le serveur
        private void stopServeurButton_Click(object sender, EventArgs e)
        {
            listenerWork = false;

            listener.stopListener();

            foreach (CGestClient c in clientsList)
            {
                c.stopReader();
            }

            stopServeurButton.Enabled = false;

            startServerButton.Enabled = true;
        }

        //Demarre le serveur
        private void startServerButton_Click(object sender, EventArgs e)
        {
            init();

            stopServeurButton.Enabled = true;

            startServerButton.Enabled = false;
        }

        private void seeUserButton_Click(object sender, EventArgs e)
        {
            seeUserButton.Enabled = false;

            usersForm uF = new usersForm(this, db);

            uF.Show();
        }

    }
}
