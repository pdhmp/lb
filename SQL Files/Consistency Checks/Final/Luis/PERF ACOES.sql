SELECT [date now],SUM([Contribution pc admin])
FROM NESTDB.dbo.Tb000_Historical_Positions 
where [date now]>='2009-12-01' AND [date now]<='2010-05-28' AND [Id Portfolio]=10
GROUP BY [date now]
ORDER BY [date now]
