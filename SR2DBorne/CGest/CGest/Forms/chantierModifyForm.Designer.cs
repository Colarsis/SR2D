namespace CGest.Forms
{
    partial class chantierModifyForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.clientCombobox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Client";
            // 
            // clientCombobox
            // 
            this.clientCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientCombobox.FormattingEnabled = true;
            this.clientCombobox.Location = new System.Drawing.Point(12, 74);
            this.clientCombobox.Name = "clientCombobox";
            this.clientCombobox.Size = new System.Drawing.Size(368, 21);
            this.clientCombobox.TabIndex = 30;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(278, 113);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(102, 34);
            this.cancelButton.TabIndex = 29;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(12, 25);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.Size = new System.Drawing.Size(368, 20);
            this.nameTextbox.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Nom";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(170, 113);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 34);
            this.saveButton.TabIndex = 26;
            this.saveButton.Text = "Valider";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // chantierModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 159);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clientCombobox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.nameTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveButton);
            this.MaximumSize = new System.Drawing.Size(408, 197);
            this.MinimumSize = new System.Drawing.Size(408, 197);
            this.Name = "chantierModifyForm";
            this.Text = "Modifier le fichier client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.clientModifyForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox clientCombobox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveButton;

    }
}