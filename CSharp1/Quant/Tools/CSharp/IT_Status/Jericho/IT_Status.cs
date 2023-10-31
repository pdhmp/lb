using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCustomControls;
using NestDLL;



namespace IT_Status
{
    public partial class IT_Status : Form
    {
        public IT_Status()
        {
            InitializeComponent();
            dgcSecurities.DataSource = bndLasts;
            dgcOrders.DataSource = bndQsegs;
            dgSummary.DataSource = bndSummary;
            dm1.Value = DateTime.Today;
            dm2.Value = DateTime.Today;            
        }



        private BindingSource bndLasts = new BindingSource();
        private BindingSource bndQsegs = new BindingSource();
        private BindingSource bndSummary = new BindingSource();
        

        private bool flagMouseDown = false;

        private void getHistoricalQSEGS(DateTime _iniData, DateTime _endDate)
        {
            //pegar os ids dos qsegs
            List<int> qsegIds = GetIndexComponents(281020);
            foreach (int id in qsegIds)
            {
                addQSEGValues(_iniData, _endDate, id, 101);
                addQSEGValues(_iniData, _endDate, id, 102);
            }
        }

        private void getSummary(DateTime _iniDate)
        {
            //count last
            addCounterSummary(_iniDate, 1, 1);
            //count volume
            addCounterSummary(_iniDate, 6, 1);
            //count tr index
            addCounterSummary(_iniDate, 101, 1);
            //count open
            addCounterSummary(_iniDate, 8, 1);
            //count high
            addCounterSummary(_iniDate, 4, 1);
            //count low
            addCounterSummary(_iniDate, 3, 1);
            //count vwap
            addCounterSummary(_iniDate, 5, 1);
            
        }

        private void addCounterSummary(DateTime _iniDate, int _srtype, int source)
        {
            string queryString = "SELECT T.PRECO, count(O.srvalue) " +
                "FROM NESTSRV06.NESTDB.dbo.Tb050_Preco_Acoes_Onshore O, NESTSRV06.NESTDB.dbo.Tb116_Tipo_Preco T " +
                "where O.srdate ='" + _iniDate.ToString("yyyyMMdd") + "' and O.srtype = " + _srtype + " and O.source = " + source + " AND O.SRTYPE = T.ID_TIPO_PRECO " +
                "GROUP BY T.PRECO";

            DataTable DataTb = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                DataTb = curConn.Return_DataTable(queryString);
            }
                        
            if (DataTb.Rows.Count > 0)
            {
                bndSummary.Add(new Summary((string)DataTb.Rows[0][0], (int)DataTb.Rows[0][1]));
            }

        }

        private void addQSEGValues(DateTime dataIni, DateTime dataFim, int idSecurity, int srType)
        {
            //para cada qseg, pego o tr index e o tr pre auc price.
            string _qsegName = "";
            DateTime _date= DateTime.Today;
            string _priceType = "";
            double _price= 0;
            double _priceDm1 = 0;
            DataTable DataTb = new DataTable();

            string query = "SELECT S.NESTTICKER, P.SRDATE, T.PRECO, P.SRVALUE " +
                "FROM NESTSRV06.NESTDB.dbo.Tb053_Precos_Indices P, NESTSRV06.NESTDB.dbo.Tb116_Tipo_Preco T, NESTSRV06.NESTDB.dbo.Tb001_Securities S " +
                "WHERE P.IDSECURITY = " + idSecurity + " AND P.SRDATE = '" + dataFim.ToString("yyyyMMdd") + "' AND P.SRTYPE = " + srType + " AND P.SRTYPE = T.ID_TIPO_PRECO AND P.IDSECURITY = S.IDSECURITY";

            using (newNestConn curConn = new newNestConn(true))
            {
                DataTb = curConn.Return_DataTable(query);
            }
            
            if (DataTb.Rows.Count > 0)
            {
                _qsegName = (string)DataTb.Rows[0][0];
                _date = DateTime.Parse(DataTb.Rows[0][1].ToString());
                _priceType = (string)DataTb.Rows[0][2];
                _price = (double)DataTb.Rows[0][3];
            }
            query = "SELECT SRVALUE "+
                "FROM NESTSRV06.NESTDB.dbo.Tb053_Precos_Indices "+
                "WHERE IDSECURITY = " + idSecurity + " AND SRDATE = '" + dataIni.ToString("yyyyMMdd") + "' AND SRTYPE = "+srType;

            using (newNestConn curConn = new newNestConn(true))
            {
                DataTb = curConn.Return_DataTable(query);
            }
            if (DataTb.Rows.Count > 0)
            {
                _priceDm1 = (double)DataTb.Rows[0][0];
                bndQsegs.Add(new QSEGS(_qsegName, _date, _priceType, _price, _priceDm1));
            }            
        }

        private List<int> GetIndexComponents(int index)
        {
            DataTable componentsTable;

            List<int> Components = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "     JOIN  " +
                "     NESTSRV06.NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite =  " + index;

            using (newNestConn conn = new newNestConn(true))
            {
                componentsTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < componentsTable.Rows.Count; i++)
            {
                Components.Add((int)componentsTable.Rows[i][0]);
            }

            return Components;
        }

