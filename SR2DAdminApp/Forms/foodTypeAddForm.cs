using SR2DAdminApp.Database;
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
    public partial class foodTypeAddForm : Form
    {

        foodTypeForm fTF;
        SR2DClassBound cB;
        SR2DDatabase db;

        List<int> othersFoodTypeIDs;

        public foodTypeAddForm(foodTypeForm fTF, SR2DClassBound cB)
        {
            InitializeComponent();

            this.fTF = fTF;
            this.cB = cB;
            this.db = cB.db;

            othersFoodTypeIDs = new List<int>();

            fillChListbox();
        }

        public void fillChListbox()
        {
            DataRowCollection rows = db.Ds.Dataset.Tables["food_types"].Rows;

            foreach(DataRow dr in rows)
            {
                chListbox.Items.Add(dr.ItemArray[1], CheckState.Unchecked);

                othersFoodTypeIDs.Add(int.Parse(dr.ItemArray[0].ToString()));
            }
        }

        public int indiceToID(int indice)
        {
            return othersFoodTypeIDs[indice];
        }

        private void foodTypeAddForm_Load(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void foodTypeAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            fTF.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (nameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom du type spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRowCollection drC = db.Ds.Dataset.Tables["food_types"].Rows;

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

            string[] tables = { "foodTypes" };

            DataRow newRow = db.Ds.Dataset.Tables["food_types"].NewRow();

            newRow["id"] = TablesUtilities.getNextID("food_types", db);
            newRow["name"] = nameTextbox.Text;

            db.Ds.Dataset.Tables["food_types"].Rows.Add(newRow);

            db.Ds.updateDatabase(tables);

            tables[0] = "excludedFoodTypes";

            foreach (int it in chListbox.CheckedIndices)
            {
                DataRow exclRow = db.Ds.Dataset.Tables["excluded_food_types_join"].NewRow();

                exclRow["id"] = TablesUtilities.getNextID("excluded_food_types_join", db);
                exclRow["master_food_types"] = newRow.ItemArray[0].ToString();
                exclRow["slave_food_types"] = indiceToID(it);

                db.Ds.Dataset.Tables["excluded_food_types_join"].Rows.Add(exclRow);
            }

            db.Ds.updateDatabase(tables);

            this.Close();
        }
    }
}
