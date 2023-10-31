namespace ImagineSync
{
    partial class frmImagineSync
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
            this.labCurrent = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labRunTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdAll = new System.Windows.Forms.Button();
            this.cmdGetPort = new System.Windows.Forms.Button();
            this.cmdUploadALL = new System.Windows.Forms.Button();
            this.tmrCheck = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labCurrent
            // 
            this.labCurrent.AutoSize = true;
            this.labCurrent.Location = new System.Drawing.Point(100, 188);
            this.labCurrent.Name = "labCurrent";
            this.labCurrent.Size = new System.Drawing.Size(0, 13);
            this.labCurrent.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Current:";
            // 
            // labRunTime
            // 
            this.labRunTime.AutoSize = true;
            this.labRunTime.Location = new System.Drawing.Point(100, 160);
            this.labRunTime.Name = "labRunTime";
            this.labRunTime.Size = new System.Drawing.Size(0, 13);
            this.labRunTime.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Last run time:";
            // 
            // cmdAll
            // 
            this.cmdAll.Location = new System.Drawing.Point(90, 107);
            this.cmdAll.Name = "cmdAll";
            this.cmdAll.Size = new System.Drawing.Size(124, 37);
            this.cmdAll.TabIndex = 9;
            this.cmdAll.Text = "Full Sync";
            this.cmdAll.UseVisualStyleBackColor = true;
            this.cmdAll.Click += new System.EventHandler(this.cmdAll_Click);
            // 
            // cmdGetPort
            // 
            this.cmdGetPort.Location = new System.Drawing.Point(90, 55);
            this.cmdGetPort.Name = "cmdGetPort";
            this.cmdGetPort.Size = new System.Drawing.Size(124, 37);
            this.cmdGetPort.TabIndex = 8;
            this.cmdGetPort.Text = "Download Portfolio";
            this.cmdGetPort.UseVisualStyleBackColor = true;
            this.cmdGetPort.Click += new System.EventHandler(this.cmdGetPort_Click);
            // 
            // cmdUploadALL
            // 
            this.cmdUploadALL.Location = new System.Drawing.Point(90, 12);
            this.cmdUploadALL.Name = "cmdUploadALL";
            this.cmdUploadALL.Size = new System.Drawing.Size(124, 37);
            this.cmdUploadALL.TabIndex = 7;
            this.cmdUploadALL.Text = "Update Positions";
            this.cmdUploadALL.UseVisualStyleBackColor = true;
            this.cmdUploadALL.Click += new System.EventHandler(this.cmdUploadALL_Click);
            // 
            // tmrCheck
            // 
            this.tmrCheck.Interval = 5000;
            this.tmrCheck.Tick += new System.EventHandler(this.tmrCheck_Tick);
            // 
            // frmImagineSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(326, 237);
            this.Controls.Add(this.labCurrent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labRunTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdAll);
            this.Controls.Add(this.cmdGetPort);
            this.Controls.Add(this.cmdUploadALL);
            this.Name = "frmImagineSync";
            this.Text = "Imagine Sync";
            this.Load += new System.EventHandler(this.frmImagineSync_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labCurrent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labRunTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdAll;
        private System.Windows.Forms.Button cmdGetPort;
        private System.Windows.Forms.Button cmdUploadALL;
        private System.Windows.Forms.Timer tmrCheck;
    }
}

