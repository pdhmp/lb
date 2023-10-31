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
            this.orderTableContainer = new System.Windows.Forms.Panel();
            this.dgOrders = new System.Windows.Forms.DataGridView();
            this.showStrategy = new System.Windows.Forms.RadioButton();
            this.showAll = new System.Windows.Forms.RadioButton();
            this.cbStrategies = new System.Windows.Forms.ComboBox();
            this.cancelAll = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgParcialOrder = new System.Windows.Forms.DataGridView();
            this.cbModeView = new System.Windows.Forms.ComboBox();
            this.orderTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgParcialOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // orderTableContainer
            // 
            this.orderTableContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.orderTableContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.orderTableContainer.Controls.Add(this.dgOrders);
            this.orderTableContainer.Location = new System.Drawing.Point(10, 74);
            this.orderTableContainer.Name = "orderTableContainer";
            this.orderTableContainer.Size = new System.Drawing.Size(568, 391);
            this.orderTableContainer.TabIndex = 0;
            // 
            // dgOrders
            // 
            this.dgOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgOrders.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOrders.Location = new System.Drawing.Point(3, 3);
            this.dgOrders.Name = "dgOrders";
            this.dgOrders.Size = new System.Drawing.Size(558, 381);
            this.dgOrders.TabIndex = 0;
            this.dgOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOrders_CellContentClick);
            // 
            // showStrategy
            // 
            this.showStrategy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showStrategy.AutoSize = true;
            this.showStrategy.Location = new System.Drawing.Point(357, 51);
            this.showStrategy.Name = "showStrategy";
            this.showStrategy.Size = new System.Drawing.Size(94, 17);
            this.showStrategy.TabIndex = 1;
            this.showStrategy.TabStop = true;
            this.showStrategy.Text = "Show Strategy";
            this.showStrategy.UseVisualStyleBackColor = true;
            this.showStrategy.CheckedChanged += new System.EventHandler(this.showStrategy_CheckedChanged);
            // 
            // showAll
            // 
            this.showAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showAll.AutoSize = true;
            this.showAll.Checked = true;
            this.showAll.Location = new System.Drawing.Point(276, 51);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(66, 17);
            this.showAll.TabIndex = 2;
            this.showAll.TabStop = true;
            this.showAll.Text = "Show All";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.CheckedChanged += new System.EventHandler(this.showAll_CheckedChanged);
            // 
            // cbStrategies
            // 
            this.cbStrategies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbStrategies.Enabled = false;
            this.cbStrategies.FormattingEnabled = true;
            this.cbStrategies.Location = new System.Drawing.Point(457, 47);
            this.cbStrategies.Name = "cbStrategies";
            this.cbStrategies.Size = new System.Drawing.Size(121, 21);
            this.cbStrategies.TabIndex = 3;
            // 
            // cancelAll
            // 
            this.cancelAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelAll.Location = new System.Drawing.Point(408, 853);
            this.cancelAll.Name = "cancelAll";
            this.cancelAll.Size = new System.Drawing.Size(75, 23);
            this.cancelAll.TabIndex = 4;
            this.cancelAll.Text = "Cancel All";
            this.cancelAll.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Location = new System.Drawing.Point(498, 853);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 5;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgParcialOrder);
            this.panel1.Location = new System.Drawing.Point(10, 497);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(568, 350);
            this.panel1.TabIndex = 6;
            // 
            // dgParcialOrder
            // 
            this.dgParcialOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgParcialOrder.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgParcialOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgParcialOrder.Location = new System.Drawing.Point(3, 3);
            this.dgParcialOrder.Name = "dgParcialOrder";
            this.dgParcialOrder.Size = new System.Drawing.Size(558, 340);
            this.dgParcialOrder.TabIndex = 0;
            // 
            // cbModeView
            // 
            this.cbModeView.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbModeView.FormattingEnabled = true;
            this.cbModeView.Items.AddRange(new object[] {
            "New",
            "Partially Filled",
            "Filled",
            "Cancelled",
            "Rejected"});
            this.cbModeView.Location = new System.Drawing.Point(452, 470);
            this.cbModeView.Name = "cbModeView";
            this.cbModeView.Size = new System.Drawing.Size(121, 21);
            this.cbModeView.TabIndex = 7;
            this.cbModeView.SelectedIndex = 0;

            // 
            // OrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 888);
            this.Controls.Add(this.cbModeView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.cancelAll);
            this.Controls.Add(this.cbStrategies);
            this.Controls.Add(this.showAll);
            this.Controls.Add(this.showStrategy);
            this.Controls.Add(this.orderTableContainer);
            this.Name = "OrderManager";
            this.Text = "OrderManager";
            this.orderTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOrders)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgParcialOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel orderTableContainer;
        private System.Windows.Forms.DataGridView dgOrders;
        private System.Windows.Forms.RadioButton showStrategy;
        private System.Windows.Forms.RadioButton showAll;
        private System.Windows.Forms.ComboBox cbStrategies;
        private System.Windows.Forms.Button cancelAll;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgParcialOrder;
        private System.Windows.Forms.ComboBox cbModeView;
        
    }
}