using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DServer.Utilities
{
    public static class SR2DLogger
    {

        public static void log(string m, bool f)
        {
            Console.WriteLine(m);
            if (f)
            {

            }
        }

        public static void log(string m, bool f, SR2DMessages r)
        {
            Console.WriteLine(m + " SR2DMessage: " + r.getName());
            if (f)
            {

            }
        }

        public static void error(Exception m, bool f)
        {
            Console.WriteLine("ERROR : " + m.Message + " StackTrace: ");
            Console.WriteLine(m.StackTrace);
        }

        public static void error(Exception m, bool f, SR2DMessages r)
        {
            Console.WriteLine("ERROR : " + m.Message + " SR2DMessage: " + r.getName() + "StackTrace: ");
            Console.WriteLine(m.StackTrace);
        }

    }
}
