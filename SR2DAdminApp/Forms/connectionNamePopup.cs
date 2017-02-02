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
    public partial class connectionNamePopup : Form
    {
        public connectionNamePopup()
        {
            InitializeComponent();

            validateButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
        }

        private void validateButton_Click(object sender, EventArgs e)
        {
            
        }

        

    }
}
