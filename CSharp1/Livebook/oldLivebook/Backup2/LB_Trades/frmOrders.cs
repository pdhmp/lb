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
using DevExpress.XtraGrid.Helpers;


namespace SGN
{
    public partial class frmOrders : LBForm
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        
        string data_now = DateTime.Now.ToString("yyyyMMdd");

        RefreshHelper hlprOrders;

        DataTable tableOrdens;

        DataTable tableTrades;

        public frmOrders()
        {
            InitializeComponent();
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {
            dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOrders.LookAndFeel.SetSkinStyle("Blue");

            dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTrade.LookAndFeel.SetSkinStyle("Blue");

            Carrega_Grid();
            
        }

        public void Carrega_Grid()
        {
            string SQLString;
            SQLString = "Select * from VW_LB_ORDERS WHERE  [Order Date]='" + data_now + "'";

            DataTable tableOrdens = CargaDados.curConn.Return_DataTable(SQLString);
            dtgOrders.DataSource = tableOrdens;

            if (Valida_Colunas(dgOrders))
            {
                dgOrders.Columns.AddField("Cancel Leaves");
                dgOrders.Columns["Cancel Leaves"].VisibleIndex =1;
                dgOrders.Columns["Cancel Leaves"].Width = 70;
                dgOrders.Columns.AddField("Cancel All");
                dgOrders.Columns["Cancel All"].VisibleIndex = 0;
                dgOrders.Columns["Cancel All"].Width = 70;

                RepositoryItemButtonEdit item = new RepositoryItemButtonEdit();
                item.Buttons[0].Tag = 1;
                item.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item.Buttons[0].Caption = "Cancel";

                RepositoryItemButtonEdit item2 = new RepositoryItemButtonEdit();
                item2.Buttons[0].Tag = 1;
                item2.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item2.Buttons[0].Caption = "Cancel";

                dtgOrders.RepositoryItems.Add(item);
                dtgOrders.RepositoryItems.Add(item2);

                dgOrders.Columns["Cancel Leaves"].ColumnEdit = item;
                dgOrders.Columns["Cancel Leaves"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgOrders.Columns["Cancel Leaves"].Visible = false;

                dgOrders.Columns["Cancel All"].ColumnEdit = item;
                dgOrders.Columns["Cancel All"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgOrders.Columns["Cancel All"].Visible = false;
            }

            dgOrders.OptionsBehavior.Editable = false;
            
            Valida.SetColumnStyle(dgOrders, 2, "Total");

            dgOrders.ExpandAllGroups();

            radAll.Checked = true;

            SQLString = "SELECT TOP 1 * FROM VW_LB_Trades Where [Trade Date]='" + data_now + "'";

            DataTable tableTrades = CargaDados.curConn.Return_DataTable(SQLString);

            dtgTrade.DataSource = tableTrades;

            if (Valida_Colunas(dgOrders))
            {
                dgTrades.Columns.AddField("Cancel");
                dgTrades.Columns["Cancel"].VisibleIndex = 1;
                dgTrades.Columns["Cancel"].Width = 55;
                dgTrades.Columns.AddField("Edit");
                dgTrades.Columns["Edit"].VisibleIndex = 0;
                dgTrades.Columns["Edit"].Width = 55;

                RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
                item3.Buttons[0].Tag = 1;
                item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item3.Buttons[0].Caption = "Cancel";

                RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
                item4.Buttons[0].Tag = 1;
                item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item4.Buttons[0].Caption = "Edit";

                dtgTrade.RepositoryItems.Add(item3);
                dtgTrade.RepositoryItems.Add(item4);

                dgTrades.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgTrades.OptionsBehavior.Editable = false;
                dgTrades.Columns["Cancel"].Visible = false;

                dgTrades.Columns["Edit"].ColumnEdit = item4;
                dgTrades.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgTrades.OptionsBehavior.Editable = false;
                dgTrades.Columns["Edit"].Visible = false;
            }

            Valida.SetColumnStyle(dgTrades, 2, "Trade Quantity"); 

        }

        private void dgAbertas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgOrders);
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
            int resultado=666;

            string Order_Status = (string)dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Status");

            if (Order_Status != "Cancelada")
            {
                //string Quantity = (string)dgAbertas.GetRowCellValue(dgAbertas.FocusedRowHandle, "Tot'");
                string Ticker = (string)dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Ticker");
                int Id_Account = Convert.ToInt32(dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Id Portfolio"));
                int Id_Order = Convert.ToInt32(dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Id Order"));
                double Total = Convert.ToDouble(dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Total"));
                double Done = Convert.ToDouble(dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Done"));


                if (Id_Order != 0)
                {
                    GridView Get_Column = sender as GridView;
                    string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                    switch (Column_Name)
                    {

                        case "Cancel Leaves":
                            resultado = Negocios.Cancela_Ordem(Id_Order,0);
                            break;

                        case "Cancel All":
                            resultado = Negocios.Cancela_Ordem_Trade(Id_Order, 0);
                            break;
                            
                        default:

                            if (Math.Abs(Done) < Math.Abs(Total))
                            {
                                if (Id_Account == 55)
                                {
                                    //resultado = Negocios.Inserir_Divisao(Id_Order, Id_User, Ticker, Convert.ToStringTotal);
                                }
                                else
                                {
                                    resultado = Negocios.Inserir_Trade(Id_Order);
                                }
                            }
                            break;
                    }
                    if (NestDLL.NUserControl.Instance.User_Id != 9)
                    {
                        if (resultado != 0 && resultado !=666)
                        {
                            RefreshGrids();
                        }
                    }
                    // ButtonEdit edit = sender as ButtonEdit;
                    // GridView view = (edit.Parent as GridControl).FocusedView as GridView;
                    // string name = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["Id Portfolio"]).ToString();
                    // MessageBox.Show(name);
                }
            }
        }

        private void dgAbertas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgOrders);

        }

        private void dgAbertas_EndGrouping(object sender, EventArgs e)
        {
            //dgAbertas.ExpandAllGroups();

            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Total", dgOrders.Columns["Total"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Done", dgOrders.Columns["Done"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Leaves", dgOrders.Columns["Leaves"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            
            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Total", dgOrders.Columns["Total"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            
            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Total", dgOrders.Columns["Total"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Cash Done", dgOrders.Columns["Cash Done"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            
            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Avg Price Done", dgOrders.Columns["Avg Price Done"]);
            ((GridSummaryItem)dgOrders.GroupSummary[dgOrders.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgAbertas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgOrders);

        }

        private void dgAbertas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgOrders);

        }

        private bool Valida_Colunas(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (Valida.Verifica_Acesso(1) || Valida.Verifica_Acesso(2))
            {
                return true;
            }
            else
            {
                return false;
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
        
        private void ShowColumnSelector() { ShowColumnSelector(true, dgOrders); }
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

        private void tbMenu_Click(object sender, EventArgs e)
        {
            RefreshGrids();
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

            int Id_Trade = Convert.ToInt32(dgTrades.GetRowCellValue(dgTrades.FocusedRowHandle, "Id Trade"));
            int Id_Order = Convert.ToInt32(dgTrades.GetRowCellValue(dgTrades.FocusedRowHandle, "Id Order"));
            int Id_Status =  Convert.ToInt32(dgTrades.GetRowCellValue(dgTrades.FocusedRowHandle, "Id Trade Status"));

            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.FieldName.ToString();

            if (Column_Name == "Cancel" && Id_Status != 4)
            {
                int resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Trade?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                if (resposta == 6)
                {
                    SQLString = "EXEC Proc_Cancela_Trade_Ordem @Id_Trade = " + Id_Trade;
                    resultado = CargaDados.curConn.ExecuteNonQuery(SQLString,1);
                    if (resultado != 0)
                    {
                        RefreshGrids();
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
                RefreshGrids();

            }
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            dgOrders.ExpandAllGroups();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgOrders.CollapseAllGroups();
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked == true)
            {
                ClearStatusFilter();
            }
        }

        private void radOpen_CheckedChanged(object sender, EventArgs e)
        {
            if (radOpen.Checked == true) { ClearStatusFilter();  dgOrders.Columns["Status"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Status] = 'Aberta'"); };
        }

        private void radDone_CheckedChanged(object sender, EventArgs e)
        {
            if (radDone.Checked == true) { ClearStatusFilter(); dgOrders.Columns["Status"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Status] = 'Executadas'"); };
        }

        private void radCancelled_CheckedChanged(object sender, EventArgs e)
        {
            if (radCancelled.Checked == true) { ClearStatusFilter(); dgOrders.Columns["Status"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Status] = 'Cancelada'"); };
        }

        private void ClearStatusFilter()
        {
                if (dgOrders.ActiveFilter.Criteria != null)
                {
                    string curFilter = dgOrders.ActiveFilter.Criteria.ToString();
                    string[] arrCriteria = curFilter.Split('[');
                    string newFilter = "";

                    foreach (string curCriteria in arrCriteria)
                    {
                        if (curCriteria.Contains("Status") == false && curCriteria != "") { newFilter += "[" + curCriteria; };
                    }
                    if (newFilter.Length > 4)
                    {
                        if (newFilter.Substring(newFilter.Length - 4, 4) == "And ") { newFilter = newFilter.Substring(0, newFilter.Length - 4); };
                    }
                    RefreshGrids();
                    dgOrders.ActiveFilterString = newFilter;
                }
        }

        private void tbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void RefreshGrids()
        {

            if (data_now == DateTime.Now.ToString("yyyyMMdd"))
            {
                switch (tbMenu.SelectedTab.Text)
                {
                    case "Orders":
                        hlprOrders = new RefreshHelper(dgOrders, "Id Order");
                        hlprOrders.SaveViewInfo();

                       // tableOrdens.Clear();
                        dtgOrders.BeginUpdate();

                       // tableOrdens.Clear();
                        tableOrdens = CargaDados.curConn.Return_DataTable("Select * from VW_LB_ORDERS WHERE [Order Date]='" + data_now + "'");
                        dtgOrders.DataSource = tableOrdens;

                        hlprOrders.LoadViewInfo();
                        dtgOrders.Refresh();
                        dtgOrders.EndUpdate();

                        break;

                    case "Trades":
                        hlprOrders = new RefreshHelper(dgOrders, "Id Order");
                        hlprOrders.SaveViewInfo();

                        dtgOrders.BeginUpdate();

                       // tableTrades.Clear();
                        tableTrades = CargaDados.curConn.Return_DataTable("SELECT * FROM VW_LB_Trades Where [Trade Date]='" + data_now + "'");
                        dtgTrade.DataSource = tableTrades;

                        dtgTrade.Refresh();

                        hlprOrders.LoadViewInfo();
                        dtgOrders.EndUpdate();

                        break;
                }
            }
            else
            {
                Carrega_Grid();
            }
        }

        private void dgTrades_EndGrouping(object sender, EventArgs e)
        {
            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Trade Quantity", dgTrades.Columns["Trade Quantity"]);
           ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

           dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Trade Price", dgTrades.Columns["Trade Price"]);
           ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

           dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Trade Cash", dgTrades.Columns["Trade Cash"]);
           ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgAbertas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Data.DataRow masterDataRow = dgOrders.GetDataRow(e.RowHandle);
            if (masterDataRow["Split"].ToString() == "1") 
            { 
                e.Appearance.ForeColor = Color.DarkGoldenrod; 
            };

            if (masterDataRow["Status"].ToString() == "Cancelada")
            {
                e.Appearance.ForeColor = Color.Red;
            };
        }

        private void dgTrades_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            System.Data.DataRow masterDataRow = dgTrades.GetDataRow(e.RowHandle);
            if (masterDataRow["Split"].ToString() == "1") 
            { 
                e.Appearance.ForeColor = Color.DarkGoldenrod; 
            };
            if (masterDataRow["Trade Status"].ToString() == "Cancel")
            {
                e.Appearance.ForeColor = Color.Red;
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshGrids();
        }
    }
}