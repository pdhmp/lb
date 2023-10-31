using System;
using System.Data;
using System.Windows.Forms;


namespace LiveBook
{
    public partial class frmRiskReportArb : LBForm
    {
        
        LB_HTML HTMLEngine = new LB_HTML();

        public frmRiskReportArb()
        {
            InitializeComponent();
        }

        private void frmRiskLimits_Load(object sender, EventArgs e)
        {
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
            dtpDate.Value = DateTime.Now;
        }

        private void UpdateRisk()
        {
            webRiskLimits.Navigate("about:blank");

            DataTable tablep = new DataTable();
            string tempHTML = "";

            try
            {

                tempHTML = tempHTML + "<html><body>";

                tempHTML = tempHTML + HTMLEngine.Risk_ReportSummary_Arb(dtpDate.Value);

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                tempHTML = tempHTML + "</body></html>";

                webRiskLimits.Document.Write(tempHTML);
                webRiskLimits.Refresh();
            }
            catch
            {
            
            }
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
            else
            {
                return Convert.ToSingle(strValue.ToString()).ToString("0.00%");
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            cmdRefresh.Enabled = false;
            UpdateRisk();
            cmdRefresh.Enabled = true;
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            cmdRefresh.Enabled = false;
            UpdateRisk();
            cmdRefresh.Enabled = true;
        }

        private void chkInternal_CheckedChanged(object sender, EventArgs e)
        {
            cmdRefresh.Enabled = false;
            UpdateRisk();
            cmdRefresh.Enabled = true;
        }

        private void webRiskLimits_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateRisk();
        }
    }
}