using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using LiveDLL;

namespace LiveBook
{
    public partial class frmForwardReconciliation : LBForm
    {
        public frmForwardReconciliation()
        {
            InitializeComponent();
            cmbDate.Value = DateTime.Today;
            GridFowardRecon.LookAndFeel.UseDefaultLookAndFeel = false;
            GridFowardRecon.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            GridFowardRecon.LookAndFeel.SetSkinStyle("Blue");
        }

        string SQLString = "";
        newNestConn curConn = new newNestConn();
        List<Security> FowardList;
        List<Security> ShareList;
        Dictionary<string, SecurityGroup> SecurityGroupList;

        List<Simulation> ValidSimulations = new List<Simulation>();

                DataTable MatchTables(DataTable dtShares, DataTable dtFowards)
        {
            SecurityGroupList = new Dictionary<string, SecurityGroup>();

            DataTable ReturnTable = this.ReturnTable();

            FowardList = JoinFowards(dtFowards);
            ShareList = JoinShares(dtShares);

            string[] FwdKey, ShareKey;

            foreach (Security curFoward in FowardList)
            {
                FwdKey = curFoward.SecurityKey.Split(';');

                foreach (Security curShare in ShareList)
                {
                    ShareKey = curShare.SecurityKey.Split(';');

                    if (ShareKey[0] == FwdKey[0] &&
                        ShareKey[1] == FwdKey[1] &&
                        ShareKey[2] == FwdKey[2])
                    {
                        if (SecurityGroupList.ContainsKey(curFoward.SecurityKey))
                        {
                            SecurityGroupList[curFoward.SecurityKey].ShareList.Add(curShare);
                        }
                        else
                        {
                            List<Security> _FowardList = new List<Security>();
                            List<Security> _ShareList = new List<Security>();

                            _FowardList.Add(curFoward);
                            _ShareList.Add(curShare);

                            SecurityGroup curSecurityGroup = new SecurityGroup(_FowardList, _ShareList);
                            SecurityGroupList.Add(curFoward.SecurityKey, curSecurityGroup);
                        }
                    }
                }
            }

            foreach (SecurityGroup curSecurityGroup in SecurityGroupList.Values)
            {
                ValidSimulations.Add(new ForwardMatcher(curSecurityGroup.ShareList, curSecurityGroup.FwdList).GetBestSimulation());
            }
            
            foreach (Simulation CreatedSimulations in ValidSimulations)
            {
                foreach (Combination curCombination in CreatedSimulations.CombinationList)
                {
                    DataRow NewRow = ReturnTable.NewRow();

                    NewRow["IdBroker"] = curCombination.curTermo.IdBroker;
                    //NewRow["Broker"] = curCombination.curTermo.Brokerage
                    NewRow["IdPortfolio"] = curCombination.curTermo.IdPortfolio;
                    //NewRow["Portfolio"] =
                    NewRow["Foward"] = curCombination.curTermo.Ticker;
                    //NewRow["NewIdSecurity"] =
                    NewRow["Expiration"] = curCombination.curTermo.Expiration;
                    NewRow["DaysToExpiration"] = curCombination.curTermo.DaysToExpiration;
                    NewRow["IdUnderlying"] = curCombination.curTermo.IdUnderlying;
                    NewRow["Underlying"] = curCombination.curVista.Ticker;
                    NewRow["Quantity"] = curCombination.curTermo.Quantity;
                    NewRow["Price"] = curCombination.curTermo.Price;
                    NewRow["PriceWithBrokerage"] = curCombination.curTermo.PriceWithBrokerage;
                    NewRow["Cash"] = curCombination.curTermo.Cash;
                    NewRow["Brokerage"] = curCombination.curTermo.Brokerage;
                    NewRow["TradeDate"] = curCombination.curTermo.TradeDate;
                    NewRow["SpotQuantity"] = curCombination.curVista.Quantity;
                    NewRow["SpotPrice"] = curCombination.curVista.Price;
                    NewRow["SpotWithBrokerage"] = curCombination.curVista.PriceWithBrokerage;
                    NewRow["SpotCash"] = curCombination.curVista.Cash;
                    NewRow["SpotBrokerage"] = curCombination.curVista.Brokerage;
                    NewRow["IdBook"] = 0;
                    NewRow["IdSection"] = 7;
                    NewRow["IdInstrument"] = 12;
                    //NewRow["IdAccount"] =
                    //NewRow["Status"] =


                    ReturnTable.Rows.Add(NewRow);
                }
            }

            return ReturnTable;
        }
        
