

SELECT Data_Trade
	, SUM(ABS(FinValue))
	, SUM(ABS(FinValue))*0.005*(1-0.97)
FROM 
(
	SELECT Data_Trade
		, Preco_Trade*Quantidade_Trade/(SELECT RoundLot FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=Id_ativo) AS FinValue
	FROM NESTDB.dbo.VW_TRADES_ALL
	WHERE [Id Book]=3 and id_portfolio=43 and idAssetClass=1
) A
GROUP BY Data_Trade
ORDER BY Data_Trade
