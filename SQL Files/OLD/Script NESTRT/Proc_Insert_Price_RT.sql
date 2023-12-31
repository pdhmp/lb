USE [NESTRT]
GO
/****** Object:  StoredProcedure [dbo].[Proc_Insert_Price_RT]    Script Date: 10/26/2010 09:35:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE Procedure [dbo].[Proc_Insert_Price_RT]
(
	 @IdSecurity numeric 
	 ,@SrValue float
	 ,@Data datetime
	 ,@SrType float
	 ,@Source int
	 ,@SourceTimeStamp datetime
	 ,@Automated int
)
	AS

Declare @Id_Tipo_Ativo int
Declare @Quoted_As_Rate int
DECLARE @TempTicker bigint

--SELECT @Id_Tipo_Ativo = ID_Tipo_Ativo, @Quoted_As_Rate=Quoted_As_Rate FROM [NESTDB].dbo.Tb001_Ativos (nolock) WHERE IdSecurity = @IdSecurity
/*
===========================================   TRIGGER IS DOING THIS NOW    ========================================
IF @Quoted_As_Rate=1 AND @SrType=1 
	BEGIN
		SET @SrType=30
		IF @SrValue<>0 SET @SrValue=@SrValue/100
	END

IF @Quoted_As_Rate=1 AND @SrType=9 
	BEGIN
		SET @SrType=31
		IF @SrValue<>0 SET @SrValue=@SrValue/100
	END

IF @Quoted_As_Rate=1 AND @SrType=10
	BEGIN
		SET @SrType=32
		IF @SrValue<>0 SET @SrValue=@SrValue/100
	END
===================================================================================================================
*/

INSERT INTO [NESTRT].dbo.Tb065_Ultimo_Preco(IdSecurity,SrValue,SrType, Source, SourceTimeStamp,Automated)
VALUES(@IdSecurity,@SrValue,@SrType,@source, @SourceTimeStamp,@Automated)

/*
IF @Id_Tipo_Ativo = 7 AND @SrType = 1
	BEGIN
		DECLARE @tempVol float
		SELECT @tempVol=NESTDB.dbo.FCN_Calc_Implied_Vol_Id_Ticker(@IdSecurity,getdate(),@SrValue)

		INSERT INTO [NESTRT].dbo.Tb065_Ultimo_Preco(IdSecurity,SrValue,SrType, Source, SourceTimeStamp,Automated)
		VALUES(@IdSecurity,@tempVol,40,@source, @SourceTimeStamp,@Automated)
	END
*/




