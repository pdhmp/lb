using System;
using System.Collections.Generic;
using System.Text;
using NestSim.Common;
using System.Data.SqlClient;
using System.IO;
using NestSimDLL;

namespace NestSim.HS
{
    public class HeadAndShoulders
    {
        private int IdSIM;
        private int _Id_Ticker_Template = 1073;
                      
        public struct T3Fields
        {
            public int bar;
            public double value;
            public double LowBand1;
            public double LowBand2;
            public double HighBand;
        }

        public Price_Table SignalTable;
        
        private Asset Asset;
        private ZigZag ZigZag;
        private SortedDictionary<int, T3Fields> T3Candidates;
        private SortedDictionary<int, TradeSignal> TradeList;

        #region Strategy Parameters

        double HurdleRate;
        double bandPercent;
        double Horizontal;
        int sideFactor;

        #endregion

        #region Exit Parameters

        private int exitBar;
        private double trailingStop;
        private double gainLimit;
        private double stopLimit;
        private int exitStrategy;

        #endregion
                
        #region P4 Flags
        
        private bool flgHigh;
        private bool flgLow;
        private bool flgExit;
        private bool flgT2;
        private bool flgP2;
        private bool flgP4;
        private bool flgT4;
        private bool flgP4T2P2;
        private bool flgP4mP3;

        private bool restartFlags;

        #endregion

        
        public HeadAndShoulders(int Id_Ticker, double _HurdleRate, double _bandPercent, double _horizontal, 
                                int _sideFactor, int _IdSIM, int _exitStrategy, int _exitBar, double _gainLimit, double _stopLimit)
        {
            
            HurdleRate = _HurdleRate;
            bandPercent = _bandPercent;
            Horizontal = _horizontal;
            sideFactor = _sideFactor;
            
            gainLimit = _gainLimit;
            stopLimit = _stopLimit;
            exitStrategy = _exitStrategy;
            exitBar = _exitBar;

            IdSIM = _IdSIM;

            Asset = new Asset(Id_Ticker, sideFactor);

            restartFlags = true;

            ZigZag = new ZigZag(Asset, HurdleRate);
            T3Candidates = new SortedDictionary<int, T3Fields>();
            TradeList = new SortedDictionary<int, TradeSignal>();
                        
        }

        public void calculateHS(DateTime beginDate, DateTime endDate, bool intraday)
        {
            Asset.loadHistoric(beginDate, endDate,intraday);
            ZigZag.historicZigZag(intraday);
            historicT3(intraday);
            historicP4(intraday);

            
            //Create signal table from signal list
            SignalTable = new Price_Table("Ticker " + Asset.Id_Ticker + " Signals", _Id_Ticker_Template ,beginDate,endDate);

            int columnIndex = SignalTable.AddValueColumn(Asset.Id_Ticker);

            foreach (TradeSignal ts in TradeList.Values)
            {
                SignalTable.SetValue(SignalTable.GetDateIndex(ts.Date), columnIndex, (float)ts.Signal);
            }




            //printHS();
        }
        
        #region T3 Candidates

