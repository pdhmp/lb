using System;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmInsertTxMellon : LBForm
    {
        public frmInsertTxMellon()
        {
            InitializeComponent();
        }

        private void frmInsertTxMellon_Load(object sender, EventArgs e)
        {

        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            InsertTxMellon();
        }


        void InsertTxMellon()
        {
            string StringSQL = "";

            if (txtMile.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 2,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtMile.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtMile30.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 3,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtMile30.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtAcoes.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 12,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtAcoes.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtArb.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 40,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtArb.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtQuant.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 19,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtQuant.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtMulti.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 20,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtMulti.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtAB1.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 37,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtAB1.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtArb30.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 62,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtArb30.Text.Replace(".", "").Replace(",", ".") + ";";
            if (txtNA1.Text != "") StringSQL += "INSERT INTO NESTDB.dbo.Tb755_MellonFee SELECT 13,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtNA1.Text.Replace(".", "").Replace(",", ".") + ";";

            using (newNestConn curConn = new newNestConn())
            {
                int Retorno = curConn.ExecuteNonQuery(StringSQL);
                if (Retorno == 99) MessageBox.Show("Error!");
                else MessageBox.Show("Inserted!");
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
