using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestSimDLL;


namespace NestSim
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
            il.Images.Add(new Icon("Resources/Folder.ico"));
            il.Images.Add(new Icon("Resources/Table.ico"));
            il.Images.Add(new Icon("Resources/Prop.ico"));
            il.Images.Add(new Icon("Resources/Strategy.ico"));
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
            dtgViewer.Columns.Clear();

            if (treeMain.SelectedNode != null && ((TreeView)sender).SelectedNode.Text != null && treeMain.SelectedNode.Tag != null)
            {
                Base_Table NodeTable = getNodeTable(baseContainer, treeMain.SelectedNode);

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "ST")
                {
                    
                    //string StrategyTag = ((TreeView)sender).SelectedNode.Tag.ToString();
                    //string ContainerTag = ((TreeView)sender).SelectedNode.Parent.Tag.ToString();

                    //int curStrategy = Convert.ToInt16(StrategyTag.ToString().Split('-')[1]);
                    //int curContainer = Convert.ToInt16(ContainerTag.ToString().Split('-')[1]);

                    //if (baseContainer.Containers[curContainer].Strategies[curStrategy].stratPerfSummary.CustomColumnCount > 0)
                    if(NodeTable.CustomColumnCount > 0)
                    {
                        //dtgViewer.DataSource = baseContainer.Containers[curContainer].Strategies[curStrategy].stratPerfSummary.ToDataTable();
                        dtgViewer.DataSource = NodeTable.ToDataTable();
                        dtgViewer.DefaultCellStyle.Format = "0.00%";
                        dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                        dtgViewer.Columns[2].DefaultCellStyle.Format = "0.00";
                    }
                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "CT")
                {
                    //string NodeTag = ((TreeView)sender).SelectedNode.Tag.ToString();
                    //string ContainerTag = ((TreeView)sender).SelectedNode.Parent.Tag.ToString();

                    //int curContainer = Convert.ToInt16(NodeTag.ToString().Split('-')[1]);

                    //if (curContainer < baseContainer.Containers.Count)

                    //if (curContainer < baseContainer.Containers.Count)
                    //{
                        //dtgViewer.DataSource = baseContainer.Containers[curContainer].stratPerfSummary.ToDataTable();
                        dtgViewer.DataSource = NodeTable.ToDataTable();
                        dtgViewer.DefaultCellStyle.Format = "0.00%";
                        dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                        dtgViewer.Columns[2].DefaultCellStyle.Format = "0.00";
                    //}
                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "MN")
                {
                    string curTag = ((TreeView)sender).SelectedNode.Tag.ToString();
                    //string StrategyTag = ((TreeView)sender).SelectedNode.Parent.Tag.ToString();
                    //string ContainerTag = ((TreeView)sender).SelectedNode.Parent.Parent.Tag.ToString();

                    int curMTable = Convert.ToInt16(curTag.ToString().Split('-')[1]);
                    //int curStrategy = Convert.ToInt16(StrategyTag.ToString().Split('-')[1]);
                    //int curContainer = Convert.ToInt16(ContainerTag.ToString().Split('-')[1]);

                    //Strategy_Base curObject = null;
                    //if (StrategyTag.Substring(0, 2) == "ST") { curObject = baseContainer.Containers[curContainer].Strategies[curStrategy]; };
                    //if (StrategyTag.Substring(0, 2) == "CT") { curObject = baseContainer.Containers[curStrategy]; };

                    switch (curMTable)
                    {
                        case 0:
                            if (NodeTable.CustomColumnCount > 0)
                            {
                                //dtgViewer.DataSource = curObject.stratPerfSummary.ToDataTable();
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                                dtgViewer.DefaultCellStyle.Format = "0.00%";
                                dtgViewer.Columns[2].DefaultCellStyle.Format = "0.00";
                            }
                        break;
                        case 1:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                //dtgViewer.DataSource = curObject.stratPositions.ToDataTable();
                                dtgViewer.DataSource = NodeTable.ToDataTable();    
                                dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                                dtgViewer.DefaultCellStyle.Format = "0.00%";
                                dtgViewer.Columns[2].DefaultCellStyle.Format = "0.00";
                            }
                            break;
                        case 2:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                //dtgViewer.DataSource = curObject.subContributions.ToDataTable();
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                            }
                            break;
                        case 3:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                //dtgViewer.DataSource = curObject.subPerformances.ToDataTable();
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                            }
                            break;
                        case 4:
                            if (NodeTable.ValueColumnCount > 0)
                            {
                                //dtgViewer.DataSource = curObject.subWeights.ToDataTable();
                                dtgViewer.DataSource = NodeTable.ToDataTable();
                                dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                            }
                            break;
                    }
                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0,2) == "PR")
                {
                    //string ParentTag = ((TreeView)sender).SelectedNode.Parent.Parent.Tag.ToString();
                    //string ContainerTag = ((TreeView)sender).SelectedNode.Parent.Parent.Parent.Tag.ToString();

                    //int curStrategy = Convert.ToInt16(ParentTag.ToString().Split('-')[1]);
                    //int curContainer = Convert.ToInt16(ContainerTag.ToString().Split('-')[1]);
                    //int curPTable = Convert.ToInt16(treeMain.SelectedNode.Tag.ToString().Split('-')[1]);

                    //dtgViewer.DataSource = getNodeDataTable(baseContainer,treeMain.SelectedNode);
                    dtgViewer.DataSource = NodeTable.ToDataTable();
                    dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                    //dtgViewer.DefaultCellStyle.Format = baseContainer.Containers[curContainer].Strategies[curStrategy].PriceTables[curPTable].ValueFormat;
                    dtgViewer.DefaultCellStyle.Format = NodeTable.ValueFormat;

                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "WT")
                {
                    //string ParentTag = ((TreeView)sender).SelectedNode.Parent.Parent.Tag.ToString();
                    //string ContainerTag = ((TreeView)sender).SelectedNode.Parent.Parent.Parent.Tag.ToString();

                    //int curStrategy = Convert.ToInt16(ParentTag.ToString().Split('-')[1]);
                    //int curContainer = Convert.ToInt16(ContainerTag.ToString().Split('-')[1]);
                    //int curPTable = Convert.ToInt16(treeMain.SelectedNode.Tag.ToString().Split('-')[1]);

                    //dtgViewer.DataSource = baseContainer.Containers[curContainer].Strategies[curStrategy].WeightTables[curPTable].ToDataTable();
                    dtgViewer.DataSource = NodeTable.ToDataTable();
                    dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                    //dtgViewer.DefaultCellStyle.Format = baseContainer.Containers[curContainer].Strategies[curStrategy].WeightTables[curPTable].ValueFormat;
                    dtgViewer.DefaultCellStyle.Format = NodeTable.ValueFormat;

                }

                if (treeMain.SelectedNode.Tag.ToString().Substring(0, 2) == "SI")
                {
                    //string ParentTag = ((TreeView)sender).SelectedNode.Parent.Parent.Tag.ToString();
                    //string ContainerTag = ((TreeView)sender).SelectedNode.Parent.Parent.Parent.Tag.ToString();

                    //int curStrategy = Convert.ToInt16(ParentTag.ToString().Split('-')[1]);
                    //int curContainer = Convert.ToInt16(ContainerTag.ToString().Split('-')[1]);
                    //int curPTable = Convert.ToInt16(treeMain.SelectedNode.Tag.ToString().Split('-')[1]);

                    //dtgViewer.DataSource = baseContainer.Containers[curContainer].Strategies[curStrategy].SignalTables[curPTable].ToDataTable();
                    dtgViewer.DataSource = NodeTable.ToDataTable();
                    dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
                    dtgViewer.DefaultCellStyle.Format = "0; -0; -";

                }

                for (int i = 0; i < dtgViewer.Columns.Count; i++)
                {
                    dtgViewer.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                dtgViewer.Visible = true;
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

    }
}