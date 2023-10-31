using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;

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
            try
            {


            string StringSQL;
            StringSQL = "INSERT INTO NESTDB.dbo.Tb755_MellonFee " +
                "SELECT 2,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtMile.Text.Replace(".", "").Replace(",", ".") +
                " UNION ALL " +     
                "SELECT 3,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtMile30.Text.Replace(".", "").Replace(",", ".") +
                " UNION ALL " + 
                "SELECT 12,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtAcoes.Text.Replace(".", "").Replace(",", ".") +
                " UNION ALL " +
                "SELECT 40,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtArb.Text.Replace(".", "").Replace(",", ".") +
                " UNION ALL " +
                "SELECT 19,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtQuant.Text.Replace(".", "").Replace(",", ".") +
                " UNION ALL " +
                "SELECT 20,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtMulti.Text.Replace(".", "").Replace(",", ".") +
                " UNION ALL " +
                "SELECT 37,'" + dtpDate.Value.ToString("yyyy-MM-dd") + "'," + txtAB1.Text.Replace(".", "").Replace(",", ".");


                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(StringSQL);

                }
                MessageBox.Show("Inserted!");

            }
            catch (Exception)
            {
                MessageBox.Show("Error!");
            }

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
