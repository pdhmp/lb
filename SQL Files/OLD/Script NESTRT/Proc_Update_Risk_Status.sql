USE [NESTRT]
GO
/****** Object:  StoredProcedure [dbo].[Proc_Update_Risk_Status]    Script Date: 10/26/2010 09:35:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE Procedure [dbo].[Proc_Update_Risk_Status] AS

BEGIN

TRUNCATE TABLE NESTRT.dbo.Tb002_Risk_Status

INSERT INTO NESTRT.dbo.Tb002_Risk_Status
SELECT Id_Portfolio, 
SUM(CASE WHEN CurrValue/Risk_Limit_Value>1 THEN 1 ELSE 0 END) Lim_Any,
SUM(CASE WHEN Risk_Limit_Type=3 AND CurrValue/Risk_Limit_Value>1 THEN 1 ELSE 0 END) Lim_VaR,
SUM(CASE WHEN Risk_Limit_Type=14 AND CurrValue/Risk_Limit_Value>1 THEN 1 ELSE 0 END) Lim_Long,
SUM(CASE WHEN Risk_Limit_Type=15 AND CurrValue/Risk_Limit_Value>1 THEN 1 ELSE 0 END) Lim_Short,
SUM(CASE WHEN Risk_Limit_Type=10 AND CurrValue/Risk_Limit_Value>1 THEN 1 ELSE 0 END) Lim_Gross,
SUM(CASE WHEN Risk_Limit_Type=11 AND CurrValue/Risk_Limit_Value>1 THEN 1 ELSE 0 END) Lim_Net
FROM
(
SELECT *, (SELECT Limit_Value FROM nestdb.[dbo].[FCN_Risk_Limit_Value](Id_Portfolio,Risk_Limit_Type)) AS CurrValue
FROM nestdb.[dbo].[FCN_Risk_Limits]()
) AS A
WHERE Risk_Limit_Type NOT IN (2,20) AND CurrValue/Risk_Limit_Value>1
GROUP BY Id_Portfolio

END

