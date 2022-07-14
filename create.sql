IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Tornado')
  BEGIN
    CREATE DATABASE [Tornado]


    END
    GO
       USE [Tornado]
    GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Category' and xtype='U')
BEGIN
    CREATE TABLE Category (
        Id INT PRIMARY KEY IDENTITY (1, 1),
        Name NVARCHAR(100) NOT NULL,
		Display_Order INT DEFAULT -1
    )
END
