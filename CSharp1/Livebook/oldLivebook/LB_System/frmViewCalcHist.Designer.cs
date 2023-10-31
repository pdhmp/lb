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
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.cmd_Reloadtrans = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.dtgViewCalc = new MyXtraGrid.MyGridControl();
            this.dgViewCalc = new MyXtraGrid.MyGridView();
            this.myGridView4 = new MyXtraGrid.MyGridView();
            this.lblTime = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewCalc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMessages
            // 
            this.txtMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessages.Location = new System.Drawing.Point(6, 365);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(688, 177);
            this.txtMessages.TabIndex = 30;
            // 
            // cmd_Reloadtrans
            // 
            this.cmd_Reloadtrans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmd_Reloadtrans.Location = new System.Drawing.Point(735, 514);
            this.cmd_Reloadtrans.Name = "cmd_Reloadtrans";
            this.cmd_Reloadtrans.Size = new System.Drawing.Size(116, 31);
            this.cmd_Reloadtrans.TabIndex = 33;
            this.cmd_Reloadtrans.Text = "Reload Transaction";
            this.cmd_Reloadtrans.UseVisualStyleBackColor = true;
            this.cmd_Reloadtrans.Click += new System.EventHandler(this.cmdReloadtrans_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // dtgViewCalc
            // 
            this.dtgViewCalc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgViewCalc.Location = new System.Drawing.Point(6, 5);
            this.dtgViewCalc.LookAndFeel.SkinName = "Blue";
            this.dtgViewCalc.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgViewCalc.MainView = this.dgViewCalc;
            this.dtgViewCalc.Name = "dtgViewCalc";
            this.dtgViewCalc.Size = new System.Drawing.Size(853, 353);
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
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(6, 21);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(17, 13);
            this.lblTime.TabIndex = 35;
            this.lblTime.Text = "xx";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblTime);
            this.groupBox1.Location = new System.Drawing.Point(700, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(151, 50);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Table Loaded:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(700, 416);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(151, 50);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Table Mode";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(100, 20);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(45, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Fast";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 20);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(48, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Slow";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(735, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 31);
            this.button1.TabIndex = 38;
            this.button1.Text = "Cancel Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmViewCalcHist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 553);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtgViewCalc);
            this.Controls.Add(this.cmd_Reloadtrans);
            this.Controls.Add(this.txtMessages);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewCalcHist";
            this.Text = "View Historical Calculate Positions";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewCalc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Button cmd_Reloadtrans;
        private System.Windows.Forms.Timer timer1;
        private MyXtraGrid.MyGridControl dtgViewCalc;
        private MyXtraGrid.MyGridView dgViewCalc;
        private MyXtraGrid.MyGridView myGridView4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button1;
    }
}