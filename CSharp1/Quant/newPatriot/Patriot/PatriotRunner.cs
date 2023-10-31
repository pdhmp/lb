using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using NestDLL;

namespace Patriot
{
    public class PatriotRunner
    {
        public event EventHandler OnFinished;

        #region Parameters
        private double _Trigger1;
        private double _Trigger2;
        private double _minGL;
        private double _minPL;
        private int _numThreads; //Number of cores available to process data

        public double Trigger1
        {
            get { return _Trigger1; }
            set { _Trigger1 = value; }
        }
        public double Trigger2
        {
            get { return _Trigger2; }
            set { _Trigger2 = value; }
        }
        public double minGL
        {
            get { return _minGL; }
            set { _minGL = value; }
        }
        public double minPL
        {
            get { return _minPL; }
            set { _minPL = value; }
        }
        public int numThreads
        {
            get { return _numThreads; }
            set { _numThreads = value; }
        }

        private int _Window1;
        private int _Window2;
        private int _Window3;

        public int Window1
        {
            get { return _Window1; }
            set { _Window1 = value; }
        }
        public int Window2
        {
            get { return _Window2; }
            set { _Window2 = value; }
        }
        public int Window3
        {
            get { return _Window3; }
            set { _Window3 = value; }
        }

        private DateTime _curDate;
        public DateTime curDate
        {
            get { return _curDate; }
            set { _curDate = value; }
        }

        private DateTime _refDate;
        public DateTime refDate
        {
            get { return _refDate; }
            set { _refDate = value; }
        }

        private double _Last30Perc = 0.2;
        public double Last30Perc
        {
            get { return _Last30Perc; }
            set { _Last30Perc = value; }
        }

        private double _CloseAucPerf = 0.15;
        public double CloseAucPerf
        {
            get { return _CloseAucPerf; }
            set { _CloseAucPerf = value; }
        }

        private double _SizeLimit = 3000000;
        public double SizeLimit
        {
            get { return _SizeLimit; }
            set { _SizeLimit = value; }
        }

        private double _SizeMultiplier = 1; 
        public double SizeMultiplier
        {
            get { return _SizeMultiplier; }
            set { _SizeMultiplier = value; }
        }

        private double _DateGain = 0;
        public double DateGain
        {
            get { return _DateGain; }
            set { _DateGain = value; }
        }

        private double _Devol = 0.975;
        public double Devol
        {
            get { return _Devol; }
            set { _Devol = value; }
        }

        private double _GrossSize = 0;
        public double GrossSize
        {
            get { return _GrossSize; }
            set { _GrossSize = value; }
        }

        private int _QtySymbols = 0;
        public int QtdSymbols
        {
            get { return _QtySymbols; }
            set { _QtySymbols = value; }
        }

        #endregion


        private long PairsProcessed = 0;


        private List<PatriotModel> runningModels = new List<PatriotModel>();
        private SortedDictionary<double, string> IdSecurityToTicker = new SortedDictionary<double, string>();

        public DateTime StartTime;

        public PatriotRunner() { }

