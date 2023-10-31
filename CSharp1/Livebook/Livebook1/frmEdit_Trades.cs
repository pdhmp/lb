using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SGN.CargaDados;
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
    public partial class frmEdit_Trades : Form
    {
        int[,] Array_Ativos_Cancelados  = new int[100,2];
        int Cont_Cancel = 0;

        int[,] Array_Ativos_Incluidos = new int[100, 6];
        int Cont_Insert = 0;

        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;

        public frmEdit_Trades()
        {
            InitializeComponent();
        }
        public void Carrega_Grid()
        {
            String StringSQl;
            dgTrades.Columns.Clear();
            //String_Campos = Valida.Retorna_ordem(Id_usuario, 5);
            SqlDataAdapter dt = new SqlDataAdapter();
            DataTable tablet = new DataTable();
            //StringSQl = " Select * from VW_Trades_Day";
            StringSQl = "";
            dt = CargaDados.DB.Return_DataAdapter(StringSQl);
            dt.Fill(tablet);

            dtg2.DataSource = tablet;
            dt.Dispose();
            tablet.Dispose();
            Valida.SetColumnStyle(dgTrades, Id_usuario, "Quantity");

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
            dgTrades.Columns["Cancel"].Visible = false;
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
            Valida_Colunas(Id_usuario, dgTrades);
            Valida.SetColumnStyle(dgTrades, Id_usuario, "Quantity");
          }
        private void Valida_Colunas(int Id_usuario, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {

            string StringSQl;
            SqlDataReader drRules;
            StringSQl = "SET DATEFORMAT DMY;exec sp_Get_Grupo_Nivel @Id_usuario= " + Id_usuario;

            drRules = CargaDados.DB.Execute_Query_DataRead(StringSQl);

            while (drRules.Read())
            {
                if (Nome_Grid.Name == "dgTrades")
                {
                    switch (Convert.ToInt32(drRules["Id_Grupo"]))
                    {
                        case 1:
                            //1 Administrador
                            //dgTrades.Columns["Edit"].Visible = true;
                            //dgTrades.Columns["Edit"].VisibleIndex = 0;
                            dgTrades.Columns["Cancel"].Visible = true;
                            dgTrades.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            //dgTrades.Columns["Edit"].Visible = true;
                            //dgTrades.Columns["Edit"].VisibleIndex = 0;
                            dgTrades.Columns["Cancel"].Visible = true;
                            dgTrades.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 3:
                            //3 Middle
                            break;
                        case 4:
                            //4 Consulta
                            break;
                    }
                }
            }
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();

        }
        private void ExportTo(IExportProvider provider, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = Nome_Grid.CreateExportLink(provider);
            (link as GridViewExportLink).ExpandAll = false;

            link.ExportTo(true);
            provider.Dispose();

            Cursor.Current = currentCursor;
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //progressBarControl1.Position = 0;
        }

        private void dgTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

        }

        private void frmEdit_Trades_Load(object sender, EventArgs e)
        {
            if (Id_usuario == 1)
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 1);
            }
            else
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 1);
            }

        }

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            //int resultado;
            string SQLString;
            int Id_Trade = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "Id Trade");
            int Id_Order = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "Id Order");
            int Id_Ativo = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "Id Ticker");

            GridView zz = sender as GridView;
            string Column_Name = zz.FocusedColumn.Caption.ToString();

            if (Column_Name == "Cancel")
            {
                SQLString = "Select Status_Ordem from Tb012_Ordens Where Id_Ordem=" + Id_Order;
                int StatusOrder = Convert.ToInt32(CargaDados.DB.Execute_Query_String(SQLString));

                int resposta;
                switch (StatusOrder)
                {
                    case 1:
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want cancel to Trade?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
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
                    Array_Ativos_Cancelados[Cont_Cancel, 0] = Id_Ativo;
                    Array_Ativos_Cancelados[Cont_Cancel, 1] = Id_Order;
                    Array_Ativos_Cancelados[Cont_Cancel, 2] = Id_Trade;
                    Cont_Cancel++;
                    /*
                    SQLString = "EXEC Proc_Cancela_Trade_Ordem @Id_Trade = " + Id_Trade;
                    resultado = CargaDados.DB.Execute_Insert_Delete_Update(SQLString);
                    
                    if (resultado != 0)
                    {
                        Carrega_Grid();
                    }
                    else
                    {
                        MessageBox.Show("Error on Cancel!");
                    }
                     */
                }
            }
            if (Column_Name == "Edit")
            {

                frmEditPriceTrade Edita_Trade = new frmEditPriceTrade();
                Edita_Trade.Id_Trade = Id_Trade;
                Edita_Trade.ShowDialog();
                Carrega_Grid();

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
                            Id_order = Convert.ToInt32(items[3].ToString());
                            break;
                        case "Id_Order":
                            Id_order = Convert.ToInt32(items[1].ToString());
                            break;

                        case "Id_Ativo":
                            Id_order = Convert.ToInt32(items[1].ToString());
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
            catch
            {
                return 0;
            }
        }


        private void Inserir_Trade(object sender, EventArgs e)
        {
            Array_Ativos_Incluidos[Cont_Insert, 0] = Convert.ToInt32(cmbTicker.SelectedValue.ToString());
            Array_Ativos_Incluidos[Cont_Insert, 1] = Convert.ToInt32(cmBrooker.SelectedValue.ToString());
            Array_Ativos_Incluidos[Cont_Insert, 2] = Convert.ToInt32(txtQtd.Text.ToString());
            Array_Ativos_Incluidos[Cont_Insert, 3] = Convert.ToInt32(txtPrice.Text.ToString());
            Array_Ativos_Incluidos[Cont_Insert, 4] = Convert.ToInt32(cmbOrder_Type.SelectedValue.ToString());
            Array_Ativos_Incluidos[Cont_Insert, 5] = Convert.ToInt32(cmbStrategy.SelectedValue.ToString());
            Array_Ativos_Incluidos[Cont_Insert, 6] = Convert.ToInt32(cmbSub_Strategy.SelectedValue.ToString());
            lblStatus.Text = "Changed";
            lblStatus.BackColor = Color.Red;
            
            Cont_Insert++;
        }
        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal testa_pos;
                testa_pos = Convert.ToDecimal(this.txtQtd.Text);
                if (testa_pos < 0)
                {
                    cmdInsert_Order_Neg.Enabled = true;
                    cmdInsert_Order.Enabled = false;
                }
                else
                {
                    cmdInsert_Order_Neg.Enabled = false;
                    cmdInsert_Order.Enabled = true;
                }

                if (this.txtPrice.Text != "")
                {
                    decimal Preco = Convert.ToDecimal(txtPrice.Text);
                    this.txtPrice.Text = Preco.ToString("##,###.00#######");
                }
                if (this.txtQtd.Text != "")
                {
                    decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                    this.txtQtd.Text = qtd.ToString("##,###.00#######");
                }

                if (this.txtCash.Text != "")
                {
                    decimal vfin = Convert.ToDecimal(this.txtCash.Text);
                    this.txtCash.Text = vfin.ToString("##,###.00#######");
                }
            }
            catch
            {

            }
        }

    
    }
}