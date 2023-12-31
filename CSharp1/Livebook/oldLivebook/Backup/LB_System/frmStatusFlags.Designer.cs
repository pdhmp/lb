namespace SGN
{
    partial class frmStatusFlags
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
            this.xlblReutersFeeder_Last = new System.Windows.Forms.Label();
            this.xlblReutFeeder_BID_ASK = new System.Windows.Forms.Label();
            this.xlblCALC_LB2_Client = new System.Windows.Forms.Label();
            this.xlblBBG_Futures = new System.Windows.Forms.Label();
            this.xlblFIX_Agora = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.xlblBBG_History = new System.Windows.Forms.Label();
            this.xlblBBG_History_TR = new System.Windows.Forms.Label();
            this.xlblImagineSync = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.xlblFIX_Agora_Flag = new System.Windows.Forms.Label();
            this.xlblBBG_History_Flag = new System.Windows.Forms.Label();
            this.xlblBBG_History_TR_Flag = new System.Windows.Forms.Label();
            this.xlblImagineSync_Flag = new System.Windows.Forms.Label();
            this.xlblBBG_Futures_Flag = new System.Windows.Forms.Label();
            this.xlblCALC_LB2_Client_Flag = new System.Windows.Forms.Label();
            this.xlblReutFeeder_BID_ASK_Flag = new System.Windows.Forms.Label();
            this.xlblReutersFeeder_Last_Flag = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // xlblReutersFeeder_Last
            // 
            this.xlblReutersFeeder_Last.AutoSize = true;
            this.xlblReutersFeeder_Last.Location = new System.Drawing.Point(6, 7);
            this.xlblReutersFeeder_Last.Name = "xlblReutersFeeder_Last";
            this.xlblReutersFeeder_Last.Size = new System.Drawing.Size(94, 13);
            this.xlblReutersFeeder_Last.TabIndex = 1;
            this.xlblReutersFeeder_Last.Text = "Reuters Feed Last";
            // 
            // xlblReutFeeder_BID_ASK
            // 
            this.xlblReutFeeder_BID_ASK.AutoSize = true;
            this.xlblReutFeeder_BID_ASK.Location = new System.Drawing.Point(6, 27);
            this.xlblReutFeeder_BID_ASK.Name = "xlblReutFeeder_BID_ASK";
            this.xlblReutFeeder_BID_ASK.Size = new System.Drawing.Size(110, 13);
            this.xlblReutFeeder_BID_ASK.TabIndex = 2;
            this.xlblReutFeeder_BID_ASK.Text = "Reuters Feed Bid-Ask";
            // 
            // xlblCALC_LB2_Client
            // 
            this.xlblCALC_LB2_Client.AutoSize = true;
            this.xlblCALC_LB2_Client.Location = new System.Drawing.Point(6, 47);
            this.xlblCALC_LB2_Client.Name = "xlblCALC_LB2_Client";
            this.xlblCALC_LB2_Client.Size = new System.Drawing.Size(76, 13);
            this.xlblCALC_LB2_Client.TabIndex = 3;
            this.xlblCALC_LB2_Client.Text = "LiveBook Calc";
            // 
            // xlblBBG_Futures
            // 
            this.xlblBBG_Futures.AutoSize = true;
            this.xlblBBG_Futures.Location = new System.Drawing.Point(6, 67);
            this.xlblBBG_Futures.Name = "xlblBBG_Futures";
            this.xlblBBG_Futures.Size = new System.Drawing.Size(69, 13);
            this.xlblBBG_Futures.TabIndex = 4;
            this.xlblBBG_Futures.Text = "Feed Futures";
            // 
            // xlblFIX_Agora
            // 
            this.xlblFIX_Agora.AutoSize = true;
            this.xlblFIX_Agora.Location = new System.Drawing.Point(185, 7);
            this.xlblFIX_Agora.Name = "xlblFIX_Agora";
            this.xlblFIX_Agora.Size = new System.Drawing.Size(54, 13);
            this.xlblFIX_Agora.TabIndex = 5;
            this.xlblFIX_Agora.Text = "FIX Agora";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "FIX TradeBook";
            // 
            // xlblBBG_History
            // 
            this.xlblBBG_History.AutoSize = true;
            this.xlblBBG_History.Location = new System.Drawing.Point(185, 27);
            this.xlblBBG_History.Name = "xlblBBG_History";
            this.xlblBBG_History.Size = new System.Drawing.Size(61, 13);
            this.xlblBBG_History.TabIndex = 7;
            this.xlblBBG_History.Text = "Hist Feeder";
            // 
            // xlblBBG_History_TR
            // 
            this.xlblBBG_History_TR.AutoSize = true;
            this.xlblBBG_History_TR.Location = new System.Drawing.Point(185, 47);
            this.xlblBBG_History_TR.Name = "xlblBBG_History_TR";
            this.xlblBBG_History_TR.Size = new System.Drawing.Size(79, 13);
            this.xlblBBG_History_TR.TabIndex = 8;
            this.xlblBBG_History_TR.Text = "Hist TR Feeder";
            // 
            // xlblImagineSync
            // 
            this.xlblImagineSync.AutoSize = true;
            this.xlblImagineSync.Location = new System.Drawing.Point(185, 67);
            this.xlblImagineSync.Name = "xlblImagineSync";
            this.xlblImagineSync.Size = new System.Drawing.Size(71, 13);
            this.xlblImagineSync.TabIndex = 9;
            this.xlblImagineSync.Text = "Imagine Sync";
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(5, 116);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(335, 225);
            this.txtStatus.TabIndex = 10;
            // 
            // xlblFIX_Agora_Flag
            // 
            this.xlblFIX_Agora_Flag.AutoSize = true;
            this.xlblFIX_Agora_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblFIX_Agora_Flag.Location = new System.Drawing.Point(282, 7);
            this.xlblFIX_Agora_Flag.Name = "xlblFIX_Agora_Flag";
            this.xlblFIX_Agora_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblFIX_Agora_Flag.TabIndex = 11;
            this.xlblFIX_Agora_Flag.Text = "NA";
            // 
            // xlblBBG_History_Flag
            // 
            this.xlblBBG_History_Flag.AutoSize = true;
            this.xlblBBG_History_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblBBG_History_Flag.Location = new System.Drawing.Point(282, 27);
            this.xlblBBG_History_Flag.Name = "xlblBBG_History_Flag";
            this.xlblBBG_History_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblBBG_History_Flag.TabIndex = 12;
            this.xlblBBG_History_Flag.Text = "NA";
            // 
            // xlblBBG_History_TR_Flag
            // 
            this.xlblBBG_History_TR_Flag.AutoSize = true;
            this.xlblBBG_History_TR_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblBBG_History_TR_Flag.Location = new System.Drawing.Point(282, 47);
            this.xlblBBG_History_TR_Flag.Name = "xlblBBG_History_TR_Flag";
            this.xlblBBG_History_TR_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblBBG_History_TR_Flag.TabIndex = 13;
            this.xlblBBG_History_TR_Flag.Text = "NA";
            // 
            // xlblImagineSync_Flag
            // 
            this.xlblImagineSync_Flag.AutoSize = true;
            this.xlblImagineSync_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblImagineSync_Flag.Location = new System.Drawing.Point(282, 67);
            this.xlblImagineSync_Flag.Name = "xlblImagineSync_Flag";
            this.xlblImagineSync_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblImagineSync_Flag.TabIndex = 14;
            this.xlblImagineSync_Flag.Text = "NA";
            // 
            // xlblBBG_Futures_Flag
            // 
            this.xlblBBG_Futures_Flag.AutoSize = true;
            this.xlblBBG_Futures_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblBBG_Futures_Flag.Location = new System.Drawing.Point(135, 67);
            this.xlblBBG_Futures_Flag.Name = "xlblBBG_Futures_Flag";
            this.xlblBBG_Futures_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblBBG_Futures_Flag.TabIndex = 18;
            this.xlblBBG_Futures_Flag.Text = "NA";
            // 
            // xlblCALC_LB2_Client_Flag
            // 
            this.xlblCALC_LB2_Client_Flag.AutoSize = true;
            this.xlblCALC_LB2_Client_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblCALC_LB2_Client_Flag.Location = new System.Drawing.Point(135, 47);
            this.xlblCALC_LB2_Client_Flag.Name = "xlblCALC_LB2_Client_Flag";
            this.xlblCALC_LB2_Client_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblCALC_LB2_Client_Flag.TabIndex = 17;
            this.xlblCALC_LB2_Client_Flag.Text = "NA";
            // 
            // xlblReutFeeder_BID_ASK_Flag
            // 
            this.xlblReutFeeder_BID_ASK_Flag.AutoSize = true;
            this.xlblReutFeeder_BID_ASK_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblReutFeeder_BID_ASK_Flag.Location = new System.Drawing.Point(135, 27);
            this.xlblReutFeeder_BID_ASK_Flag.Name = "xlblReutFeeder_BID_ASK_Flag";
            this.xlblReutFeeder_BID_ASK_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblReutFeeder_BID_ASK_Flag.TabIndex = 16;
            this.xlblReutFeeder_BID_ASK_Flag.Text = "NA";
            // 
            // xlblReutersFeeder_Last_Flag
            // 
            this.xlblReutersFeeder_Last_Flag.AutoSize = true;
            this.xlblReutersFeeder_Last_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblReutersFeeder_Last_Flag.Location = new System.Drawing.Point(135, 7);
            this.xlblReutersFeeder_Last_Flag.Name = "xlblReutersFeeder_Last_Flag";
            this.xlblReutersFeeder_Last_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblReutersFeeder_Last_Flag.TabIndex = 15;
            this.xlblReutersFeeder_Last_Flag.Text = "NA";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(135, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "NA";
            // 
            // frmStatusFlags
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(345, 323);
            this.ControlBox = false;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.xlblBBG_Futures_Flag);
            this.Controls.Add(this.xlblCALC_LB2_Client_Flag);
            this.Controls.Add(this.xlblReutFeeder_BID_ASK_Flag);
            this.Controls.Add(this.xlblReutersFeeder_Last_Flag);
            this.Controls.Add(this.xlblImagineSync_Flag);
            this.Controls.Add(this.xlblBBG_History_TR_Flag);
            this.Controls.Add(this.xlblBBG_History_Flag);
            this.Controls.Add(this.xlblFIX_Agora_Flag);
            this.Controls.Add(this.xlblBBG_History_TR);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.xlblImagineSync);
            this.Controls.Add(this.xlblBBG_History);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.xlblFIX_Agora);
            this.Controls.Add(this.xlblBBG_Futures);
            this.Controls.Add(this.xlblCALC_LB2_Client);
            this.Controls.Add(this.xlblReutFeeder_BID_ASK);
            this.Controls.Add(this.xlblReutersFeeder_Last);
            this.Location = new System.Drawing.Point(1065, 0);
            this.Name = "frmStatusFlags";
            this.Text = "System Status";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmStatusFlags_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label xlblReutersFeeder_Last;
        private System.Windows.Forms.Label xlblReutFeeder_BID_ASK;
        private System.Windows.Forms.Label xlblCALC_LB2_Client;
        private System.Windows.Forms.Label xlblBBG_Futures;
        private System.Windows.Forms.Label xlblFIX_Agora;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label xlblBBG_History;
        private System.Windows.Forms.Label xlblBBG_History_TR;
        private System.Windows.Forms.Label xlblImagineSync;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label xlblFIX_Agora_Flag;
        private System.Windows.Forms.Label xlblBBG_History_Flag;
        private System.Windows.Forms.Label xlblBBG_History_TR_Flag;
        private System.Windows.Forms.Label xlblImagineSync_Flag;
        private System.Windows.Forms.Label xlblBBG_Futures_Flag;
        private System.Windows.Forms.Label xlblCALC_LB2_Client_Flag;
        private System.Windows.Forms.Label xlblReutFeeder_BID_ASK_Flag;
        private System.Windows.Forms.Label xlblReutersFeeder_Last_Flag;
        private System.Windows.Forms.Label label9;
    }
}