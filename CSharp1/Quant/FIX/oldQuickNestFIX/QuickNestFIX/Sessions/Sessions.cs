using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNestFIX
{
    public class Sessions
    {
        private string _SessionType;
        public string SessionType
        {
            get { return _SessionType; }
            set { _SessionType = value; }
        }

        private string _Session;
        public string Session
        {
            get { return _Session; }
            set { _Session = value; }
        }

        private string _SessionStatus;
        public string SessionStatus
        {
            get { return _SessionStatus; }
            set { _SessionStatus = value; }
        }

    }
}
