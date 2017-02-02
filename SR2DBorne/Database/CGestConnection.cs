﻿using CGest.Database;
using CGest.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest
{
    public class CGestConnection
    {
        CGestDatabase database;

        private string statut;
        public string Statut { get { return statut; } }
        private NpgsqlConnection databaseConnection;
        public NpgsqlConnection DatabaseConnection { get { return databaseConnection; } }


        public CGestConnection(CGestDatabase database)
        {
            this.database = database;
            statut = "closed";
        }

        public CGestMessages create(string user, string password, string databaseName, string datasource, string port)
        {
            string connectionString = "Server=" + datasource + ";Port=" + port + ";Database=" + databaseName + ";User Id=" + user + ";Password=" + password + ";";

            databaseConnection = new NpgsqlConnection(connectionString);

            try
            {
                databaseConnection.Open();

                CGestLogger.log("The database connection has been opened", false);
                statut = "opened";

                return CGestMessages.SUC_100 ;
            }
            catch (Exception e)
            {
                CGestLogger.error(e, false);

                statut = "closed";

                return CGestMessages.ERR_100;
            }
        }

        public bool close()
        {
            try
            {
                databaseConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                CGestLogger.error(e, false);

                return false;
            }
        }

        public bool changeDatabase(string dbName)
        {
            try
            {
                databaseConnection.ChangeDatabase(dbName);

                CGestLogger.log("Database changed", false);

                return true;
            }
            catch (Exception e)
            {
                CGestLogger.error(e, false);

                statut = "closed";

                return false;
            }
        }

    }
}
