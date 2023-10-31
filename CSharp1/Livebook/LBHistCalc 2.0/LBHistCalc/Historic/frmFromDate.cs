using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;

namespace LBHistCalc
{
    public partial class frmFromDate : Form
    {
        public frmFromDate()
        {
            InitializeComponent();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            HCalcPortfolio_FromDate(dtpFromDate.Value, (int)cmbIdPortfolio.SelectedValue);
        }

        private void HCalcPortfolio_FromDate(DateTime InitialDate, int IdPortfolio)
        {
            while(InitialDate.Date != DateTime.Now.Date)
            {
                if(InitialDate.DayOfWeek != DayOfWeek.Sunday && InitialDate.DayOfWeek != DayOfWeek.Saturday)
                    using(newNestConn curConn = new newNestConn())
                    {
                        string SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(" + IdPortfolio + ",'" + InitialDate.ToString("yyyyMMdd") + "',3,0,0)";
                        curConn.ExecuteNonQuery(SQLString, 1);
                    }
                InitialDate = InitialDate.AddDays(1);
            }
        }

        private void frmfromDate_Load(object sender, EventArgs e)
        {
            LiveDLL.FormUtils.LoadCombo(cmbIdPortfolio, "Select Id_Portfolio,Port_Name from  Tb002_Portfolios where Id_Port_Type=2 and Discountinued <> 1 ", "Id_Portfolio", "Port_Name", -1);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
