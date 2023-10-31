ALTER FUNCTION FCN_Check_Portfolio_Performance(@IdPortfolio int,@ReportDate datetime)

RETURNS Table
 as 

return

SELECT Coalesce(A.Type,B.Type)Type , LucroPrejuizo as PlAdmin,[P/L] as PlLb FROM
( 
	Select 'Equities' as Type,SUM(LucroPrejuizo - Corretagem) LucroPrejuizo
	FROM NESTIMPORT.dbo.Tb_Mellon_Analise
	WHERE RefDate=@ReportDate and IdPortfolio = @IdPortfolio
	AND Descricao in 
	(
	'Ajuste de Liquidação BOVESPA',
	'Empréstimo de Ações',
	'Renda Variável',
	'Dividendos',
	'Empréstimo de Ações a Pagar',
	'Empréstimo de Ações a Receber',
	'Juros - Empréstimo de Ações',
	'Juros s/ Capital',
	'Dividendos - Manual',
	'Dividendos - Empréstimo de Ações',
	'Rendimentos',
	'jaca',
	'Corretagens e Emolumentos BOVESPA - Manual',
	'Subscrição - Manual',
	'Rendimentos - Empréstimo de Ações',
	'Compra de Renda Variável',
	'Dividendos - Empréstimo de Ações - Manual'
	)
	UNION ALL
	Select 'Bonds',SUM(LucroPrejuizo - Corretagem) LucroPrejuizo 
	FROM NESTIMPORT.dbo.Tb_Mellon_Analise
	WHERE RefDate=@ReportDate and IdPortfolio = @IdPortfolio
	AND Descricao in 
	('LFT','NTN-F')
	UNION ALL

	Select 'Options',SUM(LucroPrejuizo - Corretagem) LucroPrejuizo
	FROM NESTIMPORT.dbo.Tb_Mellon_Analise
	WHERE RefDate=@ReportDate and IdPortfolio = @IdPortfolio
	AND Descricao in 
	('Opção')
	UNION ALL

	Select 'Fowards',SUM(LucroPrejuizo - Corretagem) LucroPrejuizo
	FROM NESTIMPORT.dbo.Tb_Mellon_Analise
	WHERE RefDate=@ReportDate and IdPortfolio = @IdPortfolio
	AND Descricao in 
	('Acerto de Liquidação Financeira TRF','Compra de TRF','TERM6','TERM3','Vencimento de Termo')
	UNION ALL


	Select 'Cash',SUM(LucroPrejuizo - Corretagem) LucroPrejuizo
	FROM NESTIMPORT.dbo.Tb_Mellon_Analise
	WHERE RefDate=@ReportDate and IdPortfolio = @IdPortfolio
	AND Descricao not in 
	(
	'Opção','LFT','NTN-F','Total','Patrimônio',
	'Ajuste de Liquidação BOVESPA',
	'Empréstimo de Ações',
	'Renda Variável',
	'Dividendos',
	'Empréstimo de Ações a Pagar',
	'Empréstimo de Ações a Receber',
	'Juros - Empréstimo de Ações',
	'Juros s/ Capital',
	'Dividendos - Manual',
	'Dividendos - Empréstimo de Ações',
	'Rendimentos',
	'jaca',
	'Corretagens e Emolumentos BOVESPA - Manual',
	'Subscrição - Manual',
	'Rendimentos - Empréstimo de Ações',
	'Compra de Renda Variável',
	'Dividendos - Empréstimo de Ações - Manual',
	'Acerto de Liquidação Financeira TRF','Compra de TRF','TERM6','TERM3','Vencimento de Termo'
	)
)A
FULL OUTER JOIN
(
	Select 'Equities' as Type,Coalesce(SUM([Total P/L Admin]),0) as [P/l] FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Date Now]=@ReportDate and [Id Portfolio] = @IdPortfolio
	AND ([Id Instrument] in (2) /*OR [Id Ticker] in (76837,76839)*/)
	UNION ALL
	Select 'Bonds',Coalesce(SUM([Total P/L Admin]),0) as [P/l] FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Date Now]=@ReportDate and [Id Portfolio] = @IdPortfolio
	AND [Id Instrument] in (5)
	UNION ALL
	Select 'Options',Coalesce(SUM([Total P/L Admin]),0) as [P/l] FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Date Now]=@ReportDate and [Id Portfolio] = @IdPortfolio
	AND [Id Instrument] in (3)
	UNION ALL
	Select 'Fowards',Coalesce(SUM([Total P/L Admin]),0) as [P/l] FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Date Now]=@ReportDate and [Id Portfolio] = @IdPortfolio
	AND [Id Instrument] in (12)
	UNION ALL
	Select 'Cash',Coalesce(SUM([Total P/L Admin]),0) as [P/l] FROM NESTDB.dbo.Tb000_Historical_Positions
	WHERE [Date Now]=@ReportDate and [Id Portfolio] = @IdPortfolio
	AND ([Id Instrument] not in (2,3,5,7,12) /*AND [Id Ticker] not in (76837,76839)*/)
)B ON A.Type = B.Type



