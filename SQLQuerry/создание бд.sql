USE [master]
GO

/****** Object:  Database [CurrencyState]    Script Date: 8/13/2019 12:41:09 AM ******/
CREATE DATABASE [CurrencyState]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CurrencyState', FILENAME = N'E:\PROGRAMS\DEVELOPING\DataBase\distr\DB\MSQLServer\MSSQL14.TESTMSSQLSER\MSSQL\DATA\CurrencyState.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CurrencyState_log', FILENAME = N'E:\PROGRAMS\DEVELOPING\DataBase\distr\DB\MSQLServer\MSSQL14.TESTMSSQLSER\MSSQL\DATA\CurrencyState_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [CurrencyState] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CurrencyState].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [CurrencyState] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [CurrencyState] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [CurrencyState] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [CurrencyState] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [CurrencyState] SET ARITHABORT OFF 
GO

ALTER DATABASE [CurrencyState] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [CurrencyState] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [CurrencyState] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [CurrencyState] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [CurrencyState] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [CurrencyState] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [CurrencyState] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [CurrencyState] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [CurrencyState] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [CurrencyState] SET  ENABLE_BROKER 
GO

ALTER DATABASE [CurrencyState] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [CurrencyState] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [CurrencyState] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [CurrencyState] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [CurrencyState] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [CurrencyState] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [CurrencyState] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [CurrencyState] SET RECOVERY FULL 
GO

ALTER DATABASE [CurrencyState] SET  MULTI_USER 
GO

ALTER DATABASE [CurrencyState] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [CurrencyState] SET DB_CHAINING OFF 
GO

ALTER DATABASE [CurrencyState] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [CurrencyState] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [CurrencyState] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [CurrencyState] SET QUERY_STORE = OFF
GO

ALTER DATABASE [CurrencyState] SET  READ_WRITE 
GO


