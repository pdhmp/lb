namespace LiveBook
{
    partial class frmMonteCarlo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonteCarlo));
            this.webRiskScenarios = new System.Windows.Forms.WebBrowser();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webRiskScenarios
            // 
            this.webRiskScenarios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webRiskScenarios.Location = new System.Drawing.Point(0, 0);
            this.webRiskScenarios.MinimumSize = new System.Drawing.Size(20, 20);
            this.webRiskScenarios.Name = "webRiskScenarios";
            this.webRiskScenarios.Size = new System.Drawing.Size(498, 477);
            this.webRiskScenarios.TabIndex = 0;
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 90000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(381, 9);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(88, 28);
            this.cmdRefresh.TabIndex = 1;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // frmMonteCarlo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(498, 477);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.webRiskScenarios);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMonteCarlo";
            this.Text = "Monte Carlo Simulation";
            this.Load += new System.EventHandler(this.frmRiskOptions_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webRiskScenarios;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Button cmdRefresh;


    }
}