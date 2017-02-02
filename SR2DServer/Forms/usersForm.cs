using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SR2DServer.Database;

namespace SR2DServer.Forms
{
    public partial class usersForm : Form
    {

        public mainForm mF;
        SR2DDatabase db;

        public delegate void fillGridViewCallback(string searchValue);

        public usersForm(mainForm mF, SR2DDatabase db)
        {
            InitializeComponent();

            this.mF = mF;
            this.db = db;

            db.Ds.DatabaseUpdatedEvent += new SR2DDataset.EventHandler(update);

            fillGridView("");
        }

        public void update() //Update the grid view when database event is raised 
        {
            if (dataGridView1.InvokeRequired)
            {
                fillGridViewCallback c = new fillGridViewCallback(fillGridView);
                Invoke(c, new object[] { "" });
            }
            else
            {
                dataGridView1.Rows.Clear();

                DataRowCollection clientRows = db.Ds.Dataset.Tables["user"].Rows;

                foreach (DataRow dr in clientRows)
                {
                    if (dr.ItemArray[1].ToString().Contains(""))
                    {
                        addUsersToGrid(dr);
                    }
                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView(string searchText) //Fill the gridView
        {
            dataGridView1.Rows.Clear();

            DataRowCollection clientRows = db.Ds.Dataset.Tables["user"].Rows;

            foreach (DataRow dr in clientRows)
            {
                if (searchText != "")
                {
                    if (dr.ItemArray[1].ToString().Contains(searchText))
                    {
                        addUsersToGrid(dr);
                    }
                }
                else
                {
                    addUsersToGrid(dr);
                }

            }

            dataGridView1.ClearSelection();
        }

        public void addUsersToGrid(DataRow dr) //Add users to the gridView
        {
            string[] textRow = new string[4];

            string id = dr.ItemArray[0].ToString();
            string username = dr.ItemArray[1].ToString();
            string priority = db.Ds.Dataset.Tables["ranks"].Rows.Find(int.Parse(dr.ItemArray[3].ToString()))[2].ToString();

            textRow[0] = id;
            textRow[1] = username;
            textRow[2] = priority;
             
            dataGridView1.Rows.Add(textRow);
        }

        private void userForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mF.setButtonEnabling(mF.seeUserButton, true);

            db.Ds.DatabaseUpdatedEvent -= new SR2DDataset.EventHandler(update);
        }

        private void userForm_Resize(object sender, EventArgs e) //When form is resized
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            fillGridView(searchTextbox.Text);
        }

        private void addUserButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            userAddForm addForm = new userAddForm(this, db);

            addForm.Show();
        }

        private void modifyUserButton_Click(object sender, EventArgs e)
        {
            //this.Enabled = false;

            //chantierModifyForm modifyForm = new chantierModifyForm(cB, this, db.Ds.Dataset.Tables["chantier"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            //modifyForm.Show();
        }

        private void deleteUserButton_Click(object sender, EventArgs e) // Delete the selected user after confirmation
        {
            string[] user = { "user" };

            DialogResult deleteAskBox = MessageBox.Show("Etes-vous sûre de vouloir supprimer l'utilisateur sélectionné ?", "Confirmation de la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (deleteAskBox != DialogResult.Yes)
            {
                return;
            }

            db.Ds.Dataset.Tables["user"].Rows.Find(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())).Delete();

            db.Ds.updateDatabase();
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
