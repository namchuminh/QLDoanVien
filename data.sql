USE [master]
GO
/****** Object:  Database [QLDoanVien]    Script Date: 15/05/2023 2:20:09 SA ******/
CREATE DATABASE [QLDoanVien]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DoanVien', FILENAME = N'D:\SQL Server 2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\DoanVien.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DoanVien_log', FILENAME = N'D:\SQL Server 2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\DoanVien_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLDoanVien] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLDoanVien].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLDoanVien] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLDoanVien] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLDoanVien] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLDoanVien] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLDoanVien] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLDoanVien] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLDoanVien] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLDoanVien] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLDoanVien] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLDoanVien] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLDoanVien] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLDoanVien] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLDoanVien] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLDoanVien] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLDoanVien] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLDoanVien] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLDoanVien] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLDoanVien] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLDoanVien] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLDoanVien] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLDoanVien] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLDoanVien] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLDoanVien] SET RECOVERY FULL 
GO
ALTER DATABASE [QLDoanVien] SET  MULTI_USER 
GO
ALTER DATABASE [QLDoanVien] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLDoanVien] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLDoanVien] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLDoanVien] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLDoanVien] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLDoanVien] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLDoanVien', N'ON'
GO
ALTER DATABASE [QLDoanVien] SET QUERY_STORE = OFF
GO
USE [QLDoanVien]
GO
/****** Object:  Table [dbo].[ChiDoan]    Script Date: 15/05/2023 2:20:09 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiDoan](
	[MaChiDoan] [nvarchar](50) NOT NULL,
	[TenChiDoan] [nvarchar](50) NULL,
	[NgayThanhLap] [nvarchar](50) NULL,
	[KhuVuc] [nvarchar](255) NULL,
	[SoDoanVien] [int] NULL,
 CONSTRAINT [PK_ChiDoan] PRIMARY KEY CLUSTERED 
(
	[MaChiDoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoanVien]    Script Date: 15/05/2023 2:20:09 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoanVien](
	[MaDoanVien] [nvarchar](50) NOT NULL,
	[TenDoanVien] [nvarchar](50) NULL,
	[NgaySinh] [nvarchar](50) NULL,
	[NgayVaoDoan] [nvarchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
	[ChiDoan] [nvarchar](50) NULL,
	[Anh] [text] NULL,
 CONSTRAINT [PK_DoanVien] PRIMARY KEY CLUSTERED 
(
	[MaDoanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 15/05/2023 2:20:09 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[TaiKhoan] [nvarchar](50) NOT NULL,
	[MatKhau] [nvarchar](50) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[XepLoai]    Script Date: 15/05/2023 2:20:09 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XepLoai](
	[MaXepLoai] [int] IDENTITY(1,1) NOT NULL,
	[MaDoanVien] [nvarchar](50) NOT NULL,
	[MaChiDoan] [nvarchar](50) NOT NULL,
	[XepLoai] [nvarchar](50) NULL,
	[ThoiGian] [nvarchar](50) NULL,
	[NhanXet] [nvarchar](50) NULL,
 CONSTRAINT [PK_XepLoai_1] PRIMARY KEY CLUSTERED 
(
	[MaXepLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChiDoan] ([MaChiDoan], [TenChiDoan], [NgayThanhLap], [KhuVuc], [SoDoanVien]) VALUES (N'CĐ01', N'Chi Đoàn xã Vân Ngọc', N'08-08-2016', N'Vân Ngọc, Ngọc Hồi, Đống Đa, Hà Nội', 30)
INSERT [dbo].[ChiDoan] ([MaChiDoan], [TenChiDoan], [NgayThanhLap], [KhuVuc], [SoDoanVien]) VALUES (N'CĐ02', N'Chi Đoàn thanh niên xã Thái Hòa', N'05-05-1999', N'Thái Hòa, Vân Thịnh, Hoài Đức, Hà Nội ', 50)
GO
INSERT [dbo].[DoanVien] ([MaDoanVien], [TenDoanVien], [NgaySinh], [NgayVaoDoan], [ChucVu], [ChiDoan], [Anh]) VALUES (N'ÐV01', N'Nguyễn Văn A', N'05-05-2001', N'09-09-2017', N'Đoàn Viên', N'CĐ01', N'user.png')
INSERT [dbo].[DoanVien] ([MaDoanVien], [TenDoanVien], [NgaySinh], [NgayVaoDoan], [ChucVu], [ChiDoan], [Anh]) VALUES (N'ÐV02', N'Nguyễn Văn B', N'20-12-2002', N'09-09-2020', N'Đoàn Trưởng', N'CĐ01', N'user.png')
INSERT [dbo].[DoanVien] ([MaDoanVien], [TenDoanVien], [NgaySinh], [NgayVaoDoan], [ChucVu], [ChiDoan], [Anh]) VALUES (N'ÐV03', N'Chu Minh Nam', N'07-02-1998', N'26-03-2014', N'Đoàn Viên', N'CĐ01', N'user.png')
GO
INSERT [dbo].[TaiKhoan] ([TaiKhoan], [MatKhau]) VALUES (N'admin', N'admin')
GO
SET IDENTITY_INSERT [dbo].[XepLoai] ON 

INSERT [dbo].[XepLoai] ([MaXepLoai], [MaDoanVien], [MaChiDoan], [XepLoai], [ThoiGian], [NhanXet]) VALUES (1, N'ÐV01', N'CĐ01', N'Khá', N'Tháng 1', N'Khá trung bình')
INSERT [dbo].[XepLoai] ([MaXepLoai], [MaDoanVien], [MaChiDoan], [XepLoai], [ThoiGian], [NhanXet]) VALUES (2, N'ÐV01', N'CĐ01', N'Trung Bình', N'Tháng 2', N'Tốt')
SET IDENTITY_INSERT [dbo].[XepLoai] OFF
GO
ALTER TABLE [dbo].[DoanVien] ADD  CONSTRAINT [DF_DoanVien_Anh]  DEFAULT ('user.png') FOR [Anh]
GO
USE [master]
GO
ALTER DATABASE [QLDoanVien] SET  READ_WRITE 
GO
