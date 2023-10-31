using System;
using System.Collections.Generic;
using System.Data;
using LiveDLL;

namespace NCalculatorDLL
{
    public class LBCalculator
    {

        private DateTime _PositionDate; public DateTime PositionDate { get { return _PositionDate; } }
        private int _IdPortfolio; public int IdPortfolio { get { return _IdPortfolio; } }
        private int _IsRt;
        private string _PortFilter; public string PortFilter { get { return _PortFilter; } }
        private DateTime _CloseDate = new DateTime(1900, 01, 01); public DateTime CloseDate { get { return _CloseDate; } }

        private TimeSpan _TimeUpdateSecurityInfo;
        private TimeSpan _TimeUpdateClosePrices;

        public DataTable curPositionTable;
        public DataTable curTransactionTable;
        public DataTable curTradesTable;

        private int _LinesToCalc; public int LinesToCalc { get { return _LinesToCalc; } set { _LinesToCalc = value; } }
        private int _LinesCalculated; public int LinesCalculated { get { return _LinesCalculated; } set { _LinesCalculated = value; } }

        public string curCalcTicker = "";

        public List<PositionItem> AllPositions = new List<PositionItem>();

        public event EventHandler TickerChanged;

        Utils curUtils = new Utils();

        public LBCalculator(DateTime PositionDate, int IdPortfolio, string PortFilter, int IsRt)
        {
            _PositionDate = PositionDate;
            _PortFilter = PortFilter;
            _IdPortfolio = IdPortfolio;
            _IsRt = IsRt;
        }

        public void Calculate()
        {
            using (newNestConn curConn = new newNestConn())
            {
                _CloseDate = curConn.Return_DateTime("SELECT COALESCE(MAX(Data_PL),'2500-01-01') FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + IdPortfolio + " AND Data_PL<'" + PositionDate.ToString("yyyy-MM-dd") + "'");
            }

            curPositionTable = DBUtils.LoadPositionTable(CloseDate, PortFilter);
            curTransactionTable = DBUtils.LoadTransactionTable(PositionDate, PositionDate, PortFilter);
            curTradesTable = DBUtils.LoadTradesTable(PositionDate, PositionDate, PortFilter);

            Calculate(curPositionTable, curTransactionTable, curTradesTable);
        }

        public void ResetCloseDate(DateTime newCloseDate)
        {
            _CloseDate = newCloseDate;
            curPositionTable = DBUtils.LoadPositionTable(_CloseDate, PortFilter);
        }

        public void Calculate(DataTable PositionTable, DataTable TransactionTable, DataTable TradesTable)
        {
            GlobalVars.Instance.LoadDayBuffers(PositionDate, CloseDate); // verificar
            GlobalVars.Instance.LoadFixedBuffers(); // verificar

            DateTime iniTime = DateTime.Now;

            AllPositions.Clear();

            using (newNestConn curConn = new newNestConn())
            {
                _CloseDate = curConn.Return_DateTime("SELECT COALESCE( MAX(Data_PL), '2500-01-01') FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + IdPortfolio + " AND Data_PL<'" + PositionDate.ToString("yyyy-MM-dd") + "'");
            }

            if (CloseDate == new DateTime(1900, 01, 01)) { return; }

            LinesToCalc = AllPositions.Count;
            LinesCalculated = 0;

            InsertPositionRows(PositionTable);
            InsertTransactionRows(TransactionTable);
            InsertTradesRows(TradesTable, PositionDate);

            foreach (PositionItem curPositionItem in AllPositions)
            {
                SetPortData(curPositionItem, PositionDate);
            }

            UpdateSecurityInfo(PositionDate);

            ClearEmptyPositions(true);

            LinesToCalc = AllPositions.Count;

            DateTime StartTime = DateTime.Now;

            if (_IsRt == 1)
                GlobalVars.Instance.StatusQueue.Enqueue("Calculating Port: " + IdPortfolio + " - Lines: " + LinesToCalc);

            foreach (PositionItem curPositionItem in AllPositions)
            {
                curCalcTicker = curPositionItem.NestTicker;
                if (TickerChanged != null) TickerChanged(this, new EventArgs());
                UpdateClosePrices(curPositionItem, CloseDate);
                curPositionItem.SetCostData();
                SetOtherData(curPositionItem);
                _LinesCalculated++;
            }
            ClearEmptyPositions(false);
            curCalcTicker = "";
            if (TickerChanged != null) TickerChanged(this, new EventArgs());
        }

        #region Create Lines

        public void InsertPositionRows(DataTable PositionTable)
        {
            DateTime iniTime = DateTime.Now;
            foreach (DataRow curRow in PositionTable.Rows)
            {
                if ((PortFilter + ",").Contains(curRow["Id Portfolio"].ToString() + ","))
                {
                    PositionItem curPositionItem = new PositionItem();
                    curPositionItem.IdPortfolio = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Portfolio"]);
                    curPositionItem.IdBook = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    curPositionItem.IdSection = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Section"]);
                    curPositionItem.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Ticker"]);
                    curPositionItem.DateNow = _PositionDate;

                    curPositionItem.InitialPosition = LiveDLL.Utils.ParseToDouble(curRow["Position"]);
                    curPositionItem.PrevCashUc = LiveDLL.Utils.ParseToDouble(curRow["Cash uC"]);
                    curPositionItem.PrevCashUcAdmin = LiveDLL.Utils.ParseToDouble(curRow["Cash uC Admin"]);
                    curPositionItem.PrevAssetPl = LiveDLL.Utils.ParseToDouble(curRow["Asset P/L pC"]);
                    curPositionItem.PrevAssetPlAdmin = LiveDLL.Utils.ParseToDouble(curRow["Asset P/L pC Admin"]);
                    curPositionItem.DataPosicao = LiveDLL.Utils.ParseToDateTime(curRow["last Position"]);

                    curPositionItem.IsRT = _IsRt;

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
                DateTime rowDate = LiveDLL.Utils.ParseToDateTime(curRow["Trade_Date"]);
                if (rowDate > _CloseDate && rowDate <= _PositionDate)
                {
                    // SIDE 1
                    PositionItem curPositionItem = new PositionItem();
                    curPositionItem.IdBook = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Book1"]);
                    curPositionItem.IdSection = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Section1"]);
                    curPositionItem.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Ticker1"]);
                    curPositionItem.DateNow = _PositionDate;
                    curPositionItem.DateClose = _CloseDate;
                    curPositionItem.IsRT = _IsRt;

                    curPositionItem.LotSize = GlobalVars.Instance.GetLotSize((int)curPositionItem.IdSecurity, curPositionItem.DateNow);

                    int TransactionType = (int)LiveDLL.Utils.ParseToDouble(curRow["Transaction_Type"]);

                    double tempQuantity = LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]);

                    double AdjFactor = 1; //curPositionItem.LotSize;
                    //if(TransactionType == 86) AdjFactor = 1 / curPositionItem.LotSize;

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
                                curPositionItem.QuantBoughtFwd = tempQuantity;
                                curPositionItem.AmtBoughtFwd = -LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem.QuantBoughtDiv = tempQuantity;
                                curPositionItem.AmtBoughtDiv = LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 50:
                                curPositionItem.QuantBoughtOther = tempQuantity;
                                curPositionItem.AmtBoughtOther = 0;
                                break;
                            case 60:
                            case 70:
                                curPositionItem.QuantBoughtOther = tempQuantity;
                                curPositionItem.AmtBoughtOther = LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem.QuantBoughtOther = tempQuantity;
                                curPositionItem.AmtBoughtOther = LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
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
                                curPositionItem.QuantSoldFwd = tempQuantity;
                                curPositionItem.AmtSoldFwd = -LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem.QuantSoldDiv = tempQuantity;
                                curPositionItem.AmtSoldDiv = LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 50:
                                curPositionItem.QuantSoldOther = tempQuantity;
                                curPositionItem.AmtSoldOther = 0;
                                break;
                            case 60:
                            case 70:
                                curPositionItem.QuantSoldOther = tempQuantity;
                                curPositionItem.AmtSoldOther = -LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem.QuantSoldOther = tempQuantity;
                                curPositionItem.AmtSoldOther = -LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]) * AdjFactor;
                                break;
                        }
                    }

