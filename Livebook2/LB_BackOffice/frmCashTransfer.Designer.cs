namespace LiveBook
{
    partial class frmCashTransfer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCashTransfer));
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbCurrencyBuy = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCurrencySell = new System.Windows.Forms.ComboBox();
            this.txtFinalCash = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpSettlement = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgTransf = new MyXtraGrid.MyGridControl();
            this.gviewTransf = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new LiveBook.NestPortCombo();
            this.bntLoad = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewTransf)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(630, 456);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(452, 42);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 7;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cmbCurrencyBuy);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cmbCurrencySell);
            this.groupBox2.Controls.Add(this.txtFinalCash);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dtpSettlement);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpTradeDate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtCash);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmdInsert);
            this.groupBox2.Location = new System.Drawing.Point(12, 416);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 109);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Insert ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(355, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 85;
            this.label8.Text = "Currency Buy";
            // 
            // cmbCurrencyBuy
            // 
            this.cmbCurrencyBuy.FormattingEnabled = true;
            this.cmbCurrencyBuy.Location = new System.Drawing.Point(358, 31);
            this.cmbCurrencyBuy.Name = "cmbCurrencyBuy";
            this.cmbCurrencyBuy.Size = new System.Drawing.Size(88, 21);
            this.cmbCurrencyBuy.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(246, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 83;
            this.label7.Text = "Currency Sell";
            // 
            // cmbCurrencySell
            // 
            this.cmbCurrencySell.FormattingEnabled = true;
            this.cmbCurrencySell.Location = new System.Drawing.Point(249, 31);
            this.cmbCurrencySell.Name = "cmbCurrencySell";
            this.cmbCurrencySell.Size = new System.Drawing.Size(88, 21);
            this.cmbCurrencySell.TabIndex = 2;
            // 
            // txtFinalCash
            // 
            this.txtFinalCash.Location = new System.Drawing.Point(249, 78);
            this.txtFinalCash.Name = "txtFinalCash";
            this.txtFinalCash.Size = new System.Drawing.Size(100, 20);
            this.txtFinalCash.TabIndex = 6;
            this.txtFinalCash.Leave += new System.EventHandler(this.txtFinalCash_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(246, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 81;
            this.label5.Text = "Cash Transfered";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(127, 79);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(100, 20);
            this.txtPrice.TabIndex = 5;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(124, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Price";
            // 
            // dtpSettlement
            // 
            this.dtpSettlement.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSettlement.Location = new System.Drawing.Point(127, 33);
            this.dtpSettlement.Name = "dtpSettlement";
            this.dtpSettlement.Size = new System.Drawing.Size(100, 20);
            this.dtpSettlement.TabIndex = 1;
            this.dtpSettlement.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(124, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Settlement";
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(6, 32);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(100, 20);
            this.dtpTradeDate.TabIndex = 0;
            this.dtpTradeDate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Transfer Date";
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(6, 78);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(100, 20);
            this.txtCash.TabIndex = 4;
            this.txtCash.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Original Cash";
            // 
            // dtgTransf
            // 
            this.dtgTransf.Location = new System.Drawing.Point(11, 52);
            this.dtgTransf.LookAndFeel.SkinName = "Blue";
            this.dtgTransf.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgTransf.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTransf.MainView = this.gviewTransf;
            this.dtgTransf.Name = "dtgTransf";
            this.dtgTransf.Size = new System.Drawing.Size(718, 358);
            this.dtgTransf.TabIndex = 1;
            this.dtgTransf.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gviewTransf});
            // 
            // gviewTransf
            // 
            this.gviewTransf.GridControl = this.dtgTransf;
            this.gviewTransf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gviewTransf.Name = "gviewTransf";
            this.gviewTransf.OptionsBehavior.Editable = false;
            this.gviewTransf.OptionsView.ShowGroupPanel = false;
            this.gviewTransf.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.gviewTransf.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.gviewTransf.DoubleClick += new System.EventHandler(this.gviewTransf_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Fund";
            // 
            // cmbFund
            // 
            this.cmbFund.DisplayMember = "Port_Name";
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.IdPortType = 2;
            this.cmbFund.includeAllPortsOption = false;
            this.cmbFund.includeMHConsolOption = false;
            this.cmbFund.Location = new System.Drawing.Point(12, 25);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(155, 21);
            this.cmbFund.TabIndex = 0;
            this.cmbFund.ValueMember = "Id_Portfolio";
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // bntLoad
            // 
            this.bntLoad.Location = new System.Drawing.Point(173, 24);
            this.bntLoad.Name = "bntLoad";
            this.bntLoad.Size = new System.Drawing.Size(75, 23);
            this.bntLoad.TabIndex = 60;
            this.bntLoad.Text = "Load";
            this.bntLoad.UseVisualStyleBackColor = true;
            this.bntLoad.Click += new System.EventHandler(this.bntLoad_Click);
            // 
            // frmCashTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(740, 534);
            this.Controls.Add(this.bntLoad);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgTransf);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(756, 572);
            this.MinimumSize = new System.Drawing.Size(756, 572);
            this.Name = "frmCashTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wire Transfer";
            this.Load += new System.EventHandler(this.frmCashTransfer_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gviewTransf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private MyXtraGrid.MyGridControl dtgTransf;
        private MyXtraGrid.MyGridView gviewTransf;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private NestPortCombo cmbFund;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpSettlement;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFinalCash;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbCurrencyBuy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCurrencySell;
        private System.Windows.Forms.Button bntLoad;
    }
}