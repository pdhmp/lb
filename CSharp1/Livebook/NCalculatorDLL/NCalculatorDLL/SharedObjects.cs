using System;
using System.Collections.Generic;
using LiveDLL;
using System.Data;


namespace NCalculatorDLL
{
    public class PositionItem : IComparable
    {
        public PositionItem()
        {
            RTEnabled.Add(1);
            RTEnabled.Add(5);
            RTEnabled.Add(13);
            RTEnabled.Add(24);
            RTEnabled.Add(30);
            RTEnabled.Add(34);
        }

        public string IdentityKey
        {
            get { return IdSecurity + "" + IdSection + "" + IdBook + "" + IdPortfolio; }
        }

        public bool RTPriceFlag = false;
        public bool RTQuantityFlag = false;
        public bool FlagTrade = false;
        public bool Removed = false;

        public string Description = "";
        public string PriceTable = "";
        public string SecurityCurrency = "";
        public string Instrument = "";
        public string AssetClass = "";
        public string SubIndustry = "";
        public string Industry = "";
        public string IndustryGroup = "";
        public string Sector = "";
        public string UnderlyingCountry = "";
        public string NestSector = "";
        public string Underlying = "";
        public string SubPortfolio = "";
        public string Strategy = "";
        public string SubStrategy = "";
        public string Portfolio = "";
        public string ExchangeTicker = "";
        public string SubscribeTicker = "";


        //=========================================================              IDENTIFIERS          =================================================================================
        //===========================================================================================================================================================================

        public int IdPosition = 0;
        public int IdPortfolio = 0;
        public int IdBook = 0;
        public int IdSection = 0;
        public int IdSecurity = 0;
        public int IdInstrument = 0;
        public int IdUnderlying = 0;
        public int IdAccount = 0;
        public int IdAdministrator = 0;
        public int IdBaseUnderlying = 0;
        public int IdBaseUnderlyingCurrency = 0;
        public int IdSourceClose = 0;
        public int IdSourceCloseAdmin = 0;
        public int IdSourceLast = 0;
        public int IdSourceLastAdmin = 0;
        public int IdSubStrategy = 0;
        public int IdTickerCash = 0;
        public int IdUnderlyingAcount = 0;
        public int IdForward = 0;
        public int IdSecurityPrice = 0;
        public int IdAssetClass = 0;
        public int IdPriceTable = 0;
        public int IdPrimaryExchange = 0;
        public int IdSecurityCurrency = 0;
        public int IdStrategy = 0;
        public int IdSubPortfolio = 0;

        //=========================================================              QUANTITY          =================================================================================
        //===========================================================================================================================================================================

        #region Vars
        public double
        AmtBoughtDiv = 0,
        AmtBoughtFwd = 0,
        AmtSoldDiv = 0,
        AmtSoldFwd = 0,
        AmtSoldOther = 0,
        QuantBoughtFactor = 1,
        QuantBoughtFwd = 0,
        QuantBoughtOther = 0,
        QuantBoughtTrade = 0,
        QuantityBought = 0,
        QuantitySold = 0,
        QuantSoldDiv = 0,
        QuantSoldFwd = 0,
        QuantSoldOther = 0,
        QuantSoldTrade = 0,
        AmtBoughtOther = 0,
        AvgPriceBought = 0,
        QuantBoughtDiv = 0;
        #endregion

        private double _AmtBoughtTrade = 0;
        public double AmtBoughtTrade
        {
            get { return _AmtBoughtTrade; }
            //set { if (value != 0) { if (_AmtBoughtTrade != value) { _AmtBoughtTrade = value; UpdateQuantityDependants(); } } }
            set { { if (_AmtBoughtTrade != value) { _AmtBoughtTrade = value; UpdateQuantityDependants(); } } }
        }


        private double _AmtSoldTrade = 0;
        public double AmtSoldTrade
        {
            get { return _AmtSoldTrade; }
            //set { if (value != 0) { if (_AmtSoldTrade != value) { _AmtSoldTrade = value; UpdateQuantityDependants(); } } }
            set { { if (_AmtSoldTrade != value) { _AmtSoldTrade = value; UpdateQuantityDependants(); } } }
        }



        public double InitialPosition = 0;

        public double CurrentPosition
        {
            get
            { return Math.Round(InitialPosition, 2) + Math.Round(QuantBought, 2) + Math.Round(QuantSold, 2); }
        }

        private double _QuantBoughtCf; public double QuantBoughtCf { get { return QuantBoughtTrade + QuantBoughtDiv + QuantBoughtFwd; } }
        private double _QuantBought; public double QuantBought { get { return QuantBoughtCf + QuantBoughtOther; } }
        private double _QuantBoughtSum; public double QuantBoughtSum { get { return this.QuantBought * this.QuantBoughtFactor; } }

        private double _QuantSoldCf; public double QuantSoldCf { get { return QuantSoldTrade + QuantSoldDiv + QuantSoldFwd; } }
        private double _QuantSold; public double QuantSold { get { return QuantSoldCf + QuantSoldOther; } }
        private double _QuantSoldSum; public double QuantSoldSum { get { return this.QuantSold * this.QuantSoldFactor; } }


        //=========================================================              PRICING          =================================================================================
        //===========================================================================================================================================================================

        public double SpotCloseUsd { get { return 1 / SpotClose; } }
        public double SpotUsd { get { return 1 / Spot; } }
        public double LastPc { get { return Last * Spot; } }
        public double ClosePc { get { return this.Close * this.SpotClose; } }
        public double DisplayClosePc { get { return ClosePc; } }
        public double DisplayCostClose { get { return CostClose; } }
        public double DisplayCostClosePc { get { return CostClosePC; } }
        public double DisplayLastPc { get { return DisplayLast * Spot; } }
        public double LastPcAdmin { get { return this.LastUcAdmin * this.Spot; } }
        public double ClosePcAdmin { get { return this.CloseAdmin * this.SpotClose; } }
        public DateTime UpdTimeLast = new DateTime(1900, 1, 1);
        public DateTime UpdTimeLastAdmin = new DateTime(1900, 1, 1);
        public string FlagLast = "";
        public string FlagLastAdmin = "";
        public string FlagClose = "";
        private double _Close = 0; public double Close { get { if (_Close != 0) return _Close; else return CostClose; } set { _Close = value; } }
        private double _CloseAdmin = 0; public double CloseAdmin { get { if (_CloseAdmin != 0) return _CloseAdmin; else return CostCloseAdmin; } set { _CloseAdmin = value; } }
        private double _DisplayClose = 0; public double DisplayClose { get { if (_DisplayClose != 0) return _DisplayClose; else return CostClose; } set { _DisplayClose = value; } }

        public string SourceLast { get { lock (GlobalVars.Instance.SourceList) { return GlobalVars.Instance.SourceList[IdSourceLast]; } } }
        public string SourceLastAdmin { get { lock (GlobalVars.Instance.SourceList) { return GlobalVars.Instance.SourceList[IdSourceLastAdmin]; } } }
        public string SourceClose { get { lock (GlobalVars.Instance.SourceList) { return GlobalVars.Instance.SourceList[IdSourceClose]; } } }
        public string SourceCloseAdmin { get { lock (GlobalVars.Instance.SourceList) { return GlobalVars.Instance.SourceList[IdSourceCloseAdmin]; } } }

        //=========================================================              EXPOSURE          =================================================================================
        //===========================================================================================================================================================================

        public double NotionalCloseUc { get { return this.CostClose * this.CurrentPosition / this.LotSize; } set { NotionalCloseUc = value; } }
        public double NotionalCloseAdmin { get { return this.CostCloseAdmin * this.CurrentPosition / this.LotSize; } set { NotionalCloseAdmin = value; } }
        public double NotionalUc
        {
            get
            {
                return this.Last * this.CurrentPosition / this.LotSize;
            }
            set { NotionalUc = value; }
        }
        public double NotionalAdmin { get { return this.LastUcAdmin * this.CurrentPosition / this.LotSize; } set { NotionalAdmin = value; } }

        public double DeltaCash
        {
            get
            {
                if (this.IdAssetClass == 2) { return ((this.Delta * this.Notional) / this.ContractRatio) * this.FactorFi; }
                else if (this.IdAssetClass == 3) { return ((this.Delta * this.Notional) / this.ContractRatio) * this.FactorCurr; }
                else return (this.Delta * this.Notional) / this.ContractRatio;
            }
        }
        public double DeltaNav { get { return this.DeltaCash / this.Nav; } }

        public double LongDelta { get { if ((this.DeltaCash / this.Nav) > 0) return Math.Abs(this.DeltaCash / this.Nav); else return 0; } } // ,[Long Delta]=dbo.FCN_GETD_LONG(@Delta_Cash/@NAV)
        public double ShortDelta { get { if ((this.DeltaCash / this.Nav) < 0) return (this.DeltaCash / this.Nav); else return 0; } } // ,[Short Delta]=dbo.FCN_GETD_SHORT(@Delta_Cash/@NAV)
        public double Long { get { if ((this.Cash / this.Nav) > 0) return Math.Abs(this.Cash / this.Nav); else return 0; } } //  ,[Long]=dbo.FCN_GETD_LONG(@Cash/@NAV) 
        public double Short { get { if ((this.Cash / this.Nav) < 0) return (this.Cash / this.Nav); else return 0; } } //  ,[Short]=dbo.FCN_GETD_SHORT(@Cash/@NAV) 
        public double Gross { get { return Math.Abs(this.Cash / this.Nav); } } //  ,[Gross]=dbo.FCN_GETD_GROSS(@Cash/@NAV)) 
        public double GrossDelta { get { return Math.Abs(this.DeltaCash / this.Nav); } } // ,[Gross Delta]=dbo.FCN_GETD_GROSS(@Delta_Cash/@NAV)
        public double DeltaQuantity { get { return this.Delta * this.CurrentPosition; } } // ,[Gross Delta]=dbo.FCN_GETD_GROSS(@Delta_Cash/@NAV)

        public string Side { get { if (this.CurrentPosition > 0) return "Long"; else return "Short"; } }      //[Side]=CASE WHEN Position>0 THEN 'Long' ELSE 'Short' END
        public string DeltaSide { get { if (this.DeltaCash > 0) return "Long"; else return "Short"; } }// ,[Delta Side]=CASE WHEN @Delta_Cash>0 THEN 'Long' ELSE 'Short' END
        public double CashNav { get { return this.Cash / this.NavPc; } }

        //=========================================================              BOOK          =================================================================================
        //===========================================================================================================================================================================

        public double LongBook { get { if ((DeltaBook) > 0) return DeltaBook; else return 0; } }
        public double ShortBook { get { if ((DeltaBook) < 0) return DeltaBook; else return 0; } }
        public double GrossBook { get { return Math.Abs(DeltaBook); } }
        public double DeltaBook { get { if (this.BookSize != 0) return (this.DeltaCash / this.Nav) / this.BookSize; else return 0; } }
        public double ContributionPcBook { get { return (this.TotalPl / this.prevNAV) / this.BookSize; } }
        public double AssetBookContribution { get { return (this.AssetPlPcAdmin / (this.prevNAV * this.BookSize)); } }
        public double CurrencyBookContribution { get { return (this.CurrencyPlAdmin / (this.prevNAV * this.BookSize)); } }
        public double BookNav { get { return this.Nav * this.BookSize; } }

        //=========================================================              LOAN          =================================================================================
        //===========================================================================================================================================================================

        public double LoanPotentialGain
        {
            get
            {
                if (this.CurrentPosition > 0)
                {
                    return LoanRateOutCblc * CashUc / 252;
                }
                else
                {
                    return LoanRateInCblc * CashUc / 252;
                }
                //,[Loan Potential Gain]=CASE WHEN Position>0 THEN @LoanRateOutCBLC*[Cash uC]/252 ELSE @LoanRateInCBLC*[Cash uC]/252 END
            }
            set
            {
                LoanPotentialGain = value;
            }
        }
        public double LoanPotentialBookContribution
        {
            get
            {
                if (this.CurrentPosition > 0)
                    return LoanRateOutCblc * CashUc / BookNav;
                else
                    return LoanRateInCblc * CashUc / BookNav;
                // ,[Loan Potential Book Contribution]=CASE WHEN Position>0 THEN @LoanRateOutCBLC*[Cash uC] ELSE @LoanRateInCBLC*[Cash uC] END/[Book NAV]
            }
            set
            {
                LoanPotentialBookContribution = value;
            }
        }

        public double LoanMarginalGain
        {
            get
            {
                if (this.CurrentPosition > 0)
                    return LoanRateOutCblc * CashUc / 252 - this.LoanCost;
                else
                    return LoanRateInCblc * CashUc / 252 - this.LoanCost;

                // ,[Loan Marginal Gain]=CASE WHEN Position>0 THEN @LoanRateOutCBLC*[Cash uC]/252 ELSE @LoanRateInCBLC*[Cash uC]/252 END-[Loan Cost]
            }
            set
            {
                LoanMarginalGain = value;
            }
        }
        public double LoanMarginalBookContribution
        {
            get
            {
                if (this.CurrentPosition > 0)
                    return (this.LoanRateOutCblc * this.CashUc / 252 - this.LoanCost) / this.BookNav;
                else
                    return (this.LoanRateInCblc * this.CashUc / 252 - this.LoanCost) / this.BookNav;

                //,[Loan Marginal Book Contribution]=(CASE WHEN Position>0 THEN @LoanRateOutCBLC*[Cash uC]/252 ELSE @LoanRateInCBLC*[Cash uC]/252 END-[Loan Cost])/[Book NAV]
            }
        }

        //=========================================================              PNL/CASH FLOW          =============================================================================
        //===========================================================================================================================================================================

        public double AssetUcAdmin { get { return LastUcAdmin - this.CloseAdmin; } }
        public double AssetUc { get { return Last - CostClose; } }
        public double AssetPc { get { return (Last - CostClose) * Spot; } }
        public double ContributionPc { get { return this.TotalPl / this.prevNAV; } }
        public double ContributionAdmin { get { return this.TotalPlAdmin / this.prevNAV; } }

        public double ContributionUc { get { return this.AssetPlUc / this.prevNAV; } }
        public double DifContribution { get { return (this.TotalPl / this.prevNAV) - this.ContributionAdmin; } }
        public double AssetContribution { get { return this.AssetPlPcAdmin / this.prevNAV; } }
        public double CurrencyContribution { get { return this.CurrencyPlAdmin / this.prevNAV; } }
        public double TradeFlow { get { return AmtBoughtTrade + AmtSoldTrade + Dividends; } }
        public double CashFlow
        {
            get
            {
                if (this.IdInstrument != 4 && this.IdInstrument != 16)
                {
                    return AmtBoughtTrade + AmtBoughtFwd + AmtBoughtOther + AmtSoldTrade + AmtSoldFwd + AmtSoldOther - Dividends;
                }
                else if (this.InitialPosition != 0)
                {
                    return -this.PrevAssetPl;
                }
                else
                {
                    return 0;
                }
            }
        }
        public double AmtBoughtCf { get { return AmtBoughtTrade + AmtBoughtDiv + AmtBoughtFwd; } }
        public double AmtSoldCf { get { return AmtSoldTrade + AmtSoldDiv + AmtSoldFwd; } }


        //=========================================================              OPTIONS          =================================================================================
        //===========================================================================================================================================================================
        public DateTime VolDate = new DateTime(1900, 1, 1);

        public double GammaCash { get { return (this.Gamma * this.Notional) / this.ContractRatio; } }
        public double GammaQuantity { get { return this.Gamma * this.CurrentPosition; } }
        public double GammaNav { get { return this.GammaCash / this.Nav; } }
        public double Dividends { get { return AmtBoughtDiv + AmtSoldDiv; } }


