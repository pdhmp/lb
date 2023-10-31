namespace NestFixRouter
{
    partial class frmRouter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRouter));
            this.btnStartAcceptor = new System.Windows.Forms.Button();
            this.btnStopAcceptor = new System.Windows.Forms.Button();
            this.btnStartInitiator = new System.Windows.Forms.Button();
            this.btnStopInitiator = new System.Windows.Forms.Button();
            this.txtRouter = new System.Windows.Forms.TextBox();
            this.grpInbound = new System.Windows.Forms.GroupBox();
            this.dgvInbound = new System.Windows.Forms.DataGridView();
            this.sStatusInbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sNameInbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sSessionInbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oEnableInbound = new System.Windows.Forms.DataGridViewButtonColumn();
            this.oDisableInbound = new System.Windows.Forms.DataGridViewButtonColumn();
            this.sLastInbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlInbound = new System.Windows.Forms.Panel();
            this.btnEnableInboundSessions = new System.Windows.Forms.Button();
            this.btnDisableInboundSessions = new System.Windows.Forms.Button();
            this.grpOutbound = new System.Windows.Forms.GroupBox();
            this.dgvOutbound = new System.Windows.Forms.DataGridView();
            this.sStatusOutbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sNameOutbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sSessionOutbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oStartOutbound = new System.Windows.Forms.DataGridViewButtonColumn();
            this.oStopOutbound = new System.Windows.Forms.DataGridViewButtonColumn();
            this.sLastOutbound = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlOutbound = new System.Windows.Forms.Panel();
            this.btnEnableAllOutboundSessions = new System.Windows.Forms.Button();
            this.btnDisableAllOutboundSessions = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.grpInbound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInbound)).BeginInit();
            this.pnlInbound.SuspendLayout();
            this.grpOutbound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutbound)).BeginInit();
            this.pnlOutbound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartAcceptor
            // 
            this.btnStartAcceptor.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartAcceptor.Location = new System.Drawing.Point(0, 0);
            this.btnStartAcceptor.Name = "btnStartAcceptor";
            this.btnStartAcceptor.Size = new System.Drawing.Size(125, 33);
            this.btnStartAcceptor.TabIndex = 0;
            this.btnStartAcceptor.Text = "Start Acceptor";
            this.btnStartAcceptor.UseVisualStyleBackColor = true;
            this.btnStartAcceptor.Click += new System.EventHandler(this.btnStartAcceptor_Click);
            // 
            // btnStopAcceptor
            // 
            this.btnStopAcceptor.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStopAcceptor.Enabled = false;
            this.btnStopAcceptor.Location = new System.Drawing.Point(125, 0);
            this.btnStopAcceptor.Name = "btnStopAcceptor";
            this.btnStopAcceptor.Size = new System.Drawing.Size(125, 33);
            this.btnStopAcceptor.TabIndex = 1;
            this.btnStopAcceptor.Text = "Stop Acceptor";
            this.btnStopAcceptor.UseVisualStyleBackColor = true;
            this.btnStopAcceptor.Click += new System.EventHandler(this.btnStopAcceptor_Click);
            // 
            // btnStartInitiator
            // 
            this.btnStartInitiator.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStartInitiator.Location = new System.Drawing.Point(0, 0);
            this.btnStartInitiator.Name = "btnStartInitiator";
            this.btnStartInitiator.Size = new System.Drawing.Size(125, 33);
            this.btnStartInitiator.TabIndex = 0;
            this.btnStartInitiator.Text = "Start Initiator";
            this.btnStartInitiator.UseVisualStyleBackColor = true;
            this.btnStartInitiator.Click += new System.EventHandler(this.btnStartInitiator_Click);
            // 
            // btnStopInitiator
            // 
            this.btnStopInitiator.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStopInitiator.Enabled = false;
            this.btnStopInitiator.Location = new System.Drawing.Point(125, 0);
            this.btnStopInitiator.Name = "btnStopInitiator";
            this.btnStopInitiator.Size = new System.Drawing.Size(125, 33);
            this.btnStopInitiator.TabIndex = 1;
            this.btnStopInitiator.Text = "Stop Initiator";
            this.btnStopInitiator.UseVisualStyleBackColor = true;
            this.btnStopInitiator.Click += new System.EventHandler(this.btnStopInitiator_Click);
            // 
            // txtRouter
            // 
            this.txtRouter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRouter.Location = new System.Drawing.Point(0, 0);
            this.txtRouter.Multiline = true;
            this.txtRouter.Name = "txtRouter";
            this.txtRouter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRouter.Size = new System.Drawing.Size(1138, 90);
            this.txtRouter.TabIndex = 2;
            // 
            // grpInbound
            // 
            this.grpInbound.Controls.Add(this.checkBox1);
            this.grpInbound.Controls.Add(this.dgvInbound);
            this.grpInbound.Controls.Add(this.pnlInbound);
            this.grpInbound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInbound.Location = new System.Drawing.Point(0, 0);
            this.grpInbound.Name = "grpInbound";
            this.grpInbound.Size = new System.Drawing.Size(558, 465);
            this.grpInbound.TabIndex = 0;
            this.grpInbound.TabStop = false;
            this.grpInbound.Text = "Inbound Connections";
            // 
            // dgvInbound
            // 
            this.dgvInbound.AllowUserToAddRows = false;
            this.dgvInbound.AllowUserToDeleteRows = false;
            this.dgvInbound.AllowUserToResizeColumns = false;
            this.dgvInbound.AllowUserToResizeRows = false;
            this.dgvInbound.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInbound.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvInbound.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvInbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInbound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sStatusInbound,
            this.sNameInbound,
            this.sSessionInbound,
            this.oEnableInbound,
            this.oDisableInbound,
            this.sLastInbound});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInbound.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvInbound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInbound.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInbound.Location = new System.Drawing.Point(3, 49);
            this.dgvInbound.MultiSelect = false;
            this.dgvInbound.Name = "dgvInbound";
            this.dgvInbound.ReadOnly = true;
            this.dgvInbound.RowHeadersVisible = false;
            this.dgvInbound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInbound.ShowEditingIcon = false;
            this.dgvInbound.Size = new System.Drawing.Size(552, 413);
            this.dgvInbound.TabIndex = 4;
            this.dgvInbound.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInbound_CellContentClick);
            // 
            // sStatusInbound
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.sStatusInbound.DefaultCellStyle = dataGridViewCellStyle1;
            this.sStatusInbound.FillWeight = 48.8506F;
            this.sStatusInbound.HeaderText = "Status";
            this.sStatusInbound.Name = "sStatusInbound";
            this.sStatusInbound.ReadOnly = true;
            this.sStatusInbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sStatusInbound.Width = 50;
            // 
            // sNameInbound
            // 
            this.sNameInbound.FillWeight = 108.0819F;
            this.sNameInbound.HeaderText = "Name";
            this.sNameInbound.Name = "sNameInbound";
            this.sNameInbound.ReadOnly = true;
            this.sNameInbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sNameInbound.Width = 120;
            // 
            // sSessionInbound
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.sSessionInbound.DefaultCellStyle = dataGridViewCellStyle2;
            this.sSessionInbound.HeaderText = "Session";
            this.sSessionInbound.Name = "sSessionInbound";
            this.sSessionInbound.ReadOnly = true;
            this.sSessionInbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sSessionInbound.Width = 50;
            // 
            // oEnableInbound
            // 
            this.oEnableInbound.FillWeight = 126.9036F;
            this.oEnableInbound.HeaderText = "Enable";
            this.oEnableInbound.Name = "oEnableInbound";
            this.oEnableInbound.ReadOnly = true;
            this.oEnableInbound.Width = 50;
            // 
            // oDisableInbound
            // 
            this.oDisableInbound.FillWeight = 108.0819F;
            this.oDisableInbound.HeaderText = "Disable";
            this.oDisableInbound.Name = "oDisableInbound";
            this.oDisableInbound.ReadOnly = true;
            this.oDisableInbound.Width = 50;
            // 
            // sLastInbound
            // 
            this.sLastInbound.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sLastInbound.FillWeight = 108.0819F;
            this.sLastInbound.HeaderText = "Last Event";
            this.sLastInbound.Name = "sLastInbound";
            this.sLastInbound.ReadOnly = true;
            this.sLastInbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pnlInbound
            // 
            this.pnlInbound.Controls.Add(this.btnEnableInboundSessions);
            this.pnlInbound.Controls.Add(this.btnDisableInboundSessions);
            this.pnlInbound.Controls.Add(this.btnStopAcceptor);
            this.pnlInbound.Controls.Add(this.btnStartAcceptor);
            this.pnlInbound.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInbound.Location = new System.Drawing.Point(3, 16);
            this.pnlInbound.Name = "pnlInbound";
            this.pnlInbound.Size = new System.Drawing.Size(552, 33);
            this.pnlInbound.TabIndex = 0;
            // 
            // btnEnableInboundSessions
            // 
            this.btnEnableInboundSessions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEnableInboundSessions.Location = new System.Drawing.Point(302, 0);
            this.btnEnableInboundSessions.Name = "btnEnableInboundSessions";
            this.btnEnableInboundSessions.Size = new System.Drawing.Size(125, 33);
            this.btnEnableInboundSessions.TabIndex = 2;
            this.btnEnableInboundSessions.Text = "Enable All Sessions";
            this.btnEnableInboundSessions.UseVisualStyleBackColor = true;
            this.btnEnableInboundSessions.Click += new System.EventHandler(this.btnEnableInboundSessions_Click);
            // 
            // btnDisableInboundSessions
            // 
            this.btnDisableInboundSessions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDisableInboundSessions.Location = new System.Drawing.Point(427, 0);
            this.btnDisableInboundSessions.Name = "btnDisableInboundSessions";
            this.btnDisableInboundSessions.Size = new System.Drawing.Size(125, 33);
            this.btnDisableInboundSessions.TabIndex = 3;
            this.btnDisableInboundSessions.Text = "Disable All Sessions";
            this.btnDisableInboundSessions.UseVisualStyleBackColor = true;
            this.btnDisableInboundSessions.Click += new System.EventHandler(this.btnDisableInboundSessions_Click);
            // 
            // grpOutbound
            // 
            this.grpOutbound.Controls.Add(this.dgvOutbound);
            this.grpOutbound.Controls.Add(this.pnlOutbound);
            this.grpOutbound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpOutbound.Location = new System.Drawing.Point(0, 0);
            this.grpOutbound.Name = "grpOutbound";
            this.grpOutbound.Size = new System.Drawing.Size(576, 465);
            this.grpOutbound.TabIndex = 1;
            this.grpOutbound.TabStop = false;
            this.grpOutbound.Text = "Outbound Connections";
            // 
            // dgvOutbound
            // 
            this.dgvOutbound.AllowUserToAddRows = false;
            this.dgvOutbound.AllowUserToDeleteRows = false;
            this.dgvOutbound.AllowUserToResizeColumns = false;
            this.dgvOutbound.AllowUserToResizeRows = false;
            this.dgvOutbound.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOutbound.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvOutbound.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvOutbound.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOutbound.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sStatusOutbound,
            this.sNameOutbound,
            this.sSessionOutbound,
            this.oStartOutbound,
            this.oStopOutbound,
            this.sLastOutbound});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOutbound.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvOutbound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOutbound.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvOutbound.Location = new System.Drawing.Point(3, 49);
            this.dgvOutbound.Name = "dgvOutbound";
            this.dgvOutbound.ReadOnly = true;
            this.dgvOutbound.RowHeadersVisible = false;
            this.dgvOutbound.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutbound.Size = new System.Drawing.Size(570, 413);
            this.dgvOutbound.TabIndex = 4;
            this.dgvOutbound.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOutbound_CellContentClick);
            // 
            // sStatusOutbound
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.sStatusOutbound.DefaultCellStyle = dataGridViewCellStyle4;
            this.sStatusOutbound.FillWeight = 50.76142F;
            this.sStatusOutbound.HeaderText = "Status";
            this.sStatusOutbound.Name = "sStatusOutbound";
            this.sStatusOutbound.ReadOnly = true;
            this.sStatusOutbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sStatusOutbound.Width = 50;
            // 
            // sNameOutbound
            // 
            this.sNameOutbound.FillWeight = 112.3096F;
            this.sNameOutbound.HeaderText = "Name";
            this.sNameOutbound.Name = "sNameOutbound";
            this.sNameOutbound.ReadOnly = true;
            this.sNameOutbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.sNameOutbound.Width = 120;
            // 
            // sSessionOutbound
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.sSessionOutbound.DefaultCellStyle = dataGridViewCellStyle5;
            this.sSessionOutbound.HeaderText = "Session";
            this.sSessionOutbound.Name = "sSessionOutbound";
            this.sSessionOutbound.ReadOnly = true;
            this.sSessionOutbound.Width = 50;
            // 
            // oStartOutbound
            // 
            this.oStartOutbound.FillWeight = 112.3096F;
            this.oStartOutbound.HeaderText = "Enable";
            this.oStartOutbound.Name = "oStartOutbound";
            this.oStartOutbound.ReadOnly = true;
            this.oStartOutbound.Width = 50;
            // 
            // oStopOutbound
            // 
            this.oStopOutbound.FillWeight = 112.3096F;
            this.oStopOutbound.HeaderText = "Disable";
            this.oStopOutbound.Name = "oStopOutbound";
            this.oStopOutbound.ReadOnly = true;
            this.oStopOutbound.Width = 50;
            // 
            // sLastOutbound
            // 
            this.sLastOutbound.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sLastOutbound.FillWeight = 112.3096F;
            this.sLastOutbound.HeaderText = "Last Event";
            this.sLastOutbound.Name = "sLastOutbound";
            this.sLastOutbound.ReadOnly = true;
            this.sLastOutbound.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // pnlOutbound
            // 
            this.pnlOutbound.Controls.Add(this.btnEnableAllOutboundSessions);
            this.pnlOutbound.Controls.Add(this.btnDisableAllOutboundSessions);
            this.pnlOutbound.Controls.Add(this.btnStopInitiator);
            this.pnlOutbound.Controls.Add(this.btnStartInitiator);
            this.pnlOutbound.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlOutbound.Location = new System.Drawing.Point(3, 16);
            this.pnlOutbound.Name = "pnlOutbound";
            this.pnlOutbound.Size = new System.Drawing.Size(570, 33);
            this.pnlOutbound.TabIndex = 0;
            // 
            // btnEnableAllOutboundSessions
            // 
            this.btnEnableAllOutboundSessions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEnableAllOutboundSessions.Location = new System.Drawing.Point(320, 0);
            this.btnEnableAllOutboundSessions.Name = "btnEnableAllOutboundSessions";
            this.btnEnableAllOutboundSessions.Size = new System.Drawing.Size(125, 33);
            this.btnEnableAllOutboundSessions.TabIndex = 2;
            this.btnEnableAllOutboundSessions.Text = "Enable All Sessions";
            this.btnEnableAllOutboundSessions.UseVisualStyleBackColor = true;
            this.btnEnableAllOutboundSessions.Click += new System.EventHandler(this.btnEnableOutboundSessions_Click);
            // 
            // btnDisableAllOutboundSessions
            // 
            this.btnDisableAllOutboundSessions.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDisableAllOutboundSessions.Location = new System.Drawing.Point(445, 0);
            this.btnDisableAllOutboundSessions.Name = "btnDisableAllOutboundSessions";
            this.btnDisableAllOutboundSessions.Size = new System.Drawing.Size(125, 33);
            this.btnDisableAllOutboundSessions.TabIndex = 3;
            this.btnDisableAllOutboundSessions.Text = "Disable All Sessions";
            this.btnDisableAllOutboundSessions.UseVisualStyleBackColor = true;
            this.btnDisableAllOutboundSessions.Click += new System.EventHandler(this.btnDisableOutboundSessions_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpInbound);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpOutbound);
            this.splitContainer1.Size = new System.Drawing.Size(1138, 465);
            this.splitContainer1.SplitterDistance = 558;
            this.splitContainer1.TabIndex = 6;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtRouter);
            this.splitContainer2.Size = new System.Drawing.Size(1138, 559);
            this.splitContainer2.SplitterDistance = 465;
            this.splitContainer2.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(128, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Auto Start";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmRouter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 559);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRouter";
            this.Text = "Nest Fix Router";
            this.Load += new System.EventHandler(this.frmRouter_Load);
            this.grpInbound.ResumeLayout(false);
            this.grpInbound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInbound)).EndInit();
            this.pnlInbound.ResumeLayout(false);
            this.grpOutbound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutbound)).EndInit();
            this.pnlOutbound.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartAcceptor;
        private System.Windows.Forms.Button btnStopAcceptor;
        private System.Windows.Forms.Button btnStartInitiator;
        private System.Windows.Forms.Button btnStopInitiator;
        private System.Windows.Forms.TextBox txtRouter;
        private System.Windows.Forms.GroupBox grpInbound;
        private System.Windows.Forms.GroupBox grpOutbound;
        private System.Windows.Forms.DataGridView dgvInbound;
        private System.Windows.Forms.Button btnEnableInboundSessions;
        private System.Windows.Forms.DataGridView dgvOutbound;
        private System.Windows.Forms.Panel pnlInbound;
        private System.Windows.Forms.Panel pnlOutbound;
        private System.Windows.Forms.Button btnDisableInboundSessions;
        private System.Windows.Forms.Button btnDisableAllOutboundSessions;
        private System.Windows.Forms.Button btnEnableAllOutboundSessions;
        private System.Windows.Forms.DataGridViewTextBoxColumn sStatusInbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNameInbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sSessionInbound;
        private System.Windows.Forms.DataGridViewButtonColumn oEnableInbound;
        private System.Windows.Forms.DataGridViewButtonColumn oDisableInbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLastInbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sStatusOutbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sNameOutbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sSessionOutbound;
        private System.Windows.Forms.DataGridViewButtonColumn oStartOutbound;
        private System.Windows.Forms.DataGridViewButtonColumn oStopOutbound;
        private System.Windows.Forms.DataGridViewTextBoxColumn sLastOutbound;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

