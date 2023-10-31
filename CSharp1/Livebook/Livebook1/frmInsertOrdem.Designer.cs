namespace SGN
{
    partial class frmInsertOrdem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertOrdem));
            this.cmbCarteiras = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbMercado = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpValOrdem = new System.Windows.Forms.DateTimePicker();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbSubEstrategia = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbEstrategia = new System.Windows.Forms.ComboBox();
            this.txtValorFinanc = new System.Windows.Forms.TextBox();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAtivo = new System.Windows.Forms.ComboBox();
            this.chkGtc = new System.Windows.Forms.CheckBox();
            this.cmbBrooker = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbCarteiras
            // 
            this.cmbCarteiras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarteiras.FormattingEnabled = true;
            this.cmbCarteiras.Location = new System.Drawing.Point(69, 19);
            this.cmbCarteiras.Name = "cmbCarteiras";
            this.cmbCarteiras.Size = new System.Drawing.Size(125, 21);
            this.cmbCarteiras.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 74;
            this.label13.Text = "Portfolio";
            // 
            // cmbMercado
            // 
            this.cmbMercado.FormattingEnabled = true;
            this.cmbMercado.Location = new System.Drawing.Point(293, 19);
            this.cmbMercado.Name = "cmbMercado";
            this.cmbMercado.Size = new System.Drawing.Size(134, 21);
            this.cmbMercado.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(227, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 13);
            this.label12.TabIndex = 72;
            this.label12.Text = "Order Type";
            // 
            // dtpValOrdem
            // 
            this.dtpValOrdem.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpValOrdem.Location = new System.Drawing.Point(309, 47);
            this.dtpValOrdem.Name = "dtpValOrdem";
            this.dtpValOrdem.Size = new System.Drawing.Size(107, 20);
            this.dtpValOrdem.TabIndex = 3;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(267, 159);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(95, 35);
            this.cmdCancel.TabIndex = 12;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(123, 159);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(94, 36);
            this.cmdInsert.TabIndex = 11;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(234, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "Expiration";
            // 
            // cmbSubEstrategia
            // 
            this.cmbSubEstrategia.FormattingEnabled = true;
            this.cmbSubEstrategia.Location = new System.Drawing.Point(304, 73);
            this.cmbSubEstrategia.Name = "cmbSubEstrategia";
            this.cmbSubEstrategia.Size = new System.Drawing.Size(112, 21);
            this.cmbSubEstrategia.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(222, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 60;
            this.label7.Text = "Sub-Estrategy";
            // 
            // cmbEstrategia
            // 
            this.cmbEstrategia.FormattingEnabled = true;
            this.cmbEstrategia.Location = new System.Drawing.Point(69, 73);
            this.cmbEstrategia.Name = "cmbEstrategia";
            this.cmbEstrategia.Size = new System.Drawing.Size(125, 21);
            this.cmbEstrategia.TabIndex = 4;
            this.cmbEstrategia.SelectedIndexChanged += new System.EventHandler(this.cmbEstrategia_SelectedIndexChanged);
            // 
            // txtValorFinanc
            // 
            this.txtValorFinanc.Location = new System.Drawing.Point(368, 100);
            this.txtValorFinanc.Name = "txtValorFinanc";
            this.txtValorFinanc.Size = new System.Drawing.Size(100, 20);
            this.txtValorFinanc.TabIndex = 10;
            this.txtValorFinanc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorFinanc.TextChanged += new System.EventHandler(this.txtValorFinanc_TextChanged);
            this.txtValorFinanc.Leave += new System.EventHandler(this.txtPreco_Leave);
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(69, 100);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(94, 20);
            this.txtQtd.TabIndex = 9;
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQtd.TextChanged += new System.EventHandler(this.txtQtd_TextChanged);
            this.txtQtd.Leave += new System.EventHandler(this.txtPreco_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Quantity";
            // 
            // txtPreco
            // 
            this.txtPreco.Location = new System.Drawing.Point(212, 100);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(74, 20);
            this.txtPreco.TabIndex = 8;
            this.txtPreco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPreco.TextChanged += new System.EventHandler(this.txtPreco_TextChanged);
            this.txtPreco.Leave += new System.EventHandler(this.txtPreco_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 54;
            this.label4.Text = "Strategy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(306, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Cash Flow";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 51;
            this.label1.Text = "Ticker";
            // 
            // cmbAtivo
            // 
            this.cmbAtivo.FormattingEnabled = true;
            this.cmbAtivo.Location = new System.Drawing.Point(69, 46);
            this.cmbAtivo.Name = "cmbAtivo";
            this.cmbAtivo.Size = new System.Drawing.Size(125, 21);
            this.cmbAtivo.TabIndex = 2;
            // 
            // chkGtc
            // 
            this.chkGtc.AutoSize = true;
            this.chkGtc.Location = new System.Drawing.Point(422, 53);
            this.chkGtc.Name = "chkGtc";
            this.chkGtc.Size = new System.Drawing.Size(78, 17);
            this.chkGtc.TabIndex = 75;
            this.chkGtc.Text = "GTC - VAC";
            this.chkGtc.UseVisualStyleBackColor = true;
            this.chkGtc.CheckedChanged += new System.EventHandler(this.chkGtc_CheckedChanged);
            // 
            // cmbBrooker
            // 
            this.cmbBrooker.FormattingEnabled = true;
            this.cmbBrooker.Location = new System.Drawing.Point(69, 126);
            this.cmbBrooker.Name = "cmbBrooker";
            this.cmbBrooker.Size = new System.Drawing.Size(121, 21);
            this.cmbBrooker.TabIndex = 76;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 77;
            this.label5.Text = "Brooker";
            // 
            // frmInsertOrdem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 205);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbBrooker);
            this.Controls.Add(this.chkGtc);
            this.Controls.Add(this.cmbCarteiras);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbMercado);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dtpValOrdem);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbSubEstrategia);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbEstrategia);
            this.Controls.Add(this.txtValorFinanc);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbAtivo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsertOrdem";
            this.Text = "Insert Orders";
            this.Load += new System.EventHandler(this.frmInsertOrdem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCarteiras;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbMercado;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpValOrdem;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSubEstrategia;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbEstrategia;
        private System.Windows.Forms.TextBox txtValorFinanc;
        private System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbAtivo;
        private System.Windows.Forms.CheckBox chkGtc;
        private System.Windows.Forms.ComboBox cmbBrooker;
        private System.Windows.Forms.Label label5;
    }
}