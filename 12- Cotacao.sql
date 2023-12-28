
/****** Object:  Table [dbo].[cotacao]    Script Date: 12/28/2023 12:10:36 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cotacao]') AND type in (N'U'))
DROP TABLE [dbo].[cotacao]
GO

/****** Object:  Table [dbo].[cotacao]    Script Date: 12/28/2023 12:10:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[cotacao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fornecedor_Id] [int] NOT NULL,
	[IdCotacao] [varchar](40) NOT NULL,
	[Motivo_Id] [int] NOT NULL,
	[CotacaoStatus_Id] [int] NOT NULL,
	[Vendedor] [varchar](30) NULL,
	[DataPostagem] [datetime] NULL,
	[CondicoesPagamento_Id] [int] NULL,
	[Frete_Id] [int] NULL,
	[OutrasDespesas] [decimal](19, 2) NULL,
	[ValorFrete] [decimal](19, 2) NULL,
	[ValorFreteForaNota] [decimal](19, 2) NULL,
	[ValorSeguro] [decimal](19, 2) NULL,
	[ValorDesconto] [decimal](19, 2) NULL,
	[Observacao] [varchar](255) NULL,
	[PrazoMaximoCotacao] [datetime] NULL,
	[DataEntregaDesejavel] [datetime] NULL,
	[NomeUsuarioCadastro] [varchar](50) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[NomeUsuarioAlteracao] [varchar](50) NULL,
	[DataAlteracao] [datetime] NULL,
	[Guid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_cotacao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[cotacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_cotacao_cotacaostatus] FOREIGN KEY([CotacaoStatus_Id])
REFERENCES [dbo].[cotacao_status] ([Id])
GO

ALTER TABLE [dbo].[cotacao] CHECK CONSTRAINT [FK_tb_cotacao_cotacaostatus]
GO

ALTER TABLE [dbo].[cotacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_cotacao_fornecedor] FOREIGN KEY([Fornecedor_Id])
REFERENCES [dbo].[fornecedor] ([Id])
GO

ALTER TABLE [dbo].[cotacao] CHECK CONSTRAINT [FK_tb_cotacao_fornecedor]
GO

ALTER TABLE [dbo].[cotacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_cotacao_motivo] FOREIGN KEY([Motivo_Id])
REFERENCES [dbo].[cotacao_motivo] ([Id])
GO

ALTER TABLE [dbo].[cotacao] CHECK CONSTRAINT [FK_tb_cotacao_motivo]
GO


