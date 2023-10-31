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

namespace RTICKOneMinBarReaderOffshore
{
    class RTICKFile
    {
        List<OneMinBar> BarList = new List<OneMinBar>();

        public void ProcessFolder(string curFolder)
        {
            string[] fileEntries = Directory.GetFiles(curFolder);

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains(".csv"))
                {
                    ProcessFile(fileName);

                    File.Move(fileName, fileName.Replace("Unprocessed", "Processed"));

                    //PrintDataToFile(fileName);
                }
            }
            //InsertFileInDatabase(@"C:\temp\RTICK\DataBaseFiles\Unprocessed\");
            //Generate_Data(true);
        }

        public void ProcessFile(string fileName)
        {
            StreamReader srReader = new StreamReader(fileName);

            int posTicker, posPrice, posDate, posTime, posTimeOffSet, posType, posOpen, posHigh, posLow, posLast, posVolume, posVWAP, posNoTrades, posOpenBid, posHighBid, posLowBid, posCloseBid, posOpenAsk, posHighAsk, posLowAsk, posCloseAsk;

            posTicker = posPrice = posDate = posTime = posTimeOffSet = posType = posOpen = posHigh = posLow = posLast = posVolume = posVWAP = posNoTrades = posOpenBid = posHighBid = posLowBid = posCloseBid = posOpenAsk = posHighAsk = posLowAsk = posCloseAsk = 0;

            char[] sep = { ',' };

            string[] headers = srReader.ReadLine().Split(sep);

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
                    case "Open": posOpen = i;
                        break;
                    case "High": posHigh = i;
                        break;
                    case "Low": posLow = i;
                        break;
                    case "Last": posLast = i;
                        break;
                    case "Volume": posVolume = i;
                        break;
                    case "VWAP": posVWAP = i;
                        break;
                    case "No. Trades": posNoTrades = i;
                        break;
                    case "Open Bid": posOpenBid = i;
                        break;
                    case "High Bid": posHighBid = i;
                        break;
                    case "Low Bid": posLowBid = i;
                        break;
                    case "Close Bid": posCloseBid = i;
                        break;
                    case "Open Ask": posOpenAsk = i;
                        break;
                    case "High Ask": posHighAsk = i;
                        break;
                    case "Low Ask": posLowAsk = i;
                        break;
                    case "Close Ask": posCloseAsk = i;
                        break;
                }
            }

            bool FirstTimeInDate = true;

            string curTicker = "", prevTicker = "";
            DateTime prevDate = new DateTime(), curDate = new DateTime();
            TimeSpan curTime = new TimeSpan(), prevTime = new TimeSpan(), OpenTime = new TimeSpan(), CloseTime = new TimeSpan();
            int timeOffset, NoTrades;
            timeOffset = NoTrades = 0;
            Int64 Volume = 0;
            double Open, High, Low, Last, VWAP, OpenBid, HighBid, LowBid, CloseBid, OpenAsk, HighAsk, LowAsk, CloseAsk;
            Open = High = Low = Last = VWAP = OpenBid = HighBid = LowBid = CloseBid = OpenAsk = HighAsk = LowAsk = CloseAsk = 0;

            while (!srReader.EndOfStream)
            {
                string[] values = srReader.ReadLine().Split(sep);

                values[posDate] = values[posDate].Insert(4, "/").Insert(7, "/");

                curTicker = values[posTicker];
                curDate = DateTime.Parse(values[posDate]);
                curTime = TimeSpan.Parse(values[posTime]);
                timeOffset = int.Parse(values[posTimeOffSet]);
                if (!(values[posOpen] == "")) Open = double.Parse(values[posOpen].Replace(".", ","));
                if (!(values[posHigh] == "")) High = double.Parse(values[posHigh].Replace(".", ","));
                if (!(values[posLow] == "")) Low = double.Parse(values[posLow].Replace(".", ","));
                if (!(values[posLast] == "")) Last = double.Parse(values[posLast].Replace(".", ","));
                if (!(values[posVolume] == "")) Volume = Int64.Parse(values[posVolume].Replace(".", ","));
                if (!(values[posVWAP] == "")) VWAP = double.Parse(values[posVWAP].Replace(".", ","));
                if (!(values[posOpenBid] == "")) OpenBid = double.Parse(values[posOpenBid].Replace(".", ","));
                if (!(values[posHighBid] == "")) HighBid = double.Parse(values[posHighBid].Replace(".", ","));
                if (!(values[posLowBid] == "")) LowBid = double.Parse(values[posLowBid].Replace(".", ","));
                if (!(values[posCloseBid] == "")) CloseBid = double.Parse(values[posCloseBid].Replace(".", ","));
                if (!(values[posOpenAsk] == "")) OpenAsk = double.Parse(values[posOpenAsk].Replace(".", ","));
                if (!(values[posHighAsk] == "")) HighAsk = double.Parse(values[posHighAsk].Replace(".", ","));
                if (!(values[posLowAsk] == "")) LowAsk = double.Parse(values[posLowAsk].Replace(".", ","));
                if (!(values[posCloseAsk] == "")) CloseAsk = double.Parse(values[posCloseAsk].Replace(".", ","));
                if (!(values[posNoTrades] == "")) NoTrades = int.Parse(values[posNoTrades]);

                if ((prevTicker != curTicker && prevTicker != "") || (prevDate != curDate && prevDate != new DateTime()))
                {
                    CloseTime = prevTime;
                    FirstTimeInDate = true;

                }


                if (High != 0)
                {
                    if (FirstTimeInDate)
                    {
                        OpenTime = curTime;
                        FirstTimeInDate = false;
                    }

                    OneMinBar Bar = new OneMinBar();

                    Bar.Ticker = curTicker;
                    Bar.Date = curDate;
                    Bar.Minute = curTime.Add(-new TimeSpan(OpenTime.Hours, 0, 0)).TotalMinutes;
                    Bar.Open = Open;
                    Bar.Last = Last;
                    Bar.High = High;
                    Bar.Low = Low;
                    Bar.Volume = Volume;
                    Bar.VWAP = VWAP;
                    Bar.OpenBid = OpenBid;
                    Bar.CloseBid = CloseBid;
                    Bar.HighBid = HighBid;
                    Bar.LowBid = LowBid;
                    Bar.OpenAsk = OpenAsk;
                    Bar.CloseAsk = CloseAsk;
                    Bar.HighAsk = HighAsk;
                    Bar.LowAsk = LowAsk;
                    Bar.NoTrades = NoTrades;

                    BarList.Add(Bar);

                    prevTicker = curTicker;
                    prevDate = curDate;
                }

            }

        }

        public void PrintDataToFile(string FileName)
        {
            string folder = @"C:\temp\RTICK\OffShore DataBaseFiles\";
            string file = FileName.Substring(FileName.IndexOf("Offshore")).Remove(FileName.IndexOf(".csv"));

            foreach (OneMinBar Bar in BarList)
            {
                StreamWriter srWriter = new StreamWriter(folder + file + "_" + Bar.Ticker + "_.txt", true);

                srWriter.WriteLine(Bar.Ticker + ";" +
                                    Bar.Date.ToString("yyyy-MM-dd") + ";" +
                                    Bar.Minute + ";" +
                                    Bar.VWAP.ToString().Replace(",", ".") + ";" +
                                    Bar.Volume.ToString() + ";" +
                                    Bar.Low.ToString().Replace(",", ".") + ";" +
                                    Bar.High.ToString().Replace(",", ".") + ";" +
                                    Bar.Open.ToString().Replace(",", ".") + ";" +
                                    Bar.Last.ToString().Replace(",", ".") + ";" +
                                    Bar.NoTrades + ";" +
                                    Bar.OpenBid.ToString().Replace(",", ".") + ";" +
                                    Bar.CloseBid.ToString().Replace(",", ".") + ";" +
                                    Bar.HighBid.ToString().Replace(",", ".") + ";" +
                                    Bar.LowBid.ToString().Replace(",", ".") + ";" +
                                    Bar.OpenAsk.ToString().Replace(",", ".") + ";" +
                                    Bar.CloseAsk.ToString().Replace(",", ".") + ";" +
                                    Bar.HighAsk.ToString().Replace(",", ".") + ";" +
                                    Bar.LowAsk.ToString().Replace(",", "."));
            }

        }

        public void InsertFileInDataBase(string Folder)
        {
            string curFolder = Folder;

            string[] files = Directory.GetFiles(Folder);

            foreach (string curFile in files)
            {

                using (newNestConn curConn = new newNestConn())
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
                            IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                        }

                        if (!double.IsNaN(IdSecurity))
                        {
                            StringSQL = "INSERT INTO RTICKDB.dbo.IntradayOneMinuteBars (IdTicker,Ticker,Date,Minute,VWAP,Volume,Low,High,[Open],[Close],TotalTrades,InsertDate) VALUES "
                                        + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "'," + curValues[2] + ",'" + curValues[3] + "','" + curValues[4] + "','"
                                        + curValues[5] + "','" + curValues[6] + "'," + curValues[7] + "," + curValues[8] + "," + curValues[9] + DateTime.Now.ToString("yyyyMMdd");
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

                File.Move(curFile, DestFile);
            }

        }
    }
}
