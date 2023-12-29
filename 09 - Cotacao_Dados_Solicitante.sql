
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cotacao_dados_solicitante]') AND type in (N'U'))
DROP TABLE [dbo].[cotacao_dados_solicitante]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[cotacao_dados_solicitante](
	[Id] [Int] IDENTITY(1,1) NOT NULL,
    [IdCotacao] [varchar](40) NOT NULL,
	[DataSolicitacao] [datetime] NULL,
	[Nome] [varchar](500) NULL,
	[CNPJ] [varchar](14) NULL,
	[DataEntrega] [datetime] NULL,
	[Endereco] [varchar](50) NULL,
	[CEP] [varchar](20) NULL,
	[Cidade] [varchar](50) NULL,
	[Estado] [varchar](2) NULL,
	[Contato] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Telefone] [varchar](20) NULL,
 CONSTRAINT [PK_cotacao_dados_solicitante] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IXCotacao] ON [dbo].[cotacao_dados_solicitante]
(
	[IdCotacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_Consulta_Data] ON [dbo].[cotacao_dados_solicitante]
(
	[Id] ASC,
	[DataSolicitacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO



