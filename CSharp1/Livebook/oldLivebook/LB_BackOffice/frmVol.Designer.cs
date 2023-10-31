namespace LiveBook
{
    partial class frmVol
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVol));
            this.cmdInsert = new System.Windows.Forms.Button();
            this.txtVol = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.dgVol = new System.Windows.Forms.DataGridView();
            this.Save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVol)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdInsert
            // 
            this.cmdInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInsert.Location = new System.Drawing.Point(173, 31);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 0;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = false;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // txtVol
            // 
            this.txtVol.Location = new System.Drawing.Point(6, 19);
            this.txtVol.Name = "txtVol";
            this.txtVol.Size = new System.Drawing.Size(113, 20);
            this.txtVol.TabIndex = 2;
            this.txtVol.Leave += new System.EventHandler(this.txtVol_Leave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTicker);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 47);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ativo";
            // 
            // cmbTicker
            // 
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTicker.FormattingEnabled = true;
            this.cmbTicker.Location = new System.Drawing.Point(6, 19);
            this.cmbTicker.Name = "cmbTicker";
            this.cmbTicker.Size = new System.Drawing.Size(121, 21);
            this.cmbTicker.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtVol);
            this.groupBox2.Location = new System.Drawing.Point(12, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 48);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Volatilidade";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Location = new System.Drawing.Point(173, 84);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dgVol
            // 
            this.dgVol.AllowUserToAddRows = false;
            this.dgVol.AllowUserToDeleteRows = false;
            this.dgVol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVol.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgVol.BackgroundColor = System.Drawing.Color.White;
            this.dgVol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Save});
            this.dgVol.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgVol.Location = new System.Drawing.Point(5, 119);
            this.dgVol.Name = "dgVol";
            this.dgVol.RowHeadersVisible = false;
            this.dgVol.RowHeadersWidth = 4;
            this.dgVol.Size = new System.Drawing.Size(284, 305);
            this.dgVol.TabIndex = 6;
            this.dgVol.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgVol_CellContentClick);
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
            // frmVol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 429);
            this.Controls.Add(this.dgVol);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdInsert);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Volatility";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVol)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.TextBox txtVol;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbTicker;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.DataGridView dgVol;
        private System.Windows.Forms.DataGridViewButtonColumn Save;
    }
}