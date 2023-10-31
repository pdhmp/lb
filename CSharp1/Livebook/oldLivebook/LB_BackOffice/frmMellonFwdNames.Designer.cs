namespace LiveBook
{
    partial class frmMellonFwdNames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMellonFwdNames));
            this.dtgMellonFwd = new MyXtraGrid.MyGridControl();
            this.dgMellonFwd = new MyXtraGrid.MyGridView();
            this.txtMellonCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmbSecurity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMellonFwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMellonFwd)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgMellonFwd
            // 
            this.dtgMellonFwd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgMellonFwd.Location = new System.Drawing.Point(3, 39);
            this.dtgMellonFwd.LookAndFeel.SkinName = "Blue";
            this.dtgMellonFwd.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgMellonFwd.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMellonFwd.MainView = this.dgMellonFwd;
            this.dtgMellonFwd.Name = "dtgMellonFwd";
            this.dtgMellonFwd.Size = new System.Drawing.Size(861, 203);
            this.dtgMellonFwd.TabIndex = 24;
            this.dtgMellonFwd.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgMellonFwd});
            // 
            // dgMellonFwd
            // 
            this.dgMellonFwd.GridControl = this.dtgMellonFwd;
            this.dgMellonFwd.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgMellonFwd.Name = "dgMellonFwd";
            this.dgMellonFwd.OptionsBehavior.Editable = false;
            this.dgMellonFwd.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgMellonFwd.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgMellonFwd.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgMellonFwd.OptionsSelection.InvertSelection = true;
            this.dgMellonFwd.OptionsSelection.MultiSelect = true;
            this.dgMellonFwd.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgMellonFwd.OptionsSelection.UseIndicatorForSelection = false;
            this.dgMellonFwd.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.dgMellonFwd.OptionsView.ShowGroupPanel = false;
            this.dgMellonFwd.OptionsView.ShowIndicator = false;
            this.dgMellonFwd.DoubleClick += new System.EventHandler(this.dgMellonFwd_DoubleClick);
            // 
            // txtMellonCode
            // 
            this.txtMellonCode.Location = new System.Drawing.Point(83, 12);
            this.txtMellonCode.Name = "txtMellonCode";
            this.txtMellonCode.Size = new System.Drawing.Size(91, 20);
            this.txtMellonCode.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Nest Id";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Mellon Id";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(343, 10);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 29;
            this.cmdInsert.Text = "Match";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdClose.Location = new System.Drawing.Point(129, 248);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 30;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmbSecurity
            // 
            this.cmbSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSecurity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbSecurity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSecurity.FormattingEnabled = true;
            this.cmbSecurity.Location = new System.Drawing.Point(468, 9);
            this.cmbSecurity.Name = "cmbSecurity";
            this.cmbSecurity.Size = new System.Drawing.Size(396, 21);
            this.cmbSecurity.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Trade Date";
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Enabled = false;
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(244, 9);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(90, 20);
            this.dtpTradeDate.TabIndex = 32;
            // 
            // frmMellonFwdNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(876, 277);
            this.Controls.Add(this.dtpTradeDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSecurity);
            this.Controls.Add(this.txtMellonCode);
            this.Controls.Add(this.dtgMellonFwd);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMellonFwdNames";
            this.Text = "Mellon Forward Names";
            this.Load += new System.EventHandler(this.frmMellonFwdNames_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMellonFwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMellonFwd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtgMellonFwd;
        private MyXtraGrid.MyGridView dgMellonFwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ComboBox cmbSecurity;
        public System.Windows.Forms.TextBox txtMellonCode;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DateTimePicker dtpTradeDate;
    }
}