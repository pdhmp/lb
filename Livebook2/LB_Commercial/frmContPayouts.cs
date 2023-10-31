using System;
using System.Windows.Forms;


namespace LiveBook
{
    public partial class frmContPayouts : LBForm
    {

        LB_HTML HTMLEngine = new LB_HTML();

        public frmContPayouts()
        {
            InitializeComponent();
        }

        private void frmContPayouts_Load(object sender, EventArgs e)
        {
            //UpdateReport();
            this.cmbOfficer.SelectedIndexChanged -= new System.EventHandler(this.cmbOfficer_SelectedIndexChanged);
            LiveDLL.FormUtils.LoadCombo(cmbOfficer, "SELECT Id_Pessoa, Login FROM NESTDB.dbo.Tb014_Pessoas (nolock) WHERE IsOfficer=1 UNION SELECT -1, 'All Officers' ", "Id_Pessoa", "Login", 99);

            string tempUnions = " UNION SELECT 50, 'Nest Previdencia' ";
            tempUnions += " UNION SELECT -1, 'All Portfolios' ";
            tempUnions += " UNION SELECT -2, 'MH Consolidated' ";

            LiveDLL.FormUtils.LoadCombo(cmbPortfolio, "SELECT Id_Portfolio,Port_Name FROM VW_Portfolios WHERE Id_Port_Type= 3 " + tempUnions, "Id_Portfolio", "Port_Name", 99);


            this.cmbOfficer.SelectedIndexChanged += new System.EventHandler(this.cmbOfficer_SelectedIndexChanged);
        }

        private void UpdateReport()
        {
            try
            {
                webRiskOptions.Navigate("about:blank");

                string tempHTML = "";

                tempHTML = tempHTML + "<html><body>";

                tempHTML = tempHTML + HTMLEngine.Cont_Payouts((int)cmbPortfolio.SelectedValue, (int)cmbOfficer.SelectedValue);

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                tempHTML = tempHTML + "</body></html>";

                webRiskOptions.Document.Write(tempHTML);
                webRiskOptions.Refresh();
            }
            catch
            {

            }
        }

        private void cmbOfficer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void cmbPortfolio_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            UpdateReport();
        }
    }
}