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

namespace RTICK_GenerateVWAPBars
{
    public partial class Form1 : Form
    {
        List<OpenData> OpenList = new List<OpenData>();
        List<CloseData> CloseList = new List<CloseData>();
        List<IntradayBar> First30List = new List<IntradayBar>();
        List<IntradayBar> Last30List = new List<IntradayBar>();
        List<IntradayBar> FirstOpen30List = new List<IntradayBar>();
        List<IntradayBar> Until15_30BarList = new List<IntradayBar>();

        TimerCallback tmrCBStart;
        System.Threading.Timer tmrStart;        
        

        string ShowTicker = "";
        DateTime ShowDate = new DateTime(1900,01,01);
        DateTime InitialTime = new DateTime(1900, 01, 01);
        DateTime EndTime = new DateTime(1900, 01, 01);

        SortedDictionary<DateTime, TimeSpan> OpenTimes = new SortedDictionary<DateTime, TimeSpan>();
        SortedDictionary<DateTime, TimeSpan> CloseTimes = new SortedDictionary<DateTime, TimeSpan>();        
         
        public Form1()
        {
            InitializeComponent();         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrCBStart = new TimerCallback(RunFile);
            tmrStart = new System.Threading.Timer(tmrCBStart, null, 1000, System.Threading.Timeout.Infinite);

            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = ShowDate.ToString("dd-MMM-yyyy");
            lblTicker.Text = ShowTicker;

            DateTime UseTime = DateTime.Now;

            if (EndTime < UseTime) UseTime = EndTime;

            if (InitialTime != new DateTime(1900, 01, 01)) lblTimeTaken.Text = (EndTime.Subtract(InitialTime.TimeOfDay)).ToString("mm:ss");

            if (ShowTicker == "Finished")
            {
                tmrStart.Dispose();
                Application.ExitThread();
            }
        }

