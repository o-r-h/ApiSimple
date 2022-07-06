USE [BaseExampleDB]
GO
/****** Object:  Table [dbo].[Example]    Script Date: 19/11/2021 13:38:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Example](
	[IdExample] [bigint] IDENTITY(1,1) NOT NULL,
	[NameExample] [nvarchar](50) NULL,
	[PriceExample] [decimal](18, 2) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedAt] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Example] PRIMARY KEY CLUSTERED 
(
	[IdExample] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
