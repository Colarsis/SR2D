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
    public partial class userModifyForm : Form
    {
        userForm cF;

        DataRow dr;

        SR2DClassBound cB;
        SR2DDatabase db;
        SR2DNetworkClientControl networkControl;

        public userModifyForm(SR2DClassBound cB, userForm cF, DataRow dr)
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

            if ((bool)dr.ItemArray[6] == false)
            {
                passedCombo.SelectedIndex = 1;
            }
            else
            {
                passedCombo.SelectedIndex = 0;
                bookingList.Enabled = false;
                modifyBookingButton.Enabled = false;
            }

            /**************************************/
            /****** Ajouter les réservations ******/
            /**************************************/

            if ((bool)dr.ItemArray[7] == false)
            {
               retiredCombo.SelectedIndex = 1;
               modifyBookingButton.Enabled = false;
            }
            else
            {
                retiredCombo.SelectedIndex = 0;
                modifyBookingButton.Enabled = true;
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string[] table = { "badges" };

            if (nameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le nom de l'élève spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (surnameTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le prénom de l'élève spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (classCodeTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("Le code classe de l'élève spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (classFullTextbox.Text == "")
            {
                DialogResult errorEmptyMsg = MessageBox.Show("La classe de l'élève spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (passedCombo.SelectedIndex != 1 && passedCombo.SelectedIndex != 0)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("La valeur de l'enregistrement de l'élève spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (retiredCombo.SelectedIndex != 1 && retiredCombo.SelectedIndex != 0)
            {
                DialogResult errorEmptyMsg = MessageBox.Show("La valeur du passage de l'élève spécifié est incorrecte ! Veuillez remplir le champs correctement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            object[] array = db.Ds.Dataset.Tables["badges"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray;

            array[1] = nameTextbox.Text;
            array[2] = surnameTextbox.Text;
            array[3] = cardCodeTextbox.Text;
            array[4] = classCodeTextbox.Text;
            array[5] = classFullTextbox.Text;
            if(passedCombo.SelectedIndex == 0)
            {
                array[6] = 1;

                /* Inclure réservation ici pour éviter conflit*/
            }
            else
            {
                array[6] = 0;
            }

            if (retiredCombo.SelectedIndex == 0)
            {
                array[7] = 1;
            }
            else
            {
                array[7] = 0;
            }

            db.Ds.Dataset.Tables["badges"].Rows.Find(dr.ItemArray[0].ToString()).ItemArray = array;

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

        private void modifyBookingButton_Click(object sender, EventArgs e)
        {
            /*TODO*/
        }

        private void passedCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (passedCombo.SelectedIndex == 1)
            {
                bookingList.Enabled = true;
                modifyBookingButton.Enabled = true;
            }
            else
            {
                bookingList.Enabled = false;
                modifyBookingButton.Enabled = false;
            }
        }

        private void retiredCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( retiredCombo.SelectedIndex == 1)
            {
                modifyBookingButton.Enabled = false;
            }
            else
            {
                modifyBookingButton.Enabled = true;
            }
        }
    }
}
