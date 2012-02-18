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