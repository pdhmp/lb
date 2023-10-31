namespace LiveBook
{
    partial class frmViewCalcHist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewCalcHist));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtgViewCalc = new MyXtraGrid.MyGridControl();
            this.dgViewCalc = new MyXtraGrid.MyGridView();
            this.myGridView4 = new MyXtraGrid.MyGridView();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgViewCalc
            // 
            this.dtgViewCalc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgViewCalc.Location = new System.Drawing.Point(6, 5);
            this.dtgViewCalc.LookAndFeel.SkinName = "Blue";
            this.dtgViewCalc.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgViewCalc.MainView = this.dgViewCalc;
            this.dtgViewCalc.Name = "dtgViewCalc";
            this.dtgViewCalc.Size = new System.Drawing.Size(627, 328);
            this.dtgViewCalc.TabIndex = 34;
            this.dtgViewCalc.TabStop = false;
            this.dtgViewCalc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgViewCalc,
            this.myGridView4});
            // 
            // dgViewCalc
            // 
            this.dgViewCalc.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgViewCalc.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgViewCalc.GridControl = this.dtgViewCalc;
            this.dgViewCalc.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgViewCalc.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgViewCalc.Name = "dgViewCalc";
            this.dgViewCalc.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgViewCalc.OptionsSelection.MultiSelect = true;
            this.dgViewCalc.OptionsView.ColumnAutoWidth = false;
            this.dgViewCalc.RowHeight = 15;
            this.dgViewCalc.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgViewCalc_DragObjectDrop);
            this.dgViewCalc.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgViewCalc_ColumnWidthChanged);
            // 
            // myGridView4
            // 
            this.myGridView4.GridControl = this.dtgViewCalc;
            this.myGridView4.Name = "myGridView4";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(542, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 35;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmViewCalcHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 325);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtgViewCalc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(645, 363);
            this.MinimumSize = new System.Drawing.Size(645, 363);
            this.Name = "frmViewCalcHist";
            this.Text = "View Historical Calculate Positions";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private MyXtraGrid.MyGridControl dtgViewCalc;
        private MyXtraGrid.MyGridView dgViewCalc;
        private MyXtraGrid.MyGridView myGridView4;
        private System.Windows.Forms.Button btnClear;
    }
}