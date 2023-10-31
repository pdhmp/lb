namespace Tomahawk
{
    partial class btnCancelAll
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
            this.dgcSymbols = new NCustomControls.MyGridControl();
            this.dgvSymbols = new NCustomControls.MyGridView();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.dgcOrders = new NCustomControls.MyGridControl();
            this.dgvOrders = new NCustomControls.MyGridView();
            this.tmrHedge = new System.Windows.Forms.Timer(this.components);
            this.btnSendOrders = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFHAction = new System.Windows.Forms.Label();
            this.lblFHNewPos = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblFHPrevPos = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblFHExec = new System.Windows.Forms.Label();
            this.lblFHTicker = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMHAction = new System.Windows.Forms.Label();
            this.lblMHNewPos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblMHPrevPos = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblMHExec = new System.Windows.Forms.Label();
            this.lblMHTicker = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblValueDiff = new System.Windows.Forms.Label();
            this.lblTotalHedge = new System.Windows.Forms.Label();
            this.lblMHValue2 = new System.Windows.Forms.Label();
            this.lblFHValue2 = new System.Windows.Forms.Label();
            this.lblNetValue = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendHedge = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgcSymbols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgcSymbols
            // 
            this.dgcSymbols.Location = new System.Drawing.Point(12, 77);
            this.dgcSymbols.LookAndFeel.SkinName = "Blue";
            this.dgcSymbols.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcSymbols.MainView = this.dgvSymbols;
            this.dgcSymbols.Name = "dgcSymbols";
            this.dgcSymbols.Size = new System.Drawing.Size(836, 326);
            this.dgcSymbols.TabIndex = 0;
            this.dgcSymbols.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvSymbols});
            this.dgcSymbols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcSymbols.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvSymbols
            // 
            this.dgvSymbols.GridControl = this.dgcSymbols;
            this.dgvSymbols.Name = "dgvSymbols";
            this.dgvSymbols.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgvSymbols.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // dgcOrders
            // 
            this.dgcOrders.Location = new System.Drawing.Point(12, 409);
            this.dgcOrders.LookAndFeel.SkinName = "Blue";
            this.dgcOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgcOrders.MainView = this.dgvOrders;
            this.dgcOrders.Name = "dgcOrders";
            this.dgcOrders.Size = new System.Drawing.Size(836, 260);
            this.dgcOrders.TabIndex = 1;
            this.dgcOrders.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgvOrders});
            this.dgcOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgcOrders.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // dgvOrders
            // 
            this.dgvOrders.GridControl = this.dgcOrders;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridMouseDown);
            this.dgvOrders.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridMouseUp);
            // 
            // tmrHedge
            // 
            this.tmrHedge.Interval = 1000;
            this.tmrHedge.Tick += new System.EventHandler(this.tmrHedge_Tick);
            // 
            // btnSendOrders
            // 
            this.btnSendOrders.Location = new System.Drawing.Point(877, 77);
            this.btnSendOrders.Name = "btnSendOrders";
            this.btnSendOrders.Size = new System.Drawing.Size(154, 47);
            this.btnSendOrders.TabIndex = 2;
            this.btnSendOrders.Text = "Send Orders";
            this.btnSendOrders.UseVisualStyleBackColor = true;
            this.btnSendOrders.Click += new System.EventHandler(this.btnSendOrders_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(877, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 49);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel All Orders";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFHAction);
            this.groupBox1.Controls.Add(this.lblFHNewPos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblFHPrevPos);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblFHExec);
            this.groupBox1.Controls.Add(this.lblFHTicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(858, 402);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 131);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Full";
            // 
            // lblFHAction
            // 
            this.lblFHAction.AutoSize = true;
            this.lblFHAction.Location = new System.Drawing.Point(116, 105);
            this.lblFHAction.Name = "lblFHAction";
            this.lblFHAction.Size = new System.Drawing.Size(41, 13);
            this.lblFHAction.TabIndex = 9;
            this.lblFHAction.Text = "label10";
            // 
            // lblFHNewPos
            // 
            this.lblFHNewPos.AutoSize = true;
            this.lblFHNewPos.Location = new System.Drawing.Point(116, 61);
            this.lblFHNewPos.Name = "lblFHNewPos";
            this.lblFHNewPos.Size = new System.Drawing.Size(35, 13);
            this.lblFHNewPos.TabIndex = 7;
            this.lblFHNewPos.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "New Position: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(67, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Action: ";
            // 
            // lblFHPrevPos
            // 
            this.lblFHPrevPos.AutoSize = true;
            this.lblFHPrevPos.Location = new System.Drawing.Point(116, 38);
            this.lblFHPrevPos.Name = "lblFHPrevPos";
            this.lblFHPrevPos.Size = new System.Drawing.Size(35, 13);
            this.lblFHPrevPos.TabIndex = 5;
            this.lblFHPrevPos.Text = "label6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Previous Position: ";
            // 
            // lblFHExec
            // 
            this.lblFHExec.AutoSize = true;
            this.lblFHExec.Location = new System.Drawing.Point(116, 83);
            this.lblFHExec.Name = "lblFHExec";
            this.lblFHExec.Size = new System.Drawing.Size(35, 13);
            this.lblFHExec.TabIndex = 3;
            this.lblFHExec.Text = "label4";
            // 
            // lblFHTicker
            // 
            this.lblFHTicker.AutoSize = true;
            this.lblFHTicker.Location = new System.Drawing.Point(116, 16);
            this.lblFHTicker.Name = "lblFHTicker";
            this.lblFHTicker.Size = new System.Drawing.Size(35, 13);
            this.lblFHTicker.TabIndex = 1;
            this.lblFHTicker.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Exec Pos: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ticker: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblMHAction);
            this.groupBox2.Controls.Add(this.lblMHNewPos);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblMHPrevPos);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lblMHExec);
            this.groupBox2.Controls.Add(this.lblMHTicker);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Location = new System.Drawing.Point(858, 538);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 131);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mini";
            // 
            // lblMHAction
            // 
            this.lblMHAction.AutoSize = true;
            this.lblMHAction.Location = new System.Drawing.Point(116, 105);
            this.lblMHAction.Name = "lblMHAction";
            this.lblMHAction.Size = new System.Drawing.Size(41, 13);
            this.lblMHAction.TabIndex = 9;
            this.lblMHAction.Text = "label10";
            // 
            // lblMHNewPos
            // 
            this.lblMHNewPos.AutoSize = true;
            this.lblMHNewPos.Location = new System.Drawing.Point(116, 61);
            this.lblMHNewPos.Name = "lblMHNewPos";
            this.lblMHNewPos.Size = new System.Drawing.Size(35, 13);
            this.lblMHNewPos.TabIndex = 7;
            this.lblMHNewPos.Text = "label8";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "New Position: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(67, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Action: ";
            // 
            // lblMHPrevPos
            // 
            this.lblMHPrevPos.AutoSize = true;
            this.lblMHPrevPos.Location = new System.Drawing.Point(116, 38);
            this.lblMHPrevPos.Name = "lblMHPrevPos";
            this.lblMHPrevPos.Size = new System.Drawing.Size(35, 13);
            this.lblMHPrevPos.TabIndex = 5;
            this.lblMHPrevPos.Text = "label6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Previous Position: ";
            // 
            // lblMHExec
            // 
            this.lblMHExec.AutoSize = true;
            this.lblMHExec.Location = new System.Drawing.Point(116, 83);
            this.lblMHExec.Name = "lblMHExec";
            this.lblMHExec.Size = new System.Drawing.Size(35, 13);
            this.lblMHExec.TabIndex = 3;
            this.lblMHExec.Text = "label4";
            // 
            // lblMHTicker
            // 
            this.lblMHTicker.AutoSize = true;
            this.lblMHTicker.Location = new System.Drawing.Point(116, 16);
            this.lblMHTicker.Name = "lblMHTicker";
            this.lblMHTicker.Size = new System.Drawing.Size(35, 13);
            this.lblMHTicker.TabIndex = 1;
            this.lblMHTicker.Text = "label2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(52, 83);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Exec Pos: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(67, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Ticker: ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblValueDiff);
            this.groupBox3.Controls.Add(this.lblTotalHedge);
            this.groupBox3.Controls.Add(this.lblMHValue2);
            this.groupBox3.Controls.Add(this.lblFHValue2);
            this.groupBox3.Controls.Add(this.lblNetValue);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(858, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 139);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Hedge Value";
            // 
            // lblValueDiff
            // 
            this.lblValueDiff.AutoSize = true;
            this.lblValueDiff.Location = new System.Drawing.Point(116, 113);
            this.lblValueDiff.Name = "lblValueDiff";
            this.lblValueDiff.Size = new System.Drawing.Size(41, 13);
            this.lblValueDiff.TabIndex = 9;
            this.lblValueDiff.Text = "label20";
            // 
            // lblTotalHedge
            // 
            this.lblTotalHedge.AutoSize = true;
            this.lblTotalHedge.Location = new System.Drawing.Point(116, 91);
            this.lblTotalHedge.Name = "lblTotalHedge";
            this.lblTotalHedge.Size = new System.Drawing.Size(41, 13);
            this.lblTotalHedge.TabIndex = 8;
            this.lblTotalHedge.Text = "label19";
            // 
            // lblMHValue2
            // 
            this.lblMHValue2.AutoSize = true;
            this.lblMHValue2.Location = new System.Drawing.Point(116, 69);
            this.lblMHValue2.Name = "lblMHValue2";
            this.lblMHValue2.Size = new System.Drawing.Size(41, 13);
            this.lblMHValue2.TabIndex = 7;
            this.lblMHValue2.Text = "label18";
            // 
            // lblFHValue2
            // 
            this.lblFHValue2.AutoSize = true;
            this.lblFHValue2.Location = new System.Drawing.Point(116, 47);
            this.lblFHValue2.Name = "lblFHValue2";
            this.lblFHValue2.Size = new System.Drawing.Size(41, 13);
            this.lblFHValue2.TabIndex = 6;
            this.lblFHValue2.Text = "label17";
            // 
            // lblNetValue
            // 
            this.lblNetValue.AutoSize = true;
            this.lblNetValue.Location = new System.Drawing.Point(116, 25);
            this.lblNetValue.Name = "lblNetValue";
            this.lblNetValue.Size = new System.Drawing.Size(41, 13);
            this.lblNetValue.TabIndex = 5;
            this.lblNetValue.Text = "label16";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(51, 113);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Value Diff: ";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(102, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Total Hedge Value: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 69);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Mini Hedge Value: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Full Hedge Value: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Net Exec Value: ";
            // 
            // btnSendHedge
            // 
            this.btnSendHedge.Location = new System.Drawing.Point(874, 185);
            this.btnSendHedge.Name = "btnSendHedge";
            this.btnSendHedge.Size = new System.Drawing.Size(154, 49);
            this.btnSendHedge.TabIndex = 12;
            this.btnSendHedge.Text = "Send Hedge";
            this.btnSendHedge.UseVisualStyleBackColor = true;
            this.btnSendHedge.Click += new System.EventHandler(this.btnSendHedge_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(46, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(179, 47);
            this.btnLoad.TabIndex = 13;
            this.btnLoad.Text = "Load Data";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnCancelAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1086, 686);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSendHedge);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSendOrders);
            this.Controls.Add(this.dgcOrders);
            this.Controls.Add(this.dgcSymbols);
            this.Name = "btnCancelAll";
            this.Text = "Tomahawk";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgcSymbols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgcOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NCustomControls.MyGridControl dgcSymbols;
        private NCustomControls.MyGridView dgvSymbols;
        private System.Windows.Forms.Timer tmrRefresh;
        private NCustomControls.MyGridControl dgcOrders;
        private NCustomControls.MyGridView dgvOrders;
        private System.Windows.Forms.Timer tmrHedge;
        private System.Windows.Forms.Button btnSendOrders;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFHAction;
        private System.Windows.Forms.Label lblFHNewPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblFHPrevPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFHExec;
        private System.Windows.Forms.Label lblFHTicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMHAction;
        private System.Windows.Forms.Label lblMHNewPos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblMHPrevPos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblMHExec;
        private System.Windows.Forms.Label lblMHTicker;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblValueDiff;
        private System.Windows.Forms.Label lblTotalHedge;
        private System.Windows.Forms.Label lblMHValue2;
        private System.Windows.Forms.Label lblFHValue2;
        private System.Windows.Forms.Label lblNetValue;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendHedge;
        private System.Windows.Forms.Button btnLoad;
    }
}

