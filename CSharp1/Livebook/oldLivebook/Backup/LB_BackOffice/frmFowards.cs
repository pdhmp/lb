using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using SGN.Business;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace SGN
{
    public partial class frmFowards : LBForm
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmFowards()
        {
            InitializeComponent();
        }

        private void frmFowards_Load(object sender, EventArgs e)
        {
            
            dgFoward.LookAndFeel.UseDefaultLookAndFeel = false;
            dgFoward.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dgFoward.LookAndFeel.SetSkinStyle("Blue");
        }

          private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }
        
        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dtgFoward.Columns.Clear();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Return_Fowards]('" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "') Where Id_Portfolio=" + Id_Fund;

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dgFoward.DataSource = tablep;
            
            tablep.Dispose();

            dtgFoward.Columns.AddField("Close");
            dtgFoward.Columns["Close"].VisibleIndex = 0;
            dtgFoward.Columns["Close"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Close";
            dgFoward.RepositoryItems.Add(item5);
            dtgFoward.Columns["Close"].ColumnEdit = item5;
            dtgFoward.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dtgFoward.OptionsBehavior.Editable = false;
            dtgFoward.Columns["Close"].Visible = true;

            // Delete Button
            dtgFoward.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgFoward.Columns["Quantity"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dtgFoward.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dtgFoward.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtgFoward.Columns["Trade_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            Valida.SetColumnStyle(dtgFoward, 1);

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dtgFoward_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();
            int Id_Foward = Convert.ToInt32(dtgFoward.GetRowCellValue(dtgFoward.FocusedRowHandle, "Transaction_ID"));

            if (Column_Name == "Close")
            {
                frmEarlyFowards EarlyFoward = new frmEarlyFowards();
                EarlyFoward.Id_Foward = Id_Foward;
                EarlyFoward.ShowDialog();
                EarlyFoward.Dispose();
                Carrega_Grid();
            }
        }

 
     }
}