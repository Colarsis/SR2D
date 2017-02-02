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
    public partial class materiauxAddForm : Form
    {
        materiauxForm matF;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        public materiauxAddForm(CGestClassBound cB, materiauxForm matF)
        {
            InitializeComponent();

            this.matF = matF;
            this.cB = cB;
            this.db = cB.db;
            this.networkControl = cB.networkControler;

            fillUnits();
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

        public string getNextID(string table)
        {
            switch (table)
            {
                case "materiaux":
                    DataRowCollection drC = db.Ds.Dataset.Tables["materiaux"].Rows;

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
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom du materiau spécifié est incorrect ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (unitCombobox.SelectedItem == null)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("L'unité specifiée est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (descTextBox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("La description specifiée est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRowCollection drC = db.Ds.Dataset.Tables["materiaux"].Rows;

            foreach (DataRow dr in drC)
            {
                if (dr.ItemArray[1].ToString() == nameTextbox.Text)
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

            string[] tables = {"materiaux"};

            DataRow newRow = db.Ds.Dataset.Tables["materiaux"].NewRow();

            newRow["id_materiaux"] = getNextID("materiaux");
            newRow["name_materiaux"] = nameTextbox.Text;
            newRow["id_unit"] = unitCombobox.SelectedItem.ToString().Split('-')[0].Trim();
            newRow["description"] = descTextBox.Text;

            db.Ds.Dataset.Tables["materiaux"].Rows.Add(newRow);

            db.Ds.updateDatabase(tables);

            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            matF.Enabled = true;
        }
    }
}
