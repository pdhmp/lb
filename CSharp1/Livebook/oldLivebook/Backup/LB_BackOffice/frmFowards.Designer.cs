namespace SGN
{
    partial class frmFowards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFowards));
            this.cmdClose = new System.Windows.Forms.Button();
            this.dgFoward = new MyXtraGrid.MyGridControl();
            this.dtgFoward = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new SGN.NestPortCombo();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgFoward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFoward)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(488, 416);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // dgFoward
            // 
            this.dgFoward.Location = new System.Drawing.Point(12, 52);
            this.dgFoward.LookAndFeel.SkinName = "Blue";
            this.dgFoward.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dgFoward.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgFoward.MainView = this.dtgFoward;
            this.dgFoward.Name = "dgFoward";
            this.dgFoward.Size = new System.Drawing.Size(586, 358);
            this.dgFoward.TabIndex = 0;
            this.dgFoward.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgFoward});
            // 
            // dtgFoward
            // 
            this.dtgFoward.GridControl = this.dgFoward;
            this.dtgFoward.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dtgFoward.Name = "dtgFoward";
            this.dtgFoward.OptionsBehavior.Editable = false;
            this.dtgFoward.OptionsView.ShowGroupPanel = false;
            this.dtgFoward.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dtgFoward.DoubleClick += new System.EventHandler(this.dtgFoward_DoubleClick);
            this.dtgFoward.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
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
            this.cmbFund.Location = new System.Drawing.Point(12, 25);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(121, 21);
            this.cmbFund.TabIndex = 0;
            this.cmbFund.ValueMember = "Id_Portfolio";
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(160, 26);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 1;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(274, 26);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(157, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Initial Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(271, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "End Date";
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(387, 23);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 3;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // frmFowards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(607, 466);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgFoward);
            this.Controls.Add(this.cmdClose);
            this.Name = "frmFowards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fowards";
            this.Load += new System.EventHandler(this.frmFowards_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgFoward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFoward)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private MyXtraGrid.MyGridControl dgFoward;
        private MyXtraGrid.MyGridView dtgFoward;
        private System.Windows.Forms.Label label1;
        private NestPortCombo cmbFund;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdLoad;
    }
}