using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGestServer.Utilities
{
    public sealed class CGestMessages
    {
        private readonly string value;
        private readonly string name;

        public readonly static CGestMessages ERR_000 = new CGestMessages("ERR_000", "Invalid input string");
        public readonly static CGestMessages ERR_100 = new CGestMessages("ERR_100", "Can't open the database connection");
        public readonly static CGestMessages ERR_101 = new CGestMessages("ERR_101", "Can't close the database connection");

        public readonly static CGestMessages SUC_100 = new CGestMessages("SUC_100", "The connection to the database has been opened");
        public readonly static CGestMessages SUC_101 = new CGestMessages("SUC_101", "The connection to the database has been closed");

        private CGestMessages(string n, string m)
        {
            this.value = m;
            this.name = n;
        }

        public string getMessage()
        {
            return value;
        }

        public string getName()
        {
            return name;
        }
    }
}
