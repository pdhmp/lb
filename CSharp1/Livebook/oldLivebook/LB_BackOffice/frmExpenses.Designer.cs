namespace LiveBook
{
    partial class frmExpenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpenses));
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radJCP_Section = new System.Windows.Forms.RadioButton();
            this.radCash_Section = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.radRevenues = new System.Windows.Forms.RadioButton();
            this.radExpenses = new System.Windows.Forms.RadioButton();
            this.cmbIdSecurity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgExpenses = new MyXtraGrid.MyGridControl();
            this.dgExpenses = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.cmbFund = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgExpenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpenses)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(748, 551);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(100, 35);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(734, 16);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtValue);
            this.groupBox2.Controls.Add(this.radRevenues);
            this.groupBox2.Controls.Add(this.radExpenses);
            this.groupBox2.Controls.Add(this.cmbIdSecurity);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpTradeDate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmdInsert);
            this.groupBox2.Location = new System.Drawing.Point(12, 462);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(845, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Insert ";
            // 
            // radJCP_Section
            // 
            this.radJCP_Section.AutoSize = true;
            this.radJCP_Section.Location = new System.Drawing.Point(6, 15);
            this.radJCP_Section.Name = "radJCP_Section";
            this.radJCP_Section.Size = new System.Drawing.Size(44, 17);
            this.radJCP_Section.TabIndex = 81;
            this.radJCP_Section.TabStop = true;
            this.radJCP_Section.Text = "JCP";
            this.radJCP_Section.UseVisualStyleBackColor = true;
            // 
            // radCash_Section
            // 
            this.radCash_Section.AutoSize = true;
            this.radCash_Section.Location = new System.Drawing.Point(67, 16);
            this.radCash_Section.Name = "radCash_Section";
            this.radCash_Section.Size = new System.Drawing.Size(49, 17);
            this.radCash_Section.TabIndex = 80;
            this.radCash_Section.TabStop = true;
            this.radCash_Section.Text = "Cash";
            this.radCash_Section.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(474, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Amount";
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(477, 31);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 20);
            this.txtValue.TabIndex = 78;
            this.txtValue.Leave += new System.EventHandler(this.txtValue_Leave);
            // 
            // radRevenues
            // 
            this.radRevenues.AutoSize = true;
            this.radRevenues.Location = new System.Drawing.Point(189, 33);
            this.radRevenues.Name = "radRevenues";
            this.radRevenues.Size = new System.Drawing.Size(74, 17);
            this.radRevenues.TabIndex = 77;
            this.radRevenues.TabStop = true;
            this.radRevenues.Text = "Revenues";
            this.radRevenues.UseVisualStyleBackColor = true;
            this.radRevenues.CheckedChanged += new System.EventHandler(this.radRevenues_CheckedChanged);
            // 
            // radExpenses
            // 
            this.radExpenses.AutoSize = true;
            this.radExpenses.Location = new System.Drawing.Point(112, 33);
            this.radExpenses.Name = "radExpenses";
            this.radExpenses.Size = new System.Drawing.Size(71, 17);
            this.radExpenses.TabIndex = 76;
            this.radExpenses.TabStop = true;
            this.radExpenses.Text = "Expenses";
            this.radExpenses.UseVisualStyleBackColor = true;
            this.radExpenses.CheckedChanged += new System.EventHandler(this.radExpenses_CheckedChanged);
            // 
            // cmbIdSecurity
            // 
            this.cmbIdSecurity.FormattingEnabled = true;
            this.cmbIdSecurity.Location = new System.Drawing.Point(269, 31);
            this.cmbIdSecurity.Name = "cmbIdSecurity";
            this.cmbIdSecurity.Size = new System.Drawing.Size(202, 21);
            this.cmbIdSecurity.TabIndex = 75;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Security";
            // 
            // dtpTradeDate
            // 
            this.dtpTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTradeDate.Location = new System.Drawing.Point(6, 32);
            this.dtpTradeDate.Name = "dtpTradeDate";
            this.dtpTradeDate.Size = new System.Drawing.Size(100, 20);
            this.dtpTradeDate.TabIndex = 71;
            this.dtpTradeDate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Type";
            // 
            // dtgExpenses
            // 
            this.dtgExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgExpenses.Location = new System.Drawing.Point(12, 52);
            this.dtgExpenses.LookAndFeel.SkinName = "Blue";
            this.dtgExpenses.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgExpenses.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgExpenses.MainView = this.dgExpenses;
            this.dtgExpenses.Name = "dtgExpenses";
            this.dtgExpenses.Size = new System.Drawing.Size(845, 410);
            this.dtgExpenses.TabIndex = 0;
            this.dtgExpenses.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgExpenses});
            // 
            // dgExpenses
            // 
            this.dgExpenses.GridControl = this.dtgExpenses;
            this.dgExpenses.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgExpenses.Name = "dgExpenses";
            this.dgExpenses.OptionsBehavior.Editable = false;
            this.dgExpenses.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgExpenses.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgExpenses_CustomDrawGroupRow);
            this.dgExpenses.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgExpenses.ShowCustomizationForm += new System.EventHandler(this.dgExpenses_ShowCustomizationForm);
            this.dgExpenses.HideCustomizationForm += new System.EventHandler(this.dgExpenses_HideCustomizationForm);
            this.dgExpenses.EndGrouping += new System.EventHandler(this.dgExpenses_EndGrouping);
            this.dgExpenses.DoubleClick += new System.EventHandler(this.dgExpenses_DoubleClick);
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
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(245, 24);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 63;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(144, 24);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 62;
            this.dtpIniDate.ValueChanged += new System.EventHandler(this.dtpIniDate_ValueChanged);
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(346, 14);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 32);
            this.cmdrefresh.TabIndex = 61;
            this.cmdrefresh.Text = "Refresh";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // cmbFund
            // 
            this.cmbFund.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.Location = new System.Drawing.Point(12, 25);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(126, 19);
            this.cmbFund.TabIndex = 64;
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radCash_Section);
            this.groupBox1.Controls.Add(this.radJCP_Section);
            this.groupBox1.Location = new System.Drawing.Point(583, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 41);
            this.groupBox1.TabIndex = 83;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Section";
            // 
            // frmExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(865, 598);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgExpenses);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExpenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Expenses & Revenues";
            this.Load += new System.EventHandler(this.frmExpenses_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgExpenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpenses)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private MyXtraGrid.MyGridControl dtgExpenses;
        private MyXtraGrid.MyGridView dgExpenses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.ComboBox cmbFund;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.RadioButton radRevenues;
        private System.Windows.Forms.RadioButton radExpenses;
        private System.Windows.Forms.ComboBox cmbIdSecurity;
        private System.Windows.Forms.RadioButton radJCP_Section;
        private System.Windows.Forms.RadioButton radCash_Section;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}