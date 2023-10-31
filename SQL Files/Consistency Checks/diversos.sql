SELECT * FROM dbo.Tb012_Ordens
Where Id_Ativo = 439 and Data_Abert_ordem >= '19000101' and Data_Abert_ordem <= '20100521'
AND Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=10)--AND Z_Estrategia in (2,29)
--AND [Id Book]=2
order by Data_Abert_ordem,Z_Estrategia,Z_Sub_Estrategia


SELECT A.* FROM  dbo.Tb013_Trades A
INNER JOIN dbo.Tb012_Ordens B
ON A.Id_ordem = B.Id_ordem
Where Id_Ativo = 439 and Data_Abert_ordem >= '20090101' and Data_Abert_ordem <= '20100521'
AND Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=10)--AND Z_Estrategia in (2,29)
--order by Z_Estrategia,Z_Sub_Estrategia
order by Data_Abert_ordem,Z_Estrategia,Z_Sub_Estrategia


	SELECT *
	FROM vw_Transactions
	WHERE Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=4)
	AND 
Trade_Date>='19000101' AND Trade_Date<='20100521'
	--AND Estrategia in (2,29)
	AND Id_Ticker=19421
	ORDER BY [Id Book],[Id Section]




Select [Id Ticker],[Id Book],Book,[Id Section],Section from 
Tb000_Historical_Positions
Where [Id Ticker]=19421
and [Id POrtfolio] in (4,6)
AND [Id Book]=2


UPDATE Tb012_Ordens
SET [Id Section]=2
WHERE Id_Ativo = 123 and [Id Section]=56


SELECT [Id Ticker],Ticker,POsition,[ZId Strategy],ZStrategy,[ZId Sub Strategy],[ZSub Strategy], 
[Id Book],Book,[Id Section],Section,[Date now] FROM dbo.Tb000_Historical_Positions
wHERE --[Id Portfolio] in (4,6)
--AND 
[Id Ticker]=1568 and [id Section]=56
order by [Date now]


UPDATE Tb012_Ordens
SET STATUS_ORDEM = 4
Where Id_Ativo = 1791

1339119
1335641

Select * from dbo.Tb012_Ordens
Where Data_Abert_ordem='20100521' and Id_Account =1118
and Id_Ativo in (1407,839,14576,483)
order by Operador



UPDATE dbo.Tb013_Trades
SET sTATUSTRADE=4
WHERE Id_ordem in
(
1339652)

UPDATE dbo.Tb012_Ordens
SET STATUS_ORDEM=4
WHERE Id_ordem in
(
1339652)




SELECT * FROM dbo.Tb351_Trade_Alocation
WHERE Date_Alocation = '20090706'

SELECT * FROM dbo.Tb352_Trade_Alocation_Override
WHERE /*Id_Ticker = 1504 AND*/ Override_Date='20090706'



SELECT * FROM dbo.Tb012_Ordens
Where Data_Abert_ordem >='20090830'
AND Data_Abert_ordem <='20091231'

SELECT * FROM dbo.Tb013_Trades
Where  Id_ordem =1250754



delete FROM dbo.Tb000_Historical_Positions
wHERE [Date now]='2009' AND [Id Portfolio] in (10)
AND [Id Ticker]=767
order by [Date now]

Select * from 
dbo.vw_Transactions_ALL
Where (Id_Ticker1=8 or Id_Ticker2=8)
AND (Id_Account1 IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=10)
OR Id_Account2 IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=10))
and Trade_Date='20100115'

delete from 
dbo.Tb700_Transactions where Id_Ticker1=1791 or Id_Ticker1=1791

Select * from dbo.Tb001_Ativos
Where Simbolo like '%Cash%'

UPDATE dbo.Tb012_Ordens
SET [Id Book]=1,[Id Section]=2
Where Id_ordem in
(
SELECT Id_ordem FROM dbo.Tb012_Ordens
Where Id_Ativo = 1066 and Data_Abert_ordem >= '19000101' and Data_Abert_ordem <= '20100518'
AND Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=10)--AND Z_Estrategia in (2,29)
--AND [Id Book]=2
)



dbo.Proc_Load_Position 43,'20100506',0
/*
1148
1060
1086
2009-06-02 00:00:00.000
2009-07-15 00:00:00.000

]
UPDATE  dbo.Tb012_Ordens
SET [Id Section] = 2,
[Id Book]=4
Where Id_ordem in
(
SELECT Id_ordem FROM dbo.Tb012_Ordens
Where Id_Ativo = 1066 and Data_Abert_ordem >= '19000101' and Data_Abert_ordem <= '20100513'
AND Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio=10)--AND Z_Estrategia in (2,29)
AND [Id Book]=4 AND [Id Section]=56
)

UPDATE  dbo.Tb012_Ordens
SET Status_ordem = 4
Where Id_ordem in
(
1339095,
1335923,
1338715,
1338425,
1338327,
1338143,
1337921,
1337399,
1337616,
1337260,
1337146,
1337052,
1336613,
1336650,
1336518,
1336070,
1336233
)

Select * from dbo.Tb012_Ordens
Where Id_ordem in
(
1099404,
1119185
) 

--Select * from dbo.Tb400_Books

Select * From 
dbo.Tb111_Estrategia
Select * From 
dbo.Tb112_Sub_Estrategia
Select * From 
dbo.VW_Book_Strategies
order by [Id_Book],Id_Section


delete from dbo.Tb054_Precos_Opcoes
Where Data_hora_reg='20100525' and Tipo_preco=40

delete from dbo.Tb060_Preco_Caixa
Where Id_Ativo in 
(
5674,
1769,
1791
)

delete from dbo.Tb013_Trades
Where Id_ordem in
(
	Select Id_ordem from dbo.Tb012_Ordens
	Where Id_Ativo in 
	(
	5674,
	1769,
	1791
	)
)

delete from dbo.Tb012_Ordens
Where Id_Ativo in 
(
5674,
1769,
1791
)


delete from dbo.Tb119_Convert_Simbolos
Where Id_Ativo in 
(
5674,
1769,
1791
)


delete from dbo.Tb001_Ativos
Where Id_Ativo in 
(
5674,
1769,
1791
)

*/
Select * from 
dbo.Tb800_FIX_Drop_Copies
Where Broker_Id=36
order by Symbol