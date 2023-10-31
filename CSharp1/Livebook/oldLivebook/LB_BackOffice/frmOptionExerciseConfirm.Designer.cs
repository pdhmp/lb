namespace LiveBook
{
    partial class frmOptionExerciseCnfirm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptionExerciseCnfirm));
            this.groupconfirm = new System.Windows.Forms.GroupBox();
            this.rdOTM = new System.Windows.Forms.RadioButton();
            this.rdITM = new System.Windows.Forms.RadioButton();
            this.cmdConfirm = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.groupExercise = new System.Windows.Forms.GroupBox();
            this.txtpartial = new System.Windows.Forms.TextBox();
            this.rdPartial = new System.Windows.Forms.RadioButton();
            this.rdFull = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBroker = new System.Windows.Forms.ComboBox();
            this.groupconfirm.SuspendLayout();
            this.groupExercise.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupconfirm
            // 
            this.groupconfirm.Controls.Add(this.rdOTM);
            this.groupconfirm.Controls.Add(this.rdITM);
            this.groupconfirm.Location = new System.Drawing.Point(11, 39);
            this.groupconfirm.Name = "groupconfirm";
            this.groupconfirm.Size = new System.Drawing.Size(241, 48);
            this.groupconfirm.TabIndex = 0;
            this.groupconfirm.TabStop = false;
            this.groupconfirm.Text = "Confirm Exercise";
            // 
            // rdOTM
            // 
            this.rdOTM.AutoSize = true;
            this.rdOTM.Location = new System.Drawing.Point(173, 20);
            this.rdOTM.Name = "rdOTM";
            this.rdOTM.Size = new System.Drawing.Size(60, 17);
            this.rdOTM.TabIndex = 1;
            this.rdOTM.TabStop = true;
            this.rdOTM.Text = "Expired";
            this.rdOTM.UseVisualStyleBackColor = true;
            this.rdOTM.CheckedChanged += new System.EventHandler(this.rdOTM_CheckedChanged);
            // 
            // rdITM
            // 
            this.rdITM.AutoSize = true;
            this.rdITM.Location = new System.Drawing.Point(7, 20);
            this.rdITM.Name = "rdITM";
            this.rdITM.Size = new System.Drawing.Size(91, 17);
            this.rdITM.TabIndex = 0;
            this.rdITM.TabStop = true;
            this.rdITM.Text = "In The Money";
            this.rdITM.UseVisualStyleBackColor = true;
            this.rdITM.CheckedChanged += new System.EventHandler(this.rdITM_CheckedChanged);
            // 
            // cmdConfirm
            // 
            this.cmdConfirm.Location = new System.Drawing.Point(24, 157);
            this.cmdConfirm.Name = "cmdConfirm";
            this.cmdConfirm.Size = new System.Drawing.Size(75, 23);
            this.cmdConfirm.TabIndex = 1;
            this.cmdConfirm.Text = "Confirm";
            this.cmdConfirm.UseVisualStyleBackColor = true;
            this.cmdConfirm.Click += new System.EventHandler(this.cmdConfirm_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(147, 157);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // groupExercise
            // 
            this.groupExercise.Controls.Add(this.txtpartial);
            this.groupExercise.Controls.Add(this.rdPartial);
            this.groupExercise.Controls.Add(this.rdFull);
            this.groupExercise.Location = new System.Drawing.Point(11, 93);
            this.groupExercise.Name = "groupExercise";
            this.groupExercise.Size = new System.Drawing.Size(241, 48);
            this.groupExercise.TabIndex = 3;
            this.groupExercise.TabStop = false;
            this.groupExercise.Text = "Exercise Type";
            // 
            // txtpartial
            // 
            this.txtpartial.Location = new System.Drawing.Point(146, 20);
            this.txtpartial.Name = "txtpartial";
            this.txtpartial.Size = new System.Drawing.Size(87, 20);
            this.txtpartial.TabIndex = 2;
            this.txtpartial.Leave += new System.EventHandler(this.txtpartial_Leave);
            // 
            // rdPartial
            // 
            this.rdPartial.AutoSize = true;
            this.rdPartial.Location = new System.Drawing.Point(84, 21);
            this.rdPartial.Name = "rdPartial";
            this.rdPartial.Size = new System.Drawing.Size(54, 17);
            this.rdPartial.TabIndex = 1;
            this.rdPartial.TabStop = true;
            this.rdPartial.Text = "Partial";
            this.rdPartial.UseVisualStyleBackColor = true;
            this.rdPartial.CheckedChanged += new System.EventHandler(this.rdPartial_CheckedChanged);
            // 
            // rdFull
            // 
            this.rdFull.AutoSize = true;
            this.rdFull.Location = new System.Drawing.Point(7, 20);
            this.rdFull.Name = "rdFull";
            this.rdFull.Size = new System.Drawing.Size(41, 17);
            this.rdFull.TabIndex = 0;
            this.rdFull.TabStop = true;
            this.rdFull.Text = "Full";
            this.rdFull.UseVisualStyleBackColor = true;
            this.rdFull.CheckedChanged += new System.EventHandler(this.rdFull_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Broker";
            // 
            // cmbBroker
            // 
            this.cmbBroker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbBroker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBroker.FormattingEnabled = true;
            this.cmbBroker.Location = new System.Drawing.Point(50, 12);
            this.cmbBroker.Name = "cmbBroker";
            this.cmbBroker.Size = new System.Drawing.Size(121, 21);
            this.cmbBroker.TabIndex = 5;
            // 
            // frmOptionExerciseCnfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(264, 198);
            this.Controls.Add(this.cmbBroker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupExercise);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdConfirm);
            this.Controls.Add(this.groupconfirm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOptionExerciseCnfirm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirm Exercise Options";
            this.Load += new System.EventHandler(this.frmOptionExerciseCnfirm_Load);
            this.groupconfirm.ResumeLayout(false);
            this.groupconfirm.PerformLayout();
            this.groupExercise.ResumeLayout(false);
            this.groupExercise.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupconfirm;
        private System.Windows.Forms.RadioButton rdOTM;
        private System.Windows.Forms.RadioButton rdITM;
        private System.Windows.Forms.Button cmdConfirm;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.GroupBox groupExercise;
        private System.Windows.Forms.TextBox txtpartial;
        private System.Windows.Forms.RadioButton rdPartial;
        private System.Windows.Forms.RadioButton rdFull;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBroker;
    }
}