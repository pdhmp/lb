using System;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading;
using ServiceInterfaces;

public class ClientProcessadorEventos : IProcessadorEventos
{

  /// <summary>
  /// Recebe uma notificação do BNYMellon.
  /// </summary>
  /// <param name="evento">Evento recebido</param>
  public void Notificar(Evento evento)
  {
    Thread.Sleep(1000);
  }
}