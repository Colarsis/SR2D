namespace CGest.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvergarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seConnecterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rrrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.affichageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outilsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockButton = new System.Windows.Forms.ToolStripButton();
            this.chantierBtn = new System.Windows.Forms.ToolStripButton();
            this.materiauxBtn = new System.Windows.Forms.ToolStripButton();
            this.fournBtn = new System.Windows.Forms.ToolStripButton();
            this.clientsBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.calendarButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.advancedTreeView1 = new ColarsisUserControls.AdvancedTreeView.AdvancedTreeView();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(238, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 397);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(428, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 30);
            this.label3.TabIndex = 5;
            this.label3.Text = "by Colarsis";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.label2.Location = new System.Drawing.Point(273, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 71);
            this.label2.TabIndex = 4;
            this.label2.Text = "CGest ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.editionToolStripMenuItem,
            this.affichageToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.outilsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1022, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sauvergarderToolStripMenuItem,
            this.seConnecterToolStripMenuItem,
            this.rrrToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // sauvergarderToolStripMenuItem
            // 
            this.sauvergarderToolStripMenuItem.Name = "sauvergarderToolStripMenuItem";
            this.sauvergarderToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.sauvergarderToolStripMenuItem.Text = "Sauvegarder";
            // 
            // seConnecterToolStripMenuItem
            // 
            this.seConnecterToolStripMenuItem.Name = "seConnecterToolStripMenuItem";
            this.seConnecterToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.seConnecterToolStripMenuItem.Text = "Se connecter";
            // 
            // rrrToolStripMenuItem
            // 
            this.rrrToolStripMenuItem.Name = "rrrToolStripMenuItem";
            this.rrrToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.rrrToolStripMenuItem.Text = "Se déconnecter";
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            // 
            // editionToolStripMenuItem
            // 
            this.editionToolStripMenuItem.Name = "editionToolStripMenuItem";
            this.editionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.editionToolStripMenuItem.Text = "Edition";
            // 
            // affichageToolStripMenuItem
            // 
            this.affichageToolStripMenuItem.Name = "affichageToolStripMenuItem";
            this.affichageToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.affichageToolStripMenuItem.Text = "Affichage";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // outilsToolStripMenuItem
            // 
            this.outilsToolStripMenuItem.Name = "outilsToolStripMenuItem";
            this.outilsToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.outilsToolStripMenuItem.Text = "Outils";
            // 
            // stockButton
            // 
            this.stockButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stockButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.stockButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stockButton.Name = "stockButton";
            this.stockButton.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.stockButton.Size = new System.Drawing.Size(40, 65);
            this.stockButton.Text = "Stock";
            this.stockButton.Click += new System.EventHandler(this.stockButton_Click);
            // 
            // chantierBtn
            // 
            this.chantierBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.chantierBtn.Image = ((System.Drawing.Image)(resources.GetObject("chantierBtn.Image")));
            this.chantierBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chantierBtn.Name = "chantierBtn";
            this.chantierBtn.Size = new System.Drawing.Size(56, 65);
            this.chantierBtn.Text = "Chantier";
            this.chantierBtn.ToolTipText = "Chantier";
            this.chantierBtn.Click += new System.EventHandler(this.chantierToolstripButton_Click);
            // 
            // materiauxBtn
            // 
            this.materiauxBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.materiauxBtn.Image = ((System.Drawing.Image)(resources.GetObject("materiauxBtn.Image")));
            this.materiauxBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.materiauxBtn.Name = "materiauxBtn";
            this.materiauxBtn.Size = new System.Drawing.Size(63, 65);
            this.materiauxBtn.Text = "Materiaux";
            this.materiauxBtn.Click += new System.EventHandler(this.materiauxBtn_Click);
            // 
            // fournBtn
            // 
            this.fournBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fournBtn.Image = ((System.Drawing.Image)(resources.GetObject("fournBtn.Image")));
            this.fournBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fournBtn.Name = "fournBtn";
            this.fournBtn.Size = new System.Drawing.Size(77, 65);
            this.fournBtn.Text = "Fournisseurs";
            // 
            // clientsBtn
            // 
            this.clientsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clientsBtn.Image = ((System.Drawing.Image)(resources.GetObject("clientsBtn.Image")));
            this.clientsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clientsBtn.Name = "clientsBtn";
            this.clientsBtn.Size = new System.Drawing.Size(47, 65);
            this.clientsBtn.Text = "Clients";
            this.clientsBtn.Click += new System.EventHandler(this.clientStripButton_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stockButton,
            this.chantierBtn,
            this.materiauxBtn,
            this.fournBtn,
            this.clientsBtn,
            this.calendarButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1022, 68);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // calendarButton
            // 
            this.calendarButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.calendarButton.Image = ((System.Drawing.Image)(resources.GetObject("calendarButton.Image")));
            this.calendarButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.calendarButton.Name = "calendarButton";
            this.calendarButton.Size = new System.Drawing.Size(65, 65);
            this.calendarButton.Text = "Calendrier";
            this.calendarButton.Click += new System.EventHandler(this.calendarButton_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Location = new System.Drawing.Point(0, 495);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1022, 25);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // advancedTreeView1
            // 
            this.advancedTreeView1.AutoScroll = true;
            this.advancedTreeView1.Location = new System.Drawing.Point(0, 95);
            this.advancedTreeView1.MinimumSize = new System.Drawing.Size(180, 100);
            this.advancedTreeView1.Name = "advancedTreeView1";
            this.advancedTreeView1.Size = new System.Drawing.Size(235, 397);
            this.advancedTreeView1.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 520);
            this.Controls.Add(this.advancedTreeView1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1038, 558);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CGest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rrrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem affichageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outilsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvergarderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seConnecterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ToolStripButton stockButton;
        public System.Windows.Forms.ToolStripButton chantierBtn;
        public System.Windows.Forms.ToolStripButton materiauxBtn;
        public System.Windows.Forms.ToolStripButton fournBtn;
        public System.Windows.Forms.ToolStripButton clientsBtn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.ToolStripButton calendarButton;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private ColarsisUserControls.AdvancedTreeView.AdvancedTreeView advancedTreeView1;
    }
}