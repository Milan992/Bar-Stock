IF DB_ID('BarStock') IS NULL
CREATE DATABASE BarStock
GO
USE BarStock;

if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblArticle')
drop table tblArticle;