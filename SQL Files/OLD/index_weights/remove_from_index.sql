DECLARE @DATE AS DATETIME
DECLARE @MAINCOMPOSITE AS INT
DECLARE @COMPOSITE AS INT
DECLARE @COMPONENT AS INT
DECLARE @WEIGHT AS FLOAT

SET @DATE = '20100413'
SET @MAINCOMPOSITE = 21350
SET @COMPOSITE = 16316
SET @COMPONENT = 1238

INSERT INTO NESTDB.DBO.TB023_SECURITIES_COMPOSITION
(DATE_REF,ID_TICKER_COMPOSITE,ID_TICKER_COMPONENT,WEIGHT,QUANTITY)
VALUES(@DATE,@COMPOSITE,@COMPONENT,0,0);

SELECT @WEIGHT = SUM(WEIGHT) from
(SELECT * FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION where ID_TICKER_COMPOSITE = @COMPOSITE)a
INNER JOIN
(SELECT MAX(DATE_REF)DATE_REF,ID_TICKER_COMPONENT FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION 
WHERE dATE_REF <= @DATE  AND ID_TICKER_COMPOSITE = @COMPOSITE
group BY ID_TICKER_COMPONENT)B
on a.ID_TICKER_COMPONENT = b.ID_TICKER_COMPONENT and a.DATE_REF = b.DATE_REF

SELECT @WEIGHT

INSERT INTO NESTDB.DBO.TB023_SECURITIES_COMPOSITION
SELECT @DATE, @COMPOSITE, A.ID_TICKER_COMPONENT, a.weight/@weight adjweight, 0 from
(SELECT * FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION where ID_TICKER_COMPOSITE = @COMPOSITE)a
INNER JOIN
(SELECT MAX(DATE_REF)DATE_REF,ID_TICKER_COMPONENT FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION 
WHERE dATE_REF <= @DATE  AND ID_TICKER_COMPOSITE = @COMPOSITE
group BY ID_TICKER_COMPONENT)B
on a.ID_TICKER_COMPONENT = b.ID_TICKER_COMPONENT and a.DATE_REF = b.DATE_REF


INSERT INTO NESTDB.DBO.TB023_SECURITIES_COMPOSITION
SELECT @DATE, @MAINCOMPOSITE, A.ID_TICKER_COMPONENT, a.weight*@weight adjweight, 0 from
(SELECT * FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION 
where ID_TICKER_COMPOSITE = @MAINCOMPOSITE
AND ID_TICKER_COMPONENT = @COMPOSITE)a
INNER JOIN
(SELECT MAX(DATE_REF)DATE_REF,ID_TICKER_COMPONENT FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION 
WHERE dATE_REF <= @DATE  AND ID_TICKER_COMPOSITE = @MAINCOMPOSITE
group BY ID_TICKER_COMPONENT)B
on a.ID_TICKER_COMPONENT = b.ID_TICKER_COMPONENT and a.DATE_REF = b.DATE_REF



--select * from nestdb.dbo.tb023_securities_composition
--where id_ticker_composite = 16323 
--or id_ticker_component = 16323
--order by date_ref desc, id_ticker_component asc

--DELETE FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION
--WHERE ID_TICKER_COMPOSITE = 16323
--and date_ref = '20100413'