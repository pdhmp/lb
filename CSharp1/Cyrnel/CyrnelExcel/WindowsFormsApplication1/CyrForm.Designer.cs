namespace CyrnelSync
{
    partial class CyrForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CyrForm));
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.bntUpdate = new System.Windows.Forms.Button();
            this.btnStopUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAutoUpdateON = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastUpdated = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 300000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // bntUpdate
            // 
            this.bntUpdate.Location = new System.Drawing.Point(12, 9);
            this.bntUpdate.Name = "bntUpdate";
            this.bntUpdate.Size = new System.Drawing.Size(75, 23);
            this.bntUpdate.TabIndex = 0;
            this.bntUpdate.Text = "Update";
            this.bntUpdate.UseVisualStyleBackColor = true;
            this.bntUpdate.Click += new System.EventHandler(this.bntUpdate_Click);
            // 
            // btnStopUpdate
            // 
            this.btnStopUpdate.Location = new System.Drawing.Point(12, 45);
            this.btnStopUpdate.Name = "btnStopUpdate";
            this.btnStopUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnStopUpdate.TabIndex = 1;
            this.btnStopUpdate.Text = "StopUpdate";
            this.btnStopUpdate.UseVisualStyleBackColor = true;
            this.btnStopUpdate.Click += new System.EventHandler(this.btnStopUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Auto Save: ";
            // 
            // txtAutoUpdateON
            // 
            this.txtAutoUpdateON.AutoSize = true;
            this.txtAutoUpdateON.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAutoUpdateON.ForeColor = System.Drawing.Color.Red;
            this.txtAutoUpdateON.Location = new System.Drawing.Point(153, 55);
            this.txtAutoUpdateON.Name = "txtAutoUpdateON";
            this.txtAutoUpdateON.Size = new System.Drawing.Size(30, 13);
            this.txtAutoUpdateON.TabIndex = 3;
            this.txtAutoUpdateON.Text = "OFF";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(94, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Last Updated: ";
            // 
            // txtLastUpdated
            // 
            this.txtLastUpdated.AutoSize = true;
            this.txtLastUpdated.Location = new System.Drawing.Point(166, 33);
            this.txtLastUpdated.Name = "txtLastUpdated";
            this.txtLastUpdated.Size = new System.Drawing.Size(49, 13);
            this.txtLastUpdated.TabIndex = 5;
            this.txtLastUpdated.Text = "00:00:00";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(97, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(86, 20);
            this.dateTimePicker1.TabIndex = 6;
            this.dateTimePicker1.Value = new System.DateTime(2013, 5, 16, 12, 45, 21, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 80);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtLastUpdated);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAutoUpdateON);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStopUpdate);
            this.Controls.Add(this.bntUpdate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(260, 118);
            this.MinimumSize = new System.Drawing.Size(260, 118);
            this.Name = "Form1";
            this.Text = "Modelo Cyrnel";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Button bntUpdate;
        private System.Windows.Forms.Button btnStopUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtAutoUpdateON;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtLastUpdated;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}

