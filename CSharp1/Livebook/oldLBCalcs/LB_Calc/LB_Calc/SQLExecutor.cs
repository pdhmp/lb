using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NestDLL;

namespace LB_Hist_Update
{
    class SQLExecutor
    {
        System.Windows.Forms.Timer curTimer = new System.Windows.Forms.Timer();
        System.Threading.Thread ExecThread;

        private int _ThreadCounter;
        public int ThreadCounter { get { return _ThreadCounter; } }

        private string _LogFileName;
        public string LogFileName { get { return _LogFileName; } }

        private int _LoopInterval;
        public int LoopInterval { get { return curTimer.Interval; } }

        private string _SQLExpression;
        public string SQLExpression { get { return _SQLExpression; } }

        private bool _IsRunning;
        public bool IsRunning { get { return _IsRunning; } }

        private bool _IsTimerOn;
        public bool IsTimerOn { get { return _IsTimerOn; } }

        private string _LastRunResult;
        public string LastRunResult { get { return _LastRunResult; } }

        private DateTime _LastRunDateTime;
        public DateTime LastRunDateTime { get { return _LastRunDateTime; } }

        private int _AvgRunTimeTaken;
        public int AvgRunTimeTaken { get { return _AvgRunTimeTaken; } }

        public SQLExecutor(string __SQLExpression, int __LoopInterval, string __LogFileName)
        {
            _LoopInterval = __LoopInterval;
            curTimer.Interval = __LoopInterval;
            _SQLExpression = __SQLExpression;
            _LogFileName = __LogFileName;
            curTimer.Tick += new System.EventHandler(curTimer_Tick);
        }

        public void StartLoop()
        {
            curTimer.Start();
            _IsTimerOn = true;
        }

        public void StopLoop()
        {
            curTimer.Stop();
            _IsTimerOn = false;
        }

        public void RunNow()
        {
            curTimer_Tick(this, new EventArgs());
        }

        public void ChangeInterval(int newInterval)
        {
            curTimer.Interval = newInterval;
        }


        private void curTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_IsRunning)
                {
                    ExecThread = new System.Threading.Thread(new ThreadStart(ExecSQL));
                    ExecThread.Start();
                }
            }
            catch (Exception Excep)
            {
                NestDLL.Log.AddLogEntry(Excep.ToString(), LogFileName);
                Application.Exit();
            }
        }

        private void ExecSQL()
        {
            try
            {
                _ThreadCounter++;
                _IsRunning = true;

                using (NestDLL.old_Conn curConn = new NestDLL.old_Conn())
                {
                    DateTime StartTime = DateTime.Now;
                    string retorno = curConn.Execute_Query_String(SQLExpression);
                    TimeSpan TimeTaken = DateTime.Now.Subtract(StartTime);
                    _AvgRunTimeTaken = (int)TimeTaken.TotalMilliseconds;

                    if (retorno != "OK")
                    {
                        _LastRunResult = retorno.Replace('\n', ' ').Replace('\r', ' ');
                    }
                    else
                    {
                        _LastRunResult = "Service is OK!";
                    }
                }
                _IsRunning = false;
                _LastRunDateTime = DateTime.Now;


                //_AvgRunTimeTaken = ((_AvgRunTimeTaken * 2) + (int)TimeTaken.TotalMilliseconds) / 3;
                _ThreadCounter--;
                 
            }
            catch (Exception Excep)
            {
                NestDLL.Log.AddLogEntry(Excep.ToString(), LogFileName);
                //Application.Exit();
            }
        }

    }
}
