namespace SGN
{
    partial class zfrmDivisaoBoletas
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
            this.rdMH = new System.Windows.Forms.RadioButton();
            this.rdTop = new System.Windows.Forms.RadioButton();
            this.rdSplit = new System.Windows.Forms.RadioButton();
            this.rdBravo = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTicker = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblPPosTop = new System.Windows.Forms.Label();
            this.lblPPosMH = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtPosBravo = new System.Windows.Forms.TextBox();
            this.txtPosTop = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPosMH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtThBRavo = new System.Windows.Forms.TextBox();
            this.txtThTop = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtThMH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtSplitBravo = new System.Windows.Forms.TextBox();
            this.txtSplitTop = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtSplitMH = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.cmbChoose = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtCloseBravo = new System.Windows.Forms.TextBox();
            this.txtCloseTop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCloseMH = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lblPPLTOP = new System.Windows.Forms.Label();
            this.lblPPLMH = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtPLBravo = new System.Windows.Forms.TextBox();
            this.txtPLTop = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPLMH = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdMH
            // 
            this.rdMH.AutoSize = true;
            this.rdMH.Location = new System.Drawing.Point(14, 19);
            this.rdMH.Name = "rdMH";
            this.rdMH.Size = new System.Drawing.Size(42, 17);
            this.rdMH.TabIndex = 2;
            this.rdMH.TabStop = true;
            this.rdMH.Text = "MH";
            this.rdMH.UseVisualStyleBackColor = true;
            this.rdMH.CheckedChanged += new System.EventHandler(this.rdMH_CheckedChanged);
            // 
            // rdTop
            // 
            this.rdTop.AutoSize = true;
            this.rdTop.Location = new System.Drawing.Point(74, 19);
            this.rdTop.Name = "rdTop";
            this.rdTop.Size = new System.Drawing.Size(60, 17);
            this.rdTop.TabIndex = 3;
            this.rdTop.TabStop = true;
            this.rdTop.Text = "Topline";
            this.rdTop.UseVisualStyleBackColor = true;
            this.rdTop.CheckedChanged += new System.EventHandler(this.rdTop_CheckedChanged);
            // 
            // rdSplit
            // 
            this.rdSplit.AutoSize = true;
            this.rdSplit.Location = new System.Drawing.Point(261, 19);
            this.rdSplit.Name = "rdSplit";
            this.rdSplit.Size = new System.Drawing.Size(45, 17);
            this.rdSplit.TabIndex = 4;
            this.rdSplit.TabStop = true;
            this.rdSplit.Text = "Split";
            this.rdSplit.UseVisualStyleBackColor = true;
            this.rdSplit.CheckedChanged += new System.EventHandler(this.rdSplit_CheckedChanged);
            // 
            // rdBravo
            // 
            this.rdBravo.AutoSize = true;
            this.rdBravo.Location = new System.Drawing.Point(161, 19);
            this.rdBravo.Name = "rdBravo";
            this.rdBravo.Size = new System.Drawing.Size(72, 17);
            this.rdBravo.TabIndex = 5;
            this.rdBravo.TabStop = true;
            this.rdBravo.Text = "Bravo FIA";
            this.rdBravo.UseVisualStyleBackColor = true;
            this.rdBravo.CheckedChanged += new System.EventHandler(this.rdBravo_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(260, 477);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(146, 477);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdBravo);
            this.groupBox1.Controls.Add(this.rdMH);
            this.groupBox1.Controls.Add(this.rdTop);
            this.groupBox1.Controls.Add(this.rdSplit);
            this.groupBox1.Location = new System.Drawing.Point(12, 422);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 48);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Acount";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblId);
            this.groupBox2.Controls.Add(this.lblTicker);
            this.groupBox2.Location = new System.Drawing.Point(12, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(109, 45);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ticker";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(62, 26);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(41, 13);
            this.lblId.TabIndex = 2;
            this.lblId.Text = "label18";
            this.lblId.Visible = false;
            // 
            // lblTicker
            // 
            this.lblTicker.AutoSize = true;
            this.lblTicker.Location = new System.Drawing.Point(14, 19);
            this.lblTicker.Name = "lblTicker";
            this.lblTicker.Size = new System.Drawing.Size(35, 13);
            this.lblTicker.TabIndex = 1;
            this.lblTicker.Text = "label1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPrice);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtQuantity);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(300, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(232, 45);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Trade";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(166, 16);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(60, 20);
            this.txtPrice.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Price";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(60, 16);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(60, 20);
            this.txtQuantity.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Quantity";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblPPosTop);
            this.groupBox4.Controls.Add(this.lblPPosMH);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.txtPosBravo);
            this.groupBox4.Controls.Add(this.txtPosTop);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.txtPosMH);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Location = new System.Drawing.Point(12, 218);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(464, 80);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Position";
            // 
            // lblPPosTop
            // 
            this.lblPPosTop.AutoSize = true;
            this.lblPPosTop.Location = new System.Drawing.Point(292, 60);
            this.lblPPosTop.Name = "lblPPosTop";
            this.lblPPosTop.Size = new System.Drawing.Size(0, 13);
            this.lblPPosTop.TabIndex = 28;
            // 
            // lblPPosMH
            // 
            this.lblPPosMH.AutoSize = true;
            this.lblPPosMH.Location = new System.Drawing.Point(138, 60);
            this.lblPPosMH.Name = "lblPPosMH";
            this.lblPPosMH.Size = new System.Drawing.Size(0, 13);
            this.lblPPosMH.TabIndex = 27;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 60);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(28, 13);
            this.label21.TabIndex = 26;
            this.label21.Text = "%PL";
            // 
            // txtPosBravo
            // 
            this.txtPosBravo.Enabled = false;
            this.txtPosBravo.Location = new System.Drawing.Point(358, 19);
            this.txtPosBravo.Name = "txtPosBravo";
            this.txtPosBravo.Size = new System.Drawing.Size(100, 20);
            this.txtPosBravo.TabIndex = 21;
            // 
            // txtPosTop
            // 
            this.txtPosTop.Enabled = false;
            this.txtPosTop.Location = new System.Drawing.Point(192, 19);
            this.txtPosTop.Name = "txtPosTop";
            this.txtPosTop.Size = new System.Drawing.Size(100, 20);
            this.txtPosTop.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(144, 26);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(42, 13);
            this.label17.TabIndex = 11;
            this.label17.Text = "Topline";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(298, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Bravo FIA";
            // 
            // txtPosMH
            // 
            this.txtPosMH.Enabled = false;
            this.txtPosMH.Location = new System.Drawing.Point(38, 19);
            this.txtPosMH.Name = "txtPosMH";
            this.txtPosMH.Size = new System.Drawing.Size(100, 20);
            this.txtPosMH.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "MH";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtThBRavo);
            this.groupBox5.Controls.Add(this.txtThTop);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtThMH);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(12, 304);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(464, 53);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Theorical Position";
            // 
            // txtThBRavo
            // 
            this.txtThBRavo.Enabled = false;
            this.txtThBRavo.Location = new System.Drawing.Point(355, 19);
            this.txtThBRavo.Name = "txtThBRavo";
            this.txtThBRavo.Size = new System.Drawing.Size(100, 20);
            this.txtThBRavo.TabIndex = 21;
            // 
            // txtThTop
            // 
            this.txtThTop.Enabled = false;
            this.txtThTop.Location = new System.Drawing.Point(192, 19);
            this.txtThTop.Name = "txtThTop";
            this.txtThTop.Size = new System.Drawing.Size(100, 20);
            this.txtThTop.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(144, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Topline";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(298, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Bravo FIA";
            // 
            // txtThMH
            // 
            this.txtThMH.Enabled = false;
            this.txtThMH.Location = new System.Drawing.Point(38, 19);
            this.txtThMH.Name = "txtThMH";
            this.txtThMH.Size = new System.Drawing.Size(100, 20);
            this.txtThMH.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "MH";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtSplitBravo);
            this.groupBox6.Controls.Add(this.txtSplitTop);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.txtSplitMH);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new System.Drawing.Point(12, 363);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(464, 53);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Split";
            // 
            // txtSplitBravo
            // 
            this.txtSplitBravo.Location = new System.Drawing.Point(355, 19);
            this.txtSplitBravo.Name = "txtSplitBravo";
            this.txtSplitBravo.Size = new System.Drawing.Size(100, 20);
            this.txtSplitBravo.TabIndex = 21;
            // 
            // txtSplitTop
            // 
            this.txtSplitTop.Location = new System.Drawing.Point(192, 19);
            this.txtSplitTop.Name = "txtSplitTop";
            this.txtSplitTop.Size = new System.Drawing.Size(100, 20);
            this.txtSplitTop.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(144, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Topline";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(298, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Bravo FIA";
            // 
            // txtSplitMH
            // 
            this.txtSplitMH.Location = new System.Drawing.Point(38, 19);
            this.txtSplitMH.Name = "txtSplitMH";
            this.txtSplitMH.Size = new System.Drawing.Size(100, 20);
            this.txtSplitMH.TabIndex = 20;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "MH";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.cmbChoose);
            this.groupBox7.Location = new System.Drawing.Point(127, 16);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(167, 45);
            this.groupBox7.TabIndex = 22;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Strategy";
            // 
            // cmbChoose
            // 
            this.cmbChoose.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmbChoose.FormattingEnabled = true;
            this.cmbChoose.Location = new System.Drawing.Point(6, 16);
            this.cmbChoose.Name = "cmbChoose";
            this.cmbChoose.Size = new System.Drawing.Size(155, 21);
            this.cmbChoose.TabIndex = 0;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtCloseBravo);
            this.groupBox8.Controls.Add(this.txtCloseTop);
            this.groupBox8.Controls.Add(this.label1);
            this.groupBox8.Controls.Add(this.label6);
            this.groupBox8.Controls.Add(this.txtCloseMH);
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Location = new System.Drawing.Point(12, 159);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(464, 53);
            this.groupBox8.TabIndex = 23;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Closed Position";
            // 
            // txtCloseBravo
            // 
            this.txtCloseBravo.Enabled = false;
            this.txtCloseBravo.Location = new System.Drawing.Point(355, 19);
            this.txtCloseBravo.Name = "txtCloseBravo";
            this.txtCloseBravo.Size = new System.Drawing.Size(100, 20);
            this.txtCloseBravo.TabIndex = 21;
            // 
            // txtCloseTop
            // 
            this.txtCloseTop.Enabled = false;
            this.txtCloseTop.Location = new System.Drawing.Point(192, 19);
            this.txtCloseTop.Name = "txtCloseTop";
            this.txtCloseTop.Size = new System.Drawing.Size(100, 20);
            this.txtCloseTop.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Topline";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(298, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Bravo FIA";
            // 
            // txtCloseMH
            // 
            this.txtCloseMH.Enabled = false;
            this.txtCloseMH.Location = new System.Drawing.Point(38, 19);
            this.txtCloseMH.Name = "txtCloseMH";
            this.txtCloseMH.Size = new System.Drawing.Size(100, 20);
            this.txtCloseMH.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "MH";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.lblPPLTOP);
            this.groupBox9.Controls.Add(this.lblPPLMH);
            this.groupBox9.Controls.Add(this.label18);
            this.groupBox9.Controls.Add(this.txtPLBravo);
            this.groupBox9.Controls.Add(this.txtPLTop);
            this.groupBox9.Controls.Add(this.label14);
            this.groupBox9.Controls.Add(this.label15);
            this.groupBox9.Controls.Add(this.txtPLMH);
            this.groupBox9.Controls.Add(this.label16);
            this.groupBox9.Location = new System.Drawing.Point(12, 67);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(464, 86);
            this.groupBox9.TabIndex = 24;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "PL";
            // 
            // lblPPLTOP
            // 
            this.lblPPLTOP.AutoSize = true;
            this.lblPPLTOP.Location = new System.Drawing.Point(264, 60);
            this.lblPPLTOP.Name = "lblPPLTOP";
            this.lblPPLTOP.Size = new System.Drawing.Size(0, 13);
            this.lblPPLTOP.TabIndex = 25;
            // 
            // lblPPLMH
            // 
            this.lblPPLMH.AutoSize = true;
            this.lblPPLMH.Location = new System.Drawing.Point(110, 60);
            this.lblPPLMH.Name = "lblPPLMH";
            this.lblPPLMH.Size = new System.Drawing.Size(0, 13);
            this.lblPPLMH.TabIndex = 24;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(8, 60);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(28, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "%PL";
            // 
            // txtPLBravo
            // 
            this.txtPLBravo.Enabled = false;
            this.txtPLBravo.Location = new System.Drawing.Point(355, 19);
            this.txtPLBravo.Name = "txtPLBravo";
            this.txtPLBravo.Size = new System.Drawing.Size(100, 20);
            this.txtPLBravo.TabIndex = 21;
            // 
            // txtPLTop
            // 
            this.txtPLTop.Enabled = false;
            this.txtPLTop.Location = new System.Drawing.Point(192, 19);
            this.txtPLTop.Name = "txtPLTop";
            this.txtPLTop.Size = new System.Drawing.Size(100, 20);
            this.txtPLTop.TabIndex = 22;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(144, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 13);
            this.label14.TabIndex = 11;
            this.label14.Text = "Topline";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(298, 26);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Bravo FIA";
            // 
            // txtPLMH
            // 
            this.txtPLMH.Enabled = false;
            this.txtPLMH.Location = new System.Drawing.Point(38, 19);
            this.txtPLMH.Name = "txtPLMH";
            this.txtPLMH.Size = new System.Drawing.Size(100, 20);
            this.txtPLMH.TabIndex = 20;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "MH";
            // 
            // zfrmDivisaoBoletas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(542, 542);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "zfrmDivisaoBoletas";
            this.Text = "frmDivisaoBoletas";
            this.Load += new System.EventHandler(this.frmDivisaoBoletas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rdMH;
        private System.Windows.Forms.RadioButton rdTop;
        private System.Windows.Forms.RadioButton rdSplit;
        private System.Windows.Forms.RadioButton rdBravo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTicker;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtSplitBravo;
        private System.Windows.Forms.TextBox txtSplitTop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtSplitMH;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ComboBox cmbChoose;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtPosBravo;
        private System.Windows.Forms.TextBox txtPosTop;
        private System.Windows.Forms.TextBox txtPosMH;
        private System.Windows.Forms.TextBox txtThBRavo;
        private System.Windows.Forms.TextBox txtThTop;
        private System.Windows.Forms.TextBox txtThMH;
        private System.Windows.Forms.TextBox txtCloseBravo;
        private System.Windows.Forms.TextBox txtCloseTop;
        private System.Windows.Forms.TextBox txtCloseMH;
        private System.Windows.Forms.TextBox txtPLBravo;
        private System.Windows.Forms.TextBox txtPLTop;
        private System.Windows.Forms.TextBox txtPLMH;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblPPosTop;
        private System.Windows.Forms.Label lblPPosMH;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblPPLTOP;
        private System.Windows.Forms.Label lblPPLMH;
        private System.Windows.Forms.Label label18;
    }
}