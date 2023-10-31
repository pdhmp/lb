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
    public partial class frmOptionChain : ConnectedForm
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
        
        string _curTicker = "";
        double curTickerLast = 0;

        public frmOptionChain(string curTicker)
        {
            _curTicker = curTicker;
            InitializeComponent();
            this.Text = "Option Chain - " + _curTicker;
        }

        ~frmOptionChain()
        {
            curNDistConn.Disconnect();
        }

        private void frmOptionChain_Load(object sender, EventArgs e)
        {
            curNDistConn.OnData += new EventHandler(NewMarketData);

            dtgOptionQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOptionQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOptionQuotes.LookAndFeel.SetSkinStyle("Blue");

            dgOptionQuotes.ColumnPanelRowHeight = 32;
            dgOptionQuotes.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;


            dgOptionQuotes.Appearance.Row.ForeColor = GlobalVars.Instance.GridForeColor;
            dgOptionQuotes.Appearance.Row.BackColor = GlobalVars.Instance.GridBackColor;
            dgOptionQuotes.OptionsView.ShowHorzLines = false;
            dgOptionQuotes.OptionsView.ShowVertLines = false;
            dgOptionQuotes.Appearance.Empty.BackColor = GlobalVars.Instance.GridBackColor;
            dgOptionQuotes.Appearance.FocusedRow.BackColor = Color.DarkGray;
            dgOptionQuotes.Appearance.SelectedRow.BackColor = Color.DarkGray;


            dtgOptionQuotes.Width = this.Width - 9;
            dtgOptionQuotes.Height = this.Height - 120 + 65;

            SQLString = " SELECT ExchangeTicker, C.IdPrimaryExchange, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument, COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), '_'+LEFT(ExchangeTicker,4) as Category, 0 AS SortOrder, RoundLot " +
                               " FROM " +
                               "(" +
                                    " SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities WHERE IdInstrument=3 AND IdCurrency=900 AND Expiration>=getdate() AND ExchangeTicker LIKE '" + _curTicker.Substring(0, _curTicker.Length - 1) + "%'" +
                                    " UNION ALL " +
                                    " SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities WHERE ExchangeTicker='" + _curTicker + "' " +
                               ") A " +
                               " LEFT JOIN NESTDB.dbo.Tb001_Securities C " +
                               " ON A.IdSecurity=C.IdSecurity " +
                               " LEFT JOIN NESTDB.dbo.Tb000_Issuers D " +
                               " ON C.IdIssuer=D.IdIssuer" +
                               " LEFT JOIN NESTDB.dbo.Tb113_Setores E " +
                               " ON D.IdNestSector=E.Id_Setor " +
                               " ORDER BY Setor";  

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
                        LTMarketDataItem curItem = CreateQuote((string)curRow[0], int.Parse(curRow[1].ToString()), curRow[2].ToString(), curRow[3].ToString(), int.Parse(curRow[4].ToString()), int.Parse(curRow[5].ToString()), double.Parse(curRow[6].ToString()), DateTime.Parse(curRow[7].ToString()), curRow[8].ToString(), int.Parse(curRow[9].ToString()), double.Parse(curRow[10].ToString()));
                        if ((int)NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]) == 3) curItem.CalcIVol = true;
                    }
                }
            }

            bndDataSource.DataSource = SubscribedData;
            dtgOptionQuotes.DataSource = bndDataSource;

            dgOptionQuotes.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Change"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["PCTAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["PCTAuction"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["ABSAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["ABSAuction"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["AucVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["AucVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgOptionQuotes.Columns["Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["AucLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["AucLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["Bid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Bid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["Ask"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Ask"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["Spread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Spread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgOptionQuotes.Columns["AvgSpread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["AvgSpread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgOptionQuotes.Columns["FinVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["FinVolume"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["Volume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Volume"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgOptionQuotes.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgOptionQuotes.Columns["Expiration"].DisplayFormat.FormatString = "dd-MMM-yy";

            dgOptionQuotes.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Strike"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["AucGain"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["AucGain"].DisplayFormat.FormatString = "#,##0.;(#,##0.);\\ ";

            dgOptionQuotes.Columns["Low"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Low"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["High"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["High"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["Intrinsic"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Intrinsic"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);#,##0.00";

            dgOptionQuotes.Columns["FromLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["FromLow"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["FromHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["FromHigh"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["PercMny"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["PercMny"].DisplayFormat.FormatString = "P1";

            dgOptionQuotes.Columns["Delta"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["Delta"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);#,##0.00";

            dgOptionQuotes.Columns["DateLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["DateLow"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["DateHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["DateHigh"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["FromDateLow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["FromDateLow"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["FromDateHigh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["FromDateHigh"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["FromVWAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["FromVWAP"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["AvVolume6m"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["AvVolume6m"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgOptionQuotes.Columns["AucTimeLeft"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgOptionQuotes.Columns["AucTimeLeft"].DisplayFormat.FormatString = "HH:mm:ss";

            dgOptionQuotes.Columns["AucCloseTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgOptionQuotes.Columns["AucCloseTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgOptionQuotes.Columns["LastTradeTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgOptionQuotes.Columns["LastTradeTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgOptionQuotes.Columns["LastUpdTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgOptionQuotes.Columns["LastUpdTime"].DisplayFormat.FormatString = "HH:mm:ss";

            dgOptionQuotes.Columns["IVolBid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["IVolBid"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["IVolAsk"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["IVolAsk"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.Columns["IVolLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionQuotes.Columns["IVolLast"].DisplayFormat.FormatString = "P2";

            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Average, "Change", dgOptionQuotes.Columns["Change"], "{0:0.00%}");
            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Average, "FromLow", dgOptionQuotes.Columns["Change"], "{0:0.00%}");
            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Average, "FromHigh", dgOptionQuotes.Columns["Change"], "{0:0.00%}");
            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Average, "FinVolume", dgOptionQuotes.Columns["FinVolume"], "{0:0.00}");
            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Average, "FromDateHigh", dgOptionQuotes.Columns["FromDateHigh"], "{0:0.00%}");
            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Average, "FromDateLow", dgOptionQuotes.Columns["FromDateLow"], "{0:0.00%}");
            SortOrderGroupItem = dgOptionQuotes.GroupSummary.Add(SummaryItemType.Min, "SortOrder", dgOptionQuotes.Columns["SortOrder"], "{0:0}");

            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Sum, "CloseToDateHigh", dgOptionQuotes.Columns["CloseToDateHigh"], "{0:0; -0; -}");
            dgOptionQuotes.GroupSummary.Add(SummaryItemType.Sum, "CloseToDateLow", dgOptionQuotes.Columns["CloseToDateLow"], "{0:0; -0; -}");

            foreach (GridColumn curColumn in dgOptionQuotes.Columns)
            {
                curColumn.Visible = false;
            }

            dgOptionQuotes.Columns["Ticker"].Visible = true;
            dgOptionQuotes.Columns["Expiration"].Visible = true;
            dgOptionQuotes.Columns["Strike"].Visible = true;
            dgOptionQuotes.Columns["PercMny"].Visible = true;
            dgOptionQuotes.Columns["Delta"].Visible = true;
            dgOptionQuotes.Columns["Intrinsic"].Visible = true;
            dgOptionQuotes.Columns["Last"].Visible = true;
            dgOptionQuotes.Columns["Intrinsic"].Visible = true;
            dgOptionQuotes.Columns["Bid"].Visible = true;
            dgOptionQuotes.Columns["Ask"].Visible = true;
            dgOptionQuotes.Columns["BidSize"].Visible = true;
            dgOptionQuotes.Columns["AskSize"].Visible = true;
            dgOptionQuotes.Columns["IVolBid"].Visible = true;
            dgOptionQuotes.Columns["IVolAsk"].Visible = true;

            int curPosition = 0;

            dgOptionQuotes.Columns["Ticker"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Expiration"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Strike"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["PercMny"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Delta"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Intrinsic"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Last"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Intrinsic"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Bid"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["Ask"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["BidSize"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["AskSize"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["IVolBid"].VisibleIndex = curPosition++;
            dgOptionQuotes.Columns["IVolAsk"].VisibleIndex = curPosition++;

            dgOptionQuotes.Columns["Expiration"].GroupIndex = 1;
            dgOptionQuotes.Columns["OptType"].GroupIndex = 2;
            dgOptionQuotes.Columns["Strike"].SortOrder = ColumnSortOrder.Ascending;
            dgOptionQuotes.ExpandAllGroups();

            dgOptionQuotes.Columns["Ticker"].Width = 110;
            dgOptionQuotes.Columns["Ticker"].Fixed = FixedStyle.Left;

            dgOptionQuotes.ExpandAllGroups();

            SubscribeTickers();

            dgOptionQuotes.ExpandAllGroups();

            UpdateFilter();
            
            timer1.Start();
            tmrUpdateUndLast.Start();
            tmrBestFit.Start();
        }

        public void ReLoadColumns()
        {
            curUtils.LoadGridColumns(dgOptionQuotes, this.Text);
            dgOptionQuotes.ExpandAllGroups();
        }

        private void SubscribeTickers()
        {
            foreach (LTMarketDataItem curItem in SubscribedData)
            {
                Sources curSource = GlobalVars.Instance.getDataSource(curItem.ExchangeTradingCode);
                if (curSource == Sources.None) curSource = Sources.Bovespa;
                curNDistConn.Subscribe(curItem.ExchangeTradingCode, curSource);
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
            dtgOptionQuotes.DataSource = null;
            for (int i = SubscribedData.Count-1; i > 0; i--)
			{
                LTMarketDataItem curItem = SubscribedData[i];
                if (curItem.Last == 0 && curItem.Bid == 0 && curItem.Ask == 0 && curItem.Volume == 0)
                {
                    //SubscribedData.Remove(curItem);
                }
			}
            UpdateListIndex();
            dtgOptionQuotes.DataSource = bndDataSource;
            dgOptionQuotes.ExpandAllGroups();

        }

        private void RefreshGrid()
        {
            dgOptionQuotes.LayoutChanged();
            dgOptionQuotes.UpdateGroupSummary();
            dgOptionQuotes.RefreshData();
        }
        
        private void NewMarketData(object sender, EventArgs origE)
        {
            int i = 0;
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                //if (curUpdateItem.Ticker == "PETRN13" || curUpdateItem.Ticker == "PETRN14")
                //{
                //    Console.WriteLine(curUpdateItem.Ticker + "\t" + curUpdateItem.FLID + "\t" + curUpdateItem.ValueDouble + "\t" + curUpdateItem.ValueString);
                //}

                if (curUpdateItem.FLID == NestFLIDS.Bid || curUpdateItem.FLID == NestFLIDS.Ask)
                {
                    if (curUpdateItem.ValueDouble > 99999) curUpdateItem.ValueDouble = 99999;
                    if (curUpdateItem.ValueDouble < -99999) curUpdateItem.ValueDouble = -99999;
                }

                if (curUpdateItem.ValueDouble != 0)
                {
                    if (SubListIndex.TryGetValue(curUpdateItem.Ticker, out i))
                    {
                        LTMarketDataItem curItem = (LTMarketDataItem)SubscribedData[i];
                        curItem.Update(curUpdateItem);
                    }
                    if (curUpdateItem.Ticker == _curTicker && curUpdateItem.FLID == NestFLIDS.Last)
                    {
                        curTickerLast = curUpdateItem.ValueDouble;
                    }
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

            if (e.Column.Name == "colBid" || e.Column.Name == "colAsk")
            {
                double refValue;
                if (double.TryParse(e.DisplayText, out refValue))
                {
                    if (refValue >= 99999 || refValue <= -99999)
                    {
                        e.DisplayText = "OPEN";
                    }
                }
            }

            if (e.Column.Name == "colABSAuction" || e.Column.Name == "colPCTAuction")
            {
                object curSelRow = dgOptionQuotes.GetRowCellValue(e.RowHandle, dgOptionQuotes.Columns["PCTAuction"]);
                    
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
                    else if (refValue == 0 && e.Appearance.ForeColor != dgOptionQuotes.Appearance.Row.ForeColor)
                    {
                        e.Appearance.ForeColor = dgOptionQuotes.Appearance.Row.ForeColor;
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
                dgOptionQuotes.FocusedRowHandle = 0;
                SelectFirstonNextSort = false;
            }
        }

        public void SetSortedColumn(string colName, bool RecurseColumns)
        {
            ColumnSortOrder curOrder = ColumnSortOrder.Descending;
            if (dgOptionQuotes.Columns[colName.Replace("col", "")].SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
            foreach (GridColumn curGrouping in dgOptionQuotes.GroupedColumns)
            {
                GridSummaryItem curItem = GetGroupByName(dgOptionQuotes, colName.Replace("col", ""));
                if (curItem != null)
                    dgOptionQuotes.GroupSummarySortInfo.Add(curItem, curOrder, curGrouping);
            }
            dgOptionQuotes.ClearSorting();
            dgOptionQuotes.Columns[colName.Replace("col", "")].SortOrder = curOrder;
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
            e.LevelAppearance.Font = new Font(dgOptionQuotes.Appearance.Row.Font, FontStyle.Bold); ;
        }

        private void chkMainOptions_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            if (!chkMainOptions.Checked)
            {
                dgOptionQuotes.Columns["TradingStatus"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Bid]<>0 OR [Ask]<>0");
            }
            else
            {
                dgOptionQuotes.Columns["TradingStatus"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Bid]>0 AND [Ask]>0");
            }


        }

        private void dgOptionQuotes_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);

            string a = ((GridView)sender).FocusedColumn.Name;

            if (info.HitTest == GridHitTest.RowCell && ((GridView)sender).FocusedValue.ToString() != "" && ((GridView)sender).FocusedColumn.Name.Contains("Ticker"))
            {
                string curValue = ((GridView)sender).FocusedValue.ToString();
                frmLevel2 curFrmLevel2 = new frmLevel2(curValue);
                curFrmLevel2.Show();
                //if (curValue.IndexOf('.') == -1) curValue = curValue + ".SA";
                curFrmLevel2.txtTicker.Text = curValue;
                curFrmLevel2.cmdRequest_Click(this, new EventArgs());
                curFrmLevel2.LoadOrders();
            }
        }

        private void tmrUpdateUndLast_Tick(object sender, EventArgs e)
        {
            foreach (LTMarketDataItem curItem in SubscribedData)
            {
                curItem.UnderlyingLast = curTickerLast;
            }
        }

        private void tmrBestFit_Tick(object sender, EventArgs e)
        {
            dgOptionQuotes.BestFitColumns();
            tmrBestFit.Stop();
        }

        private void frmOptionChain_FormClosing(object sender, FormClosingEventArgs e)
        {
            curNDistConn.Dispose();
        }
    }
}