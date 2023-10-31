namespace SGN
{
    partial class frmRiskLimits
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
            this.webRiskLimits = new System.Windows.Forms.WebBrowser();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.chkInternal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // webRiskLimits
            // 
            this.webRiskLimits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskLimits.Location = new System.Drawing.Point(0, 0);
            this.webRiskLimits.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskLimits.Name = "webRiskLimits";
            this.webRiskLimits.Size = new System.Drawing.Size(498, 477);
            this.webRiskLimits.TabIndex = 0;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(260, 14);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Visible = false;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 20000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // chkInternal
            // 
            this.chkInternal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInternal.AutoSize = true;
            this.chkInternal.Location = new System.Drawing.Point(341, 16);
            this.chkInternal.Name = "chkInternal";
            this.chkInternal.Size = new System.Drawing.Size(128, 17);
            this.chkInternal.TabIndex = 2;
            this.chkInternal.Text = "Include Internal Limits";
            this.chkInternal.UseVisualStyleBackColor = true;
            this.chkInternal.CheckedChanged += new System.EventHandler(this.chkInternal_CheckedChanged);
            // 
            // frmRiskLimits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(498, 477);
            this.Controls.Add(this.chkInternal);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.webRiskLimits);
            this.Name = "frmRiskLimits";
            this.Text = "RISK - Limits";
            this.Load += new System.EventHandler(this.frmRiskLimits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskLimits;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.CheckBox chkInternal;


    }
}