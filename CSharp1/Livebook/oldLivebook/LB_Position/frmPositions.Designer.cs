namespace LiveBook
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPositions));
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrRefreshPos = new System.Windows.Forms.Timer(this.components);
            this.dtgPositions = new MyXtraGrid.MyGridControl();
            this.dgPositions = new MyXtraGrid.MyGridView();
            this.dtpHistDate = new System.Windows.Forms.DateTimePicker();
            this.cmbCustomView = new System.Windows.Forms.ComboBox();
            this.chkLinkAllBoxes = new System.Windows.Forms.CheckBox();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.radHistoric = new System.Windows.Forms.RadioButton();
            this.radRealTime = new System.Windows.Forms.RadioButton();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.labDelayed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(707, 9);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 18;
            this.cmbChoosePortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedIndexChanged);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExcel.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.cmdExcel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdExcel.FlatAppearance.BorderSize = 0;
            this.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExcel.Location = new System.Drawing.Point(260, 5);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(59, 22);
            this.cmdExcel.TabIndex = 15;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = false;
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(313, 5);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(13, 13);
            this.lblId.TabIndex = 24;
            this.lblId.Text = "0";
            this.lblId.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "0";
            this.label1.Visible = false;
            // 
            // tmrRefreshPos
            // 
            this.tmrRefreshPos.Interval = 1500;
            this.tmrRefreshPos.Tick += new System.EventHandler(this.tmrRefreshPos_Tick);
            // 
            // dtgPositions
            // 
            this.dtgPositions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgPositions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dtgPositions.Location = new System.Drawing.Point(0, 0);
            this.dtgPositions.LookAndFeel.SkinName = "Blue";
            this.dtgPositions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgPositions.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgPositions.LookAndFeel.UseWindowsXPTheme = true;
            this.dtgPositions.MainView = this.dgPositions;
            this.dtgPositions.Name = "dtgPositions";
            this.dtgPositions.Size = new System.Drawing.Size(1022, 646);
            this.dtgPositions.TabIndex = 23;
            this.dtgPositions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgPositions});
            this.dtgPositions.Click += new System.EventHandler(this.dtg_Click);
            // 
            // dgPositions
            // 
            this.dgPositions.GridControl = this.dtgPositions;
            this.dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgPositions.Name = "dgPositions";
            this.dgPositions.OptionsBehavior.Editable = false;
            this.dgPositions.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgPositions.OptionsView.ColumnAutoWidth = false;
            this.dgPositions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgPositions_DragObjectDrop);
            this.dgPositions.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgPositions_CustomDrawCell);
            this.dgPositions.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgPositions_CustomDrawGroupRow);
            this.dgPositions.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgPositions_GroupLevelStyle);
            this.dgPositions.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgPositions_RowStyle);
            this.dgPositions.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgPositions_ColumnWidthChanged);
            this.dgPositions.ShowCustomizationForm += new System.EventHandler(this.dgPositions_ShowCustomizationForm);
            this.dgPositions.HideCustomizationForm += new System.EventHandler(this.dgPositions_HideCustomizationForm);
            this.dgPositions.EndSorting += new System.EventHandler(this.dgPositions_EndSorting);
            this.dgPositions.EndGrouping += new System.EventHandler(this.dgPositions_EndGrouping);
            this.dgPositions.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgPositions_FocusedRowChanged);
            this.dgPositions.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.dgPositions_CustomColumnDisplayText);
            this.dgPositions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgPositions_MouseDown);
            this.dgPositions.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgPositions_MouseUp);
            this.dgPositions.DoubleClick += new System.EventHandler(this.dgPositions_DoubleClick);
            // 
            // dtpHistDate
            // 
            this.dtpHistDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpHistDate.CalendarFont = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHistDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHistDate.Location = new System.Drawing.Point(478, 9);
            this.dtpHistDate.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dtpHistDate.MinDate = new System.DateTime(1920, 1, 1, 0, 0, 0, 0);
            this.dtpHistDate.Name = "dtpHistDate";
            this.dtpHistDate.Size = new System.Drawing.Size(93, 20);
            this.dtpHistDate.TabIndex = 2;
            this.dtpHistDate.CloseUp += new System.EventHandler(this.dtpHistDate_CloseUp);
            // 
            // cmbCustomView
            // 
            this.cmbCustomView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomView.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomView.FormattingEnabled = true;
            this.cmbCustomView.ItemHeight = 11;
            this.cmbCustomView.Location = new System.Drawing.Point(577, 9);
            this.cmbCustomView.Name = "cmbCustomView";
            this.cmbCustomView.Size = new System.Drawing.Size(124, 19);
            this.cmbCustomView.TabIndex = 26;
            this.cmbCustomView.SelectedIndexChanged += new System.EventHandler(this.cmbCustomView_SelectedIndexChanged);
            // 
            // chkLinkAllBoxes
            // 
            this.chkLinkAllBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLinkAllBoxes.AutoSize = true;
            this.chkLinkAllBoxes.BackColor = System.Drawing.Color.Transparent;
            this.chkLinkAllBoxes.ForeColor = System.Drawing.Color.White;
            this.chkLinkAllBoxes.Location = new System.Drawing.Point(887, 11);
            this.chkLinkAllBoxes.Name = "chkLinkAllBoxes";
            this.chkLinkAllBoxes.Size = new System.Drawing.Size(107, 17);
            this.chkLinkAllBoxes.TabIndex = 28;
            this.chkLinkAllBoxes.Text = "Link All Windows";
            this.chkLinkAllBoxes.UseVisualStyleBackColor = false;
            // 
            // cmdExpand
            // 
            this.cmdExpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(29, 6);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand.TabIndex = 29;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            this.cmdExpand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdExpand_MouseDown);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(94, 6);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse.TabIndex = 30;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            this.cmdCollapse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdCollapse_MouseDown);
            // 
            // radHistoric
            // 
            this.radHistoric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radHistoric.AutoSize = true;
            this.radHistoric.BackColor = System.Drawing.Color.Transparent;
            this.radHistoric.ForeColor = System.Drawing.Color.White;
            this.radHistoric.Location = new System.Drawing.Point(404, 9);
            this.radHistoric.Name = "radHistoric";
            this.radHistoric.Size = new System.Drawing.Size(68, 17);
            this.radHistoric.TabIndex = 32;
            this.radHistoric.TabStop = true;
            this.radHistoric.Text = "Historical";
            this.radHistoric.UseVisualStyleBackColor = false;
            this.radHistoric.CheckedChanged += new System.EventHandler(this.radHistoric_CheckedChanged);
            // 
            // radRealTime
            // 
            this.radRealTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radRealTime.AutoSize = true;
            this.radRealTime.BackColor = System.Drawing.Color.Transparent;
            this.radRealTime.ForeColor = System.Drawing.Color.White;
            this.radRealTime.Location = new System.Drawing.Point(325, 9);
            this.radRealTime.Name = "radRealTime";
            this.radRealTime.Size = new System.Drawing.Size(73, 17);
            this.radRealTime.TabIndex = 31;
            this.radRealTime.TabStop = true;
            this.radRealTime.Text = "Real-Time";
            this.radRealTime.UseVisualStyleBackColor = false;
            this.radRealTime.CheckedChanged += new System.EventHandler(this.radRealTime_CheckedChanged);
            // 
            // timer2
            // 
            this.timer2.Interval = 600000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // labDelayed
            // 
            this.labDelayed.AutoSize = true;
            this.labDelayed.BackColor = System.Drawing.Color.Yellow;
            this.labDelayed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDelayed.ForeColor = System.Drawing.Color.Red;
            this.labDelayed.Location = new System.Drawing.Point(159, 9);
            this.labDelayed.Name = "labDelayed";
            this.labDelayed.Size = new System.Drawing.Size(78, 16);
            this.labDelayed.TabIndex = 33;
            this.labDelayed.Text = "DELAYED";
            this.labDelayed.Visible = false;
            // 
            // frmPositions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1020, 649);
            this.Controls.Add(this.labDelayed);
            this.Controls.Add(this.radHistoric);
            this.Controls.Add(this.radRealTime);
            this.Controls.Add(this.dtpHistDate);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.chkLinkAllBoxes);
            this.Controls.Add(this.cmbCustomView);
            this.Controls.Add(this.cmbChoosePortfolio);
            this.Controls.Add(this.dtgPositions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPositions";
            this.Text = "Positions";
            this.Load += new System.EventHandler(this.frmPositions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgPositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private System.Windows.Forms.Button cmdExcel;
        private System.Windows.Forms.Timer tmrRefreshPos;
        private MyXtraGrid.MyGridControl dtgPositions;
        private MyXtraGrid.MyGridView dgPositions;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHistDate;
        private System.Windows.Forms.ComboBox cmbCustomView;
        private System.Windows.Forms.CheckBox chkLinkAllBoxes;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.RadioButton radHistoric;
        private System.Windows.Forms.RadioButton radRealTime;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label labDelayed;
    }
}