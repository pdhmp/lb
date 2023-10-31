namespace FlexTest
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOrdID = new System.Windows.Forms.TextBox();
            this.btnSetOrdID = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSendOrder = new System.Windows.Forms.Button();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQTY = new System.Windows.Forms.TextBox();
            this.txtTickerID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSendOrders = new System.Windows.Forms.DataGridView();
            this.ClOrdID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TickerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remaining = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Filled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(239, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(82, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect FIX";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connection Status: ";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Red;
            this.lblStatus.Location = new System.Drawing.Point(129, 34);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Disconnected";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "OrdID: ";
            // 
            // txtOrdID
            // 
            this.txtOrdID.Location = new System.Drawing.Point(132, 59);
            this.txtOrdID.Name = "txtOrdID";
            this.txtOrdID.Size = new System.Drawing.Size(68, 20);
            this.txtOrdID.TabIndex = 4;
            // 
            // btnSetOrdID
            // 
            this.btnSetOrdID.Location = new System.Drawing.Point(239, 57);
            this.btnSetOrdID.Name = "btnSetOrdID";
            this.btnSetOrdID.Size = new System.Drawing.Size(82, 23);
            this.btnSetOrdID.TabIndex = 5;
            this.btnSetOrdID.Text = "Set OrdID";
            this.btnSetOrdID.UseVisualStyleBackColor = true;
            this.btnSetOrdID.Click += new System.EventHandler(this.btnSetOrdID_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSendOrder);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.txtQTY);
            this.groupBox1.Controls.Add(this.txtTickerID);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(26, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 108);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Order";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(206, 52);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSendOrder
            // 
            this.btnSendOrder.Location = new System.Drawing.Point(206, 23);
            this.btnSendOrder.Name = "btnSendOrder";
            this.btnSendOrder.Size = new System.Drawing.Size(82, 23);
            this.btnSendOrder.TabIndex = 7;
            this.btnSendOrder.Text = "Send Order";
            this.btnSendOrder.UseVisualStyleBackColor = true;
            this.btnSendOrder.Click += new System.EventHandler(this.btnSendOrder_Click);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(96, 68);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(71, 20);
            this.txtPrice.TabIndex = 5;
            this.txtPrice.Text = "-1";
            // 
            // txtQTY
            // 
            this.txtQTY.Location = new System.Drawing.Point(96, 46);
            this.txtQTY.Name = "txtQTY";
            this.txtQTY.Size = new System.Drawing.Size(71, 20);
            this.txtQTY.TabIndex = 4;
            this.txtQTY.Text = "1";
            // 
            // txtTickerID
            // 
            this.txtTickerID.Location = new System.Drawing.Point(96, 23);
            this.txtTickerID.Name = "txtTickerID";
            this.txtTickerID.Size = new System.Drawing.Size(71, 20);
            this.txtTickerID.TabIndex = 3;
            this.txtTickerID.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Price: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(55, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "QTY: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ticker ID: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvSendOrders);
            this.groupBox2.Location = new System.Drawing.Point(31, 227);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(738, 217);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sent Orders";
            // 
            // dgvSendOrders
            // 
            this.dgvSendOrders.AllowUserToAddRows = false;
            this.dgvSendOrders.AllowUserToDeleteRows = false;
            this.dgvSendOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSendOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSendOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClOrdID,
            this.TickerID,
            this.QTY,
            this.Price,
            this.Remaining,
            this.Filled});
            this.dgvSendOrders.Location = new System.Drawing.Point(15, 19);
            this.dgvSendOrders.Name = "dgvSendOrders";
            this.dgvSendOrders.ReadOnly = true;
            this.dgvSendOrders.Size = new System.Drawing.Size(705, 180);
            this.dgvSendOrders.TabIndex = 0;
            // 
            // ClOrdID
            // 
            this.ClOrdID.HeaderText = "ClOrdID";
            this.ClOrdID.Name = "ClOrdID";
            this.ClOrdID.ReadOnly = true;
            // 
            // TickerID
            // 
            this.TickerID.HeaderText = "Ticker ID";
            this.TickerID.Name = "TickerID";
            this.TickerID.ReadOnly = true;
            // 
            // QTY
            // 
            this.QTY.HeaderText = "QTY";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Remaining
            // 
            this.Remaining.HeaderText = "Remaining";
            this.Remaining.Name = "Remaining";
            this.Remaining.ReadOnly = true;
            // 
            // Filled
            // 
            this.Filled.HeaderText = "Filled";
            this.Filled.Name = "Filled";
            this.Filled.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(803, 467);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSetOrdID);
            this.Controls.Add(this.txtOrdID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSendOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOrdID;
        private System.Windows.Forms.Button btnSetOrdID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSendOrder;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQTY;
        private System.Windows.Forms.TextBox txtTickerID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSendOrders;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClOrdID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TickerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remaining;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Filled;
    }
}

