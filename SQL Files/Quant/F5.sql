
DECLARE @Id_Ticker int
DECLARE @WindowSize int
DECLARE @VolWindow int
DECLARE @RoundValue int
DECLARE @StartDate datetime
DECLARE @tempTable TABLE
(	
	RowNumber int PRIMARY KEY, Data_Hora_Reg datetime, Expiration datetime, NoTrades int, OpenInterest int, 
	OpenSettl numeric(10,2), Sett numeric(10,2), AdjSett numeric(10,2), 
	LastRate float, PrevLastRate float, RateSett float, RateAdjSett float,
	MaxRate float, MinRate float, RateDev float, Duration float,
	BuySignal int, SellSignal int,
	curSide int, Contracts int, PnL numeric(10,2), totNAV numeric(18,2)
)

SET @Id_Ticker=5007
SET @WindowSize=17
SET @VolWindow=12
SET @RoundValue=5

SELECT @StartDate=MIN(Data_Hora_Reg) FROM Tb059_Precos_Futuros WHERE Tipo_Preco=90 AND Id_Ativo=@Id_Ticker AND Valor>=30000

INSERT INTO @tempTable
SELECT * FROM 
(
	SELECT RowNumber = ROW_NUMBER() OVER (order by Data_Hora_Reg ASC), Data_Hora_Reg, Vencimento AS Expiration,
		MAX(CASE WHEN Tipo_Preco=13 THEN Valor ELSE null END) as NoTrades,
		MAX(CASE WHEN Tipo_Preco=90 THEN Valor ELSE null END) as OpenInterest,
		null as OpenSettl, 
		MAX(CASE WHEN Tipo_Preco=15 THEN Valor ELSE null END) as Sett,
		MAX(CASE WHEN Tipo_Preco=25 THEN Valor ELSE null END) as AdjSett,
		MAX(CASE WHEN Tipo_Preco=30 THEN Valor ELSE null END) as LastRate,
		null as PrevLastRate,
		CASE WHEN MAX(CASE WHEN Tipo_Preco=25 THEN Valor ELSE null END)>0 THEN dbo.FCN_Calcula_Taxa(@Id_Ticker, MAX(CASE WHEN Tipo_Preco=15 THEN Valor ELSE null END), Data_Hora_Reg, 0) ELSE 0 END AS RateSett,
		CASE WHEN MAX(CASE WHEN Tipo_Preco=25 THEN Valor ELSE null END)>0 THEN dbo.FCN_Calcula_Taxa(@Id_Ticker, MAX(CASE WHEN Tipo_Preco=25 THEN Valor ELSE null END), Data_Hora_Reg, 0) ELSE 0 END AS RateAdjSett,
		null as MaxRate, 
		null as MinRate,
		null as RateDev,
		null as Duration, 
		null as BuySignal, 
		null as SellSignal,
		null as curSide,
		null as Contracts,
		0 as PnL,
		null as totNAV
	FROM dbo.Tb059_Precos_Futuros A 
	LEFT JOIN dbo.Tb001_Ativos B
	ON A.Id_Ativo=B.Id_Ativo
	WHERE A.Id_Ativo=@Id_Ticker AND Tipo_Preco IN(1,13,15,25,30,90) AND Source=13
	GROUP BY Data_Hora_Reg,Vencimento
) AS X 
ORDER BY Data_Hora_Reg,Expiration

/*
INSERT INTO @tempTable
SELECT RowNumber+1, CONVERT(varchar,getdate(),112), Expiration, NoTrades, OpenInterest, AdjSett AS OpenSettl, 0.00, LastRate AS PrevLastRate, 0.00, 0.00, null, null, null, null, null, null, null, null, 0
FROM @tempTable WHERE RowNumber=(SELECT MAX(RowNumber) FROM @tempTable)
*/

UPDATE A SET A.OpenSettl=B.AdjSett, A.PrevLastRate=B.LastRate
FROM @tempTable A INNER JOIN @tempTable B ON A.RowNumber=B.RowNumber+1

UPDATE A SET 
	A.MaxRate=(SELECT MAX(LastRate) FROM @tempTable B WHERE B.RowNumber>=A.RowNumber-@WindowSize AND B.RowNumber<A.RowNumber), 
	A.MinRate=(SELECT MIN(LastRate) FROM @tempTable B WHERE B.RowNumber>=A.RowNumber-@WindowSize AND B.RowNumber<A.RowNumber), 
	A.RateDev=(SELECT STDEV((LastRate-PrevLastRate)*100) FROM @tempTable B WHERE B.RowNumber>=A.RowNumber-@VolWindow AND B.RowNumber<A.RowNumber) 
FROM @tempTable A

DELETE FROM @tempTable WHERE Data_Hora_Reg<=@StartDate

UPDATE @tempTable SET BuySignal=1 WHERE LastRate>MaxRate

UPDATE @tempTable SET SellSignal=-1 WHERE LastRate<MinRate

UPDATE A SET 
	curSide=CASE WHEN COALESCE((SELECT MAX(RowNumber) FROM @tempTable B WHERE BuySignal=1 AND B.RowNumber<A.RowNumber),0)>COALESCE((SELECT MAX(RowNumber) FROM @tempTable B WHERE SellSignal=-1 AND B.RowNumber<A.RowNumber),0) THEN 1 ELSE -1 END
FROM @tempTable A

UPDATE @tempTable SET Duration=CONVERT(float,Expiration-Data_Hora_Reg)/365

DECLARE @curRow int
DECLARE @totRow int
DECLARE @curPnL float
DECLARE @totPnL float
DECLARE @iniNAV float
DECLARE @totNAV float
DECLARE @curDate datetime

SET @totPnL=0
SET @curPnL=0

SET @iniNAV=800000
SET @totNAV=800000

SELECT @curRow=MIN(RowNumber) FROM @tempTable
SELECT @totRow=MAX(RowNumber) FROM @tempTable

WHILE @curRow<=@totRow
	BEGIN
		SELECT @curDate=Data_Hora_Reg FROM @tempTable WHERE RowNumber=@curRow
		UPDATE @tempTable SET Contracts=CASE WHEN Duration<>0 THEN round((dbo.LEAST3(((1/POWER(RateDev,2))/10),20,100)/Duration*@totNAV/100000)/@RoundValue/3,0)*@RoundValue ELSE 0 END * curSide WHERE RowNumber=@curRow
		SELECT @curPnL=COALESCE(Contracts*(Sett-OpenSettl)*-1, 0) FROM @tempTable WHERE RowNumber=@curRow
		SET @totPnL=@totPnL+COALESCE(@curPnL,0)
		SET @totNAV=@totNAV+@curPnL
		UPDATE @tempTable SET PnL=@curPnL, totNAV=@totNAV WHERE RowNumber=@curRow
		--IF @curDate='20090310' SET @totNAV=2848734.25 
		--IF @curDate='20090420' SET @totNAV=2400000.00
		--IF @curDate='20090518' SET @totNAV=969000.00
		SET @curRow=@curRow+1
	END 

SELECT *
FROM @tempTable