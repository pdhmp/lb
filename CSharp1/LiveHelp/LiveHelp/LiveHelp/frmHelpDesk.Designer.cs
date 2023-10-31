namespace NestDesk
{
    partial class frmHelpDesk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHelpDesk));
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dgTicket = new DevExpress.XtraGrid.GridControl();
            this.dtgTicket = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.nfiLiveHelp = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctmRestore = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniRestaurar = new System.Windows.Forms.ToolStripMenuItem();
            this.chkOpen = new System.Windows.Forms.CheckBox();
            this.ctmClose = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTicket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTicket)).BeginInit();
            this.ctmRestore.SuspendLayout();
            this.ctmClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdInsert
            // 
            this.cmdInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdInsert.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdInsert.Location = new System.Drawing.Point(413, 78);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(96, 41);
            this.cmdInsert.TabIndex = 4;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbType);
            this.groupBox1.Controls.Add(this.txtObs);
            this.groupBox1.Controls.Add(this.cmbUser);
            this.groupBox1.Controls.Add(this.cmdInsert);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Location = new System.Drawing.Point(3, 236);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 136);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Ticket";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Comments:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "User:";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(9, 32);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(158, 21);
            this.cmbType.TabIndex = 0;
            // 
            // txtObs
            // 
            this.txtObs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObs.Location = new System.Drawing.Point(9, 78);
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(398, 41);
            this.txtObs.TabIndex = 3;
            // 
            // cmbUser
            // 
            this.cmbUser.Enabled = false;
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(173, 31);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(121, 21);
            this.cmbUser.TabIndex = 1;
            // 
            // dtpDate
            // 
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(311, 32);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(96, 20);
            this.dtpDate.TabIndex = 2;
            // 
            // dgTicket
            // 
            this.dgTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTicket.Location = new System.Drawing.Point(6, 12);
            this.dgTicket.MainView = this.dtgTicket;
            this.dgTicket.Name = "dgTicket";
            this.dgTicket.Size = new System.Drawing.Size(514, 218);
            this.dgTicket.TabIndex = 3;
            this.dgTicket.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgTicket});
            // 
            // dtgTicket
            // 
            this.dtgTicket.GridControl = this.dgTicket;
            this.dtgTicket.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dtgTicket.Name = "dtgTicket";
            this.dtgTicket.OptionsBehavior.Editable = false;
            this.dtgTicket.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dtgTicket.OptionsView.ColumnAutoWidth = false;
            this.dtgTicket.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dtgTicket_DragObjectDrop);
            this.dtgTicket.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dtgTicket_CustomDrawCell);
            this.dtgTicket.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dtgTicket_ColumnWidthChanged);
            this.dtgTicket.DoubleClick += new System.EventHandler(this.dtgTicket_DoubleClick);
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdrefresh.Location = new System.Drawing.Point(432, 20);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(56, 23);
            this.cmdrefresh.TabIndex = 7;
            this.cmdrefresh.Text = "Refresh";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // nfiLiveHelp
            // 
            this.nfiLiveHelp.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.nfiLiveHelp.BalloonTipText = "Nest Live Help Running";
            this.nfiLiveHelp.BalloonTipTitle = "TrayTest";
            this.nfiLiveHelp.ContextMenuStrip = this.ctmRestore;
            this.nfiLiveHelp.Icon = ((System.Drawing.Icon)(resources.GetObject("nfiLiveHelp.Icon")));
            this.nfiLiveHelp.Text = "LiveHelp";
            this.nfiLiveHelp.Visible = true;
            this.nfiLiveHelp.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // ctmRestore
            // 
            this.ctmRestore.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniRestaurar});
            this.ctmRestore.Name = "contextMenuStrip1";
            this.ctmRestore.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ctmRestore.Size = new System.Drawing.Size(124, 26);
            // 
            // mniRestaurar
            // 
            this.mniRestaurar.Name = "mniRestaurar";
            this.mniRestaurar.Size = new System.Drawing.Size(123, 22);
            this.mniRestaurar.Text = "Restaurar";
            this.mniRestaurar.Click += new System.EventHandler(this.mniRestaurar_Click);
            // 
            // chkOpen
            // 
            this.chkOpen.AutoSize = true;
            this.chkOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.chkOpen.Location = new System.Drawing.Point(374, 24);
            this.chkOpen.Name = "chkOpen";
            this.chkOpen.Size = new System.Drawing.Size(52, 17);
            this.chkOpen.TabIndex = 9;
            this.chkOpen.Text = "Open";
            this.chkOpen.UseVisualStyleBackColor = false;
            this.chkOpen.CheckedChanged += new System.EventHandler(this.chkOpen_CheckedChanged);
            // 
            // ctmClose
            // 
            this.ctmClose.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.ctmClose.Name = "contextMenuStrip1";
            this.ctmClose.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ctmClose.Size = new System.Drawing.Size(124, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(123, 22);
            this.toolStripMenuItem1.Text = "Restaurar";
            // 
            // frmHelpDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 381);
            this.Controls.Add(this.chkOpen);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.dgTicket);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmHelpDesk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiveHelp";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmHelpDesk_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.frmHelpDesk_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTicket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTicket)).EndInit();
            this.ctmRestore.ResumeLayout(false);
            this.ctmClose.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl dgTicket;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgTicket;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.NotifyIcon nfiLiveHelp;
        private System.Windows.Forms.ContextMenuStrip ctmRestore;
        private System.Windows.Forms.ToolStripMenuItem mniRestaurar;
        private System.Windows.Forms.CheckBox chkOpen;
        private System.Windows.Forms.ContextMenuStrip ctmClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

