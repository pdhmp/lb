namespace UpdateBovespaSecurities
{
    partial class frmUpdateSecurities
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.labCounter = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labNewItems = new System.Windows.Forms.Label();
            this.labProcessed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labItemsUpdated = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(206, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(210, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status";
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 500;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // labCounter
            // 
            this.labCounter.Location = new System.Drawing.Point(206, 64);
            this.labCounter.Name = "labCounter";
            this.labCounter.Size = new System.Drawing.Size(100, 13);
            this.labCounter.TabIndex = 4;
            this.labCounter.Text = "Counter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Received Data:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "New Items Created:";
            // 
            // labNewItems
            // 
            this.labNewItems.Location = new System.Drawing.Point(206, 121);
            this.labNewItems.Name = "labNewItems";
            this.labNewItems.Size = new System.Drawing.Size(100, 13);
            this.labNewItems.TabIndex = 6;
            this.labNewItems.Text = "Counter";
            // 
            // labProcessed
            // 
            this.labProcessed.Location = new System.Drawing.Point(206, 93);
            this.labProcessed.Name = "labProcessed";
            this.labProcessed.Size = new System.Drawing.Size(100, 13);
            this.labProcessed.TabIndex = 8;
            this.labProcessed.Text = "Counter";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Processed:";
            // 
            // labItemsUpdated
            // 
            this.labItemsUpdated.Location = new System.Drawing.Point(206, 149);
            this.labItemsUpdated.Name = "labItemsUpdated";
            this.labItemsUpdated.Size = new System.Drawing.Size(100, 13);
            this.labItemsUpdated.TabIndex = 10;
            this.labItemsUpdated.Text = "Counter";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Items Updated:";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(32, 190);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(367, 278);
            this.txtStatus.TabIndex = 11;
            // 
            // frmUpdateSecurities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 491);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.labItemsUpdated);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labProcessed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labNewItems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labCounter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.Name = "frmUpdateSecurities";
            this.Text = "Update Bovespa Securities";
            this.Load += new System.EventHandler(this.frmUpdateSecurities_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label labCounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labNewItems;
        private System.Windows.Forms.Label labProcessed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labItemsUpdated;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtStatus;
    }
}