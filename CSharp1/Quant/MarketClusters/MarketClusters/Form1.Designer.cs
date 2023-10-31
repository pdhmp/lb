namespace MarketClusters
{
    partial class Form1
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
            this.cmdCorrel = new System.Windows.Forms.Button();
            this.picDrawArea = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDrawArea)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdCorrel
            // 
            this.cmdCorrel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCorrel.Location = new System.Drawing.Point(656, 483);
            this.cmdCorrel.Name = "cmdCorrel";
            this.cmdCorrel.Size = new System.Drawing.Size(136, 43);
            this.cmdCorrel.TabIndex = 1;
            this.cmdCorrel.Text = "GO";
            this.cmdCorrel.UseVisualStyleBackColor = true;
            this.cmdCorrel.Click += new System.EventHandler(this.cmdCorrel_Click);
            // 
            // picDrawArea
            // 
            this.picDrawArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picDrawArea.Location = new System.Drawing.Point(12, 12);
            this.picDrawArea.Name = "picDrawArea";
            this.picDrawArea.Size = new System.Drawing.Size(780, 465);
            this.picDrawArea.TabIndex = 2;
            this.picDrawArea.TabStop = false;
            this.picDrawArea.Resize += new System.EventHandler(this.picDrawArea_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(804, 538);
            this.Controls.Add(this.picDrawArea);
            this.Controls.Add(this.cmdCorrel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDrawArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCorrel;
        private System.Windows.Forms.PictureBox picDrawArea;
    }
}

