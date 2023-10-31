namespace LiveBook
{
    partial class frmLiquidity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLiquidity));
            this.webRiskOptions = new System.Windows.Forms.WebBrowser();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webRiskOptions
            // 
            this.webRiskOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskOptions.Location = new System.Drawing.Point(0, 0);
            this.webRiskOptions.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskOptions.Name = "webRiskOptions";
            this.webRiskOptions.Size = new System.Drawing.Size(498, 477);
            this.webRiskOptions.TabIndex = 0;
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(320, 12);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 27;
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(239, 10);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 28;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // frmLiquidity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(498, 477);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.webRiskOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLiquidity";
            this.Text = "Liquidity";
            this.Load += new System.EventHandler(this.frmRiskOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskOptions;
        private System.Windows.Forms.ComboBox cmbPortfolio;
        private System.Windows.Forms.Button cmdRefresh;


    }
}