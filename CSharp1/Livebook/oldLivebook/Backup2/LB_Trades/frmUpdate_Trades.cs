using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using SGN.Validacao;
using SGN.Business;
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
namespace SGN
{
    public partial class frmUpdate_Trades : Form
    {
        int[,] Array_Ativos_Cancelados  = new int[100,2];

        int[,] Array_Ativos_Incluidos = new int[100, 6];

        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_User;

        public frmUpdate_Trades()
        {
            InitializeComponent();
        }
        public void Carrega_Grid()
        {
            string Id_Portfolio = cmbChoosePortfolio.SelectedValue.ToString();
            string Data_Trade = dtpIniDate.Value.ToString("yyyyMMdd");
            
            String SQLString;
            dgUpTrades.Columns.Clear();
            //String_Campos = Valida.Retorna_ordem(Id_User, 5);

            SQLString = "SELECT [Id Trade],[Id Order],NestTicker,[Trade Quantity],[Trade Price]," +
            " [Order Login],Book,Section,[RoundLot], " +
            " [Ticker Currency], [Trade Status],[Trade Date], [Id Ticker], " +
            " Broker,[Trade Login] FROM dbo.VW_LB_Trades " +
            " Where [Trade Date] ='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") +
             "' and [Id Trade Status]<>4" +
            " and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString();


            DataTable tablet = CargaDados.curConn.Return_DataTable(SQLString);

            dtg2.DataSource = tablet;

            dgUpTrades.Columns.AddField("Update");
            dgUpTrades.Columns["Update"].VisibleIndex = 0;
            dgUpTrades.Columns["Update"].Width = 55;
            RepositoryItemButtonEdit item = new RepositoryItemButtonEdit();
            item.Buttons[0].Tag = 1;
            item.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item.Buttons[0].Caption = "Update";
            dtg2.RepositoryItems.Add(item);
            dgUpTrades.Columns["Update"].ColumnEdit = item;
            dgUpTrades.Columns["Update"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            //dgAbertas.Columns.AddField("Edit");
            //dgAbertas.Columns["Edit"].VisibleIndex = 0;
            //dgAbertas.Columns["Edit"].Width = 55;
            //RepositoryItemButtonEdit item2 = new RepositoryItemButtonEdit();
            //item2.Buttons[0].Tag = 1;
            //item2.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            //item2.Buttons[0].Caption = "Edit";
            // item2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(item2_ButtonClick);
            //dtg2.RepositoryItems.Add(item2);
            //dgAbertas.Columns["Edit"].ColumnEdit = item2;
            //dgAbertas.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgUpTrades.OptionsBehavior.Editable = false;
            // dgAbertas.Columns["Edit"].Visible = false;

           // Valida.SetColumnStyle(dgUpTrades, Id_User);
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

            CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type= 1", "Id_Portfolio", "Port_Name", 1);
        }

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            string SQLString;
            int Id_Trade = Retorna_Id(dgUpTrades.GetDataRow(dgUpTrades.FocusedRowHandle), "Id_Trade");
            int Id_Order = Retorna_Id(dgUpTrades.GetDataRow(dgUpTrades.FocusedRowHandle), "Id_Order");

            string Data = dtpnewDate.Value.ToString("yyyyMMdd");

            GridView zz = sender as GridView;
            string Column_Name = zz.FocusedColumn.ToString();

            if (Column_Name == "Update")
            {
                int  resposta = Convert.ToInt32(MessageBox.Show("Do you really want to Update the Trade?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                if (resposta == 6)
                {
                    SQLString = "EXEC Proc_Update_Date_Trades @Id_Order = " + Id_Order + ", @New_Date='" + Data + "'";
                    resultado = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

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
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

                return 0;
            }
        }


        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);
        }

        private void cmbChoosePortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}