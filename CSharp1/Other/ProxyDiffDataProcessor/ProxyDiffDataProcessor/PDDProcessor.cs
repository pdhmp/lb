using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using NCommonTypes;
using NestDLL;
using NestSymConn;
using System.Threading;



namespace ProxyDiffDataProcessor
{
    class PDDProcessor
    {
        BSEPlayer curPlayer;

        SortedDictionary<string, List<VWAPLine>> subData = new SortedDictionary<string, List<VWAPLine>>();

        DateTime SessionDate = DateTime.MinValue;
        TimeSpan OpenTime, TriggerTime, Closetime,LastUpdateTime;
        int DLSTimeDiff = 0;

        StreamReader proxyDiffFile;
        bool StopReading = false;

        string filePath = "";
        private ManualResetEvent doneEvent;


        //StreamWriter vale5lines = new StreamWriter(@"C:\TEMP\VALE5FILE.TXT");


        public PDDProcessor(string _Path, ManualResetEvent _doneEvent) 
        {
            filePath = _Path;
            doneEvent = _doneEvent;
        }

        public void Start(Object threadContext)
        {
            proxyDiffFile = new StreamReader(filePath);

            OpenTime = new TimeSpan(10, 00, 00);
            TriggerTime = new TimeSpan(10, 30, 00);
            Closetime = new TimeSpan(14, 00, 00);

            LoadSymbols();

            ReadFile();

            PrintLines();

            //Thread.Sleep(DateTime.Now.Millisecond * 10);

            Console.WriteLine("File " + filePath + " processed!");

            doneEvent.Set();

            /*curPlayer = new BSEPlayer(@"R:\Data\Temp folder ProxyDiff Uncompressed\20110103_Proxydiff.txt");
            curPlayer.OnData += new EventHandler(curPlayer_OnData);            
            curPlayer.Play();

            while (curPlayer.curCracker.LastMessageTime == new DateTime(1900, 01, 01))
            {
                System.Threading.Thread.Sleep(100);
            }

            SessionDate = curPlayer.curCracker.LastMessageTime;

            SetIniTime();

            LoadSymbols();

            curPlayer.PauseOnDate = false;*/


        }

        private void ReadFile()
        {
            string tempLine = "";
            string crackLine = "";

            while(!StopReading && ((tempLine = proxyDiffFile.ReadLine()) != null))
            {
                crackLine = crackLine + tempLine;

                if (crackLine.IndexOf((char)17) > 0)
                {
                    Crack(crackLine);
                    crackLine = "";
                }               
            }            
        }

        private void Crack(string crackline)
        {          
            PMessage curMessageItem = new PMessage(crackline);
            DateTime auxLastTime = curMessageItem.DataEvento;

            //if (curMessageItem.CodigoPapel == "VALE5") { vale5lines.WriteLine(crackline); }

            if (SessionDate == DateTime.MinValue) { updateSessionDate(auxLastTime.Date); }
            LastUpdateTime = auxLastTime.AddHours((-1)*DLSTimeDiff).TimeOfDay;

            if (subData.ContainsKey(curMessageItem.CodigoPapel))
            {
                switch (curMessageItem.TipoMensagem)
                {
                    case "01": msgOpen(curMessageItem); break; // Abertura
                    case "02": msgTrade(curMessageItem); break; // Trade
                    //case "32": msgOpenSummary(curMessageItem); break; // Resumo da Abertura
                }
            }
        }

