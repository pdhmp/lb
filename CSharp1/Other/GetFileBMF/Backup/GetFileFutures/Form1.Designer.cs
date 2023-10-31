namespace GetFileFutures
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
            this.cmdGetFile = new System.Windows.Forms.Button();
            this.dtpDateFile = new System.Windows.Forms.DateTimePicker();
            this.ProgressDownload = new System.Windows.Forms.ProgressBar();
            this.cmdRead = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdGetFile
            // 
            this.cmdGetFile.Location = new System.Drawing.Point(12, 49);
            this.cmdGetFile.Name = "cmdGetFile";
            this.cmdGetFile.Size = new System.Drawing.Size(75, 23);
            this.cmdGetFile.TabIndex = 0;
            this.cmdGetFile.Text = "Get File";
            this.cmdGetFile.UseVisualStyleBackColor = true;
            this.cmdGetFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // dtpDateFile
            // 
            this.dtpDateFile.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFile.Location = new System.Drawing.Point(12, 12);
            this.dtpDateFile.Name = "dtpDateFile";
            this.dtpDateFile.Size = new System.Drawing.Size(100, 20);
            this.dtpDateFile.TabIndex = 1;
            // 
            // ProgressDownload
            // 
            this.ProgressDownload.Location = new System.Drawing.Point(12, 78);
            this.ProgressDownload.Name = "ProgressDownload";
            this.ProgressDownload.Size = new System.Drawing.Size(293, 18);
            this.ProgressDownload.TabIndex = 2;
            // 
            // cmdRead
            // 
            this.cmdRead.Enabled = false;
            this.cmdRead.Location = new System.Drawing.Point(168, 49);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 3;
            this.cmdRead.Text = "Read File";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(230, 112);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblDisplay
            // 
            this.lblDisplay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.Location = new System.Drawing.Point(8, 110);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(216, 23);
            this.lblDisplay.TabIndex = 5;
            this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 155);
            this.Controls.Add(this.lblDisplay);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.ProgressDownload);
            this.Controls.Add(this.dtpDateFile);
            this.Controls.Add(this.cmdGetFile);
            this.Name = "Form1";
            this.Text = "Import File Futures Ágora";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdGetFile;
        private System.Windows.Forms.DateTimePicker dtpDateFile;
        private System.Windows.Forms.ProgressBar ProgressDownload;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Label lblDisplay;
    }
}

