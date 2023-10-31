using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NestDLL;

using Feed_Bloomberg.Engine;

namespace Feed_Bloomberg
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
                    //if (LogFile == null)
                    //{
                        //LogFile = new StreamWriter(Properties.Settings.Default.LogPath, true);
                    //}
                    //LogFile.Write(DateTime.Now.ToString("G") + " - " + StatusMessage + "\r\n");
                    //LogFile.Close();
                    //LogFile.Dispose();
                    //LogFile = null;
                    //GC.Collect();
                }
                catch { }
            }            
        }

        public static void Log_CheckIn(int ProgramID)
        {
            string SQL;

            SQL = "UPDATE [NESTLOG].[dbo].Tb900_CheckIn_Log SET CheckInTime=getdate() WHERE Program_Id=" + ProgramID.ToString();

            using (newNestConn conn = new newNestConn())
            {
                conn.ExecuteNonQuery(SQL);
            }
        }

        public static void Log_Event(int ProgramID,int ProcessID, int EventCode, string Description)
        {
            string SQL;

            SQL = "INSERT INTO [NESTLOG].[dbo].[Tb901_Event_Log]" +
                  " ([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description])" +
                  " Values(" + ProgramID.ToString() + "," + ProcessID.ToString() + "," + 
                  EventCode.ToString() + ",getdate(),'" + Description + "')";

            Log.AddLogEntry(SQL);

            try
            {
                using (newNestConn conn = new newNestConn())
                {
                    conn.ExecuteNonQuery(SQL);
                }
                
            }
            catch (Exception e)
            {
                Log.AddLogEntry(e.ToString());
                throw e;
            }
        }
    }
}
