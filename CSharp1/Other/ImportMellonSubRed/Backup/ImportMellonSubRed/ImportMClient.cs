using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.IO;


using System;
using System.Web;


namespace ImportMellonSubRed
{
    public partial class ImportMClient : Form
    {
        string allData = "";
        int Id_Portfolio = -1;

        public ImportMClient()
        {
            InitializeComponent();
        }

        private void ImportMClient_Load(object sender, EventArgs e)
        {
            //int teste = CreateClientID("GENESIO", "APLIC");
        }
        
        private void cmdReadClip_Click(object sender, EventArgs e)
        {
            allData = Clipboard.GetText(TextDataFormat.Html);
            
            // =============== READ FROM FILE BLOCK
            //StreamReader sr = new StreamReader("U:\\Luis Fonseca\\teste.txt");//, Encoding.GetEncoding("ISO-8859-3"), false);
            //allData = sr.ReadToEnd();

            // =============== WRITE TO FILE BLOCK
            
            //StreamWriter thisWriter = new StreamWriter(@"c:\temp\teste.txt");
            //thisWriter.Write(allData);
           // thisWriter.Close();
            

            if (!allData.Contains("TABLE id=tblMovimentacoes"))
            {
                MessageBox.Show("The data in the clipboard does not seem to come from Mellon. Canceling import", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                allData = "";
                cmdImportFile.Enabled = false;
                return;
            }
            else
            {
                Id_Portfolio = GetFundID();

                if (Id_Portfolio != -1)
                {
                    cmdImportFile.Enabled = true;
                }
                else
                {
                    lblFundName.Text = "Fund Name not found!";
                    cmdImportFile.Enabled = false;
                }
            }
        }

        private int GetFundID()
        {
            int curPos = 0;
            curPos = allData.IndexOf("ctl00_lblNomeFundo", curPos);
            string curFundName = GetNextBetweenTagValue(allData, curPos);

            lblFundName.Text = curFundName;

            if (curFundName.Contains("ACOES") && curFundName.Contains("FIC"))
            {
                return 12;
            }
            else if (curFundName.Contains("MILE HIGH") && curFundName.Contains("30") && curFundName.Contains("FIC"))
            {
                return 3;
            }
            else if (curFundName.Contains("MILE HIGH") && !curFundName.Contains("30") && curFundName.Contains("FIC"))
            {
                return 2;
            }
            else if (curFundName.Contains("MH1") && curFundName.Contains("FIC"))
            {
                return 15;
            }

            else if (curFundName.Contains("ARB") && curFundName.Contains("FIC"))
            {
                return 40;
            }


            return -1;
        }

        private void cmdImportFile_Click(object sender, EventArgs e)
        {
            GetFileData();
        }

        private void GetFileData()
        {
            int insertCounter = 0;
            if (allData == "") return;
            

            allData = allData.Replace("\r\n", "");
            allData = allData.Replace("Ã", "A");
            allData = allData.Replace("Ã§Ã£", "ca");
            allData = allData.Replace("Ã‡AO", "CAO");
            allData = allData.Replace("&amp;", "&");
            allData = allData.Replace("Ã‡", "C");
            
            allData = allData.Replace("Çª", "C");
            allData = allData.Replace("Ãª", "e");

            allData = allData.Replace("Ã", "A");
            allData = allData.Replace("Á", "A");
            allData = allData.Replace("É", "E");
            
            int tagPos = 0;

            string tempDate;
            DateTime DataReferencia= Convert.ToDateTime("01/01/1900");

            while (tagPos != -1)
            {
                tagPos = allData.IndexOf("lblDataReferencia", tagPos);
                //string DataReferencia = GetNextBetweenTagValue(allData, tagPos);

                if (tagPos != -1)
                {
                    tagPos = allData.IndexOf("lblDataReferencia", tagPos);

                    tempDate = GetNextBetweenTagValue(allData, tagPos);
                    if (tempDate != "")
                    {
                        DataReferencia = DateTime.Parse(tempDate);
                    }
                    else
                    {
                      //  MessageBox.Show("sss");
                    }


                    tagPos = allData.IndexOf("lblDataMovimentacao", tagPos);
                    DateTime DataMovimentacao = DateTime.Parse(GetNextBetweenTagValue(allData, tagPos));

                    tagPos = allData.IndexOf("lblDataCotizacao", tagPos);
                    DateTime DataCotizacao = DateTime.Parse(GetNextBetweenTagValue(allData, tagPos));

                    tagPos = allData.IndexOf("lblDataLiquidacaoFinanceira", tagPos);
                    DateTime DataLiquidacaoFinanceira = DateTime.Parse(GetNextBetweenTagValue(allData, tagPos));

                    tagPos = allData.IndexOf("lblCliente", tagPos);
                    string lblCliente = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblMovimentacao", tagPos);
                    string Movimentacao = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblTipoTransferencia", tagPos);
                    string TipoTransferencia = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblQtdQuotas", tagPos);
                    string QtdQuotas = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblValorCota", tagPos);
                    string ValorCota = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblValorBruto", tagPos);
                    string ValorBruto = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblIR", tagPos);
                    string IR = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblIOF", tagPos);
                    string IOF = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblValorLiquido", tagPos);
                    string ValorLiquido = GetNextBetweenTagValue(allData, tagPos);

                    tagPos = allData.IndexOf("lblNrNota", tagPos);
                    string NrNota = GetNextBetweenTagValue(allData, tagPos).Trim();

                    int TransType = GetTransactionType(Movimentacao);

                    if (!TransactionExists(NrNota, QtdQuotas, TransType))
                    {
                        string finalString = DataMovimentacao.ToString("yyyy-MM-dd");
                        int ClientID = getClientID(lblCliente, Movimentacao);

                        if (ClientID != -1)
                        {
                            string AdminRef = "";
                            string AdminRef_Sub = "";

                            if (TransType == 30)
                            {
                                AdminRef = NrNota;
                            }
                            else
                            {
                                AdminRef_Sub = NrNota;
                            }

                            string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb760_Subscriptions_Mellon]([Id_Contact],[Request_Date],[Trade_Date],[Settlement_Date],[Transaction_Type],[Transaction_NAV],[Id_Portfolio],[AdminRef],[AdminRef_Sub],[Quantity],[Fin_Amount],[IncomeTax]) " +
                                    "VALUES(" + ClientID.ToString() + ", '" + DataReferencia.ToString("yyyy-MM-dd") + "', '" + DataCotizacao.ToString("yyyy-MM-dd") + "', '" + DataLiquidacaoFinanceira.ToString("yyyy-MM-dd") + "', " + TransType.ToString() + ", " + ValorCota.Replace(",", "") + ", " + Id_Portfolio + ", '" + AdminRef + "', '" + AdminRef_Sub + "', " + QtdQuotas.Replace(",", "") + ", " + ValorLiquido.Replace(",", "") + ", " + IR.Replace(",", "") + ")";


                            using (newNestConn curConn = new newNestConn())
                            {
                                curConn.ExecuteNonQuery(SQLExpression);
                            }
                            insertCounter++;
                        }
                    }
                }
            }
        
            MessageBox.Show("" + insertCounter + " transactions inserted.", "Import Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private int GetTransactionType(string TransDescription)
        {
            switch (TransDescription)
            {
                case "Aplicacao": return 30;
                case "Resgate Parcial": return 31;
                case "Resgate Total": return 32;
                case "Resgate de Transferencia": return 31;
                default:
                    return -1;
            }

        }

        private string GetNextBetweenTagValue(string HTMLString, int curPos)
        {
            int curPos2 = 0;

            if (curPos != -1)
            {
                curPos = HTMLString.IndexOf(">", curPos);
                curPos2 = HTMLString.IndexOf("<", curPos);
                return HTMLString.Substring(curPos + 1, curPos2 - curPos - 1);
            }
            else
            {
                return "";
            }
        }

        private int getClientID(string lblCliente, string Movimentacao)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string returnString = curConn.Execute_Query_String("SELECT * FROM NESTDB.dbo.Tb751_Contacts WHERE Contact_Name LIKE '%" + lblCliente.Trim() + "%'");
                if (returnString != "")
                {
                    return int.Parse(returnString);
                }
                else
                {
                    return CreateClientID(lblCliente.Trim(), Movimentacao);
                }
            }
        }

