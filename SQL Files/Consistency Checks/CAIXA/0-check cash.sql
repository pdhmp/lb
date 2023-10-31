Declare @Id_Portfolio int
SET @Id_Portfolio=11
Declare @Date datetime
SET @Date='2010-09-02'
SELECT * FROM 
(
SELECT [ticker],[id ticker],[id instrument],[prev cash uc]
	, [prev asset p/l],[asset p/l uc],[cash uc],[Cash FLow]
	, -COALESCE((
			SELECT SUM(Quantity2) 
			FROM NESTDB.dbo.vw_Transactions_ALL 
			WHERE Id_Ticker1=A.[Id ticker] 
				AND [Id Book1]=A.[Id book] 
				AND [Id Section1]=A.[Id section] 
				AND Trade_Date=@Date AND Id_Ticker2=1844
				AND Id_Account1 IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)),0) 
+CASE WHEN A.[Id ticker]=1844 THEN (
			SELECT SUM(Quantity2) 
			FROM NESTDB.dbo.vw_Transactions_ALL 
			WHERE Trade_Date=@Date AND Id_Ticker2=1844
				AND Id_Account1 IN (SELECT Id_Account FROM NESTDB.dbo.VW_PortAccounts WHERE Id_Portfolio=@Id_Portfolio)) 
ELSE 0 END

		CashFlowFromTrans
	, round([cash uc]-[asset p/l uc]-[prev cash uc]-[Cash FLow],4) DIFF--, * 
--FROM NESTRT.dbo.Tb000_Posicao_Atual A
FROM NESTDB.dbo.Tb000_Historical_Positions A
WHERE [id Portfolio]=@Id_Portfolio AND [Date now]=@Date
) A
ORDER BY ABS(DIFF) DESC, Ticker 
--ORDER BY Ticker DESC
