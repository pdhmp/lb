SELECT NestTicker, C.IssuerName, Setor, Setor AS Industry, round(Valor/1000000,0) as MKTCAP
INTO #FlexTempTable
FROM NESTDB.dbo.Tb001_Securities A
LEFT JOIN NESTDB.dbo.Tb000_Issuer C
ON A.IdIssuer=C.IdIssuer
LEFT JOIN NESTDB.dbo.Tb113_Setores D
ON C.IdNestSector=D.Id_Setor
LEFT JOIN NESTRT.dbo.Tb065_Ultimo_Preco F
ON A.IdSecurity=F.Id_Ativo
WHERE IdPrimaryExchange IN(2, 3) AND IdInstrument=2 AND Tipo_Preco=20 AND
A.IdSecurity IN
(
SELECT Id_Ativo FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore GROUP BY Id_Ativo
UNION 
SELECT Id_Ativo FROM NESTDB.dbo.Tb051_Preco_Acoes_Offshore GROUP BY Id_Ativo
)




SELECT NestTicker + ':1281=' + IssuerName AS Data FROM #FlexTempTable
UNION SELECT NestTicker + ':1282=' + Setor FROM #FlexTempTable
UNION SELECT NestTicker + ':1283=' + Industry FROM #FlexTempTable
UNION SELECT NestTicker + ':1284=' + CAST(CAST(COALESCE(MKTCAP,0) AS int) AS varchar) FROM #FlexTempTable
ORDER BY Data

DROP TABLE #FlexTempTable