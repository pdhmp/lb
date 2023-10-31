using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ServiceInterfaces.CarteiraService;

namespace WebServiceExternoUnitTests
{
  [TestFixture]
  public class CarteiraServiceTest
  {

    /// <summary>
    /// Testa o pedido de posições por fundo
    /// </summary>
    [Test]
    public void PosicaoCarteiraTest()
    {
      CarteiraServiceClient cliente = new CarteiraServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      ComposicaoCarteira pos = null;
      DateTime dataPosicao = DateTime.Now;
      string hash = string.Empty;
      string codigoCarteira = "123456";
      RetornoWebService retVal = null;

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      retVal = cliente.RetornaPosicaoCarteira(out pos, hash, codigoCarteira, dataPosicao);

      Assert.That(pos != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa o pedido de movimentações por fundo
    /// </summary>
    [Test]
    public void ExtratoContaCorrenteTest()
    {
      CarteiraServiceClient cliente = new CarteiraServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      ExtratoContaCorrente extrato = null;
      string hash = string.Empty;
      string codigoCarteira = string.Empty;
      DateTime dataInicial = DateTime.Now.AddDays(-1);
      DateTime dataFinal = DateTime.Now;
      RetornoWebService retVal = null;

      // TODO :  retirar o comentário e substituir pelas variáveis com valores corretos.
      //retVal = cliente.RetornaPosicaoContaCorrente(out extrato, hash, codigoCarteira, dataInicial, dataFinal);

      Assert.That(extrato != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a Liberação ou Reprovação da Carteira
    /// </summary>
    [Test]
    public void LiberacaoCarteiraTest()
    {
      CarteiraServiceClient cliente = new CarteiraServiceClient();
      List<string> listaCarteiras = new List<string>();
      bool liberar = true;
      string hash = string.Empty;
      listaCarteiras.Add("CARTEIRA A");
      listaCarteiras.Add("CARTEIRA B");
      DateTime dataPosicao = DateTime.Now;
      // O motivo deverá ser usado nos casos de Reprovação
      string motivo = "MOTIVO DE REPROVACAO";
      RetornoWebService retVal = null;
      InformacaoCarteira[] informacaoCarteira = new InformacaoCarteira[] { };

      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      // retVal = cliente.LiberaCarteira(out informacaoCarteira, hash, listaCarteiras.ToArray(), dataPosicao, liberar, motivo);

      Assert.That(informacaoCarteira != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a geração do Xml Anbid
    /// </summary>
    [Test]
    public void CarteiraXmlTest()
    {
      CarteiraServiceClient cliente = new CarteiraServiceClient();

      byte[] xmlConteudo = new byte[] { };
      ValidadorXml validadorXml = new ValidadorXml();
      DateTime dataPosicao = new DateTime(2010, 08, 26);
      string hash = string.Empty;
      string codCarteira = "GAP ABSOLUTO";
      RetornoWebService retVal = null;

      //TODO : retirar o comentário e substituir pelas variáveis com valores corretos. 
     //retVal = cliente.GerarXmlAnbimaCarteira(out xmlConteudo, out validadorXml, hash, codCarteira, dataPosicao);
     Assert.That(xmlConteudo != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa a geração do arquivo Txt de carteira sintética
    /// Refere-se ao: menu do SMA ativo > analise carteira
    /// </summary>
    [Test]
    public void CarteiraSinteticaTest()
    {
      CarteiraServiceClient cliente = new CarteiraServiceClient();
      byte[] dadosCarteira = new byte[] { };
      string hash = string.Empty;
      string codCarteira = "GAP ABSOLUTO";
      RetornoWebService retVal = null;

      DateTime dataPosicao = new DateTime(2010, 08, 26);
      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      // retVal = cliente.GerarAnaliseCarteiraSintetica(out dadosCarteira, hash, codCarteira, dataPosicao);

      Assert.That(dadosCarteira != null && retVal.Status == eStatusRetorno.Sucesso);
    }
    /// <summary>
    /// Testa a geração dos dados do Analise Carteira Analitica
    /// Refere-se ao: menu do SMA ativo > analise carteira
    /// </summary>
    [Test]
    public void CarteiraAnaliticaTest()
    {
      CarteiraServiceClient cliente = new CarteiraServiceClient();
      AnaliseCarteiraDemostrativo dadosCarteira = new AnaliseCarteiraDemostrativo(); 
      string hash = string.Empty;
      string codCarteira = "GAP ABSOLUTO";
      RetornoWebService retVal = null;

      DateTime dataPosicao = new DateTime(2010, 08, 26);
      // TODO : retirar o comentário e substituir pelas variáveis com valores corretos.
      // retVal = cliente.GerarAnaliseCarteiraAnalitica(out dadosCarteira, "cd21b989-7606-46de-bd3e-09f1a858532a", codCarteira, dataPosicao);

      Assert.That(dadosCarteira != null && retVal.Status == eStatusRetorno.Sucesso);
    }  
  }
}