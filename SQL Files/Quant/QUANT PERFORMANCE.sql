SELECT *, MomBZ+MomUS+RatesBZ AS Total
FROM 
(
	SELECT [Date Now]
		, SUM(CASE WHEN [Id Section]=54 THEN [Asset P/L pC] ELSE 0 END) AS MomBZ
		, SUM(CASE WHEN [Id Section]=55 THEN [Asset P/L pC] ELSE 0 END) AS MomUS
		, SUM(CASE WHEN [Id Section]=13 THEN [Asset P/L pC] ELSE 0 END) AS RatesBZ
	FROM (SELECT [date Now],[Id Portfolio],[Id Section],[Asset P/L pC] FROM nestdb.dbo.Tb000_Historical_Positions 
			UNION ALL 
			SELECT [date Now],[Id Portfolio],[Id Section],[Asset P/L pC] FROM NESTRT.dbo.Tb000_Posicao_Atual
			) A
	WHERE [Id Portfolio]=43 AND [Date Now]>='2010-03-26'--AND [Book]='Quantitative'
	GROUP BY [date Now]
) A
ORDER BY [date Now]

