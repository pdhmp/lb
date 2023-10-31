namespace SGN
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
            this.txtRevenues = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTradeDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCash = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgExpenses = new MyXtraGrid.MyGridControl();
            this.dgExpenses = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new SGN.NestPortCombo();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgExpenses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpenses)).BeginInit();
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
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(440, 19);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(102, 34);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRevenues);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.dtpTradeDate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtCash);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmdInsert);
            this.groupBox2.Location = new System.Drawing.Point(12, 416);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(558, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Insert ";
            // 
            // txtRevenues
            // 
            this.txtRevenues.Location = new System.Drawing.Point(265, 32);
            this.txtRevenues.Name = "txtRevenues";
            this.txtRevenues.Size = new System.Drawing.Size(100, 20);
            this.txtRevenues.TabIndex = 73;
            this.txtRevenues.Leave += new System.EventHandler(this.txtRevenues_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(262, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Revenues";
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
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Expense Date";
            // 
            // txtCash
            // 
            this.txtCash.Location = new System.Drawing.Point(136, 32);
            this.txtCash.Name = "txtCash";
            this.txtCash.Size = new System.Drawing.Size(100, 20);
            this.txtCash.TabIndex = 2;
            this.txtCash.Leave += new System.EventHandler(this.txtCash_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Expenses";
            // 
            // dtgExpenses
            // 
            this.dtgExpenses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgExpenses.Location = new System.Drawing.Point(12, 52);
            this.dtgExpenses.LookAndFeel.SkinName = "Blue";
            this.dtgExpenses.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgExpenses.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgExpenses.MainView = this.dgExpenses;
            this.dtgExpenses.Name = "dtgExpenses";
            this.dtgExpenses.Size = new System.Drawing.Size(558, 358);
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
            this.dgExpenses.OptionsView.ShowGroupPanel = false;
            this.dgExpenses.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgExpenses.DoubleClick += new System.EventHandler(this.dgExpenses_DoubleClick);
            this.dgExpenses.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
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
            this.cmbFund.SelectedIndexChanged += new System.EventHandler(this.cmbFund_SelectedIndexChanged);
            // 
            // frmExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(578, 532);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtgExpenses);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmdClose);
            this.Name = "frmExpenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Expenses & Revenues";
            this.Load += new System.EventHandler(this.frmDividends_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgExpenses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpenses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox2;
        private MyXtraGrid.MyGridControl dtgExpenses;
        private MyXtraGrid.MyGridView dgExpenses;
        private System.Windows.Forms.TextBox txtCash;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTradeDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private NestPortCombo cmbFund;
        private System.Windows.Forms.TextBox txtRevenues;
        private System.Windows.Forms.Label label3;
    }
}