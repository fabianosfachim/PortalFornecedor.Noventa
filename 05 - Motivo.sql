IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[motivo]') AND type in (N'U'))
DROP TABLE [dbo].[motivo]
GO


CREATE TABLE [dbo].[motivo](
	[Id] [int]  IDENTITY(1,1) NOT NULL,
	[NomeMotivo] [varchar](50) NOT NULL,
 CONSTRAINT [PK_motivo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[motivo] ON 
GO
INSERT [dbo].[motivo] ([id], [NomeMotivo]) VALUES (1, N'Estoque')
GO
INSERT [dbo].[motivo] ([id], [NomeMotivo]) VALUES (2, N'Utilização Imediata')

GO
SET IDENTITY_INSERT [dbo].[motivo] OFF
GO