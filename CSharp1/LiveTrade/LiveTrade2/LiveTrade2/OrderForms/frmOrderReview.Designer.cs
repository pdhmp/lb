namespace LiveTrade2
{
    partial class frmOrderReview
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
            this.lblCopy = new System.Windows.Forms.Label();
            this.dtgOrders = new NCustomControls.MyGridControl();
            this.dgOrders = new NCustomControls.MyGridView();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmdRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(702, 374);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 58;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // dtgOrders
            // 
            this.dtgOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOrders.Location = new System.Drawing.Point(-1, 1);
            this.dtgOrders.LookAndFeel.SkinName = "Blue";
            this.dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOrders.MainView = this.dgOrders;
            this.dtgOrders.Name = "dtgOrders";
            this.dtgOrders.Size = new System.Drawing.Size(745, 365);
            this.dtgOrders.TabIndex = 57;
            this.dtgOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOrders});
            // 
            // dgOrders
            // 
            this.dgOrders.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgOrders.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgOrders.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgOrders.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgOrders.GridControl = this.dtgOrders;
            this.dgOrders.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgOrders.Name = "dgOrders";
            this.dgOrders.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgOrders.OptionsBehavior.Editable = false;
            this.dgOrders.OptionsSelection.MultiSelect = true;
            this.dgOrders.OptionsSelection.UseIndicatorForSelection = false;
            this.dgOrders.OptionsView.ColumnAutoWidth = false;
            this.dgOrders.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgOrders_DragObjectDrop);
            this.dgOrders.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgOrders_CustomDrawGroupRow);
            this.dgOrders.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgOrders_ColumnWidthChanged);
            this.dgOrders.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(this.dgOrders_ShowGridMenu);
            this.dgOrders.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgOrders_CustomSummaryCalculate);
            this.dgOrders.DoubleClick += new System.EventHandler(this.dgOrders_DoubleClick);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 500;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdRefresh.Location = new System.Drawing.Point(130, 372);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(112, 19);
            this.cmdRefresh.TabIndex = 61;
            this.cmdRefresh.Text = "REFRESH";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // frmOrderReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(743, 399);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.dtgOrders);
            this.Name = "frmOrderReview";
            this.Text = "Order Review";
            this.Load += new System.EventHandler(this.fmrOrderReview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCopy;
        private NCustomControls.MyGridControl dtgOrders;
        private NCustomControls.MyGridView dgOrders;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Button cmdRefresh;
    }
}