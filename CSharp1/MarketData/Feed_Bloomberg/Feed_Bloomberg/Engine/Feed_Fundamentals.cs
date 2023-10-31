using System;  
using System.Collections.Generic;
using System.Text;
using NestDLL;
using Message = Bloomberglp.Blpapi.Message;
namespace Feed_Bloomberg.Engine
{
    class Feed_Fundamentals: Feeder
    {
        int RecCounter = 0;
        int ReqCounter = 0;
        System.Windows.Forms.Timer CheckTimer;


        public Feed_Fundamentals()
            : base(3)
        {
            this.CheckTimer = new System.Windows.Forms.Timer();
            this.CheckTimer.Interval = 1;
            this.CheckTimer.Tick += new EventHandler(Checktimer_Tick);
            this.CheckTimer.Start();
        }

        void Checktimer_Tick(object sender, EventArgs e)
        {
            if (RecCounter > ReqCounter)
            {
                bool AllDataReceived = true;
                foreach (ReqData curReqData in this.Requests.Values)
                {
                    if (curReqData.DataReceived == false) AllDataReceived = false; break;
                }
                if (AllDataReceived)
                {
                    this.Stop();
                }
            }
        }

        public override string QueryTickers()
        {
            string sql = " SELECT A.idsecurity as id_Ativo, BloombergTicker as Simbolo, idinstrument as id_instrumento,1 as Tipo_Preco " +
                  " FROM  NESTDB.dbo.tb001_Securities A" +
                  " WHERE IdCurrency=900 AND IdInstrument=2 AND BloombergTicker<>''";

            return sql;
        }

        public override string SubscriptionFields()
        {
            string fields;

            fields = "VOLUME_AVG_3M";//, VOLUME_AVG_6M, CUR_MKT_CAP, DUR_ADJ_MID, LATEST_PERIOD_END_DT_FULL_RECORD, ANNOUNCEMENT_DT, TRAIL_12M_EPS_BEF_XO";

            //fields = "LAST_PRICE"; 
            return fields;
        }

        public override void ProcessMessage(Message message)
        {
            ReqData response;

            if (this.Requests.TryGetValue(message.CorrelationID, out response))
            {
                bool error = false;
                string Value = "";

                foreach (Bloomberglp.Blpapi.Element curElement in message.Elements)
                {
                    response.DataReceived = true;
                    RecCounter++;

                    //int PriceType = 0;
                    try
                    {
                        Value = curElement.GetValueAsString();
                    }
                    catch
                    {
                        error = true;
                    }

                    if (!error)
                    {
                        string idTicker = response.idTicker.ToString();
                        string priceType;

                        Value = Value.Replace(",", ".");

                        switch (curElement.Name.ToString())
                        {
                            case "VOLUME_AVG_3M": priceType = "18"; insertPrice(idTicker, Value, priceType, DateTime.Now.ToString("yyyyMMdd HH:mm:ss")); break;
                            case "VOLUME_AVG_6M": priceType = "19"; insertPrice(idTicker, Value, priceType, DateTime.Now.ToString("yyyyMMdd HH:mm:ss")); break;
                            case "CUR_MKT_CAP": priceType = "20"; insertPrice(idTicker, Value, priceType, DateTime.Now.ToString("yyyyMMdd HH:mm:ss")); break;
                            case "DUR_ADJ_MID": priceType = "91"; insertPrice(idTicker, Value, priceType, DateTime.Now.ToString("yyyyMMdd HH:mm:ss")); break;
                            case "TRAIL_12M_EPS_BEF_XO": priceType = "501"; insertPriceDTA(idTicker, Value, priceType, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), DateTime.Now.ToString("yyyyMMdd HH:mm:ss")); break;
                        }
                    }
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
                  " ([Id_Ativo],[Valor],[Tipo_Preco],[Source_TimeStamp],Source,Automated)" +
                  " VALUES (" + idTicker + "," + Value + ", " + PriceType + ",'" + Source_TimeStamp + "',1,1)";

            using (newNestConn Conn = new newNestConn())
            {
                Conn.ExecuteNonQuery(SQL);
            }
        }

        public void insertPriceDTA(string IdSecurity, string Value, string PriceType, string InsertDate, string RefDate, string KnownDate)
        {
            string SQL;

            SQL = "INSERT INTO [NESTDB].[dbo].[Tb050_DataSeries_PTA]" +
                  " ([IdSecurity],[Value],[PriceType],[InsertDate],Source,KnownDate,RefDate)" +
                  " VALUES (" + IdSecurity + "," + Value + ", " + PriceType + ",'" + InsertDate + "',1,'" + InsertDate + "','" + RefDate + "')";

            using (newNestConn Conn = new newNestConn())
            {
                Conn.ExecuteNonQuery(SQL);
            }
        }
    }
}
