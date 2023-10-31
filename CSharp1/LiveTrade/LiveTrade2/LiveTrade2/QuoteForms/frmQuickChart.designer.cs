namespace LiveTrade2
{
    partial class frmQuickChart
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
            this.zgcQuickChart = new ZedGraph.ZedGraphControl();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.radYTD = new System.Windows.Forms.RadioButton();
            this.radMTD = new System.Windows.Forms.RadioButton();
            this.radALL = new System.Windows.Forms.RadioButton();
            this.radAdjPrice = new System.Windows.Forms.RadioButton();
            this.radTotReturn = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rad12m = new System.Windows.Forms.RadioButton();
            this.rad3m = new System.Windows.Forms.RadioButton();
            this.radCustom = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtHorizLine = new LiveTrade2.TransparentTextBox();
            this.txtVertLine = new LiveTrade2.TransparentTextBox();
            this.labPrice = new System.Windows.Forms.Label();
            this.labDate = new System.Windows.Forms.Label();
            this.labVolume = new System.Windows.Forms.Label();
            this.cmdDrawTrend = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // zgcQuickChart
            // 
            this.zgcQuickChart.AllowDrop = true;
            this.zgcQuickChart.IsEnableHZoom = false;
            this.zgcQuickChart.IsEnableVZoom = false;
            this.zgcQuickChart.Location = new System.Drawing.Point(0, 26);
            this.zgcQuickChart.Name = "zgcQuickChart";
            this.zgcQuickChart.ScrollGrace = 0D;
            this.zgcQuickChart.ScrollMaxX = 0D;
            this.zgcQuickChart.ScrollMaxY = 0D;
            this.zgcQuickChart.ScrollMaxY2 = 0D;
            this.zgcQuickChart.ScrollMinX = 0D;
            this.zgcQuickChart.ScrollMinY = 0D;
            this.zgcQuickChart.ScrollMinY2 = 0D;
            this.zgcQuickChart.Size = new System.Drawing.Size(640, 385);
            this.zgcQuickChart.TabIndex = 26;
            this.zgcQuickChart.Visible = false;
            this.zgcQuickChart.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcQuickChart_MouseDownEvent);
            this.zgcQuickChart.MouseUpEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcQuickChart_MouseUpEvent);
            this.zgcQuickChart.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgcQuickChart_MouseMoveEvent);
            this.zgcQuickChart.DragDrop += new System.Windows.Forms.DragEventHandler(this.zgcQuickChart_DragDrop);
            this.zgcQuickChart.DragEnter += new System.Windows.Forms.DragEventHandler(this.zgcQuickChart_DragEnter);
            this.zgcQuickChart.DragOver += new System.Windows.Forms.DragEventHandler(this.zgcQuickChart_DragOver);
            this.zgcQuickChart.Paint += new System.Windows.Forms.PaintEventHandler(this.zgcQuickChart_Paint);
            this.zgcQuickChart.MouseHover += new System.EventHandler(this.zgcQuickChart_MouseHover);
            this.zgcQuickChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zgcQuickChart_MouseMove);
            this.zgcQuickChart.Resize += new System.EventHandler(this.zgcQuickChart_Resize);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // radYTD
            // 
            this.radYTD.AutoSize = true;
            this.radYTD.Location = new System.Drawing.Point(130, 3);
            this.radYTD.Name = "radYTD";
            this.radYTD.Size = new System.Drawing.Size(47, 17);
            this.radYTD.TabIndex = 27;
            this.radYTD.Text = "YTD";
            this.radYTD.UseVisualStyleBackColor = true;
            this.radYTD.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // radMTD
            // 
            this.radMTD.AutoSize = true;
            this.radMTD.Location = new System.Drawing.Point(183, 3);
            this.radMTD.Name = "radMTD";
            this.radMTD.Size = new System.Drawing.Size(49, 17);
            this.radMTD.TabIndex = 28;
            this.radMTD.Text = "MTD";
            this.radMTD.UseVisualStyleBackColor = true;
            this.radMTD.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // radALL
            // 
            this.radALL.AutoSize = true;
            this.radALL.Location = new System.Drawing.Point(80, 3);
            this.radALL.Name = "radALL";
            this.radALL.Size = new System.Drawing.Size(44, 17);
            this.radALL.TabIndex = 29;
            this.radALL.Text = "ALL";
            this.radALL.UseVisualStyleBackColor = true;
            this.radALL.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // radAdjPrice
            // 
            this.radAdjPrice.AutoSize = true;
            this.radAdjPrice.Checked = true;
            this.radAdjPrice.Location = new System.Drawing.Point(3, 3);
            this.radAdjPrice.Name = "radAdjPrice";
            this.radAdjPrice.Size = new System.Drawing.Size(70, 17);
            this.radAdjPrice.TabIndex = 30;
            this.radAdjPrice.TabStop = true;
            this.radAdjPrice.Text = "Adj. Price";
            this.radAdjPrice.UseVisualStyleBackColor = true;
            this.radAdjPrice.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // radTotReturn
            // 
            this.radTotReturn.AutoSize = true;
            this.radTotReturn.Location = new System.Drawing.Point(79, 3);
            this.radTotReturn.Name = "radTotReturn";
            this.radTotReturn.Size = new System.Drawing.Size(79, 17);
            this.radTotReturn.TabIndex = 31;
            this.radTotReturn.TabStop = true;
            this.radTotReturn.Text = "Tot. Return";
            this.radTotReturn.UseVisualStyleBackColor = true;
            this.radTotReturn.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rad12m);
            this.panel1.Controls.Add(this.rad3m);
            this.panel1.Controls.Add(this.radCustom);
            this.panel1.Controls.Add(this.radALL);
            this.panel1.Controls.Add(this.radYTD);
            this.panel1.Controls.Add(this.radMTD);
            this.panel1.Location = new System.Drawing.Point(6, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(339, 23);
            this.panel1.TabIndex = 32;
            // 
            // rad12m
            // 
            this.rad12m.AutoSize = true;
            this.rad12m.Location = new System.Drawing.Point(283, 3);
            this.rad12m.Name = "rad12m";
            this.rad12m.Size = new System.Drawing.Size(45, 17);
            this.rad12m.TabIndex = 32;
            this.rad12m.Text = "12m";
            this.rad12m.UseVisualStyleBackColor = true;
            this.rad12m.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // rad3m
            // 
            this.rad3m.AutoSize = true;
            this.rad3m.Checked = true;
            this.rad3m.Location = new System.Drawing.Point(238, 3);
            this.rad3m.Name = "rad3m";
            this.rad3m.Size = new System.Drawing.Size(39, 17);
            this.rad3m.TabIndex = 31;
            this.rad3m.TabStop = true;
            this.rad3m.Text = "3m";
            this.rad3m.UseVisualStyleBackColor = true;
            this.rad3m.CheckedChanged += new System.EventHandler(this.radRedraw_CheckedChanged);
            // 
            // radCustom
            // 
            this.radCustom.AutoSize = true;
            this.radCustom.Enabled = false;
            this.radCustom.Location = new System.Drawing.Point(6, 3);
            this.radCustom.Name = "radCustom";
            this.radCustom.Size = new System.Drawing.Size(60, 17);
            this.radCustom.TabIndex = 30;
            this.radCustom.Text = "Custom";
            this.radCustom.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radAdjPrice);
            this.panel2.Controls.Add(this.radTotReturn);
            this.panel2.Location = new System.Drawing.Point(351, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(163, 23);
            this.panel2.TabIndex = 33;
            // 
            // txtHorizLine
            // 
            this.txtHorizLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHorizLine.Location = new System.Drawing.Point(202, 129);
            this.txtHorizLine.Multiline = true;
            this.txtHorizLine.Name = "txtHorizLine";
            this.txtHorizLine.Size = new System.Drawing.Size(36, 98);
            this.txtHorizLine.TabIndex = 34;
            // 
            // txtVertLine
            // 
            this.txtVertLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVertLine.Location = new System.Drawing.Point(297, 129);
            this.txtVertLine.Multiline = true;
            this.txtVertLine.Name = "txtVertLine";
            this.txtVertLine.Size = new System.Drawing.Size(36, 98);
            this.txtVertLine.TabIndex = 35;
            // 
            // labPrice
            // 
            this.labPrice.AutoSize = true;
            this.labPrice.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.labPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labPrice.Location = new System.Drawing.Point(12, 104);
            this.labPrice.Name = "labPrice";
            this.labPrice.Size = new System.Drawing.Size(30, 15);
            this.labPrice.TabIndex = 36;
            this.labPrice.Text = "0,00";
            this.labPrice.Visible = false;
            // 
            // labDate
            // 
            this.labDate.AutoSize = true;
            this.labDate.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.labDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labDate.Location = new System.Drawing.Point(363, 368);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(41, 15);
            this.labDate.TabIndex = 37;
            this.labDate.Text = "31-mar";
            this.labDate.Visible = false;
            // 
            // labVolume
            // 
            this.labVolume.AutoSize = true;
            this.labVolume.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.labVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labVolume.Location = new System.Drawing.Point(12, 330);
            this.labVolume.Name = "labVolume";
            this.labVolume.Size = new System.Drawing.Size(42, 15);
            this.labVolume.TabIndex = 38;
            this.labVolume.Text = "10.000";
            this.labVolume.Visible = false;
            // 
            // cmdDrawTrend
            // 
            this.cmdDrawTrend.Location = new System.Drawing.Point(520, 0);
            this.cmdDrawTrend.Name = "cmdDrawTrend";
            this.cmdDrawTrend.Size = new System.Drawing.Size(107, 23);
            this.cmdDrawTrend.TabIndex = 39;
            this.cmdDrawTrend.Text = "Draw TrendLine";
            this.cmdDrawTrend.UseVisualStyleBackColor = true;
            this.cmdDrawTrend.Click += new System.EventHandler(this.cmdDrawTrend_Click);
            // 
            // frmQuickChart
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(639, 408);
            this.Controls.Add(this.cmdDrawTrend);
            this.Controls.Add(this.labVolume);
            this.Controls.Add(this.labDate);
            this.Controls.Add(this.labPrice);
            this.Controls.Add(this.txtVertLine);
            this.Controls.Add(this.txtHorizLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.zgcQuickChart);
            this.Controls.Add(this.panel2);
            this.Name = "frmQuickChart";
            this.Text = "Simple Chart";
            this.Activated += new System.EventHandler(this.frmQuickChart_Activated);
            this.Load += new System.EventHandler(this.frmQuickChart_Load);
            this.SizeChanged += new System.EventHandler(this.frmQuickChart_SizeChanged);
            this.Resize += new System.EventHandler(this.frmPerfChart_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl zgcQuickChart;
        private System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.RadioButton radYTD;
        private System.Windows.Forms.RadioButton radMTD;
        private System.Windows.Forms.RadioButton radALL;
        private System.Windows.Forms.RadioButton radAdjPrice;
        private System.Windows.Forms.RadioButton radTotReturn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radCustom;
        private System.Windows.Forms.RadioButton rad3m;
        private TransparentTextBox txtHorizLine;
        private TransparentTextBox txtVertLine;
        private System.Windows.Forms.RadioButton rad12m;
        private System.Windows.Forms.Label labPrice;
        private System.Windows.Forms.Label labDate;
        private System.Windows.Forms.Label labVolume;
        private System.Windows.Forms.Button cmdDrawTrend;


    }
}