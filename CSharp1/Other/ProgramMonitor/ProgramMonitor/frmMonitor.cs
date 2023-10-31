using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NestDLL;

namespace ProgramMonitor
{
    public partial class frmMonitor : Form
    {
        List<Software> KeepOpen = new List<Software>();
        List<Software> RunInterval = new List<Software>();
        List<Software> RunOnce = new List<Software>();
        static string curMessage;

        string curfileName = @"c:\temp\monitorDefs.txt";
        bool IsRunning = false;
        bool DefsLoaded = false;
        bool loadOnOpen = true;
        int LoadCount = 0;

        public frmMonitor()
        {
            InitializeComponent();
            tmrCheckPrograms.Start();
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            LoadDefsFile();

            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
            IsRunning = true;
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            cmdStop.Enabled = false;
            cmdStart.Enabled = true;
            IsRunning = false;
        }

        private void cmdOpenDefs_Click(object sender, EventArgs e)
        {
            try
            {
               Process.Start("c:\\temp\\monitorDefs.txt");
            }
            catch
            {
                MessageBox.Show("File not Found!");
            }
        }

        private void tmrCheckPrograms_Tick(object sender, EventArgs e)
        {
            txtLog.Text = curMessage;

            if (this.IsRunning)
            {
                CheckPrograms(ProgramType.KeepOpen);
                CheckPrograms(ProgramType.RunInterval);
                CheckPrograms(ProgramType.RunOnce);

                CheckClosePrograms(KeepOpen);
                CheckClosePrograms(RunInterval);
                CheckClosePrograms(RunOnce);
            }

            if (loadOnOpen)
            {
                LoadCount++;

                if (LoadCount > 2)
                {
                    cmdStart_Click(this, new EventArgs());
                    loadOnOpen = false;
                }
            }
            CloseProgramFromPeding(KeepOpen);
            CloseProgramFromPeding(RunInterval);
            CloseProgramFromPeding(RunOnce);
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            cmdStart_Click(sender, e);
            this.Hide();

        }

        private void frmMonitor_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void LoadDefsFile()
        {
            if (!DefsLoaded)
            {
                if (File.Exists(curfileName))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader("c:\\temp\\monitorDefs.txt"))
                        {
                            string curLine;

                            while ((curLine = sr.ReadLine()) != null)
                            {
                                if (curLine != "")
                                {
                                    if (curLine.Substring(0, 1) != "#")
                                    {
                                        Software curSoftware = new Software();
                                        if (curSoftware.LoadFromTextLine(curLine))
                                        {
                                            AddLog(" LOADED \t" + curSoftware.ToString());
                                            switch (curSoftware.ProgType)
                                            {
                                                case ProgramType.KeepOpen: KeepOpen.Add(curSoftware); break;
                                                case ProgramType.RunInterval: RunInterval.Add(curSoftware); break;
                                                case ProgramType.RunOnce: RunOnce.Add(curSoftware); break;
                                                default:
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }

                            sr.Close();
                            DefsLoaded = true;
                        }
                    }
                    catch
                    {
                        AddLog(" FILE ERROR \t" + curfileName);
                    }
                }
                else
                {
                    AddLog(" FILE NOT FOUND \t" + curfileName);
                }

                txtLog.Text = curMessage;
            }
        }

        private void CheckPrograms(ProgramType ProgramType)
        {
            switch (ProgramType)
            {
                case ProgramType.KeepOpen:
                    foreach (Software curSoftware in KeepOpen)
                    {
                        if (curSoftware.CheckTime(DateTime.Now.TimeOfDay))
                        {
                            if (!curSoftware.IsOpen)
                            {
                                AddLog(" NOT RUNNING \t" + curSoftware.ToString());
                                if (!curSoftware.Start())
                                {
                                    cmdStop_Click(this, new EventArgs());
                                }
                            }
                            else if (!curSoftware.IsResponding)
                            {
                                if (curSoftware.NotRespondingKillTime == new DateTime(1900, 01, 01))
                                {
                                    AddLog(" NOT RESPONDING \t" + curSoftware.ToString());
                                    curSoftware.NotRespondingKillTime = DateTime.Now.Add(curSoftware.NotRespondingKillWait);
                                }
                            }
                            else
                            {
                                //AddLog(" OK \t\t" + curSoftware.IsResponding.ToString() + " - " + curSoftware.ToString());
                                curSoftware.NotRespondingKillTime = new DateTime(1900, 01, 01);
                            }
                        }
                    }
                    break;
                case ProgramType.RunInterval:
                    foreach (Software curSoftware in RunInterval)
                    {
                        if (curSoftware.CheckTime(DateTime.Now.TimeOfDay))
                        {
                            if (!curSoftware.IsOpen)
                            {
                                if (DateTime.Now.TimeOfDay > curSoftware.LastOpenTime.Add(curSoftware.RunInterval) || curSoftware.LastOpenDate != DateTime.Now.Date)
                                    curSoftware.Start();
                            }
                        }
                    }
                    break;
                case ProgramType.RunOnce:
                    foreach (Software curSoftware in RunOnce)
                    {
                        if (curSoftware.CheckTime(DateTime.Now.TimeOfDay))
                        {
                            if (!curSoftware.IsOpen)
                            {
                                if (curSoftware.LastOpenDate != DateTime.Now.Date)
                                    curSoftware.Start();
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void CheckClosePrograms(List<Software> curProgramList)
        {
            foreach (Software curSoftware in curProgramList)
            {
                if (curSoftware.IsOpen)
                {
                    if (DateTime.Now.TimeOfDay > curSoftware.CloseTime)
                    {
                        curSoftware.Kill();
                    }
                    if (DateTime.Now > curSoftware.NotRespondingKillTime.AddMinutes(5) && curSoftware.NotRespondingKillTime != new DateTime(1900, 01, 01))
                    {
                        curSoftware.Kill();
                        curSoftware.NotRespondingKillTime = new DateTime(1900, 01, 01);
                    }
                }
            }
        }

        public static void AddLog(string LogToAdd)
        {
            curMessage = DateTime.Now.ToString("dd-MMM HH:mm:ss") + "\t" + LogToAdd + "\r\n" + curMessage;
        }

        private void RestorerApplication()
        {
            if (!this.Visible)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void CloseProgramFromPeding(List<Software> curProgramList)
        {
            string StringSQL;
            int ProgIdRunning;
            int ProgIdDB;

            StringSQL = "SELECT distinct(Id_Ticker) FROM dbo.Tb207_Pending  Where Source = 4 And [Status] = 1";

            using (newNestConn curConn = new newNestConn())
            {
                DataTable curTable = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in curTable.Rows)
                {
                    if (int.TryParse(curRow["Id_Ticker"].ToString(), out ProgIdDB))
                    {
                        if (curProgramList.Count > 0)
                        {
                            for (int Count = 0; Count < curProgramList.Count - 1; Count++)
                            {
                                ProgIdRunning = Convert.ToInt32(curProgramList[Count].ProgID.ToString());

                                if (ProgIdDB == ProgIdRunning)
                                {
                                    curProgramList[Count].Kill();
                                }

                            }
                        }
                    }
                }
            }

        }

        private void nfiMonitor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestorerApplication();
        }

    }

    public enum ProgramType
    {
        Undefined,
        KeepOpen,
        RunInterval,
        RunOnce
    }
}
