using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
using NestDLL;
using NFastData;


namespace JMStrategy
{
    public partial class frmJMStrategy : Form
    {
        public frmJMStrategy()
        {
            InitializeComponent();
        }

        private bool flagMouseDown = false;
        private bool gridPairsInitialized = false;
        private bool gridOrdersInitialized = false;

        private BindingSource bndPairs = new BindingSource();
        private BindingSource bndOrders = new BindingSource();
        private BindingSource bndClSec = new BindingSource();
        private BindingSource bndSec1Items = new BindingSource();
        private BindingSource bndSec2Items = new BindingSource();
        private BindingSource bndLast1 = new BindingSource();
        private BindingSource bndLast2 = new BindingSource();


        private JMRunner curRunner;
        private JMModel curModel;
        private List<JM2ColItem> Sec1Items;
        private List<JM2ColItem> Sec2Items;

        private void Form1_Load(object sender, EventArgs e)
        {
            JMData.Instance.IniDate = new DateTime(2000, 01, 01);
            JMData.Instance.InitializeObjects();
            JMData.Instance.LoadTickers(JMUtils.IdSecurityCDI); 

            curRunner = new JMRunner(DateTime.Today);
            curRunner.LoadModels();            

            //RunHistorical();

            timer2.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SymConn.Instance.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!flagMouseDown) RefreshGrids();
        }

        private void LoadLstModels()
        {
            lstModels.DataSource = null;
            lstModels.DataSource = curRunner.Models;
        }

        private void lstModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            curModel = (JMModel)lstModels.SelectedItem;

