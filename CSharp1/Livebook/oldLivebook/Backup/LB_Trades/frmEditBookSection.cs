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
    public partial class frmEditBookSection : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        bool HoldUpdates;

        public frmEditBookSection()
        {
            InitializeComponent();
        }

        private void frmEditStrategy_Load(object sender, EventArgs e)
        {

            

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            Carrega_Grid();

            
            dgEditBookSection.Columns.AddField("Edit");
            dgEditBookSection.Columns["Edit"].VisibleIndex = 0;
            dgEditBookSection.Columns["Edit"].Width = 55;
             
            /*
            RepositoryItemCheckEdit item4 = new RepositoryItemCheckEdit();

            item4.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Standard;
            */
            //item4.ValueChecked = Convert.ToString("true"); 
            //item4.ValueUnchecked = Convert.ToString("false"); 
            //item4.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;

            //dtg.RepositoryItems.Add(item4);
            //dgEditStrategy.Columns["Edit"].ColumnEdit = item4;
            
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Edit";
            dtg.RepositoryItems.Add(item4);
            dgEditBookSection.Columns["Edit"].ColumnEdit = item4;
            dgEditBookSection.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            


            dgEditBookSection.OptionsBehavior.Editable = false;

            HoldUpdates = false;

            //timer1.Start();
        }
        void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            if (!HoldUpdates)
            {

                SQLString = " SELECT [Id Order],Portfolio,Ticker,[Id Book],Book,[Id_Section], [Section], " +
                            " [Id Status],Status,Broker,Total,Done,Leaves,[Order price],[Avg price Done] FROM VW_LB_ORDERS " +
                            "WHERE [Order Date]= '" + DateTime.Now.ToString("yyyyMMdd") + "'";


                tablep = CargaDados.curConn.Return_DataTable(SQLString);


                dtg.DataSource = tablep;
                    

                //dgEditStrategy.Columns[0].Visible = false;
               // dgEditStrategy.Columns[1].Visible = false;
                Valida.SetColumnStyle(dgEditBookSection, 2, "Total");
                dgEditBookSection.ExpandAllGroups();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Carrega_Grid();

        }

        private void dgEditStrategy_MouseUp(object sender, MouseEventArgs e)
        {
            HoldUpdates = false;
        }

        private void dgEditStrategy_MouseDown(object sender, MouseEventArgs e)
        {
            HoldUpdates = true;
        }

        private void dgEditStrategy_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {/*
            if (Convert.ToDouble(dgEditStrategy.GetRowCellValue(e.RowHandle, "Total")) > 0)
            {
                e.Appearance.BackColor = Color.FromArgb(222, 254, 235);
            }
            if (Convert.ToDouble(dgEditStrategy.GetRowCellValue(e.RowHandle, "Total")) < 0)
            {
                e.Appearance.BackColor = Color.FromArgb(250, 220, 216);
            }
            */
        }
        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgEditStrategy_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgEditBookSection);
        }

        private void dgEditStrategy_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Columns(dgEditBookSection);

        }

        private void dgEditStrategy_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgEditStrategy_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr;
            int Id_Order;
            int Id_Book;
            int Id_Section;
            string Ticker;
            string Price;
            string Quantity;

            string Column_Name = dgEditBookSection.FocusedColumn.ToString();

            if (Column_Name == "Edit")
            {
                dr = dgEditBookSection.GetDataRow(dgEditBookSection.FocusedRowHandle);

                if (dr != null)
                {
                    object[] items = dr.ItemArray;
                    Id_Order = Convert.ToInt32(items[0].ToString());
                    Id_Book = Convert.ToInt32(items[3].ToString());
                    Id_Section = Convert.ToInt32(items[5].ToString());

                    Ticker = items[2].ToString();
                    Price = items[10].ToString();
                    Quantity = items[9].ToString();

                    frmUpdateStrategy UpDateStrategy = new frmUpdateStrategy();

                    UpDateStrategy.Top = this.Top + 100;
                    UpDateStrategy.Left = this.Left + 100;

                    UpDateStrategy.Id_Order = Id_Order;
                    UpDateStrategy.Id_Book = Id_Book;
                    UpDateStrategy.Id_Section = Id_Section;
                    UpDateStrategy.lblTicker.Text = Ticker.ToString();
                    UpDateStrategy.lblIdOrder.Text = Id_Order.ToString();
                    UpDateStrategy.lblPrice.Text = Price.ToString();
                    UpDateStrategy.lblQuantity.Text = Quantity.ToString();
                    UpDateStrategy.ShowDialog();
                }
            }

        }
    }
}