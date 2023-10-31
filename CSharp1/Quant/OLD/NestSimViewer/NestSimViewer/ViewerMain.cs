using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using ZedGraph;

namespace NestQuant.Common
{
    public partial class ViewerMain : Form
    {
        ImageList il = new ImageList();
        Base_Strategy_Container baseContainer;
        DateTime iniCounter;
        DateTime endCounter;
        TimeSpan TimeTaken;

        public ViewerMain(Strategy_Container CustomStrategyContainer)
        {
            //Initalize Base Container
            baseContainer = new Base_Strategy_Container();
            baseContainer.Name = "All Strategies";

            baseContainer.AddContainer(CustomStrategyContainer);

            InitializeMain();
        }

        public ViewerMain(List<Strategy_Container> CustomStrategyContainerList)
        {
            //Initalize Base Container
            baseContainer = new Base_Strategy_Container();
            baseContainer.Name = "All Strategies";

            foreach (Strategy_Container CustomStrategyContainer in CustomStrategyContainerList)
            {
                baseContainer.AddContainer(CustomStrategyContainer);
            }

            InitializeMain();
        }

        private void InitializeMain()
        {
            InitializeComponent();
            il.Images.Add(new Icon(NestSimViewer.Properties.Settings.Default.ResourcePath + "Folder.ico"));
            il.Images.Add(new Icon(NestSimViewer.Properties.Settings.Default.ResourcePath + "Table.ico"));
            il.Images.Add(new Icon(NestSimViewer.Properties.Settings.Default.ResourcePath + "Prop.ico"));
            il.Images.Add(new Icon(NestSimViewer.Properties.Settings.Default.ResourcePath + "Strategy.ico"));
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void cmdRunStrat_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor;

            cmdRunStrat.Enabled = false;
            iniCounter = DateTime.Now;

            baseContainer.RunAll();
            
            TreeNode mainNode = treeMain.Nodes.Add("This Sim");
            mainNode.Tag = "TS-00";
            
            endCounter = DateTime.Now;
            TimeTaken = endCounter.Subtract(iniCounter);
            lblLoadTime.Text = "Load time: " + String.Format("{0:HH:mm:ss:ffff}", new DateTime(TimeTaken.Ticks));

            //Utils.ShowInForm(thisContainer, "Value BZ");
            Utils.Trees.NodeAddContainer(treeMain.Nodes[0], baseContainer, baseContainer.Name, 0);
            treeMain.ImageList = il;
            mainNode.Expand();

            Cursor.Current = Cursors.WaitCursor;
            cmdRunStrat.Enabled = true;

        }

        private void treeMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dtgViewer.Visible = false;
            zgcGrossNet.Visible = false;
            zgcGR100.Visible = false;
            lblCopy.Visible = false;

            int prevRowHandle = dtvViewer.FocusedRowHandle;
            dtvViewer.Columns.Clear();

