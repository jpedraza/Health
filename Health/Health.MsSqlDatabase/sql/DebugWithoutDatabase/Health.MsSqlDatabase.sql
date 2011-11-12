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
USE [master]

GO
:on error exit
GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL
    AND DATABASEPROPERTYEX(N'$(DatabaseName)','Status') <> N'ONLINE')
BEGIN
    RAISERROR(N'Для состояния конечной базы данных %s не задано значение ONLINE. Чтобы выполнить развертывание в эту базу данных, необходимо задать для нее состояние ONLINE.', 16, 127,N'$(DatabaseName)') WITH NOWAIT
    RETURN
END

GO
IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Выполняется создание $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [Health_MsSqlDatabase], FILENAME = N'$(DefaultDataPath)Health_MsSqlDatabase.mdf')
    LOG ON (NAME = [Health_MsSqlDatabase_log], FILENAME = N'$(DefaultLogPath)Health_MsSqlDatabase_log.ldf') COLLATE Cyrillic_General_CI_AS
GO
EXECUTE sp_dbcmptlevel [$(DatabaseName)], 90;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Параметры базы данных изменить нельзя. Применить эти параметры может только пользователь SysAdmin.';
    END


GO
USE [$(DatabaseName)]

GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


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
REVOKE CONNECT TO [dbo]
    AS [dbo];


GO
PRINT N'Выполняется удаление [AutoCreatedLocal]...';


GO
DROP ROUTE [AutoCreatedLocal];


GO
PRINT N'Выполняется создание [dbo].[Appointments]...';


GO
CREATE TABLE [dbo].[Appointments] (
    [AppointmentId] INT      IDENTITY (1, 1) NOT NULL,
    [PatientId]     INT      NOT NULL,
    [DoctorId]      INT      NOT NULL,
    [Date]          DATETIME NOT NULL
);


GO
PRINT N'Выполняется создание AppointmentsPK...';


GO
ALTER TABLE [dbo].[Appointments]
    ADD CONSTRAINT [AppointmentsPK] PRIMARY KEY CLUSTERED ([AppointmentId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Candidates]...';


GO
CREATE TABLE [dbo].[Candidates] (
    [CandidateId]            INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]              NVARCHAR (MAX) NOT NULL,
    [LastName]               NVARCHAR (MAX) NOT NULL,
    [ThirdName]              NVARCHAR (MAX) NOT NULL,
    [Login]                  NVARCHAR (MAX) NOT NULL,
    [Password]               NVARCHAR (MAX) NOT NULL,
    [RoleId]                 INT            NOT NULL,
    [Birthday]               DATETIME       NOT NULL,
    [Token]                  NVARCHAR (MAX) NOT NULL,
    [Policy]                 NVARCHAR (MAX) NOT NULL,
    [Card]                   NVARCHAR (MAX) NOT NULL,
    [Mother]                 NVARCHAR (MAX) NOT NULL,
    [StartDateOfObservation] DATETIME       NOT NULL,
    [Phone1]                 INT            NULL,
    [Phone2]                 INT            NULL
);


GO
PRINT N'Выполняется создание CandiadtesPK...';


