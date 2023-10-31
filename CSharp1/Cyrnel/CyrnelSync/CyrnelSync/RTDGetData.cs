using System;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;
using Cyrnel.Hub.ExcelAddIn;
using Microsoft.Office.Interop.Excel;

namespace CyrnelSync
{
    public class RTDGetData : Microsoft.Office.Interop.Excel.IRTDUpdateEvent
    {

        public event EventHandler NewDataReceived;

        void IRTDUpdateEvent.Disconnect()
        {
            Console.WriteLine("Disconnected!!!");
        }

        int _HeartbeatInterval = 1;

        int IRTDUpdateEvent.HeartbeatInterval
        {
            get
            {
                return _HeartbeatInterval;
            }
            set
            {
                _HeartbeatInterval = value;
            }
        }

        void IRTDUpdateEvent.UpdateNotify()
        {
            if (NewDataReceived != null) NewDataReceived(this, new EventArgs());
            //Console.WriteLine("New Data");
        }
    }
}
