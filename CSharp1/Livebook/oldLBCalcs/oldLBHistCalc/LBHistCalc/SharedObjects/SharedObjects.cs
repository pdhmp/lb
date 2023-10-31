using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using System.Data;

namespace LBHistCalc
{
    public class PositionItem : IComparable
    {
        public long DBId = 0;

        public int IdPortfolio { get; set; }
        public int IdBook { get; set; }
        public int IdSection { get; set; }
        public Int64 IdSecurity { get; set; }

        public int IdInstrument { get; set; }
        public int IdUnderlying { get; set; }

        public double QuantBought_Trade = 0;
        public double QuantSold_Trade = 0;
        public double AmtBought_Trade = 0;
        public double AmtSold_Trade = 0;

        public double QuantBought_Other = 0;
        public double QuantSold_Other = 0;
        public double AmtBought_Other = 0;
        public double AmtSold_Other = 0;

        public double QuantBought_Div = 0;
        public double QuantSold_Div = 0;
        public double AmtBought_Div = 0;
        public double AmtSold_Div = 0;

        public double QuantBought_Fwd = 0;
        public double QuantSold_Fwd = 0;
        public double AmtBought_Fwd = 0;
        public double AmtSold_Fwd = 0;

        public double AmtBoughtCF { get { return AmtBought_Trade + AmtBought_Div + AmtBought_Fwd; } }
        public double AmtSoldCF { get { return AmtSold_Trade + AmtSold_Div + AmtSold_Fwd; } }
        public double QuantBoughtCF { get { return QuantBought_Trade + QuantBought_Div + QuantBought_Fwd; } }
        public double QuantSoldCF { get { return QuantSold_Trade + QuantSold_Div + QuantSold_Fwd; } }

        public double Dividends { get { return AmtBought_Div + AmtSold_Div; } }

        public double CashFlow
        {
            get
            {
                if (this.IdInstrument != 4 && this.IdInstrument != 16)
                {
                    return AmtBought_Trade + AmtBought_Fwd + AmtBought_Other + AmtSold_Trade + AmtSold_Fwd + AmtSold_Other - Dividends;
                }
                else if (this.InitialPosition != 0)
                {
                    return -this.PrevAssetPL;
                }
                else
                {
                    return 0;
                }
            }
        }

        public double TradeFlow { get { return AmtBought_Trade + AmtSold_Trade + Dividends; } }

        public double QuantBoughtFactor = 1;
        public double QuantSoldFactor = 1;

        public double AvgPriceBought = 0;
        public double AvgPriceSold = 0;

        public double InitialPosition = 0;

        public double CurrentPosition { get { return InitialPosition + QuantBought + QuantSold; } }
        public double QuantBought { get { return QuantBoughtCF + QuantBought_Other; } }
        public double QuantSold { get { return QuantSoldCF + QuantSold_Other; } }

        public double Close = 0;
        public double CloseAdmin = 0;

        public int IdSourceClose = 0;
        public int IdSourceCloseAdmin = 0;

        public DateTime DateNow = new DateTime(1900, 1, 1);
        public DateTime DateClose = new DateTime(1900, 1, 1);

        public double NAV = 0;
        public double NAVpC = 0;
        public int IdAdministrator = 0;

        public double PrevCashuC = 0;
        public double PrevCashuCAdmin = 0;
        public double PrevAssetPL = 0;
        public double PrevAssetPLAdmin = 0;

        public double Realized = 0;
        public double RealizedAdmin = 0;

        public double AdjFwPosition = 0;
        public double LotSize = 0;

        public double CostClose = 0;
        public double CostCloseAdmin = 0;

        public double FlagCloseAdmin = 0;

        public string NestTicker = "";

        public PositionItem()
        {

        }

