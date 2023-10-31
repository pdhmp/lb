namespace SGN
{
    partial class frmRiskSectors
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
            this.webRiskSectors = new System.Windows.Forms.WebBrowser();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // webRiskSectors
            // 
            this.webRiskSectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskSectors.Location = new System.Drawing.Point(0, 0);
            this.webRiskSectors.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskSectors.Name = "webRiskSectors";
            this.webRiskSectors.Size = new System.Drawing.Size(474, 477);
            this.webRiskSectors.TabIndex = 0;
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
            this.cmbPortfolio.Location = new System.Drawing.Point(297, 12);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 26;
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // frmRiskSectors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(474, 477);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.webRiskSectors);
            this.Name = "frmRiskSectors";
            this.Text = "RISK - Sectors";
            this.Load += new System.EventHandler(this.frmRiskSectors_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskSectors;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.ComboBox cmbPortfolio;


    }
}