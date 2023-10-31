using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ServiceInterfaces.AtivosService;

namespace WebServiceExternoUnitTests
{
  [TestFixture]
  public class AtivosServiceTest
  {

    /// <summary>
    /// Testa o bloqueio de boletos por arquivo
    /// </summary>
    [Test]
    public void BloquearBoletosDoArquivoTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      RetornoOperacaoBoletos bol = null;
      int idArquivoBoleto = 0;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.BloquearBoletosPorArquivo(out bol, idArquivoBoleto);

      Assert.That(bol != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a consulta de boletos adelic por arquivo
    /// </summary>
    [Test]
    public void ConsultarBoletosAdelicTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      List<BoletoAdelicDTO> listaBoletos = null;
      int idArquivoBoleto = 0;
      string hash = string.Empty;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.ConsultaBoletosAdelic(out listaBoletos, hash, idArquivoBoleto);

      Assert.That(listaBoletos != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a consulta de boletos futuro por arquivo
    /// </summary>
    [Test]
    public void ConsultarBoletosFuturoTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      List<BoletoFuturoDTO> listaBoletos = null;
      int idArquivoBoleto = 0;
      string hash = string.Empty;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.ConsultaBoletosFuturo(out listaBoletos, hash, idArquivoBoleto);

      Assert.That(listaBoletos != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a consulta de boletos RF por arquivo
    /// </summary>
    [Test]
    public void ConsultarBoletosRFTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      List<BoletoRFDTO> listaBoletos = null;
      int idArquivoBoleto = 0;
      string hash = string.Empty;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.ConsultaBoletosRF(out listaBoletos, hash, idArquivoBoleto);

      Assert.That(listaBoletos != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a consulta de boletos RV por arquivo
    /// </summary>
    [Test]
    public void ConsultarBoletosRVTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      List<BoletoRVDTO> listaBoletos = null;
      int idArquivoBoleto = 0;
      string hash = string.Empty;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.ConsultaBoletosRV(out listaBoletos, hash, idArquivoBoleto);

      Assert.That(listaBoletos != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a consulta de boletos por arquivo
    /// </summary>
    [Test]
    public void ConsultarArquivoBoletoTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      ArquivoBoletoDTO arqBoleto = null;
      int idArquivoBoleto = 0;
      string hash = string.Empty;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      retVal = cliente.ConsultarArquivoBoleto(out arqBoleto, hash, idArquivoBoleto);

      Assert.That(arqBoleto != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a exclusão dos boletos por arquivo
    /// </summary>
    [Test]
    public void ExcluiBoletosPorArquivoTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      RetornoOperacaoBoletos retorno = null;
      int idArquivoBoleto = 0;
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      retVal = cliente.ExcluirBoletosPorArquivo(out retorno, idArquivoBoleto);

      Assert.That(retorno != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa o upload do arquivo boleto
    /// </summary>
    [Test]
    public void UploadArquivoBoletoTest()
    {
      AtivosServiceClient cliente = new AtivosServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      Byte[] dadosArquivo = null;
      string nomeArquivo = string.Empty;
      string retVal = string.Empty;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.UploadArquivoBoleto(nomeArquivo, dadosArquivo);

      Assert.That(retVal != null && dadosArquivo != null);
    }
    
  }
}