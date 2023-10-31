SELECT Data_hora_reg,A.Quotes FROM 
(
SELECT CONVERT(varchar,Trade_DateTime,112) AS Trade_DateTime, count(*) AS Quotes
FROM nesttick.dbo.Tb001_Quote_Recap 
where id_ticker=223
GROUP BY CONVERT(varchar,Trade_DateTime,112)
) A
RIGHT JOIN
(
SELECT CONVERT(varchar,Data_hora_reg,112) as Data_hora_reg 
FROM nestdb.dbo.Tb053_Precos_Indices 
WHERE id_ativo=1073 AND source=1 AND tipo_preco=1 AND Data_hora_reg>='20090601'
) B
ON A.Trade_DateTime=Data_hora_reg
ORDER BY Data_hora_reg

