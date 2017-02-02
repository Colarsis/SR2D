namespace CGest.Forms
{
    partial class stockModifyForm
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
            this.matComboBox = new System.Windows.Forms.ComboBox();
            this.chantierComboBox = new System.Windows.Forms.ComboBox();
            this.quantityNum = new System.Windows.Forms.NumericUpDown();
            this.openMatButton = new System.Windows.Forms.Button();
            this.openChantierButton = new System.Windows.Forms.Button();
            this.matLabel = new System.Windows.Forms.Label();
            this.chantierLabel = new System.Windows.Forms.Label();
            this.unitLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNum)).BeginInit();
            this.SuspendLayout();
            // 
            // matComboBox
            // 
            this.matComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.matComboBox.FormattingEnabled = true;
            this.matComboBox.Location = new System.Drawing.Point(13, 36);
            this.matComboBox.Name = "matComboBox";
            this.matComboBox.Size = new System.Drawing.Size(369, 21);
            this.matComboBox.TabIndex = 0;
            this.matComboBox.SelectedIndexChanged += new System.EventHandler(this.matComboBox_SelectedIndexChanged);
            // 
            // chantierComboBox
            // 
            this.chantierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chantierComboBox.FormattingEnabled = true;
            this.chantierComboBox.Location = new System.Drawing.Point(13, 96);
            this.chantierComboBox.Name = "chantierComboBox";
            this.chantierComboBox.Size = new System.Drawing.Size(369, 21);
            this.chantierComboBox.TabIndex = 1;
            // 
            // quantityNum
            // 
            this.quantityNum.Location = new System.Drawing.Point(13, 141);
            this.quantityNum.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.quantityNum.Name = "quantityNum";
            this.quantityNum.Size = new System.Drawing.Size(144, 20);
            this.quantityNum.TabIndex = 2;
            // 
            // openMatButton
            // 
            this.openMatButton.Enabled = false;
            this.openMatButton.Location = new System.Drawing.Point(388, 34);
            this.openMatButton.Name = "openMatButton";
            this.openMatButton.Size = new System.Drawing.Size(26, 23);
            this.openMatButton.TabIndex = 3;
            this.openMatButton.Text = "...";
            this.openMatButton.UseVisualStyleBackColor = true;
            // 
            // openChantierButton
            // 
            this.openChantierButton.Enabled = false;
            this.openChantierButton.Location = new System.Drawing.Point(388, 94);
            this.openChantierButton.Name = "openChantierButton";
            this.openChantierButton.Size = new System.Drawing.Size(26, 23);
            this.openChantierButton.TabIndex = 4;
            this.openChantierButton.Text = "...";
            this.openChantierButton.UseVisualStyleBackColor = true;
            // 
            // matLabel
            // 
            this.matLabel.AutoSize = true;
            this.matLabel.Location = new System.Drawing.Point(14, 17);
            this.matLabel.Name = "matLabel";
            this.matLabel.Size = new System.Drawing.Size(45, 13);
            this.matLabel.TabIndex = 5;
            this.matLabel.Text = "Element";
            // 
            // chantierLabel
            // 
            this.chantierLabel.AutoSize = true;
            this.chantierLabel.Location = new System.Drawing.Point(10, 80);
            this.chantierLabel.Name = "chantierLabel";
            this.chantierLabel.Size = new System.Drawing.Size(94, 13);
            this.chantierLabel.TabIndex = 6;
            this.chantierLabel.Text = "Zone de stockage";
            // 
            // unitLabel
            // 
            this.unitLabel.AutoSize = true;
            this.unitLabel.Location = new System.Drawing.Point(164, 147);
            this.unitLabel.Name = "unitLabel";
            this.unitLabel.Size = new System.Drawing.Size(19, 13);
            this.unitLabel.TabIndex = 7;
            this.unitLabel.Text = "en";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(312, 184);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(102, 34);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(204, 184);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 34);
            this.saveButton.TabIndex = 9;
            this.saveButton.Text = "Valider";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // stockModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 235);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.unitLabel);
            this.Controls.Add(this.chantierLabel);
            this.Controls.Add(this.matLabel);
            this.Controls.Add(this.openChantierButton);
            this.Controls.Add(this.openMatButton);
            this.Controls.Add(this.quantityNum);
            this.Controls.Add(this.chantierComboBox);
            this.Controls.Add(this.matComboBox);
            this.MaximumSize = new System.Drawing.Size(441, 273);
            this.MinimumSize = new System.Drawing.Size(441, 273);
            this.Name = "stockModifyForm";
            this.Text = "Modifier l\'entrée de stock";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.stockModifyForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.quantityNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openMatButton;
        private System.Windows.Forms.Button openChantierButton;
        private System.Windows.Forms.Label matLabel;
        private System.Windows.Forms.Label chantierLabel;
        private System.Windows.Forms.Label unitLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.ComboBox matComboBox;
        public System.Windows.Forms.ComboBox chantierComboBox;
        public System.Windows.Forms.NumericUpDown quantityNum;
    }
}