        private void getHistoricalData(DateTime dataIni, DateTime dataFim, int underlying, int srType, int source)
        {
            //price type, source, yesterdday, prev, diff...
            //GET DI
            string query = "SELECT S.NESTTICKER as Symbol, F.SRDATE as Date, F.SRVALUE as Value, T.PRECO as TipoPreco " +
                "FROM NESTSRV06.NESTDB.dbo.Tb059_Precos_Futuros F, NESTSRV06.NESTDB.dbo.Tb116_Tipo_Preco T, NESTSRV06.NESTDB.dbo.Tb001_Securities S " +
                "WHERE F.SrDate > '"+dataIni.ToString("yyyyMMdd") +"' AND F.SrDate <= '"+dataFim.ToString("yyyyMMdd")+"' and F.SrType = "+srType+" and F.Source = "+source+" AND F.SRTYPE = T.ID_TIPO_PRECO AND F.IDSECURITY = S.IDSECURITY " +
                "AND F.IdSecurity in ( " +
                 "SELECT IdSecurity " +
                "FROM NESTSRV06.NESTDB.dbo.Tb001_Securities " +
                "WHERE IdUnderlying = "+underlying+ " and IdSecurity <> "+underlying+")";

            DataTable DataTb = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                DataTb = curConn.Return_DataTable(query);                
            }
            string _symbol = "";
            DateTime _date = new DateTime();
            double _price = 0;
            string _tipoPreco = "";
            for (int i = 0; i < DataTb.Rows.Count; i++)
            {
                _symbol = (string)DataTb.Rows[i][0];

                //_date = DateTime.Parse((string)DataTb.Rows[i][1]);
                _date = DateTime.Parse(DataTb.Rows[i][1].ToString());

                _price = (double)DataTb.Rows[i][2];
                _tipoPreco = (string)DataTb.Rows[i][3];
                bndLasts.Add(new Prices(_symbol, _date, _price,_tipoPreco));
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            if (!flagMouseDown)
            {
                dgvOrders.LayoutChanged();
                dgvOrders.UpdateGroupSummary();
                dgvOrders.RefreshData();

                dgvSecurities.LayoutChanged();
                dgvSecurities.UpdateGroupSummary();
                dgvSecurities.RefreshData();
            }
        }

        private void GridMouseDown(object sender, MouseEventArgs e)
        {
            flagMouseDown = true;
        }
        private void GridMouseUp(object sender, MouseEventArgs e)
        {
            flagMouseDown = false;
        }

        private void frmJericho_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bndLasts.Clear();
            bndSummary.Clear();
            bndQsegs.Clear();

            progressBar.Value = 0;
            this.Refresh();
            getHistoricalData(dm2.Value, dm1.Value, 1079, 312,22);//DI
            progressBar.Value = 15;
            this.Refresh();
            getHistoricalData(dm2.Value, dm1.Value, 1638,312,22);//coffe
            progressBar.Value = 30;
            this.Refresh();
            getHistoricalData(dm2.Value, dm1.Value, 484324,312,22);//corn
            progressBar.Value = 45;
            this.Refresh();
            getHistoricalData(dm2.Value, dm1.Value, 1073,1200,22);//ind
            progressBar.Value = 60;
            this.Refresh();
            getHistoricalQSEGS(dm2.Value, dm1.Value);
            progressBar.Value = 75;
            this.Refresh();
            getSummary(dm1.Value);
            progressBar.Value = 90;
            this.Refresh();
            updateQSEGSFutures(dm1.Value);
            progressBar.Value = 0;
            this.Refresh();
        }

        private void updateQSEGSFutures(DateTime _date)
        {
            string query = "SELECT count(idsecurity) "+
                "FROM NESTSRV06.NESTDB.dbo.Tb053_Precos_Indices "+
                "where srdate= '"+_date.ToString("yyyyMMdd")+"' and idsecurity in ("+
                 "sELECT distinct(SC.Id_Ticker_Component) "+
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) "+
                     "JOIN  "+
                     "NESTSRV06.NESTDB.dbo.Tb001_Securities S  "+
                     "ON Id_Ticker_Component = idsecurity  "+
                "WHERE  Id_Ticker_Composite = 281020)";

            DataTable DataTb = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                DataTb = curConn.Return_DataTable(query);
            }
            if (DataTb.Rows.Count > 0)
            {
                lbQSEGS.Text = DataTb.Rows[0][0].ToString();
            }
            

            query = "SELECT COUNT(SRVALUE) " +
                    "FROM nestsrv06.NESTDB.dbo.Tb059_Precos_Futuros " +
                    "WHERE SRDATE = '" + _date.ToString("yyyyMMdd") + "' AND SRTYPE = 312 AND SOURCE = 22 " +
                    "AND IDSECURITY IN (" +
                        "SELECT IdSecurity FROM NESTSRV06.NESTDB.dbo.Tb001_Securities " +
                        "WHERE IdUnderlying IN (1079, 1638, 484324, 1073))";
            using (newNestConn curConn = new newNestConn(true))
            {
                DataTb = curConn.Return_DataTable(query);
            }
            if (DataTb.Rows.Count > 0)
            {
                lbFuturos.Text = DataTb.Rows[0][0].ToString();
            }
            this.Refresh();
        }

        private void IT_Status_Load(object sender, EventArgs e)
        {
            lbQSEGS.Text = "N/A";
            lbFuturos.Text = "N/A";
            this.Refresh();
        }       
    }
}
