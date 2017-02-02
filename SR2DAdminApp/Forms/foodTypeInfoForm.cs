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
    public partial class foodTypeInfoForm : Form
    {
        foodTypeForm fTF;
        SR2DClassBound cB;
        SR2DDatabase db;
        DataRow refDR;

        public foodTypeInfoForm(foodTypeForm fTF, SR2DClassBound cB, DataRow refDR)
        {
            InitializeComponent();

            this.fTF = fTF;
            this.cB = cB;
            this.db = cB.db;
            this.refDR = refDR;

            init();
        }

        public void init()
        {
            nameTextbox.Text = refDR.ItemArray[1].ToString();

            listBox1.Items.Clear();

            DataRowCollection rows = db.Ds.Dataset.Tables["food_types"].Rows;

            DataRowCollection excludedRows = TablesUtilities.getExcludedFromFoodType(refDR, db.Ds);

            foreach (DataRow dr in rows)
            {
                if (dr != refDR)
                {

                    foreach (DataRow eDR in excludedRows)
                    {
                        if (eDR.ItemArray[2].ToString() == dr.ItemArray[0].ToString())
                        {
                            listBox1.Items.Add(dr.ItemArray[1]);
                            break;
                        }
                    }
                }

            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void foodTypeInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            fTF.Enabled = true;
        }
    }
}
