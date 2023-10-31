using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;

namespace LBHistCalc
{
    class HistoricCalc : IEquatable<HistoricCalc>
    {
        private int _LinesToCalc; public int LinesToCalc { get { return _LinesToCalc; } }
        private int _LinesCalculated; public int LinesCalculated { get { return _LinesCalculated; } }

        private DateTime _PositionDate; public DateTime PositionDate { get { return _PositionDate; } }

        private int _IdPortfolio; public int IdPortfolio { get { return _IdPortfolio; } }
        private int _IdPortfolioBRL; public int IdPortfolioBRL { get { return _IdPortfolioBRL; } }
        private int _IdPortfolioOther; public int IdPortfolioOther { get { return _IdPortfolioOther; } }
        private string _PortFilter; public string PortFilter { get { return _PortFilter; } }
        private string _Status; public string Status { get { return _Status; } }

        private int _IdPending; public int IdPending { get { return _IdPending; } }

        private TimeSpan _StartTime = new TimeSpan(0, 0, 0); public string StartTime { get { if (_StartTime == new TimeSpan(0, 0, 0)) return "-"; else return new DateTime(_StartTime.Ticks).ToString("HH:mm:ss"); } set { _StartTime = TimeSpan.Parse(value); } }
        private TimeSpan _TotalTimeTaken = new TimeSpan(0, 0, 0); public string TotalTimeTaken { get { if (_TotalTimeTaken == new TimeSpan(0, 0, 0)) return "-"; else return new DateTime(_TotalTimeTaken.Ticks).ToString("mm:ss,ffff"); } set { _TotalTimeTaken = TimeSpan.Parse(value); } }
        private TimeSpan _CreateLines = new TimeSpan(0, 0, 0); public string CreateLines { get { if (_CreateLines == new TimeSpan(0, 0, 0)) return "-"; else return new DateTime(_CreateLines.Ticks).ToString("mm:ss,ffff"); } set { _CreateLines = TimeSpan.Parse(value); } }

        private TimeSpan _CalcLines = new TimeSpan(0, 0, 0);
        public string CalcLines { get { if (_CreateLines == new TimeSpan(0, 0, 0)) return "-"; else if (_CalcLines == new TimeSpan(0, 0, 0)) return _LinesCalculated + "/" + _LinesToCalc; else return new DateTime(_CalcLines.Ticks).ToString("mm:ss,ffff"); } set { _CalcLines = TimeSpan.Parse(value); } }

        //private int _LinesToCalcBRL; public int LinesToCalcBRL { get { return _LinesToCalcBRL; } }
        //private int _LinesToCalcOther; public int LinesToCalcOther { get { return _LinesToCalcOther; } }

        public HistoricCalc(DateTime PositionDate, int IdPortfolio, int IdPending)
        {
            _PositionDate = PositionDate;
            _IdPortfolio = IdPortfolio;
            _IdPending = IdPending;
            _Status = "Not calc";
        }

        public void Calculate()
        {
            _StartTime = DateTime.Now.TimeOfDay;

            switch (IdPortfolio)
            {
                case 4:
                    _PortFilter = "4,5,6";
                    _IdPortfolioBRL = 5;
                    _IdPortfolioOther = 6;
                    break;
                case 10:
                    _PortFilter = "10,11";
                    _IdPortfolioBRL = 11;
                    _IdPortfolioOther = 0;
                    break;
                case 18:
                    _PortFilter = "18,17";
                    _IdPortfolioBRL = 17;
                    _IdPortfolioOther = 0;
                    break;
                case 38:
                    _PortFilter = "38,39";
                    _IdPortfolioBRL = 39;
                    _IdPortfolioOther = 0;
                    break;
                case 43:
                    _PortFilter = "43,42,41";
                    _IdPortfolioBRL = 41;
                    _IdPortfolioOther = 42;
                    break;
                case 50:
                    _PortFilter = "50,51";
                    _IdPortfolioBRL = 51;
                    _IdPortfolioOther = 0;
                    break;
                case 60:
                    _PortFilter = "60,61";
                    _IdPortfolioBRL = 61;
                    _IdPortfolioOther = 0;
                    break;
                default:
                    _PortFilter = "";
                    break;
            }

            _Status = "Calculating";

            LBCalculator curCalc = new LBCalculator(_PositionDate, _IdPortfolio, _PortFilter);
            curCalc.Calculate();

            _Status = "Inserting Positions";

            InsertPositions(curCalc);
            _CreateLines = DateTime.Now.TimeOfDay.Subtract(_StartTime);

            _Status = "Old routines started";

            CalculateLines(_PositionDate, curCalc.PortFilter, _IdPortfolio, _IdPortfolioBRL, _IdPortfolioOther);

            AdjustCash(_PositionDate, _IdPortfolio, _IdPortfolioBRL, _IdPortfolioOther);

            //UpdateLoans(_PositionDate);

            _CalcLines = DateTime.Now.TimeOfDay.Subtract(_CreateLines).Subtract(_StartTime);
            
            _TotalTimeTaken = DateTime.Now.TimeOfDay.Subtract(_StartTime);
            _Status = "Finished";

            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("UPDATE Tb207_Pending SET [Status]=5 WHERE Id_Pending=" + this.IdPending);
            }

