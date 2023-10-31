using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace LiveBook
{
    public partial class frmFatosRelevantes : LBForm
    {
        ContextMenu GridContextMenu;
        MenuItem mnuSetRead;

        string sTextoOld = "";
        string sTextoNew = "";
        string FilePath = @"C:\Livebook\";
        string FileName = "";
        bool bFirstTime = true;
        List<string> NewsList = new List<string>();
        List<string> UnreadList = new List<string>();

               public frmFatosRelevantes()
        {
            InitializeComponent();
        }

        public void DownloadByWebClient()
        {
            // ArquivosExibe.asp?site=C&protocolo='+ strProtocolo,'
            //string url = "http://siteempresas.bovespa.com.br/consbov/ExibeFatosRelevantesCvm.asp?site=C";

            DataTable result = new DataTable();
            result.Columns.Add("DataEnvio", typeof(string));
            result.Columns.Add("DataReferência", typeof(string));
            result.Columns.Add("ID", typeof(string));
            result.Columns.Add("Empresa", typeof(string));
            result.Columns.Add("Assunto", typeof(string));

            string StringText = string.Empty;
            string NewStringText = string.Empty;

            for (int PageCounter = 1; PageCounter < 6; PageCounter++)
            {
                using (WebClient client = new WebClient())
                {
                    string url = "http://siteempresas.bovespa.com.br/consbov/ExibeFatosRelevantesCvm.asp?empresa=&pagina=" + PageCounter.ToString() + "&SITE=C&strurl=images/fundo_.gif&BOTAO=images/volta_verde.gif";

                    client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
                    byte[] mybytes = client.DownloadData(url);

                    StringText += Encoding.UTF7.GetString(mybytes);
                    // StringText = Encoding.Default.GetString(mybytes); 

                    client.Dispose();

                    // ------------------------------------------------------------------------------------

                    int IniCounter = 0;
                    int EndCounter = 0;
                    int iCounter = 0;

                    IniCounter = StringText.IndexOf("<table border=");
                    EndCounter = StringText.IndexOf("</table>", IniCounter);
                    NewStringText = StringText.Substring(IniCounter, EndCounter - IniCounter + 8);
                    //MessageBox.Show(NewStringText);
                    NewStringText = NewStringText.Replace(@"Data de Envio", "DataEnvio").Replace(@"Data de Referência", "DataReferencia").Replace("Empresa / Assunto", "EmpresaAssunto");

                    //------------------------------------------------------------------------------------

                    // inicializa a tabela...


                    // procura as linhas da tabela...
                    System.Text.RegularExpressions.MatchCollection matches =
                    System.Text.RegularExpressions.Regex.Matches(NewStringText, @"<TR.*?>(.*?)</TR>", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);

                    // varre os resultados e adiciona na lista...
                    foreach (System.Text.RegularExpressions.Match match in matches)
                    {
                        if (match.Success && match.Groups.Count > 1)
                        {
                            string rowText = match.Groups[1].Value;

                            // pega os dados da linha...
                            System.Text.RegularExpressions.MatchCollection matchesTD =
                             System.Text.RegularExpressions.Regex.Matches(rowText, @"<TD.*?>(.*?)</TD>", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);

                            // pega o resultado da linha...
                            List<string> rowValues = new List<string>();
                            foreach (System.Text.RegularExpressions.Match matchTD in matchesTD)
                            {
                                if (match.Success && matchTD.Groups.Count > 1)
                                {
                                    rowValues.Add(matchTD.Groups[1].Value);
                                    iCounter++;
                                }
                            }

                            // preeche o grid...
                            // NESTE PONTO, rowValues contém a lista dos valores dentro de <TR>...
                            if (iCounter > 3)
                            {
                                int iPosIni = 0;
                                int iPosFim = 0;
                                string sParam = rowValues[2].Trim();

                                int pos2 = sParam.IndexOf("<a href=\"Javascript:AbreArquivo", 0);
                                string idNews = sParam.Substring(pos2 + 33, 6);

                                iPosIni = sParam.IndexOf("');", 0);
                                iPosFim = sParam.IndexOf("</a>", 0);
                                string sEmpresa = sParam.Substring(iPosIni + 5, iPosFim - iPosIni - 5);

                                iPosIni = sParam.IndexOf("<br>", 0);
                                string sAssunto = sParam.Substring(iPosIni + 4, sParam.Length - iPosIni - 4);

                                iPosIni = sParam.IndexOf("<br>", 0);

                                result.Rows.Add(rowValues[0].Trim(), rowValues[1], idNews, sEmpresa, sAssunto);
                            }
                        }
                    }
                }
                StringText = string.Empty;
                NewStringText = string.Empty;
            }
            dtg.DataSource = result;
        }

        private void frmFatosRelevantes_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");

            FileName = FilePath + "FatosRelevantes.txt";

            if (!Directory.Exists(FilePath)) { Directory.CreateDirectory(FilePath); }
            if (!File.Exists(FileName)) { File.Create(FileName).Close(); }

            LoadList();

            DownloadByWebClient();
            timer1.Start();
            checkBox1.Checked = true;
        }

        private void LoadList()
        {
            StreamReader sReader = new StreamReader(FileName);

            string tempLine = "";
            while ((tempLine = sReader.ReadLine()) != null)
            {
                lock (NewsList) { if (!NewsList.Contains(tempLine)) { NewsList.Add(tempLine); } }
            }
            sReader.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DownloadByWebClient();
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            this.BringToFront();
            this.CenterToParent();
            this.WindowState = FormWindowState.Normal;
            Console.Beep();
        }

        private void dg_DoubleClick(object sender, EventArgs e)
        {
            GridView GetColumn = sender as GridView;

            string Empresa = GetColumn.GetFocusedRowCellValue("Empresa").ToString();
            string ID = GetColumn.GetFocusedRowCellValue("ID").ToString();

            string sLink = @"http://siteempresas.bovespa.com.br/consbov/ArquivosExibe.asp?site=C&protocolo=" + ID;

            Process.Start("IEXPLORE.EXE", sLink);
            MarcarLidos(ID, Empresa);
        }

        private void MarcarLidos(string ID, string Empresa)
        {
            if (UnreadList.Contains(ID))
            {
                if (Convert.ToInt32(MessageBox.Show("Deseja marcar o fato relevante de " + Empresa + " como lido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == 6)
                {
                    lock (NewsList) { if (!NewsList.Contains(ID)) { NewsList.Add(ID); } }
                    lock (UnreadList) { if (UnreadList.Contains(ID)) { UnreadList.Remove(ID); } }

                    StreamWriter sWriter = new StreamWriter(FileName, true);
                    sWriter.WriteLine(ID);
                    sWriter.Close();
                    sWriter.Dispose();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lock (UnreadList)
            {
                for (int i = 0; i < dg.DataRowCount; i++)
                {
                    if (!NewsList.Contains(dg.GetRowCellValue(i, "ID").ToString()))
                    {
                        if (!UnreadList.Contains(dg.GetRowCellValue(i, "ID").ToString())) UnreadList.Add(dg.GetRowCellValue(i, "ID").ToString());
                    }
                }

                if (UnreadList.Count > 0)
                {
                    this.notifyIcon1.BalloonTipText = "Fato Relevante";
                    this.notifyIcon1.BalloonTipTitle = "FATO RELEVANTE";
                    this.notifyIcon1.Icon = this.Icon;
                    this.notifyIcon1.Visible = true;
                    this.notifyIcon1.ShowBalloonTip(4);
                }
            }
        }

        private void dg_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int TempVal = e.RowHandle;
            GridView curView = (GridView)sender;

            if (!NewsList.Contains(curView.GetRowCellValue(TempVal, "ID").ToString()))
            {
                e.Appearance.ForeColor = Color.Red;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("IEXPLORE.EXE", "http://siteempresas.bovespa.com.br/consbov/ExibeFatosRelevantesCvm.asp?empresa=&pagina=1&SITE=C&strurl=images/fundo_.gif&BOTAO=images/volta_verde.gif");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer2.Start();
            }
            else
            {
                timer2.Stop();
            }
        }

        private void dg_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);

            if (hitInfo.InRow)
            {
                if (hitInfo.RowHandle >= 0)
                {
                    GridContextMenu = new ContextMenu();

                    string ID = dg.GetRowCellValue(dg.FocusedRowHandle, "ID").ToString();
                    string Empresa = dg.GetRowCellValue(dg.FocusedRowHandle, "Empresa").ToString();

                    mnuSetRead = new MenuItem();
                    mnuSetRead.Text = "Marcar como lido";
                    mnuSetRead.Click += new EventHandler(mnuSetRead_Click);
                    mnuSetRead.Tag = ID + "_" + Empresa + "_" + hitInfo.RowHandle;

                    GridContextMenu.MenuItems.Add(mnuSetRead);

                    view.FocusedRowHandle = hitInfo.RowHandle;
                    GridContextMenu.Show(view.GridControl, e.Point);
                }
            }
        }

        private void mnuSetRead_Click(object sender, EventArgs e)
        {
            string[] tempValues = mnuSetRead.Tag.ToString().Split('_');
            MarcarLidos(tempValues[0], tempValues[1]);
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.CenterToParent();
            this.WindowState = FormWindowState.Normal;
            Console.Beep();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

            this.BringToFront();
            this.CenterToParent();
            this.WindowState = FormWindowState.Normal;
            Console.Beep();
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.BringToFront();
            this.CenterToParent();
            this.WindowState = FormWindowState.Normal;
            Console.Beep();
        }
    }
}
