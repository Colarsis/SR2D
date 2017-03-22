using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Colarsis/SR2D/blob/prod/LICENSE");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;

            Form1 f1 = new Form1(this);

            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;

            Form2 f2 = new Form2(this);

            f2.Show();
        }
    }
}
