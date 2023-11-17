IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[status]') AND type in (N'U'))
DROP TABLE [dbo].[status]
GO


CREATE TABLE [dbo].[status](
	[Id] [int]  IDENTITY(1,1) NOT NULL,
	[NomeStatus] [varchar](50) NOT NULL,
 CONSTRAINT [PK_status] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

go
/****** Object:  Index [IXStatus]    Script Date: 11/3/2023 7:03:34 PM ******/
CREATE NONCLUSTERED INDEX [IXStatus] ON [dbo].[status]
(
	[NomeStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


GO
SET IDENTITY_INSERT [dbo].[status] ON 
GO
INSERT [dbo].[status] ([id], [NomeStatus]) VALUES (1, N'Pendente')
GO
INSERT [dbo].[status] ([id], [NomeStatus]) VALUES (2, N'Enviada')
GO
INSERT [dbo].[status] ([id], [NomeStatus]) VALUES (3, N'Aprovada')
GO
INSERT [dbo].[status] ([id], [NomeStatus]) VALUES (4, N'Não Aprovada')

GO
SET IDENTITY_INSERT [dbo].[status] OFF
GO