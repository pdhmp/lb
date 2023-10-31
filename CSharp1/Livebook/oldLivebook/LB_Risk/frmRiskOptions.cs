using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using LiveBook.Business;

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

namespace LiveBook
{
    public partial class frmRiskOptions : LBForm
    {
        LB_HTML HTMLEngine = new LB_HTML();

        public frmRiskOptions()
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
            NestDLL.FormUtils.LoadCombo(cmbPortfolio, "Select Id_Portfolio, Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 99);
            cmbPortfolio.SelectedIndex = -1;
            cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
        }

        private void UpdateRisk()
        {
            try
            {
                webRiskOptions.Navigate("about:blank");

                string tempHTML = "";

                tempHTML = tempHTML + "<html><body>";
                if(cmbPortfolio.SelectedValue != null)
                    tempHTML = tempHTML + HTMLEngine.Risk_Options((int)cmbPortfolio.SelectedValue);

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