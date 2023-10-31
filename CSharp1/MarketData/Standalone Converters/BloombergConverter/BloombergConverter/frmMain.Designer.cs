namespace BloombergConverter
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.labUpdTime = new System.Windows.Forms.Label();
            this.tmrUpdateScreen = new System.Windows.Forms.Timer(this.components);
            this.lblConnected = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.labSubCounter = new System.Windows.Forms.Label();
            this.labValid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Update";
            // 
            // labUpdTime
            // 
            this.labUpdTime.AutoSize = true;
            this.labUpdTime.Location = new System.Drawing.Point(109, 21);
            this.labUpdTime.Name = "labUpdTime";
            this.labUpdTime.Size = new System.Drawing.Size(0, 13);
            this.labUpdTime.TabIndex = 1;
            // 
            // tmrUpdateScreen
            // 
            this.tmrUpdateScreen.Interval = 800;
            this.tmrUpdateScreen.Tick += new System.EventHandler(this.tmrUpdateScreen_Tick);
            // 
            // lblConnected
            // 
            this.lblConnected.AutoSize = true;
            this.lblConnected.BackColor = System.Drawing.Color.Gray;
            this.lblConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnected.ForeColor = System.Drawing.Color.White;
            this.lblConnected.Location = new System.Drawing.Point(66, 114);
            this.lblConnected.Name = "lblConnected";
            this.lblConnected.Size = new System.Drawing.Size(129, 16);
            this.lblConnected.TabIndex = 44;
            this.lblConnected.Text = "NOT INITIALIZED";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPort.ForeColor = System.Drawing.Color.Black;
            this.labPort.Location = new System.Drawing.Point(110, 51);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(0, 13);
            this.labPort.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 54;
            this.label5.Text = "Port";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(39, 157);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 56;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(141, 157);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 57;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // labSubCounter
            // 
            this.labSubCounter.AutoSize = true;
            this.labSubCounter.Location = new System.Drawing.Point(27, 80);
            this.labSubCounter.Name = "labSubCounter";
            this.labSubCounter.Size = new System.Drawing.Size(112, 13);
            this.labSubCounter.TabIndex = 58;
            this.labSubCounter.Text = "Total Subscriptions:  0";
            // 
            // labValid
            // 
            this.labValid.AutoSize = true;
            this.labValid.Location = new System.Drawing.Point(171, 80);
            this.labValid.Name = "labValid";
            this.labValid.Size = new System.Drawing.Size(45, 13);
            this.labValid.TabIndex = 59;
            this.labValid.Text = "Valid:  0";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(255, 194);
            this.Controls.Add(this.labValid);
            this.Controls.Add(this.labSubCounter);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.labPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblConnected);
            this.Controls.Add(this.labUpdTime);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Bloomberg Converter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labUpdTime;
        private System.Windows.Forms.Timer tmrUpdateScreen;
        private System.Windows.Forms.Label lblConnected;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label labSubCounter;
        private System.Windows.Forms.Label labValid;
    }
}

