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
    public partial class frmQuickChart : LBForm
    {
        newNestConn curConn = new newNestConn();

        private int _Id_Ticker=0;

        public int Id_Ticker
        {
            get { return _Id_Ticker; }
            set { _Id_Ticker = value; zedGraphControl1.Visible = false; CreateGraph(zedGraphControl1); }
        }
        
        public frmQuickChart()
        {
            InitializeComponent();
        }


        private void frmPerfChart_Load(object sender, EventArgs e)
        {
            SetSize();
            tmrRefresh.Start();
            CreateGraph(zedGraphControl1);
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

            if (_Id_Ticker == 0)
            {
                zgc.Visible = false;
                return;
            }
            else
            {
                zgc.Visible = true;
            }

            int Id_Portfolio;

            this.Cursor = Cursors.WaitCursor;

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
            PointPairList listSeries = new PointPairList();
            PointPairList listBravo = new PointPairList();
            PointPairList listNFund = new PointPairList();
            PointPairList listIBOV = new PointPairList();

            string curTicker = curConn.Execute_Query_String("SELECT NestTicker FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=" + _Id_Ticker);

            this.Text = "3m Chart - " + curTicker;

            string curExchange = curConn.Execute_Query_String("SELECT IdPrimaryExchange FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=" + _Id_Ticker);

            int IdBench = _Id_Ticker;
            string TableName = "";
            int TipoPreco = 1;

            if (curExchange == "2")
            {
                IdBench = 1073;
                TipoPreco = 101;
                TableName = "NESTDB.dbo.Tb050_Preco_Acoes_Onshore";
            }
            else
            {
                IdBench = 5310;
                TipoPreco = 1;
                TableName = "NESTDB.dbo.Tb051_Preco_Acoes_Offshore";
            }

            if (curExchange == "1")
            {
                IdBench = 5049;
                TipoPreco = 30;
                TableName = "NESTDB.dbo.Tb059_Precos_Futuros";
            }

            DateTime MaxDate = DateTime.Now;
            DateTime MinDate = DateTime.Now;

            bool FirstVal = true;

            double iniSeries = 1;
            double iniBench = 1;

            string SQLString = " SELECT B.Data_Hora_Reg, A.Valor AS Series, B.Valor AS Bench FROM  " +
                        " (SELECT Data_Hora_Reg, Valor FROM " + TableName + " WHERE Id_Ativo=" + _Id_Ticker + " AND Tipo_Preco=" + TipoPreco + ") A " + 
                        " RIGHT JOIN  " +
                        " (SELECT TOP 90 Data_Hora_Reg, Valor FROM NESTDB.dbo.Tb053_Precos_Indices WHERE Id_Ativo=" + IdBench + " AND Tipo_Preco=1 ORDER BY Data_Hora_Reg DESC) B " +
                        " ON A.Data_Hora_Reg=B.Data_Hora_Reg " +
                        " ORDER BY Data_Hora_Reg";

            DataTable curTable = curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                if (FirstVal && curRow["Series"].ToString() != "")
                {
                    iniSeries = Convert.ToDouble(curRow["Series"]);
                    iniBench = Convert.ToDouble(curRow["Bench"]);

                    FirstVal = false;
                }
                DateTime TempDate = Convert.ToDateTime(curRow["Data_Hora_Reg"]);
                if (TempDate > MaxDate) { MaxDate = TempDate; };
                if (TempDate < MinDate) { MinDate = TempDate; };
                if (curRow["Series"].ToString() != "")
                {
                    double tempSeries = Convert.ToDouble(curRow["Series"]);
                    double tempIBOV = Convert.ToDouble(curRow["Bench"]);

                    listSeries.Add(TempDate.ToOADate(), tempSeries / iniSeries - 1);
                    listIBOV.Add(TempDate.ToOADate(), tempIBOV / iniBench - 1);

                }
            }
            
            LineItem lineMH = myPane.AddCurve("Series", listSeries, Color.FromArgb(102,153,255), SymbolType.None);
            lineMH.Line.Width = 2.0F;

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

            myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
            myPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();


            //myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
            //myPane.XAxis.Scale.Max = MinDate.Add(new TimeSpan(12, 0, 0, 0)).ToOADate();

            myPane.IsFontsScaled = false;
            myPane.XAxis.Scale.FontSpec.Size = 10;
            myPane.YAxis.Scale.FontSpec.Size = 10;
            myPane.Legend.FontSpec.Size = 7;

            myPane.XAxis.Scale.MinorUnit = DateUnit.Day;
            myPane.XAxis.Scale.MinorUnit = DateUnit.Day;

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
                    this.Text = "3m Chart";
                }
            } 
            return default(bool);
        }
    }
}