
/*
SELECT CODCVM, RAZAO_SOC FROM nestother.dbo.Tb021_CVM_ITR_CTL WHERE RAZAO_SOC LIKE '%itau%'
*/


DECLARE @CodCVM int
SET @CodCVM=4820

/*
BRASKEM - 4820
PARANAPANEMA - 9393
BRFOODS - 16292
*/

SELECT ROW_NUMBER() OVER (ORDER BY DT_ATU_F) AS PosOrder, *, null AS QAmount
INTO #TempTable
FROM 
(
SELECT A.CODCVM, A.KnowDateTime, B.CODCONTA, B.DESCONTA, A.DT_ATU_F, B.Valor2
FROM nestother.dbo.Tb023_CVM_ITR_HDR A
LEFT JOIN nestother.dbo.Tb025_CVM_ITR_DATA B
ON A.ZipName=B.ZipName
WHERE A.CODCVM=@CodCVM AND B.FileName='ITRDEREE.001' AND B.CODCONTA IN ('3.15')--('3.05', '3.07', '3.15')
UNION ALL
SELECT A.CODCVM, A.KnowDateTime, B.CODCONTA, B.DESCONTA, DT2C3Q9, B.VALOR3
FROM nestother.dbo.Tb023_CVM_DFP_HDR A
LEFT JOIN nestother.dbo.Tb025_CVM_DFP_DATA B
ON A.ZipName=B.ZipName
WHERE A.CODCVM=@CodCVM AND B.FileName='DFPDEREE.001' AND B.CODCONTA IN ('3.15')--('3.05', '3.07', '3.15')
) A

DECLARE @curCounter int 
DECLARE @endCounter int 

SET @curCounter=2
SELECT @endCounter=MAX(PosOrder) FROM #TempTable

DECLARE @curValue decimal(18,0)
DECLARE @prevValue decimal(18,0)
DECLARE @curMonth int
DECLARE @prevMonth int


WHILE @curCounter<=@endCounter
	BEGIN 
		SELECT @curValue=Valor2, @curMonth=datepart(m, DT_ATU_F) FROM #TempTable WHERE PosOrder=@curCounter
		SELECT @prevValue=Valor2, @prevMonth=datepart(m, DT_ATU_F) FROM #TempTable WHERE PosOrder=@curCounter-1
		IF (@curMonth-@prevMonth=3) 
			BEGIN
				UPDATE #TempTable SET QAmount=@curValue-@prevValue WHERE PosOrder=@curCounter
			END
		IF @curMonth=3 AND @prevMonth=12
			BEGIN
				UPDATE #TempTable SET QAmount=@curValue WHERE PosOrder=@curCounter
			END
		SET @curCounter=@curCounter+1
	END  



SELECT * FROM #TempTable

DROP TABLE #TempTable