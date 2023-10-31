namespace QuickNestFIX
{
    partial class frmRecSymbol
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
            this.dtgOrders = new NCustomControls.MyGridControl();
            this.dgOrders = new NCustomControls.MyGridView();
            this.chkAuction = new System.Windows.Forms.CheckBox();
            this.btnSendAll = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgOrders
            // 
            this.dtgOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOrders.Location = new System.Drawing.Point(12, 64);
            this.dtgOrders.LookAndFeel.SkinName = "Blue";
            this.dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOrders.MainView = this.dgOrders;
            this.dtgOrders.Name = "dtgOrders";
            this.dtgOrders.Size = new System.Drawing.Size(555, 367);
            this.dtgOrders.TabIndex = 28;
            this.dtgOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOrders});
            this.dtgOrders.Click += new System.EventHandler(this.dtgOrders_Click);
            // 
            // dgOrders
            // 
            this.dgOrders.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgOrders.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgOrders.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgOrders.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgOrders.GridControl = this.dtgOrders;
            this.dgOrders.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgOrders.Name = "dgOrders";
            this.dgOrders.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgOrders.OptionsBehavior.Editable = false;
            this.dgOrders.OptionsSelection.UseIndicatorForSelection = false;
            this.dgOrders.Click += new System.EventHandler(this.dgOrders_Click);
            // 
            // chkAuction
            // 
            this.chkAuction.AutoSize = true;
            this.chkAuction.Location = new System.Drawing.Point(483, 41);
            this.chkAuction.Name = "chkAuction";
            this.chkAuction.Size = new System.Drawing.Size(62, 17);
            this.chkAuction.TabIndex = 30;
            this.chkAuction.Text = "Auction";
            this.chkAuction.UseVisualStyleBackColor = true;
            // 
            // btnSendAll
            // 
            this.btnSendAll.Location = new System.Drawing.Point(450, 12);
            this.btnSendAll.Name = "btnSendAll";
            this.btnSendAll.Size = new System.Drawing.Size(95, 23);
            this.btnSendAll.TabIndex = 31;
            this.btnSendAll.Text = "Send Selected";
            this.btnSendAll.UseVisualStyleBackColor = true;
            this.btnSendAll.Click += new System.EventHandler(this.btnSendAll_Click);
            // 
            // btnMatch
            // 
            this.btnMatch.Location = new System.Drawing.Point(333, 12);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(101, 23);
            this.btnMatch.TabIndex = 32;
            this.btnMatch.Text = "Match Selected";
            this.btnMatch.UseVisualStyleBackColor = true;
            this.btnMatch.Click += new System.EventHandler(this.btnMatch_Click);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 500;
            this.tmrRefresh.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(12, 12);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(128, 23);
            this.btnSelectAll.TabIndex = 33;
            this.btnSelectAll.Tag = "";
            this.btnSelectAll.Text = "Select All Orders";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(212, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 23);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel Selected";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmRecSymbol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(579, 443);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.btnSendAll);
            this.Controls.Add(this.chkAuction);
            this.Controls.Add(this.dtgOrders);
            this.Name = "frmRecSymbol";
            this.Text = "frmRecSymbol";
            this.Load += new System.EventHandler(this.frmRecSymbol_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public NCustomControls.MyGridControl dtgOrders;
        public NCustomControls.MyGridView dgOrders;
        private System.Windows.Forms.CheckBox chkAuction;
        private System.Windows.Forms.Button btnSendAll;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnCancel;
    }
}