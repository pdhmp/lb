SELECT [nest sector], Commodity_Flag
, SUM(NetBZ)*100 AS NetBZ
, SUM(NetUS)*100 AS NetUS
--, SUM([contribution pC]) AS Performance
from 
(
	Select [Id ticker], [nest sector], Commodity_Flag
	, SUM(CASE WHEN [Id Section]=54 THEN [Delta/Book] ELSE 0 END) AS NetBZ 
	, SUM(CASE WHEN [Id Section]=55 THEN [Delta/Book] ELSE 0 END) AS NetUS 
	--, SUM([contribution pC]) AS [contribution pC]
	FROM nestRT.dbo.Tb000_Posicao_Atual(nolock) X
	LEFT JOIN dbo.Tb113_Setores Z
	ON X.[Nest Sector]=Z.Setor
	where [id portfolio]=43 AND [New Strategy]='Equities' AND [Id Section] IN (54,55)
	GROUP BY [Id Ticker], [nest sector], Commodity_Flag
) A
group by [Nest Sector], Commodity_Flag
order by Commodity_Flag DESC, [Nest Sector]
