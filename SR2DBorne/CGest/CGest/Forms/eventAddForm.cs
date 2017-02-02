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
using CGest.Network;
using CGest.Utilities;

namespace CGest.Forms
{
    public partial class eventAddForm : Form
    {
        eventCalendarForm eCF;
        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public eventAddForm(CGestClassBound cB, eventCalendarForm eCF)
        {
            InitializeComponent();

            this.eCF = eCF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;
        }

        public string getNextID(string table)
        {
            switch (table)
            {
                case "event":
                    DataRowCollection drC = db.Ds.Dataset.Tables["event"].Rows;

                    int maxID = 0;

                    foreach (DataRow dr in drC)
                    {
                        if (int.Parse(dr.ItemArray[0].ToString()) > maxID)
                        {
                            maxID = int.Parse(dr.ItemArray[0].ToString());
                        }
                    }

                    maxID++;

                    return maxID.ToString();
                default:
                    return null;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            eCF.addEventButton.Enabled = true;

            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (titleTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom de l'évenement spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime begin = beginDate.Value.AddHours((int)beginHour.Value + beginDate.Value.Hour * -1).AddMinutes((int)beginMin.Value + beginDate.Value.Minute * -1);
            DateTime end = endDate.Value.AddHours((int)endHour.Value + endDate.Value.Hour * -1).AddMinutes((int)endMin.Value + endDate.Value.Minute * -1);

            if (begin >= end)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("La date de commencement de l'évenement doit être inferieur à celle de fin ! Veuillez remplir les champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] tables = { "event" };

            DataRow newRow = db.Ds.Dataset.Tables["event"].NewRow();

            newRow["id"] = getNextID("event");
            newRow["title"] = titleTextbox.Text;

            if (descTextbox.Text == "")
            {
                newRow["desc"] = " ";
            }
            else
            {
                newRow["desc"] = descTextbox.Text;
            }

            newRow["begin"] = begin.Day + "/" + begin.Month + "/" + begin.Year + "H" + begin.Hour + ":" + begin.Minute;
            newRow["end"] = end.Day + "/" + end.Month + "/" + end.Year + "H" + end.Hour + ":" + end.Minute;

            newRow["proprietary"] = networkControl.id;
            newRow["type"] = "classic";
            newRow["affected"] = networkControl.id;

            db.Ds.Dataset.Tables["event"].Rows.Add(newRow);

            db.Ds.updateDatabase(tables);

            eCF.addEventButton.Enabled = true;

            this.Close();
        }
    }
}
