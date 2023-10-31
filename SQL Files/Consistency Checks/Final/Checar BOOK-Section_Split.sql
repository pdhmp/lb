DECLARE @IniDate datetime
DECLARE @EndDate datetime
DECLARE @Id_Portfolio int

SET @IniDate='20100104'
SET @EndDate='20100622'
SET @Id_Portfolio=10

SELECT Coalesce(A.Id_Ticker_Trades,Id_Ticker_Split,Id_Ticker_Hist)Id_Ticker,C.Simbolo,D.Id_Book,D.Book AS Book_Trades,G.Id_Section,G.Section as Section_Trade,convert(int,coalesce(Quant_Trades,0))Quant_Trades,Id_Ticker_Split,F.Id_Book,F.Book AS Book_Split,I.Id_Section,I.Section as Section_Split, convert(int,coalesce(Quant_Split,0))Quant_Split,Id_Ticker_Hist,E.Id_Book,E.Book AS Book_Hist,H.Id_Section,H.Section as Section_Hist, convert(int,coalesce(Quant_Hist,0))Quant_Hist,convert(int,coalesce(Quant_Hist,0))-convert(int,coalesce(Quant_Trades,0))-convert(int,coalesce(Quant_Split,0)) AS DIFF
FROM 
(
	SELECT Id_Ticker AS Id_Ticker_Trades,[Id Book] AS Id_Book_Trades,[Id Section] as Id_Section_Trades,SUM(coalesce(Quantity,0)) AS Quant_Trades
	FROM (
			SELECT Id_Ticker,[Id Book],[Id Section] ,Quantity 
			FROM dbo.vw_Transactions
			WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
			AND Trade_Date>=@IniDate AND Trade_Date<=@EndDate and Split = 0
			UNION ALL
			SELECT [Id Ticker],[Id Book],[Id Section] , Position
			FROM NESTDB.dbo.Tb000_Historical_Positions
			WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@IniDate
		)F
	GROUP BY Id_Ticker,[Id Book],[Id Section]
) A
FULL OUTER JOIN 
(
	SELECT Id_Ticker AS Id_Ticker_Split,[Id Book] AS Id_Book_Split,[Id Section] as Id_Section_Split,SUM(coalesce(Quantity,0)) AS Quant_Split
	FROM dbo.vw_Transactions
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
	AND Trade_Date>=@IniDate AND Trade_Date<=@EndDate  and Split = 1
	GROUP BY Id_Ticker,[Id Book],[Id Section]
) W
ON A.Id_Ticker_Trades = W.Id_Ticker_Split AND A.Id_Book_Trades= W.Id_Book_Split  AND A.Id_Section_Trades= W.Id_Section_Split
FULL OUTER JOIN 
(
	SELECT [Id Ticker] AS Id_Ticker_Hist,[Id Book] AS Id_Book_Hist,[Id Section] as Id_Section_Hist, SUM(Position) AS Quant_Hist
	FROM
	(
		SELECT [Id Ticker],[Id Book],[Id Section] , Position
		FROM NESTDB.dbo.Tb000_Historical_Positions
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
		UNION ALL 
		SELECT [Id Ticker],[Id Book],[Id Section] , Position
		FROM NESTRT.dbo.Tb000_Posicao_Atual 
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
	) X
	GROUP BY [Id Ticker],[Id Book],[Id Section] 
) B
ON A.Id_Ticker_Trades=B.Id_Ticker_Hist AND A.Id_Book_Trades = B.Id_Book_Hist AND A.Id_Section_Trades = B.Id_Section_Hist
--AND  B.Id_Ticker_Hist = W.Id_Ticker_Split AND B.Id_Book_Hist= W.Id_Book_Split

LEFT JOIN dbo.Tb001_Ativos C
ON Coalesce(A.Id_Ticker_Trades,Id_Ticker_Split,Id_Ticker_Hist)=C.Id_Ativo

LEFT JOIN Tb400_Books D
ON A.Id_Book_Trades = D.[Id_Book]

LEFT JOIN Tb400_Books E
ON B.Id_Book_Hist = E.[Id_Book]

LEFT JOIN Tb400_Books F
ON W.Id_Book_Split = F.[Id_Book]

LEFT JOIN dbo.Tb404_Section G ON A.Id_Section_Trades = G.[Id_Section]

LEFT JOIN dbo.Tb404_Section H ON B.Id_Section_Hist = H.[Id_Section]

LEFT JOIN dbo.Tb404_Section I ON W.Id_Section_Split = I.[Id_Section]
ORDER BY --D.[Id_Book],
Simbolo,convert(int,coalesce(Quant_Hist,0))-convert(int,coalesce(Quant_Trades,0))-convert(int,coalesce(Quant_Split,0))
--Coalesce(A.Id_Ticker_Trades,Id_Ticker_Split,Id_Ticker_Hist),
--,Book_Hist,Book_Split--,
--ABS(coalesce(Quant_Hist,0)-coalesce(Quant_Trades,0)) DESC

/*
		SELECT *
		FROM NESTDB.dbo.Tb000_Tradesorical_Positions
		WHERE [Id Portfolio]=4 AND [Date Now]='20090108'
		ORDER BY tICKER


Select * from dbo.Tb051_Preco_Acoes_Offshore
Where Id_Ativo =31385
order by Data_hora_reg desc

Select * from NESTRT.dbo.Tb065_Ultimo_Preco
Where Id_Ativo =31385
order by Data_hora_reg desc
*/