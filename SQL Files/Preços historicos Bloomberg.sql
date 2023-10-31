USE nestdb

-- Solicita preços
-- EXEC NESTDB.dbo.Proc_Insere_Historico -- todos os instrumentos.
-- EXEC NESTDB.dbo.Proc_Insere_Historico_Individual 4 -- insira o idinstrument para pegar os preços.

DECLARE @Ontem datetime
DECLARE @Anteontem datetime

SET @Ontem= '2014-02-21'
SET @Anteontem= '2014-02-20'

SELECT TOP 1 *,'Preço Grafico IBOV' FROM NESTRT.dbo.Tb065_Ultimo_Preco where IdSecurity = 1073 AND SrType = 1 ORDER BY SourceTimeStamp desc
SELECT A.srtype, Yesterday, Prev, C.*
FROM 
(
      Select srtype,count(*) AS Yesterday from 
      dbo.VW_Precos
      where SrDate=@Ontem AND SrType IN (100,101)
      group by srtype
) A
INNER JOIN
(
      Select srtype,count(*) AS Prev from 
      dbo.VW_Precos
      where SrDate=@Anteontem AND SrType IN (100,101)
      group by srtype
) B
ON A.srtype=B.srtype
FULL OUTER JOIN 
(
      SELECT 100 AS SrType, 'TRDAY' AS TypeName, 'Verificar se o programa TR Price Feeder esta aberto e rodou na maquina da BBG' AS Comment
      UNION ALL
      SELECT 101 AS SrType, 'TRINDEX' AS TypeName, 'Se o de cima estiver OK, rodar a proc proc_Update_TRIndex_All'
) C
ON A.SrType=C.SrType


Select coalesce(E.Preco,F.Preco),coalesce(C.Descricao,D.Descricao),Yesterday,Prev,ABS(Yesterday-Prev) as Diff ,CASE WHEN ABS(Yesterday-Prev)>120 THEN 'PLEASE CHECK' ELSE '' END
FROM
(
      Select SrType,Source as Source1,count(*)Yesterday
      from dbo.VW_Precos
      where Source = 22 AND SrDate>=@Ontem  
      group by SrType,Source
)A 
full outer JOIN
(
      Select SrType,Source as Source2,count(*)Prev from 
      dbo.VW_Precos
      where Source = 22 AND SrDate>=@Anteontem 
      group by SrType,Source
)B
ON A.SrType = B.SrType AND A.Source1 = B.Source2
INNER JOIN dbo.Tb102_Sistemas_Informacoes C
ON A.Source1 = C.Id_Sistemas_Informacoes
INNER JOIN dbo.Tb102_Sistemas_Informacoes D
ON B.Source2 = D.Id_Sistemas_Informacoes
INNER JOIN dbo.Tb116_Tipo_Preco E 
ON A.SrType = E.Id_Tipo_Preco
INNER JOIN dbo.Tb116_Tipo_Preco F 
ON B.SrType = F.Id_Tipo_Preco
order by ABS(Yesterday-Prev) desc,coalesce(A.SrType,B.SrType)



Select coalesce(E.Preco,F.Preco),coalesce(C.Descricao,D.Descricao),Yesterday,Prev,ABS(Yesterday-Prev) as Diff ,CASE WHEN ABS(Yesterday-Prev)>120 THEN 'PLEASE CHECK' ELSE '' END
FROM
(
      Select SrType,Source as Source1,count(*)Yesterday
      from dbo.VW_Precos
      where Source <> 22 AND SrDate=@Ontem  
      group by SrType,Source
)A 
full outer JOIN
(
      Select SrType,Source as Source2,count(*)Prev from 
      dbo.VW_Precos
      where Source <> 22 AND SrDate=@Anteontem 
      group by SrType,Source
)B
ON A.SrType = B.SrType AND A.Source1 = B.Source2
INNER JOIN dbo.Tb102_Sistemas_Informacoes C
ON A.Source1 = C.Id_Sistemas_Informacoes
INNER JOIN dbo.Tb102_Sistemas_Informacoes D
ON B.Source2 = D.Id_Sistemas_Informacoes
INNER JOIN dbo.Tb116_Tipo_Preco E 
ON A.SrType = E.Id_Tipo_Preco
INNER JOIN dbo.Tb116_Tipo_Preco F 
ON B.SrType = F.Id_Tipo_Preco
order by ABS(Yesterday-Prev) desc,coalesce(A.SrType,B.SrType)

SELECT '==================================== REAL TIME COUNT ================================'

Select  Preco,count(*) from nestrt.dbo.Tb065_Ultimo_Preco A
INNER JOIN dbo.Tb116_Tipo_Preco B 
ON A.SrType = B.Id_Tipo_Preco
group by Preco
order by Preco





SELECT '==================================== DURATION, MKTCAP AND AVVOLUME ================================'

Select Preco,count(*) from nestrt.dbo.Tb065_Ultimo_Preco A
INNER JOIN dbo.Tb116_Tipo_Preco B 
ON A.SrType = B.Id_Tipo_Preco
where SrType in (19,20,91)
group by Preco

Select Preco,count(*),convert(varchar,FeederTimeStamp,112) from nestrt.dbo.Tb065_Ultimo_Preco A
INNER JOIN dbo.Tb116_Tipo_Preco B 
ON A.SrType = B.Id_Tipo_Preco
where SrType in (19,20,91)
group by Preco,convert(varchar,FeederTimeStamp,112)

Select Preco,count(*),convert(varchar,FeederTimeStamp,112) Preco_Zero from nestrt.dbo.Tb065_Ultimo_Preco A
INNER JOIN dbo.Tb116_Tipo_Preco B
ON A.SrType = B.Id_Tipo_Preco
where SrType in (19,20) and SrValue=0
group by Preco,convert(varchar,FeederTimeStamp,112)



--proc_Update_TRIndex_All