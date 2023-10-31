using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

using NestDLL;
using LiveBook.Business;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System.Threading;

namespace LiveBook
{
    public partial class frmPositions : LBForm
    {
        old_Conn curConn = new old_Conn();
        
        RefreshHelper hlprPositions;
        int GroupCalcDelay;
        bool ThreadRunning;
        string SelectedCustomView;
        bool bolHoldCalc;
        string prevPortfolio;
        string prevCustomView;
        string prevTable;
        string prevDate;
        string posTableName;
        RecordCollection srcList;
        int ExpandCounter = 0;
        bool flgPendExpand = false;
        int intPendExpand = 0;
        string strPendExpand = "";
        int prevBook = 0;
        DateTime LastRefreshTime = new DateTime(1900, 01, 01);

        GridSummaryItem tempItem;
        ColumnSortOrder curOrder;
        GridColumn curGrouping;
        BindingSource bndGridSource = new BindingSource();
        int SkippedRefresh = 0;

        bool FundChanged = false;

        public frmPositions()
        {
            InitializeComponent();
        }
        
        public void SetUpdateFreq(int UpdTime)
        {
            tmrRefreshPos.Interval = UpdTime;
        }

        private void frmPositions_Load(object sender, EventArgs e)
        {
            chkLinkAllBoxes.BackColor = Color.Transparent;
            chkLinkAllBoxes.Parent = (Control)dtgPositions;

            radHistoric.BackColor = Color.Transparent;
            radHistoric.Parent = (Control)dtgPositions;

            radRealTime.BackColor = Color.Transparent;
            radRealTime.Parent = (Control)dtgPositions;

            timer2.Start();
        // ======================  Initialize variables =========================================================
            
            srcList = new RecordCollection();
            hlprPositions = new RefreshHelper(dgPositions, "Column0");
            
            prevPortfolio = "0";

        // ======================  Initialize Grid =========================================================

            dgPositions.ColumnPanelRowHeight = 32;

            dgPositions.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            //dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            //dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            //dtg.LookAndFeel.SetSkinStyle("Blue");

            //dgPositions.OptionsMenu.ShowGroupSortSummaryItems = true;

            dgPositions.RowHeight = 10;

            string String_Campos;
            DataTable tablep = new DataTable();
            

            String_Campos = "Select TOP 1 * from NESTRT.dbo.FCN_Posicao_Atual()";
            tablep = curConn.Return_DataTable(String_Campos);

            bndGridSource.DataSource = srcList;
            dtgPositions.DataSource = bndGridSource;

            int c = 0;
            foreach (DataColumn col in tablep.Columns)
            {
                dgPositions.Columns[c].Caption = col.Caption;//.Replace(' ','\n');
                c++;
            }
            //Add_Totals();

            for (c = 0; c < dgPositions.Columns.Count; c++)
            {
                dgPositions.Columns[c].AppearanceCell.TextOptions.HAlignment = getAlign(c);
            }

            tablep.Dispose();

            dgPositions.Columns[1].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Column1] <> 0");
            dgPositions.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;

            dgPositions.Columns[4].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda--Ticker e QTD
            dgPositions.Columns[15].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda--Ticker e QTD
            FormatAllColumns();

    // ======================  Load Combo boxes =========================================================


            this.cmbChoosePortfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbChoosePortfolio_SelectedIndexChanged);
            this.cmbCustomView.SelectedIndexChanged -= new System.EventHandler(this.cmbCustomView_SelectedIndexChanged);

            NestDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name, Id_Port_Type from  dbo.Tb002_Portfolios (nolock) where Discountinued<>1 and Id_Port_Type=1 UNION ALL SELECT '-1', 'All Portfolios', '0' ORDER BY Id_Port_Type DESC,Port_Name", "Id_Portfolio", "Port_Name", 99);

            cmbCustomView.Items.Add("Custom");
            cmbCustomView.Items.Add("Options");
            cmbCustomView.Items.Add("Fixed Income");
            cmbCustomView.Items.Add("Exposure");
            cmbCustomView.Items.Add("Profit and Loss");
            cmbCustomView.Items.Add("Overview Country");
            cmbCustomView.Items.Add("Overview Strat");
            cmbCustomView.Items.Add("Overview Sub");
            cmbCustomView.Items.Add("Operations Report");
            cmbCustomView.Items.Add("Sector Report");
            cmbCustomView.Items.Add("Price vs Bid-Ask");
            cmbCustomView.Items.Add("Pricing");
            cmbCustomView.Items.Add("Greeks");
            cmbCustomView.Items.Add("Stock Loan");
            cmbCustomView.Items.Add("Options Report");


