using System.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCommonTypes;
using System.IO;
using NestDLL;
using NestSymConn;

namespace UpdateBovespaSecurities
{
    public partial class frmUpdateSecurities : Form
    {
        NDistConn curDistConn = new NDistConn("192.168.0.215", 15432);
        //NDistConn curDistConn = new NDistConn("127.0.0.1", 15432);
        Queue<SecListUpdateItem> UpdateQueue = new Queue<SecListUpdateItem>();
        DateTime LastConnectionTry = new DateTime(1900, 01, 01);

        int ItemsProcessed = 0;
        double ItemsTotal = 0;
        int ItemsCreated = 0;
        int ItemsUpdated = 0;

        string curStatus = "";

        bool UpdateRunning = false;
        Thread t1;
        Thread t2;

        public frmUpdateSecurities()
        {
            InitializeComponent();
        }

        private void frmUpdateSecurities_Load(object sender, EventArgs e)
        {
            curDistConn.OnSecList += new EventHandler(NewSecList);
            curDistConn.Connect();
            curDistConn.OnDisconnect += new EventHandler(Connect);
            curDistConn.GetSecList(Sources.XPUMDFBov);
            tmrUpdate.Start();

            t2 = new Thread(InsertItemsLoop);
            t2.IsBackground = true;
            t2.Start();
        }

        private void NewSecList(object sender, EventArgs origE)
        {
            ItemsTotal++;
            lock (UpdateQueue) { UpdateQueue.Enqueue((SecListUpdateItem)origE); }
        }
        
        private void Connect(object sender, EventArgs origE)
        {
            if (curDistConn != null && !curDistConn.IsConnected())
            {
                if (DateTime.Now.Subtract(LastConnectionTry).TotalSeconds > 60)
                {
                    try
                    {
                        curDistConn.Connect();
                        LastConnectionTry = DateTime.Now;
                    }
                    catch { }
                }
            }
            else
            {
                LastConnectionTry = DateTime.Now;
            }
        }

        private void AtualizaBarra(string sTexto)
        {
            //lblRead.Text = sTexto;
            //lblRead.Update();

            //progressBar1.Value++;
            //progressBar1.Update();
        }

