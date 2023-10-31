using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;


namespace LBHistCalc
{
    public partial class frmMain : Form
    {
        RTCalcRunner curRTCalcRunner;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            GlobalVars.Instance.LoadAccounts();
            GlobalVars.Instance.LoadAllSymbolData(DateTime.Now.Date.AddDays(-1));
            GlobalVars.Instance.LoadPortCurrency();

            //HCalcPortfolio(new DateTime(2012, 10, 15), 04);
            //HCalcPortfolio(new DateTime(2012, 10, 15), 38);
            //HCalcPortfolio(new DateTime(2012, 10, 15), 18);
            //HCalcPortfolio(new DateTime(2012, 10, 15), 43);
            //HCalcPortfolio(new DateTime(2012, 10, 15), 50);
            //HCalcPortfolio(new DateTime(2012, 10, 15), 10);
            //HCalcPortfolio(new DateTime(2012, 10, 15), 60);
           

            //HCalcPortfolio_FromDate(new DateTime(2012, 09, 26), 10);

            curRTCalcRunner = new RTCalcRunner();
            curRTCalcRunner.Start();
        }

        private void HCalcPortfolio(DateTime PositionDate, int IdPortfolio)
        {
            HistoricCalc curHistoricCalc = new HistoricCalc(PositionDate, IdPortfolio, 0);
            curHistoricCalc.Calculate();
        }

        private void HCalcPortfolio_FromDate(DateTime InitialDate, int IdPortfolio)
        {
            using (newNestConn curConn = new newNestConn())
            {
                DataTable curTable = curConn.Return_DataTable("SELECT Data_PL FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + IdPortfolio + " AND Data_PL>='" + InitialDate.ToString("yyyy-MM-dd") + "' ORDER BY Data_PL");

                foreach (DataRow curRow in curTable.Rows)
                {
                    DateTime curDate = NestDLL.Utils.ParseToDateTime(curRow["Data_PL"]);
                    HistoricCalc curHistoricCalc = new HistoricCalc(curDate, IdPortfolio, 0);
                    curHistoricCalc.Calculate();
                }
            }
        }

        private void LoadCalcPortfolios()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT Id_Pending, Id_Ticker, Ini_Date,Status FROM dbo.Tb207_Pending(NOLOCK) WHERE Source=3 AND Status in(-1,0) ORDER BY Ini_Date, Id_Ticker DESC";

                foreach (DataRow curRow in curConn.Return_DataTable(SQLString).Rows)
                {
                    HCalcPortfolio(NestDLL.Utils.ParseToDateTime(curRow["Ini_Date"]), (int)NestDLL.Utils.ParseToDouble(curRow["Id_Ticker"].ToString().Split(',')[0]));
                    curConn.ExecuteNonQuery("UPDATE Tb207_Pending SET [Status]=5 WHERE Id_Pending=" + NestDLL.Utils.ParseToDouble(curRow["Id_Pending"]));
                }
            }
        }

        private void cmdRecalc_Click(object sender, EventArgs e)
        {
            curRTCalcRunner.ReCalcAll();
        }

        //private void CalcAllDays(int IdPortfolio)
        //{
        //    HCalcPortfolio(new DateTime(2012, 08, 27), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 08, 28), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 08, 29), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 08, 30), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 08, 31), IdPortfolio);

        //    HCalcPortfolio(new DateTime(2012, 09, 3), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 4), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 5), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 6), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 7), IdPortfolio);

        //    HCalcPortfolio(new DateTime(2012, 09, 10), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 11), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 12), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 13), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 14), IdPortfolio);

        //    HCalcPortfolio(new DateTime(2012, 09, 17), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 18), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 19), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 20), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 21), IdPortfolio);

        //    HCalcPortfolio(new DateTime(2012, 09, 24), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 25), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 26), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 27), IdPortfolio);
        //    HCalcPortfolio(new DateTime(2012, 09, 28), IdPortfolio);

        //}

    }
}
