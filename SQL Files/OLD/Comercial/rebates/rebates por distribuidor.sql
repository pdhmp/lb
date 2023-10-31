DECLARE @Date datetime

SET @Date = '20110804'


Select Id_Distributor,Distributor,
SUM(TxMellon),SUM(TXADMLiqu)
FROM
(
	Select Id_Distributor,Distributor,[Taxa de Administracao],Rebate,MellonFee,
	-([Taxa de Administracao]*MellonFee) as TxMellon ,
	([Taxa de Administracao]-([Taxa de Administracao]*MellonFee))* Rebate as TXADMLiqu
	FROM
	(
		Select IdPortfolio,[Descricao do Fundo],A.Id_Contact,IdShareHolderMellon,Contact_Name,C.Id_Distributor,DistributorName as Distributor,DISTRIBUIDOR,[Taxa de Administracao],
		dbo.FCN_COM_DistRebate(@Date,C.Id_Distributor,IdPortfolio,'MANG') as Rebate
		FROM dbo.Tb751_Contacts A
		INNER JOIN NESTIMPORT.dbo.Tb_Mellon_Investors_Perf B
		ON A.IdShareHolderMellon = B.[CODIGO DO CLIENTE]
		LEFT JOIN dbo.Tb752_DistContacts C
		LEFT JOIN dbo.Tb750_Distributors D
		ON C.id_Distributor = D.id_Distributor 
		ON A.Id_Contact = C.Id_Contact
		WHERE [Taxa de Administracao] <>0 AND RefDate=@Date
	)A 	LEFT JOIN	
		(
			SELECT A.IdPortfolio,A.FeeMonth,A.MellonFee FROM dbo.Tb755_MellonFee A
			INNER JOIN
			(
				Select IdPortfolio,max([Feemonth]) [Feemonth]
				FROM Tb755_MellonFee
				WHERE [Feemonth]<=@Date
				GROUP BY IdPortfolio
			) B ON A.IdPortfolio = B.IdPortfolio AND A.[Feemonth] = B.[Feemonth]
		)B
		ON A.IdPortfolio = B.IdPortfolio
)A
group by Id_Distributor,Distributor



/*
UPDATE  A 
SET A.IdShareHolderMellon = B.[Codigo do Cliente]
FROM Tb751_Contacts A
INNER JOIN 
(
	Select [Id_Contact],[Codigo do Cliente] FROM
	(
		Select * FROM 
		dbo.Tb751_Contacts
		WHere IdShareHolderMellon is null
	)A
	full outer join 
	NESTIMPORT.dbo.Tb_Mellon_Investors_Perf B
	ON A.Contact_Name = B.[NOME DO CLIENTE]
	WHERE Contact_Name is not null AND  [NOME DO CLIENTE] is not null
) B
ON A.[Id_Contact] = B.[Id_Contact]


*/