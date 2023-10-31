using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using LiveDLL;

namespace LiveBook
{
    public partial class frmSubscription : LBForm
    {   
        public string TipoProvento;
        public string Ticker;
        public string ValorEmissao;
        public string Percent;
        string Id_Ativo_Inserido = "";
        DataTable tablep = new DataTable();

        newNestConn curConn = new newNestConn();

        public frmSubscription()
        {
            InitializeComponent();
        }

        private void frmSubscription_Load(object sender, EventArgs e)
        {
            lblTransType.Text = TipoProvento;

            lblVlrEmissao.Text = ValorEmissao;
            lblPercent.Text = Percent;
            
            lblTicker.Text = Ticker;
            txtTicker.Text = Ticker.Substring(0, Ticker.Length - 1);
          }

        private void cmdNewSecurity_Click(object sender, EventArgs e)
        {
            string Data = DateTime.Now.ToString("yyyy-MM-dd");
            double IdSecurity = LiveDLL.Utils.ParseToDouble(curConn.Execute_Query_String("SELECT IdSecurity FROM dbo.Tb001_Securities WHERE NestTicker = '" + Ticker + "' OR ExchangeTicker = '" + Ticker + "' "));
            if (IdSecurity <= 0)
            {
                MessageBox.Show("Security not register  ( " + Ticker + " )", "Subscription", MessageBoxButtons.OK);
                return;
            }

            frmCopySecurity CopySec = new frmCopySecurity();
            CopySec.txtOldId.Text       = IdSecurity.ToString();
            CopySec.txtName.Text        = txtTicker.Text;
            CopySec.txtNestTicker.Text  = txtTicker.Text;
            CopySec.txtExpiration.Value = Convert.ToDateTime(curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',22,'" + Convert.ToDateTime(Data).ToString("yyyyMMdd") + "',2,1)"));
            CopySec.ShowDialog();

            // -------------------------------------------------------------------

            Id_Ativo_Inserido = CopySec.Id_Ativo_Inserido;
            CopySec.Dispose();

            if (Id_Ativo_Inserido == null )
            {
                MessageBox.Show("Do you need to create a new ticker.", "Subscription", MessageBoxButtons.OK);
                return;
            }

            if (CopySec.Id_Ativo_Inserido != "inserted")
            {
                frmEditSecurity frm = new frmEditSecurity();
                frm.txtsearch.Text = txtTicker.Text;
                frm.ShowDialog();
                frm.Dispose();
            }

            DateTime DateNow = Convert.ToDateTime(curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',-1,'" + Convert.ToDateTime(Data).ToString("yyyyMMdd") + "',2,1)"));


            System.Text.StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat(" SELECT  Id_Account, [ID Book] AS idBook, [ID Section] AS idSection, [Book], [Section], Portfolio, Position, CONVERT(INT, ( [position] * ( {0} / 100 ))) AS PositionAdjusted ", Percent.Replace(",", "."));
            sbSQL.Append(" FROM NESTDB.dbo.Tb000_Historical_Positions A, VW_PortAccounts C ");
            // sbSQL.Append(" WHERE [Date Now] = '2013-05-23'  ");
            sbSQL.AppendFormat(" WHERE [Date Now] = '{0}'", DateNow.ToString("yyyy-MM-dd")) ;
            sbSQL.AppendFormat(" AND   Ticker	 = '{0}'", lblTicker.Text);
            sbSQL.Append(" AND A.[Id Portfolio] = C.Id_Portfolio ");
            sbSQL.Append(" AND C.id_Port_Type   = 2  ");
            sbSQL.Append(" AND C.id_broker      = 31 ");
            sbSQL.Append(" AND   [Id Portfolio] IN  ");
            sbSQL.Append(" ( ");
            sbSQL.Append(" 		SELECT Id_Portfolio FROM Tb002_Portfolios  WHERE id_Port_Type = 2 AND Discountinued = 0 ");
            sbSQL.Append(" ) ");
            sbSQL.Append(" ORDER BY [Id Portfolio], [id position] ");
                        
            curConn = null;
            using (curConn = new newNestConn())
            {
                tablep = new DataTable();
                tablep = curConn.Return_DataTable(sbSQL.ToString());

                dtg.DataSource = tablep;
                tablep.Dispose();
            }
        }

        private void cmdCreateTrades_Click(object sender, EventArgs e)
        {
            string sSQLOrdem = "";
            string sSQLTrade = "";
            string data_insert = DateTime.Now.ToString("yyyyMMdd");
            string Side = "";
            string tempVal = "";
            string IdOrder = "";

            foreach ( DataRow dr in tablep.Rows )
            {
                if ( Convert.ToDouble(dr["PositionAdjusted"]) < 0) Side = "2"; else Side = "1";

                sSQLOrdem =
                "  exec proc_insert_Tb012_Ordens " +
                "  @Id_Ativo_1                  =" + Id_Ativo_Inserido +
                ", @Quantidade_2                =" + dr["PositionAdjusted"].ToString().Replace(",", ".") +
                ", @Preco_3                     =" + txtPrice.Text.Replace(",", ".") +
                ", @Valor_Financeiro_4          =" + ( Convert.ToDouble( txtPrice.Text) * Convert.ToDouble(dr["PositionAdjusted"] ) ).ToString().Replace(",", ".") +
                ", @Book_5                      =" + dr["idBook"].ToString() +
                ", @Section_6                   =" + dr["idSection"].ToString() +
                ", @Tipo_Mercado_7              =" + 0 +
                ", @Operador_8                  =" + LiveDLL.NUserControl.Instance.User_Id.ToString() +
                ", @Data_Abert_Ordem_9          ='" + data_insert + "'" +
                ", @Status_Ordem_11             =1" +
                ", @Id_Corretora_14             =31"+
                ", @Data_Valid_Ordem_15         ='" + data_insert + "'" +
                ", @Id_Account_17               =" + dr["Id_Account"].ToString() +
                ", @Id_Order_Broker_16          =0" +
                ", @Id_Ticker_Debt_18           =0" +
                ", @IdSide                      = " + Side;

                tempVal = curConn.Execute_Query_String(sSQLOrdem);
                IdOrder = curConn.Execute_Query_String("SELECT @@IDENTITY");

                // ------------------------------------------------------------------------------

                sSQLTrade = "exec sp_insert_Tb013_Trades " +
                "  @Id_Ordem_1           =" + IdOrder +
                ", @Quantidade_2         =" + dr["PositionAdjusted"].ToString().Replace(",", ".") +
                ", @Preco_3              =" + txtPrice.Text.Replace(",", ".") +
                ", @Valor_Financeiro_4   =" + (Convert.ToDouble(txtPrice.Text) * Convert.ToDouble(dr["PositionAdjusted"])).ToString().Replace(",", ".") +
                ", @Corretora_5         =31"+
                ", @Operador_7          ="  + LiveDLL.NUserControl.Instance.User_Id.ToString() +
                ", @Data_Trade_8        ='" + data_insert.ToString() + "'" +
                ", @Status_Trade_12     =1";

                curConn.ExecuteNonQuery(sSQLTrade, 1);
            }
        }
    }
}