            if (curModel != null)
            {
                bndPairs.DataSource = curModel.PairList;
                dgcPairs.DataSource = bndPairs;

                lblSection.Text = curModel.Section.ToString();
                lblIdSection.Text = curModel.IdSection.ToString();
                lblIdModel.Text = curModel.IdModel.ToString();
                lblLimiter.Text = curModel.Limiter.ToString();
                lblMultiplier.Text = curModel.Multiplier.ToString();
                lblSize.Text = curModel.Size.ToString("F");
                lblTriggerIn.Text = curModel.TriggerIN.ToString("P");
                lblTriggerOut.Text = curModel.TriggerOUT.ToString("P");
                lblDays.Text = curModel.Days.ToString();

                InitializeGrids();
            }
        }

        private void RefreshGrids()
        {
            curRunner.UpdateOrders2();

            lock (curRunner.SyncOrders)
            {
                dgvPairs.LayoutChanged();
                dgvPairs.UpdateGroupSummary();
                dgvPairs.RefreshData();

                bndOrders.DataSource = curRunner.GridOrders;
                dgvOrders.LayoutChanged();
                dgvOrders.UpdateGroupSummary();
                dgvOrders.RefreshData();

                dgvClosedSec.LayoutChanged();
                dgvClosedSec.UpdateGroupSummary();
                dgvClosedSec.RefreshData();

                UpdateSecItems((JMPairs)dgvPairs.GetRow(dgvPairs.GetSelectedRows()[0]));

                dgvSecurity1.LayoutChanged();
                dgvSecurity1.UpdateGroupSummary();
                dgvSecurity1.RefreshData();

                dgvSecurity2.LayoutChanged();
                dgvSecurity2.UpdateGroupSummary();
                dgvSecurity2.RefreshData();
            }
            
        }

        private void InitializeGrids()
        {
            if (curModel != null && !gridPairsInitialized)
            {
                dgcPairs.LookAndFeel.UseDefaultLookAndFeel = false;
                dgcPairs.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                dgcPairs.LookAndFeel.SetSkinStyle("Blue");

                dgvPairs.ColumnPanelRowHeight = 32;
                dgvPairs.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                dgvPairs.Columns["Ticker1Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgvPairs.Columns["Ticker1Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

                dgvPairs.Columns["Ticker2Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgvPairs.Columns["Ticker2Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

                dgvPairs.Columns["RatioVol"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgvPairs.Columns["RatioVol"].DisplayFormat.FormatString = "P2";

                dgvPairs.Columns["RatioHistVol"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgvPairs.Columns["RatioHistVol"].DisplayFormat.FormatString = "P2";

                dgvPairs.Columns["NPFinancial"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgvPairs.Columns["NPFinancial"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

                dgvPairs.Columns["StochasticDiff"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgvPairs.Columns["StochasticDiff"].DisplayFormat.FormatString = "P2";

                foreach (GridColumn curColumn in dgvPairs.Columns)
                {
                    curColumn.Visible = false;
                }

                int curPosition = 0;

                dgvPairs.Columns["PairBlocked"].VisibleIndex = curPosition++; dgvPairs.Columns["PairBlocked"].Visible = true;
                dgvPairs.Columns["Ticker1"].VisibleIndex = curPosition++; dgvPairs.Columns["Ticker1"].Visible = true;
                dgvPairs.Columns["Ticker2"].VisibleIndex = curPosition++; dgvPairs.Columns["Ticker2"].Visible = true;
                dgvPairs.Columns["RatioVol"].VisibleIndex = curPosition++; dgvPairs.Columns["RatioVol"].Visible = true;
                dgvPairs.Columns["RatioHistVol"].VisibleIndex = curPosition++; dgvPairs.Columns["RatioHistVol"].Visible = true;
                dgvPairs.Columns["StochasticDiff"].VisibleIndex = curPosition++; dgvPairs.Columns["StochasticDiff"].Visible = true;
                dgvPairs.Columns["NPFinancial"].VisibleIndex = curPosition++; dgvPairs.Columns["NPFinancial"].Visible = true;
                dgvPairs.Columns["Sec1CurPos"].VisibleIndex = curPosition++; dgvPairs.Columns["Sec1CurPos"].Visible = true;
                dgvPairs.Columns["Sec2CurPos"].VisibleIndex = curPosition++; dgvPairs.Columns["Sec2CurPos"].Visible = true;
                dgvPairs.Columns["PreviousPosition"].VisibleIndex = curPosition++; dgvPairs.Columns["PreviousPosition"].Visible = true;
                dgvPairs.Columns["CurrentPosition"].VisibleIndex = curPosition++; dgvPairs.Columns["CurrentPosition"].Visible = true;
                dgvPairs.Columns["Action"].VisibleIndex = curPosition++; dgvPairs.Columns["Action"].Visible = true;

                //dgvPairs.BestFitColumns();
                dgvPairs.Columns["Ticker1"].BestFit();
                dgvPairs.Columns["Ticker2"].BestFit();

                gridPairsInitialized = true;
                
            }

        }              
        
        private void dgvClosedSec_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            CloseSecDoRowClick(view, pt);
        }

        private void CloseSecDoRowClick(GridView view, Point pt)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(pt);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                string colCaption = (hitInfo.Column == null ? "N/A" : hitInfo.Column.GetCaption());

                if (colCaption == "Blocked")
                {
                    JMPairs curPair = ((JMClosedSec)dgvClosedSec.GetRow(hitInfo.RowHandle)).refPair;
                    curPair.PairBlocked = !curPair.PairBlocked;
                    curPair.Security1.UpdateTicker();
                    curPair.Security2.UpdateTicker();
                }
            }        
        }

        private void dgcPairs_Click(object sender, EventArgs e)
        {
            
        }

        private void PairsDoRowClick(GridView view, Point pt)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(pt);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                string colCaption = (hitInfo.Column == null ? "N/A" : hitInfo.Column.GetCaption());                

                JMPairs curPair = (JMPairs)dgvPairs.GetRow(hitInfo.RowHandle);

                if (colCaption == "Pair Blocked")
                {
                    curPair.PairBlocked = !curPair.PairBlocked;
                }

                UpdateSecItems(curPair);
                
            }
        }

        private void dgvPairs_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            PairsDoRowClick(view, pt);
        }

        private void dgvSecurity2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }

        private void UpdateSecItems(JMPairs curPair)
        {
            if (curPair != null)
            {
                Sec1Items = curPair.Sec1Items;
                bndSec1Items.DataSource = Sec1Items;
                dgcSecurity1.DataSource = bndSec1Items;

                Sec2Items = curPair.Sec2Items;
                bndSec2Items.DataSource = Sec2Items;
                dgcSecurity2.DataSource = bndSec2Items;

                bndLast1.DataSource = curPair.Security1.LastPrice;
                dgcLast1.DataSource = bndLast1;

                bndLast2.DataSource = curPair.Security2.LastPrice;
                dgcLast2.DataSource = bndLast2;

            }
        }

        private void dgvPairs_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = (GridView)sender;
            UpdateSecItems((JMPairs)view.GetRow(view.GetSelectedRows()[0]));
        }       

        private void GridMouseDown(object sender, MouseEventArgs e)
        {
            flagMouseDown = true;
        }

        private void GridMouseUp(object sender, MouseEventArgs e)
        {
            flagMouseDown = false;
        }

        private void btnSendOrders_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Send ALL orders?", "Send Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                curRunner.SendOrders();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (JMData.Instance.CheckAllStarted())
            {
                timer2.Stop();

                curRunner.InitializeModels();

                LoadLstModels();

                bndOrders.DataSource = curRunner.GridOrders;
                dgcOrders.DataSource = bndOrders;

                bndClSec.DataSource = curRunner.ClosedSec;
                dgcClosedSec.DataSource = bndClSec;


                timer1.Start();               
            }
        }

        private void RunHistorical()
        {
            SortedDictionary<DateTime, DateTime> NextDay = new SortedDictionary<DateTime, DateTime>();

            string SQLExpression = " SELECT SRDATE FROM NESTSRV06.NESTDB.DBO.TB053_PRECOS_INDICES WITH(NOLOCK) " +
                                   " WHERE IDSECURITY = 1073 AND SRTYPE = 1 AND SRDATE > '20101011' " +
                                   " ORDER BY SRDATE ASC";

            DataTable dates = new DataTable();
            using (newNestConn curConn = new newNestConn(true))
            {
                dates = curConn.Return_DataTable(SQLExpression);
            }

            DateTime prevDate = new DateTime(2010,10,11);

            JMData.Instance.LoadPairList();

            for (int i = 0; i < dates.Rows.Count; i++)
            {
                DateTime INITIME = DateTime.Now;
                DateTime curDate = DateTime.Parse(dates.Rows[i][0].ToString());

                JMData.Instance.LoadBTPosition(prevDate);

                JMRunner histRunner = new JMRunner(curDate);
                histRunner.LoadModels();
                histRunner.InitializeModels();
                histRunner.UpdateOrders2();
                histRunner.SendOrdersBT();

                NextDay.Add(prevDate, curDate);
                prevDate = curDate;

                TimeSpan elapsed = DateTime.Now.Subtract(INITIME);

                Console.WriteLine("Finished " + curDate.ToString("dd-MM-yyyy") + ". Elapsed: " + elapsed.ToString("G"));
            }

            CustomFastData posFastData = new CustomFastData(0, DateTime.MinValue, DateTime.MaxValue, 0);

            string SQLPositions = " SELECT B.DATE,B.IDMODEL,A.IDSECURITY1,A.IDSECURITY2,B.SEC1POSITION,B.SEC2POSITION "+
                                  " FROM NESTSIM.DBO.TB701_JM_PAIRS A (NOLOCK)  "+
                                  " INNER JOIN NESTSIM.DBO.TB703_JM_BT_POSITIONS B(NOLOCK)  " +
                                  " ON A.IDPAIR = B.IDPAIR";

            DataTable positions = new DataTable();
            using (newNestConn curConn = new newNestConn(true))
            {
                positions = curConn.Return_DataTable(SQLPositions);
            }

            SortedDictionary<DateTime,SortedDictionary<int,double>> ModelPerf = new SortedDictionary<DateTime,SortedDictionary<int,double>>();

            foreach (DataRow curRow in positions.Rows)
            {
                DateTime curDate = DateTime.Parse(curRow[0].ToString());
                DateTime nextDate = curDate;
                if (NextDay.TryGetValue(curDate, out nextDate))
                {


                    int IdModel = int.Parse(curRow[1].ToString());
                    int IdSecurity1 = int.Parse(curRow[2].ToString());
                    int IdSecurity2 = int.Parse(curRow[3].ToString());
                    int Position1 = int.Parse(curRow[4].ToString());
                    int Position2 = int.Parse(curRow[5].ToString());

                    double curPerf = 0;

                    if (Position1 != 0)
                    {
                        double[] last1 = JMData.Instance.GetPriceRange(IdSecurity1, 1, nextDate, 2);
                        curPerf = curPerf + (last1[0] - last1[1]) * Position1;
                    }

                    if (Position2 != 0)
                    {
                        double[] last2 = JMData.Instance.GetPriceRange(IdSecurity2, 1, nextDate, 2);
                        curPerf = curPerf + (last2[0] - last2[1]) * Position2;
                    }

                    if (ModelPerf.ContainsKey(nextDate))
                    {
                        if (ModelPerf[nextDate].ContainsKey(IdModel))
                        {
                            ModelPerf[nextDate][IdModel] += curPerf;
                        }
                        else
                        {
                            ModelPerf[nextDate].Add(IdModel, curPerf);
                        }
                    }
                    else
                    {
                        ModelPerf.Add(nextDate, (new SortedDictionary<int, double>()));
                        ModelPerf[nextDate].Add(IdModel, curPerf);
                    }
                }
            }

            using (newNestConn curConn = new newNestConn(true))
            {
                foreach (KeyValuePair<DateTime, SortedDictionary<int, double>> kvpDate in ModelPerf)
                {
                    foreach (KeyValuePair<int, double> kvpModel in kvpDate.Value)
                    {
                        string SQLInsert = " INSERT INTO NESTSIM.DBO.TB704_JM_BT_MODELPERF " +
                                           " SELECT '" + kvpDate.Key.ToString("yyyyMMdd") + "'," + kvpModel.Key.ToString() + "," + kvpModel.Value.ToString().Replace(',', '.');

                        curConn.ExecuteNonQuery(SQLInsert);
                    }
                }
            }
        }

        private void btnSell100_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("SELL 100 shares for all securities?", "Send Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                btnSell100.Enabled = false;
                curRunner.Send100Sell();
            }
        }

        private void btnBuy100_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("BUY 100 shares for all securities?", "Send Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                btnBuy100.Enabled = false;
                curRunner.Send100Buy();
            }
        }
    }
}
