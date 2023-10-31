namespace LiveBook
{
    partial class frmEditBookSection
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditBookSection));
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgEditBookSection = new MyXtraGrid.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmbBook = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditBookSection)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(5, 43);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgEditBookSection;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(704, 378);
            this.dtg.TabIndex = 26;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgEditBookSection});
            // 
            // dgEditBookSection
            // 
            this.dgEditBookSection.GridControl = this.dtg;
            this.dgEditBookSection.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgEditBookSection.Name = "dgEditBookSection";
            this.dgEditBookSection.OptionsBehavior.Editable = false;
            this.dgEditBookSection.OptionsSelection.MultiSelect = true;
            this.dgEditBookSection.OptionsView.ShowIndicator = false;
            this.dgEditBookSection.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgEditStrategy_DragObjectDrop);
            this.dgEditBookSection.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgEditStrategy_CustomDrawGroupRow);
            this.dgEditBookSection.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgEditStrategy_RowStyle);
            this.dgEditBookSection.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgEditStrategy_ColumnWidthChanged);
            this.dgEditBookSection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgEditStrategy_MouseDown);
            this.dgEditBookSection.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgEditStrategy_MouseUp);
            this.dgEditBookSection.DoubleClick += new System.EventHandler(this.dgEditStrategy_DoubleClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 4000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.Location = new System.Drawing.Point(280, 14);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(75, 23);
            this.cmdRefresh.TabIndex = 27;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(179, 17);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 39;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(177, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 11);
            this.label1.TabIndex = 38;
            this.label1.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 11);
            this.label5.TabIndex = 37;
            this.label5.Text = "Choose Portfólio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(-1, 17);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 36;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox6.Controls.Add(this.cmbSection);
            this.groupBox6.Controls.Add(this.cmdUpdate);
            this.groupBox6.Controls.Add(this.cmbBook);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new System.Drawing.Point(5, 427);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(583, 53);
            this.groupBox6.TabIndex = 76;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "New ";
            // 
            // cmbSection
            // 
            this.cmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(299, 23);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(141, 21);
            this.cmbSection.TabIndex = 13;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Location = new System.Drawing.Point(477, 14);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(100, 30);
            this.cmdUpdate.TabIndex = 74;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbBook
            // 
            this.cmbBook.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBook.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBook.FormattingEnabled = true;
            this.cmbBook.Location = new System.Drawing.Point(60, 23);
            this.cmbBook.Name = "cmbBook";
            this.cmbBook.Size = new System.Drawing.Size(141, 21);
            this.cmbBook.TabIndex = 12;
            this.cmbBook.SelectedIndexChanged += new System.EventHandler(this.cmbBook_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(250, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Section";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Book";
            // 
            // frmEditBookSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 489);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbChoosePortfolio);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.dtg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditBookSection";
            this.Text = "Edit Book & Section";
            this.Load += new System.EventHandler(this.frmEditStrategy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgEditBookSection)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgEditBookSection;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.ComboBox cmbBook;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
    }
}