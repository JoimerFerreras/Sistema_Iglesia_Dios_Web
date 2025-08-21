USE [Iglesia_Dios_Web]
GO
DELETE FROM [dbo].[Modulos]
GO
DELETE FROM [dbo].[Funcionalidades]
GO
SET IDENTITY_INSERT [dbo].[Funcionalidades] ON 
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (1, 0, N'Ingresos', N'Ingresos/frmIngresos.aspx', 1, 2)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (2, 0, N'Egresos', N'Egresos/frmEgresos.aspx', 1, 3)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (3, 0, N'Cuentas_Por_Cobrar', N'Cuentas_Por_Cobrar/frmCuentasCobrar.aspx', 1, 4)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (4, 0, N'Cuentas_Por_Pagar', N'Cuentas_Por_Pagar/frmCuentasPagar.aspx', 1, 5)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (5, 0, N'Miembros', N'Miembros/frmMiembros.aspx', 1, 6)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (6, 8, N'Descripciones', N'Otros_Parametros/frmDescripciones.aspx', 1, 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (7, 8, N'Formas_Pago', N'Otros_Parametros/frmFormas_Pago.aspx', 1, 3)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (8, 8, N'Miscelaneos', N'Otros_Parametros/frmMiscelaneos.aspx', 1, 2)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (9, 0, N'Resumen', N'Resumen/frmResumen.aspx', 1, 1)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (10, 7, N'Log_Usuarios_Accesos', N'Usuarios/frmLogUsuariosAccesos.aspx', 1, 3)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (11, 7, N'Roles', N'Usuarios/frmRoles.aspx', 1, 2)
GO
INSERT [dbo].[Funcionalidades] ([Id_Funcionalidad], [Id_Modulo], [Nombre_Funcionalidad], [Nombre_Archivo], [Estado], [Orden]) VALUES (12, 7, N'Usuarios', N'Usuarios/frmUsuarios.aspx', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Funcionalidades] OFF
GO
SET IDENTITY_INSERT [dbo].[Modulos] ON 
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (1, N'Ingresos', 1, 1)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (2, N'Egresos', 1, 2)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (3, N'Cuentas_Por_Cobrar', 1, 3)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (4, N'Cuentas_Por_Pagar', 1, 4)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (5, N'Miembros', 1, 5)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (6, N'Inventario_Activos_Fijos', 0, 6)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (7, N'Configuracion', 1, 8)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (8, N'Otros_Parametros', 1, 7)
GO
INSERT [dbo].[Modulos] ([Id_Modulo], [Nombre_Modulo], [Estado], [Orden]) VALUES (9, N'Acerca_De', 1, 9)
GO
SET IDENTITY_INSERT [dbo].[Modulos] OFF
GO
