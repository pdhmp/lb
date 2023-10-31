namespace LiveBook
{
    partial class frmMailingHTMLViewer:LBForm
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
            this.webOutput = new System.Windows.Forms.WebBrowser();
            this.btnSend = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstSubscribers = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // webOutput
            // 
            this.webOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webOutput.Location = new System.Drawing.Point(12, 12);
            this.webOutput.MinimumSize = new System.Drawing.Size(20, 20);
            this.webOutput.Name = "webOutput";
            this.webOutput.Size = new System.Drawing.Size(774, 508);
            this.webOutput.TabIndex = 10;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(431, 526);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(144, 33);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(200, 526);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 33);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstSubscribers
            // 
            this.lstSubscribers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSubscribers.FormattingEnabled = true;
            this.lstSubscribers.Location = new System.Drawing.Point(792, 12);
            this.lstSubscribers.Name = "lstSubscribers";
            this.lstSubscribers.Size = new System.Drawing.Size(203, 511);
            this.lstSubscribers.TabIndex = 13;
            this.lstSubscribers.SelectedIndexChanged += new System.EventHandler(this.lstSubscribers_SelectedIndexChanged);
            this.lstSubscribers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstSubscribers_MouseDown);
            // 
            // frmMailingHTMLViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 571);
            this.Controls.Add(this.lstSubscribers);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.webOutput);
            this.Name = "frmMailingHTMLViewer";
            this.Text = "MailingHTMLViewer";
            this.Load += new System.EventHandler(this.frmMailingHTMLViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser webOutput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListBox lstSubscribers;
    }
}