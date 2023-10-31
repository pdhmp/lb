namespace LiveBook
{
    partial class frmStrategy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStrategy));
            this.btnInsertStrategy = new System.Windows.Forms.Button();
            this.ListStrategy = new System.Windows.Forms.ListBox();
            this.txtStrategy = new System.Windows.Forms.TextBox();
            this.txtSubStrategy = new System.Windows.Forms.TextBox();
            this.ListSubStrategy = new System.Windows.Forms.ListBox();
            this.btnInsertSubStrategy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSubPorts = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ListSubPorts = new System.Windows.Forms.ListBox();
            this.txtSubPort = new System.Windows.Forms.TextBox();
            this.btnInsertSubPort = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsertStrategy
            // 
            this.btnInsertStrategy.Location = new System.Drawing.Point(112, 275);
            this.btnInsertStrategy.Name = "btnInsertStrategy";
            this.btnInsertStrategy.Size = new System.Drawing.Size(42, 23);
            this.btnInsertStrategy.TabIndex = 8;
            this.btnInsertStrategy.Text = "Insert";
            this.btnInsertStrategy.UseVisualStyleBackColor = true;
            this.btnInsertStrategy.Click += new System.EventHandler(this.btnInsertStrategy_Click);
            // 
            // ListStrategy
            // 
            this.ListStrategy.BackColor = System.Drawing.Color.White;
            this.ListStrategy.FormattingEnabled = true;
            this.ListStrategy.Location = new System.Drawing.Point(6, 19);
            this.ListStrategy.MaximumSize = new System.Drawing.Size(148, 225);
            this.ListStrategy.MinimumSize = new System.Drawing.Size(148, 225);
            this.ListStrategy.Name = "ListStrategy";
            this.ListStrategy.Size = new System.Drawing.Size(148, 225);
            this.ListStrategy.TabIndex = 7;
            this.ListStrategy.SelectedIndexChanged += new System.EventHandler(this.ListStrategy_SelectedIndexChanged);
            // 
            // txtStrategy
            // 
            this.txtStrategy.Location = new System.Drawing.Point(6, 277);
            this.txtStrategy.Name = "txtStrategy";
            this.txtStrategy.Size = new System.Drawing.Size(100, 20);
            this.txtStrategy.TabIndex = 6;
            this.txtStrategy.Text = "New Strategy";
            this.txtStrategy.Click += new System.EventHandler(this.txtStrategy_Click);
            this.txtStrategy.Leave += new System.EventHandler(this.txtStrategy_Leave);
            // 
            // txtSubStrategy
            // 
            this.txtSubStrategy.Location = new System.Drawing.Point(6, 277);
            this.txtSubStrategy.Name = "txtSubStrategy";
            this.txtSubStrategy.Size = new System.Drawing.Size(100, 20);
            this.txtSubStrategy.TabIndex = 1;
            this.txtSubStrategy.Text = "New SubStrategy";
            this.txtSubStrategy.Click += new System.EventHandler(this.txtSubStrategy_Click);
            this.txtSubStrategy.Leave += new System.EventHandler(this.txtSubStrategy_Leave);
            // 
            // ListSubStrategy
            // 
            this.ListSubStrategy.FormattingEnabled = true;
            this.ListSubStrategy.Location = new System.Drawing.Point(6, 19);
            this.ListSubStrategy.Name = "ListSubStrategy";
            this.ListSubStrategy.Size = new System.Drawing.Size(148, 225);
            this.ListSubStrategy.TabIndex = 2;
            // 
            // btnInsertSubStrategy
            // 
            this.btnInsertSubStrategy.Location = new System.Drawing.Point(112, 275);
            this.btnInsertSubStrategy.Name = "btnInsertSubStrategy";
            this.btnInsertSubStrategy.Size = new System.Drawing.Size(42, 23);
            this.btnInsertSubStrategy.TabIndex = 3;
            this.btnInsertSubStrategy.Text = "Insert";
            this.btnInsertSubStrategy.UseVisualStyleBackColor = true;
            this.btnInsertSubStrategy.Click += new System.EventHandler(this.btnInsertSubStrategy_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSubPorts);
            this.groupBox1.Controls.Add(this.ListStrategy);
            this.groupBox1.Controls.Add(this.txtStrategy);
            this.groupBox1.Controls.Add(this.btnInsertStrategy);
            this.groupBox1.Location = new System.Drawing.Point(180, 12);
            this.groupBox1.MaximumSize = new System.Drawing.Size(160, 305);
            this.groupBox1.MinimumSize = new System.Drawing.Size(160, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 305);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stratagies";
            // 
            // cmbSubPorts
            // 
            this.cmbSubPorts.FormattingEnabled = true;
            this.cmbSubPorts.Location = new System.Drawing.Point(6, 250);
            this.cmbSubPorts.Name = "cmbSubPorts";
            this.cmbSubPorts.Size = new System.Drawing.Size(148, 21);
            this.cmbSubPorts.TabIndex = 9;
            this.cmbSubPorts.Text = "cmbSubPorts";
            this.cmbSubPorts.SelectedIndexChanged += new System.EventHandler(this.cmbSubPorts_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbStrategy);
            this.groupBox2.Controls.Add(this.ListSubStrategy);
            this.groupBox2.Controls.Add(this.txtSubStrategy);
            this.groupBox2.Controls.Add(this.btnInsertSubStrategy);
            this.groupBox2.Location = new System.Drawing.Point(348, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 305);
            this.groupBox2.TabIndex = 67;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sub-Strategies";
            // 
            // cmbStrategy
            // 
            this.cmbStrategy.FormattingEnabled = true;
            this.cmbStrategy.Location = new System.Drawing.Point(6, 250);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(148, 21);
            this.cmbStrategy.TabIndex = 66;
            this.cmbStrategy.Text = "cmbStrategy";
            this.cmbStrategy.SelectedIndexChanged += new System.EventHandler(this.cmbStrategy_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ListSubPorts);
            this.groupBox3.Controls.Add(this.txtSubPort);
            this.groupBox3.Controls.Add(this.btnInsertSubPort);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.MaximumSize = new System.Drawing.Size(160, 305);
            this.groupBox3.MinimumSize = new System.Drawing.Size(160, 305);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(160, 305);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sub-Portfolios";
            // 
            // ListSubPorts
            // 
            this.ListSubPorts.BackColor = System.Drawing.Color.White;
            this.ListSubPorts.FormattingEnabled = true;
            this.ListSubPorts.Location = new System.Drawing.Point(6, 19);
            this.ListSubPorts.Name = "ListSubPorts";
            this.ListSubPorts.Size = new System.Drawing.Size(148, 251);
            this.ListSubPorts.TabIndex = 7;
            this.ListSubPorts.SelectedIndexChanged += new System.EventHandler(this.ListSubPorts_SelectedIndexChanged);
            // 
            // txtSubPort
            // 
            this.txtSubPort.Location = new System.Drawing.Point(6, 277);
            this.txtSubPort.Name = "txtSubPort";
            this.txtSubPort.Size = new System.Drawing.Size(100, 20);
            this.txtSubPort.TabIndex = 6;
            this.txtSubPort.Text = "New SubPortfolio";
            this.txtSubPort.Click += new System.EventHandler(this.txtSubPort_Click);
            this.txtSubPort.Leave += new System.EventHandler(this.txtSubPort_Leave);
            // 
            // btnInsertSubPort
            // 
            this.btnInsertSubPort.Location = new System.Drawing.Point(112, 275);
            this.btnInsertSubPort.Name = "btnInsertSubPort";
            this.btnInsertSubPort.Size = new System.Drawing.Size(42, 23);
            this.btnInsertSubPort.TabIndex = 8;
            this.btnInsertSubPort.Text = "Insert";
            this.btnInsertSubPort.UseVisualStyleBackColor = true;
            this.btnInsertSubPort.Click += new System.EventHandler(this.btnInsertSubPort_Click);
            // 
            // frmStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(520, 328);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmStrategy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Strategies";
            this.Load += new System.EventHandler(this.frmStrategy_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsertStrategy;
        private System.Windows.Forms.ListBox ListStrategy;
        private System.Windows.Forms.TextBox txtStrategy;
        private System.Windows.Forms.TextBox txtSubStrategy;
        private System.Windows.Forms.ListBox ListSubStrategy;
        private System.Windows.Forms.Button btnInsertSubStrategy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox ListSubPorts;
        private System.Windows.Forms.TextBox txtSubPort;
        private System.Windows.Forms.Button btnInsertSubPort;
        private System.Windows.Forms.ComboBox cmbStrategy;
        private System.Windows.Forms.ComboBox cmbSubPorts;
    }
}