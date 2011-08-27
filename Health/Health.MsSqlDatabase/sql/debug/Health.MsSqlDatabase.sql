/*
Скрипт развертывания для Health.MsSqlDatabase
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Health.MsSqlDatabase"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
USE [master]
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'Для состояния конечной базы данных %s не задано значение ONLINE. Чтобы выполнить развертывание в эту базу данных, необходимо задать для нее состояние ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO

IF NOT EXISTS (SELECT 1 FROM [master].[dbo].[sysdatabases] WHERE [name] = N'$(DatabaseName)')
BEGIN
    RAISERROR(N'Не удается развернуть этот скрипт обновления на конечном объекте CRASH-PC. База данных Health.MsSqlDatabase, для которой создан этот скрипт, не существует на данном сервере.', 16, 127) WITH NOWAIT
    RETURN
END

GO

IF (@@servername != 'CRASH-PC')
BEGIN
    RAISERROR(N'Имя сервера в сценарии построения %s не соответствует имени конечного сервера %s. Проверьте, правильны ли параметры проекта базы данных и не устарел ли сценарий построения.', 16, 127,N'CRASH-PC',@@servername) WITH NOWAIT
    RETURN
END

GO

IF CAST(DATABASEPROPERTY(N'$(DatabaseName)','IsReadOnly') as bit) = 1
BEGIN
    RAISERROR(N'Не удается развернуть этот скрипт обновления, так как для базы данных %s, для которой он создан, задано состояние READ_ONLY.', 16, 127, N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
USE [$(DatabaseName)]
GO
/*
 Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся операторы SQL, которые будут исполнены перед скриптом построения.	
 Используйте синтаксис SQLCMD для включения файла в скрипт перед развертыванием.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте перед развертыванием.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
PRINT N'Выполняется удаление Разрешения...';


GO
REVOKE CONNECT TO [dbo] CASCADE
    AS [dbo];


GO
PRINT N'Выполняется создание [dbo].[GetParametersForPatientNowDate]...';


GO
CREATE PROCEDURE [dbo].[GetParametersForPatientNowDate]
	@patient_id int,
	@date datetime = 0
AS
BEGIN
	set language Russian -- говорим что первый день недели это понедельник	
	SET NOCOUNT ON;
	if (@date = 0)
		set @date = getdate()
	declare @now_date datetime = @date -- текущая дата
	declare @now_time time(7) = convert(time(7), @now_date, 108) -- текущее время
	declare @month_id int = datepart(mm, @now_date) -- текущий месяц
	declare @week_day int = datepart(dw, @now_date)-- день недели
	declare @month_day int = datepart(dd, @now_date) -- текущий день
	declare @is_even_week bit = datepart(ww, @now_date) % 2 -- четная / нечетная неделя
	declare @week_number int = 
					datepart(ww, @now_date) - 
					datepart(ww, dateadd(dd, -@month_day + 1, @now_date)) + 1 -- номер недели в месяце
			
	/*
	select * from PersonalSchedule ps where
		(@now_date between ps.DateStart and ps.DateEnd) and
		(@now_time between ps.TimeStart and ps.TimeEnd) and
		(
			(ps.WeekDay = @week_day or ps.WeekDay is null) and
			(ps.MonthId = @month_id or ps.MonthId is null) and
			(ps.WeekId = @is_even_week or ps.WeekId is null) and
			(ps.WeekNumber = @week_number or ps.WeekNumber is null) and
			(ps.MonthDay = @month_day or ps.MonthDay is null)
		)
	*/
	select @is_even_week, @week_day
END
GO
/*
Шаблон скрипта после развертывания							
--------------------------------------------------------------------------------------
 В данном файле содержатся операторы SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

GO
