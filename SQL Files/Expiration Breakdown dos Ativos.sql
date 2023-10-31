SELECT SUM(CASE WHEN Expiration<='2009-12-31' OR Expiration IS NULL THEN [Cash/NAV] ELSE 0 END) as [Now]
	, SUM(CASE WHEN Expiration>'2009-12-31' AND Expiration<'2010-03-31' THEN [Cash/NAV] ELSE 0 END) as [3m]
	, SUM(CASE WHEN Expiration>'2010-03-31' AND Expiration<'2010-12-31' THEN [Cash/NAV] ELSE 0 END) as [3-12m]
	, SUM(CASE WHEN Expiration>'2010-12-31' AND Expiration<'2014-12-31' THEN [Cash/NAV] ELSE 0 END) as [1-5y]
FROM nestdb.dbo.Tb000_Historical_Positions
WHERE [Date Now]='2009-12-30'
AND [id portfolio]=4 AND ZStrategy<>'Cash'

SELECT Ticker, [Cash/NAV] *100,  Expiration
FROM nestdb.dbo.Tb000_Historical_Positions
WHERE [Date Now]='2009-12-30'
AND [id portfolio]=4 AND ZStrategy='Cash'
