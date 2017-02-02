namespace SR2DAdminApp.Forms
{
    partial class userModifyForm
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.reserv = new System.Windows.Forms.Label();
            this.bookingList = new System.Windows.Forms.ListBox();
            this.classFullTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cardCodeTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.classCodeTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.surnameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameTextbox = new System.Windows.Forms.TextBox();
            this.modifyBookingButton = new System.Windows.Forms.Button();
            this.passedCombo = new System.Windows.Forms.ComboBox();
            this.retiredCombo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(390, 518);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(102, 34);
            this.cancelButton.TabIndex = 45;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(282, 518);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 34);
            this.saveButton.TabIndex = 44;
            this.saveButton.Text = "Valider";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 467);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 107;
            this.label5.Text = "Retirée";
            // 
            // reserv
            // 
            this.reserv.AutoSize = true;
            this.reserv.Location = new System.Drawing.Point(10, 256);
            this.reserv.Name = "reserv";
            this.reserv.Size = new System.Drawing.Size(64, 13);
            this.reserv.TabIndex = 105;
            this.reserv.Text = "Réservation";
            // 
            // bookingList
            // 
            this.bookingList.FormattingEnabled = true;
            this.bookingList.Items.AddRange(new object[] {
            "Aucune réservation pour le moment"});
            this.bookingList.Location = new System.Drawing.Point(80, 246);
            this.bookingList.Name = "bookingList";
            this.bookingList.Size = new System.Drawing.Size(414, 134);
            this.bookingList.TabIndex = 104;
            // 
            // classFullTextbox
            // 
            this.classFullTextbox.Location = new System.Drawing.Point(163, 82);
            this.classFullTextbox.Name = "classFullTextbox";
            this.classFullTextbox.ReadOnly = true;
            this.classFullTextbox.Size = new System.Drawing.Size(331, 20);
            this.classFullTextbox.TabIndex = 103;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 206);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 102;
            this.label8.Text = "Réservé";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 100;
            this.label4.Text = "Code carte";
            // 
            // cardCodeTextbox
            // 
            this.cardCodeTextbox.Location = new System.Drawing.Point(80, 139);
            this.cardCodeTextbox.Name = "cardCodeTextbox";
            this.cardCodeTextbox.ReadOnly = true;
            this.cardCodeTextbox.Size = new System.Drawing.Size(414, 20);
            this.cardCodeTextbox.TabIndex = 99;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 98;
            this.label3.Text = "Classe";
            // 
            // classCodeTextbox
            // 
            this.classCodeTextbox.Location = new System.Drawing.Point(80, 82);
            this.classCodeTextbox.Name = "classCodeTextbox";
            this.classCodeTextbox.ReadOnly = true;
            this.classCodeTextbox.Size = new System.Drawing.Size(71, 20);
            this.classCodeTextbox.TabIndex = 97;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 96;
            this.label2.Text = "Prénom";
            // 
            // surnameTextbox
            // 
            this.surnameTextbox.Location = new System.Drawing.Point(80, 56);
            this.surnameTextbox.Name = "surnameTextbox";
            this.surnameTextbox.ReadOnly = true;
            this.surnameTextbox.Size = new System.Drawing.Size(414, 20);
            this.surnameTextbox.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 94;
            this.label1.Text = "Nom";
            // 
            // nameTextbox
            // 
            this.nameTextbox.Location = new System.Drawing.Point(80, 30);
            this.nameTextbox.Name = "nameTextbox";
            this.nameTextbox.ReadOnly = true;
            this.nameTextbox.Size = new System.Drawing.Size(414, 20);
            this.nameTextbox.TabIndex = 93;
            // 
            // modifyBookingButton
            // 
            this.modifyBookingButton.Location = new System.Drawing.Point(80, 387);
            this.modifyBookingButton.Name = "modifyBookingButton";
            this.modifyBookingButton.Size = new System.Drawing.Size(414, 32);
            this.modifyBookingButton.TabIndex = 108;
            this.modifyBookingButton.Text = "Modifier la réservation";
            this.modifyBookingButton.UseVisualStyleBackColor = true;
            this.modifyBookingButton.Click += new System.EventHandler(this.modifyBookingButton_Click);
            // 
            // passedCombo
            // 
            this.passedCombo.FormattingEnabled = true;
            this.passedCombo.Items.AddRange(new object[] {
            "Oui",
            "Non"});
            this.passedCombo.Location = new System.Drawing.Point(80, 203);
            this.passedCombo.Name = "passedCombo";
            this.passedCombo.Size = new System.Drawing.Size(172, 21);
            this.passedCombo.TabIndex = 109;
            this.passedCombo.SelectedIndexChanged += new System.EventHandler(this.passedCombo_SelectedIndexChanged);
            // 
            // retiredCombo
            // 
            this.retiredCombo.FormattingEnabled = true;
            this.retiredCombo.Items.AddRange(new object[] {
            "Oui",
            "Non"});
            this.retiredCombo.Location = new System.Drawing.Point(83, 464);
            this.retiredCombo.Name = "retiredCombo";
            this.retiredCombo.Size = new System.Drawing.Size(172, 21);
            this.retiredCombo.TabIndex = 110;
            this.retiredCombo.SelectedIndexChanged += new System.EventHandler(this.retiredCombo_SelectedIndexChanged);
            // 
            // userModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 563);
            this.Controls.Add(this.retiredCombo);
            this.Controls.Add(this.passedCombo);
            this.Controls.Add(this.modifyBookingButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.reserv);
            this.Controls.Add(this.bookingList);
            this.Controls.Add(this.classFullTextbox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cardCodeTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.classCodeTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.surnameTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTextbox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.MaximumSize = new System.Drawing.Size(522, 602);
            this.MinimumSize = new System.Drawing.Size(522, 602);
            this.Name = "userModifyForm";
            this.Text = "Modifier l\'utilisateur";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.clientModifyForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label reserv;
        private System.Windows.Forms.ListBox bookingList;
        private System.Windows.Forms.TextBox classFullTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cardCodeTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox classCodeTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox surnameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameTextbox;
        private System.Windows.Forms.Button modifyBookingButton;
        private System.Windows.Forms.ComboBox passedCombo;
        private System.Windows.Forms.ComboBox retiredCombo;

    }
}