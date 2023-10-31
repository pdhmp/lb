namespace SGN
{
    partial class frmTop10Long3
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
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgTop10Desc = new MyXtraGrid.MyGridView();
            this.cmbView = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.chkEquityOnly = new System.Windows.Forms.CheckBox();
            this.chkOther = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTop10Desc)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.EmbeddedNavigator.Name = "";
            this.dtg.Location = new System.Drawing.Point(3, 53);
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.MainView = this.dgTop10Desc;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(347, 414);
            this.dtg.TabIndex = 24;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTop10Desc});
            this.dtg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtg_MouseUp);
            this.dtg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtg_MouseDown);
            // 
            // dgTop10Desc
            // 
            this.dgTop10Desc.GridControl = this.dtg;
            this.dgTop10Desc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTop10Desc.Name = "dgTop10Desc";
            this.dgTop10Desc.OptionsBehavior.Editable = false;
            this.dgTop10Desc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgTop10Desc.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgTop10Desc.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgTop10Desc.OptionsSelection.InvertSelection = true;
            this.dgTop10Desc.OptionsSelection.MultiSelect = true;
            this.dgTop10Desc.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgTop10Desc.OptionsSelection.UseIndicatorForSelection = false;
            this.dgTop10Desc.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.dgTop10Desc.OptionsView.ShowGroupPanel = false;
            this.dgTop10Desc.OptionsView.ShowIndicator = false;
            // 
            // cmbView
            // 
            this.cmbView.FormattingEnabled = true;
            this.cmbView.Location = new System.Drawing.Point(108, 3);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(152, 21);
            this.cmbView.TabIndex = 25;
            this.cmbView.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(3, 3);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(99, 21);
            this.cmbGroup.TabIndex = 28;
            // 
            // chkEquityOnly
            // 
            this.chkEquityOnly.AutoSize = true;
            this.chkEquityOnly.Checked = true;
            this.chkEquityOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEquityOnly.Location = new System.Drawing.Point(3, 30);
            this.chkEquityOnly.Name = "chkEquityOnly";
            this.chkEquityOnly.Size = new System.Drawing.Size(63, 17);
            this.chkEquityOnly.TabIndex = 30;
            this.chkEquityOnly.Text = "Equities";
            this.chkEquityOnly.UseVisualStyleBackColor = true;
            // 
            // chkOther
            // 
            this.chkOther.AutoSize = true;
            this.chkOther.Location = new System.Drawing.Point(72, 30);
            this.chkOther.Name = "chkOther";
            this.chkOther.Size = new System.Drawing.Size(86, 17);
            this.chkOther.TabIndex = 31;
            this.chkOther.Text = "Non-Equities";
            this.chkOther.UseVisualStyleBackColor = true;
            // 
            // frmTop10Long2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(354, 471);
            this.Controls.Add(this.chkOther);
            this.Controls.Add(this.chkEquityOnly);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.cmbView);
            this.Controls.Add(this.dtg);
            this.Name = "frmTop10Long2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Position Summary";
            this.Load += new System.EventHandler(this.frmTop10_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTop10_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTop10Desc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgTop10Desc;
        private System.Windows.Forms.ComboBox cmbView;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.CheckBox chkEquityOnly;
        private System.Windows.Forms.CheckBox chkOther;
    }
}