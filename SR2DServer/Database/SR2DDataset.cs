using SR2DServer.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SR2DServer.Database
{
    public class SR2DDataset //See AdminApp comments, it works the same way
    {
        //Décalration des adapters
        MySqlDataAdapter userDataAdapter;
        MySqlDataAdapter rankDataAdapter;

        //Variables de database
        SR2DConnection connection;
        SR2DDatabase database;
        private DataSet ds = new DataSet();
        public DataSet Dataset { get { return ds; } }

        public delegate void EventHandler();
        public event EventHandler DatabaseUpdatedEvent;

        //********** CONSTRUCTEUR **********//

        public SR2DDataset(SR2DDatabase db, SR2DConnection co)
        {
            this.database = db;
            this.connection = co;
        }

        //********** END CONSTRUCTEUR **********//

        //Initialisation du dataset
        public bool init()
        {
            //***** Déclaration des tables *****//

            DataTable userTable = new DataTable();
            DataTable rankTable = new DataTable();

            userTable.TableName = "user";
            rankTable.TableName = "ranks";

            //***** End Déclaration des tables *****//

            //Création des adapter et remplissage du dataset
            try
            {
                //User table adapter

                userDataAdapter = new MySqlDataAdapter();

                userDataAdapter.SelectCommand = new MySqlCommand("select * from user");
                userDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                userDataAdapter.InsertCommand = new MySqlCommand("insert into user(name, password, rank) values (@u, @p, @r)", connection.DatabaseConnection);
                userDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                userDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("u", MySqlDbType.Text));
                userDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("p", MySqlDbType.Text));
                userDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("r", MySqlDbType.Int16));
                userDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                userDataAdapter.InsertCommand.Parameters[0].SourceColumn = "name";
                userDataAdapter.InsertCommand.Parameters[1].SourceColumn = "password";
                userDataAdapter.InsertCommand.Parameters[2].SourceColumn = "rank";

                userDataAdapter.UpdateCommand = new MySqlCommand("update user set(name, password, rank) = (@u, @p, @r) where id = @idU", connection.DatabaseConnection);
                userDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                userDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("idU", MySqlDbType.Int16));
                userDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("u", MySqlDbType.Text));
                userDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("p", MySqlDbType.Text));
                userDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("r", MySqlDbType.Int16));
                userDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                userDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id";
                userDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name";
                userDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "password";
                userDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "rank";


                userDataAdapter.DeleteCommand = new MySqlCommand("delete from user where id = @id", connection.DatabaseConnection);
                userDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                userDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                userDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                userDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                //Ranks table adapter

                rankDataAdapter = new MySqlDataAdapter();

                rankDataAdapter.SelectCommand = new MySqlCommand("select * from ranks");
                rankDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                rankDataAdapter.InsertCommand = new MySqlCommand("insert into ranks(rank_number, name) values (@rN, @n)", connection.DatabaseConnection);
                rankDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                rankDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("rN", MySqlDbType.Int16));
                rankDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                rankDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                rankDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                rankDataAdapter.InsertCommand.Parameters[0].SourceColumn = "rank_number";
                rankDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name";

                rankDataAdapter.UpdateCommand = new MySqlCommand("update ranks set(rank_number, name) = (@rN, @n) where id = @id", connection.DatabaseConnection);
                rankDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                rankDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("rN", MySqlDbType.Int16));
                rankDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                rankDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                rankDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                rankDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                rankDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                rankDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "rank_number";
                rankDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name";
                rankDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "id";


                rankDataAdapter.DeleteCommand = new MySqlCommand("delete from ranks where id = @id", connection.DatabaseConnection);
                rankDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                rankDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                rankDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                rankDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                //Remplissage des tables
                userDataAdapter.Fill(userTable);
                rankDataAdapter.Fill(rankTable);

                //Modifiaction des tables
                DataColumn[] userDC = { null };
                DataColumn[] rankDC = { null };

                userDC[0] = userTable.Columns["id"];
                rankDC[0] = rankTable.Columns["id"];

                userTable.PrimaryKey = userDC;
                rankTable.PrimaryKey = rankDC;

                //Remplissage du dataset
                ds.Tables.Add(userTable);
                ds.Tables.Add(rankTable);

                return true;
            }
            catch (Exception e)
            {
                SR2DLogger.error(e, false);

                return false;
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

            if (ds.Tables["ranks"].GetChanges() != null)
            {
                userDataAdapter.Update(ds.Tables["ranks"].GetChanges());
                ds.Tables["ranks"].Clear();
                userDataAdapter.Fill(ds.Tables["ranks"]);
            }

            if(DatabaseUpdatedEvent != null)
            {
                
                DatabaseUpdatedEvent();
            }


            return true;
        }

        public void rowUpdatedEvent(object sender, MySqlRowUpdatedEventArgs e)
        {
            SR2DLogger.log(e.StatementType.ToString(), false);
        }

    }
}