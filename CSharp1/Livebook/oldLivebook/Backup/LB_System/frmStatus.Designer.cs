namespace SGN
{
    partial class frmstatus
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
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(12, 12);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(310, 160);
            this.lstStatus.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 6000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 184);
            this.ControlBox = false;
            this.Controls.Add(this.lstStatus);
            this.Location = new System.Drawing.Point(1065, 0);
            this.Name = "frmStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "System Status";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmStatus_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStatus_FormClosing);
            this.Resize += new System.EventHandler(this.frmStatus_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Timer timer1;
    }
}