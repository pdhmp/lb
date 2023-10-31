USE NESTDB

DECLARE @tempTable TABLE(dateRef datetime, Quantity numeric, QTradesOnly numeric)
DECLARE @Id_Portfolio int
DECLARE @Id_Ticker int 

SET @Id_Portfolio=10
SET @Id_Ticker=1844

DECLARE @LastDate datetime
DECLARE @FinalPos float

SELECT @LastDate=MAX([Date Now])
FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker

INSERT INTO @tempTable
	SELECT Trade_Date, SUM(Quantity) AS HisQuant, SUM(CASE WHEN Transaction_Type=10 THEN Quantity ELSE 0 END) AS QTradesOnly
	FROM dbo.vw_Transactions
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio) 
		AND Id_Ticker=@Id_Ticker AND Trade_Date>='2010-01-01' AND Trade_Date<=@LastDate
	GROUP BY Trade_Date

SELECT @FinalPos=SUM(Position)
FROM dbo.Tb000_Historical_Positions A
WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker AND [Date Now]=@LastDate

SELECT *, Hist_PositionLB-sumTransactions AS DIFF
FROM 
(
	SELECT [Date Now], SUM(Position) AS Hist_PositionLB
		, @FinalPos-COALESCE((SELECT SUM(Quantity) FROM @tempTable WHERE dateRef>A.[Date Now]),0) AS sumTransactions
	FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker
	GROUP BY [Date Now]
) X
ORDER BY  [Date Now]