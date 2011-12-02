CREATE TABLE [dbo].[ParameterMetadata]
(
	ParameterId int NOT NULL, 
	[Key] nvarchar(MAX) NOT NULL,
	Value varbinary(MAX) NULL,
	ValueTypeId int NOT NULL
)
