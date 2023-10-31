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
using SGN.Business;

namespace SGN
{
    public partial class zfrmDivisaoBoletas : Form
    {

        public double Id_Ordem;
        public int Id_User;
        public string Id_Retorno;

        CarregaDados CargaDados = new CarregaDados();
        Valida Valida = new Valida();
        Business_Class Negocio = new Business_Class();
        public double Quantidade;
        public double Preco;
        public double LP;

        public double PLMH;
        public double PLTOP;

        public double PLMH_TH;
        public double PLTOP_TH;

        public zfrmDivisaoBoletas()
        {
            InitializeComponent();
        }

        private void frmDivisaoBoletas_Load(object sender, EventArgs e)
        {
           int Id_Ativo =  Carrega_Ordem();

           lblId.Text = Convert.ToString(Id_Ativo);
           Carrega_Combo(Id_Ativo);
           Carrega_Posicao(Id_Ativo);
           Carrega_PL();
           //Calcula_TH(Id_Ativo);
           Calcula_Split(Id_Ativo);
           Calcula_TH();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int Carrega_Ordem() 
        {
            int Id_Ativo;
            
            string SQLString;
            SqlDataReader Dr;

            SQLString = "Select Done,Leaves,[Avg Price Trade],[Id Order],[Id Ticker], Ticker,Total," +
                        " [Order Price],[Cash Order],[Ticker Currency],[Order Date],[Login],[Id Strategy]," +
                        " Strategy,[Id Sub Strategy],[Sub Strategy]" +
                        " [Order Status],[Id Portfolio],[Portfolio], [Broker]"+
                        " from  dbo.VW_Group_Ordens_Trades_EN" +
                        " WHERE [Id Order Status] NOT IN (3, 4, 5) and [Id Order]=" + Id_Ordem + 
                        " order by [Id Ticker]";
            
               // Select * from VW_Ordens_abertas Where [Id Order] = " + Id_Ordem;
            
            
            
            
            Dr = CargaDados.curConn.Return_DataReader(SQLString);

            Dr.Read();
            Id_Ativo = (int)Dr["Id Ticker"];
            lblId.Text = Id_Ativo.ToString();

            SQLString = "Select Simbolo from Tb001_Ativos Where Id_Ativo=" + Id_Ativo;
            string Ticker = CargaDados.curConn.Execute_Query_String(SQLString);

            lblTicker.Text = Ticker;

            SQLString = "Select Lote_Padrao from Tb001_Ativos Where Id_Ativo=" + Id_Ativo;
            LP = Convert.ToDouble(CargaDados.curConn.Execute_Query_String(SQLString));

            Quantidade = Convert.ToDouble(Dr["Quantity"].ToString());
            Preco = Convert.ToDouble(Dr["Price"].ToString());

            txtQuantity.Text = Quantidade.ToString("#,##0.00");
            txtPrice.Text = Preco.ToString("#,##0.00");
            Dr.Close();
            Dr.Dispose();


            SQLString = "Select Id_Ativo_Onshore from dbo.Tb030_Acoes_ADR Where Id_Ativo_Onshore=" + Id_Ativo;
            string retorno = CargaDados.curConn.Execute_Query_String(SQLString);


            if (retorno != "")
            {
                rdMH.Checked = true;
            }
            else
            {
                rdSplit.Checked = true;
            }
            return Id_Ativo;
        }

        void Carrega_Combo(int Id_Ativo)
        {
            string SQLString;
            SQLString = "select [Id Ticker],Position, B.Id_Estrategia,Estrategia + ' - ' + " +
                        " Sub_Estrategia  + ' - (' + Convert(varchar,coalesce(Position,0)) + ')' EstrategiaSub,Id_Sub_Estrategia from  Tb112_Sub_Estrategia B " +
                        " inner join Tb111_Estrategia C on B.Id_Estrategia = C.Id_Estrategia " +
                        " left join (Select  sum(Position)Position,[Id Ticker],[Id Strategy],[Id Sub Strategy] " +
                        " from NESTRT.dbo.FCN_Posicao_Atual() Where [Id Ticker] =" + Id_Ativo + " and [Id Portfolio] in(5,43) " +
                        " group by [Id Ticker],[Id Strategy],[Id Sub Strategy] " +
                        " )A on B.Id_Sub_Estrategia = A.[Id Sub Strategy] order by Position desc";

            CargaDados.carregacombo(cmbChoose, SQLString, "Position", "EstrategiaSub", 99);

        }

        void Carrega_Posicao(int Id_Ativo)
        {
            SqlDataReader DR;
            
            string SQLString;
            SQLString = "Select [Id Portfolio], Position from NESTRT.dbo.FCN_Posicao_Atual()" +
                               " Where [Id Ticker] =" + Id_Ativo + " and [Id Portfolio] in(5,10,43) ";

                    DR = CargaDados.curConn.Return_DataReader(SQLString);
            

            
            while (DR.Read())
                    {

                        switch (Convert.ToInt32(DR[0]))
                        {
                            case 5:
                                txtPosTop.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                            case 10:
                                txtPosBravo.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                            case 43:
                                txtPosMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;
                        
                        }

                    } DR.Close(); DR.Dispose();

             double PosMh;
            double PosTop;
            double PosToTal;

                     PosToTal = Convert.ToDouble(txtPosMH.Text)+Convert.ToDouble(txtPosTop.Text);
                     PosTop = Convert.ToDouble(txtPosTop.Text) / PosToTal;
                    PosMh = Convert.ToDouble(txtPosMH.Text) / PosToTal;

                    lblPPosMH.Text = PosMh.ToString("#,##0.00");
                    lblPPosTop.Text= PosTop.ToString("#,##0.00");


                    SQLString = "Select A.Id_Portfolio,Quantidade  from CS_Posicoes_teste A " +
                                " inner join (select Id_Portfolio,max(Data_Posicoes)Data_Posicoes from CS_Posicoes_teste  " +
                                " Where Id_Ativo =1 and Id_Portfolio in(5,10,43)" +
                                " group by Id_Portfolio" +
                                " )B on A.Id_Portfolio = B.Id_Portfolio and A.Data_Posicoes = B.Data_Posicoes " +
                                " Where Id_Ativo =1 and A.Id_Portfolio in(5,10,43)";


                    DR = CargaDados.curConn.Return_DataReader(SQLString);


                    while (DR.Read())
                    {

                        switch (Convert.ToInt32(DR[0]))
                        {
                            case 5:
                                txtCloseMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                            case 10:
                                txtCloseBravo.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                            case 43:
                                txtCloseMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                        }

                    } DR.Close(); DR.Dispose();

        
        }

        void Carrega_PL()
        {
            SqlDataReader DR;

            string SQLString;

            SQLString = "Select A.Id_Portfolio,Valor_PL * case when A.Id_Portfolio = 4 then dbo.[FCN_Convert_Moedas](1042,900,A.Data_PL) else 1 end  from Tb025_Valor_PL A " +
                        " inner join (select Id_Portfolio,max(Data_PL)Data_PL from Tb025_Valor_PL " +
                        " where Id_Portfolio in(4,10,43) group by Id_Portfolio " +
                        " )B on A.Id_Portfolio = B.Id_Portfolio and A.Data_PL = B.Data_PL ";


            DR = CargaDados.curConn.Return_DataReader(SQLString);

            while (DR.Read())
            {
                switch (Convert.ToInt32(DR[0]))
                {
                    case 43:
                        txtPLMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                        PLMH = Convert.ToDouble(DR[1]); 
                        break;

                    case 4:
                        txtPLTop.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                        PLTOP = Convert.ToDouble(DR[1]);
                        break;

                    case 10:
                        txtPLBravo.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                        break;
                }

            } DR.Close(); DR.Dispose();

        
        }

        void Calcula_Split(int Id_Ativo)
        {
            double PLTOTAL;
            double PercTOP;
            double PercMH;
            double QTD_Ordem;
            
            double PosicaoMH;
            double PosicaoTop;
            string SQLString;

            SQLString = "Select Id_Ativo_Onshore from dbo.Tb030_Acoes_ADR Where Id_Ativo_Onshore=" + Id_Ativo;
            string retorno = CargaDados.curConn.Execute_Query_String(SQLString);


                if (txtPosTop.Text != "")
                { PosicaoTop = Convert.ToDouble(txtPosTop.Text); }
                else
                    PosicaoTop = 0;

                if (txtPosMH.Text != "")
                { PosicaoMH = Convert.ToDouble(txtPosMH.Text); }
                else
                    PosicaoMH = 0;

                PLTOTAL = PLTOP + PLMH;
                PercTOP = PLTOP / PLTOTAL;
                PercMH = PLMH / PLTOTAL;

                QTD_Ordem = Convert.ToDouble(txtQuantity.Text);
                if (retorno == "")
                {
                    txtSplitMH.Text = Convert.ToDouble(((Math.Round(QTD_Ordem * PercTOP / 100)) * 100)).ToString("#,##0.00");
                    txtSplitTop.Text = Convert.ToDouble(((Math.Round(QTD_Ordem * PercMH / 100)) * 100)).ToString("#,##0.00");
                }
                else
                {
                    txtSplitMH.Text = Convert.ToDouble(QTD_Ordem + PosicaoTop).ToString("#,##0.00");
                }
        }


        void Calcula_TH()
        {
            double PLTOTAL;
            double PercTOP;
            double PercMH;

            PLMH = Convert.ToDouble(txtPosMH.Text) + Convert.ToDouble(txtSplitMH.Text);
           PLTOP = Convert.ToDouble(txtPosTop.Text) + Convert.ToDouble(txtSplitTop.Text);

            PLTOTAL = PLTOP + PLMH;
            PercTOP = PLTOP / PLTOTAL;
            PercMH = PLMH / PLTOTAL;


            txtThMH.Text = Convert.ToDouble(((Math.Round((PLTOTAL * PercMH) / 100)) * 100)).ToString("#,##0.00");
            txtThTop.Text = Convert.ToDouble(((Math.Round((PLTOTAL * PercTOP) / 100)) * 100)).ToString("#,##0.00");


        
        }


        private void rdTop_CheckedChanged(object sender, EventArgs e)
        {
            carrega_TOP();
        }

        private void rdMH_CheckedChanged(object sender, EventArgs e)
        {

            carrega_MH();
        }
        private void rdBravo_CheckedChanged(object sender, EventArgs e)
        {
             carrega_FIA();
        }

        void carrega_MH()
        {
            //rdMH.Checked = true;

            /*rdBravo.Checked = false;
            rdTop.Checked = false;
            rdSplit.Checked = false;
            */
            txtSplitMH.Text = txtQuantity.Text;
            txtSplitMH.Enabled = true;
            txtSplitBravo.Enabled = false;
            txtSplitBravo.Text = "";

            txtSplitTop.Enabled = false;
            txtSplitTop.Text = "";


        }
        void carrega_TOP()
        {
            //rdTop.Checked = true;
            /*
            rdBravo.Checked = false;
            rdMH.Checked = false;
            rdSplit.Checked = false;
            */
            txtSplitTop.Text = txtQuantity.Text;
            txtSplitTop.Enabled = true;

            txtSplitBravo.Enabled = false;
            txtSplitBravo.Text = "";

            txtSplitMH.Enabled = false;
            txtSplitMH.Text = "";


        }

        void carrega_FIA()
        {
           // rdBravo.Checked = true;
            /*
            rdTop.Checked = false;
            rdMH.Checked = false;
            rdSplit.Checked = false;
            */
            txtSplitBravo.Text = txtQuantity.Text;
            txtSplitBravo.Enabled = true;

            txtSplitTop.Enabled = false;
            txtSplitTop.Text = "";

            txtSplitMH.Enabled = false;
            txtSplitMH.Text = "";
        }



        private void rdSplit_CheckedChanged(object sender, EventArgs e)
        {
            Calcula_Split(Convert.ToInt32(lblId.Text));
        }


        void teste()
        {
        //string aaa = cmbChoose.v
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inserir_Trades();
        }

        void Inserir_Trades()
        {
            int Id_Ativo;
            string SQLString;


            Id_Ativo = Convert.ToInt32(lblId.Text);

            double MH;
            double Top;
            double Fia;

            if (txtSplitMH.Text != "")
                MH = Convert.ToDouble(txtSplitMH.Text);
            else
                MH = 0;

            if (txtSplitTop.Text != "")
                Top = Convert.ToDouble(txtSplitTop.Text);
            else
                Top = 0;

            if (txtSplitBravo.Text != "")
                Fia = Convert.ToDouble(txtSplitBravo.Text);
            else
                Fia = 0;


            if ((MH + Top + Fia) != Quantidade)
            {
                MessageBox.Show("Error on Split. Verify Values!");
            }
            else
            {
                if (txtSplitMH.Text != "")
                {
                    SQLString = "EXEC [] " +
                        "@Id_Ativo= " + Id_Ativo +
                        "@Quantidade= " + txtSplitMH.Text.Replace(".","").Replace(",",".") +
                        "@Preco= " + txtPrice.Text.Replace(".", "").Replace(",", ".") +
                        "@Valor_Financeiro= " + Convert.ToDouble(txtSplitMH.Text) * Convert.ToDouble(txtPrice.Text) +
                        "@Estrategia= " +
                        "@Sub_Estrategia= " +
                        "@Tipo_Mercado= 1" +
                        "@Operador= 20" +
                        "@Data_Abert_Ordem= " + DateTime.Now.ToShortDateString() +
                        "@Status_Ordem= 1" +
                        "@Id_Portfolio= 43" +
                        "@Id_Corretora= 1" +
                        "@Data_Valid_Ordem= " + DateTime.Now.ToShortDateString();


                }
                if (txtSplitTop.Text != "")
                {
                    SQLString = "EXEC [] " +
                        "@Id_Ativo= " + Id_Ativo +
                        "@Quantidade= " + txtSplitTop.Text.Replace(".", "").Replace(",", ".") +
                        "@Preco= " + txtPrice.Text.Replace(".", "").Replace(",", ".") +
                        "@Valor_Financeiro= " + Convert.ToDouble(txtSplitTop.Text) * Convert.ToDouble(txtPrice.Text) +
                        "@Estrategia= " +
                        "@Sub_Estrategia= " +
                        "@Tipo_Mercado= 1" +
                        "@Operador= 20" +
                        "@Data_Abert_Ordem= " + DateTime.Now.ToShortDateString() +
                        "@Status_Ordem= 1" +
                        "@Id_Portfolio= 5" +
                        "@Id_Corretora= 1" +
                        "@Data_Valid_Ordem= " + DateTime.Now.ToShortDateString();

                }
                if (txtSplitBravo.Text != "")
                {
                    SQLString = "EXEC [] " +
                        "@Id_Ativo= " + Id_Ativo +
                        "@Quantidade= " + txtSplitBravo .Text.Replace(".", "").Replace(",", ".") +
                        "@Preco= " + txtPrice.Text.Replace(".", "").Replace(",", ".") +
                        "@Valor_Financeiro= " + Convert.ToDouble(txtSplitBravo.Text) * Convert.ToDouble(txtPrice.Text) +
                        "@Estrategia= " +
                        "@Sub_Estrategia= " +
                        "@Tipo_Mercado= 1" +
                        "@Operador= 20" +
                        "@Data_Abert_Ordem= " + DateTime.Now.ToShortDateString() +
                        "@Status_Ordem= 1" +
                        "@Id_Portfolio= 10" +
                        "@Id_Corretora= 1" +
                        "@Data_Valid_Ordem= " + DateTime.Now.ToShortDateString();
                }


            }




        }


    }
}