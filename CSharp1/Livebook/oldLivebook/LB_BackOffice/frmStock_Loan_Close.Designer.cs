namespace LiveBook
{
    partial class frmStock_Loan_Close
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStock_Loan_Close));
            this.txtFullQtd = new System.Windows.Forms.TextBox();
            this.txtRelID = new System.Windows.Forms.TextBox();
            this.rdPartialClose = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rdFullClose = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dtpIni = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // txtFullQtd
            // 
            this.txtFullQtd.Location = new System.Drawing.Point(258, 42);
            this.txtFullQtd.Name = "txtFullQtd";
            this.txtFullQtd.Size = new System.Drawing.Size(85, 20);
            this.txtFullQtd.TabIndex = 40;
            // 
            // txtRelID
            // 
            this.txtRelID.Location = new System.Drawing.Point(258, 16);
            this.txtRelID.Name = "txtRelID";
            this.txtRelID.Size = new System.Drawing.Size(85, 20);
            this.txtRelID.TabIndex = 39;
            // 
            // rdPartialClose
            // 
            this.rdPartialClose.AutoSize = true;
            this.rdPartialClose.Location = new System.Drawing.Point(77, 76);
            this.rdPartialClose.Name = "rdPartialClose";
            this.rdPartialClose.Size = new System.Drawing.Size(83, 17);
            this.rdPartialClose.TabIndex = 38;
            this.rdPartialClose.TabStop = true;
            this.rdPartialClose.Text = "Partial Close";
            this.rdPartialClose.UseVisualStyleBackColor = true;
            this.rdPartialClose.CheckedChanged += new System.EventHandler(this.rdPartialClose_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(190, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Related ID:";
            // 
            // rdFullClose
            // 
            this.rdFullClose.AutoSize = true;
            this.rdFullClose.Location = new System.Drawing.Point(77, 42);
            this.rdFullClose.Name = "rdFullClose";
            this.rdFullClose.Size = new System.Drawing.Size(70, 17);
            this.rdFullClose.TabIndex = 33;
            this.rdFullClose.TabStop = true;
            this.rdFullClose.Text = "Full Close";
            this.rdFullClose.UseVisualStyleBackColor = true;
            this.rdFullClose.CheckedChanged += new System.EventHandler(this.rdFullClose_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(205, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 28);
            this.button2.TabIndex = 36;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtQtd
            // 
            this.txtQtd.Location = new System.Drawing.Point(166, 73);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(74, 20);
            this.txtQtd.TabIndex = 31;
            this.txtQtd.Leave += new System.EventHandler(this.txtQtd_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Trade Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(94, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 28);
            this.button1.TabIndex = 35;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpIni
            // 
            this.dtpIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIni.Location = new System.Drawing.Point(77, 12);
            this.dtpIni.Name = "dtpIni";
            this.dtpIni.Size = new System.Drawing.Size(85, 20);
            this.dtpIni.TabIndex = 34;
            // 
            // frmStock_Loan_Close
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(383, 155);
            this.Controls.Add(this.txtFullQtd);
            this.Controls.Add(this.txtRelID);
            this.Controls.Add(this.rdPartialClose);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.rdFullClose);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpIni);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStock_Loan_Close";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Close Stock Loan";
            this.Load += new System.EventHandler(this.frmStock_Loan_Close_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtFullQtd;
        public System.Windows.Forms.TextBox txtRelID;
        public System.Windows.Forms.RadioButton rdPartialClose;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.RadioButton rdFullClose;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.DateTimePicker dtpIni;

    }
}