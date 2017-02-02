namespace SR2DAdminApp.Forms
{
    partial class userInfosForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.passedTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cardCodeTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.classCodeTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.surnameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.endButton = new System.Windows.Forms.Button();
            this.classFullTextbox = new System.Windows.Forms.TextBox();
            this.bookingList = new System.Windows.Forms.ListBox();
            this.reserv = new System.Windows.Forms.Label();
            this.retiredTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 83;
            this.label8.Text = "Réservé";
            // 
            // passedTextbox
            // 
            this.passedTextbox.Location = new System.Drawing.Point(78, 196);
            this.passedTextbox.Name = "passedTextbox";
            this.passedTextbox.ReadOnly = true;
            this.passedTextbox.Size = new System.Drawing.Size(172, 20);
            this.passedTextbox.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 75;
            this.label4.Text = "Code carte";
            // 
            // cardCodeTextbox
            // 
            this.cardCodeTextbox.Location = new System.Drawing.Point(78, 132);
            this.cardCodeTextbox.Name = "cardCodeTextbox";
            this.cardCodeTextbox.ReadOnly = true;
            this.cardCodeTextbox.Size = new System.Drawing.Size(414, 20);
            this.cardCodeTextbox.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 73;
            this.label3.Text = "Classe";
            // 
            // classCodeTextbox
            // 
            this.classCodeTextbox.Location = new System.Drawing.Point(78, 75);
            this.classCodeTextbox.Name = "classCodeTextbox";
            this.classCodeTextbox.ReadOnly = true;
            this.classCodeTextbox.Size = new System.Drawing.Size(71, 20);
            this.classCodeTextbox.TabIndex = 72;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Prénom";
            // 
            // surnameTextbox
            // 
            this.surnameTextbox.Location = new System.Drawing.Point(78, 49);
            this.surnameTextbox.Name = "surnameTextbox";
            this.surnameTextbox.ReadOnly = true;
            this.surnameTextbox.Size = new System.Drawing.Size(414, 20);
            this.surnameTextbox.TabIndex = 70;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Nom";
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(78, 23);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.ReadOnly = true;
            this.nameTextbox.Size = new System.Drawing.Size(414, 20);
            this.nameTextbox.TabIndex = 68;
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(390, 518);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(102, 34);
            this.endButton.TabIndex = 67;
            this.endButton.Text = "Terminer";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // classFullTextbox
            // 
            this.classFullTextbox.Location = new System.Drawing.Point(161, 75);
            this.classFullTextbox.Name = "classFullTextbox";
            this.classFullTextbox.ReadOnly = true;
            this.classFullTextbox.Size = new System.Drawing.Size(331, 20);
            this.classFullTextbox.TabIndex = 88;
            // 
            // bookingList
            // 
            this.bookingList.FormattingEnabled = true;
            this.bookingList.Items.AddRange(new object[] {
            "Aucune réservation pour le moment"});
            this.bookingList.Location = new System.Drawing.Point(78, 239);
            this.bookingList.Name = "bookingList";
            this.bookingList.Size = new System.Drawing.Size(414, 212);
            this.bookingList.TabIndex = 89;
            // 
            // reserv
            // 
            this.reserv.AutoSize = true;
            this.reserv.Location = new System.Drawing.Point(8, 249);
            this.reserv.Name = "reserv";
            this.reserv.Size = new System.Drawing.Size(64, 13);
            this.reserv.TabIndex = 90;
            this.reserv.Text = "Réservation";
            // 
            // retiredTextbox
            // 
            this.retiredTextbox.Location = new System.Drawing.Point(78, 457);
            this.retiredTextbox.Name = "retiredTextbox";
            this.retiredTextbox.ReadOnly = true;
            this.retiredTextbox.Size = new System.Drawing.Size(172, 20);
            this.retiredTextbox.TabIndex = 91;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 460);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 92;
            this.label5.Text = "Retirée";
            // 
            // userInfosForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 563);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.retiredTextbox);
            this.Controls.Add(this.reserv);
            this.Controls.Add(this.bookingList);
            this.Controls.Add(this.classFullTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.passedTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cardCodeTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.classCodeTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.surnameTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextbox);
            this.Controls.Add(this.endButton);
            this.MaximumSize = new System.Drawing.Size(522, 602);
            this.MinimumSize = new System.Drawing.Size(522, 602);
            this.Name = "userInfosForm";
            this.Text = "Informations de l\'utilisateur";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.clientInfosForm_FormClosed);
            this.Load += new System.EventHandler(this.clientInfosForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox passedTextbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cardCodeTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox classCodeTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox surnameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.TextBox classFullTextbox;
        private System.Windows.Forms.ListBox bookingList;
        private System.Windows.Forms.Label reserv;
        private System.Windows.Forms.TextBox retiredTextbox;
        private System.Windows.Forms.Label label5;
    }
}