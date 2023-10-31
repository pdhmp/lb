using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using LiveDLL;
using System.Threading;

namespace Update_Livebook
{

    public partial class Form1 : Form
    {
        bool CheckCloseUpdate = false;

        public newNestConn NestFunc = new newNestConn();

        bool Checa_rotina = false;
        bool Checa_rotina2 = false;
        bool Checa_rotina3 = false;
        bool Checa_rotina4 = false;
        bool Checa_rotina5 = false;
        bool Checa_rotina6 = false;
        bool Checa_rotina7 = false;
        bool Checa_rotina8 = false;
        bool Checa_rotina9 = false;
        bool Checa_rotina10 = false;
        bool Checa_rotina11 = false;

        int[] Interval_rotina = new int[12];
        int[] Interval_subrotina = new int[12];

        string Status1;
        string LastTime1;
        string Status2;
        string LastTime2;
        string Status3;
        string LastTime3;
        string Status4;
        string LastTime4;
        string Status5;
        string LastTime5;
        string Status6;
        string LastTime6;

        string Status7;
        string LastTime7;

        string Status8;
        string LastTime8;

        string Status9;
        string LastTime9;

        string Status10;
        string LastTime10;

        string Status11;
        string LastTime11;

        int ThreadCounter = 0;

        System.Threading.Thread t1;
        System.Threading.Thread t2;
        System.Threading.Thread t3;
        System.Threading.Thread t4;
        System.Threading.Thread t5;
        System.Threading.Thread t6;
        System.Threading.Thread t7;
        System.Threading.Thread t8;
        System.Threading.Thread t9;
        System.Threading.Thread t10;
        System.Threading.Thread t11;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Top = 80;
            this.Left = 300;

            Interval_rotina[1] = 500;
            Interval_rotina[2] = 500;
            Interval_rotina[3] = 120000;
            Interval_rotina[4] = 1000;
            Interval_rotina[5] = 1000;
            Interval_rotina[6] = 1000;
            Interval_rotina[7] = 1000;
            Interval_rotina[8] = 10000;
            Interval_rotina[9] = 10000;
            Interval_rotina[10] = 2000;
            Interval_rotina[11] = 600000;
            Interval_subrotina[11] = 100;

            StartTimers();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTimers();
            Application.ExitThread();
        }

        private void Log_Checkin()
        {
            String StringSQL = "UPDATE [NESTLOG].dbo.Tb900_CheckIn_Log SET CheckInTime=getdate() WHERE Program_Id=7";
            int retorno = NestFunc.ExecuteNonQuery(StringSQL);
        }



        private void StartTimers()
        {
            timer1.Interval  = Interval_rotina[1];
            timer2.Interval  = Interval_rotina[2];
            timer3.Interval  = Interval_rotina[3];
            timer4.Interval  = Interval_rotina[4];
            timer5.Interval  = Interval_rotina[5];
            timer6.Interval  = Interval_rotina[6];
            //timer7.Interval  = Interval_rotina[7];
           // timer8.Interval  = Interval_rotina[8];
            //timer9.Interval  = Interval_rotina[9];
            timer10.Interval  = Interval_rotina[10];
            timer11.Interval = Interval_rotina[11];
            timer11_1.Interval = Interval_subrotina[11];

            txtTimer1.Text = Interval_rotina[1].ToString();
            txtTimer2.Text = Interval_rotina[2].ToString();
            txtTimer3.Text = Interval_rotina[3].ToString();
            txtTimer4.Text = Interval_rotina[4].ToString();
            txtTimer5.Text = Interval_rotina[5].ToString();
            txtTimer6.Text = Interval_rotina[6].ToString();
            //txtTimer7.Text = Interval_rotina[7].ToString();
            //txtTimer8.Text = Interval_rotina[8].ToString();
            //txtTimer9.Text = Interval_rotina[9].ToString();
            txtTimer10.Text = Interval_rotina[10].ToString();
            txtTimer11.Text = Interval_rotina[11].ToString();

            timer1_Tick(this, new EventArgs());
            timer2_Tick(this, new EventArgs());
            timer3_Tick(this, new EventArgs());
            timer4_Tick(this, new EventArgs());
            timer5_Tick(this, new EventArgs());
            timer6_Tick(this, new EventArgs());
           // timer7_Tick(this, new EventArgs());
           // timer8_Tick(this, new EventArgs());
           // timer9_Tick(this, new EventArgs());
            timer10_Tick(this, new EventArgs());
            timer11_Tick(this, new EventArgs());
            timer11_1_Tick(this, new EventArgs());


            timer1.Start(); // sp_Calc_LB2_Recent_Price
            timer2.Start(); // sp_Calc_LB2_Oldest_Updated
            timer3.Start(); // sp_Calc_LB2_New_Positions
            timer4.Start(); // sp_Calc_LB2_Oldest_Cost_Close
            timer5.Start(); // proc_Calc_LB2_Cost_Close
            timer6.Start(); // Proc_SELECT_FIX_Drop_Copies
           // timer7.Start(); // proc_Calc_Check_Fixed_Fields
           // timer8.Start(); // proc_Calc_LB2_Hist_Cost_Close
            //timer9.Start(); // proc_Calc_LB2_Hist_Calc_Fields
            timer10.Start(); // proc_Calc_LB2_New_Quantities
            timer11.Start(); // 
            timer12.Start(); // Monitor
        }