        private void UpdateCadastro()
        {
            UpdateRunning = true;

            DataTable rootTable;
            using (newNestConn curConn = new newNestConn())
            {
                string sSQL =
                        " SELECT * FROM NESTDB.dbo.FCN001_Securities_All('" + DateTime.Now.ToString("yyyyMMdd") + "') " +
                        " WHERE IdInstrument <> 26 AND " +
                        " (Expiration>='" + DateTime.Now.ToString("yyyyMMdd") + "' " +
                        " OR Expiration='1900-01-01') ORDER BY ExchangeTicker";
                rootTable = curConn.Return_DataTable(sSQL);
            }

            while (UpdateQueue.Count >= 1)
            {
                SecListUpdateItem curSecListUpdateItem;

                lock (UpdateQueue)
                {
                    curSecListUpdateItem = UpdateQueue.Dequeue();
                }

                ItemsProcessed++;
                if (ItemsProcessed >= ItemsTotal && ItemsTotal>5000) processFinished = true;

                var name = from r in rootTable.AsEnumerable()
                           where r.Field<string>("ExchangeTicker") == curSecListUpdateItem.Ticker
                           select r;

                DataTable curTable = name.Any() ? name.CopyToDataTable() : rootTable.Clone();

                if (curTable.Rows.Count > 0)
                {
                    DataRow curRow = curTable.Rows[0];
                    bool CreateNewRow = false;

                    if (//NestDLL.Utils.ParseToDouble(curRow["RoundLot"]) != curSecListUpdateItem.LotSize ||
                        //NestDLL.Utils.ParseToDouble(curRow["RoundLotSize"]) != curSecListUpdateItem.LotSize || // verificar
                        Math.Round(NestDLL.Utils.ParseToDouble(curRow["Strike"]), 4) != Math.Round(curSecListUpdateItem.Strike, 4) //||
                        //curRow["ISIN"].ToString() != curSecListUpdateItem.ISIN
                        )
                    {
                        CreateNewRow = true;
                    }

                    if (CreateNewRow)
                    {
                        AddStatus("Updating Security..." + curRow["ExchangeTicker"]);

                        double ExpirationPrice = NestDLL.Utils.ParseToDouble(curRow["ExpirationPrice"]);

                        if (NestDLL.Utils.ParseToDateTime(curRow["ValidAsOfDate"]) != DateTime.Now.Date)
                        {
                            string InsertSQL = " INSERT INTO NESTDB.dbo.Tb001_Securities_Variable" +
                                                " (IdSecurity,ValidAsOfDate,SecName,NestTicker,ISIN,ExchangeTicker,IdPrimaryExchange,RoundLot,RoundLotSize,Expiration,Strike,ADRRatio,DealAnnounceDate,IsInDeal,DealDetails,ExpirationPrice,PriceFromUnderlying) " +
                                                " SELECT " + curRow["IdSecurity"] +
                                                ", '" + DateTime.Now.ToString("yyyyMMdd") + "'" +
                                                ", '" + curRow["SecName"] + "'" +
                                                ", '" + curRow["NestTicker"] + "'" +
                                                ", '" + curRow["ISIN"].ToString() + "'" +
                                                ", '" + curRow["ExchangeTicker"] + "'" +
                                                ", 2" +
                                //", " + curSecListUpdateItem.LotSize.ToString().Replace(",", ".") +
                                                ", " + curRow["RoundLot"].ToString().Replace(",", ".") +
                                //", " + curSecListUpdateItem.RoundLotSize.ToString().Replace(",", ".") + // verificar
                                                ",  " + curRow["RoundLotSize"].ToString().Replace(",", ".") + // " + curSecListUpdateItem.RoundLotSize.ToString().Replace(",", ".") + // verificar
                                                ", '" + NestDLL.Utils.ParseToDateTime(curRow["Expiration"]).ToString("yyyy-MM-dd") + "'" +
                                                ", " + curSecListUpdateItem.Strike.ToString().Replace(",", ".") +
                                                ", " + curRow["ADRRatio"] +
                                                ", '" + NestDLL.Utils.ParseToDateTime(curRow["DealAnnounceDate"]).ToString("yyyy-MM-dd") + "'" +
                                                ", " + curRow["IsInDeal"] +
                                                ", '" + curRow["DealDetails"] + "'" +
                                                ", " + ExpirationPrice.ToString().Replace(",", ".") +
                                                ", " + curRow["PriceFromUnderlying"];

                            using (newNestConn curConn = new newNestConn()) { curConn.ExecuteNonQuery(InsertSQL, 0); }

                            ItemsUpdated++;
                        }
                    }
                }
                else
                {
                    string curOptionType = curSecListUpdateItem.OptionType.ToString();
                    if (curSecListUpdateItem.OptionType == "c" || curSecListUpdateItem.OptionType == "p")
                    {
                        lock (ItemsToCreate)
                        {
                            ItemsToCreate.Enqueue(curSecListUpdateItem);
                            TotalItemsToCreate++;
                            //CreateOption(curSecListUpdateItem);
                        }
                    }
                }
            }

            UpdateRunning = false;
        }

        Queue<SecListUpdateItem> ItemsToCreate = new Queue<SecListUpdateItem>();
        bool processFinished = false;
        int TotalItemsToCreate = 0;

        private void InsertItemsLoop()
        {
            while (!processFinished || ItemsToCreate.Count>0)
            {
                if (ItemsToCreate.Count != 0)
                {
                    SecListUpdateItem curCreateItem;
                    lock (ItemsToCreate)
                    {
                        curCreateItem = ItemsToCreate.Dequeue();
                    }
                    CreateOption(curCreateItem);
                }
            }
        }