        public void LoadPairs()
        {
            StartTime = DateTime.Now;

            refDate = PatriotData.Instance.GetPrevDate(curDate);

            File.Delete(@"C:\Quant\Strategies\Patriot\Patriot_Trade_T_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + _curDate.ToString("yyyyMMdd") + ".xls");
            File.Delete(@"C:\Quant\Strategies\Patriot\Patriot_Excluded_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + _curDate.ToString("yyyyMMdd") + ".xls");
            File.Delete(@"C:\Quant\Strategies\Patriot\Patriot_All_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + _curDate.ToString("yyyyMMdd") + ".xls");

            //Create model for each thread
            for (int i = 0; i < _numThreads; i++)
            {
                PatriotModel curModel = new PatriotModel(this,i);
                runningModels.Add(curModel);
            }

            //Get tickers to process
            DataTable tempTable = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                string SQLExpression = "SELECT distinct A.IDSECURITY, B.NESTTICKER FROM " +
                                       "(SELECT ID_TICKER_COMPONENT AS IDSECURITY FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION (NOLOCK) " +
                                       "WHERE ID_TICKER_COMPOSITE = 169168 AND DATE_REF = '"+_refDate.ToString("yyyyMMdd")+"') A " +
                                       "LEFT JOIN NESTDB.DBO.TB001_SECURITIES B (NOLOCK) " +
                                       "ON A.IDSECURITY = B.IDSECURITY " +
                                       "ORDER BY A.IDSECURITY";
                 
                /*
                string SQLExpression = " SELECT IDSECURITY,NESTTICKER FROM NESTDB.DBO.TB001_SECURITIES" +
                                       " WHERE IDSECURITY IN (82936,29,1572,35,36,1677,86,88,89,1678,1084,5526,132,595,145," +
                                       " 1237,27903,1682,151,1582,4660,185,210,5790,228,240,243,253,256," +
                                       " 259,260,16053,279,280,293,15010,30187,313,315,1065,323,326,5528," +
                                       " 1231,1687,347,809,381,382,383,384,390,393,415,80873,1754,1074,448," +
                                       " 443,444,1219,456,458,476,5469,1584,483,64154,511,23616,5529,1694," +
                                       " 1411,1695,532,536,1061,1816,547,29080,556,1075,561,1,587,599," +
                                       " 90266,83068,612,1407,632,654,14960,664,5037,1701,710,715,717," +
                                       " 1077,720,750,752,766,767,768,775,793,800,801,806,3,831)";
                                                                               
                */
                tempTable = curConn.Return_DataTable(SQLExpression);
            }

            double[] curTickers = new double[tempTable.Rows.Count];

            for (int i = 0; i < curTickers.Length; i++)
            {
                curTickers[i] = NestDLL.Utils.ParseToDouble(tempTable.Rows[i][0]);
                IdSecurityToTicker.Add(curTickers[i], tempTable.Rows[i][1].ToString());
            }

            PatriotData.Instance.LoadTickers(curTickers);
            
            //Distribute pairs among threads
            Int64 pairCounter = 0;
            for (int i = 0; i < curTickers.Length; i++)
            {
                for (int j = i + 1; j < curTickers.Length; j++)
                {
                    int curThreadID = (int)(pairCounter % _numThreads);
                    if (!runningModels[curThreadID].AddPair(curTickers[i], curTickers[j]))
                    {
                        throw new Exception("Unable to add pair to started thread");
                    }
                    pairCounter++;
                }
            }
        }
        public void StartRunner()
        {
            foreach (PatriotModel curModel in runningModels)
            {
                curModel.OnFinished += new EventHandler(curModel_OnFinished);
                curModel.StartModel();
            }

        }

        private int FinishedThreads = 0;

        private void curModel_OnFinished(object sender, EventArgs e)
        {
            PatriotModel curModel = (PatriotModel)sender;
            PairsProcessed += curModel.PairsProcessed;

            FinishedThreads++;

            if (runningModels.Count == FinishedThreads)
            {
                GenerateBasket();

                Console.WriteLine("All threads finished. Pairs processed: " + PairsProcessed + ". Elapsed time: "
                               + TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(StartTime.Ticks)));
                
                if (OnFinished != null)
                {
                    OnFinished(this, null);
                }
            }
        }

        SortedDictionary<double, double> BuyOrders;
        SortedDictionary<double, double> SellOrders;

        private void GenerateBasket()
        {
            SortedDictionary<double, int> Security1Count = new SortedDictionary<double, int>();
            SortedDictionary<double, int> Security2Count = new SortedDictionary<double, int>();

            List<TradingPairs> toTrade = new List<TradingPairs>();

            foreach (PatriotModel curModel in runningModels)
            {
                foreach (TradingPairs curPair in curModel.StrategyPairs)
                {
                    if (curPair.Trade)
                    {
                        if (!Security1Count.ContainsKey(curPair.Security1)) { Security1Count.Add(curPair.Security1, 0); }
                        if (!Security2Count.ContainsKey(curPair.Security2)) { Security2Count.Add(curPair.Security2, 0); }
                        Security1Count[curPair.Security1]++;
                        Security2Count[curPair.Security2]++;

                        toTrade.Add(curPair);
                    }
                }
            }

            BuyOrders = new SortedDictionary<double, double>();
            SellOrders = new SortedDictionary<double, double>();

            foreach (TradingPairs curPair in toTrade)
            {
                if (curPair.Security1 == 1655 || curPair.Security2 == 1655)
                {
                    int a = 0;
                }

                double Sec1Max = Math.Min(curPair.PairPreSize, curPair.Sec1MaxSize / Security1Count[curPair.Security1]);
                curPair.PairFinalSize = Math.Min(Sec1Max, curPair.Sec2MaxSize / Security2Count[curPair.Security2]);

                double sellSec1 = 0;
                double buySec2 = 0;
                double sec1Net = curPair.PairFinalSize;
                double sec2Net = curPair.PairFinalSize;

                if (SellOrders.TryGetValue(curPair.Security1, out sellSec1))
                {
                    SellOrders[curPair.Security1] -= Math.Min(sellSec1, sec1Net);
                    sec1Net-= Math.Min(sellSec1,sec1Net);

                    if (SellOrders[curPair.Security1] == 0)
                    {
                        SellOrders.Remove(curPair.Security1);
                    }                    
                }

                if (sec1Net > 0)
                {
                    if (BuyOrders.ContainsKey(curPair.Security1))
                    {
                        BuyOrders[curPair.Security1] += sec1Net;
                    }
                    else
                    {
                        BuyOrders.Add(curPair.Security1, sec1Net);
                    }
                }

                if (BuyOrders.TryGetValue(curPair.Security2, out buySec2))
                {
                    BuyOrders[curPair.Security2] -= Math.Min(buySec2, sec2Net);
                    sec2Net -= Math.Min(buySec2, sec2Net);

                    if (BuyOrders[curPair.Security2] == 0)
                    {
                        BuyOrders.Remove(curPair.Security2);
                    }
                }

                if (sec2Net > 0)
                {
                    if (SellOrders.ContainsKey(curPair.Security2))
                    {
                        SellOrders[curPair.Security2] += sec2Net;
                    }
                    else
                    {
                        SellOrders.Add(curPair.Security2, sec2Net);
                    }
                }                             
            }

            if (curDate < DateTime.Today)
            {
                CalculatePerformance();
            }
            else
            {
                GenerateOutputFile();
            }
        }
        private void GenerateOutputFile()
        {
            StreamWriter swOrders = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_ORDERS_" + curDate.ToString("yyyyMMdd") + ".xls", false);

            foreach (KeyValuePair<double, double> kvp in BuyOrders)
            {
                swOrders.WriteLine("BUY\t" + kvp.Key + "\t" + kvp.Value);
            }
            foreach (KeyValuePair<double, double> kvp in SellOrders)
            {
                swOrders.WriteLine("SELL\t" + kvp.Key + "\t" + kvp.Value);
            }

            swOrders.Close();
        }
        private void CalculatePerformance()
        {
            StreamWriter swOrders = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_Perf_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + curDate.ToString("yyyyMMdd") + ".xls", false);

            //FillTest();

            foreach (KeyValuePair<double, double> kvp in BuyOrders)
            {
                double vwap = PatriotData.Instance.GetValue(kvp.Key, 331, curDate, curDate, true)[1];                
                double last = PatriotData.Instance.GetValue(kvp.Key, 1, curDate, curDate, true)[1];
                
                //double vwap = vwapTest[kvp.Key];
                //double last = lastTest[kvp.Key];

                bool vwapError = false;
                bool lastError = false;

                if (vwap == 0 || double.IsInfinity(vwap) || double.IsNaN(vwap)) { vwapError = true; }
                if (last == 0 || double.IsInfinity(last) || double.IsNaN(last)) { lastError = true; }

                if (vwapError && !lastError) { vwap = last; }
                else if (!vwapError && lastError) { last = vwap; }
                else if (vwapError && lastError) { last = 1; vwap = 1; }

                double gain = ((last / vwap - 1) - 2 * (0.00025 + 0.005 * (1 - _Devol))) * kvp.Value;
                
                _DateGain += gain;
                _GrossSize += Math.Abs(kvp.Value);
                _QtySymbols++;

                //swOrders.WriteLine("BUY\t" + IdSecurityToTicker[kvp.Key] +"\t" + kvp.Key + "\t" + kvp.Value+ "\t" +vwap+ "\t" +last);

            }
            foreach (KeyValuePair<double, double> kvp in SellOrders)
            {
                double vwap = PatriotData.Instance.GetValue(kvp.Key, 331, curDate, curDate, true)[1];
                double last = PatriotData.Instance.GetValue(kvp.Key, 1, curDate, curDate, true)[1];

                //double vwap = vwapTest[kvp.Key];
                //double last = lastTest[kvp.Key];

                bool vwapError = false;
                bool lastError = false;

                if (vwap == 0 || double.IsInfinity(vwap) || double.IsNaN(vwap)) { vwapError = true; }
                if (last == 0 || double.IsInfinity(last) || double.IsNaN(last)) { lastError = true; }

                if (vwapError && !lastError) { vwap = last; }
                else if (!vwapError && lastError) { last = vwap; }
                else if (vwapError && lastError) { last = 1; vwap = 1; }

                double gain = ((vwap/last - 1) - 2 * (0.00025 + 0.005 * (1 - _Devol))) * kvp.Value;                

                _DateGain += gain;
                _GrossSize += Math.Abs(kvp.Value);
                _QtySymbols++;

                //swOrders.WriteLine("SELL\t" + IdSecurityToTicker[kvp.Key] + "\t" + kvp.Key + "\t" + kvp.Value + "\t" + vwap + "\t" + last);
            }

            //swOrders.WriteLine("");
            //swOrders.WriteLine("Day P/L: \t" + _DateGain);

            swOrders.Close();
        }

