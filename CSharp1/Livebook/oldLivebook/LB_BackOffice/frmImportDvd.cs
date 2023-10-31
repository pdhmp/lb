using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using NestDLL;

namespace LiveBook
{
    public partial class frmImportDividends : Form
    {

        private  SortedDictionary<int, string[]> SortDic = new SortedDictionary<int, string[]>();

        private newNestConn curConn = new newNestConn();

        public frmImportDividends()
        {
            InitializeComponent();
        }

        private DateTime ReturnDatetime(string StringData, int DateType)
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

        private string ReturnString(string text, string initag, string endtag)
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

        private void ReadWebPage()
        {
           WebClient client = new WebClient();
           string url = "http://www.bmfbovespa.com.br/Agencia/consulta.asp?codagencia=18&nomeagencia=BOVESPA";
           string result = client.DownloadString(url);
           int pos=0;
           DataTable dt = new DataTable("Dividends");
           Type strType = System.Type.GetType("System.String");

           dt.Columns.Add("StatusDividend", strType);
           dt.Columns.Add("IdDividend", strType);
           dt.Columns.Add("TextDividend", strType);
           dt.Columns.Add("TimeDividend", strType);
           dt.Columns.Add("URLDividend", strType);

           int CountArray = 0;
           DataRow myRow;
           while ((pos = result.IndexOf("\" target=\"Noticia\">PROVENTOS", pos + 1)) != -1)
           {

              int pos2 = result.IndexOf("\"><a href=\"corpo", pos - 250);
              string urlnews = result.Substring(pos2 + 11, pos-(pos2 + 10));

              int pos3 = result.IndexOf(":", pos2-100);
              string horario = result.Substring(pos3 - 2, 5);

              int posId1 = urlnews.IndexOf("exibir&id=");
              int posId2 = urlnews.IndexOf("&", posId1+8);
              string IdNews = urlnews.Substring(posId1 + 10, posId2 - (posId1 + 10));

              int postext1 = urlnews.IndexOf("manchete=");
              string TextNews = urlnews.Substring(postext1 + 9).Replace("&quot;","");

              string StringSQL;

              StringSQL = "SELECT IdExchangeDividend FROM NESTDB.dbo.Tb720_Dividends(nolock) WHERE IdExchangeDividend=" + IdNews;

              double retorno = NestDLL.Utils.ParseToDouble(curConn.Execute_Query_String(StringSQL));
               string flagStatus;
              if (retorno > 0)
              {
                  flagStatus = "Delete";
              }
              else
              {
                  flagStatus = "Insert";
              }
              dt.Rows.Add(flagStatus,IdNews, TextNews,horario, urlnews);
  
              CountArray ++;
           }

            dtgDividends.DataSource = dt;
        }

        private void ReadNews(string urlnews, string horario, string Id_News)
        {
            WebClient client = new WebClient();
            string url = "http://www.bmfbovespa.com.br/Agencia/"+urlnews;

            string result = client.DownloadString(url);
 
            int pos = result.IndexOf("<pre>(", 0);

            string Data = result.Substring(pos+6, 5);

            int pos2 = result.IndexOf("EMPRESA:", 0);

            int pos3 = result.IndexOf("</pre></", 0);

            string curText = result.Substring(pos2, pos3 - pos2);


            string YearPart = DateTime.Now.Year.ToString();

            Data += "/" + YearPart;

            Data = YearPart+"-" + Data.Substring(3, 2) + "-"+ Data.Substring(0, 2);

            ReadFile(curText, Convert.ToDateTime(Data), horario, Id_News);
        }

