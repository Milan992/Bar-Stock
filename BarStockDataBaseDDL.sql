IF DB_ID('BarStock') IS NULL
CREATE DATABASE BarStock
GO
USE BarStock;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblArticle')
drop table tblArticle;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmployee')
drop table tblEmployee;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblCalculationMethod')
drop table tblCalculationMethod;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'vwArticle')
drop view vwArticle;


create table tblEmployee(
EmployeeID int identity(1,1) primary key,
FullName varchar(30) check(len(FullName) > 1) not null,
UserName varchar(30) check(len(UserName) > 4) not null unique,
Pass varchar(30) check(len(Pass) > 4) not null
)

Create table tblCalculationMethod(
CalculationMethodID int identity (1,1) primary key,
CalculationMethodName varchar(30) not null unique
)

create table tblArticle(
ArticleID int identity(1,1) primary key,
ArticleName varchar(30) not null unique,
Price int check(Price >= 0),
UnitOfMeasurement varchar(10) check(UnitOfMeasurement in ('kom', 'l')),
Amount decimal check(Amount >= 0) not null,
NewAmount decimal check(NewAmount >= 0),
ProcuredAmount decimal,
AmountSold decimal check(AmountSold >= 0),
CalculationMethodID int foreign key (CalculationMethodID) references tblCalculationMethod(CalculationMethodID) not null,
)

insert into tblCalculationMethod (CalculationMethodName)
values ('Upisi novo stranje artikla');

insert into tblCalculationMethod (CalculationMethodName)
values ('Upisi broj prodatih artikala');

insert into tblCalculationMethod (CalculationMethodName)
values ('Upisi stanje na brojacu');

USE [BarStock]
GO

/****** Object:  View [dbo].[vwArticle]    Script Date: 10/3/2020 10:13:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwArticle]
AS
SELECT        dbo.tblArticle.*, dbo.tblCalculationMethod.CalculationMethodName
FROM            dbo.tblArticle INNER JOIN
                         dbo.tblCalculationMethod ON dbo.tblArticle.CalculationMethodID = dbo.tblCalculationMethod.CalculationMethodID
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tblArticle"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tblCalculationMethod"
            Begin Extent = 
               Top = 6
               Left = 278
               Bottom = 102
               Right = 507
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwArticle'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwArticle'
GO


