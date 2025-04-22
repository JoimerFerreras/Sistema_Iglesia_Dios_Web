USE [master]
GO
/****** Object:  Database [Iglesia_Dios_Web]    Script Date: 22/04/2025 02:46:43 p.m. ******/
CREATE DATABASE [Iglesia_Dios_Web]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Iglesia_Dios_Web', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Iglesia_Dios_Web.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[Archivos_Cuentas_Por_Cobrar]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivos_Cuentas_Por_Cobrar](
	[Id_Archivo] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cuenta_Cobrar] [int] NULL,
	[NombreArchivo] [varchar](150) NOT NULL,
	[NombreArchivoCarpeta] [varchar](150) NOT NULL,
	[TipoArchivo] [varchar](250) NOT NULL,
	[Extencion] [varchar](10) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Archivo] [varbinary](max) NULL,
	[Fecha_Registro] [datetime] NULL,
	[Tamano] [float] NULL,
 CONSTRAINT [PK_Archivos_Cuentas_Por_Cobrar] PRIMARY KEY CLUSTERED 
(
	[Id_Archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Archivos_Cuentas_Por_Pagar]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivos_Cuentas_Por_Pagar](
	[Id_Archivo] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cuenta_Pagar] [int] NULL,
	[NombreArchivo] [varchar](150) NOT NULL,
	[NombreArchivoCarpeta] [varchar](150) NOT NULL,
	[TipoArchivo] [varchar](250) NOT NULL,
	[Extencion] [varchar](10) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Archivo] [varbinary](max) NULL,
	[Fecha_Registro] [datetime] NULL,
	[Tamano] [float] NULL,
 CONSTRAINT [PK_Archivos_Cuentas_Por_Pagar] PRIMARY KEY CLUSTERED 
(
	[Id_Archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Archivos_Egresos]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivos_Egresos](
	[Id_Archivo] [int] IDENTITY(1,1) NOT NULL,
	[Id_Egreso] [int] NULL,
	[NombreArchivo] [varchar](150) NOT NULL,
	[NombreArchivoCarpeta] [varchar](150) NOT NULL,
	[TipoArchivo] [varchar](250) NOT NULL,
	[Extencion] [varchar](10) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Archivo] [varbinary](max) NULL,
	[Fecha_Registro] [datetime] NULL,
	[Tamano] [float] NULL,
 CONSTRAINT [PK_Archivos_Egresos] PRIMARY KEY CLUSTERED 
(
	[Id_Archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Archivos_Ingresos]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Archivos_Ingresos](
	[Id_Archivo] [int] IDENTITY(1,1) NOT NULL,
	[Id_Ingreso] [int] NULL,
	[NombreArchivo] [varchar](150) NOT NULL,
	[NombreArchivoCarpeta] [varchar](150) NOT NULL,
	[TipoArchivo] [varchar](250) NOT NULL,
	[Extencion] [varchar](10) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Archivo] [varbinary](max) NULL,
	[Fecha_Registro] [datetime] NULL,
	[Tamano] [float] NULL,
 CONSTRAINT [PK_Archivos_Ingresos] PRIMARY KEY CLUSTERED 
