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
    public partial class frmTransferStrategy : LBForm
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmTransferStrategy()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            
            dtgTransf.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTransf.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTransf.LookAndFeel.SetSkinStyle("Blue");
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            
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

            dgTransf.Columns.Clear();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            int Id_Account1=0;

            switch (Id_Fund)
            { 
                case 4:
                    Id_Account1 = 1060;
                    break;

                case 10:
                    Id_Account1 = 1073;
                    break;

                case 43:
                    Id_Account1 = 1148;
                    break;
            }

            SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Return_Wire]() Where Account_Ticker=" + Id_Account1;

            RepositoryItemCheckEdit item = new RepositoryItemCheckEdit();
            //item.Tag .Tag = 1;
            //item.Buttons[0].Kind = DevExpress.XtraEditors.Controls.CheckedListBoxItem.;
            //item.Buttons[0].Caption = "Cancel";
            dtgTransf.RepositoryItems.Add(item);

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgTransf.DataSource = tablep;
            
            tablep.Dispose();

            
            // Delete Button
            dgTransf.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTransf.Columns["Cash"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTransf.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgTransf.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgTransf.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            Valida.SetColumnStyle(dgTransf, 1);
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {

        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
 
     }
}