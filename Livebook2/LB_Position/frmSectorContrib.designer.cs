namespace LiveBook
{
    partial class frmSectorContrib
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSectorContrib));
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.zgcSector = new ZedGraph.ZedGraphControl();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.cmbSector = new System.Windows.Forms.ComboBox();
            this.cmbSecurity = new System.Windows.Forms.ComboBox();
            this.zgcSecurity = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 60000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // zgcSector
            // 
            this.zgcSector.AllowDrop = true;
            this.zgcSector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
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
            this.zgcSector.Size = new System.Drawing.Size(438, 318);
            this.zgcSector.TabIndex = 0;
            //this.zgcSector.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcSector_MouseDownEvent);
            //this.zgcSector.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcSector_MouseUpEvent);
            //this.zgcSector.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcSector_MouseMoveEvent);
            //this.zgcSector.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zgcSector_MouseMove);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(115, 8);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 26;
            // 
            // cmbSector
            // 
            this.cmbSector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSector.FormattingEnabled = true;
            this.cmbSector.Location = new System.Drawing.Point(273, 8);
            this.cmbSector.Name = "cmbSector";
            this.cmbSector.Size = new System.Drawing.Size(152, 21);
            this.cmbSector.TabIndex = 27;
            // 
            // cmbSecurity
            // 
            this.cmbSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSecurity.FormattingEnabled = true;
            this.cmbSecurity.Location = new System.Drawing.Point(273, 359);
            this.cmbSecurity.Name = "cmbSecurity";
            this.cmbSecurity.Size = new System.Drawing.Size(152, 21);
            this.cmbSecurity.TabIndex = 28;
            this.cmbSecurity.SelectedIndexChanged += new System.EventHandler(this.cmbSecurity_SelectedIndexChanged);
            // 
            // zgcSecurity
            // 
            this.zgcSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcSecurity.Location = new System.Drawing.Point(0, 386);
            this.zgcSecurity.Name = "zgcSecurity";
            this.zgcSecurity.ScrollGrace = 0D;
            this.zgcSecurity.ScrollMaxX = 0D;
            this.zgcSecurity.ScrollMaxY = 0D;
            this.zgcSecurity.ScrollMaxY2 = 0D;
            this.zgcSecurity.ScrollMinX = 0D;
            this.zgcSecurity.ScrollMinY = 0D;
            this.zgcSecurity.ScrollMinY2 = 0D;
            this.zgcSecurity.Size = new System.Drawing.Size(438, 338);
            this.zgcSecurity.TabIndex = 29;
            // 
            // frmSectorContrib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 723);
            this.Controls.Add(this.zgcSecurity);
            this.Controls.Add(this.cmbSecurity);
            this.Controls.Add(this.cmbSector);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.zgcSector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSectorContrib";
            this.Text = "Sector Contribution";
            this.Load += new System.EventHandler(this.frmSectorContrib_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcSector;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.ComboBox cmbPortfolio;
        private System.Windows.Forms.ComboBox cmbSector;
        private System.Windows.Forms.ComboBox cmbSecurity;
        private ZedGraph.ZedGraphControl zgcSecurity;
    }
}