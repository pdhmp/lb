using System;
using System.Collections.Generic;
using System.Text;

namespace NestSim
{
    public class Performances_Table : Base_Table
    {

        public Performances_Table(string _Name, Price_Table pricesTable)
            : base(_Name, pricesTable.Id_Ticker_Template, pricesTable.iniDate, pricesTable.endDate)
        {
            ZeroFillFromComposite(pricesTable.Id_Ticker_Composite);
            Fill(pricesTable);
        }

        public Performances_Table(string _Name, Strategy _initialStrategy, int colCounter)
            : base(_Name, _initialStrategy.Id_Ticker_Template, _initialStrategy.iniDate, _initialStrategy.endDate)
        {
            ZeroFill(colCounter);
        }

        public void ZeroFill(int colCounter)
        {
            base.FillZeros(colCounter);
        }

        protected void Fill(Price_Table pricesTable)
        {
            for (int i = 1; i < pricesTable.DateRowCount; i++)
            {
                for (int j = 0; j < pricesTable.ValueColumnCount; j++)
                {
                    float curPerf = 0;
                    if (pricesTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = pricesTable.GetValue(i, j) / pricesTable.GetValue(i - 1, j) - 1;
                    }
                    SetValue(i, j, curPerf);
                }
            }
        }

    }
}