        //=========================================================              FOWARD          =================================================================================
        //===========================================================================================================================================================================

        public double IdCurrencyTickerRef;
        public double IdCurrencyPortfolioRef;


        private double _Brl = 0;
        public double Brl
        {
            get
            {
                if (_Brl != 0)
                    return _Brl;

                else
                {
                    if (IdPortCurrency == 900)
                    {
                        ReturnGetdNBrl curReturnGetdNBrl = GlobalVars.Instance.GetdNBrl(IdSecurity, Cash, DeltaCash);
                        _Brl = curReturnGetdNBrl.ReturnValue;
                        return _Brl;
                    }
                    else
                    {
                        ReturnGetDBrl curReturnGetDBrl = GlobalVars.Instance.GetDBrl(IdSecurity, Cash, DeltaCash);
                        _Brl = curReturnGetDBrl.ReturnValue;
                        return _Brl;
                    }
                }
            }
        }
        public double BrlNav { get { return this.Brl / this.Nav; } }
        private double _BookSize; public double BookSize { get { if (_BookSize != 0) return _BookSize; else return 1; } set { _BookSize = value; } }
        public double DaysToLiquidity { get { if (this.AvVolume6m != 0) return (this.CurrentPosition / this.AvVolume6m); else return 0; } }
        public double PlPerc { get { if (this.ClosePc != 0) { if (this.ClosePc > 0.009) return (this.LastPc / this.ClosePc) - 1; else return 0; } else return 0; } }        //,[P/L %]=CASE WHEN [Close pC]<>0 THEN (@Last_Pc/@Close_pc)-1 ELSE 0 END        }        
        public double CashPremium { get { if (this.IdInstrument == 3 || this.IdInstrument == 26) return this.Cash; else return 0; } }    //,[Cash Premium]=CASE WHEN [Id Instrument] in(3,26) THEN @Cash ELSE 0 END        }
        public double CostClosePC { get { return this.CostClose * this.SpotClose; } }
        public double CostClosePcAdmin { get { return this.CostCloseAdmin * this.SpotClose; } }

        //=========================================================              Real Time Fields        =================================================================================
        //===========================================================================================================================================================================

        private int _IsRT; public int IsRT { get { return _IsRT; } set { _IsRT = value; } }

        public List<int> RTEnabled = new List<int>();

        private double _Last = 0;
        public double Last
        {
            get { if (_IsRT == 1 && _RTLast != 0 && RTEnabled.Contains(this.IdSourceLast)) { return _RTLast; } else { return _Last; } }
            set { if (value != _Last || value == 0) { _Last = value; UpdateLastDependants(); } }
        }
        private double _RTLast = 0;
        public double RTLast
        {
            get { return _RTLast; }
            //set { if(value != _RTLast) { _RTLast = value; this.UpdTimeLast = DateTime.Now; UpdateLastDependants(); } }
            set { if (value != _RTLast) { _RTLast = value; this.UpdTimeLast = DateTime.Now; Last = value; } }
        }

        private double _LastUcAdmin = 0;
        public double LastUcAdmin
        {
            get { if (_IsRT == 1 && _RTLastUcAdmin != 0 && RTEnabled.Contains(this.IdSourceLast)) { return _RTLastUcAdmin; } else { return _LastUcAdmin; } }
            set { if (value != _LastUcAdmin || value == 0) { _LastUcAdmin = value; UpdateLastAdminDependants(); } }
        }
        private double _RTLastUcAdmin = 0;
        public double RTLastUcAdmin
        {
            get { return _RTLastUcAdmin; }
            set { if (value != _RTLastUcAdmin) { _RTLastUcAdmin = value; UpdateLastAdminDependants(); } }
        }


        //------------------

        private double _Bid = 0;
        public double Bid
        {
            get { if (_IsRT == 1 && _RTBid != 0) { return _RTBid; } else { return _Bid; } }
            set { if (value != _Bid) { _Bid = value; UpdateBidDependants(); } }
        }
        private double _RTBid = 0;
        public double RTBid
        {
            get { return _RTBid; }
            set { if (value != _RTBid) { _RTBid = value; UpdateBidDependants(); } }
        }

        //------------------

        private double _Ask = 0;
        public double Ask
        {
            get { if (_IsRT == 1 && _RTAsk != 0) { return _RTAsk; } else { return _Ask; } }
            set { if (value != _Ask) { _Ask = value; UpdateAskDependants(); } }
        }
        private double _RTAsk = 0;
        public double RTAsk
        {
            get { return _RTAsk; }
            set { if (value != _RTAsk) { _RTAsk = value; UpdateAskDependants(); } }
        }


        public double LoanBookContribution { get { return this.LoanCost / this.BookNav * 252; } } //[Loan Cost]/[Book NAV]*252";


        public double TenYearEquivDnav { get { if (this.Duration10Y != 0) return this.DeltaCash / this.Nav * this.Duration / this.Duration10Y; else return 0; } }

        private double _FactorCurr; public double FactorCurr { get { if (_FactorCurr != 0) return _FactorCurr; else return 1; } set { _FactorCurr = value; } }
        private double _FactorEqut; public double FactorEqut { get { if (_FactorEqut != 0) return _FactorEqut; else return 1; } set { _FactorEqut = value; } }
        private double _FactorFi; public double FactorFi { get { if (_FactorFi != 0) return _FactorFi; else return 1; } set { _FactorFi = value; } }


        public DateTime DateNow = new DateTime(1900, 1, 1);
        public DateTime DateClose = new DateTime(1900, 1, 1);
        public DateTime DurationDate = new DateTime(1900, 1, 1);
        public DateTime DataPosicao = new DateTime(1900, 1, 1);
        //public DateTime DataTrade = new DateTime(1900, 1, 1);
        public DateTime Expiration = new DateTime(1900, 1, 1);
        public DateTime FwdExpiration = new DateTime(1900, 1, 1);

        public string Section = "";
        public string Book = "";

        #region Vars


        public int DaysToExpiration = 0,
        OptionType = 0,
        PriceFromUnderlying = 0,
        IdSourceAdmin = 0,
        IdPortCurrency = 0;

        public double LoanCost = 0; // precisamos fazer funcionar

        //public double Last { get { if (DBLast != 0) return DBLast; else return MKTLast; } } CALC REAL TIME


        public double AdjFwPosition = 0,
        AvVolume6m = 0,
        SpotPrice,
        CurPrice,
        OrigQuantity = 0,
        PrevPrice,
        FwdAdjPl,
        FwdPrice,
        FwdAdjPrice,
        AssetPlPc = 0,
        AssetPlPcAdmin = 0,
        AssetPlUc = 0,
        AssetPlUcAdmin = 0,
        AvgPriceSold = 0,
        Cash = 0,
        CashAdmin = 0,
        CashUc = 0,
        CashUcAdmin = 0,
        ContractRatio = 0,
        CostClose = 0,
        CostCloseAdmin = 0,
        CurrencyChange = 0,
        CurrencyChangeAdmin = 0,
        CurrencyPl = 0,
        CurrencyPlAdmin = 0,
        _CxBook = 0,
        Delta = 1,
        DisplayLast = 0,
        Duration = 0,
        FlagCloseAdmin = 0,
        FwValue = 0,
        Gamma = 0,
        LoanRateInCblc = 0,
        LoanRateOutCblc = 0,
        LoanMktTotal = 0,
        LotSize = 0,
        MktCap = 0,
        ModelPrice = 0,
        ModelPriceTm1 = 0,
        Nav = 0,
        NavPc = 0,

        prevNAV = 0,
        prevNavPc = 0,

        Notional = 0,
        NotionalClose = 0,
        OptionIntrinsic = 0,
        OptionIntrinsicCashPc = 0,
        OptionTvCashPc = 0,
        OptionTvUc = 0,
        PercentBidAsk = 0,
        PercentSplit = 0,
        PrevAssetPl = 0,
        PrevAssetPlAdmin = 0,
        PrevCashUc = 0,
        PrevCashUcAdmin = 0,
        RTUpdateSource = 0,
        HistUpdateSource = 0,
        QuantSoldFactor = 1,
        RatePeriod = 0,
        RateYear = 0,
        Realized = 0,
        RealizedAdmin = 0,
        Rho = 0,
        StrategyPercent = 0,
        Spot = 1,
        SpotClose = 1,
        Strike = 0,
        Theta = 0,
        ThetaNav = 0,
        TotalPl = 0,
        TotalPlAdmin = 0,
        UnderlyingClose = 0,
        UnderlyingCloseAdmin = 0,
        UnderlyingClosePc = 0,
        UnderlyingClosePcAdmin = 0,
        UnderlyingLast = 0,
        UnderlyingLastAdmin = 0,
        UnderlyingLastPc = 0,
        UnderlyingLastPcAdmin = 0,
        Vega = 0,
        Volatility = 0,
        Duration10Y = 0,
        YearFraction = 0;

        public double CxBook
        {
            get { if (double.IsInfinity(_CxBook) || double.IsNaN(_CxBook)) return 0; return _CxBook; }
            set { _CxBook = value; }
        }

        public string BaseUnderlyingCurrency = "",
        BaseUnderlying = "",
        VolFlag = "",
        NestTicker = "",
        BloombergTicker = "",
        ReutersTicker = "",
        ImagineTicker = "",
        BtgTicker = "",
        ItauTicker = "",
        MellonTicker = "",
        AdminTicker = "";



        #endregion

        public void UpdateLastDependants()
        {
            if (IdBook == 9 && (IdSubStrategy == 22 || IdSubStrategy == 26) && IsRT == 1 && DateTime.Now.Hour < 19 && IdInstrument != 16)
            {
                if (CurrentPosition != 0)
                {
                    _Last = (PrevCashUc + CashFlow) / CurrentPosition;
                    _RTLast = _Last;
                }
                else
                {
                    _Last = 0;
                    _RTLast = 0;
                }
            }

            if (IdUnderlying == 1079 || IdInstrument == 16) // Brazilian DI Contracts
            {
                if (_Last > 0 || _RTLast > 0)
                {
                    _Last = _Last * -1;
                    _RTLast = _RTLast * -1;
                }

                if (IdBook == 5 && DateTime.Now.Hour < 19 && IsRT == 1) { _Last = Close; }
            }

            if (IdInstrument == 3 || IdInstrument == 26) // Options or Warrants
            {

                if (OptionType == 1)
                {
                    OptionIntrinsic = UnderlyingLast - Strike;
                }
                else
                {
                    OptionIntrinsic = Strike - UnderlyingLast;
                }
                if (OptionIntrinsic < 0) { OptionIntrinsic = 0; }

                OptionTvUc = _Last - OptionIntrinsic;
                CashUc = Last * (CurrentPosition / LotSize);

                OptionTvCashPc = (OptionTvUc * (CurrentPosition / LotSize)) * Spot;
                OptionIntrinsicCashPc = (OptionIntrinsic * (CurrentPosition / LotSize)) * Spot;

                Notional = UnderlyingLastPc * CurrentPosition / LotSize;
                NotionalClose = UnderlyingClosePcAdmin * CurrentPosition / LotSize;

            }
            else
            {
                Notional = LastPc * CurrentPosition / LotSize;
                NotionalClose = CostClosePC * CurrentPosition / LotSize;
                CashUc = NotionalUc;
            }


            if (IdInstrument == 4 || IdInstrument == 16)
            {
                AssetPlUc = (((Last - CostClose) * (CurrentPosition / LotSize)) + Realized);
                CashUc = AssetPlUc;
            }
            else
            {
                AssetPlUc = CashUc - CashFlow - PrevCashUc;
            }

            if (IdInstrument == 6)
            {
                AssetPlUc = 0;
                AssetPlPc = 0;
            }
            else
            {
                AssetPlPc = AssetPlUc * Spot;
            }

            if (IdInstrument == 12)
            {
                if (CurrentPosition != 0)
                {
                    CashUc = NotionalUc;
                }
                else
                {
                    CashUc = 0;
                }

                CxBook = 0;
            }


            DisplayLast = Last;

            Cash = CashUc * Spot;

            if (IdInstrument != 12)
            {
                CxBook = Cash / Nav / BookSize;
            }

            TotalPl = AssetPlPc + CurrencyPl;
            RTPriceFlag = true;
        }

        public void UpdateLastAdminDependants()
        {
            if (IdBook == 9 && (IdSubStrategy == 22 || IdSubStrategy == 26) && IsRT == 1 && DateTime.Now.Hour < 19 && IdInstrument != 16)
            {
                if (CurrentPosition != 0)
                {
                    _LastUcAdmin = (PrevCashUcAdmin + CashFlow) / CurrentPosition;
                    _RTLastUcAdmin = _LastUcAdmin;
                }
                else
                {
                    _LastUcAdmin = 0;
                    _RTLastUcAdmin = 0;
                }
            }

            if (IdInstrument == 3 || IdInstrument == 26) // Options or Warrants
            {
                CashUcAdmin = LastUcAdmin * (CurrentPosition / LotSize);
            }
            else
            {
                CashUcAdmin = NotionalAdmin;
            }

            if (IdUnderlying == 1079 || IdInstrument == 16) // Brazilian DI Contracts
            {
                if (_LastUcAdmin > 0 || _RTLastUcAdmin > 0)
                {
                    _LastUcAdmin = _LastUcAdmin * -1;
                    _RTLastUcAdmin = _RTLastUcAdmin * -1;
                }
                if (IdBook == 5 && DateTime.Now.Hour < 19 && IsRT == 1) { _LastUcAdmin = CloseAdmin; }
            }

            if (IdInstrument == 4 || IdInstrument == 16)
            {
                AssetPlUcAdmin = (((LastUcAdmin - CostCloseAdmin) * (CurrentPosition / LotSize)) + RealizedAdmin);
                CashUcAdmin = AssetPlUcAdmin;
            }
            else
            {
                AssetPlUcAdmin = CashUcAdmin - CashFlow - PrevCashUcAdmin;
            }

            if (IdInstrument == 6)
            {
                AssetPlPcAdmin = 0;
                AssetPlUcAdmin = 0;
            }
            else
            {
                AssetPlPcAdmin = AssetPlUcAdmin * Spot;
            }

            if (IdInstrument == 12)
            {
                if (CurrentPosition != 0)
                {
                    CashUcAdmin = NotionalAdmin;
                }
                else
                {
                    CashUcAdmin = 0;
                }

            }

            CashAdmin = CashUcAdmin * Spot;
            TotalPlAdmin = AssetPlPcAdmin + CurrencyPlAdmin;
        }

        public void UpdateBidDependants()
        {
            switch (IdInstrument)
            {
                case 5:
                case 6:
                case 13:
                    PercentBidAsk = 0;
                    break;
                default:
                    if (LastUcAdmin != 0 && CurrentPosition >= 0)
                    {
                        PercentBidAsk = (_Bid / LastUcAdmin) - 1;
                    }
                    break;
            }

            RTPriceFlag = true;
        }

        public void UpdateAskDependants()
        {

            switch (IdInstrument)
            {
                case 5:
                case 6:
                case 13:
                    PercentBidAsk = 0;
                    break;
                default:
                    if (LastUcAdmin != 0 && CurrentPosition < 0)
                    {
                        PercentBidAsk = ((_Ask / LastUcAdmin) - 1) * (CurrentPosition / Math.Abs(CurrentPosition));
                    }
                    break;
            }

            RTPriceFlag = true;
        }

