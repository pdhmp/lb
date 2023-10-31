using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmImportFiles : Form
    {
        SortedDictionary<int, string[]> SortDic = new SortedDictionary<int, string[]>();
        SortedDictionary<int, string[]> SortDicFut = new SortedDictionary<int, string[]>();
        SortedDictionary<int, string[]> SortDicFutResume;
        SortedDictionary<int, string[]> SortDicUS;

        public frmImportFiles()
        {
            InitializeComponent();
        }


        private void lstStatus_DragEnter(object sender, DragEventArgs e)
        {
            // for this program, we allow a file to be dropped from Explorer
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent("FileGroupDescriptor")) // or this tells us if it is an Outlook attachment drop
            {
                e.Effect = DragDropEffects.Copy;
            }
            else // or none of the above
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lstStatus_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = null;
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                {
                    fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                    FileInfo File = new FileInfo(fileNames[0].ToString());
                    // handle each file passed as needed
                    foreach (string fileName in fileNames)
                    {
                        lstFiles.Items.Add(File.Name);
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
                    {
                        fileName.Append(Convert.ToChar(fileGroupDescriptor[i]));
                    }
                    theStream.Close();

                    string extension = Path.GetExtension(fileName.ToString());
                    if (extension != ".htm" && extension != ".HTM" && extension != ".html" && extension != ".HTML" && extension != ".csv")
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
                    {
                        Trace.WriteLine("File was not created!");
                    }
                }

            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error in DragDrop function: " + ex.Message);

                // don't use MessageBox here - Outlook or Explorer is waiting !
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            GetFiles();
            Cursor.Current = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        bool CheckInsertedFile(string FileName)
        {
            string StringSQL = "Select 1 from NESTIMPORT.dbo.Tb_ImportedFiles (nolock) WHERE FileName='" + FileName + "'";
            int Retorno = 0;

            using (newNestConn curConn = new newNestConn())
            {
                Retorno = Convert.ToInt32(curConn.ExecuteNonQuery(StringSQL, 1));
            }

            return Convert.ToBoolean(Retorno);
        }

        void GetFiles()
        {
            string FileName = "";
            bool flagUS = false;
            bool flagFut = false;

            for (int i = 0; i < lstFiles.Items.Count; i++)
            {
                SortDic = new SortedDictionary<int, string[]>();

                FileName = lstFiles.Items[i].ToString();

                string url = "T:\\Import\\Confirmacoes\\" + FileName;
                string result = null;

                WebClient client = new WebClient();
                result = client.DownloadString(url);

                int checkFileBov = result.IndexOf("BOVESPA<");
                int checkFileBmf = result.IndexOf("BM&amp;F</");
                int checkFileBmfFut = result.IndexOf("* NegÛcios gerados automaticamente pelo sistema");
                int checkFileUSEquity = result.IndexOf("Time,Branch,Seq,");
                int checkFileUSOption = result.IndexOf("Trade Date,Tradebook");

                if (checkFileBmfFut != -1)
                {
                    ReadFilesBmfFut(FileName);
                    checkFileBmf = -1;
                    flagFut = true;
                }

                if (checkFileBmf == -1)
                {
                    checkFileBmf = result.IndexOf("BMF</");
                }

                if (checkFileBmf != -1)
                {
                    ReadFilesBmf(FileName);
                }

                if (checkFileBov != -1)
                {
                    ReadFilesBovespa(FileName);
                }

                if (checkFileUSEquity != -1)
                {
                    ReadFilesUSEquity(FileName);
                    flagUS = true;
                }

                if (checkFileUSOption != -1)
                {
                    ReadFilesUSOption(FileName);
                    flagUS = true;
                }

            }

            if (!flagUS)
            {
                string StringSQL = "SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_InsertData ; ";
                if (flagFut)
                {
                    StringSQL += " SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_InsertDataBmfFut ; ";
                }
                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(StringSQL);
                }
            }
            else
            {
                string StringSQL = "SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_InsertDataBmfUsEquity ; ";

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(StringSQL);
                }
            }

            MessageBox.Show("Files Imported!");
            lstFiles.Items.Clear();
        }

        void InsertFoward(string Corretora, DateTime TradeDate)
        {

        }

        void ReadFilesBovespa(string FileName)
        {
            string url = "T:\\Import\\Confirmacoes\\" + FileName;
            string result = null;
            string StringSQL;
            try
            {
                WebClient client = new WebClient();
                result = client.DownloadString(url);

                int checkFile = result.IndexOf("<XML ID=\"xmlDetalhe\">");
                if (checkFile == -1)
                {
                    MessageBox.Show("The File is not in the correct format!");
                    return;
                }

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

                int CheckImported = 0;

                if (Fundo == "NEST INVESTIMENTOS LTDA")
                {
                    MessageBox.Show("This file is from Broker account. Not Imported!");
                    return;
                }

                StringSQL = "Select IdImportedFile FROM NESTIMPORT.dbo.Tb_ImportedFiles (nolock) WHERE FileName='" + FileName + "' AND FileDate='" + DataTrade.ToString("yyyyMMdd") + "'";

                using (newNestConn curConn = new newNestConn())
                {
                    CheckImported = curConn.Return_Int(StringSQL);
                }
                if (CheckImported != 0)
                {
                    if(DataTrade==DateTime.Now.Date)
                        MessageBox.Show("This file was already imported!");
                    else
                        MessageBox.Show("This file was already imported. Please check the date.\r\n\r\nThe DATE for this file is NOT TODAY!");
                    return;
                }

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

                    if (NewStr != "")
                    {
                        Dt[0] = ReturnString(NewStr, "<DS_TIT1>", "</DS_TIT1>").Trim();
                        Dt[1] = ReturnString(NewStr, "<DS_TIT2>", "</DS_TIT2>").Trim();
                        Dt[2] = ReturnString(NewStr, "<DS_TIT3>", "</DS_TIT3>").Trim();
                        Dt[3] = ReturnString(NewStr, "<DS_TIT4>", "</DS_TIT4>").Trim();
                        Dt[4] = ReturnString(NewStr, "<DS_TIT5>", "</DS_TIT5>").Trim();
                        Dt[5] = ReturnString(NewStr, "<DS_TIT6>", "</DS_TIT6>").Trim();


                        if (Dt[1] != "")
                        {
                            if (Dt[1].Substring(Dt[1].Length - 1) == "S")
                            {
                                string sAuxi = Dt[1];
                                Dt[1] = sAuxi.Replace(sAuxi.Substring(sAuxi.Length - 1), "T");
                                Console.Write(Dt[1]);
                            }
                        }

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

                InsertDataBovespa(Corretora, DataTrade, Fundo, FileName);

                InsertFile(FileName, DataTrade);

                InsertFoward(Corretora, DataTrade);

                return;
            }

            catch (Exception ex)
            {
                // handle error
                MessageBox.Show(ex.Message);
                return;
            }
        }

        void ReadFilesBmfFut(string FileName)
        {
            SortDicFutResume = new SortedDictionary<int, string[]>();
            SortDicFut = new SortedDictionary<int, string[]>();

            string url = "T:\\Import\\Confirmacoes\\" + FileName;
            string result = null;
            string StringSQL;
            try
            {
                WebClient client = new WebClient();
                result = client.DownloadString(url);

                int checkFile = result.IndexOf("<XML ID=\"xmlNotaDetalhe\">");
                if (checkFile == -1)
                {
                    MessageBox.Show("The File is not in the correct format!");
                    return;
                }

                int posini = result.IndexOf("<XML ID=\"xmlNotaCabec\">");
                int posfim = result.IndexOf("</XML>");

                int DateType = 0;
                string StrCabec = result.Substring(posini, posfim - posini);

                if (result.IndexOf("Date of Trade") != -1 && result.IndexOf("Trade Executions") != -1)
                {
                    DateType = 1;
                }

                string Corretora = ReturnString(StrCabec, "<sNomeEmp>", "</sNomeEmp>");
                DateTime DataTrade = ReturnDatetime(ReturnString(StrCabec, "<sDataMov>", "</sDataMov>"), DateType);
                string Fundo = ReturnString(StrCabec, "<sNmCliente>", "</sNmCliente>");
                string IdCorretora = ReturnString(StrCabec, "<sCdEmprBolsa>", "</sCdEmprBolsa>");
                IdCorretora = IdCorretora.Substring(0, IdCorretora.IndexOf("-"));


                int CheckImported = 0;
                StringSQL = "Select IdImportedFile FROM NESTIMPORT.dbo.Tb_ImportedFiles (nolock) WHERE FileName='" + FileName + "' AND FileDate='" + DataTrade.ToString("yyyyMMdd") + "'";

                using (newNestConn curConn = new newNestConn())
                {
                    CheckImported = curConn.Return_Int(StringSQL);
                }

                //CheckImported = 0;
                if (CheckImported != 0)
                {
                    MessageBox.Show("This file has been imported!");
                    return;
                }


                posini = result.IndexOf("<XML ID=\"xmlNotaDetalhe\">");
                posfim = result.IndexOf("</XML>", posini);

                string StrDetalhe = result.Substring(posini, posfim - posini);

                string NewStr;
                int Counter = 0;

                posini = 0;

                for (int i = posini; i < StrDetalhe.Length; i++)
                {
                    string[] Dt = new string[9];

                    NewStr = ReturnNode(StrDetalhe, "<DETALHE>", "</DETALHE>", i, posfim);
                    //Console.WriteLine(NewStr);
                    //Console.WriteLine(i.ToString());
                    if (NewStr != "")
                    {
                        Dt[0] = ReturnString(NewStr, "<sCdNatope>", "</sCdNatope>").Trim(); //Side
                        Dt[1] = ReturnString(NewStr, "<sCdCommod>", "</sCdCommod>").Trim() + ReturnString(NewStr, "<sCdSerie>", "</sCdSerie>").Trim(); //
                        Dt[2] = ReturnString(NewStr, "<sDtVenc>", "</sDtVenc>").Trim(); //Vencto
                        Dt[3] = ReturnString(NewStr, "<sQtQtddet>", "</sQtQtddet>").Trim(); //QTD
                        Dt[4] = ReturnString(NewStr, "<sPrNegocio>", "</sPrNegocio>").Trim(); //Preco
                        Dt[5] = ReturnString(NewStr, "<sTpNegocio>", "</sTpNegocio>").Trim(); //Tipo Neg
                        Dt[6] = ReturnString(NewStr, "<sVlValope>", "</sVlValope>").Trim(); //Total
                        Dt[7] = ReturnString(NewStr, "<sInDebCre>", "</sInDebCre>").Trim(); //Deb Cred
                        Dt[8] = ReturnString(NewStr, "<sVlCorneg>", "</sVlCorneg>").Trim(); //Corretagem

                        // if (i > 40000) { MessageBox.Show("dd"); }

                        if (Dt[0] != "" || Dt[1] != "" || Dt[2] != "" || Dt[3] != "" || Dt[4] != "" || Dt[5] != "")
                        {
                            SortDicFut.Add(Counter, Dt);
                            Counter++;
                        }
                    }
                    i = StrDetalhe.IndexOf("</DETALHE>", i);
                    if (i <= posini)
                    {
                        break;
                    }
                }

                Counter = 0;

                posini = result.IndexOf("<XML ID=\"xmlNotaResumo\">");
                posfim = result.IndexOf("</XML>", posini);
                string StrResume = result.Substring(posini, posfim - posini);


                for (int i = 0; i < StrResume.Length; i++)
                {
                    string[] Dt = new string[30];

                    NewStr = ReturnNode(StrResume, "<DETALHE>", "</DETALHE>", i, posfim);
                    Console.WriteLine(NewStr);
                    Console.WriteLine(i.ToString());
                    if (NewStr != "")
                    {
                        Dt[0] = ReturnString(NewStr, "<sVlDisven>", "</sVlDisven>").Trim(); //Disp Venda
                        Dt[1] = ReturnString(NewStr, "<sVlDiscom>", "</sVlDiscom>").Trim(); //Disp Compra
                        Dt[2] = ReturnString(NewStr, "<sVlOpcven>", "</sVlOpcven>").Trim(); //Venda Opcoes
                        Dt[3] = ReturnString(NewStr, "<sVlOpccom>", "</sVlOpccom>").Trim(); //Compra Opcoes
                        Dt[4] = ReturnString(NewStr, "<sInVlTotnegDC>", "</sInVlTotnegDC>").Trim(); //Tipo Neg
                        Dt[5] = ReturnString(NewStr, "<sVlTotneg>", "</sVlTotneg>").Trim(); //Total Neg
                        Dt[6] = ReturnString(NewStr, "<sVlIr>", "</sVlIr>").Trim(); //IR
                        Dt[7] = ReturnString(NewStr, "<sVlIrrfDt>", "</sVlIrrfDt>").Trim(); //IR Day Trade
                        Dt[8] = ReturnString(NewStr, "<sVlCortot>", "</sVlCortot>").Trim(); //Taxas Ops
                        Dt[9] = ReturnString(NewStr, "<sVlTaxtot>", "</sVlTaxtot>").Trim(); //Tx RegistroBMF
                        Dt[10] = ReturnString(NewStr, "<sVlEmofdotot>", "</sVlEmofdotot>").Trim(); //Tx BMF
                        Dt[11] = ReturnString(NewStr, "<sVlIrrfCorret>", "</sVlIrrfCorret>").Trim(); //IR Corret
                        Dt[12] = ReturnString(NewStr, "<sVlCortotBro>", "</sVlCortotBro>").Trim(); //Tx ops Intermed
                        Dt[13] = ReturnString(NewStr, "<sVlAjusteNm>", "</sVlAjusteNm>").Trim(); //Ajuste Posicao
                        Dt[14] = ReturnString(NewStr, "<sInVlAjusteNmDC>", "</sInVlAjusteNmDC>").Trim(); //Ajuste Posicao Side
                        Dt[15] = ReturnString(NewStr, "<sVlAjusteDt>", "</sVlAjusteDt>").Trim(); //Ajuste Day Trade
                        Dt[16] = ReturnString(NewStr, "<sInVlAjusteDtDC>", "</sInVlAjusteDtDC>").Trim(); //Ajuste Day Trade Side
                        Dt[17] = ReturnString(NewStr, "<sVlDesptot>", "</sVlDesptot>").Trim(); //Total Desp
                        Dt[18] = ReturnString(NewStr, "<sInVlDesptotDC>", "</sInVlDesptotDC>").Trim(); //Total Desp Side
                        Dt[19] = ReturnString(NewStr, "<sVlPisCofins>", "</sVlPisCofins>").Trim(); //outros custos
                        Dt[20] = ReturnString(NewStr, "<sVlContaInv>", "</sVlContaInv>").Trim(); //Tot invest
                        Dt[21] = ReturnString(NewStr, "<sInVlContaInvDC>", "</sInVlContaInvDC>").Trim(); //Tot invest Side
                        Dt[22] = ReturnString(NewStr, "<sVlContaNor>", "</sVlContaNor>").Trim(); //Tot cot normal
                        Dt[23] = ReturnString(NewStr, "<sInVlContaNorDC>", "</sInVlContaNorDC>").Trim(); //Tot cot normal Side
                        Dt[24] = ReturnString(NewStr, "<sVlIsenImf>", "</sVlIsenImf>").Trim(); //Tot Liquido
                        Dt[25] = ReturnString(NewStr, "<sInVlIsenImfDC>", "</sInVlIsenImfDC>").Trim(); //Tot Liquido Side
                        Dt[26] = ReturnString(NewStr, "<sVlLiqnot>", "</sVlLiqnot>").Trim(); //Vl Liq Nota
                        Dt[27] = ReturnString(NewStr, "<sInVlLiqnotDC>", "</sInVlLiqnotDC>").Trim(); //Vl Liq Nota Side
                        Dt[28] = ReturnString(NewStr, "<sIss>", "</sIss>").Trim(); //ISS
                        Dt[29] = ReturnString(NewStr, "<sValAcreCort>", "</sValAcreCort>").Trim(); //outrod

                        // if (i > 40000) { MessageBox.Show("dd"); }

                        if (Dt[0] != "" || Dt[1] != "" || Dt[2] != "" || Dt[3] != "" || Dt[4] != "" || Dt[5] != "")
                        {
                            SortDicFutResume.Add(Counter, Dt);
                            Counter++;
                        }
                    }
                    i = StrDetalhe.IndexOf("</DETALHE>", i);
                    if (i <= posini)
                    {
                        break;
                    }
                }

                InsertDataBmfFut(Corretora, IdCorretora, DataTrade, Fundo, FileName);
                InsertDataBmfFutResume(Corretora, DataTrade, Fundo, FileName);

                InsertFile(FileName, DataTrade);


            }
            catch
            {

            }

        }

        void ReadFilesBmf(string FileName)
        {
            string url = "T:\\Import\\Confirmacoes\\" + FileName;
            string result = null;
            string StringSQL;
            try
            {
                WebClient client = new WebClient();
                result = client.DownloadString(url);

                int checkFile = result.IndexOf("<XML ID=\"xmlDetalhe\">");
                if (checkFile == -1)
                {
                    MessageBox.Show("The File is not in the correct format!");
                    return;
                }

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

                int CheckImported = 0;
                StringSQL = "Select IdImportedFile FROM NESTIMPORT.dbo.Tb_ImportedFiles (nolock) WHERE FileName='" + FileName + "' AND FileDate='" + DataTrade.ToString("yyyyMMdd") + "'";

                using (newNestConn curConn = new newNestConn())
                {
                    CheckImported = curConn.Return_Int(StringSQL);
                }
                if (CheckImported != 0)
                {
                    MessageBox.Show("This file has been imported!");
                    return;
                }

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
                        Dt[0] = ReturnString(NewStr, "<DS_TIT1>", "</DS_TIT1>").Trim(); //Side
                        Dt[1] = ReturnString(NewStr, "<DS_TIT2>", "</DS_TIT2>").Trim(); //
                        Dt[2] = ReturnString(NewStr, "<DS_TIT3>", "</DS_TIT3>").Trim(); //QTD
                        Dt[3] = ReturnString(NewStr, "<DS_TIT4>", "</DS_TIT4>").Trim(); //Preco
                        Dt[4] = ReturnString(NewStr, "<DS_TIT5>", "</DS_TIT5>").Trim(); //Total
                        Dt[5] = ReturnString(NewStr, "<DS_TIT6>", "</DS_TIT6>").Trim(); //Corretagem

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

                InsertDataBMF(Corretora, DataTrade, Fundo, FileName);

                InsertFile(FileName, DataTrade);

            }
            catch (Exception ex)
            {
                // handle error
                MessageBox.Show(ex.Message);
            }
        }

        void ReadFilesUSEquity(string fileName)
        {
            SortDicUS = new SortedDictionary<int, string[]>();
            string url = "T:\\Import\\Confirmacoes\\" + fileName;
            try
            {
                string SQLString = "SELECT COUNT (*) FROM [NESTIMPORT].[dbo].[Tb_NegociosUSEquity] WHERE FileName = '" + fileName + "'";

                using (newNestConn curConn = new newNestConn())
                {
                    if (curConn.Return_Int(SQLString) > 0)
                    {
                        DialogResult dialog = MessageBox.Show("Arquivo j· importado! \n\r Deseja reimporta-lo?", "Arquivo Existente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialog == DialogResult.Yes)
                        {
                            curConn.ExecuteNonQuery("DELETE FROM [NESTIMPORT].[dbo].[Tb_NegociosUSEquity] WHERE FileName = '" + fileName + "'");
                        }
                        else return;
                    }
                }

                StreamReader oStream = new StreamReader(url);
                string sLine = "";
                int iLineCounter = 0;
                char[] lTrimList = new char[] { ' ', '\"' };
                while ((sLine = oStream.ReadLine()) != null)
                {
                    if (!sLine.StartsWith("Time"))
                    {
                        string[] sParsed = sLine.Split(',');
                        for (int i = 0; i < sParsed.Length; i++)
                        {
                            if (sParsed[i] == null)
                                sParsed[i] = "";

                            sParsed[i] = sParsed[i].Trim(lTrimList);
                        }

                        SortDicUS.Add(iLineCounter++, sParsed);
                    }
                }

                //InsertFile(fileName, DateTime.Today);
                InsertDataUSEquity(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ReadFilesUSOption(string fileName)
        {
            SortDicUS = new SortedDictionary<int, string[]>();
            string url = "T:\\Import\\Confirmacoes\\" + fileName;
            try
            {
                StreamReader oStream = new StreamReader(url);
                string sLine = "";
                int iLineCounter = 0;
                char[] lTrimList = new char[] { ' ', '\"' };
                while ((sLine = oStream.ReadLine()) != null)
                {
                    if (!sLine.StartsWith("Trade"))
                    {
                        string[] sParsed = sLine.Split(',');
                        for (int i = 0; i < sParsed.Length; i++)
                        {
                            if (sParsed[i] == null)
                                sParsed[i] = "";

                            sParsed[i] = sParsed[i].Trim(lTrimList);
                        }

                        SortDicUS.Add(iLineCounter++, sParsed);
                    }
                }

                //InsertFile(fileName, DateTime.Today);
                InsertDataUSOption(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InsertFile(string FileName, DateTime FileDate)
        {
            string StringSQL;

            StringSQL = "INSERT INTO NESTIMPORT.dbo.Tb_ImportedFiles(FileName,FileDate,ImportDate)VALUES('" + FileName + "','" + FileDate.ToString("yyyyMMdd") + "',getdate())";

            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery(StringSQL);
            }
        }

        void InsertDataBovespa(string Corretora, DateTime DataPregao, string Fundo, string FileName)
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

            int Periodo = 0;

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

                if (sort.Value[0].ToString().IndexOf("TER") > 0)
                {
                    int IniPos = sort.Value[0].ToString().IndexOf("TER") + 3;
                    int EndPos = sort.Value[0].ToString().Substring(IniPos).IndexOf(" ");
                    int.TryParse(sort.Value[0].ToString().Substring(IniPos, EndPos), out Periodo);
                                    }

                if (TradeType != "" && TradeInstrument != "" && sort.Value[1].ToString() != "" && sort.Value[1].ToString() != "Soma" && sort.Value[1].ToString() != "SOMA" && (sort.Value[1].IndexOf("Total") == -1 && sort.Value[1].IndexOf("TOTAL") == -1))
                {
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

                    StringSQL = "SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_Insert_NegociosBRL '" + DataPregao.ToString("yyyyMMdd") + "','" +
                    Corretora + "','" + Fundo + "','" + sort.Value[1].ToString() + "'," + Quantidade +
                    "," + Preco + "," + Total + "," + Coretagem + ",'" + TradeType + "','" + TradeInstrument + "',0,'" + FileName + "','" + Periodo + "';";


                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(StringSQL, 1);
                    }
                }
                Console.WriteLine(sort.Value[2].ToString());

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
                if (sort.Value[2].ToString().IndexOf("TOTAL AJUSTE DIARIO") != -1 || sort.Value[2].ToString().IndexOf("TOTAL AJUSTE DI¡RIO") != -1 || sort.Value[2].ToString().IndexOf("TOTAL DAILY SETTLEMENT") != -1 || sort.Value[2].ToString().IndexOf("TOTAL AJUSTE DI√ÅRIO") != -1)
                {
                    TotalAjusteDiario = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("TOTAL NEG”CIOS") != -1 || sort.Value[2].ToString().IndexOf("TOTAL OF TRADES") != -1 || sort.Value[2].ToString().IndexOf("TOTAL NEG√ìCIOS") != -1)
                {
                    TotalNegocios = sort.Value[3].ToString();
                }
                if (sort.Value[2].ToString().IndexOf("CORRETAGEM") != -1 || sort.Value[2].ToString().IndexOf("FEE") == 0)
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

                if (sort.Value[2].ToString().IndexOf("IR S/ OPERA«’ES") != -1 || sort.Value[2].ToString().IndexOf("INCOME TAX") != -1 || sort.Value[2].ToString().IndexOf("IR S/ OPERA√á√ïES") != -1)
                {
                    IRSOperacoes = sort.Value[3].ToString();
                    if (IRSOperacoes == "") { IRSOperacoes = "0"; }
                }
                Console.WriteLine(sort.Value[2].ToString());
                if (sort.Value[2].ToString().IndexOf("TOTAL LIQUIDO") != -1 || sort.Value[2].ToString().IndexOf("TOTAL LIQUIDO ") != -1 || sort.Value[2].ToString().IndexOf("TOTAL LÕQUIDO ") != -1 || sort.Value[2].ToString().IndexOf("TOTAL LÕQUIDO") != -1 || sort.Value[2].ToString().IndexOf("TOTAL NET") != -1 || sort.Value[2].ToString().IndexOf("TOTAL L√çQUIDO") != -1)
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
                        TotalNegocios = TotalNegocios.ToString().Replace(".", "").Replace(",", ".");
                        Corretagem = Corretagem.ToString().Replace(".", "").Replace(",", ".");
                        TaxasCBLC = TaxasCBLC.ToString().Replace(".", "").Replace(",", ".");
                        TaxasBOV = TaxasBOV.ToString().Replace(".", "").Replace(",", ".");
                        TaxasOp = TaxasOp.ToString().Replace(".", "").Replace(",", ".");
                        OutrasDesp = OutrasDesp.ToString().Replace(".", "").Replace(",", ".");
                        IRSDayTrade = IRSDayTrade.ToString().Replace(".", "").Replace(",", ".");
                        IRSOperacoes = IRSOperacoes.ToString().Replace(".", "").Replace(",", ".");
                        TotalLiquido = TotalLiquido.ToString().Replace(".", "").Replace(",", ".");
                    }

                    if (TotalCompras != "0" && TotalVendas != "0" && TotalTermo != "0")
                    {
                        StringSQL = "SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_Insert_ResumoDiario '" + DataPregao.ToString("yyyyMMdd") + "','" +
                        Corretora + "','" + Fundo + "'," + TotalCompras + "," + TotalVendas + "," +
                        TotalTermo + "," + TotalAjusteDiario + "," + TotalNegocios +
                        "," + Corretagem + "," + TaxasCBLC + "," + TaxasBOV + "," +
                        TaxasOp + "," + OutrasDesp + "," + IRSDayTrade +
                        "," + IRSOperacoes + "," + TotalLiquido + ",'" + TradeInstrument + "','" + FileName + "';";

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

        void InsertDataBmfFut(string Corretora, string IdCorretora, DateTime DataPregao, string Fundo, string FileName)
        {
            string StringSQL = "";

            foreach (KeyValuePair<int, string[]> sort in SortDicFut)
            {
                if (sort.Value[0].ToString() != "" && sort.Value[1].ToString() != "" && sort.Value[2].ToString() != "" && sort.Value[3].ToString() != "" && sort.Value[4].ToString() != "")
                {
                    string Quantidade;
                    string Preco;
                    DateTime Vencto = Convert.ToDateTime(sort.Value[2].ToString());
                    string VLOPERACAO;
                    string TaxaOps;

                    Quantidade = sort.Value[3].ToString().Replace(".", "").Replace(",", ".").Replace("-", "");
                    Preco = sort.Value[4].ToString().Replace(".", "").Replace(",", ".");
                    VLOPERACAO = sort.Value[6].ToString().Replace(".", "").Replace(",", ".");
                    TaxaOps = sort.Value[8].ToString().Replace(".", "").Replace(",", ".");

                    StringSQL = "SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_Insert_NegociosBRLFUT '" + DataPregao.ToString("yyyyMMdd") + "','" +
                    Corretora + "','" + Fundo + "','" + sort.Value[0].ToString() + "','" + sort.Value[1].ToString() + "','" + Vencto.ToString("yyyyMMdd") + "'," + Quantidade +
                    "," + Preco + ",'" + sort.Value[5].ToString() + "'," + VLOPERACAO + ",'" + sort.Value[7].ToString() + "'," + TaxaOps + ",'" + FileName + "'," + IdCorretora + ";";

                    Console.WriteLine(StringSQL);

                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(StringSQL);
                    }
                }
            }
        }

        void InsertDataBMF(string Corretora, DateTime DataPregao, string Fundo, string FileName)
        {
            string TradeType = "";
            string TradeInstrument = "";
            string StringSQL = "";
            int formatType = 1;

            foreach (KeyValuePair<int, string[]> sort in SortDic)
            {
                if (sort.Value[0].ToString().IndexOf("COMPRA") != -1 || sort.Value[0].ToString().IndexOf("BOUGHT") != -1)
                {
                    TradeType = "C";
                    TradeInstrument = "Fut";
                }

                if (sort.Value[0].ToString().IndexOf("VENDA") != -1)
                {
                    TradeType = "V";
                    TradeInstrument = "Fut";
                }
                if (sort.Value[0].ToString().IndexOf("SOLD") != -1)
                {
                    TradeType = "V";
                    TradeInstrument = "Fut";
                }

                if (TradeType != "" && TradeInstrument != "" && sort.Value[1].ToString() != "" && sort.Value[2].ToString() != "Soma" && sort.Value[1].IndexOf("NET") == -1)
                {
                    // Console.WriteLine("TradeType:" + TradeType.ToString() + " - TradeInstrument:" + TradeInstrument.ToString() + " - Security:" + sort.Value[1].ToString() + " - Quantidade:" + sort.Value[2].ToString() + " - PreÁo:" + sort.Value[3].ToString() + " - Total:" + sort.Value[4].ToString() + " - Corretagem:" + sort.Value[5].ToString());

                    string Quantidade;
                    string Preco;

                    int indiceVrg = sort.Value[4].ToString().IndexOf(",");
                    int indicePnt = sort.Value[4].ToString().IndexOf(".");

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
                        Quantidade = sort.Value[3].ToString().Replace(",", "");
                        Preco = sort.Value[4].ToString().Replace(",", "");
                    }
                    else
                    {
                        Quantidade = sort.Value[3].ToString().Replace(".", "").Replace(",", ".").Replace("-", "");
                        Preco = sort.Value[4].ToString().Replace(".", "").Replace(",", ".");
                    }

                    StringSQL = "SET ARITHABORT ON; EXEC NESTIMPORT.dbo.Proc_Insert_NegociosBRL '" + DataPregao.ToString("yyyyMMdd") + "','" +
                    Corretora + "','" + Fundo + "','" + sort.Value[2].ToString() + "'," + Quantidade +
                    "," + Preco + "," + (Convert.ToDouble(sort.Value[3].ToString()) * Convert.ToDouble(sort.Value[4].ToString()) * -1).ToString().Replace(".", "").Replace(",", ".") + ",0,'" + TradeType + "','" + TradeInstrument + "',0,'" + FileName + "', 0;";

                    Console.WriteLine(StringSQL);

                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(StringSQL);
                    }
                }
            }
        }

        void InsertDataBmfFutResume(string Corretora, DateTime DataPregao, string Fundo, string FileName)
        {
            string StringSQL = "";

            foreach (KeyValuePair<int, string[]> sort in SortDicFutResume)
            {
                if (sort.Value[0].ToString() != "" && sort.Value[1].ToString() != "" && sort.Value[2].ToString() != "" && sort.Value[3].ToString() != "" && sort.Value[4].ToString() != "")
                {
                    string VendaDisponivel;
                    string CompraDisponivel;
                    string VendaOpcoes;
                    string CompraOpcoes;
                    string ValorNegocios;
                    string IRPF;
                    string IRPFDAyTRADE;
                    string TxOperacional;
                    string TxBMF;
                    string TxopsInter;
                    string AjustePos;
                    string AjusteDayTrade;
                    string TotalDespesas;
                    string OutrosCustos;
                    string TotContInvestimento;
                    string TotContNormal;
                    string TotLiquido;
                    string TotLiquidoNota;
                    string ISS;
                    string Outros;
                    string TxRegBMF;
                    string IRPFCorretagem;

                    VendaDisponivel = sort.Value[0].ToString().Replace(".", "").Replace(",", ".");
                    CompraDisponivel = sort.Value[1].ToString().Replace(".", "").Replace(",", ".");
                    VendaOpcoes = sort.Value[2].ToString().Replace(".", "").Replace(",", ".");
                    CompraOpcoes = sort.Value[3].ToString().Replace(".", "").Replace(",", ".");
                    ValorNegocios = ReturnSide(sort.Value[4].ToString()) + sort.Value[5].ToString().Replace(".", "").Replace(",", ".");
                    IRPF = sort.Value[6].ToString().Replace(".", "").Replace(",", ".");
                    IRPFDAyTRADE = sort.Value[7].ToString().Replace(".", "").Replace(",", ".");

                    //TxOperacional = sort.Value[8].ToString().Replace(".", "").Replace(",", ".");
                    //Alterado em 16/02/2012 para corrigir uma falha na importaÁ„o dos arquivos de corretagem
                    //A partir desta data È realizada uma verificaÁ„o para garantir que as taxas de corretagem 
                    //e emolumentos s„o inseridas com valores negativos

                    TxOperacional = (double.Parse(sort.Value[8].Replace(".", ",")) > 0 ? double.Parse(sort.Value[8].Replace(".", ",")) * (-1) : double.Parse(sort.Value[8].Replace(".", ","))).ToString().Replace(".", "").Replace(",", ".");
                    TxRegBMF = (double.Parse(sort.Value[9].Replace(".", ",")) > 0 ? double.Parse(sort.Value[9].Replace(".", ",")) * (-1) : double.Parse(sort.Value[9].Replace(".", ","))).ToString().Replace(".", "").Replace(",", ".");
                    TxBMF = (double.Parse(sort.Value[10].Replace(".", ",")) > 0 ? double.Parse(sort.Value[10].Replace(".", ",")) * (-1) : double.Parse(sort.Value[10].Replace(".", ","))).ToString().Replace(".", "").Replace(",", ".");
                    IRPFCorretagem = sort.Value[11].ToString().Replace(".", "").Replace(",", ".");
                    TxopsInter = sort.Value[12].ToString().Replace(".", "").Replace(",", ".");
                    AjustePos = ReturnSide(sort.Value[14].ToString()) + sort.Value[13].ToString().Replace(".", "").Replace(",", ".");
                    AjusteDayTrade = ReturnSide(sort.Value[16].ToString()) + sort.Value[15].ToString().Replace(".", "").Replace(",", ".");
                    TotalDespesas = ReturnSide(sort.Value[18].ToString()) + sort.Value[17].ToString().Replace(".", "").Replace(",", ".");
                    OutrosCustos = "-" + sort.Value[19].ToString().Replace(".", "").Replace(",", ".");
                    TotContInvestimento = ReturnSide(sort.Value[21].ToString()) + sort.Value[20].ToString().Replace(".", "").Replace(",", ".");
                    TotContNormal = ReturnSide(sort.Value[23].ToString()) + sort.Value[22].ToString().Replace(".", "").Replace(",", ".");
                    TotLiquido = ReturnSide(sort.Value[25].ToString()) + sort.Value[24].ToString().Replace(".", "").Replace(",", ".");
                    TotLiquidoNota = ReturnSide(sort.Value[27].ToString()) + sort.Value[26].ToString().Replace(".", "").Replace(",", ".");
                    ISS = "-" + sort.Value[28].ToString().Replace(".", "").Replace(",", ".");
                    Outros = "-" + sort.Value[29].ToString().Replace(".", "").Replace(",", ".");


                    StringSQL = "INSERT INTO [NESTIMPORT].[dbo].[Tb_ResumoConfirmacaoDiarioBrlFut]" +
                                "SELECT '" + DataPregao.ToString("yyyyMMdd") + "','" + Corretora + "','" + Fundo + "'," + VendaDisponivel + "," + CompraDisponivel +
                                "," + VendaOpcoes + "," + CompraOpcoes + "," + ValorNegocios + "," + IRPF + "," + IRPFDAyTRADE + "," + TxOperacional +
                                "," + TxRegBMF + "," + TxBMF + "," + IRPFCorretagem + "," + TxopsInter + "," + AjustePos + "," + AjusteDayTrade + "," + TotalDespesas +
                                "," + OutrosCustos + "," + TotContInvestimento + "," + TotContNormal + "," + TotLiquido + "," + TotLiquidoNota + "," + ISS + "," + Outros + ",0,0,0" +
                                ",'" + FileName + "',0;";

                    Console.WriteLine(StringSQL);

                    using (newNestConn curConn = new newNestConn())
                    {
                        curConn.ExecuteNonQuery(StringSQL);
                    }
                }
            }
        }

        void InsertDataUSEquity(string fileName)
        {
            System.Globalization.CultureInfo oUSCultureLayout = new System.Globalization.CultureInfo("en-us");
            string StringSQL = "";
            foreach (string[] curEntry in SortDicUS.Values)
            {
                DateTime Time = DateTime.ParseExact(curEntry[0], "yyyyMMdd", oUSCultureLayout);
                string Branch = curEntry[1];
                int Seq = Int32.Parse(curEntry[2]);
                string Instr = curEntry[3];
                int ExQty = Int32.Parse(curEntry[4]);
                string Symbol = curEntry[5];
                double ExPrice = Double.Parse(curEntry[6], oUSCultureLayout);
                int LvsQty = Int32.Parse(curEntry[7]);
                string Customer = curEntry[8];
                string Trader = curEntry[9];
                DateTime OrderTime = DateTime.ParseExact(curEntry[0] + " " + curEntry[10], "yyyyMMdd HH.mm.ss", oUSCultureLayout);
                string Destination = curEntry[11];
                string Broker = curEntry[12];
                string Account = curEntry[13];
                string TradeCurrency = curEntry[14];
                string Settlement = curEntry[15];
                double Commission = Double.Parse(curEntry[16], oUSCultureLayout);
                DateTime SettlementDate = DateTime.ParseExact(curEntry[17], "yyyyMMdd", oUSCultureLayout);
                string FirmSymbol = curEntry[18];
                string FileName = fileName;
                string Corretora = "TradeBook";


                StringBuilder oBuilder = new StringBuilder();
                oBuilder.Append("SET ARITHABORT ON; EXEC [NESTIMPORT].[dbo].[Proc_Insert_NegociosUSEquity] ");
                oBuilder.Append("'" + Time.ToString("yyyyMMdd") + "'");
                oBuilder.Append(",'" + Branch + "'");
                oBuilder.Append("," + Seq.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + Instr + "'");
                oBuilder.Append("," + ExQty.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + Symbol + "'");
                oBuilder.Append("," + ExPrice.ToString(oUSCultureLayout));
                oBuilder.Append("," + LvsQty.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + Customer + "'");
                oBuilder.Append(",'" + Trader + "'");
                oBuilder.Append(",'" + OrderTime.ToString(oUSCultureLayout) + "'");
                oBuilder.Append(",'" + Destination + "'");
                oBuilder.Append(",'" + Broker + "'");
                oBuilder.Append(",'" + Account + "'");
                oBuilder.Append(",'" + TradeCurrency + "'");
                oBuilder.Append(",'" + Settlement + "'");
                oBuilder.Append("," + Commission.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + SettlementDate.ToString("yyyyMMdd") + "'");
                oBuilder.Append(",'" + FirmSymbol + "'");
                oBuilder.Append(",'" + FileName + "'");
                oBuilder.Append(",'" + Corretora + "'");

                StringSQL = oBuilder.Append(";").ToString();
                Console.WriteLine(StringSQL);

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(StringSQL);
                }
            }
        }

        void InsertDataUSOption(string fileName)
        {
            System.Globalization.CultureInfo oUSCultureLayout = new System.Globalization.CultureInfo("en-us");
            string StringSQL = "";
            foreach (string[] curEntry in SortDicUS.Values)
            {
                DateTime TradeDate = DateTime.ParseExact(curEntry[0], "MM/dd/yyyy", oUSCultureLayout);
                string TradebookFirmID = curEntry[1];
                string FirmName = curEntry[2];
                string TradebookUserID = curEntry[3];
                string UserName = curEntry[4];
                string OptionSymbol = curEntry[5];
                string OptionSymbolRoot = curEntry[6];
                string UndelyingTicker = curEntry[7];
                string CallPut = curEntry[8];
                string BuySell = curEntry[9];

                int TradeQty = Int32.Parse(curEntry[10]);
                double AveragePrice = Double.Parse(curEntry[11], oUSCultureLayout);
                DateTime Expiry = DateTime.ParseExact(curEntry[12], "MM/yyyy", oUSCultureLayout);
                double StrikePrice = Double.Parse(curEntry[13], oUSCultureLayout);

                string OpenClose = curEntry[14];
                string AccountOrigin = curEntry[15];
                string PrimeBroker = curEntry[16];
                string PrimeBrokerID = curEntry[17];
                string ClientAccountID = curEntry[18];

                double TradebookRate = Double.Parse(curEntry[19], oUSCultureLayout);
                double TradebookCommissions = Double.Parse(curEntry[20], oUSCultureLayout);
                double ExchangeFees = Double.Parse(curEntry[21], oUSCultureLayout);
                string PricingType = curEntry[22];
                double OCCFees = Double.Parse(curEntry[23], oUSCultureLayout);
                double RegulatoryFees = Double.Parse(curEntry[24], oUSCultureLayout);
                double ReserarchDollarsCommission = Double.Parse(curEntry[25], oUSCultureLayout);
                double TotalCommissionsFees = Double.Parse(curEntry[26], oUSCultureLayout);
                string Filename = fileName;


                StringBuilder oBuilder = new StringBuilder();
                oBuilder.Append("SET ARITHABORT ON; EXEC [NESTIMPORT].[dbo].[Proc_Insert_NegociosUSOption] ");

                oBuilder.Append("'" + TradeDate.ToString(oUSCultureLayout) + "'");
                oBuilder.Append(",'" + TradebookFirmID + "'");
                oBuilder.Append(",'" + FirmName + "'");
                oBuilder.Append(",'" + TradebookUserID + "'");
                oBuilder.Append(",'" + UserName + "'");
                oBuilder.Append(",'" + OptionSymbol + "'");
                oBuilder.Append(",'" + OptionSymbolRoot + "'");
                oBuilder.Append(",'" + UndelyingTicker + "'");
                oBuilder.Append(",'" + CallPut + "'");
                oBuilder.Append(",'" + BuySell + "'");

                oBuilder.Append("," + TradeQty.ToString(oUSCultureLayout));
                oBuilder.Append("," + AveragePrice.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + Expiry.ToString(oUSCultureLayout) + "'");
                oBuilder.Append("," + StrikePrice.ToString(oUSCultureLayout));

                oBuilder.Append(",'" + OpenClose + "'");
                oBuilder.Append(",'" + AccountOrigin + "'");
                oBuilder.Append(",'" + PrimeBroker + "'");
                oBuilder.Append(",'" + PrimeBrokerID + "'");
                oBuilder.Append(",'" + ClientAccountID + "'");

                oBuilder.Append("," + TradebookRate.ToString(oUSCultureLayout));
                oBuilder.Append("," + TradebookCommissions.ToString(oUSCultureLayout));
                oBuilder.Append("," + ExchangeFees.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + PricingType + "'");
                oBuilder.Append("," + OCCFees.ToString(oUSCultureLayout));
                oBuilder.Append("," + RegulatoryFees.ToString(oUSCultureLayout));
                oBuilder.Append("," + ReserarchDollarsCommission.ToString(oUSCultureLayout));
                oBuilder.Append("," + TotalCommissionsFees.ToString(oUSCultureLayout));
                oBuilder.Append(",'" + Filename + "'");

                StringSQL = oBuilder.Append(";").ToString();
                Console.WriteLine(StringSQL);

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(StringSQL);
                }
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

        string ReturnSide(string Side)
        {
            if (Side == "D")
            {
                return "-";
            }
            else
            {
                return " ";
            }
        }

        

    }
}