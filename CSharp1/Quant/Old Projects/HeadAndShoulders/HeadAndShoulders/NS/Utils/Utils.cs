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

        #region FormRegion

        public static void ShowInForm(Base_Table curTable)
        {
            ShowInForm(curTable, "Viewer", "0.00");
        }

        public static void ShowInForm(Base_Table curTable, string FormCaption, string defFormat)
        {
            frmViewTable viewThisTable = new frmViewTable();

            viewThisTable.dtgViewer.DataSource = curTable.ToDataTable();
            viewThisTable.Text = FormCaption;
            viewThisTable.dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
            viewThisTable.dtgViewer.DefaultCellStyle.Format = defFormat;

            for (int i = 0; i < viewThisTable.dtgViewer.Columns.Count; i++)
            {
                viewThisTable.dtgViewer.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            viewThisTable.Show();
        }

        public static void ShowInForm(Strategy curStrategy, string FormCaption)
        {
            frmViewStrategy StratForm = new frmViewStrategy();
            StratForm.curStrategy = (SectorValueBZ)curStrategy;
            StratForm.Text = FormCaption;

            StratForm.AddButton("cmd_perfSummary", "perfSummary", 0, 100);

            int i = 0;
            foreach (Price_Table curTable in curStrategy.PriceTables)
            {
                string curName = curTable.Name;
                StratForm.AddButton("cmd_" + curName, curName, i, 200);
                i++;
            }

            i = 0;
            foreach (Weight_Table curTable in curStrategy.WeightTables)
            {
                string curName = curTable.Name;
                StratForm.AddButton("cmd_" + curName, curName, i, 300);
                i++;
            }

            i = 0;
            foreach (Price_Table curTable in curStrategy.SignalTables)
            {
                string curName = curTable.Name;
                StratForm.AddButton("cmd_" + curName, curName, i, 400);
                i++;
            }

            StratForm.Show();
        }

        public static void ShowInForm(Strategy_Container curContainer, string FormCaption)
        {
            frmViewContainer ContainerForm = new frmViewContainer();
            ContainerForm.curContainer = curContainer;
            ContainerForm.Text = FormCaption;

            int i = 0;
            foreach (Strategy curStrategy in curContainer.Strategies)
            {
                string curName = curStrategy.Name;
                ContainerForm.AddButton("cmd_" + curName, curName, i, 100);
                i++;
            }

            ContainerForm.Show();
        }

        #endregion

        #region TreeRegion
        public class Trees
        {
            public static void NodeAddStratTables(TreeNode curNode, Strategy curStrategy)
            {
                TreeNode newNode = curNode.Nodes.Add("Other Tables");
                newNode.Tag = "OT-0";

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

            public static void NodeAddMainTables(TreeNode curNode, Strategy_Base curStrategy)
            {
                if (curStrategy.stratPerfSummary != null && curStrategy.stratPerfSummary.CustomColumnCount > 0) { NodeAddTable(curNode, curStrategy.stratPerfSummary, "MN-0", 1); };
                if (curStrategy.stratPositions != null && curStrategy.stratPositions.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.stratPositions, "MN-1", 1); };
                if (curStrategy.subContributions != null && curStrategy.subContributions.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.subContributions, "MN-2", 1); };
                if (curStrategy.subPerformances != null && curStrategy.subPerformances.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.subPerformances, "MN-3", 1); };
                if (curStrategy.subWeights != null && curStrategy.subWeights.ValueColumnCount > 0) { NodeAddTable(curNode, curStrategy.subWeights, "MN-4", 1); };
            }

            public static void NodeAddTable(TreeNode mainNode, Base_Table curTable, string nodeTag, int nodeImageIndex)
            {
                string curName = curTable.Name;
                TreeNode curNode = mainNode.Nodes.Add(curName);
                curNode.Tag = nodeTag;
                curNode.ImageIndex = nodeImageIndex;
                curNode.SelectedImageIndex = nodeImageIndex;

            }

            public static void NodeAddContainer(TreeNode curNode, Strategy_Container curContainer, string FormCaption, int ContID)
            {
                TreeNode newNode = curNode.Nodes.Add(FormCaption);
                newNode.Tag = "CT-" + ContID;

                NodeAddProperties(newNode, curContainer);
                NodeAddMainTables(newNode, (Strategy_Base)curContainer);

                int i = 0;
                foreach (Strategy_Container curSubContainer in curContainer.Containers)
                {
                    NodeAddContainer(newNode, curSubContainer, curSubContainer.Name, i);
                    i++;
                }

                i = 0;
                foreach (Strategy curSrategy in curContainer.Strategies)
                {
                    NodeAddStrategies(newNode, curSrategy, curSrategy.Name, i);
                    i++;
                }

                newNode.Expand();

            }

            public static void NodeAddStrategies(TreeNode curNode, Strategy curStrategy, string FormCaption, int ContID)
            {
                string curName = curStrategy.Name;
                TreeNode stratNode = curNode.Nodes.Add(curName);
                stratNode.Tag = "ST-" + ContID;
                stratNode.SelectedImageIndex = 3;
                stratNode.ImageIndex = 3;

                NodeAddProperties(stratNode, curStrategy);
                NodeAddMainTables(stratNode, (Strategy_Base)curStrategy);
                NodeAddStratTables(stratNode, curStrategy);
                //stratNode.Expand();

            }

            public static void NodeAddProperties(TreeNode curNode, object curObject)
            {
                TreeNode PropNode = curNode.Nodes.Add("Properties");
                PropertyInfo[] properties = curObject.GetType().GetProperties();
                foreach (PropertyInfo curProperty in properties)
                {
                    string tempstring = curProperty.Name;
                    string tempvalue = curProperty.GetValue(curObject, null).ToString();
                    TreeNode thisPropNode = PropNode.Nodes.Add(tempstring + ": " + tempvalue);
                    thisPropNode.SelectedImageIndex = 2;
                    thisPropNode.ImageIndex = 2;
                }

            }
        }


#endregion
    }
}
