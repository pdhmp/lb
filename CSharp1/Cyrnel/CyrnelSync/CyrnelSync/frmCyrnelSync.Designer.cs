namespace CyrnelSync
{
    partial class frmCyrnelSync
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCyrnelSync));
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.labLastDBUpdate = new System.Windows.Forms.Label();
            this.btnUpdateDB = new System.Windows.Forms.Button();
            this.btnUpdateFile = new System.Windows.Forms.Button();
            this.labLastFileUpdate = new System.Windows.Forms.Label();
            this.txtOnFile = new System.Windows.Forms.Label();
            this.txtOnDB = new System.Windows.Forms.Label();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrMain
            // 
            this.tmrMain.Interval = 500;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // labLastDBUpdate
            // 
            this.labLastDBUpdate.AutoSize = true;
            this.labLastDBUpdate.Location = new System.Drawing.Point(124, 17);
            this.labLastDBUpdate.Name = "labLastDBUpdate";
            this.labLastDBUpdate.Size = new System.Drawing.Size(113, 13);
            this.labLastDBUpdate.TabIndex = 0;
            this.labLastDBUpdate.Text = "Last Update: 00:00:00";
            // 
            // btnUpdateDB
            // 
            this.btnUpdateDB.Location = new System.Drawing.Point(12, 12);
            this.btnUpdateDB.Name = "btnUpdateDB";
            this.btnUpdateDB.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateDB.TabIndex = 1;
            this.btnUpdateDB.Text = "Update DB";
            this.btnUpdateDB.UseVisualStyleBackColor = true;
            this.btnUpdateDB.Click += new System.EventHandler(this.btnUpdateDB_Click);
            // 
            // btnUpdateFile
            // 
            this.btnUpdateFile.Location = new System.Drawing.Point(12, 55);
            this.btnUpdateFile.Name = "btnUpdateFile";
            this.btnUpdateFile.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateFile.TabIndex = 2;
            this.btnUpdateFile.Text = "Update File";
            this.btnUpdateFile.UseVisualStyleBackColor = true;
            this.btnUpdateFile.Click += new System.EventHandler(this.btnUpdateFile_Click);
            // 
            // labLastFileUpdate
            // 
            this.labLastFileUpdate.AutoSize = true;
            this.labLastFileUpdate.Location = new System.Drawing.Point(124, 60);
            this.labLastFileUpdate.Name = "labLastFileUpdate";
            this.labLastFileUpdate.Size = new System.Drawing.Size(113, 13);
            this.labLastFileUpdate.TabIndex = 3;
            this.labLastFileUpdate.Text = "Last Update: 00:00:00";
            // 
            // txtOnFile
            // 
            this.txtOnFile.AutoSize = true;
            this.txtOnFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnFile.ForeColor = System.Drawing.Color.Red;
            this.txtOnFile.Location = new System.Drawing.Point(93, 60);
            this.txtOnFile.Name = "txtOnFile";
            this.txtOnFile.Size = new System.Drawing.Size(30, 13);
            this.txtOnFile.TabIndex = 4;
            this.txtOnFile.Text = "OFF";
            this.txtOnFile.Click += new System.EventHandler(this.txtOnFile_Click);
            // 
            // txtOnDB
            // 
            this.txtOnDB.AutoSize = true;
            this.txtOnDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnDB.ForeColor = System.Drawing.Color.Red;
            this.txtOnDB.Location = new System.Drawing.Point(93, 17);
            this.txtOnDB.Name = "txtOnDB";
            this.txtOnDB.Size = new System.Drawing.Size(30, 13);
            this.txtOnDB.TabIndex = 5;
            this.txtOnDB.Text = "OFF";
            this.txtOnDB.Click += new System.EventHandler(this.txtOnDB_Click);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 800;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // frmCyrnelSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(252, 90);
            this.Controls.Add(this.txtOnDB);
            this.Controls.Add(this.txtOnFile);
            this.Controls.Add(this.labLastFileUpdate);
            this.Controls.Add(this.btnUpdateFile);
            this.Controls.Add(this.btnUpdateDB);
            this.Controls.Add(this.labLastDBUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(260, 117);
            this.MinimumSize = new System.Drawing.Size(260, 117);
            this.Name = "frmCyrnelSync";
            this.Text = "Cyrnel Sync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrMain;
        private System.Windows.Forms.Label labLastDBUpdate;
        private System.Windows.Forms.Button btnUpdateDB;
        private System.Windows.Forms.Button btnUpdateFile;
        private System.Windows.Forms.Label labLastFileUpdate;
        private System.Windows.Forms.Label txtOnFile;
        private System.Windows.Forms.Label txtOnDB;
        private System.Windows.Forms.Timer tmrRefresh;
    }
}

