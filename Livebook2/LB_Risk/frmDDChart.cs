using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using LiveDLL;
using ZedGraph;

namespace LiveBook
{
    public partial class frmDDChart : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmDDChart()
        {
            InitializeComponent();
        }

        private void frmVARChart_Load(object sender, EventArgs e)
        {
            

            cmbView_SelectedValueChanged(this,e);

            SetSize();
        }

        public void Set_Portfolio_Values(int Id_Portfolio,DateTime Historical)
        {
            cmbView.SelectedValue = Id_Portfolio;
        }

        // Respond to the form 'Resize' event
        private void frmVARChart_Resize(object sender, EventArgs e)
        {
            SetSize();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 30);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 0,
                                    ClientRectangle.Height - 30);
        }


        // Build the Chart
        
        private void CreateGraph(ZedGraphControl zgc)
        {

            int Id_Portfolio;

            this.Cursor = Cursors.WaitCursor;

            Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());

            zgc.GraphPane.CurveList.Clear();
            zgc.GraphPane.Border.IsVisible = false;

            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;

            // Make up some data arrays based on the Sine function


            PointPairList listDDN = new PointPairList();
            PointPairList listLimit1 = new PointPairList();
            PointPairList listLimit2 = new PointPairList();
            PointPairList listLimit3 = new PointPairList();
            
            DateTime MaxDate = DateTime.Now;

            DateTime MinDate = DateTime.Now;
            string SQLString = "SELECT * FROM [dbo].[FCN_Get_Drawdown_Exit](" + Id_Portfolio + ") ORDER BY curDate;";

            DataTable curTable = curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in curTable.Rows)
            {
                DateTime TempDate = Convert.ToDateTime(curRow["curDate"]);
                if (TempDate > MaxDate) { MaxDate = TempDate; };
                if (TempDate < MinDate) { MinDate = TempDate; };
                if (curRow["curDDN"].ToString() != "")
                {
                    double TempDDN = Convert.ToDouble(curRow["curDDN"]);
                    double Limit1 = Convert.ToDouble(curRow["Lim1"]);
                    double Limit2 = Convert.ToDouble(curRow["Lim2"]);
                    double Limit3 = Convert.ToDouble(curRow["Lim3"]);

                    listDDN.Add(TempDate.ToOADate(), TempDDN);
                    listLimit1.Add(TempDate.ToOADate(), Limit1);
                    listLimit2.Add(TempDate.ToOADate(), Limit2);
                    listLimit3.Add(TempDate.ToOADate(), Limit3);
                }
            }

            LineItem curveDDN = myPane.AddCurve("Drawdown", listDDN, Color.FromArgb(102, 153, 255), SymbolType.None);
            curveDDN.Line.Width = 3.0F;

            LineItem curveLimit1 = myPane.AddCurve("Limit 1", listLimit1, Color.Black, SymbolType.None);
            curveLimit1.Line.Width = 1.5F;

            LineItem curveLimit2 = myPane.AddCurve("Limit 2", listLimit2, Color.Orange, SymbolType.None);
            curveLimit2.Line.Width = 1.5F;

            LineItem curveLimit3 = myPane.AddCurve("Limit 3", listLimit3, Color.Red, SymbolType.None);
            curveLimit3.Line.Width = 1.5F;
            
            // Legend and other
            myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

            myPane.XAxis.Type = AxisType.Date;
            myPane.YAxis.Scale.Format = "0%";

            myPane.XAxis.Scale.Format = "dd-MMM";
            
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            myPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0)).ToOADate();
            myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1,0,0)).ToOADate();

            myPane.IsFontsScaled = false;
            myPane.XAxis.Scale.FontSpec.Size = 10;
            myPane.YAxis.Scale.FontSpec.Size = 10;
            
            myPane.XAxis.Scale.MinorUnit = DateUnit.Day;
            myPane.XAxis.Scale.MinorStep = 5.0;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Day;
            myPane.XAxis.Scale.MajorStep = 10.0;

            myPane.YAxis.Scale.MajorStep = 0.01F;

            zgc.IsShowPointValues = true;

            this.Cursor = Cursors.Default;

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();
            zgc.Refresh();
        }
        
        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }




    }
}