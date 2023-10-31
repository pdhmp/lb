using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;
using NestDLL;
using System.Data;

namespace CyrnelSync
{
    public class CyrnelGenerator
    {
        public DateTime CurDate = new DateTime(1900, 01, 01);
        public DateTime MaxDate = new DateTime(1900, 01, 01);
        public string SQLString = "";

        public EventHandler OnFinish;

        public void CreateFile()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            GetLastDate();

            double TotalAdjust = 0;

            using (newNestConn curConn = new newNestConn())
            {
                SQLString = " SELECT '' AS Date, FundName AS 'Fund Name',Ticker AS 'Security Ticker', ROUND (Position , 2) AS Amount,  ROUND (Price,2) AS Price, '' AS Book , OptionAdjustment, IdPortfolio " +
                            " FROM NESTDB.dbo.FCN_Cyrnel_Export('" + DateTime.Now.ToString("yyyy-MM-dd") + "') ";

                dt = curConn.Return_DataTable(SQLString);
            }

            foreach (DataRow curRow in dt.Rows)
            {
                curRow["Date"] = MaxDate.ToString("yyyyMMdd");
                curRow["Fund Name"] = ReplacePortfolioName(curRow["Fund Name"].ToString());
                curRow["Security Ticker"] = curRow["Security Ticker"].ToString().Replace("-13", "").Replace("-12", "");
                curRow["Amount"] = Math.Round(double.Parse(curRow["Amount"].ToString()), 2).ToString();
                curRow["Book"] = "";


                if (curRow["Security Ticker"].ToString().Length > 2)
                {
                    if (curRow["Security Ticker"].ToString().Substring(0, 3) == "WIN") { curRow["Price"] = (double)curRow["Price"] / 5; }
                    if (curRow["Security Ticker"].ToString().Substring(0, 3) == "BGI") { curRow["Price"] = 0; }
                }
                if (curRow["IdPortfolio"].ToString() == "4") { TotalAdjust += double.Parse(curRow["OptionAdjustment"].ToString()); }
            }

            DataRow newRow = dt.NewRow();
            newRow["Date"] = MaxDate.ToString("yyyyMMdd");
            newRow["Fund Name"] = "Nest Equity Hedge";
            newRow["Security Ticker"] = "USD";
            newRow["Amount"] = TotalAdjust;
            newRow["Price"] = "1";
            dt.Rows.Add(newRow);

            dt.Columns.Remove("OptionAdjustment");
            dt.Columns.Remove("IdPortfolio");

            SaveWorkBook(dt);

        }

        private void SaveWorkBook(System.Data.DataTable curDataTable)
        {

            Excel.Application ExcelApplication;
            Excel.Workbook CyrnelWorkBook;
            Excel.Worksheet CyrnelWorkSheet;
            Excel.Range Range;
            object missing = Type.Missing;

            ExcelApplication = new Excel.Application();
            ExcelApplication.DisplayAlerts = false;

            CyrnelWorkBook = ExcelApplication.Workbooks.Add();
            try
            {
                CyrnelWorkSheet = (Excel.Worksheet)CyrnelWorkBook.ActiveSheet;
                CyrnelWorkSheet.Name = "PORTFOLIO";

                int rowCount = 1;


                CyrnelWorkSheet.Cells[1, 1] = "Date";
                CyrnelWorkSheet.Cells[1, 2] = "Fund Name";
                CyrnelWorkSheet.Cells[1, 3] = "Security Ticker";
                CyrnelWorkSheet.Cells[1, 4] = "Amount";
                CyrnelWorkSheet.Cells[1, 5] = "Price";
                CyrnelWorkSheet.Cells[1, 6] = "Book";

                foreach (DataRow curRow in curDataTable.Rows)
                {
                    rowCount++;
                   
                    CyrnelWorkSheet.Cells[rowCount, 1] = curRow["Date"].ToString();
                    CyrnelWorkSheet.Cells[rowCount, 2] = curRow["Fund Name"].ToString();
                    CyrnelWorkSheet.Cells[rowCount, 3] = curRow["Security Ticker"].ToString();
                    CyrnelWorkSheet.Cells[rowCount, 4] = NestDLL.Utils.ParseToDouble(curRow["Amount"]);
                    CyrnelWorkSheet.Cells[rowCount, 5] = NestDLL.Utils.ParseToDouble(curRow["Price"]);
                    CyrnelWorkSheet.Cells[rowCount, 6] = curRow["Book"].ToString();
                }

                CyrnelWorkSheet.Columns.AutoFit();

                Range = CyrnelWorkSheet.get_Range("a1", "f1");
                Range.Font.Bold = true;

                CyrnelWorkSheet = null;
                Range = null;
                CyrnelWorkBook.SaveAs(
                    "N:\\Risk\\Cyrnel\\Upload\\CYRNEL_UPLOAD.xls",
                    Excel.XlFileFormat.xlExcel8, missing, missing, missing, missing,
                    Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);

                if (OnFinish != null)
                {
                    OnFinish(this, new EventArgs());
                }
            }
            catch { }
            finally
            {
                CyrnelWorkBook.Close();
                CyrnelWorkBook = null;
                ExcelApplication.Quit();
            }
            // Clean up 
            // NOTE: When in release mode, this does the trick 
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

        }

        private string ReplacePortfolioName(string PortfolioName)
        {
            switch (PortfolioName)
            {
                case "Nest Arb Master FIM": return "Nest Arb";
                case "Nest Hedge Master": return "Nest Hedge";
                case "Nest Ações Master FIA": return "Nest Acoes";
                case "Nest Previdencia Master": return "Nest SulAmerica Prev";
                case "Nest Equity Hedge Master": return "Nest Equity Hedge";
                case "Nest Icatu Prev FIM": return "Nest Icatu Prev";
                case "Nest MH Master FIM": return "Nest MH";
                default: return PortfolioName;
            }
        }

        private void GetLastDate()
        {
            string SQLString =
                "SELECT MAX([Date Now]) AS MaxDate FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]<='" + CurDate.ToString("yyyy-MM-dd") + "'" +
                "AND [Date Now] not in (SELECT [Data] FROM [NESTDB].[dbo].[Tb118_Feriados] WHERE id_geografia = 0)";

            using (newNestConn curConn = new newNestConn())
            {
                MaxDate = curConn.Return_DateTime(SQLString);
            }
        }
    }
}
