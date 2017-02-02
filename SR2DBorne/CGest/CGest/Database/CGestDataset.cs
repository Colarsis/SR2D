using CGest.Utilities;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGest.Database
{
    public class CGestDataset
    {
        NpgsqlDataAdapter materiauxDataAdapter;
        NpgsqlDataAdapter stockDataAdapter;
        NpgsqlDataAdapter unitDataAdapter;
        NpgsqlDataAdapter chantierDataAdapter;
        NpgsqlDataAdapter fournisseurDataAdapter;
        NpgsqlDataAdapter representantDataAdapter;
        NpgsqlDataAdapter matAndFournDataAdapter;
        NpgsqlDataAdapter clientDataAdapter;
        NpgsqlDataAdapter eventDataAdapter;
        NpgsqlDataAdapter meetingsDataAdapter;
        NpgsqlDataAdapter employeesDataAdapter;

        public delegate void EventHandler();
        public event EventHandler DatabaseUpdatedEvent;

        public event EventHandler DatasetUpdatedEvent;

        CGestConnection connection;
        CGestDatabase database;
        private DataSet ds = new DataSet();
        public DataSet Dataset { get { return ds; } }

        public CGestDataset(CGestDatabase db, CGestConnection co)
        {
            this.database = db;
            this.connection = co;
        }

        public void init()
        {
            DataTable materiauxTable = new DataTable();
            DataTable stockTable = new DataTable();
            DataTable unitTable = new DataTable();
            DataTable chantierTable = new DataTable();
            DataTable fournisseurTable = new DataTable();
            DataTable representantTable = new DataTable();
            DataTable matAndFournTable = new DataTable();
            DataTable clientTable = new DataTable();
            DataTable eventTable = new DataTable();
            DataTable meetingsTable = new DataTable();
            DataTable employeesTable = new DataTable();

            materiauxTable.TableName = "materiaux";
            stockTable.TableName = "stock";
            unitTable.TableName = "unit";
            chantierTable.TableName = "chantier";
            fournisseurTable.TableName = "fournisseur";
            representantTable.TableName = "representant";
            matAndFournTable.TableName = "mat-fourn-corres";
            clientTable.TableName = "client";
            eventTable.TableName = "event";
            meetingsTable.TableName = "meetings";
            employeesTable.TableName = "employees";

            try
            {
                //Materiaux table adapter

                materiauxDataAdapter = new NpgsqlDataAdapter();

                materiauxDataAdapter.SelectCommand = new NpgsqlCommand("select * from materiaux");
                materiauxDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                materiauxDataAdapter.InsertCommand = new NpgsqlCommand("insert into materiaux(id_materiaux, name_materiaux, id_unit, description) values (:idM, :nM, :idU, :desc)", connection.DatabaseConnection);
                materiauxDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                materiauxDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                materiauxDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nM", NpgsqlDbType.Text));
                materiauxDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                materiauxDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("desc", NpgsqlDbType.Text));
                materiauxDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                materiauxDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                materiauxDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                materiauxDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                materiauxDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_materiaux";
                materiauxDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name_materiaux";
                materiauxDataAdapter.InsertCommand.Parameters[2].SourceColumn = "id_unit";
                materiauxDataAdapter.InsertCommand.Parameters[3].SourceColumn = "description";

                materiauxDataAdapter.UpdateCommand = new NpgsqlCommand("update materiaux set (name_materiaux, id_unit, description) = (:nM, :idU, :desc) where id_materiaux = :idM", connection.DatabaseConnection);
                materiauxDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                materiauxDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                materiauxDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nM", NpgsqlDbType.Text));
                materiauxDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                materiauxDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("desc", NpgsqlDbType.Text));
                materiauxDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                materiauxDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                materiauxDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                materiauxDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                materiauxDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_materiaux";
                materiauxDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name_materiaux";
                materiauxDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "id_unit";
                materiauxDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "description";

                materiauxDataAdapter.DeleteCommand = new NpgsqlCommand("delete from materiaux where id_materiaux = :idM", connection.DatabaseConnection);
                materiauxDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                materiauxDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                materiauxDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                materiauxDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_materiaux";

                //materiauxDataAdapter.RowUpdated += new NpgsqlRowUpdatedEventHandler(rowUpdatedEvent);

                materiauxDataAdapter.Fill(materiauxTable);

                //Stock table adapter

                stockDataAdapter = new NpgsqlDataAdapter();

                stockDataAdapter.SelectCommand = new NpgsqlCommand("select * from stock");
                stockDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                stockDataAdapter.InsertCommand = new NpgsqlCommand("insert into stock(id_stock, id_chantier, id_mat, quantity) values (:idS, :idC, :idM, :q)", connection.DatabaseConnection);
                stockDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                stockDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idS", NpgsqlDbType.Integer));
                stockDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                stockDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                stockDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("q", NpgsqlDbType.Integer));
                stockDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                stockDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                stockDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                stockDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                stockDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_stock";
                stockDataAdapter.InsertCommand.Parameters[1].SourceColumn = "id_chantier";
                stockDataAdapter.InsertCommand.Parameters[2].SourceColumn = "id_mat";
                stockDataAdapter.InsertCommand.Parameters[3].SourceColumn = "quantity";

                stockDataAdapter.UpdateCommand = new NpgsqlCommand("update stock set (id_stock, id_chantier, id_mat, quantity) = (:idC, :idM, :q) where id_stock = :idS", connection.DatabaseConnection);
                stockDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                stockDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idS", NpgsqlDbType.Integer));
                stockDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                stockDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                stockDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("q", NpgsqlDbType.Integer));
                stockDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                stockDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                stockDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                stockDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                stockDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_stock";
                stockDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "id_chantier";
                stockDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "id_materiaux";
                stockDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "quantity";

                stockDataAdapter.DeleteCommand = new NpgsqlCommand("delete from stock where id_stock = :idS", connection.DatabaseConnection);
                stockDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                stockDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idS", NpgsqlDbType.Integer));
                stockDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                stockDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_stock";

                stockDataAdapter.RowUpdated += new NpgsqlRowUpdatedEventHandler(rowUpdatedEvent);

                stockDataAdapter.Fill(stockTable);

                //Unit table adapter

                unitDataAdapter = new NpgsqlDataAdapter("select * from unit", connection.DatabaseConnection);

                unitDataAdapter.SelectCommand = new NpgsqlCommand("select * from unit");
                unitDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                unitDataAdapter.InsertCommand = new NpgsqlCommand("insert into unit(id_unit, name_unit) values (:idU, :nU)", connection.DatabaseConnection);
                unitDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                unitDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                unitDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nU", NpgsqlDbType.Text));
                unitDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                unitDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                unitDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_unit";
                unitDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name_unit";

                unitDataAdapter.UpdateCommand = new NpgsqlCommand("update unit set (name_unit) = (:nU) where id_unit = :idU", connection.DatabaseConnection);
                unitDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                unitDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                unitDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nU", NpgsqlDbType.Text));
                unitDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                unitDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                unitDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_unit";
                unitDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name_unit";

                unitDataAdapter.DeleteCommand = new NpgsqlCommand("delete from unit where id_unit = :idU", connection.DatabaseConnection);
                unitDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                unitDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idU", NpgsqlDbType.Integer));
                unitDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                unitDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_unit";

                unitDataAdapter.Fill(unitTable);

                //Chantier table adapter

                chantierDataAdapter = new NpgsqlDataAdapter("select * from chantier", connection.DatabaseConnection);

                chantierDataAdapter.SelectCommand = new NpgsqlCommand("select * from chantier");
                chantierDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                chantierDataAdapter.InsertCommand = new NpgsqlCommand("insert into chantier(id_chantier, id_client, name_chantier) values (:idCh, :idCl, :nCh)", connection.DatabaseConnection);
                chantierDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                chantierDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idCh", NpgsqlDbType.Integer));
                chantierDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idCl", NpgsqlDbType.Integer));
                chantierDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nCh", NpgsqlDbType.Text));
                chantierDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                chantierDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                chantierDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                chantierDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_chantier";
                chantierDataAdapter.InsertCommand.Parameters[1].SourceColumn = "id_client";
                chantierDataAdapter.InsertCommand.Parameters[2].SourceColumn = "name_chantier";

                chantierDataAdapter.UpdateCommand = new NpgsqlCommand("update chantier set (id_client, name_chantier) = (:idCl, :nCh) where id_chantier = :idCh", connection.DatabaseConnection);
                chantierDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                chantierDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idCl", NpgsqlDbType.Integer));
                chantierDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nCh", NpgsqlDbType.Text));
                chantierDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idCh", NpgsqlDbType.Integer));
                chantierDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                chantierDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                chantierDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                chantierDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_client";
                chantierDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name_chantier";
                chantierDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "id_chantier";

                chantierDataAdapter.DeleteCommand = new NpgsqlCommand("delete from chantier where id_chantier = :idCh", connection.DatabaseConnection);
                chantierDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                chantierDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idCh", NpgsqlDbType.Integer));
                chantierDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                chantierDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_chantier";

                chantierDataAdapter.Fill(chantierTable);

                //Fournisseur table adapter

                fournisseurDataAdapter = new NpgsqlDataAdapter("select * from fournisseur", connection.DatabaseConnection);

                fournisseurDataAdapter.SelectCommand = new NpgsqlCommand("select * from fournisseur");
                fournisseurDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                fournisseurDataAdapter.InsertCommand = new NpgsqlCommand("insert into fournisseur(id_fournisseur, name_fournisseur, phoneNbr, faxNbr, id_representant, adresse) values (:idF, :nF, :phN, :fN, :idR, :a)", connection.DatabaseConnection);
                fournisseurDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                fournisseurDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idF", NpgsqlDbType.Integer));
                fournisseurDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nF", NpgsqlDbType.Text));
                fournisseurDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("phN", NpgsqlDbType.Integer));
                fournisseurDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("fN", NpgsqlDbType.Integer));
                fournisseurDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                fournisseurDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("a", NpgsqlDbType.Text));
                fournisseurDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_fournisseur";
                fournisseurDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name_fournisseur";
                fournisseurDataAdapter.InsertCommand.Parameters[2].SourceColumn = "phoneNumber";
                fournisseurDataAdapter.InsertCommand.Parameters[3].SourceColumn = "faxNumber";
                fournisseurDataAdapter.InsertCommand.Parameters[4].SourceColumn = "id_representant";
                fournisseurDataAdapter.InsertCommand.Parameters[5].SourceColumn = "adresse";

                fournisseurDataAdapter.UpdateCommand = new NpgsqlCommand("update fournisseur set (name_fournisseur, phoneNbr, faxNbr, id_representant, adresse) = (:nF, :phN, :fN, :idR, :a) where id_fournisseur = :idF", connection.DatabaseConnection);
                fournisseurDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                fournisseurDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idF", NpgsqlDbType.Integer));
                fournisseurDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nF", NpgsqlDbType.Text));
                fournisseurDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("phN", NpgsqlDbType.Integer));
                fournisseurDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("fN", NpgsqlDbType.Integer));
                fournisseurDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                fournisseurDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("a", NpgsqlDbType.Text));
                fournisseurDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_fournisseur";
                fournisseurDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name_fournisseur";
                fournisseurDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "phoneNumber";
                fournisseurDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "faxNumber";
                fournisseurDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "id_representant";
                fournisseurDataAdapter.UpdateCommand.Parameters[5].SourceColumn = "adresse";

                fournisseurDataAdapter.DeleteCommand = new NpgsqlCommand("delete from fournisseur where id_fournisseur = :idF", connection.DatabaseConnection);
                fournisseurDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                fournisseurDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idF", NpgsqlDbType.Integer));
                fournisseurDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                fournisseurDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_fournisseur";

                fournisseurDataAdapter.Fill(fournisseurTable);

                //Representant table adapter

                representantDataAdapter = new NpgsqlDataAdapter();

                representantDataAdapter.SelectCommand = new NpgsqlCommand("select * from representant");
                representantDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                representantDataAdapter.InsertCommand = new NpgsqlCommand("insert into representant(id_representant, phoneNbr, name_representant, surname_representant) values (:idR, :pN, :nR, :sR)", connection.DatabaseConnection);
                representantDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                representantDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                representantDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("pN", NpgsqlDbType.Integer));
                representantDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nR", NpgsqlDbType.Text));
                representantDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("sR", NpgsqlDbType.Text));
                representantDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                representantDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                representantDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                representantDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                representantDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_representant";
                representantDataAdapter.InsertCommand.Parameters[1].SourceColumn = "id_client";
                representantDataAdapter.InsertCommand.Parameters[2].SourceColumn = "name_representant";
                representantDataAdapter.InsertCommand.Parameters[3].SourceColumn = "surname_representant";

                representantDataAdapter.UpdateCommand = new NpgsqlCommand("update representant set (phoneNbr, name_representant, surname_representant) = (:pN, :nR, :sR) where id_representant = :idR", connection.DatabaseConnection);
                representantDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                representantDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("pN", NpgsqlDbType.Integer));
                representantDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nR", NpgsqlDbType.Text));
                representantDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("sR", NpgsqlDbType.Text));
                representantDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                representantDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                representantDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                representantDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                representantDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                representantDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "phoneNumber";
                representantDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name_representant";
                representantDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "surname_representant";
                representantDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "id_representant";

                representantDataAdapter.DeleteCommand = new NpgsqlCommand("delete from representant where id_representant = :idR", connection.DatabaseConnection);
                representantDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                representantDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                representantDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                representantDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_representant";

                representantDataAdapter.Fill(representantTable);

                //MatAndFourn table adapter

                matAndFournDataAdapter = new NpgsqlDataAdapter();

                matAndFournDataAdapter.SelectCommand = new NpgsqlCommand("select * from \"matAndFourn\"");
                matAndFournDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                matAndFournDataAdapter.InsertCommand = new NpgsqlCommand("insert into matAndFourn(id_materiaux, id_fournisseur, prix_HT, prix_TTC, id) values (:idM, :idR, :pH, :pT, :id)", connection.DatabaseConnection);
                matAndFournDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                matAndFournDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                matAndFournDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                matAndFournDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("pH", NpgsqlDbType.Real));
                matAndFournDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("pT", NpgsqlDbType.Real));
                matAndFournDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                matAndFournDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_materiaux";
                matAndFournDataAdapter.InsertCommand.Parameters[1].SourceColumn = "id_representant";
                matAndFournDataAdapter.InsertCommand.Parameters[2].SourceColumn = "prix_HT";
                matAndFournDataAdapter.InsertCommand.Parameters[3].SourceColumn = "prix_TTC";
                matAndFournDataAdapter.InsertCommand.Parameters[4].SourceColumn = "id";

                matAndFournDataAdapter.UpdateCommand = new NpgsqlCommand("update matAndFourn set (id_materiaux, id_fournisseur, prix_HT, prix_TTC) = (:idM, :idR, :pH, :pT) where id = :id", connection.DatabaseConnection);
                matAndFournDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                matAndFournDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                matAndFournDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idR", NpgsqlDbType.Integer));
                matAndFournDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("pH", NpgsqlDbType.Real));
                matAndFournDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("pT", NpgsqlDbType.Real));
                matAndFournDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                matAndFournDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_materiaux";
                matAndFournDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "id_representant";
                matAndFournDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "prix_HT";
                matAndFournDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "prix_TTC";
                matAndFournDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "id";

                matAndFournDataAdapter.DeleteCommand = new NpgsqlCommand("delete from matAndFourn where id = :id", connection.DatabaseConnection);
                matAndFournDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                matAndFournDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                matAndFournDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                matAndFournDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                matAndFournDataAdapter.Fill(matAndFournTable);

                //Client table adapter

                clientDataAdapter = new NpgsqlDataAdapter();

                clientDataAdapter.SelectCommand = new NpgsqlCommand("select * from client");
                clientDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                clientDataAdapter.InsertCommand = new NpgsqlCommand("insert into client(id_client, name_client, surname_client, addr_client, postal_client, city_client, relation_client, desc_client, \"phoneNumber\", \"mobileNumber\", mail) values (:idC, :nC, :snC, :aC, :pC, :cC, :rC, :dC, :phNC, :mNC, :mC)", connection.DatabaseConnection);
                clientDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("snC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("aC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("pC", NpgsqlDbType.Integer));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("cC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("rC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("dC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("phNC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("mNC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("mC", NpgsqlDbType.Text));
                clientDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[7].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[8].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[9].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[10].Direction = ParameterDirection.Input;
                clientDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id_client";
                clientDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name_client";
                clientDataAdapter.InsertCommand.Parameters[2].SourceColumn = "surname_client";
                clientDataAdapter.InsertCommand.Parameters[3].SourceColumn = "addr_client";
                clientDataAdapter.InsertCommand.Parameters[4].SourceColumn = "postal_client";
                clientDataAdapter.InsertCommand.Parameters[5].SourceColumn = "city_client";
                clientDataAdapter.InsertCommand.Parameters[6].SourceColumn = "relation_client";
                clientDataAdapter.InsertCommand.Parameters[7].SourceColumn = "desc_client";
                clientDataAdapter.InsertCommand.Parameters[8].SourceColumn = "phoneNumber";
                clientDataAdapter.InsertCommand.Parameters[9].SourceColumn = "mobileNumber";
                clientDataAdapter.InsertCommand.Parameters[10].SourceColumn = "mail";

                clientDataAdapter.UpdateCommand = new NpgsqlCommand("update client set (name_client, surname_client, addr_client, postal_client, city_client, relation_client, desc_client, \"phoneNumber\", \"mobileNumber\", mail) = (:nC, :snC, :aC, :pC, :cC, :rC, :dC, :phNC, :mNC, :mC) where id_client = :idC", connection.DatabaseConnection);
                clientDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("snC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("aC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("pC", NpgsqlDbType.Integer));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("cC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("rC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("dC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("phNC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("mNC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("mC", NpgsqlDbType.Text));
                clientDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[6].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[7].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[8].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[9].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[10].Direction = ParameterDirection.Input;
                clientDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id_client";
                clientDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name_client";
                clientDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "surname_client";
                clientDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "addr_client";
                clientDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "postal_client";
                clientDataAdapter.UpdateCommand.Parameters[5].SourceColumn = "city_client";
                clientDataAdapter.UpdateCommand.Parameters[6].SourceColumn = "relation_client";
                clientDataAdapter.UpdateCommand.Parameters[7].SourceColumn = "desc_client";
                clientDataAdapter.UpdateCommand.Parameters[8].SourceColumn = "phoneNumber";
                clientDataAdapter.UpdateCommand.Parameters[9].SourceColumn = "mobileNumber";
                clientDataAdapter.UpdateCommand.Parameters[10].SourceColumn = "mail";

                clientDataAdapter.DeleteCommand = new NpgsqlCommand("delete from client where id_client = :idC", connection.DatabaseConnection);
                clientDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                clientDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                clientDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                clientDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id_client";

                clientDataAdapter.Fill(clientTable);

                //Event table adapter

                eventDataAdapter = new NpgsqlDataAdapter();

                eventDataAdapter.SelectCommand = new NpgsqlCommand("select * from events", connection.DatabaseConnection);
                eventDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                eventDataAdapter.InsertCommand = new NpgsqlCommand("insert into events (id, title, \"desc\", begin, \"end\", proprietary, type, affected) values (:id, :t, :d, :b, :e, :p, :ty, :a)", connection.DatabaseConnection);
                eventDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("t", NpgsqlDbType.Text));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("d", NpgsqlDbType.Text));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("b", NpgsqlDbType.Text));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("e", NpgsqlDbType.Text));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("p", NpgsqlDbType.Integer));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("ty", NpgsqlDbType.Text));
                eventDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("a", NpgsqlDbType.Integer));
                eventDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[7].Direction = ParameterDirection.Input;
                eventDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id";
                eventDataAdapter.InsertCommand.Parameters[1].SourceColumn = "title";
                eventDataAdapter.InsertCommand.Parameters[2].SourceColumn = "desc";
                eventDataAdapter.InsertCommand.Parameters[3].SourceColumn = "begin";
                eventDataAdapter.InsertCommand.Parameters[4].SourceColumn = "end";
                eventDataAdapter.InsertCommand.Parameters[5].SourceColumn = "proprietary";
                eventDataAdapter.InsertCommand.Parameters[6].SourceColumn = "type";
                eventDataAdapter.InsertCommand.Parameters[7].SourceColumn = "affected";

                eventDataAdapter.UpdateCommand = new NpgsqlCommand("update events set (title, \"desc\", begin, \"end\", proprietary, type, affected) = (:t, :d, :b, :e, :p, :ty, :a) where id = :id", connection.DatabaseConnection);
                eventDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("t", NpgsqlDbType.Text));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("d", NpgsqlDbType.Text));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("b", NpgsqlDbType.Text));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("e", NpgsqlDbType.Text));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("p", NpgsqlDbType.Integer));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("ty", NpgsqlDbType.Text));
                eventDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("a", NpgsqlDbType.Integer));
                eventDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[6].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[7].Direction = ParameterDirection.Input;
                eventDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id";
                eventDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "title";
                eventDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "desc";
                eventDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "begin";
                eventDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "end";
                eventDataAdapter.UpdateCommand.Parameters[5].SourceColumn = "proprietary";
                eventDataAdapter.UpdateCommand.Parameters[6].SourceColumn = "type";
                eventDataAdapter.UpdateCommand.Parameters[7].SourceColumn = "affected";

                eventDataAdapter.DeleteCommand = new NpgsqlCommand("delete from events where id = :id", connection.DatabaseConnection);
                eventDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                eventDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
                eventDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                eventDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                eventDataAdapter.Fill(eventTable);

                //meetings table adapter

                meetingsDataAdapter = new NpgsqlDataAdapter();

                meetingsDataAdapter.SelectCommand = new NpgsqlCommand("select * from meetings");
                meetingsDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                meetingsDataAdapter.InsertCommand = new NpgsqlCommand("insert into meetings(id, client_id, chantier_id, event_id, desc, report, status) values (:idM, :idC, :idCh, :idE, :d, :r, :s)", connection.DatabaseConnection);
                meetingsDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));    
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idCh", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idE", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("d", NpgsqlDbType.Text));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("r", NpgsqlDbType.Text));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("s", NpgsqlDbType.Text));
                meetingsDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id";
                meetingsDataAdapter.InsertCommand.Parameters[1].SourceColumn = "client_id";
                meetingsDataAdapter.InsertCommand.Parameters[2].SourceColumn = "chantier_id";
                meetingsDataAdapter.InsertCommand.Parameters[3].SourceColumn = "event_id";
                meetingsDataAdapter.InsertCommand.Parameters[4].SourceColumn = "desc";
                meetingsDataAdapter.InsertCommand.Parameters[5].SourceColumn = "report";
                meetingsDataAdapter.InsertCommand.Parameters[6].SourceColumn = "status";

                meetingsDataAdapter.UpdateCommand = new NpgsqlCommand("update meetings set (client_id, chantier_id, event_id, desc, report, status) = (:idC, :idCh, :idE, :d, :r, :s) where id = :idM", connection.DatabaseConnection);
                meetingsDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idC", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idCh", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idE", NpgsqlDbType.Integer));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("d", NpgsqlDbType.Text));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("r", NpgsqlDbType.Text));
                meetingsDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("s", NpgsqlDbType.Text));
                meetingsDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
                meetingsDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id";
                meetingsDataAdapter.InsertCommand.Parameters[1].SourceColumn = "client_id";
                meetingsDataAdapter.InsertCommand.Parameters[2].SourceColumn = "chantier_id";
                meetingsDataAdapter.InsertCommand.Parameters[3].SourceColumn = "event_id";
                meetingsDataAdapter.InsertCommand.Parameters[4].SourceColumn = "desc";
                meetingsDataAdapter.InsertCommand.Parameters[5].SourceColumn = "report";
                meetingsDataAdapter.InsertCommand.Parameters[6].SourceColumn = "status";

                meetingsDataAdapter.DeleteCommand = new NpgsqlCommand("delete from meetings where id = :idM", connection.DatabaseConnection);
                meetingsDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                meetingsDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idM", NpgsqlDbType.Integer));
                meetingsDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                meetingsDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                clientDataAdapter.Fill(meetingsTable);

                //Client table adapter

                employeesDataAdapter = new NpgsqlDataAdapter();

                employeesDataAdapter.SelectCommand = new NpgsqlCommand("select * from employees");
                employeesDataAdapter.SelectCommand.Connection = connection.DatabaseConnection;

                employeesDataAdapter.InsertCommand = new NpgsqlCommand("insert into employees(id, name, surname, addr, postal, city, phoneNumber, mobileNumber, mail, remarks, id_server) values (:idE, :nE, :snE, :aE, :pE, :phNE, :mNE, :mE, :rE, :idSE)", connection.DatabaseConnection);
                employeesDataAdapter.InsertCommand.Connection = connection.DatabaseConnection;
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idE", NpgsqlDbType.Integer));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("nE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("snE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("aE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("pE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("cE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("phNE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("mNE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("mE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("rE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("idSE", NpgsqlDbType.Text));
                employeesDataAdapter.InsertCommand.Parameters[0].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[1].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[2].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[3].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[4].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[5].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[6].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[7].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[8].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[9].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[10].Direction = ParameterDirection.Input;
                employeesDataAdapter.InsertCommand.Parameters[0].SourceColumn = "id";
                employeesDataAdapter.InsertCommand.Parameters[1].SourceColumn = "name";
                employeesDataAdapter.InsertCommand.Parameters[2].SourceColumn = "surname";
                employeesDataAdapter.InsertCommand.Parameters[3].SourceColumn = "addr";
                employeesDataAdapter.InsertCommand.Parameters[4].SourceColumn = "postal";
                employeesDataAdapter.InsertCommand.Parameters[5].SourceColumn = "city";
                employeesDataAdapter.InsertCommand.Parameters[6].SourceColumn = "phoneNumber";
                employeesDataAdapter.InsertCommand.Parameters[7].SourceColumn = "mobileNumber";
                employeesDataAdapter.InsertCommand.Parameters[8].SourceColumn = "mail";
                employeesDataAdapter.InsertCommand.Parameters[9].SourceColumn = "remarks";
                employeesDataAdapter.InsertCommand.Parameters[10].SourceColumn = "id_server";

                employeesDataAdapter.UpdateCommand = new NpgsqlCommand("update employees set (name, surname, addr, postal, city, phoneNumber, mobileNumber, mail, remarks, id_server) = (:nE, :snE, :aE, :pE, :phNE, :mNE, :mE, :rE, :idSE) where id = :idE", connection.DatabaseConnection);
                employeesDataAdapter.UpdateCommand.Connection = connection.DatabaseConnection;
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idE", NpgsqlDbType.Integer));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("nE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("snE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("aE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("pE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("cE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("phNE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("mNE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("mE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("rE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters.Add(new NpgsqlParameter("idSE", NpgsqlDbType.Text));
                employeesDataAdapter.UpdateCommand.Parameters[0].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[1].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[2].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[3].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[4].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[5].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[6].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[7].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[8].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[9].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[10].Direction = ParameterDirection.Input;
                employeesDataAdapter.UpdateCommand.Parameters[0].SourceColumn = "id";
                employeesDataAdapter.UpdateCommand.Parameters[1].SourceColumn = "name";
                employeesDataAdapter.UpdateCommand.Parameters[2].SourceColumn = "surname";
                employeesDataAdapter.UpdateCommand.Parameters[3].SourceColumn = "addr";
                employeesDataAdapter.UpdateCommand.Parameters[4].SourceColumn = "postal";
                employeesDataAdapter.UpdateCommand.Parameters[5].SourceColumn = "city";
                employeesDataAdapter.UpdateCommand.Parameters[6].SourceColumn = "phoneNumber";
                employeesDataAdapter.UpdateCommand.Parameters[7].SourceColumn = "mobileNumber";
                employeesDataAdapter.UpdateCommand.Parameters[8].SourceColumn = "mail";
                employeesDataAdapter.UpdateCommand.Parameters[9].SourceColumn = "remarks";
                employeesDataAdapter.UpdateCommand.Parameters[10].SourceColumn = "id_server";

                employeesDataAdapter.DeleteCommand = new NpgsqlCommand("delete from employees where id = :idE", connection.DatabaseConnection);
                employeesDataAdapter.DeleteCommand.Connection = connection.DatabaseConnection;
                employeesDataAdapter.DeleteCommand.Parameters.Add(new NpgsqlParameter("idE", NpgsqlDbType.Integer));
                employeesDataAdapter.DeleteCommand.Parameters[0].Direction = ParameterDirection.Input;
                employeesDataAdapter.DeleteCommand.Parameters[0].SourceColumn = "id";

                employeesDataAdapter.Fill(employeesTable);

                DataColumn[] matDC = {null};
                DataColumn[] stockDC = { null };
                DataColumn[] unitDC = { null };
                DataColumn[] chantierDC = { null };
                DataColumn[] fournisseurDC = { null };
                DataColumn[] representantDC = { null };
                DataColumn[] matAndFournDC = { null };
                DataColumn[] clientDC = { null };
                DataColumn[] eventDC = { null };
                DataColumn[] meetingsDC = { null };
                DataColumn[] employeesDC = { null };

                matDC[0] = materiauxTable.Columns["id_materiaux"];
                stockDC[0] = stockTable.Columns["id_stock"];
                unitDC[0] = unitTable.Columns["id_unit"];
                chantierDC[0] = chantierTable.Columns["id_chantier"];
                fournisseurDC[0] = fournisseurTable.Columns["id_fournisseur"];
                representantDC[0] = representantTable.Columns["id_representant"];
                matAndFournDC[0] = matAndFournTable.Columns["id"];
                clientDC[0] = clientTable.Columns["id_client"];
                eventDC[0] = eventTable.Columns["id"];
                meetingsDC[0] = meetingsTable.Columns["id"];
                employeesDC[0] = employeesTable.Columns["id"];

                materiauxTable.PrimaryKey = matDC;
                stockTable.PrimaryKey = stockDC;
                unitTable.PrimaryKey = unitDC;
                chantierTable.PrimaryKey = chantierDC;
                fournisseurTable.PrimaryKey = fournisseurDC;
                representantTable.PrimaryKey = representantDC;
                matAndFournTable.PrimaryKey = matAndFournDC;
                clientTable.PrimaryKey = clientDC;
                eventTable.PrimaryKey = eventDC;
                meetingsTable.PrimaryKey = meetingsDC;
                employeesTable.PrimaryKey = employeesDC;

                ds.Tables.Add(materiauxTable);
                ds.Tables.Add(stockTable);
                ds.Tables.Add(unitTable);
                ds.Tables.Add(chantierTable);
                ds.Tables.Add(fournisseurTable);
                ds.Tables.Add(representantTable);
                ds.Tables.Add(matAndFournTable);
                ds.Tables.Add(clientTable);
                ds.Tables.Add(eventTable);
                ds.Tables.Add(meetingsTable);
                ds.Tables.Add(employeesTable);
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

        public bool updateDatabase(string[] table)
        {
            foreach (string s in table)
            {
                switch (s)
                {
                    case "materiaux":
                        if (ds.Tables["materiaux"].GetChanges() != null)
                        {
                            materiauxDataAdapter.Update(ds.Tables["materiaux"].GetChanges());
                        }
                        break;
                    case "stock":
                        if (ds.Tables["stock"].GetChanges() != null)
                        {
                            stockDataAdapter.Update(ds.Tables["stock"].GetChanges());
                        }
                        break;
                    case "unit":
                        if (ds.Tables["unit"].GetChanges() != null)
                        {
                            unitDataAdapter.Update(ds.Tables["unit"].GetChanges());
                        }
                        break;
                    case "chantier":
                        if (ds.Tables["chantier"].GetChanges() != null)
                        {
                            chantierDataAdapter.Update(ds.Tables["chantier"].GetChanges());
                        }
                        break;
                    case "fournisseur":
                        if (ds.Tables["fournisseur"].GetChanges() != null)
                        {
                            fournisseurDataAdapter.Update(ds.Tables["fournisseur"].GetChanges());
                        }
                        break;
                    case "representant":
                        if (ds.Tables["representant"].GetChanges() != null)
                        {
                            representantDataAdapter.Update(ds.Tables["representant"].GetChanges());
                        }
                        break;
                    case "matAndFourn":
                        if (ds.Tables["mat-fourn-corres"].GetChanges() != null)
                        {
                            matAndFournDataAdapter.Update(ds.Tables["mat-fourn-corres"].GetChanges());
                        }
                        break;
                    case "client":
                        if (ds.Tables["client"].GetChanges() != null)
                        {
                            clientDataAdapter.Update(ds.Tables["client"].GetChanges());
                        }
                        break;
                    case "event":
                        if (ds.Tables["event"].GetChanges() != null)
                        {
                            eventDataAdapter.Update(ds.Tables["event"].GetChanges());
                        }
                        break;
                    case "meetings":
                        if (ds.Tables["meetings"].GetChanges() != null)
                        {
                            meetingsDataAdapter.Update(ds.Tables["meetings"].GetChanges());
                        }
                        break;
                    case "employees":
                        if (ds.Tables["employees"].GetChanges() != null)
                        {
                            employeesDataAdapter.Update(ds.Tables["employees"].GetChanges());
                        }
                        break;
                }
            }

            DatabaseUpdatedEvent();

            return true;

        }

        public bool updateDataset(string[] table)
        {
            foreach (string s in table)
            {
                switch (s)
                {
                    case "materiaux":
                        ds.Tables["materiaux"].Clear();
                        materiauxDataAdapter.Fill(ds.Tables["materiaux"]);
                        break;
                    case "stock":
                        ds.Tables["stock"].Clear();
                        stockDataAdapter.Fill(ds.Tables["stock"]);
                        break;
                    case "unit":
                        ds.Tables["unit"].Clear();
                        unitDataAdapter.Fill(ds.Tables["unit"]);
                        break;
                    case "chantier":
                        ds.Tables["chantier"].Clear();
                        chantierDataAdapter.Fill(ds.Tables["chantier"]);
                        break;
                    case "fournisseur":
                        ds.Tables["fournisseur"].Clear();
                        fournisseurDataAdapter.Fill(ds.Tables["fournisseur"]);
                        break;
                    case "representant":
                        ds.Tables["representant"].Clear();
                        representantDataAdapter.Fill(ds.Tables["representant"]);
                        break;
                    case "matAndFourn":
                        ds.Tables["mat-fourn-corres"].Clear();
                        matAndFournDataAdapter.Fill(ds.Tables["mat-fourn-corres"]);
                        break;
                    case "client":
                        ds.Tables["client"].Clear();
                        clientDataAdapter.Fill(ds.Tables["client"]);
                        break;
                    case "event":
                        ds.Tables["event"].Clear();
                        eventDataAdapter.Fill(ds.Tables["event"]);
                        break;
                    case "meetings":
                        ds.Tables["meetings"].Clear();
                        meetingsDataAdapter.Fill(ds.Tables["meetings"]);
                        break;
                    case "employees":
                        ds.Tables["employees"].Clear();
                        employeesDataAdapter.Fill(ds.Tables["employees"]);
                        break;
                }
            }

            if (DatasetUpdatedEvent != null)
            {
                DatasetUpdatedEvent();
            }

            return true;

        }

        public void rowUpdatedEvent(object sender, NpgsqlRowUpdatedEventArgs e)
        {
            CGestLogger.log(e.StatementType.ToString(), false);
        }

    }
}