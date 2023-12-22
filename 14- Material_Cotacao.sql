
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[material_cotacao]') AND type in (N'U'))
DROP TABLE [dbo].[material_cotacao]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[material_cotacao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Material_Id] [int] NOT NULL,
	[Descricao] [varchar](200) NOT NULL,
	[Cotacao_Id] [int] NOT NULL,
	[NomeFabricante] [varchar](200) NULL,
	[QuantidadeRequisitada] [int] NULL,
	[PrecoUnitario] [decimal](19, 4) NULL,
	[PercentualDesconto] [decimal](19, 2) NULL,
	[IpiIncluso] [bit] NULL,
	[PercentualIpi] [decimal](19, 2) NULL,
	[ValorIpi] [decimal](19, 2) NULL,
	[PercentualIcms] [decimal](19, 2) NULL,
	[PrazoEntrega] [int] NULL,
	[Marca] [varchar](100) NULL,
	[SubTotal] [decimal](19, 2) NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_material_cotacao_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO




ALTER TABLE [dbo].[material_cotacao]  WITH CHECK ADD  CONSTRAINT [FK_tb_material_cotacao] FOREIGN KEY([Cotacao_Id])
REFERENCES [dbo].[cotacao] ([Id])
GO

ALTER TABLE [dbo].[material_cotacao] CHECK CONSTRAINT [FK_tb_material_cotacao]
GO


