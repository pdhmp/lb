using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using NestDLL;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FeedAndima
{
    public partial class frmImport : Form
    {
        public newNestConn curConn = new newNestConn();
        public string StatusImport;
        public DateTime LastCheckedTime;
        public string StatusText = "";

        public frmImport()
        {
            InitializeComponent();
        }

        private void frmImport_Load(object sender, EventArgs e)
        {
            tmrCheckData.Start();
            tmrUpdateScreen.Start();
            (new Thread(new ThreadStart(StartRoutine))).Start();
        }

        void StartRoutine()
        {
            AddLog("\r\n\r\nStarting check routine");

            if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
            {
                LastCheckedTime = DateTime.Now;

                Download_File("lft");
                Download_File("ntn-b");
                Download_File("ntn-c");
                Download_File("ntn-f");
                Download_File("ltn");
            }

        }

        private void Download_File(string FileType)
        {
            AddLog("Downloading \t" + FileType.ToUpper());

            StatusImport = "Downloading...";
            WebClient webClient = new WebClient();

            //string StringURL = "http://www.anbima.com.br/merc_sec/resultados/msec_" + TradeDate.ToString("ddMMMyyyy") + "_" + FileType + ".html";
            string StringURL = "http://www.anbima.com.br/merc_sec/resultados/merc_sec" + "_" + FileType + ".asp";

            try
            {
                webClient.DownloadFile(StringURL, @"T:\Import\Andima\" + FileType + ".txt");

                StatusImport = "Download Finished!";

                if (CheckDownload(FileType))
                {
                    ImportFile(FileType);
                }
            }
            catch (Exception)
            {
                StatusImport = "Page Not Found!";
            }
        }

        bool CheckDownload(string FileType)
        {
            StatusImport = "Checking Download";
            string filename = @"T:\Import\Andima\" + FileType + ".txt";

            StreamReader SR;
            string StringText;

            SR = File.OpenText(filename);
            StringText = SR.ReadToEnd();

            int retorno = StringText.IndexOf("The page cannot be found");

            if (retorno == -1)
            {
                SR.Close();
                SR.Dispose();
                return true;
            }
            else
            {
                SR.Close();
                SR.Dispose();
                AddLog("Invalid file: " + FileType.ToUpper() + ": page not found!");
                return false;
            }
        }

        void ImportFile(string FileType)
        {
            AddLog("Importing:\t\t" + FileType.ToUpper());

            StatusImport = "Importing";

            string filename = @"T:\Import\Andima\" + FileType + ".txt";

            StreamReader SR;
            string StringText;
            string NewStringText;

            SR = File.OpenText(filename);
            StringText = SR.ReadToEnd();

            SR.Close();
            SR.Dispose();

            int IniCounter = 0;
            int EndCounter = 0;

            IniCounter = StringText.IndexOf("ximo (D+1)");
            EndCounter = StringText.IndexOf("</TD></TR></TABLE>", IniCounter);

            NewStringText = StringText.Substring(IniCounter + 19, EndCounter - IniCounter);
            IniCounter = IniCounter + 19;

            DateTime TargetDate = new DateTime();
            string aux = "";

            aux = "<TD  BGCOLOR ='#C0C0C0' ALIGN ='Right' VALIGN ='Bottom' HEIGHT='16,17'><B>";

            aux = StringText.Substring(StringText.LastIndexOf(aux) + aux.Length, 11);

            if (!DateTime.TryParse(aux, out TargetDate))
            {
                aux = "<TD  BGCOLOR ='#C0C0C0' ALIGN ='Right' VALIGN ='Bottom'><B>";
                
                aux = StringText.Substring(StringText.LastIndexOf(aux) + aux.Length, 11);

                TargetDate = DateTime.Parse(aux);
            }

            //aux = "2013-06-24";

            if (!CheckImport(FileType, TargetDate))
            {
                //return;
            }

            string Selic = "";
            string Emisao = "";
            string Vencto = "";
            string Last = "";
            string RateMin = "";
            string RateMax = "";
            string RateAvg = "";
            string MinD0 = "";
            string MaxD0 = "";
            string MinD1 = "";
            string MaxD1 = "";
            int CheckCounter = 0;

            string StringInsert = "INSERT INTO NESTIMPORT.dbo.Tb_Andima_Precos([ImportDate],[TipoTitulo],[Selic],[DataEmissao],[Vencimento],[TxMaxima],[TxMinima],[TxIndicativa] " +
                                    ",[PU],[MinimoD0],[MaximoD0],[MinimoD1],[MaximoD1])";
            bool checkUnion = false;

            while (CheckCounter < NewStringText.Length)
            {
                Selic = "";
                Emisao = "";
                Vencto = "";
                Last = "";
                RateMin = "";
                RateMax = "";
                RateAvg = "";
                MinD0 = "";
                MaxD0 = "";
                MinD1 = "";
                MaxD1 = "";

                int pos1 = NewStringText.IndexOf("'>", CheckCounter);
                if (pos1 == -1) { break; };
                int pos2 = NewStringText.IndexOf("</TD>", pos1);
                Selic = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                Emisao = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                Vencto = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                RateMax = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                RateMin = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                RateAvg = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                Last = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                MinD0 = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                int oldpos = pos2;
                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);

                MaxD0 = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                if (MaxD0.Length >= 3 && MaxD0.Substring(0, 3) == "<I>")
                {
                    pos2 = oldpos;
                    pos1 = NewStringText.IndexOf("'>", pos2);
                    pos2 = NewStringText.IndexOf("</TD>", pos1);
                    MaxD0 = NewStringText.Substring(pos1 + 5, pos2 - pos1 - 9);
                }

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                MinD1 = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                pos1 = NewStringText.IndexOf("'>", pos2);
                pos2 = NewStringText.IndexOf("</TD>", pos1);
                MaxD1 = NewStringText.Substring(pos1 + 2, pos2 - pos1 - 2);

                if (RateMax == "--") { RateMax = "0"; }
                if (RateMin == "--") { RateMin = "0"; }
                if (RateAvg == "--") { RateAvg = "0"; }
                if (Last == "--") { Last = "0"; }
                if (MinD0 == "--") { MinD0 = "0"; }
                if (MaxD0 == "--") { MaxD0 = "0"; }
                if (MinD1 == "--") { MinD1 = "0"; }
                if (MaxD1 == "--") { MaxD1 = "0"; }

                if (checkUnion)
                {
                    StringInsert += " UNION ALL ";
                }
                else
                {
                    checkUnion = true;
                }

                StringInsert += "SELECT '" + TargetDate.ToString("yyyy-MM-dd") + "','" + FileType + "','" + Selic + "','" + Convert.ToDateTime(Emisao).ToString("yyyy-MM-dd") + "','" + Convert.ToDateTime(Vencto).ToString("yyyy-MM-dd") + "'," + RateMax.Replace(".", "").Replace(",", ".") + "," + RateMin.Replace(".", "").Replace(",", ".") + "," + RateAvg.Replace(".", "").Replace(",", ".") + "," + Last.Replace(".", "").Replace(",", ".") + "," + MinD0.Replace(".", "").Replace(",", ".") + "," + MaxD0.Replace(".", "").Replace(",", ".") + "," + MinD1.Replace(".", "").Replace(",", ".") + "," + MaxD1.Replace(".", "").Replace(",", ".");

                CheckCounter = pos2;
            }

                        StatusImport = "Inserting...";
            StringInsert = StringInsert.Replace("<I>", "");
            StringInsert = StringInsert.Replace("</I>", "");

            curConn.ExecuteNonQuery(StringInsert);


            curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.Proc_UpdateSecurity_Andima ; EXEC NESTIMPORT.dbo.ProcInserDataAndima");

            StatusImport = "Imported!";
        }

        bool CheckImport(string FileType, DateTime TargetDate)
        {
            StatusImport = "Checking Import";
            string SQLString = "SELECT COUNT(*) FROM NESTIMPORT.dbo.Tb_Andima_Precos (nolock) WHERE TipoTitulo='" + FileType + "' AND ImportDate='" + TargetDate.ToString("yyyyMMdd") + "'";

            int retorno = Convert.ToInt32(curConn.Execute_Query_String(SQLString));

            if (retorno == 0)
            {
                return true;
            }
            else
            {
                AddLog("Already imported:\t" + FileType.ToUpper() + "\tfor\t" + TargetDate.ToString("dd-MMM-yyyy"));
                return false;
            }
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            lblStatus.Text = StatusImport;
            lblCheckTime.Text = LastCheckedTime.ToString("hh:mm:ss");
            txtStatus.Text = StatusText;
            txtStatus.SelectionStart = txtStatus.Text.Length;
            txtStatus.ScrollToCaret();
            txtStatus.Refresh();
        }

        private void AddLog(string LogText)
        {
            StatusText += "\r\n" + DateTime.Now.ToString("dd-MMM-yy") + "\t" + LogText;
        }

        private void tmrCheckData_Tick(object sender, EventArgs e)
        {
            (new Thread(new ThreadStart(StartRoutine))).Start();
        }

        private void btnUpdateSecurity_Click(object sender, EventArgs e)
        {
            curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.Proc_UpdateSecurity_Andima ; EXEC NESTIMPORT.dbo.ProcInserDataAndima");
            MessageBox.Show("Done!");
        }
    }
}
