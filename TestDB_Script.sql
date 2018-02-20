USE [master]
GO

/****** Object:  Database [TestDB]    Script Date: 2/19/2018 3:24:53 PM ******/
CREATE DATABASE [TestDB]
GO

ALTER DATABASE [TestDB] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TestDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TestDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TestDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TestDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TestDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [TestDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TestDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TestDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TestDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TestDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TestDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TestDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TestDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TestDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TestDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TestDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TestDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TestDB] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TestDB] SET  MULTI_USER 
GO

ALTER DATABASE [TestDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TestDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TestDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [TestDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TestDB] SET  READ_WRITE 
GO

USE [TestDB]
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[BugName] [nvarchar](300) NULL,
	[UserID] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectEmployments]    Script Date: 2/5/2018 5:13:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEmployments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[UserID] [int] NULL,
	[Role] [int] NULL,
 CONSTRAINT [PK_ProjectEmployment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2/5/2018 5:13:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](300) NULL,
	[Description] [nvarchar](500) NULL,
	[Customer] [nvarchar](300) NULL,
 CONSTRAINT [PK_dbo.Projects] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 2/5/2018 5:13:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [nvarchar](300) NULL,
	[UserID] [int] NULL,
	[ProjectID] [int] NULL,
	[Status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/5/2018 5:13:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[RoleName] [nvarchar](50) NULL
) ON [PRIMARY]
GO

