using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
{
    public class Performances_Table : Base_Table
    {
        Price_Table _PriceTable;

        public Performances_Table(string _Name, Price_Table pricesTable)
            : base(_Name, pricesTable.Id_Ticker_Template, pricesTable.iniDate, pricesTable.endDate, pricesTable.IsRealTime)
        {
            FillZeros(pricesTable);
            Fill(pricesTable);
        }

        public Performances_Table(string _Name, Strategy _initialStrategy, int colCounter)
            : base(_Name, _initialStrategy.Id_Ticker_Template, _initialStrategy.iniDate, _initialStrategy.endDate, _initialStrategy.IsRealTime)
        {
            ZeroFill(colCounter);
        }

        public void ZeroFill(int colCounter)
        {
            base.FillZeros(colCounter);
        }

        protected void Fill(Price_Table pricesTable)
        {
            _PriceTable = pricesTable;

            for (int i = 1; i < pricesTable.DateRowCount; i++)
            {
                for (int j = 0; j < pricesTable.ValueColumnCount; j++)
                {
                    double curPerf = 0;
                    if (pricesTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = pricesTable.GetValue(i, j) / pricesTable.GetValue(i - 1, j) - 1;
                    }
                    SetValue(i, j, curPerf);
                }
            }
        }

        protected override void UpdateTable()
        {            
            int curDate = DateRowCount - 1;

            for (int j = 0; j < ValueColumnCount; j++)
            {
                double curPerf = 0;
                if (_PriceTable.GetValue(curDate - 1, j) != 0)
                {
                    curPerf = _PriceTable.GetValue(curDate, j) / _PriceTable.GetValue(curDate - 1, j) - 1;
                }
                SetValue(curDate, j, curPerf);
            }
        }
    }
}
