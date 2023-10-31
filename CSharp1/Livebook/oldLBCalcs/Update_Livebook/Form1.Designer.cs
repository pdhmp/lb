namespace Update_Livebook
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmdStart = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.txtThreadCounter = new System.Windows.Forms.Label();
            this.timer6 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblupdate6 = new System.Windows.Forms.Label();
            this.lblMessage6 = new System.Windows.Forms.Label();
            this.timer7 = new System.Windows.Forms.Timer(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblMessage8 = new System.Windows.Forms.Label();
            this.lblupdate8 = new System.Windows.Forms.Label();
            this.lblMessage9 = new System.Windows.Forms.Label();
            this.lblupdate9 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblMessage7 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblupdate7 = new System.Windows.Forms.Label();
            this.timer8 = new System.Windows.Forms.Timer(this.components);
            this.timer9 = new System.Windows.Forms.Timer(this.components);
            this.timer10 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.lblupdate10 = new System.Windows.Forms.Label();
            this.lblMessage10 = new System.Windows.Forms.Label();
            this.timer11 = new System.Windows.Forms.Timer(this.components);
            this.label12 = new System.Windows.Forms.Label();
            this.lblMessage11 = new System.Windows.Forms.Label();
            this.lblupdate11 = new System.Windows.Forms.Label();
            this.timer12 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTimer3 = new System.Windows.Forms.TextBox();
            this.txtTimer2 = new System.Windows.Forms.TextBox();
            this.txtTimer4 = new System.Windows.Forms.TextBox();
            this.txtTimer5 = new System.Windows.Forms.TextBox();
            this.txtTimer1 = new System.Windows.Forms.TextBox();
            this.txtTimer10 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblupdate5 = new System.Windows.Forms.Label();
            this.lblMessage5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage4 = new System.Windows.Forms.Label();
            this.lblupdate4 = new System.Windows.Forms.Label();
            this.lblMessage3 = new System.Windows.Forms.Label();
            this.lblupdate3 = new System.Windows.Forms.Label();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.lblupdate2 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblupdate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTimer9 = new System.Windows.Forms.TextBox();
            this.txtTimer8 = new System.Windows.Forms.TextBox();
            this.txtTimer7 = new System.Windows.Forms.TextBox();
            this.txtTimer6 = new System.Windows.Forms.TextBox();
            this.txtTimer11 = new System.Windows.Forms.TextBox();
            this.timer11_1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdStart
            // 
            this.cmdStart.Location = new System.Drawing.Point(408, 193);
            this.cmdStart.Name = "cmdStart";
            this.cmdStart.Size = new System.Drawing.Size(75, 23);
            this.cmdStart.TabIndex = 12;
            this.cmdStart.Text = "Start";
            this.cmdStart.UseVisualStyleBackColor = true;
            this.cmdStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last Update:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Service Message:";
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Interval = 120000;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 1000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // timer5
            // 
            this.timer5.Interval = 1000;
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(523, 193);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "Stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtThreadCounter
            // 
            this.txtThreadCounter.AutoSize = true;
            this.txtThreadCounter.Location = new System.Drawing.Point(19, 193);
            this.txtThreadCounter.Name = "txtThreadCounter";
            this.txtThreadCounter.Size = new System.Drawing.Size(46, 13);
            this.txtThreadCounter.TabIndex = 16;
            this.txtThreadCounter.Text = "Threads";
            // 
            // timer6
            // 
            this.timer6.Interval = 1000;
            this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Insert FIX:";
            // 
            // lblupdate6
            // 
            this.lblupdate6.AutoSize = true;
            this.lblupdate6.Location = new System.Drawing.Point(524, 153);
            this.lblupdate6.Name = "lblupdate6";
            this.lblupdate6.Size = new System.Drawing.Size(39, 13);
            this.lblupdate6.TabIndex = 21;
            this.lblupdate6.Text = "Timer6";
            // 
            // lblMessage6
            // 
            this.lblMessage6.AutoSize = true;
            this.lblMessage6.Location = new System.Drawing.Point(583, 153);
            this.lblMessage6.Name = "lblMessage6";
            this.lblMessage6.Size = new System.Drawing.Size(39, 13);
            this.lblMessage6.TabIndex = 20;
            this.lblMessage6.Text = "Timer6";
            // 
            // timer7
            // 
            this.timer7.Interval = 1000;
            this.timer7.Tick += new System.EventHandler(this.timer7_Tick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 36;
            this.label8.Text = "Hist Cost Close:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 13);
            this.label10.TabIndex = 34;
            this.label10.Text = "Hist Calc Fields:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Hist Fixed Fields:";
            // 
            // lblMessage8
            // 
            this.lblMessage8.AutoSize = true;
            this.lblMessage8.Location = new System.Drawing.Point(219, 40);
            this.lblMessage8.Name = "lblMessage8";
            this.lblMessage8.Size = new System.Drawing.Size(39, 13);
            this.lblMessage8.TabIndex = 32;
            this.lblMessage8.Text = "Timer8";
            // 
            // lblupdate8
            // 
            this.lblupdate8.AutoSize = true;
            this.lblupdate8.Location = new System.Drawing.Point(161, 40);
            this.lblupdate8.Name = "lblupdate8";
            this.lblupdate8.Size = new System.Drawing.Size(39, 13);
            this.lblupdate8.TabIndex = 31;
            this.lblupdate8.Text = "Timer8";
            // 
            // lblMessage9
            // 
            this.lblMessage9.AutoSize = true;
            this.lblMessage9.Location = new System.Drawing.Point(219, 65);
            this.lblMessage9.Name = "lblMessage9";
            this.lblMessage9.Size = new System.Drawing.Size(39, 13);
            this.lblMessage9.TabIndex = 28;
            this.lblMessage9.Text = "Timer9";
            // 
            // lblupdate9
            // 
            this.lblupdate9.AutoSize = true;
            this.lblupdate9.Location = new System.Drawing.Point(161, 65);
            this.lblupdate9.Name = "lblupdate9";
            this.lblupdate9.Size = new System.Drawing.Size(39, 13);
            this.lblupdate9.TabIndex = 27;
            this.lblupdate9.Text = "Timer9";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(474, 7);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 13);
            this.label18.TabIndex = 26;
            this.label18.Text = "Service Message:";
            // 
            // lblMessage7
            // 
            this.lblMessage7.AutoSize = true;
            this.lblMessage7.Location = new System.Drawing.Point(220, 15);
            this.lblMessage7.Name = "lblMessage7";
            this.lblMessage7.Size = new System.Drawing.Size(39, 13);
            this.lblMessage7.TabIndex = 25;
            this.lblMessage7.Text = "Timer7";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(363, 7);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 13);
            this.label20.TabIndex = 24;
            this.label20.Text = "Last Update:";
            // 
            // lblupdate7
            // 
            this.lblupdate7.AutoSize = true;
            this.lblupdate7.Location = new System.Drawing.Point(161, 15);
            this.lblupdate7.Name = "lblupdate7";
            this.lblupdate7.Size = new System.Drawing.Size(39, 13);
            this.lblupdate7.TabIndex = 23;
            this.lblupdate7.Text = "Timer7";
            // 
            // timer8
            // 
            this.timer8.Interval = 10000;
            this.timer8.Tick += new System.EventHandler(this.timer8_Tick);
            // 
            // timer9
            // 
            this.timer9.Interval = 10000;
            this.timer9.Tick += new System.EventHandler(this.timer9_Tick);
            // 
            // timer10
            // 
            this.timer10.Interval = 2000;
            this.timer10.Tick += new System.EventHandler(this.timer10_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 39;
            this.label9.Text = "New Quantities:";
            // 
            // lblupdate10
            // 
            this.lblupdate10.AutoSize = true;
            this.lblupdate10.Location = new System.Drawing.Point(179, 140);
            this.lblupdate10.Name = "lblupdate10";
            this.lblupdate10.Size = new System.Drawing.Size(45, 13);
            this.lblupdate10.TabIndex = 38;
            this.lblupdate10.Text = "Timer10";
            // 
            // lblMessage10
            // 
            this.lblMessage10.AutoSize = true;
            this.lblMessage10.Location = new System.Drawing.Point(246, 140);
            this.lblMessage10.Name = "lblMessage10";
            this.lblMessage10.Size = new System.Drawing.Size(45, 13);
            this.lblMessage10.TabIndex = 37;
            this.lblMessage10.Text = "Timer10";
            // 
            // timer11
            // 
            this.timer11.Interval = 1000000;
            this.timer11.Tick += new System.EventHandler(this.timer11_Tick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(358, 127);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(93, 13);
            this.label12.TabIndex = 42;
            this.label12.Text = "Calc Total Return:";
            // 
            // lblMessage11
            // 
            this.lblMessage11.AutoSize = true;
            this.lblMessage11.Location = new System.Drawing.Point(583, 127);
            this.lblMessage11.Name = "lblMessage11";
            this.lblMessage11.Size = new System.Drawing.Size(45, 13);
            this.lblMessage11.TabIndex = 41;
            this.lblMessage11.Text = "Timer11";
            // 
            // lblupdate11
            // 
            this.lblupdate11.AutoSize = true;
            this.lblupdate11.Location = new System.Drawing.Point(524, 127);
            this.lblupdate11.Name = "lblupdate11";
            this.lblupdate11.Size = new System.Drawing.Size(45, 13);
            this.lblupdate11.TabIndex = 40;
            this.lblupdate11.Text = "Timer11";
            // 
            // timer12
            // 
            this.timer12.Tick += new System.EventHandler(this.timer12_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTimer3);
            this.groupBox1.Controls.Add(this.txtTimer2);
            this.groupBox1.Controls.Add(this.txtTimer4);
            this.groupBox1.Controls.Add(this.txtTimer5);
            this.groupBox1.Controls.Add(this.txtTimer1);
            this.groupBox1.Controls.Add(this.txtTimer10);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblupdate5);
            this.groupBox1.Controls.Add(this.lblMessage5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblupdate10);
            this.groupBox1.Controls.Add(this.label51);
            this.groupBox1.Controls.Add(this.lblMessage10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblMessage4);
            this.groupBox1.Controls.Add(this.lblupdate4);
            this.groupBox1.Controls.Add(this.lblMessage3);
            this.groupBox1.Controls.Add(this.lblupdate3);
            this.groupBox1.Controls.Add(this.lblMessage2);
            this.groupBox1.Controls.Add(this.lblupdate2);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.lblupdate);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 165);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rt Position";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txtTimer3
            // 
            this.txtTimer3.Location = new System.Drawing.Point(108, 112);
            this.txtTimer3.Name = "txtTimer3";
            this.txtTimer3.Size = new System.Drawing.Size(63, 20);
            this.txtTimer3.TabIndex = 5;
            this.txtTimer3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer3.Leave += new System.EventHandler(this.txtTimer3_Leave);
            this.txtTimer3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer2
            // 
            this.txtTimer2.Location = new System.Drawing.Point(108, 87);
            this.txtTimer2.Name = "txtTimer2";
            this.txtTimer2.Size = new System.Drawing.Size(63, 20);
            this.txtTimer2.TabIndex = 4;
            this.txtTimer2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer2.Leave += new System.EventHandler(this.txtTimer2_Leave);
            this.txtTimer2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer4
            // 
            this.txtTimer4.Location = new System.Drawing.Point(108, 37);
            this.txtTimer4.Name = "txtTimer4";
            this.txtTimer4.Size = new System.Drawing.Size(63, 20);
            this.txtTimer4.TabIndex = 2;
            this.txtTimer4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer4.Leave += new System.EventHandler(this.txtTimer4_Leave);
            this.txtTimer4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer5
            // 
            this.txtTimer5.Location = new System.Drawing.Point(108, 62);
            this.txtTimer5.Name = "txtTimer5";
            this.txtTimer5.Size = new System.Drawing.Size(63, 20);
            this.txtTimer5.TabIndex = 3;
            this.txtTimer5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer5.Leave += new System.EventHandler(this.txtTimer5_Leave);
            this.txtTimer5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer1
            // 
            this.txtTimer1.Location = new System.Drawing.Point(108, 12);
            this.txtTimer1.Name = "txtTimer1";
            this.txtTimer1.Size = new System.Drawing.Size(63, 20);
            this.txtTimer1.TabIndex = 1;
            this.txtTimer1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer1.Leave += new System.EventHandler(this.txtTimer1_Leave);
            this.txtTimer1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer10
            // 
            this.txtTimer10.Location = new System.Drawing.Point(108, 137);
            this.txtTimer10.Name = "txtTimer10";
            this.txtTimer10.Size = new System.Drawing.Size(63, 20);
            this.txtTimer10.TabIndex = 6;
            this.txtTimer10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer10.Leave += new System.EventHandler(this.txtTimer10_Leave);
            this.txtTimer10.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Clost Close:";
            // 
            // lblupdate5
            // 
            this.lblupdate5.AutoSize = true;
            this.lblupdate5.Location = new System.Drawing.Point(179, 65);
            this.lblupdate5.Name = "lblupdate5";
            this.lblupdate5.Size = new System.Drawing.Size(39, 13);
            this.lblupdate5.TabIndex = 33;
            this.lblupdate5.Text = "Timer5";
            // 
            // lblMessage5
            // 
            this.lblMessage5.AutoSize = true;
            this.lblMessage5.Location = new System.Drawing.Point(246, 65);
            this.lblMessage5.Name = "lblMessage5";
            this.lblMessage5.Size = new System.Drawing.Size(39, 13);
            this.lblMessage5.TabIndex = 32;
            this.lblMessage5.Text = "Timer5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Rec Price Options:";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(7, 115);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(77, 13);
            this.label51.TabIndex = 30;
            this.label51.Text = "New Positions:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Oldest Updated:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Recent Price:";
            // 
            // lblMessage4
            // 
            this.lblMessage4.AutoSize = true;
            this.lblMessage4.Location = new System.Drawing.Point(246, 40);
            this.lblMessage4.Name = "lblMessage4";
            this.lblMessage4.Size = new System.Drawing.Size(39, 13);
            this.lblMessage4.TabIndex = 27;
            this.lblMessage4.Text = "Timer4";
            // 
            // lblupdate4
            // 
            this.lblupdate4.AutoSize = true;
            this.lblupdate4.Location = new System.Drawing.Point(179, 40);
            this.lblupdate4.Name = "lblupdate4";
            this.lblupdate4.Size = new System.Drawing.Size(39, 13);
            this.lblupdate4.TabIndex = 26;
            this.lblupdate4.Text = "Timer4";
            // 
            // lblMessage3
            // 
            this.lblMessage3.AutoSize = true;
            this.lblMessage3.Location = new System.Drawing.Point(247, 115);
            this.lblMessage3.Name = "lblMessage3";
            this.lblMessage3.Size = new System.Drawing.Size(39, 13);
            this.lblMessage3.TabIndex = 25;
            this.lblMessage3.Text = "Timer3";
            // 
            // lblupdate3
            // 
            this.lblupdate3.AutoSize = true;
            this.lblupdate3.Location = new System.Drawing.Point(179, 115);
            this.lblupdate3.Name = "lblupdate3";
            this.lblupdate3.Size = new System.Drawing.Size(39, 13);
            this.lblupdate3.TabIndex = 24;
            this.lblupdate3.Text = "Timer3";
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Location = new System.Drawing.Point(247, 90);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(39, 13);
            this.lblMessage2.TabIndex = 23;
            this.lblMessage2.Text = "Timer2";
            // 
            // lblupdate2
            // 
            this.lblupdate2.AutoSize = true;
            this.lblupdate2.Location = new System.Drawing.Point(179, 90);
            this.lblupdate2.Name = "lblupdate2";
            this.lblupdate2.Size = new System.Drawing.Size(39, 13);
            this.lblupdate2.TabIndex = 22;
            this.lblupdate2.Text = "Timer2";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(247, 15);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(39, 13);
            this.lblMessage.TabIndex = 21;
            this.lblMessage.Text = "Timer1";
            // 
            // lblupdate
            // 
            this.lblupdate.AutoSize = true;
            this.lblupdate.Location = new System.Drawing.Point(179, 15);
            this.lblupdate.Name = "lblupdate";
            this.lblupdate.Size = new System.Drawing.Size(39, 13);
            this.lblupdate.TabIndex = 20;
            this.lblupdate.Text = "Timer1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTimer9);
            this.groupBox2.Controls.Add(this.txtTimer8);
            this.groupBox2.Controls.Add(this.txtTimer7);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblupdate7);
            this.groupBox2.Controls.Add(this.lblMessage7);
            this.groupBox2.Controls.Add(this.lblupdate9);
            this.groupBox2.Controls.Add(this.lblMessage9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblupdate8);
            this.groupBox2.Controls.Add(this.lblMessage8);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(361, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 93);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Historical Position";
            // 
            // txtTimer9
            // 
            this.txtTimer9.Location = new System.Drawing.Point(94, 62);
            this.txtTimer9.Name = "txtTimer9";
            this.txtTimer9.Size = new System.Drawing.Size(63, 20);
            this.txtTimer9.TabIndex = 9;
            this.txtTimer9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer9.Leave += new System.EventHandler(this.txtTimer9_Leave);
            this.txtTimer9.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer8
            // 
            this.txtTimer8.Location = new System.Drawing.Point(94, 37);
            this.txtTimer8.Name = "txtTimer8";
            this.txtTimer8.Size = new System.Drawing.Size(63, 20);
            this.txtTimer8.TabIndex = 8;
            this.txtTimer8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer8.Leave += new System.EventHandler(this.txtTimer8_Leave);
            this.txtTimer8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer7
            // 
            this.txtTimer7.Location = new System.Drawing.Point(94, 12);
            this.txtTimer7.Name = "txtTimer7";
            this.txtTimer7.Size = new System.Drawing.Size(63, 20);
            this.txtTimer7.TabIndex = 7;
            this.txtTimer7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer7.Leave += new System.EventHandler(this.txtTimer7_Leave);
            this.txtTimer7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer6
            // 
            this.txtTimer6.Location = new System.Drawing.Point(454, 150);
            this.txtTimer6.Name = "txtTimer6";
            this.txtTimer6.Size = new System.Drawing.Size(63, 20);
            this.txtTimer6.TabIndex = 11;
            this.txtTimer6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer6.Leave += new System.EventHandler(this.txtTimer6_Leave);
            this.txtTimer6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // txtTimer11
            // 
            this.txtTimer11.Location = new System.Drawing.Point(455, 124);
            this.txtTimer11.Name = "txtTimer11";
            this.txtTimer11.Size = new System.Drawing.Size(63, 20);
            this.txtTimer11.TabIndex = 10;
            this.txtTimer11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTimer11.Leave += new System.EventHandler(this.txtTimer11_Leave);
            this.txtTimer11.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTimer1_KeyPress);
            // 
            // timer11_1
            // 
            this.timer11_1.Tick += new System.EventHandler(this.timer11_1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(709, 229);
            this.Controls.Add(this.txtTimer11);
            this.Controls.Add(this.txtTimer6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblMessage11);
            this.Controls.Add(this.lblupdate11);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblupdate6);
            this.Controls.Add(this.lblMessage6);
            this.Controls.Add(this.txtThreadCounter);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(300, 0);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Update Livebook";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdStart;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label txtThreadCounter;
        private System.Windows.Forms.Timer timer6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblupdate6;
        private System.Windows.Forms.Label lblMessage6;
        private System.Windows.Forms.Timer timer7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblMessage8;
        private System.Windows.Forms.Label lblupdate8;
        private System.Windows.Forms.Label lblMessage9;
        private System.Windows.Forms.Label lblupdate9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblMessage7;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblupdate7;
        private System.Windows.Forms.Timer timer8;
        private System.Windows.Forms.Timer timer9;
        private System.Windows.Forms.Timer timer10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblupdate10;
        private System.Windows.Forms.Label lblMessage10;
        private System.Windows.Forms.Timer timer11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblMessage11;
        private System.Windows.Forms.Label lblupdate11;
        private System.Windows.Forms.Timer timer12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblupdate5;
        private System.Windows.Forms.Label lblMessage5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage4;
        private System.Windows.Forms.Label lblupdate4;
        private System.Windows.Forms.Label lblMessage3;
        private System.Windows.Forms.Label lblupdate3;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.Label lblupdate2;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblupdate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTimer3;
        private System.Windows.Forms.TextBox txtTimer2;
        private System.Windows.Forms.TextBox txtTimer4;
        private System.Windows.Forms.TextBox txtTimer5;
        private System.Windows.Forms.TextBox txtTimer1;
        private System.Windows.Forms.TextBox txtTimer10;
        private System.Windows.Forms.TextBox txtTimer9;
        private System.Windows.Forms.TextBox txtTimer8;
        private System.Windows.Forms.TextBox txtTimer7;
        private System.Windows.Forms.TextBox txtTimer6;
        private System.Windows.Forms.TextBox txtTimer11;
        private System.Windows.Forms.Timer timer11_1;
    }
}