        public void Merge(PositionItem MergePositionItem)
        {
            this.QuantBought_Trade += MergePositionItem.QuantBought_Trade;
            this.QuantSold_Trade += MergePositionItem.QuantSold_Trade;
            this.AmtBought_Trade += MergePositionItem.AmtBought_Trade;
            this.AmtSold_Trade += MergePositionItem.AmtSold_Trade;

            this.QuantBought_Other += MergePositionItem.QuantBought_Other;
            this.QuantSold_Other += MergePositionItem.QuantSold_Other;
            this.AmtBought_Other += MergePositionItem.AmtBought_Other;
            this.AmtSold_Other += MergePositionItem.AmtSold_Other;

            this.QuantBought_Div += MergePositionItem.QuantBought_Div;
            this.QuantSold_Div += MergePositionItem.QuantSold_Div;
            this.AmtBought_Div += MergePositionItem.AmtBought_Div;
            this.AmtSold_Div += MergePositionItem.AmtSold_Div;

            this.QuantBought_Fwd += MergePositionItem.QuantBought_Fwd;
            this.QuantSold_Fwd += MergePositionItem.QuantSold_Fwd;
            this.AmtBought_Fwd += MergePositionItem.AmtBought_Fwd;
            this.AmtSold_Fwd += MergePositionItem.AmtSold_Fwd;
        }

        public PositionItem GetClone()
        {
            PositionItem newPositionItem = (PositionItem)this.MemberwiseClone();
            return newPositionItem;
        }

        public int CompareTo(object obj)
        {
            PositionItem Temp = (PositionItem)obj;
            if (this.IdPortfolio == Temp.IdPortfolio && this.IdBook == Temp.IdBook && this.IdSection == Temp.IdSection && this.IdSecurity == Temp.IdSecurity)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public string GetUpdateString(object obj)
        {
            string UpdateString = "";

            PositionItem Temp = (PositionItem)obj;

            if (this.CurrentPosition != Temp.CurrentPosition) UpdateString += ", Position=" + Temp.CurrentPosition.ToString().Replace(",", ".");
            if (this.DateClose != Temp.DateClose) UpdateString += ", Close_Date='" + Temp.DateClose.ToString("yyyy-MM-dd") + "'";
            if (this.DateNow != Temp.DateNow) UpdateString += ", [Date Now]='" + Temp.DateNow.ToString("yyyy-MM-dd") + "'";
            if (this.NAV != Temp.NAV) UpdateString += ", NAV=" + Temp.NAV.ToString().Replace(",", ".");
            if (this.NAVpC != Temp.NAVpC) UpdateString += ", [NAV pC]=" + Temp.NAVpC.ToString().Replace(",", ".");
            if (this.IdAdministrator != Temp.IdAdministrator) UpdateString += ", [Id Administrator]=" + Temp.IdAdministrator;
            if (this.PrevCashuC != Temp.PrevCashuC) UpdateString += ", [Prev Cash uC]=" + Temp.PrevCashuC.ToString().Replace(",", ".");
            if (this.PrevCashuCAdmin != Temp.PrevCashuCAdmin) UpdateString += ", [Prev Cash uC Admin]=" + Temp.PrevCashuCAdmin.ToString().Replace(",", ".");
            if (this.PrevAssetPL != Temp.PrevAssetPL) UpdateString += ", [Prev Asset P/L]=" + Temp.PrevAssetPL.ToString().Replace(",", ".");
            if (this.PrevAssetPLAdmin != Temp.PrevAssetPLAdmin) UpdateString += ", [Prev Asset P/L Admin]=" + Temp.PrevAssetPLAdmin.ToString().Replace(",", ".");
            if (this.AdjFwPosition != Temp.AdjFwPosition) UpdateString += ", [Adjusted Fw Position]=" + Temp.AdjFwPosition.ToString().Replace(",", ".");
            if (this.Close != Temp.Close) UpdateString += ", [Close]=" + Temp.Close.ToString().Replace(",", ".");
            if (this.CloseAdmin != Temp.CloseAdmin) UpdateString += ", [Close Admin]=" + Temp.CloseAdmin.ToString().Replace(",", ".");
            if (this.CostClose != Temp.CostClose) UpdateString += ", [Cost Close]=" + Temp.CostClose.ToString().Replace(",", ".");
            if (this.Realized != Temp.Realized) UpdateString += ", [Realized]=" + Temp.Realized.ToString().Replace(",", ".");
            if (this.CostCloseAdmin != Temp.CostCloseAdmin) UpdateString += ", [Cost Close Admin]=" + Temp.CostCloseAdmin.ToString().Replace(",", ".");
            if (this.RealizedAdmin != Temp.RealizedAdmin) UpdateString += ", [Realized Admin]=" + Temp.RealizedAdmin.ToString().Replace(",", ".");
            if (this.InitialPosition != Temp.InitialPosition) UpdateString += ", [Initial Position]=" + Temp.InitialPosition.ToString().Replace(",", ".");
            if (this.IdSourceClose != Temp.IdSourceClose) UpdateString += ", [Id Source Close]=" + Temp.IdSourceClose.ToString().Replace(",", ".");
            if (this.FlagCloseAdmin != Temp.FlagCloseAdmin) UpdateString += ", [Flag Close Admin]='" + Temp.FlagCloseAdmin + "'";
            if (this.IdSourceCloseAdmin != Temp.IdSourceCloseAdmin) UpdateString += ", [Id Source Close Admin]=" + Temp.IdSourceCloseAdmin.ToString().Replace(",", ".");
            if (this.CashFlow != Temp.CashFlow) UpdateString += ", [Cash FLow]=" + Temp.CashFlow.ToString().Replace(",", ".");
            if (this.TradeFlow != Temp.TradeFlow) UpdateString += ", [Trade Flow]=" + Temp.TradeFlow.ToString().Replace(",", ".");
            if (this.Dividends != Temp.Dividends) UpdateString += ", [Dividends]=" + Temp.Dividends.ToString().Replace(",", ".");
            if ((this.QuantBought * this.QuantBoughtFactor) != (Temp.QuantBought * Temp.QuantBoughtFactor)) UpdateString += ", [Quantity Bought]=" + (Temp.QuantBought * Temp.QuantBoughtFactor).ToString().Replace(",", ".");
            if ((this.QuantSold * this.QuantSoldFactor) != (Temp.QuantSold * Temp.QuantSoldFactor)) UpdateString += ", [Quantity Sold]=" + (Temp.QuantSold * Temp.QuantSoldFactor).ToString().Replace(",", ".");



            return UpdateString;
        }

        public override string ToString()
        {
            return this.IdPortfolio + "_" + this.NestTicker + "_" + this.IdBook + "_" + this.IdSection + "_" + this.IdSecurity + "_" + this.InitialPosition + "_" + this.CurrentPosition;
        }
    }

