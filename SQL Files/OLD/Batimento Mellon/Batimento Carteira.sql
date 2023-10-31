/*
truncate table NESTIMPORT.dbo.Tb_Mellon_Acoes
truncate table NESTIMPORT.dbo.Tb_Mellon_EmprestimoAcoes
truncate table NESTIMPORT.dbo.Tb_Mellon_ContasPagar
truncate table NESTIMPORT.dbo.Tb_Mellon_Fundos
truncate table NESTIMPORT.dbo.Tb_Mellon_Opcoes
truncate table NESTIMPORT.dbo.Tb_Mellon_RendaFixa
truncate table NESTIMPORT.dbo.Tb_Mellon_Tesouraria
*/

--SELECT * FROM NESTIMPORT.dbo.Tb_Mellon_EmprestimoAcoes ORDER BY Codigo
--SELECT * FROM NESTIMPORT.dbo.Tb_Mellon_Acoes ORDER BY Ativo

DECLARE @ReportDate datetime
DECLARE @IdPortfolio int

SET @ReportDate='2010-11-30'
SET @IdPortfolio =43

DECLARE @CutOffDate datetime
SET @CutOffDate = NESTDB.dbo.FCN_NDATEADD('du',3,@ReportDate,99,1073)


SELECT 'EQUITY' AS MellonType, X.*, LBPOS.[date now], LBPOS.[id portfolio], LBPOS.ticker, LBPOS.details
	, LBPOS.position QuantidadeLB
	, LBPOS.[cash uc] ValorLB
	, LBPOS.last PrecoLB
	, LBPOS.[total p/l] ResultadoLB
FROM 
(
	SELECT COALESCE(MACOES.RefDate, MLOANS.RefDate)RefDate
		,COALESCE(MACOES.IdPortfolio, MLOANS.IdPortfolio)IdPortfolio
		,COALESCE(Ticker, Ativo)Ativo
		, '' Details
		,COALESCE(MACOES.Quantidade,0)+COALESCE(MLOANS.Quantidade,0)+COALESCE(MTERMO.Quantidade,0) Quantidade
		,COALESCE(MACOES.Cotacao,MLOANS.Preco)Preco
		,COALESCE(ValorAcoes,0)+COALESCE(ValorContasPagar,0)+COALESCE(ValorTermo,0)Financeiro
		,MACOES.Resultado
		--, ValorAcoes, ValorContasPagar, ValorEmpreAcoes, ValorTermo
	FROM 
	(
		SELECT RefDate,IdPortfolio,Ativo,QuantidadeTotal AS Quantidade,Cotacao,Resultado,ValorMercadoLiquido ValorAcoes
		FROM NESTIMPORT.dbo.Tb_Mellon_Acoes Where RefDate = @ReportDate AND IdPortfolio= @IdPortfolio

	) MACOES
	FULL OUTER JOIN 
	(
		SELECT K.*, L.ValorContasPagar
		FROM 
		(
			SELECT RefDate,IdPortfolio,Codigo Ticker
				, SUM(Quantidade)Quantidade
				, MIN(Preco) Preco
				, SUM(Financeiro) ValorEmpreAcoes
			FROM NESTIMPORT.dbo.Tb_Mellon_EmprestimoAcoes
			WHERE TipoOperacao = 'Operação Tomadora'
			AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
			GROUP BY RefDate,IdPortfolio,TipoOperacao,Codigo
		) K 
		FULL OUTER JOIN
		(
			SELECT RTRIM(SUBSTRING(Descricao,37,6)) Ticker
			, SUM(Valor) Valor
			, SUM(Valor) ValorContasPagar
			FROM NESTIMPORT.dbo.Tb_Mellon_ContasPagar
			WHERE Operacao='Empréstimo de Ações a Pagar'
			AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
			GROUP BY RTRIM(SUBSTRING(Descricao,37,6))
		) L
		ON K.Ticker=L.Ticker
		UNION
		SELECT M.*, N.ValorContasPagar
		FROM 
		(
			SELECT RefDate,IdPortfolio,Codigo Ticker
				, SUM(Quantidade)Quantidade
				, MIN(Preco) Preco
				, SUM(Financeiro) ValorEmpreAcoes
			FROM NESTIMPORT.dbo.Tb_Mellon_EmprestimoAcoes
			WHERE TipoOperacao = 'Operação  Doadora'
			AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
			GROUP BY RefDate,IdPortfolio,TipoOperacao,Codigo
		) M 
		FULL OUTER JOIN
		(
			SELECT RTRIM(SUBSTRING(Descricao,39,6)) Ticker
			, SUM(Valor) ValorContasPagar
			FROM NESTIMPORT.dbo.Tb_Mellon_ContasPagar
			WHERE Operacao='Empréstimo de Ações a Receber'
			AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
			GROUP BY RTRIM(SUBSTRING(Descricao,39,6))
		) N
		ON M.Ticker=N.Ticker
	) MLOANS
	ON MLOANS.RefDate=MACOES.RefDate
		AND MLOANS.IdPortfolio=MACOES.IdPortfolio
		AND MLOANS.Ticker=MACOES.Ativo
	FULL OUTER JOIN 
	(
		SELECT RefDate,IdPortfolio
		, NESTDB.dbo.IFNULL_STRING((SELECT NestTicker FROM NESTDB.dbo.Tb001_Securities_Variable WHERE MellonName=RTRIM(Emitente)),Emitente) Codigo
		, SUM(CASE WHEN ValorBruto>0 THEN Quantidade ELSE 0 END) Quantidade
		, SUM(PU) Preco
		, SUM(ValorBruto) ValorTermo
		, 0 Resultado
		FROM NESTIMPORT.dbo.Tb_Mellon_RendaFixa WHERE Vencto<=@CutOffDate AND ValorBruto>0
		AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
		GROUP BY RefDate, IdPortfolio, Emitente
	) MTERMO
	ON MACOES.RefDate=MTERMO.RefDate
		AND MACOES.IdPortfolio=MTERMO.IdPortfolio
		AND MACOES.Ativo=MTERMO.Codigo
) X
FULL OUTER JOIN 
(
	SELECT [date now],[id portfolio], [ticker] , '' Details,SUM(Position)Position,SUM([Cash uC])[Cash uC], MAX([Last]) Last,SUM([Total P/L])[Total P/L]
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [id portfolio]=@IdPortfolio and [date now]=@ReportDate AND Instrument IN('Share', 'Receipt')
	GROUP BY [date now],[id portfolio], [ticker]
) LBPOS
ON X.RefDate=LBPOS.[date now]
	AND X.IdPortfolio=LBPOS.[id portfolio]
	AND X.Ativo=LBPOS.[ticker]


