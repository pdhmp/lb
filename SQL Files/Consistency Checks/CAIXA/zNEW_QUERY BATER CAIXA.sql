
/*
SELECT sum([cash uc]) 
FROM NESTDB.dbo.Tb000_Historical_Positions
WHERE [date now]='2010-06-21' AND [id portfolio]=10

SELECT sum(cash) , sum([prev cash uc]) 
--FROM NESTRT.dbo.Tb000_Posicao_Atual
FROM NESTDB.dbo.Tb000_Historical_Positions
WHERE [date now]='2010-06-22' AND [id portfolio]=10
*/

DECLARE @RepDate datetime
SET @RepDate='2010-06-24'


SELECT COALESCE([id ticker], TAB2.Id_Ativo) AS [id ticker]
	, Simbolo
	, Id_Instrumento
	, Nome
	, [prev Cash uc] AS prevValue
	, [Cash uc] AS curValue
	, [Cash uc]-[prev Cash uc] AS Change
	, [Asset P/L uc] AS PL
	, [prev Asset P/L] AS PLP
	, COALESCE(CashChange,0)+COALESCE(CashChange2,0) AS CashFlow
	, TradeFlow AS TradeFlow  
FROM 
(
	SELECT *
	FROM NESTDB.dbo.Tb000_Historical_Positions
	--FROM NESTRT.dbo.Tb000_Posicao_Atual
	WHERE [date now]=@RepDate AND [id portfolio]=43 
) TAB1
FULL OUTER JOIN
(
	SELECT Id_Ticker1 AS Id_Ativo, [Id Book1], [Id Section1], SUM(Quantity2) AS CashChange
	FROM NESTDB.dbo.vw_Transactions_ALL
	WHERE Trade_Date=@RepDate AND Id_Account1 IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio=43)
	GROUP BY Id_Ticker1, [Id Book1], [Id Section1]
) TAB2
ON TAB1.[id ticker]=TAB2.Id_Ativo
AND TAB1.[id book]=TAB2.[id book1]
AND TAB1.[id section]=TAB2.[id section1]
FULL OUTER JOIN
(
	SELECT Id_Ticker2 AS Id_Ativo, [Id Book2], [Id Section2], SUM(Quantity1) AS CashChange2
	FROM NESTDB.dbo.vw_Transactions_ALL
	WHERE Trade_Date=@RepDate AND Id_Account2 IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio=43)
	GROUP BY Id_Ticker2, [Id Book2], [Id Section2]
) TAB2b
ON TAB1.[id ticker]=TAB2b.Id_Ativo
AND TAB1.[id book]=TAB2b.[id book2]
AND TAB1.[id section]=TAB2b.[id section2]
FULL OUTER JOIN 
(
	SELECT A.Id_Ativo, [id book], [id section], -SUM(Cash) AS TradeFlow
	FROM NESTDB.dbo.VW_ORDENS_ALL A
	WHERE Data_Trade=@RepDate AND Id_Account IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio=43)AND Id_Port_Type=1
	GROUP BY A.Id_Ativo, [id book], [id section]
) TAB3
ON TAB1.[id ticker]=TAB3.Id_Ativo
AND TAB1.[id book]=TAB3.[id book]
AND TAB1.[id section]=TAB3.[id section]
LEFT JOIN NESTDB.dbo.Tb001_Ativos TAB4
ON COALESCE(TAB1.[id ticker], TAB2.Id_Ativo, TAB2b.Id_Ativo, TAB3.Id_Ativo)=TAB4.Id_Ativo
ORDER BY Simbolo

