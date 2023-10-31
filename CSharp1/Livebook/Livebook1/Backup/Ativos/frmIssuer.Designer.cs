namespace SGN
{
    partial class frmIssuer
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
            this.cmbGeography = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbIndustry = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbGics = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdInsert = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbGeography);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmbGeography
            // 
            this.cmbGeography.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGeography.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGeography.FormattingEnabled = true;
            this.cmbGeography.Location = new System.Drawing.Point(78, 48);
            this.cmbGeography.Name = "cmbGeography";
            this.cmbGeography.Size = new System.Drawing.Size(130, 21);
            this.cmbGeography.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 67;
            this.label10.Text = "Geography";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(48, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(322, 20);
            this.txtName.TabIndex = 0;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 18);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 13);
            this.label23.TabIndex = 66;
            this.label23.Text = "Name";
            // 
            // cmbIndustry
            // 
            this.cmbIndustry.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbIndustry.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbIndustry.FormattingEnabled = true;
            this.cmbIndustry.Location = new System.Drawing.Point(58, 16);
            this.cmbIndustry.Name = "cmbIndustry";
            this.cmbIndustry.Size = new System.Drawing.Size(141, 21);
            this.cmbIndustry.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Industry";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbGics);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbSector);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cmbIndustry);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(383, 79);
            this.groupBox2.TabIndex = 73;
            this.groupBox2.TabStop = false;
            // 
            // cmbGics
            // 
            this.cmbGics.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGics.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGics.FormattingEnabled = true;
            this.cmbGics.Location = new System.Drawing.Point(104, 48);
            this.cmbGics.Name = "cmbGics";
            this.cmbGics.Size = new System.Drawing.Size(266, 21);
            this.cmbGics.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 75;
            this.label3.Text = "Gics Sub Industry";
            // 
            // cmbSector
            // 
            this.cmbSector.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSector.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSector.FormattingEnabled = true;
            this.cmbSector.Location = new System.Drawing.Point(249, 16);
            this.cmbSector.Name = "cmbSector";
            this.cmbSector.Size = new System.Drawing.Size(121, 21);
            this.cmbSector.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Sector";
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Silver;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Location = new System.Drawing.Point(307, 179);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 7;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdClear
            // 
            this.cmdClear.BackColor = System.Drawing.Color.Silver;
            this.cmdClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClear.Location = new System.Drawing.Point(12, 179);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 6;
            this.cmdClear.Text = "Clear Fields";
            this.cmdClear.UseVisualStyleBackColor = false;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdInsert
            // 
            this.cmdInsert.BackColor = System.Drawing.Color.Silver;
            this.cmdInsert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdInsert.Location = new System.Drawing.Point(188, 179);
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(75, 23);
            this.cmdInsert.TabIndex = 5;
            this.cmdInsert.Text = "Insert";
            this.cmdInsert.UseVisualStyleBackColor = false;
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // frmIssuer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 213);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.cmdInsert);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmIssuer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Issuer";
            this.Load += new System.EventHandler(this.frmIssuer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbGeography;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbIndustry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbGics;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.Button cmdInsert;
    }
}