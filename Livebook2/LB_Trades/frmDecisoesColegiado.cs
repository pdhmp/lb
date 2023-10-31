using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraExport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Threading;

namespace LiveBook
{
    public partial class frmDecisoesColegiado : LBForm
    {
        List<string> NewsList = new List<string>();
        List<string> UnreadList = new List<string>();

        ContextMenu GridContextMenu;
        MenuItem mnuSetRead;

               string FilePath = @"C:\Livebook\";
        string FileName = "";

        public bool FileOpened = false;

        public frmDecisoesColegiado() { InitializeComponent(); }

        private void frmDecisoesColegiado_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");

            FileName = FilePath + "DecisoesColegiado.txt";

            if (!Directory.Exists(FilePath)) { Directory.CreateDirectory(FilePath); }
            if (!File.Exists(FileName)) { File.Create(FileName).Close(); }

            LoadList();

            DownloadByWebClient();
            timer1.Start();
            timer2.Start();
        }

        public void DownloadByWebClient()
        {
            try
            {
                // ArquivosExibe.asp?site=C&protocolo='+ strProtocolo,'
                string url = "http://www.cvm.gov.br/port/descol/indice.asp?Ano=" + DateTime.Now.Year;
                string StringText = string.Empty;
                string NewStringText = string.Empty;

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)");
                    byte[] mybytes = client.DownloadData(url);

                    StringText = Encoding.UTF7.GetString(mybytes);
                    client.Dispose();

                    // ------------------------------------------------------------------------------------
                    int IniCounter = StringText.IndexOf("><b>Selecione o Dia:</b></font><br><br>");
                    NewStringText = StringText.Substring(IniCounter + 39, StringText.Length - IniCounter - 39);
                    //MessageBox.Show(NewStringText);
                    //------------------------------------------------------------------------------------

                    // inicializa a tabela...
                    DataTable result = new DataTable();
                    result.Columns.Add("Data", typeof(string));
                    result.Columns.Add("Link", typeof(string));

                    // procura as linhas da tabela...
                    System.Text.RegularExpressions.MatchCollection matches =
                    System.Text.RegularExpressions.Regex.Matches(NewStringText, @"<font.*?>(.*?)</font>", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);

                    // varre os resultados e adiciona na lista...
                    foreach (System.Text.RegularExpressions.Match match in matches)
                    {
                        if (match.Success && match.Groups.Count > 1)
                        {
                            string sParam = match.Groups[1].Value;

                            int iPos = sParam.IndexOf("<a href='resp.asp?File=", 0);
                            int iPosFim = sParam.IndexOf("' target='SubMain'");
                            string sURL = "http://www.cvm.gov.br/port/descol/" + sParam.Substring(iPos + 9, iPosFim - iPos - 9);

                            iPos = sParam.IndexOf("target='SubMain'><b>");
                            string sData = sParam.Substring(iPos + 20, 10);

                            // Console.WriteLine("URL : " + sURL + " - sData : " + sData);
                            result.Rows.Add(sData, sURL);
                        }
                    }

                    dtg.DataSource = result;
                }
            }
            catch { }
        }

        private void dg_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GridView GetColumn = sender as GridView;

                string ColumnName = GetColumn.GetFocusedRowCellValue("Data").ToString();

                System.Diagnostics.Process.Start("IEXPLORE.EXE", GetColumn.GetFocusedRowCellValue("Link").ToString());

                if (Convert.ToInt32(MessageBox.Show("Deseja marcar " + ColumnName + " como lido?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == 6)
                {
                    LeItens(ColumnName);
                }
            }
            catch { }
        }

        public void LeItens(string sTexto)
        {
            bool bAuxi = false;

            if (System.IO.File.Exists(FileName))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(FileName);
                string tempLine = "";

                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine.Contains(sTexto))
                    {
                        bAuxi = true;
                        break;
                    }
                }

                sr.Close();
                sr.Dispose();

                if (!bAuxi)
                {
                    lock (NewsList) { if (!NewsList.Contains(sTexto)) { NewsList.Add(sTexto); } }
                    lock (UnreadList) { if (UnreadList.Contains(sTexto)) { UnreadList.Remove(sTexto); } }

                    bAuxi = false;

                    System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);

                    sw.WriteLine(sTexto);
                    sw.Close();
                }
            }
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

        private void notifyIcon1_BalloonTipClosed(object sender, EventArgs e)
        {
            this.BringToFront();
            this.CenterToParent();
            this.WindowState = FormWindowState.Normal;
            Console.Beep();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DownloadByWebClient();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lock (UnreadList)
            {
                for (int i = 0; i < dg.DataRowCount; i++)
                {
                    if (!NewsList.Contains(dg.GetRowCellValue(i, "Data").ToString()))
                    {
                        if (!UnreadList.Contains(dg.GetRowCellValue(i, "Data").ToString())) UnreadList.Add(dg.GetRowCellValue(i, "Data").ToString());
                    }
                }

                if (UnreadList.Count > 0)
                {
                    this.notifyIcon1.BalloonTipText = "Nova Decisão de Colegiado";
                    this.notifyIcon1.BalloonTipTitle = "NOVA DECISÃO DE COLEGIADO";
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

            if (!NewsList.Contains(curView.GetRowCellValue(TempVal, "Data").ToString()))
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

        private void dg_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);

            if (hitInfo.InRow)
            {
                if (hitInfo.RowHandle >= 0)
                {
                    GridContextMenu = new ContextMenu();

                    string ID = dg.GetRowCellValue(dg.FocusedRowHandle, "Data").ToString();

                    mnuSetRead = new MenuItem();
                    mnuSetRead.Text = "Marcar como lido";
                    mnuSetRead.Click += new EventHandler(mnuSetRead_Click);
                    mnuSetRead.Tag = ID + "_" + hitInfo.RowHandle;

                    GridContextMenu.MenuItems.Add(mnuSetRead);

                    view.FocusedRowHandle = hitInfo.RowHandle;
                    GridContextMenu.Show(view.GridControl, e.Point);
                }
            }
        }

        private void mnuSetRead_Click(object sender, EventArgs e)
        {
            string[] tempValues = mnuSetRead.Tag.ToString().Split('_');
            LeItens(tempValues[0]);
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