        private void LoadOpenTimes()
        {
            FileStream fs = new FileStream(@"C:\TEMP\refOpenTimes.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string tempLine;

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curValues = tempLine.Split(';');
                OpenTimes.Add(DateTime.Parse(curValues[0].ToString()), TimeSpan.Parse(curValues[1].ToString()));
                CloseTimes.Add(DateTime.Parse(curValues[0].ToString()), TimeSpan.Parse(curValues[2].ToString()));
            }

            for (int i = DateTime.Today.DayOfYear - OpenTimes.Last().Key.DayOfYear - 1; i >= 1; i--)
            {
                OpenTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(13, 0, 0));
                CloseTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(20, 0, 0));
            }
        }


        private void cmdRunFile_Click(object sender, EventArgs e)
        {
            cmdRunFile.Enabled = false;

            Thread t1 = new Thread(RunFile);

            t1.Start();

            cmdRunFile.Enabled = true;
        }

        private void RunFile(object obj)
        {
            //ProcessFileVlad(@"U:\Vladimir Timerman\luis.fonseca@nestinvestimentos.com.br-indice1min-N25043377.csv");
            LoadOpenTimes();
            //InitialTime = DateTime.Now;

            //ProcessTest();
            ProcessFolder();
            InsertFileInDataBase();
            ShowTicker = "Finished";
            
            EndTime = DateTime.Now;
        }

        private void ProcessTest()
        {
            //ProcessFileVlad(@"U:\Vladimir Timerman\luis.fonseca@nestinvestimentos.com.br-indicehora-N24521765.csv");
            //ProcessFile2(@"C:\TEMP\PETR4_1.csv");
            LoadOpenTimes();
            ProcessFile(@"C:\Temp\VALE.txt");
            //ProcessFile(@"R:\RTICK\luis.fonseca@nestinvestimentos.com.br-BZDATA-N24375114-part001.csv");
            //ProcessFile2(@"C:\TEMP\BBDC4_1.csv");
            PrintDataTofile(@"C:\temp\");
        }

        private void ProcessFolder()
        {
            string CurFolder = @"R:\RTICK\Daily Requests\Unprocessed\Requests\Uncompressed\";
            //string CurFolder = @"R:\RTICK\Other Requests\Unprocessed\Requests\";
            //string curNewFolder = @"R:\RTICK\Daily Requests\Processed\Requests\";

            //string CurFolder = @"R:\RTICK\Other Requests\Unprocessed\Requests";
            //string curNewFolder = @"R:\RTICK\Other Requests\Processed\Requests";

            string[] fileEntries = Directory.GetFiles(CurFolder);

            foreach (string fileName in fileEntries)
            {
                if(fileName.Contains(".csv"))
                {
                    ProcessFile(fileName);                    
                    
                    File.Move(fileName, fileName.Replace("Unprocessed","Processed").Remove(fileName.IndexOf("Uncompressed"),14));
                    //File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                    PrintDataTofile(@"R:\RTICK\Daily Requests\Unprocessed\DataBase_Files\" + fileName.Substring(fileName.IndexOf('-') + 1).Replace(".csv", ""));
                    //PrintDataTofile(@"R:\RTICK\Other Requests\Unprocessed\DataBase_Files\" + fileName.Substring(fileName.IndexOf('-') + 1).Replace(".csv", ""));
                    EndTime = DateTime.Now;                    
                }                
            }
            //ShowTicker = "Finished";
        }

        private void ProcessFile(string curFileName)
        {                        
            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string tempLine = "";

            tempLine = sr.ReadLine();
            string[] curHeaders = tempLine.Split(',');

            // ==================== INITIALIZING VARIABLES =======================================

            // ====================== Sequence Control
            bool foundOpenTime = false;
            bool OpenAuctionEnded = false;
            bool ClosingAuctionStarted = false;
            bool OpenAuctionStarted = false;
            bool barFirst30Started = false;
            bool barFirst30Ended = false;
            bool barLast30Started = false;
            bool barLast30Ended = false;
            bool barFirstOpen30Started = false;
            bool barFirstOpen30Ended = false;
            bool barUntil15_30Started = false;
            bool barUntil15_30Ended = false;
            bool AuctionInserted = false;

            // ====================== Accumulators and Prev Values
            TimeSpan OpenTime = new TimeSpan(0, 0, 0);
            TimeSpan CloseTime = new TimeSpan(0, 0, 0);
            Int64 cumVolume = 0;
            double cumValue = 0;
            Int64 cumClosingVolume = 0;
            double cumClosingValue = 0;

            string prevTicker = "";
            Int64 prevVolume = 0;
            double prevPrice = 0;
            DateTime prevDate = new DateTime(1900, 01, 01);
            TimeSpan prevTime = new TimeSpan(0, 0, 0);
            double prevCumValue = 0;
            Int64 prevCumVolume = 0;
                        
            // ====================== Current Values
            DateTime curDate;
            TimeSpan curTime;
            int curTimeShift = 0;
            double curPrice;
            Int64 curVolume;
            String curState ;
            String curType;
            String curTicker;

            double curBid = 0;
            double curAsk = 0;

            // ====================== Variables To Save in DataBase
            OpenData curOpenData = new OpenData();
            CloseData curCloseData = new CloseData();
            IntradayBar First30Bar = new IntradayBar();
            IntradayBar Last30Bar = new IntradayBar();
            IntradayBar FirstOpen30Bar = new IntradayBar();
            IntradayBar Until15_30Bar = new IntradayBar();

            // ==================================================================================

            //StreamWriter curs = new StreamWriter(@"C:\TEMP\teste.txt");
            //curs.Close();           
            try
            {
                while ((tempLine = sr.ReadLine()) != null)
                {
                    //int posVolume = 6; int posLast = 5; int posState = 14;
                    int posVolume = 8; int posLast = 7; int posState = 14;
                    //int posVolume = 9; int posLast = 8; int posState = 12;
                    //curs.WriteLine(tempLine);
                    string[] curValues = tempLine.Split(',');
                    curTicker = curValues[0];
                    if (curValues[1] != "")
                    {
                        curValues[1] = curValues[1].Substring(0, 4) + "/" + curValues[1].Substring(4, 2) + "/" + curValues[1].Substring(6, 2);
                    }
                    curDate = new DateTime(1900, 01, 01); if (curValues[1] != "") curDate = DateTime.Parse(curValues[1]);
                    curTime = new TimeSpan(0, 0, 0);
                    try
                    {
                        if (curValues[2] != "")
                        {
                            if (curValues[2].Contains("_QL_"))
                            {
                                curValues[2] = curValues[2].Insert(curValues[2].IndexOf("_QL_"), ".");
                                curValues[2] = curValues[2].Remove(curValues[2].IndexOf("_QL_"), 5);
                            }
                            TimeSpan.Parse(curValues[2]);
                        }
                    }
                    catch (Exception E)
                    {
                        string e = E.Message;
                    }
                    curTimeShift = 0; if (curValues[3] != "") curTimeShift = int.Parse(curValues[3]);
                    curType = curValues[4];
                    curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".", ","));
                    curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                    curState = curValues[posState];

                    ShowDate = curDate;
                    ShowTicker = curTicker;

                    //if (curDate == new DateTime(2005, 04, 25))
                    //{

                    //}

                    if (curState.Contains("ODD") || curState.Contains("241[IRGCOND]"))
                    {
                        curState = "ODD";
                    }

                    if (curType == "Trade" && curState != "ODD")
                    {
                        cumVolume += curVolume;
                        cumValue += curVolume * curPrice;
                    }

                    if (prevDate != curDate)
                    {
                        int i = 0;
                    }

                    if (prevDate != curDate || prevTicker != curTicker)
                    {
                        // ======================= DATE CHANGED =======================

                        // Save Close Data

                        curCloseData.AuctionVolume = cumClosingVolume;
                        curCloseData.AuctionValue = cumClosingValue;
                        curCloseData.AuctionPrice = cumClosingValue / cumClosingVolume;
                        curCloseData.endTimeGMT = prevTime;
                        curCloseData.endTimeLocal = prevTime.Add(new TimeSpan(curTimeShift, 0, 0));

                        if (!barUntil15_30Ended)
                        {
                            Until15_30Bar.Close = curPrice;
                            Until15_30Bar.VWAP = Until15_30Bar.Value / Until15_30Bar.Volume;
                            barUntil15_30Ended = true;
                        }

                        if (!barFirstOpen30Ended)
                        {
                            FirstOpen30Bar.Close = curPrice;
                            FirstOpen30Bar.VWAP = FirstOpen30Bar.Value / FirstOpen30Bar.Volume;
                            barFirstOpen30Ended = true;
                        }

                        if (!barFirst30Ended && OpenAuctionEnded)
                        {
                            First30Bar.Close = prevPrice;
                            First30Bar.VWAP = First30Bar.Value / First30Bar.Volume;
                            barFirst30Ended = true;
                        }

                        //if (curCloseData.Date == new DateTime(2005, 04, 25))
                        //{

                        //}

                        if (OpenTime != new TimeSpan(0, 0, 0) && CloseTime != new TimeSpan(0, 0, 0))
                        {
                            if (curOpenData.Ticker != null && curOpenData.AuctionVolume != 0) OpenList.Add(curOpenData);
                            if (curCloseData.Ticker != null && curCloseData.AuctionVolume != 0) CloseList.Add(curCloseData);
                            if (First30Bar.Ticker != null && First30Bar.Volume != 0) First30List.Add(First30Bar);
                            if (Last30Bar.Ticker != null && Last30Bar.Volume != 0) Last30List.Add(Last30Bar);
                            if (FirstOpen30Bar.Ticker != null && FirstOpen30Bar.Volume != 0) FirstOpen30List.Add(FirstOpen30Bar);
                            if (Until15_30Bar.Ticker != null && Until15_30Bar.Volume != 0) Until15_30BarList.Add(Until15_30Bar);
                        }
                        // Reset everything

                        cumVolume = 0;
                        cumValue = 0;
                        prevVolume = 0;

                        prevPrice = 0;
                        curBid = 0;
                        curAsk = 0;


                        foundOpenTime = false;
                        OpenAuctionStarted = false;
                        OpenAuctionEnded = false;
                        ClosingAuctionStarted = false;
                        barFirst30Ended = false;
                        barFirst30Started = false;
                        barLast30Ended = false;
                        barLast30Started = false;
                        barFirstOpen30Ended = false;
                        barFirstOpen30Started = false;
                        barUntil15_30Started = false;
                        barUntil15_30Ended = false;
                        AuctionInserted = false;


                        // Reinitialize
                        curOpenData = new OpenData();
                        curCloseData = new CloseData();
                        Last30Bar = new IntradayBar();
                        First30Bar = new IntradayBar();
                        FirstOpen30Bar = new IntradayBar();
                        Until15_30Bar = new IntradayBar();

                        curOpenData.Date = curDate;
                        curOpenData.Ticker = curTicker;
                        curCloseData.Date = curDate;
                        curCloseData.Ticker = curTicker;

                        prevDate = curDate;
                        prevTicker = curTicker;
                    }

                    if (OpenTimes.ContainsKey(curDate))
                    {
                        OpenTime = OpenTimes[curDate];
                        CloseTime = CloseTimes[curDate];
                    }
                    else
                    {
                        OpenTime = new TimeSpan(0, 0, 0);
                        CloseTime = new TimeSpan(0, 0, 0);
                    }

                    if (curDate == new DateTime(2011, 07, 07))
                    {

                    }

                    if (OpenTime != new TimeSpan(0, 0, 0))
                    {
                        if (prevDate == curDate)
                        {
                            if (curType == "Trade" && curState != "ODD")
                            {
                                // =========================  SEARCH FOR CLOSING AUCTION ==========================================
                                if (curTime > new TimeSpan(CloseTime.Hours, 0, 0).Add(new TimeSpan(0, 0, -5)) && OpenAuctionEnded)
                                {
                                    if (!ClosingAuctionStarted)
                                    {
                                        Last30Bar.Close = prevPrice;
                                        Last30Bar.VWAP = Last30Bar.Value / Last30Bar.Volume;
                                        barLast30Ended = true;

                                        curCloseData.PreAuctionPrice = prevPrice;
                                        cumClosingVolume = 0;
                                        cumClosingValue = 0;
                                        ClosingAuctionStarted = true;
                                        cumClosingVolume += curVolume;
                                        cumClosingValue += curVolume * curPrice;
                                    }
                                    else
                                    {
                                        cumClosingVolume += curVolume;
                                        cumClosingValue += curVolume * curPrice;
                                    }
                                }

                                // =========================  SEARCH FOR OPENING AUCTION ==========================================

                                if (!OpenAuctionEnded)
                                {
                                    if (OpenAuctionStarted && (curPrice != prevPrice))
                                    {
                                        curOpenData.AuctionVolume = prevCumVolume;
                                        curOpenData.AuctionValue = cumValue - (curVolume * curPrice);
                                        curOpenData.AuctionPrice = curOpenData.AuctionValue / curOpenData.AuctionVolume;
                                        curOpenData.openTimeGMT = prevTime;
                                        curOpenData.openTimeLocal = prevTime.Add(new TimeSpan(curTimeShift, 0, 0));

                                        OpenAuctionEnded = true;

                                    }

                                    if (cumVolume > 0 && prevVolume == 0)
                                    {
                                        OpenAuctionStarted = true;

                                    }
                                }

                                // ================= CREATE UNTIL 15:30 INTERVAL BARS CONTAINING OPENING AUCTION DATA ================

                                if (curTime < new TimeSpan(OpenTime.Hours + 5, 30, 0))
                                {

                                    if (!barUntil15_30Started)
                                    {

                                        Until15_30Bar.Ticker = curTicker;
                                        Until15_30Bar.Date = curDate;
                                        Until15_30Bar.StartTimeGMT = curTime;
                                        Until15_30Bar.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                                        Until15_30Bar.BarSizeMins = 30;
                                        Until15_30Bar.Open = curPrice;
                                        Until15_30Bar.Close = curPrice;
                                        Until15_30Bar.High = curPrice;
                                        Until15_30Bar.Low = curPrice;
                                        Until15_30Bar.TotalTrades++;
                                        if (curPrice == curBid) Until15_30Bar.BidTrades++;
                                        if (curPrice == curAsk) Until15_30Bar.AskTrades++;
                                        Until15_30Bar.Volume += curVolume;
                                        Until15_30Bar.Value += curVolume * curPrice;
                                        barUntil15_30Started = true;

                                    }
                                    else
                                    {
                                        Until15_30Bar.TotalTrades++;
                                        if (curPrice == curBid) Until15_30Bar.BidTrades++;
                                        if (curPrice == curAsk) Until15_30Bar.AskTrades++;
                                        if (curPrice > Until15_30Bar.High) Until15_30Bar.High = curPrice;
                                        if (curPrice < Until15_30Bar.Low) Until15_30Bar.Low = curPrice;

                                        Until15_30Bar.Volume += curVolume;
                                        Until15_30Bar.Value += curVolume * curPrice;

                                    }
                                }
                                else
                                {
                                    if (!barUntil15_30Ended)
                                    {
                                        Until15_30Bar.Close = curPrice;
                                        Until15_30Bar.VWAP = Until15_30Bar.Value / Until15_30Bar.Volume;
                                        barUntil15_30Ended = true;
                                    }
                                }

                                // ================= CREATE FIRST 30 MINS INTERVAL BARS CONTAINING OPENING AUCTION DATA ================

                                if (curTime < new TimeSpan(OpenTime.Hours, 30, 0))
                                {

                                    if (!barFirstOpen30Started)
                                    {

                                        FirstOpen30Bar.Ticker = curTicker;
                                        FirstOpen30Bar.Date = curDate;
                                        FirstOpen30Bar.StartTimeGMT = curTime;
                                        FirstOpen30Bar.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                                        FirstOpen30Bar.BarSizeMins = 30;
                                        FirstOpen30Bar.Open = curPrice;
                                        FirstOpen30Bar.Close = curPrice;
                                        FirstOpen30Bar.High = curPrice;
                                        FirstOpen30Bar.Low = curPrice;
                                        FirstOpen30Bar.TotalTrades++;
                                        if (curPrice == curBid) FirstOpen30Bar.BidTrades++;
                                        if (curPrice == curAsk) FirstOpen30Bar.AskTrades++;
                                        FirstOpen30Bar.Volume += curVolume;
                                        FirstOpen30Bar.Value += curVolume * curPrice;
                                        barFirstOpen30Started = true;

                                    }
                                    else
                                    {
                                        FirstOpen30Bar.TotalTrades++;
                                        if (curPrice == curBid) FirstOpen30Bar.BidTrades++;
                                        if (curPrice == curAsk) FirstOpen30Bar.AskTrades++;
                                        if (curPrice > FirstOpen30Bar.High) FirstOpen30Bar.High = curPrice;
                                        if (curPrice < FirstOpen30Bar.Low) FirstOpen30Bar.Low = curPrice;

                                        FirstOpen30Bar.Volume += curVolume;
                                        FirstOpen30Bar.Value += curVolume * curPrice;

                                    }
                                }
                                else
                                {
                                    if (!barFirstOpen30Ended)
                                    {
                                        FirstOpen30Bar.Close = curPrice;
                                        FirstOpen30Bar.VWAP = FirstOpen30Bar.Value / FirstOpen30Bar.Volume;
                                        barFirstOpen30Ended = true;
                                    }
                                }



                                // ============================= CREATE FIRST 30 MINS INTERVAL BARS ================================================

                                if (curTime < new TimeSpan(OpenTime.Hours, 30, 0) && OpenAuctionEnded)
                                {
                                    if (!barFirst30Started)
                                    {
                                        if (OpenAuctionEnded && !ClosingAuctionStarted)
                                        {
                                            First30Bar.Ticker = curTicker;
                                            First30Bar.Date = curDate;
                                            First30Bar.StartTimeGMT = curTime;
                                            First30Bar.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                                            First30Bar.BarSizeMins = 30;
                                            First30Bar.Open = curPrice;
                                            First30Bar.Close = curPrice;
                                            First30Bar.High = curPrice;
                                            First30Bar.Low = curPrice;
                                            First30Bar.TotalTrades++;
                                            if (curPrice == curBid) First30Bar.BidTrades++;
                                            if (curPrice == curAsk) First30Bar.AskTrades++;
                                            First30Bar.Volume += curVolume;
                                            First30Bar.Value += curVolume * curPrice;
                                            barFirst30Started = true;
                                        }
                                    }
                                    else
                                    {
                                        First30Bar.TotalTrades++;
                                        if (curPrice == curBid) First30Bar.BidTrades++;
                                        if (curPrice == curAsk) First30Bar.AskTrades++;
                                        if (curPrice > First30Bar.High) First30Bar.High = curPrice;
                                        if (curPrice < First30Bar.Low) First30Bar.Low = curPrice;

                                        First30Bar.Volume += curVolume;
                                        First30Bar.Value += curVolume * curPrice;
                                    }
                                }
                                else
                                {
                                    if (!barFirst30Ended && OpenAuctionEnded)
                                    {
                                        First30Bar.Close = prevPrice;
                                        First30Bar.VWAP = First30Bar.Value / First30Bar.Volume;
                                        barFirst30Ended = true;
                                    }
                                }

                                // ============================= CREATE LAST 30 MINS INTERVAL BARS ================================================

                                if (curTime > new TimeSpan(CloseTime.Hours, -30, 0) && !ClosingAuctionStarted)
                                {
                                    if (!barLast30Started)
                                    {
                                        Last30Bar.Ticker = curTicker;
                                        Last30Bar.Date = curDate;
                                        Last30Bar.StartTimeGMT = curTime;
                                        Last30Bar.StartTimeLocal = curTime.Add(new TimeSpan(curTimeShift, 0, 0));
                                        Last30Bar.BarSizeMins = 30;
                                        Last30Bar.Open = curPrice;
                                        Last30Bar.Close = curPrice;
                                        Last30Bar.High = curPrice;
                                        Last30Bar.Low = curPrice;
                                        Last30Bar.TotalTrades++;
                                        if (curPrice == curBid) Last30Bar.BidTrades++;
                                        if (curPrice == curAsk) Last30Bar.AskTrades++;
                                        Last30Bar.Volume += curVolume;
                                        Last30Bar.Value += curVolume * curPrice;
                                        barLast30Started = true;

                                    }
                                    else
                                    {
                                        Last30Bar.TotalTrades++;
                                        if (curPrice == curBid) Last30Bar.BidTrades++;
                                        if (curPrice == curAsk) Last30Bar.AskTrades++;
                                        if (curPrice > Last30Bar.High) Last30Bar.High = curPrice;
                                        if (curPrice < Last30Bar.Low) Last30Bar.Low = curPrice;

                                        Last30Bar.Volume += curVolume;
                                        Last30Bar.Value += curVolume * curPrice;
                                    }
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


                            if (curType == "Quote")
                            {
                                curBid = 0; if (curValues[10] != "") curBid = double.Parse(curValues[10].Replace(".", ","));
                                curAsk = 0; if (curValues[12] != "") curAsk = double.Parse(curValues[12].Replace(".", ","));
                            }
                        }

                    }

                }
            }
            catch (Exception E)
            {
                string e = E.Message;
            }


            // Save Close Data            

            sr.Close();

            curCloseData.AuctionVolume = cumClosingVolume;
            curCloseData.AuctionValue = cumClosingValue;
            curCloseData.AuctionPrice = cumClosingValue / cumClosingVolume;
            curCloseData.endTimeGMT = prevTime;
            curCloseData.endTimeLocal = prevTime.Add(new TimeSpan(curTimeShift, 0, 0));

            if (curOpenData.Ticker != null && curOpenData.AuctionVolume != 0) OpenList.Add(curOpenData);
            if (curCloseData.Ticker != null && curCloseData.AuctionVolume != 0) CloseList.Add(curCloseData);
            if (First30Bar.Ticker != null && First30Bar.Volume != 0) First30List.Add(First30Bar);
            if (Last30Bar.Ticker != null && Last30Bar.Volume != 0) Last30List.Add(Last30Bar);
            if (FirstOpen30Bar.Ticker != null && FirstOpen30Bar.Volume != 0) FirstOpen30List.Add(FirstOpen30Bar);
            if (Until15_30Bar.Ticker != null && Until15_30Bar.Volume != 0) Until15_30BarList.Add(Until15_30Bar);


        }

        private void ProcessFile2(string curFileName)
        {

            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string tempLine = "";

            StreamWriter sw3 = new StreamWriter(@"C:\TEMP\VALE.txt");
            //StreamWriter sw = new StreamWriter(@"C:\TEMP\OpenTimes_" + curFileName.Substring(0, curFileName.IndexOf('.')).Substring(curFileName.LastIndexOf('\\') + 1) + ".txt");
            //StreamWriter sw2 = new StreamWriter(@"C:\TEMP\ViewDay_" + curFileName.Substring(0, curFileName.IndexOf('.')).Substring(curFileName.LastIndexOf('\\') + 1) + ".txt");

            tempLine = sr.ReadLine();
            string[] curHeaders = tempLine.Split(',');

            // ==================== INITIALIZING VARIABLES =======================================
            
            
            // ====================== Accumulators and Prev Values
            TimeSpan OpenTime = new TimeSpan(0, 0, 0);
            TimeSpan LastTradeTime = new TimeSpan(0, 0, 0);
            
            DateTime prevDate = new DateTime(1900, 01, 01);
            TimeSpan prevTime = new TimeSpan(0, 0, 0);
            
            // ====================== Current Values
            DateTime curDate;
            TimeSpan curTime;
            int curTimeShift = 0;

            // ====================== Variables To Save in DataBase
            OpenData curOpenData = new OpenData();
            CloseData curCloseData = new CloseData();
            IntradayBar First30Bar = new IntradayBar();
            IntradayBar Last30Bar = new IntradayBar();
            Int64 curVolume = 0;
            // ==================================================================================

            StreamWriter curs = new StreamWriter(@"C:\TEMP\teste.txt");
            curs.Close();

            TimeSpan tempTime = new TimeSpan(0, 0, 0);
            bool FoundControl = false;

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curValues = tempLine.Split(',');
                curDate = new DateTime(1900, 01, 01); if (curValues[1] != "") curDate = DateTime.Parse(curValues[1]);
                curTime = new TimeSpan(0, 0, 0); if (curValues[2] != "") curTime = TimeSpan.Parse(curValues[2]);
                curVolume = 0; if (curValues[6] != "") curVolume = Int64.Parse(curValues[6]);
                
                curTimeShift = 0; if (curValues[3] != "") curTimeShift = int.Parse(curValues[3]);
                string curType = curValues[4];
                string curState = curValues[12];

                ShowDate = curDate;

                if (curValues[0] == "VALE5.SA" && curDate>=new DateTime (2011,07,07))
                {
                    sw3.WriteLine(tempLine);
                }

                if (prevDate != curDate)
                {
                    //Console.WriteLine(prevDate.ToString("yyyy-MM-dd") + ";" + tempTime);
                    //sw.WriteLine(prevDate.ToString("yyyy-MM-dd") + ";" + tempTime.ToString() + ";" + LastTradeTime.ToString());
                    //Linecounter = 0;
                    //FoundControl = false;
                }

                prevDate = curDate;
                prevTime = curTime;

                if (curType == "Trade" && curState != "ODD" && curVolume!=0)
                {
                    LastTradeTime = curTime;

                    if (!FoundControl)
                    {
                        tempTime = prevTime;
                        FoundControl = true;
                    }
                }
            }
            //sw.Close();
            //sw2.Close();
            sw3.Close();
        }

        private void ProcessFileVlad(string curFileName)
        {

            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string tempLine = "";

            

            tempLine = sr.ReadLine();
            string[] curHeaders = tempLine.Split(',');

            StreamWriter sw = new StreamWriter(@"U:\Vladimir Timerman\CleanFile.txt");

            // ==================== INITIALIZING VARIABLES =======================================



            // ====================== Current Values
            DateTime curDate;
            TimeSpan curTime;
            int curTimeShift = 0;
            
            // ==================================================================================

            //StreamWriter curs = new StreamWriter(@"C:\TEMP\teste.txt");
            //curs.Close();

            TimeSpan tempTime = new TimeSpan(0, 0, 0);

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curValues = tempLine.Split(',');
                curDate = new DateTime(1900, 01, 01); if (curValues[1] != "") curDate = DateTime.Parse(curValues[1]);
                curTime = new TimeSpan(0, 0, 0); if (curValues[2] != "") curTime = TimeSpan.Parse(curValues[2]);

                ShowDate = curDate;

                if (curDate > new DateTime(2006, 01, 01) && curTime > new TimeSpan(12, 0, 0) && curTime < new TimeSpan(21, 1, 0) && curDate.DayOfWeek != DayOfWeek.Saturday && curDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    sw.WriteLine(tempLine.Replace(".000", "").Replace(',', ';').Replace('.', ','));
                }
            }
            sw.Close();
        }

        private void PrintDataTofile(string FileHeader)
        {
            StreamWriter swOpen = new StreamWriter(FileHeader + "_OpenBars.txt");
            StreamWriter swClose = new StreamWriter(FileHeader + "_CloseBars.txt");
            StreamWriter swOpen30 = new StreamWriter(FileHeader + "_Open30Bars.txt");
            StreamWriter swClose30 = new StreamWriter(FileHeader + "_Close30Bars.txt");
            StreamWriter swOpenFirst30 = new StreamWriter(FileHeader + "_OpenFirst30Bars.txt");
            StreamWriter swUntil15_30 = new StreamWriter(FileHeader + "_Until15_30Bars.txt");

            foreach (OpenData curOpenData in OpenList)
            {
                string curLine = "";
                curLine += curOpenData.Date.ToString("yyyy-MM-dd") + ";";
                curLine += curOpenData.Ticker + ";";
                curLine += curOpenData.openTimeGMT.ToString().Replace(".", ",") + ";";
                curLine += curOpenData.openTimeLocal.ToString().Replace(".", ",") + ";";
                curLine += curOpenData.AuctionPrice.ToString().Replace(".", ",") + ";";
                curLine += curOpenData.AuctionVolume.ToString().Replace(".", ",") + ";";
                curLine += curOpenData.AuctionValue.ToString().Replace(".", ",");

                swOpen.WriteLine(curLine);
            }

            foreach (CloseData curCloseData in CloseList)
            {
                string curLine = "";
                curLine += curCloseData.Date.ToString("yyyy-MM-dd") + ";";
                curLine += curCloseData.Ticker + ";";
                curLine += curCloseData.endTimeGMT.ToString().Replace(".", ",") + ";";
                curLine += curCloseData.endTimeLocal.ToString().Replace(".", ",") + ";";
                curLine += curCloseData.PreAuctionPrice.ToString().Replace(".", ",") + ";";
                curLine += curCloseData.AuctionPrice.ToString().Replace(".", ",") + ";";
                curLine += curCloseData.AuctionVolume.ToString().Replace(".", ",") + ";";
                curLine += curCloseData.AuctionValue.ToString().Replace(".", ",");

                swClose.WriteLine(curLine);
            }

            foreach (IntradayBar curOpen30Data in First30List)
            {
                string curLine = "";
                curLine += curOpen30Data.Date.ToString("yyyy-MM-dd") + ";";
                curLine += curOpen30Data.Ticker + ";";
                curLine += curOpen30Data.BarSizeMins + ";";
                curLine += curOpen30Data.StartTimeGMT.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.StartTimeLocal.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.Open.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.High.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.Low.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.Close.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.VWAP.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.Volume.ToString().Replace(".", ",") + ";";
                curLine += curOpen30Data.Value.ToString().Replace(".", ",");
                curLine += curOpen30Data.TotalTrades.ToString().Replace(".", ",");
                curLine += curOpen30Data.BidTrades.ToString().Replace(".", ",");
                curLine += curOpen30Data.AskTrades.ToString().Replace(".", ",");

                swOpen30.WriteLine(curLine);
            }

            foreach (IntradayBar curClose30Data in Last30List)
            {
                string curLine = "";
                curLine += curClose30Data.Date.ToString("yyyy-MM-dd") + ";";
                curLine += curClose30Data.Ticker + ";";
                curLine += curClose30Data.BarSizeMins + ";";
                curLine += curClose30Data.StartTimeGMT.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.StartTimeLocal.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.Close.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.High.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.Low.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.Close.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.VWAP.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.Volume.ToString().Replace(".", ",") + ";";
                curLine += curClose30Data.Value.ToString().Replace(".", ",");
                curLine += curClose30Data.TotalTrades.ToString().Replace(".", ",");
                curLine += curClose30Data.BidTrades.ToString().Replace(".", ",");
                curLine += curClose30Data.AskTrades.ToString().Replace(".", ",");

                swClose30.WriteLine(curLine);
            }

            foreach (IntradayBar curFirstOpen30Data in FirstOpen30List)
            {
                string curLine = "";
                curLine += curFirstOpen30Data.Date.ToString("yyyy-MM-dd") + ";";
                curLine += curFirstOpen30Data.Ticker + ";";
                curLine += curFirstOpen30Data.BarSizeMins + ";";
                curLine += curFirstOpen30Data.StartTimeGMT.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.StartTimeLocal.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.Open.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.High.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.Low.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.Close.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.VWAP.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.Volume.ToString().Replace(".", ",") + ";";
                curLine += curFirstOpen30Data.Value.ToString().Replace(".", ",");
                curLine += curFirstOpen30Data.TotalTrades.ToString().Replace(".", ",");
                curLine += curFirstOpen30Data.BidTrades.ToString().Replace(".", ",");
                curLine += curFirstOpen30Data.AskTrades.ToString().Replace(".", ",");

                swOpenFirst30.WriteLine(curLine);

            }

            foreach (IntradayBar curUntil15_30Data in Until15_30BarList)
            {
                string curLine = "";
                curLine += curUntil15_30Data.Date.ToString("yyyy-MM-dd") + ";";
                curLine += curUntil15_30Data.Ticker + ";";
                curLine += curUntil15_30Data.BarSizeMins + ";";
                curLine += curUntil15_30Data.StartTimeGMT.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.StartTimeLocal.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.Open.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.High.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.Low.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.Close.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.VWAP.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.Volume.ToString().Replace(".", ",") + ";";
                curLine += curUntil15_30Data.Value.ToString().Replace(".", ",");
                curLine += curUntil15_30Data.TotalTrades.ToString().Replace(".", ",");
                curLine += curUntil15_30Data.BidTrades.ToString().Replace(".", ",");
                curLine += curUntil15_30Data.AskTrades.ToString().Replace(".", ",");

                swUntil15_30.WriteLine(curLine);

            }

            
            swOpen.Close();
            swClose.Close();
            swOpen30.Close();
            swClose30.Close();
            swOpenFirst30.Close();
            swUntil15_30.Close();


        }

        private void InsertFileInDataBase()
        {
            string CurFolder = @"R:\RTICK\Daily Requests\Unprocessed\DataBase_Files\";
            //string CurFolder = @"R:\RTICK\Other Requests\Unprocessed\DataBase_Files";
            string CurNewFolder = @"R:\RTICK\Daily Requests\Processed\DataBase_Files\";

            string[] fileEntries = Directory.GetFiles(CurFolder);

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains("Close30Bars.txt"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[1];

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }
                            else
                            { 
                            }

                            ShowTicker = TempTicker + "-Close30";
                            ShowDate = DateTime.Parse(curValues[0]);

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[10].Replace(",", ".") + ", '" + curValues[0] + "', 330, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                { 
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[9].Replace(",", ".") + ", '" + curValues[0] + "', 331, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                }

                if (fileName.Contains("Open30Bars.txt"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[1];
                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            ShowTicker = TempTicker + "-Open30";
                            ShowDate = DateTime.Parse(curValues[0]);

                            if (!double.IsNaN(IdSecurity))
                            {
                                string tempVWAP = curValues[9];
                                if (tempVWAP == "0")
                                {
                                    Int64 tempVolume = int.Parse(curValues[10]);
                                    double tempValue = double.Parse(curValues[11]);
                                    tempVWAP = (tempValue / tempVolume).ToString();
                                }

                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[10].Replace(",", ".") + ", '" + curValues[0] + "', 320, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[9].Replace(",", ".") + ", '" + curValues[0] + "', 321, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                            }
                            else
                            {
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                }

                if (fileName.Contains("CloseBars.txt"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[1];
                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            ShowTicker = TempTicker + "-Close";
                            ShowDate = DateTime.Parse(curValues[0]);

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[6].Replace(",", ".") + ", '" + curValues[0] + "', 310, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[5].Replace(",", ".") + ", '" + curValues[0] + "', 311, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[4].Replace(",", ".") + ", '" + curValues[0] + "', 312, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                            }
                            else
                            {
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                }

                if (fileName.Contains("OpenBars.txt"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[1];
                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            ShowTicker = TempTicker + "-Open";
                            ShowDate = DateTime.Parse(curValues[0]);

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[5].Replace(",", ".") + ", '" + curValues[0] + "', 300, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[4].Replace(",", ".") + ", '" + curValues[0] + "', 301, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                            }
                            else
                            {
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    File.Move(fileName,fileName.Replace("Unprocessed", "Processed"));
                }

                if (fileName.Contains("OpenFirst30Bars.txt"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[1];
                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            ShowTicker = TempTicker + "-OpenFirst30Min";
                            ShowDate = DateTime.Parse(curValues[0]);

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[10].Replace(",", ".") + ", '" + curValues[0] + "', 340, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[9].Replace(",", ".") + ", '" + curValues[0] + "', 341, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                            }
                            else
                            {
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                }

                if (fileName.Contains("Until15_30Bars.txt"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[1];
                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            ShowTicker = TempTicker + "Until_15:30";
                            ShowDate = DateTime.Parse(curValues[0]);

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[10].Replace(",", ".") + ", '" + curValues[0] + "', 350, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                                StringSQL = "INSERT INTO Tb050_Preco_Acoes_Onshore(IdSecurity,SrValue,SrDate,SrType,Source,automated) VALUES(" + IdSecurity + ", " + curValues[9].Replace(",", ".") + ", '" + curValues[0] + "', 351, 22, 1)";
                                Result = curConn.ExecuteNonQuery(StringSQL);
                                if (Result != 2)
                                {
                                }
                            }
                            else
                            {
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                }
            }
            
        }

        private class OpenData
        {
            public DateTime Date;
            public string Ticker;
            public double AuctionValue = 0;
            public double AuctionPrice = 0;
            public Int64 AuctionVolume = 0;
            public TimeSpan openTimeGMT = new TimeSpan(0, 0, 0);
            public TimeSpan openTimeLocal = new TimeSpan(0, 0, 0);

            public string ToString()
            {
                return Ticker + " " + Date.ToString("dd-MMM-yy");
            }
        }

        private class CloseData
        {
            public DateTime Date;
            public string Ticker;
            public double AuctionValue = 0;
            public double AuctionPrice = 0;
            public Int64 AuctionVolume = 0;
            public TimeSpan endTimeGMT = new TimeSpan(0, 0, 0);
            public TimeSpan endTimeLocal = new TimeSpan(0, 0, 0);
            public double PreAuctionPrice = 0;

            public string ToString()
            {
                return Ticker + " " + Date.ToString("dd-MMM-yy");
            }
        }

        private class IntradayBar
        {
            public DateTime Date;
            public string Ticker;
            public int BarSizeMins;
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

            public string ToString()
            {
                return Ticker + " " + Date.ToString("dd-MMM-yy") + " " + BarSizeMins;// +" " + StartTimeLocal.ToString("hh:mm:ss");
            }
        }       

    }
}
