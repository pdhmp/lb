using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using NestDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Ionic.Zip;

namespace UpdateBDI
{
    public partial class frmUpdateBDI : Form
    {
        public List<BDI_Fields> BDIList;
        public Dictionary<string, int> SecurityList = new Dictionary<string, int>();
        public string Path = @"T:\Import\BDI\";
        newNestConn curConn = new newNestConn();

        public frmUpdateBDI() { InitializeComponent(); }
        private void cmdRead_Click(object sender, EventArgs e) { this.ReadFile(); }
        private void cmdRead_BMF_Click(object sender, System.EventArgs e){ this.ReadFileBMF(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            start.Start();
        }

        private void start_Tick(object sender, System.EventArgs e)
        {
            string SQL =
                     " SELECT A.IdSecurity ,B.NestTicker Ticker " +
                     " FROM dbo.Tb050_Preco_Acoes_Onshore A " +
                     " INNER JOIN dbo.Tb001_Securities_Variable B " +
                     " ON A.IdSecurity = B.IdSecurity " +
                     " where  A.SrType = 1 AND Source = 1 AND InsertDate > GETDATE() -15  " +
                     " GROUP BY A.IdSecurity, B.NestTicker ";

            DataTable curDt = curConn.Return_DataTable(SQL);

            foreach (DataRow curRow in curDt.Rows) { SecurityList.Add(curRow["Ticker"].ToString(), int.Parse(curRow["IdSecurity"].ToString())); }

            timer1_Tick(sender, e);

            ReadFile();

            timer1.Start();

            start.Stop();
        }
        
        private void ReadFile()
        {
            lblStatus.Text = "Reading file...";
            lblStatus.Update();

            foreach (string FileName in System.IO.Directory.GetFiles(Path))
            {
                if (!FileName.Contains("Imported") && !FileName.Contains(".zip"))
                {
                    string FileDate = FileName.Substring(FileName.LastIndexOf("\\") + 1, 4);

                    if (!System.IO.File.Exists(FileName))
                    {
                        lblStatus.Text = "Ultima checagem : " + DateTime.Now.ToString("HH:mm:ss ttt");
                        lblStatus.Update();

                        lblRead.Text = "Nenhum arquivo a ser processado.";
                        lblRead.Update();

                        progressBar1.Maximum = 0;
                        progressBar1.Update();

                        continue;
                    }


                    // Pega total de linhas do arquivo
                    progressBar1.Maximum = System.IO.File.ReadAllLines(FileName).Length;
                    BDIList = new List<BDI_Fields>();
                    // ------------------------------------------------------------------
                    FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);
                    BDIList.Clear();

                    string tempLine = "";

                    DateTime DataPregao = new DateTime();
                    //DateTime DataPregaoArq = new DateTime();

                    while ((tempLine = sr.ReadLine()) != null)
                    {
                        BDI_Fields curFields = new BDI_Fields();

                        curFields.TipoRegistro = tempLine.Substring(0, 2);

                        switch (curFields.TipoRegistro)
                        {
                            case "00":
                                AtualizaBarra(" Header : " + tempLine);

                                DataPregao = DateTime.ParseExact(tempLine.Substring(30, 8),
                                                                         "yyyyMMdd",
                                                                         System.Globalization.CultureInfo.InvariantCulture,
                                                                         System.Globalization.DateTimeStyles.None);
                                //DataPregao = new DateTime(2050, 10, 10);
                                break;

                            case "01":
                                curFields.BDI_01_ResumoDiarioIndices(tempLine);
                                curFields.IdSecurity = GetIdSecurity(curFields.Ticker).ToString();
                                if (curFields.Ticker.Contains("IBOVESPA")) curFields.IdSecurity = "1073";

                                if (curFields.IdSecurity != "0")
                                {
                                    AtualizaBarra(" Resumo Diario Indices : " + tempLine);
                                    BDIList.Add(curFields);
                                }

                                break;

                            case "02":
                                curFields.BDI_02_ResumoDiarioNegociacoesPapel(tempLine);
                                curFields.IdSecurity = GetIdSecurity(curFields.Ticker).ToString();

                                if (curFields.IdSecurity != "0")
                                {
                                    AtualizaBarra(" Resumo Diario Negociacoes Papel : " + curFields.Ticker);
                                    BDIList.Add(curFields);
                                }
                                break;
                        }
                    }

                    sr.Close();
                    fs.Close();

                    foreach (BDI_Fields curBDI in BDIList)
                    {
                        AtualizaBarra("Inserindo Papel: " + curBDI.Ticker);
                        string InsertString = "";

                        InsertString += curBDI.Insert(1, DataPregao); // LAST
                        InsertString += curBDI.Insert(3, DataPregao); // LOW
                        InsertString += curBDI.Insert(4, DataPregao); // HIGH
                        InsertString += curBDI.Insert(7, DataPregao); // AVERAGE
                        InsertString += curBDI.Insert(8, DataPregao); // OPEN
                        InsertString += curBDI.Insert(9, DataPregao); // BID
                        InsertString += curBDI.Insert(10, DataPregao); // ASK
                        InsertString += curBDI.Insert(11, DataPregao); // SHARES_TRADED
                        curConn.ExecuteNonQuery(InsertString);
                    }

                    string NewFileName = @"T:\Import\BDI\Imported_" + DataPregao.ToString("yyyyMMdd") + "_" + FileDate;

                    File.Move(FileName, NewFileName);
                    File.Delete(FileName);


                    // Atualiza Displays
                    // ------------------
                    lblStatus.Text = "Ultima Atualização : " + DateTime.Now.ToString("HH:mm:ss ttt");
                    lblStatus.Update();

                    lblRead.Text = "";
                    lblRead.Update();

                    progressBar1.Maximum = 0;
                    progressBar1.Update();
                    // --------------------------------
                }
                else
                {
                    lblStatus.Text = "Ultima checagem : " + DateTime.Now.ToString("HH:mm:ss ttt");
                    lblStatus.Update();

                    lblRead.Text = "Nenhum arquivo a ser processado.";
                    lblRead.Update();
                }
            }
        }

