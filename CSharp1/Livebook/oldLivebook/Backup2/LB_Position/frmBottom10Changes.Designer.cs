namespace SGN
{
    partial class frmBottom10Changes
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblCopy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTop10Desc)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtg.Location = new System.Drawing.Point(3, 30);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgTop10Desc;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(332, 178);
            this.dtg.TabIndex = 24;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTop10Desc});
            // 
            // dgTop10Desc
            // 
            this.dgTop10Desc.GridControl = this.dtg;
            this.dgTop10Desc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTop10Desc.Name = "dgTop10Desc";
            this.dgTop10Desc.OptionsBehavior.Editable = false;
            this.dgTop10Desc.OptionsCustomization.AllowColumnMoving = false;
            this.dgTop10Desc.OptionsCustomization.AllowColumnResizing = false;
            this.dgTop10Desc.OptionsCustomization.AllowFilter = false;
            this.dgTop10Desc.OptionsCustomization.AllowGroup = false;
            this.dgTop10Desc.OptionsCustomization.AllowSort = false;
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
            this.cmbView.Location = new System.Drawing.Point(183, 6);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(152, 21);
            this.cmbView.TabIndex = 25;
            this.cmbView.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(97, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.Text = "ExCurrency";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(304, 195);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 28;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // frmBottom10Changes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(339, 209);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cmbView);
            this.Controls.Add(this.dtg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBottom10Changes";
            this.Text = "Bottom 10 Changes";
            this.Load += new System.EventHandler(this.frmTop10_Load);
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
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblCopy;
    }
}