GO
ALTER TABLE [dbo].[Candidates]
    ADD CONSTRAINT [CandiadtesPK] PRIMARY KEY CLUSTERED ([CandidateId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[DefaultSchedule]...';


GO
CREATE TABLE [dbo].[DefaultSchedule] (
    [DefaultScheduleId] INT      IDENTITY (1, 1) NOT NULL,
    [ParameterId]       INT      NULL,
    [DiagnosesId]       INT      NULL,
    [Years]             INT      NULL,
    [Months]            INT      NULL,
    [Weeks]             INT      NULL,
    [Days]              INT      NULL,
    [Hours]             INT      NULL,
    [Minutes]           INT      NULL,
    [TimeStart]         TIME (7) NULL,
    [TimeEnd]           TIME (7) NULL,
    [DayOfWeek]         INT      NULL,
    [DayOfMonth]        INT      NULL,
    [MonthOfYear]       INT      NULL,
    [WeekOfMonth]       INT      NULL,
    [WeekParity]        INT      NULL
);


GO
PRINT N'Выполняется создание DefaultSchedulePK...';


GO
ALTER TABLE [dbo].[DefaultSchedule]
    ADD CONSTRAINT [DefaultSchedulePK] PRIMARY KEY CLUSTERED ([DefaultScheduleId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Diagnosis]...';


GO
CREATE TABLE [dbo].[Diagnosis] (
    [DiagnosisId]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]                NVARCHAR (MAX) NULL,
    [Code]                NVARCHAR (MAX) NULL,
    [DiagnosisClassId]    INT            NOT NULL,
    [DiagnosisSubClassId] INT            NOT NULL
);


GO
PRINT N'Выполняется создание DiagnosisPK...';


GO
ALTER TABLE [dbo].[Diagnosis]
    ADD CONSTRAINT [DiagnosisPK] PRIMARY KEY CLUSTERED ([DiagnosisId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[DiagnosisClass]...';


GO
CREATE TABLE [dbo].[DiagnosisClass] (
    [DiagnosisClassId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (MAX) NOT NULL,
    [Code]             NVARCHAR (MAX) NOT NULL,
    [Level]            INT            NOT NULL
);


GO
PRINT N'Выполняется создание DiagnosisClassPK...';


GO
ALTER TABLE [dbo].[DiagnosisClass]
    ADD CONSTRAINT [DiagnosisClassPK] PRIMARY KEY CLUSTERED ([DiagnosisClassId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Doctors]...';


GO
CREATE TABLE [dbo].[Doctors] (
    [DoctorId]    INT NOT NULL,
    [SpecialtyId] INT NOT NULL
);


GO
PRINT N'Выполняется создание DoctorsPK...';


GO
ALTER TABLE [dbo].[Doctors]
    ADD CONSTRAINT [DoctorsPK] PRIMARY KEY CLUSTERED ([DoctorId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Parameters]...';


GO
CREATE TABLE [dbo].[Parameters] (
    [ParameterId]  INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX) NULL,
    [DefaultValue] VARBINARY (1)  NULL,
    [Value]        VARBINARY (1)  NULL,
    [Metadata]     VARBINARY (1)  NULL
);


GO
PRINT N'Выполняется создание ParametersPK...';


GO
ALTER TABLE [dbo].[Parameters]
    ADD CONSTRAINT [ParametersPK] PRIMARY KEY CLUSTERED ([ParameterId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Patients]...';


GO
CREATE TABLE [dbo].[Patients] (
    [PatientId]              INT            NOT NULL,
    [Policy]                 NVARCHAR (MAX) NOT NULL,
    [Card]                   NVARCHAR (MAX) NOT NULL,
    [Mother]                 NVARCHAR (MAX) NOT NULL,
    [StartDateOfObservation] DATETIME       NOT NULL,
    [Phone1]                 INT            NULL,
    [Phone2]                 INT            NULL
);


GO
PRINT N'Выполняется создание PatientsPK...';


GO
ALTER TABLE [dbo].[Patients]
    ADD CONSTRAINT [PatientsPK] PRIMARY KEY CLUSTERED ([PatientId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[PatientsToDiagnosis]...';


GO
CREATE TABLE [dbo].[PatientsToDiagnosis] (
    [PatientId]   INT NOT NULL,
    [DiagnosisId] INT NOT NULL
);


GO
PRINT N'Выполняется создание [dbo].[PatientsToDoctors]...';


GO
CREATE TABLE [dbo].[PatientsToDoctors] (
    [DoctorId]  INT NOT NULL,
    [PatientId] INT NOT NULL
);


GO
PRINT N'Выполняется создание [dbo].[PatientsToSurgerys]...';


GO
CREATE TABLE [dbo].[PatientsToSurgerys] (
    [SurgeryId]     INT      NOT NULL,
    [PatientId]     INT      NOT NULL,
    [SurgeryDate]   DATETIME NOT NULL,
    [SurgeryStatus] BIT      NULL
);


GO
PRINT N'Выполняется создание [dbo].[PersonalSchedule]...';


GO
CREATE TABLE [dbo].[PersonalSchedule] (
    [PersonalScheduleId] INT      IDENTITY (1, 1) NOT NULL,
    [PatientId]          INT      NULL,
    [ParameterId]        INT      NULL,
    [DateStart]          DATETIME NULL,
    [DateEnd]            DATETIME NULL,
    [TimeStart]          TIME (7) NULL,
    [TimeEnd]            TIME (7) NULL,
    [DayOfWeek]          INT      NULL,
    [DayOfMonth]         INT      NULL,
    [MonthOfYear]        INT      NULL,
    [WeekOfMonth]        INT      NULL,
    [WeekParity]         INT      NULL
);


GO
PRINT N'Выполняется создание PersonalSchedulePK...';


GO
ALTER TABLE [dbo].[PersonalSchedule]
    ADD CONSTRAINT [PersonalSchedulePK] PRIMARY KEY CLUSTERED ([PersonalScheduleId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [RoleId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Выполняется создание RolesPK...';


GO
ALTER TABLE [dbo].[Roles]
    ADD CONSTRAINT [RolesPK] PRIMARY KEY CLUSTERED ([RoleId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Specialties]...';


GO
CREATE TABLE [dbo].[Specialties] (
    [SpecialtyId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL
);


GO
PRINT N'Выполняется создание SpecialtiesPK...';


GO
ALTER TABLE [dbo].[Specialties]
    ADD CONSTRAINT [SpecialtiesPK] PRIMARY KEY CLUSTERED ([SpecialtyId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Surgerys]...';


GO
CREATE TABLE [dbo].[Surgerys] (
    [SurgeryId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NULL,
    [SurgeryDescription] NVARCHAR (MAX) NULL
);


GO
PRINT N'Выполняется создание SurgeryPK...';


GO
ALTER TABLE [dbo].[Surgerys]
    ADD CONSTRAINT [SurgeryPK] PRIMARY KEY CLUSTERED ([SurgeryId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [UserId]    INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (MAX) NOT NULL,
    [LastName]  NVARCHAR (MAX) NOT NULL,
    [ThirdName] NVARCHAR (MAX) NOT NULL,
    [Login]     NVARCHAR (MAX) NOT NULL,
    [Password]  NVARCHAR (MAX) NOT NULL,
    [RoleId]    INT            NOT NULL,
    [Birthday]  DATETIME       NOT NULL,
    [Token]     NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Выполняется создание UsersPK...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [UsersPK] PRIMARY KEY CLUSTERED ([UserId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[WorkWeeks]...';


GO
CREATE TABLE [dbo].[WorkWeeks] (
    [DoctorId]            INT      NOT NULL,
    [IsWeekEndDay]        BIT      NOT NULL,
    [DayInWeek]           INT      NOT NULL,
    [TimeStart]           TIME (7) NOT NULL,
    [TimeEnd]             TIME (7) NOT NULL,
    [DinnerStart]         TIME (7) NOT NULL,
    [DinnerEnd]           TIME (7) NOT NULL,
    [AttendingHoursStart] TIME (7) NOT NULL,
    [AttendingHoursEnd]   TIME (7) NOT NULL,
    [AttendingMinutes]    INT      NOT NULL
);


GO
PRINT N'Выполняется создание AppointmentsMTODoctors...';


GO
ALTER TABLE [dbo].[Appointments] WITH NOCHECK
    ADD CONSTRAINT [AppointmentsMTODoctors] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание AppointmentsMTOPatients...';


GO
ALTER TABLE [dbo].[Appointments] WITH NOCHECK
    ADD CONSTRAINT [AppointmentsMTOPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание CandidatesMTORoles...';


GO
ALTER TABLE [dbo].[Candidates] WITH NOCHECK
    ADD CONSTRAINT [CandidatesMTORoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание DefaultScheduleMTOParameters...';


GO
ALTER TABLE [dbo].[DefaultSchedule] WITH NOCHECK
    ADD CONSTRAINT [DefaultScheduleMTOParameters] FOREIGN KEY ([ParameterId]) REFERENCES [dbo].[Parameters] ([ParameterId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание DiagnosisMTODiagnosisClass...';


GO
ALTER TABLE [dbo].[Diagnosis] WITH NOCHECK
    ADD CONSTRAINT [DiagnosisMTODiagnosisClass] FOREIGN KEY ([DiagnosisClassId]) REFERENCES [dbo].[DiagnosisClass] ([DiagnosisClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание DiagnosisMTODiagnosisSubClass...';


GO
ALTER TABLE [dbo].[Diagnosis] WITH NOCHECK
    ADD CONSTRAINT [DiagnosisMTODiagnosisSubClass] FOREIGN KEY ([DiagnosisSubClassId]) REFERENCES [dbo].[DiagnosisClass] ([DiagnosisClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание DoctorsMTOSpecialties...';


GO
ALTER TABLE [dbo].[Doctors] WITH NOCHECK
    ADD CONSTRAINT [DoctorsMTOSpecialties] FOREIGN KEY ([SpecialtyId]) REFERENCES [dbo].[Specialties] ([SpecialtyId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание DoctorsOTOUsers...';


GO
ALTER TABLE [dbo].[Doctors] WITH NOCHECK
    ADD CONSTRAINT [DoctorsOTOUsers] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsOTOUsers...';


GO
ALTER TABLE [dbo].[Patients] WITH NOCHECK
    ADD CONSTRAINT [PatientsOTOUsers] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsToDiagnosisMTMDiagnosis...';


GO
ALTER TABLE [dbo].[PatientsToDiagnosis] WITH NOCHECK
    ADD CONSTRAINT [PatientsToDiagnosisMTMDiagnosis] FOREIGN KEY ([DiagnosisId]) REFERENCES [dbo].[Diagnosis] ([DiagnosisId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsToDiagnosisMTMPatients...';


GO
ALTER TABLE [dbo].[PatientsToDiagnosis] WITH NOCHECK
    ADD CONSTRAINT [PatientsToDiagnosisMTMPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsMTMDoctorsToDoctors...';


GO
ALTER TABLE [dbo].[PatientsToDoctors] WITH NOCHECK
    ADD CONSTRAINT [PatientsMTMDoctorsToDoctors] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsMTMDoctorsToPatients...';


GO
ALTER TABLE [dbo].[PatientsToDoctors] WITH NOCHECK
    ADD CONSTRAINT [PatientsMTMDoctorsToPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsToSurgerysMTMPatients...';


GO
ALTER TABLE [dbo].[PatientsToSurgerys] WITH NOCHECK
    ADD CONSTRAINT [PatientsToSurgerysMTMPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsToSurgerysMTMSurgerys...';


GO
ALTER TABLE [dbo].[PatientsToSurgerys] WITH NOCHECK
    ADD CONSTRAINT [PatientsToSurgerysMTMSurgerys] FOREIGN KEY ([SurgeryId]) REFERENCES [dbo].[Surgerys] ([SurgeryId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PersonalScheduleMTOParameters...';


GO
ALTER TABLE [dbo].[PersonalSchedule] WITH NOCHECK
    ADD CONSTRAINT [PersonalScheduleMTOParameters] FOREIGN KEY ([ParameterId]) REFERENCES [dbo].[Parameters] ([ParameterId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PersonalScheduleMTOPatients...';


GO
ALTER TABLE [dbo].[PersonalSchedule] WITH NOCHECK
    ADD CONSTRAINT [PersonalScheduleMTOPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание UsersMTORoles...';


GO
ALTER TABLE [dbo].[Users] WITH NOCHECK
    ADD CONSTRAINT [UsersMTORoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание WorkWeeksMTODoctors...';


GO
ALTER TABLE [dbo].[WorkWeeks] WITH NOCHECK
    ADD CONSTRAINT [WorkWeeksMTODoctors] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


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
-- Выполняется этап рефакторинга для обновления развернутых журналов транзакций на целевом сервере
CREATE TABLE  [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
GO
sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
GO

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
PRINT N'Существующие данные проверяются относительно вновь созданных ограничений';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Appointments] WITH CHECK CHECK CONSTRAINT [AppointmentsMTODoctors];

ALTER TABLE [dbo].[Appointments] WITH CHECK CHECK CONSTRAINT [AppointmentsMTOPatients];

ALTER TABLE [dbo].[Candidates] WITH CHECK CHECK CONSTRAINT [CandidatesMTORoles];

ALTER TABLE [dbo].[DefaultSchedule] WITH CHECK CHECK CONSTRAINT [DefaultScheduleMTOParameters];

ALTER TABLE [dbo].[Diagnosis] WITH CHECK CHECK CONSTRAINT [DiagnosisMTODiagnosisClass];

ALTER TABLE [dbo].[Diagnosis] WITH CHECK CHECK CONSTRAINT [DiagnosisMTODiagnosisSubClass];

ALTER TABLE [dbo].[Doctors] WITH CHECK CHECK CONSTRAINT [DoctorsMTOSpecialties];

ALTER TABLE [dbo].[Doctors] WITH CHECK CHECK CONSTRAINT [DoctorsOTOUsers];

ALTER TABLE [dbo].[Patients] WITH CHECK CHECK CONSTRAINT [PatientsOTOUsers];

ALTER TABLE [dbo].[PatientsToDiagnosis] WITH CHECK CHECK CONSTRAINT [PatientsToDiagnosisMTMDiagnosis];

ALTER TABLE [dbo].[PatientsToDiagnosis] WITH CHECK CHECK CONSTRAINT [PatientsToDiagnosisMTMPatients];

ALTER TABLE [dbo].[PatientsToDoctors] WITH CHECK CHECK CONSTRAINT [PatientsMTMDoctorsToDoctors];

ALTER TABLE [dbo].[PatientsToDoctors] WITH CHECK CHECK CONSTRAINT [PatientsMTMDoctorsToPatients];

ALTER TABLE [dbo].[PatientsToSurgerys] WITH CHECK CHECK CONSTRAINT [PatientsToSurgerysMTMPatients];

ALTER TABLE [dbo].[PatientsToSurgerys] WITH CHECK CHECK CONSTRAINT [PatientsToSurgerysMTMSurgerys];

ALTER TABLE [dbo].[PersonalSchedule] WITH CHECK CHECK CONSTRAINT [PersonalScheduleMTOParameters];

ALTER TABLE [dbo].[PersonalSchedule] WITH CHECK CHECK CONSTRAINT [PersonalScheduleMTOPatients];

ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [UsersMTORoles];

ALTER TABLE [dbo].[WorkWeeks] WITH CHECK CHECK CONSTRAINT [WorkWeeksMTODoctors];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        DECLARE @VarDecimalSupported AS BIT;
        SELECT @VarDecimalSupported = 0;
        IF ((ServerProperty(N'EngineEdition') = 3)
            AND (((@@microsoftversion / power(2, 24) = 9)
                  AND (@@microsoftversion & 0xffff >= 3024))
                 OR ((@@microsoftversion / power(2, 24) = 10)
                     AND (@@microsoftversion & 0xffff >= 1600))))
            SELECT @VarDecimalSupported = 1;
        IF (@VarDecimalSupported > 0)
            BEGIN
                EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
            END
    END


GO
