using CGest.Database;
using CGest.Network;
using CGest.Utilities;
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
    public partial class clientModifyForm : Form
    {
        clientForm cF;

        DataRow dr;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public clientModifyForm(CGestClassBound cB, clientForm cF, DataRow dr)
        {
            InitializeComponent();

            this.cF = cF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            this.dr = dr;

            load();
        }

        public void load()
        {
            nameTextbox.Text = dr.ItemArray[1].ToString();
            surnameTextbox.Text = dr.ItemArray[2].ToString();
            addrTextbox.Text = dr.ItemArray[3].ToString();
            postalTextbox.Text = dr.ItemArray[4].ToString();
            cityTextbox.Text = dr.ItemArray[5].ToString();
            relTextbox.Text = dr.ItemArray[6].ToString();
            infosTextbox.Text = dr.ItemArray[7].ToString();
            fixeTelTextbox.Text = dr.ItemArray[8].ToString();
            mobilTelTextbox.Text = dr.ItemArray[9].ToString();
            mailTextbox.Text = dr.ItemArray[10].ToString();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] table = { "client" };

            if (nameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (surnameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le prénom du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (addrTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("L'adresse du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            int i;

            if (postalTextbox.Text == "" || !int.TryParse(postalTextbox.Text, out i))
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le code postal du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (cityTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le ville du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (fixeTelTextbox.Text == "")
            {
                fixeTelTextbox.Text = " ";
            }

            if (mobilTelTextbox.Text == "")
            {
                mobilTelTextbox.Text = " ";
            }

            if (mailTextbox.Text == "")
            {
                mailTextbox.Text = " ";
            }

            if (relTextbox.Text == "")
            {
                relTextbox.Text = " ";
            }

            if (infosTextbox.Text == "")
            {
                infosTextbox.Text = " ";
            }

            object[] array = db.Ds.Dataset.Tables["client"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray;

            array[1] = nameTextbox.Text;
            array[2] = surnameTextbox.Text;
            array[3] = addrTextbox.Text;
            array[4] = int.Parse(postalTextbox.Text);
            array[5] = cityTextbox.Text;
            array[6] = relTextbox.Text;
            array[7] = infosTextbox.Text;
            array[8] = fixeTelTextbox.Text;
            array[9] = mobilTelTextbox.Text;
            array[10] = mailTextbox.Text;

            db.Ds.Dataset.Tables["client"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = array;

            db.Ds.updateDatabase(table);

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientModifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cF.Enabled = true;
        }
    }
}
