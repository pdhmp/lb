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

namespace LiveTrade2
{
    public partial class frmIbovArb : ConnectedForm
    {
        public event EventHandler OnItemAlert;
        List<LTMarketDataItem> SubscribedData = new List<LTMarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        BindingSource bndDataSource = new BindingSource();
        bool flgMouseDown = false;
        public string SQLString;
        int clearCounter = 0;
        public int OpenMode = 0;
        int ColorCode = 0;
        TradingStatus curStatusList = new TradingStatus();
        bool useBSE;

        double curIndexLast = 0;
        double curFutureLast = 0;
        double curFutureBid = 0;
        double curFutureAsk = 0;
        double curFairLast = 0;

        double curDI1 = 0;
        double curDI2 = 0;

        DateTime expDI1 = new DateTime(1900, 01, 01);
        DateTime expDI2 = new DateTime(1900, 01, 01);
        DateTime expIbov = new DateTime(1900, 01, 01);

        double IdIbovFut = 81021;
        double IdRatFut1 = 5265;
        double IdRatFut2 = 676144;
        
        public frmIbovArb()
        {
            InitializeComponent();
            curNDistConn.OnData += new EventHandler(NewMarketData);
            curNDistConn.OnIndexComp += new EventHandler(NewIndexComp);
        }
        
        private void frmIbovArb_Load(object sender, EventArgs e)
        {
            curNDistConn.GetIndexComp("IBOV", 0);

            dtgQuotes.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgQuotes.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgQuotes.LookAndFeel.SetSkinStyle("Blue");
            
            ColorCode = 0;

            if (ColorCode == 1)
            {
                dgQuotes.Appearance.Row.ForeColor = Color.White;
                dgQuotes.Appearance.Row.BackColor = Color.Black;
                dgQuotes.OptionsView.ShowHorzLines = false;
                dgQuotes.OptionsView.ShowVertLines = false;
            }

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                DateTime maxDate = DateTime.Parse(curConn.Execute_Query_String("SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073"));

                SQLString = " SELECT C.NestTicker, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument, COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), IdxWeight, 'Components' AS Type " +
                           " FROM " +
                           "(" +
                                " SELECT Id_Ticker_Component AS IdSecurity, Quantity AS IdxWeight FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073 AND Date_Ref='" + maxDate.ToString("yyyy-MM-dd") + "'" +
                                " UNION SELECT 1073, 0 " +
                            ") A " +
                            " LEFT JOIN NESTDB.dbo.Tb001_Securities C " +
                            " ON A.IdSecurity=C.IdSecurity " +
                            " LEFT JOIN NESTDB.dbo.Tb000_Issuers D " +
                            " ON C.IdIssuer=D.IdIssuer " +
                            " LEFT JOIN NESTDB.dbo.Tb113_Setores E " +
                            " ON D.IdNestSector=E.Id_Setor " +
                            " WHERE NestTicker IS NOT NULL " +
                            " ORDER BY Setor";    

                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    string str_aux = curRow[0].ToString();
                    if (str_aux.Contains("-"))
                    {
                        str_aux = str_aux.Substring(0, str_aux.IndexOf("-"));
                        curRow[0] = str_aux;
                    }
                    CreateQuote(curRow[0].ToString(), curRow[1].ToString(), curRow[2].ToString(), int.Parse(curRow[3].ToString()), int.Parse(curRow[4].ToString()), NestDLL.Utils.ParseToDouble(curRow[5].ToString()), NestDLL.Utils.ParseToDateTime(curRow[6].ToString()), NestDLL.Utils.ParseToDouble(curRow[7].ToString()), curRow["Type"].ToString());
                }
            }

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                DateTime maxDate = DateTime.Parse(curConn.Execute_Query_String("SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073"));

                SQLString = " SELECT C.NestTicker, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument, COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), IdxWeight, 'Indices' AS Type " +
                                   " FROM " +
                                   "(" +
                                        " SELECT " + IdIbovFut + " AS IdSecurity, 0 AS IdxWeight " +
                                        " UNION SELECT " + IdRatFut1 + " AS IdSecurity, 0 AS IdxWeight " +
                                        " UNION SELECT " + IdRatFut2 + " AS IdSecurity, 0 AS IdxWeight " +
                                   ") A " +
                                    " LEFT JOIN NESTDB.dbo.Tb001_Securities C " +
                                    " ON A.IdSecurity=C.IdSecurity " +
                                    " LEFT JOIN NESTDB.dbo.Tb000_Issuers D " +
                                    " ON C.IdIssuer=D.IdIssuer " +
                                    " LEFT JOIN NESTDB.dbo.Tb113_Setores E " +
                                    " ON D.IdNestSector=E.Id_Setor " +
                                    " WHERE NestTicker IS NOT NULL " +
                                    " ORDER BY Setor";

