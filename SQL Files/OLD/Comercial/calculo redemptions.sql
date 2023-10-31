set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go





CREATE FUNCTION [dbo].[FCN_GET_Redemptions] (@Id_Portfolio datetime, @iniDate datetime, @endDate datetime)

RETURNS @tmpTable table(Id_Portfolio numeric, Id_Contact numeric, ContactName varchar(100), Id_Distributor int, subDate datetime, redDate datetime, Months int, PenaltyRate int, DistributorName varchar(40), FinAmount float, RebateManagement float, PortfolioName varchar(40), NetFinAmount float, GrossManagIncome float, Rebate float, MngIncomeLessRebate float, IR float, NetMngIncome float, PenaltyValue float)
AS
BEGIN
/*
DECLARE @iniDate datetime
SET @iniDate='2009-12-31'

DECLARE @endDate datetime
SET @endDate='2010-01-31'

DECLARE @Id_Portfolio datetime
SET @Id_Portfolio=2
*/

DECLARE @tempTable TABLE(Id_Portfolio numeric , Id_Contact numeric)
DECLARE @RepTable TABLE(Id_Portfolio numeric , Id_Contact numeric, subDate datetime, redDate datetime, subNAV decimal(18,8), redNAV decimal(18,8), subQuant decimal(18,8), subValue decimal(18,8), redValue decimal(18,8))

DECLARE @curContact numeric
DECLARE @curPortfolio numeric

INSERT INTO @tempTable
SELECT Id_Portfolio, Id_Contact  
FROM dbo.Tb760_Subscriptions_Mellon 
WHERE Transaction_Type IN (31, 32) AND Settlement_Date>=@iniDate AND Settlement_Date<=@endDate AND Id_Portfolio=@Id_Portfolio
GROUP BY Id_Portfolio, Id_Contact
ORDER BY Id_Portfolio, Id_Contact

WHILE EXISTS(SELECT TOP 1 Id_Contact FROM @tempTable)
	BEGIN
		SELECT TOP 1 @curContact=Id_Contact, @curPortfolio=Id_Portfolio FROM @tempTable

		INSERT INTO @RepTable 
		SELECT @curPortfolio, @curContact, subDate, redDate, subNAV, redNAV, alocQuantity, subNAV*alocQuantity, redNAV*alocQuantity 
		FROM [dbo].[ContactRedSummary](@curContact, @curPortfolio, 1)
		WHERE Transaction_Type IN (31, 32) AND redDate>=@iniDate AND redDate<=@endDate 

		DELETE FROM @tempTable WHERE Id_Contact=@curContact AND Id_Portfolio=@curPortfolio

	END

INSERT INTO @tmpTable
SELECT Z.*
	, Port_Name
	, Fin_Amount*(1-Rebate_Manag) AS Net_Fin_Amount
	, Fin_Amount*Management_Fee/12 AS Gross_Mng_Income
	, Fin_Amount*Management_Fee/12*Rebate_Manag AS Rebate
	, Fin_Amount*Management_Fee/12*(1-Rebate_Manag) AS Mng_Income_Less_Rebate
	, Fin_Amount*Management_Fee/12*(1-Rebate_Manag)*(0.1708) AS IR
	, (Fin_Amount*(1-Rebate_Manag)*Management_Fee/12)*(1-0.1708) AS Net_Mng_Income
	, (Fin_Amount*(1-Rebate_Manag)*Management_Fee/12)*(1-0.1708)*PenaltyRate*1.5 AS PenaltyValue
FROM
(
	SELECT X.Id_Portfolio
		, X.Id_Contact
		, Contact_Name
		, E.Id_Distributor
		, subDate
		, redDate
		, Months
		, NESTDB.dbo.FCN_Dist_Penalty(subDate, Months) AS PenaltyRate
		, DistributorName
		, subValue AS Fin_Amount
		, NESTDB.dbo.FCN_DistRebate(subDate, E.Id_Distributor, X.Id_Portfolio, 'MANG') Rebate_Manag
	FROM 
	(
		SELECT * , DATEDIFF(m, DATEADD(DD, 1 - DAY(subDate), subDate), DATEADD(DD, 1 - DAY(redDate), redDate)) AS Months
		FROM @RepTable
	) X
	LEFT JOIN NESTDB.dbo.Tb751_Contacts C
		ON X.Id_Contact=C.Id_Contact
	LEFT JOIN NESTDB.dbo.FCN_DistContacts(@endDate) E
		ON X.Id_Contact=E.Id_Contact
	LEFT JOIN NESTDB.dbo.Tb750_Distributors F
		ON E.Id_Distributor=F.Id_Distributor
	WHERE redDate<=@endDate
) Z
LEFT JOIN NESTDB.dbo.Tb002_Portfolios D
	ON Z.Id_Portfolio=D.Id_Portfolio
ORDER BY Id_Portfolio, redDate

RETURN

END