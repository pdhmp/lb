USE NESTDB

DECLARE @tempTable TABLE(dateRef datetime, Quantity numeric, QTradesOnly numeric)
DECLARE @Id_Portfolio int
DECLARE @Id_Ticker int 

SET @Id_Portfolio=10
SET @Id_Ticker=5746

DECLARE @LastDate datetime
DECLARE @FinalPos float

SELECT A.[Date Now] AS TrDate, TrLastPos, TrCloseDate, Valor_PL-PL_Livebook AS sumTransactions,TrNAV, TrNAVpC
--INTO #TempTable
FROM
(
	SELECT A.[Date Now], SUM([cash uC]) AS PL_Livebook, MAX([Last Position]) as TrLastPos, MAX([Close_Date]) as TrCloseDate, MAX([NAV]) as TrNAV, MAX([NAV pC]) AS TrNAVpC
	FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]<>@Id_Ticker 
	GROUP BY [Date Now]
) A
LEFT JOIN 
(
	SELECT [Date Now], [cash uC] AS NAVAdjust
	FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker
) B
ON A.[Date Now]=B.[Date Now]
LEFT JOIN 
(
	SELECT * 
	FROM dbo.Tb025_Valor_PL
	WHERE Id_Portfolio=@Id_Portfolio
) C
ON A.[Date Now]=C.Data_PL
WHERE A.[Date Now]>='2010-01-01' AND A.[Date Now]<='2010-09-26'

