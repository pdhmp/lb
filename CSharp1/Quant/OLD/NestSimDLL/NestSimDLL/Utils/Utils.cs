using System;
using System.Windows.Forms;
using System.Reflection;
using System.Collections.Generic;

namespace NestQuant.Common
{
    public static class Utils
    {
        private static double _NORMSINV95 = 1.64485362695147;
        public static double NORMSINV95
        {
            get { return _NORMSINV95; }
        }

        #region Enums
        
        public enum TableFillTypes { FillZero, FillPrevious, FillNaN }

        public enum TimeIntervals { IntervalYear, IntervalQuarter, IntervalMonth, IntervalDay, Interval15mins, Undefined }

        public enum StrategyTypes { SectorValue, HeadAndShoulder, SectorMomentum, Undefined }

        public enum TableTypes { Id_Ticker, IdSeq_Strategy, Undefined }

        public enum WeighingSchemes { FromComposite, EqualWeighed, Undefined }

        public enum PositionSchemes { Long, Flat, Short }

        public enum BenchmarkTypes { FromPosition, StrategyEqualWeighed, Custom }

        #endregion

        #region Methods

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

        public static double calcMedian(double[] curArray)
        {
            return calcMedian(curArray, true);
        }

        public static double calcMedian(double[] curArray, bool discardZeros)
        {
            int TotalItems = curArray.Length;
            int FirstNonNull = TotalItems;

            for (int i = 0; i < TotalItems; i++)
            {
                if (double.IsNaN(curArray[i]) || (curArray[i] == 0 && discardZeros)) { curArray[i] = double.NegativeInfinity; };
            }
            
            Array.Sort(curArray);

            for (int i = 0; i < TotalItems; i++)
            {
                if (curArray[i] != double.NegativeInfinity) { FirstNonNull = i; break; };
            }

            int FilledItems = TotalItems - FirstNonNull;

            if (FilledItems > 1)
            {
                if (FilledItems % 2 == 0)
                {
                    return (curArray[FirstNonNull + (FilledItems + 1) / 2] + curArray[FirstNonNull + (FilledItems - 1) / 2]) / 2;
                }
                else
                {
                    return curArray[FirstNonNull + FilledItems / 2];
                }
            }
            else
            {
                return double.NaN;
            }
        }

        public static double calcCumPerf(ref double[] curArray)
        {
            int iniRow = 0;
            int endrow = curArray.Length - 1;
            return calcCumPerf(ref curArray, iniRow, endrow);
        }

        public static double calcCumPerf(ref double[] curArray, int iniRow, int endRow)
        {
            double cumValue = 1;

            for (int i = iniRow; i <= endRow; i++)
            {
                cumValue = cumValue * (1 + curArray[i]);
            }

            return cumValue - 1;
        }

        public static double calcStdev(ref double[] curArray)
        {
            int iniRow = 0;
            int endrow = curArray.Length - 1;
            return calcStdev(ref curArray, iniRow, endrow);
        }

        public static double calcStdev(ref double[] curArray, int iniRow, int endRow)
        {
            double average = 0;
            double sqrAverage = 0;
            double stdev = 0;
            int curLenght = endRow - iniRow + 1;            

            for (int i = iniRow; i <= endRow; i++)
            {
                average = average + curArray[i];
                sqrAverage = sqrAverage + curArray[i] * curArray[i];
            }
            average = average / curLenght;
            sqrAverage = sqrAverage / curLenght;

            stdev = Math.Sqrt(sqrAverage - average * average);

            return stdev;
        }

        public static double calcMax(ref double[] curArray)
        {
            int iniRow = 0;
            int endrow = curArray.Length - 1;
            return calcMax(ref curArray, iniRow, endrow);
        }

        public static double calcMax(ref double[] curArray, int iniRow, int endRow)
        {
            double curMax = double.NegativeInfinity;

            for (int i = iniRow; i <= endRow; i++)
            {
                if (curArray[i] > curMax) { curMax = curArray[i]; };
            }
            return curMax;
        }

        public static string GetTableName(int Id_Ticker)
        {

            string SQLString = "SELECT Id_Tipo_Ativo FROM NESTDB.dbo.Tb001_Ativos WHERE Id_Ativo=" + Id_Ticker;

            using (NestConn thisConn = new NestConn())
            {
                string curType = thisConn.Execute_Query_String(SQLString);

                switch (curType)
                {
                    case "1": return "Tb050_Preco_Acoes_Onshore"; break;
                    case "2": return "Tb051_Preco_Acoes_Offshore"; break;
                    case "3": return "Tb058_Precos_Moedas"; break;
                    case "4": return "Tb053_Precos_Indices"; break;
                    case "5": return "Tb059_Precos_Futuros"; break;
                    case "6": return "Tb057_Precos_Commodities"; break;
                    case "7": return "Tb054_Precos_Opcoes"; break;
                    case "8": return "Tb052_Precos_Titulos"; break;
                    case "9": return "Tb060_Preco_Caixa"; break;
                    case "10": return "Tb056_Precos_Fundos"; break;
                    default: return "";
                }
            }
        }


