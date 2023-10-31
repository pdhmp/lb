DECLARE @IniDate datetime
DECLARE @EndDate datetime
DECLARE @Id_Portfolio int

SET @IniDate='19000101'
SET @EndDate='20091231'
SET @Id_Portfolio=10


SELECT Coalesce(A.Id_Ticker_Trades,Id_Ticker_Split,Id_Ticker_Hist),C.Simbolo,D.Estrategia AS Estrategia_Trades,convert(int,coalesce(Quant_Trades,0)),F.Estrategia AS Estrategia_Split, convert(int,coalesce(Quant_Split,0)),E.Estrategia AS Estrategia_Hist, convert(int,coalesce(Quant_Hist,0)),convert(int,coalesce(Quant_Hist,0))-convert(int,coalesce(Quant_Trades,0))-convert(int,coalesce(Quant_Split,0)) AS DIFF
FROM 
(
	SELECT Id_Ticker AS Id_Ticker_Trades,Estrategia AS Id_Estrategia_Trades,SUM(coalesce(Quantity,0)) AS Quant_Trades
	FROM dbo.vw_Transactions_STRATEGIA
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
	AND Trade_Date>=@IniDate AND Trade_Date<=@EndDate and Split = 0
	GROUP BY Id_Ticker,Estrategia
) A
FULL OUTER JOIN 
(
	SELECT Id_Ticker AS Id_Ticker_Split,Estrategia AS Id_Estrategia_Split,SUM(coalesce(Quantity,0)) AS Quant_Split
	FROM dbo.vw_Transactions_STRATEGIA
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
	AND Trade_Date>=@IniDate AND Trade_Date<=@EndDate  and Split = 1
	GROUP BY Id_Ticker,Estrategia
) W
ON A.Id_Ticker_Trades = W.Id_Ticker_Split AND A.Id_Estrategia_Trades= W.Id_Estrategia_Split
FULL OUTER JOIN 
(
	SELECT [Id Ticker] AS Id_Ticker_Hist,Estrategia AS Id_Estrategia_Hist, SUM(Position) AS Quant_Hist
	FROM
	(
		SELECT [Id Ticker],[ZId Strategy] as Estrategia, Position
		FROM NESTDB.dbo.Tb000_Historical_Positions
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
		UNION ALL 
		SELECT [Id Ticker],[ZId Strategy], Position
		FROM NESTRT.dbo.Tb000_Posicao_Atual 
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
	) X
	GROUP BY [Id Ticker],Estrategia
) B
ON A.Id_Ticker_Trades=B.Id_Ticker_Hist AND A.Id_Estrategia_Trades = B.Id_Estrategia_Hist
--AND  B.Id_Ticker_Hist = W.Id_Ticker_Split AND B.Id_Estrategia_Hist= W.Id_Estrategia_Split

LEFT JOIN dbo.Tb001_Ativos C
ON Coalesce(A.Id_Ticker_Trades,Id_Ticker_Split,Id_Ticker_Hist)=C.Id_Ativo

LEFT JOIN Tb111_Estrategia D
ON A.Id_Estrategia_Trades = D.[Id_Estrategia] 

LEFT JOIN Tb111_Estrategia E
ON B.Id_Estrategia_Hist = E.[Id_Estrategia] 

LEFT JOIN Tb111_Estrategia F
ON W.Id_Estrategia_Split = F.[Id_Estrategia] 

ORDER BY --Coalesce(A.Id_Ticker_Trades,Id_Ticker_Split,Id_Ticker_Hist),
Diff,sIMBOLO
--Estrategia_Trades,Estrategia_Hist,Estrategia_Split--,
--ABS(coalesce(Quant_Hist,0)-coalesce(Quant_Trades,0)) DESC

/*
		SELECT *
		FROM NESTDB.dbo.Tb000_Tradesorical_Positions
		WHERE [Id Portfolio]=4 AND [Date Now]='20090108'
		ORDER BY tICKER
*/