


PRINT '0'
SELECT ROW_NUMBER() OVER (ORDER BY CODCVM, DT_ATU_F) AS PosOrder, *, null AS QAmount
INTO #TempTable
FROM 
(
SELECT A.CODCVM, A.KnowDateTime, B.CODCONTA, B.DESCONTA, A.DT_ATU_F, B.Valor2
FROM nestother.dbo.Tb023_CVM_ITR_HDR A
LEFT JOIN nestother.dbo.Tb025_CVM_ITR_DATA B
ON A.ZipName=B.ZipName
WHERE B.FileName='ITRDER.001' AND B.CODCONTA IN ('3.15')--('3.05', '3.07', '3.15')
UNION ALL
SELECT A.CODCVM, A.KnowDateTime, B.CODCONTA, B.DESCONTA, DT2C3Q9, B.VALOR3
FROM nestother.dbo.Tb023_CVM_DFP_HDR A
LEFT JOIN nestother.dbo.Tb025_CVM_DFP_DATA B
ON A.ZipName=B.ZipName
WHERE B.FileName='DFPDER.001' AND B.CODCONTA IN ('3.15')--('3.05', '3.07', '3.15')
) A


PRINT '1'
DECLARE @curCounter int 
DECLARE @endCounter int 

CREATE INDEX curIndex ON #TempTable(PosOrder)

SET @curCounter=2
SELECT @endCounter=MAX(PosOrder) FROM #TempTable

DECLARE @curValue decimal(18,0)
DECLARE @prevValue decimal(18,0)
DECLARE @curMonth int
DECLARE @prevMonth int
DECLARE @curCodCVM int
DECLARE @prevCodCVM int

print @endCounter
WHILE @curCounter<=@endCounter
	BEGIN 
		SELECT @curCodCVM=CODCVM, @curValue=Valor2, @curMonth=datepart(m, DT_ATU_F) FROM #TempTable WHERE PosOrder=@curCounter
		SELECT @prevCodCVM=CODCVM, @prevValue=Valor2, @prevMonth=datepart(m, DT_ATU_F) FROM #TempTable WHERE PosOrder=@curCounter-1
		IF (@curMonth-@prevMonth=3) OR @curCodCVM<>@prevCodCVM
			BEGIN
				UPDATE #TempTable SET QAmount=@curValue-@prevValue WHERE PosOrder=@curCounter
			END
		IF @curMonth=3 AND @prevMonth=12
			BEGIN
				UPDATE #TempTable SET QAmount=@curValue WHERE PosOrder=@curCounter
			END
		SET @curCounter=@curCounter+1
	END  



SELECT * FROM #TempTable WHERE QAmount IS NULL

--DROP TABLE #TempTable

SELECT * FROM #TempTable
WHERE QAmount IS NULL AND DT_ATU_F>'2003-01-01'
--GROUP BY (CODCVM)