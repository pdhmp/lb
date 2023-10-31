using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NestDLL;

namespace ReutersTickOneMinBarsGenerator
{
    public partial class Form1 : Form
    {
        AVG_5_Days_Calculator AVG_5_Days = new AVG_5_Days_Calculator();
        //QSEG_Calculator QSEG = new QSEG_Calculator(312, DateTime.Today.AddDays(-5), 22);
        QSEG_Calculator QSEG = new QSEG_Calculator(new DateTime(2000,01,01));

        List<AuctionData> OpenAuctionList = new List<AuctionData>();
        Dictionary<string, Dictionary<DateTime, Dictionary<int, OneMinBar>>> IntradayBarsList = new Dictionary<string, Dictionary<DateTime, Dictionary<int, OneMinBar>>>();
        List<AuctionData> CloseAuctionList = new List<AuctionData>();
        List<AuctionData> IntradayAuctionList = new List<AuctionData>();

        SortedDictionary<DateTime, TimeSpan> OpenTimes = new SortedDictionary<DateTime, TimeSpan>();
        SortedDictionary<DateTime, TimeSpan> CloseTimes = new SortedDictionary<DateTime, TimeSpan>();
        SortedDictionary<DateTime, double> TotalMinutes = new SortedDictionary<DateTime, double>();

        TimerCallback tmrCallBackStart;
        
        System.Threading.Timer tmrStart;
                
        string Ticker = "";
        string Date = "";
        string Status = "";

        private volatile object ScreenSync = new object();
        
        public Form1()
        {
            InitializeComponent();
            tmrCallBackStart = new TimerCallback(Run);
            long RunTime = (long)(new TimeSpan(23, 59, 00) - new TimeSpan(DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second)).TotalMilliseconds;
            tmrStart = new System.Threading.Timer(tmrCallBackStart,null,1000,System.Threading.Timeout.Infinite);
            //tmrStart = new System.Threading.Timer(tmrCallBackStart, null, RunTime, System.Threading.Timeout.Infinite);
            timer1.Start();
        }

        private void Run(object obj)
        {

            ProcessFolder(@"C:\EUROPE\DATA");

            //QSEG.CalculateHistoricalIndexes(new DateTime(2006, 01, 01));
            //QSEG.CalculateHistoricaNewIndexes(new DateTime(2007, 01, 01));            

            //ProcessFolder(@"R:\RTICK\OneMinuteBars\OnShore\Unprocessed\");

           
            lock (ScreenSync)
            {
                Status = "Calculating 5 Days Average Prices...";
                Ticker = "";
                Date = "";
            }

            AVG_5_Days.CalculateAverage();

            lock (ScreenSync)
            {
                Status = "Calculating QSEGS...";
                Ticker = "";
                Date = "";
            }

            DateTime date = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
            QSEG.CalculateAllIndexes(date);            

            lock (ScreenSync)
            {
                Status = "Ended";
                Ticker = "";
                Date = "";
            }
        }

