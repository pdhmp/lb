namespace Feed_Bloomberg
{
    partial class Form1
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
            this.btnBBGStart = new System.Windows.Forms.Button();
            this.lblFuture = new System.Windows.Forms.Label();
            this.btnBBGStop = new System.Windows.Forms.Button();
            this.lblEqtOpt = new System.Windows.Forms.Label();
            this.btnBKPStart = new System.Windows.Forms.Button();
            this.btnBKPStop = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnBBGStart
            // 
            this.btnBBGStart.Location = new System.Drawing.Point(151, 27);
            this.btnBBGStart.Name = "btnBBGStart";
            this.btnBBGStart.Size = new System.Drawing.Size(73, 23);
            this.btnBBGStart.TabIndex = 0;
            this.btnBBGStart.Text = "Start";
            this.btnBBGStart.UseVisualStyleBackColor = true;
            this.btnBBGStart.Click += new System.EventHandler(this.btnFutStart_Click);
            // 
            // lblFuture
            // 
            this.lblFuture.AutoSize = true;
            this.lblFuture.Location = new System.Drawing.Point(24, 32);
            this.lblFuture.Name = "lblFuture";
            this.lblFuture.Size = new System.Drawing.Size(93, 13);
            this.lblFuture.TabIndex = 1;
            this.lblFuture.Text = "Bloomberg Feeder";
            // 
            // btnBBGStop
            // 
            this.btnBBGStop.Enabled = false;
            this.btnBBGStop.Location = new System.Drawing.Point(256, 27);
            this.btnBBGStop.Name = "btnBBGStop";
            this.btnBBGStop.Size = new System.Drawing.Size(73, 23);
            this.btnBBGStop.TabIndex = 2;
            this.btnBBGStop.Text = "Stop";
            this.btnBBGStop.UseVisualStyleBackColor = true;
            this.btnBBGStop.Click += new System.EventHandler(this.btnBBGStop_Click);
            // 
            // lblEqtOpt
            // 
            this.lblEqtOpt.AutoSize = true;
            this.lblEqtOpt.Location = new System.Drawing.Point(24, 75);
            this.lblEqtOpt.Name = "lblEqtOpt";
            this.lblEqtOpt.Size = new System.Drawing.Size(84, 13);
            this.lblEqtOpt.TabIndex = 4;
            this.lblEqtOpt.Text = "Reuters Backup";
            // 
            // btnBKPStart
            // 
            this.btnBKPStart.Location = new System.Drawing.Point(151, 70);
            this.btnBKPStart.Name = "btnBKPStart";
            this.btnBKPStart.Size = new System.Drawing.Size(73, 23);
            this.btnBKPStart.TabIndex = 5;
            this.btnBKPStart.Text = "Start";
            this.btnBKPStart.UseVisualStyleBackColor = true;
            this.btnBKPStart.Click += new System.EventHandler(this.btnBKPStart_Click);
            // 
            // btnBKPStop
            // 
            this.btnBKPStop.Enabled = false;
            this.btnBKPStop.Location = new System.Drawing.Point(256, 70);
            this.btnBKPStop.Name = "btnBKPStop";
            this.btnBKPStop.Size = new System.Drawing.Size(73, 23);
            this.btnBKPStop.TabIndex = 6;
            this.btnBKPStop.Text = "Stop";
            this.btnBKPStop.UseVisualStyleBackColor = true;
            this.btnBKPStop.Click += new System.EventHandler(this.btnBKPStop_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(27, 113);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(302, 79);
            this.txtStatus.TabIndex = 7;
            // 
            // timerStatus
            // 
            this.timerStatus.Enabled = true;
            this.timerStatus.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(358, 214);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnBKPStop);
            this.Controls.Add(this.btnBKPStart);
            this.Controls.Add(this.lblEqtOpt);
            this.Controls.Add(this.btnBBGStop);
            this.Controls.Add(this.lblFuture);
            this.Controls.Add(this.btnBBGStart);
            this.Name = "Form1";
            this.Text = "Bloomberg Feeder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBBGStart;
        private System.Windows.Forms.Label lblFuture;
        private System.Windows.Forms.Button btnBBGStop;
        private System.Windows.Forms.Label lblEqtOpt;
        private System.Windows.Forms.Button btnBKPStart;
        private System.Windows.Forms.Button btnBKPStop;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Timer timerStatus;
    }
}