                    ProccessRow(curPositionItem, (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Account1"]));

                    // SIDE 2

                    PositionItem curPositionItem2 = new PositionItem();
                    curPositionItem2.IdBook = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Book2"]);
                    curPositionItem2.IdSection = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Section2"]);
                    curPositionItem2.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Ticker2"]);
                    curPositionItem2.DateNow = _PositionDate;
                    curPositionItem2.DateClose = _CloseDate;
                    curPositionItem2.IsRT = _IsRt;

                    curPositionItem2.LotSize = GlobalVars.Instance.GetLotSize((int)curPositionItem2.IdSecurity, curPositionItem.DateNow);

                    if (curPositionItem2.IdSecurity == 0)
                    {
                    }

                    double tempQuantity2 = LiveDLL.Utils.ParseToDouble(curRow["Quantity2"]);

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
                                curPositionItem2.QuantBoughtFwd = tempQuantity2;
                                curPositionItem2.AmtBoughtFwd = -LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem2.QuantBoughtDiv = tempQuantity2;
                                curPositionItem2.AmtBoughtDiv = -LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 50:
                                curPositionItem2.QuantBoughtOther = tempQuantity2;
                                curPositionItem2.AmtBoughtOther = 0;
                                break;
                            case 60:
                            case 70:
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem2.QuantBoughtOther = tempQuantity2;
                                curPositionItem2.AmtBoughtOther = -LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
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
                                curPositionItem2.QuantSoldFwd = tempQuantity2;
                                curPositionItem2.AmtSoldFwd = -LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 21:
                            case 22:
                            case 23:
                                curPositionItem2.QuantSoldDiv = tempQuantity2;
                                curPositionItem2.AmtSoldDiv = -LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                            case 50:
                                curPositionItem2.QuantSoldOther = tempQuantity2;
                                curPositionItem2.AmtSoldOther = 0;
                                break;
                            case 60:
                            case 70:
                                break;
                            case 83:
                                break;
                            default:
                                curPositionItem2.QuantSoldOther = tempQuantity2;
                                curPositionItem2.AmtSoldOther = -LiveDLL.Utils.ParseToDouble(curRow["Quantity1"]) * AdjFactor;
                                break;
                        }
                    }

                    ProccessRow(curPositionItem2, (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Account2"]));
                }
            }
            double TimeTaken = DateTime.Now.Subtract(iniTime).TotalSeconds;
        }

