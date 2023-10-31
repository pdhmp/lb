namespace FeedXML2FIX
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
            this.lblSafra = new System.Windows.Forms.Label();
            this.txtSafra = new System.Windows.Forms.Label();
            this.btnSafra = new System.Windows.Forms.Button();
            this.lblFutura = new System.Windows.Forms.Label();
            this.txtFutura = new System.Windows.Forms.Label();
            this.btnFutura = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSafra
            // 
            this.lblSafra.AutoSize = true;
            this.lblSafra.Location = new System.Drawing.Point(16, 21);
            this.lblSafra.Name = "lblSafra";
            this.lblSafra.Size = new System.Drawing.Size(32, 13);
            this.lblSafra.TabIndex = 0;
            this.lblSafra.Text = "Safra";
            // 
            // txtSafra
            // 
            this.txtSafra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtSafra.Location = new System.Drawing.Point(59, 16);
            this.txtSafra.Name = "txtSafra";
            this.txtSafra.Size = new System.Drawing.Size(142, 22);
            this.txtSafra.TabIndex = 0;
            this.txtSafra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSafra
            // 
            this.btnSafra.Location = new System.Drawing.Point(207, 16);
            this.btnSafra.Name = "btnSafra";
            this.btnSafra.Size = new System.Drawing.Size(75, 22);
            this.btnSafra.TabIndex = 1;
            this.btnSafra.Text = "Force Insert";
            this.btnSafra.UseVisualStyleBackColor = true;
            this.btnSafra.Click += new System.EventHandler(this.btnSafra_Click);
            // 
            // lblFutura
            // 
            this.lblFutura.AutoSize = true;
            this.lblFutura.Location = new System.Drawing.Point(16, 53);
            this.lblFutura.Name = "lblFutura";
            this.lblFutura.Size = new System.Drawing.Size(37, 13);
            this.lblFutura.TabIndex = 0;
            this.lblFutura.Text = "Futura";
            // 
            // txtFutura
            // 
            this.txtFutura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtFutura.Location = new System.Drawing.Point(59, 48);
            this.txtFutura.Name = "txtFutura";
            this.txtFutura.Size = new System.Drawing.Size(142, 22);
            this.txtFutura.TabIndex = 0;
            this.txtFutura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFutura
            // 
            this.btnFutura.Location = new System.Drawing.Point(207, 48);
            this.btnFutura.Name = "btnFutura";
            this.btnFutura.Size = new System.Drawing.Size(75, 22);
            this.btnFutura.TabIndex = 1;
            this.btnFutura.Text = "Force Insert";
            this.btnFutura.UseVisualStyleBackColor = true;
            this.btnFutura.Click += new System.EventHandler(this.btnFutura_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 84);
            this.Controls.Add(this.btnFutura);
            this.Controls.Add(this.btnSafra);
            this.Controls.Add(this.txtFutura);
            this.Controls.Add(this.lblFutura);
            this.Controls.Add(this.txtSafra);
            this.Controls.Add(this.lblSafra);
            this.Name = "frmMain";
            this.Text = "XML2FIX";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSafra;
        private System.Windows.Forms.Label txtSafra;
        private System.Windows.Forms.Button btnSafra;
        private System.Windows.Forms.Label lblFutura;
        private System.Windows.Forms.Label txtFutura;
        private System.Windows.Forms.Button btnFutura;
    }
}