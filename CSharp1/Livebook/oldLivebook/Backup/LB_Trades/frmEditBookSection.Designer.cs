namespace SGN
{
    partial class frmEditBookSection
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
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgEditBookSection = new MyXtraGrid.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditBookSection)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(-1, 0);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgEditBookSection;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(722, 350);
            this.dtg.TabIndex = 26;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgEditBookSection});
            // 
            // dgEditBookSection
            // 
            this.dgEditBookSection.GridControl = this.dtg;
            this.dgEditBookSection.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgEditBookSection.Name = "dgEditBookSection";
            this.dgEditBookSection.OptionsBehavior.Editable = false;
            this.dgEditBookSection.OptionsView.ShowIndicator = false;
            this.dgEditBookSection.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgEditStrategy_CustomDrawGroupRow);
            this.dgEditBookSection.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgEditStrategy_MouseUp);
            this.dgEditBookSection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgEditStrategy_MouseDown);
            this.dgEditBookSection.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgEditStrategy_DragObjectDrop);
            this.dgEditBookSection.DoubleClick += new System.EventHandler(this.dgEditStrategy_DoubleClick);
            this.dgEditBookSection.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgEditStrategy_ColumnWidthChanged);
            this.dgEditBookSection.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgEditStrategy_RowStyle);
            // 
            // timer1
            // 
            this.timer1.Interval = 4000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(240, 7);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 27;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // frmEditBookSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 352);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.dtg);
            this.Name = "frmEditBookSection";
            this.Text = "Edit Book & Section";
            this.Load += new System.EventHandler(this.frmEditStrategy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditBookSection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgEditBookSection;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdRefresh;
    }
}