        public void historicT3(bool intraday)
        {
            for (int curCounter = 3; curCounter < ZigZag.Peaks.Count; curCounter++)
            {
                ZigZag.Vertex p1;
                ZigZag.Vertex p2;
                ZigZag.Vertex p3;
                ZigZag.Vertex t1;
                ZigZag.Vertex t2;
                ZigZag.Vertex t3;

                int id_P1, id_P2, id_P3;
                int id_T1, id_T2, id_T3;

                id_P1 = 0;
                id_P2 = 0;
                id_P3 = 0;
                id_T1 = 0;
                id_T2 = 0;
                id_T3 = 0;


                id_P3 = curCounter - 1;
              

                if (!ZigZag.Peaks.TryGetValue(id_P3, out p3))
                {
                    Log.AddLogEntry("Unable to get P3 to calculate T3 candidates", "C:\\Logs\\HeadAndShouldersLog.txt");
                }
                else
                {
                    id_P2 = curCounter - 2;
                    id_P1 = curCounter - 3;
                    id_T3 = p3.lastVertex + 1;
                    id_T2 = p3.lastVertex;
                    id_T1 = p3.lastVertex - 1;
                }
                
                
                if (!(ZigZag.Peaks.TryGetValue(id_P1, out p1) && ZigZag.Peaks.TryGetValue(id_P2, out p2) &&
                      ZigZag.Peaks.TryGetValue(id_P3, out p3) && ZigZag.Troughs.TryGetValue(id_T1, out t1) &&
                      ZigZag.Troughs.TryGetValue(id_T2, out t2) && ZigZag.Troughs.TryGetValue(id_T3, out t3)))
                {
                    Log.AddLogEntry("Unable to get peaks and troughs to calculate T3 candidates", "C:\\Logs\\HeadAndShouldersLog.txt");
                }
                else
                {
                    if (!intraday||(
                        p1.date.Date == p2.date.Date && p2.date.Date == p3.date.Date &&
                        t1.date.Date == t2.date.Date && t2.date.Date == t3.date.Date &&
                        p1.date.Date == t1.date.Date &&
                        !(p1.lastVertex == -1 ||p2.lastVertex == -1|| p3.lastVertex == -1||
                          t1.lastVertex == -1 ||t2.lastVertex == -1|| t3.lastVertex == -1)))
                    {
                        T3Fields result;
                        int status = findT3Candidates(id_P1, id_P2, id_P3,
                                                      id_T1, id_T2, id_T3, 
                                                      out result);

                        if (status == 1)
                        {
                            try
                            {
                                T3Candidates.Add(id_T3, result);
                            }
                            catch
                            {
                                Log.AddLogEntry("Unable to insert T3 candidate", "C:\\Logs\\HeadAndShouldersLog.txt");
                            }
                        }
                    }
                }
            }
        }
     
        public int findT3Candidates(int id_P1, int id_P2, int id_P3, int id_T1, int id_T2, int id_T3,
                                     out T3Fields t3CandidateFields)
        {
            ZigZag.Vertex p1;
            ZigZag.Vertex p2;
            ZigZag.Vertex p3;
            ZigZag.Vertex t1;
            ZigZag.Vertex t2;
            ZigZag.Vertex t3;

            t3CandidateFields = new T3Fields();

            if (!(ZigZag.Peaks.TryGetValue(id_P1, out p1) && ZigZag.Peaks.TryGetValue(id_P2, out p2) &&
                  ZigZag.Peaks.TryGetValue(id_P3, out p3) && ZigZag.Troughs.TryGetValue(id_T1, out t1) &&
                  ZigZag.Troughs.TryGetValue(id_T2, out t2) && ZigZag.Troughs.TryGetValue(id_T3, out t3)))
            {
                Log.AddLogEntry("Unable to get peaks and troughs to calculate T3 candidates", "C:\\Logs\\HeadAndShouldersLog.txt");
                return -1;
            }

           //Calculate T3 rules
            if ((p2.value > p1.value) && (t2.value > t1.value)) //Rule 1: Previous uptrend
            {
                if (p3.value > p2.value) //Rule 2: Head and shoulder (left shoulder)
                {
                    if (t3.value < ((t2.value + p2.value) / 2) && t3.value > t1.value) //Rule 3
                    {
                        if (p3.value > ((t2.value + p2.value) / 2)) //Rule 4
                        {
                            int bandWidth;
                            double bandTop;
                            double bandBottom;
                            double bandSize;
                            double bandHigh;
                            double bandLow1;
                            double bandLow2;

                            bandWidth = t3.bar - t2.bar;
                            bandTop = p3.value;
                            bandBottom = t2.value + (t3.value - t2.value) / (t3.bar - t2.bar) * (p3.bar - t2.bar);
                            bandSize = bandTop - bandBottom;
                            bandHigh = bandTop - bandSize * bandPercent;
                            bandLow1 = t2.value + bandSize * bandPercent;
                            bandLow2 = t3.value + bandSize * bandPercent;

                            t3CandidateFields.HighBand = bandHigh;
                            t3CandidateFields.LowBand1 = bandLow1;
                            t3CandidateFields.LowBand2 = bandLow2;
                            t3CandidateFields.bar = t3.bar;
                            t3CandidateFields.value = t3.value;

                            return 1;

                        }
                    }                    
                }
            }

            return 0;
        }

        #endregion
        
        #region P4 Candidates

