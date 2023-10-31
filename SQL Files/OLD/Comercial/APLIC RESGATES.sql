
DECLARE @Id_Contact int
SET @Id_Contact=700

DECLARE @Id_Portfolio int
SET @Id_Portfolio=3

DECLARE @Id_Ticker_Fund int
DECLARE @Id_Ticker_Bench int

SELECT @Id_Ticker_Fund=CASE 
	WHEN @Id_Portfolio=2 THEN 4803
	WHEN @Id_Portfolio=3 THEN 4802
	WHEN @Id_Portfolio=10 THEN 4946
	WHEN @Id_Portfolio=15 THEN 5532
END

SELECT @Id_Ticker_Bench=CASE 
	WHEN @Id_Portfolio=2 THEN 5049
	WHEN @Id_Portfolio=3 THEN 5049
	WHEN @Id_Portfolio=10 THEN 1073
	WHEN @Id_Portfolio=15 THEN 5049
END

DECLARE @SubsTable TABLE(rowNumber int, Transaction_Id int, curDate datetime, Quantity decimal(18,8), curNAV decimal(18,8), Transaction_Type int, cumQuant decimal(18,8))
DECLARE @RedsTable TABLE(rowNumber int, Transaction_Id int, curDate datetime, Quantity decimal(18,8), curNAV decimal(18,8), Transaction_Type int, cumQuant decimal(18,8))

DECLARE @AlocTable TABLE(Transaction_Id int, redDate datetime, origQuantity decimal(18,8), redNAV decimal(18,8), alocQuantity decimal(18,8), subID int, subDate datetime, subNAV decimal(18,8), Transaction_Type int)

INSERT INTO @RedsTable
SELECT ROW_NUMBER() OVER (ORDER BY Z.Transaction_Id) AS rowNumber, *, 0 AS cumQuant
FROM 
(
	SELECT A.Transaction_Id, A.Trade_Date,
	-Quantity AS Quantity,
	--NESTDB.dbo.FCN_GET_PRICE_Value_Only(4946, Settlement_Date, 1, 0, 2, 0, 0) AS Transaction_NAV 
	Transaction_NAV AS Transaction_NAV, Transaction_Type
	from  NESTDB.dbo.Tb760_Subscriptions_Mellon  A
	LEFT JOIN NESTDB.dbo.Tb002_Portfolios B
	ON A.Id_Portfolio=B.Id_Portfolio
	LEFT JOIN NESTDB.dbo.Tb751_Contacts C
	ON A.Id_Contact=C.Id_Contact
	WHERE a.Id_Portfolio=@Id_Portfolio AND A.Id_Contact=@Id_Contact AND Transaction_Type<>30
) AS Z
ORDER BY Z.Trade_Date, Transaction_Id DESC

UPDATE B SET cumQuant=-new_cumQuant 
FROM 
(
SELECT *, (SELECT SUM(Quantity) 
FROM @RedsTable WHERE rowNumber<=t.rowNumber) AS new_cumQuant
FROM @RedsTable t
) A
INNER JOIN @RedsTable B 
ON A.rowNumber=B.rowNumber

INSERT INTO @SubsTable
SELECT ROW_NUMBER() OVER (ORDER BY Z.Transaction_Id) AS rowNumber, *, 0 AS cumQuant
FROM 
(
	SELECT A.Transaction_Id, A.Trade_Date,
	CASE WHEN Transaction_Type=30 THEN Quantity ELSE -Quantity END AS Quantity,
	--NESTDB.dbo.FCN_GET_PRICE_Value_Only(4946, Settlement_Date, 1, 0, 2, 0, 0) AS Transaction_NAV 
	Transaction_NAV AS Transaction_NAV, Transaction_Type
	from  NESTDB.dbo.Tb760_Subscriptions_Mellon  A
	LEFT JOIN NESTDB.dbo.Tb002_Portfolios B
	ON A.Id_Portfolio=B.Id_Portfolio
	LEFT JOIN NESTDB.dbo.Tb751_Contacts C
	ON A.Id_Contact=C.Id_Contact
	WHERE a.Id_Portfolio=@Id_Portfolio AND A.Id_Contact=@Id_Contact AND Transaction_Type=30
) AS Z
ORDER BY Z.Trade_Date, Transaction_Id

UPDATE B SET cumQuant=new_cumQuant 
FROM 
(
	SELECT *, (SELECT SUM(Quantity) 
	FROM @SubsTable WHERE rowNumber<=t.rowNumber) AS new_cumQuant
	FROM @SubsTable t
) A
INNER JOIN @SubsTable B 
ON A.rowNumber=B.rowNumber

DECLARE @curRow int
DECLARE @curTableID int
DECLARE @SubsID int
DECLARE @RedsID int

DECLARE @SubsDate datetime
DECLARE @SubsNAV decimal(18,8)

DECLARE @SubsQuantity decimal(18,8)
DECLARE @curQuantity decimal(18,8)

SET @curRow=0

DECLARE @Quantitycheck decimal(18,8)
SET @Quantitycheck=0

WHILE @curRow<100
	BEGIN
		SET @curRow=@curRow+1
		SELECT @RedsID=Transaction_ID, @curQuantity=-Quantity FROM @RedsTable WHERE rowNumber=@curRow
		
		WHILE @curQuantity>0
			BEGIN
				SELECT TOP 1 @SubsID=Transaction_ID, @SubsQuantity=Quantity, @SubsDate=curDate, @SubsNAV=curNAV FROM @SubsTable WHERE Quantity<>0 ORDER BY curDate, Transaction_Id desc
				IF @SubsQuantity>@curQuantity
					BEGIN
						UPDATE @SubsTable SET Quantity=Quantity-@curQuantity WHERE Transaction_ID=@SubsID
						INSERT INTO @AlocTable SELECT Transaction_ID, curDate, -Quantity, curNAV, @curQuantity, @SubsID, @SubsDate, @SubsNAV, Transaction_Type int FROM @RedsTable WHERE Transaction_ID=@RedsID
						SET @curQuantity=0
					END
				ELSE
					BEGIN
						UPDATE @SubsTable SET Quantity=0 WHERE Transaction_ID=@SubsID
						INSERT INTO @AlocTable SELECT Transaction_ID, curDate, -Quantity, curNAV, @curQuantity, @SubsID, @SubsDate, @SubsNAV, Transaction_Type int FROM @RedsTable WHERE Transaction_ID=@RedsID
						SET @curQuantity=@curQuantity-@SubsQuantity
					END
			END
	END

SELECT *, Last_NAV/curNAV-1 AS PerfFund, RedBench/SubBench-1 AS PerfBench, Quantity*curNAV AS origValue, Quantity*Last_NAV AS curValue FROM
(
	SELECT *, 
	NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Fund, '2020-12-31', 1, 0, 2, 0, 0) AS Last_NAV,
	DATEDIFF(d, curDate, getdate()) AS noDays,
	NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Bench, curDate, 1, 0, 2, 0, 0) AS SubBench,
	NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Bench, getdate(), 1, 0, 2, 0, 0) AS RedBench
	FROM @SubsTable
) AS Z

SELECT *, redNAV/subNAV-1 AS PerfFund, RedBench/SubBench-1 AS PerfBench FROM
(
	SELECT *, 
	DATEDIFF(d, subDate, redDate) AS noDays,
	NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Bench, subDate, 1, 0, 2, 0, 0) AS SubBench,
	NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Bench, redDate, 1, 0, 2, 0, 0) AS RedBench
	FROM @AlocTable
) AS A

