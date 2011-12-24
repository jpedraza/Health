CREATE PROCEDURE [dbo].[GetAllMetadataForParameter]
	@parameterId int
AS
	select 
	pm.ParameterId,
	pm.Value,
	pm.ValueTypeId,
	pm.[Key]
	from ParameterMetadata as pm
	where
	pm.ParameterId=@parameterId
RETURN 0