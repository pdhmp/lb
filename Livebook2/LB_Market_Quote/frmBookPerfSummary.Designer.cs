namespace LiveBook
{
    partial class frmBookPerfSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBookPerfSummary));
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgBookPerfSummary = new MyXtraGrid.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCopy = new System.Windows.Forms.Label();
            this.cmbPortfolio = new LiveBook.NestPortCombo();
            this.lblError = new System.Windows.Forms.Label();
            this.lblErrorText = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBookPerfSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(0, 32);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgBookPerfSummary;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(377, 219);
            this.dtg.TabIndex = 25;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgBookPerfSummary});
            // 
            // dgBookPerfSummary
            // 
            this.dgBookPerfSummary.GridControl = this.dtg;
            this.dgBookPerfSummary.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgBookPerfSummary.Name = "dgBookPerfSummary";
            this.dgBookPerfSummary.OptionsBehavior.Editable = false;
            this.dgBookPerfSummary.OptionsSelection.MultiSelect = true;
            this.dgBookPerfSummary.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgBookPerfSummary.OptionsView.ShowGroupPanel = false;
            this.dgBookPerfSummary.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgFundPerfSummary_DragObjectDrop);
            this.dgBookPerfSummary.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgFundPerfSummary_ColumnWidthChanged);
            this.dgBookPerfSummary.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgFundPerfSummary_MouseDown);
            this.dgBookPerfSummary.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgFundPerfSummary_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(344, 235);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 34;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // cmbPortfolio
            // 
            this.cmbPortfolio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPortfolio.DisplayMember = "Port_Name";
            this.cmbPortfolio.FormattingEnabled = true;
            this.cmbPortfolio.IdPortType = 2;
            this.cmbPortfolio.includeAllPortsOption = false;
            this.cmbPortfolio.includeMHConsolOption = false;
            this.cmbPortfolio.Location = new System.Drawing.Point(230, 5);
            this.cmbPortfolio.Name = "cmbPortfolio";
            this.cmbPortfolio.Size = new System.Drawing.Size(135, 21);
            this.cmbPortfolio.TabIndex = 35;
            this.cmbPortfolio.ValueMember = "Id_Portfolio";
            this.cmbPortfolio.SelectionChangeCommitted += new System.EventHandler(this.cmbPortfolio_SelectionChangeCommitted);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(12, 8);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(56, 13);
            this.lblError.TabIndex = 36;
            this.lblError.Text = "MTD Error";
            // 
            // lblErrorText
            // 
            this.lblErrorText.Location = new System.Drawing.Point(74, 5);
            this.lblErrorText.Name = "lblErrorText";
            this.lblErrorText.Size = new System.Drawing.Size(82, 21);
            this.lblErrorText.TabIndex = 36;
            this.lblErrorText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer2
            // 
            this.timer2.Interval = 300000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmBookPerfSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(375, 250);
            this.Controls.Add(this.lblErrorText);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.cmbPortfolio);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.dtg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBookPerfSummary";
            this.Text = "Book Performance Summaries";
            this.Load += new System.EventHandler(this.frmresume_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgBookPerfSummary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgBookPerfSummary;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCopy;
        private NestPortCombo cmbPortfolio;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblErrorText;
        private System.Windows.Forms.Timer timer2;

    }
}