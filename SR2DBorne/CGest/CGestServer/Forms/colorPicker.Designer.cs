namespace CGestServer.Forms
{
    partial class colorPicker
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
            this.webColorPicker1 = new ColarsisUserControls.WebColorPicker();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webColorPicker1
            // 
            this.webColorPicker1.Location = new System.Drawing.Point(12, 12);
            this.webColorPicker1.Name = "webColorPicker1";
            this.webColorPicker1.Size = new System.Drawing.Size(260, 351);
            this.webColorPicker1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 369);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(260, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // colorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 404);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.webColorPicker1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "colorPicker";
            this.Text = "Choisir une couleur";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.colorPicker_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        public  ColarsisUserControls.WebColorPicker webColorPicker1;
    }
}