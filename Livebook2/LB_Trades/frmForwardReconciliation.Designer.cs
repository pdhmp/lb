namespace LiveBook
{
    partial class frmForwardReconciliation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmForwardReconciliation));
            this.GridFowardRecon = new MyXtraGrid.MyGridControl();
            this.DataGridFowardRecon = new MyXtraGrid.MyGridView();
            this.cmbDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GridFowardRecon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFowardRecon)).BeginInit();
            this.SuspendLayout();
            // 
            // GridFowardRecon
            // 
            this.GridFowardRecon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GridFowardRecon.Location = new System.Drawing.Point(18, 37);
            this.GridFowardRecon.LookAndFeel.SkinName = "Blue";
            this.GridFowardRecon.LookAndFeel.UseDefaultLookAndFeel = false;
            this.GridFowardRecon.MainView = this.DataGridFowardRecon;
            this.GridFowardRecon.Name = "GridFowardRecon";
            this.GridFowardRecon.Size = new System.Drawing.Size(1405, 553);
            this.GridFowardRecon.TabIndex = 2;
            this.GridFowardRecon.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DataGridFowardRecon});
            // 
            // DataGridFowardRecon
            // 
            this.DataGridFowardRecon.GridControl = this.GridFowardRecon;
            this.DataGridFowardRecon.Name = "DataGridFowardRecon";
            this.DataGridFowardRecon.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dtgFoward_DragObjectDrop);
            this.DataGridFowardRecon.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.DataGridFowardRecon_CustomDrawCell);
            this.DataGridFowardRecon.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dtgFoward_ColumnWidthChanged);
            this.DataGridFowardRecon.DoubleClick += new System.EventHandler(this.dtgFoward_DoubleClick);
            // 
            // cmbDate
            // 
            this.cmbDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.cmbDate.Location = new System.Drawing.Point(48, 10);
            this.cmbDate.Name = "cmbDate";
            this.cmbDate.Size = new System.Drawing.Size(90, 20);
            this.cmbDate.TabIndex = 3;
            this.cmbDate.Value = new System.DateTime(2013, 7, 31, 17, 25, 30, 0);
            this.cmbDate.ValueChanged += new System.EventHandler(this.cmbDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(144, 8);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // frmForwardReconciliation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1435, 602);
            this.Controls.Add(this.GridFowardRecon);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmForwardReconciliation";
            this.Text = "Forward Reconciliation";
            ((System.ComponentModel.ISupportInitialize)(this.GridFowardRecon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridFowardRecon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl GridFowardRecon;
        private MyXtraGrid.MyGridView DataGridFowardRecon;
        private System.Windows.Forms.DateTimePicker cmbDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoad;
    }
}