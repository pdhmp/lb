using System;
using System.Windows.Forms;
using System.Reflection;

namespace NestSim
{
    public static class Utils
    {
        #region Enums

        public enum TableFillTypes { FillZero, FillPrevious, FillNegVal }

        public enum TimeIntervals { IntervalYear, IntervalQuarter, IntervalMonth, IntervalDay, Interval15mins, Undefined }

        public enum StrategyTypes { SectorValue, HeadAndShoulder, Undefined }

        public enum TableTypes { Id_Ticker, IdSeq_Strategy, Undefined }

        public enum WeighingSchemes { FromComposite, EqualWeighed, Undefined }

        #endregion

        public static System.Array ResizeArray(System.Array oldArray, int newSize)
        {
            int oldSize = 0;
            if (oldArray != null)
            {
                oldSize = oldArray.Length;
                System.Type elementType = oldArray.GetType().GetElementType();
                System.Array newArray = System.Array.CreateInstance(elementType, newSize);
                int preserveLength = System.Math.Min(oldSize, newSize);
                if (preserveLength > 0)
                    System.Array.Copy(oldArray, newArray, preserveLength);
                return newArray;
            }
            else
            {
                return null;
            }
        }

        public static float calcMedian(float[] curArray)
        {
            int TotalItems = curArray.Length;
            int FirstNonZero = 0;

            for (int i = 0; i < TotalItems; i++)
            {
                if (curArray[i] != 0) { FirstNonZero = i; break; };
            }

            int FilledItems = TotalItems - FirstNonZero;

            if (FilledItems > 1)
            {
                if (FilledItems % 2 == 0)
                {
                    return (curArray[FirstNonZero + (FilledItems + 1) / 2] + curArray[FirstNonZero + (FilledItems - 1) / 2]) / 2;
                }
                else
                {
                    return curArray[FirstNonZero + FilledItems / 2];
                }
            }
            else
            {
                return curArray[FirstNonZero];
            }
        }
        /*
        public static void Array_CalcPerformances(Price_Table perfTable, Price_Table PTable)
        {
            for (int i = 1; i < PTable.DateRowCount; i++)
            {
                for (int j = 0; j < PTable.ValueColumnCount; j++)
                {
                    float curPerf = 0;
                    if (PTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = PTable.GetValue(i, j) / PTable.GetValue(i - 1, j) - 1;
                    }
                    perfTable.SetValue(i, j, curPerf);
                }
            }
        }
        
        public static void Array_CalcContributions(Price_Table contribTable, Price_Table signalTable, Weight_Table weightTable, Price_Table perfTable)
        {
            for (int i = 1; i < contribTable.DateRowCount; i++)
            {
                for (int j = 0; j < contribTable.ValueColumnCount; j++)
                {
                    float curPerf = 0;
                    if (signalTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = signalTable.GetValue(i - 1, j) * weightTable.GetValue(i - 1, j) * perfTable.GetValue(i, j);
                    }
                    contribTable.SetValue(i, j, curPerf);
                }
            }
        }
        
        public static void Array_CalcBlockWeights(Weight_Table returnTable, Price_Table signalTable, Weight_Table weightTable)
        {
            for (int i = 1; i < returnTable.DateRowCount; i++)
            {
                for (int j = 0; j < returnTable.ValueColumnCount; j++)
                {
                    float curPerf = 0;
                    if (signalTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = signalTable.GetValue(i - 1, j) * weightTable.GetValue(i - 1, j);
                    }
                    returnTable.SetValue(i, j, curPerf);
                }
            }
        }
*/
        #region ArrayOperations

        public class Tables
        {
            public static bool CompareLines(Base_Table ArrA, Base_Table ArrB)
            {
                bool IsCompValid = true;

                if (ArrA.Id_Ticker_Template != ArrB.Id_Ticker_Template) { IsCompValid = false; };
                if (ArrA.DateRowInterval != ArrB.DateRowInterval) { IsCompValid = false; };
                if (ArrA.DateRowCount != ArrB.DateRowCount) { IsCompValid = false; };
                if (ArrA.iniDate != ArrB.iniDate) { IsCompValid = false; };
                if (ArrA.endDate != ArrB.endDate) { IsCompValid = false; };

                if (IsCompValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            public static bool Compare(Base_Table ArrA, Base_Table ArrB)
            {
                bool IsCompValid = true;
                bool colmatch = true;

                if (ArrA.Id_Ticker_Template != ArrB.Id_Ticker_Template) { IsCompValid = false; };
                if (ArrA.DateRowInterval != ArrB.DateRowInterval) { IsCompValid = false; };
                if (ArrA.DateRowCount != ArrB.DateRowCount) { IsCompValid = false; };
                if (ArrA.ValueColumnCount != ArrB.ValueColumnCount) { IsCompValid = false; };

                for (int i = 0; i < ArrA.ValueColumnCount; i++)
                {
                    if (ArrA.ValueColumns[i] != ArrB.ValueColumns[i]) { colmatch = false; break; }
                }

                if (!colmatch) { IsCompValid = false; }

                if (IsCompValid)
                    return true;
                else
                    return false;

            }

            public static Base_Table Sum(Base_Table ArrA, Base_Table ArrB)
            {
                Base_Table Array_Sum = ArrA;
                if (Compare(ArrA, ArrB))
                {
                    for (int i = 0; i < ArrA.DateRowCount; i++)
                    {
                        for (int j = 0; j < ArrA.ValueColumnCount; j++)
                        {
                            Array_Sum.SetCustomValue(i, j, ArrA.GetCustomValue(i, j) + ArrB.GetCustomValue(i, j));
                        }
                    }
                }

                return Array_Sum;
            }

            public static Base_Table Multiply(Base_Table ArrA, Base_Table ArrB)
            {
                Base_Table arrMult = ArrA;
                if (Compare(ArrA, ArrB))
                {
                    for (int i = 0; i < ArrA.DateRowCount; i++)
                    {
                        for (int j = 0; j < ArrA.ValueColumnCount; j++)
                        {
                            arrMult.SetCustomValue(i, j, ArrA.GetCustomValue(i, j) * ArrB.GetCustomValue(i, j));
                        }
                    }
                }
                return arrMult;

            }

            public static Price_Table SetZero(Price_Table PTable, Base_Table TemplateTable)
            {
                Price_Table newPTable = PTable;
                if (Compare(PTable, TemplateTable))
                {
                    for (int i = 0; i < PTable.DateRowCount; i++)
                    {
                        for (int j = 0; j < PTable.ValueColumnCount; j++)
                        {
                            if (TemplateTable.GetValue(i, j) == 0)
                            {
                                newPTable.SetValue(i, j, 0);
                            };
                        }
                    }
                }
                return newPTable;
            }

            public static Price_Table FixNegPEs(Price_Table PTable)
            {
                Price_Table newPTable = PTable;
                for (int i = 0; i < PTable.DateRowCount; i++)
                {
                    for (int j = 0; j < PTable.ValueColumnCount; j++)
                    {
                        if (PTable.GetValue(i, j) < 0)
                        {
                            newPTable.SetValue(i, j, 100 + PTable.GetValue(i, j));
                        };
                    }
                }
                return newPTable;
            }
        }
        #endregion

       
    }
}
