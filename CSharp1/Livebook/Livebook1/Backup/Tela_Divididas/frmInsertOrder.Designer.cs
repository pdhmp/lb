namespace SGN.Tela_Divididas
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
            this.grpOrdem = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmBrooker = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.cmbportfolio = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
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
            this.cmbOrder_Type = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbSub_Strategy = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grpOrdem.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOrdem
            // 
            this.grpOrdem.Controls.Add(this.groupBox3);
            this.grpOrdem.Controls.Add(this.cmdInsert_Order_Neg);
            this.grpOrdem.Controls.Add(this.cmdInsert_Order);
            this.grpOrdem.Controls.Add(this.chkGtc);
            this.grpOrdem.Controls.Add(this.dtpExpiration);
            this.grpOrdem.Controls.Add(this.label8);
            this.grpOrdem.Controls.Add(this.cmbOrder_Type);
            this.grpOrdem.Controls.Add(this.label12);
            this.grpOrdem.Controls.Add(this.cmbSub_Strategy);
            this.grpOrdem.Controls.Add(this.label7);
            this.grpOrdem.Controls.Add(this.cmbStrategy);
            this.grpOrdem.Controls.Add(this.label4);
            this.grpOrdem.Enabled = false;
            this.grpOrdem.Location = new System.Drawing.Point(12, 12);
            this.grpOrdem.Name = "grpOrdem";
            this.grpOrdem.Size = new System.Drawing.Size(1192, 102);
            this.grpOrdem.TabIndex = 3;
            this.grpOrdem.TabStop = false;
            this.grpOrdem.Text = "Insert Order";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.cmBrooker);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbTicker);
            this.groupBox3.Controls.Add(this.cmbportfolio);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtQtd);
            this.groupBox3.Controls.Add(this.chkMarket);
            this.groupBox3.Controls.Add(this.txtCash);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPrice);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(9, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1063, 49);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(386, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 13);
            this.label14.TabIndex = 122;
            this.label14.Text = "Broker";
            // 
            // cmBrooker
            // 
            this.cmBrooker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmBrooker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmBrooker.FormattingEnabled = true;
            this.cmBrooker.Location = new System.Drawing.Point(430, 14);
            this.cmBrooker.Name = "cmBrooker";
            this.cmBrooker.Size = new System.Drawing.Size(117, 21);
            this.cmBrooker.TabIndex = 2;
            this.cmBrooker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 121;
            this.label1.Text = "Ticker";
            // 
            // cmbTicker
            // 
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTicker.FormattingEnabled = true;
            this.cmbTicker.Location = new System.Drawing.Point(259, 14);
            this.cmbTicker.Name = "cmbTicker";
            this.cmbTicker.Size = new System.Drawing.Size(108, 21);
            this.cmbTicker.TabIndex = 1;
            this.cmbTicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // cmbportfolio
            // 
            this.cmbportfolio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbportfolio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbportfolio.FormattingEnabled = true;
            this.cmbportfolio.Location = new System.Drawing.Point(58, 14);
            this.cmbportfolio.Name = "cmbportfolio";
            this.cmbportfolio.Size = new System.Drawing.Size(134, 21);
            this.cmbportfolio.TabIndex = 0;
            this.cmbportfolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 120;
            this.label13.Text = "Portfolio";
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(635, 14);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(70, 20);
            this.txtQtd.TabIndex = 3;
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQtd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtQtd.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // chkMarket
            // 
            this.chkMarket.AutoSize = true;
            this.chkMarket.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMarket.Location = new System.Drawing.Point(832, 18);
            this.chkMarket.Name = "chkMarket";
            this.chkMarket.Size = new System.Drawing.Size(59, 17);
            this.chkMarket.TabIndex = 5;
            this.chkMarket.Text = "Market";
            this.chkMarket.UseVisualStyleBackColor = true;
            this.chkMarket.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(966, 14);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(89, 20);
            this.txtCash.TabIndex = 6;
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtCash.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(580, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Quantity";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(758, 15);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(68, 20);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(904, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 115;
            this.label3.Text = "Cash Flow";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(722, 17);
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
            this.cmdInsert_Order_Neg.Location = new System.Drawing.Point(1077, 59);
            this.cmdInsert_Order_Neg.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.cmdInsert_Order_Neg.Name = "cmdInsert_Order_Neg";
            this.cmdInsert_Order_Neg.Size = new System.Drawing.Size(105, 33);
            this.cmdInsert_Order_Neg.TabIndex = 13;
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
            this.cmdInsert_Order.Location = new System.Drawing.Point(1077, 15);
            this.cmdInsert_Order.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.cmdInsert_Order.Name = "cmdInsert_Order";
            this.cmdInsert_Order.Size = new System.Drawing.Size(105, 32);
            this.cmdInsert_Order.TabIndex = 3;
            this.cmdInsert_Order.Text = "&BUY";
            this.cmdInsert_Order.UseVisualStyleBackColor = false;
            this.cmdInsert_Order.Click += new System.EventHandler(this.cmdInsert_Order_Click);
            this.cmdInsert_Order.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // chkGtc
            // 
            this.chkGtc.AutoSize = true;
            this.chkGtc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGtc.Location = new System.Drawing.Point(834, 75);
            this.chkGtc.Name = "chkGtc";
            this.chkGtc.Size = new System.Drawing.Size(76, 17);
            this.chkGtc.TabIndex = 11;
            this.chkGtc.Text = "GTC - VAC";
            this.chkGtc.UseVisualStyleBackColor = true;
            this.chkGtc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiration.Location = new System.Drawing.Point(724, 72);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(104, 20);
            this.dtpExpiration.TabIndex = 10;
            this.dtpExpiration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(663, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 105;
            this.label8.Text = "Expiration";
            // 
            // cmbOrder_Type
            // 
            this.cmbOrder_Type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOrder_Type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOrder_Type.FormattingEnabled = true;
            this.cmbOrder_Type.Location = new System.Drawing.Point(85, 71);
            this.cmbOrder_Type.Name = "cmbOrder_Type";
            this.cmbOrder_Type.Size = new System.Drawing.Size(116, 21);
            this.cmbOrder_Type.TabIndex = 7;
            this.cmbOrder_Type.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(17, 72);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 104;
            this.label12.Text = "Order Type";
            // 
            // cmbSub_Strategy
            // 
            this.cmbSub_Strategy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSub_Strategy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSub_Strategy.FormattingEnabled = true;
            this.cmbSub_Strategy.Location = new System.Drawing.Point(489, 71);
            this.cmbSub_Strategy.Name = "cmbSub_Strategy";
            this.cmbSub_Strategy.Size = new System.Drawing.Size(119, 21);
            this.cmbSub_Strategy.TabIndex = 9;
            this.cmbSub_Strategy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(412, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Sub-Strategy";
            // 
            // cmbStrategy
            // 
            this.cmbStrategy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbStrategy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStrategy.FormattingEnabled = true;
            this.cmbStrategy.Location = new System.Drawing.Point(275, 71);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(112, 21);
            this.cmbStrategy.TabIndex = 8;
            this.cmbStrategy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 94;
            this.label4.Text = "Strategy";
            // 
            // frmInsertOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1217, 129);
            this.Controls.Add(this.grpOrdem);
            this.Name = "frmInsertOrder";
            this.Text = "Insert Order";
            this.grpOrdem.ResumeLayout(false);
            this.grpOrdem.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOrdem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmBrooker;
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
        private System.Windows.Forms.ComboBox cmbOrder_Type;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbSub_Strategy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbStrategy;
        private System.Windows.Forms.Label label4;
    }
}