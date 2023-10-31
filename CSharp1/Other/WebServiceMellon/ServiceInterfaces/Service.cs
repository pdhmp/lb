﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceInterfaces.Service
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RetornoWebService", Namespace="http://schemas.datacontract.org/2004/07/Notificacao.Facade")]
    public partial class RetornoWebService : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int CodigoField;
        
        private string MensagemField;
        
        private ServiceInterfaces.Service.eStatusRetorno StatusField;
        
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
        public int Codigo
        {
            get
            {
                return this.CodigoField;
            }
            set
            {
                this.CodigoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mensagem
        {
            get
            {
                return this.MensagemField;
            }
            set
            {
                this.MensagemField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceInterfaces.Service.eStatusRetorno Status
        {
            get
            {
                return this.StatusField;
            }
            set
            {
                this.StatusField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="eStatusRetorno", Namespace="http://schemas.datacontract.org/2004/07/Notificacao.Facade")]
    public enum eStatusRetorno : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Erro = -1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sucesso = 1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FaultMessage", Namespace="http://schemas.datacontract.org/2004/07/Notificacao.Facade")]
    public partial class FaultMessage : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string CodigoErroField;
        
        private string MensagemField;
        
        private string OperacaoField;
        
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
        public string CodigoErro
        {
            get
            {
                return this.CodigoErroField;
            }
            set
            {
                this.CodigoErroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mensagem
        {
            get
            {
                return this.MensagemField;
            }
            set
            {
                this.MensagemField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Operacao
        {
            get
            {
                return this.OperacaoField;
            }
            set
            {
                this.OperacaoField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://mbri.com.br/ServicoExternoWS", ConfigurationName="ServiceInterfaces.Service.INotificacao")]
    public interface INotificacao
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://mbri.com.br/ServicoExternoWS/INotificacao/HelloWorld", ReplyAction="http://mbri.com.br/ServicoExternoWS/INotificacao/HelloWorldResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServiceInterfaces.Service.FaultMessage), Action="http://mbri.com.br/ServicoExternoWS/INotificacao/HelloWorldFaultMessageFault", Name="FaultMessage", Namespace="http://schemas.datacontract.org/2004/07/Notificacao.Facade")]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://mbri.com.br/ServicoExternoWS/INotificacao/HelloWorldToken", ReplyAction="http://mbri.com.br/ServicoExternoWS/INotificacao/HelloWorldTokenResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(ServiceInterfaces.Service.FaultMessage), Action="http://mbri.com.br/ServicoExternoWS/INotificacao/HelloWorldTokenFaultMessageFault" +
            "", Name="FaultMessage", Namespace="http://schemas.datacontract.org/2004/07/Notificacao.Facade")]
        ServiceInterfaces.Service.RetornoWebService HelloWorldToken(out string mensagem, string hash);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INotificacaoChannel : ServiceInterfaces.Service.INotificacao, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NotificacaoClient : System.ServiceModel.ClientBase<ServiceInterfaces.Service.INotificacao>, ServiceInterfaces.Service.INotificacao
    {
        
        public NotificacaoClient()
        {
        }
        
        public NotificacaoClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public NotificacaoClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public NotificacaoClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public NotificacaoClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public string HelloWorld()
        {
            return base.Channel.HelloWorld();
        }
        
        public ServiceInterfaces.Service.RetornoWebService HelloWorldToken(out string mensagem, string hash)
        {
            return base.Channel.HelloWorldToken(out mensagem, hash);
        }
    }
}