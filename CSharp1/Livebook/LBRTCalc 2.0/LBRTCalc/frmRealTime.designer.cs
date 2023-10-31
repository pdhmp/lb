using System;
namespace LBRTCalc
{
    partial class frmRealTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRealTime));
            this.labLastMktData = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.tmrUpdateScreen = new System.Windows.Forms.Timer(this.components);
            this.labRecalcAll = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labQuantityCheck = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labStarted = new System.Windows.Forms.Label();
            this.btnMKTData = new System.Windows.Forms.Button();
            this.lblRecalcAllTimeTaken = new System.Windows.Forms.Label();
            this.lblRTQuantityTimeTaken = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labAllFields = new System.Windows.Forms.Label();
            this.labRTFields = new System.Windows.Forms.Label();
            this.labQTFields = new System.Windows.Forms.Label();
            this.btnChangeFeed = new System.Windows.Forms.Button();
            this.labMktDataQueue = new System.Windows.Forms.Label();
            this.tmrStart = new System.Windows.Forms.Timer(this.components);
            this.labSQLInsertQueue = new System.Windows.Forms.Label();
            this.txtLogView = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.lblCurrentFeed = new System.Windows.Forms.Label();
            this.tmrClose = new System.Windows.Forms.Timer(this.components);
            this.chkProcessMktData = new System.Windows.Forms.CheckBox();
            this.bntRecalc = new System.Windows.Forms.Button();
            this.tmrTurnMktData = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labLastMktData
            // 
            this.labLastMktData.AutoSize = true;
            this.labLastMktData.Location = new System.Drawing.Point(170, 9);
            this.labLastMktData.Name = "labLastMktData";
            this.labLastMktData.Size = new System.Drawing.Size(49, 13);
            this.labLastMktData.TabIndex = 21;
            this.labLastMktData.Text = "00:00:00";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(12, 9);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(158, 13);
            this.label99.TabIndex = 20;
            this.label99.Text = "Last Market Data Update: ";
            // 
            // tmrUpdateScreen
            // 
            this.tmrUpdateScreen.Interval = 800;
            this.tmrUpdateScreen.Tick += new System.EventHandler(this.tmrUpdateScreen_Tick);
            // 
            // labRecalcAll
            // 
            this.labRecalcAll.AutoSize = true;
            this.labRecalcAll.Location = new System.Drawing.Point(170, 54);
            this.labRecalcAll.Name = "labRecalcAll";
            this.labRecalcAll.Size = new System.Drawing.Size(49, 13);
            this.labRecalcAll.TabIndex = 23;
            this.labRecalcAll.Text = "00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Last RecalcAll:";
            // 
            // labQuantityCheck
            // 
            this.labQuantityCheck.AutoSize = true;
            this.labQuantityCheck.Location = new System.Drawing.Point(170, 32);
            this.labQuantityCheck.Name = "labQuantityCheck";
            this.labQuantityCheck.Size = new System.Drawing.Size(49, 13);
            this.labQuantityCheck.TabIndex = 25;
            this.labQuantityCheck.Text = "00:00:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Last Quantity Update:";
            // 
            // labStarted
            // 
            this.labStarted.BackColor = System.Drawing.Color.Gray;
            this.labStarted.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labStarted.ForeColor = System.Drawing.Color.White;
            this.labStarted.Location = new System.Drawing.Point(173, 137);
            this.labStarted.Name = "labStarted";
            this.labStarted.Size = new System.Drawing.Size(116, 29);
            this.labStarted.TabIndex = 30;
            this.labStarted.Text = "NOT STARTED";
            this.labStarted.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMKTData
            // 
            this.btnMKTData.BackColor = System.Drawing.Color.Red;
            this.btnMKTData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMKTData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMKTData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMKTData.ForeColor = System.Drawing.Color.White;
            this.btnMKTData.Location = new System.Drawing.Point(175, 87);
            this.btnMKTData.Name = "btnMKTData";
            this.btnMKTData.Size = new System.Drawing.Size(113, 47);
            this.btnMKTData.TabIndex = 72;
            this.btnMKTData.Text = "MKTDATA";
            this.btnMKTData.UseVisualStyleBackColor = false;
            this.btnMKTData.Click += new System.EventHandler(this.btnMKTData_Click);
            // 
            // lblRecalcAllTimeTaken
            // 
            this.lblRecalcAllTimeTaken.AutoSize = true;
            this.lblRecalcAllTimeTaken.Location = new System.Drawing.Point(225, 54);
            this.lblRecalcAllTimeTaken.Name = "lblRecalcAllTimeTaken";
            this.lblRecalcAllTimeTaken.Size = new System.Drawing.Size(21, 13);
            this.lblRecalcAllTimeTaken.TabIndex = 73;
            this.lblRecalcAllTimeTaken.Text = "0 s";
            // 
            // lblRTQuantityTimeTaken
            // 
            this.lblRTQuantityTimeTaken.AutoSize = true;
            this.lblRTQuantityTimeTaken.Location = new System.Drawing.Point(225, 32);
            this.lblRTQuantityTimeTaken.Name = "lblRTQuantityTimeTaken";
            this.lblRTQuantityTimeTaken.Size = new System.Drawing.Size(21, 13);
            this.lblRTQuantityTimeTaken.TabIndex = 74;
            this.lblRTQuantityTimeTaken.Text = "0 s";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(295, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "AllFields (ms): ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(295, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "RTFields (ms): ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(295, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 77;
            this.label5.Text = "QTFields (ms): ";
            // 
            // labAllFields
            // 
            this.labAllFields.AutoSize = true;
            this.labAllFields.Location = new System.Drawing.Point(390, 9);
            this.labAllFields.Name = "labAllFields";
            this.labAllFields.Size = new System.Drawing.Size(29, 13);
            this.labAllFields.TabIndex = 78;
            this.labAllFields.Text = "0 ms";
            // 
            // labRTFields
            // 
            this.labRTFields.AutoSize = true;
            this.labRTFields.Location = new System.Drawing.Point(390, 32);
            this.labRTFields.Name = "labRTFields";
            this.labRTFields.Size = new System.Drawing.Size(29, 13);
            this.labRTFields.TabIndex = 79;
            this.labRTFields.Text = "0 ms";
            // 
            // labQTFields
            // 
            this.labQTFields.AutoSize = true;
            this.labQTFields.Location = new System.Drawing.Point(390, 54);
            this.labQTFields.Name = "labQTFields";
            this.labQTFields.Size = new System.Drawing.Size(29, 13);
            this.labQTFields.TabIndex = 80;
            this.labQTFields.Text = "0 ms";
            // 
            // btnChangeFeed
            // 
            this.btnChangeFeed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeFeed.Location = new System.Drawing.Point(333, 87);
            this.btnChangeFeed.Name = "btnChangeFeed";
            this.btnChangeFeed.Size = new System.Drawing.Size(89, 23);
            this.btnChangeFeed.TabIndex = 82;
            this.btnChangeFeed.Text = "Change Feed";
            this.btnChangeFeed.UseVisualStyleBackColor = true;
            this.btnChangeFeed.Click += new System.EventHandler(this.btnChangeFeed_Click);
            // 
            // labMktDataQueue
            // 
            this.labMktDataQueue.AutoSize = true;
            this.labMktDataQueue.Location = new System.Drawing.Point(14, 137);
            this.labMktDataQueue.Name = "labMktDataQueue";
            this.labMktDataQueue.Size = new System.Drawing.Size(117, 13);
            this.labMktDataQueue.TabIndex = 83;
            this.labMktDataQueue.Text = "MarketData Pending: 0";
            // 
            // tmrStart
            // 
            this.tmrStart.Interval = 2000;
            this.tmrStart.Tick += new System.EventHandler(this.tmrStart_Tick);
            // 
            // labSQLInsertQueue
            // 
            this.labSQLInsertQueue.AutoSize = true;
            this.labSQLInsertQueue.Location = new System.Drawing.Point(14, 153);
            this.labSQLInsertQueue.Name = "labSQLInsertQueue";
            this.labSQLInsertQueue.Size = new System.Drawing.Size(117, 13);
            this.labSQLInsertQueue.TabIndex = 84;
            this.labSQLInsertQueue.Text = "SQLUpdate Pending: 0";
            // 
            // txtLogView
            // 
            this.txtLogView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogView.BackColor = System.Drawing.Color.White;
            this.txtLogView.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogView.Location = new System.Drawing.Point(14, 181);
            this.txtLogView.Multiline = true;
            this.txtLogView.Name = "txtLogView";
            this.txtLogView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogView.Size = new System.Drawing.Size(444, 283);
            this.txtLogView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.labAllFields);
            this.panel1.Controls.Add(this.labRTFields);
            this.panel1.Controls.Add(this.label99);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labQTFields);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labLastMktData);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblRTQuantityTimeTaken);
            this.panel1.Controls.Add(this.labQuantityCheck);
            this.panel1.Controls.Add(this.labRecalcAll);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblRecalcAllTimeTaken);
            this.panel1.Location = new System.Drawing.Point(15, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 75);
            this.panel1.TabIndex = 85;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(314, 115);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(88, 13);
            this.label39.TabIndex = 86;
            this.label39.Text = "Current Feed: ";
            // 
            // lblCurrentFeed
            // 
            this.lblCurrentFeed.AutoSize = true;
            this.lblCurrentFeed.Location = new System.Drawing.Point(401, 115);
            this.lblCurrentFeed.Name = "lblCurrentFeed";
            this.lblCurrentFeed.Size = new System.Drawing.Size(33, 13);
            this.lblCurrentFeed.TabIndex = 87;
            this.lblCurrentFeed.Text = "None";
            // 
            // tmrClose
            // 
            this.tmrClose.Enabled = true;
            this.tmrClose.Interval = 500;
            this.tmrClose.Tick += new System.EventHandler(this.tmrClose_Tick);
            // 
            // chkProcessMktData
            // 
            this.chkProcessMktData.AutoSize = true;
            this.chkProcessMktData.Checked = true;
            this.chkProcessMktData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkProcessMktData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkProcessMktData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProcessMktData.Location = new System.Drawing.Point(17, 87);
            this.chkProcessMktData.Name = "chkProcessMktData";
            this.chkProcessMktData.Size = new System.Drawing.Size(141, 17);
            this.chkProcessMktData.TabIndex = 89;
            this.chkProcessMktData.Text = "Process MarketData";
            this.chkProcessMktData.UseVisualStyleBackColor = true;
            this.chkProcessMktData.CheckedChanged += new System.EventHandler(this.chkProcessMktData_CheckedChanged);
            // 
            // bntRecalc
            // 
            this.bntRecalc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntRecalc.Location = new System.Drawing.Point(15, 110);
            this.bntRecalc.Name = "bntRecalc";
            this.bntRecalc.Size = new System.Drawing.Size(127, 23);
            this.bntRecalc.TabIndex = 90;
            this.bntRecalc.Text = "Recalcate All Positions";
            this.bntRecalc.UseVisualStyleBackColor = true;
            this.bntRecalc.Click += new System.EventHandler(this.bntRecalc_Click);
            // 
            // tmrTurnMktData
            // 
            this.tmrTurnMktData.Enabled = true;
            this.tmrTurnMktData.Interval = 8000;
            this.tmrTurnMktData.Tick += new System.EventHandler(this.tmrTurnMktData_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 91;
            this.label6.Text = "MarketData Time";
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(336, 153);
            this.txtHour.MaxLength = 2;
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(31, 20);
            this.txtHour.TabIndex = 92;
            this.txtHour.TextChanged += new System.EventHandler(this.txtHour_TextChanged);
            // 
            // txtMinutes
            // 
            this.txtMinutes.Location = new System.Drawing.Point(381, 153);
            this.txtMinutes.MaxLength = 2;
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(31, 20);
            this.txtMinutes.TabIndex = 93;
            this.txtMinutes.TextChanged += new System.EventHandler(this.txtMinutes_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(370, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 94;
            this.label7.Text = ":";
            // 
            // frmRealTime
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(471, 476);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMinutes);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblCurrentFeed);
            this.Controls.Add(this.bntRecalc);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.chkProcessMktData);
            this.Controls.Add(this.btnChangeFeed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtLogView);
            this.Controls.Add(this.btnMKTData);
            this.Controls.Add(this.labMktDataQueue);
            this.Controls.Add(this.labSQLInsertQueue);
            this.Controls.Add(this.labStarted);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(479, 503);
            this.MinimumSize = new System.Drawing.Size(479, 503);
            this.Name = "frmRealTime";
            this.Text = "LiveBook RealTime Calculator 2.0  Started 18/10/2013 08:50";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRealTime_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRealTime_FormClosed);
            this.Load += new System.EventHandler(this.frmRealTime_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labLastMktData;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Timer tmrUpdateScreen;
        private System.Windows.Forms.Label labRecalcAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labQuantityCheck;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labStarted;
        private System.Windows.Forms.Button btnMKTData;
        private System.Windows.Forms.Label lblRecalcAllTimeTaken;
        private System.Windows.Forms.Label lblRTQuantityTimeTaken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labAllFields;
        private System.Windows.Forms.Label labRTFields;
        private System.Windows.Forms.Label labQTFields;
        private System.Windows.Forms.Button btnChangeFeed;
        private System.Windows.Forms.Label labMktDataQueue;
        private System.Windows.Forms.Timer tmrStart;
        private System.Windows.Forms.Label labSQLInsertQueue;
        private System.Windows.Forms.TextBox txtLogView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label lblCurrentFeed;
        public System.Windows.Forms.Timer tmrClose;
        private System.Windows.Forms.CheckBox chkProcessMktData;
        private System.Windows.Forms.Button bntRecalc;
        private System.Windows.Forms.Timer tmrTurnMktData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.Label label7;
    }
}