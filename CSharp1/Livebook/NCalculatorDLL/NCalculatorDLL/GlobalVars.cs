using System;
using System.Collections.Generic;
using LiveDLL;
using System.Data;
using System.Data.SqlClient;

namespace NCalculatorDLL
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();
        public System.Diagnostics.Stopwatch MasterClock = new System.Diagnostics.Stopwatch();

        public DataTable UpdatePosTable = new DataTable("UpdatePosTable");

        public int RunCounter = 0;

        //public StreamWriter fs = new StreamWriter(@"C:\temp\StrongOpen_Dados.txt");

        public static GlobalVars Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalVars();
                    }
                    return instance;
                }
            }
        }

        public GlobalVars()
        {
            InitializePositionTable();
        }

        private void InitializePositionTable()
        {
            UpdatePosTable.Columns.Add("Id Position", typeof(int));
            UpdatePosTable.Columns.Add("Id Portfolio", typeof(int));
            UpdatePosTable.Columns.Add("Portfolio", typeof(string));
            UpdatePosTable.Columns.Add("Id Ticker", typeof(int));
            UpdatePosTable.Columns.Add("Ticker", typeof(string));
            UpdatePosTable.Columns.Add("Description", typeof(string));
            UpdatePosTable.Columns.Add("Id Price Table", typeof(int));
            UpdatePosTable.Columns.Add("Price Table", typeof(string));
            UpdatePosTable.Columns.Add("ZId Strategy", typeof(int));
            UpdatePosTable.Columns.Add("ZStrategy", typeof(string));
            UpdatePosTable.Columns.Add("ZId Sub Strategy", typeof(int));
            UpdatePosTable.Columns.Add("ZSub Strategy", typeof(string));
            UpdatePosTable.Columns.Add("Last Position", typeof(DateTime));
            UpdatePosTable.Columns.Add("Close_Date", typeof(DateTime));
            UpdatePosTable.Columns.Add("Date Now", typeof(DateTime));
            UpdatePosTable.Columns.Add("Position", typeof(float));
            UpdatePosTable.Columns.Add("Lot Size", typeof(float));
            UpdatePosTable.Columns.Add("Cost Close", typeof(float));
            UpdatePosTable.Columns.Add("Cost Close pC", typeof(float));
            UpdatePosTable.Columns.Add("Last", typeof(float));
            UpdatePosTable.Columns.Add("Last pC", typeof(float));
            UpdatePosTable.Columns.Add("Close", typeof(float));
            UpdatePosTable.Columns.Add("Close pC", typeof(float));
            UpdatePosTable.Columns.Add("NAV", typeof(float));
            UpdatePosTable.Columns.Add("NAV pC", typeof(float));
            UpdatePosTable.Columns.Add("Cash", typeof(float));
            UpdatePosTable.Columns.Add("Cash/NAV", typeof(float));
            UpdatePosTable.Columns.Add("Brokerage", typeof(float));
            UpdatePosTable.Columns.Add("Delta Cash", typeof(float));
            UpdatePosTable.Columns.Add("Delta/NAV", typeof(float));
            UpdatePosTable.Columns.Add("Security Currency", typeof(string));
            UpdatePosTable.Columns.Add("Id Currency Ticker", typeof(int));
            UpdatePosTable.Columns.Add("Delta", typeof(float));
            UpdatePosTable.Columns.Add("Instrument", typeof(string));
            UpdatePosTable.Columns.Add("Asset Class", typeof(string));
            UpdatePosTable.Columns.Add("Sub Industry", typeof(string));
            UpdatePosTable.Columns.Add("Industry", typeof(string));
            UpdatePosTable.Columns.Add("Industry Group", typeof(string));
            UpdatePosTable.Columns.Add("Sector", typeof(string));
            UpdatePosTable.Columns.Add("Underlying Country", typeof(string));
            UpdatePosTable.Columns.Add("Nest Sector", typeof(string));
            UpdatePosTable.Columns.Add("Portfolio Currency", typeof(int));
            UpdatePosTable.Columns.Add("Asset uC", typeof(float));
            UpdatePosTable.Columns.Add("Asset pC", typeof(float));
            UpdatePosTable.Columns.Add("Asset P/L pC", typeof(float));
            UpdatePosTable.Columns.Add("Currency Chg", typeof(float));
            UpdatePosTable.Columns.Add("Currency P/L", typeof(float));
            UpdatePosTable.Columns.Add("Total P/L", typeof(float));
            UpdatePosTable.Columns.Add("Spot USD", typeof(float));
            UpdatePosTable.Columns.Add("Spot", typeof(float));
            UpdatePosTable.Columns.Add("Expiration", typeof(DateTime));
            UpdatePosTable.Columns.Add("P/L %", typeof(float));
            UpdatePosTable.Columns.Add("Gross", typeof(float));
            UpdatePosTable.Columns.Add("Long", typeof(float));
            UpdatePosTable.Columns.Add("Short", typeof(float));
            UpdatePosTable.Columns.Add("Contribution pC", typeof(float));
            UpdatePosTable.Columns.Add("Id Underlying", typeof(int));
            UpdatePosTable.Columns.Add("Underlying", typeof(string));
            UpdatePosTable.Columns.Add("Underlying Acount", typeof(int));
            UpdatePosTable.Columns.Add("Notional", typeof(float));
            UpdatePosTable.Columns.Add("Notional Close", typeof(float));
            UpdatePosTable.Columns.Add("Closed_PL", typeof(float));
            UpdatePosTable.Columns.Add("Display Last ", typeof(float));
            UpdatePosTable.Columns.Add("Display Last pC", typeof(float));
            UpdatePosTable.Columns.Add("Display Cost Close", typeof(float));
            UpdatePosTable.Columns.Add("Display Cost Close pC", typeof(float));
            UpdatePosTable.Columns.Add("BRL", typeof(float));
            UpdatePosTable.Columns.Add("BRL/NAV", typeof(float));
            UpdatePosTable.Columns.Add("Long Delta", typeof(float));
            UpdatePosTable.Columns.Add("Short Delta", typeof(float));
            UpdatePosTable.Columns.Add("Gross Delta", typeof(float));
            UpdatePosTable.Columns.Add("Last Calc", typeof(DateTime));
            UpdatePosTable.Columns.Add("Last Cost Close Calc", typeof(DateTime));
            UpdatePosTable.Columns.Add("Realized", typeof(float));
            UpdatePosTable.Columns.Add("Asset P/L uC", typeof(float));
            UpdatePosTable.Columns.Add("Contribution uC", typeof(float));
            UpdatePosTable.Columns.Add("Delta Quantity", typeof(float));
            UpdatePosTable.Columns.Add("Volatility", typeof(float));
            UpdatePosTable.Columns.Add("Vol Flag", typeof(string));
            UpdatePosTable.Columns.Add("Vol Date", typeof(DateTime));
            UpdatePosTable.Columns.Add("Strike", typeof(float));
            UpdatePosTable.Columns.Add("Rate Year", typeof(float));
            UpdatePosTable.Columns.Add("Rate Period", typeof(float));
            UpdatePosTable.Columns.Add("Days to Expiration", typeof(int));
            UpdatePosTable.Columns.Add("Time to Expiration", typeof(float));
            UpdatePosTable.Columns.Add("Underlying Last", typeof(float));
            UpdatePosTable.Columns.Add("Gamma", typeof(float));
            UpdatePosTable.Columns.Add("Vega", typeof(float));
            UpdatePosTable.Columns.Add("Theta", typeof(float));
            UpdatePosTable.Columns.Add("Rho", typeof(float));
            UpdatePosTable.Columns.Add("Cash Premium", typeof(float));
            UpdatePosTable.Columns.Add("Calc Error Flag", typeof(int));
            UpdatePosTable.Columns.Add("Model Price", typeof(float));
            UpdatePosTable.Columns.Add("Gamma Quantity", typeof(float));
            UpdatePosTable.Columns.Add("Gamma Cash", typeof(float));
            UpdatePosTable.Columns.Add("Gamma/NAV", typeof(float));
            UpdatePosTable.Columns.Add("Display Close", typeof(float));
            UpdatePosTable.Columns.Add("Display Close pC", typeof(float));
            UpdatePosTable.Columns.Add("Dif Contrib", typeof(float));
            UpdatePosTable.Columns.Add("Price Date", typeof(DateTime));
            UpdatePosTable.Columns.Add("Id Instrument", typeof(int));
            UpdatePosTable.Columns.Add("Id Asset Class", typeof(int));
            UpdatePosTable.Columns.Add("Option Type", typeof(int));
            UpdatePosTable.Columns.Add("Initial Position", typeof(float));
            UpdatePosTable.Columns.Add("Quantity Bought", typeof(float));
            UpdatePosTable.Columns.Add("Quantity Sold", typeof(float));
            UpdatePosTable.Columns.Add("Spot USD Close", typeof(float));
            UpdatePosTable.Columns.Add("Spot Close", typeof(float));
            UpdatePosTable.Columns.Add("Cash uC", typeof(float));
            UpdatePosTable.Columns.Add("Last pC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Close pC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Cost Close pC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Total P/L Admin", typeof(float));
            UpdatePosTable.Columns.Add("Realized Admin", typeof(float));
            UpdatePosTable.Columns.Add("Asset P/L pC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Currency P/L Admin", typeof(float));
            UpdatePosTable.Columns.Add("Contribution pC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Last Admin", typeof(float));
            UpdatePosTable.Columns.Add("Close Admin", typeof(float));
            UpdatePosTable.Columns.Add("Cost Close Admin", typeof(float));
            UpdatePosTable.Columns.Add("Id Administrator", typeof(int));
            UpdatePosTable.Columns.Add("Id Base Underlying", typeof(int));
            UpdatePosTable.Columns.Add("Base Underlying", typeof(string));
            UpdatePosTable.Columns.Add("Id Base Underlying Currency", typeof(int));
            UpdatePosTable.Columns.Add("Base Underlying Currency", typeof(string));
            UpdatePosTable.Columns.Add("Bid", typeof(float));
            UpdatePosTable.Columns.Add("Ask", typeof(float));
            UpdatePosTable.Columns.Add("% to Bid/Ask", typeof(float));
            UpdatePosTable.Columns.Add("Id Source Last", typeof(int));
            UpdatePosTable.Columns.Add("Source Last", typeof(string));
            UpdatePosTable.Columns.Add("Flag Last", typeof(string));
            UpdatePosTable.Columns.Add("Id Source Last Admin", typeof(int));
            UpdatePosTable.Columns.Add("Source Last Admin", typeof(string));
            UpdatePosTable.Columns.Add("Flag Last Admin", typeof(string));
            UpdatePosTable.Columns.Add("Id Source Close", typeof(int));
            UpdatePosTable.Columns.Add("Source Close", typeof(string));
            UpdatePosTable.Columns.Add("Flag Close Admin", typeof(string));
            UpdatePosTable.Columns.Add("UpdTime Last", typeof(DateTime));
            UpdatePosTable.Columns.Add("UpdTime last Admin", typeof(DateTime));
            UpdatePosTable.Columns.Add("Asset uC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Theta/NAV", typeof(float));
            UpdatePosTable.Columns.Add("Id Source Close Admin", typeof(int));
            UpdatePosTable.Columns.Add("Source Close Admin", typeof(string));
            UpdatePosTable.Columns.Add("Prev Cash uC", typeof(float));
            UpdatePosTable.Columns.Add("Prev Cash uC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Current NAV pC", typeof(float));
            UpdatePosTable.Columns.Add("Option Intrinsic", typeof(float));
            UpdatePosTable.Columns.Add("Option TV", typeof(float));
            UpdatePosTable.Columns.Add("Option Intrinsic Cash pC", typeof(float));
            UpdatePosTable.Columns.Add("Option TV Cash pC", typeof(float));
            UpdatePosTable.Columns.Add("Cash uC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Underlying Liquidity", typeof(int));
            UpdatePosTable.Columns.Add("Days to Liquidity", typeof(float));
            UpdatePosTable.Columns.Add("Duration", typeof(float));
            UpdatePosTable.Columns.Add("Duration Date", typeof(DateTime));
            UpdatePosTable.Columns.Add("10Y Equiv DNAV", typeof(float));
            UpdatePosTable.Columns.Add("Asset PL uC Admin", typeof(float));
            UpdatePosTable.Columns.Add("Delta/Book", typeof(float));
            UpdatePosTable.Columns.Add("Contribution pC Book", typeof(float));
            UpdatePosTable.Columns.Add("Long Book", typeof(float));
            UpdatePosTable.Columns.Add("Short Book", typeof(float));
            UpdatePosTable.Columns.Add("Gross Book", typeof(float));
            UpdatePosTable.Columns.Add("CX", typeof(float));
            UpdatePosTable.Columns.Add("CXBOOK", typeof(float));
            UpdatePosTable.Columns.Add("Id Book", typeof(int));
            UpdatePosTable.Columns.Add("Book", typeof(string));
            UpdatePosTable.Columns.Add("Id Sub Portfolio", typeof(int));
            UpdatePosTable.Columns.Add("Sub Portfolio", typeof(string));
            UpdatePosTable.Columns.Add("New Id Strategy", typeof(int));
            UpdatePosTable.Columns.Add("New Strategy", typeof(string));
            UpdatePosTable.Columns.Add("New Id Sub Strategy", typeof(int));
            UpdatePosTable.Columns.Add("New Sub Strategy", typeof(string));
            UpdatePosTable.Columns.Add("Id Section", typeof(int));
            UpdatePosTable.Columns.Add("Section", typeof(string));
            UpdatePosTable.Columns.Add("Gross uC", typeof(float));
            UpdatePosTable.Columns.Add("Position All Portfolios", typeof(float));
            UpdatePosTable.Columns.Add("Position Ex-FIA", typeof(float));
            UpdatePosTable.Columns.Add("Book NAV", typeof(float));
            UpdatePosTable.Columns.Add("Market Cap", typeof(float));
            UpdatePosTable.Columns.Add("6m Av Volume", typeof(float));
            UpdatePosTable.Columns.Add("Asset Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Currency Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Asset Book Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Currency Book Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Prev Asset P/L", typeof(float));
            UpdatePosTable.Columns.Add("Prev Asset P/L Admin", typeof(float));
            UpdatePosTable.Columns.Add("Cash FLow", typeof(float));
            UpdatePosTable.Columns.Add("Trade Flow", typeof(float));
            UpdatePosTable.Columns.Add("Id Account", typeof(int));
            UpdatePosTable.Columns.Add("Id Ticker Cash", typeof(int));
            UpdatePosTable.Columns.Add("Strategy %", typeof(float));
            UpdatePosTable.Columns.Add("Flag Calc Cost", typeof(int));
            UpdatePosTable.Columns.Add("Flag Calc Last", typeof(int));
            UpdatePosTable.Columns.Add("Loaned Out", typeof(float));
            UpdatePosTable.Columns.Add("Loaned In", typeof(float));
            UpdatePosTable.Columns.Add("Loan Cost", typeof(float));
            UpdatePosTable.Columns.Add("Foward Cost", typeof(float));
            UpdatePosTable.Columns.Add("Loaned Rate In", typeof(float));
            UpdatePosTable.Columns.Add("Loaned Rate Out", typeof(float));
            UpdatePosTable.Columns.Add("Position D-3", typeof(float));
            UpdatePosTable.Columns.Add("Net Quantity Loan", typeof(float));
            UpdatePosTable.Columns.Add("Loan Rate In CBLC", typeof(float));
            UpdatePosTable.Columns.Add("Loan Rate Out CBLC", typeof(float));
            UpdatePosTable.Columns.Add("Loan AVG Rate", typeof(float));
            UpdatePosTable.Columns.Add("Cash Admin", typeof(float));
            UpdatePosTable.Columns.Add("Loan MKT Total", typeof(float));
            UpdatePosTable.Columns.Add("Loan Potential Gain", typeof(float));
            UpdatePosTable.Columns.Add("Loan Potential Book Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Loan Marginal Gain", typeof(float));
            UpdatePosTable.Columns.Add("Loan Marginal Book Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Loan Book Contribution", typeof(float));
            UpdatePosTable.Columns.Add("Side", typeof(string));
            UpdatePosTable.Columns.Add("Id Forward", typeof(float));
            UpdatePosTable.Columns.Add("Dividends", typeof(float));
            UpdatePosTable.Columns.Add("ReportSector", typeof(string));
            UpdatePosTable.Columns.Add("ReportSectorPort", typeof(string));
            UpdatePosTable.Columns.Add("Last USD", typeof(float));
            UpdatePosTable.Columns.Add("Close USD", typeof(float));
            UpdatePosTable.Columns.Add("Delta Side", typeof(string));
            UpdatePosTable.Columns.Add("Adjusted Fw Position", typeof(float));
            UpdatePosTable.Columns.Add("FWValue", typeof(float));
            UpdatePosTable.Columns.Add("Exchange Ticker", typeof(string));
            UpdatePosTable.Columns.Add("Adjust Cash", typeof(float));
            UpdatePosTable.Columns.Add("Contribution pC Adjusted", typeof(float));
            UpdatePosTable.Columns.Add("Adjust Brokerage", typeof(float));
            UpdatePosTable.Columns.Add("FWValue2", typeof(float));
            UpdatePosTable.Columns.Add("TradeCost", typeof(float));
            UpdatePosTable.Columns.Add("FWD Adj P/L", typeof(float));
            UpdatePosTable.Columns.Add("FWD Adj Brokerage", typeof(float));
            UpdatePosTable.Columns.Add("FWD Adj Price", typeof(float));
            UpdatePosTable.Columns.Add("FWD Price", typeof(float));
            UpdatePosTable.Columns.Add("Adjust Currency", typeof(float));
            UpdatePosTable.Columns.Add("NAVPrevious", typeof(float));
            UpdatePosTable.Columns.Add("NAVpCPrevious", typeof(float));

        }

        public int getCashId(int IdCurrency)
        {
            switch (IdCurrency)
            {
                case 900: return 1844;
                case 1042: return 5791;
                case 929: return 31057;
                case 933: return 76743;
                default: return 1844;
            }
        }

        public TimeCounter GetPriceTimer = new TimeCounter();
        public TimeCounter GetRateTimer = new TimeCounter();
        public TimeCounter GetPUTimer = new TimeCounter();
        public TimeCounter LoadBuffersTimer = new TimeCounter();
        public TimeCounter GetVolTimer = new TimeCounter();
        public TimeCounter CreateUpdatePosTimer = new TimeCounter();

        public DateTime QuantityTablesChange = new DateTime();
        public DateTime RTPriceTablesChange = new DateTime();
        public DateTime HistPriceTablesChange = new DateTime();
        public DateTime BaseTablesChange = new DateTime();
        public DateTime SecuritiesTablesChange = new DateTime();
        public DateTime PortfolioTablesChange = new DateTime();
        public DateTime AccountTablesChange = new DateTime();

        public DateTime LastRecalcAll = new DateTime();
        public DateTime LastMarketDataUpdate = new DateTime();
        public DateTime LastQuantityUpdate = new DateTime();
        public TimeSpan LastRecalcAllTimeTaken = new TimeSpan(00, 00, 00);
        public TimeSpan LastRTQuantityTimeTaken = new TimeSpan(00, 00, 00);

        public double AllFieldsUpdateMS = 1;
        public double RTFieldsUpdateMS = 1;
        public double QTFieldsUpdateMS = 1;

        public bool MarketDataConnected = false;

        public int MarketDataQueueCounter;
        public int SQLInsertQueueCounter;


        //==========================================================              BUFFERS          ==================================================================================
        //===========================================================================================================================================================================

        List<AccountDataItem> AccountDataList = new List<AccountDataItem>();
        List<DateTime> LoadedDaysList = new List<DateTime>();

        public Queue<string> StatusQueue = new Queue<string>();

        public Dictionary<string, PortDataItem> PortDataList = new Dictionary<string, PortDataItem>();
        public Dictionary<string, SymbolDataItem> SymbolDataList = new Dictionary<string, SymbolDataItem>();
        public Dictionary<string, LoanDataItem> LoanDataList = new Dictionary<string, LoanDataItem>();
        public Dictionary<int, int> PortCurrencyList = new Dictionary<int, int>();
        public Dictionary<InputGetRate, ReturnGetRate> GetRateBuffer = new Dictionary<InputGetRate, ReturnGetRate>();
        public Dictionary<InputGetPU, ReturnGetPU> GetPUBuffer = new Dictionary<InputGetPU, ReturnGetPU>();
        public Dictionary<InputGetPrice, ReturnGetPrice> GetPriceBuffer = new Dictionary<InputGetPrice, ReturnGetPrice>();
        public Dictionary<InputGetVolatility, ReturnGetVolatility> GetVolatilityBuffer = new Dictionary<InputGetVolatility, ReturnGetVolatility>();
        public Dictionary<int, string> BookList = new Dictionary<int, string>();
        public Dictionary<int, SectionDataItem> SectionList = new Dictionary<int, SectionDataItem>();
        public Dictionary<int, string> SourceList = new Dictionary<int, string>();

        public int
                    SymbolData_Hit,
                    SymbolData_Miss,
                    LoanData_Hit,
                    LoanData_Miss,
                    AccountData_Hit,
                    AccountData_Miss,
                    GetRate_Hit,
                    GetRate_Miss,
                    GetPrice_Hit,
                    GetPrice_Miss,
                    GetVol_Hit,
                    GetVol_Miss,
                    GetPU_Hit,
                    GetPU_Miss;

        public int UpdBatchSize = 10;
        public int InputBatchSize = 10;


        public void ClearBuffers()
        {
            lock (BookList) { BookList.Clear(); }
            lock (SourceList) { SourceList.Clear(); }
            lock (SectionList) { SectionList.Clear(); }

            lock (PortDataList) { PortDataList.Clear(); }
            lock (SymbolDataList) { SymbolDataList.Clear(); }
            lock (LoanDataList) { LoanDataList.Clear(); }

            lock (AccountDataList) { AccountDataList.Clear(); }
            lock (PortCurrencyList) { PortCurrencyList.Clear(); }
            lock (GetRateBuffer) { GetRateBuffer.Clear(); }
            lock (GetPUBuffer) { GetPUBuffer.Clear(); }
            lock (GetPriceBuffer) { GetPriceBuffer.Clear(); }
            lock (GetVolatilityBuffer) { GetVolatilityBuffer.Clear(); }

            lock (LoadedDaysList) { LoadedDaysList.Clear(); }
        }

        public void ClearPriceBuffers()
        {
            lock (GetRateBuffer) { GetRateBuffer.Clear(); }
            lock (GetPUBuffer) { GetPUBuffer.Clear(); }
            lock (GetPriceBuffer) { GetPriceBuffer.Clear(); }
            lock (GetVolatilityBuffer) { GetVolatilityBuffer.Clear(); }
        }

        public void LoadFixedBuffers()
        {
            GlobalVars.Instance.LoadBuffersTimer.StartCounting();

            GlobalVars.Instance.LoadBookSection();
            GlobalVars.Instance.LoadAccounts();
            GlobalVars.Instance.LoadPortCurrency();

            GlobalVars.Instance.LoadBuffersTimer.End();
        }

        public void LoadDayBuffers(DateTime PositionDate, DateTime CloseDate)
        {
            //if (!LoadedDaysList.Contains(PositionDate))
            //{
            GlobalVars.Instance.LoadBuffersTimer.StartCounting();

            GlobalVars.Instance.LoadSymbolData(PositionDate);
            GlobalVars.Instance.LoadLoanData(PositionDate);
            GlobalVars.Instance.LoadPortData(PositionDate);

            lock (LoadedDaysList) { LoadedDaysList.Add(PositionDate); }

            GlobalVars.Instance.LoadBuffersTimer.End();
            //}
        }

        public void LoadPortData(DateTime PositionDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = " DECLARE @Date date " +
                                    " SET @Date = '" + PositionDate.ToString("yyyy-MM-dd") + "' " +
                                    " SELECT " +
                                    "	A.Id_Portfolio, " +
                                    "	Port_Currency, " +
                                    "	Id_Administrator, " +
                                    "	a.Port_Name, " +
                                    "	B.Valor_PL TodayNAV, " +
                                    "	B.Valor_PL * dbo.FCN_Convert_Moedas(Port_Currency, Port_Currency, B.Data_PL) AS TodayNAVpC, " +
                                    "	D.Valor_PL YesterdayNAV, " +
                                    "	D.Valor_PL * dbo.FCN_Convert_Moedas(Port_Currency, Port_Currency, B.Data_PL) AS YesterdayNAVpC " +
                                    " FROM Tb002_Portfolios A " +
                                    " INNER JOIN Tb025_Valor_PL B	ON A.Id_Portfolio = B.Id_Portfolio " +
                                    " INNER JOIN Tb025_Valor_PL D	ON A.Id_Portfolio = D.Id_Portfolio " +
                                    " INNER JOIN ( " +
                                    "			SELECT	Id_Portfolio, MAX(Data_PL) Data_PL  " +
                                    "			FROM Tb025_Valor_PL B  " +
                                    "			WHERE B.Data_PL <= @Date  " +
                                    "			GROUP BY Id_Portfolio " +
                                    "			) Today  " +
                                    " ON A.Id_Portfolio = Today.Id_Portfolio AND B.Data_PL = Today.Data_PL " +
                                    " INNER JOIN ( " +
                                    "			SELECT	Id_Portfolio, MAX(Data_PL) Data_PL  " +
                                    "			FROM Tb025_Valor_PL B  " +
                                    "			WHERE B.Data_PL < @Date  " +
                                    "			GROUP BY Id_Portfolio " +
                                    "			) Yesterday  " +
                                    " ON A.Id_Portfolio = Yesterday.Id_Portfolio AND D.Data_PL = Yesterday.Data_PL ";


                DataTable curTable = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in curTable.Rows)
                {
                    int IdPortfolio = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]);
                    PortDataItem curPortDataItem = new PortDataItem();
                    curPortDataItem.IdAdministrator = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Administrator"]);
                    curPortDataItem.IdPortCurrency = (int)LiveDLL.Utils.ParseToDouble(curRow["Port_Currency"]);
                    curPortDataItem.NAVToday = LiveDLL.Utils.ParseToDouble(curRow["TodayNAV"]);
                    curPortDataItem.NAVpCToday = LiveDLL.Utils.ParseToDouble(curRow["TodayNAVpC"]);

                    curPortDataItem.NAVYesterday = LiveDLL.Utils.ParseToDouble(curRow["YesterdayNAV"]);
                    curPortDataItem.NAVpCYesterday = LiveDLL.Utils.ParseToDouble(curRow["YesterdayNAVpC"]);

                    curPortDataItem.Portfolio = curRow["Port_Name"].ToString();

                    string tempHash = (IdPortfolio + PositionDate.ToString("yyyyMMdd"));

                    lock (PortDataList)
                    {
                        if (PortDataList.ContainsKey(tempHash))
                        {
                            PortDataList.Remove(tempHash);
                        }
                        PortDataList.Add(tempHash, curPortDataItem);
                    }
                }
            }
        }

        public void LoadSymbolData(DateTime RefDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLExpresion = " SELECT A.*, B.Price_Table_Name, " +
                        " E.Simbolo as Moeda,Instrumento,Classe_Ativo, " +
                        " Sub_Industry,Industry,Industry_Group,Sector,Sector_Nest,id_geografia,Geografia, " +
                        " I.NestTicker as Ativo_Objeto " +
                        " from Tb001_Securities A  " +
                        " inner join Tb024_Price_Table B " +
                        " on A.IdPriceTable = B.Id_Price_Table " +
                        " inner join dbo.Vw_Moedas E " +
                        " on A.IdCurrency = E.Id_Moeda " +
                        " inner join Tb029_Instrumentos F " +
                        " on A.IdInstrument = F.Id_Instrumento " +
                        " inner join Tb028_Classe_Ativo G " +
                        " on A.IdAssetClass = G.Id_Classe_Ativo " +
                        " inner join VW_Classificacao_Gics H " +
                        " on A.IdSecurity = H.IdSecurity " +
                        " left join Tb001_Securities I " +
                        " on A.IdUnderlying = I.IdSecurity " +
                        " WHERE a.IdSecurity IN  " +
                        "( " +
                        " SELECT [Id Ticker] FROM NESTDB.dbo.Tb000_Historical_Positions  WHERE [Date Now]>= '" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "' GROUP BY [Id Ticker] " +
                        " UNION ALL 	 " +
                        " SELECT [Id Ticker] FROM NESTRT.dbo.Tb000_Posicao_Atual GROUP BY [Id Ticker] " +
                        ")";


                DataTable result = curConn.Return_DataTable(SQLExpresion);
                if (result.Rows.Count != 0)
                {
                    foreach (DataRow curRow in result.Rows)
                    {
                        SymbolDataItem curSymbolData = new SymbolDataItem();
                        curSymbolData.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["IdSecurity"]);

                        curSymbolData.IdInstrument = (int)LiveDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                        curSymbolData.IdPrimaryExchange = (int)LiveDLL.Utils.ParseToDouble(curRow["IdPrimaryExchange"]);
                        curSymbolData.LotSize = LiveDLL.Utils.ParseToDouble(curRow["RoundLot"]);
                        curSymbolData.ExchangeTicker = (curRow["ExchangeTicker"]).ToString();
                        curSymbolData.IdPriceTable = (int)LiveDLL.Utils.ParseToDouble(curRow["IdPriceTable"]);
                        curSymbolData.PriceTable = (curRow["Price_Table_Name"]).ToString();
                        curSymbolData.Description = curRow["SecName"].ToString();
                        curSymbolData.IdSecurityCurrency = (int)LiveDLL.Utils.ParseToDouble(curRow["IdCurrency"]);
                        curSymbolData.RTUpdateSource = (int)LiveDLL.Utils.ParseToDouble(curRow["RTUpdateSource"]);
                        curSymbolData.HistUpdateSource = (int)LiveDLL.Utils.ParseToDouble(curRow["HistUpdateSource"]);
                        curSymbolData.SecurityCurrency = (curRow["Moeda"]).ToString();
                        curSymbolData.Instrument = (curRow["Instrumento"]).ToString();
                        curSymbolData.AssetClass = (curRow["Classe_Ativo"]).ToString();
                        curSymbolData.SubIndustry = (curRow["Sub_Industry"]).ToString();
                        curSymbolData.Industry = (curRow["Industry"]).ToString();
                        curSymbolData.IndustryGroup = (curRow["Industry_Group"]).ToString();
                        curSymbolData.Sector = (curRow["Sector"]).ToString();
                        curSymbolData.UnderlyingCountry = (curRow["Geografia"]).ToString();
                        curSymbolData.NestSector = (curRow["Sector_Nest"]).ToString();
                        curSymbolData.Expiration = LiveDLL.Utils.ParseToDateTime(curRow["Expiration"]);
                        curSymbolData.IdUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["IdUnderlying"]);
                        curSymbolData.Underlying = (curRow["Ativo_Objeto"]).ToString();
                        curSymbolData.NestTicker = curRow["NestTicker"].ToString();
                        curSymbolData.BloombergTicker = curRow["BloombergTicker"].ToString();
                        curSymbolData.ReutersTicker = curRow["ReutersTicker"].ToString();
                        curSymbolData.ImagineTicker = curRow["ImagineTicker"].ToString();
                        curSymbolData.ItauTicker = curRow["Itauticker"].ToString();
                        curSymbolData.MellonTicker = curRow["Mellonticker"].ToString();
                        curSymbolData.AdminTicker = curRow["Adminticker"].ToString();
                        curSymbolData.IdAssetClass = (int)LiveDLL.Utils.ParseToDouble(curRow["IdAssetClass"]);
                        curSymbolData.Strike = LiveDLL.Utils.ParseToDouble(curRow["Strike"]);
                        curSymbolData.OptionType = (int)LiveDLL.Utils.ParseToDouble(curRow["OptionType"]);
                        curSymbolData.ContractRatio = LiveDLL.Utils.ParseToDouble(curRow["ContractRatio"]);
                        curSymbolData.PriceFromUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["PriceFromUnderlying"]);

                        string tempHash = ((curRow["IdSecurity"]) + RefDate.ToString("yyyyMMdd"));

                        lock (SymbolDataList)
                        {
                            if (SymbolDataList.ContainsKey(tempHash))
                            {
                                SymbolDataList.Remove(tempHash);
                            }
                            SymbolDataList.Add(tempHash, curSymbolData);
                        }
                    }
                }
            }
        }

        public void LoadLoanData(DateTime RefDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLExpresion = " SELECT COALESCE(A.IdSecurity, B.IdSecurity, C.IdSecurity) AS IdSecurity, LoanRateOut, LoanRateIn, LoanMarketSize " +
                                        " FROM " +
                                        " (SELECT IdSecurity, SrValue AS LoanMarketSize FROM dbo.Tb050_Preco_Acoes_Onshore WHERE SrType=130 AND SrDate='" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "') A " +
                                        " FULL OUTER JOIN " +
                                        " (SELECT IdSecurity, SrValue AS LoanRateOut FROM dbo.Tb050_Preco_Acoes_Onshore WHERE SrType=132 AND SrDate='" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "') B " +
                                        " ON A.IdSecurity=B.IdSecurity " +
                                        " FULL OUTER JOIN " +
                                        " (SELECT IdSecurity, SrValue AS LoanRateIn FROM dbo.Tb050_Preco_Acoes_Onshore WHERE SrType=133 AND SrDate='" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "') C " +
                                        " ON A.IdSecurity=C.IdSecurity";

                DataTable result = curConn.Return_DataTable(SQLExpresion);
                if (result.Rows.Count != 0)
                {
                    foreach (DataRow curRow in result.Rows)
                    {
                        LoanDataItem curLoanDataItem = new LoanDataItem();
                        curLoanDataItem.LoanRateIn = LiveDLL.Utils.ParseToDouble(curRow["LoanRateIn"]);
                        curLoanDataItem.LoanRateOut = LiveDLL.Utils.ParseToDouble(curRow["LoanRateOut"]);
                        curLoanDataItem.LoanMarketSize = (int)LiveDLL.Utils.ParseToDouble(curRow["LoanMarketSize"]);
                        string tempHash = ((curRow["IdSecurity"]) + RefDate.ToString("yyyyMMdd"));

                        lock (LoanDataList)
                        {
                            if (LoanDataList.ContainsKey(tempHash))
                            {
                                LoanDataList.Remove(tempHash);
                            }
                            LoanDataList.Add(tempHash, curLoanDataItem);
                        }
                    }
                }
            }
        }

        public void LoadPortCurrency()
        {
            lock (PortCurrencyList)
            {
                PortCurrencyList.Clear();

                using (newNestConn curConn = new newNestConn())
                {
                    DateTime iniTime = DateTime.Now;
                    string StringSQL = "SELECT * FROM NESTDB.dbo.Tb002_Portfolios WHERE Id_Portfolio>0";

                    DataTable curTable = curConn.Return_DataTable(StringSQL);

                    foreach (DataRow curRow in curTable.Rows)
                    {
                        PortCurrencyList.Add((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]), (int)LiveDLL.Utils.ParseToDouble(curRow["Port_Currency"]));
                    }
                }
            }
        }

        public void LoadAccounts()
        {
            lock (AccountDataList)
            {
                AccountDataList.Clear();

                using (newNestConn curConn = new newNestConn())
                {
                    DateTime iniTime = DateTime.Now;
                    string StringSQL = "SELECT * FROM NESTDB.dbo.VW_PortAccounts";

                    DataTable curTable = curConn.Return_DataTable(StringSQL);

                    foreach (DataRow curRow in curTable.Rows)
                    {
                        AccountDataItem curAccountData = new AccountDataItem();
                        curAccountData.IdAccount = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Account"]);
                        curAccountData.IdPortfolio = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]);
                        curAccountData.IdPortType = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Port_Type"]);

                        AccountDataList.Add(curAccountData);
                    }
                }
            }
        }

        public void LoadBookSection()
        {
            using (newNestConn curConn = new newNestConn())
            {
                DataTable dtBooks = curConn.Return_DataTable("SELECT Id_Book, Book FROM NESTDB.dbo.Tb400_Books");

                foreach (DataRow curRow in dtBooks.Rows)
                {
                    lock (BookList)
                    {
                        if (!BookList.ContainsKey((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Book"])))
                        {
                            BookList.Add((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Book"]), curRow["Book"].ToString());
                        }
                    }
                }

                DataTable dtSections = curConn.Return_DataTable("SELECT * FROM NESTDB.dbo.VW_Strategies");

                foreach (DataRow curRow in dtSections.Rows)
                {
                    SectionDataItem curSectionDataItem = new SectionDataItem();
                    curSectionDataItem.IdSubPortfolio = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Sub_Portfolio"]);
                    curSectionDataItem.SubPortfolio = (curRow["Sub_Portfolio"]).ToString();
                    curSectionDataItem.IdStrategy = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Strategy"]);
                    curSectionDataItem.Strategy = (curRow["Strategy"]).ToString();
                    curSectionDataItem.IdSubStrategy = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Sub_Strategy"]);
                    curSectionDataItem.SubStrategy = (curRow["Sub_Strategy"]).ToString();
                    curSectionDataItem.IdSection = (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Section"]);
                    curSectionDataItem.Section = (curRow["Section"]).ToString();

                    lock (SectionList)
                    {
                        if (!SectionList.ContainsKey((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Section"])))
                        {
                            SectionList.Add((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Section"]), curSectionDataItem);
                        }
                    }
                }

                DataTable dtSources = curConn.Return_DataTable("SELECT Id_Sistemas_Informacoes, Descricao FROM NESTDB.dbo.Tb102_Sistemas_Informacoes");

                lock (SourceList)
                {
                    if (!SourceList.ContainsKey(-1))
                    { SourceList.Add(-1, "NA"); }

                    if (!SourceList.ContainsKey(-2))
                    { SourceList.Add(-2, "NA"); }

                    foreach (DataRow curRow in dtSources.Rows)
                    {
                        if (!SourceList.ContainsKey((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Sistemas_Informacoes"])))
                        { SourceList.Add((int)LiveDLL.Utils.ParseToDouble(curRow["Id_Sistemas_Informacoes"]), curRow["Descricao"].ToString()); }
                    }
                }
            }
        }

        public SymbolDataItem GetSymbolData(int IdSecurity, DateTime RefDate)
        {
            SymbolDataItem tempVal;
            DataTable result;
            newNestConn curConn = new newNestConn();

            string tempHash = (IdSecurity + RefDate.ToString("yyyyMMdd"));

            if (SymbolDataList.TryGetValue(tempHash, out tempVal))
            {
                SymbolData_Hit++;
                return tempVal;
            }
            else
            {
                SymbolData_Miss++;

                //string SQLExpresion = " SELECT A.*, B.Price_Table_Name, " +
                //                        " E.Simbolo as Moeda,Instrumento,Classe_Ativo, " +
                //                        " Sub_Industry,Industry,Industry_Group,Sector,Sector_Nest,id_geografia,Geografia, " +
                //                        " I.NestTicker as Ativo_Objeto " +
                //    //" from FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") + "') A  " +ANTES ERA ASSIM
                //                        " from Tb001_Securities A  " +
                //                        " inner join Tb024_Price_Table B " +
                //                        " on A.IdPriceTable = B.Id_Price_Table " +
                //                        " inner join dbo.Vw_Moedas E " +
                //                        " on A.IdCurrency = E.Id_Moeda " +
                //                        " inner join Tb029_Instrumentos F " +
                //                        " on A.IdInstrument = F.Id_Instrumento " +
                //                        " inner join Tb028_Classe_Ativo G " +
                //                        " on A.IdAssetClass = G.Id_Classe_Ativo " +
                //                        " inner join VW_Classificacao_Gics H " +
                //                        " on A.IdSecurity = H.IdSecurity " +
                //                        " left join Tb001_Securities I " +
                //    //" left join FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") + "') I " + ANTES ERA ASSIM
                //                        " on A.IdUnderlying = I.IdSecurity " +
                //    //" WHERE a.IdSecurity IN (SELECT [Id Ticker] FROM NESTDB.dbo.Tb000_Historical_Positions_NEW WHERE [Date Now]>='" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "' GROUP BY [Id Ticker]) "; ANTES ERA ASSIM
                //                        " WHERE a.IdSecurity IN (SELECT [Id Ticker] FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]>='" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "' AND [Id Ticker] = " + IdSecurity + " GROUP BY [Id Ticker]) order by a.IdSecurity  ";


                //string SQLExpresion = " SELECT I.*, B.Price_Table_Name, " +
                //                        " E.Simbolo as Moeda,Instrumento,Classe_Ativo, " +
                //                        " Sub_Industry,Industry,Industry_Group,Sector,Sector_Nest,id_geografia,Geografia, " +
                //                        " I.NestTicker as Ativo_Objeto " +
                //                        " from FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") + "') A  " +
                //                        " inner join Tb024_Price_Table B " +
                //                        " on A.IdPriceTable = B.Id_Price_Table " +
                //                        " inner join dbo.Vw_Moedas E " +
                //                        " on A.IdCurrency = E.Id_Moeda " +
                //                        " inner join Tb029_Instrumentos F " +
                //                        " on A.IdInstrument = F.Id_Instrumento " +
                //                        " inner join Tb028_Classe_Ativo G " +
                //                        " on A.IdAssetClass = G.Id_Classe_Ativo " +
                //                        " inner join VW_Classificacao_Gics H " +
                //                        " on A.IdSecurity = H.IdSecurity " +
                //                        " left join FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") + "') I " +
                //                        " on A.IdUnderlying = I.IdSecurity WHERE a.IdSecurity = '" + IdSecurity + "' order by a.IdSecurity ";

                string SQLExpresion = " SELECT A.*, B.Price_Table_Name, " +
                                        " E.Simbolo as Moeda,Instrumento,Classe_Ativo, " +
                                        " Sub_Industry,Industry,Industry_Group,Sector,Sector_Nest,id_geografia,Geografia, " +
                                        " I.NestTicker as Ativo_Objeto " +
                    //" from FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") + "') A  " + ANTES ERA ASSIM
                                        " from Tb001_Securities A  " +
                                        " inner join Tb024_Price_Table B " +
                                        " on A.IdPriceTable = B.Id_Price_Table " +
                                        " inner join dbo.Vw_Moedas E " +
                                        " on A.IdCurrency = E.Id_Moeda " +
                                        " inner join Tb029_Instrumentos F " +
                                        " on A.IdInstrument = F.Id_Instrumento " +
                                        " inner join Tb028_Classe_Ativo G " +
                                        " on A.IdAssetClass = G.Id_Classe_Ativo " +
                                        " inner join VW_Classificacao_Gics H " +
                                        " on A.IdSecurity = H.IdSecurity " +
                    //" left join Tb001_Securities I " + ANTES ERA ASSIM
                                        " left join FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") + "') I " +
                                        " on A.IdUnderlying = I.IdSecurity WHERE a.IdSecurity = '" + IdSecurity + "' order by a.IdSecurity ";

                result = curConn.Return_DataTable(SQLExpresion);

                if (result.Rows.Count != 0)
                {
                    DataRow curRow = result.Rows[0];

                    SymbolDataItem curSymbolData = new SymbolDataItem();
                    curSymbolData.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                    curSymbolData.IdInstrument = (int)LiveDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                    curSymbolData.IdPrimaryExchange = (int)LiveDLL.Utils.ParseToDouble(curRow["IdPrimaryExchange"]);
                    curSymbolData.LotSize = LiveDLL.Utils.ParseToDouble(curRow["RoundLot"]);
                    curSymbolData.ExchangeTicker = (curRow["ExchangeTicker"]).ToString();

                    curSymbolData.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                    curSymbolData.IdInstrument = (int)LiveDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                    curSymbolData.IdPrimaryExchange = (int)LiveDLL.Utils.ParseToDouble(curRow["IdPrimaryExchange"]);
                    curSymbolData.ExchangeTicker = (curRow["ExchangeTicker"]).ToString();
                    curSymbolData.IdPriceTable = (int)LiveDLL.Utils.ParseToDouble(curRow["IdPriceTable"]);
                    curSymbolData.PriceTable = (curRow["Price_Table_Name"]).ToString();
                    curSymbolData.IdSecurityCurrency = (int)LiveDLL.Utils.ParseToDouble(curRow["IdCurrency"]);
                    curSymbolData.SecurityCurrency = (curRow["Moeda"]).ToString();
                    curSymbolData.Instrument = (curRow["Instrumento"]).ToString();
                    curSymbolData.AssetClass = (curRow["Classe_Ativo"]).ToString();
                    curSymbolData.SubIndustry = (curRow["Sub_Industry"]).ToString();
                    curSymbolData.Industry = (curRow["Industry"]).ToString();
                    curSymbolData.IndustryGroup = (curRow["Industry_Group"]).ToString();
                    curSymbolData.Sector = (curRow["Sector"]).ToString();
                    curSymbolData.UnderlyingCountry = (curRow["Geografia"]).ToString();
                    curSymbolData.Description = curRow["SecName"].ToString();
                    curSymbolData.NestSector = (curRow["Sector_Nest"]).ToString();
                    curSymbolData.Expiration = LiveDLL.Utils.ParseToDateTime(curRow["Expiration"]);
                    curSymbolData.IdUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["IdUnderlying"]);
                    curSymbolData.RTUpdateSource = (int)LiveDLL.Utils.ParseToDouble(curRow["RTUpdateSource"]);
                    curSymbolData.HistUpdateSource = (int)LiveDLL.Utils.ParseToDouble(curRow["HistUpdateSource"]);
                    curSymbolData.Underlying = (curRow["Ativo_Objeto"]).ToString();
                    curSymbolData.IdInstrument = (int)LiveDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                    curSymbolData.IdUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["IdUnderlying"]);
                    curSymbolData.LotSize = LiveDLL.Utils.ParseToDouble(curRow["RoundLot"]);
                    curSymbolData.IdUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["IdUnderlying"]);
                    curSymbolData.Expiration = LiveDLL.Utils.ParseToDateTime(curRow["Expiration"]);

                    curSymbolData.NestTicker = curRow["NestTicker"].ToString();
                    curSymbolData.BloombergTicker = curRow["BloombergTicker"].ToString();
                    curSymbolData.ReutersTicker = curRow["ReutersTicker"].ToString();
                    curSymbolData.ImagineTicker = curRow["ImagineTicker"].ToString();
                    curSymbolData.ItauTicker = curRow["Itauticker"].ToString();
                    curSymbolData.MellonTicker = curRow["Mellonticker"].ToString();
                    curSymbolData.AdminTicker = curRow["Adminticker"].ToString();


                    curSymbolData.IdAssetClass = (int)LiveDLL.Utils.ParseToDouble(curRow["IdAssetClass"]);
                    curSymbolData.Strike = LiveDLL.Utils.ParseToDouble(curRow["Strike"]);
                    curSymbolData.OptionType = (int)LiveDLL.Utils.ParseToDouble(curRow["OptionType"]);
                    curSymbolData.ContractRatio = LiveDLL.Utils.ParseToDouble(curRow["ContractRatio"]);
                    curSymbolData.PriceFromUnderlying = (int)LiveDLL.Utils.ParseToDouble(curRow["PriceFromUnderlying"]);

                    tempHash = ((curRow["IdSecurity"]) + RefDate.ToString("yyyyMMdd"));

                    lock (SymbolDataList) { SymbolDataList.Add(tempHash, curSymbolData); }
                    return curSymbolData;
                }
            }
            return null;
        }

        public PortDataItem GetPortData(int IdPortfolio, DateTime RefDate)
        {
            PortDataItem tempVal;

            string tempHash = (IdPortfolio + RefDate.ToString("yyyyMMdd"));

            if (PortDataList.TryGetValue(tempHash, out tempVal))
            {
                return tempVal;
            }
            else
            {
                return null;
            }
        }

        public LoanDataItem GetLoanData(int IdSecurity, DateTime RefDate)
        {
            LoanDataItem tempVal;
            DataTable result;
            newNestConn curConn = new newNestConn();

            string tempHash = (IdSecurity + RefDate.ToString("yyyyMMdd"));

            if (LoanDataList.TryGetValue(tempHash, out tempVal))
            {
                return tempVal;
            }
            else
            {
                string SQLExpresion = " SELECT COALESCE(A.IdSecurity, B.IdSecurity, C.IdSecurity) AS IdSecurity, LoanRateOut, LoanRateIn, LoanMarketSize " +
                     " FROM " +
                     " (SELECT IdSecurity, SrValue AS LoanMarketSize FROM dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + IdSecurity + " AND SrType=130 AND SrDate='" + RefDate.ToString("yyyy-MM-dd") + "') A " +
                     " FULL OUTER JOIN " +
                     " (SELECT IdSecurity, SrValue AS LoanRateOut FROM dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + IdSecurity + " AND SrType=132 AND SrDate='" + RefDate.ToString("yyyy-MM-dd") + "') B " +
                     " ON A.IdSecurity=B.IdSecurity " +
                     " FULL OUTER JOIN " +
                     " (SELECT IdSecurity, SrValue AS LoanRateIn FROM dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + IdSecurity + " AND SrType=133 AND SrDate='" + RefDate.ToString("yyyy-MM-dd") + "') C " +
                     " ON A.IdSecurity=C.IdSecurity";

                result = curConn.Return_DataTable(SQLExpresion);

                if (result.Rows.Count != 0)
                {
                    LoanDataItem curLoanDataItem = new LoanDataItem();
                    curLoanDataItem.LoanRateIn = LiveDLL.Utils.ParseToDouble(result.Rows[0]["LoanRateIn"]);
                    curLoanDataItem.LoanRateOut = LiveDLL.Utils.ParseToDouble(result.Rows[0]["LoanRateOut"]);
                    curLoanDataItem.LoanMarketSize = (int)LiveDLL.Utils.ParseToDouble(result.Rows[0]["LoanMarketSize"]);
                    lock (LoanDataList) { LoanDataList.Add(tempHash, curLoanDataItem); }
                    return curLoanDataItem;
                }
            }
            return null;
        }

        public int GetIdInstrument(int IdSecurity, DateTime RefDate)
        {
            SymbolDataItem tempVal;

            if (RefDate == new DateTime(1900, 01, 01))
            {
            }

            tempVal = GetSymbolData(IdSecurity, RefDate);

            if (tempVal != null)
            {
                return tempVal.IdInstrument;
            }

            return 0;
        }

        public double GetLotSize(int IdSecurity, DateTime RefDate)
        {
            SymbolDataItem tempVal;

            tempVal = GetSymbolData(IdSecurity, RefDate);

            if (tempVal != null)
            {
                return tempVal.LotSize;
            }

            return 0;
        }

        public int GetIdPrimaryExchange(int IdSecurity, DateTime RefDate)
        {
            SymbolDataItem tempVal;

            tempVal = GetSymbolData(IdSecurity, RefDate);

            if (tempVal != null)
            {
                return tempVal.IdPrimaryExchange;
            }

            return 0;
        }

        public int GetPortCurrency(int IdPortfolio)
        {
            int IdCurrency = 0;
            if (PortCurrencyList.TryGetValue(IdPortfolio, out IdCurrency))
            {
                return IdCurrency;
            }
            else
            {
                return 0;
            }
        }

        public int GetPort(int IdAccount, int IdPortType)
        {
            lock (AccountDataList)
            {
                foreach (AccountDataItem curAccountData in AccountDataList)
                {
                    if (curAccountData.IdAccount == IdAccount && curAccountData.IdPortType == IdPortType)
                    {
                        return curAccountData.IdPortfolio;
                    }
                }
            }

            return 0;
        }

        public List<AccountDataItem> GetPorts(int IdAccount)
        {
            List<AccountDataItem> ReturnValues = new List<AccountDataItem>();

            lock (AccountDataList)
            {
                foreach (AccountDataItem curAccountData in AccountDataList)
                {
                    if (curAccountData.IdAccount == IdAccount)
                    {
                        ReturnValues.Add(curAccountData);
                    }
                }
            }
            return ReturnValues;
        }

        public ReturnGetPrice GetPrice(int IdTicker, DateTime Date, int PriceType, int IdCurrency, int ConvertType, int IdSource, int IsRT, int GetRealTime)
        {
            GlobalVars.Instance.GetPriceTimer.StartCounting();
            newNestConn curConn = new newNestConn();

            InputGetPrice curInputGetPrice = new InputGetPrice();
            curInputGetPrice.IdTicker = IdTicker;
            curInputGetPrice.Date = Date;
            curInputGetPrice.PriceType = PriceType;
            curInputGetPrice.IdCurrency = IdCurrency;
            curInputGetPrice.ConvertType = ConvertType;
            curInputGetPrice.IdSource = IdSource;
            curInputGetPrice.IsTR = IsRT;
            curInputGetPrice.GetRealTime = GetRealTime;

            lock (GetPriceBuffer)
            {
                if (IsRT == 0 && GetPriceBuffer.ContainsKey(curInputGetPrice))
                {
                    GetPrice_Hit++;
                    GlobalVars.Instance.GetPriceTimer.End();
                    return GetPriceBuffer[curInputGetPrice];
                }
                else
                {
                    GetPrice_Miss++;

                    string SqlCommand;
                    SqlCommand = "SELECT * FROM [dbo].[FCN_GET_PRICE](" + IdTicker + ",'" + Date.ToString("yyyy-MM-dd") + "'," + PriceType + "," + IdCurrency + "," + ConvertType + "," + IdSource + "," + IsRT + "," + GetRealTime + ")"; // verificar 

                    DataTable curTable = curConn.Return_DataTable(SqlCommand);

                    if (curTable.Rows.Count > 0)
                    {
                        DataRow dtRow = curTable.Rows[0];

                        ReturnGetPrice curReturnGetPrice = new ReturnGetPrice();
                        curReturnGetPrice.ReturnValue = LiveDLL.Utils.ParseToDouble(dtRow["Return_Value"]);
                        curReturnGetPrice.DisplayValue = LiveDLL.Utils.ParseToDouble(dtRow["Display_Value"]);
                        curReturnGetPrice.Source = (int)LiveDLL.Utils.ParseToDouble(dtRow["Source"]);
                        curReturnGetPrice.ReturnFlag = (dtRow["Return_Flag"]).ToString();
                        curReturnGetPrice.DateValue = LiveDLL.Utils.ParseToDateTime(dtRow["Date_Value"]);
                        curReturnGetPrice.IdSecurity = (int)LiveDLL.Utils.ParseToDouble(dtRow["IdSecurity"]);

                        if (!GetPriceBuffer.ContainsKey(curInputGetPrice))
                        {
                            GetPriceBuffer.Add(curInputGetPrice, curReturnGetPrice);
                        }
                        GlobalVars.Instance.GetPriceTimer.End();

                        return curReturnGetPrice;
                    }
                    else
                    {
                        ReturnGetPrice curReturnGetPrice = new ReturnGetPrice();
                        return curReturnGetPrice;
                    }
                }
            }
        }

        public ReturnCalcOptions GetCalcOptions(int OptionType, double UnderlyingLast, double Strike, double RateYear, double YearFraction, double Volatility, double ContractRatio)
        {
            ReturnCalcOptions curReturnCalcOptions = new ReturnCalcOptions();

            using (newNestConn curConn = new newNestConn())
            {
                string SqlCommand = "SELECT * FROM dbo.FCN_Calc_Options (" + OptionType + "," + UnderlyingLast.ToString().Replace(",", ".") + "," + Strike.ToString().Replace(",", ".") + "," + RateYear.ToString().Replace(",", ".") + "," + YearFraction.ToString().Replace(",", ".") + "," + Volatility.ToString().Replace(",", ".") + "," + ContractRatio.ToString().Replace(",", ".") + ")";
                foreach (DataRow dtRow in curConn.Return_DataTable(SqlCommand).Rows)
                {
                    curReturnCalcOptions.Price = LiveDLL.Utils.ParseToDouble(dtRow["Price"]);
                    curReturnCalcOptions.Delta = LiveDLL.Utils.ParseToDouble(dtRow["Delta"]);
                    curReturnCalcOptions.Gamma = LiveDLL.Utils.ParseToDouble(dtRow["Gamma"]);
                    curReturnCalcOptions.Vega = LiveDLL.Utils.ParseToDouble(dtRow["Vega"]);
                    curReturnCalcOptions.Theta = LiveDLL.Utils.ParseToDouble(dtRow["Theta"]);
                    curReturnCalcOptions.Rho = LiveDLL.Utils.ParseToDouble(dtRow["Rho"]);

                    break;
                }
            }

            return curReturnCalcOptions;
        }

        public ReturnGetRate GetRate(DateTime DateNow, DateTime Expiration, int IdCurrencyTicker)
        {
            GlobalVars.Instance.GetRateTimer.StartCounting();

            InputGetRate curInputGetRate = new InputGetRate();
            curInputGetRate.DateNow = DateNow;
            curInputGetRate.Expiration = Expiration;
            curInputGetRate.IdCurrencyTicker = IdCurrencyTicker;

            lock (GetRateBuffer)
            {
                if (GetRateBuffer.ContainsKey(curInputGetRate))
                {
                    GetRate_Hit++;
                    GlobalVars.Instance.GetRateTimer.End();
                    return GetRateBuffer[curInputGetRate];
                }
                else
                {
                    GetRate_Miss++;
                    ReturnGetRate curReturnGetRate = new ReturnGetRate();

                    using (newNestConn curConn = new newNestConn())
                    {
                        string StringSQL = "SELECT * FROM FCN_CALC_RATE('" + DateNow.ToString("yyyy-MM-dd") + "','" + Expiration.ToString("yyyy-MM-dd") + "'," + IdCurrencyTicker + ")";
                        DataRow dtRow = curConn.Return_DataTable(StringSQL).Rows[0];

                        curReturnGetRate.RateYear = LiveDLL.Utils.ParseToDouble(dtRow["Rate_Year"]);
                        curReturnGetRate.RatePeriod = LiveDLL.Utils.ParseToDouble(dtRow["Rate_Period"]);
                        curReturnGetRate.BusDays = (int)LiveDLL.Utils.ParseToDouble(dtRow["Bus_Days"]);
                        curReturnGetRate.YearFraction = LiveDLL.Utils.ParseToDouble(dtRow["Year_Fraction"]);

                        GetRateBuffer.Add(curInputGetRate, curReturnGetRate);

                        GlobalVars.Instance.GetRateTimer.End();
                    }
                    return curReturnGetRate;
                }
            }
        }

        public ReturnGetPU GetPU(int IdSecurity, double LastRate, DateTime Date)
        {
            GlobalVars.Instance.GetPUTimer.StartCounting();

            InputGetPU curInputGetPU = new InputGetPU();
            curInputGetPU.IdSecurity = IdSecurity;
            curInputGetPU.LastTax = LastRate;

            lock (GetPUBuffer)
            {
                if (GetPUBuffer.ContainsKey(curInputGetPU))
                {
                    GetPU_Hit++;
                    GlobalVars.Instance.GetPUTimer.End();
                    return GetPUBuffer[curInputGetPU];
                }
                else
                {
                    GetPU_Miss++;
                    ReturnGetPU curReturnGetPU = new ReturnGetPU();

                    using (newNestConn curConn = new newNestConn())
                    {
                        string StringSQL = "SELECT dbo.FCN_Calcula_PU(" + IdSecurity + ", " + (LastRate / 100).ToString().Replace(',', '.') + ", '" + Date.ToString("yyyy-MM-dd") + "', '1900-01-01') AS 'PU';";
                        DataRow dtRow = curConn.Return_DataTable(StringSQL).Rows[0];


                        curReturnGetPU.PU = LiveDLL.Utils.ParseToDouble(dtRow["PU"]);

                        GetPUBuffer.Add(curInputGetPU, curReturnGetPU);

                        GlobalVars.Instance.GetPUTimer.End();
                    }
                    return curReturnGetPU;
                }
            }
        }

        public ReturnGetdNBrl GetdNBrl(int IdSecurity, double Cash, double DeltaCash)
        {
            ReturnGetdNBrl curReturnGetdNBrl = new ReturnGetdNBrl();

            using (newNestConn curConn = new newNestConn())
            {
                curReturnGetdNBrl.ReturnValue = curConn.Return_Double("SELECT dbo.[FCN_GETD_NBRL](" + IdSecurity + "," + Cash.ToString().Replace(",", ".") + "," + DeltaCash.ToString().Replace(",", ".") + ")");
            }

            if (double.IsNaN(curReturnGetdNBrl.ReturnValue))
            {
                curReturnGetdNBrl.ReturnValue = 0;
            }

            return curReturnGetdNBrl;

        }

        public ReturnGetDBrl GetDBrl(int IdSecurity, double Cash, double DeltaCash)
        {
            ReturnGetDBrl curReturnGetDBrl = new ReturnGetDBrl();

            using (newNestConn curConn = new newNestConn())
            {
                curReturnGetDBrl.ReturnValue = curConn.Return_Double("SELECT dbo.[FCN_GETD_BRL](" + IdSecurity + "," + Cash.ToString().Replace(",", ".") + "," + DeltaCash.ToString().Replace(",", ".") + ")");
            }

            if (double.IsNaN(curReturnGetDBrl.ReturnValue))
            {
                curReturnGetDBrl.ReturnValue = 0;
            }

            return curReturnGetDBrl;

        }

        public ReturnGetVolatility GetVolatility(PositionItem curPositionItem, int ForceSide, int IdSource)
        {
            GlobalVars.Instance.GetVolTimer.StartCounting();
            newNestConn curConn = new newNestConn();

            InputGetVolatility curInputGetVolatility = new InputGetVolatility();
            curInputGetVolatility.DateNow = curPositionItem.DateNow;
            curInputGetVolatility.IdSecurity = curPositionItem.IdSecurity;
            curInputGetVolatility.ForceSide = ForceSide;
            curInputGetVolatility.IdSource = IdSource;

            lock (GetVolatilityBuffer)
            {
                if (GetVolatilityBuffer.ContainsKey(curInputGetVolatility) && curPositionItem.IsRT != 1)
                {
                    GetVol_Hit++;
                    GlobalVars.Instance.GetVolTimer.End();
                    return GetVolatilityBuffer[curInputGetVolatility];
                }

                else
                {
                    GetVol_Miss++;

                    DateTime VolDate = new DateTime(1900, 01, 01);
                    DateTime TempDate = new DateTime(1900, 01, 01);
                    DateTime YestDate = curConn.Return_DateTime("Select dbo.FCN_Ndateadd('du', -1,'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "',99," + curPositionItem.IdSecurity + ")");
                    int IsRT = 0;
                    double ReturnValue = 0;
                    double TempPrice = 0;
                    string VolFlag = "";

                    if (curPositionItem.DateNow == DateTime.Now.Date) { IsRT = 1; }
                    else { IsRT = 0; }

                    ReturnGetPrice curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 40, 0, 0, IdSource, 0, 0);
                    ReturnValue = curReturnGetPrice.ReturnValue;
                    TempDate = curReturnGetPrice.DateValue.Date;

                    if (ReturnValue == 0)
                    {
                        curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdUnderlying, curPositionItem.DateNow, 1, 0, 2, IdSource, IsRT, 0);
                        double S = curReturnGetPrice.ReturnValue;

                        ReturnGetRate curGetRate = GlobalVars.Instance.GetRate(curPositionItem.DateNow, curPositionItem.Expiration, curPositionItem.IdSecurityCurrency);
                        double R = curGetRate.RateYear;
                        double T = curGetRate.YearFraction;

                        if (ForceSide == 1 || ForceSide == 0)
                        {
                            curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 9, 0, 0, IdSource, IsRT, 0);
                            double TempBid = curReturnGetPrice.ReturnValue;
                            TempDate = curReturnGetPrice.DateValue.Date;

                            if (TempBid > 0 &&
                                (TempDate == new DateTime(1900, 01, 01) ||
                                TempDate == DateTime.Now.Date))
                            {
                                TempPrice = TempBid;
                                VolDate = TempDate;
                                VolFlag = "BID";
                            }
                            else
                            {
                                curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, YestDate, 1, 0, 0, IdSource, 0, 0);
                                TempPrice = curReturnGetPrice.ReturnValue;
                                TempDate = curReturnGetPrice.DateValue.Date;

                                if (TempPrice != 0)
                                {
                                    ReturnValue = TempPrice;
                                    VolDate = YestDate;
                                    VolFlag = "YEST CLOSE";
                                }
                                else
                                {
                                    TempPrice = 0.01;
                                    VolFlag = "BID 1";
                                }
                            }
                        }
                        else
                        {
                            curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 10, 0, 0, IdSource, IsRT, 0);
                            double TempAsk = curReturnGetPrice.ReturnValue;
                            TempDate = curReturnGetPrice.DateValue.Date;

                            if (TempAsk > 0 &&
                            (TempDate == new DateTime(1900, 01, 01) ||
                            TempDate == DateTime.Now.Date))
                            {
                                TempPrice = TempAsk;
                                VolDate = TempDate;
                                VolFlag = "ASK";
                            }
                            else
                            {
                                curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, YestDate, 1, 0, 0, IdSource, 0, 0);
                                TempPrice = curReturnGetPrice.ReturnValue;
                                TempDate = curReturnGetPrice.DateValue.Date;

                                if (TempPrice != 0)
                                {
                                    ReturnValue = TempPrice;
                                    VolDate = YestDate;
                                    VolFlag = "YEST CLOSE";
                                }
                                else
                                {
                                    curReturnGetPrice = GlobalVars.Instance.GetPrice(curPositionItem.IdSecurity, curPositionItem.DateNow, 1, 0, 0, IdSource, 1, 0);
                                    TempPrice = curReturnGetPrice.ReturnValue;
                                    TempDate = curReturnGetPrice.DateValue.Date;
                                    VolFlag = "LAST";
                                }
                            }
                        }
                        if (TempPrice > 0)
                        {
                            ReturnValue = curConn.Return_Double("SELECT dbo.FCN_Calc_Implied_Vol(" + curPositionItem.OptionType + ", " + S.ToString().Replace(",", ".") + ", " + curPositionItem.Strike.ToString().Replace(",", ".") + ", " + R.ToString().Replace(",", ".") + ", " + T.ToString().Replace(",", ".") + ", " + TempPrice.ToString().Replace(",", ".") + "," + curPositionItem.ContractRatio.ToString().Replace(",", ".") + ") ");
                        }
                        else
                        {
                            ReturnValue = 0;
                        }
                    }
                    else
                    {
                        VolDate = TempDate;
                        VolFlag = "YEST VOL";
                    }

                    ReturnGetVolatility curReturnGetVolatility = new ReturnGetVolatility();
                    curReturnGetVolatility.Volatility = ReturnValue;
                    curReturnGetVolatility.VolDate = VolDate;
                    curReturnGetVolatility.VolFlag = VolFlag;

                    if (!GetVolatilityBuffer.ContainsKey(curInputGetVolatility))
                        GetVolatilityBuffer.Add(curInputGetVolatility, curReturnGetVolatility);

                    GlobalVars.Instance.GetVolTimer.End();

                    return curReturnGetVolatility;
                }
            }
        }

        public class SymbolDataItem
        {
            public int IdSecurity = 0;
            public int IdInstrument = 0;
            public int IdPrimaryExchange = 0;
            public string ExchangeTicker = "";
            public double LotSize = 0;
            public string Description = "";
            public int IdPriceTable = 0;
            public string PriceTable = "";
            public string SecurityCurrency = "";
            public int IdSecurityCurrency = 0;
            public string Instrument = "";
            public string AssetClass = "";
            public string SubIndustry = "";
            public string Industry = "";
            public string IndustryGroup = "";
            public string Sector = "";
            public string UnderlyingCountry = "";
            public string NestSector = "";
            public DateTime Expiration = new DateTime(1900, 01, 01);
            public int IdUnderlying = 0;
            public string Underlying = "";
            public string Book = "";
            public string Section = "";
            public int IdSubPortfolio = 0;
            public string SubPortfolio = "";
            public int IdStrategy = 0;
            public string Strategy = "";
            public int IdSubStrategy = 0;
            public int PortfolioCurrency = 0;
            public int IdAssetClass = 0;
            public double Strike = 0;
            public int OptionType = 0;
            public double ContractRatio = 0;
            public int PriceFromUnderlying = 0;
            public string NestTicker = "",
                          BloombergTicker = "",
                          ReutersTicker = "",
                          ImagineTicker = "",
                          BtgTicker = "",
                          ItauTicker = "",
                          MellonTicker = "",
                          AdminTicker = "";
            public int RTUpdateSource = 0,
                       HistUpdateSource = 0;
        }
        public class AccountDataItem
        {
            public int IdAccount = 0;
            public int IdPortType = 0;
            public int IdPortfolio = 0;
        }
        public class PortDataItem
        {
            public double NAVToday = 0;
            public double NAVpCToday = 0;
            public double NAVYesterday = 0;
            public double NAVpCYesterday = 0;
            public int IdPortCurrency = 0;
            public int IdAdministrator = 0;
            public string Portfolio = "";
        }
        public class LoanDataItem
        {
            public double LoanRateIn = 0;
            public double LoanRateOut = 0;
            public int LoanMarketSize = 0;
        }
        public class SectionDataItem
        {
            public string Section = "";
            public string Strategy = "";
            public string SubPortfolio = "";
            public string SubStrategy = "";
            public int IdSection = 0;
            public int IdStrategy = 0;
            public int IdSubStrategy = 0;
            public int IdSubPortfolio = 0;

        }
        public class TimeCounter
        {
            DateTime tempStart;
            public TimeSpan ElapsedTime = new TimeSpan(0, 0, 0);

            public void StartCounting()
            {
                tempStart = DateTime.Now;
            }

            public void End()
            {
                ElapsedTime += DateTime.Now.Subtract(tempStart);
            }

            public void Reset()
            {
                ElapsedTime = new TimeSpan(0, 0, 0);
            }
        }
    }
}

