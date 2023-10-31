
DECLARE @Tabela_Posicao table([Id Position] numeric)
DECLARE @Id_Posicao numeric

/*
UPDATE A 
SET NAV = Valor_PL,[NAV pC]=Valor_PL
FROM dbo.Tb000_Historical_Positions A
INNER JOIN dbo.Tb025_Valor_PL B
ON B.Id_Portfolio = A.[Id Portfolio] AND Data_PL = [Last Position]
WHERE [Id Portfolio]=10 AND [Id Ticker] =1844
AND [Date Now]>='2010-01-01' AND [Date Now]<='2010-09-26'
*/

INSERT INTO @Tabela_Posicao
	SELECT [Id Position]
	FROM dbo.Tb000_Historical_Positions A
	WHERE [Id Portfolio]=10 AND [Id Ticker]=5746 AND [Date Now]>='2010-01-01' AND [Date Now]<='2010-09-26'


While exists(select top 1 [Id Position] from @Tabela_Posicao)
	BEGIN
		Select top 1 @Id_Posicao = [Id Position]
		from @Tabela_Posicao
PRINT @Id_Posicao
		EXEC PROC_GET_CALCULATE_COST_CLOSE_HISTORICAL @Id_Posicao
		EXEC [PROC_GET_CALCULATE_FIELDS_Hist] @Id_Position = @Id_Posicao,@Flag_Historic=1
		delete from @Tabela_Posicao Where [Id Position] = @Id_Posicao
	END

SELECT 'OK'