        private int CreateClientID(string ClientName, string Movimentacao)
        {
            frmFindClient curFindClient = new frmFindClient();
            curFindClient.lblClientName.Text = ClientName;
            curFindClient.ShowDialog();
            curFindClient.Top = this.Top;
            curFindClient.Left = this.Left;

            if (curFindClient.UserAnswer == "CREATE")
            {
                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery("INSERT INTO dbo.Tb751_Contacts(Contact_Name) VALUES ('"+ ClientName +"')");
                    string tempIdentity = curConn.Execute_Query_String("SELECT * FROM dbo.Tb751_Contacts WHERE Contact_Name = '" + ClientName + "'");
                    return int.Parse(tempIdentity);
                }
            }
            else if (curFindClient.UserAnswer == "USE SELECTED")
            {
                return curFindClient.SelectedClient;
            }
            else
            {
                return -1;
            }
        }

        private bool TransactionExists(string TransactionNumber, string Quantity, int TransType)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string returnString = "";

                if (TransType == 30)
                {
                    returnString = curConn.Execute_Query_String("SELECT * FROM NESTDB.dbo.Tb760_Subscriptions_Mellon WHERE AdminRef=" + TransactionNumber.ToString() + " AND Quantity=" + Quantity.Replace(",", ""));
                }
                else
                {
                    returnString = curConn.Execute_Query_String("SELECT * FROM NESTDB.dbo.Tb760_Subscriptions_Mellon WHERE AdminRef_Sub=" + TransactionNumber.ToString() + " AND Quantity=" + Quantity.Replace(",", ""));
                }
                if(returnString != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

   }
}