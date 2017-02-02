using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SR2DServer.Database;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace SR2DServer.Forms
{
    public partial class userAddForm : Form
    {
        usersForm uF;
        SR2DDatabase db;

        public userAddForm(usersForm uF, SR2DDatabase db )
        {
            InitializeComponent();

            this.uF = uF;
            this.db = db;

            getRanks();
        }

        //Function used to get ranks
        public void getRanks()
        {
            MySqlCommand cmd = new MySqlCommand("select * from ranks", db.Connection.DatabaseConnection);

            MySqlDataReader r = cmd.ExecuteReader();

            try
            {
                while(r.Read())
                {
                    rankCombo.Items.Add(r.GetInt16(0) + " - " + r.GetString(2));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                r.Close();
            }
        }

        public string HashSHA1(string sInputString)
        {
            SHA1 sha = SHA1.Create();
            byte[] bHash = sha.ComputeHash(Encoding.UTF8.GetBytes(sInputString));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < bHash.Length; i++)
            {
                sBuilder.Append(bHash[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public string getNextID(string table)
        {
            switch (table)
            {
                case "user":
                    DataRowCollection drC = db.Ds.Dataset.Tables["user"].Rows;

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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userAddForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            uF.Enabled = true;
        }

        private void saveButton_Click(object sender, EventArgs e) //Save the new user into the database
        {
            if (nameTextbox.Text == "")
            {
                DialogResult dr = MessageBox.Show("Veuillez entrer un nom d'utilisateur correct !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRowCollection drC = db.Ds.Dataset.Tables["user"].Rows;

            foreach (DataRow dr in drC)
            {
                if (dr.ItemArray[1].ToString() == nameTextbox.Text)
                {
                    DialogResult dre = MessageBox.Show("Le nom d'utilisateur spécifié est déjà utilisé! Veuillez en utiliser un autre !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
            }

            if (passwordTextbox.Text == "")
            {
                DialogResult dr = MessageBox.Show("Veuillez entrer un mot de passe correct !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (rankCombo.SelectedItem.ToString() == "")
            {
                DialogResult dr = MessageBox.Show("Veuillez choisir un rang valide !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DataRow newRow = db.Ds.Dataset.Tables["user"].NewRow();

            newRow["id"] = getNextID("user");
            newRow["name"] = nameTextbox.Text;
            newRow["password"] = HashSHA1(passwordTextbox.Text);
            newRow["rank"] = int.Parse(rankCombo.SelectedItem.ToString().Split(new Char[] { ' ', '-' })[0]);

            db.Ds.Dataset.Tables["user"].Rows.Add(newRow);

            db.Ds.updateDatabase();

            this.Close();
        }

    }
}
