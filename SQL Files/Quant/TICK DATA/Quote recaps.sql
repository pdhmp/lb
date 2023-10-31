 

/*
TRUNCATE TABLE [Tb049_Quote_Recap_BA]
TRUNCATE TABLE [Tb048_Quote_Recap]
*/


/*
select id_ticker, count(*) 
FROM [Tb049_Quote_Recap_BA]
GROUP BY id_ticker
*/

SELECT TOP 5 * 
FROM  NESTTICK.dbo.[Tb002_Quote_Recap_BA] 
WHERE id_ticker=3 AND Quote_DateTime<='20090903 12:35:12'
ORDER BY Quote_DateTime DESC

SELECT TOP 5 * 
FROM  NESTTICK.dbo.[Tb001_Quote_Recap] 
WHERE id_ticker=3 AND Trade_DateTime<='20090903 12:35:13'
ORDER BY Trade_DateTime DESC

SELECT id_Ticker, CONVERT(varchar,Trade_DateTime,112),SUM(Quantity)
FROM NESTTICK.dbo.[Tb001_Quote_Recap]
WHERE Condition is null
GROUP BY id_Ticker, CONVERT(varchar,Trade_DateTime,112)
order by id_Ticker, CONVERT(varchar,Trade_DateTime,112) DESC


