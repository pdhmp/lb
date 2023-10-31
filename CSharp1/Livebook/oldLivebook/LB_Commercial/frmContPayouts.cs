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
    public partial class frmContPayouts : LBForm
    {

        LB_HTML HTMLEngine = new LB_HTML();

        public frmContPayouts()
        {
            InitializeComponent();
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbPortfolio.SelectedValue = Id_Portfolio;
        }

        private void frmContPayouts_Load(object sender, EventArgs e)
        {
            
            //UpdateReport();
            this.cmbOfficer.SelectedIndexChanged -= new System.EventHandler(this.cmbOfficer_SelectedIndexChanged);
            NestDLL.FormUtils.LoadCombo(cmbOfficer, "SELECT Id_Pessoa, Login FROM NESTDB.dbo.Tb014_Pessoas (nolock) WHERE IsOfficer=1 UNION SELECT -1, 'All Officers' ", "Id_Pessoa", "Login", 99);
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

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void cmbOfficer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateReport();
        }

        private void webRiskOptions_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}