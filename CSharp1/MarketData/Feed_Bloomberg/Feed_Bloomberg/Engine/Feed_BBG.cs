using System;  
using System.Collections.Generic;
using System.Text;
using NestDLL;
using Message = Bloomberglp.Blpapi.Message;
namespace Feed_Bloomberg.Engine
{
    class Feed_BBG: Feeder
    {
        public Feed_BBG()
            : base(3)
        {
        }

        public override string QueryTickers()
        {
            string sql;

            sql = " SELECT A.idsecurity as id_Ativo, BloombergTicker as Simbolo, idinstrument as id_instrumento,1 as Tipo_Preco " +
                  " FROM  NESTDB.dbo.tb001_Securities A" +
                  " WHERE RTUpdate=1 AND RTUpdateSource=1 and a.idsecurity >0";

            return sql;
        }

        public override string SubscriptionFields()
        {
            string fields;

            fields = "TIME, LAST_PRICE,PX_CLOSE";

            return fields;
        }

        public override void ProcessMessage(Message message)
        {           
            bool error = false;
            string Value = "";
            try
            {
                Value = message.GetElement("LAST_PRICE").GetValueAsString();
                Value = message.GetElement("PX_CLOSE").GetValueAsString();
            }
            catch
            {
                error = true;
            }

            if (!error)
            {
                ReqData response;

                if (this.Requests.TryGetValue(message.CorrelationID, out response))
                {
                    string idTicker = response.idTicker.ToString();
                    string priceType = response.priceType.ToString();

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
                else
                {
                    Log.AddLogEntry("Ticker " + message.TopicName + " received without being requested");
                }
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
