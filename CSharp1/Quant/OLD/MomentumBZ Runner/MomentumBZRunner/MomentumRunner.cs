using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 
using System.Threading;
using NestQuant.Common;

namespace MomentumBZRunner
{
    public class MomentumRunner
    {

        #region Properties

        private int _ID_Ticker_Template;
        public int ID_Ticker_Template
        {
            get { return _ID_Ticker_Template; }
            set { _ID_Ticker_Template = value; }
        }

        private int _ID_Ticker_Composite;
        public int ID_Ticker_Composite
        {
            get { return _ID_Ticker_Composite; }
            set { _ID_Ticker_Composite = value; }
        }

        private DateTime _IniDate;
        public DateTime IniDate
        {
            get { return _IniDate; }
            set { _IniDate = value; }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private int _Window;
        public int Window
        {
            get { return _Window; }
            set { _Window = value; }
        }

        private int _MedianWindow;
        public int MedianWindow
        {
            get { return _MedianWindow; }
            set { _MedianWindow = value; }
        }

        private double _StrategyNAV;
        public double StrategyNAV
        {
            get { return _StrategyNAV; }
            set { _StrategyNAV = value; }
        }

        private double _TargetVol;
        public double TargetVol
        {
            get { return _TargetVol; }
            set { _TargetVol = value; }
        }

        private bool _autoSendOrders = false;
        public bool AutoSendOrders
        {
            get { return _autoSendOrders; }
            set { _autoSendOrders = value; }
        }

        private int _LotSize = 100;
        public int LotSize
        {
            get { return _LotSize; }
            set { _LotSize = value; }
        }



        #endregion

        //private FIXConn fixConn;
        private SymConn symConn = new SymConn();

        SortedDictionary<int, int> CurrentPosition = new SortedDictionary<int, int>();
        SortedDictionary<int, int> FuturePosition = new SortedDictionary<int, int>();

        SortedDictionary<string, int> SymbolToTicker = new SortedDictionary<string, int>();
        SortedDictionary<int, string> TickerToSymbol = new SortedDictionary<int, string>();
        SortedDictionary<int, int> StagedOrders = new SortedDictionary<int, int>();
        SortedDictionary<int, int> RoundedStagedOrders = new SortedDictionary<int, int>();

        Mutex StagingMutex = new Mutex();
        Mutex PositionMutex = new Mutex();

        private MomentumInfo Info;
        private MomentumBZ Momentum;
                
        	        
        public MomentumRunner()
        {
            //fixConn = new FIXConn(@"T:\Log\FIX\MomentumBZ\config\MomentumBZConfig.cfg");            
        }

        public void RunMomentum()
        {
            Info = SetupMomentumInfo();
            Momentum = new MomentumBZ(Info);
            Momentum.UpdatePositions(CurrentPosition);
            Momentum.SendOrdersEvent += new EventHandler<OrderEventArgs>(Momentum_SendOrdersEvent);
            Momentum.StartStrategy();
            symConn.OnData += new EventHandler(symConn_OnData);
            symConn.Connect();
            SubscribeTickers();

        }

        void symConn_OnData(object sender, EventArgs e)
        {
            SymDataEventArgs symEvent = (SymDataEventArgs)e;

            Dictionary<int, double> priceChange = new Dictionary<int, double>();

            if (symEvent.Value[1] != 0)
            {
                int TickerID = 0;
                if (!SymbolToTicker.TryGetValue(symEvent.Ticker, out TickerID))
                {
                    throw new Exception();
                }

                priceChange.Add(TickerID, symEvent.Value[1]);

                Momentum.UpdatePrices(priceChange);
            }            
        }

        void Momentum_SendOrdersEvent(object sender, OrderEventArgs e)
        {
            foreach (OrderEventArgs.OrderInfo oinfo in e.OrderList)
            {
                //fixConn.sendOrder(oinfo.ID_Ticker, oinfo.Quantity, oinfo.Price);
                                
                StageOrder(oinfo);                

                PositionMutex.WaitOne();
                if (FuturePosition.ContainsKey(oinfo.ID_Ticker))
                {
                    FuturePosition[oinfo.ID_Ticker] += oinfo.Quantity;
                }
                else
                {
                    FuturePosition.Add(oinfo.ID_Ticker, oinfo.Quantity);
                }
                PositionMutex.ReleaseMutex();
            }

            Momentum.UpdatePositions(FuturePosition);
        }

        private void StageOrder(OrderEventArgs.OrderInfo oinfo)
        {           
            StagingMutex.WaitOne();
            if (StagedOrders.ContainsKey(oinfo.ID_Ticker))
            {
                StagedOrders[oinfo.ID_Ticker] += oinfo.Quantity;

                int staged = StagedOrders[oinfo.ID_Ticker];
                int rounded = 0;
                if (Math.Abs(staged) >= 100)
                {
                    rounded = RoundLot(staged, LotSize);
                }

                RoundedStagedOrders[oinfo.ID_Ticker] = rounded;
            }
            else
            {
                StagedOrders.Add(oinfo.ID_Ticker, oinfo.Quantity);

                int staged = StagedOrders[oinfo.ID_Ticker];
                int rounded = 0;
                if (Math.Abs(staged) >= 100)
                {
                    rounded = RoundLot(staged, LotSize);
                }

                RoundedStagedOrders.Add(oinfo.ID_Ticker, rounded);
            }
            StagingMutex.ReleaseMutex();

            if (AutoSendOrders)
            {
                SendStagedOrders(false);
            }
        }

        private int RoundLot(int Quantity, int Lot)
        {
            int midLot = Convert.ToInt32(Math.Round(Lot / 2.0));
            int remaining = Quantity % Lot;
            int truncated = Quantity - remaining;
            int rounded = (Math.Abs(remaining) < midLot ? truncated : truncated + Math.Sign(remaining)*Lot);

            return rounded;
        }

        public void SendStagedOrders(bool auction)
        {
            StagingMutex.WaitOne();

            Dictionary<int, int> remainingStaged = new Dictionary<int, int>();

            foreach (KeyValuePair<int, int> roundedOrder in RoundedStagedOrders)
            {
                if (roundedOrder.Value != 0 && (roundedOrder.Key == 5469
                                                ))
                {
                    double price = (auction ? -2.0 : -1.0);

                    //fixConn.sendOrder(roundedOrder.Key, roundedOrder.Value, price);
                }

                PositionMutex.WaitOne();
                if (CurrentPosition.ContainsKey(roundedOrder.Key))
                {
                    CurrentPosition[roundedOrder.Key] += roundedOrder.Value;
                }
                else
                {
                    CurrentPosition.Add(roundedOrder.Key, roundedOrder.Value);
                }
                PositionMutex.ReleaseMutex();


                int remaining = StagedOrders[roundedOrder.Key] - roundedOrder.Value;

                if (Math.Abs(remaining) > 0)
                {
                    remainingStaged.Add(roundedOrder.Key, remaining);
                }
            }

            StagedOrders.Clear();
            RoundedStagedOrders.Clear();

            foreach (KeyValuePair<int, int> remOrder in remainingStaged)
            {
                StagedOrders.Add(remOrder.Key, remOrder.Value);

                int staged = StagedOrders[remOrder.Key];
                int rounded = 0;

                if (Math.Abs(staged) >= 100)
                {
                    rounded = RoundLot(staged, LotSize);
                }

                RoundedStagedOrders.Add(remOrder.Key, rounded);
            }

            StagingMutex.ReleaseMutex();

            PositionXML.SavePositions("MomentumBZ", CurrentPosition);
        }

        public DataTable ToDataTableStagedOrders()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TICKER");
            dt.Columns.Add("QUANTITY");
            dt.Columns.Add("ROUNDQTY");
            dt.Columns.Add("DIFF");

            StagingMutex.WaitOne();

            foreach (KeyValuePair<int, int> stagedOrder in StagedOrders)
            {
                DataRow line = dt.NewRow();
                line[0] = TickerToSymbol[stagedOrder.Key];
                line[1] = stagedOrder.Value;
                line[2] = RoundedStagedOrders[stagedOrder.Key];
                line[3] = RoundedStagedOrders[stagedOrder.Key] - stagedOrder.Value;

                dt.Rows.Add(line);
            }

            StagingMutex.ReleaseMutex();

            return dt;
        }

