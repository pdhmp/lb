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
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SGN
{
    public partial class frmPosicoes : Form
    {
        public frmPosicoes()
        {
            InitializeComponent();
        }

        public class myReverserClass : IComparer
        {

            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(Object x, Object y)
            {
                return ((new CaseInsensitiveComparer()).Compare(y, x));
            }

        }


        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;

        //variaveis p/ calcular Parte do Patrimonio
        double somatoria_Pos;
        double somatoria_Pos_Delta;

        double somatoria_Neg;
        double somatoria_Neg_Delta;

        double PL;

        double exposicao;
        double exposicao_delta;

        double Net;
        double Net_Delta;

        double Percent_Soma_Pos;
        double Percent_Soma_Pos_Delta;

        double Percent_Soma_Neg;
        double Percent_Soma_Neg_Delta;

        double Percent_exposicao;
        double Percent_exposicao_Delta;

        double Percent_Net;
        double Percent_Net_Delta;

        double Tot_MTM;
        double Tot_MTM_PL;
        string Moeda;
        string Id_Carteira;
        string Data_Mod2;
        string data_PL1;

        DataTable tablep = new DataTable();
        SqlDataAdapter dp = new SqlDataAdapter();

        private void frmPosicoes_Load(object sender, EventArgs e)
        {
            Carrega_form();

        }

        public void Carrega_form()
        {
            //CARTEIRAS
            if (Id_usuario == 1)
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 99);
            }
            else
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 99);
            }
        }

        public void Carrega_Grid()
        //função q pega o tab selecionado e carrega o grid dele
        {
            //  DataGridTableStyle ts = new DataGridTableStyle();

            ////////////////////////
                string String_Campos = " * ";
                string data_now = DateTime.Now.ToString("yyyyMMdd");
                SqlDataReader REad_Pl;

                if (this.cmbChoosePortfolio.SelectedIndex != -1)
            {
                    

            if (this.cmbChoosePortfolio.SelectedValue.ToString() != null)
                {
                    //Sorti = tablep.DefaultView.Sort;
                    int pos = this.BindingContext[tablep].Position;

                    tablep.Clear();
                    dgPositions.Columns.Clear();
                    tablep = new DataTable();

                    data_PL1 = Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd");

                    String_Campos = "select Z.Data_PL as Data_PL,Z.Valor_PL as Valor_PL From Tb025_Valor_PL Z" +
                     "  inner join ( select Id_Carteira, max(Data_PL) as Data_PL" +
                     " From Tb025_Valor_PL Where Data_PL <= '" + data_PL1 + "' group by Id_Carteira) mxDrv " +
                     " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira =" + cmbChoosePortfolio.SelectedValue +
                     " and Z.Data_PL <= '" + data_PL1 + "' order by Z.Data_PL"; 

                    REad_Pl = CargaDados.DB.Execute_Query_DataRead(String_Campos);
                    if (!REad_Pl.Read())
                    {
                        PL = 0;
                    }
                    else
                    {
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

                        DateTime data_PL;
                        if (data_PL1 == "")
                        {
                            data_PL = Convert.ToDateTime(DateTime.Now.ToString("yyyyMMdd"));
                            return;
                        }
                        Data_Mod2 = Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd");
                        //Inicio Abertas - tbopen


                        String_Campos = "Select coalesce(Carteira_OBJeto,Id_Carteira) as Carteira_OBJeto from Tb002_Carteiras Where Id_Carteira =  " + this.cmbChoosePortfolio.SelectedValue.ToString();
                        Id_Carteira = CargaDados.DB.Execute_Query_String(String_Campos);

                        // String_Campos = Valida.Retorna_ordem(Id_usuario, 4);

                        if (String_Campos != "")
                        {

                            String_Campos = "Select Moeda from Tb002_Carteiras where Id_Carteira = " + this.cmbChoosePortfolio.SelectedValue.ToString();
                            Moeda = CargaDados.DB.Execute_Query_String(String_Campos);

                            String_Campos = "SET LANGUAGE 'US_ENGLISH'; Select * from Fcn_LB1_Calcula_Posicao(" + Id_Carteira + ",'" + data_PL1 + "','" + Data_Mod2 + "'," + this.cmbChoosePortfolio.SelectedValue.ToString() + "," + Moeda + ") Where [Id Portfolio] = " + this.cmbChoosePortfolio.SelectedValue.ToString() + " order by [Delta Cash] desc ,[Ticker] asc";

                            dp = CargaDados.DB.Return_DataAdapter(String_Campos);
                            dp.Fill(tablep);
                            //tablep.DefaultView.Sort = sort;
                            // this.BindingContext[tablep].Position = pos;
                            dtg.DataSource = tablep;
                            dp.Dispose();
                            if (tablep.Rows.Count > 0)
                            {


                                /* LONG */
                                if (tablep.Compute("Sum(Cash)", "Cash > 0").ToString() != "")
                                {
                                    somatoria_Pos = Convert.ToDouble(tablep.Compute("Sum(Cash)", "Cash > 0"));
                                    Percent_Soma_Pos = somatoria_Pos / PL;
                                }
                                else
                                {
                                    somatoria_Pos = 0;
                                    Percent_Soma_Pos = 0;
                                }
                                /* LONG DELTA  */
                                if (tablep.Compute("Sum([Delta Cash])", "[Delta Cash] > 0  and [Asset Class] = 'Equity'").ToString() != "")
                                {
                                    somatoria_Pos_Delta = Convert.ToDouble(tablep.Compute("SUM([Delta Cash])", "[Asset Class] = 'Equity' and [Delta Cash] >= 0"));
                                    Percent_Soma_Pos_Delta = somatoria_Pos_Delta / PL;
                                    txttesteLong.Text = somatoria_Pos_Delta.ToString();
                                    double zzzz = Convert.ToDouble(tablep.Compute("SUM([Delta Cash])", "[Delta Cash] >= 0 and [Asset Class] ='Equity'  "));
                                }
                                else
                                {
                                    somatoria_Pos_Delta = 0;
                                    Percent_Soma_Pos_Delta = 0;
                                }

                                /* SHORT  */
                                if (tablep.Compute("Sum(Cash)", "Cash < 0").ToString() != "")
                                {
                                    somatoria_Neg = Convert.ToDouble(tablep.Compute("Sum(Cash)", "Cash < 0"));
                                    Percent_Soma_Neg = somatoria_Neg / PL;

                                }
                                else
                                {
                                    somatoria_Neg = 0;
                                    Percent_Soma_Neg = 0;
                                }

                                /* SHORT DELTA  */
                                if (tablep.Compute("Sum([Delta Cash])", "[Delta Cash] < 0  and [Asset Class] = 'Equity'").ToString() != "")
                                {
                                    somatoria_Neg_Delta = Convert.ToDouble(tablep.Compute("SUM([Delta Cash])", "[Asset Class] = 'Equity' and [Delta Cash] <= 0"));

                                    Percent_Soma_Neg_Delta = somatoria_Neg_Delta / PL;
                                    txtTesteShort.Text = somatoria_Neg_Delta.ToString();
                                }
                                else
                                {
                                    somatoria_Neg_Delta = 0;
                                    Percent_Soma_Neg_Delta = 0;
                                }
                                txtNAV.Text = PL.ToString();

                                /* EXPOSIÇÃO */
                                if (somatoria_Neg != 0 && somatoria_Pos != 0)
                                {
                                    exposicao = Math.Abs(somatoria_Pos) + Math.Abs(somatoria_Neg);
                                    Net = Math.Abs(somatoria_Pos) - Math.Abs(somatoria_Neg);
                                    Percent_exposicao = exposicao / PL;
                                    Percent_Net = Net / PL;
                                }

                                /* EXPOSIÇÃO DELTA */
                                if (somatoria_Neg_Delta != 0 && somatoria_Pos_Delta != 0)
                                {
                                    exposicao_delta = Math.Abs(somatoria_Pos_Delta) + Math.Abs(somatoria_Neg_Delta);
                                    Net_Delta = Math.Abs(somatoria_Pos_Delta) - Math.Abs(somatoria_Neg_Delta);

                                    Percent_exposicao_Delta = exposicao_delta / PL;
                                    Percent_Net_Delta = Net_Delta / PL;
                                }


                                if (tablep.Compute("Sum([Total P/L])", "[Total P/L]<>0").ToString() != "")
                                {
                                    Tot_MTM = Convert.ToDouble(tablep.Compute("Sum([Total P/L])", "[Total P/L]<>0"));
                                    //Tot_MTM_PL = Tot_MTM;
                                    //Tot_MTM = Tot_MTM / PL;

                                    //Tot_MTM_PL = Convert.ToDouble(tablep.Compute("Sum([Total P/L])", "[Total P/L] <> 0 "));
                                    txtmtm.Text = Tot_MTM.ToString("#,##0.00");

                                    Tot_MTM_PL = Tot_MTM / PL;

                                }
                                else
                                {
                                    Tot_MTM = 0;
                                    Tot_MTM_PL = 0;
                                }


                            }
                            else
                            {
                                somatoria_Pos = 0;
                                somatoria_Pos_Delta = 0;

                                somatoria_Neg = 0;
                                somatoria_Neg_Delta = 0;

                                exposicao = 0;
                                exposicao_delta = 0;

                                Net = 0;
                                Net_Delta = 0;

                                Percent_Soma_Pos = 0;
                                Percent_Soma_Pos_Delta = 0;

                                Percent_Soma_Neg = 0;
                                Percent_Soma_Neg_Delta = 0;

                                Percent_exposicao = 0;
                                Percent_exposicao_Delta = 0;

                                Percent_Net = 0;
                                Percent_Net_Delta = 0;

                                Tot_MTM = 0;
                                txtmtm.Text = "";

                                Tot_MTM_PL = 0;

                            }

                            //  Valida.Formatar_Grid(dgPositions, "Delta Cash");
                            tablep.Dispose();
                            // this.dgPositions.ColumnWidthChanged -= new DataGridViewColumnEventHandler(this.dgPositions_ColumnWidthChanged);

                            //retorno = Valida.Carregar_Estilo_Colunas(dgPositions, Id_usuario);

                            // this.dgPositions.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgPositions_ColumnWidthChanged);
                            Valida.SetColumnStyle(dgPositions, Id_usuario, "Delta Cash");
                            dgPositions.RowHeight = 15;
                        }
                        /*********************************************/
                    }
                    Calcula_Patrimonio();
                    REad_Pl.Close();
                    REad_Pl.Dispose();
                }

        }    
        }

        

        void Calcula_Patrimonio()
        {
            this.txtPL.Text = PL.ToString("#,##0.00");

            this.txtLong.Text = somatoria_Pos.ToString("#,##0.00");
            this.txtLongDelta.Text = somatoria_Pos_Delta.ToString("#,##0.00");

            this.txtShort.Text = somatoria_Neg.ToString("#,##0.00");
            this.txtShort_Delta.Text = somatoria_Neg_Delta.ToString("#,##0.00");

            this.txtEx.Text = exposicao.ToString("#,##0.00");
            this.txtEx_Delta.Text = exposicao_delta.ToString("#,##0.00");

            this.txtNet.Text = Net.ToString("#,##0.00");
            this.txtNet_Delta.Text = Net_Delta.ToString("#,##0.00");

            Percent_Soma_Neg = Math.Abs(Percent_Soma_Neg);

            this.txt_p_Long.Text = Percent_Soma_Pos.ToString("p").Replace("%", "");
            this.txt_p_Long_Delta.Text = Percent_Soma_Pos_Delta.ToString("p").Replace("%", "");

            this.txt_p_Short.Text = Percent_Soma_Neg.ToString("p").Replace("%", "");
            this.txt_p_Short_Delta.Text = Percent_Soma_Neg_Delta.ToString("p").Replace("%", "");

            this.txt_p_Ex.Text = Percent_exposicao.ToString("p").Replace("%", "");
            this.txt_p_Ex_Delta.Text = Percent_exposicao_Delta.ToString("p").Replace("%", "");

            this.txt_p_Net.Text = Percent_Net.ToString("p").Replace("%", "");
            this.txt_p_Net_Delta.Text = Percent_Net_Delta.ToString("p").Replace("%", "");

            this.txtmtm_perc.Text = Tot_MTM_PL.ToString("p").Replace("%", "");

        }

  
        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgPositions_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgPositions, Id_usuario);

        }

        private void dgPositions_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgPositions, Id_usuario);
        }

        private void dgPositions_EndGrouping(object sender, EventArgs e)
        {
            dgPositions.ExpandAllGroups();

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Cash", dgPositions.Columns["Cash"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.0#}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Brokerage", dgPositions.Columns["Brokerage"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.0#}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Total P/L", dgPositions.Columns["Total P/L"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.0#}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Asset P/L pC", dgPositions.Columns["Asset P/L pC"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.0#}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Currency P/L", dgPositions.Columns["Currency P/L"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.0#}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Delta Cash", dgPositions.Columns["Delta Cash"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.0#}";

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

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "P/L %", dgPositions.Columns["P/L %"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

        }

        private void dgPositions_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgPositions);

        }

        private void dgPositions_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgPositions);

        }

        private void ShowColumnSelector() { ShowColumnSelector(true, dgPositions); }
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

        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                ExportTo(new ExportXlsProvider(fileName),dgPositions);
                OpenFile(fileName);
            }
        }

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Position Report";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void ExportTo(IExportProvider provider,DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
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

        private void button5_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
            if (fileName != "")
            {
                ExportTo(new ExportTxtProvider(fileName),dgPositions);
                OpenFile(fileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_REl();
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                ExportTo(new ExportXlsProvider(fileName),RelPos);
                OpenFile(fileName);
            }
        }

        void Carrega_REl()
        {
            DataTable tablepos = new DataTable();
            SqlDataAdapter dppos = new SqlDataAdapter();
            string String_Campos;
            tablepos.Clear();
            String_Campos = "SET LANGUAGE 'US_ENGLISH'; Select Ticker,Position, [Cost Close], [Delta Cash], [Lot Size],[Date Now],[LAST], [TOTAL P/L] from Fcn_LB1_Calcula_Posicao(" + Id_Carteira + ",'" + data_PL1 + "','" + Data_Mod2 + "'," + this.cmbChoosePortfolio.SelectedValue.ToString() + "," + Moeda + ") Where [Id Portfolio] = " + this.cmbChoosePortfolio.SelectedValue.ToString() + " order by [Instrument] desc ,[Ticker] asc";

            dppos = CargaDados.DB.Return_DataAdapter(String_Campos);
            dppos.Fill(tablepos);
            //tablep.DefaultView.Sort = sort;
            // this.BindingContext[tablep].Position = pos;
            dgrelPos.DataSource = tablepos;
            dppos.Dispose();
            Valida.SetColumnStyle(RelPos, 1, "Delta Cash");
        }


        private void RelPos_ColumnChanged(object sender, EventArgs e)
        {

        }

        private void RelPos_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
           // Valida.Save_Coluns(RelPos, 1);
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
