DECLARE @IDTICKER INT
DECLARE @IDCOMPOSITE INT
DECLARE @DATE DATETIME
DECLARE @LASTDATE DATETIME
DECLARE @NEWWEIGHT FLOAT
DECLARE @TICKER varchar(8)

SET @IDTICKER = 160004
SET @IDCOMPOSITE = 16316
SET @DATE = '20120228'
SET @NEWWEIGHT = 0
set @TICKER = 'QUAL3'


SELECT @LASTDATE = MAX(DATE_REF) FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION
WHERE ID_TICKER_COMPONENT = @IDTICKER
AND ID_TICKER_COMPOSITE = @IDCOMPOSITE

INSERT INTO NESTDB.DBO.TB023_SECURITIES_COMPOSITION
SELECT	
	@DATE,
	ID_TICKER_COMPOSITE,
	ID_TICKER_COMPONENT,
	CASE WHEN ID_TICKER_COMPONENT = @IDTICKER THEN @NEWWEIGHT ELSE WEIGHT END,
	QUANTITY,
	@TICKER
FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION
WHERE ID_TICKER_COMPOSITE = @IDCOMPOSITE
AND DATE_REF = @LASTDATE


 