namespace LiveBook
{
    partial class frmRiskArb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRiskArb));
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.webRiskLimits = new System.Windows.Forms.WebBrowser();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.chkExcludeLimit = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(366, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // webRiskLimits
            // 
            this.webRiskLimits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webRiskLimits.Location = new System.Drawing.Point(0, 41);
            this.webRiskLimits.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskLimits.Name = "webRiskLimits";
            this.webRiskLimits.Size = new System.Drawing.Size(453, 376);
            this.webRiskLimits.TabIndex = 0;
            this.webRiskLimits.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webRiskLimits_DocumentCompleted);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(56, 15);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(110, 20);
            this.dtpDate.TabIndex = 6;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date:";
            // 
            // chkExcludeLimit
            // 
            this.chkExcludeLimit.AutoSize = true;
            this.chkExcludeLimit.Location = new System.Drawing.Point(198, 18);
            this.chkExcludeLimit.Name = "chkExcludeLimit";
            this.chkExcludeLimit.Size = new System.Drawing.Size(131, 17);
            this.chkExcludeLimit.TabIndex = 7;
            this.chkExcludeLimit.Text = "Exclude Internal Limits";
            this.chkExcludeLimit.UseVisualStyleBackColor = true;
            this.chkExcludeLimit.CheckedChanged += new System.EventHandler(this.chkExcludeLimit_CheckedChanged);
            // 
            // frmRiskArb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(453, 417);
            this.Controls.Add(this.chkExcludeLimit);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.webRiskLimits);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRiskArb";
            this.Text = "RISK - Arb Limits ";
            this.Load += new System.EventHandler(this.frmRiskLimits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskLimits;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExcludeLimit;


    }
}