namespace SGN
{
    partial class frmCurrency
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
            this.txtIdTicker = new System.Windows.Forms.TextBox();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtDescript = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReutersTicker = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBlbTicker = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbIssuer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIdTicker
            // 
            this.txtIdTicker.Enabled = false;
            this.txtIdTicker.Location = new System.Drawing.Point(18, 2);
            this.txtIdTicker.Name = "txtIdTicker";
            this.txtIdTicker.Size = new System.Drawing.Size(52, 20);
            this.txtIdTicker.TabIndex = 75;
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Silver;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Location = new System.Drawing.Point(386, 275);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.BackColor = System.Drawing.Color.Silver;
            this.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClear.Location = new System.Drawing.Point(18, 275);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 7;
            this.cmdClear.Text = "Clear Fields";
            this.cmdClear.UseVisualStyleBackColor = false;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.BackColor = System.Drawing.Color.Silver;
            this.cmdInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInsert.Location = new System.Drawing.Point(257, 275);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = false;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtDescript);
            this.groupBox3.Location = new System.Drawing.Point(12, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(458, 100);
            this.groupBox3.TabIndex = 87;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Description";
            // 
            // txtDescript
            // 
            this.txtDescript.Location = new System.Drawing.Point(6, 19);
            this.txtDescript.Multiline = true;
            this.txtDescript.Name = "txtDescript";
            this.txtDescript.Size = new System.Drawing.Size(443, 75);
            this.txtDescript.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtReutersTicker);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBlbTicker);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtTicker);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.cmbIssuer);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 151);
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Reuters Ticker";
            // 
            // txtReutersTicker
            // 
            this.txtReutersTicker.Location = new System.Drawing.Point(107, 96);
            this.txtReutersTicker.Name = "txtReutersTicker";
            this.txtReutersTicker.Size = new System.Drawing.Size(168, 20);
            this.txtReutersTicker.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Bloomberg Ticker";
            // 
            // txtBlbTicker
            // 
            this.txtBlbTicker.Location = new System.Drawing.Point(106, 122);
            this.txtBlbTicker.Name = "txtBlbTicker";
            this.txtBlbTicker.Size = new System.Drawing.Size(168, 20);
            this.txtBlbTicker.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 59;
            this.label11.Text = "Nest Ticker";
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(107, 70);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.Size = new System.Drawing.Size(168, 20);
            this.txtTicker.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(106, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(282, 20);
            this.txtName.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(65, 47);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 13);
            this.label23.TabIndex = 64;
            this.label23.Text = "Name";
            // 
            // cmbIssuer
            // 
            this.cmbIssuer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbIssuer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbIssuer.FormattingEnabled = true;
            this.cmbIssuer.Location = new System.Drawing.Point(106, 16);
            this.cmbIssuer.Name = "cmbIssuer";
            this.cmbIssuer.Size = new System.Drawing.Size(282, 21);
            this.cmbIssuer.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Issuer";
            // 
            // frmCurrency
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 337);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.txtIdTicker);
            this.Name = "frmCurrency";
            this.Text = "Currency";
            this.Load += new System.EventHandler(this.frmCurrency_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIdTicker;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDescript;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReutersTicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBlbTicker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbIssuer;
        private System.Windows.Forms.Label label9;
    }
}