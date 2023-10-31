namespace LiveBook
{
    partial class frmBrokers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBrokers));
            this.cmdInsertNew = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstBrokers = new System.Windows.Forms.ListBox();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.groupVariableInfo = new System.Windows.Forms.GroupBox();
            this.lstAvDates = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtd_DefaultRebate = new System.Windows.Forms.TextBox();
            this.txtd_RebateAfterCAP = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtd_CAPValue = new System.Windows.Forms.TextBox();
            this.groupDescription = new System.Windows.Forms.GroupBox();
            this.txtd_ImportName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtd_MellonExpName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtd_Broker_Ticker = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtd_Id_CBLC = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtd_Id_BMF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtd_Id_BOVESPA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtd_Nome = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lbld_Id_Corretora = new System.Windows.Forms.Label();
            this.cmbd_Id_praca = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupVariableInfo.SuspendLayout();
            this.groupDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdInsertNew
            // 
            this.cmdInsertNew.Enabled = false;
            this.cmdInsertNew.Location = new System.Drawing.Point(18, 236);
            this.cmdInsertNew.Name = "cmdInsertNew";
            this.cmdInsertNew.Size = new System.Drawing.Size(175, 33);
            this.cmdInsertNew.TabIndex = 31;
            this.cmdInsertNew.Text = "Create New Broker";
            this.cmdInsertNew.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstBrokers);
            this.groupBox1.Controls.Add(this.cmdSearch);
            this.groupBox1.Controls.Add(this.txtsearch);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 218);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Brokers";
            // 
            // lstBrokers
            // 
            this.lstBrokers.FormattingEnabled = true;
            this.lstBrokers.Location = new System.Drawing.Point(6, 47);
            this.lstBrokers.Name = "lstBrokers";
            this.lstBrokers.Size = new System.Drawing.Size(175, 160);
            this.lstBrokers.TabIndex = 2;
            this.lstBrokers.SelectedIndexChanged += new System.EventHandler(this.lstBrokers_SelectedIndexChanged);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Image = global::LiveBook.Properties.Resources.search;
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(115, 18);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(66, 23);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.UseVisualStyleBackColor = true;
            // 
            // txtsearch
            // 
            this.txtsearch.Location = new System.Drawing.Point(6, 19);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(103, 20);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.txtsearch_TextChanged);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(605, 216);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(126, 33);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(461, 216);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(126, 33);
            this.cmdUpdate.TabIndex = 3;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // groupVariableInfo
            // 
            this.groupVariableInfo.Controls.Add(this.lstAvDates);
            this.groupVariableInfo.Controls.Add(this.label2);
            this.groupVariableInfo.Controls.Add(this.txtd_DefaultRebate);
            this.groupVariableInfo.Controls.Add(this.txtd_RebateAfterCAP);
            this.groupVariableInfo.Controls.Add(this.label15);
            this.groupVariableInfo.Controls.Add(this.label17);
            this.groupVariableInfo.Controls.Add(this.label18);
            this.groupVariableInfo.Controls.Add(this.txtd_CAPValue);
            this.groupVariableInfo.Location = new System.Drawing.Point(455, 12);
            this.groupVariableInfo.Name = "groupVariableInfo";
            this.groupVariableInfo.Size = new System.Drawing.Size(291, 164);
            this.groupVariableInfo.TabIndex = 2;
            this.groupVariableInfo.TabStop = false;
            this.groupVariableInfo.Text = "Variable Information";
            // 
            // lstAvDates
            // 
            this.lstAvDates.FormattingEnabled = true;
            this.lstAvDates.Location = new System.Drawing.Point(107, 22);
            this.lstAvDates.Name = "lstAvDates";
            this.lstAvDates.Size = new System.Drawing.Size(169, 56);
            this.lstAvDates.TabIndex = 0;
            this.lstAvDates.SelectedIndexChanged += new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(18, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 124;
            this.label2.Text = "New Date";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtd_DefaultRebate
            // 
            this.txtd_DefaultRebate.Location = new System.Drawing.Point(107, 84);
            this.txtd_DefaultRebate.Name = "txtd_DefaultRebate";
            this.txtd_DefaultRebate.Size = new System.Drawing.Size(169, 20);
            this.txtd_DefaultRebate.TabIndex = 1;
            // 
            // txtd_RebateAfterCAP
            // 
            this.txtd_RebateAfterCAP.Location = new System.Drawing.Point(107, 111);
            this.txtd_RebateAfterCAP.Name = "txtd_RebateAfterCAP";
            this.txtd_RebateAfterCAP.Size = new System.Drawing.Size(105, 20);
            this.txtd_RebateAfterCAP.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 123;
            this.label15.Text = "CAP Value";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(9, 87);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 64;
            this.label17.Text = "Default Rebate";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 114);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 13);
            this.label18.TabIndex = 59;
            this.label18.Text = "Rebate After CAP";
            // 
            // txtd_CAPValue
            // 
            this.txtd_CAPValue.Location = new System.Drawing.Point(107, 140);
            this.txtd_CAPValue.Name = "txtd_CAPValue";
            this.txtd_CAPValue.Size = new System.Drawing.Size(169, 20);
            this.txtd_CAPValue.TabIndex = 3;
            this.txtd_CAPValue.Leave += new System.EventHandler(this.txtd_CAPValue_Leave);
            // 
            // groupDescription
            // 
            this.groupDescription.Controls.Add(this.txtd_ImportName);
            this.groupDescription.Controls.Add(this.label9);
            this.groupDescription.Controls.Add(this.txtd_MellonExpName);
            this.groupDescription.Controls.Add(this.label8);
            this.groupDescription.Controls.Add(this.txtd_Broker_Ticker);
            this.groupDescription.Controls.Add(this.label7);
            this.groupDescription.Controls.Add(this.txtd_Id_CBLC);
            this.groupDescription.Controls.Add(this.label6);
            this.groupDescription.Controls.Add(this.txtd_Id_BMF);
            this.groupDescription.Controls.Add(this.label5);
            this.groupDescription.Controls.Add(this.txtd_Id_BOVESPA);
            this.groupDescription.Controls.Add(this.label4);
            this.groupDescription.Controls.Add(this.txtd_Nome);
            this.groupDescription.Controls.Add(this.label3);
            this.groupDescription.Controls.Add(this.label34);
            this.groupDescription.Controls.Add(this.lbld_Id_Corretora);
            this.groupDescription.Controls.Add(this.cmbd_Id_praca);
            this.groupDescription.Controls.Add(this.label1);
            this.groupDescription.Location = new System.Drawing.Point(205, 12);
            this.groupDescription.Name = "groupDescription";
            this.groupDescription.Size = new System.Drawing.Size(244, 257);
            this.groupDescription.TabIndex = 1;
            this.groupDescription.TabStop = false;
            this.groupDescription.Text = "Security Description";
            // 
            // txtd_ImportName
            // 
            this.txtd_ImportName.Location = new System.Drawing.Point(83, 223);
            this.txtd_ImportName.Name = "txtd_ImportName";
            this.txtd_ImportName.Size = new System.Drawing.Size(150, 20);
            this.txtd_ImportName.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 230);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 13);
            this.label9.TabIndex = 107;
            this.label9.Text = "Import Name";
            // 
            // txtd_MellonExpName
            // 
            this.txtd_MellonExpName.Location = new System.Drawing.Point(83, 197);
            this.txtd_MellonExpName.Name = "txtd_MellonExpName";
            this.txtd_MellonExpName.Size = new System.Drawing.Size(150, 20);
            this.txtd_MellonExpName.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 105;
            this.label8.Text = "Mellon Name";
            // 
            // txtd_Broker_Ticker
            // 
            this.txtd_Broker_Ticker.Location = new System.Drawing.Point(83, 171);
            this.txtd_Broker_Ticker.Name = "txtd_Broker_Ticker";
            this.txtd_Broker_Ticker.Size = new System.Drawing.Size(150, 20);
            this.txtd_Broker_Ticker.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 103;
            this.label7.Text = "Broker Ticker";
            // 
            // txtd_Id_CBLC
            // 
            this.txtd_Id_CBLC.Location = new System.Drawing.Point(83, 149);
            this.txtd_Id_CBLC.Name = "txtd_Id_CBLC";
            this.txtd_Id_CBLC.Size = new System.Drawing.Size(150, 20);
            this.txtd_Id_CBLC.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 101;
            this.label6.Text = "Id CBLC";
            // 
            // txtd_Id_BMF
            // 
            this.txtd_Id_BMF.Location = new System.Drawing.Point(83, 123);
            this.txtd_Id_BMF.Name = "txtd_Id_BMF";
            this.txtd_Id_BMF.Size = new System.Drawing.Size(150, 20);
            this.txtd_Id_BMF.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 99;
            this.label5.Text = "Id BMF";
            // 
            // txtd_Id_BOVESPA
            // 
            this.txtd_Id_BOVESPA.Location = new System.Drawing.Point(83, 97);
            this.txtd_Id_BOVESPA.Name = "txtd_Id_BOVESPA";
            this.txtd_Id_BOVESPA.Size = new System.Drawing.Size(150, 20);
            this.txtd_Id_BOVESPA.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 97;
            this.label4.Text = "Id Bovespa";
            // 
            // txtd_Nome
            // 
            this.txtd_Nome.Location = new System.Drawing.Point(83, 47);
            this.txtd_Nome.Name = "txtd_Nome";
            this.txtd_Nome.Size = new System.Drawing.Size(150, 20);
            this.txtd_Nome.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 95;
            this.label3.Text = "Name";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(9, 77);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 13);
            this.label34.TabIndex = 93;
            this.label34.Text = "Geography";
            // 
            // lbld_Id_Corretora
            // 
            this.lbld_Id_Corretora.AutoSize = true;
            this.lbld_Id_Corretora.Location = new System.Drawing.Point(100, 16);
            this.lbld_Id_Corretora.Name = "lbld_Id_Corretora";
            this.lbld_Id_Corretora.Size = new System.Drawing.Size(16, 13);
            this.lbld_Id_Corretora.TabIndex = 92;
            this.lbld_Id_Corretora.Text = "Id";
            // 
            // cmbd_Id_praca
            // 
            this.cmbd_Id_praca.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbd_Id_praca.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbd_Id_praca.FormattingEnabled = true;
            this.cmbd_Id_praca.Location = new System.Drawing.Point(83, 73);
            this.cmbd_Id_praca.Name = "cmbd_Id_praca";
            this.cmbd_Id_praca.Size = new System.Drawing.Size(150, 21);
            this.cmbd_Id_praca.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 91;
            this.label1.Text = "Database Id:";
            // 
            // frmBrokers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(757, 276);
            this.Controls.Add(this.groupDescription);
            this.Controls.Add(this.groupVariableInfo);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdInsertNew);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBrokers";
            this.Text = "Brokers";
            this.Load += new System.EventHandler(this.frmBrokers_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupVariableInfo.ResumeLayout(false);
            this.groupVariableInfo.PerformLayout();
            this.groupDescription.ResumeLayout(false);
            this.groupDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdInsertNew;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstBrokers;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.GroupBox groupVariableInfo;
        private System.Windows.Forms.ListBox lstAvDates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtd_DefaultRebate;
        private System.Windows.Forms.TextBox txtd_RebateAfterCAP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtd_CAPValue;
        public System.Windows.Forms.GroupBox groupDescription;
        private System.Windows.Forms.Label lbld_Id_Corretora;
        private System.Windows.Forms.ComboBox cmbd_Id_praca;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtd_ImportName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtd_MellonExpName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtd_Broker_Ticker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtd_Id_CBLC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtd_Id_BMF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtd_Id_BOVESPA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtd_Nome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label34;
    }
}