        private void ReadFileBMF()
        {
            lblStatus.Text = "Reading file BMF...";
            lblStatus.Update();

            foreach (string FileName in System.IO.Directory.GetFiles(Path))
            {
                if (!FileName.Contains("Imported") && !FileName.Contains(".zip"))
                {
                    string FileDate = FileName.Substring(FileName.LastIndexOf("\\") + 1, 4);

                    if (!System.IO.File.Exists(FileName))
                    {
                        lblStatus.Text = "Ultima checagem : " + DateTime.Now.ToString("HH:mm:ss ttt");
                        lblStatus.Update();

                        lblRead.Text = "Nenhum arquivo a ser processado.";
                        lblRead.Update();

                        progressBar1.Maximum = 0;
                        progressBar1.Update();

                        continue;
                    }


                    // Pega total de linhas do arquivo
                    progressBar1.Maximum = System.IO.File.ReadAllLines(FileName).Length;
                    BDIList = new List<BDI_Fields>();
                    // ------------------------------------------------------------------
                    FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    string tempLine = "";

                    DateTime DataPregao = new DateTime();
                    //DateTime DataPregaoArq = new DateTime();

                    while ((tempLine = sr.ReadLine()) != null)
                    {
                        BDI_Fields curFields = new BDI_Fields();

                        curFields.TipoRegistro = tempLine.Substring(0, 2);

                        switch (curFields.TipoRegistro)
                        {
                            case "00":
                                AtualizaBarra(" Header : " + tempLine);

                                DataPregao = DateTime.ParseExact(tempLine.Substring(30, 8),
                                                                         "yyyyMMdd",
                                                                         System.Globalization.CultureInfo.InvariantCulture,
                                                                         System.Globalization.DateTimeStyles.None);
                                //DataPregao = new DateTime(2050, 10, 10);
                                break;

                            case "01":
                                AtualizaBarra(" Resumo Diario Indices : " + tempLine);
                                break;

                            case "02":
                                curFields.BDI_02_ResumoDiarioNegociacoesPapel(tempLine);
                                curFields.IdSecurity = GetIdSecurity(curFields.Ticker).ToString();

                                if (curFields.IdSecurity != "0")
                                {
                                    AtualizaBarra(" Resumo Diario Negociacoes Papel : " + curFields.Ticker);
                                    BDIList.Add(curFields);
                                }
                                break;
                        }
                    }

                    sr.Close();
                    fs.Close();

                    foreach (BDI_Fields curBDI in BDIList)
                    {
                        AtualizaBarra("Inserindo Papel: " + curBDI.Ticker);
                        string InsertString = "";

                        InsertString += curBDI.Insert(1, DataPregao); // LAST
                        InsertString += curBDI.Insert(3, DataPregao); // LOW
                        InsertString += curBDI.Insert(4, DataPregao); // HIGH
                        InsertString += curBDI.Insert(7, DataPregao); // AVERAGE
                        InsertString += curBDI.Insert(8, DataPregao); // OPEN
                        InsertString += curBDI.Insert(9, DataPregao); // BID
                        InsertString += curBDI.Insert(10, DataPregao); // ASK
                        curConn.ExecuteNonQuery(InsertString);
                    }

                    string NewFileName = @"T:\Import\BDI\Imported_" + DataPregao.ToString("yyyyMMdd") + "_" + FileDate;

                    File.Move(FileName, NewFileName);
                    File.Delete(FileName);

                    // Atualiza Displays
                    // ------------------
                    lblStatus.Text = "Ultima Atualização : " + DateTime.Now.ToString("HH:mm:ss ttt");
                    lblStatus.Update();

                    lblRead.Text = "";
                    lblRead.Update();

                    progressBar1.Maximum = 0;
                    progressBar1.Update();
                    // --------------------------------
                }
                else
                {
                    lblStatus.Text = "Ultima checagem : " + DateTime.Now.ToString("HH:mm:ss ttt");
                    lblStatus.Update();

                    lblRead.Text = "Nenhum arquivo a ser processado.";
                    lblRead.Update();
                }
            }
        }

