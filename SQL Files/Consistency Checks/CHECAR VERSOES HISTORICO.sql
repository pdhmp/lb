SELECT CONVERT(varchar,[Date Now],102) AS refDate, [Time_Group] AS calcDate,SUM([Contribution pC]) AS RTPerf, MAX(FlagEmail) FlagEmail
FROM NESTLOG.dbo.Tb000_Posicao_Atual_LOG
WHERE [Date Now]='2010-12-01' and [id portfolio]=43 AND FlagEmail=0
GROUP BY [Date Now],[Time_Group]
ORDER BY [Time_Group]

SELECT CONVERT(varchar,[Date Now],102) AS refDate, CONVERT(varchar,[Time_Stamp],102) AS calcDate,SUM([Contribution pC]) AS Perf
FROM (SELECT [Date Now],[id portfolio],[Contribution pC],[id ticker],Time_Stamp FROM NESTLOG.dbo.Tb000_Historical_Positions_LOG UNION SELECT [Date Now],[id portfolio],[Contribution pC],[id ticker],'' AS Time_Stamp FROM NESTDB.dbo.Tb000_Historical_Positions) A
WHERE [Date Now]='2010-12-01' and [id portfolio]=43 
GROUP BY [Date Now],CONVERT(varchar,[Time_Stamp],102)


SELECT A.*, B.PerfRT, ROUND(PerfHist,4)-ROUND(PerfRT,4) AS DIFF
FROM 
(
SELECT [Date Now],[Id Ticker],SUM([Contribution pC]) AS PerfHist 
FROM NESTLOG.dbo.Tb000_Historical_Positions_LOG
WHERE [Date Now]='2010-12-01' and [id portfolio]=43 AND CONVERT(varchar,[Time_Stamp],102)='2010.12.07'
GROUP BY [Date Now],[Id Ticker]
) A
FULL OUTER JOIN
(
SELECT [Date Now],[Id Ticker],SUM([Contribution pC]) AS PerfRT
FROM NESTLOG.dbo.Tb000_Posicao_Atual_LOG
WHERE [Date Now]='2010-12-01' and [id portfolio]=43
GROUP BY [Date Now],[Id Ticker]
) B
ON A.[Id Ticker]=B.[Id Ticker]
WHERE ROUND(PerfHist,4)<>ROUND(PerfRT,4)
ORDER BY ABS(ROUND(PerfHist,4)-ROUND(PerfRT,4)) DESC