namespace LiveBook
{
    partial class frmContacts_Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmContacts_Report));
            this.cmdClose = new System.Windows.Forms.Button();
            this.dtgContReport = new MyXtraGrid.MyGridControl();
            this.dgContReport = new MyXtraGrid.MyGridView();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.lblCopy = new System.Windows.Forms.Label();
            this.dtgRedemptions = new MyXtraGrid.MyGridControl();
            this.dgRedemptions = new MyXtraGrid.MyGridView();
            this.lblCopy2 = new System.Windows.Forms.Label();
            this.cmdExpand2 = new System.Windows.Forms.Button();
            this.cmdCollapse2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgContReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRedemptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRedemptions)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmdClose.Location = new System.Drawing.Point(506, 718);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dtgContReport
            // 
            this.dtgContReport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgContReport.Location = new System.Drawing.Point(12, 12);
            this.dtgContReport.LookAndFeel.SkinName = "Blue";
            this.dtgContReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgContReport.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgContReport.MainView = this.dgContReport;
            this.dtgContReport.Name = "dtgContReport";
            this.dtgContReport.Size = new System.Drawing.Size(1079, 364);
            this.dtgContReport.TabIndex = 0;
            this.dtgContReport.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgContReport});
            // 
            // dgContReport
            // 
            this.dgContReport.GridControl = this.dtgContReport;
            this.dgContReport.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgContReport.Name = "dgContReport";
            this.dgContReport.OptionsBehavior.Editable = false;
            this.dgContReport.OptionsSelection.MultiSelect = true;
            this.dgContReport.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgContReport.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.Default_CustomDrawGroupRow);
            this.dgContReport.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgContReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgContReport_MouseUp_1);
            this.dgContReport.DoubleClick += new System.EventHandler(this.dgContReport_DoubleClick);
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
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(1060, 377);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 40;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // dtgRedemptions
            // 
            this.dtgRedemptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgRedemptions.Location = new System.Drawing.Point(12, 395);
            this.dtgRedemptions.LookAndFeel.SkinName = "Blue";
            this.dtgRedemptions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgRedemptions.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRedemptions.MainView = this.dgRedemptions;
            this.dtgRedemptions.Name = "dtgRedemptions";
            this.dtgRedemptions.Size = new System.Drawing.Size(1079, 317);
            this.dtgRedemptions.TabIndex = 41;
            this.dtgRedemptions.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgRedemptions});
            // 
            // dgRedemptions
            // 
            this.dgRedemptions.GridControl = this.dtgRedemptions;
            this.dgRedemptions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgRedemptions.Name = "dgRedemptions";
            this.dgRedemptions.OptionsBehavior.Editable = false;
            this.dgRedemptions.OptionsSelection.MultiSelect = true;
            this.dgRedemptions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgRedemptions.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.Default_CustomDrawGroupRow);
            this.dgRedemptions.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            // 
            // lblCopy2
            // 
            this.lblCopy2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy2.AutoSize = true;
            this.lblCopy2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy2.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy2.Location = new System.Drawing.Point(1060, 718);
            this.lblCopy2.Name = "lblCopy2";
            this.lblCopy2.Size = new System.Drawing.Size(31, 13);
            this.lblCopy2.TabIndex = 42;
            this.lblCopy2.Text = "Copy";
            this.lblCopy2.Click += new System.EventHandler(this.lblCopy2_Click);
            // 
            // cmdExpand2
            // 
            this.cmdExpand2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand2.Location = new System.Drawing.Point(284, 404);
            this.cmdExpand2.Name = "cmdExpand2";
            this.cmdExpand2.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand2.TabIndex = 43;
            this.cmdExpand2.Text = "Expand";
            this.cmdExpand2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand2.UseVisualStyleBackColor = false;
            this.cmdExpand2.Click += new System.EventHandler(this.cmdExpand2_Click);
            // 
            // cmdCollapse2
            // 
            this.cmdCollapse2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse2.Location = new System.Drawing.Point(349, 404);
            this.cmdCollapse2.Name = "cmdCollapse2";
            this.cmdCollapse2.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse2.TabIndex = 44;
            this.cmdCollapse2.Text = "Collapse";
            this.cmdCollapse2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse2.UseVisualStyleBackColor = false;
            this.cmdCollapse2.Click += new System.EventHandler(this.cmdCollapse2_Click);
            // 
            // frmContacts_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1103, 761);
            this.Controls.Add(this.cmdCollapse2);
            this.Controls.Add(this.cmdExpand2);
            this.Controls.Add(this.lblCopy2);
            this.Controls.Add(this.dtgRedemptions);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.dtgContReport);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmContacts_Report";
            this.Text = "Monthly Report";
            this.Load += new System.EventHandler(this.frmContacts_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgContReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgContReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRedemptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRedemptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private MyXtraGrid.MyGridControl dtgContReport;
        private MyXtraGrid.MyGridView dgContReport;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.Label lblCopy;
        private MyXtraGrid.MyGridControl dtgRedemptions;
        private MyXtraGrid.MyGridView dgRedemptions;
        private System.Windows.Forms.Label lblCopy2;
        private System.Windows.Forms.Button cmdExpand2;
        private System.Windows.Forms.Button cmdCollapse2;
    }
}