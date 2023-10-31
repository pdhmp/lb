using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;

using LiveBook.Business;

namespace LiveBook
{
    public partial class zfrmTradeSet : Form
    {

        public int Id_Broker;
        public int Id_Ticker;
        public int Id_User;
        newNestConn curConn = new newNestConn();
        
        LB_Utils curUtils = new LB_Utils();
        Business_Class Negocio = new Business_Class();
        
        public zfrmTradeSet()
        {
            InitializeComponent();
        }

        private void frmTradeSet_Load(object sender, EventArgs e)
        {
           //int Id_Ativo =  Carrega_Ordem();

           //lblId.Text = Convert.ToString(Id_Ativo);
          // Carrega_Combo(Id_Ativo);
           //Carrega_Posicao(Id_Ativo);
           //Calcula_TH(Id_Ativo);
           //Calcula_Split(Id_Ativo);
           //Calcula_TH();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int Load_Split() 
        {
            int IdSecurity;
            
            string SQLString;

            SQLString = " Select Done,Leaves,[Avg Price Trade],[Id Order],[Id Ticker], Ticker,Total," +
                        " [Order Price],[Cash Order],[Ticker Currency],[Order Date],[Login],[Id Strategy]," +
                        " Strategy,[Id Sub Strategy],[Sub Strategy]" +
                        " [Order Status],[Id Portfolio],[Portfolio], [Broker]"+
                        " from  dbo.VW_Group_Ordens_Trades_EN" +
                       // " WHERE [Id Order Status] NOT IN (3, 4, 5) and [Id Order]=" + Id_Ordem + 
                        " order by [Id Ticker]";
            
               // Select * from VW_Ordens_abertas Where [Id Order] = " + Id_Ordem;
            
            
            
            
            String ReturnIdTicker = curConn.Execute_Query_String(SQLString);

            IdSecurity = int.Parse(ReturnIdTicker.ToString());
            lblId.Text = IdSecurity.ToString();

            SQLString = "Select Ticker from Tb001_Securities Where IdSecurity=" + IdSecurity;
            string Ticker = curConn.Execute_Query_String(SQLString);

            lblTicker.Text = Ticker;

            SQLString = "Select RoundLot from Tb001_Securities Where IdSecurity=" + IdSecurity;

            SQLString = "Select Id_Ativo_Onshore from dbo.Tb030_Acoes_ADR Where Id_Ativo_Onshore=" + IdSecurity;
            string retorno = curConn.Execute_Query_String(SQLString);

            return IdSecurity;
        }

        void Carrega_Posicao(int IdSecurity)
        {
            string SQLString;
            SQLString = "Select [Id Portfolio], Position from NESTRT.dbo.FCN_Posicao_Atual() " +
                               " Where [Id Ticker] =" + IdSecurity + " and [Id Portfolio] in(5,10,43) ";

            DataTable curTable = curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                switch (Convert.ToInt32(curRow[0]))
                {
                    case 5:
                        txtPosTop.Text = Convert.ToDouble(curRow[1]).ToString("#,##0.00");
                        break;

                    case 10:
                        txtPosBravo.Text = Convert.ToDouble(curRow[1]).ToString("#,##0.00");
                        break;

                    case 43:
                        txtBuyMH.Text = Convert.ToDouble(curRow[1]).ToString("#,##0.00");
                        break;
                
                }
            } 

            double PosMh;
            double PosTop;
            double PosTotal;

                     PosTotal = Convert.ToDouble(txtBuyMH.Text)+Convert.ToDouble(txtPosTop.Text);
                     PosTop = Convert.ToDouble(txtPosTop.Text) / PosTotal;
                    PosMh = Convert.ToDouble(txtBuyMH.Text) / PosTotal;

                    //lblPPosMH.Text = PosMh.ToString("#,##0.00");
                    //lblPPosTop.Text= PosTop.ToString("#,##0.00");


                    SQLString = "Select A.Id_Portfolio,Quantidade  from CS_Posicoes_teste A " +
                                " inner join (select Id_Portfolio,max(Data_Posicoes)Data_Posicoes from CS_Posicoes_teste  " +
                                " Where Id_Ativo =1 and Id_Portfolio in(5,10,43)" +
                                " group by Id_Portfolio" +
                                " )B on A.Id_Portfolio = B.Id_Portfolio and A.Data_Posicoes = B.Data_Posicoes " +
                                " Where Id_Ativo =1 and A.Id_Portfolio in(5,10,43)";


                    curTable = curConn.Return_DataTable(SQLString);
                    foreach (DataRow curRow in curTable.Rows)
                    {

                        switch (Convert.ToInt32(curRow[0]))
                        {
                            case 5:
                               // txtCloseMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                            case 10:
                               // txtCloseBravo.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                            case 43:
                                //txtCloseMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                                break;

                        }

                    } 

        
        }