public class InputGetPU : IEquatable<InputGetPU>
{
    public int IdSecurity = 0;
    public double LastTax = 0;

    public bool Equals(InputGetPU obj)
    {
        if (this.IdSecurity == obj.IdSecurity && this.LastTax == obj.LastTax)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return (IdSecurity.ToString() + LastTax.ToString()).GetHashCode();
    }

}


public class InputGetRate : IEquatable<InputGetRate>
{
    public DateTime DateNow = new DateTime(1900, 01, 01);
    public DateTime Expiration = new DateTime(1900, 01, 01);
    public int IdCurrencyTicker = 0;

    public bool Equals(InputGetRate obj)
    {
        if (this.DateNow == obj.DateNow && this.Expiration == obj.Expiration && this.IdCurrencyTicker == obj.IdCurrencyTicker)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return (DateNow.ToString("MMdd") + Expiration.ToString("yyyyMMdd") + IdCurrencyTicker).GetHashCode();

    }

}

public class InputGetPrice : IEquatable<InputGetPrice>
{
    public int IdTicker;
    public DateTime Date;
    public int PriceType;
    public int IdCurrency;
    public int ConvertType;
    public int IdSource;
    public int IsTR;
    public int GetRealTime;

    public bool Equals(InputGetPrice obj)
    {
        if (this.IdTicker == obj.IdTicker &&
            this.Date == obj.Date &&
            this.PriceType == obj.PriceType &&
            this.IdCurrency == obj.IdCurrency &&
            this.ConvertType == obj.ConvertType &&
            this.IdSource == obj.IdSource &&
            this.IsTR == obj.IsTR &&
            this.GetRealTime == obj.GetRealTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return (IdTicker + Date.ToString("yyyyMMdd") + PriceType + IdCurrency + ConvertType + IdSource + IsTR + GetRealTime).GetHashCode();
    }
}

public class InputGetVolatility : IEquatable<InputGetVolatility>
{
    public int IdSecurity;
    public DateTime DateNow;
    public int ForceSide;
    public int IdSource;

