
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[estado]') AND type in (N'U'))
DROP TABLE [dbo].[estado]
GO

CREATE TABLE [dbo].[estado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomeEstado] [varchar](50) NOT NULL,
	[SiglaEstado] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_estado] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[estado] ON 
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (1, N'Acre', N'AC')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (2, N'Alagoas', N'AL')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (3, N'Amap�', N'AP')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (4, N'Amazonas', N'AM')

INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (5, N'Bahia', N'BA')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (6, N'Cear�', N'CE')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (7, N'Esp�rito Santo', N'ES')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (8, N'Goi�s', N'GO')

INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (9, N'Maranh�o', N'MA')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (10, N'Mato Grosso', N'MT')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (11, N'Mato Grosso do Sul', N'MS')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (12, N'Minas Gerais', N'MG')

INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (13, N'Par�', N'PA')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (14, N'Para�ba', N'PB')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (15, N'Paran�', N'PT')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (16, N'Pernambuco', N'PE')

INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (17, N'Piau�', N'PI')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (18, N'Rio de Janeiro', N'RJ')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (19, N'Rio Grande do Norte', N'RN')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (20, N'Rio Grande do Sul', N'RS')

INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (21, N'Rond�nia', N'RO')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (22, N'Roraima', N'RR')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (23, N'Santa Catarina', N'SC')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (24, N'S�o Paulo', N'SP')

INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (25, N'Sergipe', N'SE')
GO
INSERT [dbo].[estado] ([id], [NomeEstado], [SiglaEstado]) VALUES (26, N'Tocantins', N'TO')


GO
SET IDENTITY_INSERT [dbo].[estado] OFF
GO