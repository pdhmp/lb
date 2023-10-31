namespace LiveBook
{
    partial class frmAppraiser
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
            this.cboAvaliado = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtg = new DevExpress.XtraGrid.GridControl();
            this.dg = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // cboAvaliado
            // 
            this.cboAvaliado.FormattingEnabled = true;
            this.cboAvaliado.Location = new System.Drawing.Point(67, 13);
            this.cboAvaliado.Name = "cboAvaliado";
            this.cboAvaliado.Size = new System.Drawing.Size(285, 21);
            this.cboAvaliado.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Avaliado";
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(16, 40);
            this.dtg.MainView = this.dg;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(840, 532);
            this.dtg.TabIndex = 35;
            this.dtg.TabStop = false;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dg});
            // 
            // dg
            // 
            this.dg.GridControl = this.dtg;
            this.dg.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dg.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dg.Name = "dg";
            this.dg.OptionsBehavior.Editable = false;
            this.dg.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dg.OptionsView.ColumnAutoWidth = false;
            this.dg.RowHeight = 15;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(714, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 21);
            this.comboBox1.TabIndex = 36;
            // 
            // frmAppraiser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 613);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.cboAvaliado);
            this.Controls.Add(this.label2);
            this.Name = "frmAppraiser";
            this.Text = "frmAvaliador";
            this.Load += new System.EventHandler(this.frmAppraiser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboAvaliado;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtg;
        private DevExpress.XtraGrid.Views.Grid.GridView dg;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}