        private MomentumInfo SetupMomentumInfo()
        {
            SortedDictionary<int, MomentumSectorInfo> MomentumSectorsInfo = new SortedDictionary<int,MomentumSectorInfo>();
            SortedDictionary<int, double> Weights = new SortedDictionary<int,double>();
            SortedDictionary<int, int> CurrentPosition;
            SortedDictionary<int, int> TickersBySector;
            double AdjustFactor = 1;         
                        
            StrategyInfo Info = new StrategyInfo();
            Info.ID_Ticker_Template = ID_Ticker_Template;
            Info.ID_Ticker_Composite = ID_Ticker_Composite;
            Info.IniDate = IniDate;
            Info.EndDate = EndDate;
            Info.Window = Window;
            Info.FillStrategyInfo();
                        

            int refBar = Info.BenchmarkPerformance.DateRowCount - 1;           
            
            for (int i = 0; i < Info.StratWeights.ValueColumnCount; i++)
            {
                int ID_Sector = Info.StratWeights.GetValueColumnID(i);
                
                Weights.Add(ID_Sector, Info.StratWeights.GetValue(refBar, i));

                MomentumSectorsInfo.Add(ID_Sector, SetupMomentumSectorInfo(Info.Sectors[ID_Sector], refBar));
            }

            TickersBySector = Info.TickersBySector;
            CurrentPosition = GetCurrentPosition();

            int bmStDevCol = Info.BenchmarkPerformance.GetCustomColumnIndex("BMROLLDEV");
            double bmStDev = Info.BenchmarkPerformance.GetCustomValue(refBar, bmStDevCol);

            if (bmStDev != 0)
            {
                AdjustFactor = (TargetVol / (bmStDev * Utils.NORMSINV95));
                AdjustFactor = (AdjustFactor < 1 ? AdjustFactor : 1);
            }

            MomentumInfo momentumInfo = new MomentumInfo(MomentumSectorsInfo,
                                                         Weights,
                                                         CurrentPosition,
                                                         TickersBySector,
                                                         AdjustFactor,
                                                         StrategyNAV);


            return momentumInfo;
        }

