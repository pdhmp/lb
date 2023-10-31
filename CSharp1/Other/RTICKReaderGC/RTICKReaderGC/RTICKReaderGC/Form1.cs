using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NestDLL;
using NestSymConn;

namespace RTICKReaderGC
{
    public partial class Form1 : Form
    {
        Dictionary<string, Dictionary<DateTime, TimeData>> Data = new Dictionary<string, Dictionary<DateTime, TimeData>>();

        Dictionary<string, Dictionary<DateTime, Dictionary<int, OneMinBar>>> OneMinBars = new Dictionary<string, Dictionary<DateTime, Dictionary<int, OneMinBar>>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProcessFolder(@"R:\RTICK\Future Files\Unprocessed");
            InsertFileInDataBase(@"C:\temp\RTICK\DataBaseFiles\OffShoreFiles_GC\Unprocessed");
        }

        private void ProcessFolder(string FolderPath)
        {
            string[] Files = Directory.GetFiles(FolderPath);

            foreach (string file in Files)
            {
                //ProcessForAnalisys(file);
                ProcessFile(file);
                PrintDataToFile(file);
                OneMinBars.Clear();
                
            }
        }

        private void ProcessFile(string FilePath)
        {
            StreamReader SrReader = new StreamReader(@FilePath);

            OneMinBar curBar = new OneMinBar();

            bool DateChanged = false;

            bool IntradayStarted = false;
            bool TickerInAuction = false;
            bool FirstSpreadFixed = false;
            bool OpenAuctionFixed = false;
            bool CloseAuctionFixed = false;

            // ====================== Accumulators and Prev Values
            TimeSpan OpenTime = new TimeSpan(0, 0, 0);
            TimeSpan CloseTime = new TimeSpan(0, 0, 0);
            TimeSpan LastPriceTime = new TimeSpan(0, 0, 0);
            double cumSpread = 0;
            Int64 TotalQuotesArrived = 0;
            double MinSpread = 0;
            double MaxSpread = 0;

            string prevTicker = "";
            double prevPrice = 0;

            DateTime prevDate = new DateTime();
            TimeSpan prevTime = new TimeSpan(0, 0, 0);

            // ====================== Current Values
            DateTime curDate = new DateTime();
            TimeSpan curTime = new TimeSpan();
            int curTimeShift = 0;
            double curPrice;
            Int64 curVolume;
            double curSpread = 0;
            String curState;
            String curType;
            String curTicker = "";
            double TotalDayMinutes = 0;
            int totalTrades = 0;

            DateTime LastTradeDate = new DateTime();
            double cumVolume = 0;

            double curBid = 0;
            double curAsk = 0;


            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            tempLine = SrReader.ReadLine();
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

            while (!SrReader.EndOfStream)
            {
                string curLine = SrReader.ReadLine();

                string[] curValues = curLine.Split(sep);

                curTicker = curValues[posTicker];

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

                curTime = new TimeSpan(0, 0, 0); if (curValues[posTime] != "") curTime = TimeSpan.Parse(curValues[posTime]);
                curTimeShift = 0; if (curValues[posTimeShift] != "") curTimeShift = int.Parse(curValues[posTimeShift]);
                curType = curValues[posType];
                curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".", ","));
                curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                curState = curValues[posState];


                DateTime LocalDate = new DateTime(curDate.Ticks);
                LocalDate = LocalDate.AddTicks(curTime.Ticks);
                LocalDate = LocalDate.AddHours(curTimeShift);
                

