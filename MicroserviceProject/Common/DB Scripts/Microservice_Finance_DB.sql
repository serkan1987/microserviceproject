USE [master]
GO
/****** Object:  Database [Microservice_Finance_DB]    Script Date: 16.11.2021 18:31:45 ******/
CREATE DATABASE [Microservice_Finance_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Microservice_Finance_DB', FILENAME = N'C:\Data Sources\MSSQL\Microservice_Finance_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Microservice_Finance_DB_log', FILENAME = N'C:\Data Sources\MSSQL\Microservice_Finance_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Microservice_Finance_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Microservice_Finance_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Microservice_Finance_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Microservice_Finance_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Microservice_Finance_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Microservice_Finance_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Microservice_Finance_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Microservice_Finance_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Microservice_Finance_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Microservice_Finance_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Microservice_Finance_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Microservice_Finance_DB', N'ON'
GO
USE [Microservice_Finance_DB]
GO
/****** Object:  Table [dbo].[FINANCE_DECIDED_COSTS]    Script Date: 16.11.2021 18:31:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FINANCE_DECIDED_COSTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BUYING_INVENTORY_REQUESTS_ID] [int] NULL,
	[APPROVED] [bit] NULL,
	[DONE] [bit] NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_FINANCE_DECIDED_COSTS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FINANCE_PRODUCTIONREQUESTS]    Script Date: 16.11.2021 18:31:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FINANCE_PRODUCTIONREQUESTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PRODUCTION_PRODUCTS_ID] [int] NOT NULL,
	[AMOUNT] [int] NOT NULL,
	[HR_DEPARTMENTS_ID] [int] NOT NULL,
	[REFERENCE_NUMBER] [int] NOT NULL,
	[APPROVED] [bit] NULL,
	[DONE] [bit] NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_FINANCE_PRODUCTIONREQUESTS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FINANCE_TRANSACTIONS]    Script Date: 16.11.2021 18:31:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FINANCE_TRANSACTIONS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TRANSACTION_IDENTITY] [nvarchar](50) NULL,
	[TRANSACTION_TYPE] [int] NULL,
	[TRANSACTION_DATE] [datetime] NULL,
	[IS_ROLLED_BACK] [bit] NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_FINANCE_TRANSACTIONS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FINANCE_TRANSACTIONS_ITEMS]    Script Date: 16.11.2021 18:31:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FINANCE_TRANSACTIONS_ITEMS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ROLLBACK_TYPE] [int] NULL,
	[DATASET] [nvarchar](50) NULL,
	[IDENTITY] [nvarchar](50) NULL,
	[OLD_VALUE] [nvarchar](50) NULL,
	[NEW_VALUE] [nvarchar](50) NULL,
	[IS_ROLLED_BACK] [bit] NULL,
	[DELETE_DATE] [datetime] NULL,
	[NAME] [nvarchar](50) NULL,
 CONSTRAINT [PK_FINANCE_TRANSACTIONS_ITEMS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Microservice_Finance_DB] SET  READ_WRITE 
GO
