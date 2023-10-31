

DECLARE @Id_SIM int
SET @Id_SIM=79

--SELECT MAX(Id_SIM) FROM NESTSIM.dbo.Trade_SIM

SELECT Id_Ticker
	, CONVERT(varchar,Trade_DateTime,112)
	, SUM(-Trade_Size)
	, NESTSIM.[dbo].[FCN_Get_Exec_Price_Day](@Id_SIM,Id_Ticker,CONVERT(varchar,Trade_DateTime,112),0,0)
	, SUM(-Trade_Size*Trade_Price)
FROM NESTSIM.dbo.Trade_SIM WHERE Id_SIM=@Id_SIM AND Id_Ticker=3 
GROUP BY Id_Ticker,CONVERT(varchar,Trade_DateTime,112)
ORDER BY Id_Ticker,CONVERT(varchar,Trade_DateTime,112)


SELECT * FROM NESTSIM.dbo.Trade_SIM WHERE Id_SIM=79 AND Id_Ticker=444  order by trade_datetime
SELECT * FROM NESTSIM.dbo.Trade_SIM WHERE CONVERT(varchar,Trade_DateTime,112)='20091019'

/*
SELECT NESTSIM.[dbo].[FCN_Get_Exec_Price_Day](79,3,'20090904',0,0)

SELECT Id_Ticker
--, CONVERT(varchar,Trade_DateTime,112)
, SUM(-Trade_Size),SUM(-Trade_Size*Trade_Price)
, SUM(-Trade_Size*ExecPrice) 
FROM 
(
	SELECT *,0 AS ExecPrice--, dbo.[FCN_Get_Exec_Price](Trade_Id_Ticker,TradeDate,TradePrice,TradeSize,120,-0.01) AS ExecPrice
	FROM NESTSIM.dbo.Trade_SIM WHERE Id_Ticker=3 AND Id_SIM=79
) AS A
GROUP BY Id_Ticker--,CONVERT(varchar,Trade_DateTime,112)
ORDER BY Id_Ticker--,CONVERT(varchar,Trade_DateTime,112)

SELECT * FROM NESTSIM.dbo.Trade_SIM WHERE Id_SIM=3 AND Id_Ticker=3 AND CONVERT(varchar,Trade_DateTime,112)='20090903'
ORDER BY Trade_DateTime







SELECT COUNT(*) FROM NESTSIM.dbo.Trade_SIM 

	SELECT *
	FROM NESTSIM.dbo.Trade_SIM 
	where trade_id_ticker=1
	order by Trade_Id_Ticker,tradedate

UPDATE NESTSIM.dbo.Trade_SIM SET TradeSize=-TradeSize

SELECT 

*/