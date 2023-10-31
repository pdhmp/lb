namespace CPort
{
    partial class frmCPort
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
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.dtgCPort = new NCustomControls.MyGridControl();
            this.dgCPort = new NCustomControls.MyGridView();
            this.lblIndexLast = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIndexChange = new System.Windows.Forms.Label();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.cmdCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCPort)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Interval = 500;
            this.tmrUpdate.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgCPort
            // 
            this.dtgCPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgCPort.Location = new System.Drawing.Point(0, 25);
            this.dtgCPort.LookAndFeel.SkinName = "Blue";
            this.dtgCPort.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtgCPort.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgCPort.MainView = this.dgCPort;
            this.dtgCPort.Name = "dtgCPort";
            this.dtgCPort.Size = new System.Drawing.Size(1059, 613);
            this.dtgCPort.TabIndex = 26;
            this.dtgCPort.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgCPort});
            this.dtgCPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgQuotes_KeyDown);
            this.dtgCPort.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseDown);
            this.dtgCPort.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgQuotes_MouseUp);
            // 
            // dgCPort
            // 
            this.dgCPort.Appearance.FocusedRow.BackColor = System.Drawing.Color.Silver;
            this.dgCPort.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.dgCPort.Appearance.FocusedRow.Options.UseBackColor = true;
            this.dgCPort.Appearance.FocusedRow.Options.UseForeColor = true;
            this.dgCPort.GridControl = this.dtgCPort;
            this.dgCPort.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgCPort.Name = "dgCPort";
            this.dgCPort.OptionsBehavior.AllowIncrementalSearch = true;
            this.dgCPort.OptionsBehavior.Editable = false;
            this.dgCPort.OptionsSelection.UseIndicatorForSelection = false;
            this.dgCPort.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgQuotes_CustomDrawCell);
            this.dgCPort.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgQuotes_CustomDrawGroupRow);
            this.dgCPort.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.dgQuotes_RowStyle);
            // 
            // lblIndexLast
            // 
            this.lblIndexLast.AutoSize = true;
            this.lblIndexLast.Location = new System.Drawing.Point(74, 9);
            this.lblIndexLast.Name = "lblIndexLast";
            this.lblIndexLast.Size = new System.Drawing.Size(13, 13);
            this.lblIndexLast.TabIndex = 28;
            this.lblIndexLast.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Index Last";
            // 
            // lblIndexChange
            // 
            this.lblIndexChange.AutoSize = true;
            this.lblIndexChange.Location = new System.Drawing.Point(155, 9);
            this.lblIndexChange.Name = "lblIndexChange";
            this.lblIndexChange.Size = new System.Drawing.Size(13, 13);
            this.lblIndexChange.TabIndex = 30;
            this.lblIndexChange.Text = "0";
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessages.Location = new System.Drawing.Point(525, 645);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(522, 137);
            this.txtMessages.TabIndex = 31;
            // 
            // cmdCopy
            // 
            this.cmdCopy.Location = new System.Drawing.Point(444, 645);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Size = new System.Drawing.Size(75, 23);
            this.cmdCopy.TabIndex = 32;
            this.cmdCopy.Text = "Copy";
            this.cmdCopy.UseVisualStyleBackColor = true;
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // frmCPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1059, 794);
            this.Controls.Add(this.cmdCopy);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.lblIndexChange);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblIndexLast);
            this.Controls.Add(this.dtgCPort);
            this.Name = "frmCPort";
            this.Text = "Quote";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmIbovArb_FormClosing);
            this.Load += new System.EventHandler(this.frmIbovArb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrUpdate;
        private NCustomControls.MyGridView dgCPort;
        private NCustomControls.MyGridControl dtgCPort;
        private System.Windows.Forms.Label lblIndexLast;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIndexChange;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button cmdCopy;
    }
}

