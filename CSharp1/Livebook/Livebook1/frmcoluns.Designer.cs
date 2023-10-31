namespace SGN
{
    partial class frmcoluns
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmcoluns));
            this.lst1 = new System.Windows.Forms.ListBox();
            this.lst2 = new System.Windows.Forms.ListBox();
            this.lstadd = new System.Windows.Forms.Button();
            this.lstRemove = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdgrid = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lst1
            // 
            this.lst1.FormattingEnabled = true;
            this.lst1.Location = new System.Drawing.Point(13, 101);
            this.lst1.Name = "lst1";
            this.lst1.Size = new System.Drawing.Size(120, 264);
            this.lst1.TabIndex = 0;
            // 
            // lst2
            // 
            this.lst2.FormattingEnabled = true;
            this.lst2.Location = new System.Drawing.Point(201, 101);
            this.lst2.Name = "lst2";
            this.lst2.Size = new System.Drawing.Size(120, 264);
            this.lst2.TabIndex = 1;
            // 
            // lstadd
            // 
            this.lstadd.Location = new System.Drawing.Point(139, 180);
            this.lstadd.Name = "lstadd";
            this.lstadd.Size = new System.Drawing.Size(56, 23);
            this.lstadd.TabIndex = 2;
            this.lstadd.Text = "Add";
            this.lstadd.UseVisualStyleBackColor = true;
            this.lstadd.Click += new System.EventHandler(this.lstadd_Click);
            // 
            // lstRemove
            // 
            this.lstRemove.Location = new System.Drawing.Point(139, 266);
            this.lstRemove.Name = "lstRemove";
            this.lstRemove.Size = new System.Drawing.Size(56, 23);
            this.lstRemove.TabIndex = 3;
            this.lstRemove.Text = "Remove";
            this.lstRemove.UseVisualStyleBackColor = true;
            this.lstRemove.Click += new System.EventHandler(this.lstRemove_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(228, 384);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 6;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(42, 384);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 7;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdOk);
            this.groupBox1.Controls.Add(this.cmdgrid);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 75);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the Grid";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(82, 44);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(45, 23);
            this.cmdOk.TabIndex = 10;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdgrid
            // 
            this.cmdgrid.FormattingEnabled = true;
            this.cmdgrid.Location = new System.Drawing.Point(6, 19);
            this.cmdgrid.Name = "cmdgrid";
            this.cmdgrid.Size = new System.Drawing.Size(121, 21);
            this.cmdgrid.TabIndex = 9;
            // 
            // frmcoluns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 419);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.lstRemove);
            this.Controls.Add(this.lstadd);
            this.Controls.Add(this.lst2);
            this.Controls.Add(this.lst1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmcoluns";
            this.Text = "frmcoluns";
            this.Load += new System.EventHandler(this.frmcoluns_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lst1;
        private System.Windows.Forms.ListBox lst2;
        private System.Windows.Forms.Button lstadd;
        private System.Windows.Forms.Button lstRemove;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmdgrid;
        private System.Windows.Forms.Button cmdOk;
    }
}