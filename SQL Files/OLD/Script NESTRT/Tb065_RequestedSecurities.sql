USE [NESTRT]
GO
/****** Object:  Table [dbo].[Tb065_RequestedSecurities]    Script Date: 10/26/2010 09:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb065_RequestedSecurities](
	[IdSecurity] [int] NOT NULL,
 CONSTRAINT [PK_Tb065_RequestedSecurities] PRIMARY KEY CLUSTERED 
(
	[IdSecurity] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
