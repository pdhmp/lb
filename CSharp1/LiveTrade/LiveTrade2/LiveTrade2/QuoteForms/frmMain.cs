using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.IO;

using DevExpress.Data;
using DevExpress.Utils;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using NCommonTypes;
using NestSymConn;
using System.Diagnostics;
using NestDLL;

namespace LiveTrade2
{
    public partial class frmMain : ConnectedForm
    {
        BindingSource bndDataSource = new BindingSource();
        BindingSource bndDataSourceL2 = new BindingSource();

        public int OpenMode = 0;

        bool InEditMode = false;

        //NDistConn curNDistConn = new NDistConn();

        List<PhytonRunner> SORunnerList = new List<PhytonRunner>();
        List<LTLine> LTLineList = new List<LTLine>();

        List<ConnectedForm> ConnectedForms = new List<ConnectedForm>();

        protected Utils curUtils = new Utils(5);

        frmOrderReview curOrderReview;
        frmAuction curAuction;
        frmQuickChart curQuickChart;
        frmLimits curLimits;
        frmPnL curPnL;
        frmWatchList curfrmWatchList;

        bool bOpenScreen = false;
        double LINKBOVConn_Lat = 0;
        double LINKBMFConn_Lat = 0;
        double XPBOVConn_Lat = 0;
        double BSEConn_Lat = 0;
        double BELLConn_Lat = 0;
        bool flgMouseDown = false;
        int retryCounter = 0;

        int[, ,] updateItems = new int[60, 8, 14];

        public frmMain()
        {
            InitializeComponent();
        }

