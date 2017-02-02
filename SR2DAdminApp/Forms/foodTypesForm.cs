using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SR2DAdminApp.Forms
{
    public partial class foodTypeForm : Form
    {

        SR2DClassBound cB;
        foodForm fF;
        SR2DDatabase db;

        public delegate void setServiceModeCallback();
        public delegate void fillDataGridViewCallback();

        public foodTypeForm(foodForm fF, SR2DClassBound cB)
        {
            InitializeComponent();

            this.fF = fF;
            this.cB = cB;
            this.db = cB.db;

            db.Ds.DatasetUpdatedEvent += new SR2DDataset.EventHandler(update);

            setServiceMode();

            fillGridView();
        }

        public void setServiceMode()
        {
            if ((string)TablesUtilities.getVarFromId("1", db.Ds).ItemArray[2] == "0")
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
            if (dataGridView2.InvokeRequired)
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

                dataGridView2.Rows.Clear();

                DataRowCollection badgesRows = db.Ds.Dataset.Tables["food_types"].Rows;

                foreach (DataRow dr in badgesRows)
                {
                    addFoodTypeToGrid(dr);
                }

                dataGridView2.ClearSelection();
            }
        }

        public void fillGridView()
        {
            dataGridView2.Rows.Clear();

            DataRowCollection badgesRows = db.Ds.Dataset.Tables["food_types"].Rows;

            foreach (DataRow dr in badgesRows)
            {
                addFoodTypeToGrid(dr);
            }

            dataGridView2.ClearSelection();
        }

        public void addFoodTypeToGrid(DataRow dr)
        {
            string[] gridViewRow = { "", ""};

            string foodTypeID = dr.ItemArray[0].ToString().TrimEnd();
            string foodTypeName = dr.ItemArray[1].ToString().TrimEnd();

            gridViewRow[0] = foodTypeID;
            gridViewRow[1] = foodTypeName;

            dataGridView2.Rows.Add(gridViewRow);
        }

        private void foodTypeForm_Load(object sender, EventArgs e)
        {

        }

        private void addTypeButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            foodTypeAddForm fTAdd = new foodTypeAddForm(this, cB);

            fTAdd.Show();
        }

        private void modifyTypeButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            foodTypeModifyForm fTModif = new foodTypeModifyForm(this, cB, db.Ds.Dataset.Tables["food_types"].Rows.Find(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));

            fTModif.Show();
        }

        private void infoTypeButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            foodTypeInfoForm fTInfo = new foodTypeInfoForm(this, cB, db.Ds.Dataset.Tables["food_types"].Rows.Find(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));

            fTInfo.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void foodTypeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new SR2DDataset.EventHandler(update);
            
            fF.setItemEnabling(fF.typesDeProduitsToolStripMenuItem, true); 
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count != 0)
            {
                panel1.Enabled = true;
            }
            else
            {
                panel1.Enabled = false;
            }
        }

        private void deleteTypeButton_Click(object sender, EventArgs e)
        {
            string id = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();

            if(TablesUtilities.getFoodTypeFromFood(TablesUtilities.getFoodTypeFromId(id, db.Ds), db.Ds).Count != 0)
            {
                DialogResult stopBox = MessageBox.Show("Des produits sont de ce type. Veuillez supprimer ces produits avant de supprimer ce type !", "Opération impossible", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DialogResult deleteBox = MessageBox.Show("Etes vous sûre de vouloir supprimer le type sélectionnée ? Cela peut entrainer des pertes de données !", "Confirmation de suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (deleteBox == DialogResult.Yes)
            {
                string[] table = { "foodTypes" };

                TablesUtilities.removeFoodTypeByID(id, db.Ds);

                db.Ds.updateDatabase(table);
            }
        }
    }
}
