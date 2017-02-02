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
    public partial class foodModifyForm : Form
    {

        foodForm fF;
        SR2DClassBound cB;
        SR2DDatabase db;
        DataRow refDR;

        List<int> foodTypeIDs;

        public foodModifyForm(foodForm fF, SR2DClassBound cB, DataRow refDR)
        {
            InitializeComponent();

            this.fF = fF;
            this.cB = cB;
            this.db = cB.db;
            this.refDR = refDR;

            foodTypeIDs = new List<int>();

            fill();
        }

        public void fill()
        {
            nameTextbox.Text = refDR.ItemArray[1].ToString();
            priceUpDown.Value = decimal.Parse(refDR.ItemArray[4].ToString().Replace('.', ','));

            typeCombo.Items.Clear();
            foodTypeIDs.Clear();

            DataRowCollection rows = db.Ds.Dataset.Tables["food_types"].Rows;

            int i;
            int actualID = 0;

            for(i = 0; i < rows.Count; i++)
            {
                typeCombo.Items.Add(rows[i].ItemArray[1].ToString());
                foodTypeIDs.Add(int.Parse(rows[i].ItemArray[0].ToString()));

                if(int.Parse(rows[i].ItemArray[0].ToString()) == (int)refDR.ItemArray[3])
                {
                    actualID = i;
                }
            }

            typeCombo.SelectedIndex = actualID;

        }

        public int indiceToID(int indice)
        {
            return foodTypeIDs[indice];
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void foodModifyForm_FormClosed(object sender, FormClosedEventArgs e)
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
                if (dr.ItemArray[1].ToString() == nameTextbox.Text && dr != refDR)
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

            object[] array = db.Ds.Dataset.Tables["food"].Rows.Find(refDR.ItemArray[0].ToString()).ItemArray;

            array[1] = nameTextbox.Text;
            array[3] = indiceToID(typeCombo.SelectedIndex).ToString();
            array[4] = priceUpDown.Value.ToString();

            db.Ds.Dataset.Tables["food"].Rows.Find(refDR.ItemArray[0].ToString()).ItemArray = array;

            db.Ds.updateDatabase(table);

            this.Close();
        }
    }
}
