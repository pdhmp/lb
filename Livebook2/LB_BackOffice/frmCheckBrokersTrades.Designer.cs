namespace LiveBook
{
    partial class frmCheckImportedFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckImportedFile));
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.CmdInsertBrokerage = new System.Windows.Forms.Button();
            this.cmdDeleteFile = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblOpenFolder = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdExportMellon = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdFutures = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgBrokerage = new DevExpress.XtraGrid.GridControl();
            this.dgBrokerage = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgDiferencesBroker = new DevExpress.XtraGrid.GridControl();
            this.dgCheckDiferencesBroker = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkShowInserted = new System.Windows.Forms.CheckBox();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBrokerage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrokerage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferencesBroker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheckDiferencesBroker)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(12, 24);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 11);
            this.label1.TabIndex = 24;
            this.label1.Text = "Trade Date";
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(213, 20);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 24);
            this.cmdrefresh.TabIndex = 23;
            this.cmdrefresh.Text = "Load Trades";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Location = new System.Drawing.Point(855, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(138, 38);
            this.groupBox4.TabIndex = 29;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Export";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(71, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 20);
            this.button5.TabIndex = 16;
            this.button5.Text = "Txt";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(59, 20);
            this.button4.TabIndex = 15;
            this.button4.Text = "Excel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // CmdInsertBrokerage
            // 
            this.CmdInsertBrokerage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmdInsertBrokerage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdInsertBrokerage.Location = new System.Drawing.Point(870, 6);
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
            this.cmdDeleteFile.Location = new System.Drawing.Point(870, 233);
            this.cmdDeleteFile.Name = "cmdDeleteFile";
            this.cmdDeleteFile.Size = new System.Drawing.Size(115, 41);
            this.cmdDeleteFile.TabIndex = 33;
            this.cmdDeleteFile.Text = "Delete Imported Files";
            this.cmdDeleteFile.UseVisualStyleBackColor = true;
            this.cmdDeleteFile.Click += new System.EventHandler(this.cmdDeleteFile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 50);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.dtgBrokerage);
            this.splitContainer1.Panel1.Controls.Add(this.CmdInsertBrokerage);
            this.splitContainer1.Panel1.Controls.Add(this.cmdDeleteFile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgDiferencesBroker);
            this.splitContainer1.Size = new System.Drawing.Size(1001, 631);
            this.splitContainer1.SplitterDistance = 294;
            this.splitContainer1.TabIndex = 36;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblOpenFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmdExportMellon);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmdFutures);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(866, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(124, 183);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Mellon File";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Yellow;
            this.panel2.Location = new System.Drawing.Point(7, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(12, 12);
            this.panel2.TabIndex = 30;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MediumOrchid;
            this.panel3.Location = new System.Drawing.Point(7, 156);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(12, 12);
            this.panel3.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Both";
            // 
            // lblOpenFolder
            // 
            this.lblOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOpenFolder.AutoSize = true;
            this.lblOpenFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenFolder.ForeColor = System.Drawing.Color.Blue;
            this.lblOpenFolder.Location = new System.Drawing.Point(32, 92);
            this.lblOpenFolder.Name = "lblOpenFolder";
            this.lblOpenFolder.Size = new System.Drawing.Size(65, 13);
            this.lblOpenFolder.TabIndex = 35;
            this.lblOpenFolder.Text = "Open Folder";
            this.lblOpenFolder.Click += new System.EventHandler(this.lblOpenFolder_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "CAP Exceeded";
            // 
            // cmdExportMellon
            // 
            this.cmdExportMellon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExportMellon.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExportMellon.Location = new System.Drawing.Point(9, 19);
            this.cmdExportMellon.Name = "cmdExportMellon";
            this.cmdExportMellon.Size = new System.Drawing.Size(102, 32);
            this.cmdExportMellon.TabIndex = 32;
            this.cmdExportMellon.Text = "Equities";
            this.cmdExportMellon.UseVisualStyleBackColor = true;
            this.cmdExportMellon.Click += new System.EventHandler(this.cmdExportMellon_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Rebate Difference";
            // 
            // cmdFutures
            // 
            this.cmdFutures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFutures.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdFutures.Location = new System.Drawing.Point(9, 57);
            this.cmdFutures.Name = "cmdFutures";
            this.cmdFutures.Size = new System.Drawing.Size(102, 32);
            this.cmdFutures.TabIndex = 34;
            this.cmdFutures.Text = "Futures";
            this.cmdFutures.UseVisualStyleBackColor = true;
            this.cmdFutures.Click += new System.EventHandler(this.cmdFutures_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(7, 120);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(12, 12);
            this.panel1.TabIndex = 29;
            // 
            // dtgBrokerage
            // 
            this.dtgBrokerage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgBrokerage.Location = new System.Drawing.Point(3, 0);
            this.dtgBrokerage.MainView = this.dgBrokerage;
            this.dtgBrokerage.Name = "dtgBrokerage";
            this.dtgBrokerage.Size = new System.Drawing.Size(852, 291);
            this.dtgBrokerage.TabIndex = 32;
            this.dtgBrokerage.TabStop = false;
            this.dtgBrokerage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgBrokerage});
            // 
            // dgBrokerage
            // 
            this.dgBrokerage.GridControl = this.dtgBrokerage;
            this.dgBrokerage.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgBrokerage.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgBrokerage.Name = "dgBrokerage";
            this.dgBrokerage.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgBrokerage.OptionsSelection.MultiSelect = true;
            this.dgBrokerage.OptionsView.ColumnAutoWidth = false;
            this.dgBrokerage.RowHeight = 15;
            this.dgBrokerage.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgBrokerage_DragObjectDrop);
            this.dgBrokerage.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgBrokerage_CustomDrawCell);
            this.dgBrokerage.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgBrokerage_ColumnWidthChanged);
            // 
            // dtgDiferencesBroker
            // 
            this.dtgDiferencesBroker.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDiferencesBroker.Location = new System.Drawing.Point(3, 3);
            this.dtgDiferencesBroker.MainView = this.dgCheckDiferencesBroker;
            this.dtgDiferencesBroker.Name = "dtgDiferencesBroker";
            this.dtgDiferencesBroker.Size = new System.Drawing.Size(995, 327);
            this.dtgDiferencesBroker.TabIndex = 28;
            this.dtgDiferencesBroker.TabStop = false;
            this.dtgDiferencesBroker.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgCheckDiferencesBroker});
            // 
            // dgCheckDiferencesBroker
            // 
            this.dgCheckDiferencesBroker.GridControl = this.dtgDiferencesBroker;
            this.dgCheckDiferencesBroker.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgCheckDiferencesBroker.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgCheckDiferencesBroker.Name = "dgCheckDiferencesBroker";
            this.dgCheckDiferencesBroker.OptionsBehavior.Editable = false;
            this.dgCheckDiferencesBroker.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgCheckDiferencesBroker.OptionsSelection.MultiSelect = true;
            this.dgCheckDiferencesBroker.OptionsView.ColumnAutoWidth = false;
            this.dgCheckDiferencesBroker.RowHeight = 15;
            this.dgCheckDiferencesBroker.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTradesRel_DragObjectDrop);
            this.dgCheckDiferencesBroker.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgCheckDiferencesBroker_CustomDrawCell);
            this.dgCheckDiferencesBroker.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgTradesRel_CustomDrawGroupRow);
            this.dgCheckDiferencesBroker.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTradesRel_ColumnWidthChanged);
            this.dgCheckDiferencesBroker.ShowCustomizationForm += new System.EventHandler(this.dgTradesRel_ShowCustomizationForm);
            this.dgCheckDiferencesBroker.HideCustomizationForm += new System.EventHandler(this.dgTradesRel_HideCustomizationForm);
            // 
            // chkShowInserted
            // 
            this.chkShowInserted.AutoSize = true;
            this.chkShowInserted.Location = new System.Drawing.Point(113, 27);
            this.chkShowInserted.Name = "chkShowInserted";
            this.chkShowInserted.Size = new System.Drawing.Size(94, 17);
            this.chkShowInserted.TabIndex = 37;
            this.chkShowInserted.Text = "Show Inserted";
            this.chkShowInserted.UseVisualStyleBackColor = true;
            this.chkShowInserted.CheckedChanged += new System.EventHandler(this.chkShowInserted_CheckedChanged);
            // 
            // frmCheckImportedFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1005, 681);
            this.Controls.Add(this.chkShowInserted);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdrefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCheckImportedFile";
            this.Text = "Trade Reconciliation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBrokerage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrokerage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferencesBroker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheckDiferencesBroker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button CmdInsertBrokerage;
        private System.Windows.Forms.Button cmdDeleteFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgBrokerage;
        private DevExpress.XtraGrid.Views.Grid.GridView dgBrokerage;
        private DevExpress.XtraGrid.GridControl dtgDiferencesBroker;
        private DevExpress.XtraGrid.Views.Grid.GridView dgCheckDiferencesBroker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblOpenFolder;
        private System.Windows.Forms.Button cmdExportMellon;
        private System.Windows.Forms.Button cmdFutures;
        private System.Windows.Forms.CheckBox chkShowInserted;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}