using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using NCommonTypes;
using NestDLL;
using NestSymConn;
using System.Threading;
using System.IO;
using System.Drawing;

namespace FeedLBSym
{
    public partial class frmMain : Form
    {
        private NDistConn _oNDistConn = new NDistConn();
        private SortedDictionary<int, LBMarketDataItem> _dcSubscribedData = new SortedDictionary<int, LBMarketDataItem>();
        private SortedDictionary<string, List<int>> _dcSubListIndex = new SortedDictionary<string, List<int>>();
        private bool _bInsertInDB = false;
        DateTime TimeToClose;
        static string ConfigurationFolder = @"C:\RTCalculator\";
        string ConfigurationFile = ConfigurationFolder + "MktDataHour.cfg";
        DateTime LastConnectionTry = new DateTime(1900, 01, 01);

        public frmMain()
        {
            InitializeComponent();

            _oNDistConn.OnData += new EventHandler(NewMarketData);
            _oNDistConn.Connect();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tmrClose.Interval = 100;
            tmrClose.Start();
            //ParseLogFile();
            tmrUpdateDB.Start();
            cmdStart_Click(this, new EventArgs());
        }


        private void LoadTimeToClose()
        {
            try
            {
                string Hour = "00";
                string Minutes = "00";

                if (!Directory.Exists(ConfigurationFolder))
                    Directory.CreateDirectory(ConfigurationFolder);

                if (File.Exists(ConfigurationFile))
                {
                    StreamReader sr = new StreamReader(ConfigurationFile);
                    string tempLine = "";
                    while ((tempLine = sr.ReadLine()) != null)
                    {
                        if (tempLine.Contains(":"))
                        {
                            string[] Time = tempLine.Split(':');
                            if (Time[0] != "") Hour = Time[0];
                            if (Time[1] != "") Minutes = Time[1];
                        }
                    }
                    sr.Close();
                }
                else
                {
                    File.Create(ConfigurationFile).Close();
                    StreamWriter sw = new StreamWriter(ConfigurationFile, false);
                    sw.WriteLine("00:00");
                    sw.Close();
                }

                TimeToClose = new DateTime(1900, 01, 01, int.Parse(Hour), int.Parse(Minutes), 00);
            }
            catch { }
        }

        private void ParseLogFile()
        {
            StreamReader sReader = new StreamReader(@"R:\Data\ProxyDiff Uncompressed\20120913_Proxydiff.txt");
            StreamWriter sWriter = new StreamWriter(@"C:\TEMP\TOTS.txt");
            StreamWriter sWriter2 = new StreamWriter(@"C:\TEMP\CONTROL.txt");
            string curLine = "";
            char[] Sep = { (char)18 };
            int LineCounter = 0;
            while ((curLine = sReader.ReadLine()) != null)
            {
                if (curLine.IndexOf((char)17) > 0)
                {
                    string[] prevValues = curLine.Split(Sep);

                    if (prevValues.Length > 1)
                    {
                        PMessage curMessage = new PMessage(prevValues[1]);

                        switch (curMessage.TipoMensagem)
                        {
                            case "01":
                            case "02":
                            case "03":
                            case "05":
                            case "30":
                            case "32":
                            case "53":
                                if (curMessage.CodigoPapel == "TOTS3")
                                {
                                    sWriter.WriteLine(curMessage.DataEvento + "\t" + curMessage.TipoMensagem + "\t" + curMessage.MessageBody);
                                }
                                break;
                            case "39":
                            case "16":
                                if (curMessage.MessageBody.Substring(0, 2) == "21") // MDIA = 24
                                {
                                    sWriter.WriteLine(curMessage.DataEvento + "\t" + curMessage.TipoMensagem + "\t" + curMessage.CodigoPapel + "\t" + curMessage.MessageBody);
                                }
                                if (curMessage.MessageBody.Contains("E"))
                                {
                                    sWriter2.WriteLine(curMessage.DataEvento + "\t" + curMessage.TipoMensagem + "\t" + curMessage.CodigoPapel + "\t" + curMessage.MessageBody);
                                }
                                break;
                            case "07":
                            case "08":
                                sWriter.WriteLine(curMessage.DataEvento + "\t" + curMessage.TipoMensagem + "\t" + curMessage.CodigoPapel + "\t" + curMessage.MessageBody);
                                break;
                            default:
                                break;
                        }
                    }
                }
                LineCounter++;
            }
            sReader.Close();
            sWriter.Close();
            sWriter2.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _oNDistConn.Disconnect();
            _oNDistConn.Dispose();
        }

