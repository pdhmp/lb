
SELECT * FROM 
(
	SELECT B.RefDate as PrevDate, A.RefDate as DateNow,coalesce(A.Descricao,B.Descricao) Description,
	coalesce(B.Valor,0) as PrevValue, coalesce(A.Valor,0) ValueNow,coalesce(B.Valor,0)-coalesce(A.Valor,0) as Diference  FROM
	(
	SELECT RefDate,Descricao,sum(Valor) as Valor FROM 
	NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE RefDate='20110228'
	and Operacao in ('Fatura de Empréstimo de Ações - Doador')
	GROUP BY RefDate,Descricao
	)A full outer join 
	(
	SELECT RefDate,Descricao,sum(Valor) as Valor FROM 
	NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE RefDate='20110225'
	and Operacao in ('Fatura de Empréstimo de Ações - Doador')
	GROUP BY RefDate,Descricao
	) B ON A.Descricao = B.Descricao
)C  --WHERE Diference >0
order by abs(Diference) desc,Description

SELECT * FROM 
(
	SELECT B.RefDate as PrevDate, A.RefDate as DateNow,coalesce(A.Descricao,B.Descricao) Description,
	coalesce(B.Valor,0) as PrevValue, coalesce(A.Valor,0) ValueNow,coalesce(B.Valor,0)-coalesce(A.Valor,0) as Diference  FROM
	(
	SELECT RefDate,
	Descricao,sum(Valor) as Valor FROM 
	NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE RefDate='20110228'
	and Operacao in ('Fatura de Empréstimo de Ações - Tomador')
	GROUP BY RefDate,Descricao
	)A full outer join 
	(
	SELECT RefDate,Descricao,sum(Valor) as Valor FROM 
	NESTIMPORT.dbo.Tb_Mellon_ContasPagar
	WHERE RefDate='20110225'
	and Operacao in ('Fatura de Empréstimo de Ações - Tomador')
	GROUP BY RefDate,Descricao
	) B ON A.Descricao = B.Descricao
)C  --WHERE Diference <0 
order by abs(Diference) desc,Description

