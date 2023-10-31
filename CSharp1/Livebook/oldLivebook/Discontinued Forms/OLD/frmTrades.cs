using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using SGN.Business;
using SGN.Validacao;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SGN.Tela_Divididas
{
    public partial class frmTrades : LBForm
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();

        public frmTrades()
        {
            InitializeComponent();
        }

        private void frmTrades_Load(object sender, EventArgs e)
        {
            
            
            dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTrade.LookAndFeel.SetSkinStyle("Blue");

        }

        private void dgTrades_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgTrades);

        }

        private void dgTrades_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;
        }

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            string SQLString;
            int Id_Trade = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "dgTrades");
            int Id_Order = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "dgTrades2");

            GridView zz = sender as GridView;
            string Column_Name = zz.FocusedColumn.Caption.ToString();

            if (Column_Name == "Cancel")
            {
                SQLString = "Select Status_Ordem from Tb012_Ordens Where Id_Ordem=" + Id_Order;
                int StatusOrder = Convert.ToInt32(CargaDados.curConn.Execute_Query_String(SQLString));

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
                    SQLString = "EXEC Proc_Cancela_Trade_Ordem @Id_Trade = " + Id_Trade;
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
            if (Column_Name == "Edit")
            {

                frmEditPriceTrade Edita_Trade = new frmEditPriceTrade();
                Edita_Trade.Id_Trade = Id_Trade;
                Edita_Trade.ShowDialog();
                Carrega_Grid();

            }
        }

        private void dgTrades_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgTrades);

        }

        private void dgTrades_EndGrouping(object sender, EventArgs e)
        {
            dgTrades.ExpandAllGroups();

            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgTrades.Columns["Quantity"]);
            ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Cash Flow", dgTrades.Columns["Cash Flow"]);
            ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgTrades_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgTrades);
        }

        private void dgTrades_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgTrades);

        }

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }
        private int Retorna_Id(DataRow dr, string Nome_Grid)
        {
            int Id_order;
            try
            {
                ;
                if (dr != null)
                {

                    object[] items = dr.ItemArray;
                    switch (Nome_Grid)
                    {
                        case "dgAbertas":
                            Id_order = Convert.ToInt32(items[3].ToString());
                            break;
                        case "dgTrades":
                            Id_order = Convert.ToInt32(items[0].ToString());
                            break;

                        case "dgTrades2":
                            Id_order = Convert.ToInt32(items[1].ToString());
                            break;

                        case "dgPositions":
                            Id_order = Convert.ToInt32(items[2].ToString());
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
        private void ShowColumnSelector() { ShowColumnSelector(true, dgTrades); }
        bool show = false;
        private void ShowColumnSelector(bool showForm, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (show)
            {
                if (showForm) Nome_Grid.ColumnsCustomization();
            }
            else
            {
                if (showForm) Nome_Grid.DestroyCustomization();
            }
        }

        public void Carrega_Grid()
        //função q pega o tab selecionado e carrega o grid dele
        {
            string SQLString;
            string data_now = DateTime.Now.ToString("yyyyMMdd");

            dgTrades.Columns.Clear();
            //String_Campos = Valida.Retorna_ordem(Id_User, 5);
            //SQLString = " Select * from VW_Trades_Day";

            SQLString = "Select [Id Trade],[Id Order],Ticker,[Trade Quantity],[Trade Price],[Cash],Rebate,Login,Strategy," +
             " [Sub Strategy],[Round Lot],Portfolio,[Ticker Currency], [Status Trade],[Trade Date]," +
             " [Id Ticker],Broker,TimeStamp from dbo.Vw_Ordens_Trades_En" +
             " Where [Trade Date]='" + data_now + "' and [Status Trade]<>4";

            DataTable tablet = CargaDados.curConn.Return_DataTable(SQLString);

            dtgTrade.DataSource = tablet;

            Valida.SetColumnStyle(dgTrades, 2);

            dgTrades.Columns.AddField("Cancel");
            dgTrades.Columns["Cancel"].VisibleIndex = 0;
            dgTrades.Columns["Cancel"].Width = 55;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Cancel";
            dtgTrade.RepositoryItems.Add(item3);
            dgTrades.Columns["Cancel"].ColumnEdit = item3;
            dgTrades.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgTrades.OptionsBehavior.Editable = false;
            dgTrades.Columns["Cancel"].Visible = false;

            dgTrades.Columns.AddField("Edit");
            dgTrades.Columns["Edit"].VisibleIndex = 0;
            dgTrades.Columns["Edit"].Width = 55;
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Edit";
            dtgTrade.RepositoryItems.Add(item4);
            dgTrades.Columns["Edit"].ColumnEdit = item4;
            dgTrades.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgTrades.OptionsBehavior.Editable = false;
            dgTrades.Columns["Edit"].Visible = false;
            Valida_Colunas(dgTrades);
            Valida.SetColumnStyle(dgTrades, 2);


        }

        private void Valida_Colunas(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {

            DataTable curTable;

            string SQLString = "exec sp_Get_Grupo_Nivel @Id_usuario= " + NestDLL.NUserControl.Instance.User_Id;

            curTable = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in curTable.Rows)
            {
                if (Nome_Grid.Name == "dgTrades")
                {
                    switch (Convert.ToInt32(curRow["Id_Grupo"]))
                    {
                        case 1:
                            //1 Administrador
                            dgTrades.Columns["Edit"].Visible = true;
                            dgTrades.Columns["Edit"].VisibleIndex = 0;
                            dgTrades.Columns["Cancel"].Visible = true;
                            dgTrades.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            dgTrades.Columns["Edit"].Visible = true;
                            dgTrades.Columns["Edit"].VisibleIndex = 0;
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
    }
}