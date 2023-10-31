namespace SGN
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
            this.components = new System.ComponentModel.Container();
            this.webRiskOptions = new System.Windows.Forms.WebBrowser();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
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
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 20000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
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
            // frmLiquidity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(498, 477);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.webRiskOptions);
            this.Name = "frmLiquidity";
            this.Text = "Liquidity";
            this.Load += new System.EventHandler(this.frmRiskOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskOptions;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.ComboBox cmbPortfolio;


    }
}