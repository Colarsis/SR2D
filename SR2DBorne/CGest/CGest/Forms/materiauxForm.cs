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
    public partial class materiauxForm : Form
    {

        MainForm mF;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public delegate void fillGridViewCallback(string searchValue);

        public materiauxForm(MainForm mF, CGestClassBound cB)
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

                DataRowCollection matRows = db.Ds.Dataset.Tables["materiaux"].Rows;

                foreach (DataRow dr in matRows)
                {
                    if (dr.ItemArray[1].ToString().Contains(""))
                    {
                        addMateriauxToGrid(dr);
                    }
                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView(string searchText)
        {
            dataGridView1.Rows.Clear();

            DataRowCollection clientRows = db.Ds.Dataset.Tables["materiaux"].Rows;

            foreach (DataRow dr in clientRows)
            {
                if (searchText != "")
                {
                    if (dr.ItemArray[1].ToString().Contains(searchText))
                    {
                        addMateriauxToGrid(dr);
                    }
                }
                else
                {
                    addMateriauxToGrid(dr);
                }

            }

            dataGridView1.ClearSelection();
        }

        public void addMateriauxToGrid(DataRow dr)
        {
            string[] textRow = {"", "", "", "", ""};

            string id = dr.ItemArray[0].ToString();
            string name = dr.ItemArray[1].ToString();
            string unit = dr.ItemArray[2].ToString();
            string desc = dr.ItemArray[3].ToString();

            textRow[0] = id;
            textRow[1] = name;
            textRow[2] = unit;
            textRow[3] = TablesUtilities.getUnitFromId(unit, db.Ds).ItemArray[1].ToString();
            textRow[4] = desc;
             
            dataGridView1.Rows.Add(textRow);
        }

        private void clientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new CGestDataset.EventHandler(update);

            mF.setButtonEnabling(mF.materiauxBtn, true);
        }

        private void clientForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            fillGridView(searchTextbox.Text);
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            materiauxAddForm addForm = new materiauxAddForm(cB, this);

            addForm.Show();
        }

        private void modifyClientButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            materiauxModifyForm modifyForm = new materiauxModifyForm(cB, this, db.Ds.Dataset.Tables["materiaux"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            modifyForm.Show();
        }

        private void deleteClientButton_Click(object sender, EventArgs e)
        {
            string[] materiaux = { "materiaux" };
            string[] stock = { "stock" };

            DataRowCollection stockCollection = TablesUtilities.getStockFromMateriaux(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), db.Ds);

            if (stockCollection.Count > 0)
            {
                DialogResult deleteChildWarningBox = MessageBox.Show("Etes-vous sûre de vouloir supprimer le materiau sélectionné ? La suppression de ce materiau entraînera la suppresion des entrées de stock associés !", "Confirmation de la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (deleteChildWarningBox != DialogResult.Yes)
                {
                    return;
                }
            }
            else
            {
                DialogResult deleteAskBox = MessageBox.Show("Etes-vous sûre de vouloir supprimer le materiau sélectionné ?", "Confirmation de la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (deleteAskBox != DialogResult.Yes)
                {
                    return;
                }
            }

            foreach (DataRow dr in stockCollection)
            {
                TablesUtilities.removeStockByID(dr.ItemArray[0].ToString(), db.Ds);
            }

            if (db.Ds.Dataset.Tables["stock"].GetChanges() != null)
            {
                db.Ds.updateDatabase(stock);
            }

            TablesUtilities.removeMateriauxByID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), db.Ds);

            db.Ds.updateDatabase(materiaux);
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
    }
}

