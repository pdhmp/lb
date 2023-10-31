

SELECT CODCVM,A.Id_Ativo, Data_Hora_Reg, NESTDB.dbo.FCN_FIN_GetKnowDate(A.Id_Ativo, Data_Hora_Reg), DATEDIFF(d, Data_Hora_Reg, NESTDB.dbo.FCN_FIN_GetKnowDate(A.Id_Ativo, Data_Hora_Reg))
FROM Tb050_Preco_Acoes_Onshore X
LEFT JOIN dbo.Tb001_Ativos A
ON A.Id_Ativo=X.Id_Ativo
LEFT JOIN dbo.Tb000_Instituicoes B
ON A.Id_Instituicao=B.Id_Instituicao
WHERE Tipo_Preco=502 AND A.Id_Ativo IN 
(
SELECT Id_Ticker_Component 
FROM dbo.Tb023_Securities_Composition 
WHERE Id_Ticker_Composite IN 
(SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component)
GROUP BY Id_Ticker_Component
)
ORDER BY Data_Hora_Reg


