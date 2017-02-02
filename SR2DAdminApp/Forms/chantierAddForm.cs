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
    public partial class chantierAddForm : Form
    {
        chantierForm cF;

        SR2DClassBound cB;
        SR2DDatabase db;
        SR2DNetworkClientControl networkControl;

        public chantierAddForm(SR2DClassBound cB, chantierForm cF)
        {
            InitializeComponent();

            this.cF = cF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            fillClients();
        }

        public void fillClients()
        {
            DataRowCollection drC = db.Ds.Dataset.Tables["client"].Rows;

            foreach (DataRow dr in drC)
            {
                string[] clientText = { "", "" };

                clientText[0] = dr.ItemArray[0].ToString();
                clientText[1] = dr.ItemArray[1].ToString();

                clientCombobox.Items.Add(clientText[0] + " - " + clientText[1]);
            }
        }

        public string getNextID(string table)
        {
            switch (table)
            {
                case "chantier":
                    DataRowCollection drC = db.Ds.Dataset.Tables["chantier"].Rows;

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
            if (nameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom du chantier spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (clientCombobox.SelectedItem == null)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le client spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRowCollection drC = db.Ds.Dataset.Tables["chantier"].Rows;

            foreach (DataRow dr in drC)
            {
                if (dr.ItemArray[2].ToString() == nameTextbox.Text)
                {
                    DialogResult errorExistMsg = MessageBox.Show("Le nom de chantier spécifié existe déjà dans la base de donnée ! Voulez quand même le rajouter ? (ATTENTION : Creer un doublon dans la base de donnée peut créer des erreurs dans le traitement des autres requêtes !)", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (errorExistMsg == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            string[] tables = {"chantier"};

            DataRow newRow = db.Ds.Dataset.Tables["chantier"].NewRow();

            newRow["id_chantier"] = getNextID("chantier");
            newRow["id_client"] = clientCombobox.SelectedItem.ToString().Split('-')[0].Trim();
            newRow["name_chantier"] = nameTextbox.Text;

            db.Ds.Dataset.Tables["chantier"].Rows.Add(newRow);

            db.Ds.updateDatabase(tables);

            this.Close();
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
