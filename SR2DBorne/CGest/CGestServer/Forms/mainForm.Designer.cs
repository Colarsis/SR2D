namespace CGestServer
{
    partial class mainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ouvrirLaConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterLapplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServeurButton = new System.Windows.Forms.Button();
            this.startServerButton = new System.Windows.Forms.Button();
            this.seeUserButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIcon.BalloonTipText = "yyyyy";
            this.notifyIcon.BalloonTipTitle = "8888";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirLaConsoleToolStripMenuItem,
            this.quitterLapplicationToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(180, 48);
            // 
            // ouvrirLaConsoleToolStripMenuItem
            // 
            this.ouvrirLaConsoleToolStripMenuItem.Name = "ouvrirLaConsoleToolStripMenuItem";
            this.ouvrirLaConsoleToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.ouvrirLaConsoleToolStripMenuItem.Text = "Ouvrir la console";
            this.ouvrirLaConsoleToolStripMenuItem.Click += new System.EventHandler(this.ouvrirLaConsoleToolStripMenuItem_Click);
            // 
            // quitterLapplicationToolStripMenuItem
            // 
            this.quitterLapplicationToolStripMenuItem.Name = "quitterLapplicationToolStripMenuItem";
            this.quitterLapplicationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.quitterLapplicationToolStripMenuItem.Text = "Quitter l\'application";
            this.quitterLapplicationToolStripMenuItem.Click += new System.EventHandler(this.quitterLapplicationToolStripMenuItem_Click);
            // 
            // stopServeurButton
            // 
            this.stopServeurButton.Location = new System.Drawing.Point(731, 13);
            this.stopServeurButton.Name = "stopServeurButton";
            this.stopServeurButton.Size = new System.Drawing.Size(163, 51);
            this.stopServeurButton.TabIndex = 1;
            this.stopServeurButton.Text = "Arreter le serveur";
            this.stopServeurButton.UseVisualStyleBackColor = true;
            this.stopServeurButton.Click += new System.EventHandler(this.stopServeurButton_Click);
            // 
            // startServerButton
            // 
            this.startServerButton.Enabled = false;
            this.startServerButton.Location = new System.Drawing.Point(731, 70);
            this.startServerButton.Name = "startServerButton";
            this.startServerButton.Size = new System.Drawing.Size(163, 51);
            this.startServerButton.TabIndex = 2;
            this.startServerButton.Text = "Démarrer le serveur";
            this.startServerButton.UseVisualStyleBackColor = true;
            this.startServerButton.Click += new System.EventHandler(this.startServerButton_Click);
            // 
            // seeUserButton
            // 
            this.seeUserButton.Location = new System.Drawing.Point(730, 156);
            this.seeUserButton.Name = "seeUserButton";
            this.seeUserButton.Size = new System.Drawing.Size(163, 51);
            this.seeUserButton.TabIndex = 3;
            this.seeUserButton.Text = "Utilisateurs";
            this.seeUserButton.UseVisualStyleBackColor = true;
            this.seeUserButton.Click += new System.EventHandler(this.seeUserButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.MaxLength = 0;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(713, 446);
            this.textBox1.TabIndex = 4;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 470);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.seeUserButton);
            this.Controls.Add(this.startServerButton);
            this.Controls.Add(this.stopServeurButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ouvrirLaConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterLapplicationToolStripMenuItem;
        private System.Windows.Forms.Button stopServeurButton;
        private System.Windows.Forms.Button startServerButton;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button seeUserButton;
    }
}

