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
using System.Collections;

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

namespace LiveBook
{
    public partial class frmOptionExercise : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmOptionExercise()
        {
            InitializeComponent();
        }

        private void frmOptionExercise_Load(object sender, EventArgs e)
        {
            
            dtgOptionExercise.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOptionExercise.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOptionExercise.LookAndFeel.SetSkinStyle("Blue");

            Load_Combos();
            
            Carrega_Grid();
        }
        
        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgOptionExercise.SelectAll();
            dgOptionExercise.CopyToClipboard();
            //  MessageBox.Show("Copied!");
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();

            int IdPortolio;

            if (!int.TryParse(cmbFund.SelectedValue.ToString(), out IdPortolio))
            {
                return;
            }

            NestDLL.FormUtils.LoadCombo(this.cmbExpiration, "SELECT Expiration, Expiration AS ExpDescription FROM NESTRT.dbo.Tb000_Posicao_Atual WHERE [Id Instrument]=3 AND [Id Portfolio]=" + IdPortolio + "GROUP BY Expiration", "Expiration", "ExpDescription", 99);


           // NestDLL.FormUtils.LoadCombo(this.cmbExpiration, "SELECT Expiration, Expiration AS ExpDescription FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Id Instrument]=3 AND [Id Portfolio]=" + IdPortolio + " AND [Date now]='2011-11-21' GROUP BY Expiration", "Expiration", "ExpDescription", 99);

        
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmbExpiration_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmdPartial_Click(object sender, EventArgs e)
        {
            LoadExercise(true);
        }

        private void cmdNet_Click(object sender, EventArgs e)
        {

            int IdAccountUnderlying = 0;


            using (frmOptionExerciseBroker Broker = new frmOptionExerciseBroker())
            {
                Broker.IdPortfolio = Convert.ToInt32(cmbFund.SelectedValue);
                Broker.ShowDialog();

                IdAccountUnderlying = Broker.ReturnBroker;
            }

            if (IdAccountUnderlying != -99)
            {
                NetQuantities(false, IdAccountUnderlying);
            }
        }
        
        private void cmdFullExercise_Click(object sender, EventArgs e)
        {

            int IdAccountUnderlying = 0;


            using (frmOptionExerciseBroker Broker = new frmOptionExerciseBroker())
            {
                Broker.IdPortfolio = Convert.ToInt32(cmbFund.SelectedValue);
                Broker.ShowDialog();

                IdAccountUnderlying = Broker.ReturnBroker;
            }

            if (IdAccountUnderlying != -99)
            {
                InserMultiples(false, IdAccountUnderlying);
            }

        }

        private void cmdExpired_Click(object sender, EventArgs e)
        {
          int resposta = Convert.ToInt32(MessageBox.Show("Do you really want to Expire these options?", "Expired Options", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
            
          if (resposta == 6)
          {

              int IdPortfolio = Convert.ToInt32(cmbFund.SelectedValue);

             string SQLString = "Select Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE ID_Broker =31 AND Id_Portfolio = " + IdPortfolio;

              int IdAccount = curConn.Return_Int(SQLString);


              InserMultiples(true, IdAccount);
          }

        }

        private void dgOptionExercise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgOptionExercise.SelectedRowsCount > 1)
            {
                cmdPartial.Enabled = false;
            }
            else
            {
                cmdPartial.Enabled = true;
            }

        }

        private void dgOptionExercise_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgOptionExercise_DoubleClick(object sender, EventArgs e)
        {

            string Column_Name = dgOptionExercise.FocusedColumn.ToString();

            if (Column_Name == "Exercise")
            {
                LoadExercise(false);
            }
        }

        private void dgOptionExercise_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (dgOptionExercise.GetRowCellValue(e.RowHandle, "Changed") == "YES")
            {
                e.Appearance.BackColor = Color.FromArgb(250, 220, 216);
            }
            else
            {
                e.Appearance.BackColor = Color.White;
            }
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


        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dgOptionExercise.Columns.Clear();
            
            int IdPortolio;

            if (!int.TryParse(cmbFund.SelectedValue.ToString(), out IdPortolio))
            {
                return;
            }

            DateTime tempExpiration;

            if (!(cmbExpiration.SelectedValue == null))
            {
                if (!DateTime.TryParse(cmbExpiration.SelectedValue.ToString(), out tempExpiration))
                {
                    return;
                }
            }
            else
            {
                return;
            }
            string Expiration = tempExpiration.ToString("yyyy-MM-dd");

            SQLString = " Select * FROM [FCN_Load_Position_Brokerage](" + IdPortolio + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Expiration.ToString() + "')";


