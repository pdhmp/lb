USE [NESTRT]
GO
/****** Object:  Table [dbo].[Tb002_Risk_Status]    Script Date: 10/26/2010 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb002_Risk_Status](
	[Id_Portfolio] [int] NULL,
	[Lim_Any] [int] NULL,
	[Lim_VaR] [int] NULL,
	[Lim_Long] [int] NULL,
	[Lim_Short] [int] NULL,
	[Lim_Gross] [int] NULL,
	[Lim_Net] [int] NULL
) ON [PRIMARY]
