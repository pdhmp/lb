namespace VWAPS
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
            this.ok = new System.Windows.Forms.Button();
            this.cmbIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCalcTRDay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(121, 158);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 2;
            this.ok.Text = "Ok";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // cmbIniDate
            // 
            this.cmbIniDate.Location = new System.Drawing.Point(103, 5);
            this.cmbIniDate.Name = "cmbIniDate";
            this.cmbIniDate.Size = new System.Drawing.Size(120, 20);
            this.cmbIniDate.TabIndex = 3;
            this.cmbIniDate.Value = new System.DateTime(2012, 6, 8, 0, 0, 0, 0);
            this.cmbIniDate.ValueChanged += new System.EventHandler(this.CalcDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ini Date";
            // 
            // cmbEndDate
            // 
            this.cmbEndDate.Location = new System.Drawing.Point(103, 31);
            this.cmbEndDate.Name = "cmbEndDate";
            this.cmbEndDate.Size = new System.Drawing.Size(120, 20);
            this.cmbEndDate.TabIndex = 10;
            this.cmbEndDate.Value = new System.DateTime(2012, 6, 8, 0, 0, 0, 0);
            this.cmbEndDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "End Date";
            // 
            // cbCalcTRDay
            // 
            this.cbCalcTRDay.AutoSize = true;
            this.cbCalcTRDay.Location = new System.Drawing.Point(121, 57);
            this.cbCalcTRDay.Name = "cbCalcTRDay";
            this.cbCalcTRDay.Size = new System.Drawing.Size(87, 17);
            this.cbCalcTRDay.TabIndex = 12;
            this.cbCalcTRDay.Text = "Calc TR Day";
            this.cbCalcTRDay.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 193);
            this.Controls.Add(this.cbCalcTRDay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbEndDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbIniDate);
            this.Controls.Add(this.ok);
            this.Name = "Form1";
            this.Text = "QSEG Calculator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker cmbEndDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbCalcTRDay;
        public System.Windows.Forms.DateTimePicker cmbIniDate;

    }
}

