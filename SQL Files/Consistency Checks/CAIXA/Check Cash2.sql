SELECT * FROM 
(
SELECT Coalesce(A.Ticker,B.Ticker) AS Ticker, A.Cash as Cash_D0, B.Cash as Cash_D1, B.[Total P/L] AS PL
from 
(
	SELECT Ticker,sum(Cash) as Cash, SUM([Total P/L]) AS [Total P/L]
	from NESTDB.dbo.Tb000_Historical_Positions 
	Where [id Portfolio]=10 and [Date now]='20101108'
	group by [Ticker]
)A
FULL OUTER JOIN 
(
	SELECT Ticker,sum(Cash) as Cash, SUM(coalesce([Total P/L],0)) AS [Total P/L]
	from NESTRT.dbo.Tb000_Posicao_Atual
	Where [id Portfolio]=10
	group by [Ticker]
)B
ON A.Ticker = B.Ticker
) X
FULL OUTER JOIN 
(
	Select C.NestTicker AS Ticker, replace(-sum(coalesce((B.Quantidade/RoundLot)*B.Preco,0)),'.',',') AS CashFlow
	from NESTDB.dbo.Tb012_Ordens A
	FULL OUTER JOIN NESTDB.dbo.Tb013_Trades B
	ON A.Id_Ordem = B.Id_Ordem
	INNER JOIN NESTDB.dbo.Tb001_Securities C
	ON A.Id_Ativo = C.IdSecurity
	INNER JOIN NESTDB.dbo.Tb003_PortAccounts D
	ON A.Id_Account = D. Id_Account
	Where Data_Trade = '20101108'
	AND Id_Portfolio=10
	group by C.NestTicker
) Y
ON X.Ticker = Y.Ticker
ORDER by Coalesce(X.Ticker,Y.Ticker)
