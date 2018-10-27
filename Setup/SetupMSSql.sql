USE master
GO
IF(DB_ID(N'evolutionDB') IS NULL) CREATE DATABASE evolutionDB;
GO
CREATE LOGIN appUser WITH PASSWORD = 'appPassword1';  
GO
USE evolutionDB;
GO
CREATE USER appUser FOR LOGIN appUser;
GO
EXEC sp_addrolemember 'db_owner', 'appUser'
go