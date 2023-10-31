namespace LiveTrade2
{
    partial class frmLimits
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaxOrderAmount = new System.Windows.Forms.TextBox();
            this.txtMaxOrderShares = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxTotalGross = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaxTotalNet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaxTotalShares = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMaxDOL = new System.Windows.Forms.TextBox();
            this.txtMaxIND = new System.Windows.Forms.TextBox();
            this.txtMaxDI1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Order Amount";
            // 
            // txtMaxOrderAmount
            // 
            this.txtMaxOrderAmount.Location = new System.Drawing.Point(194, 16);
            this.txtMaxOrderAmount.Name = "txtMaxOrderAmount";
            this.txtMaxOrderAmount.Size = new System.Drawing.Size(100, 20);
            this.txtMaxOrderAmount.TabIndex = 1;
            // 
            // txtMaxOrderShares
            // 
            this.txtMaxOrderShares.Location = new System.Drawing.Point(194, 42);
            this.txtMaxOrderShares.Name = "txtMaxOrderShares";
            this.txtMaxOrderShares.Size = new System.Drawing.Size(100, 20);
            this.txtMaxOrderShares.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Max Order Shares";
            // 
            // txtMaxTotalGross
            // 
            this.txtMaxTotalGross.Location = new System.Drawing.Point(194, 68);
            this.txtMaxTotalGross.Name = "txtMaxTotalGross";
            this.txtMaxTotalGross.Size = new System.Drawing.Size(100, 20);
            this.txtMaxTotalGross.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Max Total Gross";
            // 
            // txtMaxTotalNet
            // 
            this.txtMaxTotalNet.Location = new System.Drawing.Point(194, 94);
            this.txtMaxTotalNet.Name = "txtMaxTotalNet";
            this.txtMaxTotalNet.Size = new System.Drawing.Size(100, 20);
            this.txtMaxTotalNet.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Max Total Net";
            // 
            // txtMaxTotalShares
            // 
            this.txtMaxTotalShares.Location = new System.Drawing.Point(194, 120);
            this.txtMaxTotalShares.Name = "txtMaxTotalShares";
            this.txtMaxTotalShares.Size = new System.Drawing.Size(100, 20);
            this.txtMaxTotalShares.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Max Total Shares";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(170, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(287, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Max DOL Contracts";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Max IND Contracts";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(300, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Max DI1 Contracts";
            // 
            // txtMaxDOL
            // 
            this.txtMaxDOL.Location = new System.Drawing.Point(406, 12);
            this.txtMaxDOL.Name = "txtMaxDOL";
            this.txtMaxDOL.Size = new System.Drawing.Size(100, 20);
            this.txtMaxDOL.TabIndex = 15;
            // 
            // txtMaxIND
            // 
            this.txtMaxIND.Location = new System.Drawing.Point(406, 38);
            this.txtMaxIND.Name = "txtMaxIND";
            this.txtMaxIND.Size = new System.Drawing.Size(100, 20);
            this.txtMaxIND.TabIndex = 16;
            // 
            // txtMaxDI1
            // 
            this.txtMaxDI1.Location = new System.Drawing.Point(406, 64);
            this.txtMaxDI1.Name = "txtMaxDI1";
            this.txtMaxDI1.Size = new System.Drawing.Size(100, 20);
            this.txtMaxDI1.TabIndex = 17;
            // 
            // frmLimits
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(528, 210);
            this.Controls.Add(this.txtMaxDI1);
            this.Controls.Add(this.txtMaxIND);
            this.Controls.Add(this.txtMaxDOL);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtMaxTotalShares);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMaxTotalNet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxTotalGross);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaxOrderShares);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaxOrderAmount);
            this.Controls.Add(this.label1);
            this.Name = "frmLimits";
            this.Text = "Edit Limits";
            this.Load += new System.EventHandler(this.frmLimits_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaxOrderAmount;
        private System.Windows.Forms.TextBox txtMaxOrderShares;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxTotalGross;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaxTotalNet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMaxTotalShares;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMaxDOL;
        private System.Windows.Forms.TextBox txtMaxIND;
        private System.Windows.Forms.TextBox txtMaxDI1;
    }
}