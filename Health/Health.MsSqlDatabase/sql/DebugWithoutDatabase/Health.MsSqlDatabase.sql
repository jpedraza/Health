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
    [DiagnosisId]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (MAX) NULL,
    [Code]             NVARCHAR (MAX) NULL,
    [DiagnosisClassId] INT            NOT NULL
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
    [Parent]           INT            NULL
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
PRINT N'Выполняется создание [dbo].[FunctionalClasses]...';


GO
CREATE TABLE [dbo].[FunctionalClasses] (
    [FunctionalClassesId] INT            IDENTITY (1, 1) NOT NULL,
    [Code]                NVARCHAR (MAX) NOT NULL,
    [Description]         VARCHAR (MAX)  NOT NULL
);


GO
PRINT N'Выполняется создание FunctionalClassesPK...';


GO
ALTER TABLE [dbo].[FunctionalClasses]
    ADD CONSTRAINT [FunctionalClassesPK] PRIMARY KEY CLUSTERED ([FunctionalClassesId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[FunctionalDisorders]...';


GO
CREATE TABLE [dbo].[FunctionalDisorders] (
    [FunctionalDisordersId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]                  NVARCHAR (MAX) NOT NULL,
    [Parent]                INT            NULL
);


GO
PRINT N'Выполняется создание FunctionalDisordersPK...';


GO
ALTER TABLE [dbo].[FunctionalDisorders]
    ADD CONSTRAINT [FunctionalDisordersPK] PRIMARY KEY CLUSTERED ([FunctionalDisordersId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[FunctionalDisordersToPatients]...';


GO
CREATE TABLE [dbo].[FunctionalDisordersToPatients] (
    [FunctionalDisordersId] INT NOT NULL,
    [PatientId]             INT NOT NULL
);


GO
PRINT N'Выполняется создание [dbo].[ParameterMetadata]...';


GO
CREATE TABLE [dbo].[ParameterMetadata] (
    [MetadataId]  INT             IDENTITY (1, 1) NOT NULL,
    [ParameterId] INT             NOT NULL,
    [Key]         NVARCHAR (MAX)  NOT NULL,
    [Value]       VARBINARY (MAX) NULL,
    [ValueTypeId] INT             NOT NULL
);


GO
PRINT N'Выполняется создание [dbo].[Parameters]...';


GO
CREATE TABLE [dbo].[Parameters] (
    [ParameterId]  INT             IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (MAX)  NOT NULL,
    [DefaultValue] VARBINARY (MAX) NULL
);


GO
PRINT N'Выполняется создание ParametersPK...';


GO
ALTER TABLE [dbo].[Parameters]
    ADD CONSTRAINT [ParametersPK] PRIMARY KEY CLUSTERED ([ParameterId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[ParametersForPatients]...';


GO
CREATE TABLE [dbo].[ParametersForPatients] (
    [ParameterId] INT             NOT NULL,
    [PatientId]   INT             NOT NULL,
    [Value]       VARBINARY (MAX) NOT NULL,
    [Date]        DATETIME        NOT NULL
);


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
    [Phone2]                 INT            NULL,
    [FunctionalClassesId]    INT            NOT NULL
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
    [Name]        NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Выполняется создание SpecialtiesPK...';


GO
ALTER TABLE [dbo].[Specialties]
    ADD CONSTRAINT [SpecialtiesPK] PRIMARY KEY CLUSTERED ([SpecialtyId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[Status]...';


GO
CREATE TABLE [dbo].[Status] (
    [Code]    INT            NOT NULL,
    [Message] NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Выполняется создание StatusPK...';


GO
ALTER TABLE [dbo].[Status]
    ADD CONSTRAINT [StatusPK] PRIMARY KEY CLUSTERED ([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


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
PRINT N'Выполняется создание [dbo].[ValueTypes]...';


GO
CREATE TABLE [dbo].[ValueTypes] (
    [ValueTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL
);


GO
PRINT N'Выполняется создание ValueTypesPK...';


GO
ALTER TABLE [dbo].[ValueTypes]
    ADD CONSTRAINT [ValueTypesPK] PRIMARY KEY CLUSTERED ([ValueTypeId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Выполняется создание [dbo].[WorkWeeks]...';


GO
CREATE TABLE [dbo].[WorkWeeks] (
    [DoctorId]            INT      NOT NULL,
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
PRINT N'Выполняется создание DiagnosisClassMTO...';


GO
ALTER TABLE [dbo].[DiagnosisClass] WITH NOCHECK
    ADD CONSTRAINT [DiagnosisClassMTO] FOREIGN KEY ([Parent]) REFERENCES [dbo].[DiagnosisClass] ([DiagnosisClassId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


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
PRINT N'Выполняется создание FunctionalDisordersMTO...';


GO
ALTER TABLE [dbo].[FunctionalDisorders] WITH NOCHECK
    ADD CONSTRAINT [FunctionalDisordersMTO] FOREIGN KEY ([Parent]) REFERENCES [dbo].[FunctionalDisorders] ([FunctionalDisordersId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание FunctionalDisordersToDiagnosisMTOFunctionalDisorders...';


GO
ALTER TABLE [dbo].[FunctionalDisordersToPatients] WITH NOCHECK
    ADD CONSTRAINT [FunctionalDisordersToDiagnosisMTOFunctionalDisorders] FOREIGN KEY ([FunctionalDisordersId]) REFERENCES [dbo].[FunctionalDisorders] ([FunctionalDisordersId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание FunctionalDisordersToDiagnosisMTOPatients...';


GO
ALTER TABLE [dbo].[FunctionalDisordersToPatients] WITH NOCHECK
    ADD CONSTRAINT [FunctionalDisordersToDiagnosisMTOPatients] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание ParameterMetadataMTOParameter...';


GO
ALTER TABLE [dbo].[ParameterMetadata] WITH NOCHECK
    ADD CONSTRAINT [ParameterMetadataMTOParameter] FOREIGN KEY ([ParameterId]) REFERENCES [dbo].[Parameters] ([ParameterId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание ParameterMetadataMTOValueTypes...';


GO
ALTER TABLE [dbo].[ParameterMetadata] WITH NOCHECK
    ADD CONSTRAINT [ParameterMetadataMTOValueTypes] FOREIGN KEY ([ValueTypeId]) REFERENCES [dbo].[ValueTypes] ([ValueTypeId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание ParametersMTOPatientsParameter...';


GO
ALTER TABLE [dbo].[ParametersForPatients] WITH NOCHECK
    ADD CONSTRAINT [ParametersMTOPatientsParameter] FOREIGN KEY ([ParameterId]) REFERENCES [dbo].[Parameters] ([ParameterId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание ParametersMTOPatientsPatient...';


GO
ALTER TABLE [dbo].[ParametersForPatients] WITH NOCHECK
    ADD CONSTRAINT [ParametersMTOPatientsPatient] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


GO
PRINT N'Выполняется создание PatientsMTOFunctionalClasses...';


GO
ALTER TABLE [dbo].[Patients] WITH NOCHECK
    ADD CONSTRAINT [PatientsMTOFunctionalClasses] FOREIGN KEY ([FunctionalClassesId]) REFERENCES [dbo].[FunctionalClasses] ([FunctionalClassesId]) ON DELETE NO ACTION ON UPDATE NO ACTION;


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
PRINT N'Выполняется создание [dbo].[InsertWorkDayOfDoctors4]...';


GO
CREATE TRIGGER [dbo].[InsertWorkDayOfDoctors4]
ON [dbo].[WorkWeeks]
AFTER INSERT 
AS 
BEGIN	
	DECLARE @DAYINWEEK INT
	DECLARE @DOCTORID INT
	DECLARE @ROWS_IN_TABLE INT
	
	SET @DAYINWEEK = (SELECT inserted.DayInWeek FROM inserted)
	SET @DOCTORID = (SELECT inserted.DoctorId FROM inserted)
	
	SET @ROWS_IN_TABLE = (SELECT COUNT(*) FROM WorkWeeks
	WHERE WorkWeeks.DayInWeek = @DAYINWEEK AND
	WorkWeeks.DoctorId = @DOCTORID)
	
	IF @ROWS_IN_TABLE > 1 
	BEGIN
	ROLLBACK TRAN
	PRINT 'ОШИБКА. У доктора не может быть несколько расписаний на один рабочий день'
	END
END
GO
PRINT N'Выполняется создание [dbo].[GetAllShowDataByRoleName]...';


GO
CREATE PROCEDURE [dbo].[GetAllShowDataByRoleName]
	@param1 int = 0, 
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
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
PRINT N'Выполняется создание [dbo].[DeleteDoctor]...';


GO
CREATE PROCEDURE [dbo].[DeleteDoctor]
	@doctorId int = 0
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if exists(select * from Doctors where DoctorId = @doctorId)
	begin		
		if exists(select * from PatientsToDoctors where DoctorId = @doctorId)
		begin
			set @status = 0
			set @statusMessage = dbo.GSM(2001001)
		end
		else 
		begin		
			delete from Doctors where DoctorId = @doctorId
		end
	end
	else 
	begin
		set @status = 0
		set @statusMessage = dbo.GSM(3001001)
	end	
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[DeleteMetadata]...';


GO
CREATE PROCEDURE [dbo].[DeleteMetadata]
	@Id int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		if EXISTS( select pm.MetadataId						
							from ParameterMetadata as pm
							where pm.MetadataId=@Id)
			begin
				delete from ParameterMetadata where MetadataId=@Id
			end
		else
		begin
			set @status = 0
			set @statusMessage = dbo.GSM(4001001)
		end

	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[DeleteParameter]...';


GO
CREATE PROCEDURE [dbo].[DeleteParameter]
	@Id int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		if EXISTS( select * 
					FROM Parameters as p
					WHERE p.ParameterId=@Id)
			begin
				DELETE FROM ParameterMetadata
				WHERE ParameterId=@Id
								
				DELETE FROM Parameters WHERE ParameterId=@Id
			end
		else
		begin
			set @status = 0
			set @statusMessage = dbo.GSM(4001001)
		end

	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[DeleteValueTypes]...';


GO
CREATE PROCEDURE [dbo].[DeleteValueTypes]
	@deleteValueTypeId int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		delete ValueTypes
			where ValueTypeId=@deleteValueTypeId
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[FullUpdateDoctor]...';


GO
CREATE PROCEDURE [dbo].[FullUpdateDoctor]
	@Id int, 
	@FirstName nvarchar(MAX), 
	@LastName nvarchar(MAX), 
	@ThirdName nvarchar(MAX), 
	@Login nvarchar(MAX), 
	@Password nvarchar(MAX),
	@Birthday datetime,
	@Token nvarchar(MAX),
	@SpecialtyId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update Doctors
			set SpecialtyId=@SpecialtyId
			where DoctorId=@Id
		update Users
			set FirstName=@FirstName,
				LastName=@LastName,
				ThirdName=@ThirdName,
				Login=@Login,
				Password=@Password,
				Birthday=@Birthday,
				Token=@Token
			where UserId=@Id
			set @status = 1
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(3001001)
	end catch
	select @status as Status, @statusMessage as StatusMessage	
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllDiagnosis]...';


GO
CREATE PROCEDURE [dbo].[GetAllDiagnosis]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	SELECT d.DiagnosisId as Id,
		   d.Name,
		   d.Code,
		   dc.Name as ClassName,
		   @status as Status, @statusMessage as StatusMessage
		   from Diagnosis as d
		   join DiagnosisClass as dc on d.DiagnosisClassId = dc.DiagnosisClassId
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllDoctorShowData]...';


GO
CREATE PROCEDURE [dbo].[GetAllDoctorShowData]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	select us.UserId as Id, 
		   FirstName, 
		   LastName, 
		   ThirdName, 
		   Login, 
		   Password, 
		   ro.Name as Role, 
		   Birthday,
		   Token,
		   sp.Name as Specialty,
		   @status as Status, @statusMessage as StatusMessage
		   from Users as us 
		   JOIN Doctors as do ON us.UserId = do.DoctorId
		   JOIN Roles as ro ON us.RoleId = ro.RoleId
		   JOIN Specialties as sp ON do.SpecialtyId = sp.SpecialtyId			   
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllMetadata]...';


GO
CREATE PROCEDURE [dbo].[GetAllMetadata]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		select 
			pm.ParameterId as ParameterId,
			pm.Value as Value,
			pm.ValueTypeId as ValueTypeId,
			pm.[Key] as [Key],
			pm.MetadataId as Id,
			@status as Status,
			p.Name as ParameterName,
			vt.Name as ValueTypeName,
			@statusMessage as StatusMessage			
			from ParameterMetadata as pm
			JOIN Parameters as p ON pm.ParameterId=p.ParameterId
			JOIN ValueTypes as vt ON pm.ValueTypeId=vt.ValueTypeId
			
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
	
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllMetadataForParameterByParameterId]...';


GO
CREATE PROCEDURE [dbo].[GetAllMetadataForParameterByParameterId]
	@ParameterId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		select 
			pm.ParameterId as ParameterId,
			pm.Value as Value,
			pm.ValueTypeId as ValueTypeId,
			pm.[Key] as [Key],
			pm.MetadataId as Id,
			@status as Status,
			p.Name as ParameterName,
			vt.Name as ValueTypeName,
			@statusMessage as StatusMessage			
			FROM ParameterMetadata AS pm,
			Parameters AS p,
			ValueTypes AS vt
			WHERE pm.ParameterId=@ParameterId AND
				p.ParameterId=pm.ParameterId AND
				vt.ValueTypeId=pm.ValueTypeId
			
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllParameterShowData]...';


GO
CREATE PROCEDURE [dbo].[GetAllParameterShowData]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		select
		pa.ParameterId as ParameterId,
		pa.ParameterId as Id,
		pa.Name as Name,
		pa.DefaultValue as DefaultValue,
		@status as Status, @statusMessage as StatusMessage
		from Parameters as pa
			
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage

RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllPatientsForDoctor]...';


GO
CREATE PROCEDURE [dbo].[GetAllPatientsForDoctor]
	@doctorId int = 0
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if not(exists(select * from Doctors where DoctorId = @doctorId))
	begin
		set @statusMessage = dbo.GSM(3001001)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	if not(exists(select * from PatientsToDoctors where DoctorId = @doctorId))
	begin
		set @StatusMessage = dbo.GSM(1001001)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	select u.UserId as Id,
		   u.FirstName,
		   u.LastName,
		   u.ThirdName,
		   p.Card,
		   p.Policy,
		   @status as Status, @statusMessage as StatusMessage
		   from Users as u
		   join Patients as p on u.UserId = p.PatientId
		   join PatientsToDoctors as ptd on ptd.PatientId = p.PatientId
		   where ptd.DoctorId = @doctorId
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllUserShowData]...';


GO
CREATE PROCEDURE [dbo].[GetAllUserShowData]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	select us.UserId as Id, 
		   FirstName, 
		   LastName, 
		   ThirdName, 
		   Login, 
		   Password, 
		   ro.Name as Role, 
		   Birthday,
		   Token,
		   @status as Status, @statusMessage as StatusMessage
		   from Users as us 
		   JOIN Roles as ro ON us.RoleId = ro.RoleId
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetAllValueTypes]...';


GO
CREATE PROCEDURE [dbo].[GetAllValueTypes]
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		select @status as Status, @statusMessage as StatusMessage,
			v.ValueTypeId as ValueTypeId,
			v.Name as Name,
			v.ValueTypeId as Id
			from ValueTypes as v
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetDoctorShowData]...';


GO
CREATE PROCEDURE [dbo].[GetDoctorShowData]
	@doctorId int = 0
AS
	declare @status int
	declare @statusMessage nvarchar(MAX)
	if exists(select * from Doctors where DoctorId = @doctorId)
	begin
		set @status = 1	
		set @statusMessage = dbo.GSM(0000001)		
	end
	else 
	begin
		set @status = 0
		set @statusMessage = dbo.GSM(3001001)
	end	
	select  us.UserId as Id, 
			FirstName, 
			LastName, 
			ThirdName, 
			Login, 
			Password, 
			ro.Name as Role, 
			Birthday,
			Token,
			sp.Name as Specialty,
			@status as Status, @statusMessage as StatusMessage
			from Users as us 
			JOIN Doctors as do ON us.UserId = do.DoctorId
			JOIN Roles as ro ON us.RoleId = ro.RoleId
			JOIN Specialties as sp ON do.SpecialtyId = sp.SpecialtyId	
			WHERE do.DoctorId = @doctorId		
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetMetadataById]...';


GO
CREATE PROCEDURE [dbo].[GetMetadataById]
	@Id int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		SELECT 
			@status as Status,
			@statusMessage as StatusMessage,
			pm.MetadataId as Id,
			pm.ParameterId as ParameterId,
			pm.Value as Value,
			pm.[Key] as [Key],
			pm.ValueTypeId as ValueTypeId,
			p.Name as ParameterName,
			vt.Name as ValueTypeName

		FROM ParameterMetadata as pm,
			Parameters as p,
			ValueTypes as vt
		WHERE pm.MetadataId=@Id and p.ParameterId=pm.ParameterId 
			and vt.ValueTypeId=pm.ValueTypeId
		
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
		select @status as Status, @statusMessage as StatusMessage
	end catch
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetParameterById]...';


GO
CREATE PROCEDURE [dbo].[GetParameterById]
	@parameterId int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try		
		SELECT ParameterId, Name, DefaultValue,
			ParameterId as Id,
			@status as Status,
			@statusMessage as StatusMessage
		from 
		Parameters where ParameterId = @parameterId
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
	end catch
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetPatientFullDataById]...';


GO
CREATE PROCEDURE [dbo].[GetPatientFullDataById]
	@patientId int = 0
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	if not(exists(select * from Patients where PatientId = @patientId))
	begin
		set @statusMessage = dbo.GSM(3001002)
		select @status as Status, @statusMessage as StatusMessage
		return
	end
	select p.PatientId as Id,
		   p.Card,
		   p.Policy,
		   p.Mother,
		   p.Phone1,
		   p.Phone2,
		   p.StartDateOfObservation,
		   fc.Code as FunctionalClassCode,
		   fc.Description as FunctionalClassDescription,
		   u.FirstName,
		   u.LastName,
		   u.ThirdName,
		   u.Login,
		   u.Password,
		   u.Token,
		   r.Name as Role,
		   @status as Status, @statusMessage as StatusMessage		   	   
		   from Patients as p
		   join FunctionalClasses as fc on p.FunctionalClassesId = fc.FunctionalClassesId
		   join Users as u on p.PatientId = u.UserId
		   join Roles as r on u.RoleId = r.RoleId
		   where p.PatientId = @patientId
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GetValueTypeDataById]...';


GO
CREATE PROCEDURE [dbo].[GetValueTypeDataById]
	@ValueTypeId int
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		SELECT 
			@status as Status,
			@statusMessage as StatusMessage,
			vt.ValueTypeId as ValueTypeId,
			vt.ValueTypeId as Id,
			vt.Name as Name	
				
		from ValueTypes as vt
		where vt.ValueTypeId = @ValueTypeId
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
		select @status as Status, @statusMessage as StatusMessage
	end catch
		
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[NewParameter]...';


GO
CREATE PROCEDURE [dbo].[NewParameter]
	@nameParameter nvarchar(max), 
	@defaultValue varbinary(MAX)
AS
	declare @status int =1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		insert into Parameters(Name, DefaultValue) values(@nameParameter, @defaultValue)
	end try
		
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage

RETURN 0
GO
PRINT N'Выполняется создание [dbo].[NewParameterMetadata]...';


GO
CREATE PROCEDURE [dbo].[NewParameterMetadata]
	@ParameterId int,
	@Key nvarchar(max),
	@Value varbinary(max),
	@ValueTypeId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	
	begin try
		insert ParameterMetadata (ParameterId, [Key], Value, ValueTypeId) 
		values (@ParameterId, @Key, @Value, @ValueTypeId)
	end try

	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch

	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[NewValueTypes]...';


GO
CREATE PROCEDURE [dbo].[NewValueTypes]
	@name nvarchar(MAX)
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		insert dbo.ValueTypes(Name) values
		(@name)
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[UpdateParameter]...';


GO
CREATE PROCEDURE [dbo].[UpdateParameter]
	@ParameterId int, 
	@Name nvarchar(MAX),
	@DefaultValue varbinary(MAX)
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update dbo.Parameters
			set Name=@Name,
				DefaultValue=@DefaultValue
			where ParameterId=@ParameterId
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[UpdateParameterMetadata]...';


GO
CREATE PROCEDURE [dbo].[UpdateParameterMetadata]
	@Id int,
	@ParameterId int, 
	@Key nvarchar(MAX),
	@Value varbinary(MAX),
	@ValueTypeId int
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		if EXISTS( select pm.MetadataId						
							from ParameterMetadata as pm
							where pm.MetadataId=@Id)
			begin
				update dbo.ParameterMetadata
					set [Key]=@Key,
						Value=@Value,
						ValueTypeId=@ValueTypeId,
						ParameterId=@ParameterId
					where MetadataId=@Id
			end
		else
		begin
			set @status = 0
			set @statusMessage = dbo.GSM(4001001)
		end
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[UpdateValueTypes]...';


GO
CREATE PROCEDURE [dbo].[UpdateValueTypes]
	@ValueTypeId int, 
	@Name nvarchar(MAX)
AS
	declare @status int = 1
	declare @statusMessage nvarchar(MAX) = dbo.GSM(0000001)
	begin try
		update ValueTypes
			set Name = @Name
			where ValueTypeId=@ValueTypeId
	end try
	begin catch
		set @status = 0
		set @statusMessage = dbo.GSM(0000000)
	end catch
	select @status as Status, @statusMessage as StatusMessage
RETURN 0
GO
PRINT N'Выполняется создание [dbo].[GSM]...';


GO
CREATE FUNCTION [dbo].[GSM]
(
	@code int
)
RETURNS nvarchar(MAX)
AS
BEGIN
	RETURN (select Message from Status where Code = @code)
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
/*
Шаблон скрипта после развертывания							
------------------------------------------------------------------------------------//
 В данном файле содержатся операторы SQL, которые будут добавлены в скрипт построения.		
 Используйте синтаксис SQLCMD для включения файла в скрипт после развертывания.			
 Пример:      :r .\myfile.sql								
 Используйте синтаксис SQLCMD для создания ссылки на переменную в скрипте после развертывания.		
 Пример:      :setvar TableName MyTable							
			   SELECT * FROM [$(TableName)]					
------------------------------------------------------------------------------------//
*/
USE [Health.MsSqlDatabase]
/*Заполняем роли*/
insert into Roles (Name) values('Пациент')
insert into Roles (Name) values('Доктор')
insert into Roles (Name) values('Администратор')

/*заполняем пользователей сайта*/
insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Селигеров', 'Павел', 'Васильевич', 'логин', 'пароль', 1, '1991/12/04', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Швецова', 'Мария', 'Сергеевна', 'логин', 'пароль', 1, '1991/11/11', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Иванов', 'Сергей', 'Иванович', 'логин', 'пароль', 1, '1992/07/10', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Васнецов', 'Иван', 'Львович', 'логин', 'пароль', 1, '2001/04/03', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Иванов', 'Иван', 'Степанович', 'логин', 'пароль', 1, '1999/12/04', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Гаус', 'Никита', 'Анисимович', 'логин', 'пароль', 1, '2002/12/09', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Сухарев', 'Иван', 'Дмитриевич', 'логин', 'пароль', 1, '2003/12/11', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Иванов', 'Олег', 'Юрьевич', 'логин', 'пароль', 1, '2000/02/08', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Старосельцев', 'Олег', 'Александрович', 'логин', 'пароль', 1, '1997/11/01', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Энштейн', 'Альберт', 'Петрович', 'логин', 'пароль', 1, '1991/12/10', 'token_value')

/*Заполняем работников: */
insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Энштейн', 'Альберт', 'Витальевич', 'админ', 'фдмин', 3, '1975/12/10', 'token_value')

insert into Users (LastName, FirstName, ThirdName, Login, Password, RoleId, Birthday, Token)
	values('Дроздов', 'Евгений', 'Павлович', 'логин', 'пароль', 2, '1956/10/08', 'token_value')

/*Заполняем специальности*/
insert into Specialties(Name) values('Врач/кардиолог')
insert into Specialties(Name) values('Врач/терапевт')

/*Заполняем докторов*/
insert into Doctors(DoctorId, SpecialtyId) values(12, 1)

/*Заполняем функциональные классы*/
insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс I', 'пациенты с заболеванием сердца,
	  у которых обычные физические нагрузки не вызывают одышки, утомления или сердцебиения.')

insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс II', 'пациенты с заболеванием сердца и умеренным ограничением
	  физической активности. При обычных физических нагрузках наблюдаются одышка, усталость и сердцебиение.')

insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс III', 'пациенты с заболеванием сердца и выраженным ограничением
	  физической активности. В состоянии покоя жалобы отсутствуют, но даже при незначительных физических
	   нагрузках появляются одышка, усталость и сердцебиение.')

insert into FunctionalClasses(Code, Description)
	 values('Функциональный класс IV', 'пациенты с заболеванием сердца, у которых любой
	  уровень физической активности вызывает указанные выше субъективные симптомы.
	   Последние возникают и в состоянии покоя.')

/*Заполняем пациентов*/
insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 3, 'Анастасия Сергеевна', 1, 34435, 32333, '78935632', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 2, 'Евгения Павловна', 2, 34435, 32333, '64273642', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Лидия Петровна', 3, 34435, 32333, '65426953', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Алла Васильевна', 4, 34435, 32333, '5467253', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 4, 'Юлия Владимировна', 5, 34435, 32333, '5465421', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 3, 'Александра Дмитриевна', 6, 34435, 32333, '87684213', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 2, 'Светлана Тимофеевна', 7, 34435, 32333, '42362389', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Анна Игоревна', 8, 34435, 32333, '5234423', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Татьяна Петровна', 9, 34435, 32333, '2346274', '2001/09/21')

insert Patients(Card, FunctionalClassesId, Mother, PatientId, Phone1, Phone2, Policy, StartDateOfObservation)
	values('---/', 1, 'Марья Ивановна', 10, 34435, 32333, '453259482', '2001/09/21')

/*Заполняем виды операций*/
insert Surgerys(Name, SurgeryDescription) values
('Surgery1',
'Пластика ДМЖП по методике двойной заплаты в условиях ИК.')

insert Surgerys(Name, SurgeryDescription) values
('Surgery2',
' Пластика ДМПП,ДМЖП,ИСЛА,аномалия Эбштейна,радик.коррекц. тетрады Фалло.')

insert Surgerys(Name, SurgeryDescription) values
('Surgery3',
'Реконструкции пути оттока от ПЖ.')

/*Связываем пациентов с их операциями*/
insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(1, 1, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(2, 2, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(3, 3, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(4, 1, 0, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(5, 2, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(6, 3, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(7, 1, 0, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(8, 2, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(9, 3, 1, '2001/12/06')

insert PatientsToSurgerys(PatientId, SurgeryId, SurgeryStatus, SurgeryDate) values
(10, 1, 0, '2001/12/06')

/*Добавляем для пациентов их докторов*/
insert PatientsToDoctors(PatientId, DoctorId) values(1, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(2, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(3, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(4, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(5, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(6, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(7, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(8, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(9, 12)

insert PatientsToDoctors(PatientId, DoctorId) values(10, 12)

/*Добавить кандидатов*/
insert Candidates(Birthday, Card, LastName, FirstName, ThirdName, Login, Password, Mother, Phone1,
Policy, RoleId, Phone2, Token, StartDateOfObservation) values
('1992/07/25', '---', 'Сидоров', 'Андрей', 'Тихвинович',
 'log', 'pass', '', 212, '---', 1, 1232, 'token_value', '1998/07/12')

insert Candidates(Birthday, Card, LastName, FirstName, ThirdName, Login, Password, Mother, Phone1,
Policy, RoleId, Phone2, Token, StartDateOfObservation) values
('1992/07/25', '---', 'Сосницкий', 'Иван', 'Павлович',
 'log', 'pass', '', 212, '---', 1, 1232, 'token_value', '1998/09/12')

insert into DiagnosisClass(Name, Code) values('Болезни системы кровообращения', 'IX')
insert into DiagnosisClass(Name, Code) values('Врожденные аномалии [пороки крови], деформации и хромосомные нарушения', 'XVII')

insert into Diagnosis(Name, Code, DiagnosisClassId) values('Другие функциональные нарушения после операций на сердце', 'I97.1', 1)
insert into Diagnosis(Name, Code, DiagnosisClassId) values('Другие нарушения системы кровообращения после медицинских процедур, не классифицированные в других рубриках', 'I97.8', 1)
insert into Diagnosis(Name, Code, DiagnosisClassId) values('Дефект предсердной перегородки', 'Q21.1', 2)
insert into Diagnosis(Name, Code, DiagnosisClassId) values('Врожденный порок сердца неуточненный', 'Q24.9', 2)

insert into Parameters(Name, DefaultValue) values('Сатурация', 0)
insert into Parameters(Name, DefaultValue) values('Пульс', 0)

insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 1)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 2)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 3)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 4)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 4)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 5)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 6)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 7)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (1, 7)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 8)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 9)
insert PatientsToDiagnosis(DiagnosisId, PatientId) values (2, 10)
-- =============================================
-- Script Template
-- =============================================
/*
Правила формирования кодов статусов:
	Используется семизначный код 0000000, где
	- первая цифра - тип статуса 
		0 - статус
		1 - сообщение
		2 - предупреждение
		3 - ошибка
		4 - критическая ошибка
	- затем 3 цифры - класс ошибки
	- остальные 3 цифры - уникальный код ошибки, для родителя равен 0000
*/

-- Общие
insert into Status values(0000000, 'Все плохо!')
insert into Status values(0000001, 'Все хорошо!')

-- Общие ошибки
insert into Status values(3001000, 'Отсутствует запись в базе для данного идентификатора.')

-- Доктора
	-- Сообщения
	insert into Status values(1001001, 'У доктора нет ведомых пациентов.')
	-- Предупреждения
	insert into Status values(2001001, 'У доктора есть ведомые пациенты.')	
	-- Ошибки
	insert into Status values(3001001, 'Отсутствует информация о докторе в базе.')

-- Пациенты
	-- Ошибки
	insert into Status values(3001002, 'Отсутствует информация о пациенте в базе.')

--Параметры здоровья, и все что с ними связано.
	-- Сообщения
	insert into Status values(4000001, 'Тип метаданных записан.')
	insert into Status values(4001001, 'Такое метаданное отсутствует')
	-- Ошибки

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

ALTER TABLE [dbo].[DiagnosisClass] WITH CHECK CHECK CONSTRAINT [DiagnosisClassMTO];

ALTER TABLE [dbo].[Doctors] WITH CHECK CHECK CONSTRAINT [DoctorsMTOSpecialties];

ALTER TABLE [dbo].[Doctors] WITH CHECK CHECK CONSTRAINT [DoctorsOTOUsers];

ALTER TABLE [dbo].[FunctionalDisorders] WITH CHECK CHECK CONSTRAINT [FunctionalDisordersMTO];

ALTER TABLE [dbo].[FunctionalDisordersToPatients] WITH CHECK CHECK CONSTRAINT [FunctionalDisordersToDiagnosisMTOFunctionalDisorders];

ALTER TABLE [dbo].[FunctionalDisordersToPatients] WITH CHECK CHECK CONSTRAINT [FunctionalDisordersToDiagnosisMTOPatients];

ALTER TABLE [dbo].[ParameterMetadata] WITH CHECK CHECK CONSTRAINT [ParameterMetadataMTOParameter];

ALTER TABLE [dbo].[ParameterMetadata] WITH CHECK CHECK CONSTRAINT [ParameterMetadataMTOValueTypes];

ALTER TABLE [dbo].[ParametersForPatients] WITH CHECK CHECK CONSTRAINT [ParametersMTOPatientsParameter];

ALTER TABLE [dbo].[ParametersForPatients] WITH CHECK CHECK CONSTRAINT [ParametersMTOPatientsPatient];

ALTER TABLE [dbo].[Patients] WITH CHECK CHECK CONSTRAINT [PatientsMTOFunctionalClasses];

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
