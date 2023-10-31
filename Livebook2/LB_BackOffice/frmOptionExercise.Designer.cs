namespace LiveBook
{
    partial class frmOptionExercise
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptionExercise));
            this.cmdClose = new System.Windows.Forms.Button();
            this.dtgOptionExercise = new MyXtraGrid.MyGridControl();
            this.dgOptionExercise = new MyXtraGrid.MyGridView();
            this.lblCopy = new System.Windows.Forms.Label();
            this.cmbFund = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbExpiration = new System.Windows.Forms.ComboBox();
            this.cmdExpired = new System.Windows.Forms.Button();
            this.cmdFullExercise = new System.Windows.Forms.Button();
            this.cmdPartial = new System.Windows.Forms.Button();
            this.cmdNet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOptionExercise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOptionExercise)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(949, 460);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 29);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dtgOptionExercise
            // 
            this.dtgOptionExercise.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOptionExercise.Location = new System.Drawing.Point(7, 39);
            this.dtgOptionExercise.LookAndFeel.SkinName = "Blue";
            this.dtgOptionExercise.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgOptionExercise.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOptionExercise.MainView = this.dgOptionExercise;
            this.dtgOptionExercise.Name = "dtgOptionExercise";
            this.dtgOptionExercise.Size = new System.Drawing.Size(1040, 396);
            this.dtgOptionExercise.TabIndex = 0;
            this.dtgOptionExercise.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOptionExercise});
            // 
            // dgOptionExercise
            // 
            this.dgOptionExercise.GridControl = this.dtgOptionExercise;
            this.dgOptionExercise.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgOptionExercise.Name = "dgOptionExercise";
            this.dgOptionExercise.OptionsBehavior.Editable = false;
            this.dgOptionExercise.OptionsSelection.MultiSelect = true;
            this.dgOptionExercise.OptionsView.ColumnAutoWidth = false;
            this.dgOptionExercise.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgOptionExercise.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgOptionExercise_CustomDrawGroupRow);
            this.dgOptionExercise.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgOptionExercise_RowStyle);
            this.dgOptionExercise.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgOptionExercise.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.dgOptionExercise_SelectionChanged);
            this.dgOptionExercise.DoubleClick += new System.EventHandler(this.dgOptionExercise_DoubleClick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(1018, 438);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 27;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // cmbFund
            // 
            this.cmbFund.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFund.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.Location = new System.Drawing.Point(63, 12);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(262, 21);
            this.cmbFund.TabIndex = 67;
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Portfolio";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(493, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(74, 21);
            this.cmdRefresh.TabIndex = 69;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(333, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Expiration";
            // 
            // cmbExpiration
            // 
            this.cmbExpiration.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbExpiration.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbExpiration.FormattingEnabled = true;
            this.cmbExpiration.Location = new System.Drawing.Point(392, 12);
            this.cmbExpiration.Name = "cmbExpiration";
            this.cmbExpiration.Size = new System.Drawing.Size(95, 21);
            this.cmbExpiration.TabIndex = 70;
            this.cmbExpiration.SelectedIndexChanged += new System.EventHandler(this.cmbExpiration_SelectedIndexChanged);
            // 
            // cmdExpired
            // 
            this.cmdExpired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExpired.Location = new System.Drawing.Point(948, 10);
            this.cmdExpired.Name = "cmdExpired";
            this.cmdExpired.Size = new System.Drawing.Size(99, 23);
            this.cmdExpired.TabIndex = 72;
            this.cmdExpired.Text = "Confirm Expired";
            this.cmdExpired.UseVisualStyleBackColor = true;
            this.cmdExpired.Click += new System.EventHandler(this.cmdExpired_Click);
            // 
            // cmdFullExercise
            // 
            this.cmdFullExercise.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdFullExercise.Location = new System.Drawing.Point(710, 12);
            this.cmdFullExercise.Name = "cmdFullExercise";
            this.cmdFullExercise.Size = new System.Drawing.Size(87, 23);
            this.cmdFullExercise.TabIndex = 73;
            this.cmdFullExercise.Text = "Full Exercise";
            this.cmdFullExercise.UseVisualStyleBackColor = true;
            this.cmdFullExercise.Click += new System.EventHandler(this.cmdFullExercise_Click);
            // 
            // cmdPartial
            // 
            this.cmdPartial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPartial.Location = new System.Drawing.Point(803, 11);
            this.cmdPartial.Name = "cmdPartial";
            this.cmdPartial.Size = new System.Drawing.Size(87, 23);
            this.cmdPartial.TabIndex = 74;
            this.cmdPartial.Text = "Partial Exercise";
            this.cmdPartial.UseVisualStyleBackColor = true;
            this.cmdPartial.Click += new System.EventHandler(this.cmdPartial_Click);
            // 
            // cmdNet
            // 
            this.cmdNet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdNet.Location = new System.Drawing.Point(600, 12);
            this.cmdNet.Name = "cmdNet";
            this.cmdNet.Size = new System.Drawing.Size(87, 23);
            this.cmdNet.TabIndex = 75;
            this.cmdNet.Text = "Net Exercise";
            this.cmdNet.UseVisualStyleBackColor = true;
            this.cmdNet.Click += new System.EventHandler(this.cmdNet_Click);
            // 
            // frmOptionExercise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1059, 495);
            this.Controls.Add(this.cmdNet);
            this.Controls.Add(this.cmdPartial);
            this.Controls.Add(this.cmdFullExercise);
            this.Controls.Add(this.cmdExpired);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbExpiration);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.dtgOptionExercise);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOptionExercise";
            this.Text = "Option Exercise";
            this.Load += new System.EventHandler(this.frmOptionExercise_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgOptionExercise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOptionExercise)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private MyXtraGrid.MyGridControl dtgOptionExercise;
        private MyXtraGrid.MyGridView dgOptionExercise;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.ComboBox cmbFund;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbExpiration;
        private System.Windows.Forms.Button cmdExpired;
        private System.Windows.Forms.Button cmdFullExercise;
        private System.Windows.Forms.Button cmdPartial;
        private System.Windows.Forms.Button cmdNet;
    }
}