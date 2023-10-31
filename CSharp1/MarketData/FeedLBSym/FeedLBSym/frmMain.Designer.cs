namespace FeedLBSym
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.lblTickerCount = new System.Windows.Forms.Label();
            this.tmrUpdateDB = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeInterval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.tmrReloadTickers = new System.Windows.Forms.Timer(this.components);
            this.cmdResubscribe = new System.Windows.Forms.Button();
            this.cmdReloadTickers = new System.Windows.Forms.Button();
            this.lblConnected = new System.Windows.Forms.Label();
            this.tmrClose = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(40, 98);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(98, 36);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Enabled = false;
            this.cmdStop.Location = new System.Drawing.Point(174, 98);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(98, 36);
            this.cmdStop.TabIndex = 1;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // lblTickerCount
            // 
            this.lblTickerCount.AutoSize = true;
            this.lblTickerCount.Location = new System.Drawing.Point(114, 59);
            this.lblTickerCount.Name = "lblTickerCount";
            this.lblTickerCount.Size = new System.Drawing.Size(13, 13);
            this.lblTickerCount.TabIndex = 2;
            this.lblTickerCount.Text = "0";
            // 
            // tmrUpdateDB
            // 
            this.tmrUpdateDB.Interval = 10000;
            this.tmrUpdateDB.Tick += new System.EventHandler(this.tmrUpdateDB_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(80, 31);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Stopped";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tickers Loaded:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Timer Interval:";
            // 
            // txtTimeInterval
            // 
            this.txtTimeInterval.Location = new System.Drawing.Point(232, 31);
            this.txtTimeInterval.Name = "txtTimeInterval";
            this.txtTimeInterval.Size = new System.Drawing.Size(62, 20);
            this.txtTimeInterval.TabIndex = 7;
            this.txtTimeInterval.Text = "1000";
            this.txtTimeInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimeInterval_KeyPress);
            this.txtTimeInterval.Leave += new System.EventHandler(this.txtTimeInterval_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Last Update:";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Location = new System.Drawing.Point(229, 59);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(65, 13);
            this.lblLastUpdate.TabIndex = 8;
            this.lblLastUpdate.Text = "01/01/1900";
            // 
            // tmrReloadTickers
            // 
            this.tmrReloadTickers.Enabled = true;
            this.tmrReloadTickers.Interval = 300000;
            this.tmrReloadTickers.Tick += new System.EventHandler(this.tmrReloadTickers_Tick);
            // 
            // cmdResubscribe
            // 
            this.cmdResubscribe.Location = new System.Drawing.Point(40, 140);
            this.cmdResubscribe.Name = "cmdResubscribe";
            this.cmdResubscribe.Size = new System.Drawing.Size(98, 36);
            this.cmdResubscribe.TabIndex = 10;
            this.cmdResubscribe.Text = "ReSubscribe";
            this.cmdResubscribe.UseVisualStyleBackColor = true;
            this.cmdResubscribe.Click += new System.EventHandler(this.cmdResubscribe_Click);
            // 
            // cmdReloadTickers
            // 
            this.cmdReloadTickers.Location = new System.Drawing.Point(174, 140);
            this.cmdReloadTickers.Name = "cmdReloadTickers";
            this.cmdReloadTickers.Size = new System.Drawing.Size(98, 36);
            this.cmdReloadTickers.TabIndex = 11;
            this.cmdReloadTickers.Text = "Reload Tickers";
            this.cmdReloadTickers.UseVisualStyleBackColor = true;
            this.cmdReloadTickers.Click += new System.EventHandler(this.cmdReloadTickers_Click);
            // 
            // lblConnected
            // 
            this.lblConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnected.AutoSize = true;
            this.lblConnected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnected.ForeColor = System.Drawing.Color.White;
            this.lblConnected.Location = new System.Drawing.Point(105, 192);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(102, 16);
            this.lblConnected.TabIndex = 43;
            this.lblConnected.Text = "CONNECTED";
            // 
            // tmrClose
            // 
            this.tmrClose.Enabled = true;
            this.tmrClose.Interval = 120000;
            this.tmrClose.Tick += new System.EventHandler(this.tmrClose_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(313, 226);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.cmdReloadTickers);
            this.Controls.Add(this.cmdResubscribe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.txtTimeInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTickerCount);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(500, 100);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "NestSYM LB Feed";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Label lblTickerCount;
        private System.Windows.Forms.Timer tmrUpdateDB;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Timer tmrReloadTickers;
        private System.Windows.Forms.Button cmdResubscribe;
        private System.Windows.Forms.Button cmdReloadTickers;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Timer tmrClose;
    }
}

