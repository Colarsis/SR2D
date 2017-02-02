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
    public partial class chantierModifyForm : Form
    {
        chantierForm cF;

        DataRow dr;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public chantierModifyForm(CGestClassBound cB, chantierForm cF, DataRow dr)
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
            fillClients();

            clientCombobox.SelectedIndex = db.Ds.Dataset.Tables["client"].Rows.IndexOf(db.Ds.Dataset.Tables["client"].Rows.Find(dr.ItemArray[1].ToString()));

            nameTextbox.Text = dr.ItemArray[2].ToString();
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] table = { "chantier" };

            if (nameTextbox.Text == "" || nameTextbox.Text == dr.ItemArray[1].ToString())
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

            foreach (DataRow dr2 in drC)
            {
                if (dr2.ItemArray[2].ToString() == nameTextbox.Text && nameTextbox.Text != dr.ItemArray[2].ToString())
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

            object[] array = db.Ds.Dataset.Tables["chantier"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray;

            array[1] = clientCombobox.SelectedItem.ToString().Split('-')[0].Trim();
            array[2] = nameTextbox.Text;

            db.Ds.Dataset.Tables["chantier"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = array;

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
