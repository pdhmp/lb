namespace LiveBook
{
    partial class frmRiskOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRiskOptions));
            this.webRiskOptions = new System.Windows.Forms.WebBrowser();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmbPortfolio = new LiveBook.NestPortCombo();
            this.SuspendLayout();
            // 
            // webRiskOptions
            // 
            this.webRiskOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskOptions.Location = new System.Drawing.Point(0, 0);
            this.webRiskOptions.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskOptions.Name = "webRiskOptions";
            this.webRiskOptions.Size = new System.Drawing.Size(462, 477);
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
            this.cmbPortfolio.DisplayMember = "Port_Name";
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.IdPortType = 2;
            this.cmbPortfolio.includeAllPortsOption = false;
            this.cmbPortfolio.includeMHConsolOption = false;
            this.cmbPortfolio.Location = new System.Drawing.Point(283, 12);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(146, 21);
            this.cmbPortfolio.TabIndex = 28;
            this.cmbPortfolio.ValueMember = "Id_Portfolio";
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // frmRiskOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(462, 477);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.webRiskOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRiskOptions";
            this.Text = "RISK - Options";
            this.Load += new System.EventHandler(this.frmRiskOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskOptions;
        private System.Windows.Forms.Timer tmrUpdate;
        private NestPortCombo cmbPortfolio;


    }
}