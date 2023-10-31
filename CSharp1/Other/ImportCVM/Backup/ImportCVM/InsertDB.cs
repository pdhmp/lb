using System;
using System.Collections.Generic;
using System.Text;
using NestDLL;

namespace ImportCVM
{
    public class DB_Inserter: IDisposable
    {
        private string _zipFileName;
        public string zipFileName
        {
            get { return _zipFileName; }
            set { _zipFileName = value; }
        }

        private string _curFileName;
        public string curFileName
        {
            get { return _curFileName; }
            set { _curFileName = value; }
        }

        private DateTime _KnownDateTime;
        public DateTime KnownDateTime
        {
            get { return _KnownDateTime; }
            set { _KnownDateTime = value; }
        }

        public DB_Inserter(string zipFileName, string curFileName, DateTime KnownDateTime)
        {
            _zipFileName = zipFileName;
            _curFileName = curFileName;
            _KnownDateTime = KnownDateTime;
        }

        public void InsertDB_Batch(string tableName, List<List<string[]>> tempBatchData)
        {
            if (tempBatchData.Count > 0)
            {
                string isConsolidated = "0";

                if (curFileName.Contains("DFP")) isConsolidated = "0";
                if (curFileName.Contains("DFPC")) isConsolidated = "1";

                string SQLString = "INSERT INTO NESTOTHER.dbo." + tableName;

                string SQLFields = "ZipName, FileName, KnowDateTime, isConsolidated";

                foreach (string[] curField in tempBatchData[0])
                {
                    SQLFields = SQLFields + ", " + curField[0];
                }

                string SQLValues = "";

                for (int i = 0; i < tempBatchData.Count; i++)
                {
                    List<string[]> tempData = tempBatchData[i];

                    SQLValues = SQLValues + " UNION SELECT '" + _zipFileName + "', '" + _curFileName + "', '" + KnownDateTime.ToString("yyyy-MM-dd hh:mm:ss") + "', " + isConsolidated;

                    foreach (string[] curField in tempData)
                    {
                        SQLValues = SQLValues + ", '" + curField[1].Replace("\'", "") + "' ";
                    }
                }

                SQLValues = SQLValues.Substring(7);

                string finalSQL = SQLString + "(" + SQLFields + ") " + SQLValues + "";

                using (NestConn curConn = new NestConn())
                {
                    try
                    {
                        curConn.ExecuteNonQuery(finalSQL);
                    }
                    catch (Exception e)
                    {
                        NestDLL.Log.AddLogEntry("Error on BATCH insert for file: " + curFileName, @"T:\Log\Import_CVM_Log.txt");
                        NestDLL.Log.AddLogEntry("Error: " + e.ToString(), @"T:\Log\Import_CVM_Log.txt");
                        throw e;
                    }
                }
            }
        }

        public void InsertDB(string tableName, List<string[]> tempData)
        {
            string isConsolidated = "0";

            if (curFileName.Contains("DFP") || curFileName.Contains("ITR")) isConsolidated = "0";
            if (curFileName.Contains("DFPC") || curFileName.Contains("ITRC")) isConsolidated = "1";

            string SQLString = "INSERT INTO NESTOTHER.dbo." + tableName;

            string SQLFields = "ZipName, FileName, KnowDateTime, isConsolidated";
            string SQLValues = "'" + _zipFileName + "', '" + _curFileName + "', '" + KnownDateTime.ToString("yyyy-MM-dd hh:mm:ss") + "', " + isConsolidated;

            foreach (string[] curField in tempData)
            {
                SQLFields = SQLFields + ", " + curField[0].Replace(" ", "");
                string tempFieldValue = curField[1].Replace(',', '.').Replace("\'", "");
                if (tempFieldValue.Contains("00000-"))
                {
                    int tempIndex = tempFieldValue.IndexOf('-');
                    tempFieldValue = tempFieldValue.Substring(tempIndex);
                }
                SQLValues = SQLValues + ", '" + tempFieldValue + "'";
            }

            string finalSQL = SQLString + "(" + SQLFields + ") VALUES (" + SQLValues.Replace("'-'", "'0'") + ")";
            finalSQL = finalSQL.Replace("11980101", "19980101");
            finalSQL = finalSQL.Replace("11970101", "19970101");

            finalSQL = finalSQL.Replace("0.0000-", "0.00000");
            
            using (NestConn curConn = new NestConn())
            {
                try
                {
                    curConn.ExecuteNonQuery(finalSQL);
                }
                catch (Exception e)
                {
                    NestDLL.Log.AddLogEntry("Error on insert for file: " + curFileName, @"T:\Log\Import_CVM_Log.txt");
                    NestDLL.Log.AddLogEntry("Error: " + e.ToString(), @"T:\Log\Import_CVM_Log.txt");
                    //throw e;
                }
            }
        }
    
        void System.IDisposable.Dispose()
        {

        }

    
    }
}
