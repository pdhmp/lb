using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using LiveDLL;

namespace LiveBook
{
    public partial class frmMellonFwdNames : LBForm
    {

        public frmMellonFwdNames()
        {
            InitializeComponent();

        }

        private void frmMellonFwdNames_Load(object sender, EventArgs e)
        {
            //LiveDLL.FormUtils.LoadCombo(cmbSecurity, "SELECT Id_Foward, NestTicker + '  ' + CAST(CAST(Quantity) AS int) AS varchar) NestTicker FROM NESTDB.dbo.Tb725_Fowards A INNER JOIN NESTDB.dbo.Tb001_Securities B ON A.Id_Ticker=B.IdSecurity WHERE Id_Foward NOT IN (SELECT IdFwdLivebook FROM NESTIMPORT.dbo.Tb_Mellon_Conversion) ORDER BY NestTicker", "Id_Foward", "NestTicker", 99);
            LiveDLL.FormUtils.LoadCombo(cmbSecurity, "SELECT Id_Foward, NestTicker + '  ' + CONVERT(VARCHAR,A.Quantity) + '  ' + SUBSTRING(CONVERT(VARCHAR,round(A.Quantity * A.Price,2)),0,CHARINDEX('.',CONVERT(VARCHAR,round(A.Quantity * A.Price,2)),0)+3) NestTicker FROM NESTDB.dbo.Tb725_Fowards A INNER JOIN NESTDB.dbo.Tb001_Securities B ON A.Id_Ticker=B.IdSecurity WHERE Id_Foward NOT IN (SELECT IdFwdLivebook FROM NESTIMPORT.dbo.Tb_Mellon_Conversion) ORDER BY NestTicker", "Id_Foward", "NestTicker", 99);
            Carrega_Grid();
        }

        public void Carrega_Grid()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString;

                dgMellonFwd.Columns.Clear();

                DataTable tablep = new DataTable();

                SQLString = " SELECT A.*, B.Id_Foward, C.NestTicker " +
                            " FROM NESTIMPORT.dbo.Tb_Mellon_Conversion A " +
                            " INNER JOIN NESTDB.dbo.Tb725_Fowards B " +
                            " ON A.IdFwdLivebook=B.Id_Foward " +
                            " INNER JOIN NESTDB.dbo.Tb001_Securities C " +
                            " ON B.Id_Ticker=C.IdSecurity " +
                            " ORDER BY NestTicker";

                tablep = curConn.Return_DataTable(SQLString);

                dtgMellonFwd.DataSource = tablep;

                dgMellonFwd.Columns["IdFwdLivebook"].Visible = false;
                dgMellonFwd.Columns["IdMellonConversion"].Visible = false;

                // Delete Button
                dgMellonFwd.Columns.AddField("Delete");
                dgMellonFwd.Columns["Delete"].VisibleIndex = 0;
                dgMellonFwd.Columns["Delete"].Width = 60;
                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
                item5.Buttons[0].Tag = 1;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Delete";
                dtgMellonFwd.RepositoryItems.Add(item5);
                dgMellonFwd.Columns["Delete"].ColumnEdit = item5;
                dgMellonFwd.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgMellonFwd.OptionsBehavior.Editable = false;
                dgMellonFwd.Columns["Delete"].Visible = true;

            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {

            if (CheckFoward())
            {
                using (newNestConn curConn = new newNestConn())
                {
                    string MellonCode = txtMellonCode.Text;

                    curConn.ExecuteNonQuery("INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Conversion SELECT '" + MellonCode + "', " + cmbSecurity.SelectedValue + ",getdate()");

                    Carrega_Grid();
                }
            }
        }

        bool CheckFoward()
        {
            DataTable tablep = new DataTable();
            DateTime ExpirationLB = DateTime.Parse("1900-01-01");
            double CashLB = 0;
            DateTime ExpirationMellon = DateTime.Parse("1900-01-01");
            double CashMellon = 0;

            string StringSQL = " SELECT Expiration,Cash FROM NESTDB.dbo.Tb001_Securities A " +
                               " INNER JOIN NESTDB.dbo.Tb725_Fowards B " +
                               " ON A.IdSecurity = B.Id_Ticker " +
                               " WHERE Id_Foward = " + cmbSecurity.SelectedValue;


            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in tablep.Rows)
                {
                    ExpirationLB = Convert.ToDateTime(curRow["Expiration"]);
                    CashLB = Convert.ToDouble(curRow["Cash"]);
                }
                tablep.Dispose();
            }

            StringSQL = " SELECT Vencto,ValorBruto  FROM NESTIMPORT.dbo.Tb_Mellon_RendaFixa " +
                        " WHERE codigo = '" + txtMellonCode.Text + "' AND RefDate = '" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'  ";

            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in tablep.Rows)
                {
                    ExpirationMellon = Convert.ToDateTime(curRow["Vencto"]);
                    CashMellon = Convert.ToDouble(curRow["ValorBruto"]);
                }
                tablep.Dispose();
            }

            if (ExpirationLB != ExpirationMellon)
            {
                DialogResult UserAnswer = MessageBox.Show("The Expiration does not Match.\r\n\r\nDo you want to continue?", "Match Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                if (UserAnswer == DialogResult.No)
                {
                    return false;
                }
            }


            if ((Math.Abs(CashMellon) - Math.Abs(CashLB)) / (Math.Min(Math.Abs(CashMellon), Math.Abs(CashLB))) > 0.05)
            {
                DialogResult UserAnswer = MessageBox.Show("The Financial Ammount is diferent by more than 5%.\r\n\r\nDo you want to continue?", "Match Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

                if (UserAnswer == DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }

        private void dgMellonFwd_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Name;

            if (Column_Name == "colDelete")
            {
                if (dgMellonFwd.FocusedRowHandle != null)
                {
                    int curIndex = int.Parse(dgMellonFwd.GetRowCellValue(dgMellonFwd.FocusedRowHandle, "IdMellonConversion").ToString());

                    DialogResult UserAnswer = MessageBox.Show("do you really want to delete this entry?", "Delete Entry", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (UserAnswer == DialogResult.OK)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery("DELETE FROM NESTIMPORT.dbo.Tb_Mellon_Conversion WHERE IdMellonConversion=" + curIndex.ToString() + ";");
                            Carrega_Grid();
                        }
                    }
                }
            }
        }

        public DateTime ExpirationLB { get; set; }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            string MellonCode = txtMellonCode.Text;

            DialogResult UserAnswer = MessageBox.Show("Do you really want to mismatch this Forward?", "Mismatch Forward", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (UserAnswer == DialogResult.OK)
            {
                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery("DELETE FROM NESTIMPORT.dbo.Tb_Mellon_Conversion WHERE IdFwdMellon = " + MellonCode + ";");
                    Carrega_Grid();
                }
            }
            else
                return;
        }
    }
}
