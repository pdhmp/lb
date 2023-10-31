using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;

namespace GetFileFutures
{
    public class GetDataFromWS
    {
        //C�digo Cliente CBLC // Senha no Portal �gora

        public string GetSecurity(string CdCliente, string Password, string Symbol)
        {
            SecurityList.Operacoes SecList = new SecurityList.Operacoes();
            SecurityList.SecurityInfoTO Retorno;

            Retorno = SecList.RetornarSecurityID(CdCliente, Password, Symbol);

            if(Retorno == null){
                return "";
            } else
            {
                return Retorno.SecurityId.ToString();
            };

        }

    }

}
