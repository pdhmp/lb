DECLARE @IniDate datetime
DECLARE @EndDate datetime
DECLARE @Id_Portfolio int

SET @IniDate='19000101'
--SET @EndDate=CONVERT(varchar,getdate(),112)
SET @EndDate='20091230'
SET @Id_Portfolio=10

--2765300
SELECT A.*, B.*, C.Simbolo,Book,convert(int,coalesce(CurQuant,0))-convert(int,coalesce(HisQuant,0)) AS DIFF
FROM 
(
	SELECT Id_Ticker,[Id Book],SUM(coalesce(Quantity,0)) AS HisQuant
	FROM dbo.vw_Transactions
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)
	AND Trade_Date>=@IniDate AND Trade_Date<=@EndDate
	GROUP BY Id_Ticker,[Id Book]
) A
FULL OUTER JOIN 
(
	SELECT [Id Ticker],[Id Book], SUM(Position) AS CurQuant
	FROM
	(
		SELECT [Id Ticker],[Id Book], Position
		FROM NESTDB.dbo.Tb000_Historical_Positions
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
		UNION ALL 
		SELECT [Id Ticker],[Id Book], Position
		FROM NESTRT.dbo.Tb000_Posicao_Atual 
		WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@EndDate
	) X
	GROUP BY [Id Ticker],[Id Book]
) B
ON A.Id_Ticker=B.[Id Ticker]
LEFT JOIN dbo.Tb001_Ativos C
ON A.Id_Ticker=C.Id_Ativo

LEFT JOIN Tb400_Books D
ON A.[Id Book] = D.[Id_Book] 

ORDER BY ABS(coalesce(CurQuant,0)-coalesce(HisQuant,0)) DESC ,Simbolo

/*
		SELECT *
		FROM NESTDB.dbo.Tb000_Historical_Positions
		WHERE [Id Portfolio]=4 AND [Date Now]='20090108'
		ORDER BY tICKER
*/