namespace Dashboards
{
    partial class Dashboards
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firewallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qSEGSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.momentumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cBOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orderReviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.positionStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.qSEGSToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ordersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(745, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controllerToolStripMenuItem,
            this.firewallToolStripMenuItem});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // controllerToolStripMenuItem
            // 
            this.controllerToolStripMenuItem.Name = "controllerToolStripMenuItem";
            this.controllerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.controllerToolStripMenuItem.Text = "Monitor";
            this.controllerToolStripMenuItem.Click += new System.EventHandler(this.controllerToolStripMenuItem_Click);
            // 
            // firewallToolStripMenuItem
            // 
            this.firewallToolStripMenuItem.Name = "firewallToolStripMenuItem";
            this.firewallToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.firewallToolStripMenuItem.Text = "Firewall";
            // 
            // qSEGSToolStripMenuItem
            // 
            this.qSEGSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monitorToolStripMenuItem});
            this.qSEGSToolStripMenuItem.Name = "qSEGSToolStripMenuItem";
            this.qSEGSToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.qSEGSToolStripMenuItem.Text = "QSEGS";
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.monitorToolStripMenuItem.Text = "QSEG Monitor";
            this.monitorToolStripMenuItem.Click += new System.EventHandler(this.monitorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.momentumToolStripMenuItem,
            this.cBOToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(68, 20);
            this.toolStripMenuItem1.Text = "Strategies";
            // 
            // momentumToolStripMenuItem
            // 
            this.momentumToolStripMenuItem.Name = "momentumToolStripMenuItem";
            this.momentumToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.momentumToolStripMenuItem.Text = "Momentum";
            this.momentumToolStripMenuItem.Click += new System.EventHandler(this.momentumToolStripMenuItem_Click);
            // 
            // cBOToolStripMenuItem
            // 
            this.cBOToolStripMenuItem.Name = "cBOToolStripMenuItem";
            this.cBOToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.cBOToolStripMenuItem.Text = "CBO";
            this.cBOToolStripMenuItem.Click += new System.EventHandler(this.cBOToolStripMenuItem_Click);
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderReviewToolStripMenuItem,
            this.positionStatusToolStripMenuItem});
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.ordersToolStripMenuItem.Text = "Orders";
            // 
            // orderReviewToolStripMenuItem
            // 
            this.orderReviewToolStripMenuItem.Name = "orderReviewToolStripMenuItem";
            this.orderReviewToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.orderReviewToolStripMenuItem.Text = "OrderReview";
            this.orderReviewToolStripMenuItem.Click += new System.EventHandler(this.orderReviewToolStripMenuItem_Click);
            // 
            // positionStatusToolStripMenuItem
            // 
            this.positionStatusToolStripMenuItem.Name = "positionStatusToolStripMenuItem";
            this.positionStatusToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.positionStatusToolStripMenuItem.Text = "Position Status";
            // 
            // Dashboards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 449);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "Dashboards";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem momentumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cBOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qSEGSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firewallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderReviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem positionStatusToolStripMenuItem;

    }
}