        private void StopTimers()
        {
            
            timer1.Stop(); // sp_Calc_LB2_Recent_Price
            timer2.Stop(); // sp_Calc_LB2_Oldest_Updated
            timer3.Stop(); // sp_Calc_LB2_New_Positions
            timer4.Stop(); // sp_Calc_LB2_Oldest_Cost_Close
            timer5.Stop(); // proc_Calc_LB2_Cost_Close
            timer6.Stop(); // Proc_SELECT_FIX_Drop_Copies
            //timer7.Stop(); // proc_Calc_Check_Fixed_Fields
            //timer8.Stop(); // proc_Calc_LB2_Hist_Cost_Close
           // timer9.Stop(); // proc_Calc_LB2_Hist_Calc_Fields
            timer10.Stop(); // proc_Calc_LB2_New_Quantities
            timer11.Stop(); // 
            timer12.Stop(); // Monitor

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartTimers();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            StopTimers();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina == false)
                {
                    t1 = new System.Threading.Thread(new ThreadStart(ExecSQL1));
                    t1.Start();
                }
            }
            catch(Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer1.txt");
                Application.Exit();
            }
        }


        private void ExecSQL1()
        {
            ThreadCounter++;
            Checa_rotina = true;
            string StringSQL;
            StringSQL = "EXEC [sp_Calc_LB2_Recent_Price]";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status1 = "Error on Calculate!";
                   // StringSQL = "INSERT INTO [NESTDB].[dbo].[Tb901_Event_Log]([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description]) VALUES (7, 112, 9, getdate(),'Error Calculating line - most recent (client)')";
                   // int retorno2 = Convert.ToInt32(NestFunc.Execute_Query_String(StringSQL);
                }
                else
                    Status1 = "Service is OK!";
                Checa_rotina = false;
                LastTime1 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine1.txt");
                Application.Exit();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina2 == false)
                {
                    t2 = new System.Threading.Thread(new ThreadStart(ExecSQL2));
                    t2.Start();
                }

            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer2.txt");
                Application.Exit();
            }
        }


        private void ExecSQL2()
        {
            ThreadCounter++;
            Checa_rotina2 = true;
            string StringSQL;
            StringSQL = "EXEC [sp_Calc_LB2_Oldest_Updated]";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status2 = "Error on Calculate!";
                    StringSQL = "INSERT INTO [NESTLOG].[dbo].[Tb901_Event_Log]([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description]) VALUES (7, 112, 9, getdate(),'Error Calculating line - oldest (client)')";
                    int retorno2 = NestFunc.ExecuteNonQuery(StringSQL);
                }
                else
                    Status2 = "Service is OK!";
                Checa_rotina2 = false;
                LastTime2 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine2.txt");
                Application.Exit();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina3 == false)
                {
                    t3 = new System.Threading.Thread(new ThreadStart(ExecSQL3));
                    t3.Start();
                }

            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer3.txt");
                Application.Exit();
            }
        }


        private void ExecSQL3()
        {
            ThreadCounter++;
            Checa_rotina3 = true;
            string StringSQL;
            StringSQL = "EXEC [sp_Calc_LB2_New_Positions]";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status3 = "Error on Calculate!";
                    StringSQL = "INSERT INTO [NESLOG].[dbo].[Tb901_Event_Log]([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description]) VALUES (7, 111, 9, getdate(),'Error Calculating Last Positions (client)')";
                    int retorno2 = NestFunc.ExecuteNonQuery(StringSQL);
                }
                else
                    Status3 = "Service is OK!";
                Checa_rotina3 = false;
                LastTime3 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;

            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine3.txt");
                Application.Exit();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina4 == false)
                {
                    t4 = new System.Threading.Thread(new ThreadStart(ExecSQL4));
                    t4.Start();
                }

            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer4.txt");
                Application.Exit();
            }

        }


        private void ExecSQL4()
        {
            ThreadCounter++;
            Checa_rotina4 = true;
            string StringSQL;
            StringSQL = "EXEC [sp_Calc_LB2_Recent_Price_Options]";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status4 = "Error on Calculate!";
                    //StringSQL = "INSERT INTO [NESTDB].[dbo].[Tb901_Event_Log]([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description]) VALUES (7, 111, 9, getdate(),'Error Calculating Recent Price Options (client)')";
                    int retorno2 = NestFunc.ExecuteNonQuery(StringSQL);
                }
                else
                    Status4 = "Service is OK!";
                Checa_rotina4 = false;
                LastTime4 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine4.txt");
                Application.Exit();
            }

        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina5 == false)
                {
                    t5 = new System.Threading.Thread(new ThreadStart(ExecSQL5));
                    t5.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer5.txt");
                Application.Exit();
            }

                
        }

        private void ExecSQL5()
        {
            ThreadCounter++;
            Checa_rotina5 = true;
            string StringSQL;
            StringSQL = "EXEC [proc_Calc_LB2_Cost_Close]";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status5 = "Error on Calculate!";
                }
                else
                    Status5 = "Service is OK!";
                Checa_rotina5 = false;
                LastTime5 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine5.txt");
                Application.Exit();
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina6 == false)
                {
                    t6 = new System.Threading.Thread(new ThreadStart(ExecSQL6));
                    t6.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer6.txt");
                Application.Exit();
            }

        }

        private void ExecSQL6()
        {
            try
            {
                ThreadCounter++;
                Checa_rotina6 = true;
                string StringSQL;
                StringSQL = "EXEC [Proc_FIX_Select_Drop_Copies]";
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status6 = "Error on Insert Fix!";
                }
                else
                    Status6 = "Service is OK!";
                Checa_rotina6 = false;
                LastTime6 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine6.txt");
                Application.Exit();
            }

        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina7 == false)
                {
                    t7 = new System.Threading.Thread(new ThreadStart(ExecSQL7));
                    t7.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer7.txt");
                Application.Exit();
            }

        }

        private void ExecSQL7()
        {
            try
            {
                ThreadCounter++;
                Checa_rotina7 = true;
                string StringSQL;
                StringSQL = "EXEC proc_Calc_Check_Fixed_Fields";
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status7 = "Error on Check Fixed Fields!";
                }
                else
                    Status7 = "Service is OK!";
                Checa_rotina7 = false;
                LastTime7 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine7.txt");
                Application.Exit();
            }

        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina8 == false)
                {
                    t8 = new System.Threading.Thread(new ThreadStart(ExecSQL8));
                    t8.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer8.txt");
                Application.Exit();
            }

        }

        private void ExecSQL8()
        {
            ThreadCounter++;
            Checa_rotina8 = true;
            string StringSQL;
            StringSQL = "EXEC proc_Calc_LB2_Cost_Close_Historical";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status8 = "Error on Calc Historical Cost Close!";
                }
                else
                    Status8 = "Service is OK!";
                Checa_rotina8 = false;
                LastTime8 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine8.txt");
                Application.Exit();
            }

        }

        private void timer9_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina9 == false)
                {
                    t9 = new System.Threading.Thread(new ThreadStart(ExecSQL9));
                    t9.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer9.txt");
                Application.Exit();
            }
        }
 
        private void ExecSQL9()
        {
            ThreadCounter++;
            Checa_rotina9 = true;
            string StringSQL;
            StringSQL = "EXEC proc_Calc_LB2_Hist_Calc_Fields";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status9 = "Error on Calc Historical Fields!";
                }
                else
                    Status9 = "Service is OK!";
                Checa_rotina9 = false;
                LastTime9 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine9.txt");
                Application.Exit();
            }

        }

        private void timer10_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina10 == false)
                {
                    t10 = new System.Threading.Thread(new ThreadStart(ExecSQL10));
                    t10.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer10.txt");
                Application.Exit();
            }


        }
        private void ExecSQL10()
        {
            ThreadCounter++;
            Checa_rotina10 = true;
            string StringSQL;
            StringSQL = "EXEC proc_Calc_LB2_New_Quantities";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status10 = "Error on Calc New Quantities!";
                }
                else
                    Status10 = "Service is OK!";
                Checa_rotina10 = false;
                LastTime10 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine10.txt");
                Application.Exit();
            }


        }

        private void timer11_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Checa_rotina11 == false)
                {
                    t11 = new System.Threading.Thread(new ThreadStart(ExecSQL11));
                    t11.Start();
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer11.txt");
                Application.Exit();
            }


        }

        private void ExecSQL11()
        {
            ThreadCounter++;
            Checa_rotina11 = true;
            string StringSQL;
            StringSQL = "EXEC Proc_Update_TRIndex_RT";
            try
            {
                string retorno = NestFunc.Execute_Query_String(StringSQL);
                if (retorno != "OK")
                {
                    Status11 = "Error on Total Return!";
                }
                else
                {
                    Status11 = "Service is OK!";
                }
                Checa_rotina11 = false;
                LastTime11 = DateTime.Now.ToLongTimeString();
                ThreadCounter--;
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine11.txt");
                Application.Exit();
            }
        }

        private void ExecSQL12()
        {
            try
            {
                txtThreadCounter.Text = ThreadCounter.ToString();
                lblMessage.Text = Status1;
                lblupdate.Text = LastTime1;
                lblMessage2.Text = Status2;
                lblupdate2.Text = LastTime2;
                lblMessage3.Text = Status3;
                lblupdate3.Text = LastTime3;
                lblMessage4.Text = Status4;
                lblupdate4.Text = LastTime4;
                lblMessage5.Text = Status5;
                lblupdate5.Text = LastTime5;
                lblMessage6.Text = Status6;
                lblupdate6.Text = LastTime6;
                lblMessage7.Text = Status7;
                lblupdate7.Text = LastTime7;
                lblMessage8.Text = Status8;
                lblupdate8.Text = LastTime8;
                lblMessage9.Text = Status9;
                lblupdate9.Text = LastTime9;
                lblMessage10.Text = Status10;
                lblupdate10.Text = LastTime10;
                lblMessage11.Text = Status11;
                lblupdate11.Text = LastTime11;

                Log_Checkin();
                Application.DoEvents();
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Routine12.txt");
                Application.Exit();
            }

        }

        private void timer12_Tick(object sender, EventArgs e)
        {
            try
            {
                ExecSQL12();
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer12.txt");
                Application.Exit();
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void timer11_1_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeSpan CheckTime = DateTime.Now.TimeOfDay;

                if (!CheckCloseUpdate && DateTime.Now < Convert.ToDateTime("16:46:00"))
                {
                    timer11_Tick(this, new EventArgs());
                    CheckCloseUpdate = true;
                }
            }
            catch (Exception Excep)
            {
                LiveDLL.Log.AddLogEntry(Excep.ToString(), "C:\\Log\\Update LiveBook\\Timer11_1.txt");
                Application.Exit();
            }

        }

        private void txtTimer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                lblMessage.Select();
            };
        }

        private void SetTimerInterval(TextBox sender,System.Windows.Forms.Timer reference)
        {
            int curInterval = 0;
            try
            {
                int curPosition = Convert.ToInt16(sender.Name.Substring(8, sender.Name.Length-8));
                if (sender.Text !=   Interval_rotina[curPosition].ToString())
                {
                    curInterval = Convert.ToInt32(sender.Text);
                    reference.Interval = curInterval;
                    sender.ForeColor = Color.Red;
                }
                else
                {
                    sender.ForeColor = Color.Black;
                }
            }
            catch
            {
                sender.Text = reference.Interval.ToString();
            }
        }

        private void txtTimer1_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer1, this.timer1);
        }

        private void txtTimer2_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer2, this.timer2);
        }

        private void txtTimer3_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer3, this.timer3);

        }

        private void txtTimer4_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer4, this.timer4);

        }

        private void txtTimer5_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer5, this.timer5);

        }

        private void txtTimer6_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer6, this.timer6);

        }

        private void txtTimer7_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer7, this.timer7);
        }

        private void txtTimer8_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer8, this.timer8);

        }

        private void txtTimer9_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer9, this.timer9);

        }

        private void txtTimer10_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer10, this.timer10);

        }

        private void txtTimer11_Leave(object sender, EventArgs e)
        {
            SetTimerInterval(this.txtTimer11, this.timer11);

        }



   
    }
}