        private void ProcessFolder(string curFolder)
        {
            bool NewDataInserted = false;
            string[] fileEntries = Directory.GetFiles(curFolder);

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains(".csv"))
                {
                    if (fileName.Contains("OnShore"))
                    {
                        LoadOpenTimes(2);
                        ProcessOnShoreFile(fileName);
                    }
                    else if (fileName.Contains("OffShore") || fileName.Contains("Europe"))
                    {
                        //TODO inserir regra de horários offshore
                        ProcessOffShroreFile(fileName);
                    }
                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                    //File.Move(fileName, fileName.Replace("Unprocessed2", "Processed"));

                    PrintDataToFile(fileName);
                    OpenAuctionList.Clear();
                    CloseAuctionList.Clear();
                    IntradayBarsList.Clear();
                    IntradayAuctionList.Clear();

                    OpenTimes.Clear();
                    CloseTimes.Clear();
                    TotalMinutes.Clear();
                }
            }
            //InsertFileInDatabase(@"R:\RTICK\OneMinuteBars\DataBase Files\Unprocessed\");*/
            if (curFolder.Contains("OnShore"))
            {
                if (InsertFileInDatabase(@"C:\temp\RTICK\DataBaseFiles\OnShoreFiles\Unprocessed\"))
                {
                    NewDataInserted = true;
                }
            }
            else if (curFolder.Contains("OffShore"))
            {
                if (InsertFileInDatabase(@"C:\temp\RTICK\DataBaseFiles\OffShoreFiles\Unprocessed\"))
                {
                    NewDataInserted = true;
                }
            }
            //Generate_Data(false);
            if (NewDataInserted)
            {
                if (curFolder.Contains("OnShore"))
                {
                    Generate_Data();                
                }
                else if (curFolder.Contains("OffShore"))
                {
                    Generate_Data_Germany();
                }
            }
        }

        private void ProcessOnShoreFile(string curFileName)
        {
            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream(@"R:\RTICK\Daily Requests\Unprocessed\Requests\Uncompressed\Uns.fonseca@nestinvestimentos.com.br-Daily_Request-N26538123.csv", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream(@"C:\Temp\ABCB4_TEST.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            // ==================== INITIALIZING VARIABLES =======================================

            // ====================== Sequence Control

            bool OpenAuctionEnded = false;
            bool CloseAuctionStarted = false;
            bool OpenAuctionStarted = false;
            bool IntradayStarted = false;
            bool IntradayEnded = false;            
            bool TickerStateChanged = false;
            bool MinuteChanged = false;
            bool FirstSpreadFixed = false;

            // ====================== Accumulators and Prev Values
            TimeSpan OpenTime = new TimeSpan(0, 0, 0);
            TimeSpan CloseTime = new TimeSpan(0, 0, 0);
            Int64 cumVolume = 0;
            double cumValue = 0;
            double cumSpread = 0;
            Int64 TotalQuotesArrived = 0;
            double MinSpread = 0;
            double MaxSpread = 0;
            Int64 prevCumVolume = 0;
            double prevCumValue = 0;
            Int64 closeVolume = 0;
            double closeValue = 0;

            string prevTicker = "";
            double prevPrice = 0;
            Int64 prevVolume = 0;

            
            DateTime prevDate = new DateTime(1900, 01, 01);
            TimeSpan prevTime = new TimeSpan(0, 0, 0);

            int cumTotalTrades = 0;

            // ====================== Current Values
            DateTime curDate = new DateTime();
            DateTime ExpirationDate = new DateTime();
            TimeSpan curTime;
            int curTimeShift = 0;
            double curPrice;
            Int64 curVolume;
            double curSpread = 0;
            String curState;
            String curType;
            String curTicker;

            double curBid = 0;
            double curAsk = 0;

            OneMinBar curBar = new OneMinBar();
            AuctionData curOpenData = new AuctionData();
            AuctionData curCloseData =  new AuctionData();

            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            tempLine = sr.ReadLine();
            string[] headers = tempLine.Split(',');

            for (int i = 0; i < headers.Length; i++)
            {
                switch (headers[i])
                {
                    case "#RIC": posTicker = i;
                        break;
                    case "Date[G]": posDate = i;
                        break;
                    case "Time[G]": posTime = i;
                        break;
                    case "GMT Offset": posTimeShift = i;
                        break;
                    case "Type": posType = i;
                        break;
                    case "Price": posLast = i;
                        break;
                    case "Volume": posVolume = i;
                        break;
                    case "Bid Price": quoteBid = i;
                        break;
                    case "Ask Price": quoteAsk = i;
                        break;
                    case "Qualifiers": posState = i;
                        break;
                }
            }

            string[] curValues;
            try
            {
                while ((tempLine = sr.ReadLine()) != null)
                {                    
                    curValues = tempLine.Split(',');
                  
                    curTicker = curValues[0];

                    lock (ScreenSync)
                    {
                        Ticker = curTicker;
                        Status = "Loading " + curTicker + " Bars...";
                    }

                    try
                    {
                        if (curValues[posDate] != "")
                        {
                            curValues[posDate] = curValues[posDate].Substring(0, 4) + "/" + curValues[posDate].Substring(4, 2) + "/" + curValues[posDate].Substring(6, 2);
                        }
                        curDate = new DateTime(1900, 01, 01); if (curValues[posDate] != "") curDate = DateTime.Parse(curValues[posDate]);
                    }                        
                    catch
                    {
                        int i = 0;
                    }                    

                    lock (ScreenSync)
                    {
                        Date = curDate.ToString("dd/MM/yyyy");
                    }

                    if (curDate == new DateTime(2007, 05, 08))
                    { }

                    curTime = new TimeSpan(0, 0, 0); if (curValues[posTime] != "") curTime = TimeSpan.Parse(curValues[posTime]).Add(new TimeSpan(curTimeShift,0,0));
                    curTimeShift = 0; if (curValues[posTimeShift] != "") curTimeShift = int.Parse(curValues[posTimeShift]);
                    curType = curValues[posType];
                    curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".", ","));
                    curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                    curState = curValues[posState];

                    if (curTicker == "QUAL3.SA" || curTicker == "ABRE11.SA") //&& curTime > new TimeSpan(20, 30, 00) && curType == "Trade")
                    { }
                    else
                    { }


                    //TODO Alterar para original
                    if (curState.Contains("ODD") || curState.Contains("241[IRGCOND]") || ((((curVolume % 100) != 0) || (curVolume == 0)) && !curTicker.Contains("BOVA11")) )
                    {
                        curState = "ODD";
                    }

                    if (prevDate != curDate || prevTicker != curTicker || TickerStateChanged || (new TimeSpan(curTime.Hours, curTime.Minutes, 0) - new TimeSpan(prevTime.Hours, prevTime.Minutes, 0)) >= new TimeSpan(0, 0, 1, 0))
                    {
                        if(prevTicker != curTicker)
                        {
                            IntradayBarsList.Add(curTicker, new Dictionary<DateTime,Dictionary<int,OneMinBar>>());
                            IntradayBarsList[curTicker].Add(curDate, new Dictionary<int, OneMinBar>());
                        }

                        if(prevDate != curDate && !IntradayBarsList[curTicker].ContainsKey(curDate))                        
                        {
                            IntradayBarsList[curTicker].Add(curDate, new Dictionary<int,OneMinBar>());
                        }

                        #region Reinitilize 1

                        // ======================= MINUTE CHANGED =======================
                        if ((new TimeSpan(curTime.Hours, curTime.Minutes, 0) - new TimeSpan(prevTime.Hours, prevTime.Minutes, 0)) >= new TimeSpan(0, 0, 1, 0))
                        {
                            MinuteChanged = true;
                            curBar.MinSpread = MinSpread;
                            curBar.MaxSpread = MaxSpread;
                            curBar.AverageSpread = cumSpread / TotalQuotesArrived;
                            curBar.CloseSpread = curSpread;
                            curBar.VWAP = curBar.Value / curBar.Volume;
                            curBar.Close = prevPrice;
                            curBar.EndTimeGMT = prevTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
                            curBar.EndTimeLocal = prevTime;

                            if (curBar.RegressiveMinute < 31)
                            { }
                        }

                        // ======================= DATE OR TICKER CHANGED =======================

                        if (curTicker.Contains("KLBN4.SA"))
                        { }

                        if (prevDate != curDate || prevTicker != curTicker)
                        {
                            if (curCloseData.Ticker != null)
                            {
                                curCloseData.Value = closeValue;
                                curCloseData.Volume = closeVolume;
                                curCloseData.Price = prevPrice;
                                curCloseData.EndTimeGMT = prevTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
                                curCloseData.EndTimeLocal = prevTime;
                            }
                            else if(curBar.Ticker != null && curBar.Volume > 0)
                            {
                                curBar.MinSpread = MinSpread;
                                curBar.MaxSpread = MaxSpread;
                                curBar.AverageSpread = cumSpread / TotalQuotesArrived;
                                curBar.CloseSpread = curSpread;
                                curBar.VWAP = curBar.Value / curBar.Volume;
                                curBar.Close = prevPrice;
                                curBar.EndTimeGMT = prevTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
                                curBar.EndTimeLocal = prevTime;

                                IntradayBarsList[curBar.Ticker][prevDate].Add((int)curBar.Minute, curBar);

                                curBar = new OneMinBar();
                                MinuteChanged = false;
                            }
                            
                            /*
                            if (curFileName.Contains("IND"))
                            {
                                if (prevTicker != curTicker)
                                {
                                    string ExpirationDateQuery = "SELECT Expiration " +
                                                                    "FROM dbo.Tb001_Securities (NOLOCK) " +
                                                                    "WHERE ReutersTicker LIKE '%" + curTicker + "%' ";
                                    using (newNestConn conn = new  )
                                    {
                                        ExpirationDate = conn.Return_DateTime(ExpirationDateQuery);
                                        ExpirationDate = ExpirationDate.AddDays(ExpirationDate.DayOfWeek == DayOfWeek.Monday || ExpirationDate.DayOfWeek == DayOfWeek.Wednesday || ExpirationDate.DayOfWeek == DayOfWeek.Tuesday? -5:-2);
                                    }
                                }
                            }
                            */             
                        }

                        #endregion

                        if (OpenTime != new TimeSpan(0, 0, 0) && CloseTime != new TimeSpan(0, 0, 0))
                        {

                            if (curOpenData.Ticker != null && curOpenData.Volume != 0)
                            {
                                OpenAuctionList.Add(curOpenData);
                                curOpenData = new AuctionData();
                            }
                            if (curCloseData.Ticker != null && curCloseData.Volume != 0)
                            {
                                CloseAuctionList.Add(curCloseData);
                                curCloseData = new AuctionData();

                                if (OpenAuctionList.Count != CloseAuctionList.Count)
                                {
                                    int i = 0;
                                }
                            }
                            if (MinuteChanged)
                            {
                                if (curBar.Ticker != null && curBar.Volume != 0)
                                {
                                    if (curBar.Minute > 410)
                                    { }
                                    /*
                                    if (IntradayBarsList[curBar.Ticker][curDate].ContainsKey((int)curBar.Minute))
                                    {
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].AskTrades += curBar.AskTrades;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].BidTrades += curBar.BidTrades;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].TotalTrades += curBar.TotalTrades;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Close = curBar.Close;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Volume += curBar.Volume;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Value += curBar.Value;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].VWAP = (IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].VWAP *
                                                                                                            IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Volume +
                                                                                                            curBar.Volume * curBar.VWAP) /
                                                                                                            (IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Volume + curBar.Volume);
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].High = IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].High > curBar.High ?
                                                                                                            IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].High : curBar.High;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Low = IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Low < curBar.Low ?
                                                                                                            IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].Low : curBar.Low;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].EndTimeGMT = curBar.EndTimeGMT;
                                        IntradayBarsList[curBar.Ticker][curDate][(int)curBar.Minute].EndTimeLocal = curBar.EndTimeLocal;
                                    }
                                    else
                                    {*/
                                        IntradayBarsList[curBar.Ticker][curDate].Add((int)curBar.Minute, curBar);
                                    //}
                                }
                            }
                        }

                        #region Reinitilize 2

                        if (prevDate != curDate || prevTicker != curTicker)
                        {
                            // Reinitialize                          

                            curOpenData = new AuctionData();
                            curCloseData = new AuctionData();
                            curBar = new OneMinBar();

                            OpenAuctionStarted = false;
                            OpenAuctionEnded = false;
                            CloseAuctionStarted = false;
                            IntradayEnded = false;
                            IntradayStarted = false;
                            MinuteChanged = false;
                            TickerStateChanged = false;                            

                            closeVolume = 0;
                            closeValue = 0;
                            cumVolume = 0;
                            cumValue = 0;
                            curBid = 0;
                            curAsk = 0;
                            prevDate = curDate;
                            prevTicker = curTicker;
                            prevVolume = 0;
                            prevCumValue = 0;
                            prevCumVolume = 0;
                            cumTotalTrades = 0;

                        }


                        if (MinuteChanged)
                        {
                            curBar = new OneMinBar();
                            curBar.Ticker = curTicker;
                            curBar.Date = curDate;
                            MinuteChanged = false;
                            IntradayStarted = false;
                            IntradayEnded = false;
                            FirstSpreadFixed = false;
                            cumSpread = 0;
                            TotalQuotesArrived = 0;                            
                        }

                        TickerStateChanged = false;
                    }

                        #endregion

                    if (curType == "Trade" && curState != "ODD")
                    {
                        cumVolume += curVolume;
                        cumValue += curVolume * curPrice;
                        if (curPrice != 0)
                        { }
                        cumTotalTrades++;
                    }

                    if (OpenTimes.ContainsKey(curDate))
                    {
                        OpenTime = OpenTimes[curDate];
                        CloseTime = CloseTimes[curDate];
                    }
                    else
                    {
                        //Caso não haja preço de índice Ibov na base 
                        //suponha que os horários de abertura do dia em 
                        //questão foram os mesmos do dia anterior

                        OpenTimes.Add(curDate, OpenTimes.Last().Value);
                        CloseTimes.Add(curDate, CloseTimes.Last().Value);
                        TotalMinutes.Add(curDate, TotalMinutes.Last().Value);

                        OpenTime = OpenTimes[curDate];
                        CloseTime = CloseTimes[curDate];
                    }
                    
                    if (OpenTime != new TimeSpan(0, 0, 0))                    
                    {
                        if (curType == "Trade" && curState != "ODD")
                        {
                            #region Calculating

                            // =========================  SEARCH FOR OPENING AUCTION ==========================================  //

                            if (curDate == new DateTime(2008, 12, 16) && curTicker.Contains("G9"))
                            { }

                            if (!OpenAuctionEnded)
                            {
                                if (OpenAuctionStarted && (curPrice != prevPrice))
                                {
                                    curOpenData.Ticker = curTicker;
                                    curOpenData.Date = curDate;
                                    curOpenData.Volume = prevCumVolume;
                                    curOpenData.Value = prevCumValue;
                                    curOpenData.Price = prevPrice;
                                    curOpenData.EndTimeGMT = prevTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
                                    curOpenData.EndTimeLocal = prevTime;
                                    curOpenData.Minute = prevTime.Subtract(new TimeSpan(OpenTimes[curDate].Hours, OpenTimes[curDate].Minutes, 0)).TotalMinutes;
                                    curOpenData.TotalTrades = --cumTotalTrades;

                                    OpenAuctionEnded = true;
                                    PrintOpenTime(curTicker, curTime, curDate);
                                    TickerStateChanged = true;
                                }

                                if (cumVolume > 0 && prevVolume == 0)
                                {
                                    if ((curAsk > curBid) && ((curPrice == curAsk) || (curPrice == curBid)))
                                    {
                                        OpenAuctionEnded = true;

                                        PrintOpenTime(curTicker, curTime, curDate);
                                    }
                                    else
                                    {
                                        OpenAuctionStarted = true;
                                        curOpenData.StartTimeGMT = curTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));;
                                        curOpenData.StartTimeLocal = curTime;
                                    }
                                }
                            }

                            // =========================  SEARCH FOR CLOSING AUCTION ==========================================  //

                            if (curTime > new TimeSpan(CloseTime.Hours, 0, 0).Add(new TimeSpan(0, 0, -5)) && OpenAuctionEnded)
                            {
                                if (curTicker == "CSNA3.SA")
                                { }

                                if (curTime > new TimeSpan(21, 0, 0))
                                { }

                                if (!CloseAuctionStarted)
                                {
                                    TickerStateChanged = true;
                                    IntradayEnded = true;
                                    CloseAuctionStarted = true;

                                    curCloseData.Ticker = curTicker;
                                    curCloseData.Date = curDate;
                                    curCloseData.PreAuctionPrice = prevPrice;
                                    curCloseData.TotalTrades++;
                                    curCloseData.Minute = curTime.Subtract(new TimeSpan(OpenTimes[curDate].Hours, OpenTimes[curDate].Minutes, 0)).TotalMinutes;
                                    curCloseData.StartTimeGMT = curTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
                                    curCloseData.StartTimeLocal = curTime;

                                    closeVolume += curVolume;
                                    closeValue += curVolume * curPrice;
                                }
                                else
                                {
                                    curCloseData.TotalTrades++;
                                    closeVolume += curVolume;
                                    closeValue += curVolume * curPrice;
                                }
                            }

                            // ================================== CREATE INTRADAY 1 MIN BAR ==================================== //


                            if (OpenAuctionEnded && !CloseAuctionStarted)
                            {
                                if (!IntradayStarted)
                                {                                    
                                    curBar.Ticker = curTicker;
                                    curBar.Date = curDate;
                                    curBar.StartTimeGMT = curTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
                                    curBar.StartTimeLocal = curTime;
                                    curBar.Minute = curTime.Subtract(new TimeSpan(OpenTimes[curDate].Hours, OpenTimes[curDate].Minutes, 0)).TotalMinutes;
                                    if (TotalMinutes.TryGetValue(curDate, out curBar.RegressiveMinute))
                                    {
                                        curBar.RegressiveMinute -= (int)curBar.Minute;
                                    }
                                    curBar.Open = curPrice;
                                    curBar.Low = curPrice;
                                    curBar.High = curPrice;
                                    curBar.TotalTrades++;
                                    if (curPrice == curBid) curBar.BidTrades++;
                                    if (curPrice == curAsk) curBar.AskTrades++;
                                    curBar.Volume += curVolume;
                                    curBar.Value += curVolume * curPrice;
                                    IntradayStarted = true;
                                    if (curBar.Minute > 250)
                                    { }

                                    if (curDate == new DateTime(2009, 06, 04))
                                    { }

                                    if (curTicker.Contains("TOTS3.SA"))
                                    { }
                                }

                                else if (!IntradayEnded)
                                {
                                    curBar.TotalTrades++;
                                    if (curPrice == curBid) curBar.BidTrades++;
                                    if (curPrice == curAsk) curBar.AskTrades++;
                                    if (curPrice > curBar.High) curBar.High = curPrice;
                                    if (curPrice < curBar.Low) curBar.Low = curPrice;
                                    curBar.Volume += curVolume;
                                    curBar.Value += curVolume * curPrice;
                                }
                            }

                            if (curTime > new TimeSpan(20, 09, 16))
                            {
                                int i = 0;
                            }

                            // ==================================== RONDUP ================================================

                            prevVolume = curVolume;
                            prevPrice = curPrice;
                            prevDate = curDate;
                            prevTicker = curTicker;
                            prevTime = curTime;

                            prevCumVolume = cumVolume;
                            prevCumValue = cumValue;
                        }

                            #endregion

                        if (curType == "Quote")
                        {
                            curBid = 0;
                            curAsk = 0;
                            
                            if (curValues[quoteBid] != "")
                            {
                                curBid = double.Parse(curValues[quoteBid].Replace(".", ","));                                                               
                                
                            }

                            if (curValues[quoteAsk] != "")
                            {
                                curAsk = double.Parse(curValues[quoteAsk].Replace(".", ","));
                            }
                            
                            curSpread = curAsk - curBid;
                            cumSpread += curSpread;

                            if (!FirstSpreadFixed)
                            {
                                MinSpread = curSpread;
                                MaxSpread = curSpread;
                                FirstSpreadFixed = true;
                            }

                            if (curSpread < MinSpread)
                            {
                                MinSpread = curSpread;
                            }

                            if (curSpread > MaxSpread)
                            {
                                MaxSpread = curSpread;
                            }

                            TotalQuotesArrived++;                            
                            
                        }

                    }
                }
            }
            catch (Exception E)
            { }


            //Save Last Close Data                        
            curCloseData.Value = closeValue;
            curCloseData.Volume = closeVolume;
            curCloseData.Price = prevPrice;
            curCloseData.EndTimeGMT = prevTime.Add(new TimeSpan((-1)*curTimeShift, 0, 0));
            curCloseData.EndTimeLocal = prevTime;

            if (curCloseData.Ticker != null && curCloseData.Volume != 0)
            {
                CloseAuctionList.Add(curCloseData);                                
                curCloseData = new AuctionData();
            }
            sr.Close();
        }


        private void ProcessOffShroreFile(string curFileName)
        {
            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            
            StreamReader sr = new StreamReader(fs);

            // ==================== INITIALIZING VARIABLES =======================================

            // ====================== Sequence Control


            bool IntradayStarted = false;
            bool TickerInAuction = false;
            bool FirstSpreadFixed = false;
            bool OpenAuctionFixed = false;
            bool CloseAuctionFixed = false;

            // ====================== Accumulators and Prev Values
            TimeSpan OpenTime = new TimeSpan(0, 0, 0);
            TimeSpan CloseTime = new TimeSpan(0, 0, 0);
            double cumSpread = 0;
            Int64 TotalQuotesArrived = 0;
            double MinSpread = 0;
            double MaxSpread = 0;

            string prevTicker = "";
            double prevPrice = 0;

            DateTime prevDate = new DateTime(1900, 01, 01);
            TimeSpan prevTime = new TimeSpan(0, 0, 0);

            // ====================== Current Values
            DateTime curDate = new DateTime();
            TimeSpan curTime;
            int curTimeShift = 0;
            double curPrice;
            Int64 curVolume;
            double curSpread = 0;
            String curState;
            String curType;
            String curTicker;
            double TotalDayMinutes = 0;


            double cumVolume = 0;            

            double curBid = 0;
            double curAsk = 0;

            OneMinBar curBar = new OneMinBar();
            AuctionData curOpenData = new AuctionData();
            AuctionData curCloseData = new AuctionData();
            AuctionData curIntradayData = new AuctionData();

            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            tempLine = sr.ReadLine();
            string[] headers = tempLine.Split(',');

            for (int i = 0; i < headers.Length; i++)
            {
                switch (headers[i])
                {
                    case "#RIC": posTicker = i;
                        break;
                    case "Date[G]": posDate = i;
                        break;
                    case "Time[G]": posTime = i;
                        break;
                    case "GMT Offset": posTimeShift = i;
                        break;
                    case "Type": posType = i;
                        break;
                    case "Price": posLast = i;
                        break;
                    case "Volume": posVolume = i;
                        break;
                    case "Bid Price": quoteBid = i;
                        break;
                    case "Ask Price": quoteAsk = i;
                        break;
                    case "Qualifiers": posState = i;
                        break;
                }
            }

            string[] curValues;

            while ((tempLine = sr.ReadLine()) != null)
            {
                curValues = tempLine.Split(',');

                curTicker = curValues[0];

                lock (ScreenSync)
                {
                    Ticker = curTicker;
                    Status = "Loading " + curTicker + " Bars...";
                }

                try
                {
                    if (curValues[posDate] != "")
                    {
                        curValues[posDate] = curValues[posDate].Substring(0, 4) + "/" + curValues[posDate].Substring(4, 2) + "/" + curValues[posDate].Substring(6, 2);
                    }
                    curDate = new DateTime(1900, 01, 01); if (curValues[posDate] != "") curDate = DateTime.Parse(curValues[posDate]);
                }
                catch
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Date = curDate.ToString("dd/MM/yyyy");
                    if (curDate == new DateTime(2010, 04, 08))
                    { }
                }

                curTime = new TimeSpan(0, 0, 0); if (curValues[posTime] != "") curTime = TimeSpan.Parse(curValues[posTime]);
                curTimeShift = 0; if (curValues[posTimeShift] != "") curTimeShift = int.Parse(curValues[posTimeShift]);
                curType = curValues[posType];
                curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".",","));
                curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                curState = curValues[posState];

