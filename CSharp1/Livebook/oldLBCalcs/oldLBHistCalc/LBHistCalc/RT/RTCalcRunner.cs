using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using System.Data;

namespace LBHistCalc
{
    class RTCalcRunner
    {
        Dictionary<int, RTCalc> CalcPortfolios = new Dictionary<int, RTCalc>();

        DataTable newTransactionTable;
        DataTable newTradesTable;

        public RTCalcRunner()
        { 
        
        }

        public void Start()
        {
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("DELETE FROM NESTRT.dbo.Tb000_Posicao_Atual_NEW");
            }

            CalcPortfolios.Add(4, RTCalcPortfolio(4));
            CalcPortfolios.Add(10, RTCalcPortfolio(10));
            CalcPortfolios.Add(18, RTCalcPortfolio(18));
            CalcPortfolios.Add(38, RTCalcPortfolio(38));
            CalcPortfolios.Add(43, RTCalcPortfolio(43));
            CalcPortfolios.Add(50, RTCalcPortfolio(50));
            CalcPortfolios.Add(60, RTCalcPortfolio(60));

            InsertAllPos();
        }

        public void ReCalcAll()
        {
            DateTime StartTime = DateTime.Now;
            Console.WriteLine("STARTING UPDATE");

            DateTime InitialDate = new DateTime(2200, 01, 01);

            foreach (RTCalc curRTCalc in CalcPortfolios.Values)
            {
                if (curRTCalc.curCalc.CloseDate < InitialDate) InitialDate = curRTCalc.curCalc.CloseDate;
            }

            newTransactionTable = DBUtils.LoadTransactionTable(InitialDate, DateTime.Now.Date, "10,18,43,4,38,50,60");
            newTradesTable = DBUtils.LoadTradesTable(InitialDate, DateTime.Now.Date, "10,18,43,4,38,50,60");

            Console.WriteLine(DateTime.Now.Subtract(StartTime).TotalSeconds + "\tTABLES LOADED");

            RecalcPort(4);
            RecalcPort(10);
            RecalcPort(18);
            RecalcPort(38);
            RecalcPort(43);
            RecalcPort(50);
            RecalcPort(60);

            Console.WriteLine(DateTime.Now.Subtract(StartTime).TotalSeconds + "\tUPDATE FINISHED");
        }

        public void InsertAllPos()
        {
            DateTime StartTime = DateTime.Now;

            foreach(RTCalc curRTCalc in CalcPortfolios.Values)
            {
                foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                {
                    InsertPosition(curPositionItem);
                }
            }

            Console.WriteLine(DateTime.Now.Subtract(StartTime).TotalSeconds + "\tAll positions Insert");
        }

