namespace LiveBook
{
    partial class frmCheckPortfolioAdministrator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCheckPortfolioAdministrator));
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtgDiferencesAdmin1 = new MyXtraGrid.MyGridControl();
            this.dgDiferencesAdmin1 = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFund = new LiveBook.NestPortCombo();
            this.cmdDiferences = new System.Windows.Forms.Button();
            this.webMellon = new System.Windows.Forms.WebBrowser();
            this.txtNAVDiff = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNAVLivebook = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNAVAdmin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNAVTableLivebook = new System.Windows.Forms.TextBox();
            this.txtNAVFile = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBD_Diff = new System.Windows.Forms.TextBox();
            this.txtBD_Admin = new System.Windows.Forms.TextBox();
            this.txtBD_LB = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCX_Diff = new System.Windows.Forms.TextBox();
            this.txtCX_Admin = new System.Windows.Forms.TextBox();
            this.txtCX_LB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFW_Diff = new System.Windows.Forms.TextBox();
            this.txtFW_Admin = new System.Windows.Forms.TextBox();
            this.txtFW_LB = new System.Windows.Forms.TextBox();
            this.txtOP_Diff = new System.Windows.Forms.TextBox();
            this.txtOP_Admin = new System.Windows.Forms.TextBox();
            this.txtOP_LB = new System.Windows.Forms.TextBox();
            this.txtEQ_Diff = new System.Windows.Forms.TextBox();
            this.txtEQ_Admin = new System.Windows.Forms.TextBox();
            this.txtEQ_LB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.radEquities = new System.Windows.Forms.RadioButton();
            this.radCash = new System.Windows.Forms.RadioButton();
            this.radForwards = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblImportFile = new System.Windows.Forms.Label();
            this.splitMellon = new System.Windows.Forms.SplitContainer();
            this.chkViewMellon = new System.Windows.Forms.CheckBox();
            this.cmdEditMellon = new System.Windows.Forms.Button();
            this.cmdPartialRecalc = new System.Windows.Forms.Button();
            this.cmdFinished = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBrok_Fut_DifPerc = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtBrok_Fut_Diff = new System.Windows.Forms.TextBox();
            this.txtBrok_Fut_Admin = new System.Windows.Forms.TextBox();
            this.txtBrok_Fut_LB = new System.Windows.Forms.TextBox();
            this.txtPercDif_Loan = new System.Windows.Forms.TextBox();
            this.lblStockLoan = new System.Windows.Forms.Label();
            this.txtStockLoand_Dif = new System.Windows.Forms.TextBox();
            this.txtStockLoand_Admin = new System.Windows.Forms.TextBox();
            this.txtStockLoand_LB = new System.Windows.Forms.TextBox();
            this.txtPercDif_Expense = new System.Windows.Forms.TextBox();
            this.lblExpenses = new System.Windows.Forms.Label();
            this.txtExpense_Dif = new System.Windows.Forms.TextBox();
            this.txtExpense_Admin = new System.Windows.Forms.TextBox();
            this.txtExpense_LB = new System.Windows.Forms.TextBox();
            this.txtPercDif_Brokerage = new System.Windows.Forms.TextBox();
            this.lblBrokerage = new System.Windows.Forms.Label();
            this.txtBrokerage_Dif = new System.Windows.Forms.TextBox();
            this.txtBrokerage_Admin = new System.Windows.Forms.TextBox();
            this.txtBrokerage_LB = new System.Windows.Forms.TextBox();
            this.txtPercDif_Funds = new System.Windows.Forms.TextBox();
            this.txtPercDif_Divid = new System.Windows.Forms.TextBox();
            this.txtPercDif_Futures = new System.Windows.Forms.TextBox();
            this.txtPercDif_Bonds = new System.Windows.Forms.TextBox();
            this.txtPercDif_Cash = new System.Windows.Forms.TextBox();
            this.txtPercDif_FW = new System.Windows.Forms.TextBox();
            this.txtPercDif_Options = new System.Windows.Forms.TextBox();
            this.txtPercDif_Equities = new System.Windows.Forms.TextBox();
            this.txtPercDif_Total = new System.Windows.Forms.TextBox();
            this.lblFunds = new System.Windows.Forms.Label();
            this.txtFunds_Dif = new System.Windows.Forms.TextBox();
            this.txtFunds_Admin = new System.Windows.Forms.TextBox();
            this.txtFunds_LB = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtDividends_Dif = new System.Windows.Forms.TextBox();
            this.txtDividends_Admin = new System.Windows.Forms.TextBox();
            this.txtDividends_LB = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtFutures_Dif = new System.Windows.Forms.TextBox();
            this.txtFutures_Admin = new System.Windows.Forms.TextBox();
            this.txtFutures_LB = new System.Windows.Forms.TextBox();
            this.txtFileNAV_LB = new System.Windows.Forms.TextBox();
            this.txtFileNAV_Adm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBonds_Dif = new System.Windows.Forms.TextBox();
            this.txtBonds_Adm = new System.Windows.Forms.TextBox();
            this.txtBonds_LB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCashOther_Dif = new System.Windows.Forms.TextBox();
            this.txtCashOther_Adm = new System.Windows.Forms.TextBox();
            this.txtCashOther_LB = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtFoward_Dif = new System.Windows.Forms.TextBox();
            this.txtFoward_Adm = new System.Windows.Forms.TextBox();
            this.txtFoward_LB = new System.Windows.Forms.TextBox();
            this.txtOptions_Dif = new System.Windows.Forms.TextBox();
            this.txtOptions_Adm = new System.Windows.Forms.TextBox();
            this.txtOptions_LB = new System.Windows.Forms.TextBox();
            this.txtEquities_Dif = new System.Windows.Forms.TextBox();
            this.txtEquities_Adm = new System.Windows.Forms.TextBox();
            this.txtEquities_LB = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTotal_Dif = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTotal_Adm = new System.Windows.Forms.TextBox();
            this.txtTotal_LB = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmdInsertPrice = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCheckPerfFile = new System.Windows.Forms.TextBox();
            this.txtCheckPerfCalc = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.chkMerge = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtnavUsd = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtNavBrl = new System.Windows.Forms.TextBox();
            this.txtNavTotal = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtNavShare = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferencesAdmin1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiferencesAdmin1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMellon)).BeginInit();
            this.splitMellon.Panel1.SuspendLayout();
            this.splitMellon.Panel2.SuspendLayout();
            this.splitMellon.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(139, 26);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDate.TabIndex = 71;
            this.dtpDate.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(145, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 72;
            this.label6.Text = "Date";
            // 
            // dtgDiferencesAdmin1
            // 
            this.dtgDiferencesAdmin1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDiferencesAdmin1.Location = new System.Drawing.Point(4, 3);
            this.dtgDiferencesAdmin1.LookAndFeel.SkinName = "Blue";
            this.dtgDiferencesAdmin1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgDiferencesAdmin1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgDiferencesAdmin1.MainView = this.dgDiferencesAdmin1;
            this.dtgDiferencesAdmin1.Name = "dtgDiferencesAdmin1";
            this.dtgDiferencesAdmin1.Size = new System.Drawing.Size(1235, 293);
            this.dtgDiferencesAdmin1.TabIndex = 0;
            this.dtgDiferencesAdmin1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgDiferencesAdmin1});
            // 
            // dgDiferencesAdmin1
            // 
            this.dgDiferencesAdmin1.GridControl = this.dtgDiferencesAdmin1;
            this.dgDiferencesAdmin1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgDiferencesAdmin1.Name = "dgDiferencesAdmin1";
            this.dgDiferencesAdmin1.OptionsBehavior.Editable = false;
            this.dgDiferencesAdmin1.OptionsSelection.MultiSelect = true;
            this.dgDiferencesAdmin1.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.Default_DragObjectDrop);
            this.dgDiferencesAdmin1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgDiferencesAdmin1_CustomDrawCell);
            this.dgDiferencesAdmin1.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgDefault_CustomDrawGroupRow);
            this.dgDiferencesAdmin1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgDiferencesAdmin1_RowStyle);
            this.dgDiferencesAdmin1.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.Default_ColumnWidthChanged);
            this.dgDiferencesAdmin1.DoubleClick += new System.EventHandler(this.dgDiferencesAdmin1_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 59;
            this.label1.Text = "Fund";
            // 
            // cmbFund
            // 
            this.cmbFund.DisplayMember = "Port_Name";
            this.cmbFund.FormattingEnabled = true;
            this.cmbFund.IdPortType = 2;
            this.cmbFund.includeAllPortsOption = false;
            this.cmbFund.includeMHConsolOption = false;
            this.cmbFund.Location = new System.Drawing.Point(12, 25);
            this.cmbFund.Name = "cmbFund";
            this.cmbFund.Size = new System.Drawing.Size(121, 21);
            this.cmbFund.TabIndex = 60;
            this.cmbFund.ValueMember = "Id_Portfolio";
            // 
            // cmdDiferences
            // 
            this.cmdDiferences.Location = new System.Drawing.Point(245, 24);
            this.cmdDiferences.Name = "cmdDiferences";
            this.cmdDiferences.Size = new System.Drawing.Size(85, 21);
            this.cmdDiferences.TabIndex = 73;
            this.cmdDiferences.Text = "Load";
            this.cmdDiferences.UseVisualStyleBackColor = true;
            this.cmdDiferences.Click += new System.EventHandler(this.cmdDiferences_Click);
            // 
            // webMellon
            // 
            this.webMellon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webMellon.Location = new System.Drawing.Point(4, 3);
            this.webMellon.MinimumSize = new System.Drawing.Size(20, 20);
            this.webMellon.Name = "webMellon";
            this.webMellon.Size = new System.Drawing.Size(1236, 20);
            this.webMellon.TabIndex = 84;
            // 
            // txtNAVDiff
            // 
            this.txtNAVDiff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVDiff.Location = new System.Drawing.Point(285, 160);
            this.txtNAVDiff.Name = "txtNAVDiff";
            this.txtNAVDiff.Size = new System.Drawing.Size(89, 13);
            this.txtNAVDiff.TabIndex = 11;
            this.txtNAVDiff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(305, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 19);
            this.label8.TabIndex = 10;
            this.label8.Text = "Diference";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNAVLivebook
            // 
            this.txtNAVLivebook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVLivebook.Location = new System.Drawing.Point(188, 160);
            this.txtNAVLivebook.Name = "txtNAVLivebook";
            this.txtNAVLivebook.Size = new System.Drawing.Size(89, 13);
            this.txtNAVLivebook.TabIndex = 9;
            this.txtNAVLivebook.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(199, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 19);
            this.label7.TabIndex = 8;
            this.label7.Text = "LiveBook";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtNAVAdmin
            // 
            this.txtNAVAdmin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVAdmin.Location = new System.Drawing.Point(93, 160);
            this.txtNAVAdmin.Name = "txtNAVAdmin";
            this.txtNAVAdmin.Size = new System.Drawing.Size(89, 13);
            this.txtNAVAdmin.TabIndex = 7;
            this.txtNAVAdmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(93, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Admin";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtNAVTableLivebook);
            this.groupBox2.Controls.Add(this.txtNAVFile);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtBD_Diff);
            this.groupBox2.Controls.Add(this.txtBD_Admin);
            this.groupBox2.Controls.Add(this.txtBD_LB);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtCX_Diff);
            this.groupBox2.Controls.Add(this.txtCX_Admin);
            this.groupBox2.Controls.Add(this.txtCX_LB);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtFW_Diff);
            this.groupBox2.Controls.Add(this.txtFW_Admin);
            this.groupBox2.Controls.Add(this.txtFW_LB);
            this.groupBox2.Controls.Add(this.txtOP_Diff);
            this.groupBox2.Controls.Add(this.txtOP_Admin);
            this.groupBox2.Controls.Add(this.txtOP_LB);
            this.groupBox2.Controls.Add(this.txtEQ_Diff);
            this.groupBox2.Controls.Add(this.txtEQ_Admin);
            this.groupBox2.Controls.Add(this.txtEQ_LB);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtNAVDiff);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtNAVAdmin);
            this.groupBox2.Controls.Add(this.txtNAVLivebook);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 211);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comparison";
            // 
            // txtNAVTableLivebook
            // 
            this.txtNAVTableLivebook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVTableLivebook.Location = new System.Drawing.Point(188, 186);
            this.txtNAVTableLivebook.Name = "txtNAVTableLivebook";
            this.txtNAVTableLivebook.Size = new System.Drawing.Size(89, 13);
            this.txtNAVTableLivebook.TabIndex = 37;
            this.txtNAVTableLivebook.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNAVFile
            // 
            this.txtNAVFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNAVFile.Location = new System.Drawing.Point(93, 186);
            this.txtNAVFile.Name = "txtNAVFile";
            this.txtNAVFile.Size = new System.Drawing.Size(89, 13);
            this.txtNAVFile.TabIndex = 36;
            this.txtNAVFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 189);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "File NAV";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 163);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "Total";
            // 
            // txtBD_Diff
            // 
            this.txtBD_Diff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBD_Diff.Location = new System.Drawing.Point(285, 55);
            this.txtBD_Diff.Name = "txtBD_Diff";
            this.txtBD_Diff.Size = new System.Drawing.Size(89, 13);
            this.txtBD_Diff.TabIndex = 32;
            this.txtBD_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBD_Admin
            // 
            this.txtBD_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBD_Admin.Location = new System.Drawing.Point(93, 55);
            this.txtBD_Admin.Name = "txtBD_Admin";
            this.txtBD_Admin.Size = new System.Drawing.Size(89, 13);
            this.txtBD_Admin.TabIndex = 30;
            this.txtBD_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBD_LB
            // 
            this.txtBD_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBD_LB.Location = new System.Drawing.Point(188, 55);
            this.txtBD_LB.Name = "txtBD_LB";
            this.txtBD_LB.Size = new System.Drawing.Size(89, 13);
            this.txtBD_LB.TabIndex = 31;
            this.txtBD_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 136);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Cash+Other";
            // 
            // txtCX_Diff
            // 
            this.txtCX_Diff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCX_Diff.Location = new System.Drawing.Point(285, 133);
            this.txtCX_Diff.Name = "txtCX_Diff";
            this.txtCX_Diff.Size = new System.Drawing.Size(89, 13);
            this.txtCX_Diff.TabIndex = 27;
            this.txtCX_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCX_Admin
            // 
            this.txtCX_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCX_Admin.Location = new System.Drawing.Point(93, 133);
            this.txtCX_Admin.Name = "txtCX_Admin";
            this.txtCX_Admin.Size = new System.Drawing.Size(89, 13);
            this.txtCX_Admin.TabIndex = 25;
            this.txtCX_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCX_LB
            // 
            this.txtCX_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCX_LB.Location = new System.Drawing.Point(188, 133);
            this.txtCX_LB.Name = "txtCX_LB";
            this.txtCX_LB.Size = new System.Drawing.Size(89, 13);
            this.txtCX_LB.TabIndex = 26;
            this.txtCX_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(20, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Forwards";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 84);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Options";
            // 
            // txtFW_Diff
            // 
            this.txtFW_Diff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFW_Diff.Location = new System.Drawing.Point(285, 107);
            this.txtFW_Diff.Name = "txtFW_Diff";
            this.txtFW_Diff.Size = new System.Drawing.Size(89, 13);
            this.txtFW_Diff.TabIndex = 22;
            this.txtFW_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFW_Admin
            // 
            this.txtFW_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFW_Admin.Location = new System.Drawing.Point(93, 107);
            this.txtFW_Admin.Name = "txtFW_Admin";
            this.txtFW_Admin.Size = new System.Drawing.Size(89, 13);
            this.txtFW_Admin.TabIndex = 20;
            this.txtFW_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFW_LB
            // 
            this.txtFW_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFW_LB.Location = new System.Drawing.Point(188, 107);
            this.txtFW_LB.Name = "txtFW_LB";
            this.txtFW_LB.Size = new System.Drawing.Size(89, 13);
            this.txtFW_LB.TabIndex = 21;
            this.txtFW_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOP_Diff
            // 
            this.txtOP_Diff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOP_Diff.Location = new System.Drawing.Point(285, 81);
            this.txtOP_Diff.Name = "txtOP_Diff";
            this.txtOP_Diff.Size = new System.Drawing.Size(89, 13);
            this.txtOP_Diff.TabIndex = 19;
            this.txtOP_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOP_Admin
            // 
            this.txtOP_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOP_Admin.Location = new System.Drawing.Point(93, 81);
            this.txtOP_Admin.Name = "txtOP_Admin";
            this.txtOP_Admin.Size = new System.Drawing.Size(89, 13);
            this.txtOP_Admin.TabIndex = 17;
            this.txtOP_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOP_LB
            // 
            this.txtOP_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOP_LB.Location = new System.Drawing.Point(188, 81);
            this.txtOP_LB.Name = "txtOP_LB";
            this.txtOP_LB.Size = new System.Drawing.Size(89, 13);
            this.txtOP_LB.TabIndex = 18;
            this.txtOP_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEQ_Diff
            // 
            this.txtEQ_Diff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEQ_Diff.Location = new System.Drawing.Point(285, 29);
            this.txtEQ_Diff.Name = "txtEQ_Diff";
            this.txtEQ_Diff.Size = new System.Drawing.Size(89, 13);
            this.txtEQ_Diff.TabIndex = 16;
            this.txtEQ_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEQ_Admin
            // 
            this.txtEQ_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEQ_Admin.Location = new System.Drawing.Point(93, 29);
            this.txtEQ_Admin.Name = "txtEQ_Admin";
            this.txtEQ_Admin.Size = new System.Drawing.Size(89, 13);
            this.txtEQ_Admin.TabIndex = 14;
            this.txtEQ_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEQ_LB
            // 
            this.txtEQ_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEQ_LB.Location = new System.Drawing.Point(188, 29);
            this.txtEQ_LB.Name = "txtEQ_LB";
            this.txtEQ_LB.Size = new System.Drawing.Size(89, 13);
            this.txtEQ_LB.TabIndex = 15;
            this.txtEQ_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Equities";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Bonds";
            // 
            // radEquities
            // 
            this.radEquities.AutoSize = true;
            this.radEquities.Location = new System.Drawing.Point(12, 52);
            this.radEquities.Name = "radEquities";
            this.radEquities.Size = new System.Drawing.Size(155, 17);
            this.radEquities.TabIndex = 79;
            this.radEquities.TabStop = true;
            this.radEquities.Text = "Equities,Options and Bonds";
            this.radEquities.UseVisualStyleBackColor = true;
            this.radEquities.CheckedChanged += new System.EventHandler(this.radEquities_CheckedChanged);
            // 
            // radCash
            // 
            this.radCash.AutoSize = true;
            this.radCash.Location = new System.Drawing.Point(247, 52);
            this.radCash.Name = "radCash";
            this.radCash.Size = new System.Drawing.Size(99, 17);
            this.radCash.TabIndex = 80;
            this.radCash.TabStop = true;
            this.radCash.Text = "Cash and Other";
            this.radCash.UseVisualStyleBackColor = true;
            this.radCash.CheckedChanged += new System.EventHandler(this.radCash_CheckedChanged);
            // 
            // radForwards
            // 
            this.radForwards.AutoSize = true;
            this.radForwards.Location = new System.Drawing.Point(173, 52);
            this.radForwards.Name = "radForwards";
            this.radForwards.Size = new System.Drawing.Size(68, 17);
            this.radForwards.TabIndex = 81;
            this.radForwards.TabStop = true;
            this.radForwards.Text = "Forwards";
            this.radForwards.UseVisualStyleBackColor = true;
            this.radForwards.CheckedChanged += new System.EventHandler(this.radForwards_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(350, 52);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(36, 17);
            this.radAll.TabIndex = 82;
            this.radAll.TabStop = true;
            this.radAll.Text = "All";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.CheckedChanged += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblImportFile);
            this.groupBox3.Location = new System.Drawing.Point(874, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(179, 92);
            this.groupBox3.TabIndex = 83;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import File";
            // 
            // lblImportFile
            // 
            this.lblImportFile.AllowDrop = true;
            this.lblImportFile.Location = new System.Drawing.Point(6, 19);
            this.lblImportFile.Name = "lblImportFile";
            this.lblImportFile.Size = new System.Drawing.Size(165, 68);
            this.lblImportFile.TabIndex = 29;
            this.lblImportFile.Text = "Drag file here to import it into the system";
            this.lblImportFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblImportFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragDrop);
            this.lblImportFile.DragOver += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragOver);
            // 
            // splitMellon
            // 
            this.splitMellon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMellon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitMellon.Location = new System.Drawing.Point(12, 393);
            this.splitMellon.Name = "splitMellon";
            this.splitMellon.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMellon.Panel1
            // 
            this.splitMellon.Panel1.Controls.Add(this.dtgDiferencesAdmin1);
            this.splitMellon.Panel1MinSize = 0;
            // 
            // splitMellon.Panel2
            // 
            this.splitMellon.Panel2.Controls.Add(this.webMellon);
            this.splitMellon.Size = new System.Drawing.Size(1244, 344);
            this.splitMellon.SplitterDistance = 301;
            this.splitMellon.TabIndex = 85;
            // 
            // chkViewMellon
            // 
            this.chkViewMellon.AutoSize = true;
            this.chkViewMellon.Location = new System.Drawing.Point(173, 296);
            this.chkViewMellon.Name = "chkViewMellon";
            this.chkViewMellon.Size = new System.Drawing.Size(109, 17);
            this.chkViewMellon.TabIndex = 86;
            this.chkViewMellon.Text = "View Mellon Data";
            this.chkViewMellon.UseVisualStyleBackColor = true;
            this.chkViewMellon.CheckedChanged += new System.EventHandler(this.chkViewMellon_CheckedChanged);
            // 
            // cmdEditMellon
            // 
            this.cmdEditMellon.Location = new System.Drawing.Point(17, 292);
            this.cmdEditMellon.Name = "cmdEditMellon";
            this.cmdEditMellon.Size = new System.Drawing.Size(150, 23);
            this.cmdEditMellon.TabIndex = 87;
            this.cmdEditMellon.Text = "Edit Mellon Names";
            this.cmdEditMellon.UseVisualStyleBackColor = true;
            this.cmdEditMellon.Click += new System.EventHandler(this.cmdEditMellon_Click);
            // 
            // cmdPartialRecalc
            // 
            this.cmdPartialRecalc.Location = new System.Drawing.Point(1087, 146);
            this.cmdPartialRecalc.Name = "cmdPartialRecalc";
            this.cmdPartialRecalc.Size = new System.Drawing.Size(100, 31);
            this.cmdPartialRecalc.TabIndex = 88;
            this.cmdPartialRecalc.Text = "Partial Recalc";
            this.cmdPartialRecalc.UseVisualStyleBackColor = true;
            this.cmdPartialRecalc.Click += new System.EventHandler(this.cmdPartialRecalc_Click);
            // 
            // cmdFinished
            // 
            this.cmdFinished.Location = new System.Drawing.Point(1087, 222);
            this.cmdFinished.Name = "cmdFinished";
            this.cmdFinished.Size = new System.Drawing.Size(100, 29);
            this.cmdFinished.TabIndex = 89;
            this.cmdFinished.Text = "Finished";
            this.cmdFinished.UseVisualStyleBackColor = true;
            this.cmdFinished.Click += new System.EventHandler(this.cmdFinished_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBrok_Fut_DifPerc);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.txtBrok_Fut_Diff);
            this.groupBox1.Controls.Add(this.txtBrok_Fut_Admin);
            this.groupBox1.Controls.Add(this.txtBrok_Fut_LB);
            this.groupBox1.Controls.Add(this.txtPercDif_Loan);
            this.groupBox1.Controls.Add(this.lblStockLoan);
            this.groupBox1.Controls.Add(this.txtStockLoand_Dif);
            this.groupBox1.Controls.Add(this.txtStockLoand_Admin);
            this.groupBox1.Controls.Add(this.txtStockLoand_LB);
            this.groupBox1.Controls.Add(this.txtPercDif_Expense);
            this.groupBox1.Controls.Add(this.lblExpenses);
            this.groupBox1.Controls.Add(this.txtExpense_Dif);
            this.groupBox1.Controls.Add(this.txtExpense_Admin);
            this.groupBox1.Controls.Add(this.txtExpense_LB);
            this.groupBox1.Controls.Add(this.txtPercDif_Brokerage);
            this.groupBox1.Controls.Add(this.lblBrokerage);
            this.groupBox1.Controls.Add(this.txtBrokerage_Dif);
            this.groupBox1.Controls.Add(this.txtBrokerage_Admin);
            this.groupBox1.Controls.Add(this.txtBrokerage_LB);
            this.groupBox1.Controls.Add(this.txtPercDif_Funds);
            this.groupBox1.Controls.Add(this.txtPercDif_Divid);
            this.groupBox1.Controls.Add(this.txtPercDif_Futures);
            this.groupBox1.Controls.Add(this.txtPercDif_Bonds);
            this.groupBox1.Controls.Add(this.txtPercDif_Cash);
            this.groupBox1.Controls.Add(this.txtPercDif_FW);
            this.groupBox1.Controls.Add(this.txtPercDif_Options);
            this.groupBox1.Controls.Add(this.txtPercDif_Equities);
            this.groupBox1.Controls.Add(this.txtPercDif_Total);
            this.groupBox1.Controls.Add(this.lblFunds);
            this.groupBox1.Controls.Add(this.txtFunds_Dif);
            this.groupBox1.Controls.Add(this.txtFunds_Admin);
            this.groupBox1.Controls.Add(this.txtFunds_LB);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.txtDividends_Dif);
            this.groupBox1.Controls.Add(this.txtDividends_Admin);
            this.groupBox1.Controls.Add(this.txtDividends_LB);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.txtFutures_Dif);
            this.groupBox1.Controls.Add(this.txtFutures_Admin);
            this.groupBox1.Controls.Add(this.txtFutures_LB);
            this.groupBox1.Controls.Add(this.txtFileNAV_LB);
            this.groupBox1.Controls.Add(this.txtFileNAV_Adm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtBonds_Dif);
            this.groupBox1.Controls.Add(this.txtBonds_Adm);
            this.groupBox1.Controls.Add(this.txtBonds_LB);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCashOther_Dif);
            this.groupBox1.Controls.Add(this.txtCashOther_Adm);
            this.groupBox1.Controls.Add(this.txtCashOther_LB);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtFoward_Dif);
            this.groupBox1.Controls.Add(this.txtFoward_Adm);
            this.groupBox1.Controls.Add(this.txtFoward_LB);
            this.groupBox1.Controls.Add(this.txtOptions_Dif);
            this.groupBox1.Controls.Add(this.txtOptions_Adm);
            this.groupBox1.Controls.Add(this.txtOptions_LB);
            this.groupBox1.Controls.Add(this.txtEquities_Dif);
            this.groupBox1.Controls.Add(this.txtEquities_Adm);
            this.groupBox1.Controls.Add(this.txtEquities_LB);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtTotal_Dif);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.txtTotal_Adm);
            this.groupBox1.Controls.Add(this.txtTotal_LB);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Location = new System.Drawing.Point(445, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 375);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Performance";
            // 
            // txtBrok_Fut_DifPerc
            // 
            this.txtBrok_Fut_DifPerc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrok_Fut_DifPerc.Location = new System.Drawing.Point(332, 207);
            this.txtBrok_Fut_DifPerc.Name = "txtBrok_Fut_DifPerc";
            this.txtBrok_Fut_DifPerc.Size = new System.Drawing.Size(77, 13);
            this.txtBrok_Fut_DifPerc.TabIndex = 131;
            this.txtBrok_Fut_DifPerc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(7, 210);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(74, 13);
            this.label30.TabIndex = 130;
            this.label30.Text = "Brokerage Fut";
            // 
            // txtBrok_Fut_Diff
            // 
            this.txtBrok_Fut_Diff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrok_Fut_Diff.Location = new System.Drawing.Point(249, 207);
            this.txtBrok_Fut_Diff.Name = "txtBrok_Fut_Diff";
            this.txtBrok_Fut_Diff.Size = new System.Drawing.Size(77, 13);
            this.txtBrok_Fut_Diff.TabIndex = 129;
            this.txtBrok_Fut_Diff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBrok_Fut_Admin
            // 
            this.txtBrok_Fut_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrok_Fut_Admin.Location = new System.Drawing.Point(80, 207);
            this.txtBrok_Fut_Admin.Name = "txtBrok_Fut_Admin";
            this.txtBrok_Fut_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtBrok_Fut_Admin.TabIndex = 127;
            this.txtBrok_Fut_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBrok_Fut_LB
            // 
            this.txtBrok_Fut_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrok_Fut_LB.Location = new System.Drawing.Point(166, 207);
            this.txtBrok_Fut_LB.Name = "txtBrok_Fut_LB";
            this.txtBrok_Fut_LB.Size = new System.Drawing.Size(77, 13);
            this.txtBrok_Fut_LB.TabIndex = 128;
            this.txtBrok_Fut_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Loan
            // 
            this.txtPercDif_Loan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Loan.Location = new System.Drawing.Point(331, 252);
            this.txtPercDif_Loan.Name = "txtPercDif_Loan";
            this.txtPercDif_Loan.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Loan.TabIndex = 126;
            this.txtPercDif_Loan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblStockLoan
            // 
            this.lblStockLoan.AutoSize = true;
            this.lblStockLoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockLoan.ForeColor = System.Drawing.Color.Blue;
            this.lblStockLoan.Location = new System.Drawing.Point(6, 255);
            this.lblStockLoan.Name = "lblStockLoan";
            this.lblStockLoan.Size = new System.Drawing.Size(62, 13);
            this.lblStockLoan.TabIndex = 125;
            this.lblStockLoan.Text = "Stock Loan";
            this.lblStockLoan.Click += new System.EventHandler(this.lblStockLoan_Click);
            // 
            // txtStockLoand_Dif
            // 
            this.txtStockLoand_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStockLoand_Dif.Location = new System.Drawing.Point(248, 252);
            this.txtStockLoand_Dif.Name = "txtStockLoand_Dif";
            this.txtStockLoand_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtStockLoand_Dif.TabIndex = 124;
            this.txtStockLoand_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStockLoand_Admin
            // 
            this.txtStockLoand_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStockLoand_Admin.Location = new System.Drawing.Point(79, 252);
            this.txtStockLoand_Admin.Name = "txtStockLoand_Admin";
            this.txtStockLoand_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtStockLoand_Admin.TabIndex = 122;
            this.txtStockLoand_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStockLoand_LB
            // 
            this.txtStockLoand_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStockLoand_LB.Location = new System.Drawing.Point(165, 252);
            this.txtStockLoand_LB.Name = "txtStockLoand_LB";
            this.txtStockLoand_LB.Size = new System.Drawing.Size(77, 13);
            this.txtStockLoand_LB.TabIndex = 123;
            this.txtStockLoand_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Expense
            // 
            this.txtPercDif_Expense.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Expense.Location = new System.Drawing.Point(331, 230);
            this.txtPercDif_Expense.Name = "txtPercDif_Expense";
            this.txtPercDif_Expense.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Expense.TabIndex = 121;
            this.txtPercDif_Expense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblExpenses
            // 
            this.lblExpenses.AutoSize = true;
            this.lblExpenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpenses.ForeColor = System.Drawing.Color.Blue;
            this.lblExpenses.Location = new System.Drawing.Point(6, 233);
            this.lblExpenses.Name = "lblExpenses";
            this.lblExpenses.Size = new System.Drawing.Size(53, 13);
            this.lblExpenses.TabIndex = 120;
            this.lblExpenses.Text = "Expenses";
            this.lblExpenses.Click += new System.EventHandler(this.lblExpenses_Click);
            // 
            // txtExpense_Dif
            // 
            this.txtExpense_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExpense_Dif.Location = new System.Drawing.Point(248, 230);
            this.txtExpense_Dif.Name = "txtExpense_Dif";
            this.txtExpense_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtExpense_Dif.TabIndex = 119;
            this.txtExpense_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtExpense_Admin
            // 
            this.txtExpense_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExpense_Admin.Location = new System.Drawing.Point(79, 230);
            this.txtExpense_Admin.Name = "txtExpense_Admin";
            this.txtExpense_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtExpense_Admin.TabIndex = 117;
            this.txtExpense_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtExpense_LB
            // 
            this.txtExpense_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtExpense_LB.Location = new System.Drawing.Point(166, 230);
            this.txtExpense_LB.Name = "txtExpense_LB";
            this.txtExpense_LB.Size = new System.Drawing.Size(77, 13);
            this.txtExpense_LB.TabIndex = 118;
            this.txtExpense_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Brokerage
            // 
            this.txtPercDif_Brokerage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Brokerage.Location = new System.Drawing.Point(332, 186);
            this.txtPercDif_Brokerage.Name = "txtPercDif_Brokerage";
            this.txtPercDif_Brokerage.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Brokerage.TabIndex = 116;
            this.txtPercDif_Brokerage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblBrokerage
            // 
            this.lblBrokerage.AutoSize = true;
            this.lblBrokerage.Location = new System.Drawing.Point(7, 189);
            this.lblBrokerage.Name = "lblBrokerage";
            this.lblBrokerage.Size = new System.Drawing.Size(56, 13);
            this.lblBrokerage.TabIndex = 115;
            this.lblBrokerage.Text = "Brokerage";
            // 
            // txtBrokerage_Dif
            // 
            this.txtBrokerage_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrokerage_Dif.Location = new System.Drawing.Point(249, 186);
            this.txtBrokerage_Dif.Name = "txtBrokerage_Dif";
            this.txtBrokerage_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtBrokerage_Dif.TabIndex = 114;
            this.txtBrokerage_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBrokerage_Admin
            // 
            this.txtBrokerage_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrokerage_Admin.Location = new System.Drawing.Point(80, 186);
            this.txtBrokerage_Admin.Name = "txtBrokerage_Admin";
            this.txtBrokerage_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtBrokerage_Admin.TabIndex = 112;
            this.txtBrokerage_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBrokerage_LB
            // 
            this.txtBrokerage_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrokerage_LB.Location = new System.Drawing.Point(166, 186);
            this.txtBrokerage_LB.Name = "txtBrokerage_LB";
            this.txtBrokerage_LB.Size = new System.Drawing.Size(77, 13);
            this.txtBrokerage_LB.TabIndex = 113;
            this.txtBrokerage_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Funds
            // 
            this.txtPercDif_Funds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Funds.Location = new System.Drawing.Point(331, 275);
            this.txtPercDif_Funds.Name = "txtPercDif_Funds";
            this.txtPercDif_Funds.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Funds.TabIndex = 111;
            this.txtPercDif_Funds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Divid
            // 
            this.txtPercDif_Divid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Divid.Location = new System.Drawing.Point(332, 160);
            this.txtPercDif_Divid.Name = "txtPercDif_Divid";
            this.txtPercDif_Divid.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Divid.TabIndex = 110;
            this.txtPercDif_Divid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Futures
            // 
            this.txtPercDif_Futures.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Futures.Location = new System.Drawing.Point(332, 134);
            this.txtPercDif_Futures.Name = "txtPercDif_Futures";
            this.txtPercDif_Futures.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Futures.TabIndex = 109;
            this.txtPercDif_Futures.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Bonds
            // 
            this.txtPercDif_Bonds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Bonds.Location = new System.Drawing.Point(332, 56);
            this.txtPercDif_Bonds.Name = "txtPercDif_Bonds";
            this.txtPercDif_Bonds.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Bonds.TabIndex = 108;
            this.txtPercDif_Bonds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Cash
            // 
            this.txtPercDif_Cash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Cash.Location = new System.Drawing.Point(331, 301);
            this.txtPercDif_Cash.Name = "txtPercDif_Cash";
            this.txtPercDif_Cash.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Cash.TabIndex = 107;
            this.txtPercDif_Cash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_FW
            // 
            this.txtPercDif_FW.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_FW.Location = new System.Drawing.Point(332, 108);
            this.txtPercDif_FW.Name = "txtPercDif_FW";
            this.txtPercDif_FW.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_FW.TabIndex = 106;
            this.txtPercDif_FW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Options
            // 
            this.txtPercDif_Options.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Options.Location = new System.Drawing.Point(332, 82);
            this.txtPercDif_Options.Name = "txtPercDif_Options";
            this.txtPercDif_Options.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Options.TabIndex = 105;
            this.txtPercDif_Options.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Equities
            // 
            this.txtPercDif_Equities.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Equities.Location = new System.Drawing.Point(332, 30);
            this.txtPercDif_Equities.Name = "txtPercDif_Equities";
            this.txtPercDif_Equities.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Equities.TabIndex = 104;
            this.txtPercDif_Equities.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPercDif_Total
            // 
            this.txtPercDif_Total.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPercDif_Total.Location = new System.Drawing.Point(331, 327);
            this.txtPercDif_Total.Name = "txtPercDif_Total";
            this.txtPercDif_Total.Size = new System.Drawing.Size(77, 13);
            this.txtPercDif_Total.TabIndex = 103;
            this.txtPercDif_Total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblFunds
            // 
            this.lblFunds.AutoSize = true;
            this.lblFunds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFunds.ForeColor = System.Drawing.Color.Blue;
            this.lblFunds.Location = new System.Drawing.Point(6, 278);
            this.lblFunds.Name = "lblFunds";
            this.lblFunds.Size = new System.Drawing.Size(36, 13);
            this.lblFunds.TabIndex = 102;
            this.lblFunds.Text = "Funds";
            this.lblFunds.Click += new System.EventHandler(this.lblFunds_Click);
            // 
            // txtFunds_Dif
            // 
            this.txtFunds_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFunds_Dif.Location = new System.Drawing.Point(248, 275);
            this.txtFunds_Dif.Name = "txtFunds_Dif";
            this.txtFunds_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtFunds_Dif.TabIndex = 101;
            this.txtFunds_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFunds_Admin
            // 
            this.txtFunds_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFunds_Admin.Location = new System.Drawing.Point(79, 275);
            this.txtFunds_Admin.Name = "txtFunds_Admin";
            this.txtFunds_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtFunds_Admin.TabIndex = 99;
            this.txtFunds_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFunds_LB
            // 
            this.txtFunds_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFunds_LB.Location = new System.Drawing.Point(165, 275);
            this.txtFunds_LB.Name = "txtFunds_LB";
            this.txtFunds_LB.Size = new System.Drawing.Size(77, 13);
            this.txtFunds_LB.TabIndex = 100;
            this.txtFunds_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(7, 163);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(54, 13);
            this.label24.TabIndex = 98;
            this.label24.Text = "Dividends";
            // 
            // txtDividends_Dif
            // 
            this.txtDividends_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDividends_Dif.Location = new System.Drawing.Point(249, 160);
            this.txtDividends_Dif.Name = "txtDividends_Dif";
            this.txtDividends_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtDividends_Dif.TabIndex = 97;
            this.txtDividends_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDividends_Admin
            // 
            this.txtDividends_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDividends_Admin.Location = new System.Drawing.Point(80, 160);
            this.txtDividends_Admin.Name = "txtDividends_Admin";
            this.txtDividends_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtDividends_Admin.TabIndex = 95;
            this.txtDividends_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDividends_LB
            // 
            this.txtDividends_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDividends_LB.Location = new System.Drawing.Point(166, 160);
            this.txtDividends_LB.Name = "txtDividends_LB";
            this.txtDividends_LB.Size = new System.Drawing.Size(77, 13);
            this.txtDividends_LB.TabIndex = 96;
            this.txtDividends_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(7, 137);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(42, 13);
            this.label23.TabIndex = 94;
            this.label23.Text = "Futures";
            // 
            // txtFutures_Dif
            // 
            this.txtFutures_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFutures_Dif.Location = new System.Drawing.Point(249, 134);
            this.txtFutures_Dif.Name = "txtFutures_Dif";
            this.txtFutures_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtFutures_Dif.TabIndex = 93;
            this.txtFutures_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFutures_Admin
            // 
            this.txtFutures_Admin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFutures_Admin.Location = new System.Drawing.Point(80, 134);
            this.txtFutures_Admin.Name = "txtFutures_Admin";
            this.txtFutures_Admin.Size = new System.Drawing.Size(77, 13);
            this.txtFutures_Admin.TabIndex = 91;
            this.txtFutures_Admin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFutures_LB
            // 
            this.txtFutures_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFutures_LB.Location = new System.Drawing.Point(166, 134);
            this.txtFutures_LB.Name = "txtFutures_LB";
            this.txtFutures_LB.Size = new System.Drawing.Size(77, 13);
            this.txtFutures_LB.TabIndex = 92;
            this.txtFutures_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFileNAV_LB
            // 
            this.txtFileNAV_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileNAV_LB.Location = new System.Drawing.Point(165, 353);
            this.txtFileNAV_LB.Name = "txtFileNAV_LB";
            this.txtFileNAV_LB.Size = new System.Drawing.Size(77, 13);
            this.txtFileNAV_LB.TabIndex = 67;
            this.txtFileNAV_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFileNAV_Adm
            // 
            this.txtFileNAV_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileNAV_Adm.Location = new System.Drawing.Point(79, 353);
            this.txtFileNAV_Adm.Name = "txtFileNAV_Adm";
            this.txtFileNAV_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtFileNAV_Adm.TabIndex = 66;
            this.txtFileNAV_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 356);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "File Perf.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 330);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 64;
            this.label3.Text = "Total";
            // 
            // txtBonds_Dif
            // 
            this.txtBonds_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBonds_Dif.Location = new System.Drawing.Point(249, 56);
            this.txtBonds_Dif.Name = "txtBonds_Dif";
            this.txtBonds_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtBonds_Dif.TabIndex = 63;
            this.txtBonds_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBonds_Adm
            // 
            this.txtBonds_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBonds_Adm.Location = new System.Drawing.Point(80, 56);
            this.txtBonds_Adm.Name = "txtBonds_Adm";
            this.txtBonds_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtBonds_Adm.TabIndex = 61;
            this.txtBonds_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBonds_LB
            // 
            this.txtBonds_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBonds_LB.Location = new System.Drawing.Point(166, 56);
            this.txtBonds_LB.Name = "txtBonds_LB";
            this.txtBonds_LB.Size = new System.Drawing.Size(77, 13);
            this.txtBonds_LB.TabIndex = 62;
            this.txtBonds_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "Cash+Other";
            // 
            // txtCashOther_Dif
            // 
            this.txtCashOther_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashOther_Dif.Location = new System.Drawing.Point(248, 301);
            this.txtCashOther_Dif.Name = "txtCashOther_Dif";
            this.txtCashOther_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtCashOther_Dif.TabIndex = 59;
            this.txtCashOther_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCashOther_Adm
            // 
            this.txtCashOther_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashOther_Adm.Location = new System.Drawing.Point(79, 301);
            this.txtCashOther_Adm.Name = "txtCashOther_Adm";
            this.txtCashOther_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtCashOther_Adm.TabIndex = 57;
            this.txtCashOther_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCashOther_LB
            // 
            this.txtCashOther_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCashOther_LB.Location = new System.Drawing.Point(165, 301);
            this.txtCashOther_LB.Name = "txtCashOther_LB";
            this.txtCashOther_LB.Size = new System.Drawing.Size(77, 13);
            this.txtCashOther_LB.TabIndex = 58;
            this.txtCashOther_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 56;
            this.label16.Text = "Forwards";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(43, 13);
            this.label17.TabIndex = 55;
            this.label17.Text = "Options";
            // 
            // txtFoward_Dif
            // 
            this.txtFoward_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFoward_Dif.Location = new System.Drawing.Point(249, 108);
            this.txtFoward_Dif.Name = "txtFoward_Dif";
            this.txtFoward_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtFoward_Dif.TabIndex = 54;
            this.txtFoward_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFoward_Adm
            // 
            this.txtFoward_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFoward_Adm.Location = new System.Drawing.Point(80, 108);
            this.txtFoward_Adm.Name = "txtFoward_Adm";
            this.txtFoward_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtFoward_Adm.TabIndex = 52;
            this.txtFoward_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFoward_LB
            // 
            this.txtFoward_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFoward_LB.Location = new System.Drawing.Point(166, 108);
            this.txtFoward_LB.Name = "txtFoward_LB";
            this.txtFoward_LB.Size = new System.Drawing.Size(77, 13);
            this.txtFoward_LB.TabIndex = 53;
            this.txtFoward_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOptions_Dif
            // 
            this.txtOptions_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOptions_Dif.Location = new System.Drawing.Point(249, 82);
            this.txtOptions_Dif.Name = "txtOptions_Dif";
            this.txtOptions_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtOptions_Dif.TabIndex = 51;
            this.txtOptions_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOptions_Adm
            // 
            this.txtOptions_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOptions_Adm.Location = new System.Drawing.Point(80, 82);
            this.txtOptions_Adm.Name = "txtOptions_Adm";
            this.txtOptions_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtOptions_Adm.TabIndex = 49;
            this.txtOptions_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtOptions_LB
            // 
            this.txtOptions_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOptions_LB.Location = new System.Drawing.Point(166, 82);
            this.txtOptions_LB.Name = "txtOptions_LB";
            this.txtOptions_LB.Size = new System.Drawing.Size(77, 13);
            this.txtOptions_LB.TabIndex = 50;
            this.txtOptions_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEquities_Dif
            // 
            this.txtEquities_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEquities_Dif.Location = new System.Drawing.Point(249, 30);
            this.txtEquities_Dif.Name = "txtEquities_Dif";
            this.txtEquities_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtEquities_Dif.TabIndex = 48;
            this.txtEquities_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEquities_Adm
            // 
            this.txtEquities_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEquities_Adm.Location = new System.Drawing.Point(80, 30);
            this.txtEquities_Adm.Name = "txtEquities_Adm";
            this.txtEquities_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtEquities_Adm.TabIndex = 46;
            this.txtEquities_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEquities_LB
            // 
            this.txtEquities_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEquities_LB.Location = new System.Drawing.Point(166, 30);
            this.txtEquities_LB.Name = "txtEquities_LB";
            this.txtEquities_LB.Size = new System.Drawing.Size(77, 13);
            this.txtEquities_LB.TabIndex = 47;
            this.txtEquities_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 33);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Equities";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 59);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(37, 13);
            this.label19.TabIndex = 44;
            this.label19.Text = "Bonds";
            // 
            // txtTotal_Dif
            // 
            this.txtTotal_Dif.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal_Dif.Location = new System.Drawing.Point(248, 327);
            this.txtTotal_Dif.Name = "txtTotal_Dif";
            this.txtTotal_Dif.Size = new System.Drawing.Size(77, 13);
            this.txtTotal_Dif.TabIndex = 43;
            this.txtTotal_Dif.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(77, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 19);
            this.label20.TabIndex = 38;
            this.label20.Text = "Admin";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(235, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(103, 19);
            this.label21.TabIndex = 42;
            this.label21.Text = "Diference";
            this.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtTotal_Adm
            // 
            this.txtTotal_Adm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal_Adm.Location = new System.Drawing.Point(79, 327);
            this.txtTotal_Adm.Name = "txtTotal_Adm";
            this.txtTotal_Adm.Size = new System.Drawing.Size(77, 13);
            this.txtTotal_Adm.TabIndex = 39;
            this.txtTotal_Adm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotal_LB
            // 
            this.txtTotal_LB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal_LB.Location = new System.Drawing.Point(165, 327);
            this.txtTotal_LB.Name = "txtTotal_LB";
            this.txtTotal_LB.Size = new System.Drawing.Size(77, 13);
            this.txtTotal_LB.TabIndex = 41;
            this.txtTotal_LB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(153, 12);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(100, 19);
            this.label22.TabIndex = 40;
            this.label22.Text = "LiveBook";
            this.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmdInsertPrice
            // 
            this.cmdInsertPrice.Enabled = false;
            this.cmdInsertPrice.Location = new System.Drawing.Point(302, 292);
            this.cmdInsertPrice.Name = "cmdInsertPrice";
            this.cmdInsertPrice.Size = new System.Drawing.Size(100, 21);
            this.cmdInsertPrice.TabIndex = 90;
            this.cmdInsertPrice.Text = "Insert BTG Price";
            this.cmdInsertPrice.UseVisualStyleBackColor = true;
            this.cmdInsertPrice.Click += new System.EventHandler(this.cmdInsertPrice_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCheckPerfFile);
            this.groupBox4.Controls.Add(this.txtCheckPerfCalc);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Location = new System.Drawing.Point(874, 116);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(179, 80);
            this.groupBox4.TabIndex = 91;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Check Performance";
            // 
            // txtCheckPerfFile
            // 
            this.txtCheckPerfFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheckPerfFile.Location = new System.Drawing.Point(79, 51);
            this.txtCheckPerfFile.Name = "txtCheckPerfFile";
            this.txtCheckPerfFile.Size = new System.Drawing.Size(77, 13);
            this.txtCheckPerfFile.TabIndex = 71;
            this.txtCheckPerfFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCheckPerfCalc
            // 
            this.txtCheckPerfCalc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCheckPerfCalc.Location = new System.Drawing.Point(79, 25);
            this.txtCheckPerfCalc.Name = "txtCheckPerfCalc";
            this.txtCheckPerfCalc.Size = new System.Drawing.Size(77, 13);
            this.txtCheckPerfCalc.TabIndex = 70;
            this.txtCheckPerfCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 51);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 13);
            this.label25.TabIndex = 69;
            this.label25.Text = "File Perf.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(57, 13);
            this.label26.TabIndex = 68;
            this.label26.Text = "Calculated";
            // 
            // chkMerge
            // 
            this.chkMerge.AutoSize = true;
            this.chkMerge.Location = new System.Drawing.Point(874, 357);
            this.chkMerge.Name = "chkMerge";
            this.chkMerge.Size = new System.Drawing.Size(147, 17);
            this.chkMerge.TabIndex = 92;
            this.chkMerge.Text = "Merge with previous Date";
            this.chkMerge.UseVisualStyleBackColor = true;
            this.chkMerge.CheckedChanged += new System.EventHandler(this.chkMerge_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtnavUsd);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.txtNavBrl);
            this.groupBox5.Controls.Add(this.txtNavTotal);
            this.groupBox5.Location = new System.Drawing.Point(874, 202);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(179, 96);
            this.groupBox5.TabIndex = 93;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "NAV";
            // 
            // txtnavUsd
            // 
            this.txtnavUsd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnavUsd.Location = new System.Drawing.Point(90, 75);
            this.txtnavUsd.Name = "txtnavUsd";
            this.txtnavUsd.Size = new System.Drawing.Size(81, 13);
            this.txtnavUsd.TabIndex = 74;
            this.txtnavUsd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 23);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(56, 13);
            this.label27.TabIndex = 71;
            this.label27.Text = "NAV Total";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 75);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(55, 13);
            this.label28.TabIndex = 73;
            this.label28.Text = "NAV USD";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 49);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(53, 13);
            this.label29.TabIndex = 72;
            this.label29.Text = "NAV BRL";
            // 
            // txtNavBrl
            // 
            this.txtNavBrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavBrl.Location = new System.Drawing.Point(79, 49);
            this.txtNavBrl.Name = "txtNavBrl";
            this.txtNavBrl.Size = new System.Drawing.Size(92, 13);
            this.txtNavBrl.TabIndex = 70;
            this.txtNavBrl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNavTotal
            // 
            this.txtNavTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavTotal.Location = new System.Drawing.Point(79, 23);
            this.txtNavTotal.Name = "txtNavTotal";
            this.txtNavTotal.Size = new System.Drawing.Size(92, 13);
            this.txtNavTotal.TabIndex = 67;
            this.txtNavTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtNavShare);
            this.groupBox6.Location = new System.Drawing.Point(874, 304);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(179, 47);
            this.groupBox6.TabIndex = 94;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "NAV per Share";
            // 
            // txtNavShare
            // 
            this.txtNavShare.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNavShare.Location = new System.Drawing.Point(6, 20);
            this.txtNavShare.Name = "txtNavShare";
            this.txtNavShare.Size = new System.Drawing.Size(92, 13);
            this.txtNavShare.TabIndex = 67;
            this.txtNavShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmCheckPortfolioAdministrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1268, 742);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.chkMerge);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cmdFinished);
            this.Controls.Add(this.cmdPartialRecalc);
            this.Controls.Add(this.cmdEditMellon);
            this.Controls.Add(this.chkViewMellon);
            this.Controls.Add(this.splitMellon);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.radAll);
            this.Controls.Add(this.radForwards);
            this.Controls.Add(this.radCash);
            this.Controls.Add(this.radEquities);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdInsertPrice);
            this.Controls.Add(this.cmdDiferences);
            this.Controls.Add(this.cmbFund);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpDate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCheckPortfolioAdministrator";
            this.Text = "Portfolio Reconciliation";
            this.Load += new System.EventHandler(this.frmCheckPortfolioAdministrator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDiferencesAdmin1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiferencesAdmin1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.splitMellon.Panel1.ResumeLayout(false);
            this.splitMellon.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMellon)).EndInit();
            this.splitMellon.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtgDiferencesAdmin1;
        private MyXtraGrid.MyGridView dgDiferencesAdmin1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private NestPortCombo cmbFund;
        private System.Windows.Forms.Button cmdDiferences;
        private System.Windows.Forms.TextBox txtNAVLivebook;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNAVAdmin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNAVDiff;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radEquities;
        private System.Windows.Forms.RadioButton radCash;
        private System.Windows.Forms.RadioButton radForwards;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFW_Diff;
        private System.Windows.Forms.TextBox txtFW_Admin;
        private System.Windows.Forms.TextBox txtFW_LB;
        private System.Windows.Forms.TextBox txtOP_Diff;
        private System.Windows.Forms.TextBox txtOP_Admin;
        private System.Windows.Forms.TextBox txtOP_LB;
        private System.Windows.Forms.TextBox txtEQ_Diff;
        private System.Windows.Forms.TextBox txtEQ_Admin;
        private System.Windows.Forms.TextBox txtEQ_LB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCX_Diff;
        private System.Windows.Forms.TextBox txtCX_Admin;
        private System.Windows.Forms.TextBox txtCX_LB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblImportFile;
        private System.Windows.Forms.WebBrowser webMellon;
        private System.Windows.Forms.SplitContainer splitMellon;
        private System.Windows.Forms.CheckBox chkViewMellon;
        private System.Windows.Forms.Button cmdEditMellon;
        private System.Windows.Forms.Button cmdPartialRecalc;
        private System.Windows.Forms.Button cmdFinished;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtBD_Diff;
        private System.Windows.Forms.TextBox txtBD_Admin;
        private System.Windows.Forms.TextBox txtBD_LB;
        private System.Windows.Forms.TextBox txtNAVFile;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtNAVTableLivebook;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFileNAV_LB;
        private System.Windows.Forms.TextBox txtFileNAV_Adm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBonds_Dif;
        private System.Windows.Forms.TextBox txtBonds_Adm;
        private System.Windows.Forms.TextBox txtBonds_LB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCashOther_Dif;
        private System.Windows.Forms.TextBox txtCashOther_Adm;
        private System.Windows.Forms.TextBox txtCashOther_LB;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtFoward_Dif;
        private System.Windows.Forms.TextBox txtFoward_Adm;
        private System.Windows.Forms.TextBox txtFoward_LB;
        private System.Windows.Forms.TextBox txtOptions_Dif;
        private System.Windows.Forms.TextBox txtOptions_Adm;
        private System.Windows.Forms.TextBox txtOptions_LB;
        private System.Windows.Forms.TextBox txtEquities_Dif;
        private System.Windows.Forms.TextBox txtEquities_Adm;
        private System.Windows.Forms.TextBox txtEquities_LB;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTotal_Dif;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTotal_Adm;
        private System.Windows.Forms.TextBox txtTotal_LB;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button cmdInsertPrice;
        private System.Windows.Forms.Label lblFunds;
        private System.Windows.Forms.TextBox txtFunds_Dif;
        private System.Windows.Forms.TextBox txtFunds_Admin;
        private System.Windows.Forms.TextBox txtFunds_LB;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtDividends_Dif;
        private System.Windows.Forms.TextBox txtDividends_Admin;
        private System.Windows.Forms.TextBox txtDividends_LB;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtFutures_Dif;
        private System.Windows.Forms.TextBox txtFutures_Admin;
        private System.Windows.Forms.TextBox txtFutures_LB;
        private System.Windows.Forms.TextBox txtPercDif_Funds;
        private System.Windows.Forms.TextBox txtPercDif_Divid;
        private System.Windows.Forms.TextBox txtPercDif_Futures;
        private System.Windows.Forms.TextBox txtPercDif_Bonds;
        private System.Windows.Forms.TextBox txtPercDif_Cash;
        private System.Windows.Forms.TextBox txtPercDif_FW;
        private System.Windows.Forms.TextBox txtPercDif_Options;
        private System.Windows.Forms.TextBox txtPercDif_Equities;
        private System.Windows.Forms.TextBox txtPercDif_Total;
        private System.Windows.Forms.TextBox txtPercDif_Brokerage;
        private System.Windows.Forms.Label lblBrokerage;
        private System.Windows.Forms.TextBox txtBrokerage_Dif;
        private System.Windows.Forms.TextBox txtBrokerage_Admin;
        private System.Windows.Forms.TextBox txtBrokerage_LB;
        private System.Windows.Forms.TextBox txtPercDif_Expense;
        private System.Windows.Forms.Label lblExpenses;
        private System.Windows.Forms.TextBox txtExpense_Dif;
        private System.Windows.Forms.TextBox txtExpense_Admin;
        private System.Windows.Forms.TextBox txtExpense_LB;
        private System.Windows.Forms.TextBox txtPercDif_Loan;
        private System.Windows.Forms.Label lblStockLoan;
        private System.Windows.Forms.TextBox txtStockLoand_Dif;
        private System.Windows.Forms.TextBox txtStockLoand_Admin;
        private System.Windows.Forms.TextBox txtStockLoand_LB;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtCheckPerfCalc;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox chkMerge;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtNavBrl;
        private System.Windows.Forms.TextBox txtNavTotal;
        private System.Windows.Forms.TextBox txtnavUsd;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtCheckPerfFile;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtNavShare;
        private System.Windows.Forms.TextBox txtBrok_Fut_DifPerc;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtBrok_Fut_Diff;
        private System.Windows.Forms.TextBox txtBrok_Fut_Admin;
        private System.Windows.Forms.TextBox txtBrok_Fut_LB;
    }
}