        private void CreateOption(SecListUpdateItem curSecListUpdateItem)
        {

            AddStatus("Inserting new Option... " + curSecListUpdateItem.Ticker);
            try
            {
                using (newNestConn curConn = new newNestConn())
                {
                    string Ticker_Bloomberg = "";
                    string BBGMod = "";

                    //int LoteCotacao = convFormaCotacao(int.Parse(msgBody.Substring(236, 1)));
                    int LoteCotacao = convFormaCotacao(1);

                    int IdOptionType = 0;
                    if (curSecListUpdateItem.OptionType == "C") IdOptionType = 1;
                    if (curSecListUpdateItem.OptionType == "P") { IdOptionType = 0; BBGMod = "E"; }

                    int Id_Underlying = curConn.Return_Int("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Variable WHERE ExchangeTicker='" + curSecListUpdateItem.Underlying + "' AND IdPrimaryExchange=2");
                    int Id_Issuer = curConn.Return_Int("SELECT IdIssuer FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=" + Id_Underlying);


                    string BBGExpiration;

                    if (int.Parse(curSecListUpdateItem.Expiration.Substring(4, 2)) < 10)
                    {
                        BBGExpiration = curSecListUpdateItem.Expiration.Substring(5, 1);
                    }
                    else
                    {
                        BBGExpiration = curSecListUpdateItem.Expiration.Substring(4, 2);
                    }

                    //Ticker_Bloomberg = curSecListUpdateItem.Underlying +
                    //                    BBGMod +
                    //                    " BZ " +
                    //                    BBGExpiration +
                    //                    " " +
                    //                    curSecListUpdateItem.OptionType +
                    //                    curSecListUpdateItem.Strike.ToString("##.##").Replace(",", ".") +
                    //                    " Equity";

                    //string TickerReplace1 = msgBody.Substring(623, 10).Trim();
                    //string TickerReplace2 = msgBody.Substring(0, 10).Trim();

                    //Ticker_Reuters = Ticker.Replace(TickerReplace2, TickerReplace1) + ".SA";

                    //Ticker_Imagine = Ticker_Reuters + "-" + Expiration.Substring(2, 2);

                    string SQLString =
                            " INSERT INTO NESTDB.dbo.Tb001_Securities_Fixed " +
                            " (Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass],IdStrikeCurrency,IdPremiumCurrency,BloombergTicker,ReutersTicker,Imagineticker,OptionType,BtgTicker,ItauTicker,MellonTicker, CyrnelTicker , RTUpdate, RTUpdateSource, HistUpdate, HistUpdateSource, HistUpdateFrequency )" +
                            " VALUES (''," + Id_Underlying + ",7" + ", " + Id_Issuer + ",900,3,1,0,0,'" + Ticker_Bloomberg + "','',''," + IdOptionType + ",'" + curSecListUpdateItem.Ticker + "','" + curSecListUpdateItem.Ticker + "','" + curSecListUpdateItem.Ticker + "','" + curSecListUpdateItem.Ticker + "' , 0 , 24 , 1, 24, 2  ) ; ";
                            

                    int NewIdSecurity = 1; int RetVal = curConn.ExecuteNonQuery(SQLString);

                    ItemsCreated++;

                    NewIdSecurity = curConn.Return_Int(" SELECT @@IDENTITY");

                    if (NewIdSecurity > 0)
                    {
                        SQLString =
                                "INSERT INTO Tb001_Securities_Variable " +
                                "([IdSecurity],[SecName],[NestTicker],[RoundLot],[RoundLotSize],IdPrimaryExchange,Expiration,Strike,ExchangeTicker,ISIN,PriceFromUnderlying )" +
                                " VALUES (" +
                                NewIdSecurity + ", '" +
                                curSecListUpdateItem.Ticker + "-" + curSecListUpdateItem.Expiration.Substring(2, 2) + "','" +
                                curSecListUpdateItem.Ticker + "-" + curSecListUpdateItem.Expiration.Substring(2, 2) + "'," +
                                LoteCotacao.ToString().Replace(",", ".") + "," +
                                curSecListUpdateItem.LotSize.ToString().Replace(",", ".") + ",2,'" +
                                curSecListUpdateItem.Expiration + "'," +
                                curSecListUpdateItem.Strike.ToString().Replace(",", ".") + ",'" +
                                curSecListUpdateItem.Ticker + "', '" +
                                curSecListUpdateItem.ISIN + "',0)"; 

                        curConn.ExecuteNonQuery(SQLString, 0);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private int convFormaCotacao(int intCurFormat)
        {
            switch (intCurFormat)
            {
                case 1: return 1;
                case 3: return 100;
                case 4: return 1000;
                case 5: return 10000;
                default: return 0;
            }
        }

        private String curStatusMessage = "";

        private void AddStatus(string Message)
        {
            curStatusMessage = curStatusMessage + Message+ "\r\n";
            curStatus = Message;
        }

        public class QuoteItem
        {
            public string RealStatus
            {
                get
                {
                    if (this.StatusAction == "I") { return "H"; }; //INHIBITED
                    if (this.Status == "S") { return "U"; }//SUSP
                    if (this.Status == "G") { return "G"; }//FROZEN
                    //if (this.Status == "R" && (this.StatusTrading == "H" || this.StatusTrading == "B" || this.StatusTrading == "P" || this.StatusTrading == "R")) { return "AUCTION"; }
                    if (this.Status == "R") { return "K"; }//AUCTION
                    if (this.Status == "*" || this.Status == "")
                    {
                        if (this.StatusGroup == "B") { return "B"; }//G_NIGHT
                        if (this.StatusGroup == "C") { return "C"; }//G_PREP
                        if (this.StatusGroup == "E") { return "E"; }//G_INTERV
                        if (this.StatusGroup == "F") { return "F"; }//G_EODAY
                        if (this.StatusGroup == "O") { return "O"; }//G_OPENING
                        if (this.StatusGroup == "P") { return "P"; }//G_PREOPEN
                        if (this.StatusGroup == "R") { return "R"; }//G_AFTERMKT
                        if (this.StatusGroup == "S") { return "S"; }//G_TRADING
                        if (this.StatusGroup == "I") { return "I"; }//G_INPEDED
                        if (this.StatusGroup == "N") { return "N"; }//G_MKTCONTROL
                        if (this.StatusGroup == "Z") { return "Z"; }//G_INTERRUPTED
                    }
                    return "UNKNOWN";
                }
            }

            public string Nome = "";
            public string Especificacao = "";
            public Int64 QuantidadeTit = 0;
            public int GrupoCotacao = 0;
            public int LotePadrao = 0;
            public int LoteCotacao = 0;
            public DateTime DataUltNeg = new DateTime(1900, 01, 01);
            public float Close = 0;
            public int FormaCotacaoClose = 0;
            public float Exercicio = 0;
            // public int FormaCotacaoExercicio = 0;
            public string ISIN = "";
            public Int64 MediaUltPregoes = 0;
            public string Ticker = "";

            public int SegmentoMercado = 0;

            public string StatusAction = "";
            public string Status = "";
            public string StatusTrading = "";
            public string StatusGroup = "";

            public MarketDataItem MarketData = new MarketDataItem();

            public TimeSpan ReOpenTime = new TimeSpan(0, 0, 0);

            public QuoteItem()
            {
            }
        }

        int tempCounter = 0;

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (!curDistConn.IsConnected()) { Connect(sender, e); }

            tempCounter++;

            if (tempCounter >= (60*2))
            {
                using (newNestConn curConn = new newNestConn())
                {
                    string StringSQL = "EXEC NESTDB.dbo.PROC_UPDATE_EXCHANGE_TICKER_EXPIRATION";
                    curConn.ExecuteNonQuery(StringSQL);
                }
                tempCounter = 0;
            }

            labCounter.Text = ItemsTotal.ToString();
            labNewItems.Text = ItemsCreated.ToString() + "/" + TotalItemsToCreate.ToString();
            lblStatus.Text = curStatus;
            labProcessed.Text = ItemsProcessed.ToString();
            labItemsUpdated.Text = ItemsUpdated.ToString();
            txtStatus.Text = curStatusMessage;
            txtStatus.SelectionStart = txtStatus.TextLength;
            txtStatus.ScrollToCaret();

            if (UpdateQueue.Count > 0 && UpdateRunning == false)
            {
                UpdateRunning = true;

                t1 = new Thread(UpdateCadastro);
                t1.IsBackground = true;
                t1.Start();
            }
        }
    }
}
