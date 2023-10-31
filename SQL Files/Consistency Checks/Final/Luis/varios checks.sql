SELECT * FROM dbo.VW_ORDENS_ALL
WHERE Id_ativo=86 AND Data_Trade>='2010-01-20' AND Data_Trade<='2010-01-26' AND Id_Port_Type=1 
AND Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio IN(4))-- AND [Id Book]=2 AND [Id Section]=2
ORDER BY [data_abert_ordem]





SELECT [date now] as zzzz,Position, [id book], [id section],[new sub strategy],* 
FROM NESTDB.dbo.Tb000_Historical_Positions 
where [Id Ticker]=19941 AND [date now]>='2010-01-12' AND [date now]<='2010-02-28' AND [Id Portfolio] IN(4) --AND Position<0
ORDER BY [date now]


/*
UPDATE dbo.Tb012_Ordens SET [id section]=17
WHERE Id_Ordem IN 
(
1109142
)
*/

/*
UPDATE NESTDB.dbo.Tb000_Historical_Positions SET [Id book]=2,[id Section]=31, [New Sub Strategy]='Arbitrage', [Section]='TEND - GFSA'
where [Id Ticker]=382 AND [date now]>='2010-01-12' AND [date now]<='2010-02-28' AND [Id Portfolio] IN(4) --AND Position<0

UPDATE NESTDB.dbo.Tb000_Historical_Positions SET [Id book]=2,[id Section]=2, [New Sub Strategy]='Long-Short'
where [Id Position]=1516416 

*/
SELECT Z_Estrategia,Z_Sub_Estrategia,* FROM dbo.Tb012_Ordens
WHERE Id_ativo=439 AND Data_Abert_ordem>='2010-01-05' AND Data_Abert_ordem<='2010-06-20'-- AND [id Section]=2--AND Z_Estrategia=29 AND Z_Sub_Estrategia=33

SELECT * FROM dbo.Tb351_Trade_Alocation WHERE Id_Order=1237135


SELECT [date now] as zzzz,Position, [id book], [id section],[new sub strategy],* 
FROM NESTDB.dbo.Tb000_Historical_Positions 
where [Id Ticker]=1066 AND [date now]='2010-01-04' AND [Id Portfolio]=10 --AND [zId Strategy]=8 AND [zId Sub Strategy]=93
ORDER BY [date now]



/*
UPDATE NESTDB.dbo.Tb000_Historical_Positions SET [id section]=51 where [id position]=1656406
*/


/*
UPDATE NESTDB.dbo.Tb000_Historical_Positions SET position=20000 where [id position]=1681562

EXEC dbo.PROC_GET_CALCULATE_COST_CLOSE_HISTORICAL 1681562
EXEC dbo.PROC_GET_CALCULATE_FIELDS_HIST 1681562,1
*/



/*
UPDATE dbo.Tb012_Ordens SET [id section]=27
WHERE Id_Ordem IN 
(
SELECT Id_Ordem FROM dbo.VW_ORDENS_ALL
WHERE Id_ativo IN (1238) AND Data_Trade>='2010-01-01' AND Data_Trade<='2010-01-13' AND Id_Port_Type=1 
AND Id_Account IN (SELECT Id_Account FROM dbo.VW_PortAccounts WHERE Id_Portfolio IN(43)) AND [Id Section]=56
)
*/