        public void UpdateQuantityDependants()
        {
            if (IdInstrument == 3 || IdInstrument == 26) // Options or Warrants
            {
                CashUc = Last * (CurrentPosition / LotSize);

                OptionTvCashPc = (OptionTvUc * (CurrentPosition / LotSize)) * Spot;
                OptionIntrinsicCashPc = (OptionIntrinsic * (CurrentPosition / LotSize)) * Spot;

                Notional = UnderlyingLastPc * CurrentPosition / LotSize;
                NotionalClose = UnderlyingClosePcAdmin * CurrentPosition / LotSize;
            }
            else
            {
                Notional = LastPc * CurrentPosition / LotSize;
                NotionalClose = CostClosePC * CurrentPosition / LotSize;
                CashUc = NotionalUc;
            }

            if (IdInstrument == 4 || IdInstrument == 16)
            {
                AssetPlUc = (((Last - CostClose) * (CurrentPosition / LotSize)) + Realized);
                CashUc = AssetPlUc;
            }
            else
            {
                AssetPlUc = CashUc - CashFlow - PrevCashUc;
            }

            if (IdInstrument == 6)
            {
                AssetPlUc = 0;
                AssetPlPc = 0;
            }
            else
            {
                AssetPlPc = AssetPlUc * Spot;
            }

            if (IdInstrument == 12)
            {
                if (CurrentPosition != 0) // verificar se a divisao não deixará o CurrentPosition > 0.0000
                {
                    CashUc = NotionalUc;
                }
                else
                {
                    CashUc = 0;
                }

                CxBook = 0;
            }


            Cash = CashUc * Spot;

            if (IdInstrument != 12)
            {
                CxBook = Cash / Nav / BookSize;
            }

            TotalPl = AssetPlPc + CurrencyPl;

            SetCostData();

            RTQuantityFlag = true;
        }

        public void UpdateCashPositions()
        {
            switch (this.IdInstrument)
            {
                case (6):
                case (17):
                case (24):
                    {

                    }
                    break;
                default:
                    break;
            }
        }

        public void Merge(PositionItem MergePositionItem)
        {
            this.QuantBoughtTrade += MergePositionItem.QuantBoughtTrade;
            this.QuantSoldTrade += MergePositionItem.QuantSoldTrade;
            this.AmtBoughtTrade += MergePositionItem.AmtBoughtTrade;
            this.AmtSoldTrade += MergePositionItem.AmtSoldTrade;

            this.QuantBoughtOther += MergePositionItem.QuantBoughtOther;
            this.QuantSoldOther += MergePositionItem.QuantSoldOther;
            this.AmtBoughtOther += MergePositionItem.AmtBoughtOther;
            this.AmtSoldOther += MergePositionItem.AmtSoldOther;

            this.QuantBoughtDiv += MergePositionItem.QuantBoughtDiv;
            this.QuantSoldDiv += MergePositionItem.QuantSoldDiv;
            this.AmtBoughtDiv += MergePositionItem.AmtBoughtDiv;
            this.AmtSoldDiv += MergePositionItem.AmtSoldDiv;

            this.QuantBoughtFwd += MergePositionItem.QuantBoughtFwd;
            this.QuantSoldFwd += MergePositionItem.QuantSoldFwd;
            this.AmtBoughtFwd += MergePositionItem.AmtBoughtFwd;
            this.AmtSoldFwd += MergePositionItem.AmtSoldFwd;
        }

        public PositionItem GetClone()
        {
            PositionItem newPositionItem = (PositionItem)this.MemberwiseClone();
            return newPositionItem;
        }

