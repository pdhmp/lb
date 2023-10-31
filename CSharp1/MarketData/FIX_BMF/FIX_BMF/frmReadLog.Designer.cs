namespace FIX_BMF
{
    partial class frmReadLog
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
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.cmdLoadAsk = new System.Windows.Forms.Button();
            this.txtEndLine = new System.Windows.Forms.TextBox();
            this.cmdUpOne = new System.Windows.Forms.Button();
            this.cmdDownOne = new System.Windows.Forms.Button();
            this.cmdDownTen = new System.Windows.Forms.Button();
            this.cmdUpTen = new System.Windows.Forms.Button();
            this.cmdLoadBid = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessages.Location = new System.Drawing.Point(12, 70);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessages.Size = new System.Drawing.Size(518, 273);
            this.txtMessages.TabIndex = 1;
            // 
            // cmdLoadAsk
            // 
            this.cmdLoadAsk.Location = new System.Drawing.Point(12, 12);
            this.cmdLoadAsk.Name = "cmdLoadAsk";
            this.cmdLoadAsk.Size = new System.Drawing.Size(75, 23);
            this.cmdLoadAsk.TabIndex = 2;
            this.cmdLoadAsk.Text = "Load Ask";
            this.cmdLoadAsk.UseVisualStyleBackColor = true;
            this.cmdLoadAsk.Click += new System.EventHandler(this.cmdLoadAsk_Click);
            // 
            // txtEndLine
            // 
            this.txtEndLine.Location = new System.Drawing.Point(197, 14);
            this.txtEndLine.Name = "txtEndLine";
            this.txtEndLine.Size = new System.Drawing.Size(63, 20);
            this.txtEndLine.TabIndex = 3;
            this.txtEndLine.Text = "1";
            this.txtEndLine.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtEndLine.TextChanged += new System.EventHandler(this.txtEndLine_TextChanged);
            // 
            // cmdUpOne
            // 
            this.cmdUpOne.Location = new System.Drawing.Point(266, 12);
            this.cmdUpOne.Name = "cmdUpOne";
            this.cmdUpOne.Size = new System.Drawing.Size(34, 23);
            this.cmdUpOne.TabIndex = 4;
            this.cmdUpOne.Text = "+1";
            this.cmdUpOne.UseVisualStyleBackColor = true;
            this.cmdUpOne.Click += new System.EventHandler(this.cmdUpOne_Click);
            // 
            // cmdDownOne
            // 
            this.cmdDownOne.Location = new System.Drawing.Point(159, 12);
            this.cmdDownOne.Name = "cmdDownOne";
            this.cmdDownOne.Size = new System.Drawing.Size(34, 23);
            this.cmdDownOne.TabIndex = 5;
            this.cmdDownOne.Text = "-1";
            this.cmdDownOne.UseVisualStyleBackColor = true;
            this.cmdDownOne.Click += new System.EventHandler(this.cmdDownOne_Click);
            // 
            // cmdDownTen
            // 
            this.cmdDownTen.Location = new System.Drawing.Point(121, 12);
            this.cmdDownTen.Name = "cmdDownTen";
            this.cmdDownTen.Size = new System.Drawing.Size(34, 23);
            this.cmdDownTen.TabIndex = 6;
            this.cmdDownTen.Text = "-10";
            this.cmdDownTen.UseVisualStyleBackColor = true;
            this.cmdDownTen.Click += new System.EventHandler(this.cmdDownTen_Click);
            // 
            // cmdUpTen
            // 
            this.cmdUpTen.Location = new System.Drawing.Point(306, 12);
            this.cmdUpTen.Name = "cmdUpTen";
            this.cmdUpTen.Size = new System.Drawing.Size(34, 23);
            this.cmdUpTen.TabIndex = 7;
            this.cmdUpTen.Text = "+10";
            this.cmdUpTen.UseVisualStyleBackColor = true;
            this.cmdUpTen.Click += new System.EventHandler(this.cmdUpTen_Click);
            // 
            // cmdLoadBid
            // 
            this.cmdLoadBid.Location = new System.Drawing.Point(12, 41);
            this.cmdLoadBid.Name = "cmdLoadBid";
            this.cmdLoadBid.Size = new System.Drawing.Size(75, 23);
            this.cmdLoadBid.TabIndex = 8;
            this.cmdLoadBid.Text = "Load Bid";
            this.cmdLoadBid.UseVisualStyleBackColor = true;
            this.cmdLoadBid.Click += new System.EventHandler(this.cmdLoadBid_Click);
            // 
            // frmReadLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 355);
            this.Controls.Add(this.cmdLoadBid);
            this.Controls.Add(this.cmdUpTen);
            this.Controls.Add(this.cmdDownTen);
            this.Controls.Add(this.cmdDownOne);
            this.Controls.Add(this.cmdUpOne);
            this.Controls.Add(this.txtEndLine);
            this.Controls.Add(this.cmdLoadAsk);
            this.Controls.Add(this.txtMessages);
            this.Name = "frmReadLog";
            this.Text = "frmReadLog";
            this.Load += new System.EventHandler(this.frmReadLog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button cmdLoadAsk;
        private System.Windows.Forms.TextBox txtEndLine;
        private System.Windows.Forms.Button cmdUpOne;
        private System.Windows.Forms.Button cmdDownOne;
        private System.Windows.Forms.Button cmdDownTen;
        private System.Windows.Forms.Button cmdUpTen;
        private System.Windows.Forms.Button cmdLoadBid;
    }
}