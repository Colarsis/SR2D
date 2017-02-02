using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGestServer.Forms
{
    public partial class colorPicker : Form
    {
        userAddForm mF;

        public colorPicker(userAddForm mF)
        {
            InitializeComponent();

            this.mF = mF;

            okButton.DialogResult = DialogResult.OK;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void colorPicker_FormClosed(object sender, FormClosedEventArgs e)
        {
            mF.Enabled = true;
        }
    }
}
