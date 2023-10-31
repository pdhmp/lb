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
    public partial class frmTrades : Form
    {
        public frmTrades()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
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
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;

        public void Carrega_Grid()
        {
            string StringSQl;
            SqlDataAdapter dt = new SqlDataAdapter();
            DataTable tablet = new DataTable();
            
            /*
            StringSQl = "Select * from Vw_Trades_ENG Where [Id Portfolio] = " + cmbChoosePortfolio.SelectedValue.ToString() + 
                         " and [Trade Date] >= '" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "' and " +
                         " [Trade Date] <= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + "' ";

            */
            StringSQl = "Select [Id Trade],[Id Order],Ticker,[Trade Quantity],[Trade Price],[Cash],Rebate,Login,Strategy," +
            " [Sub Strategy],[Round Lot],Portfolio,[Ticker Currency], [Status Trade],[Trade Date]," +
            " [Id Ticker] from dbo.Vw_Ordens_Trades_En" +
            " Where [Trade Date] >='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") +
            "' and [Trade Date]<= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + "' and [Status Trade]<>4" +
            " and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString();

            
            dt = CargaDados.DB.Return_DataAdapter(StringSQl);
            dt.Fill(tablet);

            dtgTrade.DataSource = tablet;
            dt.Dispose();
            //tablet.Dispose();
            //this.dgTradesRel.ColumnWidthChanged -= new DataGridViewColumnEventHandler(this.dgTradesRel_ColumnWidthChanged);

            Valida_Colunas(Id_usuario, dgTradesRel);
            Valida.SetColumnStyle(dgTradesRel, Id_usuario, "Quantity");
            // this.dgTradesRel.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgTradesRel_ColumnWidthChanged);
        }

        private void dgTradesRel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void Carrega_Grid_Rel()
        {
            
            string StringSQl;
            SqlDataAdapter dt = new SqlDataAdapter();
            DataTable tablet = new DataTable();
            tablet.Clear();
           
            /*
            StringSQl = "Select [Trade Date], Ticker,Quantity, Price,[Round Lot],Broker,[Cash] from Vw_Trades_ENG Where [Id Portfolio] = " + cmbChoosePortfolio.SelectedValue.ToString() +
                         " and [Trade Date] >= '" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "' and " +
                         " [Trade Date] <= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + "' ";
            */

            StringSQl = "Select [Id Trade],[Id Order],Ticker,[Trade Quantity],[Trade Price],[Cash],Rebate,Login,Strategy," +
            " [Sub Strategy],[Round Lot],Portfolio,[Ticker Currency], [Status Trade],[Trade Date]," +
            " [Id Ticker] from dbo.Vw_Ordens_Trades_En" +
            " Where [Trade Date] >='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") +
            "' and [Trade Date]<= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + "' and [Status Trade]<>4" +
            " and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString();


            dt = CargaDados.DB.Return_DataAdapter(StringSQl);
            dt.Fill(tablet);

            gdTRadesREL.DataSource = tablet;
            dt.Dispose();

            Valida.SetColumnStyle(GTradesRel, 1, "Quantity");
        }

         private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgTradesRel, Id_usuario);

        }

        private void dgTradesRel_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgTradesRel, Id_usuario);

        }

        private void dgTradesRel_EndGrouping(object sender, EventArgs e)
        {
            dgTradesRel.ExpandAllGroups();

            dgTradesRel.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgTradesRel.Columns["Quantity"]);
            ((GridSummaryItem)dgTradesRel.GroupSummary[dgTradesRel.GroupSummary.Count - 1]).DisplayFormat = "{0:#,00.00}";
            dgTradesRel.GroupSummary.Add(SummaryItemType.Sum, "Cash Flow", dgTradesRel.Columns["Cash Flow"]);
            ((GridSummaryItem)dgTradesRel.GroupSummary[dgTradesRel.GroupSummary.Count - 1]).DisplayFormat = "{0:#,00.00}";
            
        }

        private void dgTradesRel_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgTradesRel);

        }

        private void dgTradesRel_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgTradesRel);

        }
        private void ShowColumnSelector() { ShowColumnSelector(true, dgTradesRel); }
        
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
                ExportTo(new ExportXlsProvider(fileName),dgTradesRel);
                OpenFile(fileName);
            }
        }

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Trades Report";
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
                ExportTo(new ExportTxtProvider(fileName),dgTradesRel);
                OpenFile(fileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_Grid_Rel();

            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                ExportTo(new ExportTxtProvider(fileName), GTradesRel);
                OpenFile(fileName);
            }
        }

        private void GTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            //Valida.Save_Coluns(GTradesRel, 1);

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
                            dgTradesRel.Columns["Edit"].Visible = true;
                            dgTradesRel.Columns["Edit"].VisibleIndex = 0;
                            dgTradesRel.Columns["Cancel"].Visible = true;
                            dgTradesRel.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            dgTradesRel.Columns["Edit"].Visible = true;
                            dgTradesRel.Columns["Edit"].VisibleIndex = 0;
                            dgTradesRel.Columns["Cancel"].Visible = true;
                            dgTradesRel.Columns["Cancel"].VisibleIndex = 1;
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
            drRules.Close();
            drRules.Dispose();


        }


    }
}