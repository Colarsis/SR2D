using SR2DAdminApp.Database;
using SR2DAdminApp.Forms;
using SR2DAdminApp.Network;
using SR2DAdminApp.Utilities;
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

namespace SR2DAdminApp
{
    public partial class initForm : Form
    {
        SR2DNetworkClientControl networkControl;
        public XmlDocument connectionFile;
        public SR2DDatabase database;
        public SR2DVars vars;

        public bool checkCo = true;

        public initForm()
        {
            InitializeComponent();

            backgroundWorker1.RunWorkerAsync();
        }

        public void setDatabase(SR2DDatabase db)
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
                SR2DLogger.log("The directory \"" + path + "\" already exist.", false);
                return true;
            }
            else
            {
                SR2DLogger.log("The directory doesn't exist ! Creating it ...", true);
                return false;
            }
        }

        public bool checkFileExist(string path)
        {
            if (File.Exists(path))
            {
                SR2DLogger.log("The file \"" + path + "\" has been found.", false);
                return true;
            }
            else
            {
                SR2DLogger.log("The file \"" + path + "\" hasn't been found ! Creating it ...", false);
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
                connectionFile.Save(SR2DURLs.connectionXmlFile.getUrlFromRoot());
                SR2DLogger.log("Connection file created.", true);
                connectionFile.Load(SR2DURLs.connectionXmlFile.getUrlFromRoot());

                return true;
            }
            catch(Exception e)
            {
                SR2DLogger.error(e, true);
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
                connectionFile.Save(SR2DURLs.configXmlFile.getUrlFromRoot());
                SR2DLogger.log("Config file created.", true);
                connectionFile.Load(SR2DURLs.configXmlFile.getUrlFromRoot());

                return true;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, true);
                return false;
            }

        }

        public bool addConfig(XmlDocument doc)
        {
            try
            {
                XmlElement opts = doc.CreateElement("Options");
                XmlElement rTD = doc.CreateElement("RealTimeDisplay");

                rTD.SetAttribute("Value", "false");

                opts.AppendChild(rTD);
                doc.DocumentElement.AppendChild(opts);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);

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

                if (!nodes.Contains("Options"))
                {
                    addConfig(configFile);

                    configFile.Save(url);
                }
                else
                {
                    List<string> optsNodes = new List<string>();

                    XmlNode optsNode = configFile.GetElementsByTagName("Options")[0];

                    foreach (XmlNode n in optsNode.ChildNodes)
                    {
                        optsNodes.Add(n.Name);
                    }

                    if(!optsNodes.Contains("RealTimeDisplay"))
                    {
                        XmlElement rTD = configFile.CreateElement("RealTimeDisplay");

                        rTD.SetAttribute("Value", "false");

                        optsNode.AppendChild(rTD);
                    }

                    configFile.Save(url);
                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
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

            networkControl = new SR2DNetworkClientControl(int.Parse(values[1]), values[0], database);

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
                SR2DLogger.log("Main folder has been created.", false);

                return true;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, true);
                return false;
            }
        }

        public string hasDefaultConnection()
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(SR2DURLs.connectionXmlFile.getUrlFromRoot());

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
            string[] returnedValue = {"127.0.0.1", "3306"};

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(SR2DURLs.configXmlFile.getUrlFromRoot());

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
                DialogResult failBox = MessageBox.Show("Une erreur est survenue lors de l'initialisation de SR2D ! Veuillez essayer de redémmarer le logiciel.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            else
            {
                SR2DLogger.log("Launching ...", false);

                //this.Close();
            }
        }

        private bool threadInit()
        {

            updateStatutOnWorker("Chargement des fichiers", 0);

            if (!checkPathExist(SR2DURLs.mainFolder.getUrlFromRoot()))
            {
                if (!createFolder(SR2DURLs.mainFolder.getUrlFromRoot()))
                {
                    return false;
                }
            }

            if (!checkFileExist(SR2DURLs.connectionXmlFile.getUrlFromRoot()))
            {
                if (!createConnectionFile())
                {
                    return false;
                }
            }

            if (!checkFileExist(SR2DURLs.configXmlFile.getUrlFromRoot()))
            {
                if (!createConfigFile())
                {
                    return false;
                }
            }

            if (!updateConfig(SR2DURLs.configXmlFile.getUrlFromRoot()))
            {
                return false;
            }

            vars = new SR2DVars();

            updateStatutOnWorker("Chargement des connections", 20);

            string defaultConnectionName = hasDefaultConnection();

            updateStatutOnWorker("Mise a jour des fichiers tiers", 25);

            updateStatutOnWorker("Connection", 30);

            if (defaultConnectionName.Split(';')[0] == "true")
            {
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.Load(SR2DURLs.connectionXmlFile.getUrlFromRoot());

                    XmlNode root = xmlDoc.DocumentElement;

                    XmlNodeList nodeList = root.ChildNodes;

                    foreach (XmlNode node in nodeList)
                    {
                        if (node.Attributes[0].Value == defaultConnectionName.Split(';')[1])
                        {
                            database = new SR2DDatabase();

                            SR2DMessages m = database.connect(node.ChildNodes[1].Attributes[0].Value, node.ChildNodes[2].Attributes[0].Value, node.ChildNodes[0].Attributes[0].Value, node.ChildNodes[4].Attributes[0].Value, node.ChildNodes[3].Attributes[0].Value);

                            if (m != SR2DMessages.SUC_100)
                            {
                                DialogResult failBox = MessageBox.Show("Une erreur est survenue lors de la connection a la base de donnée par defaut !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                ConnectionForm connectionForm = new ConnectionForm(this);

                                DialogResult r = connectionForm.ShowDialog();

                                while (r != DialogResult.OK || connectionForm.returnStatut != true)
                                {
                                    connectionForm = new ConnectionForm(this);

                                    r = connectionForm.ShowDialog();
                                }



                                Console.WriteLine("conected");
                            }
                            else
                            {
                                updateStatutOnWorker("Mise a jour de la base de donnée", 35);
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
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
            SR2DClassBound dataBound = new SR2DClassBound(database, networkControl, vars);

            MainForm mainForm = new MainForm(dataBound, this);

            mainForm.Show();

            this.Visible = false;
        }

    }
}
