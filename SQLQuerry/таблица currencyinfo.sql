USE [CurrencyState]
GO

/****** Object:  Table [dbo].[CurrencyInfo]    Script Date: 8/13/2019 12:43:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CurrencyInfo](
	[Id] [int] NOT NULL,
	[CurrencyType] [char](1) NULL,
	[CurrencyFullName] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


