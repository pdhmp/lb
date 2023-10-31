namespace FIX_BMF
{
    partial class frmControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmControl));
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.tmrUpdateScreen = new System.Windows.Forms.Timer(this.components);
            this.chkSaveLog = new System.Windows.Forms.CheckBox();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();
            this.cmdStart = new System.Windows.Forms.Button();
            this.cmdReadLog = new System.Windows.Forms.Button();
            this.txtRecIdTicker = new System.Windows.Forms.TextBox();
            this.tmrAutoStart = new System.Windows.Forms.Timer(this.components);
            this.cmdDump = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessages.Location = new System.Drawing.Point(12, 85);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessages.Size = new System.Drawing.Size(465, 198);
            this.txtMessages.TabIndex = 0;
            // 
            // tmrUpdateScreen
            // 
            this.tmrUpdateScreen.Interval = 600;
            this.tmrUpdateScreen.Tick += new System.EventHandler(this.tmrUpdateScreen_Tick);
            // 
            // chkSaveLog
            // 
            this.chkSaveLog.AutoSize = true;
            this.chkSaveLog.Location = new System.Drawing.Point(140, 18);
            this.chkSaveLog.Name = "chkSaveLog";
            this.chkSaveLog.Size = new System.Drawing.Size(87, 17);
            this.chkSaveLog.TabIndex = 1;
            this.chkSaveLog.Text = "Record Data";
            this.chkSaveLog.UseVisualStyleBackColor = true;
            this.chkSaveLog.CheckedChanged += new System.EventHandler(this.chkSaveLog_CheckedChanged);
            // 
            // chkAutoUpdate
            // 
            this.chkAutoUpdate.AutoSize = true;
            this.chkAutoUpdate.Location = new System.Drawing.Point(352, 18);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Size = new System.Drawing.Size(98, 17);
            this.chkAutoUpdate.TabIndex = 2;
            this.chkAutoUpdate.Text = "Update Screen";
            this.chkAutoUpdate.UseVisualStyleBackColor = true;
            this.chkAutoUpdate.CheckedChanged += new System.EventHandler(this.chkAutoUpdate_CheckedChanged);
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(39, 12);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 23);
            this.cmdStart.TabIndex = 3;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.cmdStart_Click);
            // 
            // cmdReadLog
            // 
            this.cmdReadLog.Location = new System.Drawing.Point(39, 42);
            this.cmdReadLog.Name = "cmdReadLog";
            this.cmdReadLog.Size = new System.Drawing.Size(75, 23);
            this.cmdReadLog.TabIndex = 4;
            this.cmdReadLog.Text = "Read Logs";
            this.cmdReadLog.UseVisualStyleBackColor = true;
            this.cmdReadLog.Click += new System.EventHandler(this.cmdReadLog_Click);
            // 
            // txtRecIdTicker
            // 
            this.txtRecIdTicker.Location = new System.Drawing.Point(233, 16);
            this.txtRecIdTicker.Name = "txtRecIdTicker";
            this.txtRecIdTicker.Size = new System.Drawing.Size(60, 20);
            this.txtRecIdTicker.TabIndex = 5;
            this.txtRecIdTicker.Text = "INDQ10";
            // 
            // tmrAutoStart
            // 
            this.tmrAutoStart.Interval = 10000;
            this.tmrAutoStart.Tick += new System.EventHandler(this.tmrAutoStart_Tick);
            // 
            // cmdDump
            // 
            this.cmdDump.Location = new System.Drawing.Point(233, 42);
            this.cmdDump.Name = "cmdDump";
            this.cmdDump.Size = new System.Drawing.Size(60, 23);
            this.cmdDump.TabIndex = 6;
            this.cmdDump.Text = "Dump";
            this.cmdDump.UseVisualStyleBackColor = true;
            this.cmdDump.Click += new System.EventHandler(this.cmdDump_Click);
            // 
            // frmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 295);
            this.Controls.Add(this.cmdDump);
            this.Controls.Add(this.txtRecIdTicker);
            this.Controls.Add(this.cmdReadLog);
            this.Controls.Add(this.cmdStart);
            this.Controls.Add(this.chkAutoUpdate);
            this.Controls.Add(this.chkSaveLog);
            this.Controls.Add(this.txtMessages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmControl";
            this.Text = "FIX BMF";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmControl_FormClosing);
            this.Load += new System.EventHandler(this.frmControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Timer tmrUpdateScreen;
        private System.Windows.Forms.CheckBox chkSaveLog;
        private System.Windows.Forms.CheckBox chkAutoUpdate;
        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Button cmdReadLog;
        private System.Windows.Forms.TextBox txtRecIdTicker;
        private System.Windows.Forms.Timer tmrAutoStart;
        private System.Windows.Forms.Button cmdDump;
    }
}