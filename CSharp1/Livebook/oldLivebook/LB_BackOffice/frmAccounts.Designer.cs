namespace LiveBook
{
    partial class frmAccounts
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
            this.dtgData = new MyXtraGrid.MyGridControl();
            this.dgEditData = new MyXtraGrid.MyGridView();
            this.txtAccountID = new System.Windows.Forms.TextBox();
            this.lblAccountID = new System.Windows.Forms.Label();
            this.lblBroker = new System.Windows.Forms.Label();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.txtAccountTicker = new System.Windows.Forms.TextBox();
            this.lblAccountTicker = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.grpEdit = new System.Windows.Forms.GroupBox();
            this.cboBroker = new System.Windows.Forms.ComboBox();
            this.btnReject = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditData)).BeginInit();
            this.grpEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgData
            // 
            this.dtgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgData.Location = new System.Drawing.Point(0, 0);
            this.dtgData.LookAndFeel.SkinName = "Blue";
            this.dtgData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgData.MainView = this.dgEditData;
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(458, 286);
            this.dtgData.TabIndex = 23;
            this.dtgData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgEditData});
            this.dtgData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dtgData_MouseDoubleClick);
            // 
            // dgEditData
            // 
            this.dgEditData.GridControl = this.dtgData;
            this.dgEditData.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgEditData.Name = "dgEditData";
            this.dgEditData.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgEditData.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dgEditData.OptionsBehavior.Editable = false;
            this.dgEditData.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgEditData.OptionsView.ShowAutoFilterRow = true;
            this.dgEditData.OptionsView.ShowGroupPanel = false;
            // 
            // txtAccountID
            // 
            this.txtAccountID.BackColor = System.Drawing.SystemColors.Control;
            this.txtAccountID.Location = new System.Drawing.Point(108, 20);
            this.txtAccountID.Name = "txtAccountID";
            this.txtAccountID.ReadOnly = true;
            this.txtAccountID.Size = new System.Drawing.Size(195, 20);
            this.txtAccountID.TabIndex = 25;
            // 
            // lblAccountID
            // 
            this.lblAccountID.AutoSize = true;
            this.lblAccountID.Location = new System.Drawing.Point(15, 23);
            this.lblAccountID.Name = "lblAccountID";
            this.lblAccountID.Size = new System.Drawing.Size(61, 13);
            this.lblAccountID.TabIndex = 26;
            this.lblAccountID.Text = "Account ID";
            // 
            // lblBroker
            // 
            this.lblBroker.AutoSize = true;
            this.lblBroker.Location = new System.Drawing.Point(15, 49);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(38, 13);
            this.lblBroker.TabIndex = 26;
            this.lblBroker.Text = "Broker";
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(108, 73);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(195, 20);
            this.txtAccountNumber.TabIndex = 25;
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Location = new System.Drawing.Point(15, 76);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(87, 13);
            this.lblAccountNumber.TabIndex = 26;
            this.lblAccountNumber.Text = "Account Number";
            // 
            // txtAccountTicker
            // 
            this.txtAccountTicker.Location = new System.Drawing.Point(108, 99);
            this.txtAccountTicker.MaxLength = 50;
            this.txtAccountTicker.Name = "txtAccountTicker";
            this.txtAccountTicker.Size = new System.Drawing.Size(195, 20);
            this.txtAccountTicker.TabIndex = 25;
            // 
            // lblAccountTicker
            // 
            this.lblAccountTicker.AutoSize = true;
            this.lblAccountTicker.Location = new System.Drawing.Point(15, 102);
            this.lblAccountTicker.Name = "lblAccountTicker";
            this.lblAccountTicker.Size = new System.Drawing.Size(80, 13);
            this.lblAccountTicker.TabIndex = 26;
            this.lblAccountTicker.Text = "Account Ticker";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(324, 93);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(114, 26);
            this.btnAccept.TabIndex = 24;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // grpEdit
            // 
            this.grpEdit.Controls.Add(this.lblAccountID);
            this.grpEdit.Controls.Add(this.lblBroker);
            this.grpEdit.Controls.Add(this.lblAccountNumber);
            this.grpEdit.Controls.Add(this.lblAccountTicker);
            this.grpEdit.Controls.Add(this.txtAccountID);
            this.grpEdit.Controls.Add(this.txtAccountNumber);
            this.grpEdit.Controls.Add(this.txtAccountTicker);
            this.grpEdit.Controls.Add(this.cboBroker);
            this.grpEdit.Controls.Add(this.btnReject);
            this.grpEdit.Controls.Add(this.btnAccept);
            this.grpEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpEdit.Location = new System.Drawing.Point(0, 286);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new System.Drawing.Size(458, 132);
            this.grpEdit.TabIndex = 27;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "Account Edit";
            // 
            // cboBroker
            // 
            this.cboBroker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBroker.FormattingEnabled = true;
            this.cboBroker.Location = new System.Drawing.Point(108, 46);
            this.cboBroker.Name = "cboBroker";
            this.cboBroker.Size = new System.Drawing.Size(195, 21);
            this.cboBroker.TabIndex = 27;
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(324, 61);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(114, 26);
            this.btnReject.TabIndex = 24;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // frmAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 418);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.grpEdit);
            this.Name = "frmAccounts";
            this.Text = "Accounts";
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditData)).EndInit();
            this.grpEdit.ResumeLayout(false);
            this.grpEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyXtraGrid.MyGridControl dtgData;
        private MyXtraGrid.MyGridView dgEditData;
        private System.Windows.Forms.TextBox txtAccountID;
        private System.Windows.Forms.Label lblAccountID;
        private System.Windows.Forms.Label lblBroker;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.TextBox txtAccountTicker;
        private System.Windows.Forms.Label lblAccountTicker;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.GroupBox grpEdit;
        private System.Windows.Forms.ComboBox cboBroker;
        private System.Windows.Forms.Button btnReject;
    }
}