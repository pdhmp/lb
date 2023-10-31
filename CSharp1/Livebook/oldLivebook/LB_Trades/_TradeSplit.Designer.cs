namespace LiveBook
{
    partial class _TradeSplit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_TradeSplit));
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgTradeSplit = new MyXtraGrid.MyGridView();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdSplitAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblCopy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradeSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(-1, -1);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgTradeSplit;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(823, 492);
            this.dtg.TabIndex = 25;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTradeSplit});
            // 
            // dgTradeSplit
            // 
            this.dgTradeSplit.GridControl = this.dtg;
            this.dgTradeSplit.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTradeSplit.Name = "dgTradeSplit";
            this.dgTradeSplit.OptionsBehavior.Editable = false;
            this.dgTradeSplit.OptionsSelection.MultiSelect = true;
            this.dgTradeSplit.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgTradeSplit.OptionsView.ShowIndicator = false;
            this.dgTradeSplit.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTradeSplit_DragObjectDrop);
            this.dgTradeSplit.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgTradeSplit_RowStyle);
            this.dgTradeSplit.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTradeSplit_ColumnWidthChanged);
            this.dgTradeSplit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgTradeSplit_MouseDown);
            this.dgTradeSplit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgTradeSplit_MouseUp);
            this.dgTradeSplit.DoubleClick += new System.EventHandler(this.dgTradeSplit_DoubleClick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(718, 6);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 26;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdSplitAll
            // 
            this.cmdSplitAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSplitAll.Location = new System.Drawing.Point(637, 6);
            this.cmdSplitAll.Name = "cmdSplitAll";
            this.cmdSplitAll.Size = new System.Drawing.Size(75, 23);
            this.cmdSplitAll.TabIndex = 27;
            this.cmdSplitAll.Text = "Split All";
            this.cmdSplitAll.UseVisualStyleBackColor = true;
            this.cmdSplitAll.Click += new System.EventHandler(this.cmdSplitAll_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(484, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Last all split: ";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.lblLastUpdate.ForeColor = System.Drawing.Color.White;
            this.lblLastUpdate.Location = new System.Drawing.Point(557, 16);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(60, 13);
            this.lblLastUpdate.TabIndex = 29;
            this.lblLastUpdate.Text = "time";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 1000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(789, 475);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 36;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // frmTradeSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(821, 492);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdSplitAll);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.dtg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTradeSplit";
            this.Text = "Broker Account Order Split";
            this.Load += new System.EventHandler(this._frmTradeSplit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradeSplit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgTradeSplit;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdSplitAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblCopy;

    }
}