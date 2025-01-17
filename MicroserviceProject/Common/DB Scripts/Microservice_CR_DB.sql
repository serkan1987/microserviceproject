USE [master]
GO
/****** Object:  Database [Microservice_CR_DB]    Script Date: 6.10.2021 18:33:14 ******/
CREATE DATABASE [Microservice_CR_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Microservice_CR_DB', FILENAME = N'D:\Data Sources\MSSQL\Microservice_CR_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Microservice_CR_DB_log', FILENAME = N'D:\Data Sources\MSSQL\Microservice_CR_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Microservice_CR_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Microservice_CR_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Microservice_CR_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Microservice_CR_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Microservice_CR_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Microservice_CR_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Microservice_CR_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Microservice_CR_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Microservice_CR_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Microservice_CR_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Microservice_CR_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Microservice_CR_DB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Microservice_CR_DB', N'ON'
GO
ALTER DATABASE [Microservice_CR_DB] SET QUERY_STORE = OFF
GO
USE [Microservice_CR_DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6.10.2021 18:33:15 ******/
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
/****** Object:  Table [dbo].[CR_CUSTOMERS]    Script Date: 6.10.2021 18:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CR_CUSTOMERS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ISLEGAL] [bit] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[MIDDLENAME] [nvarchar](50) NULL,
	[SURNAME] [nvarchar](100) NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_CR_CUSTOMERS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CR_TRANSACTION_ITEMS]    Script Date: 6.10.2021 18:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CR_TRANSACTION_ITEMS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TRANSACTION_IDENTITY] [nvarchar](100) NULL,
	[ROLLBACK_TYPE] [nvarchar](250) NOT NULL,
	[DATA_SET] [nvarchar](50) NULL,
	[NAME] [nvarchar](250) NULL,
	[IDENTITY_] [nvarchar](50) NULL,
	[OLD_VALUE] [nvarchar](250) NULL,
	[NEW_VALUE] [nvarchar](250) NULL,
	[ISROLLEDBACK] [bit] NOT NULL,
	[RollbackEntityId] [int] NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_CR_TRANSACTION_ITEMS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CR_TRANSACTIONS]    Script Date: 6.10.2021 18:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CR_TRANSACTIONS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TRANSACTION_IDENTITY] [nvarchar](100) NULL,
	[TRANSACTION_TYPE] [int] NOT NULL,
	[TRANSACTION_DATE] [datetime] NOT NULL,
	[ISROLLEDBACK] [bit] NOT NULL,
	[DeleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_CR_TRANSACTIONS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210730150505_init', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210730152901_update', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210910172542_update1', N'5.0.8')
GO
/****** Object:  Index [IX_CR_TRANSACTION_ITEMS_RollbackEntityId]    Script Date: 6.10.2021 18:33:15 ******/
CREATE NONCLUSTERED INDEX [IX_CR_TRANSACTION_ITEMS_RollbackEntityId] ON [dbo].[CR_TRANSACTION_ITEMS]
(
	[RollbackEntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CR_TRANSACTION_ITEMS]  WITH CHECK ADD  CONSTRAINT [FK_CR_TRANSACTION_ITEMS_CR_TRANSACTIONS_RollbackEntityId] FOREIGN KEY([RollbackEntityId])
REFERENCES [dbo].[CR_TRANSACTIONS] ([ID])
GO
ALTER TABLE [dbo].[CR_TRANSACTION_ITEMS] CHECK CONSTRAINT [FK_CR_TRANSACTION_ITEMS_CR_TRANSACTIONS_RollbackEntityId]
GO
USE [master]
GO
ALTER DATABASE [Microservice_CR_DB] SET  READ_WRITE 
GO
