namespace Dashboards
{
    partial class CBO
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dvOrdersCBO5 = new System.Windows.Forms.DataGridView();
            this.dvOrdersCBO11 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvOrdersCBO5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvOrdersCBO11)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(718, 508);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dvOrdersCBO5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(710, 482);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CBO 5";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dvOrdersCBO11);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(710, 482);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CBO 11";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dvOrdersCBO5
            // 
            this.dvOrdersCBO5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvOrdersCBO5.Dock = System.Windows.Forms.DockStyle.Left;
            this.dvOrdersCBO5.Location = new System.Drawing.Point(3, 3);
            this.dvOrdersCBO5.Name = "dvOrdersCBO5";
            this.dvOrdersCBO5.Size = new System.Drawing.Size(412, 476);
            this.dvOrdersCBO5.TabIndex = 0;
            // 
            // dvOrdersCBO11
            // 
            this.dvOrdersCBO11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvOrdersCBO11.Dock = System.Windows.Forms.DockStyle.Left;
            this.dvOrdersCBO11.Location = new System.Drawing.Point(3, 3);
            this.dvOrdersCBO11.Name = "dvOrdersCBO11";
            this.dvOrdersCBO11.Size = new System.Drawing.Size(412, 476);
            this.dvOrdersCBO11.TabIndex = 1;
            // 
            // CBO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 532);
            this.Controls.Add(this.tabControl1);
            this.Name = "CBO";
            this.Text = "CBO";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvOrdersCBO5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvOrdersCBO11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dvOrdersCBO5;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dvOrdersCBO11;
    }
}