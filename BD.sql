
/****** Object:  Database [Maven]    Script Date: 04/03/2026 02:07:19 ******/
CREATE DATABASE [Maven]
 CONTAINMENT = NONE

ALTER DATABASE [Maven] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Maven].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Maven] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Maven] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Maven] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Maven] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Maven] SET ARITHABORT OFF 
GO
ALTER DATABASE [Maven] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Maven] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Maven] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Maven] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Maven] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Maven] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Maven] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Maven] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Maven] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Maven] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Maven] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Maven] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Maven] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Maven] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Maven] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Maven] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Maven] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Maven] SET RECOVERY FULL 
GO
ALTER DATABASE [Maven] SET  MULTI_USER 
GO
ALTER DATABASE [Maven] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Maven] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Maven] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Maven] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Maven] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Maven] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Maven', N'ON'
GO
ALTER DATABASE [Maven] SET QUERY_STORE = OFF
GO
USE [Maven]
GO
/****** Object:  Table [dbo].[CategoriaJoya]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriaJoya](
	[CategoriaJoyaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoriaJoyaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CondicionObjeto]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CondicionObjeto](
	[CondicionObjetoId] [int] IDENTITY(1,1) NOT NULL,
	[NombreCondicion] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CondicionObjetoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoObjeto]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoObjeto](
	[EstadoObjetoId] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoObjetoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPago]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPago](
	[EstadoPagoId] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoPagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoSubasta]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoSubasta](
	[EstadoSubastaId] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoSubastaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoUsuario]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoUsuario](
	[EstadoUsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[NombreEstado] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EstadoUsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Joya]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Joya](
	[JoyaId] [int] IDENTITY(1,1) NOT NULL,
	[VendedorId] [int] NOT NULL,
	[Nombre] [nvarchar](120) NOT NULL,
	[Descripcion] [nvarchar](1000) NOT NULL,
	[EstadoObjetoId] [int] NOT NULL,
	[CondicionObjetoId] [int] NOT NULL,
	[FechaRegistro] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JoyaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JoyaCategoria]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoyaCategoria](
	[JoyaId] [int] NOT NULL,
	[CategoriaJoyaId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JoyaId] ASC,
	[CategoriaJoyaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JoyaImagen]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JoyaImagen](
	[JoyaImagenId] [int] IDENTITY(1,1) NOT NULL,
	[JoyaId] [int] NOT NULL,
	[UrlImagen] [nvarchar](400) NOT NULL,
	[FechaRegistro] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JoyaImagenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notificacion]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificacion](
	[NotificacionId] [bigint] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[SubastaId] [int] NULL,
	[Tipo] [nvarchar](40) NOT NULL,
	[Mensaje] [nvarchar](500) NOT NULL,
	[Leida] [bit] NOT NULL,
	[FechaCreacion] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificacionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[SubastaId] [int] NOT NULL,
	[CompradorId] [int] NOT NULL,
	[VendedorId] [int] NOT NULL,
	[Monto] [decimal](18, 2) NOT NULL,
	[EstadoPagoId] [int] NOT NULL,
	[FechaRegistro] [datetime2](0) NOT NULL,
	[FechaConfirmacion] [datetime2](0) NULL,
PRIMARY KEY CLUSTERED 
(
	[SubastaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puja]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puja](
	[PujaId] [bigint] IDENTITY(1,1) NOT NULL,
	[SubastaId] [int] NOT NULL,
	[CompradorId] [int] NOT NULL,
	[MontoOfertado] [decimal](18, 2) NOT NULL,
	[FechaHora] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PujaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[RolId] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subasta]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subasta](
	[SubastaId] [int] IDENTITY(1,1) NOT NULL,
	[JoyaId] [int] NOT NULL,
	[VendedorId] [int] NOT NULL,
	[FechaInicio] [datetime2](0) NOT NULL,
	[FechaCierre] [datetime2](0) NOT NULL,
	[PrecioBase] [decimal](18, 2) NOT NULL,
	[IncrementoMinimo] [decimal](18, 2) NOT NULL,
	[EstadoSubastaId] [int] NOT NULL,
	[FechaCreacion] [datetime2](0) NOT NULL,
	[FechaPublicacion] [datetime2](0) NULL,
PRIMARY KEY CLUSTERED 
(
	[SubastaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubastaResultado]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubastaResultado](
	[SubastaId] [int] NOT NULL,
	[GanadorId] [int] NULL,
	[PujaGanadoraId] [bigint] NULL,
	[MontoFinal] [decimal](18, 2) NULL,
	[FechaCierre] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubastaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 04/03/2026 02:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Correo] [nvarchar](120) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[NombreCompleto] [nvarchar](120) NOT NULL,
	[RolId] [int] NOT NULL,
	[EstadoUsuarioId] [int] NOT NULL,
	[FechaRegistro] [datetime2](0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CategoriaJoya] ON 
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (1, N'Anillos')
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (4, N'Aretes')
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (7, N'Broches')
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (2, N'Collares')
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (6, N'Dijes')
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (3, N'Pulseras')
GO
INSERT [dbo].[CategoriaJoya] ([CategoriaJoyaId], [Nombre]) VALUES (5, N'Relojes')
GO
SET IDENTITY_INSERT [dbo].[CategoriaJoya] OFF
GO
SET IDENTITY_INSERT [dbo].[CondicionObjeto] ON 
GO
INSERT [dbo].[CondicionObjeto] ([CondicionObjetoId], [NombreCondicion]) VALUES (1, N'NUEVO')
GO
INSERT [dbo].[CondicionObjeto] ([CondicionObjetoId], [NombreCondicion]) VALUES (2, N'USADO')
GO
SET IDENTITY_INSERT [dbo].[CondicionObjeto] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadoObjeto] ON 
GO
INSERT [dbo].[EstadoObjeto] ([EstadoObjetoId], [NombreEstado]) VALUES (1, N'ACTIVO')
GO
INSERT [dbo].[EstadoObjeto] ([EstadoObjetoId], [NombreEstado]) VALUES (3, N'EN_SUBASTA')
GO
INSERT [dbo].[EstadoObjeto] ([EstadoObjetoId], [NombreEstado]) VALUES (2, N'INACTIVO')
GO
INSERT [dbo].[EstadoObjeto] ([EstadoObjetoId], [NombreEstado]) VALUES (4, N'VENDIDO')
GO
SET IDENTITY_INSERT [dbo].[EstadoObjeto] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadoPago] ON 
GO
INSERT [dbo].[EstadoPago] ([EstadoPagoId], [NombreEstado]) VALUES (2, N'CONFIRMADO')
GO
INSERT [dbo].[EstadoPago] ([EstadoPagoId], [NombreEstado]) VALUES (1, N'PENDIENTE')
GO
SET IDENTITY_INSERT [dbo].[EstadoPago] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadoSubasta] ON 
GO
INSERT [dbo].[EstadoSubasta] ([EstadoSubastaId], [NombreEstado]) VALUES (3, N'ACTIVA')
GO
INSERT [dbo].[EstadoSubasta] ([EstadoSubastaId], [NombreEstado]) VALUES (1, N'BORRADOR')
GO
INSERT [dbo].[EstadoSubasta] ([EstadoSubastaId], [NombreEstado]) VALUES (5, N'CANCELADA')
GO
INSERT [dbo].[EstadoSubasta] ([EstadoSubastaId], [NombreEstado]) VALUES (4, N'FINALIZADA')
GO
INSERT [dbo].[EstadoSubasta] ([EstadoSubastaId], [NombreEstado]) VALUES (2, N'PUBLICADA')
GO
SET IDENTITY_INSERT [dbo].[EstadoSubasta] OFF
GO
SET IDENTITY_INSERT [dbo].[EstadoUsuario] ON 
GO
INSERT [dbo].[EstadoUsuario] ([EstadoUsuarioId], [NombreEstado]) VALUES (1, N'ACTIVO')
GO
INSERT [dbo].[EstadoUsuario] ([EstadoUsuarioId], [NombreEstado]) VALUES (2, N'BLOQUEADO')
GO
SET IDENTITY_INSERT [dbo].[EstadoUsuario] OFF
GO
SET IDENTITY_INSERT [dbo].[Joya] ON 
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (1, 2, N'Anillo solitario de oro 14K con zirconia', N'Anillo elegante en oro 14K con zirconia central corte brillante. Talla 6. Ideal para regalo o compromiso.', 3, 1, CAST(N'2026-02-07T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (2, 2, N'Aretes tipo argolla plata 925 (par)', N'Argollas clásicas en plata 925 con cierre seguro. Ligero, brillo alto. Incluye estuche.', 1, 1, CAST(N'2026-02-12T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (3, 3, N'Collar cadena tipo figaro acero inoxidable', N'Cadena Figaro de acero inoxidable, 55 cm. Resistente al agua, ideal uso diario.', 3, 2, CAST(N'2026-02-14T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (4, 3, N'Reloj clásico estilo minimalista (correa cuero)', N'Reloj análogo estilo minimalista, correa de cuero café, esfera blanca. Buen estado, detalles leves de uso.', 4, 2, CAST(N'2026-01-23T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (5, 4, N'Pulsera ajustable con dije de corazón (plata 925)', N'Pulsera ajustable con dije de corazón en plata 925. Perfecta para regalo. Nuevo, sin uso.', 3, 1, CAST(N'2026-02-20T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (6, 4, N'Dije medalla Virgen (baño oro)', N'Dije tipo medalla con acabado en baño de oro. Tamaño mediano, incluye argolla. Nuevo.', 1, 1, CAST(N'2026-02-22T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Joya] ([JoyaId], [VendedorId], [Nombre], [Descripcion], [EstadoObjetoId], [CondicionObjetoId], [FechaRegistro]) VALUES (7, 2, N'Broche vintage floral (aleación)', N'Broche vintage estilo floral, aleación metálica. Pieza decorativa para ropa o bolso. Buen estado.', 1, 2, CAST(N'2026-02-24T01:24:48.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Joya] OFF
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (1, 1)
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (2, 4)
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (3, 2)
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (4, 5)
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (5, 3)
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (6, 6)
GO
INSERT [dbo].[JoyaCategoria] ([JoyaId], [CategoriaJoyaId]) VALUES (7, 7)
GO
SET IDENTITY_INSERT [dbo].[JoyaImagen] ON 
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (1, 1, N'https://images.unsplash.com/photo-1605100804763-247f67b3557e?w=800&h=600&fit=crop', CAST(N'2026-02-07T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (2, 1, N'https://images.unsplash.com/photo-1605100804763-247f67b3557e?w=800&h=600&fit=crop', CAST(N'2026-02-07T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (3, 2, N'https://images.unsplash.com/photo-1617038220319-276d3cfab638?w=800&h=600&fit=crop', CAST(N'2026-02-12T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (4, 3, N'https://images.unsplash.com/photo-1611652022419-a9419f74343d?w=800&h=600&fit=crop', CAST(N'2026-02-14T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (5, 4, N'https://images.unsplash.com/photo-1524592094714-0f0654e20314?w=800&h=600&fit=crop', CAST(N'2026-01-23T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (6, 5, N'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?w=800&h=600&fit=crop', CAST(N'2026-02-20T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (7, 6, N'https://images.unsplash.com/photo-1599643477877-530eb83abc8e?w=800&h=600&fit=crop', CAST(N'2026-02-22T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (8, 7, N'https://i.etsystatic.com/17849632/r/il/f04fb9/7077379194/il_1588xN.7077379194_8xxo.jpg', CAST(N'2026-02-24T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (9, 6, N'https://images.unsplash.com/photo-1599643477877-530eb83abc8e?w=800&h=600&fit=crop', CAST(N'2026-03-04T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (10, 1, N'https://images.unsplash.com/photo-1605100804763-247f67b3557e?w=800&h=600&fit=crop', CAST(N'2026-02-07T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (11, 1, N'https://images.unsplash.com/photo-1605100804763-247f67b3557e?w=800&h=600&fit=crop', CAST(N'2026-02-07T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (12, 2, N'https://images.unsplash.com/photo-1617038220319-276d3cfab638?w=800&h=600&fit=crop', CAST(N'2026-02-12T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (13, 3, N'https://images.unsplash.com/photo-1611652022419-a9419f74343d?w=800&h=600&fit=crop', CAST(N'2026-02-14T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (14, 4, N'https://images.unsplash.com/photo-1524592094714-0f0654e20314?w=800&h=600&fit=crop', CAST(N'2026-01-23T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (15, 5, N'https://images.unsplash.com/photo-1599643478518-a784e5dc4c8f?w=800&h=600&fit=crop', CAST(N'2026-02-20T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (16, 6, N'https://images.unsplash.com/photo-1599643477877-530eb83abc8e?w=800&h=600&fit=crop', CAST(N'2026-02-22T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (17, 7, N'https://i.etsystatic.com/17849632/r/il/f04fb9/7077379194/il_1588xN.7077379194_8xxo.jpg', CAST(N'2026-02-24T01:25:24.0000000' AS DateTime2))
GO
INSERT [dbo].[JoyaImagen] ([JoyaImagenId], [JoyaId], [UrlImagen], [FechaRegistro]) VALUES (18, 6, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5GmdN2-kULG_3QdcQKRtmT_2eu2s6ld75pA&s', CAST(N'2026-03-04T01:25:25.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[JoyaImagen] OFF
GO
SET IDENTITY_INSERT [dbo].[Notificacion] ON 
GO
INSERT [dbo].[Notificacion] ([NotificacionId], [UsuarioId], [SubastaId], [Tipo], [Mensaje], [Leida], [FechaCreacion]) VALUES (1, 2, 1, N'NUEVA_PUJA', N'Recibiste una nueva puja en tu subasta.', 0, CAST(N'2026-03-04T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Notificacion] ([NotificacionId], [UsuarioId], [SubastaId], [Tipo], [Mensaje], [Leida], [FechaCreacion]) VALUES (2, 8, 1, N'PUJA_REGISTRADA', N'Tu puja fue registrada correctamente.', 1, CAST(N'2026-03-04T00:54:48.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Notificacion] OFF
GO
SET IDENTITY_INSERT [dbo].[Puja] ON 
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (1, 1, 6, CAST(67500.00 AS Decimal(18, 2)), CAST(N'2026-03-03T23:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (2, 1, 7, CAST(70000.00 AS Decimal(18, 2)), CAST(N'2026-03-04T00:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (3, 1, 8, CAST(72500.00 AS Decimal(18, 2)), CAST(N'2026-03-04T00:49:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (4, 2, 5, CAST(19000.00 AS Decimal(18, 2)), CAST(N'2026-03-04T00:29:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (5, 2, 6, CAST(20000.00 AS Decimal(18, 2)), CAST(N'2026-03-04T01:09:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (11, 6, 5, CAST(16500.00 AS Decimal(18, 2)), CAST(N'2026-02-26T19:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (12, 6, 6, CAST(18000.00 AS Decimal(18, 2)), CAST(N'2026-02-26T23:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (13, 7, 7, CAST(54500.00 AS Decimal(18, 2)), CAST(N'2026-02-19T17:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (14, 7, 8, CAST(57500.00 AS Decimal(18, 2)), CAST(N'2026-02-19T22:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (15, 8, 6, CAST(20500.00 AS Decimal(18, 2)), CAST(N'2026-02-11T15:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Puja] ([PujaId], [SubastaId], [CompradorId], [MontoOfertado], [FechaHora]) VALUES (16, 8, 5, CAST(22000.00 AS Decimal(18, 2)), CAST(N'2026-02-12T00:33:25.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Puja] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 
GO
INSERT [dbo].[Rol] ([RolId], [NombreRol]) VALUES (1, N'ADMIN')
GO
INSERT [dbo].[Rol] ([RolId], [NombreRol]) VALUES (2, N'COMPRADOR')
GO
INSERT [dbo].[Rol] ([RolId], [NombreRol]) VALUES (3, N'VENDEDOR')
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Subasta] ON 
GO
INSERT [dbo].[Subasta] ([SubastaId], [JoyaId], [VendedorId], [FechaInicio], [FechaCierre], [PrecioBase], [IncrementoMinimo], [EstadoSubastaId], [FechaCreacion], [FechaPublicacion]) VALUES (1, 1, 2, CAST(N'2026-03-03T19:24:48.0000000' AS DateTime2), CAST(N'2026-03-06T01:24:48.0000000' AS DateTime2), CAST(65000.00 AS Decimal(18, 2)), CAST(2500.00 AS Decimal(18, 2)), 3, CAST(N'2026-03-02T01:24:48.0000000' AS DateTime2), CAST(N'2026-03-03T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Subasta] ([SubastaId], [JoyaId], [VendedorId], [FechaInicio], [FechaCierre], [PrecioBase], [IncrementoMinimo], [EstadoSubastaId], [FechaCreacion], [FechaPublicacion]) VALUES (2, 3, 3, CAST(N'2026-03-03T22:24:48.0000000' AS DateTime2), CAST(N'2026-03-05T01:24:48.0000000' AS DateTime2), CAST(18000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 3, CAST(N'2026-03-03T01:24:48.0000000' AS DateTime2), CAST(N'2026-03-03T15:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Subasta] ([SubastaId], [JoyaId], [VendedorId], [FechaInicio], [FechaCierre], [PrecioBase], [IncrementoMinimo], [EstadoSubastaId], [FechaCreacion], [FechaPublicacion]) VALUES (5, 5, 4, CAST(N'2026-03-04T00:32:43.0000000' AS DateTime2), CAST(N'2026-03-07T01:32:43.0000000' AS DateTime2), CAST(22000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 3, CAST(N'2026-03-03T23:32:43.0000000' AS DateTime2), CAST(N'2026-03-04T00:32:43.0000000' AS DateTime2))
GO
INSERT [dbo].[Subasta] ([SubastaId], [JoyaId], [VendedorId], [FechaInicio], [FechaCierre], [PrecioBase], [IncrementoMinimo], [EstadoSubastaId], [FechaCreacion], [FechaPublicacion]) VALUES (6, 2, 2, CAST(N'2026-02-25T01:33:25.0000000' AS DateTime2), CAST(N'2026-02-27T01:33:25.0000000' AS DateTime2), CAST(15000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 4, CAST(N'2026-02-24T01:33:25.0000000' AS DateTime2), CAST(N'2026-02-25T01:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Subasta] ([SubastaId], [JoyaId], [VendedorId], [FechaInicio], [FechaCierre], [PrecioBase], [IncrementoMinimo], [EstadoSubastaId], [FechaCreacion], [FechaPublicacion]) VALUES (7, 4, 3, CAST(N'2026-02-17T01:33:25.0000000' AS DateTime2), CAST(N'2026-02-20T01:33:25.0000000' AS DateTime2), CAST(52000.00 AS Decimal(18, 2)), CAST(2500.00 AS Decimal(18, 2)), 4, CAST(N'2026-02-16T01:33:25.0000000' AS DateTime2), CAST(N'2026-02-17T01:33:25.0000000' AS DateTime2))
GO
INSERT [dbo].[Subasta] ([SubastaId], [JoyaId], [VendedorId], [FechaInicio], [FechaCierre], [PrecioBase], [IncrementoMinimo], [EstadoSubastaId], [FechaCreacion], [FechaPublicacion]) VALUES (8, 6, 4, CAST(N'2026-02-09T01:33:25.0000000' AS DateTime2), CAST(N'2026-02-12T01:33:25.0000000' AS DateTime2), CAST(18000.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 4, CAST(N'2026-02-08T01:33:25.0000000' AS DateTime2), CAST(N'2026-02-09T01:33:25.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Subasta] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (1, N'admin@maven.cr', N'$2a$11$AdminHashEjemplo1234567890', N'Administrador Maven', 1, 1, CAST(N'2025-11-04T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (2, N'sofia.vargas@maven.cr', N'$2a$11$SellerHashSofia123456789012', N'Sofía Vargas Mora', 3, 1, CAST(N'2025-12-04T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (3, N'daniel.rojas@maven.cr', N'$2a$11$SellerHashDaniel12345678901', N'Daniel Rojas Solís', 3, 1, CAST(N'2025-12-19T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (4, N'valeria.soto@maven.cr', N'$2a$11$SellerHashValeria1234567890', N'Valeria Soto Jiménez', 3, 1, CAST(N'2026-01-03T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (5, N'andres.mora@maven.cr', N'$2a$11$BuyerHashAndres123456789012', N'Andrés Mora Salazar', 2, 1, CAST(N'2026-01-08T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (6, N'camila.monge@maven.cr', N'$2a$11$BuyerHashCamila123456789012', N'Camila Monge Arias', 2, 1, CAST(N'2026-01-11T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (7, N'kevin.espinoza@maven.cr', N'$2a$11$BuyerHashKevin123456789012', N'Kevin Espinoza León', 2, 1, CAST(N'2026-01-18T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (8, N'natalia.ramirez@maven.cr', N'$2a$11$BuyerHashNatalia123456789', N'Natalia Ramírez Peña', 2, 1, CAST(N'2026-01-23T01:24:48.0000000' AS DateTime2))
GO
INSERT [dbo].[Usuario] ([UsuarioId], [Correo], [PasswordHash], [NombreCompleto], [RolId], [EstadoUsuarioId], [FechaRegistro]) VALUES (9, N'usuario.bloq@maven.cr', N'$2a$11$BlockedHash123456789012345', N'Usuario Bloqueado', 2, 2, CAST(N'2026-02-02T01:24:48.0000000' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Categori__75E3EFCFF1DFA1AA]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[CategoriaJoya] ADD UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Condicio__71AAD23D343E2067]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[CondicionObjeto] ADD UNIQUE NONCLUSTERED 
(
	[NombreCondicion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EstadoOb__6CE506158EE93AAD]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[EstadoObjeto] ADD UNIQUE NONCLUSTERED 
(
	[NombreEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EstadoPa__6CE506154DFD9322]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[EstadoPago] ADD UNIQUE NONCLUSTERED 
(
	[NombreEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EstadoSu__6CE50615290D5D90]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[EstadoSubasta] ADD UNIQUE NONCLUSTERED 
(
	[NombreEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__EstadoUs__6CE50615882F41E6]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[EstadoUsuario] ADD UNIQUE NONCLUSTERED 
(
	[NombreEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notificacion_Usuario_Leida]    Script Date: 04/03/2026 02:07:20 ******/
