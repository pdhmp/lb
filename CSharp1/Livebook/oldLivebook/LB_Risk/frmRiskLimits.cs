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
    public partial class frmRiskLimits : LBForm
    {
        
        LB_HTML HTMLEngine = new LB_HTML();

        public frmRiskLimits()
        {
            InitializeComponent();
        }

        private void frmRiskLimits_Load(object sender, EventArgs e)
        {
            chkInternal.Checked = true;
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
        }

        private void UpdateRisk()
        {
            webRiskLimits.Navigate("about:blank");

            DataTable tablep = new DataTable();
            string tempHTML = "";

            try
            {
                /*
                string SQLString = "SELECT * FROM dbo.[FCN_Risk_Limits]() ORDER BY Id_Portfolio, Valid_As_Of, Id_Risk_Limit_Group, Risk_Limit_Type";

                if (!chkInternal.Checked)
                {
                    SQLString = SQLString.Replace("FROM dbo.[FCN_Risk_Limits]()", "FROM dbo.[FCN_Risk_Limits]() WHERE IsInternal<>1");
                }

                */
                tempHTML = tempHTML + "<html><body>";

                tempHTML = tempHTML + HTMLEngine.Risk_Summary(chkInternal.Checked);

                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>� Equity Only</div>";
                tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>� At Cost</div>";
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
    }
}