--UNION ALL

SELECT 'OPTION',* FROM 
(
		SELECT RefDate,IdPortfolio,rtrim(Codigo)Codigo, '' Details,Quantidade,Cotacao,ValordeMercadoLiquido,Resultado
		FROM NESTIMPORT.dbo.Tb_Mellon_Opcoes
		WHERE RefDate = @ReportDate AND IdPortfolio= @IdPortfolio

) X
FULL OUTER JOIN 
(
	SELECT [date now],[id portfolio]
	, RTRIM((SELECT [ExchangeTicker] FROM NESTDB.dbo.Tb001_Securities WHERE IdSecurity=A.[Id ticker]))  ticker
	, '' Details,SUM(Position)Position,SUM([Cash uC])[Cash uC],SUM([Cash uC])/SUM(Position) Preco,SUM([Total P/L])[Total P/L]
	FROM NESTDB.dbo.Tb000_Historical_Positions A
	WHERE [id portfolio]=@IdPortfolio and [date now]=@ReportDate AND Instrument IN('Options')  
	GROUP BY [date now],[id portfolio], [Id ticker]
	HAVING SUM(Position)<>0
) Y
ON X.RefDate=Y.[date now]
	AND X.IdPortfolio=Y.[id portfolio]
	AND X.Codigo=Y.[ticker]

--UNION ALL

SELECT 'FORWARD',* FROM 
(
		SELECT RefDate,IdPortfolio
		, NESTDB.dbo.IFNULL_STRING((SELECT NestTicker FROM NESTDB.dbo.Tb001_Securities_Variable WHERE MellonName=RTRIM(Emitente)),Emitente) Codigo
		, Vencto
		, SUM(CASE WHEN ValorBruto>0 THEN Quantidade ELSE 0 END) Quantidade
		, SUM(CASE WHEN ValorBruto<0 THEN PU ELSE 0 END) Preco
		, SUM(ValorBruto) ValorBruto
		, 0 Resultado
		FROM NESTIMPORT.dbo.Tb_Mellon_RendaFixa 
		WHERE NOT(Vencto<=@CutOffDate AND ValorBruto>0)
		AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
		GROUP BY RefDate, IdPortfolio, Emitente, Vencto
) X
FULL OUTER JOIN
(
	SELECT [date now],[id portfolio], [underlying] [ticker],Expiration,SUM(Position)Position,SUM([Cash uC])[Cash uC],SUM([Last])Preco,SUM([Total P/L])[Total P/L]
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [id portfolio]=@IdPortfolio and [date now]=@ReportDate AND Instrument IN('Forward')
	GROUP BY [date now],[id portfolio], [underlying],Expiration
) Y
ON X.RefDate=Y.[date now]
	AND X.IdPortfolio=Y.[id portfolio]
	AND X.Codigo=Y.[ticker]
	AND X.Quantidade=Y.Position
	AND (X.Vencto=Y.Expiration OR X.Vencto+1=Y.Expiration)

