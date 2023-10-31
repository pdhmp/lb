

DECLARE @IniDate datetime

SET @IniDate = '2009-12-30'

SELECT C.Simbolo, CAST(CAST((ValorF/ValorI-1)*100 AS decimal(10,2)) AS varchar) + '%' ,A.*, B.ValorF
FROM 
(
/*
SELECT Id_Ativo, Valor AS ValorI
FROM dbo.Tb050_Preco_Acoes_Onshore 
WHERE Data_Hora_Reg=@IniDate AND Tipo_Preco=101
*/
SELECT Id_Ativo, MAX(Valor) AS ValorI FROM dbo.Tb050_Preco_Acoes_Onshore WHERE Tipo_Preco=101 GROUP BY Id_Ativo
) A
INNER JOIN
(
SELECT Id_Ativo, Valor  AS ValorF
FROM dbo.Tb050_Preco_Acoes_Onshore 
WHERE Data_Hora_Reg='2010-05-11' AND Tipo_Preco=101
) B
ON A.Id_Ativo=B.Id_Ativo
LEFT JOIN dbo.Tb001_Ativos C
ON A.Id_Ativo=C.Id_Ativo

ORDER BY ValorF/ValorI-1 DESC