SELECT YEAR([date now]), Month([date now]), SUM([Quantity Bought]) , SUM([Quantity sold]), (SUM([Quantity Bought]) - SUM([Quantity sold]))/1000000 
FROM nestdb.dbo.Tb000_Historical_Positions
WHERE [id portfolio] in (4,10,43) and [date now]>'2009-01-01'
AND [Instrument] IN ('Share', 'Options')
--AND [Security Currency]='USDUSD'
GROUP BY YEAR([date now]), Month([date now])
ORDER BY YEAR([date now]), Month([date now])