using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Validacao;
using SGN.Business;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SGN
{

    public partial class frmEdit : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        DataTable tablep = new DataTable();
        SqlDataAdapter dp = new SqlDataAdapter();

        public frmEdit()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }


        public void Carrega_Grid()
        {

            string StringSQl = "Select convert(varchar(10),Id_Ativo,102) + '-' + convert(char(5),id_tipo_ativo,102) " +
                " as Id_Ativo, Simbolo from TB001_Ativos";

            if (txtsearch.Text.Trim() != "")
            {
                StringSQl = StringSQl + " Where Simbolo like '%" + txtsearch.Text.ToString() + "%' order by Simbolo";
            }
            else
            {
                StringSQl = StringSQl + " order by Simbolo";
            }



            SqlDataAdapter da = CargaDados.DB.Return_DataAdapter(StringSQl);
            DataTable table = new DataTable();
            da.Fill(table);
            lstAtivos.DataSource = table;
            lstAtivos.DisplayMember = "Simbolo";
            lstAtivos.ValueMember = "Id_Ativo";

        }

        private void lstAtivos_Click(object sender, EventArgs e)
        {
            string[] parte;
            parte = lstAtivos.SelectedValue.ToString().Split('-');
            
        switch (Convert.ToInt32(parte[1]))
        {
            case 1:
                groupOPFUT.Enabled = false;
                groupOPT.Enabled = false;

                groupclass.Enabled = true;
                groupLot.Enabled = true;
                break;
            case 2:
                groupOPFUT.Enabled = false;
                groupOPT.Enabled = false;

                groupclass.Enabled = true;
                groupLot.Enabled = true;
                break;
            case 3:
                groupOPFUT.Enabled = false;
                groupOPT.Enabled = false;
                groupclass.Enabled = false;
                groupLot.Enabled = false;
                break;
            case 4:
                groupOPFUT.Enabled = false;
                groupOPT.Enabled = false;

                groupclass.Enabled = true;
                groupLot.Enabled = true;
                break;
            case 5:
                groupOPT.Enabled = false;

                groupclass.Enabled = true;
                groupLot.Enabled = true;
                groupOPFUT.Enabled = true;

                break;
            case 6:
                groupOPFUT.Enabled = false;
                groupOPT.Enabled = false;

                groupclass.Enabled = true;
                groupLot.Enabled = true;

                break;
            case 7:
                groupclass.Enabled = true;
                groupLot.Enabled = true;
                groupOPFUT.Enabled = true;
                groupOPT.Enabled = true;

                break;
        }

            Carrega_Ativos(Convert.ToInt32(parte[0]),Convert.ToInt32(parte[1]));
        }

        private void Carrega_Ativos(int Id_Ativo, int Tipo_Ativo)
        {
            lblId_Ticker.Text = Id_Ativo.ToString();
            string Sql_String;
            Sql_String = "Select A.*, B.Cod_Reuters,Cod_BBL from Tb001_Ativos A " +
            " left join Tb119_Convert_Simbolos B on A.Id_Ativo = B.Id_Ativo" +
            " Where A.id_Ativo = " + Id_Ativo ;

            SqlDataReader Dr_Ativos = CargaDados.DB.Execute_Query_DataRead(Sql_String);

            /* while (Dr_Ativos.Read())
             {
                 foreach (Control ExistingControl in this.con
                 {
                     MessageBox.Show(ExistingControl.Name.ToString() + " - " + Valida.Left(ExistingControl.Name.ToString(), 4));
                     if ( Valida.Left(ExistingControl.Name.ToString(),4) == "txti")
                     {
                        
                      if (Dr_Ativos[Valida.Right(ExistingControl.Name, ExistingControl.Name.Length - 4)] != null)
                      {
                          ExistingControl.Text = Dr_Ativos[Valida.Right(ExistingControl.Name, ExistingControl.Name.Length - 4)].ToString();
                      }
                     }
                 }
             }        
             */

            while (Dr_Ativos.Read())
            {
                if (Dr_Ativos["Id_Instituicao"].ToString() != "")
                CargaDados.carregacombo(cmbIssuer, "Select Id_Instituicao,Nome from Tb000_Instituicoes", "Id_Instituicao", "Nome", Convert.ToInt32(Dr_Ativos["Id_Instituicao"].ToString()));

                if (Dr_Ativos["Id_Tipo_Ativo"].ToString() != "")
                    CargaDados.carregacombo(cmbTickerType, "Select Id_Tipo_Ativo,Nome_Tipo_Ativo from Tb024_Tipo_Ativos", "Id_Tipo_Ativo", "Nome_Tipo_Ativo", Convert.ToInt32(Dr_Ativos["Id_Tipo_Ativo"].ToString()));

                if (Dr_Ativos["Id_Tipo_Ativo"].ToString() != "")
                    CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", Convert.ToInt32(Dr_Ativos["Moeda"].ToString()));

                if (Dr_Ativos["Moeda_Premio"].ToString() != "")
                    CargaDados.carregacombo(this.cmbCurrenPrize, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", Convert.ToInt32(Dr_Ativos["Moeda_Premio"].ToString()));

                if (Dr_Ativos["Moeda_Strike"].ToString() != "")
                    CargaDados.carregacombo(this.cmbStrikeCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", Convert.ToInt32(Dr_Ativos["Moeda_Strike"].ToString()));

                if (Dr_Ativos["Bolsa_Primaria"].ToString() != "")
                  CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", Convert.ToInt32(Dr_Ativos["Bolsa_Primaria"].ToString()));

                if (Dr_Ativos["Ativo_Objeto"].ToString() != "")
                  CargaDados.carregacombo(this.cmbObjectTicker, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Tipo_Ativo in(1,2,3,4,5,6)", "Id_Ativo", "Simbolo", Convert.ToInt32(Dr_Ativos["Ativo_Objeto"].ToString()));

                if (Dr_Ativos["Id_Instrumento"].ToString() != "")
                    CargaDados.carregacombo(this.cmbInstrument, "Select Id_Instrumento,Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", Convert.ToInt32(Dr_Ativos["Id_Instrumento"].ToString()));

                if (Dr_Ativos["Id_Classe_Ativo"].ToString() != "")
                        CargaDados.carregacombo(this.cmbAssetClass, "Select Id_Classe_Ativo,Classe_Ativo  from Tb028_Classe_Ativo ", "Id_Classe_Ativo", "Classe_Ativo", Convert.ToInt32(Dr_Ativos["Id_Classe_Ativo"].ToString()));

                if (Dr_Ativos["Frequencia_Atualizacao"].ToString() != "")
                    CargaDados.carregacombo(this.cmbUpFreq, "Select Id_Frequencia,Frequencia from Tb105_Frequencia_Atualizacao  ", "Id_Frequencia", "Frequencia", Convert.ToInt32(Dr_Ativos["Frequencia_Atualizacao"].ToString()));

            if (Dr_Ativos["Fonte_Atualizacao"].ToString() != "")
                CargaDados.carregacombo(this.cmbFonteUp, "Select Id_Sistemas_Informacoes,Descricao  from Tb102_Sistemas_Informacoes ", "Id_Sistemas_Informacoes", "Descricao", Convert.ToInt32(Dr_Ativos["Fonte_Atualizacao"]));

                if (Dr_Ativos["Nome"].ToString() != "")
                {
                    txtiNome.Text = Dr_Ativos["Nome"].ToString();
                }
                else
                {
                    txtiNome.Text = "";
                }

                if (Dr_Ativos["Simbolo"].ToString() != "")
                {
                    txtiSimbolo.Text = Dr_Ativos["Simbolo"].ToString();
                }
                else
                {
                    txtiSimbolo.Text = "";
                }


                if (Dr_Ativos["Preco_Exercicio"].ToString() != "")
                {
                    txtiPreco_Exercicio.Text = Convert.ToDecimal(Dr_Ativos["Preco_Exercicio"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txtiPreco_Exercicio.Text = "";
                }

                if (Dr_Ativos["COD_BBL"].ToString() != "")
                {
                   txtBlbTicker.Text = Dr_Ativos["COD_BBL"].ToString();
                }
                else
                {
                    txtBlbTicker.Text = "";
                }

                if (Dr_Ativos["Cod_Reuters"].ToString() != "")
                {
                   txtReutersTicker.Text = Dr_Ativos["Cod_Reuters"].ToString();
                }
                else
                {
                    txtReutersTicker.Text = "";
                }



                if (Dr_Ativos["Tipo_C_V"].ToString() != "")
                {
                    if (Dr_Ativos["Tipo_C_V"].ToString() == "1")
                    {
                        rdCall.Checked = true;
                        rdPut.Checked = false;
                    }
                    else
                    {
                        rdCall.Checked = false;
                        rdPut.Checked = true;
                    }
                }
                else
                {
                    rdCall.Checked = false;
                    rdPut.Checked = false;
                }


                if (Dr_Ativos["Lote_Padrao"].ToString() != "")
                {
                    txtiLote_Padrao.Text = Convert.ToDecimal(Dr_Ativos["Lote_Padrao"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txtiLote_Padrao.Text = "";
                }

                if (Dr_Ativos["Lote_Negociacao"].ToString() != "")
                {
                    txtiLote_Negociacao.Text = Convert.ToDecimal(Dr_Ativos["Lote_Negociacao"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txtiLote_Negociacao.Text = "";
                }

                if (Dr_Ativos["Vencimento"].ToString() != "")
                {
                    txtExpiration.Value = Convert.ToDateTime(Dr_Ativos["Vencimento"].ToString());
                }
                else
                {
                    txtExpiration.Text = "";
                }
            }
            Dr_Ativos.Close();
            Dr_Ativos.Dispose();
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int retorno = Atualiza_Dados();

            if (retorno == 0)
            {
                MessageBox.Show("Error on update. Verify Data.");
            }
            else
            {
                MessageBox.Show("Updated.");
            }
        }

        private int Atualiza_Dados()
        {
            string SQL_String = "";
            DateTime data_ex = Convert.ToDateTime(txtExpiration.Value.ToString());

            string Preco_Exercicio;
            if (txtiPreco_Exercicio.Text.ToString() != "")
            {
                Preco_Exercicio = Convert.ToString(Convert.ToDouble(txtiPreco_Exercicio.Text.ToString())).Replace(",",".");
            }
            else
                Preco_Exercicio = "0";

            switch (Convert.ToInt32(cmbTickerType.SelectedValue.ToString()))
            {
                case 1:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "")
                    {

                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                        "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",",".").ToString() +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",",".").ToString() +
                                        "  ,[Bolsa_Primaria] = " + cmbPrimaryEx.SelectedValue.ToString() +
                                        "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());


                    }
                    break;
                case 2:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "")
                    {

                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                        "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",",".").ToString() +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",",".").ToString() +
                                        "  ,[Bolsa_Primaria] = " + cmbPrimaryEx.SelectedValue.ToString() +
                                        "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());


                    }
                    break;
                case 3:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "")
            {
                  SQL_String = "UPDATE [Tb001_Ativos] " +
                                   "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                    "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                    "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                   "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                    "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                    "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());
              } 
                    break;
                case 4:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "")
                    {

                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                       "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",",".").ToString() +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",",".").ToString() +
                                        "  ,[Bolsa_Primaria] = " + cmbPrimaryEx.SelectedValue.ToString() +
                                        "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());
                    }
                    break;
                case 5:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "")
                    {

                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",",".").ToString() +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",",".").ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Ativo_Objeto] = " + cmbObjectTicker.SelectedValue.ToString() +
                                        "  ,[Vencimento] = '" + txtExpiration.Value.ToString("yyyyMMdd") +
                                        "'  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString()) ;

                    }
                    break;
                case 6:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "")
                    {

                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                        "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",",".").ToString() +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",",".").ToString() +
                                        "  ,[Bolsa_Primaria] = " + cmbPrimaryEx.SelectedValue.ToString() +
                                        "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());
                    }
                    break;
                case 7:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "" && (rdCall.Checked || rdPut.Checked))
                    {



                        int Tipo_Cal_Put;
                        if (rdCall.Checked == true)
                        {
                            Tipo_Cal_Put = 1;
                        }
                        else
                        {
                            Tipo_Cal_Put = 0;
                        }
                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",", ".").ToString() +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",", ".").ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Ativo_Objeto] = " + cmbObjectTicker.SelectedValue.ToString() +
                                        "  ,[Vencimento] = '" + data_ex.ToString("yyyyMMdd") +
                                        "'  ,[Moeda_Premio] = " + cmbCurrenPrize.SelectedValue.ToString() +
                                        "  ,[Preco_Exercicio] = " + Preco_Exercicio.ToString() +
                                        "  ,[Moeda_Strike] = " + cmbStrikeCurrency.SelectedValue.ToString() +
                                        "  ,[Tipo_C_V] = " + Tipo_Cal_Put.ToString() +
                                        "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());
                    }
                    break;
                case 8:
                    if (txtiNome.Text != "" && txtiSimbolo.Text != "" && txtiLote_Padrao.Text != "" && txtiLote_Negociacao.Text != "")
                    {

                        SQL_String = "UPDATE [Tb001_Ativos] " +
                                       "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                                        "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                                        "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                                        "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                                        "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                                        "  ,[Frequencia_Atualizacao] = " + cmbUpFreq.SelectedValue.ToString() +
                                        "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                                        "  ,[Moeda] = " + cmbCurrency.SelectedValue.ToString() +
                                        "  ,[Lote_Padrao] =" + txtiLote_Padrao.Text.Replace(",", ".").ToString() +
                                        "  ,[Vencimento] = '" + data_ex.ToString("yyyyMMdd") + "'" +
                                        "  ,[Lote_Negociacao] =" + txtiLote_Negociacao.Text.Replace(",", ".").ToString() +
                                        "  ,[Bolsa_Primaria] = " + cmbPrimaryEx.SelectedValue.ToString() +
                                        "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());
                    }
                    break;
   
                default :
                    SQL_String = "";
                    break;
            }
            int retorno;
            if (SQL_String != "")
            {
                retorno = CargaDados.DB.Execute_Insert_Delete_Update(SQL_String);

                if (retorno != 0)
                {
                    string Cod_Imagine;

                    Cod_Imagine = txtReutersTicker.Text.ToString() + "-" + Valida.Right(txtExpiration.Value.Year.ToString(),2);

                    SQL_String = "INSERT INTO Tb119_Convert_Simbolos (ID_Ativo, Cod_Reuters,Cod_BBL, Cod_Imagine) values" +
                                 "(" + lblId_Ticker.Text.ToString() + ",'" + txtReutersTicker.Text.ToString() + "'," +
                                 "'" + txtBlbTicker.Text.ToString() + "'," +
                                 "'" + Cod_Imagine + "')";
                    retorno = CargaDados.DB.Execute_Insert_Delete_Update(SQL_String);
                }
            }
            else
                retorno = 0;

            return retorno;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {

          int retorno = Negocios.Busca_Dados_Historicos(Convert.ToInt32(lblId_Ticker.Text.ToString()), "19000101");

          if (retorno == 0)
          {
              MessageBox.Show("Error on Get Historical!");          
          }

        }


     }
}