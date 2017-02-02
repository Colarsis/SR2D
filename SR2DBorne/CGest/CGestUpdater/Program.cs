using CGestUpdater.Utilitaires;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CGestUpdater
{
    class Program
    {
        static XmlDocument versionFile = new XmlDocument();

        static void Main(string[] args)
        {
            Console.WriteLine("Searching for new versions ...");
            Console.WriteLine("");
            if (downloadVersionFile())
            {
                loadVersions();
                Console.WriteLine("Your current version of CGest is " + getCurrentClientVersion());
                Console.WriteLine("");
                Console.WriteLine("Client latest version is " + loadVersions());
                Console.WriteLine("");
                if (processVersions(getCurrentClientVersion(), loadVersions()))
                {
                    Console.WriteLine("Your software is out to date, do you want to update ? Press Y/N");
                    Console.WriteLine("");
                    string result = Console.ReadLine();
                    if (result == "Y" || result == "y")
                    {
                        if (checkIfCGestIsRunning())
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Please close CGest before update it. Press any key to exit");
                            Console.Read();
                            Environment.Exit(0);
                        }
                        else
                        {
                            if (!removeCGest())
                            {
                                Console.WriteLine("");
                                Console.WriteLine("An error occured during the update. Press any key to exit");
                                Console.Read();
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Downloading ... Please do not exit before the end of the update");
                                if (downloadZipFile())
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("CGest downloaded.");
                                    Console.WriteLine("");
                                    Console.WriteLine("Extracting it.");
                                    if (unzipFile())
                                    {
                                        if (removeZip())
                                        {
                                            setNewVersion();
                                            Console.WriteLine("");
                                            Console.WriteLine("Update completed. Thanks for waiting. Press any key to exit");
                                            Console.Read();
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("An error occured during the update. The file is probably corrupted.");
                                        Console.WriteLine("Please update again. Press any key to exit");
                                        Console.Read();
                                        Environment.Exit(0);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("An error occured during the update. The file is probably corrupted.");
                                    Console.WriteLine("Please update again. Press any key to exit");
                                    Console.Read();
                                    Environment.Exit(0);
                                }
                            }
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Your software is up to date. Press any key to exit");
                    Console.Read();
                    Environment.Exit(0);
                }
                string cmd = Console.ReadLine();
            }
            else
            {
                Console.Read();
            }
        }

        static public bool removeZip()
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

        static public bool removeCGest()
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

        static public bool checkIfCGestIsRunning()
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

        static public bool processVersions(string curV, string lastV)
        {
            int cV = convertVersions(curV);
            int lV = convertVersions(lastV);

            if (cV < lV)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public int convertVersions(string version)
        {
            string[] integers = version.Split('.');

            string textVersions = "0";

            foreach (string s in integers)
            {
                textVersions += s;
            }

            return int.Parse(textVersions);
        }

        static public string getCurrentClientVersion()
        {
            XmlDocument configFile = new XmlDocument();

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

        static public string loadVersions()
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

        static public void setNewVersion()
        {
            try
            {
                XmlDocument configDoc = new XmlDocument();

                configDoc.Load(Links.configFile.getUrl());

                configDoc.GetElementsByTagName("Version")[0].Attributes[0].Value = loadVersions();

                configDoc.Save(Links.configFile.getUrl());
            }
            catch (Exception e){}
        }

        static public bool unzipFile()
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
                catch(Exception e)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        static public bool downloadZipFile()
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

        static public bool downloadVersionFile()
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
