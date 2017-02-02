using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Utilities
{
    public sealed class CGestURLs
    {

        private readonly string name;
        private readonly string fromFolder;
        private readonly string fromRoot;

        public readonly static CGestURLs connectionXmlFile = new CGestURLs("connectionXmlFile", "connection.xml");
        public readonly static CGestURLs configXmlFile = new CGestURLs("configXmlFile", "config.xml");
        public readonly static CGestURLs mainFolder = new CGestURLs("mainFolder", "");

        private CGestURLs(string n, string url)
        {
            this.name = n;
            this.fromFolder = url;
            this.fromRoot = @"c:\CGest\CGestClient\" + url;
        }

        public string getUrlFromRoot()
        {
            return fromRoot;
        }

        public string getUrlFromFolder()
        {
            return fromFolder;
        }

        public string getName()
        {
            return name;
        }

    }
}
