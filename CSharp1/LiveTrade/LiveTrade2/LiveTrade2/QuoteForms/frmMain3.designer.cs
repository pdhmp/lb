namespace LiveTrade2
{
    partial class frmMain3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain3));
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblFIXState = new System.Windows.Forms.Label();
            this.lblFIXConnected = new System.Windows.Forms.Label();
            this.cmdOrderReview = new System.Windows.Forms.Button();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.cmdSaveTickers = new System.Windows.Forms.Button();
            this.tmrBestFitCols = new System.Windows.Forms.Timer(this.components);
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
            this.cmdMKTData = new System.Windows.Forms.Button();
            this.dgLTQuotes = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLTQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 350;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(1240, 53);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(135, 19);
            this.cmdCancel.TabIndex = 40;
            this.cmdCancel.Text = "CANCEL ALL ORDERS";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // lblFIXState
            // 
            this.lblFIXState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFIXState.AutoSize = true;
            this.lblFIXState.BackColor = System.Drawing.Color.Red;
            this.lblFIXState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFIXState.ForeColor = System.Drawing.Color.White;
            this.lblFIXState.Location = new System.Drawing.Point(1237, 9);
            this.lblFIXState.Name = "lblFIXState";
            this.lblFIXState.Size = new System.Drawing.Size(84, 16);
            this.lblFIXState.TabIndex = 29;
            this.lblFIXState.Text = "FIX STATE";
            // 
            // lblFIXConnected
            // 
            this.lblFIXConnected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFIXConnected.AutoSize = true;
            this.lblFIXConnected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblFIXConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFIXConnected.ForeColor = System.Drawing.Color.White;
            this.lblFIXConnected.Location = new System.Drawing.Point(1237, 31);
            this.lblFIXConnected.Name = "lblFIXConnected";
            this.lblFIXConnected.Size = new System.Drawing.Size(146, 16);
            this.lblFIXConnected.TabIndex = 42;
            this.lblFIXConnected.Text = "FIX IS CONNECTED";
            this.lblFIXConnected.Visible = false;
            // 
            // cmdOrderReview
            // 
            this.cmdOrderReview.Location = new System.Drawing.Point(12, 3);
            this.cmdOrderReview.Name = "cmdOrderReview";
            this.cmdOrderReview.Size = new System.Drawing.Size(112, 19);
            this.cmdOrderReview.TabIndex = 56;
            this.cmdOrderReview.Text = "ORDER REVIEW";
            this.cmdOrderReview.UseVisualStyleBackColor = true;
            this.cmdOrderReview.Click += new System.EventHandler(this.cmdOrderReview_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(730, 49);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(112, 19);
            this.cmdEdit.TabIndex = 58;
            this.cmdEdit.Text = "Edit Grid";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdSaveTickers
            // 
            this.cmdSaveTickers.Location = new System.Drawing.Point(848, 49);
            this.cmdSaveTickers.Name = "cmdSaveTickers";
            this.cmdSaveTickers.Size = new System.Drawing.Size(112, 19);
            this.cmdSaveTickers.TabIndex = 59;
            this.cmdSaveTickers.Text = "Save Grid";
            this.cmdSaveTickers.UseVisualStyleBackColor = true;
            this.cmdSaveTickers.Click += new System.EventHandler(this.cmdSaveTickers_Click);
            // 
            // tmrBestFitCols
            // 
            this.tmrBestFitCols.Interval = 300000;
            this.tmrBestFitCols.Tick += new System.EventHandler(this.tmrBestFitCols_Tick);
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
            this.lblLastTime.Location = new System.Drawing.Point(1368, 33);
            this.lblLastTime.Name = "lblLastTime";
            this.lblLastTime.Size = new System.Drawing.Size(13, 13);
            this.lblLastTime.TabIndex = 62;
            this.lblLastTime.Text = "0";
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
            this.groupBox1.Location = new System.Drawing.Point(252, 3);
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
            this.groupBox2.Location = new System.Drawing.Point(460, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 65);
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
            this.lblFIXSentNet.Location = new System.Drawing.Point(195, 38);
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
            this.lblFIXSentGross.Location = new System.Drawing.Point(195, 14);
            this.lblFIXSentGross.Name = "lblFIXSentGross";
            this.lblFIXSentGross.Size = new System.Drawing.Size(13, 13);
            this.lblFIXSentGross.TabIndex = 59;
            this.lblFIXSentGross.Text = "0";
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
            this.cmdChart.Location = new System.Drawing.Point(12, 28);
            this.cmdChart.Name = "cmdChart";
            this.cmdChart.Size = new System.Drawing.Size(112, 19);
            this.cmdChart.TabIndex = 67;
            this.cmdChart.Text = "QUICK CHART";
            this.cmdChart.UseVisualStyleBackColor = true;
            this.cmdChart.Click += new System.EventHandler(this.cmdChart_Click);
            // 
            // cmdViewAuction
            // 
            this.cmdViewAuction.Location = new System.Drawing.Point(12, 53);
            this.cmdViewAuction.Name = "cmdViewAuction";
            this.cmdViewAuction.Size = new System.Drawing.Size(112, 19);
            this.cmdViewAuction.TabIndex = 68;
            this.cmdViewAuction.Text = "VIEW AUCTION";
            this.cmdViewAuction.UseVisualStyleBackColor = true;
            this.cmdViewAuction.Click += new System.EventHandler(this.cmdViewAuction_Click);
            // 
            // cmdOptionChain
            // 
            this.cmdOptionChain.Location = new System.Drawing.Point(130, 3);
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
            this.cmdEnableTrade.Location = new System.Drawing.Point(731, 8);
            this.cmdEnableTrade.Name = "cmdEnableTrade";
            this.cmdEnableTrade.Size = new System.Drawing.Size(144, 31);
            this.cmdEnableTrade.TabIndex = 70;
            this.cmdEnableTrade.Text = "TRADING ENABLED";
            this.cmdEnableTrade.UseVisualStyleBackColor = false;
            this.cmdEnableTrade.Click += new System.EventHandler(this.cmdEnableTrade_Click);
            // 
            // cmdMKTData
            // 
            this.cmdMKTData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.cmdMKTData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdMKTData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMKTData.ForeColor = System.Drawing.Color.White;
            this.cmdMKTData.Location = new System.Drawing.Point(881, 8);
            this.cmdMKTData.Name = "cmdMKTData";
            this.cmdMKTData.Size = new System.Drawing.Size(79, 31);
            this.cmdMKTData.TabIndex = 71;
            this.cmdMKTData.Text = "MKTDATA";
            this.cmdMKTData.UseVisualStyleBackColor = false;
            this.cmdMKTData.Click += new System.EventHandler(this.cmdMKTData_Click);
            // 
            // dgLTQuotes
            // 
            this.dgLTQuotes.AllowUserToAddRows = false;
            this.dgLTQuotes.AllowUserToDeleteRows = false;
            this.dgLTQuotes.AllowUserToResizeRows = false;
            this.dgLTQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLTQuotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLTQuotes.Location = new System.Drawing.Point(13, 79);
            this.dgLTQuotes.Name = "dgLTQuotes";
            this.dgLTQuotes.RowHeadersVisible = false;
            this.dgLTQuotes.Size = new System.Drawing.Size(1370, 619);
            this.dgLTQuotes.TabIndex = 72;
            this.dgLTQuotes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgLTQuotes_CellFormatting);
            // 
            // frmMain3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1395, 710);
            this.Controls.Add(this.dgLTQuotes);
            this.Controls.Add(this.cmdMKTData);
            this.Controls.Add(this.cmdEnableTrade);
            this.Controls.Add(this.cmdOptionChain);
            this.Controls.Add(this.cmdViewAuction);
            this.Controls.Add(this.cmdChart);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLastTime);
            this.Controls.Add(this.cmdSaveTickers);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdOrderReview);
            this.Controls.Add(this.lblFIXConnected);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lblFIXState);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain3";
            this.Text = "LiveTrade 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStrongOpen_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLTQuotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label lblFIXState;
        private System.Windows.Forms.Label lblFIXConnected;
        private System.Windows.Forms.Button cmdOrderReview;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.Button cmdSaveTickers;
        private System.Windows.Forms.Timer tmrBestFitCols;
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
        private System.Windows.Forms.Button cmdMKTData;
        private System.Windows.Forms.DataGridView dgLTQuotes;
    }
}