        public int GetIdSecurity(string Ticker)
        {
            if (SecurityList.ContainsKey(Ticker))
            {
                return SecurityList[Ticker];
            }
            else
            {
                int IdSecurity = curConn.Return_Int("SELECT IdSecurity FROM Tb001_Securities_Variable WHERE NestTicker = '" + Ticker + "';");

                if (IdSecurity != 0)
                {
                    SecurityList.Add(Ticker, IdSecurity);
                    return SecurityList[Ticker];
                }
                else
                    return 0;
            }
        }

        public void AtualizaBarra(string sTexto)
        {
            lblRead.Text = sTexto;
            lblRead.Update();

            progressBar1.Value++;
            progressBar1.Update();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime LastPregao = curConn.Return_DateTime("SELECT NESTDB.dbo.[FCN_NDATEADD]('du', -1, GETDATE(), 2, 1) ");
            LastPregao = Convert.ToDateTime("2014-12-31");
            bool Downloaded = false;
            foreach (string FileName in System.IO.Directory.GetFiles(Path))
            {
                if (FileName.Contains(LastPregao.ToString("yyyyMMdd"))) { Downloaded = true; }
            }

            if (!Downloaded)
            {
                DownloadFile(LastPregao);
            }

            //cmdRead_Click(sender, e);
            ReadFile();
        }

        public void DownloadFile(DateTime LastPregao)
        {
            string ZipPath = Path + "bdi" + LastPregao.ToString("MMdd") + ".zip";
            string Url = "http://www.bmfbovespa.com.br/fechamento-pregao/bdi/bdi" + LastPregao.ToString("MMdd") + ".zip";

            WebClient webClient = new WebClient();
            webClient.DownloadFile(Url, ZipPath);
            webClient.Dispose();

            using (ZipFile zip1 = ZipFile.Read(ZipPath))
            {
                // here, we extract every entry, but we could extract conditionally
                // based on entry name, size, date, checkbox status, etc.  
                foreach (ZipEntry e in zip1)
                {
                    e.Extract(Path, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }

            }
}