        public int CompareTo(object obj)
        {
            PositionItem Temp = (PositionItem)obj;
            if (this.IdPortfolio == Temp.IdPortfolio && this.IdBook == Temp.IdBook && this.IdSection == Temp.IdSection && this.IdSecurity == Temp.IdSecurity)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public string GetCompareString(PositionItem newPositionItem, PositionItem oldPositionItem, bool IgnoreRTChanges)
        {
            string UpdateString = "";

            if (newPositionItem.CurrentPosition != oldPositionItem.CurrentPosition) UpdateString += ", [Position]=" + newPositionItem.CurrentPosition.ToString().Replace(",", ".");

            if (!IgnoreRTChanges)
            {
                if (newPositionItem.QuantBoughtSum != oldPositionItem.QuantBoughtSum) UpdateString += ", [Quantity Bought]=" + newPositionItem.QuantBoughtSum.ToString().Replace(",", ".");
                if (newPositionItem.DeltaQuantity != oldPositionItem.DeltaQuantity) UpdateString += ", [Delta Quantity]=" + newPositionItem.DeltaQuantity.ToString().Replace(",", ".");
                if (newPositionItem.CostCloseAdmin != oldPositionItem.CostCloseAdmin) UpdateString += ", [Cost Close Admin]=" + newPositionItem.CostCloseAdmin.ToString().Replace(",", ".");
                if (newPositionItem.CostClosePcAdmin != oldPositionItem.CostClosePcAdmin) UpdateString += ", [Cost Close pC Admin]=" + newPositionItem.CostClosePcAdmin.ToString().Replace(",", ".");
                if (newPositionItem.CostClose != oldPositionItem.CostClose) UpdateString += ", [Cost Close]=" + newPositionItem.CostClose.ToString().Replace(",", ".");
                if (newPositionItem.DaysToLiquidity != oldPositionItem.DaysToLiquidity) UpdateString += ", [Days To Liquidity]=" + newPositionItem.DaysToLiquidity.ToString().Replace(",", ".");
                if (newPositionItem.CostClosePC != oldPositionItem.CostClosePC) UpdateString += ", [Cost Close pC]=" + newPositionItem.CostClosePC.ToString().Replace(",", ".");
                if (newPositionItem.DisplayCostClosePc != oldPositionItem.DisplayCostClosePc) UpdateString += ", [Display Cost Close pc] =" + newPositionItem.DisplayCostClosePc.ToString().Replace(",", ".");
                if (newPositionItem.DisplayCostClose != oldPositionItem.DisplayCostClose) UpdateString += ", [Display Cost Close]=" + newPositionItem.DisplayCostClose.ToString().Replace(",", ".");
                if (newPositionItem.GammaQuantity != oldPositionItem.GammaQuantity) UpdateString += ", [Gamma Quantity] =" + newPositionItem.GammaQuantity.ToString().Replace(",", ".");
                if (newPositionItem.LoanMarginalBookContribution != oldPositionItem.LoanMarginalBookContribution) UpdateString += ", [Loan Marginal Book Contribution]=" + newPositionItem.LoanMarginalBookContribution.ToString().Replace(",", ".");
                if (newPositionItem.LoanMarginalGain != oldPositionItem.LoanMarginalGain) UpdateString += ", [Loan Marginal Gain] =" + newPositionItem.LoanMarginalGain.ToString().Replace(",", "."); // verificar
                if (newPositionItem.OptionTvCashPc != oldPositionItem.OptionTvCashPc) UpdateString += ", [Option TV Cash pC]=" + newPositionItem.OptionTvCashPc.ToString().Replace(",", ".");
                if (newPositionItem.LoanPotentialGain != oldPositionItem.LoanPotentialGain) UpdateString += ", [Loan Potential Gain]=" + newPositionItem.LoanPotentialGain.ToString().Replace(",", ".");
                if (newPositionItem.LoanPotentialBookContribution != oldPositionItem.LoanPotentialBookContribution) UpdateString += ", [Loan Potential Book Contribution]=" + newPositionItem.LoanPotentialBookContribution.ToString().Replace(",", ".");
                if (newPositionItem.QuantSoldSum != oldPositionItem.QuantSoldSum) UpdateString += ", [Quantity Sold]=" + newPositionItem.QuantSoldSum.ToString().Replace(",", ".");
                if (newPositionItem.TradeFlow != oldPositionItem.TradeFlow) UpdateString += ", [Trade Flow]=" + newPositionItem.TradeFlow.ToString().Replace(",", ".");
                if (newPositionItem.LastPcAdmin != oldPositionItem.LastPcAdmin) UpdateString += ", [Last pC Admin]=" + newPositionItem.LastPcAdmin.ToString().Replace(",", ".");
                if (newPositionItem.PercentBidAsk != oldPositionItem.PercentBidAsk) UpdateString += ", [% to Bid/Ask]=" + newPositionItem.PercentBidAsk.ToString().Replace(",", ".");
                if (newPositionItem.Last != oldPositionItem.Last) UpdateString += ", [Last]=" + newPositionItem.Last.ToString().Replace(",", ".");
                if (newPositionItem.Bid != oldPositionItem.Bid) UpdateString += ", [Bid]=" + newPositionItem.Bid.ToString().Replace(",", ".");
                if (newPositionItem.Ask != oldPositionItem.Ask) UpdateString += ", [Ask]=" + newPositionItem.Ask.ToString().Replace(",", ".");
                if (newPositionItem.DisplayLast != oldPositionItem.DisplayLast) UpdateString += ", [Display Last]=" + newPositionItem.DisplayLast.ToString().Replace(",", ".");
                if (newPositionItem.DisplayLastPc != oldPositionItem.DisplayLastPc) UpdateString += ", [Display Last pC]=" + newPositionItem.DisplayLastPc.ToString().Replace(",", ".");
                if (newPositionItem.LastPc != oldPositionItem.LastPc) UpdateString += ", [Last pC]=" + newPositionItem.LastPc.ToString().Replace(",", ".");
                if (newPositionItem.LastUcAdmin != oldPositionItem.LastUcAdmin) UpdateString += ", [Last Admin]=" + newPositionItem.LastUcAdmin.ToString().Replace(",", ".");
                if (newPositionItem.UpdTimeLast != oldPositionItem.UpdTimeLast) UpdateString += ", [UpdTime Last]='" + newPositionItem.UpdTimeLast.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                if (newPositionItem.UpdTimeLastAdmin != oldPositionItem.UpdTimeLastAdmin) UpdateString += ", [UpdTime Last Admin]='" + newPositionItem.UpdTimeLastAdmin.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                if (newPositionItem.DeltaCash != oldPositionItem.DeltaCash) UpdateString += ", [Delta Cash]=" + newPositionItem.DeltaCash.ToString().Replace(",", ".");
                if (newPositionItem.DeltaNav != oldPositionItem.DeltaNav) UpdateString += ", [Delta/NAV]=" + newPositionItem.DeltaNav.ToString().Replace(",", ".");
                if (newPositionItem.LongDelta != oldPositionItem.LongDelta) UpdateString += ", [Long Delta]=" + newPositionItem.LongDelta.ToString().Replace(",", ".");
                if (newPositionItem.ShortDelta != oldPositionItem.ShortDelta) UpdateString += ", [Short Delta]=" + newPositionItem.ShortDelta.ToString().Replace(",", ".");
                if (newPositionItem.Long != oldPositionItem.Long) UpdateString += ", [Long]=" + newPositionItem.Long.ToString().Replace(",", ".");
                if (newPositionItem.Short != oldPositionItem.Short) UpdateString += ", [Short]=" + newPositionItem.Short.ToString().Replace(",", ".");
                if (newPositionItem.Gross != oldPositionItem.Gross) UpdateString += ", [Gross]=" + newPositionItem.Gross.ToString().Replace(",", ".");
                if (newPositionItem.GrossDelta != oldPositionItem.GrossDelta) UpdateString += ", [Gross Delta]=" + newPositionItem.GrossDelta.ToString().Replace(",", ".");
                if (newPositionItem.CashNav != oldPositionItem.CashNav) UpdateString += ", [Cash/NAV]=" + newPositionItem.CashNav.ToString().Replace(",", ".");
                if (newPositionItem.LongBook != oldPositionItem.LongBook) UpdateString += ", [Long Book]=" + newPositionItem.LongBook.ToString().Replace(",", ".");
                if (newPositionItem.ShortBook != oldPositionItem.ShortBook) UpdateString += ", [Short Book]=" + newPositionItem.ShortBook.ToString().Replace(",", ".");
                if (newPositionItem.GrossBook != oldPositionItem.GrossBook) UpdateString += ", [Gross Book]=" + newPositionItem.GrossBook.ToString().Replace(",", ".");
                if (newPositionItem.DeltaBook != oldPositionItem.DeltaBook) UpdateString += ", [Delta/Book]=" + newPositionItem.DeltaBook.ToString().Replace(",", ".");
                if (newPositionItem.ContributionPcBook != oldPositionItem.ContributionPcBook) UpdateString += ", [Contribution pC Book]=" + newPositionItem.ContributionPcBook.ToString().Replace(",", ".");
                if (newPositionItem.AssetBookContribution != oldPositionItem.AssetBookContribution) UpdateString += ", [Asset Book Contribution]=" + newPositionItem.AssetBookContribution.ToString().Replace(",", ".");
                if (newPositionItem.CurrencyBookContribution != oldPositionItem.CurrencyBookContribution) UpdateString += ", [Currency Book Contribution]=" + newPositionItem.CurrencyBookContribution.ToString().Replace(",", ".");
                if (newPositionItem.AssetUc != oldPositionItem.AssetUc) UpdateString += ", [ASSET uC]=" + newPositionItem.AssetUc.ToString().Replace(",", ".");
                if (newPositionItem.AssetPc != oldPositionItem.AssetPc) UpdateString += ", [ASSET pC]=" + newPositionItem.AssetPc.ToString().Replace(",", ".");
                if (newPositionItem.ContributionPc != oldPositionItem.ContributionPc) UpdateString += ", [Contribution pC]=" + newPositionItem.ContributionPc.ToString().Replace(",", ".");
                if (newPositionItem.ContributionUc != oldPositionItem.ContributionUc) UpdateString += ", [Contribution uC]=" + newPositionItem.ContributionUc.ToString().Replace(",", ".");
                if (newPositionItem.DifContribution != oldPositionItem.DifContribution) UpdateString += ", [Dif Contrib]=" + newPositionItem.DifContribution.ToString().Replace(",", ".");
                if (newPositionItem.AssetContribution != oldPositionItem.AssetContribution) UpdateString += ", [Asset Contribution]=" + newPositionItem.AssetContribution.ToString().Replace(",", ".");
                if (newPositionItem.CurrencyContribution != oldPositionItem.CurrencyContribution) UpdateString += ", [Currency Contribution]=" + newPositionItem.CurrencyContribution.ToString().Replace(",", ".");
                if (newPositionItem.Brl != oldPositionItem.Brl) UpdateString += ", [BRL]=" + newPositionItem.Brl.ToString().Replace(",", ".");
                if (newPositionItem.BrlNav != oldPositionItem.BrlNav) UpdateString += ", [BRL/NAV]=" + newPositionItem.BrlNav.ToString().Replace(",", ".");
                if (newPositionItem.PlPerc != oldPositionItem.PlPerc) UpdateString += ", [P/L %]=" + newPositionItem.PlPerc.ToString().Replace(",", ".");
                if (newPositionItem.CashPremium != oldPositionItem.CashPremium) UpdateString += ", [Cash Premium]=" + newPositionItem.CashPremium.ToString().Replace(",", ".");
                if (newPositionItem.LoanBookContribution != oldPositionItem.LoanBookContribution) UpdateString += ", [Loan Book Contribution]=" + newPositionItem.LoanBookContribution.ToString().Replace(",", ".");
                if (newPositionItem.TenYearEquivDnav != oldPositionItem.TenYearEquivDnav) UpdateString += ", [10Y Equiv DNAV]=" + newPositionItem.TenYearEquivDnav.ToString().Replace(",", ".");
                if (newPositionItem.AssetPlPc != oldPositionItem.AssetPlPc) UpdateString += ", [ASSET P/L pC]=" + newPositionItem.AssetPlPc.ToString().Replace(",", ".");
                if (newPositionItem.AssetPlPcAdmin != oldPositionItem.AssetPlPcAdmin) UpdateString += ", [Asset P/L pC Admin]=" + newPositionItem.AssetPlPcAdmin.ToString().Replace(",", ".");
                if (newPositionItem.AssetPlUc != oldPositionItem.AssetPlUc) UpdateString += ", [ASSET P/L uC]=" + newPositionItem.AssetPlUc.ToString().Replace(",", ".");
                if (newPositionItem.AssetPlUcAdmin != oldPositionItem.AssetPlUcAdmin) UpdateString += ", [Asset PL uC Admin]=" + newPositionItem.AssetPlUcAdmin.ToString().Replace(",", ".");
                if (newPositionItem.Cash != oldPositionItem.Cash) UpdateString += ", [Cash]=" + newPositionItem.Cash.ToString().Replace(",", ".");
                if (newPositionItem.CashAdmin != oldPositionItem.CashAdmin) UpdateString += ", [Cash Admin]=" + newPositionItem.CashAdmin.ToString().Replace(",", ".");
                if (newPositionItem.CashUc != oldPositionItem.CashUc) UpdateString += ", [Cash uC]=" + newPositionItem.CashUc.ToString().Replace(",", ".");
                if (newPositionItem.CashUcAdmin != oldPositionItem.CashUcAdmin) UpdateString += ", [Cash uC Admin]=" + newPositionItem.CashUcAdmin.ToString().Replace(",", ".");
                if (newPositionItem.ContributionAdmin != oldPositionItem.ContributionAdmin) UpdateString += ", [Contribution pC Admin]=" + newPositionItem.ContributionAdmin.ToString().Replace(",", ".");
                if (newPositionItem.CxBook != oldPositionItem.CxBook) UpdateString += ", [CXBOOK]=" + newPositionItem.CxBook.ToString().Replace(",", ".");
                if (newPositionItem.Notional != oldPositionItem.Notional) UpdateString += ", [Notional]=" + newPositionItem.Notional.ToString().Replace(",", ".");
                if (newPositionItem.OptionIntrinsic != oldPositionItem.OptionIntrinsic) UpdateString += ", [Option Intrinsic]=" + newPositionItem.OptionIntrinsic.ToString().Replace(",", ".");
                if (newPositionItem.OptionIntrinsicCashPc != oldPositionItem.OptionIntrinsicCashPc) UpdateString += ", [Option Intrinsic Cash pC]=" + newPositionItem.OptionIntrinsicCashPc.ToString().Replace(",", ".");
                if (newPositionItem.OptionTvUc != oldPositionItem.OptionTvUc) UpdateString += ", [Option TV]=" + newPositionItem.OptionTvUc.ToString().Replace(",", ".");
                if (newPositionItem.TotalPl != oldPositionItem.TotalPl) UpdateString += ", [Total P/L]=" + newPositionItem.TotalPl.ToString().Replace(",", ".");
                if (newPositionItem.TotalPlAdmin != oldPositionItem.TotalPlAdmin) UpdateString += ", [Total P/L Admin]=" + newPositionItem.TotalPlAdmin.ToString().Replace(",", ".");
                if (newPositionItem.UnderlyingLast != oldPositionItem.UnderlyingLast) UpdateString += ", [Underlying Last]=" + newPositionItem.UnderlyingLast.ToString().Replace(",", ".");
                if (newPositionItem.AssetUcAdmin != oldPositionItem.AssetUcAdmin) UpdateString += ", [Asset uC Admin]=" + newPositionItem.AssetUcAdmin.ToString().Replace(",", ".");
            }

            if (newPositionItem.SpotCloseUsd != oldPositionItem.SpotCloseUsd) UpdateString += ", [Spot USD Close]=" + newPositionItem.SpotCloseUsd.ToString().Replace(",", ".");
            if (newPositionItem.SpotUsd != oldPositionItem.SpotUsd) UpdateString += ", [Spot USD]=" + newPositionItem.SpotUsd.ToString().Replace(",", ".");
            if (newPositionItem.ClosePc != oldPositionItem.ClosePc) UpdateString += ", [Close pC]=" + newPositionItem.ClosePc.ToString().Replace(",", ".");
            if (newPositionItem.DisplayClosePc != oldPositionItem.DisplayClosePc) UpdateString += ", [Display Close pc]=" + newPositionItem.DisplayClosePc.ToString().Replace(",", ".");
            if (newPositionItem.ClosePcAdmin != oldPositionItem.ClosePcAdmin) UpdateString += ", [Close pC Admin]=" + newPositionItem.ClosePcAdmin.ToString().Replace(",", ".");
            if (newPositionItem.Close != oldPositionItem.Close) UpdateString += ", [Close]=" + newPositionItem.Close.ToString().Replace(",", ".");
            if (newPositionItem.CloseAdmin != oldPositionItem.CloseAdmin) UpdateString += ", [Close Admin]=" + newPositionItem.CloseAdmin.ToString().Replace(",", ".");
            if (newPositionItem.DisplayClose != oldPositionItem.DisplayClose) UpdateString += ", [Display Close]=" + newPositionItem.DisplayClose.ToString().Replace(",", ".");
            if (newPositionItem.SourceLast != oldPositionItem.SourceLast) UpdateString += ", [Source Last]='" + newPositionItem.SourceLast + "'";
            if (newPositionItem.SourceLastAdmin != oldPositionItem.SourceLastAdmin) UpdateString += ", [Source Last Admin]='" + newPositionItem.SourceLastAdmin + "'";
            if (newPositionItem.SourceClose != oldPositionItem.SourceClose) UpdateString += ", [Source Close]='" + newPositionItem.SourceClose + "'";
            if (newPositionItem.SourceCloseAdmin != oldPositionItem.SourceCloseAdmin) UpdateString += ", [Source Close Admin]='" + newPositionItem.SourceCloseAdmin + "'";
            if (newPositionItem.Side != oldPositionItem.Side) UpdateString += ", [Side]='" + newPositionItem.Side + "'";
            if (newPositionItem.DeltaSide != oldPositionItem.DeltaSide) UpdateString += ", [Delta Side]='" + newPositionItem.DeltaSide + "'";
            if (newPositionItem.BookNav != oldPositionItem.BookNav) UpdateString += ", [Book NAV]=" + newPositionItem.BookNav.ToString().Replace(",", ".");
            if (newPositionItem.CashFlow != oldPositionItem.CashFlow) UpdateString += ", [Cash FLow]=" + newPositionItem.CashFlow.ToString().Replace(",", ".");
            if (newPositionItem.GammaCash != oldPositionItem.GammaCash) UpdateString += ", [Gamma Cash]=" + newPositionItem.GammaCash.ToString().Replace(",", ".");
            if (newPositionItem.GammaNav != oldPositionItem.GammaNav) UpdateString += ", [Gamma/NAV]=" + newPositionItem.GammaNav.ToString().Replace(",", ".");
            if (newPositionItem.Dividends != oldPositionItem.Dividends) UpdateString += ", [Dividends]=" + newPositionItem.Dividends.ToString().Replace(",", ".");
            if (newPositionItem.Description != oldPositionItem.Description) UpdateString += ", [Description]='" + newPositionItem.Description + "'";
            if (newPositionItem.PriceTable != oldPositionItem.PriceTable) UpdateString += ", [Price Table]='" + newPositionItem.PriceTable + "'";
            if (newPositionItem.SecurityCurrency != oldPositionItem.SecurityCurrency) UpdateString += ", [Security Currency]='" + newPositionItem.SecurityCurrency + "'";
            if (newPositionItem.Instrument != oldPositionItem.Instrument) UpdateString += ", [Instrument]='" + newPositionItem.Instrument + "'";
            if (newPositionItem.AssetClass != oldPositionItem.AssetClass) UpdateString += ", [Asset Class]='" + newPositionItem.AssetClass + "'";
            if (newPositionItem.SubIndustry != oldPositionItem.SubIndustry) UpdateString += ", [Sub Industry]='" + newPositionItem.SubIndustry + "'";
            if (newPositionItem.Industry != oldPositionItem.Industry) UpdateString += ", [Industry]='" + newPositionItem.Industry + "'";
            if (newPositionItem.IndustryGroup != oldPositionItem.IndustryGroup) UpdateString += ", [Industry Group]='" + newPositionItem.IndustryGroup + "'";
            if (newPositionItem.Sector != oldPositionItem.Sector) UpdateString += ", [Sector]='" + newPositionItem.Sector + "'";
            if (newPositionItem.UnderlyingCountry != oldPositionItem.UnderlyingCountry) UpdateString += ", [Underlying Country]='" + newPositionItem.UnderlyingCountry + "'";
            if (newPositionItem.NestSector != oldPositionItem.NestSector) UpdateString += ", [Nest Sector]='" + newPositionItem.NestSector + "'";
            if (newPositionItem.Underlying != oldPositionItem.Underlying) UpdateString += ", [Underlying]='" + newPositionItem.Underlying + "'";
            if (newPositionItem.SubPortfolio != oldPositionItem.SubPortfolio) UpdateString += ", [Sub Portfolio]='" + newPositionItem.SubPortfolio + "'";
            if (newPositionItem.Strategy != oldPositionItem.Strategy) UpdateString += ", [New Strategy]='" + newPositionItem.Strategy + "'";
            if (newPositionItem.SubStrategy != oldPositionItem.SubStrategy) UpdateString += ", [New Sub Strategy]='" + newPositionItem.SubStrategy + "'";
            if (newPositionItem.Portfolio != oldPositionItem.Portfolio) UpdateString += ", [Portfolio]='" + newPositionItem.Portfolio + "'";
            if (newPositionItem.ExchangeTicker != oldPositionItem.ExchangeTicker) UpdateString += ", [Exchange Ticker]='" + newPositionItem.ExchangeTicker + "'";
            if (newPositionItem.IdInstrument != oldPositionItem.IdInstrument) UpdateString += ", [Id Instrument]=" + newPositionItem.IdInstrument.ToString().Replace(",", ".");
            if (newPositionItem.IdUnderlying != oldPositionItem.IdUnderlying) UpdateString += ", [Id Underlying]=" + newPositionItem.IdUnderlying.ToString().Replace(",", ".");
            if (newPositionItem.IdAccount != oldPositionItem.IdAccount) UpdateString += ", [Id Account]=" + newPositionItem.IdAccount.ToString().Replace(",", ".");
            if (newPositionItem.IdAdministrator != oldPositionItem.IdAdministrator) UpdateString += ", [Id Administrator]=" + newPositionItem.IdAdministrator.ToString().Replace(",", ".");
            if (newPositionItem.IdBaseUnderlying != oldPositionItem.IdBaseUnderlying) UpdateString += ", [Id Base Underlying]=" + newPositionItem.IdBaseUnderlying.ToString().Replace(",", ".");
            if (newPositionItem.IdBaseUnderlyingCurrency != oldPositionItem.IdBaseUnderlyingCurrency) UpdateString += ", [Id Base Underlying Currency]=" + newPositionItem.IdBaseUnderlyingCurrency.ToString().Replace(",", ".");
            if (newPositionItem.IdSourceClose != oldPositionItem.IdSourceClose) UpdateString += ", [Id Source Close]=" + newPositionItem.IdSourceClose.ToString().Replace(",", ".");
            if (newPositionItem.IdSourceCloseAdmin != oldPositionItem.IdSourceCloseAdmin) UpdateString += ", [Id Source Close Admin]=" + newPositionItem.IdSourceCloseAdmin.ToString().Replace(",", ".");
            if (newPositionItem.IdSourceLast != oldPositionItem.IdSourceLast) UpdateString += ", [Id Source Last]=" + newPositionItem.IdSourceLast.ToString().Replace(",", ".");
            if (newPositionItem.IdSourceLastAdmin != oldPositionItem.IdSourceLastAdmin) UpdateString += ", [Id Source Last Admin]=" + newPositionItem.IdSourceLastAdmin.ToString().Replace(",", ".");
            if (newPositionItem.IdSubStrategy != oldPositionItem.IdSubStrategy) UpdateString += ", [New Id Sub Strategy]=" + newPositionItem.IdSubStrategy.ToString().Replace(",", ".");
            if (newPositionItem.IdTickerCash != oldPositionItem.IdTickerCash) UpdateString += ", [Id Ticker Cash]=" + newPositionItem.IdTickerCash.ToString().Replace(",", ".");
            if (newPositionItem.IdForward != oldPositionItem.IdForward) UpdateString += ", [Id Forward]=" + newPositionItem.IdForward.ToString().Replace(",", ".");
            if (newPositionItem.IdAssetClass != oldPositionItem.IdAssetClass) UpdateString += ", [Id Asset Class]=" + newPositionItem.IdAssetClass.ToString().Replace(",", ".");
            if (newPositionItem.IdPriceTable != oldPositionItem.IdPriceTable) UpdateString += ", [Id Price Table]=" + newPositionItem.IdPriceTable.ToString().Replace(",", ".");
            if (newPositionItem.IdSecurityCurrency != oldPositionItem.IdSecurityCurrency) UpdateString += ", [Id Currency Ticker]=" + newPositionItem.IdSecurityCurrency.ToString().Replace(",", ".");
            if (newPositionItem.IdStrategy != oldPositionItem.IdStrategy) UpdateString += ", [New Id Strategy]=" + newPositionItem.IdStrategy.ToString().Replace(",", ".");
            if (newPositionItem.IdSubPortfolio != oldPositionItem.IdSubPortfolio) UpdateString += ", [Id Sub Portfolio]=" + newPositionItem.IdSubPortfolio.ToString().Replace(",", ".");
            if (newPositionItem.InitialPosition != oldPositionItem.InitialPosition) UpdateString += ", [Initial Position]=" + newPositionItem.InitialPosition.ToString().Replace(",", ".");
            if (newPositionItem.FlagLast != oldPositionItem.FlagLast) UpdateString += ", [Flag Last]='" + newPositionItem.FlagLast + "'";
            if (newPositionItem.FlagLastAdmin != oldPositionItem.FlagLastAdmin) UpdateString += ", [Flag Last Admin]='" + newPositionItem.FlagLastAdmin + "'";
            if (newPositionItem.VolDate != oldPositionItem.VolDate) UpdateString += ", [Vol Date]='" + newPositionItem.VolDate.ToString("yyyy-MM-dd") + "'";
            if (newPositionItem.DateClose != oldPositionItem.DateClose) UpdateString += ", [Close_Date]='" + newPositionItem.DateClose.ToString("yyyy-MM-dd") + "'";
            if (newPositionItem.DurationDate != oldPositionItem.DurationDate) UpdateString += ", [Duration Date]='" + newPositionItem.DurationDate.ToString("yyyy-MM-dd") + "'";
            if (newPositionItem.DataPosicao != oldPositionItem.DataPosicao) UpdateString += ", [Last Position]='" + newPositionItem.DataPosicao.ToString("yyyy-MM-dd") + "'";
            if (newPositionItem.Expiration != oldPositionItem.Expiration) UpdateString += ", [Expiration]='" + newPositionItem.Expiration.ToString("yyyy-MM-dd") + "'";
            if (newPositionItem.Section != oldPositionItem.Section) UpdateString += ", [Section]='" + newPositionItem.Section + "'";
            if (newPositionItem.Book != oldPositionItem.Book) UpdateString += ", [Book]='" + newPositionItem.Book + "'";
            if (newPositionItem.DaysToExpiration != oldPositionItem.DaysToExpiration) UpdateString += ", [Days to Expiration]=" + newPositionItem.DaysToExpiration.ToString().Replace(",", ".");
            if (newPositionItem.OptionType != oldPositionItem.OptionType) UpdateString += ", [Option Type]=" + newPositionItem.OptionType.ToString().Replace(",", ".");
            if (newPositionItem.IdPortCurrency != oldPositionItem.IdPortCurrency) UpdateString += ", [Portfolio Currency]=" + newPositionItem.IdPortCurrency.ToString().Replace(",", ".");
            if (newPositionItem.AdjFwPosition != oldPositionItem.AdjFwPosition) UpdateString += ", [Adjusted Fw Position]=" + newPositionItem.AdjFwPosition.ToString().Replace(",", ".");
            if (newPositionItem.AvVolume6m != oldPositionItem.AvVolume6m) UpdateString += ", [6m Av Volume]=" + newPositionItem.AvVolume6m.ToString().Replace(",", ".");
            if (newPositionItem.FwdAdjPl != oldPositionItem.FwdAdjPl) UpdateString += ", [FWD Adj P/L]=" + newPositionItem.FwdAdjPl.ToString().Replace(",", ".");
            if (newPositionItem.FwdPrice != oldPositionItem.FwdPrice) UpdateString += ", [FWD Price]=" + newPositionItem.FwdPrice.ToString().Replace(",", ".");
            if (newPositionItem.FwdAdjPrice != oldPositionItem.FwdAdjPrice) UpdateString += ", [FWD Adj Price]=" + newPositionItem.FwdAdjPrice.ToString().Replace(",", ".");
            if (newPositionItem.CurrencyChange != oldPositionItem.CurrencyChange) UpdateString += ", [Currency Chg]=" + newPositionItem.CurrencyChange.ToString().Replace(",", ".");
            if (newPositionItem.CurrencyPl != oldPositionItem.CurrencyPl) UpdateString += ", [Currency P/L]=" + newPositionItem.CurrencyPl.ToString().Replace(",", ".");
            if (newPositionItem.CurrencyPlAdmin != oldPositionItem.CurrencyPlAdmin) UpdateString += ", [Currency P/L Admin]=" + newPositionItem.CurrencyPlAdmin.ToString().Replace(",", ".");
            if (newPositionItem.Delta != oldPositionItem.Delta) UpdateString += ", [Delta]=" + newPositionItem.Delta.ToString().Replace(",", ".");
            if (newPositionItem.Duration != oldPositionItem.Duration) UpdateString += ", [Duration]=" + newPositionItem.Duration.ToString().Replace(",", ".");
            if (newPositionItem.FlagCloseAdmin != oldPositionItem.FlagCloseAdmin) UpdateString += ", [Flag Close Admin]=" + newPositionItem.FlagCloseAdmin.ToString().Replace(",", ".");
            if (newPositionItem.FwValue != oldPositionItem.FwValue) UpdateString += ", [FWValue]=" + newPositionItem.FwValue.ToString().Replace(",", ".");
            if (newPositionItem.Gamma != oldPositionItem.Gamma) UpdateString += ", [Gamma]=" + newPositionItem.Gamma.ToString().Replace(",", ".");
            if (newPositionItem.LoanRateInCblc != oldPositionItem.LoanRateInCblc) UpdateString += ", [Loan Rate In CBLC]=" + newPositionItem.LoanRateInCblc.ToString().Replace(",", ".");
            if (newPositionItem.LoanRateOutCblc != oldPositionItem.LoanRateOutCblc) UpdateString += ", [Loan Rate Out CBLC]=" + newPositionItem.LoanRateOutCblc.ToString().Replace(",", ".");
            if (newPositionItem.LoanMktTotal != oldPositionItem.LoanMktTotal) UpdateString += ", [Loan MKT Total]=" + newPositionItem.LoanMktTotal.ToString().Replace(",", ".");
            if (newPositionItem.LotSize != oldPositionItem.LotSize) UpdateString += ", [Lot Size]=" + newPositionItem.LotSize.ToString().Replace(",", ".");
            if (newPositionItem.MktCap != oldPositionItem.MktCap) UpdateString += ", [Market Cap]=" + newPositionItem.MktCap.ToString().Replace(",", ".");
            if (newPositionItem.Nav != oldPositionItem.Nav) UpdateString += ", [NAV]=" + newPositionItem.Nav.ToString().Replace(",", ".");
            if (newPositionItem.NavPc != oldPositionItem.NavPc) UpdateString += ", [NAV pC]=" + newPositionItem.NavPc.ToString().Replace(",", ".");
            if (newPositionItem.PrevAssetPl != oldPositionItem.PrevAssetPl) UpdateString += ", [Prev Asset P/L]=" + newPositionItem.PrevAssetPl.ToString().Replace(",", ".");
            if (newPositionItem.PrevAssetPlAdmin != oldPositionItem.PrevAssetPlAdmin) UpdateString += ", [Prev Asset P/L Admin]=" + newPositionItem.PrevAssetPlAdmin.ToString().Replace(",", ".");
            if (newPositionItem.PrevCashUc != oldPositionItem.PrevCashUc) UpdateString += ", [Prev Cash uC]=" + newPositionItem.PrevCashUc.ToString().Replace(",", ".");
            if (newPositionItem.PrevCashUcAdmin != oldPositionItem.PrevCashUcAdmin) UpdateString += ", [Prev Cash uC Admin]=" + newPositionItem.PrevCashUcAdmin.ToString().Replace(",", ".");
            if (newPositionItem.RatePeriod != oldPositionItem.RatePeriod) UpdateString += ", [Rate Period]=" + newPositionItem.RatePeriod.ToString().Replace(",", ".");
            if (newPositionItem.RateYear != oldPositionItem.RateYear) UpdateString += ", [Rate Year]=" + newPositionItem.RateYear.ToString().Replace(",", ".");
            if (newPositionItem.Realized != oldPositionItem.Realized) UpdateString += ", [Realized]=" + newPositionItem.Realized.ToString().Replace(",", ".");
            if (newPositionItem.RealizedAdmin != oldPositionItem.RealizedAdmin) UpdateString += ", [Realized Admin]=" + newPositionItem.RealizedAdmin.ToString().Replace(",", ".");
            if (newPositionItem.Rho != oldPositionItem.Rho) UpdateString += ", [Rho]=" + newPositionItem.Rho.ToString().Replace(",", ".");
            if (newPositionItem.StrategyPercent != oldPositionItem.StrategyPercent) UpdateString += ", [Strategy %]=" + newPositionItem.StrategyPercent.ToString().Replace(",", ".");
            if (newPositionItem.Spot != oldPositionItem.Spot) UpdateString += ", [Spot]=" + newPositionItem.Spot.ToString().Replace(",", ".");
            if (newPositionItem.SpotClose != oldPositionItem.SpotClose) UpdateString += ", [Spot Close]=" + newPositionItem.SpotClose.ToString().Replace(",", ".");
            if (newPositionItem.Strike != oldPositionItem.Strike) UpdateString += ", [Strike]=" + newPositionItem.Strike.ToString().Replace(",", ".");
            if (newPositionItem.Theta != oldPositionItem.Theta) UpdateString += ", [Theta]=" + newPositionItem.Theta.ToString().Replace(",", ".");
            if (newPositionItem.ThetaNav != oldPositionItem.ThetaNav) UpdateString += ", [Theta/NAV]=" + newPositionItem.ThetaNav.ToString().Replace(",", ".");
            if (newPositionItem.Vega != oldPositionItem.Vega) UpdateString += ", [Vega]=" + newPositionItem.Vega.ToString().Replace(",", ".");
            if (newPositionItem.Volatility != oldPositionItem.Volatility) UpdateString += ", [Volatility]=" + newPositionItem.Volatility.ToString().Replace(",", ".");
            if (newPositionItem.YearFraction != oldPositionItem.YearFraction) UpdateString += ", [Time to Expiration]=" + newPositionItem.YearFraction.ToString().Replace(",", ".");
            if (newPositionItem.BaseUnderlyingCurrency != oldPositionItem.BaseUnderlyingCurrency) UpdateString += ", [Base Underlying Currency]='" + newPositionItem.BaseUnderlyingCurrency + "'";
            if (newPositionItem.BaseUnderlying != oldPositionItem.BaseUnderlying) UpdateString += ", [Base Underlying]='" + newPositionItem.BaseUnderlying + "'";
            if (newPositionItem.VolFlag != oldPositionItem.VolFlag) UpdateString += ", [Vol Flag]='" + newPositionItem.VolFlag + "'";
            if (newPositionItem.NestTicker != oldPositionItem.NestTicker) UpdateString += ", [Ticker]='" + newPositionItem.NestTicker + "'";


            if (UpdateString.Length > 1)
            {
                UpdateString = UpdateString.Substring(1);
            }

            return UpdateString;
        }
        /*
                public string GetInitialString(UpdateType UpdateType)
                {
                    string UpdateString = "";

                    UpdateString += "[Id Instrument]=" + this.IdInstrument;

                    if (UpdateType == UpdateType.AllFields)
                    {
                        UpdateString += ", [Id Administrator]=" + this.IdAdministrator;
                        UpdateString += ", [Id Asset Class]=" + this.IdAssetClass;
                        UpdateString += ", [Id Source Close]=" + this.IdSourceClose.ToString().Replace(",", ".");
                        UpdateString += ", [Id Source Close Admin]=" + this.IdSourceCloseAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Id Base Underlying] =" + this.IdBaseUnderlying.ToString().Replace(",", ".");
                        UpdateString += ", [Id Base Underlying Currency] =" + this.IdBaseUnderlyingCurrency.ToString().Replace(",", ".");
                        UpdateString += ", [Id Source Last] =" + this.IdSourceLast.ToString().Replace(",", ".");
                        UpdateString += ", [Id Source Last Admin] =" + this.IdSourceLastAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Id Account] =" + this.IdAccount.ToString().Replace(",", ".");
                        UpdateString += ", [Id Ticker Cash] =" + this.IdTickerCash.ToString().Replace(",", ".");
                        UpdateString += ", [Id Forward] =" + this.IdForward.ToString().Replace(",", ".");
                        UpdateString += ", [Id Currency Ticker] = '" + this.IdSecurityCurrency + "'";
                        UpdateString += ", [Id Sub Portfolio] =" + this.IdSubPortfolio.ToString().Replace(",", ".");
                        UpdateString += ", [Id Price Table] =" + this.IdPriceTable.ToString().Replace(",", ".");
                        UpdateString += ", [Id Underlying] =" + this.IdUnderlying.ToString().Replace(",", ".");
                        UpdateString += ", [New Id Strategy] =" + this.IdStrategy.ToString().Replace(",", ".");
                        UpdateString += ", [New Id Sub Strategy] =" + this.IdSubStrategy.ToString().Replace(",", ".");

                        UpdateString += ", [Last Position] ='" + this.DateClose.ToString("yyyy-MM-dd") + "'";
                        UpdateString += ", Close_Date='" + this.DateClose.ToString("yyyy-MM-dd") + "'";

                        UpdateString += ", [Portfolio Currency] = " + this.IdPortCurrency.ToString().Replace(",", ".") + "";
                        UpdateString += ", [Strike] =" + this.Strike.ToString().Replace(",", ".");
                        UpdateString += ", [Lot Size] =" + this.LotSize.ToString().Replace(",", ".");
                        UpdateString += ", [Expiration] ='" + this.Expiration.ToString("yyyy-MM-dd") + "'";
                        UpdateString += ", [Days to Expiration] =" + this.DaysToExpiration.ToString().Replace(",", ".");
                        UpdateString += ", [Time to Expiration] =" + this.YearFraction.ToString().Replace(",", ".");
                        UpdateString += ", [Market Cap] =" + this.MktCap.ToString().Replace(",", ".");
                        UpdateString += ", [6m Av Volume] =" + this.AvVolume6m.ToString().Replace(",", ".");
                        UpdateString += ", [Loan Rate In CBLC] =" + this.LoanRateInCblc.ToString().Replace(",", ".");
                        UpdateString += ", [Loan Rate Out CBLC] =" + this.LoanRateOutCblc.ToString().Replace(",", ".");
                        UpdateString += ", [Loan MKT Total] =" + this.LoanMktTotal.ToString().Replace(",", ".");
                        UpdateString += ", [Option Type] =" + this.OptionType.ToString().Replace(",", ".");

                        UpdateString += ", [Prev Cash uC Admin]=" + this.PrevCashUcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Close]=" + this.Close.ToString().Replace(",", ".");
                        UpdateString += ", [Close Admin]=" + this.CloseAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Flag Close Admin]='" + this.FlagCloseAdmin + "'";
                        UpdateString += ", [Initial Position]=" + this.InitialPosition.ToString().Replace(",", ".");
                        UpdateString += ", [Prev Cash uC]=" + this.PrevCashUc.ToString().Replace(",", ".");
                        UpdateString += ", [Prev Asset P/L]=" + this.PrevAssetPl.ToString().Replace(",", ".");
                        UpdateString += ", [Prev Asset P/L Admin]=" + this.PrevAssetPlAdmin.ToString().Replace(",", ".");
                        UpdateString += ", NAV =" + this.Nav.ToString().Replace(",", ".");
                        UpdateString += ", [Book NAV] =" + this.BookNav.ToString().Replace(",", ".");
                        UpdateString += ", [NAV pC]=" + this.NavPc.ToString().Replace(",", ".");
                        UpdateString += ", [Spot Close] =" + this.SpotClose.ToString().Replace(",", ".");
                        UpdateString += ", [Spot USD Close] =" + this.SpotCloseUsd.ToString().Replace(",", ".");
                        UpdateString += ", [Close pC] =" + this.ClosePc.ToString().Replace(",", ".");
                        UpdateString += ", [Display Close] =" + this.DisplayClose.ToString().Replace(",", ".");
                        UpdateString += ", [Display Close pc] =" + this.DisplayClosePc.ToString().Replace(",", ".");
                        UpdateString += ", [Close pC Admin] =" + this.ClosePcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Dividends]=" + this.Dividends.ToString().Replace(",", ".");
                        UpdateString += ", [Rate Year] =" + this.RateYear.ToString().Replace(",", ".");

                        UpdateString += ", [Spot]=" + this.Spot.ToString().Replace(",", ".");
                        UpdateString += ", [Spot USD] =" + this.SpotUsd.ToString().Replace(",", ".");
                        UpdateString += ", [Brokerage] = 0";
                        UpdateString += ", [Volatility] = " + this.Volatility.ToString().Replace(",", ".");
                        UpdateString += ", [Vol Date] = '" + this.VolDate.ToString("yyyy-MM-dd") + "'";
                        UpdateString += ", [Rate Period] =" + this.RatePeriod.ToString().Replace(",", ".");
                        UpdateString += ", [Underlying Last] = COALESCE(" + this.UnderlyingLast.ToString().Replace(",", ".") + ",0)";
                        UpdateString += ", [Last Calc] ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        UpdateString += ", [Theta/NAV] =" + this.ThetaNav.ToString().Replace(",", ".");
                        UpdateString += ", [Duration] =" + this.Duration.ToString().Replace(",", ".");
                        UpdateString += ", [Duration Date] ='" + this.DurationDate.ToString("yyyy-MM-dd") + "'";
                        UpdateString += ", [10Y Equiv DNAV] =" + this.TenYearEquivDnav.ToString().Replace(",", ".");
                        UpdateString += ", [Strategy %] =" + this.StrategyPercent.ToString().Replace(",", ".");
                        UpdateString += ", [Loan Book Contribution] = " + this.LoanBookContribution.ToString().Replace(",", ".");

                        UpdateString += ", [FWValue] =" + this.FwValue.ToString().Replace(",", ".");
                        UpdateString += ", [FWD Adj P/L] =" + this.FwdAdjPl.ToString().Replace(",", ".");
                        UpdateString += ", [FWD Price] =" + this.FwdPrice.ToString().Replace(",", ".");
                        UpdateString += ", [FWD Adj Price] =" + this.FwdAdjPrice.ToString().Replace(",", ".");
                        UpdateString += ", [Adjusted Fw Position]=" + this.AdjFwPosition.ToString().Replace(",", ".");

                        #region String Columns
                        UpdateString += ", [Vol Flag] =isnull( '" + this.VolFlag + "','0')";
                        UpdateString += ", [Base Underlying] ='" + this.BaseUnderlying + "'";
                        UpdateString += ", [Base Underlying Currency] ='" + this.BaseUnderlyingCurrency + "'";
                        UpdateString += ", [Source Last] = LEFT('" + this.SourceLast + "',6)";
                        UpdateString += ", [Flag Last] = LEFT('" + this.FlagLast + "',15)";
                        UpdateString += ", [Source Last Admin] = LEFT('" + this.SourceLastAdmin + "',6)";
                        UpdateString += ", [Flag Last Admin] = LEFT('" + this.FlagLastAdmin + "',15)";
                        UpdateString += ", [Source Close] ='" + this.SourceClose + "'";
                        UpdateString += ", [Source Close Admin] ='" + this.SourceCloseAdmin + "'";
                        UpdateString += ", [Instrument] = '" + this.Instrument + "'";
                        UpdateString += ", [Asset Class] = '" + this.AssetClass + "'";
                        UpdateString += ", [Sub Industry] = '" + this.SubIndustry + "'";
                        UpdateString += ", [Description] = '" + this.Description + "'";
                        UpdateString += ", [Security Currency] = '" + this.SecurityCurrency + "'";
                        UpdateString += ", [Industry] = '" + this.Industry + "'";
                        UpdateString += ", [Industry Group] = '" + this.IndustryGroup + "'";
                        UpdateString += ", [Sector] = '" + this.Sector + "'";
                        UpdateString += ", [Underlying Country] = '" + this.UnderlyingCountry + "'";
                        UpdateString += ", [Nest Sector] = '" + this.NestSector + "'";
                        UpdateString += ", [Portfolio] = '" + this.Portfolio + "'";
                        UpdateString += ", [Ticker] = '" + this.NestTicker + "'";
                        UpdateString += ", [Price Table] = '" + this.PriceTable + "'";
                        UpdateString += ", [Underlying] = '" + this.Underlying + "'";
                        UpdateString += ", [Book] = '" + this.Book + "'";
                        UpdateString += ", [Section] = '" + this.Section + "'";
                        UpdateString += ", [Sub Portfolio] = '" + this.SubPortfolio + "'";
                        UpdateString += ", [New Strategy] = '" + this.Strategy + "'";
                        UpdateString += ", [New Sub Strategy] = '" + this.SubStrategy + "'";
                        UpdateString += ", [Exchange Ticker] = '" + this.ExchangeTicker + "'";
                        #endregion
                    }

                    if (UpdateType == UpdateType.RTQuantity || UpdateType == UpdateType.AllFields)
                    {
                        UpdateString += ", [Quantity Bought]=" + this.QuantBoughtSum.ToString().Replace(",", ".");
                        UpdateString += ", [Quantity Sold]=" + this.QuantSoldSum.ToString().Replace(",", ".");
                        UpdateString += ", Position=" + this.CurrentPosition.ToString().Replace(",", ".");
                        UpdateString += ", [Delta Quantity] =" + this.Delta.ToString().Replace(",", ".") + " * Position";

                        UpdateString += ", [Cost Close]=" + this.CostClose.ToString().Replace(",", ".");
                        UpdateString += ", [Cost Close Admin]=" + this.CostCloseAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Trade Flow]=" + this.TradeFlow.ToString().Replace(",", ".");
                        UpdateString += ", [Cost Close pC] =" + this.CostClosePC.ToString().Replace(",", ".");
                        UpdateString += ", [Display Cost Close] =" + this.DisplayClose.ToString().Replace(",", ".");
                        UpdateString += ", [Display Cost Close pc] =" + this.DisplayCostClosePc.ToString().Replace(",", ".");
                        UpdateString += ", [Cost Close pC Admin] =" + this.CostClosePcAdmin.ToString().Replace(",", ".");

                        UpdateString += ", [Gamma Quantity] =" + this.GammaQuantity.ToString().Replace(",", ".");
                        UpdateString += ", [Option TV Cash pC] =" + this.OptionTvCashPc.ToString().Replace(",", ".");
                        UpdateString += ", [Days To Liquidity] =" + this.DaysToLiquidity.ToString().Replace(",", ".");
                        UpdateString += ", [Loan Potential Gain] =" + this.LoanPotentialGain.ToString().Replace(",", ".");
                        UpdateString += ", [Loan Potential Book Contribution] =" + this.LoanPotentialBookContribution.ToString().Replace(",", ".");
                        UpdateString += ", [Loan Marginal Gain] =" + this.LoanMarginalGain.ToString().Replace(",", "."); // verificar
                        UpdateString += ", [Loan Marginal Book Contribution] =" + this.LoanMarginalBookContribution.ToString().Replace(",", "."); // verificar
                    }

                    if (UpdateType == UpdateType.RTPrice || UpdateType == UpdateType.AllFields)
                    {
                        UpdateString += ", [% to Bid/Ask] =" + this.PercentBidAsk.ToString().Replace(",", ".");
                        UpdateString += ", [Last]= " + this.Last.ToString().Replace(",", ".");
                        UpdateString += ", [Bid] =" + this.Bid.ToString().Replace(",", ".");
                        UpdateString += ", [Ask] =" + this.Ask.ToString().Replace(",", ".");
                        UpdateString += ", [Display Last] = " + this.DisplayLast.ToString().Replace(",", ".");
                        UpdateString += ", [Display Last pC] =" + this.DisplayLastPc.ToString().Replace(",", ".");
                        UpdateString += ", [Last pC] =" + this.LastPc.ToString().Replace(",", ".");
                        UpdateString += ", [Last Admin] =" + this.LastUcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Last pC Admin] =" + this.LastPcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [UpdTime Last] ='" + this.UpdTimeLast.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        UpdateString += ", [UpdTime Last Admin] ='" + this.UpdTimeLastAdmin.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    }

                    if (UpdateType == UpdateType.RTQuantity || UpdateType == UpdateType.RTPrice || UpdateType == UpdateType.AllFields)
                    {
                        UpdateString += ", [Cash] =" + this.Cash.ToString().Replace(",", ".");
                        UpdateString += ", [Cash Admin] =" + this.CashAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Delta]=" + this.Delta.ToString().Replace(",", ".");
                        UpdateString += ", [Notional] =" + this.Notional.ToString().Replace(",", ".");
                        UpdateString += ", [Cash uC] =" + this.CashUc.ToString().Replace(",", ".");
                        UpdateString += ", [Delta Cash] =" + this.DeltaCash.ToString().Replace(",", ".");
                        UpdateString += ", [Gamma Cash] =" + this.GammaCash.ToString().Replace(",", ".");
                        UpdateString += ", [Cash/NAV] =" + this.CashNav.ToString().Replace(",", ".");
                        UpdateString += ", [BRL] =" + this.Brl.ToString().Replace(",", ".");
                        UpdateString += ", [Delta/NAV] =" + this.DeltaNav.ToString().Replace(",", ".");
                        UpdateString += ", [Gamma/NAV] =" + this.GammaNav.ToString().Replace(",", ".");
                        UpdateString += ", [BRL/NAV] =" + this.BrlNav.ToString().Replace(",", ".");
                        UpdateString += ", [Long] =" + this.Long.ToString().Replace(",", ".");
                        UpdateString += ", [Short] =" + this.Short.ToString().Replace(",", ".");
                        UpdateString += ", [Gross] =" + this.Gross.ToString().Replace(",", ".");
                        UpdateString += ", [Long Book] =" + this.LongBook.ToString().Replace(",", ".");
                        UpdateString += ", [Short Book] =" + this.ShortBook.ToString().Replace(",", ".");
                        UpdateString += ", [Gross Book] =" + this.GrossBook.ToString().Replace(",", ".");
                        UpdateString += ", [Gross Delta] =" + this.GrossDelta.ToString().Replace(",", ".");
                        UpdateString += ", [Delta/Book] =" + this.DeltaBook.ToString().Replace(",", ".");
                        UpdateString += ", [Side] ='" + this.Side + "'";
                        UpdateString += ", [Delta Side] ='" + this.DeltaSide + "'";
                        UpdateString += ", [Long Delta] =" + this.LongDelta.ToString().Replace(",", ".");
                        UpdateString += ", [Short Delta] =" + this.ShortDelta.ToString().Replace(",", ".");

                        UpdateString += ", [Currency P/L] =" + this.CurrencyPl.ToString().Replace(",", ".");
                        UpdateString += ", [Currency P/L Admin] =" + this.CurrencyPlAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Currency Chg] =" + this.CurrencyChange.ToString().Replace(",", ".");
                        UpdateString += ", [Currency Contribution] =" + this.CurrencyContribution.ToString().Replace(",", ".");
                        UpdateString += ", [Cash Premium] =" + this.CashPremium.ToString().Replace(",", ".");
                        UpdateString += ", [Total P/L Admin] =" + this.TotalPlAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Total P/L] =" + this.TotalPl.ToString().Replace(",", ".");
                        UpdateString += ", [Cash FLow]=" + this.CashFlow.ToString().Replace(",", ".");
                        UpdateString += ", [ASSET uC] =" + this.AssetUc.ToString().Replace(",", ".");
                        UpdateString += ", [ASSET pC] =" + this.AssetPc.ToString().Replace(",", ".");
                        UpdateString += ", [Contribution pC] =" + this.ContributionPc.ToString().Replace(",", ".");
                        UpdateString += ", [Contribution uC] =" + this.ContributionUc.ToString().Replace(",", ".");
                        UpdateString += ", [Realized Admin]=" + this.RealizedAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [ASSET P/L pC] =" + this.AssetPlPc.ToString().Replace(",", ".");
                        UpdateString += ", [ASSET P/L uC] =" + this.AssetPlUc.ToString().Replace(",", ".");
                        UpdateString += ", [Asset Book Contribution] =" + this.AssetBookContribution.ToString().Replace(",", ".");
                        UpdateString += ", [Currency Book Contribution] =" + this.CurrencyBookContribution.ToString().Replace(",", ".");
                        UpdateString += ", [Contribution pC Book] =" + this.ContributionPcBook.ToString().Replace(",", ".");
                        UpdateString += ", [CX] =" + this.Cash.ToString().Replace(",", ".");
                        UpdateString += ", [CXBOOK] =" + this.CxBook.ToString().Replace(",", ".");
                        UpdateString += ", [P/L %] =" + this.PlPerc.ToString().Replace(",", ".");
                        UpdateString += ", [Realized]=" + this.Realized.ToString().Replace(",", ".");
                        UpdateString += ", [Asset P/L pC Admin] =" + this.AssetPlPcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Contribution pC Admin] =" + this.ContributionAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Asset uC Admin] =" + this.AssetUcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Cash uC Admin] =" + this.CashUcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Asset PL uC Admin] =" + this.AssetPlUcAdmin.ToString().Replace(",", ".");
                        UpdateString += ", [Asset Contribution] =" + this.AssetContribution.ToString().Replace(",", ".");
                        UpdateString += ", [Dif Contrib] = COALESCE(" + this.DifContribution.ToString().Replace(",", ".") + ",0)";

                        if (IdInstrument == 3 || IdInstrument == 26) // Options or Warrants
                        {
                            UpdateString += ", [Option Intrinsic] =" + this.OptionIntrinsic.ToString().Replace(",", ".");
                            UpdateString += ", [Option TV] =" + this.OptionTvUc.ToString().Replace(",", ".");
                            UpdateString += ", [Option Intrinsic Cash pC] =" + this.OptionIntrinsicCashPc.ToString().Replace(",", ".");
                            UpdateString += ", [Gamma] =" + this.Gamma.ToString().Replace(",", ".");
                            UpdateString += ", [Vega] = COALESCE(" + this.Vega.ToString().Replace(",", ".") + ",0)";
                            UpdateString += ", [Theta] =" + this.Theta.ToString().Replace(",", ".");
                            UpdateString += ", [Rho] = COALESCE(" + this.Rho.ToString().Replace(",", ".") + ",0)";
                        }

                    }



                    return UpdateString;

                }
                */
        public DataRow GetDataRow()
        {
            DataRow dtRow = GlobalVars.Instance.UpdatePosTable.NewRow();

            dtRow["Adjust Brokerage"] = 0;
            dtRow["Adjust Cash"] = 0;
            dtRow["Adjust Currency"] = 0;
            dtRow["Brokerage"] = 0;
            dtRow["Calc Error Flag"] = 0;
            dtRow["Close USD"] = 0;
            dtRow["Close_Date"] = this.DateClose.ToString("yyyy-MM-dd");
            dtRow["Closed_PL"] = 0;
            dtRow["Contribution pC"] = this.ContributionPc;
            dtRow["Contribution pC Adjusted"] = 0;
            dtRow["Contribution pC Admin"] = this.ContributionAdmin;
            dtRow["Cost Close"] = this.CostClose;
            dtRow["Current NAV pC"] = 0;
            dtRow["CX"] = this.Cash;
            dtRow["Date Now"] = this.DateNow.ToString("yyyy-MM-dd");
            dtRow["Days to Expiration"] = this.DaysToExpiration;
            dtRow["Days to Liquidity"] = this.DaysToLiquidity;
            dtRow["Display Last "] = this.DisplayLast;
            dtRow["Duration Date"] = this.DurationDate.ToString("yyyy-MM-dd");
            dtRow["Flag Calc Cost"] = 0;
            dtRow["Flag Calc Last"] = 0;
            dtRow["Foward Cost"] = 0;
            dtRow["FWD Adj Brokerage"] = 0;
            dtRow["FWD Adj Price"] = this.FwdAdjPrice;
            dtRow["FWD Price"] = this.FwdPrice;
            dtRow["FWValue2"] = 0;
            dtRow["Gross uC"] = this.Gross;
            dtRow["Last Admin"] = this.LastUcAdmin;
            dtRow["Last Calc"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            dtRow["Last Cost Close Calc"] = new DateTime(1900, 01, 01);
            dtRow["Last Position"] = this.DateClose.ToString("yyyy-MM-dd");
            dtRow["Last USD"] = 0;
            dtRow["Loan AVG Rate"] = 0;
            dtRow["Loan Rate In CBLC"] = this.LoanRateInCblc;
            dtRow["Loan Rate Out CBLC"] = this.LoanRateOutCblc;
            dtRow["Loaned In"] = 0;
            dtRow["Loaned Out"] = 0;
            dtRow["Loaned Rate In"] = 0;
            dtRow["Loaned Rate Out"] = 0;
            dtRow["Lot Size"] = this.LotSize;
            dtRow["Net Quantity Loan"] = 0;
            dtRow["New Strategy"] = this.Strategy;
            dtRow["New Sub Strategy"] = this.SubStrategy;
            dtRow["Option TV"] = this.OptionTvUc;
            dtRow["Position All Portfolios"] = 0;
            dtRow["Position D-3"] = 0;
            dtRow["Position Ex-FIA"] = 0;
            dtRow["Price Date"] = new DateTime(1900, 01, 01);
            dtRow["ReportSector"] = " ";
            dtRow["ReportSectorPort"] = " ";
            dtRow["Strategy %"] = this.StrategyPercent;
            dtRow["Time to Expiration"] = 0;
            dtRow["TradeCost"] = 0;
            dtRow["Underlying"] = this.Underlying;
            dtRow["Underlying Liquidity"] = 0;
            dtRow["UpdTime Last"] = this.UpdTimeLast.ToString("yyyy-MM-dd HH:mm:ss");
            dtRow["UpdTime last Admin"] = this.UpdTimeLastAdmin.ToString("yyyy-MM-dd HH:mm:ss");
            dtRow["Vol Date"] = new DateTime(1900, 01, 01);
            dtRow["ZId Strategy"] = 0;
            dtRow["ZId Sub Strategy"] = 0;
            dtRow["% to Bid/Ask"] = this.PercentBidAsk;
            dtRow["10Y Equiv DNAV"] = this.TenYearEquivDnav;
            dtRow["6m Av Volume"] = this.AvVolume6m;
            dtRow["Adjusted Fw Position"] = this.AdjFwPosition;
            dtRow["Ask"] = this.Ask;
            dtRow["Asset Book Contribution"] = this.AssetBookContribution;
            dtRow["Asset Class"] = this.AssetClass;
            dtRow["Asset Contribution"] = this.AssetContribution;
            dtRow["Asset P/L pC Admin"] = this.AssetPlPcAdmin;
            dtRow["Asset P/L pC"] = this.AssetPlPc;
            dtRow["Asset P/L uC"] = this.AssetPlUc;
            dtRow["Asset pC"] = this.AssetPc;
            dtRow["Asset PL uC Admin"] = this.AssetPlUcAdmin;
            dtRow["Asset uC Admin"] = this.AssetUcAdmin;
            dtRow["Asset uC"] = this.AssetUc;
            dtRow["Base Underlying Currency"] = this.BaseUnderlyingCurrency;
            dtRow["Base Underlying"] = this.BaseUnderlying;
            dtRow["Bid"] = this.Bid;
            dtRow["Book NAV"] = this.BookNav;
            dtRow["Book"] = this.Book;
            dtRow["BRL"] = this.Brl;
            dtRow["BRL/NAV"] = this.BrlNav;
            dtRow["Cash Admin"] = this.CashAdmin;
            dtRow["Cash FLow"] = this.CashFlow;
            dtRow["Cash Premium"] = this.CashPremium;
            dtRow["Cash uC Admin"] = this.CashUcAdmin;
            dtRow["Cash uC"] = this.CashUc;
            dtRow["Cash"] = this.Cash;
            dtRow["Cash/NAV"] = this.CashNav;
            dtRow["Close Admin"] = this.CloseAdmin;
            dtRow["Close pC Admin"] = this.ClosePcAdmin;
            dtRow["Close pC"] = this.ClosePc;
            dtRow["Close"] = this.Close;
            dtRow["Contribution pC Book"] = this.ContributionPcBook;
            dtRow["Contribution uC"] = this.ContributionUc;
            dtRow["Cost Close Admin"] = this.CostCloseAdmin;
            dtRow["Cost Close pC Admin"] = this.CostClosePcAdmin;
            dtRow["Cost Close pC"] = this.CostClosePC;
            dtRow["Currency Chg"] = this.CurrencyChange;
            dtRow["Currency Contribution"] = this.CurrencyContribution;
            dtRow["Currency P/L Admin"] = this.CurrencyPlAdmin;
            dtRow["Currency P/L"] = this.CurrencyPl;
            dtRow["CXBOOK"] = this.CxBook;
            dtRow["Delta Cash"] = this.DeltaCash;
            dtRow["Delta Quantity"] = this.DeltaQuantity;
            dtRow["Delta Side"] = this.DeltaSide;
            dtRow["Delta"] = this.Delta;
            dtRow["Delta/Book"] = this.DeltaBook;
            dtRow["Delta/NAV"] = this.DeltaNav;
            dtRow["Description"] = this.Description.Substring(0, Math.Min(Description.Length, 50));
            dtRow["Dif Contrib"] = this.DifContribution;
            dtRow["Display Close pC"] = this.DisplayClosePc;
            dtRow["Display Close"] = this.DisplayClose;
            dtRow["Display Cost Close pC"] = this.DisplayCostClosePc;
            dtRow["Display Cost Close"] = this.DisplayCostClose;
            dtRow["Display Last pC"] = this.DisplayLastPc;
            dtRow["Dividends"] = this.Dividends;
            dtRow["Duration"] = this.Duration;
            dtRow["Exchange Ticker"] = this.ExchangeTicker;
            dtRow["Flag Close Admin"] = this.FlagCloseAdmin;
            dtRow["Flag Last Admin"] = this.FlagLastAdmin;
            dtRow["Flag Last"] = this.FlagLast;
            dtRow["FWD Adj P/L"] = this.FwdAdjPl;
            dtRow["FWValue"] = this.FwValue;
            dtRow["Gamma Cash"] = this.GammaCash;
            dtRow["Gamma Quantity"] = this.GammaQuantity;
            dtRow["Gamma"] = this.Gamma;
            dtRow["Gamma/NAV"] = this.GammaNav;
            dtRow["Gross Book"] = this.GrossBook;
            dtRow["Gross Delta"] = this.GrossDelta;
            dtRow["Gross"] = this.Gross;
            dtRow["Id Account"] = this.IdAccount;
            dtRow["Id Administrator"] = this.IdAdministrator;
            dtRow["Id Asset Class"] = this.IdAssetClass;
            dtRow["Id Base Underlying Currency"] = this.IdBaseUnderlyingCurrency;
            dtRow["Id Base Underlying"] = this.IdBaseUnderlying;
            dtRow["Id Book"] = this.IdBook;
            dtRow["Id Currency Ticker"] = this.IdCurrencyTickerRef;
            dtRow["Id Forward"] = this.IdForward;
            dtRow["Id Instrument"] = this.IdInstrument;
            dtRow["Id Portfolio"] = this.IdPortfolio;
            dtRow["Id Position"] = this.IdPosition;
            dtRow["Id Price Table"] = this.IdPriceTable;
            dtRow["Id Section"] = this.IdSection;
            dtRow["Id Source Close Admin"] = this.IdSourceCloseAdmin;
            dtRow["Id Source Close"] = this.IdSourceClose;
            dtRow["Id Source Last Admin"] = this.IdSourceLastAdmin;
            dtRow["Id Source Last"] = this.IdSourceLast;
            dtRow["Id Sub Portfolio"] = this.IdSubPortfolio;
            dtRow["Id Ticker Cash"] = this.IdTickerCash;
            dtRow["Id Ticker"] = this.IdSecurity;
            dtRow["Id Underlying"] = this.IdUnderlying;
            dtRow["Industry Group"] = this.IndustryGroup;
            dtRow["Industry"] = this.Industry;
            dtRow["Initial Position"] = this.InitialPosition;
            dtRow["Instrument"] = this.Instrument;
            dtRow["Last pC Admin"] = this.LastPcAdmin;
            dtRow["Last pC"] = this.LastPc;
            dtRow["Last"] = this.Last;
            dtRow["Loan Book Contribution"] = this.LoanBookContribution;
            dtRow["Loan Cost"] = this.LoanCost;
            dtRow["Loan Marginal Book Contribution"] = this.LoanMarginalBookContribution;
            dtRow["Loan Marginal Gain"] = this.LoanMarginalGain;
            dtRow["Loan MKT Total"] = this.LoanMktTotal;
            dtRow["Loan Potential Book Contribution"] = this.LoanPotentialBookContribution;
            dtRow["Loan Potential Gain"] = this.LoanPotentialGain;
            dtRow["Long Book"] = this.LongBook;
            dtRow["Long Delta"] = this.LongDelta;
            dtRow["Long"] = this.Long;
            dtRow["Market Cap"] = this.MktCap;
            dtRow["Model Price"] = this.ModelPrice;
            dtRow["NAV pC"] = this.NavPc;
            dtRow["NAV"] = this.Nav;
            dtRow["Nest Sector"] = this.NestSector;
            dtRow["New Id Strategy"] = this.IdStrategy;
            dtRow["New Id Sub Strategy"] = this.IdSubStrategy;
            dtRow["Notional Close"] = this.NotionalClose;
            dtRow["Notional"] = this.Notional;
            dtRow["Option Intrinsic Cash pC"] = this.OptionIntrinsicCashPc;
            dtRow["Option Intrinsic"] = this.OptionIntrinsic;
            dtRow["Option TV Cash pC"] = this.OptionTvCashPc;
            dtRow["Option Type"] = this.OptionType;
            dtRow["P/L %"] = this.PlPerc;
            dtRow["Portfolio Currency"] = this.IdCurrencyPortfolioRef;
            dtRow["Portfolio"] = this.Portfolio;
            dtRow["Position"] = this.CurrentPosition;
            dtRow["Prev Asset P/L Admin"] = this.PrevAssetPlAdmin;
            dtRow["Prev Asset P/L"] = this.PrevAssetPl;
            dtRow["Prev Cash uC Admin"] = this.PrevCashUcAdmin;
            dtRow["Prev Cash uC"] = this.PrevCashUc;
            dtRow["Price Table"] = this.PriceTable;
            dtRow["Quantity Bought"] = this.QuantBoughtSum;
            dtRow["Quantity Sold"] = this.QuantSoldSum;
            dtRow["Rate Period"] = this.RatePeriod;
            dtRow["Rate Year"] = this.RateYear;
            dtRow["Realized Admin"] = this.RealizedAdmin;
            dtRow["Realized"] = this.Realized;
            dtRow["Rho"] = this.Rho;
            dtRow["Section"] = this.Section;
            dtRow["Sector"] = this.Sector;
            dtRow["Security Currency"] = this.SecurityCurrency;
            dtRow["Short Book"] = this.ShortBook;
            dtRow["Short Delta"] = this.ShortDelta;
            dtRow["Short"] = this.Short;
            dtRow["Side"] = this.Side;
            dtRow["Source Close Admin"] = this.SourceCloseAdmin;
            dtRow["Source Close"] = this.SourceClose;
            dtRow["Source Last Admin"] = this.SourceLastAdmin;
            dtRow["Source Last"] = this.SourceLast;
            dtRow["Spot Close"] = this.SpotClose;
            dtRow["Spot USD Close"] = this.SpotCloseUsd;
            dtRow["Spot USD"] = this.SpotUsd;
            dtRow["Spot"] = this.Spot;
            dtRow["Strike"] = this.Strike;
            dtRow["Sub Industry"] = this.SubIndustry;
            dtRow["Sub Portfolio"] = this.SubPortfolio;
            dtRow["Theta"] = this.Theta;
            dtRow["Theta/NAV"] = this.ThetaNav;
            dtRow["Ticker"] = this.NestTicker;
            dtRow["Total P/L Admin"] = this.TotalPlAdmin;
            dtRow["Total P/L"] = this.TotalPl;
            dtRow["Trade Flow"] = this.TradeFlow;
            dtRow["Underlying Acount"] = this.IdUnderlyingAcount;
            dtRow["Underlying Country"] = this.UnderlyingCountry;
            dtRow["Underlying Last"] = this.UnderlyingLast;
            dtRow["Vega"] = this.Vega;
            dtRow["Vol Flag"] = this.VolFlag;
            dtRow["Volatility"] = this.Volatility;
            dtRow["ZStrategy"] = this.Strategy;
            dtRow["ZSub Strategy"] = this.SubStrategy.Substring(0, Math.Min(this.SubStrategy.Length, 25));
            dtRow["Expiration"] = this.Expiration.ToString("yyyy-MM-dd");
            dtRow["NAVPrevious"] = this.prevNAV;
            dtRow["NAVpCPrevious"] = this.prevNavPc;

            return dtRow;
        }

        public override string ToString()
        {
            return this.IdPortfolio + "_" + this.NestTicker + "_" + this.IdBook + "_" + this.IdSection + "_" + this.IdSecurity + "_" + this.InitialPosition + "_" + this.CurrentPosition;
        }

        public void SetCostData()//PROC_GET_CALCULATE_COST_CLOSE and [FCN_Cost_Close]
        {
            double AvgpRealized = 0;
            double AvgpRealizedAdmin = 0;
            double QuantRealized = 0;
            double QuantRemaining = 0;
            double Cash_Flow_Trades = 0;
            double Trade_Flow_Trades = 0;

            this.QuantBoughtFactor = 1;
            this.QuantSoldFactor = 1;

            if (this.QuantBoughtCf != 0) this.AvgPriceBought = this.AmtBoughtCf / this.QuantBoughtCf;
            if (this.QuantSoldCf != 0) this.AvgPriceSold = this.AmtSoldCf / this.QuantSoldCf;

            if (this.IdInstrument != 6)
            {
                if (Math.Round(this.InitialPosition, 6) >= 0 && Math.Round(this.CurrentPosition, 6) >= 0)
                {
                    if (Math.Round(this.InitialPosition + this.QuantBought, 6) != 0)
                    {
                        this.CostClose = (this.QuantBought * this.AvgPriceBought + this.InitialPosition * this.Close) / (this.InitialPosition + this.QuantBought);
                        this.CostCloseAdmin = (this.QuantBought * this.AvgPriceBought + this.InitialPosition * this.CloseAdmin) / (this.InitialPosition + this.QuantBought);
                    }
                    else
                    {
                        this.CostClose = (this.QuantBought * this.AvgPriceBought);
                        this.CostCloseAdmin = (this.QuantBought * this.AvgPriceBought);
                    }
                    AvgpRealized = this.AvgPriceSold;
                    AvgpRealizedAdmin = this.AvgPriceSold;

                    if (Math.Round(Math.Abs(this.QuantSold), 6) <= Math.Round(this.InitialPosition + this.QuantBought, 6) || this.QuantSold == 0)
                    {
                        QuantRealized = Math.Abs(this.QuantSold);
                        QuantRemaining = this.InitialPosition + this.QuantBought + this.QuantSold;
                    }
                    else
                    {
                        QuantRealized = this.InitialPosition + this.QuantBought;
                        QuantRemaining = this.InitialPosition + this.QuantBought + QuantRealized;
                    }
                }

                if (Math.Round(this.InitialPosition, 6) <= 0 && Math.Round(this.CurrentPosition, 6) <= 0)
                {
                    if ((this.QuantSold + this.InitialPosition) != 0)
                    {
                        this.CostClose = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.Close) / (this.QuantSold + this.InitialPosition);
                        this.CostCloseAdmin = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.CloseAdmin) / (this.QuantSold + this.InitialPosition);
                    }
                    else
                    {
                        this.CostClose = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.Close);
                        this.CostCloseAdmin = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.CloseAdmin);
                    }
                    AvgpRealized = this.AvgPriceBought;
                    AvgpRealizedAdmin = this.AvgPriceBought;

                    if (-Math.Round(this.QuantBought, 6) >= Math.Round(this.InitialPosition, 6) + this.QuantSold || this.QuantBought == 0)
                    {
                        QuantRealized = -this.QuantBought;
                        QuantRemaining = this.InitialPosition + this.QuantBought - QuantRealized;
                    }
                    else
                    {
                        QuantRealized = this.InitialPosition + this.QuantSold;
                        QuantRemaining = this.InitialPosition + this.QuantSold - QuantRealized;
                    }
                }

