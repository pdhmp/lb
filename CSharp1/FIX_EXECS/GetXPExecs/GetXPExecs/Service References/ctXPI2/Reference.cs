﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GetXPExecs.ctXPI2 {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ctXPI2.ctServicoExternoSoap")]
    public interface ctServicoExternoSoap {
        
        // CODEGEN: Generating message contract since message XPSyncNegociosConsultarRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/XPSyncNegociosConsultar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        GetXPExecs.ctXPI2.XPSyncNegociosConsultarResponse XPSyncNegociosConsultar(GetXPExecs.ctXPI2.XPSyncNegociosConsultarRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AutenticarBroker", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        GetXPExecs.ctXPI2.XPSyncBrokerSOAPHeader AutenticarBroker(string login, string password);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.233")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class XPSyncBrokerSOAPHeader : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string brokerTokenField;
        
        private string brokerLoginField;
        
        private string brokerPasswordField;
        
        private string[] avaliableBrokersField;
        
        private string[] avaliableClientsField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string brokerToken {
            get {
                return this.brokerTokenField;
            }
            set {
                this.brokerTokenField = value;
                this.RaisePropertyChanged("brokerToken");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string brokerLogin {
            get {
                return this.brokerLoginField;
            }
            set {
                this.brokerLoginField = value;
                this.RaisePropertyChanged("brokerLogin");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string brokerPassword {
            get {
                return this.brokerPasswordField;
            }
            set {
                this.brokerPasswordField = value;
                this.RaisePropertyChanged("brokerPassword");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=3)]
        public string[] avaliableBrokers {
            get {
                return this.avaliableBrokersField;
            }
            set {
                this.avaliableBrokersField = value;
                this.RaisePropertyChanged("avaliableBrokers");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Order=4)]
        public string[] avaliableClients {
            get {
                return this.avaliableClientsField;
            }
            set {
                this.avaliableClientsField = value;
                this.RaisePropertyChanged("avaliableClients");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="XPSyncNegociosConsultar", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class XPSyncNegociosConsultarRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public GetXPExecs.ctXPI2.XPSyncBrokerSOAPHeader XPSyncBrokerSOAPHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string opcao;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string datainicial;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string datafinal;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public string nrbroker;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=4)]
        public string nrcliente;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=5)]
        public string papel;
        
        public XPSyncNegociosConsultarRequest() {
        }
        
        public XPSyncNegociosConsultarRequest(GetXPExecs.ctXPI2.XPSyncBrokerSOAPHeader XPSyncBrokerSOAPHeader, string opcao, string datainicial, string datafinal, string nrbroker, string nrcliente, string papel) {
            this.XPSyncBrokerSOAPHeader = XPSyncBrokerSOAPHeader;
            this.opcao = opcao;
            this.datainicial = datainicial;
            this.datafinal = datafinal;
            this.nrbroker = nrbroker;
            this.nrcliente = nrcliente;
            this.papel = papel;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="XPSyncNegociosConsultarResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class XPSyncNegociosConsultarResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Data.DataTable XPSyncNegociosConsultarResult;
        
        public XPSyncNegociosConsultarResponse() {
        }
        
        public XPSyncNegociosConsultarResponse(System.Data.DataTable XPSyncNegociosConsultarResult) {
            this.XPSyncNegociosConsultarResult = XPSyncNegociosConsultarResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ctServicoExternoSoapChannel : GetXPExecs.ctXPI2.ctServicoExternoSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ctServicoExternoSoapClient : System.ServiceModel.ClientBase<GetXPExecs.ctXPI2.ctServicoExternoSoap>, GetXPExecs.ctXPI2.ctServicoExternoSoap {
        
        public ctServicoExternoSoapClient() {
        }
        
        public ctServicoExternoSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ctServicoExternoSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ctServicoExternoSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ctServicoExternoSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        GetXPExecs.ctXPI2.XPSyncNegociosConsultarResponse GetXPExecs.ctXPI2.ctServicoExternoSoap.XPSyncNegociosConsultar(GetXPExecs.ctXPI2.XPSyncNegociosConsultarRequest request) {
            return base.Channel.XPSyncNegociosConsultar(request);
        }
        
        public System.Data.DataTable XPSyncNegociosConsultar(GetXPExecs.ctXPI2.XPSyncBrokerSOAPHeader XPSyncBrokerSOAPHeader, string opcao, string datainicial, string datafinal, string nrbroker, string nrcliente, string papel) {
            GetXPExecs.ctXPI2.XPSyncNegociosConsultarRequest inValue = new GetXPExecs.ctXPI2.XPSyncNegociosConsultarRequest();
            inValue.XPSyncBrokerSOAPHeader = XPSyncBrokerSOAPHeader;
            inValue.opcao = opcao;
            inValue.datainicial = datainicial;
            inValue.datafinal = datafinal;
            inValue.nrbroker = nrbroker;
            inValue.nrcliente = nrcliente;
            inValue.papel = papel;
            GetXPExecs.ctXPI2.XPSyncNegociosConsultarResponse retVal = ((GetXPExecs.ctXPI2.ctServicoExternoSoap)(this)).XPSyncNegociosConsultar(inValue);
            return retVal.XPSyncNegociosConsultarResult;
        }
        
        public GetXPExecs.ctXPI2.XPSyncBrokerSOAPHeader AutenticarBroker(string login, string password) {
            return base.Channel.AutenticarBroker(login, password);
        }
    }
}