--UNION ALL

SELECT 'FUNDS',* FROM 
(
	SELECT RefDate,IdPortfolio,Codigo,'' AS Details
					, SUM(QuantidadeCotas)Quantidade
					, 1 Preco
					, SUM(ValorAtual)Financeiro
					, 0 Resultado
	FROM NESTIMPORT.dbo.Tb_Mellon_Fundos
	WHERE RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
	GROUP BY RefDate,IdPortfolio,Codigo
) X
FULL OUTER JOIN
(
	SELECT [date now],[id portfolio], [underlying] [ticker],'' AS Details,SUM(Position)Position,SUM([Cash uC])[Cash uC],SUM([Last])Preco,SUM([Total P/L])[Total P/L]
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [id portfolio]=@IdPortfolio and [date now]=@ReportDate AND Instrument IN('Funds')
	GROUP BY [date now],[id portfolio], [underlying],Expiration
) Y
ON X.RefDate=Y.[date now]
	AND X.IdPortfolio=Y.[id portfolio]
	AND X.Codigo=Y.[ticker]

--UNION ALL

SELECT 'EXPENSES',* FROM 
(
	SELECT RefDate,IdPortfolio,Operacao,'' AS Details
				, SUM(Valor)Quantidade
				, 1 Preco
				, SUM(Valor)Financeiro
				, 0 Resultado
	FROM NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE Operacao NOT IN ('Empréstimo de Ações a Pagar','Empréstimo de Ações a Receber')
	AND RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
	GROUP BY RefDate,IdPortfolio,Operacao

) X
FULL OUTER JOIN
(
	SELECT [date now],[id portfolio], [underlying] [ticker],'' AS Details,SUM(Position)Position,SUM([Cash uC])[Cash uC],SUM([Last])Preco,SUM([Total P/L])[Total P/L]
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [id portfolio]=@IdPortfolio and [date now]=@ReportDate AND Instrument IN('Funds')
	GROUP BY [date now],[id portfolio], [underlying],Expiration
) Y
ON X.RefDate=Y.[date now]
	AND X.IdPortfolio=Y.[id portfolio]
	AND X.Operacao=Y.[ticker]

--UNION ALL

SELECT 'CASH',* FROM 
(
	SELECT RefDate,IdPortfolio,'Cash BRL' AS Codigo, '' AS Details
					, SUM(Valor)Quantidade
					, 1 Preco
					, SUM(Valor)Financeiro
					, 0 Resultado
	FROM NESTIMPORT.dbo.Tb_Mellon_Tesouraria
	WHERE RefDate = @ReportDate AND IdPortfolio= @IdPortfolio
	GROUP BY RefDate,IdPortfolio
) X
FULL OUTER JOIN
(
	SELECT [date now],[id portfolio], [underlying] [ticker],'' AS Details,SUM(Position)Position,SUM([Cash uC])[Cash uC],SUM([Last])Preco,SUM([Total P/L])[Total P/L]
	FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [id portfolio]=@IdPortfolio and [date now]=@ReportDate AND Instrument IN('Funds')
	GROUP BY [date now],[id portfolio], [underlying],Expiration
) Y
ON X.RefDate=Y.[date now]
	AND X.IdPortfolio=Y.[id portfolio]
	AND X.Codigo=Y.[ticker]
--ORDER BY Ativo
/*



SELECT SUM(Valor) FROM (
SELECT SUM(ValorMercadoBruto)Valor FROM NESTIMPORT.dbo.Tb_Mellon_Acoes
UNION SELECT SUM(Valor) FROM NESTIMPORT.dbo.Tb_Mellon_ContasPagar
--UNION SELECT SUM(Total) FROM NESTIMPORT.dbo.Tb_Mellon_EmprestimoAcoes
UNION SELECT SUM(ValorLiquido) FROM NESTIMPORT.dbo.Tb_Mellon_Fundos
UNION SELECT SUM(ValordeMercadoBruto) FROM NESTIMPORT.dbo.Tb_Mellon_Opcoes
UNION SELECT SUM(ValorBruto) FROM NESTIMPORT.dbo.Tb_Mellon_RendaFixa
UNION SELECT SUM(Valor) FROM NESTIMPORT.dbo.Tb_Mellon_Tesouraria) Z
*/

