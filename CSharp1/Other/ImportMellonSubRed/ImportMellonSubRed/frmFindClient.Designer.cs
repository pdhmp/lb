namespace ImportMellonSubRed
{
    partial class frmFindClient
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
            this.txtSearchString = new System.Windows.Forms.TextBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.lstContacts = new System.Windows.Forms.ListBox();
            this.cmdCreateNew = new System.Windows.Forms.Button();
            this.cmdUseSelected = new System.Windows.Forms.Button();
            this.lblClientName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtSearchString
            // 
            this.txtSearchString.Location = new System.Drawing.Point(12, 65);
            this.txtSearchString.Name = "txtSearchString";
            this.txtSearchString.Size = new System.Drawing.Size(272, 20);
            this.txtSearchString.TabIndex = 0;
            this.txtSearchString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchString_KeyDown);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(291, 62);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // lstContacts
            // 
            this.lstContacts.FormattingEnabled = true;
            this.lstContacts.Location = new System.Drawing.Point(13, 91);
            this.lstContacts.Name = "lstContacts";
            this.lstContacts.Size = new System.Drawing.Size(353, 212);
            this.lstContacts.TabIndex = 2;
            // 
            // cmdCreateNew
            // 
            this.cmdCreateNew.Location = new System.Drawing.Point(224, 310);
            this.cmdCreateNew.Name = "cmdCreateNew";
            this.cmdCreateNew.Size = new System.Drawing.Size(106, 37);
            this.cmdCreateNew.TabIndex = 3;
            this.cmdCreateNew.Text = "Create New";
            this.cmdCreateNew.UseVisualStyleBackColor = true;
            this.cmdCreateNew.Click += new System.EventHandler(this.cmdCreateNew_Click);
            // 
            // cmdUseSelected
            // 
            this.cmdUseSelected.Enabled = false;
            this.cmdUseSelected.Location = new System.Drawing.Point(54, 309);
            this.cmdUseSelected.Name = "cmdUseSelected";
            this.cmdUseSelected.Size = new System.Drawing.Size(107, 37);
            this.cmdUseSelected.TabIndex = 4;
            this.cmdUseSelected.Text = "Use Selected";
            this.cmdUseSelected.UseVisualStyleBackColor = true;
            this.cmdUseSelected.Click += new System.EventHandler(this.cmdUseSelected_Click);
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Location = new System.Drawing.Point(12, 9);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(79, 13);
            this.lblClientName.TabIndex = 5;
            this.lblClientName.Text = "CLIENT NAME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "The above client was not found. You can manually find it in the database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "or create a new record for him using the buttons below.";
            // 
            // frmFindClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 358);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblClientName);
            this.Controls.Add(this.cmdUseSelected);
            this.Controls.Add(this.cmdCreateNew);
            this.Controls.Add(this.lstContacts);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.txtSearchString);
            this.Name = "frmFindClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Client Not Found";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFindClient_FormClosing);
            this.Load += new System.EventHandler(this.frmFindClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchString;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.ListBox lstContacts;
        private System.Windows.Forms.Button cmdCreateNew;
        private System.Windows.Forms.Button cmdUseSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblClientName;
    }
}