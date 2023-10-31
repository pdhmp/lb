namespace LiveBook
{
    partial class frmClientTransactions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientTransactions));
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSharePrice = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbContact = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtIncomeTax = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAdminID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpPayment = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpConversion = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbTransType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new System.Windows.Forms.ComboBox();
            this.txtShare = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtgClientTransac = new MyXtraGrid.MyGridControl();
            this.dgClientTransac = new MyXtraGrid.MyGridView();
            this.lblCopy = new System.Windows.Forms.Label();
            this.chkFromDate = new System.Windows.Forms.CheckBox();
            this.cmbDateTo = new System.Windows.Forms.DateTimePicker();
            this.cmbDateFrom = new System.Windows.Forms.DateTimePicker();
            this.label99 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblImportFile = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.grpDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgClientTransac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgClientTransac)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(918, 514);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(133, 44);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(413, 132);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.txtSharePrice);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmbContact);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtIncomeTax);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtAdminID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.grpDate);
            this.groupBox2.Controls.Add(this.cmbTransType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCash);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmdInsert);
            this.groupBox2.Controls.Add(this.cmbFund);
            this.groupBox2.Controls.Add(this.txtShare);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 376);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(900, 182);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Insert ";
            // 
            // txtSharePrice
            // 
            this.txtSharePrice.Location = new System.Drawing.Point(446, 95);
            this.txtSharePrice.Name = "txtSharePrice";
            this.txtSharePrice.Size = new System.Drawing.Size(100, 20);
            this.txtSharePrice.TabIndex = 72;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(443, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 68;
            this.label11.Text = "Share Price";
            // 
            // cmbContact
            // 
            this.cmbContact.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbContact.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbContact.FormattingEnabled = true;
            this.cmbContact.Location = new System.Drawing.Point(6, 33);
            this.cmbContact.Name = "cmbContact";
            this.cmbContact.Size = new System.Drawing.Size(401, 21);
            this.cmbContact.TabIndex = 65;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "Client";
            // 
            // txtIncomeTax
            // 
            this.txtIncomeTax.Location = new System.Drawing.Point(658, 95);
            this.txtIncomeTax.Name = "txtIncomeTax";
            this.txtIncomeTax.Size = new System.Drawing.Size(100, 20);
            this.txtIncomeTax.TabIndex = 74;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(655, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "Income Tax";
            // 
            // txtAdminID
            // 
            this.txtAdminID.Location = new System.Drawing.Point(764, 95);
            this.txtAdminID.Name = "txtAdminID";
            this.txtAdminID.Size = new System.Drawing.Size(100, 20);
            this.txtAdminID.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(761, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 62;
            this.label8.Text = "Admin ID";
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.dtpTradeDate);
            this.grpDate.Controls.Add(this.label6);
            this.grpDate.Controls.Add(this.dtpPayment);
            this.grpDate.Controls.Add(this.label7);
            this.grpDate.Controls.Add(this.dtpConversion);
            this.grpDate.Controls.Add(this.label4);
            this.grpDate.Location = new System.Drawing.Point(6, 60);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(328, 64);
            this.grpDate.TabIndex = 68;
            this.grpDate.TabStop = false;
            this.grpDate.Text = "Dates";
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(6, 34);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(100, 20);
            this.dtpTradeDate.TabIndex = 68;
            this.dtpTradeDate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 70;
            this.label6.Text = "Trade";
            // 
            // dtpPayment
            // 
            this.dtpPayment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPayment.Location = new System.Drawing.Point(218, 34);
            this.dtpPayment.Name = "dtpPayment";
            this.dtpPayment.Size = new System.Drawing.Size(100, 20);
            this.dtpPayment.TabIndex = 70;
            this.dtpPayment.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(215, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 68;
            this.label7.Text = "Payment";
            // 
            // dtpConversion
            // 
            this.dtpConversion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpConversion.Location = new System.Drawing.Point(112, 34);
            this.dtpConversion.Name = "dtpConversion";
            this.dtpConversion.Size = new System.Drawing.Size(100, 20);
            this.dtpConversion.TabIndex = 69;
            this.dtpConversion.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 67;
            this.label4.Text = "Conversion";
            // 
            // cmbTransType
            // 
            this.cmbTransType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTransType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTransType.FormattingEnabled = true;
            this.cmbTransType.Location = new System.Drawing.Point(681, 33);
            this.cmbTransType.Name = "cmbTransType";
            this.cmbTransType.Size = new System.Drawing.Size(159, 21);
            this.cmbTransType.TabIndex = 67;
            this.cmbTransType.SelectedIndexChanged += new System.EventHandler(this.cmbTransType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(678, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Transaction Type";
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(552, 95);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(100, 20);
            this.txtCash.TabIndex = 73;
            this.txtCash.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(549, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Gross Amount";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(410, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Portfolio";
            // 
            // cmbFund
            // 
            this.cmbFund.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFund.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.Location = new System.Drawing.Point(413, 33);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(262, 21);
            this.cmbFund.TabIndex = 66;
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // txtShare
            // 
            this.txtShare.Location = new System.Drawing.Point(340, 95);
            this.txtShare.Name = "txtShare";
            this.txtShare.Size = new System.Drawing.Size(100, 20);
            this.txtShare.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(343, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Shares";
            // 
            // dtgClientTransac
            // 
            this.dtgClientTransac.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgClientTransac.Location = new System.Drawing.Point(7, 33);
            this.dtgClientTransac.LookAndFeel.SkinName = "Blue";
            this.dtgClientTransac.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgClientTransac.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgClientTransac.MainView = this.dgClientTransac;
            this.dtgClientTransac.Name = "dtgClientTransac";
            this.dtgClientTransac.Size = new System.Drawing.Size(1042, 337);
            this.dtgClientTransac.TabIndex = 0;
            this.dtgClientTransac.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgClientTransac});
            // 
            // dgClientTransac
            // 
            this.dgClientTransac.GridControl = this.dtgClientTransac;
            this.dgClientTransac.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgClientTransac.Name = "dgClientTransac";
            this.dgClientTransac.OptionsBehavior.Editable = false;
            this.dgClientTransac.OptionsSelection.MultiSelect = true;
            this.dgClientTransac.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgClientTransac.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgClientTransac_CustomDrawGroupRow);
            this.dgClientTransac.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgClientTransac.DoubleClick += new System.EventHandler(this.dgClientTransac_DoubleClick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(1018, 376);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 27;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // chkFromDate
            // 
            this.chkFromDate.AutoSize = true;
            this.chkFromDate.Checked = true;
            this.chkFromDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFromDate.Location = new System.Drawing.Point(12, 10);
            this.chkFromDate.Name = "chkFromDate";
            this.chkFromDate.Size = new System.Drawing.Size(100, 17);
            this.chkFromDate.TabIndex = 28;
            this.chkFromDate.Text = "Check by dates";
            this.chkFromDate.UseVisualStyleBackColor = true;
            this.chkFromDate.CheckedChanged += new System.EventHandler(this.chkFromDate_CheckedChanged);
            // 
            // cmbDateTo
            // 
            this.cmbDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cmbDateTo.Location = new System.Drawing.Point(110, 8);
            this.cmbDateTo.Name = "cmbDateTo";
            this.cmbDateTo.Size = new System.Drawing.Size(97, 20);
            this.cmbDateTo.TabIndex = 29;
            this.cmbDateTo.Value = new System.DateTime(2013, 8, 28, 11, 1, 5, 0);
            this.cmbDateTo.ValueChanged += new System.EventHandler(this.cmbDateTo_ValueChanged);
            // 
            // cmbDateFrom
            // 
            this.cmbDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cmbDateFrom.Location = new System.Drawing.Point(236, 8);
            this.cmbDateFrom.Name = "cmbDateFrom";
            this.cmbDateFrom.Size = new System.Drawing.Size(97, 20);
            this.cmbDateFrom.TabIndex = 30;
            this.cmbDateFrom.Value = new System.DateTime(2013, 8, 28, 11, 1, 5, 0);
            this.cmbDateFrom.ValueChanged += new System.EventHandler(this.cmbDateFrom_ValueChanged);
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(212, 11);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(20, 13);
            this.label99.TabIndex = 76;
            this.label99.Text = "To";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(339, 6);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 77;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblImportFile);
            this.groupBox3.Location = new System.Drawing.Point(918, 393);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(133, 115);
            this.groupBox3.TabIndex = 84;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import NestPrev File";
            // 
            // lblImportFile
            // 
            this.lblImportFile.AllowDrop = true;
            this.lblImportFile.Location = new System.Drawing.Point(6, 19);
            this.lblImportFile.Name = "lblImportFile";
            this.lblImportFile.Size = new System.Drawing.Size(121, 88);
            this.lblImportFile.TabIndex = 29;
            this.lblImportFile.Text = "Drag file here to import it into the system";
            this.lblImportFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblImportFile.Click += new System.EventHandler(this.lblImportFile_Click);
            this.lblImportFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragDrop);
            this.lblImportFile.DragOver += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragOver);
            // 
            // frmClientTransactions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1061, 570);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label99);
            this.Controls.Add(this.cmbDateFrom);
            this.Controls.Add(this.cmbDateTo);
            this.Controls.Add(this.chkFromDate);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.dtgClientTransac);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmClientTransactions";
            this.Text = "Client Transactions";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgClientTransac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgClientTransac)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtShare;
        private MyXtraGrid.MyGridControl dtgClientTransac;
        private MyXtraGrid.MyGridView dgClientTransac;
        private System.Windows.Forms.ComboBox cmbFund;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTransType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpDate;
        private System.Windows.Forms.DateTimePicker dtpPayment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpConversion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.ComboBox cmbContact;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtIncomeTax;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAdminID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSharePrice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkFromDate;
        private System.Windows.Forms.DateTimePicker cmbDateTo;
        private System.Windows.Forms.DateTimePicker cmbDateFrom;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblImportFile;
    }
}