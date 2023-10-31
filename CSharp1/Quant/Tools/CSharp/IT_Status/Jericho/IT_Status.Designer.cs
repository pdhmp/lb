namespace IT_Status
{
    partial class IT_Status
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
            this.components = new System.ComponentModel.Container();
            this.dgcOrders = new NCustomControls.MyGridControl();
            this.dgvOrders = new NCustomControls.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgcSecurities = new NCustomControls.MyGridControl();
            this.dgvSecurities = new NCustomControls.MyGridView();
            this.dm2 = new System.Windows.Forms.DateTimePicker();
            this.dm1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgSummary = new System.Windows.Forms.DataGridView();
            this.QSEGS = new System.Windows.Forms.Label();
            this.Futuros = new System.Windows.Forms.Label();
            this.lbQSEGS = new System.Windows.Forms.Label();
            this.lbFuturos = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurities)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // dgcOrders
            // 
            this.dgcOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgcOrders.Location = new System.Drawing.Point(12, 475);
            this.dgcOrders.LookAndFeel.SkinName = "Blue";
            this.dgcOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcOrders.MainView = this.dgvOrders;
            this.dgcOrders.Name = "dgcOrders";
            this.dgcOrders.Size = new System.Drawing.Size(577, 295);
            this.dgcOrders.TabIndex = 0;
            this.dgcOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvOrders});
            this.dgcOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcOrders.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvOrders
            // 
            this.dgvOrders.GridControl = this.dgcOrders;
            this.dgvOrders.Name = "dgvOrders";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgcSecurities
            // 
            this.dgcSecurities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgcSecurities.Location = new System.Drawing.Point(12, 150);
            this.dgcSecurities.LookAndFeel.SkinName = "Blue";
            this.dgcSecurities.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcSecurities.MainView = this.dgvSecurities;
            this.dgcSecurities.Name = "dgcSecurities";
            this.dgcSecurities.Size = new System.Drawing.Size(577, 319);
            this.dgcSecurities.TabIndex = 3;
            this.dgcSecurities.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSecurities});
            this.dgcSecurities.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcSecurities.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvSecurities
            // 
            this.dgvSecurities.GridControl = this.dgcSecurities;
            this.dgvSecurities.Name = "dgvSecurities";
            // 
            // dm2
            // 
            this.dm2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dm2.Location = new System.Drawing.Point(6, 19);
            this.dm2.Name = "dm2";
            this.dm2.Size = new System.Drawing.Size(85, 20);
            this.dm2.TabIndex = 4;
            this.dm2.Value = new System.DateTime(2012, 8, 16, 0, 0, 0, 0);
            // 
            // dm1
            // 
            this.dm1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dm1.Location = new System.Drawing.Point(6, 19);
            this.dm1.Name = "dm1";
            this.dm1.Size = new System.Drawing.Size(88, 20);
            this.dm1.TabIndex = 5;
            this.dm1.Value = new System.DateTime(2012, 8, 16, 0, 0, 0, 0);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dm2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 53);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prev Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dm1);
            this.groupBox2.Location = new System.Drawing.Point(121, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(104, 53);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Yesterday";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Check";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgSummary);
            this.groupBox3.Location = new System.Drawing.Point(328, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(261, 141);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bovespa Summary";
            // 
            // dgSummary
            // 
            this.dgSummary.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSummary.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgSummary.Location = new System.Drawing.Point(6, 19);
            this.dgSummary.Name = "dgSummary";
            this.dgSummary.Size = new System.Drawing.Size(249, 116);
            this.dgSummary.TabIndex = 0;
            // 
            // QSEGS
            // 
            this.QSEGS.AutoSize = true;
            this.QSEGS.Location = new System.Drawing.Point(17, 87);
            this.QSEGS.Name = "QSEGS";
            this.QSEGS.Size = new System.Drawing.Size(44, 13);
            this.QSEGS.TabIndex = 10;
            this.QSEGS.Text = "QSEGS";
            // 
            // Futuros
            // 
            this.Futuros.AutoSize = true;
            this.Futuros.Location = new System.Drawing.Point(18, 119);
            this.Futuros.Name = "Futuros";
            this.Futuros.Size = new System.Drawing.Size(42, 13);
            this.Futuros.TabIndex = 11;
            this.Futuros.Text = "Futuros";
            // 
            // lbQSEGS
            // 
            this.lbQSEGS.AutoSize = true;
            this.lbQSEGS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQSEGS.Location = new System.Drawing.Point(103, 85);
            this.lbQSEGS.Name = "lbQSEGS";
            this.lbQSEGS.Size = new System.Drawing.Size(0, 15);
            this.lbQSEGS.TabIndex = 12;
            // 
            // lbFuturos
            // 
            this.lbFuturos.AutoSize = true;
            this.lbFuturos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFuturos.Location = new System.Drawing.Point(103, 117);
            this.lbFuturos.Name = "lbFuturos";
            this.lbFuturos.Size = new System.Drawing.Size(0, 15);
            this.lbFuturos.TabIndex = 13;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 776);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(117, 12);
            this.progressBar.TabIndex = 14;
            // 
            // IT_Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 800);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lbFuturos);
            this.Controls.Add(this.lbQSEGS);
            this.Controls.Add(this.Futuros);
            this.Controls.Add(this.QSEGS);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgcSecurities);
            this.Controls.Add(this.dgcOrders);
            this.Name = "IT_Status";
            this.Text = "IT Status";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmJericho_FormClosing);
            this.Load += new System.EventHandler(this.IT_Status_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurities)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSummary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NCustomControls.MyGridControl dgcOrders;
        private NCustomControls.MyGridView dgvOrders;
        private System.Windows.Forms.Timer timer1;
        private NCustomControls.MyGridView dgvSecurities;
        private System.Windows.Forms.DateTimePicker dm2;
        private System.Windows.Forms.DateTimePicker dm1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private NCustomControls.MyGridControl dgcSecurities;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgSummary;
        private System.Windows.Forms.Label QSEGS;
        private System.Windows.Forms.Label Futuros;
        private System.Windows.Forms.Label lbQSEGS;
        private System.Windows.Forms.Label lbFuturos;
        private System.Windows.Forms.ProgressBar progressBar;

    }
}

