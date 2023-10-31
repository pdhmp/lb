using System;
using System.Data;
using LiveDLL;

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


                SQLString = "select top 15 IdPortfolio Portfolio ,CalcDate Day,	case Status 	when 'Not calc' then  2	when 'Finished' then  3	else 1	end as IdStatus,Status,CalcLines,CreateLines,TotalTimeTaken,CONVERT(time ,InsertDate) as InsertTime  from NESTRT.dbo.[Tb0010_StatusCalcPortfolios]  where convert (datetime,InsertDate) >  '" + DateTime.Now.ToString("yyyy-MM-dd") + "' order by IdStatus , InsertDate desc";

                dtSource = curConn.Return_DataTable(SQLString);

                dtgViewCalc.DataSource = dtSource;

                dgViewCalc.BestFitColumns();


            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadPosition();
        }

        private void dgViewCalc_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgViewCalc);
        }

        private void dgViewCalc_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgViewCalc);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            using (newNestConn curCoon = new newNestConn())
            {
                curCoon.ExecuteNonQuery("DELETE FROM [NESTRT].[dbo].[Tb0010_StatusCalcPortfolios];");
            }
        }
    }
}
