using SR2DAdminApp.Forms;
using SR2DAdminApp.Utilities;
using MySql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SR2DAdminApp.Database
{
    public class SR2DDatabase
    {

        /*Vars*/
        private SR2DDataset ds;
        private SR2DConnection connection;
        public SR2DDataset Ds { get { return ds; } }
        public SR2DConnection Connection { get { return connection; } }

        public SR2DDatabase()
        {
            connection = new SR2DConnection(this);
            ds = new SR2DDataset(this, connection);
        }

        //Init the dataset (cross-object function)
        public void initDataset()
        {
            ds.init();
        }

        //Create a connection with the database (cross-object function)
        public SR2DMessages connect(string user, string password, string databaseName, string datasource, string port)
        {
            if (user != "" && password != "" && databaseName != "" && datasource != "" && port != "")
            {
                return connection.create(user, password, databaseName, datasource, port);
            }
            else
            {
                return SR2DMessages.ERR_000;
            }
        }

        //Close the connection (cross-object function)
        public SR2DMessages close()
        {
            if (this.connection.close())
            {
                return SR2DMessages.SUC_101;
            }
            else
            {
                return SR2DMessages.ERR_101;
            }
        }

        //Change the connection (cross-object function)
        private bool changeDatabase(string dbName)
        {
            return connection.changeDatabase(dbName);
        }

    }
}
