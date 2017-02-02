using SR2DAdminApp.Database;
using SR2DAdminApp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DAdminApp.Utilities
{
    public class SR2DClassBound //Class transmitted beetwen every forms with database and network connection
    {

        public SR2DDatabase db { get; private set; }
        public SR2DNetworkClientControl networkControler { get; private set; }
        public SR2DVars vars { get; private set; }

        public SR2DClassBound(SR2DDatabase db, SR2DNetworkClientControl controler, SR2DVars vars)
        {
            this.db = db;
            this.networkControler = controler;
            this.vars = vars;
        }

    }
}
