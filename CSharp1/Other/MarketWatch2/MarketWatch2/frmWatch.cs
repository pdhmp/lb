using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.Data;
using DevExpress.Utils;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using NestSymConn;

namespace MarketWatch2
{
    public partial class frmWatch : Form
    {
        List<MarketDataItem> SubscribedData = new List<MarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        TradingStatus curStatusList = new TradingStatus();
        BindingSource bndDataSource = new BindingSource();

        Queue<QuoteLine> QuoteLines = new Queue<QuoteLine>();

        class QuoteLine
        {
            public DateTime LineTime = new DateTime(1999, 01, 01);
            public float[] LineValues = new float[10];
        }


        bool flgMouseDown = false;
     
        public frmWatch()
        {
            InitializeComponent();
        }

        private void SaveItems()
        {
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SymConn.Instance.OnData += new EventHandler(NewMarketData);

            CreateQuote("VALE5", "NA", "BRL", 3, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("PETR4", "NA", "BRL", 1, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("CIEL3", "NA", "BRL", 5790, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("BVMF3", "NA", "BRL", 4660, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("JBSS3", "NA", "BRL", 1219, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("CSNA3", "NA", "BRL", 260, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("GFSA3", "NA", "BRL", 382, 0, 0, new DateTime(1900, 1, 1));
            CreateQuote("MMXM3", "NA", "BRL", 511, 0, 0, new DateTime(1900, 1, 1));

            bndDataSource.DataSource = SubscribedData;
            dgMain.DataSource = bndDataSource;

            dtgQuotes.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Change"].DisplayFormat.FormatString = "P2";

            dtgQuotes.Columns["PCTAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["PCTAuction"].DisplayFormat.FormatString = "P2";

            dtgQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatString = "P2";

            dtgQuotes.Columns["ABSAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["ABSAuction"].DisplayFormat.FormatString = "P2";

            dtgQuotes.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dtgQuotes.Columns["AucVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["AucVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dtgQuotes.Columns["Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dtgQuotes.Columns["AucLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["AucLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dtgQuotes.Columns["Bid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Bid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dtgQuotes.Columns["Ask"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Ask"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dtgQuotes.Columns["Spread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Spread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dtgQuotes.Columns["AvgSpread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["AvgSpread"].DisplayFormat.FormatString = "0.000;(0.000);\\ ";

            dtgQuotes.Columns["FinVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["FinVolume"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dtgQuotes.Columns["Volume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Volume"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dtgQuotes.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtgQuotes.Columns["Expiration"].DisplayFormat.FormatString = "dd-MMM-yy";

            dtgQuotes.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["Strike"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dtgQuotes.Columns["AucGain"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgQuotes.Columns["AucGain"].DisplayFormat.FormatString = "#,##0.;(#,##0.);\\ ";

            dtgQuotes.Columns["Spread"].Visible = true;
            dtgQuotes.Columns["AvgSpread"].Visible = true;

            dtgQuotes.Columns["OptType"].Visible = false;
            dtgQuotes.Columns["Id_Ticker"].Visible = false;
            dtgQuotes.Columns["Id_Instrument"].Visible = false;
            dtgQuotes.Columns["Currency"].Visible = false;
            dtgQuotes.Columns["Exchange"].Visible = false;
            dtgQuotes.Columns["Volume"].Visible = false;
            dtgQuotes.Columns["AucLast"].Visible = false;
            dtgQuotes.Columns["PCTAuction"].Visible = false;
            dtgQuotes.Columns["ABSAuction"].Visible = false;
            dtgQuotes.Columns["CHGPrevAuc"].Visible = false;
            dtgQuotes.Columns["AucVolume"].Visible = false;
            dtgQuotes.Columns["AucCond"].Visible = false;
            dtgQuotes.Columns["AucGain"].Visible = false;
            dtgQuotes.Columns["Expiration"].Visible = false;
            dtgQuotes.Columns["Strike"].Visible = false;
            dtgQuotes.Columns["Sector"].Visible = false;
            dtgQuotes.Columns["PriceMult"].Visible = false;

            tmrUpdate.Start();
        }

        private void CreateQuote(string Ticker, string Sector, string Currency, int Id_Ticker, int Id_Instrument, double Strike, DateTime Expiration)
        {
            MarketDataItem curItem = new MarketDataItem();
            curItem.Id_Ticker = Id_Ticker;
            curItem.Ticker = Ticker;
            curItem.Id_Instrument = Id_Instrument;
            curItem.Sector = Sector;
            curItem.Currency = Currency;
            curItem.Strike = Strike;
            curItem.Expiration = Expiration;
            SubscribedData.Add(curItem);

            UpdateListIndex();

            SymConn.Instance.Subscribe(Ticker);
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].Ticker, i);
            }
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            SymDataEventArgs e = (SymDataEventArgs)origE;

            int i = 0;
            if (SubListIndex.TryGetValue(e.Ticker, out i))
            {


                //for (int i = 0; i < SubscribedData.Count; i++)
                //{
                MarketDataItem curItem = SubscribedData[i];
                if (curItem.Ticker == e.Ticker)
                {

                    if (e.Value[1] != 0) curItem.Last = e.Value[1];
                    if (e.Value[9] != 0) curItem.Bid = e.Value[9];
                    if (e.Value[10] != 0) curItem.Ask = e.Value[10];
                    if (e.Value[26] != 0) curItem.BidSize = e.Value[26];
                    if (e.Value[27] != 0) curItem.AskSize = e.Value[27];
                    if (e.Value[11] != 0) curItem.Volume = e.Value[11];
                    if (e.Value[998] != 0) curItem.Close = e.Value[998];
                    if (e.Value[557] != 0) curItem.PriceMult = 1 / e.Value[557];

                    if (e.Value[45] != 0)
                    {
                        curItem.AucLast = e.Value[45];
                    }
                    else if (e.Value[500] != 0)
                    {
                        if (e.Value[500] != 0) curItem.AucLast = e.Value[500];
                    }


                    if (e.FLID[525] != "" && e.FLID[525] != null)
                    {
                        bool newCondIsAuction = false;
                        bool curCondIsAuction = false;
                        string newCond = curStatusList.ConvStatusId(e.FLID[525]);

                        if (curItem.AucCond == "FROZEN" || curItem.AucCond == "SUSPEND" || curItem.AucCond == "AUCTION") curCondIsAuction = true;
                        if (newCond == "FROZEN" || newCond == "SUSPEND" || newCond == "AUCTION") newCondIsAuction = true;

                        curItem.AucCond = newCond;

                    }
                    else if (e.FLID[997] != "" && e.FLID[997] != null)
                    {
                        //curItem.AucCond = e.FLID[997];
                    }

                    if (e.FLID[552] != "" && e.FLID[552] != null) curItem.AucVolume = e.Value[552];
                }
                else
                {
                    int a = 0;
                }
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (!flgMouseDown) RefreshGrid();
        }

        private void RefreshGrid()
        {
            dtgQuotes.LayoutChanged();
            dtgQuotes.UpdateGroupSummary();
            dtgQuotes.RefreshData();
        }

        private void dtgQuotes_MouseDown(object sender, MouseEventArgs e)
        {
            flgMouseDown = true;
        }

        private void dtgQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            flgMouseDown = false;
        }
    }
}