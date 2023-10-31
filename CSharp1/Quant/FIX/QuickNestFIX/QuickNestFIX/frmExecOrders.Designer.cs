namespace QuickNestFIX
{
    partial class frmExecOrders
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dtgOrders = new NCustomControls.MyGridControl();
            this.dgOrders = new NCustomControls.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 30;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dtgOrders
            // 
            this.dtgOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgOrders.Location = new System.Drawing.Point(12, 37);
            this.dtgOrders.LookAndFeel.SkinName = "Blue";
            this.dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOrders.MainView = this.dgOrders;
            this.dtgOrders.Name = "dtgOrders";
            this.dtgOrders.Size = new System.Drawing.Size(820, 445);
            this.dtgOrders.TabIndex = 29;
            this.dtgOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOrders});
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
            // 
            // frmExecOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(844, 494);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dtgOrders);
            this.Name = "frmExecOrders";
            this.Text = "frmExecOrders";
            this.Load += new System.EventHandler(this.frmExecOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        public NCustomControls.MyGridControl dtgOrders;
        public NCustomControls.MyGridView dgOrders;
    }
}