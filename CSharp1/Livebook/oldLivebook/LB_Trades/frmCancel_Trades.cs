using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;

using LiveBook.Business;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
namespace LiveBook
{
    public partial class frmCancel_Trades : Form
    {
        int[,] Array_Ativos_Cancelados  = new int[100,2];
        //int Cont_Cancel = 0;

        int[,] Array_Ativos_Incluidos = new int[100, 6];
        //int Cont_Insert = 0;

        newNestConn curConn = new newNestConn();
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();
        public int Id_User;

        public frmCancel_Trades()
        {
            InitializeComponent();
        }
        public void Carrega_Grid()
        {
            string Id_Portfolio = cmbChoosePortfolio.SelectedValue.ToString();
            string Data_Trade = dtpIniDate.Value.ToString("yyyyMMdd");

            String SQLString;
            dgTrades.Columns.Clear();
            //String_Campos = curUtils.Retorna_ordem(Id_User, 5);
            DataTable tablet = new DataTable();

            SQLString = "SELECT [Id Trade],[Id Order],NestTicker,[Trade Quantity],[Trade Price]," +
            " [Order Login],Book,Section,[RoundLot], " +
            " [Ticker Currency], [Trade Status],[Trade Date], [Id Ticker], " +
            " Broker,[Trade Login] FROM dbo.VW_LB_Trades " +
            " Where [Trade Date] >='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "' and [Trade Date]<= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") +
             "' and [Id Trade Status]<>4" +
            " and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString();

            tablet = curConn.Return_DataTable(SQLString);

            dtg2.DataSource = tablet;

            curUtils.SetColumnStyle(dgTrades, 2);

            dgTrades.Columns.AddField("Cancel");
            dgTrades.Columns["Cancel"].VisibleIndex = 0;
            dgTrades.Columns["Cancel"].Width = 55;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Cancel";
            dtg2.RepositoryItems.Add(item3);
            dgTrades.Columns["Cancel"].ColumnEdit = item3;
            dgTrades.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgTrades.OptionsBehavior.Editable = false;
            /*
            dgTrades.Columns.AddField("Edit");
            dgTrades.Columns["Edit"].VisibleIndex = 0;
            dgTrades.Columns["Edit"].Width = 55;
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Edit";
            dtg2.RepositoryItems.Add(item4);
            dgTrades.Columns["Edit"].ColumnEdit = item4;
            dgTrades.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgTrades.OptionsBehavior.Editable = false;
            dgTrades.Columns["Edit"].Visible = false;
            */
            curUtils.SetColumnStyle(dgTrades, 2);
          }
        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
 
        private void frmEdit_Trades_Load(object sender, EventArgs e)
        {
            

            dtg2.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg2.LookAndFeel.SetSkinStyle("Blue");

                NestDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type= 1", "Id_Portfolio", "Port_Name", 1);


        }

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            string SQLString;
            int Id_Trade = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "Id_Trade");
            int Id_Order = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "Id_Order");

            GridView zz = sender as GridView;
            string Column_Name = zz.FocusedColumn.Caption.ToString();

            if (Column_Name == "Cancel")
            {
                SQLString = "Select Status_Ordem from Tb012_Ordens Where Id_Ordem=" + Id_Order;
                int StatusOrder = Convert.ToInt32(curConn.Execute_Query_String(SQLString));

                int resposta;
                switch (StatusOrder)
                {
                    case 1:
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want cancel this Trade?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        break;
                    case 3:
                        resposta = Convert.ToInt32(MessageBox.Show("This order is already complete. You Want really cancel the entirely order and all trades this order?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        break;
                    default:
                        resposta = 0;
                        break;
                }
                if (resposta == 6)
                {
                    SQLString = "EXEC Proc_Cancela_Trade_Ordem @Id_Trade = " + Id_Trade;
                    resultado = curConn.ExecuteNonQuery(SQLString,1);

                    if (resultado != 0)
                    {
                        Carrega_Grid();
                    }
                    else
                    {
                        MessageBox.Show("Error on Cancel!");
                    }
                }
            }
        }

        private int Retorna_Id(DataRow dr, string Nome_Coluna)
        {
            int Id_order;
            try
            {
                ;
                if (dr != null)
                {

                    object[] items = dr.ItemArray;
                    switch (Nome_Coluna)
                    {
                        case "Id_Trade":
                            Id_order = Convert.ToInt32(items[0].ToString());
                            break;
                        case "Id_Order":
                            Id_order = Convert.ToInt32(items[1].ToString());
                            break;

                        case "Id_Ativo":
                            Id_order = Convert.ToInt32(items[15].ToString());
                            break;
                        default:
                            Id_order = 0;
                            break;
                    }
                }
                else
                {
                    Id_order = 0;
                }
                return Id_order;
            }
            catch(Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                return 0;
            }
        }


        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);
        }

    }
}