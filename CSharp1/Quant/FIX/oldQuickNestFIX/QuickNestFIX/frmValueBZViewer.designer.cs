namespace QuantMonitor
{
    partial class frmValueBZViewer
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
            this.btnGetStaged = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSendOrders = new System.Windows.Forms.Button();
            this.dgvStagedOrders = new System.Windows.Forms.DataGridView();
            this.Send = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Cancel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chkAuction = new System.Windows.Forms.CheckBox();
            this.btnStageOrders = new System.Windows.Forms.Button();
            this.dtgStratData = new NCustomControls.MyGridControl();
            this.dgStratData = new NCustomControls.MyGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStratNAV = new System.Windows.Forms.TextBox();
            this.tmrRefreshGrid = new System.Windows.Forms.Timer(this.components);
            this.cmdConnect = new System.Windows.Forms.Button();
            this.labConnStatus = new System.Windows.Forms.Label();
            this.labFIXStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStagedOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStratData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgStratData)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGetStaged
            // 
            this.btnGetStaged.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetStaged.Location = new System.Drawing.Point(612, 371);
            this.btnGetStaged.Name = "btnGetStaged";
            this.btnGetStaged.Size = new System.Drawing.Size(99, 31);
            this.btnGetStaged.TabIndex = 50;
            this.btnGetStaged.Text = "Get Staged";
            this.btnGetStaged.UseVisualStyleBackColor = true;
            this.btnGetStaged.Click += new System.EventHandler(this.btnGetStaged_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(569, 107);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(272, 372);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 30);
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "Cancel All";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSendOrders
            // 
            this.btnSendOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendOrders.Location = new System.Drawing.Point(527, 590);
            this.btnSendOrders.Name = "btnSendOrders";
            this.btnSendOrders.Size = new System.Drawing.Size(121, 30);
            this.btnSendOrders.TabIndex = 47;
            this.btnSendOrders.Text = "Send All";
            this.btnSendOrders.UseVisualStyleBackColor = true;
            this.btnSendOrders.Click += new System.EventHandler(this.btnSendOrders_Click);
            // 
            // dgvStagedOrders
            // 
            this.dgvStagedOrders.AllowUserToAddRows = false;
            this.dgvStagedOrders.AllowUserToDeleteRows = false;
            this.dgvStagedOrders.AllowUserToResizeRows = false;
            this.dgvStagedOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStagedOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStagedOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStagedOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Send,
            this.Cancel});
            this.dgvStagedOrders.Location = new System.Drawing.Point(12, 408);
            this.dgvStagedOrders.Name = "dgvStagedOrders";
            this.dgvStagedOrders.ReadOnly = true;
            this.dgvStagedOrders.RowHeadersVisible = false;
            this.dgvStagedOrders.RowTemplate.Height = 15;
            this.dgvStagedOrders.Size = new System.Drawing.Size(699, 170);
            this.dgvStagedOrders.TabIndex = 46;
            this.dgvStagedOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStagedOrders_CellClick);
            // 
            // Send
            // 
            this.Send.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Send.HeaderText = "Send Order";
            this.Send.Name = "Send";
            this.Send.ReadOnly = true;
            this.Send.Text = "Send";
            // 
            // Cancel
            // 
            this.Cancel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cancel.HeaderText = "Cancel Order";
            this.Cancel.Name = "Cancel";
            this.Cancel.ReadOnly = true;
            this.Cancel.Text = "Cancel";
            // 
            // chkAuction
            // 
            this.chkAuction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAuction.AutoSize = true;
            this.chkAuction.Location = new System.Drawing.Point(654, 598);
            this.chkAuction.Name = "chkAuction";
            this.chkAuction.Size = new System.Drawing.Size(62, 17);
            this.chkAuction.TabIndex = 45;
            this.chkAuction.Text = "Auction";
            this.chkAuction.UseVisualStyleBackColor = true;
            // 
            // btnStageOrders
            // 
            this.btnStageOrders.Location = new System.Drawing.Point(167, 371);
            this.btnStageOrders.Name = "btnStageOrders";
            this.btnStageOrders.Size = new System.Drawing.Size(99, 31);
            this.btnStageOrders.TabIndex = 44;
            this.btnStageOrders.Text = "Stage Orders";
            this.btnStageOrders.UseVisualStyleBackColor = true;
            this.btnStageOrders.Click += new System.EventHandler(this.btnStageOrders_Click);
            // 
            // dtgStratData
            // 
            this.dtgStratData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgStratData.Location = new System.Drawing.Point(12, 38);
            this.dtgStratData.LookAndFeel.SkinName = "Blue";
            this.dtgStratData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgStratData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgStratData.MainView = this.dgStratData;
            this.dtgStratData.Name = "dtgStratData";
            this.dtgStratData.Size = new System.Drawing.Size(551, 328);
            this.dtgStratData.TabIndex = 26;
            this.dtgStratData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgStratData});
            // 
            // dgStratData
            // 
            this.dgStratData.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgStratData.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgStratData.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgStratData.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgStratData.GridControl = this.dtgStratData;
            this.dgStratData.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgStratData.Name = "dgStratData";
            this.dgStratData.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgStratData.OptionsBehavior.Editable = false;
            this.dgStratData.OptionsSelection.UseIndicatorForSelection = false;
            this.dgStratData.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgStratData_DragObjectDrop);
            this.dgStratData.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgStratData_CustomDrawGroupRow);
            this.dgStratData.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgStratData_ColumnWidthChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 38;
            this.label6.Text = "Strategy NAV";
            // 
            // txtStratNAV
            // 
            this.txtStratNAV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtStratNAV.Location = new System.Drawing.Point(124, 12);
            this.txtStratNAV.Name = "txtStratNAV";
            this.txtStratNAV.Size = new System.Drawing.Size(89, 20);
            this.txtStratNAV.TabIndex = 37;
            this.txtStratNAV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tmrRefreshGrid
            // 
            this.tmrRefreshGrid.Tick += new System.EventHandler(this.tmrRefreshGrid_Tick);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdConnect.Location = new System.Drawing.Point(569, 78);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 51;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // labConnStatus
            // 
            this.labConnStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labConnStatus.AutoSize = true;
            this.labConnStatus.BackColor = System.Drawing.Color.LightGray;
            this.labConnStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labConnStatus.Location = new System.Drawing.Point(566, 50);
            this.labConnStatus.Margin = new System.Windows.Forms.Padding(0);
            this.labConnStatus.Name = "labConnStatus";
            this.labConnStatus.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labConnStatus.Size = new System.Drawing.Size(48, 21);
            this.labConnStatus.TabIndex = 53;
            this.labConnStatus.Text = "NONE";
            this.labConnStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labFIXStatus
            // 
            this.labFIXStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labFIXStatus.AutoSize = true;
            this.labFIXStatus.BackColor = System.Drawing.Color.LightGray;
            this.labFIXStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFIXStatus.Location = new System.Drawing.Point(566, 170);
            this.labFIXStatus.Margin = new System.Windows.Forms.Padding(0);
            this.labFIXStatus.Name = "labFIXStatus";
            this.labFIXStatus.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labFIXStatus.Size = new System.Drawing.Size(48, 21);
            this.labFIXStatus.TabIndex = 52;
            this.labFIXStatus.Text = "NONE";
            this.labFIXStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmValueBZViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(723, 629);
            this.Controls.Add(this.labConnStatus);
            this.Controls.Add(this.labFIXStatus);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.btnGetStaged);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendOrders);
            this.Controls.Add(this.dgvStagedOrders);
            this.Controls.Add(this.chkAuction);
            this.Controls.Add(this.btnStageOrders);
            this.Controls.Add(this.dtgStratData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStratNAV);
            this.MaximizeBox = false;
            this.Name = "frmValueBZViewer";
            this.Text = "Sector Value BZ";
            this.Load += new System.EventHandler(this.frmValueBZViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStagedOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStratData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgStratData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetStaged;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSendOrders;
        private System.Windows.Forms.DataGridView dgvStagedOrders;
        private System.Windows.Forms.DataGridViewButtonColumn Send;
        private System.Windows.Forms.DataGridViewButtonColumn Cancel;
        private System.Windows.Forms.CheckBox chkAuction;
        private System.Windows.Forms.Button btnStageOrders;
        public NCustomControls.MyGridControl dtgStratData;
        public NCustomControls.MyGridView dgStratData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStratNAV;
        private System.Windows.Forms.Timer tmrRefreshGrid;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Label labConnStatus;
        private System.Windows.Forms.Label labFIXStatus;
    }
}