

-- =================================================================== UPDATE UNDERLYING AND ID UNDERLYING
/*
UPDATE A
SET A.[Id Underlying]=B.IdUnderlying, A.[Underlying]
FROM NESTDB.dbo.Tb000_Historical_Positions A
INNER JOIN NESTDB.dbo.Tb001_Securities B
ON A.[Id ticker]=B.IdSecurity
INNER JOIN NESTDB.dbo.Tb001_Securities C
ON B.IdUnderlying=C.IdSecurity
WHERE [Id Underlying]=0
*/

-- =================================================================== UPDATE ### BASE ### UNDERLYING AND ID UNDERLYING
/*
UPDATE A
SET [Base Underlying]=nestdb.dbo.[Z_IdBaseUnderlying]([id ticker]) 
FROM NESTDB.dbo.Tb000_Historical_Positions A

--ALTER FUNCTION [dbo].[Z_IdBaseUnderlying] (@Id_Ativo int)  
--
--RETURNS varchar(50) AS  
--
--BEGIN 
--
--Declare @ReturnValue varchar(50)
--
--SELECT @ReturnValue=Ticker FROM FCN_Get_Id_Underlying(@Id_Ativo)
--
--return @ReturnValue
--
--END
*/

-- =================================================================== UPDATE SECTOR

/*
UPDATE A SET A.[Nest Sector]=D.Setor
FROM NESTDB.dbo.Tb000_Historical_Positions A
INNER JOIN NESTDB.dbo.Tb001_Securities_Fixed AS B ON A.[Id Ticker] = B.IdSecurity 
INNER JOIN NESTDB.dbo.Tb000_Issuers AS C ON B.IdIssuer = C.IdIssuer 
INNER JOIN NESTDB.dbo.Tb113_Setores AS D ON C.IdNestSector = D.Id_Setor
*/

-- =================================================================== UPDATE ASSET CLASS
/*
UPDATE A SET A.[asset class]=Classe_Ativo
FROM NESTDB.dbo.Tb000_Historical_Positions A
INNER JOIN NESTDB.dbo.Tb001_Securities_Fixed AS B ON A.[Id Ticker] = B.IdSecurity 
INNER JOIN NESTDB.dbo.Tb028_Classe_Ativo AS C ON B.[idassetclass] = C.Id_Classe_Ativo
*/
-- =================================================================== UPDATE BASE UNDERLYING CURRENCY
/*
UPDATE A SET [Id Base Underlying Currency]=IdCurrency
FROM NESTDB.dbo.Tb000_Historical_Positions A
INNER JOIN NESTDB.dbo.Tb001_Securities_Fixed AS B ON A.[Id Base Underlying] = B.IdSecurity 
*/

-- =================================================================== UPDATE ASSET CLASS
/*
UPDATE A SET [Id Base Underlying Currency]=IdCurrency
FROM NESTDB.dbo.Tb000_Historical_Positions A
INNER JOIN NESTDB.dbo.Tb001_Securities_Fixed AS B ON A.[Id Base Underlying] = B.IdSecurity 
*/


SELECT [id asset class] , *
FROM NESTDB.dbo.Tb000_Historical_Positions A
WHERE [Date now]='2010-11-26' ORDER BY Ticker


