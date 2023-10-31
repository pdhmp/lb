namespace LiveBook
{
    partial class frmInsertTxMellon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertTxMellon));
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMile = new System.Windows.Forms.TextBox();
            this.txtMile30 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQuant = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtArb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMulti = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAcoes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNA1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtArb30 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAB1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(101, 16);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(22, 348);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 1;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mile FIC";
            // 
            // txtMile
            // 
            this.txtMile.Location = new System.Drawing.Point(79, 23);
            this.txtMile.Name = "txtMile";
            this.txtMile.Size = new System.Drawing.Size(100, 20);
            this.txtMile.TabIndex = 3;
            // 
            // txtMile30
            // 
            this.txtMile30.Location = new System.Drawing.Point(79, 49);
            this.txtMile30.Name = "txtMile30";
            this.txtMile30.Size = new System.Drawing.Size(100, 20);
            this.txtMile30.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mile 30 FIC";
            // 
            // txtQuant
            // 
            this.txtQuant.Location = new System.Drawing.Point(79, 127);
            this.txtQuant.Name = "txtQuant";
            this.txtQuant.Size = new System.Drawing.Size(100, 20);
            this.txtQuant.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Quant FIC";
            // 
            // txtArb
            // 
            this.txtArb.Location = new System.Drawing.Point(79, 101);
            this.txtArb.Name = "txtArb";
            this.txtArb.Size = new System.Drawing.Size(100, 20);
            this.txtArb.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Hedge FIC";
            // 
            // txtMulti
            // 
            this.txtMulti.Location = new System.Drawing.Point(79, 153);
            this.txtMulti.Name = "txtMulti";
            this.txtMulti.Size = new System.Drawing.Size(100, 20);
            this.txtMulti.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Arb FIC";
            // 
            // txtAcoes
            // 
            this.txtAcoes.Location = new System.Drawing.Point(79, 75);
            this.txtAcoes.Name = "txtAcoes";
            this.txtAcoes.Size = new System.Drawing.Size(100, 20);
            this.txtAcoes.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Ações FIC";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNA1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtArb30);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMile);
            this.groupBox1.Controls.Add(this.txtAcoes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtAB1);
            this.groupBox1.Controls.Add(this.txtMulti);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtArb);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMile30);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtQuant);
            this.groupBox1.Location = new System.Drawing.Point(22, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 283);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Funds";
            // 
            // txtNA1
            // 
            this.txtNA1.Location = new System.Drawing.Point(78, 234);
            this.txtNA1.Name = "txtNA1";
            this.txtNA1.Size = new System.Drawing.Size(100, 20);
            this.txtNA1.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(26, 237);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "NA1 FIC";
            // 
            // txtArb30
            // 
            this.txtArb30.Location = new System.Drawing.Point(79, 206);
            this.txtArb30.Name = "txtArb30";
            this.txtArb30.Size = new System.Drawing.Size(100, 20);
            this.txtArb30.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Arb 30 FIC";
            // 
            // txtAB1
            // 
            this.txtAB1.Location = new System.Drawing.Point(79, 179);
            this.txtAB1.Name = "txtAB1";
            this.txtAB1.Size = new System.Drawing.Size(100, 20);
            this.txtAB1.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "AB1 FIC";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Tx Date";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(141, 348);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 18;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmInsertTxMellon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(238, 395);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.dtpDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsertTxMellon";
            this.Text = "Insert % Tx Mellon";
            this.Load += new System.EventHandler(this.frmInsertTxMellon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMile;
        private System.Windows.Forms.TextBox txtMile30;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQuant;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtArb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMulti;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAcoes;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox txtAB1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArb30;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNA1;
        private System.Windows.Forms.Label label10;
    }
}