DECLARE @ReportDate datetime

SET  @ReportDate= '20110804'

Select B.Port_Name,A.* 
FROM
(
	Select A.IdPortfolio,Id_Contact,IdShareHolderMellon,Contact_Name,Id_Distributor,Distributor,[Taxa de Administracao],A.AdmRebate,MellonFee,
	-([Taxa de Administracao]*MellonFee) as TxMellon ,
	([Taxa de Administracao]-([Taxa de Administracao]*MellonFee))* A.AdmRebate as TXADMLiqu,PerfRebate,
	TxPerf,TxPerf*PerfRebate as TxPerfLiq 
	
	FROM
	(
		Select IdPortfolio,[Descricao do Fundo],A.Id_Contact,IdShareHolderMellon,Contact_Name,C.Id_Distributor,DistributorName as Distributor,DISTRIBUIDOR,[Taxa de Administracao],
		NESTDB.dbo.FCN_COM_DistRebate(@ReportDate,C.Id_Distributor,IdPortfolio,'MANG') as AdmRebate,
		NESTDB.dbo.FCN_COM_DistRebate(@ReportDate,C.Id_Distributor,IdPortfolio,'PERF') as PerfRebate,
		[TAXA DE PERFORMANCE APROPRIADA]+[TAXA DE PERFORMANCE SOBRE RESGATE] as TxPerf
		FROM NESTDB.dbo.Tb751_Contacts A
		INNER JOIN NESTIMPORT.dbo.Tb_Mellon_Investors_Perf B
		ON A.IdShareHolderMellon = B.[CODIGO DO CLIENTE]
		LEFT JOIN NESTDB.dbo.Tb752_DistContacts C
		LEFT JOIN NESTDB.dbo.Tb750_Distributors D
		ON C.id_Distributor = D.id_Distributor 
		ON A.Id_Contact = C.Id_Contact
		WHERE [Taxa de Administracao] <>0 AND RefDate=@ReportDate
	)A 	FULL OUTER JOIN	
		(
			SELECT A.IdPortfolio,A.FeeMonth,A.MellonFee FROM NESTDB.dbo.Tb755_MellonFee A
			INNER JOIN
			(
				Select IdPortfolio,max([Feemonth]) [Feemonth]
				FROM NESTDB.dbo.Tb755_MellonFee
				WHERE [Feemonth]<=@ReportDate
				GROUP BY IdPortfolio
			) B ON A.IdPortfolio = B.IdPortfolio AND A.[Feemonth] = B.[Feemonth]
		)B
		ON A.IdPortfolio = B.IdPortfolio
)A
inner join NESTDB.dbo.Tb002_Portfolios B ON A.IdPortfolio = B.Id_Portfolio 
Where Id_Distributor not in (455,4,3,36,2)

order by Port_name,Distributor




/*
UPDATE  A 
SET A.IdShareHolderMellon = B.[Codigo do Cliente]
FROM NESTDB.dbo.Tb751_Contacts A
INNER JOIN 
(
	Select [Id_Contact],[Codigo do Cliente] FROM
	(
		Select * FROM 
		NESTDB.dbo.Tb751_Contacts
		WHere IdShareHolderMellon is null
	)A
	full outer join 
	NESTIMPORT.dbo.Tb_Mellon_Investors_Perf B
	ON A.Contact_Name = B.[NOME DO CLIENTE]
	WHERE Contact_Name is not null AND  [NOME DO CLIENTE] is not null
) B
ON A.[Id_Contact] = B.[Id_Contact]

*/