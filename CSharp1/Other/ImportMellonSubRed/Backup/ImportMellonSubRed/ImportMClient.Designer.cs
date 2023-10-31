namespace ImportMellonSubRed
{
    partial class ImportMClient
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
            this.cmdReadClip = new System.Windows.Forms.Button();
            this.cmdImportFile = new System.Windows.Forms.Button();
            this.lblFundName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdReadClip
            // 
            this.cmdReadClip.Location = new System.Drawing.Point(52, 33);
            this.cmdReadClip.Name = "cmdReadClip";
            this.cmdReadClip.Size = new System.Drawing.Size(133, 37);
            this.cmdReadClip.TabIndex = 0;
            this.cmdReadClip.Text = "Read Clipboard";
            this.cmdReadClip.UseVisualStyleBackColor = true;
            this.cmdReadClip.Click += new System.EventHandler(this.cmdReadClip_Click);
            // 
            // cmdImportFile
            // 
            this.cmdImportFile.Enabled = false;
            this.cmdImportFile.Location = new System.Drawing.Point(52, 119);
            this.cmdImportFile.Name = "cmdImportFile";
            this.cmdImportFile.Size = new System.Drawing.Size(133, 37);
            this.cmdImportFile.TabIndex = 1;
            this.cmdImportFile.Text = "Import Data";
            this.cmdImportFile.UseVisualStyleBackColor = true;
            this.cmdImportFile.Click += new System.EventHandler(this.cmdImportFile_Click);
            // 
            // lblFundName
            // 
            this.lblFundName.AutoSize = true;
            this.lblFundName.Location = new System.Drawing.Point(12, 83);
            this.lblFundName.Name = "lblFundName";
            this.lblFundName.Size = new System.Drawing.Size(0, 13);
            this.lblFundName.TabIndex = 2;
            // 
            // ImportMClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 172);
            this.Controls.Add(this.lblFundName);
            this.Controls.Add(this.cmdImportFile);
            this.Controls.Add(this.cmdReadClip);
            this.Name = "ImportMClient";
            this.Text = "Import Mellon Data";
            this.Load += new System.EventHandler(this.ImportMClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdReadClip;
        private System.Windows.Forms.Button cmdImportFile;
        private System.Windows.Forms.Label lblFundName;
    }
}

