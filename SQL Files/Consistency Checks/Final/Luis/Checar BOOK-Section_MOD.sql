DECLARE @IniDate datetime
DECLARE @EndDate datetime
DECLARE @Id_Portfolio int

SET @IniDate='20100104'
SET @EndDate='20100624'
SET @Id_Portfolio=4

SELECT 
	C.IdSecurity,
	C.Ticker,
	D.Book AS Book_Initial,
	E.Book AS Book_Trades,
	F.Book AS Book_Final,
	INI.[Id Section] AS Section_Initial,
	TRADES.[Id Section] AS Section_Trades,
	FINAL.[Id Section] AS Section_Final,
	Quant_Initial,
	Quant_Trades,
	Quant_Final,
	convert(numeric,coalesce(Quant_Final,0))-convert(numeric,coalesce(Quant_Trades,0))-convert(numeric,coalesce(Quant_Initial,0)) AS DIFF
FROM 
(
	SELECT [Id Ticker] AS Id_Ticker_Initial,[Id Book],[Id Section], SUM(Position) AS Quant_Initial
	FROM NESTDB.dbo.Tb000_Historical_Positions (nolock)
	WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@IniDate
	GROUP BY [Id Ticker],[Id Book],[Id Section]
) AS INI
FULL OUTER JOIN 
(
	SELECT [Id Ticker] AS Id_Ticker_HistTable,[Id Book],[Id Section], SUM(Position) AS Quant_Final
	FROM
	(
		SELECT [Id Ticker],[Id Book], [Id Section], Position
		FROM NESTDB.dbo.Tb000_Historical_Positions (nolock)
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
		UNION ALL 
		SELECT [Id Ticker],[Id Book], Position, [Id Section]
		FROM NESTRT.dbo.Tb000_Posicao_Atual (nolock) 
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
	) X
	GROUP BY [Id Ticker],[Id Book],[Id Section]
) AS FINAL
ON INI.Id_Ticker_Initial=FINAL.Id_Ticker_HistTable
AND INI.[Id Book] =FINAL.[Id Book] 
AND INI.[Id Section]=FINAL.[Id Section]
FULL OUTER JOIN 
(
	SELECT Id_Ticker AS Id_Ticker_Trades,[Id Book],[Id Section],SUM(coalesce(Quantity,0)) AS Quant_Trades
	FROM dbo.vw_Transactions (nolock)
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
	AND Trade_Date>@IniDate AND Trade_Date<=@EndDate
	GROUP BY Id_Ticker,[Id Book],[Id Section]
) TRADES
ON INI.Id_Ticker_Initial=TRADES.Id_Ticker_Trades
AND INI.[Id Book] =TRADES.[Id Book] 
AND INI.[Id Section]=TRADES.[Id Section]
OR( FINAL.Id_Ticker_HistTable=TRADES.Id_Ticker_Trades
AND FINAL.[Id Book] =TRADES.[Id Book] 
AND FINAL.[Id Section]=TRADES.[Id Section])

LEFT JOIN dbo.Tb001_Securities C (nolock)
ON COALESCE(TRADES.Id_Ticker_Trades, FINAL.Id_Ticker_HistTable, INI.Id_Ticker_Initial)=C.IdSecurity
LEFT JOIN Tb400_Books D
ON INI.[Id Book] = D.[Id_Book] 
LEFT JOIN Tb400_Books E
ON TRADES.[Id Book] = E.[Id_Book] 
LEFT JOIN Tb400_Books F
ON FINAL.[Id Book] = F.[Id_Book]

ORDER BY ABS(convert(numeric,coalesce(Quant_Final,0))-convert(numeric,coalesce(Quant_Trades,0))-convert(numeric,coalesce(Quant_Initial,0))) DESC, Ticker
