namespace LiveTrade2
{
    partial class frmOptionChain
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtgOptionQuotes = new NCustomControls.MyGridControl();
            this.dgOptionQuotes = new NCustomControls.MyGridView();
            this.chkMainOptions = new System.Windows.Forms.CheckBox();
            this.tmrUpdateUndLast = new System.Windows.Forms.Timer(this.components);
            this.tmrBestFit = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOptionQuotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOptionQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgOptionQuotes
            // 
            this.dtgOptionQuotes.AllowDrop = true;
            this.dtgOptionQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gridLevelNode1.RelationName = "Level1";
            this.dtgOptionQuotes.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.dtgOptionQuotes.Location = new System.Drawing.Point(0, 24);
            this.dtgOptionQuotes.LookAndFeel.SkinName = "Blue";
            this.dtgOptionQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgOptionQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOptionQuotes.MainView = this.dgOptionQuotes;
            this.dtgOptionQuotes.Name = "dtgOptionQuotes";
            this.dtgOptionQuotes.Size = new System.Drawing.Size(694, 444);
            this.dtgOptionQuotes.TabIndex = 26;
            this.dtgOptionQuotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOptionQuotes});
            this.dtgOptionQuotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            this.dtgOptionQuotes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            // 
            // dgOptionQuotes
            // 
            this.dgOptionQuotes.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgOptionQuotes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgOptionQuotes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgOptionQuotes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgOptionQuotes.GridControl = this.dtgOptionQuotes;
            this.dgOptionQuotes.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgOptionQuotes.Name = "dgOptionQuotes";
            this.dgOptionQuotes.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgOptionQuotes.OptionsBehavior.Editable = false;
            this.dgOptionQuotes.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgOptionQuotes.OptionsSelection.UseIndicatorForSelection = false;
            this.dgOptionQuotes.OptionsView.ColumnAutoWidth = false;
            this.dgOptionQuotes.OptionsView.ShowGroupPanel = false;
            this.dgOptionQuotes.OptionsView.ShowIndicator = false;
            this.dgOptionQuotes.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgOptionQuotes.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgQuotes_CustomDrawGroupRow);
            this.dgOptionQuotes.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgQuotes_GroupLevelStyle);
            this.dgOptionQuotes.EndSorting += new System.EventHandler(this.dgQuotes_EndSorting);
            this.dgOptionQuotes.DoubleClick += new System.EventHandler(this.dgOptionQuotes_DoubleClick);
            // 
            // chkMainOptions
            // 
            this.chkMainOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMainOptions.AutoSize = true;
            this.chkMainOptions.Checked = true;
            this.chkMainOptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMainOptions.Location = new System.Drawing.Point(633, 1);
            this.chkMainOptions.Name = "chkMainOptions";
            this.chkMainOptions.Size = new System.Drawing.Size(49, 17);
            this.chkMainOptions.TabIndex = 62;
            this.chkMainOptions.Text = "Main";
            this.chkMainOptions.UseVisualStyleBackColor = true;
            this.chkMainOptions.CheckedChanged += new System.EventHandler(this.chkMainOptions_CheckedChanged);
            // 
            // tmrUpdateUndLast
            // 
            this.tmrUpdateUndLast.Interval = 1000;
            this.tmrUpdateUndLast.Tick += new System.EventHandler(this.tmrUpdateUndLast_Tick);
            // 
            // tmrBestFit
            // 
            this.tmrBestFit.Interval = 2000;
            this.tmrBestFit.Tick += new System.EventHandler(this.tmrBestFit_Tick);
            // 
            // frmOptionChain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 468);
            this.Controls.Add(this.chkMainOptions);
            this.Controls.Add(this.dtgOptionQuotes);
            this.Name = "frmOptionChain";
            this.Text = "Option Chain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOptionChain_FormClosing);
            this.Load += new System.EventHandler(this.frmOptionChain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOptionQuotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOptionQuotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private NCustomControls.MyGridView dgOptionQuotes;
        private NCustomControls.MyGridControl dtgOptionQuotes;
        private System.Windows.Forms.CheckBox chkMainOptions;
        private System.Windows.Forms.Timer tmrUpdateUndLast;
        private System.Windows.Forms.Timer tmrBestFit;
    }
}

