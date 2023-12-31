USE [NESTRT]
GO
/****** Object:  Table [dbo].[Tb065_Ultimo_Preco]    Script Date: 10/26/2010 09:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb065_Ultimo_Preco](
	[IdSrValue] [int] IDENTITY(1,1) NOT NULL,
	[IdSecurity] [int] NOT NULL,
	[SrValue] [decimal](18, 5) NOT NULL,
	[SrType] [int] NOT NULL,
	[FeederTimeStamp] [datetime] NOT NULL CONSTRAINT [DF_Tb065_Ultimo_Preco_TimeStamp]  DEFAULT (getdate()),
	[Source] [int] NOT NULL CONSTRAINT [DF_Tb065_Ultimo_Preco_Source]  DEFAULT ((0)),
	[SourceTimeStamp] [datetime] NOT NULL,
	[Automated] [tinyint] NOT NULL CONSTRAINT [DF_Tb065_Ultimo_Preco_automated]  DEFAULT ((1))
) ON [PRIMARY]
