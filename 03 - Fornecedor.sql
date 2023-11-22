USE [bd_PortalFornecedor]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fornecedor]') AND type in (N'U'))
DROP TABLE [dbo].[fornecedor]
GO

/****** Object:  Table [dbo].[fornecedor]    Script Date: 11/22/2023 9:50:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[fornecedor](
	[Id] [int] NOT NULL,
	[CnpjCpf] [varchar](14) NOT NULL,
	[RazaoSocial] [varchar](100) NOT NULL,
	[CEP] [varchar](8) NOT NULL,
	[Logradouro] [varchar](100) NOT NULL,
	[Numero] [varchar](5) NOT NULL,
	[Complemento] [varchar](50) NULL,
	[Cidade] [varchar](16) NOT NULL,
	[IdEstado] [int] NOT NULL,
	[NomeUsuarioCadastro] [varchar](50) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[NomeUsuarioAlteracao] [varchar](50) NULL,
	[DataAlteracao] [datetime] NULL,
 CONSTRAINT [PK_tb_cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[fornecedor]  WITH CHECK ADD  CONSTRAINT [FK_tb_fornecedor_estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[estado] ([Id])
GO

ALTER TABLE [dbo].[fornecedor] CHECK CONSTRAINT [FK_tb_fornecedor_estado]
GO


