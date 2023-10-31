using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Business;
using SGN.Validacao;
using System.Data.SqlClient;

namespace SGN
{
    public partial class frmTeste : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        DataTable tablep = new DataTable();
        

        public frmTeste()
        {
            InitializeComponent();
        }

        private void frmTeste_Load(object sender, EventArgs e)
        {

        }

        void CarregaDados_Variacao()
        {
                //variaveis p/ calcular Parte do Patrimonio
            double somatoria_Pos;
            double somatoria_Pos_Delta;

            double somatoria_Neg;
            double somatoria_Neg_Delta;
            
            double PL;
            
            double exposicao;
            double exposicao_delta;

            double Net;
            double Net_Delta;
            
            double Percent_Soma_Pos;
            double Percent_Soma_Pos_Delta;  
            
            double Percent_Soma_Neg;
            double Percent_Soma_Neg_Delta;

            double Percent_exposicao;
            double Percent_exposicao_Delta;

            double Percent_Net;
            double Percent_Net_Delta;

            double Tot_MTM ;
            double Tot_MTM_PL;


            PL = 0;

                
                if (tablep.Rows.Count > 0)
                {
                    /* LONG */
                    if (tablep.Compute("Sum(Cash)", "Position > 0").ToString() != "")
                    {
                        somatoria_Pos = Convert.ToDouble(tablep.Compute("Sum(Cash)", "Position > 0"));
                        Percent_Soma_Pos = somatoria_Pos / PL;
                    }
                    else
                    {
                        somatoria_Pos = 0;
                        Percent_Soma_Pos = 0;
                    }
                    /* LONG DELTA  */
                    if (tablep.Compute("Sum([Delta Cash])", "Position > 0  and [Asset Class] = 'Equity'").ToString() != "")
                    {
                        somatoria_Pos_Delta = Convert.ToDouble(tablep.Compute("Sum([Delta Cash])", "[Delta Cash] > 0  and [Asset Class] = 'Equity'"));
                        Percent_Soma_Pos_Delta = somatoria_Pos_Delta / PL;
                    }
                    else
                    {
                        somatoria_Pos_Delta = 0;
                        Percent_Soma_Pos_Delta = 0;
                    }

                    /* SHORT  */
                    if (tablep.Compute("Sum(Cash)", "Position < 0").ToString() != "")
                    {
                        somatoria_Neg = Convert.ToDouble(tablep.Compute("Sum(Cash)", "Position < 0"));
                        Percent_Soma_Neg = somatoria_Neg / PL;

                    }
                    else
                    {
                        somatoria_Neg = 0;
                        Percent_Soma_Neg = 0;
                    }

                    /* SHORT DELTA  */
                    if (tablep.Compute("Sum([Delta Cash])", "Position < 0  and [Asset Class] = 'Equity'").ToString() != "")
                    {
                        somatoria_Neg_Delta = Convert.ToDouble(tablep.Compute("Sum([Delta Cash])", "[Delta Cash] < 0  and [Asset Class] = 'Equity'"));
                        Percent_Soma_Neg_Delta = somatoria_Neg_Delta / PL;
                    }
                    else
                    {
                        somatoria_Neg_Delta = 0;
                        Percent_Soma_Neg_Delta = 0;
                    }


                    /* EXPOSIÇÃO */
                    if (somatoria_Neg != 0 && somatoria_Pos != 0)
                    {
                        exposicao = Math.Abs(somatoria_Pos) + Math.Abs(somatoria_Neg);
                        Net = Math.Abs(somatoria_Pos) - Math.Abs(somatoria_Neg);
                        Percent_exposicao = exposicao / PL;
                        Percent_Net = Net / PL;
                    }

                    /* EXPOSIÇÃO DELTA */
                    if (somatoria_Neg_Delta != 0 && somatoria_Pos_Delta != 0)
                    {
                        exposicao_delta = Math.Abs(somatoria_Pos_Delta) + Math.Abs(somatoria_Neg_Delta);
                        Net_Delta = Math.Abs(somatoria_Pos_Delta) - Math.Abs(somatoria_Neg_Delta);

                        Percent_exposicao_Delta = exposicao_delta / PL;
                        Percent_Net_Delta = Net_Delta / PL;
                    }


                    if (tablep.Compute("Sum([Total P/L MTM])", "[Total P/L MTM]<>0").ToString() != "")
                    {
                        Tot_MTM = Convert.ToDouble(tablep.Compute("Sum([Total P/L MTM])", "[Total P/L MTM]<>0"));
                        Tot_MTM = Tot_MTM / PL;


                        Tot_MTM_PL = Convert.ToDouble(tablep.Compute("Sum([Total P/L MTM])", "Position <> 0 "));
                        //Tot_MTM_PL = Convert.ToDouble(tablep.Compute("Sum([Total P/L MTM]])", "Position <>0"));
                        txtmtm.Text = Tot_MTM_PL.ToString("#,##0.00");

                        Tot_MTM_PL = Tot_MTM_PL / PL;

                    }
                    else
                    {
                        txtmtm.Text = "";
                        Tot_MTM = 0;
                        Tot_MTM_PL = 0;
                    }


                }
        
        }
    }
}