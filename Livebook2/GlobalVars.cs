
namespace LiveBook
{
    using System;
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();

        public bool appStarting = false;
        public bool appClosing = false;
        public bool appCloseTimerFired = false;
        public int IdUser;

        GlobalVars()
        {
        }

        public static GlobalVars Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalVars();
                    }
                    return instance;
                }
            }
        }

        public double TryParse_Double(object Value)
        {
            double ReturnValue = 0;
            double.TryParse(Value.ToString(), out ReturnValue);
            return ReturnValue;
        }

        public int TryParse_Int(object Value)
        {
            int ReturnValue = 0;
            int.TryParse(Value.ToString(), out ReturnValue);
            return ReturnValue;
        }

        public DateTime TryParse_Datetime(object Value)
        {
            DateTime ReturnValue;
            DateTime.TryParse(Value.ToString(), out ReturnValue);
            return ReturnValue;
        }

        public float TryParse_Float(object Value)
        {
            float ReturnValue;
            float.TryParse(Value.ToString(), out ReturnValue);
            return ReturnValue;
        }
    }
}
