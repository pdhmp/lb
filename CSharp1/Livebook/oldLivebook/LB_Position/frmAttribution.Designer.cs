namespace LiveBook
{
    partial class frmAttribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttribution));
            this.label2 = new System.Windows.Forms.Label();
            this.dtgErrors = new MyXtraGrid.MyGridControl();
            this.dgErrors = new MyXtraGrid.MyGridView();
            this.myGridView1 = new MyXtraGrid.MyGridView();
            this.zgcPerformance = new ZedGraph.ZedGraphControl();
            this.dtgAttribution = new MyXtraGrid.MyGridControl();
            this.dgAttribution = new MyXtraGrid.MyGridView();
            this.myGridView2 = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpHistDate = new System.Windows.Forms.DateTimePicker();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.cmbPortfolio = new LiveBook.NestPortCombo();
            this.chkBooks = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radYear = new System.Windows.Forms.RadioButton();
            this.radEoMonth = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radCumulative = new System.Windows.Forms.RadioButton();
            this.radNormal = new System.Windows.Forms.RadioButton();
            this.lblCopy = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkAdjust = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radMonthly = new System.Windows.Forms.RadioButton();
            this.radDaily = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAttribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-246, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Largest Errors on the period selected:";
            // 
            // dtgErrors
            // 
            this.dtgErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgErrors.Location = new System.Drawing.Point(3, 3);
            this.dtgErrors.LookAndFeel.SkinName = "Blue";
            this.dtgErrors.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgErrors.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgErrors.MainView = this.dgErrors;
            this.dtgErrors.Name = "dtgErrors";
            this.dtgErrors.Size = new System.Drawing.Size(306, 203);
            this.dtgErrors.TabIndex = 44;
            this.dtgErrors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgErrors,
            this.myGridView1});
            // 
            // dgErrors
            // 
            this.dgErrors.GridControl = this.dtgErrors;
            this.dgErrors.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgErrors.Name = "dgErrors";
            this.dgErrors.OptionsBehavior.Editable = false;
            this.dgErrors.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgErrors.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgErrors.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgErrors.OptionsSelection.InvertSelection = true;
            this.dgErrors.OptionsSelection.MultiSelect = true;
            this.dgErrors.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgErrors.OptionsSelection.UseIndicatorForSelection = false;
            this.dgErrors.OptionsView.ShowGroupPanel = false;
            this.dgErrors.OptionsView.ShowIndicator = false;
            this.dgErrors.RowHeight = 10;
            // 
            // myGridView1
            // 
            this.myGridView1.GridControl = this.dtgErrors;
            this.myGridView1.Name = "myGridView1";
            // 
            // zgcPerformance
            // 
            this.zgcPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcPerformance.Location = new System.Drawing.Point(315, 3);
            this.zgcPerformance.Name = "zgcPerformance";
            this.zgcPerformance.ScrollGrace = 0D;
            this.zgcPerformance.ScrollMaxX = 0D;
            this.zgcPerformance.ScrollMaxY = 0D;
            this.zgcPerformance.ScrollMaxY2 = 0D;
            this.zgcPerformance.ScrollMinX = 0D;
            this.zgcPerformance.ScrollMinY = 0D;
            this.zgcPerformance.ScrollMinY2 = 0D;
            this.zgcPerformance.Size = new System.Drawing.Size(1039, 203);
            this.zgcPerformance.TabIndex = 35;
            // 
            // dtgAttribution
            // 
            this.dtgAttribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgAttribution.Location = new System.Drawing.Point(3, 34);
            this.dtgAttribution.LookAndFeel.SkinName = "Blue";
            this.dtgAttribution.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgAttribution.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgAttribution.MainView = this.dgAttribution;
            this.dtgAttribution.Name = "dtgAttribution";
            this.dtgAttribution.Size = new System.Drawing.Size(1351, 468);
            this.dtgAttribution.TabIndex = 25;
            this.dtgAttribution.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgAttribution,
            this.myGridView2});
            // 
            // dgAttribution
            // 
            this.dgAttribution.GridControl = this.dtgAttribution;
            this.dgAttribution.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgAttribution.Name = "dgAttribution";
            this.dgAttribution.OptionsBehavior.Editable = false;
            this.dgAttribution.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgAttribution.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgAttribution.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgAttribution.OptionsSelection.InvertSelection = true;
            this.dgAttribution.OptionsSelection.MultiSelect = true;
            this.dgAttribution.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgAttribution.OptionsSelection.UseIndicatorForSelection = false;
            this.dgAttribution.OptionsView.ColumnAutoWidth = false;
            this.dgAttribution.OptionsView.ShowIndicator = false;
            this.dgAttribution.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgAttribution_DragObjectDrop);
            this.dgAttribution.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgAttribution_CustomDrawCell);
            this.dgAttribution.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgAttribution_CustomDrawGroupRow);
            this.dgAttribution.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgAttribution_GroupLevelStyle);
            this.dgAttribution.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgAttribution_CustomSummaryCalculate);
            this.dgAttribution.ColumnFilterChanged += new System.EventHandler(this.dgAttribution_ColumnFilterChanged);
            this.dgAttribution.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgAttribution_MouseUp);
            // 
            // myGridView2
            // 
            this.myGridView2.GridControl = this.dtgAttribution;
            this.myGridView2.Name = "myGridView2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(918, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Initial Date:";
            // 
            // dtpHistDate
            // 
            this.dtpHistDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpHistDate.CalendarFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHistDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHistDate.Location = new System.Drawing.Point(992, 8);
            this.dtpHistDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtpHistDate.MinDate = new System.DateTime(1920, 1, 1, 0, 0, 0, 0);
            this.dtpHistDate.Name = "dtpHistDate";
            this.dtpHistDate.Size = new System.Drawing.Size(93, 20);
            this.dtpHistDate.TabIndex = 32;
            this.dtpHistDate.CloseUp += new System.EventHandler(this.dtpHistDate_CloseUp);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(183, 10);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse.TabIndex = 31;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            this.cmdCollapse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdCollapse_MouseDown);
            // 
            // cmdExpand
            // 
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(118, 10);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand.TabIndex = 30;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            this.cmdExpand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdExpand_MouseDown);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.DisplayMember = "Port_Name";
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.IdPortType = 2;
            this.cmbPortfolio.includeAllPortsOption = false;
            this.cmbPortfolio.includeMHConsolOption = false;
            this.cmbPortfolio.Location = new System.Drawing.Point(1091, 7);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 40;
            this.cmbPortfolio.ValueMember = "Id_Portfolio";
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // chkBooks
            // 
            this.chkBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBooks.AutoSize = true;
            this.chkBooks.BackColor = System.Drawing.Color.White;
            this.chkBooks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.chkBooks.Location = new System.Drawing.Point(1249, 10);
            this.chkBooks.Name = "chkBooks";
            this.chkBooks.Size = new System.Drawing.Size(96, 17);
            this.chkBooks.TabIndex = 43;
            this.chkBooks.Text = "Book Gross-up";
            this.chkBooks.UseVisualStyleBackColor = false;
            this.chkBooks.CheckedChanged += new System.EventHandler(this.chkBooks_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.radYear);
            this.groupBox1.Controls.Add(this.radEoMonth);
            this.groupBox1.Controls.Add(this.radAll);
            this.groupBox1.Location = new System.Drawing.Point(705, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(207, 30);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // radYear
            // 
            this.radYear.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radYear.AutoSize = true;
            this.radYear.BackColor = System.Drawing.Color.White;
            this.radYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radYear.Location = new System.Drawing.Point(80, 9);
            this.radYear.Name = "radYear";
            this.radYear.Size = new System.Drawing.Size(60, 17);
            this.radYear.TabIndex = 46;
            this.radYear.TabStop = true;
            this.radYear.Tag = "date";
            this.radYear.Text = "EoYear";
            this.radYear.UseVisualStyleBackColor = false;
            this.radYear.CheckedChanged += new System.EventHandler(this.radYear_CheckedChanged);
            // 
            // radEoMonth
            // 
            this.radEoMonth.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radEoMonth.AutoSize = true;
            this.radEoMonth.BackColor = System.Drawing.Color.White;
            this.radEoMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radEoMonth.Location = new System.Drawing.Point(6, 10);
            this.radEoMonth.Name = "radEoMonth";
            this.radEoMonth.Size = new System.Drawing.Size(68, 17);
            this.radEoMonth.TabIndex = 44;
            this.radEoMonth.TabStop = true;
            this.radEoMonth.Tag = "date";
            this.radEoMonth.Text = "EoMonth";
            this.radEoMonth.UseVisualStyleBackColor = false;
            this.radEoMonth.CheckedChanged += new System.EventHandler(this.radMonth_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radAll.AutoSize = true;
            this.radAll.BackColor = System.Drawing.Color.White;
            this.radAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radAll.Location = new System.Drawing.Point(146, 10);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(55, 17);
            this.radAll.TabIndex = 45;
            this.radAll.TabStop = true;
            this.radAll.Text = "Today";
            this.radAll.UseVisualStyleBackColor = false;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.radCumulative);
            this.groupBox2.Controls.Add(this.radNormal);
            this.groupBox2.Location = new System.Drawing.Point(544, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(155, 30);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            // 
            // radCumulative
            // 
            this.radCumulative.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radCumulative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radCumulative.AutoSize = true;
            this.radCumulative.BackColor = System.Drawing.Color.White;
            this.radCumulative.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radCumulative.Location = new System.Drawing.Point(7, 10);
            this.radCumulative.Name = "radCumulative";
            this.radCumulative.Size = new System.Drawing.Size(77, 17);
            this.radCumulative.TabIndex = 37;
            this.radCumulative.TabStop = true;
            this.radCumulative.Tag = "type";
            this.radCumulative.Text = "Cumulative";
            this.radCumulative.UseVisualStyleBackColor = false;
            this.radCumulative.CheckedChanged += new System.EventHandler(this.radCumulative_CheckedChanged);
            // 
            // radNormal
            // 
            this.radNormal.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radNormal.AutoSize = true;
            this.radNormal.BackColor = System.Drawing.Color.White;
            this.radNormal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radNormal.Location = new System.Drawing.Point(90, 9);
            this.radNormal.Name = "radNormal";
            this.radNormal.Size = new System.Drawing.Size(58, 17);
            this.radNormal.TabIndex = 38;
            this.radNormal.TabStop = true;
            this.radNormal.Tag = "type";
            this.radNormal.Text = "Normal";
            this.radNormal.UseVisualStyleBackColor = false;
            this.radNormal.CheckedChanged += new System.EventHandler(this.radNormal_CheckedChanged);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(1323, 1);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 46;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chkAdjust);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.cmdCollapse);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.cmdExpand);
            this.splitContainer1.Panel1.Controls.Add(this.chkBooks);
            this.splitContainer1.Panel1.Controls.Add(this.cmbPortfolio);
            this.splitContainer1.Panel1.Controls.Add(this.dtpHistDate);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dtgAttribution);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblCopy);
            this.splitContainer1.Panel2.Controls.Add(this.zgcPerformance);
            this.splitContainer1.Panel2.Controls.Add(this.dtgErrors);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1357, 718);
            this.splitContainer1.SplitterDistance = 505;
            this.splitContainer1.TabIndex = 45;
            // 
            // chkAdjust
            // 
            this.chkAdjust.AutoSize = true;
            this.chkAdjust.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.chkAdjust.Location = new System.Drawing.Point(315, 10);
            this.chkAdjust.Name = "chkAdjust";
            this.chkAdjust.Size = new System.Drawing.Size(82, 17);
            this.chkAdjust.TabIndex = 49;
            this.chkAdjust.Text = "Cash Adjust";
            this.chkAdjust.UseVisualStyleBackColor = true;
            this.chkAdjust.CheckedChanged += new System.EventHandler(this.chkAdjust_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.radMonthly);
            this.groupBox3.Controls.Add(this.radDaily);
            this.groupBox3.Location = new System.Drawing.Point(407, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(131, 30);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            // 
            // radMonthly
            // 
            this.radMonthly.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radMonthly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radMonthly.AutoSize = true;
            this.radMonthly.BackColor = System.Drawing.Color.White;
            this.radMonthly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radMonthly.Location = new System.Drawing.Point(8, 9);
            this.radMonthly.Name = "radMonthly";
            this.radMonthly.Size = new System.Drawing.Size(62, 17);
            this.radMonthly.TabIndex = 37;
            this.radMonthly.TabStop = true;
            this.radMonthly.Tag = "type";
            this.radMonthly.Text = "Monthly";
            this.radMonthly.UseVisualStyleBackColor = false;
            this.radMonthly.CheckedChanged += new System.EventHandler(this.radMonthly_CheckedChanged);
            // 
            // radDaily
            // 
            this.radDaily.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDaily.AutoSize = true;
            this.radDaily.BackColor = System.Drawing.Color.White;
            this.radDaily.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radDaily.Location = new System.Drawing.Point(76, 9);
            this.radDaily.Name = "radDaily";
            this.radDaily.Size = new System.Drawing.Size(48, 17);
            this.radDaily.TabIndex = 38;
            this.radDaily.TabStop = true;
            this.radDaily.Tag = "type";
            this.radDaily.Text = "Daily";
            this.radDaily.UseVisualStyleBackColor = false;
            this.radDaily.CheckedChanged += new System.EventHandler(this.radDaily_CheckedChanged);
            // 
            // frmAttribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1381, 742);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAttribution";
            this.Text = "Attribution";
            this.Load += new System.EventHandler(this.frmAttribution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAttribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAttribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private MyXtraGrid.MyGridControl dtgErrors;
        private MyXtraGrid.MyGridView dgErrors;
        private MyXtraGrid.MyGridView myGridView1;
        private ZedGraph.ZedGraphControl zgcPerformance;
        private MyXtraGrid.MyGridControl dtgAttribution;
        private MyXtraGrid.MyGridView dgAttribution;
        private MyXtraGrid.MyGridView myGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHistDate;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.Button cmdExpand;
        private NestPortCombo cmbPortfolio;
        private System.Windows.Forms.CheckBox chkBooks;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radYear;
        private System.Windows.Forms.RadioButton radEoMonth;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radCumulative;
        private System.Windows.Forms.RadioButton radNormal;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radMonthly;
        private System.Windows.Forms.RadioButton radDaily;
        private System.Windows.Forms.CheckBox chkAdjust;


    }
}