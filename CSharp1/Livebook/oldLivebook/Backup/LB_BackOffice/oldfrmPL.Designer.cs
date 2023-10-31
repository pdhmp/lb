namespace SGN
{
    partial class frmPL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPL));
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPL = new System.Windows.Forms.TextBox();
            this.CmdCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CmbPortfolio = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpDataPL = new System.Windows.Forms.DateTimePicker();
            this.dgPL = new System.Windows.Forms.DataGridView();
            this.Save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPL)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdInsert
            // 
            this.cmdInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInsert.Location = new System.Drawing.Point(267, 31);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 3;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = false;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            this.cmdInsert.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPL);
            this.groupBox1.Location = new System.Drawing.Point(123, 75);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(107, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PL";
            // 
            // txtPL
            // 
            this.txtPL.Location = new System.Drawing.Point(6, 21);
            this.txtPL.Name = "txtPL";
            this.txtPL.Size = new System.Drawing.Size(93, 20);
            this.txtPL.TabIndex = 0;
            this.txtPL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtPL.Leave += new System.EventHandler(this.txtPl_Leave);
            // 
            // CmdCancel
            // 
            this.CmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmdCancel.Location = new System.Drawing.Point(267, 96);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(75, 23);
            this.CmdCancel.TabIndex = 4;
            this.CmdCancel.Text = "Close";
            this.CmdCancel.UseVisualStyleBackColor = false;
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            this.CmdCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CmbPortfolio);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(218, 57);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Portfólio";
            // 
            // CmbPortfolio
            // 
            this.CmbPortfolio.FormattingEnabled = true;
            this.CmbPortfolio.Location = new System.Drawing.Point(6, 19);
            this.CmbPortfolio.Name = "CmbPortfolio";
            this.CmbPortfolio.Size = new System.Drawing.Size(204, 21);
            this.CmbPortfolio.TabIndex = 0;
            this.CmbPortfolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpDataPL);
            this.groupBox3.Location = new System.Drawing.Point(12, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 57);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Date";
            // 
            // dtpDataPL
            // 
            this.dtpDataPL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataPL.Location = new System.Drawing.Point(6, 20);
            this.dtpDataPL.Name = "dtpDataPL";
            this.dtpDataPL.Size = new System.Drawing.Size(91, 20);
            this.dtpDataPL.TabIndex = 0;
            this.dtpDataPL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // dgPL
            // 
            this.dgPL.AllowUserToAddRows = false;
            this.dgPL.AllowUserToDeleteRows = false;
            this.dgPL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgPL.BackgroundColor = System.Drawing.Color.Gray;
            this.dgPL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Save});
            this.dgPL.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgPL.Location = new System.Drawing.Point(12, 138);
            this.dgPL.Name = "dgPL";
            this.dgPL.RowHeadersVisible = false;
            this.dgPL.RowHeadersWidth = 4;
            this.dgPL.Size = new System.Drawing.Size(363, 284);
            this.dgPL.TabIndex = 5;
            this.dgPL.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPL_CellFormat);
            this.dgPL.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPL_CellContentClick);
            // 
            // Save
            // 
            this.Save.HeaderText = "Save";
            this.Save.Name = "Save";
            this.Save.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Save.Text = "Save";
            this.Save.ToolTipText = "Save";
            this.Save.UseColumnTextForButtonValue = true;
            this.Save.Visible = false;
            this.Save.Width = 38;
            // 
            // frmPL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 430);
            this.Controls.Add(this.dgPL);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdInsert);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PL";
            this.Load += new System.EventHandler(this.frmPL_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPL;
        private System.Windows.Forms.Button CmdCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CmbPortfolio;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dtpDataPL;
        private System.Windows.Forms.DataGridView dgPL;
        private System.Windows.Forms.DataGridViewButtonColumn Save;
    }
}