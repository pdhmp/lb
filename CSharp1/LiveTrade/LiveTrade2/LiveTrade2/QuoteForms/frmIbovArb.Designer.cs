namespace LiveTrade2
{
    partial class frmIbovArb
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtgQuotes = new NCustomControls.MyGridControl();
            this.dgQuotes = new NCustomControls.MyGridView();
            this.chkAuction = new System.Windows.Forms.CheckBox();
            this.lblIndexLast = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFutureLast = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFairLast = new System.Windows.Forms.Label();
            this.lblFutureBid = new System.Windows.Forms.Label();
            this.lblFutureAsk = new System.Windows.Forms.Label();
            this.lblDI1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDI2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFutFair = new System.Windows.Forms.Label();
            this.lblCarryRate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgQuotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgQuotes
            // 
            this.dtgQuotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgQuotes.Location = new System.Drawing.Point(-1, 87);
            this.dtgQuotes.LookAndFeel.SkinName = "Blue";
            this.dtgQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgQuotes.MainView = this.dgQuotes;
            this.dtgQuotes.Name = "dtgQuotes";
            this.dtgQuotes.Size = new System.Drawing.Size(645, 630);
            this.dtgQuotes.TabIndex = 26;
            this.dtgQuotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgQuotes});
            this.dtgQuotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgQuotes_KeyDown);
            this.dtgQuotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            this.dtgQuotes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            // 
            // dgQuotes
            // 
            this.dgQuotes.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgQuotes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgQuotes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgQuotes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgQuotes.GridControl = this.dtgQuotes;
            this.dgQuotes.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgQuotes.Name = "dgQuotes";
            this.dgQuotes.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgQuotes.OptionsBehavior.Editable = false;
            this.dgQuotes.OptionsSelection.UseIndicatorForSelection = false;
            this.dgQuotes.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgQuotes.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgQuotes_CustomDrawGroupRow);
            this.dgQuotes.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgQuotes_RowStyle);
            // 
            // chkAuction
            // 
            this.chkAuction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAuction.AutoSize = true;
            this.chkAuction.Checked = true;
            this.chkAuction.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAuction.Location = new System.Drawing.Point(571, 2);
            this.chkAuction.Name = "chkAuction";
            this.chkAuction.Size = new System.Drawing.Size(62, 17);
            this.chkAuction.TabIndex = 27;
            this.chkAuction.Text = "Auction";
            this.chkAuction.UseVisualStyleBackColor = true;
            this.chkAuction.CheckedChanged += new System.EventHandler(this.chkAuction_CheckedChanged);
            // 
            // lblIndexLast
            // 
            this.lblIndexLast.AutoSize = true;
            this.lblIndexLast.Location = new System.Drawing.Point(74, 9);
            this.lblIndexLast.Name = "lblIndexLast";
            this.lblIndexLast.Size = new System.Drawing.Size(13, 13);
            this.lblIndexLast.TabIndex = 28;
            this.lblIndexLast.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Index Last";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "Future";
            // 
            // lblFutureLast
            // 
            this.lblFutureLast.AutoSize = true;
            this.lblFutureLast.Location = new System.Drawing.Point(74, 32);
            this.lblFutureLast.Name = "lblFutureLast";
            this.lblFutureLast.Size = new System.Drawing.Size(13, 13);
            this.lblFutureLast.TabIndex = 31;
            this.lblFutureLast.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(137, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Fair";
            // 
            // lblFairLast
            // 
            this.lblFairLast.AutoSize = true;
            this.lblFairLast.Location = new System.Drawing.Point(199, 9);
            this.lblFairLast.Name = "lblFairLast";
            this.lblFairLast.Size = new System.Drawing.Size(13, 13);
            this.lblFairLast.TabIndex = 33;
            this.lblFairLast.Text = "0";
            // 
            // lblFutureBid
            // 
            this.lblFutureBid.AutoSize = true;
            this.lblFutureBid.Location = new System.Drawing.Point(148, 32);
            this.lblFutureBid.Name = "lblFutureBid";
            this.lblFutureBid.Size = new System.Drawing.Size(13, 13);
            this.lblFutureBid.TabIndex = 34;
            this.lblFutureBid.Text = "0";
            // 
            // lblFutureAsk
            // 
            this.lblFutureAsk.AutoSize = true;
            this.lblFutureAsk.Location = new System.Drawing.Point(227, 32);
            this.lblFutureAsk.Name = "lblFutureAsk";
            this.lblFutureAsk.Size = new System.Drawing.Size(13, 13);
            this.lblFutureAsk.TabIndex = 35;
            this.lblFutureAsk.Text = "0";
            // 
            // lblDI1
            // 
            this.lblDI1.AutoSize = true;
            this.lblDI1.Location = new System.Drawing.Point(422, 9);
            this.lblDI1.Name = "lblDI1";
            this.lblDI1.Size = new System.Drawing.Size(13, 13);
            this.lblDI1.TabIndex = 36;
            this.lblDI1.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(360, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "DI Futures";
            // 
            // lblDI2
            // 
            this.lblDI2.AutoSize = true;
            this.lblDI2.Location = new System.Drawing.Point(422, 32);
            this.lblDI2.Name = "lblDI2";
            this.lblDI2.Size = new System.Drawing.Size(13, 13);
            this.lblDI2.TabIndex = 38;
            this.lblDI2.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Fair";
            // 
            // lblFutFair
            // 
            this.lblFutFair.AutoSize = true;
            this.lblFutFair.Location = new System.Drawing.Point(74, 54);
            this.lblFutFair.Name = "lblFutFair";
            this.lblFutFair.Size = new System.Drawing.Size(13, 13);
            this.lblFutFair.TabIndex = 40;
            this.lblFutFair.Text = "0";
            // 
            // lblCarryRate
            // 
            this.lblCarryRate.AutoSize = true;
            this.lblCarryRate.Location = new System.Drawing.Point(148, 54);
            this.lblCarryRate.Name = "lblCarryRate";
            this.lblCarryRate.Size = new System.Drawing.Size(13, 13);
            this.lblCarryRate.TabIndex = 41;
            this.lblCarryRate.Text = "0";
            // 
            // frmIbovArb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(645, 717);
            this.Controls.Add(this.lblCarryRate);
            this.Controls.Add(this.lblFutFair);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblDI2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblDI1);
            this.Controls.Add(this.lblFutureAsk);
            this.Controls.Add(this.lblFutureBid);
            this.Controls.Add(this.lblFairLast);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblFutureLast);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIndexLast);
            this.Controls.Add(this.chkAuction);
            this.Controls.Add(this.dtgQuotes);
            this.Name = "frmIbovArb";
            this.Text = "Quote";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIbovArb_FormClosing);
            this.Load += new System.EventHandler(this.frmIbovArb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgQuotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgQuotes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkAuction;
        public NCustomControls.MyGridView dgQuotes;
        public NCustomControls.MyGridControl dtgQuotes;
        private System.Windows.Forms.Label lblIndexLast;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFutureLast;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblFairLast;
        private System.Windows.Forms.Label lblFutureBid;
        private System.Windows.Forms.Label lblFutureAsk;
        private System.Windows.Forms.Label lblDI1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblDI2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFutFair;
        private System.Windows.Forms.Label lblCarryRate;
    }
}

