using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NestSim.Common
{
    class Log
    {
        
        //Grava mensagens de log em arquivo texto de controle.
       public static void AddLogEntry(string StatusMessage)
        {
           StreamWriter LogFile = null;
           
           if (Properties.Settings.Default.LogStatus)
           
           {
               try
               {
                   if (LogFile == null)
                   {
                       LogFile = new StreamWriter(Properties.Settings.Default.LogPath, true);
                   }
                   LogFile.Write(DateTime.Now.ToString("G") + " - " + StatusMessage + "\r\n");
                   LogFile.Dispose();
                   LogFile = null;
                   GC.Collect();
               }
               catch { }
           }            
       }

        public static void Log_CheckIn(int ProgramID)
        {
            string SQL;

            SQL = "UPDATE [TEST_DB].[dbo].Tb900_CheckIn_Log SET CheckInTime=getdate() WHERE Program_Id=" + ProgramID.ToString();
            
            using (NestConn conn = new NestConn())
            {
                conn.ExecuteNonQuery(SQL);
            }
        }

        public static void Log_Event(int ProgramID,int ProcessID, int EventCode, string Description)
        {
            string SQL;

            SQL = "INSERT INTO [TEST_DB].[dbo].[Tb901_Event_Log]" +
                  " ([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description])" +
                  " Values(" + ProgramID.ToString() + "," + ProcessID.ToString() + "," + 
                  EventCode.ToString() + ",getdate(),'" + Description + "')";

            using (NestConn conn = new NestConn())
            {
                conn.ExecuteNonQuery(SQL);
            }
        }
    }
}
