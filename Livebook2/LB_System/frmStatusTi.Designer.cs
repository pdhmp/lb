namespace LiveBook
{
    partial class frmStatusTi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatusTi));
            this.dtpPrev = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpYesterday = new System.Windows.Forms.DateTimePicker();
            this.dtgViewHistPrice = new MyXtraGrid.MyGridControl();
            this.dgViewHistPrice = new MyXtraGrid.MyGridView();
            this.myGridView4 = new MyXtraGrid.MyGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.xlblReutersFeeder_Last_Flag = new System.Windows.Forms.Label();
            this.xlblReutersFeeder_Last = new System.Windows.Forms.Label();
            this.xlblCALC_LB2_Historical_Flag = new System.Windows.Forms.Label();
            this.xlblCALC_LB2_Historical = new System.Windows.Forms.Label();
            this.xlblCALC_LB2_Client_Flag = new System.Windows.Forms.Label();
            this.xlblCALC_LB2_Client = new System.Windows.Forms.Label();
            this.lblMktCap = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVol6M = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LblSystem = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txt_Last = new System.Windows.Forms.Label();
            this.lblLast = new System.Windows.Forms.Label();
            this.txtTR_Index = new System.Windows.Forms.Label();
            this.lblTR_Index = new System.Windows.Forms.Label();
            this.txtTR_Day = new System.Windows.Forms.Label();
            this.lblTR_Day = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewHistPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewHistPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpPrev
            // 
            this.dtpPrev.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPrev.Location = new System.Drawing.Point(6, 19);
            this.dtpPrev.Name = "dtpPrev";
            this.dtpPrev.Size = new System.Drawing.Size(97, 20);
            this.dtpPrev.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Check";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpPrev);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(114, 52);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prev Date";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpYesterday);
            this.groupBox2.Location = new System.Drawing.Point(132, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 52);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Yesterday";
            // 
            // dtpYesterday
            // 
            this.dtpYesterday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpYesterday.Location = new System.Drawing.Point(6, 19);
            this.dtpYesterday.Name = "dtpYesterday";
            this.dtpYesterday.Size = new System.Drawing.Size(97, 20);
            this.dtpYesterday.TabIndex = 0;
            // 
            // dtgViewHistPrice
            // 
            this.dtgViewHistPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgViewHistPrice.Location = new System.Drawing.Point(6, 19);
            this.dtgViewHistPrice.LookAndFeel.SkinName = "Blue";
            this.dtgViewHistPrice.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgViewHistPrice.MainView = this.dgViewHistPrice;
            this.dtgViewHistPrice.Name = "dtgViewHistPrice";
            this.dtgViewHistPrice.Size = new System.Drawing.Size(488, 227);
            this.dtgViewHistPrice.TabIndex = 35;
            this.dtgViewHistPrice.TabStop = false;
            this.dtgViewHistPrice.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgViewHistPrice,
            this.myGridView4});
            // 
            // dgViewHistPrice
            // 
            this.dgViewHistPrice.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgViewHistPrice.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgViewHistPrice.GridControl = this.dtgViewHistPrice;
            this.dgViewHistPrice.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgViewHistPrice.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgViewHistPrice.Name = "dgViewHistPrice";
            this.dgViewHistPrice.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgViewHistPrice.OptionsSelection.MultiSelect = true;
            this.dgViewHistPrice.OptionsView.ColumnAutoWidth = false;
            this.dgViewHistPrice.RowHeight = 15;
            // 
            // myGridView4
            // 
            this.myGridView4.GridControl = this.dtgViewHistPrice;
            this.myGridView4.Name = "myGridView4";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.dtgViewHistPrice);
            this.groupBox3.Location = new System.Drawing.Point(12, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(520, 252);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Historical Price";
            // 
            // xlblReutersFeeder_Last_Flag
            // 
            this.xlblReutersFeeder_Last_Flag.AutoSize = true;
            this.xlblReutersFeeder_Last_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblReutersFeeder_Last_Flag.Location = new System.Drawing.Point(135, 75);
            this.xlblReutersFeeder_Last_Flag.Name = "xlblReutersFeeder_Last_Flag";
            this.xlblReutersFeeder_Last_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblReutersFeeder_Last_Flag.TabIndex = 45;
            this.xlblReutersFeeder_Last_Flag.Text = "NA";
            // 
            // xlblReutersFeeder_Last
            // 
            this.xlblReutersFeeder_Last.AutoSize = true;
            this.xlblReutersFeeder_Last.Location = new System.Drawing.Point(15, 75);
            this.xlblReutersFeeder_Last.Name = "xlblReutersFeeder_Last";
            this.xlblReutersFeeder_Last.Size = new System.Drawing.Size(94, 13);
            this.xlblReutersFeeder_Last.TabIndex = 44;
            this.xlblReutersFeeder_Last.Text = "Reuters Feed Last";
            // 
            // xlblCALC_LB2_Historical_Flag
            // 
            this.xlblCALC_LB2_Historical_Flag.AutoSize = true;
            this.xlblCALC_LB2_Historical_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblCALC_LB2_Historical_Flag.Location = new System.Drawing.Point(135, 55);
            this.xlblCALC_LB2_Historical_Flag.Name = "xlblCALC_LB2_Historical_Flag";
            this.xlblCALC_LB2_Historical_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblCALC_LB2_Historical_Flag.TabIndex = 43;
            this.xlblCALC_LB2_Historical_Flag.Text = "NA";
            // 
            // xlblCALC_LB2_Historical
            // 
            this.xlblCALC_LB2_Historical.AutoSize = true;
            this.xlblCALC_LB2_Historical.Location = new System.Drawing.Point(15, 55);
            this.xlblCALC_LB2_Historical.Name = "xlblCALC_LB2_Historical";
            this.xlblCALC_LB2_Historical.Size = new System.Drawing.Size(114, 13);
            this.xlblCALC_LB2_Historical.TabIndex = 42;
            this.xlblCALC_LB2_Historical.Text = "Calc Historical Position";
            // 
            // xlblCALC_LB2_Client_Flag
            // 
            this.xlblCALC_LB2_Client_Flag.AutoSize = true;
            this.xlblCALC_LB2_Client_Flag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlblCALC_LB2_Client_Flag.Location = new System.Drawing.Point(135, 33);
            this.xlblCALC_LB2_Client_Flag.Name = "xlblCALC_LB2_Client_Flag";
            this.xlblCALC_LB2_Client_Flag.Size = new System.Drawing.Size(24, 13);
            this.xlblCALC_LB2_Client_Flag.TabIndex = 41;
            this.xlblCALC_LB2_Client_Flag.Text = "NA";
            // 
            // xlblCALC_LB2_Client
            // 
            this.xlblCALC_LB2_Client.AutoSize = true;
            this.xlblCALC_LB2_Client.Location = new System.Drawing.Point(15, 33);
            this.xlblCALC_LB2_Client.Name = "xlblCALC_LB2_Client";
            this.xlblCALC_LB2_Client.Size = new System.Drawing.Size(97, 13);
            this.xlblCALC_LB2_Client.TabIndex = 40;
            this.xlblCALC_LB2_Client.Text = "LiveBook RT Calc ";
            // 
            // lblMktCap
            // 
            this.lblMktCap.AutoSize = true;
            this.lblMktCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMktCap.Location = new System.Drawing.Point(318, 75);
            this.lblMktCap.Name = "lblMktCap";
            this.lblMktCap.Size = new System.Drawing.Size(24, 13);
            this.lblMktCap.TabIndex = 51;
            this.lblMktCap.Text = "NA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Mkt Cap";
            // 
            // lblVol6M
            // 
            this.lblVol6M.AutoSize = true;
            this.lblVol6M.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVol6M.Location = new System.Drawing.Point(318, 55);
            this.lblVol6M.Name = "lblVol6M";
            this.lblVol6M.Size = new System.Drawing.Size(24, 13);
            this.lblVol6M.TabIndex = 49;
            this.lblVol6M.Text = "NA";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Vol 6 Months";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(318, 33);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(24, 13);
            this.lblDuration.TabIndex = 47;
            this.lblDuration.Text = "NA";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "Duration";
            // 
            // LblSystem
            // 
            this.LblSystem.AutoSize = true;
            this.LblSystem.Location = new System.Drawing.Point(15, 9);
            this.LblSystem.Name = "LblSystem";
            this.LblSystem.Size = new System.Drawing.Size(41, 13);
            this.LblSystem.TabIndex = 52;
            this.LblSystem.Text = "System";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(229, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "Price";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txt_Last
            // 
            this.txt_Last.AutoSize = true;
            this.txt_Last.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Last.Location = new System.Drawing.Point(482, 75);
            this.txt_Last.Name = "txt_Last";
            this.txt_Last.Size = new System.Drawing.Size(24, 13);
            this.txt_Last.TabIndex = 59;
            this.txt_Last.Text = "NA";
            // 
            // lblLast
            // 
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(393, 75);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(27, 13);
            this.lblLast.TabIndex = 58;
            this.lblLast.Text = "Last";
            // 
            // txtTR_Index
            // 
            this.txtTR_Index.AutoSize = true;
            this.txtTR_Index.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTR_Index.Location = new System.Drawing.Point(482, 55);
            this.txtTR_Index.Name = "txtTR_Index";
            this.txtTR_Index.Size = new System.Drawing.Size(24, 13);
            this.txtTR_Index.TabIndex = 57;
            this.txtTR_Index.Text = "NA";
            // 
            // lblTR_Index
            // 
            this.lblTR_Index.AutoSize = true;
            this.lblTR_Index.Location = new System.Drawing.Point(393, 55);
            this.lblTR_Index.Name = "lblTR_Index";
            this.lblTR_Index.Size = new System.Drawing.Size(54, 13);
            this.lblTR_Index.TabIndex = 56;
            this.lblTR_Index.Text = "TR_Index";
            // 
            // txtTR_Day
            // 
            this.txtTR_Day.AutoSize = true;
            this.txtTR_Day.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTR_Day.Location = new System.Drawing.Point(482, 33);
            this.txtTR_Day.Name = "txtTR_Day";
            this.txtTR_Day.Size = new System.Drawing.Size(24, 13);
            this.txtTR_Day.TabIndex = 55;
            this.txtTR_Day.Text = "NA";
            // 
            // lblTR_Day
            // 
            this.lblTR_Day.AutoSize = true;
            this.lblTR_Day.Location = new System.Drawing.Point(393, 33);
            this.lblTR_Day.Name = "lblTR_Day";
            this.lblTR_Day.Size = new System.Drawing.Size(47, 13);
            this.lblTR_Day.TabIndex = 54;
            this.lblTR_Day.Text = "TR_Day";
            // 
            // frmStatusTi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 425);
            this.Controls.Add(this.txt_Last);
            this.Controls.Add(this.lblLast);
            this.Controls.Add(this.txtTR_Index);
            this.Controls.Add(this.lblTR_Index);
            this.Controls.Add(this.txtTR_Day);
            this.Controls.Add(this.lblTR_Day);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LblSystem);
            this.Controls.Add(this.lblMktCap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblVol6M);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.xlblReutersFeeder_Last_Flag);
            this.Controls.Add(this.xlblReutersFeeder_Last);
            this.Controls.Add(this.xlblCALC_LB2_Historical_Flag);
            this.Controls.Add(this.xlblCALC_LB2_Historical);
            this.Controls.Add(this.xlblCALC_LB2_Client_Flag);
            this.Controls.Add(this.xlblCALC_LB2_Client);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStatusTi";
            this.Text = "IT Status";
            this.Load += new System.EventHandler(this.frmStatusTi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgViewHistPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewHistPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpPrev;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpYesterday;
        private MyXtraGrid.MyGridControl dtgViewHistPrice;
        private MyXtraGrid.MyGridView dgViewHistPrice;
        private MyXtraGrid.MyGridView myGridView4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label xlblReutersFeeder_Last_Flag;
        private System.Windows.Forms.Label xlblReutersFeeder_Last;
        private System.Windows.Forms.Label xlblCALC_LB2_Historical_Flag;
        private System.Windows.Forms.Label xlblCALC_LB2_Historical;
        private System.Windows.Forms.Label xlblCALC_LB2_Client_Flag;
        private System.Windows.Forms.Label xlblCALC_LB2_Client;
        private System.Windows.Forms.Label lblMktCap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVol6M;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LblSystem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label txt_Last;
        private System.Windows.Forms.Label lblLast;
        private System.Windows.Forms.Label txtTR_Index;
        private System.Windows.Forms.Label lblTR_Index;
        private System.Windows.Forms.Label txtTR_Day;
        private System.Windows.Forms.Label lblTR_Day;
    }
}