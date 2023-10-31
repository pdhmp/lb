namespace SGN
{
    partial class frmSubsRedemp
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.dtgSubRedemp = new MyXtraGrid.MyGridControl();
            this.dgSubRedemp = new MyXtraGrid.MyGridView();
            this.groupBox2.SuspendLayout();
            this.grpDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSubRedemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubRedemp)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(598, 424);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 44);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(377, 77);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grpDate);
            this.groupBox2.Controls.Add(this.cmbTransType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtCash);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbFund);
            this.groupBox2.Controls.Add(this.cmdInsert);
            this.groupBox2.Controls.Add(this.txtShare);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 378);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 132);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Insert ";
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.dtpTradeDate);
            this.grpDate.Controls.Add(this.label6);
            this.grpDate.Controls.Add(this.dtpPayment);
            this.grpDate.Controls.Add(this.label7);
            this.grpDate.Controls.Add(this.dtpConversion);
            this.grpDate.Controls.Add(this.label4);
            this.grpDate.Enabled = false;
            this.grpDate.Location = new System.Drawing.Point(13, 59);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(328, 64);
            this.grpDate.TabIndex = 4;
            this.grpDate.TabStop = false;
            this.grpDate.Text = "Date";
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(6, 34);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(100, 20);
            this.dtpTradeDate.TabIndex = 0;
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
            this.dtpPayment.TabIndex = 2;
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
            this.dtpConversion.TabIndex = 1;
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
            this.cmbTransType.Location = new System.Drawing.Point(13, 32);
            this.cmbTransType.Name = "cmbTransType";
            this.cmbTransType.Size = new System.Drawing.Size(130, 21);
            this.cmbTransType.TabIndex = 0;
            this.cmbTransType.SelectedIndexChanged += new System.EventHandler(this.cmbTransType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Transaction Type";
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(304, 32);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(100, 20);
            this.txtCash.TabIndex = 2;
            this.txtCash.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Cash";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Fund";
            // 
            // cmbFund
            // 
            this.cmbFund.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFund.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.Location = new System.Drawing.Point(159, 31);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(130, 21);
            this.cmbFund.TabIndex = 1;
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // txtShare
            // 
            this.txtShare.Location = new System.Drawing.Point(423, 33);
            this.txtShare.Name = "txtShare";
            this.txtShare.Size = new System.Drawing.Size(100, 20);
            this.txtShare.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(420, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Share";
            // 
            // dtgSubRedemp
            // 
            this.dtgSubRedemp.Location = new System.Drawing.Point(12, 14);
            this.dtgSubRedemp.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgSubRedemp.MainView = this.dgSubRedemp;
            this.dtgSubRedemp.Name = "dtgSubRedemp";
            this.dtgSubRedemp.Size = new System.Drawing.Size(703, 358);
            this.dtgSubRedemp.TabIndex = 0;
            this.dtgSubRedemp.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgSubRedemp});
            // 
            // dgSubRedemp
            // 
            this.dgSubRedemp.GridControl = this.dtgSubRedemp;
            this.dgSubRedemp.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgSubRedemp.Name = "dgSubRedemp";
            this.dgSubRedemp.OptionsBehavior.Editable = false;
            this.dgSubRedemp.OptionsView.ShowGroupPanel = false;
            this.dgSubRedemp.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgSubRedemp.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            // 
            // frmSubsRedemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(727, 522);
            this.Controls.Add(this.dtgSubRedemp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Name = "frmSubsRedemp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subscriptions & Redemptios";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSubRedemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubRedemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtShare;
        private MyXtraGrid.MyGridControl dtgSubRedemp;
        private MyXtraGrid.MyGridView dgSubRedemp;
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
    }
}