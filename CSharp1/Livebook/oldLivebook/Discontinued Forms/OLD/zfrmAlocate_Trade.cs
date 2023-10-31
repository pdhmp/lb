using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;


namespace SGN
{
    public partial class frmAlocate_Trade : Form
    {
        Valida Valida = new Valida();
        public int Id_User;
        public int Id_Retorno;
        public int Id_Ticker;
        public int Id_Broker;
        
        public frmAlocate_Trade()
        {
            InitializeComponent();
        }

        private void txtSplitMH_Leave(object sender, EventArgs e)
        {
            decimal SplitMH = 0;
            if (this.txtSplitMH.Text != "")
            {
                SplitMH = Math.Abs(Convert.ToDecimal(txtSplitMH.Text));
                this.txtSplitMH.Text = SplitMH.ToString("##,##0.00#######");
            }
            //decimal PercMH = (SplitMH / Convert.ToDecimal(lblQuantity.Text));

            //txtPercMH.Text = PercMH.ToString("p2");
        }

        private void txtSplitTop_Leave(object sender, EventArgs e)
        {
            decimal SplitTop=0;
            if (this.txtSplitTop.Text != "")
            {
                SplitTop = Math.Abs(Convert.ToDecimal(txtSplitTop.Text));
                this.txtSplitTop.Text = SplitTop.ToString("##,##0.00#######");
            }
            //decimal PercTop = (SplitTop / Convert.ToDecimal(lblQuantity.Text));

            //txtPercTop.Text = PercTop.ToString("p2");
        }

        private void txtSplitBravo_Leave(object sender, EventArgs e)
        {
            decimal SplitBravo = 0;
            if (this.txtSplitBravo.Text != "")
            {
                SplitBravo = Math.Abs(Convert.ToDecimal(txtSplitBravo.Text));
                this.txtSplitBravo.Text = SplitBravo.ToString("##,##0.00#######");
            }
            //decimal PercBravo = (SplitBravo / Convert.ToDecimal(lblQuantity.Text));

            //txtPercFia.Text = PercBravo.ToString("p2");
        }

        private void cmdInsertAloc_Click(object sender, EventArgs e)
        {
           // string SQLString;

            double MH;
            double Top;
            double Fia;
            double QTD;

            if (txtSplitMH.Text.ToString() != "")
            {
                MH = Convert.ToDouble(txtSplitMH.Text.ToString());
            }
            else
            {
                MH = 0;
            }

            if (txtSplitTop.Text.ToString() != "")
            {
                Top = Convert.ToDouble(txtSplitTop.Text.ToString());
            }
            else
            {
                Top = 0;
            }


            if (txtSplitBravo.Text.ToString() != "")
            {
                Fia = Convert.ToDouble(txtSplitBravo.Text.ToString());
            }
            else
            {
                Fia = 0;
            }


            if (lblQuantity.Text.ToString() != "")
            {
                QTD = Convert.ToDouble(lblQuantity.Text.ToString());
            }
            else
            {
                QTD = 0;
            }

            /*
             if ((MH + Top + Fia) == QTD)
            {
                 
                MessageBox.Show(txtPercMH.Text + ", " + txtPercTop.Text + " , " + txtPercFia.Text);

                SQLString = "EXEC Proc_INSERT_ALOCATION " + lblIdOrder.Text.ToString()  + " ,43," + Get_Value(txtPercMH.Text) + "," + DateTime.Now.ToString("yyyyMMdd");
                SQLString = SQLString + " ; EXEC Proc_INSERT_ALOCATION " + lblIdOrder.Text.ToString() + " , 5," + Get_Value(txtPercTop.Text) + "," + DateTime.Now.ToString("yyyyMMdd");
                SQLString = SQLString + " ; EXEC Proc_INSERT_ALOCATION " + lblIdOrder.Text.ToString() + " , 10," + Get_Value(txtPercFia.Text) + "," + DateTime.Now.ToString("yyyyMMdd");

                MessageBox.Show(Get_Value(txtPercMH.Text) + ", " + Get_Value(txtPercTop.Text) + " , " + Get_Value(txtPercFia.Text));

                int retorno = Valida.DB.ExecuteNonQuery(SQLString);
                if (retorno == 0)
                {
                    MessageBox.Show("Error on Insert!");
                }
                Id_Retorno = 1;
                cmdClose_Click(sender, e);
            }
             else
            {
                MessageBox.Show("Check the Quantities");
            }
             */
        }

        string Get_Value(string valor)
        {
            double valor_double = Convert.ToDouble(valor.Replace("%", "")) / 100.00;

            string New_Value = valor_double.ToString("#,##0.00");
            return New_Value.Replace(".", "").Replace("%","").Replace(",", "."); 
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAlocate_Trade_Load(object sender, EventArgs e)
        {

        }

    }
}