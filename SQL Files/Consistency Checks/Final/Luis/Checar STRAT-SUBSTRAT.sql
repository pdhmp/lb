DECLARE @IniDate datetime
DECLARE @EndDate datetime
DECLARE @Id_Portfolio int

SET @IniDate='20100105'
SET @EndDate='20100120'
SET @Id_Portfolio=10


SELECT 
	C.Id_Ativo,
	C.Simbolo,
	INI.[zId Strategy] AS Strat_Initial,
	TRADES.[zId Strategy] AS Strat_Trades,
	FINAL.[zId Strategy] AS Strat_Final,
	INI.[zId Sub Strategy] AS Section_Initial,
	TRADES.[zId Sub Strategy] AS Section_Trades,
	FINAL.[zId Sub Strategy] AS Section_Final,
	Quant_Initial,
	Quant_Trades,
	Quant_Final,
	convert(numeric,coalesce(Quant_Final,0))-convert(numeric,coalesce(Quant_Trades,0))-convert(numeric,coalesce(Quant_Initial,0)) AS DIFF
FROM 
(
	SELECT [Id Ticker] AS Id_Ticker_Initial,[zId Strategy],[zId Sub Strategy], SUM(Position) AS Quant_Initial
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@IniDate
	GROUP BY [Id Ticker],[zId Strategy],[zId Sub Strategy]
) AS INI
FULL OUTER JOIN 
(
	SELECT [Id Ticker] AS Id_Ticker_HistTable,[zId Strategy],[zId Sub Strategy], SUM(Position) AS Quant_Final
	FROM
	(
		SELECT [Id Ticker],[zId Strategy], [zId Sub Strategy], Position
		FROM NESTDB.dbo.Tb000_Historical_Positions
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
		UNION ALL 
		SELECT [Id Ticker],[zId Strategy], Position, [zId Sub Strategy]
		FROM NESTRT.dbo.Tb000_Posicao_Atual 
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
	) X
	GROUP BY [Id Ticker],[zId Strategy],[zId Sub Strategy]
) AS FINAL
ON INI.Id_Ticker_Initial=FINAL.Id_Ticker_HistTable
AND INI.[zId Strategy] =FINAL.[zId Strategy] 
AND INI.[zId Sub Strategy]=FINAL.[zId Sub Strategy]
FULL OUTER JOIN 
(
	SELECT Id_Ticker AS Id_Ticker_Trades,Estrategia [zId Strategy],Sub_Estrategia [zId Sub Strategy],SUM(coalesce(Quantity,0)) AS Quant_Trades
	FROM dbo.vw_Transactions_STRATEGIA
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
	AND Trade_Date>@IniDate AND Trade_Date<=@EndDate
	GROUP BY Id_Ticker,Estrategia,Sub_Estrategia
) TRADES
ON INI.Id_Ticker_Initial=TRADES.Id_Ticker_Trades
AND INI.[zId Strategy] =TRADES.[zId Strategy] 
AND INI.[zId Sub Strategy]=TRADES.[zId Sub Strategy]
OR( FINAL.Id_Ticker_HistTable=TRADES.Id_Ticker_Trades
AND FINAL.[zId Strategy] =TRADES.[zId Strategy] 
AND FINAL.[zId Sub Strategy]=TRADES.[zId Sub Strategy])

LEFT JOIN dbo.Tb001_Ativos C
ON COALESCE(TRADES.Id_Ticker_Trades, FINAL.Id_Ticker_HistTable, INI.Id_Ticker_Initial)=C.Id_Ativo

--ORDER BY ABS(convert(numeric,coalesce(Quant_Final,0))-convert(numeric,coalesce(Quant_Trades,0))-convert(numeric,coalesce(Quant_Initial,0))) DESC
ORDER BY simbolo