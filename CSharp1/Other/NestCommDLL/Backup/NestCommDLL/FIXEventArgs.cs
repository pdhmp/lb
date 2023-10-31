using System;

namespace NestCommDLL
{
    public class ExecutionFillArgs : EventArgs
    {
        public int ID_Ticker;
        public int LastShares;
        public int CumQty;
        public int LeavesQty;
        public int OrderQty;
        public string ClOrdID;

        /// <summary>
        /// Creates instance of ExecutionFillArgs
        /// </summary>
        public ExecutionFillArgs(string ClOrdID, int ID_Ticker, int OrderQty, int CumQty, int LeavesQty, int LastShares)
        {
            this.ClOrdID = ClOrdID;
            this.ID_Ticker = ID_Ticker;
            this.OrderQty = OrderQty;
            this.CumQty = CumQty;
            this.LeavesQty = LeavesQty;
            this.LastShares = LastShares;
        }//ExecutionFillArgs(int ID_Ticker, int OrderQty, int CumQty, int LeavesQty, int LastShares)            
    }//public class ExecutionFillArgs    
}