        private void msgOpen(PMessage curMessageItem)
        {
            string msgbody = curMessageItem.MessageBody;

            if (LastUpdateTime <= Closetime.Add(new TimeSpan(0, 30, 0)))
            {
                //Get message data
                string Ticker = curMessageItem.CodigoPapel;
                long LastSize = long.Parse(msgbody.Substring(0, 12));
                long CumSize = long.Parse(msgbody.Substring(42, 12));
                double Last = double.Parse(msgbody.Substring(13, 13)) / convNumero(msgbody.Substring(12, 1));                
                DateTime auxTradeTime = (new DateTime(int.Parse(msgbody.Substring(167, 4)),
                                                      int.Parse(msgbody.Substring(171, 2)),
                                                      int.Parse(msgbody.Substring(173, 2)),
                                                      int.Parse(msgbody.Substring(175, 2)),
                                                      int.Parse(msgbody.Substring(177, 2)),
                                                      int.Parse(msgbody.Substring(179, 2)))).AddHours((-1) * DLSTimeDiff);
                DateTime TradeDate = auxTradeTime.Date;
                TimeSpan TradeTime = auxTradeTime.TimeOfDay;

                if (Ticker.Substring(Ticker.Length - 1, 1) == "F") { int a = 0; }
                //Ticker = (Ticker.Substring(Ticker.Length - 1, 1) == "F" ? Ticker.Substring(0, Ticker.Length - 1) : Ticker);


                if (TradeDate == SessionDate)
                {
                    //Update VWAP line
                    List<VWAPLine> curList;
                    if (subData.TryGetValue(Ticker, out curList))
                    {
                        foreach (VWAPLine curLine in curList)
                        {
                            if (TradeTime >= curLine.IniTime && TradeTime < curLine.EndTime)
                            {
                                if (LastSize >= 0)
                                {
                                    if (Ticker == "PETR4") { int a = 0; }
                                    curLine.AddTrade(LastSize, Last,LastUpdateTime);
                                }

                                if (CumSize != curLine.CumQuantity) { int a = 0; }
                            }
                        }
                    }
                }
            }
            else
            {
                StopReading = true;
            }
        }


        private void msgOpenSummary(PMessage curMessageItem)
        {
            string msgbody = curMessageItem.MessageBody;


            //Get message data
            string Ticker = curMessageItem.CodigoPapel;
            long LastSize = long.Parse(msgbody.Substring(58, 12));            
            double Last = double.Parse(msgbody.Substring(15, 13)) / convNumero(msgbody.Substring(14, 1));
            string RegistryType = msgbody.Substring(56, 2);
            
            //Update VWAP line
            List<VWAPLine> curList;
            if (subData.TryGetValue(Ticker, out curList))
            {
                foreach (VWAPLine curLine in curList)
                {
                    if (LastUpdateTime >= curLine.IniTime && LastUpdateTime < curLine.EndTime)
                    {
                        if (RegistryType == "04")
                        {
                            curLine.AddTrade(LastSize, Last,LastUpdateTime);
                        }
                        else
                        {
                            if (Ticker == "PETR4")
                            {
                                int a = 0;
                            }
                        }

                        
                    }
                }
            }

        }

        private void msgTrade(PMessage curMessageItem)
        {
            string msgbody = curMessageItem.MessageBody;

            if (LastUpdateTime <= Closetime.Add(new TimeSpan(0, 30, 0)))
            {
                //Get message data
                string Ticker = curMessageItem.CodigoPapel;

                if (Ticker.Substring(Ticker.Length - 1, 1) == "F") { int a = 0; }

                //Ticker = (Ticker.Substring(Ticker.Length - 1, 1) == "F" ? Ticker.Substring(0, Ticker.Length - 1) : Ticker);

                long LastSize = long.Parse(msgbody.Substring(0, 12));
                long CumSize = long.Parse(msgbody.Substring(42, 12));
                double Last = double.Parse(msgbody.Substring(13, 13)) / convNumero(msgbody.Substring(12, 1));
                string RegistryType = msgbody.Substring(90, 2);
                DateTime auxTradeTime = (new DateTime(int.Parse(msgbody.Substring(198, 4)),
                                                      int.Parse(msgbody.Substring(202, 2)),
                                                      int.Parse(msgbody.Substring(204, 2)),
                                                      int.Parse(msgbody.Substring(206, 2)),
                                                      int.Parse(msgbody.Substring(208, 2)),
                                                      int.Parse(msgbody.Substring(210, 2)))).AddHours((-1)*DLSTimeDiff);
                DateTime TradeDate = auxTradeTime.Date;
                TimeSpan TradeTime = auxTradeTime.TimeOfDay;


                if (TradeDate == SessionDate)
                {
                    //Update VWAP line
                    List<VWAPLine> curList;
                    if (subData.TryGetValue(Ticker, out curList))
                    {
                        foreach (VWAPLine curLine in curList)
                        {
                            if (TradeTime >= curLine.IniTime && TradeTime < curLine.EndTime)
                            {
                                if (LastSize >= 0)
                                {
                                    if (RegistryType == "07")
                                    {
                                        curLine.AddTrade(LastSize, Last,LastUpdateTime);
                                    }
                                    else if (RegistryType == "00")
                                    {
                                        curLine.CancelTrade(LastSize, Last);
                                    }
                                }

                                if (CumSize != curLine.CumQuantity) { int a = 0; }
                            }
                        }
                    }
                }
            }
            else
            {
                StopReading = true;
            }
        }

