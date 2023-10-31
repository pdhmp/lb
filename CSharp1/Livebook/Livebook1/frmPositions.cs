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
    public partial class frmPositions : Form
    {

        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        bool show = false;
        int Expandable;

        public int Id_usuario;

        double PL;

        public frmPositions()
        {
            InitializeComponent();
        }

        private void frmPositions_Load(object sender, EventArgs e)
        {
           // Valida_Menu(Id_usuario);
            //CARTEIRAS
            cmbChoosePortfolio.SelectedValueChanged -= new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);
            if (Id_usuario == 1)
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 4);
            }
            else
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira",99);
            }
            cmbChoosePortfolio.SelectedValueChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);

        }

        public void Carrega_Grid()
        {
            string String_Campos;
            DataTable tablep = new DataTable();
            SqlDataAdapter dp = new SqlDataAdapter();
            SqlDataReader REad_Pl;

            string data_now = DateTime.Now.ToString("yyyyMMdd");

                        if (this.cmbChoosePortfolio.SelectedValue.ToString() != null)
                        {
                            //Sorti = tablep.DefaultView.Sort;
                            int pos = this.BindingContext[tablep].Position;

                            tablep.Clear();
                            tablep = new DataTable();

                            String_Campos = "select Z.Data_PL as Data_PL,Z.Valor_PL as Valor_PL From Tb025_Valor_PL Z" +
                             "  inner join ( select Id_Carteira, max(Data_PL) as Data_PL" +
                             " From Tb025_Valor_PL group by Id_Carteira) mxDrv " +
                             " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira =" + cmbChoosePortfolio.SelectedValue;

                            REad_Pl = CargaDados.DB.Execute_Query_DataRead(String_Campos);
                            if (! REad_Pl.Read())
                            {
                                PL = 0;
                            }
                            else
                            {
                                string data_PL1 = REad_Pl["Data_PL"].ToString();
                                if (Convert.ToInt32(cmbChoosePortfolio.SelectedValue) == 5 || Convert.ToInt32(cmbChoosePortfolio.SelectedValue) == 6)
                                {
                                    if (Convert.ToInt32(cmbChoosePortfolio.SelectedValue) == 5)
                                    {
                                        String_Campos = "select Z.Valor_PL * dbo.FCN_Convert_Moedas(1042,900,Z.Data_PL) as Valor_PL" +
                                        " From Tb025_Valor_PL Z  inner join ( select Id_Carteira, max(Data_PL) as Data_PL From Tb025_Valor_PL " +
                                        "  group by Id_Carteira) mxDrv on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira = 4";
                                        PL = Convert.ToDouble(CargaDados.DB.Execute_Query_String(String_Campos));

                                    }
                                    else
                                    {
                                        String_Campos = "select Z.Valor_PL as Valor_PL" +
                                        " From Tb025_Valor_PL Z  inner join ( select Id_Carteira, max(Data_PL) as Data_PL From Tb025_Valor_PL " +
                                        "  group by Id_Carteira) mxDrv on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira = 4";
                                        PL = Convert.ToDouble(CargaDados.DB.Execute_Query_String(String_Campos));
                                    }
                                }
                                else
                                {
                                    PL = Convert.ToDouble(REad_Pl["Valor_PL"].ToString());
                                }
                                REad_Pl.Close();
                                REad_Pl.Dispose();

                                DateTime data_PL;
                                if (data_PL1 != "")
                                {
                                    data_PL = Convert.ToDateTime(data_PL1);
                                }
                                else
                                {
                                    data_PL = Convert.ToDateTime(DateTime.Now.ToString("yyyyMMdd"));
                                    return;
                                }
                                string Data_Mod1 = data_PL.ToString("yyyyMMdd");
                                string Data_Mod2 = data_now;
                                //Inicio Abertas - tbopen

                                String_Campos = "Select coalesce(Carteira_OBJeto,Id_Carteira) as Carteira_OBJeto from Tb002_Carteiras Where Id_Carteira =  " + this.cmbChoosePortfolio.SelectedValue.ToString();
                                string Id_Carteira = CargaDados.DB.Execute_Query_String(String_Campos);

                               // String_Campos = Valida.Retorna_ordem(Id_usuario, 4);

                                if (String_Campos != "")
                                {
                                    String_Campos = "Select Moeda from Tb002_Carteiras where Id_Carteira = " + this.cmbChoosePortfolio.SelectedValue.ToString();
                                   string Moeda = CargaDados.DB.Execute_Query_String(String_Campos);

                                       String_Campos = "SET LANGUAGE 'US_ENGLISH'; Select * from [Fcn_Calcula_Posicao_TESTE_PDH_DI] (" + Id_Carteira + ",'" + Data_Mod1 + "','" + Data_Mod2 + "'," + this.cmbChoosePortfolio.SelectedValue.ToString() + "," + Moeda + ") Where [Id Portfolio] = " + this.cmbChoosePortfolio.SelectedValue.ToString() + " order by [Delta Cash] desc ,[Ticker] asc";

                                    dp = CargaDados.DB.Return_DataAdapter(String_Campos);
                                    dp.Fill(tablep);
                                    //tablep.DefaultView.Sort = sort;
                                   // this.BindingContext[tablep].Position = pos;
                                    dtg.DataSource = tablep;

                                    dp.Dispose();

                                    tablep.Dispose();
                                    Valida.SetColumnStyle(dgPositions, Id_usuario, "Delta Cash");
                                    //Valida_Colunas(Id_usuario, dgPositions);
                                    dgPositions.RowHeight = 15;
                                }
                                /*********************************************/
                            }
                           //ARRUMAR
                            //Calcula_Patrimonio();
                         }
        
        }

        private void dgPositions_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
        
        }

        private void dgPositions_Click(object sender, EventArgs e)
        {

        }

        private void dgPositions_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
                    Valida.Save_Coluns(dgPositions,Id_usuario);

        }

        private void dgPositions_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
        
        }

        private void dgPositions_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
                    Valida.Save_Coluns(dgPositions, Id_usuario);

        }

        private void dgPositions_EndGrouping(object sender, EventArgs e)
        {
        
            if (Expandable == 0)
            {
                dgPositions.ExpandAllGroups();
            }
            else
            {
                dgPositions.CollapseAllGroups();
            }

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Cash", dgPositions.Columns["Cash"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Brokerage", dgPositions.Columns["Brokerage"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Total P/L", dgPositions.Columns["Total P/L"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Asset P/L pC", dgPositions.Columns["Asset P/L pC"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Currency P/L", dgPositions.Columns["Currency P/L"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Delta Cash", dgPositions.Columns["Delta Cash"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Cash/NAV", dgPositions.Columns["Cash/NAV"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Delta/NAV", dgPositions.Columns["Delta/NAV"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Contribution pC", dgPositions.Columns["Contribution pC"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Long", dgPositions.Columns["Long"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Short", dgPositions.Columns["Short"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Gross", dgPositions.Columns["Gross"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            //dgPositions.GroupSummary.Add(SummaryItemType.Sum, "P/L %", dgPositions.Columns["P/L %"]);
            //((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

        }

        private void dgPositions_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false,dgPositions);

        }

        private void dgPositions_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgPositions);

        }

        private void cmbChoosePortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        private void cmbChoosePortfolio_SelectedValueChanged(object sender, EventArgs e)
        {
           Carrega_Grid();
 
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

 
    }
}