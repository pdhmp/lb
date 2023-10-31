namespace LiveBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrepareXML));
            this.cmdProcess = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrigPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(114, 12);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(121, 31);
            this.cmdProcess.TabIndex = 0;
            this.cmdProcess.Text = "Process File";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Exported File Path:";
            // 
            // txtOrigPath
            // 
            this.txtOrigPath.Location = new System.Drawing.Point(114, 59);
            this.txtOrigPath.Name = "txtOrigPath";
            this.txtOrigPath.Size = new System.Drawing.Size(231, 20);
            this.txtOrigPath.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(137, 102);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmPrepareXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(357, 142);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtOrigPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPrepareXML";
            this.Text = "Prepare XML Nest Arb";
            this.Load += new System.EventHandler(this.frmPrepareXML_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrigPath;
        private System.Windows.Forms.Button button1;
    }
}

