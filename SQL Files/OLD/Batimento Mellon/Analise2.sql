Select SUM(FechamentoDm1),SUM(CompraVenda),SUM(Corretagem),SUM(Fechamento),SUM(LucroPrejuizo),SUM(Variacao) 
FROM NESTIMPORT.dbo.Tb_Mellon_Analise
WHERE RefDate='20110516' and IdPortfolio = 10
AND Descricao in 
(
'Renda Vari�vel','TERM3','Vencimento de Termo',
'Dividendos',
'Dividendos - Empr�stimo de A��es',
'Dividendos - Empr�stimo de A��es - Manual',
'Dividendos - Manual',
'Empr�stimo de A��es',
'Empr�stimo de A��es a Pagar',
'Empr�stimo de A��es a Receber',
--'Fatura de Empr�stimo de A��es - Doador',
--'Fatura de Empr�stimo de A��es - Tomador',
--'Juros - Empr�stimo de A��es',
'Juros s/ Capital',
--'Juros s/ Capital - Manual',
'Rendimentos - Empr�stimo de A��es'
--'Repasse de Taxa de Liquida��o ao Doador'
)

Select *
FROM NESTIMPORT.dbo.Tb_Mellon_Analise
WHERE RefDate='20110516' and IdPortfolio = 10
AND Descricao in 
(
'Renda Vari�vel','TERM3','Vencimento de Termo'
)

Select SUM([Cash uC Admin]), sum([Total P/L Admin]) FROM NESTDB.dbo.Tb000_Historical_Positions
WHERE [Date Now]='20110516' and [Id Portfolio] = 10
AND [Id Instrument] in (1,2,7,12,15)
