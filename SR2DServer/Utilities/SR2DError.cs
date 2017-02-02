using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DServer.Utilities
{
    public sealed class SR2DMessages
    {
        private readonly string value;
        private readonly string name;

        public readonly static SR2DMessages ERR_000 = new SR2DMessages("ERR_000", "Invalid input string");
        public readonly static SR2DMessages ERR_100 = new SR2DMessages("ERR_100", "Can't open the database connection");
        public readonly static SR2DMessages ERR_101 = new SR2DMessages("ERR_101", "Can't close the database connection");

        public readonly static SR2DMessages SUC_100 = new SR2DMessages("SUC_100", "The connection to the database has been opened");
        public readonly static SR2DMessages SUC_101 = new SR2DMessages("SUC_101", "The connection to the database has been closed");

        private SR2DMessages(string n, string m)
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
