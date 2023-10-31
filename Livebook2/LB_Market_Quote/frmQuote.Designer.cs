namespace LiveBook
{
    partial class frmQuote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuote));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.txtId_Ticker = new System.Windows.Forms.TextBox();
            this.txtBid = new System.Windows.Forms.TextBox();
            this.txtLast = new System.Windows.Forms.TextBox();
            this.txtAcVolume = new System.Windows.Forms.TextBox();
            this.txtAsk = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBidQuantity = new System.Windows.Forms.TextBox();
            this.txtAskQuantity = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(227, 6);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.Size = new System.Drawing.Size(100, 20);
            this.txtTicker.TabIndex = 0;
            this.txtTicker.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTicker.Visible = false;
            // 
            // txtId_Ticker
            // 
            this.txtId_Ticker.Location = new System.Drawing.Point(227, 12);
            this.txtId_Ticker.Name = "txtId_Ticker";
            this.txtId_Ticker.Size = new System.Drawing.Size(100, 20);
            this.txtId_Ticker.TabIndex = 1;
            this.txtId_Ticker.Visible = false;
            // 
            // txtBid
            // 
            this.txtBid.BackColor = System.Drawing.Color.White;
            this.txtBid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBid.Location = new System.Drawing.Point(60, 35);
            this.txtBid.Name = "txtBid";
            this.txtBid.ReadOnly = true;
            this.txtBid.Size = new System.Drawing.Size(72, 13);
            this.txtBid.TabIndex = 2;
            this.txtBid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLast
            // 
            this.txtLast.BackColor = System.Drawing.Color.White;
            this.txtLast.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLast.Location = new System.Drawing.Point(60, 10);
            this.txtLast.Name = "txtLast";
            this.txtLast.ReadOnly = true;
            this.txtLast.Size = new System.Drawing.Size(72, 13);
            this.txtLast.TabIndex = 3;
            this.txtLast.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAcVolume
            // 
            this.txtAcVolume.BackColor = System.Drawing.Color.White;
            this.txtAcVolume.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAcVolume.Location = new System.Drawing.Point(60, 85);
            this.txtAcVolume.Name = "txtAcVolume";
            this.txtAcVolume.ReadOnly = true;
            this.txtAcVolume.Size = new System.Drawing.Size(72, 13);
            this.txtAcVolume.TabIndex = 4;
            this.txtAcVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAsk
            // 
            this.txtAsk.BackColor = System.Drawing.Color.White;
            this.txtAsk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAsk.Location = new System.Drawing.Point(60, 60);
            this.txtAsk.Name = "txtAsk";
            this.txtAsk.ReadOnly = true;
            this.txtAsk.Size = new System.Drawing.Size(72, 13);
            this.txtAsk.TabIndex = 5;
            this.txtAsk.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Last";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Bid";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ask";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Volume";
            // 
            // txtBidQuantity
            // 
            this.txtBidQuantity.BackColor = System.Drawing.Color.White;
            this.txtBidQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBidQuantity.Location = new System.Drawing.Point(138, 35);
            this.txtBidQuantity.Name = "txtBidQuantity";
            this.txtBidQuantity.ReadOnly = true;
            this.txtBidQuantity.Size = new System.Drawing.Size(81, 13);
            this.txtBidQuantity.TabIndex = 10;
            this.txtBidQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAskQuantity
            // 
            this.txtAskQuantity.BackColor = System.Drawing.Color.White;
            this.txtAskQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAskQuantity.Location = new System.Drawing.Point(138, 60);
            this.txtAskQuantity.Name = "txtAskQuantity";
            this.txtAskQuantity.ReadOnly = true;
            this.txtAskQuantity.Size = new System.Drawing.Size(81, 13);
            this.txtAskQuantity.TabIndex = 11;
            this.txtAskQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(233, 113);
            this.Controls.Add(this.txtAskQuantity);
            this.Controls.Add(this.txtBidQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAsk);
            this.Controls.Add(this.txtAcVolume);
            this.Controls.Add(this.txtLast);
            this.Controls.Add(this.txtBid);
            this.Controls.Add(this.txtId_Ticker);
            this.Controls.Add(this.txtTicker);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmQuote";
            this.Text = "NEST Quote";
            this.Load += new System.EventHandler(this.frmTop10_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.TextBox txtId_Ticker;
        private System.Windows.Forms.TextBox txtBid;
        private System.Windows.Forms.TextBox txtLast;
        private System.Windows.Forms.TextBox txtAcVolume;
        private System.Windows.Forms.TextBox txtAsk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBidQuantity;
        private System.Windows.Forms.TextBox txtAskQuantity;
    }
}