namespace SGN
{
    partial class zfrmTradeSet
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTicker = new System.Windows.Forms.Label();
            this.grpPurchases = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSellTotal = new System.Windows.Forms.Label();
            this.lblBuyTotal = new System.Windows.Forms.Label();
            this.lblNetBravo = new System.Windows.Forms.Label();
            this.lblNetTOP = new System.Windows.Forms.Label();
            this.lblNetMH = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSplitBravo = new System.Windows.Forms.TextBox();
            this.txtPosBravo = new System.Windows.Forms.TextBox();
            this.txtSplitTop = new System.Windows.Forms.TextBox();
            this.txtPosTop = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSplitMH = new System.Windows.Forms.TextBox();
            this.txtBuyMH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.grpPurchases.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 211);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(204, 211);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblId);
            this.groupBox2.Controls.Add(this.lblTicker);
            this.groupBox2.Location = new System.Drawing.Point(493, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(109, 45);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ticker";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(62, 26);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(41, 13);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "label18";
            this.lblId.Visible = false;
            // 
            // lblTicker
            // 
            this.lblTicker.AutoSize = true;
            this.lblTicker.Location = new System.Drawing.Point(14, 19);
            this.lblTicker.Name = "lblTicker";
            this.lblTicker.Size = new System.Drawing.Size(35, 13);
            this.lblTicker.TabIndex = 1;
            this.lblTicker.Text = "label1";
            // 
            // grpPurchases
            // 
            this.grpPurchases.Controls.Add(this.label6);
            this.grpPurchases.Controls.Add(this.label5);
            this.grpPurchases.Controls.Add(this.lblSellTotal);
            this.grpPurchases.Controls.Add(this.lblBuyTotal);
            this.grpPurchases.Controls.Add(this.lblNetBravo);
            this.grpPurchases.Controls.Add(this.lblNetTOP);
            this.grpPurchases.Controls.Add(this.lblNetMH);
            this.grpPurchases.Controls.Add(this.label4);
            this.grpPurchases.Controls.Add(this.label2);
            this.grpPurchases.Controls.Add(this.label1);
            this.grpPurchases.Controls.Add(this.txtSplitBravo);
            this.grpPurchases.Controls.Add(this.txtPosBravo);
            this.grpPurchases.Controls.Add(this.txtSplitTop);
            this.grpPurchases.Controls.Add(this.txtPosTop);
            this.grpPurchases.Controls.Add(this.label17);
            this.grpPurchases.Controls.Add(this.label11);
            this.grpPurchases.Controls.Add(this.txtSplitMH);
            this.grpPurchases.Controls.Add(this.txtBuyMH);
            this.grpPurchases.Controls.Add(this.label3);
            this.grpPurchases.Location = new System.Drawing.Point(12, 67);
            this.grpPurchases.Name = "grpPurchases";
            this.grpPurchases.Size = new System.Drawing.Size(600, 129);
            this.grpPurchases.TabIndex = 19;
            this.grpPurchases.TabStop = false;
            this.grpPurchases.Text = "Trade Split";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(502, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "0,00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(478, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 31;
            this.label5.Text = "Total";
            // 
            // lblSellTotal
            // 
            this.lblSellTotal.AutoSize = true;
            this.lblSellTotal.Location = new System.Drawing.Point(503, 64);
            this.lblSellTotal.Name = "lblSellTotal";
            this.lblSellTotal.Size = new System.Drawing.Size(28, 13);
            this.lblSellTotal.TabIndex = 30;
            this.lblSellTotal.Text = "0,00";
            // 
            // lblBuyTotal
            // 
            this.lblBuyTotal.AutoSize = true;
            this.lblBuyTotal.Location = new System.Drawing.Point(503, 38);
            this.lblBuyTotal.Name = "lblBuyTotal";
            this.lblBuyTotal.Size = new System.Drawing.Size(28, 13);
            this.lblBuyTotal.TabIndex = 29;
            this.lblBuyTotal.Text = "0,00";
            // 
            // lblNetBravo
            // 
            this.lblNetBravo.AutoSize = true;
            this.lblNetBravo.Location = new System.Drawing.Point(401, 90);
            this.lblNetBravo.Name = "lblNetBravo";
            this.lblNetBravo.Size = new System.Drawing.Size(28, 13);
            this.lblNetBravo.TabIndex = 28;
            this.lblNetBravo.Text = "0,00";
            this.lblNetBravo.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNetTOP
            // 
            this.lblNetTOP.AutoSize = true;
            this.lblNetTOP.Location = new System.Drawing.Point(295, 90);
            this.lblNetTOP.Name = "lblNetTOP";
            this.lblNetTOP.Size = new System.Drawing.Size(28, 13);
            this.lblNetTOP.TabIndex = 27;
            this.lblNetTOP.Text = "0,00";
            this.lblNetTOP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblNetMH
            // 
            this.lblNetMH.AutoSize = true;
            this.lblNetMH.Location = new System.Drawing.Point(189, 90);
            this.lblNetMH.Name = "lblNetMH";
            this.lblNetMH.Size = new System.Drawing.Size(28, 13);
            this.lblNetMH.TabIndex = 26;
            this.lblNetMH.Text = "0,00";
            this.lblNetMH.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Net";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Sales";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Purchases";
            // 
            // txtSplitBravo
            // 
            this.txtSplitBravo.Location = new System.Drawing.Point(329, 61);
            this.txtSplitBravo.Name = "txtSplitBravo";
            this.txtSplitBravo.Size = new System.Drawing.Size(100, 20);
            this.txtSplitBravo.TabIndex = 21;
            // 
            // txtPosBravo
            // 
            this.txtPosBravo.Enabled = false;
            this.txtPosBravo.Location = new System.Drawing.Point(329, 35);
            this.txtPosBravo.Name = "txtPosBravo";
            this.txtPosBravo.Size = new System.Drawing.Size(100, 20);
            this.txtPosBravo.TabIndex = 21;
            // 
            // txtSplitTop
            // 
            this.txtSplitTop.Location = new System.Drawing.Point(223, 61);
            this.txtSplitTop.Name = "txtSplitTop";
            this.txtSplitTop.Size = new System.Drawing.Size(100, 20);
            this.txtSplitTop.TabIndex = 22;
            // 
            // txtPosTop
            // 
            this.txtPosTop.Enabled = false;
            this.txtPosTop.Location = new System.Drawing.Point(223, 35);
            this.txtPosTop.Name = "txtPosTop";
            this.txtPosTop.Size = new System.Drawing.Size(100, 20);
            this.txtPosTop.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(245, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(42, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Topline";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(352, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Bravo FIA";
            // 
            // txtSplitMH
            // 
            this.txtSplitMH.Location = new System.Drawing.Point(117, 61);
            this.txtSplitMH.Name = "txtSplitMH";
            this.txtSplitMH.Size = new System.Drawing.Size(100, 20);
            this.txtSplitMH.TabIndex = 20;
            // 
            // txtBuyMH
            // 
            this.txtBuyMH.Enabled = false;
            this.txtBuyMH.Location = new System.Drawing.Point(117, 35);
            this.txtBuyMH.Name = "txtBuyMH";
            this.txtBuyMH.Size = new System.Drawing.Size(100, 20);
            this.txtBuyMH.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(155, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "MH";
            // 
            // zfrmTradeSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 253);
            this.Controls.Add(this.grpPurchases);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "zfrmTradeSet";
            this.Text = "Trade Split - Set Fixed Alocation";
            this.Load += new System.EventHandler(this.frmTradeSet_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpPurchases.ResumeLayout(false);
            this.grpPurchases.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTicker;
        private System.Windows.Forms.GroupBox grpPurchases;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSplitBravo;
        private System.Windows.Forms.TextBox txtSplitTop;
        private System.Windows.Forms.TextBox txtSplitMH;
        private System.Windows.Forms.TextBox txtPosBravo;
        private System.Windows.Forms.TextBox txtPosTop;
        private System.Windows.Forms.TextBox txtBuyMH;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblSellTotal;
        private System.Windows.Forms.Label lblBuyTotal;
        private System.Windows.Forms.Label lblNetBravo;
        private System.Windows.Forms.Label lblNetTOP;
        private System.Windows.Forms.Label lblNetMH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}