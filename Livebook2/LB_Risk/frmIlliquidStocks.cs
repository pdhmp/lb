using System;
using LiveDLL;

namespace LiveBook
{
    public partial class frmIlliquidStocks : LBForm
    {
        newNestConn curConn = new newNestConn();

        LB_HTML HTMLEngine = new LB_HTML();

        public frmIlliquidStocks()
        {
            InitializeComponent();
        }

        private void frmIlliquidStocks_Load(object sender, EventArgs e)
        {
            UpdateScreen();
        }

        public void UpdateScreen()
        {
            webRiskOptions.Navigate("about:blank");

            string tempHTML = "";

            tempHTML = tempHTML + "<html><body>";

            try
            {
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt;'>&nbsp;</div>";

                tempHTML = tempHTML + HTMLEngine.Illiquid_Stocks();

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                tempHTML = tempHTML + "</body></html>";

                webRiskOptions.Document.Write(tempHTML);
                webRiskOptions.Refresh();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateScreen();
        }
    }
}
