using System;  
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NestDLL;

using Subscription = Bloomberglp.Blpapi.Subscription;
using CorrelationID = Bloomberglp.Blpapi.CorrelationID;
using Message = Bloomberglp.Blpapi.Message;

namespace Feed_Bloomberg.Engine
{
    abstract class Feeder
    {
        
        public struct ReqData
        {
            public string ticker;
            public int idInstrument;
            public int idTicker;
            public int priceType;
            public bool DataReceived;
        }

        BLP Bloomberg;

        List<Subscription> subscriptionList;
        string fields;

        System.Windows.Forms.Timer timer;
        public int ProgramID;

        Form1 form;

        public DateTime lastUpdate;
        

        //Armazena as solicitações de tickers a serem realizadas
        public Dictionary<CorrelationID, ReqData> Requests;

        //Método abstrato para determinar consulta de tickers
        public abstract string QueryTickers();

        //Método abstrato para determinar os campos para consulta Bloomberg
        public abstract string SubscriptionFields();

        //Método abstrato para processar resposta Bloomberg
        public abstract void ProcessMessage(Message message);

        public Feeder(int _ProgramID)
        {
            this.subscriptionList = new List<Subscription>();
            this.fields = this.SubscriptionFields();
            this.ProgramID = _ProgramID;
        }
        
        //Inicia feeder Bloomberg
        public void Start()
        {
            Requests = new Dictionary<CorrelationID, ReqData>();

            this.LoadTickers();

            Bloomberg = new BLP(this);

            this.Subscribe();
        }


        //Busca os tickers a serem consultados na Bloomberg
        public void LoadTickers()
        {


            using (newNestConn Conn = new newNestConn())
            {
                SqlDataReader tickers;
                int reqID = 0;
                
                //Consulta a base de dados para obter os tickers a serem consultados.
                tickers = Conn.Return_DataReader(this.QueryTickers());

                //Armazena o tickers em um dicionário para posterior subscrição na Bloomberg
                while (tickers.Read())
                {
                    if (tickers.GetString(tickers.GetOrdinal("Simbolo")) != "")
                    {
                        ReqData req;

                        req.ticker = tickers.GetString(tickers.GetOrdinal("Simbolo"));
                        req.idInstrument = tickers.GetInt32(tickers.GetOrdinal("id_Instrumento"));
                        req.idTicker = tickers.GetInt32(tickers.GetOrdinal("id_Ativo"));
                        req.priceType = tickers.GetInt32(tickers.GetOrdinal("Tipo_Preco"));
                        req.DataReceived = false;

                        reqID++;

                        CorrelationID corrID = new CorrelationID(reqID);

                        this.Requests.Add(corrID, req);
                    }

                }
            }
        }

        public void Subscribe()
        {

            Log.Log_Event(this.ProgramID,103,1, "BBG Feeder started quote request");
            
            foreach (KeyValuePair<CorrelationID, ReqData> req in Requests)
            {
                subscriptionList.Add(new Subscription(req.Value.ticker, fields, req.Key));
            }

            Bloomberg.Subscribe(subscriptionList);

            Log.Log_Event(this.ProgramID,103,3, Requests.Count.ToString() + " quotes requested");

        }

        public void ReceiveMessage(Message message)
        {
            this.lastUpdate = DateTime.Now;
            this.ProcessMessage(message);
        }

        public void Stop()
        {
            Bloomberg.Stop(subscriptionList);
        }

        public void StartCheckInLog(int _ProgramID)
        {
            this.ProgramID = _ProgramID;

            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = Feed_Bloomberg.Properties.Settings.Default.CheckInTimer;
            this.timer.Tick += new EventHandler(timer_Tick);
            this.timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Log.Log_CheckIn(this.ProgramID);
        }

        public void StopCheckInLog()
        {
            this.timer.Stop();
        }

        
    }
}
