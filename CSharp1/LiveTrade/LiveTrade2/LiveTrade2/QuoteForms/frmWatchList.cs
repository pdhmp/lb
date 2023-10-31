using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DevExpress.Data;
using DevExpress.Utils;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using NestDLL;
using NestSymConn;
using NCustomControls;
using NCommonTypes;

namespace LiveTrade2
{
    public partial class frmWatchList : ConnectedForm
    {
        public event EventHandler OnItemAlert;
        List<LTMarketDataItem> SubscribedData = new List<LTMarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        BindingSource bndDataSource = new BindingSource();
        bool flgMouseDown = false;
        public string SQLString;
        int clearCounter = 0;
        public int OpenMode = 0;
        
        TradingStatus curStatusList = new TradingStatus();
        Utils curUtils = new Utils(5);
        double WaitCounter = 0;

        bool SelectFirstonNextSort = false;
        bool ListEditMode = false;
        GridGroupSummaryItem SortOrderGroupItem;

        int GridMinTop = 0;
        int GridMaxTop = 170;

        bool flgSort = true;

        private bool _CustomListFlag = false;
        public bool CustomListFlag
        {
            get { return _CustomListFlag; }
            set { _CustomListFlag = value; }
        }

        private int _CustomListID = -1;
        public int CustomListID
        {
            get { return _CustomListID; }
            set { _CustomListID = value; }
        }

        public frmWatchList()
        {
            InitializeComponent();
        }

        private void frmWatchList_Load(object sender, EventArgs e)
        {
            curNDistConn.OnData += new EventHandler(NewMarketData);

            dtpHL_IniDate.Value = LiveTrade2.Properties.Settings.Default.IniDate;
            dtpHL_EndDate.Value = LiveTrade2.Properties.Settings.Default.EndDate;

            chkLiquid.BackColor = Color.Transparent;
            chkLiquid.Parent = (Control)dtgWatchList;

            chkAuction.Parent = (Control)dtgWatchList;
            chkAuction.BackColor = Color.Transparent;

            chkMH_Trad.Parent = (Control)dtgWatchList;
            chkMH_Trad.BackColor = Color.Transparent;

            chkMH_LS.BackColor = Color.Transparent;
            chkMH_LS.Parent = (Control)dtgWatchList;

            chkFIA_Trad.BackColor = Color.Transparent;
            chkFIA_Trad.Parent = (Control)dtgWatchList;

            chkFIA_LS.BackColor = Color.Transparent;
            chkFIA_LS.Parent = (Control)dtgWatchList;


            dtgWatchList.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgWatchList.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgWatchList.LookAndFeel.SetSkinStyle("Blue");

            dgWatchList.ColumnPanelRowHeight = 32;
            dgWatchList.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            dgWatchList.Appearance.Row.ForeColor = GlobalVars.Instance.GridForeColor;
            dgWatchList.Appearance.Row.BackColor = GlobalVars.Instance.GridBackColor;
            dgWatchList.OptionsView.ShowHorzLines = false;
            dgWatchList.OptionsView.ShowVertLines = false;
            dgWatchList.Appearance.Empty.BackColor = GlobalVars.Instance.GridBackColor;
            dgWatchList.Appearance.FocusedRow.BackColor = Color.DarkGray;
            dgWatchList.Appearance.SelectedRow.BackColor = Color.DarkGray;
            

            dtgWatchList.Width = this.Width - 9;
            dtgWatchList.Height = this.Height - 120 + 65;

            SQLString = "SELECT C.NestTicker AS Ticker, C.IdPrimaryExchange, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument,        " +
                      "COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), A.Category, 0 AS SortOrder, MHTRAD_Pos, MHLS_Pos, FIATRAD_Pos, FIALS_Pos, RoundLot           " +
                      "FROM(                                                                                                                                             " +
                      "	SELECT IdSecurity, 'ALL' AS Category FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument IN(1,2,7) AND IdCurrency IN (900, 1042) AND (Expiration>=getdate() OR Expiration='1900-01-01')   " + 
                      ") A                                                                                                                                               " +
                      " LEFT JOIN NESTDB.dbo.Tb001_Securities C                                                                                                          " +
                      " ON A.IdSecurity=C.IdSecurity                                                                                                                     " +
                      " LEFT JOIN NESTDB.dbo.Tb000_Issuers D                                                                                                        " +
                      " ON C.IdIssuer=D.IdIssuer                                                                                                                   " +
                      " LEFT JOIN NESTDB.dbo.Tb113_Setores E                                                                                                             " +
                      " ON D.IdNestSector=E.Id_Setor                                                                                                                         " +
                      " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS MHTRAD_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=43 GROUP BY [id base underlying]) MHTRAD" +
                      " ON A.IdSecurity=MHTRAD.IdSecurity                                                                                                                   " +
                      " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS MHLS_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=43 GROUP BY [id base underlying]) MHLS" +
                      " ON A.IdSecurity=MHLS.IdSecurity                                                                                                                   " +
                      " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS FIATRAD_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=10 GROUP BY [id base underlying]) FIATRAD" +
                      " ON A.IdSecurity=FIATRAD.IdSecurity                                                                                                                   " +
                      " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS FIALS_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=10 GROUP BY [id base underlying]) FIALS" +
                      " ON A.IdSecurity=FIALS.IdSecurity                                                                                                                   " +
                       " WHERE ReutersTicker IS NOT NULL AND ReutersTicker<>'' AND C.IdInstrument<>3                                                                                         " +
                      " ORDER BY Setor                                                                                                                                   ";


            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    string str_aux = curRow[0].ToString();
                    if (str_aux.Contains("-"))
                    {
                        str_aux = str_aux.Substring(0, str_aux.IndexOf("-"));
                        curRow[0] = str_aux;
                    }

                    if (!SubListIndex.ContainsKey(curRow[0].ToString()))
                    {
                        LTMarketDataItem curItem = CreateQuote((string)curRow[0], (int)NestDLL.Utils.ParseToDouble(curRow[1].ToString()), curRow[2].ToString(), curRow[3].ToString(), (int)NestDLL.Utils.ParseToDouble(curRow[4]), (int)NestDLL.Utils.ParseToDouble(curRow[5]), NestDLL.Utils.ParseToDouble(curRow[6]), DateTime.Parse(curRow[7].ToString()), curRow[8].ToString(), (int)NestDLL.Utils.ParseToDouble(curRow[9]), NestDLL.Utils.ParseToDouble(curRow[10]));
                        //if (this.Text == "Main List")
                        {
                            curItem.PosMH_Trad = (int)NestDLL.Utils.ParseToDouble(curRow["MHTRAD_Pos"]);
                            curItem.PosMH_LS = (int)NestDLL.Utils.ParseToDouble(curRow["MHLS_Pos"]);
                            curItem.PosFIA_Trad = (int)NestDLL.Utils.ParseToDouble(curRow["FIATRAD_Pos"]);
                            curItem.PosFIA_SCaps = (int)NestDLL.Utils.ParseToDouble(curRow["FIALS_Pos"]);
                        }
                    }
                }
            }

