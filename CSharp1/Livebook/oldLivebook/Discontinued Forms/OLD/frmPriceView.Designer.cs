namespace SGN
{
    partial class frmPriceView
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
            this.chkDiscontinued = new System.Windows.Forms.CheckBox();
            this.lstAtivos = new System.Windows.Forms.ListBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdInsertNew = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDiscontinued);
            this.groupBox1.Controls.Add(this.lstAtivos);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.txtsearch);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 408);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ticker";
            // 
            // chkDiscontinued
            // 
            this.chkDiscontinued.AutoSize = true;
            this.chkDiscontinued.Location = new System.Drawing.Point(7, 46);
            this.chkDiscontinued.Name = "chkDiscontinued";
            this.chkDiscontinued.Size = new System.Drawing.Size(126, 17);
            this.chkDiscontinued.TabIndex = 2;
            this.chkDiscontinued.Text = "Include Discontinued";
            this.chkDiscontinued.UseVisualStyleBackColor = true;
            // 
            // lstAtivos
            // 
            this.lstAtivos.FormattingEnabled = true;
            this.lstAtivos.Location = new System.Drawing.Point(6, 71);
            this.lstAtivos.Name = "lstAtivos";
            this.lstAtivos.Size = new System.Drawing.Size(175, 329);
            this.lstAtivos.TabIndex = 1;
            // 
            // cmdSearch
            // 
            this.cmdSearch.Image = global::SGN.Properties.Resources.search;
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(115, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(66, 23);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.UseVisualStyleBackColor = true;
            // 
            // txtsearch
            // 
            this.txtsearch.Location = new System.Drawing.Point(6, 19);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(103, 20);
            this.txtsearch.TabIndex = 0;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(616, 426);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(114, 33);
            this.cmdCancel.TabIndex = 27;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdInsertNew
            // 
            this.cmdInsertNew.Location = new System.Drawing.Point(18, 426);
            this.cmdInsertNew.Name = "cmdInsertNew";
            this.cmdInsertNew.Size = new System.Drawing.Size(175, 33);
            this.cmdInsertNew.TabIndex = 28;
            this.cmdInsertNew.Text = "Insert New Price";
            this.cmdInsertNew.UseVisualStyleBackColor = true;
            // 
            // frmPriceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(742, 470);
            this.Controls.Add(this.cmdInsertNew);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPriceView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instruments";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.ListBox lstAtivos;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdInsertNew;
        private System.Windows.Forms.CheckBox chkDiscontinued;
    }
}