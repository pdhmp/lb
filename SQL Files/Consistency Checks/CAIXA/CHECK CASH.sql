DECLARE @GetDate smalldatetime
DECLARE @PrevDate smalldatetime
DECLARE @Id_Portfolio smalldatetime

DECLARE @PrevCash float
DECLARE @Settling float
DECLARE @Receivables float

SET @GetDate='20101101'
SET @Id_Portfolio=10

SET @PrevDate=dbo.FCN_NDATEADD('du', -1, @GetDate, 0, 4)

SELECT @PrevCash=[Cash uC] 
FROM dbo.Tb000_Historical_Positions 
WHERE [Date Now]=@PrevDate AND [Id Portfolio]=@Id_Portfolio AND [Id Ticker]=1844

SELECT @Settling=SUM(Quantity)
FROM dbo.vw_Transactions A
INNER JOIN dbo.VW_PortAccounts B
ON A.Id_Account=B.Id_Account
WHERE Trade_Date=@GetDate AND Id_Ticker=1844 AND Id_Portfolio=@Id_Portfolio

SELECT @Receivables=SUM(Quantity)
FROM dbo.vw_Transactions A
INNER JOIN dbo.VW_PortAccounts B
ON A.Id_Account=B.Id_Account
WHERE Trade_Date<=@GetDate AND Settlement_Date>@GetDate AND Id_Ticker=1844 AND Id_Portfolio=@Id_Portfolio

SELECT 'Previous Day Cash', @PrevCash
UNION ALL SELECT 'Settle on date', @Settling
UNION ALL SELECT 'Current Day Cash', @PrevCash+@Settling
UNION ALL SELECT 'Receivables', @Receivables
UNION ALL SELECT 'Total LB Cash', @PrevCash+@Settling+@Receivables

SELECT Settlement_Date, Trade_Date, Id_Broker, SUM(Quantity), 
SUM(CASE WHEN Quantity>0 THEN Quantity ELSE 0 END) AS Purchases, 
SUM(CASE WHEN Quantity<0 THEN Quantity ELSE 0 END) AS Sales
FROM dbo.vw_Transactions A
INNER JOIN dbo.VW_PortAccounts B
ON A.Id_Account=B.Id_Account
WHERE Trade_Date<=@GetDate AND Settlement_Date>@GetDate AND Id_Ticker=1844 AND Id_Portfolio=@Id_Portfolio
GROUP BY Settlement_Date, Trade_Date, Id_Broker
ORDER BY Settlement_Date, Trade_Date, Id_Broker