                if (Math.Round(this.InitialPosition, 6) > 0 && Math.Round(this.CurrentPosition, 6) < 0)
                {
                    if (Math.Round(this.InitialPosition + this.QuantBought, 6) != 0)
                    {
                        AvgpRealized = (this.QuantBought * this.AvgPriceBought + this.InitialPosition * this.Close) / (this.InitialPosition + this.QuantBought);
                        AvgpRealizedAdmin = (this.QuantBought * this.AvgPriceBought + this.InitialPosition * this.CloseAdmin) / (this.InitialPosition + this.QuantBought);
                    }
                    else
                    {
                        AvgpRealized = (this.QuantBought * this.AvgPriceBought);
                        AvgpRealizedAdmin = (this.QuantBought * this.AvgPriceBought);
                    }
                    this.CostClose = this.AvgPriceSold;
                    this.CostCloseAdmin = this.AvgPriceSold;

                    QuantRealized = this.QuantSold - this.CurrentPosition;
                    QuantRemaining = this.CurrentPosition;
                }

                if (Math.Round(this.InitialPosition, 6) < 0 && Math.Round(this.CurrentPosition, 6) > 0)
                {
                    if ((this.QuantSold + this.InitialPosition) != 0)
                    {
                        AvgpRealized = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.Close) / (this.QuantSold + this.InitialPosition);
                        AvgpRealizedAdmin = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.CloseAdmin) / (this.QuantSold + this.InitialPosition);
                    }
                    else
                    {
                        AvgpRealized = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.Close);
                        AvgpRealizedAdmin = (this.QuantSold * this.AvgPriceSold + this.InitialPosition * this.CloseAdmin);
                    }
                    this.CostClose = this.AvgPriceBought;
                    this.CostCloseAdmin = this.AvgPriceBought;
                    QuantRealized = this.QuantBought - this.CurrentPosition;
                    QuantRemaining = this.CurrentPosition;
                }
            }
            else
            {
                this.QuantBoughtFactor = -1;
                this.QuantSoldFactor = -1;
                QuantRealized = 0;
                QuantRemaining = this.CurrentPosition;
                Cash_Flow_Trades = this.QuantBought + this.QuantSold;
                this.CostClose = 1;
                this.CostCloseAdmin = 1;
            }

            if (this.IdSecurity == 83066)
            {
                this.QuantBoughtFactor = 0;
                this.QuantSoldFactor = 0;
                QuantRealized = 0;
                QuantRemaining = 0;
                Cash_Flow_Trades = 0;
                this.CostClose = 0.00001;
                this.CostCloseAdmin = 0.00001;
                this.Close = 1;
            }

            if (this.IdInstrument == 17 || this.IdInstrument == 24)
            {
                this.CostClose = 1;
                this.CostCloseAdmin = 1;
                //this.CashFlow = this.AmtBought_Other + this.AmtSold_Other; ============================================ ARRUMAR ===
            }

            this.Realized = (QuantRealized * (AvgpRealized - this.CostClose) / this.LotSize) + this.Dividends;
            this.RealizedAdmin = (QuantRealized * (AvgpRealizedAdmin - this.CostCloseAdmin) / this.LotSize) + this.Dividends;



            switch (this.IdInstrument)
            {
                case 17:
                case 24:
                    this.Realized = this.Realized - this.CashFlow;
                    this.RealizedAdmin = this.RealizedAdmin - this.CashFlow;
                    break;
                default:
                    break;
            }

            Trade_Flow_Trades = (this.QuantBought / this.LotSize) * this.AvgPriceBought + (this.QuantSold / this.LotSize) * this.AvgPriceSold;

            if (this.IsRT == 0)
            {
                if (this.IdSecurity == 1844
                    || this.IdSecurity == 5746
                    || this.IdSecurity == 83066
                    || this.IdSecurity == 5747
                    || this.IdSecurity == 5791
                    )
                {
                    this.InitialPosition = 0;

                    this.QuantBoughtTrade = 0;
                    this.QuantSoldTrade = 0;

                    this.QuantBoughtDiv = 0;
                    this.QuantSoldDiv = 0;

                    this.QuantBoughtFwd = 0;
                    this.QuantSoldFwd = 0;

                    this.QuantBoughtOther = 0;
                    this.QuantSoldOther = 0;
                }
            }
        }

        public string GetFieldName(string PropertyName)
        {
            switch (PropertyName)
            {
                case "IdInstrument": return "[Id Instrument]";
                case "DateClose": return "[Close_Date]";
                case "DataPosicao": return "[Last Position]";
                case "Cash": return "[Cash]";
                case "DisplayCostClose": return "[Display Cost Close]";
                case "DisplayClose": return "[Display Close]";
                case "Delta": return "[Delta]";
                case "DeltaQuantity": return "[Delta Quantity]";
                case "OptionIntrinsic": return "[Option Intrinsic]";
                case "OptionTvUc": return "[Option TV]";
                case "OptionIntrinsicCashPc": return "[Option Intrinsic Cash pC]";
                case "Gamma": return "[Gamma]";
                case "Vega": return "[Vega]";
                case "Theta": return "[Theta]";
                case "Rho": return "[Rho]";
                case "IdAdministrator": return "[Id Administrator]";
                case "IdAssetClass": return "[Id Asset Class]";
                case "IdSourceClose": return "[Id Source Close]";
                case "IdSourceCloseAdmin": return "[Id Source Close Admin]";
                case "IdBaseUnderlying": return "[Id Base Underlying]";
                case "IdBaseUnderlyingCurrency": return "[Id Base Underlying Currency]";
                case "IdSourceLast": return "[Id Source Last]";
                case "IdSourceLastAdmin": return "[Id Source Last Admin]";
                case "IdAccount": return "[Id Account]";
                case "IdTickerCash": return "[Id Ticker Cash]";
                case "IdForward": return "[Id Forward]";
                case "IdSecurityCurrency": return "[Id Currency Ticker]";
                case "IdSubPortfolio": return "[Id Sub Portfolio]";
                case "IdPriceTable": return "[Id Price Table]";
                case "IdUnderlying": return "[Id Underlying]";
                case "IdStrategy": return "[New Id Strategy]";
                case "IdSubStrategy": return "[New Id Sub Strategy]";
                case "IdPortCurrency": return "[Portfolio Currency]";
                case "Strike": return "[Strike]";
                case "LotSize": return "[Lot Size]";
                case "Expiration": return "[Expiration]";
                case "BusDays": return "[Days to Expiration]";
                case "YearFraction": return "[Time to Expiration]";
                case "MktCap": return "[Market Cap]";
                case "AvVolume6m": return "[6m Av Volume]";
                case "LoanRateInCblc": return "[Loan Rate In CBLC]";
                case "LoanRateOutCblc": return "[Loan Rate Out CBLC]";
                case "LoanMktTotal": return "[Loan MKT Total]";
                case "OptionType": return "[Option Type]";
                case "PrevCashUcAdmin": return "[Prev Cash uC Admin]";
                case "Close": return "[Close]";
                case "CloseAdmin": return "[Close Admin]";
                case "FlagCloseAdmin": return "[Flag Close Admin]";
                case "InitialPosition": return "[Initial Position]";
                case "PrevCashUc": return "[Prev Cash uC]";
                case "PrevAssetPl": return "[Prev Asset P/L]";
                case "PrevAssetPlAdmin": return "[Prev Asset P/L Admin]";
                case "Nav": return "[NAV]";
                case "BookNav": return "[Book NAV]";
                case "NavPc": return "[NAV pC]";
                case "SpotClose": return "[Spot Close]";
                case "SpotCloseUsd": return "[Spot USD Close]";
                case "ClosePc": return "[Close pC]";
                case "DisplayClosePc": return "[Display Close pc]";
                case "ClosePcAdmin": return "[Close pC Admin]";
                case "Dividends": return "[Dividends]";
                case "RateYear": return "[Rate Year]";
                case "Spot": return "[Spot]";
                case "SpotUsd": return "[Spot USD]";
                case "": return "[Brokerage]";
                case "Volatility": return "[Volatility]";
                case "VolDate": return "[Vol Date]";
                case "RatePeriod": return "[Rate Period]";
                case "UnderlyingLast": return "[Underlying Last]";
                case "Now": return "[Last Calc]";
                case "ThetaNav": return "[Theta/NAV]";
                case "Duration": return "[Duration]";
                case "DurationDate": return "[Duration Date]";
                case "TenYearEquivDnav": return "[10Y Equiv DNAV]";
                case "SectionPercent": return "[Strategy %]";
                case "LoanBookContribution": return "[Loan Book Contribution]";
                case "FwValue": return "[FWValue]";
                case "FwdAdjPl": return "[FWD Adj P/L]";
                case "FwdPrice": return "[FWD Price]";
                case "FwdAdjPrice": return "[FWD Adj Price]";
                case "AdjFwPosition": return "[Adjusted Fw Position]";
                case "VolFlag": return "[Vol Flag]";
                case "BaseUnderlying": return "[Base Underlying]";
                case "BaseUnderlyingCurrency": return "[Base Underlying Currency]";
                case "SourceLast": return "[Source Last]";
                case "ReturnFlagLast": return "[Flag Last]";
                case "SourceLastAdmin": return "[Source Last Admin]";
                case "ReturnFlagLastAdmin": return "[Flag Last Admin]";
                case "SourceClose": return "[Source Close]";
                case "SourceCloseAdmin": return "[Source Close Admin]";
                case "Instrument": return "[Instrument]";
                case "AssetClass": return "[Asset Class]";
                case "SubIndustry": return "[Sub Industry]";
                case "Description": return "[Description]";
                case "SecurityCurrency": return "[Security Currency]";
                case "Industry": return "[Industry]";
                case "IndustryGroup": return "[Industry Group]";
                case "Sector": return "[Sector]";
                case "UnderlyingCountry": return "[Underlying Country]";
                case "NestSector": return "[Nest Sector]";
                case "Portfolio": return "[Portfolio]";
                case "NestTicker": return "[Ticker]";
                case "PriceTable": return "[Price Table]";
                case "Underlying": return "[Underlying]";
                case "Book": return "[Book]";
                case "Section": return "[Section]";
                case "SubPortfolio": return "[Sub Portfolio]";
                case "Strategy": return "[New Strategy]";
                case "SubStrategy": return "[New Sub Strategy]";
                case "ExchangeTicker": return "[Exchange Ticker]";
                case "QuantBoughtSum": return "[Quantity Bought]";
                case "QuantSoldSum": return "[Quantity Sold]";
                case "CurrentPosition": return "[Position]";
                case "CostClose": return "[Cost Close]";
                case "CostCloseAdmin": return "[Cost Close Admin]";
                case "TradeFlow": return "[Trade Flow]";
                case "CostClosePC": return "[Cost Close pC]";
                case "DisplayCostClosePc": return "[Display Cost Close pc]";
                case "CostClosePcAdmin": return "[Cost Close pC Admin]";
                case "GammaQuantity": return "[Gamma Quantity]";
                case "OptionTvCashPc": return "[Option TV Cash pC]";
                case "DaysToLiquidity": return "[Days To Liquidity]";
                case "LoanPotentialGain": return "[Loan Potential Gain]";
                case "LoanPotentialBookContribution": return "[Loan Potential Book Contribution]";
                case "LoanMarginalGain": return "[Loan Marginal Gain]";
                case "LoanMarginalBookContribution": return "[Loan Marginal Book Contribution]";
                case "PercentBidAsk": return "[% to Bid/Ask]";
                case "Last": return "[Last]";
                case "Bid": return "[Bid]";
                case "Ask": return "[Ask]";
                case "DisplayLast": return "[Display Last ]";
                case "DisplayLastPc": return "[Display Last pC]";
                case "LastPc": return "[Last pC]";
                case "LastUcAdmin": return "[Last Admin]";
                case "LastPcAdmin": return "[Last pC Admin]";
                case "UpdTimeLast": return "[UpdTime Last]";
                case "UpdTimeLastAdmin": return "[UpdTime Last Admin]";
                case "CashAdmin": return "[Cash Admin]";
                case "Notional": return "[Notional]";
                case "CashUc": return "[Cash uC]";
                case "DeltaCash": return "[Delta Cash]";
                case "GammaCash": return "[Gamma Cash]";
                case "CashNav": return "[Cash/NAV]";
                case "Brl": return "[BRL]";
                case "DeltaNav": return "[Delta/NAV]";
                case "GammaNav": return "[Gamma/NAV]";
                case "BrlNav": return "[BRL/NAV]";
                case "Long": return "[Long]";
                case "Short": return "[Short]";
                case "Gross": return "[Gross]";
                case "LongBook": return "[Long Book]";
                case "ShortBook": return "[Short Book]";
                case "GrossBook": return "[Gross Book]";
                case "GrossDelta": return "[Gross Delta]";
                case "DeltaBook": return "[Delta/Book]";
                case "Side": return "[Side]";
                case "DeltaSide": return "[Delta Side]";
                case "LongDelta": return "[Long Delta]";
                case "ShortDelta": return "[Short Delta]";
                case "CurrencyPl": return "[Currency P/L]";
                case "CurrencyPlAdmin": return "[Currency P/L Admin]";
                case "CurrencyChange": return "[Currency Chg]";
                case "CurrencyContribution": return "[Currency Contribution]";
                case "CashPremium": return "[Cash Premium]";
                case "TotalPlAdmin": return "[Total P/L Admin]";
                case "TotalPl": return "[Total P/L]";
                case "CashFlow": return "[Cash FLow]";
                case "AssetUc": return "[ASSET uC]";
                case "AssetPc": return "[ASSET pC]";
                case "ContributionPc": return "[Contribution pC]";
                case "ContributionUc": return "[Contribution uC]";
                case "RealizedAdmin": return "[Realized Admin]";
                case "AssetPlPc": return "[ASSET P/L pC]";
                case "AssetPlUc": return "[ASSET P/L uC]";
                case "AssetBookContribution": return "[Asset Book Contribution]";
                case "CurrencyBookContribution": return "[Currency Book Contribution]";
                case "ContributionPcBook": return "[Contribution pC Book]";
                case "CxBook": return "[CXBOOK]";
                case "PlPerc": return "[P/L %]";
                case "Realized": return "[Realized]";
                case "AssetPlPcAdmin": return "[Asset P/L pC Admin]";
                case "ContributionAdmin": return "[Contribution pC Admin]";
                case "AssetUcAdmin": return "[Asset uC Admin]";
                case "CashUcAdmin": return "[Cash uC Admin]";
                case "AssetPlUcAdmin": return "[Asset PL uC Admin]";
                case "AssetContribution": return "[Asset Contribution]";
                case "DifContribution": return "[Dif Contrib]";
                case "NAVPrevious": return "[NAVPrevious]";
                case "NAVpCPrevious": return "NAVpCPrevious";
                default: return "";
            }
        }
    }

    public static class DBUtils
    {
        public static DataTable LoadPositionTable(DateTime CloseDate, string PortFilter)
        {
            DataTable ReturnTable;

            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]='" + CloseDate.ToString("yyyy-MM-dd") + "' AND [Id Portfolio] IN (" + PortFilter + ") ORDER BY [Id Portfolio], [Id Book], [Id Section], [Id Ticker]";
                ReturnTable = curConn.Return_DataTable(StringSQL);
            }

            return ReturnTable;
        }

        public static DataTable LoadTradesTable(DateTime iniDate, DateTime endDate, string PortFilter)
        {
            DataTable ReturnTable;

            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM NESTDB.dbo.VW_ORDENS_ALL WHERE Data_Trade>='" + iniDate.ToString("yyyy-MM-dd") + "' AND Data_Trade<='" + endDate.ToString("yyyy-MM-dd") + "' AND Status_Ordem<>4 AND Id_Port_Type=1 AND Id_Account IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio IN (" + PortFilter + "))";
                ReturnTable = curConn.Return_DataTable(StringSQL);
            }
            return ReturnTable;
        }

        public static DataTable LoadTransactionTable(DateTime iniDate, DateTime endDate, string PortFilter)
        {
            DataTable ReturnTable;

            using (newNestConn curConn = new newNestConn())
            {
                string StringSQL = "SELECT * FROM NESTDB.dbo.vw_Transactions_ALL_Slow_exTrades WHERE Trade_Date>='" + iniDate.ToString("yyyy-MM-dd") + "' AND Trade_Date<='" + endDate.ToString("yyyy-MM-dd") + "' AND Id_Account1 IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio IN (" + PortFilter + "))";
                ReturnTable = curConn.Return_DataTable(StringSQL);
            }
            return ReturnTable;
        }
    }

    public enum UpdateType
    {
        RTPrice = 1,
        RTQuantity = 2,
        AllFields = 3,
        RTSecurityInformation = 4
    }
}
