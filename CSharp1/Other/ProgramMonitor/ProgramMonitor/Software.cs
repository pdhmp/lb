using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using NestDLL;


namespace ProgramMonitor
{
    class Software
    {
        private ProgramType _ProgType = ProgramType.Undefined; public ProgramType ProgType { get { return _ProgType; } }
        private int _ProgID = 0; public int ProgID { get { return _ProgID; } }
        private TimeSpan _OpenTime = new TimeSpan(0, 0, 0); public TimeSpan OpenTime { get { return _OpenTime; } }
        private TimeSpan _CloseTime = new TimeSpan(0, 0, 0); public TimeSpan CloseTime { get { return _CloseTime; } }
        private TimeSpan _RunInterval = new TimeSpan(0, 0, 0); public TimeSpan RunInterval { get { return _RunInterval; } }
        private string _ProgPath = ""; public string ProgPath { get { return _ProgPath; } }

        private TimeSpan _NotRespondingKillWait = new TimeSpan(0, 0, 0); public TimeSpan NotRespondingKillWait { get { return _NotRespondingKillWait; } }
        private DateTime _NotRespondingKillTime = new DateTime(1900, 01, 01); public DateTime NotRespondingKillTime { get { return _NotRespondingKillTime; } set { _NotRespondingKillTime = value; } }

        private Process _ProgProcess = null; public Process ProgProcess { get { return _ProgProcess; } }
        private string _ProcessName = ""; public string ProcessName { get { return _ProcessName; } }

        private TimeSpan _LastOpenTime = new TimeSpan(0, 0, 0); public TimeSpan LastOpenTime { get { return _LastOpenTime; } }
        private DateTime _LastOpenDate = new DateTime(1900, 1, 1); public DateTime LastOpenDate { get { return _LastOpenDate; } }

