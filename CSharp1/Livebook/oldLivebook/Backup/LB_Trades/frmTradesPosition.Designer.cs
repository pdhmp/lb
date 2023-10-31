namespace SGN
{
    partial class frmTradesPosition
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
            this.dgTradesPos = new MyXtraGrid.MyGridView();
            this.rad6m = new System.Windows.Forms.RadioButton();
            this.radMonth = new System.Windows.Forms.RadioButton();
            this.radWeek = new System.Windows.Forms.RadioButton();
            this.radToday = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblIdPosition = new System.Windows.Forms.Label();
            this.lblPortfolio = new System.Windows.Forms.Label();
            this.chkAllBooks = new System.Windows.Forms.CheckBox();
            this.lblTable = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradesPos)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(3, 68);
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.MainView = this.dgTradesPos;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(537, 221);
            this.dtg.TabIndex = 25;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTradesPos});
            // 
            // dgTradesPos
            // 
            this.dgTradesPos.GridControl = this.dtg;
            this.dgTradesPos.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTradesPos.Name = "dgTradesPos";
            this.dgTradesPos.OptionsBehavior.Editable = false;
            this.dgTradesPos.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgTradesPos.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgTradesPos.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgTradesPos.OptionsSelection.InvertSelection = true;
            this.dgTradesPos.OptionsSelection.MultiSelect = true;
            this.dgTradesPos.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgTradesPos.OptionsSelection.UseIndicatorForSelection = false;
            this.dgTradesPos.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.dgTradesPos.OptionsView.ShowGroupPanel = false;
            this.dgTradesPos.OptionsView.ShowIndicator = false;
            this.dgTradesPos.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTradesPos_CustomDrawGroupRow);
            // 
            // rad6m
            // 
            this.rad6m.AutoSize = true;
            this.rad6m.Location = new System.Drawing.Point(233, 16);
            this.rad6m.Name = "rad6m";
            this.rad6m.Size = new System.Drawing.Size(68, 17);
            this.rad6m.TabIndex = 35;
            this.rad6m.TabStop = true;
            this.rad6m.Text = "6 months";
            this.rad6m.UseVisualStyleBackColor = true;
            this.rad6m.CheckedChanged += new System.EventHandler(this.radYear_CheckedChanged);
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radWeek);
            this.groupBox5.Controls.Add(this.rad6m);
            this.groupBox5.Controls.Add(this.radToday);
            this.groupBox5.Controls.Add(this.radMonth);
            this.groupBox5.Location = new System.Drawing.Point(3, 24);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(310, 38);
            this.groupBox5.TabIndex = 36;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Date Range";
            // 
            // lblIdPosition
            // 
            this.lblIdPosition.AutoSize = true;
            this.lblIdPosition.Location = new System.Drawing.Point(350, 5);
            this.lblIdPosition.Name = "lblIdPosition";
            this.lblIdPosition.Size = new System.Drawing.Size(0, 13);
            this.lblIdPosition.TabIndex = 37;
            this.lblIdPosition.Visible = false;
            // 
            // lblPortfolio
            // 
            this.lblPortfolio.AutoSize = true;
            this.lblPortfolio.Location = new System.Drawing.Point(5, 5);
            this.lblPortfolio.Name = "lblPortfolio";
            this.lblPortfolio.Size = new System.Drawing.Size(0, 13);
            this.lblPortfolio.TabIndex = 38;
            // 
            // chkAllBooks
            // 
            this.chkAllBooks.AutoSize = true;
            this.chkAllBooks.Location = new System.Drawing.Point(322, 40);
            this.chkAllBooks.Name = "chkAllBooks";
            this.chkAllBooks.Size = new System.Drawing.Size(159, 17);
            this.chkAllBooks.TabIndex = 39;
            this.chkAllBooks.Text = "Include Trades for All Books";
            this.chkAllBooks.UseVisualStyleBackColor = true;
            this.chkAllBooks.CheckedChanged += new System.EventHandler(this.chkAllBooks_CheckedChanged);
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(459, 9);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(0, 13);
            this.lblTable.TabIndex = 40;
            this.lblTable.Visible = false;
            // 
            // frmTradesPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 301);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.chkAllBooks);
            this.Controls.Add(this.lblPortfolio);
            this.Controls.Add(this.lblIdPosition);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.dtg);
            this.Name = "frmTradesPosition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Trades for Selected Position";
            this.Load += new System.EventHandler(this.frmTradesPosition_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradesPos)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgTradesPos;
        private System.Windows.Forms.RadioButton rad6m;
        private System.Windows.Forms.RadioButton radMonth;
        private System.Windows.Forms.RadioButton radWeek;
        private System.Windows.Forms.RadioButton radToday;
        private System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.Label lblIdPosition;
        private System.Windows.Forms.Label lblPortfolio;
        private System.Windows.Forms.CheckBox chkAllBooks;
        public System.Windows.Forms.Label lblTable;
    }
}