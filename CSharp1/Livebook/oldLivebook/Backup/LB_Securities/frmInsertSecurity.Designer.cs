namespace SGN
{
    partial class frmInsertSecurity
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbIssuer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbPriceTable = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbInstrument = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbAsset_Class = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtNestTicker = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNewIssuer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(130, 56);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 20);
            this.txtName.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(43, 59);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 13);
            this.label23.TabIndex = 68;
            this.label23.Text = "Name";
            // 
            // cmbIssuer
            // 
            this.cmbIssuer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbIssuer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbIssuer.FormattingEnabled = true;
            this.cmbIssuer.Location = new System.Drawing.Point(130, 108);
            this.cmbIssuer.Name = "cmbIssuer";
            this.cmbIssuer.Size = new System.Drawing.Size(260, 21);
            this.cmbIssuer.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Issuer";
            // 
            // cmbPriceTable
            // 
            this.cmbPriceTable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPriceTable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPriceTable.FormattingEnabled = true;
            this.cmbPriceTable.Location = new System.Drawing.Point(130, 29);
            this.cmbPriceTable.Name = "cmbPriceTable";
            this.cmbPriceTable.Size = new System.Drawing.Size(136, 21);
            this.cmbPriceTable.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 76;
            this.label10.Text = "Type";
            // 
            // cmbInstrument
            // 
            this.cmbInstrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbInstrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInstrument.FormattingEnabled = true;
            this.cmbInstrument.Location = new System.Drawing.Point(130, 135);
            this.cmbInstrument.Name = "cmbInstrument";
            this.cmbInstrument.Size = new System.Drawing.Size(136, 21);
            this.cmbInstrument.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(43, 138);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 78;
            this.label15.Text = "Instrument ";
            // 
            // cmbAsset_Class
            // 
            this.cmbAsset_Class.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAsset_Class.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAsset_Class.FormattingEnabled = true;
            this.cmbAsset_Class.Location = new System.Drawing.Point(130, 162);
            this.cmbAsset_Class.Name = "cmbAsset_Class";
            this.cmbAsset_Class.Size = new System.Drawing.Size(136, 21);
            this.cmbAsset_Class.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "Asset Class";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(130, 189);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(136, 21);
            this.cmbCurrency.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(43, 192);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 82;
            this.label12.Text = "Currency";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(105, 234);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(101, 38);
            this.cmdInsert.TabIndex = 8;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(261, 234);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(101, 38);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtNestTicker
            // 
            this.txtNestTicker.Location = new System.Drawing.Point(130, 82);
            this.txtNestTicker.Name = "txtNestTicker";
            this.txtNestTicker.Size = new System.Drawing.Size(136, 20);
            this.txtNestTicker.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Nest Ticker";
            // 
            // lblNewIssuer
            // 
            this.lblNewIssuer.AutoSize = true;
            this.lblNewIssuer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewIssuer.ForeColor = System.Drawing.Color.Blue;
            this.lblNewIssuer.Location = new System.Drawing.Point(396, 111);
            this.lblNewIssuer.Name = "lblNewIssuer";
            this.lblNewIssuer.Size = new System.Drawing.Size(60, 13);
            this.lblNewIssuer.TabIndex = 87;
            this.lblNewIssuer.Text = "New Issuer";
            this.lblNewIssuer.Click += new System.EventHandler(this.lblNewIssuer_Click);
            // 
            // frmInsertSecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 286);
            this.Controls.Add(this.lblNewIssuer);
            this.Controls.Add(this.txtNestTicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.cmbCurrency);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbAsset_Class);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbInstrument);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbPriceTable);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cmbIssuer);
            this.Controls.Add(this.label9);
            this.Name = "frmInsertSecurity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insert New Security";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmInsertSecurity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbIssuer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbPriceTable;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbInstrument;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbAsset_Class;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtNestTicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNewIssuer;
    }
}