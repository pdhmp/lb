using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using NestDLL;

namespace RTICKReaderIND
{
    public partial class Form1 : Form       
    {

        class TradingTime
        {
            public TimeSpan OpenTime;
            public TimeSpan CloseTime;
            public int TimeOffSet;
            public int TotalTrades;

            public TradingTime()
            {
                OpenTime = new TimeSpan();
                CloseTime = new TimeSpan();
                TimeOffSet = 0;
                TotalTrades = 0;
            }
        }

        List<AuctionData> OpenAuctionList = new List<AuctionData>();
        List<OneMinBar> IntradayBarsList = new List<OneMinBar>();

        List<DateTime> DataBaseDates;

        SortedDictionary<DateTime, TimeSpan> OpenTimes = new SortedDictionary<DateTime, TimeSpan>();
        SortedDictionary<DateTime, TimeSpan> CloseTimes = new SortedDictionary<DateTime, TimeSpan>();
        SortedDictionary<DateTime, double> TotalMinutes = new SortedDictionary<DateTime, double>();      

        Thread screenSyncThread;

        string Date = "";

        private volatile object syncScreen = new object();

        public Form1()
        {
            InitializeComponent();
            Thread RunThread = new Thread(new ThreadStart(Run));
            RunThread.Start();
        }



        private void UpdateScreen()
        {
                lock (syncScreen)
                {
                    txtDate.Text = Date;
                }
        }

        public void Run()
        {
            DataBaseDates = GetDBDates(new DateTime(2006, 01, 01));
            LoadOpenTimes();
            //ProcessFolder(@"R:\RTICK\OneMinuteBars\NewHistFiles\Unprocessed2\");
            ProcessFolder(@"R:\RTICK\Future Files\Unprocessed2\");
            
        }

        private void ProcessFolder(string curFolder)
        {
            string[] fileEntries = Directory.GetFiles(curFolder);

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains(".csv"))
                {
                    //ProcessFile(fileName);

                    SearchTradeTimes(fileName);

                    //File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));
                    File.Move(fileName, fileName.Replace("Unprocessed2", "Processed"));

                    PrintDataToFile(fileName);
                }
            }

            InsertFileInDatabase(@"C:\temp\RTICK\DataBaseFiles\INDFiles\Unprocessed");
        }