        private int convNumero(string strCurFormat)
        {
            if (strCurFormat == " ") strCurFormat = "0";

            int intCurFormat = int.Parse(strCurFormat);

            switch (intCurFormat)
            {
                case 0: return 1; break;
                case 1: return 10; break;
                case 2: return 100; break;
                case 3: return 1000; break;
                case 4: return 10000; break;
                case 5: return 100000; break;
                case 6: return 1000000; break;
                case 7: return 10000000; break;
                case 8: return 100000000; break;
                default: return 0; break;
            }
        }

        private int convFormaCotacao(int intCurFormat)
        {
            switch (intCurFormat)
            {
                case 1: return 1; break;
                case 3: return 100; break;
                case 4: return 1000; break;
                case 5: return 10000; break;
                default: return 0; break;
            }
        }

        private void updateSessionDate(DateTime _SessionDate)
        {
            SessionDate = _SessionDate;

            //Set Daylight Saving Time difference
            if (SessionDate > new DateTime(2010, 10, 17) && SessionDate < new DateTime(2011, 03, 14))
            {
                DLSTimeDiff = 1;
            }

            //Update session date in each vwap line
            foreach (List<VWAPLine> curList in subData.Values)
            {
                foreach (VWAPLine curLine in curList)
                {
                    curLine.SessionDate = SessionDate;                    
                }                
            }
        }

        private void SetIniTime()
        {
            if (SessionDate > new DateTime(2010, 10, 17) && SessionDate < new DateTime(2011, 03, 14))
            {
                OpenTime = new TimeSpan(10, 45, 00);
                TriggerTime = new TimeSpan(11, 30, 00);
                Closetime = new TimeSpan(15, 00, 00);
            }
            else
            {
                OpenTime = new TimeSpan(09, 45, 00);
                TriggerTime = new TimeSpan(10, 30, 00);
                Closetime = new TimeSpan(14, 00, 00);
            }
        }

        private void LoadSymbols()
        {
            DataTable dt = new DataTable();
            string sqlExpression = "SELECT DISTINCT EXCHANGETICKER, IDSECURITY FROM NESTDB.DBO.TB001_SECURITIES " +
                                   "WHERE IDINSTRUMENT = 2 " +
                                   "AND IDCURRENCY = 900 " +
                                   "AND SUBSTRING(NESTTICKER,LEN(NESTTICKER), 1) <> 'T' " +
                                   "AND SUBSTRING(NESTTICKER,LEN(NESTTICKER), 1) <> 'F' " +
                                   "AND DISCONTINUED = 0 " +
                                   "AND EXCHANGETICKER <> ''";

            using (newNestConn curConn = new newNestConn())
            {
                dt = curConn.Return_DataTable(sqlExpression);
            }

            foreach (DataRow dr in dt.Rows)
            {
                string Ticker = dr[0].ToString();
                long IdSecurity = long.Parse(dr[1].ToString());

                VWAPLine openLine = new VWAPLine();
                openLine.Initialize(SessionDate, OpenTime, TriggerTime, Ticker, IdSecurity);
                VWAPLine closeline = new VWAPLine();
                closeline.Initialize(SessionDate, TriggerTime, Closetime, Ticker, IdSecurity);

                List<VWAPLine> lineList = new List<VWAPLine>();
                lineList.Add(openLine);
                lineList.Add(closeline);

                subData.Add(Ticker, lineList);               
            }
        }

