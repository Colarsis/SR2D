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
    public partial class serverConnectForm : Form
    {
        public bool error = false; 

        public serverConnectForm()
        {
            InitializeComponent();

            connectButton.DialogResult = DialogResult.OK;
        }

        public string getUsername()
        {
            return usernameTextbox.Text;
        }

        public string getPassword()
        {
            return passwordTextbox.Text;
        }

        private void serverConnectForm_Shown(object sender, EventArgs e)
        {
            if (error)
            {
                label3.Text = "Infomations incorrectes !";
                label3.Visible = true;
            }
            else
            {
                label3.Visible = false;
            }
        }
    }
}
