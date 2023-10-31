using System;

namespace NestQuant.Common
{
    public class ExecutionFillArgs : EventArgs
    {
        public int ID_Ticker;
        public int LastShares;
        public int CumQty;
        public int LeavesQty;
        public int OrderQty;

        /// <summary>
        /// Creates instance of ExecutionFillArgs
        /// </summary>
        public ExecutionFillArgs(int ID_Ticker, int OrderQty, int CumQty, int LeavesQty, int LastShares)
        {
            this.ID_Ticker = ID_Ticker;
            this.OrderQty = OrderQty;
            this.CumQty = CumQty;
            this.LeavesQty = LeavesQty;
            this.LastShares = LastShares;
        }//ExecutionFillArgs(int ID_Ticker, int OrderQty, int CumQty, int LeavesQty, int LastShares)            
    }//public class ExecutionFillArgs    
}
