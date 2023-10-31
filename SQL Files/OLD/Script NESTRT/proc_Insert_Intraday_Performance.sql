set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go











ALTER PROCEDURE [dbo].[proc_Insert_Intraday_Performance]
AS

INSERT INTO NESTRT.dbo.Tb001_Intraday_Performance
SELECT getdate(),[Id Portfolio],SUM([Contribution Pc]) 
FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock)
GROUP BY [Id Portfolio]
UNION
SELECT getdate(),1073 AS Id_Ticker2, NESTDB.dbo.FCN_GETD_RT_Value_Only(1073, 1, 0, 0)/NESTDB.dbo.FCN_GET_PRICE_Value_Only(1073, getdate(),1, 0, 2, 0, 0)-1
UNION
SELECT getdate(),900 AS Id_Ticker2, NESTDB.dbo.FCN_GETD_RT_Value_Only(900, 1, 0, 0)/NESTDB.dbo.FCN_GET_PRICE_Value_Only(900, getdate(),1, 0, 2, 0, 0)-1








