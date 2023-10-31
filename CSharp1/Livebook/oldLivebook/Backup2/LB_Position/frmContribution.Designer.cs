namespace SGN
{
    partial class frmContribution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContribution));
            this.dtgContribution = new MyXtraGrid.MyGridControl();
            this.dgContribution = new MyXtraGrid.MyGridView();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.dtpHistDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.zgcPerformance = new ZedGraph.ZedGraphControl();
            this.radDaily = new System.Windows.Forms.RadioButton();
            this.radCumulative = new System.Windows.Forms.RadioButton();
            this.lblCopy = new System.Windows.Forms.Label();
            this.cmbPortfolio = new SGN.NestPortCombo();
            this.label2 = new System.Windows.Forms.Label();
            this.chkBooks = new System.Windows.Forms.CheckBox();
            this.dtgErrors = new MyXtraGrid.MyGridControl();
            this.dgErrors = new MyXtraGrid.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgContribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgContribution
            // 
            this.dtgContribution.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgContribution.Location = new System.Drawing.Point(12, 12);
            this.dtgContribution.LookAndFeel.SkinName = "Blue";
            this.dtgContribution.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgContribution.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgContribution.MainView = this.dgContribution;
            this.dtgContribution.Name = "dtgContribution";
            this.dtgContribution.Size = new System.Drawing.Size(965, 451);
            this.dtgContribution.TabIndex = 25;
            this.dtgContribution.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgContribution});
            // 
            // dgContribution
            // 
            this.dgContribution.GridControl = this.dtgContribution;
            this.dgContribution.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgContribution.Name = "dgContribution";
            this.dgContribution.OptionsBehavior.Editable = false;
            this.dgContribution.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgContribution.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgContribution.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgContribution.OptionsSelection.InvertSelection = true;
            this.dgContribution.OptionsSelection.MultiSelect = true;
            this.dgContribution.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgContribution.OptionsSelection.UseIndicatorForSelection = false;
            this.dgContribution.OptionsView.ColumnAutoWidth = false;
            this.dgContribution.OptionsView.ShowIndicator = false;
            this.dgContribution.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgAttribution_CustomDrawGroupRow);
            this.dgContribution.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgAttribution_GroupLevelStyle);
            this.dgContribution.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgAttribution_CustomDrawCell);
            this.dgContribution.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgAttribution_MouseUp);
            this.dgContribution.CustomSummaryCalculate += new DevExpress.Data.CustomSummaryEventHandler(this.dgAttribution_CustomSummaryCalculate);
            // 
            // cmdExpand
            // 
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(214, 17);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand.TabIndex = 30;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            this.cmdExpand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdExpand_MouseDown);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(279, 17);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse.TabIndex = 31;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            this.cmdCollapse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdCollapse_MouseDown);
            // 
            // dtpHistDate
            // 
            this.dtpHistDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpHistDate.CalendarFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHistDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHistDate.Location = new System.Drawing.Point(608, 18);
            this.dtpHistDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtpHistDate.MinDate = new System.DateTime(1920, 1, 1, 0, 0, 0, 0);
            this.dtpHistDate.Name = "dtpHistDate";
            this.dtpHistDate.Size = new System.Drawing.Size(93, 20);
            this.dtpHistDate.TabIndex = 32;
            this.dtpHistDate.CloseUp += new System.EventHandler(this.dtpHistDate_CloseUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(537, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Initial Date:";
            // 
            // zgcPerformance
            // 
            this.zgcPerformance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcPerformance.Location = new System.Drawing.Point(279, 482);
            this.zgcPerformance.Name = "zgcPerformance";
            this.zgcPerformance.ScrollGrace = 0;
            this.zgcPerformance.ScrollMaxX = 0;
            this.zgcPerformance.ScrollMaxY = 0;
            this.zgcPerformance.ScrollMaxY2 = 0;
            this.zgcPerformance.ScrollMinX = 0;
            this.zgcPerformance.ScrollMinY = 0;
            this.zgcPerformance.ScrollMinY2 = 0;
            this.zgcPerformance.Size = new System.Drawing.Size(698, 164);
            this.zgcPerformance.TabIndex = 35;
            // 
            // radDaily
            // 
            this.radDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDaily.AutoSize = true;
            this.radDaily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radDaily.ForeColor = System.Drawing.Color.White;
            this.radDaily.Location = new System.Drawing.Point(430, 20);
            this.radDaily.Name = "radDaily";
            this.radDaily.Size = new System.Drawing.Size(48, 17);
            this.radDaily.TabIndex = 38;
            this.radDaily.TabStop = true;
            this.radDaily.Text = "Daily";
            this.radDaily.UseVisualStyleBackColor = false;
            this.radDaily.CheckedChanged += new System.EventHandler(this.radDaily_CheckedChanged);
            // 
            // radCumulative
            // 
            this.radCumulative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radCumulative.AutoSize = true;
            this.radCumulative.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radCumulative.ForeColor = System.Drawing.Color.White;
            this.radCumulative.Location = new System.Drawing.Point(347, 20);
            this.radCumulative.Name = "radCumulative";
            this.radCumulative.Size = new System.Drawing.Size(77, 17);
            this.radCumulative.TabIndex = 37;
            this.radCumulative.TabStop = true;
            this.radCumulative.Text = "Cumulative";
            this.radCumulative.UseVisualStyleBackColor = false;
            this.radCumulative.CheckedChanged += new System.EventHandler(this.radCumulative_CheckedChanged);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(946, 466);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 39;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.DisplayMember = "Port_Name";
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(707, 18);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 40;
            this.cmbPortfolio.ValueMember = "Id_Portfolio";
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 466);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Largest Errors on the period selected:";
            // 
            // chkBooks
            // 
            this.chkBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBooks.AutoSize = true;
            this.chkBooks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.chkBooks.ForeColor = System.Drawing.Color.White;
            this.chkBooks.Location = new System.Drawing.Point(866, 20);
            this.chkBooks.Name = "chkBooks";
            this.chkBooks.Size = new System.Drawing.Size(96, 17);
            this.chkBooks.TabIndex = 43;
            this.chkBooks.Text = "Book Gross-up";
            this.chkBooks.UseVisualStyleBackColor = false;
            this.chkBooks.CheckedChanged += new System.EventHandler(this.chkBooks_CheckedChanged);
            // 
            // dtgErrors
            // 
            this.dtgErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgErrors.Location = new System.Drawing.Point(15, 482);
            this.dtgErrors.LookAndFeel.SkinName = "Blue";
            this.dtgErrors.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgErrors.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgErrors.MainView = this.dgErrors;
            this.dtgErrors.Name = "dtgErrors";
            this.dtgErrors.Size = new System.Drawing.Size(258, 164);
            this.dtgErrors.TabIndex = 44;
            this.dtgErrors.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgErrors});
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
            // frmContribution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(989, 658);
            this.Controls.Add(this.dtgErrors);
            this.Controls.Add(this.chkBooks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.radDaily);
            this.Controls.Add(this.radCumulative);
            this.Controls.Add(this.zgcPerformance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpHistDate);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.dtgContribution);
            this.Controls.Add(this.lblCopy);
            this.Name = "frmContribution";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Attribution";
            this.Load += new System.EventHandler(this.frmContribution_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgContribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgErrors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtgContribution;
        private MyXtraGrid.MyGridView dgContribution;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.DateTimePicker dtpHistDate;
        private System.Windows.Forms.Label label1;
        private ZedGraph.ZedGraphControl zgcPerformance;
        private System.Windows.Forms.RadioButton radDaily;
        private System.Windows.Forms.RadioButton radCumulative;
        private System.Windows.Forms.Label lblCopy;
        private NestPortCombo cmbPortfolio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkBooks;
        private MyXtraGrid.MyGridControl dtgErrors;
        private MyXtraGrid.MyGridView dgErrors;

    }
}