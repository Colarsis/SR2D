using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests
{
    public partial class Form2 : Form
    {

        Main mF;

        int connectionsNbr = 0;

        List<Connection> connections = new List<Connection>();

        public delegate void connectionCountDelegate();

        public Form2(Main mF)
        {
            InitializeComponent();

            this.mF = mF;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            mF.button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackgroundWorker addThread = new BackgroundWorker();

            addThread.DoWork += new DoWorkEventHandler(runWorker);
            addThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(addingComplete);

            this.Enabled = false;

            addThread.RunWorkerAsync();
            
        }

        private void runWorker(object sender, DoWorkEventArgs e)
        {
            addingControler((BackgroundWorker)sender);
        }

        private void addingComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
        }

        private void updateCounter()
        {
            if(label1.InvokeRequired)
            {
                connectionCountDelegate c = new connectionCountDelegate(uC);
                Invoke(c);
            }
            else
            {
                uC();
            }
        }

        private void uC()
        {
            label1.Text = "Nbr connections: " + connectionsNbr;
        }

        public void addingControler(BackgroundWorker sender)
        {
            for(int i = 0; i < numericUpDown1.Value; i++)
            {
                Connection co = new Connection(textBox3.Text);

                if(co.start())
                {
                    if(co.connect(textBox1.Text, textBox2.Text))
                    {
                        connections.Add(co);

                        connectionsNbr++;

                        updateCounter();
                    }
                }

                Thread.Sleep(new TimeSpan((long)(numericUpDown2.Value * 1000)));
            }

        }
    }
}