        public void InsertTradesRows(DataTable TradesTable, DateTime PositionDate)
        {
            DateTime iniTime = DateTime.Now;
            foreach (DataRow curRow in TradesTable.Rows)
            {
                DateTime rowDate = LiveDLL.Utils.ParseToDateTime(curRow["Data_Trade"]);
                if (rowDate > _CloseDate && rowDate <= _PositionDate)
                {
                    PositionItem curPositionItem = new PositionItem();
                    curPositionItem.IdBook = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    curPositionItem.DateNow = PositionDate;
                    //curPositionItem.IdPortfolio = GlobalVars.Instance.GetPort((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Account"]), (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Port_Type"]));
                    curPositionItem.IdSection = (int)LiveDLL.Utils.ParseToDouble(curRow["Id Section"]);
                    curPositionItem.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Ativo"]);

                    curPositionItem.LotSize = GlobalVars.Instance.GetLotSize((int)curPositionItem.IdSecurity, curPositionItem.DateNow);
                    curPositionItem.IdInstrument = GlobalVars.Instance.GetIdInstrument((int)curPositionItem.IdSecurity, curPositionItem.DateNow);
                    curPositionItem.DateNow = PositionDate;
                    curPositionItem.IsRT = _IsRt;

                    int tempFactor = 1;

                    double tempQuantity = LiveDLL.Utils.ParseToDouble(curRow["Done"]);

                    if (tempQuantity > 0)
                    {
                        curPositionItem.QuantBoughtTrade = tempQuantity;
                        if (curPositionItem.IdInstrument == 16)
                        {
                            using (newNestConn curConn = new newNestConn())
                            {
                                {
                                    double tempPU = curConn.Return_Double("SELECT NESTDB.dbo.FCN_Calcula_PU(" + curPositionItem.IdSecurity + "," + LiveDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]).ToString().Replace(",", ".") + "/100.00,'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "','" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "')");
                                    curPositionItem.AmtBoughtTrade = -tempPU * tempQuantity;
                                }
                            }
                        }
                        else if (curPositionItem.IdInstrument == 3)
                        {
                            curPositionItem.AmtBoughtTrade = LiveDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantBoughtTrade;
                        }
                        else if (curPositionItem.IdInstrument == 4 || curPositionItem.IdInstrument == 16)
                        {
                            curPositionItem.AmtBoughtTrade = LiveDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantBoughtTrade * curPositionItem.LotSize / tempFactor;
                        }
                        else
                        {
                            curPositionItem.AmtBoughtTrade = LiveDLL.Utils.ParseToDouble(curRow["Cash"]);
                        }
                    }
                    else
                    {
                        curPositionItem.QuantSoldTrade = tempQuantity;
                        if (curPositionItem.IdInstrument == 16)
                        {
                            using (newNestConn curConn = new newNestConn())
                            {
                                {
                                    double tempPU = curConn.Return_Double("SELECT NESTDB.dbo.FCN_Calcula_PU(" + curPositionItem.IdSecurity + "," + LiveDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]).ToString().Replace(",", ".") + "/100.00,'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "','" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "')");
                                    curPositionItem.AmtSoldTrade = -tempPU * tempQuantity;
                                }
                            }
                        }
                        else if (curPositionItem.IdInstrument == 3)
                        {

                            curPositionItem.AmtSoldTrade = LiveDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantSoldTrade;
                        }
                        else if (curPositionItem.IdInstrument == 4 || curPositionItem.IdInstrument == 16)
                        {
                            curPositionItem.AmtSoldTrade = LiveDLL.Utils.ParseToDouble(curRow["Avg Price Trade"]) * curPositionItem.QuantSoldTrade * curPositionItem.LotSize / tempFactor;
                        }
                        else
                        {
                            curPositionItem.AmtSoldTrade = LiveDLL.Utils.ParseToDouble(curRow["Cash"]);
                        }
                    }

                    ProccessRow(curPositionItem, (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Account"]));

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
                            curPositionItem_Cash.DateNow = PositionDate;
                            curPositionItem_Cash.IdSection = 1;
                            curPositionItem_Cash.IdSecurity = GlobalVars.Instance.getCashId((int)LiveDLL.Utils.ParseToDouble(curRow["Currency"]));
                            curPositionItem_Cash.LotSize = GlobalVars.Instance.GetLotSize((int)curPositionItem_Cash.IdSecurity, curPositionItem.DateNow);
                            curPositionItem_Cash.IsRT = _IsRt;

                            double tempQuantity2 = -(curPositionItem.AmtBoughtTrade + curPositionItem.AmtSoldTrade);

                            if (tempQuantity2 > 0)
                            {
                                curPositionItem_Cash.QuantBoughtOther = tempQuantity2;
                                curPositionItem_Cash.AmtBoughtOther = tempQuantity2;
                            }
                            else
                            {
                                curPositionItem_Cash.QuantSoldOther = tempQuantity2;
                                curPositionItem_Cash.AmtSoldOther = tempQuantity2;
                            }

                            ProccessRow(curPositionItem_Cash, (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Account"]));
                            curPositionItem_Cash.RTQuantityFlag = false;
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
            foreach (GlobalVars.AccountDataItem curAccountData in GlobalVars.Instance.GetPorts(AccountNumber))
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
                                if (testPositionItem.DateNow == curPositionItem.DateNow && testPositionItem.IdPortfolio == curPositionItem.IdPortfolio && testPositionItem.IdBook == curPositionItem.IdBook && testPositionItem.IdSection == curPositionItem.IdSection && testPositionItem.IdSecurity == curPositionItem.IdSecurity)
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
                    testItem.RTQuantityFlag = false;
                }
                else
                {
                    lock (AllPositions) { AllPositions.Add(curPositionItem); }
                    curPositionItem.RTQuantityFlag = false;
                }
            }
        }

        public void UpdateSecurityInfo(DateTime PositionDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                foreach (PositionItem curPositionItem in AllPositions)
                {
                    // ============================ SECURITY INFO =============================================================

                    GlobalVars.SymbolDataItem curSymbolDataItem = GlobalVars.Instance.GetSymbolData(curPositionItem.IdSecurity, PositionDate);

                    curPositionItem.IdSecurity = curSymbolDataItem.IdSecurity;
                    curPositionItem.IdInstrument = curSymbolDataItem.IdInstrument;
                    curPositionItem.IdPrimaryExchange = curSymbolDataItem.IdPrimaryExchange;
                    curPositionItem.ExchangeTicker = curSymbolDataItem.ExchangeTicker;
                    curPositionItem.LotSize = curSymbolDataItem.LotSize;
                    curPositionItem.Description = curSymbolDataItem.Description;
                    curPositionItem.IdPriceTable = curSymbolDataItem.IdPriceTable;
                    curPositionItem.PriceTable = curSymbolDataItem.PriceTable;
                    curPositionItem.SecurityCurrency = curSymbolDataItem.SecurityCurrency;
                    curPositionItem.IdSecurityCurrency = curSymbolDataItem.IdSecurityCurrency;
                    curPositionItem.Instrument = curSymbolDataItem.Instrument;
                    curPositionItem.AssetClass = curSymbolDataItem.AssetClass;
                    curPositionItem.SubIndustry = curSymbolDataItem.SubIndustry;
                    curPositionItem.Industry = curSymbolDataItem.Industry;
                    curPositionItem.IndustryGroup = curSymbolDataItem.IndustryGroup;
                    curPositionItem.Sector = curSymbolDataItem.Sector;
                    curPositionItem.UnderlyingCountry = curSymbolDataItem.UnderlyingCountry;
                    curPositionItem.NestSector = curSymbolDataItem.NestSector;
                    curPositionItem.Expiration = curSymbolDataItem.Expiration;
                    curPositionItem.IdUnderlying = curSymbolDataItem.IdUnderlying;
                    curPositionItem.Underlying = curSymbolDataItem.Underlying;
                    curPositionItem.Book = curSymbolDataItem.Book;
                    curPositionItem.IdSubPortfolio = curSymbolDataItem.IdSubPortfolio;
                    curPositionItem.SubPortfolio = curSymbolDataItem.SubPortfolio;
                    curPositionItem.IdStrategy = curSymbolDataItem.IdStrategy;
                    curPositionItem.Strategy = curSymbolDataItem.Strategy;
                    curPositionItem.NestTicker = curSymbolDataItem.NestTicker;
                    curPositionItem.ReutersTicker = curSymbolDataItem.ReutersTicker;
                    curPositionItem.ImagineTicker = curSymbolDataItem.ImagineTicker;
                    curPositionItem.BtgTicker = curSymbolDataItem.BtgTicker;
                    curPositionItem.ItauTicker = curSymbolDataItem.ItauTicker;
                    curPositionItem.MellonTicker = curSymbolDataItem.MellonTicker;
                    curPositionItem.AdminTicker = curSymbolDataItem.AdminTicker;
                    curPositionItem.IdAssetClass = curSymbolDataItem.IdAssetClass;
                    curPositionItem.Strike = curSymbolDataItem.Strike;
                    curPositionItem.OptionType = curSymbolDataItem.OptionType;
                    curPositionItem.ContractRatio = curSymbolDataItem.ContractRatio;
                    curPositionItem.PriceFromUnderlying = curSymbolDataItem.PriceFromUnderlying;

                    curPositionItem.RTUpdateSource = curSymbolDataItem.RTUpdateSource;
                    curPositionItem.HistUpdateSource = curSymbolDataItem.HistUpdateSource;

                    if (curSymbolDataItem.BloombergTicker != "") curPositionItem.BloombergTicker = curSymbolDataItem.BloombergTicker;


                    // ============================  BASE UNDERLYING  =============================================================

                    string StringSQL = "SELECT * FROM FCN_Get_Id_Underlying(" + curPositionItem.IdSecurity + ")";
                    DataRow curRow = curConn.Return_DataTable(StringSQL).Rows[0];
                    curPositionItem.IdBaseUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                    curPositionItem.BaseUnderlying = curRow["Ticker"].ToString();

                    curPositionItem.IdBaseUnderlyingCurrency = curConn.Return_Int("SELECT IdCurrency FROM NESTDB.dbo.FCN001_Securities(" + curPositionItem.IdBaseUnderlying + ",'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "')");
                    curPositionItem.BaseUnderlyingCurrency = curConn.Execute_Query_String("SELECT Simbolo FROM dbo.Vw_Moedas Where Id_Moeda=" + curPositionItem.IdBaseUnderlyingCurrency + "");

                    // ============================ DESCRIPTIVE FIELDS =============================================================

                    if (curPositionItem.PriceFromUnderlying == 0 || curPositionItem.IdSourceAdmin == -2)
                    {
                        curPositionItem.IdSecurityPrice = curPositionItem.IdSecurity;
                    }
                    else
                    {
                        curPositionItem.IdSecurityPrice = curPositionItem.IdUnderlying;
                    }

                    lock (GlobalVars.Instance.BookList)
                    {
                        curPositionItem.Book = GlobalVars.Instance.BookList[curPositionItem.IdBook];
                    }
                    GlobalVars.SectionDataItem curSectionDataItem;

                    lock (GlobalVars.Instance.SectionList)
                    {
                        if (GlobalVars.Instance.SectionList.TryGetValue(curPositionItem.IdSection, out curSectionDataItem))
                        {
                            curPositionItem.IdStrategy = curSectionDataItem.IdStrategy;
                            curPositionItem.IdSubPortfolio = curSectionDataItem.IdSubPortfolio;
                            curPositionItem.IdSubStrategy = curSectionDataItem.IdSubStrategy;
                            curPositionItem.Strategy = curSectionDataItem.Strategy;
                            curPositionItem.SubPortfolio = curSectionDataItem.SubPortfolio;
                            curPositionItem.SubStrategy = curSectionDataItem.SubStrategy;
                            curPositionItem.Section = curSectionDataItem.Section;
                        }
                    }
                    curPositionItem.IdCurrencyTickerRef = curPositionItem.IdSecurityCurrency;

                    if (curPositionItem.IdPortfolio == 43 && curPositionItem.IdSecurityCurrency != 900) { curPositionItem.IdCurrencyPortfolioRef = 210605; }

                    if (curPositionItem.IdPortfolio == 4 && (curPositionItem.IdSecurityCurrency == 900))
                    {
                        if (curPositionItem.DateNow >= new DateTime(2012, 04, 19))
                        {
                            curPositionItem.IdCurrencyTickerRef = 210605;
                        }

                        if (curPositionItem.DateNow >= new DateTime(2013, 05, 02))
                        {
                            curPositionItem.IdCurrencyTickerRef = 735528;
                        }
                    }
                    else
                    {
                    }

                    if (curPositionItem.IdSecurity == 81055 && DateTime.Now >= new DateTime(2012, 04, 19) && (curPositionItem.IdPortfolio == 41 || curPositionItem.IdPortfolio == 43 || (curPositionItem.IdPortfolio == 4 || curPositionItem.IdPortfolio == 5)))
                    {
                        curPositionItem.IdCurrencyPortfolioRef = 83115;
                    }

                    if (curPositionItem.IdCurrencyTickerRef == 210605 && curPositionItem.DateClose == new DateTime(2012, 04, 18))
                    {
                        curPositionItem.IdCurrencyTickerRef = 1080;
                    }

                    if (curPositionItem.IdAdministrator == 1054) { curPositionItem.IdSourceAdmin = -2; }
                    if (curPositionItem.IdAdministrator == 1057) { curPositionItem.IdSourceAdmin = -1; }

                    switch (curPositionItem.IdSecurityCurrency)
                    {
                        case 900: curPositionItem.IdTickerCash = 1844; break;
                        case 1042: curPositionItem.IdTickerCash = 5791; break;
                        case 929: curPositionItem.IdTickerCash = 31057; break;
                        default: break;
                    }

                    // Get forward info
                    if (curPositionItem.IdInstrument == 12)
                    {
                        curPositionItem.AdjFwPosition = curConn.Return_Double("SELECT NESTDB.dbo.[FCN_FW_Adjusted_Quantity]('" + PositionDate.ToString("yyyy-MM-dd") + "', " + curPositionItem.IdPortfolio + ", " + curPositionItem.IdBook + ", " + curPositionItem.IdSection + ", " + curPositionItem.IdSecurity + ", " + curPositionItem.IdInstrument + ", " + curPositionItem.IdUnderlying + ")");
                    }
                }
            }
        }

        public void ClearEmptyPositions(bool Flag)
        {
            List<PositionItem> ItemsToDelete = new List<PositionItem>();
            foreach (PositionItem curPositionItem in AllPositions)
            {
                if (curPositionItem.IsRT == 0)
                {
                    if (Flag && Math.Round(curPositionItem.QuantBought, 2) == 0 && Math.Round(curPositionItem.QuantSold, 2) == 0 && Math.Round(curPositionItem.CurrentPosition, 2) == 0)
                    {
                        // Realtime
                        if (curPositionItem.IdInstrument != 17 && curPositionItem.IdInstrument != 24 && curPositionItem.IdInstrument != 6)
                        {
                            ItemsToDelete.Add(curPositionItem);
                        }
                    }

                    if (!Flag && Math.Round(curPositionItem.QuantBought, 2) == 0 && Math.Round(curPositionItem.QuantSold, 2) == 0 && Math.Round(curPositionItem.CurrentPosition, 2) == 0 && Math.Round(curPositionItem.TotalPl, 2) == 0)
                    {
                        if (curPositionItem.IdInstrument != 17 && curPositionItem.IdInstrument != 24 && curPositionItem.IdInstrument != 6)
                        {
                            ItemsToDelete.Add(curPositionItem);
                        }
                    }
                }

                if (curPositionItem.IdSecurity == 0)
                {
                    ItemsToDelete.Add(curPositionItem);
                }
            }

            foreach (PositionItem curPositionItem in ItemsToDelete)
            {
                lock (AllPositions) { AllPositions.Remove(curPositionItem); }
            }
        }

        #endregion

        #region Calculate Lines

        private void SetPortData(PositionItem curPositionItem, DateTime PositionDate)
        {
            GlobalVars.PortDataItem curPortDataItem = GlobalVars.Instance.GetPortData(curPositionItem.IdPortfolio, PositionDate);
            if (curPortDataItem != null)
            {
                curPositionItem.Nav = curPortDataItem.NAVToday;
                curPositionItem.NavPc = curPortDataItem.NAVpCToday;

                curPositionItem.prevNAV = curPortDataItem.NAVYesterday;
                curPositionItem.prevNavPc = curPortDataItem.NAVpCYesterday;


                curPositionItem.IdAdministrator = curPortDataItem.IdAdministrator;
                curPositionItem.IdPortCurrency = curPortDataItem.IdPortCurrency;
                curPositionItem.DateNow = PositionDate;
                curPositionItem.Portfolio = curPortDataItem.Portfolio;

                curPositionItem.IdCurrencyPortfolioRef = curPositionItem.IdPortCurrency;
            }
        }

        public void UpdateClosePrices(PositionItem curPositionItem, DateTime CloseDate)
        {
            ReturnGetPrice curReturnGetPrice;

            if (curPositionItem.IdInstrument == 16)
            {
                curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, CloseDate, 25, 0, 2, 0, 0, 0);
            }
            else
            {
                curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, CloseDate, 1, 0, 2, 0, 0, 0);
            }

            curPositionItem.Close = curReturnGetPrice.ReturnValue; // here
            curPositionItem.IdSourceClose = curReturnGetPrice.Source;

            curPositionItem.CloseAdmin = curReturnGetPrice.ReturnValue;
            curPositionItem.IdSourceCloseAdmin = curReturnGetPrice.Source;

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

        public void SetOtherData(PositionItem curPositionItem)//PROC_GET_CALCULATE_FIELDS_HIST
        {
            ReturnGetPrice curReturnGetPrice;

            using (newNestConn curConn = new newNestConn())
            {

                curPositionItem.BookSize = curConn.Return_Double("SELECT TOP 1 Book_Size FROM NESTDB.dbo.tb008_Port_Books WHERE Id_Portfolio=" + curPositionItem.IdPortfolio + " AND Id_Book=" + curPositionItem.IdBook + " AND Book_CreateDate<='" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "' ORDER BY Book_CreateDate DESC");
                if (double.IsNaN(curPositionItem.BookSize)) { curPositionItem.BookSize = 1; }

                if (curPositionItem.IdBook == 3)
                {
                    curPositionItem.StrategyPercent = curConn.Return_Double("SELECT SectionPercent FROM NESTDB.dbo.Tb009_SectionBookWeights WHERE DateRef='" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "' AND IdPortfolio=" + IdPortfolio + " AND IdBook=" + curPositionItem.IdBook + " AND IdSection=" + curPositionItem.IdSection + "");
                }
                else
                {
                    curPositionItem.StrategyPercent = 0;
                }

                curPositionItem.PercentSplit = 1 + ifNaN(curConn.Return_Double("SELECT percent_bonus FROM dbo.Tb720_Dividends Where id_ticker=" + curPositionItem.IdSecurity + " and Record_Date='" + curPositionItem.DateClose.ToString("yyyy-MM-dd") + "'"), 0);

                // =========================================================================================================


                curPositionItem.Spot = ReturnCurrencyConvert((int)curPositionItem.IdCurrencyTickerRef, (int)curPositionItem.IdCurrencyPortfolioRef, curPositionItem.DateNow);

                curPositionItem.Close = curPositionItem.Close / curPositionItem.PercentSplit; // here
                curPositionItem.CloseAdmin = curPositionItem.CloseAdmin / curPositionItem.PercentSplit;

                if (curPositionItem.IdAssetClass == 2) // Debt
                {
                    curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdBaseUnderlying, curPositionItem.DateNow, 91, 0, 2, 0, 1, 0);
                    curPositionItem.Duration = curReturnGetPrice.ReturnValue;
                    curPositionItem.DurationDate = curReturnGetPrice.DateValue;

                    if (curPositionItem.IdSecurityCurrency == 1042)
                    {
                        curReturnGetPrice = GlobalVars.Instance.GetPrice(17733, curPositionItem.DateNow, 91, 0, 2, 0, 1, 0);
                        curPositionItem.Duration10Y = curReturnGetPrice.ReturnValue;
                    }

                    if (curPositionItem.IdSecurityCurrency == 900) curPositionItem.Duration10Y = 8.21;
                }
                else
                {
                    curPositionItem.Duration = 0;
                    curPositionItem.DurationDate = new DateTime(1900, 01, 01);
                    curPositionItem.Duration10Y = 1;
                }

                if (curPositionItem.IdInstrument == 1 || curPositionItem.IdInstrument == 2 || curPositionItem.IdInstrument == 7 || curPositionItem.IdInstrument == 12)
                {
                    GlobalVars.LoanDataItem curLoanDataItem = GlobalVars.Instance.GetLoanData(curPositionItem.IdSecurity, curPositionItem.DateNow);
                    if (curPositionItem.IdInstrument == 12) curLoanDataItem = GlobalVars.Instance.GetLoanData(curPositionItem.IdUnderlying, curPositionItem.DateNow);
                    if (curLoanDataItem != null)
                    {
                        curPositionItem.LoanRateInCblc = curLoanDataItem.LoanRateIn / 100;
                        curPositionItem.LoanRateOutCblc = curLoanDataItem.LoanRateOut / 100;
                        curPositionItem.LoanMktTotal = curLoanDataItem.LoanMarketSize;
                    }
                }

                curPositionItem.FactorFi = GlobalVars.Instance.GetPrice(curPositionItem.IdBaseUnderlying, curPositionItem.DateNow, 200, 0, 2, 0, 0, 0).ReturnValue;
                curPositionItem.FactorEqut = GlobalVars.Instance.GetPrice(curPositionItem.IdBaseUnderlying, curPositionItem.DateNow, 201, 0, 2, 0, 0, 0).ReturnValue;
                curPositionItem.FactorCurr = GlobalVars.Instance.GetPrice(curPositionItem.IdBaseUnderlying, curPositionItem.DateNow, 202, 0, 2, 0, 0, 0).ReturnValue;

                curPositionItem.SpotClose = ReturnCurrencyConvert((int)curPositionItem.IdCurrencyTickerRef, (int)curPositionItem.IdCurrencyPortfolioRef, curPositionItem.DateClose);

                if (curPositionItem.IdInstrument != 16 && curPositionItem.IdUnderlying != 1080)
                {
                    curPositionItem.CurrencyChange = curPositionItem.CostClose * (ReturnCurrencyConvert(curPositionItem.IdSecurityCurrency, curPositionItem.IdPortCurrency, curPositionItem.DateNow) - ReturnCurrencyConvert(curPositionItem.IdSecurityCurrency, curPositionItem.IdPortCurrency, curPositionItem.DateClose));
                }
                else
                {
                    curPositionItem.CurrencyChange = 0;
                }

                curPositionItem.CurrencyChangeAdmin = curPositionItem.CostCloseAdmin * (ReturnCurrencyConvert(curPositionItem.IdSecurityCurrency, curPositionItem.IdPortCurrency, curPositionItem.DateNow) - ReturnCurrencyConvert(curPositionItem.IdSecurityCurrency, curPositionItem.IdPortCurrency, curPositionItem.DateClose));

                curPositionItem.CurrencyPl = (curPositionItem.PrevCashUc * curPositionItem.Spot) - (curPositionItem.PrevCashUc * curPositionItem.SpotClose);
                curPositionItem.CurrencyPlAdmin = (curPositionItem.PrevCashUcAdmin * curPositionItem.Spot) - (curPositionItem.PrevCashUcAdmin * curPositionItem.SpotClose);

                if (curPositionItem.IdInstrument == 12)
                {
                    UpdateFowardInfo(curPositionItem);
                }

                if (curPositionItem.IdInstrument == 3 || curPositionItem.IdInstrument == 26) // Options or Warrants
                {
                    curPositionItem.MktCap = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 20, curPositionItem.IdSecurityCurrency, 2, 0, curPositionItem.IsRT, 0).ReturnValue;
                    curPositionItem.AvVolume6m = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 19, 0, 2, 0, curPositionItem.IsRT, 0).ReturnValue;

                    ReturnGetRate curReturnGetRate = GlobalVars.Instance.GetRate(curPositionItem.DateNow, curPositionItem.Expiration, curPositionItem.IdSecurityCurrency);
                    curPositionItem.RateYear = curReturnGetRate.RateYear;
                    curPositionItem.RatePeriod = curReturnGetRate.RatePeriod;
                    curPositionItem.DaysToExpiration = curReturnGetRate.BusDays;
                    curPositionItem.YearFraction = curReturnGetRate.YearFraction;

                    curPositionItem.UnderlyingLastPc = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 1, curPositionItem.IdPortCurrency, 2, 0, curPositionItem.IsRT, 0).ReturnValue;
                    curPositionItem.UnderlyingLastAdmin = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 1, curPositionItem.IdSecurityCurrency, 2, curPositionItem.IdSourceAdmin, curPositionItem.IsRT, 0).ReturnValue;
                    curPositionItem.UnderlyingLastPcAdmin = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 1, curPositionItem.IdPortCurrency, 2, curPositionItem.IdSourceAdmin, curPositionItem.IsRT, 0).ReturnValue;
                    curPositionItem.UnderlyingClosePc = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DataPosicao, 1, curPositionItem.IdPortCurrency, 2, 0, 0, 0).ReturnValue;
                    curPositionItem.UnderlyingCloseAdmin = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DataPosicao, 1, curPositionItem.IdSecurityCurrency, 2, curPositionItem.IdSourceAdmin, 0, 0).ReturnValue;
                    curPositionItem.UnderlyingClosePcAdmin = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DataPosicao, 1, curPositionItem.IdPortCurrency, 2, curPositionItem.IdSourceAdmin, 0, 0).ReturnValue;

                    curPositionItem.UnderlyingLast = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 1, 0, 2, 0, curPositionItem.IsRT, 0).ReturnValue;
                    curPositionItem.UnderlyingClose = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateClose, 1, 0, 2, 0, 0, 0).ReturnValue;

                    if (curPositionItem.IdAssetClass == 3)
                    {
                        curPositionItem.RateYear = 0;
                        curPositionItem.RatePeriod = 0;
                    }

                    int ForceSide = 1;

                    if (curPositionItem.CurrentPosition < 0) { ForceSide = -1; }

                    ReturnGetVolatility curReturnGetVolatility = GlobalVars.Instance.GetVolatility(curPositionItem, ForceSide, curPositionItem.IdSourceAdmin);
                    curPositionItem.Volatility = curReturnGetVolatility.Volatility;
                    curPositionItem.VolDate = curReturnGetVolatility.VolDate;
                    curPositionItem.VolFlag = curReturnGetVolatility.VolFlag;

                    if (curPositionItem.Volatility != 0 || curPositionItem.Expiration <= DateTime.Now)
                    {
                        if (double.IsNaN(curPositionItem.Volatility))
                            curPositionItem.Volatility = 0;

                        ReturnCalcOptions curReturnCalcOptions = GlobalVars.Instance.GetCalcOptions(curPositionItem.OptionType, curPositionItem.UnderlyingLast, curPositionItem.Strike, curPositionItem.RateYear, curPositionItem.YearFraction, curPositionItem.Volatility, curPositionItem.ContractRatio);
                        curPositionItem.ModelPrice = curReturnCalcOptions.Price;
                        curPositionItem.Delta = curReturnCalcOptions.Delta;
                        curPositionItem.Gamma = curReturnCalcOptions.Gamma;
                        curPositionItem.Vega = curReturnCalcOptions.Vega;
                        curPositionItem.Theta = curReturnCalcOptions.Theta;
                        curPositionItem.Rho = curReturnCalcOptions.Rho;
                    }

                    curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 1, 0, 2, 0, curPositionItem.IsRT, 0);
                    curPositionItem.Last = curReturnGetPrice.ReturnValue;
                    curPositionItem.IdSourceLast = curReturnGetPrice.Source;
                    curPositionItem.FlagLast = curReturnGetPrice.ReturnFlag;
                    curPositionItem.UpdTimeLast = curReturnGetPrice.DateValue;


                    curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 1, 0, 2, curPositionItem.IdSourceAdmin, curPositionItem.IsRT, 0);
                    curPositionItem.LastUcAdmin = curReturnGetPrice.ReturnValue;
                    curPositionItem.IdSourceLastAdmin = curReturnGetPrice.Source;
                    curPositionItem.FlagLastAdmin = curReturnGetPrice.ReturnFlag;
                    curPositionItem.UpdTimeLastAdmin = curReturnGetPrice.DateValue;

                    if (double.IsNaN(curPositionItem.Last))
                    {
                        curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 1, 0, 2, 0, curPositionItem.IsRT, 0);
                        curPositionItem.Last = curReturnGetPrice.ReturnValue;
                        curPositionItem.IdSourceLastAdmin = curReturnGetPrice.Source;
                        curPositionItem.FlagLastAdmin = curReturnGetPrice.ReturnFlag;
                        curPositionItem.UpdTimeLastAdmin = curReturnGetPrice.DateValue;
                    }

                    if (curPositionItem.Theta != 0 && Math.Abs(curPositionItem.Theta * curPositionItem.CurrentPosition / curPositionItem.LotSize / curPositionItem.Nav) > Math.Abs(curPositionItem.Cash / curPositionItem.NavPc))
                    {
                        curPositionItem.ThetaNav = -Math.Abs(curPositionItem.Cash / curPositionItem.NavPc);
                    }
                    else
                    {
                        curPositionItem.ThetaNav = curPositionItem.Theta * curPositionItem.CurrentPosition / curPositionItem.LotSize / curPositionItem.Nav;
                    }
                }
                else
                {
                    curPositionItem.MktCap = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, curPositionItem.DateNow, 20, curPositionItem.IdSecurityCurrency, 2, 0, curPositionItem.IsRT, 0).ReturnValue;
                    curPositionItem.AvVolume6m = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, curPositionItem.DateNow, 19, 0, 2, 0, curPositionItem.IsRT, 0).ReturnValue;

                    curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, curPositionItem.DateNow, 1, 0, 2, 0, curPositionItem.IsRT, 0);
                    curPositionItem.Last = curReturnGetPrice.ReturnValue;
                    curPositionItem.DisplayLast = curReturnGetPrice.DisplayValue;
                    curPositionItem.IdSourceLast = curReturnGetPrice.Source;
                    curPositionItem.FlagLast = curReturnGetPrice.ReturnFlag;
                    curPositionItem.UpdTimeLast = curReturnGetPrice.DateValue;

                    curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, curPositionItem.DateNow, 1, 0, 2, curPositionItem.IdSourceAdmin, curPositionItem.IsRT, 0);
                    curPositionItem.LastUcAdmin = curReturnGetPrice.ReturnValue;
                    curPositionItem.IdSourceLastAdmin = curReturnGetPrice.Source;
                    curPositionItem.FlagLastAdmin = curReturnGetPrice.ReturnFlag;
                    curPositionItem.UpdTimeLastAdmin = curReturnGetPrice.DateValue;

                    curPositionItem.ThetaNav = 0;
                }

                // BID AND ASK

                curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, curPositionItem.DateNow, 9, 0, 0, 0, curPositionItem.IsRT, 0);
                curPositionItem.Bid = curReturnGetPrice.ReturnValue;

                curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurityPrice, curPositionItem.DateNow, 10, 0, 0, 0, curPositionItem.IsRT, 0);
                curPositionItem.Ask = curReturnGetPrice.ReturnValue;
            }
        }

        public void UpdateFowardInfo(PositionItem curPositionItem)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * " +
                        " FROM dbo.Tb725_Fowards A " +
                        " INNER JOIN dbo.Tb003_PortAccounts (nolock)B " +
                        " ON A.Id_Account = B.Id_Account " +
                        " INNER JOIN dbo.FCN001_Securities(" + curPositionItem.IdSecurity + ",'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "')  C " +
                        " ON A.Id_Ticker = C.IdSecurity " +
                        " WHERE Id_Ticker=" + curPositionItem.IdSecurity + " AND B.Id_Portfolio =" + curPositionItem.IdPortfolio + " " +
                        " AND Id_Book=" + curPositionItem.IdBook + " AND Id_Section=" + curPositionItem.IdSection + " ";

                DataTable curTable = curConn.Return_DataTable(StringSQL);
                if (curTable.Rows.Count > 0)
                {
                    DataRow curRow = curTable.Rows[0];

                    curPositionItem.IdForward = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Foward"]);
                    curPositionItem.SpotPrice = (int)LiveDLL.Utils.ParseToDouble(curRow["SpotPrice"]);
                    curPositionItem.FwdExpiration = LiveDLL.Utils.ParseToDateTime(curRow["Expiration"]);
                    curPositionItem.OrigQuantity = (int)LiveDLL.Utils.ParseToDouble(curRow["Quantity"]);

                    curPositionItem.FwdPrice = curConn.Return_Double("SELECT dbo.FCN_Return_Price_FW(" + curPositionItem.IdForward + ",'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "',0)");
                    curPositionItem.FwdAdjPrice = curConn.Return_Double("SELECT dbo.FCN_Return_Price_FW(" + curPositionItem.IdForward + ",'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "',1)");

                    curPositionItem.FwdAdjPl = (
                        curPositionItem.FwdAdjPrice -
                        curConn.Return_Double("SELECT dbo.FCN_Return_Price_FW(" +
                            curPositionItem.IdForward + ",'" +
                            curPositionItem.DateClose.ToString("yyyy-MM-dd") + "',1)")) *
                            -curPositionItem.CurrentPosition *
                            (curPositionItem.OrigQuantity /
                            curPositionItem.AdjFwPosition);


                    curPositionItem.FwValue = (curConn.Return_Double("SELECT dbo.FCN_Return_Price_FW(" + curPositionItem.IdForward + ",'" + curPositionItem.DateClose.ToString("yyyy-MM-dd") + "',0)")) * (curPositionItem.CurrentPosition * (curPositionItem.OrigQuantity / curPositionItem.AdjFwPosition)) / curPositionItem.LotSize;
                }
                //if(int.isnan(curPositionItem.IdForward))
            }
        }

        #endregion

        public double ReturnCurrencyConvert(int CurrentCurrency, int TargetCurrency, DateTime Date)
        {
            int IsIntraday;
            double Currency1;
            double Currency2;
            ReturnGetPrice curReturnGetPrice;

            if (Date == DateTime.Now.Date) { IsIntraday = 1; } else { IsIntraday = 0; }

            curReturnGetPrice = GlobalVars.Instance.GetPrice(TargetCurrency, Date, 1, 0, 1, 0, IsIntraday, 0);
            Currency1 = curReturnGetPrice.ReturnValue;

            curReturnGetPrice = GlobalVars.Instance.GetPrice(CurrentCurrency, Date, 1, 0, 1, 0, IsIntraday, 0);
            Currency2 = curReturnGetPrice.ReturnValue;

            return Currency1 / Currency2;
        }

        public double ifNaN(double OriginalValue, double ReplaceValue)
        {
            if (double.IsNaN(OriginalValue)) return ReplaceValue; else return OriginalValue;
        }

        public object ifnull(object OriginalValue, object ReplaceValue)
        {
            if (OriginalValue != null && OriginalValue != "")
            {
                return OriginalValue;
            }
            else
            {
                return ReplaceValue;
            }
        }

        //Dictionary<GetCurrencyItem, double> GetCurrencyBuffer = new Dictionary<GetCurrencyItem, double>();

        //private class GetCurrencyItem
        //{
        //    public GetCurrencyItem(long _IdCurrencyTickerRef, long _IdCurrencyPortfolioRef, DateTime _DateRef)
        //    {
        //        IdCurrencyTickerRef = _IdCurrencyTickerRef;
        //        IdCurrencyPortfolioRef = _IdCurrencyPortfolioRef;
        //        DateRef = _DateRef;
        //    }

        //    long IdCurrencyTickerRef = 0;
        //    long IdCurrencyPortfolioRef = 0;
        //    DateTime DateRef = new DateTime(1900, 01, 01);
        //}

        //public double GetCurrency(long IdCurrencyTickerRef, long IdCurrencyPortfolioRef, DateTime DateRef)
        //{
        //    GetCurrencyItem tempGetCurrencyItem = new GetCurrencyItem(IdCurrencyTickerRef, IdCurrencyPortfolioRef, DateRef);
        //    if (GetCurrencyBuffer.ContainsKey(tempGetCurrencyItem))
        //    {
        //        return GetCurrencyBuffer[tempGetCurrencyItem];
        //    }
        //    else
        //    {
        //        using (newNestConn curConn = new newNestConn())
        //        {
        //            double tempValue = curConn.Return_Double("SELECT dbo.[FCN_Convert_Moedas](" + IdCurrencyTickerRef + "," + IdCurrencyPortfolioRef + ",'" + DateRef.ToString("yyyy-MM-dd") + "')");
        //            GetCurrencyItem addGetCurrencyItem = new GetCurrencyItem(IdCurrencyTickerRef, IdCurrencyPortfolioRef, DateRef);
        //            GetCurrencyBuffer.Add(addGetCurrencyItem, tempValue);
        //            return tempValue;
        //        }
        //    }
        //}

        //public void SetCashRowsToZero()
        //{
        //    // Change Cash positions to ZERO value

        //    foreach (PositionItem curPositionItem in AllPositions)
        //    {

        //    }
        //}

    }
}