using CGestServer.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CGestServer
{
    public partial class mainForm : Form
    {
        public Dictionary<EndPoint, string> clientsDictionary = new Dictionary<EndPoint, string>();

        public volatile bool listenerWork = true;

        Thread listenerThread;

        CGestListener listener;

        bool hidden = false;

        public mainForm()
        {
            InitializeComponent();

            initGraphics();

            initServer();
        }

        public void initServer()
        {
            listenerWork = true;

            listener = new CGestListener(this);
        }

        public void initGraphics()
        {
            notifyIcon.Icon = this.Icon;
        }

        public void writeConsole(string text)
        {
            textBox1.AppendText("\r\n [" + DateTime.Now.ToString("dd/MM/yyyy h:mm TT") + "] " + text);
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            notifyIcon.Visible = true;

            hidden = true;
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (hidden)
            {
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;

                notifyIcon.Visible = false;
            }
        }

        private void quitterLapplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listenerWork = false;

            listener.stopListener();

            Application.Exit();
        }

        private void ouvrirLaConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hidden)
            {
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;

                notifyIcon.Visible = false;
            }
        }

        private void stopServeurButton_Click(object sender, EventArgs e)
        {
            listenerWork = false;

            listener.stopListener();

            stopServeurButton.Enabled = false;

            startServerButton.Enabled = true;
        }

        private void startServerButton_Click(object sender, EventArgs e)
        {
            initServer();

            stopServeurButton.Enabled = true;

            startServerButton.Enabled = false;
        }

    }
}
