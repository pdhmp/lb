using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;

namespace LiveBook
{
    public partial class frmDeleteFileBrokerage : Form
    {

        private LB_Utils curUtils = new LB_Utils();

        private newNestConn curConn = new newNestConn();

        public DateTime FileDate;

        public frmDeleteFileBrokerage()
        {
            InitializeComponent();
            dtpFileDate.MinDate = DateTime.Today;
            dtpFileDate.MaxDate = DateTime.Today.AddDays(1);
        }

        private void LoadDate()
        {
            dtpFileDate.Value = FileDate;
        }

        private void LoadPortfolio()
        {
            string StringSQL;
            StringSQL = " Select Id_Portfolio,Port_Name from NESTDB.dbo.VW_Portfolios " +
                        " where Id_Port_Type=2 and Id_Portfolio in " +
                        " ( Select IdPortfolio FROM NESTIMPORT.dbo.Tb_NegociosBRL" +
                        " Where DATAPREGAO='" + FileDate.ToString("yyyyMMdd") + "')";

            NestDLL.FormUtils.LoadCombo(this.cmbPortfolio, StringSQL, "Id_Portfolio", "Port_Name", 99);
       }

        private void LoadBroker()
        {
            int IdPortfolio;

            IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());

            string StringSQL;
            StringSQL = " Select Id_Corretora,Nome from NESTDB.dbo.Tb011_Corretoras " +
                        " where Id_Corretora in  " +
                        " ( Select IdBroker FROM NESTIMPORT.dbo.Tb_NegociosBRL " +
                        " Where DATAPREGAO='" + FileDate.ToString("yyyyMMdd") + "' and IdPortfolio = " + IdPortfolio + ")";

            NestDLL.FormUtils.LoadCombo(this.cmbBroker, StringSQL, "Id_Corretora", "Nome", 99);
        }

        private void DeleteFile()
        {
            string Data = dtpFileDate.Value.ToString("yyyyMMdd");
            int IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
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
                    Data + "'," +
                    (FilterFuture ? "1;" : "0;");

                int retorno = curConn.ExecuteNonQuery(SQLString, 1);

                if (retorno != 0)
                {
                    MessageBox.Show("Deleted!");
                }
                if (retorno == 99)
                {
                    MessageBox.Show("ERROR!");
                }
            }
            else
            {
                MessageBox.Show("ERROR!");
            }
        }

        private void frmApagaPL_Load(object sender, EventArgs e)
        {
            dtpFileDate.Value = FileDate;
            LoadDate();
            LoadPortfolio();
            this.cmbPortfolio.SelectedValueChanged += new System.EventHandler(this.cmbPortfolio_SelectedValueChanged);
            LoadBroker();
        }
      
        private void cmbPortfolio_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadBroker();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteFile();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}