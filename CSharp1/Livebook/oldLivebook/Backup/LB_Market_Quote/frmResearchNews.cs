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
    public partial class frmResearchNews : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmResearchNews()
        {
            InitializeComponent();
        }

        private void frmResearchNews_Load(object sender, EventArgs e)
        {
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();

            webResearchNews.Navigate("http://192.168.0.210/");

            this.webResearchNews.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webResearchNews_DocumentCompleted);
        }

        void webResearchNews_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webResearchNews.Zoom(int.Parse("75"));
        }

        private void UpdateRisk()
        {
            //webResearchNews.Refresh();
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
            webResearchNews.Zoom(int.Parse("50"));
            cmdRefresh.Enabled = true;
        }
    }
}