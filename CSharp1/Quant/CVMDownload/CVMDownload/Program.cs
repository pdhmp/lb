using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CVMDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            Poster thisTest = new Poster();

            DateTime iniDate = new DateTime(2016, 01, 01);
            DateTime endDate = new DateTime(2016, 04, 04);
            DateTime curDate = endDate;
            DateTime prevCounter = DateTime.Now;
            TimeSpan timeTaken = new TimeSpan(0, 0, 0, 0, 1);

            string retVal = "";

            while (curDate >= iniDate)
            {
                if (!File.Exists("C:\\Log\\CVM_" + curDate.ToString("yyyy-MM-dd") + ".xml"))
                {
                    retVal = thisTest.HttpPost("https://www.rad.cvm.gov.br/DOWNLOAD/SolicitaDownload.asp", "txtlogin=30673772810&txtsenha=101010&txtData=" + curDate.ToString("dd/MM/yyyy") + "&txtHora=00:00&txtdocumento=TODOS");
                    thisTest.fileWrite("C:\\Log\\CVM_" + curDate.ToString("yyyy-MM-dd") + ".xml", retVal);
                    timeTaken = DateTime.Now.Subtract(prevCounter);
                    Console.WriteLine("Downloaded file for: " + curDate.ToString("yyyy-MM-dd") + "   " + timeTaken.TotalMilliseconds / 1000);
                }
                else
                {
                    timeTaken = DateTime.Now.Subtract(prevCounter);
                    Console.WriteLine("File for: " + curDate.ToString("yyyy-MM-dd") + " already exists!  " + timeTaken.TotalMilliseconds / 1000);
                }
                prevCounter = DateTime.Now;
                curDate = curDate.Add(new TimeSpan(-1, 0, 0, 0));
            }
        }
    }
}
