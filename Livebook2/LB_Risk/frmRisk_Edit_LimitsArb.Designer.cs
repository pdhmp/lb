namespace LiveBook
{
    partial class frmEditLimitsArb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditLimitsArb));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstSection = new System.Windows.Forms.ListBox();
            this.groupVariableInfo = new System.Windows.Forms.GroupBox();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.grpOPA1 = new System.Windows.Forms.GroupBox();
            this.cmbPar2 = new System.Windows.Forms.ComboBox();
            this.cmbPar3 = new System.Windows.Forms.ComboBox();
            this.lblPar3 = new System.Windows.Forms.Label();
            this.lblPar2 = new System.Windows.Forms.Label();
            this.txtPar1 = new System.Windows.Forms.TextBox();
            this.lblPar1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbd_StressType = new System.Windows.Forms.ComboBox();
            this.lstAvDates = new System.Windows.Forms.ListBox();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtd_StressValue = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupVariableInfo.SuspendLayout();
            this.grpOPA1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lstSection);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 371);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sections";
            // 
            // lstSection
            // 
            this.lstSection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstSection.FormattingEnabled = true;
            this.lstSection.Location = new System.Drawing.Point(6, 21);
            this.lstSection.Name = "lstSection";
            this.lstSection.Size = new System.Drawing.Size(175, 342);
            this.lstSection.TabIndex = 0;
            this.lstSection.SelectedIndexChanged += new System.EventHandler(this.lstSection_SelectedIndexChanged);
            // 
            // groupVariableInfo
            // 
            this.groupVariableInfo.Controls.Add(this.dtpIniDate);
            this.groupVariableInfo.Controls.Add(this.label4);
            this.groupVariableInfo.Controls.Add(this.dtpEndDate);
            this.groupVariableInfo.Controls.Add(this.label3);
            this.groupVariableInfo.Controls.Add(this.grpOPA1);
            this.groupVariableInfo.Controls.Add(this.label1);
            this.groupVariableInfo.Controls.Add(this.cmbd_StressType);
            this.groupVariableInfo.Controls.Add(this.lstAvDates);
            this.groupVariableInfo.Controls.Add(this.cmdUpdate);
            this.groupVariableInfo.Controls.Add(this.label2);
            this.groupVariableInfo.Controls.Add(this.txtd_StressValue);
            this.groupVariableInfo.Controls.Add(this.label17);
            this.groupVariableInfo.Location = new System.Drawing.Point(209, 12);
            this.groupVariableInfo.Name = "groupVariableInfo";
            this.groupVariableInfo.Size = new System.Drawing.Size(235, 329);
            this.groupVariableInfo.TabIndex = 1;
            this.groupVariableInfo.TabStop = false;
            this.groupVariableInfo.Text = "Variable Information";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(96, 166);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 136;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 135;
            this.label3.Text = "Ini Date";
            // 
            // grpOPA1
            // 
            this.grpOPA1.Controls.Add(this.cmbPar2);
            this.grpOPA1.Controls.Add(this.cmbPar3);
            this.grpOPA1.Controls.Add(this.lblPar3);
            this.grpOPA1.Controls.Add(this.lblPar2);
            this.grpOPA1.Controls.Add(this.txtPar1);
            this.grpOPA1.Controls.Add(this.lblPar1);
            this.grpOPA1.Location = new System.Drawing.Point(6, 192);
            this.grpOPA1.Name = "grpOPA1";
            this.grpOPA1.Size = new System.Drawing.Size(229, 104);
            this.grpOPA1.TabIndex = 134;
            this.grpOPA1.TabStop = false;
            this.grpOPA1.Text = "OPA1";
            this.grpOPA1.Visible = false;
            // 
            // cmbPar2
            // 
            this.cmbPar2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPar2.FormattingEnabled = true;
            this.cmbPar2.Location = new System.Drawing.Point(117, 43);
            this.cmbPar2.Name = "cmbPar2";
            this.cmbPar2.Size = new System.Drawing.Size(99, 21);
            this.cmbPar2.TabIndex = 5;
            // 
            // cmbPar3
            // 
            this.cmbPar3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbPar3.FormattingEnabled = true;
            this.cmbPar3.Location = new System.Drawing.Point(117, 69);
            this.cmbPar3.Name = "cmbPar3";
            this.cmbPar3.Size = new System.Drawing.Size(99, 21);
            this.cmbPar3.TabIndex = 6;
            // 
            // lblPar3
            // 
            this.lblPar3.AutoSize = true;
            this.lblPar3.Location = new System.Drawing.Point(7, 77);
            this.lblPar3.Name = "lblPar3";
            this.lblPar3.Size = new System.Drawing.Size(29, 13);
            this.lblPar3.TabIndex = 138;
            this.lblPar3.Text = "Par3";
            // 
            // lblPar2
            // 
            this.lblPar2.AutoSize = true;
            this.lblPar2.Location = new System.Drawing.Point(7, 51);
            this.lblPar2.Name = "lblPar2";
            this.lblPar2.Size = new System.Drawing.Size(29, 13);
            this.lblPar2.TabIndex = 137;
            this.lblPar2.Text = "Par2";
            // 
            // txtPar1
            // 
            this.txtPar1.Location = new System.Drawing.Point(117, 18);
            this.txtPar1.Name = "txtPar1";
            this.txtPar1.Size = new System.Drawing.Size(99, 20);
            this.txtPar1.TabIndex = 4;
            // 
            // lblPar1
            // 
            this.lblPar1.AutoSize = true;
            this.lblPar1.Location = new System.Drawing.Point(7, 25);
            this.lblPar1.Name = "lblPar1";
            this.lblPar1.Size = new System.Drawing.Size(29, 13);
            this.lblPar1.TabIndex = 135;
            this.lblPar1.Text = "Par1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 126;
            this.label1.Text = "Stress Type";
            // 
            // cmbd_StressType
            // 
            this.cmbd_StressType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbd_StressType.FormattingEnabled = true;
            this.cmbd_StressType.Location = new System.Drawing.Point(96, 113);
            this.cmbd_StressType.Name = "cmbd_StressType";
            this.cmbd_StressType.Size = new System.Drawing.Size(128, 21);
            this.cmbd_StressType.TabIndex = 3;
            this.cmbd_StressType.SelectedIndexChanged += new System.EventHandler(this.cmbd_StressType_SelectedIndexChanged);
            // 
            // lstAvDates
            // 
            this.lstAvDates.FormattingEnabled = true;
            this.lstAvDates.Location = new System.Drawing.Point(96, 21);
            this.lstAvDates.Name = "lstAvDates";
            this.lstAvDates.Size = new System.Drawing.Size(126, 56);
            this.lstAvDates.TabIndex = 1;
            this.lstAvDates.SelectedIndexChanged += new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdate.Location = new System.Drawing.Point(160, 301);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(69, 22);
            this.cmdUpdate.TabIndex = 4;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(35, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "New Date";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtd_StressValue
            // 
            this.txtd_StressValue.Location = new System.Drawing.Point(96, 87);
            this.txtd_StressValue.Name = "txtd_StressValue";
            this.txtd_StressValue.Size = new System.Drawing.Size(128, 20);
            this.txtd_StressValue.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(24, 90);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(66, 13);
            this.label17.TabIndex = 64;
            this.label17.Text = "Stress Value";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(340, 350);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(95, 33);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(96, 140);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 138;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 137;
            this.label4.Text = "End Date";
            // 
            // frmEditLimitsArb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 392);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupVariableInfo);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditLimitsArb";
            this.Text = "Edit Risk Arb";
            this.Load += new System.EventHandler(this.frmBrokers_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupVariableInfo.ResumeLayout(false);
            this.groupVariableInfo.PerformLayout();
            this.grpOPA1.ResumeLayout(false);
            this.grpOPA1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.GroupBox groupVariableInfo;
        private System.Windows.Forms.ListBox lstAvDates;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtd_StressValue;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstSection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbd_StressType;
        private System.Windows.Forms.GroupBox grpOPA1;
        private System.Windows.Forms.ComboBox cmbPar3;
        private System.Windows.Forms.Label lblPar3;
        private System.Windows.Forms.Label lblPar2;
        private System.Windows.Forms.TextBox txtPar1;
        private System.Windows.Forms.Label lblPar1;
        private System.Windows.Forms.ComboBox cmbPar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label4;
    }
}