        public void setFlagsP4(bool _restartFlags)
        {
            if (restartFlags)
            {
                flgHigh = false;
                flgLow = false;
                flgExit = false;
                flgT2 = false;
                flgP2 = true;
                flgP4 = false;
                flgT4 = false;
                flgP4T2P2 = false;
                flgP4mP3 = false;

                restartFlags = false;
            }
        }

        public bool getNeckIncr(int id_T2, int id_T3, out double _neckIncr)
        {
            ZigZag.Vertex t2;
            ZigZag.Vertex t3;

            _neckIncr = 0;

            if (!(ZigZag.Troughs.TryGetValue(id_T2, out t2) && ZigZag.Troughs.TryGetValue(id_T3, out t3)))
            {
                Log.AddLogEntry("Unable to get T2 and T3 to calculate neck increment", "C:\\Logs\\HeadAndShouldersLog.txt");
                return false;
            }

            _neckIncr = (t3.value - t2.value) / (t3.bar - t2.bar);

            return true;

        }

 
        public void historicP4(bool intraday)
        {
            for (int curCounter = 3; curCounter < ZigZag.Peaks.Count; curCounter++)
            {
                T3Fields T3;

                ZigZag.Vertex p3;

                int peakCounter, troughCounter;

                peakCounter = curCounter - 1;
                troughCounter = 0;

                if (!ZigZag.Peaks.TryGetValue(peakCounter, out p3))
                {
                    Log.AddLogEntry("Unable to get P3 to calculate P4 candidates", "C:\\Logs\\HeadAndShouldersLog.txt");
                }
                else
                {
                    troughCounter = p3.lastVertex + 1;


                    if (T3Candidates.TryGetValue(troughCounter, out T3))
                    {
                        ZigZag.Vertex P4 = new ZigZag.Vertex();
                        ZigZag.Vertex T4 = new ZigZag.Vertex();
                        ZigZag.Vertex _P4 = new ZigZag.Vertex();
                        ZigZag.Vertex _T4 = new ZigZag.Vertex();

                        int curPos = T3.bar;

                        restartFlags = true;

                        setFlagsP4(restartFlags);

                        if (sideFactor == -1)
                        {
                            P4.value = -1000;
                            T4.value = -1000;
                        }
                        else
                        {
                            P4.value = 0;
                            T4.value = 0;
                        }

                        ZigZag.Vertex checkT3;

                        ZigZag.Troughs.TryGetValue(troughCounter, out checkT3);


                        bool InTrade = false;
                        int exitTrade = 0;

                        double inValue = 0;


                        while (!flgExit && curPos < Asset.MaxBar)
                        {
                            Asset.Price assetDate;

                            Asset.barPrice.TryGetValue(curPos, out assetDate);

                            if (!intraday || (checkT3.date.Date == assetDate.date.Date))
                            {

                                if (!InTrade)
                                {
                                    if(findP4Candidates(curPos, 
                                                     peakCounter - 2, peakCounter - 1, peakCounter, P4,
                                                     troughCounter - 2, troughCounter - 1, troughCounter, 
                                                     T4, T3, out _P4, out _T4, 
                                                     intraday))
                                    {
                                        Asset.Price inBar;

                                        if (!Asset.barPrice.TryGetValue(curPos, out inBar))
                                        {
                                            Log.AddLogEntry("Unable to get in_bar to create trade signal", "C:\\Logs\\HeadAndShouldersLog.txt");
                                        }
                                        
                                        TradeSignal inTradeSignal = new TradeSignal(inBar.date, Asset.Id_Ticker, sideFactor * (-1));
                                        
                                        inTradeSignal.insertDB(IdSIM);

                                        TradeList.Add(curPos, inTradeSignal);

                                        InTrade = true;
                                        exitTrade = curPos;
                                        inValue = inBar.value;
                                                                                

                                    }

                                    P4 = _P4;
                                    T4 = _T4;

                                }
                                else
                                {
                                    Asset.Price outBar;

                                    if (!Asset.barPrice.TryGetValue(curPos, out outBar))
                                    {
                                        Log.AddLogEntry("Unable to get out_bar to create trade signal", "C:\\Logs\\HeadAndShouldersLog.txt");
                                    }

                                    if (checkExit(exitStrategy,curPos,outBar.value,exitTrade,inValue))
                                    {
                                        TradeSignal outTradeSignal = new TradeSignal(outBar.date, Asset.Id_Ticker, sideFactor);
                                        outTradeSignal.insertDB(IdSIM);

                                        TradeList.Add(curPos, outTradeSignal);

                                        flgExit = true;
                                        InTrade = false;
                                    }                                    
                                }                                
                            }
                            else
                            {
                                if (InTrade)
                                {
                                    Asset.Price outBar;

                                    if (!Asset.barPrice.TryGetValue(curPos - 1, out outBar))
                                    {
                                        Log.AddLogEntry("Unable to get out_bar to create trade signal", "C:\\Logs\\HeadAndShouldersLog.txt");
                                    }
                                    else
                                    {
                                        TradeSignal outTradeSignal = new TradeSignal(outBar.date, Asset.Id_Ticker, sideFactor);
                                        outTradeSignal.insertDB(IdSIM);
                                    }


                                }
                                flgExit = true;
                            }
                            curPos++;
                        }

                    }
                }
            }
        }
        
