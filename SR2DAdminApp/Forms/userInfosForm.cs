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
    public partial class userInfosForm : Form
    {
        userForm cF;

        SR2DClassBound cB;
        SR2DDatabase db;
        SR2DNetworkClientControl networkControl;

        DataRow dr;

        public userInfosForm(SR2DClassBound cB, userForm cF, DataRow dr)
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
            nameTextbox.Text = dr.ItemArray[1].ToString();
            surnameTextbox.Text = dr.ItemArray[2].ToString();
            classCodeTextbox.Text = dr.ItemArray[4].ToString();
            classFullTextbox.Text = dr.ItemArray[5].ToString();
            cardCodeTextbox.Text = dr.ItemArray[3].ToString();

            if((bool)dr.ItemArray[6] == false)
            {
                passedTextbox.Text = "Non";
            }
            else
            {
                passedTextbox.Text = "Oui";
            }

            /**************************************/
            /****** Ajouter les réservations ******/
            /**************************************/

            if ((bool)dr.ItemArray[7] == false)
            {
                retiredTextbox.Text = "Non";
            }
            else
            {
                retiredTextbox.Text = "Oui";
            }
            
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clientInfosForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cF.Enabled = true;
        }

        private void clientInfosForm_Load(object sender, EventArgs e)
        {

        }
    }
}
