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
            this.txtOpenQuantity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalQuantity = new System.Windows.Forms.TextBox();
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
            this.label7 = new System.Windows.Forms.Label();
            this.dtpExpiration = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(178, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Open Qtt:";
            // 
            // txtOpenQuantity
            // 
            this.txtOpenQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOpenQuantity.Location = new System.Drawing.Point(237, 56);
            this.txtOpenQuantity.Name = "txtOpenQuantity";
            this.txtOpenQuantity.Size = new System.Drawing.Size(85, 13);
            this.txtOpenQuantity.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Fw Qtt:";
            // 
            // txtTotalQuantity
            // 
            this.txtTotalQuantity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalQuantity.Location = new System.Drawing.Point(80, 56);
            this.txtTotalQuantity.Name = "txtTotalQuantity";
            this.txtTotalQuantity.Size = new System.Drawing.Size(85, 13);
            this.txtTotalQuantity.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Related ID:";
            // 
            // txtIdFoward
            // 
            this.txtIdFoward.Location = new System.Drawing.Point(246, 7);
            this.txtIdFoward.Name = "txtIdFoward";
            this.txtIdFoward.Size = new System.Drawing.Size(54, 20);
            this.txtIdFoward.TabIndex = 37;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(181, 141);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(92, 31);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtEarlyClose
            // 
            this.txtEarlyClose.Location = new System.Drawing.Point(91, 90);
            this.txtEarlyClose.Name = "txtEarlyClose";
            this.txtEarlyClose.Size = new System.Drawing.Size(74, 20);
            this.txtEarlyClose.TabIndex = 1;
            this.txtEarlyClose.Leave += new System.EventHandler(this.txtEarlyClose_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Close Date";
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(58, 141);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(92, 31);
            this.cmdOK.TabIndex = 3;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // dtpClose
            // 
            this.dtpClose.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpClose.Location = new System.Drawing.Point(74, 9);
            this.dtpClose.Name = "dtpClose";
            this.dtpClose.Size = new System.Drawing.Size(85, 20);
            this.dtpClose.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Close Quantity:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Settlement Days:";
            // 
            // txtSettlement
            // 
            this.txtSettlement.Location = new System.Drawing.Point(279, 90);
            this.txtSettlement.Name = "txtSettlement";
            this.txtSettlement.Size = new System.Drawing.Size(52, 20);
            this.txtSettlement.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(311, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Expiration";
            // 
            // dtpExpiration
            // 
            this.dtpExpiration.Enabled = false;
            this.dtpExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiration.Location = new System.Drawing.Point(370, 5);
            this.dtpExpiration.Name = "dtpExpiration";
            this.dtpExpiration.Size = new System.Drawing.Size(85, 20);
            this.dtpExpiration.TabIndex = 47;
            // 
            // frmEarlyFowards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(468, 184);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpExpiration);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSettlement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOpenQuantity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTotalQuantity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtIdFoward);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.txtEarlyClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.dtpClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEarlyFowards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Forward Early Close";
            this.Load += new System.EventHandler(this.frmEarlyFowards_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtOpenQuantity;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtTotalQuantity;
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
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.DateTimePicker dtpExpiration;
    }
}