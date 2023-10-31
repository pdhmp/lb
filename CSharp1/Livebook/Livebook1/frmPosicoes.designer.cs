namespace SGN
{
    partial class frmPosicoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPosicoes));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Rendimento = new System.Windows.Forms.GroupBox();
            this.txtmtm = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtmtm_perc = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLongDelta = new System.Windows.Forms.TextBox();
            this.txtShort_Delta = new System.Windows.Forms.TextBox();
            this.txtEx_Delta = new System.Windows.Forms.TextBox();
            this.txtNet_Delta = new System.Windows.Forms.TextBox();
            this.txt_p_Long_Delta = new System.Windows.Forms.TextBox();
            this.txt_p_Short_Delta = new System.Windows.Forms.TextBox();
            this.txt_p_Ex_Delta = new System.Windows.Forms.TextBox();
            this.txt_p_Net_Delta = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtLong = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtShort = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtEx = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtNet = new System.Windows.Forms.TextBox();
            this.txt_p_Long = new System.Windows.Forms.TextBox();
            this.txt_p_Short = new System.Windows.Forms.TextBox();
            this.txt_p_Ex = new System.Windows.Forms.TextBox();
            this.txt_p_Net = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtPL = new System.Windows.Forms.TextBox();
            this.cmdrefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbChoosePortfolio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpIniDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtg = new DevExpress.XtraGrid.GridControl();
            this.dgPositions = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.dgrelPos = new DevExpress.XtraGrid.GridControl();
            this.RelPos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtNAV = new System.Windows.Forms.TextBox();
            this.txttesteLong = new System.Windows.Forms.TextBox();
            this.txtTesteShort = new System.Windows.Forms.TextBox();
            this.Rendimento.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrelPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelPos)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user.png");
            this.imageList1.Images.SetKeyName(1, "user_female.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            // 
            // Rendimento
            // 
            this.Rendimento.Controls.Add(this.txtmtm);
            this.Rendimento.Controls.Add(this.label29);
            this.Rendimento.Controls.Add(this.txtmtm_perc);
            this.Rendimento.Controls.Add(this.label33);
            this.Rendimento.Location = new System.Drawing.Point(408, 15);
            this.Rendimento.Name = "Rendimento";
            this.Rendimento.Size = new System.Drawing.Size(195, 55);
            this.Rendimento.TabIndex = 15;
            this.Rendimento.TabStop = false;
            this.Rendimento.Text = "P&&L";
            // 
            // txtmtm
            // 
            this.txtmtm.BackColor = System.Drawing.Color.White;
            this.txtmtm.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmtm.Location = new System.Drawing.Point(40, 27);
            this.txtmtm.Name = "txtmtm";
            this.txtmtm.ReadOnly = true;
            this.txtmtm.Size = new System.Drawing.Size(88, 18);
            this.txtmtm.TabIndex = 23;
            this.txtmtm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(149, 13);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(15, 11);
            this.label29.TabIndex = 21;
            this.label29.Text = "%";
            // 
            // txtmtm_perc
            // 
            this.txtmtm_perc.BackColor = System.Drawing.Color.White;
            this.txtmtm_perc.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmtm_perc.Location = new System.Drawing.Point(134, 27);
            this.txtmtm_perc.Name = "txtmtm_perc";
            this.txtmtm_perc.ReadOnly = true;
            this.txtmtm_perc.Size = new System.Drawing.Size(53, 18);
            this.txtmtm_perc.TabIndex = 3;
            this.txtmtm_perc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(7, 31);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(27, 11);
            this.label33.TabIndex = 2;
            this.label33.Text = "MTM";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtLongDelta);
            this.groupBox1.Controls.Add(this.txtShort_Delta);
            this.groupBox1.Controls.Add(this.txtEx_Delta);
            this.groupBox1.Controls.Add(this.txtNet_Delta);
            this.groupBox1.Controls.Add(this.txt_p_Long_Delta);
            this.groupBox1.Controls.Add(this.txt_p_Short_Delta);
            this.groupBox1.Controls.Add(this.txt_p_Ex_Delta);
            this.groupBox1.Controls.Add(this.txt_p_Net_Delta);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtLong);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.txtShort);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtEx);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.txtNet);
            this.groupBox1.Controls.Add(this.txt_p_Long);
            this.groupBox1.Controls.Add(this.txt_p_Short);
            this.groupBox1.Controls.Add(this.txt_p_Ex);
            this.groupBox1.Controls.Add(this.txt_p_Net);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtPL);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 117);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exposures";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(328, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(15, 11);
            this.label15.TabIndex = 28;
            this.label15.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(222, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 11);
            this.label9.TabIndex = 27;
            this.label9.Text = "Delta";
            // 
            // txtLongDelta
            // 
            this.txtLongDelta.BackColor = System.Drawing.Color.White;
            this.txtLongDelta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLongDelta.Location = new System.Drawing.Point(223, 37);
            this.txtLongDelta.Name = "txtLongDelta";
            this.txtLongDelta.ReadOnly = true;
            this.txtLongDelta.Size = new System.Drawing.Size(100, 18);
            this.txtLongDelta.TabIndex = 26;
            this.txtLongDelta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtShort_Delta
            // 
            this.txtShort_Delta.BackColor = System.Drawing.Color.White;
            this.txtShort_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShort_Delta.Location = new System.Drawing.Point(223, 55);
            this.txtShort_Delta.Name = "txtShort_Delta";
            this.txtShort_Delta.ReadOnly = true;
            this.txtShort_Delta.Size = new System.Drawing.Size(100, 18);
            this.txtShort_Delta.TabIndex = 25;
            this.txtShort_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtEx_Delta
            // 
            this.txtEx_Delta.BackColor = System.Drawing.Color.White;
            this.txtEx_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEx_Delta.Location = new System.Drawing.Point(223, 73);
            this.txtEx_Delta.Name = "txtEx_Delta";
            this.txtEx_Delta.ReadOnly = true;
            this.txtEx_Delta.Size = new System.Drawing.Size(100, 18);
            this.txtEx_Delta.TabIndex = 24;
            this.txtEx_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNet_Delta
            // 
            this.txtNet_Delta.BackColor = System.Drawing.Color.White;
            this.txtNet_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNet_Delta.Location = new System.Drawing.Point(223, 91);
            this.txtNet_Delta.Name = "txtNet_Delta";
            this.txtNet_Delta.ReadOnly = true;
            this.txtNet_Delta.Size = new System.Drawing.Size(100, 18);
            this.txtNet_Delta.TabIndex = 23;
            this.txtNet_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Long_Delta
            // 
            this.txt_p_Long_Delta.BackColor = System.Drawing.Color.White;
            this.txt_p_Long_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Long_Delta.Location = new System.Drawing.Point(330, 37);
            this.txt_p_Long_Delta.Name = "txt_p_Long_Delta";
            this.txt_p_Long_Delta.ReadOnly = true;
            this.txt_p_Long_Delta.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Long_Delta.TabIndex = 22;
            this.txt_p_Long_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Short_Delta
            // 
            this.txt_p_Short_Delta.BackColor = System.Drawing.Color.White;
            this.txt_p_Short_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Short_Delta.Location = new System.Drawing.Point(330, 55);
            this.txt_p_Short_Delta.Name = "txt_p_Short_Delta";
            this.txt_p_Short_Delta.ReadOnly = true;
            this.txt_p_Short_Delta.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Short_Delta.TabIndex = 21;
            this.txt_p_Short_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Ex_Delta
            // 
            this.txt_p_Ex_Delta.BackColor = System.Drawing.Color.White;
            this.txt_p_Ex_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Ex_Delta.Location = new System.Drawing.Point(330, 73);
            this.txt_p_Ex_Delta.Name = "txt_p_Ex_Delta";
            this.txt_p_Ex_Delta.ReadOnly = true;
            this.txt_p_Ex_Delta.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Ex_Delta.TabIndex = 20;
            this.txt_p_Ex_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Net_Delta
            // 
            this.txt_p_Net_Delta.BackColor = System.Drawing.Color.White;
            this.txt_p_Net_Delta.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Net_Delta.Location = new System.Drawing.Point(330, 91);
            this.txt_p_Net_Delta.Name = "txt_p_Net_Delta";
            this.txt_p_Net_Delta.ReadOnly = true;
            this.txt_p_Net_Delta.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Net_Delta.TabIndex = 19;
            this.txt_p_Net_Delta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(174, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 11);
            this.label10.TabIndex = 18;
            this.label10.Text = "%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(38, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 11);
            this.label11.TabIndex = 17;
            this.label11.Text = "Long";
            // 
            // txtLong
            // 
            this.txtLong.BackColor = System.Drawing.Color.White;
            this.txtLong.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLong.Location = new System.Drawing.Point(68, 37);
            this.txtLong.Name = "txtLong";
            this.txtLong.ReadOnly = true;
            this.txtLong.Size = new System.Drawing.Size(100, 18);
            this.txtLong.TabIndex = 16;
            this.txtLong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(35, 58);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(27, 11);
            this.label16.TabIndex = 15;
            this.label16.Text = "Short";
            // 
            // txtShort
            // 
            this.txtShort.BackColor = System.Drawing.Color.White;
            this.txtShort.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShort.Location = new System.Drawing.Point(68, 55);
            this.txtShort.Name = "txtShort";
            this.txtShort.ReadOnly = true;
            this.txtShort.Size = new System.Drawing.Size(100, 18);
            this.txtShort.TabIndex = 14;
            this.txtShort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(35, 76);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(28, 11);
            this.label17.TabIndex = 13;
            this.label17.Text = "Gross";
            // 
            // txtEx
            // 
            this.txtEx.BackColor = System.Drawing.Color.White;
            this.txtEx.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEx.Location = new System.Drawing.Point(68, 73);
            this.txtEx.Name = "txtEx";
            this.txtEx.ReadOnly = true;
            this.txtEx.Size = new System.Drawing.Size(100, 18);
            this.txtEx.TabIndex = 12;
            this.txtEx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(42, 94);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(20, 11);
            this.label18.TabIndex = 11;
            this.label18.Text = "Net";
            // 
            // txtNet
            // 
            this.txtNet.BackColor = System.Drawing.Color.White;
            this.txtNet.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNet.Location = new System.Drawing.Point(68, 91);
            this.txtNet.Name = "txtNet";
            this.txtNet.ReadOnly = true;
            this.txtNet.Size = new System.Drawing.Size(100, 18);
            this.txtNet.TabIndex = 10;
            this.txtNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Long
            // 
            this.txt_p_Long.BackColor = System.Drawing.Color.White;
            this.txt_p_Long.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Long.Location = new System.Drawing.Point(173, 37);
            this.txt_p_Long.Name = "txt_p_Long";
            this.txt_p_Long.ReadOnly = true;
            this.txt_p_Long.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Long.TabIndex = 8;
            this.txt_p_Long.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Short
            // 
            this.txt_p_Short.BackColor = System.Drawing.Color.White;
            this.txt_p_Short.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Short.Location = new System.Drawing.Point(173, 55);
            this.txt_p_Short.Name = "txt_p_Short";
            this.txt_p_Short.ReadOnly = true;
            this.txt_p_Short.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Short.TabIndex = 6;
            this.txt_p_Short.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Ex
            // 
            this.txt_p_Ex.BackColor = System.Drawing.Color.White;
            this.txt_p_Ex.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Ex.Location = new System.Drawing.Point(173, 73);
            this.txt_p_Ex.Name = "txt_p_Ex";
            this.txt_p_Ex.ReadOnly = true;
            this.txt_p_Ex.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Ex.TabIndex = 4;
            this.txt_p_Ex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_p_Net
            // 
            this.txt_p_Net.BackColor = System.Drawing.Color.White;
            this.txt_p_Net.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_p_Net.Location = new System.Drawing.Point(173, 91);
            this.txt_p_Net.Name = "txt_p_Net";
            this.txt_p_Net.ReadOnly = true;
            this.txt_p_Net.Size = new System.Drawing.Size(44, 18);
            this.txt_p_Net.TabIndex = 2;
            this.txt_p_Net.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(30, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(32, 11);
            this.label19.TabIndex = 1;
            this.label19.Text = "Assets";
            // 
            // txtPL
            // 
            this.txtPL.BackColor = System.Drawing.Color.White;
            this.txtPL.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPL.Location = new System.Drawing.Point(68, 19);
            this.txtPL.Name = "txtPL";
            this.txtPL.ReadOnly = true;
            this.txtPL.Size = new System.Drawing.Size(100, 18);
            this.txtPL.TabIndex = 0;
            this.txtPL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cmdrefresh
            // 
            this.cmdrefresh.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdrefresh.Location = new System.Drawing.Point(653, 85);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 32);
            this.cmdrefresh.TabIndex = 13;
            this.cmdrefresh.Text = "Generate Portfolio";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            this.cmdrefresh.Click += new System.EventHandler(this.cmdrefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(415, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 11);
            this.label5.TabIndex = 12;
            this.label5.Text = "Choose Portfolio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(417, 98);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(617, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 11);
            this.label1.TabIndex = 17;
            this.label1.Text = "Initial Date";
            // 
            // dtpIniDate
            // 
            this.dtpIniDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIniDate.Location = new System.Drawing.Point(619, 44);
            this.dtpIniDate.Name = "dtpIniDate";
            this.dtpIniDate.Size = new System.Drawing.Size(95, 20);
            this.dtpIniDate.TabIndex = 18;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(720, 44);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(95, 20);
            this.dtpEndDate.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(718, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 11);
            this.label2.TabIndex = 20;
            this.label2.Text = "End Date";
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.EmbeddedNavigator.Name = "";
            this.dtg.Location = new System.Drawing.Point(12, 136);
            this.dtg.MainView = this.dgPositions;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(992, 586);
            this.dtg.TabIndex = 21;
            this.dtg.TabStop = false;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgPositions});
            // 
            // dgPositions
            // 
            this.dgPositions.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgPositions.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.dgPositions.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.dgPositions.Appearance.DetailTip.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.DetailTip.Options.UseFont = true;
            this.dgPositions.Appearance.Empty.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.Empty.Options.UseFont = true;
            this.dgPositions.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.EvenRow.Options.UseFont = true;
            this.dgPositions.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FilterCloseButton.Options.UseFont = true;
            this.dgPositions.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FilterPanel.Options.UseFont = true;
            this.dgPositions.Appearance.FixedLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FixedLine.Options.UseFont = true;
            this.dgPositions.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FocusedCell.Options.UseFont = true;
            this.dgPositions.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FocusedRow.Options.UseFont = true;
            this.dgPositions.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.FooterPanel.Options.UseFont = true;
            this.dgPositions.Appearance.GroupButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupButton.Options.UseFont = true;
            this.dgPositions.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupFooter.Options.UseFont = true;
            this.dgPositions.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupPanel.Options.UseFont = true;
            this.dgPositions.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.GroupRow.Options.UseFont = true;
            this.dgPositions.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.HeaderPanel.Options.UseFont = true;
            this.dgPositions.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.HideSelectionRow.Options.UseFont = true;
            this.dgPositions.Appearance.HorzLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.HorzLine.Options.UseFont = true;
            this.dgPositions.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.OddRow.Options.UseFont = true;
            this.dgPositions.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.Preview.Options.UseFont = true;
            this.dgPositions.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.Row.Options.UseFont = true;
            this.dgPositions.Appearance.RowSeparator.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.RowSeparator.Options.UseFont = true;
            this.dgPositions.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.SelectedRow.Options.UseFont = true;
            this.dgPositions.Appearance.TopNewRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.TopNewRow.Options.UseFont = true;
            this.dgPositions.Appearance.VertLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.Appearance.VertLine.Options.UseFont = true;
            this.dgPositions.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.EvenRow.Options.UseFont = true;
            this.dgPositions.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.dgPositions.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.dgPositions.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.dgPositions.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.GroupRow.Options.UseFont = true;
            this.dgPositions.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.dgPositions.AppearancePrint.Lines.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.Lines.Options.UseFont = true;
            this.dgPositions.AppearancePrint.OddRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.OddRow.Options.UseFont = true;
            this.dgPositions.AppearancePrint.Preview.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.Preview.Options.UseFont = true;
            this.dgPositions.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPositions.AppearancePrint.Row.Options.UseFont = true;
            this.dgPositions.ColumnPanelRowHeight = 15;
            this.dgPositions.GridControl = this.dtg;
            this.dgPositions.GroupRowHeight = 10;
            this.dgPositions.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgPositions.Name = "dgPositions";
            this.dgPositions.OptionsBehavior.Editable = false;
            this.dgPositions.OptionsView.ColumnAutoWidth = false;
            this.dgPositions.RowHeight = 15;
            this.dgPositions.ShowCustomizationForm += new System.EventHandler(this.dgPositions_ShowCustomizationForm);
            this.dgPositions.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgPositions_CustomDrawGroupRow);
            this.dgPositions.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgPositions_DragObjectDrop);
            this.dgPositions.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgPositions_ColumnWidthChanged);
            this.dgPositions.EndGrouping += new System.EventHandler(this.dgPositions_EndGrouping);
            this.dgPositions.HideCustomizationForm += new System.EventHandler(this.dgPositions_HideCustomizationForm);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.button5);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Location = new System.Drawing.Point(790, 92);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(214, 38);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Export";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 20);
            this.button1.TabIndex = 17;
            this.button1.Text = "Custom";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(71, 14);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(59, 20);
            this.button5.TabIndex = 16;
            this.button5.Text = "Txt";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 14);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(59, 20);
            this.button4.TabIndex = 15;
            this.button4.Text = "Excel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // dgrelPos
            // 
            this.dgrelPos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgrelPos.EmbeddedNavigator.Name = "";
            this.dgrelPos.Location = new System.Drawing.Point(683, 157);
            this.dgrelPos.MainView = this.RelPos;
            this.dgrelPos.Name = "dgrelPos";
            this.dgrelPos.Size = new System.Drawing.Size(321, 10);
            this.dgrelPos.TabIndex = 23;
            this.dgrelPos.TabStop = false;
            this.dgrelPos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.RelPos});
            this.dgrelPos.Visible = false;
            // 
            // RelPos
            // 
            this.RelPos.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.RelPos.Appearance.ColumnFilterButtonActive.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.ColumnFilterButtonActive.Options.UseFont = true;
            this.RelPos.Appearance.CustomizationFormHint.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.CustomizationFormHint.Options.UseFont = true;
            this.RelPos.Appearance.DetailTip.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.DetailTip.Options.UseFont = true;
            this.RelPos.Appearance.Empty.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.Empty.Options.UseFont = true;
            this.RelPos.Appearance.EvenRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.EvenRow.Options.UseFont = true;
            this.RelPos.Appearance.FilterCloseButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.FilterCloseButton.Options.UseFont = true;
            this.RelPos.Appearance.FilterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.FilterPanel.Options.UseFont = true;
            this.RelPos.Appearance.FixedLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.FixedLine.Options.UseFont = true;
            this.RelPos.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.FocusedCell.Options.UseFont = true;
            this.RelPos.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.FocusedRow.Options.UseFont = true;
            this.RelPos.Appearance.FooterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.FooterPanel.Options.UseFont = true;
            this.RelPos.Appearance.GroupButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.GroupButton.Options.UseFont = true;
            this.RelPos.Appearance.GroupFooter.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.GroupFooter.Options.UseFont = true;
            this.RelPos.Appearance.GroupPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.GroupPanel.Options.UseFont = true;
            this.RelPos.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.GroupRow.Options.UseFont = true;
            this.RelPos.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.HeaderPanel.Options.UseFont = true;
            this.RelPos.Appearance.HideSelectionRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.HideSelectionRow.Options.UseFont = true;
            this.RelPos.Appearance.HorzLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.HorzLine.Options.UseFont = true;
            this.RelPos.Appearance.OddRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.OddRow.Options.UseFont = true;
            this.RelPos.Appearance.Preview.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.Preview.Options.UseFont = true;
            this.RelPos.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.Row.Options.UseFont = true;
            this.RelPos.Appearance.RowSeparator.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.RowSeparator.Options.UseFont = true;
            this.RelPos.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.SelectedRow.Options.UseFont = true;
            this.RelPos.Appearance.TopNewRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.TopNewRow.Options.UseFont = true;
            this.RelPos.Appearance.VertLine.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.Appearance.VertLine.Options.UseFont = true;
            this.RelPos.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.EvenRow.Options.UseFont = true;
            this.RelPos.AppearancePrint.FilterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.FilterPanel.Options.UseFont = true;
            this.RelPos.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.RelPos.AppearancePrint.GroupFooter.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.GroupFooter.Options.UseFont = true;
            this.RelPos.AppearancePrint.GroupRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.GroupRow.Options.UseFont = true;
            this.RelPos.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.RelPos.AppearancePrint.Lines.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.Lines.Options.UseFont = true;
            this.RelPos.AppearancePrint.OddRow.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.OddRow.Options.UseFont = true;
            this.RelPos.AppearancePrint.Preview.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.Preview.Options.UseFont = true;
            this.RelPos.AppearancePrint.Row.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelPos.AppearancePrint.Row.Options.UseFont = true;
            this.RelPos.ColumnPanelRowHeight = 15;
            this.RelPos.GridControl = this.dgrelPos;
            this.RelPos.GroupRowHeight = 10;
            this.RelPos.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.RelPos.Name = "RelPos";
            this.RelPos.OptionsBehavior.Editable = false;
            this.RelPos.OptionsView.ColumnAutoWidth = false;
            this.RelPos.RowHeight = 15;
            this.RelPos.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.RelPos_ColumnWidthChanged);
            this.RelPos.ColumnChanged += new System.EventHandler(this.RelPos_ColumnChanged);
            // 
            // txtNAV
            // 
            this.txtNAV.Location = new System.Drawing.Point(832, 12);
            this.txtNAV.Name = "txtNAV";
            this.txtNAV.Size = new System.Drawing.Size(100, 20);
            this.txtNAV.TabIndex = 24;
            // 
            // txttesteLong
            // 
            this.txttesteLong.Location = new System.Drawing.Point(832, 38);
            this.txttesteLong.Name = "txttesteLong";
            this.txttesteLong.Size = new System.Drawing.Size(100, 20);
            this.txttesteLong.TabIndex = 25;
            // 
            // txtTesteShort
            // 
            this.txtTesteShort.Location = new System.Drawing.Point(832, 62);
            this.txtTesteShort.Name = "txtTesteShort";
            this.txtTesteShort.Size = new System.Drawing.Size(100, 20);
            this.txtTesteShort.TabIndex = 26;
            // 
            // frmPosicoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.txtTesteShort);
            this.Controls.Add(this.txttesteLong);
            this.Controls.Add(this.txtNAV);
            this.Controls.Add(this.dgrelPos);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpIniDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Rendimento);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbChoosePortfolio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPosicoes";
            this.Text = "Positions";
            this.Load += new System.EventHandler(this.frmPosicoes_Load);
            this.Rendimento.ResumeLayout(false);
            this.Rendimento.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrelPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelPos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox Rendimento;
        private System.Windows.Forms.TextBox txtmtm;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtmtm_perc;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLongDelta;
        private System.Windows.Forms.TextBox txtShort_Delta;
        private System.Windows.Forms.TextBox txtEx_Delta;
        private System.Windows.Forms.TextBox txtNet_Delta;
        private System.Windows.Forms.TextBox txt_p_Long_Delta;
        private System.Windows.Forms.TextBox txt_p_Short_Delta;
        private System.Windows.Forms.TextBox txt_p_Ex_Delta;
        private System.Windows.Forms.TextBox txt_p_Net_Delta;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtLong;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtShort;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtEx;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtNet;
        private System.Windows.Forms.TextBox txt_p_Long;
        private System.Windows.Forms.TextBox txt_p_Short;
        private System.Windows.Forms.TextBox txt_p_Ex;
        private System.Windows.Forms.TextBox txt_p_Net;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtPL;
        private System.Windows.Forms.Button cmdrefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbChoosePortfolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpIniDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraGrid.GridControl dtg;
        private DevExpress.XtraGrid.Views.Grid.GridView dgPositions;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private DevExpress.XtraGrid.GridControl dgrelPos;
        private DevExpress.XtraGrid.Views.Grid.GridView RelPos;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtNAV;
        private System.Windows.Forms.TextBox txttesteLong;
        private System.Windows.Forms.TextBox txtTesteShort;





    }
}

