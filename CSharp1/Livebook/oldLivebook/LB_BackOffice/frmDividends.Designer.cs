namespace LiveBook
{
    partial class frmDividends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDividends));
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBonus = new System.Windows.Forms.TextBox();
            this.dtpPayment = new System.Windows.Forms.DateTimePicker();
            this.dtpRecord = new System.Windows.Forms.DateTimePicker();
            this.dtpEx = new System.Windows.Forms.DateTimePicker();
            this.dtpDeclared = new System.Windows.Forms.DateTimePicker();
            this.cmbTransType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGross = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTicker = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNet = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtgDividends = new MyXtraGrid.MyGridControl();
            this.dgDividends = new MyXtraGrid.MyGridView();
            this.lblCopy = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDividends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDividends)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose.Location = new System.Drawing.Point(346, 442);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 34);
            this.cmdClose.TabIndex = 9;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(650, 59);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 8;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtBonus);
            this.groupBox2.Controls.Add(this.dtpPayment);
            this.groupBox2.Controls.Add(this.dtpRecord);
            this.groupBox2.Controls.Add(this.dtpEx);
            this.groupBox2.Controls.Add(this.dtpDeclared);
            this.groupBox2.Controls.Add(this.cmbTransType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtGross);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cmbTicker);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmdInsert);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtNet);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 329);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(758, 107);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Insert Dividend";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(505, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 67;
            this.label9.Text = "Percent Bonus";
            // 
            // txtBonus
            // 
            this.txtBonus.Location = new System.Drawing.Point(493, 73);
            this.txtBonus.Name = "txtBonus";
            this.txtBonus.Size = new System.Drawing.Size(100, 20);
            this.txtBonus.TabIndex = 66;
            // 
            // dtpPayment
            // 
            this.dtpPayment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPayment.Location = new System.Drawing.Point(495, 31);
            this.dtpPayment.Name = "dtpPayment";
            this.dtpPayment.Size = new System.Drawing.Size(100, 20);
            this.dtpPayment.TabIndex = 65;
            this.dtpPayment.TabStop = false;
            // 
            // dtpRecord
            // 
            this.dtpRecord.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRecord.Location = new System.Drawing.Point(389, 31);
            this.dtpRecord.Name = "dtpRecord";
            this.dtpRecord.Size = new System.Drawing.Size(100, 20);
            this.dtpRecord.TabIndex = 64;
            this.dtpRecord.TabStop = false;
            // 
            // dtpEx
            // 
            this.dtpEx.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEx.Location = new System.Drawing.Point(283, 31);
            this.dtpEx.Name = "dtpEx";
            this.dtpEx.Size = new System.Drawing.Size(100, 20);
            this.dtpEx.TabIndex = 63;
            this.dtpEx.TabStop = false;
            // 
            // dtpDeclared
            // 
            this.dtpDeclared.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDeclared.Location = new System.Drawing.Point(177, 31);
            this.dtpDeclared.Name = "dtpDeclared";
            this.dtpDeclared.Size = new System.Drawing.Size(100, 20);
            this.dtpDeclared.TabIndex = 62;
            this.dtpDeclared.TabStop = false;
            // 
            // cmbTransType
            // 
            this.cmbTransType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTransType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTransType.FormattingEnabled = true;
            this.cmbTransType.Location = new System.Drawing.Point(13, 32);
            this.cmbTransType.Name = "cmbTransType";
            this.cmbTransType.Size = new System.Drawing.Size(158, 21);
            this.cmbTransType.TabIndex = 61;
            this.cmbTransType.SelectedValueChanged += new System.EventHandler(this.cmbTransType_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Transaction Type";
            // 
            // txtGross
            // 
            this.txtGross.Location = new System.Drawing.Point(387, 73);
            this.txtGross.Name = "txtGross";
            this.txtGross.Size = new System.Drawing.Size(100, 20);
            this.txtGross.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(402, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Per Share Gross";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "NestTicker";
            // 
            // cmbTicker
            // 
            this.cmbTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTicker.FormattingEnabled = true;
            this.cmbTicker.Location = new System.Drawing.Point(167, 72);
            this.cmbTicker.Name = "cmbTicker";
            this.cmbTicker.Size = new System.Drawing.Size(108, 21);
            this.cmbTicker.TabIndex = 56;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(407, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "Record Date";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(188, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Declared Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(309, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Ex Date";
            // 
            // txtNet
            // 
            this.txtNet.Location = new System.Drawing.Point(282, 73);
            this.txtNet.Name = "txtNet";
            this.txtNet.Size = new System.Drawing.Size(100, 20);
            this.txtNet.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(295, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Per Share Net";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(508, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Payment Date";
            // 
            // dtgDividends
            // 
            this.dtgDividends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDividends.Location = new System.Drawing.Point(12, 14);
            this.dtgDividends.LookAndFeel.SkinName = "Blue";
            this.dtgDividends.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgDividends.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDividends.MainView = this.dgDividends;
            this.dtgDividends.Name = "dtgDividends";
            this.dtgDividends.Size = new System.Drawing.Size(758, 309);
            this.dtgDividends.TabIndex = 30;
            this.dtgDividends.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgDividends});
            // 
            // dgDividends
            // 
            this.dgDividends.GridControl = this.dtgDividends;
            this.dgDividends.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgDividends.Name = "dgDividends";
            this.dgDividends.OptionsBehavior.Editable = false;
            this.dgDividends.OptionsSelection.MultiSelect = true;
            this.dgDividends.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgDividends.OptionsView.ShowGroupPanel = false;
            this.dgDividends.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgDividends.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgDividends.DoubleClick += new System.EventHandler(this.dgDividends_DoubleClick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(733, 322);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 37;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // frmDividends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 488);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.dtgDividends);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDividends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dividends";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDividends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDividends)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private MyXtraGrid.MyGridControl dtgDividends;
        private MyXtraGrid.MyGridView dgDividends;
        private System.Windows.Forms.ComboBox cmbTicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGross;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTransType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDeclared;
        private System.Windows.Forms.DateTimePicker dtpPayment;
        private System.Windows.Forms.DateTimePicker dtpRecord;
        private System.Windows.Forms.DateTimePicker dtpEx;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBonus;
        private System.Windows.Forms.Label lblCopy;
    }
}