                if(curTicker == "SGEF.PA" && curDate == new DateTime(2006,01,03))
                {}

                if (curTime > new TimeSpan(20, 0, 0))
                { }           

                if (prevTicker != curTicker)
                {
                    if (curDate == new DateTime(2011, 12, 30))
                    { }
                    OpenAuctionFixed = false;
                    IntradayBarsList.Add(curTicker, new Dictionary<DateTime, Dictionary<int, OneMinBar>>());
                    IntradayBarsList[curTicker].Add(curDate, new Dictionary<int, OneMinBar>());                   

                    cumVolume = prevPrice = curPrice = curVolume = 0;

                    if (OpenTimes.ContainsKey(curDate) && CloseTimes.ContainsKey(curDate))
                    {
                        OpenTime = OpenTimes[curDate];
                        CloseTime = CloseTimes[curDate];
                        TotalDayMinutes = (int)TotalMinutes[curDate];
                    }
                    else
                    {
                        OpenTime = CloseTime = new TimeSpan(0, 0, 0);
                        TotalDayMinutes = 0;
                    }
                    cumVolume = 0;
                    CloseAuctionFixed = OpenAuctionFixed = false;
                }

                if (prevDate != curDate)
                {
                    OpenAuctionFixed = false;
                    if (!IntradayBarsList[curTicker].ContainsKey(curDate))
                    {
                        IntradayBarsList[curTicker].Add(curDate, new Dictionary<int, OneMinBar>());
                    }
                    cumVolume = prevPrice = curPrice = curVolume = 0;

                    if (OpenTimes.ContainsKey(curDate) && CloseTimes.ContainsKey(curDate))
                    {
                        OpenTime = OpenTimes[curDate];
                        CloseTime = CloseTimes[curDate];
                        TotalDayMinutes = (int)TotalMinutes[curDate];
                    }
                    else
                    {
                        OpenTime = CloseTime = new TimeSpan(0, 0, 0);
                        TotalDayMinutes = 0;
                    }
                    cumVolume = 0;
                    CloseAuctionFixed = OpenAuctionFixed = false;
                }

