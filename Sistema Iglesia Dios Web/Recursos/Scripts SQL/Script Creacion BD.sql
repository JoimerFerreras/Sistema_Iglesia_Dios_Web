USE [master]
GO
/****** Object:  Database [Iglesia_Dios_Web]    Script Date: 12/06/2024 01:01:02 a.m. ******/
CREATE DATABASE [Iglesia_Dios_Web]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Iglesia_Dios_Web', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Iglesia_Dios_Web.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Iglesia_Dios_Web_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Iglesia_Dios_Web_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Iglesia_Dios_Web] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Iglesia_Dios_Web].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ARITHABORT OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET RECOVERY FULL 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET  MULTI_USER 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Iglesia_Dios_Web] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Iglesia_Dios_Web] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Iglesia_Dios_Web', N'ON'
GO
ALTER DATABASE [Iglesia_Dios_Web] SET QUERY_STORE = ON
GO
ALTER DATABASE [Iglesia_Dios_Web] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Iglesia_Dios_Web]
GO
/****** Object:  Table [dbo].[Abonos_Cuentas_Pagar]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Abonos_Cuentas_Pagar](
	[Id_Abono_CP] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cuenta_Pagar] [int] NULL,
	[Monto_Abono] [float] NULL,
	[Moneda] [int] NULL,
	[Fecha_Abono] [datetime] NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
	[Comentario] [varchar](100) NULL,
	[Id_Forma_Pago] [int] NULL,
 CONSTRAINT [PK_Abonos_Cuentas_Pagar] PRIMARY KEY CLUSTERED 
(
	[Id_Abono_CP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Archivos_Ingresos]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivos_Ingresos](
	[Id_Archivo] [int] NOT NULL,
	[Id_Ingreso] [int] NULL,
	[NombreArchivo] [varchar](250) NULL,
	[NombreArchivoCarpeta] [varchar](250) NULL,
	[Ruta] [varchar](250) NULL,
	[Extencion] [varchar](10) NULL,
	[Descripcion] [varchar](250) NULL,
	[Archivo] [varbinary](max) NULL,
	[Fecha_Registro] [datetime] NULL,
 CONSTRAINT [PK_Archivos_Ingresos] PRIMARY KEY CLUSTERED 
(
	[Id_Archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas_Por_Pagar]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas_Por_Pagar](
	[Id_Cuenta] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Egreso] [int] NULL,
	[Monto_Total_Pagar] [float] NULL,
	[Moneda] [int] NULL,
	[Valor_Moneda] [float] NULL,
	[Fecha] [datetime] NULL,
	[No_Factura] [varchar](15) NULL,
	[Beneficiario] [int] NULL,
	[Otro_Beneficiario] [varchar](30) NULL,
	[Fecha_Vencimiento] [datetime] NULL,
	[Comentario] [varchar](100) NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
	[Id_Forma_Pago] [int] NULL,
 CONSTRAINT [PK_Cuentas_Por_Pagar] PRIMARY KEY CLUSTERED 
(
	[Id_Cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Descripciones_Egreso]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Descripciones_Egreso](
	[Id_Descripcion_Egreso] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Egreso] [varchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Descripciones_Egresos] PRIMARY KEY CLUSTERED 
(
	[Id_Descripcion_Egreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Descripciones_Ingreso]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Descripciones_Ingreso](
	[Id_Descripcion_Ingreso] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Ingreso] [varchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Descripciones_Ingreso] PRIMARY KEY CLUSTERED 
(
	[Id_Descripcion_Ingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Formas_Pago]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formas_Pago](
	[Id_Forma_Pago] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Forma_Pago] [varchar](30) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Formas_Pago] PRIMARY KEY CLUSTERED 
(
	[Id_Forma_Pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingresos]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingresos](
	[Id_Ingreso] [int] IDENTITY(1,1) NOT NULL,
	[Id_Miembro] [int] NULL,
	[Id_Descripcion_Ingreso] [int] NULL,
	[Id_Moneda] [int] NULL,
	[Monto] [float] NULL,
	[Fecha_Ingreso] [datetime] NULL,
	[Valor_Moneda] [float] NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
	[Comentario] [varchar](100) NULL,
	[Id_Forma_Pago] [int] NULL,
 CONSTRAINT [PK_Ingresos] PRIMARY KEY CLUSTERED 
(
	[Id_Ingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros](
	[Id_Miembro] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](50) NULL,
	[Apellidos] [varchar](50) NULL,
	[Nombre_Pila] [varchar](30) NULL,
	[Sexo] [int] NULL,
	[Fecha_Nacimiento] [datetime] NULL,
	[Estado_Civil] [int] NULL,
	[Tiene_Hijos] [bit] NULL,
	[Email] [varchar](50) NULL,
	[Celular] [varchar](15) NULL,
	[Sector] [varchar](50) NULL,
	[Barrio_Residencial] [varchar](50) NULL,
	[Calle] [varchar](80) NULL,
	[Numero_Casa] [varchar](10) NULL,
	[Es_Miembro] [bit] NULL,
	[Desde_Cuando_Miembro] [datetime] NULL,
	[Pertenece_Ministerio] [bit] NULL,
	[Le_Gustaria_Pertenecer_Ministerio] [bit] NULL,
	[Numero_Alternativo_Miembro] [int] NULL,
	[Rol_Miembro] [int] NULL,
	[Otro_Rol] [varchar](30) NULL,
	[Nombre_Diacono] [varchar](30) NULL,
	[Nombre_Lider_Ministerio] [varchar](30) NULL,
	[Comentarios_Diacono_Lider_Ministerio] [varchar](300) NULL,
	[Revisado_Por] [varchar](30) NULL,
	[Autorizado_Por] [varchar](30) NULL,
 CONSTRAINT [PK_Miembros] PRIMARY KEY CLUSTERED 
(
	[Id_Miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Informacion_Familiar_1]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Informacion_Familiar_1](
	[Id_Miembro] [int] NOT NULL,
	[Conyuge_Nombre] [varchar](80) NULL,
	[Conyuge_Cristiano] [bit] NULL,
	[Conyuge_FechaNacimiento] [datetime] NULL,
	[Hijo1_Nombre] [varchar](80) NOT NULL,
	[Hijo1_FechaNacimiento] [datetime] NULL,
	[Hijo1_Cristiano] [bit] NULL,
	[Hijo2_Nombre] [varchar](80) NOT NULL,
	[Hijo2_FechaNacimiento] [datetime] NULL,
	[Hijo2_Cristiano] [bit] NULL,
	[Hijo3_Nombre] [varchar](80) NOT NULL,
	[Hijo3_FechaNacimiento] [datetime] NULL,
	[Hijo3_Cristiano] [bit] NULL,
	[Hijo4_Nombre] [varchar](80) NOT NULL,
	[Hijo4_FechaNacimiento] [datetime] NULL,
	[Hijo4_Cristiano] [bit] NULL,
	[Hijo5_Nombre] [varchar](80) NOT NULL,
	[Hijo5_FechaNacimiento] [datetime] NULL,
	[Hijo5_Cristiano] [bit] NULL,
	[Hijo6_Nombre] [varchar](80) NOT NULL,
	[Hijo6_FechaNacimiento] [datetime] NULL,
	[Hijo6_Cristiano] [bit] NULL,
 CONSTRAINT [PK_Miembros_Informacion_Familiar] PRIMARY KEY CLUSTERED 
(
	[Id_Miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Informacion_Familiar_2]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Informacion_Familiar_2](
	[Id_Miembro] [int] NOT NULL,
	[Padre_Nombre_Completo] [varchar](80) NULL,
	[Padre_Edad] [int] NULL,
	[Padre_Empleado] [bit] NULL,
	[Padre_Negocio_Propio] [bit] NULL,
	[Padre_Celular] [varchar](15) NULL,
	[Padre_Miembro_Iglesia] [bit] NULL,
	[Madre_Nombre_Completo] [varchar](80) NULL,
	[Madre_Edad] [int] NULL,
	[Madre_Empleada] [bit] NULL,
	[Madre_Negocio_Propio] [bit] NULL,
	[Madre_Celular] [varchar](15) NULL,
	[Madre_Miembro_Iglesia] [bit] NULL,
	[Hermano1_Nombre_Completo] [varchar](80) NULL,
	[Hermano1_Escolaridad] [varchar](30) NULL,
	[Hermano1_Correo_Electronico] [varchar](50) NULL,
	[Hermano1_Celular] [varchar](15) NULL,
	[Hermano2_Nombre_Completo] [varchar](80) NULL,
	[Hermano2_Escolaridad] [varchar](30) NULL,
	[Hermano2_Correo_Electronico] [varchar](50) NULL,
	[Hermano2_Celular] [varchar](15) NULL,
	[Hermano3_Nombre_Completo] [varchar](80) NULL,
	[Hermano3_Escolaridad] [varchar](30) NULL,
	[Hermano3_Correo_Electronico] [varchar](50) NULL,
	[Hermano3_Celular] [varchar](15) NULL,
	[Hermano4_Nombre_Completo] [varchar](80) NULL,
	[Hermano4_Escolaridad] [varchar](30) NULL,
	[Hermano4_Correo_Electronico] [varchar](50) NULL,
	[Hermano4_Celular] [varchar](15) NULL,
	[Hermano5_Nombre_Completo] [varchar](80) NULL,
	[Hermano5_Escolaridad] [varchar](30) NULL,
	[Hermano5_Correo_Electronico] [varchar](50) NULL,
	[Hermano5_Celular] [varchar](15) NULL,
 CONSTRAINT [PK_Miembros_Informacion_Familar_2] PRIMARY KEY CLUSTERED 
(
	[Id_Miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Informacion_Laboral]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Informacion_Laboral](
	[Id_Miembro] [int] NOT NULL,
	[Empleado_Privado] [bit] NULL,
	[Empleado_Publico] [bit] NULL,
	[Dueno_Negocio] [bit] NULL,
	[Independiente] [bit] NULL,
	[Otros] [bit] NULL,
	[Nombre_Empresa_Negocio] [varchar](80) NULL,
 CONSTRAINT [PK_Miembros_Informacion_Laboral] PRIMARY KEY CLUSTERED 
(
	[Id_Miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Ministerios]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Ministerios](
	[Id_Miembro] [int] NOT NULL,
	[Id_Ministerio] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Nivel_Academico]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Nivel_Academico](
	[Id_Miembro] [int] NOT NULL,
	[Primario] [bit] NULL,
	[Secundario] [bit] NULL,
	[Grado_Universitario] [bit] NULL,
	[Post_Grado_Maestria] [bit] NULL,
 CONSTRAINT [PK_Miembros_Nivel_Academico] PRIMARY KEY CLUSTERED 
(
	[Id_Miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Pasatiempos]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Pasatiempos](
	[Id_Miembro] [int] NOT NULL,
	[Cine] [bit] NULL,
	[Leer] [bit] NULL,
	[Ver_TV] [bit] NULL,
	[Socializar] [bit] NULL,
	[Viajar] [bit] NULL,
	[Otros] [varchar](100) NULL,
 CONSTRAINT [PK_Miembros_Pasatiempos] PRIMARY KEY CLUSTERED 
(
	[Id_Miembro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ministerios]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ministerios](
	[Id_Ministerio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Ministerio] [varchar](100) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Ministerios] PRIMARY KEY CLUSTERED 
(
	[Id_Ministerio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monedas]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monedas](
	[Id_Moneda] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Moneda] [varchar](10) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Monedas] PRIMARY KEY CLUSTERED 
(
	[Id_Moneda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/06/2024 01:01:02 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre1] [varchar](30) NULL,
	[Nombre2] [varchar](30) NULL,
	[Apellido1] [varchar](30) NULL,
	[Apellido2] [varchar](30) NULL,
	[Genero] [int] NULL,
	[Bloqueo] [bit] NULL,
	[Id_Rol] [int] NULL,
	[Fecha_Creacion] [datetime] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
	[Celular] [varchar](20) NULL,
	[Telefono] [varchar](20) NULL,
	[Correo] [varchar](80) NULL,
	[Usuario] [varchar](30) NULL,
	[Password] [varbinary](100) NULL,
	[Verificacion_Dos_Pasos] [bit] NULL,
	[RestablecerPassword] [bit] NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_1]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Informacion_Familiar_1_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_1] CHECK CONSTRAINT [FK_Miembros_Informacion_Familiar_1_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_2]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Informacion_Familar_2_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_2] CHECK CONSTRAINT [FK_Miembros_Informacion_Familar_2_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Informacion_Laboral]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Informacion_Laboral_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Miembros_Informacion_Laboral] CHECK CONSTRAINT [FK_Miembros_Informacion_Laboral_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Nivel_Academico]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Nivel_Academico_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Miembros_Nivel_Academico] CHECK CONSTRAINT [FK_Miembros_Nivel_Academico_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Pasatiempos]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Pasatiempos_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Miembros_Pasatiempos] CHECK CONSTRAINT [FK_Miembros_Pasatiempos_Miembros]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de pila' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Miembros', @level2type=N'COLUMN',@level2name=N'Nombre_Pila'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero para uso interno en la iglesia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Miembros', @level2type=N'COLUMN',@level2name=N'Numero_Alternativo_Miembro'
GO
USE [master]
GO
ALTER DATABASE [Iglesia_Dios_Web] SET  READ_WRITE 
GO
