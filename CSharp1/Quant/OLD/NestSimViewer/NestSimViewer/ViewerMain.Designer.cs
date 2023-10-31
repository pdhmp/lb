namespace NestQuant.Common
{
    partial class ViewerMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewerMain));
            this.cmdRunStrat = new System.Windows.Forms.Button();
            this.treeMain = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.zgcGrossNet = new ZedGraph.ZedGraphControl();
            this.lblCopy = new System.Windows.Forms.Label();
            this.zgcGR100 = new ZedGraph.ZedGraphControl();
            this.labItemName = new System.Windows.Forms.Label();
            this.labSharpe = new System.Windows.Forms.Label();
            this.labStDev = new System.Windows.Forms.Label();
            this.labReturn = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radYear = new System.Windows.Forms.RadioButton();
            this.radMonth = new System.Windows.Forms.RadioButton();
            this.radDay = new System.Windows.Forms.RadioButton();
            this.dtgViewer = new MyXtraGrid.MyGridControl();
            this.dtvViewer = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblLoadTime = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtvViewer)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdRunStrat
            // 
            this.cmdRunStrat.Location = new System.Drawing.Point(13, 12);
            this.cmdRunStrat.Name = "cmdRunStrat";
            this.cmdRunStrat.Size = new System.Drawing.Size(89, 23);
            this.cmdRunStrat.TabIndex = 3;
            this.cmdRunStrat.Text = "Run";
            this.cmdRunStrat.UseVisualStyleBackColor = true;
            this.cmdRunStrat.Click += new System.EventHandler(this.cmdRunStrat_Click);
            // 
            // treeMain
            // 
            this.treeMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeMain.Location = new System.Drawing.Point(3, 3);
            this.treeMain.Name = "treeMain";
            this.treeMain.Size = new System.Drawing.Size(242, 561);
            this.treeMain.TabIndex = 5;
            this.treeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeMain_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.zgcGrossNet);
            this.splitContainer1.Panel2.Controls.Add(this.lblCopy);
            this.splitContainer1.Panel2.Controls.Add(this.zgcGR100);
            this.splitContainer1.Panel2.Controls.Add(this.labItemName);
            this.splitContainer1.Panel2.Controls.Add(this.labSharpe);
            this.splitContainer1.Panel2.Controls.Add(this.labStDev);
            this.splitContainer1.Panel2.Controls.Add(this.labReturn);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.radYear);
            this.splitContainer1.Panel2.Controls.Add(this.radMonth);
            this.splitContainer1.Panel2.Controls.Add(this.radDay);
            this.splitContainer1.Panel2.Controls.Add(this.dtgViewer);
            this.splitContainer1.Panel2.Controls.Add(this.lblLoadTime);
            this.splitContainer1.Panel2.Controls.Add(this.cmdRunStrat);
            this.splitContainer1.Size = new System.Drawing.Size(990, 567);
            this.splitContainer1.SplitterDistance = 248;
            this.splitContainer1.TabIndex = 6;
            // 
            // zgcGrossNet
            // 
            this.zgcGrossNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcGrossNet.Location = new System.Drawing.Point(433, 12);
            this.zgcGrossNet.Name = "zgcGrossNet";
            this.zgcGrossNet.ScrollGrace = 0;
            this.zgcGrossNet.ScrollMaxX = 0;
            this.zgcGrossNet.ScrollMaxY = 0;
            this.zgcGrossNet.ScrollMaxY2 = 0;
            this.zgcGrossNet.ScrollMinX = 0;
            this.zgcGrossNet.ScrollMinY = 0;
            this.zgcGrossNet.ScrollMinY2 = 0;
            this.zgcGrossNet.Size = new System.Drawing.Size(302, 186);
            this.zgcGrossNet.TabIndex = 41;
            this.zgcGrossNet.Visible = false;
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(704, 237);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 40;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Visible = false;
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // zgcGR100
            // 
            this.zgcGR100.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zgcGR100.Location = new System.Drawing.Point(133, 12);
            this.zgcGR100.Name = "zgcGR100";
            this.zgcGR100.ScrollGrace = 0;
            this.zgcGR100.ScrollMaxX = 0;
            this.zgcGR100.ScrollMaxY = 0;
            this.zgcGR100.ScrollMaxY2 = 0;
            this.zgcGR100.ScrollMinX = 0;
            this.zgcGR100.ScrollMinY = 0;
            this.zgcGR100.ScrollMinY2 = 0;
            this.zgcGR100.Size = new System.Drawing.Size(294, 186);
            this.zgcGR100.TabIndex = 36;
            this.zgcGR100.Visible = false;
            // 
            // labItemName
            // 
            this.labItemName.AutoSize = true;
            this.labItemName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labItemName.ForeColor = System.Drawing.Color.Navy;
            this.labItemName.Location = new System.Drawing.Point(11, 47);
            this.labItemName.Name = "labItemName";
            this.labItemName.Size = new System.Drawing.Size(110, 26);
            this.labItemName.TabIndex = 16;
            this.labItemName.Text = "Item Name";
            // 
            // labSharpe
            // 
            this.labSharpe.AutoSize = true;
            this.labSharpe.Location = new System.Drawing.Point(66, 139);
            this.labSharpe.Name = "labSharpe";
            this.labSharpe.Size = new System.Drawing.Size(36, 13);
            this.labSharpe.TabIndex = 15;
            this.labSharpe.Text = "0.00%";
            // 
            // labStDev
            // 
            this.labStDev.AutoSize = true;
            this.labStDev.Location = new System.Drawing.Point(66, 113);
            this.labStDev.Name = "labStDev";
            this.labStDev.Size = new System.Drawing.Size(36, 13);
            this.labStDev.TabIndex = 14;
            this.labStDev.Text = "0.00%";
            // 
            // labReturn
            // 
            this.labReturn.AutoSize = true;
            this.labReturn.Location = new System.Drawing.Point(66, 88);
            this.labReturn.Name = "labReturn";
            this.labReturn.Size = new System.Drawing.Size(36, 13);
            this.labReturn.TabIndex = 13;
            this.labReturn.Text = "0.00%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Sharpe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "StDev";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Return";
            // 
            // radYear
            // 
            this.radYear.AutoSize = true;
            this.radYear.Location = new System.Drawing.Point(119, 235);
            this.radYear.Name = "radYear";
            this.radYear.Size = new System.Drawing.Size(47, 17);
            this.radYear.TabIndex = 9;
            this.radYear.Text = "Year";
            this.radYear.UseVisualStyleBackColor = true;
            this.radYear.Visible = false;
            this.radYear.CheckedChanged += new System.EventHandler(this.radYear_CheckedChanged);
            // 
            // radMonth
            // 
            this.radMonth.AutoSize = true;
            this.radMonth.Location = new System.Drawing.Point(58, 235);
            this.radMonth.Name = "radMonth";
            this.radMonth.Size = new System.Drawing.Size(55, 17);
            this.radMonth.TabIndex = 8;
            this.radMonth.Text = "Month";
            this.radMonth.UseVisualStyleBackColor = true;
            this.radMonth.Visible = false;
            this.radMonth.CheckedChanged += new System.EventHandler(this.radMonth_CheckedChanged);
            // 
            // radDay
            // 
            this.radDay.AutoSize = true;
            this.radDay.Checked = true;
            this.radDay.Location = new System.Drawing.Point(8, 235);
            this.radDay.Name = "radDay";
            this.radDay.Size = new System.Drawing.Size(44, 17);
            this.radDay.TabIndex = 7;
            this.radDay.TabStop = true;
            this.radDay.Text = "Day";
            this.radDay.UseVisualStyleBackColor = true;
            this.radDay.Visible = false;
            this.radDay.CheckedChanged += new System.EventHandler(this.radDay_CheckedChanged);
            // 
            // dtgViewer
            // 
            this.dtgViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgViewer.Location = new System.Drawing.Point(3, 258);
            this.dtgViewer.LookAndFeel.SkinName = "Blue";
            this.dtgViewer.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgViewer.MainView = this.dtvViewer;
            this.dtgViewer.Name = "dtgViewer";
            this.dtgViewer.Size = new System.Drawing.Size(732, 306);
            this.dtgViewer.TabIndex = 6;
            this.dtgViewer.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtvViewer});
            this.dtgViewer.Visible = false;
            // 
            // dtvViewer
            // 
            this.dtvViewer.GridControl = this.dtgViewer;
            this.dtvViewer.Name = "dtvViewer";
            this.dtvViewer.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtvViewer.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtvViewer.OptionsBehavior.Editable = false;
            this.dtvViewer.OptionsSelection.MultiSelect = true;
            this.dtvViewer.OptionsView.ColumnAutoWidth = false;
            this.dtvViewer.OptionsView.ShowGroupPanel = false;
            this.dtvViewer.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dtvViewer_CustomDrawGroupRow);
            this.dtvViewer.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dtvViewer_CustomDrawCell);
            // 
            // lblLoadTime
            // 
            this.lblLoadTime.AutoSize = true;
            this.lblLoadTime.Location = new System.Drawing.Point(127, 17);
            this.lblLoadTime.Name = "lblLoadTime";
            this.lblLoadTime.Size = new System.Drawing.Size(0, 13);
            this.lblLoadTime.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(990, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // ViewerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(990, 595);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ViewerMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SymViewer 0.5";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtvViewer)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdRunStrat;
        private System.Windows.Forms.TreeView treeMain;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblLoadTime;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private MyXtraGrid.MyGridControl dtgViewer;
        private DevExpress.XtraGrid.Views.Grid.GridView dtvViewer;
        private System.Windows.Forms.RadioButton radYear;
        private System.Windows.Forms.RadioButton radMonth;
        private System.Windows.Forms.RadioButton radDay;
        private System.Windows.Forms.Label labSharpe;
        private System.Windows.Forms.Label labStDev;
        private System.Windows.Forms.Label labReturn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labItemName;
        private ZedGraph.ZedGraphControl zgcGR100;
        private System.Windows.Forms.Label lblCopy;
        private ZedGraph.ZedGraphControl zgcGrossNet;
    }
}