namespace InsertFIXTrades
{
    partial class frmInsertFIX
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
            this.tmrRun = new System.Windows.Forms.Timer(this.components);
            this.lblLastRunTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNotInserted = new System.Windows.Forms.Label();
            this.lblInserted = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrRun
            // 
            this.tmrRun.Interval = 5000;
            this.tmrRun.Tick += new System.EventHandler(this.tmrRun_Tick);
            // 
            // lblLastRunTime
            // 
            this.lblLastRunTime.AutoSize = true;
            this.lblLastRunTime.Location = new System.Drawing.Point(122, 95);
            this.lblLastRunTime.Name = "lblLastRunTime";
            this.lblLastRunTime.Size = new System.Drawing.Size(0, 13);
            this.lblLastRunTime.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Last run time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Not Inserted:";
            // 
            // lblNotInserted
            // 
            this.lblNotInserted.AutoSize = true;
            this.lblNotInserted.Location = new System.Drawing.Point(122, 120);
            this.lblNotInserted.Name = "lblNotInserted";
            this.lblNotInserted.Size = new System.Drawing.Size(0, 13);
            this.lblNotInserted.TabIndex = 4;
            // 
            // lblInserted
            // 
            this.lblInserted.AutoSize = true;
            this.lblInserted.Location = new System.Drawing.Point(122, 144);
            this.lblInserted.Name = "lblInserted";
            this.lblInserted.Size = new System.Drawing.Size(0, 13);
            this.lblInserted.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Inserted:";
            // 
            // frmInsertFIX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(320, 191);
            this.Controls.Add(this.lblInserted);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNotInserted);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblLastRunTime);
            this.Name = "frmInsertFIX";
            this.Text = "InsertFIXTrades";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrRun;
        private System.Windows.Forms.Label lblLastRunTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNotInserted;
        private System.Windows.Forms.Label lblInserted;
        private System.Windows.Forms.Label label4;
    }
}

