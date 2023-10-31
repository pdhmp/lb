using System;
using System.Collections.Generic;
using System.Text;

namespace NestSim
{
    public class PercPositions_Table : Base_Table
    {
        public PercPositions_Table(Price_Table signalTable, Weight_Table weightTable)
            : base(weightTable.Name, weightTable.Id_Ticker_Template, weightTable.iniDate, weightTable.endDate)
        {
            Fill(signalTable, weightTable);
        }

        public PercPositions_Table(string _Name, Strategy _initialStrategy, int colCounter)
            : base(_Name, _initialStrategy.Id_Ticker_Template, _initialStrategy.iniDate, _initialStrategy.endDate)
        {
            ZeroFill(colCounter);
        }

        public void ZeroFill(int colCounter)
        {
            FillZeros(colCounter);
        }

        public void Fill(Price_Table signalTable, Weight_Table weightTable)
        {
            ZeroFill(weightTable.ValueColumnCount);
            for (int i = 1; i < DateRowCount; i++)
            {
                for (int j = 0; j < weightTable.ValueColumnCount; j++)
                {
                    float curPerf = 0;
                    if (signalTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = signalTable.GetValue(i - 1, j) * weightTable.GetValue(i - 1, j);
                    }
                    SetValue(i, j, curPerf);
                }
            }
        }
    }
}
