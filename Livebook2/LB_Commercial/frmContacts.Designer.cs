namespace LiveBook
{
    partial class frmContacts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContacts));
            this.cmdClose = new System.Windows.Forms.Button();
            this.dtgContacts = new MyXtraGrid.MyGridControl();
            this.dgContacts = new MyXtraGrid.MyGridView();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.webReport = new System.Windows.Forms.WebBrowser();
            this.lblCopy = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dtgContacts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContacts)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(561, 601);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dtgContacts
            // 
            this.dtgContacts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgContacts.Location = new System.Drawing.Point(12, 12);
            this.dtgContacts.LookAndFeel.SkinName = "Blue";
            this.dtgContacts.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgContacts.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgContacts.MainView = this.dgContacts;
            this.dtgContacts.Name = "dtgContacts";
            this.dtgContacts.Size = new System.Drawing.Size(512, 563);
            this.dtgContacts.TabIndex = 0;
            this.dtgContacts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgContacts});
            // 
            // dgContacts
            // 
            this.dgContacts.GridControl = this.dtgContacts;
            this.dgContacts.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgContacts.Name = "dgContacts";
            this.dgContacts.OptionsBehavior.Editable = false;
            this.dgContacts.OptionsSelection.MultiSelect = true;
            this.dgContacts.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgContacts_DragObjectDrop);
            this.dgContacts.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgContacts_CustomDrawGroupRow);
            this.dgContacts.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgContacts_ColumnWidthChanged);
            this.dgContacts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgContacts_MouseUp);
            this.dgContacts.Click += new System.EventHandler(this.dgContacts_Click);
            this.dgContacts.DoubleClick += new System.EventHandler(this.dgContacts_DoubleClick);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(349, 21);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse.TabIndex = 33;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // cmdExpand
            // 
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(284, 21);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand.TabIndex = 32;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // webReport
            // 
            this.webReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webReport.Location = new System.Drawing.Point(530, 12);
            this.webReport.MinimumSize = new System.Drawing.Size(20, 20);
            this.webReport.Name = "webReport";
            this.webReport.Size = new System.Drawing.Size(795, 580);
            this.webReport.TabIndex = 34;
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(493, 579);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 35;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 601);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 35);
            this.button1.TabIndex = 36;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(132, 606);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDate.TabIndex = 72;
            this.dtpDate.TabStop = false;
            // 
            // frmContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1333, 648);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.webReport);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.dtgContacts);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmContacts";
            this.Text = "Fund Investors";
            this.Load += new System.EventHandler(this.frmContacts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgContacts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContacts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private MyXtraGrid.MyGridControl dtgContacts;
        private MyXtraGrid.MyGridView dgContacts;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.WebBrowser webReport;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}