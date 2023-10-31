namespace LiveTrade2
{
    partial class frmWatchList
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
            this.dtgWatchList = new NCustomControls.MyGridControl();
            this.dgWatchList = new NCustomControls.MyGridView();
            this.chkAuction = new System.Windows.Forms.CheckBox();
            this.dtpHL_IniDate = new System.Windows.Forms.DateTimePicker();
            this.lblHL_IniDate = new System.Windows.Forms.Label();
            this.dtpHL_EndDate = new System.Windows.Forms.DateTimePicker();
            this.lblHL_EndDate = new System.Windows.Forms.Label();
            this.gbBrazilAll = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkReceipt = new System.Windows.Forms.CheckBox();
            this.chkWarrant = new System.Windows.Forms.CheckBox();
            this.chkBzIliquid = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkBzLiquid = new System.Windows.Forms.CheckBox();
            this.chkBovespa = new System.Windows.Forms.CheckBox();
            this.gbOffshore = new System.Windows.Forms.GroupBox();
            this.chkADRs = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkUsETFs = new System.Windows.Forms.CheckBox();
            this.chkUsCommon = new System.Windows.Forms.CheckBox();
            this.radYTD = new System.Windows.Forms.RadioButton();
            this.radMTD = new System.Windows.Forms.RadioButton();
            this.rad2008Low = new System.Windows.Forms.RadioButton();
            this.rad12m = new System.Windows.Forms.RadioButton();
            this.lblOptions = new System.Windows.Forms.Label();
            this.chkLiquid = new System.Windows.Forms.CheckBox();
            this.chkMH_Trad = new System.Windows.Forms.CheckBox();
            this.chkMH_LS = new System.Windows.Forms.CheckBox();
            this.chkFIA_Trad = new System.Windows.Forms.CheckBox();
            this.chkFIA_LS = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgWatchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWatchList)).BeginInit();
            this.gbBrazilAll.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbOffshore.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgWatchList
            // 
            this.dtgWatchList.AllowDrop = true;
            this.dtgWatchList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgWatchList.Location = new System.Drawing.Point(0, 170);
            this.dtgWatchList.LookAndFeel.SkinName = "Blue";
            this.dtgWatchList.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgWatchList.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgWatchList.MainView = this.dgWatchList;
            this.dtgWatchList.Name = "dtgWatchList";
            this.dtgWatchList.Size = new System.Drawing.Size(862, 551);
            this.dtgWatchList.TabIndex = 26;
            this.dtgWatchList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgWatchList});
            this.dtgWatchList.Click += new System.EventHandler(this.dtgQuotes_Click);
            this.dtgWatchList.DragOver += new System.Windows.Forms.DragEventHandler(this.dtgQuotes_DragOver);
            this.dtgWatchList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgQuotes_KeyDown);
            this.dtgWatchList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            this.dtgWatchList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            // 
            // dgWatchList
            // 
            this.dgWatchList.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgWatchList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgWatchList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgWatchList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgWatchList.GridControl = this.dtgWatchList;
            this.dgWatchList.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgWatchList.Name = "dgWatchList";
            this.dgWatchList.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgWatchList.OptionsBehavior.Editable = false;
            this.dgWatchList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgWatchList.OptionsSelection.UseIndicatorForSelection = false;
            this.dgWatchList.OptionsView.ColumnAutoWidth = false;
            this.dgWatchList.OptionsView.ShowIndicator = false;
            this.dgWatchList.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgWatchList_DragObjectDrop);
            this.dgWatchList.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgWatchList.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgQuotes_CustomDrawGroupRow);
            this.dgWatchList.GroupLevelStyle += new DevExpress.XtraGrid.Views.Grid.GroupLevelStyleEventHandler(this.dgQuotes_GroupLevelStyle);
            this.dgWatchList.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgQuotes_RowStyle);
            this.dgWatchList.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgQuotes_ColumnWidthChanged);
            this.dgWatchList.EndSorting += new System.EventHandler(this.dgQuotes_EndSorting);
            this.dgWatchList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgQuotes_KeyDown);
            this.dgWatchList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgQuotes_MouseDown);
            this.dgWatchList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgQuotes_MouseMove);
            // 
            // chkAuction
            // 
            this.chkAuction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAuction.AutoSize = true;
            this.chkAuction.ForeColor = System.Drawing.Color.White;
            this.chkAuction.Location = new System.Drawing.Point(671, 7);
            this.chkAuction.Name = "chkAuction";
            this.chkAuction.Size = new System.Drawing.Size(62, 17);
            this.chkAuction.TabIndex = 27;
            this.chkAuction.Text = "Auction";
            this.chkAuction.UseVisualStyleBackColor = true;
            this.chkAuction.Visible = false;
            this.chkAuction.CheckedChanged += new System.EventHandler(this.chkAuction_CheckedChanged);
            // 
            // dtpHL_IniDate
            // 
            this.dtpHL_IniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHL_IniDate.Location = new System.Drawing.Point(15, 27);
            this.dtpHL_IniDate.Name = "dtpHL_IniDate";
            this.dtpHL_IniDate.Size = new System.Drawing.Size(96, 20);
            this.dtpHL_IniDate.TabIndex = 30;
            this.dtpHL_IniDate.Visible = false;
            this.dtpHL_IniDate.CloseUp += new System.EventHandler(this.dtpHL_IniDate_CloseUp);
            this.dtpHL_IniDate.DropDown += new System.EventHandler(this.dtpHL_IniDate_DropDown);
            this.dtpHL_IniDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpHL_IniDate_KeyUp);
            this.dtpHL_IniDate.MouseLeave += new System.EventHandler(this.dtpHL_IniDate_MouseLeave);
            // 
            // lblHL_IniDate
            // 
            this.lblHL_IniDate.AutoSize = true;
            this.lblHL_IniDate.Location = new System.Drawing.Point(12, 31);
            this.lblHL_IniDate.Name = "lblHL_IniDate";
            this.lblHL_IniDate.Size = new System.Drawing.Size(72, 13);
            this.lblHL_IniDate.TabIndex = 31;
            this.lblHL_IniDate.Text = "No Start Date";
            this.lblHL_IniDate.MouseEnter += new System.EventHandler(this.lblHL_IniDate_MouseEnter);
            // 
            // dtpHL_EndDate
            // 
            this.dtpHL_EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHL_EndDate.Location = new System.Drawing.Point(15, 60);
            this.dtpHL_EndDate.Name = "dtpHL_EndDate";
            this.dtpHL_EndDate.Size = new System.Drawing.Size(96, 20);
            this.dtpHL_EndDate.TabIndex = 32;
            this.dtpHL_EndDate.Visible = false;
            this.dtpHL_EndDate.CloseUp += new System.EventHandler(this.dtpHL_EndDate_CloseUp);
            this.dtpHL_EndDate.DropDown += new System.EventHandler(this.dtpHL_EndDate_DropDown);
            this.dtpHL_EndDate.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dtpHL_EndDate_KeyUp);
            this.dtpHL_EndDate.MouseLeave += new System.EventHandler(this.dtpHL_EndDate_MouseLeave);
            // 
            // lblHL_EndDate
            // 
            this.lblHL_EndDate.AutoSize = true;
            this.lblHL_EndDate.Location = new System.Drawing.Point(15, 64);
            this.lblHL_EndDate.Name = "lblHL_EndDate";
            this.lblHL_EndDate.Size = new System.Drawing.Size(69, 13);
            this.lblHL_EndDate.TabIndex = 33;
            this.lblHL_EndDate.Text = "No End Date";
            this.lblHL_EndDate.MouseEnter += new System.EventHandler(this.lblHL_EndDate_MouseEnter);
            // 
            // gbBrazilAll
            // 
            this.gbBrazilAll.Controls.Add(this.groupBox1);
            this.gbBrazilAll.Controls.Add(this.chkBzIliquid);
            this.gbBrazilAll.Controls.Add(this.groupBox2);
            this.gbBrazilAll.Location = new System.Drawing.Point(18, 98);
            this.gbBrazilAll.Name = "gbBrazilAll";
            this.gbBrazilAll.Size = new System.Drawing.Size(406, 68);
            this.gbBrazilAll.TabIndex = 34;
            this.gbBrazilAll.TabStop = false;
            this.gbBrazilAll.Text = "Brazil All";
            this.gbBrazilAll.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkReceipt);
            this.groupBox1.Controls.Add(this.chkWarrant);
            this.groupBox1.Location = new System.Drawing.Point(252, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 43);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Warrant and Receipt";
            // 
            // chkReceipt
            // 
            this.chkReceipt.AutoSize = true;
            this.chkReceipt.Checked = true;
            this.chkReceipt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReceipt.Location = new System.Drawing.Point(80, 19);
            this.chkReceipt.Name = "chkReceipt";
            this.chkReceipt.Size = new System.Drawing.Size(63, 17);
            this.chkReceipt.TabIndex = 1;
            this.chkReceipt.Text = "Receipt";
            this.chkReceipt.UseVisualStyleBackColor = true;
            this.chkReceipt.CheckedChanged += new System.EventHandler(this.chkReceipt_CheckedChanged);
            // 
            // chkWarrant
            // 
            this.chkWarrant.AutoSize = true;
            this.chkWarrant.Checked = true;
            this.chkWarrant.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWarrant.Location = new System.Drawing.Point(6, 19);
            this.chkWarrant.Name = "chkWarrant";
            this.chkWarrant.Size = new System.Drawing.Size(64, 17);
            this.chkWarrant.TabIndex = 0;
            this.chkWarrant.Text = "Warrant";
            this.chkWarrant.UseVisualStyleBackColor = true;
            this.chkWarrant.CheckedChanged += new System.EventHandler(this.chkWarrant_CheckedChanged);
            // 
            // chkBzIliquid
            // 
            this.chkBzIliquid.AutoSize = true;
            this.chkBzIliquid.Checked = true;
            this.chkBzIliquid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBzIliquid.Location = new System.Drawing.Point(155, 38);
            this.chkBzIliquid.Name = "chkBzIliquid";
            this.chkBzIliquid.Size = new System.Drawing.Size(91, 17);
            this.chkBzIliquid.TabIndex = 1;
            this.chkBzIliquid.Text = "BZ Not Liquid";
            this.chkBzIliquid.UseVisualStyleBackColor = true;
            this.chkBzIliquid.CheckedChanged += new System.EventHandler(this.chkBzIliquid_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkBzLiquid);
            this.groupBox2.Controls.Add(this.chkBovespa);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(135, 43);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Brazil Liquid";
            // 
            // chkBzLiquid
            // 
            this.chkBzLiquid.AutoSize = true;
            this.chkBzLiquid.Checked = true;
            this.chkBzLiquid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBzLiquid.Location = new System.Drawing.Point(80, 19);
            this.chkBzLiquid.Name = "chkBzLiquid";
            this.chkBzLiquid.Size = new System.Drawing.Size(52, 17);
            this.chkBzLiquid.TabIndex = 1;
            this.chkBzLiquid.Text = "Other";
            this.chkBzLiquid.UseVisualStyleBackColor = true;
            this.chkBzLiquid.CheckedChanged += new System.EventHandler(this.chkBzLiquid_CheckedChanged);
            // 
            // chkBovespa
            // 
            this.chkBovespa.AutoSize = true;
            this.chkBovespa.Checked = true;
            this.chkBovespa.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBovespa.Location = new System.Drawing.Point(6, 19);
            this.chkBovespa.Name = "chkBovespa";
            this.chkBovespa.Size = new System.Drawing.Size(68, 17);
            this.chkBovespa.TabIndex = 0;
            this.chkBovespa.Text = "Bovespa";
            this.chkBovespa.UseVisualStyleBackColor = true;
            this.chkBovespa.CheckedChanged += new System.EventHandler(this.chkBovespa_CheckedChanged);
            // 
            // gbOffshore
            // 
            this.gbOffshore.Controls.Add(this.chkADRs);
            this.gbOffshore.Controls.Add(this.groupBox4);
            this.gbOffshore.Location = new System.Drawing.Point(427, 98);
            this.gbOffshore.Name = "gbOffshore";
            this.gbOffshore.Size = new System.Drawing.Size(231, 66);
            this.gbOffshore.TabIndex = 35;
            this.gbOffshore.TabStop = false;
            this.gbOffshore.Text = "Offshore";
            this.gbOffshore.Visible = false;
            // 
            // chkADRs
            // 
            this.chkADRs.AutoSize = true;
            this.chkADRs.Location = new System.Drawing.Point(174, 36);
            this.chkADRs.Name = "chkADRs";
            this.chkADRs.Size = new System.Drawing.Size(54, 17);
            this.chkADRs.TabIndex = 1;
            this.chkADRs.Text = "ADRs";
            this.chkADRs.UseVisualStyleBackColor = true;
            this.chkADRs.CheckedChanged += new System.EventHandler(this.chkADRs_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkUsETFs);
            this.groupBox4.Controls.Add(this.chkUsCommon);
            this.groupBox4.Location = new System.Drawing.Point(6, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(164, 43);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "US";
            // 
            // chkUsETFs
            // 
            this.chkUsETFs.AutoSize = true;
            this.chkUsETFs.Location = new System.Drawing.Point(93, 19);
            this.chkUsETFs.Name = "chkUsETFs";
            this.chkUsETFs.Size = new System.Drawing.Size(69, 17);
            this.chkUsETFs.TabIndex = 1;
            this.chkUsETFs.Text = "US ETFs";
            this.chkUsETFs.UseVisualStyleBackColor = true;
            this.chkUsETFs.CheckedChanged += new System.EventHandler(this.chkUsETFs_CheckedChanged);
            // 
            // chkUsCommon
            // 
            this.chkUsCommon.AutoSize = true;
            this.chkUsCommon.Location = new System.Drawing.Point(6, 19);
            this.chkUsCommon.Name = "chkUsCommon";
            this.chkUsCommon.Size = new System.Drawing.Size(81, 17);
            this.chkUsCommon.TabIndex = 0;
            this.chkUsCommon.Text = "US Equities";
            this.chkUsCommon.UseVisualStyleBackColor = true;
            this.chkUsCommon.CheckedChanged += new System.EventHandler(this.chkUsCommon_CheckedChanged);
            // 
            // radYTD
            // 
            this.radYTD.AutoSize = true;
            this.radYTD.Location = new System.Drawing.Point(117, 28);
            this.radYTD.Name = "radYTD";
            this.radYTD.Size = new System.Drawing.Size(47, 17);
            this.radYTD.TabIndex = 38;
            this.radYTD.TabStop = true;
            this.radYTD.Text = "YTD";
            this.radYTD.UseVisualStyleBackColor = true;
            this.radYTD.CheckedChanged += new System.EventHandler(this.radYTD_CheckedChanged);
            // 
            // radMTD
            // 
            this.radMTD.AutoSize = true;
            this.radMTD.Location = new System.Drawing.Point(170, 28);
            this.radMTD.Name = "radMTD";
            this.radMTD.Size = new System.Drawing.Size(49, 17);
            this.radMTD.TabIndex = 39;
            this.radMTD.TabStop = true;
            this.radMTD.Text = "MTD";
            this.radMTD.UseVisualStyleBackColor = true;
            this.radMTD.CheckedChanged += new System.EventHandler(this.radMTD_CheckedChanged);
            // 
            // rad2008Low
            // 
            this.rad2008Low.AutoSize = true;
            this.rad2008Low.Location = new System.Drawing.Point(117, 60);
            this.rad2008Low.Name = "rad2008Low";
            this.rad2008Low.Size = new System.Drawing.Size(86, 17);
            this.rad2008Low.TabIndex = 40;
            this.rad2008Low.TabStop = true;
            this.rad2008Low.Text = "Ibov \'08 Low";
            this.rad2008Low.UseVisualStyleBackColor = true;
            this.rad2008Low.CheckedChanged += new System.EventHandler(this.rad2008Low_CheckedChanged);
            // 
            // rad12m
            // 
            this.rad12m.AutoSize = true;
            this.rad12m.Location = new System.Drawing.Point(225, 29);
            this.rad12m.Name = "rad12m";
            this.rad12m.Size = new System.Drawing.Size(74, 17);
            this.rad12m.TabIndex = 41;
            this.rad12m.TabStop = true;
            this.rad12m.Text = "12 months";
            this.rad12m.UseVisualStyleBackColor = true;
            this.rad12m.CheckedChanged += new System.EventHandler(this.rad12m_CheckedChanged);
            // 
            // lblOptions
            // 
            this.lblOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOptions.AutoSize = true;
            this.lblOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptions.ForeColor = System.Drawing.Color.Blue;
            this.lblOptions.Location = new System.Drawing.Point(807, 9);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(43, 13);
            this.lblOptions.TabIndex = 42;
            this.lblOptions.Text = "Options";
            this.lblOptions.Click += new System.EventHandler(this.lblOptions_Click);
            // 
            // chkLiquid
            // 
            this.chkLiquid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLiquid.AutoSize = true;
            this.chkLiquid.ForeColor = System.Drawing.Color.White;
            this.chkLiquid.Location = new System.Drawing.Point(611, 8);
            this.chkLiquid.Name = "chkLiquid";
            this.chkLiquid.Size = new System.Drawing.Size(54, 17);
            this.chkLiquid.TabIndex = 46;
            this.chkLiquid.Text = "Liquid";
            this.chkLiquid.UseVisualStyleBackColor = true;
            this.chkLiquid.Visible = false;
            this.chkLiquid.CheckedChanged += new System.EventHandler(this.chkLiquid_CheckedChanged);
            // 
            // chkMH_Trad
            // 
            this.chkMH_Trad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMH_Trad.AutoSize = true;
            this.chkMH_Trad.ForeColor = System.Drawing.Color.White;
            this.chkMH_Trad.Location = new System.Drawing.Point(253, 8);
            this.chkMH_Trad.Name = "chkMH_Trad";
            this.chkMH_Trad.Size = new System.Drawing.Size(68, 17);
            this.chkMH_Trad.TabIndex = 47;
            this.chkMH_Trad.Text = "MH-Trad";
            this.chkMH_Trad.UseVisualStyleBackColor = true;
            this.chkMH_Trad.Visible = false;
            this.chkMH_Trad.CheckedChanged += new System.EventHandler(this.chkMH_Trad_CheckedChanged);
            // 
            // chkMH_LS
            // 
            this.chkMH_LS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMH_LS.AutoSize = true;
            this.chkMH_LS.ForeColor = System.Drawing.Color.White;
            this.chkMH_LS.Location = new System.Drawing.Point(322, 8);
            this.chkMH_LS.Name = "chkMH_LS";
            this.chkMH_LS.Size = new System.Drawing.Size(59, 17);
            this.chkMH_LS.TabIndex = 48;
            this.chkMH_LS.Text = "MH-LS";
            this.chkMH_LS.UseVisualStyleBackColor = true;
            this.chkMH_LS.Visible = false;
            this.chkMH_LS.CheckedChanged += new System.EventHandler(this.chkMH_LS_CheckedChanged);
            // 
            // chkFIA_Trad
            // 
            this.chkFIA_Trad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFIA_Trad.AutoSize = true;
            this.chkFIA_Trad.ForeColor = System.Drawing.Color.White;
            this.chkFIA_Trad.Location = new System.Drawing.Point(387, 8);
            this.chkFIA_Trad.Name = "chkFIA_Trad";
            this.chkFIA_Trad.Size = new System.Drawing.Size(67, 17);
            this.chkFIA_Trad.TabIndex = 49;
            this.chkFIA_Trad.Text = "FIA-Trad";
            this.chkFIA_Trad.UseVisualStyleBackColor = true;
            this.chkFIA_Trad.Visible = false;
            this.chkFIA_Trad.CheckedChanged += new System.EventHandler(this.chkFIA_Trad_CheckedChanged);
            // 
            // chkFIA_LS
            // 
            this.chkFIA_LS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkFIA_LS.AutoSize = true;
            this.chkFIA_LS.ForeColor = System.Drawing.Color.White;
            this.chkFIA_LS.Location = new System.Drawing.Point(460, 8);
            this.chkFIA_LS.Name = "chkFIA_LS";
            this.chkFIA_LS.Size = new System.Drawing.Size(58, 17);
            this.chkFIA_LS.TabIndex = 50;
            this.chkFIA_LS.Text = "FIA-LS";
            this.chkFIA_LS.UseVisualStyleBackColor = true;
            this.chkFIA_LS.Visible = false;
            this.chkFIA_LS.CheckedChanged += new System.EventHandler(this.chkFIA_LS_CheckedChanged);
            // 
            // frmWatchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 733);
            this.Controls.Add(this.chkFIA_LS);
            this.Controls.Add(this.chkFIA_Trad);
            this.Controls.Add(this.chkMH_LS);
            this.Controls.Add(this.chkMH_Trad);
            this.Controls.Add(this.chkAuction);
            this.Controls.Add(this.chkLiquid);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.dtgWatchList);
            this.Controls.Add(this.rad12m);
            this.Controls.Add(this.rad2008Low);
            this.Controls.Add(this.radMTD);
            this.Controls.Add(this.radYTD);
            this.Controls.Add(this.gbOffshore);
            this.Controls.Add(this.gbBrazilAll);
            this.Controls.Add(this.lblHL_EndDate);
            this.Controls.Add(this.dtpHL_EndDate);
            this.Controls.Add(this.lblHL_IniDate);
            this.Controls.Add(this.dtpHL_IniDate);
            this.Name = "frmWatchList";
            this.Text = "Quote";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWatchList_FormClosing);
            this.Load += new System.EventHandler(this.frmWatchList_Load);
            this.Resize += new System.EventHandler(this.frmWatchList_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dtgWatchList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWatchList)).EndInit();
            this.gbBrazilAll.ResumeLayout(false);
            this.gbBrazilAll.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbOffshore.ResumeLayout(false);
            this.gbOffshore.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkAuction;
        private System.Windows.Forms.DateTimePicker dtpHL_IniDate;
        private System.Windows.Forms.Label lblHL_IniDate;
        private System.Windows.Forms.DateTimePicker dtpHL_EndDate;
        private System.Windows.Forms.Label lblHL_EndDate;
        private System.Windows.Forms.GroupBox gbBrazilAll;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkBzLiquid;
        private System.Windows.Forms.CheckBox chkBovespa;
        private System.Windows.Forms.CheckBox chkBzIliquid;
        private System.Windows.Forms.GroupBox gbOffshore;
        private System.Windows.Forms.CheckBox chkADRs;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkUsETFs;
        private System.Windows.Forms.CheckBox chkUsCommon;
        private System.Windows.Forms.RadioButton radYTD;
        private System.Windows.Forms.RadioButton radMTD;
        private System.Windows.Forms.RadioButton rad2008Low;
        private System.Windows.Forms.RadioButton rad12m;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.CheckBox chkLiquid;
        private System.Windows.Forms.CheckBox chkMH_Trad;
        private System.Windows.Forms.CheckBox chkMH_LS;
        private System.Windows.Forms.CheckBox chkFIA_Trad;
        private System.Windows.Forms.CheckBox chkFIA_LS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkReceipt;
        private System.Windows.Forms.CheckBox chkWarrant;
        private NCustomControls.MyGridView dgWatchList;
        private NCustomControls.MyGridControl dtgWatchList;
    }
}

