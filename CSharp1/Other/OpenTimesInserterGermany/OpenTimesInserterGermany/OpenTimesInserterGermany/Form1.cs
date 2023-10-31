using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OpenTimesInserterGermany
{
    public partial class Form1 : Form
    {
        Dictionary<DateTime, Dictionary<string, DateInfo>> Times = new Dictionary<DateTime, Dictionary<string, DateInfo>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProcessFolder(@"R:\RTICK\OneMinuteBars\Europe\Unprocessed");
            //ProcessFile(@"C:\temp\RTICK\OpenTimes\luis.fonseca@nestinvestimentos.com.br-Opentimes_2009-N31840154.csv");
            WriteFile();
        }

        private void ProcessFolder(string Folder)
        {
            string[] Files = Directory.GetFiles(Folder);

            foreach (string File in Files)
            {
                ProcessFile(File);
            }
        }


        private void ProcessFile(string File)
        {


            // ====================== Sequence Control

            bool OpenAuctionEnded = false;
            bool CloseAuctionEnded = false;
            bool IntradayAuctionStarted = false;
            bool IntradayAuctionEnded = false;
            bool CloseAuctionStarted = false;
            bool OpenAuctionStarted = false;
            bool IntradayStarted = false;
            bool TickerInAuction = false;
            bool FirstSpreadFixed = false;
            bool OpenAuctionFixed = false;
            bool IntradayAuctionFixed = false;
            bool CloseAuctionFixed = false;

            StreamReader Reader = new StreamReader(File);

            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            tempLine = Reader.ReadLine();
            string[] headers = tempLine.Split(',');

            DateTime curDate = new DateTime(), prevDate = new DateTime();
            TimeSpan curTime, prevTime = new TimeSpan();
            int curTimeShift = 0;
            double curPrice;
            Int64 curVolume;
            double curSpread = 0;
            String curState;
            String curType;
            String curTicker, prevTicker = "";

            double curBid = 0;
            double curAsk = 0;

            //string[] tickers = { "SAPG.DE", "ALVG.DE", "CBKG.DE" };

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

            while (!Reader.EndOfStream)
            {
                tempLine = Reader.ReadLine();
                curValues = tempLine.Split(',');

                curTicker = curValues[0];

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

                if (curDate.Day == 31 && curDate.Month == 12)
                {
                }

                curTime = new TimeSpan(0, 0, 0); if (curValues[posTime] != "") curTime = TimeSpan.Parse(curValues[posTime]);
                curTimeShift = 0; if (curValues[posTimeShift] != "") curTimeShift = int.Parse(curValues[posTimeShift]);
                curType = curValues[posType];
                curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".", ","));
                curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                curState = curValues[posState];

                if (curDate == new DateTime(2011, 12, 30))
                { }

                TimeSpan CloseTime, OpenTime = new TimeSpan();

                if (curTime > new TimeSpan(20, 0, 0))
                { }

                if (curTicker == "SGEF.PA" && curDate == new DateTime(2006, 01, 03))
                { }

                //if (tickers.Contains(curTicker))
                if(curType == "Trade" && !curState.Contains("STOCK_TYPE"))
                {
                    if (curTime.Add(new TimeSpan(curTimeShift, 0, 0)) < new TimeSpan(7, 0, 0))
                    { }
                    if (curTicker == "SGEF.PA" && curDate == new DateTime(2006, 01, 06))
                    { }

                    if (prevTicker != curTicker || prevDate != curDate)
                    {
                        OpenAuctionFixed = CloseAuctionFixed = OpenAuctionFixed = CloseAuctionFixed = false;
                        if (!Times.ContainsKey(curDate))
                        {
                            Times.Add(curDate, new Dictionary<string, DateInfo>());
                            Times[curDate].Add(curTicker, new DateInfo());
                        }
                        if (!Times[curDate].ContainsKey(curTicker))
                        {
                            Times[curDate].Add(curTicker, new DateInfo());
                        }

                        if (prevDate != curDate)
                        {
                            if (prevTicker != "")
                            {
                                if (prevTicker != curTicker)
                                {
                                    Times[prevDate][prevTicker].TotalMinutesOpened = (int)(curTime - Times[prevDate][prevTicker].OpenTimeGMT).TotalMinutes;
                                }
                                else
                                {
                                    Times[prevDate][curTicker].TotalMinutesOpened = (int)(prevTime - Times[prevDate][prevTicker].OpenTimeGMT).TotalMinutes;
                                }
                            }
                            Times[curDate][curTicker].OpenTimeGMT = curTime;
                            Times[curDate][curTicker].TimeShift = curTimeShift;
                        }

                    }

                    prevDate = curDate;
                    prevTicker = curTicker;
                    prevTime = curTime;

                    //TODO Alterar para original
                    /*
                    switch (curState.Substring(0, curState.IndexOf("]") + 1))
                    {
                        case "OpnAuc[GV1_TEXT]":
                        case "OCALL [GV1_TEXT]":
                            if (!OpenAuctionFixed)
                            {
                                OpenAuctionStarted = true;
                            }
                            break;

                        case "ClsAuc[GV1_TEXT]":
                        case "CCALL [GV1_TEXT]":
                            if (!CloseAuctionFixed)
                            {
                                CloseAuctionFixed = true;
                                
                                Times[curDate][curTicker].TotalMinutesOpened = Math.Abs(curTime.Minutes - 30) < 5 ? (int)(new TimeSpan(curTime.Hours, 30, 0) - Times[curDate][curTicker].OpenTimeGMT).TotalMinutes : (int)(new TimeSpan(curTime.Hours, 0, 0) - Times[curDate][curTicker].OpenTimeGMT).TotalMinutes;
                                
                            }
                            break;

                        case "A[ACT_FLAG1]":
                            if (!OpenAuctionFixed && OpenAuctionStarted)
                            {
                                OpenAuctionFixed = true;
                                OpenTime = curTime;
                                Times[curDate][curTicker].OpenTimeGMT = Math.Abs((curTime - new TimeSpan(curTime.Hours, 0, 0)).Minutes) < 5 ? new TimeSpan(curTime.Hours, 0, 0) : curTime;
                                Times[curDate][curTicker].TimeShift = int.Parse(curValues[posTimeShift]);
                            }
                            break;
                    }
                     * */                    
                }
                
            }

        }
        
        private void WriteFile()
        {
            StreamWriter Writer = new StreamWriter(@"C:\temp\RTICK\OpenTimes\ITA_SPA_FRA_OpenTimes2.csv");

            List<string> TickersList = new List<string>();

            foreach (DateTime Date in Times.Keys)
            {
                foreach(string Ticker in Times[Date].Keys)
                {
                    if (!TickersList.Contains(Ticker))
                    {
                        TickersList.Add(Ticker);
                    }
                }
            }

            foreach(DateTime Date in Times.Keys)
            {
                int prevIndex = 0;
                string Line = Date.ToString("dd/MM/yyyy") + ";";
                foreach(string Ticker in Times[Date].Keys)
                {
                    int index = TickersList.IndexOf(Ticker);
                    for (int i = 0; i < index - prevIndex - 1; i++) 
                        Line += ";;;;";
                    prevIndex = index;
                    Line += Ticker + ";" + Times[Date][Ticker].OpenTimeGMT.ToString("g") + ";" + Times[Date][Ticker].TimeShift + ";" + Times[Date][Ticker].TotalMinutesOpened + ";"; 
                }
                Writer.WriteLine(Line);
            }

            Writer.Close();

        }

        /*private void ProcessOffShroreGermanyFile(string curFileName)
        {
            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream(@"R:\RTICK\Daily Requests\Unprocessed\Requests\Uncompressed\Uns.fonseca@nestinvestimentos.com.br-Daily_Request-N26538123.csv", FileMode.Open, FileAccess.Read);
            //FileStream fs = new FileStream(@"C:\Temp\ABCB4_TEST.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            // ==================== INITIALIZING VARIABLES =======================================

            // ====================== Sequence Control

            bool OpenAuctionEnded = false;
            bool CloseAuctionEnded = false;
            bool IntradayAuctionStarted = false;
            bool IntradayAuctionEnded = false;
            bool CloseAuctionStarted = false;
            bool OpenAuctionStarted = false;
            bool IntradayStarted = false;
            bool TickerInAuction = false;
            bool FirstSpreadFixed = false;
            bool OpenAuctionFixed = false;
            bool IntradayAuctionFixed = false;
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

            int UnusualAuctions = 0;


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
                curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".", ","));
                curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                curState = curValues[posState];

                if (curTime > new TimeSpan(20, 0, 0))
                { }

                //TODO Alterar para original
                switch (curState.Substring(0, curState.IndexOf("]") + 1))
                {
                    case "OpnAuc[GV1_TEXT]":
                    case "OCALL [GV1_TEXT]":
                        if (!OpenAuctionFixed)
                        {
                            OpenAuctionStarted = true;
                            OpenAuctionFixed = false;
                            TickerInAuction = true;
                        }
                        break;

                    case "IntAuc[GV1_TEXT]":
                    case "ICALL [GV1_TEXT]":
                        TickerInAuction = true;
                        IntradayAuctionFixed = false;
                        IntradayAuctionStarted = true;
                        break;

                    case "ClsAuc[GV1_TEXT]":
                    case "CCALL [GV1_TEXT]":
                        if (!CloseAuctionStarted)
                        {
                            CloseTime = curTime;

                            foreach (OneMinBar Bar in IntradayBarsList[curTicker][curDate].Values)
                            {
                                Bar.RegressiveMinute = (new TimeSpan(CloseTime.Hours, CloseTime.Minutes, 0) - new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes - (int)Bar.Minute;
                                if (Bar.RegressiveMinute < 0)
                                { }
                            }
                        }
                        TickerInAuction = true;
                        CloseAuctionFixed = false;
                        CloseAuctionStarted = true;
                        break;

                    case "A[ACT_FLAG1]":
                        if (!(UnusualAuctions > 0))
                        {
                            if (!OpenAuctionEnded)
                            {
                                OpenTime = curTime;
                                OpenAuctionEnded = true;
                            }
                            else if (!IntradayAuctionEnded)
                            {
                                IntradayAuctionEnded = true;
                            }
                            else if (!CloseAuctionEnded)
                            {
                                CloseAuctionEnded = true;
                            }
                        }
                        else
                        { UnusualAuctions--; }

                        break;
                    case "TRADE [GV1_TEXT]":
                        if ((UnusualAuctions > 0))
                        {
                            UnusualAuctions = 0;
                        }

                        if (curType == "Trade")
                        {
                            if (OpenAuctionStarted && !OpenAuctionEnded)
                            {
                                OpenAuctionFixed = OpenAuctionEnded = true;
                                TickerInAuction = false;
                                OpenTime = curTime;
                            }
                            else if (IntradayAuctionStarted && !IntradayAuctionEnded)
                            {
                                IntradayAuctionFixed = IntradayAuctionEnded = true;
                                TickerInAuction = false;
                            }
                            else if (CloseAuctionStarted && !CloseAuctionEnded)
                            {
                                CloseAuctionFixed = CloseAuctionEnded = true;
                                TickerInAuction = false;
                            }
                        }


                        break;
                    case "VOLA  [GV1_TEXT]":
                        UnusualAuctions++;
                        break;

                }


                if (prevTicker != curTicker)
                {
                    OpenAuctionEnded = false;
                    OpenAuctionFixed = false;
                    IntradayAuctionEnded = false;
                    CloseAuctionEnded = false;
                    CloseAuctionStarted = false;
                    UnusualAuctions = 0;
                    IntradayBarsList.Add(curTicker, new Dictionary<DateTime, Dictionary<int, OneMinBar>>());
                    OpenTime = CloseTime = new TimeSpan(0, 0, 0);
                }

                if (prevDate != curDate)
                {
                    OpenAuctionEnded = false;
                    OpenAuctionFixed = false;
                    IntradayAuctionEnded = false;
                    CloseAuctionEnded = false;
                    CloseAuctionStarted = false;
                    UnusualAuctions = 0;
                    IntradayBarsList[curTicker].Add(curDate, new Dictionary<int, OneMinBar>());
                    OpenTime = CloseTime = new TimeSpan(0, 0, 0);
                }

                if (curState.Contains("Open") && OpenTime != curTime)
                {
                    OpenTime = curTime;
                }

                // ======================= MINUTE CHANGED =======================

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

                        if (curBar.Minute < 0)
                        { }

                        IntradayBarsList[curTicker][curDate].Add((int)curBar.Minute, curBar);

                        curBar = new OneMinBar();
                        FirstSpreadFixed = false;
                        cumSpread = 0;

                    }

                    IntradayStarted = false;
                }

                if (curType == "Trade")
                {

                    if (TickerInAuction)
                    {
                        // =========================  SEARCH FOR OPENING AUCTION ==========================================  //

                        if (OpenAuctionEnded && OpenAuctionStarted && !OpenAuctionFixed)
                        {
                            //Gravador.Grava_Log(tempLine + ",OA");
                            curOpenData.Ticker = curTicker;
                            curOpenData.Date = curDate;
                            curOpenData.Volume = curVolume;
                            curOpenData.Value = curPrice * curVolume;
                            curOpenData.Price = curPrice;
                            curOpenData.EndTimeGMT = curTime;
                            curOpenData.EndTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                            curOpenData.Minute = curTime.Subtract(new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                            curOpenData.TotalTrades = 1;

                            OpenAuctionList.Add(curOpenData);

                            curOpenData = new AuctionData();

                            OpenAuctionFixed = true;
                            TickerInAuction = false;
                        }

                        // =========================  SEARCH FOR INTRADAY AUCTION ==========================================  //

                        if (IntradayAuctionStarted && !IntradayAuctionFixed)
                        {
                            curIntradayData.Ticker = curTicker;
                            curIntradayData.Date = curDate;
                            curIntradayData.PreAuctionPrice = prevPrice;
                            curIntradayData.TotalTrades = 1;
                            curIntradayData.Minute = curTime.Subtract(new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                            curIntradayData.StartTimeGMT = curTime;
                            curIntradayData.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                        }

                        if (IntradayAuctionStarted && IntradayAuctionEnded && !IntradayAuctionFixed)
                        {
                            //Gravador.Grava_Log(tempLine + ",IA");
                            curIntradayData.Value = curVolume * curPrice;
                            curIntradayData.Volume = curVolume;
                            curIntradayData.Price = curPrice;
                            curIntradayData.EndTimeGMT = curTime;
                            curIntradayData.EndTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));

                            IntradayAuctionList.Add(curIntradayData);
                            curIntradayData = new AuctionData();

                            IntradayAuctionFixed = true;
                            TickerInAuction = false;
                        }

                        // =========================  SEARCH FOR CLOSE AUCTION ==========================================  //

                        if (CloseAuctionStarted && !CloseAuctionFixed)
                        {
                            curCloseData.Ticker = curTicker;
                            curCloseData.Date = curDate;
                            curCloseData.PreAuctionPrice = prevPrice;
                            curCloseData.TotalTrades = 1;
                            curCloseData.Minute = curTime.Subtract(new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                            curCloseData.StartTimeGMT = curTime;
                            curCloseData.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                        }

                        if (CloseAuctionStarted && CloseAuctionEnded && !CloseAuctionFixed)
                        {
                            //Gravador.Grava_Log(tempLine + ",CA");
                            curCloseData.Value = curVolume * curPrice;
                            curCloseData.Volume = curVolume;
                            curCloseData.Price = curPrice;
                            curCloseData.EndTimeGMT = curTime;
                            curCloseData.EndTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));

                            CloseAuctionList.Add(curCloseData);
                            curCloseData = new AuctionData();                            

                            CloseAuctionFixed = true;
                            TickerInAuction = false;

                        }

                    }
                    else
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
        }     */
    }

    public class DateInfo
    {
        public TimeSpan OpenTimeGMT = new TimeSpan();
        public int TimeShift = new int();
        public int TotalMinutesOpened = 0;
    }
}
