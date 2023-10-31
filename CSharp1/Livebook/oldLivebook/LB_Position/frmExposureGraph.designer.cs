namespace LiveBook
{
    partial class frmExposureGraph
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExposureGraph));
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.zgcExpTotal = new ZedGraph.ZedGraphControl();
            this.zgcSector = new ZedGraph.ZedGraphControl();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 5000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // zgcExpTotal
            // 
            this.zgcExpTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcExpTotal.Location = new System.Drawing.Point(12, 35);
            this.zgcExpTotal.Name = "zgcExpTotal";
            this.zgcExpTotal.ScrollGrace = 0D;
            this.zgcExpTotal.ScrollMaxX = 0D;
            this.zgcExpTotal.ScrollMaxY = 0D;
            this.zgcExpTotal.ScrollMaxY2 = 0D;
            this.zgcExpTotal.ScrollMinX = 0D;
            this.zgcExpTotal.ScrollMinY = 0D;
            this.zgcExpTotal.ScrollMinY2 = 0D;
            this.zgcExpTotal.Size = new System.Drawing.Size(407, 253);
            this.zgcExpTotal.TabIndex = 1;
            this.zgcExpTotal.Visible = false;
            // 
            // zgcSector
            // 
            this.zgcSector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcSector.Location = new System.Drawing.Point(0, 35);
            this.zgcSector.Name = "zgcSector";
            this.zgcSector.ScrollGrace = 0D;
            this.zgcSector.ScrollMaxX = 0D;
            this.zgcSector.ScrollMaxY = 0D;
            this.zgcSector.ScrollMaxY2 = 0D;
            this.zgcSector.ScrollMinX = 0D;
            this.zgcSector.ScrollMinY = 0D;
            this.zgcSector.ScrollMinY2 = 0D;
            this.zgcSector.Size = new System.Drawing.Size(438, 689);
            this.zgcSector.TabIndex = 0;
            this.zgcSector.Load += new System.EventHandler(this.zgcSector_Load);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(273, 8);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 26;
            // 
            // frmExposureGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 723);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.zgcExpTotal);
            this.Controls.Add(this.zgcSector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExposureGraph";
            this.Text = "Sector Exposure";
            this.Load += new System.EventHandler(this.frmExposureGraph_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcSector;
        private ZedGraph.ZedGraphControl zgcExpTotal;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.ComboBox cmbPortfolio;
    }
}