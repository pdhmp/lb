--update dbo.Tb059_Precos_Futuros set source=16 where source=99
--delete from dbo.Tb059_Precos_Futuros where source=13
--select count(*) from dbo.Tb059_Precos_Futuros where source=13

--update dbo.Tb059_Precos_Futuros where source=13

/*
delete from dbo.zBMFData WHERE Id_BMFDATA IN(
SELECT Id_BMFDATA FROM (
SELECT MAX(Id_BMFDATA) AS Id_BMFDATA, count(*) AS cont FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo
GROUP BY B.Id_Ativo , A.FutDate
) AS C
WHERE cont>1)
*/



/*
update dbo.Tb059_Precos_Futuros set tipo_preco=30
where source=13 and tipo_preco=1 and data_hora_reg>='2002-01-18'
*/

/*
update dbo.Tb059_Precos_Futuros set tipo_preco=4
where source=13 and tipo_preco=34 and data_hora_reg<'2002-01-18'
*/

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,AjusteCorrig, A.FutDate, 25, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.PrecoAbert, A.FutDate, 38, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.PrecoMin, A.FutDate, 33, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.PrecoMax, A.FutDate, 34, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.PrecoUlt, A.FutDate, 1, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.Ajuste, A.FutDate, 13, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.ContrFechto, A.FutDate, 90, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.NumNeg, A.FutDate, 13, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.ContrNegoc, A.FutDate, 11, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo

INSERT INTO dbo.Tb059_Precos_Futuros(Id_Ativo,Valor,Data_Hora_Reg,Tipo_Preco,Source)
SELECT B.Id_Ativo ,A.Volume, A.FutDate, 6, 13 FROM (
select 'OD' + left(NewContract,1)+RIGHT(NewContract,1) + ' ' + RIGHT(NewContract,2) ContrCode, *  
from zBMFData) A LEFT JOIN dbo.Tb001_Ativos B
ON A.ContrCode=B.Simbolo
order by id_Ativo
