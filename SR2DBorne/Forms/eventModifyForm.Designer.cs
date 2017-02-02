namespace CGest.Forms
{
    partial class eventModifyForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.endMin = new System.Windows.Forms.NumericUpDown();
            this.endHour = new System.Windows.Forms.NumericUpDown();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.beginMin = new System.Windows.Forms.NumericUpDown();
            this.beginHour = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.descTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextbox = new System.Windows.Forms.TextBox();
            this.beginDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.endMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginHour)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(395, 328);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(102, 34);
            this.cancelButton.TabIndex = 43;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(287, 328);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(102, 34);
            this.saveButton.TabIndex = 42;
            this.saveButton.Text = "Valider";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 277);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Fin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(451, 271);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 16);
            this.label6.TabIndex = 40;
            this.label6.Text = "min";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(374, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "h";
            // 
            // endMin
            // 
            this.endMin.Location = new System.Drawing.Point(395, 271);
            this.endMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.endMin.Name = "endMin";
            this.endMin.Size = new System.Drawing.Size(50, 20);
            this.endMin.TabIndex = 38;
            // 
            // endHour
            // 
            this.endHour.Location = new System.Drawing.Point(318, 271);
            this.endHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.endHour.Name = "endHour";
            this.endHour.Size = new System.Drawing.Size(50, 20);
            this.endHour.TabIndex = 37;
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(76, 271);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(199, 20);
            this.endDate.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Début";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(451, 245);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 16);
            this.label4.TabIndex = 34;
            this.label4.Text = "min";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(374, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "h";
            // 
            // beginMin
            // 
            this.beginMin.Location = new System.Drawing.Point(395, 245);
            this.beginMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.beginMin.Name = "beginMin";
            this.beginMin.Size = new System.Drawing.Size(50, 20);
            this.beginMin.TabIndex = 32;
            // 
            // beginHour
            // 
            this.beginHour.Location = new System.Drawing.Point(318, 245);
            this.beginHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.beginHour.Name = "beginHour";
            this.beginHour.Size = new System.Drawing.Size(50, 20);
            this.beginHour.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Description";
            // 
            // descTextbox
            // 
            this.descTextbox.Location = new System.Drawing.Point(76, 49);
            this.descTextbox.Multiline = true;
            this.descTextbox.Name = "descTextbox";
            this.descTextbox.Size = new System.Drawing.Size(414, 163);
            this.descTextbox.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Titre";
            // 
            // titleTextbox
            // 
            this.titleTextbox.Location = new System.Drawing.Point(76, 23);
            this.titleTextbox.Name = "titleTextbox";
            this.titleTextbox.Size = new System.Drawing.Size(414, 20);
            this.titleTextbox.TabIndex = 27;
            // 
            // beginDate
            // 
            this.beginDate.Location = new System.Drawing.Point(76, 245);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(199, 20);
            this.beginDate.TabIndex = 26;
            // 
            // eventModifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 384);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.endMin);
            this.Controls.Add(this.endHour);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.beginMin);
            this.Controls.Add(this.beginHour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descTextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleTextbox);
            this.Controls.Add(this.beginDate);
            this.MaximumSize = new System.Drawing.Size(523, 422);
            this.MinimumSize = new System.Drawing.Size(523, 422);
            this.Name = "eventModifyForm";
            this.Text = "eventModifyForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.eventModifyForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.endMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginHour)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown endMin;
        private System.Windows.Forms.NumericUpDown endHour;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown beginMin;
        private System.Windows.Forms.NumericUpDown beginHour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titleTextbox;
        private System.Windows.Forms.DateTimePicker beginDate;
    }
}