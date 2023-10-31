using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using LiveBook.Business;

using System.Data.SqlClient;

using System.Runtime.InteropServices;
using System.Threading;

namespace LiveBook
{
    public partial class frmStatusFlags : LBForm
    {
        newNestConn curConn = new newNestConn();
        string SQLString2;
        string ErrorString = "";
        object padLock = new object();

        public frmStatusFlags()
        {
            InitializeComponent();
        }

        private void frmStatusFlags_Load(object sender, EventArgs e)
        {
            SQLString2 = "SELECT Id_Program, Login + ' - ' + Err_Message AS Err_Message FROM NESTLOG.dbo.Tb902_Error_Report A (nolock) LEFT JOIN NESTDB.dbo.Tb014_Pessoas B (nolock) ON A.Responsability=B.Id_Pessoa Order by Severity";
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            ExecSQL();

            lock (padLock)
            {
                txtStatus.Text = ErrorString;

                foreach (Control curControl in this.Controls)
                {
                    if (curControl.Name.Contains("xlbl") && !curControl.Name.Contains("Flag"))
                    {
                        int tempDelay = GetDelay(curControl.Name.Replace("xlbl", ""));
                        if (tempDelay > 60)
                        {
                            ((Label)this.Controls[curControl.Name + "_Flag"]).Text = "OFF";
                            ((Label)this.Controls[curControl.Name + "_Flag"]).ForeColor = Color.Red;
                        }
                        else if (tempDelay > 8)
                        {
                            ((Label)this.Controls[curControl.Name + "_Flag"]).Text = "NOT RESP";
                            ((Label)this.Controls[curControl.Name + "_Flag"]).ForeColor = Color.Orange;
                        }
                        else
                        {
                            ((Label)this.Controls[curControl.Name + "_Flag"]).Text = "OK";
                            ((Label)this.Controls[curControl.Name + "_Flag"]).ForeColor = Color.Lime;
                            timer1.Interval = 20000;
                        }
                    }
                }
            }
        }

        private void ExecSQL()
        {
            try
            {
                DataTable ThreadTable = new DataTable();

                ThreadTable = curConn.Return_DataTable(SQLString2);

                lock (padLock)
                {
                    ErrorString = "";

                    foreach (DataRow curRow in ThreadTable.Rows)
                    {
                        ErrorString = ErrorString + curRow[1].ToString() + "\r\n";
                    }
                }

            }
            catch(Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());
            }
        }

        private int GetDelay(string ProgramName)
        {
            try
            {

                DateTime tempDate = Convert.ToDateTime("01/01/1900");
                DateTime tempNow = Convert.ToDateTime("01/01/1900");
                string SQLString1 = "SELECT CheckInTime " +
                                    " FROM NESTLOG.dbo.Tb900_CheckIn_Log A (nolock) " +
                                    " INNER JOIN NESTDB.dbo.Tb902_Programs B (nolock) " +
                                    " ON A.Program_Id=B.Program_Id" +
                                    " WHERE Program_Name='" + ProgramName + "'";

                string tempTime = curConn.Execute_Query_String(SQLString1);
                if (tempTime != "" && tempTime != null)
                {
                    tempDate = Convert.ToDateTime(tempTime);
                }


                string tempDBTime = curConn.Execute_Query_String("SELECT GetDate() ");
                if (tempDBTime != "" && tempDBTime != null)
                {
                    tempNow = Convert.ToDateTime(tempDBTime);
                }

                if (tempNow != null && tempDate != null)
                {
                    TimeSpan TotalDelay = tempNow - tempDate;
                    return TotalDelay.Seconds + TotalDelay.Minutes * 60 + TotalDelay.Hours * 3600;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception excep)
            {
                curUtils.Log_Error_Dump_TXT(excep.ToString(), this.Name.ToString());
                return 0;
            }

        }
    }
}