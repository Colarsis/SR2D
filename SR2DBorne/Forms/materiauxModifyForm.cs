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
    public partial class materiauxModifyForm : Form
    {
        materiauxForm matF;

        DataRow dr;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public materiauxModifyForm(CGestClassBound cB, materiauxForm matF, DataRow dr)
        {
            InitializeComponent();

            this.matF = matF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            this.dr = dr;

            load();
        }

        public void load()
        {
            fillUnits();

            unitCombobox.SelectedIndex = db.Ds.Dataset.Tables["unit"].Rows.IndexOf(db.Ds.Dataset.Tables["unit"].Rows.Find(dr.ItemArray[2].ToString()));

            nameTextbox.Text = dr.ItemArray[1].ToString();

            descTextBox.Text = dr.ItemArray[3].ToString();
        }

        public void fillUnits()
        {
            DataRowCollection drC = db.Ds.Dataset.Tables["unit"].Rows;

            foreach (DataRow dr in drC)
            {
                string[] unitText = { "", "" };

                unitText[0] = dr.ItemArray[0].ToString();
                unitText[1] = dr.ItemArray[1].ToString();

                unitCombobox.Items.Add(unitText[0] + " - " + unitText[1]);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] table = { "materiaux" };

            if (nameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom du materiau spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (unitCombobox.SelectedItem == null)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("L'unité spécifiée est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (descTextBox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("La description specifiée est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRowCollection drC = db.Ds.Dataset.Tables["materiaux"].Rows;

            foreach (DataRow dr2 in drC)
            {
                if (dr2.ItemArray[1].ToString() == nameTextbox.Text && nameTextbox.Text != dr.ItemArray[1].ToString())
                {
                    DialogResult errorExistMsg = MessageBox.Show("Le nom de materiau spécifié existe déjà dans la base de donnée ! Voulez quand même le rajouter ? (ATTENTION : Creer un doublon dans la base de donnée peut créer des erreurs dans le traitement des autres requêtes !)", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

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

            object[] array = db.Ds.Dataset.Tables["materiaux"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray;

            array[2] = unitCombobox.SelectedItem.ToString().Split('-')[0].Trim();
            array[1] = nameTextbox.Text;
            array[3] = descTextBox.Text;

            db.Ds.Dataset.Tables["materiaux"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = array;

            db.Ds.updateDatabase(table);

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientModifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            matF.Enabled = true;
        }
    }
}
