using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using LiveBook.Business;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraExport;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;

namespace LiveBook
{
    public partial class frmFowards : LBForm
    {
        newNestConn curConn = new newNestConn();
        RefreshHelper hlprOrders;

        public frmFowards()
        {
            InitializeComponent();
        }

        private void frmFowards_Load(object sender, EventArgs e)
        {
            dgFoward.LookAndFeel.UseDefaultLookAndFeel = false;
            dgFoward.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dgFoward.LookAndFeel.SetSkinStyle("Blue");
            dtpIniDate.Value = DateTime.Now.Subtract(new TimeSpan(15, 0, 0, 0));
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }
        
        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dtgFoward.Columns.Clear();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Return_Fowards]('" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "') Where Id_Portfolio=" + Id_Fund;

            tablep = curConn.Return_DataTable(SQLString);

            dgFoward.DataSource = tablep;
            
            tablep.Dispose();

            dtgFoward.Columns.AddField("Close");
            dtgFoward.Columns["Close"].VisibleIndex = 0;
            dtgFoward.Columns["Close"].Width = 60;

            dtgFoward.Columns.AddField("Edit");
            dtgFoward.Columns["Edit"].VisibleIndex = 1;
            dtgFoward.Columns["Edit"].Width = 60;

            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Close";
            dgFoward.RepositoryItems.Add(item5);
            dtgFoward.Columns["Close"].ColumnEdit = item5;
            dtgFoward.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dtgFoward.OptionsBehavior.Editable = false;
            dtgFoward.Columns["Close"].Visible = true;

            RepositoryItemButtonEdit item6 = new RepositoryItemButtonEdit();
            item6.Buttons[0].Tag = 1;
            item6.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item6.Buttons[0].Caption = "Edit";

            dgFoward.RepositoryItems.Add(item6);
            dtgFoward.Columns["Edit"].ColumnEdit = item6;
            dtgFoward.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dtgFoward.OptionsBehavior.Editable = false;
            dtgFoward.Columns["Edit"].Visible = true;


            dtgFoward.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgFoward.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dtgFoward.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgFoward.Columns["Cash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dtgFoward.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgFoward.Columns["Price"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dtgFoward.Columns["SpotPrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgFoward.Columns["SpotPrice"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dtgFoward.Columns["Rate_Foward"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dtgFoward.Columns["Rate_Foward"].DisplayFormat.FormatString = "#,##0.00%;(#,##0.00%)";
            
            dtgFoward.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dtgFoward.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtgFoward.Columns["Trade_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dtgFoward.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtgFoward.Columns["Expiration"].DisplayFormat.FormatString = "dd/MMM/yy";
            //dtgFoward.Columns["Settlement_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dtgFoward.Columns["Settlement_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dtgFoward.Columns["Settlement_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            curUtils.SetColumnStyle(dtgFoward,1);

        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }


        private void dtgFoward_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();
            int Id_Foward = Convert.ToInt32(dtgFoward.GetRowCellValue(dtgFoward.FocusedRowHandle, "Id_Foward"));

            if (Column_Name == "Close")
            {
                DateTime dtMindate = Convert.ToDateTime(dtgFoward.GetRowCellValue(dtgFoward.FocusedRowHandle, "Trade_Date"));
                DateTime dtMaxdate = Convert.ToDateTime(dtgFoward.GetRowCellValue(dtgFoward.FocusedRowHandle, "Expiration"));

                frmEarlyFowards EarlyFoward = new frmEarlyFowards();
                EarlyFoward.Id_Foward = Id_Foward;
                EarlyFoward.Top = this.Top + 200;
                EarlyFoward.Left = this.Left + 100;
                EarlyFoward.dtpClose.MinDate = dtMindate.AddDays(+1);
                EarlyFoward.dtpClose.MaxDate = dtMaxdate.AddDays(-1);

                EarlyFoward.ShowDialog();
                EarlyFoward.Dispose();
                //Carrega_Grid();
            }

            if (Column_Name == "Edit")
            {
                frmEditFoward EditFoward = new frmEditFoward();
                EditFoward.IdFoward = Id_Foward;
                EditFoward.ShowDialog();
                EditFoward.Dispose();
                //Carrega_Grid();
            }

            hlprOrders = new RefreshHelper(dtgFoward, "Id Order");
            hlprOrders.SaveViewInfo();

            dtgFoward.BeginUpdate();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

           string SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Return_Fowards]('" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "') Where Id_Portfolio=" + Id_Fund;

            // tableTrades.Clear();
           tableFWD = curConn.Return_DataTable(SQLString);
           dgFoward.DataSource = tableFWD;

            dgFoward.Refresh();

            hlprOrders.LoadViewInfo();
            dtgFoward.EndUpdate();

           
        }

        DataTable tableFWD;

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\StockLoan_Id_LB_" + NestDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log));

                ExportTo(new ExportXlsProvider(fileName));
                OpenFile(fileName);
            }
        }


        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Position";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        private void ExportTo(IExportProvider provider)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = dtgFoward.CreateExportLink(provider);
                link = dtgFoward.CreateExportLink(provider);

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
                catch (Exception e)
                {
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
 
     }
}