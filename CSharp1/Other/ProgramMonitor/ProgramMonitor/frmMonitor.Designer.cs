namespace ProgramMonitor
{
    partial class frmMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitor));
            this.cmdStart = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tmrCheckPrograms = new System.Windows.Forms.Timer(this.components);
            this.cmdOpenDefs = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.nfiMonitor = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmdStart.Location = new System.Drawing.Point(319, 12);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(88, 34);
            this.cmdStart.TabIndex = 0;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = false;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(13, 58);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(394, 360);
            this.txtLog.TabIndex = 1;
            // 
            // tmrCheckPrograms
            // 
            this.tmrCheckPrograms.Interval = 60000;
            this.tmrCheckPrograms.Tick += new System.EventHandler(this.tmrCheckPrograms_Tick);
            // 
            // cmdOpenDefs
            // 
            this.cmdOpenDefs.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmdOpenDefs.Location = new System.Drawing.Point(13, 12);
            this.cmdOpenDefs.Name = "cmdOpenDefs";
            this.cmdOpenDefs.Size = new System.Drawing.Size(88, 34);
            this.cmdOpenDefs.TabIndex = 2;
            this.cmdOpenDefs.Text = "Open Defs File";
            this.cmdOpenDefs.UseVisualStyleBackColor = false;
            this.cmdOpenDefs.Click += new System.EventHandler(this.cmdOpenDefs_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cmdStop.Location = new System.Drawing.Point(107, 12);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(88, 34);
            this.cmdStop.TabIndex = 3;
            this.cmdStop.Text = "STOP";
            this.cmdStop.UseVisualStyleBackColor = false;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // nfiMonitor
            // 
            this.nfiMonitor.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.nfiMonitor.BalloonTipText = "Program Monitor Running";
            this.nfiMonitor.BalloonTipTitle = "TrayTest";
            this.nfiMonitor.Icon = ((System.Drawing.Icon)(resources.GetObject("nfiMonitor.Icon")));
            this.nfiMonitor.Text = "Nest Monitor";
            this.nfiMonitor.Visible = true;
            this.nfiMonitor.DoubleClick += new System.EventHandler(this.nfiMonitor_DoubleClick);
            this.nfiMonitor.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nfiMonitor_MouseDoubleClick);
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(419, 430);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdOpenDefs);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.cmdStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMonitor";
            this.Text = "Program Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmMonitor_Load);
            this.Resize += new System.EventHandler(this.frmMonitor_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer tmrCheckPrograms;
        private System.Windows.Forms.Button cmdOpenDefs;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.NotifyIcon nfiMonitor;
    }
}

