using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using SR2DAdminApp.Network;
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
    public partial class foodAddForm : Form
    {
        foodForm fF;
        SR2DClassBound cB;
        SR2DDatabase db;

        List<int> foodTypeIDs;

        public foodAddForm(foodForm fF, SR2DClassBound cB)
        {
            InitializeComponent();

            this.fF = fF;
            this.cB = cB;
            this.db = cB.db;

            foodTypeIDs = new List<int>();

            fill();
        }

        public void fill()
        {
            typeCombo.Items.Clear();
            foodTypeIDs.Clear();

            DataRowCollection rows = db.Ds.Dataset.Tables["food_types"].Rows;

            foreach(DataRow dr in rows)
            {
                typeCombo.Items.Add(dr.ItemArray[1].ToString());
                foodTypeIDs.Add(int.Parse(dr.ItemArray[0].ToString()));
            }

        }

        public int indiceToID(int indice)
        {
            return foodTypeIDs[indice];
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void foodAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            fF.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (nameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom du type spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (typeCombo.SelectedIndex == -1)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le type spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRowCollection drC = db.Ds.Dataset.Tables["food"].Rows;

            foreach (DataRow dr in drC)
            {
                if (dr.ItemArray[1].ToString() == nameTextbox.Text)
                {
                    DialogResult errorExistMsg = MessageBox.Show("Le nom du type spécifié existe déjà dans la base de donnée ! Voulez quand même le rajouter ? (ATTENTION : Creer un doublon dans la base de donnée peut créer des erreurs dans le traitement des autres requêtes !)", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

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

            string[] table = { "food" };

            DataRow newRow = db.Ds.Dataset.Tables["food"].NewRow();

            newRow["id"] = TablesUtilities.getNextID("food", db);
            newRow["name"] = nameTextbox.Text;
            newRow["quantity"] = 0;
            newRow["type_id"] = indiceToID(typeCombo.SelectedIndex);
            newRow["price"] = priceUpDown.Value.ToString();

            db.Ds.Dataset.Tables["food"].Rows.Add(newRow);

            db.Ds.updateDatabase(table);

            this.Close();
        }
    }
}