/*


DELETE FROM dbo.Tb000_Historical_Positions
WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker AND [Date Now]>='2010-01-01' AND [Date Now]<='2010-09-26'

INSERT INTO dbo.Tb000_Historical_Positions
SELECT [Id Portfolio]
           ,[Portfolio]
           ,[Id Ticker]
           ,[Ticker]
           ,[Description]
           ,[Id Price Table]
           ,[Price Table]
           ,[ZId Strategy]
           ,[ZStrategy]
           ,[ZId Sub Strategy]
           ,[ZSub Strategy]
           ,TrLastPos AS [Last Position]
           ,B.TrCloseDate AS [Close_Date]
           ,B.TrDate AS [Date Now]
           ,B.sumTransactions AS [Position]
           ,[Lot Size]
           ,[Cost Close]
           ,[Cost Close pC]
           ,[Last]
           ,[Last pC]
           ,[Close]
           ,[Close pC]
           ,TrNAV AS [NAV]
           ,TrNAVpC AS [NAV pC]
           ,0 AS [Cash]
           ,0 AS [Cash/NAV]
           ,[Brokerage]
           ,0 AS [Delta Cash]
           ,0 AS [Delta/NAV]
           ,[Security Currency]
           ,[Id Currency Ticker]
           ,[Delta]
           ,[Instrument]
           ,[Asset Class]
           ,[Sub Industry]
           ,[Industry]
           ,[Industry Group]
           ,[Sector]
           ,[Underlying Country]
           ,[Nest Sector]
           ,[Portfolio Currency]
           ,0 AS [Asset uC]
           ,0 AS [Asset pC]
           ,0 AS [Asset P/L pC]
           ,0 AS [Currency Chg]
           ,0 AS [Currency P/L]
           ,0 AS [Total P/L]
           ,[Spot USD]
           ,[Spot]
           ,[Expiration]
           ,0 AS [P/L %]
           ,0 AS [Gross]
           ,0 AS [Long]
           ,0 AS [Short]
           ,[Contribution pC]
           ,[Id Underlying]
           ,[Underlying]
           ,[Underlying Acount]
           ,0 AS [Notional]
           ,0 AS [Notional Close]
           ,[Closed_PL]
           ,[Display Last ]
           ,[Display Last pC]
           ,[Display Cost Close]
           ,[Display Cost Close pC]
           ,[BRL]
           ,[BRL/NAV]
           ,0 AS [Long Delta]
           ,0 AS [Short Delta]
           ,0 AS [Gross Delta]
           ,[Last Calc]
           ,[Last Cost Close Calc]
           ,[Realized]
           ,[Asset P/L uC]
           ,[Contribution uC]
           ,0 AS [Delta Quantity]
           ,[Volatility]
           ,[Vol Flag]
           ,[Vol Date]
           ,[Strike]
           ,[Rate Year]
           ,[Rate Period]
           ,[Days to Expiration]
           ,[Time to Expiration]
           ,[Underlying Last]
           ,[Gamma]
           ,[Vega]
           ,[Theta]
           ,[Rho]
           ,[Cash Premium]
           ,[Calc Error Flag]
           ,[Model Price]
           ,[Gamma Quantity]
           ,[Gamma Cash]
           ,[Gamma/NAV]
           ,[Display Close]
           ,[Display Close pC]
           ,[Dif Contrib]
           ,[Price Date]
           ,[Id Instrument]
           ,[Id Asset Class]
           ,[Option Type]
           ,0 AS [Initial Position]
           ,0 AS [Quantity Bought]
           ,0 AS [Quantity Sold]
           ,[Spot USD Close]
           ,[Spot Close]
           ,0 AS [Cash uC]
           ,[Last pC Admin]
           ,[Close pC Admin]
           ,[Cost Close pC Admin]
           ,[Total P/L Admin]
           ,[Realized Admin]
           ,[Asset P/L pC Admin]
           ,[Currency P/L Admin]
           ,[Contribution pC Admin]
           ,[Last Admin]
           ,[Close Admin]
           ,[Cost Close Admin]
           ,[Id Administrator]
           ,[Id Base Underlying]
           ,[Base Underlying]
           ,[Id Base Underlying Currency]
           ,[Base Underlying Currency]
           ,[Bid]
           ,[Ask]
           ,[% to Bid/Ask]
           ,[Id Source Last]
           ,[Source Last]
           ,[Flag Last]
           ,[Id Source Last Admin]
           ,[Source Last Admin]
           ,[Flag Last Admin]
           ,[Id Source Close]
           ,[Source Close]
           ,[Flag Close Admin]
           ,[UpdTime Last]
           ,[UpdTime last Admin]
           ,[Asset uC Admin]
           ,[Theta/NAV]
           ,[Id Source Close Admin]
           ,[Source Close Admin]
           ,0 AS [Prev Cash uC]
           ,0 AS [Prev Cash uC Admin]
           ,[Current NAV pC]
           ,[Option Intrinsic]
           ,[Option TV]
           ,[Option Intrinsic Cash pC]
           ,[Option TV Cash pC]
           ,0 AS [Cash uC Admin]
           ,[Underlying Liquidity]
           ,[Days to Liquidity]
           ,[Duration]
           ,[Duration Date]
           ,[10Y Equiv DNAV]
           ,[Asset PL uC Admin]
           ,0 AS [Delta/Book]
           ,[Contribution pC Book]
           ,0 AS [Long Book]
           ,0 AS [Short Book]
           ,0 AS [Gross Book]
           ,0 AS [CX]
           ,0 AS [CXBOOK]
           ,[Id Book]
           ,[Book]
           ,[Id Sub Portfolio]
           ,[Sub Portfolio]
           ,[New Id Strategy]
           ,[New Strategy]
           ,[New Id Sub Strategy]
           ,[New Sub Strategy]
           ,[Id Section]
           ,[Section]
           ,[Gross uC]
           ,[Position All Portfolios]
           ,[Position Ex-FIA]
           ,0 AS [Book NAV]
           ,[Market Cap]
           ,[6m Av Volume]
           ,[Asset Contribution]
           ,[Currency Contribution]
           ,[Asset Book Contribution]
           ,[Currency Book Contribution]
           ,[Prev Asset P/L]
           ,[Prev Asset P/L Admin]
           ,0 AS [Cash FLow]
           ,0 AS [Trade Flow]
           ,[Id Account]
           ,[Id Ticker Cash]
           ,[Strategy %]
           ,[Flag Calc Cost]
           ,[Flag Calc Last]
FROM dbo.Tb000_Historical_Positions, #TempTable B
WHERE [Id Portfolio]=10 AND [Id Ticker]=@Id_Ticker AND [Date Now]='2010-09-27' 
	AND B.TrDate>='2010-01-01' AND B.TrDate<='2010-09-26'


--DROP TABLE #TempTable

*/