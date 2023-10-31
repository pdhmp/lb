Select SUM(FechamentoDm1),SUM(CompraVenda),SUM(Corretagem),SUM(Fechamento),SUM(LucroPrejuizo),SUM(Variacao) 
FROM NESTIMPORT.dbo.Tb_Mellon_Analise
WHERE RefDate='20110516' and IdPortfolio = 10
AND Descricao in 
(
'Renda Variável','TERM3','Vencimento de Termo',
'Dividendos',
'Dividendos - Empréstimo de Ações',
'Dividendos - Empréstimo de Ações - Manual',
'Dividendos - Manual',
'Empréstimo de Ações',
'Empréstimo de Ações a Pagar',
'Empréstimo de Ações a Receber',
--'Fatura de Empréstimo de Ações - Doador',
--'Fatura de Empréstimo de Ações - Tomador',
--'Juros - Empréstimo de Ações',
'Juros s/ Capital',
--'Juros s/ Capital - Manual',
'Rendimentos - Empréstimo de Ações'
--'Repasse de Taxa de Liquidação ao Doador'
)

Select *
FROM NESTIMPORT.dbo.Tb_Mellon_Analise
WHERE RefDate='20110516' and IdPortfolio = 10
AND Descricao in 
(
'Renda Variável','TERM3','Vencimento de Termo'
)

Select SUM([Cash uC Admin]), sum([Total P/L Admin]) FROM NESTDB.dbo.Tb000_Historical_Positions
WHERE [Date Now]='20110516' and [Id Portfolio] = 10
AND [Id Instrument] in (1,2,7,12,15)