        private void ProcessFile(string file)
        {
            StreamReader srReader = new StreamReader(file);

            char[] sep = {','};

            string[] headers = srReader.ReadLine().Split(sep);

            int posTicker, posPrice, posDate, posTime, posTimeOffSet, posType, posQualifier, posVolume, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeOffSet = posType = posQualifier = posVolume = quoteBid = quoteAsk = 0;

            for (int i = 0;i<headers.Length;i++)
            {
                switch (headers[i])
                {
                    case "#RIC": posTicker = i;
                        break;
                    case "Date[G]": posDate = i;
                        break;
                    case "Time[G]": posTime = i;
                        break;
                    case "GMT Offset": posTimeOffSet = i;
                        break;
                    case "Type": posType = i;
                        break;
                    case "Price": posPrice = i;
                        break;
                    case "Volume": posVolume = i;
                        break;
                    case "Bid Price": quoteBid = i;
                        break;
                    case "Ask Price": quoteAsk = i;
                        break;
                    case "Qualifiers": posQualifier = i;
                        break;
                }
            }

            string curTicker = "", prevTicker = "", curType, curQualifier;
            DateTime prevDate = new DateTime(), curDate = new DateTime();
            TimeSpan curTime = new TimeSpan(), prevTime = new TimeSpan();
            double curPrice = 0, prevPrice = 0, BidPrice = 0, AskPrice = 0, curBid = 0, curAsk = 0, curSpread = 0, MinSpread = 0, MaxSpread = 0, cumSpread = 0, prevCumValue = 0, cumValue = 0;
            int curVolume = 0, cumVolume = 0, prevVolume = 0, TimeShift = 0, cumTotalTrades = 0, prevCumVolume = 0, TotalQuotesArrived = 0;
            bool OpenAuctionEnded = false, OpenAuctionStarted = false, TickerStateChanged = false, IntradayStarted = false, IntradayEnded = false, FirstSpreadFixed = false, MinuteChanged = false, AfterMarket = false, FirstSettlementTime = true;

            OneMinBar curBar = new OneMinBar();
            AuctionData curAuction = new AuctionData();


            while (!srReader.EndOfStream)
            {           
                string curLine = srReader.ReadLine();
                string[] values = curLine.Split(sep);

                curTicker = values[posTicker];

                curDate = DateTime.Parse(values[posDate].Substring(0, 4) + "/" + values[posDate].Substring(4, 2) + "/" + values[posDate].Substring(6, 2));

                curTime = new TimeSpan(0, 0, 0); if (values[posTime] != "") curTime = TimeSpan.Parse(values[posTime]);
                TimeShift = 0; if (values[TimeShift] != "") TimeShift = int.Parse(values[posTimeOffSet]);
                curType = values[posType];
                curPrice = 0; if (values[posPrice] != "") curPrice = double.Parse(values[posPrice].Replace(".", ","));
                curVolume = 0; if (values[posVolume] != "") curVolume = int.Parse(values[posVolume]);
                curQualifier = values[posQualifier];

                if (OpenTimes.ContainsKey(curDate))
                {

                    if (prevDate != curDate || prevTicker != curTicker || TickerStateChanged || (new TimeSpan(curTime.Hours, curTime.Minutes, 0) - new TimeSpan(prevTime.Hours, prevTime.Minutes, 0)) >= new TimeSpan(0, 0, 1, 0))
                    {
                        #region Reinitilize 1

                        // ======================= MINUTE CHANGED =======================
                        if ((new TimeSpan(curTime.Hours, curTime.Minutes, 0) - new TimeSpan(prevTime.Hours, prevTime.Minutes, 0)) >= new TimeSpan(0, 0, 1, 0))
                        {
                            MinuteChanged = true;
                            curBar.MinSpread = MinSpread;
                            curBar.MaxSpread = MaxSpread;
                            //curBar.AverageSpread = cumSpread / TotalQuotesArrived;
                            curBar.CloseSpread = curSpread;
                            curBar.VWAP = curBar.Value / curBar.Volume;
                            curBar.Close = prevPrice;
                            curBar.EndTimeGMT = prevTime;
                            curBar.EndTimeLocal = prevTime.Add(new TimeSpan(TimeShift, 0, 0));                                
                        }

                        #endregion

                        if (curAuction.Ticker != null && curAuction.Price != 0)
                        {
                            OpenAuctionList.Add(curAuction);
                            curAuction = new AuctionData();
                        }

                        if (MinuteChanged)
                        {
                            if (curBar.Ticker != null && curBar.TotalTrades != 0)
                            {
                                IntradayBarsList.Add(curBar);
                            }
                        }

                        #region Reinitilize 2

                        if (prevDate != curDate || prevTicker != curTicker)
                        {                     
                            curAuction = new AuctionData();                            

                            OpenAuctionStarted = false;
                            OpenAuctionEnded = false;                            
                            IntradayEnded = false;
                            IntradayStarted = false;
                            MinuteChanged = false;
                            TickerStateChanged = false;
                            FirstSettlementTime = true;
                            AfterMarket = false;

                                                        
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

                        #endregion
                    }                    

                    if (curQualifier.Contains("31[IRGCOND]"))
                    {                     
                        if (curType == "Trade")
                        {
                            if (curVolume != 0)
                            {
                                cumVolume += curVolume;
                                cumValue += curVolume * curPrice;
                                cumTotalTrades++;

                                if (!OpenAuctionEnded)
                                {
                                    if (OpenAuctionStarted && (curPrice != prevPrice))
                                    {
                                        curAuction.Ticker = curTicker;
                                        curAuction.Date = curDate;
                                        curAuction.Volume = prevCumVolume;
                                        curAuction.Price = prevPrice;
                                        curAuction.EndTimeGMT = prevTime;
                                        curAuction.EndTimeLocal = prevTime.Add(new TimeSpan(TimeShift, 0, 0));
                                        curAuction.Minute = prevTime.Add(new TimeSpan(TimeShift, 0, 0)).Subtract(new TimeSpan(OpenTimes[curDate].Hours, 0, 0)).TotalMinutes;
                                        curAuction.TotalTrades = --cumTotalTrades;

                                        if (TotalMinutes.TryGetValue(curDate, out curBar.RegressiveMinute))
                                        {
                                            curBar.RegressiveMinute -= (int)curAuction.Minute;
                                        }

                                        OpenAuctionEnded = true;
                                        TickerStateChanged = true;
                                    }

                                    if (!OpenAuctionStarted)
                                    {
                                        if ((AskPrice > BidPrice) && (curPrice == BidPrice || curPrice == AskPrice))
                                        {
                                            OpenAuctionEnded = true;
                                        }
                                        else
                                        {
                                            OpenAuctionStarted = true;
                                            curAuction.StartTimeGMT = curTime;
                                            curAuction.StartTimeLocal = curTime.Add(new TimeSpan(TimeShift, 0, 0));
                                        }

                                    }
                                }

                                if (OpenAuctionEnded)
                                {
                                    if (!IntradayStarted)
                                    {
                                        curBar.Ticker = curTicker;
                                        curBar.Date = curDate;
                                        curBar.StartTimeGMT = curTime;
                                        curBar.StartTimeLocal = curTime.Add(new TimeSpan(TimeShift, 0, 0));
                                        curBar.Minute = curTime.Add(new TimeSpan(TimeShift, 0, 0)).Subtract(new TimeSpan(OpenTimes[curDate].Hours, 0, 0)).TotalMinutes;
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
                                        if (curBar.RegressiveMinute < 31)
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

                                prevVolume = curVolume;
                                prevPrice = curPrice;
                                prevDate = curDate;
                                prevTicker = curTicker;
                                prevTime = curTime;

                                prevCumVolume = cumVolume;
                                prevCumValue = cumValue;
                            }
                        }
                    }

                        

                    if (curType == "Quote")
                    {
                        if (values[quoteBid] != "")
                        {
                            curBid = double.Parse(values[quoteBid].Replace(".", ","));

                        }

                        if (values[quoteAsk] != "")
                        {
                            curAsk = double.Parse(values[quoteAsk].Replace(".", ","));
                        }

                        if (curAsk != 0 && curBid != 0)
                        {
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
                        }                                                     
                    }

                    prevDate = curDate;
                    prevTicker = curTicker;
                }
                
            }

            srReader.Close();
        }

        public void InsertFileInDatabase(string Folder)
        {
            string curFolder = Folder;

            string[] files = Directory.GetFiles(Folder);

            foreach (string curFile in files)
            {
                if (curFile.Contains("OpenAuction"))
                {

                    using (newNestConn curConn = new newNestConn(true))
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        /*
                        lock (ScreenSync)
                        {
                            Ticker = "None";
                            Status = "Inserting Open Auction Bars...";
                        }
                        */

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            /*
                            lock (ScreenSync)
                            {
                                Ticker = TempTicker;
                                Date = curValues[1];
                            }
                            */

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker = '" + TempTicker + "'" + " OR ReutersTicker LIKE '_" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.OpenAuction (IdTicker,Ticker,Date,EndTimeGMT,EndTimeLocal,Price,Volume,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "','" + curValues[2] + "','" + curValues[3] + "'," + curValues[4] + "," + curValues[5] + "," + curValues[6] + ",'" + DateTime.Today.ToString("yyyyMMdd HH:mm") + "')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }
                            }
                            else
                            { }
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
                    using (newNestConn curConn = new newNestConn(true))
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        /*
                        lock (ScreenSync)
                        {
                            Ticker = "None";
                            Status = "Inserting OneMin Bars...";
                        }
                        */

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            /*
                            lock (ScreenSync)
                            {
                                Ticker = TempTicker;
                                Date = curValues[1];
                            }
                            */
                             
                            if (TempTicker != PrevTicker)
                            {
                                string SecuritySearchSQL = "SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker = '" + TempTicker + "'" + " OR ReutersTicker LIKE '_" + TempTicker + "'";
                                IdSecurity = curConn.Return_Double(SecuritySearchSQL);
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.IntradayOneMinuteBars (IdTicker,Ticker,Date,Minute,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,VWAP,Volume,Low,High,[Open],[Close],AskTrades,BidTrades,TotalTrades,InsertDate,RegressiveMinute) VALUES "//,MinSpread,MaxSpread,AverageSpread) VALUES "
                                    + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "'," + curValues[2] + ",'" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "','" + curValues[6] + "'," + (curValues[7].Contains("NaN") ? "0" : curValues[7]) + "," + curValues[8] + "," + curValues[10]
                                            + "," + curValues[11] + "," + curValues[12] + "," + curValues[13] + "," + curValues[14] + "," + curValues[15] + "," + curValues[16] + ",'" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "'," + curValues[17] + ")"; //+ "," + curValues[18] + "," + curValues[19] + "," + curValues[20] + ")";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (StringSQL.Contains("20101015"))
                                { }

                                if (Result != 1)
                                {
                                }
                            }
                            else
                            { }
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
        }

        private void SearchTradeTimes(string FileName)
        {
            Dictionary<string, Dictionary<DateTime, TradingTime>> TradingTimes = new Dictionary<string, Dictionary<DateTime, TradingTime>>();

            StreamReader srReader = new StreamReader(FileName);

            char[] sep = { ',' };

            string[] headers = srReader.ReadLine().Split(sep);

            int posTicker, posPrice, posDate, posTime, posTimeOffSet, posType, posQualifier, posVolume, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeOffSet = posType = posQualifier = posVolume = quoteBid = quoteAsk = 0;

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
                    case "GMT Offset": posTimeOffSet = i;
                        break;
                    case "Type": posType = i;
                        break;
                    case "Price": posPrice = i;
                        break;
                    case "Volume": posVolume = i;
                        break;
                    case "Bid Price": quoteBid = i;
                        break;
                    case "Ask Price": quoteAsk = i;
                        break;
                    case "Qualifiers": posQualifier = i;
                        break;
                }
            }

            string curTicker = "", prevTicker = "", curType, curQualifier;
            DateTime prevDate = new DateTime(), curDate = new DateTime(), prevTradingDate = new DateTime();
            TimeSpan curTime = new TimeSpan(), prevTime = new TimeSpan();
            double curPrice = 0, prevPrice = 0, BidPrice = 0, AskPrice = 0, curBid = 0, curAsk = 0, curSpread = 0, MinSpread = 0, MaxSpread = 0, cumSpread = 0, prevCumValue = 0, cumValue = 0;
            int curVolume = 0, cumVolume = 0, prevVolume = 0, TimeShift = 0, cumTotalTrades = 0, prevCumVolume = 0, TotalQuotesArrived = 0;
            

            while (!srReader.EndOfStream)
            {
                string curLine = srReader.ReadLine();
                string[] values = curLine.Split(sep);

                curTicker = values[posTicker];

                curDate = DateTime.Parse(values[posDate].Substring(0, 4) + "/" + values[posDate].Substring(4, 2) + "/" + values[posDate].Substring(6, 2));

                curTime = new TimeSpan(0, 0, 0); if (values[posTime] != "") curTime = TimeSpan.Parse(values[posTime]);
                TimeShift = 0; if (values[TimeShift] != "") TimeShift = int.Parse(values[posTimeOffSet]);
                curType = values[posType];
                curPrice = 0; if (values[posPrice] != "") curPrice = double.Parse(values[posPrice].Replace(".", ","));
                curVolume = 0; if (values[posVolume] != "") curVolume = int.Parse(values[posVolume]);
                curQualifier = values[posQualifier];

                if (curQualifier.Contains("31[IRGCOND]") && curPrice > 0)
                {
                    cumTotalTrades++;
                    if(prevTicker != curTicker)
                    {
                        TradingTimes.Add(curTicker,new Dictionary<DateTime,TradingTime>());
                        TradingTimes[curTicker].Add(curDate, new TradingTime());
                        TradingTimes[curTicker][curDate].OpenTime = curTime.Add(new TimeSpan(TimeShift,0,0));
                        TradingTimes[curTicker][curDate].TimeOffSet = TimeShift;

                        if (prevTicker != "")
                        {
                            TradingTimes[prevTicker][prevDate == curDate ? curDate : prevDate].CloseTime = prevTime.Add(new TimeSpan(TimeShift, 0, 0));
                            TradingTimes[prevTicker][prevDate == curDate ? curDate : prevDate].TotalTrades = cumTotalTrades - 1;
                            cumTotalTrades = 1;
                        }
                    }
                    else if (prevDate != curDate)
                    {
                        TradingTimes[curTicker].Add(curDate, new TradingTime());
                        TradingTimes[curTicker][curDate].OpenTime = curTime.Add(new TimeSpan(TimeShift, 0, 0));
                        TradingTimes[curTicker][curDate].TimeOffSet = TimeShift;
                        
                        TradingTimes[curTicker][prevDate].CloseTime = prevTime.Add(new TimeSpan(TimeShift, 0, 0));
                        TradingTimes[curTicker][prevDate].TotalTrades = cumTotalTrades - 1;
                        cumTotalTrades = 1;
                       
                    }

                    prevDate = curDate;
                    prevTicker = curTicker;
                    prevTime = curTime;
                }
            }

            StreamWriter srW = new StreamWriter(@"C:\Temp\RTICK\Opentimes\ICF_Open_Times.csv");

            foreach (KeyValuePair<string, Dictionary<DateTime, TradingTime>> FatherNode in TradingTimes)
            {
                foreach (KeyValuePair<DateTime, TradingTime> SunNode in TradingTimes[FatherNode.Key])
                {
                    srW.WriteLine(FatherNode.Key + ";" + SunNode.Key.ToString("dd/MM/yyyy") + ";" + SunNode.Value.OpenTime.ToString("c") + ";" +
                                  SunNode.Value.CloseTime.ToString("c") + ";" + SunNode.Value.TimeOffSet + ";" + SunNode.Value.TotalTrades);
                }
            }
            srW.Close();

        }        

