using SR2DServer.Database;
using SR2DServer.Utilities;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SR2DServer.Database
{
    public class SR2DDatabase //See AdminApp comments, it works the same way
    {
        //Variables de dataset
        private SR2DDataset ds;
        private SR2DConnection connection;
        public SR2DDataset Ds { get { return ds; } }
        public SR2DConnection Connection { get { return connection; } }

        //********** CONSTRUCTEUR **********//

        public SR2DDatabase()
        {
            connection = new SR2DConnection(this);
        }

        //********** END CONSTRUCTEUR **********//

        //Requête d'initalisation du dataset 

        //!!! Ajout de la gestion des erreur !!!//
        public bool initDataset()
        {
            ds = new SR2DDataset(this, connection);

            return ds.init();

        }

        //Requête de connection a la base de donnée
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

        //Requête de fermeture de la connection
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

        private bool changeDatabase(string dbName)
        {
            return connection.changeDatabase(dbName);
        }

        public bool updateRows()
        {
            try
            {

                foreach (DataRow dr in Ds.Dataset.Tables["user"].Rows)
                {
                    object[] obj = dr.ItemArray;

                    ds.Dataset.Tables["user"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = obj;
                }

                ds.updateDatabase();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

    }
}
