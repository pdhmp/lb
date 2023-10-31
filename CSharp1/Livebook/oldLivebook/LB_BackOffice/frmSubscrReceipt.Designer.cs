namespace LiveBook
{
    partial class frmSubscrReceipt
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new System.Windows.Forms.ComboBox();
            this.cmdFullExercise = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtgReceipt = new DevExpress.XtraGrid.GridControl();
            this.dgReceipt = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtgWarrant = new DevExpress.XtraGrid.GridControl();
            this.dgWarrant = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgWarrant)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWarrant)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Portfolio";
            // 
            // cmbFund
            // 
            this.cmbFund.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFund.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.Location = new System.Drawing.Point(68, 8);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(183, 21);
            this.cmbFund.TabIndex = 70;
            // 
            // cmdFullExercise
            // 
            this.cmdFullExercise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFullExercise.Location = new System.Drawing.Point(257, 8);
            this.cmdFullExercise.Name = "cmdFullExercise";
            this.cmdFullExercise.Size = new System.Drawing.Size(87, 23);
            this.cmdFullExercise.TabIndex = 76;
            this.cmdFullExercise.Text = "Load";
            this.cmdFullExercise.UseVisualStyleBackColor = true;
            this.cmdFullExercise.Click += new System.EventHandler(this.cmdFullExercise_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtgReceipt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtgWarrant);
            this.splitContainer1.Size = new System.Drawing.Size(1058, 548);
            this.splitContainer1.SplitterDistance = 273;
            this.splitContainer1.TabIndex = 78;
            // 
            // dtgReceipt
            // 
            this.dtgReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgReceipt.Location = new System.Drawing.Point(3, 0);
            this.dtgReceipt.MainView = this.dgReceipt;
            this.dtgReceipt.Name = "dtgReceipt";
            this.dtgReceipt.Size = new System.Drawing.Size(1052, 270);
            this.dtgReceipt.TabIndex = 32;
            this.dtgReceipt.TabStop = false;
            this.dtgReceipt.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgReceipt});
            // 
            // dgReceipt
            // 
            this.dgReceipt.GridControl = this.dtgReceipt;
            this.dgReceipt.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgReceipt.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgReceipt.Name = "dgReceipt";
            this.dgReceipt.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgReceipt.OptionsSelection.MultiSelect = true;
            this.dgReceipt.OptionsView.ColumnAutoWidth = false;
            this.dgReceipt.RowHeight = 15;
            // 
            // dtgWarrant
            // 
            this.dtgWarrant.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgWarrant.Location = new System.Drawing.Point(3, 3);
            this.dtgWarrant.MainView = this.dgWarrant;
            this.dtgWarrant.Name = "dtgWarrant";
            this.dtgWarrant.Size = new System.Drawing.Size(1052, 265);
            this.dtgWarrant.TabIndex = 28;
            this.dtgWarrant.TabStop = false;
            this.dtgWarrant.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgWarrant});
            // 
            // dgWarrant
            // 
            this.dgWarrant.GridControl = this.dtgWarrant;
            this.dgWarrant.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgWarrant.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgWarrant.Name = "dgWarrant";
            this.dgWarrant.OptionsBehavior.Editable = false;
            this.dgWarrant.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgWarrant.OptionsSelection.MultiSelect = true;
            this.dgWarrant.OptionsView.ColumnAutoWidth = false;
            this.dgWarrant.RowHeight = 15;
            // 
            // frmSubscrReceipt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1077, 592);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdFullExercise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFund);
            this.Name = "frmSubscrReceipt";
            this.Text = "Receipt Subscr & Warrant";
            this.Load += new System.EventHandler(this.frmReceiptSubscr_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgReceipt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgWarrant)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgWarrant)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFund;
        private System.Windows.Forms.Button cmdFullExercise;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl dtgReceipt;
        private DevExpress.XtraGrid.Views.Grid.GridView dgReceipt;
        private DevExpress.XtraGrid.GridControl dtgWarrant;
        private DevExpress.XtraGrid.Views.Grid.GridView dgWarrant;
    }
}