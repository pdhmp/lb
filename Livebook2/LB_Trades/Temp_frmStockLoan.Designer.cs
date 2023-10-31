namespace LiveBook
{
    partial class TempfrmStockLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TempfrmStockLoan));
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.dtgTrade = new DevExpress.XtraGrid.GridControl();
            this.dgTempSotckLoan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTempSotckLoan)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(204, 25);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(202, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 11);
            this.label1.TabIndex = 24;
            this.label1.Text = "Date";
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(406, 15);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 32);
            this.cmdrefresh.TabIndex = 23;
            this.cmdrefresh.Text = "Generate Trades";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 11);
            this.label5.TabIndex = 22;
            this.label5.Text = "Choose Portfólio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(12, 25);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 21;
            // 
            // dtgTrade
            // 
            this.dtgTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgTrade.Location = new System.Drawing.Point(12, 53);
            this.dtgTrade.MainView = this.dgTempSotckLoan;
            this.dtgTrade.Name = "dtgTrade";
            this.dtgTrade.Size = new System.Drawing.Size(992, 669);
            this.dtgTrade.TabIndex = 28;
            this.dtgTrade.TabStop = false;
            this.dtgTrade.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTempSotckLoan});
            // 
            // dgTempSotckLoan
            // 
            this.dgTempSotckLoan.GridControl = this.dtgTrade;
            this.dgTempSotckLoan.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTempSotckLoan.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgTempSotckLoan.Name = "dgTempSotckLoan";
            this.dgTempSotckLoan.OptionsBehavior.Editable = false;
            this.dgTempSotckLoan.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgTempSotckLoan.OptionsSelection.MultiSelect = true;
            this.dgTempSotckLoan.OptionsView.ColumnAutoWidth = false;
            this.dgTempSotckLoan.RowHeight = 15;
            this.dgTempSotckLoan.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTradesRel_DragObjectDrop);
            this.dgTempSotckLoan.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTradesRel_CustomDrawGroupRow);
            this.dgTempSotckLoan.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTradesRel_ColumnWidthChanged);
            this.dgTempSotckLoan.ShowCustomizationForm += new System.EventHandler(this.dgTradesRel_ShowCustomizationForm);
            this.dgTempSotckLoan.HideCustomizationForm += new System.EventHandler(this.dgTradesRel_HideCustomizationForm);
            this.dgTempSotckLoan.EndGrouping += new System.EventHandler(this.dgTradesRel_EndGrouping);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Location = new System.Drawing.Point(866, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 38);
            this.groupBox4.TabIndex = 29;
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
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(59, 20);
            this.button4.TabIndex = 15;
            this.button4.Text = "Excel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label2.Location = new System.Drawing.Point(303, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 11);
            this.label2.TabIndex = 39;
            this.label2.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(305, 25);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 38;
            // 
            // TempfrmStockLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dtgTrade);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbChoosePortfolio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TempfrmStockLoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Stock Loan Files Retport";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTempSotckLoan)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private DevExpress.XtraGrid.GridControl dtgTrade;
        private DevExpress.XtraGrid.Views.Grid.GridView dgTempSotckLoan;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
    }
}