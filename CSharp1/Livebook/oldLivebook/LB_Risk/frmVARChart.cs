using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using System.Data.SqlClient;
using ZedGraph;

namespace LiveBook
{
    public partial class frmVARChart : LBForm
    {
        public frmVARChart()
        {
            InitializeComponent();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }

        private void frmVARChart_Load(object sender, EventArgs e)
        {
            

            cmbView.SelectedValueChanged -= new System.EventHandler(this.cmbView_SelectedValueChanged);
            carrega_Combo();
            cmbView.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);

            cmbView_SelectedValueChanged(this,e);

            SetSize();
            timer1.Start();
        }

        void Carrega_Grid(int Id_Portfolio)
        {
        }

        void carrega_Combo()
        {
        }

        public void Set_Portfolio_Values(int Id_Portfolio,DateTime Historical)
        {
            cmbView.SelectedValue = Id_Portfolio;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*
            if (!processing)
            {
                processing = true;
                Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));
                processing = false;
            }
            */
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


            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            
            string SQLString;
            

            double PrevNAV=0;
            DateTime MaxDate = DateTime.Now;
            DateTime MinDate = DateTime.Now;

            SQLString = "SELECT * FROM dbo.FCN_VAR_History(" + Id_Portfolio + ") ORDER BY VAR_Date;";

            using (newNestConn curConn = new newNestConn())
            {
                DataTable curTable = curConn.Return_DataTable(SQLString);
                foreach (DataRow curRow in curTable.Rows)
                {
                    DateTime TempDate = Convert.ToDateTime(curRow["VAR_Date"]);
                    if (TempDate > MaxDate) { MaxDate = TempDate; };
                    if (TempDate < MinDate) { MinDate = TempDate; };
                    if (curRow["VAR_Percent"].ToString() != "")
                    {
                        double PctChange = 0;

                        double TempVAR = Convert.ToDouble(curRow["VAR_Percent"]);
                        double TempVARLimit = 0;
                        if (!DBNull.Value.Equals(curRow["VARLimit"]))
                            TempVARLimit = Convert.ToDouble(curRow["VARLimit"]);

                        if (PrevNAV > 0 && curRow["NAV"].ToString() != "")
                        {
                            PctChange = Convert.ToDouble(curRow["NAV"]) / PrevNAV - 1;
                        }

                        list1.Add(TempDate.ToOADate(), TempVAR);
                        if (TempVARLimit != 1) { list4.Add(TempDate.ToOADate(), -TempVARLimit); };

                        if (PctChange < 0)
                        {
                            list2.Add(TempDate.ToOADate(), PctChange);
                        }
                        else
                        {
                            list2.Add(TempDate.ToOADate(), 0);
                        };

                        if (PctChange < 0 && PctChange < TempVAR)
                        {
                            list3.Add(TempDate.ToOADate(), PctChange);
                        }
                        else
                        {
                            list3.Add(TempDate.ToOADate(), 0);
                        };

                        if (curRow["NAV"].ToString() != "")
                        {
                            PrevNAV = Convert.ToDouble(curRow["NAV"]);
                        };

                    }
                }

                // VAR Line
                LineItem myCurve = myPane.AddCurve("VAR 95%", list1, Color.FromArgb(102, 153, 255), SymbolType.None);
                myCurve.Line.Width = 3.0F;

                // All bar settings
                myPane.BarSettings.MinBarGap = -1F;
                myPane.BarSettings.MinClusterGap = 0;

                // Red Bars
                BarItem myBar2 = myPane.AddBar("VAR Exceeded", list3, Color.Red);
                myBar2.Bar.Fill = new Fill(Color.Red, Color.Red, Color.Red);
                myBar2.Bar.Border.IsVisible = false;

                // Gray Bars
                BarItem myBar = myPane.AddBar("Day Performance", list2, Color.Gray);
                myBar.Bar.Fill = new Fill(Color.Gray, Color.Gray, Color.Gray);
                myBar.Bar.Border.IsVisible = false;

                // VAR Limit Line
                LineItem myCurve2 = myPane.AddCurve("VAR Limit", list4, Color.Black, SymbolType.None);
                myCurve2.Line.Width = 1.0F;
                myCurve2.Line.Fill = new Fill(Color.LightGray, Color.LightGray, -45F);

                // Legend and other
                myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

                myPane.XAxis.Type = AxisType.Date;
                myPane.YAxis.Scale.Format = "0.00%";

                myPane.XAxis.MajorGrid.IsVisible = true;
                myPane.YAxis.MajorGrid.IsVisible = true;
                myPane.XAxis.MajorGrid.Color = Color.Gray;
                myPane.YAxis.MajorGrid.Color = Color.Gray;

                myPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(50, 0, 0)).ToOADate();
                myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-50, 0, 0)).ToOADate();

                myPane.IsFontsScaled = false;
                myPane.XAxis.Scale.FontSpec.Size = 10;
                myPane.YAxis.Scale.FontSpec.Size = 10;
                myPane.XAxis.Scale.Format = "dd-MMM-yy";

                myPane.XAxis.Scale.MinorUnit = DateUnit.Month;
                myPane.XAxis.Scale.MinorStep = 1.0;
                myPane.XAxis.Scale.MajorUnit = DateUnit.Month;
                myPane.XAxis.Scale.MajorStep = 3.0;

                zgc.IsShowPointValues = true;

                this.Cursor = Cursors.Default;

                // Tell ZedGraph to refigure the
                // axes since the data have changed
                zgc.AxisChange();
                zgc.Refresh();
            }
        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }




    }
}