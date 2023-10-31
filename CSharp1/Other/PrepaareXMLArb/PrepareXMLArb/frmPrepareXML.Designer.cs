namespace PrepareXMLArb
{
    partial class frmPrepareXML
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
            this.cmdProcess = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(96, 53);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(103, 35);
            this.cmdProcess.TabIndex = 0;
            this.cmdProcess.Text = "Process File";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // frmPrepareXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 139);
            this.Controls.Add(this.cmdProcess);
            this.Name = "frmPrepareXML";
            this.Text = "Prepare XML Nest Arb";
            this.Load += new System.EventHandler(this.frmPrepareXML_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdProcess;
    }
}

