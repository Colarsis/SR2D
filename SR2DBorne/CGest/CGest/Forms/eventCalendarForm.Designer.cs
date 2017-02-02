namespace CGest.Forms
{
    partial class eventCalendarForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.addEventButton = new System.Windows.Forms.Button();
            this.calendar1 = new ColarsisUserControls.Calendar();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.intervalLabel);
            this.groupBox1.Controls.Add(this.nextButton);
            this.groupBox1.Controls.Add(this.previousButton);
            this.groupBox1.Controls.Add(this.addEventButton);
            this.groupBox1.Location = new System.Drawing.Point(875, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 477);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Outils";
            // 
            // intervalLabel
            // 
            this.intervalLabel.Location = new System.Drawing.Point(6, 52);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(168, 24);
            this.intervalLabel.TabIndex = 3;
            this.intervalLabel.Text = "Du 00/00/0000 au 00/00/0000";
            this.intervalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(94, 19);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(80, 30);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = ">>>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(6, 19);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(80, 30);
            this.previousButton.TabIndex = 1;
            this.previousButton.Text = "<<<";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // addEventButton
            // 
            this.addEventButton.Location = new System.Drawing.Point(6, 132);
            this.addEventButton.Name = "addEventButton";
            this.addEventButton.Size = new System.Drawing.Size(168, 57);
            this.addEventButton.TabIndex = 0;
            this.addEventButton.Text = "Nouvel évenement";
            this.addEventButton.UseVisualStyleBackColor = true;
            this.addEventButton.Click += new System.EventHandler(this.addEventButton_Click);
            // 
            // calendar1
            // 
            this.calendar1.AutoScroll = true;
            this.calendar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.calendar1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.calendar1.EventTextColor = System.Drawing.Color.Black;
            this.calendar1.EventTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendar1.HeaderTextColor = System.Drawing.Color.CadetBlue;
            this.calendar1.HeaderTextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendar1.Location = new System.Drawing.Point(12, 12);
            this.calendar1.Name = "calendar1";
            this.calendar1.SeparatorColor = System.Drawing.Color.Silver;
            this.calendar1.SeparatorThickness = 1;
            this.calendar1.Size = new System.Drawing.Size(857, 477);
            this.calendar1.TabIndex = 5;
            this.calendar1.TimeBarColor = System.Drawing.Color.DarkOrange;
            // 
            // eventCalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 501);
            this.Controls.Add(this.calendar1);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(838, 408);
            this.Name = "eventCalendarForm";
            this.Text = "eventCalendarForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.eventCalendarForm_FormClosed);
            this.Resize += new System.EventHandler(this.eventCalendarForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ColarsisUserControls.Calendar calendar1;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label intervalLabel;
        public System.Windows.Forms.Button addEventButton;
    }
}