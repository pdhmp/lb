namespace LiveBook
{
    partial class frmSubPortfolio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubPortfolio));
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgSummary = new MyXtraGrid.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmbView = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(-1, 28);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgSummary;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(467, 189);
            this.dtg.TabIndex = 25;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgSummary});
            // 
            // dgSummary
            // 
            this.dgSummary.GridControl = this.dtg;
            this.dgSummary.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgSummary.Name = "dgSummary";
            this.dgSummary.OptionsBehavior.Editable = false;
            this.dgSummary.OptionsView.ShowGroupPanel = false;
            this.dgSummary.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgBookSummary_DragObjectDrop);
            this.dgSummary.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgBookSummary_ColumnWidthChanged);
            this.dgSummary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgBookSummary_MouseDown);
            this.dgSummary.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgBookSummary_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 2500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmbView
            // 
            this.cmbView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbView.FormattingEnabled = true;
            this.cmbView.Location = new System.Drawing.Point(310, 3);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(152, 21);
            this.cmbView.TabIndex = 25;
            // 
            // frmSubPortfolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(465, 218);
            this.Controls.Add(this.cmbView);
            this.Controls.Add(this.dtg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSubPortfolio";
            this.Text = "Exposure Sub Portfolio";
            this.Load += new System.EventHandler(this.frmBookSummary_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgSummary;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cmbView;

    }
}