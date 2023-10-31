namespace LiveBook
{
    partial class frmRiskCharts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRiskCharts));
            this.zgcGrossExposure = new ZedGraph.ZedGraphControl();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.zgcVolatility = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // zgcGrossExposure
            // 
            this.zgcGrossExposure.Location = new System.Drawing.Point(0, 0);
            this.zgcGrossExposure.Name = "zgcGrossExposure";
            this.zgcGrossExposure.ScrollGrace = 0D;
            this.zgcGrossExposure.ScrollMaxX = 0D;
            this.zgcGrossExposure.ScrollMaxY = 0D;
            this.zgcGrossExposure.ScrollMaxY2 = 0D;
            this.zgcGrossExposure.ScrollMinX = 0D;
            this.zgcGrossExposure.ScrollMinY = 0D;
            this.zgcGrossExposure.ScrollMinY2 = 0D;
            this.zgcGrossExposure.Size = new System.Drawing.Size(556, 164);
            this.zgcGrossExposure.TabIndex = 26;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 50000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // zgcVolatility
            // 
            this.zgcVolatility.Location = new System.Drawing.Point(0, 167);
            this.zgcVolatility.Name = "zgcVolatility";
            this.zgcVolatility.ScrollGrace = 0D;
            this.zgcVolatility.ScrollMaxX = 0D;
            this.zgcVolatility.ScrollMaxY = 0D;
            this.zgcVolatility.ScrollMaxY2 = 0D;
            this.zgcVolatility.ScrollMinX = 0D;
            this.zgcVolatility.ScrollMinY = 0D;
            this.zgcVolatility.ScrollMinY2 = 0D;
            this.zgcVolatility.Size = new System.Drawing.Size(556, 164);
            this.zgcVolatility.TabIndex = 27;
            // 
            // frmRiskCharts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(555, 343);
            this.Controls.Add(this.zgcVolatility);
            this.Controls.Add(this.zgcGrossExposure);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRiskCharts";
            this.Text = "Risk Charts";
            this.Load += new System.EventHandler(this.frmRiskCharts_Load);
            this.Resize += new System.EventHandler(this.frmRiskCharts_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcGrossExposure;
        private System.Windows.Forms.Timer tmrRefresh;
        private ZedGraph.ZedGraphControl zgcVolatility;

    }
}