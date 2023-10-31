namespace NestDesk
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpType = new System.Windows.Forms.ComboBox();
            this.txtObs = new System.Windows.Forms.TextBox();
            this.cmbUser = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dgTicket = new DevExpress.XtraGrid.GridControl();
            this.dtgTicket = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.rdAll = new System.Windows.Forms.RadioButton();
            this.rdOpen = new System.Windows.Forms.RadioButton();
            this.rdClose = new System.Windows.Forms.RadioButton();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTicket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTicket)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(513, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Insert";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpType);
            this.groupBox1.Controls.Add(this.txtObs);
            this.groupBox1.Controls.Add(this.cmbUser);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Location = new System.Drawing.Point(3, 303);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(619, 114);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Ticket";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Obs:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "User:";
            // 
            // dtpType
            // 
            this.dtpType.FormattingEnabled = true;
            this.dtpType.Location = new System.Drawing.Point(44, 86);
            this.dtpType.Name = "dtpType";
            this.dtpType.Size = new System.Drawing.Size(121, 21);
            this.dtpType.TabIndex = 2;
            // 
            // txtObs
            // 
            this.txtObs.Location = new System.Drawing.Point(231, 26);
            this.txtObs.Multiline = true;
            this.txtObs.Name = "txtObs";
            this.txtObs.Size = new System.Drawing.Size(226, 81);
            this.txtObs.TabIndex = 3;
            // 
            // cmbUser
            // 
            this.cmbUser.FormattingEnabled = true;
            this.cmbUser.Location = new System.Drawing.Point(44, 26);
            this.cmbUser.Name = "cmbUser";
            this.cmbUser.Size = new System.Drawing.Size(121, 21);
            this.cmbUser.TabIndex = 0;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(46, 56);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(96, 20);
            this.dtpDate.TabIndex = 1;
            // 
            // dgTicket
            // 
            this.dgTicket.Location = new System.Drawing.Point(3, 12);
            this.dgTicket.MainView = this.dtgTicket;
            this.dgTicket.Name = "dgTicket";
            this.dgTicket.Size = new System.Drawing.Size(619, 285);
            this.dgTicket.TabIndex = 3;
            this.dgTicket.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgTicket});
            // 
            // dtgTicket
            // 
            this.dtgTicket.GridControl = this.dgTicket;
            this.dtgTicket.Name = "dtgTicket";
            // 
            // rdAll
            // 
            this.rdAll.AutoSize = true;
            this.rdAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.rdAll.Location = new System.Drawing.Point(178, 21);
            this.rdAll.Name = "rdAll";
            this.rdAll.Size = new System.Drawing.Size(77, 17);
            this.rdAll.TabIndex = 4;
            this.rdAll.TabStop = true;
            this.rdAll.Text = "All Ticket´s";
            this.rdAll.UseVisualStyleBackColor = false;
            // 
            // rdOpen
            // 
            this.rdOpen.AutoSize = true;
            this.rdOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.rdOpen.Location = new System.Drawing.Point(261, 21);
            this.rdOpen.Name = "rdOpen";
            this.rdOpen.Size = new System.Drawing.Size(92, 17);
            this.rdOpen.TabIndex = 5;
            this.rdOpen.TabStop = true;
            this.rdOpen.Text = "Open Ticket´s";
            this.rdOpen.UseVisualStyleBackColor = false;
            // 
            // rdClose
            // 
            this.rdClose.AutoSize = true;
            this.rdClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.rdClose.Location = new System.Drawing.Point(359, 21);
            this.rdClose.Name = "rdClose";
            this.rdClose.Size = new System.Drawing.Size(92, 17);
            this.rdClose.TabIndex = 6;
            this.rdClose.TabStop = true;
            this.rdClose.Text = "Close Ticket´s";
            this.rdClose.UseVisualStyleBackColor = false;
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Location = new System.Drawing.Point(540, 18);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdrefresh.TabIndex = 7;
            this.cmdrefresh.Text = "Refresh";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 417);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.rdClose);
            this.Controls.Add(this.rdOpen);
            this.Controls.Add(this.rdAll);
            this.Controls.Add(this.dgTicket);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Help Desk System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTicket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTicket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dtpType;
        private System.Windows.Forms.TextBox txtObs;
        private System.Windows.Forms.ComboBox cmbUser;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraGrid.GridControl dgTicket;
        private DevExpress.XtraGrid.Views.Grid.GridView dtgTicket;
        private System.Windows.Forms.RadioButton rdAll;
        private System.Windows.Forms.RadioButton rdOpen;
        private System.Windows.Forms.RadioButton rdClose;
        private System.Windows.Forms.Button cmdrefresh;
    }
}

