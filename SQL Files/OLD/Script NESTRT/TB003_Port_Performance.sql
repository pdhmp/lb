USE [NESTRT]
GO
/****** Object:  Table [dbo].[TB003_Port_Performance]    Script Date: 10/26/2010 09:33:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB003_Port_Performance](
	[Port_Name] [varchar](8000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Id_Ticker] [int] NOT NULL,
	[Id_Ticker2] [int] NOT NULL,
	[LastDate] [varchar](8) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MTDDate] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HTDDate] [varchar](34) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[YTDDate] [varchar](34) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[12mDate] [varchar](60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Last_NAV] [float] NULL,
	[MTD_NAV] [float] NULL,
	[HTD_NAV] [float] NULL,
	[YTD_NAV] [float] NULL,
	[12m_NAV] [float] NULL,
	[Today] [float] NULL,
	[MTD] [float] NULL,
	[HTD] [float] NULL,
	[YTD] [float] NULL,
	[12m] [float] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF