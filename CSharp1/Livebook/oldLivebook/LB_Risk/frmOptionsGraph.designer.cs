namespace LiveBook
{
    partial class frmOptionsGraph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptionsGraph));
            this.zgcOptionPayoff = new ZedGraph.ZedGraphControl();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.lblMinLoss = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // zgcOptionPayoff
            // 
            this.zgcOptionPayoff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcOptionPayoff.Location = new System.Drawing.Point(0, 35);
            this.zgcOptionPayoff.Name = "zgcOptionPayoff";
            this.zgcOptionPayoff.ScrollGrace = 0D;
            this.zgcOptionPayoff.ScrollMaxX = 0D;
            this.zgcOptionPayoff.ScrollMaxY = 0D;
            this.zgcOptionPayoff.ScrollMaxY2 = 0D;
            this.zgcOptionPayoff.ScrollMinX = 0D;
            this.zgcOptionPayoff.ScrollMinY = 0D;
            this.zgcOptionPayoff.ScrollMinY2 = 0D;
            this.zgcOptionPayoff.Size = new System.Drawing.Size(584, 689);
            this.zgcOptionPayoff.TabIndex = 0;
            this.zgcOptionPayoff.Load += new System.EventHandler(this.zgcSector_Load);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(419, 8);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 26;
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(289, 9);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 27;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(208, 8);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 28;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // lblMinLoss
            // 
            this.lblMinLoss.AutoSize = true;
            this.lblMinLoss.Location = new System.Drawing.Point(66, 13);
            this.lblMinLoss.Name = "lblMinLoss";
            this.lblMinLoss.Size = new System.Drawing.Size(35, 13);
            this.lblMinLoss.TabIndex = 29;
            this.lblMinLoss.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Max Loss";
            // 
            // frmOptionsGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(583, 723);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMinLoss);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.zgcOptionPayoff);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOptionsGraph";
            this.Text = "Option Payoff";
            this.Load += new System.EventHandler(this.frmExposureGraph_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcOptionPayoff;
        private System.Windows.Forms.ComboBox cmbPortfolio;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label lblMinLoss;
        private System.Windows.Forms.Label label1;
    }
}