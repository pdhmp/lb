using System;



namespace LiveBook
{
    public partial class frmRisk_Scenarios : LBForm
    {
        LB_HTML HTMLEngine = new LB_HTML();

        public frmRisk_Scenarios()
        {
            InitializeComponent();
        }

        private void frmRiskOptions_Load(object sender, EventArgs e)
        {
            
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
        }

        private void UpdateRisk()
        {
            webRiskScenarios.Navigate("about:blank");

            string tempHTML = "";

            tempHTML = tempHTML + "<html><body>";

            tempHTML = tempHTML + HTMLEngine.Risk_Scenarios();

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
            tempHTML = tempHTML + "</body></html>";

            webRiskScenarios.Document.Write(tempHTML);

            webRiskScenarios.Refresh();

        }

        private string ifStrNull(object strValue, string strFormat)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return "&nbsp;";
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return "&nbsp;";
            }
            else
            {
                return Convert.ToSingle(strValue.ToString()).ToString(strFormat);
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            UpdateRisk();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateRisk();
        }

    }
}