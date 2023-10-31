using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;

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
            CarregaDados();
        }

        void CarregaDados()
        {
            string SQLString = "Select A.Id_Foward,Expiration,Quantity,coalesce(Close_Quantity,0)," +
                                " Quantity - coalesce(Close_Quantity,0) as Open_Quantity " +
                                " from dbo.Tb725_Fowards A (nolock) left join " +
                                " (Select Id_Foward,sum(Close_Quantity) as Close_Quantity "+
                                " from dbo.Tb726_Fowards_Early_Close (nolock) " +
                                " Where Id_Foward = " + Id_Foward+ " Group by Id_Foward )B" +
                                " ON A.Id_Foward =B.Id_Foward " +
                                " inner join dbo.Tb001_Securities C ON A.Id_Ticker = C.IdSecurity " +
                                " Where A.Id_Foward= " + Id_Foward ;

            DataTable curTable = curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                txtTotalQuantity.Text = (NestDLL.Utils.ParseToDouble(curRow["Quantity"])).ToString("#,##0.00");
                txtOpenQuantity.Text = (NestDLL.Utils.ParseToDouble(curRow["Open_Quantity"])).ToString("#,##0.00");
                dtpExpiration.Value = (NestDLL.Utils.ParseToDateTime(curRow["Expiration"]));
            }
        }

        private void txtEarlyClose_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(txtEarlyClose.Text))
            {
                if (Math.Abs(Convert.ToDouble(txtEarlyClose.Text)) > Math.Abs(Convert.ToDouble(txtOpenQuantity.Text)))
                {
                    MessageBox.Show("The Quantity of Early Close is greater than Open!");
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

            if (dtpExpiration.Value != dtpClose.Value)
            {

                if (curUtils.IsNumeric(txtEarlyClose.Text))
                {
                    string SQLString;
                    SQLString = "INSERT INTO dbo.Tb726_Fowards_Early_Close(Id_Foward,Close_Quantity,Settlement_Days,Status,Close_Date)" +
                                "VALUES(" + Id_Foward + "," + txtEarlyClose.Text.Replace(',', '.') + "," + txtSettlement.Text + ",1,'" + dtpClose.Value.ToString("yyyyMMdd") + "')";

                    int retorno = curConn.ExecuteNonQuery(SQLString,1);

                    if (retorno != 0)
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("You can not put the Early Close to the expiration date", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}