        private void txtTimeInterval_Leave(object sender, EventArgs e)
        {
            int curInterval = 0;
            try
            {
                curInterval = Convert.ToInt32(txtTimeInterval.Text);
            }
            catch
            {
                curInterval = 1000;
                txtTimeInterval.Text = "1000";
            }
            tmrUpdateDB.Interval = curInterval;
            lblStatus.Text = "Interval Changed: " + tmrUpdateDB.Interval.ToString();
        }

        private void txtTimeInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                lblLastUpdate.Select();
            }
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
            _oNDistConn.Connect();
            _LoadTickers();
            _bInsertInDB = true;
            lblStatus.Text = "Started";
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
            _bInsertInDB = false;
            _oNDistConn.Disconnect();
            lblStatus.Text = "Stopped";
        }

        private void tmrUpdateDB_Tick(object sender, EventArgs e)
        {
            #region Tenta Conectar automaticamente
            if (_oNDistConn.IsConnected())
            {
                lblConnected.BackColor = Color.FromArgb(0, 192, 0);
                lblConnected.Text = "CONNECTED";
            }
            else
            {
                cmdStart_Click(this, new EventArgs());
                lblConnected.BackColor = Color.Red;
                lblConnected.Text = "DISCONNECTED";
                if (lblStatus.Text != "Stopped")
                {

                    if (DateTime.Now.Subtract(LastConnectionTry).TotalSeconds > 10)
                    {
                        try
                        {
                            _oNDistConn.Connect();
                            Thread.Sleep(2000);
                            _oNDistConn.ReSubscribe();
                            LastConnectionTry = DateTime.Now;
                        }
                        catch { }
                    }
                }
            }

            #endregion

            if (_bInsertInDB)
            {
                _InsertLastItems();
                _InsertBidAskItems();
            }

        }

        private void tmrReloadTickers_Tick(object sender, EventArgs e)
        {
            _LoadTickers();
        }

        private void NewMarketData(object sender, EventArgs e)
        {
            MarketUpdateList oUpdateList = (MarketUpdateList)e;
            foreach (MarketUpdateItem curUpdateItem in oUpdateList.ItemsList)
            {
                if (curUpdateItem.Ticker == "IBOV" && curUpdateItem.FLID == NestFLIDS.Last) Console.WriteLine("IBOV Received:" + curUpdateItem.ValueDouble);

                List<int> lIdTickers;
                lock (_dcSubListIndex)
                {
                    if (_dcSubListIndex.TryGetValue(curUpdateItem.Ticker, out lIdTickers))
                    {
                        foreach (int curIdTicker in lIdTickers)
                        {
                            LBMarketDataItem curItem = _dcSubscribedData[curIdTicker];
                            curItem.Update(curUpdateItem);
                            if (curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble != 0) { _dcSubscribedData[curIdTicker].LastUpdated = true; }
                            if (curUpdateItem.FLID == NestFLIDS.SettlementPrice && curUpdateItem.ValueDouble != 0) { _dcSubscribedData[curIdTicker].SettUpdated = true; }
                            if (curUpdateItem.FLID == NestFLIDS.Close && curUpdateItem.ValueDouble != 0) 
                            { 
                                _dcSubscribedData[curIdTicker].CloseUpdated = true; 
                            }

                            // mk
                            if (curItem.TradingStatus != TradingStatusType.AUCTION_K && curItem.TradingStatus != TradingStatusType.G_AFTERMKT_R)
                            {
                                curItem.Update(curUpdateItem);
                                if (curUpdateItem.FLID == NestFLIDS.Bid) { _dcSubscribedData[curIdTicker].BidUpdated = true; }
                                if (curUpdateItem.FLID == NestFLIDS.Ask) { _dcSubscribedData[curIdTicker].AskUpdated = true; }
                            }

                        }
                    }
                }
            }
        }

        private void _LoadTickers()
        {
            bool bAddedTickers = false;
            using (newNestConn curConn = new newNestConn())
            {
                string sStringSQl = " SELECT " +
                                    " IdSecurity,NestTicker,BloombergTicker,AdminTicker,ReutersTicker,ExchangeTicker, " +
                                    " IdInstrument,QuotedAsRate,RTUpdateSource,IdInstrument,IdCurrency,QuotedAsRate " +
                                    " FROM dbo.Tb001_Securities A " +
                                    " WHERE RTUpdate = 1 AND " +
                                    " ((Expiration IS NULL OR Expiration = '19000101') OR (Expiration >= (CONVERT(varchar, GETDATE() - 5, 112)))) AND " +
                                    " IdInstrument NOT IN (1,2,3,4,7,16,15) " +
                                    " UNION ALL " +
                                    " SELECT " +
                                    " IdSecurity,NestTicker,BloombergTicker,AdminTicker,ReutersTicker,ExchangeTicker," +
                                    " IdInstrument,QuotedAsRate,RTUpdateSource,IdInstrument,IdCurrency,QuotedAsRate " +
                                    " FROM dbo.Tb001_Securities B " +
                                    " WHERE IdSecurity IN  " +
                                    " ( " +
                                    " SELECT [Id Ticker] FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now] >= GETDATE()-5 UNION ALL " +
                                    " SELECT [Id Ticker] FROM NESTRT.dbo.Tb000_Posicao_Atual UNION ALL " +
                                    " SELECT B.IdSecurity " +
                                    " FROM dbo.Tb023_Securities_Composition A (nolock) " +
                                    " INNER JOIN dbo.Tb001_Securities B (nolock) " +
                                    " ON A.Id_Ticker_Component=B.IdSecurity " +
                                    " WHERE Id_Ticker_Composite=1073 AND Date_Ref=(SELECT MAX(Date_Ref) FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073) " +
                                    " ) " +
                                    " AND RTUpdate = 1 AND " +
                                    " ((Expiration IS NULL OR Expiration = '19000101') OR (Expiration >= (CONVERT(varchar, GETDATE() - 5, 112)))) ";



                DataTable oTable = curConn.Return_DataTable(sStringSQl);

                foreach (DataRow curRow in oTable.Rows)
                {
                    int curIdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                    Sources curSource = (Sources)((int)NestDLL.Utils.ParseToDouble(curRow["RTUpdateSource"]));
                    if (!_dcSubscribedData.ContainsKey(curIdSecurity))
                    {
                        LBMarketDataItem curItem = new LBMarketDataItem();
                        curItem.IdTicker = curIdSecurity;
                        curItem.Ticker = curRow["NestTicker"].ToString();
                        curItem.QuotedAsRate = (int)NestDLL.Utils.ParseToDouble(curRow["QuotedAsRate"]);
                        //if (curSource == Sources.Reuters)
                        //{
                        //    if (NestDLL.Utils.ParseToDouble(curRow["IdCurrency"]) == 900)
                        //    {
                        //        if (NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]) == 2 || NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]) == 3)
                        //        {
                        //            curSource = Sources.Bovespa;
                        //        }
                        //        else
                        //        {
                        //            curSource = Sources.BMF;
                        //        }
                        //    }
                        //}
                        curItem.Source = curSource;

                        switch (curSource)
                        {
                            case Sources.Bloomberg:
                                curItem.SubscribeTicker = curRow["BloombergTicker"].ToString();
                                break;
                            case Sources.Yahoo:
                                curItem.SubscribeTicker = curRow["AdminTicker"].ToString();
                                break;
                            case Sources.Reuters:
                            case Sources.FlexTrade:
                                curItem.SubscribeTicker = curRow["ReutersTicker"].ToString();
                                break;
                            case Sources.BMF:
                            case Sources.Bovespa:
                            case Sources.ProxyDiff:
                            case Sources.XPBMF:
                            case Sources.XPBOV:
                            case Sources.BELL:
                                curItem.SubscribeTicker = curRow["ExchangeTicker"].ToString();
                                break;
                            case Sources.BancoCentral:
                            case Sources.None:
                            default:
                                curItem.SubscribeTicker = curRow["NestTicker"].ToString();
                                break;
                        }

                        if (curItem.SubscribeTicker != "")
                        {
                            lock (_dcSubscribedData)
                            {
                                _dcSubscribedData.Add(curIdSecurity, curItem);
                                bAddedTickers = true;
                            }

                        }
                    }
                    else
                    {

                        if (_dcSubscribedData[curIdSecurity].Source != curSource)
                        {
                            _dcSubscribedData[curIdSecurity].Source = curSource;
                            _dcSubscribedData[curIdSecurity].Subscribed = false;
                            bAddedTickers = true;
                        }
                    }
                }

                lblTickerCount.Text = _dcSubscribedData.Count.ToString();
            }

            if (bAddedTickers)
            {
                // Break
                _UpdateListIndex();
                _SubscribeTickers();
            }
        }

        private void _SubscribeTickers()
        {
            foreach (LBMarketDataItem curItem in _dcSubscribedData.Values)
            {
                if (!curItem.Subscribed)
                {
                    _oNDistConn.Subscribe(curItem.SubscribeTicker, curItem.Source);
                    curItem.Subscribed = true;
                }
            }
        }

        private void _UpdateListIndex()
        {
            lock (_dcSubListIndex)
            {
                _dcSubListIndex.Clear();
                foreach (LBMarketDataItem curSubscribedData in _dcSubscribedData.Values)
                {
                    if (!_dcSubListIndex.ContainsKey(curSubscribedData.SubscribeTicker))
                    {
                        _dcSubListIndex.Add(curSubscribedData.SubscribeTicker, new List<int>());
                    }
                    _dcSubListIndex[curSubscribedData.SubscribeTicker].Add(curSubscribedData.IdTicker);
                }
            }
        }

        private void _InsertLastItems()
        {
            string sSQLString = "";
            lblLastUpdate.Text = DateTime.Now.ToString("HH:mm:ss");
            try
            {
                foreach (LBMarketDataItem curItem in _dcSubscribedData.Values)
                {
                    if (curItem.LastUpdated && curItem.LastTrade != 0)
                    {
                        int InsertType = 1;
                        double AdjustFactor = 1;
                        if (curItem.QuotedAsRate == 1)
                        {
                            InsertType = 30;
                            AdjustFactor = 100;
                        }

                        if (curItem.IdTicker == 1073) Console.WriteLine("IBOV Updated:" + curItem.LastTrade);

                        sSQLString += " EXEC [NESTRT].[dbo].[Proc_Insert_Price_RT] " + curItem.IdTicker.ToString() + ", " + (curItem.LastTrade / AdjustFactor).ToString().Replace(",", ".") + ", '" + curItem.LastTradeTime.ToString("yyyyMMdd HH:mm:ss") + "', " + InsertType + ", " + (int)curItem.Source + ", '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', 1; ";
                        curItem.LastUpdated = false;
                    }

                    // Captura preco de fechamento.
                    if (curItem.SettUpdated && curItem.LastTrade != 0)
                    {
                        int InsertType = 15;
                        double AdjustFactor = 1;
                        if (curItem.QuotedAsRate == 1)
                        {
                            // InsertType = 30; Nao trocar para Rate - Continuar inserido em 15
                            AdjustFactor = 100;
                        }

                        // sSQLString += " EXEC [NESTRT].[dbo].[Proc_Insert_Price_RT] " + curItem.IdTicker.ToString() + ", " + (curItem.SettlementPrice).ToString().Replace(",", ".") + ", '" + curItem.LastTradeTime.ToString("yyyyMMdd HH:mm:ss") + "', " + InsertType + ", " + (int)curItem.Source + ", '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', 1; ";
                        sSQLString += " EXEC [NESTRT].[dbo].[Proc_Insert_Price_RT] " + curItem.IdTicker.ToString() + ", " + (curItem.SettlementPrice / AdjustFactor).ToString().Replace(",", ".") + ", '" + curItem.LastTradeTime.ToString("yyyyMMdd HH:mm:ss") + "', " + InsertType + ", " + (int)curItem.Source + ", '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', 1; ";
                        curItem.SettUpdated = false;
                    }

                    if (curItem.CloseUpdated && curItem.Close != 0)
                    {
                        int InsertType = 2;
                        double AdjustFactor = 1;
                        if (curItem.QuotedAsRate == 1)
                        {
                            InsertType = 30;
                            AdjustFactor = 100;
                        }

                        sSQLString += " EXEC [NESTRT].[dbo].[Proc_Insert_Price_RT] " + curItem.IdTicker.ToString() + ", " + (curItem.Close / AdjustFactor).ToString().Replace(",", ".") + ", '" + curItem.LastTradeTime.ToString("yyyyMMdd HH:mm:ss") + "', " + InsertType + ", " + (int)curItem.Source + ", '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', 1; ";
                        curItem.LastUpdated = false;
                    }
                }
                if (sSQLString != "")
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(sSQLString, 0);
                    }
                }
            }
            catch (Exception e)
            {
                lblStatus.Text = e.ToString();
            }
        }

        private void _InsertBidAskItems()
        {
            string sSQLString = "";
            lblLastUpdate.Text = DateTime.Now.ToString("HH:mm:ss");
            try
            {
                // Watch
                foreach (LBMarketDataItem curItem in _dcSubscribedData.Values)
                {
                    if (curItem.BidUpdated)
                    {
                        sSQLString += " EXEC [NESTRT].[dbo].[Proc_Insert_Price_RT] " + curItem.IdTicker.ToString() + ", " + curItem.Bid.ToString().Replace(",", ".") + ", '" + curItem.LastTradeTime.ToString("yyyyMMdd HH:mm:ss") + "', 9, " + (int)curItem.Source + ", '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', 1; ";
                        curItem.BidUpdated = false;
                    }
                    if (curItem.AskUpdated)
                    {
                        sSQLString += " EXEC [NESTRT].[dbo].[Proc_Insert_Price_RT] " + curItem.IdTicker.ToString() + ", " + curItem.Ask.ToString().Replace(",", ".") + ", '" + curItem.LastTradeTime.ToString("yyyyMMdd HH:mm:ss") + "', 10, " + (int)curItem.Source + ", '" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "', 1; ";
                        curItem.AskUpdated = false;
                    }
                }
                if (sSQLString != "")
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(sSQLString, 0);
                    }
                }
            }
            catch (Exception e)
            {
                lblStatus.Text = e.ToString();
            }
        }

        private void cmdResubscribe_Click(object sender, EventArgs e)
        {
            //Thread.Sleep(3 * (60 * 1000));
            _oNDistConn.Connect();
            Thread.Sleep(2000);
            _oNDistConn.ReSubscribe();
        }

        private void cmdReloadTickers_Click(object sender, EventArgs e)
        {
            _LoadTickers();
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            tmrClose.Interval = 60000;

            LoadTimeToClose();

            if (DateTime.Now.TimeOfDay >= TimeToClose.TimeOfDay)
            {
                this.Close();
            }
        }
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            System.Threading.Thread.Sleep(3000);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
