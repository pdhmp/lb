namespace LiveBook
{
    partial class frmStatisticsPortfolio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatisticsPortfolio));
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.webRiskLimits = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 20000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(355, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Visible = false;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // webRiskLimits
            // 
            this.webRiskLimits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskLimits.Location = new System.Drawing.Point(0, 0);
            this.webRiskLimits.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskLimits.Name = "webRiskLimits";
            this.webRiskLimits.Size = new System.Drawing.Size(453, 417);
            this.webRiskLimits.TabIndex = 0;
            this.webRiskLimits.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webRiskLimits_DocumentCompleted);
            // 
            // frmStatisticsPortfolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(453, 417);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.webRiskLimits);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStatisticsPortfolio";
            this.Text = "Portfolio Statistics";
            this.Load += new System.EventHandler(this.frmRiskLimits_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskLimits;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Timer tmrUpdate;


    }
}