namespace LiveBook
{
    partial class frmDiferences
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDiferences));
            this.cmdClose = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtgDiferences = new MyXtraGrid.MyGridControl();
            this.dgDiferences = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new LiveBook.NestPortCombo();
            this.cmdDiferences = new System.Windows.Forms.Button();
            this.lblCopy = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferences)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiferences)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(454, 489);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(139, 26);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDate.TabIndex = 71;
            this.dtpDate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Date";
            // 
            // dtgDiferences
            // 
            this.dtgDiferences.Location = new System.Drawing.Point(12, 52);
            this.dtgDiferences.LookAndFeel.SkinName = "Blue";
            this.dtgDiferences.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgDiferences.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDiferences.MainView = this.dgDiferences;
            this.dtgDiferences.Name = "dtgDiferences";
            this.dtgDiferences.Size = new System.Drawing.Size(558, 358);
            this.dtgDiferences.TabIndex = 0;
            this.dtgDiferences.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgDiferences});
            // 
            // dgDiferences
            // 
            this.dgDiferences.GridControl = this.dtgDiferences;
            this.dgDiferences.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgDiferences.Name = "dgDiferences";
            this.dgDiferences.OptionsBehavior.Editable = false;
            this.dgDiferences.OptionsSelection.MultiSelect = true;
            this.dgDiferences.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgDiferences.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgDiferences_CustomDrawGroupRow);
            this.dgDiferences.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgDiferences.DoubleClick += new System.EventHandler(this.dgExpenses_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Fund";
            // 
            // cmbFund
            // 
            this.cmbFund.DisplayMember = "Port_Name";
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.IdPortType = 2;
            this.cmbFund.includeAllPortsOption = false;
            this.cmbFund.includeMHConsolOption = false;
            this.cmbFund.Location = new System.Drawing.Point(12, 25);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(121, 21);
            this.cmbFund.TabIndex = 60;
            this.cmbFund.ValueMember = "Id_Portfolio";
            // 
            // cmdDiferences
            // 
            this.cmdDiferences.Location = new System.Drawing.Point(256, 25);
            this.cmdDiferences.Name = "cmdDiferences";
            this.cmdDiferences.Size = new System.Drawing.Size(100, 21);
            this.cmdDiferences.TabIndex = 73;
            this.cmdDiferences.Text = "Load";
            this.cmdDiferences.UseVisualStyleBackColor = true;
            this.cmdDiferences.Click += new System.EventHandler(this.cmdDiferences_Click);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(535, 413);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 74;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // frmDiferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(578, 532);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.cmdDiferences);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.dtgDiferences);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDiferences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Diferences";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferences)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiferences)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private MyXtraGrid.MyGridControl dtgDiferences;
        private MyXtraGrid.MyGridView dgDiferences;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private NestPortCombo cmbFund;
        private System.Windows.Forms.Button cmdDiferences;
        private System.Windows.Forms.Label lblCopy;
    }
}