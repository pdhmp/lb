using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LiveBook.Business;
using NestDLL;

namespace LiveBook
{
    public partial class frmCopySecurity : Form
    {
        newNestConn curConn = new newNestConn();
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();

        public string Id_Ativo_Inserido;
        public string Nome_Ativo_Inserido;

        public frmCopySecurity()
        {
            InitializeComponent();
        }

        private void frmCopySecurity_Load(object sender, EventArgs e)
        {
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;
            int flag_new_Table = 1;

            if (flag_new_Table == 0)
            {

                //if (txtName.Text != "" && txtNestTicker.Text != "")
                //{

                //    string tempDate = "null";

                //    if (txtExpiration.Value.ToString("yyyyMMdd") != "19000101")
                //    {
                //        tempDate = "'" + txtExpiration.Value.ToString("yyyyMMdd") + "'";
                //    }
                //    else
                //    {
                //        tempDate = "null";
                //    }
                //    SQLString = "INSERT INTO [NESTDB].[dbo].[ZTb001_Ativos]([Nome],[Id_Instituicao],[Simbolo],[Id_Price_Table],[Id_Instrumento],[Id_Classe_Ativo],[Frequencia_Atualizacao],[Fonte_Atualizacao],[Descricao],[Moeda],[Lote_Padrao],[Lote_Negociacao],[Tipo_Cotacao],[Bolsa_Primaria],[Ativo_Objeto],[Vencimento],[Passivo_Objeto],[Taxa_Ativo],[Taxa_Passivo],[Preco_Exercicio],[Moeda_Premio],[Moeda_Strike],[Tipo_Taxa_Ativo],[Tipo_Taxa_Passivo],[Tipo_C_V],[ISIN],[Paga_Dividendo],[Split],[Descontinuado],[Quoted_As_Rate]) " +
                //                " SELECT '" + txtName.Text + "',[Id_Instituicao],'" + txtNestTicker.Text + "',[Id_Price_Table],[Id_Instrumento],[Id_Classe_Ativo],[Frequencia_Atualizacao],[Fonte_Atualizacao],[Descricao],[Moeda],[Lote_Padrao],[Lote_Negociacao],[Tipo_Cotacao],[Bolsa_Primaria],[Ativo_Objeto]," + tempDate + ",[Passivo_Objeto],[Taxa_Ativo],[Taxa_Passivo],[Preco_Exercicio],[Moeda_Premio],[Moeda_Strike],[Tipo_Taxa_Ativo],[Tipo_Taxa_Passivo],[Tipo_C_V],[ISIN],[Paga_Dividendo],[Split],[Descontinuado],[Quoted_As_Rate] " +
                //                " FROM [NESTDB].[dbo].[ZTb001_Ativos] WHERE Id_Ativo=" + txtOldId.Text + ";" +
                //                " SELECT @@IDENTITY";

                //    Id_Ativo_Inserido = curConn.Execute_Query_String(SQLString);

                //    if (Id_Ativo_Inserido == "")
                //    {
                //        MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                //    }
                //    else
                //    {
                //        SQLString = "INSERT INTO [NESTDB].[dbo].[Tb119_Convert_Simbolos]([Id_Ativo],[Cod_Reuters],[Cod_BBL],[Cod_Imagine]) " +
                //                    " SELECT " + Id_Ativo_Inserido + ",'" + Id_Ativo_Inserido + "_' + [Cod_Reuters],'" + Id_Ativo_Inserido + "_' + [Cod_BBL],'" + Id_Ativo_Inserido + "_' + [Cod_Imagine] FROM [NESTDB].[dbo].[Tb119_Convert_Simbolos] WHERE Id_Ativo=" + txtOldId.Text + ";";
                //        string resultado = curConn.Execute_Query_String(SQLString);
                //        MessageBox.Show("The security was inserted in the database. Please edit the previous screen to update the security specific information.");
                //        this.Hide();
                //    }
                //    Nome_Ativo_Inserido = txtName.Text.ToString();
                //}
                //else
                //{
                //    MessageBox.Show("Name and Nest ticker must be filled in!");
                //}
            }
            else
            {
                if (txtName.Text != "" && txtNestTicker.Text != "")
                {

                    string tempDate = "null";

                    if (txtExpiration.Value.ToString("yyyyMMdd") != "19000101")
                    {
                        tempDate = "'" + txtExpiration.Value.ToString("yyyyMMdd") + "'";
                    }
                    else
                    {
                        tempDate = "null";
                    }

                    SQLString = "INSERT INTO Tb001_Securities_Fixed (Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass]," +
                                " IdStrikeCurrency,IdPremiumCurrency,OptionType,BloombergTicker,ReutersTicker,Imagineticker)" +
                                " Select Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass]," +
                                " IdStrikeCurrency,IdPremiumCurrency,OptionType,CASE WHEN BloombergTicker<>'' THEN 'NEW_' ELSE '' END + BloombergTicker,CASE WHEN ReutersTicker<>'' THEN 'NEW_' ELSE '' END + ReutersTicker,CASE WHEN Imagineticker<>'' THEN 'NEW_' ELSE '' END + Imagineticker " + 
                                " FROM Tb001_Securities_Fixed " +
                                " Where IdSecurity = " + txtOldId.Text + ";" +
                                " SELECT @@IDENTITY";

                    Id_Ativo_Inserido = curConn.Execute_Query_String(SQLString);

                    if (Id_Ativo_Inserido == "")
                    {
                        MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                    }
                    else
                    {
                        SQLString = "INSERT INTO Tb001_Securities_Variable([IdSecurity],[SecName],[NestTicker],[RoundLot],[RoundLotSize],IdPrimaryExchange,Expiration,PriceFromUnderlying)" +
                                    " SELECT Top 1 " + Id_Ativo_Inserido + ",'" + txtName.Text + "','" + txtNestTicker.Text + "',[RoundLot],[RoundLotSize],IdPrimaryExchange,'" + txtExpiration.Value.ToString("yyyyMMdd") + "',0" +
                                    " FROM  Tb001_Securities_Variable " +
                                    " Where IdSecurity = " + txtOldId.Text + " order by ValidAsOfDate desc ;";

                        int resultado = curConn.ExecuteNonQuery(SQLString);
                       
                        MessageBox.Show("The security was inserted in the database. Please edit the previous screen to update the security specific information.");
                        this.Hide();
                    }
                    Nome_Ativo_Inserido = txtName.Text.ToString();
                }
                else
                {
                    MessageBox.Show("Name and Nest ticker must be filled in!");
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblNewIssuer_Click(object sender, EventArgs e)
        {
            frmIssuer InsertIssuer = new frmIssuer();
            InsertIssuer.ShowDialog(this);
        }

    }
}