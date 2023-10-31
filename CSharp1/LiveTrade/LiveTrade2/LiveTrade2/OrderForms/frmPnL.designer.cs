namespace LiveTrade2
{
    partial class frmPnL
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
            this.components = new System.ComponentModel.Container();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.dtgPnL = new NCustomControls.MyGridControl();
            this.dgPnL = new NCustomControls.MyGridView();
            this.tmrReload = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPnL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPnL)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 250;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // dtgPnL
            // 
            this.dtgPnL.AllowDrop = true;
            this.dtgPnL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgPnL.Location = new System.Drawing.Point(0, -1);
            this.dtgPnL.LookAndFeel.SkinName = "Blue";
            this.dtgPnL.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgPnL.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgPnL.MainView = this.dgPnL;
            this.dtgPnL.Name = "dtgPnL";
            this.dtgPnL.Size = new System.Drawing.Size(933, 416);
            this.dtgPnL.TabIndex = 26;
            this.dtgPnL.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgPnL});
            this.dtgPnL.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            this.dtgPnL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            // 
            // dgPnL
            // 
            this.dgPnL.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgPnL.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgPnL.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgPnL.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgPnL.GridControl = this.dtgPnL;
            this.dgPnL.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgPnL.Name = "dgPnL";
            this.dgPnL.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgPnL.OptionsBehavior.Editable = false;
            this.dgPnL.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgPnL.OptionsSelection.UseIndicatorForSelection = false;
            this.dgPnL.OptionsView.ColumnAutoWidth = false;
            this.dgPnL.OptionsView.ShowIndicator = false;
            this.dgPnL.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgPnL_DragObjectDrop);
            this.dgPnL.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgPnL.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgQuotes_CustomDrawGroupRow);
            this.dgPnL.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgQuotes_GroupLevelStyle);
            this.dgPnL.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgPnL_ColumnWidthChanged);
            this.dgPnL.EndSorting += new System.EventHandler(this.dgQuotes_EndSorting);
            this.dgPnL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgAucQuotes_MouseUp);
            this.dgPnL.DoubleClick += new System.EventHandler(this.dgQuotes_DoubleClick);
            // 
            // tmrReload
            // 
            this.tmrReload.Interval = 5000;
            this.tmrReload.Tick += new System.EventHandler(this.tmrReload_Tick);
            // 
            // frmPnL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 416);
            this.Controls.Add(this.dtgPnL);
            this.Name = "frmPnL";
            this.Text = "Profit and Loss";
            this.Load += new System.EventHandler(this.frmPnL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPnL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPnL)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private NCustomControls.MyGridView dgPnL;
        private NCustomControls.MyGridControl dtgPnL;
        private System.Windows.Forms.Timer tmrReload;
    }
}

