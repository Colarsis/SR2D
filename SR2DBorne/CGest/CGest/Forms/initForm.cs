using CGest.Database;
using CGest.Forms;
using CGest.Network;
using CGest.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CGest
{
    public partial class initForm : Form
    {
        CGestNetworkClientControl networkControl;
        public XmlDocument connectionFile;
        public CGestDatabase database;

        public bool checkCo = true;

        public initForm()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerAsync();
        }

        public void setDatabase(CGestDatabase db)
        {
            this.database = db;
        }

        public void updateStatut(string text, int value)
        {
            statutLabel.Text = "Statut : " + text + "...";

            progressBar1.Value = value;
        }

        public void updateStatutOnWorker(object text, int value)
        {
            backgroundWorker1.ReportProgress(value, text);
        }

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
            catch(Exception e)
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
                XmlElement defaultCo = connectionFile.CreateElement("Server");
                XmlElement version = connectionFile.CreateElement("Version");
                XmlElement dllVersion = connectionFile.CreateElement("TiersFileVersion");
                XmlElement path = connectionFile.CreateElement("IP");
                XmlElement port = connectionFile.CreateElement("Port");
                path.SetAttribute("IP", "127.0.0.1");
                port.SetAttribute("Port", "11347");
                version.SetAttribute("CurrentVersion", "1.1.0");
                dllVersion.SetAttribute("CurrentVersion", "1.0.1");
                defaultCo.AppendChild(path);
                defaultCo.AppendChild(port);
                root.AppendChild(defaultCo);
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
                    dllVersion.SetAttribute("CurrentVersion", "1.0.1");
                    configFile.DocumentElement.AppendChild(dllVersion);

                    configFile.Save(url);
                }

                return true;
            }
            catch(Exception e)
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

                        webClient.DownloadFile(@"http://www.colarsis.fr/cgest/CGestClient/" + actualVersion.ToString() + "/" + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"));

                        if (File.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip")))
                        {
                            ZipFile.ExtractToDirectory(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ""));

                            if(Directory.Exists(CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", "")))
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

                            webClient.DownloadFile(@"http://www.colarsis.fr/cgest/CGestClient/" + actualVersion.ToString() + "/" + t.Replace(".dll", ".zip"), CGestURLs.mainFolder.getUrlFromRoot() + t.Replace(".dll", ".zip"));

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
            catch(Exception e)
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

        public bool connectToServer()
        {
            string[] values = getServer();

            networkControl = new CGestNetworkClientControl(int.Parse(values[1]), values[0], database);

            serverConnectForm serverForm = new serverConnectForm();

            DialogResult serverFormBox = serverForm.ShowDialog();

            if (serverFormBox == DialogResult.OK)
            {
                while (true)
                {
                    if (networkControl.connect(serverForm.getUsername(), serverForm.getPassword()))
                    {
                        while (networkControl.client.enteringQueue.Count == 0)
                        {
                        }

                        while (networkControl.client.enteringQueue.Peek().Split(';')[0] != "connect")
                        {

                        }

                        string dequeue = networkControl.client.enteringQueue.Dequeue();

                        if (dequeue.Split(';')[1] != "validate")
                        {
                            serverForm.error = true;

                            serverFormBox = serverForm.ShowDialog();

                            if (serverFormBox != DialogResult.OK)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            networkControl.username = serverForm.getUsername();
                            networkControl.id = int.Parse(dequeue.Split(';')[2].ToString());

                            return true;
                        }

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
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

        public string[] getServer()
        {
            string[] returnedValue = {"127.0.0.1", "11347"};

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(CGestURLs.configXmlFile.getUrlFromRoot());

            XmlNode root = xmlDoc.DocumentElement;

            XmlNodeList nodeList = root.ChildNodes;

            foreach (XmlNode node in nodeList)
            {
                if (node.Name != null && node.Name == "Server")
                {
                    returnedValue[0] = node.ChildNodes[0].Attributes[0].Value;
                    returnedValue[1] = node.ChildNodes[1].Attributes[0].Value;

                    return returnedValue;
                }
            }

            return returnedValue;

        }

        private void init()
        {
            if (!threadInit())
            {
                DialogResult failBox = MessageBox.Show("Une erreur est survenue lors de l'initialisation de CGest ! Veuillez essayer de redémmarer le logiciel.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
            {
                CGestLogger.log("Launching ...", false);

                //this.Close();
            }
        }

        private bool threadInit()
        {

            updateStatutOnWorker("Chargement des fichiers", 0);

            if (!checkPathExist(CGestURLs.mainFolder.getUrlFromRoot()))
            {
                if (!createFolder(CGestURLs.mainFolder.getUrlFromRoot()))
                {
                    return false;
                }
            }

            if (!checkFileExist(CGestURLs.connectionXmlFile.getUrlFromRoot()))
            {
                if (!createConnectionFile())
                {
                    return false;
                }
            }

            if (!checkFileExist(CGestURLs.configXmlFile.getUrlFromRoot()))
            {
                if (!createConfigFile())
                {
                    return false;
                }
            }

            if (!updateConfig(CGestURLs.configXmlFile.getUrlFromRoot()))
            {
                return false;
            }

            updateStatutOnWorker("Chargement des connections", 20);

            string defaultConnectionName = hasDefaultConnection();

            updateStatutOnWorker("Mise a jour des fichiers tiers", 25);

            if (!updateDll(CGestURLs.configXmlFile.getUrlFromRoot()))
            {
                return false;
            }

            updateStatutOnWorker("Connection", 30);

            if (defaultConnectionName.Split(';')[0] == "true")
            {
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.Load(CGestURLs.connectionXmlFile.getUrlFromRoot());

                    XmlNode root = xmlDoc.DocumentElement;

                    XmlNodeList nodeList = root.ChildNodes;

                    foreach (XmlNode node in nodeList)
                    {
                        if (node.Attributes[0].Value == defaultConnectionName.Split(';')[1])
                        {
                            database = new CGestDatabase();

                            CGestMessages m = database.connect(node.ChildNodes[1].Attributes[0].Value, node.ChildNodes[2].Attributes[0].Value, "postgres", node.ChildNodes[4].Attributes[0].Value, node.ChildNodes[3].Attributes[0].Value);

                            if (m != CGestMessages.SUC_100)
                            {
                                DialogResult failBox = MessageBox.Show("Une erreur est survenue lors de la connection a la base de donnée par defaut !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                ConnectionForm connectionForm = new ConnectionForm(this);

                                DialogResult r = connectionForm.ShowDialog();

                                while (r != DialogResult.OK && connectionForm.returnStatut != true)
                                {
                                    connectionForm = new ConnectionForm(this);

                                    r = connectionForm.ShowDialog();
                                }

                                Console.WriteLine("conected");
                            }
                            else
                            {
                                updateStatutOnWorker("Mise a jour de la base de donnée", 35);

                                if (database.updateDatabase(node.ChildNodes[0].Attributes[0].Value))
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
                catch (Exception e)
                {

                }
            }
            else
            {

                ConnectionForm connectionForm = new ConnectionForm(this);

                DialogResult r = connectionForm.ShowDialog();

                while (r != DialogResult.OK && connectionForm.returnStatut != true)
                {
                    connectionForm = new ConnectionForm(this);

                    r = connectionForm.ShowDialog();
                }

                Console.WriteLine("connected");
            }

            updateStatutOnWorker("Connection au serveur", 50);

            if (!connectToServer())
            {
                return false;
            }

            updateStatutOnWorker("Aquisition des données", 70);

            database.initDataset();

            updateStatutOnWorker("Mise a jour des données", 50);

            if (!database.updateRows())
            {
                return false;
            }

            updateStatutOnWorker("Lancement", 100);

            return true;

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            init();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            updateStatut(e.UserState.ToString(), e.ProgressPercentage);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CGestClassBound dataBound = new CGestClassBound(database, networkControl);

            MainForm mainForm = new MainForm(dataBound, this);

            mainForm.Show();

            this.Visible = false;
        }

    }
}
