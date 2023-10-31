namespace SGN
{
    partial class frmTradeSummary
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
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgTradeSummary = new MyXtraGrid.MyGridView();
            this.cmbView = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radWeek = new System.Windows.Forms.RadioButton();
            this.radYear = new System.Windows.Forms.RadioButton();
            this.radToday = new System.Windows.Forms.RadioButton();
            this.radMonth = new System.Windows.Forms.RadioButton();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.lblCopy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradeSummary)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.EmbeddedNavigator.Name = global::SGN.Properties.Resources.resource;
            this.dtg.Location = new System.Drawing.Point(3, 42);
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.MainView = this.dgTradeSummary;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(544, 296);
            this.dtg.TabIndex = 24;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTradeSummary});
            // 
            // dgTradeSummary
            // 
            this.dgTradeSummary.GridControl = this.dtg;
            this.dgTradeSummary.Name = "dgTradeSummary";
            this.dgTradeSummary.OptionsBehavior.Editable = false;
            this.dgTradeSummary.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgTradeSummary.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgTradeSummary.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgTradeSummary.OptionsSelection.InvertSelection = true;
            this.dgTradeSummary.OptionsSelection.MultiSelect = true;
            this.dgTradeSummary.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgTradeSummary.OptionsSelection.UseIndicatorForSelection = false;
            this.dgTradeSummary.OptionsView.ShowIndicator = false;
            this.dgTradeSummary.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTradeSummary_CustomDrawGroupRow);
            this.dgTradeSummary.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTradeSummary_DragObjectDrop);
            this.dgTradeSummary.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTradeSummary_ColumnWidthChanged);
            // 
            // cmbView
            // 
            this.cmbView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbView.FormattingEnabled = true;
            this.cmbView.Location = new System.Drawing.Point(400, 13);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(147, 21);
            this.cmbView.TabIndex = 26;
            this.cmbView.SelectedIndexChanged += new System.EventHandler(this.cmbView_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radWeek);
            this.groupBox5.Controls.Add(this.radYear);
            this.groupBox5.Controls.Add(this.radToday);
            this.groupBox5.Controls.Add(this.radMonth);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(310, 38);
            this.groupBox5.TabIndex = 37;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Date Range";
            // 
            // radWeek
            // 
            this.radWeek.AutoSize = true;
            this.radWeek.Location = new System.Drawing.Point(66, 16);
            this.radWeek.Name = "radWeek";
            this.radWeek.Size = new System.Drawing.Size(77, 17);
            this.radWeek.TabIndex = 33;
            this.radWeek.TabStop = true;
            this.radWeek.Text = "This Week";
            this.radWeek.UseVisualStyleBackColor = true;
            this.radWeek.CheckedChanged += new System.EventHandler(this.radWeek_CheckedChanged);
            // 
            // radYear
            // 
            this.radYear.AutoSize = true;
            this.radYear.Location = new System.Drawing.Point(233, 16);
            this.radYear.Name = "radYear";
            this.radYear.Size = new System.Drawing.Size(70, 17);
            this.radYear.TabIndex = 35;
            this.radYear.TabStop = true;
            this.radYear.Text = "This Year";
            this.radYear.UseVisualStyleBackColor = true;
            this.radYear.CheckedChanged += new System.EventHandler(this.radYear_CheckedChanged);
            // 
            // radToday
            // 
            this.radToday.AutoSize = true;
            this.radToday.Location = new System.Drawing.Point(5, 16);
            this.radToday.Name = "radToday";
            this.radToday.Size = new System.Drawing.Size(55, 17);
            this.radToday.TabIndex = 32;
            this.radToday.TabStop = true;
            this.radToday.Text = "Today";
            this.radToday.UseVisualStyleBackColor = true;
            this.radToday.CheckedChanged += new System.EventHandler(this.radToday_CheckedChanged);
            // 
            // radMonth
            // 
            this.radMonth.AutoSize = true;
            this.radMonth.Location = new System.Drawing.Point(149, 16);
            this.radMonth.Name = "radMonth";
            this.radMonth.Size = new System.Drawing.Size(78, 17);
            this.radMonth.TabIndex = 34;
            this.radMonth.TabStop = true;
            this.radMonth.Text = "This Month";
            this.radMonth.UseVisualStyleBackColor = true;
            this.radMonth.CheckedChanged += new System.EventHandler(this.radMonth_CheckedChanged);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(319, 13);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 38;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(518, 349);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 39;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // frmTradeSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(549, 365);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.cmbView);
            this.Controls.Add(this.dtg);
            this.Name = "frmTradeSummary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Trade Summary";
            this.Load += new System.EventHandler(this.frmTradeSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradeSummary)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgTradeSummary;
        private System.Windows.Forms.ComboBox cmbView;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radWeek;
        private System.Windows.Forms.RadioButton radYear;
        private System.Windows.Forms.RadioButton radToday;
        private System.Windows.Forms.RadioButton radMonth;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label lblCopy;
    }
}