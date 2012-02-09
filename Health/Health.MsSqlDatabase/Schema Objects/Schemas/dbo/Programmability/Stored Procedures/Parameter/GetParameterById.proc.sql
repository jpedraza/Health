CREATE PROCEDURE [dbo].[GetParameterById]
	@parameterId int
AS
	SELECT ParameterId, Name, DefaultValue from 
	Parameters where ParameterId = @parameterId
RETURN 0