        void curPlayer_OnData(object sender, EventArgs e)
        {
            
            MarketUpdateList curList = (MarketUpdateList)e;

            foreach (MarketUpdateItem curUpdate in curList.ItemsList)
            {
                //Update trade time
                if (curUpdate.FLID == NestFLIDS.TradeTime)
                {
                    DateTime auxTime = Utils.UnixTimestampToDateTime((int)curUpdate.ValueDouble);
                    LastUpdateTime = auxTime.Subtract(DateTime.Today);
                }

                if (LastUpdateTime < Closetime.Add(new TimeSpan(0, 30, 0)))
                {
                    if (curUpdate.FLID == NestFLIDS.Last || curUpdate.FLID == NestFLIDS.LastSize)
                    {
                        List<VWAPLine> vwapLineList = new List<VWAPLine>();

                        if (subData.TryGetValue(curUpdate.Ticker, out vwapLineList))
                        {
                            foreach (VWAPLine curLine in vwapLineList)
                            {
                                if (LastUpdateTime > curLine.IniTime && LastUpdateTime <= curLine.EndTime)
                                {
                                    curLine.Update(curUpdate);
                                }
                            }
                        }
                    }
                }
                else
                {
                    curPlayer.Stop();

                    PrintLines();                    
                }
            }
        }

        public void PrintLines()
        {
            string union = "";
            bool first = true;
            string print = "INSERT INTO NESTTICK.DBO.TB002_VWAPDATA ";
            foreach (List<VWAPLine> curList in subData.Values)
            {
                foreach (VWAPLine curLine in curList)
                {
                    if(curLine.CumQuantity > 0)
                    {
                        print = print + union + curLine.ToString();
                        if (first)
                        {
                            union = " UNION ALL ";
                        }                    
                    }
                }
            }

            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery(print);
            }                      
        }
    }
    class VWAPLine
    {
        public DateTime SessionDate;
        public TimeSpan IniTime;
        public TimeSpan EndTime;
        public string Ticker;
        public long CumQuantity;
        public double FinancialVolume;
        public double VWAP;
        public double Last;
        public long IdSecurity;
        public TimeSpan LastMsgTime;

        private bool LastReceived = false;
        private bool LastSizeReceived = false;
        private double auxLast = 0;
        private long auxLastSize = 0;
        

        public VWAPLine()
        {
        }

        public void Initialize(DateTime _SessionDate, TimeSpan _IniTime, TimeSpan _EndTime, string _Ticker,long _IdSecurity)
        {
            SessionDate = _SessionDate;
            IniTime = _IniTime;
            EndTime = _EndTime;
            Ticker = _Ticker;
            IdSecurity = _IdSecurity;
        }

        public void AddTrade(long _Quantity, double _Price, TimeSpan _LastMsgTime)
        {
            CumQuantity += _Quantity;
            FinancialVolume += _Quantity * _Price;
            Last = _Price;
            VWAP = FinancialVolume / CumQuantity;
            LastMsgTime = _LastMsgTime;
        }

        public void CancelTrade(long _Quantity, double _Price)
        {
            CumQuantity -= _Quantity;
            FinancialVolume -= _Quantity * _Price;            
            VWAP = FinancialVolume / CumQuantity;
        }

        public void Update(MarketUpdateItem curUpdateItem)
        {
            if (curUpdateItem.FLID == NestFLIDS.Last)
            {
                if (LastReceived)
                {
                    int a = 0;
                }
                LastReceived = true;
                auxLast = curUpdateItem.ValueDouble;
            }
            if (curUpdateItem.FLID == NestFLIDS.LastSize)
            {
                if (LastSizeReceived)
                {
                    int a = 0;
                }
                LastSizeReceived = true;
                auxLastSize = Convert.ToInt64(curUpdateItem.ValueDouble);
            }

            if (LastReceived && LastSizeReceived)
            {
                CumQuantity += auxLastSize;
                FinancialVolume += (double)auxLastSize * auxLast;
                VWAP = FinancialVolume / CumQuantity;
                LastReceived = false;
                LastSizeReceived = false;
            }            
        }

        public override string ToString()
        {
            string returnString = "SELECT ";

            try
            {
                returnString = returnString + "'" + SessionDate.ToString("yyyyMMdd") + "'" + ", ";
                returnString = returnString + "'" + EndTime.ToString() + "'" + ", ";
                returnString = returnString + (EndTime - IniTime).TotalMinutes.ToString() + ", ";
                returnString = returnString + "'" + Ticker + "'" + ", ";
                returnString = returnString + CumQuantity.ToString() + ", ";
                returnString = returnString + VWAP.ToString("0.00000000").Replace(',','.') + ", ";
                returnString = returnString + Last.ToString("0.00").Replace(',','.') + ", ";
                returnString = returnString + IdSecurity.ToString();                
            }
            catch(Exception e)
            {
                Console.Write(e.ToString());
            }

            return returnString;
        }
    }
}
