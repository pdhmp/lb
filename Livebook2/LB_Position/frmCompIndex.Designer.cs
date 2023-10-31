namespace LiveBook
{
    partial class frmCompIndex
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompIndex));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCopy = new System.Windows.Forms.Label();
            this.cmbView = new System.Windows.Forms.ComboBox();
            this.cmbStrategy = new System.Windows.Forms.ComboBox();
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dgCompIndex = new MyXtraGrid.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(503, 326);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 32;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // cmbView
            // 
            this.cmbView.FormattingEnabled = true;
            this.cmbView.Location = new System.Drawing.Point(247, 2);
            this.cmbView.Name = "cmbView";
            this.cmbView.Size = new System.Drawing.Size(152, 21);
            this.cmbView.TabIndex = 25;
            this.cmbView.Visible = false;
            // 
            // cmbStrategy
            // 
            this.cmbStrategy.FormattingEnabled = true;
            this.cmbStrategy.Location = new System.Drawing.Point(10, 2);
            this.cmbStrategy.Name = "cmbStrategy";
            this.cmbStrategy.Size = new System.Drawing.Size(152, 21);
            this.cmbStrategy.TabIndex = 31;
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(10, 29);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dgCompIndex;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(511, 294);
            this.dtg.TabIndex = 33;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgCompIndex});
            // 
            // dgCompIndex
            // 
            this.dgCompIndex.GridControl = this.dtg;
            this.dgCompIndex.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgCompIndex.Name = "dgCompIndex";
            this.dgCompIndex.OptionsBehavior.Editable = false;
            this.dgCompIndex.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgCompIndex_DragObjectDrop_1);
            this.dgCompIndex.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgCompIndex_CustomDrawGroupRow);
            this.dgCompIndex.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgCompIndex_ColumnWidthChanged);
            this.dgCompIndex.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgCompIndex_MouseUp);
            // 
            // frmCompIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(533, 339);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.cmbStrategy);
            this.Controls.Add(this.cmbView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCompIndex";
            this.Text = "Compare to IBOV";
            this.Load += new System.EventHandler(this.frmCompIndex_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.ComboBox cmbView;
        private System.Windows.Forms.ComboBox cmbStrategy;
        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dgCompIndex;

    }
}