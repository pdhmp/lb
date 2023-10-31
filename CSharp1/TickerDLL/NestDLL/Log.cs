using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TickerDLL
{
    public class Log
    {
        static object FileWriteLock = new object();

        public static void AddLogEntry(string StatusMessage,string LogPath)
        {
           StreamWriter LogFile = null;
           
           try
           {
               if (LogFile == null)
               {
                   LogFile = new StreamWriter(LogPath, true);
               }
               LogFile.Write(DateTime.Now.ToString("G") + " - " + StatusMessage + "\r\n");
               LogFile.Dispose();
               LogFile = null;
               GC.Collect();
           }
           catch(Exception ex) 
           { 
               throw ex; 
           }
            
        }

        public static void Log_CheckIn(int ProgramID)
        {
            string SQL;

            SQL = "UPDATE NESTLOG.[dbo].Tb900_CheckIn_Log SET CheckInTime=getdate() WHERE Program_Id=" + ProgramID.ToString();

            try
            {
                using (newNestConn conn = new newNestConn())
                {
                    conn.ExecuteNonQuery(SQL);
                }
            }
            catch (Exception e)
            {
                Error_Dump_DLL(e.ToString(), "Log Event DLL");
            }
        }

        public static void Log_Event(int Program_ID, int Process_ID, int Event_Code, string Description)
        {
            Log_Event(Program_ID, Process_ID, Event_Code, Description, "NULL");
        }

        public static void Log_Event(int ProgramID, int ProcessID, int EventCode, string Description, string Relevant_Id)
        {
            string SQL;

            SQL = "INSERT INTO NESTLOG.[dbo].[Tb901_Event_Log]" +
                  " ([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description],[Relevant_ID])" +
                  " Values(" + ProgramID.ToString() + "," + ProcessID.ToString() + "," + 
                  EventCode.ToString() + ",getdate(),'" + Description + "'," + Relevant_Id + ")";

             try
            {
                using (newNestConn conn = new newNestConn())
                {
                    conn.ExecuteNonQuery(SQL);
                }
            }
            catch (Exception e)
            {
                Error_Dump_DLL(e.ToString(), "Log Event DLL");
            }
        }

        public static int Error_Dump_DLL(string MsgError, string Source)
        {
            lock (FileWriteLock)
            {
                string user = Environment.UserName.ToString();
                string Local_Machine = "";//= Environment.MachineName.ToString();

                string strPath = @"C:\Log\LiveBook\DLL_Error_" + user + "_" + Local_Machine + ".txt";

                FileInfo curFileInfo = new FileInfo(strPath);
                StreamWriter sw;

                if (!curFileInfo.Exists)
                {
                    curFileInfo.Create().Close();
                }

                sw = curFileInfo.AppendText();
                sw.WriteLine("Error DateTime - " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                sw.WriteLine("Source - " + Source.ToString());
                sw.WriteLine(MsgError.ToString());
                sw.WriteLine("---------/n");

                sw.Flush();
                sw.Close();
            }
            return 1;
        }

        public static DateTime GetLastRunDate(int Process_Id)
        {
            string LastRun = "";

            using (newNestConn conn = new newNestConn())
            {
                LastRun = conn.Execute_Query_String("SELECT MAX(Event_DateTime) from NESTSRV06.NESTLOG.dbo.Tb901_Event_Log WHERE Event_Code=2 AND Process_Id=" + Process_Id);
            }
            if (LastRun != "")
            {
                return Convert.ToDateTime(LastRun);
            }
            else 
            {
                return new DateTime(1900, 01, 01);
            }
        }
    
    }
}
