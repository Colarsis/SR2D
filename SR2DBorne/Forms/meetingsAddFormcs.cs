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
    public partial class meetingsAddFormcs : Form
    {
        clientForm cF;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public meetingsAddFormcs(CGestClassBound cB, clientForm cF)
        {
            InitializeComponent();

            this.cF = cF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;
        }

        public string getNextID(string table)
        {
            switch (table)
            {
                case "meetings":
                    DataRowCollection drC = db.Ds.Dataset.Tables["meetings"].Rows;

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            //if (nameTextbox.Text == "")
            //{
            //    DialogResult errorEmptyMsg = MessageBox.Show("Le nom du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            //if (surnameTextbox.Text == "")
            //{
            //    DialogResult errorEmptyMsg = MessageBox.Show("Le prénom du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            //if (addrTextbox.Text == "")
            //{
            //    DialogResult errorEmptyMsg = MessageBox.Show("L'adresse du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            //int i;

            //if (postalTextbox.Text == "" || !int.TryParse(postalTextbox.Text, out i))
            //{
            //    DialogResult errorEmptyMsg = MessageBox.Show("Le code postal du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            //if (cityTextbox.Text == "")
            //{
            //    DialogResult errorEmptyMsg = MessageBox.Show("Le ville du client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //    return;
            //}

            //if (fixeTelTextbox.Text == "")
            //{
            //    fixeTelTextbox.Text = " ";
            //}

            //if (mobilTelTextbox.Text == "")
            //{
            //    mobilTelTextbox.Text = " ";
            //}

            //if (mailTextbox.Text == "")
            //{
            //    mailTextbox.Text = " ";
            //}

            //if (relTextbox.Text == "")
            //{
            //    relTextbox.Text = " ";
            //}

            //if (infosTextbox.Text == "")
            //{
            //    infosTextbox.Text = " ";
            //}

            //string[] tables = {"client"};

            //DataRow newRow = db.Ds.Dataset.Tables["client"].NewRow();

            //newRow["id_client"] = getNextID("client");
            //newRow["name_client"] = nameTextbox.Text;
            //newRow["surname_client"] = surnameTextbox.Text;
            //newRow["addr_client"] = addrTextbox.Text;
            //newRow["postal_client"] = int.Parse(postalTextbox.Text);
            //newRow["city_client"] = cityTextbox.Text;
            //newRow["relation_client"] = relTextbox.Text;
            //newRow["desc_client"] = infosTextbox.Text;
            //newRow["phoneNumber"] = fixeTelTextbox.Text;
            //newRow["mobileNumber"] = mobilTelTextbox.Text;
            //newRow["mail"] = mailTextbox.Text;

            //db.Ds.Dataset.Tables["client"].Rows.Add(newRow);

            //db.Ds.updateDatabase(tables);

            //this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cF.Enabled = true;
        }
    }
}