                // ======================= MINUTE CHANGED =======================
                if (CloseTime != new TimeSpan(0, 0, 0) && OpenTime != new TimeSpan(0, 0, 0) && TotalDayMinutes != 0)
                {
                    if ((new TimeSpan(curTime.Hours, curTime.Minutes, 0) - new TimeSpan(prevTime.Hours, prevTime.Minutes, 0)) >= new TimeSpan(0, 0, 1, 0))
                    {
                        if (curBar.Ticker != null && curBar.Volume != 0 && OpenAuctionFixed)
                        {
                            curBar.MinSpread = MinSpread;
                            curBar.MaxSpread = MaxSpread;
                            curBar.AverageSpread = cumSpread / TotalQuotesArrived;
                            curBar.CloseSpread = curSpread;
                            curBar.VWAP = curBar.Value / curBar.Volume;
                            curBar.Close = prevPrice;
                            curBar.EndTimeGMT = prevTime;
                            curBar.EndTimeLocal = prevTime.Add(new TimeSpan(curTimeShift, 0, 0));
                            curBar.RegressiveMinute = (TotalDayMinutes - curBar.Minute);
                            IntradayBarsList[curTicker][curDate].Add((int)curBar.Minute, curBar);

                            if (curBar.RegressiveMinute < 0)
                            { }

                            curBar = new OneMinBar();
                            FirstSpreadFixed = false;
                            cumSpread = 0;

                        }

                        IntradayStarted = false;
                    }

                    if (curType == "Trade")
                    {
                        // =========================  SEARCH FOR OPENING AUCTION ==========================================  //

                        if (!OpenAuctionFixed)
                        {
                            if (curPrice != 0 && curVolume != 0 && cumVolume == 0)
                            {
                                //Gravador.Grava_Log(tempLine + ",OA");
                                curOpenData.Ticker = curTicker;
                                curOpenData.Date = curDate;
                                curOpenData.Volume = curVolume;
                                curOpenData.Value = curPrice * curVolume;
                                curOpenData.Price = curPrice;
                                curOpenData.Minute = curTime.Subtract(new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                                curOpenData.TotalTrades = 1;
                                TickerInAuction = true;
                            }
                            else if (curPrice == prevPrice && curTime.Subtract(new TimeSpan(curTimeShift, 0, 0)) < OpenTime.Add(new TimeSpan(0, 0, 10)))
                            {
                                curOpenData.Volume += curVolume;
                                curOpenData.Value += curPrice * curVolume;
                                curOpenData.TotalTrades++;
                            }
                            else
                            {
                                curOpenData.EndTimeGMT = curTime;
                                curOpenData.EndTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                                OpenAuctionList.Add(curOpenData);

                                curOpenData = new AuctionData();

                                OpenAuctionFixed = true;
                                TickerInAuction = false;
                            }
                        }

                        // =========================  SEARCH FOR CLOSE AUCTION ==========================================  //

                        if (!CloseAuctionFixed && OpenAuctionFixed)
                        {                            
                            if (curTime > CloseTime.Add(new TimeSpan(0,3,0)))
                            {
                                curCloseData.Ticker = curTicker;
                                curCloseData.Date = curDate;
                                if (curDate == new DateTime(2012, 01, 18))
                                { }
                                curCloseData.PreAuctionPrice = prevPrice;
                                curCloseData.TotalTrades = 1;
                                curCloseData.Minute = curTime.Subtract(new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                                curCloseData.Value = curVolume * curPrice;
                                curCloseData.Volume = curVolume;
                                curCloseData.Price = curPrice;
                                curCloseData.EndTimeGMT = curTime;
                                curCloseData.EndTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));

                                if (curCloseData.Volume < 1000)
                                { }

                                CloseAuctionList.Add(curCloseData);
                                curCloseData = new AuctionData();

                                CloseAuctionFixed = true;
                            }
                        }

                        if (OpenAuctionFixed && !CloseAuctionFixed)
                        {
                            if (!IntradayStarted)
                            {
                                curBar.Ticker = curTicker;
                                curBar.Date = curDate;
                                curBar.StartTimeGMT = curTime;
                                curBar.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                                curBar.Minute = curTime.Subtract(new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                                curBar.Open = curPrice;
                                curBar.Low = curPrice;
                                curBar.High = curPrice;
                                curBar.TotalTrades++;
                                if (curPrice == curBid) curBar.BidTrades++;
                                if (curPrice == curAsk) curBar.AskTrades++;
                                curBar.Volume += curVolume;
                                curBar.Value += curVolume * curPrice;
                                IntradayStarted = true;

                                if (curBar.Minute < 0)
                                { }
                            }

                            else
                            {
                                curBar.TotalTrades++;
                                if (curPrice == curBid) curBar.BidTrades++;
                                if (curPrice == curAsk) curBar.AskTrades++;
                                if (curPrice > curBar.High) curBar.High = curPrice;
                                if (curPrice < curBar.Low) curBar.Low = curPrice;
                                curBar.Volume += curVolume;
                                curBar.Value += curVolume * curPrice;
                            }
                        }


                        prevPrice = curPrice;
                        cumVolume += curVolume;

                    }
                } 

                if (curType == "Quote")
                {
                    if (curValues[quoteBid] != "")
                    {
                        curBid = double.Parse(curValues[quoteBid].Replace(".", ","));

                    }

                    if (curValues[quoteAsk] != "")
                    {
                        curAsk = double.Parse(curValues[quoteAsk].Replace(".", ","));
                    }

                    curSpread = curAsk - curBid;
                    cumSpread += curSpread;

                    if (!FirstSpreadFixed)
                    {
                        MinSpread = curSpread;
                        MaxSpread = curSpread;
                        FirstSpreadFixed = true;
                    }

                    if (curSpread < MinSpread)
                    {
                        MinSpread = curSpread;
                    }

                    if (curSpread > MaxSpread)
                    {
                        MaxSpread = curSpread;
                    }

                    TotalQuotesArrived++;

                }

                prevTime = curTime;
                prevTicker = curTicker;
                prevDate = curDate;
            }

