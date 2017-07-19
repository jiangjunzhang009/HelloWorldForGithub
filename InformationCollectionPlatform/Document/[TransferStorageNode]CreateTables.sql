


-- --DROP TABLE [dbo].[Tb_ExceptionLogPcAuto]
-- CREATE TABLE [dbo].[Tb_ExceptionLogPcAuto]


SELECT * FROM Tb_ExceptionLogPcAuto
SELECT * FROM Tb_OperationLogPcAuto
SELECT * FROM Tb_RawRequestPcAuto

SELECT * FROM Tb_ExceptionLogXcar
SELECT * FROM Tb_OperationLogXcar
SELECT * FROM Tb_RawRequestXcar

--'!@#$%^&*()_+{}|:"<>?11’''1'

--TRUNCATE TABLE Tb_ExceptionLogPcAuto
--TRUNCATE TABLE Tb_OperationLogPcAuto
--TRUNCATE TABLE Tb_RawRequestPcAuto

--TRUNCATE TABLE Tb_ExceptionLogXcar
--TRUNCATE TABLE Tb_OperationLogXcar
--TRUNCATE TABLE Tb_RawRequestXcar

-- (
	-- Id int identity(1,1),
	-- LogLevel NVARCHAR(20),
	-- LogMessage NVARCHAR(4000),
	-- LogBusinessTopic NVARCHAR(200),
	-- CreateDatetime Datetime default(getutcdate()),
	-- OtherMessage NVARCHAR(2000)	
-- )
-- GO

-- CREATE TABLE [dbo].[Tb_ExceptionLogXcar]
-- (
	-- Id int identity(1,1),
	-- LogLevel NVARCHAR(20),
	-- LogMessage NVARCHAR(4000),
	-- LogBusinessTopic NVARCHAR(200),
	-- CreateDatetime Datetime default(getutcdate()),
	-- OtherMessage NVARCHAR(2000)	
-- )
-- GO

-- CREATE TABLE [dbo].[Tb_OperationLogPcAuto]
-- (
	-- Id int identity(1,1),
	-- OperationLevel NVARCHAR(20),
	-- OperationMessage NVARCHAR(4000),
	-- OperationBusinessTopic NVARCHAR(200),
	-- CreateDatetime Datetime default(getutcdate()),
	-- OtherMessage NVARCHAR(2000)	
-- )
-- GO

-- CREATE TABLE [dbo].[Tb_OperationLogXcar]
-- (
	-- Id int identity(1,1),
	-- OperationLevel NVARCHAR(20),
	-- OperationMessage NVARCHAR(4000),
	-- OperationBusinessTopic NVARCHAR(200),
	-- CreateDatetime Datetime default(getutcdate()),
	-- OtherMessage NVARCHAR(2000)	
-- )
-- GO

-- CREATE TABLE [dbo].[Tb_RawRequestPcAuto]
-- (
	-- Id int identity(1,1),
	-- RequestUrl NVARCHAR(2000),
	-- RequestHeaders NVARCHAR(2000),
	-- RequestMethod VARCHAR(10),
	-- RequestBody NVARCHAR(4000),
	-- CreateDatetime Datetime default(getutcdate()),
	-- OtherMessage NVARCHAR(2000)	
-- )
-- GO

-- CREATE TABLE [dbo].[Tb_RawRequestXcar]
-- (
	-- Id int identity(1,1),
	-- RequestUrl NVARCHAR(2000),
	-- RequestHeaders NVARCHAR(2000),
	-- RequestMethod VARCHAR(10),
	-- RequestBody NVARCHAR(4000),
	-- CreateDatetime Datetime default(getutcdate()),
	-- OtherMessage NVARCHAR(2000)	
-- )
-- GO