        void Carrega_PL()
        {
            string SQLString;

            SQLString = "Select A.Id_Portfolio,Valor_PL * case when A.Id_Portfolio = 4 then dbo.[FCN_Convert_Moedas](1042,900,A.Data_PL) else 1 end  from Tb025_Valor_PL A " +
                        " inner join (select Id_Portfolio,max(Data_PL)Data_PL from Tb025_Valor_PL " +
                        " where Id_Portfolio in(4,10,43) group by Id_Portfolio " +
                        " )B on A.Id_Portfolio = B.Id_Portfolio and A.Data_PL = B.Data_PL ";


            DataTable curTable = curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                switch (Convert.ToInt32(curRow[0]))
                {
                    case 43:
                      //  txtPLMH.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                        //PLMH = Convert.ToDouble(DR[1]); 
                        break;

                    case 4:
                       // txtPLTop.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                        //PLTOP = Convert.ToDouble(DR[1]);
                        break;

                    case 10:
                       //txtPLBravo.Text = Convert.ToDouble(DR[1]).ToString("#,##0.00");
                        break;
                }

            } 

        
        }

        void Calcula_Split(int IdSecurity)
        {
            
            double PosicaoMH;
            double PosicaoTop;
            string SQLString;

            SQLString = "Select Id_Ativo_Onshore from dbo.Tb030_Acoes_ADR Where Id_Ativo_Onshore=" + IdSecurity;
            string retorno = curConn.Execute_Query_String(SQLString);


                if (txtPosTop.Text != "")
                { PosicaoTop = Convert.ToDouble(txtPosTop.Text); }
                else
                    PosicaoTop = 0;

                if (txtBuyMH.Text != "")
                { PosicaoMH = Convert.ToDouble(txtBuyMH.Text); }
                else
                    PosicaoMH = 0;

                //PLTOTAL = PLTOP + PLMH;
                //PercTOP = PLTOP / PLTOTAL;
                //PercMH = PLMH / PLTOTAL;

                //QTD_Ordem = Convert.ToDouble(txtQuantity.Text);
                if (retorno == "")
                {
                   // txtSplitMH.Text = Convert.ToDouble(((Math.Round(QTD_Ordem * PercTOP / 100)) * 100)).ToString("#,##0.00");
                   // txtSplitTop.Text = Convert.ToDouble(((Math.Round(QTD_Ordem * PercMH / 100)) * 100)).ToString("#,##0.00");
                }
                else
                {
                    //txtSplitMH.Text = Convert.ToDouble(QTD_Ordem + PosicaoTop).ToString("#,##0.00");
                }
        }

        void carrega_MH()
        {
            //rdMH.Checked = true;

            /*rdBravo.Checked = false;
            rdTop.Checked = false;
            rdSplit.Checked = false;
            */
            //txtSplitMH.Text = txtQuantity.Text;
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
            //txtSplitTop.Text = txtQuantity.Text;
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
            //txtSplitBravo.Text = txtQuantity.Text;
            txtSplitBravo.Enabled = true;

            txtSplitTop.Enabled = false;
            txtSplitTop.Text = "";

            txtSplitMH.Enabled = false;
            txtSplitMH.Text = "";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Inserir_Trades();
        }

        void Inserir_Trades()
        {
            int IdSecurity;
            string SQLString;


            IdSecurity = Convert.ToInt32(lblId.Text);

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


            if ((MH + Top + Fia) != 1)
            {
                MessageBox.Show("Error on Split. Verify Values!");
            }
            else
            {
               

                if (txtSplitTop.Text != "")
                {
                    SQLString = "EXEC [] " +
                        "@Id_Ativo= " + IdSecurity +
                        "@Quantidade= " + txtSplitTop.Text.Replace(".", "").Replace(",", ".") +
                        //"@Preco= " + txtPrice.Text.Replace(".", "").Replace(",", ".") +
                        //"@Valor_Financeiro= " + Convert.ToDouble(txtSplitTop.Text) * Convert.ToDouble(txtPrice.Text) +
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
                        "@Id_Ativo= " + IdSecurity +
                        "@Quantidade= " + txtSplitBravo .Text.Replace(".", "").Replace(",", ".") +
                        //"@Preco= " + txtPrice.Text.Replace(".", "").Replace(",", ".") +
                       // "@Valor_Financeiro= " + Convert.ToDouble(txtSplitBravo.Text) * Convert.ToDouble(txtPrice.Text) +
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