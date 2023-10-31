namespace LiveBook
{
    partial class frmImportDividends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportDividends));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRead = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dtgDividends = new DevExpress.XtraGrid.GridControl();
            this.dgDividends = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDividends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDividends)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 6000;
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(318, 328);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(100, 35);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(424, 328);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dtgDividends
            // 
            this.dtgDividends.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDividends.Location = new System.Drawing.Point(12, 12);
            this.dtgDividends.MainView = this.dgDividends;
            this.dtgDividends.Name = "dtgDividends";
            this.dtgDividends.Size = new System.Drawing.Size(504, 310);
            this.dtgDividends.TabIndex = 33;
            this.dtgDividends.TabStop = false;
            this.dtgDividends.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgDividends});
            // 
            // dgDividends
            // 
            this.dgDividends.GridControl = this.dtgDividends;
            this.dgDividends.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgDividends.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgDividends.Name = "dgDividends";
            this.dgDividends.OptionsBehavior.Editable = false;
            this.dgDividends.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgDividends.OptionsView.ColumnAutoWidth = false;
            this.dgDividends.RowHeight = 15;
            this.dgDividends.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgDividends_CustomDrawCell);
            this.dgDividends.DoubleClick += new System.EventHandler(this.dgDividends_DoubleClick);
            // 
            // frmImportDividends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(528, 375);
            this.Controls.Add(this.dtgDividends);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRead);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(1065, 0);
            this.Name = "frmImportDividends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Dividends";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmImportDividends_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDividends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDividends)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnClose;
        private DevExpress.XtraGrid.GridControl dtgDividends;
        private DevExpress.XtraGrid.Views.Grid.GridView dgDividends;
    }
}