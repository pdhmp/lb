namespace MarketWatch2
{
    partial class frmWatch
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
            this.dgMain = new NCustomControls.MyGridControl();
            this.dtgQuotes = new NCustomControls.MyGridView();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgQuotes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgMain
            // 
            this.dgMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMain.Location = new System.Drawing.Point(12, 12);
            this.dgMain.LookAndFeel.SkinName = "Blue";
            this.dgMain.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dgMain.MainView = this.dtgQuotes;
            this.dgMain.Name = "dgMain";
            this.dgMain.Size = new System.Drawing.Size(726, 462);
            this.dgMain.TabIndex = 0;
            this.dgMain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dtgQuotes});
            // 
            // dtgQuotes
            // 
            this.dtgQuotes.GridControl = this.dgMain;
            this.dtgQuotes.Name = "dtgQuotes";
            this.dtgQuotes.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgQuotes.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.dtgQuotes.OptionsBehavior.Editable = false;
            this.dtgQuotes.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            this.dtgQuotes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // frmWatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 486);
            this.Controls.Add(this.dgMain);
            this.Name = "frmWatch";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgQuotes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private NCustomControls.MyGridControl dgMain;
        private NCustomControls.MyGridView dtgQuotes;
        private System.Windows.Forms.Timer tmrUpdate;
    }
}

