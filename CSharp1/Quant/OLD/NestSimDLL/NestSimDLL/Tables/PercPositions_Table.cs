using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
{
    public class PercPositions_Table : Base_Table
    {
        private Price_Table _SignalTable;
        private Weight_Table _WeightTable;


        public PercPositions_Table(Signal_Table signalTable, Weight_Table weightTable)
            : base("subPositions", weightTable.Id_Ticker_Template, weightTable.iniDate, weightTable.endDate, weightTable.IsRealTime)
        {
            Fill(signalTable, weightTable);
        }

        public PercPositions_Table(string _Name, Strategy _initialStrategy, int colCounter)
            : base(_Name, _initialStrategy.Id_Ticker_Template, _initialStrategy.iniDate, _initialStrategy.endDate,_initialStrategy.IsRealTime)
        {
            ZeroFill(colCounter);
        }

        public void ZeroFill(int colCounter)
        {
            _ValueColumnType = Utils.TableTypes.IdSeq_Strategy;
            FillZeros(colCounter);
        }

        public void ZeroFill(Weight_Table weightTable)
        {
            FillZeros(weightTable);
        }

        public void Fill(Signal_Table signalTable, Weight_Table weightTable)
        {
            _Id_Ticker_Composite = signalTable.Id_Ticker_Composite;
            _ValueColumnType = weightTable.ValueColumnType;

            _SignalTable = signalTable;
            _WeightTable = weightTable;
            
            ZeroFill(weightTable);

            for (int i = 1; i < DateRowCount; i++)
            {
                for (int j = 0; j < weightTable.ValueColumnCount; j++)
                {
                    double curPos = 0;
                    if (signalTable.GetValue(i, j) != 0)
                    {
                        curPos = signalTable.GetValue(i, j) * weightTable.GetValue(i , j);
                    }
                    SetValue(i, j, curPos);
                }
            }
        }
        
        public void AddRowCountLong()
        {
            int curCol = AddCustomColumn("COUNTLONG");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] > 0) { tempTotal++; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddRowCountShort()
        {
            int curCol = AddCustomColumn("COUNTSHORT");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] < 0) { tempTotal++; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddRowGross()
        {
            int curCol = AddCustomColumn("GROSS");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] != 0) { tempTotal = tempTotal + Math.Abs(Values[j][i]); };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddRowLong()
        {
            int curCol = AddCustomColumn("LONG");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] > 0) { tempTotal = tempTotal + Values[j][i]; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddRowShort()
        {
            int curCol = AddCustomColumn("SHORT");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] < 0) { tempTotal = tempTotal + Values[j][i]; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddRowNet()
        {
            int curCol = AddCustomColumn("NET");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] != 0) { tempTotal = tempTotal + Values[j][i]; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        protected override void UpdateTable()
        {
            int i = DateRowCount - 1;

            for (int j = 0; j < _WeightTable.ValueColumnCount; j++)
            {
                double curPos = 0;
                if (_SignalTable.GetValue(i, j) != 0)
                {
                    curPos = _SignalTable.GetValue(i, j) * _WeightTable.GetValue(i, j);
                }
                SetValue(i, j, curPos);
            }            
        }
    }
}
