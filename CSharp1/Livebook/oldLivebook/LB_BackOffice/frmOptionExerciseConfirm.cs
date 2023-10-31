using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveBook.Business;
using NestDLL;

using System.Data.SqlClient;

namespace LiveBook
{
    public partial class frmOptionExerciseCnfirm : LBForm
    {
       public Int32 IdPortfolio=0;
       public Int32 IdSecurity;
       public Int32 IdBook;
       public Int32 IdSection;
       public double Quantity;
       public double Strike;
       public Int32 IdUnderlying;
       public Boolean FlagITMParcial = false;
       Int32 IdSide;
       public Boolean ReturnOK;

       public frmOptionExerciseCnfirm()
        {
            InitializeComponent();
        }

       private void rdOTM_CheckedChanged(object sender, EventArgs e)
        {
            CheckGroupConfirm();
        }

        private void rdITM_CheckedChanged(object sender, EventArgs e)
        {
            CheckGroupConfirm();
        }

        void CheckGroupConfirm()
        {
            if (rdITM.Checked == true)
            {
                groupExercise.Enabled = true;
            }
            else
            {
                groupExercise.Enabled = false;
            }
        }

        void CheckGroupExercise()
        {
            if (rdFull.Checked == true)
            {
                txtpartial.Enabled = false;
            }
            else
            {
                txtpartial.Enabled = true;
            }
        }

        private void rdFull_CheckedChanged(object sender, EventArgs e)
        {
            CheckGroupExercise();
        }

        private void rdPartial_CheckedChanged(object sender, EventArgs e)
        {
            CheckGroupExercise();
        }

        private void frmOptionExerciseCnfirm_Load(object sender, EventArgs e)
        {

            NestDLL.FormUtils.LoadCombo(this.cmbBroker, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + IdPortfolio + " order by Show_Prefered desc,Nome asc", "Id_Account", "Nome", 33);

            if (Quantity > 0)
            {
                IdSide = 1;
            }
            else
            {
                IdSide = 2;
            }

            Quantity = Math.Abs(Quantity);

            if (FlagITMParcial)
            {
                rdITM.Checked = true;
                groupconfirm.Enabled = false;
                rdPartial.Checked = true;
                txtpartial.Text = Quantity.ToString("##,##0.00#######");
            }
        }

        private void txtpartial_Leave(object sender, EventArgs e)
        {
            decimal testa_pos;

            if (decimal.TryParse(txtpartial.Text, out testa_pos))
            {
                if (this.txtpartial.Text != "")
                {
                    decimal partial = Convert.ToDecimal(txtpartial.Text);
                    this.txtpartial.Text =Math.Abs(partial).ToString("##,##0.00#######");
                }
            }
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            newNestConn curConn = new newNestConn();

            Int32 IdAccountUnderlying;
            Int32.TryParse(cmbBroker.SelectedValue.ToString(), out IdAccountUnderlying);

            Boolean FlagOTM;

            if (rdOTM.Checked)
            {
                FlagOTM = true;
            }
            else
            {
                FlagOTM = false;

                if (rdPartial.Checked)
                {
                    double NewQuantity;

                    double.TryParse(txtpartial.Text, out NewQuantity);

                    if (Math.Abs(NewQuantity) > Quantity)
                    {
                        MessageBox.Show("The partial quantity is greater than the total amount!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        Quantity = NewQuantity;
                    }

                }
            }

            Negocios.Exercise_Options(IdPortfolio, IdAccountUnderlying,IdUnderlying, Quantity, IdSide, IdSecurity, FlagOTM, Strike, IdBook, IdSection,false);
            ReturnOK = true;
            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            ReturnOK = false;
            this.Close();
        }


    }
}
