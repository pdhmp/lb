using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using System.Data.SqlClient;
using ZedGraph;

namespace SGN
{
    public partial class frmPerfChart : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmPerfChart()
        {
            InitializeComponent();
        }


        private void frmPerfChart_Load(object sender, EventArgs e)
        {
            

            cmbView.SelectedValueChanged -= new System.EventHandler(this.cmbView_SelectedValueChanged);
            carrega_Combo();
            cmbView.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);

            cmbView_SelectedValueChanged(this,e);

            SetSize();
            tmrRefresh.Start();

        }

        void carrega_Combo()
        {
            CargaDados.carregacombo(this.cmbView, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=2 ", "Id_Portfolio", "Port_Name", 99);
        }

        public void Set_Portfolio_Values(int Id_Portfolio,DateTime Historical)
        {
            cmbView.SelectedValue = Id_Portfolio;
        }

        // Respond to the form 'Resize' event
        private void frmPerfChart_Resize(object sender, EventArgs e)
        {
            SetSize();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();


        }

        private void SetSize()
        {
            zedGraphControl1.Location = new Point(0, 0);
            // Leave a small margin around the outside of the control
            zedGraphControl1.Size = new Size(ClientRectangle.Width - 0,
                                    ClientRectangle.Height);
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


            PointPairList listDates = new PointPairList();
            PointPairList listMH = new PointPairList();
            PointPairList listBravo = new PointPairList();
            PointPairList listNFund = new PointPairList();
            PointPairList listIBOV = new PointPairList();

            DateTime MaxDate = DateTime.Now;
            DateTime MinDate = DateTime.Now;

            string SQLString = " SELECT Perf_DateTime, " + 
                        " SUM(CASE WHEN Id_Portfolio=43 then curPerformance ELSE 0 END) AS MH, " + 
                        " SUM(CASE WHEN Id_Portfolio=10 then curPerformance ELSE 0 END) AS Nest_Ações, " + 
                        " SUM(CASE WHEN Id_Portfolio=4 then curPerformance ELSE 0 END) AS Nest_Fund, " +
                        " SUM(CASE WHEN Id_Portfolio=1073 then curPerformance ELSE 0 END) AS IBOV " +
                        " FROM NESTRT.dbo.Tb001_Intraday_Performance  " + 
                        " WHERE Perf_DateTime >= convert(varchar, getdate(), 112) " +
                        " GROUP BY Perf_DateTime " + 
                        " ORDER BY Perf_DateTime; ";

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                DateTime TempDate = Convert.ToDateTime(curRow["Perf_DateTime"]);
                if (TempDate > MaxDate) { MaxDate = TempDate; };
                if (TempDate < MinDate) { MinDate = TempDate; };
                if (curRow["MH"].ToString() != "")
                {
                    double tempMH = Convert.ToDouble(curRow["MH"]);
                    double tempBravo = Convert.ToDouble(curRow["Nest_Ações"]);
                    double tempNFund = Convert.ToDouble(curRow["Nest_Fund"]);
                    double tempIBOV = Convert.ToDouble(curRow["IBOV"]);

                    listMH.Add(TempDate.ToOADate(), tempMH);
                    listBravo.Add(TempDate.ToOADate(), tempBravo);
                    listNFund.Add(TempDate.ToOADate(), tempNFund);
                    listIBOV.Add(TempDate.ToOADate(), tempIBOV);

                }
            }
            
            LineItem lineMH = myPane.AddCurve("MH", listMH, Color.FromArgb(102,153,255), SymbolType.None);
            lineMH.Line.Width = 2.0F;

            LineItem lineBravo = myPane.AddCurve("NAções", listBravo, Color.Red, SymbolType.None);
            lineBravo.Line.Width = 2.0F;

            LineItem lineNFund = myPane.AddCurve("NFund", listNFund, Color.Green , SymbolType.None);
            lineNFund.Line.Width = 2.0F;

            LineItem lineIBOV = myPane.AddCurve("IBOV", listIBOV, Color.Gray, SymbolType.None);
            lineIBOV.Line.Fill = new Fill(Color.LightGray, Color.LightGray, -45F);
            lineIBOV.Line.Width = 2.0F;

            // Legend and other
            myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

            myPane.XAxis.Type = AxisType.Date;
            myPane.YAxis.Scale.Format = "0.00%";
            
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0)).ToOADate();
            myPane.XAxis.Scale.Max = MinDate.Add(new TimeSpan(12, 0, 0)).ToOADate();

            myPane.IsFontsScaled = false;
            myPane.XAxis.Scale.FontSpec.Size = 10;
            myPane.YAxis.Scale.FontSpec.Size = 10;
            myPane.Legend.FontSpec.Size = 7;

            myPane.XAxis.Scale.MinorUnit = DateUnit.Minute;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Hour;

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

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            CreateGraph(zedGraphControl1);
        }

        private bool zedGraphControl1_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
            {
                if (this.ControlBox == true)
                {
                    this.ControlBox = false;
                    this.Text = string.Empty;
                }
                else
                {
                    this.ControlBox = true;
                    this.Text = "Intraday Performance";
                }
            } 
            return default(bool);
        }
    }
}