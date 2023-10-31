using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;

using NestDLL;

namespace FeedXMLIcap
{
    public partial class FeedXMLIcap : Form
    {
        private int Status = 0;
        private DateTime LastTime = DateTime.MinValue;
        private int InsertedIdsCounter = 0;

        public FeedXMLIcap()
        {
            InitializeComponent();

            Process_File();

            timer1.Start();
        }

        private void Import_File()
        {
            Status = 1;

            XmlDocument curDocument = new XmlDocument();

            string FileName = @"C:\TEMP\TEST_NEST.XML";
            curDocument.Load(FileName);

            XmlNodeList arrNodes = curDocument.SelectNodes("/Root/order");
            string StringSQL = "";

            foreach (XmlNode node in arrNodes)
            {
                bool insert = true;

                DateTime TradeDate = DateTime.Parse(node.ChildNodes[0].InnerText);
                TimeSpan TradeTime = TimeSpan.Parse(node.ChildNodes[1].InnerText);
                string Symbol = node.ChildNodes[2].InnerText;
                double Quantity = double.Parse(node.ChildNodes[3].InnerText.Replace('.',','));
                double Price = double.Parse(node.ChildNodes[4].InnerText.Replace('.', ','));
                int Account = int.Parse(node.ChildNodes[5].InnerText);
                string ExecID = node.ChildNodes[6].InnerText;
                string Side = node.ChildNodes[7].InnerText;
                int IdStrategy = int.Parse(node.ChildNodes[8].InnerText);

                int SideNumber = 0;
                if (Side == "C") { SideNumber = 1; }
                else if (Side == "V") { SideNumber = 2; }
                else { insert = false; }

                string TargetSubID = "";
                int IdBook = 0;
                int IdSection = 0;
                if (Account == 99052006 || Account == 190810)
                {
                    TargetSubID = "FXI";
                    IdBook = 11;

                    if (IdStrategy == 1767) { IdSection = 221; } //Martim Pescador
                    else if (IdStrategy == 1575) { IdSection = 223; } //Jericho
                    else if (IdStrategy == 3625) { IdSection = 171; } // Patriot
                    else { insert = false; }
                }

                DateTime TransactTime = TradeDate.Add(TradeTime);

                if (!CheckExistingTrade(Account, Symbol, ExecID))
                {
                    insert = false;
                }


                if (insert)
                {
                     StringSQL = " EXEC [NESTDB].[dbo].[proc_FIX_InsExecReport_parameters] " +
                                 "   @Broker_Id = 19 " + 
                                 "  ,@Account = '" + Account.ToString() + "'" +
                                 "  ,@AvgPx = " + Price.ToString().Replace(',','.') +
                                 "  ,@ClOrdID = '" + ExecID + "'" +
                                 "  ,@CumQty = " + Quantity.ToString().Replace(',', '.') +
                                 "  ,@ExecID = '" + ExecID + "'" +
                                 "  ,@ExecTransType = '0'" +
                                 "  ,@LastPx = " + Price.ToString().Replace(',', '.') +
                                 "  ,@LastShares = " + Quantity.ToString().Replace(',', '.') + 
                                 "  ,@OrderID = '" + ExecID + "'" +
                                 "  ,@OrderQty = " + Quantity.ToString().Replace(',', '.') +
                                 "  ,@OrdStatus = '0'" +
                                 "  ,@Side = '" + SideNumber.ToString() + "'" +
                                 "  ,@Symbol = '" + Symbol + "'" +
                                 "  ,@Texto = ''" +
                                 "  ,@OrigClOrdID = ''" +
                                 "  ,@TransactTime = '" + TransactTime.ToString("MM/dd/yyyy HH:mm") + "'" +
                                 "  ,@SendingTime = '" + TransactTime.ToString("MM/dd/yyyy HH:mm") + "'" +
                                 "  ,@ExecType = '1'" +
                                 "  ,@LeavesQty = 0 " + 
                                 "  ,@TargetSubID ='" + TargetSubID + "'" +
                                 "  ,@Id_Login_Program=31" +
                                 "  ,@Manual_Feed=0 " +
                                 "  ,@IDBook=" + IdBook + " " +
                                 "  ,@IDSection=" + IdSection + " ;";

                     using (newNestConn curConn = new newNestConn())
                     {
                         //curConn.ExecuteNonQuery(StringSQL);
                     }

                     InsertedIdsCounter++;
                }              
            }

            LastTime = DateTime.Now;
            Status = 2;

            InsertedIdsCounter = 0;

        }

        private void Load_File()
        {
            string ftpPath = "ftp://nestinvest:ni1050@whw0054.whservidor.com";
            string remoteFilePath = "Dados/executed-equities-orders.xml";
            string localFilePath = @"c:\temp\downloadteste.xml";

            // O ftpPath deve ser montado da seguinte maneira:
            // ftp://username:password@hostftp:port
            Uri fileURI = new Uri(ftpPath + "/" + remoteFilePath);

            //Cria FtpWebRequest com o Uri que montamos
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(fileURI);

            //Fechar conexão após execução do comando
            request.KeepAlive = false;

            //Especifica o comando a ser executado
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            //Utiliza modo passivo
            request.UsePassive = true;

            //Especifica o tipo de transferencia de dados
            request.UseBinary = true;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream fileStream = new FileStream(localFilePath, FileMode.Create);

                //Divide o arquivo em lotes de 2048 bytes e baixa cada parte
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesToSave = responseStream.Read(buffer, 0, Length);

                while (bytesToSave > 0)
                {
                    //Transfere o conteúdo do buffer para o arquivo
                    fileStream.Write(buffer, 0, bytesToSave);

                    //Baixa a proxima parte do arquivo e carrega o buffer
                    bytesToSave = responseStream.Read(buffer, 0, Length);

                }

                fileStream.Close();
                response.Close();
            }
            catch(Exception e)
            {
                StreamWriter LogFile = new StreamWriter(@"C:\temp\FeedXMLIcap_Log_Error.txt", true);
                LogFile.WriteLine(DateTime.Today.ToString("dd/MM/yyy") + " Erro no download do arquivo:");
                LogFile.Write(e.ToString());
                LogFile.WriteLine("Inner Exception:");
                LogFile.Write(e.InnerException.ToString());
                LogFile.WriteLine("");
                LogFile.Close();

                MessageBox.Show("Erro no download do arquivo. \r\n Verifique arquivo de log C:\\temp\\FeedXMLIcap_Log_Error.txt", "Erro download", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckExistingTrade(int Account, string Symbol, string ExecID)
        {
            bool result = false;

            string SQLExpression = " SELECT * FROM NESTDB.DBO.TB800_FIX_DROP_COPIES " +
                                   " WHERE BROKER_ID = 19 " +
                                   " AND ACCOUNT = " + Account.ToString() +
                                   " AND SYMBOL = '" + Symbol + "'" +
                                   " AND ORDERID = '" + ExecID + "'" +
                                   " AND EXECID = '" + ExecID + "'";

            DataTable dt = new System.Data.DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                dt = curConn.Return_DataTable(SQLExpression);
            }

            if (dt.Rows.Count == 0)
            {
                result = true;
            }

            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now <= DateTime.Today.AddHours(20))
            {
                Process_File();
            }           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblLastUpdate.Text = LastTime.ToString("HH:mm:ss.ms");
            lblInserted.Text = InsertedIdsCounter.ToString();
            lblStatus.Text = Status.ToString();
        }

        private void Process_File()
        {
            Load_File();
            Import_File();
        }
    }
}
