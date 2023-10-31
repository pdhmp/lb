using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using NestDLL;
using System.Diagnostics;

namespace SGN
{
    public partial class frmImportFiles : Form
    {
        SortedDictionary<int, string[]> SortDic = new SortedDictionary<int, string[]>();

        public frmImportFiles()
        {
            InitializeComponent();
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
        }

        private void frmStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void lstStatus_DragDrop(object sender, DragEventArgs e)
        {

            string[] fileNames = null;

            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                {
                    fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                    // handle each file passed as needed
                    foreach (string fileName in fileNames)
                    {
                        // do what you are going to do with each filename
                    }
                }
                else if (e.Data.GetDataPresent("FileGroupDescriptor"))
                {
                    //
                    // the first step here is to get the filename of the attachment and
                    //   build a full-path name so we can store it in the temporary folder
                    //

                    // set up to obtain the FileGroupDescriptor and extract the file name
                    Stream theStream = (Stream)e.Data.GetData("FileGroupDescriptor");
                    byte[] fileGroupDescriptor = new byte[512];
                    theStream.Read(fileGroupDescriptor, 0, 512);
                    // used to build the filename from the FileGroupDescriptor block
                    StringBuilder fileName = new StringBuilder("");
                    // this trick gets the filename of the passed attached file
                    for (int i = 76; fileGroupDescriptor[i] != 0; i++)
                    { fileName.Append(Convert.ToChar(fileGroupDescriptor[i])); }
                    theStream.Close();

                    string extension = Path.GetExtension(fileName.ToString());


                    if (extension != ".htm" && extension != ".HTM" && extension != ".html" && extension != ".HTML")
                    {
                        MessageBox.Show("This is not valid format file!");
                        return;
                    }

                    string path = "T:\\Import\\Confirmacoes\\";  // put the zip file into the temp directory
                    string theFile = path + fileName.ToString();  // create the full-path name

                    //
                    // Second step:  we have the file name.  Now we need to get the actual raw
                    // data for the attached file and copy it to disk so we work on it.
                    //

                    // get the actual raw file into memory
                    MemoryStream ms = (MemoryStream)e.Data.GetData("FileContents", true);
                    // allocate enough bytes to hold the raw data
                    byte[] fileBytes = new byte[ms.Length];
                    // set starting position at first byte and read in the raw data
                    ms.Position = 0;
                    ms.Read(fileBytes, 0, (int)ms.Length);
                    // create a file and save the raw zip file to it
                    FileStream fs = new FileStream(theFile, FileMode.Create);
                    fs.Write(fileBytes, 0, (int)fileBytes.Length);

                    fs.Close();	// close the file

                    FileInfo tempFile = new FileInfo(theFile);

                    // always good to make sure we actually created the file
                    if (tempFile.Exists == true)
                    {
                        // for now, just delete what we created
                        //tempFile.Delete();
                        lstFiles.Items.Add(tempFile.Name.ToString());

                    }
                    else
                    { Trace.WriteLine("File was not created!"); }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error in DragDrop function: " + ex.Message);

                // don't use MessageBox here - Outlook or Explorer is waiting !
            }
        }

        private void lstStatus_DragEnter(object sender, DragEventArgs e)
        {
            // for this program, we allow a file to be dropped from Explorer
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            { e.Effect = DragDropEffects.Copy; }
            //    or this tells us if it is an Outlook attachment drop
            else if (e.Data.GetDataPresent("FileGroupDescriptor"))
            {
                e.Effect = DragDropEffects.Copy;
            }
            //    or none of the above
            else
            { e.Effect = DragDropEffects.None; }


        }

        void GetFiles()
        {
            string FileName = "";

            for (int i = 0; i < lstFiles.Items.Count; i++)
            {
                SortDic = new SortedDictionary<int, string[]>();

                FileName = lstFiles.Items[i].ToString();

                ReadFiles(FileName);

            }
            MessageBox.Show("Imported Files!");
            lstFiles.Items.Clear();
        }

