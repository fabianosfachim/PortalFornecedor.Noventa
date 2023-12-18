IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cotacao_motivo]') AND type in (N'U'))
DROP TABLE [dbo].[cotacao_motivo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[cotacao_motivo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCotacao] [varchar](40) NOT NULL,
	[IdMotivo] [int] NOT NULL,
 CONSTRAINT [PK_cotacao_motivo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[cotacao_motivo]  WITH CHECK ADD  CONSTRAINT [FK_cotacao_motivo_motivo] FOREIGN KEY([IdMotivo])
REFERENCES [dbo].[motivo] ([Id])
GO

ALTER TABLE [dbo].[cotacao_motivo] CHECK CONSTRAINT [FK_cotacao_motivo_motivo]
GO
