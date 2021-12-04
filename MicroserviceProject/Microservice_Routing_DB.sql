USE [master]
GO
/****** Object:  Database [Microservice_Routing_DB]    Script Date: 28.11.2021 12:15:55 ******/
CREATE DATABASE [Microservice_Routing_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Microservice_Routing_DB', FILENAME = N'C:\Data Sources\MSSQL\Microservice_Routing_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Microservice_Routing_DB_log', FILENAME = N'C:\Data Sources\MSSQL\Microservice_Routing_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Microservice_Routing_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Microservice_Routing_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Microservice_Routing_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Microservice_Routing_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Microservice_Routing_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Microservice_Routing_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Microservice_Routing_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Microservice_Routing_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Microservice_Routing_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Microservice_Routing_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Microservice_Routing_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Microservice_Routing_DB', N'ON'
GO
USE [Microservice_Routing_DB]
GO
/****** Object:  Table [dbo].[HOSTS]    Script Date: 28.11.2021 12:15:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOSTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](250) NULL,
	[HOST] [nvarchar](1000) NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_HOSTS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SERVICE_ROUTES]    Script Date: 28.11.2021 12:15:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICE_ROUTES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](250) NULL,
	[CALLTYPE] [nvarchar](50) NULL,
	[ENDPOINT] [nvarchar](500) NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_SERVICE_ROUTES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SERVICE_ROUTES_ALTERNATIVES]    Script Date: 28.11.2021 12:15:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICE_ROUTES_ALTERNATIVES](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SERVICE_ROUTES_ID] [int] NULL,
	[ALTERNATIVE_SERVICE_ROUTE_ID] [int] NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_SERVICE_ROUTES_ALTERNATIVES] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SERVICE_ROUTES_QUERYKEYS]    Script Date: 28.11.2021 12:15:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SERVICE_ROUTES_QUERYKEYS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SERVICE_ROUTE_ID] [int] NULL,
	[KEY] [nvarchar](50) NULL,
	[DELETE_DATE] [datetime] NULL,
 CONSTRAINT [PK_SERVICE_ROUTE_QUERYKEYS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HOSTS] ON 

INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (1, N'Services.Api.Infrastructure.Authorization', N'http://localhost:16859', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (2, N'Services.Api.Infrastructure.Authorization', N'http://localhost/MicroserviceProject.Services.Api.Authorization', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (3, N'Services.Api.Business.Departments.HR.Departments', N'http://localhost:26920', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (4, N'Services.Api.Business.Departments.HR.Departments', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (5, N'Services.Api.Business.Departments.Accounting', N'http://localhost:30775', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (6, N'Services.Api.Business.Departments.Accounting', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (7, N'Services.Api.Business.Departments.IT', N'http://localhost:65390', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (8, N'Services.Api.Business.Departments.IT', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (9, N'Services.Api.Business.Departments.AA', N'http://localhost:34308', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (10, N'Services.Api.Business.Departments.AA', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (11, N'Services.Api.Business.Departments.Buying', N'http://localhost:26558', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (12, N'Services.Api.Business.Departments.Buying', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Buying', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (13, N'Services.Api.Business.Departments.Finance', N'http://localhost:32355', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (14, N'Services.Api.Business.Departments.Finance', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (15, N'Services.Api.Business.Departments.CR', N'http://localhost:60403', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (16, N'Services.Api.Business.Departments.CR', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.CR', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (17, N'Services.Api.Business.Departments.Selling', N'http://localhost:5139', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (18, N'Services.Api.Business.Departments.Selling', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Selling', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (19, N'Services.Api.Business.Departments.Production', N'http://localhost:9311', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (20, N'Services.Api.Business.Departments.Production', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Production', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (21, N'Services.Api.Business.Departments.Storage', N'http://localhost:58984', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (22, N'Services.Api.Business.Departments.Storage', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Storage', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (23, N'Services.WebSockets.Security', N'http://localhost:43449', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (24, N'Services.WebSockets.Reliability', N'http://localhost:23681', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (25, N'Services.Api.Gateway.Public', N'http://localhost:20200', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (26, N'Presentation.UI.Web', N'http://localhost:8501', NULL)
INSERT [dbo].[HOSTS] ([ID], [NAME], [HOST], [DELETE_DATE]) VALUES (27, N'Presentation.UI.Web.Identity', N'http://localhost:32636', NULL)
SET IDENTITY_INSERT [dbo].[HOSTS] OFF
GO
SET IDENTITY_INSERT [dbo].[SERVICE_ROUTES] ON 

INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (3, N'authorization.auth.getuser', N'GET', N'http://localhost:16859/Auth/GetUser', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (4, N'authorization.auth.gettoken', N'POST', N'http://localhost:16859/Auth/GetToken', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1002, N'authorization.alternative.auth.getuser', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Authorization/Auth/GetUser', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1003, N'authorization.alternative.auth.gettoken', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Authorization/Auth/GetToken', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1004, N'hr.department.getdepartments', N'GET', N'http://localhost:26920/Department/GetDepartments', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1005, N'hr.department.createdepartment', N'POST', N'http://localhost:26920/Department/CreateDepartment', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1006, N'hr.person.getpeople', N'GET', N'http://localhost:26920/Person/GetPeople', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1007, N'hr.person.createperson', N'POST', N'http://localhost:26920/Person/CreatePerson', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1008, N'hr.person.gettitles', N'GET', N'http://localhost:26920/Person/GetTitles', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1009, N'hr.person.createtitle', N'POST', N'http://localhost:26920/Person/CreateTitle', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1010, N'hr.person.getworkers', N'GET', N'http://localhost:26920/Person/GetWorkers', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1011, N'hr.person.createworker', N'POST', N'http://localhost:26920/Person/CreateWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1012, N'accounting.bankaccounts.createbankaccount', N'POST', N'http://localhost:30775/BankAccounts/CreateBankAccount', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1014, N'accounting.bankaccounts.getbankaccountsofworker', N'GET', N'http://localhost:30775/BankAccounts/GetBankAccountsOfWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1015, N'accounting.bankaccounts.getcurrencies', N'GET', N'http://localhost:30775/BankAccounts/GetCurrencies', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1016, N'accounting.bankaccounts.createcurrency', N'POST', N'http://localhost:30775/BankAccounts/CreateCurrency', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1017, N'it.inventory.getinventories', N'GET', N'http://localhost:65390/Inventory/GetInventories', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1018, N'it.inventory.createinventory', N'POST', N'http://localhost:65390/Inventory/CreateInventory', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1019, N'it.inventory.assigninventorytoworker', N'POST', N'http://localhost:65390/Inventory/AssignInventoryToWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1020, N'aa.inventory.getinventories', N'GET', N'http://localhost:34308/Inventory/GetInventories', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1021, N'aa.inventory.createinventory', N'POST', N'http://localhost:34308/Inventory/CreateInventory', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1022, N'aa.inventory.assigninventorytoworker', N'POST', N'http://localhost:34308/Inventory/AssignInventoryToWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1023, N'aa.inventory.getinventoriesfornewworker', N'GET', N'http://localhost:34308/Inventory/GetInventoriesForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1024, N'it.inventory.getinventoriesfornewworker', N'GET', N'http://localhost:65390/Inventory/GetInventoriesForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1025, N'accounting.alternative.bankaccounts.createbankaccount', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/BankAccounts/CreateBankAccount', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1026, N'accounting.alternative.bankaccounts.getbankaccountsofworker', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/BankAccounts/GetBankAccountsOfWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1027, N'accounting.alternative.bankaccounts.getcurrencies', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/BankAccounts/GetCurrencies', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1028, N'accounting.alternative.bankaccounts.createcurrency', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/BankAccounts/CreateCurrency', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1029, N'aa.alternative.inventory.getinventories', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Inventory/GetInventories', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1030, N'aa.alternative.inventory.createinventory', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Inventory/CreateInventory', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1031, N'aa.alternative.inventory.assigninventorytoworker', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Inventory/AssignInventoryToWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1032, N'aa.alternative.inventory.getinventoriesfornewworker', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Inventory/GetInventoriesForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1033, N'hr.alternative.department.getdepartments', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Department/GetDepartments', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1034, N'hr.alternative.department.createdepartment', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Department/CreateDepartment', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1035, N'hr.alternative.person.getpeople', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Person/GetPeople', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1036, N'hr.alternative.person.createperson', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Person/CreatePerson', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1037, N'hr.alternative.person.gettitles', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Person/GetTitles', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1038, N'hr.alternative.person.createtitle', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Person/CreateTitle', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1039, N'hr.alternative.person.getworkers', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Person/GetWorkers', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1040, N'hr.alternative.person.createworker', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Person/CreateWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1041, N'it.alternative.inventory.getinventories', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Inventory/GetInventories', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1042, N'it.alternative.inventory.createinventory', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Inventory/CreateInventory', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1043, N'it.alternative.inventory.assigninventorytoworker', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Inventory/GetInventoriesForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1044, N'it.alternative.inventory.getinventoriesfornewworker', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Inventory/GetInventoriesForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1045, N'it.inventory.createdefaultinventoryfornewworker', N'POST', N'http://localhost:65390/Inventory/CreateDefaultInventoryForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1046, N'it.alternative.inventory.createdefaultinventoryfornewworker', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Inventory/CreateDefaultInventoryForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1047, N'aa.inventory.createdefaultinventoryfornewworker', N'POST', N'http://localhost:34308/Inventory/CreateDefaultInventoryForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1048, N'aa.alternative.inventory.createdefaultinventoryfornewworker', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Inventory/CreateDefaultInventoryForNewWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1049, N'hr.transaction.rollbacktransaction', N'POST', N'http://localhost:26920/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1050, N'hr.alternative.transaction.rollbacktransaction', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1051, N'accounting.transaction.rollbacktransaction', N'POST', N'http://localhost:30775/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1052, N'accounting.alternative.transaction.rollbacktransaction', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1053, N'it.transaction.rollbacktransaction', N'POST', N'http://localhost:65390/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1054, N'it.alternative.transaction.rollbacktransaction', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1055, N'aa.transaction.rollbacktransaction', N'POST', N'http://localhost:34308/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (1056, N'aa.alternative.transaction.rollbacktransaction', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2045, N'buying.request.createinventoryrequest', N'POST', N'http://localhost:26558/Request/CreateInventoryRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2046, N'buying.request.getinventoryrequests', N'GET', N'http://localhost:26558/Request/GetInventoryRequests', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2047, N'buying.transaction.rollbacktransaction', N'POST', N'http://localhost:26558/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2048, N'buying.alternative.request.createinventoryrequest', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Buying/Request/CreateInventoryRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2049, N'buying.alternative.request.getinventoryrequests', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Buying/Request/GetInventoryRequests', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2050, N'buying.alternative.transaction.rollbacktransaction', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Buying/Transaction/RollbackTransaction', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2051, N'finance.cost.createcost', N'POST', N'http://localhost:32355/Cost/CreateCost', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2052, N'finance.cost.getdecidedcosts', N'GET', N'http://localhost:32355/Cost/GetDecidedCosts', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2053, N'finance.cost.decidecost', N'POST', N'http://localhost:32355/Cost/DecideCost', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2054, N'buying.request.validatecostinventory', N'POST', N'http://localhost:26558/Request/ValidateCostInventory', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2055, N'aa.inventory.informinventoryrequest', N'POST', N'http://localhost:34308/Inventory/InformInventoryRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2056, N'it.inventory.informinventoryrequest', N'POST', N'http://localhost:65390/Inventory/InformInventoryRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2057, N'finance.alternative.cost.createcost', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/Cost/CreateCost', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2058, N'finance.alternative.cost.getdecidedcosts', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/Cost/GetDecidedCosts', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2059, N'finance.alternative.cost.decidecost', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/Cost/DecideCost', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2060, N'buying.alternative.request.validatecostinventory', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Buying/Request/ValidateCostInventory', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2061, N'aa.alternative.inventory.informinventoryrequest', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Inventory/InformInventoryRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2062, N'it.alternative.inventory.informinventoryrequest', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Inventory/InformInventoryRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2063, N'logging.logging.writerequestresponselog', N'POST', N'http://localhost:15455/Logging/WriteRequestResponseLog', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2065, N'websockets.security.sendtokennotification', N'POST', N'http://localhost:43449/SendTokenNotification', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2066, N'websockets.reliability.senderrornotification', N'POST', N'http://localhost:23681/SendErrorNotification', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2067, N'accounting.bankaccounts.getsalarypaymentsofworker', N'GET', N'http://localhost:30775/BankAccounts/GetSalaryPaymentsOfWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2068, N'accounting.alternative.bankaccounts.getsalarypaymentsofworker', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/BankAccounts/GetSalaryPaymentsOfWorker', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2069, N'accounting.bankaccounts.createsalarypayment', N'POST', N'http://localhost:30775/BankAccounts/CreateSalaryPayment', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2070, N'accounting.alternative.bankaccounts.createsalarypayment', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/BankAccounts/CreateSalaryPayment', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2071, N'cr.customers.getcustomers', N'GET', N'http://localhost:60403/Customers/GetCustomers', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2072, N'cr.customers.createcustomer', N'POST', N'http://localhost:60403/Customers/CreateCustomer', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2073, N'cr.alternative.customers.getcustomers', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.CR/Customers/GetCustomers', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2074, N'cr.alternative.customers.createcustomer', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.CR/Customers/CreateCustomer', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2075, N'selling.selling.getsolds', N'GET', N'http://localhost:5139/Selling/GetSolds', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2076, N'selling.selling.createselling', N'POST', N'http://localhost:5139/Selling/CreateSelling', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2077, N'selling.alternative.selling.getsolds', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Selling/Selling/GetSolds', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2078, N'selling.alternative.selling.createselling', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Selling/Selling/CreateSelling', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2079, N'production.product.getproducts', N'GET', N'http://localhost:9311/Product/GetProducts', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2080, N'production.product.createproduct', N'POST', N'http://localhost:9311/Product/CreateProduct', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2081, N'production.alternative.product.getproducts', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Production/Product/GetProducts', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2082, N'production.alternative.product.createproduct', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Production/Product/CreateProduct', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2083, N'storage.stock.getstock', N'GET', N'http://localhost:58984/Stock/GetStock', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2084, N'storage.stock.createstock', N'POST', N'http://localhost:58984/Stock/CreateStock', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2085, N'storage.alternative.stock.getstock', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Storage/Stock/GetStock', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2086, N'storage.alternative.stock.createstock', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Storage/Stock/CreateStock', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2087, N'production.production.produceproduct', N'POST', N'http://localhost:9311/Production/ProduceProduct', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2088, N'production.alternative.production.produceproduct', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Production/Production/ProduceProduct', NULL)
GO
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2089, N'storage.stock.descendproductstock', N'POST', N'http://localhost:58984/Stock/DescendProductStock', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2090, N'storage.alternative.stock.descendproductstock', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Storage/Stock/DescendProductStock', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2091, N'production.production.reevaluateproduceproduct', N'GET', N'http://localhost:9311/Production/ReEvaluateProduceProduct', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2092, N'production.alternative.production.reevaluateproduceproduct', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Production/Production/ReEvaluateProduceProduct', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2093, N'finance.productionrequest.getproductionrequests', N'GET', N'http://localhost:32355/ProductionRequest/GetProductionRequests', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2094, N'finance.alternative.productionrequest.getproductionrequests', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/ProductionRequest/GetProductionRequests', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2095, N'finance.productionrequest.createproductionrequest', N'POST', N'http://localhost:32355/ProductionRequest/CreateProductionRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2096, N'finance.alternative.productionrequest.createproductionrequest', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/ProductionRequest/CreateProductionRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2097, N'finance.productionrequest.decideproductionrequest', N'POST', N'http://localhost:32355/ProductionRequest/DecideProductionRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2098, N'finance.alternative.productionrequest.decideproductionrequest', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/ProductionRequest/DecideProductionRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2099, N'selling.selling.notifyproductionrequest', N'POST', N'http://localhost:5139/Selling/NotifyProductionRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2100, N'selling.alternative.selling.notifyproductionrequest', N'POST', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Selling/Selling/NotifyProductionRequest', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2101, N'presentation.ui.web.identity.user.login', N'GET', N'http://localhost:31636/Login', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2102, N'gateway.public.hr.getdepartments', N'GET', N'http://localhost:20200/HR/GetDepartments', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2103, N'accounting.identity.removesessionifexistsincache', N'GET', N'http://localhost:30775/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2104, N'accounting.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Accounting/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2105, N'hr.identity.removesessionifexistsincache', N'GET', N'http://localhost:26920/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2106, N'hr.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.HR/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2107, N'it.identity.removesessionifexistsincache', N'GET', N'http://localhost:65390/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2108, N'it.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.IT/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2109, N'aa.identity.removesessionifexistsincache', N'GET', N'http://localhost:34308/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2110, N'aa.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.AA/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2111, N'buying.identity.removesessionifexistsincache', N'GET', N'http://localhost:26558/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2112, N'buying.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Buying/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2113, N'finance.identity.removesessionifexistsincache', N'GET', N'http://localhost:32355/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2114, N'finance.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Finance/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2115, N'cr.identity.removesessionifexistsincache', N'GET', N'http://localhost:60403/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2116, N'cr.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.CR/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2117, N'selling.identity.removesessionifexistsincache', N'GET', N'http://localhost:5139/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2118, N'selling.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Selling/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2119, N'production.identity.removesessionifexistsincache', N'GET', N'http://localhost:9311/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2120, N'production.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Production/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2121, N'storage.identity.removesessionifexistsincache', N'GET', N'http://localhost:58984/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2122, N'storage.alternative.identity.removesessionifexistsincache', N'GET', N'http://localhost/MicroserviceProject.Services.Api.Business.Departments.Storage/Identity/RemoveSessionIfExistsInCache', NULL)
INSERT [dbo].[SERVICE_ROUTES] ([ID], [NAME], [CALLTYPE], [ENDPOINT], [DELETE_DATE]) VALUES (2123, N'gateway.public.identity.removesessionifexistsincache', N'GET', N'http://localhost:20200/Identity/RemoveSessionIfExistsInCache', NULL)
SET IDENTITY_INSERT [dbo].[SERVICE_ROUTES] OFF
GO
SET IDENTITY_INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ON 

INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1, 1020, 1029, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (2, 1012, 1025, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (3, 1014, 1026, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (4, 1015, 1027, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (5, 1016, 1028, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (6, 1021, 1030, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (7, 1022, 1031, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (8, 1023, 1032, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (9, 3, 1002, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (10, 4, 1003, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (11, 1004, 1033, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (12, 1005, 1034, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (13, 1006, 1035, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (14, 1007, 1036, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (15, 1008, 1037, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (16, 1009, 1038, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (17, 1010, 1039, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (18, 1011, 1040, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (19, 1017, 1041, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (20, 1018, 1042, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (21, 1019, 1043, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (22, 1020, 1029, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (23, 1024, 1044, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (24, 1045, 1046, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (25, 1047, 1048, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (26, 1049, 1050, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (27, 1051, 1052, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (28, 1053, 1054, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (29, 1055, 1056, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1024, 2045, 2048, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1025, 2046, 2049, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1026, 2047, 2050, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1027, 2067, 2068, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1028, 2069, 2070, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1029, 2071, 2073, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1030, 2072, 2074, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1031, 2075, 2077, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1032, 2076, 2078, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1033, 2079, 2081, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1034, 2080, 2082, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1035, 2083, 2085, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1036, 2084, 2086, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1037, 2087, 2088, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1038, 2089, 2090, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1039, 2091, 2092, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1040, 2093, 2094, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1041, 2095, 2096, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1042, 2097, 2098, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1043, 2099, 2100, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1044, 2103, 2104, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1045, 2105, 2106, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1046, 2107, 2108, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1047, 2109, 2110, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1048, 2111, 2112, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1049, 2113, 2114, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1050, 2115, 2116, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1051, 2117, 2118, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1052, 2119, 2120, NULL)
INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] ([ID], [SERVICE_ROUTES_ID], [ALTERNATIVE_SERVICE_ROUTE_ID], [DELETE_DATE]) VALUES (1053, 2121, 2122, NULL)
SET IDENTITY_INSERT [dbo].[SERVICE_ROUTES_ALTERNATIVES] OFF
GO
SET IDENTITY_INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ON 

INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (2, 3, N'token', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1002, 1002, N'token', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1003, 1014, N'workerId', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1005, 1026, N'workerId', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1006, 2067, N'workerId', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1007, 2068, N'workerId', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1008, 2083, N'productId', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1009, 2085, N'productId', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1010, 2091, N'referencenumber', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1011, 2092, N'referencenumber', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1012, 2101, N'redirectInfo', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1013, 2103, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1014, 2104, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1015, 2105, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1016, 2106, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1017, 2107, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1018, 2108, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1019, 2109, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1020, 2110, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1021, 2111, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1022, 2112, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1023, 2113, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1024, 2114, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1025, 2115, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1026, 2116, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1027, 2117, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1028, 2118, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1029, 2119, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1030, 2120, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1031, 2121, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1032, 2122, N'tokenKey', NULL)
INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] ([ID], [SERVICE_ROUTE_ID], [KEY], [DELETE_DATE]) VALUES (1033, 2123, N'tokenKey', NULL)
SET IDENTITY_INSERT [dbo].[SERVICE_ROUTES_QUERYKEYS] OFF
GO
ALTER TABLE [dbo].[SERVICE_ROUTES_QUERYKEYS]  WITH CHECK ADD  CONSTRAINT [FK_SERVICE_ROUTES_QUERYKEYS_SERVICE_ROUTES] FOREIGN KEY([SERVICE_ROUTE_ID])
REFERENCES [dbo].[SERVICE_ROUTES] ([ID])
GO
ALTER TABLE [dbo].[SERVICE_ROUTES_QUERYKEYS] CHECK CONSTRAINT [FK_SERVICE_ROUTES_QUERYKEYS_SERVICE_ROUTES]
GO
USE [master]
GO
ALTER DATABASE [Microservice_Routing_DB] SET  READ_WRITE 
GO
