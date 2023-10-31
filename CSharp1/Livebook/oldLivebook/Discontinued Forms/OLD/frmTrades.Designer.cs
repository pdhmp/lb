namespace SGN.Tela_Divididas
{
    partial class frmTrades
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
            this.dtgTrade = new DevExpress.XtraGrid.GridControl();
            this.dgTrades = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgTrade
            // 
            this.dtgTrade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgTrade.EmbeddedNavigator.Name = "";
            this.dtgTrade.Location = new System.Drawing.Point(12, 12);
            this.dtgTrade.MainView = this.dgTrades;
            this.dtgTrade.Name = "dtgTrade";
            this.dtgTrade.Size = new System.Drawing.Size(1218, 942);
            this.dtgTrade.TabIndex = 17;
            this.dtgTrade.TabStop = false;
            this.dtgTrade.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTrades});
            // 
            // dgTrades
            // 
            this.dgTrades.GridControl = this.dtgTrade;
            this.dgTrades.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgTrades.Name = "dgTrades";
            this.dgTrades.OptionsBehavior.Editable = false;
            this.dgTrades.OptionsView.ColumnAutoWidth = false;
            this.dgTrades.RowHeight = 15;
            this.dgTrades.ShowCustomizationForm += new System.EventHandler(this.dgTrades_ShowCustomizationForm);
            this.dgTrades.DoubleClick += new System.EventHandler(this.dgTrades_DoubleClick);
            this.dgTrades.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTrades_CustomDrawGroupRow);
            this.dgTrades.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTrades_DragObjectDrop);
            this.dgTrades.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTrades_ColumnWidthChanged);
            this.dgTrades.EndGrouping += new System.EventHandler(this.dgTrades_EndGrouping);
            this.dgTrades.HideCustomizationForm += new System.EventHandler(this.dgTrades_HideCustomizationForm);
            // 
            // frmTrades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1242, 966);
            this.Controls.Add(this.dtgTrade);
            this.Name = "frmTrades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "frmTrades";
            this.Load += new System.EventHandler(this.frmTrades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgTrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTrades)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtgTrade;
        private DevExpress.XtraGrid.Views.Grid.GridView dgTrades;
    }
}