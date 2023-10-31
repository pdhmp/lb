namespace LiveBook
{
    partial class frmInsertLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertLoan));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFundo = new System.Windows.Forms.ComboBox();
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdDom = new System.Windows.Forms.RadioButton();
            this.rdTom = new System.Windows.Forms.RadioButton();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RdFixo = new System.Windows.Forms.RadioButton();
            this.rdRev = new System.Windows.Forms.RadioButton();
            this.rdNormal = new System.Windows.Forms.RadioButton();
            this.cmbCorretora = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTaxa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpVencto = new System.Windows.Forms.DateTimePicker();
            this.txtRelID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCBLCId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Portfolio";
            // 
            // cmbFundo
            // 
            this.cmbFundo.FormattingEnabled = true;
            this.cmbFundo.Location = new System.Drawing.Point(56, 11);
            this.cmbFundo.Name = "cmbFundo";
            this.cmbFundo.Size = new System.Drawing.Size(220, 21);
            this.cmbFundo.TabIndex = 0;
            this.cmbFundo.SelectedIndexChanged += new System.EventHandler(this.cmbFundo_SelectedIndexChanged);
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(445, 52);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(85, 20);
            this.dtpIni.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdDom);
            this.groupBox1.Controls.Add(this.rdTom);
            this.groupBox1.Location = new System.Drawing.Point(8, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(145, 47);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Side";
            // 
            // rdDom
            // 
            this.rdDom.AutoSize = true;
            this.rdDom.Location = new System.Drawing.Point(79, 19);
            this.rdDom.Name = "rdDom";
            this.rdDom.Size = new System.Drawing.Size(60, 17);
            this.rdDom.TabIndex = 6;
            this.rdDom.TabStop = true;
            this.rdDom.Text = "Doador";
            this.rdDom.UseVisualStyleBackColor = true;
            // 
            // rdTom
            // 
            this.rdTom.AutoSize = true;
            this.rdTom.Location = new System.Drawing.Point(6, 19);
            this.rdTom.Name = "rdTom";
            this.rdTom.Size = new System.Drawing.Size(67, 17);
            this.rdTom.TabIndex = 5;
            this.rdTom.TabStop = true;
            this.rdTom.Text = "Tomador";
            this.rdTom.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(854, 11);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(92, 28);
            this.cmdOK.TabIndex = 14;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Trade Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RdFixo);
            this.groupBox2.Controls.Add(this.rdRev);
            this.groupBox2.Controls.Add(this.rdNormal);
            this.groupBox2.Location = new System.Drawing.Point(159, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(204, 47);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Type";
            // 
            // RdFixo
            // 
            this.RdFixo.AutoSize = true;
            this.RdFixo.Location = new System.Drawing.Point(153, 19);
            this.RdFixo.Name = "RdFixo";
            this.RdFixo.Size = new System.Drawing.Size(44, 17);
            this.RdFixo.TabIndex = 9;
            this.RdFixo.TabStop = true;
            this.RdFixo.Text = "Fixo";
            this.RdFixo.UseVisualStyleBackColor = true;
            // 
            // rdRev
            // 
            this.rdRev.AutoSize = true;
            this.rdRev.Location = new System.Drawing.Point(70, 19);
            this.rdRev.Name = "rdRev";
            this.rdRev.Size = new System.Drawing.Size(77, 17);
            this.rdRev.TabIndex = 8;
            this.rdRev.TabStop = true;
            this.rdRev.Text = "Reversível";
            this.rdRev.UseVisualStyleBackColor = true;
            // 
            // rdNormal
            // 
            this.rdNormal.AutoSize = true;
            this.rdNormal.Location = new System.Drawing.Point(6, 19);
            this.rdNormal.Name = "rdNormal";
            this.rdNormal.Size = new System.Drawing.Size(58, 17);
            this.rdNormal.TabIndex = 7;
            this.rdNormal.TabStop = true;
            this.rdNormal.Text = "Normal";
            this.rdNormal.UseVisualStyleBackColor = true;
            // 
            // cmbCorretora
            // 
            this.cmbCorretora.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCorretora.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCorretora.FormattingEnabled = true;
            this.cmbCorretora.Location = new System.Drawing.Point(326, 11);
            this.cmbCorretora.Name = "cmbCorretora";
            this.cmbCorretora.Size = new System.Drawing.Size(115, 21);
            this.cmbCorretora.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Broker";
            // 
            // cmbTicker
            // 
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTicker.FormattingEnabled = true;
            this.cmbTicker.Location = new System.Drawing.Point(490, 11);
            this.cmbTicker.Name = "cmbTicker";
            this.cmbTicker.Size = new System.Drawing.Size(104, 21);
            this.cmbTicker.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(447, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Ticker";
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(652, 11);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(74, 20);
            this.txtQtd.TabIndex = 3;
            this.txtQtd.Leave += new System.EventHandler(this.txtQtd_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(600, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Quantity";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(854, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 28);
            this.button2.TabIndex = 15;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(731, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Rate";
            // 
            // txtTaxa
            // 
            this.txtTaxa.Location = new System.Drawing.Point(767, 11);
            this.txtTaxa.Name = "txtTaxa";
            this.txtTaxa.Size = new System.Drawing.Size(57, 20);
            this.txtTaxa.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(542, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Expiration";
            // 
            // dtpVencto
            // 
            this.dtpVencto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVencto.Location = new System.Drawing.Point(603, 52);
            this.dtpVencto.Name = "dtpVencto";
            this.dtpVencto.Size = new System.Drawing.Size(83, 20);
            this.dtpVencto.TabIndex = 11;
            // 
            // txtRelID
            // 
            this.txtRelID.Location = new System.Drawing.Point(767, 61);
            this.txtRelID.Name = "txtRelID";
            this.txtRelID.Size = new System.Drawing.Size(74, 20);
            this.txtRelID.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(699, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Loan ID:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(696, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "CBLC ID:";
            // 
            // txtCBLCId
            // 
            this.txtCBLCId.Location = new System.Drawing.Point(767, 36);
            this.txtCBLCId.Name = "txtCBLCId";
            this.txtCBLCId.Size = new System.Drawing.Size(74, 20);
            this.txtCBLCId.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(826, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "%";
            // 
            // frmInsertLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(965, 92);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCBLCId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRelID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpVencto);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTaxa);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.cmbTicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbCorretora);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpIni);
            this.Controls.Add(this.cmbFundo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsertLoan";
            this.Text = "Insert Stock Loan";
            this.Load += new System.EventHandler(this.frmInsertLoan_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cmbFundo;
        public System.Windows.Forms.DateTimePicker dtpIni;
        public System.Windows.Forms.RadioButton rdDom;
        public System.Windows.Forms.RadioButton rdTom;
        public System.Windows.Forms.RadioButton rdRev;
        public System.Windows.Forms.RadioButton rdNormal;
        public System.Windows.Forms.ComboBox cmbCorretora;
        public System.Windows.Forms.ComboBox cmbTicker;
        public System.Windows.Forms.TextBox txtQtd;
        public System.Windows.Forms.TextBox txtTaxa;
        public System.Windows.Forms.RadioButton RdFixo;
        public System.Windows.Forms.DateTimePicker dtpVencto;
        public System.Windows.Forms.TextBox txtRelID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txtCBLCId;
        public System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label10;
    }
}