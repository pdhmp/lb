namespace JMStrategy
{
    partial class frmJMStrategy
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSendOrders = new System.Windows.Forms.Button();
            this.dgcClosedSec = new NCustomControls.MyGridControl();
            this.dgvClosedSec = new NCustomControls.MyGridView();
            this.dgcOrders = new NCustomControls.MyGridControl();
            this.dgvOrders = new NCustomControls.MyGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgcLast2 = new DevExpress.XtraGrid.GridControl();
            this.dgvLast2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dgcLast1 = new DevExpress.XtraGrid.GridControl();
            this.dgvLast1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblDays = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgcSecurity2 = new NCustomControls.MyGridControl();
            this.dgvSecurity2 = new NCustomControls.MyGridView();
            this.dgcSecurity1 = new NCustomControls.MyGridControl();
            this.dgvSecurity1 = new NCustomControls.MyGridView();
            this.lblSection = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstModels = new System.Windows.Forms.ListBox();
            this.lblIdSection = new System.Windows.Forms.Label();
            this.lblIdModel = new System.Windows.Forms.Label();
            this.lblTriggerIn = new System.Windows.Forms.Label();
            this.lblTriggerOut = new System.Windows.Forms.Label();
            this.lblMultiplier = new System.Windows.Forms.Label();
            this.lblLimiter = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgcPairs = new NCustomControls.MyGridControl();
            this.dgvPairs = new NCustomControls.MyGridView();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnSell100 = new System.Windows.Forms.Button();
            this.btnBuy100 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcClosedSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClosedSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcLast2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLast2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcLast1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLast1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurity2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcPairs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPairs)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1131, 685);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.btnBuy100);
            this.tabPage1.Controls.Add(this.btnSell100);
            this.tabPage1.Controls.Add(this.btnSendOrders);
            this.tabPage1.Controls.Add(this.dgcClosedSec);
            this.tabPage1.Controls.Add(this.dgcOrders);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1123, 659);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Orders";
            // 
            // btnSendOrders
            // 
            this.btnSendOrders.Location = new System.Drawing.Point(490, 348);
            this.btnSendOrders.Name = "btnSendOrders";
            this.btnSendOrders.Size = new System.Drawing.Size(238, 74);
            this.btnSendOrders.TabIndex = 2;
            this.btnSendOrders.Text = "Send All Orders";
            this.btnSendOrders.UseVisualStyleBackColor = true;
            this.btnSendOrders.Click += new System.EventHandler(this.btnSendOrders_Click);
            // 
            // dgcClosedSec
            // 
            this.dgcClosedSec.Location = new System.Drawing.Point(490, 6);
            this.dgcClosedSec.LookAndFeel.SkinName = "Blue";
            this.dgcClosedSec.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcClosedSec.MainView = this.dgvClosedSec;
            this.dgcClosedSec.Name = "dgcClosedSec";
            this.dgcClosedSec.Size = new System.Drawing.Size(570, 311);
            this.dgcClosedSec.TabIndex = 1;
            this.dgcClosedSec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvClosedSec});
            this.dgcClosedSec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcClosedSec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvClosedSec
            // 
            this.dgvClosedSec.GridControl = this.dgcClosedSec;
            this.dgvClosedSec.Name = "dgvClosedSec";
            this.dgvClosedSec.OptionsBehavior.Editable = false;
            this.dgvClosedSec.Click += new System.EventHandler(this.dgvClosedSec_Click);
            // 
            // dgcOrders
            // 
            this.dgcOrders.Location = new System.Drawing.Point(8, 6);
            this.dgcOrders.LookAndFeel.SkinName = "Blue";
            this.dgcOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcOrders.LookAndFeel.UseWindowsXPTheme = true;
            this.dgcOrders.MainView = this.dgvOrders;
            this.dgcOrders.Name = "dgcOrders";
            this.dgcOrders.Size = new System.Drawing.Size(400, 619);
            this.dgcOrders.TabIndex = 0;
            this.dgcOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvOrders});
            this.dgcOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcOrders.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvOrders
            // 
            this.dgvOrders.GridControl = this.dgcOrders;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.OptionsBehavior.Editable = false;
            this.dgvOrders.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.dgcLast2);
            this.tabPage2.Controls.Add(this.dgcLast1);
            this.tabPage2.Controls.Add(this.lblDays);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.dgcSecurity2);
            this.tabPage2.Controls.Add(this.dgcSecurity1);
            this.tabPage2.Controls.Add(this.lblSection);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.lstModels);
            this.tabPage2.Controls.Add(this.lblIdSection);
            this.tabPage2.Controls.Add(this.lblIdModel);
            this.tabPage2.Controls.Add(this.lblTriggerIn);
            this.tabPage2.Controls.Add(this.lblTriggerOut);
            this.tabPage2.Controls.Add(this.lblMultiplier);
            this.tabPage2.Controls.Add(this.lblLimiter);
            this.tabPage2.Controls.Add(this.lblSize);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.dgcPairs);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1123, 659);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Model";
            // 
            // dgcLast2
            // 
            this.dgcLast2.Location = new System.Drawing.Point(927, 387);
            this.dgcLast2.MainView = this.dgvLast2;
            this.dgcLast2.Name = "dgcLast2";
            this.dgcLast2.Size = new System.Drawing.Size(186, 251);
            this.dgcLast2.TabIndex = 27;
            this.dgcLast2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvLast2});
            // 
            // dgvLast2
            // 
            this.dgvLast2.GridControl = this.dgcLast2;
            this.dgvLast2.Name = "dgvLast2";
            this.dgvLast2.OptionsView.ShowGroupPanel = false;
            // 
            // dgcLast1
            // 
            this.dgcLast1.Location = new System.Drawing.Point(489, 387);
            this.dgcLast1.MainView = this.dgvLast1;
            this.dgcLast1.Name = "dgcLast1";
            this.dgcLast1.Size = new System.Drawing.Size(186, 251);
            this.dgcLast1.TabIndex = 26;
            this.dgcLast1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvLast1});
            // 
            // dgvLast1
            // 
            this.dgvLast1.GridControl = this.dgcLast1;
            this.dgvLast1.Name = "dgvLast1";
            this.dgvLast1.OptionsView.ShowGroupPanel = false;
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.Location = new System.Drawing.Point(110, 235);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(31, 13);
            this.lblDays.TabIndex = 25;
            this.lblDays.Text = "Days";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Days";
            // 
            // dgcSecurity2
            // 
            this.dgcSecurity2.Location = new System.Drawing.Point(696, 387);
            this.dgcSecurity2.LookAndFeel.SkinName = "Blue";
            this.dgcSecurity2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcSecurity2.MainView = this.dgvSecurity2;
            this.dgcSecurity2.Name = "dgcSecurity2";
            this.dgcSecurity2.Size = new System.Drawing.Size(213, 251);
            this.dgcSecurity2.TabIndex = 23;
            this.dgcSecurity2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSecurity2});
            this.dgcSecurity2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcSecurity2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvSecurity2
            // 
            this.dgvSecurity2.GridControl = this.dgcSecurity2;
            this.dgvSecurity2.Name = "dgvSecurity2";
            this.dgvSecurity2.OptionsView.ShowGroupPanel = false;
            this.dgvSecurity2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgvSecurity2_FocusedRowChanged);
            // 
            // dgcSecurity1
            // 
            this.dgcSecurity1.Location = new System.Drawing.Point(259, 387);
            this.dgcSecurity1.LookAndFeel.SkinName = "Blue";
            this.dgcSecurity1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcSecurity1.MainView = this.dgvSecurity1;
            this.dgcSecurity1.Name = "dgcSecurity1";
            this.dgcSecurity1.Size = new System.Drawing.Size(213, 251);
            this.dgcSecurity1.TabIndex = 22;
            this.dgcSecurity1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSecurity1});
            this.dgcSecurity1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcSecurity1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvSecurity1
            // 
            this.dgvSecurity1.GridControl = this.dgcSecurity1;
            this.dgvSecurity1.Name = "dgvSecurity1";
            this.dgvSecurity1.OptionsView.ShowGroupPanel = false;
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(110, 41);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(27, 13);
            this.lblSection.TabIndex = 21;
            this.lblSection.Text = "Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Section";
            // 
            // lstModels
            // 
            this.lstModels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstModels.FormattingEnabled = true;
            this.lstModels.Location = new System.Drawing.Point(30, 387);
            this.lstModels.Name = "lstModels";
            this.lstModels.Size = new System.Drawing.Size(210, 251);
            this.lstModels.TabIndex = 19;
            this.lstModels.SelectedIndexChanged += new System.EventHandler(this.lstModels_SelectedIndexChanged);
            // 
            // lblIdSection
            // 
            this.lblIdSection.AutoSize = true;
            this.lblIdSection.Location = new System.Drawing.Point(110, 63);
            this.lblIdSection.Name = "lblIdSection";
            this.lblIdSection.Size = new System.Drawing.Size(27, 13);
            this.lblIdSection.TabIndex = 18;
            this.lblIdSection.Text = "Size";
            // 
            // lblIdModel
            // 
            this.lblIdModel.AutoSize = true;
            this.lblIdModel.Location = new System.Drawing.Point(110, 86);
            this.lblIdModel.Name = "lblIdModel";
            this.lblIdModel.Size = new System.Drawing.Size(27, 13);
            this.lblIdModel.TabIndex = 17;
            this.lblIdModel.Text = "Size";
            // 
            // lblTriggerIn
            // 
            this.lblTriggerIn.AutoSize = true;
            this.lblTriggerIn.Location = new System.Drawing.Point(110, 109);
            this.lblTriggerIn.Name = "lblTriggerIn";
            this.lblTriggerIn.Size = new System.Drawing.Size(27, 13);
            this.lblTriggerIn.TabIndex = 16;
            this.lblTriggerIn.Text = "Size";
            // 
            // lblTriggerOut
            // 
            this.lblTriggerOut.AutoSize = true;
            this.lblTriggerOut.Location = new System.Drawing.Point(110, 134);
            this.lblTriggerOut.Name = "lblTriggerOut";
            this.lblTriggerOut.Size = new System.Drawing.Size(27, 13);
            this.lblTriggerOut.TabIndex = 15;
            this.lblTriggerOut.Text = "Size";
            // 
            // lblMultiplier
            // 
            this.lblMultiplier.AutoSize = true;
            this.lblMultiplier.Location = new System.Drawing.Point(110, 159);
            this.lblMultiplier.Name = "lblMultiplier";
            this.lblMultiplier.Size = new System.Drawing.Size(27, 13);
            this.lblMultiplier.TabIndex = 14;
            this.lblMultiplier.Text = "Size";
            // 
            // lblLimiter
            // 
            this.lblLimiter.AutoSize = true;
            this.lblLimiter.Location = new System.Drawing.Point(110, 183);
            this.lblLimiter.Name = "lblLimiter";
            this.lblLimiter.Size = new System.Drawing.Size(27, 13);
            this.lblLimiter.TabIndex = 13;
            this.lblLimiter.Text = "Size";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(110, 209);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(27, 13);
            this.lblSize.TabIndex = 12;
            this.lblSize.Text = "Size";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Id Model";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Trigger In";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Trigger Out";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Multiplier";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Limiter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Id Section";
            // 
            // dgcPairs
            // 
            this.dgcPairs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgcPairs.Location = new System.Drawing.Point(259, 6);
            this.dgcPairs.LookAndFeel.SkinName = "Blue";
            this.dgcPairs.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcPairs.MainView = this.dgvPairs;
            this.dgcPairs.Name = "dgcPairs";
            this.dgcPairs.Size = new System.Drawing.Size(854, 348);
            this.dgcPairs.TabIndex = 1;
            this.dgcPairs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvPairs});
            this.dgcPairs.Click += new System.EventHandler(this.dgcPairs_Click);
            this.dgcPairs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcPairs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvPairs
            // 
            this.dgvPairs.GridControl = this.dgcPairs;
            this.dgvPairs.Name = "dgvPairs";
            this.dgvPairs.OptionsBehavior.Editable = false;
            this.dgvPairs.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgvPairs_FocusedRowChanged);
            this.dgvPairs.Click += new System.EventHandler(this.dgvPairs_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnSell100
            // 
            this.btnSell100.Location = new System.Drawing.Point(867, 348);
            this.btnSell100.Name = "btnSell100";
            this.btnSell100.Size = new System.Drawing.Size(99, 23);
            this.btnSell100.TabIndex = 3;
            this.btnSell100.Text = "Sell 100 shares";
            this.btnSell100.UseVisualStyleBackColor = true;
            this.btnSell100.Click += new System.EventHandler(this.btnSell100_Click);
            // 
            // btnBuy100
            // 
            this.btnBuy100.Location = new System.Drawing.Point(867, 399);
            this.btnBuy100.Name = "btnBuy100";
            this.btnBuy100.Size = new System.Drawing.Size(99, 23);
            this.btnBuy100.TabIndex = 4;
            this.btnBuy100.Text = "Buy 100 Shares";
            this.btnBuy100.UseVisualStyleBackColor = true;
            this.btnBuy100.Click += new System.EventHandler(this.btnBuy100_Click);
            // 
            // frmJMStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1130, 685);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmJMStrategy";
            this.Text = "JM Strategy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgcClosedSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClosedSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgcLast2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLast2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcLast1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLast1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurity2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcPairs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPairs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private NCustomControls.MyGridControl dgcPairs;
        private NCustomControls.MyGridView dgvPairs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIdSection;
        private System.Windows.Forms.Label lblIdModel;
        private System.Windows.Forms.Label lblTriggerIn;
        private System.Windows.Forms.Label lblTriggerOut;
        private System.Windows.Forms.Label lblMultiplier;
        private System.Windows.Forms.Label lblLimiter;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.ListBox lstModels;
        private NCustomControls.MyGridControl dgcOrders;
        private NCustomControls.MyGridView  dgvOrders;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label label4;
        private NCustomControls.MyGridControl dgcClosedSec;
        private NCustomControls.MyGridView dgvClosedSec;
        private NCustomControls.MyGridControl dgcSecurity2;
        private NCustomControls.MyGridView dgvSecurity2;
        private NCustomControls.MyGridControl dgcSecurity1;
        private NCustomControls.MyGridView dgvSecurity1;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSendOrders;
        private DevExpress.XtraGrid.GridControl dgcLast2;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvLast2;
        private DevExpress.XtraGrid.GridControl dgcLast1;
        private DevExpress.XtraGrid.Views.Grid.GridView dgvLast1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnBuy100;
        private System.Windows.Forms.Button btnSell100;
    }
}

