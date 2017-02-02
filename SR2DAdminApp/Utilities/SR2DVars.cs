using SR2DAdminApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SR2DAdminApp.Utilities
{
    public class SR2DVars
    {

        public bool realTimeDisplay;

        public SR2DVars(bool realTimeDisplay)
        {
            this.realTimeDisplay = realTimeDisplay;
        }

        public SR2DVars()
        {
            updateOptions();
        }

        public bool setOption(string name, object value)
        {
            try
            {
                XmlDocument configFile = new XmlDocument();

                configFile.Load(SR2DURLs.configXmlFile.getUrlFromRoot());

                XmlNode optsNode = configFile.GetElementsByTagName("Options")[0];

                Dictionary<string, int> optnIDs = new Dictionary<string,int>();

                int i;

                for(i = 0; i < optsNode.ChildNodes.Count; i++)
                {
                    optnIDs.Add(optsNode.ChildNodes[i].Name, i);
                }

                switch(name)
                {
                    case "RTD":
                        int id;

                        if(optnIDs.TryGetValue("RealTimeDisplay", out id))
                        {
                            if ((bool)value)
                            {
                                optsNode.ChildNodes[id].Attributes[0].Value = "true";
                            }
                            else
                            {
                                optsNode.ChildNodes[id].Attributes[0].Value = "false";
                            }
                        }
                        else
                        {
                            return false;
                        }
                        break;
                }

                configFile.Save(SR2DURLs.configXmlFile.getUrlFromRoot());

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);

                return false;
            }
        }

        public Dictionary<string, object> loadOptions()
        {
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();

                XmlDocument configFile = new XmlDocument();

                configFile.Load(SR2DURLs.configXmlFile.getUrlFromRoot());

                List<string> nodes = new List<string>();

                XmlNode optsNode = configFile.GetElementsByTagName("Options")[0];

                foreach (XmlNode n in configFile.DocumentElement.ChildNodes)
                {
                    switch (n.Name)
                    {
                        case "RealTimeDisplay":
                            if (n.Attributes[0].Value == "true")
                            {
                                options.Add("RTD", true);
                            }
                            else
                            {
                                options.Add("RTD", false);
                            }
                            break;
                    }
                }

                return options;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                return null;
            }
        }

        public bool updateOptions()
        {
            Dictionary<string, object> opts = loadOptions();

            if(opts != null)
            {
                foreach(KeyValuePair<string, object> kV in opts)
                {
                    switch (kV.Key)
                    {
                        case "RTD":
                            realTimeDisplay = (bool)kV.Value;
                            break;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
