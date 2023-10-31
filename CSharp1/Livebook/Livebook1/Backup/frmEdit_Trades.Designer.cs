namespace SGN
{
    partial class frmEdit_Trades
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.dtg2 = new DevExpress.XtraGrid.GridControl();
            this.dgTrades = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmd_Commit = new System.Windows.Forms.Button();
            this.grpOrdem = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmBrooker = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdInsert_Order_Neg = new System.Windows.Forms.Button();
            this.cmdInsert_Order = new System.Windows.Forms.Button();
            this.cmbOrder_Type = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbSub_Strategy = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).BeginInit();
            this.grpOrdem.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Location = new System.Drawing.Point(942, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(139, 38);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Export";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(71, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 20);
            this.button5.TabIndex = 16;
            this.button5.Text = "Txt";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(59, 20);
            this.button4.TabIndex = 15;
            this.button4.Text = "Excel";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(199, 28);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(197, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 11);
            this.label1.TabIndex = 34;
            this.label1.Text = "Date";
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(315, 28);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 23);
            this.cmdrefresh.TabIndex = 33;
            this.cmdrefresh.Text = "Generate Trades";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(23, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 11);
            this.label5.TabIndex = 32;
            this.label5.Text = "Choose Portfólio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(19, 28);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 31;
            // 
            // dtg2
            // 
            this.dtg2.EmbeddedNavigator.Name = "";
            this.dtg2.Location = new System.Drawing.Point(12, 62);
            this.dtg2.MainView = this.dgTrades;
            this.dtg2.Name = "dtg2";
            this.dtg2.Size = new System.Drawing.Size(1090, 632);
            this.dtg2.TabIndex = 40;
            this.dtg2.TabStop = false;
            this.dtg2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTrades});
            // 
            // dgTrades
            // 
            this.dgTrades.GridControl = this.dtg2;
            this.dgTrades.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgTrades.Name = "dgTrades";
            this.dgTrades.OptionsBehavior.Editable = false;
            this.dgTrades.OptionsView.ColumnAutoWidth = false;
            this.dgTrades.RowHeight = 15;
            this.dgTrades.DoubleClick += new System.EventHandler(this.dgTrades_DoubleClick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(474, 29);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 20);
            this.lblStatus.TabIndex = 41;
            // 
            // cmd_Commit
            // 
            this.cmd_Commit.Location = new System.Drawing.Point(570, 26);
            this.cmd_Commit.Name = "cmd_Commit";
            this.cmd_Commit.Size = new System.Drawing.Size(95, 23);
            this.cmd_Commit.TabIndex = 42;
            this.cmd_Commit.Text = "Commit Changes";
            this.cmd_Commit.UseVisualStyleBackColor = true;
            // 
            // grpOrdem
            // 
            this.grpOrdem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOrdem.Controls.Add(this.groupBox3);
            this.grpOrdem.Controls.Add(this.cmdInsert_Order_Neg);
            this.grpOrdem.Controls.Add(this.cmdInsert_Order);
            this.grpOrdem.Controls.Add(this.cmbOrder_Type);
            this.grpOrdem.Controls.Add(this.label12);
            this.grpOrdem.Controls.Add(this.cmbSub_Strategy);
            this.grpOrdem.Controls.Add(this.label9);
            this.grpOrdem.Controls.Add(this.cmbStrategy);
            this.grpOrdem.Controls.Add(this.label10);
            this.grpOrdem.Location = new System.Drawing.Point(12, 700);
            this.grpOrdem.Name = "grpOrdem";
            this.grpOrdem.Size = new System.Drawing.Size(935, 102);
            this.grpOrdem.TabIndex = 43;
            this.grpOrdem.TabStop = false;
            this.grpOrdem.Text = "Insert Order";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.cmBrooker);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbTicker);
            this.groupBox3.Controls.Add(this.txtQtd);
            this.groupBox3.Controls.Add(this.txtCash);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPrice);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(9, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(796, 49);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(177, 21);
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
            this.cmBrooker.Location = new System.Drawing.Point(227, 18);
            this.cmBrooker.Name = "cmBrooker";
            this.cmBrooker.Size = new System.Drawing.Size(117, 21);
            this.cmBrooker.TabIndex = 2;
            this.cmBrooker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 121;
            this.label3.Text = "Ticker";
            // 
            // cmbTicker
            // 
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTicker.FormattingEnabled = true;
            this.cmbTicker.Location = new System.Drawing.Point(50, 18);
            this.cmbTicker.Name = "cmbTicker";
            this.cmbTicker.Size = new System.Drawing.Size(108, 21);
            this.cmbTicker.TabIndex = 1;
            this.cmbTicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(426, 19);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(70, 20);
            this.txtQtd.TabIndex = 3;
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQtd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtQtd.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(696, 19);
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
            this.label6.Location = new System.Drawing.Point(371, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Quantity";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(549, 18);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(68, 20);
            this.txtPrice.TabIndex = 4;
            this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpress_KeyDown);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(634, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 115;
            this.label4.Text = "Cash Flow";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(513, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 114;
            this.label7.Text = "Price";
            // 
            // cmdInsert_Order_Neg
            // 
            this.cmdInsert_Order_Neg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.cmdInsert_Order_Neg.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdInsert_Order_Neg.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert_Order_Neg.Location = new System.Drawing.Point(819, 56);
            this.cmdInsert_Order_Neg.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.cmdInsert_Order_Neg.Name = "cmdInsert_Order_Neg";
            this.cmdInsert_Order_Neg.Size = new System.Drawing.Size(105, 33);
            this.cmdInsert_Order_Neg.TabIndex = 13;
            this.cmdInsert_Order_Neg.Text = "&SELL";
            this.cmdInsert_Order_Neg.UseVisualStyleBackColor = false;
            this.cmdInsert_Order_Neg.Click += new System.EventHandler(this.Inserir_Trade);
            // 
            // cmdInsert_Order
            // 
            this.cmdInsert_Order.BackColor = System.Drawing.Color.Lime;
            this.cmdInsert_Order.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdInsert_Order.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert_Order.Location = new System.Drawing.Point(819, 12);
            this.cmdInsert_Order.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.cmdInsert_Order.Name = "cmdInsert_Order";
            this.cmdInsert_Order.Size = new System.Drawing.Size(105, 32);
            this.cmdInsert_Order.TabIndex = 3;
            this.cmdInsert_Order.Text = "&BUY";
            this.cmdInsert_Order.UseVisualStyleBackColor = false;
            this.cmdInsert_Order.Click += new System.EventHandler(this.Inserir_Trade);
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(412, 74);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Sub-Strategy";
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
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(220, 72);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 94;
            this.label10.Text = "Strategy";
            // 
            // frmEdit_Trades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 814);
            this.Controls.Add(this.grpOrdem);
            this.Controls.Add(this.cmd_Commit);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dtg2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbChoosePortfolio);
            this.Name = "frmEdit_Trades";
            this.Text = "frmEdit_Trades";
            this.Load += new System.EventHandler(this.frmEdit_Trades_Load);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).EndInit();
            this.grpOrdem.ResumeLayout(false);
            this.grpOrdem.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private DevExpress.XtraGrid.GridControl dtg2;
        private DevExpress.XtraGrid.Views.Grid.GridView dgTrades;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button cmd_Commit;
        private System.Windows.Forms.GroupBox grpOrdem;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmBrooker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbTicker;
        private System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdInsert_Order_Neg;
        private System.Windows.Forms.Button cmdInsert_Order;
        private System.Windows.Forms.ComboBox cmbOrder_Type;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbSub_Strategy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbStrategy;
        private System.Windows.Forms.Label label10;
    }
}