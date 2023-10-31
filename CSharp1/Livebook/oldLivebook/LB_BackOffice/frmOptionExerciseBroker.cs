using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveBook
{
    public partial class frmOptionExerciseBroker : Form
    {
        public int ReturnBroker;
        public int IdPortfolio;

        public frmOptionExerciseBroker()
        {
            InitializeComponent();
        }

        private void frmOptionExerciseBroker_Load(object sender, EventArgs e)
        {
            NestDLL.FormUtils.LoadCombo(this.cmbBroker, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + IdPortfolio + " order by Show_Prefered desc,Nome asc", "Id_Account", "Nome", getIdAccount());
        }

        private void cmdConfirm_Click(object sender, EventArgs e)
        {
            ReturnBroker = Convert.ToInt32(cmbBroker.SelectedValue.ToString());
            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            ReturnBroker = -99;
            this.Close();

        }


        int getIdAccount()
        {
            int IdAccount=0;

            switch (IdPortfolio)
            {
                case 43:
                    IdAccount = 155;
                    break;

                case 4:
                    IdAccount = 1156;
                    break;

                case 10:
                    IdAccount = 1157;
                    break;

                case 38:
                    IdAccount = 1233;
                    break;

                case 18:
                    IdAccount = 1337;
                    break;

                default:
                    IdAccount = 0;
                    break;
            }

            return IdAccount;

        }
    }
}
