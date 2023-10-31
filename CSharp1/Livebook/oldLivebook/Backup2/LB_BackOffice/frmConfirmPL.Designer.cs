namespace SGN
{
    partial class frmConfirmPL
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.CmbPortfolio = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCashUSDAdj = new System.Windows.Forms.TextBox();
            this.txtCashBRLAdj = new System.Windows.Forms.TextBox();
            this.txtNavUSDAdj = new System.Windows.Forms.TextBox();
            this.txtNavBRLAdj = new System.Windows.Forms.TextBox();
            this.txtNAVAdj = new System.Windows.Forms.TextBox();
            this.txtCashUSDAdm = new System.Windows.Forms.TextBox();
            this.txtCashBRLAdm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNavUSDNest = new System.Windows.Forms.TextBox();
            this.txtNavBRLNest = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNAVNest = new System.Windows.Forms.TextBox();
            this.txtNavUSDAdm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNavBRLAdm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNAVAdm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCashBRLNest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCashUSDNest = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgPL = new System.Windows.Forms.DataGridView();
            this.Save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPL)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(373, 280);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 34);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(164, 19);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDate.TabIndex = 2;
            this.dtpDate.TabStop = false;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(221, 207);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(102, 34);
            this.cmdUpdate.TabIndex = 8;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // CmbPortfolio
            // 
            this.CmbPortfolio.FormattingEnabled = true;
            this.CmbPortfolio.Location = new System.Drawing.Point(6, 19);
            this.CmbPortfolio.Name = "CmbPortfolio";
            this.CmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.CmbPortfolio.TabIndex = 1;
            this.CmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.CmbPortfolio_SelectedIndexChanged);
            this.CmbPortfolio.SelectedValueChanged += new System.EventHandler(this.CmbPortfolio_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCashUSDAdj);
            this.groupBox2.Controls.Add(this.txtCashBRLAdj);
            this.groupBox2.Controls.Add(this.txtNavUSDAdj);
            this.groupBox2.Controls.Add(this.txtNavBRLAdj);
            this.groupBox2.Controls.Add(this.txtNAVAdj);
            this.groupBox2.Controls.Add(this.CmbPortfolio);
            this.groupBox2.Controls.Add(this.txtCashUSDAdm);
            this.groupBox2.Controls.Add(this.dtpDate);
            this.groupBox2.Controls.Add(this.txtCashBRLAdm);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtNavUSDNest);
            this.groupBox2.Controls.Add(this.txtNavBRLNest);
            this.groupBox2.Controls.Add(this.cmdUpdate);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtNAVNest);
            this.groupBox2.Controls.Add(this.txtNavUSDAdm);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtNavBRLAdm);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtNAVAdm);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCashBRLNest);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtCashUSDNest);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(461, 252);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Update NAVs";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(356, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Difference";
            // 
            // txtCashUSDAdj
            // 
            this.txtCashUSDAdj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashUSDAdj.Location = new System.Drawing.Point(338, 182);
            this.txtCashUSDAdj.Name = "txtCashUSDAdj";
            this.txtCashUSDAdj.ReadOnly = true;
            this.txtCashUSDAdj.Size = new System.Drawing.Size(100, 13);
            this.txtCashUSDAdj.TabIndex = 39;
            this.txtCashUSDAdj.TabStop = false;
            this.txtCashUSDAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCashBRLAdj
            // 
            this.txtCashBRLAdj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashBRLAdj.Location = new System.Drawing.Point(338, 157);
            this.txtCashBRLAdj.Name = "txtCashBRLAdj";
            this.txtCashBRLAdj.ReadOnly = true;
            this.txtCashBRLAdj.Size = new System.Drawing.Size(100, 13);
            this.txtCashBRLAdj.TabIndex = 38;
            this.txtCashBRLAdj.TabStop = false;
            this.txtCashBRLAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNavUSDAdj
            // 
            this.txtNavUSDAdj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavUSDAdj.Location = new System.Drawing.Point(338, 130);
            this.txtNavUSDAdj.Name = "txtNavUSDAdj";
            this.txtNavUSDAdj.ReadOnly = true;
            this.txtNavUSDAdj.Size = new System.Drawing.Size(100, 13);
            this.txtNavUSDAdj.TabIndex = 37;
            this.txtNavUSDAdj.TabStop = false;
            this.txtNavUSDAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNavBRLAdj
            // 
            this.txtNavBRLAdj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavBRLAdj.Location = new System.Drawing.Point(338, 104);
            this.txtNavBRLAdj.Name = "txtNavBRLAdj";
            this.txtNavBRLAdj.ReadOnly = true;
            this.txtNavBRLAdj.Size = new System.Drawing.Size(100, 13);
            this.txtNavBRLAdj.TabIndex = 36;
            this.txtNavBRLAdj.TabStop = false;
            this.txtNavBRLAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNAVAdj
            // 
            this.txtNAVAdj.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVAdj.Location = new System.Drawing.Point(338, 78);
            this.txtNAVAdj.Name = "txtNAVAdj";
            this.txtNAVAdj.ReadOnly = true;
            this.txtNAVAdj.Size = new System.Drawing.Size(100, 13);
            this.txtNAVAdj.TabIndex = 35;
            this.txtNAVAdj.TabStop = false;
            this.txtNAVAdj.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCashUSDAdm
            // 
            this.txtCashUSDAdm.Location = new System.Drawing.Point(221, 178);
            this.txtCashUSDAdm.Name = "txtCashUSDAdm";
            this.txtCashUSDAdm.Size = new System.Drawing.Size(100, 20);
            this.txtCashUSDAdm.TabIndex = 7;
            this.txtCashUSDAdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCashUSDAdm.TextChanged += new System.EventHandler(this.txtCashUSDAdm_TextChanged);
            this.txtCashUSDAdm.Leave += new System.EventHandler(this.txtCashUSDAdm_Leave);
            // 
            // txtCashBRLAdm
            // 
            this.txtCashBRLAdm.Location = new System.Drawing.Point(221, 153);
            this.txtCashBRLAdm.Name = "txtCashBRLAdm";
            this.txtCashBRLAdm.Size = new System.Drawing.Size(100, 20);
            this.txtCashBRLAdm.TabIndex = 6;
            this.txtCashBRLAdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCashBRLAdm.TextChanged += new System.EventHandler(this.txtCashBRLAdm_TextChanged);
            this.txtCashBRLAdm.Leave += new System.EventHandler(this.txtCashBRLAdm_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "LB";
            // 
            // txtNavUSDNest
            // 
            this.txtNavUSDNest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavUSDNest.ForeColor = System.Drawing.Color.Black;
            this.txtNavUSDNest.Location = new System.Drawing.Point(101, 128);
            this.txtNavUSDNest.Name = "txtNavUSDNest";
            this.txtNavUSDNest.ReadOnly = true;
            this.txtNavUSDNest.Size = new System.Drawing.Size(100, 13);
            this.txtNavUSDNest.TabIndex = 32;
            this.txtNavUSDNest.TabStop = false;
            this.txtNavUSDNest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNavBRLNest
            // 
            this.txtNavBRLNest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavBRLNest.ForeColor = System.Drawing.Color.Black;
            this.txtNavBRLNest.Location = new System.Drawing.Point(101, 102);
            this.txtNavBRLNest.Name = "txtNavBRLNest";
            this.txtNavBRLNest.ReadOnly = true;
            this.txtNavBRLNest.Size = new System.Drawing.Size(100, 13);
            this.txtNavBRLNest.TabIndex = 31;
            this.txtNavBRLNest.TabStop = false;
            this.txtNavBRLNest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(235, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Administrator";
            // 
            // txtNAVNest
            // 
            this.txtNAVNest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVNest.ForeColor = System.Drawing.Color.Black;
            this.txtNAVNest.Location = new System.Drawing.Point(101, 76);
            this.txtNAVNest.Name = "txtNAVNest";
            this.txtNAVNest.ReadOnly = true;
            this.txtNAVNest.Size = new System.Drawing.Size(100, 13);
            this.txtNAVNest.TabIndex = 30;
            this.txtNAVNest.TabStop = false;
            this.txtNAVNest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNavUSDAdm
            // 
            this.txtNavUSDAdm.Location = new System.Drawing.Point(221, 126);
            this.txtNavUSDAdm.Name = "txtNavUSDAdm";
            this.txtNavUSDAdm.Size = new System.Drawing.Size(100, 20);
            this.txtNavUSDAdm.TabIndex = 5;
            this.txtNavUSDAdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNavUSDAdm.TextChanged += new System.EventHandler(this.txtNavUSDAdm_TextChanged);
            this.txtNavUSDAdm.Leave += new System.EventHandler(this.txtNavUSDAdm_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "NAV";
            // 
            // txtNavBRLAdm
            // 
            this.txtNavBRLAdm.Location = new System.Drawing.Point(221, 100);
            this.txtNavBRLAdm.Name = "txtNavBRLAdm";
            this.txtNavBRLAdm.Size = new System.Drawing.Size(100, 20);
            this.txtNavBRLAdm.TabIndex = 4;
            this.txtNavBRLAdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNavBRLAdm.TextChanged += new System.EventHandler(this.txtNavBRLAdm_TextChanged);
            this.txtNavBRLAdm.Leave += new System.EventHandler(this.txtNavBRLAdm_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Cash BRL";
            // 
            // txtNAVAdm
            // 
            this.txtNAVAdm.Location = new System.Drawing.Point(221, 74);
            this.txtNAVAdm.Name = "txtNAVAdm";
            this.txtNAVAdm.Size = new System.Drawing.Size(100, 20);
            this.txtNAVAdm.TabIndex = 3;
            this.txtNAVAdm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNAVAdm.TextChanged += new System.EventHandler(this.txtNAVAdm_TextChanged);
            this.txtNAVAdm.Leave += new System.EventHandler(this.txtNAVAdm_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Cash USD";
            // 
            // txtCashBRLNest
            // 
            this.txtCashBRLNest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashBRLNest.ForeColor = System.Drawing.Color.Black;
            this.txtCashBRLNest.Location = new System.Drawing.Point(101, 154);
            this.txtCashBRLNest.Name = "txtCashBRLNest";
            this.txtCashBRLNest.ReadOnly = true;
            this.txtCashBRLNest.Size = new System.Drawing.Size(100, 13);
            this.txtCashBRLNest.TabIndex = 33;
            this.txtCashBRLNest.TabStop = false;
            this.txtCashBRLNest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "NAV USD";
            // 
            // txtCashUSDNest
            // 
            this.txtCashUSDNest.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashUSDNest.ForeColor = System.Drawing.Color.Black;
            this.txtCashUSDNest.Location = new System.Drawing.Point(101, 180);
            this.txtCashUSDNest.Name = "txtCashUSDNest";
            this.txtCashUSDNest.ReadOnly = true;
            this.txtCashUSDNest.Size = new System.Drawing.Size(100, 13);
            this.txtCashUSDNest.TabIndex = 34;
            this.txtCashUSDNest.TabStop = false;
            this.txtCashUSDNest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "NAV BRL";
            // 
            // dgPL
            // 
            this.dgPL.AllowUserToAddRows = false;
            this.dgPL.AllowUserToDeleteRows = false;
            this.dgPL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgPL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgPL.BackgroundColor = System.Drawing.Color.Gray;
            this.dgPL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Save});
            this.dgPL.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgPL.Location = new System.Drawing.Point(490, 12);
            this.dgPL.Name = "dgPL";
            this.dgPL.RowHeadersVisible = false;
            this.dgPL.RowHeadersWidth = 4;
            this.dgPL.Size = new System.Drawing.Size(336, 252);
            this.dgPL.TabIndex = 90;
            this.dgPL.TabStop = false;
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
            // frmConfirmPL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(840, 326);
            this.Controls.Add(this.dgPL);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Name = "frmConfirmPL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Confirm NAV";
            this.Load += new System.EventHandler(this.frmConfirmPL_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.ComboBox CmbPortfolio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCashBRLNest;
        private System.Windows.Forms.TextBox txtCashUSDNest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNavUSDAdm;
        private System.Windows.Forms.TextBox txtNavBRLAdm;
        private System.Windows.Forms.TextBox txtNAVAdm;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNavBRLNest;
        private System.Windows.Forms.TextBox txtNavUSDNest;
        private System.Windows.Forms.TextBox txtCashBRLAdm;
        private System.Windows.Forms.TextBox txtCashUSDAdm;
        private System.Windows.Forms.DataGridView dgPL;
        private System.Windows.Forms.DataGridViewButtonColumn Save;
        private System.Windows.Forms.TextBox txtCashUSDAdj;
        private System.Windows.Forms.TextBox txtCashBRLAdj;
        private System.Windows.Forms.TextBox txtNavUSDAdj;
        private System.Windows.Forms.TextBox txtNavBRLAdj;
        private System.Windows.Forms.TextBox txtNAVAdj;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNAVNest;
    }
}