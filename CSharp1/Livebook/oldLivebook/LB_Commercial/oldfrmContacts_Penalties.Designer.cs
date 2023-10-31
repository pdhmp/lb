namespace SGN
{
    partial class frmContacts_Penalties
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.dtgRedemptions = new MyXtraGrid.MyGridControl();
            this.dgRedemptions = new MyXtraGrid.MyGridView();
            this.lblCopy2 = new System.Windows.Forms.Label();
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
            // dtgRedemptions
            // 
            this.dtgRedemptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgRedemptions.Location = new System.Drawing.Point(12, 49);
            this.dtgRedemptions.LookAndFeel.SkinName = "Blue";
            this.dtgRedemptions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgRedemptions.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRedemptions.MainView = this.dgRedemptions;
            this.dtgRedemptions.Name = "dtgRedemptions";
            this.dtgRedemptions.Size = new System.Drawing.Size(1079, 663);
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
            this.dgRedemptions.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.Default_CustomDrawGroupRow);
            this.dgRedemptions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
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
            this.lblCopy2.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // frmContacts_Penalties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1103, 761);
            this.Controls.Add(this.lblCopy2);
            this.Controls.Add(this.dtgRedemptions);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.cmdClose);
            this.Name = "frmContacts_Penalties";
            this.Text = "Penalty Report";
            this.Load += new System.EventHandler(this.frmContacts_Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRedemptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRedemptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.Button cmdExpand;
        private MyXtraGrid.MyGridControl dtgRedemptions;
        private MyXtraGrid.MyGridView dgRedemptions;
        private System.Windows.Forms.Label lblCopy2;
    }
}