            tablep = curConn.Return_DataTable(SQLString);

            dtgOptionExercise.DataSource = tablep;

            dgOptionExercise.Columns.AddField("Exercise");
            dgOptionExercise.Columns["Exercise"].VisibleIndex = 0;
            dgOptionExercise.Columns["Exercise"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Insert";
            dtgOptionExercise.RepositoryItems.Add(item5);
            dgOptionExercise.Columns["Exercise"].ColumnEdit = item5;
            dgOptionExercise.Columns["Exercise"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgOptionExercise.OptionsBehavior.Editable = false;
            dgOptionExercise.Columns["Exercise"].Visible = true;
            
            tablep.Dispose();

            dgOptionExercise.Columns["Delta/NAV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionExercise.Columns["Delta/NAV"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            dgOptionExercise.Columns["ITM %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionExercise.Columns["ITM %"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            
            dgOptionExercise.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionExercise.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgOptionExercise.Columns["Position"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionExercise.Columns["Position"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgOptionExercise.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionExercise.Columns["Cash"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgOptionExercise.Columns["Strike"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgOptionExercise.Columns["Strike"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgOptionExercise.Columns["Expiration"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgOptionExercise.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgOptionExercise.Columns["Expiration"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgOptionExercise.GroupSummary.Add(SummaryItemType.Sum, "Position", dgOptionExercise.Columns["Position"], "{0:#,#0.00}");

            curUtils.SetColumnStyle(dgOptionExercise, 1);

            dgOptionExercise.ExpandAllGroups();

        }

        void Load_Combos()
        {
            NestDLL.FormUtils.LoadCombo(this.cmbFund, "Select Id_Portfolio,Port_Name from Tb002_Portfolios Where Id_Port_Type=2 and Discountinued=0 order by Port_Name", "Id_Portfolio", "Port_Name", 99);

            object sender = cmbFund;
            EventArgs e = null;

            cmbFund_SelectedIndexChanged(sender, e);
        }

        void LoadExercise(Boolean FlagPartial)
        {
            frmOptionExerciseCnfirm OptionConfirm = new frmOptionExerciseCnfirm();

            DataRow dr;
            dr = dgOptionExercise.GetDataRow(dgOptionExercise.FocusedRowHandle);

            if (dr != null)
            {
                object[] items = dr.ItemArray;

                OptionConfirm.IdPortfolio = Convert.ToInt32(items[0].ToString());
                OptionConfirm.IdSecurity = Convert.ToInt32(items[2].ToString());
                OptionConfirm.IdBook = Convert.ToInt32(items[16].ToString());
                OptionConfirm.IdSection = Convert.ToInt32(items[17].ToString());
                OptionConfirm.Quantity = Convert.ToDouble(items[5].ToString());
                OptionConfirm.Strike = Convert.ToDouble(items[8].ToString());
                OptionConfirm.IdUnderlying = Convert.ToInt32(items[11].ToString());
                OptionConfirm.FlagITMParcial = FlagPartial;
                OptionConfirm.ShowDialog();

                if (OptionConfirm.ReturnOK)
                {
                    dgOptionExercise.SetRowCellValue(dgOptionExercise.FocusedRowHandle, "Changed", "YES");
                }
            }

        }
                      
        void NetQuantities(Boolean OTM, int IdAccountUnderlying)
        {
            try
            {
                int ArraySize = dgOptionExercise.SelectedRowsCount;
                string[,] NetArray = new string[ArraySize, 20];
                double NewQtt = 0;
                double MaxQtt = 0;
                int IdSide = 0;
                int IdBook = 0;
                int IdSection = 0;

                for (int i = 0; i < dgOptionExercise.SelectedRowsCount; i++)
                {
                    int RowIndex = Convert.ToInt32(dgOptionExercise.GetSelectedRows().GetValue(i).ToString());
                    DataRow dr = dgOptionExercise.GetDataRow(RowIndex);
                    if (dr != null)
                    {
                        object[] items = dr.ItemArray;
                        if (i != 0)
                        {
                            if( NetArray[0, 3].ToString() != items[3].ToString())
                            {
                                MessageBox.Show("Error! Diferent Securities to Net. Please try again.");
                            }
                        }

                        for (int f = 0; f < 19; f++) // nunca preeche o 19!
                        {
                            NetArray[i, f] = items[f].ToString();
                        }

                        if (Math.Abs(Convert.ToDouble(items[5])) >= Math.Abs(MaxQtt))
                        {
                            MaxQtt = Convert.ToDouble(items[5]);
                            IdBook = Convert.ToInt32(items[16]);
                            IdSection =  Convert.ToInt32(items[17]);
                        }

                        NewQtt += Convert.ToDouble(items[5].ToString()); 
                        IdSide = 0;
                        if (Convert.ToDouble(items[5].ToString()) != 0)
                        {
                            if (Convert.ToDouble(items[5].ToString()) > 0)
                            {
                                IdSide = 1;
                            }
                            else
                            {
                                IdSide = 2;
                            }
                        }
                        dgOptionExercise.SetRowCellValue(RowIndex, "Changed", "YES");
                    }
                }
                IdSide = 0;


                for (int i = 0; i <= ArraySize-1; i++)
                {
                    if (Convert.ToInt32(NetArray[i, 16]) == IdBook && Convert.ToInt32(NetArray[i, 17]) == IdSection)
                    {
                        if (Convert.ToDouble(NetArray[i, 5].ToString()) > 0)
                        {
                            IdSide = 1;
                        }
                        else
                        {
                            IdSide = 2;
                        }
                        NewQtt = Math.Abs(NewQtt);
                        Negocios.Exercise_Options(Convert.ToInt32(NetArray[i, 0].ToString()), IdAccountUnderlying, Convert.ToInt32(NetArray[i, 11].ToString()), NewQtt, IdSide, Convert.ToInt32(NetArray[i, 2].ToString()), false, Convert.ToDouble(NetArray[i, 8].ToString()), Convert.ToInt32(NetArray[i, 16].ToString()), Convert.ToInt32(NetArray[i, 17].ToString()), false);
                    }
                    else
                    {
                        double Quantity = Math.Abs(Convert.ToDouble(NetArray[i,5].ToString()));
                        if (Convert.ToDouble(NetArray[i, 5].ToString()) > 0)
                        {
                            IdSide = 1;
                        }
                        else
                        {
                            IdSide = 2;
                        }

                        int IdPortfolio = Convert.ToInt32(cmbFund.SelectedValue);
                        string SQLString = "Select Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE ID_Broker =31 AND Id_Portfolio = " + IdPortfolio;
                        int IdAccount = curConn.Return_Int(SQLString);

                        Negocios.Exercise_Options(Convert.ToInt32(NetArray[i, 0].ToString()), IdAccount, Convert.ToInt32(NetArray[i, 11].ToString()), Quantity, IdSide, Convert.ToInt32(NetArray[i, 2].ToString()), false, Convert.ToDouble(NetArray[i, 8].ToString()), Convert.ToInt32(NetArray[i, 16].ToString()), Convert.ToInt32(NetArray[i, 17].ToString()), false);
                        if (IdSide == 2)
                        {
                            IdSide = 1;
                        }
                        else
                        {
                            IdSide = 2;
                        }
                        Negocios.Exercise_Options(Convert.ToInt32(NetArray[i, 0].ToString()), IdAccount, Convert.ToInt32(NetArray[i, 11].ToString()), Quantity, IdSide, Convert.ToInt32(NetArray[i, 2].ToString()), false, Convert.ToDouble(NetArray[i, 8].ToString()), IdBook , IdSection, false);

                    }
                }
             
                MessageBox.Show("Inserted!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        void InserMultiples(Boolean OTM, int IdAccountUnderlying)
        {
            try
            {
                for (int i = 0; i < dgOptionExercise.SelectedRowsCount; i++)
                {
                    int RowIndex;
                    DataRow dr;

                    RowIndex = Convert.ToInt32(dgOptionExercise.GetSelectedRows().GetValue(i).ToString());

                    dr = dgOptionExercise.GetDataRow(RowIndex);

                    if (dr != null)
                    {
                        object[] items = dr.ItemArray;

                        int IdSide = 0;

                        if (Convert.ToDouble(items[5].ToString()) != 0)
                        {
                            if (Convert.ToDouble(items[5].ToString()) > 0)
                            {
                                IdSide = 1;
                            }
                            else
                            {
                                IdSide = 2;
                            }

                            double Quantity = Math.Abs(Convert.ToDouble(items[5].ToString()));

                            Negocios.Exercise_Options(Convert.ToInt32(items[0].ToString()), IdAccountUnderlying, Convert.ToInt32(items[11].ToString()), Quantity, IdSide, Convert.ToInt32(items[2].ToString()), OTM, Convert.ToDouble(items[8].ToString()), Convert.ToInt32(items[16].ToString()), Convert.ToInt32(items[17].ToString()),false);
                        }
                        dgOptionExercise.SetRowCellValue(dgOptionExercise.FocusedRowHandle, "Changed", "YES");

                    }
                }
                MessageBox.Show("Inserted!");
            }
            catch (Exception)
            {

                throw;
            }
        }

     }
}