                SQLString = SQLString.Replace("C.NestTicker", "ExchangeTicker");

                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    CreateQuote(curRow[0].ToString(), curRow[1].ToString(), curRow[2].ToString(), int.Parse(curRow[3].ToString()), int.Parse(curRow[4].ToString()), double.Parse(curRow[5].ToString()), DateTime.Parse(curRow[6].ToString()), double.Parse(curRow[7].ToString()), curRow["Type"].ToString());
                }
            }

            bndDataSource.DataSource = SubscribedData;
            dtgQuotes.DataSource = bndDataSource;

            dgQuotes.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Change"].DisplayFormat.FormatString = "P2";

            dgQuotes.Columns["PCTAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["PCTAuction"].DisplayFormat.FormatString = "P2";

            dgQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["CHGPrevAuc"].DisplayFormat.FormatString = "P2";
            
            dgQuotes.Columns["ABSAuction"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["ABSAuction"].DisplayFormat.FormatString = "P2";

            dgQuotes.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["AucVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["AucVolume"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgQuotes.Columns["Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["AucLast"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["AucLast"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["Bid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Bid"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["Ask"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Ask"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["Spread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Spread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgQuotes.Columns["AvgSpread"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["AvgSpread"].DisplayFormat.FormatString = "0.00;(0.00);\\ ";

            dgQuotes.Columns["FinVolume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["FinVolume"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["Volume"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Volume"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgQuotes.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgQuotes.Columns["Expiration"].DisplayFormat.FormatString = "dd-MMM-yy";

            dgQuotes.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["Strike"].DisplayFormat.FormatString = "#,##0.0;(#,##0.0);\\ ";

            dgQuotes.Columns["AucGain"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["AucGain"].DisplayFormat.FormatString = "#,##0.;(#,##0.);\\ ";

            dgQuotes.Columns["IndexQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["IndexQuantity"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["IndexContrib"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["IndexContrib"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgQuotes.Columns["AucGain"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgQuotes.Columns["AucGain"].DisplayFormat.FormatString = "#,##0.;(#,##0.);\\ ";

            dgQuotes.Columns["Ticker"].VisibleIndex = 0;
            dgQuotes.Columns["Sector"].VisibleIndex = 1;
            dgQuotes.Columns["Close"].VisibleIndex = 2;
            dgQuotes.Columns["Last"].VisibleIndex = 3;
            dgQuotes.Columns["AucLast"].VisibleIndex = 4;
            dgQuotes.Columns["Change"].VisibleIndex = 5;
            dgQuotes.Columns["PCTAuction"].VisibleIndex = 6;
            dgQuotes.Columns["AucVolume"].VisibleIndex = 7;
            dgQuotes.Columns["AucGain"].VisibleIndex = 8;
            dgQuotes.Columns["TradingStatus"].VisibleIndex = 9;
            dgQuotes.Columns["FinVolume"].VisibleIndex = 10;
            dgQuotes.Columns["BidSize"].VisibleIndex = 11;
            dgQuotes.Columns["Bid"].VisibleIndex = 12;
            dgQuotes.Columns["Ask"].VisibleIndex = 13;
            dgQuotes.Columns["AskSize"].VisibleIndex = 14;

            dgQuotes.GroupSummary.Add(SummaryItemType.Average, "Change", dgQuotes.Columns["Change"], "{0:0.00%}");
            dgQuotes.GroupSummary.Add(SummaryItemType.Average, "FinVolume", dgQuotes.Columns["FinVolume"], "{0:#,##0.00}");
            dgQuotes.GroupSummary.Add(SummaryItemType.Sum, "IndexContrib", dgQuotes.Columns["IndexContrib"], "{0:#,##0.00}");
            dgQuotes.Columns["Ask"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;


            foreach (GridColumn curColumn in dgQuotes.Columns)
            {
                curColumn.Visible = false;
            }

            dgQuotes.Columns["Ticker"].Visible = true;
            dgQuotes.Columns["Sector"].Visible = true;
            dgQuotes.Columns["Close"].Visible = true;
            dgQuotes.Columns["Last"].Visible = true;
            dgQuotes.Columns["AucLast"].Visible = true;
            dgQuotes.Columns["Change"].Visible = true;
            dgQuotes.Columns["PCTAuction"].Visible = true;
            dgQuotes.Columns["AucVolume"].Visible = true;
            dgQuotes.Columns["AucGain"].Visible = true;
            dgQuotes.Columns["TradingStatus"].Visible = true;
            dgQuotes.Columns["FinVolume"].Visible = true;
            dgQuotes.Columns["Currency"].Visible = true;
            dgQuotes.Columns["Change"].Visible = true;
            dgQuotes.Columns["IndexContrib"].Visible = true;
            dgQuotes.Columns["BidSize"].Visible = true;
            dgQuotes.Columns["AskSize"].Visible = true;

            dgQuotes.Columns["SecType"].GroupIndex = 1;
            dgQuotes.Columns["SecType"].SortOrder = ColumnSortOrder.Descending;
            dgQuotes.ExpandAllGroups();
            dgQuotes.Columns["Last"].SortOrder = ColumnSortOrder.Descending;

            //chkAuction.Checked = false;
            dgQuotes.BestFitColumns();
            dgQuotes.Columns["Ticker"].Width = 110;

            timer1.Start();
        }

        private void CreateQuote(string Ticker, string Sector, string Currency, int IdTicker, int IdInstrument, double Strike, DateTime Expiration, double idxQuantity, string SecType)
        {
            LTMarketDataItem curItem = new LTMarketDataItem();
            curItem.IdTicker = IdTicker;
            curItem.Ticker = Ticker;
            curItem.IdInstrument = IdInstrument;
            curItem.Sector = Sector;
            curItem.Currency = Currency;
            curItem.Strike = Strike;
            curItem.Expiration = Expiration;
            curItem.IndexQuantity = idxQuantity;
            curItem.SecType = SecType;
            SubscribedData.Add(curItem);

            UpdateListIndex();

            if (curItem.SecType == "Components")
                curNDistConn.Subscribe(Ticker, Sources.Bovespa);
            else
                curNDistConn.Subscribe(Ticker, Sources.BMF);
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].Ticker, i);
            }
        }

        private void ClearEmpty()
        {
            dtgQuotes.DataSource = null;
            for (int i = SubscribedData.Count-1; i > 0; i--)
			{
                LTMarketDataItem curItem = SubscribedData[i];
                if (curItem.Last == 0 && curItem.Bid == 0 && curItem.Ask == 0 && curItem.Volume == 0)
                {
                    SubscribedData.Remove(curItem);
                }
			}
            UpdateListIndex();
            dtgQuotes.DataSource = bndDataSource;
            dgQuotes.ExpandAllGroups();

        }

        private void chkAuction_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuction.Checked)
            {
                dgQuotes.Columns["AucLast"].Visible = true;
                dgQuotes.Columns["PCTAuction"].Visible = true;
                dgQuotes.Columns["AucVolume"].Visible = true;
                dgQuotes.Columns["AucGain"].Visible = true;
                dgQuotes.Columns["Last"].Visible = true;
                dgQuotes.Columns["TradingStatus"].Visible = true;

                dgQuotes.Columns["ABSAuction"].Visible = true;
                dgQuotes.Columns["CHGPrevAuc"].Visible = false;
                
                dgQuotes.Columns["Bid"].Visible = false;
                dgQuotes.Columns["Ask"].Visible = false;
                dgQuotes.Columns["Spread"].Visible = false;
                dgQuotes.Columns["AvgSpread"].Visible = false;

                dgQuotes.Columns["ABSAuction"].SortOrder = ColumnSortOrder.Descending;
                //dgQuotes.Columns["AucGain"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[AucGain] >= 10000");

            }
            else
            {
                dgQuotes.Columns["AucLast"].Visible = false;
                dgQuotes.Columns["PCTAuction"].Visible = false;
                dgQuotes.Columns["ABSAuction"].Visible = false;
                dgQuotes.Columns["CHGPrevAuc"].Visible = false;
                dgQuotes.Columns["AucVolume"].Visible = false;
                dgQuotes.Columns["TradingStatus"].Visible = false;
                dgQuotes.Columns["AucGain"].Visible = false;
                dgQuotes.Columns["Last"].Visible = true;
                dgQuotes.Columns["Bid"].Visible = true;
                dgQuotes.Columns["Ask"].Visible = true;
            }
        }

        private void RefreshGrid()
        {
            dgQuotes.LayoutChanged();
            dgQuotes.UpdateGroupSummary();
            dgQuotes.RefreshData();
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                int i = 0;

                if (SubListIndex.TryGetValue(curUpdateItem.Ticker, out i))
                {
                    MarketDataItem curItem = SubscribedData[i];
                    if (!(curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble == 0))
                    {
                        curItem.Update(curUpdateItem);
                    }

                    if (curItem.IdTicker == IdIbovFut)
                    {
                        curFutureLast = curItem.Last;
                        curFutureBid = curItem.Bid;
                        curFutureAsk = curItem.Last;
                        expIbov = curItem.Expiration;
                    }
                    else if (curItem.IdTicker == IdRatFut1)
                    {
                        expDI1 = curItem.Expiration;
                        if (curUpdateItem.ValueDouble != 0 && curUpdateItem.FLID == NestFLIDS.Last) 
                            curDI1 = curUpdateItem.ValueDouble;
                    }
                    else if (curItem.IdTicker == IdRatFut2)
                    {
                        expDI2 = curItem.Expiration;
                        if (curUpdateItem.ValueDouble != 0 && curUpdateItem.FLID == NestFLIDS.Last) 
                            curDI2 = curUpdateItem.ValueDouble;
                    }
                    else if (curItem.IdTicker == 1073)
                    {
                        curIndexLast = curItem.Last;
                    }
                }
            }
        }

        private void NewIndexComp(object sender, EventArgs origE)
        { 

        }

        private void LoadGrid()
        { 

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!flgMouseDown) RefreshGrid();
            if (clearCounter == 30)
            {
                ClearEmpty();
            }
            if (clearCounter < 20) clearCounter++;

            lblIndexLast.Text = curIndexLast.ToString("#,##0.00");
            lblFutureLast.Text = curFutureLast.ToString("#,##0.00");
            lblFutureBid.Text = curFutureBid.ToString("#,##0.00");
            lblFutureAsk.Text = curFutureAsk.ToString("#,##0.00");
            lblFairLast.Text = curFairLast.ToString("#,##0.00");
            lblDI1.Text = curDI1.ToString("#,##0.00");
            lblDI2.Text = curDI2.ToString("#,##0.00");

            double curFutFair = curFairLast * Math.Pow((1 + curDI1 / 100 - 0.03), ((expIbov.Subtract(DateTime.Now.Date).TotalDays) / 252));

            if(curDI1 != 0) lblFutFair.Text = curFutFair.ToString("#,##0.00");

            double curCarryRate = Math.Pow(curFutureLast / curFutFair, (252 / (expIbov.Subtract(DateTime.Now.Date).TotalDays))) - 1;

            if (curCarryRate != 0) lblCarryRate.Text = curCarryRate.ToString("#,##0.00%");
        }

        private void frmIbovArb_FormClosing(object sender, FormClosingEventArgs e)
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

            switch(ColorCode)
            {
                case 0: upColor = Color.Green; break;
                case 1: upColor = Color.Lime; break;
                default: upColor = Color.Green; break;
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
            if (!dgQuotes.IsGroupRow(e.RowHandle))
            {
                string tempStatus = dgQuotes.GetRowCellValue(e.RowHandle, "TradingStatus").ToString();
                if (!chkAuction.Checked)
                {
                    if(tempStatus == "FROZEN" || tempStatus == "SUSPEND" || tempStatus == "AUCTION")
                    {
                        e.Appearance.BackColor = Color.Silver;
                        if (ColorCode == 1) e.Appearance.ForeColor = Color.Black;
                    }
                }
                else
                {
                    if (tempStatus != "FROZEN" && tempStatus != "SUSPEND" && tempStatus != "AUCTION")
                    {
                        e.Appearance.BackColor = Color.LightGray;
                        if (ColorCode == 1) e.Appearance.ForeColor = Color.Black;
                    }
                }

            }
        }
    }
}