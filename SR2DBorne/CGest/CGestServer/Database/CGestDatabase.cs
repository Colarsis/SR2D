using CGestServer.Database;
using CGestServer.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGestServer.Database
{
    public class CGestDatabase
    {
        //Variables de dataset
        private CGestDataset ds;
        private CGestConnection connection;
        public CGestDataset Ds { get { return ds; } }
        public CGestConnection Connection { get { return connection; } }

        //********** CONSTRUCTEUR **********//

        public CGestDatabase()
        {
            connection = new CGestConnection(this);
        }

        //********** END CONSTRUCTEUR **********//

        //Requête d'initalisation du dataset 

        //!!! Ajout de la gestion des erreur !!!//
        public void initDataset()
        {
            ds = new CGestDataset(this, connection);

            ds.init();

        }

        //Requête de connection a la base de donnée
        public CGestMessages connect(string user, string password, string databaseName, string datasource, string port)
        {
            if (user != "" && password != "" && databaseName != "" && datasource != "" && port != "")
            {
                return connection.create(user, password, databaseName, datasource, port);
            }
            else
            {
                return CGestMessages.ERR_000;
            }
        }

        //Requête de fermeture de la connection
        public CGestMessages close()
        {
            if (this.connection.close())
            {
                return CGestMessages.SUC_101;
            }
            else
            {
                return CGestMessages.ERR_101;
            }
        }

        private bool changeDatabase(string dbName)
        {
            return connection.changeDatabase(dbName);
        }

        public bool updateDatabase(string dbName)
        {
            try
            {
                NpgsqlCommand cmd = connection.DatabaseConnection.CreateCommand();

                cmd.Connection = connection.DatabaseConnection;
                cmd.CommandText = "CREATE DATABASE \"" + dbName + "\" WITH OWNER = postgres ENCODING = 'UTF8' TABLESPACE = pg_default LC_COLLATE = 'French_France.1252' LC_CTYPE = 'French_France.1252' CONNECTION LIMIT = -1;";

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                if (!e.Message.Contains("42P04"))
                {
                    return false;
                }
            }

            changeDatabase(dbName);

            List<string> cmds = new List<string>();

            cmds.Add("CREATE TABLE IF NOT EXISTS users ( id_user integer NOT NULL, username text NOT NULL, password text NOT NULL, last_connection text NOT NULL, color text, priority text, CONSTRAINT \"userPK\" PRIMARY KEY (id_user) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE users OWNER TO postgres;");
            cmds.Add("ALTER TABLE users ADD COLUMN color text;");
            cmds.Add("ALTER TABLE users ADD COLUMN priority text;");

            foreach (string cmdString in cmds)
            {
                try
                {
                    NpgsqlCommand cmd = connection.DatabaseConnection.CreateCommand();

                    cmd = connection.DatabaseConnection.CreateCommand();
                    cmd.Connection = connection.DatabaseConnection;

                    cmd.CommandText = cmdString;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    if (!e.Message.Contains("42P07") && !e.Message.Contains("42701"))
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public bool updateRows()
        {
            try
            {

                foreach (DataRow dr in Ds.Dataset.Tables["user"].Rows)
                {
                    object[] obj = dr.ItemArray;

                    if (dr.ItemArray[4].ToString() == "")
                    {
                        obj[4] = "RED";
                    }

                    if (dr.ItemArray[5].ToString() == "")
                    {
                        obj[5] = "0";
                    }

                    ds.Dataset.Tables["user"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = obj;
                }

                ds.updateDatabase();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
