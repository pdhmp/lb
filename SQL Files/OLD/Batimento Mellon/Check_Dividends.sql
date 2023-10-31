SELECT * FROM 
(
	SELECT B.RefDate as PrevDate, A.RefDate as DateNow,coalesce(A.Descricao,B.Descricao) Description,
	coalesce(B.Valor,0) as PrevValue, coalesce(A.Valor,0) ValueNow,coalesce(B.Valor,0)-coalesce(A.Valor,0) as Diference  FROM
	(
	SELECT * FROM 
	NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE RefDate='20110228'
	and Operacao in ('Dividendos','Dividendos - Empréstimo de Ações','Juros s/ Capital')
	)A full outer join 
	(
	SELECT * FROM 
	NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE RefDate='20110225'
	and Operacao in ('Dividendos','Dividendos - Empréstimo de Ações','Juros s/ Capital')
	) B ON A.Descricao = B.Descricao
)C  WHERE Diference <> 0
order by abs(Diference) desc,Description

SELECT Ticker, Dividends FROM
dbo.Tb000_Historical_Positions
WHERE [Id POrtfolio]=10 and [Date now]='20110228'
and Dividends is not null order by Ticker



