using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ImportCVM
{
    class ReadDFP
    {

        string curPath = "";
        string zipFileName = "";
        DateTime KnownDateTime;
        public bool bolErrors = false;

        public ReadDFP(string _curPath, string _zipFileName)
        {
            curPath = _curPath;
            zipFileName = _zipFileName;

            try
            {
                ReadControlInfo();
                ReadConfigInfo();
                ReadHeaderInfo();
                ReadDFPInfo();
            }
            catch (Exception e)
            {
                bolErrors = true;
            }
        }

        private void ReadControlInfo()
        {
            Utils.FileColumns curColumns = new Utils.FileColumns(@"N:\TI\Projects\CSharp\Other\ImportCVM\ImportCVM\CVM_CTR.col");

            using (StreamReader thisReader = File.OpenText(curPath + "\\CVM.CTR"))
            {
                string curLine = "";
                while ((curLine = thisReader.ReadLine()) != null)
                {

                    List<string[]> tempData = Utils.ConvertLine(curLine, curColumns, false);
                    string tempDate = tempData[5][1];
                    string tempHour = tempData[6][1];
                    //tempHour = "08:17:00";
                    KnownDateTime = new DateTime(int.Parse(tempDate.Substring(0, 4)), int.Parse(tempDate.Substring(4, 2)), int.Parse(tempDate.Substring(6, 2)));
                    KnownDateTime = KnownDateTime.Add(TimeSpan.Parse(tempHour));

                    using (DB_Inserter curDBInserter = new DB_Inserter(zipFileName, "DFPCTL.001", KnownDateTime))
                    {
                        curDBInserter.InsertDB("Tb021_CVM_DFP_CTL", tempData);
                    } 
                }
            }
        }

        private void ReadConfigInfo()
        {
            Utils.FileColumns curColumns = new Utils.FileColumns(@"N:\TI\Projects\CSharp\Other\ImportCVM\ImportCVM\config.col");

            using (StreamReader thisReader = File.OpenText(curPath + "\\config.001"))
            {
                string curLine = "";
                while ((curLine = thisReader.ReadLine()) != null)
                {
                    List<string[]> tempData = Utils.ConvertLine(curLine, curColumns, false);
                    using (DB_Inserter curDBInserter = new DB_Inserter(zipFileName, "config.001", KnownDateTime))
                    {
                        curDBInserter.InsertDB("Tb022_CVM_DFP_CONFIG", tempData);
                    } 
                }
            }
        }

        private void ReadHeaderInfo()
        {
            Utils.FileColumns curColumns = new Utils.FileColumns(@"N:\TI\Projects\CSharp\Other\ImportCVM\ImportCVM\DFPHDR.col");

            using (StreamReader thisReader = File.OpenText(curPath + "\\DFPHDR.001"))
            {
                string curLine = "";
                while ((curLine = thisReader.ReadLine()) != null)
                {
                    List<string[]> tempData = Utils.ConvertLine(curLine, curColumns, true);
                    using (DB_Inserter curDBInserter = new DB_Inserter(zipFileName, "DFPHDR.001", KnownDateTime))
                    {
                        curDBInserter.InsertDB("Tb023_CVM_DFP_HDR", tempData);
                    }
                }
            }
        }

        private void ReadDFPInfo()
        {
            Utils.FileColumns curColumns = new Utils.FileColumns(@"N:\TI\Projects\CSharp\Other\ImportCVM\ImportCVM\DFPGENERIC.col");

            string[] fileEntries = Directory.GetFiles(curPath);

            foreach (string tempFile in fileEntries)
            {
                List<List<string[]>> allItems = new List<List<string[]>>();
                string curFile = tempFile.Substring(tempFile.LastIndexOf('\\') + 1);
                if (curFile.Substring(0, 3) == "DFP" && ((!curFile.Contains("DFPHDR") && !curFile.Contains("DIVDI") && !curFile.Contains("SOCEX") && !curFile.Contains("DEMU")) || (curFile.Contains("DFPDER") || curFile.Contains("DFPCDER"))) && curFile.Split('.')[1] == "001")
                //if (curFile.Substring(0, 3) == "DFP" && (curFile.Contains("DFPDER") || curFile.Contains("DFPCDER")) && curFile.Split('.')[1] == "001")
                {
                    using (StreamReader thisReader = File.OpenText(curPath + "\\" + curFile))
                    {
                        string curLine = "";
                        while ((curLine = thisReader.ReadLine()) != null)
                        {
                            List<string[]> tempData = Utils.ConvertLine(curLine, curColumns, false);
                            allItems.Add(tempData);
                        }
                    }
                    using (DB_Inserter curDBInserter = new DB_Inserter(zipFileName, curFile, KnownDateTime))
                    {
                        curDBInserter.InsertDB_Batch("Tb025_CVM_DFP_DATA", allItems);
                    }
                }
            }
        }




    }
}
