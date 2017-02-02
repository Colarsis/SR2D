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
    public partial class foodTypeModifyForm : Form
    {

        foodTypeForm fTF;
        SR2DClassBound cB;
        SR2DDatabase db;
        DataRow refDR;

        List<int> othersFoodTypeIDs;
        List<CheckState> othersFoodTypeInitialState;

        public foodTypeModifyForm(foodTypeForm fTF, SR2DClassBound cB, DataRow refDR)
        {
            InitializeComponent();

            this.fTF = fTF;
            this.cB = cB;
            this.db = cB.db;
            this.refDR = refDR;

            othersFoodTypeInitialState = new List<CheckState>();
            othersFoodTypeIDs = new List<int>();

            fill();
        }

        public void fill()
        {
            nameTextbox.Text = refDR.ItemArray[1].ToString();

            chListbox.Items.Clear();
            othersFoodTypeInitialState.Clear();
            othersFoodTypeIDs.Clear();

            DataRowCollection rows = db.Ds.Dataset.Tables["food_types"].Rows;

            DataRowCollection excludedRows = TablesUtilities.getExcludedFromFoodType(refDR, db.Ds);

            foreach(DataRow dr in rows)
            {
                if(dr != refDR)
                {

                    bool triggered = false;

                    foreach(DataRow eDR in excludedRows)
                    {
                        if(eDR.ItemArray[2].ToString() == dr.ItemArray[0].ToString())
                        {
                            chListbox.Items.Add(dr.ItemArray[1], CheckState.Checked);
                            othersFoodTypeInitialState.Add(CheckState.Checked);
                            triggered = true;
                            break;
                        }
                    }

                    if(!triggered)
                    {
                        chListbox.Items.Add(dr.ItemArray[1], CheckState.Unchecked);
                        othersFoodTypeInitialState.Add(CheckState.Unchecked);
                    }
                    
                    othersFoodTypeIDs.Add(int.Parse(dr.ItemArray[0].ToString()));
                }
                
            }
        }

        public int indiceToID(int indice)
        {
            return othersFoodTypeIDs[indice];
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

            string[] table = { "foodTypes" };


            object[] array = db.Ds.Dataset.Tables["food_types"].Rows.Find(refDR.ItemArray[0].ToString()).ItemArray;

            array[1] = nameTextbox.Text;

            db.Ds.Dataset.Tables["food_types"].Rows.Find(refDR.ItemArray[0].ToString()).ItemArray = array;

            db.Ds.updateDatabase(table);

            table[0] = "excludedFoodTypes";

            int i;

            List<int> toDeleteIDs = new List<int>();

            for(i = 0; i < chListbox.Items.Count; i++)
            {

                Console.WriteLine(i);

                if(chListbox.GetItemCheckState(i) != othersFoodTypeInitialState[i])
                {
                    if (chListbox.GetItemCheckState(i) == CheckState.Checked)
                    {
                        DataRow exclRow = db.Ds.Dataset.Tables["excluded_food_types_join"].NewRow();

                        exclRow["id"] = TablesUtilities.getNextID("excluded_food_types_join", db);
                        exclRow["master_food_types"] = refDR.ItemArray[0].ToString();
                        exclRow["slave_food_types"] = indiceToID(i);

                        db.Ds.Dataset.Tables["excluded_food_types_join"].Rows.Add(exclRow);
                    }
                    else if (chListbox.GetItemCheckState(i) == CheckState.Unchecked)
                    {
                        

                        foreach (DataRow d in db.Ds.Dataset.Tables["excluded_food_types_join"].Rows)
                        {
                            if (int.Parse(d.ItemArray[1].ToString()) == int.Parse(refDR.ItemArray[0].ToString()) && (int)d.ItemArray[2] == othersFoodTypeIDs[i])
                            {
                                toDeleteIDs.Add(int.Parse(d.ItemArray[0].ToString()));
                            }
                        }
                    }                
                }
            }

            foreach (int it in toDeleteIDs)
            {
                db.Ds.Dataset.Tables["excluded_food_types_join"].Rows.Find(it).Delete();
            }

            db.Ds.updateDatabase(table);

            this.Close();
        }

        private void foodTypeModifyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            fTF.Enabled = true;
        }
    }
}
