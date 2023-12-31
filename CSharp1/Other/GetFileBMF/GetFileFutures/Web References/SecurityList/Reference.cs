﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace GetFileFutures.SecurityList {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="OperacoesSoap", Namespace="http://www.agorainvest.com.br/WebServices")]
    public partial class Operacoes : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RetornaOperacoesOperationCompleted;
        
        private System.Threading.SendOrPostCallback SelecionaOrdensOperationCompleted;
        
        private System.Threading.SendOrPostCallback RetornarSecurityIDOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Operacoes() {
            this.Url = global::GetFileFutures.Properties.Settings.Default.GetFileFutures_SecurityList_Operacoes;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event RetornaOperacoesCompletedEventHandler RetornaOperacoesCompleted;
        
        /// <remarks/>
        public event SelecionaOrdensCompletedEventHandler SelecionaOrdensCompleted;
        
        /// <remarks/>
        public event RetornarSecurityIDCompletedEventHandler RetornarSecurityIDCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.agorainvest.com.br/WebServices/RetornaOperacoes", RequestNamespace="http://www.agorainvest.com.br/WebServices", ResponseNamespace="http://www.agorainvest.com.br/WebServices", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RetornaOperacoes(string User, string Password, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] HashCode) {
            object[] results = this.Invoke("RetornaOperacoes", new object[] {
                        User,
                        Password,
                        HashCode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RetornaOperacoesAsync(string User, string Password, byte[] HashCode) {
            this.RetornaOperacoesAsync(User, Password, HashCode, null);
        }
        
        /// <remarks/>
        public void RetornaOperacoesAsync(string User, string Password, byte[] HashCode, object userState) {
            if ((this.RetornaOperacoesOperationCompleted == null)) {
                this.RetornaOperacoesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRetornaOperacoesOperationCompleted);
            }
            this.InvokeAsync("RetornaOperacoes", new object[] {
                        User,
                        Password,
                        HashCode}, this.RetornaOperacoesOperationCompleted, userState);
        }
        
        private void OnRetornaOperacoesOperationCompleted(object arg) {
            if ((this.RetornaOperacoesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RetornaOperacoesCompleted(this, new RetornaOperacoesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.agorainvest.com.br/WebServices/SelecionaOrdens", RequestNamespace="http://www.agorainvest.com.br/WebServices", ResponseNamespace="http://www.agorainvest.com.br/WebServices", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SelecionaOrdens(string User, string Password, string StrTipo, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] HashCode, int intCliCD, string Seqord) {
            object[] results = this.Invoke("SelecionaOrdens", new object[] {
                        User,
                        Password,
                        StrTipo,
                        HashCode,
                        intCliCD,
                        Seqord});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SelecionaOrdensAsync(string User, string Password, string StrTipo, byte[] HashCode, int intCliCD, string Seqord) {
            this.SelecionaOrdensAsync(User, Password, StrTipo, HashCode, intCliCD, Seqord, null);
        }
        
        /// <remarks/>
        public void SelecionaOrdensAsync(string User, string Password, string StrTipo, byte[] HashCode, int intCliCD, string Seqord, object userState) {
            if ((this.SelecionaOrdensOperationCompleted == null)) {
                this.SelecionaOrdensOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSelecionaOrdensOperationCompleted);
            }
            this.InvokeAsync("SelecionaOrdens", new object[] {
                        User,
                        Password,
                        StrTipo,
                        HashCode,
                        intCliCD,
                        Seqord}, this.SelecionaOrdensOperationCompleted, userState);
        }
        
        private void OnSelecionaOrdensOperationCompleted(object arg) {
            if ((this.SelecionaOrdensCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SelecionaOrdensCompleted(this, new SelecionaOrdensCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://www.agorainvest.com.br/WebServices/RetornarSecurityID", RequestNamespace="http://www.agorainvest.com.br/WebServices", ResponseNamespace="http://www.agorainvest.com.br/WebServices", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public SecurityInfoTO RetornarSecurityID(string User, string Password, string symbol) {
            object[] results = this.Invoke("RetornarSecurityID", new object[] {
                        User,
                        Password,
                        symbol});
            return ((SecurityInfoTO)(results[0]));
        }
        
        /// <remarks/>
        public void RetornarSecurityIDAsync(string User, string Password, string symbol) {
            this.RetornarSecurityIDAsync(User, Password, symbol, null);
        }
        
        /// <remarks/>
        public void RetornarSecurityIDAsync(string User, string Password, string symbol, object userState) {
            if ((this.RetornarSecurityIDOperationCompleted == null)) {
                this.RetornarSecurityIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRetornarSecurityIDOperationCompleted);
            }
            this.InvokeAsync("RetornarSecurityID", new object[] {
                        User,
                        Password,
                        symbol}, this.RetornarSecurityIDOperationCompleted, userState);
        }
        
        private void OnRetornarSecurityIDOperationCompleted(object arg) {
            if ((this.RetornarSecurityIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RetornarSecurityIDCompleted(this, new RetornarSecurityIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.agorainvest.com.br/WebServices")]
    public partial class SecurityInfoTO {
        
        private string symbolField;
        
        private string securityGroupField;
        
        private string securityTypeField;
        
        private string productField;
        
        private string assetField;
        
        private int roundLotField;
        
        private string priceTypeField;
        
        private System.DateTime maturityDateField;
        
        private double strikePriceField;
        
        private double contractMultiplierField;
        
        private double minPriceIncrementField;
        
        private int tickSizeDenominatorField;
        
        private System.DateTime issueDateField;
        
        private string tradingSessionSubIDField;
        
        private System.DateTime tradingSessionOpenTimeField;
        
        private string tradingStatusField;
        
        private System.DateTime lastTradeField;
        
        private double previousCloseField;
        
        private System.DateTime timeStampField;
        
        private string securityDescField;
        
        private string securityIdField;
        
        /// <remarks/>
        public string Symbol {
            get {
                return this.symbolField;
            }
            set {
                this.symbolField = value;
            }
        }
        
        /// <remarks/>
        public string SecurityGroup {
            get {
                return this.securityGroupField;
            }
            set {
                this.securityGroupField = value;
            }
        }
        
        /// <remarks/>
        public string SecurityType {
            get {
                return this.securityTypeField;
            }
            set {
                this.securityTypeField = value;
            }
        }
        
        /// <remarks/>
        public string Product {
            get {
                return this.productField;
            }
            set {
                this.productField = value;
            }
        }
        
        /// <remarks/>
        public string Asset {
            get {
                return this.assetField;
            }
            set {
                this.assetField = value;
            }
        }
        
        /// <remarks/>
        public int RoundLot {
            get {
                return this.roundLotField;
            }
            set {
                this.roundLotField = value;
            }
        }
        
        /// <remarks/>
        public string PriceType {
            get {
                return this.priceTypeField;
            }
            set {
                this.priceTypeField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime MaturityDate {
            get {
                return this.maturityDateField;
            }
            set {
                this.maturityDateField = value;
            }
        }
        
        /// <remarks/>
        public double StrikePrice {
            get {
                return this.strikePriceField;
            }
            set {
                this.strikePriceField = value;
            }
        }
        
        /// <remarks/>
        public double ContractMultiplier {
            get {
                return this.contractMultiplierField;
            }
            set {
                this.contractMultiplierField = value;
            }
        }
        
        /// <remarks/>
        public double MinPriceIncrement {
            get {
                return this.minPriceIncrementField;
            }
            set {
                this.minPriceIncrementField = value;
            }
        }
        
        /// <remarks/>
        public int TickSizeDenominator {
            get {
                return this.tickSizeDenominatorField;
            }
            set {
                this.tickSizeDenominatorField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime IssueDate {
            get {
                return this.issueDateField;
            }
            set {
                this.issueDateField = value;
            }
        }
        
        /// <remarks/>
        public string TradingSessionSubID {
            get {
                return this.tradingSessionSubIDField;
            }
            set {
                this.tradingSessionSubIDField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime TradingSessionOpenTime {
            get {
                return this.tradingSessionOpenTimeField;
            }
            set {
                this.tradingSessionOpenTimeField = value;
            }
        }
        
        /// <remarks/>
        public string TradingStatus {
            get {
                return this.tradingStatusField;
            }
            set {
                this.tradingStatusField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime LastTrade {
            get {
                return this.lastTradeField;
            }
            set {
                this.lastTradeField = value;
            }
        }
        
        /// <remarks/>
        public double PreviousClose {
            get {
                return this.previousCloseField;
            }
            set {
                this.previousCloseField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime TimeStamp {
            get {
                return this.timeStampField;
            }
            set {
                this.timeStampField = value;
            }
        }
        
        /// <remarks/>
        public string SecurityDesc {
            get {
                return this.securityDescField;
            }
            set {
                this.securityDescField = value;
            }
        }
        
        /// <remarks/>
        public string SecurityId {
            get {
                return this.securityIdField;
            }
            set {
                this.securityIdField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    public delegate void RetornaOperacoesCompletedEventHandler(object sender, RetornaOperacoesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RetornaOperacoesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RetornaOperacoesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    public delegate void SelecionaOrdensCompletedEventHandler(object sender, SelecionaOrdensCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SelecionaOrdensCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SelecionaOrdensCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    public delegate void RetornarSecurityIDCompletedEventHandler(object sender, RetornarSecurityIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.79.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RetornarSecurityIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RetornarSecurityIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public SecurityInfoTO Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SecurityInfoTO)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591