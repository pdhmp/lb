namespace UpdateBDI
{
    partial class frmUpdateBDI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUpdateBDI));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblRead = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdRead = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.start = new System.Windows.Forms.Timer(this.components);
            this.cmdRead_BMF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(8, 107);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(376, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // lblRead
            // 
            this.lblRead.Location = new System.Drawing.Point(55, 74);
            this.lblRead.Name = "lblRead";
            this.lblRead.Size = new System.Drawing.Size(333, 15);
            this.lblRead.TabIndex = 8;
            this.lblRead.Text = "Lines";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(55, 52);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(333, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Status:";
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(8, 12);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(123, 26);
            this.cmdRead.TabIndex = 5;
            this.cmdRead.Text = "Read File - Bovespa";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 3600000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // start
            // 
            this.start.Interval = 3000;
            this.start.Tick += new System.EventHandler(this.start_Tick);
            // 
            // cmdRead_BMF
            // 
            this.cmdRead_BMF.Location = new System.Drawing.Point(147, 12);
            this.cmdRead_BMF.Name = "cmdRead_BMF";
            this.cmdRead_BMF.Size = new System.Drawing.Size(123, 26);
            this.cmdRead_BMF.TabIndex = 5;
            this.cmdRead_BMF.Text = "Read File - BMF";
            this.cmdRead_BMF.UseVisualStyleBackColor = true;
            this.cmdRead_BMF.Click += new System.EventHandler(this.cmdRead_BMF_Click);
            // 
            // frmUpdateBDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(393, 142);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblRead);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdRead_BMF);
            this.Controls.Add(this.cmdRead);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUpdateBDI";
            this.Text = "Update BDI";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblRead;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer start;
        private System.Windows.Forms.Button cmdRead_BMF;
    }
}

