using System;
using System.Data;
using System.Collections.Generic;
using NestDLL;
using NestQuant.Common;


namespace JMStrategy
{
    public class JMPairs
    {        
        public JMSecurities Security1;
        public JMSecurities Security2;

        public List<JM2ColItem> Sec1Items = new List<JM2ColItem>();
        public List<JM2ColItem> Sec2Items = new List<JM2ColItem>();

        public List<Double> Sec1Last = new List<Double>();
        public List<Double> Sec2Last = new List<Double>();

        public string Ticker1 { get { return Security1.Ticker; } }
        public string Ticker2 { get { return Security2.Ticker; } }

        public double Ticker1Last { get { return Security1.Last; } }
        public double Ticker2Last { get { return Security2.Last; } }

        //public double Ratio { get { return RatioArray[0]; } }
        //public double LNRatio { get { return LNRatioArray[0]; } }

        private int _IdPair;
        public int IdPair
        {
            get { return _IdPair; }
            set { _IdPair = value; }
        }        

        //Ratio
        private double _RatioVol;
        private double _RatioHistVol;

        public double RatioVol
        {
            get { return _RatioVol; }
        }        
        public double RatioHistVol
        {
            get { return _RatioHistVol; }
        }

        private int HistDays = 100;

        //NewPositions
        private double _NPFinancial;
        private int _Security1NewPosition;
        private int _Security2NewPosition;
        
        public int Security1NewPosition { get { return _Security1NewPosition; } }
        public int Security2NewPosition { get { return _Security2NewPosition; } }
        public double NPFinancial
        {
            get { return _NPFinancial; }
        }

        //Stochastic
        private double _StochasticDiff = 0;

        public double StochasticDiff
        {
            get { return _StochasticDiff; }
        }

        //Last Position
        private int _Sec1PrevPos = 0;
        private int _Sec2PrevPos = 0;

        public int Sec1PrevPos
        {
            get { return _Sec1PrevPos; }
            set { _Sec1PrevPos = value; }
        }
        public int Sec2PrevPos
        {
            get { return _Sec2PrevPos; }
            set { _Sec2PrevPos = value; }
        }
        public string PreviousPosition
        {
            get
            {
                string prevPos = "";
                if (_Sec1PrevPos == 0) { prevPos = "Zero"; }
                else if (_Sec1PrevPos > 0) {prevPos = "Bought";}
                else {prevPos = "Sold";}

                return prevPos;
            }
        }

        //Current Position
        private int _Sec1CurPos;
        private int _Sec2CurPos;

        public int Sec1CurPos
        {
            get { return _Sec1CurPos; }
            set { _Sec1CurPos = value; }
        }
        public int Sec2CurPos
        {
            get { return _Sec2CurPos; }
            set { _Sec2CurPos = value; }
        }
        public string CurrentPosition
        {
            get
            {
                string curPos = "";
                if (_Sec1CurPos == 0) { curPos = "Zero"; }
                else if (_Sec1CurPos > 0) { curPos = "Bought"; }
                else { curPos = "Sold"; }

                return curPos;
            }
        }

        //Action
        public int Sec1Action { get { return _Sec1CurPos - _Sec1PrevPos - Sec1Sent - Sec1Exec; } }
        public int Sec2Action { get { return _Sec2CurPos - _Sec2PrevPos - Sec2Sent - Sec2Exec; } }
        private string _Action;
        public string Action { get { return _Action; } }

        private int Sec1Sent = 0;
        private int Sec2Sent = 0;

        private int Sec1Exec = 0;
        private int Sec2Exec = 0;


        private double[] RatioArray; 
        private double[] LNRatioArray;
        private volatile object ArraySync = new object();

        private bool ArraysInitialized = false;

        private bool _PairBlocked = false;

        public bool PairBlocked
        {
            get { return _PairBlocked; }
            set { _PairBlocked= value; }
        }


        public event EventHandler OnAction;
        public event EventHandler OnClosedSec;

        public volatile object SyncOrders = new object();

        public JMModel ParentModel;