CREATE NONCLUSTERED INDEX [IX_Notificacion_Usuario_Leida] ON [dbo].[Notificacion]
(
	[UsuarioId] ASC,
	[Leida] ASC,
	[FechaCreacion] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Puja_Subasta_Fecha]    Script Date: 04/03/2026 02:07:20 ******/
CREATE NONCLUSTERED INDEX [IX_Puja_Subasta_Fecha] ON [dbo].[Puja]
(
	[SubastaId] ASC,
	[FechaHora] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Puja_Subasta_Monto]    Script Date: 04/03/2026 02:07:20 ******/
CREATE NONCLUSTERED INDEX [IX_Puja_Subasta_Monto] ON [dbo].[Puja]
(
	[SubastaId] ASC,
	[MontoOfertado] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Rol__4F0B537F843E27D1]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[Rol] ADD UNIQUE NONCLUSTERED 
(
	[NombreRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Subasta_Estado_Fechas]    Script Date: 04/03/2026 02:07:20 ******/
CREATE NONCLUSTERED INDEX [IX_Subasta_Estado_Fechas] ON [dbo].[Subasta]
(
	[EstadoSubastaId] ASC,
	[FechaInicio] ASC,
	[FechaCierre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuario__60695A194D8AAF51]    Script Date: 04/03/2026 02:07:20 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Joya] ADD  DEFAULT (sysdatetime()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[JoyaImagen] ADD  DEFAULT (sysdatetime()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Notificacion] ADD  DEFAULT ((0)) FOR [Leida]
GO
ALTER TABLE [dbo].[Notificacion] ADD  DEFAULT (sysdatetime()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Pago] ADD  DEFAULT (sysdatetime()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Puja] ADD  DEFAULT (sysdatetime()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Subasta] ADD  DEFAULT (sysdatetime()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[SubastaResultado] ADD  DEFAULT (sysdatetime()) FOR [FechaCierre]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (sysdatetime()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Joya]  WITH CHECK ADD  CONSTRAINT [FK_Joya_Condicion] FOREIGN KEY([CondicionObjetoId])
REFERENCES [dbo].[CondicionObjeto] ([CondicionObjetoId])
GO
ALTER TABLE [dbo].[Joya] CHECK CONSTRAINT [FK_Joya_Condicion]
GO
ALTER TABLE [dbo].[Joya]  WITH CHECK ADD  CONSTRAINT [FK_Joya_Estado] FOREIGN KEY([EstadoObjetoId])
REFERENCES [dbo].[EstadoObjeto] ([EstadoObjetoId])
GO
ALTER TABLE [dbo].[Joya] CHECK CONSTRAINT [FK_Joya_Estado]
GO
ALTER TABLE [dbo].[Joya]  WITH CHECK ADD  CONSTRAINT [FK_Joya_Vendedor] FOREIGN KEY([VendedorId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[Joya] CHECK CONSTRAINT [FK_Joya_Vendedor]
GO
ALTER TABLE [dbo].[JoyaCategoria]  WITH CHECK ADD  CONSTRAINT [FK_JoyaCategoria_Categoria] FOREIGN KEY([CategoriaJoyaId])
REFERENCES [dbo].[CategoriaJoya] ([CategoriaJoyaId])
GO
ALTER TABLE [dbo].[JoyaCategoria] CHECK CONSTRAINT [FK_JoyaCategoria_Categoria]
GO
ALTER TABLE [dbo].[JoyaCategoria]  WITH CHECK ADD  CONSTRAINT [FK_JoyaCategoria_Joya] FOREIGN KEY([JoyaId])
REFERENCES [dbo].[Joya] ([JoyaId])
GO
ALTER TABLE [dbo].[JoyaCategoria] CHECK CONSTRAINT [FK_JoyaCategoria_Joya]
GO
ALTER TABLE [dbo].[JoyaImagen]  WITH CHECK ADD  CONSTRAINT [FK_JoyaImagen_Joya] FOREIGN KEY([JoyaId])
REFERENCES [dbo].[Joya] ([JoyaId])
GO
ALTER TABLE [dbo].[JoyaImagen] CHECK CONSTRAINT [FK_JoyaImagen_Joya]
GO
ALTER TABLE [dbo].[Notificacion]  WITH CHECK ADD  CONSTRAINT [FK_Notificacion_Subasta] FOREIGN KEY([SubastaId])
REFERENCES [dbo].[Subasta] ([SubastaId])
GO
ALTER TABLE [dbo].[Notificacion] CHECK CONSTRAINT [FK_Notificacion_Subasta]
GO
ALTER TABLE [dbo].[Notificacion]  WITH CHECK ADD  CONSTRAINT [FK_Notificacion_Usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[Notificacion] CHECK CONSTRAINT [FK_Notificacion_Usuario]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_Estado] FOREIGN KEY([EstadoPagoId])
REFERENCES [dbo].[EstadoPago] ([EstadoPagoId])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_Estado]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [FK_Pago_Subasta] FOREIGN KEY([SubastaId])
REFERENCES [dbo].[Subasta] ([SubastaId])
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [FK_Pago_Subasta]
GO
ALTER TABLE [dbo].[Puja]  WITH CHECK ADD  CONSTRAINT [FK_Puja_Comprador] FOREIGN KEY([CompradorId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[Puja] CHECK CONSTRAINT [FK_Puja_Comprador]
GO
ALTER TABLE [dbo].[Puja]  WITH CHECK ADD  CONSTRAINT [FK_Puja_Subasta] FOREIGN KEY([SubastaId])
REFERENCES [dbo].[Subasta] ([SubastaId])
GO
ALTER TABLE [dbo].[Puja] CHECK CONSTRAINT [FK_Puja_Subasta]
GO
ALTER TABLE [dbo].[Subasta]  WITH CHECK ADD  CONSTRAINT [FK_Subasta_Estado] FOREIGN KEY([EstadoSubastaId])
REFERENCES [dbo].[EstadoSubasta] ([EstadoSubastaId])
GO
ALTER TABLE [dbo].[Subasta] CHECK CONSTRAINT [FK_Subasta_Estado]
GO
ALTER TABLE [dbo].[Subasta]  WITH CHECK ADD  CONSTRAINT [FK_Subasta_Joya] FOREIGN KEY([JoyaId])
REFERENCES [dbo].[Joya] ([JoyaId])
GO
ALTER TABLE [dbo].[Subasta] CHECK CONSTRAINT [FK_Subasta_Joya]
GO
ALTER TABLE [dbo].[Subasta]  WITH CHECK ADD  CONSTRAINT [FK_Subasta_Vendedor] FOREIGN KEY([VendedorId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[Subasta] CHECK CONSTRAINT [FK_Subasta_Vendedor]
GO
ALTER TABLE [dbo].[SubastaResultado]  WITH CHECK ADD  CONSTRAINT [FK_SubastaResultado_Ganador] FOREIGN KEY([GanadorId])
REFERENCES [dbo].[Usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[SubastaResultado] CHECK CONSTRAINT [FK_SubastaResultado_Ganador]
GO
ALTER TABLE [dbo].[SubastaResultado]  WITH CHECK ADD  CONSTRAINT [FK_SubastaResultado_Puja] FOREIGN KEY([PujaGanadoraId])
REFERENCES [dbo].[Puja] ([PujaId])
GO
ALTER TABLE [dbo].[SubastaResultado] CHECK CONSTRAINT [FK_SubastaResultado_Puja]
GO
ALTER TABLE [dbo].[SubastaResultado]  WITH CHECK ADD  CONSTRAINT [FK_SubastaResultado_Subasta] FOREIGN KEY([SubastaId])
REFERENCES [dbo].[Subasta] ([SubastaId])
GO
ALTER TABLE [dbo].[SubastaResultado] CHECK CONSTRAINT [FK_SubastaResultado_Subasta]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Estado] FOREIGN KEY([EstadoUsuarioId])
REFERENCES [dbo].[EstadoUsuario] ([EstadoUsuarioId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Estado]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rol] ([RolId])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD  CONSTRAINT [CK_Pago_Monto] CHECK  (([Monto]>(0)))
GO
ALTER TABLE [dbo].[Pago] CHECK CONSTRAINT [CK_Pago_Monto]
GO
ALTER TABLE [dbo].[Puja]  WITH CHECK ADD  CONSTRAINT [CK_Puja_Monto] CHECK  (([MontoOfertado]>(0)))
GO
ALTER TABLE [dbo].[Puja] CHECK CONSTRAINT [CK_Puja_Monto]
GO
ALTER TABLE [dbo].[Subasta]  WITH CHECK ADD  CONSTRAINT [CK_Subasta_Fechas] CHECK  (([FechaCierre]>[FechaInicio]))
GO
ALTER TABLE [dbo].[Subasta] CHECK CONSTRAINT [CK_Subasta_Fechas]
GO
USE [master]
GO
ALTER DATABASE [Maven] SET  READ_WRITE 
GO
