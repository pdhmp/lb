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
    public partial class frmAuction : ConnectedForm
    {
        List<LTMarketDataItem> SubscribedData = new List<LTMarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        BindingSource bndDataSource = new BindingSource();
        bool flgMouseDown = false;
        public string SQLString;

        TradingStatus curStatusList = new TradingStatus();
        Utils curUtils = new Utils(3);
        bool SelectFirstonNextSort = false;
        GridGroupSummaryItem SortOrderGroupItem;

        public frmAuction()
        {
            InitializeComponent();
        }

        ~frmAuction()
        {
            curNDistConn.Disconnect();
        }

        private void frmWatchList_Load(object sender, EventArgs e)
        {
            curNDistConn.OnData += new EventHandler(NewMarketData);

            dtgAucQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgAucQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgAucQuotes.LookAndFeel.SetSkinStyle("Blue");

            dgAucQuotes.ColumnPanelRowHeight = 32;
            dgAucQuotes.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;


            dgAucQuotes.Appearance.Row.ForeColor = GlobalVars.Instance.GridForeColor;
            dgAucQuotes.Appearance.Row.BackColor = GlobalVars.Instance.GridBackColor;
            dgAucQuotes.OptionsView.ShowHorzLines = false;
            dgAucQuotes.OptionsView.ShowVertLines = false;
            dgAucQuotes.Appearance.Empty.BackColor = GlobalVars.Instance.GridBackColor;
            dgAucQuotes.Appearance.FocusedRow.BackColor = Color.DarkGray;
            dgAucQuotes.Appearance.SelectedRow.BackColor = Color.DarkGray;


            SQLString = "SELECT C.NestTicker AS Ticker, C.IdPrimaryExchange, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument,        " +
                       "COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), A.Category, 0 AS SortOrder, MHTRAD_Pos, MHLS_Pos, FIATRAD_Pos, FIALS_Pos, RoundLot           " +
                       "FROM(                                                                                                                                             " +
                       "	SELECT ID_ATIVO as IdSecurity, CATEGORY                                                                                                        " +
                       "	FROM                                                                                                                                           " +
                       "	(                                                                                                                                              " +
                       "		SELECT ID_ATIVO, CATEGORY, ORD,                                                                                                            " +
                       "			   RANK() OVER(PARTITION BY ID_ATIVO                                                                                                   " +
                       "						   ORDER BY ORD) AS 'RANK'                                                                                                 " +
                       "		FROM                                                                                                                                       " +
                       "		(                                                                                                                                          " +
                       "			SELECT ID_TICKER_COMPONENT AS ID_ATIVO, 'BOVESPA' AS Category, 1 as ord                                                                " +
                       "				FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION                                                                                       " +
                       "				WHERE ID_TICKER_COMPOSITE = 1073                                                                                                   " +
                       "				AND DATE_REF = (                                                                                                                   " +
                       "									SELECT MAX(DATE_REF)                                                                                           " +
                       "									FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION                                                                   " +
                       "									WHERE ID_TICKER_COMPOSITE = 1073                                                                               " +
                       "								)                                                                                                                  " +
                       "			UNION ALL                                                                                                                              " +
                       "			SELECT ID_TICKER_COMPONENT AS ID_ATIVO, 'BZ LIQUID' AS Category, 2 AS ord FROM                                                         " +
                       "			(SELECT B.ID_TICKER_COMPONENT, B.WEIGHT,                                                                                               " +
                       "				   RANK() OVER (PARTITION BY A.ID_TICKER_COMPONENT, B.ID_TICKER_COMPONENT                                                          " +
                       "								ORDER BY A.DATE_REF DESC, B.DATE_REF DESC) AS 'RANK'                                                               " +
                       "			FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION A,                                                                                        " +
                       "				 NESTDB.DBO.TB023_SECURITIES_COMPOSITION B                                                                                         " +
                       "			WHERE A.ID_TICKER_COMPOSITE = 21350                                                                                                    " +
                       "			AND A.ID_TICKER_COMPONENT = B.ID_TICKER_COMPOSITE) X                                                                                   " +
                       "			WHERE RANK = 1                                                                                                                         " +
                       "			AND WEIGHT <> 0                                                                                                                        " +
                       "            UNION ALL                                                                                                                             " +
                       "            SELECT IDSECURITY, 'BZ ILIQUID' AS CATEGORY, 3 AS ORD                                                                                 " +
                       "			FROM NESTDB.DBO.TB001_SECURITIES                                                                                                       " +
                       "			WHERE (LASTTRADEDATE = '19000101' OR LASTTRADEDATE IS NULL)                                                                            " +
                       "			AND IDINSTRUMENT IN (2) AND IdCurrency=900                                                                                             " +
                       "            UNION ALL                                                                                                                             " +
                       "            (SELECT IdSecurity, 'US COMMON' AS CATEGORY, 4 AS ORD FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument IN(2) AND IdCurrency=1042   " +
                       "            UNION SELECT IdSecurity, 'US COMMON' AS CATEGORY, 4 AS ORD FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=1073)                    " +
                       "            UNION ALL                                                                                                                             " +
                       "            SELECT IdSecurity, 'US ETFS' AS CATEGORY, 5 AS ORD FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument IN(1) AND IdCurrency=1042      " +
                       "            UNION ALL                                                                                                                             " +
                       "            SELECT IdSecurity, 'ADRS' AS CATEGORY, 6 AS ORD FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument IN(7) AND IdCurrency=1042         " +
                       "            UNION ALL                                                                                                                             " +
                       "            SELECT IdSecurity, 'RECEIPT' AS CATEGORY, 7 AS ORD FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument IN(15) AND                     " +
                       "                                                                                             (Expiration = '19000101' OR Expiration >=getdate())  " +
                       "            UNION ALL                                                                                                                             " +
                       "            SELECT IdSecurity, 'WARRANT' AS CATEGORY, 8 AS ORD FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument IN(26) AND                     " +
                       "                                                                                             (Expiration = '19000101' OR Expiration >=getdate())  " +
                       "		) Y                                                                                                                                        " +
                       "	) Z                                                                                                                                            " +
                       "	WHERE Z.RANK = 1                                                                                                                               " +
                       ") A                                                                                                                                               " +
                       " LEFT JOIN NESTDB.dbo.Tb001_Securities C                                                                                                          " +
                       " ON A.IdSecurity=C.IdSecurity                                                                                                                     " +
                       " LEFT JOIN NESTDB.dbo.Tb000_Issuers D                                                                                                        " +
                       " ON C.IdIssuer=D.IdIssuer                                                                                                                   " +
                       " LEFT JOIN NESTDB.dbo.Tb113_Setores E                                                                                                             " +
                       " ON D.IdNestSector=E.Id_Setor                                                                                                                         " +
                       //" LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS MHTRAD_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=43 AND [Id Book]=2 GROUP BY [id base underlying]) MHTRAD" +
                       //" ON A.IdSecurity=MHTRAD.IdSecurity                                                                                                                   " +
                       //" LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS MHLS_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=43 AND [Id Book]=1 GROUP BY [id base underlying]) MHLS" +
                       //" ON A.IdSecurity=MHLS.IdSecurity                                                                                                                   " +
                       //" LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS FIATRAD_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=10 AND [Id Book]=2 GROUP BY [id base underlying]) FIATRAD" +
                       //" ON A.IdSecurity=FIATRAD.IdSecurity                                                                                                                   " +
                       //" LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS FIALS_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=10 AND [Id Book]=1 GROUP BY [id base underlying]) FIALS" +
                       //" ON A.IdSecurity=FIALS.IdSecurity                                                                                                                   " +
                       " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS MHTRAD_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=43 GROUP BY [id base underlying]) MHTRAD" +
                       " ON A.IdSecurity=MHTRAD.IdSecurity                                                                                                                   " +
                       " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS MHLS_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=43 GROUP BY [id base underlying]) MHLS" +
                       " ON A.IdSecurity=MHLS.IdSecurity                                                                                                                   " +
                       " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS FIATRAD_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=10 GROUP BY [id base underlying]) FIATRAD" +
                       " ON A.IdSecurity=FIATRAD.IdSecurity                                                                                                                   " +
                       " LEFT JOIN (SELECT [id base underlying] AS IdSecurity, CASE WHEN SUM([delta/NAV])>0 THEN 1 WHEN SUM([delta/NAV])<0 THEN -1 ELSE 0 END AS FIALS_Pos FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]=10 GROUP BY [id base underlying]) FIALS" +
                       " ON A.IdSecurity=FIALS.IdSecurity                                                                                                                   " +
                        " WHERE ReutersTicker IS NOT NULL AND ReutersTicker<>''                                                                                            " +
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
            dtgAucQuotes.DataSource = bndDataSource;

            dgAucQuotes.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Change"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["PCTAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["PCTAuction"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["ABSAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["ABSAuction"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["AucVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["AucVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgAucQuotes.Columns["Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["AucLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["AucLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["Bid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Bid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["Ask"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Ask"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["Spread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Spread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgAucQuotes.Columns["AvgSpread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["AvgSpread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgAucQuotes.Columns["FinVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["FinVolume"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["Volume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Volume"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgAucQuotes.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgAucQuotes.Columns["Expiration"].DisplayFormat.FormatString = "dd-MMM-yy";

            dgAucQuotes.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Strike"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgAucQuotes.Columns["AucGain"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["AucGain"].DisplayFormat.FormatString = "#,##0.;(#,##0.);\\ ";

            dgAucQuotes.Columns["Low"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["Low"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["High"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["High"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["FromLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["FromLow"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["FromHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["FromHigh"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["DateLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["DateLow"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["DateHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["DateHigh"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["FromDateLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["FromDateLow"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["FromDateHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["FromDateHigh"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["FromVWAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["FromVWAP"].DisplayFormat.FormatString = "P2";

            dgAucQuotes.Columns["AvVolume6m"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgAucQuotes.Columns["AvVolume6m"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgAucQuotes.Columns["AucTimeLeft"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgAucQuotes.Columns["AucTimeLeft"].DisplayFormat.FormatString = "HH:mm:ss";

            dgAucQuotes.Columns["AucCloseTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgAucQuotes.Columns["AucCloseTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgAucQuotes.Columns["LastTradeTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgAucQuotes.Columns["LastTradeTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgAucQuotes.Columns["LastUpdTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgAucQuotes.Columns["LastUpdTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgAucQuotes.Columns["Ticker"].VisibleIndex = 0;
            dgAucQuotes.Columns["Sector"].VisibleIndex = 1;
            dgAucQuotes.Columns["Close"].VisibleIndex = 2;
            dgAucQuotes.Columns["Last"].VisibleIndex = 3;
            dgAucQuotes.Columns["AucLast"].VisibleIndex = 4;
            dgAucQuotes.Columns["Change"].VisibleIndex = 5;
            dgAucQuotes.Columns["PCTAuction"].VisibleIndex = 6;
            dgAucQuotes.Columns["AucVolume"].VisibleIndex = 7;
            dgAucQuotes.Columns["AucGain"].VisibleIndex = 8;
            dgAucQuotes.Columns["TradingStatus"].VisibleIndex = 9;
            dgAucQuotes.Columns["FinVolume"].VisibleIndex = 10;

            dgAucQuotes.GroupSummary.Add(SummaryItemType.Average, "Change", dgAucQuotes.Columns["Change"], "{0:0.00%}");
            dgAucQuotes.GroupSummary.Add(SummaryItemType.Average, "FromLow", dgAucQuotes.Columns["Change"], "{0:0.00%}");
            dgAucQuotes.GroupSummary.Add(SummaryItemType.Average, "FromHigh", dgAucQuotes.Columns["Change"], "{0:0.00%}");
            dgAucQuotes.GroupSummary.Add(SummaryItemType.Average, "FinVolume", dgAucQuotes.Columns["FinVolume"], "{0:0.00}");
            dgAucQuotes.GroupSummary.Add(SummaryItemType.Average, "FromDateHigh", dgAucQuotes.Columns["FromDateHigh"], "{0:0.00%}");
            dgAucQuotes.GroupSummary.Add(SummaryItemType.Average, "FromDateLow", dgAucQuotes.Columns["FromDateLow"], "{0:0.00%}");
            SortOrderGroupItem = dgAucQuotes.GroupSummary.Add(SummaryItemType.Min, "SortOrder", dgAucQuotes.Columns["SortOrder"], "{0:0}");

            dgAucQuotes.GroupSummary.Add(SummaryItemType.Sum, "CloseToDateHigh", dgAucQuotes.Columns["CloseToDateHigh"], "{0:0; -0; -}");
            dgAucQuotes.GroupSummary.Add(SummaryItemType.Sum, "CloseToDateLow", dgAucQuotes.Columns["CloseToDateLow"], "{0:0; -0; -}");

            foreach (GridColumn curColumn in dgAucQuotes.Columns)
            {
                curColumn.Visible = false;
            }

            dgAucQuotes.Columns["Ticker"].Visible = true;
            dgAucQuotes.Columns["AucLast"].Visible = true;
            dgAucQuotes.Columns["PCTAuction"].Visible = true;
            dgAucQuotes.Columns["AucVolume"].Visible = true;
            dgAucQuotes.Columns["AucGain"].Visible = true;
            dgAucQuotes.Columns["Last"].Visible = true;
            dgAucQuotes.Columns["Change"].Visible = true;
            dgAucQuotes.Columns["TradingStatus"].Visible = true;
            dgAucQuotes.Columns["AucCloseTime"].Visible = true;
            dgAucQuotes.Columns["AucTimeLeft"].Visible = true;

            dgAucQuotes.Columns["ABSAuction"].Visible = true;

            dgAucQuotes.Columns["ABSAuction"].SortOrder = ColumnSortOrder.Descending;

            string FilterText = "([TradingStatus] = 'G_PREOPEN_P' OR [TradingStatus] = 'AUCTION_K' OR [TradingStatus] = 'SUSP_U' OR [TradingStatus] = 'FROZEN_G')";
            if (chkNest.Checked) FilterText += " AND ([PosMH_Trad] <> 0 OR [PosFIA_Trad] <> 0)";
            dgAucQuotes.Columns["TradingStatus"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, FilterText);


            dgAucQuotes.Columns["Ticker"].Width = 110;
            dgAucQuotes.Columns["Ticker"].Fixed = FixedStyle.Left;

            dgAucQuotes.ExpandAllGroups();

            SubscribeTickers();

            dgAucQuotes.ExpandAllGroups();

            timer1.Start();
        }

        private void UpdateFilter()
        {
            string FilterText = "([TradingStatus] = 'G_PREOPEN_P' OR [TradingStatus] = 'AUCTION_K' OR [TradingStatus] = 'SUSP_U' OR [TradingStatus] = 'FROZEN_G')";
            if (chkNest.Checked)
            {
                if (FilterText != "") FilterText += " AND ";
                FilterText += " ([PosMH_Trad] <> 0 OR [PosFIA_Trad] <> 0 OR [PosFIA_SCaps] <> 0)";
            }
            if (chkLiquid.Checked)
            {
                if (FilterText != "") FilterText += " AND ";
                FilterText += " ([FinVolume] > 1)";
            }
            dgAucQuotes.Columns["TradingStatus"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, FilterText);
        }

        public void ReLoadColumns()
        {
            curUtils.LoadGridColumns(dgAucQuotes, this.Text);
            dgAucQuotes.ExpandAllGroups();
        }

        private void SubscribeTickers()
        {
            foreach (LTMarketDataItem curItem in SubscribedData)
            {
                curNDistConn.Subscribe(curItem.Ticker, GlobalVars.Instance.getDataSource(curItem.Ticker));
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
            dtgAucQuotes.DataSource = null;
            for (int i = SubscribedData.Count-1; i > 0; i--)
			{
                LTMarketDataItem curItem = SubscribedData[i];
                if (curItem.Last == 0 && curItem.Bid == 0 && curItem.Ask == 0 && curItem.Volume == 0)
                {
                    //SubscribedData.Remove(curItem);
                }
			}
            UpdateListIndex();
            dtgAucQuotes.DataSource = bndDataSource;
            dgAucQuotes.ExpandAllGroups();

        }

        private void RefreshGrid()
        {
            dgAucQuotes.LayoutChanged();
            dgAucQuotes.UpdateGroupSummary();
            dgAucQuotes.RefreshData();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!flgMouseDown) RefreshGrid();
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

            if (e.Column.Name == "colABSAuction" || e.Column.Name == "colPCTAuction")
            {
                object curSelRow = dgAucQuotes.GetRowCellValue(e.RowHandle, dgAucQuotes.Columns["PCTAuction"]);
                    
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
                    else if (refValue == 0 && e.Appearance.ForeColor != dgAucQuotes.Appearance.Row.ForeColor)
                    {
                        e.Appearance.ForeColor = dgAucQuotes.Appearance.Row.ForeColor;
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

        private void dgQuotes_EndSorting(object sender, EventArgs e)
        {
            if (SelectFirstonNextSort)
            {
                dgAucQuotes.FocusedRowHandle = 0;
                SelectFirstonNextSort = false;
            }
        }

        public void SetSortedColumn(string colName, bool RecurseColumns)
        {
            ColumnSortOrder curOrder = ColumnSortOrder.Descending;
            if (dgAucQuotes.Columns[colName.Replace("col", "")].SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
            foreach (GridColumn curGrouping in dgAucQuotes.GroupedColumns)
            {
                GridSummaryItem curItem = GetGroupByName(dgAucQuotes, colName.Replace("col", ""));
                if (curItem != null)
                    dgAucQuotes.GroupSummarySortInfo.Add(curItem, curOrder, curGrouping);
            }
            dgAucQuotes.ClearSorting();
            dgAucQuotes.Columns[colName.Replace("col", "")].SortOrder = curOrder;
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

        private void dgQuotes_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.White;
            e.LevelAppearance.ForeColor = Color.Black;
            e.LevelAppearance.Font = new Font(dgAucQuotes.Appearance.Row.Font, FontStyle.Bold); ;
        }

        private void dgQuotes_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);

            string a = ((GridView)sender).FocusedColumn.Name;

            if (info.HitTest == GridHitTest.RowCell && ((GridView)sender).FocusedValue.ToString() != "" && ((GridView)sender).FocusedColumn.Name.Contains("Ticker"))
            {
                if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                {
                    string curTicker = dgAucQuotes.FocusedValue.ToString();

                    if (curTicker != "")
                    {
                        frmOptionChain newOptionChain = new frmOptionChain(curTicker);
                        newOptionChain.Show();
                    }
                }
                else
                {
                    string curValue = ((GridView)sender).FocusedValue.ToString();
                    frmLevel2 curFrmLevel2 = new frmLevel2(curValue);
                    curFrmLevel2.Show();
                    //if (curValue.IndexOf('.') == -1) curValue = curValue + ".SA";
                    curFrmLevel2.txtTicker.Text = curValue;
                    curFrmLevel2.cmdRequest_Click(this, new EventArgs());
                    curFrmLevel2.LoadOrders();
                    //e.Cancel = true; 
                }
            }
        }

        private void dgAucQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            //dgAucQuotes.GetDataRow(dgAucQuotes.FocusedRowHandle).ToString();

            dgAucQuotes.UnselectRow(dgAucQuotes.FocusedRowHandle);
            


        }

        private void FilterBoxes_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        GridHitInfo downHitInfo = null;

        private void dgAucQuotes_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    if (downHitInfo.Column != null)
                    {
                        //DataRow row = view.GetDataRow(downHitInfo.RowHandle);
                        string curCol = downHitInfo.Column.Name;
                        string IdTicker = "DRAGITEM\t" + dgAucQuotes.GetRowCellValue(downHitInfo.RowHandle, curCol.Replace("col", "")).ToString();
                        view.GridControl.DoDragDrop(IdTicker, DragDropEffects.Move);

                        downHitInfo = null;
                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
                flgMouseDown = false;
            }
        }

        private void dgAucQuotes_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                downHitInfo = hitInfo;
        }

    }
}