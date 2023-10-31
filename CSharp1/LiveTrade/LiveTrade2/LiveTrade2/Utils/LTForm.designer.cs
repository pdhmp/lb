namespace LiveTrade2
{
    partial class LTForm
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
            this.SuspendLayout();
            // 
            // LTForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 268);
            this.Name = "LTForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LTForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LBForm_FormClosed);
            this.Load += new System.EventHandler(this.LTForm_Load);
            this.Move += new System.EventHandler(this.LTForm_Move);
            this.Resize += new System.EventHandler(this.LTForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}