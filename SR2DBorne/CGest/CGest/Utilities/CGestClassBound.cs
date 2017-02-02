using CGest.Database;
using CGest.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Utilities
{
    public class CGestClassBound
    {

        public CGestDatabase db { get; private set; }
        public CGestNetworkClientControl networkControler { get; private set; }

        public CGestClassBound(CGestDatabase db, CGestNetworkClientControl controler)
        {
            this.db = db;
            this.networkControler = controler;
        }

    }
}
