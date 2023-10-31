DECLARE @AllContacts table(IdContact numeric)
DECLARE @IdContact numeric
DECLARE @ReturnTable TABLE(IdContact numeric, Fund varchar(5), rowNumber int, Transaction_Id int, curDate datetime, Quantity decimal(18,8), curNAV decimal(18,8), Transaction_Type int, cumQuant decimal(18,8), Last_NAV float, noDays int, SubBench float, RedBench float, PerfFund float, PerfBench float, origValue decimal(18,2), curValue decimal(18,2), Excess float)


INSERT INTO @AllContacts
SELECT Id_Contact FROM NESTDB.dbo.FCN_COM_Contact_Summary() WHERE Id_Portfolio IN (3, 15) AND Quantity>0.01


WHILE EXISTS(SELECT top 1 IdContact FROM @AllContacts)
	BEGIN
		SELECT top 1 @IdContact=IdContact FROM @AllContacts 

		INSERT INTO @ReturnTable
		SELECT @IdContact,'MH30', A.*, PerfFund-PerfBench AS Excess
		FROM NESTDB.dbo.FCN_COM_ContactSubSummary(@IdContact, 3, 0) A

		INSERT INTO @ReturnTable
		SELECT @IdContact,'MH1', A.*, PerfFund-PerfBench AS Excess
		FROM NESTDB.dbo.FCN_COM_ContactSubSummary(@IdContact, 15, 0) A

		INSERT INTO @ReturnTable
		SELECT @IdContact,'FIA', A.*, PerfFund-PerfBench AS Excess
		FROM NESTDB.dbo.FCN_COM_ContactSubSummary(@IdContact, 12, 0) A

		DELETE FROM @AllContacts WHERE IdContact=@IdContact

	END

SELECT * FROM @ReturnTable
--
--SELECT A.*
--	, PerfFund-PerfBench AS Excess
--FROM NESTDB.dbo.FCN_COM_ContactSubSummary(B.Id_Contact, 3, 0) A
--INNER JOIN 
--( 
--	SELECT * FROM NESTDB.dbo.FCN_COM_Contact_Summary()
--	WHERE Id_Portfolio IN (3, 15) AND Quantity>0.01
--) B
--ON A.IdContact=B.Id_Contact