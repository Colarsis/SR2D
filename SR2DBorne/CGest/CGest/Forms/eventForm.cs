using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGest.Database;
using CGest.Utilities;
using CGest.Network;

namespace CGest.Forms
{
    public partial class eventForm : Form
    {

        eventCalendarForm eCF;
        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public int id;

        public eventForm(eventCalendarForm eCF, int id, CGestClassBound cB)
        {
            InitializeComponent();

            this.eCF = eCF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;
            this.id = id;

            init();
        }

        public void init()
        {
            DataRow eventRow = eCF.getRowFromID(id);

            if (eventRow != null)
            {
                titleLabel.Text = eventRow.ItemArray[1].ToString();
                descTexbox.Text = eventRow.ItemArray[2].ToString();

                beginLabel.Text = "Le " + eventRow.ItemArray[3].ToString().Split('H')[0] + " à " + eventRow.ItemArray[3].ToString().Split('H')[1];
                endLabel.Text = "Le " + eventRow.ItemArray[4].ToString().Split('H')[0] + " à " + eventRow.ItemArray[4].ToString().Split('H')[1];

                networkControl.requestUserID(eventRow.ItemArray[5].ToString());

                while (networkControl.client.enteringQueue.Count == 0)
                {
                }

                while (networkControl.client.enteringQueue.Peek().Split(';')[0] != "userid")
                {
                }

                string dequeue = networkControl.client.enteringQueue.Dequeue();

                if (dequeue.Split(';')[2] != "refused" && dequeue.Split(';')[2] != "error")
                {
                    if (dequeue.Split(';')[1] == eventRow.ItemArray[5].ToString())
                    {
                        creatorLabel.Text = dequeue.Split(';')[2];
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Une erreur est survenue lors du chargement de l'évenement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }

                networkControl.requestUserID(eventRow.ItemArray[7].ToString());
                
                while (networkControl.client.enteringQueue.Count == 0)
                {
                }

                while (networkControl.client.enteringQueue.Peek().Split(';')[0] != "userid")
                {

                }

                dequeue = networkControl.client.enteringQueue.Dequeue();

                if (dequeue.Split(';')[2] != "refused" && dequeue.Split(';')[2] != "error")
                {
                    if (dequeue.Split(';')[1] == eventRow.ItemArray[7].ToString())
                    {
                        foLabel.Text = dequeue.Split(';')[2];
                    }
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Une erreur est survenue lors du chargement de l'évenement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Une erreur est survenue lors du chargement de l'évenement", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }



            
        }

        private void eventForm_Resize(object sender, EventArgs e)
        {
            descTexbox.Width = 433 + (this.Width - 564);

            modifyButton.Location = new Point(326 + (this.Width - 564), modifyButton.Location.Y);
            deleteButton.Location = new Point(434 + (this.Width - 564), deleteButton.Location.Y);
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            eventModifyForm eMF = new eventModifyForm(this, cB);

            eMF.Show();

            this.Enabled = false;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            string[] tables = { "event" };

            DialogResult dr = MessageBox.Show("Etes-vous sûre de vouloir supprimer cet évenement ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                TablesUtilities.removeEventByID(id.ToString(), db.Ds);

                db.Ds.updateDatabase(tables);

                this.Close();
            }
        }
    }
}
