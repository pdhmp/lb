USE NESTDB

DECLARE @Id_Portfolio1 int 
DECLARE @Id_Portfolio2 int 
DECLARE @Id_Book int 
DECLARE @Id_Section int

DECLARE @Id_Ticker int

DECLARE @Id_Estrategia int 
DECLARE @Id_Sub_Estrategia int 

DECLARE @Book varchar(20)
DECLARE @Id_Sub_Portfolio varchar(20)
DECLARE @Sub_Portfolio varchar(20)
DECLARE @New_Id_Strategy varchar(20)
DECLARE @New_Strategy varchar(20)
DECLARE @New_Id_Sub_Strategy varchar(20)
DECLARE @New_Sub_Strategy varchar(20)
DECLARE @Section varchar(20)

SET @Id_Book = 1
SET @Id_Section = 2

SET @Id_Ticker=5790


SET @Id_Portfolio1 = 10
SET @Id_Portfolio2 = 11

Set @Id_Estrategia=10
Set @Id_Sub_Estrategia=8

SELECT 
@Book=Book,
@Id_Sub_Portfolio=Id_Sub_Portfolio,
@Sub_Portfolio=Sub_Portfolio,
@New_Id_Strategy=Id_Strategy,
@New_Strategy=Strategy,
@New_Id_Sub_Strategy=Id_Sub_Strategy,
@New_Sub_Strategy=Sub_Strategy,
@Section=Section
FROM dbo.VW_Book_Strategies WHERE Id_Book=@Id_Book and Id_Section=@Id_Section

-- ========================================== ATUALIZAR HISTORICAL POSITIONS =================================

UPDATE dbo.Tb000_Historical_Positions SET
	[Id Book]=@Id_Book
	,[Book]=@Book
	,[Id Sub Portfolio]=@Id_Sub_Portfolio
	,[Sub Portfolio]=@Sub_Portfolio
	,[New Id Strategy]=@New_Id_Strategy
	,[New Strategy]=@New_Strategy
	,[New Id Sub Strategy]=@New_Id_Sub_Strategy
	,[New Sub Strategy]=@New_Sub_Strategy
	,[Id Section]=@Id_Section
	,[Section]=@Section
	WHERE [Id Portfolio] in (@Id_Portfolio1,@Id_Portfolio2) AND [Date Now]>='2010-01-01' 
	 AND [Date Now] <='2010-01-08' 
	AND [ZId Strategy] =@Id_Estrategia AND [ZId Sub Strategy] =@Id_Sub_Estrategia
	AND [Id Ticker]=@Id_Ticker

-- ========================================== ATUALIZAR TRADES =================================

	UPDATE A SET [Id Book]=@Id_Book ,[Id Section]=@Id_Section
	FROM Tb012_Ordens A
	inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account = B.Id_Account
	WHERE Id_Portfolio in (@Id_Portfolio1,@Id_Portfolio2) AND Data_Abert_Ordem>='2010-01-01' 
	AND Data_Abert_Ordem<='2010-01-08' 
	AND Z_Estrategia =@Id_Estrategia AND Z_Sub_Estrategia =@Id_Sub_Estrategia
	AND Id_Ativo=@Id_Ticker


-- ========================================== ATUALIZAR TRANSACTIONS =================================
/*
UPDATE A SET [Id Book1]=@Id_Book ,[Id Section1]=@Id_Section
	FROM dbo.Tb700_Transactions A
	inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account1 = B.Id_Account
	WHERE Id_Portfolio in (@Id_Portfolio1,@Id_Portfolio2) AND Trade_Date<='2010-01-04' 
	--AND  zId_Strategy1=@Id_Estrategia AND zId_Sub_Strategy1 =@Id_Sub_Estrategia
	AND [Id_Ticker1]=@Id_Ticker


UPDATE A SET [Id Book2]=@Id_Book ,[Id Section2]=@Id_Section
	FROM dbo.Tb700_Transactions A
	inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account2 = B.Id_Account
	WHERE Id_Portfolio in (@Id_Portfolio1,@Id_Portfolio2) AND Trade_Date<='2010-01-04' 
	--AND zId_Strategy2 =@Id_Estrategia AND zId_Sub_Strategy2 =@Id_Sub_Estrategia
	AND [Id_Ticker2]=@Id_Ticker
*/



/*
UPDATE A SET [Id Section1]=2
	FROM dbo.Tb700_Transactions A
	inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account1 = B.Id_Account
	WHERE Id_Portfolio in (4,6) AND Trade_Date<='2010-01-04' 
	--AND  zId_Strategy1=@Id_Estrategia AND zId_Sub_Strategy1 =@Id_Sub_Estrategia
	AND [Id_Ticker1]=595 and [Id Section1]=56


UPDATE A SET [Id Section2]=2
	FROM dbo.Tb700_Transactions A
	inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account2 = B.Id_Account
	WHERE Id_Portfolio in (4,6) AND Trade_Date<='2010-01-04' 
	--AND zId_Strategy2 =@Id_Estrategia AND zId_Sub_Strategy2 =@Id_Sub_Estrategia
	AND [Id_Ticker2]=595 and [Id Section2]=56


*/

Select *
from Tb012_Ordens A
inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account = B.Id_Account
	WHERE Id_Portfolio in (10,11) AND Data_Abert_Ordem>='2010-01-01' 
and [Id Section]=56
order by Data_Abert_Ordem


Select *
from Tb012_Ordens A
inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account = B.Id_Account
	WHERE Id_Portfolio in (10,11) AND Data_Abert_Ordem>='2010-01-01' 
and Id_Ativo =439
order by Data_Abert_Ordem


Select *
	FROM Tb012_Ordens A
	inner join dbo.Tb003_PortAccounts B
	ON A.Id_Account = B.Id_Account
	WHERE Id_Portfolio in (10,11) AND Data_Abert_Ordem='2010-01-08' 
	--AND Data_Abert_Ordem<='2010-04-08' 
	--AND Z_Estrategia =@Id_Estrategia AND Z_Sub_Estrategia =@Id_Sub_Estrategia
	AND Id_Ativo=7

Select * FROM Tb000_Historical_Positions
	WHERE [Id Portfolio] in (10,11) AND [Date Now]='2010-01-08' 
	-- AND [Date Now] <='2010-01-08' 
	--AND [ZId Strategy] =@Id_Estrategia AND [ZId Sub Strategy] =@Id_Sub_Estrategia
	AND [Id Ticker]=7