            //Console.WriteLine(_IdPortfolio + "\t" + PositionDate + "\t" + DateTime.Now.TimeOfDay.Subtract(_StartTime).TotalSeconds + "\tFINISHED");
        }

        private void InsertPositions(LBCalculator curLBCalculator)
        {
            //bool FirstVal = true;

            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("DELETE FROM Tb000_Historical_Positions WHERE [Date Now]='" + curLBCalculator.PositionDate.ToString("yyyy-MM-dd") + "' AND [Id Portfolio] IN(" + curLBCalculator.PortFilter + ")");

                DateTime iniTime = DateTime.Now;

                foreach (PositionItem curPositionItem in curLBCalculator.AllPositions)
                {
                    //if (curPositionItem.IdPortfolio == 10)
                    {
                        string StringSQL = "INSERT INTO Tb000_Historical_Positions([Id Portfolio], [Id Book], [Id Section], [Id Ticker], Position, [Last Position], [Date Now], "
                            + "Close_Date, NAV, [NAV pC], [Id Administrator], [Prev Cash uC], [Prev Cash uC Admin], [Prev Asset P/L], [Prev Asset P/L Admin], [Adjusted Fw Position], "
                            + "[Close], [Close Admin], [Cost Close], [Realized], [Cost Close Admin], [Realized Admin], [Initial Position], [Quantity Bought], [Quantity Sold], [Last Calc], "
                            + "[Last Cost Close Calc], [Id Source Close], [Flag Close Admin], [Id Source Close Admin], [Cash FLow], [Trade Flow], [Dividends])\r\n ";

                        //if (!FirstVal) StringSQL += "UNION ALL\r\n";

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
                        //FirstVal = false;
                        curConn.ExecuteNonQuery(StringSQL);
                        StringSQL = "";
                    }
                }



                double TimeTaken = DateTime.Now.Subtract(iniTime).TotalSeconds;
            }
        }

        private void UpdateLoans(DateTime PositionDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.[PROC_UpdateLoans_Historical] " + _IdPortfolio + ", '" + PositionDate.ToString("yyyy-MM-dd") + "'");
                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.[PROC_UpdateLoans_Historical] " + _IdPortfolioBRL + ", '" + PositionDate.ToString("yyyy-MM-dd") + "'");
                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.[PROC_UpdateLoans_Historical] " + _IdPortfolioOther + ", '" + PositionDate.ToString("yyyy-MM-dd") + "'");
            }
        }

        private void CalculateLines(DateTime PositionDate, string PortFilter, int IdPortfolio, int IdPortfolioBRL, int IdPortfolioOther)
        {
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery(" EXEC [dbo].[Proc_Atualiza_Dados_Fixos_Historical] '" + PositionDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + "");
                curConn.ExecuteNonQuery(" EXEC [dbo].[Proc_Atualiza_Dados_Fixos_Historical] '" + PositionDate.ToString("yyyy-MM-dd") + "', " + IdPortfolioBRL + "");
                curConn.ExecuteNonQuery(" EXEC [dbo].[Proc_Atualiza_Dados_Fixos_Historical] '" + PositionDate.ToString("yyyy-MM-dd") + "', " + IdPortfolioOther + "");

                string StringSQL = "SELECT * FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]='" + PositionDate.ToString("yyyy-MM-dd") + "' AND [Id Portfolio] IN (" + PortFilter + ") ORDER BY CASE WHEN [Id Instrument]=3 THEN 1 ELSE 0 END";

                DataTable curTable = curConn.Return_DataTable(StringSQL);

                _LinesToCalc = curTable.Rows.Count;
                _LinesCalculated = 0;

                foreach (DataRow curRow in curTable.Rows)
                {
                    _Status = "Calculating: " + curRow["Ticker"];
                    curConn.ExecuteNonQuery("EXEC [PROC_GET_CALCULATE_FIELDS_Hist] @Id_Position = " + NestDLL.Utils.ParseToDouble(curRow["Id Position"]) + ", @Flag_Historic=1");
                    _LinesCalculated++;
                    //if (_LinesCalculated / 50.0 == (int)(_LinesCalculated / 50)) { Console.WriteLine(_LinesCalculated + "/" + _LinesToCalc); }
                }
            }
        }

        public void AdjustCash(DateTime CloseDate, int IdPortfolio, int IdPortfolioBRL, int IdPortfolioOther)
        {
            using (newNestConn curConn = new newNestConn())
            {
                double PortValBRL = curConn.Return_Double("SELECT coalesce(Valor_PL,0) FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + IdPortfolioBRL + " AND Data_PL='" + CloseDate.ToString("yyyy-MM-dd") + "'");
                double PortValOther = curConn.Return_Double("SELECT coalesce(Valor_PL,0) FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + IdPortfolioOther + " AND Data_PL='" + CloseDate.ToString("yyyy-MM-dd") + "'");

                double DBValBRL = curConn.Return_Double("SELECT coalesce(SUM([Cash]),0) FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]= " + IdPortfolioBRL + " AND [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "'");
                double DBValOther = curConn.Return_Double("SELECT coalesce(SUM([Cash]),0) FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]= " + IdPortfolioOther + " AND [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "'");

                double IdPosCashBRL_Main = curConn.Return_Double("SELECT [Id Position] FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]= " + IdPortfolio + " AND [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "' AND [Id Book]=5 AND [Id Section]=1 AND [Id Ticker]=1844");
                double IdPosCashBRL_BRL = curConn.Return_Double("SELECT [Id Position] FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]= " + IdPortfolioBRL + " AND [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "' AND [Id Book]=5 AND [Id Section]=1 AND [Id Ticker]=1844");

                double IdPosCashOther_Main = curConn.Return_Double("SELECT [Id Position] FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]= " + IdPortfolio + " AND [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "' AND [Id Book]=5 AND [Id Section]=1 AND [Id Ticker]=5791");
                double IdPosCashOther_Other = curConn.Return_Double("SELECT [Id Position] FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]= " + IdPortfolioOther + " AND [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "' AND [Id Book]=5 AND [Id Section]=1 AND [Id Ticker]=5791");

                if (IdPortfolioOther == 0)
                {
                    PortValOther = 0;
                    DBValOther = 0;
                    IdPosCashOther_Other = 0;
                    IdPosCashOther_Main = 0;
                    IdPosCashOther_Other = 0;
                }

                curConn.ExecuteNonQuery("UPDATE dbo.Tb000_Historical_Positions SET Position=" + (PortValBRL - DBValBRL).ToString().Replace(",", ".") + " WHERE [Id Position]=" + IdPosCashBRL_Main);
                curConn.ExecuteNonQuery("UPDATE dbo.Tb000_Historical_Positions SET Position=" + (PortValBRL - DBValBRL).ToString().Replace(",", ".") + " WHERE [Id Position]=" + IdPosCashBRL_BRL);

                curConn.ExecuteNonQuery("UPDATE dbo.Tb000_Historical_Positions SET Position=" + (PortValOther - DBValOther).ToString().Replace(",", ".") + " WHERE [Id Position]=" + IdPosCashOther_Main);
                curConn.ExecuteNonQuery("UPDATE dbo.Tb000_Historical_Positions SET Position=" + (PortValOther - DBValOther).ToString().Replace(",", ".") + " WHERE [Id Position]=" + IdPosCashOther_Other);


                curConn.ExecuteNonQuery("EXEC [PROC_GET_CALCULATE_FIELDS_Hist] @Id_Position = " + IdPosCashBRL_Main + ", @Flag_Historic=1");
                curConn.ExecuteNonQuery("EXEC [PROC_GET_CALCULATE_FIELDS_Hist] @Id_Position = " + IdPosCashBRL_BRL + ", @Flag_Historic=1");
                if (IdPortfolioOther != 0)
                {
                    curConn.ExecuteNonQuery("EXEC [PROC_GET_CALCULATE_FIELDS_Hist] @Id_Position = " + IdPosCashOther_Main + ", @Flag_Historic=1");
                    curConn.ExecuteNonQuery("EXEC [PROC_GET_CALCULATE_FIELDS_Hist] @Id_Position = " + IdPosCashOther_Other + ", @Flag_Historic=1");
                }
            }
        }

        public bool Equals(HistoricCalc obj)
        {
            if (this.IdPending == obj.IdPending)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
