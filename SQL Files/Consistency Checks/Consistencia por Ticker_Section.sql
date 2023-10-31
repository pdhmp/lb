DECLARE @tempTable TABLE(dateRef datetime,[Id Book] int,[Id Section] int, Quantity numeric, QTradesOnly numeric)
DECLARE @Id_Portfolio int
DECLARE @Id_Ticker int 
DECLARE @IniDate datetime

SET @Id_Portfolio=4
SET @Id_Ticker=439
SET @IniDate='2010-01-04'

INSERT INTO @tempTable
	SELECT [Date Now],[Id Book],[Id Section], SUM(Position) AS HisQuant, SUM(Position) AS QTradesOnly
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Id Portfolio]=@Id_Portfolio AND [Date Now]=@IniDate AND [Id Ticker]=@Id_Ticker
	GROUP BY [Date Now],[Id Book],[Id Section]

INSERT INTO @tempTable
	SELECT Trade_Date,[Id Book],[Id Section], SUM(Quantity) AS HisQuant, SUM(CASE WHEN Transaction_Type=10 THEN Quantity ELSE 0 END) AS QTradesOnly
	FROM dbo.vw_Transactions
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio) 
		AND Id_Ticker=@Id_Ticker AND Trade_Date>@IniDate
	GROUP BY Trade_Date,[Id Book],[Id Section]

SELECT *, Hist_PositionLB-sumTransactions AS DIFF_ALL, Hist_PositionLB-sumTradesOnly AS DIFF_Trades
FROM 
(
	SELECT [Date Now],[Id Book],[Id Section], SUM(Position) AS Hist_PositionLB
		, (SELECT SUM(Quantity) FROM @tempTable WHERE dateRef<=A.[Date Now] AND [Id Book]=A.[Id Book] AND [Id Section]=A.[Id Section]) AS sumTransactions
		, (SELECT SUM(QTradesOnly) FROM @tempTable WHERE dateRef<=A.[Date Now] AND [Id Book]=A.[Id Book] AND [Id Section]=A.[Id Section]) AS sumTradesOnly
	FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker AND [Date Now]>=@IniDate
	GROUP BY [Date Now],[Id Book],[Id Section]
) X
ORDER BY [Date Now],[Id Book],[Id Section]

