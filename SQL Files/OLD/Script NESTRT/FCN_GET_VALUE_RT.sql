USE [NESTRT]
GO
/****** Object:  UserDefinedFunction [dbo].[FCN_GET_VALUE_RT]    Script Date: 10/26/2010 09:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE FUNCTION [dbo].[FCN_GET_VALUE_RT] 
(
@IdTicker int,@IniDate datetime,@PriceType int,@IdSource int
)

RETURNS @tabela table(Return_Value float,Date_Value datetime,IdSecurity int,Source int)
AS
BEGIN
DECLARE @Return_Value float
DECLARE @Date_Value datetime
DECLARE @TickerType int
DECLARE @Source int

--DECLARE @Tabela_Temp table(IdSecurity int, Return_Value float,Date_Value datetime,Source int)
set @Return_Value=0
set @Date_Value='19000101'
set @Source=0

IF @PriceType=999999--IN (9, 21)
	BEGIN
		SET @Return_Value=dbo.Get_RT_Quote(@IdTicker, @PriceType)
		SET @Date_Value='19000101'
		SET @Source=5
	END
ELSE
	BEGIN
		IF @IdSource=0 
			BEGIN
				SELECT TOP 1 @Return_Value=SrValue, @Date_Value=SourceTimeStamp,@Source=Source 
				FROM(
						SELECT SrValue, SourceTimeStamp,Source, 
						CASE
						when Source=18 then 0	
						when Source=15 then 1	
						when Source=7 then 2
						when Source=5 then 3 
						when Source=1 then 4
						ELSE 5  END Ordem 
						FROM [NESTRT].dbo.Tb065_Ultimo_Preco(NOLOCK) 
						WHERE IdSecurity=@IdTicker and SrType=@PriceType and FeederTimeStamp >= convert(varchar ,getdate(),112)
				)Z
				ORDER BY Ordem
	/*
				SELECT TOP 1 @Return_Value=SrValue, @Date_Value=SourceTimeStamp,@Source=Source 
				FROM Tb065_Ultimo_Preco(nolock) 
				WHERE IdSecurity=@IdTicker and SrType=@PriceType
				and FeederTimeStamp >= convert(varchar ,getdate(),112) order by FeederTimeStamp desc 
	*/
			END
		ELSE
			BEGIN
				IF @IdSource=-1 
					BEGIN
						SELECT TOP 1 @Return_Value=SrValue, @Date_Value=SourceTimeStamp,@Source=Source 
						FROM(
								SELECT SrValue, SourceTimeStamp,Source, 
								CASE
								when Source=18 then 0	
								when Source=15 then 1	
								when Source=7 then 2
								when Source=5 then 3 
								when Source=1 then 4
								ELSE 5 END Ordem 
								FROM [NESTRT].dbo.Tb065_Ultimo_Preco(NOLOCK) 
								WHERE Source IN(1,5,7,18,15) AND IdSecurity=@IdTicker and SrType=@PriceType and FeederTimeStamp >= convert(varchar ,getdate(),112)
						)Z
						ORDER BY Ordem
					END		
				ELSE
					BEGIN
						IF @IdSource=-2 
							BEGIN
								SELECT TOP 1 @Return_Value=SrValue, @Date_Value=SourceTimeStamp,@Source=Source 
								FROM(
										SELECT SrValue, SourceTimeStamp,Source, 
										CASE
										when Source=19 then 0	
										when Source=18 then 1	
										when Source=7 then 2
										when Source=5 then 3 
										when Source=1 then 4
										ELSE 5 END Ordem 
										FROM [NESTRT].dbo.Tb065_Ultimo_Preco(NOLOCK) 
										WHERE Source IN(1,5,7,18,19) AND IdSecurity=@IdTicker and SrType=@PriceType and FeederTimeStamp >= convert(varchar ,getdate(),112)
								)Z
								ORDER BY Ordem
							END		
						ELSE
							BEGIN
								SELECT @Return_Value=SrValue, @Date_Value=SourceTimeStamp,@Source=Source FROM [NESTRT].dbo.Tb065_Ultimo_Preco(nolock) 
								WHERE IdSecurity=@IdTicker AND SrType=@PriceType 
								AND FeederTimeStamp >= convert(varchar ,getdate(),112) AND Source=@IdSource
							END
					END
			END
	END 

	INSERT INTO @tabela(IdSecurity,Return_Value,Date_Value,Source)
		VALUES (@IdTicker,@Return_Value,@Date_Value,@Source)

	RETURN 

END
