        private SortedDictionary<int, int> GetCurrentPosition()
        {
            string SQLExpression = "SELECT [ID TICKER], [POSITION] FROM NESTRT.DBO.TB000_POSICAO_ATUAL " +
                                   "WHERE [ID BOOK] = 3 AND [ID SECTION] = 54 AND [ID PORTFOLIO] = 43";

            using (NestConn conn = new NestConn())
            {
                conn.openConn();

                DataTable dt = conn.ExecuteDataTable(SQLExpression);

                foreach (DataRow dr in dt.Rows)
                {
                    CurrentPosition.Add(Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]));
                    FuturePosition.Add(Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]));
                }

            }


            
            return CurrentPosition;
        }

        private MomentumSectorInfo SetupMomentumSectorInfo(SectorInfo Info, int refBar)
        {
            SortedDictionary<int, double> DayPrices = new SortedDictionary<int, double>();
            SortedDictionary<int, double> Weights = new SortedDictionary<int, double>();
            SortedDictionary<int, double> WindowIniPrice = new SortedDictionary<int, double>();

            for (int i = 0; i < Info.Price.ValueColumnCount; i++)
            {
                try
                {
                    int ID_Ticker = Info.Price.GetValueColumnID(i);
                    double LastPrice = Info.Price.GetValue(refBar, i);
                    double Weight = Info.Weight.GetValue(refBar, i);
                    double WindowPrice = Info.Price.GetValue(refBar - (Window - 1), i);

                    Weights.Add(ID_Ticker, Weight);
                    DayPrices.Add(ID_Ticker, LastPrice);
                    WindowIniPrice.Add(ID_Ticker, WindowPrice);
                }
                catch
                {
                    throw new System.NotImplementedException();
                }
            }

            double[] SignalReference = new double[MedianWindow];

            int curCol = Info.PerfSummary.GetCustomColumnIndex("ROLLPERF");

            for (int i = refBar - MedianWindow + 2; i <= refBar; i++)
            {
                SignalReference[i - (refBar - MedianWindow + 2)] = Info.PerfSummary.GetCustomValue(i, curCol);
            }

            SignalReference[SignalReference.Length - 1] = 0;

            MomentumSectorInfo momentumSectorInfo = new MomentumSectorInfo(Info.ID_Ticker_Composite,
                                                                           DayPrices,
                                                                           Weights,
                                                                           WindowIniPrice,
                                                                           SignalReference);
            return momentumSectorInfo;
        }

        private void SubscribeTickers()
        {
            foreach (int ticker in Info.TickersBySector.Keys)
            {
                string SQLExpression = "SELECT COD_REUTERS FROM NESTDB.DBO.TB119_CONVERT_SIMBOLOS WHERE ID_ATIVO = " + ticker.ToString();

                string symbol = "";

                using (NestConn conn = new NestConn())
                {
                    symbol = conn.Execute_Query_String(SQLExpression);
                }

                symbol = symbol.Split('.')[0];

                if (symbol != "")
                {
                    symConn.Subscribe(symbol);
                    SymbolToTicker.Add(symbol, ticker);
                    TickerToSymbol.Add(ticker, symbol);
                }
                
            }            
        }

        public void Stop()
        {            
            symConn.Dispose();
            //fixConn.Dispose();
        }
    }
}
