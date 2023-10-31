namespace LiveBook
{
    partial class frmTradeSplit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTradeSplit));
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgTradeSplit = new MyXtraGrid.MyGridView();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.cmdSplitAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.lblCopy = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLongRestante = new System.Windows.Forms.Label();
            this.lblLongTotal = new System.Windows.Forms.Label();
            this.txtLongHedge = new System.Windows.Forms.TextBox();
            this.txtLongFIA = new System.Windows.Forms.TextBox();
            this.txtLongNFUND = new System.Windows.Forms.TextBox();
            this.txtLongMH = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtShortHedge = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblShortRestante = new System.Windows.Forms.Label();
            this.lblShortTotal = new System.Windows.Forms.Label();
            this.txtShortFIA = new System.Windows.Forms.TextBox();
            this.txtShortNFUND = new System.Windows.Forms.TextBox();
            this.txtShortMH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.chkManualAjuste = new System.Windows.Forms.CheckBox();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.chkViewInitial = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradeSplit)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(-1, 87);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgTradeSplit;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(1126, 507);
            this.dtg.TabIndex = 25;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgTradeSplit});
            // 
            // dgTradeSplit
            // 
            this.dgTradeSplit.GridControl = this.dtg;
            this.dgTradeSplit.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgTradeSplit.Name = "dgTradeSplit";
            this.dgTradeSplit.OptionsBehavior.Editable = false;
            this.dgTradeSplit.OptionsSelection.MultiSelect = true;
            this.dgTradeSplit.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgTradeSplit.OptionsView.ShowIndicator = false;
            this.dgTradeSplit.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgTradeSplit_DragObjectDrop);
            this.dgTradeSplit.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgTradeSplit_CustomDrawCell);
            this.dgTradeSplit.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgTradeSplit_ColumnWidthChanged);
            this.dgTradeSplit.ShowGridMenu += new DevExpress.XtraGrid.Views.Grid.GridMenuEventHandler(this.dgTradeSplit_ShowGridMenu);
            this.dgTradeSplit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgTradeSplit_MouseDown);
            this.dgTradeSplit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgTradeSplit_MouseUp);
            this.dgTradeSplit.DoubleClick += new System.EventHandler(this.dgTradeSplit_DoubleClick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRefresh.Location = new System.Drawing.Point(1037, 58);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 26;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdSplitAll
            // 
            this.cmdSplitAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSplitAll.Location = new System.Drawing.Point(956, 58);
            this.cmdSplitAll.Name = "cmdSplitAll";
            this.cmdSplitAll.Size = new System.Drawing.Size(75, 23);
            this.cmdSplitAll.TabIndex = 27;
            this.cmdSplitAll.Text = "Split All";
            this.cmdSplitAll.UseVisualStyleBackColor = true;
            this.cmdSplitAll.Click += new System.EventHandler(this.cmdSplitAll_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(803, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Last all split: ";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.lblLastUpdate.ForeColor = System.Drawing.Color.White;
            this.lblLastUpdate.Location = new System.Drawing.Point(876, 63);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(60, 13);
            this.lblLastUpdate.TabIndex = 29;
            this.lblLastUpdate.Text = "time";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 15000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(1)))));
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(1092, 597);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 36;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(195, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Hedge";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 40;
            this.label10.Text = "NFUND";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 20);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "MH";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLongRestante);
            this.groupBox1.Controls.Add(this.lblLongTotal);
            this.groupBox1.Controls.Add(this.txtLongHedge);
            this.groupBox1.Controls.Add(this.txtLongFIA);
            this.groupBox1.Controls.Add(this.txtLongNFUND);
            this.groupBox1.Controls.Add(this.txtLongMH);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(104, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 71);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "% Long";
            // 
            // lblLongRestante
            // 
            this.lblLongRestante.AutoSize = true;
            this.lblLongRestante.Location = new System.Drawing.Point(252, 41);
            this.lblLongRestante.Name = "lblLongRestante";
            this.lblLongRestante.Size = new System.Drawing.Size(15, 13);
            this.lblLongRestante.TabIndex = 51;
            this.lblLongRestante.Text = "%";
            // 
            // lblLongTotal
            // 
            this.lblLongTotal.AutoSize = true;
            this.lblLongTotal.Location = new System.Drawing.Point(252, 24);
            this.lblLongTotal.Name = "lblLongTotal";
            this.lblLongTotal.Size = new System.Drawing.Size(15, 13);
            this.lblLongTotal.TabIndex = 50;
            this.lblLongTotal.Text = "%";
            // 
            // txtLongHedge
            // 
            this.txtLongHedge.Location = new System.Drawing.Point(195, 38);
            this.txtLongHedge.Name = "txtLongHedge";
            this.txtLongHedge.Size = new System.Drawing.Size(50, 20);
            this.txtLongHedge.TabIndex = 49;
            this.txtLongHedge.Text = "0";
            this.txtLongHedge.TextChanged += new System.EventHandler(this.txtLongHedge_TextChanged);
            // 
            // txtLongFIA
            // 
            this.txtLongFIA.Location = new System.Drawing.Point(137, 38);
            this.txtLongFIA.Name = "txtLongFIA";
            this.txtLongFIA.Size = new System.Drawing.Size(50, 20);
            this.txtLongFIA.TabIndex = 48;
            this.txtLongFIA.Text = "0";
            this.txtLongFIA.TextChanged += new System.EventHandler(this.txtLongFIA_TextChanged);
            // 
            // txtLongNFUND
            // 
            this.txtLongNFUND.Location = new System.Drawing.Point(77, 38);
            this.txtLongNFUND.Name = "txtLongNFUND";
            this.txtLongNFUND.Size = new System.Drawing.Size(50, 20);
            this.txtLongNFUND.TabIndex = 47;
            this.txtLongNFUND.Text = "0";
            this.txtLongNFUND.TextChanged += new System.EventHandler(this.txtLongNFUND_TextChanged);
            // 
            // txtLongMH
            // 
            this.txtLongMH.Location = new System.Drawing.Point(18, 38);
            this.txtLongMH.Name = "txtLongMH";
            this.txtLongMH.Size = new System.Drawing.Size(50, 20);
            this.txtLongMH.TabIndex = 46;
            this.txtLongMH.Text = "0";
            this.txtLongMH.TextChanged += new System.EventHandler(this.txtLongMH_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(137, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 13);
            this.label16.TabIndex = 42;
            this.label16.Text = "FIA";
            // 
            // txtShortHedge
            // 
            this.txtShortHedge.Location = new System.Drawing.Point(194, 38);
            this.txtShortHedge.Name = "txtShortHedge";
            this.txtShortHedge.Size = new System.Drawing.Size(50, 20);
            this.txtShortHedge.TabIndex = 49;
            this.txtShortHedge.Text = "0";
            this.txtShortHedge.TextChanged += new System.EventHandler(this.txtShortHedge_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblShortRestante);
            this.groupBox2.Controls.Add(this.lblShortTotal);
            this.groupBox2.Controls.Add(this.txtShortHedge);
            this.groupBox2.Controls.Add(this.txtShortFIA);
            this.groupBox2.Controls.Add(this.txtShortNFUND);
            this.groupBox2.Controls.Add(this.txtShortMH);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(423, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 71);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "% Short";
            // 
            // lblShortRestante
            // 
            this.lblShortRestante.AutoSize = true;
            this.lblShortRestante.Location = new System.Drawing.Point(257, 41);
            this.lblShortRestante.Name = "lblShortRestante";
            this.lblShortRestante.Size = new System.Drawing.Size(15, 13);
            this.lblShortRestante.TabIndex = 52;
            this.lblShortRestante.Text = "%";
            // 
            // lblShortTotal
            // 
            this.lblShortTotal.AutoSize = true;
            this.lblShortTotal.Location = new System.Drawing.Point(257, 20);
            this.lblShortTotal.Name = "lblShortTotal";
            this.lblShortTotal.Size = new System.Drawing.Size(15, 13);
            this.lblShortTotal.TabIndex = 51;
            this.lblShortTotal.Text = "%";
            // 
            // txtShortFIA
            // 
            this.txtShortFIA.Location = new System.Drawing.Point(136, 38);
            this.txtShortFIA.Name = "txtShortFIA";
            this.txtShortFIA.Size = new System.Drawing.Size(50, 20);
            this.txtShortFIA.TabIndex = 48;
            this.txtShortFIA.Text = "0";
            this.txtShortFIA.TextChanged += new System.EventHandler(this.txtShortFIA_TextChanged);
            // 
            // txtShortNFUND
            // 
            this.txtShortNFUND.Location = new System.Drawing.Point(76, 38);
            this.txtShortNFUND.Name = "txtShortNFUND";
            this.txtShortNFUND.Size = new System.Drawing.Size(50, 20);
            this.txtShortNFUND.TabIndex = 47;
            this.txtShortNFUND.Text = "0";
            this.txtShortNFUND.TextChanged += new System.EventHandler(this.txtShortNFUND_TextChanged);
            // 
            // txtShortMH
            // 
            this.txtShortMH.Location = new System.Drawing.Point(18, 38);
            this.txtShortMH.Name = "txtShortMH";
            this.txtShortMH.Size = new System.Drawing.Size(50, 20);
            this.txtShortMH.TabIndex = 46;
            this.txtShortMH.Text = "0";
            this.txtShortMH.TextChanged += new System.EventHandler(this.txtShortMH_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(194, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 44;
            this.label3.Text = "Hedge";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(136, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "FIA";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(76, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "NFUND";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(24, 13);
            this.label17.TabIndex = 38;
            this.label17.Text = "MH";
            // 
            // chkManualAjuste
            // 
            this.chkManualAjuste.AutoSize = true;
            this.chkManualAjuste.Location = new System.Drawing.Point(733, 20);
            this.chkManualAjuste.Name = "chkManualAjuste";
            this.chkManualAjuste.Size = new System.Drawing.Size(116, 17);
            this.chkManualAjuste.TabIndex = 44;
            this.chkManualAjuste.Text = "Manual Adjustment";
            this.chkManualAjuste.UseVisualStyleBackColor = true;
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Location = new System.Drawing.Point(730, 40);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(40, 13);
            this.lblUpdate.TabIndex = 45;
            this.lblUpdate.Text = "update";
            this.lblUpdate.Visible = false;
            // 
            // chkViewInitial
            // 
            this.chkViewInitial.AutoSize = true;
            this.chkViewInitial.Location = new System.Drawing.Point(855, 20);
            this.chkViewInitial.Name = "chkViewInitial";
            this.chkViewInitial.Size = new System.Drawing.Size(76, 17);
            this.chkViewInitial.TabIndex = 46;
            this.chkViewInitial.Text = "View Initial";
            this.chkViewInitial.UseVisualStyleBackColor = true;
            this.chkViewInitial.CheckedChanged += new System.EventHandler(this.chkViewInitial_CheckedChanged);
            // 
            // frmTradeSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1124, 614);
            this.Controls.Add(this.chkViewInitial);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.chkManualAjuste);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.lblLastUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdSplitAll);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.dtg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTradeSplit";
            this.Text = "Broker Account Order Split";
            this.Load += new System.EventHandler(this.frmTradeSplit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgTradeSplit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgTradeSplit;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Button cmdSplitAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtLongHedge;
        private System.Windows.Forms.TextBox txtLongFIA;
        private System.Windows.Forms.TextBox txtLongNFUND;
        private System.Windows.Forms.TextBox txtLongMH;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtShortHedge;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtShortFIA;
        private System.Windows.Forms.TextBox txtShortNFUND;
        private System.Windows.Forms.TextBox txtShortMH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox chkManualAjuste;
        private System.Windows.Forms.Label lblLongTotal;
        private System.Windows.Forms.Label lblShortTotal;
        private System.Windows.Forms.Label lblLongRestante;
        private System.Windows.Forms.Label lblShortRestante;
        private System.Windows.Forms.Label lblUpdate;
        private System.Windows.Forms.CheckBox chkViewInitial;

    }
}