            this.cmbChoosePortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedIndexChanged);
            this.cmbCustomView.SelectedIndexChanged += new System.EventHandler(this.cmbCustomView_SelectedIndexChanged);

    // ======================  Set initial values for all controls =========================================================

            cmbChoosePortfolio_SelectedIndexChanged(this,e);
            cmbCustomView.SelectedItem = "Custom";

            radRealTime.Checked = true;
            chkLinkAllBoxes.Checked = true;

            tmrRefreshPos.Start();
            Add_Totals();
        }

        private void tmrRefreshPos_Tick(object sender, EventArgs e)
        {
            if (flgPendExpand && dgPositions.RowCount > 0)
            {
                ExpandByName(strPendExpand, intPendExpand-1);
                ExpandByName(strPendExpand, intPendExpand);
                flgPendExpand = false;
            }

            if (DateTime.Now.Subtract(LastRefreshTime).TotalSeconds > 30)
            {
                labDelayed.Visible = true;
            }
            else
            {
                labDelayed.Visible = false;
            }

            if (!ThreadRunning && !bolHoldCalc)
            {
                System.Threading.Thread t1;
                t1 = new System.Threading.Thread(new ThreadStart(UpdateGrid));
                t1.Start();

                GroupCalcDelay++;

                int selRow = 0;
                //
                if (dgPositions.RowCount > 0)
                {
                    selRow = dgPositions.FocusedRowHandle;

                    dgPositions.BeginUpdate();

                    //dgPositions.RefreshData();

                    if (GroupCalcDelay == 3)
                    {
                        /*
                        dgPositions.SaveLayoutToXml("c:\\LBPos.xml", OptionsLayoutBase.FullLayout);
                        //Remove_Totals();
                        //Add_Totals();
                        if (File.Exists("c:\\LBPos.xml"))
                        {
                            dgPositions.RestoreLayoutFromXml("c:\\LBPos.xml", OptionsLayoutBase.FullLayout);
                        }
                        //ApplyCustomColumns();
                        GroupCalcDelay = 0;
                        if (dgPositions.GroupSummary.Count > 0) 
                        {
                            //Remove_Totals();
                            //Add_Totals(); 
                        };
                        */
                    }
                    this.dgPositions.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgPositions_FocusedRowChanged);
                    if (selRow >= 0)
                    {
                        if (!FundChanged)
                        {
                            //dgPositions.FocusedRowHandle = selRow;
                        }
                        else
                        {
                            FundChanged = false;
                            dgPositions.CollapseAllGroups();
                        }
                    }
                    this.dgPositions.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.dgPositions_FocusedRowChanged);
                    bndGridSource.ResetBindings(false);
                    dgPositions.EndUpdate();
                }
                else
                {
                    dgPositions.RefreshData();
                }
            }
            else
            {
                SkippedRefresh++;
            }
            //dgPositions.FocusedRowHandle = -1;
        }

        public void Carrega_Grid()
        {
            if (cmbChoosePortfolio.SelectedValue != null && cmbCustomView.SelectedItem != null)
            {
                if (lblId.Text.ToString() != prevPortfolio || SelectedCustomView != prevCustomView || prevDate != dtpHistDate.Value.ToString("yyyyMMdd")|| prevTable != posTableName) 
                {
                    bolHoldCalc = true; 
                   // while (ThreadRunning) { }; 
                    bolHoldCalc = false;

                    srcList = new RecordCollection();
                    bndGridSource.DataSource = srcList;
                    prevCustomView = SelectedCustomView;
                }
                if (posTableName == "Tb000_Historical_Positions")
                {
                    this.Text = "Positions (HISTORICAL as of " + dtpHistDate.Value.ToString("dd/MM/yy") + ")";
                }
                else
                {
                    this.Text = "Positions";
                }
                if (this.chkLinkAllBoxes.Checked && cmbChoosePortfolio.SelectedValue.ToString() != "-1")
                {
                    Carrega_Portfolio(Convert.ToInt32(cmbChoosePortfolio.SelectedValue.ToString()), dtpHistDate.Value);
                }
            }
        }

        Random rnd = new Random();
        
        void UpdateGrid()
        {
            ThreadRunning = true;
            DateTime IniUpdate = DateTime.Now;
            try
            {
                string String_Campos;
                DataTable tablep = new DataTable();
                
                string IdPortfolio;
                IdPortfolio = lblId.Text.ToString();
                string Temp_Table_Name;

                string Position_Date = dtpHistDate.Value.ToString("yyyyMMdd");

                if (IdPortfolio == null) { IdPortfolio = "0"; }
                if (IdPortfolio == "System.Data.DataRowView") { IdPortfolio = "0"; }

                if (IdPortfolio == "0" || posTableName == null)
                {
                    ThreadRunning = false;
                    return;
                }

                Temp_Table_Name = "";

                if (posTableName.ToString() == "NESTRT.dbo.FCN_Posicao_Atual()")
                { Temp_Table_Name = posTableName; }
                else
                { Temp_Table_Name = posTableName + " (nolock)"; }

                String_Campos = "SELECT * FROM " + Temp_Table_Name + " WHERE [Id Portfolio] =" + IdPortfolio + " AND [Date Now]='" + Position_Date + "' order by [Delta Cash] desc, [Ticker] asc";

                if (IdPortfolio == "-1")
                { 
                    String_Campos = String_Campos.Replace("[Id Portfolio] =-1", "[Id Portfolio] <> 0 "); 
                }
                if (SelectedCustomView == "Options" || SelectedCustomView == "Greeks")
                {
                    String_Campos = String_Campos.Replace("WHERE", "WHERE ([Id Instrument]=3 OR [Id Base Underlying] IN (SELECT [Id Base Underlying] FROM " + Temp_Table_Name + " WHERE [Id Instrument]=3 AND [Id Portfolio] =" + IdPortfolio + " AND [Date Now]='" + Position_Date + "')) AND ");
                }
                if (SelectedCustomView == "Fixed Income") { String_Campos = String_Campos.Replace("WHERE", "WHERE [Id Asset Class]=2 AND "); }

                try
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        tablep = curConn.Return_DataTable(String_Campos);
                    }
                }
                catch (Exception e)
                {
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    if (!e.ToString().Contains("NOLOCK"))
                    {
                        throw e;
                    }
                }

                prevPortfolio = IdPortfolio;

                int rowcounter = tablep.Rows.Count;
                int collcounter = tablep.Columns.Count;
                int r = 1;
                 
                int flagAlter = 0;

                foreach (DataRow row in tablep.Rows)
                {
                    if (bolHoldCalc != false)
                    {
                        ThreadRunning = false;
                        return;
                    }
                    int curIndex = 0;
                    if (srcList != null) { curIndex = srcList.GetIndex(Convert.ToString(row[0])); }
                    if (curIndex == 99999 && (row[3] != null))
                    {
                        Record newrow = new Record();
                        newrow.Column0 = Convert.ToInt32(row[0]);
                        srcList.Add(newrow);
                        int a = srcList.Count;
                        string b = Convert.ToString(row[0]);
                        curIndex = srcList.GetIndex(Convert.ToString(row[0]));
                        flagAlter = 1;
                    }
                    int c = 0;
                    foreach (DataColumn col in tablep.Columns)
                    {
                        if (srcList[curIndex] != null)
                        {
                            if (srcList[curIndex].GetValue(c) != row[col].ToString())
                            {
                                srcList[curIndex].SetValue(c, row[col]);
                            };
                        }
                            c++;
                        
                    }
                    r++;
                }
                string find;
                DataRow[] linhas;
                for (int i = 0; i < srcList.Count-1; i++)
                {
                    if (srcList[i] != null)
                    {
                        find = srcList[i].Column0.ToString();
                        linhas = null;
                        linhas = tablep.Select("[Id Position]=" + find);
                    
                    if (linhas.Length < 1)
                    {
                        srcList[i].SetValue(0, 0);
                        srcList[i].SetValue(1, 0);
                    }
                    }
                }


                /*
                List<int> ExistingItems = new List<int>();
                foreach (DataRow row in tablep.Rows)
                {
                    if (curUtils.IsNumeric(srcList[0].ToString()))
                    {
                        ExistingItems.Add(int.Parse(srcList[0].ToString()));
                    }
                }
                */

                /*
              int zz;
              int contador = srcList.Count - 1;

              
              while (contador >= 0)
              {
                  if (!ExistingItems.Contains((int)srcList[contador].Column0))
                  {
                      int c = 0;
                      foreach (DataColumn col in tablep.Columns)
                      {
                          srcList[contador].SetValue(c, 0);
                          c++;
                      }
                  }

                  contador--;
              }
              */
                tablep.Dispose();
                /*
                int contador = srcList.Count - 1;

                while (contador >= 0)
                {
                    string SQLString = "SELECT count(*) FROM  " + Temp_Table_Name + " WHERE [Id Position] =" + srcList[contador].Column0 + " AND [Date Now]='" + Position_Date + "'";
                    int retorno = Convert.ToInt32(curConn.Execute_Query_String(SQLString));

                    if (retorno == 0)
                    {
                        int c = 0;
                        foreach (DataColumn col in tablep.Columns)
                        {
                            srcList[contador].SetValue(c, 0);
                            c++;
                        }
                    }
                    contador--;
                }
                */
                if (flagAlter == 1)
                {
                }
                flagAlter = 0;
            }
            catch { }

            TimeSpan TimeTaken = DateTime.Now - IniUpdate;

            double refhreshTime = TimeTaken.TotalMilliseconds;

            LastRefreshTime = DateTime.Now;

            ThreadRunning = false;

         }

        public void CreateNewLine(object data, int idPosition)
         {
             RecordCollection coll = data as RecordCollection;
             Record row = new Record();
             row.Column0 = Convert.ToInt32(idPosition);
             coll.Add(row);
             data = coll;
         }

        void SetValue(object data, int row, int column, object val)
         {
             RecordCollection rc = data as RecordCollection;
             rc.SetValue(row, column, val);
         }

         #region CustomViews
        
         private void ApplyCustomColumns()
         {
             if (cmbCustomView.SelectedItem != null)
             {
                 if (cmbCustomView.SelectedItem.ToString() == "Options")
                 {
                     StdOptionsView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Fixed Income")
                 {
                     StdFIView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Exposure")
                 {
                     StdExposureView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Profit and Loss")
                 {
                     StdPNLView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Overview Country")
                 {
                     StdOverCountryView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Overview Strat")
                 {
                     StdOverStratView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Overview Sub")
                 {
                     StdOverSubView();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Custom")
                 {
                     curUtils.SetColumnStyle(dgPositions, 1);
                 }

                 if (cmbCustomView.SelectedItem.ToString() == "Operations Report")
                 {
                     StdOperationsReport();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Sector Report")
                 {
                     StdOverSectorReport();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Price vs Bid-Ask")
                 {
                     StdPricingBidAsk();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Pricing")
                 {
                     StdPricing();
                 }
                 if (cmbCustomView.SelectedItem.ToString() == "Greeks")
                 {
                     StdGreeks();
                 }

                if (cmbCustomView.SelectedItem.ToString() == "Stock Loan")
                 {
                     StdStockLoan();
                 }
                if (cmbCustomView.SelectedItem.ToString() == "Options Report")
                 {
                     StdOptionsReport();
                 }
                 
                 
             }
             else 
             {
                 curUtils.SetColumnStyle(dgPositions, 1);

             }


         }
         
        void StdOptionsView() 
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;
            dgPositions.Columns["Column9"].Visible = true;
            dgPositions.Columns["Column80"].Visible = true;
            dgPositions.Columns["Column50"].Visible = true;
            dgPositions.Columns["Column85"].Visible = true;
            dgPositions.Columns["Column19"].Visible = true;
            dgPositions.Columns["Column32"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;
            dgPositions.Columns["Column44"].Visible = true;
            dgPositions.Columns["Column28"].Visible = true;
            dgPositions.Columns["Column76"].Visible = true;
            dgPositions.Columns["Column25"].Visible = true;
            dgPositions.Columns["Column77"].Visible = true;
            dgPositions.Columns["Column78"].Visible = true;
            dgPositions.Columns["Column79"].Visible = true;
            dgPositions.Columns["Column86"].Visible = true;
            dgPositions.Columns["Column87"].Visible = true;
            dgPositions.Columns["Column88"].Visible = true;
            dgPositions.Columns["Column89"].Visible = true;
            dgPositions.Columns["Column122"].Visible = true;


            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;
            dgPositions.Columns["Column9"].VisibleIndex = 2;
            dgPositions.Columns["Column80"].VisibleIndex = 3;
            dgPositions.Columns["Column85"].VisibleIndex = 4;
            dgPositions.Columns["Column50"].VisibleIndex = 5;
            dgPositions.Columns["Column19"].VisibleIndex = 6;
            dgPositions.Columns["Column32"].VisibleIndex = 7;
            dgPositions.Columns["Column29"].VisibleIndex = 8;
            dgPositions.Columns["Column44"].VisibleIndex = 9;
            dgPositions.Columns["Column28"].VisibleIndex = 10;
            dgPositions.Columns["Column76"].VisibleIndex = 11;
            dgPositions.Columns["Column25"].VisibleIndex = 12;
            dgPositions.Columns["Column77"].VisibleIndex = 13;
            dgPositions.Columns["Column78"].VisibleIndex = 14;
            dgPositions.Columns["Column79"].VisibleIndex = 15;
            dgPositions.Columns["Column86"].VisibleIndex = 16;
            dgPositions.Columns["Column87"].VisibleIndex = 17;
            dgPositions.Columns["Column88"].VisibleIndex = 18;
            dgPositions.Columns["Column89"].VisibleIndex = 19;
            dgPositions.Columns["Column122"].VisibleIndex = 20;

            dgPositions.Columns["Column122"].GroupIndex = 0;

        }

        void RemoveGroup()
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn testCol in dgPositions.Columns)
            {
                testCol.Visible = false;
                testCol.GroupIndex = -1;
            }

        }

        void StdGreeks()
        {
            RemoveGroup();
            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column165"].Visible = true;
            dgPositions.Columns["Column122"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;
            dgPositions.Columns["Column80"].Visible = true;
            dgPositions.Columns["Column50"].Visible = true;
            dgPositions.Columns["Column85"].Visible = true;
            dgPositions.Columns["Column19"].Visible = true;
            dgPositions.Columns["Column77"].Visible = true;
            dgPositions.Columns["Column78"].Visible = true;
            dgPositions.Columns["Column79"].Visible = true;
            dgPositions.Columns["Column76"].Visible = true;
            dgPositions.Columns["Column32"].Visible = true;
            dgPositions.Columns["Column86"].Visible = true;
            dgPositions.Columns["Column87"].Visible = true;
            dgPositions.Columns["Column88"].Visible = true;
            dgPositions.Columns["Column89"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;
            dgPositions.Columns["Column95"].Visible = true;
            dgPositions.Columns["Column140"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;
            dgPositions.Columns["Column80"].VisibleIndex = 2;
            dgPositions.Columns["Column50"].VisibleIndex = 3;
            dgPositions.Columns["Column85"].VisibleIndex = 4;
            dgPositions.Columns["Column19"].VisibleIndex = 5;
            dgPositions.Columns["Column77"].VisibleIndex = 6;
            dgPositions.Columns["Column78"].VisibleIndex = 7;
            dgPositions.Columns["Column79"].VisibleIndex = 8;
            dgPositions.Columns["Column76"].VisibleIndex = 9;
            dgPositions.Columns["Column32"].VisibleIndex = 10;
            dgPositions.Columns["Column86"].VisibleIndex = 11;
            dgPositions.Columns["Column87"].VisibleIndex = 12;
            dgPositions.Columns["Column88"].VisibleIndex = 13;
            dgPositions.Columns["Column89"].VisibleIndex = 14;
            dgPositions.Columns["Column29"].VisibleIndex = 15;
            dgPositions.Columns["Column95"].VisibleIndex = 16;
            dgPositions.Columns["Column140"].VisibleIndex = 17;

            dgPositions.Columns["Column165"].GroupIndex = 0;
            dgPositions.Columns["Column122"].GroupIndex = 1;

        }

        void StdFIView()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;

            dgPositions.Columns["Column33"].Visible = true;
            dgPositions.Columns["Column5"].Visible = true;
            dgPositions.Columns["Column50"].Visible = true;
            dgPositions.Columns["Column62"].Visible = true;
            dgPositions.Columns["Column28"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;
            dgPositions.Columns["Column153"].Visible = true;
            dgPositions.Columns["Column154"].Visible = true;
            dgPositions.Columns["Column155"].Visible = true;
            dgPositions.Columns["Column55"].Visible = true;


            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;

            dgPositions.Columns["Column33"].VisibleIndex = 2;
            dgPositions.Columns["Column5"].VisibleIndex = 3;
            dgPositions.Columns["Column50"].VisibleIndex = 4;
            dgPositions.Columns["Column62"].VisibleIndex = 5;
            dgPositions.Columns["Column28"].VisibleIndex = 6;
            dgPositions.Columns["Column29"].VisibleIndex = 7;
            dgPositions.Columns["Column153"].VisibleIndex = 8;
            dgPositions.Columns["Column154"].VisibleIndex = 9;
            dgPositions.Columns["Column155"].VisibleIndex = 10;

            dgPositions.Columns["Column30"].GroupIndex = 0;

        }

        void StdExposureView()
        {
            RemoveGroup();
            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;

            dgPositions.Columns["Column9"].Visible = true;
            dgPositions.Columns["Column11"].Visible = true;
            dgPositions.Columns["Column33"].Visible = true;
            dgPositions.Columns["Column40"].Visible = true;

            dgPositions.Columns["Column68"].Visible = true;
            dgPositions.Columns["Column69"].Visible = true;
            dgPositions.Columns["Column70"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;

            dgPositions.Columns["Column9"].VisibleIndex = 2;
            dgPositions.Columns["Column11"].VisibleIndex = 3;
            dgPositions.Columns["Column33"].VisibleIndex = 4;
            dgPositions.Columns["Column40"].VisibleIndex = 5;

            dgPositions.Columns["Column68"].VisibleIndex = 6;
            dgPositions.Columns["Column69"].VisibleIndex = 7;
            dgPositions.Columns["Column70"].VisibleIndex = 8;
            dgPositions.Columns["Column29"].VisibleIndex = 9;

            dgPositions.Columns["Column33"].GroupIndex = 0;
        }

        void StdPNLView()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;

            dgPositions.Columns["Column108"].Visible = true;
            dgPositions.Columns["Column17"].Visible = true;
            dgPositions.Columns["Column19"].Visible = true;
            dgPositions.Columns["Column74"].Visible = true;
            dgPositions.Columns["Column45"].Visible = true;
            dgPositions.Columns["Column49"].Visible = true;
            dgPositions.Columns["Column107"].Visible = true;
            dgPositions.Columns["Column44"].Visible = true;
            dgPositions.Columns["Column46"].Visible = true;
            dgPositions.Columns["Column73"].Visible = true;
            dgPositions.Columns["Column47"].Visible = true;
            dgPositions.Columns["Column55"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;

            dgPositions.Columns["Column143"].Visible = true;
            dgPositions.Columns["Column144"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;

            dgPositions.Columns["Column108"].VisibleIndex = 2;
            dgPositions.Columns["Column17"].VisibleIndex = 3;
            dgPositions.Columns["Column19"].VisibleIndex = 4;
            dgPositions.Columns["Column74"].VisibleIndex = 5;
            dgPositions.Columns["Column45"].VisibleIndex = 6;
            dgPositions.Columns["Column107"].VisibleIndex = 7;
            dgPositions.Columns["Column49"].VisibleIndex = 8;
            dgPositions.Columns["Column44"].VisibleIndex = 9;
            dgPositions.Columns["Column46"].VisibleIndex = 10;
            dgPositions.Columns["Column73"].VisibleIndex = 11;
            dgPositions.Columns["Column47"].VisibleIndex = 12;
            dgPositions.Columns["Column55"].VisibleIndex = 13;
            dgPositions.Columns["Column29"].VisibleIndex = 14;
            dgPositions.Columns["Column143"].VisibleIndex = 14;
            dgPositions.Columns["Column144"].VisibleIndex = 14;

            dgPositions.Columns["Column30"].GroupIndex = 0;
        }

        void StdOverCountryView()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;

            dgPositions.Columns["Column57"].Visible = true;
            dgPositions.Columns["Column21"].Visible = true;
            dgPositions.Columns["Column17"].Visible = true;
            dgPositions.Columns["Column19"].Visible = true;
            dgPositions.Columns["Column68"].Visible = true;
            dgPositions.Columns["Column69"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;
            dgPositions.Columns["Column28"].Visible = true;

            dgPositions.Columns["Column32"].Visible = true;
            dgPositions.Columns["Column55"].Visible = true;
            dgPositions.Columns["Column47"].Visible = true;
            dgPositions.Columns["Column46"].Visible = true;
            dgPositions.Columns["Column44"].Visible = true;
            dgPositions.Columns["Column66"].Visible = true;
            dgPositions.Columns["Column67"].Visible = true;

            dgPositions.Columns["Column2"].Visible = true;
            dgPositions.Columns["Column9"].Visible = true;
            dgPositions.Columns["Column39"].Visible = true;
            dgPositions.Columns["Column33"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;

            dgPositions.Columns["Column57"].VisibleIndex = 2;
            dgPositions.Columns["Column21"].VisibleIndex = 3;
            dgPositions.Columns["Column17"].VisibleIndex = 4;
            dgPositions.Columns["Column19"].VisibleIndex = 5;
            dgPositions.Columns["Column68"].VisibleIndex = 6;
            dgPositions.Columns["Column69"].VisibleIndex = 7;
            dgPositions.Columns["Column29"].VisibleIndex = 8;
            dgPositions.Columns["Column28"].VisibleIndex = 9;

            dgPositions.Columns["Column32"].VisibleIndex = 10;
            dgPositions.Columns["Column55"].VisibleIndex = 11;
            dgPositions.Columns["Column47"].VisibleIndex = 12;
            dgPositions.Columns["Column46"].VisibleIndex = 13;
            dgPositions.Columns["Column44"].VisibleIndex = 14;
            dgPositions.Columns["Column66"].VisibleIndex = 15;
            dgPositions.Columns["Column67"].VisibleIndex = 16;

            dgPositions.Columns["Column2"].VisibleIndex = 17;
            dgPositions.Columns["Column9"].VisibleIndex = 18;
            dgPositions.Columns["Column39"].VisibleIndex = 19;
            dgPositions.Columns["Column33"].VisibleIndex = 20;

            dgPositions.Columns["Column9"].GroupIndex = 0;
            dgPositions.Columns["Column39"].GroupIndex = 1;
            dgPositions.Columns["Column33"].GroupIndex = 2;

        }

        void StdOperationsReport()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;
            dgPositions.Columns["Column165"].Visible = true;
            dgPositions.Columns["Column111"].Visible = true;
            dgPositions.Columns["Column28"].Visible = true;
            dgPositions.Columns["Column16"].Visible = true;
            dgPositions.Columns["Column14"].Visible = true;
            dgPositions.Columns["Column117"].Visible = true;
            dgPositions.Columns["Column20"].Visible = true;
            dgPositions.Columns["Column109"].Visible = true;
            dgPositions.Columns["Column112"].Visible = true;
            dgPositions.Columns["Column33"].Visible = true; //Instrument
            dgPositions.Columns["Column30"].Visible = true; //Underlying Country
            dgPositions.Columns["Column156"].Visible = true; //[Asset PL uC Admin]

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;
            dgPositions.Columns["Column165"].VisibleIndex = 2;
            dgPositions.Columns["Column111"].VisibleIndex = 3;
            dgPositions.Columns["Column28"].VisibleIndex = 4;
            dgPositions.Columns["Column16"].VisibleIndex = 5;
            dgPositions.Columns["Column14"].VisibleIndex = 6;
            dgPositions.Columns["Column117"].VisibleIndex =7;
            dgPositions.Columns["Column20"].VisibleIndex = 8;
            dgPositions.Columns["Column109"].VisibleIndex = 9;
            dgPositions.Columns["Column112"].VisibleIndex = 10;
          //new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCountry, DevExpress.Data.ColumnSortOrder.Ascending)});
            //dgPositions.Columns["Column100"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            dgPositions.Columns["Column33"].VisibleIndex = 11; //Instrument
            dgPositions.Columns["Column30"].VisibleIndex = 12; //Underlying Country
            dgPositions.Columns["Column156"].VisibleIndex = 13; //[Asset PL uC Admin]
        }

        void StdOverSectorReport()
        {
            RemoveGroup();

            dgPositions.Columns["Column34"].Visible = true; // Asset Class
            dgPositions.Columns["Column25"].Visible = true; //Cash
            dgPositions.Columns["Column26"].Visible = true; //Cash/NAV
            dgPositions.Columns["Column55"].Visible = true; //Contribution pC
            dgPositions.Columns["Column17"].Visible = true; //Cost Close
            dgPositions.Columns["Column18"].Visible = true; //Cost Close pC
            dgPositions.Columns["Column32"].Visible = true; //Delta
            dgPositions.Columns["Column28"].Visible = true; //Delta Cash
            dgPositions.Columns["Column29"].Visible = true; //Delta/NAV
            dgPositions.Columns["Column50"].Visible = true; //Expiration
            dgPositions.Columns["Column70"].Visible = true; //Gross Delta
            dgPositions.Columns["Column33"].Visible = true; //Instrument
            dgPositions.Columns["Column19"].Visible = true; //Last
            dgPositions.Columns["Column68"].Visible = true; //Long Delta 
            dgPositions.Columns["Column40"].Visible = true; //Nest Sector
            dgPositions.Columns["Column2"].Visible = true; //Portfolio
            dgPositions.Columns["Column15"].Visible = true; //Position
            dgPositions.Columns["Column38"].Visible = true; //Sector
            dgPositions.Columns["Column69"].Visible = true; //Short Delta
            dgPositions.Columns["Column165"].Visible = true; //Book
            dgPositions.Columns["Column167"].Visible = true; //Sub Portfolio
            dgPositions.Columns["Column169"].Visible = true; //New Strategy
            dgPositions.Columns["Column171"].Visible = true; //New Sub Strategy
            dgPositions.Columns["Column173"].Visible = true; //Section
//            dgPositions.Columns["Column9"].Visible = true; //Strategy
            dgPositions.Columns["Column4"].Visible = true; //Ticker
            dgPositions.Columns["Column47"].Visible = true; //Total P/L
            dgPositions.Columns["Column57"].Visible = true; //Underlying
            dgPositions.Columns["Column39"].Visible = true; //Underlying Country
            dgPositions.Columns["Column30"].Visible = true; //Security Currency

            dgPositions.Columns["Column2"].VisibleIndex = 0; //Portfolio
            dgPositions.Columns["Column4"].VisibleIndex = 1; //Ticker
            dgPositions.Columns["Column29"].VisibleIndex =2 ; //Delta/NAV
            dgPositions.Columns["Column15"].VisibleIndex =3 ; //Position
            dgPositions.Columns["Column39"].VisibleIndex =4 ; //Underlying Country
            dgPositions.Columns["Column34"].VisibleIndex =5 ; // Asset Class
            dgPositions.Columns["Column38"].VisibleIndex =6 ; //Sector
            dgPositions.Columns["Column33"].VisibleIndex =7 ; //Instrument

            dgPositions.Columns["Column165"].VisibleIndex = 8; //Book
            dgPositions.Columns["Column167"].VisibleIndex = 9; //Sub Portfolio
            dgPositions.Columns["Column169"].VisibleIndex = 10; //New Strategy
            dgPositions.Columns["Column171"].VisibleIndex = 11; //New Sub Strategy
            dgPositions.Columns["Column173"].VisibleIndex = 12; //Section


//            dgPositions.Columns["Column9"].VisibleIndex = 8; //Strategy
            dgPositions.Columns["Column40"].VisibleIndex =13 ; //Nest Sector
            dgPositions.Columns["Column17"].VisibleIndex =14 ; //Cost Close
            dgPositions.Columns["Column18"].VisibleIndex =15 ; //Cost Close pC
            dgPositions.Columns["Column19"].VisibleIndex =16 ; //Last
            dgPositions.Columns["Column25"].VisibleIndex =17 ; //Cash
            dgPositions.Columns["Column70"].VisibleIndex =18 ; //Gross Delta
            dgPositions.Columns["Column68"].VisibleIndex =19 ; //Long Delta 
            dgPositions.Columns["Column69"].VisibleIndex =20 ; //Short Delta
            dgPositions.Columns["Column47"].VisibleIndex =21 ; //Total P/L
            dgPositions.Columns["Column26"].VisibleIndex =22 ; //Cash/NAV
            dgPositions.Columns["Column28"].VisibleIndex =23 ; //Delta Cash
            dgPositions.Columns["Column32"].VisibleIndex =24 ; //Delta
            dgPositions.Columns["Column50"].VisibleIndex =25 ; //Expiration
            dgPositions.Columns["Column55"].VisibleIndex =26 ; //Contribution pC
            dgPositions.Columns["Column57"].VisibleIndex =27 ; //Underlying
            dgPositions.Columns["Column30"].VisibleIndex = 28; //Security Currency
        }

        void StdStockLoan()
        {
            RemoveGroup();
            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;
            dgPositions.Columns["Column165"].Visible = true;
            dgPositions.Columns["Column173"].Visible = true;
            dgPositions.Columns["Column157"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;

            dgPositions.Columns["Column194"].Visible = true;
            dgPositions.Columns["Column193"].Visible = true;
            dgPositions.Columns["Column197"].Visible = true;
            dgPositions.Columns["Column198"].Visible = true;
            dgPositions.Columns["Column200"].Visible = true;
            dgPositions.Columns["Column201"].Visible = true;
            dgPositions.Columns["Column202"].Visible = true;
            dgPositions.Columns["Column203"].Visible = true;
            dgPositions.Columns["Column195"].Visible = true;
            dgPositions.Columns["Column205"].Visible = true;
            dgPositions.Columns["Column206"].Visible = true;
            dgPositions.Columns["Column152"].Visible = true;
            dgPositions.Columns["Column207"].Visible = true;
            dgPositions.Columns["Column208"].Visible = true;
            dgPositions.Columns["Column209"].Visible = true;
            dgPositions.Columns["Column210"].Visible = true;
            dgPositions.Columns["Column211"].Visible = true;


            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;
            dgPositions.Columns["Column165"].VisibleIndex = 2;
            dgPositions.Columns["Column173"].VisibleIndex = 3;
            dgPositions.Columns["Column157"].VisibleIndex = 4;
            dgPositions.Columns["Column29"].VisibleIndex = 5;

            dgPositions.Columns["Column194"].VisibleIndex = 6;
            dgPositions.Columns["Column193"].VisibleIndex = 7;
            dgPositions.Columns["Column197"].VisibleIndex = 8;
            dgPositions.Columns["Column198"].VisibleIndex = 9;
            dgPositions.Columns["Column200"].VisibleIndex = 10;
            dgPositions.Columns["Column201"].VisibleIndex = 11;
            dgPositions.Columns["Column202"].VisibleIndex = 12;
            dgPositions.Columns["Column203"].VisibleIndex = 13;
            dgPositions.Columns["Column205"].VisibleIndex = 14;
            dgPositions.Columns["Column195"].VisibleIndex = 15;
            dgPositions.Columns["Column210"].VisibleIndex = 16;
            dgPositions.Columns["Column206"].VisibleIndex = 17;
            dgPositions.Columns["Column152"].VisibleIndex = 18;
            dgPositions.Columns["Column207"].VisibleIndex = 19;
            dgPositions.Columns["Column208"].VisibleIndex = 20;
            dgPositions.Columns["Column209"].VisibleIndex = 21;
            dgPositions.Columns["Column211"].VisibleIndex = 22;



            dgPositions.Columns["Column2"].GroupIndex = 0;
            dgPositions.Columns["Column165"].GroupIndex = 1;
            dgPositions.Columns["Column211"].GroupIndex = 2;
        }

        void StdOverSubView()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;

            dgPositions.Columns["Column57"].Visible = true;
            dgPositions.Columns["Column21"].Visible = true;
            dgPositions.Columns["Column17"].Visible = true;
            dgPositions.Columns["Column19"].Visible = true;
            dgPositions.Columns["Column68"].Visible = true;
            dgPositions.Columns["Column69"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;
            dgPositions.Columns["Column28"].Visible = true;

            dgPositions.Columns["Column157"].Visible = true;
            dgPositions.Columns["Column158"].Visible = true;
            dgPositions.Columns["Column32"].Visible = true;
            dgPositions.Columns["Column55"].Visible = true;
            dgPositions.Columns["Column47"].Visible = true;
            dgPositions.Columns["Column46"].Visible = true;
            dgPositions.Columns["Column44"].Visible = true;
            dgPositions.Columns["Column66"].Visible = true;
            dgPositions.Columns["Column67"].Visible = true;

            dgPositions.Columns["Column2"].Visible = true;
            dgPositions.Columns["Column9"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;

            dgPositions.Columns["Column57"].VisibleIndex = 2;
            dgPositions.Columns["Column21"].VisibleIndex = 3;
            dgPositions.Columns["Column17"].VisibleIndex = 4;
            dgPositions.Columns["Column19"].VisibleIndex = 5;
            dgPositions.Columns["Column68"].VisibleIndex = 6;
            dgPositions.Columns["Column69"].VisibleIndex = 7;
            dgPositions.Columns["Column29"].VisibleIndex = 8;
            dgPositions.Columns["Column28"].VisibleIndex = 9;

            dgPositions.Columns["Column157"].VisibleIndex = 10;
            dgPositions.Columns["Column158"].VisibleIndex = 11;

            dgPositions.Columns["Column32"].VisibleIndex = 12;
            dgPositions.Columns["Column55"].VisibleIndex = 13;
            dgPositions.Columns["Column47"].VisibleIndex = 14;
            dgPositions.Columns["Column46"].VisibleIndex = 15;
            dgPositions.Columns["Column44"].VisibleIndex = 16;
            dgPositions.Columns["Column66"].VisibleIndex = 17;
            dgPositions.Columns["Column67"].VisibleIndex = 18;

            dgPositions.Columns["Column11"].GroupIndex = 0;
            dgPositions.Columns["Column9"].GroupIndex = 1;
        }

        void StdOverStratView()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true;
            dgPositions.Columns["Column15"].Visible = true;

            dgPositions.Columns["Column57"].Visible = true;
            dgPositions.Columns["Column21"].Visible = true;
            dgPositions.Columns["Column17"].Visible = true;
            dgPositions.Columns["Column19"].Visible = true;
            dgPositions.Columns["Column68"].Visible = true;
            dgPositions.Columns["Column69"].Visible = true;
            dgPositions.Columns["Column29"].Visible = true;
            dgPositions.Columns["Column28"].Visible = true;

            dgPositions.Columns["Column32"].Visible = true;
            dgPositions.Columns["Column55"].Visible = true;
            dgPositions.Columns["Column47"].Visible = true;
            dgPositions.Columns["Column46"].Visible = true;
            dgPositions.Columns["Column44"].Visible = true;
            dgPositions.Columns["Column66"].Visible = true;
            dgPositions.Columns["Column67"].Visible = true;

            dgPositions.Columns["Column2"].Visible = true;
            dgPositions.Columns["Column9"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 0;
            dgPositions.Columns["Column15"].VisibleIndex = 1;

            dgPositions.Columns["Column57"].VisibleIndex = 2;
            dgPositions.Columns["Column21"].VisibleIndex = 3;
            dgPositions.Columns["Column17"].VisibleIndex = 4;
            dgPositions.Columns["Column19"].VisibleIndex = 5;
            dgPositions.Columns["Column68"].VisibleIndex = 6;
            dgPositions.Columns["Column69"].VisibleIndex = 7;
            dgPositions.Columns["Column29"].VisibleIndex = 8;
            dgPositions.Columns["Column28"].VisibleIndex = 9;

            dgPositions.Columns["Column32"].VisibleIndex = 10;
            dgPositions.Columns["Column55"].VisibleIndex = 11;
            dgPositions.Columns["Column47"].VisibleIndex = 12;
            dgPositions.Columns["Column46"].VisibleIndex = 13;
            dgPositions.Columns["Column44"].VisibleIndex = 14;
            dgPositions.Columns["Column66"].VisibleIndex = 15;
            dgPositions.Columns["Column67"].VisibleIndex = 16;

            dgPositions.Columns["Column9"].GroupIndex = 0;
            dgPositions.Columns["Column11"].GroupIndex = 1;

            flgPendExpand = true;
            intPendExpand = 1;
            strPendExpand = "Long-Short";
        }

        void StdPricingBidAsk()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true; //Ticker
            dgPositions.Columns["Column15"].Visible = true; //Position
            dgPositions.Columns["Column33"].Visible = true; //Instrument
            dgPositions.Columns["Column30"].Visible = true; //Instrument
            dgPositions.Columns["Column9"].Visible = true; 
            dgPositions.Columns["Column26"].Visible = true; 
            dgPositions.Columns["Column117"].Visible = true; 
            dgPositions.Columns["Column125"].Visible = true; 
            dgPositions.Columns["Column126"].Visible = true; 
            dgPositions.Columns["Column127"].Visible = true; 

            dgPositions.Columns["Column4"].VisibleIndex = 1;
            dgPositions.Columns["Column15"].VisibleIndex = 2;
            dgPositions.Columns["Column33"].VisibleIndex = 3;
            dgPositions.Columns["Column30"].VisibleIndex = 4;
            dgPositions.Columns["Column9"].VisibleIndex = 5;
            dgPositions.Columns["Column26"].VisibleIndex = 6;
            dgPositions.Columns["Column117"].VisibleIndex = 7;
            dgPositions.Columns["Column125"].VisibleIndex = 8;
            dgPositions.Columns["Column126"].VisibleIndex = 9;
            dgPositions.Columns["Column127"].VisibleIndex = 10;

            dgPositions.Columns["Column33"].GroupIndex = 0;
        }

        void StdPricing()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true; //Ticker
            dgPositions.Columns["Column15"].Visible = true; //Position
            dgPositions.Columns["Column33"].Visible = true; //Instrument
            dgPositions.Columns["Column29"].Visible = true; //Delta/NAV
            dgPositions.Columns["Column17"].Visible = true; //Cost Close pC
            dgPositions.Columns["Column19"].Visible = true; //Last pC
            dgPositions.Columns["Column129"].Visible = true; //Source Last
            dgPositions.Columns["Column130"].Visible = true; //Flag Last
            dgPositions.Columns["Column55"].Visible = true; //Contribution pC

            dgPositions.Columns["Column142"].Visible = true; //Source Close Admin
            dgPositions.Columns["Column136"].Visible = true; //Flag Close Admin

            dgPositions.Columns["Column119"].Visible = true; //Cost Close Admin
            dgPositions.Columns["Column117"].Visible = true; //Last Admin
            dgPositions.Columns["Column132"].Visible = true; //Source Last Admin
            dgPositions.Columns["Column133"].Visible = true; //Flag Last Admin
            dgPositions.Columns["Column116"].Visible = true; //Contribution pC Admin
            dgPositions.Columns["Column98"].Visible = true; //Dif Contrib Adm
            dgPositions.Columns["Column139"].Visible = true; //Asset uC Admin

            dgPositions.Columns["Column30"].Visible = true;

            dgPositions.Columns["Column4"].VisibleIndex = 1;
            dgPositions.Columns["Column15"].VisibleIndex = 2; 
            dgPositions.Columns["Column33"].VisibleIndex = 3; 
            dgPositions.Columns["Column29"].VisibleIndex = 4;
            dgPositions.Columns["Column17"].VisibleIndex = 5;
            dgPositions.Columns["Column19"].VisibleIndex = 6;
            dgPositions.Columns["Column129"].VisibleIndex = 7;
            dgPositions.Columns["Column130"].VisibleIndex = 8;
            dgPositions.Columns["Column55"].VisibleIndex = 9;
            dgPositions.Columns["Column142"].VisibleIndex = 10;
            dgPositions.Columns["Column136"].VisibleIndex = 11;
            dgPositions.Columns["Column119"].VisibleIndex = 12;
            dgPositions.Columns["Column117"].VisibleIndex = 13;
            dgPositions.Columns["Column132"].VisibleIndex = 14;
            dgPositions.Columns["Column133"].VisibleIndex = 15;
            dgPositions.Columns["Column116"].VisibleIndex = 16;
            dgPositions.Columns["Column98"].VisibleIndex = 17;
            dgPositions.Columns["Column139"].VisibleIndex = 18;

            dgPositions.Columns["Column30"].GroupIndex = 0;

            dgPositions.Columns["Column4"].VisibleIndex = 1;
            dgPositions.Columns["Column15"].VisibleIndex = 2; 

        }

        void StdStockLoan2()
        {
            RemoveGroup();

            dgPositions.Columns["Column2"].Visible = true; //Portfolio
            dgPositions.Columns["Column33"].Visible = true; //Instrument
            dgPositions.Columns["Column4"].Visible = true; //Ticker
            dgPositions.Columns["Column15"].Visible = true; //Position
            dgPositions.Columns["Column118"].Visible = true; //Close
            dgPositions.Columns["Column117"].Visible = true; //Last
            dgPositions.Columns["Column28"].Visible = true; //Delta Cash
            dgPositions.Columns["Column165"].Visible = true; //Book
            dgPositions.Columns["Column30"].Visible = true; //Sec Currency

            dgPositions.Columns["Column2"].VisibleIndex = 0; //Portfolio
            dgPositions.Columns["Column33"].VisibleIndex = 1; //Instrument
            dgPositions.Columns["Column4"].VisibleIndex = 2; //Ticker
            dgPositions.Columns["Column15"].VisibleIndex = 3; //Position
            dgPositions.Columns["Column118"].VisibleIndex = 4; //Close
            dgPositions.Columns["Column117"].VisibleIndex = 5; //Last
            dgPositions.Columns["Column28"].VisibleIndex = 6; //Delta Cash
            dgPositions.Columns["Column165"].VisibleIndex = 7; //Book
            dgPositions.Columns["Column30"].VisibleIndex = 8; //Sec Currency
       
        }

        void StdOptionsReport()
        {
            RemoveGroup();

            dgPositions.Columns["Column4"].Visible = true; //Ticker
            dgPositions.Columns["Column15"].Visible = true; //Position
            dgPositions.Columns["Column2"].Visible = true; //Portfolio
            dgPositions.Columns["Column102"].Visible = true; //OptionType
            dgPositions.Columns["Column57"].Visible = true; //Underlying
            dgPositions.Columns["Column80"].Visible = true; //Strike
            dgPositions.Columns["Column50"].Visible = true; //Expiration
            dgPositions.Columns["Column85"].Visible = true; //Underlying Last
            dgPositions.Columns["Column165"].Visible = true; //Book
            dgPositions.Columns["Column14"].Visible = true; //Date Now
            dgPositions.Columns["Column117"].Visible = true; //Last Admin
            dgPositions.Columns["Column109"].Visible = true; //Last pC Admin
            dgPositions.Columns["Column33"].Visible = true; //Instrument
            dgPositions.Columns["Column30"].Visible = true; //Security Currency


            dgPositions.Columns["Column4"].VisibleIndex= 0; //Ticker
            dgPositions.Columns["Column15"].VisibleIndex= 1; //Position
            dgPositions.Columns["Column2"].VisibleIndex= 2; //Portfolio
            dgPositions.Columns["Column102"].VisibleIndex= 3; //OptionType
            dgPositions.Columns["Column57"].VisibleIndex= 4; //Underlying
            dgPositions.Columns["Column80"].VisibleIndex= 5; //Strike
            dgPositions.Columns["Column50"].VisibleIndex= 6; //Expiration
            dgPositions.Columns["Column85"].VisibleIndex= 7; //Underlying Last
            dgPositions.Columns["Column165"].VisibleIndex= 8; //Book
            dgPositions.Columns["Column14"].VisibleIndex= 9; //Date Now
            dgPositions.Columns["Column117"].VisibleIndex= 10; //Last Admin
            dgPositions.Columns["Column109"].VisibleIndex= 11; //Last pC Admin
            dgPositions.Columns["Column33"].VisibleIndex= 12; //Instrument
            dgPositions.Columns["Column30"].VisibleIndex= 13; //Security Currency


        }
        

#endregion

        void ExpandByName(string grpName, int grpLevel)
        {
            for (int i = -1; ; i--)
            {
                if (!dgPositions.IsValidRowHandle(i)) return;
                int curLevel = dgPositions.GetRowLevel(i);

                if (curLevel == grpLevel)
                {
                    string GroupText = dgPositions.GetGroupRowDisplayText(i, false);

                    if (GroupText.Contains(grpName))
                    {
                        dgPositions.SetRowExpanded(i, true);
                    }
                    else 
                    {
                        dgPositions.SetRowExpanded(i, false);
                    }
                }
            }

        }

        DevExpress.Utils.HorzAlignment getAlign(int colNumber)
        {
            switch (colNumber)
            {
                case 0: return DevExpress.Utils.HorzAlignment.Far; 
                case 1: return DevExpress.Utils.HorzAlignment.Far; 
                case 2: return DevExpress.Utils.HorzAlignment.Near; 
                case 3: return DevExpress.Utils.HorzAlignment.Far; 
                case 4: return DevExpress.Utils.HorzAlignment.Near; 
                case 5: return DevExpress.Utils.HorzAlignment.Near; 
                case 6: return DevExpress.Utils.HorzAlignment.Far; 
                case 7: return DevExpress.Utils.HorzAlignment.Near; 
                case 8: return DevExpress.Utils.HorzAlignment.Far; 
                case 9: return DevExpress.Utils.HorzAlignment.Near; 
                case 10: return DevExpress.Utils.HorzAlignment.Far; 
                case 11: return DevExpress.Utils.HorzAlignment.Near; 
                case 12: return DevExpress.Utils.HorzAlignment.Near; 
                case 13: return DevExpress.Utils.HorzAlignment.Near; 
                case 14: return DevExpress.Utils.HorzAlignment.Near; 
                case 15: return DevExpress.Utils.HorzAlignment.Far; 
                case 16: return DevExpress.Utils.HorzAlignment.Far; 
                case 17: return DevExpress.Utils.HorzAlignment.Far; 
                case 18: return DevExpress.Utils.HorzAlignment.Far; 
                case 19: return DevExpress.Utils.HorzAlignment.Far; 
                case 20: return DevExpress.Utils.HorzAlignment.Far; 
                case 21: return DevExpress.Utils.HorzAlignment.Far; 
                case 22: return DevExpress.Utils.HorzAlignment.Far; 
                case 23: return DevExpress.Utils.HorzAlignment.Far; 
                case 24: return DevExpress.Utils.HorzAlignment.Far; 
                case 25: return DevExpress.Utils.HorzAlignment.Far; 
                case 26: return DevExpress.Utils.HorzAlignment.Far; 
                case 27: return DevExpress.Utils.HorzAlignment.Far; 
                case 28: return DevExpress.Utils.HorzAlignment.Far; 
                case 29: return DevExpress.Utils.HorzAlignment.Far; 
                case 30: return DevExpress.Utils.HorzAlignment.Near; 
                case 31: return DevExpress.Utils.HorzAlignment.Far; 
                case 32: return DevExpress.Utils.HorzAlignment.Far; 
                case 33: return DevExpress.Utils.HorzAlignment.Near; 
                case 34: return DevExpress.Utils.HorzAlignment.Near; 
                case 35: return DevExpress.Utils.HorzAlignment.Near; 
                case 36: return DevExpress.Utils.HorzAlignment.Near; 
                case 37: return DevExpress.Utils.HorzAlignment.Near; 
                case 38: return DevExpress.Utils.HorzAlignment.Near; 
                case 39: return DevExpress.Utils.HorzAlignment.Near; 
                case 40: return DevExpress.Utils.HorzAlignment.Near; 
                case 41: return DevExpress.Utils.HorzAlignment.Far; 
                case 42: return DevExpress.Utils.HorzAlignment.Far; 
                case 43: return DevExpress.Utils.HorzAlignment.Far; 
                case 44: return DevExpress.Utils.HorzAlignment.Far; 
                case 45: return DevExpress.Utils.HorzAlignment.Far; 
                case 46: return DevExpress.Utils.HorzAlignment.Far; 
                case 47: return DevExpress.Utils.HorzAlignment.Far; 
                case 48: return DevExpress.Utils.HorzAlignment.Far; 
                case 49: return DevExpress.Utils.HorzAlignment.Far; 
                case 50: return DevExpress.Utils.HorzAlignment.Center; 
                case 51: return DevExpress.Utils.HorzAlignment.Far; 
                case 52: return DevExpress.Utils.HorzAlignment.Far; 
                case 53: return DevExpress.Utils.HorzAlignment.Far; 
                case 54: return DevExpress.Utils.HorzAlignment.Far; 
                case 55: return DevExpress.Utils.HorzAlignment.Far; 
                case 56: return DevExpress.Utils.HorzAlignment.Far; 
                case 57: return DevExpress.Utils.HorzAlignment.Near; 
                case 58: return DevExpress.Utils.HorzAlignment.Far; 
                case 59: return DevExpress.Utils.HorzAlignment.Far; 
                case 60: return DevExpress.Utils.HorzAlignment.Far; 
                case 61: return DevExpress.Utils.HorzAlignment.Far; 
                case 62: return DevExpress.Utils.HorzAlignment.Far; 
                case 63: return DevExpress.Utils.HorzAlignment.Far; 
                case 64: return DevExpress.Utils.HorzAlignment.Far; 
                case 65: return DevExpress.Utils.HorzAlignment.Far; 
                case 66: return DevExpress.Utils.HorzAlignment.Far; 
                case 67: return DevExpress.Utils.HorzAlignment.Far; 
                case 68: return DevExpress.Utils.HorzAlignment.Far; 
                case 69: return DevExpress.Utils.HorzAlignment.Far; 
                case 70: return DevExpress.Utils.HorzAlignment.Far; 
                case 71: return DevExpress.Utils.HorzAlignment.Far; 
                case 72: return DevExpress.Utils.HorzAlignment.Far; 
                case 73: return DevExpress.Utils.HorzAlignment.Far; 
                case 74: return DevExpress.Utils.HorzAlignment.Far; 
                case 75: return DevExpress.Utils.HorzAlignment.Far; 
                case 76: return DevExpress.Utils.HorzAlignment.Far; 
                case 77: return DevExpress.Utils.HorzAlignment.Far; 
                case 78: return DevExpress.Utils.HorzAlignment.Far; 
                case 79: return DevExpress.Utils.HorzAlignment.Far; 
                case 80: return DevExpress.Utils.HorzAlignment.Far; 
                case 81: return DevExpress.Utils.HorzAlignment.Far; 
                case 82: return DevExpress.Utils.HorzAlignment.Far; 
                case 83: return DevExpress.Utils.HorzAlignment.Far; 
                case 84: return DevExpress.Utils.HorzAlignment.Far; 
                case 85: return DevExpress.Utils.HorzAlignment.Far; 
                case 86: return DevExpress.Utils.HorzAlignment.Far; 
                case 87: return DevExpress.Utils.HorzAlignment.Far; 
                case 88: return DevExpress.Utils.HorzAlignment.Far; 
                case 89: return DevExpress.Utils.HorzAlignment.Far; 
                case 90: return DevExpress.Utils.HorzAlignment.Far; 
                case 91: return DevExpress.Utils.HorzAlignment.Far; 
                case 92: return DevExpress.Utils.HorzAlignment.Far; 
                case 93: return DevExpress.Utils.HorzAlignment.Far; 
                case 94: return DevExpress.Utils.HorzAlignment.Far; 
                case 95: return DevExpress.Utils.HorzAlignment.Far; 
                case 96: return DevExpress.Utils.HorzAlignment.Far; 
                case 97: return DevExpress.Utils.HorzAlignment.Far; 
                case 98: return DevExpress.Utils.HorzAlignment.Far; 
                case 99: return DevExpress.Utils.HorzAlignment.Far; 
                case 100: return DevExpress.Utils.HorzAlignment.Far; 
                case 101: return DevExpress.Utils.HorzAlignment.Far; 
                case 102: return DevExpress.Utils.HorzAlignment.Far; 
                case 103: return DevExpress.Utils.HorzAlignment.Far; 
                case 104: return DevExpress.Utils.HorzAlignment.Far; 
                case 105: return DevExpress.Utils.HorzAlignment.Far; 
                case 106: return DevExpress.Utils.HorzAlignment.Far; 
                case 107: return DevExpress.Utils.HorzAlignment.Far; 
                case 108: return DevExpress.Utils.HorzAlignment.Far; 
                case 109: return DevExpress.Utils.HorzAlignment.Far; 
                case 110: return DevExpress.Utils.HorzAlignment.Far; 
                case 111: return DevExpress.Utils.HorzAlignment.Far; 
                case 112: return DevExpress.Utils.HorzAlignment.Far; 
                case 113: return DevExpress.Utils.HorzAlignment.Far; 
                case 114: return DevExpress.Utils.HorzAlignment.Far; 
                case 115: return DevExpress.Utils.HorzAlignment.Far; 
                case 116: return DevExpress.Utils.HorzAlignment.Far; 
                case 117: return DevExpress.Utils.HorzAlignment.Far; 
                case 118: return DevExpress.Utils.HorzAlignment.Far; 
                case 119: return DevExpress.Utils.HorzAlignment.Far; 
                case 120: return DevExpress.Utils.HorzAlignment.Far; 
                case 121: return DevExpress.Utils.HorzAlignment.Far; 
                case 122: return DevExpress.Utils.HorzAlignment.Near; 
                case 123: return DevExpress.Utils.HorzAlignment.Near; 
                case 124: return DevExpress.Utils.HorzAlignment.Far; 
                case 125: return DevExpress.Utils.HorzAlignment.Near; 
                case 126: return DevExpress.Utils.HorzAlignment.Near; 
                case 127: return DevExpress.Utils.HorzAlignment.Near; 
                case 128: return DevExpress.Utils.HorzAlignment.Center; 
                case 129: return DevExpress.Utils.HorzAlignment.Center; 
                case 130: return DevExpress.Utils.HorzAlignment.Center; 
                case 131: return DevExpress.Utils.HorzAlignment.Center; 
                case 132: return DevExpress.Utils.HorzAlignment.Center; 
                case 133: return DevExpress.Utils.HorzAlignment.Center; 
                case 139: return DevExpress.Utils.HorzAlignment.Far; 
                case 140: return DevExpress.Utils.HorzAlignment.Far; 
                case 141: return DevExpress.Utils.HorzAlignment.Far; 
                case 142: return DevExpress.Utils.HorzAlignment.Near; 
                case 143: return DevExpress.Utils.HorzAlignment.Far; 
                case 144: return DevExpress.Utils.HorzAlignment.Far; 
                case 145: return DevExpress.Utils.HorzAlignment.Far; 
                case 146: return DevExpress.Utils.HorzAlignment.Far; 
                case 147: return DevExpress.Utils.HorzAlignment.Far; 
                case 148: return DevExpress.Utils.HorzAlignment.Far; 
                case 149: return DevExpress.Utils.HorzAlignment.Far; 
                case 150: return DevExpress.Utils.HorzAlignment.Far; 
                case 151: return DevExpress.Utils.HorzAlignment.Far; 
                case 152: return DevExpress.Utils.HorzAlignment.Far; 
                case 153: return DevExpress.Utils.HorzAlignment.Far; 
                case 154: return DevExpress.Utils.HorzAlignment.Far; 
                case 155: return DevExpress.Utils.HorzAlignment.Far; 
                case 156: return DevExpress.Utils.HorzAlignment.Far; 
                case 157: return DevExpress.Utils.HorzAlignment.Far; 
                case 158: return DevExpress.Utils.HorzAlignment.Far; 
                case 159: return DevExpress.Utils.HorzAlignment.Far; 
                case 160: return DevExpress.Utils.HorzAlignment.Far; 
                case 161: return DevExpress.Utils.HorzAlignment.Far; 
                case 162: return DevExpress.Utils.HorzAlignment.Far; 
                case 163: return DevExpress.Utils.HorzAlignment.Far; 
                case 164: return DevExpress.Utils.HorzAlignment.Far; 
                case 165: return DevExpress.Utils.HorzAlignment.Far; 
                case 166: return DevExpress.Utils.HorzAlignment.Far; 
                case 167: return DevExpress.Utils.HorzAlignment.Far; 
                case 168: return DevExpress.Utils.HorzAlignment.Far; 
                case 169: return DevExpress.Utils.HorzAlignment.Far; 
                case 170: return DevExpress.Utils.HorzAlignment.Far; 
                case 171: return DevExpress.Utils.HorzAlignment.Far; 
                case 172: return DevExpress.Utils.HorzAlignment.Far; 
                case 173: return DevExpress.Utils.HorzAlignment.Far;
                case 174: return DevExpress.Utils.HorzAlignment.Far;
                case 175: return DevExpress.Utils.HorzAlignment.Far;
                case 176: return DevExpress.Utils.HorzAlignment.Far;
                case 177: return DevExpress.Utils.HorzAlignment.Far;
                case 178: return DevExpress.Utils.HorzAlignment.Far;
                case 179: return DevExpress.Utils.HorzAlignment.Far;
                case 180: return DevExpress.Utils.HorzAlignment.Far;
                case 181: return DevExpress.Utils.HorzAlignment.Far;
                case 182: return DevExpress.Utils.HorzAlignment.Far;
                case 183: return DevExpress.Utils.HorzAlignment.Far;
                case 184: return DevExpress.Utils.HorzAlignment.Far;
                case 185: return DevExpress.Utils.HorzAlignment.Far;
                case 186: return DevExpress.Utils.HorzAlignment.Far;
                case 187: return DevExpress.Utils.HorzAlignment.Far;
                case 188: return DevExpress.Utils.HorzAlignment.Far;
                case 189: return DevExpress.Utils.HorzAlignment.Far;
                case 190: return DevExpress.Utils.HorzAlignment.Far;
                case 191: return DevExpress.Utils.HorzAlignment.Far;
                case 192: return DevExpress.Utils.HorzAlignment.Far;
                case 193: return DevExpress.Utils.HorzAlignment.Far;
                case 194: return DevExpress.Utils.HorzAlignment.Far;
                case 195: return DevExpress.Utils.HorzAlignment.Far;
                case 196: return DevExpress.Utils.HorzAlignment.Far;
                case 197: return DevExpress.Utils.HorzAlignment.Far;
                case 198: return DevExpress.Utils.HorzAlignment.Far;
                case 199: return DevExpress.Utils.HorzAlignment.Far;
                case 200: return DevExpress.Utils.HorzAlignment.Far;
                case 201: return DevExpress.Utils.HorzAlignment.Far;
                case 202: return DevExpress.Utils.HorzAlignment.Far;
                case 203: return DevExpress.Utils.HorzAlignment.Far;
                case 204: return DevExpress.Utils.HorzAlignment.Far;
                case 205: return DevExpress.Utils.HorzAlignment.Far;
                case 206: return DevExpress.Utils.HorzAlignment.Far;
                case 207: return DevExpress.Utils.HorzAlignment.Far;
                case 208: return DevExpress.Utils.HorzAlignment.Far;
                case 209: return DevExpress.Utils.HorzAlignment.Far;
                case 210: return DevExpress.Utils.HorzAlignment.Far;
                case 211: return DevExpress.Utils.HorzAlignment.Near;
                case 212: return DevExpress.Utils.HorzAlignment.Far;
                case 213: return DevExpress.Utils.HorzAlignment.Far;
                case 214: return DevExpress.Utils.HorzAlignment.Near;
                case 215: return DevExpress.Utils.HorzAlignment.Near;
                case 216: return DevExpress.Utils.HorzAlignment.Far;
                case 217: return DevExpress.Utils.HorzAlignment.Far;
                case 218: return DevExpress.Utils.HorzAlignment.Far;
                case 219: return DevExpress.Utils.HorzAlignment.Far;
                case 220: return DevExpress.Utils.HorzAlignment.Far;
                case 221: return DevExpress.Utils.HorzAlignment.Far;
                case 222: return DevExpress.Utils.HorzAlignment.Far;
                case 223: return DevExpress.Utils.HorzAlignment.Far;



                default: return DevExpress.Utils.HorzAlignment.Center;
            }
        }

        void FormatAllColumns()
        {
            dgPositions.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[0].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[1].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[3].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[6].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[7].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[8].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[8].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[10].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[10].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[12].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[12].DisplayFormat.FormatString = "dd/MMM/yy";

            dgPositions.Columns[13].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[13].DisplayFormat.FormatString = "dd/MMM/yy";

            dgPositions.Columns[14].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[14].DisplayFormat.FormatString = "dd/MMM/yy";
            
            dgPositions.Columns[15].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[15].DisplayFormat.FormatString = "#,##0.##;-#,##0.##";

            dgPositions.Columns[16].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[16].DisplayFormat.FormatString = "#,##0.000;(#,##0.000)";

            dgPositions.Columns[17].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[17].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[18].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[18].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[19].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[19].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[20].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[20].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[21].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[21].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[22].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[22].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[23].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[23].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[24].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[24].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[25].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[25].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[26].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[26].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[27].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[27].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[28].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[28].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[29].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[29].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[31].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[31].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[32].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[32].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[41].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[41].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[42].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[42].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[43].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[43].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[44].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[44].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[45].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[45].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[46].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[46].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[48].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[48].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[49].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[49].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[47].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[47].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[50].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[50].DisplayFormat.FormatString = "dd/MMM/yy";

            dgPositions.Columns[51].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[51].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[52].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[52].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[53].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[53].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[54].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[54].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[55].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[55].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[56].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[56].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[58].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[58].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[59].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[59].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[60].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[60].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[61].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[61].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[62].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[62].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[63].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[63].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[64].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[64].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[65].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[65].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[66].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[66].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[67].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[67].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[68].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[68].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[69].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[69].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[70].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[70].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[71].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[71].DisplayFormat.FormatString = "hh:mm:ss";

            dgPositions.Columns[72].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[72].DisplayFormat.FormatString = "dd/MMM/yy hh:mm:ss";

            dgPositions.Columns[73].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[73].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[74].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[74].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
           
            dgPositions.Columns[75].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[75].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[76].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[76].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[77].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[77].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[79].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[79].DisplayFormat.FormatString = "dd/MMM/yy hh:mm:ss";

            dgPositions.Columns[80].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[80].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[81].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[81].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[82].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[82].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[83].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[83].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[84].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[84].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[85].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[85].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[86].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[86].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[87].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[87].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[88].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[88].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[89].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[89].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[90].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[90].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[91].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[91].DisplayFormat.FormatString = "#,##;(#,##)";

            dgPositions.Columns[92].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[92].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[93].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[93].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[94].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[94].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[95].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[95].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[96].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[96].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[97].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[97].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[98].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[98].DisplayFormat.FormatString = "P2";

            dgPositions.Columns[99].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPositions.Columns[99].DisplayFormat.FormatString = "dd/MMM/yy hh:mm:ss";

            dgPositions.Columns[100].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[100].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[101].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[101].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[102].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[102].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[103].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[103].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[104].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[104].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[105].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[105].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[106].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[106].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[107].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[107].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[108].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[108].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[109].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[109].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[110].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[110].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[111].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[111].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[112].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[112].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[113].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[113].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[114].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[114].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[115].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[115].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[117].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[117].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[116].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[116].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[118].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[118].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[119].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[119].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[125].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[125].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[126].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[126].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[127].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[127].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[139].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[139].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[140].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[140].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[143].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[143].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[144].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[144].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[145].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[145].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[146].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[146].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[147].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[147].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[148].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[148].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[149].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[149].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[150].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[150].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[152].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[152].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            
            dgPositions.Columns[153].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[153].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            
            dgPositions.Columns[155].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[155].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[156].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[156].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[157].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[157].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[158].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[158].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[159].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[159].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[160].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[160].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[161].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[161].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[162].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[162].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[163].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[163].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[174].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[174].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[175].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[175].DisplayFormat.FormatString = "#,##0.##;-#,##0.##";

            dgPositions.Columns[176].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[176].DisplayFormat.FormatString = "#,##0.##;-#,##0.##";

            dgPositions.Columns[177].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[177].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[178].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[178].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[179].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[179].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[180].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[180].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[181].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[181].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[182].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[182].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[183].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[183].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[184].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[184].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[185].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[185].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[186].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[186].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[187].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[187].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[188].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[188].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[189].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[189].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[190].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[190].DisplayFormat.FormatString =  "p2";

            dgPositions.Columns[191].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[191].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[192].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[192].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[193].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[193].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgPositions.Columns[194].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[194].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgPositions.Columns[195].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[195].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);-";

            dgPositions.Columns[196].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[196].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[197].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[197].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%);-";

            dgPositions.Columns[198].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[198].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%);-";

            dgPositions.Columns[199].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[199].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[200].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[200].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgPositions.Columns[201].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[201].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[202].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[202].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[203].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[203].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%);-";

            dgPositions.Columns[204].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[204].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);-";

            dgPositions.Columns[205].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[205].DisplayFormat.FormatString = "#,##0.;(#,##0.);-";

            dgPositions.Columns[206].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[206].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);-";

            dgPositions.Columns[207].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[207].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%);-";

            dgPositions.Columns[208].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[208].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);-";

            dgPositions.Columns[209].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[209].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%);-";

            dgPositions.Columns[210].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[210].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%);-";

            dgPositions.Columns[213].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[213].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[216].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[216].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[217].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[217].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgPositions.Columns[220].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[220].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPositions.Columns[222].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[222].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[223].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[223].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[224].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[224].DisplayFormat.FormatString = "p2";

            dgPositions.Columns[225].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[225].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            
            dgPositions.Columns[227].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPositions.Columns[227].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
        
        }
        
        private void dgPositions_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            if (cmbCustomView.SelectedItem.ToString() == "Custom") { curUtils.Save_Columns(dgPositions); }
        }

        private void dgPositions_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            if (cmbCustomView.SelectedItem.ToString() == "Custom") { curUtils.Save_Columns(dgPositions); }
        }

        public IList CreateData()
        {
            RecordCollection coll = new RecordCollection();
            Record row = new Record();
            coll.Add(row); 
            return coll;
        }
  
        public event ChangeData Carrega_Dados;
        public delegate void ChangeData(int Id_Position, int Id_Ticker,string Table);

        public event ChangeData_Trades_Pos Carrega_Trades_Pos;
        public delegate void ChangeData_Trades_Pos(int Id_Position, string Table);

        public event ChangePortfolio Carrega_Portfolio;
        public delegate void ChangePortfolio(int Id_Portfolio, DateTime Historical);


        private void dgPositions_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {

            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            string curCount = "0";
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);

            foreach (GridSummaryItem si in view.GroupSummary)
            {
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                {
                    items.Add(si);
                    if (si.FieldName == "Column15")
                    {
                        curCount = si.GetDisplayText(values[si], false);
                    }
                }
            }
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);

            string groupText;
            if (Convert.ToInt32(curCount) > 1)
            {
                groupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]).ToString() + " (" + curCount.ToString() + ")";
            }
            else
            {
                groupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]).ToString();
            }
            info.GroupText = groupText;
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            foreach (GridGroupSummaryItem item in items)
            {

                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                if (column == null)
                {
                    MessageBox.Show("Column name is null");
                }
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                if (((rect.X + rect.Width * 0) > view.Columns[4].Width + view.Columns[15].Width))
                {
                    // calculate summary text and boundaries
                    string text = item.GetDisplayText(values[item], false);
                    SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                    int width = Convert.ToInt32(sz.Width) + 1;
                    rect.X += rect.Width - width - 2;
                    rect.Width = width;
                    rect.Y = e.Bounds.Y;
                    rect.Height = e.Bounds.Height - 2;
                    e.Appearance.DrawString(e.Cache, text, rect);
                }
            }

            e.Handled = true;
        
        }

        private void dgPositions_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false,dgPositions);

        }

        private void dgPositions_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgPositions);

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

        private void ShowColumnSelector() { ShowColumnSelector(true, dgPositions); }
        
        bool show = false;

        private void ShowColumnSelector(bool showForm, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (show)
            {
                if (showForm) Nome_Grid.ColumnsCustomization();
            }
            else
            {
                if (showForm) Nome_Grid.DestroyCustomization();
            }
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            ExpandGroups();
        }

        private void ExpandGroups()
        {
            dgPositions.CollapseAllGroups();

            ExpandCounter++;

            if (ExpandCounter > dgPositions.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgPositions.IsValidRowHandle(i)) return;
                if (dgPositions.GetRowLevel(i) < ExpandCounter)
                {
                    dgPositions.SetRowExpanded(i, true);
                }
            }
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgPositions.CollapseAllGroups();
            ExpandCounter = 0;
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Position_Id_LB_" + NestDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Portfolio_" + cmbChoosePortfolio.Text + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log));

                ExportTo(new ExportXlsProvider(fileName));

                OpenFile(fileName);
            }
        }

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Position";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        private void ExportTo(IExportProvider provider)
        {
            IExportProvider Log_Provider;
            Log_Provider = provider;
            
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = dgPositions.CreateExportLink(provider);
            (link as GridViewExportLink).ExpandAll = false;
            link.ExportTo(true);
            provider.Dispose();

            Cursor.Current = currentCursor;
        }

        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch(Exception e)
                {
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgPositions_EndGrouping(object sender, EventArgs e)
        {
            //hlprPositions.LoadViewInfo(); 
        }

        private void radRealTime_CheckedChanged(object sender, EventArgs e)
        {
            if (radRealTime.Checked == true)
            {
                int tempPortfolio = Convert.ToInt16(cmbChoosePortfolio.SelectedValue);

                NestDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name, Id_Port_Type from  dbo.Tb002_Portfolios (nolock) where Id_Port_Type=2 and RT_Position=1 UNION ALL SELECT '-1', 'All Portfolios', '0' ORDER BY Id_Port_Type DESC,Port_Name", "Id_Portfolio", "Port_Name", tempPortfolio);

                if (cmbChoosePortfolio.SelectedValue == null)
                {
                    cmbChoosePortfolio.SelectedValue = 1;
                }

                prevTable = posTableName;
                posTableName = "NESTRT.dbo.FCN_Posicao_Atual()";
                tmrRefreshPos.Stop();
                dtpHistDate.Value = DateTime.Today;
                dtpHistDate.Enabled = false;
                Carrega_Grid();
                tmrRefreshPos.Interval = 500;
                tmrRefreshPos.Start();
                dtgPositions.LookAndFeel.SetSkinStyle("Blue");
                //radRealTime.BackColor = Color.FromArgb(77, 117, 170);
                radRealTime.ForeColor = Color.White;
                //radHistoric.BackColor = Color.FromArgb(77, 117, 170);
                radHistoric.ForeColor = Color.White;
                //chkLinkAllBoxes.BackColor = Colo\r.FromArgb(77, 117, 170);
                chkLinkAllBoxes.ForeColor = Color.White;
            }
        }

        private void radHistoric_CheckedChanged(object sender, EventArgs e)
        {
            if (radHistoric.Checked == true)
            {

                int tempPortfolio = Convert.ToInt16(cmbChoosePortfolio.SelectedValue);

                NestDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name, Id_Port_Type from  dbo.Tb002_Portfolios (nolock) where Discountinued<>1 and Id_Port_Type in (1,2) and Id_portfolio <> 48 UNION ALL SELECT '-1', 'All Portfolios', '0' ORDER BY Id_Port_Type DESC,Port_Name", "Id_Portfolio", "Port_Name", tempPortfolio);

                DateTime histDate = DateTime.Now.AddDays(-1);
                if (histDate.DayOfWeek == DayOfWeek.Sunday) histDate = DateTime.Now.AddDays(-3);

                dtpHistDate.Value = histDate;
                tmrRefreshPos.Stop();
                prevTable = posTableName;
                prevDate = dtpHistDate.Value.ToString("yyyyMMdd");
                posTableName = "NESTDB.dbo.Tb000_Historical_Positions";

                dtpHistDate.Enabled = true;
                Carrega_Grid();
                UpdateGrid();
                tmrRefreshPos.Interval = 2000;
                tmrRefreshPos.Start();
                dtgPositions.LookAndFeel.SetSkinStyle("The Asphalt World");

                //radRealTime.BackColor = Color.FromArgb(239,235,239);
                radRealTime.ForeColor = Color.Black;
                //radHistoric.BackColor = Color.FromArgb(239, 235, 239);
                radHistoric.ForeColor = Color.Black;
                //chkLinkAllBoxes.BackColor = Color.FromArgb(239, 235, 239);
                chkLinkAllBoxes.ForeColor = Color.Black;
            }
        }

        private void dgPositions_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (!dgPositions.IsGroupRow(e.RowHandle))
            {
                if (Convert.ToDouble(dgPositions.GetRowCellValue(e.RowHandle, "Column28")) > 0)
                {
                    if (NestDLL.NUserControl.Instance.User_Id != 17) { e.Appearance.BackColor = Color.FromArgb(222, 254, 235); };
                }

                if (Convert.ToDouble(dgPositions.GetRowCellValue(e.RowHandle, "Column28")) < 0)
                {
                    if (NestDLL.NUserControl.Instance.User_Id != 17) { e.Appearance.BackColor = Color.FromArgb(250, 220, 216); };
                }

                if (Convert.ToDouble(dgPositions.GetRowCellValue(e.RowHandle, "Column28")) == 0)
                {
                    if (NestDLL.NUserControl.Instance.User_Id != 17) { e.Appearance.BackColor = Color.White; };
                }

                if (Convert.ToDouble(dgPositions.GetRowCellValue(e.RowHandle, "Column91")) == 1)
                {
                    e.Appearance.BackColor = Color.Red;
                }
                if (Convert.ToDouble(dgPositions.GetRowCellValue(e.RowHandle, "Column91")) == 2)
                {
                    e.Appearance.BackColor = Color.Yellow;
                }
            }
        }

        private void cmbCustomView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExpandCounter = 0;

            SelectedCustomView = cmbCustomView.SelectedItem.ToString();
            Carrega_Grid();
            UpdateGrid();
            dgPositions.CollapseAllGroups();
            ApplyCustomColumns();
        }

        private void dgPositions_MouseDown(object sender, MouseEventArgs e)
        {
            bolHoldCalc = true;
        }

        private void dgPositions_MouseUp(object sender, MouseEventArgs e)
        {
            bolHoldCalc = false;

            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            GridView view = sender as GridView;
            if (view.State != GridState.ColumnDown) return;

            //info.Column.SortOrder

            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                curOrder = ColumnSortOrder.Descending;
                if (info.Column.SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
                foreach (GridColumn loopGrouping in dgPositions.GroupedColumns)
                {
                    curGrouping = loopGrouping;
                    tempItem = GetGroupByName(dgPositions, info.Column.Name.Replace("col", ""));
                    if(tempItem != null)
                    {
                        dgPositions.GroupSummarySortInfo.Add(tempItem, curOrder, curGrouping);
                    }
                }
                dgPositions.ClearSorting();

                info.Column.SortOrder = curOrder;
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

        private void Add_Totals()
        {
            dgPositions.GroupSummary.Add(SummaryItemType.Count, "Column15", dgPositions.Columns["Column15"], "{0:0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column25", dgPositions.Columns["Column25"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column26", dgPositions.Columns["Column26"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column28", dgPositions.Columns["Column28"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column29", dgPositions.Columns["Column29"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column32", dgPositions.Columns["Column32"], "{0:#,#0.0000}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column42", dgPositions.Columns["Column42"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column43", dgPositions.Columns["Column43"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column44", dgPositions.Columns["Column44"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column47", dgPositions.Columns["Column47"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column46", dgPositions.Columns["Column46"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column52", dgPositions.Columns["Column52"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column53", dgPositions.Columns["Column53"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column54", dgPositions.Columns["Column54"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column55", dgPositions.Columns["Column55"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column66", dgPositions.Columns["Column66"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column67", dgPositions.Columns["Column67"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column68", dgPositions.Columns["Column68"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column69", dgPositions.Columns["Column69"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column70", dgPositions.Columns["Column70"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column73", dgPositions.Columns["Column73"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column76", dgPositions.Columns["Column76"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column74", dgPositions.Columns["Column74"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column86", dgPositions.Columns["Column86"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column87", dgPositions.Columns["Column87"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column88", dgPositions.Columns["Column88"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column89", dgPositions.Columns["Column89"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column95", dgPositions.Columns["Column95"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column98", dgPositions.Columns["Column98"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column103", dgPositions.Columns["Column103"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column104", dgPositions.Columns["Column104"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column105", dgPositions.Columns["Column105"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column108", dgPositions.Columns["Column108"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column112", dgPositions.Columns["Column112"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column113", dgPositions.Columns["Column113"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column114", dgPositions.Columns["Column114"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column115", dgPositions.Columns["Column115"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column116", dgPositions.Columns["Column116"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column139", dgPositions.Columns["Column139"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column140", dgPositions.Columns["Column140"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column143", dgPositions.Columns["Column143"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column144", dgPositions.Columns["Column144"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column150", dgPositions.Columns["Column150"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column155", dgPositions.Columns["Column155"], "{0:P2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column156", dgPositions.Columns["Column156"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column157", dgPositions.Columns["Column157"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column158", dgPositions.Columns["Column158"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column159", dgPositions.Columns["Column158"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column160", dgPositions.Columns["Column158"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column161", dgPositions.Columns["Column158"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column162", dgPositions.Columns["Column158"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column163", dgPositions.Columns["Column158"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column180", dgPositions.Columns["Column180"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column182", dgPositions.Columns["Column182"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column193", dgPositions.Columns["Column193"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column194", dgPositions.Columns["Column194"], "{0:#,#0}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column195", dgPositions.Columns["Column195"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column204", dgPositions.Columns["Column204"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column206", dgPositions.Columns["Column206"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column207", dgPositions.Columns["Column207"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column208", dgPositions.Columns["Column208"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column209", dgPositions.Columns["Column209"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column210", dgPositions.Columns["Column210"], "{0:p2}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column220", dgPositions.Columns["Column220"], "{0:#,#0.00}");

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column222", dgPositions.Columns["Column222"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column223", dgPositions.Columns["Column223"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column224", dgPositions.Columns["Column224"], "{0:#,#0.00}");
            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column226", dgPositions.Columns["Column224"], "{0:#,#0.00}");
        
        
        }

        private string GetColumnName(string NameToGet)
        {
            switch (NameToGet)
            {
                case "Id Position": return "Column0"; 
                case "Id Portfolio": return "Column1"; 
                case "Portfolio": return "Column2"; 
                case "Id Ticker": return "Column3"; 
                case "Ticker": return "Column4"; 
                case "Description": return "Column5"; 
                case "Id Ticker Type": return "Column6"; 
                case "Ticker Type": return "Column7"; 
                case "Id Strategy": return "Column8"; 
                case "Strategy": return "Column9"; 
                case "Id Sub Strategy": return "Column10"; 
                case "Sub Strategy": return "Column11"; 
                case "Last Position": return "Column12"; 
                case "Close_Date": return "Column13"; 
                case "Date Now": return "Column14"; 
                case "Position": return "Column15"; 
                case "Lot Size": return "Column16"; 
                case "Cost Close": return "Column17"; 
                case "Cost Close pC": return "Column18"; 
                case "Last": return "Column19"; 
                case "Last pC": return "Column20"; 
                case "Close": return "Column21"; 
                case "Close pC": return "Column22"; 
                case "NAV": return "Column23"; 
                case "NAV pC": return "Column24"; 
                case "Cash": return "Column25"; 
                case "Cash/NAV": return "Column26"; 
                case "Brokerage": return "Column27"; 
                case "Delta Cash": return "Column28"; 
                case "Delta/NAV": return "Column29"; 
                case "Security Currency": return "Column30"; 
                case "Id Currency Ticker": return "Column31"; 
                case "Delta": return "Column32"; 
                case "Instrument": return "Column33"; 
                case "Asset Class": return "Column34"; 
                case "Sub Industry": return "Column35"; 
                case "Industry": return "Column36"; 
                case "Industry Group": return "Column37"; 
                case "Sector": return "Column38"; 
                case "Underlying Country": return "Column39"; 
                case "Nest Sector": return "Column40"; 
                case "Portfolio Currency": return "Column41"; 
                case "Asset uC": return "Column42"; 
                case "Asset pC": return "Column43"; 
                case "Asset P/L pC": return "Column44"; 
                case "Currency Chg": return "Column45"; 
                case "Currency P/L": return "Column46"; 
                case "Total P/L": return "Column47"; 
                case "Spot USD": return "Column48"; 
                case "Spot": return "Column49"; 
                case "Expiration": return "Column50"; 
                case "P/L %": return "Column51"; 
                case "Gross": return "Column52"; 
                case "Long": return "Column53"; 
                case "Short": return "Column54"; 
                case "Contribution pC": return "Column55"; 
                case "Id Underlying": return "Column56"; 
                case "Underlying": return "Column57"; 
                case "Underlying Acount": return "Column58"; 
                case "Notional": return "Column59"; 
                case "Notional Close": return "Column60"; 
                case "Closed_PL": return "Column61"; 
                case "Display Last ": return "Column62"; 
                case "Display Last pC": return "Column63"; 
                case "Display Cost Close": return "Column64"; 
                case "Display Cost Close pC": return "Column65"; 
                case "BRL": return "Column66"; 
                case "BRL/NAV": return "Column67"; 
                case "Long Delta": return "Column68"; 
                case "Short Delta": return "Column69"; 
                case "Gross Delta": return "Column70"; 
                case "Last Calc": return "Column71"; 
                case "Last Cost Close Calc": return "Column72"; 
                case "Realized": return "Column73"; 
                case "Asset P/L uC": return "Column74"; 
                case "Contribution uC": return "Column75"; 
                case "Delta Quantity": return "Column76"; 
                case "Volatility": return "Column77"; 
                case "Vol Flag": return "Column78"; 
                case "Vol Date": return "Column79"; 
                case "Strike": return "Column80"; 
                case "Rate Year": return "Column81"; 
                case "Rate Period": return "Column82"; 
                case "Days to Expiration": return "Column83"; 
                case "Time to Expiration": return "Column84"; 
                case "Underlying Last": return "Column85"; 
                case "Gamma": return "Column86"; 
                case "Vega": return "Column87"; 
                case "Theta": return "Column88"; 
                case "Rho": return "Column89"; 
                case "Cash Premium": return "Column90"; 
                case "Calc Error Flag": return "Column91"; 
                case "Model Price": return "Column92"; 
                case "Gamma Quantity": return "Column93"; 
                case "Gamma Cash": return "Column94"; 
                case "Gamma/NAV": return "Column95"; 
                case "Display Close": return "Column96"; 
                case "Display Close pC": return "Column97"; 
                case "Dif Contrib": return "Column98"; 
                case "Price Date": return "Column99"; 
                case "Id Instrument": return "Column100"; 
                case "Id Asset Class": return "Column101"; 
                case "Option Type": return "Column102"; 
                case "Initial Position": return "Column103"; 
                case "Quantity Bought": return "Column104"; 
                case "Quantity Sold": return "Column105"; 
                case "Spot USD Close": return "Column106"; 
                case "Spot Close": return "Column107"; 
                case "Cash uC": return "Column108"; 
                case "Last pC Admin": return "Column109"; 
                case "Close pC Admin": return "Column110"; 
                case "Cost Close pC Admin": return "Column111"; 
                case "Total P/L Admin": return "Column112"; 
                case "Realized Admin": return "Column113"; 
                case "Asset P/L pC Admin": return "Column114"; 
                case "Currency P/L Admin": return "Column115"; 
                case "Contribution pC Admin": return "Column116"; 
                case "Last Admin": return "Column117"; 
                case "Close Admin": return "Column118"; 
                case "Cost Close Admin": return "Column119"; 
                case "Id Administrator": return "Column120"; 
                case "Id Base Underlying": return "Column121"; 
                case "Base Underlying": return "Column122"; 
                case "Id Base Underlying Currency": return "Column123"; 
                case "Base Underlying Currency": return "Column124"; 
                case "Bid": return "Column125"; 
                case "Ask": return "Column126"; 
                case "% to Bid/Ask": return "Column127"; 
                case "Id Source Last": return "Column128"; 
                case "Source Last": return "Column129"; 
                case "Flag Last": return "Column130"; 
                case "Id Source Last Admin": return "Column131"; 
                case "Source Last Admin": return "Column132"; 
                case "Flag Last Admin": return "Column133"; 
                case "Id Source Close": return "Column134"; 
                case "Source Close": return "Column135"; 
                case "Flag Close Admin": return "Column136"; 
                case "UpdTime Last": return "Column137"; 
                case "UpdTime last Admin": return "Column138"; 
                case "Asset uC Admin": return "Column139"; 
                case "Theta/NAV": return "Column140"; 
                case "Id Source Close Admin": return "Column141"; 
                case "Source Close Admin": return "Column142"; 
                case "Prev Cash uC": return "Column143"; 
                case "Prev Cash uC Admin": return "Column144"; 
                case "Current NAV pC": return "Column145"; 
                case "Option Intrinsic": return "Column146"; 
                case "Option TV": return "Column147"; 
                case "Option Intrinsic Cash pC": return "Column148"; 
                case "Option TV Cash pC": return "Column149"; 
                case "Cash uC Admin": return "Column150"; 
                default:
                    return null;
            }
        }

        private void Remove_Totals()
        {
            dgPositions.GroupSummary.Clear();
            /*
            for (int i = 0; i < this.dgPositions.GroupSummary.Count; i++)
            {
                this.dgPositions.GroupSummary.RemoveAt(i);
            }*/
        }
        
        private void dgPositions_EndSorting(object sender, EventArgs e)
        {
            //hlprPositions.LoadViewInfo(); 
        }
        
        private void dgPositions_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.dgPositions.GetFocusedRowCellValue("Column0") != null)
            {
                int Id_Position = Convert.ToInt32(this.dgPositions.GetFocusedRowCellValue("Column0"));
                int Id_Ticker = Convert.ToInt32(this.dgPositions.GetFocusedRowCellValue("Column3"));

                if(Carrega_Dados != null) this.Carrega_Dados(Id_Position, Id_Ticker,posTableName);
                if(Carrega_Trades_Pos != null) this.Carrega_Trades_Pos(Id_Position, posTableName);
                base.Focus();

            
            }
        }

        private void cmbChoosePortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            FundChanged = true;

            lblId.Text = cmbChoosePortfolio.SelectedValue.ToString();

            if(dgPositions.FocusedRowHandle>0)
                if (dgPositions.GetRowCellValue(dgPositions.FocusedRowHandle, "Column164").ToString() != "") { prevBook = Convert.ToInt32(dgPositions.GetRowCellValue(dgPositions.FocusedRowHandle, "Column164").ToString()); };

            dgPositions.FocusedRowHandle = -1;

            Carrega_Grid();

            if (this.chkLinkAllBoxes.Checked && cmbChoosePortfolio.SelectedValue.ToString() != "-1")
            {
                Carrega_Portfolio(Convert.ToInt32(cmbChoosePortfolio.SelectedValue.ToString()), dtpHistDate.Value);
            }
        }

        private void dgPositions_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue != null)
            {
                if (e.CellValue.ToString() == "1/1/1900 00:00:00")
                {
                    e.Appearance.ForeColor = Color.White;
                }

                if (NestDLL.NUserControl.Instance.User_Id == 17)
                {
                    if (e.Column.Name == "colColumn15" || e.Column.Name == "colColumn55" || e.Column.Name == "colColumn116")
                    {
                        //Console.WriteLine(e.Appearance.ForeColor.Name.ToString());
                        if (Convert.ToSingle(e.CellValue) > 0 && e.Appearance.ForeColor != Color.Green)
                        {
                            e.Appearance.ForeColor = Color.Green;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }
                        else if (Convert.ToSingle(e.CellValue) < 0 && e.Appearance.ForeColor != Color.Red)
                        {
                            e.Appearance.ForeColor = Color.Red;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                        }
                        else if (Convert.ToSingle(e.CellValue) == 0 && e.Appearance.ForeColor != Color.Black)
                        {
                            e.Appearance.ForeColor = Color.Red;
                            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                        }
                    }
                }
            }
        }

        private void dgPositions_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            switch (e.Level)
            {
                case 0:
                    e.LevelAppearance.BackColor = Color.LightGray;
                    e.LevelAppearance.Font = new Font(dgPositions.Appearance.Row.Font, FontStyle.Bold); ;
                    break;
                case 1: 
                    e.LevelAppearance.BackColor = Color.FromArgb(219, 220, 250);
                    e.LevelAppearance.ForeColor = Color.MidnightBlue;
                    break;
                case 2: e.LevelAppearance.BackColor = Color.FromArgb(230, 230, 250); break;
            }
        }

        private void cmdExpand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgPositions.ExpandAllGroups();
                ExpandCounter = dgPositions.GroupCount;
            }
        }

        private void cmdCollapse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgPositions.CollapseAllGroups();
                ExpandCounter = 0;
            }
        }
        DateTime oldDate = DateTime.Now;
        DateTime lastDate = DateTime.Now;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (oldDate.Day < lastDate.Day)
            {
                dtpHistDate.Value = lastDate;
                oldDate = lastDate;
            }
            lastDate = DateTime.Now;
        }

        private void dgPositions_DoubleClick(object sender, EventArgs e)
        {
            Point pt = dtgPositions.PointToClient(MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = dgPositions.CalcHitInfo(pt);
            if (hi.InRow && hi.Column != null)
            {
                if(hi.Column.Name == "colColumn4")
                {
                    string Id_Position = this.dgPositions.GetFocusedRowCellValue("Column0").ToString();
                    string Id_Ticker = this.dgPositions.GetFocusedRowCellValue("Column3").ToString();
                    string Id_Book = this.dgPositions.GetFocusedRowCellValue("Column164").ToString();
                    string Id_Section = this.dgPositions.GetFocusedRowCellValue("Column172").ToString();

                    string Id_Underlying = this.dgPositions.GetFocusedRowCellValue("Column56").ToString();
                    string Id_Base_Underlying = this.dgPositions.GetFocusedRowCellValue("Column121").ToString();

                    string Id_Ticker_Type = this.dgPositions.GetFocusedRowCellValue("Column6").ToString();
                    string Id_Instrument = this.dgPositions.GetFocusedRowCellValue("Column100").ToString();
                    string Id_Asset_Class = this.dgPositions.GetFocusedRowCellValue("Column101").ToString();

                    string curMessage = "Id_Position: \t" + Id_Position.ToString() + "\n";
                    curMessage = curMessage + "Id_Ticker: \t\t" + Id_Ticker.ToString() + "\n";
                    curMessage = curMessage + "Id_Book: \t\t" + Id_Book.ToString() + "\n";
                    curMessage = curMessage + "Id_Section: \t" + Id_Section.ToString() + "\n\n";

                    curMessage = curMessage + "Id_Underlying: \t" + Id_Underlying.ToString() + "\n";
                    curMessage = curMessage + "Id_Base_Underlying: \t" + Id_Base_Underlying.ToString() + "\n\n";

                    curMessage = curMessage + "Id_Ticker_Type: \t" + Id_Ticker_Type.ToString() + "\n";
                    curMessage = curMessage + "Id_Instrument: \t" + Id_Instrument.ToString() + "\n";
                    curMessage = curMessage + "Id_Asset_Class: \t" + Id_Asset_Class.ToString() + "\n";
                    curMessage = curMessage + "Skipped Refreshes: \t" + SkippedRefresh.ToString() + "\n";

                    DialogResult UserAnswer = MessageBox.Show(curMessage, "Position Ids", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);

                    if (UserAnswer == DialogResult.Yes)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            if (radHistoric.Checked)
                            {
                                curConn.ExecuteNonQuery("UPDATE NESTDB.dbo.Tb000_Historical_Positions SET [Calc Error Flag]=2 WHERE [Id Position]=" + Id_Position);
                                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.PROC_GET_CALCULATE_COST_CLOSE_HISTORICAL " + Id_Position);
                                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.PROC_GET_CALCULATE_FIELDS_HIST " + Id_Position + ", 1");
                            }
                            else
                            {
                                curConn.ExecuteNonQuery("UPDATE NESTRT.dbo.Tb000_Posicao_Atual SET [Calc Error Flag]=2 WHERE [Id Position]=" + Id_Position);
                                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.PROC_GET_CALCULATE_COST_CLOSE " + Id_Position);
                                curConn.ExecuteNonQuery("EXEC NESTDB.dbo.PROC_GET_CALCULATE_FIELDS_HIST " + Id_Position + ", 0");
                            }
                        }
                    }
                }
            }
        }

        private void dgPositions_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name == "colColumn71" || e.Column.Name == "colColumn137" || e.Column.Name == "colColumn138" || e.Column.Name == "colColumn79") 
            {
                DateTime curdate = ((DateTime)e.Value).Date;
                if (((DateTime)e.Value).Date == DateTime.Today)
                {
                    e.DisplayText = ((DateTime)e.Value).TimeOfDay.ToString();
                }
                else
                {
                    e.DisplayText = ((DateTime)e.Value).Date.ToString("dd-MMM-yy");
                }
            }
        }

        private void dtg_Click(object sender, EventArgs e)
        {

        }

        private void dtpHistDate_CloseUp(object sender, EventArgs e)
        {
            Carrega_Grid();
            UpdateGrid();
        }
    }

    #region recordSetValue
    public class RecordCollection : CollectionBase, IBindingList, ITypedList
    {
        public Record this[int i] 
        { 
            get 
            {
                if (i <= this.Count)
                {
                    return (Record)List[i];
                }
                else
                {
                    return null;
                }
            } 
        }
        public void Add(Record record)
        {
            int res = List.Add(record);
            record.owner = this;
            record.Index = res;
        }
        public void SetValue(int row, int col, object val)
        {
            this[row].SetValue(col, val);
        }
        
        public int GetIndex(string searchval) 
        {
            int retval=99999;
            for (int i=0; i < this.Count  ;i++)
            {
                if (this[i] != null)
                {
                    if (searchval == this[i].GetValue(0))
                    {
                        retval = i;
                        break;
                    }
                }
            }

            return retval;
        }

        internal void OnListChanged(Record rec)
        {
            if (listChangedHandler != null) listChangedHandler(this, new ListChangedEventArgs(ListChangedType.ItemChanged, rec.Index, rec.Index));
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] accessors)
        {
            PropertyDescriptorCollection coll = TypeDescriptor.GetProperties(typeof(Record));
            ArrayList list = new ArrayList(coll);
            list.Sort(new PDComparer());
            PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);
            for (int n = 0; n < 232; n++)
            {
                res.Add(list[n] as PropertyDescriptor);
            }
            return res;
        }
        
        class PDComparer : IComparer
        {
            int IComparer.Compare(object a, object b)
            {
                return Comparer.Default.Compare(GetName(a), GetName(b));
            }
            int GetName(object a)
            {
                PropertyDescriptor pd = (PropertyDescriptor)a;
                if (pd.Name.StartsWith("Column")) return Convert.ToInt32(pd.Name.Substring(6));
                return -1;

            }
        }   
        string ITypedList.GetListName(PropertyDescriptor[] accessors) { return ""; }
        public object AddNew() { return null; }
        public bool AllowEdit { get { return true; } }
        public bool AllowNew { get { return false; } }
        public bool AllowRemove { get { return false; } }

        private ListChangedEventHandler listChangedHandler;
        public event ListChangedEventHandler ListChanged
        {
            add { listChangedHandler += value; }
            remove { listChangedHandler -= value; }
        }
        public void AddIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void ApplySort(PropertyDescriptor pd, ListSortDirection dir) { throw new NotSupportedException(); }
        public int Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        public bool IsSorted { get { return false; } }
        public void RemoveIndex(PropertyDescriptor pd) { throw new NotSupportedException(); }
        public void RemoveSort() { throw new NotSupportedException(); }
        public ListSortDirection SortDirection { get { throw new NotSupportedException(); } }
        public PropertyDescriptor SortProperty { get { throw new NotSupportedException(); } }
        public bool SupportsChangeNotification { get { return true; } }
        public bool SupportsSearching { get { return false; } }
        public bool SupportsSorting { get { return false; } }
    }
    
    public class Record
    {
        internal int Index = -1;
        internal RecordCollection owner;
        string[] values = new string[232];
        
        public double Column0 { get { return ValidDouble(values[0]); } set { SetValue(0, value); } }
        public double Column1 { get { return ValidDouble(values[1]); } set { SetValue(1, value); } }
        public string Column2 { get { return values[2]; } set { SetValue(2, value); } }
        public double Column3 { get { return ValidDouble(values[3]); } set { SetValue(3, value); } }
        public string Column4 { get { return values[4]; } set { SetValue(4, value); } }
        public string Column5 { get { return values[5]; } set { SetValue(5, value); } }
        public double Column6 { get { return ValidDouble(values[6]); } set { SetValue(6, value); } }
        public string Column7 { get { return values[7]; } set { SetValue(7, value); } }
        public double Column8 { get { return ValidDouble(values[8]); } set { SetValue(8, value); } }
        public string Column9 { get { return values[9]; } set { SetValue(9, value); } }
        public double Column10 { get { return ValidDouble(values[10]); } set { SetValue(10, value); } }
        public string Column11 { get { return values[11]; } set { SetValue(11, value); } }
        public DateTime Column12 { get { return ValidDateTime(values[12]); } set { SetValue(12, value); } }
        public DateTime Column13 { get { return ValidDateTime(values[13]); } set { SetValue(13, value); } }
        public DateTime Column14 { get { return ValidDateTime(values[14]); } set { SetValue(14, value); } }
        public double Column15 { get { return ValidDouble(values[15]); } set { SetValue(15, value); } }
        public double Column16 { get { return ValidDouble(values[16]); } set { SetValue(16, value); } }
        public double Column17 { get { return ValidDouble(values[17]); } set { SetValue(17, value); } }
        public double Column18 { get { return ValidDouble(values[18]); } set { SetValue(18, value); } }
        public double Column19 { get { return ValidDouble(values[19]); } set { SetValue(19, value); } }
        public double Column20 { get { return ValidDouble(values[20]); } set { SetValue(20, value); } }
        public double Column21 { get { return ValidDouble(values[21]); } set { SetValue(21, value); } }
        public double Column22 { get { return ValidDouble(values[22]); } set { SetValue(22, value); } }
        public double Column23 { get { return ValidDouble(values[23]); } set { SetValue(23, value); } }
        public double Column24 { get { return ValidDouble(values[24]); } set { SetValue(24, value); } }
        public double Column25 { get { return ValidDouble(values[25]); } set { SetValue(25, value); } }
        public double Column26 { get { return ValidDouble(values[26].Replace("%", "")); } set { SetValue(26, value); } }
        public double Column27 { get { return ValidDouble(values[27]); } set { SetValue(27, value); } }
        public double Column28 { get { return ValidDouble(values[28]); } set { SetValue(28, value); } }
        public double Column29 { get { return ValidDouble(values[29].Replace("%", "")); } set { SetValue(29, value); } }
        public string Column30 { get { return values[30]; } set { SetValue(30, value); } }
        public double Column31 { get { return ValidDouble(values[31]); } set { SetValue(31, value); } }
        public double Column32 { get { return ValidDouble(values[32]); } set { SetValue(32, value); } }
        public string Column33 { get { return values[33]; } set { SetValue(33, value); } }
        public string Column34 { get { return values[34]; } set { SetValue(34, value); } }
        public string Column35 { get { return values[35]; } set { SetValue(35, value); } }
        public string Column36 { get { return values[36]; } set { SetValue(36, value); } }
        public string Column37 { get { return values[37]; } set { SetValue(37, value); } }
        public string Column38 { get { return values[38]; } set { SetValue(38, value); } }
        public string Column39 { get { return values[39]; } set { SetValue(39, value); } }
        public string Column40 { get { return values[40]; } set { SetValue(40, value); } }
        public double Column41 { get { return ValidDouble(values[41]); } set { SetValue(41, value); } }
        public double Column42 { get { return ValidDouble(values[42]); } set { SetValue(42, value); } }
        public double Column43 { get { return ValidDouble(values[43]); } set { SetValue(43, value); } }
        public double Column44 { get { return ValidDouble(values[44]); } set { SetValue(44, value); } }
        public double Column45 { get { return ValidDouble(values[45]); } set { SetValue(45, value); } }
        public double Column46 { get { return ValidDouble(values[46]); } set { SetValue(46, value); } }
        public double Column47 { get { return ValidDouble(values[47]); } set { SetValue(47, value); } }
        public double Column48 { get { return ValidDouble(values[48]); } set { SetValue(48, value); } }
        public double Column49 { get { return ValidDouble(values[49]); } set { SetValue(49, value); } }
        public DateTime Column50 { get { return ValidDateTime(values[50]); } set { SetValue(50, value); } }
        public double Column51 { get { return ValidDouble(values[51].Replace("%", "")); } set { SetValue(51, value); } }
        public double Column52 { get { return ValidDouble(values[52].Replace("%", "")); } set { SetValue(52, value); } }
        public double Column53 { get { return ValidDouble(values[53].Replace("%", "")); } set { SetValue(53, value); } }
        public double Column54 { get { return ValidDouble(values[54].Replace("%", "")); } set { SetValue(54, value); } }
        public double Column55 { get { return ValidDouble(values[55].Replace("%", "")); } set { SetValue(55, value); } }
        public double Column56 { get { return ValidDouble(values[56]); } set { SetValue(56, value); } }
        public string Column57 { get { return values[57]; } set { SetValue(57, value); } }
        public double Column58 { get { return ValidDouble(values[58]); } set { SetValue(58, value); } }
        public double Column59 { get { return ValidDouble(values[59]); } set { SetValue(59, value); } }
        public double Column60 { get { return ValidDouble(values[60]); } set { SetValue(60, value); } }
        public double Column61 { get { return ValidDouble(values[61]); } set { SetValue(61, value); } }
        public double Column62 { get { return ValidDouble(values[62]); } set { SetValue(62, value); } }
        public double Column63 { get { return ValidDouble(values[63]); } set { SetValue(63, value); } }
        public double Column64 { get { return ValidDouble(values[64]); } set { SetValue(64, value); } }
        public double Column65 { get { return ValidDouble(values[65]); } set { SetValue(65, value); } }
        public double Column66 { get { return ValidDouble(values[66]); } set { SetValue(66, value); } }
        public double Column67 { get { return ValidDouble(values[67].Replace("%", "")); } set { SetValue(67, value); } }
        public double Column68 { get { return ValidDouble(values[68].Replace("%", "")); } set { SetValue(68, value); } }
        public double Column69 { get { return ValidDouble(values[69].Replace("%", "")); } set { SetValue(69, value); } }
        public double Column70 { get { return ValidDouble(values[70].Replace("%", "")); } set { SetValue(70, value); } }
        public DateTime Column71 { get { return ValidDateTime(values[71]); } set { SetValue(71, value); } }
        public string Column72 { get { return values[72]; } set { SetValue(72, value); } }
        public double Column73 { get { return ValidDouble(values[73]); } set { SetValue(73, value); } }
        public double Column74 { get { return ValidDouble(values[74]); } set { SetValue(74, value); } }
        public double Column75 { get { return ValidDouble(values[75]); } set { SetValue(75, value); } }
        public double Column76 { get { return ValidDouble(values[76]); } set { SetValue(76, value); } }
        public double Column77 { get { return ValidDouble(values[77].Replace("%", "")); } set { SetValue(77, value); } }
        public string Column78 { get { return values[78]; } set { SetValue(78, value); } }
        public DateTime Column79 { get { return ValidDateTime(values[79]); } set { SetValue(79, value); } }
        public double Column80 { get { return ValidDouble(values[80]); } set { SetValue(80, value); } }
        public double Column81 { get { return ValidDouble(values[81]); } set { SetValue(81, value); } }
        public double Column82 { get { return ValidDouble(values[82]); } set { SetValue(82, value); } }
        public double Column83 { get { return ValidDouble(values[83]); } set { SetValue(83, value); } }
        public double Column84 { get { return ValidDouble(values[84]); } set { SetValue(84, value); } }
        public double Column85 { get { return ValidDouble(values[85]); } set { SetValue(85, value); } }
        public double Column86 { get { return ValidDouble(values[86]); } set { SetValue(86, value); } }
        public double Column87 { get { return ValidDouble(values[87]); } set { SetValue(87, value); } }
        public double Column88 { get { return ValidDouble(values[88]); } set { SetValue(88, value); } }
        public double Column89 { get { return ValidDouble(values[89]); } set { SetValue(89, value); } }
        public double Column90 { get { return ValidDouble(values[90]); } set { SetValue(90, value); } }
        public double Column91 { get { return ValidDouble(values[91]); } set { SetValue(91, value); } }
        public double Column92 { get { return ValidDouble(values[92]); } set { SetValue(92, value); } }
        public double Column93 { get { return ValidDouble(values[93]); } set { SetValue(93, value); } }
        public double Column94 { get { return ValidDouble(values[94]); } set { SetValue(94, value); } }
        public double Column95 { get { return ValidDouble(values[95]); } set { SetValue(95, value); } }
        public double Column96 { get { return ValidDouble(values[96]); } set { SetValue(96, value); } }
        public double Column97 { get { return ValidDouble(values[97]); } set { SetValue(97, value); } }
        public double Column98 { get { return ValidDouble(values[98]); } set { SetValue(98, value); } }
        public DateTime Column99 { get { return ValidDateTime(values[99]); } set { SetValue(99, value); } }
        public double Column100 { get { return ValidDouble(values[100]); } set { SetValue(100, value); } }
        public double Column101 { get { return ValidDouble(values[101]); } set { SetValue(101, value); } }
        public string Column102 { get { return Convert.ToString(values[102]); } set { SetValue(102, value); } }
        public double Column103 { get { return ValidDouble(values[103]); } set { SetValue(103, value); } }
        public double Column104 { get { return ValidDouble(values[104]); } set { SetValue(104, value); } }
        public double Column105 { get { return ValidDouble(values[105]); } set { SetValue(105, value); } }
        public double Column106 { get { return ValidDouble(values[106]); } set { SetValue(106, value); } }
        public double Column107 { get { return ValidDouble(values[107]); } set { SetValue(107, value); } }
        public double Column108 { get { return ValidDouble(values[108]); } set { SetValue(108, value); } }
        public double Column109 { get { return ValidDouble(values[109]); } set { SetValue(109, value); } }
        public double Column110 { get { return ValidDouble(values[110]); } set { SetValue(110, value); } }
        public double Column111 { get { return ValidDouble(values[111]); } set { SetValue(111, value); } }
        public double Column112 { get { return ValidDouble(values[112]); } set { SetValue(112, value); } }
        public double Column113 { get { return ValidDouble(values[113]); } set { SetValue(113, value); } }
        public double Column114 { get { return ValidDouble(values[114]); } set { SetValue(114, value); } }
        public double Column115 { get { return ValidDouble(values[115]); } set { SetValue(115, value); } }
        public double Column116 { get { return ValidDouble(values[116]); } set { SetValue(116, value); } }
        public double Column117 { get { return ValidDouble(values[117]); } set { SetValue(117, value); } }
        public double Column118 { get { return ValidDouble(values[118]); } set { SetValue(118, value); } }
        public double Column119 { get { return ValidDouble(values[119]); } set { SetValue(119, value); } }
        public double Column120 { get { return ValidDouble(values[120]); } set { SetValue(120, value); } }
        public double Column121 { get { return ValidDouble(values[121]); } set { SetValue(121, value); } }
        public string Column122 { get { return values[122]; } set { SetValue(122, value); } }
        public double Column123 { get { return ValidDouble(values[123]); } set { SetValue(123, value); } }
        public string Column124 { get { return values[124]; } set { SetValue(124, value); } }
        public double Column125 { get { return ValidDouble(values[125]); } set { SetValue(125, value); } }
        public double Column126 { get { return ValidDouble(values[126]); } set { SetValue(126, value); } }
        public double Column127 { get { return ValidDouble(values[127]); } set { SetValue(127, value); } }
        public double Column128 { get { return ValidDouble(values[128]); } set { SetValue(128, value); } }
        public string Column129 { get { return values[129]; } set { SetValue(129, value); } }
        public string Column130 { get { return values[130]; } set { SetValue(130, value); } }
        public double Column131 { get { return ValidDouble(values[131]); } set { SetValue(131, value); } }
        public string Column132 { get { return values[132]; } set { SetValue(132, value); } }
        public string Column133 { get { return values[133]; } set { SetValue(133, value); } }
        public double Column134 { get { return ValidDouble(values[134]); } set { SetValue(134, value); } }
        public string Column135 { get { return values[135]; } set { SetValue(135, value); } }
        public string Column136 { get { return values[136]; } set { SetValue(136, value); } }
        public DateTime Column137 { get { return ValidDateTime(values[137]); } set { SetValue(137, value); } }
        public DateTime Column138 { get { return ValidDateTime(values[138]); } set { SetValue(138, value); } }
        public double Column139 { get { return ValidDouble(values[139]); } set { SetValue(139, value); } }
        public double Column140 { get { return ValidDouble(values[140]); } set { SetValue(140, value); } }
        public double Column141 { get { return ValidDouble(values[141]); } set { SetValue(141, value); } }
        public string Column142 { get { return values[142]; } set { SetValue(142, value); } }
        public double Column143 { get { return ValidDouble(values[143]); } set { SetValue(143, value); } }
        public double Column144 { get { return ValidDouble(values[144]); } set { SetValue(144, value); } }
        public double Column145 { get { return ValidDouble(values[145]); } set { SetValue(145, value); } }
        public double Column146 { get { return ValidDouble(values[146]); } set { SetValue(146, value); } }
        public double Column147 { get { return ValidDouble(values[147]); } set { SetValue(147, value); } }
        public double Column148 { get { return ValidDouble(values[148]); } set { SetValue(148, value); } }
        public double Column149 { get { return ValidDouble(values[149]); } set { SetValue(149, value); } }
        public double Column150 { get { return ValidDouble(values[150]); } set { SetValue(150, value); } }
        public double Column151 { get { return ValidDouble(values[151]); } set { SetValue(151, value); } }
        public double Column152 { get { return ValidDouble(values[152]); } set { SetValue(152, value); } }
        public double Column153 { get { return ValidDouble(values[153]); } set { SetValue(153, value); } }
        public DateTime Column154 { get { return ValidDateTime(values[154]); } set { SetValue(154, value); } }
        public double Column155 { get { return ValidDouble(values[155]); } set { SetValue(155, value); } }
        public double Column156 { get { return ValidDouble(values[156]); } set { SetValue(156, value); } }
        public double Column157 { get { return ValidDouble(values[157]); } set { SetValue(157, value); } }
        public double Column158 { get { return ValidDouble(values[158]); } set { SetValue(158, value); } }
        public double Column159 { get { return ValidDouble(values[159]); } set { SetValue(159, value); } }
        public double Column160 { get { return ValidDouble(values[160]); } set { SetValue(160, value); } }
        public double Column161 { get { return ValidDouble(values[161]); } set { SetValue(161, value); } }
        public double Column162 { get { return ValidDouble(values[162]); } set { SetValue(162, value); } }
        public double Column163 { get { return ValidDouble(values[163]); } set { SetValue(163, value); } }

        public double Column164 { get { return ValidDouble(values[164]); } set { SetValue(164, value); } }
        public string Column165 { get { return values[165]; } set { SetValue(165, value); } }

        public double Column166 { get { return ValidDouble(values[166]); } set { SetValue(166, value); } }
        public string Column167 { get { return values[167]; } set { SetValue(167, value); } }

        public double Column168 { get { return ValidDouble(values[168]); } set { SetValue(168, value); } }
        public string Column169 { get { return values[169]; } set { SetValue(169, value); } }

        public double Column170 { get { return ValidDouble(values[170]); } set { SetValue(170, value); } }
        public string Column171 { get { return values[171]; } set { SetValue(171, value); } }

        public double Column172 { get { return ValidDouble(values[172]); } set { SetValue(172, value); } }
        public string Column173 { get { return values[173]; } set { SetValue(173, value); } }

        public double Column174 { get { return ValidDouble(values[174]); } set { SetValue(174, value); } }

        public double Column175 { get { return ValidDouble(values[175]); } set { SetValue(175, value); } }
        public double Column176 { get { return ValidDouble(values[176]); } set { SetValue(176, value); } }

        public double Column177 { get { return ValidDouble(values[177]); } set { SetValue(177, value); } }

        public double Column178 { get { return ValidDouble(values[178]); } set { SetValue(178, value); } }
        public double Column179 { get { return ValidDouble(values[179]); } set { SetValue(179, value); } }

        public double Column180 { get { return ValidDouble(values[180]); } set { SetValue(180, value); } }
        public double Column181 { get { return ValidDouble(values[181]); } set { SetValue(181, value); } }

        public double Column182 { get { return ValidDouble(values[182]); } set { SetValue(182, value); } }
        public double Column183 { get { return ValidDouble(values[183]); } set { SetValue(183, value); } }

        public double Column184 { get { return ValidDouble(values[184]); } set { SetValue(185, value); } }
        public double Column185 { get { return ValidDouble(values[185]); } set { SetValue(185, value); } }
        public double Column186 { get { return ValidDouble(values[186]); } set { SetValue(186, value); } }
        public double Column187 { get { return ValidDouble(values[187]); } set { SetValue(187, value); } }
        public double Column188 { get { return ValidDouble(values[188]); } set { SetValue(188, value); } }
        public double Column189 { get { return ValidDouble(values[189]); } set { SetValue(189, value); } }
        public double Column190 { get { return ValidDouble(values[190]); } set { SetValue(190, value); } }
        public double Column191 { get { return ValidDouble(values[191]); } set { SetValue(191, value); } }
        public double Column192 { get { return ValidDouble(values[192]); } set { SetValue(192, value); } }
        public double Column193 { get { return ValidDouble(values[193]); } set { SetValue(193, value); } }
        public double Column194 { get { return ValidDouble(values[194]); } set { SetValue(194, value); } }
        public double Column195 { get { return ValidDouble(values[195]); } set { SetValue(195, value); } }
        public double Column196 { get { return ValidDouble(values[196]); } set { SetValue(196, value); } }

        public double Column197 { get { return ValidDouble(values[197]); } set { SetValue(197, value); } }
        public double Column198 { get { return ValidDouble(values[198]); } set { SetValue(198, value); } }
        public double Column199 { get { return ValidDouble(values[199]); } set { SetValue(199, value); } }
        public double Column200 { get { return ValidDouble(values[200]); } set { SetValue(201, value); } }
        public double Column201 { get { return ValidDouble(values[201]); } set { SetValue(201, value); } }

        public double Column202 { get { return ValidDouble(values[202]); } set { SetValue(202, value); } }
        public double Column203 { get { return ValidDouble(values[203]); } set { SetValue(203, value); } }
        public double Column204 { get { return ValidDouble(values[204]); } set { SetValue(204, value); } }
        public double Column205 { get { return ValidDouble(values[205]); } set { SetValue(205, value); } }

        public double Column206 { get { return ValidDouble(values[206]); } set { SetValue(206, value); } }
        public double Column207 { get { return ValidDouble(values[207]); } set { SetValue(207, value); } }
        public double Column208 { get { return ValidDouble(values[208]); } set { SetValue(208, value); } }
        public double Column209 { get { return ValidDouble(values[209]); } set { SetValue(209, value); } }
        public double Column210 { get { return ValidDouble(values[210]); } set { SetValue(210, value); } }
        public string Column211 { get { return values[211]; } set { SetValue(211, value); } }
        public double Column212 { get { return ValidDouble(values[212]); } set { SetValue(212, value); } }
        public double Column213 { get { return ValidDouble(values[213]); } set { SetValue(213, value); } }
        public string Column214 { get { return values[214]; } set { SetValue(214, value); } }
        public string Column215 { get { return values[215]; } set { SetValue(215, value); } }

        public double Column216 { get { return ValidDouble(values[216]); } set { SetValue(216, value); } }
        public double Column217 { get { return ValidDouble(values[217]); } set { SetValue(217, value); } }
        public string Column218 { get { return values[218]; } set { SetValue(218, value); } }
        public double Column219 { get { return ValidDouble(values[219]); } set { SetValue(219, value); } }
        public double Column220 { get { return ValidDouble(values[220]); } set { SetValue(220, value); } }
        public string Column221 { get { return values[221]; } set { SetValue(221, value); } }
        public double Column222 { get { return ValidDouble(values[222]); } set { SetValue(222, value); } }
        public double Column223 { get { return ValidDouble(values[223]); } set { SetValue(223, value); } }
        public double Column224 { get { return ValidDouble(values[224]); } set { SetValue(224, value); } }
        public double Column225 { get { return ValidDouble(values[225]); } set { SetValue(225, value); } }
        public double Column226 { get { return ValidDouble(values[226]); } set { SetValue(226, value); } }

        public double Column227 { get { return ValidDouble(values[227]); } set { SetValue(227, value); } }
        public double Column228 { get { return ValidDouble(values[228]); } set { SetValue(228, value); } }
        public double Column229 { get { return ValidDouble(values[229]); } set { SetValue(229, value); } }
        public double Column230 { get { return ValidDouble(values[230]); } set { SetValue(230, value); } }
        public double Column231 { get { return ValidDouble(values[231]); } set { SetValue(231, value); } }
        

        public double ValidDouble(string ValueToTest)
        { 
            if (!DBNull.Value.Equals(ValueToTest))
            {
                if(ValueToTest != "")
                {
                    return Convert.ToDouble(ValueToTest);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public DateTime ValidDateTime(string ValueToTest)
        {
            try
            {
                if (!DBNull.Value.Equals(ValueToTest))
                {
                    if (ValueToTest != "" && ValueToTest != null)
                    {
                        return Convert.ToDateTime(ValueToTest);
                    }
                    else
                    {
                        return Convert.ToDateTime("1/1/1900");
                    }
                }
                else
                {
                    return Convert.ToDateTime("1/1/1900");
                }
            }
            catch
            {
                return Convert.ToDateTime("1/1/1900");
            }
        }

        public string GetValue(int index)
        {
            if (index < values.Length)
            {
                if (values[index] != null)
                {
                    return values[index];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public void SetValue(int index, object val)
        {
           /*
            if (val.ToString() == "" || values[index] == null)
            {
                values[index] = "0";
            }
            */

            if (values[index] != val.ToString())
            {
                values[index] = val.ToString();
                //if (index == 19)
                //{
                   // values[index] = getFormat(index, values[index]);
                    //Columns["Expiration"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //}
                /*
                if (this.owner != null)
                {
                    //this.owner.OnListChanged(this);
                    //Application.DoEvents();
                }
                 */ 
            }
        }
        /*
        string zgetFormat(int colNumber, string strValue)
        { 
            strValue = strValue.Replace(".","");
            //strValue = strValue.Replace(",",".");
            if (strValue == "") { return ""; }  else {
            switch (colNumber)
            {
               case 0: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 1: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 2: return strValue; break;
                case 3: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 4: return strValue; break;
                case 5: return strValue; break;
                case 6: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 7: return strValue; break;
                case 8: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 9: return strValue; break;
                case 10: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 11: return strValue; break;
                case 12: return String.Format("{0:dd/MMM}", Convert.ToDateTime(strValue)); break;
                case 13: return String.Format("{0:dd/MMM}", Convert.ToDateTime(strValue)); break;
                case 14: return String.Format("{0:dd/MMM}", Convert.ToDateTime(strValue)); break;
                case 15: return String.Format("{0:#,##0.##}", Convert.ToDouble(strValue)); break;
                case 16: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 17: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 18: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 19: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 20: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 21: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 22: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 23: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 24: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 25: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 26: return String.Format("{0:0.00##%}", Convert.ToDouble(strValue)); break;
                case 27: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 28: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 29: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 30: return strValue; break;
                case 31: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 32: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 33: return strValue; break;
                case 34: return strValue; break;
                case 35: return strValue; break;
                case 36: return strValue; break;
                case 37: return strValue; break;
                case 38: return strValue; break;
                case 39: return strValue; break;
                case 40: return strValue; break;
                case 41: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 42: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 43: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 44: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 45: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 46: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 47: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 48: return String.Format("{0:#,##0.0000}", Convert.ToDouble(strValue)); break;
                case 49: return String.Format("{0:#,##0.0000}", Convert.ToDouble(strValue)); break;
                case 50: return String.Format("{0:dd/MMM/yy}", Convert.ToDateTime(strValue)); break;
                case 51: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 52: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 53: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 54: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 55: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 56: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 57: return strValue; break;
                case 58: return String.Format("{0:0}", Convert.ToDouble(strValue)); break;
                case 59: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 60: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 61: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 62: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 63: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 64: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 65: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 66: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 67: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 68: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 69: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 70: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 71: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 72: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 73: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 74: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 75: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 76: return String.Format("{0:#,##0}", Convert.ToDouble(strValue)); break;
                case 77: return String.Format("{0:0.00%}", Convert.ToDouble(strValue)); break;
                case 78: return String.Format("{0:#,##0.0000}", Convert.ToDouble(strValue)); break;
                case 79: return String.Format("{0:dd/MMM/yy}", Convert.ToDouble(strValue)); break;
                case 80: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 81: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 82: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 83: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 84: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 85: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 86: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 87: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 88: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;
                case 89: return String.Format("{0:#,##0.00}", Convert.ToDouble(strValue)); break;

                default: return strValue; break;
            }
        }

        }
        */
        //</label1>
    }
    #endregion

}