namespace SGN
{
    partial class frmAddSecurityDate
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
            this.dtpd_LastTradeDate = new System.Windows.Forms.DateTimePicker();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dtpd_LastTradeDate
            // 
            this.dtpd_LastTradeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpd_LastTradeDate.Location = new System.Drawing.Point(85, 65);
            this.dtpd_LastTradeDate.Name = "dtpd_LastTradeDate";
            this.dtpd_LastTradeDate.Size = new System.Drawing.Size(98, 20);
            this.dtpd_LastTradeDate.TabIndex = 100;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(51, 109);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 101;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(158, 109);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 102;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 53);
            this.label1.TabIndex = 103;
            this.label1.Text = "Please enter the date as of which the new information will be valid. Data will be" +
                " copied from the most recent date available.";
            // 
            // frmAddSecurityDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 153);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.dtpd_LastTradeDate);
            this.Name = "frmAddSecurityDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Add new date";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpd_LastTradeDate;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Label label1;
    }
}