namespace LBHistCalc
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
            this.cmdRecalc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdRecalc
            // 
            this.cmdRecalc.Location = new System.Drawing.Point(103, 47);
            this.cmdRecalc.Name = "cmdRecalc";
            this.cmdRecalc.Size = new System.Drawing.Size(75, 23);
            this.cmdRecalc.TabIndex = 0;
            this.cmdRecalc.Text = "Recalc";
            this.cmdRecalc.UseVisualStyleBackColor = true;
            this.cmdRecalc.Click += new System.EventHandler(this.cmdRecalc_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.cmdRecalc);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdRecalc;
    }
}

