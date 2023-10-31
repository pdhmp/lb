namespace Jericho
{
    partial class frmJericho
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dgcSecurities = new NCustomControls.MyGridControl();
            this.dgvSecurities = new NCustomControls.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHdgEnbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPosCash = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblOpenPos = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblHdgCash = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurities)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurities)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgcOrders
            // 
            this.dgcOrders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgcOrders.Location = new System.Drawing.Point(12, 401);
            this.dgcOrders.LookAndFeel.SkinName = "Blue";
            this.dgcOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcOrders.MainView = this.dgvOrders;
            this.dgcOrders.Name = "dgcOrders";
            this.dgcOrders.Size = new System.Drawing.Size(797, 323);
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
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(110, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(274, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(77, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.dgcSecurities.Location = new System.Drawing.Point(12, 57);
            this.dgcSecurities.LookAndFeel.SkinName = "Blue";
            this.dgcSecurities.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcSecurities.MainView = this.dgvSecurities;
            this.dgcSecurities.Name = "dgcSecurities";
            this.dgcSecurities.Size = new System.Drawing.Size(1088, 327);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hedge Enabled: ";
            // 
            // lblHdgEnbl
            // 
            this.lblHdgEnbl.AutoSize = true;
            this.lblHdgEnbl.Location = new System.Drawing.Point(127, 63);
            this.lblHdgEnbl.Name = "lblHdgEnbl";
            this.lblHdgEnbl.Size = new System.Drawing.Size(29, 13);
            this.lblHdgEnbl.TabIndex = 5;
            this.lblHdgEnbl.Text = "false";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Action: ";
            // 
            // lblPosCash
            // 
            this.lblPosCash.AutoSize = true;
            this.lblPosCash.Location = new System.Drawing.Point(127, 86);
            this.lblPosCash.Name = "lblPosCash";
            this.lblPosCash.Size = new System.Drawing.Size(35, 13);
            this.lblPosCash.TabIndex = 8;
            this.lblPosCash.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Hedge Cash: ";
            // 
            // lblOpenPos
            // 
            this.lblOpenPos.AutoSize = true;
            this.lblOpenPos.Location = new System.Drawing.Point(127, 40);
            this.lblOpenPos.Name = "lblOpenPos";
            this.lblOpenPos.Size = new System.Drawing.Size(35, 13);
            this.lblOpenPos.TabIndex = 11;
            this.lblOpenPos.Text = "label8";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(127, 134);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(35, 13);
            this.lblAction.TabIndex = 12;
            this.lblAction.Text = "label9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Open Positions: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(29, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Position Cash: ";
            // 
            // lblHdgCash
            // 
            this.lblHdgCash.AutoSize = true;
            this.lblHdgCash.Location = new System.Drawing.Point(127, 110);
            this.lblHdgCash.Name = "lblHdgCash";
            this.lblHdgCash.Size = new System.Drawing.Size(41, 13);
            this.lblHdgCash.TabIndex = 15;
            this.lblHdgCash.Text = "label12";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblHdgCash);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblHdgEnbl);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblAction);
            this.groupBox1.Controls.Add(this.lblPosCash);
            this.groupBox1.Controls.Add(this.lblOpenPos);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(834, 401);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 178);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hedge";
            // 
            // frmJericho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 736);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgcSecurities);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgcOrders);
            this.Name = "frmJericho";
            this.Text = "Jericho";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmJericho_FormClosing);
            this.Load += new System.EventHandler(this.Jericho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSecurities)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSecurities)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NCustomControls.MyGridControl dgcOrders;
        private NCustomControls.MyGridView dgvOrders;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private NCustomControls.MyGridControl dgcSecurities;
        private NCustomControls.MyGridView dgvSecurities;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHdgEnbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPosCash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblOpenPos;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblHdgCash;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}