            if (treeMain.SelectedNode != null && ((TreeView)sender).SelectedNode.Text != null && treeMain.SelectedNode.Tag != null)
            {
                Base_Table NodeTable = getNodeTable(baseContainer, treeMain.SelectedNode);
                labItemName.Text = treeMain.SelectedNode.Text;

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "ST")
                {
                    if(NodeTable.CustomColumnCount > 0)
                    {
                        dtgViewer.DataSource = NodeTable.ToDataTable();
                        labReturn.Text = ((perfSummary_Table)NodeTable).AnnualizedPerformance.ToString("0.00%");
                        labStDev.Text = ((perfSummary_Table)NodeTable).AnnualizedDev.ToString("0.00%");
                        labSharpe.Text = ((perfSummary_Table)NodeTable).AnnualizedSharpe.ToString("0.00");
                        UpdateGr100Chart(NodeTable.ToDataTable());
                        if (treeMain.SelectedNode.Nodes.Count > 3)
                        {
                            Base_Table ExpTable = getNodeTable(baseContainer, treeMain.SelectedNode.Nodes[4]);
                            UpdateGrossNetChart(ExpTable.ToDataTable());
                        }
                        FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                        dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                        dtvViewer.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        dtvViewer.Columns[2].DisplayFormat.FormatString = "0.00";
                    }
                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "CT")
                {
                    dtgViewer.DataSource = NodeTable.ToDataTable();
                    labReturn.Text = ((perfSummary_Table)NodeTable).AnnualizedPerformance.ToString("0.00%");
                    labStDev.Text = ((perfSummary_Table)NodeTable).AnnualizedDev.ToString("0.00%");
                    labSharpe.Text = ((perfSummary_Table)NodeTable).AnnualizedSharpe.ToString("0.00");
                    UpdateGr100Chart(NodeTable.ToDataTable());
                    FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                    dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                    dtvViewer.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dtvViewer.Columns[2].DisplayFormat.FormatString = "0.00";
                    dtvViewer.GroupSummary.Add(SummaryItemType.Sum, "PERF", dtvViewer.Columns["PERF"], "{0:p2}");
                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "MN")
                {
                    string curTag = ((TreeView)sender).SelectedNode.Tag.ToString();

                    int curMTable = Convert.ToInt16(curTag.ToString().Split('-')[1]);

                    switch (curMTable)
                    {
                        case 0:
                            if (NodeTable.CustomColumnCount > 0)
                            {
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                                dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                                dtvViewer.Columns[2].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                                dtvViewer.Columns[2].DisplayFormat.FormatString = "0.00";
                            }
                        break;
                        case 1:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                                dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                            }
                            break;
                        case 2:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                                dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                            }
                            break;
                        case 3:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                                dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                            }
                            break;
                        case 4:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0.00%");
                                dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                            }
                            break;
                    }
                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0,2) == "PR")
                {
                    dtgViewer.DataSource = NodeTable.ToDataTable();
                    dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";

                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "WT")
                {
                    dtgViewer.DataSource = NodeTable.ToDataTable();
                    dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";

                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "SI")
                {
                    dtgViewer.DataSource = NodeTable.ToDataTable();

                    FormatAllColumns(dtvViewer, DevExpress.Utils.FormatType.Numeric, "0; -0; -");
                    dtvViewer.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dtvViewer.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";

                }

                for (int i = 0; i < dtvViewer.Columns.Count; i++)
                {
                    if (NodeTable.ValueColumnType == Utils.TableTypes.Id_Ticker && i > 0)
                    {
                        using (NestConn conn = new NestConn())
                        {
                            string secName = conn.Execute_Query_String("SELECT Simbolo FROM NESTDB.dbo.Tb001_Ativos WHERE Id_Ativo=" + dtvViewer.Columns[i].Name.Replace("col", ""));
                            dtvViewer.Columns[i].Caption = secName;

                        }
                    }
                    if (i > -1) { dtvViewer.Columns[i].Width = 90; } else { dtvViewer.Columns[i].BestFit(); };
                }

                dtgViewer.Visible = true;
                zgcGrossNet.Visible = true;
                zgcGR100.Visible = true;
                lblCopy.Visible = true;

                UpdatePeriod();
                if (prevRowHandle > 10) { dtvViewer.FocusedRowHandle = prevRowHandle - 10; };
                dtvViewer.FocusedRowHandle = prevRowHandle;
                
            }

        }

        private Stack<TreeNode> getParentNodeStack(TreeNode Node, Stack<TreeNode> NodeStack)
        {
            NodeStack.Push(Node);

            if (Node.Level > 0)
            {                
                NodeStack = getParentNodeStack(Node.Parent, NodeStack);
            }

            return NodeStack;
        }

        private Stack<TreeNode> getParentNodeStack(TreeNode Node)
        {
            Stack<TreeNode> NodeStack = new Stack<TreeNode>();

            NodeStack = getParentNodeStack(Node, NodeStack);

            return NodeStack;

        }

        private Base_Table getNodeTable(Strategy_Base Parent, Stack<TreeNode> NodeStack)
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
                    case "CT":
                        result = ((Strategy_Container)Parent).Containers[curNode].stratPerfSummary;
                        break;
                    case "ST":
                        result = ((Strategy_Container)Parent).Strategies[curNode].stratPerfSummary;
                        break;
                    case "MN":
                        switch (curNode)
                        {
                            case 0:
                                result = Parent.stratPerfSummary;
                                break;
                            case 1:
                                result = Parent.stratPositions;
                                break;
                            case 2:
                                result = Parent.subContributions;
                                break;
                            case 3:
                                result = Parent.subPerformances;
                                break;
                            case 4:
                                result = Parent.subWeights;
                                break;
                        }
                        break;
                    case "PR":
                        result = ((Strategy)Parent).PriceTables[curNode];
                        break;
                    case "WT":
                        result = ((Strategy)Parent).WeightTables[curNode];
                        break;
                    case "SI":
                        result = ((Strategy)Parent).SignalTables[curNode];
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
                    case "CT":
                        if(Node.Level >1)
                            result = getNodeTable(((Strategy_Container)Parent).Containers[curNode], NodeStack);
                        else
                            result = getNodeTable(Parent, NodeStack);
                        break;
                    case "ST":
                        result = getNodeTable(((Strategy_Container)Parent).Strategies[curNode], NodeStack);
                        break;
                    default:
                        result = getNodeTable(Parent, NodeStack);
                        break;
                }
            }   

            //Return data table
            return result;
        }
        
        private Base_Table getNodeTable(Strategy_Base Parent, TreeNode Node)
        {
            return getNodeTable(Parent, getParentNodeStack(Node));
        }

        private void FormatAllColumns(DevExpress.XtraGrid.Views.Grid.GridView curView, DevExpress.Utils.FormatType curType, string curFormat)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn curColumn in curView.Columns)
            {
                curColumn.DisplayFormat.FormatType = curType;
                curColumn.DisplayFormat.FormatString = curFormat;
            }
        }

        private void radDay_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePeriod();
        }

        private void radMonth_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePeriod();
        }

        private void radYear_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePeriod();
        }

        private void UpdatePeriod()
        {
            if (radDay.Checked)
            {
                dtvViewer.Columns[0].GroupIndex = -1;
                dtvViewer.Columns[1].BestFit();
            }
            if (radMonth.Checked)
            {
                dtvViewer.Columns[0].GroupIndex = 0;
                dtvViewer.Columns[0].GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateMonth;
                dtvViewer.Columns[1].Width = 150;
            }
            if (radYear.Checked)
            {
                dtvViewer.Columns[0].GroupIndex = 0;
                dtvViewer.Columns[0].GroupInterval = DevExpress.XtraGrid.ColumnGroupInterval.DateYear;
                dtvViewer.Columns[1].Width = 150;
            }
        }

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void dtvViewer_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {

            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]);

            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {

                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                if (column == null)
                {
                    MessageBox.Show("Column name is null");
                }
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                int a = 0;

                if (a == 0)
                {
                    // calculate summary text and boundaries
                    string text = item.GetDisplayText(values[item], false);
                    SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                    int width = Convert.ToInt32(sz.Width) + 1;
                    rect.X += rect.Width - width - 2;
                    rect.Width = width;
                    rect.Y = e.Bounds.Y;
                    rect.Height = e.Bounds.Height - 2;
                    e.Appearance.DrawString(e.Cache, text, rect);
                }
            }

            e.Handled = true;

        }

        private void dtvViewer_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue is double)
            {
                double curValue = Convert.ToDouble(e.CellValue);
                if (Double.IsNaN(curValue))
                {
                    e.DisplayText = "NaN";
                    if (e.RowHandle != ((GridView)sender).FocusedRowHandle)
                    {
                        e.DisplayText = "";
                        e.Appearance.BackColor = Color.LightGray;
                    }
                }
            }
        }

        private void UpdateGr100Chart(DataTable PerfTable)
        {
            zgcGR100.GraphPane.CurveList.Clear();
            zgcGR100.GraphPane.Border.IsVisible = false;

            // get a reference to the GraphPane
            GraphPane myPane = zgcGR100.GraphPane;

            // Set the Titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;

            PointPairList listGR100 = new PointPairList();
            DateTime MaxDate = DateTime.MinValue;
            DateTime MinDate = DateTime.MaxValue;

            for (int i = 0; i < PerfTable.Rows.Count; i++)
            {
                DateTime TempDate = Convert.ToDateTime(PerfTable.Rows[i][0]);
                double tempValue = Convert.ToDouble(PerfTable.Rows[i][2]);

                listGR100.Add(TempDate.ToOADate(), tempValue);

                if (TempDate > MaxDate) { MaxDate = TempDate; };
                if (TempDate < MinDate) { MinDate = TempDate; };

            }

            LineItem lineLS = myPane.AddCurve("LS", listGR100, Color.FromArgb(102, 153, 255), SymbolType.None);
            lineLS.Line.Width = 2.0F;

            myPane.XAxis.Type = AxisType.Date;
            myPane.YAxis.Scale.Format = "0.00";

            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();//.Add(new TimeSpan(-1, 0, 0)).ToOADate();
            myPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();

            myPane.IsFontsScaled = false;
            myPane.XAxis.Scale.FontSpec.Size = 10;
            myPane.YAxis.Scale.FontSpec.Size = 10;
            myPane.Legend.FontSpec.Size = 12;

            myPane.Legend.IsVisible = false;

            myPane.XAxis.Scale.MinorUnit = DateUnit.Day;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Day;

            zgcGR100.IsShowPointValues = true;

            this.Cursor = Cursors.Default;

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgcGR100.AxisChange();
            zgcGR100.Refresh();
        }

        private void UpdateGrossNetChart(DataTable PosnTable)
        {
            zgcGrossNet.GraphPane.CurveList.Clear();
            zgcGrossNet.GraphPane.Border.IsVisible = false;

            // get a reference to the GraphPane
            GraphPane myPane = zgcGrossNet.GraphPane;

            // Set the Titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;

            PointPairList listGross = new PointPairList();
            PointPairList listNet = new PointPairList();
            DateTime MaxDate = DateTime.MinValue;
            DateTime MinDate = DateTime.MaxValue;

            int totColumns = PosnTable.Columns.Count;

            if (totColumns > 4)
            {
                for (int i = 0; i < PosnTable.Rows.Count; i++)
                {
                    DateTime TempDate = Convert.ToDateTime(PosnTable.Rows[i][0]);
                    double tempGross = Convert.ToDouble(PosnTable.Rows[i][totColumns - 5]);
                    double tempNet = Convert.ToDouble(PosnTable.Rows[i][totColumns - 2]);

                    listGross.Add(TempDate.ToOADate(), tempGross);
                    listNet.Add(TempDate.ToOADate(), tempNet);

                    if (TempDate > MaxDate) { MaxDate = TempDate; };
                    if (TempDate < MinDate) { MinDate = TempDate; };

                }

                LineItem lineNet = myPane.AddCurve("Net", listNet, Color.FromArgb(102, 153, 255), SymbolType.None);
                lineNet.Line.Width = 2.0F;

                LineItem lineGross = myPane.AddCurve("Gross", listGross, Color.Gray, SymbolType.None);
                lineGross.Line.Fill = new Fill(Color.LightGray, Color.LightGray, -45F);
                lineGross.Line.Width = 0.5F;

                myPane.XAxis.Type = AxisType.Date;
                myPane.YAxis.Scale.Format = "0%";

                myPane.XAxis.MajorGrid.IsVisible = true;
                myPane.YAxis.MajorGrid.IsVisible = true;
                myPane.XAxis.MajorGrid.Color = Color.Gray;
                myPane.YAxis.MajorGrid.Color = Color.Gray;

                myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
                myPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();

                myPane.IsFontsScaled = false;
                myPane.XAxis.Scale.FontSpec.Size = 10;
                myPane.YAxis.Scale.FontSpec.Size = 10;
                myPane.Legend.FontSpec.Size = 12;

                myPane.Legend.IsVisible = false;

                myPane.XAxis.Scale.MinorUnit = DateUnit.Day;
                myPane.XAxis.Scale.MajorUnit = DateUnit.Day;

                zgcGR100.IsShowPointValues = true;

                this.Cursor = Cursors.Default;

                zgcGrossNet.AxisChange();
                zgcGrossNet.Refresh();
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dtvViewer.SelectAll();
            dtvViewer.CopyToClipboard();
        }
    
    }
}