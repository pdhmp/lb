namespace LiveBook
{
    public partial class frmSection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSection));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSubStrategy = new System.Windows.Forms.ComboBox();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.cmbMirrorSection = new System.Windows.Forms.ComboBox();
            this.btnInsertSection = new System.Windows.Forms.Button();
            this.ListSection = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSection = new System.Windows.Forms.TextBox();
            this.chkMirror = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 83;
            this.label5.Text = "Sub Strategy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Strategy";
            // 
            // cmbSubStrategy
            // 
            this.cmbSubStrategy.BackColor = System.Drawing.Color.White;
            this.cmbSubStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubStrategy.FormattingEnabled = true;
            this.cmbSubStrategy.Location = new System.Drawing.Point(82, 42);
            this.cmbSubStrategy.Name = "cmbSubStrategy";
            this.cmbSubStrategy.Size = new System.Drawing.Size(214, 21);
            this.cmbSubStrategy.TabIndex = 81;
            this.cmbSubStrategy.SelectedIndexChanged += new System.EventHandler(this.cmbSubStrategy_SelectedIndexChanged_1);
            // 
            // cmbStrategy
            // 
            this.cmbStrategy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStrategy.BackColor = System.Drawing.Color.White;
            this.cmbStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStrategy.FormattingEnabled = true;
            this.cmbStrategy.Location = new System.Drawing.Point(63, 12);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(233, 21);
            this.cmbStrategy.TabIndex = 80;
            this.cmbStrategy.SelectedIndexChanged += new System.EventHandler(this.cmbStrategy_SelectedIndexChanged_1);
            // 
            // cmbMirrorSection
            // 
            this.cmbMirrorSection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMirrorSection.Enabled = false;
            this.cmbMirrorSection.FormattingEnabled = true;
            this.cmbMirrorSection.Location = new System.Drawing.Point(93, 71);
            this.cmbMirrorSection.Name = "cmbMirrorSection";
            this.cmbMirrorSection.Size = new System.Drawing.Size(203, 21);
            this.cmbMirrorSection.TabIndex = 75;
            this.cmbMirrorSection.SelectedIndexChanged += new System.EventHandler(this.cmbMirrorSection_SelectedIndexChanged);
            // 
            // btnInsertSection
            // 
            this.btnInsertSection.Location = new System.Drawing.Point(203, 98);
            this.btnInsertSection.Name = "btnInsertSection";
            this.btnInsertSection.Size = new System.Drawing.Size(93, 21);
            this.btnInsertSection.TabIndex = 73;
            this.btnInsertSection.Text = "Create Section";
            this.btnInsertSection.UseVisualStyleBackColor = true;
            this.btnInsertSection.Click += new System.EventHandler(this.btnInsertSection_Click_1);
            // 
            // ListSection
            // 
            this.ListSection.BackColor = System.Drawing.Color.White;
            this.ListSection.FormattingEnabled = true;
            this.ListSection.HorizontalScrollbar = true;
            this.ListSection.Location = new System.Drawing.Point(5, 20);
            this.ListSection.Name = "ListSection";
            this.ListSection.ScrollAlwaysVisible = true;
            this.ListSection.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ListSection.Size = new System.Drawing.Size(285, 173);
            this.ListSection.TabIndex = 67;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 71;
            this.label6.Text = "New Section";
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(82, 99);
            this.txtSection.Name = "txtSection";
            this.txtSection.Size = new System.Drawing.Size(115, 20);
            this.txtSection.TabIndex = 72;
            // 
            // chkMirror
            // 
            this.chkMirror.AutoSize = true;
            this.chkMirror.Location = new System.Drawing.Point(11, 73);
            this.chkMirror.Name = "chkMirror";
            this.chkMirror.Size = new System.Drawing.Size(76, 17);
            this.chkMirror.TabIndex = 76;
            this.chkMirror.Text = "Copy From";
            this.chkMirror.UseVisualStyleBackColor = true;
            this.chkMirror.CheckedChanged += new System.EventHandler(this.chkMirror_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ListSection);
            this.groupBox1.Location = new System.Drawing.Point(6, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(294, 202);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Existing Sections for this Sub Strategy:";
            // 
            // frmSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(303, 340);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnInsertSection);
            this.Controls.Add(this.chkMirror);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbMirrorSection);
            this.Controls.Add(this.txtSection);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbSubStrategy);
            this.Controls.Add(this.cmbStrategy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSection";
            this.Text = "Edit Sections";
            this.Load += new System.EventHandler(this.frmSection_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSubStrategy;
        private System.Windows.Forms.ComboBox cmbStrategy;
        private System.Windows.Forms.Button btnInsertSection;
        private System.Windows.Forms.ListBox ListSection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSection;
        private System.Windows.Forms.ComboBox cmbMirrorSection;
        private System.Windows.Forms.CheckBox chkMirror;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}