        SortedDictionary<double, double> vwapTest = new SortedDictionary<double, double>();
        SortedDictionary<double, double> lastTest = new SortedDictionary<double, double>();

        private void FillTest()
        {
            vwapTest.Add(259,31.8029);
            vwapTest.Add(347,24.2332);
            vwapTest.Add(381,25.0442);
            vwapTest.Add(443,27.999);
            vwapTest.Add(595,34.9107);
            vwapTest.Add(632,25.0606);
            vwapTest.Add(717,28.2043);
            vwapTest.Add(1677,16.3605);
            vwapTest.Add(23616,29.344);
            vwapTest.Add(132,34.8551);
            vwapTest.Add(29080,14.4743);
            vwapTest.Add(547,57.9982);
            vwapTest.Add(5037,25.7669);
            vwapTest.Add(1077,10.5579);
            vwapTest.Add(210,31.9413);
            vwapTest.Add(228,29.1817);
            vwapTest.Add(35,46.4354);
            vwapTest.Add(280,14.2137);
            vwapTest.Add(30187,12.9384);
            vwapTest.Add(243,33.7666);
            vwapTest.Add(654,10.1971);
            vwapTest.Add(720,8.9396);
            vwapTest.Add(390, 19.5689);
            vwapTest.Add(383, 13.2769);
            vwapTest.Add(15010, 9.0556);
            vwapTest.Add(5469, 3.7985);
            vwapTest.Add(36, 57.3989);
            vwapTest.Add(1061, 25.561);
            vwapTest.Add(831, 18.9525);
            vwapTest.Add(1695, 34.5697);
            vwapTest.Add(710, 8.1649);
            vwapTest.Add(384, 15.6917);
            vwapTest.Add(1084, 6.25);
            vwapTest.Add(476, 26.4091);
            lastTest.Add(259, 31.95);
            lastTest.Add(347, 24.18);
            lastTest.Add(381, 25.11);
            lastTest.Add(443, 28.08);
            lastTest.Add(595, 34.97);
            lastTest.Add(632, 25.4);
            lastTest.Add(717, 28.39);
            lastTest.Add(1677, 16.33);
            lastTest.Add(23616, 29.48);
            lastTest.Add(132, 35);
            lastTest.Add(29080, 14.38);
            lastTest.Add(547, 58.05);
            lastTest.Add(5037, 25.75);
            lastTest.Add(1077, 10.36);
            lastTest.Add(210, 32.15);
            lastTest.Add(228, 29.37);
            lastTest.Add(35, 46.89);
            lastTest.Add(280, 14.2);
            lastTest.Add(30187, 12.99);
            lastTest.Add(243, 33.7);
            lastTest.Add(654, 10.17);
            lastTest.Add(720, 8.87);
            lastTest.Add(390, 19.51);
            lastTest.Add(383, 13.33);
            lastTest.Add(15010, 9.1);
            lastTest.Add(5469, 3.81);
            lastTest.Add(36, 57.83);
            lastTest.Add(1061, 25.59);
            lastTest.Add(831, 18.99);
            lastTest.Add(1695, 34.72);
            lastTest.Add(710, 8.13);
            lastTest.Add(384, 15.63);
            lastTest.Add(1084, 6.32);
            lastTest.Add(476, 26.45);

            




            

        }

