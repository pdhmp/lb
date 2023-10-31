using System;  
using System.Collections.Generic;
using System.Text;
using NestDLL;

using Message = Bloomberglp.Blpapi.Message;
namespace Feed_Bloomberg.Engine
{
    class Backup_Reuters: Feeder
    {
        public Backup_Reuters()
            : base(3)
        {
        }
        
        public override string QueryTickers()
        {
            string sql;

            sql = "Select A.idsecurity as id_Ativo, BloombergTicker as Simbolo, idinstrument as id_instrumento,1 as Tipo_Preco "+
                  "from  NESTDB.dbo.tb001_Securities A" +
                  " where RTUpdate=1 AND RTUpdateSource=5 AND BloombergTicker is not null and BloombergTicker <> ''";

            return sql;
        }

        public override string SubscriptionFields()
        {
            string fields;

            fields = "TIME, LAST_PRICE";

            return fields;
        }

        public override void ProcessMessage(Message message)
        {
            ReqData response;

            if (this.Requests.TryGetValue(message.CorrelationID,out response))
            {
                string Value = ""; 
                
                string idTicker = response.idTicker.ToString();
                string priceType = response.priceType.ToString();

                try
                {

                    bool error = false;

                    try
                    {
                        Value = message.GetElement("LAST_PRICE").GetValueAsString();
                    }
                    catch
                    {
                        error = true;
                    }
                    if (!error)
                    {
                        double dValue;
                     
                        switch (response.idInstrument)
                        {
                            case 13: //Treasury Bills
                                if (response.priceType == 1) //LAST_PRICE
                                {
                                    //Converte para taxa
                                    double.TryParse(Value, out dValue);
                                    dValue = dValue / 100;
                                    Value = dValue.ToString();

                                    //Substitui o tipo de preço para RATE LAST
                                    priceType = "30";
                                }
                                break;
                            default:
                                break;
                        }

                        Value = Value.Replace(",", ".");

                        insertPrice(idTicker, Value, priceType,
                                    DateTime.Now.ToString("yyyyMMdd HH:mm:ss"));
                    }
                }
                catch (Exception ex)
                {
                    Log.AddLogEntry("LAST_PRICE not found for ticker: "+ message.TopicName);
                }
            }
            else
            {
                Log.AddLogEntry("Ticker " + message.TopicName + " received without being requested");
            }
        }

        public void insertPrice(string idTicker, string Value, string PriceType, string Source_TimeStamp)
        {
            string SQL;

            SQL = "INSERT INTO [NESTRT].[dbo].[Tb065_Ultimo_Preco]" +
                  " ([IdSecurity],[SrValue],[SrType],[SourceTimeStamp],Source,Automated)" +
                  " VALUES (" + idTicker + "," + Value + ", " + PriceType + ",'" + Source_TimeStamp + "',1,1)";

            using (newNestConn Conn = new newNestConn())
            {
                Conn.ExecuteNonQuery(SQL);
            }
        }
    }
}
