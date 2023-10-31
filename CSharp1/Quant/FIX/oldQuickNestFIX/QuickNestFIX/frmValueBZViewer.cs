using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

using NestGeneric;
using NCommonTypes;
using NestDLL;

using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace QuantMonitor
{
    public partial class frmValueBZViewer : Form
    {
        NestSocketClient curNestSocketClient = new NestSocketClient();
        
        BindingSource bsSectorSignal = new BindingSource();
        BindingSource bsStagedOrders = new BindingSource();

        Utils curUtils = new Utils(2);

        List<St_OrderEventArgs> StagedOrders = new List<St_OrderEventArgs>();

        public List<TickerPE> PositionPEs = new List<TickerPE>();

        bool bRefreshGrids = false;

        int Status_FIX = 0;
        int Status_MKTDATA_SYM = 0;

        public frmValueBZViewer()
        {
            InitializeComponent();
        }

        private void frmValueBZViewer_Load(object sender, EventArgs e)
        {

            dgStratData.ColumnPanelRowHeight = 32;
            dgStratData.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            curNestSocketClient.Connect("127.0.0.1", 12310);
            //curNestSocketClient.Connect("192.168.0.23", 12310);
            curNestSocketClient.NewMessage +=new EventHandler(ReceivedMessage);

            bsSectorSignal.DataSource = PositionPEs;
            dtgStratData.DataSource = bsSectorSignal;

            bsStagedOrders.DataSource = StagedOrders;
            dgvStagedOrders.DataSource = bsStagedOrders;

            curUtils.LoadGridColumns(dgStratData);

            dgStratData.Columns["adjEPS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["adjEPS"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgStratData.Columns["curPrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["curPrice"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgStratData.Columns["curPE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["curPE"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgStratData.Columns["closePE"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["closePE"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

            dgStratData.Columns["EPSShareNumber"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["EPSShareNumber"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgStratData.Columns["curShareNumber"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["curShareNumber"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

            dgStratData.Columns["curEYield"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["curEYield"].DisplayFormat.FormatString = "P2";

            dgStratData.Columns["closeEYield"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["closeEYield"].DisplayFormat.FormatString = "P2";

            dgStratData.Columns["DayChange"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["DayChange"].DisplayFormat.FormatString = "P2";

            dgStratData.Columns["Weight"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["Weight"].DisplayFormat.FormatString = "P2";

            dgStratData.Columns["StratContrib"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgStratData.Columns["StratContrib"].DisplayFormat.FormatString = "P2";

            dgStratData.GroupSummary.Add(SummaryItemType.Sum, "StratContrib", dgStratData.Columns["StratContrib"]);
            ((GridSummaryItem)dgStratData.GroupSummary[dgStratData.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";

            tmrRefreshGrid.Start();
        }

        private void UpdateForm()
        {
            if (curNestSocketClient.IsConnected)
            {
                labConnStatus.Text = "STRAT Connected"; labConnStatus.ForeColor = Color.White; labConnStatus.BackColor = Color.LightGreen;
                cmdConnect.Enabled = false;
            }
            else
            {
                labConnStatus.Text = "STRAT Disconnected"; labConnStatus.ForeColor = Color.Yellow; labConnStatus.BackColor = Color.Red;
                cmdConnect.Enabled = true;
            }

            switch (Status_FIX)
            {
                case 1: labFIXStatus.Text = "FIX Connected"; labFIXStatus.ForeColor = Color.White; labFIXStatus.BackColor = Color.LightGreen; break;
                case 2: labFIXStatus.Text = "FIX Disconnected"; labFIXStatus.ForeColor = Color.Yellow; labFIXStatus.BackColor = Color.Red; break;
                default: labFIXStatus.Text = "Unknown"; break;
            }
        }

        public void StagedOrdersClear()
        {
            for (int i = StagedOrders.Count - 1; i >= 0; i--)
            {
                StagedOrders.Remove(StagedOrders[i]);
            }
        }

        private void RequestData_All()
        {
            curNestSocketClient.SendMessage("REQ" + (char)16 + "MKT");
            curNestSocketClient.SendMessage("REQ" + (char)16 + "STA");
            curNestSocketClient.SendMessage("REQ" + (char)16 + "STRAT");
            curNestSocketClient.SendMessage("REQ" + (char)16 + "ORDSTG");
        }

        public void ReceivedMessage(object sender, EventArgs e)
        {
            string curMessage = ((NestSocketClient.MsgEventArgs)e).strMessage;

            string[] curMessagePart = curMessage.Split((char)16);

            // --------------------------  RECEIVED STATUS DATA --------------------------------------
            if (curMessagePart[0] == "INF")
            {
                if (curMessagePart[1] == "STA")
                {
                    if (curMessagePart[2] == "MKTDATASYM")
                    {
                        if (curMessagePart[3] == "True") { Status_MKTDATA_SYM = 1; } else { Status_MKTDATA_SYM = 2; }

                    }

                    if (curMessagePart[2] == "FIXCONN")
                    {
                        if (curMessagePart[3] == "True") { Status_FIX = 1; } else { Status_FIX = 2; }
                    }
                }


                // --------------------------  RECEIVED MARKET DATA --------------------------------------

                if (curMessagePart[1] == "MKT")
                {
                    //if (curMessagePart[2] == "LAST") curLast = double.Parse(curMessagePart[3]);
                    //if (curMessagePart[2] == "BID") curBid = double.Parse(curMessagePart[3]);
                    //if (curMessagePart[2] == "ASK") curAsk = double.Parse(curMessagePart[3]);
                    //if (curMessagePart[2] == "BIDSZ") curBidSz = double.Parse(curMessagePart[3]);
                    //if (curMessagePart[2] == "ASKSZ") curAskSz = double.Parse(curMessagePart[3]);
                    //if (curMessagePart[2] == "VOLUME") curVolume = double.Parse(curMessagePart[3]);
                }

                // --------------------------  RECEIVED STRATEGY DATA --------------------------------------

                if (curMessagePart[1] == "STRAT")
                {
                    if (curMessagePart[2] == "CLEARALL")
                    {
                        //StagedOrdersClear();
                    }

                    if (curMessagePart[2] == "ADD")
                    {
                        try
                        {
                            TickerPE curTickerPE = new TickerPE();

                            curTickerPE.IdTickerComposite = int.Parse(curMessagePart[3]);
                            curTickerPE.IdTicker = int.Parse(curMessagePart[4]);
                            curTickerPE.Ticker = curMessagePart[5];
                            curTickerPE.EPSDate = DateTime.Parse(curMessagePart[8]);
                            curTickerPE.EPSKnownDate = DateTime.Parse(curMessagePart[9]);
                            curTickerPE.EPSValue = double.Parse(curMessagePart[10]);
                            curTickerPE.EPSShareNumber = double.Parse(curMessagePart[11]);
                            curTickerPE.curShareNumberDate = DateTime.Parse(curMessagePart[12]);
                            curTickerPE.curShareNumber = double.Parse(curMessagePart[13]);
                            double tempVal;
                            if(double.TryParse(curMessagePart[14], out tempVal)) curTickerPE.adjEPS = double.Parse(curMessagePart[14]);
                            curTickerPE.curPrice = double.Parse(curMessagePart[15]);

                            curTickerPE.closeShareNumber = double.Parse(curMessagePart[6]);
                            curTickerPE.closePrice = double.Parse(curMessagePart[7]);

                            curTickerPE.curSignal = int.Parse(curMessagePart[16]);
                            curTickerPE.closeSignal = int.Parse(curMessagePart[17]);
                            curTickerPE.Weight = double.Parse(curMessagePart[18]);
                            curTickerPE.StratContrib = double.Parse(curMessagePart[19]);
                            curTickerPE.StratName = "ValueBZ";

                            PositionPEs.Add(curTickerPE);

                        }
                        catch
                        { 
                        
                        }
                    }
                    if (curMessagePart[2] == "ENDADD")
                    {
                        bRefreshGrids = true;
                    }

                }
            }

            if (curMessagePart[0] == "ORDINF")
            {
                // --------------------------  UPDATE STAGED ORDERS --------------------------------------
                if (curMessagePart[1] == "ORDSTG")
                {
                    if (curMessagePart[2] == "CLEARALL")
                    {
                        StagedOrdersClear();
                    }

                    if (curMessagePart[2] == "ADD")
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            string curTicker = curConn.Execute_Query_String("SELECT Ticker FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=" + curMessagePart[4]);

                            St_OrderEventArgs curItem = new St_OrderEventArgs(curMessagePart[3], int.Parse(curMessagePart[4]), curTicker, int.Parse(curMessagePart[5]), double.Parse(curMessagePart[6]));
                            StagedOrders.Add(curItem);
                        }
                    }

                    if (curMessagePart[2] == "ENDBATCH")
                    {
                        bRefreshGrids = true;
                    }
                }

                // --------------------------  ORDER REPORTS --------------------------------------

                if (curMessagePart[1] == "NEWORD")
                {
                    MessageBox.Show("New Order Staged: " + curMessagePart[2].ToString() + " @ " + curMessagePart[3].ToString(), "NEW ORDER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (curMessagePart[1] == "SENTORD")
                {
                    MessageBox.Show("Sent Order: " + curMessagePart[2].ToString() + " @ " + curMessagePart[3].ToString(), "NEW ORDER", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (curMessagePart[1] == "EXEC")
                {
                    MessageBox.Show("Executed: " + curMessagePart[2].ToString());
                }
            }
            else
            {
                Console.WriteLine(curMessagePart[0]);
            }
        }       

        private void RefreshGrid()
        {
            if (bRefreshGrids)
            {
                bsSectorSignal.ResetBindings(false);
                bsStagedOrders.ResetBindings(false);
                bRefreshGrids = false;
                dgStratData.ExpandAllGroups();
            }
        }

        private void tmrRefreshGrid_Tick(object sender, EventArgs e)
        {
            UpdateForm();
            RefreshGrid();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            PositionPEs.Clear();
            RequestData_All();
            RefreshGrid();
        }

        private void btnStageOrders_Click(object sender, EventArgs e)
        {
            curNestSocketClient.SendMessage("CMD" + (char)16 + "CALCSTAGED");
            RefreshGrid();
        }

        private void btnGetStaged_Click(object sender, EventArgs e)
        {
            curNestSocketClient.SendMessage("REQ" + (char)16 + "ORDSTG");
            RefreshGrid();
        }

        private void dgvStagedOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                curNestSocketClient.SendMessage("CMD" 
                                                + (char)16 + "SENDORD" 
                                                + (char)16 + dgvStagedOrders[2, e.RowIndex].Value.ToString() 
                                                + (char)16 + chkAuction.Checked.ToString());
            }
            else if (e.ColumnIndex == 1)
            {
                curNestSocketClient.SendMessage("CMD"
                                                + (char)16 + "CANCELORD"
                                                + (char)16 + dgvStagedOrders[2, e.RowIndex].Value.ToString());
            }
        }        

        private void btnSendOrders_Click(object sender, EventArgs e)
        {
            curNestSocketClient.SendMessage("CMD" + (char)16 + "SENDSTAGED" + (char)16 + chkAuction.Checked.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            curNestSocketClient.SendMessage("CMD" + (char)16 + "CLEARSTAGED");
        }

        private void GetStratParam()
        {
            curNestSocketClient.SendMessage("REQ" + (char)16 + "STRAT");
        }

        private void btnUpdateNAV_Click(object sender, EventArgs e)
        {
            try
            {
                double newNAV = Convert.ToDouble(txtStratNAV.Text);

                curNestSocketClient.SendMessage("CMD" + (char)16 + "UPDATENAV" + (char)16 + newNAV.ToString());
            }
            catch
            {
                MessageBox.Show("Unable to send UPDATENAV request to strategy", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void dgStratData_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.SaveGridColumns(dgStratData);
        }

        private void dgStratData_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.SaveGridColumns(dgStratData);
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            curNestSocketClient.ReConnect();
        }

        private void dgStratData_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = view.GroupedColumns[level].Caption + "" + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

                if ((rect.X + rect.Width / 2) > view.Columns[4].Width)
                {
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
    } 
}