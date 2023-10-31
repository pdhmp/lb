namespace SGN
{
    partial class frmRiskWeeklyReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRiskWeeklyReport));
            this.webRiskLimits = new System.Windows.Forms.WebBrowser();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.chkInternal = new System.Windows.Forms.CheckBox();
            this.cmbPortfolio = new SGN.NestPortCombo();
            this.SuspendLayout();
            // 
            // webRiskLimits
            // 
            this.webRiskLimits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskLimits.Location = new System.Drawing.Point(0, 0);
            this.webRiskLimits.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskLimits.Name = "webRiskLimits";
            this.webRiskLimits.Size = new System.Drawing.Size(1082, 665);
            this.webRiskLimits.TabIndex = 0;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(824, 10);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // chkInternal
            // 
            this.chkInternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInternal.AutoSize = true;
            this.chkInternal.Location = new System.Drawing.Point(920, 39);
            this.chkInternal.Name = "chkInternal";
            this.chkInternal.Size = new System.Drawing.Size(128, 17);
            this.chkInternal.TabIndex = 2;
            this.chkInternal.Text = "Include Internal Limits";
            this.chkInternal.UseVisualStyleBackColor = true;
            this.chkInternal.CheckedChanged += new System.EventHandler(this.chkInternal_CheckedChanged);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.DisplayMember = "Port_Name";
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(920, 12);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(138, 21);
            this.cmbPortfolio.TabIndex = 26;
            this.cmbPortfolio.ValueMember = "Id_Portfolio";
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // frmRiskWeeklyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1082, 665);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.chkInternal);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.webRiskLimits);
            this.Name = "frmRiskWeeklyReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "RISK - Weekly Report";
            this.Load += new System.EventHandler(this.frmRiskLimits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskLimits;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.CheckBox chkInternal;
        private NestPortCombo cmbPortfolio;


    }
}