        private volatile object excludedSync = new object();
        private volatile object tradeSync = new object();
        private volatile object allSync = new object();
        public void PrintTradingPairs(TradingPairs curPair)
        {
            if (curPair.Trade)
            {
                lock (tradeSync)
                {
                    StreamWriter swTrade = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_Trade_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + _curDate.ToString("yyyyMMdd") + ".xls", true);
                    swTrade.WriteLine(IdSecurityToTicker[curPair.Security1] + "\t" +
                                      IdSecurityToTicker[curPair.Security2] + "\t" +
                                      curPair.PL1 + "\t" + curPair.GL1 + "\t" + curPair.PValue1 + "\t" + (curPair.Side1 == 1 ? "Compra" : "Vende") + "\t" + curPair.InvalidCounter + "\t \t" +
                                      curPair.PL2 + "\t" + curPair.GL2 + "\t" + curPair.PValue2 + "\t" + (curPair.Side2 == 1 ? "Compra" : "Vende") + "\t" +
                                      curPair.PL3 + "\t" + curPair.GL3 + "\t" + curPair.PValue3 + "\t" + (curPair.Side3 == 1 ? "Compra" : "Vende"));

                    swTrade.Close();
                }
            }
            else
            {
                lock (excludedSync)
                {
                    StreamWriter swExcluded = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_Excluded_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + _curDate.ToString("yyyyMMdd") + ".xls", true);
                    swExcluded.WriteLine(IdSecurityToTicker[curPair.Security1] + "\t" +
                                      IdSecurityToTicker[curPair.Security2] + "\t" +
                                      curPair.PL1 + "\t" + curPair.GL1 + "\t" + curPair.PValue1 + "\t" + (curPair.Side1 == 1 ? "Compra" : "Vende") + "\t" + curPair.InvalidCounter + "\t \t" +
                                      curPair.PL2 + "\t" + curPair.GL2 + "\t" + curPair.PValue2 + "\t" + (curPair.Side2 == 1 ? "Compra" : "Vende") + "\t" +
                                      curPair.PL3 + "\t" + curPair.GL3 + "\t" + curPair.PValue3 + "\t" + (curPair.Side3 == 1 ? "Compra" : "Vende"));

                    swExcluded.Close();
                }
            }

            lock (allSync)
            {
                StreamWriter swAll = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_All_" + _Trigger1.ToString() + "_" + _Trigger2.ToString() + "_" + _curDate.ToString("yyyyMMdd") + ".xls", true);
                swAll.WriteLine(IdSecurityToTicker[curPair.Security1] + "\t" +
                                  IdSecurityToTicker[curPair.Security2] + "\t" +
                                  curPair.PL1 + "\t" + curPair.GL1 + "\t" + curPair.PValue1 + "\t" + (curPair.Side1 == 1 ? "Compra" : "Vende") + "\t" + curPair.InvalidCounter + "\t \t" +
                                  curPair.PL2 + "\t" + curPair.GL2 + "\t" + curPair.PValue2 + "\t" + (curPair.Side2 == 1 ? "Compra" : "Vende") + "\t" +
                                  curPair.PL3 + "\t" + curPair.GL3 + "\t" + curPair.PValue3 + "\t" + (curPair.Side3 == 1 ? "Compra" : "Vende"));

                swAll.Close();
            }            
        }
    }
}
