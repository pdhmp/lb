namespace LiveBook
{
    partial class frmCheckBrokersTradesFut
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
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgBrokerageFut = new DevExpress.XtraGrid.GridControl();
            this.dgBrokerageFut = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.CmdInsertBrokerage = new System.Windows.Forms.Button();
            this.cmdDeleteFile = new System.Windows.Forms.Button();
            this.dtgDiferencesBrokerFut = new DevExpress.XtraGrid.GridControl();
            this.dgCheckDiferencesBrokerFut = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBrokerageFut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrokerageFut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferencesBrokerFut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheckDiferencesBrokerFut)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(12, 24);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 34;
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(113, 20);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 24);
            this.cmdrefresh.TabIndex = 33;
            this.cmdrefresh.Text = "Load Trades";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 11);
            this.label1.TabIndex = 39;
            this.label1.Text = "Trade Date";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(14, 50);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgBrokerageFut);
            this.splitContainer1.Panel1.Controls.Add(this.CmdInsertBrokerage);
            this.splitContainer1.Panel1.Controls.Add(this.cmdDeleteFile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgDiferencesBrokerFut);
            this.splitContainer1.Size = new System.Drawing.Size(1093, 477);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 40;
            // 
            // dtgBrokerageFut
            // 
            this.dtgBrokerageFut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgBrokerageFut.Location = new System.Drawing.Point(3, 0);
            this.dtgBrokerageFut.MainView = this.dgBrokerageFut;
            this.dtgBrokerageFut.Name = "dtgBrokerageFut";
            this.dtgBrokerageFut.Size = new System.Drawing.Size(951, 235);
            this.dtgBrokerageFut.TabIndex = 32;
            this.dtgBrokerageFut.TabStop = false;
            this.dtgBrokerageFut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgBrokerageFut});
            // 
            // dgBrokerageFut
            // 
            this.dgBrokerageFut.GridControl = this.dtgBrokerageFut;
            this.dgBrokerageFut.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgBrokerageFut.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgBrokerageFut.Name = "dgBrokerageFut";
            this.dgBrokerageFut.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgBrokerageFut.OptionsSelection.MultiSelect = true;
            this.dgBrokerageFut.OptionsView.ColumnAutoWidth = false;
            this.dgBrokerageFut.RowHeight = 15;
            this.dgBrokerageFut.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgBrokerageFut_DragObjectDrop);
            this.dgBrokerageFut.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgBrokerageFut_ColumnWidthChanged);
            // 
            // CmdInsertBrokerage
            // 
            this.CmdInsertBrokerage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmdInsertBrokerage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdInsertBrokerage.Location = new System.Drawing.Point(967, 6);
            this.CmdInsertBrokerage.Name = "CmdInsertBrokerage";
            this.CmdInsertBrokerage.Size = new System.Drawing.Size(115, 32);
            this.CmdInsertBrokerage.TabIndex = 30;
            this.CmdInsertBrokerage.Text = "Insert Brokerage";
            this.CmdInsertBrokerage.UseVisualStyleBackColor = true;
            this.CmdInsertBrokerage.Click += new System.EventHandler(this.CmdInsertBrokerage_Click);
            // 
            // cmdDeleteFile
            // 
            this.cmdDeleteFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDeleteFile.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeleteFile.Location = new System.Drawing.Point(967, 186);
            this.cmdDeleteFile.Name = "cmdDeleteFile";
            this.cmdDeleteFile.Size = new System.Drawing.Size(115, 41);
            this.cmdDeleteFile.TabIndex = 33;
            this.cmdDeleteFile.Text = "Delete Imported Files";
            this.cmdDeleteFile.UseVisualStyleBackColor = true;
            // 
            // dtgDiferencesBrokerFut
            // 
            this.dtgDiferencesBrokerFut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDiferencesBrokerFut.Location = new System.Drawing.Point(3, 3);
            this.dtgDiferencesBrokerFut.MainView = this.dgCheckDiferencesBrokerFut;
            this.dtgDiferencesBrokerFut.Name = "dtgDiferencesBrokerFut";
            this.dtgDiferencesBrokerFut.Size = new System.Drawing.Size(1087, 229);
            this.dtgDiferencesBrokerFut.TabIndex = 28;
            this.dtgDiferencesBrokerFut.TabStop = false;
            this.dtgDiferencesBrokerFut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgCheckDiferencesBrokerFut});
            // 
            // dgCheckDiferencesBrokerFut
            // 
            this.dgCheckDiferencesBrokerFut.GridControl = this.dtgDiferencesBrokerFut;
            this.dgCheckDiferencesBrokerFut.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgCheckDiferencesBrokerFut.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgCheckDiferencesBrokerFut.Name = "dgCheckDiferencesBrokerFut";
            this.dgCheckDiferencesBrokerFut.OptionsBehavior.Editable = false;
            this.dgCheckDiferencesBrokerFut.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgCheckDiferencesBrokerFut.OptionsSelection.MultiSelect = true;
            this.dgCheckDiferencesBrokerFut.OptionsView.ColumnAutoWidth = false;
            this.dgCheckDiferencesBrokerFut.RowHeight = 15;
            this.dgCheckDiferencesBrokerFut.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgCheckDiferencesBrokerFut_DragObjectDrop);
            this.dgCheckDiferencesBrokerFut.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgCheckDiferencesBrokerFut_CustomDrawCell);
            this.dgCheckDiferencesBrokerFut.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgCheckDiferencesBrokerFut_ColumnWidthChanged);
            // 
            // frmCheckBrokersTradesFut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1119, 540);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.cmdrefresh);
            this.Name = "frmCheckBrokersTradesFut";
            this.Text = "Trade Reconciliation Futures";
            this.Load += new System.EventHandler(this.frmCheckBrokersTradesFut_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgBrokerageFut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrokerageFut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferencesBrokerFut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheckDiferencesBrokerFut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgBrokerageFut;
        private DevExpress.XtraGrid.Views.Grid.GridView dgBrokerageFut;
        private System.Windows.Forms.Button CmdInsertBrokerage;
        private System.Windows.Forms.Button cmdDeleteFile;
        private DevExpress.XtraGrid.GridControl dtgDiferencesBrokerFut;
        private DevExpress.XtraGrid.Views.Grid.GridView dgCheckDiferencesBrokerFut;
    }
}