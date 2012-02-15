
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/15/2012 19:27:12
-- Generated from EDMX file: D:\BACKUP\ftp\projects\health\Health\EFModel\Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EFHealth];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_RoleUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SpecialtyDoctor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Doctor] DROP CONSTRAINT [FK_SpecialtyDoctor];
GO
IF OBJECT_ID(N'[dbo].[FK_Doctor_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Doctor] DROP CONSTRAINT [FK_Doctor_inherits_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_inherits_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users_Patient] DROP CONSTRAINT [FK_Patient_inherits_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[QueryStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueryStatus];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Specialties]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Specialties];
GO
IF OBJECT_ID(N'[dbo].[Users_Doctor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Doctor];
GO
IF OBJECT_ID(N'[dbo].[Users_Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users_Patient];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'QueryStatus'
CREATE TABLE [dbo].[QueryStatus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [Code] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [ThirdName] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Birthday] datetime  NOT NULL,
    [Token] nvarchar(max)  NOT NULL,
    [Role_Id] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Specialties'
CREATE TABLE [dbo].[Specialties] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Users_Doctor'
CREATE TABLE [dbo].[Users_Doctor] (
    [Id] int  NOT NULL,
    [Specialty_Id] int  NOT NULL
);
GO

-- Creating table 'Users_Patient'
CREATE TABLE [dbo].[Users_Patient] (
    [Policy] nvarchar(max)  NOT NULL,
    [Card] nvarchar(max)  NOT NULL,
    [Mother] nvarchar(max)  NOT NULL,
    [StartDateOfObservation] datetime  NOT NULL,
    [Phone1] nvarchar(max)  NOT NULL,
    [Phone2] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'QueryStatus'
ALTER TABLE [dbo].[QueryStatus]
ADD CONSTRAINT [PK_QueryStatus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Specialties'
ALTER TABLE [dbo].[Specialties]
ADD CONSTRAINT [PK_Specialties]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users_Doctor'
ALTER TABLE [dbo].[Users_Doctor]
ADD CONSTRAINT [PK_Users_Doctor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users_Patient'
ALTER TABLE [dbo].[Users_Patient]
ADD CONSTRAINT [PK_Users_Patient]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Role_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([Role_Id]);
GO

-- Creating foreign key on [Specialty_Id] in table 'Users_Doctor'
ALTER TABLE [dbo].[Users_Doctor]
ADD CONSTRAINT [FK_SpecialtyDoctor]
    FOREIGN KEY ([Specialty_Id])
    REFERENCES [dbo].[Specialties]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SpecialtyDoctor'
CREATE INDEX [IX_FK_SpecialtyDoctor]
ON [dbo].[Users_Doctor]
    ([Specialty_Id]);
GO

-- Creating foreign key on [Id] in table 'Users_Doctor'
ALTER TABLE [dbo].[Users_Doctor]
ADD CONSTRAINT [FK_Doctor_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Users_Patient'
ALTER TABLE [dbo].[Users_Patient]
ADD CONSTRAINT [FK_Patient_inherits_User]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------