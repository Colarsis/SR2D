using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGestServer.Database;
using System.Security.Cryptography;

namespace CGestServer.Forms
{
    public partial class userAddForm : Form
    {
        usersForm uF;
        CGestDatabase db;

        public userAddForm(usersForm uF, CGestDatabase db )
        {
            InitializeComponent();

            this.uF = uF;
            this.db = db;
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

        private void colorButton_Click(object sender, EventArgs e)
        {
            colorPicker cP = new colorPicker(this);

            DialogResult dr = cP.ShowDialog();

            if (dr == DialogResult.OK)
            {
                colorPanel.BackColor = cP.webColorPicker1.SelectedColor;
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

        private void saveButton_Click(object sender, EventArgs e)
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

            DataRow newRow = db.Ds.Dataset.Tables["user"].NewRow();

            newRow["id_user"] = getNextID("user");
            newRow["username"] = nameTextbox.Text;
            newRow["password"] = HashSHA1(passwordTextbox.Text);
            newRow["last_connection"] = DateTime.Now.Day + "/"  + DateTime.Now.Month + "/" + DateTime.Now.Year + "H" + DateTime.Now.Hour + ":" + DateTime.Now.Minute;
            newRow["color"] = colorPanel.BackColor.Name;
            newRow["priority"] = priorityNum.Value;

            Console.WriteLine(priorityNum.Value);
            Console.WriteLine(newRow.ItemArray[5]);

            db.Ds.Dataset.Tables["user"].Rows.Add(newRow);

            db.Ds.updateDatabase();

            this.Close();
        }

    }
}