        /*
        public static void Array_CalcPerformances(Price_Table perfTable, Price_Table PTable)
        {
            for (int i = 1; i < PTable.DateRowCount; i++)
            {
                for (int j = 0; j < PTable.ValueColumnCount; j++)
                {
                    double curPerf = 0;
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
                    double curPerf = 0;
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
                    double curPerf = 0;
                    if (signalTable.GetValue(i - 1, j) != 0)
                    {
                        curPerf = signalTable.GetValue(i - 1, j) * weightTable.GetValue(i - 1, j);
                    }
                    returnTable.SetValue(i, j, curPerf);
                }
            }
        }
*/

        #endregion

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

            // For all functions and Null means double.NaN
            public static Price_Table SetNull(Price_Table PTable, Base_Table TemplateTable)
            {
                return SetNull(PTable, TemplateTable, false);
            }
            
            public static Price_Table SetNull(Price_Table PTable, Base_Table TemplateTable, bool SetZeroAsNull)
            {
                if (Compare(PTable, TemplateTable))
                {
                    for (int i = 0; i < PTable.DateRowCount; i++)
                    {
                        for (int j = 0; j < PTable.ValueColumnCount; j++)
                        {
                            if (double.IsNaN(TemplateTable.GetValue(i, j)))
                            {
                                PTable.SetValue(i, j, double.NaN);
                            }
                            if (SetZeroAsNull && TemplateTable.GetValue(i, j) == 0)
                            {
                                PTable.SetValue(i, j, double.NaN);
                            }
                        }
                    }
                }
                return PTable;
            }

            public static Price_Table FixNegPEs(Price_Table PTable)
            {
                Price_Table newPTable = PTable;
                for (int i = 0; i < PTable.DateRowCount; i++)
                {
                    for (int j = 0; j < PTable.ValueColumnCount; j++)
                    {
                        if (PTable.GetValue(i, j) < 0 && !double.IsNaN(PTable.GetValue(i, j)))
                        {
                            newPTable.SetValue(i, j, 100 + PTable.GetValue(i, j));
                        };
                    }
                }
                return newPTable;
            }
        }
        #endregion

    
        #region TreeRegion
        public class Trees
        {
            public static void NodeAddStratTables(TreeNode curNode, Strategy curStrategy)
            {
                if (curStrategy.PriceTables.Count  > 0 ||
                    curStrategy.WeightTables.Count > 0 ||
                    curStrategy.SignalTables.Count > 0)
                {
                    TreeNode newNode = curNode.Nodes.Add("Other Tables");                    

                    int i = 0;
                    foreach (Price_Table curTable in curStrategy.PriceTables)
                    {
                        NodeAddTable(newNode, curTable, "PR-" + i, 1);
                        i++;
                    }

                    i = 0;
                    foreach (Weight_Table curTable in curStrategy.WeightTables)
                    {
                        NodeAddTable(newNode, curTable, "WT-" + i, 1);
                        i++;
                    }

                    i = 0;
                    foreach (Price_Table curTable in curStrategy.SignalTables)
                    {
                        NodeAddTable(newNode, curTable, "SI-" + i, 1);
                        i++;
                    }
                }
            }

