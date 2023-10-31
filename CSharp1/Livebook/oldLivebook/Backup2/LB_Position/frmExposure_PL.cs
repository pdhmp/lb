using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using System.Data.SqlClient;

namespace SGN
{
    public partial class frmExposure_PL : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
      
        public frmExposure_PL()
        {
            InitializeComponent();
        }

        public void Carrega_Exposure()
        {
            /*
             double PL, double somatoria_Pos, double somatoria_Pos_Delta, 
            double somatoria_Neg, double somatoria_Neg_Delta, double exposicao, double exposicao_delta,
            double Net,double Net_Delta,double Percent_Soma_Neg,double Percent_Soma_Neg_Delta,
            double Percent_Soma_Pos,double Percent_Soma_Pos_Delta,double Percent_exposicao,
            double Percent_exposicao_Delta, double Percent_Net, double Percent_Net_Delta, double Tot_MTM_PL, double Tot_MTM
             */
            string SQLString;

            string Id_portfolio;
            string Data;

            Id_portfolio = lblid.Text;
            Data = DateTime.Now.ToString("yyyyMMdd");

            SQLString = "Select * from FCN_Return_Exposure(" + Id_portfolio +")";

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                this.txtPL.Text = Convert.ToDecimal(curRow["NAV"]).ToString("#,##0.00");
                this.txtLong.Text = Convert.ToDecimal(curRow["Long"]).ToString("#,##0.00");
                this.txtLongDelta.Text = Convert.ToDecimal(curRow["Delta_Long"]).ToString("#,##0.00");


                this.txtShort.Text = Math.Abs(Convert.ToDecimal(curRow["Short"])).ToString("#,##0.00");
                this.txtShort_Delta.Text = Math.Abs(Convert.ToDecimal(curRow["Delta_Short"])).ToString("#,##0.00");

                this.txtEx.Text = Convert.ToDecimal(curRow["Exposicao"]).ToString("#,##0.00");
                this.txtEx_Delta.Text = Convert.ToDecimal(curRow["Delta_Exposicao"]).ToString("#,##0.00");

                this.txtNet.Text = Convert.ToDecimal(curRow["Net"]).ToString("#,##0.00");
                this.txtNet_Delta.Text = Convert.ToDecimal(curRow["Delta_Net"]).ToString("#,##0.00");

                this.txt_p_Long.Text = Convert.ToDecimal(curRow["perc_Long"]).ToString("p").Replace("%", "");
                this.txt_p_Long_Delta.Text = Convert.ToDecimal(curRow["Perc_delta_Long"]).ToString("p").Replace("%", "");

                this.txt_p_Short.Text = Convert.ToDecimal(curRow["Perc_Short"]).ToString("p").Replace("%", "");
                this.txt_p_Short_Delta.Text = Convert.ToDecimal(curRow["Perc_Delta_Short"]).ToString("p").Replace("%", "");

                this.txt_p_Ex.Text = Convert.ToDecimal(curRow["Perc_Exposicao"]).ToString("p").Replace("%", "");
                this.txt_p_Ex_Delta.Text = Convert.ToDecimal(curRow["Perc_Delta_Exposicao"]).ToString("p").Replace("%", "");

                this.txt_p_Net.Text = Convert.ToDecimal(curRow["Perc_Net"]).ToString("p").Replace("%", "");
                this.txt_p_Net_Delta.Text = Convert.ToDecimal(curRow["Perc_Delta_Net"]).ToString("p").Replace("%", "");

                this.txtmtm_perc.Text = Convert.ToDecimal(curRow["Perc_Total_MTM"]).ToString("p").Replace("%", "");

                this.txtmtm.Text = Convert.ToDecimal(curRow["Total_MTM"]).ToString("#,##0.00");
            }



           // Percent_Soma_Neg = Math.Abs(Percent_Soma_Neg);
           // Percent_Soma_Neg_Delta = Math.Abs(Percent_Soma_Neg_Delta);

        }
        private void frmExposure_PL_Load(object sender, EventArgs e)
        {
            

            Carrega_Dados();
            timer1.Start();
        }
        public event FrmClosing Fechando;
        public delegate void FrmClosing();

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbChoosePortfolio.SelectedValue = Id_Portfolio;
        }

        void Carrega_Dados()
        {
            cmbChoosePortfolio.SelectedValueChanged -= new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);

            CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);

            cmbChoosePortfolio.SelectedValueChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);

            lblid.Text = cmbChoosePortfolio.SelectedValue.ToString();
        }

        private void cmbChoosePortfolio_SelectedValueChanged(object sender, EventArgs e)
        {
            lblid.Text = cmbChoosePortfolio.SelectedValue.ToString();
        }

        bool processing = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!processing)
            {
                //for (int n = 0; n < 50; n++)
                processing = true;
                Carrega_Exposure();
                processing = false;
            }

        }
       }
}