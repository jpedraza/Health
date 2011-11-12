CREATE TABLE [dbo].[FunctionalClasses]
(
	FunctionalClassesId int NOT NULL IDENTITY(1, 1), 
	Code nvarchar(MAX) NOT NULL,
	[Description] varchar(MAX) NOT NULL
)
