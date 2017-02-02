using SR2DAdminApp.Database;
using SR2DAdminApp.Utilities;
using SR2DAdminApp.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SR2DAdminApp.Forms
{
    public partial class MainForm : Form
    {
        SR2DNetworkClientControl networkControler;
        SR2DDatabase database;
        SR2DClassBound cB;

        public delegate void setServiceModeCallback();

        initForm iF;

        public MainForm(SR2DClassBound cB, initForm iF)
        {
            InitializeComponent();

            this.cB = cB;

            this.database = cB.db;
            this.networkControler = cB.networkControler;

            database.Ds.DatasetUpdatedEvent += new SR2DDataset.EventHandler(update);

            updateHUD();

            this.iF = iF;
        }

        public void updateHUD()
        {
            setServiceMode();
            userLabel.Text = "Bienvenue " + networkControler.username;

            if (cB.vars.realTimeDisplay)
            {
                RTDMenuItem.Checked = true;
                drawRTD();
            }
            else
            {
                RTDMenuItem.Checked = false;
            }

        }

        public void drawRTD()
        {
        }

        public void update()
        {
            if (this.InvokeRequired)
            {
                setServiceModeCallback c2 = new setServiceModeCallback(updateHUD);
                Invoke(c2);
            }
            else
            {
                updateHUD();
            }
        }

        public void setServiceMode()
        {
            if ((string)TablesUtilities.getVarFromId("1", database.Ds).ItemArray[2] == "1")
            {
                serviceStatusLabel.Text = "Etat du service: En cours";
                serviceStatusLabel.ForeColor = Color.Green;
            }
            else
            {
                serviceStatusLabel.Text = "Etat du service: Aucun service en cours";
                serviceStatusLabel.ForeColor = SystemColors.ControlText;
            }
        }

        public void setButtonEnabling(ToolStripButton b, bool s)
        {
            if (s)
            {
                b.Enabled = true;
            }
            else
            {
                b.Enabled = false;
            }
        }

        public void setButtonEnabling(Button b, bool s)
        {
            if (s)
            {
                b.Enabled = true;
            }
            else
            {
                b.Enabled = false;
            }
        }

        private void userButton_Click(object sender, EventArgs e)
        {
            userForm user = new userForm(this, cB);

            user.Show();

            setButtonEnabling(userButton, false);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            panel1.Width = this.Width;
            panel1.Height = this.Height - 139;
            label2.Left = panel1.Width / 2 - 124; 
            label2.Top = panel1.Height / 2 - 50;
            label3.Left = panel1.Width / 2 + 31;
            label3.Top = panel1.Height / 2 - 18;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            iF.Close();
            Environment.Exit(0);
        }

        private void foodToolstripButton_Click(object sender, EventArgs e)
        {
            foodForm food = new foodForm(this, cB);

            food.Show();


            setButtonEnabling(foodButton, false);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void RTDMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if(sender != this)
            {
                if(RTDMenuItem.Checked)
                {
                    cB.vars.setOption("RTD", true);
                }
                else
                {
                    cB.vars.setOption("RTD", false);
                }
            }
        }

        private void dayGestButton_Click(object sender, EventArgs e)
        {
            ServiceManagerForm food = new ServiceManagerForm(this, cB);

            food.Show();

            setButtonEnabling(dayGestButton, false);
        }
    }
}
