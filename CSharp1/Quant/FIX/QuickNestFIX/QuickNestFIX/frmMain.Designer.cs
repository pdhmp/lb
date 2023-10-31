namespace QuickNestFIX
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receivedOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receivedBySymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.execOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(942, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.receivedOrdersToolStripMenuItem,
            this.receivedBySymbolToolStripMenuItem,
            this.execOrdersToolStripMenuItem});
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.ordersToolStripMenuItem.Text = "Orders";
            // 
            // receivedOrdersToolStripMenuItem
            // 
            this.receivedOrdersToolStripMenuItem.Name = "receivedOrdersToolStripMenuItem";
            this.receivedOrdersToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.receivedOrdersToolStripMenuItem.Text = "Received Orders";
            this.receivedOrdersToolStripMenuItem.Click += new System.EventHandler(this.receivedOrdersToolStripMenuItem_Click);
            // 
            // receivedBySymbolToolStripMenuItem
            // 
            this.receivedBySymbolToolStripMenuItem.Name = "receivedBySymbolToolStripMenuItem";
            this.receivedBySymbolToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.receivedBySymbolToolStripMenuItem.Text = "Received by Symbol";
            this.receivedBySymbolToolStripMenuItem.Click += new System.EventHandler(this.receivedBySymbolToolStripMenuItem_Click);
            // 
            // execOrdersToolStripMenuItem
            // 
            this.execOrdersToolStripMenuItem.Name = "execOrdersToolStripMenuItem";
            this.execOrdersToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.execOrdersToolStripMenuItem.Text = "ExecOrders";
            this.execOrdersToolStripMenuItem.Click += new System.EventHandler(this.execOrdersToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 580);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Quick Nest Fix";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receivedOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receivedBySymbolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem execOrdersToolStripMenuItem;
    }
}