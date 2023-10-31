namespace XPBMFConverter
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
            this.labTradeTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdDisconnect = new System.Windows.Forms.Button();
            this.labConnectedBSE = new System.Windows.Forms.Label();
            this.cmdReloadConfig = new System.Windows.Forms.Button();
            this.labConfigFile = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labPort = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Last Update";
            // 
            // labUpdTime
            // 
            this.labUpdTime.AutoSize = true;
            this.labUpdTime.Location = new System.Drawing.Point(136, 25);
            this.labUpdTime.Name = "labUpdTime";
            this.labUpdTime.Size = new System.Drawing.Size(0, 13);
            this.labUpdTime.TabIndex = 1;
            // 
            // tmrUpdateScreen
            // 
            this.tmrUpdateScreen.Interval = 800;
            this.tmrUpdateScreen.Tick += new System.EventHandler(this.tmrUpdateScreen_Tick);
            // 
            // labTradeTime
            // 
            this.labTradeTime.AutoSize = true;
            this.labTradeTime.Location = new System.Drawing.Point(136, 50);
            this.labTradeTime.Name = "labTradeTime";
            this.labTradeTime.Size = new System.Drawing.Size(0, 13);
            this.labTradeTime.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Last Trade";
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(68, 159);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(75, 23);
            this.cmdConnect.TabIndex = 4;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdDisconnect
            // 
            this.cmdDisconnect.Location = new System.Drawing.Point(175, 159);
            this.cmdDisconnect.Name = "cmdDisconnect";
            this.cmdDisconnect.Size = new System.Drawing.Size(75, 23);
            this.cmdDisconnect.TabIndex = 5;
            this.cmdDisconnect.Text = "Disconnect";
            this.cmdDisconnect.UseVisualStyleBackColor = true;
            this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
            // 
            // labConnectedBSE
            // 
            this.labConnectedBSE.AutoSize = true;
            this.labConnectedBSE.BackColor = System.Drawing.Color.Gray;
            this.labConnectedBSE.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labConnectedBSE.ForeColor = System.Drawing.Color.White;
            this.labConnectedBSE.Location = new System.Drawing.Point(103, 127);
            this.labConnectedBSE.Name = "labConnectedBSE";
            this.labConnectedBSE.Size = new System.Drawing.Size(129, 16);
            this.labConnectedBSE.TabIndex = 44;
            this.labConnectedBSE.Text = "NOT INITIALIZED";
            // 
            // cmdReloadConfig
            // 
            this.cmdReloadConfig.Location = new System.Drawing.Point(106, 196);
            this.cmdReloadConfig.Name = "cmdReloadConfig";
            this.cmdReloadConfig.Size = new System.Drawing.Size(108, 23);
            this.cmdReloadConfig.TabIndex = 45;
            this.cmdReloadConfig.Text = "Reload Config File";
            this.cmdReloadConfig.UseVisualStyleBackColor = true;
            this.cmdReloadConfig.Click += new System.EventHandler(this.cmdReloadConfig_Click);
            // 
            // labConfigFile
            // 
            this.labConfigFile.AutoSize = true;
            this.labConfigFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labConfigFile.ForeColor = System.Drawing.Color.Blue;
            this.labConfigFile.Location = new System.Drawing.Point(136, 75);
            this.labConfigFile.Name = "labConfigFile";
            this.labConfigFile.Size = new System.Drawing.Size(0, 13);
            this.labConfigFile.TabIndex = 49;
            this.labConfigFile.Click += new System.EventHandler(this.labConfigFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Config File";
            // 
            // labPort
            // 
            this.labPort.AutoSize = true;
            this.labPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPort.ForeColor = System.Drawing.Color.Black;
            this.labPort.Location = new System.Drawing.Point(136, 99);
            this.labPort.Name = "labPort";
            this.labPort.Size = new System.Drawing.Size(0, 13);
            this.labPort.TabIndex = 53;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 52;
            this.label5.Text = "Port";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 241);
            this.Controls.Add(this.labPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labConfigFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdReloadConfig);
            this.Controls.Add(this.labConnectedBSE);
            this.Controls.Add(this.cmdDisconnect);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.labTradeTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labUpdTime);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "XPBMF Converter";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labUpdTime;
        private System.Windows.Forms.Timer tmrUpdateScreen;
        private System.Windows.Forms.Label labTradeTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdDisconnect;
        private System.Windows.Forms.Label labConnectedBSE;
        private System.Windows.Forms.Button cmdReloadConfig;
        private System.Windows.Forms.Label labConfigFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labPort;
        private System.Windows.Forms.Label label5;
    }
}

