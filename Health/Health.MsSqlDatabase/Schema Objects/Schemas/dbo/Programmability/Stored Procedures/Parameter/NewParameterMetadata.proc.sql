CREATE PROCEDURE [dbo].[NewParameterMetadata]
	@ParameterId int,
	@Key nvarchar(max),
	@Value varbinary(max),
	@ValueTypeId int
AS
	insert ParameterMetadata (ParameterId, [Key], Value, ValueTypeId) 
	values (@ParameterId, @Key, @Value, @ValueTypeId)
RETURN 0