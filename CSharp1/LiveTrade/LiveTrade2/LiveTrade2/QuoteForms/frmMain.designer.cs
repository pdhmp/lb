namespace LiveTrade2
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblFIXState = new System.Windows.Forms.Label();
            this.lblFIXConnected = new System.Windows.Forms.Label();
            this.cmdOrderReview = new System.Windows.Forms.Button();
            this.dtgLTQuotes = new NCustomControls.MyGridControl();
            this.dgLTQuotes = new NCustomControls.MyGridView();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.cmdSaveTickers = new System.Windows.Forms.Button();
            this.chkMTDChange = new System.Windows.Forms.CheckBox();
            this.chkYTDChange = new System.Windows.Forms.CheckBox();
            this.lblLastTime = new System.Windows.Forms.Label();
            this.chkBidAsk = new System.Windows.Forms.CheckBox();
            this.chkBidAskSize = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFIXSentNet = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFIXSentGross = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFIXExecs = new System.Windows.Forms.Label();
            this.lblFIXReplaces = new System.Windows.Forms.Label();
            this.cmdChart = new System.Windows.Forms.Button();
            this.cmdViewAuction = new System.Windows.Forms.Button();
            this.cmdOptionChain = new System.Windows.Forms.Button();
            this.cmdEnableTrade = new System.Windows.Forms.Button();
            this.cmdLimits = new System.Windows.Forms.Button();
            this.cmdWatchList = new System.Windows.Forms.Button();
            this.cmdPNL = new System.Windows.Forms.Button();
            this.labNotInserted = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBell_1 = new System.Windows.Forms.Label();
            this.lblBSE_1 = new System.Windows.Forms.Label();
            this.lblLinkBOV_1 = new System.Windows.Forms.Label();
            this.lblXPBOV_1 = new System.Windows.Forms.Label();
            this.lblBell_0 = new System.Windows.Forms.Label();
            this.lblBSE_0 = new System.Windows.Forms.Label();
            this.lblLinkBOV_0 = new System.Windows.Forms.Label();
            this.lblXPBOV_0 = new System.Windows.Forms.Label();
            this.cmdMKTData = new System.Windows.Forms.Button();
            this.optAgg = new System.Windows.Forms.RadioButton();
            this.optCheck = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLTQuotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLTQuotes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 125;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lblFIXState
            // 
            this.lblFIXState.AutoSize = true;
            this.lblFIXState.BackColor = System.Drawing.Color.Red;
            this.lblFIXState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFIXState.ForeColor = System.Drawing.Color.White;
            this.lblFIXState.Location = new System.Drawing.Point(960, 58);
            this.lblFIXState.Name = "lblFIXState";
            this.lblFIXState.Size = new System.Drawing.Size(84, 16);
            this.lblFIXState.TabIndex = 29;
            this.lblFIXState.Text = "FIX STATE";
            // 
            // lblFIXConnected
            // 
            this.lblFIXConnected.AutoSize = true;
            this.lblFIXConnected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblFIXConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFIXConnected.ForeColor = System.Drawing.Color.White;
            this.lblFIXConnected.Location = new System.Drawing.Point(960, 34);
            this.lblFIXConnected.Name = "lblFIXConnected";
            this.lblFIXConnected.Size = new System.Drawing.Size(146, 16);
            this.lblFIXConnected.TabIndex = 42;
            this.lblFIXConnected.Text = "FIX IS CONNECTED";
            this.lblFIXConnected.Visible = false;
            // 
            // cmdOrderReview
            // 
            this.cmdOrderReview.Location = new System.Drawing.Point(130, 3);
            this.cmdOrderReview.Name = "cmdOrderReview";
            this.cmdOrderReview.Size = new System.Drawing.Size(112, 19);
            this.cmdOrderReview.TabIndex = 56;
            this.cmdOrderReview.Text = "ORDER REVIEW";
            this.cmdOrderReview.UseVisualStyleBackColor = true;
            this.cmdOrderReview.Click += new System.EventHandler(this.cmdOrderReview_Click);
            // 
            // dtgLTQuotes
            // 
            this.dtgLTQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgLTQuotes.Location = new System.Drawing.Point(1, 94);
            this.dtgLTQuotes.LookAndFeel.SkinName = "Blue";
            this.dtgLTQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgLTQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgLTQuotes.MainView = this.dgLTQuotes;
            this.dtgLTQuotes.Name = "dtgLTQuotes";
            this.dtgLTQuotes.Size = new System.Drawing.Size(1388, 458);
            this.dtgLTQuotes.TabIndex = 57;
            this.dtgLTQuotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgLTQuotes});
            // 
            // dgLTQuotes
            // 
            this.dgLTQuotes.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgLTQuotes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgLTQuotes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgLTQuotes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgLTQuotes.GridControl = this.dtgLTQuotes;
            this.dgLTQuotes.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgLTQuotes.Name = "dgLTQuotes";
            this.dgLTQuotes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgLTQuotes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgLTQuotes.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.dgLTQuotes.OptionsCustomization.AllowFilter = false;
            this.dgLTQuotes.OptionsCustomization.AllowGroup = false;
            this.dgLTQuotes.OptionsCustomization.AllowSort = false;
            this.dgLTQuotes.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgLTQuotes.OptionsSelection.UseIndicatorForSelection = false;
            this.dgLTQuotes.OptionsView.ColumnAutoWidth = false;
            this.dgLTQuotes.OptionsView.RowAutoHeight = true;
            this.dgLTQuotes.OptionsView.ShowGroupPanel = false;
            this.dgLTQuotes.OptionsView.ShowHorzLines = false;
            this.dgLTQuotes.OptionsView.ShowVertLines = false;
            this.dgLTQuotes.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgLTQuotes.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgLTQuotes_ColumnWidthChanged);
            this.dgLTQuotes.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.dgLTQuotes_ShowingEditor);
            this.dgLTQuotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgLTQuotes_MouseDown);
            this.dgLTQuotes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgLTQuotes_MouseUp);
            this.dgLTQuotes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dgLTQuotes_MouseMove);
            this.dgLTQuotes.DoubleClick += new System.EventHandler(this.dgLTQuotes_DoubleClick);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(484, 35);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(112, 19);
            this.cmdEdit.TabIndex = 58;
            this.cmdEdit.Text = "Edit Grid";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdSaveTickers
            // 
            this.cmdSaveTickers.Location = new System.Drawing.Point(484, 60);
            this.cmdSaveTickers.Name = "cmdSaveTickers";
            this.cmdSaveTickers.Size = new System.Drawing.Size(112, 19);
            this.cmdSaveTickers.TabIndex = 59;
            this.cmdSaveTickers.Text = "Save Grid";
            this.cmdSaveTickers.UseVisualStyleBackColor = true;
            this.cmdSaveTickers.Click += new System.EventHandler(this.cmdSaveTickers_Click);
            // 
            // chkMTDChange
            // 
            this.chkMTDChange.AutoSize = true;
            this.chkMTDChange.Checked = true;
            this.chkMTDChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMTDChange.Location = new System.Drawing.Point(6, 42);
            this.chkMTDChange.Name = "chkMTDChange";
            this.chkMTDChange.Size = new System.Drawing.Size(90, 17);
            this.chkMTDChange.TabIndex = 60;
            this.chkMTDChange.Text = "MTD Change";
            this.chkMTDChange.UseVisualStyleBackColor = true;
            this.chkMTDChange.CheckedChanged += new System.EventHandler(this.chkMTDChange_CheckedChanged);
            // 
            // chkYTDChange
            // 
            this.chkYTDChange.AutoSize = true;
            this.chkYTDChange.Checked = true;
            this.chkYTDChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkYTDChange.Location = new System.Drawing.Point(6, 19);
            this.chkYTDChange.Name = "chkYTDChange";
            this.chkYTDChange.Size = new System.Drawing.Size(88, 17);
            this.chkYTDChange.TabIndex = 61;
            this.chkYTDChange.Text = "YTD Change";
            this.chkYTDChange.UseVisualStyleBackColor = true;
            this.chkYTDChange.CheckedChanged += new System.EventHandler(this.chkYTDChange_CheckedChanged);
            // 
            // lblLastTime
            // 
            this.lblLastTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastTime.AutoSize = true;
            this.lblLastTime.Location = new System.Drawing.Point(614, 61);
            this.lblLastTime.Name = "lblLastTime";
            this.lblLastTime.Size = new System.Drawing.Size(13, 13);
            this.lblLastTime.TabIndex = 62;
            this.lblLastTime.Text = "0";
            this.lblLastTime.Visible = false;
            // 
            // chkBidAsk
            // 
            this.chkBidAsk.AutoSize = true;
            this.chkBidAsk.Checked = true;
            this.chkBidAsk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBidAsk.Location = new System.Drawing.Point(100, 19);
            this.chkBidAsk.Name = "chkBidAsk";
            this.chkBidAsk.Size = new System.Drawing.Size(64, 17);
            this.chkBidAsk.TabIndex = 63;
            this.chkBidAsk.Text = "Bid/Ask";
            this.chkBidAsk.UseVisualStyleBackColor = true;
            this.chkBidAsk.CheckedChanged += new System.EventHandler(this.chkBidAsk_CheckedChanged);
            // 
            // chkBidAskSize
            // 
            this.chkBidAskSize.AutoSize = true;
            this.chkBidAskSize.Checked = true;
            this.chkBidAskSize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBidAskSize.Location = new System.Drawing.Point(100, 42);
            this.chkBidAskSize.Name = "chkBidAskSize";
            this.chkBidAskSize.Size = new System.Drawing.Size(87, 17);
            this.chkBidAskSize.TabIndex = 64;
            this.chkBidAskSize.Text = "Bid/Ask Size";
            this.chkBidAskSize.UseVisualStyleBackColor = true;
            this.chkBidAskSize.CheckedChanged += new System.EventHandler(this.chkBidAskSize_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkYTDChange);
            this.groupBox1.Controls.Add(this.chkBidAskSize);
            this.groupBox1.Controls.Add(this.chkMTDChange);
            this.groupBox1.Controls.Add(this.chkBidAsk);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 65);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Columns";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblFIXSentNet);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblFIXSentGross);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblFIXExecs);
            this.groupBox2.Controls.Add(this.lblFIXReplaces);
            this.groupBox2.Location = new System.Drawing.Point(220, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 65);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FIX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(118, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 62;
            this.label4.Text = "Net Sent";
            // 
            // lblFIXSentNet
            // 
            this.lblFIXSentNet.AutoSize = true;
            this.lblFIXSentNet.Location = new System.Drawing.Point(183, 38);
            this.lblFIXSentNet.Name = "lblFIXSentNet";
            this.lblFIXSentNet.Size = new System.Drawing.Size(13, 13);
            this.lblFIXSentNet.TabIndex = 61;
            this.lblFIXSentNet.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Gross Sent";
            // 
            // lblFIXSentGross
            // 
            this.lblFIXSentGross.AutoSize = true;
            this.lblFIXSentGross.Location = new System.Drawing.Point(183, 14);
            this.lblFIXSentGross.Name = "lblFIXSentGross";
            this.lblFIXSentGross.Size = new System.Drawing.Size(13, 13);
            this.lblFIXSentGross.TabIndex = 59;
            this.lblFIXSentGross.Text = "0";
            this.lblFIXSentGross.Click += new System.EventHandler(this.lblFIXSentGross_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "FIX Execs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "FIX Replaces";
            // 
            // lblFIXExecs
            // 
            this.lblFIXExecs.AutoSize = true;
            this.lblFIXExecs.Location = new System.Drawing.Point(83, 14);
            this.lblFIXExecs.Name = "lblFIXExecs";
            this.lblFIXExecs.Size = new System.Drawing.Size(13, 13);
            this.lblFIXExecs.TabIndex = 56;
            this.lblFIXExecs.Text = "0";
            // 
            // lblFIXReplaces
            // 
            this.lblFIXReplaces.AutoSize = true;
            this.lblFIXReplaces.Location = new System.Drawing.Point(83, 38);
            this.lblFIXReplaces.Name = "lblFIXReplaces";
            this.lblFIXReplaces.Size = new System.Drawing.Size(13, 13);
            this.lblFIXReplaces.TabIndex = 55;
            this.lblFIXReplaces.Text = "0";
            // 
            // cmdChart
            // 
            this.cmdChart.Location = new System.Drawing.Point(602, 3);
            this.cmdChart.Name = "cmdChart";
            this.cmdChart.Size = new System.Drawing.Size(112, 19);
            this.cmdChart.TabIndex = 67;
            this.cmdChart.Text = "QUICK CHART";
            this.cmdChart.UseVisualStyleBackColor = true;
            this.cmdChart.Click += new System.EventHandler(this.cmdChart_Click);
            // 
            // cmdViewAuction
            // 
            this.cmdViewAuction.Location = new System.Drawing.Point(248, 3);
            this.cmdViewAuction.Name = "cmdViewAuction";
            this.cmdViewAuction.Size = new System.Drawing.Size(112, 19);
            this.cmdViewAuction.TabIndex = 68;
            this.cmdViewAuction.Text = "VIEW AUCTION";
            this.cmdViewAuction.UseVisualStyleBackColor = true;
            this.cmdViewAuction.Click += new System.EventHandler(this.cmdViewAuction_Click);
            // 
            // cmdOptionChain
            // 
            this.cmdOptionChain.Location = new System.Drawing.Point(12, 3);
            this.cmdOptionChain.Name = "cmdOptionChain";
            this.cmdOptionChain.Size = new System.Drawing.Size(112, 19);
            this.cmdOptionChain.TabIndex = 69;
            this.cmdOptionChain.Text = "OPTION CHAIN";
            this.cmdOptionChain.UseVisualStyleBackColor = true;
            this.cmdOptionChain.Click += new System.EventHandler(this.cmdOptionChain_Click);
            // 
            // cmdEnableTrade
            // 
            this.cmdEnableTrade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdEnableTrade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdEnableTrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdEnableTrade.ForeColor = System.Drawing.Color.White;
            this.cmdEnableTrade.Location = new System.Drawing.Point(1141, 31);
            this.cmdEnableTrade.Name = "cmdEnableTrade";
            this.cmdEnableTrade.Size = new System.Drawing.Size(85, 47);
            this.cmdEnableTrade.TabIndex = 70;
            this.cmdEnableTrade.Text = "TRADING ENABLED";
            this.cmdEnableTrade.UseVisualStyleBackColor = false;
            this.cmdEnableTrade.Click += new System.EventHandler(this.cmdEnableTrade_Click);
            // 
            // cmdLimits
            // 
            this.cmdLimits.Location = new System.Drawing.Point(366, 3);
            this.cmdLimits.Name = "cmdLimits";
            this.cmdLimits.Size = new System.Drawing.Size(112, 19);
            this.cmdLimits.TabIndex = 72;
            this.cmdLimits.Text = "EDIT LIMITS";
            this.cmdLimits.UseVisualStyleBackColor = true;
            this.cmdLimits.Click += new System.EventHandler(this.cmdLimits_Click);
            // 
            // cmdWatchList
            // 
            this.cmdWatchList.Location = new System.Drawing.Point(484, 3);
            this.cmdWatchList.Name = "cmdWatchList";
            this.cmdWatchList.Size = new System.Drawing.Size(112, 19);
            this.cmdWatchList.TabIndex = 73;
            this.cmdWatchList.Text = "WATCHLIST";
            this.cmdWatchList.UseVisualStyleBackColor = true;
            this.cmdWatchList.Click += new System.EventHandler(this.cmdWatchList_Click);
            // 
            // cmdPNL
            // 
            this.cmdPNL.Location = new System.Drawing.Point(720, 3);
            this.cmdPNL.Name = "cmdPNL";
            this.cmdPNL.Size = new System.Drawing.Size(112, 19);
            this.cmdPNL.TabIndex = 74;
            this.cmdPNL.Text = "PNL";
            this.cmdPNL.UseVisualStyleBackColor = true;
            this.cmdPNL.Click += new System.EventHandler(this.cmdPNL_Click);
            // 
            // labNotInserted
            // 
            this.labNotInserted.AutoSize = true;
            this.labNotInserted.BackColor = System.Drawing.Color.Red;
            this.labNotInserted.ForeColor = System.Drawing.Color.White;
            this.labNotInserted.Location = new System.Drawing.Point(960, 13);
            this.labNotInserted.Name = "labNotInserted";
            this.labNotInserted.Size = new System.Drawing.Size(135, 13);
            this.labNotInserted.TabIndex = 75;
            this.labNotInserted.Text = "TRADES NOT INSERTED";
            this.labNotInserted.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBell_1);
            this.panel1.Controls.Add(this.lblBSE_1);
            this.panel1.Controls.Add(this.lblLinkBOV_1);
            this.panel1.Controls.Add(this.lblXPBOV_1);
            this.panel1.Controls.Add(this.lblBell_0);
            this.panel1.Controls.Add(this.lblBSE_0);
            this.panel1.Controls.Add(this.lblLinkBOV_0);
            this.panel1.Controls.Add(this.lblXPBOV_0);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(604, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 54);
            this.panel1.TabIndex = 80;
            // 
            // lblBell_1
            // 
            this.lblBell_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBell_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblBell_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBell_1.ForeColor = System.Drawing.Color.White;
            this.lblBell_1.Location = new System.Drawing.Point(200, 26);
            this.lblBell_1.Name = "lblBell_1";
            this.lblBell_1.Size = new System.Drawing.Size(65, 22);
            this.lblBell_1.TabIndex = 88;
            this.lblBell_1.Text = "0 ms";
            this.lblBell_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBSE_1
            // 
            this.lblBSE_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBSE_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblBSE_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBSE_1.ForeColor = System.Drawing.Color.White;
            this.lblBSE_1.Location = new System.Drawing.Point(134, 26);
            this.lblBSE_1.Name = "lblBSE_1";
            this.lblBSE_1.Size = new System.Drawing.Size(65, 22);
            this.lblBSE_1.TabIndex = 87;
            this.lblBSE_1.Text = "0 ms";
            this.lblBSE_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLinkBOV_1
            // 
            this.lblLinkBOV_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLinkBOV_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblLinkBOV_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinkBOV_1.ForeColor = System.Drawing.Color.White;
            this.lblLinkBOV_1.Location = new System.Drawing.Point(68, 26);
            this.lblLinkBOV_1.Name = "lblLinkBOV_1";
            this.lblLinkBOV_1.Size = new System.Drawing.Size(65, 22);
            this.lblLinkBOV_1.TabIndex = 86;
            this.lblLinkBOV_1.Text = "0 ms";
            this.lblLinkBOV_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblXPBOV_1
            // 
            this.lblXPBOV_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblXPBOV_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblXPBOV_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPBOV_1.ForeColor = System.Drawing.Color.White;
            this.lblXPBOV_1.Location = new System.Drawing.Point(2, 26);
            this.lblXPBOV_1.Name = "lblXPBOV_1";
            this.lblXPBOV_1.Size = new System.Drawing.Size(65, 22);
            this.lblXPBOV_1.TabIndex = 85;
            this.lblXPBOV_1.Text = "0 ms";
            this.lblXPBOV_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBell_0
            // 
            this.lblBell_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBell_0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblBell_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBell_0.ForeColor = System.Drawing.Color.White;
            this.lblBell_0.Location = new System.Drawing.Point(200, 3);
            this.lblBell_0.Name = "lblBell_0";
            this.lblBell_0.Size = new System.Drawing.Size(65, 22);
            this.lblBell_0.TabIndex = 84;
            this.lblBell_0.Text = "BELL";
            this.lblBell_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBSE_0
            // 
            this.lblBSE_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBSE_0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblBSE_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBSE_0.ForeColor = System.Drawing.Color.White;
            this.lblBSE_0.Location = new System.Drawing.Point(134, 3);
            this.lblBSE_0.Name = "lblBSE_0";
            this.lblBSE_0.Size = new System.Drawing.Size(65, 22);
            this.lblBSE_0.TabIndex = 83;
            this.lblBSE_0.Text = "PDIFF";
            this.lblBSE_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLinkBOV_0
            // 
            this.lblLinkBOV_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLinkBOV_0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblLinkBOV_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinkBOV_0.ForeColor = System.Drawing.Color.White;
            this.lblLinkBOV_0.Location = new System.Drawing.Point(68, 3);
            this.lblLinkBOV_0.Name = "lblLinkBOV_0";
            this.lblLinkBOV_0.Size = new System.Drawing.Size(65, 22);
            this.lblLinkBOV_0.TabIndex = 82;
            this.lblLinkBOV_0.Text = "Link Bov";
            this.lblLinkBOV_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblXPBOV_0
            // 
            this.lblXPBOV_0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblXPBOV_0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblXPBOV_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXPBOV_0.ForeColor = System.Drawing.Color.White;
            this.lblXPBOV_0.Location = new System.Drawing.Point(2, 3);
            this.lblXPBOV_0.Name = "lblXPBOV_0";
            this.lblXPBOV_0.Size = new System.Drawing.Size(65, 22);
            this.lblXPBOV_0.TabIndex = 81;
            this.lblXPBOV_0.Text = "XP Bov";
            this.lblXPBOV_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdMKTData
            // 
            this.cmdMKTData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdMKTData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMKTData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMKTData.ForeColor = System.Drawing.Color.White;
            this.cmdMKTData.Location = new System.Drawing.Point(875, 29);
            this.cmdMKTData.Name = "cmdMKTData";
            this.cmdMKTData.Size = new System.Drawing.Size(79, 47);
            this.cmdMKTData.TabIndex = 81;
            this.cmdMKTData.Text = "MKTDATA";
            this.cmdMKTData.UseVisualStyleBackColor = false;
            this.cmdMKTData.Click += new System.EventHandler(this.cmdMKTData_Click);
            // 
            // optAgg
            // 
            this.optAgg.AutoSize = true;
            this.optAgg.Location = new System.Drawing.Point(839, 6);
            this.optAgg.Name = "optAgg";
            this.optAgg.Size = new System.Drawing.Size(48, 17);
            this.optAgg.TabIndex = 82;
            this.optAgg.Text = "AGG";
            this.optAgg.UseVisualStyleBackColor = true;
            this.optAgg.CheckedChanged += new System.EventHandler(this.optCheck_CheckedChanged);
            // 
            // optCheck
            // 
            this.optCheck.AutoSize = true;
            this.optCheck.Checked = true;
            this.optCheck.Location = new System.Drawing.Point(888, 6);
            this.optCheck.Name = "optCheck";
            this.optCheck.Size = new System.Drawing.Size(56, 17);
            this.optCheck.TabIndex = 82;
            this.optCheck.TabStop = true;
            this.optCheck.Text = "Check";
            this.optCheck.UseVisualStyleBackColor = true;
            this.optCheck.CheckedChanged += new System.EventHandler(this.optCheck_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1391, 556);
            this.Controls.Add(this.optCheck);
            this.Controls.Add(this.optAgg);
            this.Controls.Add(this.cmdMKTData);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labNotInserted);
            this.Controls.Add(this.cmdPNL);
            this.Controls.Add(this.cmdWatchList);
            this.Controls.Add(this.cmdLimits);
            this.Controls.Add(this.cmdEnableTrade);
            this.Controls.Add(this.cmdOptionChain);
            this.Controls.Add(this.cmdViewAuction);
            this.Controls.Add(this.cmdChart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLastTime);
            this.Controls.Add(this.cmdSaveTickers);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.dtgLTQuotes);
            this.Controls.Add(this.cmdOrderReview);
            this.Controls.Add(this.lblFIXConnected);
            this.Controls.Add(this.lblFIXState);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = " LiveTrade 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStrongOpen_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLTQuotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLTQuotes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblFIXState;
        private System.Windows.Forms.Label lblFIXConnected;
        private System.Windows.Forms.Button cmdOrderReview;
        private NCustomControls.MyGridControl dtgLTQuotes;
        private NCustomControls.MyGridView dgLTQuotes;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.Button cmdSaveTickers;
        private System.Windows.Forms.CheckBox chkMTDChange;
        private System.Windows.Forms.CheckBox chkYTDChange;
        private System.Windows.Forms.Label lblLastTime;
        private System.Windows.Forms.CheckBox chkBidAsk;
        private System.Windows.Forms.CheckBox chkBidAskSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFIXSentNet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFIXSentGross;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFIXExecs;
        private System.Windows.Forms.Label lblFIXReplaces;
        private System.Windows.Forms.Button cmdChart;
        private System.Windows.Forms.Button cmdViewAuction;
        private System.Windows.Forms.Button cmdOptionChain;
        private System.Windows.Forms.Button cmdEnableTrade;
        private System.Windows.Forms.Button cmdLimits;
        private System.Windows.Forms.Button cmdWatchList;
        private System.Windows.Forms.Button cmdPNL;
        private System.Windows.Forms.Label labNotInserted;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBell_1;
        private System.Windows.Forms.Label lblBSE_1;
        private System.Windows.Forms.Label lblLinkBOV_1;
        private System.Windows.Forms.Label lblXPBOV_1;
        private System.Windows.Forms.Label lblBell_0;
        private System.Windows.Forms.Label lblBSE_0;
        private System.Windows.Forms.Label lblLinkBOV_0;
        private System.Windows.Forms.Label lblXPBOV_0;
        private System.Windows.Forms.Button cmdMKTData;
        private System.Windows.Forms.RadioButton optAgg;
        private System.Windows.Forms.RadioButton optCheck;
    }
}

