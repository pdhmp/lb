using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SGN.CargaDados;
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
    public partial class frmOrders : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;

        public frmOrders()
        {
            InitializeComponent();
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {

        }

        public void Carrega_Grid()
        {
            string data_now = DateTime.Now.ToString("yyyyMMdd");

            switch (tbMenu.SelectedTab.Text)
            {
                case "Open":

                    string StringSQl;
                    dgAbertas.Columns.Clear();
                    SqlDataAdapter da = new SqlDataAdapter();
                    DataTable table = new DataTable();

                    StringSQl = " Select * from VW_Ordens_abertas order by Ticker asc";

                    da = CargaDados.DB.Return_DataAdapter(StringSQl);
                    da.Fill(table);
                    dtg2.DataSource = table;
                    da.Dispose();
                    table.Dispose();

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
                    Valida_Colunas(Id_usuario, dgAbertas);
                    Valida.SetColumnStyle(dgAbertas, Id_usuario, "Quantity");
                    break;

                case "Done":
                    //  String_Campos = Valida.Retorna_ordem(Id_usuario, 2);
                    SqlDataAdapter dc = new SqlDataAdapter();
                    DataTable tablec = new DataTable();

                    StringSQl = " Select * from VW_Ordens_Fechadas where [TimeStampf]='" + data_now + "' order by Ticker asc";
                    dc = CargaDados.DB.Return_DataAdapter(StringSQl);
                    dc.Fill(tablec);
                    dtgFec.DataSource = tablec;
                    dc.Dispose();
                    tablec.Dispose();
                    //Valida.Formatar_Grid(dgFechadas, "Quantity");
                    Valida.SetColumnStyle(dgFechadas, Id_usuario, "Quantity");
                    Valida_Colunas(Id_usuario, dgFechadas);
                    break;

                case "Cancel":
                    // String_Campos = Valida.Retorna_ordem(Id_usuario, 3);
                    SqlDataAdapter dl = new SqlDataAdapter();
                    DataTable tablel = new DataTable();

                    StringSQl = " Select * from VW_Ordens_Canceladas where [TimeStampf]='" + data_now + "' order by Ticker asc";
                    dl = CargaDados.DB.Return_DataAdapter(StringSQl);
                    dl.Fill(tablel);

                    dtgCancel.DataSource = tablel;
                    dl.Dispose();
                    tablel.Dispose();
                    // Valida.Formatar_Grid(dgCanceladas, "Quantity");
                    Valida.SetColumnStyle(dgCanceladas, Id_usuario, "Quantity");
                    Valida_Colunas(Id_usuario, dgCanceladas);
                    break;

            }

        }

        private void dgAbertas_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {

        }

        private void dgAbertas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgAbertas, Id_usuario);

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
            int Id_order = Retorna_Id(dgAbertas.GetDataRow(dgAbertas.FocusedRowHandle), "dgAbertas");

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
                        resultado = Negocios.Cancela_Ordem(Id_order);

                        break;

                    default:

                        resultado = Negocios.Inserir_Trade(Id_order, Id_usuario);
                        break;

                }
                if (Id_usuario != 9)
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
            Valida.Save_Coluns(dgAbertas, Id_usuario);

        }

        private void dgAbertas_EndGrouping(object sender, EventArgs e)
        {
            dgAbertas.ExpandAllGroups();

            dgAbertas.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgAbertas.Columns["Quantity"]);
            dgAbertas.GroupSummary.Add(SummaryItemType.Sum, "Cash Flow", dgAbertas.Columns["Cash Flow"]);

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

        private void Valida_Colunas(int Id_usuario, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
                        string StringSQl;
            SqlDataReader drRules;
            StringSQl = "SET DATEFORMAT DMY;exec sp_Get_Grupo_Nivel @Id_usuario= " + Id_usuario;

            drRules = CargaDados.DB.Execute_Query_DataRead(StringSQl);

            while (drRules.Read())
            {
                if (Nome_Grid.Name == "dgAbertas")
                {
                    switch (Convert.ToInt32(drRules["Id_Grupo"]))
                    {
                        case 1:
                            //1 Administrador

                            //dgAbertas.Columns["Edit"].Visible = true;
                            //dgAbertas.Columns["Edit"].VisibleIndex = 0;
                            dgAbertas.Columns["Cancel"].Visible = false;
                            dgAbertas.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            //dgAbertas.Columns["Edit"].Visible = true;
                            //dgAbertas.Columns["Edit"].VisibleIndex = 0;
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
            catch
            {
                return 0;
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
            Valida.Save_Coluns(dgFechadas, Id_usuario);

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
            Valida.Save_Coluns(dgFechadas, Id_usuario);

        }

        private void dgFechadas_EndGrouping(object sender, EventArgs e)
        {
            dgFechadas.ExpandAllGroups();

            dgFechadas.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgFechadas.Columns["Quantity"]);
            dgFechadas.GroupSummary.Add(SummaryItemType.Sum, "Cash Flow", dgFechadas.Columns["Cash Flow"]);

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
            Valida.Save_Coluns(dgCanceladas, Id_usuario);

        }

        private void dgCanceladas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgCanceladas, Id_usuario);

        }

        private void dgCanceladas_EndGrouping(object sender, EventArgs e)
        {
            dgCanceladas.ExpandAllGroups();

            dgCanceladas.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgCanceladas.Columns["Quantity"]);
            dgCanceladas.GroupSummary.Add(SummaryItemType.Sum, "Cash Flow", dgCanceladas.Columns["Cash Flow"]);

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


    }
}