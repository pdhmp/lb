namespace LiveBook
{
    partial class frmBrokerage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrokerage));
            this.cmdClose = new System.Windows.Forms.Button();
            this.dtgBrokerage = new MyXtraGrid.MyGridControl();
            this.dgBrokerage = new MyXtraGrid.MyGridView();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radMonthly = new System.Windows.Forms.RadioButton();
            this.radDaily = new System.Windows.Forms.RadioButton();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.radAnnual = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dtgBrokerage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrokerage)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(680, 425);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dtgBrokerage
            // 
            this.dtgBrokerage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgBrokerage.Location = new System.Drawing.Point(12, 52);
            this.dtgBrokerage.LookAndFeel.SkinName = "Blue";
            this.dtgBrokerage.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgBrokerage.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgBrokerage.MainView = this.dgBrokerage;
            this.dtgBrokerage.Name = "dtgBrokerage";
            this.dtgBrokerage.Size = new System.Drawing.Size(771, 364);
            this.dtgBrokerage.TabIndex = 0;
            this.dtgBrokerage.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgBrokerage});
            // 
            // dgBrokerage
            // 
            this.dgBrokerage.GridControl = this.dtgBrokerage;
            this.dgBrokerage.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgBrokerage.Name = "dgBrokerage";
            this.dgBrokerage.OptionsBehavior.Editable = false;
            this.dgBrokerage.OptionsView.ColumnAutoWidth = false;
            this.dgBrokerage.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgBrokerage.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dtgBrokerage_CustomDrawGroupRow);
            this.dgBrokerage.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgBrokerage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgBrokerage_MouseUp);
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(11, 26);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(125, 26);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Initial Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label3.Location = new System.Drawing.Point(122, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "End Date";
            // 
            // cmdExcel
            // 
            this.cmdExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExcel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExcel.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.cmdExcel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdExcel.FlatAppearance.BorderSize = 0;
            this.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExcel.Location = new System.Drawing.Point(707, 23);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(76, 23);
            this.cmdExcel.TabIndex = 65;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = false;
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.radAnnual);
            this.groupBox3.Controls.Add(this.radMonthly);
            this.groupBox3.Controls.Add(this.radDaily);
            this.groupBox3.Location = new System.Drawing.Point(226, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(197, 30);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            // 
            // radMonthly
            // 
            this.radMonthly.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radMonthly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radMonthly.AutoSize = true;
            this.radMonthly.BackColor = System.Drawing.Color.White;
            this.radMonthly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radMonthly.Location = new System.Drawing.Point(74, 10);
            this.radMonthly.Name = "radMonthly";
            this.radMonthly.Size = new System.Drawing.Size(62, 17);
            this.radMonthly.TabIndex = 37;
            this.radMonthly.TabStop = true;
            this.radMonthly.Tag = "type";
            this.radMonthly.Text = "Monthly";
            this.radMonthly.UseVisualStyleBackColor = false;
            this.radMonthly.CheckedChanged += new System.EventHandler(this.radMonthly_CheckedChanged);
            // 
            // radDaily
            // 
            this.radDaily.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radDaily.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radDaily.AutoSize = true;
            this.radDaily.BackColor = System.Drawing.Color.White;
            this.radDaily.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radDaily.Location = new System.Drawing.Point(142, 10);
            this.radDaily.Name = "radDaily";
            this.radDaily.Size = new System.Drawing.Size(48, 17);
            this.radDaily.TabIndex = 38;
            this.radDaily.TabStop = true;
            this.radDaily.Tag = "type";
            this.radDaily.Text = "Daily";
            this.radDaily.UseVisualStyleBackColor = false;
            this.radDaily.CheckedChanged += new System.EventHandler(this.radDaily_CheckedChanged);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(463, 23);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 3;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // radAnnual
            // 
            this.radAnnual.AccessibleName = global::LiveBook.Properties.Resources.resource;
            this.radAnnual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radAnnual.AutoSize = true;
            this.radAnnual.BackColor = System.Drawing.Color.White;
            this.radAnnual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.radAnnual.Location = new System.Drawing.Point(10, 10);
            this.radAnnual.Name = "radAnnual";
            this.radAnnual.Size = new System.Drawing.Size(58, 17);
            this.radAnnual.TabIndex = 39;
            this.radAnnual.TabStop = true;
            this.radAnnual.Tag = "type";
            this.radAnnual.Text = "Annual";
            this.radAnnual.UseVisualStyleBackColor = false;
            this.radAnnual.CheckedChanged += new System.EventHandler(this.radAnnual_CheckedChanged);
            // 
            // frmBrokerage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(792, 472);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.dtgBrokerage);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBrokerage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Brokerage";
            this.Load += new System.EventHandler(this.frmFowards_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgBrokerage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBrokerage)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private MyXtraGrid.MyGridControl dtgBrokerage;
        private MyXtraGrid.MyGridView dgBrokerage;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdExcel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radMonthly;
        private System.Windows.Forms.RadioButton radDaily;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.RadioButton radAnnual;
    }
}