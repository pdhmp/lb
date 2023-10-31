using System;
using System.Collections.Generic;
using NestDLL;
using NCommonTypes;
using NestSymConn;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using NCalculatorDLL;

namespace LBRTCalc
{
    public class RTCalcRunner
    {
        private NDistConn curNDistConn;
        private newNestConn curConn = new newNestConn();

        Thread RecalcAllThread;
        Thread UpdateQuantitiesThread;
        Thread MarketDataProcessThread;
        Thread SQLInsertProcessThread;

        public bool BloombergBackup = false;
        public bool ProcessSQLInsert = false;
        public bool FlagNewPositionFound = false;

        public bool RecalcAllRunning = false;
        public bool StartRunning = false;
        public bool Started = false;
        public bool Initialized = false;
        public bool ContinueProcessing = false;

        public bool RefreshSlowRunning = false;
        public bool RefreshFastRunning = false;
        public bool RefreshQntRunning = false;

        public EventHandler OnStarted;

        public Dictionary<int, RealTimeCalc> CalcPortfolios = new Dictionary<int, RealTimeCalc>();
        private SortedDictionary<string, LBMarketDataItem> SubscribeList = new SortedDictionary<string, LBMarketDataItem>();
        private Dictionary<string, List<PositionItem>> SymbolIndex = new Dictionary<string, List<PositionItem>>();
        public Dictionary<int, PositionItem> DBPositions = new Dictionary<int, PositionItem>();
        private Queue<MarketUpdateList> MarketDataQueue = new Queue<MarketUpdateList>();
        private Queue<string> SQLInsertQueue = new Queue<string>();
        private Queue<int> PortToDeleteQueue = new Queue<int>();
        private object MarketDataProcessSync = new object();
        private object SQLInsertProcessSync = new object();

        DataTable newTransactionTable;
        DataTable newTradesTable;

        public DateTime DataDistTimer = new DateTime(1900, 01, 01);
        public DateTime _CloseDate = new DateTime(1900, 01, 01);
        public DateTime LastFWCalc = new DateTime(1900, 01, 01);

        public RTCalcRunner()
        {
            RecalcAllThread = new Thread(ReCalcAll);
            RecalcAllThread.Name = "RecalcAllThread";

            UpdateQuantitiesThread = new Thread(UpdateQuantities);
            UpdateQuantitiesThread.Name = "UpdateQuantitiesThread";
        }

        public void Start()
        {
            ContinueProcessing = false;
            if (!Started)
            {
                GlobalVars.Instance.StatusQueue.Enqueue("Starting...");
                StartRunning = true;
                Started = true;

                ProcessSQLInsert = true;

                SQLInsertProcessThread = new Thread(new ThreadStart(StartSQLInsert));
                SQLInsertProcessThread.Name = "SQLInsertProcessThread";
                SQLInsertProcessThread.Start();

                //DataTable curTable = curConn.Return_DataTable("SELECT * FROM NESTDB.dbo.Tb002_Portfolios WHERE Discountinued=0 and RT_Position=1");
                DataTable curTable = curConn.Return_DataTable("SELECT * FROM NESTDB.dbo.Tb002_Portfolios WHERE Discountinued=0 and RT_Position=1");

                foreach (DataRow curRow in curTable.Rows)
                {
                    int IdPortfolio = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]);
                    if (!CalcPortfolios.ContainsKey(IdPortfolio))
                    {
                        CreateCalculator(IdPortfolio);
                    }
                }

                Initialized = true;

                GlobalVars.Instance.BaseTablesChange = DateTime.Now;

                curNDistConn = new NDistConn("192.168.0.206", 15432);
                curNDistConn.OnData += new EventHandler(NewMarketData);

                SubscribeAllTickers();

                ContinueProcessing = true;


                StartRunning = false;

                if (OnStarted != null)
                {
                    OnStarted(this, new EventArgs());
                }
            }
            

