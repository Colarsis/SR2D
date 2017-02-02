using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Utilities
{
    public static class CGestLogger
    {

        public static void log(string m, bool f)
        {
            Console.WriteLine(m);
            if (f)
            {

            }
        }

        public static void log(string m, bool f, CGestMessages r)
        {
            Console.WriteLine(m + " CGestMessage: " + r.getName());
            if (f)
            {

            }
        }

        public static void error(Exception m, bool f)
        {
            Console.WriteLine("ERROR : " + m.Message + " StackTrace: ");
            Console.WriteLine(m.StackTrace);
        }

        public static void error(Exception m, bool f, CGestMessages r)
        {
            Console.WriteLine("ERROR : " + m.Message + " CGestMessage: " + r.getName() + "StackTrace: ");
            Console.WriteLine(m.StackTrace);
        }

    }
}
