using SR2DAdminApp.Database;
using SR2DAdminApp.Network;
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
    public partial class MeetingsForm : Form
    {
        MainForm mF;

        SR2DClassBound cB;
        SR2DDatabase db;
        SR2DNetworkClientControl networkControl;

        public delegate void fillGridViewCallback(string searchValue);

        public MeetingsForm(MainForm mF, SR2DClassBound cB)
        {
            InitializeComponent();

            this.mF = mF;

            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            fillGridView("");

            db.Ds.DatasetUpdatedEvent += new SR2DDataset.EventHandler(update);
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

                DataRowCollection meetingsRows = db.Ds.Dataset.Tables["meetings"].Rows;

                foreach (DataRow dr in meetingsRows)
                {
                    if (dr.ItemArray[1].ToString().Contains(""))
                    {
                        addMeetingsToGrid(dr);
                    }
                }

                dataGridView1.ClearSelection();
            }
        }

        public void fillGridView(string searchText)
        {
            dataGridView1.Rows.Clear();

            DataRowCollection meetingsRows = db.Ds.Dataset.Tables["meetings"].Rows;

            foreach (DataRow dr in meetingsRows)
            {
                if (searchText != "")
                {
                    if (dr.ItemArray[1].ToString().ToLower().Contains(searchText))
                    {
                        addMeetingsToGrid(dr);
                    }
                }
                else
                {
                    addMeetingsToGrid(dr);
                }

            }

            dataGridView1.ClearSelection();
        }

        public void addMeetingsToGrid(DataRow dr)
        {
            string[] textRow = {"", "", "", "", "", "", "", ""};

            string id = dr.ItemArray[0].ToString();
            string client_id = dr.ItemArray[1].ToString();
            string chantier_id = dr.ItemArray[2].ToString();
            string event_id = dr.ItemArray[3].ToString();
            string client = TablesUtilities.getClientNameFromID(client_id, db.Ds);
            string inter = TablesUtilities.getEventFromId(event_id, db.Ds).ItemArray[7].ToString();
            string date = TablesUtilities.stringToDate(TablesUtilities.getEventFromId(event_id, db.Ds).ItemArray[3].ToString());
            string status = dr.ItemArray[6].ToString();

            textRow[0] = id;
            textRow[1] = client_id;
            textRow[2] = chantier_id;
            textRow[3] = event_id;
            textRow[3] = client;
            textRow[3] = inter;
            textRow[3] = date;
            textRow[3] = status;

            dataGridView1.Rows.Add(textRow);
        }

        private void meetingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            db.Ds.DatasetUpdatedEvent -= new SR2DDataset.EventHandler(update);

            //mF.setButtonEnabling(mF.meetingsBtn, true);
        }

        private void meetingsForm_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = 864 + (this.Width - 1083);
            dataGridView1.Height = 477 + (this.Height - 539);

            groupBox1.Height = 477 + (this.Height - 539);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            fillGridView(searchTextbox.Text.ToString().ToLower());
        }

        private void addMeetingsButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            //meetingsAddFormcs addForm = new meetingsAddFormcs(cB, this);

            //addForm.Show();
        }

        private void modifyMeetingsButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            //meetingsModifyForm modifyForm = new meetingsModifyForm(cB, this, db.Ds.Dataset.Tables["meetings"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            //modifyForm.Show();
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            //meetingsInfosForm infosForm = new meetingsInfosForm(cB, this, db.Ds.Dataset.Tables["meetings"].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            //infosForm.Show();
        }

        private void deleteMeetingsButton_Click(object sender, EventArgs e)
        {
            string[] events = { "event" };
            string[] meetings = { "meetings" };
            
            DataRow eventRow = TablesUtilities.getEventFromId(dataGridView1.SelectedRows[0].Cells[3].Value.ToString(), db.Ds);

            DialogResult deleteAskBox = MessageBox.Show("Etes-vous sûre de vouloir supprimer le rendez-vous sélectionné ?", "Confirmation de la suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (deleteAskBox != DialogResult.Yes)
            {
                    return;
            }

            if (eventRow != null)
            {
                TablesUtilities.removeEventByID(eventRow.ItemArray[0].ToString(), db.Ds);

                db.Ds.updateDatabase(events);
            }

            TablesUtilities.removeMeetingsByID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), db.Ds);

            db.Ds.updateDatabase(meetings);

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

        private void meetingsForm_Load(object sender, EventArgs e)
        {

        }

        private void endButton_Click(object sender, EventArgs e)
        {

        }

        private void calendarMeetingButton_Click(object sender, EventArgs e)
        {

        }
    }
}