            while (ContinueProcessing)
            { }
        }

        public void Connect()
        {
            if (Initialized && Started && ProcessMktData)
                try
                {
                    if (curNDistConn.IsConnected())
                    {
                        curNDistConn.Disconnect();
                    }

                    GlobalVars.Instance.StatusQueue.Enqueue("Trying to Connect");
                    curNDistConn.Connect();

                    if (curNDistConn.IsConnected())
                    {
                        GlobalVars.Instance.StatusQueue.Enqueue("Connected");
                        GlobalVars.Instance.MarketDataConnected = true;
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }

                    curNDistConn.ReSubscribe();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
        }

        public void Dispose()
        {
            CheckQuantityChangeRunning = false;
            UpdateQuantitiesRunning = false;
            BloombergBackup = false;
            ProcessMktData = false;
            ProcessSQLInsert = false;
            FlagNewPositionFound = false;
            RecalcAllRunning = false;
            StartRunning = false;
            Started = false;
            Initialized = false;
            RefreshSlowRunning = false;
            RefreshFastRunning = false;
            RefreshQntRunning = false;
            ContinueProcessing = false;
        }

        private void FlushPositionTable()
        {
            lock (SQLInsertProcessSync)
            {
                foreach (DataRow dtRow in GlobalVars.Instance.UpdatePosTable.Rows)
                {
                    for (int i = 0; i < GlobalVars.Instance.UpdatePosTable.Columns.Count; i++)
                    {
                        string stringVal = dtRow[i].ToString();
                        if (stringVal.Contains("NaN (N") || stringVal.Contains("Infinito"))
                        {
                            dtRow[i] = 0;
                            dtRow["Calc Error Flag"] = 1;
                        }
                    }
                }

                SqlParameter parameter = new SqlParameter("@TempTable", SqlDbType.Structured);
                parameter.Value = GlobalVars.Instance.UpdatePosTable;

                int RetVal = curConn.ExecuteNonQuery("[NESTRT].[dbo].[Proc_Update_Positions]", parameter, CommandType.StoredProcedure);

                if (RetVal == -1 || RetVal == -2 || RetVal == 0)
                {
                    GlobalVars.Instance.StatusQueue.Enqueue("MISSING Pos in Update: " + (GlobalVars.Instance.UpdatePosTable.Rows.Count - RetVal).ToString());
                }

                GlobalVars.Instance.UpdatePosTable.Clear();
            }
        }


        #region Market Data

        public bool ProcessMktData;

        public void EnableMarketData()
        {
            ProcessMktData = true;
            MarketDataProcessThread = new Thread(new ThreadStart(ProcessMarketData));
            MarketDataProcessThread.Name = "MarketDataProcessThread";
            MarketDataProcessThread.Start();
            GlobalVars.Instance.StatusQueue.Enqueue("Enabling Market Data...");
            curNDistConn.Connect();
        }

        public void DisableMarketData()
        {
            if (curNDistConn != null)
            {
                curNDistConn.Disconnect();
            }
            ProcessMktData = false;
            GlobalVars.Instance.StatusQueue.Enqueue("Disabling Market Data...");
        }

        private void UpdateRTPriceDB()
        {
            lock (CalcPortfolios)
            {
                foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                {
                    bool CalcFW = false;
                    if (DateTime.Now.Subtract(LastFWCalc).TotalSeconds > 120)
                    {
                        LastFWCalc = DateTime.Now;
                        CalcFW = true;
                    }

                    lock (curRTCalc.curCalc.AllPositions)
                    {
                        foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                        {
                            if (curPositionItem.RTPriceFlag && (curPositionItem.IdBook != 7 || CalcFW))
                            {
                                GlobalVars.Instance.LastMarketDataUpdate = DateTime.Now;
                                long startms = GlobalVars.Instance.MasterClock.ElapsedMilliseconds;
                                UpdatePosition(curPositionItem);
                                double prevVal = GlobalVars.Instance.RTFieldsUpdateMS;
                                GlobalVars.Instance.RTFieldsUpdateMS = (prevVal * 100 + (GlobalVars.Instance.MasterClock.ElapsedMilliseconds - startms)) / 101;
                                curPositionItem.RTPriceFlag = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Calculator Manager

        public void CreateCalculator(int IdPortfolio)
        {
            RealTimeCalc curRTCalc = CalcPortfolio(IdPortfolio);
            curRTCalc.LastBaseDateCalc = curConn.Return_DateTime("SELECT COALESCE(MAX([Last Calc]),'2500-01-01') FROM dbo.Tb000_Historical_Positions WHERE [Id Portfolio]=" + IdPortfolio);
            lock (CalcPortfolios) { CalcPortfolios.Add(IdPortfolio, curRTCalc); }
            curRTCalc.DeleteDBPositions();
            InsertAllPositions(curRTCalc);
            UpdateAllPositions_ViaDatatable(curRTCalc);
            //UpdateAllPositions(curRTCalc);
        }

        public void DeleteAllCalculators()
        {
            try
            {
                if (curNDistConn != null)
                {
                    curNDistConn.Disconnect();
                }

                DisableMarketData();
                ProcessSQLInsert = false;

                while (RecalcAllRunning || StartRunning)
                {
                    Thread.Sleep(1000);
                }

                UnSubscribeAllTickers();

                lock (CalcPortfolios)
                {
                    List<int> portfoliosToDelete = new List<int>();
                    foreach (int curIdPortoflio in CalcPortfolios.Keys)
                    {
                        portfoliosToDelete.Add(curIdPortoflio);
                    }
                    foreach (int curIdPortoflio in portfoliosToDelete)
                    {
                        DeleteCalculator(curIdPortoflio);
                    }
                }

                lock (CalcPortfolios) { CalcPortfolios.Clear(); }
                lock (MarketDataQueue) { MarketDataQueue.Clear(); }
                lock (SQLInsertQueue) { SQLInsertQueue.Clear(); }
                lock (DBPositions) { DBPositions.Clear(); }
                GlobalVars.Instance.ClearBuffers();

                Started = false;
                Initialized = false;
            }
            catch { }
        }

        public void DeleteCalculator(int IdPortfolio)
        {
            lock (DBPositions)
            {
                List<int> ItemsToDelete = new List<int>();
                foreach (KeyValuePair<int, PositionItem> curKvp in DBPositions)
                {
                    if (curKvp.Value.IdPortfolio == IdPortfolio) { ItemsToDelete.Add(curKvp.Key); }
                }

                foreach (int curItemToDelete in ItemsToDelete)
                {
                    DBPositions.Remove(curItemToDelete);
                }
            }

            lock (SymbolIndex)
            {
                foreach (List<PositionItem> curList in SymbolIndex.Values)
                {
                    List<PositionItem> ItemsToDelete = new List<PositionItem>();

                    foreach (PositionItem curPositionItem in curList)
                    {
                        if (curPositionItem.IdPortfolio == IdPortfolio)
                        {
                            ItemsToDelete.Add(curPositionItem);
                        }
                    }
                    foreach (PositionItem curPositionItem in ItemsToDelete)
                    {
                        curList.Remove(curPositionItem);
                    }
                }
            }

            //GlobalVars.Instance.ClearBuffers();            

            lock (CalcPortfolios) { CalcPortfolios.Remove(IdPortfolio); }

            GlobalVars.Instance.StatusQueue.Enqueue("Deleted Calculator for Port:" + IdPortfolio);
        }

        public void ReCalcAll()
        {
            if (Initialized && Started)
            {
                if (!RecalcAllRunning)
                {
                    RecalcAllRunning = true;

                    //GlobalVars.Instance.ClearBuffers();
                    GlobalVars.Instance.LoadFixedBuffers();

                    GlobalVars.Instance.ClearPriceBuffers(); // NAO SEI O QUE ESSA FUNCAO FAZ == Essa funçao limpa somente os buffers de preço. Os buffers fixos são manipulados pelo LoadFixedBuffers.

                    GlobalVars.Instance.StatusQueue.Enqueue("Recalculating all positions");

                    DateTime StartTime = DateTime.Now;
                    DateTime InitialDate = new DateTime(2200, 01, 01);

                    string curPortFilter = "";

                    lock (CalcPortfolios)
                    {
                        foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                        {
                            DateTime LastHistoricalCalc = curConn.Return_DateTime("SELECT COALESCE(MAX([Last Calc]),'2500-01-01') FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Id Underlying] IS NOT NULL AND [Id Portfolio]= " + curRTCalc.IdPortfolio);

                            if (LastHistoricalCalc != curRTCalc.LastBaseDateCalc)
                            {
                                int counter = curConn.Return_Int("SELECT COUNT (*) FROM dbo.Tb000_Historical_Positions WHERE [Id Underlying] IS NULL AND [Id Portfolio]=43 AND [Date Now] = '" + curRTCalc.PositionDate.ToString("yyyy-MM-dd") + "'");

                                if (counter == 0)
                                {
                                    PortToDeleteQueue.Enqueue(curRTCalc.IdPortfolio);
                                }
                                curRTCalc.LastBaseDateCalc = LastHistoricalCalc;
                            }

                            curPortFilter += "," + curRTCalc.PortFilter;
                            _CloseDate = curRTCalc.curCalc.CloseDate;

                            if (_CloseDate < InitialDate)
                            {
                                InitialDate = _CloseDate;
                            }
                        }
                    }

                    curPortFilter = curPortFilter.Substring(1);

                    newTransactionTable = DBUtils.LoadTransactionTable(InitialDate, DateTime.Now.Date, curPortFilter);
                    newTradesTable = DBUtils.LoadTradesTable(InitialDate, DateTime.Now.Date, curPortFilter);

                    List<int> PortsToCalculate = new List<int>();

                    lock (CalcPortfolios)
                    {
                        foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                        {
                            PortsToCalculate.Add(curRTCalc.IdPortfolio);
                        }
                    }

                    foreach (int curIdPortfolio in PortsToCalculate)
                    {
                        RecalcPortfolio(curIdPortfolio);
                    }

                    while (PortToDeleteQueue.Count > 0)
                    {
                        int PortToDelete = PortToDeleteQueue.Dequeue();

                        DeleteCalculator(PortToDelete);
                        CreateCalculator(PortToDelete);
                        SubscribeAllTickers();
                    }

                    GlobalVars.Instance.LastRecalcAllTimeTaken = DateTime.Now.Subtract(StartTime);
                    GlobalVars.Instance.LastRecalcAll = DateTime.Now;
                    GlobalVars.Instance.ClearPriceBuffers();

                    RecalcAllRunning = false;
                }
            }
        }

        public void RecalcPortfolio(int IdPortfolio)
        {
            RealTimeCalc curRTCalc = CalcPortfolios[IdPortfolio];
            LBCalculator newRTCalc = curRTCalc.getRecalc(newTransactionTable, newTradesTable);
            string UpdateString;

            foreach (PositionItem newPositionItem in newRTCalc.AllPositions)
            {
                PositionItem curPositionItem = curRTCalc.curCalc.AllPositions.Find(
                            delegate(PositionItem testPositionItem)
                            {
                                if (testPositionItem.DateNow == newPositionItem.DateNow &&
                                    testPositionItem.IdPortfolio == newPositionItem.IdPortfolio &&
                                    testPositionItem.IdBook == newPositionItem.IdBook &&
                                    testPositionItem.IdSection == newPositionItem.IdSection &&
                                    testPositionItem.IdSecurity == newPositionItem.IdSecurity)
                                { return true; }

                                else { return false; }
                            }
                            );

                if (curPositionItem != null)
                {
                    bool FlagIgnoreLast = false;

                    if (curPositionItem.RTLast != 0 || curPositionItem.RTEnabled.Contains(curPositionItem.IdSourceLast))
                    { FlagIgnoreLast = true; }

                    if (!ProcessMktData)
                    { FlagIgnoreLast = false; }

                    UpdateString = curPositionItem.GetCompareString(newPositionItem, curPositionItem, FlagIgnoreLast);

                    if (UpdateString != "")
                    {
                        newPositionItem.IdPosition = curPositionItem.IdPosition;

                        //lock(curRTCalc.curCalc.AllPositions)
                        //{
                        newPositionItem.RTPriceFlag = false;

                        lock (curRTCalc.curCalc.AllPositions)
                        {
                            curRTCalc.curCalc.AllPositions.Remove(curPositionItem);
                            curRTCalc.curCalc.AllPositions.Add(newPositionItem);
                        }

                        lock (SymbolIndex)
                        {
                            if (SymbolIndex.ContainsKey(curPositionItem.SubscribeTicker)) { SymbolIndex[curPositionItem.SubscribeTicker].Remove(curPositionItem); }
                            if (SymbolIndex.ContainsKey(newPositionItem.SubscribeTicker)) { SymbolIndex[newPositionItem.SubscribeTicker].Add(newPositionItem); }
                        }

                        Subscribe(newPositionItem);
                        UnSubscribe(curPositionItem);

                        if (newPositionItem.RTEnabled.Contains(newPositionItem.IdSourceLast) && ProcessMktData)
                        {
                            newPositionItem.RTLast = curPositionItem.RTLast;
                            newPositionItem.RTLastUcAdmin = curPositionItem.RTLastUcAdmin;
                            newPositionItem.DisplayLast = curPositionItem.DisplayLast;
                        }

                        newPositionItem.QuantBoughtTrade = curPositionItem.QuantBoughtTrade;
                        newPositionItem.AmtBoughtTrade = curPositionItem.AmtBoughtTrade;
                        newPositionItem.QuantSoldTrade = curPositionItem.QuantSoldTrade;
                        newPositionItem.AmtSoldTrade = curPositionItem.AmtSoldTrade;

                        curPositionItem.IdPosition = -99999;

                        UpdatePosition(newPositionItem);
                    }
                }
                else
                {
                    lock (curRTCalc.curCalc.AllPositions)
                    {
                        curRTCalc.curCalc.AllPositions.Add(newPositionItem);
                    }
                    InsertPosition(newPositionItem);
                    UpdatePosition(newPositionItem);
                    Subscribe(newPositionItem);
                }
            }

            List<PositionItem> ItemsToDelete = new List<PositionItem>();

            foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
            {
                PositionItem newPositionItem = newRTCalc.AllPositions.Find(
                            delegate(PositionItem testPositionItem)
                            {
                                if (testPositionItem.DateNow == curPositionItem.DateNow &&
                                    testPositionItem.IdPortfolio == curPositionItem.IdPortfolio &&
                                   testPositionItem.IdBook == curPositionItem.IdBook &&
                                   testPositionItem.IdSection == curPositionItem.IdSection &&
                                   testPositionItem.IdSecurity == curPositionItem.IdSecurity)
                                { return true; }

                                else { return false; }
                            }
                            );

                if (newPositionItem == null)
                {
                    ItemsToDelete.Add(curPositionItem);
                }
            }

            foreach (PositionItem curPositionItem in ItemsToDelete)
            {
                lock (curRTCalc.curCalc.AllPositions)
                {
                    curRTCalc.curCalc.AllPositions.Remove(curPositionItem);
                }
                string StringSQL = "DELETE FROM NESTRT.dbo.Tb000_Posicao_Atual WHERE [Id Position]=" + curPositionItem.IdPosition + " ; ";
                curConn.ExecuteNonQuery(StringSQL);
            }
        }

        #endregion

        #region Positions Manager

        private RealTimeCalc CalcPortfolio(int IdPortfolio)
        {
            RealTimeCalc curRTCalc = new RealTimeCalc(IdPortfolio);
            curRTCalc.Calculate();
            return curRTCalc;
        }

        public void InsertAllPositions(RealTimeCalc curRTCalc)
        {
            GlobalVars.Instance.StatusQueue.Enqueue("Inserting Port: " + curRTCalc.IdPortfolio + " - Lines: " + curRTCalc.curCalc.AllPositions.Count);

            int count = 0;
            DateTime StartTime = DateTime.Now;

            foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
            {
                InsertPosition(curPositionItem);
                count++;
            }
        }

        private void InsertPosition(PositionItem curPositionItem)
        {
            string StringSQL;

            StringSQL = "INSERT INTO NESTRT.dbo.Tb000_Posicao_Atual ([Date Now], [Id Portfolio], [Id Book], [Id Section], [Id Ticker], Position, NAV)\r\n ";

            StringSQL += "SELECT "
                + "'" + curPositionItem.DateNow.ToString("yyyy-MM-dd") + "', "
                + curPositionItem.IdPortfolio + ", "
                + curPositionItem.IdBook + ", "
                + curPositionItem.IdSection + ", "
                + curPositionItem.IdSecurity + ", "
                + curPositionItem.CurrentPosition.ToString().Replace(",", ".") + ", "
                + curPositionItem.Nav.ToString().Replace(",", ".") +
                " SELECT @@IDENTITY ; ";

            using (newNestConn curConn = new newNestConn())
            {
                curPositionItem.IdPosition = curConn.Return_Int(StringSQL);
                if (curPositionItem.IdPosition == 0)
                {
                    Console.Write("");
                }
            }
        }

        //private void UpdateAllPositions(RealTimeCalc curRTCalc)
        //{
        //    DateTime StartTime = DateTime.Now;

        //    GlobalVars.Instance.StatusQueue.Enqueue("Updating Port: " + curRTCalc.IdPortfolio + " - Lines: " + curRTCalc.curCalc.AllPositions.Count);

        //    foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
        //    {
        //        long startms = GlobalVars.Instance.MasterClock.ElapsedMilliseconds;
        //        UpdatePosition(curPositionItem);
        //        GlobalVars.Instance.AllFieldsUpdateMS = GlobalVars.Instance.MasterClock.ElapsedMilliseconds - startms;
        //        curPositionItem.RTPriceFlag = false;
        //    }
        //}

        private void UpdateAllPositions_ViaDatatable(RealTimeCalc curRTCalc)
        {
            DateTime StartTime = DateTime.Now;
            GlobalVars.Instance.StatusQueue.Enqueue("Updating Port: " + curRTCalc.IdPortfolio + " - Lines: " + curRTCalc.curCalc.AllPositions.Count);

            long startms = GlobalVars.Instance.MasterClock.ElapsedMilliseconds;
            lock (SQLInsertProcessSync)
            {
                foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                {
                    UpdatePosition(curPositionItem);
                    curPositionItem.RTPriceFlag = false;
                }
                FlushPositionTable();
            }

            GlobalVars.Instance.AllFieldsUpdateMS = GlobalVars.Instance.MasterClock.ElapsedMilliseconds - startms;
        }

        private void UpdatePosition_OLD(PositionItem newPositionItem)
        {
            string StringSQL;

            PositionItem dbPositionItem;
            string UpdateString = "";

            if (DBPositions.TryGetValue(newPositionItem.IdPosition, out dbPositionItem))
            {
                UpdateString = newPositionItem.GetCompareString(newPositionItem, dbPositionItem, false);
                if (UpdateString.Length > 1)
                {
                    UpdateString += ", [Last Calc] = getdate()";
                }
            }
            else
            {
                //UpdateString = newPositionItem.GetInitialString(UpdateType.AllFields);
            }

            if (UpdateString.Length > 1 && UpdateString != null)
            {
                if (UpdateString.Contains("NaN (Não é um número)") || UpdateString.Contains("Infinito"))
                {
                    UpdateString = UpdateString.Replace("NaN (Não é um número)", "0");
                    UpdateString = UpdateString.Replace("Infinito", "0");
                    UpdateString += ", [Calc Error Flag]=1";
                }
                else
                {
                    UpdateString += ", [Calc Error Flag]=0";
                }

                StringSQL = "UPDATE NESTRT.dbo.Tb000_Posicao_Atual  SET " + UpdateString + " WHERE [Id Position]=" + newPositionItem.IdPosition + " ; ";
                lock (SQLInsertProcessSync)
                {
                    GlobalVars.Instance.UpdatePosTable.Rows.Add(newPositionItem.GetDataRow());
                    //SQLInsertQueue.Enqueue(StringSQL);
                }

                lock (DBPositions)
                {
                    DBPositions.Remove(newPositionItem.IdPosition);
                    DBPositions.Add(newPositionItem.IdPosition, newPositionItem.GetClone());
                }
            }
        }

        private void UpdatePosition(PositionItem newPositionItem)
        {
            PositionItem dbPositionItem;
            string UpdateString = "";

            if (DBPositions.TryGetValue(newPositionItem.IdPosition, out dbPositionItem))
            {
                UpdateString = newPositionItem.GetCompareString(newPositionItem, dbPositionItem, false);
                //if (UpdateString.Length > 1)
                //{
                //    UpdateString += ", [Last Calc] = getdate()";
                //}
            }
            else
            {
                UpdateString = "New Position";
            }

            if (UpdateString.Length > 1 && UpdateString != null)
            {
                //if (UpdateString.Contains("NaN (Não é um número)") || UpdateString.Contains("Infinito"))
                //{
                //    UpdateString = UpdateString.Replace("NaN (Não é um número)", "0");
                //    UpdateString = UpdateString.Replace("Infinito", "0");
                //    newPositionItem.CalcError = 1;  // UpdateString += ", [Calc Error Flag]=1";
                //}
                //else
                //{
                //    newPositionItem.CalcError = 0;  //  UpdateString += ", [Calc Error Flag]=0";
                //}

                //StringSQL = "UPDATE NESTRT.dbo.Tb000_Posicao_Atual  SET " + UpdateString + " WHERE [Id Position]=" + newPositionItem.IdPosition + " ; ";
                lock (SQLInsertProcessSync)
                {
                    if (Initialized)
                    {
                        List<DataRow> RowsToDelete = new List<DataRow>();

                        foreach (DataRow dtRow in GlobalVars.Instance.UpdatePosTable.Rows)
                        {
                            if (newPositionItem.IdPosition == int.Parse(dtRow["Id Position"].ToString()))
                            {
                                RowsToDelete.Add(dtRow);
                            }

                            //for (int i = 0; i < GlobalVars.Instance.UpdatePosTable.Columns.Count; i++)
                            //{
                            //    string stringVal = dtRow[i].ToString();
                            //    if (stringVal.Contains("NaN (N") || stringVal.Contains("Infinito"))
                            //    {
                            //        dtRow[i] = 0;
                            //        dtRow["Calc Error Flag"] = 1;
                            //        break;
                            //    }
                            //}
                        }

                        foreach (DataRow rowToDelete in RowsToDelete)
                        {
                            GlobalVars.Instance.UpdatePosTable.Rows.Remove(rowToDelete);
                        }

                        RowsToDelete.Clear();
                    }

                    GlobalVars.Instance.UpdatePosTable.Rows.Add(newPositionItem.GetDataRow());

                    //DataRow curRow = newPositionItem.GetDataRow();

                    //for (int i = 0; i < GlobalVars.Instance.UpdatePosTable.Columns.Count; i++)
                    //{
                    //    string stringVal = curRow[i].ToString();
                    //    if (stringVal.Contains("NaN (N") || stringVal.Contains("Infinito"))
                    //    {
                    //        curRow[i] = 0;
                    //        curRow["Calc Error Flag"] = 1;
                    //    }
                    //}

                    //RowsToDelete.Clear();

                    //GlobalVars.Instance.UpdatePosTable.Rows.Add(newPositionItem.GetDataRow());
                    ////SQLInsertQueue.Enqueue(StringSQL);
                }

                lock (DBPositions)
                {
                    DBPositions.Remove(newPositionItem.IdPosition);
                    DBPositions.Add(newPositionItem.IdPosition, newPositionItem.GetClone());
                }
            }
        }

        #endregion

        #region Checks

        public bool CheckPriceChange()
        {
            DateTime StartTime = DateTime.Now;
            DateTime HistoricalPrice;
            DateTime RealTimePrice;

            string SQLString = "";
            SQLString = " Use NESTDB ; SELECT  MAX(last_user_update) last_user_update FROM " +
                         " ( " +
                         " SELECT OBJECT_NAME(OBJECT_ID) AS TableName, last_user_update " +
                         " FROM sys.dm_db_index_usage_stats " +
                         " WHERE database_id = DB_ID( 'NESTDB') " +
                         " AND ( " +
                         " OBJECT_ID=OBJECT_ID('Tb050_Preco_Acoes_Onshore') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb051_Preco_Acoes_Offshore') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb052_Precos_Titulos') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb053_Precos_Indices') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb054_Precos_Opcoes') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb055_Precos_Swap') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb056_Precos_Fundos') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb057_Precos_Commodities') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb058_Precos_Moedas') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb059_Precos_Futuros') OR " +
                         " OBJECT_ID=OBJECT_ID('Tb060_Preco_Caixa') " +
                         " ) ) X Use NESTDB ; ";

            HistoricalPrice = curConn.Return_DateTime(SQLString);

            if (HistoricalPrice != GlobalVars.Instance.HistPriceTablesChange && RecalcAllThread.ThreadState != System.Threading.ThreadState.Running)
            {
                GlobalVars.Instance.HistPriceTablesChange = HistoricalPrice;
                return true;
            }

            SQLString = " USE NESTRT ; SELECT  MAX(last_user_update) last_user_update FROM " +
                        " ( " +
                        " SELECT OBJECT_NAME(OBJECT_ID) AS TableName, last_user_update  " +
                        " FROM sys.dm_db_index_usage_stats  " +
                        " WHERE database_id = DB_ID( 'NESTRT')  " +
                        " AND ( " +
                        "		OBJECT_ID=OBJECT_ID('Tb065_Ultimo_Preco')  " +
                        "	  ) " +
                        " ) X  Use NESTDB ;";

            RealTimePrice = curConn.Return_DateTime(SQLString);

            if (DateTime.Now.Hour >= 18)
            {
                if (RealTimePrice != GlobalVars.Instance.RTPriceTablesChange && RecalcAllThread.ThreadState != System.Threading.ThreadState.Running)
                {
                    GlobalVars.Instance.RTPriceTablesChange = RealTimePrice;
                    return true;
                }
            }

            return false;
        }

        public bool CheckBaseTableChange()
        {
            DateTime StartTime = DateTime.Now;
            DateTime LastUpdateTime;

            string SQLString =
                                 " SELECT  MAX(last_user_update) last_user_update FROM  " +
                                " (  " +
                                " SELECT OBJECT_NAME(OBJECT_ID) AS TableName, last_user_update  " +
                                " FROM sys.dm_db_index_usage_stats  " +
                                " WHERE database_id = DB_ID( 'NESTDB') " +
                                " AND (OBJECT_ID=OBJECT_ID('dbo.Tb404_Section')  " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb400_Books')  " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb725_Fowards') " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb726_Fowards_Early_Close') " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb003_PortAccounts') " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb007_Accounts') " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb003_PortAccounts') " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb726_Fowards_Early_Close') " +
                                " OR OBJECT_ID=OBJECT_ID('dbo.Tb002_Portfolios'))  " +
                                " ) x ";

            LastUpdateTime = curConn.Return_DateTime(SQLString);


            if (LastUpdateTime != GlobalVars.Instance.BaseTablesChange && RecalcAllThread.ThreadState != System.Threading.ThreadState.Running)
            {
                GlobalVars.Instance.BaseTablesChange = LastUpdateTime;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckSecurityChange()
        {
            DateTime tempTime;

            string SQLString =
                   " SELECT  MAX(last_user_update) last_user_update FROM  " +
                   " (  " +
                   " SELECT OBJECT_NAME(OBJECT_ID) AS TableName, last_user_update  " +
                   " FROM sys.dm_db_index_usage_stats  " +
                   " WHERE database_id = DB_ID( 'NESTDB') " +
                   " AND (OBJECT_ID=OBJECT_ID('dbo.Tb001_Securities_Fixed')  " +
                   " OR OBJECT_ID=OBJECT_ID('dbo.Tb001_Securities_Variable'))  " +
                   " ) x ";

            tempTime = curConn.Return_DateTime(SQLString);

            if (tempTime != GlobalVars.Instance.SecuritiesTablesChange && RecalcAllThread.ThreadState != System.Threading.ThreadState.Running)
            {
                GlobalVars.Instance.SecuritiesTablesChange = tempTime;
                GlobalVars.Instance.LoadSymbolData(DateTime.Now.Date);
                return true;
            }
            else
                return false;
        }

        #endregion

        #region Realtime Prices

        private void NewMarketData(object sender, EventArgs e)
        {
            if (Started && Initialized)
            {
                if (!ProcessMktData) { return; }

                MarketUpdateList curList = (MarketUpdateList)e;

                MarketUpdateList curList2 = new MarketUpdateList();

                foreach (MarketUpdateItem xlist in curList.ItemsList)
                {
                    curList2.ItemsList.Add(xlist);
                }

                lock (MarketDataProcessSync)
                {
                    MarketDataQueue.Enqueue(curList2);
                }
            }
        }

        private void ProcessMarketData()
        {
            while (ProcessMktData)
            {
                MarketUpdateList curList = new MarketUpdateList();
                bool nothingToProcess = false;

                GlobalVars.Instance.MarketDataQueueCounter = MarketDataQueue.Count;

                lock (MarketDataProcessSync)
                {
                    if (MarketDataQueue.Count > 0) { curList = MarketDataQueue.Dequeue(); }
                    else { nothingToProcess = true; }
                }

                if (nothingToProcess) { Thread.Sleep(100); }
                else { ProcessMarketUpdateList(curList); }
            }
        }

        private void StartSQLInsert()
        {
            while (ProcessSQLInsert)
            {
                MarketUpdateList curList = new MarketUpdateList();
                bool nothingToProcess = false;

                GlobalVars.Instance.SQLInsertQueueCounter = SQLInsertQueue.Count;

                lock (SQLInsertProcessSync)
                {
                    //if (SQLInsertQueue.Count > 0)
                    //{
                    //    string StringSQL = SQLInsertQueue.Dequeue();
                    //    curConn.ExecuteNonQuery(StringSQL);
                    //}
                    //else { nothingToProcess = true; }

                    if (GlobalVars.Instance.UpdatePosTable.Rows.Count > 0)
                    {
                        FlushPositionTable();
                    }
                    else { nothingToProcess = true; }
                }



                if (nothingToProcess) { Thread.Sleep(100); }
            }
        }

        private void ProcessMarketUpdateList(MarketUpdateList curList)
        {
            GlobalVars.Instance.MarketDataConnected = true;
            DataDistTimer = DateTime.Now;

            MarketUpdateList UpdateList = curList;

            foreach (MarketUpdateItem curUpdateItem in UpdateList.ItemsList)
            {
                if (SubscribeList.ContainsKey(curUpdateItem.Ticker))
                {
                    SubscribeList[curUpdateItem.Ticker].Update(curUpdateItem);

                    if (curUpdateItem.ValueDouble != double.MaxValue && curUpdateItem.ValueDouble != double.MinValue)
                    {
                        switch (curUpdateItem.FLID)
                        {
                            case NestFLIDS.Bid:
                            case NestFLIDS.Ask:
                            case NestFLIDS.Last:
                                InsertMarketPrices(SymbolIndex[curUpdateItem.Ticker], curUpdateItem.FLID, Math.Round(curUpdateItem.ValueDouble, 8));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void InsertMarketPrices(List<PositionItem> Positions, NestFLIDS curFLID, double curValue)
        {
            int curIdSecurity = 0;
            if (curValue > 0)
            {
                try
                {
                    lock (Positions)
                    {
                        foreach (PositionItem curPositionItem in Positions)
                        {
                            switch (curPositionItem.IdSourceLast)
                            {
                                case 0:
                                case 1:
                                case 4:
                                case 5:
                                case 13:
                                case 16:
                                case 18:
                                case 24:
                                case 25:
                                case 26:
                                case 27:
                                case 28:
                                case 30:
                                case 32:
                                case 33:
                                case 34:

                                    curIdSecurity = curPositionItem.IdSecurity;

                                    if (curPositionItem.IdInstrument != 16)
                                    {

                                        if (curFLID == NestFLIDS.Last) { curPositionItem.RTLast = curValue; curPositionItem.RTLastUcAdmin = curValue; }
                                        else if (curFLID == NestFLIDS.Bid) { curPositionItem.RTBid = curValue; }
                                        else if (curFLID == NestFLIDS.Ask) { curPositionItem.RTAsk = curValue; }
                                    }
                                    else if (curPositionItem.IdInstrument == 16)
                                    {
                                        if (curFLID == NestFLIDS.Last)
                                        {
                                            curPositionItem.RTLast = GlobalVars.Instance.GetPU(curPositionItem.IdSecurityPrice, curValue, DateTime.Now).PU;
                                            curPositionItem.RTLastUcAdmin = curPositionItem.RTLast;
                                            curPositionItem.DisplayLast = curValue;
                                        }
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    lock (CalcPortfolios)
                    {
                        foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                        {
                            lock (curRTCalc.curCalc.AllPositions)
                            {
                                foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                                {
                                    switch (curPositionItem.IdSourceLast)
                                    {
                                        case 0:
                                        case 1:
                                        case 4:
                                        case 5:
                                        case 13:
                                        case 16:
                                        case 18:
                                        case 24:
                                        case 25:
                                        case 26:
                                        case 27:
                                        case 28:
                                        case 30:
                                        case 32:
                                        case 33:
                                        case 34:
                                            //curIdSecurity = curPositionItem.IdSecurity;

                                            if (curPositionItem.IdInstrument == 15 || curPositionItem.IdInstrument == 12 || curPositionItem.PriceFromUnderlying == 1)
                                            {
                                                if (curPositionItem.IdUnderlying == curIdSecurity && curPositionItem.IdSecurity != curIdSecurity)
                                                {
                                                    {
                                                        if (curFLID == NestFLIDS.Last) { curPositionItem.RTLast = curValue; curPositionItem.RTLastUcAdmin = curValue; }
                                                        else if (curFLID == NestFLIDS.Bid) { curPositionItem.RTBid = curValue; }
                                                        else if (curFLID == NestFLIDS.Ask) { curPositionItem.RTAsk = curValue; }
                                                    }
                                                }
                                            }
                                            break;
                                        default: break;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        public void SubscribeAllTickers()
        {
            if (!BloombergBackup)
            {
                lock (CalcPortfolios)
                {
                    foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                    {
                        foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                        {
                            Subscribe(curPositionItem);
                        }
                    }
                }
            }
            else
            {
                lock (CalcPortfolios)
                {
                    foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                    {
                        foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                        {
                            SubscribeBloomberg(curPositionItem);
                        }
                    }
                }
            }
        }

        private void Subscribe(PositionItem curPositionItem)
        {
            string SubscriptionTicker = GetSubscriptionTicker(curPositionItem);
            Sources curSource = (Sources)(curPositionItem.RTUpdateSource);



            LBMarketDataItem curLBMarketDataItem = new LBMarketDataItem();

            curLBMarketDataItem.IdTicker = curPositionItem.IdSecurity;
            curLBMarketDataItem.Ticker = curPositionItem.NestTicker;
            curLBMarketDataItem.Subscribed = false;
            curLBMarketDataItem.Source = curSource;
            curLBMarketDataItem.SubscribeTicker = SubscriptionTicker;

            curPositionItem.SubscribeTicker = SubscriptionTicker;

            if (!SubscribeList.ContainsKey(SubscriptionTicker))
            {
                lock (SubscribeList) { SubscribeList.Add(SubscriptionTicker, curLBMarketDataItem); }
                curNDistConn.Subscribe(SubscriptionTicker, curLBMarketDataItem.Source);
            }

            if (!SymbolIndex.ContainsKey(SubscriptionTicker))
                lock (SymbolIndex) { SymbolIndex.Add(SubscriptionTicker, new List<PositionItem>()); }

            if (!SymbolIndex[SubscriptionTicker].Contains(curPositionItem))
                lock (SymbolIndex) { SymbolIndex[SubscriptionTicker].Add(curPositionItem); }
        }

        private void SubscribeBloomberg(PositionItem curPositionItem)
        {
            if (curPositionItem.BloombergTicker == "") return;

            string SubscriptionTicker = curPositionItem.BloombergTicker;

            Sources curSource = Sources.Bloomberg;

            LBMarketDataItem curLBMarketDataItem = new LBMarketDataItem();

            curLBMarketDataItem.IdTicker = curPositionItem.IdSecurity;
            curLBMarketDataItem.Ticker = SubscriptionTicker;
            curLBMarketDataItem.Subscribed = false;
            curLBMarketDataItem.Source = curSource;
            curLBMarketDataItem.SubscribeTicker = SubscriptionTicker;

            curPositionItem.SubscribeTicker = SubscriptionTicker;

            if (!SubscribeList.ContainsKey(SubscriptionTicker))
            {
                lock (SubscribeList) { SubscribeList.Add(SubscriptionTicker, curLBMarketDataItem); }
                curNDistConn.Subscribe(SubscriptionTicker, curLBMarketDataItem.Source);
            }

            if (!SymbolIndex.ContainsKey(SubscriptionTicker))
                lock (SymbolIndex) { SymbolIndex.Add(SubscriptionTicker, new List<PositionItem>()); }

            if (!SymbolIndex[SubscriptionTicker].Contains(curPositionItem))
                lock (SymbolIndex) { SymbolIndex[SubscriptionTicker].Add(curPositionItem); }
        }

        public void UnSubscribeAllTickers()
        {
            lock (CalcPortfolios)
            {
                foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                {
                    foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                    {
                        UnSubscribeBloomberg(curPositionItem);
                        UnSubscribe(curPositionItem);
                    }
                }
            }

            lock (SubscribeList) { SubscribeList.Clear(); }
            lock (SymbolIndex) { SymbolIndex.Clear(); }
        }

        private void UnSubscribeBloomberg(PositionItem curPositionItem)
        {
            string SubscriptionTicker = curPositionItem.BloombergTicker;
            Sources curSource = Sources.Bloomberg;

            if (SymbolIndex.ContainsKey(SubscriptionTicker))
            {
                if (SymbolIndex[SubscriptionTicker].Contains(curPositionItem))
                {
                    lock (SymbolIndex)
                    {
                        SymbolIndex[SubscriptionTicker].Remove(curPositionItem);

                        if (SymbolIndex[SubscriptionTicker].Count == 0)
                        {
                            SymbolIndex.Remove(SubscriptionTicker);
                        }
                    }
                    curNDistConn.UnSubscribe(SubscriptionTicker, curSource);
                }
            }
        }

        private void UnSubscribe(PositionItem curPositionItem)
        {
            string SubscriptionTicker = GetSubscriptionTicker(curPositionItem);
            Sources curSource = (Sources)(curPositionItem.RTUpdateSource);

            if (SymbolIndex.ContainsKey(SubscriptionTicker))
            {
                if (SymbolIndex[SubscriptionTicker].Contains(curPositionItem))
                {
                    lock (SymbolIndex)
                    {
                        SymbolIndex[SubscriptionTicker].Remove(curPositionItem);

                        if (SymbolIndex[SubscriptionTicker].Count == 0)
                        {
                            SymbolIndex.Remove(SubscriptionTicker);
                        }
                    }
                    curNDistConn.UnSubscribe(SubscriptionTicker, curSource);
                }
            }
        }

        private string GetSubscriptionTicker(PositionItem curPositionItem)
        {
            Sources curSource = (Sources)(curPositionItem.RTUpdateSource);

            switch (curSource)
            {
                case Sources.Bloomberg:
                    return curPositionItem.BloombergTicker;
                case Sources.Yahoo:
                    return curPositionItem.AdminTicker;
                case Sources.Reuters:
                case Sources.FlexTrade:
                    return curPositionItem.ReutersTicker;
                case Sources.BMF:
                case Sources.Bovespa:
                case Sources.ProxyDiff:
                case Sources.XPBMF:
                case Sources.XPBOV:
                case Sources.BELL:
                    return curPositionItem.ExchangeTicker;
                case Sources.BancoCentral:
                case Sources.None:
                default:
                    return curPositionItem.NestTicker;
            }
        }

        #endregion

        #region Realtime Quantity

        public bool CheckQuantityChangeRunning = false;
        public bool UpdateQuantitiesRunning = false;
        DateTime QuantityTablesChange = new DateTime(1900, 01, 01);

        public void CheckQuantityChange()
        {
            if (!CheckQuantityChangeRunning)
            {
                CheckQuantityChangeRunning = true;

                string SQLString = "SELECT  MAX(last_user_update) last_user_update FROM " +
                                    " ( " +
                                    " SELECT OBJECT_NAME(OBJECT_ID) AS TableName, last_user_update " +
                                     " FROM sys.dm_db_index_usage_stats " +
                                     " WHERE database_id = DB_ID( 'NESTDB') " +
                                     " AND (OBJECT_ID=OBJECT_ID('Tb012_Ordens') OR OBJECT_ID=OBJECT_ID('dbo.Tb013_Trades') OR OBJECT_ID=OBJECT_ID('dbo.Tb351_Trade_Alocation')) " +
                                     " ) X";

                QuantityTablesChange = curConn.Return_DateTime(SQLString);

                if (QuantityTablesChange != GlobalVars.Instance.QuantityTablesChange && UpdateQuantitiesThread.ThreadState != System.Threading.ThreadState.Running)
                {
                    //GlobalVars.Instance.StatusQueue.Enqueue("Quantity Updated " + DateTime.Now.ToString("hh:mm:ss:fff"));
                    UpdateQuantitiesThread = new Thread(UpdateQuantities);
                    UpdateQuantitiesThread.Name = "UpdateQuantitiesThread";
                    UpdateQuantitiesThread.Start();
                }
                CheckQuantityChangeRunning = false;
            }
        }

        public void UpdateQuantities()
        {
            if (!UpdateQuantitiesRunning)
            {
                UpdateQuantitiesRunning = true;

                GlobalVars.Instance.QuantityTablesChange = QuantityTablesChange;
                DateTime StartTime = DateTime.Now;
                string SQLString = " SELECT DataTrade,IdPortfolio,IdBook,IdSection,IdSecurity, SUM(Done) Done, SUM(AmtTrade) AmtTrade " +
                                " FROM  " +
                                " ( " +
                                " SELECT A.Data_Trade DataTrade, B.id_Portfolio IdPortfolio, A.[id book] IdBook, A.[id section] IdSection, A.Id_Ativo IdSecurity, A.Done " +
                                " , CASE " +
                                    " WHEN IdInstrument=16 THEN -NESTDB.dbo.FCN_Calcula_PU(A.Id_Ativo,[Avg Price Trade]/100.00,Data_Trade,Data_Trade)*A.Done " +
                                    " WHEN IdInstrument=3 THEN [Avg Price Trade]*A.Done " +
                                    " WHEN IdInstrument=4 THEN[Avg Price Trade]*A.Done*C.RoundLot " +
                                    " ELSE [Avg Price Trade]*A.Done/C.RoundLot " +
                                    " END AS AmtTrade " +
                                " FROM  NESTDB.dbo.VW_ORDENS_ALL A   " +
                                " INNER JOIN  NESTDB.dbo.VW_PortAccounts B " +
                                " ON A.id_account=B.Id_Account  " +
                                " INNER JOIN  NESTDB.dbo.Tb001_Securities C " +
                                " ON A.Id_Ativo=C.IdSecurity " +
                                " WHERE A.Data_Trade >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                                " AND A.Status_Ordem <> 4  " +
                                " AND A.Id_Port_Type=1  " +
                                " ) X " +
                                " GROUP BY DataTrade,IdPortfolio,IdBook,IdSection,IdSecurity, CASE WHEN Done>0 THEN 1 ELSE -1 END ";

                DataTable dt = curConn.Return_DataTable(SQLString);

                lock (CalcPortfolios)
                {
                    foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                    {
                        lock (curRTCalc)
                        {
                            foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                            {
                                curPositionItem.FlagTrade = false;
                            }
                        }
                    }

                    foreach (DataRow curRow in dt.Rows)
                    {
                        RealTimeCalc curRTCalc;

                        if (CalcPortfolios.TryGetValue(int.Parse(curRow["IdPortfolio"].ToString()), out curRTCalc))
                        {
                            PositionItem curPositionItem = curRTCalc.curCalc.AllPositions.Find(
                                                            delegate(PositionItem testPositionItem)
                                                            {
                                                                if (testPositionItem.DateNow == DateTime.Parse(curRow["DataTrade"].ToString()) &&
                                                                    testPositionItem.IdPortfolio == int.Parse(curRow["IdPortfolio"].ToString()) &&
                                                                    testPositionItem.IdBook == int.Parse(curRow["IdBook"].ToString()) &&
                                                                    testPositionItem.IdSection == int.Parse(curRow["IdSection"].ToString()) &&
                                                                    testPositionItem.IdSecurity == int.Parse(curRow["IdSecurity"].ToString()))
                                                                { return true; }

                                                                else { return false; }
                                                            }
                                                            );

                            if (curPositionItem != null)
                            {
                                double Done = NestDLL.Utils.ParseToDouble(curRow["Done"].ToString());
                                double AmtTrade = NestDLL.Utils.ParseToDouble(curRow["AmtTrade"].ToString());

                                if (Done > 0)
                                {

                                    if (Math.Round(curPositionItem.QuantBoughtTrade, 6) != Math.Round(Done, 6))
                                    {
                                        curPositionItem.QuantBoughtTrade = Done;
                                        curPositionItem.AmtBoughtTrade = AmtTrade;
                                    }
                                }
                                else
                                {
                                    if (Math.Round(curPositionItem.QuantSoldTrade, 6) != Math.Round(Done, 6))
                                    {
                                        curPositionItem.QuantSoldTrade = Done;
                                        curPositionItem.AmtSoldTrade = AmtTrade;
                                    }
                                }

                                curPositionItem.FlagTrade = true;
                            }
                            else
                            {
                                FlagNewPositionFound = true;
                            }
                        }
                    }

                    lock (CalcPortfolios)
                    {
                        foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                        {
                            lock (curRTCalc.curCalc.AllPositions)
                            {
                                foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                                {
                                    if (curPositionItem.FlagTrade == false)
                                    {
                                        curPositionItem.QuantBoughtTrade = 0;
                                        curPositionItem.AmtBoughtTrade = 0;
                                        curPositionItem.QuantSoldTrade = 0;
                                        curPositionItem.AmtSoldTrade = 0;
                                    }
                                }
                            }
                        }
                    }
                }
                GlobalVars.Instance.LastRTQuantityTimeTaken = DateTime.Now.Subtract(StartTime);
                UpdateQuantitiesRunning = false;
            }
        }

        public void UpdateQuantityRT()
        {
            lock (CalcPortfolios)
            {
                foreach (RealTimeCalc curRTCalc in CalcPortfolios.Values)
                {
                    lock (curRTCalc.curCalc.AllPositions)
                    {
                        foreach (PositionItem curPositionItem in curRTCalc.curCalc.AllPositions)
                        {
                            if (curPositionItem.RTQuantityFlag)
                            {
                                UpdatePosition(curPositionItem);
                                curPositionItem.RTQuantityFlag = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Timers

        public void tmrRefreshSlow_Tick(object sender, EventArgs e)
        {
            if (Started && Initialized)
            {
                if (!RefreshSlowRunning)
                {
                    RefreshSlowRunning = true;

                    bool BaseTableChanged = false;
                    bool SecurityChanged = false;
                    bool PriceChanged = false;

                    if (CheckBaseTableChange()) { BaseTableChanged = true; }
                    else if (CheckSecurityChange()) { SecurityChanged = true; }
                    else if (CheckPriceChange()) { PriceChanged = true; }

                    if (BaseTableChanged || SecurityChanged || PriceChanged || FlagNewPositionFound || DateTime.Now.Subtract(GlobalVars.Instance.LastRecalcAll).TotalMinutes >= 3)
                    {
                        if (RecalcAllThread.ThreadState != System.Threading.ThreadState.Running)
                        {
                            RecalcAllThread = new Thread(ReCalcAll);
                            RecalcAllThread.Name = "RecalcAllThread";
                            RecalcAllThread.Start();
                        }

                        FlagNewPositionFound = false;
                    }

                    RefreshSlowRunning = false;
                }
            }
        }

        public void tmrRefreshFast_Tick(object sender, EventArgs e)
        {
            if (Started && Initialized)
            {
                if (!RefreshFastRunning)
                {
                    RefreshFastRunning = true;

                    UpdateRTPriceDB();

                    RefreshFastRunning = false;
                }
            }
        }

        public void tmrRefreshQuantity_Tick(object sender, EventArgs e)
        {
            if (Started && Initialized)
            {
                if (!RefreshQntRunning)
                {
                    RefreshQntRunning = true;

                    CheckQuantityChange();
                    UpdateQuantityRT();

                    RefreshQntRunning = false;
                }
            }
        }

        public void tmrCheckConnection_Tick(object sender, EventArgs e)
        {
            if (Started && Initialized)
            {
                if (DateTime.Now.Subtract(DataDistTimer).Seconds > 30)
                {
                    if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 20)
                    {
                        GlobalVars.Instance.MarketDataConnected = false;
                        if (ProcessMktData) Connect();
                    }
                }
            }
        }

        #endregion
    }

    internal class LBMarketDataItem : MarketDataItem
    {
        public Sources Source { get; set; }
        public string SubscribeTicker { get; set; }
        public bool Subscribed { get; set; }
        public bool LastUpdated { get; set; }
        public bool BidUpdated { get; set; }
        public bool AskUpdated { get; set; }
        public bool SettUpdated { get; set; }
    }
}
