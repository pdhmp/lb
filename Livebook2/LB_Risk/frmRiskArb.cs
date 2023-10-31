using System;
using System.Data;
using System.Threading;

namespace LiveBook
{
    public partial class frmRiskArb : LBForm
    {

        LB_HTML HTMLEngine = new LB_HTML();

        public frmRiskArb()
        {
            InitializeComponent();

        }

        private void frmRiskLimits_Load(object sender, EventArgs e)
        {
            chkExcludeLimit.Checked = true;
            UpdateRisk();
        }

        private void UpdateRisk()
        {
            webRiskLimits.Navigate("about:blank");

            DataTable tablep = new DataTable();
            string tempHTML = "";

            try
            {
                if (chkExcludeLimit.Checked == false)
                {
                    tempHTML = tempHTML + "<html><body>";

                    tempHTML = tempHTML + HTMLEngine.Risk_Summary_Arb(dtpDate.Value);

                    tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                    tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                    tempHTML = tempHTML + "</body></html>";

                    webRiskLimits.Document.Write(tempHTML);
                    webRiskLimits.Refresh();
                }
                else
                {
                    tempHTML = tempHTML + "<html><body>";

                    tempHTML = tempHTML + HTMLEngine.Risk_ReportSummary_Arb(dtpDate.Value);

                    tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                    tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                    tempHTML = tempHTML + "</body></html>";

                    webRiskLimits.Document.Write(tempHTML);
                    webRiskLimits.Refresh();

                }


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

        private void chkInternal_CheckedChanged(object sender, EventArgs e)
        {
            cmdRefresh.Enabled = false;
            //UpdateRisk();
            cmdRefresh.Enabled = true;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            //UpdateRisk();
        }

        private void chkExcludeLimit_CheckedChanged(object sender, EventArgs e)
        {
            //UpdateRisk();
        }

        private void webRiskLimits_DocumentCompleted(object sender, System.Windows.Forms.WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}