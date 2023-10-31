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

using NestSymConn;
using NCommonTypes;
using NestDLL;
using NCustomControls;

using System.IO;

namespace CPort
{
    public partial class frmCPort : Form
    {
        List<CPortItem> SubscribedData = new List<CPortItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        BindingSource bndDataSource = new BindingSource();
        bool flgMouseDown = false;
        public string SQLString;
        int clearCounter = 0;
        public int OpenMode = 0;
        int ColorCode = 0;
        TradingStatus curStatusList = new TradingStatus();
        bool useBSE;
        double futIdSecurity = 64143;

        StreamWriter curLogStream = new StreamWriter(@"c:\temp\test2.txt", false);

        double yprevValue = 0;
        double nprevValue = 0;
        Int64 ycounter = 0;
        Int64 ncounter = 0;

        double curIndexLast = 0;
        double curFutureLast = 0;
        double curFutureBid = 0;
        double curFutureAsk = 0;
        double curFairLast = 0;
        double curIndexChange = 0;
        string strMessage = "";
        int FutPosition = -1;
        int RecalcCounter = 0;

        bool loadFinish = false;

        public frmCPort()
        {
            InitializeComponent();
        }

        private void frmIbovArb_Load(object sender, EventArgs e)
        {
            useBSE = true;

            SymConn.Instance.OnData += new EventHandler(NewMarketData);

            dtgCPort.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgCPort.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgCPort.LookAndFeel.SetSkinStyle("Blue");

            dgCPort.ColumnPanelRowHeight = 32;
            dgCPort.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            ColorCode = 0;

            if (ColorCode == 1)
            {
                dgCPort.Appearance.Row.ForeColor = Color.White;
                dgCPort.Appearance.Row.BackColor = Color.Black;
                dgCPort.OptionsView.ShowHorzLines = false;
                dgCPort.OptionsView.ShowVertLines = false;
            }

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                
                DateTime maxDate = DateTime.Parse(curConn.Execute_Query_String("SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073"));

                SQLString = " SELECT CASE WHEN ExchangeTicker<>'' THEN ExchangeTicker ELSE NestTicker END as ExchangeTicker, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument, " +
                            " COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), IdxWeight, F.SrValue " +
                            " FROM (  " +
                                    " SELECT 3 as IdSecurity, 0 IdxWeight" +
                            " ) A " +
                            " LEFT JOIN NESTDB.dbo.Tb001_Securities C " +
                            " ON A.IdSecurity=C.IdSecurity   " +
                            " LEFT JOIN NESTDB.dbo.Tb000_Issuers D " +
                            " ON C.IdIssuer=D.IdIssuer   " +
                            " LEFT JOIN NESTDB.dbo.Tb113_Setores E " +
                            " ON D.IdNestSector=E.Id_Setor " +
                            " LEFT JOIN (SELECT * FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE SrType=100 AND Source=1 AND SrDate=(SELECT MAX(SrDate) FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=1073)) AS F " +
                            " ON A.IdSecurity=F.IdSecurity " +
                            " WHERE ReutersTicker IS NOT NULL AND ReutersTicker<>''  " +
                            " ORDER BY A.IdSecurity DESC";

                DataTable dt = curConn.Return_DataTable(SQLString);
                
                foreach (DataRow curRow in dt.Rows)
                {
                    CPortItem curItem = CreateQuote("Y" + curRow[0].ToString(), curRow[1].ToString(), curRow[2].ToString(), int.Parse(curRow[3].ToString()), int.Parse(curRow[4].ToString()), double.Parse(curRow[5].ToString()), DateTime.Parse(curRow[6].ToString()), double.Parse(curRow[7].ToString()));
                    curItem = CreateQuote("" + curRow[0].ToString(), curRow[1].ToString(), curRow[2].ToString(), int.Parse(curRow[3].ToString()), int.Parse(curRow[4].ToString()), double.Parse(curRow[5].ToString()), DateTime.Parse(curRow[6].ToString()), double.Parse(curRow[7].ToString()));
                }
            }

            bndDataSource.DataSource = SubscribedData;
            dtgCPort.DataSource = bndDataSource;

            dgCPort.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Change"].DisplayFormat.FormatString = "P2";

