namespace LBHistCalc
{
    partial class frmHistCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHistCalc));
            this.dtgLinesToCalc = new NCustomControls.MyGridControl();
            this.dgLinesToCalc = new NCustomControls.MyGridView();
            this.cmdCalcAll = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.cmdLoadData = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.tmrSavePos = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLinesToCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinesToCalc)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgLinesToCalc
            // 
            this.dtgLinesToCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgLinesToCalc.Location = new System.Drawing.Point(15, 12);
            this.dtgLinesToCalc.LookAndFeel.SkinName = "Blue";
            this.dtgLinesToCalc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgLinesToCalc.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgLinesToCalc.MainView = this.dgLinesToCalc;
            this.dtgLinesToCalc.Name = "dtgLinesToCalc";
            this.dtgLinesToCalc.Size = new System.Drawing.Size(755, 352);
            this.dtgLinesToCalc.TabIndex = 27;
            this.dtgLinesToCalc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgLinesToCalc});
            // 
            // dgLinesToCalc
            // 
            this.dgLinesToCalc.GridControl = this.dtgLinesToCalc;
            this.dgLinesToCalc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgLinesToCalc.Name = "dgLinesToCalc";
            this.dgLinesToCalc.OptionsBehavior.Editable = false;
            this.dgLinesToCalc.OptionsSelection.MultiSelect = true;
            this.dgLinesToCalc.OptionsView.ColumnAutoWidth = false;
            this.dgLinesToCalc.OptionsView.ShowGroupPanel = false;
            // 
            // cmdCalcAll
            // 
            this.cmdCalcAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCalcAll.Location = new System.Drawing.Point(449, 480);
            this.cmdCalcAll.Name = "cmdCalcAll";
            this.cmdCalcAll.Size = new System.Drawing.Size(93, 31);
            this.cmdCalcAll.TabIndex = 28;
            this.cmdCalcAll.Text = "Calc All";
            this.cmdCalcAll.UseVisualStyleBackColor = true;
            this.cmdCalcAll.Visible = false;
            this.cmdCalcAll.Click += new System.EventHandler(this.cmdCalcAll_Click);
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessages.Location = new System.Drawing.Point(15, 370);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(644, 141);
            this.txtMessages.TabIndex = 29;
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 500;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // cmdLoadData
            // 
            this.cmdLoadData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdLoadData.Enabled = false;
            this.cmdLoadData.Location = new System.Drawing.Point(677, 480);
            this.cmdLoadData.Name = "cmdLoadData";
            this.cmdLoadData.Size = new System.Drawing.Size(93, 31);
            this.cmdLoadData.TabIndex = 30;
            this.cmdLoadData.Text = "LoadData";
            this.cmdLoadData.UseVisualStyleBackColor = true;
            this.cmdLoadData.Visible = false;
            this.cmdLoadData.Click += new System.EventHandler(this.cmdLoadData_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStop.Enabled = false;
            this.cmdStop.Location = new System.Drawing.Point(665, 370);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(105, 31);
            this.cmdStop.TabIndex = 31;
            this.cmdStop.Text = "Cancel Calculate";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // tmrSavePos
            // 
            this.tmrSavePos.Interval = 5000;
            this.tmrSavePos.Tick += new System.EventHandler(this.tmrSavePos_Tick);
            // 
            // frmHistCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(782, 523);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdLoadData);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.cmdCalcAll);
            this.Controls.Add(this.dtgLinesToCalc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHistCalc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calc Hstorical Positions";
            this.Load += new System.EventHandler(this.frmHistCalc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLinesToCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgLinesToCalc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NCustomControls.MyGridControl dtgLinesToCalc;
        private NCustomControls.MyGridView dgLinesToCalc;
        private System.Windows.Forms.Button cmdCalcAll;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Button cmdLoadData;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.Timer tmrSavePos;
    }
}

