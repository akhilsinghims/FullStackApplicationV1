USE [DemoApiDb]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 24-08-2020 20:28:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
	[AccountId] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[AccountType] [nvarchar](45) NOT NULL,
	[OwnerId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Owner] ([OwnerId])
GO


