using CGest.Forms;
using CGest.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Database
{
    public class CGestDatabase
    {
        private CGestDataset ds;
        private CGestConnection connection;
        public CGestDataset Ds { get { return ds; } }
        public CGestConnection Connection { get { return connection; } }

        public CGestDatabase()
        {
            connection = new CGestConnection(this);
            ds = new CGestDataset(this, connection);
        }

        public void initDataset()
        {
            ds.init();
        }

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

            //Tables
            cmds.Add("CREATE TABLE IF NOT EXISTS client ( id_client serial NOT NULL, name_client text NOT NULL, surname_client text, addr_client text, postal_client integer, city_client text, relation_client text, desc_client text, \"phoneNumber\" text, \"mobileNumber\" text, mail text,CONSTRAINT \"clé_client\" PRIMARY KEY (id_client) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE client OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS events ( id serial NOT NULL, title text, \"desc\" text, begin text, \"end\" text, proprietary integer, type text, affected integer, CONSTRAINT even_pkey PRIMARY KEY (id) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE events OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS chantier ( id_chantier serial NOT NULL, id_client integer NOT NULL, name_chantier text NOT NULL, CONSTRAINT \"clé_chantier\" PRIMARY KEY (id_chantier),CONSTRAINT \"clé_client\" FOREIGN KEY (id_client) REFERENCES client (id_client) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE chantier OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS unit( id_unit serial NOT NULL, name_unit text, CONSTRAINT id_unit PRIMARY KEY (id_unit) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE unit OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS materiaux ( id_materiaux serial NOT NULL, name_materiaux text NOT NULL, id_unit integer NOT NULL, description text, CONSTRAINT \"clé_materiaux\" PRIMARY KEY (id_materiaux) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE materiaux OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS stock ( id_stock serial NOT NULL, id_chantier integer NOT NULL, id_mat integer NOT NULL, quantity integer NOT NULL, CONSTRAINT \"clé_stock\" PRIMARY KEY (id_stock), CONSTRAINT \"clé_chantier\" FOREIGN KEY (id_chantier) REFERENCES chantier (id_chantier) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT \"clé_materiaux\" FOREIGN KEY (id_mat) REFERENCES materiaux (id_materiaux) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE stock OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS representant ( id_representant serial NOT NULL, \"phoneNbr\" integer NOT NULL, name_representant text NOT NULL, surname_representant text NOT NULL, CONSTRAINT \"clé_représentants\" PRIMARY KEY (id_representant) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE representant OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS fournisseur ( id_fournisseur serial NOT NULL, name_fournisseur text NOT NULL, \"phoneNbr\" integer NOT NULL, \"faxNbr\" integer, id_representant integer NOT NULL, adresse text, CONSTRAINT \"clé_fournisseur\" PRIMARY KEY (id_fournisseur), CONSTRAINT representant_to_this FOREIGN KEY (id_representant) REFERENCES representant (id_representant) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE fournisseur OWNER TO postgres;");
            cmds.Add("CREATE SEQUENCE \"stockAndFourn_id_seq\" INCREMENT 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1;");
            cmds.Add("CREATE TABLE IF NOT EXISTS \"matAndFourn\" ( id_materiaux integer NOT NULL, id_fournisseur integer NOT NULL, \"prix_HT\" real NOT NULL, \"prix_TTC\" real NOT NULL, id integer NOT NULL DEFAULT nextval('\"stockAndFourn_id_seq\"'::regclass), CONSTRAINT \"clé_this\" PRIMARY KEY (id), CONSTRAINT fourn_to_mat FOREIGN KEY (id_fournisseur) REFERENCES fournisseur (id_fournisseur) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT mat_to_this FOREIGN KEY (id_materiaux) REFERENCES materiaux (id_materiaux) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE \"matAndFourn\" OWNER TO postgres;") ;
            cmds.Add("CREATE TABLE IF NOT EXISTS meetings ( id integer NOT NULL, client_id integer, chantier_id integer, event_id integer, \"desc\" text, report text, status text, CONSTRAINT meetings_pk PRIMARY KEY (id), CONSTRAINT chantier_sk FOREIGN KEY (chantier_id) REFERENCES chantier (id_chantier) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT client_sk FOREIGN KEY (client_id) REFERENCES client (id_client) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT event_sk FOREIGN KEY (event_id) REFERENCES events (id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE meetings OWNER TO postgres;");
            cmds.Add("CREATE TABLE IF NOT EXISTS employees ( id integer NOT NULL, name text, surname text, addr text, postal integer, city text, \"phoneNumber\" text, \"mobileNumber\" text, mail text, remarks text, id_server integer, CONSTRAINT employees_pk PRIMARY KEY (id) ) WITH ( OIDS=FALSE );");
            cmds.Add("ALTER TABLE employees OWNER TO postgres;");

            //Columns
            cmds.Add("ALTER TABLE client ADD COLUMN surname_client text;");
            cmds.Add("ALTER TABLE client ADD COLUMN addr_client text;");
            cmds.Add("ALTER TABLE client ADD COLUMN postal_client integer;");
            cmds.Add("ALTER TABLE client ADD COLUMN city_client text;");
            cmds.Add("ALTER TABLE client ADD COLUMN relation_client text;");
            cmds.Add("ALTER TABLE client ADD COLUMN desc_client text;");
            cmds.Add("ALTER TABLE client ADD COLUMN \"phoneNumber\" text;");
            cmds.Add("ALTER TABLE client ADD COLUMN \"mobileNumber\" text;");
            cmds.Add("ALTER TABLE client ADD COLUMN mail text;");

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
            string[] tables = {"client"};

            try
            {
                foreach (DataRow dr in Ds.Dataset.Tables["client"].Rows)
                {
                    object[] obj = dr.ItemArray;

                    if (dr.ItemArray[2].ToString() == "")
                    {
                        obj[2] = "default";
                    }

                    if (dr.ItemArray[3].ToString() == "")
                    {
                        obj[3] = "default";
                    }

                    if (dr.ItemArray[4].ToString() == "")
                    {
                        obj[4] = 00000;
                    }

                    if (dr.ItemArray[5].ToString() == "")
                    {
                        obj[5] = "default";
                    }

                    if (dr.ItemArray[6].ToString() == "")
                    {
                        obj[6] = "null";
                    }

                    if (dr.ItemArray[7].ToString() == "")
                    {
                        obj[7] = "default";
                    }

                    if (dr.ItemArray[8].ToString() == "")
                    {
                        obj[8] = "000000000000";
                    }

                    if (dr.ItemArray[9].ToString() == "")
                    {
                        obj[9] = "000000000000";
                    }

                    if (dr.ItemArray[10].ToString() == "")
                    {
                        obj[10] = "default@default.com";
                    }

                    ds.Dataset.Tables["client"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = obj;
                }

                ds.updateDatabase(tables);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace)
                    ;
                return false;
            }
        }

    }
}
