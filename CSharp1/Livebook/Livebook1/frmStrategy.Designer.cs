namespace SGN
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.lstSubStrategy = new System.Windows.Forms.ListBox();
            this.txtSubStrategy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstStrategy = new System.Windows.Forms.ListBox();
            this.txtStrategy = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.lstSubStrategy);
            this.panel2.Controls.Add(this.txtSubStrategy);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(166, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 301);
            this.panel2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Strategy";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(109, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(42, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Insert";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lstSubStrategy
            // 
            this.lstSubStrategy.FormattingEnabled = true;
            this.lstSubStrategy.Location = new System.Drawing.Point(3, 27);
            this.lstSubStrategy.Name = "lstSubStrategy";
            this.lstSubStrategy.Size = new System.Drawing.Size(151, 225);
            this.lstSubStrategy.TabIndex = 2;
            // 
            // txtSubStrategy
            // 
            this.txtSubStrategy.Location = new System.Drawing.Point(3, 269);
            this.txtSubStrategy.Name = "txtSubStrategy";
            this.txtSubStrategy.Size = new System.Drawing.Size(100, 20);
            this.txtSubStrategy.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "New Sub Strategy";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(261, 319);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(118, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Insert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstStrategy
            // 
            this.lstStrategy.FormattingEnabled = true;
            this.lstStrategy.Location = new System.Drawing.Point(12, 38);
            this.lstStrategy.Name = "lstStrategy";
            this.lstStrategy.Size = new System.Drawing.Size(148, 225);
            this.lstStrategy.TabIndex = 7;
            this.lstStrategy.SelectedValueChanged += new System.EventHandler(this.lstStrategy_SelectedValueChanged);
            // 
            // txtStrategy
            // 
            this.txtStrategy.Location = new System.Drawing.Point(12, 282);
            this.txtStrategy.Name = "txtStrategy";
            this.txtStrategy.Size = new System.Drawing.Size(100, 20);
            this.txtStrategy.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "New Strategy";
            // 
            // frmStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 358);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lstStrategy);
            this.Controls.Add(this.txtStrategy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel2);
            this.Name = "frmStrategy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStrategy";
            this.Load += new System.EventHandler(this.frmStrategy_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lstSubStrategy;
        private System.Windows.Forms.TextBox txtSubStrategy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstStrategy;
        private System.Windows.Forms.TextBox txtStrategy;
        private System.Windows.Forms.Label label1;
    }
}