USE [master]
GO
/****** Object:  Database [EventsDB]    Script Date: 23/08/2020 02:20:25 AM ******/
CREATE DATABASE [EventsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EventCombo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\EventCombo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EventCombo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\EventCombo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EventsDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EventsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EventsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EventsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EventsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EventsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EventsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EventsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EventsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EventsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EventsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EventsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EventsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EventsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EventsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EventsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EventsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EventsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EventsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EventsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EventsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EventsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EventsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EventsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EventsDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EventsDB] SET  MULTI_USER 
GO
ALTER DATABASE [EventsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EventsDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EventsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EventsDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EventsDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EventsDB] SET QUERY_STORE = OFF
GO
USE [EventsDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 23/08/2020 02:20:25 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 23/08/2020 02:20:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Date] [date] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 23/08/2020 02:20:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[EventId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/08/2020 02:20:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](50) NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Event] ON 
GO
INSERT [dbo].[Event] ([Id], [Title], [Date], [UserId], [DateTime], [IsDeleted]) VALUES (1, N'Event 1', CAST(N'2020-08-20' AS Date), 1, CAST(N'2020-08-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Event] ([Id], [Title], [Date], [UserId], [DateTime], [IsDeleted]) VALUES (2, N'Event 2', CAST(N'2020-08-20' AS Date), 1, CAST(N'2020-08-20T18:30:46.750' AS DateTime), NULL)
GO
INSERT [dbo].[Event] ([Id], [Title], [Date], [UserId], [DateTime], [IsDeleted]) VALUES (3, N'Event 3', CAST(N'2020-10-20' AS Date), 1, CAST(N'2020-08-20T18:30:56.207' AS DateTime), 1)
GO
INSERT [dbo].[Event] ([Id], [Title], [Date], [UserId], [DateTime], [IsDeleted]) VALUES (4, N'Event A', CAST(N'2020-10-20' AS Date), 3, CAST(N'2020-08-20T18:31:06.540' AS DateTime), NULL)
GO
INSERT [dbo].[Event] ([Id], [Title], [Date], [UserId], [DateTime], [IsDeleted]) VALUES (5, N'Event B', CAST(N'2020-08-20' AS Date), 3, CAST(N'2020-08-20T18:31:16.210' AS DateTime), NULL)
GO
INSERT [dbo].[Event] ([Id], [Title], [Date], [UserId], [DateTime], [IsDeleted]) VALUES (6, N'Event Test', CAST(N'2020-08-20' AS Date), 1, CAST(N'2020-08-23T01:20:09.990' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Event] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (1, CAST(150.00 AS Decimal(18, 2)), CAST(N'2020-08-21T14:40:24.413' AS DateTime), 1, 3, NULL)
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (2, CAST(25.00 AS Decimal(18, 2)), CAST(N'2020-08-21T14:43:41.243' AS DateTime), 2, 3, NULL)
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (3, CAST(310.00 AS Decimal(18, 2)), CAST(N'2020-08-21T14:43:49.133' AS DateTime), 3, 3, NULL)
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (5, CAST(87.00 AS Decimal(18, 2)), CAST(N'2020-08-21T14:44:14.377' AS DateTime), 3, 4, NULL)
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (7, CAST(95.00 AS Decimal(18, 2)), CAST(N'2020-08-21T14:44:45.907' AS DateTime), 5, 1, NULL)
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (8, CAST(140.00 AS Decimal(18, 2)), CAST(N'2020-08-23T01:25:38.527' AS DateTime), 1, 8, NULL)
GO
INSERT [dbo].[Order] ([Id], [Amount], [DateTime], [EventId], [UserId], [IsDeleted]) VALUES (9, CAST(150.00 AS Decimal(18, 2)), CAST(N'2020-08-23T01:51:19.640' AS DateTime), 1, 7, NULL)
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (1, N'Bilal', N'Rauf', N'brauf7668@gmail.com', N'12345', N'Admin', CAST(N'2020-08-20T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (3, N'Aqiba', N'Kausar', N'brauf76681@gmail.com', N'12345', N'Client', CAST(N'2020-08-20T16:53:57.410' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (4, N'Rafi', N'Khan', N'brauf76@gmail.com', N'12345', N'Client', CAST(N'2020-08-20T17:02:39.560' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (5, N'Kami', N'Khan', N'brau@gmail.com', N'12345', N'Client', CAST(N'2020-08-20T17:03:02.663' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (6, N'Faisal', N'Imran', N'brauf34@gmail.com', N'12345', N'Client', CAST(N'2020-08-20T17:03:15.443' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (7, N'Faisal', N'Kareem', N'brauf24@gmail.com', N'12345', N'Client', CAST(N'2020-08-20T17:03:31.470' AS DateTime))
GO
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [Email], [Password], [Role], [DateTime]) VALUES (8, N'Bilal', N'Rauf', N'brauf766812@gmail.com', N'12345', N'Client', CAST(N'2020-08-23T01:10:55.087' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_User_Event_Date]    Script Date: 23/08/2020 02:20:26 AM ******/
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [UC_User_Event_Date] UNIQUE NONCLUSTERED 
(
	[Title] ASC,
	[UserId] ASC,
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UC_Order_Event_User]    Script Date: 23/08/2020 02:20:26 AM ******/
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [UC_Order_Event_User] UNIQUE NONCLUSTERED 
(
	[EventId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UC_User_Email]    Script Date: 23/08/2020 02:20:26 AM ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UC_User_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Events_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Events_Users]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Orders_Events]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Orders_Users]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Event Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Event', @level2type=N'COLUMN',@level2name=N'Date'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Event Owner' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Event', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Entry Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Event', @level2type=N'COLUMN',@level2name=N'DateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Event Buyer' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Order', @level2type=N'COLUMN',@level2name=N'UserId'
GO
USE [master]
GO
ALTER DATABASE [EventsDB] SET  READ_WRITE 
GO
