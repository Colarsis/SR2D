using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using MySql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DAdminApp.Database
{
    public class SR2DConnection
    {
        /*Vars*/
        SR2DDatabase database;
        private string statut;
        public string Statut { get { return statut; } }
        private MySqlConnection databaseConnection;
        public MySqlConnection DatabaseConnection { get { return databaseConnection; } }


        public SR2DConnection(SR2DDatabase database)
        {
            this.database = database;
            statut = "closed";
        }

        //Create the connection
        public SR2DMessages create(string user, string password, string databaseName, string datasource, string port)
        {
            string connectionString = "server=" + datasource + ";port=" + port + ";uid=" + user + ";pwd=" + password + ";database=" + databaseName + ";";

            databaseConnection = new MySqlConnection(connectionString);

            try
            {
                databaseConnection.Open();

                SR2DLogger.log("The database connection has been opened", false);
                statut = "opened";

                return SR2DMessages.SUC_100 ;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, false);

                statut = "closed";

                return SR2DMessages.ERR_100;
            }
        }

        //Clode the connection
        public bool close()
        {
            try
            {
                databaseConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, false);

                return false;
            }
        }

        //Change the database
        public bool changeDatabase(string dbName)
        {
            try
            {
                databaseConnection.ChangeDatabase(dbName);

                SR2DLogger.log("Database changed", false);

                return true;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, false);

                statut = "closed";

                return false;
            }
        }

    }
}
