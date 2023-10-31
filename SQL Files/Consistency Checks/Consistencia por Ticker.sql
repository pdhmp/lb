DECLARE @tempTable TABLE(dateRef datetime, Quantity numeric, QTradesOnly numeric)
DECLARE @Id_Portfolio int
DECLARE @Id_Ticker int 

SET @Id_Portfolio=10
SET @Id_Ticker=7


INSERT INTO @tempTable
	SELECT Trade_Date, SUM(Quantity) AS HisQuant, SUM(CASE WHEN Transaction_Type=10 THEN Quantity ELSE 0 END) AS QTradesOnly
	FROM dbo.vw_Transactions
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio) 
		AND Id_Ticker=@Id_Ticker
	GROUP BY Trade_Date


SELECT *, Hist_PositionLB-sumTransactions AS DIFF_ALL, Hist_PositionLB-sumTradesOnly AS DIFF_Trades
FROM 
(
	SELECT [Date Now], SUM(Position) AS Hist_PositionLB
		, (SELECT SUM(Quantity) FROM @tempTable WHERE dateRef<=A.[Date Now]) AS sumTransactions
		, (SELECT SUM(QTradesOnly) FROM @tempTable WHERE dateRef<=A.[Date Now]) AS sumTradesOnly
	FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker
	GROUP BY [Date Now]
) X
ORDER BY [Date Now]

