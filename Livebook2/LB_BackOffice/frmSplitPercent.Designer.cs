namespace LiveBook
{
    partial class frmSplitPercent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplitPercent));
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHedge = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBravo = new System.Windows.Forms.TextBox();
            this.txtNFund = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMH = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.CmdCancel = new System.Windows.Forms.Button();
            this.dtpDataPL = new System.Windows.Forms.DateTimePicker();
            this.dgPL = new System.Windows.Forms.DataGridView();
            this.Save = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.optMH_Fund = new System.Windows.Forms.RadioButton();
            this.optFiaHedge = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPL)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdInsert
            // 
            this.cmdInsert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cmdInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInsert.Location = new System.Drawing.Point(98, 311);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = false;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            this.cmdInsert.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHedge);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTotal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtBravo);
            this.groupBox1.Controls.Add(this.txtNFund);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtMH);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Split Percent";
            // 
            // txtHedge
            // 
            this.txtHedge.Location = new System.Drawing.Point(337, 35);
            this.txtHedge.Name = "txtHedge";
            this.txtHedge.Size = new System.Drawing.Size(66, 20);
            this.txtHedge.TabIndex = 6;
            this.txtHedge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHedge.TextChanged += new System.EventHandler(this.txtHedge_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(343, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 41;
            this.label7.Text = "Hedge";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(306, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "%";
            // 
            // txtTotal
            // 
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Location = new System.Drawing.Point(449, 35);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(50, 13);
            this.txtTotal.TabIndex = 36;
            this.txtTotal.Text = "0,00";
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(451, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Total Split";
            // 
            // txtBravo
            // 
            this.txtBravo.Location = new System.Drawing.Point(232, 35);
            this.txtBravo.Name = "txtBravo";
            this.txtBravo.Size = new System.Drawing.Size(66, 20);
            this.txtBravo.TabIndex = 5;
            this.txtBravo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBravo.TextChanged += new System.EventHandler(this.txtBravo_TextChanged);
            // 
            // txtNFund
            // 
            this.txtNFund.Location = new System.Drawing.Point(126, 35);
            this.txtNFund.Name = "txtNFund";
            this.txtNFund.Size = new System.Drawing.Size(66, 20);
            this.txtNFund.TabIndex = 4;
            this.txtNFund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNFund.TextChanged += new System.EventHandler(this.txtNFund_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(139, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "NestFund";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(244, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Bravo FIA";
            // 
            // txtMH
            // 
            this.txtMH.Location = new System.Drawing.Point(24, 35);
            this.txtMH.Name = "txtMH";
            this.txtMH.Size = new System.Drawing.Size(66, 20);
            this.txtMH.TabIndex = 3;
            this.txtMH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMH.TextChanged += new System.EventHandler(this.txtMH_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(40, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "MH";
            // 
            // CmdCancel
            // 
            this.CmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmdCancel.Location = new System.Drawing.Point(281, 311);
            this.CmdCancel.Name = "CmdCancel";
            this.CmdCancel.Size = new System.Drawing.Size(75, 23);
            this.CmdCancel.TabIndex = 8;
            this.CmdCancel.Text = "Close";
            this.CmdCancel.UseVisualStyleBackColor = false;
            this.CmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            this.CmdCancel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // dtpDataPL
            // 
            this.dtpDataPL.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataPL.Location = new System.Drawing.Point(108, 12);
            this.dtpDataPL.Name = "dtpDataPL";
            this.dtpDataPL.Size = new System.Drawing.Size(91, 20);
            this.dtpDataPL.TabIndex = 0;
            this.dtpDataPL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // dgPL
            // 
            this.dgPL.AllowUserToAddRows = false;
            this.dgPL.AllowUserToDeleteRows = false;
            this.dgPL.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgPL.BackgroundColor = System.Drawing.Color.Gray;
            this.dgPL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Save});
            this.dgPL.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dgPL.Location = new System.Drawing.Point(12, 109);
            this.dgPL.Name = "dgPL";
            this.dgPL.RowHeadersVisible = false;
            this.dgPL.RowHeadersWidth = 4;
            this.dgPL.Size = new System.Drawing.Size(543, 195);
            this.dgPL.TabIndex = 5;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Date Split Percent";
            // 
            // optMH_Fund
            // 
            this.optMH_Fund.AutoSize = true;
            this.optMH_Fund.Location = new System.Drawing.Point(243, 12);
            this.optMH_Fund.Name = "optMH_Fund";
            this.optMH_Fund.Size = new System.Drawing.Size(100, 17);
            this.optMH_Fund.TabIndex = 1;
            this.optMH_Fund.TabStop = true;
            this.optMH_Fund.Text = "MH - Nest Fund";
            this.optMH_Fund.UseVisualStyleBackColor = true;
            this.optMH_Fund.CheckedChanged += new System.EventHandler(this.optMH_Fund_CheckedChanged);
            // 
            // optFiaHedge
            // 
            this.optFiaHedge.AutoSize = true;
            this.optFiaHedge.Location = new System.Drawing.Point(349, 12);
            this.optFiaHedge.Name = "optFiaHedge";
            this.optFiaHedge.Size = new System.Drawing.Size(82, 17);
            this.optFiaHedge.TabIndex = 2;
            this.optFiaHedge.TabStop = true;
            this.optFiaHedge.Text = "FIA - Hedge";
            this.optFiaHedge.UseVisualStyleBackColor = true;
            this.optFiaHedge.CheckedChanged += new System.EventHandler(this.optFiaHedge_CheckedChanged);
            // 
            // frmSplitPercent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(685, 346);
            this.Controls.Add(this.optFiaHedge);
            this.Controls.Add(this.optMH_Fund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpDataPL);
            this.Controls.Add(this.dgPL);
            this.Controls.Add(this.CmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdInsert);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSplitPercent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Split Percent";
            this.Load += new System.EventHandler(this.frmSplitPercent_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CmdCancel;
        private System.Windows.Forms.DateTimePicker dtpDataPL;
        private System.Windows.Forms.DataGridView dgPL;
        private System.Windows.Forms.DataGridViewButtonColumn Save;
        private System.Windows.Forms.TextBox txtBravo;
        private System.Windows.Forms.TextBox txtNFund;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtMH;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHedge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton optMH_Fund;
        private System.Windows.Forms.RadioButton optFiaHedge;
    }
}