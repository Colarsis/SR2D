using CGest.Database;
using CGest.Utilities;
using CGest.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGest.Forms
{
    public partial class clientForm : Form
    {

        MainForm mF;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public delegate void fillGridViewCallback(string searchValue);

        public clientForm(MainForm mF, CGestClassBound cB)
        {
            InitializeComponent();

            this.mF = mF;

            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            fillGridView("");

            db.Ds.DatasetUpdatedEvent += new CGestDataset.EventHandler(update);
        }

        public void update()
        {
            if (dataGridView1.InvokeRequired)
            {
                fillGridViewCallback c = new fillGridViewCallback(fillGridView);
                Invoke(c, new object[] { "" });
            }
            else
            {
                dataGridView1.Rows.Clear();

                DataRowCollection clientRows = db.Ds.Dataset.Tables["client"].Rows;

                foreach (DataRow dr in clientRows)
                {
                    if (dr.ItemArray[1].ToString().Contains(""))
                    {
                        addClientToGrid(dr);
                    }
                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView(string searchText)
        {
            dataGridView1.Rows.Clear();

            DataRowCollection clientRows = db.Ds.Dataset.Tables["client"].Rows;

            foreach (DataRow dr in clientRows)
            {
                if (searchText != "")
                {
                    if (dr.ItemArray[1].ToString().ToLower().Contains(searchText))
                    {
                        addClientToGrid(dr);
                    }
                }
                else
                {
                    addClientToGrid(dr);
                }

            }

            dataGridView1.ClearSelection();
        }

        public void addClientToGrid(DataRow dr)
        {
            string[] textRow = {"", "", "", ""};

            string id = dr.ItemArray[0].ToString();
            string name = dr.ItemArray[1].ToString();
            string surname = dr.ItemArray[2].ToString();
            string addr = TablesUtilities.arrayToAddress(new string[] { dr.ItemArray[3].ToString(), dr.ItemArray[4].ToString(), dr.ItemArray[5].ToString() });

            textRow[0] = id;
            textRow[1] = name;
            textRow[2] = surname;
            textRow[3] = addr;

            dataGridView1.Rows.Add(textRow);
        }

        private void clientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new CGestDataset.EventHandler(update);

            mF.setButtonEnabling(mF.clientsBtn, true);
        }

        private void clientForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            fillGridView(searchTextbox.Text.ToString().ToLower());
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            clientAddForm addForm = new clientAddForm(cB, this);

            addForm.Show();
        }

        private void modifyClientButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            clientModifyForm modifyForm = new clientModifyForm(cB, this, db.Ds.Dataset.Tables["client"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            modifyForm.Show();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            clientInfosForm modifyForm = new clientInfosForm(cB, this, db.Ds.Dataset.Tables["client"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            modifyForm.Show();
        }

        private void deleteClientButton_Click(object sender, EventArgs e)
        {
            string[] meetings = { "meetings" };
            string[] chantier = { "chantier" };
            string[] stock = { "stock" };
            string[] client = { "client" };



            DataRowCollection meetingsCollection = TablesUtilities.getMeetingsFromClient(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), db.Ds);

            DataRowCollection chantierCollection = TablesUtilities.getChantierFromClient(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), db.Ds);

            DataTable drT = db.Ds.Dataset.Tables["stock"].Clone();

            foreach (DataRow chantierRow in chantierCollection)
            {
                DataRowCollection tempStockConnection = TablesUtilities.getStockFromChantier(chantierRow, db.Ds);

                foreach (DataRow stockRow in tempStockConnection)
                {
                    drT.Rows.Add(stockRow.ItemArray);
                }
            }

            if (chantierCollection.Count > 0 || meetingsCollection.Count > 0)
            {
                DialogResult deleteChildWarningBox = MessageBox.Show("Etes-vous sûre de vouloir supprimer le client sélectionné ? La suppression de ce client entraînera la suppresion des chantier, entrées de stock et rendez-vous associés !", "Confirmation de la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (deleteChildWarningBox != DialogResult.Yes)
                {
                    return;
                }
            }
            else
            {
                DialogResult deleteAskBox = MessageBox.Show("Etes-vous sûre de vouloir supprimer le client sélectionné ?", "Confirmation de la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (deleteAskBox != DialogResult.Yes)
                {
                    return;
                }
            }

            foreach (DataRow dr in meetingsCollection)
            {
                TablesUtilities.removeMeetingsByID(dr.ItemArray[0].ToString(), db.Ds);
            }

            if (meetingsCollection.Count > 0)
            {
                db.Ds.updateDatabase(meetings);
            }

            foreach (DataRow dr in drT.Rows)
            {
                TablesUtilities.removeStockByID(dr.ItemArray[0].ToString(), db.Ds);
            }

            if (drT.Rows.Count > 0)
            {
                db.Ds.updateDatabase(stock);
            }

            foreach (DataRow dr in chantierCollection)
            {
                TablesUtilities.removeChantierByID(dr.ItemArray[0].ToString(), db.Ds);
            }

            if (chantierCollection.Count > 0)
            {
                db.Ds.updateDatabase(chantier);
            }

            TablesUtilities.removeClientByID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), db.Ds);

            db.Ds.updateDatabase(client);

            rowActionPanel.Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                rowActionPanel.Enabled = false;
            }
            else
            {
                rowActionPanel.Enabled = true;
            }
        }

        private void clientForm_Load(object sender, EventArgs e)
        {

        }
    }
}

