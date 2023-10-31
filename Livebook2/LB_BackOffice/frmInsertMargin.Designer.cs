namespace LiveBook
{
    partial class frmInsertMargin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertMargin));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFundo = new System.Windows.Forms.ComboBox();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdRet = new System.Windows.Forms.RadioButton();
            this.rdDep = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCorretora = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            // dtpData
            // 
            this.dtpData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpData.Location = new System.Drawing.Point(450, 54);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(85, 20);
            this.dtpData.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdRet);
            this.groupBox1.Controls.Add(this.rdDep);
            this.groupBox1.Location = new System.Drawing.Point(568, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 47);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Side";
            // 
            // rdRet
            // 
            this.rdRet.AutoSize = true;
            this.rdRet.Location = new System.Drawing.Point(72, 19);
            this.rdRet.Name = "rdRet";
            this.rdRet.Size = new System.Drawing.Size(78, 17);
            this.rdRet.TabIndex = 5;
            this.rdRet.TabStop = true;
            this.rdRet.Text = "Withdrawal";
            this.rdRet.UseVisualStyleBackColor = true;
            // 
            // rdDep
            // 
            this.rdDep.AutoSize = true;
            this.rdDep.Location = new System.Drawing.Point(6, 19);
            this.rdDep.Name = "rdDep";
            this.rdDep.Size = new System.Drawing.Size(61, 17);
            this.rdDep.TabIndex = 4;
            this.rdDep.TabStop = true;
            this.rdDep.Text = "Deposit";
            this.rdDep.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(753, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 28);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(383, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Trade Date";
            // 
            // cmbCorretora
            // 
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
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
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
            this.txtQtd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQtd_KeyDown);
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
            this.button2.Location = new System.Drawing.Point(753, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 28);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmInsertMargin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(852, 96);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.cmbTicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbCorretora);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.cmbFundo);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsertMargin";
            this.Text = "Insert Margin";
            this.Load += new System.EventHandler(this.frmInsertMargin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cmbFundo;
        public System.Windows.Forms.DateTimePicker dtpData;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.RadioButton rdRet;
        public System.Windows.Forms.RadioButton rdDep;
        public System.Windows.Forms.ComboBox cmbCorretora;
        public System.Windows.Forms.ComboBox cmbTicker;
        public System.Windows.Forms.TextBox txtQtd;
        public System.Windows.Forms.Button button2;
    }
}