CREATE TABLE [dbo].[ParameterMetadata]
(
	MetadataId int IDENTITY(1, 1) NOT NULL,
	ParameterId int NOT NULL,
	[Key] nvarchar(MAX) NOT NULL,
	Value varbinary(MAX) NULL,
	ValueTypeId int NOT NULL
)
