using SR2DAdminApp.Utilities;
using MySql;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SR2DAdminApp.Database
{
    public class SR2DDataset
    {
        /*DataAdapters*/
        MySqlDataAdapter badgesDataAdapter;
        MySqlDataAdapter foodDataAdapter;
        MySqlDataAdapter foodTypesDataAdapter;
        MySqlDataAdapter bookingDataAdapter;
        MySqlDataAdapter varsDataAdapter;
        MySqlDataAdapter excludedFoodTypesDataAdapter;

        /*Events*/
        public delegate void EventHandler();
        public event EventHandler DatabaseUpdatedEvent;

        public event EventHandler DatasetUpdatedEvent;

        /*Vars*/
        SR2DConnection connection;
        SR2DDatabase database;
        private DataSet ds = new DataSet();
        public DataSet Dataset { get { return ds; } }

        public SR2DDataset(SR2DDatabase db, SR2DConnection co)
        {
            this.database = db;
            this.connection = co;
        }

        //Init all database adapters
        public void init()
        {
            /*Dataset tables*/
            DataTable badgesTable = new DataTable();
            DataTable foodTable = new DataTable();
            DataTable foodTypesTable = new DataTable();
            DataTable bookingTable = new DataTable();
            DataTable varsTable = new DataTable();
            DataTable excludedFoodTypesTable = new DataTable();

            /*Tables name*/
            badgesTable.TableName = "badges";
            foodTable.TableName = "food";
            foodTypesTable.TableName = "food_types";
            bookingTable.TableName = "booking";
            varsTable.TableName = "vars";
            excludedFoodTypesTable.TableName = "excluded_food_types_join";

            try
            {
                //Badges table adapter
                badgesDataAdapter = new MySqlDataAdapter();

                //Select command
                badgesDataAdapter.SelectCommand = new MySqlCommand("select * from badges");
                badgesDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                //Insert command
                badgesDataAdapter.InsertCommand = new MySqlCommand("insert into badges(first_name, name, code_id, class_code, class_fullname, passed, retired) values (@fn, @n, @cID, @clC, @clFn, @p, @r)", connection.DatabaseConnection);
                badgesDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("fn", MySqlDbType.Text));
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("cID", MySqlDbType.Text));
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("clC", MySqlDbType.Text));
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("clFn", MySqlDbType.Text));
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("p", MySqlDbType.Int16));
                badgesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("r", MySqlDbType.Int16));
                badgesDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
                badgesDataAdapter.InsertCommand.Parameters[0].SourceColumn = "first_name";
                badgesDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name";
                badgesDataAdapter.InsertCommand.Parameters[2].SourceColumn = "code_id";
                badgesDataAdapter.InsertCommand.Parameters[3].SourceColumn = "class_code";
                badgesDataAdapter.InsertCommand.Parameters[4].SourceColumn = "class_fullname";
                badgesDataAdapter.InsertCommand.Parameters[5].SourceColumn = "passed";
                badgesDataAdapter.InsertCommand.Parameters[6].SourceColumn = "retired";

                //Update command
                badgesDataAdapter.UpdateCommand = new MySqlCommand("update badges set first_name=@fn, name=@n, code_id=@cID, class_code=@clC, class_fullname=@clFn, passed=@p, retired=@r where id = @id", connection.DatabaseConnection);
                badgesDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("fn", MySqlDbType.Text));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("cID", MySqlDbType.Text));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("clC", MySqlDbType.Text));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("clFn", MySqlDbType.Text));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("p", MySqlDbType.Int16));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("r", MySqlDbType.Int16));
                badgesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                badgesDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[6].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[7].Direction = ParameterDirection.Input;
                badgesDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "first_name";
                badgesDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name";
                badgesDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "code_id";
                badgesDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "class_code";
                badgesDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "class_fullname";
                badgesDataAdapter.UpdateCommand.Parameters[5].SourceColumn = "passed";
                badgesDataAdapter.UpdateCommand.Parameters[6].SourceColumn = "retired";
                badgesDataAdapter.UpdateCommand.Parameters[7].SourceColumn = "id";

                //Delete command
                badgesDataAdapter.DeleteCommand = new MySqlCommand("delete from badges where id = @id", connection.DatabaseConnection);
                badgesDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                badgesDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                badgesDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                badgesDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                /*DEBUG ONLY*/
                badgesDataAdapter.RowUpdated += new MySqlRowUpdatedEventHandler(rowUpdatedEvent);
                /*DEBUG ONLY*/

                //Fill tables thanks to select command
                badgesDataAdapter.Fill(badgesTable);

                //Food table adapter
                foodDataAdapter = new MySqlDataAdapter();

                foodDataAdapter.SelectCommand = new MySqlCommand("select * from food");
                foodDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                foodDataAdapter.InsertCommand = new MySqlCommand("insert into food(name, quantity, type_id, price) values (@n, @q, @tID, @p)", connection.DatabaseConnection);
                foodDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                foodDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                foodDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("q", MySqlDbType.Int16));
                foodDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("tID", MySqlDbType.Int16));
                foodDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("p", MySqlDbType.Decimal));
                foodDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                foodDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                foodDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                foodDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                foodDataAdapter.InsertCommand.Parameters[0].SourceColumn = "name";
                foodDataAdapter.InsertCommand.Parameters[1].SourceColumn = "quantity";
                foodDataAdapter.InsertCommand.Parameters[2].SourceColumn = "type_id";
                foodDataAdapter.InsertCommand.Parameters[3].SourceColumn = "price";

                foodDataAdapter.UpdateCommand = new MySqlCommand("update food set name=@n, quantity=@q, type_id=@tID, price=@p where id = @id", connection.DatabaseConnection);
                foodDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                foodDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                foodDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("q", MySqlDbType.Int16));
                foodDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("tID", MySqlDbType.Int16));
                foodDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("p", MySqlDbType.Int16));
                foodDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                foodDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                foodDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                foodDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                foodDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                foodDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                foodDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "name";
                foodDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "quantity";
                foodDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "type_id";
                foodDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "price";
                foodDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "id";

                foodDataAdapter.DeleteCommand = new MySqlCommand("delete from food where id = @id", connection.DatabaseConnection);
                foodDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                foodDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                foodDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                foodDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                foodDataAdapter.RowUpdated += new MySqlRowUpdatedEventHandler(rowUpdatedEvent);

                foodDataAdapter.Fill(foodTable);

                //Food types table adapter
                foodTypesDataAdapter = new MySqlDataAdapter();

                foodTypesDataAdapter.SelectCommand = new MySqlCommand("select * from food_types");
                foodTypesDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                foodTypesDataAdapter.InsertCommand = new MySqlCommand("insert into food_types(id, name) values (@id, @n)", connection.DatabaseConnection);
                foodTypesDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                foodTypesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                foodTypesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                foodTypesDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                foodTypesDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                foodTypesDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id";
                foodTypesDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name";

                foodTypesDataAdapter.UpdateCommand = new MySqlCommand("update food_types set name=@n where id = @id", connection.DatabaseConnection);
                foodTypesDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                foodTypesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                foodTypesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("n", MySqlDbType.Text));
                foodTypesDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                foodTypesDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                foodTypesDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id";
                foodTypesDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name";

                foodTypesDataAdapter.DeleteCommand = new MySqlCommand("delete from food_types where id = @id", connection.DatabaseConnection);
                foodTypesDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                foodTypesDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                foodTypesDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                foodTypesDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                foodTypesDataAdapter.Fill(foodTypesTable);

                //Booking table adapter
                bookingDataAdapter = new MySqlDataAdapter();

                bookingDataAdapter.SelectCommand = new MySqlCommand("select * from booking");
                bookingDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                bookingDataAdapter.InsertCommand = new MySqlCommand("insert into booking(badge_id, food_id) values (@idB, @idF)", connection.DatabaseConnection);
                bookingDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                bookingDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("idB", MySqlDbType.Int16));
                bookingDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("idF", MySqlDbType.Int16));
                bookingDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                bookingDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                bookingDataAdapter.InsertCommand.Parameters[0].SourceColumn = "badge_id";
                bookingDataAdapter.InsertCommand.Parameters[1].SourceColumn = "food_id";

                bookingDataAdapter.UpdateCommand = new MySqlCommand("update booking set badge_id=@idB, food_id=@idF where id = @id", connection.DatabaseConnection);
                bookingDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                bookingDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                bookingDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("idB", MySqlDbType.Int16));
                bookingDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("idF", MySqlDbType.Int16));
                bookingDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                bookingDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                bookingDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                bookingDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id";
                bookingDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "badge_id";
                bookingDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "food_id";

                bookingDataAdapter.DeleteCommand = new MySqlCommand("delete from booking where id = @id", connection.DatabaseConnection);
                bookingDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                bookingDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                bookingDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                bookingDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                bookingDataAdapter.Fill(bookingTable);

                //Vars table adapter
                varsDataAdapter = new MySqlDataAdapter();

                varsDataAdapter.SelectCommand = new MySqlCommand("select * from vars");
                varsDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                varsDataAdapter.InsertCommand = new MySqlCommand("insert into vars(k, value) values (@k, @v)", connection.DatabaseConnection);
                varsDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                varsDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("k", MySqlDbType.Text));
                varsDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("v", MySqlDbType.Text));
                varsDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                varsDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                varsDataAdapter.InsertCommand.Parameters[0].SourceColumn = "k";
                varsDataAdapter.InsertCommand.Parameters[1].SourceColumn = "value";

                varsDataAdapter.UpdateCommand = new MySqlCommand("update vars set k=@k, value=@v where id=@id", connection.DatabaseConnection);
                varsDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                varsDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("k", MySqlDbType.Text));
                varsDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("v", MySqlDbType.Text));
                varsDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                varsDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                varsDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                varsDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                varsDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "k";
                varsDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "value";
                varsDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "id";

                varsDataAdapter.DeleteCommand = new MySqlCommand("delete from vars where id = @id", connection.DatabaseConnection);
                varsDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                varsDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                varsDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                varsDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                varsDataAdapter.Fill(varsTable);

                //Excluded food types table adapter
                excludedFoodTypesDataAdapter = new MySqlDataAdapter();

                excludedFoodTypesDataAdapter.SelectCommand = new MySqlCommand("select * from excluded_food_types_join");
                excludedFoodTypesDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                excludedFoodTypesDataAdapter.InsertCommand = new MySqlCommand("insert into excluded_food_types_join(master_food_types, slave_food_types) values (@mFT, @sFT)", connection.DatabaseConnection);
                excludedFoodTypesDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                excludedFoodTypesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("mFT", MySqlDbType.Int16));
                excludedFoodTypesDataAdapter.InsertCommand.Parameters.Add(new MySqlParameter("sFT", MySqlDbType.Int16));
                excludedFoodTypesDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                excludedFoodTypesDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                excludedFoodTypesDataAdapter.InsertCommand.Parameters[0].SourceColumn = "master_food_types";
                excludedFoodTypesDataAdapter.InsertCommand.Parameters[1].SourceColumn = "slave_food_types";

                excludedFoodTypesDataAdapter.UpdateCommand = new MySqlCommand("update excluded_food_types_join set master_food_types=@mFT, slave_food_types=@sFT where id = @id", connection.DatabaseConnection);
                excludedFoodTypesDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("mFT", MySqlDbType.Int16));
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("sFT", MySqlDbType.Int16));
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "master_food_types";
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "slave_food_types";
                excludedFoodTypesDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "id";

                excludedFoodTypesDataAdapter.DeleteCommand = new MySqlCommand("delete from excluded_food_types_join where id = @id", connection.DatabaseConnection);
                excludedFoodTypesDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                excludedFoodTypesDataAdapter.DeleteCommand.Parameters.Add(new MySqlParameter("id", MySqlDbType.Int16));
                excludedFoodTypesDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                excludedFoodTypesDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                excludedFoodTypesDataAdapter.Fill(excludedFoodTypesTable);

                //Create datacolumn
                DataColumn[] badgesDC = { null };
                DataColumn[] foodDC = { null };
                DataColumn[] foodTypesDC = { null };
                DataColumn[] bookingDC = { null };
                DataColumn[] varsDC = { null };
                DataColumn[] excludedFoodTypesDC = { null };

                //Set column to be the id column for table
                badgesDC[0] = badgesTable.Columns["id"];
                foodDC[0] = foodTable.Columns["id"];
                foodTypesDC[0] = foodTypesTable.Columns["id"];
                bookingDC[0] = bookingTable.Columns["id"];
                varsDC[0] = varsTable.Columns["id"];
                excludedFoodTypesDC[0] = excludedFoodTypesTable.Columns["id"];

                //Set table primary key (id) column
                badgesTable.PrimaryKey = badgesDC;
                foodTable.PrimaryKey = foodDC;
                foodTypesTable.PrimaryKey = foodTypesDC;
                bookingTable.PrimaryKey = bookingDC;
                varsTable.PrimaryKey = varsDC;
                excludedFoodTypesTable.PrimaryKey = excludedFoodTypesDC;

                //Add table to the dataset
                ds.Tables.Add(badgesTable);
                ds.Tables.Add(foodTable);
                ds.Tables.Add(foodTypesTable);
                ds.Tables.Add(bookingTable);
                ds.Tables.Add(varsTable);
                ds.Tables.Add(excludedFoodTypesTable);

            }
            catch (Exception e)
            {
                SR2DLogger.error(e, false);
            }
            
        }

        //Create the dataset if doesn't exist
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

        //Update specified tables rows
        public bool updateDatabase(string[] table)
        {
            foreach (string s in table)
            {
                switch (s)
                {
                    case "badges":
                        if (ds.Tables["badges"].GetChanges() != null)
                        {
                            badgesDataAdapter.Update(ds.Tables["badges"].GetChanges());
                        }
                        break;
                    case "food":
                        if (ds.Tables["food"].GetChanges() != null)
                        {
                            foodDataAdapter.Update(ds.Tables["food"].GetChanges());
                        }
                        break;
                    case "foodTypes":
                        if (ds.Tables["food_types"].GetChanges() != null)
                        {
                            foodTypesDataAdapter.Update(ds.Tables["food_types"].GetChanges());
                        }
                        break;
                    case "booking":
                        if (ds.Tables["booking"].GetChanges() != null)
                        {
                            bookingDataAdapter.Update(ds.Tables["booking"].GetChanges());
                        }
                        break;
                    case "vars":
                        if (ds.Tables["vars"].GetChanges() != null)
                        {
                            varsDataAdapter.Update(ds.Tables["vars"].GetChanges());
                        }
                        break;
                    case "excludedFoodTypes":
                        if (ds.Tables["excluded_food_types_join"].GetChanges() != null)
                        {
                            excludedFoodTypesDataAdapter.Update(ds.Tables["excluded_food_types_join"].GetChanges());
                        }
                        break;
                }
            }

            //Raise database update event
            DatabaseUpdatedEvent();

            return true;

        }

        //Update dataset specified tables
        public bool updateDataset(string[] table)
        {
            try
            {
                foreach (string s in table)
                {
                    switch (s)
                    {
                        case "badges":
                            ds.Tables["badges"].Clear();
                            badgesDataAdapter.Fill(ds.Tables["badges"]);
                            break;
                        case "food":
                            ds.Tables["food"].Clear();
                            foodDataAdapter.Fill(ds.Tables["food"]);
                            break;
                        case "foodTypes":
                            ds.Tables["food_types"].Clear();
                            foodTypesDataAdapter.Fill(ds.Tables["food_types"]);
                            break;
                        case "booking":
                            ds.Tables["booking"].Clear();
                            bookingDataAdapter.Fill(ds.Tables["booking"]);
                            break;
                        case "vars":
                            ds.Tables["vars"].Clear();
                            varsDataAdapter.Fill(ds.Tables["vars"]);
                            break;
                        case "excludedFoodTypes":
                            ds.Tables["excluded_food_types_join"].Clear();
                            excludedFoodTypesDataAdapter.Fill(ds.Tables["excluded_food_types_join"]);
                            break;
                    }
                }

                if (DatasetUpdatedEvent != null)
                {
                    //Raise dataset update event
                    DatasetUpdatedEvent();
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }

        //Why is it here ???
        public void rowUpdatedEvent(object sender, MySqlRowUpdatedEventArgs e)
        {
            SR2DLogger.log(e.StatementType.ToString(), false);
        }

    }
}