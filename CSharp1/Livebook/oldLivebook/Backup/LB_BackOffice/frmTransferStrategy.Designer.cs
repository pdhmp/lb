namespace SGN
{
    partial class frmTransferStrategy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransferStrategy));
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.dtgTransf = new MyXtraGrid.MyGridControl();
            this.dgTransf = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new SGN.NestPortCombo();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransf)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(495, 527);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(397, 34);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Update";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // dtgTransf
            // 
            this.dtgTransf.Location = new System.Drawing.Point(12, 52);
            this.dtgTransf.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgTransf.MainView = this.dgTransf;
            this.dtgTransf.Name = "dtgTransf";
            this.dtgTransf.Size = new System.Drawing.Size(586, 358);
            this.dtgTransf.TabIndex = 0;
            this.dtgTransf.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTransf});
            // 
            // dgTransf
            // 
            this.dgTransf.GridControl = this.dtgTransf;
            this.dgTransf.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTransf.Name = "dgTransf";
            this.dgTransf.OptionsBehavior.Editable = false;
            this.dgTransf.OptionsView.ShowGroupPanel = false;
            this.dgTransf.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgTransf.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
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
            this.cmbFund.Location = new System.Drawing.Point(12, 25);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(121, 21);
            this.cmbFund.TabIndex = 60;
            this.cmbFund.ValueMember = "Id_Portfolio";
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(148, 25);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(100, 20);
            this.dtpTradeDate.TabIndex = 71;
            this.dtpTradeDate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Trade Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.cmdInsert);
            this.groupBox1.Location = new System.Drawing.Point(12, 416);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 93);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Sub Strategy";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(146, 42);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "Strategy";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // frmTransferStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(610, 574);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgTransf);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.dtpTradeDate);
            this.Controls.Add(this.label6);
            this.Name = "frmTransferStrategy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Strategy Transfer";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTransf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransf)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private MyXtraGrid.MyGridControl dtgTransf;
        private MyXtraGrid.MyGridView dgTransf;
        private System.Windows.Forms.Label label1;
        private NestPortCombo cmbFund;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}