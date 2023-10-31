

SELECT B.Id_Portfolio, A.Id_Account, Id_Strategy, Id_Sub_Strategy, Id_Ticker, SUM(Quantity) 
FROM vw_Transactions A


INNER JOIN dbo.VW_Port_Accounts B
ON A.Id_Account=B.Id_Account

WHERE Trade_Date='20090624' AND --Id_Ticker=5757 1571
GROUP BY B.Id_Portfolio, A.Id_Account, Id_Strategy, Id_Sub_Strategy, Id_Ticker





/*


select 1,[Id Broker],[Id Instrument],
SUM(CASE WHEN Cash>0 THEN Cash ELSE 0 END) AS Purchases,
SUM(CASE WHEN Cash<0 THEN Cash ELSE 0 END) AS Sales
from dbo.vw_Transactions_TR 
WHERE Id_Account1 IN (SELECT Id_Account FROM dbo.Tb003_PortAccounts WHERE Id_Portfolio=1) AND Settlement_Date<='20090616' AND Settlement_Date<='20090616' 
GROUP BY [Id Broker],[Id Instrument]
ORDER BY [Ticker Currency]


SELECT * FROM dbo.vw_Transactions WHERE Settlement_Date='20090618'


SELECT Id_Portfolio,A.Id_Account, B.* 
FROM dbo.Tb003_PortAccounts A 
INNER JOIN dbo.vw_Transactions B
ON A.Id_Account=B.Id_Account
WHERE Id_Portfolio=10 AND Trade_Date='20090618'


SELECT * FROM dbo.vw_Transactions_TR 
WHERE Settlement_Date='20090616'



*/


-- SELECT * FROM  WHERE id_Account=0

-- SELECT * FROM vw_Transactions_TR WHERE Trade_Date='20090624'
