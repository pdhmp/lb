namespace LiveBook
{
    partial class frmAccounts
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
            this.chkByPortfolio = new System.Windows.Forms.CheckBox();
            this.chkByBroker = new System.Windows.Forms.CheckBox();
            this.cmbBroker = new System.Windows.Forms.ComboBox();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.GridControl = new MyXtraGrid.MyGridControl();
            this.GridView = new MyXtraGrid.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // chkByPortfolio
            // 
            this.chkByPortfolio.AutoSize = true;
            this.chkByPortfolio.Location = new System.Drawing.Point(12, 14);
            this.chkByPortfolio.Name = "chkByPortfolio";
            this.chkByPortfolio.Size = new System.Drawing.Size(111, 17);
            this.chkByPortfolio.TabIndex = 1;
            this.chkByPortfolio.Text = "Select by Portfolio";
            this.chkByPortfolio.UseVisualStyleBackColor = true;
            this.chkByPortfolio.CheckedChanged += new System.EventHandler(this.chkByPortfolio_CheckedChanged);
            // 
            // chkByBroker
            // 
            this.chkByBroker.AutoSize = true;
            this.chkByBroker.Location = new System.Drawing.Point(12, 37);
            this.chkByBroker.Name = "chkByBroker";
            this.chkByBroker.Size = new System.Drawing.Size(104, 17);
            this.chkByBroker.TabIndex = 2;
            this.chkByBroker.Text = "Select by Broker";
            this.chkByBroker.UseVisualStyleBackColor = true;
            this.chkByBroker.CheckedChanged += new System.EventHandler(this.chkByBroker_CheckedChanged);
            // 
            // cmbBroker
            // 
            this.cmbBroker.Enabled = false;
            this.cmbBroker.FormattingEnabled = true;
            this.cmbBroker.Location = new System.Drawing.Point(129, 35);
            this.cmbBroker.Name = "cmbBroker";
            this.cmbBroker.Size = new System.Drawing.Size(228, 21);
            this.cmbBroker.TabIndex = 3;
            this.cmbBroker.Text = "cmbBroker";
            this.cmbBroker.SelectedIndexChanged += new System.EventHandler(this.cmbBroker_SelectedIndexChanged);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Enabled = false;
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(129, 12);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(228, 21);
            this.cmbPortfolio.TabIndex = 4;
            this.cmbPortfolio.Text = "cmbPortfolio";
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(363, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(83, 42);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // GridControl
            // 
            this.GridControl.Location = new System.Drawing.Point(12, 62);
            this.GridControl.LookAndFeel.SkinName = "Blue";
            this.GridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridControl.MainView = this.GridView;
            this.GridControl.Name = "GridControl";
            this.GridControl.Size = new System.Drawing.Size(434, 250);
            this.GridControl.TabIndex = 6;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView});
            // 
            // GridView
            // 
            this.GridView.GridControl = this.GridControl;
            this.GridView.Name = "GridView";
            // 
            // frmAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(458, 324);
            this.Controls.Add(this.GridControl);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.cmbBroker);
            this.Controls.Add(this.chkByBroker);
            this.Controls.Add(this.chkByPortfolio);
            this.Name = "frmAccounts";
            this.Text = "Accounts";
            this.Load += new System.EventHandler(this.frmAccounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkByPortfolio;
        private System.Windows.Forms.CheckBox chkByBroker;
        private System.Windows.Forms.ComboBox cmbBroker;
        private System.Windows.Forms.ComboBox cmbPortfolio;
        private System.Windows.Forms.Button btnUpdate;
        private MyXtraGrid.MyGridControl GridControl;
        private MyXtraGrid.MyGridView GridView;


    }
}