using CGestUpdater.Utilitaires;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace CGestClientUpdater
{
    public partial class Form1 : Form
    {

        XmlDocument versionFile = new XmlDocument();

        public Form1()
        {
            InitializeComponent();

            label1.Text = "Searching for new versions ...";
            if (downloadVersionFile())
            {
                if (processVersions(getCurrentClientVersion(), loadVersions()))
                {
                    if (checkIfCGestIsRunning())
                    {
                        DialogResult msgBox = MessageBox.Show("Please close CGest before the update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Environment.Exit(0);
                    }
                    else
                    {
                        label1.Text = "Prepearing update ...";

                        if (!removeCGest())
                        {
                            DialogResult msgBox = MessageBox.Show("An error occured during the CGest update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            Environment.Exit(0);
                        }
                        else
                        {
                            label1.Text = "Downloading ...";
                            if (downloadZipFile())
                            {
                                label1.Text = "Updating ...";
                                if (unzipFile())
                                {
                                    if (removeZip())
                                    {
                                        setNewVersion();
                                        label1.Text = "Update completed ...";

                                        launchCGest();

                                        Environment.Exit(0);
                                    }
                                }
                                else
                                {
                                    DialogResult msgBox = MessageBox.Show("An error occured during the CGest update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    Environment.Exit(0);
                                }
                            }
                            else
                            {
                                DialogResult msgBox = MessageBox.Show("An error occured during the CGest update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                Environment.Exit(0);
                            }
                        }
                    }
                }
                else
                {
                    launchCGest();

                    Environment.Exit(0);
                }
            }
            else
            {
                DialogResult msgBox = MessageBox.Show("An error occured during the CGest update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(0);
            }
        }

        public void launchCGest()
        {
            try
            {
                if(File.Exists(Links.exeFile.getUrl()))
                {
                    Process.Start(Links.exeFile.getUrl());
                }
            }
            catch (Exception e)
            {

            }
        }

        public bool removeZip()
        {
            if (File.Exists(Links.zipFile.getUrl()))
            {
                try
                {
                    File.Delete(Links.zipFile.getUrl());

                    Directory.Delete(Links.exeFolder.getUrl(), true);

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool removeCGest()
        {
            if (File.Exists(Links.zipFile.getUrl()))
            {
                try
                {
                    File.Delete(Links.zipFile.getUrl());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    return false;
                }
            }

            if (File.Exists(Links.exeFile.getUrl()))
            {
                try
                {
                    File.Delete(Links.exeFile.getUrl());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    return false;
                }
            }

            if (Directory.Exists(Links.exeFolder.getUrl()))
            {
                try
                {
                    Directory.Delete(Links.exeFolder.getUrl(), true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    return false;
                }
            }

            return true;
        }

        public bool checkIfCGestIsRunning()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName.Contains("CGestClient"))
                {
                    return true;
                }
            }

            return false;
        }

        public bool processVersions(string curV, string lastV)
        {
            if (curV == "")
            {
                return false;
            }

            int cV = convertVersions(curV);
            int lV = convertVersions(lastV);

            Console.WriteLine(cV.ToString());
            Console.WriteLine(lV.ToString());

            if (cV < lV)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int convertVersions(string version)
        {
            string[] integers = version.Split('.');

            string textVersions = "0";

            foreach (string s in integers)
            {
                textVersions += s;
            }

            return int.Parse(textVersions);
        }

        public string getCurrentClientVersion()
        {
            XmlDocument configFile = new XmlDocument();

            try
            {
                configFile.Load(Links.configFile.getUrl());

                XmlNode mainNode = configFile.DocumentElement;

                XmlNodeList nodeList = mainNode.ChildNodes;

                string currentVersion = "";

                foreach (XmlNode node in nodeList)
                {
                    if (node.Name != null && node.Name == "Version")
                    {
                        currentVersion = node.Attributes[0].Value;
                    }
                }

                return currentVersion;
            }
            catch(Exception e)
            {
                return "";
            }
            
        }

        public string loadVersions()
        {
            XmlNode mainNode = versionFile.DocumentElement;
            XmlNodeList nodeList = mainNode.ChildNodes;

            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes[0].Name != null && node.Attributes[0].Name == "Version")
                {
                    if (node.Name != null && node.Name == "Client")
                    {
                        return node.Attributes[0].Value;
                    }
                }
            }

            return "";
        }

        public void setNewVersion()
        {
            try
            {
                XmlDocument configDoc = new XmlDocument();

                configDoc.Load(Links.configFile.getUrl());

                configDoc.GetElementsByTagName("Version")[0].Attributes[0].Value = loadVersions();

                configDoc.Save(Links.configFile.getUrl());
            }
            catch (Exception e) { }
        }

        public bool unzipFile()
        {
            if (File.Exists(Links.zipFile.getUrl()))
            {
                try
                {
                    ZipFile.ExtractToDirectory(Links.zipFile.getUrl(), Links.exeFolder.getUrl());

                    if (Directory.Exists(Links.exeFolder.getUrl()))
                    {
                        if (File.Exists(Links.unzipedFile.getUrl()))
                        {
                            File.Copy(Links.unzipedFile.getUrl(), Links.exeFile.getUrl());

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool downloadZipFile()
        {
            WebClient webClient = new WebClient();

            webClient.Credentials = new NetworkCredential("cgest", "cgestclientpaswd");

            try
            {
                webClient.DownloadFile(Links.serverExeFile.getUrl() + loadVersions() + @"\CGestClient.zip", Links.zipFile.getUrl());

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool downloadVersionFile()
        {
            string xmlString;

            WebClient webC = new WebClient();

            webC.Credentials = new NetworkCredential("cgest", "cgestclientpaswd");

            try
            {
                xmlString = webC.DownloadString(Links.versionFile.getUrl());

                versionFile.LoadXml(xmlString);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured when trying to download version file.");

                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