        public void LoadGridUngrouped()
        {
            string a = MonthParse(cmbDate.Value);

            DataTable dtShares = curConn.Return_DataTable("SELECT * from [FCN_Foward_Positions_Ungrouped] ('" + cmbDate.Value.ToString("yyyy-MM-dd") + "') where Type = 'VIS' and Ticker like '%vale%'  "); //  and IdBroker= 51 ");
            DataTable dtFowards = curConn.Return_DataTable("SELECT * from [FCN_Foward_Positions_Ungrouped] ('" + cmbDate.Value.ToString("yyyy-MM-dd") + "') where Type = 'TER'   and Ticker like '%vale%'  "); // and Ticker like '%vale%' and IdBroker= 51  and IdBroker= 51 ");

            if (dtShares.Rows.Count > 0 && dtFowards.Rows.Count > 0)
            {
                GridFowardRecon.DataSource = MatchTables(dtShares, dtFowards);

                try
                {
                    DataGridFowardRecon.Columns.AddField("Insert");
                    DataGridFowardRecon.Columns["Insert"].VisibleIndex = 0;
                    DataGridFowardRecon.Columns["Insert"].Width = 60;

                    RepositoryItemButtonEdit item1 = new RepositoryItemButtonEdit();
                    item1.Buttons[0].Tag = 1;
                    item1.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    item1.Buttons[0].Caption = "Insert";
                    GridFowardRecon.RepositoryItems.Add(item1);
                    DataGridFowardRecon.Columns["Insert"].ColumnEdit = item1;
                    DataGridFowardRecon.Columns["Insert"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                    DataGridFowardRecon.OptionsBehavior.Editable = false;
                    DataGridFowardRecon.Columns["Insert"].Visible = true;

                    DataGridFowardRecon.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["Price"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["PriceWithBrokerage"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["PriceWithBrokerage"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["Cash"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["Brokerage"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["Brokerage"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["SpotPrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["SpotPrice"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["SpotWithBrokerage"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["SpotWithBrokerage"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["SpotCash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["SpotCash"].DisplayFormat.FormatString = "N";

                    DataGridFowardRecon.Columns["SpotBrokerage"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    DataGridFowardRecon.Columns["SpotBrokerage"].DisplayFormat.FormatString = "N";

                    curUtils.SetColumnStyle(DataGridFowardRecon, 1);
                }

                catch { }
            }
        }

        private List<Security> JoinFowards(DataTable FowardsTable)
        {
            int i = 1;

            Dictionary<string, Security> _FowardDictionary = new Dictionary<string, Security>();

            // Join Fowards
            foreach (DataRow curFowardRow in FowardsTable.Rows)
            {
                Security curFoward = new Security(curFowardRow["Type"].ToString());

                curFoward.IdBroker = (int)curFowardRow["IdBroker"];
                curFoward.IdPortfolio = (int)curFowardRow["IdPortfolio"];
                curFoward.Ticker = curFowardRow["Ticker"].ToString();
                curFoward.Quantity = (double)curFowardRow["Quantity"];
                curFoward.Cash = (double)curFowardRow["Cash"];
                curFoward.Brokerage = (double)curFowardRow["Brokerage"];
                curFoward.DaysToExpiration = (int)curFowardRow["DaysToExpiration"];
                curFoward.Expiration = DateTime.Parse(curFowardRow["Expiration"].ToString());

                curFoward.SecurityKey = curFoward.Ticker.Substring(0, curFoward.Ticker.Length - 1) + ";" + curFoward.IdPortfolio.ToString() + ";" + curFoward.IdBroker.ToString() + ";" + curFoward.DaysToExpiration.ToString();

                if (!_FowardDictionary.ContainsKey(curFoward.SecurityKey))
                {
                    curFoward.Identifier = (i++).ToString();
                    lock (_FowardDictionary) { _FowardDictionary.Add(curFoward.SecurityKey, curFoward); }
                    continue;
                }

                foreach (Security FowardInList in _FowardDictionary.Values)
                {
                    if (curFoward.SecurityKey == FowardInList.SecurityKey)
                    {
                        FowardInList.Quantity += curFoward.Quantity;
                        FowardInList.Cash += curFoward.Cash;
                        FowardInList.Brokerage += curFoward.Brokerage;
                        FowardInList.Price = FowardInList.Cash / FowardInList.Quantity;
                        FowardInList.PriceWithBrokerage = (FowardInList.Cash + FowardInList.Brokerage) / FowardInList.Quantity;
                    }
                }
            }


            List<Security> _FowardList = new List<Security>();

            foreach (Security item in _FowardDictionary.Values)
            {
                _FowardList.Add(item);
            }

            return _FowardList;
        }

        private List<Security> JoinShares(DataTable SharesTable)
        {
            List<Security> _ShareList = new List<Security>();

            int i = 1;
            foreach (DataRow curShareRow in SharesTable.Rows)
            {
                Security curShare = new Security(curShareRow["Type"].ToString());

                curShare.IdBroker = (int)curShareRow["IdBroker"];
                curShare.IdPortfolio = (int)curShareRow["IdPortfolio"];
                curShare.Ticker = curShareRow["Ticker"].ToString();
                curShare.Quantity = (double)curShareRow["Quantity"];
                curShare.Cash = (double)curShareRow["Cash"];
                curShare.Brokerage = (double)curShareRow["Brokerage"];
                curShare.DaysToExpiration = (int)curShareRow["DaysToExpiration"];
                curShare.Price = (double)curShareRow["Price"];
                curShare.PriceWithBrokerage = (double)curShareRow["PriceWithBrokerage"];
                curShare.Expiration = DateTime.Parse(curShareRow["Expiration"].ToString());
                curShare.Identifier = (i++).ToString();
                curShare.SecurityKey = curShare.Ticker + ";" + curShare.IdPortfolio.ToString() + ";" + curShare.IdBroker.ToString() + ";" + i.ToString();

                lock (_ShareList) { _ShareList.Add(curShare); }
            }
            return _ShareList;
        }

        private void cmbDate_ValueChanged(object sender, EventArgs e)
        {
            LoadGridUngrouped();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadGridUngrouped();
        }

        private void dtgFoward_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void dtgFoward_DoubleClick(object sender, EventArgs e)
        {
            GridView GetColumn = sender as GridView;
            string ColumnName = GetColumn.FocusedColumn.Caption.ToString();

            Security curSecurity = new Security();

            //curSecurity.Expiration = Convert.ToDateTime(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "Expiration"));
            //curSecurity.TradeDate = Convert.ToDateTime(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "TradeDate"));

            //curSecurity.Ticker = Convert.ToString(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "Ticker"));

            //curSecurity.IdUnderlying = Convert.ToInt32(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "IdUnderlying"));
            //curSecurity.IdBroker = Convert.ToInt32(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "IdBroker"));
            //curSecurity.IdPortfolio = Convert.ToInt32(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "IdPortfolio"));

            //curSecurity.Quantity = Convert.ToDouble(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "Quantity"));
            //curSecurity.SpotPrice = Convert.ToDouble(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "SpotPrice"));
            //curSecurity.Price = Convert.ToDouble(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "Price"));
            //curSecurity.Cash = Convert.ToDouble(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "Cash"));
            //curSecurity.PriceWithBrokerage = Convert.ToDouble(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "PriceWithBrokerage"));
            //curSecurity.SpotBrokerage = Convert.ToDouble(DataGridFowardRecon.GetRowCellValue(DataGridFowardRecon.FocusedRowHandle, "SpotBrokerage"));


            if (ColumnName == "Insert")
            {
                if (MessageBox.Show("Insert this Foward?", "Insert Foward", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Insert(curSecurity);


                }
            }
            else
            {
                DataGridFowardRecon.OptionsView.ShowHorzLines = false;
                DataGridFowardRecon.OptionsView.ShowVertLines = false;
            }

        }

        private void Insert(Security curSecurity)
        {
            int IdAccount;

            SQLString = "DECLARE @IdSecurity int  " +
                                "INSERT INTO NESTDB.dbo.Tb001_Securities_Fixed  " +
                                "		(Description,IdUnderlying,IdIssuer, " +
                                "		[IdInstrument],IdAssetClass, " +
                                "		IdCurrency,IdPriceTable,TRDate, " +
                                "		QuotedAsRate,OptionType,ContractRatio, " +
                                "		IdStrikeCurrency,IdPremiumCurrency, " +
                                "		LastTradeDate,Discontinued,RTUpdate, " +
                                "		RTUpdateSource,HistUpdate, " +
                                "		HistUpdateSource,HistUpdateFrequency,CyrnelTicker " +
                                "		) " +
                                "SELECT  " +
                                "		[Description],[IdUnderlying],[IdIssuer], " +
                                "		12 AS [IdInstrument],[IdAssetClass], " +
                                "		[IdCurrency],[IdPriceTable],[TRDate], " +
                                "		[QuotedAsRate],[OptionType],[ContractRatio], " +
                                "		[IdStrikeCurrency],[IdPremiumCurrency], " +
                                "		[LastTradeDate],[Discontinued],0 AS [RTUpdate], " +
                                "		0 AS [RTUpdateSource],0 AS [HistUpdate], " +
                                "		0 AS [HistUpdateSource],6 AS [HistUpdateFrequency], '" + curSecurity.Ticker + "' AS [CyrnelTicker] " +
                                "FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE idsecurity= " + curSecurity.IdUnderlying + " ;  " +
                                "Select @IdSecurity = @@IDENTITY   " +
                                "INSERT INTO NESTDB.dbo.Tb001_Securities_Variable  " +
                                "		(IdSecurity, ValidAsOfDate, " +
                                "		SecName, NestTicker, " +
                                "		IdPrimaryExchange, RoundLot, RoundLotSize, " +
                                "		Expiration,	 " +
                                "		Strike,	ADRRatio,DealAnnounceDate,IsInDeal, " +
                                "		DealDetails,ExpirationPrice,PriceFromUnderlying) " +
                                "SELECT  @IdSecurity [IdSecurity],[ValidAsOfDate], " +
                                "		'" + curSecurity.Ticker + " " + MonthParse(curSecurity.Expiration) + "' AS [SecName], " +
                                "		'" + curSecurity.Ticker + " " + MonthParse(curSecurity.Expiration) + "' AS [NestTicker], " +
                                "		[IdPrimaryExchange],[RoundLot],1 AS [RoundLotSize], " +
                                "		'" + curSecurity.Expiration.ToString("yyyy-MM-dd") + "' AS [Expiration], " +
                                "		[Strike],[ADRRatio],[DealAnnounceDate],[IsInDeal], " +
                                "		[DealDetails],[ExpirationPrice],[PriceFromUnderlying] " +
                                "FROM NESTDB.dbo.Tb001_Securities_Variable " +
                                "WHERE idsecurity = " + curSecurity.IdUnderlying + " ; ";

            // SecurityCounter = curConn.ExecuteNonQuery(SQLString);

            SQLString = "SELECT Id_Account FROM dbo.VW_Account_Broker WHERE Id_Portfolio= " + curSecurity.IdPortfolio + " AND Id_Broker= " + curSecurity.IdBroker;

            IdAccount = curConn.ExecuteNonQuery(SQLString);

            //SQLString = " INSERT INTO [NESTDB].[dbo].[Tb725_Fowards] " +
            //            "   ( " +
            //            "    [Id_Account],[Id_Ticker] ,[Id_Book] ,[Id_Section] ,[Quantity] ,[Price]  , " +
            //            "    [SpotPrice] ,[Cash],[Trade_Date],[Id_User],[Status_Foward] ,[PriceBrokerage] ,[SpotPriceBrokerage] " +
            //            "   ) " +
            //            " SELECT  " + IdAccount + ",  @IdSecurity, 7, 0," +
            //            curSecurity.Quantity.ToString().Replace(",", ".") + ", " +
            //            curSecurity.Price.ToString().Replace(",", ".") + ", " +
            //            curSecurity.SpotPrice.ToString().Replace(",", ".") + ", " +
            //            curSecurity.Cash.ToString().Replace(",", ".") + ", " +
            //            curSecurity.TradeDate.ToString("yyyy-MM-dd") + ", 39, 1 ," +
            //            curSecurity.PriceWithBrokerage.ToString().Replace(",", ".") + ", " +
            //            curSecurity.SpotBrokerage.ToString().Replace(",", ".");

            ////FowardCounter = curConn.ExecuteNonQuery(SQLString);


            //SQLString = "EXEC NESTDB.[dbo].[proc_insert_Tb012_Ordens]" +
            //    //"" + curSecurity.IdSecurity +
            //            "," + curSecurity.Quantity +
            //            "," + curSecurity.SpotPrice +
            //            "," + curSecurity.SpotCash +
            //            ",7,0,1,39," + curSecurity.TradeDate.ToString("yyyyMMdd") +
            //            ",1," + curSecurity.IdBroker +
            //            "," + curSecurity.TradeDate.ToString("yyyyMMdd") +
            //            "," + IdAccount + ",0,0,0";

            //   TransactionCounter = curConn.ExecuteNonQuery(SQLString);
        }

        private void dtgFoward_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void DataGridFowardRecon_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int TempVal = e.RowHandle;
            GridView curView = (GridView)sender;

            if (curView.GetRowCellValue(TempVal, "Status").ToString() == "NOT INSERTED")
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Red;
            }
            else
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Green;
            }
        }

        private string MonthParse(DateTime TargetDate)
        {
            string Date = TargetDate.ToString("dd-MM-yy");

            if (Date.Contains("-01-")) { Date = Date.Replace("-01-", "-JAN-"); }
            else if (Date.Contains("-02-")) { Date = Date.Replace("-02-", "-FEV-"); }
            else if (Date.Contains("-03-")) { Date = Date.Replace("-03-", "-MAR-"); }
            else if (Date.Contains("-04-")) { Date = Date.Replace("-04-", "-ABR-"); }
            else if (Date.Contains("-05-")) { Date = Date.Replace("-05-", "-MAI-"); }
            else if (Date.Contains("-06-")) { Date = Date.Replace("-06-", "-JUN-"); }
            else if (Date.Contains("-07-")) { Date = Date.Replace("-07-", "-JUL-"); }
            else if (Date.Contains("-08-")) { Date = Date.Replace("-08-", "-AGO-"); }
            else if (Date.Contains("-09-")) { Date = Date.Replace("-09-", "-SET-"); }
            else if (Date.Contains("-10-")) { Date = Date.Replace("-10-", "-OUT-"); }
            else if (Date.Contains("-11-")) { Date = Date.Replace("-11-", "-NOV-"); }
            else if (Date.Contains("-12-")) { Date = Date.Replace("-12-", "-DEZ-"); }

            return Date;
        }

        private DataTable ReturnTable()
        {
            DataTable ReturnTable = new DataTable();

            ReturnTable.Columns.Add("IdBroker");
            ReturnTable.Columns.Add("Broker");
            ReturnTable.Columns.Add("IdPortfolio");
            ReturnTable.Columns.Add("Portfolio");
            ReturnTable.Columns.Add("Foward");
            ReturnTable.Columns.Add("NewIdSecurity");
            ReturnTable.Columns.Add("Expiration");
            ReturnTable.Columns.Add("DaysToExpiration");
            ReturnTable.Columns.Add("IdUnderlying");
            ReturnTable.Columns.Add("Underlying");
            ReturnTable.Columns.Add("Quantity");
            ReturnTable.Columns.Add("Price");
            ReturnTable.Columns.Add("PriceWithBrokerage");
            ReturnTable.Columns.Add("Cash");
            ReturnTable.Columns.Add("Brokerage");
            ReturnTable.Columns.Add("TradeDate");
            ReturnTable.Columns.Add("SpotQuantity");
            ReturnTable.Columns.Add("SpotPrice");
            ReturnTable.Columns.Add("SpotWithBrokerage");
            ReturnTable.Columns.Add("SpotCash");
            ReturnTable.Columns.Add("SpotBrokerage");
            ReturnTable.Columns.Add("IdBook");
            ReturnTable.Columns.Add("IdSection");
            ReturnTable.Columns.Add("IdInstrument");
            ReturnTable.Columns.Add("IdAccount");
            ReturnTable.Columns.Add("Status");

            return ReturnTable;

        }

    }

    public class SecurityGroup
    {

        public List<Security> FwdList;
        public List<Security> ShareList;

        public SecurityGroup(List<Security> FwdList, List<Security> ShareList)
        {
            this.FwdList = FwdList;
            this.ShareList = ShareList;
        }
    }

    public class Security
    {
        public Security() { }
        public Security(string Type) { this.Type = Type; }

        public string SecurityKey = "";
        public string Type;
        public string Identifier = "";
        public string Ticker = "";

        public int DaysToExpiration = 0;
        public int IdPortfolio = 0;
        public int IdBroker = 0;
        public int IdUnderlying = 0;

        public double Quantity = 0;
        public double Price = 0;
        public double PriceWithBrokerage = 0;
        public double Cash = 0;
        public double Brokerage = 0;

        public DateTime Expiration;
        public DateTime TradeDate;
    }
}