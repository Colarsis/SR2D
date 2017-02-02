using CGestServer.Utilities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGestServer.Database
{
    public class CGestDataset
    {
        //Décalration des adapters
        NpgsqlDataAdapter userDataAdapter;

        //Variables de database
        CGestConnection connection;
        CGestDatabase database;
        private DataSet ds = new DataSet();
        public DataSet Dataset { get { return ds; } }

        public delegate void EventHandler();
        public event EventHandler DatabaseUpdatedEvent;

        //********** CONSTRUCTEUR **********//

        public CGestDataset(CGestDatabase db, CGestConnection co)
        {
            this.database = db;
            this.connection = co;
        }

        //********** END CONSTRUCTEUR **********//

        //Initialisation du dataset
        public void init()
        {
            //***** Déclaration des tables *****//

            DataTable userTable = new DataTable();

            userTable.TableName = "user";

            //***** End Déclaration des tables *****//

            //Création des adapter et remplissage du dataset
            try
            {
                //User table adapter

                userDataAdapter = new NpgsqlDataAdapter();

                userDataAdapter.SelectCommand = new NpgsqlCommand("select * from users");
                userDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                userDataAdapter.InsertCommand = new NpgsqlCommand("insert into users(id_user, username, password, last_connection, color, priority) values (:idU, :u, :p, :lC, :c, :pr)", connection.DatabaseConnection);
                userDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                userDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                userDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("u", NpgsqlDbType.Text));
                userDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("p", NpgsqlDbType.Text));
                userDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("lC", NpgsqlDbType.Text));
                userDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("c", NpgsqlDbType.Text));
                userDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("pr", NpgsqlDbType.Integer));
                userDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_user";
                userDataAdapter.InsertCommand.Parameters[1].SourceColumn = "username";
                userDataAdapter.InsertCommand.Parameters[2].SourceColumn = "password";
                userDataAdapter.InsertCommand.Parameters[3].SourceColumn = "last_connection";
                userDataAdapter.InsertCommand.Parameters[4].SourceColumn = "color";
                userDataAdapter.InsertCommand.Parameters[5].SourceColumn = "priority";

                userDataAdapter.UpdateCommand = new NpgsqlCommand("update users set(username, password, last_connection, color, priority) = (:u, :p, :lC, :c, :pr) where id_user = :idU", connection.DatabaseConnection);
                userDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                userDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                userDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("u", NpgsqlDbType.Text));
                userDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("p", NpgsqlDbType.Text));
                userDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("lC", NpgsqlDbType.Text));
                userDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("c", NpgsqlDbType.Text));
                userDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("pr", NpgsqlDbType.Integer));
                userDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_user";
                userDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "username";
                userDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "password";
                userDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "last_connection";
                userDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "color";
                userDataAdapter.UpdateCommand.Parameters[5].SourceColumn = "priority";


                userDataAdapter.DeleteCommand = new NpgsqlCommand("delete from users where id_user = :idU", connection.DatabaseConnection);
                userDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                userDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                userDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                userDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_user";

                //Remplissage des tables
                userDataAdapter.Fill(userTable);

                //Modifiaction des tables
                DataColumn[] userDC = { null };

                userDC[0] = userTable.Columns["id_user"];

                userTable.PrimaryKey = userDC;

                //Remplissage du dataset
                ds.Tables.Add(userTable);
            }
            catch (Exception e)
            {
                CGestLogger.error(e, false);
            }

        }

        public bool createDS()
        {
            if (ds != null)
            {
                ds = new DataSet();

                return true;
            }
            else
            {
                return false;
            }
        }

        //Mise a jour de la base de donnée
        public bool updateDatabase()
        {
            if (ds.Tables["user"].GetChanges() != null)
            {
                userDataAdapter.Update(ds.Tables["user"].GetChanges());
                ds.Tables["user"].Clear();
                userDataAdapter.Fill(ds.Tables["user"]);
            }

            if(DatabaseUpdatedEvent != null)
            {
                
                DatabaseUpdatedEvent();
            }


            return true;
        }

        public void rowUpdatedEvent(object sender, NpgsqlRowUpdatedEventArgs e)
        {
            CGestLogger.log(e.StatementType.ToString(), false);
        }

    }
}