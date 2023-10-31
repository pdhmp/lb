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
    public partial class frmLiquidity : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        LB_HTML HTMLEngine = new LB_HTML();

        public frmLiquidity()
        {
            InitializeComponent();
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbPortfolio.SelectedValue = Id_Portfolio;
        }

        private void frmRiskOptions_Load(object sender, EventArgs e)
        {
            
            cmbPortfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            CargaDados.carregacombo(this.cmbPortfolio, "Select Id_Portfolio, Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 10);
            cmbPortfolio.SelectedIndex = -1;
            cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
        }

        private void UpdateRisk()
        {
                webRiskOptions.Navigate("about:blank");

                string tempHTML = "";

                tempHTML = tempHTML + "<html><body>";

                string PortName = "";
                try
                {

                PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + cmbPortfolio.SelectedValue.ToString());

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt; font-weight:bold'>" + PortName + "</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

                tempHTML = tempHTML + HTMLEngine.Risk_Liquidity((int)cmbPortfolio.SelectedValue, false);

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>� Numer of days to reduce the porfolio gross exposure to the target value being 1/3 of daily volume.</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>� Percent of portfolio gross exposure that can be sold in that number of days being 1/3 of daily volume.</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
                tempHTML = tempHTML + "</body></html>";

                webRiskOptions.Document.Write(tempHTML);
                webRiskOptions.Refresh();
            }

            catch (Exception e)
            {
               // MessageBox.Show(e.ToString());
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

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRisk();
        }
    }
}