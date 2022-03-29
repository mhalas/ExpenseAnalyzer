CREATE DATABASE ExpenseDB;

USE [ExpenseDB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User] (
    [Id]       INT            IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Username] NVARCHAR (25)  NOT NULL,
    [Password] NVARCHAR (100) NOT NULL
);

CREATE TABLE [dbo].[UserCategory] (
    [Id]           INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [UserId]       INT NOT NULL,
    [CategoryName] nvarchar(100) NOT NULL

	FOREIGN KEY (UserId) REFERENCES dbo.[User](ID)
);

CREATE TABLE [dbo].[UserCategoryValue] (
    [Id]             INT            IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [UserCategoryId] INT            NOT NULL,
    [SellerName]     NVARCHAR (100) NOT NULL,

	FOREIGN KEY (UserCategoryId) REFERENCES dbo.[UserCategory](ID)
);

CREATE TABLE [dbo].[UserConfiguration] (
    [Id]          INT            IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Key]         NVARCHAR (25)  NOT NULL,
    [Value]       NVARCHAR (25)  NOT NULL,
    [Description] NVARCHAR (200) NOT NULL,
    [UserId]      INT            NOT NULL,

	FOREIGN KEY (UserId) REFERENCES dbo.[User](ID)
);

CREATE TABLE [dbo].[Expense] (
    [Id]             INT             IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Description]    NVARCHAR (255)  NULL,
    [PayDate]        DATE            NOT NULL,
    [Price]          DECIMAL (18, 2) NOT NULL,
    [SellerName]     NVARCHAR (150)  NOT NULL,
    [UserCategoryId] INT             NOT NULL,
    [UserId]         INT             NOT NULL,

	FOREIGN KEY (UserId) REFERENCES dbo.[User](ID),
	FOREIGN KEY (UserCategoryId) REFERENCES dbo.[UserCategory](ID)
);