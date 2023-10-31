namespace LiveBook
{
    partial class frmEarlyFowards
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEarlyFowards));
            this.label3 = new System.Windows.Forms.Label();
            this.txtInitialQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAvailableQuantity = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdFoward = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtEarlyClose = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.dtpClose = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSettlement = new System.Windows.Forms.TextBox();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dgEvents = new DevExpress.XtraGrid.GridControl();
            this.gridEvents = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgEvents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "InitialQuantity";
            // 
            // txtInitialQuantity
            // 
            this.txtInitialQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInitialQuantity.Location = new System.Drawing.Point(127, 87);
            this.txtInitialQuantity.Name = "txtInitialQuantity";
            this.txtInitialQuantity.Size = new System.Drawing.Size(96, 13);
            this.txtInitialQuantity.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Available Quantity";
            // 
            // txtAvailableQuantity
            // 
            this.txtAvailableQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAvailableQuantity.Location = new System.Drawing.Point(127, 63);
            this.txtAvailableQuantity.Name = "txtAvailableQuantity";
            this.txtAvailableQuantity.Size = new System.Drawing.Size(96, 13);
            this.txtAvailableQuantity.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Related ID:";
            // 
            // txtIdFoward
            // 
            this.txtIdFoward.Location = new System.Drawing.Point(262, 25);
            this.txtIdFoward.Name = "txtIdFoward";
            this.txtIdFoward.Size = new System.Drawing.Size(65, 20);
            this.txtIdFoward.TabIndex = 37;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(265, 377);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(103, 22);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtEarlyClose
            // 
            this.txtEarlyClose.Location = new System.Drawing.Point(113, 116);
            this.txtEarlyClose.Name = "txtEarlyClose";
            this.txtEarlyClose.Size = new System.Drawing.Size(85, 20);
            this.txtEarlyClose.TabIndex = 1;
            this.txtEarlyClose.Leave += new System.EventHandler(this.txtEarlyClose_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Close Date";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(145, 377);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(103, 22);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // dtpClose
            // 
            this.dtpClose.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpClose.Location = new System.Drawing.Point(94, 25);
            this.dtpClose.Name = "dtpClose";
            this.dtpClose.Size = new System.Drawing.Size(96, 20);
            this.dtpClose.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Close Quantity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Settlement Days:";
            // 
            // txtSettlement
            // 
            this.txtSettlement.Location = new System.Drawing.Point(311, 116);
            this.txtSettlement.Name = "txtSettlement";
            this.txtSettlement.Size = new System.Drawing.Size(63, 20);
            this.txtSettlement.TabIndex = 2;
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Enabled = false;
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiration.Location = new System.Drawing.Point(392, 25);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(96, 20);
            this.dtpExpiration.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(333, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Expiration";
            // 
            // dgEvents
            // 
            this.dgEvents.Location = new System.Drawing.Point(12, 165);
            this.dgEvents.MainView = this.gridEvents;
            this.dgEvents.Name = "dgEvents";
            this.dgEvents.Size = new System.Drawing.Size(487, 199);
            this.dgEvents.TabIndex = 1;
            this.dgEvents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridEvents});
            // 
            // gridEvents
            // 
            this.gridEvents.GridControl = this.dgEvents;
            this.gridEvents.Name = "gridEvents";
            this.gridEvents.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.gridEvents.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.gridEvents.DoubleClick += new System.EventHandler(this.gridEvents_DoubleClick);
            // 
            // frmEarlyFowards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 411);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.dgEvents);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAvailableQuantity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtInitialQuantity);
            this.Controls.Add(this.txtEarlyClose);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSettlement);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.txtIdFoward);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEarlyFowards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Forward Early Close";
            this.Load += new System.EventHandler(this.frmEarlyFowards_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgEvents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtInitialQuantity;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtAvailableQuantity;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtIdFoward;
        private System.Windows.Forms.Button cmdCancel;
        public System.Windows.Forms.TextBox txtEarlyClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdOK;
        public System.Windows.Forms.DateTimePicker dtpClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtSettlement;
        public System.Windows.Forms.DateTimePicker dtpExpiration;
        private System.Windows.Forms.Label label7;
        private DevExpress.XtraGrid.GridControl dgEvents;
        private DevExpress.XtraGrid.Views.Grid.GridView gridEvents;
    }
}