DECLARE @tempTable TABLE(dateRef datetime, Quantity numeric, QTradesOnly numeric)
DECLARE @Id_Portfolio int
DECLARE @Id_Ticker int 

SET @Id_Portfolio=10
SET @Id_Ticker=1844


SELECT A.*, Valor_PL, round(PL_Livebook-Valor_PL,2) AS DIFF
FROM
(
SELECT A.[Date Now], SUM([cash uC]) AS PL_Livebook
FROM dbo.Tb000_Historical_Positions A
WHERE [Id Portfolio]=@Id_Portfolio 
AND [Date Now] >='20101101'
GROUP BY [Date Now]
) A
LEFT JOIN 
(
SELECT [Date Now], SUM([cash uC]) AS PosSize
FROM dbo.Tb000_Historical_Positions A
WHERE [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=@Id_Ticker
AND [Date Now] >='20101101'
GROUP BY [Date Now]
) B
ON A.[Date Now]=B.[Date Now]
LEFT JOIN 
(
SELECT * 
FROM dbo.Tb025_Valor_PL
WHERE Id_Portfolio=@Id_Portfolio
AND Data_PL >='20101101'
) C
ON A.[Date Now]=C.Data_PL