        public JMPairs(JMSecurities Sec1, JMSecurities Sec2, int __IdPair, JMModel _ParentModel)
        {
            Security1 = Sec1;
            Security2 = Sec2;
            _IdPair = __IdPair;

            ParentModel = _ParentModel;

            RatioArray = new double[JMUtils.MaxDays];
            LNRatioArray = new double[JMUtils.MaxDays];
            
            Security1.SecurityUpdated += new EventHandler(UpdatePair);
            Security2.SecurityUpdated += new EventHandler(UpdatePair);

            LoadCurrentPosition();
        }

        private void LoadCurrentPosition()
        {
            string SQLExpression = "";
            string execExpression = "";

            if (ParentModel.DateRef == DateTime.Today)
            {
                SQLExpression = " SELECT TOP(1) * FROM NESTSIM.DBO.TB702_JM_POSITIONS (NOLOCK) " +
                                " WHERE IDMODEL = " + ParentModel.IdModel.ToString() +
                                " AND IDPAIR = " + _IdPair.ToString() +
                                " AND DATE < '" + ParentModel.DateRef.ToString("yyyyMMdd") + "'" +
                                " ORDER BY DATE DESC";


                execExpression = " SELECT * FROM NESTSIM.DBO.TB702_JM_POSITIONS (NOLOCK) " +
                                 " WHERE IDMODEL = " + ParentModel.IdModel.ToString() +
                                 " AND IDPAIR = " + _IdPair.ToString() +
                                 " AND DATE = '" + ParentModel.DateRef.ToString("yyyyMMdd") + "'" +
                                 " ORDER BY DATE DESC";
            }
            else
            {
                SQLExpression = " SELECT TOP(1) * FROM NESTSIM.DBO.TB703_JM_BT_POSITIONS (NOLOCK) " +
                                " WHERE IDMODEL = " + ParentModel.IdModel.ToString() +
                                " AND IDPAIR = " + _IdPair.ToString() +
                                " AND DATE < '" + ParentModel.DateRef.ToString("yyyyMMdd") + "'" +
                                " ORDER BY DATE DESC";

                execExpression = " SELECT * FROM NESTSIM.DBO.TB703_JM_BT_POSITIONS (NOLOCK) " +
                                 " WHERE IDMODEL = " + ParentModel.IdModel.ToString() +
                                 " AND IDPAIR = " + _IdPair.ToString() +
                                 " AND DATE = '" + ParentModel.DateRef.ToString("yyyyMMdd") + "'" +
                                 " ORDER BY DATE DESC";
            }

            if (ParentModel.DateRef == DateTime.Today)
            {
                using (newNestConn curConn = new newNestConn(true))
                {
                    DataTable result = curConn.Return_DataTable(SQLExpression);

                    if (result.Rows.Count > 0)
                    {
                        _Sec1PrevPos = int.Parse(result.Rows[0]["SEC1POSITION"].ToString());
                        _Sec2PrevPos = int.Parse(result.Rows[0]["SEC2POSITION"].ToString());
                    }

                    DataTable exec = curConn.Return_DataTable(execExpression);

                    if (exec.Rows.Count > 0)
                    {
                        int auxSec1Exec = int.Parse(exec.Rows[0]["SEC1POSITION"].ToString());
                        int auxSec2Exec = int.Parse(exec.Rows[0]["SEC2POSITION"].ToString());

                        Sec1Exec = auxSec1Exec - _Sec1PrevPos;
                        Sec2Exec = auxSec2Exec - _Sec2PrevPos;
                    }
                    else
                    {
                        string insertString = "";

                        if (ParentModel.DateRef == DateTime.Today)
                        {
                            insertString = "INSERT INTO NESTSIM.DBO.TB702_JM_POSITIONS" +
                                           "(DATE,IDMODEL,IDPAIR,SEC1POSITION,SEC2POSITION) VALUES " +
                                           "('" + ParentModel.DateRef.ToString("yyyyMMdd") + "'," + ParentModel.IdModel + "," + IdPair + "," + _Sec1PrevPos.ToString() + "," + _Sec2PrevPos.ToString() + ")";
                        }
                        else
                        {
                            insertString = "INSERT INTO NESTSIM.DBO.TB703_JM_BT_POSITIONS" +
                                           "(DATE,IDMODEL,IDPAIR,SEC1POSITION,SEC2POSITION) VALUES " +
                                           "('" + ParentModel.DateRef.ToString("yyyyMMdd") + "'," + ParentModel.IdModel + "," + IdPair + "," + _Sec1PrevPos.ToString() + "," + _Sec2PrevPos.ToString() + ")";
                        }

                        curConn.ExecuteNonQuery(insertString);
                    }
                }
            }
            else
            {
                _Sec1PrevPos = 0;
                _Sec2PrevPos = 0;

                if (JMData.Instance.Sec1BTPos.ContainsKey(ParentModel.IdModel))
                {
                    if (JMData.Instance.Sec1BTPos[ParentModel.IdModel].ContainsKey(IdPair))
                    {
                        _Sec1PrevPos = JMData.Instance.Sec1BTPos[ParentModel.IdModel][IdPair];
                        _Sec2PrevPos = JMData.Instance.Sec2BTPos[ParentModel.IdModel][IdPair];
                    }
                }                
            }
        }

