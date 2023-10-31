using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;

namespace LBHistCalc
{
    class LBCalculator
    {
        private DateTime _PositionDate; public DateTime PositionDate { get { return _PositionDate; } }
        private int _IdPortfolio; public int IdPortfolio { get { return _IdPortfolio; } }
        private string _PortFilter; public string PortFilter { get { return _PortFilter; } }
        private DateTime _CloseDate = new DateTime(1900, 01, 01); public DateTime CloseDate { get { return _CloseDate; } }

        private TimeSpan _TimeUpdateSecurityInfo;
        private TimeSpan _TimeUpdateClosePrices;

        public DataTable curPositionTable;
        public DataTable curTransactionTable;
        public DataTable curTradesTable;

        public List<PositionItem> AllPositions = new List<PositionItem>();

        Dictionary<int, PortDataItem> PortData = new Dictionary<int, PortDataItem>();

        Utils curUtils = new Utils();

        public LBCalculator(DateTime PositionDate, int IdPortfolio, string PortFilter)
        {
            _PositionDate = PositionDate;
            _PortFilter = PortFilter;
            _IdPortfolio = IdPortfolio;
        }

        public void Calculate()
        {
            using (newNestConn curConn = new newNestConn())
            {
                _CloseDate = curConn.Return_DateTime("SELECT MAX(Data_PL) FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + _IdPortfolio + " AND Data_PL<'" + _PositionDate.ToString("yyyy-MM-dd") + "'");
            }

            curPositionTable = DBUtils.LoadPositionTable(CloseDate, PortFilter);
            curTransactionTable = DBUtils.LoadTransactionTable(_PositionDate, _PositionDate, PortFilter);
            curTradesTable = DBUtils.LoadTradesTable(_PositionDate, _PositionDate, PortFilter);

            Calculate(curPositionTable, curTransactionTable, curTradesTable);
        }

        public void Calculate(DataTable PositionTable, DataTable TransactionTable, DataTable TradesTable)
        {
            DateTime iniTime = DateTime.Now;

            AllPositions.Clear();

            using (newNestConn curConn = new newNestConn())
            {
                _CloseDate = curConn.Return_DateTime("SELECT MAX(Data_PL) FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + _IdPortfolio + " AND Data_PL<'" + PositionDate.ToString("yyyy-MM-dd") + "'");
            }

            if (_CloseDate == new DateTime(1900, 01, 01))
                return;

            LoadPortData(CloseDate);
            GlobalVars.Instance.LoadAccounts();

            InsertPositionRows(PositionTable);
            InsertTransactionRows(TransactionTable);
            InsertTradesRows(TradesTable, PositionDate);

            UpdateSecurityInfo();

            ClearEmptyPositions();

            SetPortData(PositionDate);

            UpdateClosePrices(CloseDate);
            UpdateCostData();

            SetCashRowsToZero();
        }

        #region Load Position, Transaction and Trade Data



        #endregion

        public void InsertPositionRows(DataTable PositionTable)
        {
            DateTime iniTime = DateTime.Now;
            foreach (DataRow curRow in PositionTable.Rows)
            {
                if ((PortFilter + ",").Contains(curRow["Id Portfolio"].ToString() + ","))
                {
                    PositionItem curPositionItem = new PositionItem();
                    curPositionItem.IdPortfolio = (int)NestDLL.Utils.ParseToDouble(curRow["Id Portfolio"]);
                    curPositionItem.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    curPositionItem.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section"]);
                    curPositionItem.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id Ticker"]);

                    if (curPositionItem.IdSecurity == 1844)
                    {

                    }

                    curPositionItem.InitialPosition = NestDLL.Utils.ParseToDouble(curRow["Position"]);

                    curPositionItem.PrevCashuC = NestDLL.Utils.ParseToDouble(curRow["Cash uC"]);
                    curPositionItem.PrevCashuCAdmin = NestDLL.Utils.ParseToDouble(curRow["Cash uC Admin"]);
                    curPositionItem.PrevAssetPL = NestDLL.Utils.ParseToDouble(curRow["Asset P/L pC"]);
                    curPositionItem.PrevAssetPLAdmin = NestDLL.Utils.ParseToDouble(curRow["Asset P/L pC Admin"]);

                    ProccessRow(curPositionItem);
                }
            }
            double TimeTaken = DateTime.Now.Subtract(iniTime).TotalSeconds;
        }

        public void InsertTransactionRows(DataTable TransactionTable)
        {
            DateTime iniTime = DateTime.Now;
            foreach (DataRow curRow in TransactionTable.Rows)
            {
                DateTime rowDate = NestDLL.Utils.ParseToDateTime(curRow["Trade_Date"]);
                if (rowDate > _CloseDate && rowDate <= _PositionDate)
                {
                    // SIDE 1

                    PositionItem curPositionItem = new PositionItem();
                    curPositionItem.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book1"]);
                    curPositionItem.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section1"]);
                    curPositionItem.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Ticker1"]);
                    curPositionItem.LotSize = GlobalVars.Instance.getLotSize((int)curPositionItem.IdSecurity, curPositionItem.DateNow);

                    int TransactionType = (int)NestDLL.Utils.ParseToDouble(curRow["Transaction_Type"]);

                    double tempQuantity = NestDLL.Utils.ParseToDouble(curRow["Quantity1"]);

                    if (curPositionItem.IdSecurity == 476)
                    {

                    }

                    double AdjFactor = curPositionItem.LotSize;
                    if (TransactionType == 86) AdjFactor = 1 / curPositionItem.LotSize;

                    if (tempQuantity > 0)
                    {
                        switch (TransactionType)
                        {
                            case 80:
                            case 81:
                            case 82:
                            case 84:
                            case 85:
                            case 86:
                                curPositionItem.QuantBought_Fwd = tempQuantity;
                                curPositionItem.AmtBought_Fwd = -NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem.QuantBought_Div = tempQuantity;
                                curPositionItem.AmtBought_Div = NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 60:
                            case 70:
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem.QuantBought_Other = tempQuantity;
                                curPositionItem.AmtBought_Other = -NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                        }
                    }
                    else
                    {
                        switch (TransactionType)
                        {
                            case 80:
                            case 81:
                            case 82:
                            case 84:
                            case 85:
                            case 86:
                                curPositionItem.QuantSold_Fwd = tempQuantity;
                                curPositionItem.AmtSold_Fwd = -NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem.QuantSold_Div = tempQuantity;
                                curPositionItem.AmtSold_Div = NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 60:
                            case 70:
                                curPositionItem.QuantSold_Other = tempQuantity;
                                curPositionItem.AmtSold_Other = -NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem.QuantSold_Other = tempQuantity;
                                curPositionItem.AmtSold_Other = -NestDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                        }
                    }

                    ProccessRow(curPositionItem, (int)NestDLL.Utils.ParseToDouble(curRow["Id_Account1"]));

                    // SIDE 2

                    PositionItem curPositionItem2 = new PositionItem();
                    curPositionItem2.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book2"]);
                    curPositionItem2.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section2"]);
                    curPositionItem2.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Ticker2"]);
                    curPositionItem2.LotSize = GlobalVars.Instance.getLotSize((int)curPositionItem2.IdSecurity, curPositionItem2.DateNow);

                    if (curPositionItem2.IdSecurity == 0)
                    {
                    }

                    double tempQuantity2 = NestDLL.Utils.ParseToDouble(curRow["Quantity1"]);

                    if (curPositionItem2.IdSecurity == 1844)
                    {

                    }

                    if (tempQuantity2 > 0)
                    {
                        switch (TransactionType)
                        {
                            case 80:
                            case 81:
                            case 82:
                            case 84:
                            case 85:
                            case 86:
                                curPositionItem2.QuantBought_Fwd = tempQuantity;
                                curPositionItem2.AmtBought_Fwd = -NestDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem2.QuantBought_Div = tempQuantity;
                                curPositionItem2.AmtBought_Div = -NestDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 60:
                            case 70:
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem2.QuantBought_Other = tempQuantity;
                                curPositionItem2.AmtBought_Other = -NestDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                        }
                    }
                    else
                    {
                        switch (TransactionType)
                        {
                            case 80:
                            case 81:
                            case 82:
                            case 84:
                            case 85:
                            case 86:
                                curPositionItem2.QuantSold_Fwd = tempQuantity;
                                curPositionItem2.AmtSold_Fwd = -NestDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem2.QuantSold_Div = tempQuantity;
                                curPositionItem2.AmtSold_Div = -NestDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 60:
                            case 70:
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem2.QuantSold_Other = tempQuantity;
                                curPositionItem2.AmtSold_Other = -NestDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                        }
                    }

                    ProccessRow(curPositionItem2, (int)NestDLL.Utils.ParseToDouble(curRow["Id_Account2"]));
                }
            }
            double TimeTaken = DateTime.Now.Subtract(iniTime).TotalSeconds;
        }

        public void InsertTradesRows(DataTable TradesTable, DateTime PositionDate)
        {
            DateTime iniTime = DateTime.Now;
            foreach (DataRow curRow in TradesTable.Rows)
            {
                DateTime rowDate = NestDLL.Utils.ParseToDateTime(curRow["Data_Trade"]);
                if (rowDate > _CloseDate && rowDate <= _PositionDate)
                {
                    PositionItem curPositionItem = new PositionItem();
                    curPositionItem.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    curPositionItem.DateNow = PositionDate;
                    //curPositionItem.IdPortfolio = GlobalVars.Instance.GetPort((int)NestDLL.Utils.ParseToDouble(curRow["Id_Account"]), (int)NestDLL.Utils.ParseToDouble(curRow["Id_Port_Type"]));
                    curPositionItem.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section"]);
                    curPositionItem.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Ativo"]);
                    if (curPositionItem.IdSecurity == 476 || curPositionItem.IdSecurity == 269)
                    {
                        //tempFactor = 100000;
                    }

                    curPositionItem.LotSize = GlobalVars.Instance.getLotSize((int)curPositionItem.IdSecurity, curPositionItem.DateNow);
                    curPositionItem.IdInstrument = GlobalVars.Instance.getIdInstrument((int)curPositionItem.IdSecurity, curPositionItem.DateNow);
                    curPositionItem.DateNow = PositionDate;

                    int tempFactor = 1;



                    double tempQuantity = NestDLL.Utils.ParseToDouble(curRow["Done"]);

                    if (tempQuantity > 0)
                    {
                        curPositionItem.QuantBought_Trade = tempQuantity;
                        if (curPositionItem.IdInstrument == 16)
                        {
                            using (newNestConn curConn = new newNestConn())
                            {
                                {
                                    double tempPU = curConn.Return_Double("SELECT NESTDB.dbo.FCN_Calcula_PU(" + curPositionItem.IdSecurity + "," + NestDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]).ToString().Replace(",", ".") + "/100.00,'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "','" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "')");
                                    curPositionItem.AmtBought_Trade = -tempPU * tempQuantity;
                                }
                            }
                        }
                        else if (curPositionItem.IdInstrument == 3)
                        {
                            curPositionItem.AmtBought_Trade = NestDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantBought_Trade;// *curPositionItem.LotSize;
                        }
                        else
                        {
                            curPositionItem.AmtBought_Trade = NestDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantBought_Trade * curPositionItem.LotSize / tempFactor;
                        }
                    }
                    else
                    {
                        curPositionItem.QuantSold_Trade = tempQuantity;
                        if (curPositionItem.IdInstrument == 16)
                        {
                            using (newNestConn curConn = new newNestConn())
                            {
                                {
                                    double tempPU = curConn.Return_Double("SELECT NESTDB.dbo.FCN_Calcula_PU(" + curPositionItem.IdSecurity + "," + NestDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]).ToString().Replace(",", ".") + "/100.00,'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "','" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "')");
                                    curPositionItem.AmtSold_Trade = -tempPU * tempQuantity;
                                }
                            }
                        }
                        else if (curPositionItem.IdInstrument == 3)
                        {

                            curPositionItem.AmtSold_Trade = NestDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantSold_Trade;// * curPositionItem.LotSize;
                        }
                        else
                        {
                            curPositionItem.AmtSold_Trade = NestDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantSold_Trade * curPositionItem.LotSize;
                        }
                    }

                    ProccessRow(curPositionItem, (int)NestDLL.Utils.ParseToDouble(curRow["Id_Account"]));

                    // ====================== Create Cash transaction for the Trade 

                    switch (curPositionItem.IdInstrument)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 5:
                        case 7:
                        case 13:
                        case 15:
                        case 26:
                            PositionItem curPositionItem_Cash = new PositionItem();
                            curPositionItem_Cash.IdBook = 5;
                            curPositionItem_Cash.IdPortfolio = curPositionItem.IdPortfolio;
                            curPositionItem_Cash.IdSection = 1;
                            curPositionItem_Cash.IdSecurity = GlobalVars.Instance.getCashId((int)NestDLL.Utils.ParseToDouble(curRow["Currency"]));
                            curPositionItem_Cash.LotSize = GlobalVars.Instance.getLotSize((int)curPositionItem_Cash.IdSecurity, curPositionItem.DateNow);

                            double tempQuantity2 = -(curPositionItem.AmtBought_Trade + curPositionItem.AmtSold_Trade);

                            if (tempQuantity2 > 0)
                            {
                                curPositionItem_Cash.QuantBought_Other = tempQuantity2;
                                curPositionItem_Cash.AmtBought_Other = tempQuantity2;
                            }
                            else
                            {
                                curPositionItem_Cash.QuantSold_Other = tempQuantity2;
                                curPositionItem_Cash.AmtSold_Other = tempQuantity2;
                            }

                            ProccessRow(curPositionItem_Cash, (int)NestDLL.Utils.ParseToDouble(curRow["Id_Account"]));
                            break;
                        default:
                            break;
                    }
                }
            }
            double TimeTaken = DateTime.Now.Subtract(iniTime).TotalSeconds;
        }

