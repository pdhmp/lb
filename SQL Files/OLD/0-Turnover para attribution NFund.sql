SELECT CASE WHEN A.Id_Account IN (1057,1061) THEN 'USD' ELSE 'BRL' END , [Id book], SUM(ABS(Cash))
FROM NESTDB.dbo.VW_ORDENS_ALL A 
INNER JOIN NESTDB.dbo.VW_PortAccounts B
ON A.Id_Account=B.Id_Account
WHERE Id_Portfolio=4 AND A.Id_Port_Type=1 AND Data_Abert_Ordem>'2010-06-30' AND Data_Abert_Ordem<'2011-01-01' AND A.Id_Account NOT IN (1046,1060,1200)
GROUP BY CASE WHEN A.Id_Account IN (1057,1061) THEN 'USD' ELSE 'BRL' END, [Id book]
ORDER BY CASE WHEN A.Id_Account IN (1057,1061) THEN 'USD' ELSE 'BRL' END, [Id book]




SELECT A.Id_Account,Broker, SUM(ABS(Cash))
FROM NESTDB.dbo.VW_ORDENS_ALL A 
INNER JOIN NESTDB.dbo.VW_PortAccounts B
ON A.Id_Account=B.Id_Account
WHERE Id_Portfolio=4 AND A.Id_Port_Type=1 AND Data_Abert_Ordem>'2010-06-30' AND Data_Abert_Ordem<'2011-01-01' --AND A.Id_Account NOT IN (1046,1060,1200)
GROUP BY A.Id_Account, [Id book],Broker
ORDER BY A.Id_Account,Broker