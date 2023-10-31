namespace LiveBook
{
    partial class frmPositionSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPositionSummary));
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgTop10Desc = new MyXtraGrid.MyGridView();
            this.cmbView = new LiveBook.NestPortCombo();
            this.cmbStrategy = new LiveBook.NestBookCombo();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCopy = new System.Windows.Forms.Label();
            this.chkEquities = new System.Windows.Forms.CheckBox();
            this.chkMacro = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkExCurrency = new System.Windows.Forms.CheckBox();
            this.chkTop = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTop10Desc)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(6, 59);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgTop10Desc;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(515, 195);
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
            this.dgTop10Desc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgTop10Desc.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgTop10Desc.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgTop10Desc.OptionsSelection.InvertSelection = true;
            this.dgTop10Desc.OptionsSelection.MultiSelect = true;
            this.dgTop10Desc.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgTop10Desc.OptionsSelection.UseIndicatorForSelection = false;
            this.dgTop10Desc.OptionsView.ShowGroupPanel = false;
            this.dgTop10Desc.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgTop10Desc_CustomDrawCell);
            this.dgTop10Desc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgTop10Desc_MouseDown);
            this.dgTop10Desc.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgTop10Desc_MouseMove);
            this.dgTop10Desc.Click += new System.EventHandler(this.dgTop10Desc_Click);
            // 
            // cmbView
            // 
            this.cmbView.DisplayMember = "Port_Name";
            this.cmbView.FormattingEnabled = true;
            this.cmbView.IdPortType = 2;
            this.cmbView.includeAllPortsOption = false;
            this.cmbView.includeMHConsolOption = false;
            this.cmbView.Location = new System.Drawing.Point(6, 7);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(138, 21);
            this.cmbView.TabIndex = 25;
            this.cmbView.ValueMember = "Id_Portfolio";
            // 
            // cmbStrategy
            // 
            this.cmbStrategy.DisplayMember = "Book";
            this.cmbStrategy.FormattingEnabled = true;
            this.cmbStrategy.Id_Portfolio = 43;
            this.cmbStrategy.IncludeAllStrats = true;
            this.cmbStrategy.Location = new System.Drawing.Point(150, 7);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(122, 21);
            this.cmbStrategy.TabIndex = 26;
            this.cmbStrategy.ValueMember = "Id_Book";
            // 
            // cmbGroup
            // 
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(6, 32);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(99, 21);
            this.cmbGroup.TabIndex = 28;
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(490, 257);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 30;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // chkEquities
            // 
            this.chkEquities.AutoSize = true;
            this.chkEquities.Checked = true;
            this.chkEquities.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEquities.Location = new System.Drawing.Point(166, 34);
            this.chkEquities.Name = "chkEquities";
            this.chkEquities.Size = new System.Drawing.Size(63, 17);
            this.chkEquities.TabIndex = 31;
            this.chkEquities.Text = "Equities";
            this.chkEquities.UseVisualStyleBackColor = true;
            // 
            // chkMacro
            // 
            this.chkMacro.AutoSize = true;
            this.chkMacro.Location = new System.Drawing.Point(235, 34);
            this.chkMacro.Name = "chkMacro";
            this.chkMacro.Size = new System.Drawing.Size(56, 17);
            this.chkMacro.TabIndex = 32;
            this.chkMacro.Text = "Macro";
            this.chkMacro.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Include:";
            // 
            // chkExCurrency
            // 
            this.chkExCurrency.AutoSize = true;
            this.chkExCurrency.Location = new System.Drawing.Point(297, 34);
            this.chkExCurrency.Name = "chkExCurrency";
            this.chkExCurrency.Size = new System.Drawing.Size(83, 17);
            this.chkExCurrency.TabIndex = 34;
            this.chkExCurrency.Text = "Ex-Currency";
            this.chkExCurrency.UseVisualStyleBackColor = true;
            this.chkExCurrency.Visible = false;
            // 
            // chkTop
            // 
            this.chkTop.AutoSize = true;
            this.chkTop.Location = new System.Drawing.Point(397, 34);
            this.chkTop.Name = "chkTop";
            this.chkTop.Size = new System.Drawing.Size(98, 17);
            this.chkTop.TabIndex = 36;
            this.chkTop.Text = "Top/Bottom 20";
            this.chkTop.UseVisualStyleBackColor = true;
            this.chkTop.CheckedChanged += new System.EventHandler(this.chkTop_CheckedChanged);
            // 
            // frmPositionSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(525, 271);
            this.Controls.Add(this.chkTop);
            this.Controls.Add(this.chkExCurrency);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkMacro);
            this.Controls.Add(this.chkEquities);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.cmbView);
            this.Controls.Add(this.cmbStrategy);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.dtg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPositionSummary";
            this.Text = "Position Summary";
            this.Load += new System.EventHandler(this.frmTop10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTop10Desc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgTop10Desc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.CheckBox chkEquities;
        private System.Windows.Forms.CheckBox chkMacro;
        private System.Windows.Forms.Label label1;
        private LiveBook.NestBookCombo cmbStrategy;
        private NestPortCombo cmbView;
        private System.Windows.Forms.CheckBox chkExCurrency;
        private System.Windows.Forms.CheckBox chkTop;
    }
}