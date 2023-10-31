namespace LiveBook
{
    partial class frmEditFoward
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditFoward));
            this.txtSpotPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmBroker = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSecurity = new System.Windows.Forms.TextBox();
            this.txtFund = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBook = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSpotPriceWBrokerage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPriceWBrokerage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSpotPrice
            // 
            this.txtSpotPrice.Location = new System.Drawing.Point(78, 44);
            this.txtSpotPrice.Name = "txtSpotPrice";
            this.txtSpotPrice.Size = new System.Drawing.Size(50, 20);
            this.txtSpotPrice.TabIndex = 2;
            this.txtSpotPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpotPrice.Leave += new System.EventHandler(this.txtSpotPrice_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 137;
            this.label5.Text = "Spot Price";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 136;
            this.label14.Text = "Broker";
            // 
            // cmBroker
            // 
            this.cmBroker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmBroker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmBroker.FormattingEnabled = true;
            this.cmBroker.Location = new System.Drawing.Point(54, 15);
            this.cmBroker.Name = "cmBroker";
            this.cmBroker.Size = new System.Drawing.Size(125, 21);
            this.cmBroker.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSecurity);
            this.groupBox3.Controls.Add(this.txtFund);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(520, 48);
            this.groupBox3.TabIndex = 131;
            this.groupBox3.TabStop = false;
            // 
            // txtSecurity
            // 
            this.txtSecurity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSecurity.Enabled = false;
            this.txtSecurity.Location = new System.Drawing.Point(313, 19);
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.Size = new System.Drawing.Size(201, 13);
            this.txtSecurity.TabIndex = 137;
            // 
            // txtFund
            // 
            this.txtFund.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFund.Enabled = false;
            this.txtFund.Location = new System.Drawing.Point(68, 19);
            this.txtFund.Name = "txtFund";
            this.txtFund.Size = new System.Drawing.Size(191, 13);
            this.txtFund.TabIndex = 136;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(15, 19);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 120;
            this.label13.Text = "Portfolio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(272, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 135;
            this.label1.Text = "Ticker";
            // 
            // cmbBook
            // 
            this.cmbBook.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBook.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBook.FormattingEnabled = true;
            this.cmbBook.Location = new System.Drawing.Point(54, 42);
            this.cmbBook.Name = "cmbBook";
            this.cmbBook.Size = new System.Drawing.Size(125, 21);
            this.cmbBook.TabIndex = 2;
            this.cmbBook.SelectedValueChanged += new System.EventHandler(this.cmbBook_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 94;
            this.label4.Text = "Book";
            // 
            // cmbSection
            // 
            this.cmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(54, 67);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(125, 21);
            this.cmbSection.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Section";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(90, 16);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(88, 20);
            this.txtQuantity.TabIndex = 0;
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // txtCash
            // 
            this.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCash.Location = new System.Drawing.Point(230, 47);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(79, 13);
            this.txtCash.TabIndex = 3;
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCash.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 134;
            this.label6.Text = "Available Qtd";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(220, 15);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(79, 20);
            this.txtPrice.TabIndex = 1;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(168, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 133;
            this.label3.Text = "Cash Flow";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(184, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 132;
            this.label2.Text = "Price";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSection);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmBroker);
            this.groupBox1.Controls.Add(this.cmbBook);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Location = new System.Drawing.Point(12, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(191, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSpotPriceWBrokerage);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtPriceWBrokerage);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtCash);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtSpotPrice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtQuantity);
            this.groupBox2.Location = new System.Drawing.Point(209, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 131);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "0";
            // 
            // txtSpotPriceWBrokerage
            // 
            this.txtSpotPriceWBrokerage.Location = new System.Drawing.Point(151, 101);
            this.txtSpotPriceWBrokerage.Name = "txtSpotPriceWBrokerage";
            this.txtSpotPriceWBrokerage.Size = new System.Drawing.Size(73, 20);
            this.txtSpotPriceWBrokerage.TabIndex = 140;
            this.txtSpotPriceWBrokerage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpotPriceWBrokerage.Leave += new System.EventHandler(this.txtSpotPriceWBrokerage_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 13);
            this.label10.TabIndex = 141;
            this.label10.Text = "Spot Price With Brokerage";
            // 
            // txtPriceWBrokerage
            // 
            this.txtPriceWBrokerage.Location = new System.Drawing.Point(126, 73);
            this.txtPriceWBrokerage.Name = "txtPriceWBrokerage";
            this.txtPriceWBrokerage.Size = new System.Drawing.Size(72, 20);
            this.txtPriceWBrokerage.TabIndex = 138;
            this.txtPriceWBrokerage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPriceWBrokerage.Leave += new System.EventHandler(this.txtPriceWBrokerage_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 139;
            this.label9.Text = "Price With Brokerage";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.ForeColor = System.Drawing.Color.Black;
            this.cmdUpdate.Location = new System.Drawing.Point(549, 74);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(94, 34);
            this.cmdUpdate.TabIndex = 2;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(549, 124);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(94, 36);
            this.cmdClose.TabIndex = 141;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Location = new System.Drawing.Point(12, 169);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 28);
            this.button3.TabIndex = 142;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(6, 19);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(99, 20);
            this.dtpTradeDate.TabIndex = 143;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpTradeDate);
            this.groupBox4.Location = new System.Drawing.Point(538, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(115, 49);
            this.groupBox4.TabIndex = 144;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trade Date";
            // 
            // frmEditFoward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(667, 202);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditFoward";
            this.Text = "Edit Foward";
            this.Load += new System.EventHandler(this.frmEditFoward_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtSpotPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmBroker;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBook;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.TextBox txtSecurity;
        private System.Windows.Forms.TextBox txtFund;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSpotPriceWBrokerage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPriceWBrokerage;
        private System.Windows.Forms.Label label9;
    }
}