            bndDataSource.DataSource = SubscribedData;
            dtgWatchList.DataSource = bndDataSource;
            
            dgWatchList.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Change"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["PCTAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["PCTAuction"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["CHGPrevAuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["CHGPrevAuc"].DisplayFormat.FormatString = "P2";
            
            dgWatchList.Columns["ABSAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["ABSAuction"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["AucVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["AucVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgWatchList.Columns["Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["AucLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["AucLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["Bid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Bid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["Ask"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Ask"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["Spread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Spread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgWatchList.Columns["AvgSpread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["AvgSpread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";
            
            dgWatchList.Columns["FinVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["FinVolume"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["Volume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Volume"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgWatchList.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgWatchList.Columns["Expiration"].DisplayFormat.FormatString = "dd-MMM-yy";

            dgWatchList.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Strike"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgWatchList.Columns["AucGain"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["AucGain"].DisplayFormat.FormatString = "#,##0.;(#,##0.);\\ ";

            dgWatchList.Columns["Low"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["Low"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["High"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["High"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["FromLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["FromLow"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["FromHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["FromHigh"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["DateLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["DateLow"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["DateHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["DateHigh"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["FromDateLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["FromDateLow"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["FromDateHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["FromDateHigh"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["FromVWAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["FromVWAP"].DisplayFormat.FormatString = "P2";

            dgWatchList.Columns["AvVolume6m"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgWatchList.Columns["AvVolume6m"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgWatchList.Columns["AucTimeLeft"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgWatchList.Columns["AucTimeLeft"].DisplayFormat.FormatString = "HH:mm:ss";

            dgWatchList.Columns["AucCloseTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgWatchList.Columns["AucCloseTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgWatchList.Columns["LastTradeTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgWatchList.Columns["LastTradeTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgWatchList.Columns["LastUpdTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgWatchList.Columns["LastUpdTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgWatchList.Columns["Ticker"].VisibleIndex = 0;
            dgWatchList.Columns["Sector"].VisibleIndex = 1;
            dgWatchList.Columns["Close"].VisibleIndex = 2;
            dgWatchList.Columns["Last"].VisibleIndex = 3;
            dgWatchList.Columns["AucLast"].VisibleIndex = 4;
            dgWatchList.Columns["Change"].VisibleIndex = 5;
            dgWatchList.Columns["PCTAuction"].VisibleIndex = 6;
            dgWatchList.Columns["AucVolume"].VisibleIndex = 7;
            dgWatchList.Columns["AucGain"].VisibleIndex = 8;
            //dgQuotes.Columns["AucCond"].VisibleIndex = 9;
            dgWatchList.Columns["FinVolume"].VisibleIndex = 10;

            //dgWatchList.GroupSummary.Add(SummaryItemType.Average, "Change", dgWatchList.Columns["Change"], "{0:0.00%}");
            //dgWatchList.GroupSummary.Add(SummaryItemType.Average, "FromLow", dgWatchList.Columns["Change"], "{0:0.00%}");
            //dgWatchList.GroupSummary.Add(SummaryItemType.Average, "FromHigh", dgWatchList.Columns["Change"], "{0:0.00%}");
            //dgWatchList.GroupSummary.Add(SummaryItemType.Average, "FinVolume", dgWatchList.Columns["FinVolume"], "{0:0.00}");
            //dgWatchList.GroupSummary.Add(SummaryItemType.Average, "FromDateHigh", dgWatchList.Columns["FromDateHigh"], "{0:0.00%}");
            //dgWatchList.GroupSummary.Add(SummaryItemType.Average, "FromDateLow", dgWatchList.Columns["FromDateLow"], "{0:0.00%}");
            SortOrderGroupItem = dgWatchList.GroupSummary.Add(SummaryItemType.Min, "SortOrder", dgWatchList.Columns["SortOrder"], "{0:0}");

            dgWatchList.GroupSummary.Add(SummaryItemType.Sum, "CloseToDateHigh", dgWatchList.Columns["CloseToDateHigh"], "{0:0; -0; -}");
            dgWatchList.GroupSummary.Add(SummaryItemType.Sum, "CloseToDateLow", dgWatchList.Columns["CloseToDateLow"], "{0:0; -0; -}");

            curUtils.LoadGridColumns(dgWatchList, this.Text);

            if (OpenMode != 4)
            {
                
            }

            if (OpenMode == 5)
            {
                gbBrazilAll.Visible = true;
                gbOffshore.Visible = true;
                FilterUniqueList();
                chkLiquid.Checked = true;
                chkLiquid.Visible = true;
                chkAuction.Visible = true;
                chkMH_Trad.Visible = true;
                chkMH_LS.Visible = true;
                chkFIA_Trad.Visible = true;
                chkFIA_LS.Visible = true;
            }
            

            switch (OpenMode)
            {
                case 1: // Normal
                case 5: // Unique List                    
                case 4: // Auction
                    dgWatchList.Columns["Expiration"].Visible = false;
                    dgWatchList.Columns["Strike"].Visible = false;

                    dgWatchList.Columns["OptType"].Visible = false;
                    dgWatchList.Columns["IdTicker"].Visible = false;
                    dgWatchList.Columns["IdInstrument"].Visible = false;
                    dgWatchList.Columns["Currency"].Visible = false;
                    dgWatchList.Columns["BidSize"].Visible = false;
                    dgWatchList.Columns["AskSize"].Visible = false;
                    dgWatchList.Columns["Exchange"].Visible = false;
                    dgWatchList.Columns["Volume"].Visible = false;
                    dgWatchList.Columns["Spread"].Visible = false;
                    dgWatchList.Columns["AvgSpread"].Visible = false;
                    dgWatchList.Columns["PriceMult"].Visible = false;
                    dgWatchList.Columns["High"].Visible = false;
                    dgWatchList.Columns["Low"].Visible = false;
                    dgWatchList.Columns["LastSize"].Visible = false;
                    dgWatchList.Columns["VWAP"].Visible = false;
                    dgWatchList.Columns["Open"].Visible = false;
                    dgWatchList.Columns["QuotedAsRate"].Visible = false;
                    dgWatchList.Columns["ExchangeTradingCode"].Visible = false;
                    dgWatchList.Columns["ExchangeSymbol"].Visible = false;
                    dgWatchList.Columns["IndexContrib"].Visible = false;
                    dgWatchList.Columns["IndexQuantity"].Visible = false;

                    
                    break;
                case 2: // Option Chains
                    dgWatchList.Columns["Expiration"].Visible = true;
                    dgWatchList.Columns["Strike"].Visible = true;
                    dgWatchList.Columns["Sector"].Visible = false;
                    dgWatchList.Columns["AvgSpread"].Visible = false;
                    dgWatchList.Columns["Expiration"].GroupIndex = 1;
                    dgWatchList.Columns["OptType"].GroupIndex = 2;
                    dgWatchList.Columns["Strike"].SortOrder = ColumnSortOrder.Ascending;
                    dgWatchList.ExpandAllGroups();

                    dgWatchList.Columns["IdTicker"].Visible = false;
                    dgWatchList.Columns["IdInstrument"].Visible = false;
                    dgWatchList.Columns["Currency"].Visible = false;
                    dgWatchList.Columns["BidSize"].Visible = false;
                    dgWatchList.Columns["AskSize"].Visible = false;
                    dgWatchList.Columns["Exchange"].Visible = false;
                    dgWatchList.Columns["PriceMult"].Visible = false;
                    break;
                case 3: // Futures
                    dgWatchList.Columns["Expiration"].Visible = true;
                    dgWatchList.Columns["Strike"].Visible = false;
                    dgWatchList.Columns["AvgSpread"].Visible = false;
                    dgWatchList.Columns["Sector"].GroupIndex = 1;
                    dgWatchList.Columns["Expiration"].SortOrder = ColumnSortOrder.Ascending;
                    dgWatchList.ExpandAllGroups();

                    dgWatchList.Columns["OptType"].Visible = false;
                    dgWatchList.Columns["IdTicker"].Visible = false;
                    dgWatchList.Columns["IdInstrument"].Visible = false;
                    dgWatchList.Columns["Currency"].Visible = false;
                    dgWatchList.Columns["BidSize"].Visible = false;
                    dgWatchList.Columns["AskSize"].Visible = false;
                    dgWatchList.Columns["Exchange"].Visible = false;
                    dgWatchList.Columns["Volume"].Visible = false;
                    dgWatchList.Columns["FinVolume"].Visible = false;
                    dgWatchList.Columns["Spread"].Visible = false;
                    dgWatchList.Columns["AvgSpread"].Visible = false;
                    dgWatchList.Columns["PriceMult"].Visible = false;
                    break;
                default:
                    break;
            }           
            
            
            chkAuction.Checked = false;
            dgWatchList.Columns["Ticker"].Width = 110;


            dgWatchList.Columns["Ticker"].Fixed = FixedStyle.Left;

            dgWatchList.ExpandAllGroups();

            frmWatchList_Resize(this, new EventArgs());

            //LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);

            UncheckRadios();

            SubscribeTickers();
            LargeGrid();

            dgWatchList.ExpandAllGroups();

            timer1.Start();
        }

        public void ReLoadColumns()
        {
            curUtils.LoadGridColumns(dgWatchList, this.Text);
            dgWatchList.ExpandAllGroups();
        }

        private void SubscribeTickers()
        {
            foreach (LTMarketDataItem curItem in SubscribedData)
            {
                curNDistConn.Subscribe(curItem.ExchangeTradingCode, GlobalVars.Instance.getDataSource(curItem.ExchangeTradingCode));
            }
        }

        private LTMarketDataItem CreateQuote(string Ticker, int IdPrimaryExchange, string Sector, string Currency, int IdTicker, int IdInstrument, double Strike, DateTime Expiration, string Category, int SortOrder, double RoundLot)
        {

            LTMarketDataItem curItem = new LTMarketDataItem();
            curItem.IdTicker = IdTicker;
            curItem.Exchange = IdPrimaryExchange;
            curItem.Ticker = Ticker;

            if (IdPrimaryExchange != 2)
                curItem.ExchangeTradingCode = Ticker;
            else
                curItem.ExchangeTradingCode = Ticker;

            if (curItem.ExchangeTradingCode == "") curItem.ExchangeTradingCode = IdTicker.ToString();

            curItem.IdInstrument = IdInstrument;
            curItem.Sector = Sector;
            curItem.Currency = Currency;
            curItem.Strike = Strike;
            curItem.Expiration = Expiration;
            curItem.Category = Category;
            curItem.SortOrder = SortOrder;
            SubscribedData.Add(curItem);

            if (RoundLot > 0) curItem.PriceMult = 1 / RoundLot;

            UpdateListIndex();
            return curItem;
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                try
                {
                    SubListIndex.Add(SubscribedData[i].ExchangeTradingCode, i);
                }
                catch
                {
                    SubscribedData[i].ExchangeTradingCode = "ZZZERROR_" + SubscribedData[i].ExchangeTradingCode;
                    SubListIndex.Add(SubscribedData[i].ExchangeTradingCode, i);
                }
            }
        }

        private void ClearEmpty()
        {
            dtgWatchList.DataSource = null;
            for (int i = SubscribedData.Count-1; i > 0; i--)
			{
                LTMarketDataItem curItem = SubscribedData[i];
                if (curItem.Last == 0 && curItem.Bid == 0 && curItem.Ask == 0 && curItem.Volume == 0)
                {
                    //SubscribedData.Remove(curItem);
                }
			}
            UpdateListIndex();
            dtgWatchList.DataSource = bndDataSource;
            dgWatchList.ExpandAllGroups();
        }

        private void chkAuction_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuction.Checked)
            {
                dgWatchList.Columns["AucLast"].Visible = true;
                dgWatchList.Columns["PCTAuction"].Visible = true;
                dgWatchList.Columns["AucVolume"].Visible = true;
                dgWatchList.Columns["AucGain"].Visible = true;
                dgWatchList.Columns["Last"].Visible = true;
                dgWatchList.Columns["AucCond"].Visible = true;
                dgWatchList.Columns["AucCloseTime"].Visible = true;
                dgWatchList.Columns["AucTimeLeft"].Visible = true;

                dgWatchList.Columns["ABSAuction"].Visible = true;
                dgWatchList.Columns["CHGPrevAuc"].Visible = false;
                
                dgWatchList.Columns["Bid"].Visible = false;
                dgWatchList.Columns["Ask"].Visible = false;
                dgWatchList.Columns["Spread"].Visible = false;
                dgWatchList.Columns["AvgSpread"].Visible = false;

                dgWatchList.Columns["ABSAuction"].SortOrder = ColumnSortOrder.Descending;
                dgWatchList.Columns["AucCond"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[AucCond] = 'G_PREOPEN' OR [AucCond] = 'AUCTION' OR [AucCond] = 'SUSP' OR [AucCond] = 'FROZEN'");
            }
            else
            {
                curUtils.LoadGridColumns(dgWatchList, this.Text);

                dgWatchList.Columns["AucLast"].Visible = false;
                dgWatchList.Columns["PCTAuction"].Visible = false;
                dgWatchList.Columns["ABSAuction"].Visible = false;
                dgWatchList.Columns["CHGPrevAuc"].Visible = false;
                dgWatchList.Columns["AucVolume"].Visible = false;
                dgWatchList.Columns["AucCond"].Visible = false;
                dgWatchList.Columns["AucGain"].Visible = false;

                dgWatchList.Columns["AucCloseTime"].Visible = false;
                dgWatchList.Columns["AucTimeLeft"].Visible = false;

                dgWatchList.Columns["AucCond"].ClearFilter();
            }
        }

        private void chkLiquid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiquid.Checked)
            {
                dgWatchList.Columns["AvVolume6m"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[AvVolume6m] > 0.1 OR [FinVolume] > 0.5");
            }
            else
            {
                dgWatchList.Columns["AvVolume6m"].ClearFilter();
                dgWatchList.Columns["FinVolume"].ClearFilter();
            }
        }

        private void RefreshGrid()
        {
            //dgWatchList.LayoutChanged();
            //dgWatchList.UpdateGroupSummary();
            dgWatchList.RefreshData();
        }
        
        private void NewMarketData(object sender, EventArgs origE)
        {
            int i = 0;
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                if (SubListIndex.TryGetValue(curUpdateItem.Ticker, out i))
                {
                    LTMarketDataItem curItem = (LTMarketDataItem)SubscribedData[i];
                    curItem.Update(curUpdateItem);
                }
            }
        }

        bool LoadingDates = false;

        private void LoadDatesHighLow(DateTime iniDate, DateTime endDate)
        {
            if (!LoadingDates)
            {
                LoadingDates = true;
                LiveTrade2.Properties.Settings.Default.IniDate = iniDate;
                LiveTrade2.Properties.Settings.Default.EndDate = endDate;
                LiveTrade2.Properties.Settings.Default.Save();

                lblHL_IniDate.Text = iniDate.ToString("dd-MMM-yyyy");
                lblHL_EndDate.Text = endDate.ToString("dd-MMM-yyyy");

                System.Threading.Thread DatesThread = new Thread(LoadDateValues);
                DatesThread.SetApartmentState(ApartmentState.STA);
                DatesThread.Start();
            }
        }

        private void LoadDateValues()
        {
            DateTime iniDate = LiveTrade2.Properties.Settings.Default.IniDate;
            DateTime endDate = LiveTrade2.Properties.Settings.Default.EndDate;

            using (newNestConn curConn = new newNestConn())
            {
                foreach (LTMarketDataItem curDataItem in SubscribedData)
                {
                    if (curDataItem.Last != 0)
                    {
                        if (curDataItem.IdTicker == 852)
                        { 
                        }

                        double LastTRIndex = curConn.Return_Double("SELECT SrValue FROM " + NestDLL.Utils.GetTableName(curDataItem.IdTicker) + " WHERE IdSecurity=" + curDataItem.IdTicker + " AND SrType=101 AND SrDate=dbo.FCN_NDATEADD('du',-1,CONVERT(varchar,getdate(),112),1,1)");
                        double LastPX = curConn.Return_Double("SELECT SrValue FROM " + NestDLL.Utils.GetTableName(curDataItem.IdTicker) + " WHERE IdSecurity=" + curDataItem.IdTicker + " AND SrType=1 AND SrDate=dbo.FCN_NDATEADD('du',-1,CONVERT(varchar,getdate(),112),1,1)");

                        if (!double.IsNaN(LastTRIndex))
                        {
                            double MaxTRIndex = curConn.Return_Double("SELECT MAX(SrValue) AS MaxVal FROM " + NestDLL.Utils.GetTableName(curDataItem.IdTicker) + " WHERE IdSecurity=" + curDataItem.IdTicker + " AND SrType=101 AND SrDate>='" + iniDate.ToString("yyyy-MM-dd") + "' AND SrDate<='" + endDate.ToString("yyyy-MM-dd") + "'");
                            curDataItem.DateHigh = MaxTRIndex / LastTRIndex * LastPX;

                            double MinTRIndex = curConn.Return_Double("SELECT MIN(SrValue) AS MaxVal FROM " + NestDLL.Utils.GetTableName(curDataItem.IdTicker) + " WHERE IdSecurity=" + curDataItem.IdTicker + " AND SrType=101 AND SrDate>='" + iniDate.ToString("yyyy-MM-dd") + "' AND SrDate<='" + endDate.ToString("yyyy-MM-dd") + "'");
                            curDataItem.DateLow = MinTRIndex / LastTRIndex * LastPX;
                        }
                        else
                        {
                            if (curDataItem.Currency == "USD")
                            {
                                double MaxLast = curConn.Return_Double("SELECT MAX(SrValue) AS MaxVal FROM " + NestDLL.Utils.GetTableName(curDataItem.IdTicker) + " WHERE IdSecurity=" + curDataItem.IdTicker + " AND SrType=1 AND SrDate>='" + iniDate.ToString("yyyy-MM-dd") + "' AND SrDate<='" + endDate.ToString("yyyy-MM-dd") + "'");
                                if (LastPX > 0)
                                {
                                    curDataItem.DateHigh = MaxLast / LastPX;

                                    double MinLast = curConn.Return_Double("SELECT MIN(SrValue) AS MaxVal FROM " + NestDLL.Utils.GetTableName(curDataItem.IdTicker) + " WHERE IdSecurity=" + curDataItem.IdTicker + " AND SrType=1 AND SrDate>='" + iniDate.ToString("yyyy-MM-dd") + "' AND SrDate<='" + endDate.ToString("yyyy-MM-dd") + "'");
                                    curDataItem.DateLow = MinLast / LastPX;
                                }
                            }
                        }
                    }
                }
            }
            LoadingDates = false;
        }

        double ParseToDouble(object strValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return 0;
            }
            else
            {
                return double.Parse(strValue.ToString());
            }
        }

        private void LoadGrid()
        { 

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WaitCounter == 0)
            {
                if (!flgMouseDown) RefreshGrid();
                if (clearCounter == 30)
                {
                    ClearEmpty();
                }
                if (clearCounter < GridMinTop) clearCounter++;
            }
            else
            {
                WaitCounter--;
            }

        }

        private void frmWatchList_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MarketData.Dispose();
        }

        private void dtgQuotes_MouseDown(object sender, MouseEventArgs e)
        {
            flgMouseDown = true;
        }

        private void dtgQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            flgMouseDown = false;
        }

        private void dgQuotes_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Color upColor;

            upColor = Color.Lime;

            if (e.Appearance.BackColor != Color.Black)
            {
                upColor = Color.Green;
            }

            if (e.Column.Name == "colTicker")
            {
                //if (e.DisplayText.Substring(0, 1) == && e.DisplayText.Length == 6) e.DisplayText = e.DisplayText.Substring(1);
            }

            if (e.Column.Name == "colChange" || e.Column.Name == "colPCT_Auction")
            {
                if (Convert.ToSingle(e.CellValue) > 0.005 && e.Appearance.ForeColor != upColor)
                {
                    e.Appearance.ForeColor = upColor;
                    if (double.Parse(e.CellValue.ToString()) > 0.01)
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
                else if (Convert.ToSingle(e.CellValue) < -0.005 && e.Appearance.ForeColor != Color.Red)
                {
                    e.Appearance.ForeColor = Color.Red;
                    if (double.Parse(e.CellValue.ToString()) < -0.01)
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                }
                else if (Convert.ToSingle(e.CellValue) == 0 && e.Appearance.ForeColor != Color.Black)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }

                
                int IdInstrument = 0;
                NCustomControls.MyGridView curView = (NCustomControls.MyGridView)sender;
                
                IdInstrument = int.Parse(curView.GetRowCellValue(e.RowHandle, "IdInstrument").ToString());

                if (IdInstrument == 16)
                {
                    double chgValue = double.Parse(e.DisplayText.Replace("%",""));
                    e.DisplayText = chgValue.ToString("0.00");
                }
            }

            if (e.Column.Name == "colABSAuction")
            {
                object curSelRow = dgWatchList.GetRowCellValue(e.RowHandle, dgWatchList.Columns["PCTAuction"]);
                    
                if (curSelRow != null)
                {
                    double refValue = double.Parse(curSelRow.ToString());

                    if (refValue < 0 && e.DisplayText.Substring(0,1) != "-") e.DisplayText = "-" + e.DisplayText;

                    if (refValue > 0 && e.Appearance.ForeColor != upColor)
                    {
                        e.Appearance.ForeColor = upColor;
                        if (refValue > 0.01)
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }
                    }
                    else if (refValue < 0 && e.Appearance.ForeColor != Color.Red)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        if (refValue < -0.01)
                        {
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }
                    }
                    else if (refValue == 0 && e.Appearance.ForeColor != dgWatchList.Appearance.Row.ForeColor)
                    {
                        e.Appearance.ForeColor = dgWatchList.Appearance.Row.ForeColor;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                    }


                    int IdInstrument = 0;
                    NCustomControls.MyGridView curView = (NCustomControls.MyGridView)sender;

                    IdInstrument = int.Parse(curView.GetRowCellValue(e.RowHandle, "IdInstrument").ToString());

                    if (IdInstrument == 16)
                    {
                        double chgValue = double.Parse(e.DisplayText.Replace("%", ""));
                        e.DisplayText = chgValue.ToString("0.00");
                    }
                }
            }
        }

        private void dgQuotes_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            //info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]).ToUpper();
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;

        }

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void CopyAllItems()
        {
            string CopyString = "";
            for (int i = 0; i < SubscribedData.Count; i++)
            {
                LTMarketDataItem curItem = SubscribedData[i];
                CopyString = CopyString + curItem.Ticker + '\t';
                CopyString = CopyString + curItem.Strike + '\t';
                CopyString = CopyString + curItem.Expiration.ToString("yyyy-MM-dd") + '\t';
                CopyString = CopyString + curItem.Last.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Change.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Bid.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Ask.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Spread.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.AvgSpread.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Volume.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.FinVolume.ToString("0.00") + '\t';
                CopyString = CopyString + "\r\n";
            }

            Clipboard.SetDataObject(CopyString, true);

        }

        private void dtgQuotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.L && e.Control == true)
            {
                CopyAllItems();
            }
        }

        private void dgQuotes_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (!dgWatchList.IsGroupRow(e.RowHandle))
            {
                string tempStatus = dgWatchList.GetRowCellValue(e.RowHandle, "TradingStatus").ToString();
                if (!chkAuction.Checked)
                {
                    if (tempStatus == "FROZEN" || tempStatus == "SUSPEND" || tempStatus == "AUCTION" || tempStatus == "G_PREOPEN")
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (tempStatus != "FROZEN" && tempStatus != "SUSPEND" && tempStatus != "AUCTION" || tempStatus == "G_PREOPEN")
                    {
                        e.Appearance.BackColor = Color.LightGray;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void dgQuotes_KeyDown(object sender, KeyEventArgs e)
        {
            WaitCounter = 1000/timer1.Interval;
        }

        private void dgQuotes_EndSorting(object sender, EventArgs e)
        {
            if (SelectFirstonNextSort)
            {
                dgWatchList.FocusedRowHandle = 0;
                SelectFirstonNextSort = false;
            }
        }
        
        private GridSummaryItem GetGroupByName(GridView view, string Groupname)
        {
            foreach (GridSummaryItem curItem in view.GroupSummary)
            {
                if (curItem.FieldName == Groupname)
                {
                    return curItem;
                }
            }
            return null;
        }

        private void dtpHL_IniDate_KeyUp(object sender, KeyEventArgs e)
        {            
            dtpHL_EndDate.Value = dtpHL_IniDate.Value;
            if (e.KeyCode == Keys.Enter)
            {
                LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
                UncheckRadios();
            }
        }

        private void dtpHL_EndDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
                UncheckRadios();
            }
        }

        private void dtgQuotes_Click(object sender, EventArgs e)
        {

        }
                
        GridHitInfo downHitInfo = null;
        
        private void SmallGrid()
        {
            if (dtgWatchList.Top < GridMaxTop)
            {
                dtgWatchList.Height = this.Height - GridMaxTop - 30;
                dtgWatchList.Top = GridMaxTop;
            }
        }

        private void LargeGrid()
        {
            if (dtgWatchList.Top > GridMinTop)
            {
                dtgWatchList.Height = this.Height - GridMinTop - 30;
                dtgWatchList.Top = GridMinTop;
            }
        }

        private void dgQuotes_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void dgQuotes_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                downHitInfo = hitInfo;

        }

        private void dtgQuotes_DragOver(object sender, DragEventArgs e)
        {
            string DroppedIdTicker = (string)e.Data.GetData(typeof(string));
            if (DroppedIdTicker != null)
            {
                if (DroppedIdTicker.Split('\t')[0] == "DRAGITEM")
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.None;
            }
            else
                e.Effect = DragDropEffects.None;

        }
                
        private void frmWatchList_Resize(object sender, EventArgs e)
        {
            if (dgWatchList.Columns.Count > 0)
            {
                curUtils.LoadGridColumns(dgWatchList, this.Text);

                foreach (GridColumn curColumn in dgWatchList.Columns)
                {
                    switch (curColumn.Name)
                    {
                        //case "colTicker": if (this.Width > 0 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colLast": if (this.Width > 0 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colChange": if (this.Width > 250 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colBid": if (this.Width > 250 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colAsk": if (this.Width > 250 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colBidSize": if (this.Width > 350 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colAskSize": if (this.Width > 350 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        //case "colFinVolume": if (this.Width > 350 && curColumn.Visible == true) { curColumn.Visible = true; } else { curColumn.Visible = false; }; break;
                        
                        default:
                            //if (this.Width < 600) { curColumn.Visible = false; };
                            break;
                    }
                }
            }
        }

        #region  DatesDropDown
    
        bool EndDate_Editing = false;

        private void lblHL_EndDate_MouseEnter(object sender, EventArgs e)
        {
            dtpHL_EndDate.Visible = true;
            lblHL_EndDate.Visible = false;
        }

        private void dtpHL_EndDate_MouseLeave(object sender, EventArgs e)
        {
            if (!EndDate_Editing)
            {
                dtpHL_EndDate.Visible = false;
                lblHL_EndDate.Visible = true;
            }
        }

        private void dtpHL_EndDate_DropDown(object sender, EventArgs e)
        {
            EndDate_Editing = true;
        }

        private void dtpHL_EndDate_CloseUp(object sender, EventArgs e)
        {
            EndDate_Editing = false;
            dtpHL_EndDate.Visible = false;
            lblHL_EndDate.Visible = true;
            LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
            UncheckRadios();
        }

        bool IniDate_Editing = false;

        private void lblHL_IniDate_MouseEnter(object sender, EventArgs e)
        {
            dtpHL_IniDate.Visible = true;
            lblHL_IniDate.Visible = false;
        }
        
        private void dtpHL_IniDate_MouseLeave(object sender, EventArgs e)
        {
            if (!IniDate_Editing)
            {
                dtpHL_IniDate.Visible = false;
                lblHL_IniDate.Visible = true;
            }
        }

        private void dtpHL_IniDate_DropDown(object sender, EventArgs e)
        {
            IniDate_Editing = true;
        }

        private void dtpHL_IniDate_CloseUp(object sender, EventArgs e)
        {
            IniDate_Editing = false;
            dtpHL_IniDate.Visible = false;
            lblHL_IniDate.Visible = true;
            dtpHL_EndDate.Value = dtpHL_IniDate.Value;
            LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
            UncheckRadios();
        }

        private void radYTD_CheckedChanged(object sender, EventArgs e)
        {
            if (radYTD.Checked == true)
            {
                dtpHL_EndDate.Value = DateTime.Now.Date;
                dtpHL_IniDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
                LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
            }
        }


        private void radMTD_CheckedChanged(object sender, EventArgs e)
        {
            if (radMTD.Checked == true)
            {
                dtpHL_EndDate.Value = DateTime.Now.Date;
                dtpHL_IniDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
            }
        }

        private void rad12m_CheckedChanged(object sender, EventArgs e)
        {
            if (rad12m.Checked == true)
            {
                dtpHL_EndDate.Value = DateTime.Now.Date;
                dtpHL_IniDate.Value = new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day);
                LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
            }
        }

        private void rad2008Low_CheckedChanged(object sender, EventArgs e)
        {
            if (rad2008Low.Checked == true)
            {
                dtpHL_EndDate.Value = DateTime.Now.Date;
                dtpHL_IniDate.Value = new DateTime(2008, 10, 24);
                LoadDatesHighLow(dtpHL_IniDate.Value, dtpHL_EndDate.Value);
            }
        }

        private void UncheckRadios()
        {
            rad12m.Checked = false;
            radYTD.Checked = false;
            radMTD.Checked = false;
            rad2008Low.Checked = false;
        }

     #endregion

        #region FilterCheckBoxes

        private void FilterUniqueList()
        {
            string filter = "";
            string or = "";

            if (chkBovespa.Checked == true)
            {
                filter = filter + or + "[Category] = 'Bovespa'";
                or = " or ";
            }
            if (chkBzLiquid.Checked == true)
            {
                filter = filter + or + "[Category] = 'BZ LIQUID'";
                or = " or ";
            }
            if (chkBzIliquid.Checked == true)
            {
                filter = filter + or + "[Category] = 'BZ ILIQUID'";
                or = " or ";
            }
            if (chkUsCommon.Checked == true)
            {
                filter = filter + or + "[Category] = 'US COMMON'";
                or = " or ";
            }
            if (chkUsETFs.Checked == true)
            {
                filter = filter + or + "[Category] = 'US ETFS'";
                or = " or ";
            }
            if (chkADRs.Checked == true)
            {
                filter = filter + or + "[Category] = 'ADRS'";
                or = " or ";
            }
            if (chkWarrant.Checked == true)
            {
                filter = filter + or + "[Category] = 'WARRANT'";
                or = " or ";  
            }
            if (chkReceipt.Checked == true)
            {
                filter = filter + or + "[Category] = 'RECEIPT'";
                or = " or ";
            }            

            dgWatchList.Columns["Category"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, filter);
        }

        private void chkBovespa_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkBzLiquid_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkBzIliquid_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkUsCommon_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkUsETFs_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkADRs_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkWarrant_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }

        private void chkReceipt_CheckedChanged(object sender, EventArgs e)
        {
            FilterUniqueList();
        }   

        #endregion

        private void dgQuotes_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.White;
            e.LevelAppearance.ForeColor = Color.Black;
            e.LevelAppearance.Font = new Font(dgWatchList.Appearance.Row.Font, FontStyle.Bold); ;
        }

        private void chkMH_Trad_CheckedChanged(object sender, EventArgs e)
        {
            UpdateposFilters();
        }

        private void chkMH_LS_CheckedChanged(object sender, EventArgs e)
        {
            UpdateposFilters();
        }

        private void chkFIA_Trad_CheckedChanged(object sender, EventArgs e)
        {
            UpdateposFilters();
        }

        private void chkFIA_LS_CheckedChanged(object sender, EventArgs e)
        {
            UpdateposFilters();
        }

        private void UpdateposFilters()
        {
            dgWatchList.Columns["PosFIA_LS"].ClearFilter();

            string tempfilter = "(";
            if (chkFIA_LS.Checked) tempfilter = tempfilter + " OR [PosFIA_LS] <> 0";
            if (chkFIA_Trad.Checked) tempfilter = tempfilter + " OR [PosFIA_Trad] <> 0";
            if (chkMH_LS.Checked) tempfilter = tempfilter + " OR [PosMH_LS] <> 0";
            if (chkMH_Trad.Checked) tempfilter = tempfilter + " OR [PosMH_Trad] <> 0";
            tempfilter = tempfilter + ")";

            tempfilter = tempfilter.Replace("( OR [", "([");

            dgWatchList.Columns["PosFIA_Trad"].ClearFilter();

            if(tempfilter != "()") dgWatchList.Columns["PosFIA_LS"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, tempfilter);

        }

        private void lblOptions_Click(object sender, EventArgs e)
        {
            if (this.lblOptions.Text == "Options")
            {
                SmallGrid();
                lblOptions.Text = "Hide";
            }
            else if (this.lblOptions.Text == "Hide")
            {
                LargeGrid();
                lblOptions.Text = "Options";
            }
        }

        private void dgQuotes_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.SaveGridColumns(dgWatchList, this.Text);
        }

        private void dgWatchList_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.SaveGridColumns(dgWatchList, this.Text);
        }
    }
}