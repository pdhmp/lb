using System;
using System.Windows.Forms;

using LiveDLL;

namespace LiveBook
{
    public partial class frmDeleteFileBrokerage : Form
    {

        private LB_Utils curUtils = new LB_Utils();

        private newNestConn curConn = new newNestConn();

        public string FileDate;

        public frmDeleteFileBrokerage()
        {
            InitializeComponent();
            dtpFileDate.MinDate = DateTime.Today;
            dtpFileDate.MaxDate = DateTime.Today.AddDays(1);
        }

        private void frmDeleteFileBrokerage_Load(object sender, EventArgs e)
        {
            DateTime x = dtpFileDate.Value;

            FileDate = x.ToString("yyyy-MM-dd");

            LoadPortfolio();
        }



        private void LoadPortfolio()
        {
            string StringSQL;
            StringSQL = " Select Id_Portfolio,Port_Name from " +
                         " NESTDB.dbo.VW_Portfolios  " +
                         " where Id_Port_Type=2 and Id_Portfolio in  ( Select IdPortfolio FROM NESTIMPORT.dbo.Tb_NegociosBRL Where DATAPREGAO='" + FileDate + "') " +
                         " union all " +
                         " Select -1 as Id_Portfolio , '' as Port_Name";

            LiveDLL.FormUtils.LoadCombo(this.cmbPortfolio, StringSQL, "Id_Portfolio", "Port_Name");
        }

        private void LoadBroker()
        {
            int IdPortfolio;

            if (int.TryParse(cmbPortfolio.SelectedValue.ToString(), out IdPortfolio))
            {
                if (IdPortfolio != -1)
                {
                    string StringSQL;
                    StringSQL = " SELECT B.Id_Broker,B. Nome " +
                                " FROM NESTIMPORT.DBO.Tb_NegociosBRL A " +
                                " INNER JOIN NESTDB..VW_Account_Broker B " +
                                " ON a.IdBroker = b.Id_Broker   or  A.CORRETORA = B.Nome " +
                                " WHERE DATAPREGAO='" + FileDate + "' and IdPortfolio = " + IdPortfolio +
                                " GROUP BY B.Id_Broker,B.Nome " +
                                " ORDER BY b.Nome "                                ;

                    LiveDLL.FormUtils.LoadCombo(this.cmbBroker, StringSQL, "Id_Broker", "Nome");
                }
            }
        }

        private void DeleteFile()
        {
            int IdPortfolio;

            if (int.TryParse(cmbPortfolio.SelectedValue.ToString(), out IdPortfolio))
            {
                if (IdPortfolio != -1)
                {
                    int IdBroker = Convert.ToInt32(cmbBroker.SelectedValue.ToString());
                    bool FilterFuture = rdoFutures.Checked;

                    int resposta;
                    string SQLString;
                    resposta = Convert.ToInt32(MessageBox.Show("Are you sure you want to delete this file?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                    if (resposta == 6)
                    {
                        SQLString = "EXEC NESTIMPORT.dbo.Proc_DeleteImportedFilesBrokers  " +
                            IdPortfolio + "," +
                            IdBroker + ",'" +
                            FileDate + "'," +
                            (FilterFuture ? "1;" : "0;");

                        int retorno = curConn.ExecuteNonQuery(SQLString);

                        if (retorno > 0)
                        {
                            MessageBox.Show("Deleted!");
                        }
                        else
                        {
                            MessageBox.Show("ERROR!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR!");
                    }
                }
            }

            LoadPortfolio();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBroker();
        }




    }
}