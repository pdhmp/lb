namespace SGN
{
    partial class frmPositions
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
            this.dtg = new DevExpress.XtraGrid.GridControl();
            this.dgPositions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.cmdTxt = new System.Windows.Forms.Button();
            this.cmdExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.EmbeddedNavigator.Name = "";
            this.dtg.Location = new System.Drawing.Point(3, 67);
            this.dtg.MainView = this.dgPositions;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(1227, 887);
            this.dtg.TabIndex = 14;
            this.dtg.TabStop = false;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgPositions});
            // 
            // dgPositions
            // 
            this.dgPositions.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgPositions.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.dgPositions.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.dgPositions.Appearance.DetailTip.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.DetailTip.Options.UseFont = true;
            this.dgPositions.Appearance.Empty.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.Empty.Options.UseFont = true;
            this.dgPositions.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.EvenRow.Options.UseFont = true;
            this.dgPositions.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FilterCloseButton.Options.UseFont = true;
            this.dgPositions.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FilterPanel.Options.UseFont = true;
            this.dgPositions.Appearance.FixedLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FixedLine.Options.UseFont = true;
            this.dgPositions.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FocusedCell.Options.UseFont = true;
            this.dgPositions.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FocusedRow.Options.UseFont = true;
            this.dgPositions.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FooterPanel.Options.UseFont = true;
            this.dgPositions.Appearance.GroupButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupButton.Options.UseFont = true;
            this.dgPositions.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupFooter.Options.UseFont = true;
            this.dgPositions.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupPanel.Options.UseFont = true;
            this.dgPositions.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupRow.Options.UseFont = true;
            this.dgPositions.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgPositions.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.HideSelectionRow.Options.UseFont = true;
            this.dgPositions.Appearance.HorzLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.HorzLine.Options.UseFont = true;
            this.dgPositions.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.OddRow.Options.UseFont = true;
            this.dgPositions.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.Preview.Options.UseFont = true;
            this.dgPositions.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.Row.Options.UseFont = true;
            this.dgPositions.Appearance.RowSeparator.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.RowSeparator.Options.UseFont = true;
            this.dgPositions.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.SelectedRow.Options.UseFont = true;
            this.dgPositions.Appearance.TopNewRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.TopNewRow.Options.UseFont = true;
            this.dgPositions.Appearance.VertLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.VertLine.Options.UseFont = true;
            this.dgPositions.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.EvenRow.Options.UseFont = true;
            this.dgPositions.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.dgPositions.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.dgPositions.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.dgPositions.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.GroupRow.Options.UseFont = true;
            this.dgPositions.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.dgPositions.AppearancePrint.Lines.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.Lines.Options.UseFont = true;
            this.dgPositions.AppearancePrint.OddRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.OddRow.Options.UseFont = true;
            this.dgPositions.AppearancePrint.Preview.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.Preview.Options.UseFont = true;
            this.dgPositions.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.Row.Options.UseFont = true;
            this.dgPositions.ColumnPanelRowHeight = 15;
            this.dgPositions.GridControl = this.dtg;
            this.dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgPositions.GroupRowHeight = 10;
            this.dgPositions.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgPositions.Name = "dgPositions";
            this.dgPositions.OptionsBehavior.Editable = false;
            this.dgPositions.OptionsView.ColumnAutoWidth = false;
            this.dgPositions.RowHeight = 15;
            this.dgPositions.ShowCustomizationForm += new System.EventHandler(this.dgPositions_ShowCustomizationForm);
            this.dgPositions.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgPositions_CustomDrawGroupRow);
            this.dgPositions.Click += new System.EventHandler(this.dgPositions_Click);
            this.dgPositions.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.dgPositions_CalcRowHeight);
            this.dgPositions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgPositions_DragObjectDrop);
            this.dgPositions.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgPositions_ColumnWidthChanged);
            this.dgPositions.EndGrouping += new System.EventHandler(this.dgPositions_EndGrouping);
            this.dgPositions.HideCustomizationForm += new System.EventHandler(this.dgPositions_HideCustomizationForm);
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(186, 19);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(89, 19);
            this.cmdrefresh.TabIndex = 17;
            this.cmdrefresh.Text = "Refresh";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbChoosePortfolio);
            this.groupBox1.Controls.Add(this.cmdrefresh);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 49);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose Portfolio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(9, 19);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 18;
            this.cmbChoosePortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedIndexChanged);
            this.cmbChoosePortfolio.SelectedValueChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.cmdExpand);
            this.groupBox9.Controls.Add(this.cmdCollapse);
            this.groupBox9.Location = new System.Drawing.Point(444, 12);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(127, 49);
            this.groupBox9.TabIndex = 22;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "All Groups";
            // 
            // cmdExpand
            // 
            this.cmdExpand.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(5, 20);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(55, 19);
            this.cmdExpand.TabIndex = 16;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = true;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(66, 20);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(55, 19);
            this.cmdCollapse.TabIndex = 17;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = true;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.cmdTxt);
            this.groupBox10.Controls.Add(this.cmdExcel);
            this.groupBox10.Location = new System.Drawing.Point(577, 12);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(140, 49);
            this.groupBox10.TabIndex = 21;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Export";
            // 
            // cmdTxt
            // 
            this.cmdTxt.Location = new System.Drawing.Point(71, 20);
            this.cmdTxt.Name = "cmdTxt";
            this.cmdTxt.Size = new System.Drawing.Size(59, 20);
            this.cmdTxt.TabIndex = 16;
            this.cmdTxt.Text = "Txt";
            this.cmdTxt.UseVisualStyleBackColor = true;
            this.cmdTxt.Click += new System.EventHandler(this.cmdTxt_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Location = new System.Drawing.Point(6, 20);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(59, 20);
            this.cmdExcel.TabIndex = 15;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = true;
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // frmPositions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 966);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtg);
            this.Name = "frmPositions";
            this.Text = "Positions";
            this.Load += new System.EventHandler(this.frmPositions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl dtg;
        private DevExpress.XtraGrid.Views.Grid.GridView dgPositions;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button cmdTxt;
        private System.Windows.Forms.Button cmdExcel;
    }
}