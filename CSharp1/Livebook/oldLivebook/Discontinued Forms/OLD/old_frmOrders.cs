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


namespace SGN
{
    public partial class old_frmOrders : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();

        public old_frmOrders()
        {
            InitializeComponent();
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {
            dtg2.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg2.LookAndFeel.SetSkinStyle("Blue");


            dtgFec.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgFec.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgFec.LookAndFeel.SetSkinStyle("Blue");


            dtgCancel.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgCancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgCancel.LookAndFeel.SetSkinStyle("Blue");


            dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTrade.LookAndFeel.SetSkinStyle("Blue");

            Carrega_Grid();

        }

        public void Carrega_Grid()
        {
            string data_now = DateTime.Now.ToString("yyyyMMdd");

            switch (tbMenu.SelectedTab.Text)
            {
                case "Open":
                    string SQLString;
                    dgAbertas.Columns.Clear();

                    SQLString = " Select [Id Portfolio],[Portfolio],Done,Leaves,[Avg Price Trade],[Id Order],[Id Ticker], Ticker,Total," +
                                " [Order Price],[Cash Order],[Ticker Currency],[Order Date],[Login],[Id Strategy],Strategy," +
                                " [Id Sub Strategy],[Sub Strategy],[Order Status],[Id Account],[Account]," +
                                " [Broker] from  dbo.VW_Group_Ordens_Trades_EN" +
                                " WHERE [Order Date]='" + data_now + "' AND [Id Order Status] NOT IN (3, 4, 5)  " +
                                " order by Done desc ,[Id Ticker]";

                        //" Select * from VW_Ordens_abertas order by Ticker asc";

                    DataTable table = CargaDados.curConn.Return_DataTable(SQLString);
                    dtg2.DataSource = table;

                    dgAbertas.Columns.AddField("Cancel");
                    dgAbertas.Columns["Cancel"].VisibleIndex = 0;
                    dgAbertas.Columns["Cancel"].Width = 55;
                    RepositoryItemButtonEdit item = new RepositoryItemButtonEdit();
                    item.Buttons[0].Tag = 1;
                    item.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                    item.Buttons[0].Caption = "Cancel";
                    //item.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(item_ButtonClick);
                    dtg2.RepositoryItems.Add(item);
                    dgAbertas.Columns["Cancel"].ColumnEdit = item;
                    dgAbertas.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                    dgAbertas.Columns["Cancel"].Visible = false;

                    dgAbertas.OptionsBehavior.Editable = false;
                    Valida_Colunas(dgAbertas);
                    Valida.SetColumnStyle(dgAbertas,2,"Total");
                    dgAbertas.ExpandAllGroups();
                    break;

                case "Done":

                    SQLString = "  Select [Id Portfolio],[Portfolio],Done,Leaves,[Avg Price Trade],[Id Order],[Id Ticker], Ticker,Total," +
                    " [Order Price],[Cash Order],[Ticker Currency],[Order Date],[Login],[Id Strategy],Strategy,[Id Sub Strategy],[Sub Strategy]" +
                    " [Order Status],[Id Account],[Account], [Broker]" +
                    " from  dbo.VW_Group_Ordens_Trades_EN" +
                    " WHERE [Order Date]='" + data_now + "' AND [Id Order Status] =3" +
                    " order by [Id Ticker]";

                    DataTable tablec = CargaDados.curConn.Return_DataTable(SQLString);
                    dtgFec.DataSource = tablec;

                    Valida.SetColumnStyle(dgFechadas, 2, "Total");
                    Valida_Colunas(dgFechadas);
                    dgAbertas.ExpandAllGroups();
                    break;

                case "Cancel":

                    SQLString = " select * from  dbo.VW_Group_Ordens_Trades_EN " +
                                " WHERE [Order Date]='" + data_now + "' AND [Id Order Status] = 4" +
                                " order by [Id Ticker]";

                    DataTable tablel = CargaDados.curConn.Return_DataTable(SQLString);

                    dtgCancel.DataSource = tablel;

                    Valida.SetColumnStyle(dgCanceladas, 2, "Total");
                    Valida_Colunas(dgCanceladas);
                    dgAbertas.ExpandAllGroups();
                    break;


                case "Trades":
                    dgTrades.Columns.Clear();

                    SQLString = "Select [Id Portfolio],[Portfolio],[Id Trade],[Id Order],Ticker,[Trade Quantity],[Trade Price],[Cash],Rebate,[Trade Login],Strategy," +
                    " [Sub Strategy],[Round Lot],Account,[Ticker Currency], [Status Trade],[Trade Date]," +
                    " [Id Ticker],Broker,TimeStamp from dbo.Vw_Ordens_Trades_En" +
                    " Where [Trade Date]='" + data_now + "' and [Status Trade]<>4";
                    
                    DataTable tablet = CargaDados.curConn.Return_DataTable(SQLString);

                    dtgTrade.DataSource = tablet;

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
                    Valida.SetColumnStyle(dgTrades, 2, "Trade Quantity"); 
                    Valida_Colunas(dgTrades);

                    break;

            }

        }

        private void dgAbertas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgAbertas);
        }

        private void dgAbertas_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgAbertas_DoubleClick(object sender, EventArgs e)
        {

            int resultado;
            int Id_order = Convert.ToInt32(Retorna_Id(dgAbertas.GetDataRow(dgAbertas.FocusedRowHandle), "dgAbertas"));

            int Id_Account = Convert.ToInt32(dgAbertas.GetRowCellValue(dgAbertas.FocusedRowHandle, "Id Account"));
//            (string)dgAbertas.GetRowCellValue(dgNotDB.FocusedRowHandle, "Id Portfolio");
            string  Quantity = Retorna_Id(dgAbertas.GetDataRow(dgAbertas.FocusedRowHandle), "Quantity");
            string Ticker = Retorna_Id(dgAbertas.GetDataRow(dgAbertas.FocusedRowHandle), "Ticker");
            
            GridView zz = sender as GridView;

            if (Id_order != 0)
            {
                string Column_Name = zz.FocusedColumn.Caption.ToString();

                switch (Column_Name)
                {
                    case "Edit":

                        Negocios.Editar_Ordem(Id_order);
                        resultado = 1;
                        break;

                    case "Cancel":
                        resultado = Negocios.Cancela_Ordem(Id_order,0);
                        break;

                    default:

                        if (Id_Account == 55)
                        {
                            resultado = Negocios.Inserir_Divisao(Id_order, Ticker, Quantity);
                        }
                        else
                        {
                            resultado = Negocios.Inserir_Trade(Id_order);
                        }
                        break;
                }
                if (NestDLL.NUserControl.Instance.User_Id != 9)
                {
                    if (resultado != 0)
                    {
                        Carrega_Grid();
                    }
                }
                // ButtonEdit edit = sender as ButtonEdit;
                // GridView view = (edit.Parent as GridControl).FocusedView as GridView;
                // string name = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["Id Portfolio"]).ToString();
                // MessageBox.Show(name);
            }
        }

        private void dgAbertas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgAbertas);

        }

        private void dgAbertas_EndGrouping(object sender, EventArgs e)
        {
            dgAbertas.ExpandAllGroups();

            dgAbertas.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgAbertas.Columns["Quantity"]);
            ((GridSummaryItem)dgAbertas.GroupSummary[dgAbertas.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            dgAbertas.GroupSummary.Add(SummaryItemType.Sum, "Cash Flow", dgAbertas.Columns["Cash Flow"]);
            ((GridSummaryItem)dgAbertas.GroupSummary[dgAbertas.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgAbertas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgAbertas);

        }

        private void dgAbertas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgAbertas);

        }

        private void Valida_Colunas(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {

            string SQLString;
            SqlDataReader drRules;
            SQLString = "exec sp_Get_Grupo_Nivel @Id_usuario= " + NestDLL.NUserControl.Instance.User_Id;

            drRules = CargaDados.curConn.Return_DataReader(SQLString);

            while (drRules.Read())
            {
                if (Nome_Grid.Name == "dgAbertas")
                {
                    switch (Convert.ToInt32(drRules["Id_Grupo"]))
                    {
                        case 1:
                            //1 Administrador

                            dgAbertas.Columns["Cancel"].Visible = false;
                            dgAbertas.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            dgAbertas.Columns["Cancel"].Visible = false;
                            dgAbertas.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 3:
                            break;
                        case 4:
                            //4 Consulta
                            break;
                    }
                }
            }

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
        private string Retorna_Id(DataRow dr, string Nome_Grid)
        {
            string Id_order;
            try
            {
                ;
                if (dr != null)
                {

                    object[] items = dr.ItemArray;
                    switch (Nome_Grid)
                    {
                        case "dgAbertas":
                            Id_order = items[5].ToString();
                            break;
                        case "dgTrades":
                            Id_order = items[2].ToString();
                            break;

                        case "dgTrades2":
                            Id_order = items[3].ToString();
                            break;

                        case "dgPositions":
                            Id_order = items[2].ToString();
                            break;
                        case "dgAbertas2":
                            Id_order = items[18].ToString();
                            break;

                        case "Quantity":
                            Id_order = items[8].ToString();
                            break;

                        case "Ticker":
                            Id_order = items[7].ToString();
                            break;
                        
                        default:
                            Id_order = "0";
                            break;
                    }
                }
                else
                {
                    Id_order = "0";
                }
                return Id_order;
            }
            catch(Exception e)
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

                return "0";
            }
        }
        private void ShowColumnSelector() { ShowColumnSelector(true, dgAbertas); }
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

        private void dgFechadas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgFechadas);
        }

        private void dgFechadas_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgFechadas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgFechadas);

        }

        private void dgFechadas_EndGrouping(object sender, EventArgs e)
        {
            dgFechadas.ExpandAllGroups();

            dgFechadas.GroupSummary.Add(SummaryItemType.Sum, "Total", dgFechadas.Columns["Total"]);
            ((GridSummaryItem)dgFechadas.GroupSummary[dgFechadas.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            dgFechadas.GroupSummary.Add(SummaryItemType.Sum, "Cash Order", dgFechadas.Columns["Cash Order"]);
            ((GridSummaryItem)dgFechadas.GroupSummary[dgFechadas.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgFechadas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgFechadas);

        }

        private void dgFechadas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgFechadas);

        }

        private void dgCanceladas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgCanceladas);

        }

        private void dgCanceladas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgCanceladas);

        }

        private void dgCanceladas_EndGrouping(object sender, EventArgs e)
        {
            dgCanceladas.ExpandAllGroups();

            dgCanceladas.GroupSummary.Add(SummaryItemType.Sum, "Total", dgCanceladas.Columns["Total"]);
            ((GridSummaryItem)dgCanceladas.GroupSummary[dgCanceladas.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            dgCanceladas.GroupSummary.Add(SummaryItemType.Sum, "Cash Order", dgCanceladas.Columns["Cash Order"]);
            ((GridSummaryItem)dgCanceladas.GroupSummary[dgCanceladas.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgCanceladas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgCanceladas);

        }

        private void dgCanceladas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgCanceladas);

        }

        private void tbMenu_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void old_frmOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            Valida.Save_Properties_Form(this);

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

        private void dgTrades_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgTrades);
        }

        private void dgTrades_EndGrouping(object sender, EventArgs e)
        {
            dgTrades.ExpandAllGroups();

            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Trade Quantity", dgFechadas.Columns["Trade Quantity"]);
            ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Cash", dgFechadas.Columns["Cash"]);
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

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            string SQLString;
            int Id_Trade = Convert.ToInt32(Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "dgTrades"));
            int Id_Order = Convert.ToInt32(Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "dgTrades2"));

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
    }
}