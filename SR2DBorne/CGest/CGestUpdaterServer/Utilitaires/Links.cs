using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGestUpdater.Utilitaires
{
    class Links
    {
        private readonly string name;
        private readonly string url;

        public readonly static Links versionFile = new Links("versionFile", "http://www.colarsis.fr/cgest/version.xml");
        public readonly static Links configFile = new Links("configFile", @"c:\CGest\CGestServer\config.xml");
        public readonly static Links exeFile = new Links("exeFile", @"c:\CGest\CGestServer\CGestServer.exe");
        public readonly static Links exeFolder = new Links("exeFolder", @"c:\CGest\CGestServer\CGestServer");
        public readonly static Links zipFile = new Links("zipFile", @"c:\CGest\CGestServer\CGestServer.zip");
        public readonly static Links unzipedFile = new Links("unzipedFile", @"c:\CGest\CGestServer\CGestServer\CGestServer.exe");
        public readonly static Links serverExeFile = new Links("exeFile", "http://www.colarsis.fr/cgest/CGestServer/");

        private Links(string n, string url)
        {
            this.name = n;
            this.url = url;
        }

        public string getUrl()
        {
            return url;
        }

        public string getName()
        {
            return name;
        }
    }
}
