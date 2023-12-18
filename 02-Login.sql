IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[login]') AND type in (N'U'))
DROP TABLE [dbo].[login]
GO


CREATE TABLE [dbo].[login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Nome] [varchar](100) NOT NULL,
	[NomeUsuarioCadastro] [varchar](50) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[NomeUsuarioAlteracao] [varchar](50) NULL,
	[DataAlteracaoCadastro] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
	[DataUltimaSessaoAtivaUsuario] [datetime] NULL,
	[Guid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_tb_login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


