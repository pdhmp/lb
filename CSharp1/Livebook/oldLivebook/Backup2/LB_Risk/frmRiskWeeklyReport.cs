using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SGN.Business;
using SGN.Validacao;
using NestDLL;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SGN
{
    public partial class frmRiskWeeklyReport : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        LB_HTML HTMLEngine = new LB_HTML();

        public frmRiskWeeklyReport()
        {
            InitializeComponent();
        }

        private void frmRiskLimits_Load(object sender, EventArgs e)
        {
            
            UpdateRisk();
        }

        private void UpdateRisk()
        {
            webRiskLimits.Navigate("about:blank");

            DataTable tablep = new DataTable();
            string tempHTML = "";

            try
            {

                tempHTML = tempHTML + "<html><body>";
                tempHTML = tempHTML + "<table border=0>";

                tempHTML = tempHTML + "<tr><td colspan=2>";

                tempHTML = tempHTML + HTMLEngine.Risk_Summary((int)cmbPortfolio.SelectedValue, chkInternal.Checked);
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=8pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "</td></tr>";

                tempHTML = tempHTML + "<tr><td colspan=2 valign=TOP>";

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Portfolio Characteristics</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

                tempHTML = tempHTML + HTMLEngine.Risk_Sectors((int)cmbPortfolio.SelectedValue);

                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>¹ Interest Rate Exposure expressed as US 10y duration equivalent. Positive numbers indicate long exposure in bonds (short rates)</div>";

                tempHTML = tempHTML + "</td></tr>";

                tempHTML = tempHTML + "<tr><td valign=TOP>";

                tempHTML = tempHTML + HTMLEngine.Risk_Liquidity((int)cmbPortfolio.SelectedValue, true);

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>¹ Numer of days to reduce the porfolio <BR>gross exposure to the target value <BR>being 1/3 of daily volume.</div>";

                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td valign=TOP>";

                tempHTML = tempHTML + HTMLEngine.Risk_Options((int)cmbPortfolio.SelectedValue, true);

                tempHTML = tempHTML + "</td></tr>";

                tempHTML = tempHTML + "</table>";

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

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdRefresh.Enabled = false;
            UpdateRisk();
            cmdRefresh.Enabled = true;
        }
    }
}