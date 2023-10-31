namespace LiveTrade2.SystemForms
{
    partial class frmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.mnuTeste = new System.Windows.Forms.MenuStrip();
            this.menuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionChainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditLimitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuickChartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTeste.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuTeste
            // 
            this.mnuTeste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOptions,
            this.mnuAdmin});
            this.mnuTeste.Location = new System.Drawing.Point(0, 0);
            this.mnuTeste.Name = "mnuTeste";
            this.mnuTeste.Size = new System.Drawing.Size(1242, 24);
            this.mnuTeste.TabIndex = 1;
            this.mnuTeste.Text = "menuStrip1";
            // 
            // menuOptions
            // 
            this.menuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionChainToolStripMenuItem,
            this.EditLimitToolStripMenuItem,
            this.QuickChartToolStripMenuItem});
            this.menuOptions.Name = "menuOptions";
            this.menuOptions.Size = new System.Drawing.Size(50, 20);
            this.menuOptions.Text = "Menu";
            // 
            // OptionChainToolStripMenuItem
            // 
            this.OptionChainToolStripMenuItem.Name = "OptionChainToolStripMenuItem";
            this.OptionChainToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OptionChainToolStripMenuItem.Text = "Option Chain";
            this.OptionChainToolStripMenuItem.Click += new System.EventHandler(this.positionsToolStripMenuItem2_Click);
            // 
            // EditLimitToolStripMenuItem
            // 
            this.EditLimitToolStripMenuItem.Name = "EditLimitToolStripMenuItem";
            this.EditLimitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.EditLimitToolStripMenuItem.Text = "Edit Limits";
            this.EditLimitToolStripMenuItem.Click += new System.EventHandler(this.EditLimitToolStripMenuItem_Click);
            // 
            // QuickChartToolStripMenuItem
            // 
            this.QuickChartToolStripMenuItem.Name = "QuickChartToolStripMenuItem";
            this.QuickChartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.QuickChartToolStripMenuItem.Text = "Quick Chart";
            this.QuickChartToolStripMenuItem.Click += new System.EventHandler(this.QuickChartToolStripMenuItem_Click);
            // 
            // mnuAdmin
            // 
            this.mnuAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePasswordToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.mnuAdmin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mnuAdmin.ImageTransparentColor = System.Drawing.Color.DarkOrange;
            this.mnuAdmin.Name = "mnuAdmin";
            this.mnuAdmin.Size = new System.Drawing.Size(48, 20);
            this.mnuAdmin.Text = "&Tools";
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("changePasswordToolStripMenuItem.Image")));
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 780);
            this.Controls.Add(this.mnuTeste);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuTeste;
            this.Name = "frmMenu";
            this.Text = "Nest LiveTrade";
            this.mnuTeste.ResumeLayout(false);
            this.mnuTeste.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuTeste;
        private System.Windows.Forms.ToolStripMenuItem menuOptions;
        private System.Windows.Forms.ToolStripMenuItem OptionChainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditLimitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem QuickChartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuAdmin;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}