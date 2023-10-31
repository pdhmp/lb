namespace SGN
{
    partial class frmEdit
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstAtivos = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.groupclass = new System.Windows.Forms.GroupBox();
            this.cmbFonteUp = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbUpFreq = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAssetClass = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbInstrument = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.groupOPT = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.rdPut = new System.Windows.Forms.RadioButton();
            this.rdCall = new System.Windows.Forms.RadioButton();
            this.cmbStrikeCurrency = new System.Windows.Forms.ComboBox();
            this.cmbCurrenPrize = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtiPreco_Exercicio = new System.Windows.Forms.TextBox();
            this.groupOPFUT = new System.Windows.Forms.GroupBox();
            this.txtExpiration = new System.Windows.Forms.DateTimePicker();
            this.cmbObjectTicker = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblId_Ticker = new System.Windows.Forms.Label();
            this.cmbTickerType = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReutersTicker = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBlbTicker = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtiSimbolo = new System.Windows.Forms.TextBox();
            this.txtiNome = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbIssuer = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupLot = new System.Windows.Forms.GroupBox();
            this.txtiLote_Negociacao = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPrimaryEx = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbCurrency = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtiLote_Padrao = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupclass.SuspendLayout();
            this.groupOPT.SuspendLayout();
            this.groupOPFUT.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupLot.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstAtivos);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtsearch);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 331);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ticker";
            // 
            // lstAtivos
            // 
            this.lstAtivos.FormattingEnabled = true;
            this.lstAtivos.Location = new System.Drawing.Point(6, 45);
            this.lstAtivos.Name = "lstAtivos";
            this.lstAtivos.Size = new System.Drawing.Size(175, 277);
            this.lstAtivos.TabIndex = 5;
            this.lstAtivos.Click += new System.EventHandler(this.lstAtivos_Click);
            // 
            // button1
            // 
            this.button1.Image = global::SGN.Properties.Resources.search;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(115, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Search";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.Location = new System.Drawing.Point(6, 19);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(103, 20);
            this.txtsearch.TabIndex = 0;
            this.txtsearch.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.groupclass);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.groupOPT);
            this.groupBox2.Controls.Add(this.groupOPFUT);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupLot);
            this.groupBox2.Location = new System.Drawing.Point(205, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(686, 331);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(555, 302);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 117;
            this.button4.Text = "Cancel";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupclass
            // 
            this.groupclass.Controls.Add(this.cmbFonteUp);
            this.groupclass.Controls.Add(this.label6);
            this.groupclass.Controls.Add(this.cmbUpFreq);
            this.groupclass.Controls.Add(this.label7);
            this.groupclass.Controls.Add(this.cmbAssetClass);
            this.groupclass.Controls.Add(this.label4);
            this.groupclass.Controls.Add(this.cmbInstrument);
            this.groupclass.Controls.Add(this.label3);
            this.groupclass.Location = new System.Drawing.Point(6, 191);
            this.groupclass.Name = "groupclass";
            this.groupclass.Size = new System.Drawing.Size(227, 131);
            this.groupclass.TabIndex = 117;
            this.groupclass.TabStop = false;
            this.groupclass.Text = "Class";
            // 
            // cmbFonteUp
            // 
            this.cmbFonteUp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbFonteUp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbFonteUp.FormattingEnabled = true;
            this.cmbFonteUp.Location = new System.Drawing.Point(85, 99);
            this.cmbFonteUp.Name = "cmbFonteUp";
            this.cmbFonteUp.Size = new System.Drawing.Size(136, 21);
            this.cmbFonteUp.TabIndex = 93;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 94;
            this.label6.Text = "Source data";
            // 
            // cmbUpFreq
            // 
            this.cmbUpFreq.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUpFreq.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUpFreq.FormattingEnabled = true;
            this.cmbUpFreq.Location = new System.Drawing.Point(85, 71);
            this.cmbUpFreq.Name = "cmbUpFreq";
            this.cmbUpFreq.Size = new System.Drawing.Size(136, 21);
            this.cmbUpFreq.TabIndex = 91;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 92;
            this.label7.Text = "Update Freq.";
            // 
            // cmbAssetClass
            // 
            this.cmbAssetClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbAssetClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbAssetClass.FormattingEnabled = true;
            this.cmbAssetClass.Location = new System.Drawing.Point(85, 44);
            this.cmbAssetClass.Name = "cmbAssetClass";
            this.cmbAssetClass.Size = new System.Drawing.Size(136, 21);
            this.cmbAssetClass.TabIndex = 89;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "Asset Class";
            // 
            // cmbInstrument
            // 
            this.cmbInstrument.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbInstrument.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbInstrument.FormattingEnabled = true;
            this.cmbInstrument.Location = new System.Drawing.Point(85, 16);
            this.cmbInstrument.Name = "cmbInstrument";
            this.cmbInstrument.Size = new System.Drawing.Size(136, 21);
            this.cmbInstrument.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "Instrument ";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(555, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 115;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupOPT
            // 
            this.groupOPT.Controls.Add(this.label15);
            this.groupOPT.Controls.Add(this.rdPut);
            this.groupOPT.Controls.Add(this.rdCall);
            this.groupOPT.Controls.Add(this.cmbStrikeCurrency);
            this.groupOPT.Controls.Add(this.cmbCurrenPrize);
            this.groupOPT.Controls.Add(this.label20);
            this.groupOPT.Controls.Add(this.label19);
            this.groupOPT.Controls.Add(this.label18);
            this.groupOPT.Controls.Add(this.txtiPreco_Exercicio);
            this.groupOPT.Location = new System.Drawing.Point(388, 8);
            this.groupOPT.Name = "groupOPT";
            this.groupOPT.Size = new System.Drawing.Size(290, 101);
            this.groupOPT.TabIndex = 111;
            this.groupOPT.TabStop = false;
            this.groupOPT.Text = "Option";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(163, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "Type";
            // 
            // rdPut
            // 
            this.rdPut.AutoSize = true;
            this.rdPut.Location = new System.Drawing.Point(248, 76);
            this.rdPut.Name = "rdPut";
            this.rdPut.Size = new System.Drawing.Size(41, 17);
            this.rdPut.TabIndex = 50;
            this.rdPut.TabStop = true;
            this.rdPut.Text = "Put";
            this.rdPut.UseVisualStyleBackColor = true;
            // 
            // rdCall
            // 
            this.rdCall.AutoSize = true;
            this.rdCall.Location = new System.Drawing.Point(200, 76);
            this.rdCall.Name = "rdCall";
            this.rdCall.Size = new System.Drawing.Size(42, 17);
            this.rdCall.TabIndex = 49;
            this.rdCall.TabStop = true;
            this.rdCall.Text = "Call";
            this.rdCall.UseVisualStyleBackColor = true;
            // 
            // cmbStrikeCurrency
            // 
            this.cmbStrikeCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbStrikeCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbStrikeCurrency.FormattingEnabled = true;
            this.cmbStrikeCurrency.Location = new System.Drawing.Point(90, 46);
            this.cmbStrikeCurrency.Name = "cmbStrikeCurrency";
            this.cmbStrikeCurrency.Size = new System.Drawing.Size(141, 21);
            this.cmbStrikeCurrency.TabIndex = 10;
            // 
            // cmbCurrenPrize
            // 
            this.cmbCurrenPrize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCurrenPrize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCurrenPrize.FormattingEnabled = true;
            this.cmbCurrenPrize.Location = new System.Drawing.Point(90, 19);
            this.cmbCurrenPrize.Name = "cmbCurrenPrize";
            this.cmbCurrenPrize.Size = new System.Drawing.Size(141, 21);
            this.cmbCurrenPrize.TabIndex = 9;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(2, 78);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(82, 13);
            this.label20.TabIndex = 47;
            this.label20.Text = "Exercise Prices ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(9, 22);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(75, 13);
            this.label19.TabIndex = 46;
            this.label19.Text = "Currency Prize";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(11, 49);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 13);
            this.label18.TabIndex = 45;
            this.label18.Text = "Strike currency";
            // 
            // txtiPreco_Exercicio
            // 
            this.txtiPreco_Exercicio.Location = new System.Drawing.Point(90, 73);
            this.txtiPreco_Exercicio.Name = "txtiPreco_Exercicio";
            this.txtiPreco_Exercicio.Size = new System.Drawing.Size(61, 20);
            this.txtiPreco_Exercicio.TabIndex = 12;
            // 
            // groupOPFUT
            // 
            this.groupOPFUT.Controls.Add(this.txtExpiration);
            this.groupOPFUT.Controls.Add(this.cmbObjectTicker);
            this.groupOPFUT.Controls.Add(this.label13);
            this.groupOPFUT.Controls.Add(this.label10);
            this.groupOPFUT.Location = new System.Drawing.Point(388, 115);
            this.groupOPFUT.Name = "groupOPFUT";
            this.groupOPFUT.Size = new System.Drawing.Size(206, 77);
            this.groupOPFUT.TabIndex = 95;
            this.groupOPFUT.TabStop = false;
            this.groupOPFUT.Text = "Future/Option";
            // 
            // txtExpiration
            // 
            this.txtExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtExpiration.Location = new System.Drawing.Point(77, 45);
            this.txtExpiration.Name = "txtExpiration";
            this.txtExpiration.Size = new System.Drawing.Size(117, 20);
            this.txtExpiration.TabIndex = 49;
            // 
            // cmbObjectTicker
            // 
            this.cmbObjectTicker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbObjectTicker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbObjectTicker.FormattingEnabled = true;
            this.cmbObjectTicker.Location = new System.Drawing.Point(77, 18);
            this.cmbObjectTicker.Name = "cmbObjectTicker";
            this.cmbObjectTicker.Size = new System.Drawing.Size(117, 21);
            this.cmbObjectTicker.TabIndex = 8;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 21);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Object Ticker";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Expiration";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblId_Ticker);
            this.groupBox3.Controls.Add(this.cmbTickerType);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtReutersTicker);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtBlbTicker);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtiSimbolo);
            this.groupBox3.Controls.Add(this.txtiNome);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Controls.Add(this.cmbIssuer);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(8, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(374, 176);
            this.groupBox3.TabIndex = 86;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ticker";
            // 
            // lblId_Ticker
            // 
            this.lblId_Ticker.AutoSize = true;
            this.lblId_Ticker.Location = new System.Drawing.Point(15, 25);
            this.lblId_Ticker.Name = "lblId_Ticker";
            this.lblId_Ticker.Size = new System.Drawing.Size(41, 13);
            this.lblId_Ticker.TabIndex = 71;
            this.lblId_Ticker.Text = "label16";
            this.lblId_Ticker.Visible = false;
            // 
            // cmbTickerType
            // 
            this.cmbTickerType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbTickerType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTickerType.Enabled = false;
            this.cmbTickerType.FormattingEnabled = true;
            this.cmbTickerType.Location = new System.Drawing.Point(105, 67);
            this.cmbTickerType.Name = "cmbTickerType";
            this.cmbTickerType.Size = new System.Drawing.Size(261, 21);
            this.cmbTickerType.TabIndex = 69;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(38, 70);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(64, 13);
            this.label21.TabIndex = 70;
            this.label21.Text = "Ticker Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Reuters Ticker";
            // 
            // txtReutersTicker
            // 
            this.txtReutersTicker.Location = new System.Drawing.Point(107, 120);
            this.txtReutersTicker.Name = "txtReutersTicker";
            this.txtReutersTicker.Size = new System.Drawing.Size(168, 20);
            this.txtReutersTicker.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Bloomberg Ticker";
            // 
            // txtBlbTicker
            // 
            this.txtBlbTicker.Location = new System.Drawing.Point(106, 146);
            this.txtBlbTicker.Name = "txtBlbTicker";
            this.txtBlbTicker.Size = new System.Drawing.Size(168, 20);
            this.txtBlbTicker.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(62, 13);
            this.label11.TabIndex = 59;
            this.label11.Text = "Nest Ticker";
            // 
            // txtiSimbolo
            // 
            this.txtiSimbolo.Location = new System.Drawing.Point(107, 94);
            this.txtiSimbolo.Name = "txtiSimbolo";
            this.txtiSimbolo.Size = new System.Drawing.Size(168, 20);
            this.txtiSimbolo.TabIndex = 2;
            // 
            // txtiNome
            // 
            this.txtiNome.Location = new System.Drawing.Point(106, 44);
            this.txtiNome.Name = "txtiNome";
            this.txtiNome.Size = new System.Drawing.Size(260, 20);
            this.txtiNome.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(65, 47);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(35, 13);
            this.label23.TabIndex = 64;
            this.label23.Text = "Name";
            // 
            // cmbIssuer
            // 
            this.cmbIssuer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbIssuer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbIssuer.FormattingEnabled = true;
            this.cmbIssuer.Location = new System.Drawing.Point(106, 16);
            this.cmbIssuer.Name = "cmbIssuer";
            this.cmbIssuer.Size = new System.Drawing.Size(260, 21);
            this.cmbIssuer.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 57;
            this.label9.Text = "Issuer";
            // 
            // groupLot
            // 
            this.groupLot.Controls.Add(this.txtiLote_Negociacao);
            this.groupLot.Controls.Add(this.label5);
            this.groupLot.Controls.Add(this.cmbPrimaryEx);
            this.groupLot.Controls.Add(this.label14);
            this.groupLot.Controls.Add(this.cmbCurrency);
            this.groupLot.Controls.Add(this.label12);
            this.groupLot.Controls.Add(this.txtiLote_Padrao);
            this.groupLot.Controls.Add(this.label8);
            this.groupLot.Location = new System.Drawing.Point(239, 198);
            this.groupLot.Name = "groupLot";
            this.groupLot.Size = new System.Drawing.Size(274, 96);
            this.groupLot.TabIndex = 84;
            this.groupLot.TabStop = false;
            this.groupLot.Text = "Lot";
            // 
            // txtiLote_Negociacao
            // 
            this.txtiLote_Negociacao.Location = new System.Drawing.Point(205, 71);
            this.txtiLote_Negociacao.Name = "txtiLote_Negociacao";
            this.txtiLote_Negociacao.Size = new System.Drawing.Size(60, 20);
            this.txtiLote_Negociacao.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 76;
            this.label5.Text = "Round Lot Size";
            // 
            // cmbPrimaryEx
            // 
            this.cmbPrimaryEx.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPrimaryEx.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPrimaryEx.FormattingEnabled = true;
            this.cmbPrimaryEx.Location = new System.Drawing.Point(107, 41);
            this.cmbPrimaryEx.Name = "cmbPrimaryEx";
            this.cmbPrimaryEx.Size = new System.Drawing.Size(158, 21);
            this.cmbPrimaryEx.TabIndex = 7;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(9, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 72;
            this.label14.Text = "Primary Exchange";
            // 
            // cmbCurrency
            // 
            this.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCurrency.FormattingEnabled = true;
            this.cmbCurrency.Location = new System.Drawing.Point(107, 14);
            this.cmbCurrency.Name = "cmbCurrency";
            this.cmbCurrency.Size = new System.Drawing.Size(158, 21);
            this.cmbCurrency.TabIndex = 6;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(52, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 13);
            this.label12.TabIndex = 70;
            this.label12.Text = "Currency";
            // 
            // txtiLote_Padrao
            // 
            this.txtiLote_Padrao.Location = new System.Drawing.Point(55, 68);
            this.txtiLote_Padrao.Name = "txtiLote_Padrao";
            this.txtiLote_Padrao.Size = new System.Drawing.Size(60, 20);
            this.txtiLote_Padrao.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 69;
            this.label8.Text = "Lot Size";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(531, 205);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 32);
            this.button3.TabIndex = 118;
            this.button3.Text = "Get Historical Data ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 349);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmEdit";
            this.Text = "Instruments";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupclass.ResumeLayout(false);
            this.groupclass.PerformLayout();
            this.groupOPT.ResumeLayout(false);
            this.groupOPT.PerformLayout();
            this.groupOPFUT.ResumeLayout(false);
            this.groupOPFUT.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupLot.ResumeLayout(false);
            this.groupLot.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReutersTicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBlbTicker;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtiSimbolo;
        private System.Windows.Forms.TextBox txtiNome;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbIssuer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupLot;
        private System.Windows.Forms.TextBox txtiLote_Negociacao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPrimaryEx;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbCurrency;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtiLote_Padrao;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupOPFUT;
        private System.Windows.Forms.DateTimePicker txtExpiration;
        private System.Windows.Forms.ComboBox cmbObjectTicker;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupOPT;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.RadioButton rdPut;
        private System.Windows.Forms.RadioButton rdCall;
        private System.Windows.Forms.ComboBox cmbStrikeCurrency;
        private System.Windows.Forms.ComboBox cmbCurrenPrize;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtiPreco_Exercicio;
        private System.Windows.Forms.ComboBox cmbTickerType;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ListBox lstAtivos;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupclass;
        private System.Windows.Forms.ComboBox cmbFonteUp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbUpFreq;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbAssetClass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbInstrument;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblId_Ticker;
        private System.Windows.Forms.Button button3;
    }
}