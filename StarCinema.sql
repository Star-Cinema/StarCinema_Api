USE [master]
GO
/****** Object:  Database [StarCinema2]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE DATABASE [StarCinema2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'StarCinema2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\StarCinema2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'StarCinema2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\StarCinema2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [StarCinema2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StarCinema2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StarCinema2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StarCinema2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StarCinema2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StarCinema2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StarCinema2] SET ARITHABORT OFF 
GO
ALTER DATABASE [StarCinema2] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [StarCinema2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StarCinema2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StarCinema2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StarCinema2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StarCinema2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StarCinema2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StarCinema2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StarCinema2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StarCinema2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [StarCinema2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StarCinema2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StarCinema2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StarCinema2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StarCinema2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StarCinema2] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [StarCinema2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StarCinema2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [StarCinema2] SET  MULTI_USER 
GO
ALTER DATABASE [StarCinema2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StarCinema2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [StarCinema2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [StarCinema2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [StarCinema2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [StarCinema2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [StarCinema2] SET QUERY_STORE = ON
GO
ALTER DATABASE [StarCinema2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [StarCinema2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingDetails]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NOT NULL,
	[TicketId] [int] NOT NULL,
	[SeatId] [int] NOT NULL,
 CONSTRAINT [PK_BookingDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreateAt] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingsServices]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingsServices](
	[BookingsId] [int] NOT NULL,
	[ServicesId] [int] NOT NULL,
 CONSTRAINT [PK_BookingsServices] PRIMARY KEY CLUSTERED 
(
	[BookingsId] ASC,
	[ServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsTrash] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Films]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Films](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Producer] [nvarchar](255) NOT NULL,
	[Director] [nvarchar](255) NOT NULL,
	[Duration] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](255) NOT NULL,
	[Release] [datetime2](7) NOT NULL,
	[VideoLink] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Films] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilmId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[bookingId] [int] NOT NULL,
	[PriceTicket] [float] NOT NULL,
	[PriceService] [float] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[ModeOfPayment] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FilmId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RoomId] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Seats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [float] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[ScheduleId] [int] NOT NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/1/2023 8:24:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Avatar] [nvarchar](max) NULL,
	[Phone] [nvarchar](12) NULL,
	[Dob] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Token] [nvarchar](512) NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Gender] [bit] NULL,
	[IsEmailVerified] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230531124157_initDb', N'7.0.5')
GO
SET IDENTITY_INSERT [dbo].[BookingDetails] ON 

INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (1, 1, 22, 1)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (2, 2, 22, 1)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (3, 3, 14, 1)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (4, 3, 14, 2)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (5, 4, 14, 4)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (6, 4, 14, 3)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (7, 5, 14, 3)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (8, 5, 14, 4)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (9, 6, 6, 1)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (10, 6, 6, 2)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (11, 7, 6, 1)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (12, 7, 6, 2)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (13, 8, 6, 3)
INSERT [dbo].[BookingDetails] ([Id], [BookingId], [TicketId], [SeatId]) VALUES (14, 8, 6, 4)
SET IDENTITY_INSERT [dbo].[BookingDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (1, 1, CAST(N'2023-05-31T20:14:45.6930538' AS DateTime2), N'Success', 1)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (2, 1, CAST(N'2023-05-31T20:14:56.3440581' AS DateTime2), N'Success', 1)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (3, 8, CAST(N'2023-05-31T21:55:26.0290668' AS DateTime2), N'Success', 0)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (4, 8, CAST(N'2023-05-31T22:25:33.5764459' AS DateTime2), N'Expired', 0)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (5, 8, CAST(N'2023-05-31T22:39:23.3784383' AS DateTime2), N'Success', 0)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (6, 8, CAST(N'2023-05-31T22:50:22.6196366' AS DateTime2), N'Success', 0)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (7, 2, CAST(N'2023-05-31T22:50:26.2703739' AS DateTime2), N'Success', 0)
INSERT [dbo].[Bookings] ([Id], [UserId], [CreateAt], [Status], [IsDelete]) VALUES (8, 9, CAST(N'2023-06-01T01:39:49.8710701' AS DateTime2), N'Success', 0)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
INSERT [dbo].[BookingsServices] ([BookingsId], [ServicesId]) VALUES (8, 1)
INSERT [dbo].[BookingsServices] ([BookingsId], [ServicesId]) VALUES (3, 2)
INSERT [dbo].[BookingsServices] ([BookingsId], [ServicesId]) VALUES (6, 2)
INSERT [dbo].[BookingsServices] ([BookingsId], [ServicesId]) VALUES (4, 3)
INSERT [dbo].[BookingsServices] ([BookingsId], [ServicesId]) VALUES (8, 3)
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [IsTrash]) VALUES (1, N'Hoạt hình 3D', 0)
INSERT [dbo].[Categories] ([Id], [Name], [IsTrash]) VALUES (2, N'Tình cảm', 0)
INSERT [dbo].[Categories] ([Id], [Name], [IsTrash]) VALUES (3, N'Đời sống', 0)
INSERT [dbo].[Categories] ([Id], [Name], [IsTrash]) VALUES (4, N'Hành động', 0)
INSERT [dbo].[Categories] ([Id], [Name], [IsTrash]) VALUES (5, N'Kinh dị', 0)
INSERT [dbo].[Categories] ([Id], [Name], [IsTrash]) VALUES (6, N'ggsdgrs', 0)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Films] ON 

INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (1, N'THE LITTLE MERMAID', N'Walt Disney Pictures', N'Rob Marshall', 135, N'“Nàng Tiên Cá” là câu chuyện được yêu thích về Ariel - một nàng tiên cá trẻ xinh đẹp và mạnh mẽ với khát khao phiêu lưu. Ariel là con gái út của Vua Triton và cũng là người ngang ngạnh nhất, nàng khao khát khám phá về thế giới bên kia đại dương. Trong một lần ghé thăm đất liền, nàng đã phải lòng Hoàng tử Eric bảnh bao. Trong khi tiên cá bị cấm tiếp xúc với con người, Ariel đã làm theo trái tim mình. Nàng đã thỏa thuận với phù thủy biển Ursula hung ác để cơ hội sống cuộc sống trên đất liền. Nhưng cuối cùng việc này lại đe dọa tới mạng sống của Ariel và vương miện của cha nàng. Phim mới The Little Mermaid khởi chiếu 21.04.2023 tại rạp chiếu phim toàn quốc.', N'Mỹ', CAST(N'2023-05-31T00:00:00.0000000' AS DateTime2), N'RxXHUnAi45E', 0, 1)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (2, N'FAST & FURIOUS 10', N'Universal Pictures', N'Louis Leterrier', 141, N'Trong Fast Five (2011), Dom và nhóm của anh đã tiêu diệt trùm ma túy người Brazil Hernan Reyes ở Rio De Janeiro. Điều họ không biết là con trai của Reyes, Dante đã chứng kiến tất cả và dành 12 năm qua để lên một kế hoạch “hoàn hảo” sẽ khiến gia đình Dom phải trả giá đắt. Trải qua nhiều nhiệm vụ khó khăn tưởng chừng như bất khả thi nhưng Dom Toretto và gia đình của anh ấy đều đã vượt qua. Họ đánh bại mọi kẻ thù trên hành trình hơn 20 năm qua. Nhưng giờ đây, Dante được đánh giá là kẻ nguy hiểm nhất mà họ sẽ đối mặt: một mối đe dọa đáng sợ xuất hiện từ bóng tối của quá khứ, một kẻ thù đẫm máu, với quyết tâm phá tan gia đình và phá hủy mọi thứ mà Dom yêu thương mãi mãi. Phim mới Fast & Furious 10 ra mắt tại các rạp chiếu phim từ 19.05.2023.', N'Mỹ', CAST(N'2023-05-31T00:00:00.0000000' AS DateTime2), N'https://youtu.be/jTHpOm6L2FQ', 0, 4)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (3, N'DORAEMON: NOBITA’S SKY UTOPIA 2023', N'Shin-Ei Animation', N'Douyama Takumi', 108, N'Doraemon: Nobita’s Sky Utopia 2023 kể về chuyến phiêu lưu của Doraemon, Nobita và những người bạn thân tới Paradapia - một hòn đảo hình trăng lưỡi liềm lơ lửng trên bầu trời. Ở nơi đó, tất cả đều hoàn hảo… đến mức cậu nhóc Nobita mê ngủ ngày cũng có thể trở thành một thần đồng toán học, một siêu sao thể thao. Cả hội Doraemon cùng sử dụng một món bảo bối độc đáo chưa từng xuất hiện trước đây để đến với vương quốc tuyệt vời này. Cùng với những người bạn ở đây, đặc biệt là chàng robot mèo Sonya, nhóm Doraemon đã có chuyến hành trình tới vương quốc trên mây tuyệt vời… cho đến khi những bí mật đằng sau vùng đất lý tưởng này được hé lộ.  Phim Điện Ảnh Doraemon: Nobita và Vùng Đất Lý Tưởng Trên Bầu Trời ra mắt tại các rạp chiếu phim từ 26.05.2023', N'Nhật Bản', CAST(N'2023-05-31T00:00:00.0000000' AS DateTime2), N'https://youtu.be/bUTfUVLP_Zk', 0, 1)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (4, N'LẬT MẶT 6: TẤM VÉ ĐỊNH MỆNH', N'Unknown', N'Lý Hải', 132, N'Tấm vé có mệnh giá 10.000 đồng và sở hữu những con số "định mệnh": 10, 16, 18, 20, 27, 28 - ngày sinh của hội bạn thân sáu người do Trung Dũng, Quốc Cường, Thanh Thức, Huy Khánh, Hoàng Mèo, Trần Kim Hải đảm nhận. Tuy nhiên, nhân vật do Thanh Thức thủ vai, cũng là người giữ tấm vé trúng giải độc đắc lại không may bị tai nạn và qua đời, từ đây, những người còn lại phải dùng đủ mọi cách để tìm lại tấm vé “đổi đời”. Liệu nhóm bạn có thành công và giải mã được cái chết bị ẩn người người bạn thân? Cùng chờ đón đến 28.04 để biết được câu trả lời nha! Phim mới Lật Mặt 6: Tấm Vé Định Mệnh ra mắt tại các rạp chiếu phim từ 28.04.2023.', N'Việt Nam', CAST(N'2023-05-31T00:00:00.0000000' AS DateTime2), N'https://youtu.be/2EnP2tVC00Q', 0, 3)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (5, N'SPIDER-MAN: ACROSS THE SPIDER-VERSE', N'Sony Pictures', N'Joaquim Dos Santos', 140, N'Miles Morales tái xuất trong phần tiếp theo của bom tấn hoạt hình từng đoạt giải Oscar - Spider-Man: Across the Spider-Verse. Sau khi gặp lại Gwen Stacy, chàng Spider-Man thân thiện đến từ Brooklyn phải du hành qua đa vũ trụ và gặp một nhóm Người Nhện chịu trách nhiệm bảo vệ các thế giới song song. Nhưng khi nhóm siêu anh hùng xung đột về cách xử lý một mối đe dọa mới, Miles buộc phải đọ sức với các Người Nhện khác và phải xác định lại ý nghĩa của việc trở thành một người hùng để có thể cứu những người cậu yêu thương nhất. Phim mới Người Nhện: Du Hành Vũ Trụ Nhện dự kiến khởi chiếu 01.06.2023 tại các rạp chiếu phim toàn quốc.', N'Mỹ', CAST(N'2023-06-05T00:00:00.0000000' AS DateTime2), N'https://youtu.be/HVgwRbQfpCc', 0, 1)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (6, N'ROUND UP: NO WAY OUT', N'BA Entertainment', N'Lee Sang Young', 105, N'Quái vật cơ bắp Seok-do (Ma Dong Seok) dẫn đầu đội hình sự truy lùng đường dây buôn chất cấm của thiếu gia Joo Seong Cheol. Cuộc truy đuổi càng thêm gay cấn khi cú đấm công lý "chú Ma" chạm trán thanh kiếm lừng lẫy chốn giang hồ Nhật Bản. Phim mới Vây Hãm: Ngoài Vòng Pháp Luật khởi chiếu 02.06.2023 tại rạp chiếu phim toàn quốc.', N'Hàn Quốc', CAST(N'2023-06-05T00:00:00.0000000' AS DateTime2), N'https://youtu.be/ze0YBIE0ZkA', 0, 4)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (7, N'KHANZAB : TIẾNG GỌI ÂM BINH', N'PT Umbara Brothers Film', N'Anggy Umbara', 88, N'Chuyện phim theo chân Rahayu - cô gái từng chứng kiến cha mình bị giết hại trong vụ thảm sát Banyuwangi năm 1998. Tại đây, những thầy cúng bị nghi ngờ thực hành ma thuật đen sẽ bị người dân giả dạng ninja để sát hại. Sau sự cố này, Rahayu cùng gia đình quyết định rời khỏi Banyuwangi để chuyển đến quê hương của họ ở Jetis, Yogyakarta. Phim mới Tiếng Gọi Âm Binh khởi chiếu 26.05.2023 tại rạp chiếu phim toàn quốc.', N'Indonesia', CAST(N'2023-06-05T00:00:00.0000000' AS DateTime2), N'https://youtu.be/RSADESwWRyw', 0, 5)
INSERT [dbo].[Films] ([Id], [Name], [Producer], [Director], [Duration], [Description], [Country], [Release], [VideoLink], [IsDelete], [CategoryId]) VALUES (8, N'THE CREEPING : OÁN HỒN', N'Cryptoscope Films', N'Jamie Hooper', 94, N'Trải nghiệm thời thơ ấu đau thương, Anna trở về căn nhà xưa để chăm sóc người bà ốm yếu. Từ đó, những điều kỳ lạ bắt đầu xảy ra và các sự kiện kỳ quái dần xuất hiện cho đến khi Anna phát hiện ra mọi việc có liên quan đến một quá khứ bi thảm đã ám lên các thành viên trong gia đình. Điều gì sẽ xảy ra với Anna khi mọi oán niệm được ẩn giấu phía sau ngôi nhà và người bà kỳ lạ? Phim mới Oán Hồn khởi chiếu 26.05.2023 tại các rạp chiếu phim toàn quốc.', N'Mỹ', CAST(N'2023-06-05T00:00:00.0000000' AS DateTime2), N'https://youtu.be/2EnP2tVC00Q', 0, 5)
SET IDENTITY_INSERT [dbo].[Films] OFF
GO
SET IDENTITY_INSERT [dbo].[Images] ON 

INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (1, 1, N'1', N'https://m.media-amazon.com/images/M/MV5BYTUxYjczMWUtYzlkZC00NTcwLWE3ODQtN2I2YTIxOTU0ZTljXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_FMjpg_UX1000_.jpg')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (2, 2, N'2', N'https://upload.wikimedia.org/wikipedia/vi/2/22/Fast_X_VN_poster.jpg')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (3, 3, N'3', N'https://www.cgv.vn/media/catalog/product/cache/1/image/c5f0a1eff4c394a251036189ccddaacd/m/a/main_poster_-_dmmovie2023.jpg')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (4, 4, N'4', N'https://boxofficevietnam.com/wp-content/uploads/2023/04/63fd8018573c7487393661.jpeg')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (5, 5, N'5', N'https://images2.thanhnien.vn/528068263637045248/2023/5/26/spider-man-across-the-spider-verse-poster-16850724641101103572976.jpg')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (7, 6, N'6', N'https://media-cache.cinematerial.com/p/500x/79oihpxr/the-roundup-no-way-out-vietnamese-movie-poster.jpg?https://media-cache.cinematerial.com/p/500x/79oihpxr/the-roundup-no-way-out-vietnamese-movie-poster.jpg?v=1685329019')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (8, 7, N'7', N'https://www.cgv.vn/media/catalog/product/cache/1/image/c5f0a1eff4c394a251036189ccddaacd/k/z/kz_main-poster_fb.jpg')
INSERT [dbo].[Images] ([Id], [FilmId], [Name], [Path]) VALUES (9, 8, N'8', N'https://cdn.galaxycine.vn/media/2023/4/13/oan-hon-du-kien-khoi-chieu-26-05-2023_1681379006801.jpg')
SET IDENTITY_INSERT [dbo].[Images] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (1, 1, 50000, 0, CAST(N'2023-05-31T20:14:45.6930538' AS DateTime2), N'CASH')
INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (2, 2, 50000, 0, CAST(N'2023-05-31T20:14:56.3440581' AS DateTime2), N'CASH')
INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (3, 3, 100000, 35000, CAST(N'2023-05-31T21:56:54.1028374' AS DateTime2), N'NCB')
INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (4, 5, 100000, 0, CAST(N'2023-05-31T22:40:38.6228246' AS DateTime2), N'NCB')
INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (5, 7, 100000, 0, CAST(N'2023-05-31T22:51:26.8750146' AS DateTime2), N'NCB')
INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (6, 6, 100000, 35000, CAST(N'2023-05-31T22:51:33.9411245' AS DateTime2), N'NCB')
INSERT [dbo].[Payments] ([Id], [bookingId], [PriceTicket], [PriceService], [CreatedDate], [ModeOfPayment]) VALUES (7, 8, 100000, 70000, CAST(N'2023-06-01T01:42:11.5997439' AS DateTime2), N'NCB')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'admin')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'user')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([Id], [Name], [IsDelete]) VALUES (1, N'Room 1', 0)
INSERT [dbo].[Rooms] ([Id], [Name], [IsDelete]) VALUES (2, N'Room 2', 0)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (1, 1, 1, CAST(N'2023-05-31T09:00:00.0000000' AS DateTime2), CAST(N'2023-05-31T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (2, 2, 1, CAST(N'2023-05-31T11:30:00.0000000' AS DateTime2), CAST(N'2023-05-31T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (3, 3, 1, CAST(N'2023-05-31T14:00:00.0000000' AS DateTime2), CAST(N'2023-05-31T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (4, 4, 1, CAST(N'2023-05-31T16:30:00.0000000' AS DateTime2), CAST(N'2023-05-31T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (5, 1, 1, CAST(N'2023-06-01T07:00:00.0000000' AS DateTime2), CAST(N'2023-06-01T08:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (6, 1, 1, CAST(N'2023-06-01T19:00:00.0000000' AS DateTime2), CAST(N'2023-06-01T20:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (7, 1, 1, CAST(N'2023-06-01T21:00:00.0000000' AS DateTime2), CAST(N'2023-06-01T22:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (8, 2, 1, CAST(N'2023-06-02T07:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T08:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (9, 2, 1, CAST(N'2023-06-02T19:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T20:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (10, 2, 1, CAST(N'2023-06-02T21:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T22:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (11, 2, 1, CAST(N'2023-06-01T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-01T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (12, 3, 1, CAST(N'2023-06-01T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-01T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (13, 4, 1, CAST(N'2023-06-01T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-01T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (14, 1, 1, CAST(N'2023-06-02T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (15, 2, 1, CAST(N'2023-06-02T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-02T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (16, 3, 1, CAST(N'2023-06-02T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-02T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (17, 4, 1, CAST(N'2023-06-02T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-02T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (18, 1, 1, CAST(N'2023-06-03T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-03T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (19, 2, 1, CAST(N'2023-06-03T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-03T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (20, 3, 1, CAST(N'2023-06-03T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-03T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (21, 4, 1, CAST(N'2023-06-03T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-03T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (22, 1, 1, CAST(N'2023-06-04T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-04T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (23, 2, 1, CAST(N'2023-06-04T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-04T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (24, 3, 1, CAST(N'2023-06-04T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-04T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (25, 4, 1, CAST(N'2023-06-04T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-04T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (26, 5, 1, CAST(N'2023-06-05T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-05T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (27, 6, 1, CAST(N'2023-06-05T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-05T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (28, 7, 1, CAST(N'2023-06-05T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-05T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (29, 8, 1, CAST(N'2023-06-05T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-05T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (30, 5, 1, CAST(N'2023-06-06T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-06T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (31, 6, 1, CAST(N'2023-06-06T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-06T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (32, 7, 1, CAST(N'2023-06-06T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-06T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (33, 8, 1, CAST(N'2023-06-06T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-06T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (34, 5, 1, CAST(N'2023-06-07T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-07T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (35, 6, 1, CAST(N'2023-06-07T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-07T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (36, 7, 1, CAST(N'2023-06-07T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-07T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (37, 8, 1, CAST(N'2023-06-07T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-07T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (38, 5, 1, CAST(N'2023-06-08T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-08T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (39, 6, 1, CAST(N'2023-06-08T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-08T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (40, 7, 1, CAST(N'2023-06-08T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-08T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (41, 8, 1, CAST(N'2023-06-08T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-08T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (42, 5, 1, CAST(N'2023-06-09T09:00:00.0000000' AS DateTime2), CAST(N'2023-06-09T11:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (43, 6, 1, CAST(N'2023-06-09T11:30:00.0000000' AS DateTime2), CAST(N'2023-06-09T13:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (44, 7, 1, CAST(N'2023-06-09T14:00:00.0000000' AS DateTime2), CAST(N'2023-06-09T16:00:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (45, 8, 1, CAST(N'2023-06-09T16:30:00.0000000' AS DateTime2), CAST(N'2023-06-09T18:30:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (46, 6, 1, CAST(N'2023-06-07T07:00:00.0000000' AS DateTime2), CAST(N'2023-06-07T08:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (47, 6, 1, CAST(N'2023-06-07T19:00:00.0000000' AS DateTime2), CAST(N'2023-06-07T20:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (48, 6, 1, CAST(N'2023-06-07T21:00:00.0000000' AS DateTime2), CAST(N'2023-06-07T22:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (49, 8, 1, CAST(N'2023-06-08T07:00:00.0000000' AS DateTime2), CAST(N'2023-06-08T08:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (50, 8, 1, CAST(N'2023-06-08T19:00:00.0000000' AS DateTime2), CAST(N'2023-06-08T20:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (51, 8, 1, CAST(N'2023-06-08T21:00:00.0000000' AS DateTime2), CAST(N'2023-06-08T22:45:00.0000000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (52, 1, 1, CAST(N'2023-06-01T03:03:00.7080000' AS DateTime2), CAST(N'2023-06-01T05:18:00.7080000' AS DateTime2))
INSERT [dbo].[Schedules] ([Id], [FilmId], [RoomId], [StartTime], [EndTime]) VALUES (54, 1, 2, CAST(N'2023-06-01T08:17:05.7760000' AS DateTime2), CAST(N'2023-06-01T10:32:05.7760000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[Seats] ON 

INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (1, N'A1', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (2, N'A2', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (3, N'A3', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (4, N'A4', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (5, N'A5', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (6, N'A6', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (7, N'A7', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (8, N'A8', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (9, N'A9', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (10, N'B1', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (11, N'B2', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (12, N'B3', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (13, N'B4', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (14, N'B5', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (15, N'B6', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (16, N'B7', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (17, N'B8', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (18, N'B9', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (19, N'E1', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (20, N'E2', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (21, N'E3', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (22, N'E4', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (23, N'E5', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (24, N'E6', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (25, N'E7', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (26, N'E8', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (27, N'E9', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (28, N'F1', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (29, N'F2', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (30, N'F3', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (31, N'F4', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (32, N'F5', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (33, N'F6', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (34, N'F7', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (35, N'F8', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (36, N'F9', 1, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (37, N'A1', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (38, N'A2', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (39, N'A3', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (40, N'A4', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (41, N'A5', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (42, N'A6', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (43, N'A7', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (44, N'A8', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (45, N'A9', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (46, N'B1', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (47, N'B2', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (48, N'B3', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (49, N'B4', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (50, N'B5', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (51, N'B6', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (52, N'B7', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (53, N'B8', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (54, N'B9', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (55, N'E1', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (56, N'E2', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (57, N'E3', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (58, N'E4', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (59, N'E5', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (60, N'E6', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (61, N'E7', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (62, N'E8', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (63, N'E9', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (64, N'F1', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (65, N'F2', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (66, N'F3', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (67, N'F4', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (68, N'F5', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (69, N'F6', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (70, N'F7', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (71, N'F8', 2, 0)
INSERT [dbo].[Seats] ([Id], [Name], [RoomId], [IsDelete]) VALUES (72, N'F9', 2, 0)
SET IDENTITY_INSERT [dbo].[Seats] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Name], [Price]) VALUES (1, N'Bắp', 35000)
INSERT [dbo].[Services] ([Id], [Name], [Price]) VALUES (2, N'Nước ngọt', 35000)
INSERT [dbo].[Services] ([Id], [Name], [Price]) VALUES (3, N'Combo Bắp nước', 35000)
INSERT [dbo].[Services] ([Id], [Name], [Price]) VALUES (4, N'test', 232323)
INSERT [dbo].[Services] ([Id], [Name], [Price]) VALUES (5, N'Nguyen Trong Anh', 3434)
INSERT [dbo].[Services] ([Id], [Name], [Price]) VALUES (6, N'Nguyen Trong Anh', 2323)
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (1, 50000, 1)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (2, 50000, 2)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (3, 50000, 3)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (4, 50000, 4)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (5, 50000, 5)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (6, 50000, 6)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (7, 50000, 7)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (8, 50000, 8)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (9, 50000, 9)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (10, 50000, 10)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (11, 50000, 11)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (12, 50000, 12)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (13, 50000, 13)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (14, 50000, 14)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (15, 50000, 15)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (16, 50000, 16)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (17, 50000, 17)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (18, 50000, 18)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (19, 50000, 19)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (20, 50000, 20)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (21, 50000, 21)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (22, 50000, 22)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (23, 50000, 23)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (24, 50000, 24)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (25, 50000, 25)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (26, 50000, 26)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (27, 50000, 27)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (28, 50000, 28)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (29, 50000, 29)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (30, 50000, 30)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (31, 50000, 31)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (32, 50000, 32)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (33, 50000, 33)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (34, 50000, 34)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (35, 50000, 35)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (36, 50000, 36)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (37, 50000, 37)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (38, 50000, 38)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (39, 50000, 39)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (40, 50000, 40)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (41, 50000, 41)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (42, 50000, 42)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (43, 50000, 43)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (44, 50000, 44)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (45, 50000, 45)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (46, 50000, 46)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (47, 50000, 47)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (48, 50000, 48)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (49, 50000, 49)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (50, 50000, 50)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (51, 50000, 51)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (52, 10000, 52)
INSERT [dbo].[Tickets] ([Id], [Price], [ScheduleId]) VALUES (54, 100000, 54)
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (1, N'admin@gmail.com', N'Admin', NULL, N'0935667334', CAST(N'1998-06-01T19:00:00.0000000' AS DateTime2), 0, 1, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 1)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (2, N'user01@gmail.com', N'Nguyen Van A01', NULL, N'0935699934', CAST(N'2001-06-01T19:00:00.0000000' AS DateTime2), 0, 2, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (3, N'user02@gmail.com', N'Tran Huu An02', NULL, N'0935699934', CAST(N'2001-06-01T19:00:00.0000000' AS DateTime2), 0, 2, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (4, N'user03@gmail.com', N'Nguyen Van B03', NULL, N'0935699934', CAST(N'2001-06-01T19:00:00.0000000' AS DateTime2), 0, 2, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (5, N'user04@gmail.com', N'Le Thi Hoa04', NULL, N'0935699934', CAST(N'2001-06-01T19:00:00.0000000' AS DateTime2), 0, 2, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (6, N'user05@gmail.com', N'Nguyen Van Tuan05', NULL, N'0935699934', CAST(N'2001-06-01T19:00:00.0000000' AS DateTime2), 0, 2, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (7, N'hungklyhongkl@gmail.com', N'Trần Văn Hùng', NULL, N'0935699934', CAST(N'2001-06-01T19:00:00.0000000' AS DateTime2), 0, 2, NULL, 0xE6B2726360D763A90824FAC9EDAFF3112BBDD4091F6ACAEB9F422A94E2732A853C758D11684C33CE527AC68D80CB85C7DCEECB7AF7B5BB26BC153CED92E1B14D, 0x233C45EA5E46538DE6495614E3D4D68DE51E457CD2919E15412B4638FE4AA5ADA6E7F26B6946A8F8A2ACBEFD989BC52620F11A416AF198E206E31052CFDED51AC01CB9ECFBD0A95D4833B62F7E90037672B8C3F97E50162E76FCFD5F9B9D0FA50C3C0808FCDF0DB43D987C6E984700BBD9D16935AA191373468FF264D1C3DDB6, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (8, N'anhn32616@gmail.com', N'Nguyễn Trọng Anh', N'http://res.cloudinary.com/dsirezdju/image/upload/v1685544270/anhlua_mrclnr.jpg', N'0329136011', CAST(N'2023-05-28T17:00:00.0000000' AS DateTime2), 0, 2, N'021752', 0x98F4B74C77539376CE816E8D7398D4D45D549A1C1FF55634D48AD4752D4F499325D47BD8FE7E7869DA1FD416B18D4080B690129D73609AC8EEE2C6E5EF539596, 0xE9AEEF25FFD6B8AA726AB54654533EC73E0BEDC7863CBA76C3C0B5A0CDB4BAD2F7CEF508249954EC8D7B1E917CBB13F866E51031C59EFF18A107F3A3A3326C957D185516381E477981194C34BAECCB8667E71D7E3868CBFB3022763A459F608611377240B1FF45F0E625277FFDC76B94D07A4CFBE859D5DEC4406A2963276C05, 1, 0)
INSERT [dbo].[User] ([Id], [Email], [Name], [Avatar], [Phone], [Dob], [IsDelete], [RoleId], [Token], [PasswordHash], [PasswordSalt], [Gender], [IsEmailVerified]) VALUES (9, N'nguyentronganh53@gmail.com', N'Nguyễn Trọng Anh', N'http://res.cloudinary.com/dsirezdju/image/upload/v1685557821/anhlua_xuktiz.jpg', N'0896918934', CAST(N'2106-02-06T18:29:54.9300000' AS DateTime2), 0, 2, N'819849', 0x4952BBCE866F64271030D6F59AB8464A79A668E7AE27357AD9307BE33C5429BA920D7C7DB9305DA02C609DE74BC93BF87CF17B4FB20D58A2DCAD5A3F994DC962, 0x117CC2A05BDBA55F14DE0E8AA767911FD5EC180266C3D571A8E74127FB21CAEA87CB5A271FEA71C39A2EF59CF4B37FCCEA25A8A5318637B760802B120069DB3533A3824EF40A39BE540D9F3F6A4C59D3D2A2B0EBE1946E80D1003441C2BC2EB5B0B493A2C3CA174B89A3F90955FB3FC2A9BA7D40A846F3D6DF25ABE5616F2D0A, 1, 1)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
/****** Object:  Index [IX_BookingDetails_BookingId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_BookingDetails_BookingId] ON [dbo].[BookingDetails]
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookingDetails_SeatId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_BookingDetails_SeatId] ON [dbo].[BookingDetails]
(
	[SeatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookingDetails_TicketId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_BookingDetails_TicketId] ON [dbo].[BookingDetails]
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookings_UserId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Bookings_UserId] ON [dbo].[Bookings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BookingsServices_ServicesId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_BookingsServices_ServicesId] ON [dbo].[BookingsServices]
(
	[ServicesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Films_CategoryId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Films_CategoryId] ON [dbo].[Films]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Images_FilmId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Images_FilmId] ON [dbo].[Images]
(
	[FilmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Payments_bookingId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Payments_bookingId] ON [dbo].[Payments]
(
	[bookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedules_FilmId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_FilmId] ON [dbo].[Schedules]
(
	[FilmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Schedules_RoomId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Schedules_RoomId] ON [dbo].[Schedules]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Seats_RoomId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_Seats_RoomId] ON [dbo].[Seats]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tickets_ScheduleId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Tickets_ScheduleId] ON [dbo].[Tickets]
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_User_RoleId]    Script Date: 6/1/2023 8:24:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_RoleId] ON [dbo].[User]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetails_Bookings_BookingId] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Bookings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetails_Bookings_BookingId]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetails_Seats_SeatId] FOREIGN KEY([SeatId])
REFERENCES [dbo].[Seats] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetails_Seats_SeatId]
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD  CONSTRAINT [FK_BookingDetails_Tickets_TicketId] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([Id])
GO
ALTER TABLE [dbo].[BookingDetails] CHECK CONSTRAINT [FK_BookingDetails_Tickets_TicketId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_User_UserId]
GO
ALTER TABLE [dbo].[BookingsServices]  WITH CHECK ADD  CONSTRAINT [FK_BookingsServices_Bookings_BookingsId] FOREIGN KEY([BookingsId])
REFERENCES [dbo].[Bookings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingsServices] CHECK CONSTRAINT [FK_BookingsServices_Bookings_BookingsId]
GO
ALTER TABLE [dbo].[BookingsServices]  WITH CHECK ADD  CONSTRAINT [FK_BookingsServices_Services_ServicesId] FOREIGN KEY([ServicesId])
REFERENCES [dbo].[Services] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BookingsServices] CHECK CONSTRAINT [FK_BookingsServices_Services_ServicesId]
GO
ALTER TABLE [dbo].[Films]  WITH CHECK ADD  CONSTRAINT [FK_Films_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Films] CHECK CONSTRAINT [FK_Films_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_Films_FilmId] FOREIGN KEY([FilmId])
REFERENCES [dbo].[Films] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_Films_FilmId]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Bookings_bookingId] FOREIGN KEY([bookingId])
REFERENCES [dbo].[Bookings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Bookings_bookingId]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Films_FilmId] FOREIGN KEY([FilmId])
REFERENCES [dbo].[Films] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Films_FilmId]
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD  CONSTRAINT [FK_Schedules_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Schedules] CHECK CONSTRAINT [FK_Schedules_Rooms_RoomId]
GO
ALTER TABLE [dbo].[Seats]  WITH CHECK ADD  CONSTRAINT [FK_Seats_Rooms_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Seats] CHECK CONSTRAINT [FK_Seats_Rooms_RoomId]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Schedules_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Schedules_ScheduleId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Roles_RoleId]
GO
USE [master]
GO
ALTER DATABASE [StarCinema2] SET  READ_WRITE 
GO
