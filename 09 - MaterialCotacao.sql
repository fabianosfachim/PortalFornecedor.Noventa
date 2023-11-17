

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[materialcotacao]') AND type in (N'U'))
DROP TABLE [dbo].[materialcotacao]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[materialcotacao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Cotacao_Id] [int] NOT NULL,
	[Material_Id] [int] NOT NULL,
	[PrecoUnitario] [decimal](19, 2) NULL,
	[QuantidadeRequisitada] [decimal](19, 2) NULL,
	[SubTotal] [decimal](19, 2) NULL,
	[PercentualDesconto] [decimal](19, 2) NULL,
	[PercentualIpi] [decimal](19, 2) NULL,
	[ValorIpi] [decimal](19, 2) NULL,
	[TotalItens] [decimal](19, 2) NULL,
	[Unidade] [varchar](50)  NULL,
	[TipoMaterial] [varchar](500) NULL,
	[IpiIncluso] [bit] NULL,
	[PercentualIcms] [decimal](19, 2) NULL,
	[PrazoEntrega] [int] NULL,
	[Marca] [varchar](100) NULL,
	[Observacao] [varchar](max) NULL,
	[Fabricante_Id] [int] NULL,
	[Equivalencia] [varchar](100) NULL,
	[Referencia] [varchar](100) NULL,
	[NomeUsuarioCadastro] [varchar](50) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[NomeUsuarioAlteracao] [varchar](50) NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_materialcotacao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[materialcotacao]  WITH CHECK ADD  CONSTRAINT [FK_materialcotacao_cotacao] FOREIGN KEY([Cotacao_Id])
REFERENCES [dbo].[cotacao] ([Id])
GO

ALTER TABLE [dbo].[materialcotacao] CHECK CONSTRAINT [FK_materialcotacao_cotacao]
GO

ALTER TABLE [dbo].[materialcotacao]  WITH CHECK ADD  CONSTRAINT [FK_materialcotacao_fabricante] FOREIGN KEY([Fabricante_Id])
REFERENCES [dbo].[fabricante] ([Id])
GO

ALTER TABLE [dbo].[materialcotacao] CHECK CONSTRAINT [FK_materialcotacao_fabricante]
GO

ALTER TABLE [dbo].[materialcotacao]  WITH CHECK ADD  CONSTRAINT [FK_materialcotacao_material] FOREIGN KEY([Material_Id])
REFERENCES [dbo].[material] ([Id])
GO

ALTER TABLE [dbo].[materialcotacao] CHECK CONSTRAINT [FK_materialcotacao_material]
GO