        public void ProccessRow(PositionItem curPositionItem, int AccountNumber)
        {
            foreach (GlobalVars.AccountData curAccountData in GlobalVars.Instance.GetPorts(AccountNumber))
            {
                if ((PortFilter + ",").Contains(curAccountData.IdPortfolio + ","))
                {
                    PositionItem tempPositionItem = curPositionItem.GetClone();
                    tempPositionItem.IdPortfolio = curAccountData.IdPortfolio;
                    ProccessRow(tempPositionItem);
                }
            }
        }

        public void ProccessRow(PositionItem curPositionItem)
        {
            if (curPositionItem.IdSecurity != 0)
            {
                PositionItem testItem = AllPositions.Find(
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
                    testItem.Merge(curPositionItem);
                }
                else
                {
                    AllPositions.Add(curPositionItem);
                }
            }
        }

        public void ClearEmptyPositions()
        {
            List<PositionItem> ItemsToDelete = new List<PositionItem>();
            foreach (PositionItem curPositionItem in AllPositions)
            {
                if (curPositionItem.QuantBought == 0 && curPositionItem.QuantSold == 0 && curPositionItem.CurrentPosition == 0 && curPositionItem.InitialPosition == 0)
                {
                    if (curPositionItem.IdInstrument != 17 && curPositionItem.IdInstrument != 24)
                    {
                        ItemsToDelete.Add(curPositionItem);
                    }
                }
                if (curPositionItem.IdSecurity == 0)
                {
                    ItemsToDelete.Add(curPositionItem);
                }
            }

            foreach (PositionItem curPositionItem in ItemsToDelete)
            {
                AllPositions.Remove(curPositionItem);
            }
        }

