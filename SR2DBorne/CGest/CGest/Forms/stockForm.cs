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
    public partial class stockForm : Form
    {
        MainForm baseForm;
        CGestDatabase db;
        CGestNetworkClientControl networkControler;
        CGestClassBound cB;

        List<int> chantierIdsList = new List<int>();

        public delegate void fillDataGridViewCallback(string searchValue);

        public stockForm(CGestClassBound cB , MainForm baseForm)
        {
            InitializeComponent();

            this.networkControler = cB.networkControler;

            this.db = cB.db;
            this.baseForm = baseForm;
            this.cB = cB;

            this.Height = baseForm.Height - 150;
            this.Width = baseForm.Width - 200;

            this.CenterToParent();

            initChantierBox();

            db.Ds.DatasetUpdatedEvent += new CGestDataset.EventHandler(update);

            fillGridView(searchTextbox.Text);
        }

        public void update()
        {
            if (dataGridView1.InvokeRequired)
            {
                fillDataGridViewCallback c = new fillDataGridViewCallback(fillGridView);
                Invoke(c, new object[] { "" });
            }
            else
            {
                dataGridView1.Rows.Clear();

                int chantierID = chantierIdsList[clientComboBox.SelectedIndex];

                foreach (DataRow dr in db.Ds.Dataset.Tables["materiaux"].Rows)
                {
                    if (dr.ItemArray[1].ToString() == chantierID.ToString())
                    {
                        addRowToStockGridView(dr, "");
                    }
                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView(string searchValue)
        {
            dataGridView1.Rows.Clear();

            int chantierID = chantierIdsList[clientComboBox.SelectedIndex];

            foreach (DataRow dr in db.Ds.Dataset.Tables["stock"].Rows)
            {
                if (dr.ItemArray[1].ToString() == chantierID.ToString())
                {
                    addRowToStockGridView(dr, searchValue);
                }
            }

            dataGridView1.ClearSelection();
        }

        public void addRowToStockGridView(DataRow dr, string searchValue)
        {
            string[] gridViewRow = { "", "", "", "", "" };

            DataRow matRow = TablesUtilities.getMatFromId(dr.ItemArray[2].ToString(), db.Ds);
            DataRow unitRow = TablesUtilities.getUnitFromId(matRow.ItemArray[2].ToString(), db.Ds);

            if(matRow == null)
            {
                return;
            }

            if (unitRow == null)
            {
                return;
            }

            string stockID = dr.ItemArray[0].ToString().TrimEnd();
            string matName = matRow.ItemArray[1].ToString().TrimEnd();
            string matDesc = matRow.ItemArray[3].ToString().TrimEnd();
            string unitName = unitRow.ItemArray[1].ToString().TrimEnd();
            string matQuantity = dr.ItemArray[3].ToString().TrimEnd();

            gridViewRow[0] = stockID;
            gridViewRow[1] = matName;
            gridViewRow[2] = unitName;
            gridViewRow[3] = matDesc;
            gridViewRow[4] = matQuantity;

            dataGridView1.Rows.Add(gridViewRow);
        }

        public void initChantierBox()
        {

            DataRowCollection chantierCollection = TablesUtilities.getRowsToArray("chantier", db.Ds, out chantierIdsList);

            foreach (DataRow dr in chantierCollection)
            {
                clientComboBox.Items.Add(dr.ItemArray[2]);
            }

            if (clientComboBox.Items.Count > 0)
            {
                clientComboBox.SelectedItem = clientComboBox.Items[0];
            }

        }

        private void stockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new CGestDataset.EventHandler(update);

            baseForm.setButtonEnabling(baseForm.stockButton, true);
        }

        private void stockForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            fillGridView(searchTextbox.Text);
        }

        private void clientComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillGridView(searchTextbox.Text);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                rowActionPanel.Enabled = true;
            }
            else
            {
                rowActionPanel.Enabled = false;
            }
        }

        private void deleteStockButton_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            DialogResult deleteBox = MessageBox.Show("Etes vous sûre de vouloir supprimer l'entrée de stock sélectionnée ?", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (deleteBox == DialogResult.Yes)
            {
                string[] table = { "stock" };

                TablesUtilities.removeStockByID(id, db.Ds);

                db.Ds.updateDatabase(table);
            }
        }

        private void modifyStockButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            stockModifyForm modifyForm = new stockModifyForm(dataGridView1.SelectedRows[0].Cells[0].ToString(), cB, this);

            modifyForm.Show();
        }

        private void addStockButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            stockAddForm stockForm = new stockAddForm(cB, this);

            stockForm.Show();
        }

    }
}
