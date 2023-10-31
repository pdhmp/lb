namespace NestSim
{
    partial class Main
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
            this.dtpBgnDate = new System.Windows.Forms.DateTimePicker();
            this.lblBgnDate = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblHurdelRate = new System.Windows.Forms.Label();
            this.txtHurdleMin = new System.Windows.Forms.TextBox();
            this.lblBandPercernt = new System.Windows.Forms.Label();
            this.txtBandMin = new System.Windows.Forms.TextBox();
            this.lblHorizontal = new System.Windows.Forms.Label();
            this.txtHorizontalMin = new System.Windows.Forms.TextBox();
            this.lblSideFactor = new System.Windows.Forms.Label();
            this.txtSideFactor = new System.Windows.Forms.TextBox();
            this.chkIntraday = new System.Windows.Forms.CheckBox();
            this.lblExitBar = new System.Windows.Forms.Label();
            this.txtExitBarMin = new System.Windows.Forms.TextBox();
            this.txtHorizontalMax = new System.Windows.Forms.TextBox();
            this.txtBandMax = new System.Windows.Forms.TextBox();
            this.txtHurdleMax = new System.Windows.Forms.TextBox();
            this.txtExitBarInterval = new System.Windows.Forms.TextBox();
            this.txtHorizontalInterval = new System.Windows.Forms.TextBox();
            this.txtBandInterval = new System.Windows.Forms.TextBox();
            this.txtHurdleInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExitBarMax = new System.Windows.Forms.TextBox();
            this.txtGainInterval = new System.Windows.Forms.TextBox();
            this.txtGainMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGainMin = new System.Windows.Forms.TextBox();
            this.txtStopInterval = new System.Windows.Forms.TextBox();
            this.txtStopMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtStopMin = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtExitStrategy = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dtpBgnDate
            // 
            this.dtpBgnDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBgnDate.Location = new System.Drawing.Point(99, 292);
            this.dtpBgnDate.Name = "dtpBgnDate";
            this.dtpBgnDate.Size = new System.Drawing.Size(104, 20);
            this.dtpBgnDate.TabIndex = 20;
            this.dtpBgnDate.Value = new System.DateTime(1996, 1, 1, 0, 0, 0, 0);
            // 
            // lblBgnDate
            // 
            this.lblBgnDate.AutoSize = true;
            this.lblBgnDate.Location = new System.Drawing.Point(21, 296);
            this.lblBgnDate.Name = "lblBgnDate";
            this.lblBgnDate.Size = new System.Drawing.Size(60, 13);
            this.lblBgnDate.TabIndex = 35;
            this.lblBgnDate.Text = "Begin Date";
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(21, 322);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(52, 13);
            this.lblEndDate.TabIndex = 36;
            this.lblEndDate.Text = "End Date";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(99, 318);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(104, 20);
            this.dtpEndDate.TabIndex = 21;
            this.dtpEndDate.Value = new System.DateTime(2009, 12, 31, 0, 0, 0, 0);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(202, 359);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(98, 30);
            this.btnCalculate.TabIndex = 23;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblHurdelRate
            // 
            this.lblHurdelRate.AutoSize = true;
            this.lblHurdelRate.Location = new System.Drawing.Point(21, 35);
            this.lblHurdelRate.Name = "lblHurdelRate";
            this.lblHurdelRate.Size = new System.Drawing.Size(64, 13);
            this.lblHurdelRate.TabIndex = 27;
            this.lblHurdelRate.Text = "Hurdle Rate";
            this.lblHurdelRate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtHurdleMin
            // 
            this.txtHurdleMin.Location = new System.Drawing.Point(99, 32);
            this.txtHurdleMin.Name = "txtHurdleMin";
            this.txtHurdleMin.Size = new System.Drawing.Size(56, 20);
            this.txtHurdleMin.TabIndex = 0;
            this.txtHurdleMin.Text = "0,03";
            // 
            // lblBandPercernt
            // 
            this.lblBandPercernt.AutoSize = true;
            this.lblBandPercernt.Location = new System.Drawing.Point(21, 61);
            this.lblBandPercernt.Name = "lblBandPercernt";
            this.lblBandPercernt.Size = new System.Drawing.Size(72, 13);
            this.lblBandPercernt.TabIndex = 28;
            this.lblBandPercernt.Text = "Band Percent";
            this.lblBandPercernt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBandMin
            // 
            this.txtBandMin.Location = new System.Drawing.Point(99, 58);
            this.txtBandMin.Name = "txtBandMin";
            this.txtBandMin.Size = new System.Drawing.Size(56, 20);
            this.txtBandMin.TabIndex = 3;
            this.txtBandMin.Text = "0,20";
            // 
            // lblHorizontal
            // 
            this.lblHorizontal.AutoSize = true;
            this.lblHorizontal.Location = new System.Drawing.Point(21, 87);
            this.lblHorizontal.Name = "lblHorizontal";
            this.lblHorizontal.Size = new System.Drawing.Size(54, 13);
            this.lblHorizontal.TabIndex = 29;
            this.lblHorizontal.Text = "Horizontal";
            this.lblHorizontal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtHorizontalMin
            // 
            this.txtHorizontalMin.Location = new System.Drawing.Point(99, 84);
            this.txtHorizontalMin.Name = "txtHorizontalMin";
            this.txtHorizontalMin.Size = new System.Drawing.Size(56, 20);
            this.txtHorizontalMin.TabIndex = 6;
            this.txtHorizontalMin.Text = "1,50";
            // 
            // lblSideFactor
            // 
            this.lblSideFactor.AutoSize = true;
            this.lblSideFactor.Location = new System.Drawing.Point(21, 250);
            this.lblSideFactor.Name = "lblSideFactor";
            this.lblSideFactor.Size = new System.Drawing.Size(61, 13);
            this.lblSideFactor.TabIndex = 34;
            this.lblSideFactor.Text = "Side Factor";
            this.lblSideFactor.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSideFactor
            // 
            this.txtSideFactor.Location = new System.Drawing.Point(99, 247);
            this.txtSideFactor.Name = "txtSideFactor";
            this.txtSideFactor.Size = new System.Drawing.Size(56, 20);
            this.txtSideFactor.TabIndex = 19;
            this.txtSideFactor.Text = "1";
            // 
            // chkIntraday
            // 
            this.chkIntraday.AutoSize = true;
            this.chkIntraday.Location = new System.Drawing.Point(24, 355);
            this.chkIntraday.Name = "chkIntraday";
            this.chkIntraday.Size = new System.Drawing.Size(64, 17);
            this.chkIntraday.TabIndex = 22;
            this.chkIntraday.Text = "Intraday";
            this.chkIntraday.UseVisualStyleBackColor = true;
            // 
            // lblExitBar
            // 
            this.lblExitBar.AutoSize = true;
            this.lblExitBar.Location = new System.Drawing.Point(21, 125);
            this.lblExitBar.Name = "lblExitBar";
            this.lblExitBar.Size = new System.Drawing.Size(43, 13);
            this.lblExitBar.TabIndex = 30;
            this.lblExitBar.Text = "Exit Bar";
            this.lblExitBar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtExitBarMin
            // 
            this.txtExitBarMin.Location = new System.Drawing.Point(99, 122);
            this.txtExitBarMin.Name = "txtExitBarMin";
            this.txtExitBarMin.Size = new System.Drawing.Size(56, 20);
            this.txtExitBarMin.TabIndex = 9;
            this.txtExitBarMin.Text = "1";
            // 
            // txtHorizontalMax
            // 
            this.txtHorizontalMax.Location = new System.Drawing.Point(170, 84);
            this.txtHorizontalMax.Name = "txtHorizontalMax";
            this.txtHorizontalMax.Size = new System.Drawing.Size(63, 20);
            this.txtHorizontalMax.TabIndex = 7;
            this.txtHorizontalMax.Text = "1,50";
            // 
            // txtBandMax
            // 
            this.txtBandMax.Location = new System.Drawing.Point(170, 58);
            this.txtBandMax.Name = "txtBandMax";
            this.txtBandMax.Size = new System.Drawing.Size(63, 20);
            this.txtBandMax.TabIndex = 4;
            this.txtBandMax.Text = "0,20";
            // 
            // txtHurdleMax
            // 
            this.txtHurdleMax.Location = new System.Drawing.Point(170, 32);
            this.txtHurdleMax.Name = "txtHurdleMax";
            this.txtHurdleMax.Size = new System.Drawing.Size(63, 20);
            this.txtHurdleMax.TabIndex = 1;
            this.txtHurdleMax.Text = "0,03";
            // 
            // txtExitBarInterval
            // 
            this.txtExitBarInterval.Location = new System.Drawing.Point(239, 122);
            this.txtExitBarInterval.Name = "txtExitBarInterval";
            this.txtExitBarInterval.Size = new System.Drawing.Size(61, 20);
            this.txtExitBarInterval.TabIndex = 11;
            this.txtExitBarInterval.Text = "1";
            // 
            // txtHorizontalInterval
            // 
            this.txtHorizontalInterval.Location = new System.Drawing.Point(239, 84);
            this.txtHorizontalInterval.Name = "txtHorizontalInterval";
            this.txtHorizontalInterval.Size = new System.Drawing.Size(61, 20);
            this.txtHorizontalInterval.TabIndex = 8;
            this.txtHorizontalInterval.Text = "1,50";
            // 
            // txtBandInterval
            // 
            this.txtBandInterval.Location = new System.Drawing.Point(239, 58);
            this.txtBandInterval.Name = "txtBandInterval";
            this.txtBandInterval.Size = new System.Drawing.Size(61, 20);
            this.txtBandInterval.TabIndex = 5;
            this.txtBandInterval.Text = "0,20";
            // 
            // txtHurdleInterval
            // 
            this.txtHurdleInterval.Location = new System.Drawing.Point(239, 32);
            this.txtHurdleInterval.Name = "txtHurdleInterval";
            this.txtHurdleInterval.Size = new System.Drawing.Size(61, 20);
            this.txtHurdleInterval.TabIndex = 2;
            this.txtHurdleInterval.Text = "0,03";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Min";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Max";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Interval";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtExitBarMax
            // 
            this.txtExitBarMax.Location = new System.Drawing.Point(170, 122);
            this.txtExitBarMax.Name = "txtExitBarMax";
            this.txtExitBarMax.Size = new System.Drawing.Size(63, 20);
            this.txtExitBarMax.TabIndex = 10;
            this.txtExitBarMax.Text = "1";
            // 
            // txtGainInterval
            // 
            this.txtGainInterval.Location = new System.Drawing.Point(239, 148);
            this.txtGainInterval.Name = "txtGainInterval";
            this.txtGainInterval.Size = new System.Drawing.Size(61, 20);
            this.txtGainInterval.TabIndex = 14;
            this.txtGainInterval.Text = "0,02";
            // 
            // txtGainMax
            // 
            this.txtGainMax.Location = new System.Drawing.Point(170, 148);
            this.txtGainMax.Name = "txtGainMax";
            this.txtGainMax.Size = new System.Drawing.Size(63, 20);
            this.txtGainMax.TabIndex = 13;
            this.txtGainMax.Text = "0,02";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Gain Limit";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtGainMin
            // 
            this.txtGainMin.Location = new System.Drawing.Point(99, 148);
            this.txtGainMin.Name = "txtGainMin";
            this.txtGainMin.Size = new System.Drawing.Size(56, 20);
            this.txtGainMin.TabIndex = 12;
            this.txtGainMin.Text = "0,02";
            // 
            // txtStopInterval
            // 
            this.txtStopInterval.Location = new System.Drawing.Point(239, 174);
            this.txtStopInterval.Name = "txtStopInterval";
            this.txtStopInterval.Size = new System.Drawing.Size(61, 20);
            this.txtStopInterval.TabIndex = 17;
            this.txtStopInterval.Text = "0,01";
            // 
            // txtStopMax
            // 
            this.txtStopMax.Location = new System.Drawing.Point(170, 174);
            this.txtStopMax.Name = "txtStopMax";
            this.txtStopMax.Size = new System.Drawing.Size(63, 20);
            this.txtStopMax.TabIndex = 16;
            this.txtStopMax.Text = "0,01";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Stop Limit";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtStopMin
            // 
            this.txtStopMin.Location = new System.Drawing.Point(99, 174);
            this.txtStopMin.Name = "txtStopMin";
            this.txtStopMin.Size = new System.Drawing.Size(56, 20);
            this.txtStopMin.TabIndex = 15;
            this.txtStopMin.Text = "0,01";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Exit Strategy";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtExitStrategy
            // 
            this.txtExitStrategy.Location = new System.Drawing.Point(99, 200);
            this.txtExitStrategy.Name = "txtExitStrategy";
            this.txtExitStrategy.Size = new System.Drawing.Size(56, 20);
            this.txtExitStrategy.TabIndex = 18;
            this.txtExitStrategy.Text = "0";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 412);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtExitStrategy);
            this.Controls.Add(this.txtStopInterval);
            this.Controls.Add(this.txtStopMax);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStopMin);
            this.Controls.Add(this.txtGainInterval);
            this.Controls.Add(this.txtGainMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGainMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtExitBarInterval);
            this.Controls.Add(this.txtHorizontalInterval);
            this.Controls.Add(this.txtBandInterval);
            this.Controls.Add(this.txtHurdleInterval);
            this.Controls.Add(this.txtExitBarMax);
            this.Controls.Add(this.txtHorizontalMax);
            this.Controls.Add(this.txtBandMax);
            this.Controls.Add(this.txtHurdleMax);
            this.Controls.Add(this.lblExitBar);
            this.Controls.Add(this.txtExitBarMin);
            this.Controls.Add(this.chkIntraday);
            this.Controls.Add(this.lblSideFactor);
            this.Controls.Add(this.txtSideFactor);
            this.Controls.Add(this.lblHorizontal);
            this.Controls.Add(this.txtHorizontalMin);
            this.Controls.Add(this.lblBandPercernt);
            this.Controls.Add(this.txtBandMin);
            this.Controls.Add(this.lblHurdelRate);
            this.Controls.Add(this.txtHurdleMin);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.lblBgnDate);
            this.Controls.Add(this.dtpBgnDate);
            this.Name = "Main";
            this.Text = "Head and Shoulders";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpBgnDate;
        private System.Windows.Forms.Label lblBgnDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblHurdelRate;
        private System.Windows.Forms.TextBox txtHurdleMin;
        private System.Windows.Forms.Label lblBandPercernt;
        private System.Windows.Forms.TextBox txtBandMin;
        private System.Windows.Forms.Label lblHorizontal;
        private System.Windows.Forms.TextBox txtHorizontalMin;
        private System.Windows.Forms.Label lblSideFactor;
        private System.Windows.Forms.TextBox txtSideFactor;
        private System.Windows.Forms.CheckBox chkIntraday;
        private System.Windows.Forms.Label lblExitBar;
        private System.Windows.Forms.TextBox txtExitBarMin;
        private System.Windows.Forms.TextBox txtHorizontalMax;
        private System.Windows.Forms.TextBox txtBandMax;
        private System.Windows.Forms.TextBox txtHurdleMax;
        private System.Windows.Forms.TextBox txtExitBarInterval;
        private System.Windows.Forms.TextBox txtHorizontalInterval;
        private System.Windows.Forms.TextBox txtBandInterval;
        private System.Windows.Forms.TextBox txtHurdleInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExitBarMax;
        private System.Windows.Forms.TextBox txtGainInterval;
        private System.Windows.Forms.TextBox txtGainMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGainMin;
        private System.Windows.Forms.TextBox txtStopInterval;
        private System.Windows.Forms.TextBox txtStopMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStopMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtExitStrategy;
    }
}

