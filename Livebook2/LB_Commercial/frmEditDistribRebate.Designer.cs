namespace LiveBook
{
    partial class frmEditDistribRebate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditDistribRebate));
            this.button2 = new System.Windows.Forms.Button();
            this.txtId_Distributor = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtgEditData = new MyXtraGrid.MyGridControl();
            this.dgEditData = new MyXtraGrid.MyGridView();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtId_Portfolio = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgEditData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditData)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(449, 253);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 28);
            this.button2.TabIndex = 13;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtId_Distributor
            // 
            this.txtId_Distributor.Location = new System.Drawing.Point(81, 12);
            this.txtId_Distributor.Name = "txtId_Distributor";
            this.txtId_Distributor.Size = new System.Drawing.Size(85, 20);
            this.txtId_Distributor.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Distributor:";
            // 
            // dtgEditData
            // 
            this.dtgEditData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgEditData.Location = new System.Drawing.Point(12, 38);
            this.dtgEditData.LookAndFeel.SkinName = "Blue";
            this.dtgEditData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgEditData.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgEditData.MainView = this.dgEditData;
            this.dtgEditData.Name = "dtgEditData";
            this.dtgEditData.Size = new System.Drawing.Size(529, 209);
            this.dtgEditData.TabIndex = 22;
            this.dtgEditData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgEditData});
            // 
            // dgEditData
            // 
            this.dgEditData.GridControl = this.dtgEditData;
            this.dgEditData.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgEditData.Name = "dgEditData";
            this.dgEditData.OptionsSelection.MultiSelect = true;
            this.dgEditData.OptionsView.ShowGroupPanel = false;
            this.dgEditData.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.dgEditData_CellValueChanged);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(12, 253);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(92, 28);
            this.cmdAdd.TabIndex = 23;
            this.cmdAdd.Text = "Add New";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(110, 253);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(92, 28);
            this.cmdDelete.TabIndex = 24;
            this.cmdDelete.Text = "Delete Selected";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Portfolio:";
            // 
            // txtId_Portfolio
            // 
            this.txtId_Portfolio.Location = new System.Drawing.Point(268, 10);
            this.txtId_Portfolio.Name = "txtId_Portfolio";
            this.txtId_Portfolio.Size = new System.Drawing.Size(85, 20);
            this.txtId_Portfolio.TabIndex = 25;
            // 
            // frmEditDistribRebate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(553, 293);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtId_Portfolio);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.dtgEditData);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtId_Distributor);
            this.Controls.Add(this.button2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditDistribRebate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit Distributor Rebates";
            this.Load += new System.EventHandler(this.frmStock_Loan_Close_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgEditData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox txtId_Distributor;
        private System.Windows.Forms.Label label8;
        private MyXtraGrid.MyGridControl dtgEditData;
        private MyXtraGrid.MyGridView dgEditData;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtId_Portfolio;
    }
}