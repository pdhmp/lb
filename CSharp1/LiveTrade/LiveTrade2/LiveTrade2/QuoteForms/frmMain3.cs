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
    public partial class frmMain3 : ConnectedForm
    {
        BindingSource bndDataSource = new BindingSource();
        BindingSource bndDataSourceL2 = new BindingSource();

        public int OpenMode = 0;
        
        bool InEditMode = false;

        //NDistConn curNDistConn = new NDistConn();

        List<PhytonRunner> SORunnerList = new List<PhytonRunner>();
        List<LTLine> LTLineList = new List<LTLine>();

        List<ConnectedForm> ConnectedForms = new List<ConnectedForm>();

        frmOrderReview curOrderReview;
        frmAuction curAuction;
        frmQuickChart curQuickChart;

        bool flgMouseDown = false;

        Queue<UpdateItem> UpdateQueue = new Queue<UpdateItem>();

        bool[,] updateItems = new bool[60, 8];
       
        class UpdateItem
        {
            public int RowHandle;
            public int ColPos;

            public UpdateItem(int _RowHandle, int _ColPos)
            {
                RowHandle = _RowHandle;
                ColPos = _ColPos;
            }
        }

        public frmMain3()
        {
            InitializeComponent();
        }

        ~frmMain3()
        {
            curNDistConn.Disconnect();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            //CreateTodayRunner();
            curNDistConn.OnData += new EventHandler(NewMarketData);
            //NestSymConn.NDistConn.Instance.LINKBOVSource = NCommonTypes.NEnuns.NSYMSources.FLEXBSE;// Trocar para NESTBSE para usar o nest bse como fonte do level 2
            //NDistConn.Instance._IncludeL2 = true; // Trocar para true para usar o nest bse como fonte do level 2

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
            }

            cmdEnableTrade_Click(this, new EventArgs());

            LoadTickers();

            Thread runThread = new Thread(LoadTickerInfo);
            runThread.Start();

            dgLTQuotes.DataSource = LTLineList;

            //dgLTQuotes.DefaultCellStyle.ForeColor = Color.Lime;
            //dgLTQuotes.DefaultCellStyle.BackColor = Color.Black ;
            dgLTQuotes.GridColor = Color.Black;

            foreach (DataGridViewRow curRow in dgLTQuotes.Rows)
            {
                curRow.Height = 14;
            }

            int curIndex = 0;

            foreach (DataGridViewTextBoxColumn curColumn in dgLTQuotes.Columns)
            {
                curColumn.DisplayIndex = curIndex++;
                if (curColumn.Name.Contains("Spacer")) { curColumn.HeaderText = " "; } else { curColumn.HeaderText = curColumn.Name.Substring(0, curColumn.Name.Length -1); }
                if (curColumn.Name.Contains("Last")) { curColumn.DefaultCellStyle.Format = "#,##0.00;(#,##0.00);\\ "; curColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; }
                if (curColumn.Name.Contains("Change")) { curColumn.DefaultCellStyle.Format = "#,##0.00%;-#,##0.00%;0.00%\\ "; curColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; }
                if (curColumn.Name.Contains("Bid")) { curColumn.DefaultCellStyle.Format = "#,##0.00;(#,##0.00);\\ "; }
                if (curColumn.Name.Contains("Ask")) { curColumn.DefaultCellStyle.Format = "#,##0.00;(#,##0.00);\\ "; curColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; }
                if (curColumn.Name.Contains("BidSize")) { curColumn.DefaultCellStyle.Format = "#,##0;(#,##0);\\ "; }
                if (curColumn.Name.Contains("AskSize")) { curColumn.DefaultCellStyle.Format = "#,##0;(#,##0);\\ "; curColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; }
                if (curColumn.Name.Contains("IdSecurity")) curColumn.Visible = false;
                if (curColumn.Name.Contains("AucCond")) curColumn.Visible = false;
                if (curColumn.Name.Contains("AucLast")) curColumn.Visible = false;
                if (curColumn.Name.Contains("AucChange")) curColumn.Visible = false;
                //curColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            //dgLTQuotes.BestFitColumns();

            curNDistConn.Connect();

            chkMTDChange.Checked = false;
            chkYTDChange.Checked = false;
            chkBidAsk.Checked = false;
            chkBidAskSize.Checked = false;

            tmrUpdate.Start();
            tmrBestFitCols.Start();
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
                if (curQuote.Ticker == "")
                {
                    return;
                }

                if (curQuote.FLID == NestFLIDS.UpdateTime)
                {
                    DateTime curUpdTime = new DateTime(1970, 1, 1, 0, 0, 0).Add(TimeSpan.FromSeconds(curQuote.ValueDouble));
                    if (curUpdTime > GlobalVars.Instance.LastQuoteReceived) GlobalVars.Instance.LastQuoteReceived = curUpdTime.AddHours(-3);
                }

                if (!(curQuote.FLID == NestFLIDS.Last && curQuote.ValueDouble == 0))
                {
                    if (curQuote.FLID == NestFLIDS.Last)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            LTLine curItem = LTLineList[i];
                            if (curItem.Quote1 == curQuote.Ticker) { curItem.SetLast1(curQuote.ValueDouble); updateItems[i, 1] = true; }
                            if (curItem.Quote2 == curQuote.Ticker) { curItem.SetLast2(curQuote.ValueDouble); updateItems[i, 2] = true; }
                            if (curItem.Quote3 == curQuote.Ticker) { curItem.SetLast3(curQuote.ValueDouble); updateItems[i, 3] = true; }
                            if (curItem.Quote4 == curQuote.Ticker) { curItem.SetLast4(curQuote.ValueDouble); updateItems[i, 4] = true; }
                            if (curItem.Quote5 == curQuote.Ticker) { curItem.SetLast5(curQuote.ValueDouble); updateItems[i, 5] = true; }
                            if (curItem.Quote6 == curQuote.Ticker) { curItem.SetLast6(curQuote.ValueDouble); updateItems[i, 6] = true; }
                            if (curItem.Quote7 == curQuote.Ticker) { curItem.SetLast7(curQuote.ValueDouble); updateItems[i, 7] = true; }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.Close)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            if (curQuote.ValueDouble != 0)
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.SetClose1(curQuote.ValueDouble); updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.SetClose2(curQuote.ValueDouble); updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.SetClose3(curQuote.ValueDouble); updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.SetClose4(curQuote.ValueDouble); updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.SetClose5(curQuote.ValueDouble); updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.SetClose6(curQuote.ValueDouble); updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.SetClose7(curQuote.ValueDouble); updateItems[i, 7] = true; }
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
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.Bid1 = curQuote.ValueDouble; updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.Bid2 = curQuote.ValueDouble; updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.Bid3 = curQuote.ValueDouble; updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.Bid4 = curQuote.ValueDouble; updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.Bid5 = curQuote.ValueDouble; updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.Bid6 = curQuote.ValueDouble; updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.Bid7 = curQuote.ValueDouble; updateItems[i, 7] = true; }
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
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.Ask1 = curQuote.ValueDouble; updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.Ask2 = curQuote.ValueDouble; updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.Ask3 = curQuote.ValueDouble; updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.Ask4 = curQuote.ValueDouble; updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.Ask5 = curQuote.ValueDouble; updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.Ask6 = curQuote.ValueDouble; updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.Ask7 = curQuote.ValueDouble; updateItems[i, 7] = true; }
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
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.BidSize1 = curQuote.ValueDouble; updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.BidSize2 = curQuote.ValueDouble; updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.BidSize3 = curQuote.ValueDouble; updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.BidSize4 = curQuote.ValueDouble; updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.BidSize5 = curQuote.ValueDouble; updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.BidSize6 = curQuote.ValueDouble; updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.BidSize7 = curQuote.ValueDouble; updateItems[i, 7] = true; }
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
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.AskSize1 = curQuote.ValueDouble; updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.AskSize2 = curQuote.ValueDouble; updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.AskSize3 = curQuote.ValueDouble; updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.AskSize4 = curQuote.ValueDouble; updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.AskSize5 = curQuote.ValueDouble; updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.AskSize6 = curQuote.ValueDouble; updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.AskSize7 = curQuote.ValueDouble; updateItems[i, 7] = true; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.TradingStatus)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.AucCond1 = curQuote.ValueString; updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.AucCond2 = curQuote.ValueString; updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.AucCond3 = curQuote.ValueString; updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.AucCond4 = curQuote.ValueString; updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.AucCond5 = curQuote.ValueString; updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.AucCond6 = curQuote.ValueString; updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.AucCond7 = curQuote.ValueString; updateItems[i, 7] = true; }
                            }
                        }
                    }
                    else if (curQuote.FLID == NestFLIDS.AucLast)
                    {
                        for (int i = 0; i < LTLineList.Count; i++)
                        {
                            {
                                LTLine curItem = LTLineList[i];
                                if (curItem.Quote1 == curQuote.Ticker) { curItem.AucLast1 = curQuote.ValueDouble; updateItems[i, 1] = true; }
                                if (curItem.Quote2 == curQuote.Ticker) { curItem.AucLast2 = curQuote.ValueDouble; updateItems[i, 2] = true; }
                                if (curItem.Quote3 == curQuote.Ticker) { curItem.AucLast3 = curQuote.ValueDouble; updateItems[i, 3] = true; }
                                if (curItem.Quote4 == curQuote.Ticker) { curItem.AucLast4 = curQuote.ValueDouble; updateItems[i, 4] = true; }
                                if (curItem.Quote5 == curQuote.Ticker) { curItem.AucLast5 = curQuote.ValueDouble; updateItems[i, 5] = true; }
                                if (curItem.Quote6 == curQuote.Ticker) { curItem.AucLast6 = curQuote.ValueDouble; updateItems[i, 6] = true; }
                                if (curItem.Quote7 == curQuote.Ticker) { curItem.AucLast7 = curQuote.ValueDouble; updateItems[i, 7] = true; }
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

        private void RedrawCells(int RowHandle, int ColPos)
        {
            dgLTQuotes.InvalidateCell(ColPos * 13 - 5, RowHandle);//Last
            dgLTQuotes.InvalidateCell(ColPos * 13 - 2, RowHandle);//Bid
            dgLTQuotes.InvalidateCell(ColPos * 13 - 1, RowHandle);//Ask
            dgLTQuotes.InvalidateCell(ColPos * 13 - 3, RowHandle);//BidSize
            dgLTQuotes.InvalidateCell(ColPos * 13 - 0, RowHandle);//AskSize
            dgLTQuotes.InvalidateCell(ColPos * 13 + 1, RowHandle);//ChangeMTD
            dgLTQuotes.InvalidateCell(ColPos * 13 + 2, RowHandle);//ChangeYTD
            dgLTQuotes.InvalidateCell(ColPos * 13 - 4, RowHandle);//Change


            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["Last" + ColPos]);
            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["Bid" + ColPos]);
            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["Ask" + ColPos]);
            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["BidSize" + ColPos]);
            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["AskSize" + ColPos]);
            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["Change" + ColPos]);
            //if (dgLTQuotes.Columns["" + ColPos].Visible) dgLTQuotes.RefreshRowCell(dgLTQuotes.GetRowHandle(RowHandle), dgLTQuotes.Columns["ChangeMTD" + ColPos]);
        }

        private void cmdFlushDebug_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(curSORunner.logMessages);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
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
                }
                else
                {
                    lblFIXConnected.Visible = false;
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
                //lblFIXState.Text = "MKT DATA".
                 cmdMKTData.BackColor = Color.Red;
            }


            //lblLastTime.Text = GlobalVars.Instance.LastQuoteReceived.ToString("HH:mm:ss");
            lblLastTime.Text = UpdateQueue.Count.ToString();

            if (!InEditMode && !flgMouseDown)
            {
                //dtgLTQuotes.BeginUpdate();
                for (int i = 0; i < 60; i++)
                {
                    for (int j = 1; j <= 7; j++)
                    {
                        if (updateItems[i, j])
                        {
                            RedrawCells(i, j);
                            updateItems[i, j] = false;
                        }
                    }
                }
                //dtgLTQuotes.EndUpdate();
            }
        }

        private void tmrBestFitCols_Tick(object sender, EventArgs e)
        {
            //dgLTQuotes.BestFitColumns();
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

        private void cmdViewAuction_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (curAuction == null || curAuction.IsDisposed)
            {
                curAuction = new frmAuction();
                ConnectedForms.Add(curAuction);
            }
            curAuction.Show();
            curAuction.BringToFront();
            if (curAuction.WindowState == FormWindowState.Minimized) curAuction.WindowState = FormWindowState.Normal;
            Cursor.Current = Cursors.Default; 
        }

        private void cmdOrderReview_Click(object sender, EventArgs e)
        {
            if (curOrderReview == null || curOrderReview.IsDisposed) curOrderReview = new frmOrderReview();
            curOrderReview.Show();
            curOrderReview.BringToFront();
            if (curOrderReview.WindowState == FormWindowState.Minimized) curOrderReview.WindowState = FormWindowState.Normal;
        }

        private void cmdOptionChain_Click(object sender, EventArgs e)
        {
            //if (dgLTQuotes.FocusedColumn.Name.Contains("Quote"))
            //{
            //    string curTicker = dgLTQuotes.FocusedValue.ToString();

            //    if (curTicker != "")
            //    {
            //        frmOptionChain newOptionChain = new frmOptionChain(curTicker);
            //        ConnectedForms.Add(newOptionChain);
            //        newOptionChain.Show();
            //    }
            //}
        }

        private void cmdChart_Click(object sender, EventArgs e)
        {
            if (curQuickChart == null || curQuickChart.IsDisposed) curQuickChart = new frmQuickChart();
            curQuickChart.curTicker = "PETR4";
            curQuickChart.Show();
            curQuickChart.BringToFront();
            if (curQuickChart.WindowState == FormWindowState.Minimized) curQuickChart.WindowState = FormWindowState.Normal;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            FIXConnections.Instance.curFixConn.cancelAllOrders();
            FIXConnections.Instance.curFixConn.resendAllStatus();
        }

        private void dgQuotes_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //string curColumn = "";
            //Color ForeColor = Color.Lime;
            //Color BackColor = Color.Black;
            //string curAucCond = dgLTQuotes.GetRowCellValue(e.RowHandle, "AucCond" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)).ToString();
            //bool InAuction = false;
            //bool FontBold = false;

            //if (e.Column.Name.Contains("Quote")) curColumn = "Quote";
            //else if (e.Column.Name.Contains("Last")) curColumn = "Last";
            //else if (e.Column.Name.Contains("ChangeMTD")) curColumn = "ChangeMTD";
            //else if (e.Column.Name.Contains("ChangeYTD")) curColumn = "ChangeYTD";
            //else if (e.Column.Name.Length > 8) { if (e.Column.Name.Substring(3, 6) == "Change") curColumn = "Change"; }
            //else if (e.Column.Name.Contains("Spacer")) curColumn = "Spacer";
            

            //if (curAucCond == "K" || curAucCond == "U" || curAucCond == "P") InAuction = true;

            //if (InAuction && curColumn != "Spacer")
            //{
            //    BackColor = Color.Yellow;
            //    ForeColor = Color.Black;
            //}

            //if (e.CellValue.ToString().Trim() != "")
            //{
            //    if (dgLTQuotes.GetRowCellValue(e.RowHandle, "Quote" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)).ToString() == "")
            //    {
            //        e.DisplayText = "";
            //    }
            //    else
            //    {
            //        if (curColumn == "Quote")
            //        {
            //            if (e.CellValue.ToString().Substring(0, 1) == "'")
            //            {
            //                e.DisplayText = e.CellValue.ToString().Substring(1);
            //                FontBold = true;
            //            }
            //            else
            //            {
            //                switch (curAucCond)
            //                {
            //                    case "R": BackColor = Color.LightBlue; ForeColor = Color.Black; break;
            //                    case "G": BackColor = Color.Orange; ForeColor = Color.Black; break;
            //                    case "U":
            //                    case "K":
            //                    case "P": BackColor = Color.Yellow; ForeColor = Color.Black; break;
            //                    default: BackColor = Color.Black; ForeColor = Color.Lime; break;
            //                }
            //            }
            //        }
            //        else if (curColumn == "Last")
            //        {
            //            if (InAuction)
            //            {
            //                e.DisplayText = ((double)dgLTQuotes.GetRowCellValue(e.RowHandle, "AucLast" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1))).ToString("0.00");
            //            }
            //        }
            //        else if (curColumn.Contains("Change"))
            //        {
            //            if (InAuction)
            //            {
            //                e.DisplayText = ((double)dgLTQuotes.GetRowCellValue(e.RowHandle, "AucChange" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1))).ToString("0.00%");
            //            }
            //            else
            //            {
            //                if (dgLTQuotes.GetRowCellValue(e.RowHandle, "Quote" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)).ToString().Length > 3)
            //                {
            //                    if (dgLTQuotes.GetRowCellValue(e.RowHandle, "Quote" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)).ToString().Substring(0, 3) == "DI1")
            //                    {
            //                        e.DisplayText = Convert.ToSingle(e.CellValue).ToString("0.00");
            //                    }
            //                }
            //                if (Convert.ToSingle(e.CellValue) > 0.005)
            //                {
            //                    if (double.Parse(e.CellValue.ToString()) > 0.03 && curColumn.Length < 7) { FontBold = true; }
            //                }
            //                else if (Convert.ToSingle(e.CellValue) < -0.005)
            //                {
            //                    ForeColor = Color.Red;
            //                    if (double.Parse(e.CellValue.ToString()) < -0.03 && curColumn.Length < 7) { FontBold = true; }
            //                }
            //                else if (Convert.ToSingle(e.CellValue) < 0)
            //                {
            //                    ForeColor = Color.Red;
            //                }
                             
            //                //if ((double)e.CellValue == 0)
            //                //{
            //                //    if ((double)dgLTQuotes.GetRowCellValue(e.RowHandle, "Last" + e.Column.Name.Substring(e.Column.Name.Length - 1, 1)) > 0)
            //                //    {
            //                //        e.DisplayText = "0.00%";
            //                //        ForeColor = Color.Lime;
            //                //    }
            //                //}
            //            }
            //        }
            //    }
            //}
            //else if (curColumn == "Spacer")
            //{
            //    BackColor = Color.FromArgb(20, 20, 20);
            //}

            //e.Appearance.ForeColor = ForeColor;
            //e.Appearance.BackColor = BackColor;
            //if (FontBold)
            //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //else
            //    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
        }
        
        //private Rectangle GetColumnBounds(GridColumn column)
        //{
        //    GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
        //    GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
        //    if (colInfo != null)
        //        return colInfo.Bounds;
        //    else
        //        return Rectangle.Empty;
        //}

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (InEditMode)
            {
                cmdEdit.Text = "Edit";
                InEditMode = false;
                //dgLTQuotes.OptionsView.ShowHorzLines = false;
                //dgLTQuotes.OptionsView.ShowVertLines = false;
            }
            else
            {
                cmdEdit.Text = "Finish Edit";
                InEditMode = true;
                //dgLTQuotes.OptionsView.ShowHorzLines = true;
                //dgLTQuotes.OptionsView.ShowVertLines = true;
            }
        }

        private void dgLTQuotes_DoubleClick(object sender, EventArgs e)
        {
            //GridView view = sender as GridView;
            //Point p = view.GridControl.PointToClient(MousePosition);
            //GridHitInfo info = view.CalcHitInfo(p);

            //if (info.HitTest == GridHitTest.RowCell && !InEditMode && ((GridView)sender).FocusedValue.ToString() != "" && ((GridView)sender).FocusedColumn.Name.Contains("Quote"))
            //{
            //    if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
            //    {
            //        if (dgLTQuotes.FocusedColumn.Name.Contains("Quote"))
            //        {
            //            Cursor.Current = Cursors.WaitCursor; 

            //            string curTicker = dgLTQuotes.FocusedValue.ToString();

            //            if (curTicker != "")
            //            {
            //                frmOptionChain newOptionChain = new frmOptionChain(curTicker);
            //                newOptionChain.Show();
            //            }

            //            Cursor.Current = Cursors.Default; 
            //        }
            //    }
            //    else
            //    {
            //        frmLevel2 curFrmLevel2 = new frmLevel2();
            //        ConnectedForms.Add(curFrmLevel2);
            //        curFrmLevel2.Show();
            //        string curValue = ((GridView)sender).FocusedValue.ToString();
            //        //if (curValue.IndexOf('.') == -1) curValue = curValue + ".SA";
            //        curFrmLevel2.txtTicker.Text = curValue;
            //        curFrmLevel2.cmdRequest_Click(this, new EventArgs());
            //        curFrmLevel2.LoadOrders();
            //        //e.Cancel = true; 
            //    }
            //}
        }

        private void dgLTQuotes_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(!InEditMode) e.Cancel = true;
        }

        private void cmdSaveTickers_Click(object sender, EventArgs e)
        {
            SaveTickers();
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

                curNDistConn.Subscribe(ExchangeTradingCode, curSource);
            }
        }

        //private void ReSubscribe()
        //{
        //    for (int curCounter = 0; curCounter < LTLineList.Count; curCounter++)
        //    {
        //        SubscribeData(LTLineList[curCounter].Quote1);
        //        SubscribeData(LTLineList[curCounter].Quote2);
        //        SubscribeData(LTLineList[curCounter].Quote3);
        //        SubscribeData(LTLineList[curCounter].Quote4);
        //        SubscribeData(LTLineList[curCounter].Quote5);
        //        SubscribeData(LTLineList[curCounter].Quote6);
        //        SubscribeData(LTLineList[curCounter].Quote7);
        //    }
        //}


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

            double _IdSecurity1 = 0; public double IdSecurity1 { get { return _IdSecurity1; } set { _IdSecurity1 = value; } }
            double _IdSecurity2 = 0; public double IdSecurity2 { get { return _IdSecurity2; } set { _IdSecurity2 = value; } }
            double _IdSecurity3 = 0; public double IdSecurity3 { get { return _IdSecurity3; } set { _IdSecurity3 = value; } }
            double _IdSecurity4 = 0; public double IdSecurity4 { get { return _IdSecurity4; } set { _IdSecurity4 = value; } }
            double _IdSecurity5 = 0; public double IdSecurity5 { get { return _IdSecurity5; } set { _IdSecurity5 = value; } }
            double _IdSecurity6 = 0; public double IdSecurity6 { get { return _IdSecurity6; } set { _IdSecurity6 = value; } }
            double _IdSecurity7 = 0; public double IdSecurity7 { get { return _IdSecurity7; } set { _IdSecurity7 = value; } }

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
            string _AucCond1 = ""; public string AucCond1 { get { return _AucCond1; } set { _AucCond1 = value; } }
            double _AucLast1 = 0; public double AucLast1 { get { return _AucLast1; } set { _AucLast1 = value; } }
            double _AucChange1 = 0; public double AucChange1 { get { if (Last1 != 0) return _AucLast1 / Last1 - 1; else return 0; } set { _AucChange1 = value; } }

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
            string _AucCond2 = ""; public string AucCond2 { get { return _AucCond2; } set { _AucCond2 = value; } }
            double _AucLast2 = 0; public double AucLast2 { get { return _AucLast2; } set { _AucLast2 = value; } }
            double _AucChange2 = 0; public double AucChange2 { get { if (Last2 != 0) return _AucLast2 / Last2 - 1; else return 0; } set { _AucChange2 = value; } }

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
            string _AucCond3 = ""; public string AucCond3 { get { return _AucCond3; } set { _AucCond3 = value; } }
            double _AucLast3 = 0; public double AucLast3 { get { return _AucLast3; } set { _AucLast3 = value; } }
            double _AucChange3 = 0; public double AucChange3 { get { if (Last3 != 0) return _AucLast3 / Last3 - 1; else return 0; } set { _AucChange3 = value; } }

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
            string _AucCond4 = ""; public string AucCond4 { get { return _AucCond4; } set { _AucCond4 = value; } }
            double _AucLast4 = 0; public double AucLast4 { get { return _AucLast4; } set { _AucLast4 = value; } }
            double _AucChange4 = 0; public double AucChange4 { get { if (Last4 != 0) return _AucLast4 / Last4 - 1; else return 0; } set { _AucChange4 = value; } }

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
            string _AucCond5 = ""; public string AucCond5 { get { return _AucCond5; } set { _AucCond5 = value; } }
            double _AucLast5 = 0; public double AucLast5 { get { return _AucLast5; } set { _AucLast5 = value; } }
            double _AucChange5 = 0; public double AucChange5 { get { if (Last5 != 0) return _AucLast5 / Last5 - 1; else return 0; } set { _AucChange5 = value; } }

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
            string _AucCond6 = ""; public string AucCond6 { get { return _AucCond6; } set { _AucCond6 = value; } }
            double _AucLast6 = 0; public double AucLast6 { get { return _AucLast6; } set { _AucLast6 = value; } }
            double _AucChange6 = 0; public double AucChange6 { get { if (Last6 != 0) return _AucLast6 / Last6 - 1; else return 0; } set { _AucChange6 = value; } }

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
            string _AucCond7 = ""; public string AucCond7 { get { return _AucCond7; } set { _AucCond7 = value; } }
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

        private void UpdateColumnOrder()
        {
            InEditMode = true;
            //dgLTQuotes.BeginUpdate();
            int TotalColumns = 13;

            foreach (DataGridViewTextBoxColumn curColumn in dgLTQuotes.Columns)
            {
                //if (curColumn.Name.Contains("ChangeMTD")) { curColumn.BestFit(); }
                if (curColumn.Name.Contains("Quote")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 1); }
                if (curColumn.Name.Contains("Last")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 2); }
                if (curColumn.Name.Contains("Change")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 3); }
                if (curColumn.Name.Contains("BidSize")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 4); }
                if (curColumn.Name.Contains("Bid")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 5); }
                if (curColumn.Name.Contains("Ask")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 6); }
                if (curColumn.Name.Contains("AskSize")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 7); }
                if (curColumn.Name.Contains("ChangeMTD")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 8); }
                if (curColumn.Name.Contains("ChangeYTD")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 9); }
                if (curColumn.Name.Contains("Spacer")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 10); }
                if (curColumn.Name.Contains("AucCond")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 11); }
                if (curColumn.Name.Contains("AucLast")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 12); }
                if (curColumn.Name.Contains("AucChange")) { curColumn.DisplayIndex = TotalColumns * int.Parse(curColumn.Name.Substring(curColumn.Name.Length - 1)) - (TotalColumns - 13); }

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

            //dgLTQuotes.EndUpdate();
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
                        //string IdTicker = "DRAGITEM\t" + dgLTQuotes.GetRowCellValue(downHitInfo.RowHandle, curCol.Replace("col", "")).ToString();
                        string IdTicker = "0";
                        view.GridControl.DoDragDrop(IdTicker, DragDropEffects.Move);

                        downHitInfo = null;
                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
            }
        }

        private void dgLTQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            flgMouseDown = false;
        }

        private void cmdMKTData_Click(object sender, EventArgs e)
        {
            //curNDistConn.Connect();
            ReSubscribe();

            foreach (ConnectedForm curConnectedForm in ConnectedForms)
            {
                //curConnectedForm.curNDistConn.Connect();
                curConnectedForm.ReSubscribe();
            }
        }

        private void dgLTQuotes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Lime;
            e.CellStyle.BackColor = Color.Black;
        }
    }
}