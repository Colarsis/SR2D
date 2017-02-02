using CGest.Database;
using CGest.Utilities;
using CGest.Network;
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
using ColarsisUserControls.AdvancedTreeView;

namespace CGest.Forms
{
    public partial class MainForm : Form
    {
        CGestNetworkClientControl networkControler;
        CGestDatabase database;
        CGestClassBound cB;

        initForm iF;

        public MainForm(CGestClassBound cB, initForm iF)
        {
            InitializeComponent();

            this.cB = cB;

            this.database = cB.db;
            this.networkControler = cB.networkControler;

            this.iF = iF;

            Node n1 = new Node("Tâches (0)", null);
            Node n2 = new Node("Rendez-vous (3)", null);
            Node n3 = new Node("Rappels (2)", null);

            TreeNode t1 = new TreeNode();
            TreeNode t2 = new TreeNode();
            TreeNode t3 = new TreeNode();
            TreeNode t4 = new TreeNode();
            TreeNode t5 = new TreeNode();

            t1.Text = "Mr Dupont à 14h30";
            t2.Text = "Mr Maris à 15h30";
            t3.Text = "Mr Talon à 17h00";
            t4.Text = "Aller chercher le courrier à 14h00";
            t5.Text = "Appeler Mlle Priste à 16h00";

            n2.addNode(t1);
            n2.addNode(t2);
            n2.addNode(t3);

            n3.addNode(t4);
            n3.addNode(t5);

            advancedTreeView1.addNode(n1);
            advancedTreeView1.addNode(n2);
            advancedTreeView1.addNode(n3);
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

        private void stockButton_Click(object sender, EventArgs e)
        {
            stockForm stock = new stockForm(cB, this);

            stock.Show();

            setButtonEnabling(stockButton, false);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            panel1.Width = this.Width - 235;
            panel1.Height = this.Height - 139;
            label2.Left = panel1.Width / 2 - 124; 
            label2.Top = panel1.Height / 2 - 50;
            label3.Left = panel1.Width / 2 + 31;
            label3.Top = panel1.Height / 2 - 18;
            advancedTreeView1.Height = this.Height - 159;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            iF.Close();
            Environment.Exit(0);
        }

        private void clientStripButton_Click(object sender, EventArgs e)
        {
            clientForm cF = new clientForm(this, cB);

            cF.Show();

            setButtonEnabling(clientsBtn, false);
        }

        private void chantierToolstripButton_Click(object sender, EventArgs e)
        {
            chantierForm chF = new chantierForm(this, cB);

            chF.Show();


            setButtonEnabling(chantierBtn, false);
        }

        private void materiauxBtn_Click(object sender, EventArgs e)
        {
            materiauxForm matF = new materiauxForm(this, cB);

            matF.Show();

            setButtonEnabling(materiauxBtn, false);
        }

        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void calendarButton_Click(object sender, EventArgs e)
        {
            eventCalendarForm eCF = new eventCalendarForm(this, cB);

            eCF.Show();

            setButtonEnabling(calendarButton, false);
        }
    }
}
