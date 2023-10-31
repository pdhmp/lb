namespace FixLogViewer
{
    partial class frmLogReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogReader));
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgMessages = new NCustomControls.MyGridControl();
            this.dgMessages = new NCustomControls.MyGridView();
            this.cmdChangeDictionary = new System.Windows.Forms.Button();
            this.dtgDetails = new NCustomControls.MyGridControl();
            this.dgDetails = new NCustomControls.MyGridView();
            this.myGridView1 = new NCustomControls.MyGridView();
            this.pgrOpenProgress = new System.Windows.Forms.ProgressBar();
            this.cmdReload = new System.Windows.Forms.Button();
            this.labPercent = new System.Windows.Forms.Label();
            this.radEntireFile = new System.Windows.Forms.RadioButton();
            this.radLast10 = new System.Windows.Forms.RadioButton();
            this.radLast1 = new System.Windows.Forms.RadioButton();
            this.radContains = new System.Windows.Forms.RadioButton();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.labLinesLoaded = new System.Windows.Forms.Label();
            this.chkExcludeAdmin = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Location = new System.Drawing.Point(412, 9);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(75, 23);
            this.cmdOpenFile.TabIndex = 28;
            this.cmdOpenFile.Text = "Open File";
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // openFD
            // 
            this.openFD.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 37);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgMessages);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cmdChangeDictionary);
            this.splitContainer1.Panel2.Controls.Add(this.dtgDetails);
            this.splitContainer1.Size = new System.Drawing.Size(998, 558);
            this.splitContainer1.SplitterDistance = 331;
            this.splitContainer1.TabIndex = 30;
            // 
            // dtgMessages
            // 
            this.dtgMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgMessages.Location = new System.Drawing.Point(3, 3);
            this.dtgMessages.LookAndFeel.SkinName = "Blue";
            this.dtgMessages.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgMessages.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgMessages.MainView = this.dgMessages;
            this.dtgMessages.Name = "dtgMessages";
            this.dtgMessages.Size = new System.Drawing.Size(992, 324);
            this.dtgMessages.TabIndex = 28;
            this.dtgMessages.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgMessages});
            // 
            // dgMessages
            // 
            this.dgMessages.GridControl = this.dtgMessages;
            this.dgMessages.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgMessages.Name = "dgMessages";
            this.dgMessages.OptionsBehavior.Editable = false;
            this.dgMessages.OptionsSelection.MultiSelect = true;
            this.dgMessages.OptionsView.ColumnAutoWidth = false;
            this.dgMessages.OptionsView.ShowGroupPanel = false;
            this.dgMessages.Click += new System.EventHandler(this.dtgMessages_Click);
            // 
            // cmdChangeDictionary
            // 
            this.cmdChangeDictionary.Location = new System.Drawing.Point(652, 3);
            this.cmdChangeDictionary.Name = "cmdChangeDictionary";
            this.cmdChangeDictionary.Size = new System.Drawing.Size(147, 23);
            this.cmdChangeDictionary.TabIndex = 31;
            this.cmdChangeDictionary.Text = "Change Dictionary";
            this.cmdChangeDictionary.UseVisualStyleBackColor = true;
            this.cmdChangeDictionary.Click += new System.EventHandler(this.cmdChangeDictionary_Click);
            // 
            // dtgDetails
            // 
            this.dtgDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgDetails.Location = new System.Drawing.Point(3, 3);
            this.dtgDetails.LookAndFeel.SkinName = "Blue";
            this.dtgDetails.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgDetails.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDetails.MainView = this.dgDetails;
            this.dtgDetails.Name = "dtgDetails";
            this.dtgDetails.Size = new System.Drawing.Size(643, 210);
            this.dtgDetails.TabIndex = 29;
            this.dtgDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgDetails});
            // 
            // dgDetails
            // 
            this.dgDetails.GridControl = this.dtgDetails;
            this.dgDetails.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.OptionsBehavior.Editable = false;
            this.dgDetails.OptionsSelection.MultiSelect = true;
            this.dgDetails.OptionsView.ColumnAutoWidth = false;
            this.dgDetails.OptionsView.ShowGroupPanel = false;
            // 
            // myGridView1
            // 
            this.myGridView1.Name = "myGridView1";
            // 
            // pgrOpenProgress
            // 
            this.pgrOpenProgress.Location = new System.Drawing.Point(494, 9);
            this.pgrOpenProgress.Name = "pgrOpenProgress";
            this.pgrOpenProgress.Size = new System.Drawing.Size(186, 23);
            this.pgrOpenProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgrOpenProgress.TabIndex = 31;
            this.pgrOpenProgress.Visible = false;
            // 
            // cmdReload
            // 
            this.cmdReload.Enabled = false;
            this.cmdReload.Location = new System.Drawing.Point(732, 9);
            this.cmdReload.Name = "cmdReload";
            this.cmdReload.Size = new System.Drawing.Size(75, 23);
            this.cmdReload.TabIndex = 32;
            this.cmdReload.Text = "Reload File";
            this.cmdReload.UseVisualStyleBackColor = true;
            this.cmdReload.Click += new System.EventHandler(this.cmdReload_Click);
            // 
            // labPercent
            // 
            this.labPercent.BackColor = System.Drawing.Color.White;
            this.labPercent.Location = new System.Drawing.Point(686, 12);
            this.labPercent.Name = "labPercent";
            this.labPercent.Size = new System.Drawing.Size(40, 16);
            this.labPercent.TabIndex = 33;
            this.labPercent.Text = "0 %";
            this.labPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labPercent.Visible = false;
            // 
            // radEntireFile
            // 
            this.radEntireFile.AutoSize = true;
            this.radEntireFile.Checked = true;
            this.radEntireFile.Location = new System.Drawing.Point(12, 12);
            this.radEntireFile.Name = "radEntireFile";
            this.radEntireFile.Size = new System.Drawing.Size(71, 17);
            this.radEntireFile.TabIndex = 34;
            this.radEntireFile.TabStop = true;
            this.radEntireFile.Text = "Entire File";
            this.radEntireFile.UseVisualStyleBackColor = true;
            // 
            // radLast10
            // 
            this.radLast10.AutoSize = true;
            this.radLast10.Location = new System.Drawing.Point(89, 12);
            this.radLast10.Name = "radLast10";
            this.radLast10.Size = new System.Drawing.Size(68, 17);
            this.radLast10.TabIndex = 35;
            this.radLast10.Text = "Last 10%";
            this.radLast10.UseVisualStyleBackColor = true;
            // 
            // radLast1
            // 
            this.radLast1.AutoSize = true;
            this.radLast1.Location = new System.Drawing.Point(163, 12);
            this.radLast1.Name = "radLast1";
            this.radLast1.Size = new System.Drawing.Size(62, 17);
            this.radLast1.TabIndex = 36;
            this.radLast1.Text = "Last 1%";
            this.radLast1.UseVisualStyleBackColor = true;
            // 
            // radContains
            // 
            this.radContains.AutoSize = true;
            this.radContains.Location = new System.Drawing.Point(231, 12);
            this.radContains.Name = "radContains";
            this.radContains.Size = new System.Drawing.Size(66, 17);
            this.radContains.TabIndex = 37;
            this.radContains.Text = "Contains";
            this.radContains.UseVisualStyleBackColor = true;
            this.radContains.CheckedChanged += new System.EventHandler(this.radContains_CheckedChanged);
            // 
            // txtFilter
            // 
            this.txtFilter.Enabled = false;
            this.txtFilter.Location = new System.Drawing.Point(304, 12);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(100, 20);
            this.txtFilter.TabIndex = 38;
            // 
            // labLinesLoaded
            // 
            this.labLinesLoaded.BackColor = System.Drawing.Color.White;
            this.labLinesLoaded.Location = new System.Drawing.Point(813, 12);
            this.labLinesLoaded.Name = "labLinesLoaded";
            this.labLinesLoaded.Size = new System.Drawing.Size(148, 16);
            this.labLinesLoaded.TabIndex = 39;
            this.labLinesLoaded.Text = "      ";
            this.labLinesLoaded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkExcludeAdmin
            // 
            this.chkExcludeAdmin.AutoSize = true;
            this.chkExcludeAdmin.Checked = true;
            this.chkExcludeAdmin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExcludeAdmin.Location = new System.Drawing.Point(816, 10);
            this.chkExcludeAdmin.Name = "chkExcludeAdmin";
            this.chkExcludeAdmin.Size = new System.Drawing.Size(119, 17);
            this.chkExcludeAdmin.TabIndex = 40;
            this.chkExcludeAdmin.Text = "Exclude Admin Msg";
            this.chkExcludeAdmin.UseVisualStyleBackColor = true;
            // 
            // frmLogReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1022, 614);
            this.Controls.Add(this.chkExcludeAdmin);
            this.Controls.Add(this.labLinesLoaded);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.radContains);
            this.Controls.Add(this.cmdOpenFile);
            this.Controls.Add(this.radLast1);
            this.Controls.Add(this.pgrOpenProgress);
            this.Controls.Add(this.cmdReload);
            this.Controls.Add(this.radLast10);
            this.Controls.Add(this.labPercent);
            this.Controls.Add(this.radEntireFile);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogReader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FIX Log Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogReader_FormClosing);
            this.Load += new System.EventHandler(this.frmHistCalc_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.OpenFileDialog openFD;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private NCustomControls.MyGridControl dtgMessages;
        private NCustomControls.MyGridView dgMessages;
        private NCustomControls.MyGridView myGridView1;
        private NCustomControls.MyGridControl dtgDetails;
        private NCustomControls.MyGridView dgDetails;
        private System.Windows.Forms.Button cmdChangeDictionary;
        private System.Windows.Forms.ProgressBar pgrOpenProgress;
        private System.Windows.Forms.Button cmdReload;
        private System.Windows.Forms.Label labPercent;
        private System.Windows.Forms.RadioButton radEntireFile;
        private System.Windows.Forms.RadioButton radLast10;
        private System.Windows.Forms.RadioButton radLast1;
        private System.Windows.Forms.RadioButton radContains;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label labLinesLoaded;
        private System.Windows.Forms.CheckBox chkExcludeAdmin;
    }
}

