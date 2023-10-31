using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using NestDLL;

namespace CBLCRates
{
    public partial class frmImport : Form
    {
        public newNestConn curConn = new newNestConn();

        public frmImport()
        {
            InitializeComponent();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            StartRoutine();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartRoutine();
        }


        void StartRoutine()
        {
            Download_File();
            ImportCLBCToday();
            Application.Exit();
        }

        private void Download_File()   
        {
            DateTime TradeDate;

            TradeDate = Convert.ToDateTime(curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',-1,'" + DateTime.Now.ToString("yyyyMMdd") + "',2,1)"));

            lblStatus.Text = "Downloading...";
            WebClient webClient = new WebClient();
            webClient.DownloadFile("http://www.cblc.com.br/cblc/consultas/btc/FormDownload.asp?arquivo=../Arquivos/DBTCER9999.txt", @"T:\Import\CBLC\StockLoanRates.txt");

            webClient.DownloadFile("http://www.cblc.com.br/cblc/consultas/Arquivos/DBTC" + TradeDate.ToString("yyyyMMdd") + ".dat", @"T:\Import\CBLC\StockLoanContractsOpen.txt");

            lblStatus.Text = "Download Finished!";
        }

        void ImportCLBCToday()
        {
            lblStatus.Text = "Importing";
  
            string filename=@"T:\Import\CBLC\StockLoanRates.txt";

            StreamReader SR;
            string StringLine;

            SR=File.OpenText(filename);
            StringLine = SR.ReadLine();
            string Security;
            string NumeroContratos;
            string QtdAcoes;
            decimal ValorTotal;
            decimal TxMediaDoador;
            decimal TxMediaTomador;
            string StringSQL="";
            string TradeDate = "";

            while (StringLine != null)
            {
                //Console.WriteLine(StringLine);
                if (StringLine.Substring(0, 6) == "00DBTC")
                {
                    TradeDate = StringLine.Substring(32, 8).Trim();
                }
                if (StringLine.Substring(0, 2) == "01")
                {
                    Security = StringLine.Substring(2, 19).Trim();
                    NumeroContratos = StringLine.Substring(52, 10);
                    QtdAcoes = StringLine.Substring(62, 11);

                    ValorTotal = Convert.ToDecimal(StringLine.Substring(73, 20)) / 100;
                    TxMediaDoador = Convert.ToDecimal(StringLine.Substring(93, 7)) / 100;
                    TxMediaTomador = Convert.ToDecimal(StringLine.Substring(100, 7))/100;

                    StringSQL = StringSQL + " EXEC NESTIMPORT.dbo.proc_InsertStockLoansCBLC '" + TradeDate + "','" + Security + "'," + NumeroContratos + "," + QtdAcoes + "," + ValorTotal.ToString().Replace(",", ".") + "," + TxMediaDoador.ToString().Replace(",", ".") + "," + TxMediaTomador.ToString().Replace(",", ".") + " ; ";

                }
                StringLine = SR.ReadLine();
            }
            SR.Close();

            using (newNestConn curConn = new newNestConn())
            {
                lblStatus.Text = "Inserting...";
                curConn.ExecuteNonQuery("DELETE FROM NESTIMPORT.DBO.TB_StockLoansCBLC WHERE TradeDate = '" + TradeDate + "'");
                curConn.ExecuteNonQuery(StringSQL);
            }
            lblStatus.Text = "Imported!";
            ImportCLBCContractsOpen();
        }

        void ImportCLBCContractsOpen()
        {
            lblStatus.Text = "Importing";

            string filename = @"T:\Import\CBLC\StockLoanContractsOpen.txt";

            StreamReader SR;
            string StringLine;

            SR = File.OpenText(filename);
            StringLine = SR.ReadLine();
            string Security;
            string IssuerName;
            string SecurityType;
            string ISIN;
            double ValorContratosFechados;
            double PrecoMedio;
            double FatorCotacao;
            double SaldoContratosReais;


            string StringSQL = "";
            string TradeDate = "";

            while (StringLine != null)
            {
                Console.WriteLine(StringLine);
                if (StringLine.Substring(0, 6) == "00DBTC")
                {
                    TradeDate = StringLine.Substring(30, 8).Trim();
                }
                if (StringLine.Substring(0, 2) == "01")
                {
                    Security = StringLine.Substring(2, 12).Trim();
                    IssuerName = StringLine.Substring(14, 12).Trim();
                    SecurityType = StringLine.Substring(26, 3).Trim();
                    ISIN = StringLine.Substring(29, 11).Trim();
                    ValorContratosFechados = Convert.ToDouble(StringLine.Substring(41, 18).Trim());

                    PrecoMedio = NestDLL.Utils.ParseToDouble(StringLine.Substring(60, 18).Trim().Replace("000000", ""));
                    FatorCotacao = Convert.ToDouble(StringLine.Substring(77, 7).Trim());
                    SaldoContratosReais = Convert.ToDouble(StringLine.Substring(85, 18).Trim());

                    StringSQL += " INSERT INTO NESTIMPORT.DBO.TB_OpenStockLoansCBLC " +
                                  " SELECT '" + TradeDate + "'" +
                                  ",'" + Security + "'" +
                                  ",'" + IssuerName + "'" +
                                  ",'" + SecurityType + "'" +
                                  ",'" + ISIN + "'" +
                                  "," + (ValorContratosFechados).ToString().Replace(",", ".")+
                                  "," + (PrecoMedio/100).ToString().Replace(",", ".") +
                                  "," + FatorCotacao.ToString().Replace(",", ".") +
                                  "," + (SaldoContratosReais/100).ToString().Replace(",", ".") + ",0,0; ";
                }
                StringLine = SR.ReadLine();
            }
            SR.Close();

            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("DELETE FROM NESTIMPORT.DBO.TB_OpenStockLoansCBLC WHERE TradeDate = '" + TradeDate + "'");
                lblStatus.Text = "Inserting...";
                curConn.ExecuteNonQuery(StringSQL);
            }
            InsertData(TradeDate);

            lblStatus.Text = "Imported!";

        }


        void InsertData(string TradeDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                 lblStatus.Text = "Inserting IdSecurity";
                curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.Proc_InsertSecurityCBLC");
                lblStatus.Text = "Inserting in Table Price";
                curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.ProcInserDataCBLC '" + TradeDate + "'");
                lblStatus.Text = "Inserted";
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

   
    }
}