            sr.Close();
        }       

        private void LoadOpenTimes(int Id_Mercado)
        {
            /*
            FileStream fs = new FileStream(@"N:\TI\Projects\CSharp\Other\ReutersTickOneMinBarsGenerator\refOpenTimes.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string tempLine;

            try
            {
                while ((tempLine = sr.ReadLine()) != null)
                {
                    string[] curValues = tempLine.Split(';');
                    OpenTimes.Add(DateTime.Parse(curValues[0].ToString()), TimeSpan.Parse(curValues[1].ToString()));
                    CloseTimes.Add(DateTime.Parse(curValues[0].ToString()), TimeSpan.Parse(curValues[2].ToString()));
                    TotalMinutes.Add(DateTime.Parse(curValues[0].ToString()), -1 * (new TimeSpan(int.Parse(curValues[1].Substring(0, curValues[1].IndexOf(":"))), 0, 0) - new TimeSpan(int.Parse(curValues[2].Substring(0, curValues[2].IndexOf(":"))), 0, 0)).TotalMinutes);
                }
            }
            catch
            {
            }
            for (int i = (int)(DateTime.Today - OpenTimes.Last().Key.Date).TotalDays - 1; i >= 1; i--)
            {
                if (i == 1)
                { }
                if (DateTime.Today.AddDays(-i) == new DateTime(2012, 01, 17))
                {
                    OpenTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(14, 25, 0));
                    CloseTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(20, 0, 0));
                    TotalMinutes.Add(DateTime.Today.AddDays(-i), -1 * (new TimeSpan(14, 25, 0) - new TimeSpan(20, 0, 0)).TotalMinutes);
                }
                else if (DateTime.Today.AddDays(-i) == new DateTime(2012, 02, 22))
                {
                    OpenTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(15, 00, 0));
                    CloseTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(20, 0, 0));
                    TotalMinutes.Add(DateTime.Today.AddDays(-i), -1 * (new TimeSpan(15, 00, 0) - new TimeSpan(20, 0, 0)).TotalMinutes);
                }
                else if (DateTime.Today.AddDays(-i) < new DateTime(2012, 02, 27))
                {                   
                    OpenTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(13, 0, 0));
                    CloseTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(20, 0, 0));
                    TotalMinutes.Add(DateTime.Today.AddDays(-i), -1 * (new TimeSpan(13, 0, 0) - new TimeSpan(20, 0, 0)).TotalMinutes);
                }
                else
                {
                    OpenTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(14, 0, 0));
                    CloseTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(21, 0, 0));
                    TotalMinutes.Add(DateTime.Today.AddDays(-i), -1 * (new TimeSpan(14, 0, 0) - new TimeSpan(21, 0, 0)).TotalMinutes);
                }
            }
            */
            
            List<DateTime> OpenDates = new List<DateTime>();
            DataTable Tb = new DataTable();

            string OpenDatesQuery = "SELECT DISTINCT(SrDate) " +
                                    "FROM NESTDB.dbo.Tb053_Precos_Indices (NOLOCK) " +
                                    "WHERE IdSecurity = 1073 AND " +
                                    "    SrType = 1 AND " +
                                    "    SOURCE = 1 AND" +
                                    "    SrDate >= '20060101'";

            using (newNestConn conn = new newNestConn())
            {
                Tb = conn.Return_DataTable(OpenDatesQuery);
            }

            for (int i = 0; i < Tb.Rows.Count; i++)
            {                
                OpenDates.Add(DateTime.Parse(Tb.Rows[i][0].ToString()));
            }


            Dictionary<DateTime, DateTime> OpenTimeChanged = new Dictionary<DateTime, DateTime>();

            foreach (DateTime Date in OpenDates)
            {
                string OpenTimeQuery = "SELECT TOP 1 OpenTimeLocal,CloseTimeLocal,GMT_Offset " +
                                        "FROM NESTDB.dbo.Tb108_Horarios_Mercados " +
                                        "WHERE DateRef <= getdate() AND Id_Mercado = " + Id_Mercado + " " +
                                        "ORDER BY DateRef DESC ";

                using (newNestConn conn = new newNestConn())
                {
                    Tb = conn.Return_DataTable(OpenTimeQuery);

                    OpenTimes.Add(Date, DateTime.Parse(Tb.Rows[0][0].ToString()).TimeOfDay);
                    CloseTimes.Add(Date, DateTime.Parse(Tb.Rows[0][1].ToString()).TimeOfDay);
                    TotalMinutes.Add(Date, (CloseTimes[Date] - OpenTimes[Date]).TotalMinutes);
                }
            }

            if(!OpenTimes.ContainsKey(DateTime.Today))
            {
                OpenTimes.Add(DateTime.Today, OpenTimes.Last().Value);
                CloseTimes.Add(DateTime.Today, CloseTimes.Last().Value);
                TotalMinutes.Add(DateTime.Today, TotalMinutes.Last().Value);
            }
            
        }       

        public void PrintDataToFile(string FileName)
        {

            string curFolder = "";
            string filename = "";

            if (FileName.Contains("Germany"))
            {
                filename = FileName.Substring(FileName.IndexOf("Germany"));
                curFolder = @"C:\temp\RTICK\DataBaseFiles\OffShoreFiles\Unprocessed\";

            }
            else if (FileName.Contains("OneMinBar"))
            {
                filename = FileName.Substring(FileName.IndexOf("OneMinBar"));
                curFolder = @"C:\temp\RTICK\DataBaseFiles\OnShoreFiles\Unprocessed\";
            }
            filename = filename.Remove(filename.IndexOf(".csv"));

            string StartTimeGMT = "";
            string EndTimeGMT = "";
            string Minute = "";
            string ReverseMinute = "";
            string StartTimeLocal = "";
            string EndTimeLocal = "";

            foreach (AuctionData Auction in OpenAuctionList)
            {
                lock (ScreenSync)
                {
                    Ticker = Auction.Ticker;
                    Status = "Writing " + Auction.Ticker + " Bars...";
                }


                StreamWriter srWriter = new StreamWriter(@curFolder + filename + "OpenAuction_" + Auction.Ticker + ".txt", true);
                //StreamWriter srWriter = new StreamWriter(@curFolder + "OpenAuction_" + Auction.Ticker + "TESTE.txt", true); 

                EndTimeGMT = Auction.EndTimeGMT.ToString().IndexOf(".") > 0 ? Auction.EndTimeGMT.ToString().Remove(Auction.EndTimeGMT.ToString().IndexOf(".")) : Auction.EndTimeGMT.ToString();
                EndTimeLocal = Auction.EndTimeLocal.ToString().IndexOf(".") > 0 ? Auction.EndTimeLocal.ToString().Remove(Auction.EndTimeLocal.ToString().IndexOf(".")) : Auction.EndTimeLocal.ToString();

                srWriter.WriteLine(Auction.Ticker + ";" +
                                    Auction.Date.ToString("yyyy-MM-dd") + ";" +
                                    EndTimeGMT + ";" +
                                    EndTimeLocal + ";" +
                                    Auction.Price.ToString().Replace(",", ".") + ";" +
                                    Auction.Volume.ToString() + ";" +
                                    Auction.TotalTrades);
                srWriter.Close();
            }



            foreach (AuctionData Auction in CloseAuctionList)
            {

                lock (ScreenSync)
                {
                    Ticker = Auction.Ticker;
                    Status = "Writing " + Auction.Ticker + " Bars...";
                }

                StreamWriter srWriter = new StreamWriter(@curFolder + filename + "CloseAuction_" + Auction.Ticker + ".txt", true);
                //StreamWriter srWriter = new StreamWriter(@curFolder + "CloseAuction_" + Auction.Ticker + "TESTE.txt", true);

                StartTimeGMT = Auction.StartTimeGMT.ToString().IndexOf(".") > 0 ? Auction.StartTimeGMT.ToString().Remove(Auction.StartTimeGMT.ToString().IndexOf(".")) : Auction.StartTimeGMT.ToString();
                StartTimeLocal = Auction.StartTimeLocal.ToString().IndexOf(".") > 0 ? Auction.StartTimeLocal.ToString().Remove(Auction.StartTimeLocal.ToString().IndexOf(".")) : Auction.StartTimeLocal.ToString();
                EndTimeGMT = Auction.EndTimeGMT.ToString().IndexOf(".") > 0 ? Auction.EndTimeGMT.ToString().Remove(Auction.EndTimeGMT.ToString().IndexOf(".")) : Auction.EndTimeGMT.ToString();
                EndTimeLocal = Auction.EndTimeLocal.ToString().IndexOf(".") > 0 ? Auction.EndTimeLocal.ToString().Remove(Auction.EndTimeLocal.ToString().IndexOf(".")) : Auction.EndTimeLocal.ToString();

                srWriter.WriteLine(Auction.Ticker + ";" +
                                    Auction.Date.ToString("yyyy-MM-dd") + ";" +
                                    StartTimeGMT + ";" +
                                    StartTimeLocal + ";" +
                                    EndTimeGMT + ";" +
                                    EndTimeLocal + ";" +
                                    Auction.PreAuctionPrice.ToString().Replace(",", ".") + ";" +
                                    Auction.Price.ToString().Replace(",", ".") + ";" +
                                    Auction.Volume.ToString() + ";" +
                                    Auction.TotalTrades);
                srWriter.Close();
            }

            foreach (AuctionData Auction in IntradayAuctionList)
            {

                lock (ScreenSync)
                {
                    Ticker = Auction.Ticker;
                    Status = "Writing " + Auction.Ticker + " Bars...";
                }

                StreamWriter srWriter = new StreamWriter(@curFolder + filename + "IntradayAuction_" + Auction.Ticker + ".txt", true);
                //StreamWriter srWriter = new StreamWriter(@curFolder + "CloseAuction_" + Auction.Ticker + "TESTE.txt", true);

                StartTimeGMT = Auction.StartTimeGMT.ToString().IndexOf(".") > 0 ? Auction.StartTimeGMT.ToString().Remove(Auction.StartTimeGMT.ToString().IndexOf(".")) : Auction.StartTimeGMT.ToString();
                StartTimeLocal = Auction.StartTimeLocal.ToString().IndexOf(".") > 0 ? Auction.StartTimeLocal.ToString().Remove(Auction.StartTimeLocal.ToString().IndexOf(".")) : Auction.StartTimeLocal.ToString();
                EndTimeGMT = Auction.EndTimeGMT.ToString().IndexOf(".") > 0 ? Auction.EndTimeGMT.ToString().Remove(Auction.EndTimeGMT.ToString().IndexOf(".")) : Auction.EndTimeGMT.ToString();
                EndTimeLocal = Auction.EndTimeLocal.ToString().IndexOf(".") > 0 ? Auction.EndTimeLocal.ToString().Remove(Auction.EndTimeLocal.ToString().IndexOf(".")) : Auction.EndTimeLocal.ToString();

                srWriter.WriteLine(Auction.Ticker + ";" +
                                    Auction.Date.ToString("yyyy-MM-dd") + ";" +
                                    StartTimeGMT + ";" +
                                    StartTimeLocal + ";" +
                                    EndTimeGMT + ";" +
                                    EndTimeLocal + ";" +
                                    Auction.PreAuctionPrice.ToString().Replace(",", ".") + ";" +
                                    Auction.Price.ToString().Replace(",", ".") + ";" +
                                    Auction.Volume.ToString() + ";" +
                                    Auction.TotalTrades);
                srWriter.Close();
            }

            foreach (string Ticker in IntradayBarsList.Keys)
            {
                foreach (DateTime Date in IntradayBarsList[Ticker].Keys)
                {
                    foreach (OneMinBar Bar in IntradayBarsList[Ticker][Date].Values)
                    {
                        lock (ScreenSync)
                        {
                            Status = "Writing " + Bar.Ticker + " Bars...";
                        }
                        try
                        {

                            StreamWriter srWriter = new StreamWriter(@curFolder + filename + "OneMinBar_" + Bar.Ticker + ".txt", true);
                            //StreamWriter srWriter = new StreamWriter(@curFolder + "OneMinBar_" + Bar.Ticker + "TESTE.txt", true);

                            StartTimeGMT = Bar.StartTimeGMT.ToString().IndexOf(".") > 0 ? Bar.StartTimeGMT.ToString().Remove(Bar.StartTimeGMT.ToString().IndexOf(".")) : Bar.StartTimeGMT.ToString();
                            StartTimeLocal = Bar.StartTimeLocal.ToString().IndexOf(".") > 0 ? Bar.StartTimeLocal.ToString().Remove(Bar.StartTimeLocal.ToString().IndexOf(".")) : Bar.StartTimeLocal.ToString();
                            EndTimeGMT = Bar.EndTimeGMT.ToString().IndexOf(".") > 0 ? Bar.EndTimeGMT.ToString().Remove(Bar.EndTimeGMT.ToString().IndexOf(".")) : Bar.EndTimeGMT.ToString();
                            EndTimeLocal = Bar.EndTimeLocal.ToString().IndexOf(".") > 0 ? Bar.EndTimeLocal.ToString().Remove(Bar.EndTimeLocal.ToString().IndexOf(".")) : Bar.EndTimeLocal.ToString();
                            Minute = Bar.Minute.ToString().IndexOf(",") > 0 ? Bar.Minute.ToString().Remove(Bar.Minute.ToString().IndexOf(",")) : Bar.Minute.ToString();
                            ReverseMinute = Bar.RegressiveMinute.ToString().IndexOf(",") > 0 ? Bar.RegressiveMinute.ToString().Remove(Bar.RegressiveMinute.ToString().IndexOf(",")) : Bar.RegressiveMinute.ToString();

                            srWriter.WriteLine(Bar.Ticker + ";" +
                                                Bar.Date.ToString("yyyy-MM-dd") + ";" +
                                                Minute + ";" +
                                                StartTimeGMT + ";" +
                                                StartTimeLocal + ";" +
                                                EndTimeGMT + ";" +
                                                EndTimeLocal + ";" +
                                                Bar.VWAP.ToString().Replace(",", ".") + ";" +
                                                Bar.Volume.ToString() + ";" +
                                                Bar.Value.ToString().Replace(",", ".") + ";" +
                                                Bar.Low.ToString().Replace(",", ".") + ";" +
                                                Bar.High.ToString().Replace(",", ".") + ";" +
                                                Bar.Open.ToString().Replace(",", ".") + ";" +
                                                Bar.Close.ToString().Replace(",", ".") + ";" +
                                                Bar.AskTrades + ";" +
                                                Bar.BidTrades + ";" +
                                                Bar.TotalTrades + ";" +
                                                ReverseMinute + ";" +
                                                Bar.MinSpread.ToString().Replace(",", ".") + ";" +
                                                Bar.MaxSpread.ToString().Replace(",", ".") + ";" +
                                                Bar.AverageSpread.ToString().Replace(",", ".")
                                                );
                            srWriter.Close();
                        }

                        catch (Exception E)
                        {
                            //MessageBox.Show(E.Message);
                        }
                    }
                }
            }
        }

        public bool InsertFileInDatabase(string Folder)
        {
            bool DataInserted = false;
            string curFolder = Folder;

            string[] files = Directory.GetFiles(Folder);

            foreach (string curFile in files)
            {
                if (curFile.Contains("OpenAuction"))
                {

                    using (newNestConn curConn = new newNestConn(true))
                    {
                        DataInserted = true;
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        lock (ScreenSync)
                        {
                            Ticker = "None";
                            Status = "Inserting Open Auction Bars...";
                        }

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            lock (ScreenSync)
                            {
                                Ticker = TempTicker;
                                Date = curValues[1];
                            }

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.OpenAuction (IdTicker,Ticker,Date,EndTimeGMT,EndTimeLocal,Price,Volume,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "','" + curValues[2] + "','" + curValues[3] + "'," + curValues[4] + "," + curValues[5] + "," + curValues[6] + ",'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    string DestFile = curFile.Replace("Unprocessed", "Processed");

                    if (File.Exists(DestFile))
                    {
                        File.Delete(DestFile);
                    }

                    File.Move(curFile, DestFile);
                }

                if (curFile.Contains("CloseAuction"))
                {
                    DataInserted = true;
                    using (newNestConn curConn = new newNestConn(true))
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        lock (ScreenSync)
                        {
                            Ticker = "None";
                            Status = "Inserting Close Auction Bars...";
                        }

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            lock (ScreenSync)
                            {
                                Ticker = TempTicker;
                                Date = curValues[1];
                            }

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.CloseAuction (IdTicker,Ticker,Date,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,PreAuction_Price,Price,Volume,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "','" + curValues[2] + "','" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "'," + curValues[6] + "," + curValues[7] + "," + curValues[8] + "," + curValues[9] + ",'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }

                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    string DestFile = curFile.Replace("Unprocessed", "Processed");

                    if (File.Exists(DestFile))
                    {
                        File.Delete(DestFile);
                    }

                    File.Move(curFile, DestFile);
                }

                if (curFile.Contains("IntradayAuction"))
                {
                    DataInserted = true;
                    using (newNestConn curConn = new newNestConn(true))
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        lock (ScreenSync)
                        {
                            Ticker = "None";
                            Status = "Inserting Close Auction Bars...";
                        }

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            lock (ScreenSync)
                            {
                                Ticker = TempTicker;
                                Date = curValues[1];
                            }

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.IntradayAuction (IdTicker,Ticker,Date,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,PreAuction_Price,Price,Volume,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "','" + curValues[2] + "','" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "'," + curValues[6] + "," + curValues[7] + "," + curValues[8] + "," + curValues[9] + ",'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }

                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    string DestFile = curFile.Replace("Unprocessed", "Processed");

                    if (File.Exists(DestFile))
                    {
                        File.Delete(DestFile);
                    }

                    File.Move(curFile, DestFile);
                }

                if (curFile.Contains("OneMinBar") && !curFile.Contains("CloseAuction") && !curFile.Contains("OpenAuction"))
                {
                    DataInserted = true;
                    using (newNestConn curConn = new newNestConn(true))
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        lock (ScreenSync)
                        {
                            Ticker = "None";
                            Status = "Inserting OneMin Bars...";
                        }

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            lock (ScreenSync)
                            {
                                Ticker = TempTicker;
                                Date = curValues[1];
                            }

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.IntradayOneMinuteBars (IdTicker,Ticker,Date,Minute,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,VWAP,Volume,Low,High,[Open],[Close],AskTrades,BidTrades,TotalTrades,InsertDate,RegressiveMinute) VALUES "//,MinSpread,MaxSpread,AverageSpread) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "'," + curValues[2] + ",'" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "','" + curValues[6] + "'," + curValues[7] + "," + curValues[8] + "," + curValues[10]
                                            + "," + curValues[11] + "," + curValues[12] + "," + curValues[13] + "," + curValues[14] + "," + curValues[15] + "," + curValues[16] + ",'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "'," + curValues[17] + ")"; //+ "," + curValues[18] + "," + curValues[19] + "," + curValues[20] + ")";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }

                    string DestFile = curFile.Replace("Unprocessed", "Processed");

                    if (File.Exists(DestFile))
                    {
                        File.Delete(DestFile);
                    }

                    File.Move(curFile, DestFile);
                }
                
            }
            if (DataInserted) return true;         
            else return false;
        }

        private void Generate_Data()
        {
            //DateTime curDate = new DateTime(2006, 01, 01);
            DateTime curDate = DateTime.Today.AddDays(-1);
                       

            string SQL1 = "";
            string SQL2 = "";
            string SQL3 = "";
            string SQL4 = "";
            string SQL5 = "";
            string SQL6 = "";
            string SQL7 = "";
            string SQL8 = "";
            string SQL9 = "";
            string SQL10 = "";
            string SQL11 = "";
            string SQL12 = "";
            string SQL13 = "";
            string SQL14 = "";
            string SQL15 = "";
            string SQL16 = "";
            string SQL17 = "";
            string SQL18 = "";

                /*                     
                int[] Tables = { 0 };
                int[] Tables2 = { 2 };   
                int[] Tables3 = { 0, 2 };

                lock (ScreenSync)
                {
                    Ticker = "Generating VWAP Prices...";
                }

                ProcessVWap(440, 60, 30, curDate, true, Tables2, true);
                ProcessVWap(450, 90, 60, curDate, true, Tables2, true);
                ProcessVWap(460, 120, 90, curDate, true, Tables2, true);

                ProcessVWap(360, 30, 20, curDate, true, Tables);
                ProcessVWap(370, 45, 30, curDate, true, Tables);
                ProcessVWap(350, 1000, 90, curDate, true, Tables);             

                ProcessVWap(330, 30, 0, curDate, true, Tables3);
            */
            
                #region Data Generate Queries
                 
                #region Queries for Yesterday
               

                SQL1 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),361,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute <=30 and RegressiveMinute > 20 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL2 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),360,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=30 and RegressiveMinute > 20 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL3 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),371,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=45 and RegressiveMinute > 30 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL4 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),370,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=45 and RegressiveMinute > 30 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL5 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(Volume) AS VWAP,Date, getdate(),331,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=30 and date = @date " +
                "GROUP BY IdTicker,Date " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(Volume) AS VWAP,Date, getdate(),331,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=30 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL6 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(Volume) AS VWAPVolume,Date, getdate(),330,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=30 and date = @date " +
                "GROUP BY IdTicker,Date " +

                 "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(Volume) AS VWAPVolume,Date, getdate(),330,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=30 and date = @date " +
                "GROUP BY IdTicker,Date ";


                SQL7 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, PreAuction_Price, Date, getdate(),312,22,1 " +
                "FROM RTICKDB.dbo.CloseAuction  " +
                "WHERE date = @date ";

                SQL8 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, Volume, Date, getdate(),310,22,1 " +
                "FROM RTICKDB.dbo.CloseAuction  " +
                "WHERE  date = @date ";



                SQL9 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT A.IdTicker, SUM(VWAP*Volume)/SUM(Volume) AS VWAP,A.Date, getdate(),351,22,1 from " +
                "( " +
                "	SELECT SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP, SUM(convert(float,Volume)) as Volume, IdTicker, Date " +
                "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "	WHERE RegressiveMinute >= 90 and date = @date " +
                "	GROUP BY IdTicker,Date " +

                "	UNION " +

                "	SELECT Price as VWAP,convert(float,Volume),IdTicker,Date " +
                "		FROM RTICKDB.dbo.OpenAuction	 " +
                "	WHERE date = @date " +
                ") A " +
                "GROUP BY A.IdTicker,A.Date ";


                SQL10 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,VWAPVolume)),Date, getdate(),350,22,1 FROM " +
                "( " +
                "	SELECT SUM(convert(float,Volume)) AS VWAPVolume,idticker,date " +
                "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "	WHERE RegressiveMinute >= 90 and date = @date " +
                "	GROUP BY IdTicker,Date " +

                "	UNION " +

                "	SELECT volume as VWAPVolume,idticker,date " +
                "		FROM RTICKDB.dbo.OpenAuction " +
                "	WHERE date = @date " +
                ") A " +
                "GROUP BY A.idticker,A.Date ";
                
                SQL11 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, MAX(High) ,Date, getdate(),380,22,1 FROM " +
                "( " +
                "	SELECT MAX(High) AS High,idticker,date " +
                "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "	WHERE RegressiveMinute >= 30 AND Date = @date " +
                "	GROUP BY IdTicker,Date " +
                " " +
                "	UNION " +
                " " +
                "	SELECT Price as High,idticker,date " +
                "		FROM RTICKDB.dbo.OpenAuction " +
                "	WHERE Date = @date " +
                ") A " +
                "GROUP BY A.idticker,A.Date ";

                SQL12 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, MIN(Low) ,Date, getdate(),381,22,1 FROM " +
                "( " +
                "	SELECT MIN(Low) AS Low ,idticker,date " +
                "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "	WHERE RegressiveMinute >= 30 AND Date = @date " +
                "	GROUP BY IdTicker,Date " +
                " " +
                "	UNION " +
                " " +
                "	SELECT Price as Low,idticker,date " +
                "		FROM RTICKDB.dbo.OpenAuction " +
                "	WHERE Date = @date " +
                ") A " +
                "GROUP BY A.idticker,A.Date ";

                SQL13 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),421,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=60 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL14 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),420,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=60 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL15 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +


                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),441,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=60 and RegressiveMinute>30 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL16 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +


                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),440,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=60 and RegressiveMinute>30 and date = @date " +
                "GROUP BY IdTicker,Date ";

                SQL17 = 

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +


                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),451,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=90 and RegressiveMinute>60  date = @date " +
                "GROUP BY IdTicker,Date " ;
            
                SQL18 =  
                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),450,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=90 and RegressiveMinute>60 and date = @date " +
                "GROUP BY IdTicker,Date ";

                lock (ScreenSync)
                {
                    Status = "Generating Data from generated Bars for yesteday...";
                    Ticker = "";
                    Date = "";
                }                
                #endregion           
           

            #region Execute Queries

            using (newNestConn Conn = new newNestConn(true))
            {
                int result;

                
                lock (ScreenSync)
                {
                    Ticker = "Query 1";                    
                }
                
                result = Conn.ExecuteNonQuery(SQL1);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 2";
                }

                result = Conn.ExecuteNonQuery(SQL2);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 3";
                }

                result = Conn.ExecuteNonQuery(SQL3);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 4";
                }

                result = Conn.ExecuteNonQuery(SQL4);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 5";
                }

                result = Conn.ExecuteNonQuery(SQL5);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 6";
                }

                result = Conn.ExecuteNonQuery(SQL6);
                if (result != 1)
                {
                    int i = 0;
                }
                
                lock (ScreenSync)
                {
                    Ticker = "Query 7";
                }

                result = Conn.ExecuteNonQuery(SQL7);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 8";
                }
                
                result = Conn.ExecuteNonQuery(SQL8);
                if (result != 1)
                {
                    int i = 0;
                }
                
                lock (ScreenSync)
                {
                    Ticker = "Query 9";
                }

                result = Conn.ExecuteNonQuery(SQL9);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 10";
                }

                result = Conn.ExecuteNonQuery(SQL10);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 11";
                }

                result = Conn.ExecuteNonQuery(SQL11);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 12";
                }

                result = Conn.ExecuteNonQuery(SQL12);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 13";
                }

                result = Conn.ExecuteNonQuery(SQL13);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 14";
                }

                result = Conn.ExecuteNonQuery(SQL14);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 15";
                }

                result = Conn.ExecuteNonQuery(SQL15);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 16";
                }

                result = Conn.ExecuteNonQuery(SQL16);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 17";
                }

                result = Conn.ExecuteNonQuery(SQL17);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 18";
                }

                result = Conn.ExecuteNonQuery(SQL18);
                if (result != 1)
                {
                    int i = 0;
                }

                #endregion
            
            }
            
                #endregion
           

        }

        private void Generate_Data_Germany()
        {
            string SQL1 = "";
            string SQL2 = "";
            string SQL3 = "";
            string SQL4 = "";

            string tickers =
                "(221330,221331,221332,221333,221334,221335,221336," +
                "221337,221338,221339,221340,221341,221342,221343," +
                "221344,221345,221346,221347,221348,221349,221350," +
                "221351,221352,221353,221354,221355,221356,221357," +
                "221358,221359,221360,221361,221362,221363,221364," +
                "221365,221366,221367,221368,221369,221370,221371," +
                "221372,221373,221374,221375,221376,221377,221378," +
                "221379,221380,221381,221382,221383,221384,221385," +
                "221386,221387,221388,221389,221390,221391,221392," +
                "221313,221314,221315,221316,221317,221318,221319," +
                "221320,221321,221322,221323,221324,221325,221326," +
                "221327,221328,221329,220923,220924) ";

            
                SQL1 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, PreAuction_Price, Date, getdate(),312,22,1 " +
                "FROM RTICKDB.dbo.CloseAuction  " +
                "WHERE date = @date AND IdTicker in "+tickers;

                SQL2 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, Volume, Date, getdate(),310,22,1 " +
                "FROM RTICKDB.dbo.CloseAuction  " +
                "WHERE  date = @date AND IdTicker in "+tickers;

                SQL3 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),421,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=60 and date = @date AND IdTicker in " + tickers +
                "GROUP BY IdTicker,Date";

                SQL4 =

                "declare @date datetime " +

                "IF(DATENAME(dw,getdate()) = 'Monday') " +
                "    SET @date = convert(varchar,getdate()-3,112) " +
                "ELSE " +
                "    SET @date = convert(varchar,getdate()-1,112) " +

                "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
                "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),420,22,1 " +
                "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                "WHERE RegressiveMinute<=60 and date = @date AND IdTicker in " + tickers +
                "GROUP BY IdTicker,Date ";

                lock (ScreenSync)
                {
                    Status = "Generating Data from Germany generated Bars for yesteday...";
                    Ticker = "";
                    Date = "";
                }
           

            using (newNestConn Conn = new newNestConn())
            {
                lock (ScreenSync)
                {
                    Ticker = "Query 1";
                }

                int result;
                result = Conn.ExecuteNonQuery(SQL1);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {

                    Ticker = "Query 2";
                }

                result = Conn.ExecuteNonQuery(SQL2);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 3";
                }

                result = Conn.ExecuteNonQuery(SQL3);
                if (result != 1)
                {
                    int i = 0;
                }

                lock (ScreenSync)
                {
                    Ticker = "Query 4";
                }

                result = Conn.ExecuteNonQuery(SQL4);
                if (result != 1)
                {
                    int i = 0;
                }
            }

        }


        private void ProcessVWap(int SrType, int RangeBegin, int RangeEnd, DateTime Date, bool RegressiveMinute, int[] Tables, bool RangeDate = false, bool WithAuction = false)
        {
            //Target Table 0 : NESTDB.dbo.Tb050_Preco_Acoes_Onshore
            //Target Table 1 : NESTDB.dbo.Tb050_Preco_Acoes_Offshore
            //Target Table 2 : RTICKDB.dbo.Tb001_Precos_RTICK

            string SQL = "";

            if (RegressiveMinute)
            {
                if (!RangeDate)
                {
                    SQL =
                    "SELECT IdTicker, Ticker, Minute, RegressiveMinute, VWAP, Volume, Date " +
                    "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                    "WHERE   Date = '" + Date.ToString("yyyyMMdd") + "' AND " +
                    "        RegressiveMinute <= " + RangeBegin + " AND " +
                    "        RegressiveMinute > " + RangeEnd + " " +
                    "ORDER BY IdTicker, Date";
                }
                else
                {
                    SQL =
                    "SELECT IdTicker, Ticker, Minute, RegressiveMinute, VWAP, Volume, Date " +
                    "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                    "WHERE   Date >= '" + Date.ToString("yyyyMMdd") + "' AND " +
                    "        RegressiveMinute <= " + RangeBegin + " AND " +
                    "        RegressiveMinute > " + RangeEnd + " " +
                    "ORDER BY IdTicker, Date";
                }
            }
            else
            {
                if (!RangeDate)
                {
                    SQL =
                    "SELECT IdTicker, Ticker, Minute, RegressiveMinute, VWAP, Volume, Date " +
                    "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                    "WHERE   Date = '" + Date.ToString("yyyyMMdd") + "' AND " +
                    "        Minute >= " + RangeBegin + " AND " +
                    "        Minute < " + RangeEnd + " " +
                    "ORDER BY IdTicker, Date";
                }
                else
                {
                    SQL =
                    "SELECT IdTicker, Ticker, Minute, RegressiveMinute, VWAP, Volume, Date " +
                    "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
                    "WHERE   Date >= '" + Date.ToString("yyyyMMdd") + "' AND " +
                    "        Minute >= " + RangeBegin + " AND " +
                    "        Minute < " + RangeEnd + " " +
                    "ORDER BY IdTicker, Date";
                }
            }

            DataTable Table = new DataTable();

            using (newNestConn conn = new newNestConn())
            {
                Table = conn.Return_DataTable(SQL);
            }

            string Ticker = "", PrevTicker = "";
            double cumVolume = 0, cumFinance = 0;
            int IdTicker = 0, PrevIdTicker = 0;
            DateTime curDate, prevDate = new DateTime();

            Dictionary<int, Dictionary<DateTime, double>> VwapList = new Dictionary<int, Dictionary<DateTime, double>>();
            Dictionary<int,Dictionary<DateTime, double>> VolumeList = new Dictionary<int,Dictionary<DateTime,double>>();

            for (int i = 0; i < Table.Rows.Count; i++)
            {
                IdTicker = (int)Table.Rows[i][0];
                if (IdTicker == 160004)
                { }
                else
                { }
                Ticker = Table.Rows[i][1].ToString();
                curDate = DateTime.Parse(Table.Rows[i][6].ToString());

                if (PrevTicker != "" && prevDate != new DateTime() && (Ticker != PrevTicker || prevDate != curDate || i == Table.Rows.Count - 1))
                {
                    if (i == Table.Rows.Count - 1 && PrevTicker == Ticker)
                    {                        
                        cumVolume += double.Parse(Table.Rows[i][5].ToString());
                        cumFinance += double.Parse(Table.Rows[i][5].ToString()) * double.Parse(Table.Rows[i][4].ToString());

                        if (!VolumeList.ContainsKey(IdTicker))
                        {
                            VolumeList.Add(IdTicker, new Dictionary<DateTime, double>());
                        }
                        if (!VolumeList[IdTicker].ContainsKey(curDate))
                        {
                            VolumeList[IdTicker].Add(curDate, cumVolume);
                        }

                        if (!VwapList.ContainsKey(IdTicker))
                        {
                            VwapList.Add(IdTicker, new Dictionary<DateTime, double>());
                        }
                        if (!VwapList[IdTicker].ContainsKey(curDate))
                        {
                            VwapList[IdTicker].Add(curDate, cumFinance / cumVolume);
                        }                        
                    }
                    else
                    {                        
                        if (cumVolume != 0 && cumFinance != 0)
                        {
                            if (!VolumeList.ContainsKey(PrevIdTicker))
                            {
                                VolumeList.Add(PrevIdTicker, new Dictionary<DateTime, double>());
                            }
                            if (!VolumeList[PrevIdTicker].ContainsKey(prevDate))
                            {
                                VolumeList[PrevIdTicker].Add(prevDate, cumVolume);
                            }

                            if (!VwapList.ContainsKey(PrevIdTicker))
                            {
                                VwapList.Add(PrevIdTicker, new Dictionary<DateTime, double>());
                            }
                            if (!VwapList[PrevIdTicker].ContainsKey(prevDate))
                            {
                                VwapList[PrevIdTicker].Add(prevDate, cumFinance / cumVolume);
                            }
                        }

                        if (i == Table.Rows.Count - 1)
                        {
                            cumVolume = double.Parse(Table.Rows[i][5].ToString());
                            cumFinance = double.Parse(Table.Rows[i][5].ToString()) * double.Parse(Table.Rows[i][4].ToString());

                            if (!VolumeList.ContainsKey(IdTicker))
                            {
                                VolumeList.Add(IdTicker, new Dictionary<DateTime, double>());
                            }
                            if (!VolumeList[IdTicker].ContainsKey(curDate))
                            {
                                VolumeList[IdTicker].Add(curDate, cumVolume);
                            }

                            if (!VwapList.ContainsKey(IdTicker))
                            {
                                VwapList.Add(IdTicker, new Dictionary<DateTime, double>());
                            }
                            if (!VwapList[IdTicker].ContainsKey(curDate))
                            {
                                VwapList[IdTicker].Add(curDate, cumFinance / cumVolume);
                            }
                        }
                    }
                    cumVolume = cumFinance = 0;                    
                }

                cumVolume += double.Parse(Table.Rows[i][5].ToString());
                cumFinance += double.Parse(Table.Rows[i][5].ToString()) * double.Parse(Table.Rows[i][4].ToString());

                PrevTicker = Ticker;
                PrevIdTicker = IdTicker;
                prevDate = curDate;
                
            }

            /*
            if (WithAuction)
            {
                for (int j = 0; j < VwapList.Count; j++)
                {
                    string Auction_SQL = "";

                    double AucVolume = 0, AucPrice = 0;

                    int IdSec = VwapList.ElementAt(j).Key;

                    if (RegressiveMinute)
                    {
                        Auction_SQL =

                            "SELECT Price AS VWAP,CONVERT(FLOAT,Volume) " +
                            "		FROM RTICKDB.dbo.CloseAuction	 " +
                            "	WHERE date = '" + Date.ToString("yyyyMMdd") + "' AND IdTicker = " + IdSec;

                    }
                    else
                    {
                        Auction_SQL =

                            "SELECT Price AS VWAP,CONVERT(FLOAT,Volume),IdTicker,Date " +
                            "		FROM RTICKDB.dbo.OpenAuction	 " +
                            "	WHERE date = '" + Date.ToString("yyyyMMdd") + "' AND IdTicker = " + IdSec;
                    }

                    DataTable AucTable = new DataTable();

                    using (newNestConn conn = new newNestConn())
                    {
                        AucTable = conn.Return_DataTable(Auction_SQL);
                        AucVolume = double.Parse(AucTable.Rows[0][1].ToString());
                        AucPrice = double.Parse(AucTable.Rows[0][0].ToString());
                    }                   

                    VwapList[IdSec] = ((VwapList[IdSec] * VolumeList[IdSec]) + (AucPrice * AucVolume)) / (AucVolume + VolumeList[IdSec]);
                    VolumeList[IdSec] += AucVolume;
                }
            }
            */

            string table = "";

            foreach (int TargetTable in Tables)
            {
                switch (TargetTable)
                {
                    case 0: table = "NESTDB.dbo.Tb050_Preco_Acoes_Onshore";
                        break;
                    case 1: table = "NESTDB.dbo.Tb050_Preco_Acoes_Offshore";
                        break;
                    case 2: table = "RTICKDB.dbo.Tb001_Precos_RTICK";
                        break;
                }

                foreach (KeyValuePair<int, Dictionary<DateTime, double>> Node in VwapList)
                {
                    foreach (KeyValuePair<DateTime, double> Node1 in Node.Value)
                    {
                        string Insert_SQL =

                            "INSERT INTO " + table + " ([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) VALUES " +
                            "(" + Node.Key + "," + Node1.Value.ToString().Replace(",", ".") + ",'" + Node1.Key.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + "'," + (SrType + 1) + ",22,1)";

                        using (newNestConn conn = new newNestConn())
                        {
                            conn.ExecuteNonQuery(Insert_SQL);
                        }
                    }
                }
                foreach (KeyValuePair<int, Dictionary<DateTime, double>> Node in VolumeList)
                {
                    foreach (KeyValuePair<DateTime, double> Node1 in Node.Value)
                    {
                        string Insert_SQL =

                            "INSERT INTO " + table + " ([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) VALUES " +
                            "(" + Node.Key + "," + Node1.Value.ToString().Replace(",", ".") + ",'" + Node1.Key.ToString("yyyyMMdd") + "','" + DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + "'," + SrType + ",22,1)";

                        using (newNestConn conn = new newNestConn())
                        {
                            conn.ExecuteNonQuery(Insert_SQL);
                        }
                    }

                }
            }
        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (ScreenSync)
            {
                lblStatus.Text = Status;
                lblDate.Text = Date;
                lblTicker.Text = Ticker;
            } 
        }

        private void PrintOpenTime(string ticker, TimeSpan Time,DateTime Date)
        {
            //StreamWriter SW = new StreamWriter(@"C:\Temp\"+ticker+".csv",true);
            //SW.WriteLine(ticker + ";" + Date + ";" + Time.ToString());
            //SW.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
        

    public class OneMinBar
    {
        public DateTime Date;        
        public double Minute;
        public double RegressiveMinute;
        public string Ticker;
        public double Open = 0;
        public double Close = 0;
        public double High = 0;
        public double Low = 0;
        public Int64 Volume = 0;
        public double Value = 0;
        public double VWAP = 0;
        public int TotalTrades = 0;
        public int BidTrades = 0;
        public int AskTrades = 0;
        public TimeSpan StartTimeGMT = new TimeSpan(0, 0, 0);
        public TimeSpan StartTimeLocal = new TimeSpan(0, 0, 0);
        public TimeSpan EndTimeGMT = new TimeSpan(0, 0, 0);
        public TimeSpan EndTimeLocal = new TimeSpan(0, 0, 0);
        public double MinSpread = 0;
        public double AverageSpread = 0;
        public double MaxSpread = 0;
        public double CloseSpread = 0;

        public string ToString()
        {
            return Ticker + "_" + Date.ToString("dd-MMM-yy") + "_" + Minute;
        }
    }

    public class AuctionData
    {
        public DateTime Date;
        public double Minute;
        public string Ticker;
        public double Value = 0;
        public double Price = 0;
        public Int64 Volume = 0;
        public int TotalTrades = 0;
        public int BidTrades = 0;
        public int AskTrades = 0;
        public TimeSpan StartTimeGMT = new TimeSpan(0, 0, 0);
        public TimeSpan StartTimeLocal = new TimeSpan(0, 0, 0);
        public TimeSpan EndTimeGMT = new TimeSpan(0, 0, 0);
        public TimeSpan EndTimeLocal = new TimeSpan(0, 0, 0);
        public double PreAuctionPrice = 0;

        public string ToString()
        {
            return Ticker + "_" + Date.ToString("dd-MMM-yy") + "_" + Minute;
        }
    }

    class Gravador
    {
        public static void Grava_Log(string msg)
        {
            StreamWriter sw = new StreamWriter(@"C:\Temp\TesteVOW.DE.csv", true);
            sw.WriteLine(msg);
            sw.Close();
        }
    }


    
}