        public bool findP4Candidates(int bar, int id_P1, int id_P2, int id_P3, ZigZag.Vertex _P4,
                                     int id_T1, int id_T2, int id_T3, ZigZag.Vertex _T4,
                                     T3Fields T3Fields, out ZigZag.Vertex P4, out ZigZag.Vertex T4,
                                     bool intraday)
        {
            double neckIncr = 0;
            double neckLine;
            double curValue;

            bool retValue = false;

            ZigZag.Vertex p1;
            ZigZag.Vertex p2;
            ZigZag.Vertex p3;
            ZigZag.Vertex t1;
            ZigZag.Vertex t2;
            ZigZag.Vertex t3;

            P4 = _P4;
            T4 = _T4;

            Asset.Price barPrice;

            DateTime curDate;

            if (!(ZigZag.Peaks.TryGetValue(id_P1, out p1) && ZigZag.Peaks.TryGetValue(id_P2, out p2) &&
                  ZigZag.Peaks.TryGetValue(id_P3, out p3) && ZigZag.Troughs.TryGetValue(id_T1, out t1) &&
                  ZigZag.Troughs.TryGetValue(id_T2, out t2) && ZigZag.Troughs.TryGetValue(id_T3, out t3)))
            {
                Log.AddLogEntry("Unable to get peaks and troughs to calculate P4 candidates", "C:\\Logs\\HeadAndShouldersLog.txt");
                return retValue;
            }
            
            if (!getNeckIncr(id_T2, id_T3, out neckIncr))
            {
                Log.AddLogEntry("Unable to get neck increment", "C:\\Logs\\HeadAndShouldersLog.txt");
                return retValue;
            }

            if (!Asset.barPrice.TryGetValue(bar, out barPrice))
            {
                Log.AddLogEntry("Unable to get asset´s price on bar", "C:\\Logs\\HeadAndShouldersLog.txt");
                return retValue;
            }
      


            flgP4T2P2 = false;
            neckLine = t3.value + neckIncr * (bar - t3.bar);

            if (intraday)
            {
                curValue = barPrice.value;
            }
            else
            {
                curValue = barPrice.value / Asset.LastIndex * Asset.LastPrice;
            }
            
            curDate = barPrice.date;

            if (sideFactor * (curValue / t3.value - 1) > HurdleRate && !flgT4)
            {
                flgP4 = true;
                if (curValue >= P4.value)
                {
                    P4.value = curValue;
                    P4.bar = bar;
                    T4.value = P4.value;
                }
                if (P4.value >= p3.value)
                {
                    flgP4mP3 = true;
                }
            }

            if (flgP4)
            {
                if (P4.value > T3Fields.LowBand2 && P4.value > t3.value * (1 + HurdleRate))
                {
                    flgLow = true;
                }
                
                if (t2.value < (t3.value + P4.value) / 2)
                {
                    flgT2 = true;
                }
                else
                {
                    flgT2 = false;
                }

                if (p2.value > (t3.value + P4.value) / 2)
                {
                    flgP2 = false;
                }
                else
                {
                    flgP2 = true;
                }
                
            }

            if (P4.value > T3Fields.HighBand)
            {
                flgExit = true;
                return retValue;
            }

            if (flgP4)
            {
                if (sideFactor * (curValue / P4.value - 1) < -HurdleRate)
                {
                    flgT4 = true;
                    if (curValue < T4.value)
                    {
                        T4.value = curValue;
                    }
                }
            }

            if (flgT4)
            {
                if(sideFactor*(curValue / T4.value - 1) > HurdleRate)
                {
                    flgExit = true;
                    return retValue;
                }
            }

            if (curValue < neckLine && flgP4)
            {
                if (flgLow && flgT2 && !flgP2 && !flgP4mP3)
                {
                    if (P4.value > (t2.value + p2.value) / 2)
                    {
                        if ((P4.bar - p3.bar) <= (double)(p3.bar - p2.bar) * Horizontal &&
                            (p3.bar - p2.bar) <= (double)(P4.bar - p3.bar) * Horizontal)
                        {
                            retValue = true;
                            //flgExit = true;
                        }
                    }
                }
                else
                {
                    flgExit = true;
                }
            }

            return retValue;
        }

