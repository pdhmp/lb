namespace NewMomentumUS
{
    partial class Form1
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
            this.txtIDTickerTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIDTickerComposite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWindow = new System.Windows.Forms.TextBox();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnRun = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStratNAV = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMedianWindow = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTargetVol = new System.Windows.Forms.TextBox();
            this.dgvStagedOrders = new System.Windows.Forms.DataGridView();
            this.chkAutoSendOrders = new System.Windows.Forms.CheckBox();
            this.btnSendOrders = new System.Windows.Forms.Button();
            this.chkAuction = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStagedOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIDTickerTemplate
            // 
            this.txtIDTickerTemplate.Location = new System.Drawing.Point(152, 65);
            this.txtIDTickerTemplate.Name = "txtIDTickerTemplate";
            this.txtIDTickerTemplate.Size = new System.Drawing.Size(89, 20);
            this.txtIDTickerTemplate.TabIndex = 0;
            this.txtIDTickerTemplate.Text = "5310";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID Ticker Template";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID Ticker Composite";
            // 
            // txtIDTickerComposite
            // 
            this.txtIDTickerComposite.Location = new System.Drawing.Point(152, 91);
            this.txtIDTickerComposite.Name = "txtIDTickerComposite";
            this.txtIDTickerComposite.Size = new System.Drawing.Size(89, 20);
            this.txtIDTickerComposite.TabIndex = 2;
            this.txtIDTickerComposite.Text = "27376";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Window";
            // 
            // txtWindow
            // 
            this.txtWindow.Location = new System.Drawing.Point(152, 169);
            this.txtWindow.Name = "txtWindow";
            this.txtWindow.Size = new System.Drawing.Size(89, 20);
            this.txtWindow.TabIndex = 4;
            this.txtWindow.Text = "20";
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(152, 117);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(89, 20);
            this.dtpIniDate.TabIndex = 6;
            this.dtpIniDate.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ini Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(152, 143);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(89, 20);
            this.dtpEndDate.TabIndex = 8;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(46, 293);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(191, 71);
            this.btnRun.TabIndex = 10;
            this.btnRun.Text = "RUN!";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Strategy NAV";
            // 
            // txtStratNAV
            // 
            this.txtStratNAV.Location = new System.Drawing.Point(152, 221);
            this.txtStratNAV.Name = "txtStratNAV";
            this.txtStratNAV.Size = new System.Drawing.Size(89, 20);
            this.txtStratNAV.TabIndex = 11;
            this.txtStratNAV.Text = "500000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Median Window";
            // 
            // txtMedianWindow
            // 
            this.txtMedianWindow.Location = new System.Drawing.Point(152, 195);
            this.txtMedianWindow.Name = "txtMedianWindow";
            this.txtMedianWindow.Size = new System.Drawing.Size(89, 20);
            this.txtMedianWindow.TabIndex = 13;
            this.txtMedianWindow.Text = "11";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "TargetVol";
            // 
            // txtTargetVol
            // 
            this.txtTargetVol.Location = new System.Drawing.Point(152, 247);
            this.txtTargetVol.Name = "txtTargetVol";
            this.txtTargetVol.Size = new System.Drawing.Size(89, 20);
            this.txtTargetVol.TabIndex = 15;
            this.txtTargetVol.Text = "0,010";
            // 
            // dgvStagedOrders
            // 
            this.dgvStagedOrders.AllowUserToAddRows = false;
            this.dgvStagedOrders.AllowUserToDeleteRows = false;
            this.dgvStagedOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStagedOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStagedOrders.Location = new System.Drawing.Point(290, 62);
            this.dgvStagedOrders.Name = "dgvStagedOrders";
            this.dgvStagedOrders.ReadOnly = true;
            this.dgvStagedOrders.RowTemplate.Height = 15;
            this.dgvStagedOrders.Size = new System.Drawing.Size(445, 482);
            this.dgvStagedOrders.TabIndex = 17;
            // 
            // chkAutoSendOrders
            // 
            this.chkAutoSendOrders.AutoSize = true;
            this.chkAutoSendOrders.Location = new System.Drawing.Point(588, 24);
            this.chkAutoSendOrders.Name = "chkAutoSendOrders";
            this.chkAutoSendOrders.Size = new System.Drawing.Size(147, 17);
            this.chkAutoSendOrders.TabIndex = 18;
            this.chkAutoSendOrders.Text = "Auto Send Staged Orders";
            this.chkAutoSendOrders.UseVisualStyleBackColor = true;
            this.chkAutoSendOrders.CheckedChanged += new System.EventHandler(this.chkAutoSendOrders_CheckedChanged);
            // 
            // btnSendOrders
            // 
            this.btnSendOrders.Location = new System.Drawing.Point(290, 16);
            this.btnSendOrders.Name = "btnSendOrders";
            this.btnSendOrders.Size = new System.Drawing.Size(121, 31);
            this.btnSendOrders.TabIndex = 19;
            this.btnSendOrders.Text = "Send Staged Orders";
            this.btnSendOrders.UseVisualStyleBackColor = true;
            this.btnSendOrders.Click += new System.EventHandler(this.btnSendOrders_Click);
            // 
            // chkAuction
            // 
            this.chkAuction.AutoSize = true;
            this.chkAuction.Location = new System.Drawing.Point(417, 24);
            this.chkAuction.Name = "chkAuction";
            this.chkAuction.Size = new System.Drawing.Size(62, 17);
            this.chkAuction.TabIndex = 20;
            this.chkAuction.Text = "Auction";
            this.chkAuction.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(777, 586);
            this.Controls.Add(this.chkAuction);
            this.Controls.Add(this.btnSendOrders);
            this.Controls.Add(this.chkAutoSendOrders);
            this.Controls.Add(this.dgvStagedOrders);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtTargetVol);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtMedianWindow);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStratNAV);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWindow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIDTickerComposite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIDTickerTemplate);
            this.Name = "Form1";
            this.Text = "Sector Momentum US";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStagedOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIDTickerTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIDTickerComposite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWindow;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStratNAV;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMedianWindow;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTargetVol;
        private System.Windows.Forms.DataGridView dgvStagedOrders;
        private System.Windows.Forms.CheckBox chkAutoSendOrders;
        private System.Windows.Forms.Button btnSendOrders;
        private System.Windows.Forms.CheckBox chkAuction;

    }
}