        public bool IsResponding
        {
            get
            {
                if (this.ProgProcess != null)
                {
                    return _ProgProcess.Responding;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                if ((this.ProgProcess != null))
                {
                    if (!this.ProgProcess.HasExited) { return true; } else { return false; }
                }
                else
                    return false;
            }
        }

        public bool LoadFromTextLine(string description)
        {
            try
            {
                char[] separator = new char[1]; separator[0] = '\t';

                string[] splitLine = description.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                if (splitLine.Count() == 5)
                {
                    switch (int.Parse(splitLine[0]))
                    {
                        case 0: _ProgType = ProgramType.KeepOpen; break;
                        case 1: _ProgType = ProgramType.RunInterval; break;
                        case 2: _ProgType = ProgramType.RunOnce; break;
                        default: break;
                    }
                    _OpenTime = new TimeSpan(int.Parse(splitLine[1].Substring(0, 2)), int.Parse(splitLine[1].Substring(2, 2)), 0);
                    _CloseTime = new TimeSpan(int.Parse(splitLine[2].Substring(0, 2)), int.Parse(splitLine[2].Substring(2, 2)), 0);

                    if (_ProgType == ProgramType.KeepOpen) _NotRespondingKillWait = TimeSpan.FromTicks(int.Parse(splitLine[3]) * 10000000);
                    if (_ProgType == ProgramType.RunInterval) _RunInterval = new TimeSpan(int.Parse(splitLine[3].Substring(0, 2)), int.Parse(splitLine[3].Substring(2, 2)), 0);
                    if (_ProgType == ProgramType.RunOnce)
                    {
                        _ProgID = int.Parse(splitLine[3]);
                        using (newNestConn curConn = new newNestConn())
                        {
                            DateTime lastDateTime = curConn.Return_DateTime("SELECT Top 1 Event_DateTime FROM NESTLOG.dbo.Tb901_Event_Log WHERE PROCESS_ID=" + _ProgID + "  AND Event_Code=2 ORDER BY Event_DateTime DESC ");
                            _LastOpenDate = lastDateTime.Date;
                        }
                    }

                    _ProgPath = splitLine[4];

                    string tempName = _ProgPath.Substring(_ProgPath.LastIndexOf('\\') + 1);
                    _ProcessName = tempName.Substring(0, tempName.IndexOf('.'));

                    setProcess();

                    return true;
                }
                else
                {
                    return false;
                }


                /*
                    
                char[] DefsSeparator = new char[1]; DefsSeparator[0] = '\n';
                char[] ValueSeparator = new char[1]; ValueSeparator[0] = ':';

                string[] Defs = description.Split(DefsSeparator);

                foreach (string curDef in Defs)
                {
                    if (curDef.Contains("MonitoringType"))
                    {
                        switch (curDef.Split(ValueSeparator)[1].Trim())
                        {
                            case "KeepOpen": _ProgType = ProgramTypes.KeepOpen; break;
                            case "RunInterval": _ProgType = ProgramTypes.RunInterval; break;
                            case "RunOnce": _ProgType = ProgramTypes.RunOnce; break;
                        }
                    }

                    if (curDef.Contains("OpenTime"))
                    {
                        _OpenTime = new TimeSpan(int.Parse(curDef.Split(ValueSeparator)[1].Trim().Substring(0, 2)), int.Parse(curDef.Split(ValueSeparator)[1].Trim().Substring(2, 2)), 0);
                    }
                    if(curDef.Contains("CloseTime"))
                    {
                        _CloseTime = new TimeSpan(int.Parse(curDef.Split(ValueSeparator)[1].Trim().Substring(0, 2)), int.Parse(curDef.Split(ValueSeparator)[1].Trim().Substring(2, 2)), 0);
                    }
                }

                foreach (string curDef in Defs)
                {                        
                    if (curDef.Contains("NotRespondingAliveTime") && _ProgType == ProgramTypes.KeepOpen)
                    {
                        this._NotRespondingKillWait = new TimeSpan(int.Parse(curDef.Split(ValueSeparator)[1].Trim()));
                    }
                    if(curDef.Contains("IntervalTime") && _ProgType == ProgramTypes.RunInterval)
                    {
                        this._RunInterval = new TimeSpan(int.Parse(curDef.Split(ValueSeparator)[1].Trim()));
                    }
                    if (curDef.Contains("ProgramID") && _ProgType == ProgramTypes.RunOnce)
                    {
                        this._ProgID = int.Parse(curDef.Split(ValueSeparator)[1].Trim());
                    }   
                        
                }

                */



            }
            catch
            {
                return false;
            }
        }

        public bool CheckTime(TimeSpan TimeToCheck)
        {
            if (TimeToCheck > this.OpenTime && TimeToCheck < this.CloseTime)
                return true;
            else
                return false;
        }

        public bool Start()
        {
            return Start(false);
        }

        public bool Start(bool ForceOpen)
        {
            if ((DateTime.Now.TimeOfDay > _OpenTime && DateTime.Now.TimeOfDay < _CloseTime) || ForceOpen)
            {
                if (!this.IsOpen)
                {
                    try
                    {
                        Process.Start(_ProgPath);

                        System.Threading.Thread.Sleep(3000);

                        setProcess();

                        _LastOpenTime = DateTime.Now.TimeOfDay;
                        _LastOpenDate = DateTime.Now.Date;
                        AddLog(" STARTED \t" + this.ToString());
                        return true;
                    }
                    catch (Exception curErr)
                    {
                        MessageBox.Show(this.ProgPath + "\r\n" + "\r\n" + "ERROR: " + curErr.ToString() + "\r\n");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        public override string ToString()
        {
            return _ProgPath.Substring(_ProgPath.LastIndexOf('\\') + 1);
        }

        public bool Kill()
        {
            if (IsOpen)
            {
                this.ProgProcess.Kill();
                AddLog(" KILLED \t\t" + this.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool setProcess()
        {
            Process[] tempProcess = Process.GetProcessesByName(_ProcessName);
            if (tempProcess.Length > 0)
            {
                _ProgProcess = tempProcess[0];
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