        private void UpdatePair(object sender, EventArgs e)
        {
            if (Security1.SecurityInitialized && Security2.SecurityInitialized)
            {
                lock (ArraySync)
                {
                    lock (JMData.Instance.MainSync)
                    {
                        CalculateRatioVol();

                        CalculateNewPositions();

                        CalculateAction();

                        UpdateSecItems();
                    }

                    UpdateAction();
                }
            }
        }

        private void InitializeArrays()
        {
            for (int i = 0; i < JMUtils.MaxDays; i++)
            {
                RatioArray[i] = Security1.LastPrice[i] / Security2.LastPrice[i];
            }

            for (int i = 0; i < JMUtils.MaxDays - 1; i++)
            {
                LNRatioArray[i] = Math.Log(RatioArray[i] / RatioArray[i + 1]);
            }

            ArraysInitialized = true;
        }

        private void UpdateArrays()
        {
            RatioArray[0] = Security1.LastPrice[0] / Security2.LastPrice[0];
            LNRatioArray[0] = Math.Log(RatioArray[0] / RatioArray[1]);
        }

        private void CalculateRatioVol()
        {
            if (!ArraysInitialized)
            {
                InitializeArrays();
            }
            else
            {
                UpdateArrays();
            }

            double auxRatioVol = NestQuant.Common.Utils.calcStdev(ref LNRatioArray, 0, ParentModel.Days - 1) * Math.Sqrt((double)ParentModel.Days / (double)(ParentModel.Days - 1));
            _RatioHistVol = NestQuant.Common.Utils.calcStdev(ref LNRatioArray, 0, HistDays - 1) * Math.Sqrt((double)HistDays / (double)(HistDays - 1));
            _RatioVol = Math.Min(ParentModel.Limiter * _RatioHistVol, auxRatioVol);
        }

        private void CalculateNewPositions()
        {
            _NPFinancial = ParentModel.Size * _RatioVol / _RatioHistVol;

            double Sec1Finan = 0;
            double Sec2Finan = 0;

            if (Security1.Volatility < Security2.Volatility)
            {
                Sec1Finan = _NPFinancial;
            }
            else
            {
                Sec1Finan = _NPFinancial * Security2.Volatility / Security1.Volatility;
            }

            _Security1NewPosition = (int)(Math.Round(Sec1Finan / Security1.Last / 100.0) * 100);

            Sec2Finan = (-1.0) * Security1.Last * ((double)_Security1NewPosition) * Security1.Volatility / Security2.Volatility;

            _Security2NewPosition = (int)(Math.Round(Sec2Finan / Security2.Last / 100.0) * 100);
        }

