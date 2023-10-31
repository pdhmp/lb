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
using System.Threading;
namespace LiveBook
{
    public partial class frmRegistroOpas : LBForm
    {
        public string CvmSite = "http://www.cvm.gov.br/asp/cvmwww/registro/opa/opareg.asp?AnoProc=" + DateTime.Now.Year;
        public bool FileOpened = false;
        DataTable result = new DataTable();

        public frmRegistroOpas()
        {
            InitializeComponent();
        }

        private void frmRegistroOpas_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            DownloadByWebClient();
            timer1.Start();
        }

        public void DownloadByWebClient()
        {
            string StringText = string.Empty;
            string NewStringText = string.Empty;

            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
                byte[] mybytes = client.DownloadData(CvmSite);

                StringText = Encoding.UTF7.GetString(mybytes);
                // StringText = Encoding.Default.GetString(mybytes); 
                client.Dispose();

                // ------------------------------------------------------------------------------------

                int IniCounter = 0;
                int EndCounter = 0;
                int iCounter = 0;

                IniCounter = StringText.IndexOf("<TABLE BORDER=");
                EndCounter = StringText.IndexOf("</TABLE>", IniCounter);
                NewStringText = StringText.Substring(IniCounter, EndCounter - IniCounter + 8);
                // MessageBox.Show(NewStringText);
                NewStringText = NewStringText.Replace(@"<A HREF='registradas/listareg.asp?Tipo=4&AnoProc=2013'", "").Replace(@"<A HREF='registradas/listareg.asp?Tipo=11&AnoProc=2013'", "").Replace(@"</A>", "").Replace("<FONT CLASS='BodyPB'>", "").Replace("CLASS=", "").Replace("<FONT", "").Replace("'BodyXP'>", "").Replace("'MenuItemP'>", "").Replace("</FONT>", "").Replace("(*)", "");

                //------------------------------------------------------------------------------------
                // limpa a tabela...
                result.Columns.Clear();
                result.Rows.Clear();
                // inicializa a tabela...
                result.Columns.Add("Description", typeof(string));
                result.Columns.Add("Quantity", typeof(string));
                result.Columns.Add("Volume", typeof(string));
                result.Columns.Add("Historical", typeof(string));

                // procura as linhas da tabela...
                System.Text.RegularExpressions.MatchCollection matches =
                 System.Text.RegularExpressions.Regex.Matches(NewStringText, @"<TR.*?>(.*?)</TR>", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);

                // varre os resultados e adiciona na lista...
                foreach (System.Text.RegularExpressions.Match match in matches)
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
                                rowValues.Add(matchTD.Groups[1].Value.Trim());
                                iCounter++;
                            }
                        }

                        if (iCounter > 3)
                        {
                            // preeche o grid...
                            // NESTE PONTO, rowValues contém a lista dos valores dentro de <TR>...
                            result.Rows.Add(rowValues[0].Trim(), rowValues[1], rowValues[2], ReadOPAs(rowValues[0].Trim()));
                        }
                    }

                dtg.DataSource = result;

                foreach (DataRow curRow in result.Rows)
                {
                    if (!curRow["Description"].ToString().Contains("TOTAL"))
                    {
                        if (curRow["Quantity"].ToString() != curRow["Historical"].ToString())
                        {
                            this.notifyIcon1.BalloonTipText = "OPA - Ofertas Públicas de Aquisição de Ações Registradas";
                            this.notifyIcon1.BalloonTipTitle = curRow["Description"].ToString();
                            this.notifyIcon1.Icon = this.Icon;
                            this.notifyIcon1.Visible = true;
                            this.notifyIcon1.ShowBalloonTip(4);
                        }
                    }
                }
            }
            dg.BestFitColumns();
        }

        public string ReadOPAs(string sParam)
        {
            string sAuxi = "";
            string Path = @"C:\Livebook\";
            string sFile = Path + "OPAs.txt";
            string tempLine = "";

            if (!FileOpened)
            {
                FileOpened = true;
                if (!Directory.Exists(Path)) { Directory.CreateDirectory(Path); }
                if (!File.Exists(sFile)) { File.Create(sFile).Close(); }

                System.IO.StreamReader sr = new System.IO.StreamReader(sFile);
                sParam = SubstituiAcentos(sParam).Trim().Replace(" ", "_");

                while ((tempLine = sr.ReadLine()) != null)
                {
                    string[] curLine = tempLine.Split('=');
                    if (curLine[0] == sParam.ToString().Trim())
                    {
                        sAuxi = curLine[1];
                        break;
                    }
                }

                sr.Close();
                FileOpened = false;
            }
            return sAuxi;
        }

        public static string SubstituiAcentos(string s)
        {
            string str = s.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();
            foreach (char ch in str)
            {
                if (System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch) != System.Globalization.UnicodeCategory.NonSpacingMark) builder.Append(ch);
            }

            return builder.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DownloadByWebClient();
        }

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            this.BringToFront();
            this.CenterToScreen();
            this.WindowState = FormWindowState.Normal;
            Console.Beep();
        }

        private void dg_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int TempVal = e.RowHandle;
            GridView curView = (GridView)sender;

            if (curView.GetRowCellValue(TempVal, "Quantity").ToString() != curView.GetRowCellValue(TempVal, "Historical").ToString())
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

        private void dg_DoubleClick(object sender, EventArgs e)
        {
            GridView GetColumn = sender as GridView;
            string ColumnName = GetColumn.GetFocusedRowCellValue("Description").ToString();

            if (ColumnName == "ALIENAÇÃO DE CONTROLE (**)" || ColumnName == "ALIENAÇÃO DE CONTROLE") { Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=4&AnoProc=" + DateTime.Now.Year); }
            else if (ColumnName == "CANCELAMENTO DE REGISTRO") { Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=11&AnoProc=" + DateTime.Now.Year); }
            else if (ColumnName == "AUMENTO DE PARTICIPACÃO") { Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=200&AnoProc=" + DateTime.Now.Year); }
            else if (ColumnName == "VOLUNTÁRIA") { Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=154&AnoProc=" + DateTime.Now.Year); }
            else if (ColumnName == "CONCORRENTE") { Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=184&AnoProc=" + DateTime.Now.Year); }
            else if (ColumnName == "TOTAL DE OFERTAS  NO ANO:")
            {
                Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=4&AnoProc=" + DateTime.Now.Year);
                Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=11&AnoProc=" + DateTime.Now.Year);
                Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=200&AnoProc=" + DateTime.Now.Year);
                Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=154&AnoProc=" + DateTime.Now.Year);
                Process.Start("IEXPLORE.EXE", "http://www.cvm.gov.br/asp/cvmwww/registro/opa/registradas/listareg.asp?Tipo=184&AnoProc=" + DateTime.Now.Year);
            }

            if (Convert.ToInt32(MessageBox.Show("Deseja marcar " + ColumnName + " como lido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == 6)
            {
                MarcarComoLido(ColumnName);
            }

        }

        private void MarcarComoLido(string TipoNoticia)
        {
            string sFile = @"C:\Livebook\OPAs.txt";

            if (!FileOpened)
            {
                FileOpened = true;

                StreamWriter sw = new StreamWriter(sFile);

                foreach (DataRow curRow in result.Rows)
                {
                    if (curRow["Description"].ToString() == "ALIENAÇÃO DE CONTROLE (**)") { sw.WriteLine("ALIENACAO_DE_CONTROLE_(**)=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "ALIENAÇÃO DE CONTROLE") { sw.WriteLine("ALIENACAO_DE_CONTROLE=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "AQUISIÇÃO DE CONTROLE") { sw.WriteLine("AQUISICAO_DE_CONTROLE=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "AUMENTO DE PARTICIPAÇÃO") { sw.WriteLine("AUMENTO_DE_PARTICIPACAO=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "CANCELAMENTO DE REGISTRO") { sw.WriteLine("CANCELAMENTO_DE_REGISTRO=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "VOLUNTÁRIA") { sw.WriteLine("VOLUNTARIA=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "CONCORRENTE") { sw.WriteLine("CONCORRENTE=" + curRow["Quantity"].ToString()); }
                    if (curRow["Description"].ToString() == "TOTAL DE OFERTAS  NO ANO:") { sw.WriteLine("TOTAL_DE_OFERTAS__NO_ANO:=" + curRow["Quantity"].ToString()); }
                }
                sw.Close();

                FileOpened = false;
            }
            else { System.Threading.Thread.Sleep(5000); MarcarComoLido(TipoNoticia); }
        }
    }
}
