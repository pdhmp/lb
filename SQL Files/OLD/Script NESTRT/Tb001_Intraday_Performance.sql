USE [NESTRT]
GO
/****** Object:  Table [dbo].[Tb001_Intraday_Performance]    Script Date: 10/26/2010 09:32:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb001_Intraday_Performance](
	[Id_RT_Performance] [int] IDENTITY(1,1) NOT NULL,
	[Perf_DateTime] [datetime] NOT NULL,
	[Id_Portfolio] [int] NOT NULL,
	[curPerformance] [float] NOT NULL
) ON [PRIMARY]