        public void RecalcPort(int IdPortfolio)
        {
            RTCalc curRTCalc = CalcPortfolios[IdPortfolio];
            LBCalculator newRTCalc = curRTCalc.getRecalc(newTransactionTable, newTradesTable);

            foreach (PositionItem curPositionItem in newRTCalc.AllPositions)
            {

                PositionItem testItem = curRTCalc.curCalc.AllPositions.Find(
                            delegate(PositionItem testPositionItem)
                            {
                                if (testPositionItem.IdPortfolio == curPositionItem.IdPortfolio && testPositionItem.IdBook == curPositionItem.IdBook && testPositionItem.IdSection == curPositionItem.IdSection && testPositionItem.IdSecurity == curPositionItem.IdSecurity)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            );

                if (testItem != null)
                {
                    string tempString = testItem.GetUpdateString(curPositionItem);
                    if (tempString != "")
                    {
                        curPositionItem.DBId = testItem.DBId;
                        curRTCalc.curCalc.AllPositions.Remove(testItem);
                        curRTCalc.curCalc.AllPositions.Add(curPositionItem);
                        UpdatePosition(tempString, curPositionItem.DBId);
                        Console.WriteLine("Changed\t" + testItem.ToString());
                    }
                }
                else
                {
                    curRTCalc.curCalc.AllPositions.Add(curPositionItem);
                    InsertPosition(curPositionItem);
                }
            }
        
        }

        private RTCalc RTCalcPortfolio(int IdPortfolio)
        {
            RTCalc curRTCalc = new RTCalc(IdPortfolio);
            curRTCalc.Calculate();
            return curRTCalc;
        }

        private void InsertPosition(PositionItem curPositionItem)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "INSERT INTO NESTRT.dbo.Tb000_Posicao_Atual_NEW([Id Portfolio], [Id Book], [Id Section], [Id Ticker], Position, [Last Position], [Date Now], "
                    + "Close_Date, NAV, [NAV pC], [Id Administrator], [Prev Cash uC], [Prev Cash uC Admin], [Prev Asset P/L], [Prev Asset P/L Admin], [Adjusted Fw Position], "
                    + "[Close], [Close Admin], [Cost Close], [Realized], [Cost Close Admin], [Realized Admin], [Initial Position], [Quantity Bought], [Quantity Sold], [Last Calc], "
                    + "[Last Cost Close Calc], [Id Source Close], [Flag Close Admin], [Id Source Close Admin], [Cash FLow], [Trade Flow], [Dividends])\r\n ";

                StringSQL += "SELECT "
                    + curPositionItem.IdPortfolio + ", "
                    + curPositionItem.IdBook + ", "
                    + curPositionItem.IdSection + ", "
                    + curPositionItem.IdSecurity + ", "
                    + curPositionItem.CurrentPosition.ToString().Replace(",", ".") + ", "
                    + "'" + curPositionItem.DateClose.ToString("yyyy-MM-dd") + "', "
                    + "'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "', "
                    + "'" + curPositionItem.DateClose.ToString("yyyy-MM-dd") + "', "
                    + curPositionItem.NAV.ToString().Replace(",", ".") + ", "
                    + curPositionItem.NAVpC.ToString().Replace(",", ".") + ", "
                    + curPositionItem.IdAdministrator + ", "
                    + curPositionItem.PrevCashuC.ToString().Replace(",", ".") + ", "
                    + curPositionItem.PrevCashuCAdmin.ToString().Replace(",", ".") + ", "
                    + curPositionItem.PrevAssetPL.ToString().Replace(",", ".") + ", "
                    + curPositionItem.PrevAssetPLAdmin.ToString().Replace(",", ".") + ", "
                    + curPositionItem.AdjFwPosition.ToString().Replace(",", ".") + ", "
                    + curPositionItem.Close.ToString().Replace(",", ".") + ", "
                    + curPositionItem.CloseAdmin.ToString().Replace(",", ".") + ", "
                    + curPositionItem.CostClose.ToString().Replace(",", ".") + ", "
                    + curPositionItem.Realized.ToString().Replace(",", ".") + ", "
                    + curPositionItem.CostCloseAdmin.ToString().Replace(",", ".") + ", "
                    + curPositionItem.RealizedAdmin.ToString().Replace(",", ".") + ", "
                    + curPositionItem.InitialPosition.ToString().Replace(",", ".") + ", "
                    + (curPositionItem.QuantBought * curPositionItem.QuantBoughtFactor).ToString().Replace(",", ".") + ", "
                    + (curPositionItem.QuantSold * curPositionItem.QuantSoldFactor).ToString().Replace(",", ".") + ", "
                    + " '1900-01-01', "
                    + " getdate(), "
                    + curPositionItem.IdSourceClose + ", "
                    + "'" + curPositionItem.FlagCloseAdmin + "', "
                    + curPositionItem.IdSourceCloseAdmin + ", "
                    + curPositionItem.CashFlow.ToString().Replace(",", ".") + ", "
                    + curPositionItem.TradeFlow.ToString().Replace(",", ".") + ", "
                    + curPositionItem.Dividends.ToString().Replace(",", ".")
                    + "\r\n";
                
                curConn.ExecuteNonQuery(StringSQL);

                curPositionItem.DBId = curConn.Return_Int("SELECT @@IDENTITY");

            }
        }
        
        private void UpdatePosition(string UpdateString, long DBId)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "UPDATE NESTRT.dbo.Tb000_Posicao_Atual_NEW SET " + UpdateString.Substring(2) + " WHERE [Id Position]=" + DBId;
                curConn.ExecuteNonQuery(StringSQL);
            }
        }
    }
}