        private void LoadOpenTimes()
        {

            FileStream fs = new FileStream(@"C:\temp\RTICK\OpenTimes\IND_Open_Times.csv", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string tempLine;

            try
            {
                while ((tempLine = sr.ReadLine()) != null)
                {
                    string[] curValues = tempLine.Split(';');
                    OpenTimes.Add(DateTime.Parse(curValues[0].ToString()), TimeSpan.Parse(curValues[1].ToString()));
                    CloseTimes.Add(DateTime.Parse(curValues[0].ToString()), TimeSpan.Parse(curValues[2].ToString()));
                    TotalMinutes.Add(DateTime.Parse(curValues[0].ToString()), (int)(new TimeSpan(CloseTimes[DateTime.Parse(curValues[0])].Hours,CloseTimes[DateTime.Parse(curValues[0])].Minutes,0) - new TimeSpan(OpenTimes[DateTime.Parse(curValues[0])].Hours,0,0)).TotalMinutes);
                }
            }
            catch
            {
            }
            for (int i = DateTime.Today.DayOfYear - OpenTimes.Last().Key.DayOfYear - 1; i >= 1; i--)
            {
                OpenTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(10, 0, 0));
                CloseTimes.Add(DateTime.Today.AddDays(-i), new TimeSpan(18, 30, 0));
                TotalMinutes.Add(DateTime.Today.AddDays(-i), (int)(new TimeSpan(18, 30, 0) - new TimeSpan(10, 0, 0)).TotalMinutes);
            }

            /*
            List<DateTime> OpenDates = new List<DateTime>();
            DataTable Tb = new DataTable();

            string OpenDatesQuery = "SELECT DISTINCT(SrDate) " +
                                    "FROM NESTDB.dbo.Tb053_Precos_Indices (NOLOCK) " +
                                    "WHERE IdSecurity = 1073 AND " +
                                    "    SrType = 1 AND " +
                                    "    SOURCE = 1 AND" +
                                    "    SrDate >= '20040108'";

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
                string OpenTimeQuery = "SELECT TOP 1 OpenTimeGMT,TotalMinutesOpened " +
                                        "FROM NESTDB.dbo.Tb108_Open_Times_Mercados " +
                                        "WHERE DateRef <= '" + Date.ToString("yyyyMMdd") + "' " +
                                        "ORDER BY DateRef DESC ";

                using (newNestConn conn = new newNestConn())
                {
                    Tb = conn.Return_DataTable(OpenTimeQuery);

                    OpenTimes.Add(Date, DateTime.Parse(Tb.Rows[0][0].ToString()).TimeOfDay);
                    TotalMinutes.Add(Date, double.Parse(Tb.Rows[0][1].ToString()));
                    CloseTimes.Add(Date, DateTime.Parse(Tb.Rows[0][0].ToString()).AddMinutes(double.Parse(Tb.Rows[0][1].ToString())).TimeOfDay);
                }
            }
             * */
        }

