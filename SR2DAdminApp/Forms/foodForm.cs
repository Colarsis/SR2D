using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using SR2DAdminApp.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColarsisUserControls.AdvancedTreeView;

namespace SR2DAdminApp.Forms
{
    public partial class foodForm : Form
    {
        MainForm baseForm;
        SR2DDatabase db;
        SR2DNetworkClientControl networkControler;
        SR2DClassBound cB;

        int activeSearchCrit = 0;
        string searchText = "";

        public delegate void setServiceModeCallback();
        public delegate void fillDataGridViewCallback();

        public foodForm(MainForm baseForm, SR2DClassBound cB)
        {
            InitializeComponent();

            this.networkControler = cB.networkControler;

            this.db = cB.db;
            this.baseForm = baseForm;
            this.cB = cB;

            this.Height = baseForm.Height - 150;
            this.Width = baseForm.Width - 200;

            this.CenterToParent();

            db.Ds.DatasetUpdatedEvent += new SR2DDataset.EventHandler(update);

            setServiceMode();

            fillGridView();
        }

        public void setServiceMode()
        {
            if((string)TablesUtilities.getVarFromId("1", db.Ds).ItemArray[2] == "0")
            {
                messageLabel.Visible = false;
                rowActionPanel.Enabled = true;
            }
            else
            {
                messageLabel.Visible = true;
                rowActionPanel.Enabled = false;
            }
        }
        
        public void update()
        {
            if (dataGridView1.InvokeRequired)
            {
                fillDataGridViewCallback c = new fillDataGridViewCallback(fillGridView);
                Invoke(c);

                if (messageLabel.InvokeRequired || rowActionPanel.InvokeRequired)
                {
                    setServiceModeCallback c2 = new setServiceModeCallback(setServiceMode);
                    Invoke(c2);
                }
                else
                {
                    setServiceMode();
                }          
            }
            else
            {

                if (messageLabel.InvokeRequired || rowActionPanel.InvokeRequired)
                {
                    setServiceModeCallback c2 = new setServiceModeCallback(setServiceMode);
                    Invoke(c2);
                }
                else
                {
                    setServiceMode();
                }   

                dataGridView1.Rows.Clear();

                DataRowCollection foodRows = db.Ds.Dataset.Tables["food"].Rows;

                foreach (DataRow dr in foodRows)
                {
                    if (searchText != "")
                    {
                        switch (activeSearchCrit)
                        {
                            case 0:
                                if (dr.ItemArray[1].ToString().ToLower().Contains(searchText))
                                {
                                    addFoodToGrid(dr);
                                }
                                break;
                            case 1:
                                if (db.Ds.Dataset.Tables["food_types"].Rows.Find(dr.ItemArray[3]).ItemArray[1].ToString().ToLower().Contains(searchText))
                                {
                                    addFoodToGrid(dr);
                                }
                                break;
                        }

                    }
                    else
                    {
                        addFoodToGrid(dr);
                    }

                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView()
        {
            dataGridView1.Rows.Clear();

            DataRowCollection foodRows = db.Ds.Dataset.Tables["food"].Rows;

            foreach (DataRow dr in foodRows)
            {
                if (searchText != "")
                {
                    switch (activeSearchCrit)
                    {
                        case 0:
                            if (dr.ItemArray[1].ToString().ToLower().Contains(searchText))
                            {
                                addFoodToGrid(dr);
                            }
                            break;
                        case 1:
                            if (db.Ds.Dataset.Tables["food_types"].Rows.Find(dr.ItemArray[3]).ItemArray[1].ToString().ToLower().Contains(searchText))
                            {
                                addFoodToGrid(dr);
                            }
                            break;
                    }

                }
                else
                {
                    addFoodToGrid(dr);
                }

            }

            dataGridView1.ClearSelection();
        }

        public void addFoodToGrid(DataRow dr)
        {
            string[] gridViewRow = { "", "", ""};

            DataRow foodTypeRow = TablesUtilities.getFoodTypeFromId(dr.ItemArray[3].ToString(), db.Ds);

            if(foodTypeRow == null)
            {
                return;
            }

            string foodID = dr.ItemArray[0].ToString().TrimEnd();
            string foodName = dr.ItemArray[1].ToString().TrimEnd();
            string foodType = foodTypeRow.ItemArray[1].ToString().TrimEnd();

            gridViewRow[0] = foodID;
            gridViewRow[1] = foodName;
            gridViewRow[2] = foodType;

            dataGridView1.Rows.Add(gridViewRow);
        }

        public void setItemEnabling(ToolStripMenuItem b, bool s)
        {
            if (s)
            {
                b.Enabled = true;
            }
            else
            {
                b.Enabled = false;
            }
        }

        private void stockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new SR2DDataset.EventHandler(update);

            baseForm.setButtonEnabling(baseForm.foodButton, true);
        }

        private void stockForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 462 + (this.Height - 539);

            groupBox1.Height = 462 + (this.Height - 539);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                deleteFoodButton.Enabled = true;
                modifyFoodButton.Enabled = true;
            }
            else
            {
                deleteFoodButton.Enabled = false;
                modifyFoodButton.Enabled = false;
            }
        }

        private void deleteStockButton_Click(object sender, EventArgs e)
        {
            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            DialogResult deleteBox = MessageBox.Show("Etes vous sûre de vouloir supprimer le produit sélectionnée ? Cela peut entrainer des pertes de données !", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (deleteBox == DialogResult.Yes)
            {
                string[] table = { "food" };

                TablesUtilities.removeFoodByID(id, db.Ds);

                db.Ds.updateDatabase(table);
            }
        }

        private void modifyStockButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            foodModifyForm modifyForm = new foodModifyForm(this, cB, db.Ds.Dataset.Tables["food"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            modifyForm.Show();
        }

        private void addStockButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            foodAddForm stockForm = new foodAddForm(this, cB);

            stockForm.Show();
        }

        private void searchCritCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (searchCritCombo.SelectedIndex)
            {
                case 0:
                    activeSearchCrit = 0;
                    break;
                case 1:
                    activeSearchCrit = 1;
                    break;
                default:
                    activeSearchCrit = 0;
                    break;
            }
        }

        private void searchTextbox_TextChanged(object sender, EventArgs e)
        {
            searchText = searchTextbox.Text.ToLower();

            fillGridView();
        }

        private void typesDeProduitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typesDeProduitsToolStripMenuItem.Enabled = false;

            foodTypeForm fTF = new foodTypeForm(this, cB);

            fTF.Show();
        }

    }
}
