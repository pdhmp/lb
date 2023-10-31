namespace SGN
{
    partial class frmOrders
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dtgTrade = new MyXtraGrid.MyGridControl();
            this.dgTrades = new MyXtraGrid.MyGridView();
            this.myGridView1 = new MyXtraGrid.MyGridView();
            this.myGridView2 = new MyXtraGrid.MyGridView();
            this.myGridView3 = new MyXtraGrid.MyGridView();
            this.tbopen = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radCancelled = new System.Windows.Forms.RadioButton();
            this.radDone = new System.Windows.Forms.RadioButton();
            this.radOpen = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.dtgOrders = new MyXtraGrid.MyGridControl();
            this.dgOrders = new MyXtraGrid.MyGridView();
            this.myGridView4 = new MyXtraGrid.MyGridView();
            this.tbMenu = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView3)).BeginInit();
            this.tbopen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).BeginInit();
            this.tbMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dtgTrade);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 454);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Trades";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dtgTrade
            // 
            this.dtgTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgTrade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dtgTrade.Location = new System.Drawing.Point(5, 5);
            this.dtgTrade.LookAndFeel.SkinName = "Blue";
            this.dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgTrade.LookAndFeel.UseWindowsXPTheme = true;
            this.dtgTrade.MainView = this.dgTrades;
            this.dtgTrade.Name = "dtgTrade";
            this.dtgTrade.Size = new System.Drawing.Size(780, 443);
            this.dtgTrade.TabIndex = 24;
            this.dtgTrade.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTrades,
            this.myGridView1});
            // 
            // dgTrades
            // 
            this.dgTrades.GridControl = this.dtgTrade;
            this.dgTrades.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTrades.Name = "dgTrades";
            this.dgTrades.OptionsBehavior.Editable = false;
            this.dgTrades.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgTrades.OptionsSelection.MultiSelect = true;
            this.dgTrades.OptionsView.ColumnAutoWidth = false;
            this.dgTrades.ShowCustomizationForm += new System.EventHandler(this.dgTrades_ShowCustomizationForm);
            this.dgTrades.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTrades_CustomDrawGroupRow);
            this.dgTrades.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgTrades_CustomDrawCell);
            this.dgTrades.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTrades_DragObjectDrop);
            this.dgTrades.DoubleClick += new System.EventHandler(this.dgTrades_DoubleClick);
            this.dgTrades.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTrades_ColumnWidthChanged);
            this.dgTrades.EndGrouping += new System.EventHandler(this.dgTrades_EndGrouping);
            this.dgTrades.HideCustomizationForm += new System.EventHandler(this.dgTrades_HideCustomizationForm);
            // 
            // myGridView1
            // 
            this.myGridView1.GridControl = this.dtgTrade;
            this.myGridView1.Name = "myGridView1";
            // 
            // myGridView2
            // 
            this.myGridView2.Name = "myGridView2";
            // 
            // myGridView3
            // 
            this.myGridView3.Name = "myGridView3";
            // 
            // tbopen
            // 
            this.tbopen.Controls.Add(this.button1);
            this.tbopen.Controls.Add(this.label1);
            this.tbopen.Controls.Add(this.radCancelled);
            this.tbopen.Controls.Add(this.radDone);
            this.tbopen.Controls.Add(this.radOpen);
            this.tbopen.Controls.Add(this.radAll);
            this.tbopen.Controls.Add(this.cmdCollapse);
            this.tbopen.Controls.Add(this.cmdExpand);
            this.tbopen.Controls.Add(this.dtgOrders);
            this.tbopen.Location = new System.Drawing.Point(4, 22);
            this.tbopen.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbopen.Name = "tbopen";
            this.tbopen.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbopen.Size = new System.Drawing.Size(791, 454);
            this.tbopen.TabIndex = 0;
            this.tbopen.Text = "Orders";
            this.tbopen.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(668, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 22);
            this.button1.TabIndex = 40;
            this.button1.Text = "Refresh";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Filter Status:";
            // 
            // radCancelled
            // 
            this.radCancelled.AutoSize = true;
            this.radCancelled.Location = new System.Drawing.Point(256, 8);
            this.radCancelled.Name = "radCancelled";
            this.radCancelled.Size = new System.Drawing.Size(72, 17);
            this.radCancelled.TabIndex = 38;
            this.radCancelled.TabStop = true;
            this.radCancelled.Text = "Cancelled";
            this.radCancelled.UseVisualStyleBackColor = true;
            this.radCancelled.CheckedChanged += new System.EventHandler(this.radCancelled_CheckedChanged);
            // 
            // radDone
            // 
            this.radDone.AutoSize = true;
            this.radDone.Location = new System.Drawing.Point(199, 8);
            this.radDone.Name = "radDone";
            this.radDone.Size = new System.Drawing.Size(51, 17);
            this.radDone.TabIndex = 37;
            this.radDone.TabStop = true;
            this.radDone.Text = "Done";
            this.radDone.UseVisualStyleBackColor = true;
            this.radDone.CheckedChanged += new System.EventHandler(this.radDone_CheckedChanged);
            // 
            // radOpen
            // 
            this.radOpen.AutoSize = true;
            this.radOpen.Location = new System.Drawing.Point(142, 8);
            this.radOpen.Name = "radOpen";
            this.radOpen.Size = new System.Drawing.Size(51, 17);
            this.radOpen.TabIndex = 36;
            this.radOpen.TabStop = true;
            this.radOpen.Text = "Open";
            this.radOpen.UseVisualStyleBackColor = true;
            this.radOpen.CheckedChanged += new System.EventHandler(this.radOpen_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(100, 8);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(36, 17);
            this.radAll.TabIndex = 35;
            this.radAll.TabStop = true;
            this.radAll.Text = "All";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(345, 7);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(72, 22);
            this.cmdCollapse.TabIndex = 34;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // cmdExpand
            // 
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(423, 7);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(72, 22);
            this.cmdExpand.TabIndex = 33;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // dtgOrders
            // 
            this.dtgOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOrders.Location = new System.Drawing.Point(5, 35);
            this.dtgOrders.LookAndFeel.SkinName = "Blue";
            this.dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOrders.MainView = this.dgOrders;
            this.dtgOrders.Name = "dtgOrders";
            this.dtgOrders.Size = new System.Drawing.Size(781, 416);
            this.dtgOrders.TabIndex = 14;
            this.dtgOrders.TabStop = false;
            this.dtgOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOrders,
            this.myGridView4});
            // 
            // dgOrders
            // 
            this.dgOrders.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgOrders.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgOrders.GridControl = this.dtgOrders;
            this.dgOrders.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgOrders.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgOrders.Name = "dgOrders";
            this.dgOrders.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgOrders.OptionsSelection.MultiSelect = true;
            this.dgOrders.OptionsView.ColumnAutoWidth = false;
            this.dgOrders.RowHeight = 15;
            this.dgOrders.ShowCustomizationForm += new System.EventHandler(this.dgAbertas_ShowCustomizationForm);
            this.dgOrders.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgAbertas_CustomDrawGroupRow);
            this.dgOrders.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgAbertas_CustomDrawCell);
            this.dgOrders.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgAbertas_DragObjectDrop);
            this.dgOrders.DoubleClick += new System.EventHandler(this.dgAbertas_DoubleClick);
            this.dgOrders.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgAbertas_ColumnWidthChanged);
            this.dgOrders.HideCustomizationForm += new System.EventHandler(this.dgAbertas_HideCustomizationForm);
            // 
            // myGridView4
            // 
            this.myGridView4.GridControl = this.dtgOrders;
            this.myGridView4.Name = "myGridView4";
            // 
            // tbMenu
            // 
            this.tbMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMenu.Controls.Add(this.tbopen);
            this.tbMenu.Controls.Add(this.tabPage1);
            this.tbMenu.Location = new System.Drawing.Point(11, 13);
            this.tbMenu.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbMenu.Name = "tbMenu";
            this.tbMenu.SelectedIndex = 0;
            this.tbMenu.Size = new System.Drawing.Size(799, 480);
            this.tbMenu.TabIndex = 16;
            this.tbMenu.Click += new System.EventHandler(this.tbMenu_Click);
            this.tbMenu.SelectedIndexChanged += new System.EventHandler(this.tbMenu_SelectedIndexChanged);
            // 
            // frmOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(821, 506);
            this.Controls.Add(this.tbMenu);
            this.Name = "frmOrders";
            this.Text = "Order Review";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView3)).EndInit();
            this.tbopen.ResumeLayout(false);
            this.tbopen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).EndInit();
            this.tbMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private MyXtraGrid.MyGridControl dtgTrade;
        private MyXtraGrid.MyGridView dgTrades;
        private MyXtraGrid.MyGridView myGridView1;
        private MyXtraGrid.MyGridView myGridView2;
        private MyXtraGrid.MyGridView myGridView3;
        private System.Windows.Forms.TabPage tbopen;
        private MyXtraGrid.MyGridControl dtgOrders;
        private MyXtraGrid.MyGridView dgOrders;
        private MyXtraGrid.MyGridView myGridView4;
        private System.Windows.Forms.TabControl tbMenu;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.RadioButton radCancelled;
        private System.Windows.Forms.RadioButton radDone;
        private System.Windows.Forms.RadioButton radOpen;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;


    }
}