        public void PrintDataToFile(string FileName)
        {
            //string curFolder = @"R:\RTICK\OneMinuteBars\DataBase Files\Unprocessed\";
            string curFolder = @"C:\temp\RTICK\DataBaseFiles\INDFiles\Unprocessed\";
            //string filename = FileName.Substring(FileName.IndexOf("OneMinBar"));
            //string filename = FileName.Substring(FileName.IndexOf("SP600"));
            //filename = filename.Remove(filename.IndexOf(".csv"));

            string filename = FileName.Substring(FileName.IndexOf("IND"));

            string StartTimeGMT = "";
            string EndTimeGMT = "";
            string Minute = "";
            string ReverseMinute = "";
            string StartTimeLocal = "";
            string EndTimeLocal = "";            

            foreach (AuctionData Auction in OpenAuctionList)
            {
                /*
                lock (ScreenSync)
                {
                    Ticker = Auction.Ticker;
                    Status = "Writing " + Auction.Ticker + " Bars...";
                }*/


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


            foreach (OneMinBar Bar in IntradayBarsList)
            {
                /*              
                lock (ScreenSync)
                {
                    Ticker = Bar.Ticker;
                    Status = "Writing " + Bar.Ticker + " Bars...";
                }*/
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


        public List<DateTime> GetDBDates(DateTime IniDate)
        {
            List<DateTime> Dates = new List<DateTime>();
            DataTable Tb;
            string SqlQuery =
                "SELECT DISTINCT(SrDate) " +
                "FROM NESTDB.dbo.Tb053_Precos_Indices (NOLOCK) " +
                "WHERE IdSecurity = 1073 AND " +
                "    SrType = 1 AND " +
                "    SOURCE = 1";

            using (newNestConn conn = new newNestConn())
            {
                Tb = conn.Return_DataTable(SqlQuery);
            }

            for (int i = 0; i < Tb.Rows.Count; i++)
            {
                if (DateTime.Parse(Tb.Rows[i][0].ToString()) >= IniDate)
                    Dates.Add(DateTime.Parse(Tb.Rows[i][0].ToString()));
            }

            return Dates;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
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
        public double Last = 0;

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
}
