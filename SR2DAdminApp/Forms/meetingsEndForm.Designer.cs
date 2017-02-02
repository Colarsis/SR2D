namespace SR2DAdminApp.Forms
{
    partial class meetingsEndForm
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
            this.clientNameLabel = new System.Windows.Forms.Label();
            this.clientGroupbox = new System.Windows.Forms.GroupBox();
            this.addrLabel = new System.Windows.Forms.Label();
            this.postalLabel = new System.Windows.Forms.Label();
            this.cityLabel = new System.Windows.Forms.Label();
            this.chantierGroupbox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clientFileButton = new System.Windows.Forms.Button();
            this.chantierFileButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.reportTextbox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.clientGroupbox.SuspendLayout();
            this.chantierGroupbox.SuspendLayout();
            this.SuspendLayout();
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
            // clientGroupbox
            // 
            this.clientGroupbox.Controls.Add(this.cityLabel);
            this.clientGroupbox.Controls.Add(this.postalLabel);
            this.clientGroupbox.Controls.Add(this.addrLabel);
            this.clientGroupbox.Controls.Add(this.clientNameLabel);
            this.clientGroupbox.Location = new System.Drawing.Point(12, 12);
            this.clientGroupbox.Name = "clientGroupbox";
            this.clientGroupbox.Size = new System.Drawing.Size(465, 115);
            this.clientGroupbox.TabIndex = 1;
            this.clientGroupbox.TabStop = false;
            this.clientGroupbox.Text = "Client";
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
            // postalLabel
            // 
            this.postalLabel.AutoSize = true;
            this.postalLabel.Location = new System.Drawing.Point(18, 83);
            this.postalLabel.Name = "postalLabel";
            this.postalLabel.Size = new System.Drawing.Size(37, 13);
            this.postalLabel.TabIndex = 2;
            this.postalLabel.Text = "00000";
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
            // chantierGroupbox
            // 
            this.chantierGroupbox.Controls.Add(this.label1);
            this.chantierGroupbox.Location = new System.Drawing.Point(12, 133);
            this.chantierGroupbox.Name = "chantierGroupbox";
            this.chantierGroupbox.Size = new System.Drawing.Size(465, 58);
            this.chantierGroupbox.TabIndex = 2;
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
            // clientFileButton
            // 
            this.clientFileButton.Location = new System.Drawing.Point(12, 198);
            this.clientFileButton.Name = "clientFileButton";
            this.clientFileButton.Size = new System.Drawing.Size(229, 30);
            this.clientFileButton.TabIndex = 3;
            this.clientFileButton.Text = "Fiche client";
            this.clientFileButton.UseVisualStyleBackColor = true;
            // 
            // chantierFileButton
            // 
            this.chantierFileButton.Location = new System.Drawing.Point(246, 198);
            this.chantierFileButton.Name = "chantierFileButton";
            this.chantierFileButton.Size = new System.Drawing.Size(230, 30);
            this.chantierFileButton.TabIndex = 4;
            this.chantierFileButton.Text = "Fiche chantier";
            this.chantierFileButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 256);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 39;
            this.label7.Text = "Rapport";
            // 
            // reportTextbox
            // 
            this.reportTextbox.Location = new System.Drawing.Point(63, 256);
            this.reportTextbox.Multiline = true;
            this.reportTextbox.Name = "reportTextbox";
            this.reportTextbox.Size = new System.Drawing.Size(414, 145);
            this.reportTextbox.TabIndex = 38;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(375, 414);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(102, 34);
            this.cancelButton.TabIndex = 41;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(267, 414);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 34);
            this.saveButton.TabIndex = 40;
            this.saveButton.Text = "Valider";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // meetingsEndForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 460);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.reportTextbox);
            this.Controls.Add(this.chantierFileButton);
            this.Controls.Add(this.clientFileButton);
            this.Controls.Add(this.chantierGroupbox);
            this.Controls.Add(this.clientGroupbox);
            this.Name = "meetingsEndForm";
            this.Text = "Compte-rendu du rendez-vous du *date*";
            this.clientGroupbox.ResumeLayout(false);
            this.clientGroupbox.PerformLayout();
            this.chantierGroupbox.ResumeLayout(false);
            this.chantierGroupbox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clientNameLabel;
        private System.Windows.Forms.GroupBox clientGroupbox;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.Label postalLabel;
        private System.Windows.Forms.Label addrLabel;
        private System.Windows.Forms.GroupBox chantierGroupbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clientFileButton;
        private System.Windows.Forms.Button chantierFileButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox reportTextbox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
    }
}