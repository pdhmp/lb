namespace SGN
{
    partial class LBForm
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LBForm
            // 
            this.ClientSize = new System.Drawing.Size(290, 268);
            this.Name = "LBForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.LBForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LBForm_FormClosed);
            this.Move += new System.EventHandler(this.LBForm_Move);
            this.Resize += new System.EventHandler(this.LBForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
