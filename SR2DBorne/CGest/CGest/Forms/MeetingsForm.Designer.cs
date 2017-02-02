namespace CGest.Forms
{
    partial class MeetingsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chantierIDCOlumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventIDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rowActionPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.calendarMeetingButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.modifyMeetingButton = new System.Windows.Forms.Button();
            this.deleteMeetingButton = new System.Windows.Forms.Button();
            this.addMeetingButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchTextbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.rowActionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.clientIDColumn,
            this.chantierIDCOlumn,
            this.eventIDColumn,
            this.clientColumn,
            this.empColumn,
            this.dateColumn,
            this.statusColumn});
            this.dataGridView1.GridColor = System.Drawing.Color.Silver;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(864, 477);
            this.dataGridView1.TabIndex = 5;
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "id";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Visible = false;
            // 
            // clientIDColumn
            // 
            this.clientIDColumn.HeaderText = "client";
            this.clientIDColumn.Name = "clientIDColumn";
            this.clientIDColumn.ReadOnly = true;
            this.clientIDColumn.Visible = false;
            // 
            // chantierIDCOlumn
            // 
            this.chantierIDCOlumn.HeaderText = "chantier";
            this.chantierIDCOlumn.Name = "chantierIDCOlumn";
            this.chantierIDCOlumn.ReadOnly = true;
            this.chantierIDCOlumn.Visible = false;
            // 
            // eventIDColumn
            // 
            this.eventIDColumn.HeaderText = "event";
            this.eventIDColumn.Name = "eventIDColumn";
            this.eventIDColumn.ReadOnly = true;
            this.eventIDColumn.Visible = false;
            // 
            // clientColumn
            // 
            this.clientColumn.HeaderText = "Client";
            this.clientColumn.Name = "clientColumn";
            this.clientColumn.ReadOnly = true;
            // 
            // empColumn
            // 
            this.empColumn.HeaderText = "Interlocuteur";
            this.empColumn.Name = "empColumn";
            this.empColumn.ReadOnly = true;
            // 
            // dateColumn
            // 
            this.dateColumn.HeaderText = "Date et heure";
            this.dateColumn.Name = "dateColumn";
            this.dateColumn.ReadOnly = true;
            // 
            // statusColumn
            // 
            this.statusColumn.HeaderText = "Status";
            this.statusColumn.Name = "statusColumn";
            this.statusColumn.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rowActionPanel);
            this.groupBox1.Controls.Add(this.addMeetingButton);
            this.groupBox1.Controls.Add(this.searchButton);
            this.groupBox1.Controls.Add(this.searchTextbox);
            this.groupBox1.Location = new System.Drawing.Point(882, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 477);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Outils";
            // 
            // rowActionPanel
            // 
            this.rowActionPanel.Controls.Add(this.button2);
            this.rowActionPanel.Controls.Add(this.calendarMeetingButton);
            this.rowActionPanel.Controls.Add(this.button1);
            this.rowActionPanel.Controls.Add(this.modifyMeetingButton);
            this.rowActionPanel.Controls.Add(this.deleteMeetingButton);
            this.rowActionPanel.Enabled = false;
            this.rowActionPanel.Location = new System.Drawing.Point(9, 164);
            this.rowActionPanel.Name = "rowActionPanel";
            this.rowActionPanel.Size = new System.Drawing.Size(164, 233);
            this.rowActionPanel.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 78);
            this.button2.TabIndex = 7;
            this.button2.Text = "Autres informations";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // calendarMeetingButton
            // 
            this.calendarMeetingButton.Location = new System.Drawing.Point(83, 87);
            this.calendarMeetingButton.Name = "calendarMeetingButton";
            this.calendarMeetingButton.Size = new System.Drawing.Size(78, 78);
            this.calendarMeetingButton.TabIndex = 4;
            this.calendarMeetingButton.Text = "Afficher dans le calendrier";
            this.calendarMeetingButton.UseVisualStyleBackColor = true;
            this.calendarMeetingButton.Click += new System.EventHandler(this.calendarMeetingButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 57);
            this.button1.TabIndex = 3;
            this.button1.Text = "Terminer le rendez-vous";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.endButton_Click);
            // 
            // modifyMeetingButton
            // 
            this.modifyMeetingButton.Location = new System.Drawing.Point(3, 3);
            this.modifyMeetingButton.Name = "modifyMeetingButton";
            this.modifyMeetingButton.Size = new System.Drawing.Size(78, 78);
            this.modifyMeetingButton.TabIndex = 2;
            this.modifyMeetingButton.Text = "Modifier le rendez-vous";
            this.modifyMeetingButton.UseVisualStyleBackColor = true;
            this.modifyMeetingButton.Click += new System.EventHandler(this.modifyMeetingsButton_Click);
            // 
            // deleteMeetingButton
            // 
            this.deleteMeetingButton.Location = new System.Drawing.Point(83, 3);
            this.deleteMeetingButton.Name = "deleteMeetingButton";
            this.deleteMeetingButton.Size = new System.Drawing.Size(78, 78);
            this.deleteMeetingButton.TabIndex = 1;
            this.deleteMeetingButton.Text = "Supprimer le rendez-vous";
            this.deleteMeetingButton.UseVisualStyleBackColor = true;
            this.deleteMeetingButton.Click += new System.EventHandler(this.deleteMeetingsButton_Click);
            // 
            // addMeetingButton
            // 
            this.addMeetingButton.Location = new System.Drawing.Point(12, 102);
            this.addMeetingButton.Name = "addMeetingButton";
            this.addMeetingButton.Size = new System.Drawing.Size(158, 57);
            this.addMeetingButton.TabIndex = 0;
            this.addMeetingButton.Text = "Nouveau rendez-vous";
            this.addMeetingButton.UseVisualStyleBackColor = true;
            this.addMeetingButton.Click += new System.EventHandler(this.addMeetingsButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(7, 46);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(166, 34);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Rechercher un élement";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchTextbox
            // 
            this.searchTextbox.Location = new System.Drawing.Point(7, 19);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(167, 20);
            this.searchTextbox.TabIndex = 0;
            // 
            // MeetingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 501);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.MinimumSize = new System.Drawing.Size(838, 454);
            this.Name = "MeetingsForm";
            this.Text = "Rendez-vous";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.meetingsForm_FormClosed);
            this.Resize += new System.EventHandler(this.meetingsForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rowActionPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn chantierIDCOlumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventIDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn empColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel rowActionPanel;
        private System.Windows.Forms.Button modifyMeetingButton;
        private System.Windows.Forms.Button deleteMeetingButton;
        private System.Windows.Forms.Button addMeetingButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchTextbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button calendarMeetingButton;
        private System.Windows.Forms.Button button2;
    }
}