        private void CalculateAction()
        {
            _StochasticDiff = Security1.Stochastic - Security2.Stochastic;

            if (_Sec1PrevPos == 0)
            {
                if (_StochasticDiff >= ParentModel.TriggerIN)
                {
                    Sell();
                }
                else
                {
                    if (_StochasticDiff <= (-1F) * ParentModel.TriggerIN)
                    {
                        Buy();
                    }
                    else
                    {
                        NoAction();
                    }
                }
            }
            else
            {
                if (_Sec1PrevPos > 0)
                {
                    if (_StochasticDiff >= ParentModel.TriggerIN)
                    {
                        Sell();
                    }
                    else
                    {
                        if (_StochasticDiff >= (-1F) * ParentModel.TriggerOUT)
                        {
                            Zero();
                        }
                        else
                        {
                            NoAction();
                        }
                    }
                }
                else
                {
                    if (_StochasticDiff <= (-1F) * ParentModel.TriggerIN)
                    {
                        Buy();
                    }
                    else
                    {
                        if (_StochasticDiff <= ParentModel.TriggerOUT)
                        {
                            Zero();
                        }
                        else
                        {
                            NoAction();
                        }
                    }
                }
            }
        }

        private void Sell()
        {
            _Action = "Sell";
            _Sec1CurPos = (-1) * _Security1NewPosition;
            _Sec2CurPos = (-1) * _Security2NewPosition;            
        }
        private void Buy()
        {
            _Action = "Buy";
            _Sec1CurPos = _Security1NewPosition;
            _Sec2CurPos = _Security2NewPosition;            
        }
        private void NoAction()
        {
            _Action = "No Action";
            _Sec1CurPos = _Sec1PrevPos;
            _Sec2CurPos = _Sec2PrevPos;            
        }
        private void Zero()
        {
            _Action = "Zero";
            _Sec1CurPos = 0;
            _Sec2CurPos = 0;            
        }

        private void UpdateAction()
        {
            CheckClosed();

            if (OnAction != null)
            {
                OnAction(this, null);
            }
        }
        private void CheckClosed()
        {
            if (ParentModel.DateRef == DateTime.Today)
            {
                if (Security1.CurData.AucCond == "G_MKTCONTROL")
                {
                    JMClosedSec clSec = new JMClosedSec();                    
                    clSec.ClosedSec = Security1.Ticker;
                    clSec.PairSec = Security2.Ticker;
                    clSec.ClosedQty = Sec1Action;
                    clSec.PairQty = Sec2Action;
                    clSec.Blocked = PairBlocked;
                    clSec.IdModel = ParentModel.IdModel;
                    clSec.IdPair = IdPair;
                    clSec.refPair = this;

                    if (OnClosedSec != null && Sec1Action != 0)
                    {
                        OnClosedSec(this, clSec);
                    }
                }

                else if (Security2.CurData.AucCond == "G_MKTCONTROL")
                {
                    JMClosedSec clSec = new JMClosedSec();
                    clSec.ClosedSec = Security2.Ticker;
                    clSec.PairSec = Security1.Ticker;
                    clSec.ClosedQty = Sec2Action;
                    clSec.PairQty = Sec1Action;
                    clSec.Blocked = PairBlocked;
                    clSec.IdModel = ParentModel.IdModel;
                    clSec.IdPair = IdPair;
                    clSec.refPair = this;

                    if (OnClosedSec != null && Sec2Action != 0)
                    {
                        OnClosedSec(this, clSec);
                    }
                }
            }
        }

