using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ServiceInterfaces.Service;

namespace WSNotificacaoUnitTests
{
  [TestFixture]
  public class TesteComunicacao
  {
    /// <summary>
    /// Testa o pedido hello word
    /// </summary>
    [Test]
    public void TestaHelloWorld()
    {
      NotificacaoClient cliente = new NotificacaoClient();
      Assert.IsFalse(cliente.State == System.ServiceModel.CommunicationState.Faulted);

      string hello = cliente.HelloWorld().Trim().ToLower();
      Assert.AreEqual(hello, "hello world");
    }
  }
}
