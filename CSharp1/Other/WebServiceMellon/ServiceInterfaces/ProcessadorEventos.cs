﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3615
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceInterfaces
{
  using System.Runtime.Serialization;


  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "Evento", Namespace = "http://schemas.datacontract.org/2004/07/ClienteExternoInterface")]
  public partial class Evento : object, System.Runtime.Serialization.IExtensibleDataObject
  {

    private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

    private ServiceInterfaces.Parametro[] ParametrosField;

    private ServiceInterfaces.Servico[] ServicosField;

    private ServiceInterfaces.Token TokenField;

    public System.Runtime.Serialization.ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public ServiceInterfaces.Parametro[] Parametros
    {
      get
      {
        return this.ParametrosField;
      }
      set
      {
        this.ParametrosField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public ServiceInterfaces.Servico[] Servicos
    {
      get
      {
        return this.ServicosField;
      }
      set
      {
        this.ServicosField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public ServiceInterfaces.Token Token
    {
      get
      {
        return this.TokenField;
      }
      set
      {
        this.TokenField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "Token", Namespace = "http://schemas.datacontract.org/2004/07/Mellon.Seguranca.SegurancaBusinessRules")]
  public partial class Token : object, System.Runtime.Serialization.IExtensibleDataObject
  {

    private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

    private System.DateTime DataCriacaoField;

    private string HashField;

    public System.Runtime.Serialization.ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public System.DateTime DataCriacao
    {
      get
      {
        return this.DataCriacaoField;
      }
      set
      {
        this.DataCriacaoField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Hash
    {
      get
      {
        return this.HashField;
      }
      set
      {
        this.HashField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "Parametro", Namespace = "http://schemas.datacontract.org/2004/07/ClienteExternoInterface")]
  public partial class Parametro : object, System.Runtime.Serialization.IExtensibleDataObject
  {

    private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

    private string ChaveField;

    private string ValorField;

    public System.Runtime.Serialization.ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Chave
    {
      get
      {
        return this.ChaveField;
      }
      set
      {
        this.ChaveField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Valor
    {
      get
      {
        return this.ValorField;
      }
      set
      {
        this.ValorField = value;
      }
    }
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
  [System.Runtime.Serialization.DataContractAttribute(Name = "Servico", Namespace = "http://schemas.datacontract.org/2004/07/ClienteExternoInterface")]
  public partial class Servico : object, System.Runtime.Serialization.IExtensibleDataObject
  {

    private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

    private string NomeField;

    private string UrlField;

    public System.Runtime.Serialization.ExtensionDataObject ExtensionData
    {
      get
      {
        return this.extensionDataField;
      }
      set
      {
        this.extensionDataField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute(IsRequired = true)]
    public string Nome
    {
      get
      {
        return this.NomeField;
      }
      set
      {
        this.NomeField = value;
      }
    }

    [System.Runtime.Serialization.DataMemberAttribute()]
    public string Url
    {
      get
      {
        return this.UrlField;
      }
      set
      {
        this.UrlField = value;
      }
    }
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
  [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ServiceInterfaces.IProcessadorEventos")]
  public interface IProcessadorEventos
  {

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IProcessadorEventos/Notificar", ReplyAction = "http://tempuri.org/IProcessadorEventos/NotificarResponse")]
    void Notificar(ServiceInterfaces.Evento myValue);
  }

  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
  public interface IProcessadorEventosChannel : ServiceInterfaces.IProcessadorEventos, System.ServiceModel.IClientChannel
  {
  }

  [System.Diagnostics.DebuggerStepThroughAttribute()]
  [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
  public partial class ProcessadorEventosClient : System.ServiceModel.ClientBase<ServiceInterfaces.IProcessadorEventos>, ServiceInterfaces.IProcessadorEventos
  {

    public ProcessadorEventosClient()
    {
    }

    public ProcessadorEventosClient(string endpointConfigurationName)
      :
            base(endpointConfigurationName)
    {
    }

    public ProcessadorEventosClient(string endpointConfigurationName, string remoteAddress)
      :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public ProcessadorEventosClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress)
      :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public ProcessadorEventosClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress)
      :
            base(binding, remoteAddress)
    {
    }

    public void Notificar(ServiceInterfaces.Evento myValue)
    {
      base.Channel.Notificar(myValue);
    }
  }
}
