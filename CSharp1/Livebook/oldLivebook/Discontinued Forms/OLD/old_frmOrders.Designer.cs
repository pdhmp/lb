namespace SGN
{
    partial class old_frmOrders
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
            this.tbMenu = new System.Windows.Forms.TabControl();
            this.tbopen = new System.Windows.Forms.TabPage();
            this.dtg2 = new MyXtraGrid.MyGridControl();
            this.dgAbertas = new MyXtraGrid.MyGridView();
            this.tbclose = new System.Windows.Forms.TabPage();
            this.dtgFec = new MyXtraGrid.MyGridControl();
            this.dgFechadas = new MyXtraGrid.MyGridView();
            this.tbcancel = new System.Windows.Forms.TabPage();
            this.dtgCancel = new MyXtraGrid.MyGridControl();
            this.dgCanceladas = new MyXtraGrid.MyGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dtgTrade = new MyXtraGrid.MyGridControl();
            this.dgTrades = new MyXtraGrid.MyGridView();
            this.tbMenu.SuspendLayout();
            this.tbopen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAbertas)).BeginInit();
            this.tbclose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFechadas)).BeginInit();
            this.tbcancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanceladas)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMenu
            // 
            this.tbMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMenu.Controls.Add(this.tbopen);
            this.tbMenu.Controls.Add(this.tbclose);
            this.tbMenu.Controls.Add(this.tbcancel);
            this.tbMenu.Controls.Add(this.tabPage1);
            this.tbMenu.Location = new System.Drawing.Point(11, 13);
            this.tbMenu.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbMenu.Name = "tbMenu";
            this.tbMenu.SelectedIndex = 0;
            this.tbMenu.Size = new System.Drawing.Size(1220, 940);
            this.tbMenu.TabIndex = 16;
            this.tbMenu.Click += new System.EventHandler(this.tbMenu_Click);
            // 
            // tbopen
            // 
            this.tbopen.Controls.Add(this.dtg2);
            this.tbopen.Location = new System.Drawing.Point(4, 22);
            this.tbopen.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbopen.Name = "tbopen";
            this.tbopen.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbopen.Size = new System.Drawing.Size(1212, 914);
            this.tbopen.TabIndex = 0;
            this.tbopen.Text = "Open";
            this.tbopen.UseVisualStyleBackColor = true;
            // 
            // dtg2
            // 
            this.dtg2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg2.EmbeddedNavigator.Name = "";
            this.dtg2.Location = new System.Drawing.Point(1, 3);
            this.dtg2.MainView = this.dgAbertas;
            this.dtg2.Name = "dtg2";
            this.dtg2.Size = new System.Drawing.Size(1206, 904);
            this.dtg2.TabIndex = 14;
            this.dtg2.TabStop = false;
            this.dtg2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgAbertas});
            // 
            // dgAbertas
            // 
            this.dgAbertas.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgAbertas.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgAbertas.GridControl = this.dtg2;
            this.dgAbertas.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgAbertas.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgAbertas.Name = "dgAbertas";
            this.dgAbertas.OptionsView.ColumnAutoWidth = false;
            this.dgAbertas.RowHeight = 15;
            this.dgAbertas.ShowCustomizationForm += new System.EventHandler(this.dgAbertas_ShowCustomizationForm);
            this.dgAbertas.DoubleClick += new System.EventHandler(this.dgAbertas_DoubleClick);
            this.dgAbertas.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgAbertas_CustomDrawGroupRow);
            this.dgAbertas.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgCanceladas_DragObjectDrop);
            this.dgAbertas.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgAbertas_ColumnWidthChanged);
            this.dgAbertas.EndGrouping += new System.EventHandler(this.dgFechadas_EndGrouping);
            this.dgAbertas.HideCustomizationForm += new System.EventHandler(this.dgAbertas_HideCustomizationForm);
            // 
            // tbclose
            // 
            this.tbclose.Controls.Add(this.dtgFec);
            this.tbclose.Location = new System.Drawing.Point(4, 22);
            this.tbclose.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbclose.Name = "tbclose";
            this.tbclose.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbclose.Size = new System.Drawing.Size(1212, 914);
            this.tbclose.TabIndex = 1;
            this.tbclose.Text = "Done";
            this.tbclose.UseVisualStyleBackColor = true;
            // 
            // dtgFec
            // 
            this.dtgFec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgFec.EmbeddedNavigator.Name = "";
            this.dtgFec.Location = new System.Drawing.Point(2, 3);
            this.dtgFec.MainView = this.dgFechadas;
            this.dtgFec.Name = "dtgFec";
            this.dtgFec.Size = new System.Drawing.Size(1205, 904);
            this.dtgFec.TabIndex = 15;
            this.dtgFec.TabStop = false;
            this.dtgFec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgFechadas});
            // 
            // dgFechadas
            // 
            this.dgFechadas.GridControl = this.dtgFec;
            this.dgFechadas.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgFechadas.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgFechadas.Name = "dgFechadas";
            this.dgFechadas.OptionsBehavior.Editable = false;
            this.dgFechadas.OptionsView.ColumnAutoWidth = false;
            this.dgFechadas.RowHeight = 15;
            this.dgFechadas.ShowCustomizationForm += new System.EventHandler(this.dgFechadas_ShowCustomizationForm);
            this.dgFechadas.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgFechadas_CustomDrawGroupRow);
            this.dgFechadas.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgFechadas_DragObjectDrop);
            this.dgFechadas.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgFechadas_ColumnWidthChanged);
            this.dgFechadas.EndGrouping += new System.EventHandler(this.dgFechadas_EndGrouping);
            this.dgFechadas.HideCustomizationForm += new System.EventHandler(this.dgFechadas_HideCustomizationForm);
            // 
            // tbcancel
            // 
            this.tbcancel.Controls.Add(this.dtgCancel);
            this.tbcancel.Location = new System.Drawing.Point(4, 22);
            this.tbcancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbcancel.Name = "tbcancel";
            this.tbcancel.Size = new System.Drawing.Size(1212, 914);
            this.tbcancel.TabIndex = 2;
            this.tbcancel.Text = "Cancel";
            this.tbcancel.UseVisualStyleBackColor = true;
            // 
            // dtgCancel
            // 
            this.dtgCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgCancel.EmbeddedNavigator.Name = "";
            this.dtgCancel.Location = new System.Drawing.Point(2, 3);
            this.dtgCancel.MainView = this.dgCanceladas;
            this.dtgCancel.Name = "dtgCancel";
            this.dtgCancel.Size = new System.Drawing.Size(1207, 908);
            this.dtgCancel.TabIndex = 15;
            this.dtgCancel.TabStop = false;
            this.dtgCancel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgCanceladas});
            // 
            // dgCanceladas
            // 
            this.dgCanceladas.GridControl = this.dtgCancel;
            this.dgCanceladas.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgCanceladas.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgCanceladas.Name = "dgCanceladas";
            this.dgCanceladas.OptionsBehavior.Editable = false;
            this.dgCanceladas.OptionsView.ColumnAutoWidth = false;
            this.dgCanceladas.RowHeight = 15;
            this.dgCanceladas.ShowCustomizationForm += new System.EventHandler(this.dgCanceladas_ShowCustomizationForm);
            this.dgCanceladas.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgCanceladas_DragObjectDrop);
            this.dgCanceladas.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgCanceladas_ColumnWidthChanged);
            this.dgCanceladas.EndGrouping += new System.EventHandler(this.dgCanceladas_EndGrouping);
            this.dgCanceladas.HideCustomizationForm += new System.EventHandler(this.dgCanceladas_HideCustomizationForm);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dtgTrade);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1212, 914);
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
            this.dtgTrade.EmbeddedNavigator.Name = "";
            this.dtgTrade.Location = new System.Drawing.Point(6, 6);
            this.dtgTrade.LookAndFeel.SkinName = "Blue";
            this.dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgTrade.LookAndFeel.UseWindowsXPTheme = true;
            this.dtgTrade.MainView = this.dgTrades;
            this.dtgTrade.Name = "dtgTrade";
            this.dtgTrade.Size = new System.Drawing.Size(1200, 902);
            this.dtgTrade.TabIndex = 24;
            this.dtgTrade.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTrades});
            // 
            // dgTrades
            // 
            this.dgTrades.GridControl = this.dtgTrade;
            this.dgTrades.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTrades.Name = "dgTrades";
            this.dgTrades.OptionsBehavior.Editable = false;
            this.dgTrades.OptionsView.ColumnAutoWidth = false;
            this.dgTrades.ShowCustomizationForm += new System.EventHandler(this.dgTrades_ShowCustomizationForm);
            this.dgTrades.DoubleClick += new System.EventHandler(this.dgTrades_DoubleClick);
            this.dgTrades.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTrades_CustomDrawGroupRow);
            this.dgTrades.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTrades_DragObjectDrop);
            this.dgTrades.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTrades_ColumnWidthChanged);
            this.dgTrades.EndGrouping += new System.EventHandler(this.dgTrades_EndGrouping);
            this.dgTrades.HideCustomizationForm += new System.EventHandler(this.dgTrades_HideCustomizationForm);
            // 
            // frmOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1242, 966);
            this.Controls.Add(this.tbMenu);
            this.Name = "frmOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Open Orders";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.old_frmOrders_FormClosing);
            this.tbMenu.ResumeLayout(false);
            this.tbopen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAbertas)).EndInit();
            this.tbclose.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFechadas)).EndInit();
            this.tbcancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanceladas)).EndInit();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbMenu;
        private System.Windows.Forms.TabPage tbopen;
        private MyXtraGrid.MyGridControl dtg2;
        private MyXtraGrid.MyGridView dgAbertas;
        private System.Windows.Forms.TabPage tbclose;
        private MyXtraGrid.MyGridControl dtgFec;
        private MyXtraGrid.MyGridView dgFechadas;
        private System.Windows.Forms.TabPage tbcancel;
        private MyXtraGrid.MyGridControl dtgCancel;
        private MyXtraGrid.MyGridView dgCanceladas;
        private System.Windows.Forms.TabPage tabPage1;
        private MyXtraGrid.MyGridControl dtgTrade;
        private MyXtraGrid.MyGridView dgTrades;

    }
}