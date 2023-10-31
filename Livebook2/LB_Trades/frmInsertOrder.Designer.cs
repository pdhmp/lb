namespace LiveBook
{
    partial class frmInsertOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInsertOrder));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbportfolio = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbBook = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmBroker = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.chkMarket = new System.Windows.Forms.CheckBox();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdInsert_Order_Neg = new System.Windows.Forms.Button();
            this.cmdInsert_Order = new System.Windows.Forms.Button();
            this.chkGtc = new System.Windows.Forms.CheckBox();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSpotPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkIncludeExp = new System.Windows.Forms.CheckBox();
            this.txtPriceWBrokerage = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSpotPriceWBrokerage = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.chkIncludeTrade = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbportfolio);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cmbBook);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cmbSection);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(7, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(239, 105);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            // 
            // cmbportfolio
            // 
            this.cmbportfolio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbportfolio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbportfolio.FormattingEnabled = true;
            this.cmbportfolio.Location = new System.Drawing.Point(76, 15);
            this.cmbportfolio.Name = "cmbportfolio";
            this.cmbportfolio.Size = new System.Drawing.Size(153, 21);
            this.cmbportfolio.TabIndex = 20;
            this.cmbportfolio.SelectedValueChanged += new System.EventHandler(this.cmbportfolio_SelectedValueChanged);
            this.cmbportfolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
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
            // cmbBook
            // 
            this.cmbBook.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBook.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBook.FormattingEnabled = true;
            this.cmbBook.Location = new System.Drawing.Point(76, 42);
            this.cmbBook.Name = "cmbBook";
            this.cmbBook.Size = new System.Drawing.Size(125, 21);
            this.cmbBook.TabIndex = 21;
            this.cmbBook.SelectedIndexChanged += new System.EventHandler(this.cmbBook_SelectedValueChanged);
            this.cmbBook.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 94;
            this.label4.Text = "Id Book";
            // 
            // cmbSection
            // 
            this.cmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(76, 69);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(125, 21);
            this.cmbSection.TabIndex = 22;
            this.cmbSection.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Id Section";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(249, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 122;
            this.label14.Text = "Broker";
            // 
            // cmBroker
            // 
            this.cmBroker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmBroker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmBroker.FormattingEnabled = true;
            this.cmBroker.Location = new System.Drawing.Point(293, 64);
            this.cmBroker.Name = "cmBroker";
            this.cmBroker.Size = new System.Drawing.Size(143, 21);
            this.cmBroker.TabIndex = 4;
            this.cmBroker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(252, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Ticker";
            // 
            // cmbTicker
            // 
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTicker.FormattingEnabled = true;
            this.cmbTicker.Location = new System.Drawing.Point(293, 18);
            this.cmbTicker.Name = "cmbTicker";
            this.cmbTicker.Size = new System.Drawing.Size(143, 21);
            this.cmbTicker.TabIndex = 0;
            this.cmbTicker.SelectedIndexChanged += new System.EventHandler(this.cmbTicker_SelectedIndexChanged);
            this.cmbTicker.Enter += new System.EventHandler(this.cmbTicker_Enter);
            this.cmbTicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.cmbTicker.Leave += new System.EventHandler(this.cmbTicker_Leave);
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(508, 19);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(88, 20);
            this.txtQtd.TabIndex = 1;
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQtd.TextChanged += new System.EventHandler(this.txtQtd_TextChanged);
            this.txtQtd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtQtd.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // chkMarket
            // 
            this.chkMarket.AutoSize = true;
            this.chkMarket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMarket.Location = new System.Drawing.Point(770, 47);
            this.chkMarket.Name = "chkMarket";
            this.chkMarket.Size = new System.Drawing.Size(59, 17);
            this.chkMarket.TabIndex = 6;
            this.chkMarket.Text = "Market";
            this.chkMarket.UseVisualStyleBackColor = true;
            this.chkMarket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // txtCash
            // 
            this.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCash.Location = new System.Drawing.Point(517, 68);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(63, 13);
            this.txtCash.TabIndex = 6;
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtCash.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(453, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Quantity";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(670, 19);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(79, 20);
            this.txtPrice.TabIndex = 2;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(455, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 115;
            this.label3.Text = "Cash Flow";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(634, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 114;
            this.label2.Text = "Price";
            // 
            // cmdInsert_Order_Neg
            // 
            this.cmdInsert_Order_Neg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdInsert_Order_Neg.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdInsert_Order_Neg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert_Order_Neg.Location = new System.Drawing.Point(949, 53);
            this.cmdInsert_Order_Neg.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.cmdInsert_Order_Neg.Name = "cmdInsert_Order_Neg";
            this.cmdInsert_Order_Neg.Size = new System.Drawing.Size(105, 33);
            this.cmdInsert_Order_Neg.TabIndex = 11;
            this.cmdInsert_Order_Neg.Text = "&SELL";
            this.cmdInsert_Order_Neg.UseVisualStyleBackColor = false;
            this.cmdInsert_Order_Neg.Click += new System.EventHandler(this.cmdInsert_Order_Neg_Click);
            this.cmdInsert_Order_Neg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // cmdInsert_Order
            // 
            this.cmdInsert_Order.BackColor = System.Drawing.Color.Lime;
            this.cmdInsert_Order.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdInsert_Order.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert_Order.Location = new System.Drawing.Point(949, 9);
            this.cmdInsert_Order.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.cmdInsert_Order.Name = "cmdInsert_Order";
            this.cmdInsert_Order.Size = new System.Drawing.Size(105, 32);
            this.cmdInsert_Order.TabIndex = 10;
            this.cmdInsert_Order.Text = "&BUY";
            this.cmdInsert_Order.UseVisualStyleBackColor = false;
            this.cmdInsert_Order.Click += new System.EventHandler(this.cmdInsert_Order_Click);
            this.cmdInsert_Order.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // chkGtc
            // 
            this.chkGtc.AutoSize = true;
            this.chkGtc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGtc.Location = new System.Drawing.Point(835, 47);
            this.chkGtc.Name = "chkGtc";
            this.chkGtc.Size = new System.Drawing.Size(76, 17);
            this.chkGtc.TabIndex = 7;
            this.chkGtc.Text = "GTC - VAC";
            this.chkGtc.UseVisualStyleBackColor = true;
            this.chkGtc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiration.Location = new System.Drawing.Point(828, 19);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(95, 20);
            this.dtpExpiration.TabIndex = 3;
            this.dtpExpiration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(767, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Expiration";
            // 
            // txtSpotPrice
            // 
            this.txtSpotPrice.Location = new System.Drawing.Point(670, 43);
            this.txtSpotPrice.Name = "txtSpotPrice";
            this.txtSpotPrice.Size = new System.Drawing.Size(50, 20);
            this.txtSpotPrice.TabIndex = 5;
            this.txtSpotPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(609, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 124;
            this.label5.Text = "Spot Price";
            // 
            // chkIncludeExp
            // 
            this.chkIncludeExp.AutoSize = true;
            this.chkIncludeExp.Location = new System.Drawing.Point(293, 42);
            this.chkIncludeExp.Name = "chkIncludeExp";
            this.chkIncludeExp.Size = new System.Drawing.Size(99, 17);
            this.chkIncludeExp.TabIndex = 125;
            this.chkIncludeExp.Text = "Include Expired";
            this.chkIncludeExp.UseVisualStyleBackColor = true;
            this.chkIncludeExp.CheckedChanged += new System.EventHandler(this.chkIncludeExp_CheckedChanged);
            // 
            // txtPriceWBrokerage
            // 
            this.txtPriceWBrokerage.Location = new System.Drawing.Point(713, 66);
            this.txtPriceWBrokerage.Name = "txtPriceWBrokerage";
            this.txtPriceWBrokerage.Size = new System.Drawing.Size(50, 20);
            this.txtPriceWBrokerage.TabIndex = 126;
            this.txtPriceWBrokerage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPriceWBrokerage.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(600, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 13);
            this.label9.TabIndex = 127;
            this.label9.Text = "Price With Brokerage";
            this.label9.Visible = false;
            // 
            // txtSpotPriceWBrokerage
            // 
            this.txtSpotPriceWBrokerage.Location = new System.Drawing.Point(738, 90);
            this.txtSpotPriceWBrokerage.Name = "txtSpotPriceWBrokerage";
            this.txtSpotPriceWBrokerage.Size = new System.Drawing.Size(50, 20);
            this.txtSpotPriceWBrokerage.TabIndex = 128;
            this.txtSpotPriceWBrokerage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSpotPriceWBrokerage.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(600, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 13);
            this.label10.TabIndex = 129;
            this.label10.Text = "Spot Price With Brokerage";
            this.label10.Visible = false;
            // 
            // chkIncludeTrade
            // 
            this.chkIncludeTrade.AutoSize = true;
            this.chkIncludeTrade.Location = new System.Drawing.Point(812, 70);
            this.chkIncludeTrade.Name = "chkIncludeTrade";
            this.chkIncludeTrade.Size = new System.Drawing.Size(92, 17);
            this.chkIncludeTrade.TabIndex = 130;
            this.chkIncludeTrade.Text = "Include Trade";
            this.chkIncludeTrade.UseVisualStyleBackColor = true;
            // 
            // frmInsertOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1087, 116);
            this.Controls.Add(this.chkIncludeTrade);
            this.Controls.Add(this.txtSpotPriceWBrokerage);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtPriceWBrokerage);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.chkIncludeExp);
            this.Controls.Add(this.txtSpotPrice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmdInsert_Order_Neg);
            this.Controls.Add(this.cmBroker);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTicker);
            this.Controls.Add(this.cmdInsert_Order);
            this.Controls.Add(this.chkGtc);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.chkMarket);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.txtCash);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInsertOrder";
            this.Text = "Insert Order";
            this.Load += new System.EventHandler(this.frmInsertOrder_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmBroker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTicker;
        private System.Windows.Forms.ComboBox cmbportfolio;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.CheckBox chkMarket;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdInsert_Order_Neg;
        private System.Windows.Forms.Button cmdInsert_Order;
        private System.Windows.Forms.CheckBox chkGtc;
        private System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBook;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSpotPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkIncludeExp;
        private System.Windows.Forms.TextBox txtPriceWBrokerage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSpotPriceWBrokerage;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkIncludeTrade;
    }
}