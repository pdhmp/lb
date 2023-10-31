namespace SGN
{
    partial class frmSpam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSpam));
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtgSpam = new DevExpress.XtraGrid.GridControl();
            this.dgSpam = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdrefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSpam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpam)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label2.Location = new System.Drawing.Point(111, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 11);
            this.label2.TabIndex = 47;
            this.label2.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(113, 21);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 46;
            // 
            // dtgSpam
            // 
            this.dtgSpam.Location = new System.Drawing.Point(12, 52);
            this.dtgSpam.MainView = this.dgSpam;
            this.dtgSpam.Name = "dtgSpam";
            this.dtgSpam.Size = new System.Drawing.Size(909, 507);
            this.dtgSpam.TabIndex = 45;
            this.dtgSpam.TabStop = false;
            this.dtgSpam.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgSpam});
            // 
            // dgSpam
            // 
            this.dgSpam.GridControl = this.dtgSpam;
            this.dgSpam.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgSpam.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgSpam.Name = "dgSpam";
            this.dgSpam.OptionsBehavior.Editable = false;
            this.dgSpam.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgSpam.OptionsSelection.MultiSelect = true;
            this.dgSpam.OptionsView.ColumnAutoWidth = false;
            this.dgSpam.RowHeight = 15;
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(12, 21);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 11);
            this.label1.TabIndex = 43;
            this.label1.Text = "Initial Date";
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(214, 11);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 32);
            this.cmdrefresh.TabIndex = 42;
            this.cmdrefresh.Text = "Generate Report";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // frmSpam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(933, 571);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtgSpam);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdrefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSpam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Spam Blocked";
            this.Load += new System.EventHandler(this.frmSpam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgSpam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private DevExpress.XtraGrid.GridControl dtgSpam;
        private DevExpress.XtraGrid.Views.Grid.GridView dgSpam;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdrefresh;
    }
}