(
	[Id_Archivo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas_Por_Cobrar]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas_Por_Cobrar](
	[Id_Cuenta_Cobrar] [int] IDENTITY(1,1) NOT NULL,
	[Id_Descripcion] [int] NULL,
	[Id_Miembro] [int] NULL,
	[Id_Miscelaneo] [int] NULL,
	[Fecha_CC] [datetime] NULL,
	[Valor] [float] NULL,
	[Id_Forma_Pago] [int] NULL,
	[Tipo_Documento] [char](2) NULL,
	[No_Documento] [varchar](15) NULL,
	[Comentario] [varchar](500) NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
 CONSTRAINT [PK_Cuentas_Por_Cobrar] PRIMARY KEY CLUSTERED 
(
	[Id_Cuenta_Cobrar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas_Por_Pagar]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas_Por_Pagar](
	[Id_Cuenta_Pagar] [int] IDENTITY(1,1) NOT NULL,
	[Id_Descripcion] [int] NULL,
	[Id_Miembro] [int] NULL,
	[Id_Miscelaneo] [int] NULL,
	[Fecha_CP] [datetime] NULL,
	[Valor] [float] NULL,
	[Id_Forma_Pago] [int] NULL,
	[Tipo_Documento] [char](2) NULL,
	[No_Documento] [varchar](15) NULL,
	[Comentario] [varchar](500) NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
 CONSTRAINT [PK_Cuentas_Por_Pagar] PRIMARY KEY CLUSTERED 
(
	[Id_Cuenta_Pagar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Descripciones]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Descripciones](
	[Id_Descripcion] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Tipo_Descripcion] [int] NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Descripciones] PRIMARY KEY CLUSTERED 
(
	[Id_Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Egresos]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Egresos](
	[Id_Egreso] [int] IDENTITY(1,1) NOT NULL,
	[Id_Miembro] [int] NULL,
	[Id_Descripcion] [int] NULL,
	[Monto] [float] NULL,
	[Fecha_Egreso] [datetime] NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
	[Comentario] [varchar](500) NULL,
	[Id_Forma_Pago] [int] NULL,
	[Id_Miscelaneo] [int] NULL,
 CONSTRAINT [PK_Egresos] PRIMARY KEY CLUSTERED 
(
	[Id_Egreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Formas_Pago]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formas_Pago](
	[Id_Forma_Pago] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Forma_Pago] [varchar](50) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Formas_Pago] PRIMARY KEY CLUSTERED 
(
	[Id_Forma_Pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingresos]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingresos](
	[Id_Ingreso] [int] IDENTITY(1,1) NOT NULL,
	[Id_Miembro] [int] NULL,
	[Id_Descripcion] [int] NULL,
	[Monto] [float] NULL,
	[Fecha_Ingreso] [datetime] NULL,
	[Id_Usuario_Registro] [int] NULL,
	[Fecha_Registro] [datetime] NULL,
	[Id_Usuario_Ultima_Modificacion] [int] NULL,
	[Fecha_Ultima_Modificacion] [datetime] NULL,
	[Comentario] [varchar](500) NULL,
	[Id_Forma_Pago] [int] NULL,
	[Id_Miscelaneo] [int] NULL,
 CONSTRAINT [PK_Ingresos] PRIMARY KEY CLUSTERED 
(
	[Id_Ingreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Miembros_Informacion_Familiar_1]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Miembros_Informacion_Familiar_2]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Miembros_Informacion_Laboral]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Miembros_Ministerios]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miembros_Ministerios](
	[Id_Miembro] [int] NOT NULL,
	[Id_Ministerio] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Miembros_Nivel_Academico]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Miembros_Pasatiempos]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Ministerios]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
