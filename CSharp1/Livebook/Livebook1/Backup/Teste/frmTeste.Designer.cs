namespace SGN
{
    partial class frmTeste
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgPositions = new System.Windows.Forms.DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).BeginInit();
            this.Rendimento.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgPositions
            // 
            this.dgPositions.AllowUserToAddRows = false;
            this.dgPositions.AllowUserToDeleteRows = false;
            this.dgPositions.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPositions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPositions.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgPositions.Location = new System.Drawing.Point(12, 134);
            this.dgPositions.Name = "dgPositions";
            this.dgPositions.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPositions.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPositions.RowHeadersVisible = false;
            this.dgPositions.RowHeadersWidth = 4;
            this.dgPositions.RowTemplate.Height = 15;
            this.dgPositions.Size = new System.Drawing.Size(1074, 667);
            this.dgPositions.TabIndex = 16;
            // 
            // Rendimento
            // 
            this.Rendimento.BackColor = System.Drawing.SystemColors.Control;
            this.Rendimento.Controls.Add(this.txtmtm);
            this.Rendimento.Controls.Add(this.label29);
            this.Rendimento.Controls.Add(this.txtmtm_perc);
            this.Rendimento.Controls.Add(this.label33);
            this.Rendimento.Location = new System.Drawing.Point(408, 13);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
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
            this.cmdrefresh.Location = new System.Drawing.Point(619, 84);
            this.cmdrefresh.Name = "cmdrefresh";
            this.cmdrefresh.Size = new System.Drawing.Size(105, 32);
            this.cmdrefresh.TabIndex = 13;
            this.cmdrefresh.Text = "Refresh";
            this.cmdrefresh.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(415, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 11);
            this.label5.TabIndex = 12;
            this.label5.Text = "Choose Portfolio";
            // 
            // cmbChoosePortfolio
            // 
            this.cmbChoosePortfolio.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbChoosePortfolio.FormattingEnabled = true;
            this.cmbChoosePortfolio.Location = new System.Drawing.Point(417, 96);
            this.cmbChoosePortfolio.Name = "cmbChoosePortfolio";
            this.cmbChoosePortfolio.Size = new System.Drawing.Size(174, 19);
            this.cmbChoosePortfolio.TabIndex = 11;
            // 
            // frmTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 1002);
            this.Controls.Add(this.dgPositions);
            this.Controls.Add(this.Rendimento);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdrefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbChoosePortfolio);
            this.Name = "frmTeste";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teste Novo Posi�oes";
            this.Load += new System.EventHandler(this.frmTeste_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPositions)).EndInit();
            this.Rendimento.ResumeLayout(false);
            this.Rendimento.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPositions;
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
    }
}