        void ReadFiles(string FileName)
        {
            string url = "T:\\Import\\Confirmacoes\\" + FileName;
            string result = null;

            try
            {
                WebClient client = new WebClient();
                result = client.DownloadString(url);

                int posini = result.IndexOf("<XML ID=\"xmlCabec\">");
                int posfim = result.IndexOf("</XML>");

                int DateType = 0;
                string StrCabec = result.Substring(posini, posfim - posini);

                if (result.IndexOf("Date of Trade") != -1 && result.IndexOf("Trade Executions") != -1)
                {
                    DateType = 1;
                }

                string Corretora = ReturnString(StrCabec, "<NM_EMPRESA>", "</NM_EMPRESA>");
                DateTime DataTrade = ReturnDatetime(ReturnString(StrCabec, "<DT_PREGAO>", "</DT_PREGAO>"), DateType);
                string Fundo = ReturnString(StrCabec, "<NM_CLIENTE>", "</NM_CLIENTE>");

                posini = result.IndexOf("<XML ID=\"xmlDetalhe\">");
                posfim = result.IndexOf("</XML>", posini);

                string StrDetalhe = result.Substring(posini, posfim - posini);


                string NewStr;
                int Counter = 0;

                posini = 0;

                for (int i = posini; i < StrDetalhe.Length; i++)
                {
                    string[] Dt = new string[6];

                    NewStr = ReturnNode(StrDetalhe, "<DETALHE>", "</DETALHE>", i, posfim);
                    Console.WriteLine(NewStr);
                    Console.WriteLine(i.ToString());
                    if (NewStr != "")
                    {
                        Dt[0] = ReturnString(NewStr, "<DS_TIT1>", "</DS_TIT1>").Trim();
                        Dt[1] = ReturnString(NewStr, "<DS_TIT2>", "</DS_TIT2>").Trim();
                        Dt[2] = ReturnString(NewStr, "<DS_TIT3>", "</DS_TIT3>").Trim();
                        Dt[3] = ReturnString(NewStr, "<DS_TIT4>", "</DS_TIT4>").Trim();
                        Dt[4] = ReturnString(NewStr, "<DS_TIT5>", "</DS_TIT5>").Trim();
                        Dt[5] = ReturnString(NewStr, "<DS_TIT6>", "</DS_TIT6>").Trim();

                        // if (i > 40000) { MessageBox.Show("dd"); }

                        if (Dt[0] != "" || Dt[1] != "" || Dt[2] != "" || Dt[3] != "" || Dt[4] != "" || Dt[5] != "")
                        {
                            SortDic.Add(Counter, Dt);
                            Counter++;
                        }
                    }
                    i = StrDetalhe.IndexOf("</DETALHE>", i);
                    if (i <= posini)
                    {
                        break;
                    }
                }
                InsertData(Corretora, DataTrade, Fundo);

            }

            catch (Exception ex)
            {
                // handle error
                MessageBox.Show(ex.Message);
            }


        }



        DateTime ReturnDatetime(string StringData, int DateType)
        {
            DateTime ConvertedDate;

            string[] datepart = StringData.Split('/');

            if (DateType == 1)
            {
                ConvertedDate = new DateTime(Convert.ToInt32(datepart[2]), Convert.ToInt32(datepart[0]), Convert.ToInt32(datepart[1]));

            }
            else
            {
                ConvertedDate = new DateTime(Convert.ToInt32(datepart[2]), Convert.ToInt32(datepart[1]), Convert.ToInt32(datepart[0]));
            }


            return ConvertedDate;

        }

