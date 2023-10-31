namespace SGN
{
    partial class frmMargin
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.myGridView1 = new MyXtraGrid.MyGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dtgMgReceivedFiles = new MyXtraGrid.MyGridControl();
            this.dgMgReceivedFiles = new MyXtraGrid.MyGridView();
            this.dtgNRMargin = new MyXtraGrid.MyGridControl();
            this.dgNRMargin = new MyXtraGrid.MyGridView();
            this.myGridView6 = new MyXtraGrid.MyGridView();
            this.myGridView7 = new MyXtraGrid.MyGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtgMarginTransactions = new MyXtraGrid.MyGridControl();
            this.dgMarginTransactions = new MyXtraGrid.MyGridView();
            this.myGridView8 = new MyXtraGrid.MyGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.dtgMargin = new MyXtraGrid.MyGridControl();
            this.dgMargin = new MyXtraGrid.MyGridView();
            this.myGridView9 = new MyXtraGrid.MyGridView();
            this.tabLoans = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMgReceivedFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMgReceivedFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNRMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNRMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView7)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMarginTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMarginTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView8)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView9)).BeginInit();
            this.tabLoans.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(886, 682);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 17;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(805, 682);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 28);
            this.cmdRefresh.TabIndex = 18;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInsert.Location = new System.Drawing.Point(724, 682);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 29);
            this.cmdInsert.TabIndex = 26;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExcel.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.cmdExcel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdExcel.FlatAppearance.BorderSize = 0;
            this.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExcel.Location = new System.Drawing.Point(563, 682);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(76, 29);
            this.cmdExcel.TabIndex = 34;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = false;
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // myGridView1
            // 
            this.myGridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.myGridView1.Name = "myGridView1";
            this.myGridView1.OptionsBehavior.Editable = false;
            this.myGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.dtgMgReceivedFiles);
            this.tabPage4.Controls.Add(this.dtgNRMargin);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(941, 647);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Received Files";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 455);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(355, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "File Not Received (is in DB, but CBLC file for this broker was not received)";
            // 
            // dtgMgReceivedFiles
            // 
            this.dtgMgReceivedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgMgReceivedFiles.EmbeddedNavigator.Name = "";
            this.dtgMgReceivedFiles.Location = new System.Drawing.Point(6, 6);
            this.dtgMgReceivedFiles.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgMgReceivedFiles.MainView = this.dgMgReceivedFiles;
            this.dtgMgReceivedFiles.Name = "dtgMgReceivedFiles";
            this.dtgMgReceivedFiles.Size = new System.Drawing.Size(542, 446);
            this.dtgMgReceivedFiles.TabIndex = 30;
            this.dtgMgReceivedFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgMgReceivedFiles});
            // 
            // dgMgReceivedFiles
            // 
            this.dgMgReceivedFiles.GridControl = this.dtgMgReceivedFiles;
            this.dgMgReceivedFiles.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgMgReceivedFiles.Name = "dgMgReceivedFiles";
            this.dgMgReceivedFiles.OptionsBehavior.Editable = false;
            this.dgMgReceivedFiles.OptionsView.ColumnAutoWidth = false;
            this.dgMgReceivedFiles.OptionsView.ShowGroupPanel = false;
            // 
            // dtgNRMargin
            // 
            this.dtgNRMargin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgNRMargin.EmbeddedNavigator.Name = "";
            this.dtgNRMargin.Location = new System.Drawing.Point(6, 471);
            this.dtgNRMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgNRMargin.MainView = this.dgNRMargin;
            this.dtgNRMargin.Name = "dtgNRMargin";
            this.dtgNRMargin.Size = new System.Drawing.Size(929, 170);
            this.dtgNRMargin.TabIndex = 29;
            this.dtgNRMargin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgNRMargin});
            // 
            // dgNRMargin
            // 
            this.dgNRMargin.GridControl = this.dtgNRMargin;
            this.dgNRMargin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgNRMargin.Name = "dgNRMargin";
            this.dgNRMargin.OptionsBehavior.Editable = false;
            this.dgNRMargin.OptionsView.ShowGroupPanel = false;
            this.dgNRMargin.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgNRMargin.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            // 
            // myGridView6
            // 
            this.myGridView6.Name = "myGridView6";
            // 
            // myGridView7
            // 
            this.myGridView7.Name = "myGridView7";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dtgMarginTransactions);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(941, 647);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transactions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtgMarginTransactions
            // 
            this.dtgMarginTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgMarginTransactions.EmbeddedNavigator.Name = "";
            this.dtgMarginTransactions.Location = new System.Drawing.Point(6, 18);
            this.dtgMarginTransactions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgMarginTransactions.MainView = this.dgMarginTransactions;
            this.dtgMarginTransactions.Name = "dtgMarginTransactions";
            this.dtgMarginTransactions.Size = new System.Drawing.Size(929, 623);
            this.dtgMarginTransactions.TabIndex = 26;
            this.dtgMarginTransactions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgMarginTransactions,
            this.myGridView8});
            // 
            // dgMarginTransactions
            // 
            this.dgMarginTransactions.GridControl = this.dtgMarginTransactions;
            this.dgMarginTransactions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgMarginTransactions.Name = "dgMarginTransactions";
            this.dgMarginTransactions.OptionsBehavior.Editable = false;
            this.dgMarginTransactions.OptionsView.ColumnAutoWidth = false;
            this.dgMarginTransactions.DoubleClick += new System.EventHandler(this.dgMarginTransactions_DoubleClick);
            this.dgMarginTransactions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgMarginTransactions.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            // 
            // myGridView8
            // 
            this.myGridView8.GridControl = this.dtgMarginTransactions;
            this.myGridView8.Name = "myGridView8";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdExpand);
            this.tabPage1.Controls.Add(this.cmdCollapse);
            this.tabPage1.Controls.Add(this.dtgMargin);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(941, 647);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Positions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // cmdExpand
            // 
            this.cmdExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(307, 5);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand.TabIndex = 32;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(372, 5);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse.TabIndex = 33;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // dtgMargin
            // 
            this.dtgMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgMargin.EmbeddedNavigator.Name = "";
            this.dtgMargin.Location = new System.Drawing.Point(6, 29);
            this.dtgMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgMargin.MainView = this.dgMargin;
            this.dtgMargin.Name = "dtgMargin";
            this.dtgMargin.Size = new System.Drawing.Size(929, 612);
            this.dtgMargin.TabIndex = 25;
            this.dtgMargin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgMargin,
            this.myGridView9});
            // 
            // dgMargin
            // 
            this.dgMargin.GridControl = this.dtgMargin;
            this.dgMargin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgMargin.Name = "dgMargin";
            this.dgMargin.OptionsBehavior.Editable = false;
            this.dgMargin.DoubleClick += new System.EventHandler(this.dgMargin_DoubleClick);
            this.dgMargin.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgStockLoan_CustomDrawGroupRow);
            this.dgMargin.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgStockLoan_CustomSummaryCalculate);
            this.dgMargin.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgMargin.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgMargin.EndGrouping += new System.EventHandler(this.dgStockLoan_EndGrouping);
            this.dgMargin.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgMargin_RowStyle);
            // 
            // myGridView9
            // 
            this.myGridView9.GridControl = this.dtgMargin;
            this.myGridView9.Name = "myGridView9";
            // 
            // tabLoans
            // 
            this.tabLoans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLoans.Controls.Add(this.tabPage1);
            this.tabLoans.Controls.Add(this.tabPage2);
            this.tabLoans.Controls.Add(this.tabPage4);
            this.tabLoans.Location = new System.Drawing.Point(12, 3);
            this.tabLoans.Name = "tabLoans";
            this.tabLoans.SelectedIndex = 0;
            this.tabLoans.Size = new System.Drawing.Size(949, 673);
            this.tabLoans.TabIndex = 31;
            this.tabLoans.SelectedIndexChanged += new System.EventHandler(this.tabLoans_SelectedIndexChanged);
            // 
            // frmMargin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(968, 716);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.tabLoans);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.button1);
            this.Name = "frmMargin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Margin";
            this.Load += new System.EventHandler(this.frmMargin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMgReceivedFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMgReceivedFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgNRMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgNRMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView7)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMarginTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMarginTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView8)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView9)).EndInit();
            this.tabLoans.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Button cmdExcel;
        private MyXtraGrid.MyGridView myGridView1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private MyXtraGrid.MyGridControl dtgMgReceivedFiles;
        private MyXtraGrid.MyGridView dgMgReceivedFiles;
        private MyXtraGrid.MyGridControl dtgNRMargin;
        private MyXtraGrid.MyGridView dgNRMargin;
        private MyXtraGrid.MyGridView myGridView6;
        private MyXtraGrid.MyGridView myGridView7;
        private System.Windows.Forms.TabPage tabPage2;
        private MyXtraGrid.MyGridControl dtgMarginTransactions;
        private MyXtraGrid.MyGridView dgMarginTransactions;
        private MyXtraGrid.MyGridView myGridView8;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private MyXtraGrid.MyGridControl dtgMargin;
        private MyXtraGrid.MyGridView dgMargin;
        private MyXtraGrid.MyGridView myGridView9;
        private System.Windows.Forms.TabControl tabLoans;

    }
}