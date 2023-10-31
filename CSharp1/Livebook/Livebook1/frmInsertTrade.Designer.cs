namespace SGN
{
    partial class frmInsertTrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertTrade));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtVFpend = new System.Windows.Forms.TextBox();
            this.txtPrecoPend = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQtdpend = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrecoOrdem = new System.Windows.Forms.TextBox();
            this.txtVFOrdem = new System.Windows.Forms.TextBox();
            this.txtQtdordem = new System.Windows.Forms.TextBox();
            this.txtPrecoExec = new System.Windows.Forms.TextBox();
            this.txtVfExec = new System.Windows.Forms.TextBox();
            this.txtQtdExec = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtRebate = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtValorFinanc = new System.Windows.Forms.TextBox();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAtivo = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCart = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(171, 296);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(95, 30);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(43, 296);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(94, 30);
            this.cmdInsert.TabIndex = 11;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVFpend);
            this.groupBox1.Controls.Add(this.txtPrecoPend);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtQtdpend);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPrecoOrdem);
            this.groupBox1.Controls.Add(this.txtVFOrdem);
            this.groupBox1.Controls.Add(this.txtQtdordem);
            this.groupBox1.Controls.Add(this.txtPrecoExec);
            this.groupBox1.Controls.Add(this.txtVfExec);
            this.groupBox1.Controls.Add(this.txtQtdExec);
            this.groupBox1.Location = new System.Drawing.Point(10, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 125);
            this.groupBox1.TabIndex = 97;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Executadas";
            // 
            // txtVFpend
            // 
            this.txtVFpend.Location = new System.Drawing.Point(203, 98);
            this.txtVFpend.Name = "txtVFpend";
            this.txtVFpend.ReadOnly = true;
            this.txtVFpend.Size = new System.Drawing.Size(80, 20);
            this.txtVFpend.TabIndex = 119;
            this.txtVFpend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrecoPend
            // 
            this.txtPrecoPend.Location = new System.Drawing.Point(135, 98);
            this.txtPrecoPend.Name = "txtPrecoPend";
            this.txtPrecoPend.ReadOnly = true;
            this.txtPrecoPend.Size = new System.Drawing.Size(62, 20);
            this.txtPrecoPend.TabIndex = 118;
            this.txtPrecoPend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 117;
            this.label5.Text = "Balance";
            // 
            // txtQtdpend
            // 
            this.txtQtdpend.Location = new System.Drawing.Point(70, 98);
            this.txtQtdpend.Name = "txtQtdpend";
            this.txtQtdpend.ReadOnly = true;
            this.txtQtdpend.Size = new System.Drawing.Size(59, 20);
            this.txtQtdpend.TabIndex = 116;
            this.txtQtdpend.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 114;
            this.label9.Text = "Order";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 113;
            this.label8.Text = "Executed";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(227, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 112;
            this.label1.Text = "Cash Flow";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(83, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 111;
            this.label4.Text = "Quantity";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 110;
            this.label7.Text = "Price";
            // 
            // txtPrecoOrdem
            // 
            this.txtPrecoOrdem.Location = new System.Drawing.Point(135, 46);
            this.txtPrecoOrdem.Name = "txtPrecoOrdem";
            this.txtPrecoOrdem.ReadOnly = true;
            this.txtPrecoOrdem.Size = new System.Drawing.Size(62, 20);
            this.txtPrecoOrdem.TabIndex = 109;
            this.txtPrecoOrdem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVFOrdem
            // 
            this.txtVFOrdem.Location = new System.Drawing.Point(203, 46);
            this.txtVFOrdem.Name = "txtVFOrdem";
            this.txtVFOrdem.ReadOnly = true;
            this.txtVFOrdem.Size = new System.Drawing.Size(80, 20);
            this.txtVFOrdem.TabIndex = 108;
            this.txtVFOrdem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQtdordem
            // 
            this.txtQtdordem.Location = new System.Drawing.Point(70, 46);
            this.txtQtdordem.Name = "txtQtdordem";
            this.txtQtdordem.ReadOnly = true;
            this.txtQtdordem.Size = new System.Drawing.Size(59, 20);
            this.txtQtdordem.TabIndex = 107;
            this.txtQtdordem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPrecoExec
            // 
            this.txtPrecoExec.Location = new System.Drawing.Point(135, 72);
            this.txtPrecoExec.Name = "txtPrecoExec";
            this.txtPrecoExec.ReadOnly = true;
            this.txtPrecoExec.Size = new System.Drawing.Size(62, 20);
            this.txtPrecoExec.TabIndex = 97;
            this.txtPrecoExec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtVfExec
            // 
            this.txtVfExec.Location = new System.Drawing.Point(203, 72);
            this.txtVfExec.Name = "txtVfExec";
            this.txtVfExec.ReadOnly = true;
            this.txtVfExec.Size = new System.Drawing.Size(80, 20);
            this.txtVfExec.TabIndex = 96;
            this.txtVfExec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQtdExec
            // 
            this.txtQtdExec.Location = new System.Drawing.Point(70, 72);
            this.txtQtdExec.Name = "txtQtdExec";
            this.txtQtdExec.ReadOnly = true;
            this.txtQtdExec.Size = new System.Drawing.Size(59, 20);
            this.txtQtdExec.TabIndex = 95;
            this.txtQtdExec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtRebate);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtValorFinanc);
            this.groupBox3.Controls.Add(this.txtQtd);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPreco);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(10, 208);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 65);
            this.groupBox3.TabIndex = 99;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trade";
            // 
            // txtRebate
            // 
            this.txtRebate.Location = new System.Drawing.Point(230, 38);
            this.txtRebate.Name = "txtRebate";
            this.txtRebate.Size = new System.Drawing.Size(53, 20);
            this.txtRebate.TabIndex = 63;
            this.txtRebate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(241, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 13);
            this.label13.TabIndex = 64;
            this.label13.Text = "Rebate";
            // 
            // txtValorFinanc
            // 
            this.txtValorFinanc.Location = new System.Drawing.Point(139, 38);
            this.txtValorFinanc.Name = "txtValorFinanc";
            this.txtValorFinanc.Size = new System.Drawing.Size(80, 20);
            this.txtValorFinanc.TabIndex = 59;
            this.txtValorFinanc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(9, 38);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(59, 20);
            this.txtQtd.TabIndex = 57;
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQtd.TextChanged += new System.EventHandler(this.txtQtd_TextChanged);
            this.txtQtd.Leave += new System.EventHandler(this.TxtQtd_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "Quantity";
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(74, 38);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(62, 20);
            this.txtPreco.TabIndex = 58;
            this.txtPreco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPreco.TextChanged += new System.EventHandler(this.txtPreco_TextChanged);
            this.txtPreco.Leave += new System.EventHandler(this.TxtPreco_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "Cash Flow";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Price";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.lblAtivo);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.lblCart);
            this.groupBox5.Location = new System.Drawing.Point(10, 11);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(292, 60);
            this.groupBox5.TabIndex = 101;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Order";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 38);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 110;
            this.label12.Text = "Ticker";
            // 
            // lblAtivo
            // 
            this.lblAtivo.AutoSize = true;
            this.lblAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtivo.Location = new System.Drawing.Point(52, 35);
            this.lblAtivo.Name = "lblAtivo";
            this.lblAtivo.Size = new System.Drawing.Size(59, 16);
            this.lblAtivo.TabIndex = 109;
            this.lblAtivo.Text = "label11";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 108;
            this.label11.Text = "Portfólio";
            // 
            // lblCart
            // 
            this.lblCart.AutoSize = true;
            this.lblCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCart.Location = new System.Drawing.Point(52, 13);
            this.lblCart.Name = "lblCart";
            this.lblCart.Size = new System.Drawing.Size(59, 16);
            this.lblCart.TabIndex = 107;
            this.lblCart.Text = "label11";
            // 
            // frmInsertTrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 346);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdInsert);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsertTrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert Trade";
            this.Load += new System.EventHandler(this.frmInsertTrade_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPrecoExec;
        private System.Windows.Forms.TextBox txtVfExec;
        private System.Windows.Forms.TextBox txtQtdExec;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtValorFinanc;
        private System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAtivo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCart;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrecoOrdem;
        private System.Windows.Forms.TextBox txtVFOrdem;
        private System.Windows.Forms.TextBox txtQtdordem;
        private System.Windows.Forms.TextBox txtRebate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtVFpend;
        private System.Windows.Forms.TextBox txtPrecoPend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQtdpend;
    }
}