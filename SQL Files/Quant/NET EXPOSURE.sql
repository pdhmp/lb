SELECT [date now], SUM([Delta cash])/MAX([book NAV]), SUM(CASE WHEN [Delta cash]<0 THEN [Delta cash] ELSE 0 END)/MAX([book NAV]) AS Short
FROM NESTDB.dbo.Tb000_Historical_Positions
WHERE [id Portfolio]=43 AND [Id Book]=3 AND [asset class]='Equity'
GROUP BY [date now]
ORDER BY [date now]