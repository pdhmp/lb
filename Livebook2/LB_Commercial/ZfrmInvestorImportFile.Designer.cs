namespace LiveBook
{
    partial class frmRebates
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblImportFile = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblImportFile);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 63);
            this.groupBox3.TabIndex = 84;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import File";
            // 
            // lblImportFile
            // 
            this.lblImportFile.AllowDrop = true;
            this.lblImportFile.Location = new System.Drawing.Point(6, 19);
            this.lblImportFile.Name = "lblImportFile";
            this.lblImportFile.Size = new System.Drawing.Size(215, 33);
            this.lblImportFile.TabIndex = 29;
            this.lblImportFile.Text = "Drag file here to import it into the system";
            this.lblImportFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblImportFile.Click += new System.EventHandler(this.lblImportFile_Click);
            this.lblImportFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragDrop);
            this.lblImportFile.DragOver += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragOver);
            // 
            // frmRebates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(371, 160);
            this.Controls.Add(this.groupBox3);
            this.Name = "frmRebates";
            this.Text = "Investors Rebates";
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblImportFile;
    }
}