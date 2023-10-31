using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;

namespace LiveBook
{
    public partial class frmViewCalcHist : LBForm
    {
        public frmViewCalcHist()
        {
            InitializeComponent();
        }

        DateTime OpenDate = DateTime.Now;
        



        private void Form1_Load(object sender, EventArgs e)
        {
            OpenDate = OpenDate.AddMinutes(-20);

            LoadPosition();
            timer1.Start();
        }


        void LoadPosition()
        {
            DataTable dtSource;
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString;

                SQLString = "Select * FROM NESTRT.dbo.Tb0010_StatusCalcPortfolios (nolock) WHERE InsertDate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND (StartTime >='" + OpenDate.ToString("HH:mm:ss") + "' or Status = 'Not calc') order by StartTime,CalcDate";
                dtSource = curConn.Return_DataTable(SQLString);

                dtgViewCalc.DataSource = dtSource;
            }
        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadPosition();
            GetDateLoaded();
        }

        private void cmdReloadtrans_Click(object sender, EventArgs e)
        {
            using (newNestConn curConn = new newNestConn())
            {

                curConn.ExecuteNonQuery("INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(1,convert(varchar,getdate(),112),2,0,0)");
                lblTime.Text = "Running";
            }
        }

        void GetDateLoaded()
        {
            string SQLString;
            DateTime ReturnDate;

            SQLString = "SELECT CheckInTime FROM NESTLOG.dbo.Tb900_CheckIn_Log (Nolock)" +
                    " WHERE Program_Id=202 ";

            using (newNestConn curConn = new newNestConn())
            {
                ReturnDate = Convert.ToDateTime(curConn.Execute_Query_String(SQLString).ToString());


                if (lblTime.Text != ReturnDate.ToString())
                {
                    if (ReturnDate == Convert.ToDateTime("02/01/1900"))
                    {
                        lblTime.Text = "Running";
                    }
                    else
                    {
                        lblTime.Text = ReturnDate.ToString();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(1,convert(varchar,getdate(),112),6,0,0)");
            }
        }

        private void dgViewCalc_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgViewCalc);
        }

        private void dgViewCalc_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgViewCalc);
        }

    }
}
