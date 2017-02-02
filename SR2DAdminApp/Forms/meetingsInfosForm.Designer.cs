namespace SR2DAdminApp.Forms
{
    partial class meetingsInfosForm
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
            this.chantierFileButton = new System.Windows.Forms.Button();
            this.clientFileButton = new System.Windows.Forms.Button();
            this.chantierGroupbox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clientGroupbox = new System.Windows.Forms.GroupBox();
            this.cityLabel = new System.Windows.Forms.Label();
            this.postalLabel = new System.Windows.Forms.Label();
            this.addrLabel = new System.Windows.Forms.Label();
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.dateLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.descTextbox = new System.Windows.Forms.TextBox();
            this.descLabel = new System.Windows.Forms.Label();
            this.reportLabel = new System.Windows.Forms.Label();
            this.reportTextbox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chantierGroupbox.SuspendLayout();
            this.clientGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // chantierFileButton
            // 
            this.chantierFileButton.Location = new System.Drawing.Point(246, 265);
            this.chantierFileButton.Name = "chantierFileButton";
            this.chantierFileButton.Size = new System.Drawing.Size(230, 30);
            this.chantierFileButton.TabIndex = 8;
            this.chantierFileButton.Text = "Fiche chantier";
            this.chantierFileButton.UseVisualStyleBackColor = true;
            // 
            // clientFileButton
            // 
            this.clientFileButton.Location = new System.Drawing.Point(12, 265);
            this.clientFileButton.Name = "clientFileButton";
            this.clientFileButton.Size = new System.Drawing.Size(229, 30);
            this.clientFileButton.TabIndex = 7;
            this.clientFileButton.Text = "Fiche client";
            this.clientFileButton.UseVisualStyleBackColor = true;
            // 
            // chantierGroupbox
            // 
            this.chantierGroupbox.Controls.Add(this.label1);
            this.chantierGroupbox.Location = new System.Drawing.Point(12, 200);
            this.chantierGroupbox.Name = "chantierGroupbox";
            this.chantierGroupbox.Size = new System.Drawing.Size(465, 58);
            this.chantierGroupbox.TabIndex = 6;
            this.chantierGroupbox.TabStop = false;
            this.chantierGroupbox.Text = "Chantier";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "name";
            // 
            // clientGroupbox
            // 
            this.clientGroupbox.Controls.Add(this.cityLabel);
            this.clientGroupbox.Controls.Add(this.postalLabel);
            this.clientGroupbox.Controls.Add(this.addrLabel);
            this.clientGroupbox.Controls.Add(this.clientNameLabel);
            this.clientGroupbox.Location = new System.Drawing.Point(12, 79);
            this.clientGroupbox.Name = "clientGroupbox";
            this.clientGroupbox.Size = new System.Drawing.Size(465, 115);
            this.clientGroupbox.TabIndex = 5;
            this.clientGroupbox.TabStop = false;
            this.clientGroupbox.Text = "Client";
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Location = new System.Drawing.Point(61, 83);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(25, 13);
            this.cityLabel.TabIndex = 3;
            this.cityLabel.Text = "ville";
            // 
            // postalLabel
            // 
            this.postalLabel.AutoSize = true;
            this.postalLabel.Location = new System.Drawing.Point(18, 83);
            this.postalLabel.Name = "postalLabel";
            this.postalLabel.Size = new System.Drawing.Size(37, 13);
            this.postalLabel.TabIndex = 2;
            this.postalLabel.Text = "00000";
            // 
            // addrLabel
            // 
            this.addrLabel.AutoSize = true;
            this.addrLabel.Location = new System.Drawing.Point(18, 58);
            this.addrLabel.Name = "addrLabel";
            this.addrLabel.Size = new System.Drawing.Size(44, 13);
            this.addrLabel.TabIndex = 1;
            this.addrLabel.Text = "adresse";
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.AutoSize = true;
            this.clientNameLabel.Location = new System.Drawing.Point(18, 25);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(33, 13);
            this.clientNameLabel.TabIndex = 0;
            this.clientNameLabel.Text = "name";
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Location = new System.Drawing.Point(14, 320);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(51, 13);
            this.dateLabel.TabIndex = 9;
            this.dateLabel.Text = "Le *date*";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(14, 357);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(46, 13);
            this.statusLabel.TabIndex = 10;
            this.statusLabel.Text = "Status : ";
            // 
            // descTextbox
            // 
            this.descTextbox.Location = new System.Drawing.Point(76, 396);
            this.descTextbox.Multiline = true;
            this.descTextbox.Name = "descTextbox";
            this.descTextbox.ReadOnly = true;
            this.descTextbox.Size = new System.Drawing.Size(401, 107);
            this.descTextbox.TabIndex = 39;
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(14, 399);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(60, 13);
            this.descLabel.TabIndex = 40;
            this.descLabel.Text = "Description";
            // 
            // reportLabel
            // 
            this.reportLabel.AutoSize = true;
            this.reportLabel.Location = new System.Drawing.Point(29, 512);
            this.reportLabel.Name = "reportLabel";
            this.reportLabel.Size = new System.Drawing.Size(45, 13);
            this.reportLabel.TabIndex = 42;
            this.reportLabel.Text = "Rapport";
            // 
            // reportTextbox
            // 
            this.reportTextbox.Location = new System.Drawing.Point(76, 509);
            this.reportTextbox.Multiline = true;
            this.reportTextbox.Name = "reportTextbox";
            this.reportTextbox.ReadOnly = true;
            this.reportTextbox.Size = new System.Drawing.Size(401, 107);
            this.reportTextbox.TabIndex = 41;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(14, 20);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 43;
            this.nameLabel.Text = "Nom : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Interlocuteur : ";
            // 
            // meetingsInfosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 627);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.reportLabel);
            this.Controls.Add(this.reportTextbox);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.descTextbox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.chantierFileButton);
            this.Controls.Add(this.clientFileButton);
            this.Controls.Add(this.chantierGroupbox);
            this.Controls.Add(this.clientGroupbox);
            this.MaximumSize = new System.Drawing.Size(505, 665);
            this.MinimumSize = new System.Drawing.Size(505, 665);
            this.Name = "meetingsInfosForm";
            this.Text = "Informations du rendez-vous";
            this.chantierGroupbox.ResumeLayout(false);
            this.chantierGroupbox.PerformLayout();
            this.clientGroupbox.ResumeLayout(false);
            this.clientGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button chantierFileButton;
        private System.Windows.Forms.Button clientFileButton;
        private System.Windows.Forms.GroupBox chantierGroupbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox clientGroupbox;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label postalLabel;
        private System.Windows.Forms.Label addrLabel;
        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TextBox descTextbox;
        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label reportLabel;
        private System.Windows.Forms.TextBox reportTextbox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label label2;
    }
}