    public bool Equals(InputGetVolatility obj)
    {
        if (this.IdSecurity == obj.IdSecurity &&
            this.DateNow == obj.DateNow &&
            this.ForceSide == obj.ForceSide &&
            this.IdSource == obj.IdSource)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return (IdSecurity + DateNow.ToString("MMdd") + ForceSide + IdSource).GetHashCode();
    }

}

public class ReturnGetPrice
{
    public double ReturnValue = 0;
    public double DisplayValue = 0;
    public int Source = 0;
    public int IdSecurity = 0;
    public string ReturnFlag = "";
    public DateTime DateValue = new DateTime(1900, 01, 01);

}

public class ReturnGetRate
{
    public double RateYear = 0;
    public double RatePeriod = 0;
    public int BusDays = 0;
    public double YearFraction = 0;

}

public class ReturnGetPU
{
    public double PU;
}

public class ReturnGetdNBrl
{
    public double ReturnValue;
}

public class ReturnGetDBrl
{
    public double ReturnValue;
}

public class ReturnCalcOptions
{
    public double Price = 0;
    public double Delta = 0;
    public double Gamma = 0;
    public double Vega = 0;
    public double Theta = 0;
    public double Rho = 0;
}

public class ReturnGetVolatility
{
    public double Volatility = 0;
    public DateTime VolDate;
    public string VolFlag = "";
}