            dgCPort.Columns["PCTAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["PCTAuction"].DisplayFormat.FormatString = "P2";
            

            dgCPort.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["AucVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["AucVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["AucLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["AucLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["Bid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Bid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["Ask"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Ask"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";
            
            dgCPort.Columns["FinVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["FinVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["Volume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Volume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgCPort.Columns["Expiration"].DisplayFormat.FormatString = "dd-MMM-yy";

            dgCPort.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["Strike"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgCPort.Columns["PositionQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["PositionQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["SentQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["SentQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["TargetQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["TargetQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["HedgeSize"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["HedgeSize"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["PositionValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["PositionValue"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["TradedValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["TradedValue"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["TradedPnL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["TradedPnL"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["validLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["validLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["validChange"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["validChange"].DisplayFormat.FormatString = "P2";


            dgCPort.Columns["prevQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["prevQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["curQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["curQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["diffQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["diffQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgCPort.Columns["publishBid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["publishBid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgCPort.Columns["publishAsk"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCPort.Columns["publishAsk"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            foreach (GridColumn curColumn in dgCPort.Columns)
            {
                curColumn.Visible = false;
            }

            int curPosition = 0;
            /*
            dgCPort.Columns["Ticker"].VisibleIndex = curPosition++; dgCPort.Columns["Ticker"].Visible = true;
            dgCPort.Columns["Sector"].VisibleIndex = curPosition++; dgCPort.Columns["Sector"].Visible = true;
            dgCPort.Columns["Close"].VisibleIndex = curPosition++; dgCPort.Columns["Close"].Visible = true;
            //dgCPort.Columns["Last"].VisibleIndex = curPosition++; dgCPort.Columns["Last"].Visible = true;
            dgCPort.Columns["BidSize"].VisibleIndex = curPosition++; dgCPort.Columns["Bid"].Visible = true;
            dgCPort.Columns["Bid"].VisibleIndex = curPosition++; dgCPort.Columns["Bid"].Visible = true;
            dgCPort.Columns["publishBid"].VisibleIndex = curPosition++; dgCPort.Columns["publishBid"].Visible = true;
            dgCPort.Columns["validLast"].VisibleIndex = curPosition++; dgCPort.Columns["validLast"].Visible = true;
            dgCPort.Columns["prevLast"].VisibleIndex = curPosition++; dgCPort.Columns["prevLast"].Visible = true;
            dgCPort.Columns["publishAsk"].VisibleIndex = curPosition++; dgCPort.Columns["publishAsk"].Visible = true;
            dgCPort.Columns["Ask"].VisibleIndex = curPosition++; dgCPort.Columns["Ask"].Visible = true;
            dgCPort.Columns["AskSize"].VisibleIndex = curPosition++; dgCPort.Columns["Ask"].Visible = true;
            //dgCPort.Columns["Spread"].VisibleIndex = curPosition++; dgCPort.Columns["Spread"].Visible = true;
            //dgCPort.Columns["AucLast"].VisibleIndex = curPosition++; dgCPort.Columns["AucLast"].Visible = true;
            dgCPort.Columns["FinVolume"].VisibleIndex = curPosition++; dgCPort.Columns["FinVolume"].Visible = true;
            dgCPort.Columns["AucCond"].VisibleIndex = curPosition++; dgCPort.Columns["AucCond"].Visible = true;
            //dgCPort.Columns["AucVolume"].VisibleIndex = curPosition++; dgCPort.Columns["AucVolume"].Visible = true;
            dgCPort.Columns["PositionQuantity"].VisibleIndex = curPosition++; dgCPort.Columns["PositionQuantity"].Visible = true;
            dgCPort.Columns["TargetQuantity"].VisibleIndex = curPosition++; dgCPort.Columns["TargetQuantity"].Visible = true;
            dgCPort.Columns["SentQuantity"].VisibleIndex = curPosition++; dgCPort.Columns["SentQuantity"].Visible = true;
            dgCPort.Columns["PositionValue"].VisibleIndex = curPosition++; dgCPort.Columns["PositionValue"].Visible = true;
            dgCPort.Columns["TradedValue"].VisibleIndex = curPosition++; dgCPort.Columns["TradedValue"].Visible = true;
            dgCPort.Columns["TradedPnL"].VisibleIndex = curPosition++; dgCPort.Columns["TradedValue"].Visible = true;
            //dgCPort.Columns["Change"].VisibleIndex = curPosition++; dgCPort.Columns["Change"].Visible = true;
            dgCPort.Columns["validChange"].VisibleIndex = curPosition++; dgCPort.Columns["validChange"].Visible = true;
            //dgCPort.Columns["PCTAuction"].VisibleIndex = curPosition++; dgCPort.Columns["PCTAuction"].Visible = true;
            //dgCPort.Columns["futChange"].VisibleIndex = curPosition++; dgCPort.Columns["futChange"].Visible = true;
            dgCPort.Columns["TradeSignal"].VisibleIndex = curPosition++; dgCPort.Columns["TradeSignal"].Visible = true;
            //dgCPort.Columns["HedgeSize"].VisibleIndex = curPosition++; dgCPort.Columns["HedgeSize"].Visible = true;
            dgCPort.Columns["TradingPhase"].VisibleIndex = curPosition++; dgCPort.Columns["TradingPhase"].Visible = true;

            dgCPort.Columns["prevQuantity"].VisibleIndex = curPosition++; dgCPort.Columns["prevQuantity"].Visible = true;
            dgCPort.Columns["curQuantity"].VisibleIndex = curPosition++; dgCPort.Columns["curQuantity"].Visible = true;
            dgCPort.Columns["diffQuantity"].VisibleIndex = curPosition++; dgCPort.Columns["curQuantity"].Visible = true;

            dgCPort.GroupSummary.Add(SummaryItemType.Sum, "IndexContrib", dgCPort.Columns["IndexContrib"], "{0:#,##0.00}");
            dgCPort.GroupSummary.Add(SummaryItemType.Sum, "HedgeSize", dgCPort.Columns["HedgeSize"], "{0:#,##0.00}");
            dgCPort.GroupSummary.Add(SummaryItemType.Sum, "PositionValue", dgCPort.Columns["PositionValue"], "{0:#,##0}");
            dgCPort.GroupSummary.Add(SummaryItemType.Sum, "TradedPnL", dgCPort.Columns["TradedPnL"], "{0:#,##0}");
           
            dgCPort.Columns["Currency"].GroupIndex = 1;
            dgCPort.ExpandAllGroups();
            dgCPort.Columns["FinVolume"].SortOrder = ColumnSortOrder.Descending;

            dgCPort.BestFitColumns();
            dgCPort.Columns["Ticker"].Width = 110;
            */
            loadFinish = true;

            //dgStrongOpen.Columns["PrevChangeSpread"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[PrevChangeSpread] < " + ((StgItem)SubscribedData[0]).PrevChangeTrigger.ToString("0.000").Replace(",", ".") + " OR [PositionQuantity]<>0");

            tmrUpdate.Start();
        }

        private CPortItem CreateQuote(string Ticker, string Sector, string Currency, int IdTicker, int IdInstrument, double Strike, DateTime Expiration, double idxQuantity)
        {
            CPortItem curItem = new CPortItem();
            curItem.IdTicker = IdTicker;
            curItem.Ticker = Ticker;
            curItem.IdInstrument = IdInstrument;
            curItem.Sector = Sector;
            curItem.Currency = Currency;
            curItem.Strike = Strike;
            curItem.Expiration = Expiration;
            SubscribedData.Add(curItem);

            UpdateListIndex();

            SymConn.Instance.Subscribe(Ticker);

            return curItem;
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].Ticker, i);
                if (SubscribedData[i].IdTicker == futIdSecurity) FutPosition = i;
            }
        }

        private void RefreshGrid()
        {
            dgCPort.LayoutChanged();
            dgCPort.UpdateGroupSummary();
            dgCPort.RefreshData();
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            while (!loadFinish)
            { }


            MarketUpdateList curList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdate in curList.ItemsList)
            {
                int i = 0;
                if (SubListIndex.TryGetValue(curUpdate.Ticker, out i))
                {
                    CPortItem curItem = (CPortItem)SubscribedData[i];
                    curItem.Update(curUpdate);

                    double compValue = curItem.Last;
                    NestFLIDS curFLID = NestFLIDS.Last;

                    if (curItem.Ticker == "YVALE5" && compValue != yprevValue && curUpdate.FLID == curFLID)
                    {
                        curLogStream.WriteLine(ycounter++ + " \t " + DateTime.Now.TimeOfDay.TotalMilliseconds + " \t " + curItem.Ticker + " \t " + compValue.ToString());
                        yprevValue = compValue;
                    }
                    if (curItem.Ticker == "VALE5" && compValue != nprevValue && curUpdate.FLID == curFLID)
                    {
                        curLogStream.WriteLine(ncounter++ + " \t " + DateTime.Now.TimeOfDay.TotalMilliseconds + " \t " + curItem.Ticker + " \t " + compValue.ToString());
                        nprevValue = compValue;
                    }

                }
            }
                    /*
                    // Check if trade was triggered

                    if (curItem.SentQuantity != 0 || curItem.TargetQuantity != 0 )
                    {
                        int minLot = 199;
                        if (curItem.IdTicker == futIdSecurity) minLot = 0;

                        if (curItem.TargetQuantity - curItem.SentQuantity > 0 + minLot || (curItem.TargetQuantity == 0 && curItem.SentQuantity != 0))
                        {
                            double TradeShares = curItem.TargetQuantity - curItem.SentQuantity;
                            double TradePrice = curItem.Ask;
                            if (TradePrice == 0) TradePrice = curItem.validLast;

                            strMessage += DateTime.Now.TimeOfDay + "\tBUY\t" + TradeShares.ToString("#,##0") + " \t " + curItem.Ticker + "\t@\t" + TradePrice.ToString("#,##0.00") + "\t" + (-TradeShares * TradePrice).ToString("#,##0.00") + "\r\n";

                            curItem.SentQuantity = curItem.SentQuantity + TradeShares;
                            curItem.TradedValue = curItem.TradedValue + -TradeShares * TradePrice;
                        }

                        if (curItem.TargetQuantity - curItem.SentQuantity < 0 - minLot || (curItem.TargetQuantity == 0 && curItem.SentQuantity != 0))
                        {
                            double TradeShares = curItem.TargetQuantity - curItem.SentQuantity;
                            double TradePrice = curItem.Bid;
                            if (TradePrice == 0) TradePrice = curItem.validLast;

                            //Console.WriteLine(DateTime.Now.TimeOfDay + "\tSELL\t" + TradeShares.ToString("#,##0") + " \t " + curItem.Ticker + "\t@\t" + TradePrice.ToString("#,##0.00") + "\t" + (-TradeShares * TradePrice).ToString("#,##0.00"));
                            strMessage += DateTime.Now.TimeOfDay + "\tSELL\t" + TradeShares.ToString("#,##0") + " \t " + curItem.Ticker + "\t@\t" + TradePrice.ToString("#,##0.00") + "\t" + (-TradeShares * TradePrice).ToString("#,##0.00") + "\r\n";

                            curItem.SentQuantity = curItem.SentQuantity + TradeShares;
                            curItem.TradedValue = curItem.TradedValue + -TradeShares * TradePrice;
                        }
                     */
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!flgMouseDown) RefreshGrid();
            lblIndexLast.Text = curIndexLast.ToString("#,##0.00");
            lblIndexChange.Text = curFairLast.ToString("#,##0.00%");
            txtMessages.Text = strMessage;
            txtMessages.SelectionStart = txtMessages.TextLength;
            txtMessages.ScrollToCaret();

            //SymConn.Instance.Subscribe("BRTO4");
            //SymConn.Instance.Subscribe("SBSP3");
            //SymConn.Instance.Subscribe("TMAR5");
            //SymConn.Instance.Subscribe("TNLP3");
            //SymConn.Instance.Subscribe("CSAN3");
            //SymConn.Instance.Subscribe("MRFG3");
            //SymConn.Instance.Subscribe("DTEX3");
            //SymConn.Instance.Subscribe("LAME4");
            //SymConn.Instance.Subscribe("ECOD3");
            //SymConn.Instance.Subscribe("INDV10");

            //SymConn.Instance.ConnectSym();
            RecalcCounter++;

            if (RecalcCounter > 2 * 60)
            {
                foreach (CPortItem curItem in SubscribedData)
                {
                    curItem.SetQuants();
                }

                RecalcCounter = 0;
            }
        }

        private void frmIbovArb_FormClosing(object sender, FormClosingEventArgs e)
        {
            SymConn.Instance.Dispose();
            curLogStream.Close();
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

            switch(ColorCode)
            {
                case 0: upColor = Color.Green; break;
                case 1: upColor = Color.Lime; break;
                default: upColor = Color.Green; break;
            }

            if (e.Column.Name == "colvalidLast" )
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
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

                if (column.FieldName == "IndexContrib")
                {
                    curFairLast = double.Parse(text);
                }
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
                MarketDataItem curItem = SubscribedData[i];
                CopyString = CopyString + curItem.Ticker + '\t';
                CopyString = CopyString + curItem.Strike + '\t';
                CopyString = CopyString + curItem.Expiration.ToString("yyyy-MM-dd") + '\t';
                CopyString = CopyString + curItem.Last.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Change.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Bid.ToString("0.00") + '\t';
                CopyString = CopyString + curItem.Ask.ToString("0.00") + '\t';
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
            if (!dgCPort.IsGroupRow(e.RowHandle))
            {
                if (1 < -0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255,255,190);
                    if (ColorCode == 1) e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void cmdCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(strMessage);
        }

    }
}