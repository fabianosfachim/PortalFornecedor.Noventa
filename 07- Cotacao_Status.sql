IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cotacao_status]') AND type in (N'U'))
DROP TABLE [dbo].[cotacao_status]
GO

/****** Object:  Table [dbo].[cotacao_status]    Script Date: 12/18/2023 10:09:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[cotacao_status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCotacao] [varchar](40) NOT NULL,
	[IdStatus] [int] NOT NULL,
	[DataStatus] [datetime] NOT NULL,
 CONSTRAINT [PK_cotacao_status_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[cotacao_status]  WITH CHECK ADD  CONSTRAINT [FK_cotacao_status_status] FOREIGN KEY([IdStatus])
REFERENCES [dbo].[status] ([Id])
GO

ALTER TABLE [dbo].[cotacao_status] CHECK CONSTRAINT [FK_cotacao_status_status]
GO


