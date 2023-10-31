namespace RTICKReaderDI
{
    partial class RTICKFileCrack
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
            this.label2 = new System.Windows.Forms.Label();
            this.progBar = new System.Windows.Forms.ProgressBar();
            this.lbInstrument = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Instrument";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Status";
            // 
            // progBar
            // 
            this.progBar.Location = new System.Drawing.Point(15, 109);
            this.progBar.Name = "progBar";
            this.progBar.Size = new System.Drawing.Size(415, 20);
            this.progBar.TabIndex = 2;
            // 
            // lbInstrument
            // 
            this.lbInstrument.AutoSize = true;
            this.lbInstrument.Location = new System.Drawing.Point(74, 9);
            this.lbInstrument.Name = "lbInstrument";
            this.lbInstrument.Size = new System.Drawing.Size(0, 13);
            this.lbInstrument.TabIndex = 3;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(74, 38);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 13);
            this.lbStatus.TabIndex = 4;
            // 
            // RTICKFileCrack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(442, 141);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.lbInstrument);
            this.Controls.Add(this.progBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "RTICKFileCrack";
            this.Text = "RTICK File Cracker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progBar;
        private System.Windows.Forms.Label lbInstrument;
        private System.Windows.Forms.Label lbStatus;
    }
}

