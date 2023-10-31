

DECLARE @RepDate datetime
SET @RepDate='2009-12-31'

DECLARE @Id_Portfolio numeric
SET @Id_Portfolio=10

SELECT X.Id_Portfolio
	, Port_Name
	, X.Id_Contact
	, Contact_Name
	, E.Id_Distributor
	, curDate
	, DistributorName
	, Fin_Amount
	, Rebate_Manag
	, Fin_Amount*Management_Fee/12 AS Management_Income
	, Fin_Amount*Management_Fee/12*Rebate_Manag AS Rebate
	, Fin_Amount*Management_Fee/12*(1-Rebate_Manag) AS NetIncome
	, 1
	, Fin_Amount*Management_Fee/12*(1-Rebate_Manag)*1 AS Comission
	, Fin_Amount*Management_Fee/12*(1-Rebate_Manag)*1*(0.1708) AS IR
	, Fin_Amount*Management_Fee/12*(1-Rebate_Manag)*1*(1-0.1708) AS NetOfIR
FROM 
(
	SELECT DATEADD(DD, 1 - DAY(Settlement_Date), Settlement_Date) AS curDate, Id_Portfolio, Id_Contact, SUM(Fin_Amount) AS Fin_Amount
	from  NESTDB.dbo.Tb760_Subscriptions_Mellon  A
	WHERE Settlement_Date<='2009-12-31' AND Id_Portfolio=@Id_Portfolio AND Transaction_Type=30
	GROUP BY DATEADD(DD, 1 - DAY(Settlement_Date), Settlement_Date), Id_Portfolio, Id_Contact
) X
LEFT JOIN NESTDB.dbo.Tb751_Contacts C
	ON X.Id_Contact=C.Id_Contact
LEFT JOIN NESTDB.dbo.Tb002_Portfolios D
	ON X.Id_Portfolio=D.Id_Portfolio
LEFT JOIN NESTDB.dbo.FCN_DistContacts(@RepDate) E
	ON X.Id_Contact=E.Id_Contact
LEFT JOIN NESTDB.dbo.Tb750_Distributors F
	ON E.Id_Distributor=F.Id_Distributor
LEFT JOIN NESTDB.dbo.FCN_DistRebates(@RepDate, @Id_Portfolio) G
	ON E.Id_Distributor=G.Id_Distributor
WHERE curDate<=@RepDate
ORDER BY F.Id_Distributor DESC


/*
DECLARE @RedsTable TABLE(Id_Portfolio int, Id_Contact int, Transaction_Id int, redDate datetime, origQuantity decimal(18,8), redNAV decimal(18,8), alocQuantity decimal(18,8), subID int, subDate datetime, subNAV decimal(18,8), Transaction_Type int, noDays int, SubBench float, RedBench float, PerfFund float, PerfBench float)
DECLARE @SubsTable TABLE(Id_Portfolio int, Id_Contact int, rowNumber int, Transaction_Id int, curDate datetime, Quantity decimal(18,8), curNAV decimal(18,8), Transaction_Type int, cumQuant decimal(18,8), Last_NAV float, noDays int, SubBench float, RedBench float, PerfFund float, PerfBench float, origValue decimal(18,2), curValue decimal(18,2))
DECLARE @tempTable TABLE(Id_Contact numeric)
DECLARE @curContact numeric

INSERT INTO @tempTable
SELECT Id_Contact FROM dbo.Tb760_Subscriptions_Mellon WHERE Id_Portfolio=@Id_Portfolio GROUP BY Id_Contact

WHILE EXISTS(SELECT TOP 1 Id_Contact FROM @tempTable)
	BEGIN
		SELECT TOP 1 @curContact=Id_Contact FROM @tempTable

		INSERT INTO @SubsTable SELECT @Id_Portfolio, @curContact, * FROM [dbo].[ContactSubSummary](@curContact, @Id_Portfolio, 1)

		--INSERT INTO @RedsTable SELECT @Id_Portfolio, @curContact, * FROM [dbo].[ContactRedSummary](@curContact, @Id_Portfolio, 1)

		DELETE FROM @tempTable WHERE Id_Contact=@curContact

	END

--SELECT * FROM @RedsTable

SELECT X.Id_Portfolio
	, Port_Name
	, X.Id_Contact
	, Contact_Name
	, E.Id_Distributor
	, curDate
	, NoMonths
	, origValue
	, DistributorName
	, Rebate_Manag
	, origValue*Management_Fee/12 AS Management_Income
	, origValue*Management_Fee/12*Rebate_Manag AS Rebate
	, origValue*Management_Fee/12*(1-Rebate_Manag) AS NetIncome
	, PayRate
	, origValue*Management_Fee/12*(1-Rebate_Manag)*PayRate AS Comission
	, origValue*Management_Fee/12*(1-Rebate_Manag)*PayRate*(0.1708) AS IR
	, origValue*Management_Fee/12*(1-Rebate_Manag)*PayRate*(1-0.1708) AS NetOfIR
FROM
(
	SELECT *,
		CASE 
			WHEN NoMonths>18 THEN 0 
			WHEN NoMonths>12 THEN 0.20 
			WHEN NoMonths>6 THEN 0.25
			WHEN NoMonths>0 THEN 0.30 
		END AS PayRate
	FROM 
	(
		SELECT *, DATEDIFF(m, curDate, @RepDate) + 1 AS NoMonths
		FROM @SubsTable 
		WHERE curValue<>0 
	) A
) X
LEFT JOIN NESTDB.dbo.Tb751_Contacts C
	ON X.Id_Contact=C.Id_Contact
LEFT JOIN NESTDB.dbo.Tb002_Portfolios D
	ON X.Id_Portfolio=D.Id_Portfolio
LEFT JOIN NESTDB.dbo.Tb752_DistContacts E
	ON X.Id_Contact=E.Id_Contact
LEFT JOIN NESTDB.dbo.Tb750_Distributors F
	ON E.Id_Distributor=F.Id_Distributor
LEFT JOIN NESTDB.dbo.Tb753_DistRebates G
	ON E.Id_Distributor=G.Id_Distributor
WHERE curDate<=@RepDate
ORDER BY comission
*/