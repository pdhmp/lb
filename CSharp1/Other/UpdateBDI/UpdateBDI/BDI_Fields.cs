using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateBDI
{

    public class BDI_Fields
    {
        public BDI_Fields() { }

        public string IdSecurity = "";
        public string TipoRegistro = "";
        public string Ticker = "";
        public string PrecoAbertura = "";
        public string PrecoMaximo = "";
        public string PrecoMinimo = "";
        public string PrecoMedio = "";
        public string PrecoUltimoNegocio = "";
        public string SinalOscilacao = "";
        public string OscilacaoPreco = "";
        public string PrecoMelhorOfertaCompra = "";
        public string PrecoMelhorOfertaVenda = "";
        public string NroNegocioEfetuados = "";
        public string QuantidadeTitulosNegociados = "";
        public string VolumeTotalTItulos = "";

        public string Insert(int PriceType, DateTime DataPregao)
        {
            switch (PriceType)
            {
                case 1:
                    if (double.Parse(PrecoUltimoNegocio) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoUltimoNegocio + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                case 3:
                    if (double.Parse(PrecoMinimo) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoMinimo + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                case 4:
                    if (double.Parse(PrecoMaximo) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoMaximo + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                               case 7:
                    if (double.Parse(PrecoMedio) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoMedio + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                case 8:
                    if (double.Parse(PrecoAbertura) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoAbertura + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                case 9:
                    if (double.Parse(PrecoMelhorOfertaCompra) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoMelhorOfertaCompra + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                case 10:
                    if (double.Parse(PrecoMelhorOfertaVenda) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + PrecoMelhorOfertaVenda + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                case 11:
                    if (double.Parse(QuantidadeTitulosNegociados) > 0)
                    { return "EXEC Proc_Insert_Price	@IdSecurity = " + this.IdSecurity + ",@SrValue = " + QuantidadeTitulosNegociados + ",@Data = '" + DataPregao.ToString("yyyy-MM-dd") + "',@SrType = " + PriceType + ",@Source = 24,@Automated = 1 ;"; }
                    return "";
                default: return "";
            }
        }



        public void BDI_02_ResumoDiarioNegociacoesPapel(string sTexto)
        {

            Ticker = sTexto.Substring(57, 12).Trim();

            PrecoUltimoNegocio = sTexto.Substring(134, 9) + "." + sTexto.Substring(143, 2); // 1 LAST
            PrecoMinimo = sTexto.Substring(112, 9) + "." + sTexto.Substring(121, 2);        // 3 LOW
            PrecoMaximo = sTexto.Substring(101, 9) + "." + sTexto.Substring(110, 2);        // 4 HIGH
            QuantidadeTitulosNegociados = sTexto.Substring(178, 15) + "." + sTexto.Substring(191, 2);        // 11 SHARES_TRADED
            PrecoMedio = sTexto.Substring(123, 9) + "." + sTexto.Substring(132, 2);         // 7 AVERAGE
            PrecoAbertura = sTexto.Substring(90, 9) + "." + sTexto.Substring(99, 2);        // 8 OPEN
            PrecoMelhorOfertaCompra = sTexto.Substring(151, 9) + "." + sTexto.Substring(160, 2);  // 9 BID
            PrecoMelhorOfertaVenda = sTexto.Substring(162, 9) + "." + sTexto.Substring(171, 2);   // 10 ASK

            if (PrecoUltimoNegocio == "0" || PrecoMinimo == "0" || PrecoMaximo == "0" || PrecoMedio == "0" || PrecoAbertura == "0" || PrecoMelhorOfertaCompra == "0" || PrecoMelhorOfertaVenda == "0")
            {

            }
        }

        public void BDI_01_ResumoDiarioIndices(string sTexto)
        {

            Ticker = sTexto.Substring(04, 30).Trim();

            PrecoUltimoNegocio = sTexto.Substring(92, 6); // 1 LAST
            PrecoMinimo = sTexto.Substring(40, 6);        // 3 LOW
            PrecoMaximo = sTexto.Substring(46, 6) ;        // 4 HIGH
            QuantidadeTitulosNegociados = sTexto.Substring(164, 15);        // 11 SHARES_TRADED
            PrecoMedio = "0";//sTexto.Substring(123, 9) + "." + sTexto.Substring(132, 2);         // 7 AVERAGE
            PrecoAbertura = sTexto.Substring(34, 6);        // 8 OPEN
            PrecoMelhorOfertaCompra = "0";//= sTexto.Substring(151, 9) + "." + sTexto.Substring(160, 2);  // 9 BID
            PrecoMelhorOfertaVenda = "0";//= sTexto.Substring(162, 9) + "." + sTexto.Substring(171, 2);   // 10 ASK

            if (PrecoUltimoNegocio == "0" || PrecoMinimo == "0" || PrecoMaximo == "0" || PrecoMedio == "0" || PrecoAbertura == "0" || PrecoMelhorOfertaCompra == "0" || PrecoMelhorOfertaVenda == "0")
            {

            }
        }
    }
}
