using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmStatusOps : Form
    {
        public frmStatusOps()
        {
            InitializeComponent();
        }

        private void frmStatusOps_Load(object sender, EventArgs e)
        {
            CheckTransactions();
            DateTime histDate1 = DateTime.Now.AddDays(-1);
            if (histDate1.DayOfWeek == DayOfWeek.Sunday) histDate1 = DateTime.Now.AddDays(-3);

            dtpYesterday.Value = histDate1;


            DateTime histDate2 = DateTime.Now.AddDays(-2);
            if (histDate2.DayOfWeek == DayOfWeek.Sunday) histDate2 = DateTime.Now.AddDays(-4);

            dtpPrev.Value = histDate2;


        }

        void CheckTransactions()
        {
            string SQLString;
            DateTime ReturnDate;

            SQLString = "SELECT CheckInTime FROM NESTLOG.dbo.Tb900_CheckIn_Log (Nolock)" +
                    " WHERE Program_Id=202 ";

            using (newNestConn curConn = new newNestConn())
            {
                ReturnDate = Convert.ToDateTime(curConn.Execute_Query_String(SQLString).ToString());


                if (lblTime.Text != ReturnDate.ToString())
                {
                    if (ReturnDate == Convert.ToDateTime("02/01/1900"))
                    {
                        lblTime.Text = "Running";
                    }
                    else
                    {
                        lblTime.Text = ReturnDate.ToString();
                    }
                }
            }
        }

        void CheckDividends()
        {
            string SQLString;
            Int32 ReturnDate;

            SQLString = " Select count(*) FROM dbo.Tb720_Dividends (nolock) WHERE Declared_Date='" + dtpYesterday.Value.ToString("yyyyMMdd") + "'";

            using (newNestConn curConn = new newNestConn())
            {
                ReturnDate = Convert.ToInt32(curConn.Execute_Query_String(SQLString).ToString());

                lblDividends.Text = ReturnDate.ToString("##,##0.00#######");

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckTransactions();

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            LoadNAV();
            LoadAnal();
            LoadPriceHist();
            LoadPtax();
            LoadPort();
            CheckDividends();
            CheckBrokerage();
        }

        void LoadPriceHist()
        {
            string StringSQL;

            StringSQL = "Select Srdate,IdCurrency,Source,count(*) as counter FROM dbo.VW_Precos A " + 
                        " inner join  dbo.Tb001_Securities B" + 
                        " ON A.IdSecurity = B.IdSecurity" +
                        " Where Source in (15,23) and srType in (1)" +
                        " and Srdate >='" + dtpPrev.Value.ToString("yyyyMMdd") + "' and Srdate <='" + dtpYesterday.Value.ToString("yyyyMMdd") + "'" +
                        " AND IdCurrency in (900,1042) group by Srdate,IdCurrency,Source ";

            using (newNestConn curConn = new newNestConn())
            {
                DataTable tablep = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in tablep.Rows)
                {
                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpPrev.Value.ToString("yyyy-MM-dd") && curRow["IdCurrency"].ToString() == "900" && curRow["Source"].ToString() == "15")
                    {
                        lblPrevLast.Text = curRow["counter"].ToString();
                    }

                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpYesterday.Value.ToString("yyyy-MM-dd") && curRow["IdCurrency"].ToString() == "900" && curRow["Source"].ToString() == "15")
                    {
                        lblYestLast.Text = curRow["counter"].ToString();
                    }

                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpPrev.Value.ToString("yyyy-MM-dd") && curRow["IdCurrency"].ToString() == "1042" && curRow["Source"].ToString() == "15")
                    {
                        lblPrevAdr.Text = curRow["counter"].ToString();
                    }

                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpYesterday.Value.ToString("yyyy-MM-dd") && curRow["IdCurrency"].ToString() == "1042" && curRow["Source"].ToString() == "15")
                    {
                        lblYestAdr.Text = curRow["counter"].ToString();
                    }

                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpPrev.Value.ToString("yyyy-MM-dd") && curRow["IdCurrency"].ToString() == "900" && curRow["Source"].ToString() == "23")
                    {
                       lblBondsPrev.Text = curRow["counter"].ToString();
                    }

                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpYesterday.Value.ToString("yyyy-MM-dd") && curRow["IdCurrency"].ToString() == "900" && curRow["Source"].ToString() == "23")
                    {
                        lblBondsYest.Text = curRow["counter"].ToString();
                    }

                
                }

                double prev;
                double Yest;

                double.TryParse(lblPrevLast.Text.ToString(), out prev);
                double.TryParse(lblYestLast.Text.ToString(), out Yest);

                if (Math.Abs(1 - (prev/Yest)) > 0.10)
                {
                    lblYestLast.ForeColor = Color.Red;
                    lblPrevLast.ForeColor = Color.Red;
                }
                else
                {
                    lblYestLast.ForeColor = Color.Green;
                    lblPrevLast.ForeColor = Color.Green;
                }

                prev = 0;
                Yest = 0;

                double.TryParse(lblPrevAdr.Text.ToString(), out prev);
                double.TryParse(lblYestAdr.Text.ToString(), out Yest);

                if (Math.Abs(1 - (prev / Yest)) > 0.10)
                {
                    lblPrevAdr.ForeColor = Color.Red;
                    lblYestAdr.ForeColor = Color.Red;
                }
                else
                {
                    lblPrevAdr.ForeColor = Color.Green;
                    lblYestAdr.ForeColor = Color.Green;
                }


                prev = 0;
                Yest = 0;

                double.TryParse(lblBondsPrev.Text.ToString(), out prev);
                double.TryParse(lblBondsYest.Text.ToString(), out Yest);

                if (Math.Abs(1 - (prev / Yest)) > 0.10)
                {
                    lblBondsPrev.ForeColor = Color.Red;
                    lblBondsYest.ForeColor = Color.Red;
                }
                else
                {
                    lblBondsPrev.ForeColor = Color.Green;
                    lblBondsYest.ForeColor = Color.Green;
                }
            }

        }

        void LoadPtax()
        {
            string StringSQL;

            StringSQL = "Select Srdate,count(*) as counter FROM dbo.Tb058_Precos_Moedas " +
                " Where IdSecurity = 210605 AND srType =1 " +
                    " and Srdate >='" + dtpPrev.Value.ToString("yyyyMMdd") + "' and Srdate <='" + dtpYesterday.Value.ToString("yyyyMMdd") + "'" +
                " group by Srdate";

            using (newNestConn curConn = new newNestConn())
            {
                DataTable tablep = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in tablep.Rows)
                {
                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpPrev.Value.ToString("yyyy-MM-dd"))
                    {
                        if (curRow["counter"].ToString() == "1")
                        {
                            lblDolPrev.Text = "Ok";
                            lblDolPrev.ForeColor = Color.Green;
                        }
                        else
                        {
                            lblDolPrev.Text = "Error";
                            lblDolPrev.ForeColor = Color.Red;
                        }
                    }

                    if (Convert.ToDateTime(curRow["SrDate"]).ToString("yyyy-MM-dd") == dtpYesterday.Value.ToString("yyyy-MM-dd"))
                    {
                        if (curRow["counter"].ToString() == "1")
                        {
                            lblDolYest.Text = "Ok";
                            lblDolYest.ForeColor = Color.Green;

                        }
                        else
                        {
                            lblDolPrev.Text = "Error";
                            lblDolYest.ForeColor = Color.Red;
                        }
                    }
                }
            }
        }

        void CheckBrokerage()
        {
            string StringSQL;
            Int32 ReturnValue=0;

            StringSQL = "Select count(*) FROM NESTIMPORT.dbo.Fcn_Return_Cost('" + dtpYesterday.Value.ToString("yyyyMMdd") + "')";

            //<='" + dtpYesterday.Value.ToString("yyyyMMdd") + "'" +

            using (newNestConn curConn = new newNestConn())
            {
                ReturnValue = Convert.ToInt32(curConn.Execute_Query_String(StringSQL).ToString());
            }

            if (ReturnValue == 0)
            {
                lblBrokerageYest.Text = "Ok";
                lblBrokerageYest.ForeColor = Color.Green;
            }
            else
            {
                lblBrokerageYest.Text = "Error";
                lblBrokerageYest.ForeColor = Color.Red;
            }

            StringSQL = "Select count(*) FROM NESTIMPORT.dbo.Fcn_Return_Cost('" + dtpPrev.Value.ToString("yyyyMMdd") + "')";

            //<='" + dtpYesterday.Value.ToString("yyyyMMdd") + "'" +
            ReturnValue = 0;

            using (newNestConn curConn = new newNestConn())
            {
                ReturnValue = Convert.ToInt32(curConn.Execute_Query_String(StringSQL).ToString());
            }

            if (ReturnValue == 0)
            {
                lblBrokeragePrev.Text = "Ok";
                lblBrokeragePrev.ForeColor = Color.Green;
            }
            else
            {
                lblBrokeragePrev.Text = "Error";
                lblBrokeragePrev.ForeColor = Color.Red;
            }
        
        }

        
        object padLock = new object();

        void LoadNAV()
        {
            string StringSQL = " Select Id_Portfolio as IdPortfolio FROM NESTDB.dbo.Tb025_Valor_PL " +
                                " WHERE Data_PL='" + dtpYesterday.Value.ToString("yyyy-MM-dd") + "' " +
                                " and Id_Portfolio in (4,43,10,18,38)";


            ClearAllData(grpNAV, "NAV");
            LoadData(grpNAV, "NAV", StringSQL);

        }

        void LoadAnal()
        {
           
            string StringSQL = " SELECT distinct(IdPortfolio)IdPortfolio FROM NESTIMPORT.dbo.Tb_Mellon_Analise " +
                               " WHERE RefDate='" + dtpYesterday.Value.ToString("yyyy-MM-dd") + "' " +
                               " and IdPortfolio in (4,43,10,18,38)";


            ClearAllData(grpAnalysis, "An");
            LoadData(grpAnalysis, "An", StringSQL);

        }

        void LoadPort()
        {
            string StringSQL = " select IdPortfolio FROM " +
                               " NESTIMPORT.dbo.Tb_Mellon_Patrimonio " +
                               " WHERE RefDate='" + dtpYesterday.Value.ToString("yyyy-MM-dd") + "' " +
                               " and IdPortfolio in (10,18,43,38) " +
                               " UNION ALL " +
                               " select IdPortfolio FROM  " +
                               " NESTIMPORT.dbo.Tb_BTG_NAVShares " +
                               " WHERE RefDate='" + dtpYesterday.Value.ToString("yyyy-MM-dd") + "'";

            ClearAllData(grpPort, "Port");
            LoadData(grpPort, "Port", StringSQL);

        }

        void LoadData(Control GetControl,string Complement,string StringSQL)
        {
            int IdPortfolio = 0;
            string ControlValue = "";

            using (newNestConn curConn = new newNestConn())
            {
                DataTable tablep = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in tablep.Rows)
                {
                    IdPortfolio = Convert.ToInt32(curRow["IdPortfolio"].ToString());
                    //ControlValue = curRow[""].ToString();

                    lock (padLock)
                    {
                        ((Label)GetControl.Controls["lbl" + GetPortName(IdPortfolio) + "_" + Complement]).Text = "OK";
                        ((Label)GetControl.Controls["lbl" + GetPortName(IdPortfolio) + "_" + Complement]).ForeColor = Color.Green;
                    }
                }
            }
        }

        string GetPortName(int IdPortfolio)
        {
            string PortName = "";

            switch (IdPortfolio)
            {
                case 4:
                    PortName = "NFund";
                    break;
                case 43:
                    PortName = "Mile";
                    break;
                case 10:
                    PortName = "Fia";
                    break;
                case 18:
                    PortName = "Quant";
                    break;
                case 38:
                    PortName = "Arb";
                    break;
                default:
                    break;
            }

            return PortName;
        
        }

        void ClearAllData(Control GetControl,string Complement)
        {
            ClearData(GetControl, Complement, 4);
            ClearData(GetControl, Complement, 43);
            ClearData(GetControl, Complement, 10);
            ClearData(GetControl, Complement, 18);
            ClearData(GetControl, Complement, 38);
        }

        void ClearData(Control GetControl,string Complement,int IdPortfolio)
        {
            lock (padLock)
            {
                 ((Label)GetControl.Controls["lbl" + GetPortName(IdPortfolio) + "_" + Complement]).Text = "NULL";
                ((Label)GetControl.Controls["lbl" + GetPortName(IdPortfolio) + "_" + Complement]).ForeColor = Color.Red;
            }
        
        }
    }
}
