namespace LBHistCalc
{
    partial class frmHistCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistCalc));
            this.dtgLinesToCalc = new NCustomControls.MyGridControl();
            this.dgLinesToCalc = new NCustomControls.MyGridView();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.tmrSavePos = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblTimeLoadData = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblTimeVol = new System.Windows.Forms.Label();
            this.lblTimeRate = new System.Windows.Forms.Label();
            this.lblTimePrice = new System.Windows.Forms.Label();
            this.lblHitsRate = new System.Windows.Forms.Label();
            this.lblHitsVol = new System.Windows.Forms.Label();
            this.lblHitsPrice = new System.Windows.Forms.Label();
            this.lblPorts = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdFromDate = new System.Windows.Forms.Button();
            this.tmrClose = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLinesToCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinesToCalc)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgLinesToCalc
            // 
            this.dtgLinesToCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgLinesToCalc.Location = new System.Drawing.Point(15, 12);
            this.dtgLinesToCalc.LookAndFeel.SkinName = "Blue";
            this.dtgLinesToCalc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgLinesToCalc.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgLinesToCalc.MainView = this.dgLinesToCalc;
            this.dtgLinesToCalc.Name = "dtgLinesToCalc";
            this.dtgLinesToCalc.Size = new System.Drawing.Size(879, 365);
            this.dtgLinesToCalc.TabIndex = 27;
            this.dtgLinesToCalc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgLinesToCalc});
            // 
            // dgLinesToCalc
            // 
            this.dgLinesToCalc.GridControl = this.dtgLinesToCalc;
            this.dgLinesToCalc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgLinesToCalc.Name = "dgLinesToCalc";
            this.dgLinesToCalc.OptionsBehavior.Editable = false;
            this.dgLinesToCalc.OptionsSelection.MultiSelect = true;
            this.dgLinesToCalc.OptionsView.ColumnAutoWidth = false;
            this.dgLinesToCalc.OptionsView.ShowGroupPanel = false;
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMessages.Location = new System.Drawing.Point(15, 384);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(538, 177);
            this.txtMessages.TabIndex = 29;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 500;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // tmrSavePos
            // 
            this.tmrSavePos.Interval = 5000;
            this.tmrSavePos.Tick += new System.EventHandler(this.tmrSavePos_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lblTimeLoadData);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.lblTimeVol);
            this.groupBox1.Controls.Add(this.lblTimeRate);
            this.groupBox1.Controls.Add(this.lblTimePrice);
            this.groupBox1.Controls.Add(this.lblHitsRate);
            this.groupBox1.Controls.Add(this.lblHitsVol);
            this.groupBox1.Controls.Add(this.lblHitsPrice);
            this.groupBox1.Controls.Add(this.lblPorts);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(559, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 152);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Buffers";
            // 
            // lblTimeLoadData
            // 
            this.lblTimeLoadData.AutoSize = true;
            this.lblTimeLoadData.Location = new System.Drawing.Point(256, 94);
            this.lblTimeLoadData.Name = "lblTimeLoadData";
            this.lblTimeLoadData.Size = new System.Drawing.Size(49, 13);
            this.lblTimeLoadData.TabIndex = 18;
            this.lblTimeLoadData.Text = "00:00:00";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(121, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 17;
            this.label16.Text = "- / -";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(17, 94);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 13);
            this.label17.TabIndex = 16;
            this.label17.Text = "Load Data";
            // 
            // lblTimeVol
            // 
            this.lblTimeVol.AutoSize = true;
            this.lblTimeVol.Location = new System.Drawing.Point(256, 78);
            this.lblTimeVol.Name = "lblTimeVol";
            this.lblTimeVol.Size = new System.Drawing.Size(49, 13);
            this.lblTimeVol.TabIndex = 15;
            this.lblTimeVol.Text = "00:00:00";
            // 
            // lblTimeRate
            // 
            this.lblTimeRate.AutoSize = true;
            this.lblTimeRate.Location = new System.Drawing.Point(256, 62);
            this.lblTimeRate.Name = "lblTimeRate";
            this.lblTimeRate.Size = new System.Drawing.Size(49, 13);
            this.lblTimeRate.TabIndex = 14;
            this.lblTimeRate.Text = "00:00:00";
            // 
            // lblTimePrice
            // 
            this.lblTimePrice.AutoSize = true;
            this.lblTimePrice.Location = new System.Drawing.Point(256, 46);
            this.lblTimePrice.Name = "lblTimePrice";
            this.lblTimePrice.Size = new System.Drawing.Size(49, 13);
            this.lblTimePrice.TabIndex = 11;
            this.lblTimePrice.Text = "00:00:00";
            // 
            // lblHitsRate
            // 
            this.lblHitsRate.AutoSize = true;
            this.lblHitsRate.Location = new System.Drawing.Point(121, 62);
            this.lblHitsRate.Name = "lblHitsRate";
            this.lblHitsRate.Size = new System.Drawing.Size(30, 13);
            this.lblHitsRate.TabIndex = 10;
            this.lblHitsRate.Text = "0 / 0";
            // 
            // lblHitsVol
            // 
            this.lblHitsVol.AutoSize = true;
            this.lblHitsVol.Location = new System.Drawing.Point(121, 78);
            this.lblHitsVol.Name = "lblHitsVol";
            this.lblHitsVol.Size = new System.Drawing.Size(30, 13);
            this.lblHitsVol.TabIndex = 9;
            this.lblHitsVol.Text = "0 / 0";
            // 
            // lblHitsPrice
            // 
            this.lblHitsPrice.AutoSize = true;
            this.lblHitsPrice.Location = new System.Drawing.Point(121, 46);
            this.lblHitsPrice.Name = "lblHitsPrice";
            this.lblHitsPrice.Size = new System.Drawing.Size(30, 13);
            this.lblHitsPrice.TabIndex = 8;
            this.lblHitsPrice.Text = "0 / 0";
            // 
            // lblPorts
            // 
            this.lblPorts.AutoSize = true;
            this.lblPorts.Location = new System.Drawing.Point(122, 120);
            this.lblPorts.Name = "lblPorts";
            this.lblPorts.Size = new System.Drawing.Size(13, 13);
            this.lblPorts.TabIndex = 7;
            this.lblPorts.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Get Vol";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Get Rate";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Get Price";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ports Calculated:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(247, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Total Time";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(122, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hit / Miss";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buffer";
            // 
            // cmdFromDate
            // 
            this.cmdFromDate.Location = new System.Drawing.Point(764, 543);
            this.cmdFromDate.Name = "cmdFromDate";
            this.cmdFromDate.Size = new System.Drawing.Size(130, 23);
            this.cmdFromDate.TabIndex = 31;
            this.cmdFromDate.Text = "Calc From Date";
            this.cmdFromDate.UseVisualStyleBackColor = true;
            this.cmdFromDate.Click += new System.EventHandler(this.cmdFromDate_Click);
            // 
            // tmrClose
            // 
            this.tmrClose.Enabled = true;
            this.tmrClose.Interval = 500;
            this.tmrClose.Tick += new System.EventHandler(this.tmrClose_Tick);
            // 
            // frmHistCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(906, 580);
            this.Controls.Add(this.cmdFromDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.dtgLinesToCalc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHistCalc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historical Positions Calculator 2.0  Started 21/10/2013 09:24";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmHistCalc_FormClosed);
            this.Load += new System.EventHandler(this.frmHistCalc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLinesToCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinesToCalc)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NCustomControls.MyGridControl dtgLinesToCalc;
        private NCustomControls.MyGridView dgLinesToCalc;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Timer tmrSavePos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHitsPrice;
        private System.Windows.Forms.Label lblPorts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTimeLoadData;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblTimeVol;
        private System.Windows.Forms.Label lblTimeRate;
        private System.Windows.Forms.Label lblTimePrice;
        private System.Windows.Forms.Label lblHitsRate;
        private System.Windows.Forms.Label lblHitsVol;
        private System.Windows.Forms.Button cmdFromDate;
        public System.Windows.Forms.Timer tmrClose;
    }
}