        private void UpdateSecItems()
        {
            Sec1Items.Clear();
            Sec1Items.Add(new JM2ColItem("Ticker", Ticker1));
            Sec1Items.Add(new JM2ColItem("Volatility", Security1.Volatility.ToString()));
            Sec1Items.Add(new JM2ColItem("Close Price", Security1.LastPrice[1].ToString()));
            Sec1Items.Add(new JM2ColItem("Last Price", Security1.Last.ToString()));
            Sec1Items.Add(new JM2ColItem("New Position", Security1NewPosition.ToString()));
            Sec1Items.Add(new JM2ColItem("Stochastic", Security1.Stochastic.ToString()));
            Sec1Items.Add(new JM2ColItem("Previous Position", Sec1PrevPos.ToString()));
            Sec1Items.Add(new JM2ColItem("Action", Sec1Action.ToString()));
            Sec1Items.Add(new JM2ColItem("Current Position", Sec1CurPos.ToString()));

            Sec1Last.Clear();
            for (int i = 0; i < Math.Min(ParentModel.Days + 5, Security1.LastPrice.Length); i++)
            {
                Sec1Last.Add(Security1.LastPrice[i]);
            }            

            Sec2Items.Clear();
            Sec2Items.Add(new JM2ColItem("Ticker", Ticker2));
            Sec2Items.Add(new JM2ColItem("Volatility", Security2.Volatility.ToString()));
            Sec2Items.Add(new JM2ColItem("Close Price", Security2.LastPrice[1].ToString()));
            Sec2Items.Add(new JM2ColItem("Last Price", Security2.Last.ToString()));
            Sec2Items.Add(new JM2ColItem("New Position", Security2NewPosition.ToString()));
            Sec2Items.Add(new JM2ColItem("Stochastic", Security2.Stochastic.ToString()));
            Sec2Items.Add(new JM2ColItem("Previous Position", Sec2PrevPos.ToString()));
            Sec2Items.Add(new JM2ColItem("Action", Sec2Action.ToString()));
            Sec2Items.Add(new JM2ColItem("Current Position", Sec2CurPos.ToString()));

            Sec2Last.Clear();
            for (int i = 0; i < Math.Min(ParentModel.Days + 5, Security2.LastPrice.Length); i++)
            {
                Sec1Last.Add(Security2.LastPrice[i]);
            }                       
        }

        public void UpdateSentQty(int IdSecurity, int Qty)
        {
            if (IdSecurity == Security1.IdSecurity)
            {
                Sec1Sent += Qty;
            }
            else if (IdSecurity == Security2.IdSecurity)
            {
                Sec2Sent += Qty;
            }            
        }

        public void UpdateExecQty(int IdSecurity, int Qty)
        {
            if (IdSecurity == Security1.IdSecurity)
            {
                Sec1Sent -= Qty;
                Sec1Exec += Qty;
            }
            else if (IdSecurity == Security2.IdSecurity)
            {
                Sec2Sent -= Qty;
                Sec2Exec += Qty;
            }

            string deleteString = "";
            string insertString = "";

            int Sec1TablePos = _Sec1PrevPos + Sec1Exec;
            int Sec2TablePos = _Sec2PrevPos + Sec2Exec;

            if (ParentModel.DateRef == DateTime.Today)
            {
                deleteString = " DELETE FROM NESTSIM.DBO.TB702_JM_POSITIONS " +
                               " WHERE DATE = '" + ParentModel.DateRef.ToString("yyyyMMdd") + "' " +
                               " AND IDMODEL = " + ParentModel.IdModel +
                               " AND IDPAIR = " + IdPair;

                insertString = "INSERT INTO NESTSIM.DBO.TB702_JM_POSITIONS" +
                               "(DATE,IDMODEL,IDPAIR,SEC1POSITION,SEC2POSITION) VALUES " +
                               "('" + ParentModel.DateRef.ToString("yyyyMMdd") + "'," + ParentModel.IdModel + "," + IdPair + "," + Sec1TablePos.ToString() + "," + Sec2TablePos.ToString() + ")";
            }
            else
            {
                deleteString = " DELETE FROM NESTSIM.DBO.TB703_JM_BT_POSITIONS " +
                               " WHERE DATE = '" + ParentModel.DateRef.ToString("yyyyMMdd") + "' " +
                               " AND IDMODEL = " + ParentModel.IdModel +
                               " AND IDPAIR = " + IdPair;

                insertString = "INSERT INTO NESTSIM.DBO.TB703_JM_BT_POSITIONS" +
                               "(DATE,IDMODEL,IDPAIR,SEC1POSITION,SEC2POSITION) VALUES " +
                               "('" + ParentModel.DateRef.ToString("yyyyMMdd") + "'," + ParentModel.IdModel + "," + IdPair + "," + Sec1TablePos.ToString() + "," + Sec2TablePos.ToString() + ")";
            }

            if ((Sec1TablePos == 0 && Sec2TablePos != 0) || (Sec1TablePos != 0 && Sec2TablePos == 0))
            {
                int a = 0;
            }


            using (newNestConn curConn = new newNestConn(true))
            {
                curConn.ExecuteNonQuery(deleteString);
                curConn.ExecuteNonQuery(insertString);
            }
        }
        
    }
}