/****** Object:  Table [dbo].[Miscelaneos]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Miscelaneos](
	[Id_Miscelaneo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion_Miscelaneo] [varchar](100) NULL,
	[Estado] [bit] NULL,
 CONSTRAINT [PK_Miscelaneos] PRIMARY KEY CLUSTERED 
(
	[Id_Miscelaneo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 22/04/2025 02:46:43 p.m. ******/
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
ALTER TABLE [dbo].[Archivos_Cuentas_Por_Cobrar]  WITH CHECK ADD  CONSTRAINT [FK_Archivos_Cuentas_Por_Cobrar_Cuentas_Por_Cobrar] FOREIGN KEY([Id_Cuenta_Cobrar])
REFERENCES [dbo].[Cuentas_Por_Cobrar] ([Id_Cuenta_Cobrar])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Archivos_Cuentas_Por_Cobrar] CHECK CONSTRAINT [FK_Archivos_Cuentas_Por_Cobrar_Cuentas_Por_Cobrar]
GO
ALTER TABLE [dbo].[Archivos_Cuentas_Por_Pagar]  WITH CHECK ADD  CONSTRAINT [FK_Archivos_Cuentas_Por_Pagar_Cuentas_Por_Pagar] FOREIGN KEY([Id_Cuenta_Pagar])
REFERENCES [dbo].[Cuentas_Por_Pagar] ([Id_Cuenta_Pagar])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Archivos_Cuentas_Por_Pagar] CHECK CONSTRAINT [FK_Archivos_Cuentas_Por_Pagar_Cuentas_Por_Pagar]
GO
ALTER TABLE [dbo].[Archivos_Egresos]  WITH CHECK ADD  CONSTRAINT [FK_Archivos_Egresos_Egresos] FOREIGN KEY([Id_Egreso])
REFERENCES [dbo].[Egresos] ([Id_Egreso])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Archivos_Egresos] CHECK CONSTRAINT [FK_Archivos_Egresos_Egresos]
GO
ALTER TABLE [dbo].[Archivos_Ingresos]  WITH CHECK ADD  CONSTRAINT [FK_Archivos_Ingresos_Ingresos] FOREIGN KEY([Id_Ingreso])
REFERENCES [dbo].[Ingresos] ([Id_Ingreso])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Archivos_Ingresos] CHECK CONSTRAINT [FK_Archivos_Ingresos_Ingresos]
GO
ALTER TABLE [dbo].[Cuentas_Por_Cobrar]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_Por_Cobrar_Cuentas_Por_Cobrar] FOREIGN KEY([Id_Cuenta_Cobrar])
REFERENCES [dbo].[Cuentas_Por_Cobrar] ([Id_Cuenta_Cobrar])
GO
ALTER TABLE [dbo].[Cuentas_Por_Cobrar] CHECK CONSTRAINT [FK_Cuentas_Por_Cobrar_Cuentas_Por_Cobrar]
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_1]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Informacion_Familiar_1_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_1] CHECK CONSTRAINT [FK_Miembros_Informacion_Familiar_1_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_2]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Informacion_Familar_2_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
GO
ALTER TABLE [dbo].[Miembros_Informacion_Familiar_2] CHECK CONSTRAINT [FK_Miembros_Informacion_Familar_2_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Informacion_Laboral]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Informacion_Laboral_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
GO
ALTER TABLE [dbo].[Miembros_Informacion_Laboral] CHECK CONSTRAINT [FK_Miembros_Informacion_Laboral_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Nivel_Academico]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Nivel_Academico_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
GO
ALTER TABLE [dbo].[Miembros_Nivel_Academico] CHECK CONSTRAINT [FK_Miembros_Nivel_Academico_Miembros]
GO
ALTER TABLE [dbo].[Miembros_Pasatiempos]  WITH CHECK ADD  CONSTRAINT [FK_Miembros_Pasatiempos_Miembros] FOREIGN KEY([Id_Miembro])
REFERENCES [dbo].[Miembros] ([Id_Miembro])
GO
ALTER TABLE [dbo].[Miembros_Pasatiempos] CHECK CONSTRAINT [FK_Miembros_Pasatiempos_Miembros]
GO
/****** Object:  StoredProcedure [dbo].[ReporteIngresos_Detalle]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReporteIngresos_Detalle]
    @TipoFecha NVARCHAR(50),
    @FechaInicial DATETIME,
    @FechaFinal DATETIME,
    @Miembro INT,
    @Descripcion_Ingreso INT,
    @Moneda INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX) = N'
        SELECT 
            Id_Ingreso,
            DI.Descripcion_Ingreso,
            M.Nombres + '' '' + M.Apellidos AS Miembro,
            Mon.Nombre_Moneda AS Moneda,
            Monto,
            Valor_Moneda,
            FORMAT(Fecha_Ingreso, ''dd/MM/yyyy'') AS Fecha_Ingreso,
            FORMAT(Fecha_Registro, ''dd/MM/yyyy'') AS Fecha_Registro
        FROM Ingresos I
        LEFT JOIN Descripciones_Ingreso DI ON DI.Id_Descripcion_Ingreso = I.Id_Descripcion_Ingreso
        LEFT JOIN Miembros M ON M.Id_Miembro = I.Id_Miembro
        LEFT JOIN Monedas Mon ON Mon.Id_Moneda = I.Id_Moneda
        WHERE (' + QUOTENAME(@TipoFecha) + ' BETWEEN @FechaInicial AND @FechaFinal)';

    IF @Miembro > 0
        SET @sql = @sql + ' AND (M.Id_Miembro = @Miembro)';

    IF @Descripcion_Ingreso > 0
        SET @sql = @sql + ' AND (DI.Id_Descripcion_Ingreso = @Descripcion_Ingreso)';

    IF @Moneda > 0
        SET @sql = @sql + ' AND (Mon.Id_Moneda = @Moneda)';

    SET @sql = @sql + ' ORDER BY Id_Ingreso DESC';

    -- Ejecutar la consulta dinámica con parámetros
    EXEC sp_executesql @sql, 
        N'@FechaInicial DATETIME, @FechaFinal DATETIME, @Miembro INT, @Descripcion_Ingreso INT, @Moneda INT', 
        @FechaInicial, @FechaFinal, @Miembro, @Descripcion_Ingreso, @Moneda;
END
GO
/****** Object:  StoredProcedure [dbo].[ReporteIngresos_Resumen]    Script Date: 22/04/2025 02:46:43 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ReporteIngresos_Resumen]
    @TipoFecha NVARCHAR(50),
    @FechaInicial DATETIME,
    @FechaFinal DATETIME,
    @Miembro INT = 0,
    @Descripcion_Ingreso INT = 0,
    @Moneda INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX) = N'
        SELECT 
            DI.Descripcion_Ingreso,
            Mon.Nombre_Moneda AS Moneda,
            SUM(Monto) AS Total_Monto
        FROM Ingresos I
        LEFT JOIN Descripciones_Ingreso DI ON DI.Id_Descripcion_Ingreso = I.Id_Descripcion_Ingreso
        LEFT JOIN Monedas Mon ON Mon.Id_Moneda = I.Id_Moneda
        WHERE (' + QUOTENAME(@TipoFecha) + ' BETWEEN @FechaInicial AND @FechaFinal)';

    -- Filtros opcionales
    IF @Miembro > 0
        SET @sql = @sql + ' AND (I.Id_Miembro = @Miembro)';

    IF @Descripcion_Ingreso > 0
        SET @sql = @sql + ' AND (DI.Id_Descripcion_Ingreso = @Descripcion_Ingreso)';

    IF @Moneda > 0
        SET @sql = @sql + ' AND (Mon.Id_Moneda = @Moneda)';

    -- Agrupar y ordenar
    SET @sql = @sql + ' GROUP BY DI.Descripcion_Ingreso, Mon.Nombre_Moneda ORDER BY DI.Descripcion_Ingreso';

    -- Ejecutar SQL dinámico con parámetros
    EXEC sp_executesql @sql, 
        N'@FechaInicial DATETIME, @FechaFinal DATETIME, @Miembro INT, @Descripcion_Ingreso INT, @Moneda INT', 
        @FechaInicial, @FechaFinal, @Miembro, @Descripcion_Ingreso, @Moneda;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Nombre de pila' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Miembros', @level2type=N'COLUMN',@level2name=N'Nombre_Pila'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Numero para uso interno en la iglesia' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Miembros', @level2type=N'COLUMN',@level2name=N'Numero_Alternativo_Miembro'
GO
USE [master]
GO
ALTER DATABASE [Iglesia_Dios_Web] SET  READ_WRITE 
GO
