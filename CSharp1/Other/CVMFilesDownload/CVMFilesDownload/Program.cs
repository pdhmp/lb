using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;

using System.IO;

namespace CVMFilesDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            string remotePath = "";
            using (StreamReader thisReader = File.OpenText(@"C:\temp\missfiles.txt"))
            {
                string curLine = "";
                while ((curLine = thisReader.ReadLine()) != null)
                {
                    remotePath = curLine;
                    WebClient client = new WebClient();
                    string curFileName = remotePath.Replace("http://siteempresas.bovespa.com.br/DXW/FormDetalheDownload.asp?", "").Replace("=", "_").Replace("&", "___").Replace("/", "") + ".zip";
                    client.DownloadFile(remotePath, @"C:\itr\New Files\" + curFileName);


                    Console.WriteLine("Imported: " + curLine);
                }
            }
        }
    }
}
