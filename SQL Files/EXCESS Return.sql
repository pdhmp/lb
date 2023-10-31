
DECLARE @Id_Ticker int
SET @Id_Ticker=12456

DECLARE @Initial_Date datetime
DECLARE @Temp_Table TABLE(Data_Hora_Reg datetime, Valor_Index float, Valor_Fund float)


SELECT @Initial_Date=MIN(Data_Hora_Reg) 
FROM dbo.Tb056_Precos_Fundos
WHERE Id_Ativo=@Id_Ticker AND Tipo_Preco=1 AND Source=1

INSERT INTO @Temp_Table
SELECT A.Data_Hora_Reg, A.Valor, COALESCE(B.Valor, (SELECT TOP 1 Valor FROM dbo.Tb056_Precos_Fundos C WHERE Id_Ativo=@Id_Ticker AND Tipo_Preco=1 AND Source=1 AND Data_Hora_Reg<=A.Data_Hora_Reg ORDER BY Data_Hora_Reg DESC))  FROM 
(
	SELECT Data_Hora_Reg, Valor 
	FROM dbo.Tb053_Precos_Indices 
	WHERE Id_Ativo=1073 AND Tipo_Preco=1 AND Source=1 AND Data_Hora_Reg>=@Initial_Date
) AS A
LEFT JOIN
(
	SELECT Data_Hora_Reg, Valor 
	FROM dbo.Tb056_Precos_Fundos
	WHERE Id_Ativo=@Id_Ticker AND Tipo_Preco=1 AND Source=1
) AS B
ON A.Data_Hora_Reg=B.Data_Hora_Reg
ORDER BY A.Data_Hora_Reg

SELECT A.Data_Hora_Reg, A.Valor_index/B.Valor_index-1 AS Perf_Index, A.Valor_Fund/B.Valor_Fund-1 AS Perf_Fund, A.Valor_Fund/B.Valor_Fund-A.Valor_index/B.Valor_index AS Excess
FROM
	(
		SELECT Row_Number() over(order by Data_Hora_Reg asc) AS Row_Number, *
		FROM @Temp_Table
	) AS A
	INNER JOIN 
	(
		SELECT Row_Number() over(order by Data_Hora_Reg asc) AS Row_Number, *
		FROM @Temp_Table
	) AS B
	ON A.Row_Number=B.Row_Number+1
ORDER BY A.Data_Hora_Reg