namespace QuantMonitor
{
    partial class frmValueBZViewer
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
            this.dtgStratData = new NCustomControls.MyGridControl();
            this.dgStratData = new NCustomControls.MyGridView();
            this.tmrRefreshGrid = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgStratData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgStratData)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgStratData
            // 
            this.dtgStratData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgStratData.Location = new System.Drawing.Point(12, 38);
            this.dtgStratData.LookAndFeel.SkinName = "Blue";
            this.dtgStratData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgStratData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgStratData.MainView = this.dgStratData;
            this.dtgStratData.Name = "dtgStratData";
            this.dtgStratData.Size = new System.Drawing.Size(438, 328);
            this.dtgStratData.TabIndex = 26;
            this.dtgStratData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgStratData});
            // 
            // dgStratData
            // 
            this.dgStratData.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgStratData.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgStratData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgStratData.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgStratData.GridControl = this.dtgStratData;
            this.dgStratData.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgStratData.Name = "dgStratData";
            this.dgStratData.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgStratData.OptionsBehavior.Editable = false;
            this.dgStratData.OptionsSelection.UseIndicatorForSelection = false;
            this.dgStratData.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgStratData_CustomDrawGroupRow);
            this.dgStratData.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgStratData_DragObjectDrop);
            this.dgStratData.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgStratData_ColumnWidthChanged);
            // 
            // tmrRefreshGrid
            // 
            this.tmrRefreshGrid.Tick += new System.EventHandler(this.tmrRefreshGrid_Tick);
            // 
            // frmValueBZViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(610, 428);
            this.Controls.Add(this.dtgStratData);
            this.MaximizeBox = false;
            this.Name = "frmValueBZViewer";
            this.Text = "Sector Value BZ";
            this.Load += new System.EventHandler(this.frmValueBZViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgStratData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgStratData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public NCustomControls.MyGridControl dtgStratData;
        public NCustomControls.MyGridView dgStratData;
        private System.Windows.Forms.Timer tmrRefreshGrid;
    }
}