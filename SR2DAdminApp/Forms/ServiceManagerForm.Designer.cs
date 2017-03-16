namespace SR2DAdminApp.Forms
{
    partial class ServiceManagerForm
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
            this.actionButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.id = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.type = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.quantity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cartesianChart2 = new LiveCharts.WinForms.CartesianChart();
            this.label1 = new System.Windows.Forms.Label();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.prepButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.objectListView2 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).BeginInit();
            this.SuspendLayout();
            // 
            // actionButton
            // 
            this.actionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.actionButton.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.actionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.actionButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actionButton.Location = new System.Drawing.Point(12, 584);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(560, 65);
            this.actionButton.TabIndex = 0;
            this.actionButton.Text = "TERMINER LE SERVICE";
            this.actionButton.UseVisualStyleBackColor = false;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 551);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.objectListView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(552, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Disponibilités";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(75, 248);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(402, 29);
            this.label5.TabIndex = 5;
            this.label5.Text = "Service non disponible actuellement";
            this.label5.Visible = false;
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.id);
            this.objectListView1.AllColumns.Add(this.name);
            this.objectListView1.AllColumns.Add(this.type);
            this.objectListView1.AllColumns.Add(this.quantity);
            this.objectListView1.CellEditUseWholeCell = false;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.id,
            this.name,
            this.type,
            this.quantity});
            this.objectListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.GridLines = true;
            this.objectListView1.Location = new System.Drawing.Point(6, 6);
            this.objectListView1.MultiSelect = false;
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(540, 513);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            // 
            // id
            // 
            this.id.AspectName = "id";
            this.id.Groupable = false;
            this.id.IsEditable = false;
            this.id.IsVisible = false;
            this.id.MaximumWidth = 0;
            this.id.MinimumWidth = 0;
            this.id.Searchable = false;
            this.id.Text = "id";
            this.id.Width = 0;
            // 
            // name
            // 
            this.name.AspectName = "Name";
            this.name.FillsFreeSpace = true;
            this.name.Groupable = false;
            this.name.IsEditable = false;
            this.name.Text = "Produit";
            // 
            // type
            // 
            this.type.AspectName = "type";
            this.type.IsEditable = false;
            this.type.MaximumWidth = 0;
            this.type.MinimumWidth = 0;
            this.type.Text = "type";
            this.type.Width = 0;
            // 
            // quantity
            // 
            this.quantity.AspectName = "quantity";
            this.quantity.CellEditUseWholeCell = true;
            this.quantity.FillsFreeSpace = true;
            this.quantity.Groupable = false;
            this.quantity.Text = "Quantité";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.cartesianChart2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cartesianChart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(552, 525);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Suivi";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(402, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Service non disponible actuellement";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Réservations retirées";
            // 
            // cartesianChart2
            // 
            this.cartesianChart2.Location = new System.Drawing.Point(6, 296);
            this.cartesianChart2.Name = "cartesianChart2";
            this.cartesianChart2.Size = new System.Drawing.Size(540, 223);
            this.cartesianChart2.TabIndex = 2;
            this.cartesianChart2.Text = "cartesianChart2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Réservations effectués";
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(6, 46);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(540, 223);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.prepButton);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.objectListView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(552, 525);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Préparation";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // prepButton
            // 
            this.prepButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prepButton.Location = new System.Drawing.Point(0, 496);
            this.prepButton.Name = "prepButton";
            this.prepButton.Size = new System.Drawing.Size(552, 23);
            this.prepButton.TabIndex = 2;
            this.prepButton.Text = "Valider un préparation";
            this.prepButton.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(402, 29);
            this.label4.TabIndex = 5;
            this.label4.Text = "Service non disponible actuellement";
            this.label4.Visible = false;
            // 
            // objectListView2
            // 
            this.objectListView2.AllColumns.Add(this.olvColumn1);
            this.objectListView2.AllColumns.Add(this.olvColumn2);
            this.objectListView2.AllColumns.Add(this.olvColumn7);
            this.objectListView2.AllColumns.Add(this.olvColumn3);
            this.objectListView2.AllColumns.Add(this.olvColumn4);
            this.objectListView2.AllColumns.Add(this.olvColumn5);
            this.objectListView2.AllColumns.Add(this.olvColumn6);
            this.objectListView2.CellEditUseWholeCell = false;
            this.objectListView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn7,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6});
            this.objectListView2.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView2.FullRowSelect = true;
            this.objectListView2.GridLines = true;
            this.objectListView2.Location = new System.Drawing.Point(0, 3);
            this.objectListView2.MultiSelect = false;
            this.objectListView2.Name = "objectListView2";
            this.objectListView2.Size = new System.Drawing.Size(552, 487);
            this.objectListView2.TabIndex = 3;
            this.objectListView2.UseCompatibleStateImageBehavior = false;
            this.objectListView2.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "id";
            this.olvColumn1.Groupable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsVisible = false;
            this.olvColumn1.MaximumWidth = 0;
            this.olvColumn1.MinimumWidth = 0;
            this.olvColumn1.Searchable = false;
            this.olvColumn1.Text = "id";
            this.olvColumn1.Width = 0;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Groupable = false;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Produit";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "student";
            this.olvColumn7.FillsFreeSpace = true;
            this.olvColumn7.Groupable = false;
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.Text = "Eleve";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "type";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.MaximumWidth = 0;
            this.olvColumn3.MinimumWidth = 0;
            this.olvColumn3.Text = "type";
            this.olvColumn3.Width = 0;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "quantity";
            this.olvColumn4.CellEditUseWholeCell = true;
            this.olvColumn4.FillsFreeSpace = true;
            this.olvColumn4.Groupable = false;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "Quantité";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "state";
            this.olvColumn5.FillsFreeSpace = true;
            this.olvColumn5.Groupable = false;
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.Text = "Etat";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "supplement";
            this.olvColumn6.FillsFreeSpace = true;
            this.olvColumn6.Groupable = false;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Text = "Reste";
            // 
            // ServiceManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 662);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 700);
            this.Name = "ServiceManagerForm";
            this.Text = "ServiceManagerForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ServiceManagerForm_FormClosed);
            this.Resize += new System.EventHandler(this.ServiceManagerForm_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button actionButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label label2;
        private LiveCharts.WinForms.CartesianChart cartesianChart2;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn id;
        private BrightIdeasSoftware.OLVColumn name;
        private BrightIdeasSoftware.OLVColumn type;
        private BrightIdeasSoftware.OLVColumn quantity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private BrightIdeasSoftware.ObjectListView objectListView2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private System.Windows.Forms.Button prepButton;
    }
}