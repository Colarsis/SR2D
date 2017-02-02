using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DServer.Utilities
{
    public sealed class SR2DURLs
    {

        private readonly string name;
        private readonly string fromFolder;
        private readonly string fromRoot;

        public readonly static SR2DURLs connectionXmlFile = new SR2DURLs("connectionXmlFile", "connection.xml");
        public readonly static SR2DURLs configXmlFile = new SR2DURLs("configXmlFile", "config.xml");
        public readonly static SR2DURLs mainFolder = new SR2DURLs("mainFolder", "");

        private SR2DURLs(string n, string url)
        {
            this.name = n;
            this.fromFolder = url;
            this.fromRoot = @"c:\SR2D\SR2DServer\" + url;
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
