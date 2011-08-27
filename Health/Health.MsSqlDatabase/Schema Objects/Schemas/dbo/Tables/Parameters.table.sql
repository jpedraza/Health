CREATE TABLE [dbo].[Parameters]
(
	ParameterId int IDENTITY(1, 1) NOT NULL, 
	Name nvarchar(MAX),
	DefaultValue varbinary
)
