using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ServiceInterfaces.PassivoService;

namespace WebServiceExternoUnitTests
{
  [TestFixture]
  public class PassivoServiceTest
  {

    /// <summary>
    /// Testa o pedido de posi��es por fundo
    /// </summary>
    [Test]
    public void PosicaoCotistaPorFundoTest()
    {
      PassivoServiceClient cliente = new PassivoServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      PosicaoContaPorFundo[] pos = null;
      DateTime dataPosicao = DateTime.Now;
      String hash = String.Empty;
      int codigoExternoFundo = int.MinValue;
      RetornoWebService retVal = null;

      // TODO : retirar o coment�rio e substituir pelas vari�veis com valores corretos.
      //retVal = cliente.PosicaoCotistaPorFundo(out pos,hash,codigoExternoFundo,dataPosicao);
      
      Assert.That(pos != null && retVal.Status == eStatusRetorno.Sucesso);
    }

    /// <summary>
    /// Testa o pedido de movimenta��es por fundo
    /// </summary>
    [Test]
    public void MovimentacaoCotistaPorFundoTest()
    {
      PassivoServiceClient cliente = new PassivoServiceClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      MovimentacoesFundo movFundo = null;
      String hash = String.Empty;
      int codigoExternoFundo = int.MinValue;
      DateTime dataInicial = DateTime.Now;
      DateTime dataFinal = DateTime.Now.AddDays(1);
      RetornoWebService retVal = null; 

      // TODO :  retirar o coment�rio e substituir pelas vari�veis com valores corretos.
      //retVal = cliente.MovimentacaoCotistaPorFundo(out movFundo,hash,codigoExternoFundo,dataInicial, dataFinal);
      
      Assert.That(movFundo != null && retVal.Status == eStatusRetorno.Sucesso);
    }
  }
}