    public class PortDataItem
    {
        public double NAV = 0;
        public double NAVpC = 0;
        public int PortCurrency = 0;
        public int IdAdministrator = 0;
    }
    
    public static class DBUtils
    {
        public static DataTable LoadPositionTable(DateTime CloseDate, string PortFilter)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "' AND [Id Portfolio] IN (" + PortFilter + ") ORDER BY [Id Portfolio], [Id Book], [Id Section], [Id Ticker]";
                DataTable ReturnTable = curConn.Return_DataTable(StringSQL);
                return ReturnTable;
            }
        }

        public static DataTable LoadTradesTable(DateTime iniDate, DateTime endDate, string PortFilter)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM NESTDB.dbo.VW_ORDENS_ALL WHERE Data_Trade>='" + iniDate.ToString("yyyy-MM-dd") + "' AND Data_Trade<='" + endDate.ToString("yyyy-MM-dd") + "' AND Status_Ordem<>4 AND Id_Port_Type=1 AND Id_Account IN (SELECT Id_Account FROM VW_PortAccounts WHERE Id_Portfolio IN (" + PortFilter + "))";
                DataTable ReturnTable = curConn.Return_DataTable(StringSQL);
                return ReturnTable;
            }
        }

        public static DataTable LoadTransactionTable(DateTime iniDate, DateTime endDate, string PortFilter)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM NESTDB.dbo.vw_Transactions_ALL_Slow_exTrades WHERE Trade_Date>='" + iniDate.ToString("yyyy-MM-dd") + "' AND Trade_Date<='" + endDate.ToString("yyyy-MM-dd") + "' AND Id_Account1 IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio IN (" + PortFilter + "))";
                DataTable ReturnTable = curConn.Return_DataTable(StringSQL);
                return ReturnTable;
            }
        }
    }
}
