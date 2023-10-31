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
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbBroker = new System.Windows.Forms.ComboBox();
            this.txtAccountTicker = new System.Windows.Forms.TextBox();
            this.txtAccountNumber = new System.Windows.Forms.TextBox();
            this.txtIdAccount = new System.Windows.Forms.TextBox();
            this.lblAccountTicker = new System.Windows.Forms.Label();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.lblBroker = new System.Windows.Forms.Label();
            this.lblAccountID = new System.Windows.Forms.Label();
            this.cmbPortfolio = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditData)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgData
            // 
            this.dtgData.Location = new System.Drawing.Point(3, 34);
            this.dtgData.LookAndFeel.SkinName = "Blue";
            this.dtgData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgData.MainView = this.dgEditData;
            this.dtgData.Name = "dtgData";
            this.dtgData.Size = new System.Drawing.Size(465, 283);
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
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(335, 369);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(114, 26);
            this.btnUpdate.TabIndex = 24;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(335, 401);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(114, 26);
            this.btnClear.TabIndex = 24;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // cmbBroker
            // 
            this.cmbBroker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBroker.FormattingEnabled = true;
            this.cmbBroker.Location = new System.Drawing.Point(105, 354);
            this.cmbBroker.Name = "cmbBroker";
            this.cmbBroker.Size = new System.Drawing.Size(224, 21);
            this.cmbBroker.TabIndex = 27;
            this.cmbBroker.SelectedIndexChanged += new System.EventHandler(this.cmbBroker_SelectedIndexChanged);
            // 
            // txtAccountTicker
            // 
            this.txtAccountTicker.Location = new System.Drawing.Point(105, 407);
            this.txtAccountTicker.MaxLength = 50;
            this.txtAccountTicker.Name = "txtAccountTicker";
            this.txtAccountTicker.Size = new System.Drawing.Size(224, 20);
            this.txtAccountTicker.TabIndex = 25;
            this.txtAccountTicker.TextChanged += new System.EventHandler(this.txtAccountTicker_TextChanged);
            // 
            // txtAccountNumber
            // 
            this.txtAccountNumber.Location = new System.Drawing.Point(105, 381);
            this.txtAccountNumber.Name = "txtAccountNumber";
            this.txtAccountNumber.Size = new System.Drawing.Size(224, 20);
            this.txtAccountNumber.TabIndex = 25;
            this.txtAccountNumber.TextChanged += new System.EventHandler(this.txtAccountNumber_TextChanged);
            // 
            // txtIdAccount
            // 
            this.txtIdAccount.BackColor = System.Drawing.Color.LightGray;
            this.txtIdAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIdAccount.Location = new System.Drawing.Point(105, 331);
            this.txtIdAccount.Name = "txtIdAccount";
            this.txtIdAccount.ReadOnly = true;
            this.txtIdAccount.Size = new System.Drawing.Size(224, 20);
            this.txtIdAccount.TabIndex = 25;
            this.txtIdAccount.TextChanged += new System.EventHandler(this.txtIdAccount_TextChanged);
            // 
            // lblAccountTicker
            // 
            this.lblAccountTicker.AutoSize = true;
            this.lblAccountTicker.Location = new System.Drawing.Point(12, 410);
            this.lblAccountTicker.Name = "lblAccountTicker";
            this.lblAccountTicker.Size = new System.Drawing.Size(80, 13);
            this.lblAccountTicker.TabIndex = 26;
            this.lblAccountTicker.Text = "Account Ticker";
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Location = new System.Drawing.Point(12, 384);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(87, 13);
            this.lblAccountNumber.TabIndex = 26;
            this.lblAccountNumber.Text = "Account Number";
            // 
            // lblBroker
            // 
            this.lblBroker.AutoSize = true;
            this.lblBroker.Location = new System.Drawing.Point(12, 357);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(38, 13);
            this.lblBroker.TabIndex = 26;
            this.lblBroker.Text = "Broker";
            // 
            // lblAccountID
            // 
            this.lblAccountID.AutoSize = true;
            this.lblAccountID.Location = new System.Drawing.Point(12, 331);
            this.lblAccountID.Name = "lblAccountID";
            this.lblAccountID.Size = new System.Drawing.Size(61, 13);
            this.lblAccountID.TabIndex = 26;
            this.lblAccountID.Text = "Account ID";
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.Location = new System.Drawing.Point(15, 7);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(152, 21);
            this.cmbPortfolio.TabIndex = 28;
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            // 
            // frmAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(469, 435);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.lblAccountID);
            this.Controls.Add(this.lblBroker);
            this.Controls.Add(this.dtgData);
            this.Controls.Add(this.lblAccountNumber);
            this.Controls.Add(this.lblAccountTicker);
            this.Controls.Add(this.cmbBroker);
            this.Controls.Add(this.txtIdAccount);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtAccountNumber);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.txtAccountTicker);
            this.Name = "frmAccounts";
            this.Text = "Accounts";
            ((System.ComponentModel.ISupportInitialize)(this.dtgData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtgData;
        private MyXtraGrid.MyGridView dgEditData;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cmbBroker;
        private System.Windows.Forms.TextBox txtAccountTicker;
        private System.Windows.Forms.TextBox txtAccountNumber;
        private System.Windows.Forms.TextBox txtIdAccount;
        private System.Windows.Forms.Label lblAccountTicker;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.Label lblBroker;
        private System.Windows.Forms.Label lblAccountID;
        private System.Windows.Forms.ComboBox cmbPortfolio;
    }
}