        private int ReadFile(string curText, DateTime Data, string horario, string Id_News)
        {
            Int32 pos=0;

            DateTime RecDate;

            using (newNestConn curConn = new newNestConn())
            {
                RecDate = Convert.ToDateTime(curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',-1,'" + Convert.ToDateTime(Data).ToString("yyyyMMdd") + "',2,1)"));
            }
            string TickersImported="";

            string oldTicker = "";
            while ((pos = curText.IndexOf("APROVADO", pos+1)) != -1)
            {
                string TipoProvento = curText.Substring(pos-16,16 ).Trim();
                string Ticker = curText.Substring(pos-36,13 ).Trim();
                string datapag="";
                DateTime datapag2 = DateTime.Parse("01/01/1900"); 
                string valor = "";
                Boolean FlagInsert = false;
                int TransType = 0;

                int pos22 = curText.IndexOf("VLR PROV:", pos + 1);
                valor = curText.Substring(pos22 + 9, 12).Trim();

                if (TipoProvento == "DIVIDENDO")
                {
                    int pos2 = curText.IndexOf("VLR PROV:", pos + 1);
                    valor = curText.Substring(pos2+9, 12).Trim();
                    int pos3 = curText.IndexOf("PREV PAG:", pos + 1);
                    datapag = curText.Substring(pos3 + 9, 12).Trim();
                    FlagInsert = true;
                    TransType = 21;
                }

                if (TipoProvento == "REST CAP DIN")
                {
                    int pos2 = curText.IndexOf("VLR PROV:", pos + 1);
                    valor = curText.Substring(pos2 + 9, 12).Trim();
                    int pos3 = curText.IndexOf("PREV PAG:", pos + 1);
                    datapag = curText.Substring(pos3 + 9, 12).Trim();
                    FlagInsert = true;
                    TransType = 21;
                }
                if (TipoProvento == "JRS CAP PROPRIO")
                {
                    int pos2 = curText.IndexOf("VLR PROV:", pos + 1);
                    valor = curText.Substring(pos2 + 9, 12).Trim();
                    int pos3 = curText.IndexOf("PREV PAG:", pos + 1);
                    if (pos3 == -1)
                    {
                        datapag = Data.AddYears(10).ToString("dd/MM/yyyy");
                    
                    }
                    else
                    {
                        datapag = curText.Substring(pos3 + 9, 12).Trim();
                    }
                    FlagInsert = true;
                    TransType = 22;
                }

                if (TipoProvento == "BONIFICACAO" || TipoProvento == "SUBSCRICAO")
                {
                    int pos2 = curText.IndexOf("PERCENT:", pos + 1);
                    valor = curText.Substring(pos2 + 8, 12).Trim();
                    FlagInsert = true;
                    TransType = 23;
                }

                if (TipoProvento == "DESDOBRAMENTO")
                {
                    int pos2 = curText.IndexOf("PERCENT:", pos + 1);
                    valor = curText.Substring(pos2 + 8, 12).Trim();
                    TransType = 23;
                }
                if (TipoProvento == "GRUPAMENTO")
                {
                    int pos2 = curText.IndexOf("FATOR:", pos + 1);
                    valor = curText.Substring(pos2 + 6, 12).Trim();
                    FlagInsert = true;
                    TransType = 23;
                }


                double Net = 0;
                double Gross = 0;
                double Bonus = 0;

                if (TransType == 21 || TransType == 22)
                {
                    Net = Convert.ToDouble(valor);
                    Gross = Convert.ToDouble(valor);
                }

                if (TransType == 23)
                {
                    Bonus = Convert.ToDouble(valor);
                }

                if (FlagInsert == true)
                {
                    if (Ticker == "")
                    {
                        Ticker = oldTicker;
                    }
                    double IdSecurity = NestDLL.Utils.ParseToDouble(curConn.Execute_Query_String("SELECT IdSecurity FROM dbo.Tb001_Securities WHERE NestTicker = '" + Ticker + "' OR ExchangeTicker = '" + Ticker + "' "));

                    if (!DateTime.TryParse(datapag, out datapag2))
                    {
                        datapag2 = Data;
                    }

                    if (IdSecurity != 0)
                    {
                        Console.WriteLine("Security:" + Ticker + " - Tipo Provento: " + TipoProvento + " - Record Date:" + RecDate + " - EX Date:" + Data + " - Data Pagto:" + datapag + " - Valor:" + valor);
                        TickersImported += Ticker+ " ";

                        string SQLString = "INSERT INTO [NESTDB].[dbo].[Tb720_Dividends]([Transaction_Type],[Id_Ticker],[Declared_Date],[Ex_Date],[Record_Date],[Payment_Date],[Per_Share_Net],[Per_Share_Gross],[Percent_Bonus],[Description],IdExchangeDividend)" +
                            "VALUES (" + TransType + ", " + IdSecurity + ", '" + Data.ToString("yyyyMMdd") + "', '" + Data.ToString("yyyyMMdd") + "', '" + RecDate.ToString("yyyyMMdd") + "', '" + datapag2.ToString("yyyyMMdd") + "', " + Net.ToString().Replace(",", ".") + ", " + Gross.ToString().Replace(",", ".") + ", " + Bonus.ToString().Replace(",", ".") + ", '','" + Id_News + "')";

                        curConn.ExecuteNonQuery(SQLString);
                        oldTicker = Ticker;
                    }
                }
            }

            if (TickersImported != "")
            {
                MessageBox.Show("File processed successfully inserted for these assets: " + TickersImported , "Dividends", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("File successfully processed but no dividends was inserted!", "Dividends", MessageBoxButtons.OK);
            
            }


            return 0;
        }


        private void frmImportDividends_Load(object sender, EventArgs e)
        {
            dtgDividends.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDividends.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDividends.LookAndFeel.SetSkinStyle("Blue");
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            ReadWebPage();
            Cursor.Current = Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgDividends_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colStatusDividend" || e.Column.Name == "colURLDividend")
            {
                e.Appearance.ForeColor = Color.Blue;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Underline);
            }
        }

        private void dgDividends_DoubleClick(object sender, EventArgs e)
        {
            Point pt = dtgDividends.PointToClient(MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = dgDividends.CalcHitInfo(pt);

            if (hi.InRow && hi.Column != null)
            {
                string StatusDvd = this.dgDividends.GetFocusedRowCellValue("StatusDividend").ToString();
                if (hi.Column.Name == "colStatusDividend")
                {
                    string Id_News = this.dgDividends.GetFocusedRowCellValue("IdDividend").ToString();
                    string urlNews = this.dgDividends.GetFocusedRowCellValue("URLDividend").ToString();
                    string Horario = this.dgDividends.GetFocusedRowCellValue("TimeDividend").ToString();

                    if (StatusDvd == "Insert")
                    {
                     ReadNews(urlNews, Horario, Id_News);
                    }
                    if (StatusDvd == "Delete")
                    {
                        string StringSQL = "DELETE FROM dbo.Tb720_Dividends WHere IdExchangeDividend =" + Id_News;
                        curConn.ExecuteNonQuery(StringSQL);
                        MessageBox.Show("Deleted!");
                        btnRead_Click(sender, e);
                    }
                }
                if (hi.Column.Name == "colURLDividend")
                {
                    string urlNews = this.dgDividends.GetFocusedRowCellValue("URLDividend").ToString();
                    string url = "http://www.bmfbovespa.com.br/Agencia/" + urlNews;

                    System.Diagnostics.Process.Start("IEXPLORE.EXE", url);
                }
            }
        }

    }
}