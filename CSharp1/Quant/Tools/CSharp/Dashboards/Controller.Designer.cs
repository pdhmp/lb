namespace Dashboards
{
    partial class Controller
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.btSendCommand = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BMFReqBt = new System.Windows.Forms.Button();
            this.symbolBMF = new System.Windows.Forms.TextBox();
            this.EntryTypeBMF = new System.Windows.Forms.TextBox();
            this.cmdReqBMF = new System.Windows.Forms.ComboBox();
            this.cbMonitor = new System.Windows.Forms.GroupBox();
            this.startOSM = new System.Windows.Forms.Button();
            this.startMDBMF = new System.Windows.Forms.Button();
            this.startNestProject = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BOVRequestBT = new System.Windows.Forms.Button();
            this.symbolBOV = new System.Windows.Forms.TextBox();
            this.EntryTypeBOV = new System.Windows.Forms.TextBox();
            this.cmdReqBOV = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cbMonitor.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCommand);
            this.groupBox1.Controls.Add(this.btSendCommand);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Command";
            // 
            // cbCommand
            // 
            this.cbCommand.FormattingEnabled = true;
            this.cbCommand.Items.AddRange(new object[] {
            "START",
            "SEND ORDERS"});
            this.cbCommand.Location = new System.Drawing.Point(7, 19);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.Size = new System.Drawing.Size(120, 21);
            this.cbCommand.TabIndex = 1;
            // 
            // btSendCommand
            // 
            this.btSendCommand.Location = new System.Drawing.Point(16, 46);
            this.btSendCommand.Name = "btSendCommand";
            this.btSendCommand.Size = new System.Drawing.Size(94, 23);
            this.btSendCommand.TabIndex = 0;
            this.btSendCommand.Text = "Send Command";
            this.btSendCommand.UseVisualStyleBackColor = true;
            this.btSendCommand.Click += new System.EventHandler(this.btSendCommand_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BMFReqBt);
            this.groupBox2.Controls.Add(this.symbolBMF);
            this.groupBox2.Controls.Add(this.EntryTypeBMF);
            this.groupBox2.Controls.Add(this.cmdReqBMF);
            this.groupBox2.Location = new System.Drawing.Point(144, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 141);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MD Request BMF";
            // 
            // BMFReqBt
            // 
            this.BMFReqBt.Location = new System.Drawing.Point(64, 106);
            this.BMFReqBt.Name = "BMFReqBt";
            this.BMFReqBt.Size = new System.Drawing.Size(94, 23);
            this.BMFReqBt.TabIndex = 4;
            this.BMFReqBt.Text = "Send Request";
            this.BMFReqBt.UseVisualStyleBackColor = true;
            this.BMFReqBt.Click += new System.EventHandler(this.BMFReqBt_Click);
            // 
            // symbolBMF
            // 
            this.symbolBMF.Location = new System.Drawing.Point(49, 80);
            this.symbolBMF.Name = "symbolBMF";
            this.symbolBMF.Size = new System.Drawing.Size(121, 20);
            this.symbolBMF.TabIndex = 2;
            // 
            // EntryTypeBMF
            // 
            this.EntryTypeBMF.Location = new System.Drawing.Point(49, 53);
            this.EntryTypeBMF.Name = "EntryTypeBMF";
            this.EntryTypeBMF.Size = new System.Drawing.Size(121, 20);
            this.EntryTypeBMF.TabIndex = 1;
            // 
            // cmdReqBMF
            // 
            this.cmdReqBMF.FormattingEnabled = true;
            this.cmdReqBMF.Items.AddRange(new object[] {
            "SUBSCRIBE",
            "UNSUBSCRIBE",
            "UNSUBSCRIBE ALL"});
            this.cmdReqBMF.Location = new System.Drawing.Point(49, 26);
            this.cmdReqBMF.Name = "cmdReqBMF";
            this.cmdReqBMF.Size = new System.Drawing.Size(121, 21);
            this.cmdReqBMF.TabIndex = 0;
            // 
            // cbMonitor
            // 
            this.cbMonitor.Controls.Add(this.startOSM);
            this.cbMonitor.Controls.Add(this.startMDBMF);
            this.cbMonitor.Controls.Add(this.startNestProject);
            this.cbMonitor.Location = new System.Drawing.Point(2, 159);
            this.cbMonitor.Name = "cbMonitor";
            this.cbMonitor.Size = new System.Drawing.Size(514, 145);
            this.cbMonitor.TabIndex = 2;
            this.cbMonitor.TabStop = false;
            this.cbMonitor.Text = "Monitor";
            // 
            // startOSM
            // 
            this.startOSM.Location = new System.Drawing.Point(6, 99);
            this.startOSM.Name = "startOSM";
            this.startOSM.Size = new System.Drawing.Size(138, 34);
            this.startOSM.TabIndex = 2;
            this.startOSM.Text = "Start OSM";
            this.startOSM.UseVisualStyleBackColor = true;
            this.startOSM.Click += new System.EventHandler(this.startOSM_Click);
            // 
            // startMDBMF
            // 
            this.startMDBMF.Location = new System.Drawing.Point(7, 59);
            this.startMDBMF.Name = "startMDBMF";
            this.startMDBMF.Size = new System.Drawing.Size(138, 34);
            this.startMDBMF.TabIndex = 1;
            this.startMDBMF.Text = "Start MD BMF";
            this.startMDBMF.UseVisualStyleBackColor = true;
            this.startMDBMF.Click += new System.EventHandler(this.startMDBMF_Click);
            // 
            // startNestProject
            // 
            this.startNestProject.Location = new System.Drawing.Point(7, 19);
            this.startNestProject.Name = "startNestProject";
            this.startNestProject.Size = new System.Drawing.Size(138, 34);
            this.startNestProject.TabIndex = 0;
            this.startNestProject.Text = "Start NestProject";
            this.startNestProject.UseVisualStyleBackColor = true;
            this.startNestProject.Click += new System.EventHandler(this.startNestProject_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.BOVRequestBT);
            this.groupBox3.Controls.Add(this.symbolBOV);
            this.groupBox3.Controls.Add(this.EntryTypeBOV);
            this.groupBox3.Controls.Add(this.cmdReqBOV);
            this.groupBox3.Location = new System.Drawing.Point(333, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(183, 141);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MD Request Bovespa";
            // 
            // BOVRequestBT
            // 
            this.BOVRequestBT.Location = new System.Drawing.Point(67, 106);
            this.BOVRequestBT.Name = "BOVRequestBT";
            this.BOVRequestBT.Size = new System.Drawing.Size(94, 23);
            this.BOVRequestBT.TabIndex = 4;
            this.BOVRequestBT.Text = "Send Request";
            this.BOVRequestBT.UseVisualStyleBackColor = true;
            // 
            // symbolBOV
            // 
            this.symbolBOV.Location = new System.Drawing.Point(56, 79);
            this.symbolBOV.Name = "symbolBOV";
            this.symbolBOV.Size = new System.Drawing.Size(121, 20);
            this.symbolBOV.TabIndex = 2;
            // 
            // EntryTypeBOV
            // 
            this.EntryTypeBOV.Location = new System.Drawing.Point(56, 54);
            this.EntryTypeBOV.Name = "EntryTypeBOV";
            this.EntryTypeBOV.Size = new System.Drawing.Size(121, 20);
            this.EntryTypeBOV.TabIndex = 1;
            // 
            // cmdReqBOV
            // 
            this.cmdReqBOV.FormattingEnabled = true;
            this.cmdReqBOV.Items.AddRange(new object[] {
            "SUBSCRIBE",
            "UNSUBSCRIBE",
            "UNSUBSCRIBE ALL"});
            this.cmdReqBOV.Location = new System.Drawing.Point(56, 27);
            this.cmdReqBOV.Name = "cmdReqBOV";
            this.cmdReqBOV.Size = new System.Drawing.Size(121, 21);
            this.cmdReqBOV.TabIndex = 0;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 308);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cbMonitor);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Controller";
            this.Text = "Controller";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.cbMonitor.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.Button btSendCommand;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox symbolBMF;
        private System.Windows.Forms.TextBox EntryTypeBMF;
        private System.Windows.Forms.ComboBox cmdReqBMF;
        private System.Windows.Forms.Button BMFReqBt;
        private System.Windows.Forms.GroupBox cbMonitor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BOVRequestBT;
        private System.Windows.Forms.TextBox symbolBOV;
        private System.Windows.Forms.TextBox EntryTypeBOV;
        private System.Windows.Forms.ComboBox cmdReqBOV;
        private System.Windows.Forms.Button startOSM;
        private System.Windows.Forms.Button startMDBMF;
        private System.Windows.Forms.Button startNestProject;
        
    }
}