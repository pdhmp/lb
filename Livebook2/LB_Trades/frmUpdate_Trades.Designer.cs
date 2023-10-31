namespace LiveBook
{
    partial class frmUpdate_Trades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdate_Trades));
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.dtg2 = new DevExpress.XtraGrid.GridControl();
            this.dgUpTrades = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dtpnewDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUpTrades)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(186, 24);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(184, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 11);
            this.label1.TabIndex = 34;
            this.label1.Text = "Date";
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(302, 19);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 28);
            this.cmdrefresh.TabIndex = 33;
            this.cmdrefresh.Text = "Load Trades";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 11);
            this.label5.TabIndex = 32;
            this.label5.Text = "Portfolio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(6, 24);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 31;
            this.cmbChoosePortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedIndexChanged);
            // 
            // dtg2
            // 
            this.dtg2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg2.Location = new System.Drawing.Point(7, 68);
            this.dtg2.MainView = this.dgUpTrades;
            this.dtg2.Name = "dtg2";
            this.dtg2.Size = new System.Drawing.Size(874, 446);
            this.dtg2.TabIndex = 40;
            this.dtg2.TabStop = false;
            this.dtg2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgUpTrades});
            // 
            // dgUpTrades
            // 
            this.dgUpTrades.GridControl = this.dtg2;
            this.dgUpTrades.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgUpTrades.Name = "dgUpTrades";
            this.dgUpTrades.OptionsBehavior.Editable = false;
            this.dgUpTrades.OptionsView.ColumnAutoWidth = false;
            this.dgUpTrades.RowHeight = 15;
            this.dgUpTrades.DoubleClick += new System.EventHandler(this.dgTrades_DoubleClick);
            // 
            // dtpnewDate
            // 
            this.dtpnewDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpnewDate.Location = new System.Drawing.Point(155, 23);
            this.dtpnewDate.Name = "dtpnewDate";
            this.dtpnewDate.Size = new System.Drawing.Size(95, 20);
            this.dtpnewDate.TabIndex = 41;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpnewDate);
            this.groupBox1.Location = new System.Drawing.Point(617, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 54);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 11);
            this.label2.TabIndex = 36;
            this.label2.Text = "On update change trades to:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbChoosePortfolio);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmdrefresh);
            this.groupBox2.Controls.Add(this.dtpIniDate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(7, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(435, 54);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // frmUpdate_Trades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(885, 520);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtg2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUpdate_Trades";
            this.Text = "Update Trades";
            this.Load += new System.EventHandler(this.frmEdit_Trades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgUpTrades)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private DevExpress.XtraGrid.GridControl dtg2;
        private DevExpress.XtraGrid.Views.Grid.GridView dgUpTrades;
        private System.Windows.Forms.DateTimePicker dtpnewDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}