                if (curType == "Trade" && curVolume != 0 && curPrice != 0)
                {
                    
                        if (prevTicker != curTicker)
                        {
                            if (!OneMinBars.ContainsKey(curTicker))
                            {
                                OneMinBars.Add(curTicker, new Dictionary<DateTime, Dictionary<int, OneMinBar>>());
                                OneMinBars[curTicker].Add(curDate, new Dictionary<int, OneMinBar>());

                            }
                            DateChanged = true;
                        }

                        if (prevDate != curDate)
                        {
                            if (!OneMinBars[curTicker].ContainsKey(curDate))
                            {
                                OneMinBars[curTicker].Add(curDate, new Dictionary<int, OneMinBar>());
                            }

                            DateChanged = true;
                        }

                        if ((new TimeSpan(curTime.Hours, curTime.Minutes, 0) - new TimeSpan(prevTime.Hours, prevTime.Minutes, 0)).TotalMinutes >= 1 || DateChanged)
                        
                        {
                            if (curBar.Ticker != null && curBar.Volume > 0)
                            {
                                curBar.VWAP = curBar.Value / curBar.Volume;
                                curBar.Close = prevPrice;
                                curBar.EndTimeLocal = new DateTime(prevDate.Ticks + prevTime.Ticks).Add(new TimeSpan(curTimeShift, 0, 0)).TimeOfDay;
                                curBar.EndTimeGMT = prevTime;
                                curBar.RegressiveMinute = new TimeSpan(24, 00, 00).TotalMinutes - (int)curBar.Minute;

                                if (DateChanged)
                                {
                                    if (prevTicker == curTicker)
                                    {
                                        OneMinBars[curTicker][prevDate].Add((int)curBar.Minute, curBar);

                                    }
                                    else
                                    {
                                        OneMinBars[prevTicker][prevDate].Add((int)curBar.Minute, curBar);
                                    }                                   
                                }
                                else
                                {
                                    OneMinBars[curTicker][curDate].Add((int)curBar.Minute, curBar);
                                }
                            }
                            
                            if (DateChanged)
                            {
                                DateChanged = false;
                            }

                            curBar = new OneMinBar();

                            curBar.Ticker = curTicker;
                            curBar.Date = curDate;
                            curBar.Open = curPrice;
                            curBar.StartTimeLocal = LocalDate.TimeOfDay;
                            curBar.StartTimeGMT = curTime;
                            curBar.Minute = curTime.TotalMinutes;
                            curBar.Low = curPrice;
                            curBar.High = curPrice;
                            curBar.CurTimeShift = curTimeShift;
                        }


                        curBar.TotalTrades++;
                        if (curPrice == curBid && curVolume != 0) curBar.BidTrades++;
                        if (curPrice == curAsk && curVolume != 0) curBar.AskTrades++;
                        if (curPrice == curBid) curBar.BidTradesZeroVol++;
                        if (curPrice == curAsk) curBar.AskTradesZeroVol++;
                        if (curPrice > curBar.High) curBar.High = curPrice;
                        if (curPrice < curBar.Low) curBar.Low = curPrice;
                        curBar.Volume += curVolume;
                        curBar.Value += curVolume * curPrice;
                    
                        prevTime = curTime;
                        prevDate = curDate;
                        prevTicker = curTicker;
                    
                }
                else if (curType == "Trade")
                {

                    StreamWriter SrAux = new StreamWriter(@"C:\temp\RTICK\Análise GC\Análise GC.csv", true);
                    SrAux.WriteLine(curTicker + "," + curDate + "," + curTime + "," + LocalDate.TimeOfDay + "," + curVolume + "," + curPrice);
                    SrAux.Close();
                }
                else if (curType == "Quote" && (curBid != 0 || curAsk != 0))
                {

                    curBid = curValues[quoteBid] != "" ? double.Parse(curValues[quoteBid].Replace(".", ",")) : curBid;
                    curAsk = curValues[quoteAsk] != "" ? double.Parse(curValues[quoteAsk].Replace(".", ",")) : curAsk;

                    if (DateTime.Parse(curValues[posDate]) != curDate)
                    {
                    }
                }             
                

                
            }
        }

        private void ProcessForAnalisys(string FilePath)
        {
            StreamReader SrReader = new StreamReader(FilePath);

            bool IntradayStarted = false;
            bool TickerInAuction = false;
            bool FirstSpreadFixed = false;
            bool OpenAuctionFixed = false;
            bool CloseAuctionFixed = false;

            // ====================== Accumulators and Prev Values
            TimeSpan OpenTime = new TimeSpan(0, 0, 0);
            TimeSpan CloseTime = new TimeSpan(0, 0, 0);
            TimeSpan LastPriceTime = new TimeSpan(0,0,0);
            double cumSpread = 0;
            Int64 TotalQuotesArrived = 0;
            double MinSpread = 0;
            double MaxSpread = 0;

            string prevTicker = "";
            double prevPrice = 0;

            DateTime prevDate = new DateTime();
            TimeSpan prevTime = new TimeSpan(0, 0, 0);

            // ====================== Current Values
            DateTime curDate = new DateTime();
            TimeSpan curTime = new TimeSpan();
            int curTimeShift = 0;
            double curPrice;
            Int64 curVolume;
            double curSpread = 0;
            String curState;
            String curType;
            String curTicker = "";
            double TotalDayMinutes = 0;
            int totalTrades = 0;

            DateTime LastTradeDate = new DateTime();
            double cumVolume = 0;

            double curBid = 0;
            double curAsk = 0;


            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            tempLine = SrReader.ReadLine();
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

            while (!SrReader.EndOfStream)
            {
                tempLine = SrReader.ReadLine();

                curValues = tempLine.Split(',');

                curTicker = curValues[0];

                //lock (ScreenSync)
                //{
                //    Ticker = curTicker;
                //    Status = "Loading " + curTicker + " Bars...";
                //}

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

                //lock (ScreenSync)
                //{
                //    Date = curDate.ToString("dd/MM/yyyy");
                //    if (curDate == new DateTime(2010, 04, 08))
                //    { }
                //}

                curTime = new TimeSpan(0, 0, 0); if (curValues[posTime] != "") curTime = TimeSpan.Parse(curValues[posTime]);
                curTimeShift = 0; if (curValues[posTimeShift] != "") curTimeShift = int.Parse(curValues[posTimeShift]);
                curType = curValues[posType];
                curPrice = 0; if (curValues[posLast] != "") curPrice = double.Parse(curValues[posLast].Replace(".", ","));
                curVolume = 0; if (curValues[posVolume] != "") curVolume = Int64.Parse(curValues[posVolume]);
                curState = curValues[posState];

 
                curDate = curDate.AddTicks(curTime.Ticks);
                curDate = curDate.AddHours(curTimeShift);

                curTime = curDate.TimeOfDay;
                curDate = curDate.Date;

                if (curType == "Trade")
                {
                    if (prevTicker != curTicker)
                    {
                        if (!Data.ContainsKey(curTicker))
                        {
                            Data.Add(curTicker, new Dictionary<DateTime, TimeData>());
                            Data[curTicker].Add(curDate, new TimeData());
                        }

                        if (LastTradeDate == prevDate && prevDate != new DateTime())
                        {
                            Data[prevTicker][prevDate].CloseTime = LastPriceTime;
                            Data[prevTicker][prevDate].Volume = (long)cumVolume;
                            Data[prevTicker][prevDate].TotalTrades = totalTrades;
                            cumVolume = 0;
                            totalTrades = 0;
                        }
                    }

                    if (prevDate != curDate)
                    {
                        if (!Data[curTicker].ContainsKey(curDate))
                        {
                            Data[curTicker].Add(curDate, new TimeData());
                        }

                        if (LastTradeDate == prevDate && prevDate != new DateTime())
                        {
                            Data[prevTicker][prevDate].CloseTime = LastPriceTime;
                            Data[prevTicker][prevDate].Volume = (long)cumVolume;
                            Data[prevTicker][prevDate].TotalTrades = totalTrades;
                            cumVolume = 0;
                            totalTrades = 0;
                        }

                        Data[curTicker][curDate].OpenTime = curTime;
                        Data[curTicker][curDate].TimeOffSet = curTimeShift;
                    }

                    if (prevDate != new DateTime())
                    {
                        if ((prevDate != curDate) && (Data[prevTicker][prevDate].CloseTime < Data[prevTicker][prevDate].OpenTime))
                        { }
                    }
                    LastTradeDate = curDate;
                    LastPriceTime = curTime;
                    prevTicker = curTicker;
                    prevDate = curDate;
                    prevTime = curTime;
                    cumVolume += curVolume;
                    totalTrades++;
                }
                
            }
            if (Data.ContainsKey(curTicker))
            {
                if (Data[curTicker].ContainsKey(curDate))
                {
                    Data[curTicker][curDate].CloseTime = curTime;
                }
            }
            

            foreach (string ticker in Data.Keys)
            {
                foreach(DateTime Date in Data[ticker].Keys)
                {
                    string Line = ticker + ";" + Date.ToString("dd/MM/yyyy") + ";" + new DateTime(Data[ticker][Date].OpenTime.Ticks).ToString("HH:mm:ss") + ";" + new DateTime(Data[ticker][Date].CloseTime.Ticks).ToString("HH:mm:ss") + ";" + Data[ticker][Date].TimeOffSet + ";" + Data[ticker][Date].TotalTrades + ";" + Data[ticker][Date].Volume + ";";
                    Writer.Write(Line, @"C:\temp\RTICK\OpenTimes\GCOpenTimes.csv");
                }
            }
        }

        private void PrintDataToFile(string FileName)
        {
            string curFolder = "";
            string filename = "";

            if (FileName.Contains("GC_OneByOne"))
            {
                filename = FileName.Substring(FileName.IndexOf("GC_OneByOne"));
                curFolder = @"C:\temp\RTICK\DataBaseFiles\OffShoreFiles_GC\Unprocessed\";

            }
            
           filename = filename.Remove(filename.IndexOf(".csv"));

            string StartTimeGMT = "";
            string EndTimeGMT = "";
            string Minute = "";
            string ReverseMinute = "";
            string StartTimeLocal = "";
            string EndTimeLocal = "";

            foreach (string Ticker in OneMinBars.Keys)
            {
                foreach (DateTime Date in OneMinBars[Ticker].Keys)
                {
                    foreach (OneMinBar Bar in OneMinBars[Ticker][Date].Values)
                    {
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

                            if (Bar.VWAP == 0 && Bar.Volume != 0)
                            { }

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
                                                Bar.AverageSpread.ToString().Replace(",", ".") + ";" +
                                                Bar.AskTradesZeroVol + ";" +
                                                Bar.BidTradesZeroVol
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

        private void InsertFileInDataBase(string DirectoryName)
        {
            string[] Files = Directory.GetFiles(DirectoryName);

            foreach (string curFile in Files)
            {
                if (curFile.Contains("OneMinBar") && !curFile.Contains("CloseAuction") && !curFile.Contains("OpenAuction"))
                {
                    using (newNestConn curConn = new newNestConn(true))
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }
                            double VWAP = 0;
                            if (!double.IsNaN(IdSecurity) && !double.IsNaN(double.Parse(curValues[7])))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.IntradayOneMinuteBars (IdTicker,Ticker,Date,Minute,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,VWAP,Volume,Low,High,[Open],[Close],AskTrades,BidTrades,TotalTrades,InsertDate,RegressiveMinute) VALUES "//,MinSpread,MaxSpread,AverageSpread) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "'," + curValues[2] + ",'" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "','" + curValues[6] + "'," + curValues[7] + "," + curValues[8] + "," + curValues[10]
                                            + "," + curValues[11] + "," + curValues[12] + "," + curValues[13] + "," + curValues[14] + "," + curValues[15] + "," + curValues[16] + ",'" + DateTime.Now.ToString("yyyyMMdd HH:mm") + "'," + curValues[17] + ")"; //+ "," + curValues[18] + "," + curValues[19] + "," + curValues[20] + ")";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }
                            }
                            else if (!double.IsNaN(double.Parse(curValues[7])))
                            {}
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
    }

    class TimeData
    {
        public TimeSpan OpenTime;
        public TimeSpan CloseTime;
        public int TimeOffSet;
        public int TotalTrades;
        public long Volume;

        public TimeData()
        {
            OpenTime = new TimeSpan();
            CloseTime = new TimeSpan();
            TimeOffSet = 0;
            TotalTrades = 0;
            Volume = 0;
        }
    }
    
    static class Writer
    {      
        public static void Write(string Line, string FilePath)
        {
            StreamWriter srWirter;
            srWirter = new StreamWriter(FilePath, true);
            srWirter.WriteLine(Line);
            srWirter.Close();
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
        public int BidTradesZeroVol = 0;
        public int AskTradesZeroVol = 0;
        public int CurTimeShift = 0;

        public string ToString()
        {
            return Ticker + "_" + Date.ToString("dd-MMM-yy") + "_" + Minute;
        }
    }

}
