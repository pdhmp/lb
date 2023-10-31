using System;
using System.Windows.Forms;


namespace LiveBook
{
    public partial class frmResearchNews : LBForm
    {
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