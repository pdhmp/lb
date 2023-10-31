namespace SimpleDequeuerForm
{
    partial class OrderManager
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
            this.dgcOrdersSummary = new NCustomControls.MyGridControl();
            this.dgvOrders = new NCustomControls.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgcOrders = new NCustomControls.MyGridControl();
            this.dgvSecurities = new NCustomControls.MyGridView();
            this.cbStrategies = new System.Windows.Forms.ComboBox();
            this.rdShowAll = new System.Windows.Forms.RadioButton();
            this.rdShowStrategy = new System.Windows.Forms.RadioButton();
            this.cbOptions = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrdersSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurities)).BeginInit();
            this.SuspendLayout();
            // 
            // dgcOrdersSummary
            // 
            this.dgcOrdersSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgcOrdersSummary.Location = new System.Drawing.Point(12, 429);
            this.dgcOrdersSummary.LookAndFeel.SkinName = "Blue";
            this.dgcOrdersSummary.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcOrdersSummary.MainView = this.dgvOrders;
            this.dgcOrdersSummary.Name = "dgcOrdersSummary";
            this.dgcOrdersSummary.Size = new System.Drawing.Size(577, 359);
            this.dgcOrdersSummary.TabIndex = 0;
            this.dgcOrdersSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvOrders});
            this.dgcOrdersSummary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcOrdersSummary.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvOrders
            // 
            this.dgvOrders.GridControl = this.dgcOrdersSummary;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.OptionsBehavior.Editable = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dgcOrders
            // 
            this.dgcOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgcOrders.Location = new System.Drawing.Point(12, 39);
            this.dgcOrders.LookAndFeel.SkinName = "Blue";
            this.dgcOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcOrders.MainView = this.dgvSecurities;
            this.dgcOrders.Name = "dgcOrders";
            this.dgcOrders.Size = new System.Drawing.Size(577, 357);
            this.dgcOrders.TabIndex = 3;
            this.dgcOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSecurities});
            this.dgcOrders.DoubleClick += new System.EventHandler(this.dgcOrders_Click);
            this.dgcOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcOrders.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvSecurities
            // 
            this.dgvSecurities.GridControl = this.dgcOrders;
            this.dgvSecurities.Name = "dgvSecurities";
            this.dgvSecurities.OptionsBehavior.Editable = false;
            // 
            // cbStrategies
            // 
            this.cbStrategies.FormattingEnabled = true;
            this.cbStrategies.Location = new System.Drawing.Point(468, 12);
            this.cbStrategies.Name = "cbStrategies";
            this.cbStrategies.Size = new System.Drawing.Size(121, 21);
            this.cbStrategies.TabIndex = 4;
            this.cbStrategies.SelectedIndexChanged += new System.EventHandler(this.cbStrategies_SelectedIndexChanged);
            // 
            // rdShowAll
            // 
            this.rdShowAll.AutoSize = true;
            this.rdShowAll.Location = new System.Drawing.Point(296, 16);
            this.rdShowAll.Name = "rdShowAll";
            this.rdShowAll.Size = new System.Drawing.Size(66, 17);
            this.rdShowAll.TabIndex = 5;
            this.rdShowAll.TabStop = true;
            this.rdShowAll.Text = "Show All";
            this.rdShowAll.UseVisualStyleBackColor = true;
            this.rdShowAll.CheckedChanged += new System.EventHandler(this.rdShowAll_CheckedChanged);
            // 
            // rdShowStrategy
            // 
            this.rdShowStrategy.AutoSize = true;
            this.rdShowStrategy.Location = new System.Drawing.Point(368, 16);
            this.rdShowStrategy.Name = "rdShowStrategy";
            this.rdShowStrategy.Size = new System.Drawing.Size(94, 17);
            this.rdShowStrategy.TabIndex = 6;
            this.rdShowStrategy.TabStop = true;
            this.rdShowStrategy.Text = "Show Strategy";
            this.rdShowStrategy.UseVisualStyleBackColor = true;
            this.rdShowStrategy.CheckedChanged += new System.EventHandler(this.rdShowStrategy_CheckedChanged);
            // 
            // cbOptions
            // 
            this.cbOptions.FormattingEnabled = true;
            this.cbOptions.Items.AddRange(new object[] {
            "NEW",
            "PARTIALLY FILLED",
            "FILLED",
            "CANCELED",
            "REJECTED"});
            this.cbOptions.Location = new System.Drawing.Point(480, 402);
            this.cbOptions.Name = "cbOptions";
            this.cbOptions.Size = new System.Drawing.Size(109, 21);
            this.cbOptions.TabIndex = 7;
            this.cbOptions.SelectedIndexChanged += new System.EventHandler(this.cbOptions_SelectedIndexChanged);
            // 
            // OrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 800);
            this.Controls.Add(this.cbOptions);
            this.Controls.Add(this.rdShowStrategy);
            this.Controls.Add(this.rdShowAll);
            this.Controls.Add(this.cbStrategies);
            this.Controls.Add(this.dgcOrders);
            this.Controls.Add(this.dgcOrdersSummary);
            this.Name = "OrderManager";
            this.Text = "OrderManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OrderManager_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrdersSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurities)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NCustomControls.MyGridControl dgcOrdersSummary;
        private NCustomControls.MyGridView dgvOrders;
        private System.Windows.Forms.Timer timer1;
        private NCustomControls.MyGridView dgvSecurities;
        private NCustomControls.MyGridControl dgcOrders;
        private System.Windows.Forms.ComboBox cbStrategies;
        private System.Windows.Forms.RadioButton rdShowAll;
        private System.Windows.Forms.RadioButton rdShowStrategy;
        private System.Windows.Forms.ComboBox cbOptions;

    }
}

