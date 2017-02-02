using SR2DServer.Database;
using SR2DServer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SR2DServer.Database
{
    public class SR2DConnection //See AdminApp comments, it's just the same
    {
        //Base de donnée SR2D
        SR2DDatabase database;

        //Variables de connection
        private string statut;
        public string Statut { get { return statut; } }
        private MySqlConnection databaseConnection;
        public MySqlConnection DatabaseConnection { get { return databaseConnection; } }

        //*********** CONSTRUCTEUR **********//

        public SR2DConnection(SR2DDatabase database)
        {
            this.database = database;
            statut = "closed";
        }

        //*********** END CONSTRUCTEUR **********//

        //Creation d'une connection
        public SR2DMessages create(string user, string password, string databaseName, string datasource, string port)
        {
            string connectionString = "server=" + datasource + ";port=" + port + ";uid=" + user + ";pwd=" + password + ";database=" + databaseName + ";";

            databaseConnection = new MySqlConnection(connectionString);

            try
            {
                databaseConnection.Open();
                SR2DLogger.log("The database connection has been opened", false);
                statut = "opened";

                return SR2DMessages.SUC_100;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, false);

                statut = "closed";

                return SR2DMessages.ERR_100;
            }
        }

        //Fermeture de connection
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
