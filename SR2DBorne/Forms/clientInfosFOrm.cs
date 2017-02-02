using CGest.Database;
using CGest.Network;
using CGest.Utilities;
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
    public partial class clientInfosForm : Form
    {
        clientForm cF;

        CGestClassBound cB;
        CGestDatabase db;
        CGestNetworkClientControl networkControl;

        DataRow dr;

        public clientInfosForm(CGestClassBound cB, clientForm cF, DataRow dr)
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
            addrTextbox.Text = dr.ItemArray[3].ToString();
            postalTextbox.Text = dr.ItemArray[4].ToString();
            cityTextbox.Text = dr.ItemArray[5].ToString();
            relTextbox.Text = dr.ItemArray[6].ToString();
            infosTextbox.Text = dr.ItemArray[7].ToString();
            fixeTelTextbox.Text = dr.ItemArray[8].ToString();
            mobilTelTextbox.Text = dr.ItemArray[9].ToString();
            mailTextbox.Text = dr.ItemArray[10].ToString();
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
