using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using SR2DAdminApp.Network;
using SR2DAdminApp.Forms;
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
    public partial class userForm : Form
    {

        MainForm mF;

        SR2DClassBound cB;
        SR2DDatabase db;
        SR2DNetworkClientControl networkControl;

        int activeSearchCrit = 0;
        string searchText = "";

        public delegate void fillGridViewCallback();

        public userForm(MainForm mF, SR2DClassBound cB)
        {
            InitializeComponent();

            this.mF = mF;

            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            fillGridView();

            db.Ds.DatasetUpdatedEvent += new SR2DDataset.EventHandler(update);
        }

        public void update()
        {
            if (dataGridView1.InvokeRequired)
            {
                fillGridViewCallback c = new fillGridViewCallback(fillGridView);
                Invoke(c);
            }
            else
            {
                dataGridView1.Rows.Clear();

                DataRowCollection badgesRows = db.Ds.Dataset.Tables["badges"].Rows;

                foreach (DataRow dr in badgesRows)
                {
                    if (searchText != "")
                    {
                        switch (activeSearchCrit)
                        {
                            case 0:
                                if (dr.ItemArray[1].ToString().ToLower().Contains(searchText))
                                {
                                    addStudentToGrid(dr);
                                }
                                break;
                            case 1:
                                if (dr.ItemArray[2].ToString().ToLower().Contains(searchText))
                                {
                                    addStudentToGrid(dr);
                                }
                                break;
                            case 2:
                                if (dr.ItemArray[4].ToString().ToLower().Contains(searchText))
                                {
                                    addStudentToGrid(dr);
                                }
                                break;
                        }

                    }
                    else
                    {
                        addStudentToGrid(dr);
                    }

                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView()
        {
            dataGridView1.Rows.Clear();

            DataRowCollection badgesRows = db.Ds.Dataset.Tables["badges"].Rows;

            foreach (DataRow dr in badgesRows)
            {
                if (searchText != "")
                {
                    switch(activeSearchCrit)
                    {
                        case 0:
                            if (dr.ItemArray[1].ToString().ToLower().Contains(searchText))
                            {
                                addStudentToGrid(dr);
                            }
                            break;
                        case 1:
                            if (dr.ItemArray[2].ToString().ToLower().Contains(searchText))
                            {
                                addStudentToGrid(dr);
                            }
                            break;
                        case 2:
                            if (dr.ItemArray[4].ToString().ToLower().Contains(searchText))
                            {
                                addStudentToGrid(dr);
                            }
                            break;
                    }

                }
                else
                {
                    addStudentToGrid(dr);
                }

            }

            dataGridView1.ClearSelection();
        }

        public void addStudentToGrid(DataRow dr)
        {
            string[] textRow = {"", "", "", ""};

            string id = dr.ItemArray[0].ToString();
            string name = dr.ItemArray[1].ToString();
            string surname = dr.ItemArray[2].ToString();
            string class_code = dr.ItemArray[4].ToString();

            textRow[0] = id;
            textRow[1] = name;
            textRow[2] = surname;
            textRow[3] = class_code;

            dataGridView1.Rows.Add(textRow);
        }

        private void clientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new SR2DDataset.EventHandler(update);

            mF.setButtonEnabling(mF.userButton, true);
        }

        private void clientForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void modifyClientButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            userModifyForm modifyForm = new userModifyForm(cB, this, db.Ds.Dataset.Tables["badges"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            modifyForm.Show();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            userInfosForm modifyForm = new userInfosForm(cB, this, db.Ds.Dataset.Tables["badges"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            modifyForm.Show();
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

        private void searchTextbox_TextChanged(object sender, EventArgs e)
        {
            searchText = searchTextbox.Text.ToLower();

            fillGridView();
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
                case 2:
                    activeSearchCrit = 2;
                    break;
                default:
                    activeSearchCrit = 0;
                    break;
            }
        }
    }
}