        public void UpdateSecurityInfo()
        {
            using (newNestConn curConn = new newNestConn())
            {
                foreach (PositionItem curPositionItem in AllPositions)
                {
                    if (curPositionItem.IdSecurity == 476)
                    {

                    }

                    string StringSQL = "SELECT * FROM NESTDB.dbo.FCN001_Securities_All('" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "') WHERE IdSecurity=" + curPositionItem.IdSecurity + "";

                    DataRow curRow = curConn.Return_DataTable(StringSQL).Rows[0];

                    if (curRow.ToString() != "")
                    {
                        curPositionItem.IdInstrument = (int)NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                        curPositionItem.IdUnderlying = (int)NestDLL.Utils.ParseToDouble(curRow["IdUnderlying"]);
                        curPositionItem.LotSize = NestDLL.Utils.ParseToDouble(curRow["RoundLot"]);
                        curPositionItem.NestTicker = curRow["NestTicker"].ToString();

                        if (curPositionItem.IdInstrument == 12)
                        {
                            curPositionItem.AdjFwPosition = curConn.Return_Int("SELECT NESTDB.dbo.[FCN_FW_Adjusted_Quantity]('" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "', " + curPositionItem.IdPortfolio + ", " + curPositionItem.IdBook + ", " + curPositionItem.IdSection + ", " + curPositionItem.IdSecurity + ", " + curPositionItem.IdInstrument + ", " + curPositionItem.IdUnderlying + ")");
                        }
                    }
                }

            }
        }
        public void UpdateClosePrices(DateTime CloseDate)
        {
            ReturnData curReturnData;

            using (newNestConn curConn = new newNestConn())
            {
                foreach (PositionItem curPositionItem in AllPositions)
                {
                    //string StringSQL = "";
                    //string StringSQL2 = "";

                    if (curPositionItem.IdInstrument == 16)
                    {
                        curReturnData = GetClose(curPositionItem.IdSecurity, CloseDate, 25, -1);
                        //StringSQL = "SELECT * FROM FCN_GET_PRICE(" + curPositionItem.IdSecurity + ",'" + CloseDate.ToString("yyyy-MM-dd") + "',25,0,2,0,0,0)";
                    }
                    else
                    {
                        curReturnData = GetClose(curPositionItem.IdSecurity, CloseDate, 1, -1);
                        //StringSQL = "SELECT * FROM FCN_GET_PRICE(" + curPositionItem.IdSecurity + ",'" + CloseDate.ToString("yyyy-MM-dd") + "',1,0,2,0,0,0)";
                    }
                    //DataTable curTable = curConn.Return_DataTable(StringSQL);
                    //DataRow curRow = curTable.Rows[0];
                    //curPositionItem.Close = NestDLL.Utils.ParseToDouble(curRow["Return_Value"]);
                    //curPositionItem.IdSourceClose = (int)NestDLL.Utils.ParseToDouble(curRow["Source"]);

                    curPositionItem.Close = curReturnData.Value;
                    curPositionItem.IdSourceClose = curReturnData.Source;


                    if (curPositionItem.IdInstrument == 16)
                    {
                        curReturnData = GetClose(curPositionItem.IdSecurity, CloseDate, 25, -1);
                        //StringSQL2 = "SELECT * FROM FCN_GET_PRICE(" + curPositionItem.IdSecurity + ",'" + CloseDate.ToString("yyyy-MM-dd") + "',25,0,2,0,0,-1)";
                    }
                    else
                    {
                        curReturnData = GetClose(curPositionItem.IdSecurity, CloseDate, 1, -1);
                        //StringSQL2 = "SELECT * FROM FCN_GET_PRICE(" + curPositionItem.IdSecurity + ",'" + CloseDate.ToString("yyyy-MM-dd") + "',1,0,2,0,0,-1)";
                    }
                    //DataTable curTable2 = curConn.Return_DataTable(StringSQL);
                    //DataRow curRow2 = curTable.Rows[0];
                    //curPositionItem.CloseAdmin = NestDLL.Utils.ParseToDouble(curRow2["Return_Value"]);
                    //curPositionItem.IdSourceCloseAdmin = (int)NestDLL.Utils.ParseToDouble(curRow["Source"]);

                    curPositionItem.CloseAdmin = curReturnData.Value;
                    curPositionItem.IdSourceCloseAdmin = curReturnData.Source;

                    curPositionItem.DateClose = CloseDate;

                    if ((curPositionItem.IdInstrument == 17 || curPositionItem.IdInstrument == 24) && curPositionItem.Close == 0)
                    {
                        curPositionItem.Close = 1;
                        curPositionItem.CloseAdmin = 1;
                    }

                    if ((curPositionItem.IdInstrument == 16))
                    {
                        curPositionItem.Close = -curPositionItem.Close;
                        curPositionItem.CloseAdmin = -curPositionItem.CloseAdmin;
                    }
                }
            }
        }