        string ReturnString(string text, string initag, string endtag)
        {
            int TagSize = initag.Length;
            string ReturnString = "";
            int posini = text.IndexOf(initag);
            int posfim = -1;
            try
            {
                if (posini != -1)
                {
                    posfim = text.IndexOf(endtag);
                }

                if (posini != -1 && posfim != -1)
                {
                    ReturnString = text.Substring(posini + TagSize, posfim - posini - TagSize);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return ReturnString;
        }

        string ReturnNode(string text, string initag, string endtag, int StartPos, int EndPos)
        {
            int TagSize = initag.Length;
            string ReturnString = "";
            int posini = text.IndexOf(initag, StartPos);
            int posfim = -1;

            try
            {
                if (posini != -1)
                {
                    posfim = text.IndexOf(endtag, posini);
                }
                if (posini != -1 && posfim != -1)
                {
                    ReturnString = text.Substring(posini + TagSize, posfim - posini - TagSize);

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            return ReturnString;
        }

        void InsertData(string Corretora, DateTime DataPregao, string Fundo)
        {
            string TradeType = "";
            string TradeInstrument = "";
            string StringSQL = "";

            string TotalCompras = "0";
            string TotalVendas = "0";
            string TotalTermo = "0";
            string TotalAjusteDiario = "0";
            string TotalNegocios = "0";
            string Corretagem = "0";
            string TaxasCBLC = "0";
            string TaxasBOV = "0";
            string TaxasOp = "0";
            string OutrasDesp = "0";
            string IRSDayTrade = "0";
            string IRSOperacoes = "0";
            string TotalLiquido = "0";
            int formatType = 1;

            foreach (KeyValuePair<int, string[]> sort in SortDic)
            {
                if (sort.Value[0].ToString().IndexOf("COMPRA") != -1 || sort.Value[0].ToString().IndexOf("BOUGHT") != -1)
                {
                    TradeType = "C";

                    TradeInstrument = sort.Value[0].ToString().Substring(7, 3);

                }
                if (sort.Value[0].ToString().IndexOf("VENDA") != -1)
                {
                    TradeType = "V";
                    TradeInstrument = sort.Value[0].ToString().Substring(6, 3);
                }
                if (sort.Value[0].ToString().IndexOf("SOLD") != -1)
                {
                    TradeType = "V";
                    TradeInstrument = sort.Value[0].ToString().Substring(5, 3);
                }


                if (TradeType != "" && TradeInstrument != "" && sort.Value[1].ToString() != "" && sort.Value[1].ToString() != "Soma" && sort.Value[1].IndexOf("Total") == -1)
                {
                    // Console.WriteLine("TradeType:" + TradeType.ToString() + " - TradeInstrument:" + TradeInstrument.ToString() + " - Security:" + sort.Value[1].ToString() + " - Quantidade:" + sort.Value[2].ToString() + " - Preço:" + sort.Value[3].ToString() + " - Total:" + sort.Value[4].ToString() + " - Corretagem:" + sort.Value[5].ToString());

                    string Quantidade;
                    string Preco;
                    string Total;
                    string Coretagem;


                    int indiceVrg = sort.Value[3].ToString().IndexOf(",");
                    int indicePnt = sort.Value[3].ToString().IndexOf(".");


                    if (indiceVrg != -1 && indicePnt != -1)
                    {
                        if (indicePnt > indiceVrg)
                        {
                            formatType = 0;
                        }
                        else
                        {
                            formatType = 1;
                        }
                    }

                    else if (indiceVrg != -1 && indicePnt == -1)
                    {
                        formatType = 1;
                    }
                    else if (indiceVrg == -1 && indicePnt != -1)
                    {
                        formatType = 0;
                    }

                    if (formatType == 0)
                    {
                        Quantidade = sort.Value[2].ToString().Replace(",", "");
                        Preco = sort.Value[3].ToString().Replace(",", "");
                        Total = sort.Value[4].ToString().Replace(",", "");
                        Coretagem = sort.Value[5].ToString().Replace(",", "");
                    }
                    else
                    {
                        Quantidade = sort.Value[2].ToString().Replace(".", "").Replace(",", ".");
                        Preco = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        Total = sort.Value[4].ToString().Replace(".", "").Replace(",", ".");
                        Coretagem = sort.Value[5].ToString().Replace(".", "").Replace(",", ".");

                    }

                    StringSQL = "EXEC NESTIMPORT.dbo.Proc_Insert_NegociosBRL '" + DataPregao.ToString("yyyyMMdd") + "','" +
                    Corretora + "','" + Fundo + "','" + sort.Value[1].ToString() + "'," + Quantidade +
                    "," + Preco + "," + Total + "," + Coretagem + ",'" + TradeType + "','" + TradeInstrument + "';";


                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(StringSQL);
                    }
                }

                if (sort.Value[2].ToString().IndexOf("TOTAL COMPRAS") != -1 || sort.Value[2].ToString().IndexOf("TOTAL BOUGHT") != -1)
                {
                    TotalCompras = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("TOTAL VENDAS") != -1 || sort.Value[2].ToString().IndexOf("TOTAL SOLD") != -1)
                {
                    TotalVendas = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("TOTAL TERMO") != -1 || sort.Value[2].ToString().IndexOf("TOTAL OTHERS") != -1)
                {
                    TotalTermo = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("TOTAL AJUSTE DIÁRIO") != -1 || sort.Value[2].ToString().IndexOf("TOTAL DAILY SETTLEMENT") != -1)
                {
                    TotalAjusteDiario = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("TOTAL NEGÓCIOS") != -1 || sort.Value[2].ToString().IndexOf("TOTAL OF TRADES") != -1)
                {
                    TotalNegocios = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("CORRETAGEM") != -1 || sort.Value[2].ToString().IndexOf("FEE") != -1)
                {
                    Corretagem = sort.Value[3].ToString();
                }

                if (sort.Value[2].ToString().IndexOf("TAXAS OPER. CBLC") != -1 || sort.Value[2].ToString().IndexOf("EXCHANGE FEES CBLC") != -1)
                {
                    TaxasCBLC = sort.Value[3].ToString();
                }

                if (sort.Value[2].ToString().IndexOf("TAXAS OPER. BOVESPA") != -1 || sort.Value[2].ToString().IndexOf("EXCHANGE FEES BOVESPA") != -1)
                {
                    TaxasBOV = sort.Value[3].ToString();
                }

                if (sort.Value[2].ToString().IndexOf("TAXAS OPERACIONAIS") != -1 || sort.Value[2].ToString().IndexOf("EXCHANGE FEES") != -1)
                {
                    TaxasOp = sort.Value[3].ToString();
                }

                if (sort.Value[2].ToString().IndexOf("OUTRAS DESPESAS") != -1 || sort.Value[2].ToString().IndexOf("OTHER RATES") != -1)
                {
                    OutrasDesp = sort.Value[3].ToString();
                }

                if (sort.Value[2].ToString().IndexOf("IR S/ DAY-TRADE") != -1 || sort.Value[2].ToString().IndexOf("DAY-TRADE INCOME TAX") != -1)
                {
                    IRSDayTrade = sort.Value[3].ToString();
                }

                if (sort.Value[2].ToString().IndexOf("IR S/ OPERAÇÕES") != -1 || sort.Value[2].ToString().IndexOf("INCOME TAX") != -1)
                {
                    IRSOperacoes = sort.Value[3].ToString();
                    if (IRSOperacoes == "") { IRSOperacoes = "0"; }
                }
                Console.WriteLine(sort.Value[2].ToString());
                if (sort.Value[2].ToString().IndexOf("TOTAL LÍQUIDO ") != -1 || sort.Value[2].ToString().IndexOf("TOTAL NET") != -1)
                {
                    TotalLiquido = sort.Value[3].ToString();

                    if (formatType == 0)
                    {
                        TotalCompras = TotalCompras.ToString().Replace(",", "");
                        TotalVendas = TotalVendas.ToString().Replace(",", "");
                        TotalTermo = TotalTermo.ToString().Replace(",", "");
                        TotalAjusteDiario = TotalAjusteDiario.ToString().Replace(",", "");
                        TotalNegocios = TotalNegocios.ToString().Replace(",", "");
                        Corretagem = Corretagem.ToString().Replace(",", "");
                        TaxasCBLC = TaxasCBLC.ToString().Replace(",", "");
                        TaxasBOV = TaxasBOV.ToString().Replace(",", "");
                        TaxasOp = TaxasOp.ToString().Replace(",", "");
                        OutrasDesp = OutrasDesp.ToString().Replace(",", "");
                        IRSDayTrade = IRSDayTrade.ToString().Replace(",", "");
                        IRSOperacoes = IRSOperacoes.ToString().Replace(",", "");
                        TotalLiquido = TotalLiquido.ToString().Replace(",", "");

                    }
                    else
                    {
                        TotalCompras = TotalCompras.ToString().Replace(".", "").Replace(",", ".");
                        TotalVendas = TotalVendas.ToString().Replace(".", "").Replace(",", ".");
                        TotalTermo = TotalTermo.ToString().Replace(".", "").Replace(",", ".");
                        TotalAjusteDiario = TotalAjusteDiario.ToString().Replace(".", "").Replace(",", ".");
                        TotalNegocios = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        Corretagem = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        TaxasCBLC = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        TaxasBOV = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        TaxasOp = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        OutrasDesp = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        IRSDayTrade = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        IRSOperacoes = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                        TotalLiquido = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");

                    }


                    if (TotalCompras != "0" && TotalVendas != "0" && TotalTermo != "0")
                    {
                        StringSQL = "EXEC NESTIMPORT.dbo.Proc_Insert_ResumoDiario '" + DataPregao.ToString("yyyyMMdd") + "','" +
                        Corretora + "','" + Fundo + "'," + TotalCompras + "," + TotalVendas + "," +
                        TotalTermo + "," + TotalAjusteDiario + "," + TotalNegocios +
                        "," + Corretagem + "," + TaxasCBLC + "," + TaxasBOV + "," +
                        TaxasOp + "," + OutrasDesp + "," + IRSDayTrade +
                        "," + IRSOperacoes + "," + TotalLiquido + ",'" + TradeInstrument + "'";

                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery(StringSQL);
                        }
                        TotalCompras = "0";
                        TotalVendas = "0";
                        TotalTermo = "0";
                        TotalAjusteDiario = "0";
                        TotalNegocios = "0";
                        Corretagem = "0";
                        TaxasCBLC = "0";
                        TaxasBOV = "0";
                        TaxasOp = "0";
                        OutrasDesp = "0";
                        IRSDayTrade = "0";
                        IRSOperacoes = "0";
                        TotalLiquido = "0";

                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetFiles();
        }

    }
}