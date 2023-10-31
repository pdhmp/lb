using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;
using DevExpress.XtraEditors;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace LiveBook
{
    public partial class frmEarlyFowards : Form
    {
        public frmEarlyFowards()
        {
            InitializeComponent();
        }
        public int Id_Foward;

        newNestConn curConn = new newNestConn();
        LB_Utils curUtils = new LB_Utils();

        private void frmEarlyFowards_Load(object sender, EventArgs e)
        {
            txtIdFoward.Text = Id_Foward.ToString();
            dgEvents.LookAndFeel.UseDefaultLookAndFeel = false;
            dgEvents.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dgEvents.LookAndFeel.SetSkinStyle("Blue");

            CarregaDados();
            CarregaGrid();
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

        void CarregaDados()
        {
            string SQLString =
                "SELECT  " +
                "A.Id_Foward, " +
                "Expiration, " +
                "dbo.FCN_FW_Available_Quantity(a.Id_Foward,'" + dtpClose.Value.ToString("yyyyMMdd") + "') AS AvailableQuantity, " +
                "COALESCE(Close_Quantity,0) AS ClosedQuantity, " +
                "Quantity AS InitialQuantity  " +
                "FROM dbo.Tb725_Fowards A (NOLOCK) " +
                "LEFT JOIN " +
                "	(SELECT 	" +
                "	Id_Foward,	" +
                "	SUM(Close_Quantity) AS Close_Quantity  " +
                "	FROM dbo.Tb726_Fowards_Early_Close (NOLOCK)  WHERE Id_Foward = " + Id_Foward + " GROUP BY Id_Foward " +
                "	)B " +
                "ON A.Id_Foward = B.Id_Foward  " +
                "INNER JOIN dbo.Tb001_Securities C ON A.Id_Ticker = C.IdSecurity  " +
                "WHERE A.Id_Foward= " + Id_Foward;

            DataTable curTable = curConn.Return_DataTable(SQLString);
            foreach(DataRow curRow in curTable.Rows)
            {
                txtAvailableQuantity.Text = (LiveDLL.Utils.ParseToDouble(curRow["AvailableQuantity"])).ToString("#,##0.00");
                txtInitialQuantity.Text = (LiveDLL.Utils.ParseToDouble(curRow["InitialQuantity"])).ToString("#,##0.00");
                dtpExpiration.Value = (LiveDLL.Utils.ParseToDateTime(curRow["Expiration"]));
            }
        }

        void CarregaGrid()
        {
            string SQLString;

            DataTable tablep = new DataTable();

            SQLString = "SELECT [Id_Foward],[Close_Quantity],[Settlement_Days],CONVERT (DATE, [Close_Date]) AS [Close_Date] FROM [NESTDB].[dbo].[Tb726_Fowards_Early_Close] WHERE Id_Foward = " + Id_Foward;

            tablep = curConn.Return_DataTable(SQLString);

            gridEvents.Columns.Clear();
            dgEvents.DataSource = null;

            dgEvents.Visible = false;

            this.Size = new System.Drawing.Size(526, 235);

            cmdOK.Location = new System.Drawing.Point(145, 163);
            cmdCancel.Location = new System.Drawing.Point(265, 163);

            if(tablep.Rows.Count > 0)
            {
                this.Size = new System.Drawing.Size(528, 449);

                cmdOK.Location = new System.Drawing.Point(145, 377);
                cmdCancel.Location = new System.Drawing.Point(265, 377);

                dgEvents.Visible = true;

                dgEvents.DataSource = tablep;

                gridEvents.BestFitColumns();

                gridEvents.Columns.AddField("Delete");
                gridEvents.Columns["Delete"].VisibleIndex = 0;
                gridEvents.Columns["Delete"].Width = 60;

                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();

                item5.Buttons[0].Tag = 1;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Delete";
                dgEvents.RepositoryItems.Add(item5);
                gridEvents.Columns["Delete"].ColumnEdit = item5;
                gridEvents.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                gridEvents.OptionsBehavior.Editable = false;
                gridEvents.Columns["Delete"].Visible = true;

                curUtils.SetColumnStyle(gridEvents, 1);

                tablep.Dispose();
            }
        }

        private void gridEvents_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if(Column_Name == "Delete")
            {
                if(gridEvents.FocusedRowHandle != null)
                {
                    DateTime CloseDate = DateTime.Parse(gridEvents.GetRowCellValue(gridEvents.FocusedRowHandle, "Close_Date").ToString());
                    string CloseQuantity = gridEvents.GetRowCellValue(gridEvents.FocusedRowHandle, "Close_Quantity").ToString();

                    string SQLString = "DELETE FROM [NESTDB].[dbo].[Tb726_Fowards_Early_Close]  WHERE Id_Foward = " + Id_Foward + "  AND Close_Date = '" + CloseDate.ToString("yyyyMMdd") + "' AND Close_Quantity = " + CloseQuantity;

                    System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Delete this Early Close?", "Delete Early Close", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                    if(userConfirmation == System.Windows.Forms.DialogResult.OK)
                    {
                        using(newNestConn curConn = new newNestConn())
                        {
                            if(curConn.ExecuteNonQuery(SQLString) > 0)
                            {
                                MessageBox.Show("Deleted!");
                            }
                            else
                                MessageBox.Show("Not Deleted!");
                        }
                    }
                }
            }

            CarregaDados();
            CarregaGrid();
        }

        private void txtEarlyClose_Leave(object sender, EventArgs e)
        {
            if(curUtils.IsNumeric(txtEarlyClose.Text))
            {
                if(Math.Abs(Convert.ToDouble(txtEarlyClose.Text)) > Math.Abs(Convert.ToDouble(txtAvailableQuantity.Text)))
                {
                    MessageBox.Show("The Quantity of Early Close is greater than Available Quantity!");
                    txtEarlyClose.Text = "";
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if(dtpExpiration.Value != dtpClose.Value)
            {
                if(curUtils.IsNumeric(txtEarlyClose.Text) && curUtils.IsNumeric(txtSettlement.Text))
                {
                    string SQLString;
                    SQLString = "INSERT INTO dbo.Tb726_Fowards_Early_Close(Id_Foward,Close_Quantity,Settlement_Days,Status,Close_Date)" +
                                "VALUES(" + Id_Foward + "," + txtEarlyClose.Text.Replace(',', '.') + "," + txtSettlement.Text + ",1,'" + dtpClose.Value.ToString("yyyyMMdd") + "')";

                    int retorno = curConn.ExecuteNonQuery(SQLString, 1);

                    txtEarlyClose.Text = "";
                    txtSettlement.Text = "";

                    if(retorno == 0)
                    {
                        MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Ok!");
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("You can not put the Early Close to the expiration date", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CarregaDados();
            CarregaGrid();
        }
    }
}