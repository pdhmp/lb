
DECLARE @IniTime datetime
DECLARE @EndTime datetime

SET @IniTime='11:00:00'
SET @EndTime='14:00:00'

INSERT INTO NESTTICK.dbo.tb002_VWAPDATA
SELECT * FROM
(
SELECT TradeDate
	, @EndTime AS VWAPEndTime
	, DateDiff(n, @IniTime, @EndTime) AS VWAPLength
	, Ticker
	, SUM(Quantity) AS Quantity
	, SUM(FinAmount)/SUM(Quantity) AS VWAP
	,	(
			SELECT TOP 1 Last
			FROM NESTTICK.dbo.Tb000_IntradayBov X
			LEFT JOIN NESTTICK.dbo.Tb000_OpenAdjust Y
			ON X.TradeDate=Y.RefDate
			WHERE SlotTime<=@EndTime+OpenAdjust AND Ticker=A.Ticker AND TradeDate=A.TradeDate
			ORDER BY SlotTime DESC
		) AS Last
	, (SELECT TOP 1 IdSecurity FROM NESTDB.dbo.Tb001_Securities_Variable WHERE NestTicker=A.Ticker) AS IdSecurity
FROM NESTTICK.dbo.Tb000_IntradayBov A
LEFT JOIN NESTTICK.dbo.Tb000_OpenAdjust B
ON A.TradeDate=B.RefDate
WHERE SlotTime>=@IniTime+OpenAdjust AND SlotTime<=@EndTime+OpenAdjust -- AND Ticker='PETR4'
GROUP BY TradeDate, Ticker
) AS VWAPData
WHERE IdSecurity IS NOT NULL
ORDER BY Ticker,TradeDate

/*

SELECT *
FROM NESTTICK.dbo.Tb000_IntradayBov 
WHERE ticker='PETR4' AND SlotTime>='11:00:00' AND SlotTime<='11:30:00'
ORDER BY TradeDate, Ticker



SELECT *, CAST(AvgPx as float)
FROM NESTTICK.dbo.tb002_VWAPDATA
WHERE IdSecurity=1
ORDER BY DataSessao



*/

