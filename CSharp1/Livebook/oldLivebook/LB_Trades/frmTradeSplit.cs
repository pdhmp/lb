using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;



using NestDLL;
using DevExpress.XtraEditors.Repository;
using System.Threading;


namespace TradeSplit
{
    public partial class frmTradeSplit : Form 
    {
        List<SplitItem> SplitItemList = new List<SplitItem>();
        List<SplitItem> InitialItemList = new List<SplitItem>();

        BindingSource bndGridSource = new BindingSource();

        DateTime LastUpdateTime = new DateTime(1900, 01, 01);

        Dictionary<int, string> OverrideTypeList = new Dictionary<int, string>();

        double BasePercentMH = 0.75;
        double BasePercentNFUND = 0.25;
        double BasePercentFIA = 0.75;
        double BasePercentHedge = 0.25;

        public bool hasLoaded = false;
        bool MouseIsDown = false;
        bool GridInitialized = false;

        DateTime RunDate = DateTime.Now.Date;
        DateTime BaseDate = DateTime.Now.Date;

        public frmTradeSplit()
        {
            InitializeComponent();
        }

        private void LoadOverrideTypeList()
        {
            OverrideTypeList.Clear();

            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT * FROM dbo.Tb353_Override_Types";

                DataTable curTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow row in curTable.Rows)
                {
                    OverrideTypeList.Add((int)NestDLL.Utils.ParseToDouble(row["Override_Id"]), row["Override_Description"].ToString());
                }
            }
        }

        private void frmTradeSplit_Load(object sender, EventArgs e)
        {
            LoadOverrideTypeList();

            using (newNestConn curConn = new newNestConn())
            {
                BaseDate = curConn.Return_DateTime("SELECT MAX(Data_PL) FROM dbo.Tb025_Valor_PL WHERE Data_PL<'" + RunDate.ToString("yyyy-MM-dd") + "'");
            }

            DevExpress.XtraGrid.GridControl curGrid = (DevExpress.XtraGrid.GridControl)this.dtg;

            curGrid.LookAndFeel.UseDefaultLookAndFeel = false;
            curGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            curGrid.LookAndFeel.SetSkinStyle("Blue");

            bndGridSource.DataSource = SplitItemList;
            dtg.DataSource = bndGridSource;
            InitializeGrid();

            dgTradeSplit.ExpandAllGroups();
            
            tmrUpdate.Start();

            RefreshGrid();
        }

        private void InitializeGrid()
        {
            if (!GridInitialized)
            {
                //curUtils.SetColumnStyle(dgTradeSplit, 1);

                dgTradeSplit.ColumnPanelRowHeight = 32;

                dgTradeSplit.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

                foreach (DevExpress.XtraGrid.Columns.GridColumn curColumn in dgTradeSplit.Columns)
                {
                    if (curColumn.Name.Substring(0, 5) == "colId"
                        || curColumn.Name.Contains("Initial")
                        || curColumn.Name.Contains("Trades")
                        || curColumn.Name.Contains("TotalFixed")
                        )
                    {
                        curColumn.Visible = false;
                    }

                    curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    curColumn.DisplayFormat.FormatString = "#,##0;(#,##0);-";
                }

                // Add Edit Button
                dgTradeSplit.Columns.AddField("Edit");
                dgTradeSplit.Columns["Edit"].VisibleIndex = 0;
                dgTradeSplit.Columns["Edit"].Width = 55;
                RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
                item4.Buttons[0].Tag = 1;
                item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item4.Buttons[0].Caption = "Edit";
                dtg.RepositoryItems.Add(item4);
                dgTradeSplit.Columns["Edit"].ColumnEdit = item4;
                dgTradeSplit.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

                // Add Update
                dgTradeSplit.Columns.AddField("Update");
                dgTradeSplit.Columns["Update"].VisibleIndex = 0;
                dgTradeSplit.Columns["Update"].Width = 55;
                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
                item5.Buttons[0].Tag = 2;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Update";
                dtg.RepositoryItems.Add(item5);
                dgTradeSplit.Columns["Update"].ColumnEdit = item5;
                dgTradeSplit.Columns["Update"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

                

                GridInitialized = true;
            }
        }

        private void RefreshGrid()
        {
            if (!MouseIsDown)
            {
                LoadBrokerData();
                LoadSplitStatus();
                LoadInitialData();
                LoadTradesData();
                dgTradeSplit.RefreshData();
            }
        }

        private void LoadBrokerData()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT Broker, A.IdSecurity,D.IdInstrument, NestTicker, ExchangeTicker, Book, Section, IdBroker, [IdBook] ,[IdSection] ,SUM(B.Quantidade) AS BrokerNet, SUM(CASE WHEN B.Quantidade>0 THEN B.Quantidade ELSE 0 END) AS BrokerBuy, SUM(CASE WHEN B.Quantidade<0 THEN B.Quantidade ELSE 0 END) AS BrokerSell, G.Override_Type " +
                        " FROM dbo.Tb012_Orders A " +
                        " INNER JOIN dbo.Tb013_Trades B " +
                        " ON A.IdOrder = B.Id_Ordem " +
                        " INNER JOIN dbo.VW_PortAccounts C " +
                        " ON A.IdAccount = C.Id_Account " +
                        " INNER JOIN dbo.Tb001_Securities D " +
                        " ON A.IdSecurity = D.IdSecurity " +
                        " INNER JOIN dbo.Tb404_Section E " +
                        " ON A.IdSection = E.Id_Section " +
                        " INNER JOIN dbo.Tb400_Books F " +
                        " ON A.IdBook = F.Id_Book " +
                        " LEFT JOIN (SELECT * FROM dbo.Tb352_Trade_Alocation_Override WHERE Override_Date='" + RunDate.ToString("yyyy-MM-dd") + "') G " +
                        " ON A.IdBook = G.Id_Book " +
                        " AND A.IdBroker = G.Id_Broker " +
                        " AND A.IdSecurity = G.Id_Ticker " +
                        " AND A.IdBook = G.Id_Book " +
                        " AND A.IdSection = G.Id_Section " +
                        " WHERE OpenOrderDate='" + RunDate.ToString("yyyy-MM-dd") + "' " +
                        " AND Id_Portfolio=48 AND IdStatusOrder<>4 AND B.StatusTrade<>4 " +
                        " GROUP BY Broker,A.IdSecurity,D.IdInstrument,IdBroker,[IdBook],[IdSection],NestTicker,ExchangeTicker,Book,Section,G.Override_Type";


                DataTable curTable = curConn.Return_DataTable(SQLString);

                List<SplitItem> DBSplitItems = new List<SplitItem>();

                foreach (DataRow curRow in curTable.Rows)
                {
                    SplitItem curSplitItem = new SplitItem();
                    curSplitItem.Broker = curRow["Broker"].ToString();
                    curSplitItem.NestTicker = curRow["NestTicker"].ToString();
                    curSplitItem.ExchangeTicker = curRow["ExchangeTicker"].ToString();
                    curSplitItem.Book = curRow["Book"].ToString();
                    curSplitItem.Section = curRow["Section"].ToString();
                    curSplitItem.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                    curSplitItem.IdBroker = (int)NestDLL.Utils.ParseToDouble(curRow["IdBroker"]);
                    curSplitItem.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["IdBook"]);
                    curSplitItem.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["IdSection"]);
                    curSplitItem.IdInstrument = (int)NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                    curSplitItem.BrokerNet = NestDLL.Utils.ParseToDouble(curRow["BrokerNet"]);
                    curSplitItem.BrokerBuy = NestDLL.Utils.ParseToDouble(curRow["BrokerBuy"]);
                    curSplitItem.BrokerSell = NestDLL.Utils.ParseToDouble(curRow["BrokerSell"]);
                    curSplitItem.IdOverrideType = (int)NestDLL.Utils.ParseToDouble(curRow["Override_Type"]);
                    curSplitItem.OverrideTypeList = OverrideTypeList;

                    if (SplitItemList.Contains(curSplitItem))
                    {
                        SplitItem tempSplitItem = SplitItemList[SplitItemList.IndexOf(curSplitItem)];
                        tempSplitItem.BrokerNet = curSplitItem.BrokerNet;
                        tempSplitItem.BrokerBuy = curSplitItem.BrokerBuy;
                        tempSplitItem.BrokerSell = curSplitItem.BrokerSell;
                        tempSplitItem.IdOverrideType = curSplitItem.IdOverrideType;
                    }
                    else
                    {
                        SplitItemList.Add(curSplitItem);
                    }

                    DBSplitItems.Add(curSplitItem);
                }

                List<SplitItem> ItemsToDelete = new List<SplitItem>();

                foreach (SplitItem curSplitItem in SplitItemList)
                {
                    if (!DBSplitItems.Contains(curSplitItem))
                    {
                        ItemsToDelete.Add(curSplitItem);
                    }
                }

                foreach (SplitItem curSplitItem in ItemsToDelete)
                {
                    if (SplitItemList.Contains(curSplitItem))
                    {
                        SplitItemList.Remove(curSplitItem);
                    }
                }
            }
        }

        private void LoadSplitStatus()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT * FROM dbo.FCN_GET_Trade_Split_Status()";

                DataTable curTable = curConn.Return_DataTable(SQLString);
                
                foreach (DataRow curRow in curTable.Rows)
                {
                    SplitItem curSplitStatus = new SplitItem();
                    curSplitStatus.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id Ticker"]);
                    curSplitStatus.IdBroker = (int)NestDLL.Utils.ParseToDouble(curRow["Id Broker"]);
                    curSplitStatus.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    curSplitStatus.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section"]);

                    if (SplitItemList.Contains(curSplitStatus))
                    {
                        SplitItem tempSplitItem = SplitItemList[SplitItemList.IndexOf(curSplitStatus)];
                        tempSplitItem.MHBuy = NestDLL.Utils.ParseToDouble(curRow["MH_Buy"]);
                        tempSplitItem.MHSell = NestDLL.Utils.ParseToDouble(curRow["MH_Sell"]);
                        tempSplitItem.NFUNDBuy = NestDLL.Utils.ParseToDouble(curRow["NFUND_Buy"]);
                        tempSplitItem.NFUNDSell = NestDLL.Utils.ParseToDouble(curRow["NFUND_Sell"]);
                        tempSplitItem.FIABuy = NestDLL.Utils.ParseToDouble(curRow["Bravo_Buy"]);
                        tempSplitItem.FIASell = NestDLL.Utils.ParseToDouble(curRow["Bravo_Sell"]);
                        tempSplitItem.ArbBuy = NestDLL.Utils.ParseToDouble(curRow["Arb_Buy"]);
                        tempSplitItem.ArbSell = NestDLL.Utils.ParseToDouble(curRow["Arb_Sell"]);
                        tempSplitItem.PrevBuy = NestDLL.Utils.ParseToDouble(curRow["Prev_Buy"]);
                        tempSplitItem.PrevSell = NestDLL.Utils.ParseToDouble(curRow["Prev_Sell"]);
                        tempSplitItem.HedgeBuy = NestDLL.Utils.ParseToDouble(curRow["Hedge_Buy"]);
                        tempSplitItem.HedgeSell = NestDLL.Utils.ParseToDouble(curRow["Hedge_Sell"]);
                    }
                }
            }
        }

        private void LoadInitialData()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT [Id Portfolio],[Id Ticker],[Id Book],[ID Section],[Id Instrument],ROUND(coalesce([Position],0),2) AS InitialPosition" +
                        " FROM Tb000_Historical_Positions " +
                        " WHERE [Date Now] = '" + BaseDate.ToString("yyyy-MM-dd") + "' AND ROUND(coalesce([Position],0),2)<>0";


                DataTable curTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in curTable.Rows)
                {
                    SplitItem curSplitItem = new SplitItem();

                    int IdPortfolio = (int)NestDLL.Utils.ParseToDouble(curRow["Id Portfolio"]);
                    curSplitItem.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id Ticker"]);
                    curSplitItem.IdBroker = -1;
                    curSplitItem.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    curSplitItem.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section"]);
                    curSplitItem.IdInstrument = (int)NestDLL.Utils.ParseToDouble(curRow["Id Instrument"]);
                    curSplitItem.OverrideTypeList = OverrideTypeList;

                    switch (IdPortfolio)
                    {
                        case 4: curSplitItem.NFUNDInitial = NestDLL.Utils.ParseToDouble(curRow["InitialPosition"]); break;
                        case 10: curSplitItem.FIAInitial = NestDLL.Utils.ParseToDouble(curRow["InitialPosition"]); break;
                        case 38: curSplitItem.ArbInitial = NestDLL.Utils.ParseToDouble(curRow["InitialPosition"]); break;
                        case 43: curSplitItem.MHInitial = NestDLL.Utils.ParseToDouble(curRow["InitialPosition"]); break;
                        case 50: curSplitItem.PrevInitial = NestDLL.Utils.ParseToDouble(curRow["InitialPosition"]); break;
                        case 60: curSplitItem.HedgeInitial = NestDLL.Utils.ParseToDouble(curRow["InitialPosition"]); break;
                        default: break;
                    }

                    if (InitialItemList.Contains(curSplitItem))
                    {
                        SplitItem tempSplitItem = InitialItemList[InitialItemList.IndexOf(curSplitItem)];

                        switch (IdPortfolio)
                        {
                            case 4: tempSplitItem.NFUNDInitial = curSplitItem.NFUNDInitial; break;
                            case 10: tempSplitItem.FIAInitial = curSplitItem.FIAInitial; break;
                            case 38: tempSplitItem.ArbInitial = curSplitItem.ArbInitial; break;
                            case 43: tempSplitItem.MHInitial = curSplitItem.MHInitial; break;
                            case 50: tempSplitItem.PrevInitial = curSplitItem.PrevInitial; break;
                            case 60: tempSplitItem.HedgeInitial = curSplitItem.HedgeInitial; break;
                            default: break;
                        }
                    }
                    else
                    {
                        InitialItemList.Add(curSplitItem);
                    }
                }
            }
        }

        private void LoadTradesData()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT D.Id_Portfolio,B.[IdBook],B.[IdSection],b.IdSecurity,SUM(a.Quantidade) AS NetTrades " +
                        " FROM dbo.Tb013_Trades AS a (nolock) INNER JOIN " +
                        " 	dbo.Tb012_Orders AS b (nolock) ON b.IdOrder = a.Id_Ordem INNER JOIN " +
                        " 	dbo.VW_PortAccounts D (nolock) ON B.IdAccount = D.Id_Account " +
                        " WHERE (a.StatusTrade <> 4) AND b.IdStatusOrder<>4  " +
                        " AND (a.Data_Trade >= '" + RunDate.ToString("yyyy-MM-dd") + "' AND a.Data_Trade <= '" + RunDate.AddDays(1).ToString("yyyy-MM-dd") + "')  " +
                        " AND D.Id_portfolio<>48 " +
                        " GROUP BY D.Id_Portfolio,B.[IdBook],B.[IdSection],b.IdSecurity";

                DataTable curTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in curTable.Rows)
                {
                    SplitItem curSplitItem = new SplitItem();

                    int IdPortfolio = (int)NestDLL.Utils.ParseToDouble(curRow["Id_portfolio"]);
                    curSplitItem.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                    curSplitItem.IdBroker = -1;
                    curSplitItem.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["IdBook"]);
                    curSplitItem.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["IdSection"]);
                    curSplitItem.IdInstrument = curConn.Return_Int("SELECT IdInstrument FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE IdSecurity=" + curSplitItem.IdSecurity);
                    curSplitItem.OverrideTypeList = OverrideTypeList;

                    switch (IdPortfolio)
                    {
                        case 4: curSplitItem.NFUNDTrades = NestDLL.Utils.ParseToDouble(curRow["NetTrades"]); break;
                        case 10: curSplitItem.FIATrades = NestDLL.Utils.ParseToDouble(curRow["NetTrades"]); break;
                        case 38: curSplitItem.ArbTrades = NestDLL.Utils.ParseToDouble(curRow["NetTrades"]); break;
                        case 43: curSplitItem.MHTrades = NestDLL.Utils.ParseToDouble(curRow["NetTrades"]); break;
                        case 50: curSplitItem.PrevTrades = NestDLL.Utils.ParseToDouble(curRow["NetTrades"]); break;
                        case 60: curSplitItem.HedgeTrades = NestDLL.Utils.ParseToDouble(curRow["NetTrades"]); break;
                        default: break;
                    }

                    if (InitialItemList.Contains(curSplitItem))
                    {
                        SplitItem tempSplitItem = InitialItemList[InitialItemList.IndexOf(curSplitItem)];

                        switch (IdPortfolio)
                        {
                            case 4: tempSplitItem.NFUNDTrades = curSplitItem.NFUNDTrades; break;
                            case 10: tempSplitItem.FIATrades = curSplitItem.FIATrades; break;
                            case 38: tempSplitItem.ArbTrades = curSplitItem.ArbTrades; break;
                            case 43: tempSplitItem.MHTrades = curSplitItem.MHTrades; break;
                            case 50: tempSplitItem.PrevTrades = curSplitItem.PrevTrades; break;
                            case 60: tempSplitItem.HedgeTrades = curSplitItem.HedgeTrades; break;
                            default: break;
                        }
                    }
                    else
                    {
                        InitialItemList.Add(curSplitItem);
                    }
                }
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
            //UpdateSplit(384, 2, 2);
        }

        private void cmdSplitAll_Click(object sender, EventArgs e)
        {
            cmdSplitAll.Enabled = false;
            using (newNestConn curConn = new newNestConn())
            {
                BasePercentMH = curConn.Return_Double("SELECT Return_Value from dbo.FCN_GET_PRICE(13663,'" + RunDate.ToString("yyyy-MM-dd") + "',14,0,2,0,0,0)");
                BasePercentNFUND = curConn.Return_Double("SELECT Return_Value from dbo.FCN_GET_PRICE(5228,'" + RunDate.ToString("yyyy-MM-dd") + "',14,0,2,0,0,0)");
                BasePercentFIA = curConn.Return_Double("SELECT Return_Value from dbo.FCN_GET_PRICE(21140,'" + RunDate.ToString("yyyy-MM-dd") + "',14,0,2,0,0,0)");
                BasePercentHedge = curConn.Return_Double("SELECT Return_Value from dbo.FCN_GET_PRICE(683986,'" + RunDate.ToString("yyyy-MM-dd") + "',14,0,2,0,0,0)");
                
                string SQLString = "SELECT A.IdSecurity,[IdBook],[IdSection] " +
                           " FROM dbo.Tb012_Orders A " +
                           " INNER JOIN dbo.Tb013_Trades B " +
                           " ON A.IdOrder = B.Id_Ordem " +
                           " INNER JOIN dbo.VW_PortAccounts C " +
                           " ON A.IdAccount = C.Id_Account " +
                           " WHERE OpenOrderDate='" + RunDate.ToString("yyyy-MM-dd") + "' AND Id_Portfolio=48 AND IdStatusOrder<>4 AND B.StatusTrade<>4 " +
                           " GROUP BY Broker,A.IdSecurity,IdBroker,[IdBook],[IdSection]";


                DataTable curTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in curTable.Rows)
                {
                    UpdateSplit((int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]), (int)NestDLL.Utils.ParseToDouble(curRow["IdBook"]), (int)NestDLL.Utils.ParseToDouble(curRow["IdSection"]));
                }
            }
            UpdateAllDB();
            cmdSplitAll.Enabled = true;
            RefreshGrid();
        }

        private void UpdateSplit(int IdSecurity, int IdBook, int IdSection)
        {
            List<SplitItem> ItemsToSplit = new List<SplitItem>();

            foreach (SplitItem curSplitItem in SplitItemList)
            {
                if (curSplitItem.IdSecurity == IdSecurity && curSplitItem.IdBook == IdBook && curSplitItem.IdSection == IdSection)
                {
                    ItemsToSplit.Add(curSplitItem);
                }
            }

            SplitItem tempSplitItem = new SplitItem();
            tempSplitItem.IdBroker = -1;
            tempSplitItem.IdSecurity = IdSecurity;
            tempSplitItem.IdBook = IdBook;
            tempSplitItem.IdSection = IdSection;

            SplitItem InitialSplitItem = new SplitItem();

            if (InitialItemList.Contains(tempSplitItem))
            {
                InitialSplitItem = InitialItemList[InitialItemList.IndexOf(tempSplitItem)];
            }

            double FundAcctBroker = 0;
            double PurchasesBroker = 0;
            double SalesBroker = 0;

            double TotalFinalAll = 0;
            double PurchasesALL = 0;
            double SalesALL = 0;

            double TargetDayTradeALL = 0;

            double Divisor = 100;

            double PercentMH = BasePercentMH;
            double PercentNFUND = BasePercentNFUND;
            double PercentHedge = BasePercentHedge;
            double PercentFIA = BasePercentFIA;

            // ========================================= MH NFUND SECTION ======================================================

            foreach (SplitItem curSplitItem in ItemsToSplit)
            {
                if (curSplitItem.IdInstrument == 4 || curSplitItem.IdInstrument == 16)
                {
                    Divisor = 5;
                }

                if (curSplitItem.IdBroker != 22 && curSplitItem.IdBroker != 106)
                {
                    FundAcctBroker += curSplitItem.BrokerNet;
                    PurchasesBroker += curSplitItem.BrokerBuy;
                    SalesBroker += curSplitItem.BrokerSell;
                }
            }

            double TotalFixedMH = InitialSplitItem.MHInitial + InitialSplitItem.MHTrades;
            double TotalFixedNFUND = InitialSplitItem.NFUNDInitial + InitialSplitItem.NFUNDTrades;

            if (TotalFixedMH == 0 && TotalFixedNFUND != 0) { PercentNFUND = 1; PercentMH = 0; }
            if (TotalFixedNFUND == 0 && TotalFixedMH != 0) { PercentNFUND = 0; PercentMH = 1; }

            TotalFinalAll = TotalFixedMH + TotalFixedNFUND + FundAcctBroker;

            double TargetFinalMH = TotalFinalAll * PercentMH;
            double TargetFinalNFUND = TotalFinalAll * PercentNFUND;

            double NeededMH = TargetFinalMH - TotalFixedMH;
            double NeededNFUND = TargetFinalNFUND - TotalFixedNFUND;

            double PurchasesMH = 0;
            double PurchasesNFUND = 0;
            double SalesMH = 0;
            double SalesNFUND = 0;

            if (NeededMH > 0 && PurchasesBroker > 0) PurchasesMH = NeededMH;
            if (NeededMH > 0 && NeededMH > PurchasesBroker) PurchasesMH = PurchasesBroker;

            if (NeededNFUND > 0 && PurchasesBroker > 0) PurchasesNFUND = NeededNFUND;
            if (NeededNFUND > 0 && NeededNFUND > PurchasesBroker) PurchasesNFUND = PurchasesBroker;

            if (NeededMH < 0 && SalesBroker < 0) SalesMH = NeededMH;
            if (NeededMH < 0 && NeededMH < SalesBroker) SalesMH = SalesBroker;

            if (NeededNFUND < 0 && SalesBroker < 0) SalesNFUND = NeededNFUND;
            if (NeededNFUND < 0 && NeededNFUND < SalesBroker) SalesNFUND = SalesBroker;

            double TargetAlocMH = PurchasesMH + SalesMH;
            double TargetAlocNFUND = PurchasesNFUND + SalesNFUND;

            PurchasesALL = PurchasesMH + PurchasesNFUND;
            SalesALL = SalesMH + SalesNFUND;

            TargetDayTradeALL = 0;

            if (Math.Abs(PurchasesALL - PurchasesMH - PurchasesNFUND) > Math.Abs(SalesALL - SalesMH - SalesNFUND))
                TargetDayTradeALL = Math.Abs(PurchasesBroker - PurchasesMH - PurchasesNFUND);
            else
                TargetDayTradeALL = Math.Abs(SalesBroker - SalesMH - SalesNFUND);

            double TargetDayTradeMH = TargetDayTradeALL * PercentMH;
            double TargetDayTradeNFUND = TargetDayTradeALL * PercentNFUND;

            foreach (SplitItem curSplitItem in ItemsToSplit)
            {
                if (curSplitItem.IdBroker != 22 && curSplitItem.IdBroker != 106 && curSplitItem.IdOverrideType == 0)
                {
                    if (PurchasesBroker != 0)
                    {
                        curSplitItem.MHBuy = CustomRound((PurchasesMH + TargetDayTradeMH) / PurchasesBroker * curSplitItem.BrokerBuy, Divisor);
                        curSplitItem.NFUNDBuy = CustomRound((PurchasesNFUND + TargetDayTradeNFUND) / PurchasesBroker * curSplitItem.BrokerBuy, Divisor);
                    }

                    if (SalesBroker != 0)
                    {
                        curSplitItem.MHSell = CustomRound((SalesMH - TargetDayTradeMH) / SalesBroker * curSplitItem.BrokerSell, Divisor);
                        curSplitItem.NFUNDSell = CustomRound((SalesNFUND - TargetDayTradeNFUND) / SalesBroker * curSplitItem.BrokerSell, Divisor);
                    }
                }
            }

            // ========================================= MH NFUND END ======================================================

            // ========================================= Hedge FIA SECTION ======================================================

            //ItemsToSplit.Clear();

            FundAcctBroker = 0;
            PurchasesBroker = 0;
            SalesBroker = 0;

            foreach (SplitItem curSplitItem in ItemsToSplit)
            {
                if (curSplitItem.IdBroker == 22 || curSplitItem.IdBroker == 106)
                {
                    FundAcctBroker += curSplitItem.BrokerNet;
                    PurchasesBroker += curSplitItem.BrokerBuy;
                    SalesBroker += curSplitItem.BrokerSell;
                }
            }

            double TotalFixedHedge = InitialSplitItem.HedgeInitial + InitialSplitItem.HedgeTrades;
            double TotalFixedFIA = InitialSplitItem.FIAInitial + InitialSplitItem.FIATrades;

            //if (TotalFixedHedge == 0 && TotalFixedFIA != 0) { PercentFIA = 1; PercentHedge = 0; }
            //if (TotalFixedFIA == 0 && TotalFixedHedge != 0) { PercentFIA = 0; PercentHedge = 1; }

            TotalFinalAll = TotalFixedHedge + TotalFixedFIA + FundAcctBroker;

            double TargetFinalHedge = TotalFinalAll * PercentHedge;
            double TargetFinalFIA = TotalFinalAll * PercentFIA;

            double NeededHedge = TargetFinalHedge - TotalFixedHedge;
            double NeededFIA = TargetFinalFIA - TotalFixedFIA;

            double PurchasesHedge = 0;
            double PurchasesFIA = 0;
            double SalesHedge = 0;
            double SalesFIA = 0;

            if (NeededHedge > 0 && PurchasesBroker > 0) PurchasesHedge = NeededHedge;
            if (NeededHedge > 0 && NeededHedge > PurchasesBroker) PurchasesHedge = PurchasesBroker;

            if (NeededFIA > 0 && PurchasesBroker > 0) PurchasesFIA = NeededFIA;
            if (NeededFIA > 0 && NeededFIA > PurchasesBroker) PurchasesFIA = PurchasesBroker;

            if (NeededHedge < 0 && SalesBroker < 0) SalesHedge = NeededHedge;
            if (NeededHedge < 0 && NeededHedge < SalesBroker) SalesHedge = SalesBroker;

            if (NeededFIA < 0 && SalesBroker < 0) SalesFIA = NeededFIA;
            if (NeededFIA < 0 && NeededFIA < SalesBroker) SalesFIA = SalesBroker;

            double TargetAlocHedge = PurchasesHedge + SalesHedge;
            double TargetAlocFIA = PurchasesFIA + SalesFIA;

            PurchasesALL = PurchasesHedge + PurchasesFIA;
            SalesALL = SalesHedge + SalesFIA;

            TargetDayTradeALL = 0;

            if (Math.Abs(PurchasesALL - PurchasesHedge - PurchasesFIA) > Math.Abs(SalesALL - SalesHedge - SalesFIA))
                TargetDayTradeALL = Math.Abs(PurchasesBroker - PurchasesHedge - PurchasesFIA);
            else
                TargetDayTradeALL = Math.Abs(SalesBroker - SalesHedge - SalesFIA);

            double TargetDayTradeHedge = TargetDayTradeALL * PercentHedge;
            double TargetDayTradeFIA = TargetDayTradeALL * PercentFIA;

            //double percHedgeBuy = TargetDayTradeALL + PurchasesHedge;PurchasesBroker

            foreach (SplitItem curSplitItem in ItemsToSplit)
            {
                if (curSplitItem.IdBroker == 22 || curSplitItem.IdBroker == 106 && curSplitItem.IdOverrideType == 0)
                {
                    if (PurchasesBroker != 0)
                    {
                        curSplitItem.HedgeBuy = CustomRound((PurchasesHedge + TargetDayTradeHedge) / PurchasesBroker * curSplitItem.BrokerBuy , Divisor);
                        curSplitItem.FIABuy = CustomRound((PurchasesFIA + TargetDayTradeFIA) / PurchasesBroker * curSplitItem.BrokerBuy , Divisor);
                    }

                    if (SalesBroker != 0)
                    {
                        curSplitItem.HedgeSell = CustomRound((SalesHedge - TargetDayTradeHedge) / SalesBroker * curSplitItem.BrokerSell , Divisor);
                        curSplitItem.FIASell = CustomRound((SalesFIA - TargetDayTradeFIA) / SalesBroker * curSplitItem.BrokerSell , Divisor);
                    }
                }
            }

            // ========================================= Hedge FIA END ======================================================

            // ========================================= OVERRIDES ======================================================
            foreach (SplitItem curSplitItem in ItemsToSplit)
            {
                if (curSplitItem.IdOverrideType > 0 && curSplitItem.IdOverrideType != 9)
                {
                    curSplitItem.MHBuy = 0;
                    curSplitItem.MHSell = 0;
                    curSplitItem.NFUNDBuy = 0;
                    curSplitItem.NFUNDSell = 0;
                    curSplitItem.FIABuy = 0;
                    curSplitItem.FIASell = 0;
                    curSplitItem.ArbBuy = 0;
                    curSplitItem.ArbSell = 0;
                    curSplitItem.HedgeBuy = 0;
                    curSplitItem.HedgeSell = 0;
                }

                switch (curSplitItem.IdOverrideType)
                {
                    case 1: curSplitItem.MHBuy = PurchasesBroker; curSplitItem.MHSell = SalesBroker; break;
                    case 2: curSplitItem.NFUNDBuy = PurchasesBroker; curSplitItem.NFUNDSell = SalesBroker; break;
                    case 3: curSplitItem.FIABuy = PurchasesBroker; curSplitItem.FIASell = SalesBroker; break;
                    case 4: curSplitItem.ArbBuy = PurchasesBroker; curSplitItem.ArbSell = SalesBroker; break;
                    default: break;
                }

                //if (curSplitItem.IdOverrideType == 9)
                //{
                //    LoadManual(curSplitItem);
                //}
            }
            
            // ========================================= OVERRIDES END ======================================================

        }

        private void LoadManual(SplitItem curSplitItem)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT * FROM [dbo].[FCN_GET_Trade_Split_Status_Security] (" + curSplitItem.IdSecurity + "," + curSplitItem.IdBook + "," + curSplitItem.IdSection + "," + curSplitItem.IdBroker + ") ";

                DataTable curTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow row in curTable.Rows)
                {
                    curSplitItem.MHBuy = NestDLL.Utils.ParseToDouble(row["MH_Buy"]);
                    curSplitItem.NFUNDBuy = NestDLL.Utils.ParseToDouble(row["NFund_Buy"]);
                    curSplitItem.FIABuy = NestDLL.Utils.ParseToDouble(row["Bravo_Buy"]);
                    curSplitItem.ArbBuy = NestDLL.Utils.ParseToDouble(row["Arb_Buy"]);
                    curSplitItem.PrevBuy = NestDLL.Utils.ParseToDouble(row["Prev_Buy"]);
                    curSplitItem.HedgeBuy = NestDLL.Utils.ParseToDouble(row["Hedge_Buy"]);


                    curSplitItem.MHSell = NestDLL.Utils.ParseToDouble(row["MH_Sell"]);
                    curSplitItem.NFUNDSell = NestDLL.Utils.ParseToDouble(row["NFund_Sell"]);
                    curSplitItem.FIASell = NestDLL.Utils.ParseToDouble(row["Bravo_Sell"]);
                    curSplitItem.ArbSell = NestDLL.Utils.ParseToDouble(row["Arb_Sell"]);
                    curSplitItem.PrevSell = NestDLL.Utils.ParseToDouble(row["Prev_Sell"]);
                    curSplitItem.HedgeSell = NestDLL.Utils.ParseToDouble(row["Hedge_Sell"]);
                }
            }
        }

        private double CustomRound(double NumberToRound, double Divisor)
        {
            return Math.Round(NumberToRound / Divisor, 0) * Divisor;
        }

        private void dgTradeSplit_DoubleClick(object sender, EventArgs e)
        {
            int Id_Broker;
            int Id_Ticker;
            int Id_Book;
            int Id_Section;
            string Ticker_Name;
            string Broker_Name;

            string Column_Name = dgTradeSplit.FocusedColumn.ToString();

            if (Column_Name == "Edit")
            {
                Id_Ticker = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Ticker"));
                Id_Broker = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Broker"));

                Id_Book = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Book"));
                Id_Section = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Section"));

                Ticker_Name = Convert.ToString(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "NestTicker"));
                Broker_Name = Convert.ToString(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Broker"));

                //frmTrade_Aloc_Override Aloca = new frmTrade_Aloc_Override();

                //Aloca.Top = this.Top + 100;
                //Aloca.Left = this.Left + 100;

                //Aloca.Id_Ticker = Id_Ticker;
                //Aloca.Id_Broker = Id_Broker;
                //Aloca.Id_Book = Id_Book;
                //Aloca.Id_Section = Id_Section;
                //Aloca.lblTicker.Text = Ticker_Name;
                //Aloca.lblBroker.Text = Broker_Name;
                //Aloca.ShowDialog();

                //ReloadSplit = true;

            }

            if (Column_Name == "Update")
            {
                UpdateSplit(int.Parse(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "IdSecurity").ToString()), int.Parse(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "IdBook").ToString()), int.Parse(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "IdSection").ToString()));
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void dgTradeSplit_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
        }

        private void dgTradeSplit_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;
        }

        private void dgTradeSplit_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            MyXtraGrid.MyGridView curcontrol = (MyXtraGrid.MyGridView)sender;
            DevExpress.XtraGrid.Views.Base.GridCell[] SelectedCells = curcontrol.GetSelectedCells();

            for (int i = 0; i < SelectedCells.Length; i++)
            {
                if (e.RowHandle == SelectedCells[i].RowHandle && e.Column == SelectedCells[i].Column)
                    return;
            }

            string ColumnName = e.Column.Name.Replace("col", "");
            int IdOverrideType = int.Parse(dgTradeSplit.GetRowCellValue(e.RowHandle, "IdOverrideType").ToString());

            if (!ColumnName.Contains("Broker"))
            {
                if (ColumnName.Contains("Buy")) { e.Appearance.BackColor = Color.LightGray; }//.WhiteSmoke;
                if (ColumnName.Contains("Sell")) { e.Appearance.BackColor = Color.LightGray; }
            }
            if (IdOverrideType != 0) e.Appearance.BackColor = Color.LightBlue;

            if (e.DisplayText == "-") e.DisplayText = "";
        }

        private void dgTradeSplit_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            //curUtils.Save_Columns(dgTradeSplit);
        }

        private void dgTradeSplit_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            //curUtils.Save_Columns(dgTradeSplit);
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTradeSplit.SelectAll();
            dgTradeSplit.CopyToClipboard();
        }

        private void UpdateAllDB()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT * FROM dbo.FCN_GET_Trade_Split_Status()";

                DataTable curTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in curTable.Rows)
                {
                    SplitItem DBSplitStatus = new SplitItem();
                    DBSplitStatus.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["Id Ticker"]);
                    DBSplitStatus.IdBroker = (int)NestDLL.Utils.ParseToDouble(curRow["Id Broker"]);
                    DBSplitStatus.IdBook = (int)NestDLL.Utils.ParseToDouble(curRow["Id Book"]);
                    DBSplitStatus.IdSection = (int)NestDLL.Utils.ParseToDouble(curRow["Id Section"]);

                    DBSplitStatus.MHBuy = NestDLL.Utils.ParseToDouble(curRow["MH_Buy"]);
                    DBSplitStatus.MHSell = NestDLL.Utils.ParseToDouble(curRow["MH_Sell"]);
                    DBSplitStatus.NFUNDBuy = NestDLL.Utils.ParseToDouble(curRow["NFUND_Buy"]);
                    DBSplitStatus.NFUNDSell = NestDLL.Utils.ParseToDouble(curRow["NFUND_Sell"]);
                    DBSplitStatus.FIABuy = NestDLL.Utils.ParseToDouble(curRow["Bravo_Buy"]);
                    DBSplitStatus.FIASell = NestDLL.Utils.ParseToDouble(curRow["Bravo_Sell"]);
                    DBSplitStatus.ArbBuy = NestDLL.Utils.ParseToDouble(curRow["Arb_Buy"]);
                    DBSplitStatus.ArbSell = NestDLL.Utils.ParseToDouble(curRow["Arb_Sell"]);
                    DBSplitStatus.PrevBuy = NestDLL.Utils.ParseToDouble(curRow["Prev_Buy"]);
                    DBSplitStatus.PrevSell = NestDLL.Utils.ParseToDouble(curRow["Prev_Sell"]);
                    DBSplitStatus.HedgeBuy = NestDLL.Utils.ParseToDouble(curRow["Hedge_Buy"]);
                    DBSplitStatus.HedgeSell = NestDLL.Utils.ParseToDouble(curRow["Hedge_Sell"]);

                    SplitItem tempSplitItem = SplitItemList[SplitItemList.IndexOf(DBSplitStatus)];

                    if (DBSplitStatus.QuantitiesChanged(tempSplitItem))
                    {
                        Console.WriteLine(tempSplitItem.NestTicker);
                        UpDateDB(tempSplitItem);
                    }
                }
            }

            foreach (SplitItem curSplitItem in SplitItemList)
            {
                
            }
        }

        private void UpDateDB(SplitItem curSplitItem)
        {
            using (newNestConn curConn = new newNestConn())
            {
                double percBuyMH  = 0;
                double percBuyNFUND  = 0;
                double percBuyBravo  = 0;
                double percBuyArb  = 0;
                double percBuyPrev  = 0;
                double percBuyHedge  = 0;

                double percSellMH  = 0;
                double percSellNFUND  = 0;
                double percSellBravo  = 0;
                double percSellArb  = 0;
                double percSellPrev  = 0;
                double percSellHedge  = 0;

                if (curSplitItem.BrokerBuy != 0)
                {
                    percBuyMH = curSplitItem.MHBuy / curSplitItem.BrokerBuy;
                    percBuyNFUND = curSplitItem.NFUNDBuy / curSplitItem.BrokerBuy;
                    percBuyBravo = curSplitItem.FIABuy / curSplitItem.BrokerBuy;
                    percBuyArb = curSplitItem.ArbBuy / curSplitItem.BrokerBuy;
                    percBuyPrev = curSplitItem.PrevBuy / curSplitItem.BrokerBuy;
                    percBuyHedge = curSplitItem.HedgeBuy / curSplitItem.BrokerBuy;
                }

                if (curSplitItem.BrokerSell != 0)
                {
                    percSellMH = curSplitItem.MHSell / curSplitItem.BrokerSell;
                    percSellNFUND = curSplitItem.NFUNDSell / curSplitItem.BrokerSell;
                    percSellBravo = curSplitItem.FIASell / curSplitItem.BrokerSell;
                    percSellArb = curSplitItem.ArbSell / curSplitItem.BrokerSell;
                    percSellPrev = curSplitItem.PrevSell / curSplitItem.BrokerSell;
                    percSellHedge = curSplitItem.HedgeSell / curSplitItem.BrokerSell;
                }

                string SQLString = "EXEC dbo.proc_Split_Trades_Manual " + curSplitItem.IdBroker + ", " + curSplitItem.IdSecurity + "," + curSplitItem.IdBook + "," + curSplitItem.IdSection + ", '" + RunDate.ToString("yyyy-MM-dd") + "',"
                    + percBuyMH.ToString().Replace(',', '.') + ", "
                    + percBuyNFUND.ToString().Replace(',', '.') + ", "
                    + percBuyBravo.ToString().Replace(',', '.') + ", "
                    + percBuyArb.ToString().Replace(',', '.') + ", "
                    + percBuyPrev.ToString().Replace(',', '.') + ", "
                    + percBuyHedge.ToString().Replace(',', '.') + ", "

                    + percSellMH.ToString().Replace(',', '.') + ", "
                    + percSellNFUND.ToString().Replace(',', '.') + ", "
                    + percSellBravo.ToString().Replace(',', '.') + ", "
                    + percSellArb.ToString().Replace(',', '.') + ", "
                    + percSellPrev.ToString().Replace(',', '.') + ", "
                    + percSellHedge.ToString().Replace(',', '.') + ";";

                curConn.ExecuteNonQuery(SQLString);

            }
        }

        class SplitItem : IEquatable<SplitItem>
        {
            public Dictionary<int, string> OverrideTypeList;

            private string _Broker; public string Broker { get { return _Broker; } set { _Broker = value; } }
            private string _NestTicker; public string NestTicker { get { return _NestTicker; } set { _NestTicker = value; } }
            private string _ExchangeTicker; public string ExchangeTicker { get { return _ExchangeTicker; } set { _ExchangeTicker = value; } }
            private string _Book; public string Book { get { return _Book; } set { _Book = value; } }
            private string _Section; public string Section { get { return _Section; } set { _Section = value; } }

            private int _IdSecurity; public int IdSecurity { get { return _IdSecurity; } set { _IdSecurity = value; } }
            private int _IdBroker; public int IdBroker { get { return _IdBroker; } set { _IdBroker = value; } }
            private int _IdBook; public int IdBook { get { return _IdBook; } set { _IdBook = value; } }
            private int _IdSection; public int IdSection { get { return _IdSection; } set { _IdSection = value; } }
            private int _IdInstrument; public int IdInstrument { get { return _IdInstrument; } set { _IdInstrument = value; } }
            private int _IdOverrideType; public int IdOverrideType { get { return _IdOverrideType; } set { _IdOverrideType = value; } }

            private double _BrokerNet; public double BrokerNet { get { return _BrokerNet; } set { _BrokerNet = value; } }
            private double _BrokerBuy; public double BrokerBuy { get { return _BrokerBuy; } set { _BrokerBuy = value; } }
            private double _BrokerSell; public double BrokerSell { get { return _BrokerSell; } set { _BrokerSell = value; } }

            public double UnallocBuy { get { return _BrokerBuy - _MHBuy - _NFUNDBuy - _FIABuy - _ArbBuy - _PrevBuy - _HedgeBuy; } }
            public double UnallocSell { get { return _BrokerSell - _MHSell - _NFUNDSell - _FIASell - _ArbSell - _PrevSell - _HedgeSell; } }

            private double _MHInitial; public double MHInitial { get { return _MHInitial; } set { _MHInitial = value; } }
            private double _MHTrades; public double MHTrades { get { return _MHTrades; } set { _MHTrades = value; } }
            public double MHTotalFixed { get { return _MHInitial + _MHTrades; } }
            //private double _MHNet; public double MHNet { get { return _MHNet; } set { _MHNet = value; } }
            private double _MHBuy; public double MHBuy { get { return _MHBuy; } set { _MHBuy = value; } }
            private double _MHSell; public double MHSell { get { return _MHSell; } set { _MHSell = value; } }

            private double _NFUNDInitial; public double NFUNDInitial { get { return _NFUNDInitial; } set { _NFUNDInitial = value; } }
            private double _NFUNDTrades; public double NFUNDTrades { get { return _NFUNDTrades; } set { _NFUNDTrades = value; } }
            public double NFUNDTotalFixed { get { return _NFUNDInitial + _NFUNDTrades; } }
            //private double _NFUNDNet; public double NFUNDNet { get { return _NFUNDNet; } set { _NFUNDNet = value; } }
            private double _NFUNDBuy; public double NFUNDBuy { get { return _NFUNDBuy; } set { _NFUNDBuy = value; } }
            private double _NFUNDSell; public double NFUNDSell { get { return _NFUNDSell; } set { _NFUNDSell = value; } }

            private double _FIAInitial; public double FIAInitial { get { return _FIAInitial; } set { _FIAInitial = value; } }
            private double _FIATrades; public double FIATrades { get { return _FIATrades; } set { _FIATrades = value; } }
            public double FIATotalFixed { get { return _FIAInitial + _FIATrades; } }
            //private double _FIANet; public double FIANet { get { return _FIANet; } set { _FIANet = value; } }
            private double _FIABuy; public double FIABuy { get { return _FIABuy; } set { _FIABuy = value; } }
            private double _FIASell; public double FIASell { get { return _FIASell; } set { _FIASell = value; } }

            private double _ArbInitial; public double ArbInitial { get { return _ArbInitial; } set { _ArbInitial = value; } }
            private double _ArbTrades; public double ArbTrades { get { return _ArbTrades; } set { _ArbTrades = value; } }
            public double ArbTotalFixed { get { return _ArbInitial + _ArbTrades; } }
            //private double _ArbNet; public double ArbNet { get { return _ArbNet; } set { _ArbNet = value; } }
            private double _ArbBuy; public double ArbBuy { get { return _ArbBuy; } set { _ArbBuy = value; } }
            private double _ArbSell; public double ArbSell { get { return _ArbSell; } set { _ArbSell = value; } }

            private double _PrevInitial; public double PrevInitial { get { return _PrevInitial; } set { _PrevInitial = value; } }
            private double _PrevTrades; public double PrevTrades { get { return _PrevTrades; } set { _PrevTrades = value; } }
            public double PrevTotalFixed { get { return _PrevInitial + _PrevTrades; } }
            //private double _PrevNet; public double PrevNet { get { return _PrevNet; } set { _PrevNet = value; } }
            private double _PrevBuy; public double PrevBuy { get { return _PrevBuy; } set { _PrevBuy = value; } }
            private double _PrevSell; public double PrevSell { get { return _PrevSell; } set { _PrevSell = value; } }

            private double _HedgeInitial; public double HedgeInitial { get { return _HedgeInitial; } set { _HedgeInitial = value; } }
            private double _HedgeTrades; public double HedgeTrades { get { return _HedgeTrades; } set { _HedgeTrades = value; } }
            public double HedgeTotalFixed { get { return _HedgeInitial + _HedgeTrades; } }
            //private double _HedgeNet; public double HedgeNet { get { return _HedgeNet; } set { _HedgeNet = value; } }
            private double _HedgeBuy; public double HedgeBuy { get { return _HedgeBuy; } set { _HedgeBuy = value; } }
            private double _HedgeSell; public double HedgeSell { get { return _HedgeSell; } set { _HedgeSell = value; } }
            
            public string OverrideType { get { return OverrideTypeList[_IdOverrideType]; } }

            public bool Equals(SplitItem other)
            {
                if (this.IdSecurity == other.IdSecurity
                    && this.IdBroker == other.IdBroker
                    && this.IdBook == other.IdBook
                    && this.IdSection == other.IdSection
                    )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool QuantitiesChanged(SplitItem other)
            {
                if (
                Math.Round(this.MHBuy, 4) != Math.Round(other.MHBuy, 4)
                || Math.Round(this.MHSell, 4) != Math.Round(other.MHSell, 4)
                || Math.Round(this.NFUNDBuy, 4) != Math.Round(other.NFUNDBuy, 4)
                || Math.Round(this.NFUNDSell, 4) != Math.Round(other.NFUNDSell, 4)
                || Math.Round(this.FIABuy, 4) != Math.Round(other.FIABuy, 4)
                || Math.Round(this.FIASell, 4) != Math.Round(other.FIASell, 4)
                || Math.Round(this.ArbBuy, 4) != Math.Round(other.ArbBuy, 4)
                || Math.Round(this.ArbSell, 4) != Math.Round(other.ArbSell, 4)
                || Math.Round(this.PrevBuy, 4) != Math.Round(other.PrevBuy, 4)
                || Math.Round(this.PrevSell, 4) != Math.Round(other.PrevSell, 4)
                || Math.Round(this.HedgeBuy, 4) != Math.Round(other.HedgeBuy, 4)
                || Math.Round(this.HedgeSell, 4) != Math.Round(other.HedgeSell, 4)
                )
                    return true;
                else
                    return false;
            }

        }

        
    }
}