namespace LiveBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMargin));
            this.btnClose = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.myGridView1 = new MyXtraGrid.MyGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dtGridReceivedFiles = new MyXtraGrid.MyGridControl();
            this.gridViewReceivedFiles = new MyXtraGrid.MyGridView();
            this.dtGridNotRecMargin = new MyXtraGrid.MyGridControl();
            this.gridViewNotRecMargin = new MyXtraGrid.MyGridView();
            this.myGridView6 = new MyXtraGrid.MyGridView();
            this.myGridView7 = new MyXtraGrid.MyGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dtGridMarginTransactions = new MyXtraGrid.MyGridControl();
            this.gridViewMarginTransactions = new MyXtraGrid.MyGridView();
            this.myGridView8 = new MyXtraGrid.MyGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnExpand = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.dtGridMargin = new MyXtraGrid.MyGridControl();
            this.gridViewMargin = new MyXtraGrid.MyGridView();
            this.myGridView9 = new MyXtraGrid.MyGridView();
            this.tabLoans = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridReceivedFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReceivedFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridNotRecMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNotRecMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView7)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMarginTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMarginTransactions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView8)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView9)).BeginInit();
            this.tabLoans.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(886, 682);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 28);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.cmdRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
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
            this.cmdExcel.Click += new System.EventHandler(this.btnExcel_Click);
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
            this.tabPage4.Controls.Add(this.dtGridReceivedFiles);
            this.tabPage4.Controls.Add(this.dtGridNotRecMargin);
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
            // dtGridReceivedFiles
            // 
            this.dtGridReceivedFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtGridReceivedFiles.Location = new System.Drawing.Point(6, 6);
            this.dtGridReceivedFiles.LookAndFeel.SkinName = "Blue";
            this.dtGridReceivedFiles.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtGridReceivedFiles.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtGridReceivedFiles.MainView = this.gridViewReceivedFiles;
            this.dtGridReceivedFiles.Name = "dtGridReceivedFiles";
            this.dtGridReceivedFiles.Size = new System.Drawing.Size(929, 446);
            this.dtGridReceivedFiles.TabIndex = 30;
            this.dtGridReceivedFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewReceivedFiles});
            // 
            // gridViewReceivedFiles
            // 
            this.gridViewReceivedFiles.GridControl = this.dtGridReceivedFiles;
            this.gridViewReceivedFiles.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridViewReceivedFiles.Name = "gridViewReceivedFiles";
            this.gridViewReceivedFiles.OptionsBehavior.Editable = false;
            this.gridViewReceivedFiles.OptionsView.ColumnAutoWidth = false;
            this.gridViewReceivedFiles.OptionsView.ShowGroupPanel = false;
            // 
            // dtGridNotRecMargin
            // 
            this.dtGridNotRecMargin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridNotRecMargin.Location = new System.Drawing.Point(6, 471);
            this.dtGridNotRecMargin.LookAndFeel.SkinName = "Blue";
            this.dtGridNotRecMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtGridNotRecMargin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtGridNotRecMargin.MainView = this.gridViewNotRecMargin;
            this.dtGridNotRecMargin.Name = "dtGridNotRecMargin";
            this.dtGridNotRecMargin.Size = new System.Drawing.Size(929, 170);
            this.dtGridNotRecMargin.TabIndex = 29;
            this.dtGridNotRecMargin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNotRecMargin});
            // 
            // gridViewNotRecMargin
            // 
            this.gridViewNotRecMargin.GridControl = this.dtGridNotRecMargin;
            this.gridViewNotRecMargin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridViewNotRecMargin.Name = "gridViewNotRecMargin";
            this.gridViewNotRecMargin.OptionsBehavior.Editable = false;
            this.gridViewNotRecMargin.OptionsView.ShowGroupPanel = false;
            this.gridViewNotRecMargin.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.gridViewNotRecMargin.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
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
            this.tabPage2.Controls.Add(this.dtGridMarginTransactions);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(941, 647);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Transactions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dtGridMarginTransactions
            // 
            this.dtGridMarginTransactions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridMarginTransactions.Location = new System.Drawing.Point(6, 18);
            this.dtGridMarginTransactions.LookAndFeel.SkinName = "Blue";
            this.dtGridMarginTransactions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtGridMarginTransactions.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtGridMarginTransactions.MainView = this.gridViewMarginTransactions;
            this.dtGridMarginTransactions.Name = "dtGridMarginTransactions";
            this.dtGridMarginTransactions.Size = new System.Drawing.Size(929, 623);
            this.dtGridMarginTransactions.TabIndex = 26;
            this.dtGridMarginTransactions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMarginTransactions,
            this.myGridView8});
            // 
            // gridViewMarginTransactions
            // 
            this.gridViewMarginTransactions.GridControl = this.dtGridMarginTransactions;
            this.gridViewMarginTransactions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridViewMarginTransactions.Name = "gridViewMarginTransactions";
            this.gridViewMarginTransactions.OptionsBehavior.Editable = false;
            this.gridViewMarginTransactions.OptionsView.ColumnAutoWidth = false;
            this.gridViewMarginTransactions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.gridViewMarginTransactions.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.gridViewMarginTransactions.DoubleClick += new System.EventHandler(this.dgMarginTransactions_DoubleClick);
            // 
            // myGridView8
            // 
            this.myGridView8.GridControl = this.dtGridMarginTransactions;
            this.myGridView8.Name = "myGridView8";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnExpand);
            this.tabPage1.Controls.Add(this.cmdCollapse);
            this.tabPage1.Controls.Add(this.dtGridMargin);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(941, 647);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Positions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnExpand
            // 
            this.btnExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpand.Location = new System.Drawing.Point(307, 5);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(59, 22);
            this.btnExpand.TabIndex = 32;
            this.btnExpand.Text = "Expand";
            this.btnExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExpand.UseVisualStyleBackColor = false;
            this.btnExpand.Click += new System.EventHandler(this.btnExpand_Click);
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
            this.cmdCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // dtGridMargin
            // 
            this.dtGridMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtGridMargin.Location = new System.Drawing.Point(6, 29);
            this.dtGridMargin.LookAndFeel.SkinName = "Blue";
            this.dtGridMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtGridMargin.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtGridMargin.MainView = this.gridViewMargin;
            this.dtGridMargin.Name = "dtGridMargin";
            this.dtGridMargin.Size = new System.Drawing.Size(929, 612);
            this.dtGridMargin.TabIndex = 25;
            this.dtGridMargin.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMargin,
            this.myGridView9});
            // 
            // gridViewMargin
            // 
            this.gridViewMargin.GridControl = this.dtGridMargin;
            this.gridViewMargin.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridViewMargin.Name = "gridViewMargin";
            this.gridViewMargin.OptionsBehavior.Editable = false;
            this.gridViewMargin.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.gridViewMargin.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgStockLoan_CustomDrawGroupRow);
            this.gridViewMargin.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgMargin_RowStyle);
            this.gridViewMargin.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.gridViewMargin.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgStockLoan_CustomSummaryCalculate);
            this.gridViewMargin.EndGrouping += new System.EventHandler(this.dgStockLoan_EndGrouping);
            this.gridViewMargin.DoubleClick += new System.EventHandler(this.dgMargin_DoubleClick);
            // 
            // myGridView9
            // 
            this.myGridView9.GridControl = this.dtGridMargin;
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
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMargin";
            this.Text = "Margin";
            this.Load += new System.EventHandler(this.frmMargin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridReceivedFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewReceivedFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridNotRecMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNotRecMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView7)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMarginTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMarginTransactions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView8)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView9)).EndInit();
            this.tabLoans.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Button cmdExcel;
        private MyXtraGrid.MyGridView myGridView1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private MyXtraGrid.MyGridControl dtGridReceivedFiles;
        private MyXtraGrid.MyGridView gridViewReceivedFiles;
        private MyXtraGrid.MyGridControl dtGridNotRecMargin;
        private MyXtraGrid.MyGridView gridViewNotRecMargin;
        private MyXtraGrid.MyGridView myGridView6;
        private MyXtraGrid.MyGridView myGridView7;
        private System.Windows.Forms.TabPage tabPage2;
        private MyXtraGrid.MyGridControl dtGridMarginTransactions;
        private MyXtraGrid.MyGridView gridViewMarginTransactions;
        private MyXtraGrid.MyGridView myGridView8;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private MyXtraGrid.MyGridControl dtGridMargin;
        private MyXtraGrid.MyGridView gridViewMargin;
        private MyXtraGrid.MyGridView myGridView9;
        private System.Windows.Forms.TabControl tabLoans;

    }
}