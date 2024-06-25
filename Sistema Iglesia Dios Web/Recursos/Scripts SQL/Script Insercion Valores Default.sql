USE [Iglesia_Dios_Web]
GO

-- Insertar usuario por defecto
INSERT [dbo].[Usuarios] ([Id_Usuario], [Nombre1], [Nombre2], [Apellido1], [Apellido2], [Genero], [Bloqueo], [Id_Rol], [Fecha_Creacion], [Fecha_Ultima_Modificacion], [Celular], [Telefono], [Correo], [Usuario], [Password], [Verificacion_Dos_Pasos], [RestablecerPassword]) VALUES (1, N'Joimer', N'Emanuel', N'Ferreras', N'Cuevas', 1, 0, 0, NULL, NULL, N'', N'', N'', N'Administrador', 0x77684D62AF2B117893981CF2E509EB41, 0, 0)
GO

-- Insertar moneda por defecto
INSERT INTO Monedas(Nombre_Moneda, Estado) VALUES('DOP', 1)



