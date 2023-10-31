using System;

using LiveDLL;

namespace LiveBook
{
    public partial class frmRiskSectors : LBForm
    {
        newNestConn curConn = new newNestConn();
        LB_HTML HTMLEngine = new LB_HTML();

        public frmRiskSectors()
        {
            InitializeComponent();
        }

        private void frmRiskSectors_Load(object sender, EventArgs e)
        {
            cmbPortfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            LiveDLL.FormUtils.LoadCombo(this.cmbPortfolio, "Select Id_Portfolio, Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=2 and Discountinued = 0", "Id_Portfolio", "Port_Name", 10);
            cmbPortfolio.SelectedIndex = -1;
            cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbPortfolio.SelectedValue = Id_Portfolio;
        }

        private void UpdateRisk()
        {
            string tempHTML = "";

            webRiskSectors.Navigate("about:blank");

            tempHTML = tempHTML + "<html><body>";
            string PortName = "";

            if (cmbPortfolio.SelectedValue != null)
            {
                PortName = curConn.Execute_Query_String("Select Port_Name from dbo.Tb002_Portfolios where Id_Portfolio=" + cmbPortfolio.SelectedValue.ToString());

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=17pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>" + PortName + "</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

                tempHTML = tempHTML + HTMLEngine.Risk_Sectors((int)cmbPortfolio.SelectedValue);

                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>¹ Interest Rate Exposure expressed as US 10y duration equivalent. Positive numbers indicate long exposure in bonds (short rates)</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                tempHTML = tempHTML + "</body></html>";
            }
            webRiskSectors.Document.Write(tempHTML);
            webRiskSectors.Refresh();
        }

        private string ifStrNull(object strValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return "&nbsp;";
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return "&nbsp;";
            }
            else if (Convert.ToSingle(strValue.ToString()) == 0)
            {
                return "-&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            else
            {
                return Convert.ToSingle(strValue.ToString()).ToString("0.00%");
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateRisk();
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRisk();
        }

    }
}