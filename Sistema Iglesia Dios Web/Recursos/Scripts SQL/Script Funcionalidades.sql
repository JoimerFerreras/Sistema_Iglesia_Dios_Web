USE [Iglesia_Dios_Web]
GO
/****** Object:  Table [dbo].[Funcionalidades]    Script Date: 1/5/2025 12:46:55 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Funcionalidades]') AND type in (N'U'))
DROP TABLE [dbo].[Funcionalidades]
GO
/****** Object:  Table [dbo].[Funcionalidades]    Script Date: 1/5/2025 12:46:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionalidades](
	[Id_Funcionalidad] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Funcionalidad] [varchar](100) NULL,
	[Nombre_Archivo] [varchar](100) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Funcionalidades] PRIMARY KEY CLUSTERED 
(
	[Id_Funcionalidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Funcionalidades] ON 
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (1, N'Ingresos', N'frmIngresos', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (2, N'Egresos', N'frmEgresos', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (3, N'Cuentas_Por_Cobrar', N'frmCuentasCobrar', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (4, N'Cuentas_Por_Pagar', N'frmCuentasPagar', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (5, N'Miembros', N'frmMiembros', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (6, N'Descripciones', N'frmDescripciones', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (7, N'Formas_Pago', N'frmFormas_Pago', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (8, N'Miscelaneos', N'frmMiscelaneos', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (9, N'Resumen', N'frmResumen', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (10, N'Log_Usuarios_Accesos', N'frmLogUsuariosAccesos', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (11, N'Roles', N'frmRoles', 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado]) VALUES (12, N'Usuarios', N'frmUsuarios', 1)
GO
SET IDENTITY_INSERT [dbo].[Funcionalidades] OFF
GO