        #endregion
                
        private bool checkExit(int exitStrategy, int bar, double barValue, int inBar, double inValue)
        {
            bool checkExit = false;
            switch (exitStrategy)
            {
                case 0: //Max Bar
                    if (bar >= inBar + exitBar)
                    {
                        checkExit = true;
                    }
                    break;
                case 1: //Gain & Stop
                    if ((sideFactor * (barValue / inValue - 1) <= -gainLimit) ||
                       (sideFactor * (barValue / inValue - 1) >= stopLimit))
                    {
                        checkExit = true;
                    }
                    if (bar >= inBar + exitBar)
                    {
                        checkExit = true;
                    }
                    break;
                case 2: //Trailing Stop
                    if (sideFactor * (barValue / trailingStop - 1) >= stopLimit)
                    {
                        checkExit = true;
                    }
                    if (barValue < trailingStop)
                    {
                        trailingStop = barValue;
                    }
                    if (bar >= inBar + exitBar)
                    {
                        checkExit = true;
                    }
                    break;
                default:
                    if (bar >= inBar + exitBar)
                    {
                        checkExit = true;
                    }
                    break;                 

            }

            return checkExit;
        }

        public List<string[]> printHS()
        {
            List<string[]> print = new List<string[]>();

            StreamWriter DebugFile = null;

            if (DebugFile == null)
            {
                DebugFile = new StreamWriter("C:\\Debug\\Ticker_" + Asset.Id_Ticker.ToString() + ".txt", false);
            }

            foreach (KeyValuePair<int,Asset.Price> assetPrice in Asset.barPrice)
            {
                string[] aux = new string[6];

                aux[0] = assetPrice.Value.date.ToString("yyyy-MM-dd HH:mm:ss");
                double auxValue = assetPrice.Value.value;
                aux[1] = auxValue.ToString();
                aux[2] = "";
                aux[3] = "";
                aux[4] = "";
                aux[5] = "";

                TradeSignal trade;

                foreach (KeyValuePair<int, ZigZag.Vertex> peak in ZigZag.Peaks)
                {
                    if (peak.Value.bar == assetPrice.Key)
                    {
                        aux[2] = "Peak";
                    }
                }

                foreach (KeyValuePair<int, ZigZag.Vertex> trough in ZigZag.Troughs)
                {
                    if (trough.Value.bar == assetPrice.Key)
                    {
                        aux[3] = "Trough";

                        T3Fields fields;

                        if (T3Candidates.TryGetValue(trough.Key, out fields))
                        {
                            aux[4] = "T3 Candidate";
                        }
                    }
                }

                if (TradeList.TryGetValue(assetPrice.Key, out trade))
                {
                    aux[5] = trade.Signal.ToString();
                }

                print.Add(aux);

                if (Asset.Id_Ticker == 28)
                {
                    try
                    {
                        DebugFile.Write(aux[0] + "; " + aux[1] + "; " + aux[2] + "; " + aux[3] + "; " + aux[4] + "; " + aux[5] + "\r\n");
                    }
                    catch { }
                }
            }

            DebugFile.Dispose();
            DebugFile = null;
            GC.Collect();
    
            return print;
    
        }

    }
}
