

SELECT Id_Ticker_Component 
FROM dbo.Tb023_Securities_Composition 
WHERE Id_Ticker_Composite IN 
(SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component)
GROUP BY Id_Ticker_Component