        ~frmMain()
        {
            curNDistConn.Disconnect();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //curNDistConn.BovespaOverride = Sources.LINKBOV;
            optAgg.Checked = true;
            curNDistConn.OnData += new EventHandler(NewMarketData);

            GlobalVars.Instance.LoadBookSection();
            GlobalVars.Instance.LoadAccountNumbers();

            for (int i = 0; i < 60; i++)
            {
                LTLine curLTLine = new LTLine();
                LTLineList.Add(curLTLine);
            }

            FIXConnections.Instance.InitializeFIX();
            if (FIXConnections.Instance.curFixConn != null)
            {
                FIXConnections.Instance.curFixConn.OnReject += new EventHandler(OrderRejected);
                FIXConnections.Instance.curFixConn.OnOrderUpdate += new EventHandler(FIXUpdateReceived);
            }

            cmdEnableTrade_Click(this, new EventArgs());

            LoadTickers();

            Thread runThread = new Thread(LoadTickerInfo);
            runThread.Start();

            dtgLTQuotes.DataSource = LTLineList;


            dgLTQuotes.Appearance.Row.ForeColor = GlobalVars.Instance.GridForeColor;
            dgLTQuotes.Appearance.Row.BackColor = GlobalVars.Instance.GridBackColor;

            dgLTQuotes.RowHeight = 14;

            int curIndex = 0;

            foreach (GridColumn curColumn in dgLTQuotes.Columns)
            {
                curColumn.VisibleIndex = curIndex++;
                if (curColumn.Name.Contains("Spacer")) { curColumn.Caption = " "; } else { curColumn.Caption = curColumn.Name.Substring(3, curColumn.Name.Length - 4); }
                if (curColumn.Name.Contains("Last")) { curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; curColumn.DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ "; }
                if (curColumn.Name.Contains("Change")) { curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; curColumn.DisplayFormat.FormatString = "#,##0.00%;-#,##0.00%;0.00%\\ "; curColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center; }
                if (curColumn.Name.Contains("Bid")) { curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; curColumn.DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ "; }
                if (curColumn.Name.Contains("Ask")) { curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; curColumn.DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ "; curColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near; }
                if (curColumn.Name.Contains("BidSize")) { curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; curColumn.DisplayFormat.FormatString = "#,##0;(#,##0);\\ "; }
                if (curColumn.Name.Contains("AskSize")) { curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; curColumn.DisplayFormat.FormatString = "#,##0;(#,##0);\\ "; curColumn.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near; }
                if (curColumn.Name.Contains("IdSecurity")) curColumn.Visible = false;
                if (curColumn.Name.Contains("AucCond")) curColumn.Visible = false;
                if (curColumn.Name.Contains("AucLast")) curColumn.Visible = false;
                if (curColumn.Name.Contains("AucChange")) curColumn.Visible = false;
            }

            //dgLTQuotes.BestFitColumns();

            curUtils.LoadGridColumns(dgLTQuotes, this.Text);

            curNDistConn.Connect();

            chkMTDChange.Checked = false;
            chkYTDChange.Checked = false;
            chkBidAsk.Checked = false;
            chkBidAskSize.Checked = false;

            PendingInsert = true;

            tmrUpdate.Start();

            curNDistConn.Subscribe("XPBOVConn", Sources.XPBOV);
            curNDistConn.Subscribe("LINKBOVConn", Sources.LINKBOV);
            curNDistConn.Subscribe("BSEConn", Sources.ProxyDiff);
            curNDistConn.Subscribe("BELLConn", Sources.BELL);
        }

        private void cmdEnableTrade_Click(object sender, EventArgs e)
        {
            if (FIXConnections.Instance.curFixConn != null)
            {
                if (FIXConnections.Instance.curFixConn.curLimits.OrderSendBlocked == true)
                {
                    FIXConnections.Instance.curFixConn.curLimits.OrderSendBlocked = false;
                    cmdEnableTrade.Text = "TRADING ENABLED";
                    cmdEnableTrade.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    FIXConnections.Instance.curFixConn.curLimits.OrderSendBlocked = true;
                    cmdEnableTrade.Text = "TRADING DISABLED";
                    cmdEnableTrade.BackColor = Color.Red;
                }
            }
            else
            {
                cmdEnableTrade.Text = "TRADING DISABLED";
                cmdEnableTrade.BackColor = Color.Red;
            }
        }

        private void OrderRejected(object sender, EventArgs origE)
        {
            NFIXConnLT.RejectEventArgs args = (NFIXConnLT.RejectEventArgs)origE;
            MessageBox.Show("Order rejected!\r\n\r\nID:\t" + args.IdOrder + "\r\nSymbol:\t" + args.Symbol + "\r\nPrice:\t" + args.Price.ToString("0.00") + "\r\nReason:\t" + args.Reason + "\r\n\r\n", "Order Rejected", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            MarketUpdateList curList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curQuote in curList.ItemsList)
            {

                //if (curQuote.FLID == NestFLIDS.AucCloseTime)
                //{
                //    Console.WriteLine(curQuote.ValueDouble);
                //}


                if (curQuote.Ticker == "")
                {
                    return;
                }

                if (curQuote.ValueDouble >= 9999999999) curQuote.ValueDouble = -2;
                if (curQuote.ValueDouble <= -9999999999) curQuote.ValueDouble = -1;

                if (curQuote.FLID == NestFLIDS.TradeTime)
                {
                    DateTime tempTime = new DateTime(1970, 1, 1, 0, 0, 0).Add(TimeSpan.FromSeconds(curQuote.ValueDouble));
                    if (tempTime > GlobalVars.Instance.LastQuoteReceived) GlobalVars.Instance.LastQuoteReceived = tempTime;
                }

                if (curQuote.FLID == NestFLIDS.Latency)
                {
                    switch (curQuote.Ticker)
                    {
                        case "LINKBOVConn": LINKBOVConn_Lat = curQuote.ValueDouble; break;
                        case "XPBOVConn": XPBOVConn_Lat = curQuote.ValueDouble; break;
                        case "LINKBMFConn": LINKBMFConn_Lat = curQuote.ValueDouble; break;
                        case "BSEConn": BSEConn_Lat = curQuote.ValueDouble; break;
                        case "BELLConn": BELLConn_Lat = curQuote.ValueDouble; break;
                        case "BELLConnXP": BELLConn_Lat = curQuote.ValueDouble; break;
                        default: break;
                    }
                }

                if (!(curQuote.FLID == NestFLIDS.Last && curQuote.ValueDouble == 0))
                {
                    if (curQuote.FLID == NestFLIDS.Last)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            LTLine curItem = LTLineList[i];
                            if (curItem.Quote1 == curQuote.Ticker) { curItem.SetLast1(curQuote.ValueDouble); updateItems[i, 1, 3] = 1; }
                            if (curItem.Quote2 == curQuote.Ticker) { curItem.SetLast2(curQuote.ValueDouble); updateItems[i, 2, 3] = 1; }
                            if (curItem.Quote3 == curQuote.Ticker) { curItem.SetLast3(curQuote.ValueDouble); updateItems[i, 3, 3] = 1; }
                            if (curItem.Quote4 == curQuote.Ticker) { curItem.SetLast4(curQuote.ValueDouble); updateItems[i, 4, 3] = 1; }
                            if (curItem.Quote5 == curQuote.Ticker) { curItem.SetLast5(curQuote.ValueDouble); updateItems[i, 5, 3] = 1; }
                            if (curItem.Quote6 == curQuote.Ticker) { curItem.SetLast6(curQuote.ValueDouble); updateItems[i, 6, 3] = 1; }
                            if (curItem.Quote7 == curQuote.Ticker) { curItem.SetLast7(curQuote.ValueDouble); updateItems[i, 7, 3] = 1; }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.Close)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            if (curQuote.ValueDouble != 0)
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.SetClose1(curQuote.ValueDouble); updateItems[i, 1, 4] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.SetClose2(curQuote.ValueDouble); updateItems[i, 2, 4] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.SetClose3(curQuote.ValueDouble); updateItems[i, 3, 4] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.SetClose4(curQuote.ValueDouble); updateItems[i, 4, 4] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.SetClose5(curQuote.ValueDouble); updateItems[i, 5, 4] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.SetClose6(curQuote.ValueDouble); updateItems[i, 6, 4] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.SetClose7(curQuote.ValueDouble); updateItems[i, 7, 4] = 1; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.Bid)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            if (curQuote.ValueDouble != 0)
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.Bid1 = curQuote.ValueDouble; updateItems[i, 1, 6] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.Bid2 = curQuote.ValueDouble; updateItems[i, 2, 6] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.Bid3 = curQuote.ValueDouble; updateItems[i, 3, 6] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.Bid4 = curQuote.ValueDouble; updateItems[i, 4, 6] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.Bid5 = curQuote.ValueDouble; updateItems[i, 5, 6] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.Bid6 = curQuote.ValueDouble; updateItems[i, 6, 6] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.Bid7 = curQuote.ValueDouble; updateItems[i, 7, 6] = 1; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.Ask)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            if (curQuote.ValueDouble != 0)
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.Ask1 = curQuote.ValueDouble; updateItems[i, 1, 7] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.Ask2 = curQuote.ValueDouble; updateItems[i, 2, 7] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.Ask3 = curQuote.ValueDouble; updateItems[i, 3, 7] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.Ask4 = curQuote.ValueDouble; updateItems[i, 4, 7] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.Ask5 = curQuote.ValueDouble; updateItems[i, 5, 7] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.Ask6 = curQuote.ValueDouble; updateItems[i, 6, 7] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.Ask7 = curQuote.ValueDouble; updateItems[i, 7, 7] = 1; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.BidSize)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            if (curQuote.ValueDouble != 0)
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.BidSize1 = curQuote.ValueDouble; updateItems[i, 1, 5] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.BidSize2 = curQuote.ValueDouble; updateItems[i, 2, 5] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.BidSize3 = curQuote.ValueDouble; updateItems[i, 3, 5] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.BidSize4 = curQuote.ValueDouble; updateItems[i, 4, 5] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.BidSize5 = curQuote.ValueDouble; updateItems[i, 5, 5] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.BidSize6 = curQuote.ValueDouble; updateItems[i, 6, 5] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.BidSize7 = curQuote.ValueDouble; updateItems[i, 7, 5] = 1; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.AskSize)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            if (curQuote.ValueDouble != 0)
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.AskSize1 = curQuote.ValueDouble; updateItems[i, 1, 8] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.AskSize2 = curQuote.ValueDouble; updateItems[i, 2, 8] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.AskSize3 = curQuote.ValueDouble; updateItems[i, 3, 8] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.AskSize4 = curQuote.ValueDouble; updateItems[i, 4, 8] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.AskSize5 = curQuote.ValueDouble; updateItems[i, 5, 8] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.AskSize6 = curQuote.ValueDouble; updateItems[i, 6, 8] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.AskSize7 = curQuote.ValueDouble; updateItems[i, 7, 8] = 1; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.TradingStatus)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.AucCond1 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 1, 12] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.AucCond2 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 2, 12] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.AucCond3 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 3, 12] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.AucCond4 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 4, 12] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.AucCond5 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 5, 12] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.AucCond6 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 6, 12] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.AucCond7 = (TradingStatusType)curQuote.ValueDouble; updateItems[i, 7, 12] = 1; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.AucLast)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.AucLast1 = curQuote.ValueDouble; updateItems[i, 1, 13] = 1; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.AucLast2 = curQuote.ValueDouble; updateItems[i, 2, 13] = 1; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.AucLast3 = curQuote.ValueDouble; updateItems[i, 3, 13] = 1; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.AucLast4 = curQuote.ValueDouble; updateItems[i, 4, 13] = 1; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.AucLast5 = curQuote.ValueDouble; updateItems[i, 5, 13] = 1; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.AucLast6 = curQuote.ValueDouble; updateItems[i, 6, 13] = 1; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.AucLast7 = curQuote.ValueDouble; updateItems[i, 7, 13] = 1; }
                            }
                        }
                    }
                    else
                    {
                        if (curQuote.FLID == NestFLIDS.TradingStatus)
                        {
                            string TempStatus = curQuote.ValueString;
                        }
                    }
                }
            }
        }

        private void RedrawCells(int RowHandle, int ColPos, int Item)
        {
            int curRow = dgLTQuotes.GetRowHandle(RowHandle);
            switch (Item)
            {
                case 3:
                case 4:
                    if (dgLTQuotes.Columns["Quote" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Quote" + ColPos]);
                    if (dgLTQuotes.Columns["Last" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Last" + ColPos]);
                    if (dgLTQuotes.Columns["Change" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Change" + ColPos]);
                    if (dgLTQuotes.Columns["ChangeMTD" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["ChangeMTD" + ColPos]);
                    if (dgLTQuotes.Columns["ChangeYTD" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["ChangeYTD" + ColPos]);
                    break;
                case 5: if (dgLTQuotes.Columns["BidSize" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["BidSize" + ColPos]); break;
                case 6: if (dgLTQuotes.Columns["Bid" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Bid" + ColPos]); break;
                case 7: if (dgLTQuotes.Columns["Ask" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Ask" + ColPos]); break;
                case 8: if (dgLTQuotes.Columns["AskSize" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["AskSize" + ColPos]); break;
                case 12:
                case 13:
                    if (dgLTQuotes.Columns["Quote" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Quote" + ColPos]);
                    if (dgLTQuotes.Columns["Last" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Last" + ColPos]);
                    if (dgLTQuotes.Columns["Change" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Change" + ColPos]);
                    if (dgLTQuotes.Columns["ChangeMTD" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["ChangeMTD" + ColPos]);
                    if (dgLTQuotes.Columns["ChangeYTD" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["ChangeYTD" + ColPos]);
                    if (dgLTQuotes.Columns["Spacer" + ColPos].Visible) dgLTQuotes.RefreshRowCell(curRow, dgLTQuotes.Columns["Spacer" + ColPos]);
                    break;
                default: break;
            }
        }

        private void ShowLatency()
        {
            int iMinLatency = 500;

            // lblLastTime.Text = DateTime.Now.TimeOfDay.Subtract(GlobalVars.Instance.LastQuoteReceived.TimeOfDay).TotalMilliseconds.ToString("0") + " ms";
            // lblLastTime.Text = BSEConn_Lat.ToString("#,###0") + " ms";
            // labBELLConn_Lat.Text    = BELLConn_Lat.ToString("#,###0") + " ms";
            // labBSEConn_Lat.Text     = BSEConn_Lat.ToString("#,###0") + " ms";
            // labLINKBOVConn_Lat.Text = LINKBOVConn_Lat.ToString("#,###0") + " ms";


            // BELL
            // --------------------------------------------------------------
            lblBell_1.Text = BELLConn_Lat.ToString("#,###0") + " ms";

            if (BELLConn_Lat >= iMinLatency)
                lblBell_1.BackColor = Color.FromArgb(255, 128, 0);
            else
                lblBell_1.BackColor = Color.FromArgb(0, 192, 0);

            // BSE
            // --------------------------------------------------------------
            lblBSE_1.Text = BSEConn_Lat.ToString("#,###0") + " ms";

            if (BSEConn_Lat >= iMinLatency)
                lblBSE_1.BackColor = Color.FromArgb(255, 128, 0);
            else
                lblBSE_1.BackColor = Color.FromArgb(0, 192, 0);

            // Link BOV
            // --------------------------------------------------------------
            lblLinkBOV_1.Text = LINKBOVConn_Lat.ToString("#,###0") + " ms";

            if (LINKBOVConn_Lat >= iMinLatency)
                lblLinkBOV_1.BackColor = Color.FromArgb(255, 128, 0);
            else
                lblLinkBOV_1.BackColor = Color.FromArgb(0, 192, 0);


            // XP BOV
            //---------------------------------------------------------
            lblXPBOV_1.Text = XPBOVConn_Lat.ToString("#,###0") + " ms";

            if (XPBOVConn_Lat >= iMinLatency)
                lblXPBOV_1.BackColor = Color.FromArgb(255, 128, 0);
            else
                lblXPBOV_1.BackColor = Color.FromArgb(0, 192, 0);

        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            lblFIXSentGross.Text = curNDistConn.LastUpdTime().ToString().Substring(11);

            if (FIXConnections.Instance.curFixConn != null)
            {
                lblFIXState.Text = "FIX IS ENABLED";
                lblFIXState.BackColor = Color.FromArgb(0, 192, 0);
                lblFIXReplaces.Text = FIXConnections.Instance.curFixConn.ReplacesSent.ToString();
                lblFIXExecs.Text = FIXConnections.Instance.curFixConn.FillsReceived.ToString();
                lblFIXSentGross.Text = FIXConnections.Instance.curFixConn.curLimits.SentGross.ToString("#,##0.00");
                lblFIXSentNet.Text = FIXConnections.Instance.curFixConn.curLimits.SentNet.ToString("#,##0.00");

                if (FIXConnections.Instance.curFixConn.IsAliveSession)
                {
                    lblFIXConnected.Visible = true;

                    // Se todas a conexoes estiverem disponiveis
                    // Podemos abrir as telas Dafaults
                    if (!bOpenScreen) this.OpenLevel2();
                }
                else
                {
                    lblFIXConnected.Visible = false;
                    lblFIXState.Text = "FIX IS TURNED OFF";
                    lblFIXState.BackColor = Color.Red;
                }
            }
            else
            {
                lblFIXState.Text = "FIX IS TURNED OFF";
                lblFIXState.BackColor = Color.Red;
            }

            if (curNDistConn.IsConnected())
            {
                cmdMKTData.BackColor = Color.FromArgb(0, 192, 0);
            }
            else
            {
                cmdMKTData.BackColor = Color.Red;

                if (retryCounter > (5000 / tmrUpdate.Interval) && cmdMKTData.BackColor == Color.Red)
                {
                    cmdMKTData_Click(this, new EventArgs());
                    retryCounter = 0;
                }
                else
                {
                    retryCounter++;
                }
            }

            this.ShowLatency();

            if (!InEditMode && !flgMouseDown)
            {
                for (int i = 0; i < 60; i++)
                {
                    for (int j = 1; j < 8; j++)
                    {
                        for (int k = 1; k < 14; k++)
                        {
                            if (updateItems[i, j, k] >= 4)
                            {
                                updateItems[i, j, k] = 0;
                                RedrawCells(i, j, k);
                            }
                            else if (updateItems[i, j, k] == 1)
                            {
                                RedrawCells(i, j, k);
                                updateItems[i, j, k]++;
                            }
                            else if (updateItems[i, j, k] > 1)
                            {
                                updateItems[i, j, k]++;
                            }
                        }
                    }
                }
            }

            if (PendingInsert) { PendingInsert = false; InsertExecutions(); }
        }

        private void frmStrongOpen_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalVars.Instance.appClosing = true;
            curNDistConn.WasDisposed = true;

            foreach (PhytonRunner tempSORunner in SORunnerList)
            {
                tempSORunner.StopProcessingQueue = true;

                if (tempSORunner.curPlayer != null)
                {
                    tempSORunner.curPlayer.Stop();
                }
                if (FIXConnections.Instance.curFixConn != null)
                    FIXConnections.Instance.curFixConn.Dispose();
            }
        }

        private void SaveTickers()
        {
            StreamWriter sw = new StreamWriter(@"C:\LiveTrade\TickerList.csv");
            foreach (LTLine curLine in LTLineList)
            {
                sw.WriteLine(curLine.Quote1 + ";" + curLine.Quote2 + ";" + curLine.Quote3 + ";" + curLine.Quote4 + ";" + curLine.Quote5 + ";" + curLine.Quote6 + ";" + curLine.Quote7);
            }
            sw.Close();
        }

        private void LoadTickers()
        {
            StreamReader sr = new StreamReader(@"C:\LiveTrade\TickerList.csv");
            string tempLine = "";
            int curCounter = 0;

            while ((tempLine = sr.ReadLine()) != null)
            {
                LTLineList[curCounter].Quote1 = tempLine.Split(';')[0];
                LTLineList[curCounter].Quote2 = tempLine.Split(';')[1];
                LTLineList[curCounter].Quote3 = tempLine.Split(';')[2];
                LTLineList[curCounter].Quote4 = tempLine.Split(';')[3];
                LTLineList[curCounter].Quote5 = tempLine.Split(';')[4];
                LTLineList[curCounter].Quote6 = tempLine.Split(';')[5];
                LTLineList[curCounter].Quote7 = tempLine.Split(';')[6];

                SubscribeData(LTLineList[curCounter].Quote1);
                SubscribeData(LTLineList[curCounter].Quote2);
                SubscribeData(LTLineList[curCounter].Quote3);
                SubscribeData(LTLineList[curCounter].Quote4);
                SubscribeData(LTLineList[curCounter].Quote5);
                SubscribeData(LTLineList[curCounter].Quote6);
                SubscribeData(LTLineList[curCounter].Quote7);

                curCounter++;
            }
            sr.Close();
        }

        private void SubscribeData(string ExchangeTradingCode)
        {
            if (ExchangeTradingCode != "" && ExchangeTradingCode.Substring(0, 1) != "'")
            {
                int _curIdInstrument = GlobalVars.Instance.getIdInstrument(ExchangeTradingCode);
                int Exchange = GlobalVars.Instance.getIdPrimaryExchange(ExchangeTradingCode);
                Sources curSource;

                curSource = GlobalVars.Instance.getDataSource(ExchangeTradingCode);

                // if (curSource == Sources.BMF) curSource = Sources.LINKBMF; // ================================================== COMENTAR
                // if (curSource == Sources.Bovespa) curSource = Sources.XPBOV; // ================================================== COMENTAR
                // if (curSource == Sources.Bovespa) curSource = Sources.LINKBOV; // <<<<<<<< ================================================== COMENTAR

                if (curSource == Sources.None) curSource = Sources.Bovespa;

                curNDistConn.Subscribe(ExchangeTradingCode, curSource);
            }

        }

        private void LoadTickerInfo()
        {
            using (newNestConn curconn = new newNestConn())
            {
                for (int i = 0; i < LTLineList.Count; i++)
                {
                    LTLine curLTLine = LTLineList[i];
                    if (curLTLine.IdSecurity1 == 0 && curLTLine.Quote1 != "") curLTLine.IdSecurity1 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote1);
                    if (curLTLine.IdSecurity2 == 0 && curLTLine.Quote2 != "") curLTLine.IdSecurity2 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote2);
                    if (curLTLine.IdSecurity3 == 0 && curLTLine.Quote3 != "") curLTLine.IdSecurity3 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote3);
                    if (curLTLine.IdSecurity4 == 0 && curLTLine.Quote4 != "") curLTLine.IdSecurity4 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote4);
                    if (curLTLine.IdSecurity5 == 0 && curLTLine.Quote5 != "") curLTLine.IdSecurity5 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote5);
                    if (curLTLine.IdSecurity6 == 0 && curLTLine.Quote6 != "") curLTLine.IdSecurity6 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote6);
                    if (curLTLine.IdSecurity7 == 0 && curLTLine.Quote7 != "") curLTLine.IdSecurity7 = GlobalVars.Instance.getIdSecurity(curLTLine.Quote7);

                    if (curLTLine.IdSecurity1 > 0) curLTLine._MTD_TRPrice1 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity1 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity2 > 0) curLTLine._MTD_TRPrice2 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity2 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity3 > 0) curLTLine._MTD_TRPrice3 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity3 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity4 > 0) curLTLine._MTD_TRPrice4 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity4 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity5 > 0) curLTLine._MTD_TRPrice5 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity5 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity6 > 0) curLTLine._MTD_TRPrice6 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity6 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity7 > 0) curLTLine._MTD_TRPrice7 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity7 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");

                    if (curLTLine.IdSecurity1 > 0) curLTLine._YTD_TRPrice1 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity1 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity2 > 0) curLTLine._YTD_TRPrice2 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity2 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity3 > 0) curLTLine._YTD_TRPrice3 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity3 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity4 > 0) curLTLine._YTD_TRPrice4 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity4 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity5 > 0) curLTLine._YTD_TRPrice5 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity5 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity6 > 0) curLTLine._YTD_TRPrice6 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity6 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity7 > 0) curLTLine._YTD_TRPrice7 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity7 + " AND SrType=101 AND SrDate<'" + new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");

                    if (curLTLine.IdSecurity1 > 0) curLTLine._Prev_TRPrice1 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity1 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity2 > 0) curLTLine._Prev_TRPrice2 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity2 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity3 > 0) curLTLine._Prev_TRPrice3 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity3 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity4 > 0) curLTLine._Prev_TRPrice4 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity4 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity5 > 0) curLTLine._Prev_TRPrice5 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity5 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity6 > 0) curLTLine._Prev_TRPrice6 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity6 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                    if (curLTLine.IdSecurity7 > 0) curLTLine._Prev_TRPrice7 = curconn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + curLTLine.IdSecurity7 + " AND SrType=101 AND SrDate<'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC");
                }
            }
        }

        #region FormControls

        private void ClientFormClosed(object sender, EventArgs e)
        {
            ConnectedForms.Remove((ConnectedForm)sender);
        }

        private void cmdOptionChain_Click(object sender, EventArgs e)
        {
            if (dgLTQuotes.FocusedColumn.Name.Contains("Quote"))
            {
                string curTicker = dgLTQuotes.FocusedValue.ToString();

                if (curTicker != "")
                {
                    frmOptionChain newOptionChain = new frmOptionChain(curTicker);
                    ConnectedForms.Add(newOptionChain);
                    newOptionChain.ClientClosedForm += new EventHandler(ClientFormClosed);
                    newOptionChain.Show();
                }
            }
        }

        private void StartChartThread()
        {
            if (curQuickChart == null || curQuickChart.IsDisposed) curQuickChart = new frmQuickChart();
            curQuickChart.curTicker = "PETR4";
            curQuickChart.ShowDialog();
            //curQuickChart.BringToFront();
            //if (curQuickChart.WindowState == FormWindowState.Minimized) curQuickChart.WindowState = FormWindowState.Normal;
            //while (true)
            //{ }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            FIXConnections.Instance.curFixConn.cancelAllOrders();
            FIXConnections.Instance.curFixConn.resendAllStatus();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (InEditMode)
            {
                cmdEdit.Text = "Edit";
                InEditMode = false;
                dgLTQuotes.OptionsView.ShowHorzLines = false;
                dgLTQuotes.OptionsView.ShowVertLines = false;

                foreach (LTLine curLTLine in LTLineList)
                {
                    SubscribeData(curLTLine.Quote1);
                    SubscribeData(curLTLine.Quote2);
                    SubscribeData(curLTLine.Quote3);
                    SubscribeData(curLTLine.Quote4);
                    SubscribeData(curLTLine.Quote5);
                    SubscribeData(curLTLine.Quote6);
                    SubscribeData(curLTLine.Quote7);
                }
                //ReSubscribe();
            }
            else
            {
                cmdEdit.Text = "Finish Edit";
                InEditMode = true;
                dgLTQuotes.OptionsView.ShowHorzLines = true;
                dgLTQuotes.OptionsView.ShowVertLines = true;
            }
        }

        private void cmdSaveTickers_Click(object sender, EventArgs e)
        {
            SaveTickers();
        }

        private void chkMTDChange_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColumnOrder();
        }

        private void chkYTDChange_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColumnOrder();
        }

        private void chkBidAsk_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColumnOrder();
        }

        private void chkBidAskSize_CheckedChanged(object sender, EventArgs e)
        {
            UpdateColumnOrder();
        }

        private void cmdMKTData_Click(object sender, EventArgs e)
        {
            if (!curNDistConn.IsConnected()) { curNDistConn.Connect(); }
            ReSubscribe();

            foreach (ConnectedForm curConnectedForm in ConnectedForms)
            {
                if (!curConnectedForm.IsDisposed)
                {
                    if (!curConnectedForm.curNDistConn.IsConnected()) { curConnectedForm.curNDistConn.Connect(); }
                    curConnectedForm.ReSubscribe();
                }
            }
        }

        private void cmdWatchList_Click(object sender, EventArgs e)
        {
            if (curfrmWatchList == null || curfrmWatchList.IsDisposed)
            {
                curfrmWatchList = new frmWatchList();
                ConnectedForms.Add(curfrmWatchList);
                curfrmWatchList.ClientClosedForm += new EventHandler(ClientFormClosed);
                curfrmWatchList.Show();
            }
            else
            {
                if (curfrmWatchList.WindowState == FormWindowState.Minimized)
                    curfrmWatchList.WindowState = FormWindowState.Normal;
                else
                {
                    curfrmWatchList.TopMost = true;
                    curfrmWatchList.TopMost = false;
                }
            }
        }

        private void cmdPNL_Click(object sender, EventArgs e)
        {
            if (curPnL == null || curPnL.IsDisposed)
            {
                curPnL = new frmPnL();
                ConnectedForms.Add(curPnL);
                curPnL.ClientClosedForm += new EventHandler(ClientFormClosed);
                curPnL.Show();
            }
            else
            {
                if (curPnL.WindowState == FormWindowState.Minimized)
                    curPnL.WindowState = FormWindowState.Normal;
                else
                {
                    curPnL.BringToFront();
                }
            }
        }

        private void cmdOrderReview_Click(object sender, EventArgs e)
        {
            if (curOrderReview == null || curOrderReview.IsDisposed)
            {
                curOrderReview = new frmOrderReview();
                ConnectedForms.Add(curOrderReview);
                curOrderReview.ClientClosedForm += new EventHandler(ClientFormClosed);
                curOrderReview.Show();
            }
            else
            {
                if (curOrderReview.WindowState == FormWindowState.Minimized)
                    curOrderReview.WindowState = FormWindowState.Normal;
                else
                {
                    curOrderReview.BringToFront();
                }
            }
        }


        private void cmdViewAuction_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (curAuction == null || curAuction.IsDisposed)
            {
                curAuction = new frmAuction();
                ConnectedForms.Add(curAuction);
                curAuction.ClientClosedForm += new EventHandler(ClientFormClosed);
            }
            curAuction.Show();
            curAuction.BringToFront();
            if (curAuction.WindowState == FormWindowState.Minimized) curAuction.WindowState = FormWindowState.Normal;
            Cursor.Current = Cursors.Default;
        }

        private void cmdChart_Click(object sender, EventArgs e)
        {
            if (curQuickChart == null || curQuickChart.IsDisposed)
            {
                curQuickChart = new frmQuickChart();
                ConnectedForms.Add(curQuickChart);
                curQuickChart.ClientClosedForm += new EventHandler(ClientFormClosed);
                curQuickChart.Show();
                curQuickChart.curTicker = "PETR4";
            }
            else
            {
                if (curQuickChart.WindowState == FormWindowState.Minimized)
                    curQuickChart.WindowState = FormWindowState.Normal;
                else
                {
                    curQuickChart.BringToFront();
                }
            }
        }

        #endregion

        private void dgQuotes_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            string curColumn = "";
            Color ForeColor = GlobalVars.Instance.GridForeColor;
            Color BackColor = GlobalVars.Instance.GridBackColor;
            string curAucCond = dgLTQuotes.GetRowCellValue(e.RowHandle, "AucCond" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)).ToString();
            bool InAuction = false;
            bool FontBold = false;

            if (e.Column.Name.Contains("Quote")) curColumn = "Quote";
            else if (e.Column.Name.Contains("Last")) curColumn = "Last";
            else if (e.Column.Name.Contains("Bid")) curColumn = "Bid";
            else if (e.Column.Name.Contains("Ask")) curColumn = "Ask";
            else if (e.Column.Name.Contains("ChangeMTD")) curColumn = "ChangeMTD";
            else if (e.Column.Name.Contains("ChangeYTD")) curColumn = "ChangeYTD";
            else if (e.Column.Name.Contains("Spacer")) curColumn = "Spacer";
            else if (e.Column.Name.Length > 8) { if (e.Column.Name.Substring(3, 6) == "Change") curColumn = "Change"; }

            string curTicker = "";
            int curGroup = 0;

            if (e.Column.Name.Length > 3)
            {
                curTicker = dgLTQuotes.GetRowCellValue(e.RowHandle, "Quote" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)).ToString();
                curGroup = int.Parse(e.Column.Name.Substring(e.Column.Name.Length - 1, 1));
            }

            if (curAucCond == "AUCTION_K" || curAucCond == "G_PREOPEN_P") InAuction = true;

            if (InAuction && curColumn != "Spacer")
            {
                BackColor = Color.Yellow;
                ForeColor = Color.Black;
            }

            if (e.CellValue.ToString().Trim() != "")
            {
                if (curTicker == "")
                {
                    e.DisplayText = "";
                }
                else if (curTicker.Substring(0, 1) == "'")
                {
                    e.DisplayText = "";
                    if (curColumn == "Quote")
                    {
                        Rectangle rect = GetColumnBounds(e.Column);

                        string text = curTicker;
                        SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                        int width = Convert.ToInt32(sz.Width) + 1;
                        //rect.X += rect.Width - width + 5;
                        rect.Width = width + 200;
                        rect.Y = e.Bounds.Y;
                        rect.Height = e.Bounds.Height - 2;

                        System.Drawing.Font TextFont = new Font(e.Appearance.Font, FontStyle.Bold);
                        Brush TextBrush = new SolidBrush(Color.White);

                        e.Appearance.DrawString(e.Cache, curTicker.Substring(1), rect, TextFont, TextBrush, new StringFormat());

                        FontBold = true;
                    }
                }
                else
                {
                    if (curColumn == "Quote")
                    {
                        switch (curAucCond)
                        {
                            case "G_AFTERMKT_R": BackColor = Color.LightBlue; ForeColor = Color.Black; break;
                            case "SUSP_U":
                            case "FROZEN_G": BackColor = Color.Orange; ForeColor = Color.Black; break;
                            case "AUCTION_K":
                            case "G_PREOPEN_P": BackColor = Color.Yellow; ForeColor = Color.Black; break;
                            default: BackColor = GlobalVars.Instance.GridBackColor; ForeColor = GlobalVars.Instance.GridForeColor; break;
                        }

                        if (GlobalVars.Instance.GridColorCode == 2) FontBold = true;
                    }
                    else if (curColumn == "Last")
                    {
                        if (InAuction && (double)dgLTQuotes.GetRowCellValue(e.RowHandle, "AucLast" + curGroup) != 0)
                        {
                            e.DisplayText = ((double)dgLTQuotes.GetRowCellValue(e.RowHandle, "AucLast" + curGroup)).ToString("0.00");
                        }
                        else
                        {
                            // BackColor = GlobalVars.Instance.GridBackColor;
                            //ForeColor = GlobalVars.Instance.GridForeColor;
                        }
                        if (curTicker.Length > 3)
                        {
                            if (curTicker.Substring(0, 3) == "IND" || curTicker.Substring(0, 3) == "WIN")
                            {
                                e.DisplayText = Convert.ToSingle(e.CellValue).ToString("#,##0");
                            }
                            if (curTicker.Substring(0, 3) == "USD" && curTicker.Length == 6)
                            {
                                e.DisplayText = Convert.ToSingle(e.CellValue).ToString("#,##0.####");
                            }
                        }
                        if (e.DisplayText == "0,00") { e.DisplayText = ""; }

                        if (updateItems[e.RowHandle, curGroup, 3] > 1)
                        {
                            BackColor = GlobalVars.Instance.GridChangeColor;
                        }
                    }
                    else if (curColumn == "Bid" || curColumn == "Ask")
                    {
                        if (updateItems[e.RowHandle, curGroup, 3] > 1)
                        {
                            BackColor = Color.Navy;
                        }

                        if (curTicker.Length > 3)
                        {
                            if (curTicker.Substring(0, 3) == "IND" || curTicker.Substring(0, 3) == "WIN")
                            {
                                e.DisplayText = Convert.ToSingle(e.CellValue).ToString("#,##0");
                            }
                        }
                        if (Convert.ToSingle(e.CellValue) == -2)
                        {
                            e.DisplayText = "OPEN";
                        }
                        if (Convert.ToSingle(e.CellValue) <= -1)
                        {
                            e.DisplayText = "OPEN";
                        }
                    }
                    else if (curColumn.Contains("Change"))
                    {
                        if (!InAuction) ForeColor = GlobalVars.Instance.GridPositiveColor;
                        if (InAuction && (double)dgLTQuotes.GetRowCellValue(e.RowHandle, "AucLast" + curGroup) != 0)
                        {
                            e.DisplayText = ((double)dgLTQuotes.GetRowCellValue(e.RowHandle, "AucChange" + curGroup)).ToString("0.00%");
                            if (e.DisplayText == "-100,00%") { e.DisplayText = ""; }
                        }
                        else
                        {
                            //BackColor = GlobalVars.Instance.GridBackColor;
                            //ForeColor = GlobalVars.Instance.GridChangeColor;

                            if (curTicker.Length > 3)
                            {
                                if (curTicker.Substring(0, 3) == "DI1")
                                {
                                    e.DisplayText = Convert.ToSingle(e.CellValue).ToString("0.00");
                                }
                            }
                            if (Convert.ToSingle(e.CellValue) > 0.005)
                            {
                                if (double.Parse(e.CellValue.ToString()) > 0.03 && curColumn.Length < 7) { FontBold = true; }
                            }
                            else if (Convert.ToSingle(e.CellValue) < -0.005)
                            {
                                ForeColor = Color.Red;
                                if (double.Parse(e.CellValue.ToString()) < -0.03 && curColumn.Length < 7) { FontBold = true; }
                            }
                            else if (Convert.ToSingle(e.CellValue) < 0)
                            {
                                ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            else if (curColumn == "Spacer")
            {
                BackColor = GlobalVars.Instance.GridSpacerColor;
            }

            e.Appearance.ForeColor = ForeColor;
            e.Appearance.BackColor = BackColor;
            if (FontBold)
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            else
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
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

        private void UpdateColumnOrder()
        {
            InEditMode = true;
            dgLTQuotes.BeginUpdate();
            int TotalColumns = 13;

            foreach (GridColumn curColumn in dgLTQuotes.Columns)
            {
                //if (curColumn.Name.Contains("ChangeMTD")) { curColumn.BestFit(); }
                if (curColumn.Name.Contains("Quote")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 1); }
                if (curColumn.Name.Contains("Last")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 2); }
                if (curColumn.Name.Contains("Change")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 3); }
                if (curColumn.Name.Contains("BidSize")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 4); }
                if (curColumn.Name.Contains("Bid")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 5); }
                if (curColumn.Name.Contains("Ask")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 6); }
                if (curColumn.Name.Contains("AskSize")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 7); }
                if (curColumn.Name.Contains("ChangeMTD")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 8); }
                if (curColumn.Name.Contains("ChangeYTD")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 9); }
                if (curColumn.Name.Contains("Spacer")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 10); }
                if (curColumn.Name.Contains("AucCond")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 11); }
                if (curColumn.Name.Contains("AucLast")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 12); }
                if (curColumn.Name.Contains("AucChange")) { curColumn.VisibleIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 13); }

                if (curColumn.Name.Contains("Bid")) { if (chkBidAsk.Checked) curColumn.Visible = true; else curColumn.Visible = false; }
                if (curColumn.Name.Contains("Ask")) { if (chkBidAsk.Checked) curColumn.Visible = true; else curColumn.Visible = false; }
                if (curColumn.Name.Contains("BidSize")) { if (chkBidAskSize.Checked) curColumn.Visible = true; else curColumn.Visible = false; }
                if (curColumn.Name.Contains("AskSize")) { if (chkBidAskSize.Checked) curColumn.Visible = true; else curColumn.Visible = false; }
                if (curColumn.Name.Contains("ChangeMTD")) { if (chkMTDChange.Checked) curColumn.Visible = true; else curColumn.Visible = false; }
                if (curColumn.Name.Contains("ChangeYTD")) { if (chkYTDChange.Checked) curColumn.Visible = true; else curColumn.Visible = false; }

                if (curColumn.Name.Contains("AucCond")) { curColumn.Visible = false; }
                if (curColumn.Name.Contains("AucLast")) { curColumn.Visible = false; }
                if (curColumn.Name.Contains("AucChange")) { curColumn.Visible = false; }
            }

            if (chkBidAsk.Checked && chkBidAskSize.Checked)
            {
                tmrUpdate.Interval = 600;
            }
            else
            {
                tmrUpdate.Interval = 350;
            }

            dgLTQuotes.EndUpdate();
            InEditMode = false;
        }

        GridHitInfo downHitInfo = null;

        private void dgLTQuotes_MouseDown(object sender, MouseEventArgs e)
        {
            flgMouseDown = true;

            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                downHitInfo = hitInfo;
        }

        private void dgLTQuotes_MouseMove(object sender, MouseEventArgs e)
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
                        string IdTicker = "DRAGITEM\t" + dgLTQuotes.GetRowCellValue(downHitInfo.RowHandle, curCol.Replace("col", "")).ToString();
                        view.GridControl.DoDragDrop(IdTicker, DragDropEffects.Move);

                        downHitInfo = null;
                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
                flgMouseDown = false;
            }
        }

        private void dgLTQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            flgMouseDown = false;
        }

        private void dgLTQuotes_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);

            if (info.HitTest == GridHitTest.RowCell && !InEditMode && ((GridView)sender).FocusedValue.ToString() != "" && ((GridView)sender).FocusedColumn.Name.Contains("Quote"))
            {
                if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                {
                    if (dgLTQuotes.FocusedColumn.Name.Contains("Quote"))
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        string curTicker = dgLTQuotes.FocusedValue.ToString();

                        if (curTicker != "")
                        {
                            frmOptionChain newOptionChain = new frmOptionChain(curTicker);
                            ConnectedForms.Add(newOptionChain);
                            newOptionChain.ClientClosedForm += new EventHandler(ClientFormClosed);
                            newOptionChain.Show();
                        }

                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
                    string curValue = ((GridView)sender).FocusedValue.ToString();
                    frmLevel2 curFrmLevel2 = new frmLevel2(curValue);
                    ConnectedForms.Add(curFrmLevel2);
                    curFrmLevel2.Show();
                    curFrmLevel2.ClientClosedForm += new EventHandler(ClientFormClosed);
                    //if (curValue.IndexOf('.') == -1) curValue = curValue + ".SA";
                    curFrmLevel2.txtTicker.Text = curValue;
                    curFrmLevel2.cmdRequest_Click(this, new EventArgs());
                    curFrmLevel2.LoadOrders();
                    //e.Cancel = true; 
                }
            }
        }

        private void dgLTQuotes_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!InEditMode) e.Cancel = true;
        }

        bool PendingInsert = false;

        private void FIXUpdateReceived(object sender, EventArgs e)
        {
            PendingInsert = true;
        }

        private void InsertExecutions()
        {
            try
            {
                using (newNestConn curConn = new newNestConn())
                {
                    foreach (NFIXConnLT.OrderLT curOrder in FIXConnections.Instance.curFixConn.OrderList)
                    {
                        if (!curOrder.Inserted)
                        {
                            string tempIdOrder = curConn.Execute_Query_String("SELECT [Id_Order_Broker] FROM [NESTDB].[dbo].[Tb012_Ordens] WHERE [Id_Order_Broker]='" + curOrder.OrderID.getValue() + "' AND Data_Abert_Ordem='" + DateTime.Now.ToString("yyyy-MM-dd") + "'");
                            //string tempIdOrder = curConn.Execute_Query_String("SELECT [Id_Order_Broker] FROM [NESTDB].[dbo].[Tb012_Ordens] WHERE [Id_Order_Broker]='" + curOrder.OrderID.getValue() + "'");

                            if (tempIdOrder != "")
                            {
                                FIXConnections.Instance.curFixConn.UpdateOrderDatabaseID(curOrder.OrderID.getValue(), tempIdOrder);
                            }
                            else
                            {
                                int tempSide = 1;
                                if (curOrder.NetDone < 0) tempSide = 2;

                                int tempIdSecurity = curOrder.IdSecurity;

                                if (tempIdSecurity == 0)
                                {
                                    tempIdSecurity = curConn.Return_Int("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities WHERE NestTicker='" + curOrder.strSymbol + "'");
                                }

                                if (!curOrder.OrderID.getValue().Contains("NEW_") && curOrder.Done > 0)
                                {
                                    Int32 iID_Account = 0;
                                    Int32 iID_Broker = 0;

                                    string sSQLConcat = curConn.Execute_Query_String("SELECT convert(varchar, Id_Account) + ';' + convert(varchar, id_Broker)  as sConcat FROM [NESTDB].[dbo].[Tb007_Accounts] WHERE [Account_Number] LIKE '" + curOrder.Account + "%'");

                                    if (sSQLConcat.Length > 0 || curOrder.Account.getValue().Contains("99914"))
                                    {
                                        if (curOrder.Account.getValue().Contains("99914"))
                                        {
                                            iID_Account = 1204;
                                            iID_Broker = 22;
                                        }
                                        else
                                        {
                                            // Separa campos retornados do banco
                                            string[] sFields = null;
                                            sFields = sSQLConcat.Split(';');
                                            iID_Account = int.Parse(sFields[0]);
                                            iID_Broker = int.Parse(sFields[1]);
                                        }

                                        string SQLString = " EXEC proc_insert_Tb012_Ordens " + tempIdSecurity + ", " + curOrder.NetDone + ", " + curOrder.ExecPrice.ToString().Replace(",", ".") + ", " + (curOrder.NetDone * curOrder.ExecPrice).ToString().Replace(",", ".") + ", " + curOrder.IdBook + ", " +
                                                        " " + curOrder.IdSection + ", 1, " + NestDLL.NUserControl.Instance.User_Id + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', 0, " + iID_Broker + ", '" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + iID_Account + ", '" + curOrder.OrderID + "', 0," + tempSide;

                                        string tempVal = curConn.Execute_Query_String(SQLString);
                                        tempIdOrder = curConn.Execute_Query_String("SELECT @@IDENTITY");

                                        if (tempIdOrder != "")
                                            FIXConnections.Instance.curFixConn.UpdateOrderDatabaseID(curOrder.OrderID.getValue(), tempIdOrder);
                                    }
                                }
                            }
                        }
                    }

                    foreach (NFIXConnLT.ExecLT curExec in FIXConnections.Instance.curFixConn.ExecList)
                    {
                        if (!curExec.Inserted && curExec.LastShares != 0)
                        {
                            string tempIdExec = curConn.Execute_Query_String("SELECT [ExecID] FROM [NESTDB].[dbo].[Tb013_Trades] WHERE ExecID='" + curExec.ExecID + "'");

                            if (tempIdExec != "")
                            {
                                FIXConnections.Instance.curFixConn.UpdateExecDatabaseID(curExec.ExecID, tempIdExec);
                            }
                            else
                            {
                                string sSQLConcat = curConn.Execute_Query_String("SELECT convert(varchar, Id_Ordem) + ';' + convert(varchar, idSide)   + ';' + convert(varchar, Id_Corretora)  as sConcat FROM [NESTDB].[dbo].[Tb012_Ordens] WHERE [Id_Order_Broker] = '" + curExec.OrderID.getValue() + "'");

                                if (sSQLConcat.Length > 0)
                                {
                                    // Separa campos retornados do banco
                                    string[] sFields = null;
                                    sFields = sSQLConcat.Split(';');
                                    string tempIdOrder = sFields[0];
                                    string sIdSide = sFields[1];
                                    string sID_Corretora = sFields[2];

                                    double dRoundLot = curConn.Return_Double("SELECT RoundLot FROM Tb012_Ordens, FCN001_Securities_All('" + DateTime.Now.ToString("yyyy-MM-dd") + "') WHERE Data_Abert_Ordem = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND id_Ativo = idSecurity AND id_ordem = " + tempIdOrder);
                                    if (dRoundLot <= 0) dRoundLot = 1;

                                    int AdjFactor = 1;
                                    if (sIdSide == "2") AdjFactor = -1;

                                    string SQLString = " EXEC sp_insert_Tb013_Trades_FIX " + tempIdOrder + "," + (curExec.LastShares * AdjFactor) + "," + curExec.LastPrice.ToString().Replace(",", ".") + "," + (curExec.LastShares * curExec.LastPrice * AdjFactor / dRoundLot).ToString().Replace(",", ".") +
                                    ", " + sID_Corretora + "," + NestDLL.NUserControl.Instance.User_Id + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "',1, '" + curExec.ExecID + "'";

                                    string tempVal = curConn.Execute_Query_String(SQLString);

                                    tempIdExec = curConn.Execute_Query_String("SELECT @@IDENTITY");

                                    FIXConnections.Instance.curFixConn.UpdateExecDatabaseID(curExec.ExecID, tempIdExec);
                                }
                            }
                        }
                    }
                }
                labNotInserted.Visible = false;
            }
            catch (Exception e)
            {
                labNotInserted.Visible = true;
            }
        }

        class LTLine
        {
            double _Close1 = 0;
            double _Close2 = 0;
            double _Close3 = 0;
            double _Close4 = 0;
            double _Close5 = 0;
            double _Close6 = 0;
            double _Close7 = 0;

            public double _MTD_TRPrice1 = 0;
            public double _MTD_TRPrice2 = 0;
            public double _MTD_TRPrice3 = 0;
            public double _MTD_TRPrice4 = 0;
            public double _MTD_TRPrice5 = 0;
            public double _MTD_TRPrice6 = 0;
            public double _MTD_TRPrice7 = 0;

            public double _YTD_TRPrice1 = 0;
            public double _YTD_TRPrice2 = 0;
            public double _YTD_TRPrice3 = 0;
            public double _YTD_TRPrice4 = 0;
            public double _YTD_TRPrice5 = 0;
            public double _YTD_TRPrice6 = 0;
            public double _YTD_TRPrice7 = 0;

            public double _Prev_TRPrice1 = 0;
            public double _Prev_TRPrice2 = 0;
            public double _Prev_TRPrice3 = 0;
            public double _Prev_TRPrice4 = 0;
            public double _Prev_TRPrice5 = 0;
            public double _Prev_TRPrice6 = 0;
            public double _Prev_TRPrice7 = 0;

            //1 - IdSecurity1
            //2 - Quote1
            //3 - Last1
            //4 - Change1
            //5 - BidSize1
            //6 - Bid1
            //7 - Ask1
            //8 - AskSize1
            //9 - ChangeMTD1
            //10 - ChangeYTD1
            //11 - Spacer1
            //12 - AucCond1
            //13 - AucLast1
            //14 - AucChange1

            double _IdSecurity1 = 0; public double IdSecurity1 { get { return _IdSecurity1; } set { _IdSecurity1 = value; } }
            string _Quote1 = ""; public string Quote1 { get { return _Quote1; } set { _Quote1 = value; } }
            double _Last1 = 0; public double Last1 { get { if (_Last1 != 0) { return (_Last1); } else { return _Close1; }; } }
            public double Change1 { get { if (_Close1 != 0 && _Last1 != 0) { if (!_Quote1.Contains("DI1")) return (_Last1 / _Close1 - 1) + 0.00001; else return (_Last1 - _Close1); } else { return 0; }; } }
            double _BidSize1 = 0; public double BidSize1 { get { return _BidSize1; } set { _BidSize1 = value; } }
            double _Bid1 = 0; public double Bid1 { get { return _Bid1; } set { _Bid1 = value; } }
            double _Ask1 = 0; public double Ask1 { get { return _Ask1; } set { _Ask1 = value; } }
            double _AskSize1 = 0; public double AskSize1 { get { return _AskSize1; } set { _AskSize1 = value; } }
            public double ChangeMTD1 { get { if (_Prev_TRPrice1 != 0 && !double.IsNaN(_Prev_TRPrice1)) return (_Prev_TRPrice1 / _MTD_TRPrice1) * (1 + Change1) - 1; else return 0; } }
            public double ChangeYTD1 { get { if (_Prev_TRPrice1 != 0 && !double.IsNaN(_Prev_TRPrice1)) return (_Prev_TRPrice1 / _YTD_TRPrice1) * (1 + Change1) - 1; else return 0; } }
            public string Spacer1 { get { return "  "; } }
            TradingStatusType _AucCond1 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond1 { get { return _AucCond1; } set { _AucCond1 = value; } }
            double _AucLast1 = 0; public double AucLast1 { get { return _AucLast1; } set { _AucLast1 = value; } }
            double _AucChange1 = 0; public double AucChange1 { get { if (Last1 != 0) return _AucLast1 / Last1 - 1; else return 0; } set { _AucChange1 = value; } }

            double _IdSecurity2 = 0; public double IdSecurity2 { get { return _IdSecurity2; } set { _IdSecurity2 = value; } }
            string _Quote2 = ""; public string Quote2 { get { return _Quote2; } set { _Quote2 = value; } }
            double _Last2 = 0; public double Last2 { get { if (_Last2 != 0) { return (_Last2); } else { return _Close2; }; } }
            public double Change2 { get { if (_Close2 != 0 && _Last2 != 0) { if (!_Quote2.Contains("DI1")) return (_Last2 / _Close2 - 1) + 0.00001; else return (_Last2 - _Close2); } else { return 0; }; } }
            double _BidSize2 = 0; public double BidSize2 { get { return _BidSize2; } set { _BidSize2 = value; } }
            double _Bid2 = 0; public double Bid2 { get { return _Bid2; } set { _Bid2 = value; } }
            double _Ask2 = 0; public double Ask2 { get { return _Ask2; } set { _Ask2 = value; } }
            double _AskSize2 = 0; public double AskSize2 { get { return _AskSize2; } set { _AskSize2 = value; } }
            public double ChangeMTD2 { get { if (_Prev_TRPrice2 != 0 && !double.IsNaN(_Prev_TRPrice2)) return (_Prev_TRPrice2 / _MTD_TRPrice2) * (1 + Change2) - 1; else return 0; } }
            public double ChangeYTD2 { get { if (_Prev_TRPrice2 != 0 && !double.IsNaN(_Prev_TRPrice2)) return (_Prev_TRPrice2 / _YTD_TRPrice2) * (1 + Change2) - 1; else return 0; } }
            public string Spacer2 { get { return "  "; } }
            TradingStatusType _AucCond2 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond2 { get { return _AucCond2; } set { _AucCond2 = value; } }
            double _AucLast2 = 0; public double AucLast2 { get { return _AucLast2; } set { _AucLast2 = value; } }
            double _AucChange2 = 0; public double AucChange2 { get { if (Last2 != 0) return _AucLast2 / Last2 - 1; else return 0; } set { _AucChange2 = value; } }

            double _IdSecurity3 = 0; public double IdSecurity3 { get { return _IdSecurity3; } set { _IdSecurity3 = value; } }
            string _Quote3 = ""; public string Quote3 { get { return _Quote3; } set { _Quote3 = value; } }
            double _Last3 = 0; public double Last3 { get { if (_Last3 != 0) { return (_Last3); } else { return _Close3; }; } }
            public double Change3 { get { if (_Close3 != 0 && _Last3 != 0) { if (!_Quote3.Contains("DI1")) return (_Last3 / _Close3 - 1) + 0.00001; else return (_Last3 - _Close3); } else { return 0; }; } }
            double _BidSize3 = 0; public double BidSize3 { get { return _BidSize3; } set { _BidSize3 = value; } }
            double _Bid3 = 0; public double Bid3 { get { return _Bid3; } set { _Bid3 = value; } }
            double _Ask3 = 0; public double Ask3 { get { return _Ask3; } set { _Ask3 = value; } }
            double _AskSize3 = 0; public double AskSize3 { get { return _AskSize3; } set { _AskSize3 = value; } }
            public double ChangeMTD3 { get { if (_Prev_TRPrice3 != 0 && !double.IsNaN(_Prev_TRPrice3)) return (_Prev_TRPrice3 / _MTD_TRPrice3) * (1 + Change3) - 1; else return 0; } }
            public double ChangeYTD3 { get { if (_Prev_TRPrice3 != 0 && !double.IsNaN(_Prev_TRPrice3)) return (_Prev_TRPrice3 / _YTD_TRPrice3) * (1 + Change3) - 1; else return 0; } }
            public string Spacer3 { get { return "  "; } }
            TradingStatusType _AucCond3 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond3 { get { return _AucCond3; } set { _AucCond3 = value; } }
            double _AucLast3 = 0; public double AucLast3 { get { return _AucLast3; } set { _AucLast3 = value; } }
            double _AucChange3 = 0; public double AucChange3 { get { if (Last3 != 0) return _AucLast3 / Last3 - 1; else return 0; } set { _AucChange3 = value; } }

            double _IdSecurity4 = 0; public double IdSecurity4 { get { return _IdSecurity4; } set { _IdSecurity4 = value; } }
            string _Quote4 = ""; public string Quote4 { get { return _Quote4; } set { _Quote4 = value; } }
            double _Last4 = 0; public double Last4 { get { if (_Last4 != 0) { return (_Last4); } else { return _Close4; }; } }
            public double Change4 { get { if (_Close4 != 0 && _Last4 != 0) { if (!_Quote4.Contains("DI1")) return (_Last4 / _Close4 - 1) + 0.00001; else return (_Last4 - _Close4); } else { return 0; }; } }
            double _BidSize4 = 0; public double BidSize4 { get { return _BidSize4; } set { _BidSize4 = value; } }
            double _Bid4 = 0; public double Bid4 { get { return _Bid4; } set { _Bid4 = value; } }
            double _Ask4 = 0; public double Ask4 { get { return _Ask4; } set { _Ask4 = value; } }
            double _AskSize4 = 0; public double AskSize4 { get { return _AskSize4; } set { _AskSize4 = value; } }
            public double ChangeMTD4 { get { if (_Prev_TRPrice4 != 0 && !double.IsNaN(_Prev_TRPrice4)) return (_Prev_TRPrice4 / _MTD_TRPrice4) * (1 + Change4) - 1; else return 0; } }
            public double ChangeYTD4 { get { if (_Prev_TRPrice4 != 0 && !double.IsNaN(_Prev_TRPrice4)) return (_Prev_TRPrice4 / _YTD_TRPrice4) * (1 + Change4) - 1; else return 0; } }
            public string Spacer4 { get { return "  "; } }
            TradingStatusType _AucCond4 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond4 { get { return _AucCond4; } set { _AucCond4 = value; } }
            double _AucLast4 = 0; public double AucLast4 { get { return _AucLast4; } set { _AucLast4 = value; } }
            double _AucChange4 = 0; public double AucChange4 { get { if (Last4 != 0) return _AucLast4 / Last4 - 1; else return 0; } set { _AucChange4 = value; } }

            double _IdSecurity5 = 0; public double IdSecurity5 { get { return _IdSecurity5; } set { _IdSecurity5 = value; } }
            string _Quote5 = ""; public string Quote5 { get { return _Quote5; } set { _Quote5 = value; } }
            double _Last5 = 0; public double Last5 { get { if (_Last5 != 0) { return (_Last5); } else { return _Close5; }; } }
            public double Change5 { get { if (_Close5 != 0 && _Last5 != 0) { if (!_Quote5.Contains("DI1")) return (_Last5 / _Close5 - 1) + 0.00001; else return (_Last5 - _Close5); } else { return 0; }; } }
            double _BidSize5 = 0; public double BidSize5 { get { return _BidSize5; } set { _BidSize5 = value; } }
            double _Bid5 = 0; public double Bid5 { get { return _Bid5; } set { _Bid5 = value; } }
            double _Ask5 = 0; public double Ask5 { get { return _Ask5; } set { _Ask5 = value; } }
            double _AskSize5 = 0; public double AskSize5 { get { return _AskSize5; } set { _AskSize5 = value; } }
            public double ChangeMTD5 { get { if (_Prev_TRPrice5 != 0 && !double.IsNaN(_Prev_TRPrice5)) return (_Prev_TRPrice5 / _MTD_TRPrice5) * (1 + Change5) - 1; else return 0; } }
            public double ChangeYTD5 { get { if (_Prev_TRPrice5 != 0 && !double.IsNaN(_Prev_TRPrice5)) return (_Prev_TRPrice5 / _YTD_TRPrice5) * (1 + Change5) - 1; else return 0; } }
            public string Spacer5 { get { return "  "; } }
            TradingStatusType _AucCond5 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond5 { get { return _AucCond5; } set { _AucCond5 = value; } }
            double _AucLast5 = 0; public double AucLast5 { get { return _AucLast5; } set { _AucLast5 = value; } }
            double _AucChange5 = 0; public double AucChange5 { get { if (Last5 != 0) return _AucLast5 / Last5 - 1; else return 0; } set { _AucChange5 = value; } }

            double _IdSecurity6 = 0; public double IdSecurity6 { get { return _IdSecurity6; } set { _IdSecurity6 = value; } }
            string _Quote6 = ""; public string Quote6 { get { return _Quote6; } set { _Quote6 = value; } }
            double _Last6 = 0; public double Last6 { get { if (_Last6 != 0) { return (_Last6); } else { return _Close6; }; } }
            public double Change6 { get { if (_Close6 != 0 && _Last6 != 0) { if (!_Quote6.Contains("DI1")) return (_Last6 / _Close6 - 1) + 0.00001; else return (_Last6 - _Close6); } else { return 0; }; } }
            double _BidSize6 = 0; public double BidSize6 { get { return _BidSize6; } set { _BidSize6 = value; } }
            double _Bid6 = 0; public double Bid6 { get { return _Bid6; } set { _Bid6 = value; } }
            double _Ask6 = 0; public double Ask6 { get { return _Ask6; } set { _Ask6 = value; } }
            double _AskSize6 = 0; public double AskSize6 { get { return _AskSize6; } set { _AskSize6 = value; } }
            public double ChangeMTD6 { get { if (_Prev_TRPrice6 != 0 && !double.IsNaN(_Prev_TRPrice6)) return (_Prev_TRPrice6 / _MTD_TRPrice6) * (1 + Change6) - 1; else return 0; } }
            public double ChangeYTD6 { get { if (_Prev_TRPrice6 != 0 && !double.IsNaN(_Prev_TRPrice6)) return (_Prev_TRPrice6 / _YTD_TRPrice6) * (1 + Change6) - 1; else return 0; } }
            public string Spacer6 { get { return "  "; } }
            TradingStatusType _AucCond6 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond6 { get { return _AucCond6; } set { _AucCond6 = value; } }
            double _AucLast6 = 0; public double AucLast6 { get { return _AucLast6; } set { _AucLast6 = value; } }
            double _AucChange6 = 0; public double AucChange6 { get { if (Last6 != 0) return _AucLast6 / Last6 - 1; else return 0; } set { _AucChange6 = value; } }

            double _IdSecurity7 = 0; public double IdSecurity7 { get { return _IdSecurity7; } set { _IdSecurity7 = value; } }
            string _Quote7 = ""; public string Quote7 { get { return _Quote7; } set { _Quote7 = value; } }
            double _Last7 = 0; public double Last7 { get { if (_Last7 != 0) { return (_Last7); } else { return _Close7; }; } }
            public double Change7 { get { if (_Close7 != 0 && _Last7 != 0) { if (!_Quote7.Contains("DI1")) return (_Last7 / _Close7 - 1) + 0.00001; else return (_Last7 - _Close7); } else { return 0; }; } }
            double _BidSize7 = 0; public double BidSize7 { get { return _BidSize7; } set { _BidSize7 = value; } }
            double _Bid7 = 0; public double Bid7 { get { return _Bid7; } set { _Bid7 = value; } }
            double _Ask7 = 0; public double Ask7 { get { return _Ask7; } set { _Ask7 = value; } }
            double _AskSize7 = 0; public double AskSize7 { get { return _AskSize7; } set { _AskSize7 = value; } }
            public double ChangeMTD7 { get { if (_Prev_TRPrice7 != 0 && !double.IsNaN(_Prev_TRPrice7)) return (_Prev_TRPrice7 / _MTD_TRPrice7) * (1 + Change7) - 1; else return 0; } }
            public double ChangeYTD7 { get { if (_Prev_TRPrice7 != 0 && !double.IsNaN(_Prev_TRPrice7)) return (_Prev_TRPrice7 / _YTD_TRPrice7) * (1 + Change7) - 1; else return 0; } }
            public string Spacer7 { get { return "  "; } }
            TradingStatusType _AucCond7 = TradingStatusType.UNKNOWN; public TradingStatusType AucCond7 { get { return _AucCond7; } set { _AucCond7 = value; } }
            double _AucLast7 = 0; public double AucLast7 { get { return _AucLast7; } set { _AucLast7 = value; } }
            double _AucChange7 = 0; public double AucChange7 { get { if (Last7 != 0) return _AucLast7 / Last7 - 1; else return 0; } set { _AucChange7 = value; } }

            public void SetLast1(double curValue) { _Last1 = curValue; }
            public void SetClose1(double curValue) { _Close1 = curValue; }

            public void SetLast2(double curValue) { _Last2 = curValue; }
            public void SetClose2(double curValue) { _Close2 = curValue; }

            public void SetLast3(double curValue) { _Last3 = curValue; }
            public void SetClose3(double curValue) { _Close3 = curValue; }

            public void SetLast4(double curValue) { _Last4 = curValue; }
            public void SetClose4(double curValue) { _Close4 = curValue; }

            public void SetLast5(double curValue) { _Last5 = curValue; }
            public void SetClose5(double curValue) { _Close5 = curValue; }

            public void SetLast6(double curValue) { _Last6 = curValue; }
            public void SetClose6(double curValue) { _Close6 = curValue; }

            public void SetLast7(double curValue) { _Last7 = curValue; }
            public void SetClose7(double curValue) { _Close7 = curValue; }

        }

        private void dgLTQuotes_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.SaveGridColumns(dgLTQuotes, this.Text);
        }

        private void cmdLimits_Click(object sender, EventArgs e)
        {
            if (curLimits == null || curLimits.IsDisposed)
            {
                curLimits = new frmLimits();
            }

            curLimits.Show();
        }

        

        private void OpenLevel2()
        {
            if (FIXConnections.Instance.curFixConn == null)
                return;

            string sFile = @"C:\LiveTrade\LTConfigDefaultOpenSecurity.csv";

            if (!File.Exists(sFile))
                return;

            frmOrderReview curFrmOrderReview = new frmOrderReview();
            curFrmOrderReview.Show();

            frmPnL curPnL = new frmPnL();
            curPnL.Show();

            StreamReader sr = new StreamReader(sFile);
            string tempLine = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curLine = tempLine.Split(';');

                frmLevel2 curFrmLevel2 = new frmLevel2(curLine[0]);
                ConnectedForms.Add(curFrmLevel2);
                curFrmLevel2.Show();
                curFrmLevel2.ClientClosedForm += new EventHandler(ClientFormClosed);
                //if (curValue.IndexOf('.') == -1) curValue = curValue + ".SA";
                curFrmLevel2.txtTicker.Text = curLine[0];
                curFrmLevel2.cmdRequest_Click(this, new EventArgs());
                curFrmLevel2.LoadOrders();
            }

            bOpenScreen = true;

        }

        private void optCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (optCheck.Checked)
            {
                optAgg.Checked = false;
                GlobalVars.Instance.CheckOrAGG = 1;
            }
            else
            {
                optCheck.Checked = false;
                optAgg.Checked = true;
                GlobalVars.Instance.CheckOrAGG = 0;
            }
        }

        private void lblFIXSentGross_Click(object sender, EventArgs e)
        {

        }
    }
}