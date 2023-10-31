using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
{
    public class Contributions_Table : Base_Table
    {
        private PercPositions_Table _WeightTable;
        private Performances_Table _PerfTable;
        private bool _InnerStrategy;

        public Contributions_Table(string _Name, PercPositions_Table weightTable, Performances_Table perfTable)
            : base(_Name, weightTable.Id_Ticker_Template, weightTable.iniDate, weightTable.endDate, weightTable.IsRealTime)
        {
            Fill(weightTable, perfTable);
        }

        public Contributions_Table(string _Name, PercPositions_Table weightTable, Performances_Table perfTable, bool innerStrategy)
            : base(_Name, weightTable.Id_Ticker_Template, weightTable.iniDate, weightTable.endDate, weightTable.IsRealTime)
        {
            Fill(weightTable, perfTable, innerStrategy);
        }

        public Contributions_Table(string _Name, Strategy _initialStrategy, int colCounter)
            : base(_Name, _initialStrategy.Id_Ticker_Template, _initialStrategy.iniDate, _initialStrategy.endDate, _initialStrategy.IsRealTime)
        {
            FillZeros(colCounter);
        }

        public void Fill(PercPositions_Table weightTable, Performances_Table perfTable)
        {
            Fill(weightTable, perfTable, false);
        }

        public void Fill(PercPositions_Table weightTable, Performances_Table perfTable, bool innerStrategy)
        {
            _WeightTable = weightTable;
            _PerfTable = perfTable;
            _InnerStrategy = innerStrategy;

            int window = 1;

            if (innerStrategy)
                window = 0;

            FillZeros(weightTable);

            for (int i = window; i < DateRowCount; i++)
            {
                for (int j = 0; j < weightTable.ValueColumnCount; j++)
                {
                    double curPerf = 0;
                    if (weightTable.GetValue(i - window, j) != 0)
                    {
                        curPerf = weightTable.GetValue(i - window, j) * perfTable.GetValue(i, j);
                    }
                    SetValue(i, j, curPerf);
                }
            }
            AddRowSum(false);
            ValueFormat = "0.00%; -0.00%; -"; 
        }

        protected override void UpdateTable()
        {
            int window = 1;

            if (_InnerStrategy)
                window = 0;

            int i = DateRowCount - 1;

            for (int j = 0; j < _WeightTable.ValueColumnCount; j++)
            {
                double curPerf = 0;
                if (_WeightTable.GetValue(i - window, j) != 0)
                {
                    curPerf = _WeightTable.GetValue(i - window, j) * _PerfTable.GetValue(i, j);
                }
                SetValue(i, j, curPerf);
            }

            RefreshRowSum(false);
            
            
        }
    }
}
