namespace CGest.Forms
{
    partial class ConnectionForm
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
            this.components = new System.ComponentModel.Container();
            this.connectionsList = new System.Windows.Forms.ListBox();
            this.defaultConnectionCheckBox = new System.Windows.Forms.CheckBox();
            this.loadConnectionButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.delConnectionButton = new System.Windows.Forms.Button();
            this.addConnectionButton = new System.Windows.Forms.Button();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.portNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.sourceTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider5 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.portNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).BeginInit();
            this.SuspendLayout();
            // 
            // connectionsList
            // 
            this.connectionsList.FormattingEnabled = true;
            this.connectionsList.Location = new System.Drawing.Point(413, 58);
            this.connectionsList.Name = "connectionsList";
            this.connectionsList.Size = new System.Drawing.Size(187, 69);
            this.connectionsList.TabIndex = 38;
            this.connectionsList.SelectedIndexChanged += new System.EventHandler(this.connectionsList_SelectedIndexChanged);
            // 
            // defaultConnectionCheckBox
            // 
            this.defaultConnectionCheckBox.AutoSize = true;
            this.defaultConnectionCheckBox.Enabled = false;
            this.defaultConnectionCheckBox.Location = new System.Drawing.Point(382, 181);
            this.defaultConnectionCheckBox.Name = "defaultConnectionCheckBox";
            this.defaultConnectionCheckBox.Size = new System.Drawing.Size(220, 17);
            this.defaultConnectionCheckBox.TabIndex = 37;
            this.defaultConnectionCheckBox.Text = "Enregistrer comme connection par defaut";
            this.defaultConnectionCheckBox.UseVisualStyleBackColor = true;
            this.defaultConnectionCheckBox.CheckedChanged += new System.EventHandler(this.defaultConnectionCheckBox_CheckedChanged);
            // 
            // loadConnectionButton
            // 
            this.loadConnectionButton.Enabled = false;
            this.loadConnectionButton.Location = new System.Drawing.Point(413, 138);
            this.loadConnectionButton.Name = "loadConnectionButton";
            this.loadConnectionButton.Size = new System.Drawing.Size(189, 29);
            this.loadConnectionButton.TabIndex = 36;
            this.loadConnectionButton.Text = "Charger la connection enregistrée";
            this.loadConnectionButton.UseVisualStyleBackColor = true;
            this.loadConnectionButton.Click += new System.EventHandler(this.loadConnectionButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Connections sauvergardée(s)";
            // 
            // delConnectionButton
            // 
            this.delConnectionButton.Enabled = false;
            this.delConnectionButton.Location = new System.Drawing.Point(115, 226);
            this.delConnectionButton.Name = "delConnectionButton";
            this.delConnectionButton.Size = new System.Drawing.Size(97, 37);
            this.delConnectionButton.TabIndex = 34;
            this.delConnectionButton.Text = "Supprimer";
            this.delConnectionButton.UseVisualStyleBackColor = true;
            this.delConnectionButton.Click += new System.EventHandler(this.delConnectionButton_Click);
            // 
            // addConnectionButton
            // 
            this.addConnectionButton.Location = new System.Drawing.Point(12, 226);
            this.addConnectionButton.Name = "addConnectionButton";
            this.addConnectionButton.Size = new System.Drawing.Size(97, 37);
            this.addConnectionButton.TabIndex = 33;
            this.addConnectionButton.Text = "Sauvegarder";
            this.addConnectionButton.UseVisualStyleBackColor = true;
            this.addConnectionButton.Click += new System.EventHandler(this.addConnectionButton_Click);
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(12, 181);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(41, 13);
            this.sourceLabel.TabIndex = 32;
            this.sourceLabel.Text = "Source";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(12, 140);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 31;
            this.portLabel.Text = "Port";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(12, 101);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(71, 13);
            this.passwordLabel.TabIndex = 30;
            this.passwordLabel.Text = "Mot de passe";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(12, 61);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(84, 13);
            this.userLabel.TabIndex = 29;
            this.userLabel.Text = "Nom d\'utilisateur";
            // 
            // pathLabel
            // 
            this.pathLabel.Location = new System.Drawing.Point(12, 16);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(82, 27);
            this.pathLabel.TabIndex = 28;
            this.pathLabel.Text = "Nom de la base de donnée";
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(395, 226);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 37);
            this.connectButton.TabIndex = 27;
            this.connectButton.Text = "Se connecter";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(505, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(97, 37);
            this.cancelButton.TabIndex = 26;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1, 269);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(611, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 25;
            this.progressBar1.Visible = false;
            // 
            // portNumUpDown
            // 
            this.portNumUpDown.Location = new System.Drawing.Point(104, 138);
            this.portNumUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portNumUpDown.Name = "portNumUpDown";
            this.portNumUpDown.Size = new System.Drawing.Size(91, 20);
            this.portNumUpDown.TabIndex = 24;
            this.portNumUpDown.Value = new decimal(new int[] {
            3050,
            0,
            0,
            0});
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.Location = new System.Drawing.Point(104, 178);
            this.sourceTextBox.MaxLength = 255;
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.Size = new System.Drawing.Size(136, 20);
            this.sourceTextBox.TabIndex = 23;
            this.sourceTextBox.Text = "localhost";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(104, 98);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(275, 20);
            this.passwordTextBox.TabIndex = 22;
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(104, 58);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(275, 20);
            this.userTextBox.TabIndex = 21;
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(104, 18);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(275, 20);
            this.pathTextBox.TabIndex = 20;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // errorProvider5
            // 
            this.errorProvider5.ContainerControl = this;
            // 
            // ConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 293);
            this.Controls.Add(this.connectionsList);
            this.Controls.Add(this.defaultConnectionCheckBox);
            this.Controls.Add(this.loadConnectionButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delConnectionButton);
            this.Controls.Add(this.addConnectionButton);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.portNumUpDown);
            this.Controls.Add(this.sourceTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.pathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Se connecter";
            ((System.ComponentModel.ISupportInitialize)(this.portNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox connectionsList;
        private System.Windows.Forms.CheckBox defaultConnectionCheckBox;
        private System.Windows.Forms.Button loadConnectionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button delConnectionButton;
        private System.Windows.Forms.Button addConnectionButton;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.NumericUpDown portNumUpDown;
        public System.Windows.Forms.TextBox sourceTextBox;
        public System.Windows.Forms.TextBox passwordTextBox;
        public System.Windows.Forms.TextBox userTextBox;
        public System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
        private System.Windows.Forms.ErrorProvider errorProvider5;

    }
}