        public ReturnData GetClose(long IdSecurity, DateTime GetDate, int SrType, int Source)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM FCN_GET_PRICE(" + IdSecurity + ",'" + GetDate.ToString("yyyy-MM-dd") + "'," + SrType + ",0,2,0,0," + Source + ")";

                DataTable curTable = curConn.Return_DataTable(StringSQL);
                DataRow curRow = curTable.Rows[0];

                ReturnData curReturnData = new ReturnData();
                curReturnData.Value = NestDLL.Utils.ParseToDouble(curRow["Return_Value"]);
                curReturnData.Source = (int)NestDLL.Utils.ParseToDouble(curRow["Source"]);

                return curReturnData;
            }
        }

        public class ReturnData
        {
            public double Value;
            public int Source;
        }

        public void SetCashRowsToZero()
        {
            // Change Cash positions to ZERO value

            foreach (PositionItem curPositionItem in AllPositions)
            {
                if (curPositionItem.IdSecurity == 1844
                    || curPositionItem.IdSecurity == 5746
                    || curPositionItem.IdSecurity == 83066
                    || curPositionItem.IdSecurity == 5747
                    || curPositionItem.IdSecurity == 5791
                    )
                {
                    curPositionItem.InitialPosition = 0;

                    curPositionItem.QuantBought_Trade = 0;
                    curPositionItem.QuantSold_Trade = 0;

                    curPositionItem.QuantBought_Div = 0;
                    curPositionItem.QuantSold_Div = 0;

                    curPositionItem.QuantBought_Fwd = 0;
                    curPositionItem.QuantSold_Fwd = 0;

                    curPositionItem.QuantBought_Other = 0;
                    curPositionItem.QuantSold_Other = 0;
                }
            }
        }

        private void LoadPortData(DateTime CloseDate)
        {
            PortData.Clear();

            using (newNestConn curConn = new newNestConn())
            {

                string StringSQL = " SELECT A.Id_Portfolio, Port_Currency, Id_Administrator, Valor_PL, Valor_PL*dbo.FCN_Convert_Moedas(Port_Currency,Port_Currency,'" + CloseDate.ToString("yyyy-MM-dd") + "') AS NAVpC " +
                                    " FROM Tb002_Portfolios A " +
                                    " INNER JOIN Tb025_Valor_PL B " +
                                    " ON A.Id_Portfolio=B.Id_Portfolio " +
                                    " WHERE Data_PL='" + CloseDate.ToString("yyyy-MM-dd") + "'";

                DataTable curTable = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in curTable.Rows)
                {
                    int IdPortfolio = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]);
                    PortDataItem curPortDataItem = new PortDataItem();
                    curPortDataItem.IdAdministrator = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Administrator"]);
                    curPortDataItem.PortCurrency = (int)NestDLL.Utils.ParseToDouble(curRow["Port_Currency"]);
                    curPortDataItem.NAV = (int)NestDLL.Utils.ParseToDouble(curRow["Valor_PL"]);
                    curPortDataItem.NAVpC = (int)NestDLL.Utils.ParseToDouble(curRow["NAVpC"]);

                    PortData.Add(IdPortfolio, curPortDataItem);
                }
            }
        }

        private void SetPortData(DateTime PositionDate)
        {
            foreach (PositionItem curPositionItem in AllPositions)
            {
                PortDataItem curPortDataItem;
                if (PortData.TryGetValue(curPositionItem.IdPortfolio, out curPortDataItem))
                {
                    curPositionItem.NAV = curPortDataItem.NAV;
                    curPositionItem.NAVpC = curPortDataItem.NAVpC;
                    curPositionItem.IdAdministrator = curPortDataItem.IdAdministrator;
                    curPositionItem.DateNow = PositionDate;
                }
            }
        }

        private void UpdateCostData()
        {
            foreach (PositionItem curPositionItem in AllPositions)
            {
                SetCostData(curPositionItem);
            }
        }

        private void SetCostData(PositionItem curPositionItem)
        {
            double AvgpRealized = 0;
            double AvgpRealizedAdmin = 0;
            double QuantRealized = 0;
            double QuantRemaining = 0;
            double Cash_Flow_Trades = 0;
            double Trade_Flow_Trades = 0;

            curPositionItem.QuantBoughtFactor = 1;
            curPositionItem.QuantSoldFactor = 1;

            if (curPositionItem.IdSecurity == 476)
            {

            }

            if (curPositionItem.QuantBoughtCF != 0) curPositionItem.AvgPriceBought = curPositionItem.AmtBoughtCF / curPositionItem.QuantBoughtCF;
            if (curPositionItem.QuantSoldCF != 0) curPositionItem.AvgPriceSold = curPositionItem.AmtSoldCF / curPositionItem.QuantSoldCF;

            if (curPositionItem.IdInstrument != 6)
            {
                if (Math.Round(curPositionItem.InitialPosition, 6) >= 0 && Math.Round(curPositionItem.CurrentPosition, 6) >= 0)
                {
                    if (Math.Round(curPositionItem.InitialPosition + curPositionItem.QuantBought, 6) != 0)
                    {
                        curPositionItem.CostClose = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought + curPositionItem.InitialPosition * curPositionItem.Close) / (curPositionItem.InitialPosition + curPositionItem.QuantBought);
                        curPositionItem.CostCloseAdmin = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought + curPositionItem.InitialPosition * curPositionItem.CloseAdmin) / (curPositionItem.InitialPosition + curPositionItem.QuantBought);
                    }
                    else
                    {
                        curPositionItem.CostClose = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought);
                        curPositionItem.CostCloseAdmin = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought);
                    }
                    AvgpRealized = curPositionItem.AvgPriceSold;
                    AvgpRealizedAdmin = curPositionItem.AvgPriceSold;

                    if (Math.Round(Math.Abs(curPositionItem.QuantSold), 6) <= Math.Round(curPositionItem.InitialPosition + curPositionItem.QuantBought, 6) || curPositionItem.QuantSold == 0)
                    {
                        QuantRealized = Math.Abs(curPositionItem.QuantSold);
                        QuantRemaining = curPositionItem.InitialPosition + curPositionItem.QuantBought + curPositionItem.QuantSold;
                    }
                    else
                    {
                        QuantRealized = curPositionItem.InitialPosition + curPositionItem.QuantBought;
                        QuantRemaining = curPositionItem.InitialPosition + curPositionItem.QuantBought + QuantRealized;
                    }
                }

                if (Math.Round(curPositionItem.InitialPosition, 6) <= 0 && Math.Round(curPositionItem.CurrentPosition, 6) <= 0)
                {
                    if ((curPositionItem.QuantSold + curPositionItem.InitialPosition) != 0)
                    {
                        curPositionItem.CostClose = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.Close) / (curPositionItem.QuantSold + curPositionItem.InitialPosition);
                        curPositionItem.CostCloseAdmin = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.CloseAdmin) / (curPositionItem.QuantSold + curPositionItem.InitialPosition);
                    }
                    else
                    {
                        curPositionItem.CostClose = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.Close);
                        curPositionItem.CostCloseAdmin = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.CloseAdmin);
                    }
                    AvgpRealized = curPositionItem.AvgPriceBought;
                    AvgpRealizedAdmin = curPositionItem.AvgPriceBought;

                    if (-Math.Round(curPositionItem.QuantBought, 6) >= Math.Round(curPositionItem.InitialPosition, 6) + curPositionItem.QuantSold || curPositionItem.QuantBought == 0)
                    {
                        QuantRealized = -curPositionItem.QuantBought;
                        QuantRemaining = curPositionItem.InitialPosition + curPositionItem.QuantBought - QuantRealized;
                    }
                    else
                    {
                        QuantRealized = curPositionItem.InitialPosition + curPositionItem.QuantSold;
                        QuantRemaining = curPositionItem.InitialPosition + curPositionItem.QuantSold - QuantRealized;
                    }
                }

                if (Math.Round(curPositionItem.InitialPosition, 6) > 0 && Math.Round(curPositionItem.CurrentPosition, 6) < 0)
                {
                    if (Math.Round(curPositionItem.InitialPosition + curPositionItem.QuantBought, 6) != 0)
                    {
                        AvgpRealized = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought + curPositionItem.InitialPosition * curPositionItem.Close) / (curPositionItem.InitialPosition + curPositionItem.QuantBought);
                        AvgpRealizedAdmin = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought + curPositionItem.InitialPosition * curPositionItem.CloseAdmin) / (curPositionItem.InitialPosition + curPositionItem.QuantBought);
                    }
                    else
                    {
                        AvgpRealized = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought);
                        AvgpRealizedAdmin = (curPositionItem.QuantBought * curPositionItem.AvgPriceBought);
                    }
                    curPositionItem.CostClose = curPositionItem.AvgPriceSold;
                    curPositionItem.CostCloseAdmin = curPositionItem.AvgPriceSold;

                    QuantRealized = curPositionItem.QuantSold - curPositionItem.CurrentPosition;
                    QuantRemaining = curPositionItem.CurrentPosition;
                }

                if (Math.Round(curPositionItem.InitialPosition, 6) < 0 && Math.Round(curPositionItem.CurrentPosition, 6) > 0)
                {
                    if ((curPositionItem.QuantSold + curPositionItem.InitialPosition) != 0)
                    {
                        AvgpRealized = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.Close) / (curPositionItem.QuantSold + curPositionItem.InitialPosition);
                        AvgpRealizedAdmin = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.CloseAdmin) / (curPositionItem.QuantSold + curPositionItem.InitialPosition);
                    }
                    else
                    {
                        AvgpRealized = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.Close);
                        AvgpRealizedAdmin = (curPositionItem.QuantSold * curPositionItem.AvgPriceSold + curPositionItem.InitialPosition * curPositionItem.CloseAdmin);
                    }
                    curPositionItem.CostClose = curPositionItem.AvgPriceBought;
                    curPositionItem.CostCloseAdmin = curPositionItem.AvgPriceBought;
                    QuantRealized = curPositionItem.QuantBought - curPositionItem.CurrentPosition;
                    QuantRemaining = curPositionItem.CurrentPosition;
                }
            }
            else
            {
                curPositionItem.QuantBoughtFactor = -1;
                curPositionItem.QuantSoldFactor = -1;
                QuantRealized = 0;
                QuantRemaining = curPositionItem.CurrentPosition;
                Cash_Flow_Trades = curPositionItem.QuantBought + curPositionItem.QuantSold;
                curPositionItem.CostClose = 1;
                curPositionItem.CostCloseAdmin = 1;
            }

            if (curPositionItem.IdSecurity == 83066)
            {
                curPositionItem.QuantBoughtFactor = 0;
                curPositionItem.QuantSoldFactor = 0;
                QuantRealized = 0;
                QuantRemaining = 0;
                Cash_Flow_Trades = 0;
                curPositionItem.CostClose = 0.00001;
                curPositionItem.CostCloseAdmin = 0.00001;
                curPositionItem.Close = 1;
            }

            if (curPositionItem.IdInstrument == 17 || curPositionItem.IdInstrument == 24)
            {
                curPositionItem.CostClose = 1;
                curPositionItem.CostCloseAdmin = 1;
                //curPositionItem.CashFlow = curPositionItem.AmtBought_Other + curPositionItem.AmtSold_Other; ============================================ ARRUMAR ===
            }

            curPositionItem.Realized = (QuantRealized * (AvgpRealized - curPositionItem.CostClose) / curPositionItem.LotSize) + curPositionItem.Dividends;
            curPositionItem.RealizedAdmin = (QuantRealized * (AvgpRealizedAdmin - curPositionItem.CostCloseAdmin) / curPositionItem.LotSize) + curPositionItem.Dividends;

            switch (curPositionItem.IdInstrument)
            {
                case 17:
                case 24:
                    curPositionItem.Realized = curPositionItem.Realized - curPositionItem.CashFlow;
                    curPositionItem.RealizedAdmin = curPositionItem.RealizedAdmin - curPositionItem.CashFlow;
                    break;
                default:
                    break;
            }

            Trade_Flow_Trades = (curPositionItem.QuantBought / curPositionItem.LotSize) * curPositionItem.AvgPriceBought + (curPositionItem.QuantSold / curPositionItem.LotSize) * curPositionItem.AvgPriceSold;

            if (curPositionItem.IdInstrument == 4 || curPositionItem.IdInstrument == 16)
            {
                //Cash_Flow_Trades = curPositionItem.PrevAssetPL;
                //curPositionItem.Realized -= curPositionItem.PrevCashuC;
                //curPositionItem.RealizedAdmin -= curPositionItem.PrevCashuC;
            }
            else
            {
                //Cash_Flow_Trades = Trade_Flow_Trades + curPositionItem.CashFlow;
            }
        }


    }
}
