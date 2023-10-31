select convert(varchar,Trade_DateTime,112) as Trade_DateTime,Id_Ticker , count(*)
from nesttick.dbo.Tb001_Quote_Recap
group by convert(varchar,Trade_DateTime,112),Id_Ticker
order by  convert(varchar,Trade_DateTime,112), count(*) desc


select count(*) FROM nesttick.dbo.Tb001_Quote_Recap
select count(*) FROM nesttick.dbo.Tb002_Quote_Recap_BA
select count(*) FROM nesttick.dbo.Tb010_Imported_Tickers

/*
TRUNCATE TABLE nesttick.dbo.Tb001_Quote_Recap
TRUNCATE TABLE nesttick.dbo.Tb002_Quote_Recap_BA
TRUNCATE TABLE nesttick.dbo.Tb010_Imported_Tickers

*/

SELECT * FROM 
(
SELECT TOP 20 * 
FROM  NESTTICK.dbo.[Tb002_Quote_Recap_BA] 
WHERE id_ticker=223 AND Quote_DateTime<='20090904 12:35:12'
ORDER BY Quote_DateTime DESC
) AS A

UNION
SELECT * FROM 
(
SELECT TOP 20 Id_Trade, Id_Ticker, Trade_DateTime, Price, quantity, 0 AS zz, 0 AS zz2
FROM  NESTTICK.dbo.[Tb001_Quote_Recap] 
WHERE id_ticker=223 AND Trade_DateTime<='20090904 12:35:12'
ORDER BY Trade_DateTime DESC
) AS B

ORDER BY Quote_DateTime DESC

SELECT id_Ticker, CONVERT(varchar,Trade_DateTime,112),SUM(Quantity)
FROM NESTTICK.dbo.[Tb001_Quote_Recap]
WHERE Condition is null
GROUP BY id_Ticker, CONVERT(varchar,Trade_DateTime,112)
order by id_Ticker, CONVERT(varchar,Trade_DateTime,112) DESC

SELECT * 
FROM  NESTTICK.dbo.[Tb002_Quote_Recap_BA] 
WHERE id_ticker=223 AND bid_price=34.1 AND Ask_Price=34.18
ORDER BY Quote_DateTime DESC


SELECT * FROM NESTLOG.dbo.Tb901_Event_Log WHERE Program_id=31 order by event_datetime