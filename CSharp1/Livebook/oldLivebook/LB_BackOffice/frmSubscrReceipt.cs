using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using LiveBook.Business;
using NestDLL;

namespace LiveBook
{
    public partial class frmSubscrReceipt : LBForm
    {
        public frmSubscrReceipt()
        {
            InitializeComponent();
        }
        newNestConn curConn = new newNestConn();

        private void Carrega_Grid_Subs()
        {
            dgReceipt.Columns.Clear();

            string SQLString;

            DataTable tablep = new DataTable();

            int IdPortolio;

            if (!int.TryParse(cmbFund.SelectedValue.ToString(), out IdPortolio))
            {
                return;
            }

            SQLString = " Select [Id Portfolio],[Portfolio],[Id Ticker],[Ticker],Expiration,Position,[Last Admin],[Close Admin],[Id Book],Book,[Id Section],Section " +
           " FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]= " + IdPortolio + " AND [Id Instrument]=15";
            tablep = curConn.Return_DataTable(SQLString);

            dtgReceipt.DataSource = tablep;

            dgReceipt.Columns.AddField("Convert");
            dgReceipt.Columns["Convert"].VisibleIndex = 0;
            dgReceipt.Columns["Convert"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Convert";
            dtgReceipt.RepositoryItems.Add(item5);
            dgReceipt.Columns["Convert"].ColumnEdit = item5;
            dgReceipt.Columns["Convert"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgReceipt.OptionsBehavior.Editable = false;
            dgReceipt.Columns["Convert"].Visible = true;

            tablep.Dispose();

            dgReceipt.GroupSummary.Add(SummaryItemType.Sum, "Position", dgReceipt.Columns["Position"], "{0:#,#0.00}");

            curUtils.SetColumnStyle(dgReceipt, 1);

            dgReceipt.ExpandAllGroups();

        }


        private void Carrega_Grid_Warrant()
        {

            dgWarrant.Columns.Clear();

            string SQLString;

            DataTable tablep = new DataTable();

            int IdPortolio;

            if (!int.TryParse(cmbFund.SelectedValue.ToString(), out IdPortolio))
            {
                return;
            }

            SQLString = " Select [Id Portfolio],[Portfolio],[Id Ticker],[Ticker],Expiration,Position,[Last Admin],[Close Admin],[Id Book],Book,[Id Section],Section " +
            " FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio]= " + IdPortolio + " AND [Id Instrument]=26";

            tablep = curConn.Return_DataTable(SQLString);

            dtgWarrant.DataSource = tablep;

            dgWarrant.Columns.AddField("Convert");
            dgWarrant.Columns["Convert"].VisibleIndex = 0;
            dgWarrant.Columns["Convert"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Convert";
            dtgWarrant.RepositoryItems.Add(item5);
            dgWarrant.Columns["Convert"].ColumnEdit = item5;
            dgWarrant.Columns["Convert"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgWarrant.OptionsBehavior.Editable = false;
            dgWarrant.Columns["Convert"].Visible = true;

            tablep.Dispose();

            dgWarrant.GroupSummary.Add(SummaryItemType.Sum, "Position", dgWarrant.Columns["Position"], "{0:#,#0.00}");

            curUtils.SetColumnStyle(dgReceipt, 1);

            dgWarrant.ExpandAllGroups();

        }

        void CarregaGrid()
        {
            Carrega_Grid_Subs();
            Carrega_Grid_Warrant();
        }

        private void frmReceiptSubscr_Load(object sender, EventArgs e)
        {
            dtgWarrant.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgWarrant.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgWarrant.LookAndFeel.SetSkinStyle("Blue");

            dtgReceipt.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgReceipt.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgReceipt.LookAndFeel.SetSkinStyle("Blue");

            
            Load_Combos();

            CarregaGrid();
        }

        void Load_Combos()
        {
            NestDLL.FormUtils.LoadCombo(this.cmbFund, "Select Id_Portfolio,Port_Name from Tb002_Portfolios Where Id_Port_Type=2 and Discountinued=0 order by Port_Name", "Id_Portfolio", "Port_Name", 99);
        }

        private void cmdFullExercise_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

    }
}
