using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using SGN.Validacao;
namespace SGN
{
    public partial class frmEarlyFowards : Form
    {
        public frmEarlyFowards()
        {
            InitializeComponent();
        }
        public int Id_Foward;

        CarregaDados CargaDados = new CarregaDados();
        Valida Valid = new Valida();

        private void frmEarlyFowards_Load(object sender, EventArgs e)
        {

            txtIdFoward.Text = Id_Foward.ToString();
            CarregaDados();
        }

        void CarregaDados()
        {
            string SQLString = "Select A.Id_Foward,Quantity,coalesce(Close_Quantity,0)," +
                                " Quantity - coalesce(Close_Quantity,0) as Open_Quantity " +
                                " from dbo.Tb725_Fowards A left join " +
                                " (Select Id_Foward,sum(Close_Quantity) as Close_Quantity "+
                                " from dbo.Tb726_Fowards_Early_Close "+
                                " Where Id_Foward = " + Id_Foward+ " Group by Id_Foward )B" +
                                " ON A.Id_Foward =B.Id_Foward Where A.Id_Foward= " + Id_Foward ;

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                txtTotalQuantity.Text = curRow["Quantity"].ToString();
                txtOpenQuantity.Text = curRow["Open_Quantity"].ToString();
                txtTotalQuantity.Text = curRow["Quantity"].ToString();
                txtTotalQuantity.Text = curRow["Quantity"].ToString();
            }
        }

        private void txtEarlyClose_Leave(object sender, EventArgs e)
        {
            if (Valid.IsNumeric(txtEarlyClose.Text))
            {
                if (Convert.ToDouble(txtEarlyClose.Text) > Convert.ToDouble(txtOpenQuantity.Text))
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
            if (Valid.IsNumeric(txtEarlyClose.Text))
            {
                string SQLString;
                SQLString= "INSERT INTO dbo.Tb726_Fowards_Early_Close(Id_Foward,Close_Quantity,Settlement_Days,Status,Close_Date)" +
                            "VALUES(" + Id_Foward + "," + txtEarlyClose.Text.Replace(',', '.') + "," + txtSettlement.Text + ",1,'" + dtpClose.Value.ToString("yyyyMMdd") + "')";

                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString);

                if (retorno != 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error on Insert!");
                }
            }
        }
    }
}