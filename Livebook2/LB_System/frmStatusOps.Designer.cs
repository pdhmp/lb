namespace LiveBook
{
    partial class frmStatusOps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStatusOps));
            this.lblTime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDividends = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblBondsYest = new System.Windows.Forms.Label();
            this.lblBondsPrev = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.lblDolYest = new System.Windows.Forms.Label();
            this.lblDolPrev = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblYestAdr = new System.Windows.Forms.Label();
            this.lblYestLast = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrevAdr = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPrevLast = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dtpYesterday = new System.Windows.Forms.DateTimePicker();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dtpPrev = new System.Windows.Forms.DateTimePicker();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.grpAnalysis = new System.Windows.Forms.GroupBox();
            this.lblNFund_An = new System.Windows.Forms.Label();
            this.lblFia_An = new System.Windows.Forms.Label();
            this.lblArb_An = new System.Windows.Forms.Label();
            this.lblQuant_An = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblMile_An = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.grpPort = new System.Windows.Forms.GroupBox();
            this.lblNFund_Port = new System.Windows.Forms.Label();
            this.lblFia_Port = new System.Windows.Forms.Label();
            this.lblArb_Port = new System.Windows.Forms.Label();
            this.lblQuant_Port = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblMile_Port = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.grpNAV = new System.Windows.Forms.GroupBox();
            this.lblNFund_NAV = new System.Windows.Forms.Label();
            this.lblFia_NAV = new System.Windows.Forms.Label();
            this.lblArb_NAV = new System.Windows.Forms.Label();
            this.lblQuant_NAV = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblMile_NAV = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblBrokerageYest = new System.Windows.Forms.Label();
            this.lblBrokeragePrev = new System.Windows.Forms.Label();
            this.lblBrokerage = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpAnalysis.SuspendLayout();
            this.grpPort.SuspendLayout();
            this.grpNAV.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(147, 34);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(17, 13);
            this.lblTime.TabIndex = 35;
            this.lblTime.Text = "xx";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblDividends);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.lblTime);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(310, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(287, 88);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initial Day";
            // 
            // lblDividends
            // 
            this.lblDividends.AutoSize = true;
            this.lblDividends.Location = new System.Drawing.Point(147, 54);
            this.lblDividends.Name = "lblDividends";
            this.lblDividends.Size = new System.Drawing.Size(17, 13);
            this.lblDividends.TabIndex = 37;
            this.lblDividends.Text = "xx";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(6, 54);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 13);
            this.label28.TabIndex = 38;
            this.label28.Text = "Dividends";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Transaction Table";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblBrokerageYest);
            this.groupBox3.Controls.Add(this.lblBrokeragePrev);
            this.groupBox3.Controls.Add(this.lblBrokerage);
            this.groupBox3.Controls.Add(this.lblBondsYest);
            this.groupBox3.Controls.Add(this.lblBondsPrev);
            this.groupBox3.Controls.Add(this.label41);
            this.groupBox3.Controls.Add(this.lblDolYest);
            this.groupBox3.Controls.Add(this.lblDolPrev);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.lblYestAdr);
            this.groupBox3.Controls.Add(this.lblYestLast);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblPrevAdr);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.lblPrevLast);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(12, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 137);
            this.groupBox3.TabIndex = 43;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Historical Price";
            // 
            // lblBondsYest
            // 
            this.lblBondsYest.AutoSize = true;
            this.lblBondsYest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBondsYest.Location = new System.Drawing.Point(210, 95);
            this.lblBondsYest.Name = "lblBondsYest";
            this.lblBondsYest.Size = new System.Drawing.Size(24, 13);
            this.lblBondsYest.TabIndex = 65;
            this.lblBondsYest.Text = "NA";
            // 
            // lblBondsPrev
            // 
            this.lblBondsPrev.AutoSize = true;
            this.lblBondsPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBondsPrev.Location = new System.Drawing.Point(135, 95);
            this.lblBondsPrev.Name = "lblBondsPrev";
            this.lblBondsPrev.Size = new System.Drawing.Size(24, 13);
            this.lblBondsPrev.TabIndex = 64;
            this.lblBondsPrev.Text = "NA";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(6, 95);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(65, 13);
            this.label41.TabIndex = 63;
            this.label41.Text = "Brasil Bonds";
            // 
            // lblDolYest
            // 
            this.lblDolYest.AutoSize = true;
            this.lblDolYest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDolYest.Location = new System.Drawing.Point(210, 75);
            this.lblDolYest.Name = "lblDolYest";
            this.lblDolYest.Size = new System.Drawing.Size(24, 13);
            this.lblDolYest.TabIndex = 62;
            this.lblDolYest.Text = "NA";
            // 
            // lblDolPrev
            // 
            this.lblDolPrev.AutoSize = true;
            this.lblDolPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDolPrev.Location = new System.Drawing.Point(135, 75);
            this.lblDolPrev.Name = "lblDolPrev";
            this.lblDolPrev.Size = new System.Drawing.Size(24, 13);
            this.lblDolPrev.TabIndex = 61;
            this.lblDolPrev.Text = "NA";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 75);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "Dolar Bm&&F";
            // 
            // lblYestAdr
            // 
            this.lblYestAdr.AutoSize = true;
            this.lblYestAdr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYestAdr.Location = new System.Drawing.Point(210, 55);
            this.lblYestAdr.Name = "lblYestAdr";
            this.lblYestAdr.Size = new System.Drawing.Size(24, 13);
            this.lblYestAdr.TabIndex = 59;
            this.lblYestAdr.Text = "NA";
            // 
            // lblYestLast
            // 
            this.lblYestLast.AutoSize = true;
            this.lblYestLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYestLast.Location = new System.Drawing.Point(210, 35);
            this.lblYestLast.Name = "lblYestLast";
            this.lblYestLast.Size = new System.Drawing.Size(24, 13);
            this.lblYestLast.TabIndex = 58;
            this.lblYestLast.Text = "NA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Yesterday";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Prev";
            // 
            // lblPrevAdr
            // 
            this.lblPrevAdr.AutoSize = true;
            this.lblPrevAdr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrevAdr.Location = new System.Drawing.Point(135, 55);
            this.lblPrevAdr.Name = "lblPrevAdr";
            this.lblPrevAdr.Size = new System.Drawing.Size(24, 13);
            this.lblPrevAdr.TabIndex = 55;
            this.lblPrevAdr.Text = "NA";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 54;
            this.label6.Text = "ADR Mellon Price";
            // 
            // lblPrevLast
            // 
            this.lblPrevLast.AutoSize = true;
            this.lblPrevLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrevLast.Location = new System.Drawing.Point(135, 35);
            this.lblPrevLast.Name = "lblPrevLast";
            this.lblPrevLast.Size = new System.Drawing.Size(24, 13);
            this.lblPrevLast.TabIndex = 53;
            this.lblPrevLast.Text = "NA";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Prior Mellon Price ";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dtpYesterday);
            this.groupBox4.Location = new System.Drawing.Point(132, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(114, 52);
            this.groupBox4.TabIndex = 42;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Yesterday";
            // 
            // dtpYesterday
            // 
            this.dtpYesterday.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpYesterday.Location = new System.Drawing.Point(6, 19);
            this.dtpYesterday.Name = "dtpYesterday";
            this.dtpYesterday.Size = new System.Drawing.Size(97, 20);
            this.dtpYesterday.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dtpPrev);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(114, 52);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Prev Date";
            // 
            // dtpPrev
            // 
            this.dtpPrev.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPrev.Location = new System.Drawing.Point(6, 19);
            this.dtpPrev.Name = "dtpPrev";
            this.dtpPrev.Size = new System.Drawing.Size(97, 20);
            this.dtpPrev.TabIndex = 0;
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(252, 31);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 40;
            this.cmdLoad.Text = "Check";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // grpAnalysis
            // 
            this.grpAnalysis.Controls.Add(this.lblNFund_An);
            this.grpAnalysis.Controls.Add(this.lblFia_An);
            this.grpAnalysis.Controls.Add(this.lblArb_An);
            this.grpAnalysis.Controls.Add(this.lblQuant_An);
            this.grpAnalysis.Controls.Add(this.label21);
            this.grpAnalysis.Controls.Add(this.label22);
            this.grpAnalysis.Controls.Add(this.label23);
            this.grpAnalysis.Controls.Add(this.label24);
            this.grpAnalysis.Controls.Add(this.lblMile_An);
            this.grpAnalysis.Controls.Add(this.label26);
            this.grpAnalysis.Location = new System.Drawing.Point(12, 213);
            this.grpAnalysis.Name = "grpAnalysis";
            this.grpAnalysis.Size = new System.Drawing.Size(141, 139);
            this.grpAnalysis.TabIndex = 44;
            this.grpAnalysis.TabStop = false;
            this.grpAnalysis.Text = "Analysis File";
            // 
            // lblNFund_An
            // 
            this.lblNFund_An.AutoSize = true;
            this.lblNFund_An.Location = new System.Drawing.Point(100, 45);
            this.lblNFund_An.Name = "lblNFund_An";
            this.lblNFund_An.Size = new System.Drawing.Size(17, 13);
            this.lblNFund_An.TabIndex = 57;
            this.lblNFund_An.Text = "xx";
            // 
            // lblFia_An
            // 
            this.lblFia_An.AutoSize = true;
            this.lblFia_An.Location = new System.Drawing.Point(100, 65);
            this.lblFia_An.Name = "lblFia_An";
            this.lblFia_An.Size = new System.Drawing.Size(17, 13);
            this.lblFia_An.TabIndex = 56;
            this.lblFia_An.Text = "xx";
            // 
            // lblArb_An
            // 
            this.lblArb_An.AutoSize = true;
            this.lblArb_An.Location = new System.Drawing.Point(100, 85);
            this.lblArb_An.Name = "lblArb_An";
            this.lblArb_An.Size = new System.Drawing.Size(17, 13);
            this.lblArb_An.TabIndex = 55;
            this.lblArb_An.Text = "xx";
            // 
            // lblQuant_An
            // 
            this.lblQuant_An.AutoSize = true;
            this.lblQuant_An.Location = new System.Drawing.Point(100, 105);
            this.lblQuant_An.Name = "lblQuant_An";
            this.lblQuant_An.Size = new System.Drawing.Size(17, 13);
            this.lblQuant_An.TabIndex = 54;
            this.lblQuant_An.Text = "xx";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 45);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 13);
            this.label21.TabIndex = 53;
            this.label21.Text = "Nest Fund";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 65);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(21, 13);
            this.label22.TabIndex = 52;
            this.label22.Text = "Fia";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 85);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(23, 13);
            this.label23.TabIndex = 51;
            this.label23.Text = "Arb";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 105);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(36, 13);
            this.label24.TabIndex = 50;
            this.label24.Text = "Quant";
            // 
            // lblMile_An
            // 
            this.lblMile_An.AutoSize = true;
            this.lblMile_An.Location = new System.Drawing.Point(100, 25);
            this.lblMile_An.Name = "lblMile_An";
            this.lblMile_An.Size = new System.Drawing.Size(17, 13);
            this.lblMile_An.TabIndex = 48;
            this.lblMile_An.Text = "xx";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(26, 13);
            this.label26.TabIndex = 49;
            this.label26.Text = "Mile";
            // 
            // grpPort
            // 
            this.grpPort.Controls.Add(this.lblNFund_Port);
            this.grpPort.Controls.Add(this.lblFia_Port);
            this.grpPort.Controls.Add(this.lblArb_Port);
            this.grpPort.Controls.Add(this.lblQuant_Port);
            this.grpPort.Controls.Add(this.label11);
            this.grpPort.Controls.Add(this.label12);
            this.grpPort.Controls.Add(this.label13);
            this.grpPort.Controls.Add(this.label14);
            this.grpPort.Controls.Add(this.lblMile_Port);
            this.grpPort.Controls.Add(this.label16);
            this.grpPort.Location = new System.Drawing.Point(159, 213);
            this.grpPort.Name = "grpPort";
            this.grpPort.Size = new System.Drawing.Size(145, 139);
            this.grpPort.TabIndex = 45;
            this.grpPort.TabStop = false;
            this.grpPort.Text = "Portfolio File";
            // 
            // lblNFund_Port
            // 
            this.lblNFund_Port.AutoSize = true;
            this.lblNFund_Port.Location = new System.Drawing.Point(100, 45);
            this.lblNFund_Port.Name = "lblNFund_Port";
            this.lblNFund_Port.Size = new System.Drawing.Size(17, 13);
            this.lblNFund_Port.TabIndex = 57;
            this.lblNFund_Port.Text = "xx";
            // 
            // lblFia_Port
            // 
            this.lblFia_Port.AutoSize = true;
            this.lblFia_Port.Location = new System.Drawing.Point(100, 65);
            this.lblFia_Port.Name = "lblFia_Port";
            this.lblFia_Port.Size = new System.Drawing.Size(17, 13);
            this.lblFia_Port.TabIndex = 56;
            this.lblFia_Port.Text = "xx";
            // 
            // lblArb_Port
            // 
            this.lblArb_Port.AutoSize = true;
            this.lblArb_Port.Location = new System.Drawing.Point(100, 85);
            this.lblArb_Port.Name = "lblArb_Port";
            this.lblArb_Port.Size = new System.Drawing.Size(17, 13);
            this.lblArb_Port.TabIndex = 55;
            this.lblArb_Port.Text = "xx";
            // 
            // lblQuant_Port
            // 
            this.lblQuant_Port.AutoSize = true;
            this.lblQuant_Port.Location = new System.Drawing.Point(100, 105);
            this.lblQuant_Port.Name = "lblQuant_Port";
            this.lblQuant_Port.Size = new System.Drawing.Size(17, 13);
            this.lblQuant_Port.TabIndex = 54;
            this.lblQuant_Port.Text = "xx";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 13);
            this.label11.TabIndex = 53;
            this.label11.Text = "Nest Fund";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 65);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 52;
            this.label12.Text = "Fia";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 51;
            this.label13.Text = "Arb";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 105);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 13);
            this.label14.TabIndex = 50;
            this.label14.Text = "Quant";
            // 
            // lblMile_Port
            // 
            this.lblMile_Port.AutoSize = true;
            this.lblMile_Port.Location = new System.Drawing.Point(100, 25);
            this.lblMile_Port.Name = "lblMile_Port";
            this.lblMile_Port.Size = new System.Drawing.Size(17, 13);
            this.lblMile_Port.TabIndex = 48;
            this.lblMile_Port.Text = "xx";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(26, 13);
            this.label16.TabIndex = 49;
            this.label16.Text = "Mile";
            // 
            // grpNAV
            // 
            this.grpNAV.Controls.Add(this.lblNFund_NAV);
            this.grpNAV.Controls.Add(this.lblFia_NAV);
            this.grpNAV.Controls.Add(this.lblArb_NAV);
            this.grpNAV.Controls.Add(this.lblQuant_NAV);
            this.grpNAV.Controls.Add(this.label33);
            this.grpNAV.Controls.Add(this.label34);
            this.grpNAV.Controls.Add(this.label35);
            this.grpNAV.Controls.Add(this.label36);
            this.grpNAV.Controls.Add(this.lblMile_NAV);
            this.grpNAV.Controls.Add(this.label38);
            this.grpNAV.Location = new System.Drawing.Point(8, 358);
            this.grpNAV.Name = "grpNAV";
            this.grpNAV.Size = new System.Drawing.Size(145, 131);
            this.grpNAV.TabIndex = 46;
            this.grpNAV.TabStop = false;
            this.grpNAV.Text = "LiveBook NAV";
            // 
            // lblNFund_NAV
            // 
            this.lblNFund_NAV.AutoSize = true;
            this.lblNFund_NAV.Location = new System.Drawing.Point(100, 45);
            this.lblNFund_NAV.Name = "lblNFund_NAV";
            this.lblNFund_NAV.Size = new System.Drawing.Size(17, 13);
            this.lblNFund_NAV.TabIndex = 57;
            this.lblNFund_NAV.Text = "xx";
            // 
            // lblFia_NAV
            // 
            this.lblFia_NAV.AutoSize = true;
            this.lblFia_NAV.Location = new System.Drawing.Point(100, 65);
            this.lblFia_NAV.Name = "lblFia_NAV";
            this.lblFia_NAV.Size = new System.Drawing.Size(17, 13);
            this.lblFia_NAV.TabIndex = 56;
            this.lblFia_NAV.Text = "xx";
            // 
            // lblArb_NAV
            // 
            this.lblArb_NAV.AutoSize = true;
            this.lblArb_NAV.Location = new System.Drawing.Point(100, 85);
            this.lblArb_NAV.Name = "lblArb_NAV";
            this.lblArb_NAV.Size = new System.Drawing.Size(17, 13);
            this.lblArb_NAV.TabIndex = 55;
            this.lblArb_NAV.Text = "xx";
            // 
            // lblQuant_NAV
            // 
            this.lblQuant_NAV.AutoSize = true;
            this.lblQuant_NAV.Location = new System.Drawing.Point(100, 105);
            this.lblQuant_NAV.Name = "lblQuant_NAV";
            this.lblQuant_NAV.Size = new System.Drawing.Size(17, 13);
            this.lblQuant_NAV.TabIndex = 54;
            this.lblQuant_NAV.Text = "xx";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 45);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(56, 13);
            this.label33.TabIndex = 53;
            this.label33.Text = "Nest Fund";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(6, 65);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(21, 13);
            this.label34.TabIndex = 52;
            this.label34.Text = "Fia";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(6, 85);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(23, 13);
            this.label35.TabIndex = 51;
            this.label35.Text = "Arb";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(6, 105);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(36, 13);
            this.label36.TabIndex = 50;
            this.label36.Text = "Quant";
            // 
            // lblMile_NAV
            // 
            this.lblMile_NAV.AutoSize = true;
            this.lblMile_NAV.Location = new System.Drawing.Point(100, 25);
            this.lblMile_NAV.Name = "lblMile_NAV";
            this.lblMile_NAV.Size = new System.Drawing.Size(17, 13);
            this.lblMile_NAV.TabIndex = 48;
            this.lblMile_NAV.Text = "xx";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(6, 25);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(26, 13);
            this.label38.TabIndex = 49;
            this.label38.Text = "Mile";
            // 
            // lblBrokerageYest
            // 
            this.lblBrokerageYest.AutoSize = true;
            this.lblBrokerageYest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrokerageYest.Location = new System.Drawing.Point(210, 115);
            this.lblBrokerageYest.Name = "lblBrokerageYest";
            this.lblBrokerageYest.Size = new System.Drawing.Size(24, 13);
            this.lblBrokerageYest.TabIndex = 68;
            this.lblBrokerageYest.Text = "NA";
            // 
            // lblBrokeragePrev
            // 
            this.lblBrokeragePrev.AutoSize = true;
            this.lblBrokeragePrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrokeragePrev.Location = new System.Drawing.Point(135, 115);
            this.lblBrokeragePrev.Name = "lblBrokeragePrev";
            this.lblBrokeragePrev.Size = new System.Drawing.Size(24, 13);
            this.lblBrokeragePrev.TabIndex = 67;
            this.lblBrokeragePrev.Text = "NA";
            // 
            // lblBrokerage
            // 
            this.lblBrokerage.AutoSize = true;
            this.lblBrokerage.Location = new System.Drawing.Point(6, 115);
            this.lblBrokerage.Name = "lblBrokerage";
            this.lblBrokerage.Size = new System.Drawing.Size(84, 13);
            this.lblBrokerage.TabIndex = 66;
            this.lblBrokerage.Text = "Brasil Brokerage";
            // 
            // frmStatusOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(608, 500);
            this.Controls.Add(this.grpNAV);
            this.Controls.Add(this.grpPort);
            this.Controls.Add(this.grpAnalysis);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStatusOps";
            this.Text = "Operation Status";
            this.Load += new System.EventHandler(this.frmStatusOps_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.grpAnalysis.ResumeLayout(false);
            this.grpAnalysis.PerformLayout();
            this.grpPort.ResumeLayout(false);
            this.grpPort.PerformLayout();
            this.grpNAV.ResumeLayout(false);
            this.grpNAV.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpYesterday;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DateTimePicker dtpPrev;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label lblPrevAdr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPrevLast;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblYestAdr;
        private System.Windows.Forms.Label lblYestLast;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDolYest;
        private System.Windows.Forms.Label lblDolPrev;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpAnalysis;
        private System.Windows.Forms.Label lblNFund_An;
        private System.Windows.Forms.Label lblFia_An;
        private System.Windows.Forms.Label lblArb_An;
        private System.Windows.Forms.Label lblQuant_An;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblMile_An;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox grpPort;
        private System.Windows.Forms.Label lblNFund_Port;
        private System.Windows.Forms.Label lblFia_Port;
        private System.Windows.Forms.Label lblArb_Port;
        private System.Windows.Forms.Label lblQuant_Port;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblMile_Port;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblDividends;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox grpNAV;
        private System.Windows.Forms.Label lblNFund_NAV;
        private System.Windows.Forms.Label lblFia_NAV;
        private System.Windows.Forms.Label lblArb_NAV;
        private System.Windows.Forms.Label lblQuant_NAV;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblMile_NAV;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblBondsYest;
        private System.Windows.Forms.Label lblBondsPrev;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label lblBrokerageYest;
        private System.Windows.Forms.Label lblBrokeragePrev;
        private System.Windows.Forms.Label lblBrokerage;
    }
}