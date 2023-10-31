namespace LiveBook
{
    partial class frmTrade_Aloc_Override
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTrade_Aloc_Override));
            this.rdMH = new System.Windows.Forms.RadioButton();
            this.rdFund = new System.Windows.Forms.RadioButton();
            this.rdSplit = new System.Windows.Forms.RadioButton();
            this.rdBravo = new System.Windows.Forms.RadioButton();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTicker = new System.Windows.Forms.Label();
            this.lblBroker = new System.Windows.Forms.Label();
            this.grpModel = new System.Windows.Forms.GroupBox();
            this.rdHedge = new System.Windows.Forms.RadioButton();
            this.rdPrev = new System.Windows.Forms.RadioButton();
            this.rdArb = new System.Windows.Forms.RadioButton();
            this.grpFixed = new System.Windows.Forms.GroupBox();
            this.txtSellHeadge = new System.Windows.Forms.TextBox();
            this.txtBuyHeadge = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSellPrev = new System.Windows.Forms.TextBox();
            this.txtBuyPrev = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSellArb = new System.Windows.Forms.TextBox();
            this.txtBuyArb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNetTotal = new System.Windows.Forms.TextBox();
            this.txtSellAloc = new System.Windows.Forms.TextBox();
            this.txtSellRemain = new System.Windows.Forms.TextBox();
            this.txtBuyRemain = new System.Windows.Forms.TextBox();
            this.txtBuyAloc = new System.Windows.Forms.TextBox();
            this.txtBuyTotal = new System.Windows.Forms.TextBox();
            this.txtSellTotal = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSellBravo = new System.Windows.Forms.TextBox();
            this.txtSellNFund = new System.Windows.Forms.TextBox();
            this.txtSellMH = new System.Windows.Forms.TextBox();
            this.txtBuyBravo = new System.Windows.Forms.TextBox();
            this.txtBuyNFund = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBuyMH = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.radModel = new System.Windows.Forms.RadioButton();
            this.radFixed = new System.Windows.Forms.RadioButton();
            this.radNoOverride = new System.Windows.Forms.RadioButton();
            this.radRemoveAloc = new System.Windows.Forms.RadioButton();
            this.grpModel.SuspendLayout();
            this.grpFixed.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdMH
            // 
            this.rdMH.AutoSize = true;
            this.rdMH.Location = new System.Drawing.Point(13, 31);
            this.rdMH.Name = "rdMH";
            this.rdMH.Size = new System.Drawing.Size(42, 17);
            this.rdMH.TabIndex = 2;
            this.rdMH.TabStop = true;
            this.rdMH.Text = "MH";
            this.rdMH.UseVisualStyleBackColor = true;
            // 
            // rdFund
            // 
            this.rdFund.AutoSize = true;
            this.rdFund.Location = new System.Drawing.Point(63, 31);
            this.rdFund.Name = "rdFund";
            this.rdFund.Size = new System.Drawing.Size(71, 17);
            this.rdFund.TabIndex = 3;
            this.rdFund.TabStop = true;
            this.rdFund.Text = "NestFund";
            this.rdFund.UseVisualStyleBackColor = true;
            // 
            // rdSplit
            // 
            this.rdSplit.AutoSize = true;
            this.rdSplit.Enabled = false;
            this.rdSplit.Location = new System.Drawing.Point(391, 30);
            this.rdSplit.Name = "rdSplit";
            this.rdSplit.Size = new System.Drawing.Size(45, 17);
            this.rdSplit.TabIndex = 4;
            this.rdSplit.TabStop = true;
            this.rdSplit.Text = "Split";
            this.rdSplit.UseVisualStyleBackColor = true;
            // 
            // rdBravo
            // 
            this.rdBravo.AutoSize = true;
            this.rdBravo.Location = new System.Drawing.Point(142, 31);
            this.rdBravo.Name = "rdBravo";
            this.rdBravo.Size = new System.Drawing.Size(41, 17);
            this.rdBravo.TabIndex = 5;
            this.rdBravo.TabStop = true;
            this.rdBravo.Text = "FIA";
            this.rdBravo.UseVisualStyleBackColor = true;
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(176, 326);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(75, 23);
            this.cmdOK.TabIndex = 14;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(296, 326);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 15;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Ticker:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Broker";
            // 
            // lblTicker
            // 
            this.lblTicker.AutoSize = true;
            this.lblTicker.Location = new System.Drawing.Point(54, 9);
            this.lblTicker.Name = "lblTicker";
            this.lblTicker.Size = new System.Drawing.Size(52, 13);
            this.lblTicker.TabIndex = 14;
            this.lblTicker.Text = "No Name";
            // 
            // lblBroker
            // 
            this.lblBroker.AutoSize = true;
            this.lblBroker.Location = new System.Drawing.Point(199, 9);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(52, 13);
            this.lblBroker.TabIndex = 15;
            this.lblBroker.Text = "No Name";
            // 
            // grpModel
            // 
            this.grpModel.Controls.Add(this.rdHedge);
            this.grpModel.Controls.Add(this.rdPrev);
            this.grpModel.Controls.Add(this.rdArb);
            this.grpModel.Controls.Add(this.rdMH);
            this.grpModel.Controls.Add(this.rdFund);
            this.grpModel.Controls.Add(this.rdBravo);
            this.grpModel.Controls.Add(this.rdSplit);
            this.grpModel.Enabled = false;
            this.grpModel.Location = new System.Drawing.Point(12, 87);
            this.grpModel.Name = "grpModel";
            this.grpModel.Size = new System.Drawing.Size(551, 75);
            this.grpModel.TabIndex = 17;
            this.grpModel.TabStop = false;
            this.grpModel.Text = "Select Account";
            // 
            // rdHedge
            // 
            this.rdHedge.AutoSize = true;
            this.rdHedge.Location = new System.Drawing.Point(323, 30);
            this.rdHedge.Name = "rdHedge";
            this.rdHedge.Size = new System.Drawing.Size(57, 17);
            this.rdHedge.TabIndex = 8;
            this.rdHedge.TabStop = true;
            this.rdHedge.Text = "Hedge";
            this.rdHedge.UseVisualStyleBackColor = true;
            // 
            // rdPrev
            // 
            this.rdPrev.AutoSize = true;
            this.rdPrev.Location = new System.Drawing.Point(270, 30);
            this.rdPrev.Name = "rdPrev";
            this.rdPrev.Size = new System.Drawing.Size(47, 17);
            this.rdPrev.TabIndex = 7;
            this.rdPrev.TabStop = true;
            this.rdPrev.Text = "Prev";
            this.rdPrev.UseVisualStyleBackColor = true;
            // 
            // rdArb
            // 
            this.rdArb.AutoSize = true;
            this.rdArb.Location = new System.Drawing.Point(222, 31);
            this.rdArb.Name = "rdArb";
            this.rdArb.Size = new System.Drawing.Size(41, 17);
            this.rdArb.TabIndex = 6;
            this.rdArb.TabStop = true;
            this.rdArb.Text = "Arb";
            this.rdArb.UseVisualStyleBackColor = true;
            // 
            // grpFixed
            // 
            this.grpFixed.Controls.Add(this.txtSellHeadge);
            this.grpFixed.Controls.Add(this.txtBuyHeadge);
            this.grpFixed.Controls.Add(this.label5);
            this.grpFixed.Controls.Add(this.txtSellPrev);
            this.grpFixed.Controls.Add(this.txtBuyPrev);
            this.grpFixed.Controls.Add(this.label4);
            this.grpFixed.Controls.Add(this.txtSellArb);
            this.grpFixed.Controls.Add(this.txtBuyArb);
            this.grpFixed.Controls.Add(this.label3);
            this.grpFixed.Controls.Add(this.txtNetTotal);
            this.grpFixed.Controls.Add(this.txtSellAloc);
            this.grpFixed.Controls.Add(this.txtSellRemain);
            this.grpFixed.Controls.Add(this.txtBuyRemain);
            this.grpFixed.Controls.Add(this.txtBuyAloc);
            this.grpFixed.Controls.Add(this.txtBuyTotal);
            this.grpFixed.Controls.Add(this.txtSellTotal);
            this.grpFixed.Controls.Add(this.label19);
            this.grpFixed.Controls.Add(this.label18);
            this.grpFixed.Controls.Add(this.label8);
            this.grpFixed.Controls.Add(this.label6);
            this.grpFixed.Controls.Add(this.label7);
            this.grpFixed.Controls.Add(this.txtSellBravo);
            this.grpFixed.Controls.Add(this.txtSellNFund);
            this.grpFixed.Controls.Add(this.txtSellMH);
            this.grpFixed.Controls.Add(this.txtBuyBravo);
            this.grpFixed.Controls.Add(this.txtBuyNFund);
            this.grpFixed.Controls.Add(this.label10);
            this.grpFixed.Controls.Add(this.label12);
            this.grpFixed.Controls.Add(this.txtBuyMH);
            this.grpFixed.Controls.Add(this.label13);
            this.grpFixed.Enabled = false;
            this.grpFixed.Location = new System.Drawing.Point(11, 191);
            this.grpFixed.Name = "grpFixed";
            this.grpFixed.Size = new System.Drawing.Size(1146, 106);
            this.grpFixed.TabIndex = 23;
            this.grpFixed.TabStop = false;
            this.grpFixed.Text = "Alocation Quantity";
            // 
            // txtSellHeadge
            // 
            this.txtSellHeadge.Location = new System.Drawing.Point(476, 59);
            this.txtSellHeadge.Name = "txtSellHeadge";
            this.txtSellHeadge.Size = new System.Drawing.Size(100, 20);
            this.txtSellHeadge.TabIndex = 11;
            this.txtSellHeadge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellHeadge.TextChanged += new System.EventHandler(this.txtSellHeadge_TextChanged);
            this.txtSellHeadge.Leave += new System.EventHandler(this.txtSellHeadge_Leave);
            // 
            // txtBuyHeadge
            // 
            this.txtBuyHeadge.Location = new System.Drawing.Point(476, 33);
            this.txtBuyHeadge.Name = "txtBuyHeadge";
            this.txtBuyHeadge.Size = new System.Drawing.Size(100, 20);
            this.txtBuyHeadge.TabIndex = 5;
            this.txtBuyHeadge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyHeadge.TextChanged += new System.EventHandler(this.txtBuyHeadge_TextChanged);
            this.txtBuyHeadge.Leave += new System.EventHandler(this.txtBuyHeadge_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(492, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 47;
            this.label5.Text = "Hedge";
            // 
            // txtSellPrev
            // 
            this.txtSellPrev.Location = new System.Drawing.Point(582, 59);
            this.txtSellPrev.Name = "txtSellPrev";
            this.txtSellPrev.Size = new System.Drawing.Size(100, 20);
            this.txtSellPrev.TabIndex = 12;
            this.txtSellPrev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellPrev.TextChanged += new System.EventHandler(this.txtSellPrev_TextChanged);
            this.txtSellPrev.Leave += new System.EventHandler(this.txtSellPrev_Leave);
            // 
            // txtBuyPrev
            // 
            this.txtBuyPrev.Location = new System.Drawing.Point(582, 33);
            this.txtBuyPrev.Name = "txtBuyPrev";
            this.txtBuyPrev.Size = new System.Drawing.Size(100, 20);
            this.txtBuyPrev.TabIndex = 6;
            this.txtBuyPrev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyPrev.TextChanged += new System.EventHandler(this.txtBuyPrev_TextChanged);
            this.txtBuyPrev.Leave += new System.EventHandler(this.txtBuyPrev_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(598, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Prev";
            // 
            // txtSellArb
            // 
            this.txtSellArb.Location = new System.Drawing.Point(266, 59);
            this.txtSellArb.Name = "txtSellArb";
            this.txtSellArb.Size = new System.Drawing.Size(100, 20);
            this.txtSellArb.TabIndex = 9;
            this.txtSellArb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellArb.TextChanged += new System.EventHandler(this.txtSellArb_TextChanged);
            this.txtSellArb.Leave += new System.EventHandler(this.txtSellArb_Leave);
            // 
            // txtBuyArb
            // 
            this.txtBuyArb.Location = new System.Drawing.Point(266, 33);
            this.txtBuyArb.Name = "txtBuyArb";
            this.txtBuyArb.Size = new System.Drawing.Size(100, 20);
            this.txtBuyArb.TabIndex = 3;
            this.txtBuyArb.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyArb.TextChanged += new System.EventHandler(this.txtBuyArb_TextChanged);
            this.txtBuyArb.Leave += new System.EventHandler(this.txtBuyArb_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Arb";
            // 
            // txtNetTotal
            // 
            this.txtNetTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNetTotal.Location = new System.Drawing.Point(807, 78);
            this.txtNetTotal.Name = "txtNetTotal";
            this.txtNetTotal.Size = new System.Drawing.Size(50, 13);
            this.txtNetTotal.TabIndex = 39;
            this.txtNetTotal.Text = "0,00";
            this.txtNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSellAloc
            // 
            this.txtSellAloc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSellAloc.Location = new System.Drawing.Point(695, 59);
            this.txtSellAloc.Name = "txtSellAloc";
            this.txtSellAloc.Size = new System.Drawing.Size(50, 13);
            this.txtSellAloc.TabIndex = 38;
            this.txtSellAloc.Text = "0,00";
            this.txtSellAloc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSellRemain
            // 
            this.txtSellRemain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSellRemain.Location = new System.Drawing.Point(751, 59);
            this.txtSellRemain.Name = "txtSellRemain";
            this.txtSellRemain.Size = new System.Drawing.Size(50, 13);
            this.txtSellRemain.TabIndex = 37;
            this.txtSellRemain.Text = "0,00";
            this.txtSellRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellRemain.TextChanged += new System.EventHandler(this.txtSellRemain_TextChanged);
            // 
            // txtBuyRemain
            // 
            this.txtBuyRemain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuyRemain.Location = new System.Drawing.Point(751, 36);
            this.txtBuyRemain.Name = "txtBuyRemain";
            this.txtBuyRemain.Size = new System.Drawing.Size(50, 13);
            this.txtBuyRemain.TabIndex = 36;
            this.txtBuyRemain.Text = "0,00";
            this.txtBuyRemain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyRemain.TextChanged += new System.EventHandler(this.txtBuyRemain_TextChanged);
            // 
            // txtBuyAloc
            // 
            this.txtBuyAloc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuyAloc.Location = new System.Drawing.Point(695, 36);
            this.txtBuyAloc.Name = "txtBuyAloc";
            this.txtBuyAloc.Size = new System.Drawing.Size(50, 13);
            this.txtBuyAloc.TabIndex = 35;
            this.txtBuyAloc.Text = "0,00";
            this.txtBuyAloc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtBuyTotal
            // 
            this.txtBuyTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBuyTotal.Location = new System.Drawing.Point(807, 36);
            this.txtBuyTotal.Name = "txtBuyTotal";
            this.txtBuyTotal.Size = new System.Drawing.Size(50, 13);
            this.txtBuyTotal.TabIndex = 34;
            this.txtBuyTotal.Text = "0,00";
            this.txtBuyTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSellTotal
            // 
            this.txtSellTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSellTotal.Location = new System.Drawing.Point(807, 59);
            this.txtSellTotal.Name = "txtSellTotal";
            this.txtSellTotal.Size = new System.Drawing.Size(50, 13);
            this.txtSellTotal.TabIndex = 33;
            this.txtSellTotal.Text = "0,00";
            this.txtSellTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(822, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(31, 13);
            this.label19.TabIndex = 29;
            this.label19.Text = "Total";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(748, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = "Remaining";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(696, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Alocated";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Sell";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Buy";
            // 
            // txtSellBravo
            // 
            this.txtSellBravo.Location = new System.Drawing.Point(370, 59);
            this.txtSellBravo.Name = "txtSellBravo";
            this.txtSellBravo.Size = new System.Drawing.Size(100, 20);
            this.txtSellBravo.TabIndex = 10;
            this.txtSellBravo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellBravo.TextChanged += new System.EventHandler(this.txtSellBravo_TextChanged);
            this.txtSellBravo.Leave += new System.EventHandler(this.txtSellBravo_Leave);
            // 
            // txtSellNFund
            // 
            this.txtSellNFund.Location = new System.Drawing.Point(160, 59);
            this.txtSellNFund.Name = "txtSellNFund";
            this.txtSellNFund.Size = new System.Drawing.Size(100, 20);
            this.txtSellNFund.TabIndex = 8;
            this.txtSellNFund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellNFund.TextChanged += new System.EventHandler(this.txtSellTop_TextChanged);
            this.txtSellNFund.Leave += new System.EventHandler(this.txtSellTop_Leave);
            // 
            // txtSellMH
            // 
            this.txtSellMH.Location = new System.Drawing.Point(51, 59);
            this.txtSellMH.Name = "txtSellMH";
            this.txtSellMH.Size = new System.Drawing.Size(100, 20);
            this.txtSellMH.TabIndex = 7;
            this.txtSellMH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellMH.TextChanged += new System.EventHandler(this.txtSellMH_TextChanged);
            this.txtSellMH.Leave += new System.EventHandler(this.txtSellMH_Leave);
            // 
            // txtBuyBravo
            // 
            this.txtBuyBravo.Location = new System.Drawing.Point(370, 33);
            this.txtBuyBravo.Name = "txtBuyBravo";
            this.txtBuyBravo.Size = new System.Drawing.Size(100, 20);
            this.txtBuyBravo.TabIndex = 4;
            this.txtBuyBravo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyBravo.TextChanged += new System.EventHandler(this.txtBuyBravo_TextChanged);
            this.txtBuyBravo.Leave += new System.EventHandler(this.txtBuyBravo_Leave);
            // 
            // txtBuyNFund
            // 
            this.txtBuyNFund.Location = new System.Drawing.Point(160, 33);
            this.txtBuyNFund.Name = "txtBuyNFund";
            this.txtBuyNFund.Size = new System.Drawing.Size(100, 20);
            this.txtBuyNFund.TabIndex = 1;
            this.txtBuyNFund.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyNFund.TextChanged += new System.EventHandler(this.txtBuyTop_TextChanged);
            this.txtBuyNFund.Leave += new System.EventHandler(this.txtBuyTop_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(187, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "NestFund";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(386, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(23, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "FIA";
            // 
            // txtBuyMH
            // 
            this.txtBuyMH.Location = new System.Drawing.Point(51, 33);
            this.txtBuyMH.Name = "txtBuyMH";
            this.txtBuyMH.Size = new System.Drawing.Size(100, 20);
            this.txtBuyMH.TabIndex = 0;
            this.txtBuyMH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBuyMH.TextChanged += new System.EventHandler(this.txtBuyMH_TextChanged);
            this.txtBuyMH.Leave += new System.EventHandler(this.txtBuyMH_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(84, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "MH";
            // 
            // radModel
            // 
            this.radModel.AutoSize = true;
            this.radModel.Location = new System.Drawing.Point(11, 64);
            this.radModel.Name = "radModel";
            this.radModel.Size = new System.Drawing.Size(97, 17);
            this.radModel.TabIndex = 24;
            this.radModel.TabStop = true;
            this.radModel.Text = "Model Override";
            this.radModel.UseVisualStyleBackColor = true;
            this.radModel.CheckedChanged += new System.EventHandler(this.radModel_CheckedChanged);
            // 
            // radFixed
            // 
            this.radFixed.AutoSize = true;
            this.radFixed.Location = new System.Drawing.Point(12, 168);
            this.radFixed.Name = "radFixed";
            this.radFixed.Size = new System.Drawing.Size(135, 17);
            this.radFixed.TabIndex = 25;
            this.radFixed.TabStop = true;
            this.radFixed.Text = "Fixed Quantity Override";
            this.radFixed.UseVisualStyleBackColor = true;
            this.radFixed.CheckedChanged += new System.EventHandler(this.radFixed_CheckedChanged);
            // 
            // radNoOverride
            // 
            this.radNoOverride.AutoSize = true;
            this.radNoOverride.Location = new System.Drawing.Point(11, 41);
            this.radNoOverride.Name = "radNoOverride";
            this.radNoOverride.Size = new System.Drawing.Size(226, 17);
            this.radNoOverride.TabIndex = 26;
            this.radNoOverride.TabStop = true;
            this.radNoOverride.Text = "No Override (use system default allocation)";
            this.radNoOverride.UseVisualStyleBackColor = true;
            this.radNoOverride.CheckedChanged += new System.EventHandler(this.radRemoveOverride_CheckedChanged);
            // 
            // radRemoveAloc
            // 
            this.radRemoveAloc.AutoSize = true;
            this.radRemoveAloc.Location = new System.Drawing.Point(11, 303);
            this.radRemoveAloc.Name = "radRemoveAloc";
            this.radRemoveAloc.Size = new System.Drawing.Size(318, 17);
            this.radRemoveAloc.TabIndex = 13;
            this.radRemoveAloc.TabStop = true;
            this.radRemoveAloc.Text = "Don\'t allocate Override (leave all trades in the Broker account)";
            this.radRemoveAloc.UseVisualStyleBackColor = true;
            this.radRemoveAloc.CheckedChanged += new System.EventHandler(this.radRemoveAloc_CheckedChanged);
            // 
            // frmTrade_Aloc_Override
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1288, 401);
            this.Controls.Add(this.radRemoveAloc);
            this.Controls.Add(this.radNoOverride);
            this.Controls.Add(this.radFixed);
            this.Controls.Add(this.radModel);
            this.Controls.Add(this.grpFixed);
            this.Controls.Add(this.grpModel);
            this.Controls.Add(this.lblBroker);
            this.Controls.Add(this.lblTicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTrade_Aloc_Override";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Trade Alocation Override";
            this.Load += new System.EventHandler(this.frmTrade_Aloc_Override_Load);
            this.grpModel.ResumeLayout(false);
            this.grpModel.PerformLayout();
            this.grpFixed.ResumeLayout(false);
            this.grpFixed.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdBravo;
        private System.Windows.Forms.RadioButton rdMH;
        private System.Windows.Forms.RadioButton rdFund;
        private System.Windows.Forms.RadioButton rdSplit;
        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblTicker;
        public System.Windows.Forms.Label lblBroker;
        private System.Windows.Forms.GroupBox grpModel;
        private System.Windows.Forms.GroupBox grpFixed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSellBravo;
        private System.Windows.Forms.TextBox txtSellNFund;
        private System.Windows.Forms.TextBox txtSellMH;
        private System.Windows.Forms.TextBox txtBuyBravo;
        private System.Windows.Forms.TextBox txtBuyNFund;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBuyMH;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radModel;
        private System.Windows.Forms.RadioButton radFixed;
        private System.Windows.Forms.RadioButton radNoOverride;
        private System.Windows.Forms.RadioButton radRemoveAloc;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBuyTotal;
        private System.Windows.Forms.TextBox txtSellTotal;
        private System.Windows.Forms.TextBox txtSellAloc;
        private System.Windows.Forms.TextBox txtSellRemain;
        private System.Windows.Forms.TextBox txtBuyRemain;
        private System.Windows.Forms.TextBox txtBuyAloc;
        private System.Windows.Forms.TextBox txtNetTotal;
        private System.Windows.Forms.RadioButton rdArb;
        private System.Windows.Forms.TextBox txtSellArb;
        private System.Windows.Forms.TextBox txtBuyArb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdHedge;
        private System.Windows.Forms.RadioButton rdPrev;
        private System.Windows.Forms.TextBox txtSellHeadge;
        private System.Windows.Forms.TextBox txtBuyHeadge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSellPrev;
        private System.Windows.Forms.TextBox txtBuyPrev;
        private System.Windows.Forms.Label label4;
    }
}