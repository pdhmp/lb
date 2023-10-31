namespace RTICK_GenerateVWAPBars
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdRunFile = new System.Windows.Forms.Button();
            this.lblTicker = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTimeTaken = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdRunFile
            // 
            this.cmdRunFile.Location = new System.Drawing.Point(85, 50);
            this.cmdRunFile.Name = "cmdRunFile";
            this.cmdRunFile.Size = new System.Drawing.Size(114, 30);
            this.cmdRunFile.TabIndex = 0;
            this.cmdRunFile.Text = "Run File";
            this.cmdRunFile.UseVisualStyleBackColor = true;
            this.cmdRunFile.Click += new System.EventHandler(this.cmdRunFile_Click);
            // 
            // lblTicker
            // 
            this.lblTicker.AutoSize = true;
            this.lblTicker.Location = new System.Drawing.Point(64, 105);
            this.lblTicker.Name = "lblTicker";
            this.lblTicker.Size = new System.Drawing.Size(35, 13);
            this.lblTicker.TabIndex = 1;
            this.lblTicker.Text = "label1";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(64, 136);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 13);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "label1";
            // 
            // lblTimeTaken
            // 
            this.lblTimeTaken.AutoSize = true;
            this.lblTimeTaken.Location = new System.Drawing.Point(64, 170);
            this.lblTimeTaken.Name = "lblTimeTaken";
            this.lblTimeTaken.Size = new System.Drawing.Size(35, 13);
            this.lblTimeTaken.TabIndex = 3;
            this.lblTimeTaken.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 268);
            this.Controls.Add(this.lblTimeTaken);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTicker);
            this.Controls.Add(this.cmdRunFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdRunFile;
        private System.Windows.Forms.Label lblTicker;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTimeTaken;
    }
}

