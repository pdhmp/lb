namespace LiveBook
{
    partial class frmDeleteFileBrokerage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeleteFileBrokerage));
            this.cmdClose = new System.Windows.Forms.Button();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.dtpFileDate = new System.Windows.Forms.DateTimePicker();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            this.grpPortfolio = new System.Windows.Forms.GroupBox();
            this.grpBroker = new System.Windows.Forms.GroupBox();
            this.cmbBroker = new System.Windows.Forms.ComboBox();
            this.grpInstuments = new System.Windows.Forms.GroupBox();
            this.rdoOther = new System.Windows.Forms.RadioButton();
            this.rdoFutures = new System.Windows.Forms.RadioButton();
            this.grpDate.SuspendLayout();
            this.grpPortfolio.SuspendLayout();
            this.grpBroker.SuspendLayout();
            this.grpInstuments.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(542, 72);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(58, 23);
            this.cmdClose.TabIndex = 3;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.dtpFileDate);
            this.grpDate.Location = new System.Drawing.Point(12, 12);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(108, 51);
            this.grpDate.TabIndex = 8;
            this.grpDate.TabStop = false;
            this.grpDate.Text = "Select Date";
            // 
            // dtpFileDate
            // 
            this.dtpFileDate.Enabled = false;
            this.dtpFileDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFileDate.Location = new System.Drawing.Point(6, 19);
            this.dtpFileDate.Name = "dtpFileDate";
            this.dtpFileDate.Size = new System.Drawing.Size(95, 20);
            this.dtpFileDate.TabIndex = 0;
            this.dtpFileDate.Value = new System.DateTime(2013, 5, 9, 19, 7, 38, 0);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(468, 72);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(58, 23);
            this.cmdDelete.TabIndex = 2;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(6, 19);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 1;
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // grpPortfolio
            // 
            this.grpPortfolio.Controls.Add(this.cmbPortfolio);
            this.grpPortfolio.Location = new System.Drawing.Point(126, 12);
            this.grpPortfolio.Name = "grpPortfolio";
            this.grpPortfolio.Size = new System.Drawing.Size(165, 51);
            this.grpPortfolio.TabIndex = 27;
            this.grpPortfolio.TabStop = false;
            this.grpPortfolio.Text = "Portfólio";
            // 
            // grpBroker
            // 
            this.grpBroker.Controls.Add(this.cmbBroker);
            this.grpBroker.Location = new System.Drawing.Point(297, 12);
            this.grpBroker.Name = "grpBroker";
            this.grpBroker.Size = new System.Drawing.Size(165, 51);
            this.grpBroker.TabIndex = 28;
            this.grpBroker.TabStop = false;
            this.grpBroker.Text = "Broker";
            // 
            // cmbBroker
            // 
            this.cmbBroker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBroker.FormattingEnabled = true;
            this.cmbBroker.Location = new System.Drawing.Point(6, 19);
            this.cmbBroker.Name = "cmbBroker";
            this.cmbBroker.Size = new System.Drawing.Size(152, 21);
            this.cmbBroker.TabIndex = 1;
            // 
            // grpInstuments
            // 
            this.grpInstuments.Controls.Add(this.rdoOther);
            this.grpInstuments.Controls.Add(this.rdoFutures);
            this.grpInstuments.Location = new System.Drawing.Point(468, 12);
            this.grpInstuments.Name = "grpInstuments";
            this.grpInstuments.Size = new System.Drawing.Size(132, 51);
            this.grpInstuments.TabIndex = 28;
            this.grpInstuments.TabStop = false;
            this.grpInstuments.Text = "Instruments";
            // 
            // rdoOther
            // 
            this.rdoOther.AutoSize = true;
            this.rdoOther.Checked = true;
            this.rdoOther.Location = new System.Drawing.Point(72, 20);
            this.rdoOther.Name = "rdoOther";
            this.rdoOther.Size = new System.Drawing.Size(51, 17);
            this.rdoOther.TabIndex = 0;
            this.rdoOther.TabStop = true;
            this.rdoOther.Text = "Other";
            this.rdoOther.UseVisualStyleBackColor = true;
            // 
            // rdoFutures
            // 
            this.rdoFutures.AutoSize = true;
            this.rdoFutures.Location = new System.Drawing.Point(6, 20);
            this.rdoFutures.Name = "rdoFutures";
            this.rdoFutures.Size = new System.Drawing.Size(60, 17);
            this.rdoFutures.TabIndex = 0;
            this.rdoFutures.Text = "Futures";
            this.rdoFutures.UseVisualStyleBackColor = true;
            // 
            // frmDeleteFileBrokerage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(608, 106);
            this.Controls.Add(this.grpInstuments);
            this.Controls.Add(this.grpBroker);
            this.Controls.Add(this.grpPortfolio);
            this.Controls.Add(this.grpDate);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDeleteFileBrokerage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Imported Files Brokerage ";
            this.Load += new System.EventHandler(this.frmDeleteFileBrokerage_Load);
            this.grpDate.ResumeLayout(false);
            this.grpPortfolio.ResumeLayout(false);
            this.grpBroker.ResumeLayout(false);
            this.grpInstuments.ResumeLayout(false);
            this.grpInstuments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.GroupBox grpDate;
        private System.Windows.Forms.DateTimePicker dtpFileDate;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.ComboBox cmbPortfolio;
        private System.Windows.Forms.GroupBox grpPortfolio;
        private System.Windows.Forms.GroupBox grpBroker;
        private System.Windows.Forms.ComboBox cmbBroker;
        private System.Windows.Forms.GroupBox grpInstuments;
        private System.Windows.Forms.RadioButton rdoOther;
        private System.Windows.Forms.RadioButton rdoFutures;
    }
}