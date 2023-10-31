using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Data.SqlClient;
using System.IO;


namespace GetFileFutures
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "Importing...";
            cmdGetFile.Enabled = false;
            Get_File(sender, e);

        }

        private void Get_File(object sender, EventArgs e)
        {
            //string StringDate = "110507";
            string StringDate = dtpDateFile.Value.ToString("yyMMdd");
            string remotePath = "http://www.bmf.com.br/Ftp/ContratosCadastradosDescompactados/CD" + StringDate.ToString() + ".TXT";
            string curFileName = "File_CD" + StringDate.ToString() + ".txt";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                    webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                    webClient.DownloadFileAsync(new Uri(remotePath), @"T:\Import\BMF\" + curFileName);
                }
            }
            catch (Exception Excep)
            {
                if (Excep.ToString().IndexOf("(404) Not Found.") > 0)
                {
                    MessageBox.Show("File  Not Found");
                }

            }
        }

        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ProgressDownload.Value = e.ProgressPercentage;
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            lblDisplay.Text = "Imported!";
            cmdRead.Enabled = true;
            cmdGetFile.Enabled = false;
            MessageBox.Show("Download completed!");

        }

        void Read_File()
        {
            string strPathWrite = @"T:\Import\BMF\List.txt";

            FileInfo FInfo = new FileInfo(strPathWrite);
            if (FInfo.Exists)
            {
                FInfo.Delete();
            }
            FInfo.Create().Close();

            string strPath = @"T:\Import\BMF\File_CD" + dtpDateFile.Value.ToString("yyMMdd") + ".txt";
            string SLine;
            FileInfo FileInfo = new FileInfo(strPath);
            int Counter = 0;
            GetDataFromWS ws = new GetDataFromWS();
            string Symbol;
            string Security;

            using (StreamReader sR = FileInfo.OpenText())
            {
                while ((SLine = sR.ReadLine()) != null)
                {
                    Symbol = SLine.Substring(105, 14);
                    Security = ws.GetSecurity("36600", "634259", Symbol.Trim());

                    WriteFile(Symbol, Security);
                }
                Counter++;
            }
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            lblDisplay.Text = "Reading...";
            cmdRead.Enabled = false;
            Read_File();
            cmdRead.Enabled = true;
            cmdGetFile.Enabled = !Enabled;
            lblDisplay.Text = "Readed!";
        }

        public static int WriteFile(string Symbol, string Security)
        {
            string strPathWrite = @"T:\Import\BMF\List.txt";

            FileInfo FInfo = new FileInfo(strPathWrite);
            StreamWriter SW;
            if (Security.ToString() != "")
            {
                SW = FInfo.AppendText();
                SW.WriteLine(Symbol.ToString().Trim() + "," + Security.ToString());
                SW.Flush();
                SW.Close();
            }

            return 1;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}