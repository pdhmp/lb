namespace LiveBook
{
    partial class frmEditMailingContacts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditMailingContacts));
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbPort = new LiveBook.NestPortCombo();
            this.btnNewContact = new System.Windows.Forms.Button();
            this.lsClients = new System.Windows.Forms.ListBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.rdPort = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdText = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIdContact = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkMHedge = new System.Windows.Forms.CheckBox();
            this.chkMPrev = new System.Windows.Forms.CheckBox();
            this.chkMIcatu = new System.Windows.Forms.CheckBox();
            this.chkMArb = new System.Windows.Forms.CheckBox();
            this.chkMAcoes = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDHedge = new System.Windows.Forms.CheckBox();
            this.chkDPrev = new System.Windows.Forms.CheckBox();
            this.chkDIcatu = new System.Windows.Forms.CheckBox();
            this.chkDArb = new System.Windows.Forms.CheckBox();
            this.chkDAcoes = new System.Windows.Forms.CheckBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmbPort);
            this.panel1.Controls.Add(this.btnNewContact);
            this.panel1.Controls.Add(this.lsClients);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.rdPort);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rdText);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 430);
            this.panel1.TabIndex = 0;
            // 
            // cmbPort
            // 
            this.cmbPort.DisplayMember = "Port_Name";
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.IdPortType = 2;
            this.cmbPort.includeAllPortsOption = false;
            this.cmbPort.includeMHConsolOption = false;
            this.cmbPort.Location = new System.Drawing.Point(4, 29);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(232, 21);
            this.cmbPort.TabIndex = 30;
            this.cmbPort.ValueMember = "Id_Portfolio";
            this.cmbPort.SelectedIndexChanged += new System.EventHandler(this.cmbPort_SelectedIndexChanged);
            // 
            // btnNewContact
            // 
            this.btnNewContact.Location = new System.Drawing.Point(5, 389);
            this.btnNewContact.Name = "btnNewContact";
            this.btnNewContact.Size = new System.Drawing.Size(232, 33);
            this.btnNewContact.TabIndex = 29;
            this.btnNewContact.Text = "Create New Contact";
            this.btnNewContact.UseVisualStyleBackColor = true;
            this.btnNewContact.Click += new System.EventHandler(this.btnNewContact_Click);
            // 
            // lsClients
            // 
            this.lsClients.FormattingEnabled = true;
            this.lsClients.Location = new System.Drawing.Point(5, 53);
            this.lsClients.Name = "lsClients";
            this.lsClients.Size = new System.Drawing.Size(232, 329);
            this.lsClients.TabIndex = 6;
            this.lsClients.SelectedIndexChanged += new System.EventHandler(this.lsClients_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::LiveBook.Properties.Resources.search;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(171, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(66, 23);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(5, 29);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(160, 20);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // rdPort
            // 
            this.rdPort.AutoSize = true;
            this.rdPort.Location = new System.Drawing.Point(142, 6);
            this.rdPort.Name = "rdPort";
            this.rdPort.Size = new System.Drawing.Size(63, 17);
            this.rdPort.TabIndex = 3;
            this.rdPort.TabStop = true;
            this.rdPort.Text = "Portfolio";
            this.rdPort.UseVisualStyleBackColor = true;
            this.rdPort.CheckedChanged += new System.EventHandler(this.rdPort_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search By:";
            // 
            // rdText
            // 
            this.rdText.AutoSize = true;
            this.rdText.Location = new System.Drawing.Point(59, 6);
            this.rdText.Name = "rdText";
            this.rdText.Size = new System.Drawing.Size(77, 17);
            this.rdText.TabIndex = 0;
            this.rdText.TabStop = true;
            this.rdText.Text = "Name/Mail";
            this.rdText.UseVisualStyleBackColor = true;
            this.rdText.CheckedChanged += new System.EventHandler(this.rdText_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.AccessibleRole = System.Windows.Forms.AccessibleRole.Pane;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.txtIdContact);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.txtMail);
            this.panel2.Controls.Add(this.txtName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(260, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 430);
            this.panel2.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Id Contact";
            // 
            // txtIdContact
            // 
            this.txtIdContact.BackColor = System.Drawing.Color.Silver;
            this.txtIdContact.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIdContact.Location = new System.Drawing.Point(65, 10);
            this.txtIdContact.Name = "txtIdContact";
            this.txtIdContact.Size = new System.Drawing.Size(100, 13);
            this.txtIdContact.TabIndex = 32;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(91, 388);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 33);
            this.btnUpdate.TabIndex = 31;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkMHedge);
            this.groupBox2.Controls.Add(this.chkMPrev);
            this.groupBox2.Controls.Add(this.chkMIcatu);
            this.groupBox2.Controls.Add(this.chkMArb);
            this.groupBox2.Controls.Add(this.chkMAcoes);
            this.groupBox2.Location = new System.Drawing.Point(138, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(123, 265);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Monthly List";
            // 
            // chkMHedge
            // 
            this.chkMHedge.AutoSize = true;
            this.chkMHedge.Location = new System.Drawing.Point(8, 124);
            this.chkMHedge.Name = "chkMHedge";
            this.chkMHedge.Size = new System.Drawing.Size(83, 17);
            this.chkMHedge.TabIndex = 5;
            this.chkMHedge.Text = "Nest Hedge";
            this.chkMHedge.UseVisualStyleBackColor = true;
            // 
            // chkMPrev
            // 
            this.chkMPrev.AutoSize = true;
            this.chkMPrev.Location = new System.Drawing.Point(8, 171);
            this.chkMPrev.Name = "chkMPrev";
            this.chkMPrev.Size = new System.Drawing.Size(107, 17);
            this.chkMPrev.TabIndex = 4;
            this.chkMPrev.Text = "Nest Previdencia";
            this.chkMPrev.UseVisualStyleBackColor = true;
            // 
            // chkMIcatu
            // 
            this.chkMIcatu.AutoSize = true;
            this.chkMIcatu.Location = new System.Drawing.Point(8, 218);
            this.chkMIcatu.Name = "chkMIcatu";
            this.chkMIcatu.Size = new System.Drawing.Size(100, 17);
            this.chkMIcatu.TabIndex = 2;
            this.chkMIcatu.Text = "Nest Prev Icatu";
            this.chkMIcatu.UseVisualStyleBackColor = true;
            // 
            // chkMArb
            // 
            this.chkMArb.AutoSize = true;
            this.chkMArb.Location = new System.Drawing.Point(8, 77);
            this.chkMArb.Name = "chkMArb";
            this.chkMArb.Size = new System.Drawing.Size(101, 17);
            this.chkMArb.TabIndex = 1;
            this.chkMArb.Text = "Nest Long Lony";
            this.chkMArb.UseVisualStyleBackColor = true;
            // 
            // chkMAcoes
            // 
            this.chkMAcoes.AutoSize = true;
            this.chkMAcoes.Location = new System.Drawing.Point(8, 30);
            this.chkMAcoes.Name = "chkMAcoes";
            this.chkMAcoes.Size = new System.Drawing.Size(81, 17);
            this.chkMAcoes.TabIndex = 0;
            this.chkMAcoes.Text = "Nest Ações";
            this.chkMAcoes.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDHedge);
            this.groupBox1.Controls.Add(this.chkDPrev);
            this.groupBox1.Controls.Add(this.chkDIcatu);
            this.groupBox1.Controls.Add(this.chkDArb);
            this.groupBox1.Controls.Add(this.chkDAcoes);
            this.groupBox1.Location = new System.Drawing.Point(6, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(126, 265);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Daily List";
            // 
            // chkDHedge
            // 
            this.chkDHedge.AutoSize = true;
            this.chkDHedge.Location = new System.Drawing.Point(10, 124);
            this.chkDHedge.Name = "chkDHedge";
            this.chkDHedge.Size = new System.Drawing.Size(83, 17);
            this.chkDHedge.TabIndex = 5;
            this.chkDHedge.Text = "Nest Hedge";
            this.chkDHedge.UseVisualStyleBackColor = true;
            // 
            // chkDPrev
            // 
            this.chkDPrev.AutoSize = true;
            this.chkDPrev.Location = new System.Drawing.Point(10, 171);
            this.chkDPrev.Name = "chkDPrev";
            this.chkDPrev.Size = new System.Drawing.Size(107, 17);
            this.chkDPrev.TabIndex = 4;
            this.chkDPrev.Text = "Nest Previdencia";
            this.chkDPrev.UseVisualStyleBackColor = true;
            // 
            // chkDIcatu
            // 
            this.chkDIcatu.AutoSize = true;
            this.chkDIcatu.Location = new System.Drawing.Point(10, 218);
            this.chkDIcatu.Name = "chkDIcatu";
            this.chkDIcatu.Size = new System.Drawing.Size(100, 17);
            this.chkDIcatu.TabIndex = 2;
            this.chkDIcatu.Text = "Nest Prev Icatu";
            this.chkDIcatu.UseVisualStyleBackColor = true;
            // 
            // chkDArb
            // 
            this.chkDArb.AutoSize = true;
            this.chkDArb.Location = new System.Drawing.Point(10, 77);
            this.chkDArb.Name = "chkDArb";
            this.chkDArb.Size = new System.Drawing.Size(99, 17);
            this.chkDArb.TabIndex = 1;
            this.chkDArb.Text = "Nest Long Only";
            this.chkDArb.UseVisualStyleBackColor = true;
            // 
            // chkDAcoes
            // 
            this.chkDAcoes.AutoSize = true;
            this.chkDAcoes.Location = new System.Drawing.Point(10, 30);
            this.chkDAcoes.Name = "chkDAcoes";
            this.chkDAcoes.Size = new System.Drawing.Size(81, 17);
            this.chkDAcoes.TabIndex = 0;
            this.chkDAcoes.Text = "Nest Ações";
            this.chkDAcoes.UseVisualStyleBackColor = true;
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(6, 91);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(255, 20);
            this.txtMail.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(6, 52);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(255, 20);
            this.txtName.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mail";
            // 
            // frmEditMailingContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(534, 454);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmEditMailingContacts";
            this.Text = "Edit Contacts";
            this.Load += new System.EventHandler(this.frmEditMailingContacts_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.RadioButton rdText;
        private System.Windows.Forms.RadioButton rdPort;

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        public System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListBox lsClients;
        private System.Windows.Forms.Button btnNewContact;
        private System.Windows.Forms.GroupBox groupBox2;
        private NestPortCombo cmbPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.CheckBox chkDHedge;
        private System.Windows.Forms.CheckBox chkDPrev;
        private System.Windows.Forms.CheckBox chkDIcatu;
        private System.Windows.Forms.CheckBox chkDArb;
        private System.Windows.Forms.CheckBox chkDAcoes;
        private System.Windows.Forms.CheckBox chkMHedge;
        private System.Windows.Forms.CheckBox chkMPrev;
        private System.Windows.Forms.CheckBox chkMIcatu;
        private System.Windows.Forms.CheckBox chkMArb;
        private System.Windows.Forms.CheckBox chkMAcoes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIdContact;
    }
}