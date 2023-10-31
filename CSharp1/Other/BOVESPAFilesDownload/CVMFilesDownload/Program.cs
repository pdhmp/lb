using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.IO;


namespace BMF_FilesDownload
{
    class Program
    {
        static void Main(string[] args)
        {

            string remotePath = "";

            string StringDate = "110507";
            //string StringDate = DateTime.Now.ToString("yyMMdd");
            remotePath = "http://www.bmf.com.br/Ftp/ContratosCadastradosDescompactados/CD" + StringDate.ToString() + ".TXT";
            try
            {

                WebClient client = new WebClient();

                //client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(Completed);

                
                string curFileName = "File_CD" + StringDate.ToString() + ".txt";

                    Console.WriteLine("Waiting..........");

                    client.DownloadFile(remotePath, @"T:\Import\BMF\" + curFileName);

                    Console.WriteLine("Imported: " + curFileName);

            }
                catch(WebException e)
                {
                    if (e.ToString().IndexOf("(404) Not Found.") >0)
                    {
                        Console.WriteLine("File  Not Found");
                    
                    }


                }
            

        }
        private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write(e.ProgressPercentage);
        }
        /*
        private void Completed(object sender, AsyncCompletedEventHandler e)
        {
            Console.WriteLine("Download completed!");
        }
        */
    }
}
