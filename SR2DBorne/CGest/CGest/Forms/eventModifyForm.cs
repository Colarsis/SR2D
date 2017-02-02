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
    public partial class eventModifyForm : Form
    {

        private CGestClassBound cB;
        private eventForm eF;
        private CGestDatabase db;
        private CGestNetworkClientControl networkControl;

        public eventModifyForm(eventForm eF, CGestClassBound cB)
        {
            InitializeComponent();

            this.eF = eF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            init();
        }

        public void init()
        {
            titleTextbox.Text = eF.titleLabel.Text;
            descTextbox.Text = eF.descTexbox.Text;

            string[] begin = eF.beginLabel.Text.Replace("Le ", "").Replace(" à ", "H").Split('H');
            string[] date = begin[0].Split('/');
            string[] hour = begin[1].Split(':');

            beginDate.Value = new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]), 0, 0, 0);
            beginHour.Value = int.Parse(hour[0]);
            beginMin.Value = int.Parse(hour[1]);

            string[] end = eF.endLabel.Text.Replace("Le ", "").Replace(" à ", "H").Split('H');
            string[] endingDate = end[0].Split('/');
            string[] endingHour = end[1].Split(':');

            endDate.Value = new DateTime(int.Parse(endingDate[2]), int.Parse(endingDate[1]), int.Parse(endingDate[0]), 0, 0, 0);
            endHour.Value = int.Parse(endingHour[0]);
            endMin.Value = int.Parse(endingHour[1]);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eventModifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            eF.Enabled = true;
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

            object[] obj = db.Ds.Dataset.Tables["event"].Rows.Find(eF.id).ItemArray;

            obj[1] = titleTextbox.Text;

            if (descTextbox.Text == "")
            {
                obj[2] = " ";
            }
            else
            {
                obj[2] = descTextbox.Text;
            }

            obj[3] = beginDate.Value.Day + "/" + beginDate.Value.Month + "/" + beginDate.Value.Year + "H" + beginHour.Value + ":" + beginMin.Value;
            obj[4] = endDate.Value.Day + "/" + endDate.Value.Month + "/" + endDate.Value.Year + "H" + endHour.Value + ":" + endMin.Value;

            obj[5] = networkControl.id;

            db.Ds.Dataset.Tables["event"].Rows.Find(eF.id).ItemArray = obj;

            db.Ds.updateDatabase(tables);

            this.Close();

            eF.Close();
        }
    }
}
