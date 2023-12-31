
DECLARE @TempTable TABLE (Id_Book int, Date_Now datetime, Perf float)

DECLARE @GetDate datetime
DECLARE @MTDDate datetime
DECLARE @HTDDate datetime
DECLARE @YTDDate datetime

SET @GetDate = CONVERT(varchar,getdate(),112)
SET @MTDDate = CONVERT(varchar,DATEADD("d", -1, LEFT(convert(varchar,@GetDate,112), 6) + '01'), 112)
SET	@HTDDate = CASE WHEN MONTH(@GetDate)<7 THEN CAST(LEFT(convert(varchar,@GetDate,112), 4)-1 AS varchar) + '1231' ELSE LEFT(convert(varchar,@GetDate,112), 4) + '0630' END 
SET	@YTDDate = CAST(LEFT(convert(varchar,@GetDate,112), 4)-1 AS varchar) + '1231'

INSERT INTO @TempTable
SELECT [Id Book], [Date Now], SUM([Contribution pC Book])
FROM (SELECT [Id Portfolio], [Id Book], [Date Now], [Contribution pC Book] FROM NESTDB.dbo.Tb000_Historical_Positions UNION ALL SELECT [Id Portfolio], [Id Book], [Date Now], [Contribution pC Book] FROM NESTRT.dbo.Tb000_Posicao_Atual) A
WHERE [Date Now]>='2010-03-26' AND [Id Portfolio]=43
GROUP BY [Id Book], [Date Now]
ORDER BY [Id Book], [Date Now]


SELECT B.*
	, (SELECT EXP(SUM(LOG(1+Perf)))-1 FROM @TempTable WHERE Id_Book=A.Id_Book AND Date_Now=@GetDate) AS PerfToday
	, (SELECT EXP(SUM(LOG(1+Perf)))-1 FROM @TempTable WHERE Id_Book=A.Id_Book AND Date_Now>@MTDDate) AS PerfMTD
	, (SELECT EXP(SUM(LOG(1+Perf)))-1 FROM @TempTable WHERE Id_Book=A.Id_Book AND Date_Now>@HTDDate) AS PerfHTD
	, (SELECT EXP(SUM(LOG(1+Perf)))-1 FROM @TempTable WHERE Id_Book=A.Id_Book AND Date_Now>@YTDDate) AS PerfYTD
FROM 
(SELECT Id_Book FROM @TempTable GROUP BY Id_Book) A
LEFT JOIN NESTDB.dbo.Tb400_Books B
ON A.Id_Book=B.Id_Book

