namespace LiveTrade2
{
    partial class frmAuction
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtgAucQuotes = new NCustomControls.MyGridControl();
            this.dgAucQuotes = new NCustomControls.MyGridView();
            this.chkLiquid = new System.Windows.Forms.CheckBox();
            this.chkNest = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAucQuotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAucQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgAucQuotes
            // 
            this.dtgAucQuotes.AllowDrop = true;
            this.dtgAucQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgAucQuotes.Location = new System.Drawing.Point(0, 30);
            this.dtgAucQuotes.LookAndFeel.SkinName = "Blue";
            this.dtgAucQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgAucQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgAucQuotes.MainView = this.dgAucQuotes;
            this.dtgAucQuotes.Name = "dtgAucQuotes";
            this.dtgAucQuotes.Size = new System.Drawing.Size(933, 385);
            this.dtgAucQuotes.TabIndex = 26;
            this.dtgAucQuotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgAucQuotes});
            this.dtgAucQuotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            this.dtgAucQuotes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            // 
            // dgAucQuotes
            // 
            this.dgAucQuotes.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgAucQuotes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgAucQuotes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgAucQuotes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgAucQuotes.GridControl = this.dtgAucQuotes;
            this.dgAucQuotes.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgAucQuotes.Name = "dgAucQuotes";
            this.dgAucQuotes.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgAucQuotes.OptionsBehavior.Editable = false;
            this.dgAucQuotes.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgAucQuotes.OptionsSelection.UseIndicatorForSelection = false;
            this.dgAucQuotes.OptionsView.ColumnAutoWidth = false;
            this.dgAucQuotes.OptionsView.ShowGroupPanel = false;
            this.dgAucQuotes.OptionsView.ShowIndicator = false;
            this.dgAucQuotes.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgAucQuotes.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgQuotes_CustomDrawGroupRow);
            this.dgAucQuotes.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgQuotes_GroupLevelStyle);
            this.dgAucQuotes.EndSorting += new System.EventHandler(this.dgQuotes_EndSorting);
            this.dgAucQuotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgAucQuotes_MouseDown);
            this.dgAucQuotes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgAucQuotes_MouseUp);
            this.dgAucQuotes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgAucQuotes_MouseMove);
            this.dgAucQuotes.DoubleClick += new System.EventHandler(this.dgQuotes_DoubleClick);
            // 
            // chkLiquid
            // 
            this.chkLiquid.AutoSize = true;
            this.chkLiquid.Location = new System.Drawing.Point(646, 7);
            this.chkLiquid.Name = "chkLiquid";
            this.chkLiquid.Size = new System.Drawing.Size(54, 17);
            this.chkLiquid.TabIndex = 27;
            this.chkLiquid.Text = "Liquid";
            this.chkLiquid.UseVisualStyleBackColor = true;
            this.chkLiquid.CheckedChanged += new System.EventHandler(this.FilterBoxes_CheckedChanged);
            // 
            // chkNest
            // 
            this.chkNest.AutoSize = true;
            this.chkNest.Location = new System.Drawing.Point(706, 7);
            this.chkNest.Name = "chkNest";
            this.chkNest.Size = new System.Drawing.Size(105, 17);
            this.chkNest.TabIndex = 28;
            this.chkNest.Text = "Current Positions";
            this.chkNest.UseVisualStyleBackColor = true;
            this.chkNest.CheckedChanged += new System.EventHandler(this.FilterBoxes_CheckedChanged);
            // 
            // frmAuction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 416);
            this.Controls.Add(this.chkNest);
            this.Controls.Add(this.chkLiquid);
            this.Controls.Add(this.dtgAucQuotes);
            this.Name = "frmAuction";
            this.Text = "Auction Viewer";
            this.Load += new System.EventHandler(this.frmWatchList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAucQuotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAucQuotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private NCustomControls.MyGridView dgAucQuotes;
        private NCustomControls.MyGridControl dtgAucQuotes;
        private System.Windows.Forms.CheckBox chkLiquid;
        private System.Windows.Forms.CheckBox chkNest;
    }
}

