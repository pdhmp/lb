using System;
using System.Collections.Generic;
using System.Text;

namespace NestSim
{
    public class Contributions_Table : Base_Table
    {

        public Contributions_Table(string _Name, PercPositions_Table weightTable, Performances_Table perfTable)
            : base(_Name, weightTable.Id_Ticker_Template, weightTable.iniDate, weightTable.endDate)
        {
            Fill(weightTable, perfTable);
        }

        public Contributions_Table(string _Name, Strategy _initialStrategy, int colCounter)
            : base(_Name, _initialStrategy.Id_Ticker_Template, _initialStrategy.iniDate, _initialStrategy.endDate)
        {
            FillZeros(colCounter);
        }

        public void Fill(PercPositions_Table weightTable, Performances_Table perfTable)
        {
            FillZeros(weightTable.ValueColumnCount);
            for (int i = 1; i < DateRowCount; i++)
            {
                for (int j = 0; j < weightTable.ValueColumnCount; j++)
                {
                    float curPerf = 0;
                    if (weightTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = weightTable.GetValue(i - 1, j) * perfTable.GetValue(i, j);
                    }
                    SetValue(i, j, curPerf);
                }
            }
            AddCustomSum();
            ValueFormat = "0.00%; -0.00%; -"; 
        }
    }
}
