namespace LiveBook
{
    partial class frmCopySecurity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCopySecurity));
            this.txtName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtNestTicker = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtExpiration = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtOldId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(182, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 20);
            this.txtName.TabIndex = 2;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(48, 33);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 13);
            this.label23.TabIndex = 68;
            this.label23.Text = "New Security Name";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Location = new System.Drawing.Point(106, 123);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(101, 38);
            this.cmdInsert.TabIndex = 8;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = true;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(262, 123);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(101, 38);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtNestTicker
            // 
            this.txtNestTicker.Location = new System.Drawing.Point(182, 56);
            this.txtNestTicker.Name = "txtNestTicker";
            this.txtNestTicker.Size = new System.Drawing.Size(136, 20);
            this.txtNestTicker.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "New Security Nest Ticker";
            // 
            // txtExpiration
            // 
            this.txtExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtExpiration.Location = new System.Drawing.Point(182, 82);
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.Size = new System.Drawing.Size(150, 20);
            this.txtExpiration.TabIndex = 87;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(48, 86);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 88;
            this.label10.Text = "Expiration";
            // 
            // txtOldId
            // 
            this.txtOldId.Location = new System.Drawing.Point(182, 4);
            this.txtOldId.Name = "txtOldId";
            this.txtOldId.Size = new System.Drawing.Size(86, 20);
            this.txtOldId.TabIndex = 89;
            // 
            // frmCopySecurity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(484, 175);
            this.Controls.Add(this.txtOldId);
            this.Controls.Add(this.txtExpiration);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtNestTicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label23);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCopySecurity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create a Security based on another (Copy)";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCopySecurity_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button cmdInsert;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtNestTicker;
        public System.Windows.Forms.TextBox txtOldId;
        public System.Windows.Forms.DateTimePicker txtExpiration;
    }
}