            public static void NodeAddMainTables(TreeNode curNode, Strategy curStrategy)
            {
                if (curStrategy.stPerfSummary != null && curStrategy.stPerfSummary.CustomColumnCount > 0) { NodeAddTable(curNode, curStrategy.stPerfSummary, "MN-0", 1); };
                if (curStrategy.stContributions != null && curStrategy.stContributions.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.stContributions, "MN-1", 1); };
                if (curStrategy.stPerformances != null && curStrategy.stPerformances.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.stPerformances, "MN-2", 1); };
                if (curStrategy.stPositions != null && curStrategy.stPositions.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.stPositions, "MN-3", 1); };
                if (curStrategy.tkPositions != null && curStrategy.tkPositions.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.tkPositions, "MN-4", 1); };
                if (curStrategy.tkPerformances != null && curStrategy.tkPerformances.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.tkPerformances, "MN-5", 1); };                
            }
            
            public static void NodeAddTable(TreeNode mainNode, Base_Table curTable, string nodeTag, int nodeImageIndex)
            {
                string curName = curTable.Name;
                TreeNode curNode = mainNode.Nodes.Add(curName);
                curNode.Tag = nodeTag;
                curNode.ImageIndex = nodeImageIndex;
                curNode.SelectedImageIndex = nodeImageIndex;

            }

            public static void NodeAddStrategy(TreeNode curNode, Strategy curStrategy, string FormCaption, int ContID)
            {
                TreeNode newNode = curNode.Nodes.Add(FormCaption);
                newNode.Tag = "ST-" + ContID;
                newNode.SelectedImageIndex = 3;
                newNode.ImageIndex = 3;

                NodeAddProperties(newNode, curStrategy);
                
                NodeAddMainTables(newNode, curStrategy);
                NodeAddStratTables(newNode, curStrategy);
                if (curStrategy.TradeList != null && curStrategy.TradeList.TradeList != null)
                {
                    TreeNode tradelist = newNode.Nodes.Add("TradeList");
                    tradelist.Tag = "TL-0";
                    tradelist.SelectedImageIndex = 1;
                    tradelist.ImageIndex = 1;
                }
                if (curStrategy.Strategies.Count > 0)
                {

                    TreeNode stratNode = newNode.Nodes.Add("Strategies");                    

                    foreach (KeyValuePair<int, Strategy> inStrategy in curStrategy.Strategies)
                    {
                        NodeAddStrategy(stratNode, inStrategy.Value, inStrategy.Value.Name, inStrategy.Key);
                    }
                }
                

            }           

            public static void NodeAddProperties(TreeNode curNode, object curObject)
            {
                TreeNode PropNode = curNode.Nodes.Add("Properties");
                PropertyInfo[] properties = curObject.GetType().GetProperties();
                foreach (PropertyInfo curProperty in properties)
                {
                    string tempstring = curProperty.Name;
                    string tempvalue = "";
                    if (curProperty.GetValue(curObject, null) != null)
                    {
                        tempvalue = curProperty.GetValue(curObject, null).ToString();                        
                    }
                    else
                    {
                        tempvalue = "NULL";
                    }
                    TreeNode thisPropNode = PropNode.Nodes.Add(tempstring + ": " + tempvalue);
                    thisPropNode.SelectedImageIndex = 2;
                    thisPropNode.ImageIndex = 2;

                }

            }

            public static Base_Table getNodeTable(Strategy Parent, TreeNode Node)
            {
                return getNodeTable(Parent, getParentNodeStack(Node), null);
            }

            public static Base_Table getNodeTable(Strategy Parent, Stack<TreeNode> NodeStack, TreeNode prevStratNode)
            {
                Base_Table result = null;

                //Pop stack
                TreeNode Node = NodeStack.Pop();

                string NodeTag = Node.Tag.ToString();

                string[] NodeTagSplit = NodeTag.Split('-');

                string NodeType = NodeTagSplit[0];
                int curNode = Convert.ToInt16(NodeTagSplit[1]);

                //Check if it´s the last node
                if (NodeStack.Count == 0)
                {
                    //If it´s the last node, get data table
                    switch (NodeType)
                    {
                         case "ST":
                            result = Parent.stPerfSummary;
                            break;
                        case "MN":
                            switch (curNode)
                            {
                                case 0:
                                    result = Parent.stPerfSummary;
                                    break;
                                case 1:
                                    result = Parent.stContributions;
                                    break;
                                case 2:
                                    result = Parent.stPerformances;
                                    break;
                                case 3:
                                    result = Parent.stPositions;
                                    break;
                                case 4:
                                    result = Parent.tkPositions;
                                    break;
                                case 5:
                                    result = Parent.tkPerformances;
                                    break;
                            }
                            break;
                        case "PR":
                            result = Parent.PriceTables[curNode];
                            break;
                        case "WT":
                            result = Parent.WeightTables[curNode];
                            break;
                        case "SI":
                            result = Parent.SignalTables[curNode];
                            break;
                        default:
                            //throw new System.NotImplementedException();
                            break;
                    }
                }
                else 
                {
                    //If it isn´t the last node, get data table from next node
                    switch (NodeType)
                    {
                        case "ST":
                            TreeNode nextNode = NodeStack.Peek();
                            string nextNodeTag = nextNode.Tag.ToString();
                            string nextNodeType = nextNodeTag.Split('-')[0];
                            int nextNodeCur = Convert.ToInt16(nextNodeTag.Split('-')[1]);

                            if (nextNodeType == "ST")
                            {
                                result = getNodeTable(Parent.Strategies[nextNodeCur], NodeStack, prevStratNode);
                            }
                            else
                            {
                                result = getNodeTable(Parent, NodeStack, prevStratNode);
                            }
                            break;
                            
                        default:
                            result = getNodeTable(Parent, NodeStack, prevStratNode);
                            break;
                    }
                }

                //Return data table
                return result;
            }

            public static Stack<TreeNode> getParentNodeStack(TreeNode Node)
            {
                Stack<TreeNode> NodeStack = new Stack<TreeNode>();

                NodeStack = getParentNodeStack(Node, NodeStack);

                return NodeStack;

            }

            public static Stack<TreeNode> getParentNodeStack(TreeNode Node, Stack<TreeNode> NodeStack)
            {

                if(Node.Tag != null)
                    NodeStack.Push(Node);

                if (Node.Level > 0)
                {
                    NodeStack = getParentNodeStack(Node.Parent, NodeStack);
                }

                return NodeStack;
            }

        }

        #endregion
    }
}
