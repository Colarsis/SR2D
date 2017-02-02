using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DAdminApp.Utilities
{
    public sealed class SR2DMessages //All error messages (many missing, if you want to add, DO IT) PS: A good job to do with the future "Première SI", Corentin Guilloteau
    {
        private readonly string value;
        private readonly string name;

        public readonly static SR2DMessages ERR_000 = new SR2DMessages("ERR_000", "Invalid input string");
        public readonly static SR2DMessages ERR_100 = new SR2DMessages("ERR_100", "Can't open the database connection");
        public readonly static SR2DMessages ERR_101 = new SR2DMessages("ERR_101", "Can't close the database connection");
        public readonly static SR2DMessages ERR_200 = new SR2DMessages("ERR_200", "Stock entrie already exist");
        public readonly static SR2DMessages ERR_201 = new SR2DMessages("ERR_201", "Error when adding stock entrie");

        public readonly static SR2DMessages SUC_100 = new SR2DMessages("SUC_100", "The connection to the database has been opened");
        public readonly static SR2DMessages SUC_101 = new SR2DMessages("SUC_101", "The connection to the database has been closed");
        public readonly static SR2DMessages SUC_200 = new SR2DMessages("SUC_200", "Stock entrie added");

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
